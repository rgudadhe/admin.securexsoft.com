Imports System.Data.SqlClient
Imports System.Data

Partial Class Department_Default
    Inherits BasePage

    Protected Sub Button1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button1.Click
        Dim DSDesi As New DataSet
        Dim clsDesi As New ETS.BL.Designations
        With clsDesi
            .Name = TxtDesnName.Text
            ._WhereString.Append(" AND DepartmentID not in ('" & HDesiID.Value & "')")
            DSDesi = .getDesignationList()
        End With

        Dim DR() As DataRow = DSDesi.Tables(0).Select("Name='" & TxtDesnName.Text & "'")
        If UBound(DR) >= 0 Then
            TxtDeptname.Focus()
            MsgDisp.Text = "Record already exists."
        Else
            With clsDesi
                .DesignationID = HDesiID.Value
                .Description = TxtDesnDesc.Text
                If .UpdateDesignation() > 0 Then
                    MsgDisp.Text = "Record has beed updated successfully."
                Else
                    MsgDisp.Text = "Failed updating record."
                End If
            End With
        End If
        DSDesi.Dispose()
        clsDesi = Nothing
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack() Then
            HDesiID.Value = Request("DesignID")
            Dim DSDes As New DataSet
            Dim clsDes As New ETS.BL.Designations
            With clsDes
                DSDes = .getContractorsDesignationList(Session("ContractorID").ToString, "", HDesiID.Value)

            End With
            clsDes = Nothing
            If DSDes.Tables.Count > 0 Then
                If DSDes.Tables(0).Rows.Count > 0 Then
                    With DSDes.Tables(0).Rows(0)
                        If Not IsDBNull(.Item("Deptname")) Then
                            TxtDeptname.Text = .Item("Deptname")
                        End If
                        If Not IsDBNull(.Item("name")) Then
                            TxtDesnName.Text = .Item("name")
                        End If
                        If Not IsDBNull(.Item("Description")) Then
                            TxtDesnDesc.Text = .Item("Description")
                        End If
                    End With
                End If
            End If
            DSDes.Dispose()
        End If
    End Sub


    Protected Sub btnDelete_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnDelete.Click
        Dim clsDes As New ETS.BL.Designations
        With clsDes
            .DesignationID = HDesiID.Value
            .Deleted = True
            If .UpdateDesignation > 0 Then
                MsgDisp.Text = "Record has beed updated successfully."
            Else
                MsgDisp.Text = "Failed updating record."
            End If
        End With
        clsDes = Nothing
    End Sub
End Class
