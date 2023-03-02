<%@ Page Language="VB" %>
<%@ Import Namespace="SoftArtisans.Net" %>
<script runat="server">
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim oFileUp As New FileUp(Context)
        Try
            Server.ScriptTimeout = 1200
            Session.Timeout = 1200
            Dim strPriority = String.Empty
            Dim strCustJob As String = String.Empty
            Dim strFileEx As String = String.Empty
            Dim AccID As String = String.Empty
            Dim strQuery As String = String.Empty
            Dim CustVRec As Boolean = False
            Dim ConString
            ConString = System.Configuration.ConfigurationManager.AppSettings("ETSCon")
          
            oFileUp.Path = Server.MapPath("/ETS_Files/Temp")
            strPriority = Request.Form("Priority")
            If String.IsNullOrEmpty(strPriority) Then
                strPriority = ""
            End If
            AccID = Request.Form("AccID")
            strQuery = "select VFolder from DBO.tblaccounts where accountid = '" & AccID & "'"
            Dim SQLCmd As New System.Data.SqlClient.SqlCommand(strQuery, New System.Data.SqlClient.SqlConnection(ConString))
            Try
                SQLCmd.Connection.Open()
                Dim DRRec As System.Data.SqlClient.SqlDataReader = SQLCmd.ExecuteReader()
                If DRRec.HasRows Then
                    If DRRec.Read Then
                        If DRRec("VFolder").ToString = "True" Then
                            CustVRec = True
                        End If
                    End If
                End If
                DRRec.Close()
            Finally
                If SQLCmd.Connection.State = System.Data.ConnectionState.Open Then
                    SQLCmd.Connection.Close()
                End If
            End Try
            
            If String.IsNullOrEmpty(AccID) Then
                Response.Write("7")
                Exit Sub
            End If
            oFileUp.SaveAs(oFileUp.ShortFilename)
            Dim oFile As New IO.FileInfo(Server.MapPath("/ETS_Files/Temp") & "/" & Request.Form("DictName"))
            If oFile.Exists Then
                If oFile.Length.ToString = Request.Form("DictSize") Then
            
                    Dim oConn As New Data.SqlClient.SqlConnection
                    Dim thisTransaction As Data.SqlClient.SqlTransaction
                    oConn.ConnectionString = ConString
                    oConn.Open()
                    thisTransaction = oConn.BeginTransaction()
                    Try
                        If Len(oFile.Extension) > 0 Then
                            strCustJob = oFile.Name.Replace(oFile.Extension, "").ToString()
                            strFileEx = oFile.Extension
                        Else
                            strCustJob = oFile.Name
                        End If
                        Dim sqlString As String = "INSERT INTO SecureWeb.dbo.tblVoiceLog(AccID, FileName, Filetype, Size, Path, UploadDate, location, loginid) " & _
                        "VALUES ('" & AccID & "','" & oFile.Name.ToString & "', '" & strFileEx & "', '" & (oFile.Length / 1024).ToString("0.00") & "' ,'" & oFileUp.UserFilename & "', '" & Now & "', '" & Request.ServerVariables("REMOTE_ADDR") & "', '" & Request.Form("LoginID") & "')"
                        Dim oCommand As New Data.SqlClient.SqlCommand(sqlString, oConn)
                        oCommand.Transaction = thisTransaction
                        Dim Res As Integer = oCommand.ExecuteNonQuery
                        If Res > 0 Then
                            If CustVRec = True Then
                                If Not IO.Directory.Exists(Server.MapPath("/ETS_Files/CustVoice/" & Request.Form("UserName"))) Then
                                    IO.Directory.CreateDirectory(Server.MapPath("/ETS_Files/CustVoice/" & Request.Form("UserName")))
                                End If
                                If IO.File.Exists(IO.Path.Combine(Server.MapPath("/ETS_Files/CustVoice/" & Request.Form("UserName")), Request.Form("UserName") & "_" & strCustJob & "_" & Request.Form("ParentFolder") & "_" & strPriority & strFileEx)) Then
                                    IO.File.Delete(IO.Path.Combine(Server.MapPath("/ETS_Files/CustVoice/" & Request.Form("UserName")), Request.Form("UserName") & "_" & strCustJob & "_" & Request.Form("ParentFolder") & "_" & strPriority & strFileEx))
                                End If
                                oFile.MoveTo(IO.Path.Combine(Server.MapPath("/ETS_Files/CustVoice/" & Request.Form("UserName")), Request.Form("UserName") & "_" & strCustJob & "_" & Request.Form("ParentFolder") & "_" & strPriority & strFileEx))
                                If IO.File.Exists(IO.Path.Combine(Server.MapPath("/ETS_Files/CustVoice/" & Request.Form("UserName")), Request.Form("UserName") & "_" & strCustJob & "_" & Request.Form("ParentFolder") & "_" & strPriority & strFileEx)) Then
                                    thisTransaction.Commit()
                                    Response.Write("1")
                                Else
                                    thisTransaction.Rollback()
                                    Response.Write("3")
                                End If
                            Else
                                If IO.File.Exists(IO.Path.Combine(Server.MapPath("/ETS_Files/DSSInBound"), Request.Form("UserName") & "_" & strCustJob & "_" & Request.Form("ParentFolder") & "_" & strPriority & strFileEx)) Then
                                    IO.File.Delete(IO.Path.Combine(Server.MapPath("/ETS_Files/DSSInBound"), Request.Form("UserName") & "_" & strCustJob & "_" & Request.Form("ParentFolder") & "_" & strPriority & strFileEx))
                                End If
                                oFile.MoveTo(IO.Path.Combine(Server.MapPath("/ETS_Files/DSSInBound"), Request.Form("UserName") & "_" & strCustJob & "_" & Request.Form("ParentFolder") & "_" & strPriority & strFileEx))
                                If IO.File.Exists(IO.Path.Combine(Server.MapPath("/ETS_Files/DSSInBound"), Request.Form("UserName") & "_" & strCustJob & "_" & Request.Form("ParentFolder") & "_" & strPriority & strFileEx)) Then
                                    thisTransaction.Commit()
                                    Response.Write("1")
                                Else
                                    thisTransaction.Rollback()
                                    Response.Write("3")
                                End If
                            End If
                            
                        Else
                            thisTransaction.Rollback()
                            Response.Write("6")
                        End If
                        oConn.Close()
                    Catch ex As Exception
                        thisTransaction.Rollback()
                        Response.Write("5")
                    End Try
                Else
                    Response.Write("2")
                End If
            Else
                Response.Write("0")
            End If
            ' oFileUp.Dispose()
        Catch ex As Exception
            Response.Write("4")
        Finally
            oFileUp.Dispose()
        End Try
    End Sub
</script>

