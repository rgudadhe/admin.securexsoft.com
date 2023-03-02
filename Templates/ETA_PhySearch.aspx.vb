Imports System
Imports System.Data
Namespace ets
    Partial Class Templates_TA_PhySearch
        Inherits BasePage
        Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
            Literal1.Text = ""
        End Sub
        Protected Sub btnSearchPhy_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSearchPhy.Click
            If Not rptBindPhy() Then
                Literal1.Text = "Error Occured"
            End If
        End Sub
        Private Function rptBindPhy() As Boolean
            Dim clsPhy As New ets.BL.Physicians
            Dim DSPhy As DataSet = clsPhy.getPhywithActDetails(Session("ContractorID"), Session("WorkgroupID"), "", txtPin.Text, txtPhyF.Text, txtPhyL.Text)
            clsPhy = Nothing
            rptPhy.DataSource = DSPhy
            rptPhy.DataBind()
            DSPhy.Dispose()
            rptBindPhy = True
        End Function

        Protected Sub btnEdit_Click(ByVal sender As Object, ByVal e As System.EventArgs)
            Dim PhyID As String
            Dim PhyName As String
            Dim btn As Button
            btn = CType(sender, Button)
            Dim hdn As HiddenField = btn.Parent.FindControl("PhyID")
            PhyID = hdn.Value.ToString
            Dim lbl As Label = btn.Parent.FindControl("txtName")
            PhyName = lbl.Text
            If PhyID <> "" Then
                Response.Redirect("EditTemplateAssignments.aspx?PhyID=" & PhyID & "&PhyName=" & PhyName)
            End If
        End Sub


    End Class
End Namespace