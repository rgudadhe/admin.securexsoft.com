Imports System.Data.Sqlclient
Imports System.Data
Partial Class ViewVasReport
    Inherits BasePage
    Protected Sub ShowActDetails()
        Dim strDate As String
        Dim strCategory As String = String.Empty
        Dim InpDate As Date
        strDate = DLMonth.SelectedValue & "/1/" & DLYear.SelectedValue
        InpDate = Date.Parse(strDate)
        Dim C1SDate As Date
        Dim C1EDate As Date
        Dim C2SDate As Date
        Dim C2EDate As Date
        C1SDate = Month(InpDate) & "/1/" & Year(InpDate)
        C2SDate = Month(InpDate) & "/16/" & Year(InpDate)
        C1EDate = Month(InpDate) & "/16/" & Year(InpDate)
        C2EDate = DateAdd(DateInterval.Day, -1, DateAdd(DateInterval.Month, 1, C1SDate))
        Dim t2 As New Table
        t2.Style("width") = "100%"
        t2.BorderWidth = 2
        t2.GridLines = GridLines.Both
        Dim I As Integer = 0
        Dim InvBillAmount As Double = "0.0"
        Dim DLines As String = 0
        Dim DSLines As String = 0
        Dim BillUnits As String = 0
        Dim BillSUnits As String = 0
        Dim BillAmt As String = 0
        Dim BillOtherAmt As String = 0
        Dim BillTotAmount As Double = 0
        Dim date1 As Date
        Dim STSTDLines As String = 0
        Dim STSTDSLines As String = 0
        Dim STBillUnits As String = 0
        Dim STBillSUnits As String = 0
        Dim STBillAmt As String = 0
        Dim STBillOtherAmt As String = 0
        Dim STBillTotAmount As String = 0
        Dim minbilling As Double = "0.00"
        Dim RemBalance As Double = "0.00"
        Dim TSTDLines As String = 0
        Dim TSTDSLines As String = 0
        Dim TBillUnits As String = 0
        Dim TBillSUnits As String = 0
        Dim TBillAmt As String = 0
        Dim TBillOtherAmt As String = 0
        Dim TBillTotAmount As String = 0
        Dim objInvVas As New ETS.BL.InvoiceItemDetails
        Dim DTSet As DataSet = objInvVas.getInvoiceVasDetailsByAccuntID(Session("ContractorID").ToString, DLAct.SelectedValue, C1SDate, C2EDate)
        If DTSet.Tables.Count > 0 Then
            For Each DRRec As Data.DataRow In DTSet.Tables(0).Rows
                Dim ARow1 As New TableRow
                Dim ACell1 As New TableCell
                Dim ACell2 As New TableCell
                Dim ACell3 As New TableCell
                Dim ACell4 As New TableCell
                Dim ACell5 As New TableCell
                Dim ACell6 As New TableCell
                Dim ACell7 As New TableCell
                Dim ACell8 As New TableCell
                Dim ACell9 As New TableCell
                Dim ACell10 As New TableCell
                Dim ACell11 As New TableCell
                Dim ACell12 As New TableCell
                Dim ACell13 As New TableCell
                Dim ACell14 As New TableCell
                Dim ACell15 As New TableCell
                Dim ACell16 As New TableCell
                Dim ACell17 As New TableCell
                Dim ACell18 As New TableCell
                ARow1.HorizontalAlign = HorizontalAlign.Center
                ACell1.HorizontalAlign = HorizontalAlign.Left
                ACell2.HorizontalAlign = HorizontalAlign.Left
                ACell4.HorizontalAlign = HorizontalAlign.Right
                ACell5.HorizontalAlign = HorizontalAlign.Right
                ACell6.HorizontalAlign = HorizontalAlign.Right
                ACell1.Text = DRRec("AccountName").ToString
                ACell2.Text = DRRec("Descr").ToString
                If IsDate(DRRec("ServiceDate").ToString) Then
                    date1 = DRRec("ServiceDate").ToString
                    ACell3.Text = date1.ToString("MM/dd/yyyy")
                Else
                    ACell3.Text = DRRec("ServiceDate").ToString
                End If

                ACell4.Text = DRRec("Quantity").ToString
                ACell5.Text = FormatNumber(DRRec("Amount").ToString, 3)
                ACell6.Text = FormatNumber(DRRec("totAmount").ToString, 3)
                BillTotAmount = BillTotAmount + DRRec("totAmount").ToString
                If DRRec("invoiceid").ToString = "11111111-1111-1111-1111-111111111111" Then
                    ACell7.Text = "Not Posted"
                    ACell8.Text = "<a href=remdetails.aspx?autoid='" & DRRec("autoid").ToString & "'>Remove</a> "
                Else
                    ACell7.Font.Bold = True
                    ACell7.Text = "Posted"
                    ACell8.Text = ""
                End If
                'ACell1.CssClass = "gridalt2"
                'ACell2.CssClass = "gridalt2"
                'ACell3.CssClass = "gridalt2"
                'ACell4.CssClass = "gridalt2"
                'ACell5.CssClass = "gridalt2"
                'ACell6.CssClass = "gridalt2"
                'ACell7.CssClass = "gridalt2"
                'ACell8.CssClass = "gridalt2"
                ARow1.Cells.Add(ACell1)
                ARow1.Cells.Add(ACell2)
                ARow1.Cells.Add(ACell3)
                ARow1.Cells.Add(ACell4)
                ARow1.Cells.Add(ACell5)
                ARow1.Cells.Add(ACell6)
                ARow1.Cells.Add(ACell7)
                ARow1.Cells.Add(ACell8)
                ARow1.CssClass = "gridalt2"
                tblMins.Rows.Add(ARow1)
            Next
            Dim Cell1 As New TableCell
            Dim cell2 As New TableCell
            Dim cell3 As New TableCell
            Cell1.ColumnSpan = 5
            cell2.HorizontalAlign = HorizontalAlign.Right
            Cell1.HorizontalAlign = HorizontalAlign.Right
            Cell1.Text = "Total Amount"
            cell2.Text = FormatNumber(BillTotAmount, 3)
            cell3.ColumnSpan = 2
            Dim Row1 As New TableRow
            Row1.CssClass = "tblbg"
            Row1.Cells.Add(Cell1)
            Row1.Cells.Add(cell2)
            Row1.Cells.Add(cell3)
            tblMins.Rows.Add(Row1)
        End If
    End Sub
    Protected Sub tblsubmit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles tblsubmit.Click
        ShowActDetails()
    End Sub
    Function WorkingDays(ByVal StartDate As Date, ByVal EndDate As Date) As Integer
        Dim intCount As Integer
        intCount = 0
        Do While StartDate <= EndDate
            Select Case Weekday(StartDate)
                Case "1"
                    intCount = intCount
                Case "7"
                    intCount = intCount
                Case "2"
                    intCount = intCount + 1
                Case "3"
                    intCount = intCount + 1
                Case "4"
                    intCount = intCount + 1
                Case "5"
                    intCount = intCount + 1
                Case "6"
                    intCount = intCount + 1
            End Select
            StartDate = DateAdd(DateInterval.Day, 1, StartDate)
        Loop
        If intCount = 0 Then
            intCount = 1
        End If
        WorkingDays = intCount
    End Function
    Protected Overrides Sub Finalize()
        MyBase.Finalize()
    End Sub
  
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then

            Dim ObjAct As New ETS.BL.Accounts
            Dim DTSet As DataSet = ObjAct.getAccountList
            If DTSet.Tables.Count > 0 Then
                Dim DTView As New System.Data.DataView
                DTView = New System.Data.DataView(DTSet.Tables(0), String.Empty, "AccountName", System.Data.DataViewRowState.CurrentRows)
                DLAct.DataSource = DTView
                ' DLAct.DataSource = DTSet.Tables(0)
                DLAct.DataTextField = "AccountName"
                DLAct.DataValueField = "AccountID"
                DLAct.DataBind()
            End If
            Dim LI1 As New ListItem
            LI1.Text = "Any"
            LI1.Value = ""
            LI1.Selected = True
            DLAct.Items.Add(LI1)
            GetMyMonthList(DLMonth, True)
            GetMyYearList(DLYear, True)
            If Request("ShowAct") = "Yes" Then
                DLMonth.SelectedIndex = DLMonth.Items.IndexOf(DLMonth.Items.FindByValue(Request("DLMonth1")))
                DLYear.SelectedIndex = DLYear.Items.IndexOf(DLYear.Items.FindByValue(Request("DLYear1")))
                ShowActDetails()
            Else
                DLMonth.SelectedIndex = DLMonth.Items.IndexOf(DLMonth.Items.FindByValue(Now.Month))
                DLYear.SelectedIndex = DLYear.Items.IndexOf(DLYear.Items.FindByValue(Now.Year))
            End If
        End If
    End Sub
End Class
