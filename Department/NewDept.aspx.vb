Imports System.Data
Partial Class Department_Default
    Inherits BasePage


    Protected Sub Button1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button1.Click
        Dim clsDep As New ETS.BL.Department
        With clsDep
            .Name = TxtDeptName.Text
            .ContractorID = Session("ContractorID").ToString
            .getDepartmentDetails()
            If Len(.DepartmentID) = 36 Then
                TxtDeptName.Focus()
                MsgDisp.Text = "Record already exists."
            Else
                .Description = TxtDeptDesc.Text
                If .InsertDepartment = 1 Then
                    TxtDeptName.Text = ""
                    TxtDeptDesc.Text = ""
                    MsgDisp.Text = "Record has beed added successfully."
                Else
                    MsgDisp.Text = "Adding department failed."
                End If
            End If
        End With
        clsDep = Nothing
    End Sub


End Class
