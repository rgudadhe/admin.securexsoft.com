Imports System.Data.SqlClient
Imports System.Data
Imports System.Data.SqlDbType
Partial Class photo
    Inherits BasePage
    Public strConn As String = System.Configuration.ConfigurationManager.AppSettings("ETSCon")
    Protected Sub Button4_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button4.Click
        Dim RecFound As String
        Dim stream As IO.Stream
        Dim strQuery As String
        stream = FileUpload1.PostedFile.InputStream
        Dim uploadedFile(stream.Length) As Byte
        If FileUpload1.HasFile Then
            If UCase(Right(FileUpload1.FileName, 3)) = "JPG" Or UCase(Right(FileUpload1.FileName, 3)) = "GIF" Then
                RecFound = "Yes"
                stream.Read(uploadedFile, 0, stream.Length)
                strQuery = "update TblUsers set Photo= @DocumentFile where userid = '" & HUserID.Value & "'"
                Dim cmdUp As New SqlCommand(strQuery, New SqlConnection(strConn))
                Try
                    cmdUp.Connection.Open()
                    If RecFound = "Yes" Then
                        cmdUp.Parameters.Add("@DocumentFile", System.Data.SqlDbType.Image, uploadedFile.Length).Value = uploadedFile
                    End If
                    cmdUp.ExecuteNonQuery()
                Finally
                    If cmdUp.Connection.State = ConnectionState.Open Then
                        cmdUp.Connection.Close()
                    End If
                End Try
                lblMsg.Text = "File is uploaded successfully. "
                Response.Write("<script language='javascript'>window.opener.location.reload();</script>")
                'Response.End()
                Response.Write("<script>window.close();</script>")
                Exit Sub
            Else
                lblMsg.Text = "Uploaded file is not in correct format. "
                Exit Sub
            End If
        Else
            lblMsg.Text = "Photo is not selected."
            Exit Sub
        End If


    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Request("userid") <> "" Then
            HUserID.Value = Request("userid")
            Dim SQLCmdUsr As New SqlCommand("Select * from tblUsers where Photo is not NULL and userid = '" & HUserID.Value & "' ", New SqlConnection(strConn))
            'Response.Write("Select * from tblUsers where UserID='" & UserId & "'")
            'Response.End()
            Try
                SQLCmdUsr.Connection.Open()
                Dim DRReUsr As SqlDataReader = SQLCmdUsr.ExecuteReader()
                If DRReUsr.HasRows = True Then
                    btnRemove.Visible = True
                Else
                    btnRemove.Visible = False
                End If
                DRReUsr.Close()
            Finally
                If SQLCmdUsr.Connection.State = ConnectionState.Open Then
                    SQLCmdUsr.Connection.Close()
                End If
            End Try


        Else
            lblMsg.Text = "Error in checking user details. Please contact system administrator for more details."
            Response.End()
        End If
    End Sub

    Protected Sub btnRemove_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnRemove.Click
        Dim strQuery
        strQuery = "update TblUsers set Photo= NULL where userid = '" & HUserID.Value & "'"
        Dim cmdUp As New SqlCommand(strQuery, New SqlConnection(strConn))
        Try
            cmdUp.Connection.Open()
            cmdUp.ExecuteNonQuery()
        Finally
            If cmdUp.Connection.State = ConnectionState.Open Then
                cmdUp.Connection.Close()
            End If
        End Try
        lblMsg.Text = "Photo has been removed successfully. "
        Response.Write("<script language='javascript'>window.opener.location.reload();</script>")
        'Response.End()
        Response.Write("<script>window.close();</script>")
    End Sub
End Class
