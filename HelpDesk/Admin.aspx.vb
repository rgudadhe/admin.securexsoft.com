
Partial Class HelpDesk_Admin
    Inherits System.Web.UI.Page
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        hdnUserId.Value = Session("UserID").ToString
        hdnFrom.Value = "Admin"
    End Sub
End Class
