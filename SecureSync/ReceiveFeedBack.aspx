<%@ Page Language="VB" %>

<%@ Import Namespace="SoftArtisans.Net" %>
<%@ Import Namespace="system.data.sqlclient" %>
<script runat="server">
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim oFileUp As New FileUp(Context)
        Try
            Dim strCustJob As String = String.Empty
            Dim strFileEx As String = String.Empty
            Dim AccID As String = String.Empty
           
            Dim strQuery As String
            Dim strfileName As String
            
            Dim strConn As String
            Dim sQuery1 As String
            Dim TransID As String
            Dim SWversion As Integer
            strConn = System.Configuration.ConfigurationManager.AppSettings("ETSCon")
            'oFileUp.Path = Server.MapPath("/ETS_Files/transcription")
            oFileUp.Path = "\\win11617\d$\ets\transcriptions"
            Dim i As Integer
            i = 0
            Dim JobNum() As String = Split(oFileUp.ShortFilename, "_")
            Dim StrJobNum As String
            Dim RName As String = JobNum(UBound(JobNum))
            'Response.Write(JobNum(UBound(JobNum)))
            If InStr(JobNum(UBound(JobNum)), ".doc") <> 0 Then
                StrJobNum = Mid(JobNum(UBound(JobNum)), 1, Len(JobNum(UBound(JobNum))) - 4)
                'Response.Write(Mid(JobNum(UBound(JobNum)), 1, Len(JobNum(UBound(JobNum))) - 4))
            End If
            
            If StrJobNum <> "" Then
                strQuery = "SELECT transcriptionid, JobNumber,TemplateID,isnull(swversion,0) as swversion from  DBO.tblTranscriptionMain Where Accountid='" & Request.Form("AccID") & "' and JobNumber = '" & StrJobNum & "' "
                'Response.Write(strQuery)
                Dim SQLCmd1 As New SqlCommand(strQuery, New SqlConnection(strConn))
                SQLCmd1.Connection.Open()
                Dim DRRec1 As SqlDataReader = SQLCmd1.ExecuteReader()
                If DRRec1.HasRows Then
                    If DRRec1.Read Then
                        TransID = DRRec1("transcriptionid").ToString
                        strfileName = "\\win11617\d$\ets\transcriptions\" & TransID & ".doc"
                        Dim OFM1 As New IO.FileInfo(strfileName)
                        If OFM1.Exists Then
                            OFM1.CopyTo(strfileName & "_p", True)
                        End If
                        oFileUp.SaveAs(TransID & ".doc")
                        Dim OFM As New IO.FileInfo(strfileName)
                        If OFM.Exists Then
                            'serverFile = OFM.Length
                            If OFM.Length = oFileUp.Form("Dictations").TotalBytes Then
                                
                                If System.IO.File.Exists(strfileName) Then
                                   
                                    Dim RecFound As Boolean = False
                                    Dim strFailed As Boolean = False
                                    Try
                                        
                                        If DRRec1("templateID").ToString = "" Then
                                            RecFound = False
                                        Else
                                            RecFound = True
                                        End If
                                        Dim DocHand As New HandleWordControl.WordMethods
                                        'If DocHand.SignReport(strfileName, RecFound) = True Then
                                        '    strFailed = True
                                        'Else
                                        '    strFailed = False
                                        'End If
                                    Catch ex As Exception
                                        strFailed = False
                                    End Try
                                    If strFailed = True Then
                                        SWversion = DRRec1("swversion").ToString
                                        If KeepVersion(TransID, SWversion, Request.Form("AccID"), Request.Form("UserLogin"), Request.Form("REMOTE_ADDR")) Then
                                            'Response.Write("Yes")
                                        Else
                                            Response.Write("No")
                                        End If
                                        'Response.Write("Yes")
                                    Else
                                        Response.Write("No")
                                    End If

                                End If
                            End If
                        End If
                    End If
                End If
                DRRec1.Close()
                SQLCmd1.Connection.Close()
            End If
            Response.Write("1")
        Catch ex As Exception
            Response.Write(ex.Message)
        Finally
            oFileUp.Dispose()
        End Try
    End Sub
    
    Function KeepVersion(ByVal transid As String, ByVal SWversion As Integer, ByVal AccID As String, ByVal UserLogin As String, ByVal IPadd As String) As Boolean
        Dim ProcFolder As String = Server.MapPath("../ETS_Files")
        Dim strConn As String = System.Configuration.ConfigurationManager.AppSettings("ETSCon")
        Dim sQuery As String
        Dim CurrVersion As Integer = SWversion + 1
        Dim SQLConn1 As New System.Data.SqlClient.SqlConnection(strConn)
        Dim Trans1 As System.Data.SqlClient.SqlTransaction
        Dim Recupdate1 As Integer = 0
        Dim Recupdate2 As Integer = 0
        Dim SrcFileName As String = ProcFolder & "\Transcriptions\" & transid & ".doc"
        Dim DestFileName As String = ProcFolder & "\Transcriptions\SWVersion\" & transid & ".doc." & CurrVersion
        Dim SrcFile As New System.IO.FileInfo(SrcFileName)
        If SrcFile.Exists Then
            SrcFile.CopyTo(DestFileName, True)
        End If
        Try
            SQLConn1.Open()
            Trans1 = SQLConn1.BeginTransaction
            sQuery = "update DBO.tblTranscriptionMain set Cstatus = '222', SignedDate = '" & Now() & "', SWVersion='" & CurrVersion & "'  where TranscriptionID = '" & transid & "'"
            Dim cmdUp1 As New System.Data.SqlClient.SqlCommand(sQuery, SQLConn1)
            cmdUp1.Transaction = Trans1
            Recupdate1 = cmdUp1.ExecuteNonQuery()
            sQuery = "INSERT INTO SecureWeb.dbo.tblRecordLog(AccID, transcriptionID, IPAdd, TAction, loginID, ActDate, website, version) VALUES ('" & AccID & "','" & transid & "', '" & IPadd & "' ,'Sign Report', '" & UserLogin & "', '" & Now & "', '1', '" & CurrVersion & "')"
            Dim cmdUp As New System.Data.SqlClient.SqlCommand(sQuery, SQLConn1)
            cmdUp.Transaction = Trans1
            Recupdate2 = cmdUp.ExecuteNonQuery()
            If Recupdate1 > 0 And Recupdate2 > 0 Then
                Trans1.Commit()
            Else
                Trans1.Rollback()
            End If
            Return True
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
