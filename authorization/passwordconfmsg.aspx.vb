
Partial Class emaiValidation
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load


        lblemail.Text = Request("emailID")
        'FNAme.Text = Request("UFName")


    End Sub
End Class
