Partial Class Department_Default
    Inherits BasePage
    Protected Sub Button1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button1.Click
        If Not TxtAccountName.Text = "" Then
            Dim clsGrpAct As New ETS.BL.GrpAccounts
            With clsGrpAct
                .GrpActName = TxtAccountName.Text
                .Description = TxtDescription.Text
                .CreateDate = Now().ToString
                .ContractorID = Session("ContractorID").ToString
                If .InsertGrpAct() = 1 Then
                    TxtAccountName.Text = ""
                    TxtDescription.Text = ""
                    MsgDisp.Text = "Record has been inserted into database successfully"
                Else
                    MsgDisp.Text = "Failed inserting record"
                End If
            End With
            clsGrpAct = Nothing
        End If
    End Sub
End Class
