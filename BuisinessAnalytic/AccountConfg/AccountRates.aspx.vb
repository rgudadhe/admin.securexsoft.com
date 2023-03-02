Imports System.Data.SqlClient
Imports System.Data

Partial Class Billing_LCMethods_LCMethodology
    Inherits BasePage
    Protected Sub BindData()
        Dim obj As New ETS.BL.AccountWiseRates
        With obj
            .ContractorID = Session("contractorid").ToString
            .AccountID = DLAccount.SelectedValue
        End With
        'obj._WhereString = " Order by uname "
        Dim DTSet As System.Data.DataSet = obj.getAccountWiseRatesList
        If DTSet.Tables.Count > 0 Then
            MyDataGrid.DataSource = DTSet.Tables(0)
            MyDataGrid.DataBind()
        End If
        obj = Nothing
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
        Dim oConn As New Data.SqlClient.SqlConnection
        Dim oCommand As New Data.SqlClient.SqlCommand
        Dim ConString As String = System.Configuration.ConfigurationManager.AppSettings("ETSCon")
        Dim thisTransaction As Data.SqlClient.SqlTransaction
        oConn.ConnectionString = ConString
        oConn.Open()
        thisTransaction = oConn.BeginTransaction()
        Dim row = MyDataGrid.Rows(e.RowIndex)
        Dim obj As New ETS.BL.AccountWiseRates

        Dim RowAffected As Integer
        With obj
            obj.ContractorID = Session("contractorid").ToString
            obj.Level = MyDataGrid.DataKeys(e.RowIndex).Value.ToString()
            obj.AccountID = DLAccount.SelectedValue
            obj.DeleteAccountWiseRates(oConn, thisTransaction)

            RowAffected = obj.InsertAccountWiseRates(oConn, thisTransaction, "[AccountID],[Level],[Rate],[Currency],[contractorID],[updatedate]", "'" & DLAccount.SelectedValue & "','" & MyDataGrid.DataKeys(e.RowIndex).Value.ToString() & "','" & (CType((row.Cells(2).Controls(0)), TextBox)).Text & "','" & (CType((row.Cells(3).Controls(0)), TextBox)).Text & "', '" & Session("contractorid").ToString & "','" & Now & "'")
            If RowAffected > 0 Then
                thisTransaction.Commit()
            Else
                thisTransaction.Rollback()
            End If

        End With
        obj = Nothing
        MyDataGrid.EditIndex = -1
        If oConn.State = Data.ConnectionState.Open Then
            oConn.Close()
            oConn = Nothing
        End If

        
        BindData()
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            Dim objAct As New ETS.BL.Accounts
            Dim DTSet1 As System.Data.DataSet = objAct.getAccountList

            If DTSet1.Tables.Count > 0 Then

                Dim dv As DataView
                dv = New DataView
                With dv
                    .Table = DTSet1.Tables(0)
                    .AllowDelete = True
                    .AllowEdit = True
                    .AllowNew = True
                    .RowFilter = "Indirect='False' OR Indirect IS NULL"
                    .Sort = "AccountName Asc"
                End With

                'Dim DTView As New DataView(DTSet1.Tables(0), "Indirect='True'", "AccountName Asc", DataViewRowState.ModifiedCurrent)
                DLAccount.DataSource = dv
                DLAccount.DataTextField = "AccountName"
                DLAccount.DataValueField = "AccountID"
                DLAccount.DataBind()
            End If
            objAct = Nothing
        End If
        BindData()
    End Sub
End Class
