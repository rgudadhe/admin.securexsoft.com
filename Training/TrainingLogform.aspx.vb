
Partial Class TrainingLogForm
    Inherits System.Web.UI.Page
    Protected Sub Page_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.PreInit
        If Not String.IsNullOrEmpty(Request.Form("SEARCH")) Then
            Server.Transfer("TrainingLogResult.aspx")
        End If
    End Sub
End Class
