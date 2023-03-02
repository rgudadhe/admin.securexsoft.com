
Partial Class Billing_Reports_upInvDetails
    Inherits BasePage

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Response.Write(Request("chBillAccID"))

    End Sub
End Class
