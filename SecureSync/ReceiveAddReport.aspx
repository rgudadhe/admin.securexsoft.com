<%@ Page Language="VB" %>

<%@ Import Namespace="SoftArtisans.Net" %>
<%@ Import Namespace="system.data.sqlclient" %>
<script runat="server">
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim ProcFolder As String = Server.MapPath("../ETS_Files")
        Dim strFileName As String
        Dim Matched As String
        Dim upl As New SoftArtisans.Net.FileUp(Context)
        strFileName = ProcFolder & "\Transcriptions\" & upl.Form("FileID").ShortFileName
        upl.Form("FileID").saveas(strFileName)
        Dim OFM As New IO.FileInfo(strFileName)
        If OFM.Exists Then
            If OFM.Length = upl.Form("FileID").TotalBytes Then
                Matched = "Yes"
                Dim strConn As String
                strConn = System.Configuration.ConfigurationManager.AppSettings("ETSCon")
                Dim strQuery As String
                Dim SWVersion As Integer = 0
                strQuery = "SELECT isnull(SWVersion, 0) as swversion from DBO.tbltranscriptionmain where TranscriptionID = '" & Request.Form("TransID") & "' "
                Dim SQLCmdT As New System.Data.SqlClient.SqlCommand(strQuery, New System.Data.SqlClient.SqlConnection(strConn))
                Try
                    SQLCmdT.Connection.Open()
                    Dim DRRecT As System.Data.SqlClient.SqlDataReader = SQLCmdT.ExecuteReader()
                    If DRRecT.HasRows Then
                        If DRRecT.Read Then
                            SWVersion = DRRecT("swversion").ToString
                        End If
                    End If
                Finally
                    If SQLCmdT.Connection.State = Data.ConnectionState.Open Then
                        SQLCmdT.Connection.Close()
                    End If
                End Try
                If KeepVersion(Request.Form("TransID"), SWVersion, Request.Form("AccID"), Request.Form("UserLogin"), Request.Form("REMOTE_ADDR")) Then
                    Response.Write("Yes")
                Else
                    Response.Write("No")
                End If

            End If
        End If
        upl.Dispose()
        'Response.Write("#" & Matched)
    End Sub
    Function KeepVersion(ByVal transid As String, ByVal SWversion As Integer, ByVal AccID As String, ByVal UserLogin As String, ByVal IPadd As String) As Boolean
        Dim ProcFolder As String = Server.MapPath("../ETS_Files")
        Dim strConn As String = System.Configuration.ConfigurationManager.AppSettings("ETSCon")
        Dim sQuery As String
        Dim CurrVersion As Integer = SWversion + 1
        Dim SQLConn1 As New SqlConnection(strConn)
        Dim Trans1 As SqlTransaction
        Dim Recupdate1 As Integer = 0
        Dim Recupdate2 As Integer = 0
        Dim SrcFileName As String = ProcFolder & "\Transcriptions\" & transid & ".doc"
        Dim DestFileName As String = ProcFolder & "\Transcriptions\SWVersion\" & transid & ".doc." & CurrVersion
        Dim SrcFile As New IO.FileInfo(SrcFileName)
        If SrcFile.Exists Then
            SrcFile.CopyTo(DestFileName, True)
        End If
        Try
            SQLConn1.Open()
            Trans1 = SQLConn1.BeginTransaction
            sQuery = "update DBO.tblTranscriptionMain set Cstatus = '111', SWVersion='" & CurrVersion & "'  where TranscriptionID = '" & transid & "'"
            Dim cmdUp1 As New SqlCommand(sQuery, SQLConn1)
            cmdUp1.Transaction = Trans1
            Recupdate1 = cmdUp1.ExecuteNonQuery()
            sQuery = "INSERT INTO SecureWeb.dbo.tblRecordLog(AccID, transcriptionID, IPAdd, TAction, loginID, ActDate, website, version) VALUES ('" & AccID & "','" & transid & "', '" & IPadd & "' ,'Addendum Report', '" & UserLogin & "', '" & Now & "', '1', '" & CurrVersion & "')"
            Dim cmdUp As New SqlCommand(sQuery, SQLConn1)
            cmdUp.Transaction = Trans1
            Recupdate2 = cmdUp.ExecuteNonQuery()
            If Recupdate1 > 0 And Recupdate2 > 0 Then
                Trans1.Commit()
            Else
                Trans1.Rollback()
            End If
            Return True
        Catch ex As Exception
            Return False
        Finally
            If SQLConn1.State = System.Data.ConnectionState.Open Then
                SQLConn1.Close()
            End If
        End Try
        'Return False
    End Function
</script>
