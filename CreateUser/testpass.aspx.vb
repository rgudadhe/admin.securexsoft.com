Imports Encrypass
Partial Class CreateUser_testpass
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim EPass As New EncryPass.Encry
        Dim Newuserpass As String
        Newuserpass = EPass.encrypt("hba00616", "welcome")
        Response.Write(Newuserpass)


    End Sub
End Class
