Imports System.Data
Imports System.Data.SqlClient
Imports System.Data.SqlDbType




Partial Class BuisinessAnalytic_Configuration_Default
    Inherits BasePage

    Public strConn As String = System.Configuration.ConfigurationManager.AppSettings("ETSCon")
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'lblMsg.Text = String.Empty
       
        BindData()


    End Sub



   
    Protected Sub BindData()
        'Panel1.Visible = True
        Dim obj As New ETS.BL.PhysiciansLinesDeductionsNew

        Dim DTSet As System.Data.DataSet = obj.getPhysiansListForLinesDeductionByCon(session("ContractorID").Tostring)
        'response.write(DTSet.Tables(0).rows.Count)
        If DTSet.Tables.Count > 0 Then
            MyDataGrid.DataSource = DTSet.Tables(0)
            MyDataGrid.DataBind()
        End If
        obj = Nothing
    End Sub
    ''Protected Sub Button4_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button4.Click
    ''    Dim oConn As New Data.SqlClient.SqlConnection
    ''    Dim oCommand As New Data.SqlClient.SqlCommand
    ''    Dim ConString As String = System.Configuration.ConfigurationManager.AppSettings("ETSCon")
    ''    Dim thisTransaction As Data.SqlClient.SqlTransaction
    ''    oConn.ConnectionString = ConString
    ''    oConn.Open()
    ''    thisTransaction = oConn.BeginTransaction()
    ''    Dim obj As New ETS.BL.PhysiciansLinesDeductions

    ''    Dim RowAffected As Integer
    ''    With obj
    ''        obj.PhysicianID = DLPhysician.SelectedValue
    ''        obj.DeletetblPhysiciansLinesDeductions(oConn, thisTransaction)
    ''        RowAffected = obj.InserttblPhysiciansLinesDeductions(oConn, thisTransaction, "[PhysicianID],[Mode],[Value],[updatedate]", "'" & DLPhysician.SelectedValue & "', '" & DLMode.SelectedValue & "','" & TXTValue.Text & "','" & Now & "'")
    ''        If RowAffected > 0 Then
    ''            thisTransaction.Commit()
    ''        Else
    ''            thisTransaction.Rollback()
    ''        End If

    ''    End With
    ''    obj = Nothing


    ''    ShowData()
    ''End Sub

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
        ' response.write(e.AffectedRows)
        If e.AffectedRows > 0 Then
            lblMsg.Text = "Record has been updated successfully"
        Else
            lblMsg.Text = "Error in updating record. Please check with System Administrator for more details."
        End If
    End Sub


    Protected Sub MyDataGrid_RowUpdating(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewUpdateEventArgs) Handles MyDataGrid.RowUpdating
        Try


            Dim row = MyDataGrid.Rows(e.RowIndex)
            Dim obj As New ETS.BL.PhysiciansLinesDeductionsNew
            With obj
                '.contractorid = Session("contractorid").ToString
                .PhysicianID = MyDataGrid.DataKeys(e.RowIndex).Value.ToString
                '.LPM = (CType((row.Cells(3).Controls(0)), TextBox)).Text
                '.Percentage = (CType((row.Cells(4).Controls(0)), TextBox)).Text
                '.Fixed = (CType((row.Cells(5).Controls(0)), TextBox)).Text
                '.Units = (CType((row.Cells(6).Controls(0)), TextBox)).Text
                .LPM = IIf(String.IsNullOrEmpty((CType((row.Cells(3).Controls(0)), TextBox)).Text), 0, (CType((row.Cells(3).Controls(0)), TextBox)).Text)
                .Percentage = IIf(String.IsNullOrEmpty((CType((row.Cells(4).Controls(0)), TextBox)).Text), 0, (CType((row.Cells(4).Controls(0)), TextBox)).Text)
                .Fixed = IIf(String.IsNullOrEmpty((CType((row.Cells(5).Controls(0)), TextBox)).Text), 0, (CType((row.Cells(5).Controls(0)), TextBox)).Text)
                .Units = IIf(String.IsNullOrEmpty((CType((row.Cells(6).Controls(0)), TextBox)).Text), 0, (CType((row.Cells(6).Controls(0)), TextBox)).Text)
                '.Salary = (CType((row.Cells(5).Controls(0)), TextBox)).Text
                '.Currency = (CType((row.Cells(4).Controls(0)), TextBox)).Text
                If .UpdatetblPhysiciansLinesDeductionsDetails() <> 1 Then
                    '.contractorid = Session("contractorid").ToString
                    .InserttblPhysiciansLinesDeductions()
                End If
            End With
            obj = Nothing
            lblMsg.Text = "Record has been updated successfully"
            MyDataGrid.EditIndex = -1
            BindData()
        Catch ex As Exception
            response.write(ex.message)
        End Try
    End Sub
End Class
