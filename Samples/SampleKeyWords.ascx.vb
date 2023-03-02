
Partial Class Samples_SampleKeyWords
    Inherits System.Web.UI.UserControl
    Protected Sub btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSave.Click
        If btnSave.Text = "Edit" Then
            txtKeawords.Enabled = True
            txtSampleName.Enabled = True
            btnSave.Text = "Save"
        Else
            Dim clsSamples As New ETS.BL.Samples
            With clsSamples
                .SampleID = hdnSampleID.Value
                .KeyWords = txtKeawords.Text
                .Name = txtSampleName.Text
                If .UpdateSample() > 0 Then
                    txtKeawords.Enabled = False
                    txtSampleName.Enabled = False
                    btnSave.Text = "Edit"
                End If
            End With
            clsSamples = Nothing
        End If
    End Sub
End Class
