
Partial Class ViewUpd
    Inherits BasePage
    Protected Sub Button2_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button2.Click
        GetData()
    End Sub
    Protected Sub MyDataGrid_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles MyDataGrid.RowCommand
        If Trim(UCase(e.CommandName)) = Trim(UCase("Delete")) Then
            Dim varTrackID As String = String.Empty
            varTrackID = e.CommandArgument.ToString

            Dim clsUp As New ETS.BL.Updates
            clsUp.trackID = varTrackID
            Dim RetVal As Integer = 0
            RetVal = clsUp.DeleteUpdateDetails()
            clsUp = Nothing
            If RetVal = 1 Then
                GetData()
            End If
        End If
    End Sub
    Protected Sub MyDataGrid_RowDeleting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewDeleteEventArgs) Handles MyDataGrid.RowDeleting

    End Sub
    Protected Sub GetData()
        Try
            Dim SubDate1 As String
            Dim SubDate2 As String
            If TxtDate1.Text <> "" And IsDate(TxtDate1.Text) Then
                SubDate1 = TxtDate1.Text
            Else
                SubDate1 = "1/1/2006"
            End If

            If TxtDate2.Text <> "" And IsDate(TxtDate2.Text) Then
                SubDate2 = TxtDate2.Text
            Else
                SubDate2 = Now()
            End If

            Dim clsUp As New ETS.BL.Updates
            Dim DS As New Data.DataSet
            DS = clsUp.getUpdatesByDate(SubDate1, SubDate2)

            MyDataGrid.DataSource = DS
            MyDataGrid.DataBind()
            clsUp = Nothing
            If MyDataGrid.Rows.Count > 0 Then
                MyDataGrid.ShowFooter = True
                MyDataGrid.UseAccessibleHeader = True
                MyDataGrid.HeaderRow.TableSection = TableRowSection.TableHeader
                MyDataGrid.FooterRow.TableSection = TableRowSection.TableFooter
            End If
        Catch ex As Exception
        End Try
    End Sub

    Protected Sub MyDataGrid_Sorting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewSortEventArgs) Handles MyDataGrid.Sorting

    End Sub
End Class
