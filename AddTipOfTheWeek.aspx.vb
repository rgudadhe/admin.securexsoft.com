Partial Class TipOfTheWeek
    Inherits BasePage
    Protected Sub Button1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button1.Click
        Dim RowsAfected As Integer
        If TxtTip.Text = "" Then
            iresponse.Text = "Tip Of The Week Field is required. "
            TxtDesc.Focus()
            Exit Sub
        End If

        Try
            Dim clsSystemTipOfTheWeeks As New ETS.BL.TipOfTheWeek
            With clsSystemTipOfTheWeeks
                .Tip = TxtTip.Text.ToString.Replace("'", "''")
                .Description = TxtDesc.Text.ToString.Replace("'", "''")
                .Dateupdated = Now()
                .PostedBy = Session("Userid").ToString
                .contractorid = Session("contractorid").ToString
            End With
            RowsAfected = clsSystemTipOfTheWeeks.InsertTipOfTheWeekDetails
            If RowsAfected = 1 Then
                iresponse.Text = "Tip Of The Week has been added successfully."
            Else
                iresponse.Text = "Failed adding details"
            End If
        Catch ex As Exception
            'Response.Write(ex.Message)
        End Try
    End Sub
End Class
