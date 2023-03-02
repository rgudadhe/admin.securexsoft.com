Imports System
Imports System.Data
Namespace ets
    Partial Class Templates_TATempSearch
        Inherits BasePage
        Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
            'iResponse.Text = ""
            'lblPhyName.Text = "Template assignments for " & Request("PhyName")
            'hdnPhyID.Value = Request("PhyID")

            If Not Page.IsPostBack Then
                Dim hdnTemp As HiddenField
                hdnTemp = DirectCast(PreviousPage.FindControl("hdnPhysicianID"), HiddenField)
                If Not hdnTemp Is Nothing Then
                    'Response.Write("ID:" & hdnTemp.Value.ToString)
                    hdnPhyID.Value = hdnTemp.Value.ToString
                End If

                Dim hdnTempName As HiddenField
                hdnTempName = DirectCast(PreviousPage.FindControl("hdnPhysicianName"), HiddenField)
                Dim varstrTemp As String = String.Empty
                If Not hdnTempName Is Nothing Then
                    'Response.Write("Name:" & hdnTempName.Value.ToString)
                    Dim varTempSplit() As String
                    varTempSplit = hdnTempName.Value.ToString.Split("|")

                    varstrTemp = varTempSplit(0)
                End If

                iResponse.Text = ""
                lblPhyName.Text = "Template assignments for " & varstrTemp & "<a style=""cursor:hand ;"" onMouseover=""tip_it('ToolTip','Physician Names','" & Replace(hdnTempName.Value.ToString, "|", ",") & "'); "" onMouseout=""hideIt('ToolTip');""> more</a>"
            End If
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
            If Not String.IsNullOrEmpty(hdnPhyID.Value.ToString) Then
                Dim varStr() As String
                varStr = Split(hdnPhyID.Value.ToString, "|")

                For i As Integer = 0 To UBound(varStr)
                    'Response.Write(varStr(i).ToString & "<BR>")
                    SetTemplateForPhy(varStr(i))
                Next

                Server.Transfer("ViewTemplateAssigments.aspx", True)
            End If


            'Dim strTempIDs(rptTemp.Items.Count) As String
            'Dim i As Integer = 0
            'For Each ctl As RepeaterItem In rptTemp.Items
            '    Dim chk As CheckBox = ctl.FindControl("chkSel")
            '    If chk.Checked Then
            '        Dim hdn As HiddenField = chk.Parent.FindControl("TemplateID")
            '        If Not String.IsNullOrEmpty(hdn.Value) Then
            '            strTempIDs(i) = hdn.Value
            '            i = +1
            '        End If
            '    End If
            'Next
            'Dim clsPT As New ets.BL.PhysiciansTempaltes
            'With clsPT
            '    .PhysicianID = hdnPhyID.Value
            '    If .SetPhysiciansTemplates(strTempIDs) Then
            '        Response.Redirect("EditTemplateAssignments.aspx?PhyID=" & hdnPhyID.Value & "&iRes=" & "Templates have been assigned Successfully", True)
            '    Else
            '        Response.Redirect("EditTemplateAssignments.aspx?PhyID=" & hdnPhyID.Value & "&iRes=" & "Assigning Templates Failed", True)
            '    End If
            'End With
            'clsPT = Nothing

        End Sub
        Protected Sub SetTemplateForPhy(ByVal PhyID As String)
            Dim strTempIDs(rptTemp.Items.Count) As String
            Dim varTempStr As String = String.Empty
            Dim i As Integer = 0
            For Each ctl As RepeaterItem In rptTemp.Items
                Dim chk As CheckBox = ctl.FindControl("chkSel")
                If chk.Checked Then
                    Dim hdn As HiddenField = chk.Parent.FindControl("TemplateID")
                    If Not hdn Is Nothing Then
                        If Not String.IsNullOrEmpty(hdn.Value) Then
                            If String.IsNullOrEmpty(varTempStr) Then
                                varTempStr = Trim(hdn.Value.ToString)
                            Else
                                varTempStr = Trim(varTempStr) & "|" & Trim(hdn.Value.ToString)
                            End If
                            'strTempIDs(i) = hdn.Value
                            'i = +1
                        End If
                    End If
                End If
            Next
            Dim clsPT As New ets.BL.PhysiciansTempaltes
            With clsPT
                .PhysicianID = PhyID.ToString

                If .btn_Assign_Click_From_Multiple(varTempStr) Then
                    'Response.Redirect("EditTemplateAssignments.aspx?PhyID=" & hdnPhyID.Value & "&iRes=" & "Templates have been assigned Successfully", True)
                Else
                    'Response.Redirect("EditTemplateAssignments.aspx?PhyID=" & hdnPhyID.Value & "&iRes=" & "Assigning Templates Failed", True)
                End If
            End With
            clsPT = Nothing
        End Sub
    End Class
End Namespace