Imports System
Imports System.Data
Namespace ets
    Partial Class Templates_EditTemplateAssignments
        Inherits BasePage

        Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
            If Not IsPostBack Then
                hdnPhyID.Value = Request("PhyID")
                If Request("iRes") <> "" Then
                    iResponse.Text = Request("iRes")
                End If
                Dim clsPhy As New ets.BL.Physicians
                Dim DSPhyFlderWT As dataset = clsPhy.getPhysiciansFldrWT(Request("PhyID"))
                clsPhy = Nothing
                rptPhyTemp.DataSource = DSPhyFlderWT
                rptPhyTemp.DataBind()
                DSPhyFlderWT.Dispose()
            End If
        End Sub

        Protected Sub btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs)
            Dim DT As New DataTable
            DT.Columns.Add("TemplateID", GetType(System.String))
            DT.Columns.Add("FolderName", GetType(System.String))

            For Each ctl As RepeaterItem In rptPhyTemp.Items
                Dim hdn As HiddenField = ctl.FindControl("TemplateID")
                Dim txt As TextBox = hdn.Parent.FindControl("txtFolder")
                If Not String.IsNullOrEmpty(txt.Text) Then
                    Dim DR As DataRow = DT.NewRow
                    DR("TemplateID") = hdn.Value
                    DR("FolderName") = txt.Text
                    DT.Rows.Add(DR)
                End If
            Next
            Dim clsPhy As New ets.BL.Physicians
            clsPhy.PhysicianID = Request("PhyID")
            Dim Res As String
            If clsPhy.setPhysiciansFldrWT(DT) Then
                Res = "Changes have been saved Successfully"
            Else
                Res = "Failed saving changes"
            End If

            Response.Redirect("FldWTAssignments.aspx?PhyID=" & hdnPhyID.Value & "&iRes=" & Res, True)
            
        End Sub
    End Class
End Namespace