
Partial Class WorkGroup_DepartmentAssignments
    Inherits BasePage
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            BindWrkGrpData()
        End If
    End Sub
    Protected Sub BindWrkGrpData()
        Dim DS As New Data.DataSet

        Dim clsWrkGrp As ETS.BL.WorkGroup
        Try
            clsWrkGrp = New ETS.BL.WorkGroup
            clsWrkGrp.ContractorID = Session("ContractorID").ToString
            DS = clsWrkGrp.getWrkGrpList

            If DS.Tables.Count > 0 Then
                If DS.Tables(0).Rows.Count > 0 Then
                    ddlWorkGrp.DataTextField = "Description"
                    ddlWorkGrp.DataValueField = "WorkGroupID"
                    ddlWorkGrp.DataSource = DS
                    ddlWorkGrp.DataBind()
                End If
            End If
            ddlWorkGrp.Items.Insert(0, New ListItem("Please Select", String.Empty))
        Catch ex As Exception
            lblMsg.Text = String.Empty
            lblMsg.Text = "Err :" & ex.Message
        Finally
            DS.Dispose()
            clsWrkGrp = Nothing
        End Try
    End Sub
    Protected Sub AssignedDepartments()
        If Not String.IsNullOrEmpty(ddlWorkGrp.Items(ddlWorkGrp.SelectedIndex).Value.ToString) Then
            Dim DS As New Data.DataSet
            Dim clsWrkDept As ETS.BL.WrokGroupDeptAssignments
            Try
                clsWrkDept = New ETS.BL.WrokGroupDeptAssignments
                DS = clsWrkDept.GetDeptAssignedByWrkGrp(ddlWorkGrp.Items(ddlWorkGrp.SelectedIndex).Value.ToString)

                If DS.Tables.Count > 0 Then
                    If DS.Tables(0).Rows.Count > 0 Then
                        lstAssigned.DataTextField = "Name"
                        lstAssigned.DataValueField = "DepartmentID"
                        lstAssigned.DataSource = DS
                        lstAssigned.DataBind()
                    End If
                End If
            Catch ex As Exception
                lblMsg.Text = String.Empty
                lblMsg.Text = "Err :" & ex.Message
            Finally
                DS.Dispose()
                clsWrkDept = Nothing
            End Try
        End If
    End Sub
    Protected Sub AvialbleDepartments()
        If Not String.IsNullOrEmpty(ddlWorkGrp.Items(ddlWorkGrp.SelectedIndex).Value.ToString) Then
            Dim DS As New Data.DataSet
            Dim clsWrkDept As ETS.BL.WrokGroupDeptAssignments
            Try
                clsWrkDept = New ETS.BL.WrokGroupDeptAssignments
                DS = clsWrkDept.GetDeptAvailableForWrkGrp()
                If DS.Tables.Count > 0 Then
                    If DS.Tables(0).Rows.Count > 0 Then
                        lstAvailable.DataTextField = "Name"
                        lstAvailable.DataValueField = "DepartmentID"
                        lstAvailable.DataSource = DS
                        lstAvailable.DataBind()
                    End If
                End If
            Catch ex As Exception
                lblMsg.Text = String.Empty
                lblMsg.Text = "Err :" & ex.Message
            Finally
                DS.Dispose()
                clsWrkDept = Nothing
            End Try
        End If
    End Sub
    Protected Sub btnAdd_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnAdd.Click
        Try
            Dim intCt As Integer
            For intCt = lstAvailable.Items.Count - 1 To 0 Step -1 ' Looping Backwards
                If lstAvailable.Items(intCt).Selected Then
                    Dim LI As New ListItem
                    LI.Text = lstAvailable.Items(intCt).Text
                    LI.Value = lstAvailable.Items(intCt).Value
                    lstAssigned.Items.Add(LI)
                    lstAvailable.Items.Remove(lstAvailable.Items(intCt))
                End If
            Next
        Catch ex As Exception
            lblMsg.Text = String.Empty
            lblMsg.Text = "Err :" & ex.Message
        End Try
    End Sub
    Protected Sub btnRemove_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnRemove.Click
        Try
            Dim intCt As Integer
            For intCt = lstAssigned.Items.Count - 1 To 0 Step -1 ' Looping Backwards
                If lstAssigned.Items(intCt).Selected Then
                    Dim LI As New ListItem
                    LI.Text = lstAssigned.Items(intCt).Text
                    LI.Value = lstAssigned.Items(intCt).Value
                    lstAvailable.Items.Add(LI)
                    lstAssigned.Items.Remove(lstAssigned.Items(intCt))
                End If
            Next
        Catch ex As Exception
            lblMsg.Text = String.Empty
            lblMsg.Text = "Err :" & ex.Message
        End Try
    End Sub
    Protected Sub ddlWorkGrp_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlWorkGrp.SelectedIndexChanged
        lblMsg.Text = String.Empty
        lstAssigned.Items.Clear()
        lstAvailable.Items.Clear()
        AssignedDepartments()
        AvialbleDepartments()
    End Sub

    Protected Sub btmSubmit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btmSubmit.Click
        If Not String.IsNullOrEmpty(ddlWorkGrp.Items(ddlWorkGrp.SelectedIndex).Value.ToString) Then
            Dim intCt As Integer
            Dim DT As New Data.DataTable
            DT.Columns.Add(New Data.DataColumn("DeptID"))

            For intCt = lstAssigned.Items.Count - 1 To 0 Step -1 ' Looping Backwards
                Dim DR As Data.DataRow = DT.NewRow
                DR("DeptID") = lstAssigned.Items(intCt).Value.ToString
                DT.Rows.Add(DR)
            Next

            'If DT.Rows.Count > 0 Then
            Dim clsWrkGrpDept As ETS.BL.WrokGroupDeptAssignments
            Try
                clsWrkGrpDept = New ETS.BL.WrokGroupDeptAssignments
                clsWrkGrpDept.WorkGroupID = ddlWorkGrp.Items(ddlWorkGrp.SelectedIndex).Value.ToString
                If clsWrkGrpDept.btnSubmit_click(DT) = True Then
                    lblMsg.Text = String.Empty
                    lblMsg.Text = "Assignment Updated"
                    Exit Sub
                Else
                    lblMsg.Text = String.Empty
                    lblMsg.Text = "Assignment failed"
                    Exit Sub
                End If
            Catch ex As Exception
                Response.Write(ex.Message)
            Finally
                clsWrkGrpDept = Nothing
            End Try
            'End If
        Else
            lblMsg.Text = String.Empty
            lblMsg.Text = "Please select workgroup"
            Exit Sub
        End If
    End Sub
End Class
