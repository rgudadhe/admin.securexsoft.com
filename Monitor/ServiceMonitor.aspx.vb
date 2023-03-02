Partial Class ServiceMonitor
    Inherits System.Web.UI.Page
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            lblDateTime.Text = "DateTime : " & Now & " (EST)"
        End If
    End Sub
    Protected Sub UpdateTimer_Tick(ByVal sender As Object, ByVal e As System.EventArgs) Handles UpdateTimer.Tick
        lblDateTime.Text = "DateTime : " & Now & " (EST)"
    End Sub
End Class
