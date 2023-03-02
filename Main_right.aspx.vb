
Imports System
Namespace ets
    Public Class main_right
        Inherits PageBase
        Protected WithEvents iHead As System.Web.UI.HtmlControls.HtmlGenericControl
        Protected WithEvents Body As System.Web.UI.HtmlControls.HtmlGenericControl
        Dim DisplayUrl


        Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs)
            'splDV.CookieDays = 0
            'splDV.StyleFolder = "styles/default"

            DisplayUrl = Split(Request.QueryString("cId").ToString(), "|")
            'lHeader.Text = DisplayUrl(0).ToString
            'lContent.Text = DisplayUrl(0).ToString
            'splDV.RightPanel.Content.Url = DisplayUrl(1).ToString
            'Response.Redirect(DisplayUrl(1).ToString)
            Dim iHead As HtmlControl = CType(Me.FindControl("iHead"), HtmlControl)
            Dim Body As HtmlControl = CType(Me.FindControl("Body"), HtmlControl)
            iHead.Attributes("src") = "Title.aspx?lHeader=" & DisplayUrl(0).ToString
            Body.Attributes("src") = DisplayUrl(1).ToString

        End Sub
    End Class
End Namespace
