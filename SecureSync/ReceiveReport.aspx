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
                Dim sQuery1 As String
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
                strQuery = "SELECT * from SecureWeb.dbo.tblRecordLog where TAction ='Pending Review' and  TranscriptionID = '" & Request.Form("TransID") & "' "
                Dim SQLCmd1 As New System.Data.SqlClient.SqlCommand(strQuery, New System.Data.SqlClient.SqlConnection(strConn))
                Try
                    SQLCmd1.Connection.Open()
                    Dim DRRec1 As System.Data.SqlClient.SqlDataReader = SQLCmd1.ExecuteReader()
                    If DRRec1.HasRows Then
                        If DRRec1.Read Then
                            If DRRec1("loginID").ToString = Request.Form("UserLogin") Then
                                If KeepVersion(Request.Form("TransID"), SWVersion, True, False, Request.Form("AccID"), Request.Form("UserLogin"), Request.Form("REMOTE_ADDR")) Then
                                    Response.Write("Yes")
                                Else
                                    Response.Write("No")
                                End If
                            Else
                                If KeepVersion(Request.Form("TransID"), SWVersion, True, True, Request.Form("AccID"), Request.Form("UserLogin"), Request.Form("REMOTE_ADDR")) Then
                                    Response.Write("Yes")
                                Else
                                    Response.Write("No")
                                End If
                            End If

                        End If

                    Else
                        If KeepVersion(Request.Form("TransID"), SWVersion, True, False, Request.Form("AccID"), Request.Form("UserLogin"), Request.Form("REMOTE_ADDR")) Then
                            Response.Write("Yes")
                        Else
                            Response.Write("No")
                        End If
                           
                    End If
                    DRRec1.Close()
                Finally
                    If SQLCmd1.Connection.State = System.Data.ConnectionState.Open Then
                        SQLCmd1.Connection.Close()
                    End If
                    
                End Try

                
            End If
        End If
        upl.Dispose()
        'Response.Write("Error" & Err.Description)
    End Sub
    Function KeepVersion(ByVal transid As String, ByVal SWversion As Integer, ByVal modified As Boolean, ByVal FReview As Boolean, ByVal AccID As String, ByVal UserLogin As String, ByVal IPadd As String) As Boolean
        Dim ProcFolder As String = Server.MapPath("../ETS_Files")
        Dim strConn As String = System.Configuration.ConfigurationManager.AppSettings("ETSCon")
        Dim sQuery As String
        Dim CurrVersion As Integer = SWversion + 1
        Dim SQLConn1 As New System.Data.SqlClient.SqlConnection(strConn)
        Dim Trans1 As System.Data.SqlClient.SqlTransaction
        Dim Recupdate1 As Integer = 0
        Dim Recupdate2 As Integer = 0
        Dim Recupdate3 As Integer = 0
        Dim SrcFileName As String = ProcFolder & "\Transcriptions\" & transid & ".doc"
        Dim DestFileName As String = ProcFolder & "\Transcriptions\SWVersion\" & transid & ".doc." & CurrVersion
        Dim SrcFile As New System.IO.FileInfo(SrcFileName)
        If SrcFile.Exists Then
            SrcFile.CopyTo(DestFileName, True)
        End If
        Try
            SQLConn1.Open()
            Trans1 = SQLConn1.BeginTransaction
            If FReview = True Then
                sQuery = "update DBO.tblTranscriptionMain set Modified=1, review=0, FReview=1, SWVersion='" & CurrVersion & "'  where TranscriptionID = '" & transid & "'"
            Else
                sQuery = "update DBO.tblTranscriptionMain set Modified=1, SWVersion='" & CurrVersion & "'  where TranscriptionID = '" & transid & "'"
            End If
            Dim cmdUp1 As New System.Data.SqlClient.SqlCommand(sQuery, SQLConn1)
            cmdUp1.Transaction = Trans1
            Recupdate1 = cmdUp1.ExecuteNonQuery()
            sQuery = "INSERT INTO SecureWeb.dbo.tblRecordLog(AccID, transcriptionID, IPAdd, TAction, loginID, ActDate, website, version) VALUES ('" & AccID & "','" & transid & "', '" & IPadd & "' ,'Save Report', '" & UserLogin & "', '" & Now & "', '1', '" & CurrVersion & "')"
            Dim cmdUp As New System.Data.SqlClient.SqlCommand(sQuery, SQLConn1)
            cmdUp.Transaction = Trans1
            Recupdate2 = cmdUp.ExecuteNonQuery()
            If FReview = True Then
                sQuery = "INSERT INTO SecureWeb.dbo.tblRecordLog(AccID, transcriptionID, IPAdd, TAction, loginID, ActDate, website, version) VALUES ('" & AccID & "','" & transid & "', '" & IPadd & "' ,'Finished Review', '" & UserLogin & "', '" & Now & "', '1', '" & CurrVersion & "')"
                Dim cmdUp2 As New System.Data.SqlClient.SqlCommand(sQuery, SQLConn1)
                cmdUp2.Transaction = Trans1
                Recupdate3 = cmdUp.ExecuteNonQuery()
                If Recupdate1 > 0 And Recupdate2 > 0 And Recupdate3 > 0 Then
                    Trans1.Commit()
                    Return True
                Else
                    Trans1.Rollback()
                    Return False
                End If
            Else
                If Recupdate1 > 0 And Recupdate2 > 0 Then
                    Trans1.Commit()
                    Return True
                Else
                    Trans1.Rollback()
                    Return False
                End If
            End If
            
            
        Catch ex As Exception
            Return ex.Message
        Finally
            If SQLConn1.State = System.Data.ConnectionState.Open Then
                SQLConn1.Close()
            End If
        End Try
        Return False
    End Function
</script>
