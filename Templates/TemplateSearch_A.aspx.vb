Imports System
Imports System.Data
Namespace ets
    Partial Class Templates_TemplateSearchA
        Inherits BasePage
        Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
            iResponse.Text = ""
        End Sub
        Protected Sub btnSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSearch.Click
            rptBind()
        End Sub
        Private Function rptBind() As Boolean
            Dim DSTemplates As New DataSet
            Dim clsTemplates As New ets.BL.Templates
            With clsTemplates
                .ContractorID = Session("ContractorID")
                ._WhereString.Append(" and  TemplateName like '" & txtTemplateName.Text & "' ")
                DSTemplates = .getTemplateList
            End With
            clsTemplates = Nothing
            rptTemp.DataSource = DSTemplates
            rptTemp.DataBind()
            DSTemplates.Dispose()
        End Function
        Protected Sub btnEdit_Click(ByVal sender As Object, ByVal e As System.EventArgs)
            Dim TempID As String
            Dim TempName As String
            Dim btn As Button
            btn = CType(sender, Button)
            Dim hdn As HiddenField = btn.Parent.FindControl("TemplateID")
            TempID = hdn.Value.ToString
            Dim lbl As Label = btn.Parent.FindControl("txtName")
            TempName = lbl.Text
            If TempID <> "" Then
                Response.Redirect("TemplateAttributes.aspx?TempID=" & TempID & "&Name=" & TempName, True)
            End If
        End Sub
        Protected Sub btnCEdit_Click(ByVal sender As Object, ByVal e As System.EventArgs)
            Dim TempID As String
            Dim TempName As String
            Dim btn As Button
            btn = CType(sender, Button)
            Dim hdn As HiddenField = btn.Parent.FindControl("TemplateID")
            TempID = hdn.Value.ToString
            Dim lbl As Label = btn.Parent.FindControl("txtName")
            TempName = lbl.Text
            If TempID <> "" Then
                Response.Redirect("TemplateCustomAttributes.aspx?TempID=" & TempID & "&Name=" & TempName, True)
            End If
        End Sub
    End Class
End Namespace