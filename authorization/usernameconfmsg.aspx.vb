Imports System.Reflection

Partial Class emaiValidation
    Inherits System.Web.UI.Page

    Protected Sub Page_Init(sender As Object, e As System.EventArgs) Handles Me.Init

    End Sub

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load

        lblemail.Text = Request("emailID")
        
        Page.Response.Cache.SetCacheability(HttpCacheability.NoCache)





    End Sub

   
End Class
