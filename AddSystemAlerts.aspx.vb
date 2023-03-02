Partial Class SystemAlerts
    Inherits BasePage
    Protected Sub Button1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button1.Click
        Dim RowsAfected As Integer
        If TxtAlert.Text = "" Then
            iresponse.Text = "Alert Field is required. "
            TxtAlert.Focus()
            Exit Sub
        End If
       
        Try
            Dim clsSystemAlerts As New ETS.BL.SystemAlerts
            With clsSystemAlerts
                .Alert = TxtAlert.Text.ToString.Replace("'", "''")
                .Dateupdated = TxtDate.Text
                .PostedBy = Session("Userid").ToString
                .contractorid = Session("contractorid").ToString
            End With

            RowsAfected = clssystemAlerts.InsertSystemAlertsDetails
            If RowsAfected = 1 Then
                iresponse.Text = "System Alerts has been added successfully."
            Else
                iresponse.Text = "Failed adding details"
            End If
        Catch ex As Exception
            'Response.Write(ex.Message)
        End Try
    End Sub
End Class
