
Partial Class ForceRouting4PhyResult
    Inherits BasePage
    Protected Sub Page_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Init
        If Not String.IsNullOrEmpty(Request.Form("SEARCH")) Then
            Server.Transfer("ForceRouting4PhyResult.aspx")
        End If
    End Sub
End Class
