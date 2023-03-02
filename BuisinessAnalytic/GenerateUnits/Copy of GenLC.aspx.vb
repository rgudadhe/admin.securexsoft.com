Imports System.Data.SqlClient
Partial Class GenLC
    Inherits BasePage

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        
        If Not IsPostBack Then
            Dim strConn As String
            strConn = System.Configuration.ConfigurationManager.AppSettings("ETSCon")
            Dim SQLCmd1 As New SqlCommand("Select * from ETS.dbo.tblaccounts where contractorid ='" & Session("contractorid").ToString & "' order by Accountname", New SqlConnection(strConn))
            Try
                SQLCmd1.Connection.Open()
                Dim DRRec1 As SqlDataReader = SQLCmd1.ExecuteReader()
                If DRRec1.HasRows = True Then
                    While DRRec1.Read
                        Dim LI As New ListItem
                        LI.Text = DRRec1("Accountname")
                        LI.Value = DRRec1("AccountID").ToString
                        DLAct.Items.Add(LI)
                    End While
                End If
                DRRec1.Close()
            Finally
                If SQLCmd1.Connection.State = System.Data.ConnectionState.Open Then
                    SQLCmd1.Connection.Close()
                    SQLCmd1 = Nothing
                End If
            End Try
            GetMyMonthList(DLMonth, True)
            GetMyYearList(DLYear, True)
            Table1.Visible = False
            Table2.Visible = False
        End If


    End Sub

    Protected Sub BtnSubmit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtnSubmit.Click
        Dim strConn As String
        strConn = System.Configuration.ConfigurationManager.AppSettings("ETSCon")

        Dim C1CurrSDate As Date
        Dim C1CurrEDate As Date
        Dim C2CurrSDate As Date
        Dim C2CurrEDate As Date
        Dim DefaultLC As Boolean = False
        C1CurrSDate = DLMonth.SelectedValue & "/1/" & DLYear.SelectedValue
        C1CurrEDate = DLMonth.SelectedValue & "/16/" & DLYear.SelectedValue
        C2CurrSDate = DLMonth.SelectedValue & "/16/" & DLYear.SelectedValue
        C2CurrEDate = DateAdd(DateInterval.Month, 1, C1CurrSDate)



        Dim strCycle As Boolean
        Dim strQuery As String
        Dim ReportLC As Integer
        Dim StdBillingLC As Integer
        Dim Rptheader As Boolean
        Dim RptFooter As Boolean
        Dim RptBody As Boolean
        Dim RptBIU As Boolean
        Dim RptShifted As Boolean
        Dim RptSpaces As Boolean
        Dim RptSCT As Boolean
        Dim CharsPerLines As Integer
        Dim CountMethod As String
        Dim BillAccID As String
        Dim AccountID As String
        Dim Cycle As String
        Dim MinBilling As String
        Dim BillMonth As String
        Dim BillYear As String
        Dim Mode As String
        Dim RepRate As String
        Dim RepMiscRate As String
        Dim RepStatRate As String
        Dim RepLCMethodID As String
        Dim strWorkType As String
        Dim WTMode As Boolean = False
        Dim Regenerate As Boolean
        Dim GenLC As Boolean
        Dim RecFound As Boolean
        Dim TranscriptionID As String
        Dim stdLCCount As Integer
        Dim Rate As Double
        Dim Amount As Double
        Dim Type As String
        Dim SubActName As String
        Dim SubActID As String
        Dim sdate As Date
        Dim edate As Date
        Dim j As Integer
        Dim UDFound As Boolean
        Dim LCMthFound As Boolean
        j = 0

        CountMethod = "CharsPerLine"
        Rptheader = False
        RptFooter = False
        RptBody = False
        RptBIU = False
        RptShifted = False
        RptSpaces = False
        RptSCT = False
        CharsPerLines = 0
        ReportLC = 0
        StdBillingLC = 0
        If DLCycle.SelectedValue = "1" Then
            strCycle = True
            If DLAct.SelectedValue = "All" Then
                strQuery = "Select * from ETS.dbo.tblaccounts where  contractorid ='" & Session("contractorid").ToString & "'  and Cycle='" & strCycle & "' order by Accountname"
            Else
                strQuery = "Select * from ETS.dbo.tblaccounts where contractorid ='" & Session("contractorid").ToString & "' and  Cycle='" & strCycle & "' and accountid='" & DLAct.SelectedValue & "'  order by Accountname"
            End If

        Else
            strCycle = False
            If DLAct.SelectedValue = "All" Then
                strQuery = "Select * from ETS.dbo.tblaccounts  where contractorid ='" & Session("contractorid").ToString & "'   order by Accountname"
            Else
                strQuery = "Select * from ETS.dbo.tblaccounts where  contractorid ='" & Session("contractorid").ToString & "' and accountid='" & DLAct.SelectedValue & "' "
            End If

        End If
        Dim SQLCmd1 As New SqlCommand(strQuery, New SqlConnection(strConn))
        Try
            SQLCmd1.Connection.Open()
            Dim DRRec1 As SqlDataReader = SQLCmd1.ExecuteReader()
            If DRRec1.HasRows = True Then
                Table1.Visible = True
                Table2.Visible = True
                While DRRec1.Read
                    BillMonth = DLMonth.SelectedValue
                    BillYear = DLYear.SelectedValue
                    Regenerate = False
                    RecFound = False
                    GenLC = False
                    UDFound = False
                    LCMthFound = False
                    DefaultLC = False
                    strQuery = "Select * from Secureweb.dbo.tblbillaccounts where AccountID = '" & DRRec1("AccountID").ToString & "' and Billmonth='" & DLMonth.SelectedValue & "' and BillYear = '" & DLYear.SelectedValue & "' and BillCycle='" & DLCycle.SelectedValue & "' "

                    'Response.End()

                    Dim SQLCmd2 As New SqlCommand(strQuery, New SqlConnection(strConn))
                    Try
                        SQLCmd2.Connection.Open()
                        Dim DRRec2 As SqlDataReader = SQLCmd2.ExecuteReader()
                        If DRRec2.HasRows = True Then
                            If DRRec2.Read Then
                                BillAccID = DRRec2("BillAccID").ToString
                                GenLC = False
                                RecFound = True
                                Regenerate = True
                                UDFound = True
                                LCMthFound = True
                            End If
                        Else
                            If DRRec1("Cycle").ToString = "True" Then
                                If DLCycle.SelectedValue = "1" Then
                                    sdate = C1CurrSDate
                                    edate = C1CurrEDate
                                ElseIf DLCycle.SelectedValue = "2" Then
                                    sdate = C2CurrSDate
                                    edate = C2CurrEDate
                                End If
                            ElseIf DRRec1("Cycle").ToString = "False" Then
                                If DLCycle.SelectedValue = "1" Then
                                    sdate = C1CurrSDate
                                    edate = C1CurrEDate
                                ElseIf DLCycle.SelectedValue = "2" Then
                                    sdate = C1CurrSDate
                                    edate = C2CurrEDate
                                End If
                            End If
                            If Trim(DRRec1("Mode").ToString) = "S" Or Trim(DRRec1("Mode").ToString) = "" Then
                                strQuery = "Select * from Secureweb.dbo.BillDetails where AccountID = '" & DRRec1("AccountID").ToString & "'  "
                                '    'Response.Write(strQuery)
                                Dim SQLCmd3 As New SqlCommand(strQuery, New SqlConnection(strConn))
                                Try
                                    SQLCmd3.Connection.Open()
                                    Dim DRRec3 As SqlDataReader = SQLCmd3.ExecuteReader()
                                    If DRRec3.HasRows = True Then
                                        UDFound = True
                                        If DRRec3.Read Then
                                            strQuery = "DELETE FROM ETS.dbo.tblbillinglines WHERE BillAccID ='11111111-1111-1111-1111-111111111111' AND Transcriptionid in (Select TranscriptionID FROM SecureWeb.dbo.tblTranscriptionClientMain where AccountID = '" & DRRec1("AccountID").ToString & "'   and datemodified >= '" & sdate & "' and datemodified < '" & edate & "') "
                                            Dim SQLCmdDel As New SqlCommand(strQuery, New SqlConnection(strConn))
                                            Try
                                                SQLCmdDel.Connection.Open()
                                                SQLCmdDel.ExecuteNonQuery()
                                            Finally
                                                If SQLCmdDel.Connection.State = System.Data.ConnectionState.Open Then
                                                    SQLCmdDel.Connection.Close()
                                                    SQLCmdDel = Nothing
                                                End If
                                            End Try
                                            strQuery = "Select NewID() as UID"
                                            Dim SQLCmd6 As New SqlCommand(strQuery, New SqlConnection(strConn))
                                            SQLCmd6.Connection.Open()
                                            BillAccID = SQLCmd6.ExecuteScalar.ToString
                                            AccountID = DRRec1("AccountID").ToString
                                            Cycle = DRRec1("Cycle").ToString
                                            MinBilling = DRRec1("MinBilling").ToString
                                            BillMonth = DLMonth.SelectedValue
                                            BillYear = DLYear.SelectedValue
                                            Mode = Trim(DRRec1("Mode").ToString)
                                            If IsDBNull(DRRec3("WTMode")) Or DRRec3("WTMode").ToString.ToLower = "false" Then
                                                WTMode = False
                                            Else
                                                WTMode = True
                                            End If
                                            strQuery = "Insert Into SEcureWeb.dbo.tblBillAccounts (BillAccID, AccountID, Cycle, MinBilling, BillMonth, BillYear, BillCycle, Mode, ModDate,WTMode) Values ('" & BillAccID & "', '" & AccountID & "','" & Cycle & "', '" & MinBilling & "', '" & BillMonth & "', '" & BillYear & "', '" & DLCycle.SelectedValue & "', '" & Mode & "', '" & Now & "','" & WTMode & "') "
                                            Dim SQLCmdUp1 As New SqlCommand(strQuery, New SqlConnection(strConn))
                                            Try
                                                SQLCmdUp1.Connection.Open()
                                                SQLCmdUp1.ExecuteNonQuery()
                                            Finally
                                                If SQLCmdUp1.Connection.State = System.Data.ConnectionState.Open Then
                                                    SQLCmdUp1.Connection.Close()
                                                    SQLCmdUp1 = Nothing
                                                End If
                                            End Try


                                            ''Response.Write(strQuery)
                                            strQuery = "Insert Into SEcureWeb.dbo.tblAccBillDetails (BillAccID, AccountID, Rate, MiscRate, StatRate, LCMethodID,ModDate) Values ('" & BillAccID & "', '" & AccountID & "','" & DRRec3("Rate") & "', '" & DRRec3("MiscRate") & "', '" & DRRec3("StatRate") & "'"
                                            'Response.Write(strQuery)
                                            'Response.End()

                                            If DRRec3("LCMethodID").ToString = "" Then
                                                strQuery = strQuery & ", NULL, '" & Now & "') "
                                            Else
                                                strQuery = strQuery & ", '" & DRRec3("LCMethodID").ToString & "', '" & Now & "') "
                                            End If


                                            Dim SQLCmdUp2 As New SqlCommand(strQuery, New SqlConnection(strConn))
                                            Try
                                                SQLCmdUp2.Connection.Open()
                                                SQLCmdUp2.ExecuteNonQuery()
                                            Finally
                                                If SQLCmdUp2.Connection.State = System.Data.ConnectionState.Open Then
                                                    SQLCmdUp2.Connection.Close()
                                                    SQLCmdUp2 = Nothing
                                                End If
                                            End Try


                                            If DRRec3("LCMethodID").ToString <> "" Then
                                                strQuery = "Select  * from ETS.dbo.tblLCMethodology where TrackID = '" & DRRec3("LCMethodID").ToString & "' "
                                                Dim SQLCmd4 As New SqlCommand(strQuery, New SqlConnection(strConn))
                                                Try
                                                    SQLCmd4.Connection.Open()
                                                    Dim DRRec4 As SqlDataReader = SQLCmd4.ExecuteReader()
                                                    If DRRec4.HasRows = True Then
                                                        LCMthFound = True
                                                        If DRRec4.Read Then
                                                            If DRRec4("isdefault").ToString.ToUpper = "TRUE" Then
                                                                DefaultLC = True
                                                            Else
                                                                DefaultLC = False
                                                            End If
                                                            Rptheader = DRRec4("Rptheader").ToString
                                                            RptFooter = DRRec4("RptFooter").ToString
                                                            RptBody = DRRec4("RptBody").ToString
                                                            RptBIU = DRRec4("RptBIU").ToString
                                                            RptShifted = DRRec4("RptShifted").ToString
                                                            RptSpaces = DRRec4("RptSpaces").ToString
                                                            RptSCT = DRRec4("RptSCT").ToString
                                                            CharsPerLines = DRRec4("CharsPerLines")
                                                            CountMethod = DRRec4("CountMethod").ToString
                                                            Dim conn2 As New SqlConnection(strConn)
                                                            conn2.Open()
                                                            strQuery = "Select distinct T.*, TL.*,M.Type,M.Location,M.Priority,M.DictatorID,M.Jobnumber from SecureWeb.dbo.tblTranscriptionClientMain M Left Outer Join tblTransLC T ON T.TranscriptionID = M.TranscriptionID Left Outer Join tblTemplates TL ON TL.TemplateID = M.TemplateID where T.TranscriptionID is not NULL and M.AccountID = '" & DRRec1("AccountID").ToString & "'   and M.datemodified >= '" & sdate & "' and M.datemodified < '" & edate & "'  order by JobNumber"
                                                            'strQuery = "Select distinct * from SecureWeb.dbo.tblTranscriptionClientMain M Left Outer Join tblTransLC T ON T.TranscriptionID = M.TranscriptionID Left Outer Join tblTemplates TL ON TL.TemplateID = M.TemplateID where T.TranscriptionID is not NULL and M.AccountID = '" & DRRec1("AccountID").ToString & "'   and datediff(day,M.datemodified, '7/31/2010') between 0 and  30  order by JobNumber"
                                                            'Response.Write(strQuery)
                                                            'Response.End()
                                                            Dim i As Integer
                                                            i = 0
                                                            Dim SQLCmd5 As New SqlCommand(strQuery, New SqlConnection(strConn))
                                                            Try
                                                                SQLCmd5.CommandTimeout = 1200
                                                                SQLCmd5.Connection.Open()
                                                                Dim DRRec5 As SqlDataReader = SQLCmd5.ExecuteReader()
                                                                If DRRec5.HasRows = True Then
                                                                    RecFound = True
                                                                    While DRRec5.Read
                                                                        i = i + 1
                                                                        ReportLC = 0
                                                                        strWorkType = String.Empty
                                                                        TranscriptionID = DRRec5("TranscriptionId").ToString
                                                                        stdLCCount = DRRec5("stdLC")
                                                                        ''Response.Write(DRRec5("transcriptionid").ToString & "#")
                                                                        If CountMethod = "PerDictator" Then
                                                                            ReportLC = DRRec5("stdLC")
                                                                        End If
                                                                        If CountMethod = "PerReport" Then
                                                                            ReportLC = 1
                                                                        ElseIf DefaultLC = True Then
                                                                            ReportLC = DRRec5("billingLC")
                                                                        Else

                                                                            If CountMethod = "CharsPerLine" Then
                                                                                If RptBody = True Then
                                                                                    '        'Response.Write(ReportLC & "#")
                                                                                    If RptSpaces = True Then
                                                                                        ReportLC += DRRec5("NumBodyCharsWSpaces")
                                                                                    Else
                                                                                        ReportLC += DRRec5("NumBodyCharsWOSpaces")
                                                                                    End If
                                                                                    '       'Response.Write(ReportLC & "#")
                                                                                    If RptSCT = True Then
                                                                                        ReportLC += DRRec5("NumBodyBTRChars")
                                                                                    End If
                                                                                    '      'Response.Write(ReportLC & "#")
                                                                                    If RptShifted = True Then
                                                                                        ReportLC += DRRec5("NumBodyCharsWShift")
                                                                                    End If
                                                                                    '     'Response.Write(ReportLC & "#")
                                                                                    If RptBIU = True Then
                                                                                        ReportLC += DRRec5("NumBodyBIU")
                                                                                    End If
                                                                                End If

                                                                                If Rptheader = True Then
                                                                                    ReportLC += DRRec5("HeaderCount")
                                                                                    '    'Response.Write(ReportLC & "#")
                                                                                    If RptSpaces = True Then
                                                                                        ReportLC += DRRec5("HeaderCountWSpaces")
                                                                                    End If
                                                                                    '   'Response.Write(ReportLC & "#")
                                                                                    If RptSCT = True Then
                                                                                        ReportLC += (DRRec5("HeaderBTRCount") - DRRec5("HeaderCount"))
                                                                                    End If
                                                                                    If RptShifted = True Then
                                                                                        ReportLC += DRRec5("HeaderWShift")
                                                                                    End If
                                                                                    If RptBIU = True Then
                                                                                        ReportLC += DRRec5("HeaderBIUCount")
                                                                                    End If
                                                                                    '  'Response.Write(ReportLC & "#")
                                                                                End If

                                                                                If RptFooter = True Then
                                                                                    ' 'Response.Write(ReportLC & "#")
                                                                                    ReportLC += DRRec5("FooterCount")
                                                                                    If RptSpaces = True Then
                                                                                        ReportLC += DRRec5("FooterCountWSpaces")
                                                                                    End If
                                                                                    If RptSCT = True Then
                                                                                        ReportLC += (DRRec5("footerBTRCount") - DRRec5("FooterCount"))
                                                                                    End If
                                                                                    If RptShifted = True Then
                                                                                        ReportLC += DRRec5("FooterWShift")
                                                                                    End If
                                                                                    If RptBIU = True Then
                                                                                        ReportLC += DRRec5("FooterBIUCount")
                                                                                    End If
                                                                                    ''Response.Write(ReportLC & "#")

                                                                                End If
                                                                                If CharsPerLines > 0 Then
                                                                                    ReportLC = ReportLC / CharsPerLines
                                                                                End If
                                                                            End If


                                                                            If CountMethod = "Words" Then
                                                                                If RptBody = True Then
                                                                                    ReportLC += DRRec5("NumBodywords")

                                                                                End If

                                                                                If Rptheader = True Then
                                                                                    ReportLC += DRRec5("Numheaderwords")
                                                                                End If

                                                                                If RptFooter = True Then
                                                                                    ReportLC += DRRec5("Numfooterwords")
                                                                                End If
                                                                            End If

                                                                            If CountMethod = "Pages" Then
                                                                                ReportLC += DRRec5("NumPages")

                                                                            End If
                                                                            If CountMethod = "GrossLines" Then
                                                                                If RptBody = True Then
                                                                                    ReportLC += DRRec5("bodyLines")
                                                                                End If

                                                                                If Rptheader = True Then
                                                                                    ReportLC += DRRec5("GrossHeaderCount")
                                                                                End If

                                                                                If RptFooter = True Then
                                                                                    ReportLC += DRRec5("GrossFooterCount")
                                                                                End If

                                                                            End If

                                                                            If CountMethod = "AllLines" Then
                                                                                If RptBody = True Then
                                                                                    ReportLC += DRRec5("bodyLinesWBlanks")

                                                                                End If

                                                                                If Rptheader = True Then
                                                                                    ReportLC += DRRec5("GrossHeaderCountWBlanks")
                                                                                End If

                                                                                If RptFooter = True Then
                                                                                    ReportLC += DRRec5("GrossFooterCountWBlanks")
                                                                                End If
                                                                            End If
                                                                        End If

                                                                        Dim Row1 As New TableRow
                                                                        Dim Cell1 As New TableCell
                                                                        Dim Cell2 As New TableCell
                                                                        Dim Cell3 As New TableCell
                                                                        Dim Cell4 As New TableCell
                                                                        Dim Cell5 As New TableCell
                                                                        Dim Cell6 As New TableCell
                                                                        Dim Cell As New TableCell
                                                                        Cell.Text = i
                                                                        Cell1.Text = DRRec1("AccountName").ToString
                                                                        Cell2.Text = Trim(DRRec1("Mode").ToString)
                                                                        Cell3.Text = DRRec5("Jobnumber").ToString
                                                                        Cell4.Text = ReportLC
                                                                        Cell4.Text = ReportLC
                                                                        Cell5.Text = CountMethod
                                                                        Row1.Cells.Add(Cell)
                                                                        Row1.Cells.Add(Cell1)
                                                                        Row1.Cells.Add(Cell2)
                                                                        Row1.Cells.Add(Cell3)
                                                                        Row1.Cells.Add(Cell4)
                                                                        Row1.Cells.Add(Cell5)
                                                                        Table1.Rows.Add(Row1)
                                                                        Type = DRRec5("Type").ToString
                                                                        If IsDBNull(DRRec3("WTMode")) Or DRRec3("WTMode").ToString.ToLower = "false" Then
                                                                            Rate = DRRec3("Rate")
                                                                        Else
                                                                            If DRRec5("TemplateType").ToString.ToUpper = "C" Then
                                                                                Rate = DRRec3("Consult")
                                                                                strWorkType = "C"
                                                                            ElseIf DRRec5("TemplateType").ToString.ToUpper = "H" Then
                                                                                Rate = DRRec3("HP")
                                                                                strWorkType = "H"
                                                                            ElseIf DRRec5("TemplateType").ToString.ToUpper = "D" Then
                                                                                Rate = DRRec3("Discharge")
                                                                                strWorkType = "D"
                                                                            ElseIf DRRec5("TemplateType").ToString.ToUpper = "I" Then
                                                                                Rate = DRRec3("IME")
                                                                                strWorkType = "I"
                                                                            ElseIf DRRec5("TemplateType").ToString.ToUpper = "L" Then
                                                                                Rate = DRRec3("Letter")
                                                                                strWorkType = "L"
                                                                            ElseIf DRRec5("TemplateType").ToString.ToUpper = "N" Then
                                                                                Rate = DRRec3("PrNote")
                                                                                strWorkType = "N"
                                                                            ElseIf DRRec5("TemplateType").ToString.ToUpper = "O" Then
                                                                                Rate = DRRec3("OpNote")
                                                                                strWorkType = "O"
                                                                            ElseIf DRRec5("TemplateType").ToString.ToUpper = "P" Then
                                                                                Rate = DRRec3("PsychEval")
                                                                                strWorkType = "P"
                                                                            Else
                                                                                Rate = DRRec3("Rate")
                                                                                strWorkType = String.Empty
                                                                            End If
                                                                        End If
                                                                        Amount = ReportLC * Rate

                                                                        strQuery = "Insert Into ETS.dbo.tblBillingLines (BillAccID, TranscriptionID, stdLCCount, unit, Rate, Amount, Type, Priority, updateDate,WorkType) Values ('" & BillAccID & "', '" & TranscriptionID & "','" & stdLCCount & "', '" & ReportLC & "', '" & Rate & "', '" & Amount & "', '" & Type & "','False', '" & Now & "', '" & strWorkType & "') "
                                                                        ''Response.Write(strQuery & "<BR>")


                                                                        Dim SQLCmdUp3 As New SqlCommand(strQuery, conn2)
                                                                        'Try
                                                                        '    SQLCmdUp3.Connection.Open()
                                                                        SQLCmdUp3.ExecuteNonQuery()



                                                                        If DRRec5("Priority").ToString = "True" And Not CountMethod = "PerReport" Then
                                                                            Rate = DRRec3("StatRate")
                                                                            Amount = ReportLC * Rate
                                                                            strQuery = "Insert Into ETS.dbo.tblBillingLines (BillAccID, TranscriptionID, stdLCCount, unit, Rate, Amount, Type, Priority, updateDate,WorkType) Values ('" & BillAccID & "', '" & TranscriptionID & "','" & stdLCCount & "', '" & ReportLC & "', '" & Rate & "', '" & Amount & "', '" & Type & "', 'True',  '" & Now & "', '" & strWorkType & "') "
                                                                            ''Response.Write(strQuery)

                                                                            Dim SQLCmdUpST As New SqlCommand(strQuery, New SqlConnection(strConn))
                                                                            Try
                                                                                SQLCmdUpST.Connection.Open()
                                                                                SQLCmdUpST.ExecuteNonQuery()
                                                                            Finally
                                                                                If SQLCmdUpST.Connection.State = System.Data.ConnectionState.Open Then
                                                                                    SQLCmdUpST.Connection.Close()
                                                                                    SQLCmdUpST = Nothing
                                                                                End If
                                                                            End Try
                                                                        End If

                                                                    End While
                                                                End If
                                                                If conn2.State = System.Data.ConnectionState.Open Then
                                                                    conn2.Close()
                                                                    conn2 = Nothing
                                                                End If
                                                            Finally
                                                                If SQLCmd5.Connection.State = System.Data.ConnectionState.Open Then
                                                                    SQLCmd5.Connection.Close()
                                                                    SQLCmd5 = Nothing
                                                                End If
                                                            End Try

                                                        End If
                                                    End If
                                                Finally
                                                    If SQLCmd4.Connection.State = System.Data.ConnectionState.Open Then
                                                        SQLCmd4.Connection.Close()
                                                        SQLCmd4 = Nothing
                                                    End If
                                                End Try

                                            End If

                                        End If
                                        GenLC = True
                                    End If
                                Finally
                                    If SQLCmd3.Connection.State = System.Data.ConnectionState.Open Then
                                        SQLCmd3.Connection.Close()
                                        SQLCmd3 = Nothing
                                    End If
                                End Try


                            ElseIf Trim(DRRec1("Mode").ToString) = "DV" Then

                                strQuery = "Select * from Secureweb.dbo.BillDetails where AccountID = '" & DRRec1("AccountID").ToString & "'  "
                                Dim SQLCmd3 As New SqlCommand(strQuery, New SqlConnection(strConn))
                                Try
                                    SQLCmd3.Connection.Open()
                                    Dim DRRec3 As SqlDataReader = SQLCmd3.ExecuteReader()
                                    If DRRec3.HasRows = True Then
                                        UDFound = True
                                        If DRRec3.Read Then
                                            strQuery = "Select NewID() as UID"
                                            Dim SQLCmd6 As New SqlCommand(strQuery, New SqlConnection(strConn))
                                            SQLCmd6.Connection.Open()
                                            BillAccID = SQLCmd6.ExecuteScalar.ToString
                                            AccountID = DRRec1("AccountID").ToString
                                            Cycle = DRRec1("Cycle").ToString
                                            MinBilling = DRRec1("MinBilling").ToString
                                            BillMonth = DLMonth.SelectedValue
                                            BillYear = DLYear.SelectedValue
                                            Mode = Trim(DRRec1("Mode").ToString)

                                            ''Response.Write(BillAccID)
                                            strQuery = "Insert Into SEcureWeb.dbo.tblBillAccounts (BillAccID, AccountID, Cycle, MinBilling, BillMonth, BillYear, BillCycle, Mode, ModDate) Values ('" & BillAccID & "', '" & AccountID & "','" & Cycle & "', '" & MinBilling & "', '" & BillMonth & "', '" & BillYear & "', '" & DLCycle.SelectedValue & "', '" & Mode & "', '" & Now & "') "
                                            Dim SQLCmdUp1 As New SqlCommand(strQuery, New SqlConnection(strConn))
                                            Try
                                                SQLCmdUp1.Connection.Open()
                                                SQLCmdUp1.ExecuteNonQuery()
                                            Finally
                                                If SQLCmdUp1.Connection.State = System.Data.ConnectionState.Open Then
                                                    SQLCmdUp1.Connection.Close()
                                                    SQLCmdUp1 = Nothing
                                                End If
                                            End Try

                                            strQuery = "DELETE FROM ETS.dbo.tblbillinglines WHERE BillAccID ='11111111-1111-1111-1111-111111111111' AND Transcriptionid in (Select TranscriptionID FROM SecureWeb.dbo.tblTranscriptionClientMain where AccountID = '" & DRRec1("AccountID").ToString & "'   and datemodified >= '" & sdate & "' and datemodified < '" & edate & "') "
                                            Dim SQLCmdDel As New SqlCommand(strQuery, New SqlConnection(strConn))
                                            Try
                                                SQLCmdDel.Connection.Open()
                                                SQLCmdDel.ExecuteNonQuery()
                                            Finally
                                                If SQLCmdDel.Connection.State = System.Data.ConnectionState.Open Then
                                                    SQLCmdDel.Connection.Close()
                                                    SQLCmdDel = Nothing
                                                End If
                                            End Try
                                            ''Response.Write(strQuery)
                                            Dim Dcount As Integer
                                            Dim DevActID As String
                                            Dim DevActName As String

                                            For Dcount = 1 To 2
                                                strQuery = "Select NewID() as UID"
                                                Dim SQLCmdDev As New SqlCommand(strQuery, New SqlConnection(strConn))
                                                SQLCmdDev.Connection.Open()
                                                DevActID = SQLCmdDev.ExecuteScalar.ToString
                                                SQLCmdDev.Connection.Close()
                                                If Dcount = 1 Then
                                                    DevActName = "Telephone"
                                                ElseIf Dcount = 2 Then
                                                    DevActName = "DVR"
                                                End If


                                                strQuery = "Insert Into SecureWeb.dbo.tblAccBillDetails (BillAccID, AccountID, SubActID, SubActName,  Rate, MiscRate, StatRate, LCMethodID,ModDate) Values ('" & BillAccID & "', '" & AccountID & "', '" & DevActID & "', '" & DevActName & "','" & DRRec3("Rate") & "', '" & DRRec3("MiscRate") & "', '" & DRRec3("StatRate") & "', '" & DRRec3("LCMethodID").ToString & "', '" & Now & "') "
                                                Dim SQLCmdUp2 As New SqlCommand(strQuery, New SqlConnection(strConn))
                                                Try
                                                    SQLCmdUp2.Connection.Open()
                                                    SQLCmdUp2.ExecuteNonQuery()
                                                Finally
                                                    If SQLCmdUp2.Connection.State = System.Data.ConnectionState.Open Then
                                                        SQLCmdUp2.Connection.Close()
                                                        SQLCmdUp2 = Nothing
                                                    End If
                                                End Try



                                                strQuery = "Select  * from ETS.dbo.tblLCMethodology where TrackID = '" & DRRec3("LCMethodID").ToString & "' "
                                                Dim SQLCmd4 As New SqlCommand(strQuery, New SqlConnection(strConn))
                                                Try
                                                    SQLCmd4.Connection.Open()
                                                    Dim DRRec4 As SqlDataReader = SQLCmd4.ExecuteReader()
                                                    If DRRec4.HasRows = True Then
                                                        LCMthFound = True
                                                        If DRRec4.Read Then
                                                            Rptheader = DRRec4("Rptheader").ToString
                                                            RptFooter = DRRec4("RptFooter").ToString
                                                            RptBody = DRRec4("RptBody").ToString
                                                            RptBIU = DRRec4("RptBIU").ToString
                                                            RptShifted = DRRec4("RptShifted").ToString
                                                            RptSpaces = DRRec4("RptSpaces").ToString
                                                            RptSCT = DRRec4("RptSCT").ToString
                                                            CharsPerLines = DRRec4("CharsPerLines")
                                                            CountMethod = DRRec4("CountMethod").ToString
                                                            If DRRec4("isdefault").ToString.ToUpper = "TRUE" Then
                                                                DefaultLC = True
                                                            Else
                                                                DefaultLC = False
                                                            End If
                                                            strQuery = "Select NewID() as UID"
                                                            strQuery = "Select distinct T.*, M.Type,M.Location,M.Priority,M.DictatorID,M.Jobnumber from SecureWeb.dbo.tblTranscriptionClientMain M Left Outer Join tblTransLC T ON T.TranscriptionID = M.TranscriptionID where T.TranscriptionID is not NULL and M.AccountID = '" & DRRec1("AccountID").ToString & "'   and M.datemodified >= '" & sdate & "' and M.datemodified < '" & edate & "' "

                                                            If Dcount = 1 Then
                                                                strQuery = strQuery & " and M.Type in ('.Wav','.mp3') "
                                                            ElseIf Dcount = 2 Then
                                                                strQuery = strQuery & " and M.Type not in ('.Wav','.mp3') "
                                                            End If

                                                            strQuery = strQuery & "  order by JobNumber"

                                                            ''Response.Write(strQuery)
                                                            Dim i As Integer
                                                            i = 0
                                                            Dim SQLCmd5 As New SqlCommand(strQuery, New SqlConnection(strConn))
                                                            Try
                                                                SQLCmd5.Connection.Open()
                                                                Dim DRRec5 As SqlDataReader = SQLCmd5.ExecuteReader()
                                                                If DRRec5.HasRows = True Then
                                                                    RecFound = True
                                                                    While DRRec5.Read
                                                                        i = i + 1
                                                                        ReportLC = 0
                                                                        TranscriptionID = DRRec5("TranscriptionId").ToString
                                                                        stdLCCount = DRRec5("stdLC")
                                                                        If Dcount = 1 Then
                                                                            Rate = DRRec3("Rate")
                                                                        ElseIf Dcount = 2 Then
                                                                            Rate = DRRec3("MiscRate")
                                                                        End If
                                                                        If CountMethod = "PerDictator" Then
                                                                            ReportLC = DRRec5("stdLC")
                                                                        End If

                                                                        ''Response.Write(DRRec5("transcriptionid").ToString & "#")
                                                                        If CountMethod = "PerReport" Then
                                                                            ReportLC = 1
                                                                        ElseIf DefaultLC = True Then
                                                                            ReportLC = DRRec5("billingLC")
                                                                        Else
                                                                            If CountMethod = "CharsPerLine" Then
                                                                                If RptBody = True Then
                                                                                    '        'Response.Write(ReportLC & "#")
                                                                                    If RptSpaces = True Then
                                                                                        ReportLC += DRRec5("NumBodyCharsWSpaces")
                                                                                    Else
                                                                                        ReportLC += DRRec5("NumBodyCharsWOSpaces")
                                                                                    End If
                                                                                    '       'Response.Write(ReportLC & "#")
                                                                                    If RptSCT = True Then
                                                                                        ReportLC += DRRec5("NumBodyBTRChars")
                                                                                    End If
                                                                                    '      'Response.Write(ReportLC & "#")
                                                                                    If RptShifted = True Then
                                                                                        ReportLC += DRRec5("NumBodyCharsWShift")
                                                                                    End If
                                                                                    '     'Response.Write(ReportLC & "#")
                                                                                    If RptBIU = True Then
                                                                                        ReportLC += DRRec5("NumBodyBIU")
                                                                                    End If
                                                                                End If

                                                                                If Rptheader = True Then
                                                                                    ReportLC += DRRec5("HeaderCount")
                                                                                    '    'Response.Write(ReportLC & "#")
                                                                                    If RptSpaces = True Then
                                                                                        ReportLC += DRRec5("HeaderCountWSpaces")
                                                                                    End If
                                                                                    '   'Response.Write(ReportLC & "#")
                                                                                    If RptSCT = True Then
                                                                                        ReportLC += (DRRec5("HeaderBTRCount") - DRRec5("HeaderCount"))
                                                                                    End If
                                                                                    If RptShifted = True Then
                                                                                        ReportLC += DRRec5("HeaderWShift")
                                                                                    End If
                                                                                    If RptBIU = True Then
                                                                                        ReportLC += DRRec5("HeaderBIUCount")
                                                                                    End If
                                                                                    '  'Response.Write(ReportLC & "#")
                                                                                End If

                                                                                If RptFooter = True Then
                                                                                    ' 'Response.Write(ReportLC & "#")
                                                                                    ReportLC += DRRec5("FooterCount")
                                                                                    If RptSpaces = True Then
                                                                                        ReportLC += DRRec5("FooterCountWSpaces")
                                                                                    End If
                                                                                    If RptSCT = True Then
                                                                                        ReportLC += (DRRec5("footerBTRCount") - DRRec5("FooterCount"))
                                                                                    End If
                                                                                    If RptShifted = True Then
                                                                                        ReportLC += DRRec5("FooterWShift")
                                                                                    End If
                                                                                    If RptBIU = True Then
                                                                                        ReportLC += DRRec5("FooterBIUCount")
                                                                                    End If
                                                                                    ''Response.Write(ReportLC & "#")

                                                                                End If
                                                                                If CharsPerLines > 0 Then
                                                                                    ReportLC = ReportLC / CharsPerLines
                                                                                End If
                                                                            End If


                                                                            If CountMethod = "Words" Then
                                                                                If RptBody = True Then
                                                                                    ReportLC += DRRec5("NumBodywords")

                                                                                End If

                                                                                If Rptheader = True Then
                                                                                    ReportLC += DRRec5("Numheaderwords")
                                                                                End If

                                                                                If RptFooter = True Then
                                                                                    ReportLC += DRRec5("Numfooterwords")
                                                                                End If
                                                                            End If

                                                                            If CountMethod = "Pages" Then
                                                                                ReportLC += DRRec5("NumPages")

                                                                            End If
                                                                            If CountMethod = "GrossLines" Then
                                                                                If RptBody = True Then
                                                                                    ReportLC += DRRec5("bodyLines")

                                                                                End If

                                                                                If Rptheader = True Then
                                                                                    ReportLC += DRRec5("GrossHeaderCount")
                                                                                End If

                                                                                If RptFooter = True Then
                                                                                    ReportLC += DRRec5("GrossFooterCount")
                                                                                End If

                                                                            End If

                                                                            If CountMethod = "AllLines" Then
                                                                                If RptBody = True Then
                                                                                    ReportLC += DRRec5("bodyLinesWBlanks")

                                                                                End If

                                                                                If Rptheader = True Then
                                                                                    ReportLC += DRRec5("GrossHeaderCountWBlanks")
                                                                                End If

                                                                                If RptFooter = True Then
                                                                                    ReportLC += DRRec5("GrossFooterCountWBlanks")
                                                                                End If
                                                                            End If
                                                                        End If

                                                                        Dim Row1 As New TableRow
                                                                        Dim Cell1 As New TableCell
                                                                        Dim Cell2 As New TableCell
                                                                        Dim Cell3 As New TableCell
                                                                        Dim Cell4 As New TableCell
                                                                        Dim Cell5 As New TableCell
                                                                        Dim Cell6 As New TableCell
                                                                        Dim Cell As New TableCell
                                                                        Cell.Text = i
                                                                        Cell1.Text = DRRec1("AccountName").ToString
                                                                        Cell2.Text = Trim(DRRec1("Mode").ToString)
                                                                        Cell3.Text = DRRec5("Jobnumber").ToString
                                                                        Cell4.Text = ReportLC
                                                                        Cell4.Text = ReportLC
                                                                        Cell5.Text = CountMethod
                                                                        Row1.Cells.Add(Cell)
                                                                        Row1.Cells.Add(Cell1)
                                                                        Row1.Cells.Add(Cell2)
                                                                        Row1.Cells.Add(Cell3)
                                                                        Row1.Cells.Add(Cell4)
                                                                        Row1.Cells.Add(Cell5)
                                                                        Table1.Rows.Add(Row1)
                                                                        Amount = ReportLC * Rate
                                                                        Type = DRRec5("Type").ToString

                                                                        strQuery = "Insert Into ETS.dbo.tblBillingLines (BillAccID, SubActID, TranscriptionID, stdLCCount, unit, Rate, Amount, Type,Priority, updateDate) Values ('" & BillAccID & "', '" & DevActID & "', '" & TranscriptionID & "','" & stdLCCount & "', '" & ReportLC & "', '" & Rate & "', '" & Amount & "', '" & Type & "', 'False', '" & Now & "') "
                                                                        ''Response.Write(strQuery)

                                                                        Dim SQLCmdUp3 As New SqlCommand(strQuery, New SqlConnection(strConn))
                                                                        SQLCmdUp3.Connection.Open()
                                                                        SQLCmdUp3.ExecuteNonQuery()
                                                                        SQLCmdUp3.Connection.Close()
                                                                        ''Response.Write("Priority: " & DRRec5("Priority").ToString)

                                                                        If DRRec5("Priority").ToString = "True" And Not CountMethod = "PerReport" Then

                                                                            Rate = DRRec3("StatRate")
                                                                            Amount = ReportLC * Rate
                                                                            strQuery = "Insert Into ETS.dbo.tblBillingLines (BillAccID, SubActID, TranscriptionID, stdLCCount, unit, Rate, Amount, Type, Priority, updateDate) Values ('" & BillAccID & "', '" & DevActID & "', '" & TranscriptionID & "','" & stdLCCount & "', '" & ReportLC & "', '" & Rate & "', '" & Amount & "', '" & Type & "',  'True', '" & Now & "') "
                                                                            Dim SQLCmdUpST As New SqlCommand(strQuery, New SqlConnection(strConn))
                                                                            SQLCmdUpST.Connection.Open()
                                                                            SQLCmdUpST.ExecuteNonQuery()
                                                                            SQLCmdUpST.Connection.Close()
                                                                        End If
                                                                    End While
                                                                End If
                                                            Finally
                                                                If SQLCmd5.Connection.State = System.Data.ConnectionState.Open Then
                                                                    SQLCmd5.Connection.Close()
                                                                    SQLCmd5 = Nothing
                                                                End If
                                                            End Try

                                                        End If
                                                    End If
                                                Finally
                                                    If SQLCmd4.Connection.State = System.Data.ConnectionState.Open Then
                                                        SQLCmd4.Connection.Close()
                                                        SQLCmd4 = Nothing
                                                    End If
                                                End Try

                                            Next
                                        End If
                                        GenLC = True
                                    End If
                                Finally
                                    If SQLCmd3.Connection.State = System.Data.ConnectionState.Open Then
                                        SQLCmd3.Connection.Close()
                                        SQLCmd3 = Nothing
                                    End If
                                End Try

                            ElseIf Trim(DRRec1("Mode").ToString) = "LC" Then

                                Dim LocCode As Integer
                                strQuery = "Select NewID() as UID"
                                Dim SQLCmd6 As New SqlCommand(strQuery, New SqlConnection(strConn))
                                SQLCmd6.Connection.Open()
                                BillAccID = SQLCmd6.ExecuteScalar.ToString
                                AccountID = DRRec1("AccountID").ToString
                                Cycle = DRRec1("Cycle").ToString
                                MinBilling = DRRec1("MinBilling").ToString
                                BillMonth = DLMonth.SelectedValue
                                BillYear = DLYear.SelectedValue
                                Mode = Trim(DRRec1("Mode").ToString)

                                ''Response.Write(BillAccID)
                                strQuery = "Insert Into SEcureWeb.dbo.tblBillAccounts (BillAccID, AccountID, Cycle, MinBilling, BillMonth, BillYear, BillCycle, Mode, ModDate) Values ('" & BillAccID & "', '" & AccountID & "','" & Cycle & "', '" & MinBilling & "', '" & BillMonth & "', '" & BillYear & "', '" & DLCycle.SelectedValue & "', '" & Mode & "', '" & Now & "') "
                                ''Response.Write(strQuery)
                                Dim SQLCmdUp1 As New SqlCommand(strQuery, New SqlConnection(strConn))
                                Try
                                    SQLCmdUp1.Connection.Open()
                                    SQLCmdUp1.ExecuteNonQuery()
                                Finally
                                    If SQLCmdUp1.Connection.State = System.Data.ConnectionState.Open Then
                                        SQLCmdUp1.Connection.Close()
                                        SQLCmdUp1 = Nothing
                                    End If
                                End Try
                                strQuery = "DELETE FROM ETS.dbo.tblbillinglines WHERE BillAccID ='11111111-1111-1111-1111-111111111111' AND Transcriptionid in (Select TranscriptionID FROM SecureWeb.dbo.tblTranscriptionClientMain where AccountID = '" & DRRec1("AccountID").ToString & "'   and datemodified >= '" & sdate & "' and datemodified < '" & edate & "') "
                                Dim SQLCmdDel As New SqlCommand(strQuery, New SqlConnection(strConn))
                                Try
                                    SQLCmdDel.Connection.Open()
                                    SQLCmdDel.ExecuteNonQuery()
                                Finally
                                    If SQLCmdDel.Connection.State = System.Data.ConnectionState.Open Then
                                        SQLCmdDel.Connection.Close()
                                        SQLCmdDel = Nothing
                                    End If
                                End Try
                                strQuery = "Select * from ETS.dbo.tblAccountsLocations where AccountID = '" & DRRec1("AccountID").ToString & "'  "
                                'Response.Write(strQuery)
                                Dim SQLCmdLC As New SqlCommand(strQuery, New SqlConnection(strConn))
                                Try
                                    SQLCmdLC.Connection.Open()
                                    Dim DRRecLC As SqlDataReader = SQLCmdLC.ExecuteReader()
                                    If DRRecLC.HasRows = True Then
                                        While DRRecLC.Read
                                            SubActID = DRRecLC("TrackID").ToString
                                            SubActName = DRRecLC("LocName").ToString
                                            LocCode = DRRecLC("LocCode").ToString
                                            strQuery = "Select * from Secureweb.dbo.BillDetails where AccountID = '" & DRRec1("AccountID").ToString & "' and SubActID = '" & SubActID & "'   "
                                            '  'Response.Write(strQuery)
                                            Dim SQLCmd3 As New SqlCommand(strQuery, New SqlConnection(strConn))
                                            Try
                                                SQLCmd3.Connection.Open()
                                                Dim DRRec3 As SqlDataReader = SQLCmd3.ExecuteReader()
                                                If DRRec3.HasRows = True Then
                                                    UDFound = True
                                                    If DRRec3.Read Then
                                                        ''Response.Write(strQuery)
                                                        strQuery = "Insert Into SecureWeb.dbo.tblAccBillDetails (BillAccID, AccountID, SubActID, SubActName, Rate, MiscRate, StatRate, LCMethodID,ModDate) Values ('" & BillAccID & "', '" & AccountID & "', '" & SubActID & "', '" & SubActName & "','" & DRRec3("Rate") & "', '" & DRRec3("MiscRate") & "', '" & DRRec3("StatRate") & "', '" & DRRec3("LCMethodID").ToString & "', '" & Now & "') "
                                                        'Response.Write(strQuery)
                                                        Dim SQLCmdUp2 As New SqlCommand(strQuery, New SqlConnection(strConn))
                                                        SQLCmdUp2.Connection.Open()
                                                        SQLCmdUp2.ExecuteNonQuery()
                                                        SQLCmdUp2.Connection.Close()
                                                        strQuery = "Select  * from ETS.dbo.tblLCMethodology where TrackID = '" & DRRec3("LCMethodID").ToString & "' "
                                                        '                'Response.Write(strQuery)
                                                        Dim SQLCmd4 As New SqlCommand(strQuery, New SqlConnection(strConn))
                                                        Try
                                                            SQLCmd4.Connection.Open()
                                                            Dim DRRec4 As SqlDataReader = SQLCmd4.ExecuteReader()
                                                            If DRRec4.HasRows = True Then
                                                                LCMthFound = True
                                                                If DRRec4.Read Then
                                                                    Rptheader = DRRec4("Rptheader").ToString
                                                                    RptFooter = DRRec4("RptFooter").ToString
                                                                    RptBody = DRRec4("RptBody").ToString
                                                                    RptBIU = DRRec4("RptBIU").ToString
                                                                    RptShifted = DRRec4("RptShifted").ToString
                                                                    RptSpaces = DRRec4("RptSpaces").ToString
                                                                    RptSCT = DRRec4("RptSCT").ToString
                                                                    CharsPerLines = DRRec4("CharsPerLines")
                                                                    CountMethod = DRRec4("CountMethod").ToString

                                                                    If DRRec4("isdefault").ToString.ToUpper = "TRUE" Then
                                                                        DefaultLC = True
                                                                    Else
                                                                        DefaultLC = False
                                                                    End If

                                                                    strQuery = "Select distinct T.*, M.Type,M.Location,M.Priority,M.DictatorID,M.Jobnumber from SecureWeb.dbo.tblTranscriptionClientMain M Left Outer Join tblTransLC T ON T.TranscriptionID = M.TranscriptionID where T.TranscriptionID is not NULL and M.AccountID = '" & DRRec1("AccountID").ToString & "'   and M.datemodified >= '" & sdate & "' and M.datemodified < '" & edate & "' and M.Location ='" & LocCode & "' order by JobNumber"
                                                                    'Response.Write(strQuery)
                                                                    Dim i As Integer
                                                                    i = 0
                                                                    Dim SQLCmd5 As New SqlCommand(strQuery, New SqlConnection(strConn))
                                                                    Try
                                                                        SQLCmd5.Connection.Open()
                                                                        Dim DRRec5 As SqlDataReader = SQLCmd5.ExecuteReader()
                                                                        If DRRec5.HasRows = True Then
                                                                            RecFound = True
                                                                            While DRRec5.Read
                                                                                i = i + 1
                                                                                ReportLC = 0
                                                                                TranscriptionID = DRRec5("TranscriptionId").ToString
                                                                                stdLCCount = DRRec5("stdLC")
                                                                                Rate = DRRec3("Rate")
                                                                                If CountMethod = "PerDictator" Then
                                                                                    ReportLC = DRRec5("stdLC")
                                                                                End If
                                                                                ''Response.Write(DRRec5("transcriptionid").ToString & "#")
                                                                                If CountMethod = "PerReport" Then
                                                                                    ReportLC = 1
                                                                                ElseIf DefaultLC = True Then
                                                                                    ReportLC = DRRec5("billingLC")
                                                                                Else
                                                                                    If CountMethod = "CharsPerLine" Then
                                                                                        If RptBody = True Then
                                                                                            '        'Response.Write(ReportLC & "#")
                                                                                            If RptSpaces = True Then
                                                                                                ReportLC += DRRec5("NumBodyCharsWSpaces")
                                                                                            Else
                                                                                                ReportLC += DRRec5("NumBodyCharsWOSpaces")
                                                                                            End If
                                                                                            '       'Response.Write(ReportLC & "#")
                                                                                            If RptSCT = True Then
                                                                                                ReportLC += DRRec5("NumBodyBTRChars")
                                                                                            End If
                                                                                            '      'Response.Write(ReportLC & "#")
                                                                                            If RptShifted = True Then
                                                                                                ReportLC += DRRec5("NumBodyCharsWShift")
                                                                                            End If
                                                                                            '     'Response.Write(ReportLC & "#")
                                                                                            If RptBIU = True Then
                                                                                                ReportLC += DRRec5("NumBodyBIU")
                                                                                            End If
                                                                                        End If

                                                                                        If Rptheader = True Then
                                                                                            ReportLC += DRRec5("HeaderCount")
                                                                                            '    'Response.Write(ReportLC & "#")
                                                                                            If RptSpaces = True Then
                                                                                                ReportLC += DRRec5("HeaderCountWSpaces")
                                                                                            End If
                                                                                            '   'Response.Write(ReportLC & "#")
                                                                                            If RptSCT = True Then
                                                                                                ReportLC += (DRRec5("HeaderBTRCount") - DRRec5("HeaderCount"))
                                                                                            End If
                                                                                            If RptShifted = True Then
                                                                                                ReportLC += DRRec5("HeaderWShift")
                                                                                            End If
                                                                                            If RptBIU = True Then
                                                                                                ReportLC += DRRec5("HeaderBIUCount")
                                                                                            End If
                                                                                            '  'Response.Write(ReportLC & "#")
                                                                                        End If

                                                                                        If RptFooter = True Then
                                                                                            ' 'Response.Write(ReportLC & "#")
                                                                                            ReportLC += DRRec5("FooterCount")
                                                                                            If RptSpaces = True Then
                                                                                                ReportLC += DRRec5("FooterCountWSpaces")
                                                                                            End If
                                                                                            If RptSCT = True Then
                                                                                                ReportLC += (DRRec5("footerBTRCount") - DRRec5("FooterCount"))
                                                                                            End If
                                                                                            If RptShifted = True Then
                                                                                                ReportLC += DRRec5("FooterWShift")
                                                                                            End If
                                                                                            If RptBIU = True Then
                                                                                                ReportLC += DRRec5("FooterBIUCount")
                                                                                            End If
                                                                                            ''Response.Write(ReportLC & "#")

                                                                                        End If
                                                                                        If CharsPerLines > 0 Then
                                                                                            ReportLC = ReportLC / CharsPerLines
                                                                                        End If
                                                                                    End If


                                                                                    If CountMethod = "Words" Then
                                                                                        If RptBody = True Then
                                                                                            ReportLC += DRRec5("NumBodywords")

                                                                                        End If

                                                                                        If Rptheader = True Then
                                                                                            ReportLC += DRRec5("Numheaderwords")
                                                                                        End If

                                                                                        If RptFooter = True Then
                                                                                            ReportLC += DRRec5("Numfooterwords")
                                                                                        End If
                                                                                    End If

                                                                                    If CountMethod = "Pages" Then
                                                                                        ReportLC += DRRec5("NumPages")

                                                                                    End If
                                                                                    If CountMethod = "GrossLines" Then
                                                                                        If RptBody = True Then
                                                                                            ReportLC += DRRec5("bodyLines")

                                                                                        End If

                                                                                        If Rptheader = True Then
                                                                                            ReportLC += DRRec5("GrossHeaderCount")
                                                                                        End If

                                                                                        If RptFooter = True Then
                                                                                            ReportLC += DRRec5("GrossFooterCount")
                                                                                        End If

                                                                                    End If

                                                                                    If CountMethod = "AllLines" Then
                                                                                        If RptBody = True Then
                                                                                            ReportLC += DRRec5("bodyLinesWBlanks")

                                                                                        End If

                                                                                        If Rptheader = True Then
                                                                                            ReportLC += DRRec5("GrossHeaderCountWBlanks")
                                                                                        End If

                                                                                        If RptFooter = True Then
                                                                                            ReportLC += DRRec5("GrossFooterCountWBlanks")
                                                                                        End If
                                                                                    End If
                                                                                End If

                                                                                Dim Row1 As New TableRow
                                                                                Dim Cell1 As New TableCell
                                                                                Dim Cell2 As New TableCell
                                                                                Dim Cell3 As New TableCell
                                                                                Dim Cell4 As New TableCell
                                                                                Dim Cell5 As New TableCell
                                                                                Dim Cell6 As New TableCell
                                                                                Dim Cell7 As New TableCell
                                                                                Dim Cell As New TableCell
                                                                                Cell.Text = i
                                                                                Cell1.Text = DRRec1("AccountName").ToString
                                                                                Cell2.Text = Trim(DRRec1("Mode").ToString)
                                                                                Cell3.Text = DRRec5("Jobnumber").ToString
                                                                                Cell4.Text = ReportLC
                                                                                Cell4.Text = ReportLC
                                                                                Cell5.Text = CountMethod
                                                                                Cell7.Text = SubActName
                                                                                Row1.Cells.Add(Cell)
                                                                                Row1.Cells.Add(Cell1)
                                                                                Row1.Cells.Add(Cell2)
                                                                                Row1.Cells.Add(Cell3)
                                                                                Row1.Cells.Add(Cell4)
                                                                                Row1.Cells.Add(Cell5)
                                                                                Row1.Cells.Add(Cell7)
                                                                                Table1.Rows.Add(Row1)
                                                                                Amount = ReportLC * Rate
                                                                                Type = DRRec5("Type").ToString



                                                                                strQuery = "Insert Into ETS.dbo.tblBillingLines (BillAccID, SubActID, TranscriptionID, stdLCCount, unit, Rate, Amount, Type, Priority, updateDate) Values ('" & BillAccID & "', '" & SubActID & "', '" & TranscriptionID & "','" & stdLCCount & "', '" & ReportLC & "', '" & Rate & "', '" & Amount & "', '" & Type & "',  'False', '" & Now & "') "
                                                                                Dim SQLCmdUp3 As New SqlCommand(strQuery, New SqlConnection(strConn))
                                                                                Try
                                                                                    SQLCmdUp3.Connection.Open()
                                                                                    SQLCmdUp3.ExecuteNonQuery()
                                                                                Finally
                                                                                    If SQLCmdUp3.Connection.State = System.Data.ConnectionState.Open Then
                                                                                        SQLCmdUp3.Connection.Close()
                                                                                        SQLCmdUp3 = Nothing
                                                                                    End If
                                                                                End Try


                                                                                If DRRec5("Priority").ToString = "True" And Not CountMethod = "PerReport" Then

                                                                                    Rate = DRRec3("StatRate")
                                                                                    Amount = ReportLC * Rate
                                                                                    strQuery = "Insert Into ETS.dbo.tblBillingLines (BillAccID, SubActID, TranscriptionID, stdLCCount, unit, Rate, Amount, Type, Priority, updateDate) Values ('" & BillAccID & "', '" & SubActID & "', '" & TranscriptionID & "','" & stdLCCount & "', '" & ReportLC & "', '" & Rate & "', '" & Amount & "', '" & Type & "', 'True', '" & Now & "') "
                                                                                    Dim SQLCmdUpST As New SqlCommand(strQuery, New SqlConnection(strConn))
                                                                                    Try
                                                                                        SQLCmdUpST.Connection.Open()
                                                                                        SQLCmdUpST.ExecuteNonQuery()
                                                                                    Finally
                                                                                        If SQLCmdUpST.Connection.State = System.Data.ConnectionState.Open Then
                                                                                            SQLCmdUpST.Connection.Close()
                                                                                            SQLCmdUpST = Nothing
                                                                                        End If
                                                                                    End Try

                                                                                End If


                                                                            End While
                                                                        End If
                                                                    Finally
                                                                        If SQLCmd5.Connection.State = System.Data.ConnectionState.Open Then
                                                                            SQLCmd5.Connection.Close()
                                                                            SQLCmd5 = Nothing
                                                                        End If
                                                                    End Try

                                                                End If
                                                            End If
                                                        Finally
                                                            If SQLCmd4.Connection.State = System.Data.ConnectionState.Open Then
                                                                SQLCmd4.Connection.Close()
                                                                SQLCmd4 = Nothing
                                                            End If
                                                        End Try
                                                    End If
                                                End If
                                            Finally
                                                If SQLCmd3.Connection.State = System.Data.ConnectionState.Open Then
                                                    SQLCmd3.Connection.Close()
                                                    SQLCmd3 = Nothing
                                                End If
                                            End Try

                                        End While
                                        GenLC = True
                                    End If
                                Finally
                                    If SQLCmdLC.Connection.State = System.Data.ConnectionState.Open Then
                                        SQLCmdLC.Connection.Close()
                                        SQLCmdLC = Nothing
                                    End If
                                End Try
                            ElseIf Trim(DRRec1("Mode").ToString) = "TT" Then

                                Dim TAT As Integer
                                strQuery = "Select NewID() as UID"
                                Dim SQLCmd6 As New SqlCommand(strQuery, New SqlConnection(strConn))
                                SQLCmd6.Connection.Open()
                                BillAccID = SQLCmd6.ExecuteScalar.ToString
                                AccountID = DRRec1("AccountID").ToString
                                Cycle = DRRec1("Cycle").ToString
                                MinBilling = DRRec1("MinBilling").ToString
                                BillMonth = DLMonth.SelectedValue
                                BillYear = DLYear.SelectedValue
                                Mode = Trim(DRRec1("Mode").ToString)

                                ''Response.Write(BillAccID)
                                strQuery = "Insert Into SEcureWeb.dbo.tblBillAccounts (BillAccID, AccountID, Cycle, MinBilling, BillMonth, BillYear, BillCycle, Mode, ModDate) Values ('" & BillAccID & "', '" & AccountID & "','" & Cycle & "', '" & MinBilling & "', '" & BillMonth & "', '" & BillYear & "', '" & DLCycle.SelectedValue & "', '" & Mode & "', '" & Now & "') "
                                ''Response.Write(strQuery)
                                Dim SQLCmdUp1 As New SqlCommand(strQuery, New SqlConnection(strConn))
                                Try
                                    SQLCmdUp1.Connection.Open()
                                    SQLCmdUp1.ExecuteNonQuery()
                                Finally
                                    If SQLCmdUp1.Connection.State = System.Data.ConnectionState.Open Then
                                        SQLCmdUp1.Connection.Close()
                                        SQLCmdUp1 = Nothing
                                    End If
                                End Try
                                strQuery = "DELETE FROM ETS.dbo.tblbillinglines WHERE BillAccID ='11111111-1111-1111-1111-111111111111' AND Transcriptionid in (Select TranscriptionID FROM SecureWeb.dbo.tblTranscriptionClientMain where AccountID = '" & DRRec1("AccountID").ToString & "'   and datemodified >= '" & sdate & "' and datemodified < '" & edate & "') "
                                Dim SQLCmdDel As New SqlCommand(strQuery, New SqlConnection(strConn))
                                Try
                                    SQLCmdDel.Connection.Open()
                                    SQLCmdDel.ExecuteNonQuery()
                                Finally
                                    If SQLCmdDel.Connection.State = System.Data.ConnectionState.Open Then
                                        SQLCmdDel.Connection.Close()
                                        SQLCmdDel = Nothing
                                    End If
                                End Try
                                strQuery = "Select * from SecureWeb.dbo.tblAccountsTAT where AccountID = '" & DRRec1("AccountID").ToString & "'  "
                                'Response.Write(strQuery)
                                Dim SQLCmdLC As New SqlCommand(strQuery, New SqlConnection(strConn))
                                Try
                                    SQLCmdLC.Connection.Open()
                                    Dim DRRecLC As SqlDataReader = SQLCmdLC.ExecuteReader()
                                    If DRRecLC.HasRows = True Then
                                        While DRRecLC.Read
                                            SubActID = DRRecLC("TrackID").ToString
                                            SubActName = DRRecLC("TAT").ToString
                                            TAT = DRRecLC("TAT").ToString
                                            strQuery = "Select * from Secureweb.dbo.BillDetails where AccountID = '" & DRRec1("AccountID").ToString & "' and SubActID = '" & SubActID & "'   "
                                            '  'Response.Write(strQuery)
                                            Dim SQLCmd3 As New SqlCommand(strQuery, New SqlConnection(strConn))
                                            Try
                                                SQLCmd3.Connection.Open()
                                                Dim DRRec3 As SqlDataReader = SQLCmd3.ExecuteReader()
                                                If DRRec3.HasRows = True Then
                                                    UDFound = True
                                                    If DRRec3.Read Then
                                                        ''Response.Write(strQuery)
                                                        strQuery = "Insert Into SecureWeb.dbo.tblAccBillDetails (BillAccID, AccountID, SubActID, SubActName, Rate, MiscRate, StatRate, LCMethodID,ModDate) Values ('" & BillAccID & "', '" & AccountID & "', '" & SubActID & "', '" & SubActName & "','" & DRRec3("Rate") & "', '" & DRRec3("MiscRate") & "', '" & DRRec3("StatRate") & "', '" & DRRec3("LCMethodID").ToString & "', '" & Now & "') "
                                                        'Response.Write(strQuery)
                                                        Dim SQLCmdUp2 As New SqlCommand(strQuery, New SqlConnection(strConn))
                                                        SQLCmdUp2.Connection.Open()
                                                        SQLCmdUp2.ExecuteNonQuery()
                                                        SQLCmdUp2.Connection.Close()
                                                        strQuery = "Select  * from ETS.dbo.tblLCMethodology where TrackID = '" & DRRec3("LCMethodID").ToString & "' "
                                                        '                'Response.Write(strQuery)
                                                        Dim SQLCmd4 As New SqlCommand(strQuery, New SqlConnection(strConn))
                                                        Try
                                                            SQLCmd4.Connection.Open()
                                                            Dim DRRec4 As SqlDataReader = SQLCmd4.ExecuteReader()
                                                            If DRRec4.HasRows = True Then
                                                                LCMthFound = True
                                                                If DRRec4.Read Then
                                                                    Rptheader = DRRec4("Rptheader").ToString
                                                                    RptFooter = DRRec4("RptFooter").ToString
                                                                    RptBody = DRRec4("RptBody").ToString
                                                                    RptBIU = DRRec4("RptBIU").ToString
                                                                    RptShifted = DRRec4("RptShifted").ToString
                                                                    RptSpaces = DRRec4("RptSpaces").ToString
                                                                    RptSCT = DRRec4("RptSCT").ToString
                                                                    CharsPerLines = DRRec4("CharsPerLines")
                                                                    CountMethod = DRRec4("CountMethod").ToString

                                                                    If DRRec4("isdefault").ToString.ToUpper = "TRUE" Then
                                                                        DefaultLC = True
                                                                    Else
                                                                        DefaultLC = False
                                                                    End If

                                                                    strQuery = "Select distinct T.*, M.Type,M.TAT,M.Priority,M.DictatorID,M.Jobnumber from SecureWeb.dbo.tblTranscriptionClientMain M Left Outer Join tblTransLC T ON T.TranscriptionID = M.TranscriptionID where T.TranscriptionID is not NULL and M.AccountID = '" & DRRec1("AccountID").ToString & "'   and M.datemodified >= '" & sdate & "' and M.datemodified < '" & edate & "' and M.TAT ='" & TAT & "' order by JobNumber"
                                                                    'Response.Write(strQuery)
                                                                    Dim i As Integer
                                                                    i = 0
                                                                    Dim SQLCmd5 As New SqlCommand(strQuery, New SqlConnection(strConn))
                                                                    Try
                                                                        SQLCmd5.Connection.Open()
                                                                        Dim DRRec5 As SqlDataReader = SQLCmd5.ExecuteReader()
                                                                        If DRRec5.HasRows = True Then
                                                                            RecFound = True
                                                                            While DRRec5.Read
                                                                                i = i + 1
                                                                                ReportLC = 0
                                                                                TranscriptionID = DRRec5("TranscriptionId").ToString
                                                                                stdLCCount = DRRec5("stdLC")
                                                                                Rate = DRRec3("Rate")
                                                                                If CountMethod = "PerDictator" Then
                                                                                    ReportLC = DRRec5("stdLC")
                                                                                End If
                                                                                ''Response.Write(DRRec5("transcriptionid").ToString & "#")
                                                                                If CountMethod = "PerReport" Then
                                                                                    ReportLC = 1
                                                                                ElseIf DefaultLC = True Then
                                                                                    ReportLC = DRRec5("billingLC")
                                                                                Else
                                                                                    If CountMethod = "CharsPerLine" Then
                                                                                        If RptBody = True Then
                                                                                            '        'Response.Write(ReportLC & "#")
                                                                                            If RptSpaces = True Then
                                                                                                ReportLC += DRRec5("NumBodyCharsWSpaces")
                                                                                            Else
                                                                                                ReportLC += DRRec5("NumBodyCharsWOSpaces")
                                                                                            End If
                                                                                            '       'Response.Write(ReportLC & "#")
                                                                                            If RptSCT = True Then
                                                                                                ReportLC += DRRec5("NumBodyBTRChars")
                                                                                            End If
                                                                                            '      'Response.Write(ReportLC & "#")
                                                                                            If RptShifted = True Then
                                                                                                ReportLC += DRRec5("NumBodyCharsWShift")
                                                                                            End If
                                                                                            '     'Response.Write(ReportLC & "#")
                                                                                            If RptBIU = True Then
                                                                                                ReportLC += DRRec5("NumBodyBIU")
                                                                                            End If
                                                                                        End If

                                                                                        If Rptheader = True Then
                                                                                            ReportLC += DRRec5("HeaderCount")
                                                                                            '    'Response.Write(ReportLC & "#")
                                                                                            If RptSpaces = True Then
                                                                                                ReportLC += DRRec5("HeaderCountWSpaces")
                                                                                            End If
                                                                                            '   'Response.Write(ReportLC & "#")
                                                                                            If RptSCT = True Then
                                                                                                ReportLC += (DRRec5("HeaderBTRCount") - DRRec5("HeaderCount"))
                                                                                            End If
                                                                                            If RptShifted = True Then
                                                                                                ReportLC += DRRec5("HeaderWShift")
                                                                                            End If
                                                                                            If RptBIU = True Then
                                                                                                ReportLC += DRRec5("HeaderBIUCount")
                                                                                            End If
                                                                                            '  'Response.Write(ReportLC & "#")
                                                                                        End If

                                                                                        If RptFooter = True Then
                                                                                            ' 'Response.Write(ReportLC & "#")
                                                                                            ReportLC += DRRec5("FooterCount")
                                                                                            If RptSpaces = True Then
                                                                                                ReportLC += DRRec5("FooterCountWSpaces")
                                                                                            End If
                                                                                            If RptSCT = True Then
                                                                                                ReportLC += (DRRec5("footerBTRCount") - DRRec5("FooterCount"))
                                                                                            End If
                                                                                            If RptShifted = True Then
                                                                                                ReportLC += DRRec5("FooterWShift")
                                                                                            End If
                                                                                            If RptBIU = True Then
                                                                                                ReportLC += DRRec5("FooterBIUCount")
                                                                                            End If
                                                                                            ''Response.Write(ReportLC & "#")

                                                                                        End If
                                                                                        If CharsPerLines > 0 Then
                                                                                            ReportLC = ReportLC / CharsPerLines
                                                                                        End If
                                                                                    End If


                                                                                    If CountMethod = "Words" Then
                                                                                        If RptBody = True Then
                                                                                            ReportLC += DRRec5("NumBodywords")

                                                                                        End If

                                                                                        If Rptheader = True Then
                                                                                            ReportLC += DRRec5("Numheaderwords")
                                                                                        End If

                                                                                        If RptFooter = True Then
                                                                                            ReportLC += DRRec5("Numfooterwords")
                                                                                        End If
                                                                                    End If

                                                                                    If CountMethod = "Pages" Then
                                                                                        ReportLC += DRRec5("NumPages")

                                                                                    End If
                                                                                    If CountMethod = "GrossLines" Then
                                                                                        If RptBody = True Then
                                                                                            ReportLC += DRRec5("bodyLines")

                                                                                        End If

                                                                                        If Rptheader = True Then
                                                                                            ReportLC += DRRec5("GrossHeaderCount")
                                                                                        End If

                                                                                        If RptFooter = True Then
                                                                                            ReportLC += DRRec5("GrossFooterCount")
                                                                                        End If

                                                                                    End If

                                                                                    If CountMethod = "AllLines" Then
                                                                                        If RptBody = True Then
                                                                                            ReportLC += DRRec5("bodyLinesWBlanks")

                                                                                        End If

                                                                                        If Rptheader = True Then
                                                                                            ReportLC += DRRec5("GrossHeaderCountWBlanks")
                                                                                        End If

                                                                                        If RptFooter = True Then
                                                                                            ReportLC += DRRec5("GrossFooterCountWBlanks")
                                                                                        End If
                                                                                    End If
                                                                                End If

                                                                                Dim Row1 As New TableRow
                                                                                Dim Cell1 As New TableCell
                                                                                Dim Cell2 As New TableCell
                                                                                Dim Cell3 As New TableCell
                                                                                Dim Cell4 As New TableCell
                                                                                Dim Cell5 As New TableCell
                                                                                Dim Cell6 As New TableCell
                                                                                Dim Cell7 As New TableCell
                                                                                Dim Cell As New TableCell
                                                                                Cell.Text = i
                                                                                Cell1.Text = DRRec1("AccountName").ToString
                                                                                Cell2.Text = Trim(DRRec1("Mode").ToString)
                                                                                Cell3.Text = DRRec5("Jobnumber").ToString
                                                                                Cell4.Text = ReportLC
                                                                                Cell4.Text = ReportLC
                                                                                Cell5.Text = CountMethod
                                                                                Cell7.Text = SubActName
                                                                                Row1.Cells.Add(Cell)
                                                                                Row1.Cells.Add(Cell1)
                                                                                Row1.Cells.Add(Cell2)
                                                                                Row1.Cells.Add(Cell3)
                                                                                Row1.Cells.Add(Cell4)
                                                                                Row1.Cells.Add(Cell5)
                                                                                Row1.Cells.Add(Cell7)
                                                                                Table1.Rows.Add(Row1)
                                                                                Amount = ReportLC * Rate
                                                                                Type = DRRec5("Type").ToString



                                                                                strQuery = "Insert Into ETS.dbo.tblBillingLines (BillAccID, SubActID, TranscriptionID, stdLCCount, unit, Rate, Amount, Type, Priority, updateDate) Values ('" & BillAccID & "', '" & SubActID & "', '" & TranscriptionID & "','" & stdLCCount & "', '" & ReportLC & "', '" & Rate & "', '" & Amount & "', '" & Type & "',  'False', '" & Now & "') "
                                                                                Dim SQLCmdUp3 As New SqlCommand(strQuery, New SqlConnection(strConn))
                                                                                Try
                                                                                    SQLCmdUp3.Connection.Open()
                                                                                    SQLCmdUp3.ExecuteNonQuery()
                                                                                Finally
                                                                                    If SQLCmdUp3.Connection.State = System.Data.ConnectionState.Open Then
                                                                                        SQLCmdUp3.Connection.Close()
                                                                                        SQLCmdUp3 = Nothing
                                                                                    End If
                                                                                End Try


                                                                                If DRRec5("Priority").ToString = "True" And Not CountMethod = "PerReport" Then

                                                                                    Rate = DRRec3("StatRate")
                                                                                    Amount = ReportLC * Rate
                                                                                    strQuery = "Insert Into ETS.dbo.tblBillingLines (BillAccID, SubActID, TranscriptionID, stdLCCount, unit, Rate, Amount, Type, Priority, updateDate) Values ('" & BillAccID & "', '" & SubActID & "', '" & TranscriptionID & "','" & stdLCCount & "', '" & ReportLC & "', '" & Rate & "', '" & Amount & "', '" & Type & "', 'True', '" & Now & "') "
                                                                                    Dim SQLCmdUpST As New SqlCommand(strQuery, New SqlConnection(strConn))
                                                                                    Try
                                                                                        SQLCmdUpST.Connection.Open()
                                                                                        SQLCmdUpST.ExecuteNonQuery()
                                                                                    Finally
                                                                                        If SQLCmdUpST.Connection.State = System.Data.ConnectionState.Open Then
                                                                                            SQLCmdUpST.Connection.Close()
                                                                                            SQLCmdUpST = Nothing
                                                                                        End If
                                                                                    End Try

                                                                                End If


                                                                            End While
                                                                        End If
                                                                    Finally
                                                                        If SQLCmd5.Connection.State = System.Data.ConnectionState.Open Then
                                                                            SQLCmd5.Connection.Close()
                                                                            SQLCmd5 = Nothing
                                                                        End If
                                                                    End Try

                                                                End If
                                                            End If
                                                        Finally
                                                            If SQLCmd4.Connection.State = System.Data.ConnectionState.Open Then
                                                                SQLCmd4.Connection.Close()
                                                                SQLCmd4 = Nothing
                                                            End If
                                                        End Try
                                                    End If
                                                End If
                                            Finally
                                                If SQLCmd3.Connection.State = System.Data.ConnectionState.Open Then
                                                    SQLCmd3.Connection.Close()
                                                    SQLCmd3 = Nothing
                                                End If
                                            End Try

                                        End While
                                        GenLC = True
                                    End If
                                Finally
                                    If SQLCmdLC.Connection.State = System.Data.ConnectionState.Open Then
                                        SQLCmdLC.Connection.Close()
                                        SQLCmdLC = Nothing
                                    End If
                                End Try

                            ElseIf Trim(DRRec1("Mode").ToString) = "DC" Then
                                Dim LocCode As Integer
                                strQuery = "Select NewID() as UID"
                                Dim SQLCmd6 As New SqlCommand(strQuery, New SqlConnection(strConn))
                                SQLCmd6.Connection.Open()
                                BillAccID = SQLCmd6.ExecuteScalar.ToString
                                AccountID = DRRec1("AccountID").ToString
                                Cycle = DRRec1("Cycle").ToString
                                MinBilling = DRRec1("MinBilling").ToString
                                BillMonth = DLMonth.SelectedValue
                                BillYear = DLYear.SelectedValue
                                Mode = Trim(DRRec1("Mode").ToString)

                                ''Response.Write(BillAccID)
                                strQuery = "Insert Into SEcureWeb.dbo.tblBillAccounts (BillAccID, AccountID, Cycle, MinBilling, BillMonth, BillYear, BillCycle, Mode, ModDate) Values ('" & BillAccID & "', '" & AccountID & "','" & Cycle & "', '" & MinBilling & "', '" & BillMonth & "', '" & BillYear & "', '" & DLCycle.SelectedValue & "', '" & Mode & "', '" & Now & "') "
                                Dim SQLCmdUp1 As New SqlCommand(strQuery, New SqlConnection(strConn))
                                Try
                                    SQLCmdUp1.Connection.Open()
                                    SQLCmdUp1.ExecuteNonQuery()
                                Finally
                                    If SQLCmdUp1.Connection.State = System.Data.ConnectionState.Open Then
                                        SQLCmdUp1.Connection.Close()
                                        SQLCmdUp1 = Nothing
                                    End If
                                End Try

                                strQuery = "DELETE FROM ETS.dbo.tblbillinglines WHERE BillAccID ='11111111-1111-1111-1111-111111111111' AND Transcriptionid in (Select TranscriptionID FROM SecureWeb.dbo.tblTranscriptionClientMain where AccountID = '" & DRRec1("AccountID").ToString & "'   and datemodified >= '" & sdate & "' and datemodified < '" & edate & "') "
                                Dim SQLCmdDel As New SqlCommand(strQuery, New SqlConnection(strConn))
                                Try
                                    SQLCmdDel.Connection.Open()
                                    SQLCmdDel.ExecuteNonQuery()
                                Finally
                                    If SQLCmdDel.Connection.State = System.Data.ConnectionState.Open Then
                                        SQLCmdDel.Connection.Close()
                                        SQLCmdDel = Nothing
                                    End If
                                End Try
                                strQuery = "Select * from SecureWeb.dbo.GrpDictators where AccID = '" & DRRec1("AccountID").ToString & "'  "
                                Dim SQLCmdLC As New SqlCommand(strQuery, New SqlConnection(strConn))
                                Try
                                    SQLCmdLC.Connection.Open()
                                    Dim DRRecLC As SqlDataReader = SQLCmdLC.ExecuteReader()
                                    If DRRecLC.HasRows = True Then
                                        While DRRecLC.Read
                                            SubActID = DRRecLC("GrpDicID").ToString
                                            SubActName = DRRecLC("GrpDicName").ToString

                                            strQuery = "Select * from Secureweb.dbo.BillDetails where AccountID = '" & DRRec1("AccountID").ToString & "' and SubActID = '" & SubActID & "'   "
                                            Dim SQLCmd3 As New SqlCommand(strQuery, New SqlConnection(strConn))
                                            Try
                                                SQLCmd3.Connection.Open()
                                                Dim DRRec3 As SqlDataReader = SQLCmd3.ExecuteReader()
                                                If DRRec3.HasRows = True Then
                                                    UDFound = True
                                                    If DRRec3.Read Then


                                                        ''Response.Write(strQuery)
                                                        strQuery = "Insert Into SEcureWeb.dbo.tblAccBillDetails (BillAccID, AccountID, SubActID, SubActName, Rate, MiscRate, StatRate, LCMethodID,ModDate) Values ('" & BillAccID & "', '" & AccountID & "', '" & SubActID & "', '" & SubActName & "','" & DRRec3("Rate") & "', '" & DRRec3("MiscRate") & "', '" & DRRec3("StatRate") & "', '" & DRRec3("LCMethodID").ToString & "', '" & Now & "') "
                                                        Dim SQLCmdUp2 As New SqlCommand(strQuery, New SqlConnection(strConn))
                                                        Try
                                                            SQLCmdUp2.Connection.Open()
                                                            SQLCmdUp2.ExecuteNonQuery()
                                                        Finally

                                                            If SQLCmdUp2.Connection.State = System.Data.ConnectionState.Open Then
                                                                SQLCmdUp2.Connection.Close()
                                                                SQLCmdUp2 = Nothing
                                                            End If
                                                        End Try


                                                        strQuery = "Select  * from ETS.dbo.tblLCMethodology where TrackID = '" & DRRec3("LCMethodID").ToString & "' "
                                                        Dim SQLCmd4 As New SqlCommand(strQuery, New SqlConnection(strConn))
                                                        Try
                                                            SQLCmd4.Connection.Open()
                                                            Dim DRRec4 As SqlDataReader = SQLCmd4.ExecuteReader()
                                                            If DRRec4.HasRows = True Then
                                                                LCMthFound = True
                                                                If DRRec4.Read Then
                                                                    Rptheader = DRRec4("Rptheader").ToString
                                                                    RptFooter = DRRec4("RptFooter").ToString
                                                                    RptBody = DRRec4("RptBody").ToString
                                                                    RptBIU = DRRec4("RptBIU").ToString
                                                                    RptShifted = DRRec4("RptShifted").ToString
                                                                    RptSpaces = DRRec4("RptSpaces").ToString
                                                                    RptSCT = DRRec4("RptSCT").ToString
                                                                    CharsPerLines = DRRec4("CharsPerLines")
                                                                    CountMethod = DRRec4("CountMethod").ToString

                                                                    If DRRec4("isdefault").ToString.ToUpper = "TRUE" Then
                                                                        DefaultLC = True
                                                                    Else
                                                                        DefaultLC = False
                                                                    End If
                                                                    strQuery = "Select distinct T.*, M.Type,M.Location,M.Priority,M.DictatorID,M.Jobnumber from SecureWeb.dbo.tblTranscriptionClientMain M INNER JOIN SEcureWEb.dbo.AssignDic A ON A.UserID = M.dictatorID and A.AccID = M.AccountID Left Outer Join tblTransLC T ON T.TranscriptionID = M.TranscriptionID where T.TranscriptionID is not NULL and M.AccountID = '" & DRRec1("AccountID").ToString & "'   and M.datemodified >= '" & sdate & "' and M.datemodified < '" & edate & "' and A.GrpDicID = '" & SubActID & "' order by JobNumber"
                                                                    'strQuery = "Select distinct * from SecureWeb.dbo.tblTranscriptionClientMain M INNER JOIN SEcureWEb.dbo.AssignDic A ON A.UserID = M.dictatorID and A.AccID = M.AccountID Left Outer Join tblTransLC T ON T.TranscriptionID = M.TranscriptionID where T.TranscriptionID is not NULL and M.AccountID = '" & DRRec1("AccountID").ToString & "'   and datediff(day,M.datemodified, '7/31/2010') between 0 and  30  and A.GrpDicID = '" & SubActID & "' order by JobNumber"
                                                                    ' Response.Write(strQuery)
                                                                    Dim i As Integer
                                                                    i = 0
                                                                    Dim SQLCmd5 As New SqlCommand(strQuery, New SqlConnection(strConn))
                                                                    SQLCmd5.CommandTimeout = 300
                                                                    Try
                                                                        SQLCmd5.Connection.Open()
                                                                        Dim DRRec5 As SqlDataReader = SQLCmd5.ExecuteReader()
                                                                        If DRRec5.HasRows = True Then
                                                                            RecFound = True
                                                                            While DRRec5.Read
                                                                                i = i + 1
                                                                                ReportLC = 0
                                                                                TranscriptionID = DRRec5("TranscriptionId").ToString
                                                                                stdLCCount = DRRec5("stdLC")
                                                                                Rate = DRRec3("Rate")

                                                                                If CountMethod = "CharsPerLine" Then
                                                                                    ReportLC = DRRec5("stdLC")
                                                                                End If
                                                                                If CountMethod = "PerDictator" Then
                                                                                    ReportLC = DRRec5("stdLC")
                                                                                    Rate = 0
                                                                                End If

                                                                                ''Response.Write(DRRec5("transcriptionid").ToString & "#")
                                                                                If CountMethod = "PerReport" Then
                                                                                    ReportLC = 1
                                                                                ElseIf DefaultLC = True Then
                                                                                    ReportLC = DRRec5("billingLC")
                                                                                Else
                                                                                    If CountMethod = "CharsPerLine" Then
                                                                                        If RptBody = True Then
                                                                                            '        'Response.Write(ReportLC & "#")
                                                                                            If RptSpaces = True Then
                                                                                                ReportLC += DRRec5("NumBodyCharsWSpaces")
                                                                                            Else
                                                                                                ReportLC += DRRec5("NumBodyCharsWOSpaces")
                                                                                            End If
                                                                                            '       'Response.Write(ReportLC & "#")
                                                                                            If RptSCT = True Then
                                                                                                ReportLC += DRRec5("NumBodyBTRChars")
                                                                                            End If
                                                                                            '      'Response.Write(ReportLC & "#")
                                                                                            If RptShifted = True Then
                                                                                                ReportLC += DRRec5("NumBodyCharsWShift")
                                                                                            End If
                                                                                            '     'Response.Write(ReportLC & "#")
                                                                                            If RptBIU = True Then
                                                                                                ReportLC += DRRec5("NumBodyBIU")
                                                                                            End If
                                                                                        End If

                                                                                        If Rptheader = True Then
                                                                                            ReportLC += DRRec5("HeaderCount")
                                                                                            '    'Response.Write(ReportLC & "#")
                                                                                            If RptSpaces = True Then
                                                                                                ReportLC += DRRec5("HeaderCountWSpaces")
                                                                                            End If
                                                                                            '   'Response.Write(ReportLC & "#")
                                                                                            If RptSCT = True Then
                                                                                                ReportLC += (DRRec5("HeaderBTRCount") - DRRec5("HeaderCount"))
                                                                                            End If
                                                                                            If RptShifted = True Then
                                                                                                ReportLC += DRRec5("HeaderWShift")
                                                                                            End If
                                                                                            If RptBIU = True Then
                                                                                                ReportLC += DRRec5("HeaderBIUCount")
                                                                                            End If
                                                                                            '  'Response.Write(ReportLC & "#")
                                                                                        End If

                                                                                        If RptFooter = True Then
                                                                                            ' 'Response.Write(ReportLC & "#")
                                                                                            ReportLC += DRRec5("FooterCount")
                                                                                            If RptSpaces = True Then
                                                                                                ReportLC += DRRec5("FooterCountWSpaces")
                                                                                            End If
                                                                                            If RptSCT = True Then
                                                                                                ReportLC += (DRRec5("footerBTRCount") - DRRec5("FooterCount"))
                                                                                            End If
                                                                                            If RptShifted = True Then
                                                                                                ReportLC += DRRec5("FooterWShift")
                                                                                            End If
                                                                                            If RptBIU = True Then
                                                                                                ReportLC += DRRec5("FooterBIUCount")
                                                                                            End If
                                                                                            ''Response.Write(ReportLC & "#")

                                                                                        End If
                                                                                        If CharsPerLines > 0 Then
                                                                                            ReportLC = ReportLC / CharsPerLines
                                                                                        End If
                                                                                    End If


                                                                                    If CountMethod = "Words" Then
                                                                                        If RptBody = True Then
                                                                                            ReportLC += DRRec5("NumBodywords")

                                                                                        End If

                                                                                        If Rptheader = True Then
                                                                                            ReportLC += DRRec5("Numheaderwords")
                                                                                        End If

                                                                                        If RptFooter = True Then
                                                                                            ReportLC += DRRec5("Numfooterwords")
                                                                                        End If
                                                                                    End If

                                                                                    If CountMethod = "Pages" Then
                                                                                        ReportLC += DRRec5("NumPages")

                                                                                    End If
                                                                                    If CountMethod = "GrossLines" Then
                                                                                        If RptBody = True Then
                                                                                            ReportLC += DRRec5("bodyLines")

                                                                                        End If

                                                                                        If Rptheader = True Then
                                                                                            ReportLC += DRRec5("GrossHeaderCount")
                                                                                        End If

                                                                                        If RptFooter = True Then
                                                                                            ReportLC += DRRec5("GrossFooterCount")
                                                                                        End If

                                                                                    End If

                                                                                    If CountMethod = "AllLines" Then
                                                                                        If RptBody = True Then
                                                                                            ReportLC += DRRec5("bodyLinesWBlanks")

                                                                                        End If

                                                                                        If Rptheader = True Then
                                                                                            ReportLC += DRRec5("GrossHeaderCountWBlanks")
                                                                                        End If

                                                                                        If RptFooter = True Then
                                                                                            ReportLC += DRRec5("GrossFooterCountWBlanks")
                                                                                        End If
                                                                                    End If
                                                                                End If

                                                                                Dim Row1 As New TableRow
                                                                                Dim Cell1 As New TableCell
                                                                                Dim Cell2 As New TableCell
                                                                                Dim Cell3 As New TableCell
                                                                                Dim Cell4 As New TableCell
                                                                                Dim Cell5 As New TableCell
                                                                                Dim Cell6 As New TableCell
                                                                                Dim Cell7 As New TableCell
                                                                                Dim Cell As New TableCell
                                                                                Cell.Text = i
                                                                                Cell1.Text = DRRec1("AccountName").ToString
                                                                                Cell2.Text = Trim(DRRec1("Mode").ToString)
                                                                                Cell3.Text = DRRec5("Jobnumber").ToString
                                                                                Cell4.Text = ReportLC
                                                                                Cell4.Text = ReportLC
                                                                                Cell5.Text = CountMethod
                                                                                Cell7.Text = SubActName
                                                                                Row1.Cells.Add(Cell)
                                                                                Row1.Cells.Add(Cell1)
                                                                                Row1.Cells.Add(Cell2)
                                                                                Row1.Cells.Add(Cell3)
                                                                                Row1.Cells.Add(Cell4)
                                                                                Row1.Cells.Add(Cell5)
                                                                                Row1.Cells.Add(Cell7)
                                                                                Table1.Rows.Add(Row1)
                                                                                Amount = ReportLC * Rate
                                                                                Type = DRRec5("Type").ToString

                                                                                strQuery = "Insert Into ETS.dbo.tblBillingLines (BillAccID, SubActID, TranscriptionID, stdLCCount, unit, Rate, Amount, Type, Priority, updateDate) Values ('" & BillAccID & "', '" & SubActID & "', '" & TranscriptionID & "','" & stdLCCount & "', '" & ReportLC & "', '" & Rate & "', '" & Amount & "', '" & Type & "', 'False', '" & Now & "') "
                                                                                Dim SQLCmdUp3 As New SqlCommand(strQuery, New SqlConnection(strConn))
                                                                                Try
                                                                                    SQLCmdUp3.Connection.Open()
                                                                                    SQLCmdUp3.ExecuteNonQuery()
                                                                                Finally

                                                                                    If SQLCmdUp3.Connection.State = System.Data.ConnectionState.Open Then
                                                                                        SQLCmdUp3.Connection.Close()
                                                                                        SQLCmdUp3 = Nothing
                                                                                    End If
                                                                                End Try

                                                                                If DRRec5("Priority").ToString = "True" And Not CountMethod = "PerReport" Then

                                                                                    Rate = DRRec3("StatRate")
                                                                                    Amount = ReportLC * Rate
                                                                                    strQuery = "Insert Into ETS.dbo.tblBillingLines (BillAccID, SubActID, TranscriptionID, stdLCCount, unit, Rate, Amount, Type, Priority, updateDate) Values ('" & BillAccID & "', '" & SubActID & "', '" & TranscriptionID & "','" & stdLCCount & "', '" & ReportLC & "', '" & Rate & "', '" & Amount & "', '" & Type & "', 'True', '" & Now & "') "
                                                                                    Dim SQLCmdUpST As New SqlCommand(strQuery, New SqlConnection(strConn))
                                                                                    Try
                                                                                        SQLCmdUpST.Connection.Open()
                                                                                        SQLCmdUpST.ExecuteNonQuery()
                                                                                    Finally

                                                                                        If SQLCmdUpST.Connection.State = System.Data.ConnectionState.Open Then
                                                                                            SQLCmdUpST.Connection.Close()
                                                                                            SQLCmdUpST = Nothing
                                                                                        End If
                                                                                    End Try
                                                                                End If



                                                                            End While
                                                                        End If
                                                                    Finally

                                                                        If SQLCmd5.Connection.State = System.Data.ConnectionState.Open Then
                                                                            SQLCmd5.Connection.Close()
                                                                            SQLCmd5 = Nothing
                                                                        End If
                                                                    End Try
                                                                End If
                                                            End If
                                                        Finally

                                                            If SQLCmd4.Connection.State = System.Data.ConnectionState.Open Then
                                                                SQLCmd4.Connection.Close()
                                                                SQLCmd4 = Nothing
                                                            End If
                                                        End Try
                                                    End If
                                                End If
                                            Finally
                                                If SQLCmd3.Connection.State = System.Data.ConnectionState.Open Then
                                                    SQLCmd3.Connection.Close()
                                                    SQLCmd3 = Nothing
                                                End If
                                            End Try

                                        End While
                                        GenLC = True
                                    End If
                                Finally
                                    If SQLCmdLC.Connection.State = System.Data.ConnectionState.Open Then
                                        SQLCmdLC.Connection.Close()
                                        SQLCmdLC = Nothing
                                    End If
                                End Try
                            ElseIf Trim(DRRec1("Mode").ToString) = "TW" Then
                                Dim LocCode As Integer
                                strQuery = "Select NewID() as UID"
                                Dim SQLCmd6 As New SqlCommand(strQuery, New SqlConnection(strConn))
                                SQLCmd6.Connection.Open()
                                BillAccID = SQLCmd6.ExecuteScalar.ToString
                                AccountID = DRRec1("AccountID").ToString
                                Cycle = DRRec1("Cycle").ToString
                                MinBilling = DRRec1("MinBilling").ToString
                                BillMonth = DLMonth.SelectedValue
                                BillYear = DLYear.SelectedValue
                                Mode = Trim(DRRec1("Mode").ToString)

                                'Response.Write(BillAccID)
                                strQuery = "Insert Into SEcureWeb.dbo.tblBillAccounts (BillAccID, AccountID, Cycle, MinBilling, BillMonth, BillYear, BillCycle, Mode, ModDate) Values ('" & BillAccID & "', '" & AccountID & "','" & Cycle & "', '" & MinBilling & "', '" & BillMonth & "', '" & BillYear & "', '" & DLCycle.SelectedValue & "', '" & Mode & "', '" & Now & "') "
                                Dim SQLCmdUp1 As New SqlCommand(strQuery, New SqlConnection(strConn))
                                Try
                                    SQLCmdUp1.Connection.Open()
                                    SQLCmdUp1.ExecuteNonQuery()
                                Finally
                                    If SQLCmdUp1.Connection.State = System.Data.ConnectionState.Open Then
                                        SQLCmdUp1.Connection.Close()
                                        SQLCmdUp1 = Nothing
                                    End If
                                End Try

                                strQuery = "DELETE FROM ETS.dbo.tblbillinglines WHERE BillAccID ='11111111-1111-1111-1111-111111111111' AND Transcriptionid in (Select TranscriptionID FROM SecureWeb.dbo.tblTranscriptionClientMain where AccountID = '" & DRRec1("AccountID").ToString & "'   and datemodified >= '" & sdate & "' and datemodified < '" & edate & "') "
                                Dim SQLCmdDel As New SqlCommand(strQuery, New SqlConnection(strConn))
                                Try
                                    SQLCmdDel.Connection.Open()
                                    SQLCmdDel.ExecuteNonQuery()
                                Finally
                                    If SQLCmdDel.Connection.State = System.Data.ConnectionState.Open Then
                                        SQLCmdDel.Connection.Close()
                                        SQLCmdDel = Nothing
                                    End If
                                End Try
                                strQuery = "Select * from SecureWeb.dbo.tblGrpTemplates where AccID = '" & DRRec1("AccountID").ToString & "'  "
                                Dim SQLCmdLC As New SqlCommand(strQuery, New SqlConnection(strConn))
                                Try
                                    SQLCmdLC.Connection.Open()
                                    Dim DRRecLC As SqlDataReader = SQLCmdLC.ExecuteReader()
                                    If DRRecLC.HasRows = True Then
                                        While DRRecLC.Read
                                            SubActID = DRRecLC("GrpTempID").ToString
                                            SubActName = DRRecLC("GrpTempName").ToString

                                            strQuery = "Select * from Secureweb.dbo.BillDetails where AccountID = '" & DRRec1("AccountID").ToString & "' and SubActID = '" & SubActID & "'   "
                                            Dim SQLCmd3 As New SqlCommand(strQuery, New SqlConnection(strConn))
                                            Try
                                                SQLCmd3.Connection.Open()
                                                Dim DRRec3 As SqlDataReader = SQLCmd3.ExecuteReader()
                                                If DRRec3.HasRows = True Then
                                                    UDFound = True
                                                    If DRRec3.Read Then


                                                        ''Response.Write(strQuery)
                                                        strQuery = "Insert Into SEcureWeb.dbo.tblAccBillDetails (BillAccID, AccountID, SubActID, SubActName, Rate, MiscRate, StatRate, LCMethodID,ModDate) Values ('" & BillAccID & "', '" & AccountID & "', '" & SubActID & "', '" & SubActName & "','" & DRRec3("Rate") & "', '" & DRRec3("MiscRate") & "', '" & DRRec3("StatRate") & "', '" & DRRec3("LCMethodID").ToString & "', '" & Now & "') "
                                                        Dim SQLCmdUp2 As New SqlCommand(strQuery, New SqlConnection(strConn))
                                                        Try
                                                            SQLCmdUp2.Connection.Open()
                                                            SQLCmdUp2.ExecuteNonQuery()
                                                        Finally

                                                            If SQLCmdUp2.Connection.State = System.Data.ConnectionState.Open Then
                                                                SQLCmdUp2.Connection.Close()
                                                                SQLCmdUp2 = Nothing
                                                            End If
                                                        End Try


                                                        strQuery = "Select  * from ETS.dbo.tblLCMethodology where TrackID = '" & DRRec3("LCMethodID").ToString & "' "
                                                        Dim SQLCmd4 As New SqlCommand(strQuery, New SqlConnection(strConn))
                                                        Try
                                                            SQLCmd4.Connection.Open()
                                                            Dim DRRec4 As SqlDataReader = SQLCmd4.ExecuteReader()
                                                            If DRRec4.HasRows = True Then
                                                                LCMthFound = True
                                                                If DRRec4.Read Then
                                                                    Rptheader = DRRec4("Rptheader").ToString
                                                                    RptFooter = DRRec4("RptFooter").ToString
                                                                    RptBody = DRRec4("RptBody").ToString
                                                                    RptBIU = DRRec4("RptBIU").ToString
                                                                    RptShifted = DRRec4("RptShifted").ToString
                                                                    RptSpaces = DRRec4("RptSpaces").ToString
                                                                    RptSCT = DRRec4("RptSCT").ToString
                                                                    CharsPerLines = DRRec4("CharsPerLines")
                                                                    CountMethod = DRRec4("CountMethod").ToString

                                                                    If DRRec4("isdefault").ToString.ToUpper = "TRUE" Then
                                                                        DefaultLC = True
                                                                    Else
                                                                        DefaultLC = False
                                                                    End If
                                                                    strQuery = "Select distinct T.*, M.Type,M.Location,M.Priority,M.DictatorID,M.Jobnumber from SecureWeb.dbo.tblTranscriptionClientMain M INNER JOIN SEcureWEb.dbo.tblAssignTemplates A ON A.TemplateID = M.TemplateID and A.AccID = M.AccountID Left Outer Join tblTransLC T ON T.TranscriptionID = M.TranscriptionID where T.TranscriptionID is not NULL and M.AccountID = '" & DRRec1("AccountID").ToString & "'   and M.datemodified >= '" & sdate & "' and M.datemodified < '" & edate & "' and A.GrpTempID = '" & SubActID & "' order by JobNumber"
                                                                    'strQuery = "Select distinct * from SecureWeb.dbo.tblTranscriptionClientMain M INNER JOIN SEcureWEb.dbo.AssignDic A ON A.UserID = M.dictatorID and A.AccID = M.AccountID Left Outer Join tblTransLC T ON T.TranscriptionID = M.TranscriptionID where T.TranscriptionID is not NULL and M.AccountID = '" & DRRec1("AccountID").ToString & "'   and datediff(day,M.datemodified, '7/31/2010') between 0 and  30  and A.GrpDicID = '" & SubActID & "' order by JobNumber"
                                                                    'Response.Write(strQuery)
                                                                    Dim i As Integer
                                                                    i = 0
                                                                    Dim SQLCmd5 As New SqlCommand(strQuery, New SqlConnection(strConn))
                                                                    SQLCmd5.CommandTimeout = 300
                                                                    Try
                                                                        SQLCmd5.Connection.Open()
                                                                        Dim DRRec5 As SqlDataReader = SQLCmd5.ExecuteReader()
                                                                        If DRRec5.HasRows = True Then
                                                                            RecFound = True
                                                                            While DRRec5.Read
                                                                                i = i + 1
                                                                                ReportLC = 0
                                                                                TranscriptionID = DRRec5("TranscriptionId").ToString
                                                                                stdLCCount = DRRec5("stdLC")
                                                                                Rate = DRRec3("Rate")

                                                                                If CountMethod = "CharsPerLine" Then
                                                                                    ReportLC = DRRec5("stdLC")
                                                                                End If
                                                                                If CountMethod = "PerDictator" Then
                                                                                    ReportLC = DRRec5("stdLC")
                                                                                End If

                                                                                ''Response.Write(DRRec5("transcriptionid").ToString & "#")
                                                                                If CountMethod = "PerReport" Then
                                                                                    ReportLC = 1
                                                                                ElseIf DefaultLC = True Then
                                                                                    ReportLC = DRRec5("billingLC")
                                                                                Else
                                                                                    If CountMethod = "CharsPerLine" Then
                                                                                        If RptBody = True Then
                                                                                            '        'Response.Write(ReportLC & "#")
                                                                                            If RptSpaces = True Then
                                                                                                ReportLC += DRRec5("NumBodyCharsWSpaces")
                                                                                            Else
                                                                                                ReportLC += DRRec5("NumBodyCharsWOSpaces")
                                                                                            End If
                                                                                            '       'Response.Write(ReportLC & "#")
                                                                                            If RptSCT = True Then
                                                                                                ReportLC += DRRec5("NumBodyBTRChars")
                                                                                            End If
                                                                                            '      'Response.Write(ReportLC & "#")
                                                                                            If RptShifted = True Then
                                                                                                ReportLC += DRRec5("NumBodyCharsWShift")
                                                                                            End If
                                                                                            '     'Response.Write(ReportLC & "#")
                                                                                            If RptBIU = True Then
                                                                                                ReportLC += DRRec5("NumBodyBIU")
                                                                                            End If
                                                                                        End If

                                                                                        If Rptheader = True Then
                                                                                            ReportLC += DRRec5("HeaderCount")
                                                                                            '    'Response.Write(ReportLC & "#")
                                                                                            If RptSpaces = True Then
                                                                                                ReportLC += DRRec5("HeaderCountWSpaces")
                                                                                            End If
                                                                                            '   'Response.Write(ReportLC & "#")
                                                                                            If RptSCT = True Then
                                                                                                ReportLC += (DRRec5("HeaderBTRCount") - DRRec5("HeaderCount"))
                                                                                            End If
                                                                                            If RptShifted = True Then
                                                                                                ReportLC += DRRec5("HeaderWShift")
                                                                                            End If
                                                                                            If RptBIU = True Then
                                                                                                ReportLC += DRRec5("HeaderBIUCount")
                                                                                            End If
                                                                                            '  'Response.Write(ReportLC & "#")
                                                                                        End If

                                                                                        If RptFooter = True Then
                                                                                            ' 'Response.Write(ReportLC & "#")
                                                                                            ReportLC += DRRec5("FooterCount")
                                                                                            If RptSpaces = True Then
                                                                                                ReportLC += DRRec5("FooterCountWSpaces")
                                                                                            End If
                                                                                            If RptSCT = True Then
                                                                                                ReportLC += (DRRec5("footerBTRCount") - DRRec5("FooterCount"))
                                                                                            End If
                                                                                            If RptShifted = True Then
                                                                                                ReportLC += DRRec5("FooterWShift")
                                                                                            End If
                                                                                            If RptBIU = True Then
                                                                                                ReportLC += DRRec5("FooterBIUCount")
                                                                                            End If
                                                                                            ''Response.Write(ReportLC & "#")

                                                                                        End If
                                                                                        If CharsPerLines > 0 Then
                                                                                            ReportLC = ReportLC / CharsPerLines
                                                                                        End If
                                                                                    End If


                                                                                    If CountMethod = "Words" Then
                                                                                        If RptBody = True Then
                                                                                            ReportLC += DRRec5("NumBodywords")

                                                                                        End If

                                                                                        If Rptheader = True Then
                                                                                            ReportLC += DRRec5("Numheaderwords")
                                                                                        End If

                                                                                        If RptFooter = True Then
                                                                                            ReportLC += DRRec5("Numfooterwords")
                                                                                        End If
                                                                                    End If

                                                                                    If CountMethod = "Pages" Then
                                                                                        ReportLC += DRRec5("NumPages")

                                                                                    End If
                                                                                    If CountMethod = "GrossLines" Then
                                                                                        If RptBody = True Then
                                                                                            ReportLC += DRRec5("bodyLines")

                                                                                        End If

                                                                                        If Rptheader = True Then
                                                                                            ReportLC += DRRec5("GrossHeaderCount")
                                                                                        End If

                                                                                        If RptFooter = True Then
                                                                                            ReportLC += DRRec5("GrossFooterCount")
                                                                                        End If

                                                                                    End If

                                                                                    If CountMethod = "AllLines" Then
                                                                                        If RptBody = True Then
                                                                                            ReportLC += DRRec5("bodyLinesWBlanks")

                                                                                        End If

                                                                                        If Rptheader = True Then
                                                                                            ReportLC += DRRec5("GrossHeaderCountWBlanks")
                                                                                        End If

                                                                                        If RptFooter = True Then
                                                                                            ReportLC += DRRec5("GrossFooterCountWBlanks")
                                                                                        End If
                                                                                    End If
                                                                                End If

                                                                                Dim Row1 As New TableRow
                                                                                Dim Cell1 As New TableCell
                                                                                Dim Cell2 As New TableCell
                                                                                Dim Cell3 As New TableCell
                                                                                Dim Cell4 As New TableCell
                                                                                Dim Cell5 As New TableCell
                                                                                Dim Cell6 As New TableCell
                                                                                Dim Cell7 As New TableCell
                                                                                Dim Cell As New TableCell
                                                                                Cell.Text = i
                                                                                Cell1.Text = DRRec1("AccountName").ToString
                                                                                Cell2.Text = Trim(DRRec1("Mode").ToString)
                                                                                Cell3.Text = DRRec5("Jobnumber").ToString
                                                                                Cell4.Text = ReportLC
                                                                                Cell4.Text = ReportLC
                                                                                Cell5.Text = CountMethod
                                                                                Cell7.Text = SubActName
                                                                                Row1.Cells.Add(Cell)
                                                                                Row1.Cells.Add(Cell1)
                                                                                Row1.Cells.Add(Cell2)
                                                                                Row1.Cells.Add(Cell3)
                                                                                Row1.Cells.Add(Cell4)
                                                                                Row1.Cells.Add(Cell5)
                                                                                Row1.Cells.Add(Cell7)
                                                                                Table1.Rows.Add(Row1)
                                                                                Amount = ReportLC * Rate
                                                                                Type = DRRec5("Type").ToString

                                                                                strQuery = "Insert Into ETS.dbo.tblBillingLines (BillAccID, SubActID, TranscriptionID, stdLCCount, unit, Rate, Amount, Type, Priority, updateDate) Values ('" & BillAccID & "', '" & SubActID & "', '" & TranscriptionID & "','" & stdLCCount & "', '" & ReportLC & "', '" & Rate & "', '" & Amount & "', '" & Type & "', 'False', '" & Now & "') "
                                                                                Dim SQLCmdUp3 As New SqlCommand(strQuery, New SqlConnection(strConn))
                                                                                Try
                                                                                    SQLCmdUp3.Connection.Open()
                                                                                    SQLCmdUp3.ExecuteNonQuery()
                                                                                Finally

                                                                                    If SQLCmdUp3.Connection.State = System.Data.ConnectionState.Open Then
                                                                                        SQLCmdUp3.Connection.Close()
                                                                                        SQLCmdUp3 = Nothing
                                                                                    End If
                                                                                End Try

                                                                                If DRRec5("Priority").ToString = "True" And Not CountMethod = "PerReport" Then

                                                                                    Rate = DRRec3("StatRate")
                                                                                    Amount = ReportLC * Rate
                                                                                    strQuery = "Insert Into ETS.dbo.tblBillingLines (BillAccID, SubActID, TranscriptionID, stdLCCount, unit, Rate, Amount, Type, Priority, updateDate) Values ('" & BillAccID & "', '" & SubActID & "', '" & TranscriptionID & "','" & stdLCCount & "', '" & ReportLC & "', '" & Rate & "', '" & Amount & "', '" & Type & "', 'True', '" & Now & "') "
                                                                                    Dim SQLCmdUpST As New SqlCommand(strQuery, New SqlConnection(strConn))
                                                                                    Try
                                                                                        SQLCmdUpST.Connection.Open()
                                                                                        SQLCmdUpST.ExecuteNonQuery()
                                                                                    Finally

                                                                                        If SQLCmdUpST.Connection.State = System.Data.ConnectionState.Open Then
                                                                                            SQLCmdUpST.Connection.Close()
                                                                                            SQLCmdUpST = Nothing
                                                                                        End If
                                                                                    End Try
                                                                                End If



                                                                            End While
                                                                        End If
                                                                    Finally

                                                                        If SQLCmd5.Connection.State = System.Data.ConnectionState.Open Then
                                                                            SQLCmd5.Connection.Close()
                                                                            SQLCmd5 = Nothing
                                                                        End If
                                                                    End Try
                                                                End If
                                                            End If
                                                        Finally

                                                            If SQLCmd4.Connection.State = System.Data.ConnectionState.Open Then
                                                                SQLCmd4.Connection.Close()
                                                                SQLCmd4 = Nothing
                                                            End If
                                                        End Try
                                                    End If
                                                End If
                                            Finally
                                                If SQLCmd3.Connection.State = System.Data.ConnectionState.Open Then
                                                    SQLCmd3.Connection.Close()
                                                    SQLCmd3 = Nothing
                                                End If
                                            End Try

                                        End While
                                        GenLC = True
                                    End If
                                Finally
                                    If SQLCmdLC.Connection.State = System.Data.ConnectionState.Open Then
                                        SQLCmdLC.Connection.Close()
                                        SQLCmdLC = Nothing
                                    End If
                                End Try
                            End If
                        End If
                        Dim Cll1 As New TableCell
                        Dim Cll2 As New TableCell
                        Dim Cll3 As New TableCell
                        Dim TRow As New TableRow
                        j = j + 1
                        Cll1.Text = j
                        Cll2.Text = DRRec1("AccountName").ToString
                        If GenLC = True Then
                            Cll3.Text = "Successful"
                        ElseIf Regenerate = True Then
                            Cll3.Text = "<input type=button value='Re-Generate LineCount' onclick=RemDetails('" & BillAccID & "','" & strCycle & "','" & BillMonth & "','" & BillYear & "'); class='button'>"
                        ElseIf UDFound = True Then
                            Cll3.Text = "Unit rates are not assigned"
                        ElseIf LCMthFound = False Then
                            Cll3.Text = "Linecount method is not assigned"
                        Else
                            Cll3.Text = "No records found"
                        End If

                        TRow.Cells.Add(Cll1)
                        TRow.Cells.Add(Cll2)
                        TRow.Cells.Add(Cll3)
                        Table2.Rows.Add(TRow)
                    Finally
                        If SQLCmd2.Connection.State = System.Data.ConnectionState.Open Then
                            SQLCmd2.Connection.Close()
                            SQLCmd2 = Nothing
                        End If
                    End Try



                End While
            End If
        Finally
            If SQLCmd1.Connection.State = System.Data.ConnectionState.Open Then
                SQLCmd1.Connection.Close()
                SQLCmd1 = Nothing
            End If
        End Try

    End Sub


End Class




