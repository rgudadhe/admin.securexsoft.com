<%@ Page Language="VB" %>
<%@ Import Namespace="SoftArtisans.Net" %>
<%@ Import Namespace="system.data.sqlclient" %>
<script runat="server">
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            
       
            Dim ProcFolder As String = Server.MapPath("../ETS_Files")
            Dim strFileName As String
            Dim Matched As Boolean = False
            Dim upl As New SoftArtisans.Net.FileUp(Context)
            strFileName = ProcFolder & "\Templates\" & upl.Form("FileID").ShortFileName
            upl.Form("FileID").saveas(strFileName)
            Dim OFM As New IO.FileInfo(strFileName)
            If OFM.Exists Then
                If OFM.Length = upl.Form("FileID").TotalBytes Then
                    Matched = True
               
              
                
                End If
            End If
            If Matched Then
                Dim Wrd1 As New Word.Application
                Dim chkPath As String = strFileName
                Dim IsDocBlank As Boolean = False
                Try
                    Wrd1.Documents.Open(chkPath)
                    Response.Write("Yes")
                Catch ex As Exception
                    Response.Write(ex.Message)
           
                Finally

                    Wrd1.Quit(False)


                End Try
            End If
        
            upl.Dispose()
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
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
        Dim Recupdate4 As Integer = 0
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
                sQuery = "update secureweb.dbo.tblTranscriptionActions set Modified=1, review=0, FReview=1  where TranscriptionID = '" & transid & "'"
            Else
                sQuery = "update secureweb.dbo.tblTranscriptionActions set Modified=1  where TranscriptionID = '" & transid & "'"
            End If
            Dim cmdUp1 As New System.Data.SqlClient.SqlCommand(sQuery, SQLConn1)
            cmdUp1.Transaction = Trans1
            Recupdate1 = cmdUp1.ExecuteNonQuery()
            sQuery = "update secureweb.dbo.tblTranscriptionCStatus set SWVersion='" & CurrVersion & "'  where TranscriptionID = '" & transid & "'"
            Dim cmdUp4 As New System.Data.SqlClient.SqlCommand(sQuery, SQLConn1)
            cmdUp4.Transaction = Trans1
            Recupdate4 = cmdUp4.ExecuteNonQuery()
            sQuery = "INSERT INTO SecureWeb.dbo.tblRecordLog(AccID, transcriptionID, IPAdd, TAction, loginID, ActDate, website, version) VALUES ('" & AccID & "','" & transid & "', '" & IPadd & "' ,'Save Report', '" & UserLogin & "', '" & Now & "', 0, '" & CurrVersion & "')"
            Dim cmdUp As New System.Data.SqlClient.SqlCommand(sQuery, SQLConn1)
            cmdUp.Transaction = Trans1
            Recupdate2 = cmdUp.ExecuteNonQuery()
            UpAuditLog(transid, IPadd, AccID, UserLogin, "Save Report")
            If FReview = True Then
                sQuery = "INSERT INTO SecureWeb.dbo.tblRecordLog(AccID, transcriptionID, IPAdd, TAction, loginID, ActDate, website, version) VALUES ('" & AccID & "','" & transid & "', '" & IPadd & "' ,'Finished Review', '" & UserLogin & "', '" & Now & "', 0, '" & CurrVersion & "')"
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
                UpAuditLog(transid, IPadd, AccID, UserLogin, "Finished Review")
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
            Return False
        Finally
            If SQLConn1.State = System.Data.ConnectionState.Open Then
                SQLConn1.Close()
            End If
        End Try
        Return False
    End Function
    
    
    Protected Sub UpAuditLog(ByVal TransID As String, ByVal IPAdd As String, ByVal AccID As String, ByVal strName As String, ByVal Taction As String)
        Dim squery
        Dim strConn As String
        Dim strQuery As String
        strConn = System.Configuration.ConfigurationManager.AppSettings("ETSCon")
        strQuery = "SELECT TranscriptionID from SecureWeb.dbo.tblauditLog  Where TranscriptionID = '" & TransID & "' "
        Dim SQLCmd1 As New SqlCommand(strQuery, New SqlConnection(strConn))
        Try
            SQLCmd1.Connection.Open()
            Dim DRRec1 As SqlDataReader = SQLCmd1.ExecuteReader()
            If DRRec1.HasRows Then
                squery = "Update SecureWeb.dbo.tblAuditLog SET IPAdd='" & IPAdd & "', TAction='" & Taction & "', loginID ='" & strName & "', ActDate='" & Now & "'  Where TranscriptionID = '" & TransID & "'  "
                Dim cmdUp As New SqlCommand(squery, New SqlConnection(strConn))
                Try
                    cmdUp.Connection.Open()
                    cmdUp.ExecuteNonQuery()
                Finally
                    If cmdUp.Connection.State = System.Data.ConnectionState.Open Then
                        cmdUp.Connection.Close()
                    End If
                End Try
            Else
                squery = "INSERT INTO SecureWeb.dbo.tblAuditLog(AccID, transcriptionID, IPAdd, TAction, loginID, ActDate) VALUES ('" & AccID & "','" & TransID & "', '" & IPAdd & "' ,'" & Taction & "', '" & strName & "', '" & Now & "')"
                Dim cmdUp As New SqlCommand(squery, New SqlConnection(strConn))
                Try
                    cmdUp.Connection.Open()
                    cmdUp.ExecuteNonQuery()
                Finally
                    If cmdUp.Connection.State = System.Data.ConnectionState.Open Then
                        cmdUp.Connection.Close()
                    End If
                End Try

            End If
        Finally
            If SQLCmd1.Connection.State = System.Data.ConnectionState.Open Then
                SQLCmd1.Connection.Close()
            End If
        End Try

    End Sub

</script>
