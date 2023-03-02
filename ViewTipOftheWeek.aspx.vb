
Partial Class ViewUpd
    Inherits BasePage
    Protected Sub Button2_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button2.Click
        GetData()
    End Sub
    Protected Sub MyDataGrid_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles MyDataGrid.RowCommand
        'If Trim(UCase(e.CommandName)) = Trim(UCase("Delete")) Then
        '    Dim varTrackID As String = String.Empty
        '    varTrackID = e.CommandArgument.ToString

        '    Dim clsUp As New ETS.BL.SystemAlerts
        '    clsUp.trackID = varTrackID
        '    Dim RetVal As Integer = 0
        '    RetVal = clsUp.DeleteSystemAlertsDetails()
        '    clsUp = Nothing
        '    If RetVal = 1 Then
        '        GetData()
        '    End If
        'End If
    End Sub
    Protected Sub MyDataGrid_RowDeleting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewDeleteEventArgs) Handles MyDataGrid.RowDeleting
        Dim row = MyDataGrid.Rows(e.RowIndex)
        Dim obj As New ETS.BL.TipOfTheWeek
        With obj
            obj.trackID = MyDataGrid.DataKeys(e.RowIndex).Value.ToString()
            obj.DeleteTipOfTheWeekDetails()
        End With
        obj = Nothing
        GetData()
    End Sub
    Protected Sub MyDataGrid_RowCancelingEdit(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCancelEditEventArgs) Handles MyDataGrid.RowCancelingEdit
        MyDataGrid.EditIndex = -1
        GetData()
    End Sub

    Protected Sub MyDataGrid_RowEditing(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewEditEventArgs) Handles MyDataGrid.RowEditing
        'Set the edit index.
        MyDataGrid.EditIndex = e.NewEditIndex
        'Bind data to the GridView control.
        'MyDataGrid.BindData()
        GetData()

    End Sub

    Protected Sub MyDataGrid_RowUpdated(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewUpdatedEventArgs) Handles MyDataGrid.RowUpdated
        If e.AffectedRows > 0 Then
            lblDisp.Text = "Record has been updated successfully"
        Else
            lblDisp.Text = "Error in updating record. Please check with System Administrator for more details."
        End If
    End Sub
    Protected Sub MyDataGrid_RowUpdating(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewUpdateEventArgs) Handles MyDataGrid.RowUpdating

        Dim row = MyDataGrid.Rows(e.RowIndex)
        Dim obj As New ETS.BL.TipOfTheWeek
        With obj
            obj.trackID = MyDataGrid.DataKeys(e.RowIndex).Value.ToString()
            obj.Tip = (CType((row.Cells(1).Controls(0)), TextBox)).Text
            obj.Description = (CType((row.Cells(2).Controls(0)), TextBox)).Text.Replace(vbCrLf, "<br />")
            obj.PostedBy = Session("userid").ToString
            obj.Dateupdated = Now()
            obj.UpdateTipOfTheWeekDetails()
        End With
        obj = Nothing
        MyDataGrid.EditIndex = -1
        GetData()
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

            Dim clsUp As New ETS.BL.HomePage
            Dim DS As New Data.DataSet
            DS = clsUp.getTipOftheWeekByDate(SubDate1, SubDate2)

            MyDataGrid.DataSource = DS
            MyDataGrid.DataBind()

            clsUp = Nothing
            'If MyDataGrid.Rows.Count > 0 Then
            '    MyDataGrid.ShowFooter = True
            '    MyDataGrid.UseAccessibleHeader = True
            '    MyDataGrid.HeaderRow.TableSection = TableRowSection.TableHeader
            '    MyDataGrid.FooterRow.TableSection = TableRowSection.TableFooter
            'End If
        Catch ex As Exception
        End Try
    End Sub

    Protected Sub MyDataGrid_Sorting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewSortEventArgs) Handles MyDataGrid.Sorting

    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        GetData()
    End Sub
End Class
