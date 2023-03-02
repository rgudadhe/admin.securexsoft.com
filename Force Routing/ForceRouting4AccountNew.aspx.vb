
Partial Class ForceRouting4AccResult
    Inherits BasePage
    Protected Sub Page_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Init
        If Not String.IsNullOrEmpty(Request.Form("SEARCH")) Then
            Server.Transfer("ForceRouting4AccResultNew.aspx")
        End If
    End Sub
End Class
