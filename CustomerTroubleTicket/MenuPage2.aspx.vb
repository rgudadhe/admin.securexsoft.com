
Partial Class MenuPage2
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'If IsPostBack Then
        '    Dim i As Integer
        '    LblAL.Text = i + 1
        '    Response.Write(i)
        'End If

        LblDate.Text = Now & " EST"
        LblAL.Text = "Logged in as " & Session("AccessLevel")

     
        If Not Session("Admin") = "True" Then
            'AccordionPane5.Visible = False
            'Dim APane As AjaxControlToolkit.AccordionPane = AccordionPane5.FindControl("AccordionPane7")
            'APane.Visible = False
            'If Not Session("Access_Approve") = "True" Then
            '    '   AccordionPane3.Visible = False
            'End If
            'If Not Session("Access_Voice") = "True" Then
            '    AccordionPane4.Visible = False
            'End If
            'If Not Session("Access_Demo") = "True" Then
            '    '  AccordionPane5.Visible = False
            'End If
            'If Not Session("Access_Cust") = "True" Then
            'Dim APane1 As AjaxControlToolkit.AccordionPane = AccordionPane5.FindControl("AccordionPane8")
            'APane1.Visible = False
            'End If
            'If Not Session("Access_QL") = "True" Then
            '    AccordionPane1.Visible = False
            'End If
        End If


    End Sub
End Class
