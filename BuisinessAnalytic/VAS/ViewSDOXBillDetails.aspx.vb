Imports System.Data.SqlClient

Partial Class Billing_LCMethods_LCMethodology
    Inherits BasePage

   

   

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub

    'Protected Sub DLMethod_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles DLMethod.SelectedIndexChanged
    '    'CHheader.Checked = False
    '    'CHfooter.Checked = False
    '    'CHbody.Checked = False
    '    'CHBIU.Checked = False
    '    'CHspaces.Checked = False
    '    'CHSchars.Checked = False
    '    'CHsrt.Checked = False
    '    'If DLMethod.SelectedValue = "Pages" Then
    '    '    TableCell7.Enabled = False
    '    '    TableCell8.Enabled = False
    '    '    TableCell10.Enabled = False
    '    '    TableCell11.Enabled = False
    '    '    TableCell12.Enabled = False
    '    '    TableCell13.Enabled = False
    '    '    TableCell14.Enabled = False
    '    '    TableCell15.Enabled = False
    '    'ElseIf DLMethod.SelectedValue = "Words" Then
    '    '    TableCell7.Enabled = True
    '    '    TableCell8.Enabled = False
    '    '    TableCell10.Enabled = True
    '    '    TableCell11.Enabled = False
    '    '    TableCell12.Enabled = True
    '    '    TableCell13.Enabled = False
    '    '    TableCell14.Enabled = False
    '    '    TableCell15.Enabled = False
    '    'ElseIf DLMethod.SelectedValue = "GrossLines" Then
    '    '    TableCell7.Enabled = True
    '    '    TableCell8.Enabled = False
    '    '    TableCell10.Enabled = True
    '    '    TableCell11.Enabled = False
    '    '    TableCell12.Enabled = True
    '    '    TableCell13.Enabled = False
    '    '    TableCell14.Enabled = False
    '    '    TableCell15.Enabled = False
    '    'ElseIf DLMethod.SelectedValue = "AllLines" Then
    '    '    TableCell7.Enabled = True
    '    '    TableCell8.Enabled = False
    '    '    TableCell10.Enabled = True
    '    '    TableCell11.Enabled = False
    '    '    TableCell12.Enabled = True
    '    '    TableCell13.Enabled = False
    '    '    TableCell14.Enabled = False
    '    '    TableCell15.Enabled = False
    '    'ElseIf DLMethod.SelectedValue = "CharsPerLine" Then
    '    '    TableCell7.Enabled = True
    '    '    TableCell8.Enabled = True
    '    '    TableCell10.Enabled = True
    '    '    TableCell11.Enabled = True
    '    '    TableCell12.Enabled = True
    '    '    TableCell13.Enabled = True
    '    '    TableCell14.Enabled = True
    '    '    TableCell15.Enabled = True
    '    'End If

    'End Sub

    'Protected Sub CHspaces_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles CHspaces.CheckedChanged
    '    'If CHspaces.Checked = True Then
    '    '    CHsrt.Checked = False
    '    'End If
    'End Sub

    'Protected Sub CHsrt_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles CHsrt.CheckedChanged
    '    'If CHsrt.Checked = True Then
    '    '    CHspaces.Checked = False
    '    'End If
    'End Sub

    'Protected Sub DLLCMethod_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles DLLCMethod.SelectedIndexChanged
    '    CHheader.Checked = False
    '    CHfooter.Checked = False
    '    CHbody.Checked = False
    '    CHBIU.Checked = False
    '    CHspaces.Checked = False
    '    CHSchars.Checked = False
    '    CHsrt.Checked = False

    '    If DLLCMethod.Items(0).Value = "" And DLLCMethod.Items(0).Text = "Any" Then
    '        DLLCMethod.Items.RemoveAt(0)
    '    End If
    '    Dim strConn As String
    '    strConn = System.Configuration.ConfigurationManager.AppSettings("ETSCon")
    '    Dim SQLCmd As New SqlCommand("Select * from tblLCMethodology where trackid ='" & DLLCMethod.SelectedValue & "'", New SqlConnection(strConn))
    '    SQLCmd.Connection.Open()
    '    Dim DRRec As SqlDataReader = SQLCmd.ExecuteReader()
    '    If DRRec.HasRows = True Then
    '        'Response.Write("Yes")
    '        If DRRec.Read Then
    '            Panel1.Visible = True
    '            Table2.Visible = True
    '            'TableCell3.Enabled = True
    '            'TxtDescr.Text = DRRec("description")
    '            If DRRec("CountMethod") = "Pages" Then
    '                DLMethod.Items(1).Selected = False
    '                DLMethod.Items(2).Selected = False
    '                DLMethod.Items(3).Selected = False
    '                DLMethod.Items(4).Selected = False
    '                DLMethod.Items(0).Selected = True
    '            ElseIf DRRec("CountMethod") = "CharsPerLine" Then
    '                DLMethod.Items(0).Selected = False
    '                DLMethod.Items(2).Selected = False
    '                DLMethod.Items(3).Selected = False
    '                DLMethod.Items(4).Selected = False
    '                DLMethod.Items(1).Selected = True
    '            ElseIf DRRec("CountMethod") = "Words" Then
    '                DLMethod.Items(0).Selected = False
    '                DLMethod.Items(1).Selected = False
    '                DLMethod.Items(3).Selected = False
    '                DLMethod.Items(4).Selected = False
    '                DLMethod.Items(2).Selected = True
    '            ElseIf DRRec("CountMethod") = "GrossLines" Then
    '                DLMethod.Items(0).Selected = False
    '                DLMethod.Items(1).Selected = False
    '                DLMethod.Items(2).Selected = False
    '                DLMethod.Items(4).Selected = False
    '                DLMethod.Items(3).Selected = True
    '            ElseIf DRRec("CountMethod") = "AllLines" Then
    '                DLMethod.Items(0).Selected = False
    '                DLMethod.Items(1).Selected = False
    '                DLMethod.Items(2).Selected = False
    '                DLMethod.Items(3).Selected = False
    '                DLMethod.Items(4).Selected = True
    '            End If
    '            CHheader.Checked = DRRec("RptHeader")
    '            CHfooter.Checked = DRRec("RptFooter")
    '            CHbody.Checked = DRRec("RptBody")
    '            CHBIU.Checked = DRRec("RptBIU")
    '            CHspaces.Checked = DRRec("RptSpaces")
    '            CHSchars.Checked = DRRec("RptShifted")
    '            CHsrt.Checked = DRRec("RptSCT")

    '            If DLMethod.SelectedValue = "Pages" Then
    '                TableCell7.Enabled = False
    '                TableCell8.Enabled = False
    '                TableCell10.Enabled = False
    '                TableCell11.Enabled = False
    '                TableCell12.Enabled = False
    '                TableCell13.Enabled = False
    '                TableCell14.Enabled = False
    '                TableCell15.Enabled = False
    '            ElseIf DLMethod.SelectedValue = "Words" Then
    '                TableCell7.Enabled = True
    '                TableCell8.Enabled = False
    '                TableCell10.Enabled = True
    '                TableCell11.Enabled = False
    '                TableCell12.Enabled = True
    '                TableCell13.Enabled = False
    '                TableCell14.Enabled = False
    '                TableCell15.Enabled = False
    '            ElseIf DLMethod.SelectedValue = "GrossLines" Then
    '                TableCell7.Enabled = True
    '                TableCell8.Enabled = False
    '                TableCell10.Enabled = True
    '                TableCell11.Enabled = False
    '                TableCell12.Enabled = True
    '                TableCell13.Enabled = False
    '                TableCell14.Enabled = False
    '                TableCell15.Enabled = False
    '            ElseIf DLMethod.SelectedValue = "AllLines" Then
    '                TableCell7.Enabled = True
    '                TableCell8.Enabled = False
    '                TableCell10.Enabled = True
    '                TableCell11.Enabled = False
    '                TableCell12.Enabled = True
    '                TableCell13.Enabled = False
    '                TableCell14.Enabled = False
    '                TableCell15.Enabled = False
    '            ElseIf DLMethod.SelectedValue = "CharsPerLine" Then
    '                TableCell7.Enabled = True
    '                TableCell8.Enabled = True
    '                TableCell10.Enabled = True
    '                TableCell11.Enabled = True
    '                TableCell12.Enabled = True
    '                TableCell13.Enabled = True
    '                TableCell14.Enabled = True
    '                TableCell15.Enabled = True
    '            End If
    '        End If
    '    End If
    '    SQLCmd.Connection.Close()
    'End Sub

    

    'Protected Sub DLAct_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles DLAct.SelectedIndexChanged
    '    If DLAct.Items(0).Value = "" And DLAct.Items(0).Text = "Any" Then
    '        DLAct.Items.RemoveAt(0)
    '    End If
    '    Table2.Visible = True
    '    Dim strConn As String
    '    strConn = System.Configuration.ConfigurationManager.AppSettings("ETSCon")
    '    Dim SQLCmd As New SqlCommand("Select * from tblAccounts where accountid ='" & DLAct.SelectedValue & "'", New SqlConnection(strConn))
    '    SQLCmd.Connection.Open()
    '    Dim DRRec As SqlDataReader = SQLCmd.ExecuteReader()
    '    If DRRec.HasRows = True Then
    '        'Response.Write("Yes")
    '        If DRRec.Read Then
    '            TxtDescr.Text = DRRec("Description").ToString
    '            TxtActNumber.Text = DRRec("AccountNo").ToString
    '            TxtBillNumber.Text = DRRec("BillActnumber").ToString
    '            TxtContPerson.Text = DRRec("BillContName").ToString
    '            TxtTel.Text = DRRec("BillContNO").ToString
    '            TxtEMail.Text = DRRec("BillEmail").ToString
    '            TxtEFax.Text = DRRec("BillFax").ToString
    '            TxtAddress.Text = DRRec("BillAddress").ToString
    '            TxtCity.Text = DRRec("BillCity").ToString
    '            TxtState.Text = DRRec("BillState").ToString
    '            TxtCntry.Text = DRRec("BillCntry").ToString
    '            TxtZip.Text = DRRec("BillZip").ToString
    '            Dim i As Integer
    '            Dim SelCount As Integer
    '            SelCount = 0
    '            For i = 0 To DLGroup.Items.Count - 1
    '                DLGroup.Items(i).Selected = False
    '                If DLGroup.Items(i).Value = DRRec("GrpActID").ToString Then
    '                    SelCount = i
    '                End If
    '            Next
    '            DLGroup.Items(SelCount).Selected = True

    '            SelCount = 0
    '            For i = 0 To DLCategory.Items.Count - 1
    '                DLCategory.Items(i).Selected = False
    '                If DLCategory.Items(i).Value = DRRec("GrpActID").ToString Then
    '                    SelCount = i
    '                End If
    '            Next
    '            DLCategory.Items(SelCount).Selected = True

    '            SelCount = 0
    '            For i = 0 To DLCategory.Items.Count - 1
    '                DLCategory.Items(i).Selected = False
    '                If DLCategory.Items(i).Value = DRRec("Category").ToString Then
    '                    SelCount = i
    '                End If
    '            Next
    '            DLCategory.Items(SelCount).Selected = True

    '            SelCount = 0
    '            For i = 0 To DLMode.Items.Count - 1
    '                DLMode.Items(i).Selected = False
    '                If DLMode.Items(i).Value = DRRec("Mode").ToString Then
    '                    SelCount = i
    '                End If
    '            Next
    '            DLMode.Items(SelCount).Selected = True

    '            SelCount = 0
    '            For i = 0 To DLDelv.Items.Count - 1
    '                DLDelv.Items(i).Selected = False

    '                If Trim(DLDelv.Items(i).Value) = Trim(DRRec("DelMode").ToString) Then

    '                    SelCount = i
    '                End If
    '            Next
    '            DLDelv.Items(SelCount).Selected = True


    '            SelCount = 0
    '            For i = 0 To DLTerm.Items.Count - 1
    '                DLTerm.Items(i).Selected = False
    '                If DLTerm.Items(i).Value = DRRec("PayTerm").ToString Then
    '                    SelCount = i
    '                End If
    '            Next
    '            DLTerm.Items(SelCount).Selected = True

    '            SelCount = 0
    '            For i = 0 To DLLCMethod.Items.Count - 1
    '                DLLCMethod.Items(i).Selected = False
    '                If DLLCMethod.Items(i).Value = DRRec("LCMethodID").ToString Then
    '                    SelCount = i
    '                End If
    '            Next
    '            DLLCMethod.Items(SelCount).Selected = True


    '        End If
    '    End If

    '    SQLCmd.Connection.Close()

    'End Sub

    'Protected Sub MyDataGrid_RowUpdating(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewUpdateEventArgs) Handles MyDataGrid.RowUpdating
    '    if e.

    'End Sub

    Protected Sub MyDataGrid_RowUpdated(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewUpdatedEventArgs) Handles MyDataGrid.RowUpdated
        If e.AffectedRows > 0 Then
            lblDisp.Text = "Record has been updated successfully"
        Else
            lblDisp.Text = "Error in updating record. Please check with System Administrator for more details."
        End If
    End Sub


    Protected Sub tblsubmit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles tblsubmit.Click
        Dim strConn As String
        strConn = System.Configuration.ConfigurationManager.AppSettings("SDoxCon")
        'Response.Write(DLMonth.SelectedValue & "/1/" & DLYear.SelectedValue)
        'Response.End()
        Dim strDate As Date = DLMonth.SelectedValue & "/1/" & DLYear.SelectedValue
        Dim SQLCmd As New SqlCommand("Securedox.dbo.SF_getSDOXBillingDetails", New SqlConnection(strConn))
        SQLCmd.CommandType = Data.CommandType.StoredProcedure
        SQLCmd.Parameters.AddWithValue("@Month", DLMonth.SelectedValue)
        SQLCmd.Parameters.AddWithValue("@year", DLYear.SelectedValue)
        SQLCmd.Parameters.AddWithValue("@strDate", strDate.AddMonths(1))
        Try
            SQLCmd.Connection.Open()

            Dim DRRec As SqlDataReader = SQLCmd.ExecuteReader()
            Dim DT As New System.Data.DataTable
            DT.Load(DRRec)
            MyDataGrid.DataSource = DT
            MyDataGrid.DataBind()
            '        SQLCmd.Connection.Close()

        Finally
            If SQLCmd.Connection.State = System.Data.ConnectionState.Open Then
                SQLCmd.Connection.Close()
                SQLCmd = Nothing
            End If
        End Try
    End Sub
End Class
