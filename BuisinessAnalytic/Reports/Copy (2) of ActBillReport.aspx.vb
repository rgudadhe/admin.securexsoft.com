Imports System.Data.Sqlclient
Imports System.Data
Partial Class MIS_DailyMins
    Inherits BasePage

    Protected Sub ShowIndActDetails(ByVal AccID As String, ByVal BillMonth As String, ByVal BillYear As String, ByVal BillCycle As String)
        Dim strConn As String
        Dim strCategory As String
        strCategory = ""
        Dim t2 As New Table
        t2.Style("width") = "100%"
        t2.BorderWidth = 2
        t2.GridLines = GridLines.Both
        Dim I As Integer
        I = 0
        strConn = System.Configuration.ConfigurationManager.AppSettings("ETSCon")
        Dim strQuery As String
        Dim DLines As String
        Dim DSLines As String
        Dim BillUnits As String
        Dim BillSUnits As String
        Dim BillAmt As String
        Dim BillOtherAmt As String
        Dim BillTotAmount As String
        DLines = 0
        DSLines = 0
        BillUnits = 0
        BillSUnits = 0
        BillAmt = 0
        BillOtherAmt = 0
        BillTotAmount = 0
        Dim STSTDLines As String
        Dim STSTDSLines As String
        Dim STBillUnits As String
        Dim STBillSUnits As String
        Dim STBillAmt As String
        Dim STBillOtherAmt As String
        Dim STBillTotAmount As String
        STSTDLines = 0
        STSTDSLines = 0
        STBillUnits = 0
        STBillSUnits = 0
        STBillAmt = 0
        STBillOtherAmt = 0
        STBillTotAmount = 0
        Dim C1SDate As Date
        Dim C1EDate As Date
        Dim C2SDate As Date
        Dim C2EDate As Date
        Dim TSTDLines As String
        Dim TSTDSLines As String
        Dim TBillUnits As String
        Dim TBillSUnits As String
        Dim TBillAmt As String
        Dim TBillOtherAmt As String
        Dim TBillTotAmount As String
        TSTDLines = 0
        TSTDSLines = 0
        TBillUnits = 0
        TBillSUnits = 0
        TBillAmt = 0
        TBillOtherAmt = 0
        TBillTotAmount = 0
        Dim SubActName As String
        Dim DvRate As String
        Dim MiscRate As String
        Dim StatRate As String
        Dim Mode As String
        Dim ActName As String
        Dim NumDict As Integer
     
        Dim BoolCycle As Boolean
        Dim Billdate As Date
        Dim BillAmount As Double
        Dim AccountID As String
        BillAmount = "0.00"

        strQuery = "Select A.Accountid, BA.Mode, A.AccountName as Description, A.BillActNumber, C.priority, C.Description as CateDescr, BA.MinBilling, BA.BillAccID, BA.Posted, BA.BillMonth, BA.BillYear, BA.BillCycle, BA.Cycle, BA.InvoiceID "
        strQuery = strQuery & " from tblaccounts A "
        strQuery = strQuery & " LEFT OUTER JOIN (Select * from AdminSecureweb.dbo.tblBillAccounts) BA"
        strQuery = strQuery & " on BA.AccountID = A.AccountID"
        strQuery = strQuery & " LEFT OUTER JOIN (Select * from tblActCategories) C"
        strQuery = strQuery & " on C.category = A.Category  where A.AccountID = '" & AccID & "' "
        'Response.Write(strQuery)
        Dim SQLCmd As New SqlCommand(strQuery, New SqlConnection(strConn))
        Try
            SQLCmd.Connection.Open()
            Dim DRRec As SqlDataReader = SQLCmd.ExecuteReader()
            If DRRec.HasRows Then
                If DRRec.Read Then

                    tblDtls.Text = "Invoice Summary - " & DRRec("Description").ToString

                    AccountID = DRRec("AccountID").ToString

                    C1SDate = BillMonth & "/1/" & BillYear
                    C2SDate = BillMonth & "/16/" & BillYear
                    C1EDate = BillMonth & "/16/" & BillYear
                    C2EDate = DateAdd(DateInterval.Day, -1, DateAdd(DateInterval.Month, 1, C1SDate))
                    Billdate = DateAdd(DateInterval.Month, 1, C1SDate)

                    Mode = Trim(DRRec("Mode").ToString)
                    ActName = DRRec("Description").ToString
                    strQuery = "Select  IsNULL(MTLines, 0) as MTLines , IsNULL(MTRate, 0.00) as MTRate, IsNULL(MTPLines, 0) as MTPLines, IsNULL(MTPRate, 0.00) as MTPRate, IsNULL(QALines, 0) as QALines, IsNULL(QARate, 0.00) as QARate,  IsNULL(MTSLines, 0) as MTSLines , IsNULL(MTSRate, 0.00) as MTSRate, IsNULL(MTPSLines, 0) as MTPSLines, IsNULL(MTPSRate, 0.00) as MTPSRate, IsNULL(QASLines, 0) as QASLines, IsNULL(QASRate, 0.00) as QASRate, IsNULL(STATLines, 0) as STATLines, IsNULL(CPL, 0) as CPL from AdminSecureweb.dbo.tblInDirActDetails where AccountID = '" & DRRec("AccountID").ToString & "' and BillMonth='" & BillMonth & "' and BillYear ='" & BillYear & "' and BillCycle='" & BillCycle & "' "



                    Dim SQLCmdI As New SqlCommand(strQuery, New SqlConnection(strConn))
                    Try
                        SQLCmdI.Connection.Open()
                        Dim DRRecI As SqlDataReader = SQLCmdI.ExecuteReader()
                        If DRRecI.HasRows Then
                            If DRRecI.Read Then
                                For I = 1 To 6
                                    If I = 1 Then
                                        If CInt(DRRecI("MTLines").ToString) > 0 Then
                                            Dim vRow1 As New TableRow
                                            Dim vCell1 As New TableCell
                                            Dim vCell2 As New TableCell
                                            Dim vCell3 As New TableCell
                                            Dim vCell4 As New TableCell
                                            Dim vCell5 As New TableCell
                                            Dim vCell6 As New TableCell
                                            vCell1.Text = Billdate.ToShortDateString
                                            vCell2.Text = "MT-B"
                                            vCell3.Text = "Previous Period Activity MT Lines"
                                            vCell4.HorizontalAlign = HorizontalAlign.Right
                                            vCell5.HorizontalAlign = HorizontalAlign.Right
                                            vCell6.HorizontalAlign = HorizontalAlign.Right
                                            vCell4.Text = CInt(DRRecI("MTLines").ToString)
                                            vCell5.Text = "$ " & FormatNumber(DRRecI("MTRate").ToString, 6)
                                            BillAmount = DRRecI("MTLines").ToString * DRRecI("MTRate").ToString
                                            vCell6.Text = "$ " & FormatNumber(BillAmount, 2)
                                            vRow1.CssClass = "tblbg2"
                                            vRow1.Cells.Add(vCell1)
                                            vRow1.Cells.Add(vCell2)
                                            vRow1.Cells.Add(vCell3)
                                            vRow1.Cells.Add(vCell4)
                                            vRow1.Cells.Add(vCell5)
                                            vRow1.Cells.Add(vCell6)
                                            tblMins.Rows.Add(vRow1)
                                            BillTotAmount += BillAmount
                                        End If
                                    ElseIf I = 2 Then
                                        If CInt(DRRecI("MTPLines").ToString) > 0 Then
                                            Dim vRow1 As New TableRow
                                            Dim vCell1 As New TableCell
                                            Dim vCell2 As New TableCell
                                            Dim vCell3 As New TableCell
                                            Dim vCell4 As New TableCell
                                            Dim vCell5 As New TableCell
                                            Dim vCell6 As New TableCell
                                            vCell1.Text = Billdate.ToShortDateString
                                            vCell2.Text = "MT-B"
                                            vCell3.Text = "Previous Period Activity MT Plus Lines"
                                            vCell4.HorizontalAlign = HorizontalAlign.Right
                                            vCell5.HorizontalAlign = HorizontalAlign.Right
                                            vCell6.HorizontalAlign = HorizontalAlign.Right
                                            vCell4.Text = CInt(DRRecI("MTPLines").ToString)
                                            vCell5.Text = "$ " & FormatNumber(DRRecI("MTPRate").ToString, 6)
                                            BillAmount = DRRecI("MTPLines").ToString * DRRecI("MTPRate").ToString
                                            vCell6.Text = "$ " & FormatNumber(BillAmount, 2)
                                            vRow1.CssClass = "tblbg2"
                                            vRow1.Cells.Add(vCell1)
                                            vRow1.Cells.Add(vCell2)
                                            vRow1.Cells.Add(vCell3)
                                            vRow1.Cells.Add(vCell4)
                                            vRow1.Cells.Add(vCell5)
                                            vRow1.Cells.Add(vCell6)
                                            tblMins.Rows.Add(vRow1)
                                            BillTotAmount += BillAmount
                                        End If
                                    ElseIf I = 3 Then
                                        If CInt(DRRecI("QALines").ToString) > 0 Then
                                            Dim vRow1 As New TableRow
                                            Dim vCell1 As New TableCell
                                            Dim vCell2 As New TableCell
                                            Dim vCell3 As New TableCell
                                            Dim vCell4 As New TableCell
                                            Dim vCell5 As New TableCell
                                            Dim vCell6 As New TableCell
                                            vCell1.Text = Billdate.ToShortDateString
                                            vCell2.Text = "MT-B"
                                            vCell3.Text = "Previous Period Activity QA Lines"
                                            vCell4.HorizontalAlign = HorizontalAlign.Right
                                            vCell5.HorizontalAlign = HorizontalAlign.Right
                                            vCell6.HorizontalAlign = HorizontalAlign.Right
                                            vCell4.Text = CInt(DRRecI("QALines").ToString)
                                            vCell5.Text = "$ " & FormatNumber(DRRecI("QARate").ToString, 6)
                                            BillAmount = DRRecI("QALines").ToString * DRRecI("QARate").ToString
                                            vCell6.Text = "$ " & FormatNumber(BillAmount, 2)
                                            vRow1.CssClass = "tblbg2"
                                            vRow1.Cells.Add(vCell1)
                                            vRow1.Cells.Add(vCell2)
                                            vRow1.Cells.Add(vCell3)
                                            vRow1.Cells.Add(vCell4)
                                            vRow1.Cells.Add(vCell5)
                                            vRow1.Cells.Add(vCell6)
                                            tblMins.Rows.Add(vRow1)
                                            BillTotAmount += BillAmount
                                        End If
                                    ElseIf I = 4 Then
                                        If CInt(DRRecI("MTSLines").ToString) > 0 Then
                                            Dim vRow1 As New TableRow
                                            Dim vCell1 As New TableCell
                                            Dim vCell2 As New TableCell
                                            Dim vCell3 As New TableCell
                                            Dim vCell4 As New TableCell
                                            Dim vCell5 As New TableCell
                                            Dim vCell6 As New TableCell
                                            vCell1.Text = Billdate.ToShortDateString
                                            vCell2.Text = "MT-B"
                                            vCell3.Text = "Previous Period Activity MT STAT Lines"
                                            vCell4.HorizontalAlign = HorizontalAlign.Right
                                            vCell5.HorizontalAlign = HorizontalAlign.Right
                                            vCell6.HorizontalAlign = HorizontalAlign.Right
                                            vCell4.Text = CInt(DRRecI("MTSLines").ToString)
                                            vCell5.Text = "$ " & FormatNumber(DRRecI("MTSRate").ToString, 6)
                                            BillAmount = DRRecI("MTSLines").ToString * DRRecI("MTSRate").ToString
                                            vCell6.Text = "$ " & FormatNumber(BillAmount, 2)
                                            vRow1.CssClass = "tblbg2"
                                            vRow1.Cells.Add(vCell1)
                                            vRow1.Cells.Add(vCell2)
                                            vRow1.Cells.Add(vCell3)
                                            vRow1.Cells.Add(vCell4)
                                            vRow1.Cells.Add(vCell5)
                                            vRow1.Cells.Add(vCell6)
                                            tblMins.Rows.Add(vRow1)
                                            BillTotAmount += BillAmount
                                        End If
                                    ElseIf I = 5 Then
                                        If CInt(DRRecI("MTPSLines").ToString) > 0 Then
                                            Dim vRow1 As New TableRow
                                            Dim vCell1 As New TableCell
                                            Dim vCell2 As New TableCell
                                            Dim vCell3 As New TableCell
                                            Dim vCell4 As New TableCell
                                            Dim vCell5 As New TableCell
                                            Dim vCell6 As New TableCell
                                            vCell1.Text = Billdate.ToShortDateString
                                            vCell2.Text = "MT-B"
                                            vCell3.Text = "Previous Period Activity MT Plus STAT Lines"
                                            vCell4.HorizontalAlign = HorizontalAlign.Right
                                            vCell5.HorizontalAlign = HorizontalAlign.Right
                                            vCell6.HorizontalAlign = HorizontalAlign.Right
                                            vCell4.Text = CInt(DRRecI("MTPSLines").ToString)
                                            vCell5.Text = "$ " & FormatNumber(DRRecI("MTPSRate").ToString, 6)
                                            BillAmount = DRRecI("MTPSLines").ToString * DRRecI("MTPSRate").ToString
                                            vCell6.Text = "$ " & FormatNumber(BillAmount, 2)
                                            vRow1.CssClass = "tblbg2"
                                            vRow1.Cells.Add(vCell1)
                                            vRow1.Cells.Add(vCell2)
                                            vRow1.Cells.Add(vCell3)
                                            vRow1.Cells.Add(vCell4)
                                            vRow1.Cells.Add(vCell5)
                                            vRow1.Cells.Add(vCell6)
                                            tblMins.Rows.Add(vRow1)
                                            BillTotAmount += BillAmount
                                        End If
                                    ElseIf I = 6 Then
                                        If CInt(DRRecI("QASLines").ToString) > 0 Then
                                            Dim vRow1 As New TableRow
                                            Dim vCell1 As New TableCell
                                            Dim vCell2 As New TableCell
                                            Dim vCell3 As New TableCell
                                            Dim vCell4 As New TableCell
                                            Dim vCell5 As New TableCell
                                            Dim vCell6 As New TableCell
                                            vCell1.Text = Billdate.ToShortDateString
                                            vCell2.Text = "MT-B"
                                            vCell3.Text = "Previous Period Activity QA STAT Lines"
                                            vCell4.HorizontalAlign = HorizontalAlign.Right
                                            vCell5.HorizontalAlign = HorizontalAlign.Right
                                            vCell6.HorizontalAlign = HorizontalAlign.Right
                                            vCell4.Text = CInt(DRRecI("QASLines").ToString)
                                            vCell5.Text = "$ " & FormatNumber(DRRecI("QASRate").ToString, 6)
                                            BillAmount = DRRecI("QASLines").ToString * DRRecI("QASRate").ToString
                                            vCell6.Text = "$ " & FormatNumber(BillAmount, 2)
                                            vRow1.CssClass = "tblbg2"
                                            vRow1.Cells.Add(vCell1)
                                            vRow1.Cells.Add(vCell2)
                                            vRow1.Cells.Add(vCell3)
                                            vRow1.Cells.Add(vCell4)
                                            vRow1.Cells.Add(vCell5)
                                            vRow1.Cells.Add(vCell6)
                                            tblMins.Rows.Add(vRow1)
                                            BillTotAmount += BillAmount
                                        End If
                                    End If


                                Next

                            End If
                        End If

                    Finally
                        If SQLCmdI.Connection.State = System.Data.ConnectionState.Open Then
                            SQLCmdI.Connection.Close()
                            SQLCmdI = Nothing
                        End If
                    End Try

                    strQuery = "Select IT.Mode, ID.Item, IT.Descr, IT.Quantity, IT.Amount, IT.Totamount, IT.ServiceDate  from AdminSecureweb.dbo.tblInvItemDet IT Left Outer Join AdminSecureweb.dbo.ItemDetails ID ON IT.ItemID = ID.ItemID where IT.Servicedate between  '" & C1SDate & "' and '" & C2EDate & "' and AccountID='" & AccountID & "' and IT.Mode='VAS' "
                    'Response.Write(strQuery)

                    Dim SQLCmd3 As New SqlCommand(strQuery, New SqlConnection(strConn))
                    Try
                        SQLCmd3.Connection.Open()
                        Dim DRRec3 As SqlDataReader = SQLCmd3.ExecuteReader()
                        If DRRec3.HasRows Then
                            While DRRec3.Read
                                Dim vRow1 As New TableRow
                                Dim vCell1 As New TableCell
                                Dim vCell2 As New TableCell
                                Dim vCell3 As New TableCell
                                Dim vCell4 As New TableCell
                                Dim vCell5 As New TableCell
                                Dim vCell6 As New TableCell
                                Billdate = DRRec3("ServiceDate").ToString
                                vCell1.Text = Billdate.ToShortDateString
                                vCell2.Text = DRRec3("Item").ToString
                                vCell3.Text = DRRec3("Descr").ToString
                                vCell4.HorizontalAlign = HorizontalAlign.Right
                                vCell5.HorizontalAlign = HorizontalAlign.Right
                                vCell6.HorizontalAlign = HorizontalAlign.Right
                                vCell4.Text = DRRec3("Quantity").ToString
                                vCell5.Text = "$ " & FormatNumber(DRRec3("Amount").ToString, 6)
                                vCell6.Text = "$ " & FormatNumber(DRRec3("Totamount").ToString, 2)
                                BillAmount = CDbl(FormatNumber(DRRec3("Totamount").ToString, 2))
                                vRow1.CssClass = "tblbg2"
                                vRow1.Cells.Add(vCell1)
                                vRow1.Cells.Add(vCell2)
                                vRow1.Cells.Add(vCell3)
                                vRow1.Cells.Add(vCell4)
                                vRow1.Cells.Add(vCell5)
                                vRow1.Cells.Add(vCell6)
                                tblMins.Rows.Add(vRow1)
                                BillTotAmount += BillAmount
                            End While
                        End If
                        DRRec.Close()
                    Finally
                        If SQLCmd3.Connection.State = System.Data.ConnectionState.Open Then
                            SQLCmd3.Connection.Close()
                            SQLCmd3 = Nothing
                        End If
                    End Try



                    Dim A11Row1 As New TableRow
                    Dim A11Cell1 As New TableCell
                    Dim A11Cell2 As New TableCell
                    Dim A11Cell3 As New TableCell
                    Dim A11Cell4 As New TableCell
                    Dim A11Cell5 As New TableCell
                    Dim A11Cell6 As New TableCell
                    Dim A11Cell7 As New TableCell
                    Dim A11Cell8 As New TableCell
                    Dim A11Cell9 As New TableCell


                    A11Row1.HorizontalAlign = HorizontalAlign.Center
                    A11Row1.CssClass = "tblbg2"
                    A11Row1.Font.Bold = True
                    A11Cell1.ColumnSpan = 4
                    A11Cell1.HorizontalAlign = HorizontalAlign.Right
                    A11Cell1.Text = ""
                    A11Cell2.Text = "Total"
                    A11Cell3.Text = "$ " & FormatNumber(BillTotAmount, 2)
                    A11Row1.Cells.Add(A11Cell1)
                    A11Row1.Cells.Add(A11Cell2)
                    A11Row1.Cells.Add(A11Cell3)
                    tblMins.Rows.Add(A11Row1)
                End If
            End If
        Finally
            If SQLCmd.Connection.State = System.Data.ConnectionState.Open Then
                SQLCmd.Connection.Close()
                SQLCmd = Nothing
            End If
        End Try
    End Sub

    Protected Sub ShowActDetails(ByVal BillAccID As String)
        Dim strConn As String
        Dim strCategory As String
        strCategory = ""
        Dim t2 As New Table
        t2.Style("width") = "100%"
        t2.BorderWidth = 2
        t2.GridLines = GridLines.Both
        Dim I As Integer
        I = 0
        strConn = System.Configuration.ConfigurationManager.AppSettings("ETSCon")
        Dim strQuery As String
        Dim DLines As String
        Dim DSLines As String
        Dim BillUnits As String
        Dim BillSUnits As String
        Dim BillAmt As String
        Dim BillOtherAmt As String
        Dim BillTotAmount As String
        DLines = 0
        DSLines = 0
        BillUnits = 0
        BillSUnits = 0
        BillAmt = 0
        BillOtherAmt = 0
        BillTotAmount = 0
        Dim STSTDLines As String
        Dim STSTDSLines As String
        Dim STBillUnits As String
        Dim STBillSUnits As String
        Dim STBillAmt As String
        Dim STBillOtherAmt As String
        Dim STBillTotAmount As String
        STSTDLines = 0
        STSTDSLines = 0
        STBillUnits = 0
        STBillSUnits = 0
        STBillAmt = 0
        STBillOtherAmt = 0
        STBillTotAmount = 0
        Dim C1SDate As Date
        Dim C1EDate As Date
        Dim C2SDate As Date
        Dim C2EDate As Date
        Dim TSTDLines As String
        Dim TSTDSLines As String
        Dim TBillUnits As String
        Dim TBillSUnits As String
        Dim TBillAmt As String
        Dim TBillOtherAmt As String
        Dim TBillTotAmount As String
        TSTDLines = 0
        TSTDSLines = 0
        TBillUnits = 0
        TBillSUnits = 0
        TBillAmt = 0
        TBillOtherAmt = 0
        TBillTotAmount = 0
        Dim SubActName As String
        Dim DvRate As String
        Dim MiscRate As String
        Dim StatRate As String
        Dim Mode As String
        Dim ActName As String
        Dim NumDict As Integer
        Dim BillMonth As Integer
        Dim BillYear As Integer
        Dim BillCycle As Integer
        Dim BoolCycle As Boolean
        Dim Billdate As Date
        Dim BillAmount As Double
        Dim AccountID As String
        Dim WType As String = String.Empty
        Dim strBillCycle As String
        Dim ActBillCycle As Boolean = False
        Dim Recupdate As Boolean = False
        Dim VasStartDate As Date
        Dim VasEndDate As Date
        Dim WTMode As Boolean = False
        BillAmount = "0.00"

        strQuery = "Select A.Accountid, BA.Mode,  A.AccountName as Description, A.BillActNumber, C.priority, C.Description as CateDescr, BA.MinBilling, BA.BillAccID, BA.Posted, BA.BillMonth, BA.BillYear, BA.BillCycle, BA.Cycle, BA.InvoiceID, BA.WTMode "
        strQuery = strQuery & " from tblaccounts A "
        strQuery = strQuery & " INNER JOIN (Select * from AdminSecureweb.dbo.tblBillAccounts) BA"
        strQuery = strQuery & " on BA.AccountID = A.AccountID"
        strQuery = strQuery & " LEFT OUTER JOIN (Select * from tblActCategories) C"
        strQuery = strQuery & " on C.category = A.Category  where BA.BillAccID = '" & BillAccID & "' "
        'Response.Write(strQuery)
        Dim SQLCmd As New SqlCommand(strQuery, New SqlConnection(strConn))
        Try
            SQLCmd.Connection.Open()
            Dim DRRec As SqlDataReader = SQLCmd.ExecuteReader()
            If DRRec.HasRows Then
                If DRRec.Read Then
                    If DRRec("Posted").ToString = "True" Then
                        tblDtls.Text = "Invoice Summary - " & DRRec("Description").ToString
                        strQuery = "Select IT.AutoID, IT.Mode, ID.Item, IT.Descr, IT.Quantity, IT.Amount, IT.Totamount, IT.ServiceDate, IT.WTMode, IT.WorkType  from AdminSecureweb.dbo.tblInvItemDet IT Left Outer Join AdminSecureweb.dbo.ItemDetails ID ON IT.ItemID = ID.ItemID where InvoiceID='" & DRRec("InvoiceID").ToString & "' "
                        'Response.Write(strQuery)
                        Dim SQLCmd3 As New SqlCommand(strQuery, New SqlConnection(strConn))
                        Try
                            SQLCmd3.Connection.Open()
                            Dim DRRec3 As SqlDataReader = SQLCmd3.ExecuteReader()
                            If DRRec3.HasRows Then
                                While DRRec3.Read
                                    Dim vRow1 As New TableRow
                                    Dim vCell1 As New TableCell
                                    Dim vCell2 As New TableCell
                                    Dim vCell3 As New TableCell
                                    Dim vCell4 As New TableCell
                                    Dim vCell5 As New TableCell
                                    Dim vCell6 As New TableCell
                                    Billdate = DRRec3("ServiceDate").ToString
                                    vCell1.Text = Billdate.ToShortDateString
                                    vCell2.Text = DRRec3("Item").ToString
                                    vCell3.Text = "<a href='ActBillReport.aspx?PostDictDetails=True&BillAccID=" & BillAccID & "&InvItemID=" & DRRec3("AutoID").ToString & "&activity=" & DRRec3("Descr").ToString & "&service=" & DRRec3("item").ToString & "&WTMode=" & DRRec3("WTMode").ToString & "&WType=" & Trim(DRRec3("WorkType").ToString) & "'>" & DRRec3("Descr").ToString & "</a>"
                                    vCell4.HorizontalAlign = HorizontalAlign.Right
                                    vCell5.HorizontalAlign = HorizontalAlign.Right
                                    vCell6.HorizontalAlign = HorizontalAlign.Right
                                    vCell4.Text = DRRec3("Quantity").ToString
                                    vCell5.Text = "$ " & FormatNumber(DRRec3("Amount").ToString, 6)
                                    vCell6.Text = "$ " & FormatNumber(DRRec3("Totamount").ToString, 2)
                                    BillAmount = CDbl(FormatNumber(DRRec3("Totamount").ToString, 2))
                                    vRow1.CssClass = "tblbg2"
                                    vRow1.Cells.Add(vCell1)
                                    vRow1.Cells.Add(vCell2)
                                    vRow1.Cells.Add(vCell3)
                                    vRow1.Cells.Add(vCell4)
                                    vRow1.Cells.Add(vCell5)
                                    vRow1.Cells.Add(vCell6)
                                    tblMins.Rows.Add(vRow1)
                                    BillTotAmount += BillAmount
                                End While
                            End If
                            DRRec3.Close()
                        Finally
                            If SQLCmd3.Connection.State = System.Data.ConnectionState.Open Then
                                SQLCmd3.Connection.Close()
                                SQLCmd3 = Nothing
                            End If
                        End Try
                    Else
                        If DRRec("WTMode").ToString.ToLower = "true" Then
                            WTMode = True
                        Else
                            WTMode = False
                        End If
                        Recupdate = False
                        AccountID = DRRec("AccountID").ToString
                        BillMonth = DRRec("BillMonth")
                        BillYear = DRRec("BillYear")
                        BillCycle = DRRec("BillCycle")
                        BoolCycle = DRRec("Cycle").ToString
                        If DRRec("Cycle").ToString = "True" Then
                            ActBillCycle = True
                        Else
                            ActBillCycle = False
                        End If
                        If DRRec("billCycle").ToString = "1" Then
                            strBillCycle = "Cycle1"
                        Else
                            strBillCycle = "Cycle2"
                        End If
                        C1SDate = BillMonth & "/1/" & BillYear
                        C2SDate = BillMonth & "/16/" & BillYear
                        C1EDate = BillMonth & "/16/" & BillYear
                        C2EDate = DateAdd(DateInterval.Day, -1, DateAdd(DateInterval.Month, 1, C1SDate))
                        'Billdate = DateAdd(DateInterval.Month, 1, C1SDate)
                        If BillCycle = "2" Then
                            Billdate = DateAdd(DateInterval.Month, 1, C1SDate)
                        Else
                            Billdate = C1EDate
                        End If

                        Mode = Trim(DRRec("Mode").ToString)
                        ActName = DRRec("Description").ToString
                        If Mode = "DC" Or Mode = "LC" Or Mode = "TT" Or Mode = "DV" Or Mode = "TW" Then
                            strQuery = "Select  B.SubActID, B.SubActName,  B.Rate, B.MiscRate, B.StatRate, L.MethodName, isnull(T1.Lines,0) AS BillUnits, isnull(T2.Lines,0) AS BillSUnits,  isnull(T1.stdLines,0) AS stdLines, isnull(T2.stdLines,0) AS stdSLines, isnull(T1.Amt,0) AS BillAmt    "
                            strQuery = strQuery & " from AdminSecureweb.dbo.tblAccBillDetails B"
                            strQuery = strQuery & " LEFT OUTER JOIN (Select TrackID, MethodName from AdminETS.dbo.tblLCMethodology ) L"
                            strQuery = strQuery & " on L.TrackID = B.LCMethodID "
                            strQuery = strQuery & " INNER JOIN (Select BillAccID, SubActID, sum(unit) AS Lines, sum(amount) AS Amt, sum(stdLCCount) AS stdLines from tblBillingLines where BillAccID = '" & BillAccID & "'  and priority = 'False' Group by BillAccID, SubActID) T1"
                            strQuery = strQuery & " on T1.BillAccID = B.BillAccID and T1.SubActID = B.SubActID  "
                            strQuery = strQuery & " LEFT OUTER JOIN (Select BillAccID, SubActID, sum(unit) AS Lines, sum(amount) AS Amt, sum(stdLCCount) AS stdLines from tblBillingLines where BillAccID = '" & BillAccID & "'  and priority='True' Group by BillAccID, SubActID) T2"
                            strQuery = strQuery & " on T2.BillAccID = B.BillAccID and T2.SubActID = B.SubActID  where B.BillAccID = '" & BillAccID & "'"
                        Else
                            If WTMode = True Then
                                strQuery = "Select  T1.WorkType, T1.Rate as WTRate, B.SubActID, B.SubActName,  B.Rate, B.MiscRate, B.StatRate, L.MethodName, isnull(T1.Lines,0) AS BillUnits, isnull(T2.Lines,0) AS BillSUnits,  isnull(T1.stdLines,0) AS stdLines, isnull(T2.stdLines,0) AS stdSLines, isnull(T1.Amt,0) AS BillAmt    "
                                strQuery = strQuery & " from AdminSecureweb.dbo.tblAccBillDetails B"
                                strQuery = strQuery & " LEFT OUTER JOIN (Select TrackID, MethodName from AdminETS.dbo.tblLCMethodology ) L"
                                strQuery = strQuery & " on L.TrackID = B.LCMethodID "
                                strQuery = strQuery & " INNER JOIN (Select BillAccID, WorkType, Rate, sum(unit) AS Lines, sum(amount) AS Amt, sum(stdLCCount) AS stdLines from tblBillingLines where BillAccID = '" & BillAccID & "'  and priority = 'False' Group by BillAccID, WorkType, Rate) T1"
                                strQuery = strQuery & " on T1.BillAccID = B.BillAccID  "
                                strQuery = strQuery & " LEFT OUTER JOIN (Select BillAccID, sum(unit) AS Lines, sum(amount) AS Amt, sum(stdLCCount) AS stdLines from tblBillingLines where BillAccID = '" & BillAccID & "'  and priority='True' Group by BillAccID) T2"
                                strQuery = strQuery & " on T2.BillAccID = B.BillAccID  where B.BillAccID = '" & BillAccID & "'"
                            Else
                                strQuery = "Select  B.SubActID, B.SubActName,  B.Rate, B.MiscRate, B.StatRate, L.MethodName, isnull(T1.Lines,0) AS BillUnits, isnull(T2.Lines,0) AS BillSUnits,  isnull(T1.stdLines,0) AS stdLines, isnull(T2.stdLines,0) AS stdSLines, isnull(T1.Amt,0) AS BillAmt    "
                                strQuery = strQuery & " from AdminSecureweb.dbo.tblAccBillDetails B"
                                strQuery = strQuery & " LEFT OUTER JOIN (Select TrackID, MethodName from AdminETS.dbo.tblLCMethodology ) L"
                                strQuery = strQuery & " on L.TrackID = B.LCMethodID "
                                strQuery = strQuery & " INNER JOIN (Select BillAccID, sum(unit) AS Lines, sum(amount) AS Amt, sum(stdLCCount) AS stdLines from tblBillingLines where BillAccID = '" & BillAccID & "'  and priority = 'False' Group by BillAccID) T1"
                                strQuery = strQuery & " on T1.BillAccID = B.BillAccID  "
                                strQuery = strQuery & " LEFT OUTER JOIN (Select BillAccID, sum(unit) AS Lines, sum(amount) AS Amt, sum(stdLCCount) AS stdLines from tblBillingLines where BillAccID = '" & BillAccID & "'  and priority='True' Group by BillAccID) T2"
                                strQuery = strQuery & " on T2.BillAccID = B.BillAccID  where B.BillAccID = '" & BillAccID & "'"
                            End If


                        End If

                        ' Response.Write(strQuery)

                        Dim SQLCmd1 As New SqlCommand(strQuery, New SqlConnection(strConn))
                        Try
                            SQLCmd1.Connection.Open()
                            Dim DRRec1 As SqlDataReader = SQLCmd1.ExecuteReader()
                            If DRRec1.HasRows Then
                                While (DRRec1.Read)

                                    Dim Row1 As New TableRow
                                    Dim Cell1 As New TableCell
                                    Dim Cell2 As New TableCell
                                    Dim Cell3 As New TableCell
                                    Dim Cell4 As New TableCell
                                    Dim Cell5 As New TableCell
                                    Dim Cell6 As New TableCell
                                    Dim sRow1 As New TableRow
                                    Dim sCell1 As New TableCell
                                    Dim sCell2 As New TableCell
                                    Dim sCell3 As New TableCell
                                    Dim sCell4 As New TableCell
                                    Dim sCell5 As New TableCell
                                    Dim sCell6 As New TableCell


                                    SubActName = DRRec1("SubActName").ToString
                                    If WTMode = True Then
                                        DvRate = DRRec1("WTRate").ToString
                                    Else
                                        DvRate = DRRec1("Rate").ToString
                                    End If

                                    MiscRate = DRRec1("MiscRate").ToString
                                    StatRate = DRRec1("STATRate").ToString

                                    Row1.Font.Size = "8"



                                    'DLines += CInt(FormatNumber(DRRec1("stdLines"), 0))
                                    'DSLines += CInt(FormatNumber(DRRec1("stdSLines"), 0))
                                    BillUnits = CInt(FormatNumber(DRRec1("BillUnits"), 0))
                                    BillSUnits = CInt(FormatNumber(DRRec1("BillSUnits"), 0))
                                    'Response.Write(BillSUnits)
                                    'BillAmt += CDbl(FormatNumber(DRRec1("BillAmt"), 2))
                                    'Cell5.Text = CInt(FormatNumber(DRRec1("stdLines"), 0))
                                    'Cell6.Text = CInt(FormatNumber(DRRec1("stdSLines"), 0))
                                    'Cell7.Text = CInt(FormatNumber(DRRec1("BillUnits"), 0))
                                    'Cell8.Text = CInt(FormatNumber(DRRec1("BillSUnits"), 0))
                                    'Cell9.Text = CDbl(FormatNumber(DRRec1("BillAmt"), 2))
                                    If Mode = "S" Or Mode = "" Then
                                        If DRRec1("MethodName").ToString = "PerDictator" Then
                                            strQuery = "Select count(*) as NumDict from AdminSecureweb.dbo.AssignDic where GrpDicID = '" & DRRec1("SubActID").ToString & "'  "
                                            'strQuery = "Select count(*) as NumDict from (Select M.DictatorID as Numdict from AdminSecureweb.dbo.tblTranscriptionClientMain M INNER Join tblBillingLines T ON T.TranscriptionID = M.TranscriptionID where  T.BillAccID = '" & BillAccID & "' Group by M.dictatorID)T1 "
                                            Dim SQLCmd2 As New SqlCommand(strQuery, New SqlConnection(strConn))
                                            Try
                                                SQLCmd2.Connection.Open()
                                                Dim DRRec2 As SqlDataReader = SQLCmd2.ExecuteReader()
                                                If DRRec2.HasRows Then
                                                    If DRRec2.Read Then
                                                        'DLines += CInt(FormatNumber(DRRec1("stdLines"), 0))
                                                        'DSLines += CInt(FormatNumber(DRRec1("stdSLines"), 0))
                                                        'BillUnits += CInt(FormatNumber(DRRec1("BillUnits"), 0))
                                                        'BillSUnits += CInt(FormatNumber(DRRec1("BillSUnits"), 0))
                                                        'BillAmt += CDbl(FormatNumber(DRRec1("Rate") * DRRec2("NumDict"), 2))
                                                        ' Cell5.Text = CInt(FormatNumber(DRRec1("stdLines"), 0))
                                                        ' Cell6.Text = CInt(FormatNumber(DRRec1("stdSLines"), 0))

                                                        NumDict = CInt(DRRec2("NumDict"))
                                                        BillAmount = FormatNumber(NumDict * DvRate, 2)
                                                        Cell1.Text = Billdate
                                                        Cell2.Text = "MT-B"
                                                        Cell3.Text = "Previous Period Transcription Activity "
                                                        Cell4.HorizontalAlign = HorizontalAlign.Right
                                                        Cell5.HorizontalAlign = HorizontalAlign.Right
                                                        Cell6.HorizontalAlign = HorizontalAlign.Right
                                                        Cell4.Text = NumDict
                                                        Cell5.Text = "$ " & FormatNumber(DvRate, 6)
                                                        Cell6.Text = "$ " & FormatNumber(NumDict * DvRate, 2)
                                                        Row1.CssClass = "tblbg2"
                                                        Row1.Cells.Add(Cell1)
                                                        Row1.Cells.Add(Cell2)
                                                        Row1.Cells.Add(Cell3)
                                                        Row1.Cells.Add(Cell4)
                                                        Row1.Cells.Add(Cell5)
                                                        Row1.Cells.Add(Cell6)
                                                        If BillAmount > 0 Then
                                                            tblMins.Rows.Add(Row1)
                                                        End If


                                                    End If

                                                End If
                                            Finally
                                                If SQLCmd2.Connection.State = System.Data.ConnectionState.Open Then
                                                    SQLCmd2.Connection.Close()
                                                    SQLCmd2 = Nothing
                                                End If
                                            End Try
                                        Else
                                            BillAmount = FormatNumber(BillUnits * DvRate, 2)
                                            'Response.Write(BillUnits & "$" & DvRate)
                                            Cell1.Text = Billdate
                                            Cell2.Text = "MT-B"
                                            If WTMode = True Then
                                                'Response.Write(DRRec1("worktype").ToString)
                                                If Trim(DRRec1("worktype").ToString) = "C" Then
                                                    WType = "Consultation Note"
                                                ElseIf Trim(DRRec1("worktype").ToString) = "D" Then
                                                    WType = "Discharge Summary"
                                                ElseIf Trim(DRRec1("worktype").ToString) = "H" Then
                                                    WType = "Hystory and Physical"
                                                ElseIf Trim(DRRec1("worktype").ToString) = "I" Then
                                                    WType = "IME"
                                                ElseIf Trim(DRRec1("worktype").ToString) = "L" Then
                                                    WType = "Letter"
                                                ElseIf Trim(DRRec1("worktype").ToString) = "N" Then
                                                    WType = "Progress Note"
                                                ElseIf Trim(DRRec1("worktype").ToString) = "O" Then
                                                    WType = "Operative Note"
                                                ElseIf Trim(DRRec1("worktype").ToString) = "P" Then
                                                    WType = "Psych Eval"
                                                Else
                                                    WType = "Others"
                                                End If
                                                Cell3.Text = "<a href='ActBillReport.aspx?DictDetails=True&sBillAccID=" & BillAccID & "&sMode=" & Mode & "&WTMode=" & WTMode & "&WType=" & Trim(DRRec1("worktype").ToString) & "'>Previous Period Transcription Activity (Worktype: " & WType & ")</a> "
                                            Else
                                                Cell3.Text = "<a href='ActBillReport.aspx?DictDetails=True&sBillAccID=" & BillAccID & "&sMode=" & Mode & "'>Previous Period Transcription Activity</a> "
                                            End If
                                            Cell4.HorizontalAlign = HorizontalAlign.Right
                                            Cell5.HorizontalAlign = HorizontalAlign.Right
                                            Cell6.HorizontalAlign = HorizontalAlign.Right
                                            Cell4.Text = BillUnits
                                            Cell5.Text = "$ " & FormatNumber(DvRate, 6)
                                            Cell6.Text = "$ " & FormatNumber(BillUnits * DvRate, 2)
                                            Row1.CssClass = "tblbg2"
                                            Row1.Cells.Add(Cell1)
                                            Row1.Cells.Add(Cell2)
                                            Row1.Cells.Add(Cell3)
                                            Row1.Cells.Add(Cell4)
                                            Row1.Cells.Add(Cell5)
                                            Row1.Cells.Add(Cell6)
                                            'Response.Write("BillAmount:" & BillAmount)
                                            If BillAmount > 0 Then
                                                tblMins.Rows.Add(Row1)
                                            End If
                                            If BillSUnits > 0 And Recupdate = False Then
                                                Recupdate = True
                                                BillAmount += FormatNumber(BillSUnits * StatRate, 2)
                                                'Response.Write("BillAmount: " & BillAmount)
                                                sCell1.Text = Billdate
                                                sCell2.Text = "MT-S"
                                                sCell3.Text = "<a href='ActBillReport.aspx?DictDetails=True&sBillAccID=" & BillAccID & "&sMode=" & Mode & "&sPriority=True'>Previous Period Transcription Activity - STAT Lines </a> "
                                                sCell4.HorizontalAlign = HorizontalAlign.Right
                                                sCell5.HorizontalAlign = HorizontalAlign.Right
                                                sCell6.HorizontalAlign = HorizontalAlign.Right

                                                sCell4.Text = BillSUnits
                                                sCell5.Text = "$ " & FormatNumber(StatRate, 6)
                                                sCell6.Text = "$ " & FormatNumber(BillSUnits * StatRate, 2)
                                                sRow1.CssClass = "tblbg2"
                                                sRow1.Cells.Add(sCell1)
                                                sRow1.Cells.Add(sCell2)
                                                sRow1.Cells.Add(sCell3)
                                                sRow1.Cells.Add(sCell4)
                                                sRow1.Cells.Add(sCell5)
                                                sRow1.Cells.Add(sCell6)
                                                If BillAmount > 0 Then
                                                    tblMins.Rows.Add(sRow1)
                                                End If
                                            End If
                                        End If

                                    ElseIf Mode = "DV" Then
                                        If DRRec1("MethodName").ToString = "PerDictator" Then
                                            strQuery = "Select count(*) as NumDict from AdminSecureweb.dbo.AssignDic where GrpDicID = '" & DRRec1("SubActID").ToString & "'  "
                                            'strQuery = "Select count(*) as NumDict from (Select M.DictatorID as Numdict from AdminSecureweb.dbo.tblTranscriptionClientMain M INNER Join tblBillingLines T ON T.TranscriptionID = M.TranscriptionID where  T.BillAccID = '" & BillAccID & "' and T.SubActID = '" & DRRec1("SubActID").ToString & "' Group by M.dictatorID)T1 "
                                            Dim SQLCmd2 As New SqlCommand(strQuery, New SqlConnection(strConn))
                                            Try
                                                SQLCmd2.Connection.Open()
                                                Dim DRRec2 As SqlDataReader = SQLCmd2.ExecuteReader()
                                                If DRRec2.HasRows Then
                                                    If DRRec2.Read Then
                                                        'DLines += CInt(FormatNumber(DRRec1("stdLines"), 0))
                                                        'DSLines += CInt(FormatNumber(DRRec1("stdSLines"), 0))
                                                        'BillUnits += CInt(FormatNumber(DRRec1("BillUnits"), 0))
                                                        'BillSUnits += CInt(FormatNumber(DRRec1("BillSUnits"), 0))
                                                        'BillAmt += CDbl(FormatNumber(DRRec1("Rate") * DRRec2("NumDict"), 2))
                                                        ' Cell5.Text = CInt(FormatNumber(DRRec1("stdLines"), 0))
                                                        ' Cell6.Text = CInt(FormatNumber(DRRec1("stdSLines"), 0))

                                                        NumDict = CInt(DRRec2("NumDict"))
                                                        BillAmount = FormatNumber(NumDict * DvRate, 2)
                                                        Cell1.Text = Billdate
                                                        Cell2.Text = "MT-B"
                                                        Cell3.Text = "<a href='ActBillReport.aspx?DictDetails=True&sBillAccID=" & BillAccID & "&sSubActID=" & DRRec1("SubActID").ToString & "&sMode=" & Mode & "&sSubActName=" & SubActName & "'>Previous Period Transcription Activity (Device: " & SubActName & ")</a>"
                                                        Cell4.HorizontalAlign = HorizontalAlign.Right
                                                        Cell5.HorizontalAlign = HorizontalAlign.Right
                                                        Cell6.HorizontalAlign = HorizontalAlign.Right

                                                        Cell4.Text = NumDict
                                                        Cell5.Text = "$ " & FormatNumber(DvRate, 6)
                                                        Cell6.Text = "$ " & FormatNumber(NumDict * DvRate, 2)
                                                        Row1.CssClass = "tblbg2"
                                                        Row1.Cells.Add(Cell1)
                                                        Row1.Cells.Add(Cell2)
                                                        Row1.Cells.Add(Cell3)
                                                        Row1.Cells.Add(Cell4)
                                                        Row1.Cells.Add(Cell5)


                                                        If BillAmount > 0 Then
                                                            tblMins.Rows.Add(Row1)
                                                        End If



                                                    End If

                                                End If
                                            Finally
                                                If SQLCmd2.Connection.State = System.Data.ConnectionState.Open Then
                                                    SQLCmd2.Connection.Close()
                                                    SQLCmd2 = Nothing
                                                End If
                                            End Try
                                        Else
                                            If SubActName = "Telephone" Then
                                                BillAmount = FormatNumber(BillUnits * DvRate, 2)
                                                Cell1.Text = Billdate
                                                Cell2.Text = "MT-B"
                                                Cell3.Text = "<a href='ActBillReport.aspx?DictDetails=True&sBillAccID=" & BillAccID & "&sSubActID=" & DRRec1("SubActID").ToString & "&sMode=" & Mode & "&sSubActName=" & SubActName & "'>Previous Period Transcription Activity (Device: " & SubActName & ")</a>"
                                                Cell4.HorizontalAlign = HorizontalAlign.Right
                                                Cell5.HorizontalAlign = HorizontalAlign.Right
                                                Cell6.HorizontalAlign = HorizontalAlign.Right

                                                Cell4.Text = BillUnits
                                                Cell5.Text = "$ " & FormatNumber(DvRate, 6)
                                                Cell6.Text = "$ " & FormatNumber(BillUnits * DvRate, 2)
                                            Else
                                                BillAmount = FormatNumber(BillUnits * MiscRate, 2)
                                                Cell1.Text = Billdate
                                                Cell2.Text = "MT-B"
                                                Cell3.Text = "<a href='ActBillReport.aspx?DictDetails=True&sBillAccID=" & BillAccID & "&sSubActID=" & DRRec1("SubActID").ToString & "&sMode=" & Mode & "&sSubActName=" & SubActName & "'>Previous Period Transcription Activity (Device: " & SubActName & ")</a>"
                                                Cell4.HorizontalAlign = HorizontalAlign.Right
                                                Cell5.HorizontalAlign = HorizontalAlign.Right
                                                Cell6.HorizontalAlign = HorizontalAlign.Right

                                                Cell4.Text = BillUnits
                                                Cell5.Text = "$ " & FormatNumber(MiscRate, 6)
                                                Cell6.Text = "$ " & FormatNumber(BillUnits * MiscRate, 2)
                                            End If
                                            Row1.CssClass = "tblbg2"
                                            Row1.Cells.Add(Cell1)
                                            Row1.Cells.Add(Cell2)
                                            Row1.Cells.Add(Cell3)
                                            Row1.Cells.Add(Cell4)
                                            Row1.Cells.Add(Cell5)
                                            Row1.Cells.Add(Cell6)

                                            ' If BillAmount > 0 Then
                                            tblMins.Rows.Add(Row1)
                                            ' End If

                                            If BillSUnits > 0 Then
                                                'Response.Write(BillSUnits)
                                                BillAmount += FormatNumber(BillSUnits * StatRate, 2)
                                                sCell1.Text = Billdate
                                                sCell2.Text = "MT-S"
                                                sCell3.Text = "<a href='ActBillReport.aspx?DictDetails=True&sBillAccID=" & BillAccID & "&sSubActID=" & DRRec1("SubActID").ToString & "&sMode=" & Mode & "&sSubActName=" & SubActName & "&sPriority=True'>Previous Period Transcription Activity - STAT Lines (Device: " & SubActName & ")</a>"
                                                sCell4.HorizontalAlign = HorizontalAlign.Right
                                                sCell5.HorizontalAlign = HorizontalAlign.Right
                                                sCell6.HorizontalAlign = HorizontalAlign.Right

                                                sCell4.Text = BillSUnits
                                                sCell5.Text = "$ " & FormatNumber(StatRate, 6)
                                                sCell6.Text = "$ " & FormatNumber(BillSUnits * StatRate, 2)
                                                sRow1.CssClass = "tblbg2"
                                                sRow1.Cells.Add(sCell1)
                                                sRow1.Cells.Add(sCell2)
                                                sRow1.Cells.Add(sCell3)
                                                sRow1.Cells.Add(sCell4)
                                                sRow1.Cells.Add(sCell5)
                                                sRow1.Cells.Add(sCell6)
                                                If BillAmount > 0 Then
                                                    tblMins.Rows.Add(sRow1)
                                                End If

                                            End If
                                        End If

                                    ElseIf Mode = "LC" Then
                                        If DRRec1("MethodName").ToString = "PerDictator" Then
                                            strQuery = "Select count(*) as NumDict from AdminSecureweb.dbo.AssignDic where GrpDicID = '" & DRRec1("SubActID").ToString & "'  "
                                            'strQuery = "Select count(*) as NumDict from (Select M.DictatorID as Numdict from AdminSecureweb.dbo.tblTranscriptionClientMain M INNER Join tblBillingLines T ON T.TranscriptionID = M.TranscriptionID where  T.BillAccID = '" & BillAccID & "' and T.SubActID = '" & DRRec1("SubActID").ToString & "' Group by M.dictatorID)T1 "
                                            Dim SQLCmd2 As New SqlCommand(strQuery, New SqlConnection(strConn))
                                            Try
                                                SQLCmd2.Connection.Open()
                                                Dim DRRec2 As SqlDataReader = SQLCmd2.ExecuteReader()
                                                If DRRec2.HasRows Then
                                                    If DRRec2.Read Then
                                                        'DLines += CInt(FormatNumber(DRRec1("stdLines"), 0))
                                                        'DSLines += CInt(FormatNumber(DRRec1("stdSLines"), 0))
                                                        'BillUnits += CInt(FormatNumber(DRRec1("BillUnits"), 0))
                                                        'BillSUnits += CInt(FormatNumber(DRRec1("BillSUnits"), 0))
                                                        'BillAmt += CDbl(FormatNumber(DRRec1("Rate") * DRRec2("NumDict"), 2))
                                                        ' Cell5.Text = CInt(FormatNumber(DRRec1("stdLines"), 0))
                                                        ' Cell6.Text = CInt(FormatNumber(DRRec1("stdSLines"), 0))

                                                        NumDict = CInt(DRRec2("NumDict"))
                                                        BillAmount = FormatNumber(NumDict * DvRate, 2)
                                                        Cell1.Text = Billdate
                                                        Cell2.Text = "MT-B"
                                                        Cell3.Text = "<a href='ActBillReport.aspx?DictDetails=True&sBillAccID=" & BillAccID & "&sSubActID=" & DRRec1("SubActID").ToString & "&sMode=" & Mode & "&sSubActName=" & SubActName & "'>Previous Period Transcription Activity (Location: " & SubActName & ")</a>"
                                                        Cell4.HorizontalAlign = HorizontalAlign.Right
                                                        Cell5.HorizontalAlign = HorizontalAlign.Right
                                                        Cell6.HorizontalAlign = HorizontalAlign.Right

                                                        Cell4.Text = NumDict
                                                        Cell5.Text = "$ " & FormatNumber(DvRate, 6)
                                                        Cell6.Text = "$ " & FormatNumber(NumDict * DvRate, 2)
                                                        Row1.CssClass = "tblbg2"
                                                        Row1.Cells.Add(Cell1)
                                                        Row1.Cells.Add(Cell2)
                                                        Row1.Cells.Add(Cell3)
                                                        Row1.Cells.Add(Cell4)
                                                        Row1.Cells.Add(Cell5)
                                                        Row1.Cells.Add(Cell6)
                                                        'If BillAmount > 0 Then
                                                        tblMins.Rows.Add(Row1)
                                                        'End If



                                                    End If

                                                End If
                                            Finally
                                                If SQLCmd2.Connection.State = System.Data.ConnectionState.Open Then
                                                    SQLCmd2.Connection.Close()
                                                    SQLCmd2 = Nothing
                                                End If
                                            End Try
                                        Else
                                            BillAmount = FormatNumber(BillUnits * DvRate, 2)
                                            Cell1.Text = Billdate
                                            Cell2.Text = "MT-B"
                                            Cell3.Text = "<a href='ActBillReport.aspx?DictDetails=True&sBillAccID=" & BillAccID & "&sSubActID=" & DRRec1("SubActID").ToString & "&sMode=" & Mode & "&sSubActName=" & SubActName & "'>Previous Period Transcription Activity (Location: " & SubActName & ")</a>"
                                            Cell4.HorizontalAlign = HorizontalAlign.Right
                                            Cell5.HorizontalAlign = HorizontalAlign.Right
                                            Cell6.HorizontalAlign = HorizontalAlign.Right

                                            Cell4.Text = BillUnits
                                            Cell5.Text = "$ " & FormatNumber(DvRate, 6)
                                            Cell6.Text = "$ " & FormatNumber(BillUnits * DvRate, 2)
                                            Row1.CssClass = "tblbg2"
                                            Row1.Cells.Add(Cell1)
                                            Row1.Cells.Add(Cell2)
                                            Row1.Cells.Add(Cell3)
                                            Row1.Cells.Add(Cell4)
                                            Row1.Cells.Add(Cell5)
                                            Row1.Cells.Add(Cell6)
                                            'If BillAmount > 0 Then
                                            tblMins.Rows.Add(Row1)
                                            'End If
                                            If BillSUnits > 0 Then
                                                'Response.Write(BillSUnits)
                                                BillAmount += FormatNumber(BillSUnits * StatRate, 2)
                                                sCell1.Text = Billdate
                                                sCell2.Text = "MT-S"
                                                sCell3.Text = "<a href='ActBillReport.aspx?DictDetails=True&sBillAccID=" & BillAccID & "&sSubActID=" & DRRec1("SubActID").ToString & "&sMode=" & Mode & "&sSubActName=" & SubActName & "&sPriority=True'>Previous Period Transcription Activity - STAT Lines (Location: " & SubActName & ")</a>"
                                                sCell4.HorizontalAlign = HorizontalAlign.Right
                                                sCell5.HorizontalAlign = HorizontalAlign.Right
                                                sCell6.HorizontalAlign = HorizontalAlign.Right

                                                sCell4.Text = BillSUnits
                                                sCell5.Text = "$ " & FormatNumber(StatRate, 6)
                                                sCell6.Text = "$ " & FormatNumber(BillSUnits * StatRate, 2)
                                                sRow1.CssClass = "tblbg2"
                                                sRow1.Cells.Add(sCell1)
                                                sRow1.Cells.Add(sCell2)
                                                sRow1.Cells.Add(sCell3)
                                                sRow1.Cells.Add(sCell4)
                                                sRow1.Cells.Add(sCell5)
                                                sRow1.Cells.Add(sCell6)
                                                If BillAmount > 0 Then
                                                    tblMins.Rows.Add(sRow1)
                                                End If
                                            End If
                                        End If


                                    ElseIf Mode = "DC" Or Mode = "TW" Then
                                        If DRRec1("MethodName").ToString = "PerDictator" Then
                                            strQuery = "Select count(*) as NumDict from AdminSecureweb.dbo.AssignDic where GrpDicID = '" & DRRec1("SubActID").ToString & "'  "
                                            'strQuery = "Select count(*) as NumDict from (Select M.DictatorID as Numdict from AdminSecureweb.dbo.tblTranscriptionClientMain M INNER Join tblBillingLines T ON T.TranscriptionID = M.TranscriptionID where  T.BillAccID = '" & BillAccID & "' and T.SubActID = '" & DRRec1("SubActID").ToString & "' Group by M.dictatorID)T1 "
                                            Dim SQLCmd2 As New SqlCommand(strQuery, New SqlConnection(strConn))
                                            Try
                                                SQLCmd2.Connection.Open()
                                                Dim DRRec2 As SqlDataReader = SQLCmd2.ExecuteReader()
                                                If DRRec2.HasRows Then
                                                    If DRRec2.Read Then
                                                        'DLines += CInt(FormatNumber(DRRec1("stdLines"), 0))
                                                        'DSLines += CInt(FormatNumber(DRRec1("stdSLines"), 0))
                                                        'BillUnits += CInt(FormatNumber(DRRec1("BillUnits"), 0))
                                                        'BillSUnits += CInt(FormatNumber(DRRec1("BillSUnits"), 0))
                                                        'BillAmt += CDbl(FormatNumber(DRRec1("Rate") * DRRec2("NumDict"), 2))
                                                        ' Cell5.Text = CInt(FormatNumber(DRRec1("stdLines"), 0))
                                                        ' Cell6.Text = CInt(FormatNumber(DRRec1("stdSLines"), 0))

                                                        NumDict = CInt(DRRec2("NumDict"))
                                                        BillAmount = FormatNumber(NumDict * DvRate, 2)
                                                        Cell1.Text = Billdate
                                                        Cell2.Text = "MT-B"
                                                        Cell3.Text = "<a href='ActBillReport.aspx?DictDetails=True&sBillAccID=" & BillAccID & "&sSubActID=" & DRRec1("SubActID").ToString & "&sMode=" & Mode & "&sSubActName=" & SubActName & "'>Previous Period Transcription Activity (Group Name: " & SubActName & ")</a>"
                                                        Cell4.HorizontalAlign = HorizontalAlign.Right
                                                        Cell5.HorizontalAlign = HorizontalAlign.Right
                                                        Cell6.HorizontalAlign = HorizontalAlign.Right

                                                        Cell4.Text = NumDict
                                                        Cell5.Text = "$ " & FormatNumber(DvRate, 6)
                                                        Cell6.Text = "$ " & FormatNumber(NumDict * DvRate, 2)
                                                        Row1.CssClass = "tblbg2"
                                                        Row1.Cells.Add(Cell1)
                                                        Row1.Cells.Add(Cell2)
                                                        Row1.Cells.Add(Cell3)
                                                        Row1.Cells.Add(Cell4)
                                                        Row1.Cells.Add(Cell5)
                                                        Row1.Cells.Add(Cell6)
                                                        If BillAmount > 0 Then
                                                            tblMins.Rows.Add(Row1)
                                                        End If


                                                    End If

                                                End If
                                            Finally
                                                If SQLCmd2.Connection.State = System.Data.ConnectionState.Open Then
                                                    SQLCmd2.Connection.Close()
                                                    SQLCmd2 = Nothing
                                                End If
                                            End Try
                                        Else
                                            BillAmount = FormatNumber(BillUnits * DvRate, 2)
                                            Cell1.Text = Billdate
                                            Cell2.Text = "MT-B"
                                            Cell3.Text = "<a href='ActBillReport.aspx?DictDetails=True&sBillAccID=" & BillAccID & "&sSubActID=" & DRRec1("SubActID").ToString & "&sMode=" & Mode & "&sSubActName=" & SubActName & "'>Previous Period Transcription Activity (Group Name: " & SubActName & ")</a>"
                                            Cell4.HorizontalAlign = HorizontalAlign.Right
                                            Cell5.HorizontalAlign = HorizontalAlign.Right
                                            Cell6.HorizontalAlign = HorizontalAlign.Right

                                            Cell4.Text = BillUnits
                                            Cell5.Text = "$ " & FormatNumber(DvRate, 6)
                                            Cell6.Text = "$ " & FormatNumber(BillUnits * DvRate, 2)
                                            Row1.CssClass = "tblbg2"
                                            Row1.Cells.Add(Cell1)
                                            Row1.Cells.Add(Cell2)
                                            Row1.Cells.Add(Cell3)
                                            Row1.Cells.Add(Cell4)
                                            Row1.Cells.Add(Cell5)
                                            Row1.Cells.Add(Cell6)
                                            If BillAmount > 0 Then
                                                tblMins.Rows.Add(Row1)
                                            End If
                                            If BillSUnits > 0 Then
                                                'Response.Write(BillSUnits)
                                                BillAmount += FormatNumber(BillSUnits * StatRate, 2)
                                                sCell1.Text = Billdate
                                                sCell2.Text = "MT-S"
                                                sCell3.Text = "<a href='ActBillReport.aspx?DictDetails=True&sBillAccID=" & BillAccID & "&sSubActID=" & DRRec1("SubActID").ToString & "&sMode=" & Mode & "&sPriority=True&sSubActName=" & SubActName & "'>Previous Period Transcription Activity - STAT Lines (Group Name: " & SubActName & ")</a>"
                                                sCell4.HorizontalAlign = HorizontalAlign.Right
                                                sCell5.HorizontalAlign = HorizontalAlign.Right
                                                sCell6.HorizontalAlign = HorizontalAlign.Right

                                                sCell4.Text = BillSUnits
                                                sCell5.Text = "$ " & FormatNumber(StatRate, 6)
                                                sCell6.Text = "$ " & FormatNumber(BillSUnits * StatRate, 2)
                                                sRow1.CssClass = "tblbg2"
                                                sRow1.Cells.Add(sCell1)
                                                sRow1.Cells.Add(sCell2)
                                                sRow1.Cells.Add(sCell3)
                                                sRow1.Cells.Add(sCell4)
                                                sRow1.Cells.Add(sCell5)
                                                sRow1.Cells.Add(sCell6)
                                                If BillAmount > 0 Then
                                                    tblMins.Rows.Add(sRow1)
                                                End If
                                            End If

                                        End If


                                    End If
                                    BillTotAmount += BillAmount
                                End While
                            End If
                            DRRec1.Close()

                        Finally
                            If SQLCmd1.Connection.State = System.Data.ConnectionState.Open Then
                                SQLCmd1.Connection.Close()
                                SQLCmd1 = Nothing
                            End If
                        End Try
                        If ActBillCycle = True Then
                            If strBillCycle = "Cycle1" Then
                                VasStartDate = C1SDate
                                VasEndDate = C1EDate.AddDays(-1)
                            ElseIf strBillCycle = "Cycle2" Then
                                VasStartDate = C2SDate
                                VasEndDate = C2EDate
                            End If
                        Else
                            If strBillCycle = "Cycle2" Then
                                VasStartDate = C1SDate
                                VasEndDate = C2EDate
                            End If
                        End If
                        strQuery = "Select IT.Mode, ID.Item, IT.Descr, IT.Quantity, IT.Amount, IT.Totamount, IT.ServiceDate  from AdminSecureweb.dbo.tblInvItemDet IT Left Outer Join AdminSecureweb.dbo.ItemDetails ID ON IT.ItemID = ID.ItemID where IT.Servicedate between  '" & VasStartDate & "' and '" & VasEndDate & "' and AccountID='" & AccountID & "' and IT.Mode='VAS' "
                        '               Response.Write("Query" & strQuery)

                        Dim SQLCmd3 As New SqlCommand(strQuery, New SqlConnection(strConn))
                        Try
                            SQLCmd3.Connection.Open()
                            Dim DRRec3 As SqlDataReader = SQLCmd3.ExecuteReader()
                            If DRRec3.HasRows Then
                                While DRRec3.Read
                                    Dim vRow1 As New TableRow
                                    Dim vCell1 As New TableCell
                                    Dim vCell2 As New TableCell
                                    Dim vCell3 As New TableCell
                                    Dim vCell4 As New TableCell
                                    Dim vCell5 As New TableCell
                                    Dim vCell6 As New TableCell
                                    Billdate = DRRec3("ServiceDate").ToString
                                    vCell1.Text = Billdate.ToShortDateString
                                    vCell2.Text = DRRec3("Item").ToString
                                    vCell3.Text = DRRec3("Descr").ToString
                                    vCell4.HorizontalAlign = HorizontalAlign.Right
                                    vCell5.HorizontalAlign = HorizontalAlign.Right
                                    vCell6.HorizontalAlign = HorizontalAlign.Right

                                    vCell4.Text = DRRec3("Quantity").ToString
                                    vCell5.Text = "$ " & FormatNumber(DRRec3("Amount").ToString, 6)
                                    vCell6.Text = "$ " & FormatNumber(DRRec3("Totamount").ToString, 2)
                                    BillAmount = CDbl(FormatNumber(DRRec3("Totamount").ToString, 2))
                                    vRow1.CssClass = "tblbg2"
                                    vRow1.Cells.Add(vCell1)
                                    vRow1.Cells.Add(vCell2)
                                    vRow1.Cells.Add(vCell3)
                                    vRow1.Cells.Add(vCell4)
                                    vRow1.Cells.Add(vCell5)
                                    vRow1.Cells.Add(vCell6)
                                    tblMins.Rows.Add(vRow1)
                                    BillTotAmount += BillAmount
                                End While
                            End If
                            DRRec.Close()
                        Finally
                            If SQLCmd3.Connection.State = System.Data.ConnectionState.Open Then
                                SQLCmd3.Connection.Close()
                                SQLCmd3 = Nothing
                            End If
                        End Try


                    End If
                    Dim A11Row1 As New TableRow
                    Dim A11Cell1 As New TableCell
                    Dim A11Cell2 As New TableCell
                    Dim A11Cell3 As New TableCell
                    Dim A11Cell4 As New TableCell
                    Dim A11Cell5 As New TableCell
                    Dim A11Cell6 As New TableCell
                    Dim A11Cell7 As New TableCell
                    Dim A11Cell8 As New TableCell
                    Dim A11Cell9 As New TableCell


                    A11Row1.HorizontalAlign = HorizontalAlign.Center
                    A11Row1.CssClass = "tblbg2"
                    A11Row1.Font.Bold = True
                    A11Cell1.ColumnSpan = 4
                    A11Cell1.HorizontalAlign = HorizontalAlign.Right
                    A11Cell1.Text = ""
                    A11Cell2.Text = "Total"
                    A11Cell3.HorizontalAlign = HorizontalAlign.Right

                    A11Cell3.Text = "$ " & FormatNumber(BillTotAmount, 2)
                    A11Row1.Cells.Add(A11Cell1)
                    A11Row1.Cells.Add(A11Cell2)
                    A11Row1.Cells.Add(A11Cell3)
                    tblMins.Rows.Add(A11Row1)

                End If

            End If
        Finally
            If SQLCmd.Connection.State = System.Data.ConnectionState.Open Then
                SQLCmd.Connection.Close()
                SQLCmd = Nothing
            End If
        End Try

    End Sub


    Protected Sub PostDictDetails(ByVal InvItemID As String, ByVal Activity As String, ByVal BillAccID As String, ByVal Service As String)

        tblDict.Visible = True
        Dim StrQuery As String
        Dim strConn As String
        Dim TotUnits As Long
        Dim TotRecCount As Long
        Dim TotAmt As Decimal
        TotUnits = 0
        TotRecCount = 0
        TotAmt = 0
        lblActivity.Text = Activity

        strConn = System.Configuration.ConfigurationManager.AppSettings("ETSCon")
        StrQuery = "Select M.DictatorID, P.dname, Sum(B.Unit) as BillUnits, Sum(B.Amount) As Amount, count(B.TranscriptionID) As RecCount  "
        StrQuery = StrQuery & " from tblBillingLines B "
        StrQuery = StrQuery & "  INNER JOIN (Select * from AdminSecureweb.dbo.tblTranscriptionClientMain) M"
        StrQuery = StrQuery & " on B.TranscriptionID = M.TranscriptionID"
        StrQuery = StrQuery & " INNER JOIN (Select firstname + ' ' + lastname as dname, PhysicianID  from tblPhysicians) P"
        StrQuery = StrQuery & " on M.DictatorID = P.PhysicianID  where B.InvItemID = '" & InvItemID & "' "
        If Request("WTMode") = "True" Then
            If Request("WType") <> "" Then
                StrQuery = StrQuery & " and B.WorkType = '" & Request("WType") & "' "
            End If
        End If
        If Service = "MT-S" Then
            StrQuery = StrQuery & " and B.Priority = 1 "
        Else
            StrQuery = StrQuery & " and (B.Priority is null or B.Priority = 0 ) "
        End If
        StrQuery = StrQuery & " Group by  P.dname, M.DictatorID order by p.dname"

        'Response.Write(StrQuery)

        Dim SQLCmd As New SqlCommand(StrQuery, New SqlConnection(strConn))
        Try
            SQLCmd.Connection.Open()
            Dim DRRec As SqlDataReader = SQLCmd.ExecuteReader()
            If DRRec.HasRows Then
                While DRRec.Read
                    Dim Row1 As New TableRow
                    Dim Cell1 As New TableCell
                    Dim Cell2 As New TableCell
                    Dim Cell3 As New TableCell
                    Dim Cell4 As New TableCell
                    Row1.CssClass = "tblbg2"
                    Cell1.Text = "<a href='ActBillReport.aspx?PostRepDetails=True&InvItemID=" & InvItemID & "&BillAccId=" & BillAccID & "&activity=" & Activity & "&sDictID=" & DRRec("dictatorid").ToString & "'>" & DRRec("dname").ToString & "</a>"
                    Cell2.HorizontalAlign = HorizontalAlign.Center
                    Cell2.Text = CInt(DRRec("Billunits"))
                    Cell3.Text = CInt(DRRec("RecCount"))
                    Cell4.Text = FormatNumber(DRRec("Amount").ToString, 2)
                    Row1.Cells.Add(Cell1)
                    Row1.Cells.Add(Cell2)
                    Row1.Cells.Add(Cell4)
                    Row1.Cells.Add(Cell3)
                    tblDict.Rows.Add(Row1)
                    TotUnits += CInt(DRRec("Billunits"))
                    TotRecCount += CInt(DRRec("RecCount"))
                    TotAmt += DRRec("Amount")
                    ' Response.Write(DRRec("Amount"))

                End While
            End If
            DRRec.Close()
        Finally
            If SQLCmd.Connection.State = System.Data.ConnectionState.Open Then
                SQLCmd.Connection.Close()
                SQLCmd = Nothing
            End If
        End Try
        Dim tRow1 As New TableRow
        Dim tCell1 As New TableCell
        Dim tCell2 As New TableCell
        Dim tCell3 As New TableCell
        Dim tCell4 As New TableCell
        tRow1.CssClass = "tblbgbody"
        tCell1.Text = "<a href='ActBillReport.aspx?PostRepDetails=True&InvItemID=" & InvItemID & "&BillAccId=" & BillAccID & "&activity=" & Activity & "'>Total</a>"
        tCell2.HorizontalAlign = HorizontalAlign.Center
        tCell3.HorizontalAlign = HorizontalAlign.Center
        tCell2.Text = TotUnits
        tCell3.Text = TotRecCount
        tCell4.Text = FormatNumber(TotAmt, 2)
        tRow1.Cells.Add(tCell1)
        tRow1.Cells.Add(tCell2)
        tRow1.Cells.Add(tCell4)
        tRow1.Cells.Add(tCell3)
        tblDict.Rows.Add(tRow1)



    End Sub


    Protected Sub DictDetails(ByVal sBillAccID As String, ByVal sSubActID As String, ByVal sPriority As Boolean, ByVal sMode As String, ByVal sSubActName As String)

        tblDict.Visible = True
        Dim StrQuery As String
        Dim strConn As String
        Dim TotUnits As Long
        Dim WType As String
        Dim TotRecCount As Long
        Dim TotAmt As Decimal
        TotUnits = 0
        TotRecCount = 0
        TotAmt = 0
        If sMode = "S" Or sMode = "" Then
            If sPriority = True Then
                lblActivity.Text = "Previous Period Transcription Activity - STAT Lines"
            Else
                If Request("WTMode") = "True" Then
                    'Response.Write(DRRec1("worktype").ToString)
                    If Request("wtype") = "C" Then
                        WType = "Consultation Note"
                    ElseIf Request("wtype") = "D" Then
                        WType = "Discharge Summary"
                    ElseIf Request("wtype") = "H" Then
                        WType = "Hystory and Physical"
                    ElseIf Request("wtype") = "I" Then
                        WType = "IME"
                    ElseIf Request("wtype") = "L" Then
                        WType = "Letter"
                    ElseIf Request("wtype") = "N" Then
                        WType = "Progress Note"
                    ElseIf Request("wtype") = "O" Then
                        WType = "Operative Note"
                    ElseIf Request("wtype") = "P" Then
                        WType = "Psych Eval"
                    Else
                        WType = "Others"
                    End If
                    lblActivity.Text = "Previous Period Transcription Activity (WorkType: " & WType & ")"
                Else
                    lblActivity.Text = "Previous Period Transcription Activity"
                End If

            End If

        ElseIf sMode = "LC" Then

            If sPriority = True Then
                lblActivity.Text = "Previous Period Transcription Activity - STAT Lines (Location: " & sSubActName & ")"
            Else
                lblActivity.Text = "Previous Period Transcription Activity (Location: " & sSubActName & ")"
            End If
        ElseIf sMode = "TT" Then

            If sPriority = True Then
                lblActivity.Text = "Previous Period Transcription Activity - STAT Lines (TAT: " & sSubActName & " hrs)"
            Else
                lblActivity.Text = "Previous Period Transcription Activity (TAT: " & sSubActName & " hrs)"
            End If
        ElseIf sMode = "DV" Then

            If sPriority = True Then
                lblActivity.Text = "Previous Period Transcription Activity - STAT Lines (Device: " & sSubActName & ")"
            Else
                lblActivity.Text = "Previous Period Transcription Activity (Device: " & sSubActName & ")"
            End If

        ElseIf sMode = "DC" Or sMode = "TW" Then
            If sPriority = True Then
                lblActivity.Text = "Previous Period Transcription Activity - STAT Lines (Group Name: " & sSubActName & ")"
            Else
                lblActivity.Text = "Previous Period Transcription Activity (Group Name: " & sSubActName & ")"
            End If
        End If

        strConn = System.Configuration.ConfigurationManager.AppSettings("ETSCon")
        StrQuery = "Select M.DictatorID, P.dname, Sum(B.Unit) as BillUnits, Sum(B.Amount) As Amount , count(B.TranscriptionID) As RecCount "
        StrQuery = StrQuery & " from tblBillingLines B "
        StrQuery = StrQuery & "  INNER JOIN (Select * from AdminSecureweb.dbo.tblTranscriptionClientMain) M"
        StrQuery = StrQuery & " on B.TranscriptionID = M.TranscriptionID"
        StrQuery = StrQuery & "  INNER JOIN (Select firstname + ' ' + lastname as dname, PhysicianID  from tblPhysicians) P"
        StrQuery = StrQuery & " on M.DictatorID = P.PhysicianID  where B.BillAccID = '" & sBillAccID & "' "
        If sMode = "DC" Or sMode = "TW" Or sMode = "LC" Or sMode = "TT" Or sMode = "DV" Then
            StrQuery = StrQuery & " and B.SubActID = '" & sSubActID & "' "
        End If
        If sPriority = "True" Then
            StrQuery = StrQuery & " and B.Priority = 'True' "
        Else
            StrQuery = StrQuery & " and B.Priority = 'False' "
            If Request("WTMode") = "True" Then
                'Response.Write(DRRec1("worktype").ToString)
                If Request("wtype") <> "" Then
                    StrQuery = StrQuery & " and B.WorkType = '" & Request("wtype") & "' "
                End If
            End If
        End If
        StrQuery = StrQuery & " Group by  P.dname, M.DictatorID order by p.dname"

        'Response.Write(StrQuery)
        'Response.End()

        Dim SQLCmd As New SqlCommand(StrQuery, New SqlConnection(strConn))
        Try
            SQLCmd.Connection.Open()
            Dim DRRec As SqlDataReader = SQLCmd.ExecuteReader()
            If DRRec.HasRows Then
                While DRRec.Read
                    Dim Row1 As New TableRow
                    Dim Cell1 As New TableCell
                    Dim Cell2 As New TableCell
                    Dim Cell3 As New TableCell
                    Dim Cell4 As New TableCell
                    Row1.CssClass = "tblbg2"
                    Cell1.Text = "<a href='ActBillReport.aspx?RepDetails=True&sBillAccID=" & sBillAccID & "&sSubActID=" & sSubActID & "&sMode=" & sMode & "&sPriority=" & sPriority & "&sDictID=" & DRRec("dictatorid").ToString & "&sSubActName=" & sSubActName & "'>" & DRRec("dname").ToString & "</a>"
                    Cell2.HorizontalAlign = HorizontalAlign.Center
                    Cell2.Text = CInt(DRRec("Billunits"))
                    Cell3.Text = CInt(DRRec("RecCount"))
                    Cell4.Text = FormatNumber(DRRec("Amount").ToString, 2)
                    Row1.Cells.Add(Cell1)
                    Row1.Cells.Add(Cell2)
                    Row1.Cells.Add(Cell4)
                    Row1.Cells.Add(Cell3)
                    tblDict.Rows.Add(Row1)
                    TotUnits += CInt(DRRec("Billunits"))
                    TotRecCount += CInt(DRRec("RecCount"))
                    TotAmt += DRRec("Amount")
                    'Response.Write(DRRec("Amount") & "#")
                End While
            End If
            DRRec.Close()
        Finally
            If SQLCmd.Connection.State = System.Data.ConnectionState.Open Then
                SQLCmd.Connection.Close()
                SQLCmd = Nothing
            End If
        End Try
        Dim tRow1 As New TableRow
        Dim tCell1 As New TableCell
        Dim tCell2 As New TableCell
        Dim tCell3 As New TableCell
        Dim tCell4 As New TableCell
        tRow1.CssClass = "tblbgbody"
        tCell1.Text = "<a href='ActBillReport.aspx?RepDetails=True&sBillAccID=" & sBillAccID & "&sSubActID=" & sSubActID & "&sMode=" & sMode & "&sPriority=" & sPriority & "&sSubActName=" & sSubActName & "'>Total</a>"
        tCell2.HorizontalAlign = HorizontalAlign.Center
        tCell3.HorizontalAlign = HorizontalAlign.Center
        tCell4.HorizontalAlign = HorizontalAlign.Center
        tCell2.Text = TotUnits
        tCell3.Text = TotRecCount
        tCell4.Text = FormatNumber(TotAmt, 2)
        tRow1.Cells.Add(tCell1)
        tRow1.Cells.Add(tCell2)
        tRow1.Cells.Add(tCell4)
        tRow1.Cells.Add(tCell3)

        tblDict.Rows.Add(tRow1)



    End Sub


    Protected Sub RepDetails(ByVal sBillAccID As String, ByVal sSubActID As String, ByVal sPriority As Boolean, ByVal sMode As String, ByVal sDictID As String)
        tblReports.Visible = True
        Dim StrQuery As String
        Dim strConn As String
        Dim TotUnits As Long
        Dim DicName As String
        TotUnits = 0
        strConn = System.Configuration.ConfigurationManager.AppSettings("ETSCon")
        StrQuery = "Select P.dname, JobNumber, CustJobID, M.SubmitDate,M.dateModified, B.Unit, B.Amount,B.Rate, TA.Patient, B.Priority, T1.TemplateName  "
        StrQuery = StrQuery & " from tblBillingLines B "
        StrQuery = StrQuery & "  INNER JOIN (Select * from AdminSecureweb.dbo.tblTranscriptionClientMain) M"
        StrQuery = StrQuery & " on B.TranscriptionID = M.TranscriptionID  LEFT OUTER JOIN (Select TranscriptionID, PFirstName + ' ' + PLastName as Patient from secureweb.dbo.tbltransattributes  UNION Select  TranscriptionID, PFirstName + ' ' + PLastName as Patient from secureweb.dbo.tbltransattributes_Arch) TA  on B.TranscriptionID = TA.TranscriptionID "
        StrQuery = StrQuery & "  LEFT JOIN (Select * from AdminETS.dbo.tbltemplates) T1"
        StrQuery = StrQuery & " on M.TemplateID = T1.TemplateID"
        StrQuery = StrQuery & "  INNER JOIN (Select firstname + ' ' + lastname as dname, PhysicianID  from tblPhysicians) P"
        StrQuery = StrQuery & " on M.DictatorID = P.PhysicianID  where B.BillAccID = '" & sBillAccID & "' "
        If sMode = "DC" Or sMode = "TW" Or sMode = "LC" Or sMode = "TT" Or sMode = "DV" Then
            StrQuery = StrQuery & " and B.SubActID = '" & sSubActID & "' "
        End If
        If sPriority = "True" Then
            StrQuery = StrQuery & " and B.Priority = '" & sPriority & "' "
        Else
            StrQuery = StrQuery & " and B.Priority = 'False' "
        End If
        If Request("WTMode") = "True" Then
            If Request("Wtype") <> "" Then
                StrQuery = StrQuery & " and B.Worktype = '" & Request("Wtype") & "' "
            End If
        End If
        If sDictID <> "" Then
            StrQuery = StrQuery & " and M.dictatorID = '" & sDictID & "' "
        End If
        StrQuery = StrQuery & " order by M.datemodified"

        'Response.Write(strQuery)

        Dim SQLCmd As New SqlCommand(StrQuery, New SqlConnection(strConn))
        Try
            SQLCmd.Connection.Open()
            Dim DRRec As SqlDataReader = SQLCmd.ExecuteReader()
            If DRRec.HasRows Then
                While DRRec.Read
                    Dim Row1 As New TableRow
                    Dim Cell1 As New TableCell
                    Dim Cell2 As New TableCell
                    Dim Cell3 As New TableCell
                    Dim Cell4 As New TableCell
                    Dim Cell5 As New TableCell
                    Dim Cell6 As New TableCell
                    Dim Cell7 As New TableCell
                    Dim Cell8 As New TableCell
                    Dim Cell9 As New TableCell
                    Dim Cell10 As New TableCell
                    Dim Cell11 As New TableCell
                    Row1.CssClass = "tblbg2"
                    DicName = DRRec("dname").ToString
                    Cell1.Text = DRRec("dname").ToString
                    Cell2.Text = DRRec("jobnumber").ToString
                    Cell3.Text = DRRec("CustJobID").ToString
                    Cell7.Text = DRRec("TemplateName").ToString
                    Cell4.Text = DRRec("dateModified").ToString
                    Cell11.Text = DRRec("SubmitDate")
                    Cell5.Text = CInt(DRRec("unit").ToString)
                    Cell9.Text = FormatNumber(DRRec("Rate").ToString, 3)
                    Cell10.Text = FormatNumber(DRRec("Amount").ToString, 3)
                    Cell8.Text = DRRec("Patient").ToString
                    If DRRec("Priority").ToString = "True" Then
                        Cell6.Text = "Yes"
                    Else
                        Cell6.Text = "No"
                    End If
                    Row1.Cells.Add(Cell1)
                    Row1.Cells.Add(Cell2)
                    Row1.Cells.Add(Cell3)
                    Row1.Cells.Add(Cell8)
                    Row1.Cells.Add(Cell7)
                    Row1.Cells.Add(Cell11)
                    Row1.Cells.Add(Cell4)
                    Row1.Cells.Add(Cell5)
                    Row1.Cells.Add(Cell9)
                    Row1.Cells.Add(Cell10)
                    Row1.Cells.Add(Cell6)
                    tblReports.Rows.Add(Row1)
                    TotUnits += CInt(DRRec("unit"))

                End While
            End If
            DRRec.Close()
        Finally
            If SQLCmd.Connection.State = System.Data.ConnectionState.Open Then
                SQLCmd.Connection.Close()
                SQLCmd = Nothing
            End If
        End Try
        Dim tRow1 As New TableRow
        Dim tCell1 As New TableCell
        Dim tCell2 As New TableCell
        Dim tCell3 As New TableCell
        Dim tCell31 As New TableCell
        Dim tCell32 As New TableCell
        Dim tCell33 As New TableCell
        Dim tCell34 As New TableCell
        Dim tCell35 As New TableCell
        Dim tCell4 As New TableCell
        Dim tCell5 As New TableCell
        Dim tCell6 As New TableCell
        Dim tCell7 As New TableCell
        tRow1.CssClass = "tblbgbody"
        tCell4.Text = "Total"
        tCell1.Text = ""
        tCell2.Text = ""
        tCell3.Text = ""
        tCell6.Text = ""
        tCell7.Text = ""
        tCell5.Text = TotUnits
        tRow1.Cells.Add(tCell1)
        tRow1.Cells.Add(tCell2)
        tRow1.Cells.Add(tCell3)
        tRow1.Cells.Add(tCell31)
        tRow1.Cells.Add(tCell32)
        'tRow1.Cells.Add(tCell33)
        'tRow1.Cells.Add(tCell34)
        '  tRow1.Cells.Add(tCell35)
        tRow1.Cells.Add(tCell7)
        tRow1.Cells.Add(tCell4)
        tRow1.Cells.Add(tCell5)
        tRow1.Cells.Add(tCell6)
        tblReports.Rows.Add(tRow1)
        If sDictID = "" Then
            lblDict.Text = "All Dictators"
        Else
            lblDict.Text = DicName
        End If



    End Sub


    Protected Sub PostRepDetails(ByVal InvItemID As String, ByVal sDictID As String, ByVal Service As String)
        Try

     
            tblReports.Visible = True
            Dim StrQuery As String
            Dim strConn As String
            Dim TotUnits As Long
            Dim DicName As String
            TotUnits = 0
            strConn = System.Configuration.ConfigurationManager.AppSettings("ETSCon")
            StrQuery = "Select P.dname, JobNumber, CustJobID, M.SubmitDate, M.dateModified, B.Unit, B.Amount,B.Rate, TA.Patient, B.Priority, T1.TemplateName  "
            StrQuery = StrQuery & " from tblBillingLines B "
            StrQuery = StrQuery & "  INNER JOIN (Select * from AdminSecureweb.dbo.tblTranscriptionClientMain) M"
            StrQuery = StrQuery & " on B.TranscriptionID = M.TranscriptionID  LEFT OUTER JOIN (Select TranscriptionID, PFirstName + ' ' + PLastName as Patient from secureweb.dbo.tbltransattributes  UNION Select  TranscriptionID, PFirstName + ' ' + PLastName as Patient from secureweb.dbo.tbltransattributes_Arch) TA  on B.TranscriptionID = TA.TranscriptionID "
            StrQuery = StrQuery & "  LEFT JOIN (Select * from AdminETS.dbo.tbltemplates) T1"
            StrQuery = StrQuery & " on M.TemplateID = T1.TemplateID"
            StrQuery = StrQuery & "  INNEr JOIN (Select firstname + ' ' + lastname as dname, PhysicianID  from tblPhysicians) P"
            StrQuery = StrQuery & " on M.DictatorID = P.PhysicianID  where B.InvItemID = '" & InvItemID & "' "
            If Request("WTMode") = "True" Then
                If Request("WType") <> "" Then
                    StrQuery = StrQuery & " and B.WorkType = '" & Request("WType") & "' "
                End If
            End If
            If Service = "MT-S" Then
                StrQuery = StrQuery & " and B.Priority = 1 "
            Else
                StrQuery = StrQuery & " and (B.Priority is null or B.Priority = 0 ) "
            End If
            If sDictID <> "" Then
                StrQuery = StrQuery & " and M.dictatorID = '" & sDictID & "' "
            End If
            StrQuery = StrQuery & " order by M.datemodified"

            'Response.Write(strQuery)

            Dim SQLCmd As New SqlCommand(StrQuery, New SqlConnection(strConn))
            Try
                SQLCmd.Connection.Open()
                Dim DRRec As SqlDataReader = SQLCmd.ExecuteReader()
                If DRRec.HasRows Then
                    While DRRec.Read
                        Dim Row1 As New TableRow
                        Dim Cell1 As New TableCell
                        Dim Cell2 As New TableCell
                        Dim Cell3 As New TableCell
                        Dim Cell4 As New TableCell
                        Dim Cell5 As New TableCell
                        Dim Cell6 As New TableCell
                        Dim Cell7 As New TableCell
                        Dim Cell8 As New TableCell
                        Dim Cell9 As New TableCell
                        Dim Cell10 As New TableCell
                        Dim Cell11 As New TableCell
                        Row1.CssClass = "tblbg2"
                        DicName = DRRec("dname")
                        Cell1.Text = DRRec("dname")
                        Cell2.Text = DRRec("jobnumber")
                        Cell3.Text = DRRec("CustJobID")
                        Cell4.Text = DRRec("dateModified")
                        Cell11.Text = DRRec("SubmitDate")
                        Cell7.Text = DRRec("TemplateName").ToString
                        Cell5.Text = CInt(DRRec("unit"))
                        Cell9.Text = FormatNumber(DRRec("Rate").ToString, 3)
                        Cell10.Text = FormatNumber(DRRec("Amount").ToString, 3)
                        Cell8.Text = DRRec("Patient").ToString
                        If DRRec("Priority").ToString = "True" Then
                            Cell6.Text = "Yes"
                        Else
                            Cell6.Text = "No"
                        End If
                        Row1.Cells.Add(Cell1)
                        Row1.Cells.Add(Cell2)
                        Row1.Cells.Add(Cell3)
                        Row1.Cells.Add(Cell8)
                        Row1.Cells.Add(Cell7)
                        Row1.Cells.Add(Cell11)
                        Row1.Cells.Add(Cell4)
                        Row1.Cells.Add(Cell5)
                        Row1.Cells.Add(Cell9)
                        Row1.Cells.Add(Cell10)
                        Row1.Cells.Add(Cell6)
                        tblReports.Rows.Add(Row1)
                        TotUnits += CInt(DRRec("unit"))

                    End While
                End If
                DRRec.Close()
            Finally
                If SQLCmd.Connection.State = System.Data.ConnectionState.Open Then
                    SQLCmd.Connection.Close()
                    SQLCmd = Nothing
                End If
            End Try
            Dim tRow1 As New TableRow
            Dim tCell1 As New TableCell
            Dim tCell2 As New TableCell
            Dim tCell3 As New TableCell
            Dim tCell31 As New TableCell
            Dim tCell32 As New TableCell
            Dim tCell33 As New TableCell
            Dim tCell34 As New TableCell
            Dim tCell35 As New TableCell
            Dim tCell36 As New TableCell
            Dim tCell4 As New TableCell
            Dim tCell5 As New TableCell
            Dim tCell6 As New TableCell
            tRow1.CssClass = "tblbgbody"
            tCell4.Text = "Total"
            tCell1.Text = ""
            tCell2.Text = ""
            tCell3.Text = ""
            tCell6.Text = ""
            tCell5.Text = TotUnits
            tRow1.Cells.Add(tCell1)
            tRow1.Cells.Add(tCell2)
            tRow1.Cells.Add(tCell3)
            tRow1.Cells.Add(tCell31)
            tRow1.Cells.Add(tCell32)
            tRow1.Cells.Add(tCell33)
            'tRow1.Cells.Add(tCell34)
            'tRow1.Cells.Add(tCell35)
            ' tRow1.Cells.Add(tCell36)
            tRow1.Cells.Add(tCell4)
            tRow1.Cells.Add(tCell5)
            tRow1.Cells.Add(tCell6)
            tblReports.Rows.Add(tRow1)
            If sDictID = "" Then
                lblDict.Text = "All Dictators"
            Else
                lblDict.Text = DicName
            End If
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try


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
        'Response.Write("Page Loaded")
        tblDict.Visible = False
        tblReports.Visible = False
        Dim sDictID As String
        Dim sSubactName As String
        sDictID = ""
        If Request("IndAct") = "True" Then
            ShowIndActDetails(Request("AccID"), Request("BillMonth"), Request("BillYear"), Request("BillCycle"))
        End If

        If Request("AllActDetails") = "True" Then

            Dim Billmonth As Integer
            Dim Billyear As Integer
            Dim Billcycle As Integer
            Billmonth = Request("billmonth")
            Billyear = Request("billyear")
            Billcycle = Request("billcycle")
            ShowAllActDetails(Billmonth, Billyear, Billcycle)

        End If


        If Request("ActDetails") = "True" Then
            ShowActDetails(Request("BillAccID"))
        End If
        If Request("DictDetails") = "True" Then

            Dim sMode As String
            Dim sPriority As Boolean
            Dim sSubActID As String
            Dim sBillAccID As String
            sSubactName = Request("sSubactName")
            sBillAccID = Request("sBillAccID")
            sSubActID = Request("sSubActID")
            If Request("sPriority") = "True" Then
                sPriority = True
            Else
                sPriority = False
            End If
            sMode = Request("sMode")

            ShowActDetails(sBillAccID)
            DictDetails(sBillAccID, sSubActID, sPriority, sMode, sSubactName)
            RepDetails(sBillAccID, sSubActID, sPriority, sMode, sDictID)
        End If

        If Request("PostDictDetails") = "True" Then

            Dim InvItemID As String
            InvItemID = Request("InvItemID")
            Dim BillAccID As String
            BillAccID = Request("BillAccID")
            Dim Activity As String
            Activity = Request("Activity")
            Dim Service As String
            Service = Request("service")
            ShowActDetails(BillAccID)
            PostDictDetails(InvItemID, Activity, BillAccID, Service)
            PostRepDetails(InvItemID, sDictID, Service)
        End If


        If Request("PostRepDetails") = "True" Then

            Dim InvItemID As String
            InvItemID = Request("InvItemID")
            Dim BillAccID As String
            BillAccID = Request("BillAccID")
            Dim Activity As String
            Activity = Request("Activity")
            sDictID = Request("sDictID")
            Dim Service As String
            Service = Request("service")
            ShowActDetails(BillAccID)
            PostDictDetails(InvItemID, Activity, BillAccID, Service)
            PostRepDetails(InvItemID, sDictID, Service)
        End If


        If Request("RepDetails") = "True" Then

            Dim sMode As String
            Dim sPriority As Boolean
            Dim sSubActID As String
            Dim sBillAccID As String
            sSubactName = Request("sSubactName")
            sDictID = Request("sDictID")
            sBillAccID = Request("sBillAccID")
            sSubActID = Request("sSubActID")
            If Request("sPriority") = "True" Then
                sPriority = True
            Else
                sPriority = False
            End If
            sMode = Request("sMode")

            ShowActDetails(sBillAccID)
            DictDetails(sBillAccID, sSubActID, sPriority, sMode, sSubactName)
            RepDetails(sBillAccID, sSubActID, sPriority, sMode, sDictID)
        End If

    End Sub

    Protected Sub ShowAllActDetails(ByVal billmonth As Integer, ByVal billyear As Integer, ByVal billcycle As Integer)
        Dim strConn As String
        Dim strCategory As String
        strCategory = ""
        Dim t2 As New Table
        t2.Style("width") = "100%"
        t2.BorderWidth = 2
        t2.GridLines = GridLines.Both
        Dim I As Integer
        I = 0
        strConn = System.Configuration.ConfigurationManager.AppSettings("ETSCon")
        Dim strQuery As String
        Dim DLines As String
        Dim DSLines As String
        Dim BillUnits As String
        Dim BillSUnits As String
        Dim BillAmt As String
        Dim BillOtherAmt As String
        Dim BillTotAmount As String

        Dim STSTDLines As String
        Dim STSTDSLines As String
        Dim STBillUnits As String
        Dim STBillSUnits As String
        Dim STBillAmt As String
        Dim STBillOtherAmt As String
        Dim STBillTotAmount As String
        STSTDLines = 0
        STSTDSLines = 0
        STBillUnits = 0
        STBillSUnits = 0
        STBillAmt = 0
        STBillOtherAmt = 0
        STBillTotAmount = 0
        Dim C1SDate As Date
        Dim C1EDate As Date
        Dim C2SDate As Date
        Dim C2EDate As Date
        Dim TSTDLines As String
        Dim TSTDSLines As String
        Dim TBillUnits As String
        Dim TBillSUnits As String
        Dim TBillAmt As String
        Dim TBillOtherAmt As String
        Dim TBillTotAmount As String
        TSTDLines = 0
        TSTDSLines = 0
        TBillUnits = 0
        TBillSUnits = 0
        TBillAmt = 0
        TBillOtherAmt = 0
        TBillTotAmount = 0
        Dim SubActName As String
        Dim DvRate As String
        Dim MiscRate As String
        Dim StatRate As String
        Dim Mode As String
        Dim ActName As String
        Dim NumDict As Integer
        Dim BillAccID As String
        Dim BoolCycle As Boolean
        Dim Billdate As Date
        Dim BillAmount As Double
        Dim AccountID As String
        Dim strBillCycle As String
        Dim ActBillCycle As Boolean = False
        Dim VasStartDate As Date
        Dim VasEndDate As Date
        BillAmount = "0.00"
        Dim WType As String = String.Empty
        Dim WTMode As Boolean = False
        strQuery = "Select A.Accountid, BA.Mode,  A.AccountName as Description, A.BillActNumber, C.priority, C.Description as CateDescr, BA.MinBilling, BA.BillAccID, BA.Posted, BA.BillMonth, BA.BillYear, BA.BillCycle, BA.Cycle, BA.InvoiceID, BA.WTMode "
        strQuery = strQuery & " from tblaccounts A "
        strQuery = strQuery & " INNER JOIN (Select * from AdminSecureweb.dbo.tblBillAccounts) BA"
        strQuery = strQuery & " on BA.AccountID = A.AccountID"
        strQuery = strQuery & " LEFT OUTER JOIN (Select * from tblActCategories) C"
        strQuery = strQuery & " on C.category = A.Category  where BA.Billmonth='" & billmonth & "' and BA.Billyear='" & billyear & "'  and BA.Billcycle='" & billcycle & "' "

        '        Response.Write(strQuery)

        Dim SQLCmd As New SqlCommand(strQuery, New SqlConnection(strConn))
        Try
            SQLCmd.Connection.Open()
            Dim DRRec As SqlDataReader = SQLCmd.ExecuteReader()
            If DRRec.HasRows Then
                While DRRec.Read
                    DLines = 0
                    DSLines = 0
                    BillUnits = 0
                    BillSUnits = 0
                    BillAmt = 0
                    BillOtherAmt = 0
                    BillTotAmount = 0
                    BillAccID = DRRec("BillAccID").ToString
                    AccountID = DRRec("AccountID").ToString
                    billmonth = DRRec("BillMonth")
                    billyear = DRRec("BillYear")
                    billcycle = DRRec("BillCycle")
                    BoolCycle = DRRec("Cycle").ToString
                    If DRRec("Cycle").ToString = "True" Then
                        ActBillCycle = True
                    Else
                        ActBillCycle = False
                    End If
                    If DRRec("billCycle").ToString = "1" Then
                        strBillCycle = "Cycle1"
                    Else
                        strBillCycle = "Cycle2"
                    End If
                    C1SDate = billmonth & "/1/" & billyear
                    C2SDate = billmonth & "/16/" & billyear
                    C1EDate = billmonth & "/16/" & billyear
                    C2EDate = DateAdd(DateInterval.Day, -1, DateAdd(DateInterval.Month, 1, C1SDate))
                    Billdate = DateAdd(DateInterval.Month, 1, C1SDate)
                    If billcycle = "2" Then
                        Billdate = DateAdd(DateInterval.Month, 1, C1SDate)
                    Else
                        Billdate = C1EDate
                    End If
                    Mode = Trim(DRRec("Mode").ToString)
                    ActName = DRRec("Description").ToString

                    Dim Trow1 As New TableRow
                    Trow1.CssClass = "tblbg1"
                    Dim tcell1 As New TableCell
                    tcell1.HorizontalAlign = HorizontalAlign.Center
                    tcell1.ColumnSpan = "6"
                    tcell1.Text = "Account Name: " & DRRec("Description")
                    Trow1.Cells.Add(tcell1)
                    tblMins.Rows.Add(Trow1)
                    If DRRec("Posted").ToString = "True" Then
                        strQuery = "Select IT.AutoID, IT.Mode, ID.Item, IT.Descr, IT.Quantity, IT.Amount, IT.Totamount, IT.ServiceDate, IT.WTMode, IT.WorkType  from AdminSecureweb.dbo.tblInvItemDet IT Left Outer Join AdminSecureweb.dbo.ItemDetails ID ON IT.ItemID = ID.ItemID where InvoiceID='" & DRRec("InvoiceID").ToString & "' "
                        'Response.Write(strQuery)
                        Dim SQLCmd3 As New SqlCommand(strQuery, New SqlConnection(strConn))
                        Try
                            SQLCmd3.Connection.Open()
                            Dim DRRec3 As SqlDataReader = SQLCmd3.ExecuteReader()
                            If DRRec3.HasRows Then
                                While DRRec3.Read
                                    Dim vRow1 As New TableRow
                                    Dim vCell1 As New TableCell
                                    Dim vCell2 As New TableCell
                                    Dim vCell3 As New TableCell
                                    Dim vCell4 As New TableCell
                                    Dim vCell5 As New TableCell
                                    Dim vCell6 As New TableCell
                                    Billdate = DRRec3("ServiceDate").ToString
                                    vCell1.Text = Billdate.ToShortDateString
                                    vCell2.Text = DRRec3("Item").ToString
                                    vCell3.Text = "<a href='ActBillReport.aspx?PostDictDetails=True&BillAccID=" & BillAccID & "&InvItemID=" & DRRec3("AutoID").ToString & "&activity=" & DRRec3("Descr").ToString & "&WTMode=" & DRRec3("WTMode").ToString & "&WType=" & Trim(DRRec3("worktype").ToString) & "'>" & DRRec3("Descr").ToString & "</a>"
                                    vCell4.HorizontalAlign = HorizontalAlign.Right
                                    vCell5.HorizontalAlign = HorizontalAlign.Right
                                    vCell6.HorizontalAlign = HorizontalAlign.Right
                                    vCell4.Text = DRRec3("Quantity").ToString
                                    vCell5.Text = "$ " & FormatNumber(DRRec3("Amount").ToString, 6)
                                    vCell6.Text = "$ " & FormatNumber(DRRec3("Totamount").ToString, 2)
                                    BillAmount = CDbl(FormatNumber(DRRec3("Totamount").ToString, 2))
                                    vRow1.CssClass = "tblbg2"
                                    vRow1.Cells.Add(vCell1)
                                    vRow1.Cells.Add(vCell2)
                                    vRow1.Cells.Add(vCell3)
                                    vRow1.Cells.Add(vCell4)
                                    vRow1.Cells.Add(vCell5)
                                    vRow1.Cells.Add(vCell6)
                                    tblMins.Rows.Add(vRow1)
                                    BillTotAmount += BillAmount
                                End While
                            End If
                            DRRec3.Close()
                        Finally
                            If SQLCmd3.Connection.State = System.Data.ConnectionState.Open Then
                                SQLCmd3.Connection.Close()
                                SQLCmd3 = Nothing
                            End If
                        End Try
                    Else

                        If DRRec("WTMode").ToString.ToLower = "true" Then
                            WTMode = True
                        Else
                            WTMode = False
                        End If
                        AccountID = DRRec("AccountID").ToString
                        billmonth = DRRec("BillMonth")
                        billyear = DRRec("BillYear")
                        billcycle = DRRec("BillCycle")
                        BoolCycle = DRRec("Cycle").ToString

                        'C1SDate = billmonth & "/1/" & billyear
                        'C2SDate = billmonth & "/16/" & billyear
                        'C1EDate = billmonth & "/16/" & billyear
                        'C2EDate = DateAdd(DateInterval.Day, -1, DateAdd(DateInterval.Month, 1, C1SDate))
                        'Billdate = DateAdd(DateInterval.Month, 1, C1SDate)
                        If DRRec("Cycle").ToString = "True" Then
                            ActBillCycle = True
                        Else
                            ActBillCycle = False
                        End If
                        If DRRec("billCycle").ToString = "1" Then
                            strBillCycle = "Cycle1"
                        Else
                            strBillCycle = "Cycle2"
                        End If
                        C1SDate = billmonth & "/1/" & billyear
                        C2SDate = billmonth & "/16/" & billyear
                        C1EDate = billmonth & "/16/" & billyear
                        C2EDate = DateAdd(DateInterval.Day, -1, DateAdd(DateInterval.Month, 1, C1SDate))
                        'Billdate = DateAdd(DateInterval.Month, 1, C1SDate)
                        If billcycle = "2" Then
                            Billdate = DateAdd(DateInterval.Month, 1, C1SDate)
                        Else
                            Billdate = C1EDate
                        End If

                        Mode = Trim(DRRec("Mode").ToString)
                        ActName = DRRec("Description").ToString
                        If Mode = "DC" Or Mode = "TW" Or Mode = "LC" Or Mode = "TT" Or Mode = "DV" Then
                            strQuery = "Select  B.SubActID, B.SubActName,  B.Rate, B.MiscRate, B.StatRate, L.MethodName, isnull(T1.Lines,0) AS BillUnits, isnull(T2.Lines,0) AS BillSUnits,  isnull(T1.stdLines,0) AS stdLines, isnull(T2.stdLines,0) AS stdSLines, isnull(T1.Amt,0) AS BillAmt    "
                            strQuery = strQuery & " from AdminSecureweb.dbo.tblAccBillDetails B"
                            strQuery = strQuery & " LEFT OUTER JOIN (Select TrackID, MethodName from AdminETS.dbo.tblLCMethodology ) L"
                            strQuery = strQuery & " on L.TrackID = B.LCMethodID "
                            strQuery = strQuery & " INNER JOIN (Select BillAccID, SubActID, sum(unit) AS Lines, sum(amount) AS Amt, sum(stdLCCount) AS stdLines from tblBillingLines where BillAccID = '" & BillAccID & "'  and priority = 'False' Group by BillAccID, SubActID) T1"
                            strQuery = strQuery & " on T1.BillAccID = B.BillAccID and T1.SubActID = B.SubActID  "
                            strQuery = strQuery & " LEFT OUTER JOIN (Select BillAccID, SubActID, sum(unit) AS Lines, sum(amount) AS Amt, sum(stdLCCount) AS stdLines from tblBillingLines where BillAccID = '" & BillAccID & "'  and priority='True' Group by BillAccID, SubActID) T2"
                            strQuery = strQuery & " on T2.BillAccID = B.BillAccID and T2.SubActID = B.SubActID  where B.BillAccID = '" & BillAccID & "'"
                        Else
                            If WTMode = True Then
                                strQuery = "Select  T1.WorkType, T1.Rate as WTRate, B.SubActID, B.SubActName,  B.Rate, B.MiscRate, B.StatRate, L.MethodName, isnull(T1.Lines,0) AS BillUnits, isnull(T2.Lines,0) AS BillSUnits,  isnull(T1.stdLines,0) AS stdLines, isnull(T2.stdLines,0) AS stdSLines, isnull(T1.Amt,0) AS BillAmt    "
                                strQuery = strQuery & " from AdminSecureweb.dbo.tblAccBillDetails B"
                                strQuery = strQuery & " LEFT OUTER JOIN (Select TrackID, MethodName from AdminETS.dbo.tblLCMethodology ) L"
                                strQuery = strQuery & " on L.TrackID = B.LCMethodID "
                                strQuery = strQuery & " INNER JOIN (Select BillAccID, WorkType, Rate, sum(unit) AS Lines, sum(amount) AS Amt, sum(stdLCCount) AS stdLines from tblBillingLines where BillAccID = '" & BillAccID & "'  and priority = 'False' Group by BillAccID, WorkType, Rate) T1"
                                strQuery = strQuery & " on T1.BillAccID = B.BillAccID  "
                                strQuery = strQuery & " LEFT OUTER JOIN (Select BillAccID, sum(unit) AS Lines, sum(amount) AS Amt, sum(stdLCCount) AS stdLines from tblBillingLines where BillAccID = '" & BillAccID & "'  and priority='True' Group by BillAccID) T2"
                                strQuery = strQuery & " on T2.BillAccID = B.BillAccID  where B.BillAccID = '" & BillAccID & "'"
                            Else
                                strQuery = "Select  B.SubActID, B.SubActName,  B.Rate, B.MiscRate, B.StatRate, L.MethodName, isnull(T1.Lines,0) AS BillUnits, isnull(T2.Lines,0) AS BillSUnits,  isnull(T1.stdLines,0) AS stdLines, isnull(T2.stdLines,0) AS stdSLines, isnull(T1.Amt,0) AS BillAmt    "
                                strQuery = strQuery & " from AdminSecureweb.dbo.tblAccBillDetails B"
                                strQuery = strQuery & " LEFT OUTER JOIN (Select TrackID, MethodName from AdminETS.dbo.tblLCMethodology ) L"
                                strQuery = strQuery & " on L.TrackID = B.LCMethodID "
                                strQuery = strQuery & " INNER JOIN (Select BillAccID, sum(unit) AS Lines, sum(amount) AS Amt, sum(stdLCCount) AS stdLines from tblBillingLines where BillAccID = '" & BillAccID & "'  and priority = 'False' Group by BillAccID) T1"
                                strQuery = strQuery & " on T1.BillAccID = B.BillAccID  "
                                strQuery = strQuery & " LEFT OUTER JOIN (Select BillAccID, sum(unit) AS Lines, sum(amount) AS Amt, sum(stdLCCount) AS stdLines from tblBillingLines where BillAccID = '" & BillAccID & "'  and priority='True' Group by BillAccID) T2"
                                strQuery = strQuery & " on T2.BillAccID = B.BillAccID  where B.BillAccID = '" & BillAccID & "'"
                            End If

                        End If

                        'Response.Write(strQuery)

                        Dim SQLCmd1 As New SqlCommand(strQuery, New SqlConnection(strConn))
                        Try
                            SQLCmd1.Connection.Open()
                            Dim DRRec1 As SqlDataReader = SQLCmd1.ExecuteReader()
                            If DRRec1.HasRows Then
                                While (DRRec1.Read)

                                    Dim Row1 As New TableRow
                                    Dim Cell1 As New TableCell
                                    Dim Cell2 As New TableCell
                                    Dim Cell3 As New TableCell
                                    Dim Cell4 As New TableCell
                                    Dim Cell5 As New TableCell
                                    Dim Cell6 As New TableCell
                                    Dim sRow1 As New TableRow
                                    Dim sCell1 As New TableCell
                                    Dim sCell2 As New TableCell
                                    Dim sCell3 As New TableCell
                                    Dim sCell4 As New TableCell
                                    Dim sCell5 As New TableCell
                                    Dim sCell6 As New TableCell


                                    SubActName = DRRec1("SubActName").ToString
                                    If WTMode = True Then
                                        DvRate = DRRec1("WTRate").ToString
                                    Else
                                        DvRate = DRRec1("Rate").ToString
                                    End If
                                    MiscRate = DRRec1("MiscRate").ToString
                                    StatRate = DRRec1("STATRate").ToString

                                    Row1.Font.Size = "9"



                                    'DLines += CInt(FormatNumber(DRRec1("stdLines"), 0))
                                    'DSLines += CInt(FormatNumber(DRRec1("stdSLines"), 0))
                                    BillUnits = CInt(FormatNumber(DRRec1("BillUnits"), 0))
                                    BillSUnits = CInt(FormatNumber(DRRec1("BillSUnits"), 0))
                                    'BillAmt += CDbl(FormatNumber(DRRec1("BillAmt"), 2))
                                    'Cell5.Text = CInt(FormatNumber(DRRec1("stdLines"), 0))
                                    'Cell6.Text = CInt(FormatNumber(DRRec1("stdSLines"), 0))
                                    'Cell7.Text = CInt(FormatNumber(DRRec1("BillUnits"), 0))
                                    'Cell8.Text = CInt(FormatNumber(DRRec1("BillSUnits"), 0))
                                    'Cell9.Text = CDbl(FormatNumber(DRRec1("BillAmt"), 2))
                                    If Mode = "S" Or Mode = "" Then
                                        If DRRec1("MethodName").ToString = "PerDictator" Then
                                            strQuery = "Select count(*) as NumDict from AdminSecureweb.dbo.AssignDic where GrpDicID = '" & DRRec1("SubActID").ToString & "'  "
                                            'strQuery = "Select count(*) as NumDict from (Select M.DictatorID as Numdict from AdminSecureweb.dbo.tblTranscriptionClientMain M INNER Join tblBillingLines T ON T.TranscriptionID = M.TranscriptionID where  T.BillAccID = '" & BillAccID & "' Group by M.dictatorID)T1 "
                                            Dim SQLCmd2 As New SqlCommand(strQuery, New SqlConnection(strConn))
                                            Try
                                                SQLCmd2.Connection.Open()
                                                Dim DRRec2 As SqlDataReader = SQLCmd2.ExecuteReader()
                                                If DRRec2.HasRows Then
                                                    If DRRec2.Read Then
                                                        'DLines += CInt(FormatNumber(DRRec1("stdLines"), 0))
                                                        'DSLines += CInt(FormatNumber(DRRec1("stdSLines"), 0))
                                                        'BillUnits += CInt(FormatNumber(DRRec1("BillUnits"), 0))
                                                        'BillSUnits += CInt(FormatNumber(DRRec1("BillSUnits"), 0))
                                                        'BillAmt += CDbl(FormatNumber(DRRec1("Rate") * DRRec2("NumDict"), 2))
                                                        ' Cell5.Text = CInt(FormatNumber(DRRec1("stdLines"), 0))
                                                        ' Cell6.Text = CInt(FormatNumber(DRRec1("stdSLines"), 0))

                                                        NumDict = CInt(DRRec2("NumDict"))
                                                        BillAmount = FormatNumber(NumDict * DvRate, 2)
                                                        Cell1.Text = Billdate
                                                        Cell2.Text = "MT-B"
                                                        Cell3.Text = "Previous Period Transcription Activity "
                                                        Cell4.HorizontalAlign = HorizontalAlign.Right
                                                        Cell5.HorizontalAlign = HorizontalAlign.Right
                                                        Cell6.HorizontalAlign = HorizontalAlign.Right
                                                        Cell4.Text = NumDict
                                                        Cell5.Text = "$ " & FormatNumber(DvRate, 6)
                                                        Cell6.Text = "$ " & FormatNumber(NumDict * DvRate, 2)
                                                        Row1.CssClass = "tblbg2"
                                                        Row1.Cells.Add(Cell1)
                                                        Row1.Cells.Add(Cell2)
                                                        Row1.Cells.Add(Cell3)
                                                        Row1.Cells.Add(Cell4)
                                                        Row1.Cells.Add(Cell5)
                                                        Row1.Cells.Add(Cell6)
                                                        tblMins.Rows.Add(Row1)


                                                    End If

                                                End If
                                            Finally
                                                If SQLCmd2.Connection.State = System.Data.ConnectionState.Open Then
                                                    SQLCmd2.Connection.Close()
                                                    SQLCmd2 = Nothing
                                                End If
                                            End Try
                                        Else
                                            BillAmount = FormatNumber(BillUnits * DvRate, 2)
                                            Cell1.Text = Billdate
                                            Cell2.Text = "MT-B"
                                            If WTMode = True Then
                                                'Response.Write(DRRec1("worktype").ToString)
                                                If Trim(DRRec1("worktype").ToString) = "C" Then
                                                    WType = "Consultation Note"
                                                ElseIf Trim(DRRec1("worktype").ToString) = "D" Then
                                                    WType = "Discharge Summary"
                                                ElseIf Trim(DRRec1("worktype").ToString) = "H" Then
                                                    WType = "Hystory and Physical"
                                                ElseIf Trim(DRRec1("worktype").ToString) = "I" Then
                                                    WType = "IME"
                                                ElseIf Trim(DRRec1("worktype").ToString) = "L" Then
                                                    WType = "Letter"
                                                ElseIf Trim(DRRec1("worktype").ToString) = "N" Then
                                                    WType = "Progress Note"
                                                ElseIf Trim(DRRec1("worktype").ToString) = "O" Then
                                                    WType = "Operative Note"
                                                ElseIf Trim(DRRec1("worktype").ToString) = "P" Then
                                                    WType = "Psych Eval"
                                                Else
                                                    WType = "Others"
                                                End If
                                                Cell3.Text = "<a href='ActBillReport.aspx?DictDetails=True&sBillAccID=" & BillAccID & "&sMode=" & Mode & "&WTMode=" & WTMode & "&WType=" & Trim(DRRec1("worktype").ToString) & "'>Previous Period Transcription Activity (Worktype: " & WType & ")</a> "
                                            Else
                                                Cell3.Text = "<a href='ActBillReport.aspx?DictDetails=True&sBillAccID=" & BillAccID & "&sMode=" & Mode & "'>Previous Period Transcription Activity</a> "
                                            End If
                                            Cell4.HorizontalAlign = HorizontalAlign.Right
                                            Cell5.HorizontalAlign = HorizontalAlign.Right
                                            Cell6.HorizontalAlign = HorizontalAlign.Right

                                            Cell4.Text = BillUnits
                                            Cell5.Text = "$ " & FormatNumber(DvRate, 6)
                                            Cell6.Text = "$ " & FormatNumber(BillUnits * DvRate, 2)
                                            Row1.CssClass = "tblbg2"
                                            Row1.Cells.Add(Cell1)
                                            Row1.Cells.Add(Cell2)
                                            Row1.Cells.Add(Cell3)
                                            Row1.Cells.Add(Cell4)
                                            Row1.Cells.Add(Cell5)
                                            Row1.Cells.Add(Cell6)
                                            tblMins.Rows.Add(Row1)

                                            If BillSUnits > 0 Then
                                                BillAmount += FormatNumber(BillSUnits * StatRate, 2)
                                                sCell1.Text = Billdate
                                                sCell2.Text = "MT-S"
                                                sCell3.Text = "<a href='ActBillReport.aspx?DictDetails=True&sBillAccID=" & BillAccID & "&sMode=" & Mode & "&sPriority=True'>Previous Period Transcription Activity - STAT Lines </a> "
                                                sCell4.HorizontalAlign = HorizontalAlign.Right
                                                sCell5.HorizontalAlign = HorizontalAlign.Right
                                                sCell6.HorizontalAlign = HorizontalAlign.Right

                                                sCell4.Text = BillSUnits
                                                sCell5.Text = "$ " & FormatNumber(StatRate, 6)
                                                sCell6.Text = "$ " & FormatNumber(BillSUnits * StatRate, 2)
                                                sRow1.CssClass = "tblbg2"
                                                sRow1.Cells.Add(sCell1)
                                                sRow1.Cells.Add(sCell2)
                                                sRow1.Cells.Add(sCell3)
                                                sRow1.Cells.Add(sCell4)
                                                sRow1.Cells.Add(sCell5)
                                                sRow1.Cells.Add(sCell6)
                                                tblMins.Rows.Add(sRow1)
                                            End If
                                        End If

                                    ElseIf Mode = "DV" Then
                                        If DRRec1("MethodName").ToString = "PerDictator" Then
                                            strQuery = "Select count(*) as NumDict from AdminSecureweb.dbo.AssignDic where GrpDicID = '" & DRRec1("SubActID").ToString & "'  "
                                            'strQuery = "Select count(*) as NumDict from (Select M.DictatorID as Numdict from AdminSecureweb.dbo.tblTranscriptionClientMain M INNER Join tblBillingLines T ON T.TranscriptionID = M.TranscriptionID where  T.BillAccID = '" & BillAccID & "' and T.SubActID = '" & DRRec1("SubActID").ToString & "' Group by M.dictatorID)T1 "
                                            Dim SQLCmd2 As New SqlCommand(strQuery, New SqlConnection(strConn))
                                            Try
                                                SQLCmd2.Connection.Open()
                                                Dim DRRec2 As SqlDataReader = SQLCmd2.ExecuteReader()
                                                If DRRec2.HasRows Then
                                                    If DRRec2.Read Then
                                                        'DLines += CInt(FormatNumber(DRRec1("stdLines"), 0))
                                                        'DSLines += CInt(FormatNumber(DRRec1("stdSLines"), 0))
                                                        'BillUnits += CInt(FormatNumber(DRRec1("BillUnits"), 0))
                                                        'BillSUnits += CInt(FormatNumber(DRRec1("BillSUnits"), 0))
                                                        'BillAmt += CDbl(FormatNumber(DRRec1("Rate") * DRRec2("NumDict"), 2))
                                                        ' Cell5.Text = CInt(FormatNumber(DRRec1("stdLines"), 0))
                                                        ' Cell6.Text = CInt(FormatNumber(DRRec1("stdSLines"), 0))

                                                        NumDict = CInt(DRRec2("NumDict"))
                                                        BillAmount = FormatNumber(NumDict * DvRate, 2)
                                                        Cell1.Text = Billdate
                                                        Cell2.Text = "MT-B"
                                                        Cell3.Text = "<a href='ActBillReport.aspx?DictDetails=True&sBillAccID=" & BillAccID & "&sSubActID=" & DRRec1("SubActID").ToString & "&sMode=" & Mode & "&sSubActName=" & SubActName & "'>Previous Period Transcription Activity (Device: " & SubActName & ")</a>"
                                                        Cell4.HorizontalAlign = HorizontalAlign.Right
                                                        Cell5.HorizontalAlign = HorizontalAlign.Right
                                                        Cell6.HorizontalAlign = HorizontalAlign.Right

                                                        Cell4.Text = NumDict
                                                        Cell5.Text = "$ " & FormatNumber(DvRate, 6)
                                                        Cell6.Text = "$ " & FormatNumber(NumDict * DvRate, 2)
                                                        Row1.CssClass = "tblbg2"
                                                        Row1.Cells.Add(Cell1)
                                                        Row1.Cells.Add(Cell2)
                                                        Row1.Cells.Add(Cell3)
                                                        Row1.Cells.Add(Cell4)
                                                        Row1.Cells.Add(Cell5)
                                                        Row1.Cells.Add(Cell6)
                                                        tblMins.Rows.Add(Row1)


                                                    End If

                                                End If
                                            Finally
                                                If SQLCmd2.Connection.State = System.Data.ConnectionState.Open Then
                                                    SQLCmd2.Connection.Close()
                                                    SQLCmd2 = Nothing
                                                End If
                                            End Try
                                        Else
                                            If SubActName = "Telephone" Then
                                                BillAmount = FormatNumber(BillUnits * DvRate, 2)
                                                Cell1.Text = Billdate
                                                Cell2.Text = "MT-B"
                                                Cell3.Text = "<a href='ActBillReport.aspx?DictDetails=True&sBillAccID=" & BillAccID & "&sSubActID=" & DRRec1("SubActID").ToString & "&sMode=" & Mode & "&sSubActName=" & SubActName & "'>Previous Period Transcription Activity (Device: " & SubActName & ")</a>"
                                                Cell4.HorizontalAlign = HorizontalAlign.Right
                                                Cell5.HorizontalAlign = HorizontalAlign.Right
                                                Cell6.HorizontalAlign = HorizontalAlign.Right

                                                Cell4.Text = BillUnits
                                                Cell5.Text = "$ " & FormatNumber(DvRate, 6)
                                                Cell6.Text = "$ " & FormatNumber(BillUnits * DvRate, 2)
                                            Else
                                                BillAmount = FormatNumber(BillUnits * MiscRate, 2)
                                                Cell1.Text = Billdate
                                                Cell2.Text = "MT-B"
                                                Cell3.Text = "<a href='ActBillReport.aspx?DictDetails=True&sBillAccID=" & BillAccID & "&sSubActID=" & DRRec1("SubActID").ToString & "&sMode=" & Mode & "&sSubActName=" & SubActName & "'>Previous Period Transcription Activity (Device: " & SubActName & ")</a>"
                                                Cell4.HorizontalAlign = HorizontalAlign.Right
                                                Cell5.HorizontalAlign = HorizontalAlign.Right
                                                Cell6.HorizontalAlign = HorizontalAlign.Right

                                                Cell4.Text = BillUnits
                                                Cell5.Text = "$ " & FormatNumber(MiscRate, 6)
                                                Cell6.Text = "$ " & FormatNumber(BillUnits * MiscRate, 2)
                                            End If
                                            Row1.CssClass = "tblbg2"
                                            Row1.Cells.Add(Cell1)
                                            Row1.Cells.Add(Cell2)
                                            Row1.Cells.Add(Cell3)
                                            Row1.Cells.Add(Cell4)
                                            Row1.Cells.Add(Cell5)
                                            Row1.Cells.Add(Cell6)
                                            tblMins.Rows.Add(Row1)
                                            If BillSUnits > 0 Then
                                                'Response.Write(BillSUnits)
                                                BillAmount += FormatNumber(BillSUnits * StatRate, 2)
                                                sCell1.Text = Billdate
                                                sCell2.Text = "MT-S"
                                                sCell3.Text = "<a href='ActBillReport.aspx?DictDetails=True&sBillAccID=" & BillAccID & "&sSubActID=" & DRRec1("SubActID").ToString & "&sMode=" & Mode & "&sSubActName=" & SubActName & "&sPriority=True'>Previous Period Transcription Activity - STAT Lines (Device: " & SubActName & ")</a>"
                                                sCell4.HorizontalAlign = HorizontalAlign.Right
                                                sCell5.HorizontalAlign = HorizontalAlign.Right
                                                sCell6.HorizontalAlign = HorizontalAlign.Right

                                                sCell4.Text = BillSUnits
                                                sCell5.Text = "$ " & FormatNumber(StatRate, 6)
                                                sCell6.Text = "$ " & FormatNumber(BillSUnits * StatRate, 2)
                                                sRow1.CssClass = "tblbg2"
                                                sRow1.Cells.Add(sCell1)
                                                sRow1.Cells.Add(sCell2)
                                                sRow1.Cells.Add(sCell3)
                                                sRow1.Cells.Add(sCell4)
                                                sRow1.Cells.Add(sCell5)
                                                sRow1.Cells.Add(sCell6)
                                                tblMins.Rows.Add(sRow1)
                                            End If
                                        End If

                                    ElseIf Mode = "LC" Then
                                        If DRRec1("MethodName").ToString = "PerDictator" Then
                                            strQuery = "Select count(*) as NumDict from AdminSecureweb.dbo.AssignDic where GrpDicID = '" & DRRec1("SubActID").ToString & "'  "
                                            'strQuery = "Select count(*) as NumDict from (Select M.DictatorID as Numdict from AdminSecureweb.dbo.tblTranscriptionClientMain M INNER Join tblBillingLines T ON T.TranscriptionID = M.TranscriptionID where  T.BillAccID = '" & BillAccID & "' and T.SubActID = '" & DRRec1("SubActID").ToString & "' Group by M.dictatorID)T1 "
                                            Dim SQLCmd2 As New SqlCommand(strQuery, New SqlConnection(strConn))
                                            Try
                                                SQLCmd2.Connection.Open()
                                                Dim DRRec2 As SqlDataReader = SQLCmd2.ExecuteReader()
                                                If DRRec2.HasRows Then
                                                    If DRRec2.Read Then
                                                        'DLines += CInt(FormatNumber(DRRec1("stdLines"), 0))
                                                        'DSLines += CInt(FormatNumber(DRRec1("stdSLines"), 0))
                                                        'BillUnits += CInt(FormatNumber(DRRec1("BillUnits"), 0))
                                                        'BillSUnits += CInt(FormatNumber(DRRec1("BillSUnits"), 0))
                                                        'BillAmt += CDbl(FormatNumber(DRRec1("Rate") * DRRec2("NumDict"), 2))
                                                        ' Cell5.Text = CInt(FormatNumber(DRRec1("stdLines"), 0))
                                                        ' Cell6.Text = CInt(FormatNumber(DRRec1("stdSLines"), 0))

                                                        NumDict = CInt(DRRec2("NumDict"))
                                                        BillAmount = FormatNumber(NumDict * DvRate, 2)
                                                        Cell1.Text = Billdate
                                                        Cell2.Text = "MT-B"
                                                        Cell3.Text = "<a href='ActBillReport.aspx?DictDetails=True&sBillAccID=" & BillAccID & "&sSubActID=" & DRRec1("SubActID").ToString & "&sMode=" & Mode & "&sSubActName=" & SubActName & "'>Previous Period Transcription Activity (Location: " & SubActName & ")</a>"
                                                        Cell4.HorizontalAlign = HorizontalAlign.Right
                                                        Cell5.HorizontalAlign = HorizontalAlign.Right
                                                        Cell6.HorizontalAlign = HorizontalAlign.Right

                                                        Cell4.Text = NumDict
                                                        Cell5.Text = "$ " & FormatNumber(DvRate, 6)
                                                        Cell6.Text = "$ " & FormatNumber(NumDict * DvRate, 2)
                                                        Row1.CssClass = "tblbg2"
                                                        Row1.Cells.Add(Cell1)
                                                        Row1.Cells.Add(Cell2)
                                                        Row1.Cells.Add(Cell3)
                                                        Row1.Cells.Add(Cell4)
                                                        Row1.Cells.Add(Cell5)
                                                        Row1.Cells.Add(Cell6)
                                                        tblMins.Rows.Add(Row1)


                                                    End If

                                                End If
                                            Finally
                                                If SQLCmd2.Connection.State = System.Data.ConnectionState.Open Then
                                                    SQLCmd2.Connection.Close()
                                                    SQLCmd2 = Nothing
                                                End If
                                            End Try
                                        Else
                                            BillAmount = FormatNumber(BillUnits * DvRate, 2)
                                            Cell1.Text = Billdate
                                            Cell2.Text = "MT-B"
                                            Cell3.Text = "<a href='ActBillReport.aspx?DictDetails=True&sBillAccID=" & BillAccID & "&sSubActID=" & DRRec1("SubActID").ToString & "&sMode=" & Mode & "&sSubActName=" & SubActName & "'>Previous Period Transcription Activity (Location: " & SubActName & ")</a>"
                                            Cell4.HorizontalAlign = HorizontalAlign.Right
                                            Cell5.HorizontalAlign = HorizontalAlign.Right
                                            Cell6.HorizontalAlign = HorizontalAlign.Right

                                            Cell4.Text = BillUnits
                                            Cell5.Text = "$ " & FormatNumber(DvRate, 6)
                                            Cell6.Text = "$ " & FormatNumber(BillUnits * DvRate, 2)
                                            Row1.CssClass = "tblbg2"
                                            Row1.Cells.Add(Cell1)
                                            Row1.Cells.Add(Cell2)
                                            Row1.Cells.Add(Cell3)
                                            Row1.Cells.Add(Cell4)
                                            Row1.Cells.Add(Cell5)
                                            Row1.Cells.Add(Cell6)
                                            tblMins.Rows.Add(Row1)
                                            If BillSUnits > 0 Then
                                                'Response.Write(BillSUnits)
                                                BillAmount += FormatNumber(BillSUnits * StatRate, 2)
                                                sCell1.Text = Billdate
                                                sCell2.Text = "MT-S"
                                                sCell3.Text = "<a href='ActBillReport.aspx?DictDetails=True&sBillAccID=" & BillAccID & "&sSubActID=" & DRRec1("SubActID").ToString & "&sMode=" & Mode & "&sSubActName=" & SubActName & "&sPriority=True'>Previous Period Transcription Activity - STAT Lines (Location: " & SubActName & ")</a>"
                                                sCell4.HorizontalAlign = HorizontalAlign.Right
                                                sCell5.HorizontalAlign = HorizontalAlign.Right
                                                sCell6.HorizontalAlign = HorizontalAlign.Right

                                                sCell4.Text = BillSUnits
                                                sCell5.Text = "$ " & FormatNumber(StatRate, 6)
                                                sCell6.Text = "$ " & FormatNumber(BillSUnits * StatRate, 2)
                                                sRow1.CssClass = "tblbg2"
                                                sRow1.Cells.Add(sCell1)
                                                sRow1.Cells.Add(sCell2)
                                                sRow1.Cells.Add(sCell3)
                                                sRow1.Cells.Add(sCell4)
                                                sRow1.Cells.Add(sCell5)
                                                sRow1.Cells.Add(sCell6)
                                                tblMins.Rows.Add(sRow1)
                                            End If
                                        End If
                                    ElseIf Mode = "TT" Then
                                        If DRRec1("MethodName").ToString = "PerDictator" Then
                                            strQuery = "Select count(*) as NumDict from AdminSecureweb.dbo.AssignDic where GrpDicID = '" & DRRec1("SubActID").ToString & "'  "
                                            'strQuery = "Select count(*) as NumDict from (Select M.DictatorID as Numdict from AdminSecureweb.dbo.tblTranscriptionClientMain M INNER Join tblBillingLines T ON T.TranscriptionID = M.TranscriptionID where  T.BillAccID = '" & BillAccID & "' and T.SubActID = '" & DRRec1("SubActID").ToString & "' Group by M.dictatorID)T1 "
                                            Dim SQLCmd2 As New SqlCommand(strQuery, New SqlConnection(strConn))
                                            Try
                                                SQLCmd2.Connection.Open()
                                                Dim DRRec2 As SqlDataReader = SQLCmd2.ExecuteReader()
                                                If DRRec2.HasRows Then
                                                    If DRRec2.Read Then
                                                        'DLines += CInt(FormatNumber(DRRec1("stdLines"), 0))
                                                        'DSLines += CInt(FormatNumber(DRRec1("stdSLines"), 0))
                                                        'BillUnits += CInt(FormatNumber(DRRec1("BillUnits"), 0))
                                                        'BillSUnits += CInt(FormatNumber(DRRec1("BillSUnits"), 0))
                                                        'BillAmt += CDbl(FormatNumber(DRRec1("Rate") * DRRec2("NumDict"), 2))
                                                        ' Cell5.Text = CInt(FormatNumber(DRRec1("stdLines"), 0))
                                                        ' Cell6.Text = CInt(FormatNumber(DRRec1("stdSLines"), 0))

                                                        NumDict = CInt(DRRec2("NumDict"))
                                                        BillAmount = FormatNumber(NumDict * DvRate, 2)
                                                        Cell1.Text = Billdate
                                                        Cell2.Text = "MT-B"
                                                        Cell3.Text = "<a href='ActBillReport.aspx?DictDetails=True&sBillAccID=" & BillAccID & "&sSubActID=" & DRRec1("SubActID").ToString & "&sMode=" & Mode & "&sSubActName=" & SubActName & "'>Previous Period Transcription Activity (Location: " & SubActName & ")</a>"
                                                        Cell4.HorizontalAlign = HorizontalAlign.Right
                                                        Cell5.HorizontalAlign = HorizontalAlign.Right
                                                        Cell6.HorizontalAlign = HorizontalAlign.Right

                                                        Cell4.Text = NumDict
                                                        Cell5.Text = "$ " & FormatNumber(DvRate, 6)
                                                        Cell6.Text = "$ " & FormatNumber(NumDict * DvRate, 2)
                                                        Row1.CssClass = "tblbg2"
                                                        Row1.Cells.Add(Cell1)
                                                        Row1.Cells.Add(Cell2)
                                                        Row1.Cells.Add(Cell3)
                                                        Row1.Cells.Add(Cell4)
                                                        Row1.Cells.Add(Cell5)
                                                        Row1.Cells.Add(Cell6)
                                                        tblMins.Rows.Add(Row1)


                                                    End If

                                                End If
                                            Finally
                                                If SQLCmd2.Connection.State = System.Data.ConnectionState.Open Then
                                                    SQLCmd2.Connection.Close()
                                                    SQLCmd2 = Nothing
                                                End If
                                            End Try
                                        Else
                                            BillAmount = FormatNumber(BillUnits * DvRate, 2)
                                            Cell1.Text = Billdate
                                            Cell2.Text = "MT-B"
                                            Cell3.Text = "<a href='ActBillReport.aspx?DictDetails=True&sBillAccID=" & BillAccID & "&sSubActID=" & DRRec1("SubActID").ToString & "&sMode=" & Mode & "&sSubActName=" & SubActName & "'>Previous Period Transcription Activity (TAT: " & SubActName & " hrs)</a>"
                                            Cell4.HorizontalAlign = HorizontalAlign.Right
                                            Cell5.HorizontalAlign = HorizontalAlign.Right
                                            Cell6.HorizontalAlign = HorizontalAlign.Right

                                            Cell4.Text = BillUnits
                                            Cell5.Text = "$ " & FormatNumber(DvRate, 6)
                                            Cell6.Text = "$ " & FormatNumber(BillUnits * DvRate, 2)
                                            Row1.CssClass = "tblbg2"
                                            Row1.Cells.Add(Cell1)
                                            Row1.Cells.Add(Cell2)
                                            Row1.Cells.Add(Cell3)
                                            Row1.Cells.Add(Cell4)
                                            Row1.Cells.Add(Cell5)
                                            Row1.Cells.Add(Cell6)
                                            tblMins.Rows.Add(Row1)
                                            If BillSUnits > 0 Then
                                                'Response.Write(BillSUnits)
                                                BillAmount += FormatNumber(BillSUnits * StatRate, 2)
                                                sCell1.Text = Billdate
                                                sCell2.Text = "MT-S"
                                                sCell3.Text = "<a href='ActBillReport.aspx?DictDetails=True&sBillAccID=" & BillAccID & "&sSubActID=" & DRRec1("SubActID").ToString & "&sMode=" & Mode & "&sSubActName=" & SubActName & "&sPriority=True'>Previous Period Transcription Activity - STAT Lines (TAT: " & SubActName & " hrs)</a>"
                                                sCell4.HorizontalAlign = HorizontalAlign.Right
                                                sCell5.HorizontalAlign = HorizontalAlign.Right
                                                sCell6.HorizontalAlign = HorizontalAlign.Right

                                                sCell4.Text = BillSUnits
                                                sCell5.Text = "$ " & FormatNumber(StatRate, 6)
                                                sCell6.Text = "$ " & FormatNumber(BillSUnits * StatRate, 2)
                                                sRow1.CssClass = "tblbg2"
                                                sRow1.Cells.Add(sCell1)
                                                sRow1.Cells.Add(sCell2)
                                                sRow1.Cells.Add(sCell3)
                                                sRow1.Cells.Add(sCell4)
                                                sRow1.Cells.Add(sCell5)
                                                sRow1.Cells.Add(sCell6)
                                                tblMins.Rows.Add(sRow1)
                                            End If
                                        End If


                                    ElseIf Mode = "DC" Or Mode = "TW" Then
                                        If DRRec1("MethodName").ToString = "PerDictator" Then
                                            strQuery = "Select count(*) as NumDict from AdminSecureweb.dbo.AssignDic where GrpDicID = '" & DRRec1("SubActID").ToString & "'  "
                                            'strQuery = "Select count(*) as NumDict from (Select M.DictatorID as Numdict from AdminSecureweb.dbo.tblTranscriptionClientMain M INNER Join tblBillingLines T ON T.TranscriptionID = M.TranscriptionID where  T.BillAccID = '" & BillAccID & "' and T.SubActID = '" & DRRec1("SubActID").ToString & "' Group by M.dictatorID)T1 "
                                            Dim SQLCmd2 As New SqlCommand(strQuery, New SqlConnection(strConn))
                                            Try
                                                SQLCmd2.Connection.Open()
                                                Dim DRRec2 As SqlDataReader = SQLCmd2.ExecuteReader()
                                                If DRRec2.HasRows Then
                                                    If DRRec2.Read Then
                                                        'DLines += CInt(FormatNumber(DRRec1("stdLines"), 0))
                                                        'DSLines += CInt(FormatNumber(DRRec1("stdSLines"), 0))
                                                        'BillUnits += CInt(FormatNumber(DRRec1("BillUnits"), 0))
                                                        'BillSUnits += CInt(FormatNumber(DRRec1("BillSUnits"), 0))
                                                        'BillAmt += CDbl(FormatNumber(DRRec1("Rate") * DRRec2("NumDict"), 2))
                                                        ' Cell5.Text = CInt(FormatNumber(DRRec1("stdLines"), 0))
                                                        ' Cell6.Text = CInt(FormatNumber(DRRec1("stdSLines"), 0))

                                                        NumDict = CInt(DRRec2("NumDict"))
                                                        BillAmount = FormatNumber(NumDict * DvRate, 2)
                                                        Cell1.Text = Billdate
                                                        Cell2.Text = "MT-B"
                                                        Cell3.Text = "<a href='ActBillReport.aspx?DictDetails=True&sBillAccID=" & BillAccID & "&sSubActID=" & DRRec1("SubActID").ToString & "&sMode=" & Mode & "&sSubActName=" & SubActName & "'>Previous Period Transcription Activity (Group Name: " & SubActName & ")</a>"
                                                        Cell4.HorizontalAlign = HorizontalAlign.Right
                                                        Cell5.HorizontalAlign = HorizontalAlign.Right
                                                        Cell6.HorizontalAlign = HorizontalAlign.Right

                                                        Cell4.Text = NumDict
                                                        Cell5.Text = "$ " & FormatNumber(DvRate, 6)
                                                        Cell6.Text = "$ " & FormatNumber(NumDict * DvRate, 2)
                                                        Row1.CssClass = "tblbg2"
                                                        Row1.Cells.Add(Cell1)
                                                        Row1.Cells.Add(Cell2)
                                                        Row1.Cells.Add(Cell3)
                                                        Row1.Cells.Add(Cell4)
                                                        Row1.Cells.Add(Cell5)
                                                        Row1.Cells.Add(Cell6)
                                                        tblMins.Rows.Add(Row1)


                                                    End If

                                                End If
                                            Finally
                                                If SQLCmd2.Connection.State = System.Data.ConnectionState.Open Then
                                                    SQLCmd2.Connection.Close()
                                                    SQLCmd2 = Nothing
                                                End If
                                            End Try
                                        Else
                                            BillAmount = FormatNumber(BillUnits * DvRate, 2)
                                            Cell1.Text = Billdate
                                            Cell2.Text = "MT-B"
                                            Cell3.Text = "<a href='ActBillReport.aspx?DictDetails=True&sBillAccID=" & BillAccID & "&sSubActID=" & DRRec1("SubActID").ToString & "&sMode=" & Mode & "&sSubActName=" & SubActName & "'>Previous Period Transcription Activity (Group Name: " & SubActName & ")</a>"
                                            Cell4.HorizontalAlign = HorizontalAlign.Right
                                            Cell5.HorizontalAlign = HorizontalAlign.Right
                                            Cell6.HorizontalAlign = HorizontalAlign.Right

                                            Cell4.Text = BillUnits
                                            Cell5.Text = "$ " & FormatNumber(DvRate, 6)
                                            Cell6.Text = "$ " & FormatNumber(BillUnits * DvRate, 2)
                                            Row1.CssClass = "tblbg2"
                                            Row1.Cells.Add(Cell1)
                                            Row1.Cells.Add(Cell2)
                                            Row1.Cells.Add(Cell3)
                                            Row1.Cells.Add(Cell4)
                                            Row1.Cells.Add(Cell5)
                                            Row1.Cells.Add(Cell6)
                                            tblMins.Rows.Add(Row1)
                                            If BillSUnits > 0 Then
                                                'Response.Write(BillSUnits)
                                                BillAmount += FormatNumber(BillSUnits * StatRate, 2)
                                                sCell1.Text = Billdate
                                                sCell2.Text = "MT-S"
                                                sCell3.Text = "<a href='ActBillReport.aspx?DictDetails=True&sBillAccID=" & BillAccID & "&sSubActID=" & DRRec1("SubActID").ToString & "&sMode=" & Mode & "&sPriority=True&sSubActName=" & SubActName & "'>Previous Period Transcription Activity - STAT Lines (Group Name: " & SubActName & ")</a>"
                                                sCell4.HorizontalAlign = HorizontalAlign.Right
                                                sCell5.HorizontalAlign = HorizontalAlign.Right
                                                sCell6.HorizontalAlign = HorizontalAlign.Right

                                                sCell4.Text = BillSUnits
                                                sCell5.Text = "$ " & FormatNumber(StatRate, 6)
                                                sCell6.Text = "$ " & FormatNumber(BillSUnits * StatRate, 2)
                                                sRow1.CssClass = "tblbg2"
                                                sRow1.Cells.Add(sCell1)
                                                sRow1.Cells.Add(sCell2)
                                                sRow1.Cells.Add(sCell3)
                                                sRow1.Cells.Add(sCell4)
                                                sRow1.Cells.Add(sCell5)
                                                sRow1.Cells.Add(sCell6)
                                                tblMins.Rows.Add(sRow1)
                                            End If
                                        End If


                                    End If
                                    BillTotAmount += BillAmount
                                End While
                            End If
                            DRRec1.Close()
                        Finally
                            If SQLCmd1.Connection.State = System.Data.ConnectionState.Open Then
                                SQLCmd1.Connection.Close()
                                SQLCmd1 = Nothing
                            End If
                        End Try

                        If ActBillCycle = True Then
                            If strBillCycle = "Cycle1" Then
                                VasStartDate = C1SDate
                                VasEndDate = C1EDate.AddDays(-1)
                            ElseIf strBillCycle = "Cycle2" Then
                                VasStartDate = C2SDate
                                VasEndDate = C2EDate
                            End If
                        Else
                            If strBillCycle = "Cycle2" Then
                                VasStartDate = C1SDate
                                VasEndDate = C2EDate
                            End If
                        End If

                        strQuery = "Select IT.Mode, ID.Item, IT.Descr, IT.Quantity, IT.Amount, IT.Totamount, IT.ServiceDate  from AdminSecureweb.dbo.tblInvItemDet IT Left Outer Join AdminSecureweb.dbo.ItemDetails ID ON IT.ItemID = ID.ItemID where IT.Servicedate between  '" & VasStartDate & "' and '" & VasEndDate & "' and AccountID='" & AccountID & "' and IT.Mode='VAS' "

                        Dim SQLCmd3 As New SqlCommand(strQuery, New SqlConnection(strConn))
                        Try
                            SQLCmd3.Connection.Open()
                            Dim DRRec3 As SqlDataReader = SQLCmd3.ExecuteReader()
                            If DRRec3.HasRows Then
                                While DRRec3.Read
                                    Dim vRow1 As New TableRow
                                    Dim vCell1 As New TableCell
                                    Dim vCell2 As New TableCell
                                    Dim vCell3 As New TableCell
                                    Dim vCell4 As New TableCell
                                    Dim vCell5 As New TableCell
                                    Dim vCell6 As New TableCell
                                    Billdate = DRRec3("ServiceDate").ToString
                                    vCell1.Text = Billdate.ToShortDateString
                                    vCell2.Text = DRRec3("Item").ToString
                                    vCell3.Text = DRRec3("Descr").ToString
                                    vCell4.HorizontalAlign = HorizontalAlign.Right
                                    vCell5.HorizontalAlign = HorizontalAlign.Right
                                    vCell6.HorizontalAlign = HorizontalAlign.Right

                                    vCell4.Text = DRRec3("Quantity").ToString
                                    vCell5.Text = "$ " & FormatNumber(DRRec3("Amount").ToString, 6)
                                    vCell6.Text = "$ " & FormatNumber(DRRec3("Totamount").ToString, 2)
                                    BillAmount = CDbl(FormatNumber(DRRec3("Totamount").ToString, 2))
                                    vRow1.CssClass = "tblbg2"
                                    vRow1.Cells.Add(vCell1)
                                    vRow1.Cells.Add(vCell2)
                                    vRow1.Cells.Add(vCell3)
                                    vRow1.Cells.Add(vCell4)
                                    vRow1.Cells.Add(vCell5)
                                    vRow1.Cells.Add(vCell6)
                                    tblMins.Rows.Add(vRow1)
                                    BillTotAmount += BillAmount
                                End While
                            End If
                            DRRec3.Close()
                        Finally
                            If SQLCmd3.Connection.State = System.Data.ConnectionState.Open Then
                                SQLCmd3.Connection.Close()
                                SQLCmd3 = Nothing
                            End If
                        End Try


                    End If
                    Dim A11Row1 As New TableRow
                    Dim A11Cell1 As New TableCell
                    Dim A11Cell2 As New TableCell
                    Dim A11Cell3 As New TableCell
                    Dim A11Cell4 As New TableCell
                    Dim A11Cell5 As New TableCell
                    Dim A11Cell6 As New TableCell
                    Dim A11Cell7 As New TableCell
                    Dim A11Cell8 As New TableCell
                    Dim A11Cell9 As New TableCell


                    A11Row1.HorizontalAlign = HorizontalAlign.Center
                    A11Row1.CssClass = "tblbg2"
                    A11Row1.Font.Bold = True
                    A11Cell1.ColumnSpan = 4
                    A11Cell1.HorizontalAlign = HorizontalAlign.Right
                    A11Cell1.Text = ""
                    A11Cell2.Text = "Total"
                    A11Cell3.HorizontalAlign = HorizontalAlign.Right
                    A11Cell3.Text = "$ " & FormatNumber(BillTotAmount, 2)
                    A11Row1.Cells.Add(A11Cell1)
                    A11Row1.Cells.Add(A11Cell2)
                    A11Row1.Cells.Add(A11Cell3)
                    tblMins.Rows.Add(A11Row1)
                End While
            End If
        Finally
            If SQLCmd.Connection.State = System.Data.ConnectionState.Open Then
                SQLCmd.Connection.Close()
                SQLCmd = Nothing
            End If
        End Try
    End Sub

   
End Class

