Imports System.Data.SqlClient
Imports System.Data
Imports System.IO
Imports System.Drawing.Imaging

Partial Class CreateUser_UserPhoto
    Inherits BasePage

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim strConn As String
        strConn = System.Configuration.ConfigurationManager.AppSettings("ETSCon")
        Dim SQLCmd As New SqlCommand("Select * from tblUsers where UserID='" & Request("UserId") & "'", New SqlConnection(strConn))
        Try
            SQLCmd.Connection.Open()
            Dim dr As SqlDataReader = SQLCmd.ExecuteReader()
            If dr.Read = True Then
                If Not IsDBNull(dr("photo")) Then
                    Response.BinaryWrite(dr("photo"))
                    Response.ContentType = "Image/Jpeg"
                Else
                    Response.Redirect("~/Images/images.jpg")

                End If
            End If
            dr.Close()
        Finally
            If SQLCmd.Connection.State = ConnectionState.Open Then
                SQLCmd.Connection.Close()
            End If
        End Try

    End Sub
End Class
