Imports System.Data.SqlClient
Imports System.Data
Imports System.IO
Imports HTMLConverter


Partial Class Billing_Reports_Postbilling
    Inherits BasePage
    Public ProcFolder As String = Server.MapPath("../")
    Public DT As New DataTable

    


    Protected Sub PostBilling()
        DT.Columns.Clear()
        DT.Rows.Clear()
        'Dim ObjHTML As New HTMLConverter.HTMLConverterX
        'ObjHTML.Convert("http://ets.edictate.com/secureweb/billing/InvDetails.htm", "c:\test.pdf", "-cpdf -log C:\\htmlconverter.log")
        'Try
        'Dim mObj As HTMLConverter.HTMLConverterX
        'mObj = New HTMLConverter.HTMLConverterXClass()
        'Dim cdm As comm
        '    Dim runner As New System.Diagnostics.Process()
        '    Dim RunExe As String
        '    Dim ConHtml As String
        '    Dim ConPDF As String
        '    Dim CommArgs As String

        '    runner.StartInfo.UseShellExecute = False
        '    runner.StartInfo.FileName = "cmd.exe"
        '    RunExe = """C:\Program Files\TotalHTMLConverterX\HTMLConverterX.exe"""
        '    ConHtml = "C:\invdetails.htm"
        '    ConPDF = "C:\test2.pdf"
        '    'CommArgs = RunExe & " " & ConHtml & " " & ConPDF & " -c PDF"
        '    CommArgs = "Copy c:\test1.pdf c:\test2.pdf"

        '    runner.StartInfo.Arguments = CommArgs
        '    runner.StartInfo.RedirectStandardOutput = True
        '    runner.Start()
        'Catch ex As Exception
        '    Response.Write("Error Message" & ex.Message)

        'End Try

        'Response.End()
        Dim HRow1 As New TableRow
        Dim HCell1 As New TableCell
        Dim HCell2 As New TableCell
        Dim OriRefNumber As String = String.Empty
        HRow1.CssClass = "tblbg2"
        'Try
        Dim strConn As String
        Dim strCategory As String
        strCategory = ""
        Dim t2 As New Table
        t2.Style("width") = "100%"
        t2.BorderWidth = 2
        t2.GridLines = GridLines.Both
        Dim I As Integer = 0
        'I = 0
        strConn = System.Configuration.ConfigurationManager.AppSettings("ETSCon")
        Dim strQuery As String
        Dim DLines As String
        Dim DSLines As String
        Dim BillUnits As String
        Dim BillSUnits As String
        Dim BillAmt As String
        Dim BillOtherAmt As String
        Dim BillTotAmount As String
        Dim Spriority As Boolean = False
        'Spriority = False
        Dim STSTDLines As String
        Dim STSTDSLines As String
        Dim STBillUnits As String
        Dim STBillSUnits As String
        Dim STBillAmt As String
        Dim STBillOtherAmt As String
        Dim STBillTotAmount As String
        Dim subActID As String
        Dim SepIrecFound As Boolean = False
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
        'Dim DT As New DataTable
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
        BillAmount = "0.00"

        Dim AutoID As String
        Dim ItemDescr As String
        Dim ItemID As String
        Dim quantity As Double
        Dim amount As Double
        Dim totamount As Double
        Dim strBillCycle As String
        Dim ActBillCycle As Boolean = False
        Dim tabletext As String
        Dim VasStartDate As Date
        Dim VasEndDate As Date
        Dim Recupdate As Boolean = False
        Dim WType As String = String.Empty
        Dim WTMode As Boolean = False
        Dim BillAccID As String
        Dim RefNumber As String = String.Empty
        Dim PayTerm As String = String.Empty
        Dim DelMode As String = String.Empty
        Dim DispPeriod As String = String.Empty
        Dim AccountName As String
        Dim BillActnumber As String
        Dim GroupAccountName As String = String.Empty
        Dim DueDate As Date
        Dim BillEMail As String
        Dim boolEMail As String
        Dim boolEFax As String
        Dim TxnDate As Date
        Dim DC1 As New DataColumn("RefNumber")
        Dim DC2 As New DataColumn("Customer")
        Dim DC3 As New DataColumn("SalesTerm")
        Dim DC4 As New DataColumn("TxnDate")
        Dim DC5 As New DataColumn("DueDate")
        Dim DC6 As New DataColumn("BillEmail")
        Dim DC7 As New DataColumn("ToBePrinted")
        Dim DC8 As New DataColumn("ToBeEmailed")
        Dim DC9 As New DataColumn("LineItem")
        Dim DC10 As New DataColumn("LineQty")
        Dim DC11 As New DataColumn("LineDesc")
        Dim DC12 As New DataColumn("LineUnitPrice")
        Dim DC13 As New DataColumn("Msg")
        ' Dim DC14 As New DataColumn("AccountName")
        Dim DC15 As New DataColumn("BillActNumber")
        Dim RecCount As Integer = 0
        DT.Columns.Add(DC1)
        DT.Columns.Add(DC2)
        DT.Columns.Add(DC3)
        DT.Columns.Add(DC4)
        DT.Columns.Add(DC5)
        DT.Columns.Add(DC13)
        DT.Columns.Add(DC6)
        DT.Columns.Add(DC7)
        DT.Columns.Add(DC8)
        DT.Columns.Add(DC9)
        DT.Columns.Add(DC10)
        DT.Columns.Add(DC11)
        DT.Columns.Add(DC12)
        ' DT.Columns.Add(DC14)
        DT.Columns.Add(DC15)
        Dim BoolGrpAccount As Boolean = False


        strQuery = "Select CASE WHEN G.GrpActID IS NOT NULL THEN 'True' ELSE 'False' END ISGroupAccount, G.GrpActName, ISNULL(G.[BillActNumber], A.BillActNumber) AS BillACtNumber, A.Accountid, IsNull(A.Sepinvoice, 'False') AS SepInvoice, BA.Mode,BA.RefNumber,A.BillEMail, A.DelMode, A.PayTerm ,A.AccountName, CASE WHEN G.[GrpActID] IS NOT NULL THEN  G.GrpActName ELSE A.BillActNumber + ' - ' + A.AccountName END as Description, A.BillActNumber, C.priority, C.Description as CateDescr, BA.MinBilling, BA.BillAccID, BA.Posted, BA.BillMonth, BA.BillYear, BA.BillCycle, isnull(BA.Cycle, 'False') AS Cycle, BA.WTMode "
        strQuery = strQuery & " from tblaccounts A "
        strQuery = strQuery & " INNER JOIN (Select * from AdminSecureweb.dbo.tblBillAccounts) BA"
        strQuery = strQuery & " on BA.AccountID = A.AccountID"
        strQuery = strQuery & " LEFT OUTER JOIN [ADMINETS].[dbo].[tblGrpAccounts] G"
        strQuery = strQuery & " on G.GrpActID = A.GrpBillActID"

        strQuery = strQuery & " LEFT OUTER JOIN (Select * from tblActCategories) C"
        strQuery = strQuery & " on C.category = A.Category  where A.contractorid ='" & Session("contractorid").ToString & "' AND BA.BillMonth = '" & DLMonth.SelectedValue & "' and BillYear ='" & DLYear.SelectedValue & "' and BillCycle='" & DLCycle.SelectedValue & "' "
        If DLStatus.SelectedValue = "Posted" Then
            strQuery = strQuery & " AND BA.Posted = 'True' "
        ElseIf DLStatus.SelectedValue = "NotPosted" Then
            strQuery = strQuery & " AND (BA.Posted = 'False' OR BA.Posted Is NULL ) "
        End If
        strQuery = strQuery & " Order by G.[GrpActName], A.accountName "
        'Response.Write(strQuery)
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
                    RecCount = 0
                    SepIrecFound = False
                    If DRRec("ISGroupAccount") Then
                        BoolGrpAccount = True
                        If Not GroupAccountName = DRRec("GrpActName").ToString Then
                            RefNumber = DRRec("RefNumber").ToString
                        End If
                    Else
                        BoolGrpAccount = False
                        RefNumber = DRRec("RefNumber").ToString
                    End If

                    If DRRec("WTMode").ToString.ToLower = "true" Then
                        WTMode = True
                    Else
                        WTMode = False
                    End If

                    If IsNumeric(DRRec("Payterm").ToString) Then
                        DueDate = DateAdd(DateInterval.Day, DRRec("Payterm"), TxnDate)
                    Else
                        DueDate = DateAdd(DateInterval.Day, 65, TxnDate)
                    End If
                    If DRRec("DelMode").ToString.Trim = "EMail" Then
                        boolEMail = "Y"
                        boolEFax = "N"
                    ElseIf DRRec("DelMode").ToString.Trim = "EFax" Then
                        boolEFax = "Y"
                        boolEMail = "N"
                    Else
                        boolEMail = "N"
                        boolEFax = "N"
                    End If
                    'Response.Write(DRRec("DelMode").ToString.Trim)
                    BillAccID = DRRec("BillAccID").ToString
                    BillActnumber = DRRec("BillACtNumber").ToString
                    AccountName = DRRec("AccountName").ToString
                    GroupAccountName = DRRec("GrpActName").ToString
                    BillEMail = DRRec("BillEMail").ToString
                    AccountID = DRRec("AccountID").ToString

                    BillMonth = DRRec("BillMonth")
                    BillYear = DRRec("BillYear")
                    BillCycle = DRRec("BillCycle")
                    BoolCycle = DRRec("Cycle").ToString
                    'Response.Write(BoolCycle)
                    If IsNumeric(DRRec("Payterm").ToString) Then
                        PayTerm = "Net " & DRRec("PayTerm").ToString
                    Else
                        PayTerm = String.Empty
                    End If

                    Recupdate = False
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

                    If BillCycle = "2" Then
                        Billdate = DateAdd(DateInterval.Month, 1, C1SDate)
                    Else
                        Billdate = C1EDate
                    End If
                    C1SDate = BillMonth & "/1/" & BillYear
                    C2SDate = BillMonth & "/16/" & BillYear
                    C1EDate = BillMonth & "/15/" & BillYear
                    C2EDate = DateAdd(DateInterval.Day, -1, DateAdd(DateInterval.Month, 1, C1SDate))
                    If DLCycle.SelectedValue = 1 Then
                        TxnDate = DLMonth.SelectedValue & "/15/" & DLYear.SelectedValue
                        DispPeriod = " (" & C1SDate.ToString("MMM dd - ") & C1EDate.ToString("MMM dd") & ") "
                    Else
                        If BoolCycle = True Then
                            DispPeriod = " (" & C2SDate.ToString("MMM dd - ") & C2EDate.ToString("MMM dd") & ") "
                        Else
                            DispPeriod = " (" & C1SDate.ToString("MMM dd - ") & C2EDate.ToString("MMM dd") & ") "
                        End If
                        TxnDate = DLMonth.SelectedValue & "/1/" & DLYear.SelectedValue
                        TxnDate = DateAdd(DateInterval.Month, 1, TxnDate)
                        TxnDate = DateAdd(DateInterval.Day, -1, TxnDate)
                    End If
                    Mode = Trim(DRRec("Mode").ToString)
                    ActName = DRRec("Description").ToString
                    HCell1.Text = ActName
                    'Response.Write(ActName)
                    If Mode = "DC" Or Mode = "LC" Or Mode = "DV" Or Mode = "TW" Then
                        strQuery = "Select  B.SubActID, B.SubActName,  B.Rate, B.MiscRate, B.StatRate, L.MethodName, isnull(T1.Lines,0) AS BillUnits, isnull(T2.Lines,0) AS BillSUnits,  isnull(T1.stdLines,0) AS stdLines, isnull(T2.stdLines,0) AS stdSLines, isnull(T1.Amt,0) AS BillAmt    "
                        strQuery = strQuery & " from AdminSecureweb.dbo.tblAccBillDetails B"
                        strQuery = strQuery & " LEFT OUTER JOIN (Select TrackID, MethodName from AdminETS.dbo.tblLCMethodology ) L"
                        strQuery = strQuery & " on L.TrackID = B.LCMethodID "
                        strQuery = strQuery & " INNER JOIN (Select BillAccID, SubActID, sum(unit) AS Lines, sum(amount) AS Amt, sum(stdLCCount) AS stdLines from tblBillingLines where BillAccID = '" & BillAccID & "' and priority='False' Group by BillAccID, SubActID) T1"
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



                    Dim SQLCmd1 As New SqlCommand(strQuery, New SqlConnection(strConn))
                    Try
                        SQLCmd1.Connection.Open()
                        Dim DRRec1 As SqlDataReader = SQLCmd1.ExecuteReader()
                        If DRRec1.HasRows Then
                            While (DRRec1.Read)
                                Spriority = False
                                'strQuery = "Select NewID() as UID"
                                'Dim SQLCmdUID As New SqlCommand(strQuery, New SqlConnection(strConn))
                                'SQLCmdUID.Connection.Open()
                                'AutoID = SQLCmdUID.ExecuteScalar.ToString
                                'SQLCmdUID.Connection.Close()
                                subActID = DRRec1("subActID").ToString
                                SubActName = DRRec1("SubActName").ToString
                                'DvRate = DRRec1("Rate").ToString
                                If WTMode = True Then
                                    DvRate = DRRec1("WTRate").ToString
                                Else
                                    DvRate = DRRec1("Rate").ToString
                                End If
                                MiscRate = DRRec1("MiscRate").ToString
                                StatRate = DRRec1("STATRate").ToString




                                'DLines += CInt(FormatNumber(DRRec1("stdLines"), 0))
                                'DSLines += CInt(FormatNumber(DRRec1("stdSLines"), 0))
                                BillUnits = FormatNumber(DRRec1("BillUnits"), 2)
                                BillSUnits = FormatNumber(DRRec1("BillSUnits"), 2)
                                'BillAmt += CDbl(FormatNumber(DRRec1("BillAmt"), 2))
                                'Cell5.Text = CInt(FormatNumber(DRRec1("stdLines"), 0))
                                'Cell6.Text = CInt(FormatNumber(DRRec1("stdSLines"), 0))
                                'Cell7.Text = CInt(FormatNumber(DRRec1("BillUnits"), 0))
                                'Cell8.Text = CInt(FormatNumber(DRRec1("BillSUnits"), 0))
                                'Cell9.Text = CDbl(FormatNumber(DRRec1("BillAmt"), 2))
                                'Response.Write(BillUnits)
                                If Mode = "S" Or Mode = "" Then

                                    If DRRec1("MethodName") = "PerDictator" Then
                                        ItemID = "09541ff5-aa07-45e4-8d65-bb9180b60133"
                                        strQuery = "Select count(*) as NumDict from AdminSecureweb.dbo.AssignDic where GrpDicID = '" & DRRec1("SubActID").ToString & "'  "
                                        'strQuery = "Select count(*) as NumDict from (Select M.DictatorID as Numdict from AdminETS.dbo.tbltranscriptionMain M INNER Join tblBillingLines T ON T.TranscriptionID = M.TranscriptionID where  T.BillAccID = '" & BillAccID & "'  Group by M.dictatorID)T1 "


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
                                                    BillAmount += FormatNumber(NumDict * DvRate, 2)
                                                    If BoolGrpAccount Then
                                                        ItemDescr = "Previous Period Transcription Activity" & DispPeriod & "(" & AccountName & ")"
                                                    Else
                                                        ItemDescr = "Previous Period Transcription Activity" & DispPeriod
                                                    End If

                                                    quantity = NumDict
                                                    amount = FormatNumber(DvRate, 6)
                                                    totamount = FormatNumber(NumDict * DvRate, 2)
                                                    AddRow(RefNumber, ActName, PayTerm, TxnDate, DueDate, String.Empty, BillEMail, boolEMail, boolEFax, "MT-B", quantity, ItemDescr, amount, BillActnumber)
                                                End If

                                            End If
                                        Finally
                                            If SQLCmd1.Connection.State = System.Data.ConnectionState.Open Then
                                                SQLCmd1.Connection.Close()
                                                SQLCmd1 = Nothing
                                            End If
                                        End Try
                                    Else

                                        ItemID = "09541ff5-aa07-45e4-8d65-bb9180b60133"
                                        BillAmount += FormatNumber(BillUnits * DvRate, 2)
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
                                            If BoolGrpAccount Then
                                                ItemDescr = "Previous Period Transcription Activity" & DispPeriod & "(" & AccountName & ")" & "(Worktype: " & WType & ")"
                                            Else
                                                ItemDescr = "Previous Period Transcription Activity" & DispPeriod & "(Worktype: " & WType & ")"
                                            End If

                                        Else
                                            If BoolGrpAccount Then
                                                ItemDescr = "Previous Period Transcription Activity" & DispPeriod & "(" & AccountName & ")"
                                            Else
                                                ItemDescr = "Previous Period Transcription Activity" & DispPeriod
                                            End If

                                        End If

                                        quantity = FormatNumber(BillUnits, 2)
                                        amount = FormatNumber(DvRate, 6)
                                        totamount = FormatNumber(BillUnits * DvRate, 2)
                                        AddRow(RefNumber, ActName, PayTerm, TxnDate, DueDate, String.Empty, BillEMail, boolEMail, boolEFax, "MT-B", quantity, ItemDescr, amount, BillActnumber)
                                        If BillSUnits > 0 And Recupdate = False Then
                                            Recupdate = True
                                            Spriority = True
                                            ItemID = "da208b70-b5d1-4d4c-aa41-017c2568cd47"
                                            If BoolGrpAccount Then
                                                ItemDescr = "Previous Period Transcription Activity" & DispPeriod & "(" & AccountName & ")" & "- STAT Lines"
                                            Else
                                                ItemDescr = "Previous Period Transcription Activity" & DispPeriod & "- STAT Lines"
                                            End If

                                            BillAmount += FormatNumber(BillSUnits * StatRate, 2)
                                            quantity = FormatNumber(BillSUnits, 2)
                                            amount = FormatNumber(StatRate, 6)
                                            totamount = FormatNumber(BillSUnits * StatRate, 2)

                                            AddRow(RefNumber, ActName, PayTerm, TxnDate, DueDate, String.Empty, BillEMail, boolEMail, boolEFax, "MT-S", quantity, ItemDescr, amount, BillActnumber)
                                        End If
                                    End If
                                ElseIf Mode = "DV" Then
                                    If DRRec1("MethodName") = "PerDictator" Then
                                        ItemID = "09541ff5-aa07-45e4-8d65-bb9180b60133"
                                        strQuery = "Select count(*) as NumDict from AdminSecureweb.dbo.AssignDic where GrpDicID = '" & DRRec1("SubActID").ToString & "'  "
                                        'strQuery = "Select count(*) as NumDict from (Select M.DictatorID as Numdict from AdminETS.dbo.tbltranscriptionMain M INNER Join tblBillingLines T ON T.TranscriptionID = M.TranscriptionID where  T.BillAccID = '" & BillAccID & "' and T.SubActID = '" & DRRec1("SubActID").ToString & "' Group by M.dictatorID)T1 "
                                        'Response.Write(strQuery)

                                        Dim SQLCmd2 As New SqlCommand(strQuery, New SqlConnection(strConn))
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
                                                If BoolGrpAccount Then
                                                    ItemDescr = "Previous Period Transcription Activity" & DispPeriod & "(" & AccountName & ")" & "(Device: " & SubActName & ")"
                                                Else
                                                    ItemDescr = "Previous Period Transcription Activity" & DispPeriod & "(Device: " & SubActName & ")"
                                                End If

                                                quantity = NumDict
                                                amount = FormatNumber(DvRate, 6)
                                                totamount = FormatNumber(NumDict * DvRate, 2)
                                                AddRow(RefNumber, ActName, PayTerm, TxnDate, DueDate, String.Empty, BillEMail, boolEMail, boolEFax, "MT-B", quantity, ItemDescr, amount, BillActnumber)


                                            End If

                                        End If
                                        If SQLCmd2.Connection.State = ConnectionState.Open Then
                                            SQLCmd2.Connection.Close()
                                        End If
                                    Else

                                        If SubActName = "Telephone" Then
                                            ItemID = "09541ff5-aa07-45e4-8d65-bb9180b60133"
                                            BillAmount = FormatNumber(BillUnits * DvRate, 2)
                                            If BoolGrpAccount Then
                                                ItemDescr = "Previous Period Transcription Activity" & DispPeriod & "(" & AccountName & ")" & "(Device: " & SubActName & ")"
                                            Else
                                                ItemDescr = "Previous Period Transcription Activity" & DispPeriod & "(Device: " & SubActName & ")"
                                            End If

                                            quantity = FormatNumber(BillUnits, 2)
                                            amount = FormatNumber(DvRate, 6)
                                            totamount = FormatNumber(BillUnits * DvRate, 2)
                                            AddRow(RefNumber, ActName, PayTerm, TxnDate, DueDate, String.Empty, BillEMail, boolEMail, boolEFax, "MT-B", quantity, ItemDescr, amount, BillActnumber)
                                        Else
                                            ItemID = "09541ff5-aa07-45e4-8d65-bb9180b60133"
                                            BillAmount = FormatNumber(BillUnits * MiscRate, 2)
                                            If BoolGrpAccount Then
                                                ItemDescr = "Previous Period Transcription Activity" & DispPeriod & "(" & AccountName & ")" & "(Device: " & SubActName & ")"
                                            Else
                                                ItemDescr = "Previous Period Transcription Activity" & DispPeriod & "(Device: " & SubActName & ")"
                                            End If

                                            quantity = FormatNumber(BillUnits, 2)
                                            amount = FormatNumber(MiscRate, 6)
                                            totamount = FormatNumber(BillUnits * MiscRate, 2)
                                            AddRow(RefNumber, ActName, PayTerm, TxnDate, DueDate, String.Empty, BillEMail, boolEMail, boolEFax, "MT-B", quantity, ItemDescr, amount, BillActnumber)
                                        End If

                                        If BillSUnits > 0 Then
                                            Spriority = True
                                            'Response.Write(BillSUnits)
                                            ItemID = "da208b70-b5d1-4d4c-aa41-017c2568cd47"
                                            If BoolGrpAccount Then
                                                ItemDescr = "Previous Period Transcription Activity" & DispPeriod & "(" & AccountName & ")" & "- STAT Lines (Device: " & SubActName & ")"
                                            Else
                                                ItemDescr = "Previous Period Transcription Activity" & DispPeriod & "- STAT Lines (Device: " & SubActName & ")"
                                            End If

                                            BillAmount += FormatNumber(BillSUnits * StatRate, 2)
                                            quantity = FormatNumber(BillSUnits, 2)
                                            amount = FormatNumber(StatRate, 6)
                                            totamount = FormatNumber(BillSUnits * StatRate, 2)
                                            AddRow(RefNumber, ActName, PayTerm, TxnDate, DueDate, String.Empty, BillEMail, boolEMail, boolEFax, "MT-S", quantity, ItemDescr, amount, BillActnumber)
                                        End If
                                    End If

                                ElseIf Mode = "LC" Then
                                    If DRRec1("MethodName") = "PerDictator" Then
                                        ItemID = "09541ff5-aa07-45e4-8d65-bb9180b60133"
                                        strQuery = "Select count(*) as NumDict from AdminSecureweb.dbo.AssignDic where GrpDicID = '" & DRRec1("SubActID").ToString & "'  "
                                        'strQuery = "Select count(*) as NumDict from (Select M.DictatorID as Numdict from AdminETS.dbo.tbltranscriptionMain M INNER Join tblBillingLines T ON T.TranscriptionID = M.TranscriptionID where  T.BillAccID = '" & BillAccID & "' and T.SubActID = '" & DRRec1("SubActID").ToString & "' Group by M.dictatorID)T1 "
                                        'Response.Write(strQuery)

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
                                                    If BoolGrpAccount Then
                                                        ItemDescr = "Previous Period Transcription Activity" & DispPeriod & "(" & AccountName & ")" & "(Location: " & SubActName & ")"
                                                    Else
                                                        ItemDescr = "Previous Period Transcription Activity" & DispPeriod & "(Location: " & SubActName & ")"
                                                    End If

                                                    quantity = NumDict
                                                    amount = FormatNumber(DvRate, 6)
                                                    totamount = FormatNumber(NumDict * DvRate, 2)
                                                    AddRow(RefNumber, ActName, PayTerm, TxnDate, DueDate, String.Empty, BillEMail, boolEMail, boolEFax, "MT-B", quantity, ItemDescr, amount, BillActnumber)


                                                End If

                                            End If
                                        Finally
                                            If SQLCmd2.Connection.State = System.Data.ConnectionState.Open Then
                                                SQLCmd2.Connection.Close()
                                                SQLCmd2 = Nothing
                                            End If
                                        End Try
                                    Else


                                        ItemID = "09541ff5-aa07-45e4-8d65-bb9180b60133"
                                        BillAmount = FormatNumber(BillUnits * DvRate, 2)
                                        If BoolGrpAccount Then
                                            ItemDescr = "Previous Period Transcription Activity" & DispPeriod & "(" & AccountName & ")" & "(Location: " & SubActName & ")"
                                        Else
                                            ItemDescr = "Previous Period Transcription Activity" & DispPeriod & "(Location: " & SubActName & ")"
                                        End If

                                        quantity = FormatNumber(BillUnits, 2)
                                        amount = FormatNumber(DvRate, 6)
                                        totamount = FormatNumber(BillUnits * DvRate, 2)
                                        AddRow(RefNumber, ActName, PayTerm, TxnDate, DueDate, String.Empty, BillEMail, boolEMail, boolEFax, "MT-B", quantity, ItemDescr, amount, BillActnumber)


                                        If BillSUnits > 0 Then
                                            Spriority = True
                                            'Response.Write(BillSUnits)
                                            ItemID = "da208b70-b5d1-4d4c-aa41-017c2568cd47"
                                            If BoolGrpAccount Then
                                                ItemDescr = "Previous Period Transcription Activity" & DispPeriod & "(" & AccountName & ")" & "- STAT Lines (Location: " & SubActName & ")"
                                            Else
                                                ItemDescr = "Previous Period Transcription Activity" & DispPeriod & "- STAT Lines (Location: " & SubActName & ")"
                                            End If

                                            BillAmount += FormatNumber(BillSUnits * StatRate, 2)
                                            quantity = FormatNumber(BillSUnits, 2)
                                            amount = FormatNumber(StatRate, 6)
                                            totamount = FormatNumber(BillSUnits * StatRate, 2)
                                            AddRow(RefNumber, ActName, PayTerm, TxnDate, DueDate, String.Empty, BillEMail, boolEMail, boolEFax, "MT-S", quantity, ItemDescr, amount, BillActnumber)
                                        End If
                                    End If

                                ElseIf Mode = "DC" Then



                                    strQuery = "Select SepInvoice, GrpDicName,Description from AdminSecureweb.dbo.GrpDictators where SepInvoice = 'True' and GrpDicID = '" & DRRec1("SubActID").ToString & "'  "
                                    'strQuery = "Select count(*) as NumDict from (Select M.DictatorID as Numdict from AdminETS.dbo.tbltranscriptionMain M INNER Join tblBillingLines T ON T.TranscriptionID = M.TranscriptionID where  T.BillAccID = '" & BillAccID & "'  and T.SubActID = '" & DRRec1("SubActID").ToString & "' Group by M.dictatorID)T1 "
                                    'Response.Write(strQuery)

                                    Dim SQLCmdS As New SqlCommand(strQuery, New SqlConnection(strConn))
                                    SQLCmdS.Connection.Open()
                                    Dim DRRecS As SqlDataReader = SQLCmdS.ExecuteReader()
                                    If DRRecS.HasRows Then
                                        If DRRecS.Read Then
                                            SepIrecFound = True
                                            RecCount = RecCount + 1
                                            OriRefNumber = RefNumber
                                            RefNumber = RefNumber & RecCount
                                            'ActName = DRRecS("Description").ToString
                                        End If
                                    End If
                                    If SQLCmdS.Connection.State = ConnectionState.Open Then
                                        SQLCmdS.Connection.Close()
                                    End If

                                    If DRRec1("MethodName") = "PerDictator" Then
                                        ItemID = "09541ff5-aa07-45e4-8d65-bb9180b60133"
                                        strQuery = "Select count(*) as NumDict from AdminSecureweb.dbo.AssignDic where GrpDicID = '" & DRRec1("SubActID").ToString & "'  "
                                        'strQuery = "Select count(*) as NumDict from (Select M.DictatorID as Numdict from AdminETS.dbo.tbltranscriptionMain M INNER Join tblBillingLines T ON T.TranscriptionID = M.TranscriptionID where  T.BillAccID = '" & BillAccID & "'  and T.SubActID = '" & DRRec1("SubActID").ToString & "' Group by M.dictatorID)T1 "
                                        'Response.Write(strQuery)

                                        Dim SQLCmd2 As New SqlCommand(strQuery, New SqlConnection(strConn))
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
                                                If BoolGrpAccount Then
                                                    ItemDescr = "Previous Period Transcription Activity" & DispPeriod & "(" & AccountName & ")" & "(Group Name: " & SubActName & ")"
                                                Else
                                                    ItemDescr = "Previous Period Transcription Activity" & DispPeriod & "(Group Name: " & SubActName & ")"
                                                End If

                                                quantity = NumDict
                                                amount = FormatNumber(DvRate, 6)
                                                totamount = FormatNumber(NumDict * DvRate, 2)
                                                AddRow(RefNumber, ActName, PayTerm, TxnDate, DueDate, String.Empty, BillEMail, boolEMail, boolEFax, "MT-B", quantity, ItemDescr, amount, BillActnumber)


                                            End If

                                        End If
                                        If SQLCmd2.Connection.State = ConnectionState.Open Then
                                            SQLCmd2.Connection.Close()
                                        End If

                                    Else
                                        ItemID = "09541ff5-aa07-45e4-8d65-bb9180b60133"
                                        BillAmount = FormatNumber(BillUnits * DvRate, 2)
                                        If BoolGrpAccount Then
                                            ItemDescr = "Previous Period Transcription Activity" & DispPeriod & "(" & AccountName & ")" & "(Group Name: " & SubActName & ")"
                                        Else
                                            ItemDescr = "Previous Period Transcription Activity" & DispPeriod & "(Group Name: " & SubActName & ")"
                                        End If

                                        quantity = FormatNumber(BillUnits, 2)
                                        amount = FormatNumber(DvRate, 6)
                                        totamount = FormatNumber(BillUnits * DvRate, 2)
                                        AddRow(RefNumber, ActName, PayTerm, TxnDate, DueDate, String.Empty, BillEMail, boolEMail, boolEFax, "MT-B", quantity, ItemDescr, amount, BillActnumber)

                                        If BillSUnits > 0 Then
                                            'Response.Write(BillSUnits)
                                            Spriority = True
                                            ItemID = "da208b70-b5d1-4d4c-aa41-017c2568cd47"
                                            If BoolGrpAccount Then
                                                ItemDescr = "Previous Period Transcription Activity" & DispPeriod & "(" & AccountName & ")" & "- STAT Lines (Group Name: " & SubActName & ")"
                                            Else
                                                ItemDescr = "Previous Period Transcription Activity" & DispPeriod & "- STAT Lines (Group Name: " & SubActName & ")"
                                            End If

                                            BillAmount += FormatNumber(BillSUnits * StatRate, 2)
                                            quantity = FormatNumber(BillSUnits, 2)
                                            amount = FormatNumber(StatRate, 6)
                                            totamount = FormatNumber(BillSUnits * StatRate, 2)
                                            AddRow(RefNumber, ActName, PayTerm, TxnDate, DueDate, String.Empty, BillEMail, boolEMail, boolEFax, "MT-S", quantity, ItemDescr, amount, BillActnumber)
                                        End If
                                    End If
                                    If SepIrecFound = True Then
                                        RefNumber = OriRefNumber

                                    End If
                                ElseIf Mode = "TW" Then



                                    strQuery = "Select SepInvoice, GrpTempName,Description from AdminSecureweb.dbo.tblGrpTemplates where SepInvoice = 'True' and GrpTempID = '" & DRRec1("SubActID").ToString & "'  "
                                    'strQuery = "Select count(*) as NumDict from (Select M.DictatorID as Numdict from AdminETS.dbo.tbltranscriptionMain M INNER Join tblBillingLines T ON T.TranscriptionID = M.TranscriptionID where  T.BillAccID = '" & BillAccID & "'  and T.SubActID = '" & DRRec1("SubActID").ToString & "' Group by M.dictatorID)T1 "
                                    'Response.Write(strQuery)

                                    Dim SQLCmdS As New SqlCommand(strQuery, New SqlConnection(strConn))
                                    SQLCmdS.Connection.Open()
                                    Dim DRRecS As SqlDataReader = SQLCmdS.ExecuteReader()
                                    If DRRecS.HasRows Then
                                        If DRRecS.Read Then
                                            SepIrecFound = True
                                            RecCount = RecCount + 1
                                            OriRefNumber = RefNumber
                                            RefNumber = RefNumber & RecCount
                                            'ActName = DRRecS("Description").ToString
                                        End If
                                    End If
                                    If SQLCmdS.Connection.State = ConnectionState.Open Then
                                        SQLCmdS.Connection.Close()
                                    End If

                                    If DRRec1("MethodName") = "PerDictator" Then
                                        ItemID = "09541ff5-aa07-45e4-8d65-bb9180b60133"
                                        strQuery = "Select count(*) as NumDict from AdminSecureweb.dbo.AssignDic where GrpDicID = '" & DRRec1("SubActID").ToString & "'  "
                                        'strQuery = "Select count(*) as NumDict from (Select M.DictatorID as Numdict from AdminETS.dbo.tbltranscriptionMain M INNER Join tblBillingLines T ON T.TranscriptionID = M.TranscriptionID where  T.BillAccID = '" & BillAccID & "'  and T.SubActID = '" & DRRec1("SubActID").ToString & "' Group by M.dictatorID)T1 "
                                        'Response.Write(strQuery)

                                        Dim SQLCmd2 As New SqlCommand(strQuery, New SqlConnection(strConn))
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
                                                If BoolGrpAccount Then
                                                    ItemDescr = "Previous Period Transcription Activity" & DispPeriod & "(" & AccountName & ")" & "(Group Name: " & SubActName & ")"
                                                Else
                                                    ItemDescr = "Previous Period Transcription Activity" & DispPeriod & "(Group Name: " & SubActName & ")"
                                                End If

                                                quantity = NumDict
                                                amount = FormatNumber(DvRate, 6)
                                                totamount = FormatNumber(NumDict * DvRate, 2)
                                                AddRow(RefNumber, ActName, PayTerm, TxnDate, DueDate, String.Empty, BillEMail, boolEMail, boolEFax, "MT-B", quantity, ItemDescr, amount, BillActnumber)


                                            End If

                                        End If
                                        If SQLCmd2.Connection.State = ConnectionState.Open Then
                                            SQLCmd2.Connection.Close()
                                        End If

                                    Else
                                        ItemID = "09541ff5-aa07-45e4-8d65-bb9180b60133"
                                        BillAmount = FormatNumber(BillUnits * DvRate, 2)
                                        If BoolGrpAccount Then
                                            ItemDescr = "Previous Period Transcription Activity" & DispPeriod & "(" & AccountName & ")" & "(Group Name: " & SubActName & ")"
                                        Else
                                            ItemDescr = "Previous Period Transcription Activity" & DispPeriod & "(Group Name: " & SubActName & ")"
                                        End If

                                        quantity = FormatNumber(BillUnits, 2)
                                        amount = FormatNumber(DvRate, 6)
                                        totamount = FormatNumber(BillUnits * DvRate, 2)
                                        AddRow(RefNumber, ActName, PayTerm, TxnDate, DueDate, String.Empty, BillEMail, boolEMail, boolEFax, "MT-B", quantity, ItemDescr, amount, BillActnumber)

                                        If BillSUnits > 0 Then
                                            'Response.Write(BillSUnits)
                                            Spriority = True
                                            ItemID = "da208b70-b5d1-4d4c-aa41-017c2568cd47"
                                            If BoolGrpAccount Then
                                                ItemDescr = "Previous Period Transcription Activity" & DispPeriod & "(" & AccountName & ")" & "- STAT Lines (Group Name: " & SubActName & ")"
                                            Else
                                                ItemDescr = "Previous Period Transcription Activity" & DispPeriod & "- STAT Lines (Group Name: " & SubActName & ")"
                                            End If

                                            BillAmount += FormatNumber(BillSUnits * StatRate, 2)
                                            quantity = FormatNumber(BillSUnits, 2)
                                            amount = FormatNumber(StatRate, 6)
                                            totamount = FormatNumber(BillSUnits * StatRate, 2)
                                            AddRow(RefNumber, ActName, PayTerm, TxnDate, DueDate, String.Empty, BillEMail, boolEMail, boolEFax, "MT-S", quantity, ItemDescr, amount, BillActnumber)
                                        End If
                                    End If
                                    If SepIrecFound = True Then
                                        RefNumber = OriRefNumber

                                    End If

                                End If



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
                            VasEndDate = C1EDate
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
                    'If ActName = "370 - Saratoga Cardiology Associates" Then
                    '    Response.Write(strQuery)
                    'End If

                    Dim SQLCmd6 As New SqlCommand(strQuery, New SqlConnection(strConn))
                    Try
                        SQLCmd6.Connection.Open()
                        Dim DRRec6 As SqlDataReader = SQLCmd6.ExecuteReader()
                        If DRRec6.HasRows Then
                            While DRRec6.Read
                                Dim vRow1 As New TableRow
                                Dim vCell1 As New TableCell
                                Dim vCell2 As New TableCell
                                Dim vCell3 As New TableCell
                                Dim vCell4 As New TableCell
                                Dim vCell5 As New TableCell
                                Dim vCell6 As New TableCell
                                Billdate = DRRec6("ServiceDate").ToString

                                AddRow(RefNumber, ActName, PayTerm, TxnDate, DueDate, String.Empty, BillEMail, boolEMail, boolEFax, DRRec6("Item").ToString, DRRec6("Quantity").ToString, DRRec6("Descr").ToString, DRRec6("Amount").ToString, BillActnumber)


                            End While
                        End If
                        DRRec6.Close()
                    Finally
                        If SQLCmd6.Connection.State = System.Data.ConnectionState.Open Then
                            SQLCmd6.Connection.Close()
                            SQLCmd6 = Nothing
                        End If
                    End Try
                    'End If

                End While
            Else
                HCell2.Font.Bold = True
                HCell2.ForeColor = Drawing.Color.Firebrick
                HCell2.Text = "Not Posted"
            End If
        Finally
            If SQLCmd.Connection.State = System.Data.ConnectionState.Open Then
                SQLCmd.Connection.Close()
                SQLCmd = Nothing
            End If
        End Try
        MyDataGrid.DataSource = DT
        MyDataGrid.DataBind()
        'Catch ex As Exception
        '    Response.Write(ex.Message)
        '    'HCell2.Font.Bold = True
        '    'HCell2.ForeColor = Drawing.Color.Firebrick
        '    'HCell2.Text = "Not Posted"
        'End Try

    End Sub


    'Protected Sub InsertRec(ByVal AutoID As String, ByVal InvoiceID As String, ByVal AccountID As String, ByVal SubActID As String, ByVal Mode As String, ByVal Descr As String, ByVal itemid As String, ByVal quantity As Long, ByVal amount As Double, ByVal totamount As String, ByVal ServiceDate As Date, ByVal WTMode As String, ByVal WType As String)
    '    Dim strQuery As String
    '    Dim strConn As String
    '    strConn = System.Configuration.ConfigurationManager.AppSettings("ETSCon")
    '    If Mode = "LC" Or Mode = "DC" Or Mode = "DV" Then
    '        strQuery = "Insert Into AdminSecureweb.dbo.tblinvItemDet (AutoID, InvoiceID, AccountID, SubActID, Mode, Descr, itemid, quantity, amount, totamount, ServiceDate, Dateupdate) Values ('" & AutoID & "' , '" & InvoiceID & "' , '" & AccountID & "' , '" & SubActID & "' , 'Trans' , '" & Descr & "' , '" & itemid & "' , '" & quantity & "' , '" & amount & "' , '" & totamount & "' , '" & ServiceDate & "', '" & Now & "') "
    '    Else
    '        strQuery = "Insert Into AdminSecureweb.dbo.tblinvItemDet (AutoID, InvoiceID, AccountID,  Mode, Descr, itemid, quantity, amount, totamount, ServiceDate, Dateupdate, WTMode,WorkType) Values ('" & AutoID & "' , '" & InvoiceID & "' , '" & AccountID & "' , 'Trans' , '" & Descr & "' , '" & itemid & "' , '" & quantity & "' , '" & amount & "' , '" & totamount & "' , '" & ServiceDate & "', '" & Now & "','" & WTMode & "','" & WType & "') "
    '    End If
    '    '    Response.Write(strQuery)
    '    Dim SQLCmd5 As New SqlCommand(strQuery, New SqlConnection(strConn))
    '    SQLCmd5.Connection.Open()
    '    SQLCmd5.ExecuteNonQuery()
    '    SQLCmd5.Connection.Close()

    'End Sub

    'Protected Sub UpdateRec(ByVal InvItemID As String, ByVal BillAccID As String, ByVal SubActID As String, ByVal Priority As Boolean, ByVal Mode As String)
    '    Dim strConn As String
    '    strConn = System.Configuration.ConfigurationManager.AppSettings("ETSCon")
    '    Dim strQuery As String
    '    strQuery = "update AdminETS.dbo.tblBillingLines set InvItemID ='" & InvItemID & "' where BillAccID='" & BillAccID & "'  "
    '    If Mode = "LC" Or Mode = "DC" Or Mode = "DV" Then
    '        strQuery = strQuery & " and SubActID ='" & SubActID & "' "
    '    End If
    '    If Priority = True Then
    '        strQuery = strQuery & " and Priority ='" & Priority & "' "
    '    End If
    '    Dim SQLCmd5 As New SqlCommand(strQuery, New SqlConnection(strConn))
    '    SQLCmd5.Connection.Open()
    '    SQLCmd5.ExecuteNonQuery()
    '    SQLCmd5.Connection.Close()

    'End Sub
    Protected Sub AddRow(ByVal RefNumber As String, ByVal ActName As String, ByVal PayTerm As String, ByVal TxnDate As String, ByVal DueDate As String, ByVal Msg As String, ByVal BillEMail As String, ByVal boolEMail As String, ByVal boolEFax As String, ByVal Item As String, ByVal quantity As String, ByVal ItemDescr As String, ByVal amount As String, ByVal BillActNumber As String)
        Dim DR As DataRow = DT.NewRow
        DR(0) = RefNumber
        DR(1) = ActName
        DR(2) = PayTerm
        DR(3) = TxnDate
        DR(4) = DueDate
        DR(5) = String.Empty
        DR(6) = BillEMail
        DR(7) = boolEFax
        DR(8) = boolEMail
        DR(9) = Item
        DR(10) = quantity
        DR(11) = ItemDescr
        DR(12) = FormatNumber(amount, 6)
        DR(13) = BillActNumber
        DT.Rows.Add(DR)
        DR = Nothing
    End Sub

    Protected Sub Button1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button1.Click
        Dim filename As String
        filename = "InvoiceDetails " & Month(Now) & Day(Now) & Year(Now) & ".xls"
        Dim attachment As String = "attachment; filename=" & filename
        Response.ClearContent()
        Response.AddHeader("content-disposition", attachment)
        Response.ContentType = "application/ms-excel"
        Dim sw As New StringWriter()
        Dim htw As New HtmlTextWriter(sw)
        'If RBPage.SelectedValue = "AP" Then
        '    MyDataGrid.AllowPaging = False
        'ElseIf RBPage.SelectedValue = "CP" Then
        '    MyDataGrid.AllowPaging = True
        'Else
        '    MyDataGrid.AllowPaging = False
        'End If
        'MyDataGrid.AllowSorting = False
        'BindData(Hsort.Value, Horder.Value)
        'MyDataGrid.ShowCount = False
        PostBilling()
        Dim Table1 As New Table
        Table1.GridLines = GridLines.Both
        Table1.Font.Name = "Trebuchet MS"
        Table1.Font.Size = 8
        'Table1.CssClass = "common"
        'Table1.Font.Italic = True
        'Response.Write(MyDataGrid.HeaderRow.Cells.Count)
        'Response.End()
        Dim x As Integer
        If (Not (MyDataGrid.HeaderRow) Is Nothing) Then
            Dim TRow1 As New TableRow
            For x = 0 To MyDataGrid.HeaderRow.Cells.Count - 1
                'If MyDataGrid.Columns(x).Visible = True Then
                Dim TCell1 As New TableCell
                TCell1.Text = MyDataGrid.HeaderRow.Cells(x).Text
                TCell1.Font.Bold = True
                TCell1.BackColor = Drawing.Color.Gray
                TCell1.ForeColor = Drawing.Color.White
                TRow1.Cells.Add(TCell1)
                'End If
            Next
            Table1.Rows.Add(TRow1)
        End If
        Dim i As Integer
        Dim k As Integer
        Dim AltRec As Boolean = True
        k = 0
        For Each row As GridViewRow In MyDataGrid.Rows
            k = k + 1
            Dim TRow1 As New TableRow
            If row.RowIndex = 0 Then
                row.Font.Bold = True
                row.BackColor = Drawing.Color.Navy
                row.ForeColor = Drawing.Color.White
            ElseIf AltRec = True Then
                row.CssClass = "gridalt1"
                AltRec = False
            ElseIf AltRec = False Then
                row.CssClass = "gridalt2"
                AltRec = True
            End If
            For i = 0 To row.Cells.Count - 1
                ' If MyDataGrid.Columns(i).Visible = True Then
                Dim TCell1 As New TableCell
                TCell1.Text = row.Cells(i).Text
                TRow1.Cells.Add(TCell1)
                'End If
            Next
            Table1.Rows.Add(TRow1)
            If MyDataGrid.AllowPaging = True And MyDataGrid.PageSize = k Then
                Exit For
            End If
        Next
        Table1.RenderControl(htw)
        Response.Write(sw.ToString())
        Response.[End]()
    End Sub

    Protected Sub tblsubmit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles tblsubmit.Click
        PostBilling()
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            GetMyMonthList(DLMonth, True)
            GetMyYearList(DLYear, True)
        End If
    End Sub
End Class
