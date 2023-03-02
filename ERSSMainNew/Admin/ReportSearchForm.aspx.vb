
Partial Class ReportSearchForm
    Inherits BasePage
    Protected Sub btnSubmit_ServerClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSubmit.ServerClick
        Server.Transfer("SummaryResult.aspx", True)
    End Sub
End Class
