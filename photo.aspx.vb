Imports System.Data.SqlClient
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
                cmdUp.Connection.Open()
                If RecFound = "Yes" Then
                    cmdUp.Parameters.Add("@DocumentFile", System.Data.SqlDbType.Image, uploadedFile.Length).Value = uploadedFile
                End If
                cmdUp.ExecuteNonQuery()
                cmdUp.Connection.Close()
                lblMsg.Text = "File is uploaded successfully. "
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
        Else
            lblMsg.Text = "Error in checking user details. Please contact system administrator for more details."
            Response.End()
        End If
    End Sub
End Class
