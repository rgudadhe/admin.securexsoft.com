
Partial Class MenuPage2
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'If IsPostBack Then
        '    Dim i As Integer
        '    LblAL.Text = i + 1
        '    Response.Write(i)
        'End If

        LblDate.Text = Now
        LblAL.Text = "Logged in as " & Session("AccessLevel")

     
        'If Not Session("Admin") = "True" Then
        '    AccordionPane7.Visible = False

        '    If Not Session("Access_Approve") = "True" Then
        '        AccordionPane3.Visible = False
        '    End If
        '    If Not Session("Access_Voice") = "True" Then
        '        AccordionPane4.Visible = False
        '    End If
        '    If Not Session("Access_Demo") = "True" Then
        '        AccordionPane5.Visible = False
        '    End If
        '    If Not Session("Access_Cust") = "True" Then
        '        AccordionPane8.Visible = False
        '    End If
        '    If Not Session("Access_QL") = "True" Then
        '        AccordionPane1.Visible = False
        '    End If
        'End If


    End Sub
End Class
