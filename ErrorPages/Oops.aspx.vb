
Partial Class ErrorPages_Default
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Session.Abandon()
        FormsAuthentication.SignOut()
    End Sub
End Class
