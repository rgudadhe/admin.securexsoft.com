Imports System.Data
Partial Class Department_Default
    Inherits BasePage


    Protected Sub Button1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button1.Click
        Dim DSDesi As New DataSet
        Dim clsDesi As New ETS.BL.Designations
        With clsDesi
            .DepartmentID = DeptName.SelectedItem.Value
            DSDEsi = .getDesignationList()
        End With

        Dim DR() As DataRow = DSDesi.Tables(0).Select("Name='" & TxtDeptName.Text & "'")
        If UBound(DR) >= 0 Then
            TxtDeptName.Focus()
            MsgDisp.Text = "Record already exists."
        Else
            With clsDesi
                .Name = TxtDeptName.Text
                .Description = TxtDeptDesc.Text
                If .InsertDesignation() = 1 Then
                    TxtDeptName.Text = ""
                    TxtDeptDesc.Text = ""
                    MsgDisp.Text = "Record has beed added successfully."
                Else
                    MsgDisp.Text = "Failed adding record."
                End If
            End With
        End If
        DSDesi.Dispose()
        clsDesi = Nothing

    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack() Then
            Dim DSDep As New DataSet
            Dim clsDep As New ETS.BL.Department
            With clsDep
                '.ContractorID = Session("ContractorID").ToString
                DSDep = .GetDepartmentLstByWrkGroupID(Session("ContractorID").ToString, Session("WorkGroupID"), String.Empty)
            End With
            clsDep = Nothing
            If DSDep.Tables.Count > 0 Then
                DeptName.DataSource = DSDep
                DeptName.DataTextField = "Name"
                DeptName.DataValueField = "DepartmentID"
                DeptName.DataBind()
                DSDep.Dispose()
            Else
                Response.Write("No department found. First create department and then add designation to it.")

                Response.End()
            End If


        End If
    End Sub

    Protected Sub DeptName_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles DeptName.SelectedIndexChanged

    End Sub
End Class
