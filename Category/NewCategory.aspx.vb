Imports System
Imports System.Data
Partial Class Department_Default
    Inherits BasePage
    Protected Sub Button1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button1.Click
        Dim clsActCate As New ETS.BL.ActCategories
        With clsActCate
            .ContractorID = Session("ContractorID").ToString
            Dim DSCate As DataSet = .getActCateList()
            If DSCate.Tables(0).Select("Description='" & TxtCategory.Text & "'").Length > 0 Then
                Response.Write("This Category already exists.")
                TxtCategory.Focus()
            ElseIf DSCate.Tables(0).Select("Priority='" & TxtPriority.Text & "'").Length > 0 Then
                Response.Write("This Priority already exists.")
                TxtPriority.Focus()
            Else
                .Description = TxtCategory.Text
                .Priority = TxtPriority.Text
                If .InsertActCate() = 1 Then
                    TxtCategory.Text = ""
                    TxtPriority.Text = ""
                    Response.Write("Record has been inserted successfully")
                Else
                    Response.Write("Failed inserting record")
                End If
            End If
            DSCate.Dispose()
        End With
        clsActCate = Nothing
    End Sub
End Class
