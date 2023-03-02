Imports System.Data.SqlClient
Partial Class Department_Default
    Inherits BasePage

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            Dim clsDep As New ETS.BL.Department
            With clsDep
                .DepartmentID = Request.QueryString("DeptID")
                .ContractorID = Session("ContractorID").ToString
                .getDepartmentDetails()
                TxtDeptName.Text = .Name
                TxtDeptDesc.Text = .Description
                DeptID.Value = .DepartmentID
            End With
            clsDep = Nothing
            Button3.Visible = False
        End If
    End Sub

    Protected Sub Button1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button1.Click
        Dim clsDep As New ETS.BL.Department
        With clsDep
            .DepartmentID = DeptID.Value
            .Name = TxtDeptName.Text
            .Description = TxtDeptDesc.Text
            If .UpdateDepartmentDetails > 0 Then
                MsgDisp.Text = "Record has been udpated successfully."
            Else
                MsgDisp.Text = "Updating department details failed."
            End If
        End With
        clsDep = Nothing
    End Sub

    Protected Sub Button2_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button2.Click
        Dim clsDep As New ETS.BL.Department
        With clsDep
            .DepartmentID = DeptID.Value
            .Deleted = True
            If .UpdateDepartmentDetails > 0 Then
                MsgDisp.Text = "Record has been deleted successfully."
            Else
                MsgDisp.Text = "Deleting department failed."
            End If
        End With
        clsDep = Nothing
    End Sub
End Class
