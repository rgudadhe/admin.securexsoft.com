Imports Microsoft.VisualBasic
Imports System.Data.SqlClient
Imports System.Web.UI.WebControls
Imports System.Web.UI.WebControls.WebParts
Imports System.Web.UI.HtmlControls
Imports System
Imports System.Configuration
Imports System.IO
Imports System.Web
Imports System.Web.Security
Imports System.Web.UI
Public Class BasePage
    Inherits System.Web.UI.Page
    Protected Overloads Overrides Sub OnInit(ByVal e As EventArgs)
        'Session.Abandon()
        MyBase.OnInit(e)

        If Context.Session IsNot Nothing Then
            'Tested and the IsNewSession is more advanced then simply checking if 
            ' a cookie is present, it does take into account a session timeout, because 
            ' I tested a timeout and it did show as a new session 
            If Session.IsNewSession Then
                ' If it says it is a new session, but an existing cookie exists, then it must 
                ' have timed out (can't use the cookie collection because even on first 
                ' request it already contains the cookie (request and response 
                ' seem to share the collection) 
                Dim szCookieHeader As String = Request.Headers("Cookie")
                If (szCookieHeader IsNot Nothing) AndAlso (szCookieHeader.IndexOf("ASP.NET_SessionId") >= 0) Then
                    Response.Redirect("../PopupLogin.aspx")
                    'Response.Write("<script>window.open ('http://ets.edictate.com/SecureWeb/login/relogin.aspx''name','height=150,width=400, left=300, top=100');</script>")
                End If
            End If
            Response.AppendHeader("Refresh", Convert.ToString((Session.Timeout * 600)) & "; url=" & "../PopupLogin.aspx")
            'InjectSessionExpireScript()
        End If
    End Sub
End Class
