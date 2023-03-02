Namespace ets
    Partial Class FileImportLog
        Inherits BasePage
        Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
            If Not String.IsNullOrEmpty(Request.Form("btnSearch")) Then
                Server.Transfer("FileImportLogResult.aspx")
            End If
        End Sub


    End Class
End Namespace