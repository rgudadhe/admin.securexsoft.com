<%@ Page Language="VB" %>
<%@ Import Namespace="SoftArtisans.Net" %>
<script runat="server">
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim oFileUp As New FileUp(Context)
        Try
            Dim strCustJob As String = String.Empty
            Dim strFileEx As String = String.Empty
            Dim AccID As String = String.Empty
            
            oFileUp.Path = Server.MapPath("/ETS_Files/Temp")
            oFileUp.SaveAs(oFileUp.ShortFilename)
            AccID = Request.Form("AccID")
            If String.IsNullOrEmpty(AccID) Then
                Response.Write("7")
                Exit Sub
            End If
            Dim oFile As New IO.FileInfo(Server.MapPath("/ETS_Files/Temp") & "/" & Request.Form("DemoName"))
            If oFile.Exists Then
                If oFile.Length.ToString = Request.Form("DemoSize") Then
                    Dim ConString
                    ConString = System.Configuration.ConfigurationManager.AppSettings("ETSCon")
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
                        Dim sqlString As String = "INSERT INTO SecureWeb.dbo.tblDemoLog(AccID, FileName, Filetype, Size, Path, UploadDate, location, LoginID) " & _
                        "VALUES ('" & AccID & "','" & oFile.Name.ToString & "', '" & strFileEx & "', '" & (oFile.Length / 1024).ToString("0.00") & "' ,'" & oFileUp.UserFilename & "', '" & Now & "', '" & Request.ServerVariables("REMOTE_ADDR") & "', '" & Request.Form("LoginID") & "')"
                        Dim oCommand As New Data.SqlClient.SqlCommand(sqlString, oConn)
                        oCommand.Transaction = thisTransaction
                        Dim Res As Integer = oCommand.ExecuteNonQuery
                        If Res > 0 Then
                            If IO.File.Exists(IO.Path.Combine(Server.MapPath("/ETS_Files/SecureWeb/Demo"), Request.Form("UserName") & "_" & strCustJob & strFileEx)) Then
                                IO.File.Delete(IO.Path.Combine(Server.MapPath("/ETS_Files/SecureWeb/Demo"), Request.Form("UserName") & "_" & strCustJob & strFileEx))
                            End If
                            oFile.MoveTo(IO.Path.Combine(Server.MapPath("/ETS_Files/SecureWeb/Demo"), Request.Form("UserName") & "_" & strCustJob & strFileEx))
                            If IO.File.Exists(IO.Path.Combine(Server.MapPath("/ETS_Files/SecureWeb/Demo"), Request.Form("UserName") & "_" & strCustJob & strFileEx)) Then
                                thisTransaction.Commit()
                                Response.Write("1")
                            Else
                                thisTransaction.Rollback()
                                Response.Write("3")
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
            oFileUp.Dispose()
        Catch ex As Exception
            Response.Write("4")
        Finally
            oFileUp.Dispose()
        End Try
    End Sub
</script>

