
Partial Class emaiValidation
    Inherits System.Web.UI.Page

  
    Protected Sub login_Click(sender As Object, e As System.EventArgs) Handles login.Click
        Response.Redirect("https://admin.securexsoft.com/login.aspx")
    End Sub
End Class
