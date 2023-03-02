Imports System.Data.Sqlclient
Imports System.Data
Partial Class MIS_DailyMins
    Inherits BasePage
    Protected Sub ShowActDetails()
        Dim strConn As String
        Dim Mode As String
        Dim strDate As String
        Dim strCategory As String
        Dim strCycle As String
        Dim BillAccID As String
        strCategory = ""
        Dim InpDate As Date
        strDate = DLMonth.SelectedValue & "/1/" & DLYear.SelectedValue
        InpDate = Date.Parse(strDate)
        Dim C1SDate As Date
        Dim C1EDate As Date
        Dim C2SDate As Date
        Dim C2EDate As Date
        Dim minDate As Date
        Dim MonthSel As Boolean = False
        C1SDate = Month(InpDate) & "/1/" & Year(InpDate)
        C2SDate = Month(InpDate) & "/16/" & Year(InpDate)
        C1EDate = Month(InpDate) & "/16/" & Year(InpDate)
        C2EDate = DateAdd(DateInterval.Day, -1, DateAdd(DateInterval.Month, 1, C1SDate))
        If DLCycle.SelectedValue = "1" Then
            tblDtls.Text = "Billing Report (" & C1SDate & " - " & C1EDate & ")"
            minDate = C1EDate.AddDays(-1)
            strCycle = "Cycle1"
        ElseIf DLCycle.SelectedValue = "2" Then
            tblDtls.Text = "Summary (" & C1SDate & " - " & C2EDate & ")"
            minDate = C2EDate
            strCycle = "Cycle2"
            strCycle = "Cycle1"
        Else
            tblDtls.Text = "Summary (" & C1SDate & " - " & C2EDate & ")"
            minDate = C2EDate
            strCycle = "Month"
            MonthSel = True
        End If
        Dim t2 As New Table
        t2.Style("width") = "100%"
        't2.BorderWidth = 2
        't2.GridLines = GridLines.Both
        Dim I As Integer
        I = 0
        strConn = System.Configuration.ConfigurationManager.AppSettings("ETSCon")
        Dim InvBillAmount As Double
        InvBillAmount = 0.0
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
        Dim ActOthCharges As Double
        Dim InvCode As String
        Dim minbilling As Double
        Dim minCharges As Double
        Dim RemBalance As Double
        RemBalance = "0.00"
        minbilling = "0.00"
        minCharges = "0.00"
        STSTDLines = 0
        STSTDSLines = 0
        STBillUnits = 0
        STBillSUnits = 0
        STBillAmt = 0
        STBillOtherAmt = 0
        STBillTotAmount = 0
        Dim TSTDLines As String
        Dim TSTDSLines As String
        Dim TBillUnits As String
        Dim TBillSUnits As String
        Dim TBillAmt As String
        Dim TBillOtherAmt As String
        Dim TBillTotAmount As String
        Dim InvoiceID As String
        Dim strBillCycle As String
        Dim ActBillCycle As Boolean = False
        TSTDLines = 0
        TSTDSLines = 0
        TBillUnits = 0
        TBillSUnits = 0
        TBillAmt = 0
        TBillOtherAmt = 0
        TBillTotAmount = 0
        strQuery = "Select A.Accountid, ISNULL(IsDiscountEnabled,0) AS IsDiscountEnabled, CASE WHEN D1.DiscCharges IS NULL THEN 0 ELSE 1 END AS IsDiscCharged1, CASE WHEN D2.DiscCharges IS NULL THEN 0 ELSE 1 END AS IsDiscCharged2, ISNULL(A.Discount, 0) AS Discount , A.Indirect, IsNull(V1.OthCharges,0) as ActOthCharges1, IsNull(V2.OthCharges,0) as ActOthCharges2, IsNull(M1.minCharges,0) as MinCharges1,IsNull(M2.minCharges,0) as MinCharges2, BA.Mode, A.AccountName as Description, A.BillActNumber, C.priority, C.Description as CateDescr, BA.MinBilling, BA.BillAccID, BA.Posted, I.Amount as InvAmount, I.trackID, I.InvCode, I.InvRecFound, BA.BillCycle, BA.Cycle    "
        strQuery = strQuery & " from tblaccounts A "
        strQuery = strQuery & " INNER JOIN (Select * from AdminSecureweb.dbo.tblBillAccounts) BA"
        strQuery = strQuery & " on BA.AccountID = A.AccountID"
        strQuery = strQuery & " LEFT OUTER JOIN (Select * from tblActCategories) C"
        strQuery = strQuery & " on C.category = A.Category"
        strQuery = strQuery & " LEFT OUTER JOIN (Select TrackId, InvCode, AccID, Amount, 'Yes' as InvRecFound from AdminSecureweb.dbo.InvupData where InvType ='Invoice' and BillMonth='" & DLMonth.SelectedValue & "' and Billyear='" & DLYear.SelectedValue & "' and BillCycle='Cycle" & DLCycle.SelectedValue & "' ) I "
        strQuery = strQuery & " on I.AccID = BA.AccountID"
        strQuery = strQuery & " LEFT OUTER JOIN (Select AccountID, sum(totamount) as OthCharges from AdminSecureweb.dbo.tblInvItemDet where Mode = 'VAS' and Servicedate >=  '" & C1SDate & "' and Servicedate < '" & C2SDate & "'  Group By AccountID) V1  on V1.accountid = A.Accountid"
        strQuery = strQuery & " LEFT OUTER JOIN (Select AccountID, sum(totamount) as OthCharges from AdminSecureweb.dbo.tblInvItemDet where Mode = 'VAS' and Servicedate between  '" & C1EDate & "' and '" & C2EDate & "'  Group By AccountID) V2  on V2.accountid = A.Accountid"
        strQuery = strQuery & " LEFT OUTER JOIN (Select AccountID, sum(totamount) as minCharges from AdminSecureweb.dbo.tblInvItemDet where ItemID='25c7d577-967e-48ab-a62d-87fb6f420be1' and Mode = 'VAS' and Servicedate >=  '" & C1SDate & "' and Servicedate < '" & C2SDate & "'   Group By AccountID) M1  on M1.accountid = A.Accountid"
        strQuery = strQuery & " LEFT OUTER JOIN (Select AccountID, sum(totamount) as minCharges from AdminSecureweb.dbo.tblInvItemDet where ItemID='25c7d577-967e-48ab-a62d-87fb6f420be1' and Mode = 'VAS' and Servicedate between  '" & C1EDate & "' and '" & C2EDate & "'  Group By AccountID) M2  on M2.accountid = A.Accountid"
        strQuery = strQuery & " LEFT OUTER JOIN (Select AccountID, sum(totamount) as DiscCharges from AdminSecureweb.dbo.tblInvItemDet where ItemID='48CFFC7C-0A43-4273-BEBF-FA7B9CA17D74' and IsAuto=1 and Mode = 'VAS' and Servicedate >=  '" & C1SDate & "' and Servicedate < '" & C2SDate & "'   Group By AccountID) D1  on D1.accountid = A.Accountid"
        strQuery = strQuery & " LEFT OUTER JOIN (Select AccountID, sum(totamount) as DiscCharges from AdminSecureweb.dbo.tblInvItemDet where ItemID='48CFFC7C-0A43-4273-BEBF-FA7B9CA17D74' and IsAuto=1 and Mode = 'VAS' and Servicedate between  '" & C1EDate & "' and '" & C2EDate & "'  Group By AccountID) D2  on D2.accountid = A.Accountid"
        If MonthSel = True Then
            strQuery = strQuery & "  where BA.BillMonth = '" & DLMonth.SelectedValue & "' and BA.BillYear='" & DLYear.SelectedValue & "' AND A.contractorid = '" & Session("contractorid").ToString & "'  Order by Priority, A.BillActNumber"
        Else
            strQuery = strQuery & "  where BA.BillMonth = '" & DLMonth.SelectedValue & "' and BA.BillYear='" & DLYear.SelectedValue & "' and BillCycle = '" & DLCycle.SelectedValue & "'   and A.contractorid = '" & Session("contractorid").ToString & "'  Order by Priority, A.BillActNumber "
        End If
        'strQuery = strQuery & "  UNION Select A.Accountid, A.Indirect, IsNull(V1.OthCharges,0) as ActOthCharges1, IsNull(V2.OthCharges,0) as ActOthCharges2, IsNull(M.minCharges,0) as MinCharges, BA.Mode, A.Description, A.BillActNumber, C.priority, C.Description as CateDescr, BA.MinBilling, BA.BillAccID, BA.Posted, I.Amount as InvAmount, I.trackID, I.InvCode, I.InvRecFound, BA.BillCycle, BA.Cycle    "
        'strQuery = strQuery & " from tblaccounts A "
        'strQuery = strQuery & " LEFT OUTER JOIN (Select * from AdminSecureweb.dbo.tblBillAccounts) BA"
        'strQuery = strQuery & " on BA.AccountID = A.AccountID"
        'strQuery = strQuery & " LEFT OUTER JOIN (Select * from tblActCategories) C"
        'strQuery = strQuery & " on C.category = A.Category"
        'strQuery = strQuery & " LEFT OUTER JOIN (Select TrackId, InvCode, AccID, Amount, 'Yes' as InvRecFound from AdminSecureweb.dbo.InvupData where InvType ='Invoice' and BillMonth='" & DLMonth.SelectedValue & "' and Billyear='" & DLYear.SelectedValue & "' and BillCycle='Cycle" & DLCycle.SelectedValue & "' ) I "
        'strQuery = strQuery & " on I.AccID = BA.AccountID"
        'strQuery = strQuery & " LEFT OUTER JOIN (Select AccountID, sum(totamount) as OthCharges from AdminSecureweb.dbo.tblInvItemDet where Mode = 'VAS' and Servicedate >=  '" & C1SDate & "' and Servicedate < '" & C2SDate & "' Group By AccountID) V1  on V1.accountid = A.Accountid"
        'strQuery = strQuery & " LEFT OUTER JOIN (Select AccountID, sum(totamount) as OthCharges from AdminSecureweb.dbo.tblInvItemDet where Mode = 'VAS' and Servicedate between  '" & C1EDate & "' and '" & C2EDate & "'  Group By AccountID) V2  on V2.accountid = A.Accountid"
        'strQuery = strQuery & " LEFT OUTER JOIN (Select AccountID, sum(totamount) as minCharges from AdminSecureweb.dbo.tblInvItemDet where ItemID='25c7d577-967e-48ab-a62d-87fb6f420be1' and Mode = 'VAS' and Servicedate between  '" & C1SDate & "' and '" & C2EDate & "'  Group By AccountID) M  on M.accountid = A.Accountid"
        'strQuery = strQuery & "  where  A.Indirect = 'True' Order by Priority, A.BillActNumber"
        'Response.Write(strQuery)
        'Response.End()
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
                    ActOthCharges = 0
                    I = I + 1
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
                    'Response.Write(DRRec("description").ToString & "#" & DRRec("BillCycle").ToString & "#" & DRRec("Cycle").ToString & "#" & strBillCycle & "#" & ActBillCycle & "$")
                    If ActBillCycle = True Then
                        If strBillCycle = "Cycle1" Then
                            ActOthCharges = DRRec("ActOthCharges1").ToString
                            minCharges = DRRec("minCharges1").ToString

                        ElseIf strBillCycle = "Cycle2" Then
                            ActOthCharges = DRRec("ActOthCharges2").ToString
                            minCharges = DRRec("minCharges2").ToString
                        End If
                    Else
                        If strBillCycle = "Cycle2" Then
                            ActOthCharges = CDbl(DRRec("ActOthCharges1").ToString) + CDbl(DRRec("ActOthCharges2").ToString)
                            minCharges = CDbl(DRRec("minCharges1").ToString) + CDbl(DRRec("minCharges2").ToString)

                        End If
                    End If
                    If I = 1 Then
                        strCategory = DRRec("CateDescr").ToString
                        Dim Row2 As New TableRow
                        Row2.HorizontalAlign = HorizontalAlign.Center
                        'Row2.CssClass = "tblbgbody"
                        'Row2.Font.Bold = True
                        'Row2.Font.Italic = True
                        'Row2.ForeColor = Drawing.Color.White
                        'Row2.Font.Size = "8"
                        Dim CatCell As New TableCell
                        CatCell.ColumnSpan = "14"
                        CatCell.Text = DRRec("CateDescr").ToString
                        CatCell.CssClass = "HeaderDiv"
                        Row2.Cells.Add(CatCell)
                        tblMins.Rows.Add(Row2)
                    Else
                        If strCategory <> DRRec("CateDescr").ToString Then
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
                            'ARow1.CssClass = "tblbg"
                            'ARow1.Font.Bold = True
                            'ARow1.Font.Size = "8"
                            ACell1.ColumnSpan = 4
                            ACell1.Text = "SubTotal"
                            ACell1.CssClass = "alt1"
                            ACell2.HorizontalAlign = HorizontalAlign.Right
                            ACell2.CssClass = "alt1"
                            ACell3.HorizontalAlign = HorizontalAlign.Right
                            ACell3.CssClass = "alt1"
                            ACell4.HorizontalAlign = HorizontalAlign.Right
                            ACell4.CssClass = "alt1"
                            ACell5.HorizontalAlign = HorizontalAlign.Right
                            ACell5.CssClass = "alt1"
                            ACell6.HorizontalAlign = HorizontalAlign.Right
                            ACell6.CssClass = "alt1"
                            ACell7.HorizontalAlign = HorizontalAlign.Right
                            ACell7.CssClass = "alt1"
                            ACell8.HorizontalAlign = HorizontalAlign.Right
                            ACell8.CssClass = "alt1"
                            ACell2.Text = FormatNumber(STSTDLines, 0)
                            ACell3.Text = FormatNumber(STSTDSLines, 0)
                            ACell4.Text = FormatNumber(STBillUnits, 0)
                            ACell5.Text = FormatNumber(STBillSUnits, 0)
                            ACell6.Text = FormatNumber(STBillAmt, 2)
                            ACell7.Text = FormatNumber(STBillOtherAmt, 2)
                            ACell8.Text = FormatNumber(STBillTotAmount, 2)
                            ACell9.Text = "-"
                            ACell9.CssClass = "alt1"
                            ACell10.Text = "-"
                            ACell10.CssClass = "alt1"
                            ACell11.Text = "-"
                            ACell11.CssClass = "alt1"
                            ACell12.Text = "-"
                            ACell12.CssClass = "alt1"
                            ACell13.Text = "-"
                            ACell13.CssClass = "alt1"
                            ACell14.Text = "-"
                            ACell14.CssClass = "alt1"
                            ACell15.Text = "-"
                            ACell15.CssClass = "alt1"
                            ACell16.Text = "-"
                            ACell16.CssClass = "alt1"
                            ACell18.Text = "-"
                            ACell18.CssClass = "alt1"

                            ARow1.Cells.Add(ACell1)
                            ARow1.Cells.Add(ACell2)
                            ARow1.Cells.Add(ACell3)
                            'ARow1.Cells.Add(ACell18)
                            ARow1.Cells.Add(ACell4)
                            ARow1.Cells.Add(ACell5)
                            ARow1.Cells.Add(ACell6)
                            ARow1.Cells.Add(ACell7)
                            ARow1.Cells.Add(ACell8)
                            ARow1.Cells.Add(ACell13)
                            ARow1.Cells.Add(ACell14)
                            ARow1.Cells.Add(ACell15)
                            'ARow1.Cells.Add(ACell16)
                            tblMins.Rows.Add(ARow1)
                            strCategory = DRRec("CateDescr").ToString
                            Dim Row2 As New TableRow
                            'Row2.HorizontalAlign = HorizontalAlign.Center
                            'Row2.CssClass = "tblbgbody"
                            'Row2.Font.Bold = True
                            'Row2.Font.Italic = True
                            'Row2.ForeColor = Drawing.Color.White
                            'Row2.Font.Size = "8"
                            Dim CatCell As New TableCell
                            CatCell.ColumnSpan = "14"
                            CatCell.Text = DRRec("CateDescr").ToString
                            CatCell.CssClass = "HeaderDiv"
                            Row2.Cells.Add(CatCell)
                            tblMins.Rows.Add(Row2)
                            STSTDLines = 0
                            STSTDSLines = 0
                            STBillUnits = 0
                            STBillSUnits = 0
                            STBillAmt = 0
                            STBillOtherAmt = 0
                            STBillTotAmount = 0
                        End If
                    End If
                    If DRRec("Indirect").ToString = "True" Then
                        strQuery = "Select  IsNULL(MTLines, 0) as MTLines , IsNULL(MTRate, 0.00) as MTRate, IsNULL(MTPLines, 0) as MTPLines, IsNULL(MTPRate, 0.00) as MTPRate, IsNULL(QALines, 0) as QALines, IsNULL(QARate, 0.00) as QARate,  IsNULL(MTSLines, 0) as MTSLines , IsNULL(MTSRate, 0.00) as MTSRate, IsNULL(MTPSLines, 0) as MTPSLines, IsNULL(MTPSRate, 0.00) as MTPSRate, IsNULL(QASLines, 0) as QASLines, IsNULL(QASRate, 0.00) as QASRate, IsNULL(STATLines, 0) as STATLines, IsNULL(CPL, 0) as CPL from AdminSecureweb.dbo.tblInDirActDetails where AccountID = '" & DRRec("AccountID").ToString & "' and BillMonth='" & DLMonth.SelectedValue & "' and BillYear ='" & DLYear.SelectedValue & "' and BillCycle='" & strCycle & "' "
                        Dim SQLCmdI As New SqlCommand(strQuery, New SqlConnection(strConn))
                        Try
                            SQLCmdI.Connection.Open()
                            Dim DRRecI As SqlDataReader = SQLCmdI.ExecuteReader()
                            If DRRecI.HasRows Then
                                If DRRecI.Read Then
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
                                    Dim Cell12 As New TableCell
                                    Dim Cell13 As New TableCell
                                    Dim Cell14 As New TableCell
                                    Dim Cell15 As New TableCell
                                    Dim Cell16 As New TableCell
                                    Dim Cell17 As New TableCell
                                    Dim Cell18 As New TableCell
                                    Row1.Font.Size = "10"
                                    DLines = DRRecI("CPL").ToString
                                    DLines = DRRecI("CPL").ToString
                                    BillUnits = CInt(DRRecI("MTLines").ToString) + CInt(DRRecI("MTPLines").ToString) + CInt(DRRecI("QALines").ToString)
                                    BillSUnits = CInt(DRRecI("MTSLines").ToString) + CInt(DRRecI("MTPSLines").ToString) + CInt(DRRecI("QASLines").ToString)
                                    BillAmt = (DRRecI("MTLines").ToString * DRRecI("MTRate").ToString) + (DRRecI("MTPLines").ToString * DRRecI("MTPRate").ToString) + (DRRecI("QALines").ToString * DRRecI("QARate").ToString)
                                    Cell1.HorizontalAlign = HorizontalAlign.Left
                                    Cell1.Text = "<A HREF='ActbillReport.aspx?IndAct=True&ActDetails=True&Billmonth=" & DLMonth.SelectedValue & "&BillYear=" & DLYear.SelectedValue & "&BillCycle=" & strCycle & "&AccID=" & DRRec("AccountID").ToString & "' Target=_Blank>" & DRRec("Description").ToString & "</a>"
                                    Cell2.Text = DRRec("BillActNumber").ToString

                                    Cell3.HorizontalAlign = HorizontalAlign.Right
                                    Cell4.HorizontalAlign = HorizontalAlign.Right
                                    Cell5.HorizontalAlign = HorizontalAlign.Right
                                    Cell6.HorizontalAlign = HorizontalAlign.Right
                                    Cell7.HorizontalAlign = HorizontalAlign.Right
                                    Cell8.HorizontalAlign = HorizontalAlign.Right
                                    Cell9.HorizontalAlign = HorizontalAlign.Right
                                    Cell3.Text = DLines
                                    Cell4.Text = DSLines
                                    Cell5.Text = BillUnits
                                    Cell6.Text = BillSUnits
                                    Cell7.Text = FormatNumber(BillAmt, 2)

                                    'If ActBillCycle = True Then
                                    '    If strBillCycle = "Cycle1" Then
                                    '        ActOthCharges = DRRec("ActOthCharges1").ToString
                                    '    ElseIf strBillCycle = "Cycle2" Then
                                    '        ActOthCharges = DRRec("ActOthCharges2").ToString
                                    '    End If
                                    'Else
                                    '    If strBillCycle = "Cycle2" Then
                                    '        ActOthCharges = DRRec("ActOthCharges1").ToString + DRRec("ActOthCharges2").ToString
                                    '    End If
                                    'End If
                                    Cell8.Text = FormatNumber(ActOthCharges, 2)
                                    InvBillAmount = FormatNumber(BillAmt + ActOthCharges, 2)
                                    Cell9.Text = FormatNumber(InvBillAmount, 2)
                                    '  Response.Write(DRRec("Posted").ToString & "#" & DRRec("InvRecFound").ToString)
                                    Cell10.Text = "Posted"
                                    Cell11.Text = "-"
                                    Cell12.Text = "-"
                                    Cell13.Text = "-"
                                    Cell18.Text = strBillCycle
                                    'Row1.CssClass = "tblbg2"
                                    'Cell1.CssClass = "alt3"
                                    'Cell2.CssClass = "alt1"
                                    'Cell14.CssClass = "alt1"
                                    'Cell18.CssClass = "alt1"
                                    'Cell3.CssClass = "alt1"
                                    'Cell4.CssClass = "alt1"
                                    'Cell5.CssClass = "alt1"
                                    'Cell6.CssClass = "alt1"
                                    'Cell7.CssClass = "alt1"
                                    'Cell8.CssClass = "alt1"
                                    'Cell9.CssClass = "alt1"
                                    'Cell10.CssClass = "alt1"
                                    'Cell11.CssClass = "alt1"
                                    'Cell12.CssClass = "alt1"

                                    Row1.Cells.Add(Cell1)
                                    Row1.Cells.Add(Cell2)
                                    Row1.Cells.Add(Cell14)
                                    Row1.Cells.Add(Cell18)
                                    Row1.Cells.Add(Cell3)
                                    Row1.Cells.Add(Cell4)
                                    Row1.Cells.Add(Cell5)
                                    Row1.Cells.Add(Cell6)
                                    Row1.Cells.Add(Cell7)
                                    Row1.Cells.Add(Cell8)
                                    Row1.Cells.Add(Cell9)
                                    Row1.Cells.Add(Cell10)
                                    Row1.Cells.Add(Cell11)
                                    Row1.Cells.Add(Cell12)

                                    'Row1.Cells.Add(Cell14)
                                    'Row1.Cells.Add(Cell15)
                                    'Row1.Cells.Add(Cell16)
                                    'Row1.Cells.Add(Cell17)
                                    tblMins.Rows.Add(Row1)
                                    STSTDLines += CInt(DLines)
                                    STSTDSLines += CInt(DSLines)
                                    STBillUnits += CInt(BillUnits)
                                    STBillSUnits += CInt(BillSUnits)
                                    STBillAmt += CDbl(BillAmt)
                                    STBillOtherAmt += CDbl(FormatNumber(ActOthCharges, 2))
                                    STBillTotAmount += CDbl(FormatNumber(BillAmt + ActOthCharges, 2))
                                    TSTDLines += CInt(DLines)
                                    TSTDSLines += CInt(DSLines)
                                    TBillUnits += CInt(BillUnits)
                                    TBillSUnits += CInt(BillSUnits)
                                    TBillAmt += CDbl(BillAmt)
                                    TBillOtherAmt += CDbl(FormatNumber(ActOthCharges, 2))
                                    TBillTotAmount += CDbl(FormatNumber(BillAmt + ActOthCharges, 2))
                                End If
                            End If
                        Finally
                            If SQLCmdI.Connection.State = System.Data.ConnectionState.Open Then
                                SQLCmdI.Connection.Close()
                                SQLCmdI = Nothing
                            End If
                        End Try

                    Else
                        minbilling = DRRec("minbilling").ToString
                        BillAccID = DRRec("BillAccId").ToString
                        InvoiceID = DRRec("trackId").ToString
                        Mode = Trim(DRRec("Mode").ToString)
                        InvCode = Trim(DRRec("InvCode").ToString)
                        If Mode = "DC" Or Mode = "TW" Or Mode = "LC" Or Mode = "TT" Or Mode = "DV" Then
                            strQuery = "Select  B.SubActID,  B.Rate, B.MiscRate, B.StatRate, L.MethodName, isnull(T1.Lines,0) AS BillUnits, isnull(T2.Lines,0) AS BillSUnits,  isnull(T1.stdLines,0) AS stdLines, isnull(T2.stdLines,0) AS stdSLines, isnull(T1.Amt,0) AS BillAmt, isnull(T2.Amt,0) AS BillSAmt     "
                            strQuery = strQuery & " from AdminSecureweb.dbo.tblAccBillDetails B"
                            strQuery = strQuery & " LEFT OUTER JOIN (Select TrackID, MethodName from AdminETS.dbo.tblLCMethodology ) L"
                            strQuery = strQuery & " on L.TrackID = B.LCMethodID "
                            strQuery = strQuery & " INNER JOIN (Select BillAccID, subActID, sum(unit) AS Lines, sum(amount) AS Amt, sum(stdLCCount) AS stdLines from tblBillingLines where BillAccID = '" & BillAccID & "'  and priority = 'False' Group by BillAccID, subActID) T1"
                            strQuery = strQuery & " on T1.BillAccID = B.BillAccID  and T1.SubActID = B.SubActID  "
                            strQuery = strQuery & " LEFT OUTER JOIN (Select BillAccID, subActID, sum(unit) AS Lines, sum(amount) AS Amt, sum(stdLCCount) AS stdLines from tblBillingLines where BillAccID = '" & BillAccID & "'  and priority='True' Group by BillAccID, subActID) T2"
                            strQuery = strQuery & " on T2.BillAccID = B.BillAccID  and T2.SubActID = B.SubActID where B.BillAccID = '" & BillAccID & "'"
                        Else

                            strQuery = "Select  B.SubActID,  B.Rate, B.MiscRate, B.StatRate, L.MethodName, isnull(T1.Lines,0) AS BillUnits, isnull(T2.Lines,0) AS BillSUnits,  isnull(T1.stdLines,0) AS stdLines, isnull(T2.stdLines,0) AS stdSLines, isnull(T1.Amt,0) AS BillAmt, isnull(T2.Amt,0) AS BillSAmt    "
                            strQuery = strQuery & " from AdminSecureweb.dbo.tblAccBillDetails B"
                            strQuery = strQuery & " LEFT OUTER JOIN (Select TrackID, MethodName from AdminETS.dbo.tblLCMethodology ) L"
                            strQuery = strQuery & " on L.TrackID = B.LCMethodID "
                            strQuery = strQuery & " INNER JOIN (Select BillAccID, sum(unit) AS Lines, sum(amount) AS Amt, sum(stdLCCount) AS stdLines from tblBillingLines where BillAccID = '" & BillAccID & "'  and priority = 'False' Group by BillAccID) T1"
                            strQuery = strQuery & " on T1.BillAccID = B.BillAccID "
                            strQuery = strQuery & " LEFT OUTER JOIN (Select BillAccID, sum(unit) AS Lines, sum(amount) AS Amt, sum(stdLCCount) AS stdLines from tblBillingLines where BillAccID = '" & BillAccID & "'  and priority='True' Group by BillAccID) T2"
                            strQuery = strQuery & " on T2.BillAccID = B.BillAccID where B.BillAccID = '" & BillAccID & "'"
                        End If
                        ' Response.Write(BillAccID & "<BR>")
                        If DRRec("BillActNumber").ToString = "900-5" Then
                            'Response.Write(strQuery)
                        End If

                        Dim SQLCmd1 As New SqlCommand(strQuery, New SqlConnection(strConn))
                        Try
                            SQLCmd1.Connection.Open()
                            Dim DRRec1 As SqlDataReader = SQLCmd1.ExecuteReader()
                            If DRRec1.HasRows Then
                                While (DRRec1.Read)
                                    If DRRec1("MethodName").ToString = "PerDictator" Then
                                        strQuery = "Select count(*) as NumDict from AdminSecureweb.dbo.AssignDic where GrpDicID = '" & DRRec1("SubActID").ToString & "'  "
                                        'strQuery = "Select count(*) as NumDict from (Select M.DictatorID as Numdict from AdminETS.dbo.tbltranscriptionMain M INNER Join tblBillingLines T ON T.TranscriptionID = M.TranscriptionID where  T.BillAccID = '" & BillAccID & "' and T.SubActID = '" & DRRec1("SubActID").ToString & "' Group by M.dictatorID)T1 "

                                        Dim SQLCmd2 As New SqlCommand(strQuery, New SqlConnection(strConn))
                                        SQLCmd2.Connection.Open()
                                        Dim DRRec2 As SqlDataReader = SQLCmd2.ExecuteReader()
                                        If DRRec2.HasRows Then
                                            If DRRec2.Read Then
                                                DLines += CInt(FormatNumber(DRRec1("stdLines"), 0))
                                                DSLines += CInt(FormatNumber(DRRec1("stdSLines"), 0))
                                                BillUnits += CDbl(FormatNumber(DRRec1("BillUnits"), 2))
                                                BillSUnits += CDbl(FormatNumber(DRRec1("BillSUnits"), 2))
                                                BillAmt += CDbl(FormatNumber(DRRec1("Rate") * DRRec2("NumDict"), 2))
                                            End If

                                        End If
                                    Else
                                        DLines += CInt(FormatNumber(DRRec1("stdLines"), 0))
                                        DSLines += CInt(FormatNumber(DRRec1("stdSLines"), 0))
                                        BillUnits += CDbl(FormatNumber(DRRec1("BillUnits"), 2))
                                        BillSUnits += CDbl(FormatNumber(DRRec1("BillSUnits"), 2))
                                        '  BillAmt += CDbl(FormatNumber(DRRec1("BillAmt") + DRRec1("BillSAmt"), 2))
                                        ' Try
                                        BillAmt += CDbl(FormatNumber(DRRec1("Rate") * DRRec1("BillUnits"), 2))
                                        BillAmt += CDbl(FormatNumber(DRRec1("StatRate") * DRRec1("BillSUnits"), 2))
                                        ' Catch ex As Exception

                                        '   End Try

                                        If DRRec("BillActNumber").ToString = "900-5" Then
                                            'Response.Write(DRRec1("Rate") * DRRec1("BillUnits") + DRRec1("StatRate") * DRRec1("BillSUnits"))
                                        End If
                                    End If
                                End While
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
                                Dim Cell12 As New TableCell
                                Dim Cell13 As New TableCell
                                Dim Cell14 As New TableCell
                                Dim Cell15 As New TableCell
                                Dim Cell16 As New TableCell
                                Dim Cell17 As New TableCell
                                Dim Cell18 As New TableCell
                                Row1.Font.Size = "8"
                                Cell1.HorizontalAlign = HorizontalAlign.Left
                                Cell1.Text = "<A HREF='ActbillReport.aspx?IndAct=False&ActDetails=True&BillAccID=" & BillAccID & "' Target=_Blank>" & DRRec("Description").ToString & "</a>"
                                Cell2.Text = DRRec("BillActNumber").ToString
                              
                                'Response.Write(DRRec("BillActNumber").ToString)
                                Cell3.HorizontalAlign = HorizontalAlign.Right
                                Cell4.HorizontalAlign = HorizontalAlign.Right
                                Cell5.HorizontalAlign = HorizontalAlign.Right
                                Cell6.HorizontalAlign = HorizontalAlign.Right
                                Cell7.HorizontalAlign = HorizontalAlign.Right
                                Cell8.HorizontalAlign = HorizontalAlign.Right
                                Cell9.HorizontalAlign = HorizontalAlign.Right
                                Cell3.Text = DLines
                                Cell4.Text = DSLines
                                Cell5.Text = BillUnits
                                Cell6.Text = BillSUnits
                                Cell7.Text = FormatNumber(BillAmt, 2)
                                'If ActBillCycle = True Then
                                '    If strBillCycle = "Cycle1" Then
                                '        ActOthCharges = DRRec("ActOthCharges1").ToString
                                '    ElseIf strBillCycle = "Cycle2" Then
                                '        ActOthCharges = DRRec("ActOthCharges2").ToString
                                '    End If
                                'Else
                                '    If strBillCycle = "Cycle2" Then
                                '        ActOthCharges = DRRec("ActOthCharges1").ToString + DRRec("ActOthCharges2").ToString
                                '    End If
                                'End If
                                Cell8.Text = FormatNumber(ActOthCharges, 2)
                                'Response.Write("$" & BillAmt & "$")
                                'Response.Write("$" & ActOthCharges & "$")
                                InvBillAmount = FormatNumber(CDbl(FormatNumber(BillAmt, 2)) + CDbl(FormatNumber(ActOthCharges, 2)), 2)
                                Cell9.Text = FormatNumber(InvBillAmount, 2)
                                'Response.Write(DRRec("Posted").ToString & "#" & DRRec("InvRecFound").ToString)
                                If DRRec("Posted").ToString = "True" Then
                                    Cell10.Text = "Posted"
                                    Cell11.Text = InvCode
                                    Cell12.Text = "<a href='../invoices/pdf/" & InvoiceID & ".pdf' target='_blank'>Download</a>"
                                    'Cell13.Text = "<input type=checkbox name=chInvoiceID value=" & InvoiceID & " onclick='highlightRow(this);'>"
                                Else
                                    If DLCycle.SelectedValue = 1 And DRRec("IsDiscountEnabled").ToString And DRRec("IsDiscCharged1").ToString = False Then
                                        Dim Disc As Double
                                        Disc = -(BillAmt * (DRRec("Discount") / 100))
                                        ActOthCharges = ActOthCharges + Disc
                                        Cell8.Text = FormatNumber(ActOthCharges, 2)
                                        InvBillAmount = FormatNumber(BillAmt + ActOthCharges, 2)
                                        Cell9.Text = FormatNumber(InvBillAmount, 2)
                                        InsRecord(DRRec("AccountID").ToString, "48CFFC7C-0A43-4273-BEBF-FA7B9CA17D74", "Preferred Customer Discount @ " & DRRec("Discount") & "%", 1, Disc, Disc, minDate)
                                    End If
                                    If DLCycle.SelectedValue = 2 And DRRec("IsDiscountEnabled").ToString And DRRec("IsDiscCharged2").ToString = False Then
                                        Dim Disc As Double
                                        Disc = -(BillAmt * (DRRec("Discount") / 100))
                                        ActOthCharges = ActOthCharges + Disc
                                        Cell8.Text = FormatNumber(ActOthCharges, 2)

                                        InvBillAmount = FormatNumber(BillAmt + ActOthCharges, 2)
                                        Cell9.Text = FormatNumber(InvBillAmount, 2)
                                        InsRecord(DRRec("AccountID").ToString, "48CFFC7C-0A43-4273-BEBF-FA7B9CA17D74", "Preferred Customer Discount @ " & DRRec("Discount") & "%", 1, Disc, Disc, minDate)
                                    End If
                                    If DLCycle.SelectedValue = 2 And minbilling > BillAmt Then
                                        Dim PrevBillAmount As Double = 0
                                        Dim TotMonthAmt As Double = 0
                                        Try
                                            strQuery = "select SUM(J.totamount) AS PrevBillAmount  from AdminSecureweb.dbo.InvupData I INNER JOIN  AdminSecureweb.dbo.tblInvItemDet J ON I.TrackID = J.InvoiceID INNER JOIN  AdminSecureweb.dbo.tblBillAccounts B ON B.InvoiceID = I.TrackID and J.Mode ='Trans'   where B.BillMonth = '" & DLMonth.SelectedValue & "' and B.BillYear='" & DLYear.SelectedValue & "' AND J.Mode ='Trans' AND B.AccountID ='" & DRRec("AccountID").ToString & "' "
                                            Dim SQLCmdPr As New SqlCommand(strQuery, New SqlConnection(strConn))
                                            Try
                                                SQLCmdPr.Connection.Open()
                                                PrevBillAmount = SQLCmdPr.ExecuteScalar

                                            Catch ex As Exception
                                                If SQLCmdPr.Connection.State = ConnectionState.Open Then
                                                    SQLCmdPr.Connection.Close()
                                                End If
                                            End Try
                                       
                                            TotMonthAmt = BillAmt + PrevBillAmount
                                            If minbilling > TotMonthAmt Then
                                                RemBalance = minbilling - TotMonthAmt

                                                '  Response.Write(FormatNumber(RemBalance, 2) & "#" & FormatNumber(minCharges, 2))
                                                If Not FormatNumber(RemBalance, 2) = FormatNumber(minCharges, 2) And DLCycle.SelectedValue = 2 Then
                                                    ' Response.Write(FormatNumber(RemBalance, 2) & "$" & FormatNumber(minCharges, 2))
                                                    InsRecord(DRRec("AccountID").ToString, "25c7d577-967e-48ab-a62d-87fb6f420be1", "Minimum monthly billing @ $" & FormatNumber(minbilling, 0) & "/month applicable", 1, RemBalance, RemBalance, minDate)
                                                    InvBillAmount = FormatNumber(BillAmt + ActOthCharges + RemBalance, 2)
                                                    Cell8.Text = FormatNumber(RemBalance + ActOthCharges, 2)


                                                Else
                                                    InvBillAmount = FormatNumber(BillAmt + ActOthCharges, 2)
                                                    Cell8.Text = FormatNumber(ActOthCharges, 2)
                                                End If
                                                Cell9.Text = FormatNumber(InvBillAmount, 2)
                                            End If

                                        Catch ex As Exception

                                        End Try
                                        '  Response.Write(minbilling & "#" & BillAmt)
                                    End If
                                    Cell11.Text = "-"
                                    Cell12.Text = "-"
                                    Cell13.Text = "-"
                                    If DRRec("InvRecFound").ToString = "Yes" Then
                                        '   Response.Write(-FormatNumber(DRRec("Invamount"), 2) & "#" & FormatNumber(BillAmt + DRRec("ActOthCharges"), 2))
                                        If -FormatNumber(DRRec("Invamount"), 2) = FormatNumber(InvBillAmount, 2) Then
                                            Cell10.Text = "<input type=checkbox name=chBillAccID value=" & BillAccID & "#" & InvoiceID & " onclick='highlightRow(this);'>"
                                            'Dim chkCheckBox As New CheckBox()
                                            'chkCheckBox.ID = "ReportID"
                                            'chkCheckBox.InputAttributes.Add("value", BillAccID)
                                            'chkCheckBox.InputAttributes.Add("onclick", "highlightRow(this)")
                                            'Cell10.Controls.Add(chkCheckBox)
                                            'Response.Write("Matching")
                                        End If

                                    End If
                                End If


                                'Cell12.Text = "-"

                                If Mode = "S" Or Mode = "" Then
                                    Cell14.Text = "Standard"
                                ElseIf Mode = "DV" Then
                                    Cell14.Text = "DeviceWise"
                                ElseIf Mode = "DC" Then
                                    Cell14.Text = "DictatorWise"
                                ElseIf Mode = "TW" Then
                                    Cell14.Text = "TemplateWise"
                                ElseIf Mode = "LC" Then
                                    Cell14.Text = "LocationWise"
                                ElseIf Mode = "TT" Then
                                    Cell14.Text = "TATWise"
                                End If

                                Cell18.Text = strBillCycle
                                'Row1.CssClass = "tblbg2"
                                'Cell1.CssClass = "alt1"
                                'Cell2.CssClass = "alt1"
                                'Cell14.CssClass = "alt1"
                                'Cell18.CssClass = "alt1"
                                'Cell3.CssClass = "alt1"
                                'Cell4.CssClass = "alt1"
                                'Cell5.CssClass = "alt1"
                                'Cell6.CssClass = "alt1"
                                'Cell7.CssClass = "alt1"
                                'Cell8.CssClass = "alt1"
                                'Cell9.CssClass = "alt1"
                                'Cell10.CssClass = "alt1"
                                'Cell11.CssClass = "alt1"
                                'Cell12.CssClass = "alt1"

                                Row1.Cells.Add(Cell1)
                                Row1.Cells.Add(Cell2)
                                Row1.Cells.Add(Cell14)
                                Row1.Cells.Add(Cell18)
                                Row1.Cells.Add(Cell3)
                                Row1.Cells.Add(Cell4)
                                Row1.Cells.Add(Cell5)
                                Row1.Cells.Add(Cell6)
                                Row1.Cells.Add(Cell7)
                                Row1.Cells.Add(Cell8)
                                Row1.Cells.Add(Cell9)
                                Row1.Cells.Add(Cell10)
                                Row1.Cells.Add(Cell11)
                                Row1.Cells.Add(Cell12)
                                'Row1.Cells.Add(Cell13)
                                'Row1.Cells.Add(Cell14)
                                'Row1.Cells.Add(Cell15)
                                'Row1.Cells.Add(Cell16)
                                'Row1.Cells.Add(Cell17)
                                tblMins.Rows.Add(Row1)
                                STSTDLines += CInt(DLines)
                                STSTDSLines += CInt(DSLines)
                                STBillUnits += CDbl(BillUnits)
                                STBillSUnits += CDbl(BillSUnits)
                                STBillAmt += CDbl(BillAmt)
                                STBillOtherAmt += CDbl(FormatNumber(ActOthCharges, 2))
                                STBillTotAmount += CDbl(FormatNumber(BillAmt + ActOthCharges, 2))
                                TSTDLines += CInt(DLines)
                                TSTDSLines += CInt(DSLines)
                                TBillUnits += CInt(BillUnits)
                                TBillSUnits += CInt(BillSUnits)
                                TBillAmt += CDbl(BillAmt)
                                TBillOtherAmt += CDbl(FormatNumber(ActOthCharges, 2))
                                TBillTotAmount += CDbl(FormatNumber(BillAmt + ActOthCharges, 2))
                            Else
                                'Response.Write(DRRec("Description").ToString)
                                If ActOthCharges > 0 Then
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
                                    Dim Cell12 As New TableCell
                                    Dim Cell13 As New TableCell
                                    Dim Cell14 As New TableCell
                                    Dim Cell15 As New TableCell
                                    Dim Cell16 As New TableCell
                                    Dim Cell17 As New TableCell
                                    Dim Cell18 As New TableCell
                                    Row1.Font.Size = "8"
                                    Cell1.HorizontalAlign = HorizontalAlign.Left
                                    Cell1.Text = "<A HREF='ActbillReport.aspx?IndAct=False&ActDetails=True&BillAccID=" & BillAccID & "' Target=_Blank>" & DRRec("Description").ToString & "</a>"
                                    Cell2.Text = DRRec("BillActNumber").ToString
                                   
                                    Cell3.HorizontalAlign = HorizontalAlign.Left
                                    Cell4.HorizontalAlign = HorizontalAlign.Left
                                    Cell5.HorizontalAlign = HorizontalAlign.Left
                                    Cell6.HorizontalAlign = HorizontalAlign.Left
                                    Cell7.HorizontalAlign = HorizontalAlign.Left
                                    Cell8.HorizontalAlign = HorizontalAlign.Left
                                    Cell9.HorizontalAlign = HorizontalAlign.Left
                                    Cell3.Text = DLines
                                    Cell4.Text = DSLines
                                    Cell5.Text = BillUnits
                                    Cell6.Text = BillSUnits
                                    Cell7.Text = BillAmt
                                    'If ActBillCycle = True Then
                                    '    If strBillCycle = "Cycle1" Then
                                    '        ActOthCharges = DRRec("ActOthCharges1").ToString
                                    '    ElseIf strBillCycle = "Cycle2" Then
                                    '        ActOthCharges = DRRec("ActOthCharges2").ToString
                                    '    End If
                                    'Else
                                    '    If strBillCycle = "Cycle2" Then
                                    '        ActOthCharges = DRRec("ActOthCharges1").ToString + DRRec("ActOthCharges2").ToString
                                    '    End If
                                    'End If
                                    Cell8.Text = FormatNumber(ActOthCharges, 2)
                                    Cell9.Text = FormatNumber(BillAmt + ActOthCharges, 2)

                                    If DRRec("Posted").ToString = "True" Then
                                        Cell10.Text = "Posted"
                                        Cell11.Text = InvCode
                                        Cell12.Text = "<a href='../invoices/pdf/" & InvoiceID & ".pdf' target='_blank'>Download</a>"
                                        'Cell13.Text = "<input type=checkbox name=chInvoiceID value=" & InvoiceID & " onclick='highlightRow(this);'>"
                                    Else
                                        If minbilling < BillAmt Then
                                            ' Response.Write(minbilling & "#" & BillAmt)
                                        End If
                                        Cell11.Text = "-"
                                        Cell12.Text = "-"
                                        Cell13.Text = "-"
                                        If DRRec("InvRecFound").ToString = "Yes" Then
                                            'Response.Write(-FormatNumber(DRRec("Invamount"), 2) & "#" & FormatNumber(BillAmt + DRRec("ActOthCharges"), 2))
                                            If -FormatNumber(DRRec("Invamount"), 2) = FormatNumber(BillAmt + ActOthCharges, 2) Then
                                                Cell10.Text = "<input type=checkbox name=chBillAccID value=" & BillAccID & "#" & InvoiceID & " onclick='highlightRow(this);'>"
                                                'Dim chkCheckBox As New CheckBox()
                                                'chkCheckBox.ID = "ReportID"
                                                'chkCheckBox.InputAttributes.Add("value", BillAccID)
                                                'chkCheckBox.InputAttributes.Add("onclick", "highlightRow(this)")
                                                'Cell10.Controls.Add(chkCheckBox)
                                                'Response.Write("Matching")
                                            End If

                                        End If
                                    End If


                                    'Cell12.Text = "-"

                                    If Mode = "S" Or Mode = "" Then
                                        Cell14.Text = "Standard"
                                    ElseIf Mode = "DV" Then
                                        Cell14.Text = "DeviceWise"
                                    ElseIf Mode = "DC" Then
                                        Cell14.Text = "DictatorWise"
                                    ElseIf Mode = "TW" Then
                                        Cell14.Text = "TemplateWise"
                                    ElseIf Mode = "LC" Then
                                        Cell14.Text = "LocationWise"
                                    ElseIf Mode = "TT" Then
                                        Cell14.Text = "TATWise"
                                    End If
                                    Cell18.Text = strBillCycle

                                    'Row1.CssClass = "tblbg2"
                                    'Cell1.CssClass = "alt1"
                                    'Cell2.CssClass = "alt1"
                                    'Cell14.CssClass = "alt1"
                                    'Cell18.CssClass = "alt1"
                                    'Cell3.CssClass = "alt1"
                                    'Cell4.CssClass = "alt1"
                                    'Cell5.CssClass = "alt1"
                                    'Cell6.CssClass = "alt1"
                                    Cell7.CssClass = "alt1"
                                    Cell8.CssClass = "alt1"
                                    Cell9.CssClass = "alt1"
                                    'Cell10.CssClass = "alt1"
                                    'Cell11.CssClass = "alt1"
                                    'Cell12.CssClass = "alt1"
                                    Row1.Cells.Add(Cell1)
                                    Row1.Cells.Add(Cell2)
                                    Row1.Cells.Add(Cell14)
                                    Row1.Cells.Add(Cell18)
                                    Row1.Cells.Add(Cell3)
                                    Row1.Cells.Add(Cell4)
                                    Row1.Cells.Add(Cell5)
                                    Row1.Cells.Add(Cell6)
                                    Row1.Cells.Add(Cell7)
                                    Row1.Cells.Add(Cell8)
                                    Row1.Cells.Add(Cell9)
                                    Row1.Cells.Add(Cell10)
                                    Row1.Cells.Add(Cell11)
                                    Row1.Cells.Add(Cell12)
                                    'Row1.Cells.Add(Cell13)
                                    'Row1.Cells.Add(Cell14)
                                    'Row1.Cells.Add(Cell15)
                                    'Row1.Cells.Add(Cell16)
                                    'Row1.Cells.Add(Cell17)
                                    tblMins.Rows.Add(Row1)
                                    STSTDLines += CInt(DLines)
                                    STSTDSLines += CInt(DSLines)
                                    STBillUnits += CDbl(BillUnits)
                                    STBillSUnits += CDbl(BillSUnits)
                                    STBillAmt += CDbl(BillAmt)
                                    STBillOtherAmt += CDbl(FormatNumber(ActOthCharges, 2))
                                    STBillTotAmount += CDbl(FormatNumber(BillAmt + ActOthCharges, 2))
                                    TSTDLines += CInt(DLines)
                                    TSTDSLines += CInt(DSLines)
                                    TBillUnits += CDbl(BillUnits)
                                    TBillSUnits += CDbl(BillSUnits)
                                    TBillAmt += CDbl(BillAmt)
                                    TBillOtherAmt += CDbl(FormatNumber(ActOthCharges, 2))
                                    TBillTotAmount += CDbl(FormatNumber(BillAmt + ActOthCharges, 2))
                                End If

                            End If
                            DRRec1.Close()
                        Finally
                            If SQLCmd1.Connection.State = System.Data.ConnectionState.Open Then
                                SQLCmd1.Connection.Close()
                                SQLCmd1 = Nothing
                            End If
                        End Try
                    End If
                End While
                Dim A1Row1 As New TableRow
                Dim A1Cell1 As New TableCell
                Dim A1Cell2 As New TableCell
                Dim A1Cell3 As New TableCell
                Dim A1Cell4 As New TableCell
                Dim A1Cell5 As New TableCell
                Dim A1Cell6 As New TableCell
                Dim A1Cell7 As New TableCell
                Dim A1Cell8 As New TableCell
                Dim A1Cell9 As New TableCell
                Dim A1Cell10 As New TableCell
                Dim A1Cell11 As New TableCell
                Dim A1Cell12 As New TableCell
                Dim A1Cell13 As New TableCell
                Dim A1Cell14 As New TableCell
                Dim A1Cell15 As New TableCell
                Dim A1Cell16 As New TableCell
                Dim A1Cell17 As New TableCell
                Dim A1Cell18 As New TableCell

                A1Row1.HorizontalAlign = HorizontalAlign.Center
                'A1Row1.CssClass = "tblbg"
                'A1Row1.Font.Bold = True
                A1Cell1.ColumnSpan = 4
                A1Cell1.Text = "SubTotal"
                A1Cell1.CssClass = "alt1"
                A1Cell2.HorizontalAlign = HorizontalAlign.Right
                A1Cell2.CssClass = "alt1"
                A1Cell3.HorizontalAlign = HorizontalAlign.Right
                A1Cell3.CssClass = "alt1"
                A1Cell4.HorizontalAlign = HorizontalAlign.Right
                A1Cell4.CssClass = "alt1"
                A1Cell5.HorizontalAlign = HorizontalAlign.Right
                A1Cell5.CssClass = "alt1"
                A1Cell6.HorizontalAlign = HorizontalAlign.Right
                A1Cell6.CssClass = "alt1"
                A1Cell7.HorizontalAlign = HorizontalAlign.Right
                A1Cell7.CssClass = "alt1"
                A1Cell8.HorizontalAlign = HorizontalAlign.Right
                A1Cell8.CssClass = "alt1"
                A1Cell2.Text = FormatNumber(STSTDLines, 0)
                A1Cell3.Text = FormatNumber(STSTDSLines, 0)
                A1Cell4.Text = FormatNumber(STBillUnits, 0)
                A1Cell5.Text = FormatNumber(STBillSUnits, 0)
                A1Cell6.Text = FormatNumber(STBillAmt, 2)
                A1Cell7.Text = FormatNumber(STBillOtherAmt, 2)
                A1Cell8.Text = FormatNumber(STBillTotAmount, 2)
                A1Cell9.Text = "-"
                A1Cell9.CssClass = "alt1"
                A1Cell10.Text = "-"
                A1Cell10.CssClass = "alt1"
                A1Cell11.Text = "-"
                A1Cell11.CssClass = "alt1"
                A1Cell12.Text = "-"
                A1Cell12.CssClass = "alt1"
                A1Cell13.Text = "-"
                A1Cell13.CssClass = "alt1"
                A1Cell14.Text = "-"
                A1Cell14.CssClass = "alt1"
                A1Cell15.Text = "-"
                A1Cell15.CssClass = "alt1"
                A1Cell16.Text = "-"
                A1Cell16.CssClass = "alt1"
                A1Cell18.Text = "-"
                A1Cell18.CssClass = "alt1"

                A1Row1.Cells.Add(A1Cell1)
                A1Row1.Cells.Add(A1Cell2)
                A1Row1.Cells.Add(A1Cell3)
                'A1Row1.Cells.Add(A1Cell18)
                A1Row1.Cells.Add(A1Cell4)
                A1Row1.Cells.Add(A1Cell5)
                A1Row1.Cells.Add(A1Cell6)
                A1Row1.Cells.Add(A1Cell7)
                A1Row1.Cells.Add(A1Cell8)
                A1Row1.Cells.Add(A1Cell13)
                A1Row1.Cells.Add(A1Cell14)
                A1Row1.Cells.Add(A1Cell15)
                ''A1Row1.Cells.Add(A1Cell16)
                tblMins.Rows.Add(A1Row1)

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
                Dim A11Cell10 As New TableCell
                Dim A11Cell11 As New TableCell
                Dim A11Cell12 As New TableCell
                Dim A11Cell13 As New TableCell
                Dim A11Cell14 As New TableCell
                Dim A11Cell15 As New TableCell
                Dim A11Cell16 As New TableCell
                Dim A11Cell17 As New TableCell
                Dim A11Cell18 As New TableCell

                A11Row1.HorizontalAlign = HorizontalAlign.Center
                'A11Row1.CssClass = "tblbgbody"
                'A11Row1.Font.Bold = True
                A11Cell1.ColumnSpan = 4
                A11Cell2.HorizontalAlign = HorizontalAlign.Right
                A11Cell3.HorizontalAlign = HorizontalAlign.Right
                A11Cell4.HorizontalAlign = HorizontalAlign.Right
                A11Cell5.HorizontalAlign = HorizontalAlign.Right
                A11Cell6.HorizontalAlign = HorizontalAlign.Right
                A11Cell7.HorizontalAlign = HorizontalAlign.Right
                A11Cell8.HorizontalAlign = HorizontalAlign.Right
                A11Cell1.Text = "<a href='ActbillReport.aspx?IndAct=False&AllActDetails=True&billmonth=" & DLMonth.SelectedValue & "&billyear=" & DLYear.SelectedValue & "&billcycle=" & DLCycle.SelectedValue & "' target=_blank>Total</a>"
                A11Cell1.CssClass = "alt1"
                A11Cell2.Text = FormatNumber(TSTDLines, 0)
                A11Cell2.CssClass = "alt1"
                A11Cell3.Text = FormatNumber(TSTDSLines, 0)
                A11Cell3.CssClass = "alt1"
                A11Cell4.Text = FormatNumber(TBillUnits, 0)
                A11Cell4.CssClass = "alt1"
                A11Cell5.Text = FormatNumber(TBillSUnits, 0)
                A11Cell5.CssClass = "alt1"
                A11Cell6.Text = FormatNumber(TBillAmt, 2)
                A11Cell6.CssClass = "alt1"
                A11Cell7.Text = FormatNumber(TBillOtherAmt, 2)
                A11Cell7.CssClass = "alt1"
                A11Cell8.Text = FormatNumber(TBillTotAmount, 2)
                A11Cell8.CssClass = "alt1"
                A11Cell9.Text = ""
                A11Cell9.CssClass = "alt1"
                A11Cell10.Text = "-"
                A11Cell10.CssClass = "alt1"
                A11Cell11.Text = "-"
                A11Cell11.CssClass = "alt1"
                A11Cell12.Text = "-"
                A11Cell12.CssClass = "alt1"
                A11Cell13.Text = "<input type=checkbox name=SelAll onclick='changeAll();' >"
                A11Cell13.CssClass = "alt1"
                A11Cell14.Text = "-"
                A11Cell14.CssClass = "alt1"
                A11Cell15.Text = "-"
                A11Cell15.CssClass = "alt1"
                A11Cell16.Text = "-"
                A11Cell16.CssClass = "alt1"
                A11Cell18.Text = "-"
                A11Cell18.CssClass = "alt1"
                A11Row1.Cells.Add(A11Cell1)
                A11Row1.Cells.Add(A11Cell2)
                A11Row1.Cells.Add(A11Cell3)
                '   A11Row1.Cells.Add(A11Cell18)
                A11Row1.Cells.Add(A11Cell4)
                A11Row1.Cells.Add(A11Cell5)
                A11Row1.Cells.Add(A11Cell6)
                A11Row1.Cells.Add(A11Cell7)
                A11Row1.Cells.Add(A11Cell8)
                A11Row1.Cells.Add(A11Cell13)
                A11Row1.Cells.Add(A11Cell14)
                A11Row1.Cells.Add(A11Cell15)
                ' A11Row1.Cells.Add(A11Cell16)
                tblMins.Rows.Add(A11Row1)

                Dim A12Row1 As New TableRow
                Dim A12Cell1 As New TableCell
                Dim A12Cell2 As New TableCell
                Dim A12Cell3 As New TableCell
                Dim A12Cell4 As New TableCell
                Dim A12Cell5 As New TableCell

                btnPost.Visible = True
                ' btnEmail.Visible = True

                A12Row1.HorizontalAlign = HorizontalAlign.Center
                'A12Row1.CssClass = "tblbg2"
                'A12Row1.Font.Bold = True
                A12Cell1.ColumnSpan = 10
                A12Cell1.Text = ""
                A12Cell1.CssClass = "alt1"
                A12Cell2.Controls.Add(btnPost)
                A12Cell2.CssClass = "alt1"
                A12Cell3.Text = "-"
                A12Cell3.CssClass = "alt1"
                A12Cell4.Text = "-"
                A12Cell4.CssClass = "alt1"
                A12Cell5.Text = ""
                A12Cell5.CssClass = "alt1"

                A12Row1.Cells.Add(A12Cell1)
                A12Row1.Cells.Add(A12Cell2)
                A12Row1.Cells.Add(A12Cell3)
                A12Row1.Cells.Add(A12Cell4)
                '   A12Row1.Cells.Add(A12Cell5)

                tblMins.Rows.Add(A12Row1)
            End If
        Finally
            If SQLCmd.Connection.State = System.Data.ConnectionState.Open Then
                SQLCmd.Connection.Close()
                SQLCmd = Nothing
            End If
        End Try

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
    Function InsRecord(ByVal AccID As String, ByVal ItemID As String, ByVal Descr As String, ByVal Quantity As Integer, ByVal amount As Double, ByVal Totamount As Double, ByVal ServiceDate As String) As Boolean
        Try

            Dim strConn As String
            Dim strQuery As String
            strConn = System.Configuration.ConfigurationManager.AppSettings("ETSCon")
            strQuery = "Delete from  AdminSecureweb.dbo.tblinvItemdet WHERE AccountID = '" & AccID & "' and InvoiceID ='11111111-1111-1111-1111-111111111111' and itemid='" & ItemID & "' and servicedate='" & ServiceDate & "' "
            '            Response.Write(strQuery)
            Dim cmdIns1 As New SqlCommand(strQuery, New SqlConnection(strConn))
            Try
                cmdIns1.Connection.Open()
                cmdIns1.ExecuteNonQuery()
            Finally
                If cmdIns1.Connection.State = System.Data.ConnectionState.Open Then
                    cmdIns1.Connection.Close()
                    cmdIns1 = Nothing
                End If
            End Try
            strQuery = "Insert Into AdminSecureweb.dbo.tblinvItemdet (AccountID,InvoiceID, itemid, Descr, quantity, Amount,totamount,mode,servicedate, dateupdate, IsAuto) Values ('" & AccID & "', '11111111-1111-1111-1111-111111111111', '" & ItemID & "', '" & Descr & "', '" & Quantity & "', convert(money," & amount & "),  convert(money," & Totamount & "), 'VAS', '" & ServiceDate & "', '" & Now & "', 1)"
            ' Response.Write(strQuery)
            Dim cmdIns As New SqlCommand(strQuery, New SqlConnection(strConn))
            Try
                cmdIns.Connection.Open()
                cmdIns.ExecuteNonQuery()
            Finally
                If cmdIns.Connection.State = System.Data.ConnectionState.Open Then
                    cmdIns.Connection.Close()
                    cmdIns = Nothing
                End If
            End Try
            Return True
        Catch ex As Exception
            Return False
        End Try

    End Function

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            GetMyMonthList(DLMonth, True)
            GetMyYearList(DLYear, True)
        End If
    End Sub
End Class
