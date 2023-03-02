
Partial Class Securefax_UploadCertificate
    Inherits System.Web.UI.Page
    Dim varStrUserID As String = String.Empty
    Dim varStrAction As String = String.Empty
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            'Page.Form.Enctype = "multipart/form-data"
            Dim objConn As New Data.SqlClient.SqlConnection
            objConn = OpenConnection(objConn)
            Try

                If Trim(UCase(Request.QueryString("Action"))) = Trim(UCase("Delete")) Then
                    Dim varFileUploadPath As String = String.Empty
                    varFileUploadPath = "\\win11617\h$\ETS\FAXDocuments\Certificates\"

                    If System.IO.File.Exists(varFileUploadPath & Trim(Request.QueryString("UserId")) & ".pfx") Then
                        System.IO.File.Delete(varFileUploadPath & Trim(Request.QueryString("UserId")) & ".pfx")

                        Dim objCmdDel As New Data.SqlClient.SqlCommand("DELETE FROM SecureFax.dbo.tblUserCertificates WHERE UserId='" & Trim(Request.QueryString("UserId")) & "'", objConn)
                        objCmdDel.ExecuteNonQuery()
                    End If

                    Upload.Visible = False

                    Response.Write("<center><font face=""Trebuchet MS"" size=""2"" color=""#000080"">Certificate deleted sucessfully </font></center>")
                    Response.Write("<center><a href=""../LeaveAttendanceMainNew/CloseWindow.aspx""><font face=""Trebuchet MS"" size=""2"" color=""#000080"">Close Window</font></a></center>")
                    Response.End()

                End If
            Catch ex As Exception
            Finally
                If objConn.State <> Data.ConnectionState.Closed Then
                    objConn.Close()
                    objConn = Nothing
                End If
            End Try
        End If
    End Sub
    Public Function OpenConnection(ByRef Conn As Data.SqlClient.SqlConnection) As Data.SqlClient.SqlConnection
        'Conn.ConnectionString = System.Configuration.ConfigurationManager.AppSettings("ETSCon")
        Conn.ConnectionString = "Server=sqlmain\one;Database=ETS;UID=usersqlbkp;Pwd=y0u4@209#"
        Conn.Open()
        Return Conn
    End Function
    Protected Sub btnSubmit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSubmit.Click
        Dim objConn As New Data.SqlClient.SqlConnection
        objConn = OpenConnection(objConn)

        Try
            varStrUserID = Trim(Request.QueryString("UserId")).ToString
            varStrAction = Trim(Request.QueryString("Action")).ToString

            'Response.Write(varStrUserID)
            'Response.Write(varStrAction)
            'Response.End()
            If Not String.IsNullOrEmpty(varStrUserID) And Not String.IsNullOrEmpty(varStrAction) Then
                Dim varFileUploadPath As String = String.Empty
                varFileUploadPath = "\\win11617\h$\ETS\FAXDocuments\Certificates\"


                If Trim(UCase(varStrAction)) = Trim(UCase("Add")) Then
                    If System.IO.File.Exists(varFileUploadPath & varStrUserID & System.IO.Path.GetExtension(FileUpload.FileName)) Then
                        System.IO.File.Delete(varFileUploadPath & varStrUserID & System.IO.Path.GetExtension(FileUpload.FileName))
                    End If

                    FileUpload.PostedFile.SaveAs(varFileUploadPath & varStrUserID & System.IO.Path.GetExtension(FileUpload.FileName))

                    If System.IO.File.Exists(varFileUploadPath & varStrUserID & System.IO.Path.GetExtension(FileUpload.FileName)) Then
                        'Response.Write("INSERT INTO SecureFax.dbo.tblUserCertificates (UserID,UpdateDate,UpdateBy,Active) VALUES('" & varStrUserID & "','" & Now() & "','" & Session("UserID").ToString & "',1)")
                        'Response.End()
                        Dim InsertCmd As Data.SqlClient.SqlCommand
                        InsertCmd = New Data.SqlClient.SqlCommand
                        InsertCmd.CommandType = Data.CommandType.Text

                        InsertCmd.CommandText = "INSERT INTO SecureFax.dbo.tblUserCertificates (UserID,UpdateDate,UpdateBy) VALUES('" & varStrUserID & "','" & Now() & "','" & Session("UserID").ToString & "')"
                        InsertCmd.Connection = objConn
                        InsertCmd.ExecuteNonQuery()

                        Response.Write("<center><font face=""Trebuchet MS"" size=""2"" color=""#000080"">Certificate added sucessfully </font></center>")
                        Response.Write("<center><a href=""../LeaveAttendanceMainNew/CloseWindow.aspx""><font face=""Trebuchet MS"" size=""2"" color=""#000080"">Close Window</font></a></center>")
                        Response.End()


                        'Response.Write("Res:" & objCmd.ExecuteNonQuery())
                    End If

                ElseIf Trim(UCase(varStrAction)) = Trim(UCase("Update")) Then
                    If System.IO.File.Exists(varFileUploadPath & varStrUserID & System.IO.Path.GetExtension(FileUpload.FileName)) Then
                        System.IO.File.Delete(varFileUploadPath & varStrUserID & System.IO.Path.GetExtension(FileUpload.FileName))
                        Dim objCmdDel As New Data.SqlClient.SqlCommand("DELETE FROM SecureFax.dbo.tblUserCertificates WHERE UserId='" & varStrUserID & "'", objConn)
                        objCmdDel.ExecuteNonQuery()
                    End If

                    FileUpload.PostedFile.SaveAs(varFileUploadPath & varStrUserID & System.IO.Path.GetExtension(FileUpload.FileName))

                    Dim InsertCmd As Data.SqlClient.SqlCommand
                    InsertCmd = New Data.SqlClient.SqlCommand
                    InsertCmd.CommandType = Data.CommandType.Text

                    InsertCmd.CommandText = "INSERT INTO SecureFax.dbo.tblUserCertificates (UserID,UpdateDate,UpdateBy) VALUES('" & varStrUserID & "','" & Now() & "','" & Session("UserID").ToString & "')"
                    InsertCmd.Connection = objConn
                    InsertCmd.ExecuteNonQuery()

                    Response.Write("<center><font face=""Trebuchet MS"" size=""2"" color=""#000080"">Certificate updated sucessfully </font></center>")
                    Response.Write("<center><a href=""../LeaveAttendanceMainNew/CloseWindow.aspx""><font face=""Trebuchet MS"" size=""2"" color=""#000080"">Close Window</font></a></center>")
                    Response.End()

                End If
            End If
        Catch ex As Exception
            'Response.Write(ex.Message)
        Finally
            If objConn.State <> Data.ConnectionState.Closed Then
                objConn.Close()
                objConn = Nothing
            End If
        End Try
    End Sub
End Class
