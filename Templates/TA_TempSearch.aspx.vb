Imports System
Imports System.Data
Namespace ets
    Partial Class Templates_TATempSearch
        Inherits BasePage
        Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
            iResponse.Text = ""
            lblPhyName.Text = "Template assignments for " & Request("PhyName")
            hdnPhyID.Value = Request("PhyID")
        End Sub



        Protected Sub btnSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSearch.Click
            If Not rptBind() Then
                iResponse.Text = "Error Occured"
            End If
        End Sub


        Private Function rptBind() As Boolean
            Dim DSTemp As New dataset
            Dim clsTemplates As New ets.BL.Templates
            With clsTemplates
                .ContractorID = Session("ContractorID").ToString
                ._WhereString.Append(" AND TemplateName like '" & txtTemplateName.Text & "'")
                DSTemp = .getTemplateList()
            End With
            clsTemplates = Nothing
            rptTemp.DataSource = DSTemp
            rptTemp.DataBind()
            rptBind = True
            DSTemp.Dispose()
        End Function


        Protected Sub btnSel_Click(ByVal sender As Object, ByVal e As System.EventArgs)
            Dim varTempStr As String = String.Empty
            Dim strTempIDs(rptTemp.Items.Count) As String
            Dim i As Integer = 0
            For Each ctl As RepeaterItem In rptTemp.Items
                Dim chk As CheckBox = ctl.FindControl("chkSel")
                If chk.Checked Then
                    Dim hdn As HiddenField = chk.Parent.FindControl("TemplateID")
                    If Not String.IsNullOrEmpty(hdn.Value) Then
                        If String.IsNullOrEmpty(varTempStr) Then
                            varTempStr = Trim(hdn.Value.ToString)
                        Else
                            varTempStr = Trim(varTempStr) & "|" & Trim(hdn.Value.ToString)
                        End If
                        'strTempIDs(i) = hdn.Value
                        'i = +1
                    End If
                    'If Not String.IsNullOrEmpty(hdn.Value) Then
                    '    strTempIDs(i) = hdn.Value
                    '    i = +1
                    'End If
                End If
            Next
            Dim clsPT As New ets.BL.PhysiciansTempaltes
            With clsPT
                .PhysicianID = hdnPhyID.Value
                'If .SetPhysiciansTemplates(strTempIDs) Then
                '    Response.Redirect("EditTemplateAssignments.aspx?PhyID=" & hdnPhyID.Value & "&iRes=" & "Templates have been assigned Successfully", True)
                'Else
                '    Response.Redirect("EditTemplateAssignments.aspx?PhyID=" & hdnPhyID.Value & "&iRes=" & "Assigning Templates Failed", True)
                'End If

                If .btn_Assign_Click_From_Multiple(varTempStr) Then
                    Response.Redirect("EditTemplateAssignments.aspx?PhyID=" & hdnPhyID.Value & "&PhyName=" & Request("PhyName") & "&iRes=" & "Templates have been assigned Successfully", True)
                Else
                    Response.Redirect("EditTemplateAssignments.aspx?PhyID=" & hdnPhyID.Value & "&PhyName=" & Request("PhyName") & "&iRes=" & "Assigning Templates Failed", True)
                End If
            End With
            clsPT = Nothing

        End Sub


    End Class
End Namespace