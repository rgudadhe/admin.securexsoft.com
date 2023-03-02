Public Class _Default
    Inherits System.Web.UI.Page
    Public Bit As String
    Protected Sub form1_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles form1.Load
        Bit = request("Bit")
    End Sub
End Class

