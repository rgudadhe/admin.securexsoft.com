Imports System.Data

Partial Class Department_Default
    Inherits BasePage
    Protected Sub Button1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button1.Click
        Dim DSUCate As New DataSet
        Dim clsUCate As New ETS.BL.UserCategories
        With clsUCate
            .ContractorID = Session("contractorID").ToString
            .Name = TxtName.Text
            DSUCate = .getCategoryList()
            If DSUCate.Tables(0).Rows.Count > 0 Then
                MsgDisp.Text = "Record already exists."
                TxtName.Focus()
            Else
                .Description = TxtDesc.Text
                .Prefix = TxtPrefix.Text
                If .InsertCategory = 1 Then
                    MsgDisp.Text = "Record has beed added successfully."
                Else
                    MsgDisp.Text = "Failed adding record."
                End If
            End If
            DSUCate.Dispose()
        End With
        clsUCate = Nothing
    End Sub

End Class
