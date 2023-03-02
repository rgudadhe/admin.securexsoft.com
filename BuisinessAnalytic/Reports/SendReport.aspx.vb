Imports System.Data.Sqlclient
Imports System.Data
Partial Class MIS_DailyMins
    Inherits BasePage
    Protected Sub ShowActDetails()
        Dim strConn As String
        Dim Mode As String
        Dim strDate As String
        Dim strCategory As String
        Dim BillAccID As String
        strCategory = ""
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
        If DLCycle.SelectedValue = "1" Then
            tblDtls.Text = "Billing Report (" & C1SDate & " - " & C1EDate & ")"
        Else
            tblDtls.Text = "Billing Report (" & C1SDate & " - " & C2EDate & ")"
        End If

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
        Dim TSTDLines As String
        Dim TSTDSLines As String
        Dim TBillUnits As String
        Dim TBillSUnits As String
        Dim TBillAmt As String
        Dim TBillOtherAmt As String
        Dim TBillTotAmount As String
        Dim InvoiceID As String

        TSTDLines = 0
        TSTDSLines = 0
        TBillUnits = 0
        TBillSUnits = 0
        TBillAmt = 0
        TBillOtherAmt = 0
        TBillTotAmount = 0
        strQuery = "Select A.Accountid, IsNull(V.OthCharges,0) as ActOthCharges, BA.Mode, A.Description, A.BillActNumber, C.priority, C.Description as CateDescr, BA.MinBilling, BA.BillAccID, BA.Posted, I.Amount as InvAmount, I.trackID, I.InvRecFound    "
        strQuery = strQuery & " from tblaccounts A "
        strQuery = strQuery & " INNER JOIN (Select * from AdminSecureweb.dbo.tblBillAccounts) BA"
        strQuery = strQuery & " on BA.AccountID = A.AccountID"
        strQuery = strQuery & " LEFT OUTER JOIN (Select * from tblActCategories) C"
        strQuery = strQuery & " on C.category = A.Category"
        strQuery = strQuery & " LEFT OUTER JOIN (Select TrackId, AccID, Amount, 'Yes' as InvRecFound from AdminSecureweb.dbo.InvupData where BillMonth='" & DLMonth.SelectedValue & "' and Billyear='" & DLYear.SelectedValue & "' and BillCycle='Cycle" & DLCycle.SelectedValue & "' ) I "
        strQuery = strQuery & " on I.AccID = BA.AccountID"
        strQuery = strQuery & " LEFT OUTER JOIN (Select AccountID, sum(totamount) as OthCharges from AdminSecureweb.dbo.tblInvItemDet where Servicedate between  '" & C1SDate & "' and '" & C2EDate & "'  Group By AccountID) V"
        strQuery = strQuery & " on V.accountid = A.Accountid  where BA.BillMonth = '" & DLMonth.SelectedValue & "' and BA.BillYear='" & DLYear.SelectedValue & "' and BillCycle = '" & DLCycle.SelectedValue & "' Order by C.Priority, A.Description"
        'Response.Write(strQuery)

        Dim SQLCmd As New SqlCommand(strQuery, New SqlConnection(strConn))
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
                BillAccID = DRRec("BillAccId").ToString
                InvoiceID = DRRec("trackId").ToString
                Mode = Trim(DRRec("Mode").ToString)
                I = I + 1
                If I = 1 Then
                    strCategory = DRRec("CateDescr").ToString
                    Dim Row2 As New TableRow
                    Row2.HorizontalAlign = HorizontalAlign.Center
                    Row2.CssClass = "tblbgbody"
                    Row2.Font.Bold = True
                    Row2.Font.Italic = True
                    Row2.ForeColor = Drawing.Color.White
                    Row2.Font.Size = "10"
                    Dim CatCell As New TableCell
                    CatCell.ColumnSpan = "14"
                    CatCell.Text = DRRec("CateDescr").ToString
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
                        ARow1.CssClass = "tblbg"
                        ARow1.Font.Bold = True
                        ARow1.Font.Size = "9"
                        ACell1.ColumnSpan = 3
                        ACell1.Text = "SubTotal"
                        ACell2.Text = FormatNumber(STSTDLines, 0)
                        ACell3.Text = FormatNumber(STSTDSLines, 0)
                        ACell4.Text = FormatNumber(STBillUnits, 0)
                        ACell5.Text = FormatNumber(STBillSUnits, 0)
                        ACell6.Text = FormatNumber(STBillAmt, 2)
                        ACell7.Text = FormatNumber(STBillOtherAmt, 2)
                        ACell8.Text = FormatNumber(STBillTotAmount, 2)
                        ACell9.Text = "-"
                        ACell10.Text = "-"
                        ACell11.Text = "-"
                        ACell12.Text = "-"
                        ACell13.Text = "-"
                        ACell14.Text = "-"
                        ACell15.Text = "-"
                        ACell16.Text = "-"

                        ARow1.Cells.Add(ACell1)
                        ARow1.Cells.Add(ACell2)
                        ARow1.Cells.Add(ACell3)

                        ARow1.Cells.Add(ACell4)
                        ARow1.Cells.Add(ACell5)
                        ARow1.Cells.Add(ACell6)
                        ARow1.Cells.Add(ACell7)
                        ARow1.Cells.Add(ACell8)
                        ARow1.Cells.Add(ACell13)
                        ARow1.Cells.Add(ACell14)
                        ARow1.Cells.Add(ACell15)
                        ARow1.Cells.Add(ACell16)
                        tblMins.Rows.Add(ARow1)
                        strCategory = DRRec("CateDescr").ToString
                        Dim Row2 As New TableRow
                        Row2.HorizontalAlign = HorizontalAlign.Center
                        Row2.CssClass = "tblbgbody"
                        Row2.Font.Bold = True
                        Row2.Font.Italic = True
                        Row2.ForeColor = Drawing.Color.White
                        Row2.Font.Size = "10"
                        Dim CatCell As New TableCell
                        CatCell.ColumnSpan = "14"
                        CatCell.Text = DRRec("CateDescr").ToString
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

                If Mode = "DC" Or Mode = "LC" Then
                    strQuery = "Select  B.SubActID, B.Rate, L.MethodName, isnull(T1.Lines,0) AS BillUnits, isnull(T2.Lines,0) AS BillSUnits,  isnull(T1.stdLines,0) AS stdLines, isnull(T2.stdLines,0) AS stdSLines, isnull(T1.Amt,0) AS BillAmt    "
                    strQuery = strQuery & " from AdminSecureweb.dbo.tblAccBillDetails B"
                    strQuery = strQuery & " LEFT OUTER JOIN (Select TrackID, MethodName from AdminETS.dbo.tblLCMethodology ) L"
                    strQuery = strQuery & " on L.TrackID = B.LCMethodID "
                    strQuery = strQuery & " LEFT OUTER JOIN (Select BillAccID, subActID, sum(unit) AS Lines, sum(amount) AS Amt, sum(stdLCCount) AS stdLines from tblBillingLines where BillAccID = '" & BillAccID & "' Group by BillAccID, subActID) T1"
                    strQuery = strQuery & " on T1.BillAccID = B.BillAccID  and T1.SubActID = B.SubActID  "
                    strQuery = strQuery & " LEFT OUTER JOIN (Select BillAccID, subActID, sum(unit) AS Lines, sum(amount) AS Amt, sum(stdLCCount) AS stdLines from tblBillingLines where BillAccID = '" & BillAccID & "'  and priority='True' Group by BillAccID, subActID) T2"
                    strQuery = strQuery & " on T2.BillAccID = B.BillAccID  and T2.SubActID = B.SubActID where B.BillAccID = '" & BillAccID & "'"

                Else
                    strQuery = "Select  B.SubActID, B.Rate, L.MethodName, isnull(T1.Lines,0) AS BillUnits, isnull(T2.Lines,0) AS BillSUnits,  isnull(T1.stdLines,0) AS stdLines, isnull(T2.stdLines,0) AS stdSLines, isnull(T1.Amt,0) AS BillAmt    "
                    strQuery = strQuery & " from AdminSecureweb.dbo.tblAccBillDetails B"
                    strQuery = strQuery & " LEFT OUTER JOIN (Select TrackID, MethodName from AdminETS.dbo.tblLCMethodology ) L"
                    strQuery = strQuery & " on L.TrackID = B.LCMethodID "
                    strQuery = strQuery & " LEFT OUTER JOIN (Select BillAccID, sum(unit) AS Lines, sum(amount) AS Amt, sum(stdLCCount) AS stdLines from tblBillingLines where BillAccID = '" & BillAccID & "' Group by BillAccID) T1"
                    strQuery = strQuery & " on T1.BillAccID = B.BillAccID "
                    strQuery = strQuery & " LEFT OUTER JOIN (Select BillAccID, sum(unit) AS Lines, sum(amount) AS Amt, sum(stdLCCount) AS stdLines from tblBillingLines where BillAccID = '" & BillAccID & "'  and priority='True' Group by BillAccID) T2"
                    strQuery = strQuery & " on T2.BillAccID = B.BillAccID where B.BillAccID = '" & BillAccID & "'"

                End If
                Dim SQLCmd1 As New SqlCommand(strQuery, New SqlConnection(strConn))
                SQLCmd1.Connection.Open()
                Dim DRRec1 As SqlDataReader = SQLCmd1.ExecuteReader()
                If DRRec1.HasRows Then
                    While (DRRec1.Read)
                        If DRRec1("MethodName") = "PerDictator" Then
                            strQuery = "Select count(*) as NumDict from (Select M.DictatorID as Numdict from AdminETS.dbo.tbltranscriptionMain M INNER Join tblBillingLines T ON T.TranscriptionID = M.TranscriptionID where  T.BillAccID = '" & BillAccID & "' and T.SubActID = '" & DRRec1("SubActID").ToString & "' Group by M.dictatorID)T1 "

                            Dim SQLCmd2 As New SqlCommand(strQuery, New SqlConnection(strConn))
                            SQLCmd2.Connection.Open()
                            Dim DRRec2 As SqlDataReader = SQLCmd2.ExecuteReader()
                            If DRRec2.HasRows Then
                                If DRRec2.Read Then
                                    DLines += CInt(FormatNumber(DRRec1("stdLines"), 0))
                                    DSLines += CInt(FormatNumber(DRRec1("stdSLines"), 0))
                                    BillUnits += CInt(FormatNumber(DRRec1("BillUnits"), 0))
                                    BillSUnits += CInt(FormatNumber(DRRec1("BillSUnits"), 0))
                                    BillAmt += CDbl(FormatNumber(DRRec1("Rate") * DRRec2("NumDict"), 2))
                                End If

                            End If
                        Else
                            DLines += CInt(FormatNumber(DRRec1("stdLines"), 0))
                            DSLines += CInt(FormatNumber(DRRec1("stdSLines"), 0))
                            BillUnits += CInt(FormatNumber(DRRec1("BillUnits"), 0))
                            BillSUnits += CInt(FormatNumber(DRRec1("BillSUnits"), 0))
                            BillAmt += CDbl(FormatNumber(DRRec1("BillAmt"), 2))
                        End If

                    End While
                End If
                DRRec1.Close()
                SQLCmd1.Connection.Close()

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
                Row1.Font.Size = "9"
                Cell1.Text = "<A HREF='ActbillReport.aspx?ActDetails=True&BillAccID=" & BillAccID & "' Target=_Blank>" & DRRec("Description").ToString & "</a>"

                Cell2.Text = DRRec("BillActNumber").ToString

                Cell3.Text = DLines
                Cell4.Text = DSLines
                Cell5.Text = BillUnits
                Cell6.Text = BillSUnits
                Cell7.Text = BillAmt
                Cell8.Text = FormatNumber(DRRec("ActOthCharges").ToString, 2)
                Cell9.Text = FormatNumber(BillAmt + DRRec("ActOthCharges"), 2)

                If DRRec("Posted").ToString = "True" Then
                    Cell10.Text = "Posted"
                Else
                    If DRRec("InvRecFound").ToString = "Yes" Then
                        'Response.Write(-FormatNumber(DRRec("Invamount"), 2) & "#" & FormatNumber(BillAmt + DRRec("ActOthCharges"), 2))
                        If -FormatNumber(DRRec("Invamount"), 2) = FormatNumber(BillAmt + DRRec("ActOthCharges"), 2) Then
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

                Cell11.Text = "-"
                Cell12.Text = "-"
                Cell13.Text = "-"
                If Mode = "S" Or Mode = "" Then
                    Cell14.Text = "Standard"
                ElseIf Mode = "DV" Then
                    Cell14.Text = "DeviceWise"
                ElseIf Mode = "DC" Then
                    Cell14.Text = "DictatorWise"
                ElseIf Mode = "LC" Then
                    Cell14.Text = "LocationWise"
                End If


                Row1.CssClass = "tblbg2"
                Row1.Cells.Add(Cell1)
                Row1.Cells.Add(Cell2)
                Row1.Cells.Add(Cell14)
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
                Row1.Cells.Add(Cell13)
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
                STBillOtherAmt += CDbl(FormatNumber(DRRec("ActOthCharges"), 2))
                STBillTotAmount += CDbl(FormatNumber(BillAmt + DRRec("ActOthCharges"), 2))
                TSTDLines += CInt(DLines)
                TSTDSLines += CInt(DSLines)
                TBillUnits += CInt(BillUnits)
                TBillSUnits += CInt(BillSUnits)
                TBillAmt += CDbl(BillAmt)
                TBillOtherAmt += CDbl(FormatNumber(DRRec("ActOthCharges"), 2))
                TBillTotAmount += CDbl(FormatNumber(BillAmt + DRRec("ActOthCharges"), 2))

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
            A1Row1.CssClass = "tblbg"
            A1Row1.Font.Bold = True
            A1Cell1.ColumnSpan = 3
            A1Cell1.Text = "SubTotal"
            A1Cell2.Text = FormatNumber(STSTDLines, 0)
            A1Cell3.Text = FormatNumber(STSTDSLines, 0)
            A1Cell4.Text = FormatNumber(STBillUnits, 0)
            A1Cell5.Text = FormatNumber(STBillSUnits, 0)
            A1Cell6.Text = FormatNumber(STBillAmt, 2)
            A1Cell7.Text = FormatNumber(STBillOtherAmt, 2)
            A1Cell8.Text = FormatNumber(STBillTotAmount, 2)
            A1Cell9.Text = "-"
            A1Cell10.Text = "-"
            A1Cell11.Text = "-"
            A1Cell12.Text = "-"
            A1Cell13.Text = "-"
            A1Cell14.Text = "-"
            A1Cell15.Text = "-"
            A1Cell16.Text = "-"

            A1Row1.Cells.Add(A1Cell1)
            A1Row1.Cells.Add(A1Cell2)
            A1Row1.Cells.Add(A1Cell3)
            A1Row1.Cells.Add(A1Cell4)
            A1Row1.Cells.Add(A1Cell5)
            A1Row1.Cells.Add(A1Cell6)
            A1Row1.Cells.Add(A1Cell7)
            A1Row1.Cells.Add(A1Cell8)
            A1Row1.Cells.Add(A1Cell13)
            A1Row1.Cells.Add(A1Cell14)
            A1Row1.Cells.Add(A1Cell15)
            A1Row1.Cells.Add(A1Cell16)
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
            A11Row1.CssClass = "tblbgbody"
            A11Row1.Font.Bold = True
            A11Cell1.ColumnSpan = 3
            A11Cell1.Text = "<a href='actbillreport.aspx?AllActDetails=True&billmonth=" & DLMonth.SelectedValue & "&billyear=" & DLYear.SelectedValue & "&billcycle=" & DLCycle.SelectedValue & "' target=_blank>Total</a>"
            A11Cell2.Text = FormatNumber(TSTDLines, 0)
            A11Cell3.Text = FormatNumber(TSTDSLines, 0)
            A11Cell4.Text = FormatNumber(TBillUnits, 0)
            A11Cell5.Text = FormatNumber(TBillSUnits, 0)
            A11Cell6.Text = FormatNumber(TBillAmt, 2)
            A11Cell7.Text = FormatNumber(TBillOtherAmt, 2)
            A11Cell8.Text = FormatNumber(TBillTotAmount, 2)
            A11Cell9.Text = ""
            A11Cell10.Text = "-"
            A11Cell11.Text = "-"
            A11Cell12.Text = "-"
            A11Cell13.Text = "<input type=checkbox name=SelAll onclick='changeAll();' >"
            A11Cell14.Text = "-"
            A11Cell15.Text = "-"
            A11Cell16.Text = "-"

            A11Row1.Cells.Add(A11Cell1)
            A11Row1.Cells.Add(A11Cell2)
            A11Row1.Cells.Add(A11Cell3)
            A11Row1.Cells.Add(A11Cell4)
            A11Row1.Cells.Add(A11Cell5)
            A11Row1.Cells.Add(A11Cell6)
            A11Row1.Cells.Add(A11Cell7)
            A11Row1.Cells.Add(A11Cell8)
            A11Row1.Cells.Add(A11Cell13)
            A11Row1.Cells.Add(A11Cell14)
            A11Row1.Cells.Add(A11Cell15)
            A11Row1.Cells.Add(A11Cell16)
            tblMins.Rows.Add(A11Row1)

            Dim A12Row1 As New TableRow
            Dim A12Cell1 As New TableCell
            Dim A12Cell2 As New TableCell
            Dim A12Cell3 As New TableCell
            Dim A12Cell4 As New TableCell
            Dim A12Cell5 As New TableCell

            btnPost.Visible = True
            btnEmail.Visible = True

            A12Row1.HorizontalAlign = HorizontalAlign.Center
            A12Row1.CssClass = "tblbg2"
            A12Row1.Font.Bold = True
            A12Cell1.ColumnSpan = 10
            A12Cell1.Text = ""
            A12Cell2.Controls.Add(btnPost)
            A12Cell3.Text = "-"
            A12Cell4.Controls.Add(btnEmail)
            A12Cell5.Text = ""

            A12Row1.Cells.Add(A12Cell1)
            A12Row1.Cells.Add(A12Cell2)
            A12Row1.Cells.Add(A12Cell3)
            A12Row1.Cells.Add(A12Cell4)
            A12Row1.Cells.Add(A12Cell5)

            tblMins.Rows.Add(A12Row1)
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

End Class
