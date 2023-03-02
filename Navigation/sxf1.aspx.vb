
Partial Class Navigation_sxf1
    Inherits System.Web.UI.Page
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        hdnUserID.Value = Session("UserID").ToString
        Page.ClientScript.RegisterStartupScript(Me.GetType, "MyFunction", "GetData();", True)
    End Sub
End Class
