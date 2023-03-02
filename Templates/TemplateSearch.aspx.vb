Imports System
Imports System.Data
Namespace ets
    Partial Class Templates_TemplateSearh
        Inherits BasePage
        Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
            iResponse.Text = ""
        End Sub
        Protected Sub btnSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSearch.Click
            If Not rptBind() Then
                iResponse.Text = "Error Occured"
            End If
        End Sub

        Protected Sub Button1_Click(ByVal sender As Object, ByVal e As System.EventArgs)
            Dim TempName As String
            Dim TempType As String
            Dim TypeDesc As String
            Dim TempID As String
            Dim Exbtn As msWebControlsLibrary.ExImageButton
            Dim txt As TextBox
            Exbtn = CType(sender, msWebControlsLibrary.ExImageButton)
            Dim btn1 As Button = Exbtn.Parent.FindControl("iPopUp")
            btn1.Enabled = True
            btn1.Visible = True
            btn1 = Exbtn.Parent.FindControl("iPopUp1")
            btn1.Enabled = True
            txt = Exbtn.Parent.FindControl("txtName")
            TempName = txt.Text
            txt.Enabled = False

            txt = Exbtn.Parent.FindControl("txtType")
            txt.Visible = False
            TempType = txt.Text
            txt = Exbtn.Parent.FindControl("txttypeDesc")
            txt.Visible = False
            TypeDesc = txt.Text

            Dim lbl As Label = Exbtn.Parent.FindControl("lblType")
            lbl.Visible = True

            Dim hdn As HiddenField = Exbtn.Parent.FindControl("TemplateID")
            TempID = hdn.Value.ToString
            Dim clsTemplate As New ets.BL.Templates
            With clsTemplate
                .TemplateID = TempID
                .TemplateName = TempName
                .TemplateType = TempType
                .TypeDesc = TypeDesc
                If .UpdateTemplateDetails > 0 Then
                    iResponse.Text = "Record updated successfully"
                End If
            End With
            clsTemplate = Nothing
            rptBind()
            Exbtn.Enabled = False
        End Sub

        Protected Sub iPopUp_Click(ByVal sender As Object, ByVal e As System.EventArgs)
            Dim btn As Button = CType(sender, Button)
            Dim txt As TextBox = btn.Parent.FindControl("txtType")
            txt.Visible = True
            txt = btn.Parent.FindControl("txttypeDesc")
            txt.Visible = True
            Dim lbl As Label = btn.Parent.FindControl("lblType")
            lbl.Visible = False
            Dim btn1 As msWebControlsLibrary.ExImageButton = btn.Parent.FindControl("Button1")
            btn1.Enabled = True
            btn.Visible = False
        End Sub
        Protected Sub iPopUp1_Click(ByVal sender As Object, ByVal e As System.EventArgs)
            Dim btn As Button = CType(sender, Button)
            Dim txt As TextBox = btn.Parent.FindControl("txtName")
            txt.Enabled = True
            Dim btn1 As msWebControlsLibrary.ExImageButton = btn.Parent.FindControl("Button1")
            btn1.Enabled = True
            btn.Enabled = False
        End Sub

        Protected Sub DDType_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs)
            Dim ddList As DropDownList = CType(sender, DropDownList)
            If ddList.SelectedValue <> "" Then
                Dim lbl As Label = ddList.Parent.FindControl("lblType")
                lbl.Text = ddList.SelectedValue
                lbl.Visible = True
                Dim btn As Button = ddList.FindControl("iPopUp")
                btn.Visible = True
                Dim btn1 As msWebControlsLibrary.ExImageButton
                btn1 = ddList.FindControl("Button1")
                btn1.Enabled = True
                ddList.SelectedIndex = 0
                ddList.Visible = False
            End If
        End Sub

        Protected Sub btnDelete_Click(ByVal sender As Object, ByVal e As System.EventArgs)
            Dim TempID As String
            Dim btn As msWebControlsLibrary.ExImageButton
            btn = CType(sender, msWebControlsLibrary.ExImageButton)
            Dim hdn As HiddenField = btn.Parent.FindControl("TemplateID")
            TempID = hdn.Value.ToString
            Dim clsTemplate As New ets.BL.Templates
            With clsTemplate
                .TemplateID = TempID
                If .DeleteTemplateDetails > 0 Then
                    iResponse.Text = "Record deleted successfully"
                End If
            End With
            clsTemplate = Nothing
            rptBind()
        End Sub
        Private Function rptBind() As Boolean
            Try

                Dim DSTemplates As New DataSet
                Dim clsTemplates As New ets.BL.Templates
                With clsTemplates
                    .ContractorID = Session("ContractorID")
                    ._WhereString.Append(" and  TemplateName like '" & txtTemplateName.Text & "' and (IsVRS is Null OR IsVRS = 0) ")
                    DSTemplates = .getTemplateList
                End With
                clsTemplates = Nothing
                rptTemp.DataSource = DSTemplates
                rptTemp.DataBind()
                DSTemplates.Dispose()
                Return True
            Catch ex As Exception
                Return False
            End Try
        End Function
    End Class
End Namespace