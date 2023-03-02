Imports System.Data.SqlClient

Partial Class Billing_LCMethods_LCMethodology
    Inherits BasePage
    Protected Sub BindData()
        Dim obj As New ETS.BL.PlatformRates
        With obj
            .ContractorID = Session("contractorid").ToString
            .PlatformID = DLPlatform.SelectedValue
        End With
        'obj._WhereString = " Order by uname "
        Dim DTSet As System.Data.DataSet = obj.getPlatformRatesList
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
        MyDataGrid.Columns(0).InsertVisible = False
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
        'Dim thisTransaction As Data.SqlClient.SqlTransaction
        'oConn.ConnectionString = ConString
        'oConn.Open()
        'thisTransaction = oConn.BeginTransaction()
        Dim row = MyDataGrid.Rows(e.RowIndex)
        Dim obj As New ETS.BL.PlatformRates

        'Dim RowAffected As Integer
        With obj
            obj.ContractorID = Session("contractorid").ToString
            obj.TrackID = MyDataGrid.DataKeys(e.RowIndex).Value.ToString()
            obj.PlatformID = DLPlatform.SelectedValue
            obj.Rate = (CType((row.Cells(2).Controls(0)), TextBox)).Text
            'obj.Currency = (CType((row.Cells(3).Controls(0)), TextBox)).Text
            obj.UpdatePlatformRatesDetails()

            'RowAffected = obj.InsertPlatformRates(oConn, thisTransaction, "[PlatformID],[Level],[Rate],[Currency],[contractorID],[updatedate]", "'" & DLPlatform.SelectedValue & "','" & MyDataGrid.DataKeys(e.RowIndex).Value.ToString() & "','" & (CType((row.Cells(2).Controls(0)), TextBox)).Text & "','" & (CType((row.Cells(3).Controls(0)), TextBox)).Text & "', '" & Session("contractorid").ToString & "','" & Now & "'")
            'If RowAffected > 0 Then
            '    thisTransaction.Commit()
            'Else
            '    thisTransaction.Rollback()
            'End If

        End With
        obj = Nothing
        MyDataGrid.EditIndex = -1
        'If oConn.State = Data.ConnectionState.Open Then
        '    oConn.Close()
        '    oConn = Nothing
        'End If
        BindData()
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            Dim strConn As String
            strConn = System.Configuration.ConfigurationManager.AppSettings("ETSCon")
            Dim LI2 As New ListItem
            LI2.Text = "SecureXFlow"
            LI2.Value = "11111111-1111-1111-1111-111111111111"
            DLPlatform.Items.Add(LI2)
            Dim oConn As New Data.SqlClient.SqlConnection(strConn)
            Try
                oConn.Open()
                Dim SQLCmd2 As New SqlCommand("Select AccountName, AccountID from tblaccounts where IsPlatForm = 'True' and contractorID='" & Session("ContractorID") & "' and (IsDeleted is null or IsDeleted=0)", oConn)
                Dim DRRec2 As SqlDataReader = SQLCmd2.ExecuteReader()
                If DRRec2.HasRows = True Then
                    While DRRec2.Read
                        Dim LI As New ListItem
                        LI.Text = DRRec2("accountname").ToString
                        LI.Value = DRRec2("accountid").ToString
                        DLPlatform.Items.Add(LI)
                    End While
                End If
                DRRec2.Close()
            Catch ex As Exception
                Response.Write(ex.Message)
            Finally
                If oConn.State <> System.Data.ConnectionState.Open Then
                    oConn.Close()
                    oConn = Nothing
                End If
            End Try
        End If
        BindData()
    End Sub
End Class
