
Partial Class ForceRouting4TempResult
    Inherits BasePage
    Protected Sub Page_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Init
        If Not String.IsNullOrEmpty(Request.Form("SEARCH")) Then
            Server.Transfer("ForceRouting4TempResult.aspx")
        End If
    End Sub
End Class
