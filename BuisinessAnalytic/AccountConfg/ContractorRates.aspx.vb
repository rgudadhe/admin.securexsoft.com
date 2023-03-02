Imports System.Data.SqlClient

Partial Class Billing_LCMethods_LCMethodology
    Inherits BasePage
    Protected Sub BindData()
        Dim obj As New ETS.BL.ContractorRates
        With obj
            .ContractorID = Session("contractorid").ToString
            .PlatformID = DLPlatform.SelectedValue
        End With
        'obj._WhereString = " Order by uname "
        Dim DTSet As System.Data.DataSet = obj.getContractorRatesList
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
        Dim obj As New ETS.BL.ContractorRates

        Dim RowAffected As Integer
        With obj
            obj.ContractorID = Session("contractorid").ToString
            obj.Level = MyDataGrid.DataKeys(e.RowIndex).Value.ToString()
            obj.PlatformID = DLPlatform.SelectedValue
            obj.DeleteContractorRates(oConn, thisTransaction)

            RowAffected = obj.InsertContractorRates(oConn, thisTransaction, "[PlatformID],[Level],[Rate],[Currency],[contractorID],[updatedate]", "'" & DLPlatform.SelectedValue & "','" & MyDataGrid.DataKeys(e.RowIndex).Value.ToString() & "','" & (CType((row.Cells(2).Controls(0)), TextBox)).Text & "','" & (CType((row.Cells(3).Controls(0)), TextBox)).Text & "', '" & Session("contractorid").ToString & "','" & Now & "'")
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

        'Dim strConn As String
        'Dim row = MyDataGrid.Rows(e.RowIndex)
        'Dim obj As New ETS.BL.PlatformRates
        'With obj
        '    Dim sQuery1 As String
        '    strConn = System.Configuration.ConfigurationManager.AppSettings("ETSCon")
        '    sQuery1 = "Delete from AdminETS.dbo.tblPlatformHBARates where Level ='" & MyDataGrid.DataKeys(e.RowIndex).Value.ToString() & "' and ContractorID='" & Session("contractorid").ToString & "' and PlatformID='" & DLPlatform.SelectedValue & "' "
        '    Dim cmd As New SqlCommand(sQuery1, New SqlConnection(strConn))
        '    Try
        '        cmd.Connection.Open()
        '        cmd.ExecuteNonQuery()
        '    Catch ex As Exception
        '        Response.Write(ex.Message)
        '    Finally
        '        If cmd.Connection.State <> Data.ConnectionState.Open Then
        '            cmd.Connection.Close()
        '            cmd.Connection = Nothing
        '        End If
        '    End Try
        '    Dim sQuery2 As String
        '    sQuery2 = "INSERT INTO AdminETS.dbo.tblPlatformHBARates ([PlatformID],[Level],[Rate],[Currency],[contractorID],[updatedate])VALUES('" & DLPlatform.SelectedValue & "','" & MyDataGrid.DataKeys(e.RowIndex).Value.ToString() & "','" & (CType((row.Cells(2).Controls(0)), TextBox)).Text & "','" & (CType((row.Cells(3).Controls(0)), TextBox)).Text & "', '" & Session("contractorid").ToString & "','" & Now & "')"
        '    'Response.Write(sQuery2)
        '    Dim cmd1 As New SqlCommand(sQuery2, New SqlConnection(strConn))
        '    Try
        '        cmd1.Connection.Open()
        '        cmd1.ExecuteNonQuery()
        '    Catch ex As Exception
        '        Response.Write(ex.Message)
        '    Finally
        '        If cmd1.Connection.State <> Data.ConnectionState.Open Then
        '            cmd1.Connection.Close()
        '            cmd1.Connection = Nothing
        '        End If
        '    End Try

        '    '.ContractorID = Session("contractorid").ToString
        '    '.PlatformID = DLPlatform.SelectedValue
        '    '._WhereString.Append(" and Level = '" & MyDataGrid.DataKeys(e.RowIndex).Value.ToString() & "'")
        '    '.Level = MyDataGrid.DataKeys(e.RowIndex).Value.ToString()
        '    ''.DeletePlatformRates()
        '    ''.Target = (CType((row.Cells(2).Controls(0)), TextBox)).Text
        '    ''.Salary = (CType((row.Cells(4).Controls(0)), TextBox)).Text
        '    ''.Currency = (CType((row.Cells(3).Controls(0)), TextBox)).Text
        '    ''.UpdateTargetDetails()
        'End With
        'obj = Nothing
        'MyDataGrid.EditIndex = -1
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
