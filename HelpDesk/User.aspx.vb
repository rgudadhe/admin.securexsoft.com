
Partial Class HelpDesk_User
    Inherits System.Web.UI.Page
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        hdnUserId.Value = Session("UserID").ToString
        hdnFrom.Value = "User"
    End Sub
End Class
