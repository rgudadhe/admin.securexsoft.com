Imports System.Data.SqlClient

Partial Class Billing_LCMethods_LCMethodology
    Inherits BasePage

   
    Protected Sub BindData()
        Dim obj As New ETS.BL.UsersIDMappings
        With obj
            .contractorid = Session("contractorid").ToString
        End With
        'obj._WhereString = " Order by uname "
        Dim DTSet As System.Data.DataSet = obj.getUsersIDMappingsList
        If DTSet.Tables.Count > 0 Then
            MyDataGrid.DataSource = DTSet.Tables(0)
            MyDataGrid.DataBind()
        End If
        obj = Nothing
    End Sub



    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        BindData()
    End Sub

    Protected Sub MyDataGrid_RowCancelingEdit(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCancelEditEventArgs) Handles MyDataGrid.RowCancelingEdit
        MyDataGrid.EditIndex = -1
    End Sub

    Protected Sub MyDataGrid_RowEditing(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewEditEventArgs) Handles MyDataGrid.RowEditing
        'Set the edit index.
        MyDataGrid.EditIndex = e.NewEditIndex
        'Bind data to the GridView control.
        'MyDataGrid.BindData()
        BindData()

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
        Dim obj As New ETS.BL.UsersIDMappings
        With obj
            .ContractorID = Session("contractorid").ToString
            .AutoID = MyDataGrid.DataKeys(e.RowIndex).Value.ToString
            .MTID = (CType((row.Cells(2).Controls(0)), TextBox)).Text
            .QAID = (CType((row.Cells(3).Controls(0)), TextBox)).Text
            ' .QABID = (CType((row.Cells(4).Controls(0)), TextBox)).Text
            'response.Write  row.Cells(4).Controls(0)
            .UpdateUsersIDMappingsDetails()
        End With
        obj = Nothing
        MyDataGrid.EditIndex = -1
        BindData()
    End Sub
End Class
