
Partial Class RoutingTool_JobsRouting
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim LI As New ListItem
        LI.Text = "Select Option"
        LI.Value = ""
        'LI.Selected = True
        Dim LI1 As New ListItem
        LI1.Text = "Change Status"
        LI1.Value = "Status"
        Dim LI2 As New ListItem
        LI2.Text = "Change TAT"
        LI2.Value = "TAT"
        DLStatus.Items.Add(LI)
        DLStatus.Items.Add(LI1)
        DLStatus.Items.Add(LI2)
    End Sub
End Class
