Partial Class Enhancements
    Inherits BasePage
    Protected Sub Button1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button1.Click
        Dim RowsAfected As Integer
        If TxtEnhancement.Text = "" Then
            iresponse.Text = "Enhancement Field is required. "
            TxtDesc.Focus()
            Exit Sub
        End If

        Try
            Dim clsSystemEnhancements As New ETS.BL.SWEnhancement
            With clsSystemEnhancements
                .Enhancement = TxtEnhancement.Text.ToString.Replace("'", "''")
                .Description = TxtDesc.Text.ToString.Replace("'", "''")
                .Dateupdated = Now()
                .ReleaseDate = TxtDate.Text
                .PostedBy = Session("Userid").ToString
                .contractorid = Session("contractorid").ToString
            End With

            RowsAfected = clsSystemEnhancements.InsertEnhancementsDetails
            If RowsAfected = 1 Then
                iresponse.Text = "System Enhancements has been added successfully."
            Else
                iresponse.Text = "Failed adding details"
            End If
        Catch ex As Exception
            'Response.Write(ex.Message)
        End Try
    End Sub
End Class
