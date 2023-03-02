Imports System.Data.SqlClient

Partial Class Billing_LCMethods_LCMethodology
    Inherits BasePage

   

  

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            rcount.Value = 1
            'Response.Write("Yes")
            Dim LI1 As New ListItem
            LI1.Text = "Select Item"
            LI1.Value = ""
            DLItem1.Items.Add(LI1)

            Dim objAct As New ETS.BL.Accounts
            Dim DTSet1 As System.Data.DataSet = objAct.getAccountList
            If DTSet1.Tables.Count > 0 Then
                Dim DV As New Data.DataView(DTSet1.Tables(0))
                DV.Sort = " AccountName Asc"
                DLAct.DataSource = DV
                DLAct.DataTextField = "AccountName"
                DLAct.DataValueField = "AccountID"
                DLAct.DataBind()
            End If
            objAct = Nothing


            Dim objItem As New ETS.BL.ItemDetails
            Dim DTSet2 As System.Data.DataSet = objItem.getVASItemList
            If DTSet2.Tables.Count > 0 Then
                If DTSet2.Tables(0).Rows.Count > 0 Then
                    For Each DRRec As Data.DataRow In DTSet2.Tables(0).Rows
                        Dim LI As New ListItem
                        LI.Text = DRRec("Item")
                        LI.Value = DRRec("ItemID").ToString & "#" & FormatNumber(DRRec("rate").ToString, 3) & "#" & DRRec("mode").ToString & "#" & DRRec("Description")
                        DLItem1.Items.Add(LI)
                    Next

                End If
                '    DLItem1.DataSource = DTSet2.Tables(0)
                '    DLItem1.DataTextField = "Item"
                '    DLItem1.DataValueField = "ItemDetails"
                '    DLItem1.DataBind()
            End If
            'Dim strConn As String
            'strConn = System.Configuration.ConfigurationManager.AppSettings("ETSCon")

            'Dim SQLCmd1 As New SqlCommand("Select * from AdminETS.dbo.tblaccounts where contractorid='" & Session("contractorid").ToString & "' order by Accountname", New SqlConnection(strConn))
            'Try
            '    SQLCmd1.Connection.Open()
            '    Dim DRRec1 As SqlDataReader = SQLCmd1.ExecuteReader()
            '    If DRRec1.HasRows = True Then
            '        'Response.Write("Yes")
            '        While DRRec1.Read

            '            '  If DRRec.Read Then
            '            Dim LI As New ListItem
            '            'Response.Write(DRRec("Methodname"))
            '            LI.Text = DRRec1("Accountname")
            '            LI.Value = DRRec1("AccountID").ToString
            '            DLAct.Items.Add(LI)
            '        End While
            '    End If
            '    DRRec1.Close()
            'Finally
            '    If SQLCmd1.Connection.State = System.Data.ConnectionState.Open Then
            '        SQLCmd1.Connection.Close()
            '        SQLCmd1 = Nothing
            '    End If
            'End Try

            'Dim SQLCmd As New SqlCommand("Select * from AdminSecureweb.dbo.ItemDetails where contractorid='" & Session("contractorid").ToString & "' order by Description", New SqlConnection(strConn))
            'Try
            '    SQLCmd.Connection.Open()
            '    Dim DRRec As SqlDataReader = SQLCmd.ExecuteReader()
            '    If DRRec.HasRows = True Then
            '        'Response.Write("Yes")
            '        While DRRec.Read

            '            '  If DRRec.Read Then
            '            Dim LI As New ListItem
            '            'Response.Write(DRRec("Methodname"))
            '            LI.Text = DRRec("Item")
            '            LI.Value = DRRec("ItemID").ToString & "#" & FormatNumber(DRRec("rate").ToString, 3) & "#" & DRRec("mode").ToString & "#" & DRRec("Description")
            '            DLItem1.Items.Add(LI)
            '        End While
            '    End If
            '    DRRec.Close()
            'Finally
            '    If SQLCmd.Connection.State = System.Data.ConnectionState.Open Then
            '        SQLCmd.Connection.Close()
            '        SQLCmd = Nothing
            '    End If
            'End Try
            sdate1.Text = Today
            DLItem1.Attributes.Add("onChange", "sm_jump(this);")
            txtQua1.Attributes.Add("onBlur", "CalcValue(this);")
            txtAmt1.Attributes.Add("onBlur", "CalcValue(this);")
        End If


    End Sub


    Protected Sub btnUpdate_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnUpdate.Click
        'Dim strConn As String
        'Dim strQuery As String
        'strConn = System.Configuration.ConfigurationManager.AppSettings("ETSCon")
        'Response.Write(New ETS.DAL.ETS.DAL.DALBASE().GetConnectionString)
        'Response.Write(DLAct.SelectedValue)
        Dim i As Integer
        Dim Rcount As Integer
        Dim DLItem As String
        Dim DLSelItemVal() As String
        Dim txtQua As Double
        Dim txtAmt As Double
        Dim txtDesc As String
        Dim txtTtl As Double
        Dim sdate
        Rcount = Request("rcount")
        Dim DT As New Data.DataTable
        Dim DC1 As New Data.DataColumn("itemid")
        Dim DC2 As New Data.DataColumn("Descr")
        Dim DC3 As New Data.DataColumn("quantity")
        Dim DC4 As New Data.DataColumn("Amount")
        Dim DC5 As New Data.DataColumn("totamount")
        Dim DC6 As New Data.DataColumn("mode")
        Dim DC7 As New Data.DataColumn("servicedate")
        DT.Columns.Add(DC1)
        DT.Columns.Add(DC2)
        DT.Columns.Add(DC3)
        DT.Columns.Add(DC4)
        DT.Columns.Add(DC5)
        DT.Columns.Add(DC6)
        DT.Columns.Add(DC7)
        For i = 1 To Rcount
            Dim DR As Data.DataRow = DT.NewRow
            DLItem = "DLItem" & i
            DLSelItemVal = Split(Request(DLItem), "#")
            txtDesc = Request("txtDesc" & i)
            txtQua = Request("txtQua" & i)
            txtAmt = Request("txtAmt" & i)
            txtTtl = Request("txtTtl" & i)
            sdate = Request("sdate" & i)
            DR("itemid") = DLSelItemVal(0)
            DR("Descr") = txtDesc
            DR("quantity") = txtQua
            DR("Amount") = txtAmt
            DR("totamount") = txtTtl
            DR("mode") = DLSelItemVal(2)
            DR("servicedate") = sdate
            DT.Rows.Add(DR)
           
        Next
        Dim objInvItem As New ETS.BL.InvoiceItemDetails
        'Response.Write(objInvItem.UpdateBatchInvoiceItemDetails(DT, DLAct.SelectedValue))
        If objInvItem.UpdateBatchInvoiceItemDetails(DT, DLAct.SelectedValue) Then
            lblDisp.Text = "VAS Details has been updated successfully."
            txtDesc1.Text = ""
            txtQua1.Text = ""
            txtAmt1.Text = ""
            txtTtl1.Text = ""
            For i = 0 To DLItem1.Items.Count - 1
                DLItem1.Items(i).Selected = False
            Next
            DLItem1.Items(0).Selected = True
            sdate = Now.ToShortDateString
            pnl.Visible = False


        Else
            lblDisp.Text = "Issue in updating details."
        End If
        
    End Sub
End Class
