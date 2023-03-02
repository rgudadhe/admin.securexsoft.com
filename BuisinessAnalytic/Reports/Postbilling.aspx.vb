Imports System.Data.Sqlclient
Imports System.Data
Imports System.IO
Imports HTMLConverter


Partial Class Billing_Reports_Postbilling
    Inherits BasePage
    Public ProcFolder As String = Server.MapPath("../")

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
     
        'Dim mObj As HTMLConverter.HTMLConverterX
        'mObj = New HTMLConverter.HTMLConverterXClass()
        'mObj.Convert("http://ets.edictate.com/secureweb/billing/InvDetails.htm", "c:\test9.pdf", "-cpdf -log C:\\htmlconverter.log")

        'Dim process1 As New System.Diagnostics.Process()
        ''Dim sPath As String
        ''Dim Mode As String
        ''Dim LevelNo As String
        '' Set the directory where the file resides 
        'process1.StartInfo.WorkingDirectory = Server.MapPath("/HTMLConverter").ToString
        '' Set the filename name of the file you want to open 
        ''Response.Write(Server.MapPath("/ETS_Files").ToString & "\DSS_Sample.exe" & " " & Server.MapPath("/ETS_Files").ToString & "\test.dss$0.50$1.00$test.wav")
        'process1.StartInfo.FileName = "HTMLConverterX.exe"
        'process1.StartInfo.Arguments = "c:\invdetails.htm c:\test8.pdf -c PDF"
        ''Response.Write(sPath & "$" & Mode & "$" & LevelNo)
        ''Response.End()

        ''process1.StartInfo.Arguments = 
        ''process1.StartInfo.Arguments = 
        '' Start the process 
        'process1.Start()
        'process1.WaitForExit()
        'process1.Close()
        'process1.Dispose()

        'Dim runner As New System.Diagnostics.Process()
        'Dim RunExe As String
        'Dim ConHtml As String
        'Dim ConPDF As String
        'Dim CommArgs As String

        'runner.StartInfo.UseShellExecute = False
        'runner.StartInfo.FileName = "cmd.exe"
        'RunExe = "HTMLConverterX.exe"
        'ConHtml = "C:\invdetails.htm"
        'ConPDF = "C:\test7.pdf"
        ''CommArgs = RunExe & " " & ConHtml & " " & ConPDF & " -c PDF"
        'CommArgs = "Copy c:\test1.pdf c:\test7.pdf"
        'Response.Write(CommArgs)


        'runner.StartInfo.Arguments = CommArgs
        'runner.StartInfo.RedirectStandardOutput = True
        'runner.Start()

        Dim BillAccID As String
        Dim InvoiceID As String

        Dim InpString() As String = Split(Request("chBillAccID"), ",")
        Dim i As Integer
        For i = 0 To InpString.Length - 1
            Dim Inpsplit() As String
            Inpsplit = Split(InpString(i), "#")
            BillAccID = Inpsplit(0)
            InvoiceID = Inpsplit(1)
            PostBilling(BillAccID, InvoiceID)
        Next
    End Sub


    Protected Sub PostBilling(ByVal BillAccID As String, ByVal InvoiceID As String)
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
        HRow1.CssClass = "tblbg2"
        Try

        
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
            Dim Spriority As Boolean
            Spriority = False

            Dim STSTDLines As String
            Dim STSTDSLines As String
            Dim STBillUnits As String
            Dim STBillSUnits As String
            Dim STBillAmt As String
            Dim STBillOtherAmt As String
            Dim STBillTotAmount As String
            Dim subActID As String

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

            strQuery = "Select A.Accountid, BA.Mode, A.AccountName as Description, A.BillActNumber, C.priority, C.Description as CateDescr, BA.MinBilling, BA.BillAccID, BA.Posted, BA.BillMonth, BA.BillYear, BA.BillCycle, BA.Cycle, BA.WTMode "
            strQuery = strQuery & " from tblaccounts A "
            strQuery = strQuery & " INNER JOIN (Select * from AdminSecureweb.dbo.tblBillAccounts) BA"
            strQuery = strQuery & " on BA.AccountID = A.AccountID"
            strQuery = strQuery & " LEFT OUTER JOIN (Select * from tblActCategories) C"
            strQuery = strQuery & " on C.category = A.Category  where BA.BillAccID = '" & BillAccID & "' "
            'Response.Write(strQuery)
            'Response.End()


            Dim SQLCmd As New SqlCommand(strQuery, New SqlConnection(strConn))
            Try
                SQLCmd.Connection.Open()
                Dim DRRec As SqlDataReader = SQLCmd.ExecuteReader()
                If DRRec.HasRows Then
                    If DRRec.Read Then

                        DLines = 0
                        DSLines = 0
                        BillUnits = 0
                        BillSUnits = 0
                        BillAmt = 0
                        BillOtherAmt = 0
                        BillTotAmount = 0

                        If DRRec("WTMode").ToString.ToLower = "true" Then
                            WTMode = True
                        Else
                            WTMode = False
                        End If
                        BillAccID = DRRec("BillAccID").ToString
                        AccountID = DRRec("AccountID").ToString
                        BillMonth = DRRec("BillMonth")
                        BillYear = DRRec("BillYear")
                        BillCycle = DRRec("BillCycle")
                        BoolCycle = DRRec("Cycle").ToString
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
                        C1SDate = BillMonth & "/1/" & BillYear
                        C2SDate = BillMonth & "/16/" & BillYear
                        C1EDate = BillMonth & "/16/" & BillYear
                        C2EDate = DateAdd(DateInterval.Day, -1, DateAdd(DateInterval.Month, 1, C1SDate))
                        If BillCycle = "2" Then
                            Billdate = DateAdd(DateInterval.Month, 1, C1SDate)
                        Else
                            Billdate = C1EDate
                        End If

                        Mode = Trim(DRRec("Mode").ToString)
                        ActName = DRRec("Description").ToString
                        HCell1.Text = ActName
                        'If Mode = "DC" Or Mode = "LC" Or Mode = "DV" Then
                        If Mode = "DC" Or Mode = "LC" Or Mode = "TT" Or Mode = "DV" Or Mode = "TW" Then
                            strQuery = "Select  B.SubActID, B.SubActName,  B.Rate, B.MiscRate, B.StatRate, L.MethodName, isnull(T1.Lines,0) AS BillUnits, isnull(T2.Lines,0) AS BillSUnits,  isnull(T1.stdLines,0) AS stdLines, isnull(T2.stdLines,0) AS stdSLines, isnull(T1.Amt,0) AS BillAmt    "
                            strQuery = strQuery & " from AdminSecureweb.dbo.tblAccBillDetails B"
                            strQuery = strQuery & " LEFT OUTER JOIN (Select TrackID, MethodName from AdminETS.dbo.tblLCMethodology ) L"
                            strQuery = strQuery & " on L.TrackID = B.LCMethodID "
                            strQuery = strQuery & " INNER JOIN (Select BillAccID, SubActID, sum(unit) AS Lines, sum(amount) AS Amt, sum(stdLCCount) AS stdLines from tblBillingLines where BillAccID = '" & BillAccID & "' and priority='False' Group by BillAccID, SubActID) T1"
                            strQuery = strQuery & " on T1.BillAccID = B.BillAccID and T1.SubActID = B.SubActID  "
                            strQuery = strQuery & " LEFT OUTER JOIN (Select BillAccID, SubActID, sum(unit) AS Lines, sum(amount) AS Amt, sum(stdLCCount) AS stdLines from tblBillingLines where BillAccID = '" & BillAccID & "'  and priority='True' Group by BillAccID, SubActID) T2"
                            strQuery = strQuery & " on T2.BillAccID = B.BillAccID and T2.SubActID = B.SubActID  where B.BillAccID = '" & BillAccID & "'"


                        Else

                            'strQuery = "Select  B.SubActID, B.SubActName,  B.Rate, B.MiscRate, B.StatRate, L.MethodName, isnull(T1.Lines,0) AS BillUnits, isnull(T2.Lines,0) AS BillSUnits,  isnull(T1.stdLines,0) AS stdLines, isnull(T2.stdLines,0) AS stdSLines, isnull(T1.Amt,0) AS BillAmt    "
                            'strQuery = strQuery & " from AdminSecureweb.dbo.tblAccBillDetails B"
                            'strQuery = strQuery & " LEFT OUTER JOIN (Select TrackID, MethodName from AdminETS.dbo.tblLCMethodology ) L"
                            'strQuery = strQuery & " on L.TrackID = B.LCMethodID "
                            'strQuery = strQuery & " INNER JOIN (Select BillAccID, sum(unit) AS Lines, sum(amount) AS Amt, sum(stdLCCount) AS stdLines from tblBillingLines where BillAccID = '" & BillAccID & "' and priority='False'  Group by BillAccID) T1"
                            'strQuery = strQuery & " on T1.BillAccID = B.BillAccID  "
                            'strQuery = strQuery & " LEFT OUTER JOIN (Select BillAccID, sum(unit) AS Lines, sum(amount) AS Amt, sum(stdLCCount) AS stdLines from tblBillingLines where BillAccID = '" & BillAccID & "'  and priority='True' Group by BillAccID) T2"
                            'strQuery = strQuery & " on T2.BillAccID = B.BillAccID  where B.BillAccID = '" & BillAccID & "'"
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
                        'Response.End()


                        Dim SQLCmd1 As New SqlCommand(strQuery, New SqlConnection(strConn))
                        Try
                            SQLCmd1.Connection.Open()
                            Dim DRRec1 As SqlDataReader = SQLCmd1.ExecuteReader()
                            If DRRec1.HasRows Then
                                While (DRRec1.Read)
                                    Spriority = False
                                    strQuery = "Select NewID() as UID"
                                    Dim SQLCmdUID As New SqlCommand(strQuery, New SqlConnection(strConn))
                                    SQLCmdUID.Connection.Open()
                                    AutoID = SQLCmdUID.ExecuteScalar.ToString
                                    SQLCmdUID.Connection.Close()
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
                                    BillUnits = CInt(FormatNumber(DRRec1("BillUnits"), 0))
                                    BillSUnits = CInt(FormatNumber(DRRec1("BillSUnits"), 0))
                                    'BillAmt += CDbl(FormatNumber(DRRec1("BillAmt"), 2))
                                    'Cell5.Text = CInt(FormatNumber(DRRec1("stdLines"), 0))
                                    'Cell6.Text = CInt(FormatNumber(DRRec1("stdSLines"), 0))
                                    'Cell7.Text = CInt(FormatNumber(DRRec1("BillUnits"), 0))
                                    'Cell8.Text = CInt(FormatNumber(DRRec1("BillSUnits"), 0))
                                    'Cell9.Text = CDbl(FormatNumber(DRRec1("BillAmt"), 2))
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
                                                        ItemDescr = "Previous Period Transcription Activity "
                                                        quantity = NumDict
                                                        amount = FormatNumber(DvRate, 5)
                                                        totamount = FormatNumber(NumDict * DvRate, 2)
                                                        tabletext = tabletext & "<tr><td align=center><font face='Arial' size=2>" & Billdate & "</td><td align=center><font face='Arial' size=2>MT-B</td><td align=center><font face='Arial' size=2>" & ItemDescr & "</td><td align=right><font face='Arial' size=2>" & quantity & "</td><td align=right><font face='Arial' size=2>$ " & FormatNumber(amount, 5) & "</td><td align=right><font face='Arial' size=2>$ " & FormatNumber(totamount, 2) & "</td></tr>"

                                                        InsertRec(AutoID, InvoiceID, AccountID, subActID, Mode, ItemDescr, ItemID, quantity, amount, totamount, Billdate, False, String.Empty)
                                                        UpdateRec(AutoID, BillAccID, subActID, Spriority, Mode)



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
                                                ItemDescr = "Previous Period Transcription Activity  (Worktype: " & WType & ")"
                                            Else
                                                ItemDescr = "Previous Period Transcription Activity "
                                            End If

                                            quantity = BillUnits
                                            amount = FormatNumber(DvRate, 5)
                                            totamount = FormatNumber(BillUnits * DvRate, 2)

                                            tabletext = tabletext & "<tr><td align=center><font face='Arial' size=2>" & Billdate & "</td><td align=center><font face='Arial' size=2>MT-B</td><td align=center><font face='Arial' size=2>" & ItemDescr & "</td><td align=right><font face='Arial' size=2>" & quantity & "</td><td align=right><font face='Arial' size=2>$ " & FormatNumber(amount, 5) & "</td><td align=right><font face='Arial' size=2>$ " & FormatNumber(totamount, 2) & "</td></tr>"
                                            If WTMode = True Then
                                                InsertRec(AutoID, InvoiceID, AccountID, subActID, Mode, ItemDescr, ItemID, quantity, amount, totamount, Billdate, WTMode, Trim(DRRec1("worktype").ToString))
                                            Else
                                                InsertRec(AutoID, InvoiceID, AccountID, subActID, Mode, ItemDescr, ItemID, quantity, amount, totamount, Billdate, WTMode, String.Empty)
                                            End If
                                            UpdateRec(AutoID, BillAccID, subActID, Spriority, Mode)


                                            If BillSUnits > 0 And Recupdate = False Then
                                                Recupdate = True
                                                Spriority = True
                                                ItemID = "da208b70-b5d1-4d4c-aa41-017c2568cd47"
                                                ItemDescr = "Previous Period Transcription Activity - STAT Lines"
                                                BillAmount += FormatNumber(BillSUnits * StatRate, 2)
                                                quantity = BillSUnits
                                                amount = FormatNumber(StatRate, 5)
                                                totamount = FormatNumber(BillSUnits * StatRate, 2)

                                                tabletext = tabletext & "<tr><td align=center><font face='Arial' size=2>" & Billdate & "</td><td align=center><font face='Arial' size=2>MT-S</td><td align=center><font face='Arial' size=2>" & ItemDescr & "</td><td align=right><font face='Arial' size=2>" & quantity & "</td><td align=right><font face='Arial' size=2>$ " & FormatNumber(amount, 5) & "</td><td align=right><font face='Arial' size=2>$ " & FormatNumber(totamount, 2) & "</td></tr>"
                                                InsertRec(AutoID, InvoiceID, AccountID, subActID, Mode, ItemDescr, ItemID, quantity, amount, totamount, Billdate, False, String.Empty)
                                                UpdateRec(AutoID, BillAccID, subActID, Spriority, Mode)
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
                                                    ItemDescr = "Previous Period Transcription Activity (Device: " & SubActName & ")"
                                                    quantity = NumDict
                                                    amount = FormatNumber(DvRate, 5)
                                                    totamount = FormatNumber(NumDict * DvRate, 2)
                                                    tabletext = tabletext & "<tr><td align=center><font face='Arial' size=2>" & Billdate & "</td><td align=center><font face='Arial' size=2>MT-B</td><td align=center><font face='Arial' size=2>" & ItemDescr & "</td><td align=right><font face='Arial' size=2>" & quantity & "</td><td align=right><font face='Arial' size=2>$ " & FormatNumber(amount, 5) & "</td><td align=right><font face='Arial' size=2>$ " & FormatNumber(totamount, 2) & "</td></tr>"
                                                    InsertRec(AutoID, InvoiceID, AccountID, subActID, Mode, ItemDescr, ItemID, quantity, amount, totamount, Billdate, False, String.Empty)
                                                    UpdateRec(AutoID, BillAccID, subActID, Spriority, Mode)


                                                End If

                                            End If
                                        Else

                                            If SubActName = "Telephone" Then
                                                ItemID = "09541ff5-aa07-45e4-8d65-bb9180b60133"
                                                BillAmount = FormatNumber(BillUnits * DvRate, 2)
                                                ItemDescr = "Previous Period Transcription Activity (Device: " & SubActName & ")"
                                                quantity = BillUnits
                                                amount = FormatNumber(DvRate, 5)
                                                totamount = FormatNumber(BillUnits * DvRate, 2)
                                                tabletext = tabletext & "<tr><td align=center><font face='Arial' size=2>" & Billdate & "</td><td align=center><font face='Arial' size=2>MT-B</td><td align=center><font face='Arial' size=2>" & ItemDescr & "</td><td align=right><font face='Arial' size=2>" & quantity & "</td><td align=right><font face='Arial' size=2>$ " & FormatNumber(amount, 5) & "</td><td align=right><font face='Arial' size=2>$ " & FormatNumber(totamount, 2) & "</td></tr>"
                                                InsertRec(AutoID, InvoiceID, AccountID, subActID, Mode, ItemDescr, ItemID, quantity, amount, totamount, Billdate, False, String.Empty)
                                                UpdateRec(AutoID, BillAccID, subActID, Spriority, Mode)
                                            Else
                                                ItemID = "09541ff5-aa07-45e4-8d65-bb9180b60133"
                                                BillAmount = FormatNumber(BillUnits * MiscRate, 2)
                                                ItemDescr = "Previous Period Transcription Activity (Device: " & SubActName & ")"
                                                quantity = BillUnits
                                                amount = FormatNumber(MiscRate, 5)
                                                totamount = FormatNumber(BillUnits * MiscRate, 2)
                                                tabletext = tabletext & "<tr><td align=center><font face='Arial' size=2>" & Billdate & "</td><td align=center><font face='Arial' size=2>MT-B</td><td align=center><font face='Arial' size=2>" & ItemDescr & "</td><td align=right><font face='Arial' size=2>" & quantity & "</td><td align=right><font face='Arial' size=2>$ " & FormatNumber(amount, 5) & "</td><td align=right><font face='Arial' size=2>$ " & FormatNumber(totamount, 2) & "</td></tr>"
                                                InsertRec(AutoID, InvoiceID, AccountID, subActID, Mode, ItemDescr, ItemID, quantity, amount, totamount, Billdate, False, String.Empty)
                                                UpdateRec(AutoID, BillAccID, subActID, Spriority, Mode)
                                            End If



                                            If BillSUnits > 0 Then
                                                Spriority = True
                                                'Response.Write(BillSUnits)
                                                ItemID = "da208b70-b5d1-4d4c-aa41-017c2568cd47"
                                                ItemDescr = "Previous Period Transcription Activity - STAT Lines (Device: " & SubActName & ")"
                                                BillAmount += FormatNumber(BillSUnits * StatRate, 2)
                                                quantity = BillSUnits
                                                amount = FormatNumber(StatRate, 5)
                                                totamount = FormatNumber(BillSUnits * StatRate, 2)
                                                tabletext = tabletext & "<tr><td align=center><font face='Arial' size=2>" & Billdate & "</td><td align=center><font face='Arial' size=2>MT-S</td><td align=center><font face='Arial' size=2>" & ItemDescr & "</td><td align=right><font face='Arial' size=2>" & quantity & "</td><td align=right><font face='Arial' size=2>$ " & FormatNumber(amount, 5) & "</td><td align=right><font face='Arial' size=2>$ " & FormatNumber(totamount, 2) & "</td></tr>"
                                                InsertRec(AutoID, InvoiceID, AccountID, subActID, Mode, ItemDescr, ItemID, quantity, amount, totamount, Billdate, False, String.Empty)
                                                UpdateRec(AutoID, BillAccID, subActID, Spriority, Mode)
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
                                                        ItemDescr = "Previous Period Transcription Activity (Location: " & SubActName & ")"
                                                        quantity = NumDict
                                                        amount = FormatNumber(DvRate, 5)
                                                        totamount = FormatNumber(NumDict * DvRate, 2)
                                                        tabletext = tabletext & "<tr><td align=center><font face='Arial' size=2>" & Billdate & "</td><td align=center><font face='Arial' size=2>MT-B</td><td align=center><font face='Arial' size=2>" & ItemDescr & "</td><td align=right><font face='Arial' size=2>" & quantity & "</td><td align=right><font face='Arial' size=2>$ " & FormatNumber(amount, 5) & "</td><td align=right><font face='Arial' size=2>$ " & FormatNumber(totamount, 2) & "</td></tr>"
                                                        InsertRec(AutoID, InvoiceID, AccountID, subActID, Mode, ItemDescr, ItemID, quantity, amount, totamount, Billdate, False, String.Empty)
                                                        UpdateRec(AutoID, BillAccID, subActID, Spriority, Mode)


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
                                            ItemDescr = "Previous Period Transcription Activity (Location: " & SubActName & ")"
                                            quantity = BillUnits
                                            amount = FormatNumber(DvRate, 5)
                                            totamount = FormatNumber(BillUnits * DvRate, 2)
                                            tabletext = tabletext & "<tr><td align=center><font face='Arial' size=2>" & Billdate & "</td><td align=center><font face='Arial' size=2>MT-B</td><td align=center><font face='Arial' size=2>" & ItemDescr & "</td><td align=right><font face='Arial' size=2>" & quantity & "</td><td align=right><font face='Arial' size=2>$ " & FormatNumber(amount, 5) & "</td><td align=right><font face='Arial' size=2>$ " & FormatNumber(totamount, 2) & "</td></tr>"
                                            InsertRec(AutoID, InvoiceID, AccountID, subActID, Mode, ItemDescr, ItemID, quantity, amount, totamount, Billdate, False, String.Empty)
                                            UpdateRec(AutoID, BillAccID, subActID, Spriority, Mode)


                                            If BillSUnits > 0 Then
                                                Spriority = True
                                                'Response.Write(BillSUnits)
                                                ItemID = "da208b70-b5d1-4d4c-aa41-017c2568cd47"
                                                ItemDescr = "Previous Period Transcription Activity - STAT Lines (Location: " & SubActName & ")"
                                                BillAmount += FormatNumber(BillSUnits * StatRate, 2)
                                                quantity = BillSUnits
                                                amount = FormatNumber(StatRate, 5)
                                                totamount = FormatNumber(BillSUnits * StatRate, 2)
                                                tabletext = tabletext & "<tr><td align=center><font face='Arial' size=2>" & Billdate & "</td><td align=center><font face='Arial' size=2>MT-S</td><td align=center><font face='Arial' size=2>" & ItemDescr & "</td><td align=right><font face='Arial' size=2>" & quantity & "</td><td align=right><font face='Arial' size=2>$ " & FormatNumber(amount, 5) & "</td><td align=right><font face='Arial' size=2>$ " & FormatNumber(totamount, 2) & "</td></tr>"
                                                InsertRec(AutoID, InvoiceID, AccountID, subActID, Mode, ItemDescr, ItemID, quantity, amount, totamount, Billdate, False, String.Empty)
                                                UpdateRec(AutoID, BillAccID, subActID, Spriority, Mode)
                                            End If
                                        End If

                                    ElseIf Mode = "DC" Or Mode = "TW" Then
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
                                                    ItemDescr = "Previous Period Transcription Activity (Group Name: " & SubActName & ")"
                                                    quantity = NumDict
                                                    amount = FormatNumber(DvRate, 5)
                                                    totamount = FormatNumber(NumDict * DvRate, 2)
                                                    tabletext = tabletext & "<tr><td align=center><font face='Arial' size=2>" & Billdate & "</td><td align=center><font face='Arial' size=2>MT-B</td><td align=center><font face='Arial' size=2>" & ItemDescr & "</td><td align=right><font face='Arial' size=2>" & quantity & "</td><td align=right><font face='Arial' size=2>$ " & FormatNumber(amount, 5) & "</td><td align=right><font face='Arial' size=2>$ " & FormatNumber(totamount, 2) & "</td></tr>"
                                                    InsertRec(AutoID, InvoiceID, AccountID, subActID, Mode, ItemDescr, ItemID, quantity, amount, totamount, Billdate, False, String.Empty)
                                                    UpdateRec(AutoID, BillAccID, subActID, Spriority, Mode)


                                                End If

                                            End If
                                        Else
                                            ItemID = "09541ff5-aa07-45e4-8d65-bb9180b60133"
                                            BillAmount = FormatNumber(BillUnits * DvRate, 2)
                                            ItemDescr = "Previous Period Transcription Activity (Group Name: " & SubActName & ")"
                                            quantity = BillUnits
                                            amount = FormatNumber(DvRate, 5)
                                            totamount = FormatNumber(BillUnits * DvRate, 2)
                                            tabletext = tabletext & "<tr><td align=center><font face='Arial' size=2>" & Billdate & "</td><td align=center><font face='Arial' size=2>MT-B</td><td align=center><font face='Arial' size=2>" & ItemDescr & "</td><td align=right><font face='Arial' size=2>" & quantity & "</td><td align=right><font face='Arial' size=2>$ " & FormatNumber(amount, 5) & "</td><td align=right><font face='Arial' size=2>$ " & FormatNumber(totamount, 2) & "</td></tr>"
                                            InsertRec(AutoID, InvoiceID, AccountID, subActID, Mode, ItemDescr, ItemID, quantity, amount, totamount, Billdate, False, String.Empty)
                                            UpdateRec(AutoID, BillAccID, subActID, Spriority, Mode)

                                            If BillSUnits > 0 Then
                                                'Response.Write(BillSUnits)
                                                Spriority = True
                                                ItemID = "da208b70-b5d1-4d4c-aa41-017c2568cd47"
                                                ItemDescr = "Previous Period Transcription Activity - STAT Lines (Group Name: " & SubActName & ")"
                                                BillAmount += FormatNumber(BillSUnits * StatRate, 2)
                                                quantity = BillSUnits
                                                amount = FormatNumber(StatRate, 5)
                                                totamount = FormatNumber(BillSUnits * StatRate, 2)
                                                tabletext = tabletext & "<tr><td align=center><font face='Arial' size=2>" & Billdate & "</td><td align=center><font face='Arial' size=2>MT-S</td><td align=center><font face='Arial' size=2>" & ItemDescr & "</td><td align=right><font face='Arial' size=2>" & quantity & "</td><td align=right><font face='Arial' size=2>$ " & FormatNumber(amount, 5) & "</td><td align=right><font face='Arial' size=2>$ " & FormatNumber(totamount, 2) & "</td></tr>"
                                                InsertRec(AutoID, InvoiceID, AccountID, subActID, Mode, ItemDescr, ItemID, quantity, amount, totamount, Billdate, False, String.Empty)
                                                UpdateRec(AutoID, BillAccID, subActID, Spriority, Mode)
                                            End If
                                        End If

                                    ElseIf Mode = "Dv" Then
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

                        'If BillCycle = "2" Then
                        strQuery = "Update AdminSecureweb.dbo.tblInvItemDet Set InvoiceID = '" & InvoiceID & "'  where Servicedate between  '" & VasStartDate & "' and '" & VasEndDate & "' and AccountID='" & AccountID & "' and Mode='VAS' "
                        Dim SQLCmd3 As New SqlCommand(strQuery, New SqlConnection(strConn))
                        Try
                            SQLCmd3.Connection.Open()
                            SQLCmd3.ExecuteNonQuery()
                        Finally
                            If SQLCmd3.Connection.State = System.Data.ConnectionState.Open Then
                                SQLCmd3.Connection.Close()
                                SQLCmd3 = Nothing
                            End If
                        End Try
                        strQuery = "Select IT.Mode, ID.Item, IT.Descr, IT.Quantity, IT.Amount, IT.Totamount, IT.ServiceDate  from AdminSecureweb.dbo.tblInvItemDet IT Left Outer Join AdminSecureweb.dbo.ItemDetails ID ON IT.ItemID = ID.ItemID where IT.Servicedate between  '" & VasStartDate & "' and '" & VasEndDate & "' and AccountID='" & AccountID & "' and IT.Mode='VAS' "
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

                                    tabletext = tabletext & "<tr><td align=center><font face='Arial' size=2>" & Billdate.ToShortDateString & "</td><td align=center><font face='Arial' size=2>" & DRRec6("Item").ToString & "</td><td align=center><font face='Arial' size=2>" & DRRec6("Descr").ToString & "</td><td align=right><font face='Arial' size=2>" & FormatNumber(DRRec6("Amount").ToString, 5) & "</td><td align=right><font face='Arial' size=2>$ " & FormatNumber(DRRec6("Totamount").ToString, 2) & "</td><td align=right><font face='Arial' size=2>$ " & FormatNumber(DRRec6("Totamount").ToString, 2) & "</td></tr>"
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
                        strQuery = "Update AdminSecureweb.dbo.Invupdata Set status='Posted'  where TrackID = '" & InvoiceID & "' "
                        Dim SQLCmd4 As New SqlCommand(strQuery, New SqlConnection(strConn))
                        Try
                            SQLCmd4.Connection.Open()
                            SQLCmd4.ExecuteNonQuery()
                        Finally
                            If SQLCmd4.Connection.State = System.Data.ConnectionState.Open Then
                                SQLCmd4.Connection.Close()
                                SQLCmd4 = Nothing
                            End If
                        End Try

                        strQuery = "Select * from AdminSecureweb.dbo.Invupdata where TrackID = '" & InvoiceID & "'"
                        Dim IInvCode As String
                        Dim IInvdate As Date
                        Dim IDuedate As Date
                        Dim Itotamount As Double
                        Dim icomments As String
                        Dim Term As Double


                        Dim SQLCmd7 As New SqlCommand(strQuery, New SqlConnection(strConn))
                        Try
                            SQLCmd7.Connection.Open()
                            Dim DRRec7 As SqlDataReader = SQLCmd7.ExecuteReader()
                            If DRRec7.HasRows Then
                                If DRRec7.Read Then
                                    IInvCode = DRRec7("InvCode").ToString
                                    IInvdate = DRRec7("InvDate").ToString
                                    Term = 45
                                    If DRRec7("duedate").ToString <> "" Then
                                        IDuedate = DRRec7("duedate").ToString
                                    Else
                                        IDuedate = DateAdd(DateInterval.Day, Term, IInvdate)
                                    End If
                                    'Response.Write((FormatNumber(DRRec7("amount").ToString, 2, )))
                                    Itotamount = -(FormatNumber(DRRec7("amount").ToString, 2))
                                    icomments = DRRec7("Comments").ToString
                                End If
                            End If
                            DRRec7.Close()
                        Finally
                            If SQLCmd7.Connection.State = System.Data.ConnectionState.Open Then
                                SQLCmd7.Connection.Close()
                                SQLCmd7 = Nothing
                            End If
                        End Try
                        Dim LblAddress As String
                        LblAddress = ""
                        strQuery = "Select *  from AdminETS.dbo.tblAccounts A, AdminSecureweb.dbo.tblBillAccounts B where A.accountid = B.AccountID and  B.BillAccID = '" & BillAccID & "' "
                        Dim SQLCmdAdd As New SqlCommand(strQuery, New SqlConnection(strConn))
                        Try
                            SQLCmdAdd.Connection.Open()
                            Dim DRRecAdd As SqlDataReader = SQLCmdAdd.ExecuteReader()
                            If DRRecAdd.HasRows Then
                                If DRRecAdd.Read Then
                                    LblAddress = DRRecAdd("Description").ToString & "<BR>" & DRRecAdd("BillContName").ToString & "<BR>" & DRRecAdd("BillAddress").ToString.Replace(",", "<BR>")
                                End If
                            End If
                            DRRecAdd.Close()
                        Finally
                            If SQLCmdAdd.Connection.State = System.Data.ConnectionState.Open Then
                                SQLCmdAdd.Connection.Close()
                                SQLCmdAdd = Nothing
                            End If
                        End Try






                        strQuery = "Update AdminSecureweb.dbo.tblBillAccounts Set InvoiceID = '" & InvoiceID & "',   Posted='True' where BillAccID = '" & BillAccID & "' "
                        Dim SQLCmd5 As New SqlCommand(strQuery, New SqlConnection(strConn))
                        Try
                            SQLCmd5.Connection.Open()
                            SQLCmd5.ExecuteNonQuery()
                        Finally
                            If SQLCmd5.Connection.State = System.Data.ConnectionState.Open Then
                                SQLCmd5.Connection.Close()
                                SQLCmd5 = Nothing
                            End If
                        End Try
                        Dim strInvName As String
                        Dim strInvName1 As String
                        strInvName = ProcFolder & "\InvDetails.htm"
                        Dim strFileName As String
                        strFileName = ProcFolder & "\invoices\htm\" & InvoiceID & ".htm"
                        Dim FInfo As New IO.FileInfo(strFileName)
                        If FInfo.Exists Then
                            FInfo.Delete()
                        End If
                        strInvName1 = ProcFolder & "\invoices\pdf\" & InvoiceID & ".pdf"
                        Dim BillInvFile As New FileInfo(strInvName)
                        BillInvFile.CopyTo(strFileName)

                        Dim objStreamReader As StreamReader
                        objStreamReader = File.OpenText(strFileName)
                        Dim contents As String = objStreamReader.ReadToEnd()
                        contents = contents.Replace("TableText", tabletext)
                        contents = contents.Replace("IInvCode", IInvCode)
                        contents = contents.Replace("IInvDate", IInvdate.ToShortDateString)
                        contents = contents.Replace("IdueDate", IDuedate)
                        contents = contents.Replace("itotAmount", FormatNumber(Itotamount, 2))
                        contents = contents.Replace("icomments", icomments)
                        contents = contents.Replace("iaddress", LblAddress)

                        'Response.Write(contents)
                        'Response.End()
                        objStreamReader.Close()
                        Dim objWriter As StreamWriter
                        Try
                            objWriter = New StreamWriter(strFileName)
                            objWriter.Write(contents)
                            objWriter.Close()
                        Catch Ex As Exception
                            Response.Write(Ex.Message)
                        End Try


                        'Dim process1 As New System.Diagnostics.Process()
                        'process1.StartInfo.FileName = "c:\Program Files\TotalHTMLConverterX\HTMLConverterX.exe"
                        'process1.StartInfo.Arguments = """" & strFileName & """" & " " & """" & strInvName1 & """" & " -c PDF"
                        'process1.Start()
                        'process1.WaitForExit()
                        'process1.Close()
                        'process1.Dispose()
                        ''Response.Write(contents)
                        HCell2.Font.Bold = True
                        HCell2.Text = "Posted"
                    End If
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
        Catch ex As Exception
            Response.Write(ex.Message)
            HCell2.Font.Bold = True
            HCell2.ForeColor = Drawing.Color.Firebrick
            HCell2.Text = "Not Posted"
        End Try
        HRow1.Cells.Add(HCell1)
        HRow1.Cells.Add(HCell2)
        tblInvoice.Rows.Add(HRow1)
    End Sub


    Protected Sub InsertRec(ByVal AutoID As String, ByVal InvoiceID As String, ByVal AccountID As String, ByVal SubActID As String, ByVal Mode As String, ByVal Descr As String, ByVal itemid As String, ByVal quantity As Long, ByVal amount As Double, ByVal totamount As String, ByVal ServiceDate As Date, ByVal WTMode As String, ByVal WType As String)
        Dim strQuery As String
        Dim strConn As String
        strConn = System.Configuration.ConfigurationManager.AppSettings("ETSCon")
        If Mode = "LC" Or Mode = "DC" Or Mode = "DV" Then
            strQuery = "Insert Into AdminSecureweb.dbo.tblinvItemDet (AutoID, InvoiceID, AccountID, SubActID, Mode, Descr, itemid, quantity, amount, totamount, ServiceDate, Dateupdate) Values ('" & AutoID & "' , '" & InvoiceID & "' , '" & AccountID & "' , '" & SubActID & "' , 'Trans' , '" & Descr & "' , '" & itemid & "' , '" & quantity & "' , '" & amount & "' , '" & totamount & "' , '" & ServiceDate & "', '" & Now & "') "
        Else
            strQuery = "Insert Into AdminSecureweb.dbo.tblinvItemDet (AutoID, InvoiceID, AccountID,  Mode, Descr, itemid, quantity, amount, totamount, ServiceDate, Dateupdate, WTMode,WorkType) Values ('" & AutoID & "' , '" & InvoiceID & "' , '" & AccountID & "' , 'Trans' , '" & Descr & "' , '" & itemid & "' , '" & quantity & "' , '" & amount & "' , '" & totamount & "' , '" & ServiceDate & "', '" & Now & "','" & WTMode & "','" & WType & "') "
        End If
        '    Response.Write(strQuery)
        Dim SQLCmd5 As New SqlCommand(strQuery, New SqlConnection(strConn))
        SQLCmd5.Connection.Open()
        SQLCmd5.ExecuteNonQuery()
        SQLCmd5.Connection.Close()

    End Sub

    Protected Sub UpdateRec(ByVal InvItemID As String, ByVal BillAccID As String, ByVal SubActID As String, ByVal Priority As Boolean, ByVal Mode As String)
        Dim strConn As String
        strConn = System.Configuration.ConfigurationManager.AppSettings("ETSCon")
        Dim strQuery As String
        strQuery = "update AdminETS.dbo.tblBillingLines set InvItemID ='" & InvItemID & "' where BillAccID='" & BillAccID & "'  "
        If Mode = "LC" Or Mode = "DC" Or Mode = "DV" Or Mode = "TW" Then
            strQuery = strQuery & " and SubActID ='" & SubActID & "' "
        End If
        If Priority = True Then
            strQuery = strQuery & " and Priority ='" & Priority & "' "
        End If
        Dim SQLCmd5 As New SqlCommand(strQuery, New SqlConnection(strConn))
        SQLCmd5.Connection.Open()
        SQLCmd5.ExecuteNonQuery()
        SQLCmd5.Connection.Close()

    End Sub



End Class
