Imports System.Data.SqlClient
Partial Class Billing_LCMethods_AddNewVAS
    Inherits BasePage

    Protected Sub Button2_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button2.Click
        Try
            Dim obj As New ETS.BL.ItemDetails
            With obj
                .contractorid = Session("contractorid").ToString
                .Item = txtItem.Text
                .Description = txtDesc.Text
                .Rate = txtRate.Text
                .Mode = "VAS"
                If .InsertItemDetails = 1 Then
                    Response.Write("updated")
                Else
                    Response.Write("Not updated")
                End If
            End With
            obj = Nothing

            txtItem.Text = String.Empty
            txtRate.Text = String.Empty
            txtDesc.Text = String.Empty

            Response.Write("<script language='javascript'>alert('Details have been updated successfully.');</script>")
        Catch ex As Exception
            Response.Write("Error : " & ex.Message & " " & "Please contact Customer Support for more details.")
        End Try
    End Sub
End Class
