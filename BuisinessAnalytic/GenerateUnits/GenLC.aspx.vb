Imports System.Data.SqlClient
Imports System.Data
Partial Class GenLC_New
    Inherits BasePage

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        
        Label1.Visible = False
        If Not IsPostBack Then

            Dim script As String = "$(document).ready(function () { $('[id*=btnSubmit]').click(); });"
            ClientScript.RegisterStartupScript(Me.GetType, "load", script, True)
            Dim strConn As String
            strConn = System.Configuration.ConfigurationManager.AppSettings("ETSCon")
            Dim SQLCmd1 As New SqlCommand("Select * from AdminETS.dbo.tblaccounts where (Isdeleted Is NULL OR Isdeleted = 0) and contractorid ='" & Session("contractorid").ToString & "' order by Accountname", New SqlConnection(strConn))
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
                    SQLCmd1.Connection.Dispose()
                    SQLCmd1 = Nothing
                End If
            End Try
            GetMyMonthList(DLMonth, True)
            GetMyYearList(DLYear, True)
            ' Table1.Visible = False
            Table2.Visible = False
        End If


    End Sub

    Protected Sub BtnSubmit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSubmit.Click
        Dim strConn As String
        strConn = System.Configuration.ConfigurationManager.AppSettings("ETSCon")
        Dim strConn1 As String
        strConn1 = System.Configuration.ConfigurationManager.AppSettings("ETSConOne")
        Dim AccountID As String = String.Empty
        Dim C1CurrSDate As Date
        Dim C1CurrEDate As Date
        Dim C2CurrSDate As Date
        Dim C2CurrEDate As Date
        Dim DefaultLC As Boolean = False
        C1CurrSDate = DLMonth.SelectedValue & "/1/" & DLYear.SelectedValue
        C1CurrEDate = DLMonth.SelectedValue & "/15/" & DLYear.SelectedValue
        C2CurrSDate = DLMonth.SelectedValue & "/16/" & DLYear.SelectedValue
        C2CurrEDate = DateAdd(DateInterval.Month, 1, C1CurrSDate)
        C2CurrEDate = C2CurrEDate.AddDays(-1)



        Dim strCycle As Boolean
        Dim strQuery As String
        Dim ReportLC As Double
        ' Dim ReportLCWithDec As Decimal
        Dim StdBillingLC As Integer
        Dim Rptheader As Boolean
        Dim RptFooter As Boolean
        Dim RptBody As Boolean
        Dim RptBIU As Boolean
        Dim RptShifted As Boolean
        Dim RptSpaces As Boolean
        Dim RptSCT As Boolean
        Dim RptBIUOnOff As Boolean
        Dim RptShiftedAll As Boolean
        Dim RptDocVariable As Boolean
        Dim CharsPerLines As Integer
        Dim BIUVal As Integer
        Dim BIUShiftedAll As Integer
        Dim CountMethod As String
        Dim BillAccID As String
        'Dim AccountID As String
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
        Dim stdTempCharDed As Integer
        Dim Rate As Double
        Dim Amount As Double
        Dim Type As String
        Dim SubActName As String
        Dim SubActID As String
        Dim sdate As Date
        Dim edate As Date
        Dim j As Integer = 0
        Dim k As Integer = 0
        Dim m As Integer = 0
        Dim n As Integer = 0
        Dim UDFound As Boolean
        Dim LCMthFound As Boolean
        Dim ErrMessage As String = String.Empty
        Dim ErrFound As Boolean = False
        Dim StatChgPerReport As Boolean = False
        Dim AccountName As String = String.Empty
        CountMethod = "CharsPerLine"
        Rptheader = False
        RptFooter = False
        RptBody = False
        RptBIU = False
        RptShifted = False
        RptSpaces = False
        RptBIUOnOff = False
        RptShiftedAll = False
        RptDocVariable = False
        RptSCT = False
        CharsPerLines = 0
        BIUVal = 0
        BIUShiftedAll = 0
        ReportLC = 0
        StdBillingLC = 0
        If DLCycle.SelectedValue = "1" Then
            strCycle = True
            If DLAct.SelectedValue = "All" Then
                strQuery = "Select A.*, IsNull(A.StatChgPerReport,0) As StatChgPerReport,0 As IsAccFaxBilling, 0 AS AccFaxCharges, 0 As IsAccVPNBilling, 0 AS AccVPNCharges, 0 As IsAccCutPasteBilling, 0 AS AccCutPasteBilling , 0 AS FaxPageCount, 0 AS FaxRecCount  from ETS.dbo.tblaccounts A where (Isdeleted Is NULL OR Isdeleted = 0) and  contractorid ='" & Session("contractorid").ToString & "'  and Cycle='" & strCycle & "' order by Accountname"
            Else
                strQuery = "Select A.*, IsNull(A.StatChgPerReport,0) As StatChgPerReport,0 As IsAccFaxBilling, 0 AS AccFaxCharges, 0 As IsAccVPNBilling, 0 AS AccVPNCharges, 0 As IsAccCutPasteBilling, 0 AS AccCutPasteBilling , 0 AS FaxPageCount, 0 AS FaxRecCount  from ETS.dbo.tblaccounts A where (Isdeleted Is NULL OR Isdeleted = 0) and contractorid ='" & Session("contractorid").ToString & "' and  Cycle='" & strCycle & "' and accountid='" & DLAct.SelectedValue & "'  order by Accountname"
            End If

        Else
            strCycle = False
            If DLAct.SelectedValue = "All" Then
                strQuery = "Select A.*, IsNull(A.StatChgPerReport,0) As StatChgPerReport, Isnull(A.IsFaxBilling,0) As IsAccFaxBilling, ISNULL(A.FaxCharges,0) AS AccFaxCharges, Isnull(A.IsVPNBilling,0) As IsAccVPNBilling, ISNULL(A.VPNCharges,0) AS AccVPNCharges, Isnull(A.IsCutPasteBilling,0) As IsAccCutPasteBilling, ISNULL(A.CutPasteBilling,0) AS AccCutPasteBilling , ISNULL(F.FaxPageCount, 0) AS FaxPageCount, ISNULL(F.FaxRecCount, 0) AS FaxRecCount from ETS.dbo.tblaccounts A  LEFT OUTER JOIN (select AccountID, SUM(r.documentpagecount) AS FaxPageCount , Count(r.documentpagecount) AS FaxRecCount from tbloutbound O INNER JOIN  tblJobDeliveryResult R ON R.JobID =O.JOBID and R.RecordID = O.RecordID INNER JOIN secureweb.dbo.tbltranscriptionclient C ON C.TranscriptionID = O.JOBID WHERE (O.StatusID =1 or O.StatusID =5) and O.lastattempttime > '" & C1CurrSDate & "' and o.lastattempttime < '" & C2CurrEDate.AddDays(1) & "' group by AccountID ) F ON F.AccountID=A.AccountID where A.contractorid ='" & Session("contractorid").ToString & "' and (A.Isdeleted Is NULL OR A.Isdeleted = 0)    order by A.Accountname"
            Else
                strQuery = "Select A.*, IsNull(A.StatChgPerReport,0) As StatChgPerReport,Isnull(A.IsFaxBilling,0) As IsAccFaxBilling, ISNULL(A.FaxCharges,0) AS AccFaxCharges, Isnull(A.IsVPNBilling,0) As IsAccVPNBilling, ISNULL(A.VPNCharges,0) AS AccVPNCharges, Isnull(A.IsCutPasteBilling,0) As IsAccCutPasteBilling, ISNULL(A.CutPasteBilling,0) AS AccCutPasteBilling , ISNULL(F.FaxPageCount, 0) AS FaxPageCount, ISNULL(F.FaxRecCount, 0) AS FaxRecCount from ETS.dbo.tblaccounts A  LEFT OUTER JOIN (select AccountID, SUM(r.documentpagecount) AS FaxPageCount, Count(r.documentpagecount) AS FaxRecCount from tbloutbound O INNER JOIN  tblJobDeliveryResult R ON R.JobID =O.JOBID and R.RecordID = O.RecordID INNER JOIN secureweb.dbo.tbltranscriptionclient C ON C.TranscriptionID = O.JOBID WHERE (O.StatusID =1 or O.StatusID =5) and O.lastattempttime > '" & C1CurrSDate & "' and o.lastattempttime < '" & C2CurrEDate.AddDays(1) & "' group by AccountID ) F ON F.AccountID=A.AccountID where A.contractorid ='" & Session("contractorid").ToString & "'  and A.accountid='" & DLAct.SelectedValue & "' and (A.Isdeleted Is NULL OR A.Isdeleted = 0)  "
            End If

        End If
        Dim SQLCmd1 As New SqlCommand(strQuery, New SqlConnection(strConn1))
        Try
            SQLCmd1.Connection.Open()
            Dim DRRec1 As SqlDataReader = SQLCmd1.ExecuteReader()
            If DRRec1.HasRows = True Then
                'Table1.Visible = True
                Table2.Visible = True
                While DRRec1.Read
                    BillAccID = String.Empty
                    BillMonth = DLMonth.SelectedValue
                    BillYear = DLYear.SelectedValue
                    Regenerate = False
                    RecFound = False
                    GenLC = False
                    UDFound = False
                    LCMthFound = False
                    DefaultLC = False
                    ErrFound = False
                    AccountName = DRRec1("AccountName").ToString
                    AccountID = DRRec1("AccountID").ToString
                    StatChgPerReport = DRRec1("StatChgPerReport").ToString
                    strQuery = "Select * from AdminSecureweb.dbo.tblbillaccounts where AccountID = '" & DRRec1("AccountID").ToString & "' and Billmonth='" & DLMonth.SelectedValue & "' and BillYear = '" & DLYear.SelectedValue & "' and BillCycle='" & DLCycle.SelectedValue & "' "

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
                            ' Response.Write(DRRec1("Mode").ToString)
                            If Trim(DRRec1("Mode").ToString) = "S" Or Trim(DRRec1("Mode").ToString) = "" Then
                                SqlConnection.ClearAllPools()
                                Dim myConnection1 As New SqlConnection(strConn)
                                Dim MyTransAttr1 As SqlTransaction
                                Dim strQuery1 As String
                                Dim cmdUp1 As New SqlCommand()
                                myConnection1.Open()
                                MyTransAttr1 = myConnection1.BeginTransaction()
                                cmdUp1.Connection = myConnection1
                                cmdUp1.Transaction = MyTransAttr1
                                cmdUp1.CommandTimeout = 600
                                Try

                                    strQuery = "Select * from AdminSecureweb.dbo.BillDetails where AccountID = '" & DRRec1("AccountID").ToString & "'  "
                                    '    'Response.Write(strQuery)
                                    Dim SQLCmd3 As New SqlCommand(strQuery, New SqlConnection(strConn))
                                    Try
                                        SQLCmd3.Connection.Open()
                                        Dim DRRec3 As SqlDataReader = SQLCmd3.ExecuteReader()
                                        If DRRec3.HasRows = True Then
                                            UDFound = True
                                            If DRRec3.Read Then
                                                strQuery = "DELETE FROM AdminETS.dbo.tblbillinglines WHERE BillAccID ='11111111-1111-1111-1111-111111111111' AND Transcriptionid in (Select TranscriptionID FROM AdminSecureweb.dbo.tblTranscriptionClientMain where AccountID = '" & DRRec1("AccountID").ToString & "'  and datediff(day,datemodified, '" & edate & "') between 0 and datediff(day,'" & sdate & "','" & edate & "')  ) "
                                                'Dim SQLCmdDel As New SqlCommand(strQuery, New SqlConnection(strConn))
                                                'Try
                                                '    SQLCmdDel.Connection.Open()
                                                '    SQLCmdDel.ExecuteNonQuery()
                                                'Finally
                                                '    If SQLCmdDel.Connection.State = System.Data.ConnectionState.Open Then
                                                '        SQLCmdDel.Connection.Close()
                                                '        SQLCmdDel = Nothing
                                                '    End If
                                                'End Try
                                                cmdUp1.CommandText = strQuery
                                                cmdUp1.CommandType = CommandType.Text
                                                cmdUp1.ExecuteNonQuery()
                                                strQuery = "Select NewID() as UID"
                                                Dim SQLCmd6 As New SqlCommand(strQuery, New SqlConnection(strConn))
                                                Try
                                                    SQLCmd6.Connection.Open()
                                                    BillAccID = SQLCmd6.ExecuteScalar.ToString

                                                Finally
                                                    If SQLCmd6.Connection.State = ConnectionState.Open Then
                                                        SQLCmd6.Connection.Close()
                                                        SQLCmd6.Connection.Dispose()
                                                    End If
                                                End Try

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
                                                strQuery = "Insert Into AdminSecureweb.dbo.tblBillAccounts (BillAccID, AccountID, Cycle, MinBilling, BillMonth, BillYear, BillCycle, Mode, ModDate,WTMode) Values ('" & BillAccID & "', '" & AccountID & "','" & Cycle & "', '" & MinBilling & "', '" & BillMonth & "', '" & BillYear & "', '" & DLCycle.SelectedValue & "', '" & Mode & "', '" & Now & "','" & WTMode & "') "
                                                cmdUp1.CommandText = strQuery
                                                cmdUp1.CommandType = CommandType.Text
                                                cmdUp1.ExecuteNonQuery()


                                                ''Response.Write(strQuery)
                                                strQuery = "Insert Into AdminSecureweb.dbo.tblAccBillDetails (BillAccID, AccountID, Rate, MiscRate, StatRate, LCMethodID,ModDate) Values ('" & BillAccID & "', '" & AccountID & "','" & DRRec3("Rate") & "', '" & DRRec3("MiscRate") & "', '" & DRRec3("StatRate") & "'"
                                                'Response.Write(strQuery)
                                                'Response.End()

                                                If DRRec3("LCMethodID").ToString = "" Then
                                                    strQuery = strQuery & ", NULL, '" & Now & "') "
                                                Else
                                                    strQuery = strQuery & ", '" & DRRec3("LCMethodID").ToString & "', '" & Now & "') "
                                                End If


                                                cmdUp1.CommandText = strQuery
                                                cmdUp1.CommandType = CommandType.Text
                                                cmdUp1.ExecuteNonQuery()


                                                If DRRec3("LCMethodID").ToString <> "" Then
                                                    strQuery = "Select  * from AdminETS.dbo.tblLCMethodology where TrackID = '" & DRRec3("LCMethodID").ToString & "' "
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
                                                                RptBIUOnOff = DRRec4("RptBIUOnOff").ToString
                                                                RptShiftedAll = DRRec4("RptShiftedAll").ToString
                                                                RptDocVariable = DRRec4("RptDocVariable").ToString
                                                                RptShifted = DRRec4("RptShifted").ToString
                                                                RptSpaces = DRRec4("RptSpaces").ToString
                                                                RptSCT = DRRec4("RptSCT").ToString
                                                                CharsPerLines = DRRec4("CharsPerLines")
                                                                BIUVal = DRRec4("BIUVal")
                                                                BIUShiftedAll = DRRec4("BIUShiftedAll")
                                                                CountMethod = DRRec4("CountMethod").ToString
                                                                'Dim conn2 As New SqlConnection(strConn)
                                                                'conn2.Open()
                                                                strQuery = "Select distinct IsNull(TB.Value, 0) AS TemplateDeduction, T.*, TL.*,M.Type,M.Location,CASE WHEN M.Priority = 1 AND (DATEDIFF(MINUTE, M.DateCreated, ISNULL(M.DateModified, GETDATE()))/60) < M.TAT THEN 'True' ELSE 'False' END as Priority,M.DictatorID,M.Jobnumber,   ROUND(CAST(datediff(s,0,M.duration) AS Float)*1.0/60,2) as Mins from AdminSecureweb.dbo.tblTranscriptionClientMain M Left Outer Join tblTransLC T ON T.TranscriptionID = M.TranscriptionID Left Outer Join tblTemplates TL ON TL.TemplateID = M.TemplateID LEFT OUTER JOIN tbltemplatesLinesDeductionsForBilling TB ON M.TemplateID = TB.TemplateID  where T.TranscriptionID is not NULL and M.AccountID = '" & DRRec1("AccountID").ToString & "'   and datediff(day,M.datemodified, '" & edate & "') between 0 and datediff(day,'" & sdate & "','" & edate & "')    order by JobNumber"
                                                                'strQuery = "Select distinct * from AdminSecureweb.dbo.tblTranscriptionClientMain M Left Outer Join tblTransLC T ON T.TranscriptionID = M.TranscriptionID Left Outer Join tblTemplates TL ON TL.TemplateID = M.TemplateID where T.TranscriptionID is not NULL and M.AccountID = '" & DRRec1("AccountID").ToString & "'   and datediff(day,M.datemodified, '7/31/2010') between 0 and  30  order by JobNumber"
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
                                                                            ' Response.Write(TranscriptionID & "#")
                                                                            stdLCCount = DRRec5("stdLC")
                                                                            stdTempCharDed = DRRec5("TemplateDeduction")
                                                                            ''Response.Write(DRRec5("transcriptionid").ToString & "#")
                                                                            'Response.Write(CountMethod)
                                                                            'Response.Write(DRRec5("Mins"))
                                                                            If CountMethod = "PerDictator" Then
                                                                                ReportLC = DRRec5("stdLC")
                                                                            End If
                                                                            If CountMethod = "PerReport" Then
                                                                                ReportLC = 1
                                                                            ElseIf CountMethod = "Minutes" Then
                                                                                If String.IsNullOrEmpty(DRRec5("Mins")) Then
                                                                                    ReportLC = 0
                                                                                Else
                                                                                    ReportLC = DRRec5("Mins")
                                                                                End If

                                                                            ElseIf DefaultLC = True Then
                                                                                ReportLC = DRRec5("billingLC")
                                                                            Else

                                                                                If CountMethod = "CharsPerLine" Then
                                                                                    If RptBody = True Then
                                                                                        'Response.Write("#LC1> " & ReportLC)
                                                                                        If RptSpaces = True Then
                                                                                            ReportLC += DRRec5("NumBodyCharsWSpaces")
                                                                                        Else
                                                                                            ReportLC += DRRec5("NumBodyCharsWOSpaces")
                                                                                        End If
                                                                                        ' Response.Write("#LC2> " & ReportLC)
                                                                                        '       'Response.Write(ReportLC & "#")
                                                                                        If RptSCT = True Then
                                                                                            ReportLC += DRRec5("NumBodyBTRChars")
                                                                                        End If
                                                                                        '  Response.Write("#LC3> " & ReportLC)
                                                                                        '      'Response.Write(ReportLC & "#")
                                                                                        If RptShifted = True Then
                                                                                            ReportLC += DRRec5("NumBodyCharsWShift")
                                                                                        End If
                                                                                        ' Response.Write("#LC4> " & ReportLC)
                                                                                        '     'Response.Write(ReportLC & "#")
                                                                                        If RptBIU = True Then
                                                                                            ReportLC += DRRec5("NumBodyBIU")
                                                                                        End If
                                                                                        ' Response.Write("#LC4> " & ReportLC)
                                                                                        If RptBIUOnOff = True Then
                                                                                            ReportLC += DRRec5("NumBodyBIUOnOff") * BIUVal
                                                                                        End If
                                                                                        ' Response.Write("#LC5> " & ReportLC)
                                                                                        If RptShiftedAll = True Then
                                                                                            ReportLC += DRRec5("NumBodyCharsWShiftAll") * BIUShiftedAll
                                                                                        End If
                                                                                        ' Response.Write("#LC6> " & ReportLC)

                                                                                    End If

                                                                                    If Rptheader = True Then
                                                                                        ' Response.Write("#LC7> " & ReportLC)
                                                                                        ReportLC += DRRec5("HeaderCount")
                                                                                        '    'Response.Write(ReportLC & "#")
                                                                                        'Response.Write("#LC8> " & ReportLC)
                                                                                        If RptSpaces = True Then
                                                                                            ReportLC += DRRec5("HeaderCountWSpaces")
                                                                                        End If
                                                                                        'Response.Write("#LC9> " & ReportLC)
                                                                                        '   'Response.Write(ReportLC & "#")
                                                                                        If RptSCT = True Then
                                                                                            ReportLC += (DRRec5("HeaderBTRCount") - DRRec5("HeaderCount"))
                                                                                        End If
                                                                                        'Response.Write("#LC10> " & ReportLC)
                                                                                        If RptShifted = True Then
                                                                                            ReportLC += DRRec5("HeaderWShift")
                                                                                        End If
                                                                                        'Response.Write("#LC11> " & ReportLC)
                                                                                        If RptBIU = True Then
                                                                                            ReportLC += DRRec5("HeaderBIUCount")
                                                                                        End If
                                                                                        ' Response.Write("#LC12> " & ReportLC)
                                                                                        If RptShiftedAll = True Then
                                                                                            ReportLC += DRRec5("HeaderWShiftAll") * BIUShiftedAll
                                                                                        End If
                                                                                        'Response.Write("#LC13> " & ReportLC)
                                                                                        If RptBIUOnOff = True Then
                                                                                            ReportLC += DRRec5("HeaderBIUCountOnOff") * BIUVal
                                                                                        End If
                                                                                        'Response.Write("#LC14> " & ReportLC)
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
                                                                                        If RptBIUOnOff = True Then
                                                                                            ReportLC += DRRec5("FooterBIUCountOnOff") * BIUVal
                                                                                        End If
                                                                                        If RptShiftedAll = True Then
                                                                                            ReportLC += DRRec5("FooterWShiftAll") * BIUShiftedAll
                                                                                        End If
                                                                                        ''Response.Write(ReportLC & "#")

                                                                                    End If
                                                                                    If RptDocVariable = True Then
                                                                                        ReportLC += DRRec5("DocVarCount")
                                                                                    End If
                                                                                    'Response.Write("$" & ReportLC & "$")
                                                                                    ReportLC = ReportLC - stdTempCharDed
                                                                                    'Response.Write("%" & ReportLC & "%")
                                                                                    If ReportLC < 0 Then
                                                                                        ReportLC = 0
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

                                                                            'Dim Row1 As New TableRow
                                                                            'Dim Cell1 As New TableCell
                                                                            'Dim Cell2 As New TableCell
                                                                            'Dim Cell3 As New TableCell
                                                                            'Dim Cell4 As New TableCell
                                                                            'Dim Cell5 As New TableCell
                                                                            'Dim Cell6 As New TableCell
                                                                            'Dim Cell As New TableCell
                                                                            'Cell.Text = i
                                                                            'Cell1.Text = DRRec1("AccountName").ToString
                                                                            'Cell2.Text = Trim(DRRec1("Mode").ToString)
                                                                            'Cell3.Text = DRRec5("Jobnumber").ToString
                                                                            'Cell4.Text = ReportLC
                                                                            'Cell4.Text = ReportLC
                                                                            'Cell5.Text = CountMethod
                                                                            'Row1.Cells.Add(Cell)
                                                                            'Row1.Cells.Add(Cell1)
                                                                            'Row1.Cells.Add(Cell2)
                                                                            'Row1.Cells.Add(Cell3)
                                                                            'Row1.Cells.Add(Cell4)
                                                                            'Row1.Cells.Add(Cell5)
                                                                            'Table1.Rows.Add(Row1)
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

                                                                            strQuery = "Insert Into AdminETS.dbo.tblBillingLines (BillAccID, TranscriptionID, stdLCCount, unit, Rate, Amount, Type, Priority, updateDate,WorkType) Values ('" & BillAccID & "', '" & TranscriptionID & "','" & stdLCCount & "', '" & ReportLC & "', '" & Rate & "', '" & Amount & "', '" & Type & "','False', '" & Now & "', '" & strWorkType & "') "
                                                                            '  Response.Write(strQuery)
                                                                            cmdUp1.CommandText = strQuery
                                                                            cmdUp1.CommandType = CommandType.Text
                                                                            cmdUp1.ExecuteNonQuery()

                                                                            If DRRec5("Priority").ToString = "True" And Not CountMethod = "PerReport" Then
                                                                                If StatChgPerReport Then
                                                                                    Rate = DRRec3("StatRate")
                                                                                    Amount = Rate
                                                                                    strQuery = "Insert Into AdminETS.dbo.tblBillingLines (BillAccID, TranscriptionID, stdLCCount, unit, Rate, Amount, Type, Priority, updateDate,WorkType) Values ('" & BillAccID & "', '" & TranscriptionID & "','" & stdLCCount & "', '" & 1 & "', '" & Rate & "', '" & Amount & "', '" & Type & "', 'True',  '" & Now & "', '" & strWorkType & "') "
                                                                                Else
                                                                                    Rate = DRRec3("StatRate")
                                                                                    Amount = ReportLC * Rate
                                                                                    strQuery = "Insert Into AdminETS.dbo.tblBillingLines (BillAccID, TranscriptionID, stdLCCount, unit, Rate, Amount, Type, Priority, updateDate,WorkType) Values ('" & BillAccID & "', '" & TranscriptionID & "','" & stdLCCount & "', '" & ReportLC & "', '" & Rate & "', '" & Amount & "', '" & Type & "', 'True',  '" & Now & "', '" & strWorkType & "') "
                                                                                End If
                                                                               
                                                                                cmdUp1.CommandText = strQuery
                                                                                cmdUp1.CommandType = CommandType.Text
                                                                                cmdUp1.ExecuteNonQuery()
                                                                            End If

                                                                        End While
                                                                    End If
                                                                    'If conn2.State = System.Data.ConnectionState.Open Then
                                                                    '    conn2.Close()
                                                                    '    conn2 = Nothing
                                                                    'End If
                                                                Catch ex As Exception
                                                                    Response.Write(ex.Message)
                                                                Finally
                                                                    If SQLCmd5.Connection.State = System.Data.ConnectionState.Open Then
                                                                        SQLCmd5.Connection.Close()
                                                                        SQLCmd5.Connection.Dispose()
                                                                        SQLCmd5 = Nothing
                                                                    End If
                                                                End Try

                                                            End If
                                                        End If
                                                    Catch ex As Exception
                                                        Response.Write(ex.Message)
                                                    Finally
                                                        If SQLCmd4.Connection.State = System.Data.ConnectionState.Open Then
                                                            SQLCmd4.Connection.Close()
                                                            SQLCmd4.Connection.Dispose()
                                                            SQLCmd4 = Nothing
                                                        End If
                                                    End Try

                                                End If

                                            End If
                                            GenLC = True
                                        End If
                                    Catch ex As Exception
                                        Response.Write(ex.Message)
                                    Finally
                                        If SQLCmd3.Connection.State = System.Data.ConnectionState.Open Then
                                            SQLCmd3.Connection.Close()
                                            SQLCmd3.Connection.Dispose()
                                            SQLCmd3 = Nothing
                                        End If
                                    End Try
                                    If DLCycle.SelectedValue = 1 Then
                                        strQuery = "Delete from  AdminSecureweb.dbo.tblinvItemdet WHERE AccountID = '" & AccountID & "' and InvoiceID ='11111111-1111-1111-1111-111111111111'  and ISAuto=1 and servicedate='" & C1CurrEDate.AddDays(-1) & "' "
                                        cmdUp1.CommandText = strQuery
                                        cmdUp1.CommandType = CommandType.Text
                                        cmdUp1.ExecuteNonQuery()
                                    Else
                                        strQuery = "Delete from  AdminSecureweb.dbo.tblinvItemdet WHERE AccountID = '" & AccountID & "' and InvoiceID ='11111111-1111-1111-1111-111111111111'  and ISAuto=1 and servicedate='" & C2CurrEDate & "' "
                                        cmdUp1.CommandText = strQuery
                                        cmdUp1.CommandType = CommandType.Text
                                        cmdUp1.ExecuteNonQuery()
                                    End If
                                    If DLCycle.SelectedValue = 2 And DRRec1("IsAccFaxBilling").ToString And DRRec1("FaxPageCount") > 0 Then
                                        If Not DRRec1("AccFaxCharges") > 0 Then
                                            ErrFound = True
                                            ErrMessage = "Fax charges not set."
                                        End If
                                        If ErrFound = False Then
                                            strQuery = "Insert Into AdminSecureweb.dbo.tblinvItemdet (AccountID,InvoiceID, itemid, Descr, quantity, Amount,totamount,mode,servicedate, dateupdate, IsAuto) Values ('" & AccountID & "', '11111111-1111-1111-1111-111111111111', '548B3D50-3F0E-4A61-BFF9-115569721C55', 'Number of pages Faxed', '" & DRRec1("FaxPageCount") & "', convert(money," & DRRec1("AccFaxCharges") & "),  convert(money," & DRRec1("FaxPageCount") * DRRec1("AccFaxCharges") & "), 'VAS', '" & C2CurrEDate & "', '" & Now & "',1) "
                                            cmdUp1.CommandText = strQuery
                                            cmdUp1.CommandType = CommandType.Text
                                            cmdUp1.ExecuteNonQuery()
                                        End If

                                    End If

                                    If DLCycle.SelectedValue = 2 And DRRec1("IsAccVPNBilling").ToString Then

                                        If Not DRRec1("AccVPNCharges") > 0 Then
                                            ErrFound = True
                                            ErrMessage = "VPN charges not set."
                                        End If
                                        If ErrFound = False Then
                                            strQuery = "Insert Into AdminSecureweb.dbo.tblinvItemdet (AccountID,InvoiceID, itemid, Descr, quantity, Amount,totamount,mode,servicedate, dateupdate, IsAuto) Values ('" & AccountID & "', '11111111-1111-1111-1111-111111111111', '781ED926-346B-48A2-80C0-9914D8ECC0BC', 'VPN Charges', '1', convert(money," & DRRec1("AccVPNCharges") & "),  convert(money," & DRRec1("AccVPNCharges") & "), 'VAS', '" & C2CurrEDate & "', '" & Now & "',1) "
                                            cmdUp1.CommandText = strQuery
                                            cmdUp1.CommandType = CommandType.Text
                                            cmdUp1.ExecuteNonQuery()
                                        End If

                                    End If

                                    If DLCycle.SelectedValue = 2 And DRRec1("IsAccCutPasteBilling").ToString Then

                                        If Not DRRec1("AccCutPasteBilling") > 0 Then
                                            ErrFound = True
                                            ErrMessage = "Cut/Paste charges not set."
                                        End If
                                        If ErrFound = False Then
                                            strQuery = "Insert Into AdminSecureweb.dbo.tblinvItemdet (AccountID,InvoiceID, itemid, Descr, quantity, Amount,totamount,mode,servicedate, dateupdate, IsAuto) Values ('" & AccountID & "', '11111111-1111-1111-1111-111111111111', '16d2956b-e2c1-47b8-aff0-ad42b8d8cde8', 'Additional Cut and Paste charges', '1', convert(money," & DRRec1("AccCutPasteBilling") & "),  convert(money," & DRRec1("AccCutPasteBilling") & "), 'VAS', '" & C2CurrEDate & "', '" & Now & "',1) "
                                            cmdUp1.CommandText = strQuery
                                            cmdUp1.CommandType = CommandType.Text
                                            cmdUp1.ExecuteNonQuery()
                                        End If

                                    End If
                                    'Response.Write(ErrFound)
                                    If ErrFound = False Then
                                        MyTransAttr1.Commit()
                                    Else
                                        MyTransAttr1.Rollback()
                                    End If

                                Catch ex As Exception
                                    Response.Write(ex.Message)
                                    MyTransAttr1.Rollback()
                                    ErrFound = True
                                    ErrMessage = ex.Message
                                Finally
                                    If myConnection1.State = ConnectionState.Open Then
                                        myConnection1.Close()
                                        myConnection1.Dispose()
                                    End If
                                End Try

                            ElseIf Trim(DRRec1("Mode").ToString) = "DV" Then
                                Dim myConnection2 As New SqlConnection(strConn)
                                Dim MyTransAttr2 As SqlTransaction

                                Dim cmdUp2 As New SqlCommand()
                                myConnection2.Open()
                                MyTransAttr2 = myConnection2.BeginTransaction()
                                cmdUp2.Connection = myConnection2
                                cmdUp2.Transaction = MyTransAttr2
                                cmdUp2.CommandTimeout = 600
                                Try

                                    strQuery = "Select * from AdminSecureweb.dbo.BillDetails where AccountID = '" & DRRec1("AccountID").ToString & "'  "
                                    Dim SQLCmd3 As New SqlCommand(strQuery, New SqlConnection(strConn))
                                    Try
                                        SQLCmd3.Connection.Open()
                                        Dim DRRec3 As SqlDataReader = SQLCmd3.ExecuteReader()
                                        If DRRec3.HasRows = True Then
                                            UDFound = True
                                            If DRRec3.Read Then
                                                strQuery = "Select NewID() as UID"
                                                Dim SQLCmd6 As New SqlCommand(strQuery, New SqlConnection(strConn))
                                                Try
                                                    SQLCmd6.Connection.Open()
                                                    BillAccID = SQLCmd6.ExecuteScalar.ToString

                                                Finally
                                                    If SQLCmd6.Connection.State = ConnectionState.Open Then
                                                        SQLCmd6.Connection.Close()
                                                        SQLCmd6.Connection.Dispose()
                                                    End If
                                                End Try
                                                AccountID = DRRec1("AccountID").ToString
                                                Cycle = DRRec1("Cycle").ToString
                                                MinBilling = DRRec1("MinBilling").ToString
                                                BillMonth = DLMonth.SelectedValue
                                                BillYear = DLYear.SelectedValue
                                                Mode = Trim(DRRec1("Mode").ToString)

                                                ''Response.Write(BillAccID)
                                                strQuery = "Insert Into AdminSecureweb.dbo.tblBillAccounts (BillAccID, AccountID, Cycle, MinBilling, BillMonth, BillYear, BillCycle, Mode, ModDate) Values ('" & BillAccID & "', '" & AccountID & "','" & Cycle & "', '" & MinBilling & "', '" & BillMonth & "', '" & BillYear & "', '" & DLCycle.SelectedValue & "', '" & Mode & "', '" & Now & "') "
                                                cmdUp2.CommandText = strQuery
                                                cmdUp2.CommandType = CommandType.Text
                                                cmdUp2.ExecuteNonQuery()

                                                strQuery = "DELETE FROM AdminETS.dbo.tblbillinglines WHERE BillAccID ='11111111-1111-1111-1111-111111111111' AND Transcriptionid in (Select TranscriptionID FROM AdminSecureweb.dbo.tblTranscriptionClientMain where AccountID = '" & DRRec1("AccountID").ToString & "'  and datediff(day,datemodified, '" & edate & "') between 0 and datediff(day,'" & sdate & "','" & edate & "')  ) "
                                                cmdUp2.CommandText = strQuery
                                                cmdUp2.CommandType = CommandType.Text
                                                cmdUp2.ExecuteNonQuery()
                                                ''Response.Write(strQuery)
                                                Dim Dcount As Integer
                                                Dim DevActID As String
                                                Dim DevActName As String

                                                For Dcount = 1 To 2
                                                    strQuery = "Select NewID() as UID"
                                                    Dim SQLCmdDev As New SqlCommand(strQuery, New SqlConnection(strConn))
                                                    Try


                                                        SQLCmdDev.Connection.Open()
                                                        DevActID = SQLCmdDev.ExecuteScalar.ToString
                                                    Finally
                                                        If SQLCmdDev.Connection.State = ConnectionState.Open Then
                                                            SQLCmdDev.Connection.Close()
                                                            SQLCmdDev.Connection.Dispose()
                                                        End If

                                                    End Try

                                                    If Dcount = 1 Then
                                                        DevActName = "Telephone"
                                                    ElseIf Dcount = 2 Then
                                                        DevActName = "DVR"
                                                    End If


                                                    strQuery = "Insert Into AdminSecureweb.dbo.tblAccBillDetails (BillAccID, AccountID, SubActID, SubActName,  Rate, MiscRate, StatRate, LCMethodID,ModDate) Values ('" & BillAccID & "', '" & AccountID & "', '" & DevActID & "', '" & DevActName & "','" & DRRec3("Rate") & "', '" & DRRec3("MiscRate") & "', '" & DRRec3("StatRate") & "', '" & DRRec3("LCMethodID").ToString & "', '" & Now & "') "
                                                    cmdUp2.CommandText = strQuery
                                                    cmdUp2.CommandType = CommandType.Text
                                                    cmdUp2.ExecuteNonQuery()



                                                    strQuery = "Select  * from AdminETS.dbo.tblLCMethodology where TrackID = '" & DRRec3("LCMethodID").ToString & "' "
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
                                                                RptBIUOnOff = DRRec4("RptBIUOnOff").ToString
                                                                RptShiftedAll = DRRec4("RptShiftedAll").ToString
                                                                RptDocVariable = DRRec4("RptDocVariable").ToString
                                                                RptShifted = DRRec4("RptShifted").ToString
                                                                RptSpaces = DRRec4("RptSpaces").ToString
                                                                RptSCT = DRRec4("RptSCT").ToString
                                                                CharsPerLines = DRRec4("CharsPerLines")
                                                                BIUVal = DRRec4("BIUVal")
                                                                BIUShiftedAll = DRRec4("BIUShiftedAll")
                                                                CountMethod = DRRec4("CountMethod").ToString
                                                                If DRRec4("isdefault").ToString.ToUpper = "TRUE" Then
                                                                    DefaultLC = True
                                                                Else
                                                                    DefaultLC = False
                                                                End If
                                                                ' strQuery = "Select NewID() as UID"
                                                                strQuery = "Select distinct  IsNull(TB.Value, 0) AS TemplateDeduction, T.*, M.Type,M.Location,CASE WHEN M.Priority = 1 AND (DATEDIFF(MINUTE, M.DateCreated, ISNULL(M.DateModified, GETDATE()))/60) < M.TAT THEN 'True' ELSE 'False' END as Priority,M.DictatorID,M.Jobnumber,ROUND(CAST(datediff(s,0,M.duration) AS Float)*1.0/60,2) as Mins from AdminSecureweb.dbo.tblTranscriptionClientMain M Left Outer Join tblTransLC T ON T.TranscriptionID = M.TranscriptionID  LEFT OUTER JOIN tbltemplatesLinesDeductionsForBilling TB ON M.TemplateID = TB.TemplateID where T.TranscriptionID is not NULL and M.AccountID = '" & DRRec1("AccountID").ToString & "'   and datediff(day,M.datemodified, '" & edate & "') between 0 and datediff(day,'" & sdate & "','" & edate & "')   "

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
                                                                            stdTempCharDed = DRRec5("TemplateDeduction")
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
                                                                            ElseIf CountMethod = "Minutes" Then
                                                                                If String.IsNullOrEmpty(DRRec5("Mins")) Then
                                                                                    ReportLC = 0
                                                                                Else
                                                                                    ReportLC = DRRec5("Mins")
                                                                                End If
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
                                                                                        If RptBIUOnOff = True Then
                                                                                            ReportLC += DRRec5("NumBodyBIUOnOff") * BIUVal
                                                                                        End If

                                                                                        If RptShiftedAll = True Then
                                                                                            ReportLC += DRRec5("NumBodyCharsWShiftAll") * BIUShiftedAll
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
                                                                                        If RptShiftedAll = True Then
                                                                                            ReportLC += DRRec5("HeaderWShiftAll") * BIUShiftedAll
                                                                                        End If
                                                                                        If RptBIUOnOff = True Then
                                                                                            ReportLC += DRRec5("HeaderBIUCountOnOff") * BIUVal
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
                                                                                        If RptBIUOnOff = True Then
                                                                                            ReportLC += DRRec5("FooterBIUCountOnOff") * BIUVal
                                                                                        End If
                                                                                        If RptShiftedAll = True Then
                                                                                            ReportLC += DRRec5("FooterWShiftAll") * BIUShiftedAll
                                                                                        End If
                                                                                    End If

                                                                                    If RptDocVariable = True Then
                                                                                        ReportLC += DRRec5("DocVarCount")
                                                                                    End If
                                                                                    ReportLC = ReportLC - stdTempCharDed
                                                                                    If ReportLC < 0 Then
                                                                                        ReportLC = 0
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

                                                                            'Dim Row1 As New TableRow
                                                                            'Dim Cell1 As New TableCell
                                                                            'Dim Cell2 As New TableCell
                                                                            'Dim Cell3 As New TableCell
                                                                            'Dim Cell4 As New TableCell
                                                                            'Dim Cell5 As New TableCell
                                                                            'Dim Cell6 As New TableCell
                                                                            'Dim Cell As New TableCell
                                                                            'Cell.Text = i
                                                                            'Cell1.Text = DRRec1("AccountName").ToString
                                                                            'Cell2.Text = Trim(DRRec1("Mode").ToString)
                                                                            'Cell3.Text = DRRec5("Jobnumber").ToString
                                                                            'Cell4.Text = ReportLC
                                                                            'Cell4.Text = ReportLC
                                                                            'Cell5.Text = CountMethod
                                                                            'Row1.Cells.Add(Cell)
                                                                            'Row1.Cells.Add(Cell1)
                                                                            'Row1.Cells.Add(Cell2)
                                                                            'Row1.Cells.Add(Cell3)
                                                                            'Row1.Cells.Add(Cell4)
                                                                            'Row1.Cells.Add(Cell5)
                                                                            'Table1.Rows.Add(Row1)
                                                                            Amount = ReportLC * Rate
                                                                            Type = DRRec5("Type").ToString

                                                                            strQuery = "Insert Into AdminETS.dbo.tblBillingLines (BillAccID, SubActID, TranscriptionID, stdLCCount, unit, Rate, Amount, Type,Priority, updateDate) Values ('" & BillAccID & "', '" & DevActID & "', '" & TranscriptionID & "','" & stdLCCount & "', '" & ReportLC & "', '" & Rate & "', '" & Amount & "', '" & Type & "', 'False', '" & Now & "') "
                                                                            cmdUp2.CommandText = strQuery
                                                                            cmdUp2.CommandType = CommandType.Text
                                                                            cmdUp2.ExecuteNonQuery()

                                                                            If DRRec5("Priority").ToString = "True" And Not CountMethod = "PerReport" Then
                                                                                If StatChgPerReport Then
                                                                                    Rate = DRRec3("StatRate")
                                                                                    Amount = Rate
                                                                                    strQuery = "Insert Into AdminETS.dbo.tblBillingLines (BillAccID, SubActID, TranscriptionID, stdLCCount, unit, Rate, Amount, Type, Priority, updateDate) Values ('" & BillAccID & "', '" & DevActID & "', '" & TranscriptionID & "','" & stdLCCount & "', '" & 1 & "', '" & Rate & "', '" & Amount & "', '" & Type & "',  'True', '" & Now & "') "
                                                                                Else
                                                                                    Rate = DRRec3("StatRate")
                                                                                    Amount = ReportLC * Rate
                                                                                    strQuery = "Insert Into AdminETS.dbo.tblBillingLines (BillAccID, SubActID, TranscriptionID, stdLCCount, unit, Rate, Amount, Type, Priority, updateDate) Values ('" & BillAccID & "', '" & DevActID & "', '" & TranscriptionID & "','" & stdLCCount & "', '" & ReportLC & "', '" & Rate & "', '" & Amount & "', '" & Type & "',  'True', '" & Now & "') "

                                                                                End If
                                                                                cmdUp2.CommandText = strQuery
                                                                                cmdUp2.CommandType = CommandType.Text
                                                                                cmdUp2.ExecuteNonQuery()
                                                                            End If
                                                                        End While
                                                                    End If
                                                                Finally
                                                                    If SQLCmd5.Connection.State = System.Data.ConnectionState.Open Then
                                                                        SQLCmd5.Connection.Close()
                                                                        SQLCmd5.Connection.Dispose()
                                                                        SQLCmd5 = Nothing
                                                                    End If
                                                                End Try

                                                            End If
                                                        End If
                                                    Finally
                                                        If SQLCmd4.Connection.State = System.Data.ConnectionState.Open Then
                                                            SQLCmd4.Connection.Close()
                                                            SQLCmd4.Connection.Dispose()
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
                                    If DLCycle.SelectedValue = 1 Then
                                        strQuery = "Delete from  AdminSecureweb.dbo.tblinvItemdet WHERE AccountID = '" & AccountID & "' and InvoiceID ='11111111-1111-1111-1111-111111111111'  and ISAuto=1 and servicedate='" & C1CurrEDate.AddDays(-1) & "' "
                                        cmdUp2.CommandText = strQuery
                                        cmdUp2.CommandType = CommandType.Text
                                        cmdUp2.ExecuteNonQuery()
                                    Else
                                        strQuery = "Delete from  AdminSecureweb.dbo.tblinvItemdet WHERE AccountID = '" & AccountID & "' and InvoiceID ='11111111-1111-1111-1111-111111111111'  and ISAuto=1 and servicedate='" & C2CurrEDate & "' "
                                        cmdUp2.CommandText = strQuery
                                        cmdUp2.CommandType = CommandType.Text
                                        cmdUp2.ExecuteNonQuery()
                                    End If
                                    If DLCycle.SelectedValue = 2 And DRRec1("IsAccFaxBilling").ToString And DRRec1("FaxPageCount") > 0 Then
                                        If Not DRRec1("AccFaxCharges") > 0 Then
                                            ErrFound = True
                                            ErrMessage = "Fax charges not set."
                                        End If
                                        If ErrFound = False Then
                                            strQuery = "Insert Into AdminSecureweb.dbo.tblinvItemdet (AccountID,InvoiceID, itemid, Descr, quantity, Amount,totamount,mode,servicedate, dateupdate, IsAuto) Values ('" & AccountID & "', '11111111-1111-1111-1111-111111111111', '548B3D50-3F0E-4A61-BFF9-115569721C55', 'Number of pages Faxed', '" & DRRec1("FaxPageCount") & "', convert(money," & DRRec1("AccFaxCharges") & "),  convert(money," & DRRec1("FaxPageCount") * DRRec1("AccFaxCharges") & "), 'VAS', '" & C2CurrEDate & "', '" & Now & "',1) "
                                            cmdUp2.CommandText = strQuery
                                            cmdUp2.CommandType = CommandType.Text
                                            cmdUp2.ExecuteNonQuery()
                                        End If

                                    End If

                                    If DLCycle.SelectedValue = 2 And DRRec1("IsAccVPNBilling").ToString Then

                                        If Not DRRec1("AccVPNCharges") > 0 Then
                                            ErrFound = True
                                            ErrMessage = "VPN charges not set."
                                        End If
                                        If ErrFound = False Then
                                            strQuery = "Insert Into AdminSecureweb.dbo.tblinvItemdet (AccountID,InvoiceID, itemid, Descr, quantity, Amount,totamount,mode,servicedate, dateupdate, IsAuto) Values ('" & AccountID & "', '11111111-1111-1111-1111-111111111111', '781ED926-346B-48A2-80C0-9914D8ECC0BC', 'VPN Charges', '1', convert(money," & DRRec1("AccVPNCharges") & "),  convert(money," & DRRec1("AccVPNCharges") & "), 'VAS', '" & C2CurrEDate & "', '" & Now & "',1) "
                                            cmdUp2.CommandText = strQuery
                                            cmdUp2.CommandType = CommandType.Text
                                            cmdUp2.ExecuteNonQuery()
                                        End If

                                    End If

                                    If DLCycle.SelectedValue = 2 And DRRec1("IsAccCutPasteBilling").ToString Then

                                        If Not DRRec1("AccCutPasteBilling") > 0 Then
                                            ErrFound = True
                                            ErrMessage = "Cut/Paste charges not set."
                                        End If
                                        If ErrFound = False Then
                                            strQuery = "Insert Into AdminSecureweb.dbo.tblinvItemdet (AccountID,InvoiceID, itemid, Descr, quantity, Amount,totamount,mode,servicedate, dateupdate, IsAuto) Values ('" & AccountID & "', '11111111-1111-1111-1111-111111111111', '16d2956b-e2c1-47b8-aff0-ad42b8d8cde8', 'Additional Cut and Paste charges', '1', convert(money," & DRRec1("AccCutPasteBilling") & "),  convert(money," & DRRec1("AccCutPasteBilling") & "), 'VAS', '" & C2CurrEDate & "', '" & Now & "',1) "
                                            cmdUp2.CommandText = strQuery
                                            cmdUp2.CommandType = CommandType.Text
                                            cmdUp2.ExecuteNonQuery()
                                        End If

                                    End If
                                    If ErrFound = False Then
                                        MyTransAttr2.Commit()
                                    Else
                                        MyTransAttr2.Rollback()
                                    End If

                                Catch ex As Exception
                                    MyTransAttr2.Rollback()
                                    ErrFound = True
                                    ErrMessage = ex.Message
                                Finally
                                    If myConnection2.State = ConnectionState.Open Then
                                        myConnection2.Close()
                                    End If
                                End Try
                            ElseIf Trim(DRRec1("Mode").ToString) = "LC" Then

                                Dim myConnection As New SqlConnection(strConn)
                                Dim MyTransAttr As SqlTransaction
                                ' Dim strQuery As String
                                Dim cmdUp As New SqlCommand()
                                myConnection.Open()
                                MyTransAttr = myConnection.BeginTransaction()
                                cmdUp.Connection = myConnection
                                cmdUp.Transaction = MyTransAttr
                                cmdUp.CommandTimeout = 600
                                Try
                                    strQuery = "Select distinct T.*, M.Type,M.Location,CASE WHEN M.Priority = 1 AND (DATEDIFF(MINUTE, M.DateCreated, ISNULL(M.DateModified, GETDATE()))/60) < M.TAT THEN 'True' ELSE 'False' END as Priority,M.DictatorID,M.Jobnumber,   ROUND(CAST(datediff(s,0,M.duration) AS Float)*1.0/60,2) as Mins from AdminSecureweb.dbo.tblTranscriptionClientMain M Left Outer Join tblTransLC T ON T.TranscriptionID = M.TranscriptionID where T.TranscriptionID is not NULL and M.AccountID = '" & DRRec1("AccountID").ToString & "'   and datediff(day,M.datemodified, '" & edate & "') between 0 and datediff(day,'" & sdate & "','" & edate & "')   and M.Location not in (Select LocCode from AdminETS.dbo.tblAccountsLocations where AccountID = '" & DRRec1("AccountID").ToString & "')  "
                                    'Response.Write(strQuery)
                                    Dim SQLCmdChk1 As New SqlCommand(strQuery, New SqlConnection(strConn))
                                    Try
                                        SQLCmdChk1.Connection.Open()
                                        Dim DRRecChk1 As SqlDataReader = SQLCmdChk1.ExecuteReader()
                                        If DRRecChk1.HasRows = True Then
                                            ErrMessage = "Undefined location(s) found."
                                            ErrFound = True
                                        End If
                                    Catch ex As Exception
                                        ErrMessage = ex.Message
                                        ErrFound = True
                                    Finally
                                        If SQLCmdChk1.Connection.State = System.Data.ConnectionState.Open Then
                                            SQLCmdChk1.Connection.Close()
                                            SQLCmdChk1 = Nothing
                                        End If
                                    End Try
                                    If ErrFound = False Then
                                        strQuery = "Select * from AdminETS.dbo.tblAccountsLocations A LEFT OUTER JOIN AdminSecureweb.dbo.BillDetails B ON A.AccountID= B.AccountID and A.TrackID = B.SubActID where A.AccountID = '" & DRRec1("AccountID").ToString & "' and B.SubActID IS NULL  "
                                        'Response.Write(strQuery)
                                        Dim SQLCmdChk2 As New SqlCommand(strQuery, New SqlConnection(strConn))
                                        Try
                                            SQLCmdChk2.Connection.Open()
                                            Dim DRRecChk2 As SqlDataReader = SQLCmdChk2.ExecuteReader()
                                            If DRRecChk2.HasRows = True Then
                                                ErrMessage = "Rate was not set for locations(s)."
                                                ErrFound = True
                                            End If
                                        Catch ex As Exception
                                            ErrMessage = ex.Message
                                            ErrFound = True
                                        Finally
                                            If SQLCmdChk2.Connection.State = System.Data.ConnectionState.Open Then
                                                SQLCmdChk2.Connection.Close()
                                                SQLCmdChk2 = Nothing
                                            End If
                                        End Try
                                    End If
                                    If ErrFound = False Then
                                        Dim LocCode As Integer
                                        strQuery = "Select NewID() as UID"
                                        Dim SQLCmd6 As New SqlCommand(strQuery, New SqlConnection(strConn))
                                        Try
                                            SQLCmd6.Connection.Open()
                                            BillAccID = SQLCmd6.ExecuteScalar.ToString
                                        Finally
                                            If SQLCmd6.Connection.State = ConnectionState.Open Then
                                                SQLCmd6.Connection.Close()
                                            End If
                                        End Try

                                        AccountID = DRRec1("AccountID").ToString
                                        Cycle = DRRec1("Cycle").ToString
                                        MinBilling = DRRec1("MinBilling").ToString
                                        BillMonth = DLMonth.SelectedValue
                                        BillYear = DLYear.SelectedValue
                                        Mode = Trim(DRRec1("Mode").ToString)

                                        ''Response.Write(BillAccID)
                                        strQuery = "Insert Into AdminSecureweb.dbo.tblBillAccounts (BillAccID, AccountID, Cycle, MinBilling, BillMonth, BillYear, BillCycle, Mode, ModDate) Values ('" & BillAccID & "', '" & AccountID & "','" & Cycle & "', '" & MinBilling & "', '" & BillMonth & "', '" & BillYear & "', '" & DLCycle.SelectedValue & "', '" & Mode & "', '" & Now & "') "
                                        cmdUp.CommandText = strQuery
                                        cmdUp.CommandType = CommandType.Text
                                        cmdUp.ExecuteNonQuery()
                                        strQuery = "DELETE FROM AdminETS.dbo.tblbillinglines WHERE BillAccID ='11111111-1111-1111-1111-111111111111' AND Transcriptionid in (Select TranscriptionID FROM AdminSecureweb.dbo.tblTranscriptionClientMain where AccountID = '" & DRRec1("AccountID").ToString & "'  and datediff(day,datemodified, '" & edate & "') between 0 and datediff(day,'" & sdate & "','" & edate & "')  ) "
                                        cmdUp.CommandText = strQuery
                                        cmdUp.CommandType = CommandType.Text
                                        cmdUp.ExecuteNonQuery()
                                        strQuery = "Select * from AdminETS.dbo.tblAccountsLocations where AccountID = '" & DRRec1("AccountID").ToString & "'  "
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
                                                    strQuery = "Select * from AdminSecureweb.dbo.BillDetails where AccountID = '" & DRRec1("AccountID").ToString & "' and SubActID = '" & SubActID & "'   "
                                                    '  'Response.Write(strQuery)
                                                    Dim SQLCmd3 As New SqlCommand(strQuery, New SqlConnection(strConn))
                                                    Try
                                                        SQLCmd3.Connection.Open()
                                                        Dim DRRec3 As SqlDataReader = SQLCmd3.ExecuteReader()
                                                        If DRRec3.HasRows = True Then
                                                            UDFound = True
                                                            If DRRec3.Read Then
                                                                ''Response.Write(strQuery)
                                                                strQuery = "Insert Into AdminSecureweb.dbo.tblAccBillDetails (BillAccID, AccountID, SubActID, SubActName, Rate, MiscRate, StatRate, LCMethodID,ModDate) Values ('" & BillAccID & "', '" & AccountID & "', '" & SubActID & "', '" & SubActName & "','" & DRRec3("Rate") & "', '" & DRRec3("MiscRate") & "', '" & DRRec3("StatRate") & "', '" & DRRec3("LCMethodID").ToString & "', '" & Now & "') "
                                                                'Response.Write(strQuery)
                                                                Dim SQLCmdUp2 As New SqlCommand(strQuery, New SqlConnection(strConn))
                                                                SQLCmdUp2.Connection.Open()
                                                                SQLCmdUp2.ExecuteNonQuery()
                                                                SQLCmdUp2.Connection.Close()
                                                                strQuery = "Select  * from AdminETS.dbo.tblLCMethodology where TrackID = '" & DRRec3("LCMethodID").ToString & "' "
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
                                                                            RptBIUOnOff = DRRec4("RptBIUOnOff").ToString
                                                                            RptShiftedAll = DRRec4("RptShiftedAll").ToString
                                                                            RptDocVariable = DRRec4("RptDocVariable").ToString
                                                                            RptShifted = DRRec4("RptShifted").ToString
                                                                            RptSpaces = DRRec4("RptSpaces").ToString
                                                                            RptSCT = DRRec4("RptSCT").ToString
                                                                            CharsPerLines = DRRec4("CharsPerLines")
                                                                            BIUVal = DRRec4("BIUVal")
                                                                            BIUShiftedAll = DRRec4("BIUShiftedAll")
                                                                            CountMethod = DRRec4("CountMethod").ToString

                                                                            If DRRec4("isdefault").ToString.ToUpper = "TRUE" Then
                                                                                DefaultLC = True
                                                                            Else
                                                                                DefaultLC = False
                                                                            End If

                                                                            strQuery = "Select distinct  IsNull(TB.Value, 0) AS TemplateDeduction, T.*, M.Type,M.Location,CASE WHEN M.Priority = 1 AND (DATEDIFF(MINUTE, M.DateCreated, ISNULL(M.DateModified, GETDATE()))/60) < M.TAT THEN 'True' ELSE 'False' END as Priority,M.DictatorID,M.Jobnumber,   ROUND(CAST(datediff(s,0,M.duration) AS Float)*1.0/60,2) as Mins from AdminSecureweb.dbo.tblTranscriptionClientMain M Left Outer Join tblTransLC T ON T.TranscriptionID = M.TranscriptionID  LEFT OUTER JOIN tbltemplatesLinesDeductionsForBilling TB ON M.TemplateID = TB.TemplateID where T.TranscriptionID is not NULL and M.AccountID = '" & DRRec1("AccountID").ToString & "'   and datediff(day,M.datemodified, '" & edate & "') between 0 and datediff(day,'" & sdate & "','" & edate & "')   and M.Location ='" & LocCode & "' order by JobNumber"
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
                                                                                        stdTempCharDed = DRRec5("TemplateDeduction")
                                                                                        Rate = DRRec3("Rate")
                                                                                        If CountMethod = "PerDictator" Then
                                                                                            ReportLC = DRRec5("stdLC")
                                                                                        End If
                                                                                        ''Response.Write(DRRec5("transcriptionid").ToString & "#")
                                                                                        If CountMethod = "PerReport" Then
                                                                                            ReportLC = 1
                                                                                        ElseIf CountMethod = "Minutes" Then
                                                                                            If String.IsNullOrEmpty(DRRec5("Mins")) Then
                                                                                                ReportLC = 0
                                                                                            Else
                                                                                                ReportLC = DRRec5("Mins")
                                                                                            End If
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
                                                                                                    If RptBIUOnOff = True Then
                                                                                                        ReportLC += DRRec5("NumBodyBIUOnOff") * BIUVal
                                                                                                    End If

                                                                                                    If RptShiftedAll = True Then
                                                                                                        ReportLC += DRRec5("NumBodyCharsWShiftAll") * BIUShiftedAll
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
                                                                                                    If RptShiftedAll = True Then
                                                                                                        ReportLC += DRRec5("HeaderWShiftAll") * BIUShiftedAll
                                                                                                    End If
                                                                                                    If RptBIUOnOff = True Then
                                                                                                        ReportLC += DRRec5("HeaderBIUCountOnOff") * BIUVal
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
                                                                                                    If RptBIUOnOff = True Then
                                                                                                        ReportLC += DRRec5("FooterBIUCountOnOff") * BIUVal
                                                                                                    End If
                                                                                                    If RptShiftedAll = True Then
                                                                                                        ReportLC += DRRec5("FooterWShiftAll") * BIUShiftedAll
                                                                                                    End If

                                                                                                    ''Response.Write(ReportLC & "#")

                                                                                                End If

                                                                                                If RptDocVariable = True Then
                                                                                                    ReportLC += DRRec5("DocVarCount")
                                                                                                End If
                                                                                                ReportLC = ReportLC - stdTempCharDed
                                                                                                If ReportLC < 0 Then
                                                                                                    ReportLC = 0
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

                                                                                        'Dim Row1 As New TableRow
                                                                                        'Dim Cell1 As New TableCell
                                                                                        'Dim Cell2 As New TableCell
                                                                                        'Dim Cell3 As New TableCell
                                                                                        'Dim Cell4 As New TableCell
                                                                                        'Dim Cell5 As New TableCell
                                                                                        'Dim Cell6 As New TableCell
                                                                                        'Dim Cell7 As New TableCell
                                                                                        'Dim Cell As New TableCell
                                                                                        'Cell.Text = i
                                                                                        'Cell1.Text = DRRec1("AccountName").ToString
                                                                                        'Cell2.Text = Trim(DRRec1("Mode").ToString)
                                                                                        'Cell3.Text = DRRec5("Jobnumber").ToString
                                                                                        'Cell4.Text = ReportLC
                                                                                        'Cell4.Text = ReportLC
                                                                                        'Cell5.Text = CountMethod
                                                                                        'Cell7.Text = SubActName
                                                                                        'Row1.Cells.Add(Cell)
                                                                                        'Row1.Cells.Add(Cell1)
                                                                                        'Row1.Cells.Add(Cell2)
                                                                                        'Row1.Cells.Add(Cell3)
                                                                                        'Row1.Cells.Add(Cell4)
                                                                                        'Row1.Cells.Add(Cell5)
                                                                                        'Row1.Cells.Add(Cell7)
                                                                                        'Table1.Rows.Add(Row1)
                                                                                        Amount = ReportLC * Rate
                                                                                        Type = DRRec5("Type").ToString



                                                                                        strQuery = "Insert Into AdminETS.dbo.tblBillingLines (BillAccID, SubActID, TranscriptionID, stdLCCount, unit, Rate, Amount, Type, Priority, updateDate) Values ('" & BillAccID & "', '" & SubActID & "', '" & TranscriptionID & "','" & stdLCCount & "', '" & ReportLC & "', '" & Rate & "', '" & Amount & "', '" & Type & "',  'False', '" & Now & "') "
                                                                                        cmdUp.CommandText = strQuery
                                                                                        cmdUp.CommandType = CommandType.Text
                                                                                        cmdUp.ExecuteNonQuery()


                                                                                        If DRRec5("Priority").ToString = "True" And Not CountMethod = "PerReport" Then
                                                                                            If StatChgPerReport Then
                                                                                                Rate = DRRec3("StatRate")
                                                                                                Amount = Rate
                                                                                                strQuery = "Insert Into AdminETS.dbo.tblBillingLines (BillAccID, SubActID, TranscriptionID, stdLCCount, unit, Rate, Amount, Type, Priority, updateDate) Values ('" & BillAccID & "', '" & SubActID & "', '" & TranscriptionID & "','" & stdLCCount & "', '" & 1 & "', '" & Rate & "', '" & Amount & "', '" & Type & "', 'True', '" & Now & "') "
                                                                                            Else
                                                                                                Rate = DRRec3("StatRate")
                                                                                                Amount = ReportLC * Rate
                                                                                                strQuery = "Insert Into AdminETS.dbo.tblBillingLines (BillAccID, SubActID, TranscriptionID, stdLCCount, unit, Rate, Amount, Type, Priority, updateDate) Values ('" & BillAccID & "', '" & SubActID & "', '" & TranscriptionID & "','" & stdLCCount & "', '" & ReportLC & "', '" & Rate & "', '" & Amount & "', '" & Type & "', 'True', '" & Now & "') "
                                                                                            End If
                                                                                           
                                                                                            cmdUp.CommandText = strQuery
                                                                                            cmdUp.CommandType = CommandType.Text
                                                                                            cmdUp.ExecuteNonQuery()

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
                                    If DLCycle.SelectedValue = 1 Then
                                        strQuery = "Delete from  AdminSecureweb.dbo.tblinvItemdet WHERE AccountID = '" & AccountID & "' and InvoiceID ='11111111-1111-1111-1111-111111111111'  and ISAuto=1 and servicedate='" & C1CurrEDate.AddDays(-1) & "' "
                                        cmdUp.CommandText = strQuery
                                        cmdUp.CommandType = CommandType.Text
                                        cmdUp.ExecuteNonQuery()
                                    Else
                                        strQuery = "Delete from  AdminSecureweb.dbo.tblinvItemdet WHERE AccountID = '" & AccountID & "' and InvoiceID ='11111111-1111-1111-1111-111111111111'  and ISAuto=1 and servicedate='" & C2CurrEDate & "' "
                                        cmdUp.CommandText = strQuery
                                        cmdUp.CommandType = CommandType.Text
                                        cmdUp.ExecuteNonQuery()
                                    End If
                                    If DLCycle.SelectedValue = 2 And DRRec1("IsAccFaxBilling").ToString And DRRec1("FaxPageCount") > 0 Then
                                        If Not DRRec1("AccFaxCharges") > 0 Then
                                            ErrFound = True
                                            ErrMessage = "Fax charges not set."
                                        End If
                                        If ErrFound = False Then
                                            strQuery = "Insert Into AdminSecureweb.dbo.tblinvItemdet (AccountID,InvoiceID, itemid, Descr, quantity, Amount,totamount,mode,servicedate, dateupdate, IsAuto) Values ('" & AccountID & "', '11111111-1111-1111-1111-111111111111', '548B3D50-3F0E-4A61-BFF9-115569721C55', 'Number of pages Faxed', '" & DRRec1("FaxPageCount") & "', convert(money," & DRRec1("AccFaxCharges") & "),  convert(money," & DRRec1("FaxPageCount") * DRRec1("AccFaxCharges") & "), 'VAS', '" & C2CurrEDate & "', '" & Now & "',1) "
                                            cmdUp.CommandText = strQuery
                                            cmdUp.CommandType = CommandType.Text
                                            cmdUp.ExecuteNonQuery()
                                        End If

                                    End If

                                    If DLCycle.SelectedValue = 2 And DRRec1("IsAccVPNBilling").ToString Then

                                        If Not DRRec1("AccVPNCharges") > 0 Then
                                            ErrFound = True
                                            ErrMessage = "VPN charges not set."
                                        End If
                                        If ErrFound = False Then
                                            strQuery = "Insert Into AdminSecureweb.dbo.tblinvItemdet (AccountID,InvoiceID, itemid, Descr, quantity, Amount,totamount,mode,servicedate, dateupdate, IsAuto) Values ('" & AccountID & "', '11111111-1111-1111-1111-111111111111', '781ED926-346B-48A2-80C0-9914D8ECC0BC', 'VPN Charges', '1', convert(money," & DRRec1("AccVPNCharges") & "),  convert(money," & DRRec1("AccVPNCharges") & "), 'VAS', '" & C2CurrEDate & "', '" & Now & "',1) "
                                            cmdUp.CommandText = strQuery
                                            cmdUp.CommandType = CommandType.Text
                                            cmdUp.ExecuteNonQuery()
                                        End If

                                    End If

                                    If DLCycle.SelectedValue = 2 And DRRec1("IsAccCutPasteBilling").ToString Then

                                        If Not DRRec1("AccCutPasteBilling") > 0 Then
                                            ErrFound = True
                                            ErrMessage = "Cut/Paste charges not set."
                                        End If
                                        If ErrFound = False Then
                                            strQuery = "Insert Into AdminSecureweb.dbo.tblinvItemdet (AccountID,InvoiceID, itemid, Descr, quantity, Amount,totamount,mode,servicedate, dateupdate, IsAuto) Values ('" & AccountID & "', '11111111-1111-1111-1111-111111111111', '16d2956b-e2c1-47b8-aff0-ad42b8d8cde8', 'Additional Cut and Paste charges', '1', convert(money," & DRRec1("AccCutPasteBilling") & "),  convert(money," & DRRec1("AccCutPasteBilling") & "), 'VAS', '" & C2CurrEDate & "', '" & Now & "',1) "
                                            cmdUp.CommandText = strQuery
                                            cmdUp.CommandType = CommandType.Text
                                            cmdUp.ExecuteNonQuery()
                                        End If

                                    End If
                                    If ErrFound = False Then
                                        MyTransAttr.Commit()
                                    Else
                                        MyTransAttr.Rollback()
                                    End If
                                Catch ex As Exception
                                    MyTransAttr.Rollback()
                                    ErrFound = True
                                    ErrMessage = ex.Message
                                Finally
                                    If myConnection.State = ConnectionState.Open Then
                                        myConnection.Close()
                                    End If
                                End Try
                            ElseIf Trim(DRRec1("Mode").ToString) = "TT" Then
                                Dim myConnection As New SqlConnection(strConn)
                                Dim MyTransAttr As SqlTransaction
                                'Dim strQuery As String
                                Dim cmdUp As New SqlCommand()
                                myConnection.Open()
                                MyTransAttr = myConnection.BeginTransaction()
                                cmdUp.Connection = myConnection
                                cmdUp.Transaction = MyTransAttr
                                cmdUp.CommandTimeout = 600
                                Try
                                    Dim TAT As Integer
                                    strQuery = "Select NewID() as UID"
                                    Dim SQLCmd6 As New SqlCommand(strQuery, New SqlConnection(strConn))
                                    Try
                                        SQLCmd6.Connection.Open()
                                        BillAccID = SQLCmd6.ExecuteScalar.ToString
                                    Finally
                                        If SQLCmd6.Connection.State = ConnectionState.Open Then
                                            SQLCmd6.Connection.Close()
                                        End If
                                    End Try
                                    AccountID = DRRec1("AccountID").ToString
                                    Cycle = DRRec1("Cycle").ToString
                                    MinBilling = DRRec1("MinBilling").ToString
                                    BillMonth = DLMonth.SelectedValue
                                    BillYear = DLYear.SelectedValue
                                    Mode = Trim(DRRec1("Mode").ToString)

                                    ''Response.Write(BillAccID)
                                    strQuery = "Insert Into AdminSecureweb.dbo.tblBillAccounts (BillAccID, AccountID, Cycle, MinBilling, BillMonth, BillYear, BillCycle, Mode, ModDate) Values ('" & BillAccID & "', '" & AccountID & "','" & Cycle & "', '" & MinBilling & "', '" & BillMonth & "', '" & BillYear & "', '" & DLCycle.SelectedValue & "', '" & Mode & "', '" & Now & "') "
                                    cmdUp.CommandText = strQuery
                                    cmdUp.CommandType = CommandType.Text
                                    cmdUp.ExecuteNonQuery()
                                    strQuery = "DELETE FROM AdminETS.dbo.tblbillinglines WHERE BillAccID ='11111111-1111-1111-1111-111111111111' AND Transcriptionid in (Select TranscriptionID FROM AdminSecureweb.dbo.tblTranscriptionClientMain where AccountID = '" & DRRec1("AccountID").ToString & "'  and datediff(day,datemodified, '" & edate & "') between 0 and datediff(day,'" & sdate & "','" & edate & "')  ) "
                                    cmdUp.CommandText = strQuery
                                    cmdUp.CommandType = CommandType.Text
                                    cmdUp.ExecuteNonQuery()
                                    strQuery = "Select * from AdminSecureweb.dbo.tblAccountsTAT where AccountID = '" & DRRec1("AccountID").ToString & "'  "
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
                                                strQuery = "Select * from AdminSecureweb.dbo.BillDetails where AccountID = '" & DRRec1("AccountID").ToString & "' and SubActID = '" & SubActID & "'   "
                                                '  'Response.Write(strQuery)
                                                Dim SQLCmd3 As New SqlCommand(strQuery, New SqlConnection(strConn))
                                                Try
                                                    SQLCmd3.Connection.Open()
                                                    Dim DRRec3 As SqlDataReader = SQLCmd3.ExecuteReader()
                                                    If DRRec3.HasRows = True Then
                                                        UDFound = True
                                                        If DRRec3.Read Then
                                                            ''Response.Write(strQuery)
                                                            strQuery = "Insert Into AdminSecureweb.dbo.tblAccBillDetails (BillAccID, AccountID, SubActID, SubActName, Rate, MiscRate, StatRate, LCMethodID,ModDate) Values ('" & BillAccID & "', '" & AccountID & "', '" & SubActID & "', '" & SubActName & "','" & DRRec3("Rate") & "', '" & DRRec3("MiscRate") & "', '" & DRRec3("StatRate") & "', '" & DRRec3("LCMethodID").ToString & "', '" & Now & "') "
                                                            cmdUp.CommandText = strQuery
                                                            cmdUp.CommandType = CommandType.Text
                                                            cmdUp.ExecuteNonQuery()

                                                            strQuery = "Select  * from AdminETS.dbo.tblLCMethodology where TrackID = '" & DRRec3("LCMethodID").ToString & "' "
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
                                                                        RptBIUOnOff = DRRec4("RptBIUOnOff").ToString
                                                                        RptShiftedAll = DRRec4("RptShiftedAll").ToString
                                                                        RptDocVariable = DRRec4("RptDocVariable").ToString
                                                                        RptShifted = DRRec4("RptShifted").ToString
                                                                        RptSpaces = DRRec4("RptSpaces").ToString
                                                                        RptSCT = DRRec4("RptSCT").ToString
                                                                        CharsPerLines = DRRec4("CharsPerLines")
                                                                        BIUVal = DRRec4("BIUVal")
                                                                        BIUShiftedAll = DRRec4("BIUShiftedAll")
                                                                        CountMethod = DRRec4("CountMethod").ToString

                                                                        If DRRec4("isdefault").ToString.ToUpper = "TRUE" Then
                                                                            DefaultLC = True
                                                                        Else
                                                                            DefaultLC = False
                                                                        End If

                                                                        strQuery = "Select distinct  IsNull(TB.Value, 0) AS TemplateDeduction, T.*, M.Type,M.TAT,CASE WHEN M.Priority = 1 AND (DATEDIFF(MINUTE, M.DateCreated, ISNULL(M.DateModified, GETDATE()))/60) < M.TAT THEN 'True' ELSE 'False' END as Priority,M.DictatorID,M.Jobnumber,   ROUND(CAST(datediff(s,0,M.duration) AS Float)*1.0/60,2) as Mins from AdminSecureweb.dbo.tblTranscriptionClientMain M Left Outer Join tblTransLC T ON T.TranscriptionID = M.TranscriptionID  LEFT OUTER JOIN tbltemplatesLinesDeductionsForBilling TB ON M.TemplateID = TB.TemplateID where T.TranscriptionID is not NULL and M.AccountID = '" & DRRec1("AccountID").ToString & "'   and datediff(day,M.datemodified, '" & edate & "') between 0 and datediff(day,'" & sdate & "','" & edate & "')   and M.TAT ='" & TAT & "' order by JobNumber"
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
                                                                                    stdTempCharDed = DRRec5("TemplateDeduction")
                                                                                    Rate = DRRec3("Rate")
                                                                                    If CountMethod = "PerDictator" Then
                                                                                        ReportLC = DRRec5("stdLC")
                                                                                    End If
                                                                                    ''Response.Write(DRRec5("transcriptionid").ToString & "#")
                                                                                    If CountMethod = "PerReport" Then
                                                                                        ReportLC = 1
                                                                                    ElseIf CountMethod = "Minutes" Then
                                                                                        If String.IsNullOrEmpty(DRRec5("Mins")) Then
                                                                                            ReportLC = 0
                                                                                        Else
                                                                                            ReportLC = DRRec5("Mins")
                                                                                        End If
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
                                                                                                If RptBIUOnOff = True Then
                                                                                                    ReportLC += DRRec5("NumBodyBIUOnOff") * BIUVal
                                                                                                End If

                                                                                                If RptShiftedAll = True Then
                                                                                                    ReportLC += DRRec5("NumBodyCharsWShiftAll") * BIUShiftedAll
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
                                                                                                If RptShiftedAll = True Then
                                                                                                    ReportLC += DRRec5("HeaderWShiftAll") * BIUShiftedAll
                                                                                                End If
                                                                                                If RptBIUOnOff = True Then
                                                                                                    ReportLC += DRRec5("HeaderBIUCountOnOff") * BIUVal
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
                                                                                                If RptBIUOnOff = True Then
                                                                                                    ReportLC += DRRec5("FooterBIUCountOnOff") * BIUVal
                                                                                                End If
                                                                                                If RptShiftedAll = True Then
                                                                                                    ReportLC += DRRec5("FooterWShiftAll") * BIUShiftedAll
                                                                                                End If
                                                                                                ''Response.Write(ReportLC & "#")

                                                                                            End If

                                                                                            If RptDocVariable = True Then
                                                                                                ReportLC += DRRec5("DocVarCount")
                                                                                            End If
                                                                                            ReportLC = ReportLC - stdTempCharDed
                                                                                            If ReportLC < 0 Then
                                                                                                ReportLC = 0
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

                                                                                    'Dim Row1 As New TableRow
                                                                                    'Dim Cell1 As New TableCell
                                                                                    'Dim Cell2 As New TableCell
                                                                                    'Dim Cell3 As New TableCell
                                                                                    'Dim Cell4 As New TableCell
                                                                                    'Dim Cell5 As New TableCell
                                                                                    'Dim Cell6 As New TableCell
                                                                                    'Dim Cell7 As New TableCell
                                                                                    'Dim Cell As New TableCell
                                                                                    'Cell.Text = i
                                                                                    'Cell1.Text = DRRec1("AccountName").ToString
                                                                                    'Cell2.Text = Trim(DRRec1("Mode").ToString)
                                                                                    'Cell3.Text = DRRec5("Jobnumber").ToString
                                                                                    'Cell4.Text = ReportLC
                                                                                    'Cell4.Text = ReportLC
                                                                                    'Cell5.Text = CountMethod
                                                                                    'Cell7.Text = SubActName
                                                                                    'Row1.Cells.Add(Cell)
                                                                                    'Row1.Cells.Add(Cell1)
                                                                                    'Row1.Cells.Add(Cell2)
                                                                                    'Row1.Cells.Add(Cell3)
                                                                                    'Row1.Cells.Add(Cell4)
                                                                                    'Row1.Cells.Add(Cell5)
                                                                                    'Row1.Cells.Add(Cell7)
                                                                                    'Table1.Rows.Add(Row1)
                                                                                    Amount = ReportLC * Rate
                                                                                    Type = DRRec5("Type").ToString



                                                                                    strQuery = "Insert Into AdminETS.dbo.tblBillingLines (BillAccID, SubActID, TranscriptionID, stdLCCount, unit, Rate, Amount, Type, Priority, updateDate) Values ('" & BillAccID & "', '" & SubActID & "', '" & TranscriptionID & "','" & stdLCCount & "', '" & ReportLC & "', '" & Rate & "', '" & Amount & "', '" & Type & "',  'False', '" & Now & "') "
                                                                                    cmdUp.CommandText = strQuery
                                                                                    cmdUp.CommandType = CommandType.Text
                                                                                    cmdUp.ExecuteNonQuery()


                                                                                    If DRRec5("Priority").ToString = "True" And Not CountMethod = "PerReport" Then
                                                                                        If StatChgPerReport Then
                                                                                            Rate = DRRec3("StatRate")
                                                                                            Amount = Rate
                                                                                            strQuery = "Insert Into AdminETS.dbo.tblBillingLines (BillAccID, SubActID, TranscriptionID, stdLCCount, unit, Rate, Amount, Type, Priority, updateDate) Values ('" & BillAccID & "', '" & SubActID & "', '" & TranscriptionID & "','" & stdLCCount & "', '" & 1 & "', '" & Rate & "', '" & Amount & "', '" & Type & "', 'True', '" & Now & "') "
                                                                                        Else
                                                                                            Rate = DRRec3("StatRate")
                                                                                            Amount = ReportLC * Rate
                                                                                            strQuery = "Insert Into AdminETS.dbo.tblBillingLines (BillAccID, SubActID, TranscriptionID, stdLCCount, unit, Rate, Amount, Type, Priority, updateDate) Values ('" & BillAccID & "', '" & SubActID & "', '" & TranscriptionID & "','" & stdLCCount & "', '" & ReportLC & "', '" & Rate & "', '" & Amount & "', '" & Type & "', 'True', '" & Now & "') "
                                                                                        End If
                                                                                       
                                                                                        cmdUp.CommandText = strQuery
                                                                                        cmdUp.CommandType = CommandType.Text
                                                                                        cmdUp.ExecuteNonQuery()

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
                                    If DLCycle.SelectedValue = 1 Then
                                        strQuery = "Delete from  AdminSecureweb.dbo.tblinvItemdet WHERE AccountID = '" & AccountID & "' and InvoiceID ='11111111-1111-1111-1111-111111111111'  and ISAuto=1 and servicedate='" & C1CurrEDate.AddDays(-1) & "' "
                                        cmdUp.CommandText = strQuery
                                        cmdUp.CommandType = CommandType.Text
                                        cmdUp.ExecuteNonQuery()
                                    Else
                                        strQuery = "Delete from  AdminSecureweb.dbo.tblinvItemdet WHERE AccountID = '" & AccountID & "' and InvoiceID ='11111111-1111-1111-1111-111111111111'  and ISAuto=1 and servicedate='" & C2CurrEDate & "' "
                                        cmdUp.CommandText = strQuery
                                        cmdUp.CommandType = CommandType.Text
                                        cmdUp.ExecuteNonQuery()
                                    End If
                                    If DLCycle.SelectedValue = 2 And DRRec1("IsAccFaxBilling").ToString And DRRec1("FaxPageCount") > 0 Then
                                        If Not DRRec1("AccFaxCharges") > 0 Then
                                            ErrFound = True
                                            ErrMessage = "Fax charges not set."
                                        End If
                                        If ErrFound = False Then
                                            strQuery = "Insert Into AdminSecureweb.dbo.tblinvItemdet (AccountID,InvoiceID, itemid, Descr, quantity, Amount,totamount,mode,servicedate, dateupdate, IsAuto) Values ('" & AccountID & "', '11111111-1111-1111-1111-111111111111', '548B3D50-3F0E-4A61-BFF9-115569721C55', 'Number of pages Faxed', '" & DRRec1("FaxPageCount") & "', convert(money," & DRRec1("AccFaxCharges") & "),  convert(money," & DRRec1("FaxPageCount") * DRRec1("AccFaxCharges") & "), 'VAS', '" & C2CurrEDate & "', '" & Now & "',1) "
                                            cmdUp.CommandText = strQuery
                                            cmdUp.CommandType = CommandType.Text
                                            cmdUp.ExecuteNonQuery()
                                        End If

                                    End If

                                    If DLCycle.SelectedValue = 2 And DRRec1("IsAccVPNBilling").ToString Then

                                        If Not DRRec1("AccVPNCharges") > 0 Then
                                            ErrFound = True
                                            ErrMessage = "VPN charges not set."
                                        End If
                                        If ErrFound = False Then
                                            strQuery = "Insert Into AdminSecureweb.dbo.tblinvItemdet (AccountID,InvoiceID, itemid, Descr, quantity, Amount,totamount,mode,servicedate, dateupdate, IsAuto) Values ('" & AccountID & "', '11111111-1111-1111-1111-111111111111', '781ED926-346B-48A2-80C0-9914D8ECC0BC', 'VPN Charges', '1', convert(money," & DRRec1("AccVPNCharges") & "),  convert(money," & DRRec1("AccVPNCharges") & "), 'VAS', '" & C2CurrEDate & "', '" & Now & "',1) "
                                            cmdUp.CommandText = strQuery
                                            cmdUp.CommandType = CommandType.Text
                                            cmdUp.ExecuteNonQuery()
                                        End If

                                    End If

                                    If DLCycle.SelectedValue = 2 And DRRec1("IsAccCutPasteBilling").ToString Then

                                        If Not DRRec1("AccCutPasteBilling") > 0 Then
                                            ErrFound = True
                                            ErrMessage = "Cut/Paste charges not set."
                                        End If
                                        If ErrFound = False Then
                                            strQuery = "Insert Into AdminSecureweb.dbo.tblinvItemdet (AccountID,InvoiceID, itemid, Descr, quantity, Amount,totamount,mode,servicedate, dateupdate, IsAuto) Values ('" & AccountID & "', '11111111-1111-1111-1111-111111111111', '16d2956b-e2c1-47b8-aff0-ad42b8d8cde8', 'Additional Cut and Paste charges', '1', convert(money," & DRRec1("AccCutPasteBilling") & "),  convert(money," & DRRec1("AccCutPasteBilling") & "), 'VAS', '" & C2CurrEDate & "', '" & Now & "',1) "
                                            cmdUp.CommandText = strQuery
                                            cmdUp.CommandType = CommandType.Text
                                            cmdUp.ExecuteNonQuery()
                                        End If

                                    End If
                                    If ErrFound = False Then
                                        MyTransAttr.Commit()
                                    Else
                                        MyTransAttr.Rollback()
                                    End If
                                Catch ex As Exception
                                    MyTransAttr.Rollback()
                                    ErrFound = True
                                    ErrMessage = ex.Message
                                Finally
                                    If myConnection.State = ConnectionState.Open Then
                                        myConnection.Close()
                                    End If
                                End Try
                            ElseIf Trim(DRRec1("Mode").ToString) = "DC" Then
                                Dim myConnection As New SqlConnection(strConn)
                                Dim MyTransAttr As SqlTransaction
                                ' Dim strQuery As String
                                Dim cmdUp As New SqlCommand()
                                myConnection.Open()
                                MyTransAttr = myConnection.BeginTransaction()
                                cmdUp.Connection = myConnection
                                cmdUp.Transaction = MyTransAttr
                                cmdUp.CommandTimeout = 600
                                Try
                                    strQuery = "Select distinct T.*, M.Type,M.Location,CASE WHEN M.Priority = 1 AND (DATEDIFF(MINUTE, M.DateCreated, ISNULL(M.DateModified, GETDATE()))/60) < M.TAT THEN 'True' ELSE 'False' END as Priority,M.DictatorID,M.Jobnumber,   ROUND(CAST(datediff(s,0,M.duration) AS Float)*1.0/60,2) as Mins from AdminSecureweb.dbo.tblTranscriptionClientMain M  Left Outer Join tblTransLC T ON T.TranscriptionID = M.TranscriptionID where T.TranscriptionID is not NULL and M.AccountID = '" & DRRec1("AccountID").ToString & "'   and datediff(day,M.datemodified, '" & edate & "') between 0 and datediff(day,'" & sdate & "','" & edate & "')   and M.dictatorID NOT IN (Select A.UserID FROM AdminSecureweb.dbo.AssignDic A INNER JOIN AdminSecureweb.dbo.GrpDictators G ON G.GrpDicID = A.GrpDicID where G.AccID = '" & DRRec1("AccountID").ToString & "')   "
                                    'Response.Write(strQuery)
                                    Dim SQLCmdChk1 As New SqlCommand(strQuery, New SqlConnection(strConn))
                                    Try
                                        SQLCmdChk1.Connection.Open()
                                        Dim DRRecChk1 As SqlDataReader = SQLCmdChk1.ExecuteReader()
                                        If DRRecChk1.HasRows = True Then
                                            ErrMessage = "Dictator(s) was not added."
                                            ErrFound = True
                                        End If
                                    Catch ex As Exception
                                        ErrMessage = ex.Message
                                        ErrFound = True
                                    Finally
                                        If SQLCmdChk1.Connection.State = System.Data.ConnectionState.Open Then
                                            SQLCmdChk1.Connection.Close()
                                            SQLCmdChk1 = Nothing
                                        End If
                                    End Try
                                    If ErrFound = False Then
                                        strQuery = "Select * from AdminSecureweb.dbo.GrpDictators A LEFT OUTER JOIN AdminSecureweb.dbo.BillDetails B ON A.AccID= B.AccountID and A.GrpDicID = B.SubActID where A.AccID = '" & DRRec1("AccountID").ToString & "' and B.SubActID IS NULL  "
                                        ' Response.Write(strQuery)
                                        Dim SQLCmdChk2 As New SqlCommand(strQuery, New SqlConnection(strConn))
                                        Try
                                            SQLCmdChk2.Connection.Open()
                                            Dim DRRecChk2 As SqlDataReader = SQLCmdChk2.ExecuteReader()
                                            If DRRecChk2.HasRows = True Then
                                                ErrMessage = "Rate was not set for Group Dictator(s)."
                                                ErrFound = True
                                            End If
                                        Catch ex As Exception
                                            ErrMessage = ex.Message
                                            ErrFound = True
                                        Finally
                                            If SQLCmdChk2.Connection.State = System.Data.ConnectionState.Open Then
                                                SQLCmdChk2.Connection.Close()
                                                SQLCmdChk2 = Nothing
                                            End If
                                        End Try
                                    End If
                                    If ErrFound = False Then
                                        Dim LocCode As Integer
                                        strQuery = "Select NewID() as UID"
                                        Dim SQLCmd6 As New SqlCommand(strQuery, New SqlConnection(strConn))
                                        Try
                                            SQLCmd6.Connection.Open()
                                            BillAccID = SQLCmd6.ExecuteScalar.ToString
                                        Finally
                                            If SQLCmd6.Connection.State = ConnectionState.Open Then
                                                SQLCmd6.Connection.Close()
                                            End If
                                        End Try
                                        AccountID = DRRec1("AccountID").ToString
                                        Cycle = DRRec1("Cycle").ToString
                                        MinBilling = DRRec1("MinBilling").ToString
                                        BillMonth = DLMonth.SelectedValue
                                        BillYear = DLYear.SelectedValue
                                        Mode = Trim(DRRec1("Mode").ToString)

                                        ''Response.Write(BillAccID)
                                        strQuery = "Insert Into AdminSecureweb.dbo.tblBillAccounts (BillAccID, AccountID, Cycle, MinBilling, BillMonth, BillYear, BillCycle, Mode, ModDate) Values ('" & BillAccID & "', '" & AccountID & "','" & Cycle & "', '" & MinBilling & "', '" & BillMonth & "', '" & BillYear & "', '" & DLCycle.SelectedValue & "', '" & Mode & "', '" & Now & "') "
                                        cmdUp.CommandText = strQuery
                                        cmdUp.CommandType = CommandType.Text
                                        cmdUp.ExecuteNonQuery()

                                        strQuery = "DELETE FROM AdminETS.dbo.tblbillinglines WHERE BillAccID ='11111111-1111-1111-1111-111111111111' AND Transcriptionid in (Select TranscriptionID FROM AdminSecureweb.dbo.tblTranscriptionClientMain where AccountID = '" & DRRec1("AccountID").ToString & "'  and datediff(day,datemodified, '" & edate & "') between 0 and datediff(day,'" & sdate & "','" & edate & "')  ) "
                                        cmdUp.CommandText = strQuery
                                        cmdUp.CommandType = CommandType.Text
                                        cmdUp.ExecuteNonQuery()

                                        strQuery = "Select * from AdminSecureweb.dbo.GrpDictators where AccID = '" & DRRec1("AccountID").ToString & "'  "
                                        Dim SQLCmdLC As New SqlCommand(strQuery, New SqlConnection(strConn))
                                        Try
                                            SQLCmdLC.Connection.Open()
                                            Dim DRRecLC As SqlDataReader = SQLCmdLC.ExecuteReader()
                                            If DRRecLC.HasRows = True Then
                                                While DRRecLC.Read
                                                    SubActID = DRRecLC("GrpDicID").ToString
                                                    SubActName = DRRecLC("GrpDicName").ToString

                                                    strQuery = "Select * from AdminSecureweb.dbo.BillDetails where AccountID = '" & DRRec1("AccountID").ToString & "' and SubActID = '" & SubActID & "'   "
                                                    Dim SQLCmd3 As New SqlCommand(strQuery, New SqlConnection(strConn))
                                                    Try
                                                        SQLCmd3.Connection.Open()
                                                        Dim DRRec3 As SqlDataReader = SQLCmd3.ExecuteReader()
                                                        If DRRec3.HasRows = True Then
                                                            UDFound = True
                                                            If DRRec3.Read Then


                                                                ''Response.Write(strQuery)
                                                                strQuery = "Insert Into AdminSecureweb.dbo.tblAccBillDetails (BillAccID, AccountID, SubActID, SubActName, Rate, MiscRate, StatRate, LCMethodID,ModDate) Values ('" & BillAccID & "', '" & AccountID & "', '" & SubActID & "', '" & SubActName & "','" & DRRec3("Rate") & "', '" & DRRec3("MiscRate") & "', '" & DRRec3("StatRate") & "', '" & DRRec3("LCMethodID").ToString & "', '" & Now & "') "
                                                                cmdUp.CommandText = strQuery
                                                                cmdUp.CommandType = CommandType.Text
                                                                cmdUp.ExecuteNonQuery()


                                                                strQuery = "Select  * from AdminETS.dbo.tblLCMethodology where TrackID = '" & DRRec3("LCMethodID").ToString & "' "
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
                                                                            RptBIUOnOff = DRRec4("RptBIUOnOff").ToString
                                                                            RptShiftedAll = DRRec4("RptShiftedAll").ToString
                                                                            RptDocVariable = DRRec4("RptDocVariable").ToString
                                                                            RptShifted = DRRec4("RptShifted").ToString
                                                                            RptSpaces = DRRec4("RptSpaces").ToString
                                                                            RptSCT = DRRec4("RptSCT").ToString
                                                                            CharsPerLines = DRRec4("CharsPerLines")
                                                                            BIUVal = DRRec4("BIUVal")
                                                                            BIUShiftedAll = DRRec4("BIUShiftedAll")
                                                                            CountMethod = DRRec4("CountMethod").ToString

                                                                            If DRRec4("isdefault").ToString.ToUpper = "TRUE" Then
                                                                                DefaultLC = True
                                                                            Else
                                                                                DefaultLC = False
                                                                            End If
                                                                            strQuery = "Select distinct IsNull(TB.Value, 0) AS TemplateDeduction, T.*, M.Type,M.Location,CASE WHEN M.Priority = 1 AND (DATEDIFF(MINUTE, M.DateCreated, ISNULL(M.DateModified, GETDATE()))/60) < M.TAT THEN 'True' ELSE 'False' END as Priority,M.DictatorID,M.Jobnumber,   ROUND(CAST(datediff(s,0,M.duration) AS Float)*1.0/60,2) as Mins from AdminSecureweb.dbo.tblTranscriptionClientMain M INNER JOIN AdminSecureweb.dbo.AssignDic A ON A.UserID = M.dictatorID  Left Outer Join tblTransLC T ON T.TranscriptionID = M.TranscriptionID LEFT OUTER JOIN tbltemplatesLinesDeductionsForBilling TB ON M.TemplateID = TB.TemplateID  where T.TranscriptionID is not NULL and A.AccID = '" & DRRec1("AccountID").ToString & "'   and datediff(day,M.datemodified, '" & edate & "') between 0 and datediff(day,'" & sdate & "','" & edate & "')   and A.GrpDicID = '" & SubActID & "' order by JobNumber"
                                                                            'strQuery = "Select distinct * from AdminSecureweb.dbo.tblTranscriptionClientMain M INNER JOIN AdminSecureweb.dbo.AssignDic A ON A.UserID = M.dictatorID and A.AccID = M.AccountID Left Outer Join tblTransLC T ON T.TranscriptionID = M.TranscriptionID where T.TranscriptionID is not NULL and M.AccountID = '" & DRRec1("AccountID").ToString & "'   and datediff(day,M.datemodified, '7/31/2010') between 0 and  30  and A.GrpDicID = '" & SubActID & "' order by JobNumber"
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
                                                                                        stdTempCharDed = DRRec5("TemplateDeduction")
                                                                                        Rate = DRRec3("Rate")


                                                                                        If CountMethod = "PerDictator" Then
                                                                                            ReportLC = DRRec5("stdLC")
                                                                                            Rate = 0
                                                                                        End If

                                                                                        ''Response.Write(DRRec5("transcriptionid").ToString & "#")
                                                                                        If CountMethod = "PerReport" Then
                                                                                            ReportLC = 1
                                                                                        ElseIf CountMethod = "Minutes" Then
                                                                                            If String.IsNullOrEmpty(DRRec5("Mins")) Then
                                                                                                ReportLC = 0
                                                                                            Else
                                                                                                ReportLC = DRRec5("Mins")
                                                                                            End If
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
                                                                                                    If RptBIUOnOff = True Then
                                                                                                        ReportLC += DRRec5("NumBodyBIUOnOff") * BIUVal
                                                                                                    End If

                                                                                                    If RptShiftedAll = True Then
                                                                                                        ReportLC += DRRec5("NumBodyCharsWShiftAll") * BIUShiftedAll
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
                                                                                                    If RptShiftedAll = True Then
                                                                                                        ReportLC += DRRec5("HeaderWShiftAll") * BIUShiftedAll
                                                                                                    End If
                                                                                                    If RptBIUOnOff = True Then
                                                                                                        ReportLC += DRRec5("HeaderBIUCountOnOff") * BIUVal
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
                                                                                                    If RptBIUOnOff = True Then
                                                                                                        ReportLC += DRRec5("FooterBIUCountOnOff") * BIUVal
                                                                                                    End If
                                                                                                    If RptShiftedAll = True Then
                                                                                                        ReportLC += DRRec5("FooterWShiftAll") * BIUShiftedAll
                                                                                                    End If
                                                                                                    ''Response.Write(ReportLC & "#")

                                                                                                End If
                                                                                                If RptDocVariable = True Then
                                                                                                    ReportLC += DRRec5("DocVarCount")
                                                                                                End If
                                                                                                ReportLC = ReportLC - stdTempCharDed
                                                                                                If ReportLC < 0 Then
                                                                                                    ReportLC = 0
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

                                                                                        'Dim Row1 As New TableRow
                                                                                        'Dim Cell1 As New TableCell
                                                                                        'Dim Cell2 As New TableCell
                                                                                        'Dim Cell3 As New TableCell
                                                                                        'Dim Cell4 As New TableCell
                                                                                        'Dim Cell5 As New TableCell
                                                                                        'Dim Cell6 As New TableCell
                                                                                        'Dim Cell7 As New TableCell
                                                                                        'Dim Cell As New TableCell
                                                                                        'Cell.Text = i
                                                                                        'Cell1.Text = DRRec1("AccountName").ToString
                                                                                        'Cell2.Text = Trim(DRRec1("Mode").ToString)
                                                                                        'Cell3.Text = DRRec5("Jobnumber").ToString
                                                                                        'Cell4.Text = ReportLC
                                                                                        'Cell4.Text = ReportLC
                                                                                        'Cell5.Text = CountMethod
                                                                                        'Cell7.Text = SubActName
                                                                                        'Row1.Cells.Add(Cell)
                                                                                        'Row1.Cells.Add(Cell1)
                                                                                        'Row1.Cells.Add(Cell2)
                                                                                        'Row1.Cells.Add(Cell3)
                                                                                        'Row1.Cells.Add(Cell4)
                                                                                        'Row1.Cells.Add(Cell5)
                                                                                        'Row1.Cells.Add(Cell7)
                                                                                        'Table1.Rows.Add(Row1)
                                                                                        Amount = ReportLC * Rate
                                                                                        Type = DRRec5("Type").ToString

                                                                                        strQuery = "Insert Into AdminETS.dbo.tblBillingLines (BillAccID, SubActID, TranscriptionID, stdLCCount, unit, Rate, Amount, Type, Priority, updateDate) Values ('" & BillAccID & "', '" & SubActID & "', '" & TranscriptionID & "','" & stdLCCount & "', '" & ReportLC & "', '" & Rate & "', '" & Amount & "', '" & Type & "', 'False', '" & Now & "') "
                                                                                        cmdUp.CommandText = strQuery
                                                                                        cmdUp.CommandType = CommandType.Text
                                                                                        cmdUp.ExecuteNonQuery()


                                                                                        If DRRec5("Priority").ToString = "True" And Not CountMethod = "PerReport" Then

                                                                                            If StatChgPerReport Then
                                                                                                Rate = DRRec3("StatRate")
                                                                                                Amount = Rate
                                                                                                strQuery = "Insert Into AdminETS.dbo.tblBillingLines (BillAccID, SubActID, TranscriptionID, stdLCCount, unit, Rate, Amount, Type, Priority, updateDate) Values ('" & BillAccID & "', '" & SubActID & "', '" & TranscriptionID & "','" & stdLCCount & "', '" & 1 & "', '" & Rate & "', '" & Amount & "', '" & Type & "', 'True', '" & Now & "') "
                                                                                            Else
                                                                                                Rate = DRRec3("StatRate")
                                                                                                Amount = ReportLC * Rate
                                                                                                strQuery = "Insert Into AdminETS.dbo.tblBillingLines (BillAccID, SubActID, TranscriptionID, stdLCCount, unit, Rate, Amount, Type, Priority, updateDate) Values ('" & BillAccID & "', '" & SubActID & "', '" & TranscriptionID & "','" & stdLCCount & "', '" & ReportLC & "', '" & Rate & "', '" & Amount & "', '" & Type & "', 'True', '" & Now & "') "
                                                                                            End If

                                                                                            cmdUp.CommandText = strQuery
                                                                                            cmdUp.CommandType = CommandType.Text
                                                                                            cmdUp.ExecuteNonQuery()
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
                                    If DLCycle.SelectedValue = 1 Then
                                        strQuery = "Delete from  AdminSecureweb.dbo.tblinvItemdet WHERE AccountID = '" & AccountID & "' and InvoiceID ='11111111-1111-1111-1111-111111111111'  and ISAuto=1 and servicedate='" & C1CurrEDate.AddDays(-1) & "' "
                                        cmdUp.CommandText = strQuery
                                        cmdUp.CommandType = CommandType.Text
                                        cmdUp.ExecuteNonQuery()
                                    Else
                                        strQuery = "Delete from  AdminSecureweb.dbo.tblinvItemdet WHERE AccountID = '" & AccountID & "' and InvoiceID ='11111111-1111-1111-1111-111111111111'  and ISAuto=1 and servicedate='" & C2CurrEDate & "' "
                                        cmdUp.CommandText = strQuery
                                        cmdUp.CommandType = CommandType.Text
                                        cmdUp.ExecuteNonQuery()
                                    End If
                                    If DLCycle.SelectedValue = 2 And DRRec1("IsAccFaxBilling").ToString And DRRec1("FaxPageCount") > 0 Then
                                        If Not DRRec1("AccFaxCharges") > 0 Then
                                            ErrFound = True
                                            ErrMessage = "Fax charges not set."
                                        End If
                                        If ErrFound = False Then
                                            strQuery = "Insert Into AdminSecureweb.dbo.tblinvItemdet (AccountID,InvoiceID, itemid, Descr, quantity, Amount,totamount,mode,servicedate, dateupdate, IsAuto) Values ('" & AccountID & "', '11111111-1111-1111-1111-111111111111', '548B3D50-3F0E-4A61-BFF9-115569721C55', 'Number of pages Faxed', '" & DRRec1("FaxPageCount") & "', convert(money," & DRRec1("AccFaxCharges") & "),  convert(money," & DRRec1("FaxPageCount") * DRRec1("AccFaxCharges") & "), 'VAS', '" & C2CurrEDate & "', '" & Now & "',1) "
                                            cmdUp.CommandText = strQuery
                                            cmdUp.CommandType = CommandType.Text
                                            cmdUp.ExecuteNonQuery()
                                        End If

                                    End If

                                    If DLCycle.SelectedValue = 2 And DRRec1("IsAccVPNBilling").ToString Then

                                        If Not DRRec1("AccVPNCharges") > 0 Then
                                            ErrFound = True
                                            ErrMessage = "VPN charges not set."
                                        End If
                                        If ErrFound = False Then
                                            strQuery = "Insert Into AdminSecureweb.dbo.tblinvItemdet (AccountID,InvoiceID, itemid, Descr, quantity, Amount,totamount,mode,servicedate, dateupdate, IsAuto) Values ('" & AccountID & "', '11111111-1111-1111-1111-111111111111', '781ED926-346B-48A2-80C0-9914D8ECC0BC', 'VPN Charges', '1', convert(money," & DRRec1("AccVPNCharges") & "),  convert(money," & DRRec1("AccVPNCharges") & "), 'VAS', '" & C2CurrEDate & "', '" & Now & "',1) "
                                            cmdUp.CommandText = strQuery
                                            cmdUp.CommandType = CommandType.Text
                                            cmdUp.ExecuteNonQuery()
                                        End If

                                    End If

                                    If DLCycle.SelectedValue = 2 And DRRec1("IsAccCutPasteBilling").ToString Then

                                        If Not DRRec1("AccCutPasteBilling") > 0 Then
                                            ErrFound = True
                                            ErrMessage = "Cut/Paste charges not set."
                                        End If
                                        If ErrFound = False Then
                                            strQuery = "Insert Into AdminSecureweb.dbo.tblinvItemdet (AccountID,InvoiceID, itemid, Descr, quantity, Amount,totamount,mode,servicedate, dateupdate, IsAuto) Values ('" & AccountID & "', '11111111-1111-1111-1111-111111111111', '16d2956b-e2c1-47b8-aff0-ad42b8d8cde8', 'Additional Cut and Paste charges', '1', convert(money," & DRRec1("AccCutPasteBilling") & "),  convert(money," & DRRec1("AccCutPasteBilling") & "), 'VAS', '" & C2CurrEDate & "', '" & Now & "',1) "
                                            cmdUp.CommandText = strQuery
                                            cmdUp.CommandType = CommandType.Text
                                            cmdUp.ExecuteNonQuery()
                                        End If

                                    End If
                                    If ErrFound = False Then
                                        MyTransAttr.Commit()
                                    Else
                                        MyTransAttr.Rollback()
                                    End If
                                Catch ex As Exception
                                    MyTransAttr.Rollback()
                                    ErrFound = True
                                    ErrMessage = ex.Message
                                Finally
                                    If myConnection.State = ConnectionState.Open Then
                                        myConnection.Close()
                                    End If
                                End Try
                            ElseIf Trim(DRRec1("Mode").ToString) = "TW" Then
                                Dim myConnection As New SqlConnection(strConn)
                                Dim MyTransAttr As SqlTransaction
                                ' Dim strQuery As String
                                Dim cmdUp As New SqlCommand()
                                myConnection.Open()
                                MyTransAttr = myConnection.BeginTransaction()
                                cmdUp.Connection = myConnection
                                cmdUp.Transaction = MyTransAttr
                                cmdUp.CommandTimeout = 600
                                Try
                                    strQuery = "Select distinct T.*, M.Type,M.Location,CASE WHEN M.Priority = 1 AND (DATEDIFF(MINUTE, M.DateCreated, ISNULL(M.DateModified, GETDATE()))/60) < M.TAT THEN 'True' ELSE 'False' END as Priority,M.DictatorID,M.Jobnumber,   ROUND(CAST(datediff(s,0,M.duration) AS Float)*1.0/60,2) as Mins from AdminSecureweb.dbo.tblTranscriptionClientMain M  Left Outer Join tblTransLC T ON T.TranscriptionID = M.TranscriptionID where T.TranscriptionID is not NULL and M.AccountID = '" & DRRec1("AccountID").ToString & "'   and datediff(day,M.datemodified, '" & edate & "') between 0 and datediff(day,'" & sdate & "','" & edate & "')   and M.TemplateID NOT IN (Select A.TemplateID FROM AdminSecureweb.dbo.tblAssignTemplates A INNER JOIN AdminSecureweb.dbo.tblGrpTemplates G ON G.GrpTempID = A.GrpTempID where G.AccID = '" & DRRec1("AccountID").ToString & "')   "
                                    'Response.Write(strQuery)
                                    Dim SQLCmdChk1 As New SqlCommand(strQuery, New SqlConnection(strConn))
                                    Try
                                        SQLCmdChk1.Connection.Open()
                                        Dim DRRecChk1 As SqlDataReader = SQLCmdChk1.ExecuteReader()
                                        If DRRecChk1.HasRows = True Then
                                            ErrMessage = "Templates(s) was not added."
                                            ErrFound = True
                                        End If
                                    Catch ex As Exception
                                        ErrMessage = ex.Message
                                        ErrFound = True
                                    Finally
                                        If SQLCmdChk1.Connection.State = System.Data.ConnectionState.Open Then
                                            SQLCmdChk1.Connection.Close()
                                            SQLCmdChk1 = Nothing
                                        End If
                                    End Try
                                    If ErrFound = False Then
                                        strQuery = "Select * from AdminSecureweb.dbo.tblGrpTemplates A LEFT OUTER JOIN AdminSecureweb.dbo.BillDetails B ON A.AccID= B.AccountID and A.GrpTempID = B.SubActID where A.AccID = '" & DRRec1("AccountID").ToString & "' and B.SubActID IS NULL  "
                                        ' Response.Write(strQuery)
                                        Dim SQLCmdChk2 As New SqlCommand(strQuery, New SqlConnection(strConn))
                                        Try
                                            SQLCmdChk2.Connection.Open()
                                            Dim DRRecChk2 As SqlDataReader = SQLCmdChk2.ExecuteReader()
                                            If DRRecChk2.HasRows = True Then
                                                ErrMessage = "Rate was not set for Group Templates(s)."
                                                ErrFound = True
                                            End If
                                        Catch ex As Exception
                                            ErrMessage = ex.Message
                                            ErrFound = True
                                        Finally
                                            If SQLCmdChk2.Connection.State = System.Data.ConnectionState.Open Then
                                                SQLCmdChk2.Connection.Close()
                                                SQLCmdChk2 = Nothing
                                            End If
                                        End Try
                                    End If
                                    If ErrFound = False Then
                                        Dim LocCode As Integer
                                        strQuery = "Select NewID() as UID"
                                        Dim SQLCmd6 As New SqlCommand(strQuery, New SqlConnection(strConn))
                                        Try
                                            SQLCmd6.Connection.Open()
                                            BillAccID = SQLCmd6.ExecuteScalar.ToString
                                        Finally
                                            If SQLCmd6.Connection.State = ConnectionState.Open Then
                                                SQLCmd6.Connection.Close()
                                            End If
                                        End Try
                                        AccountID = DRRec1("AccountID").ToString
                                        Cycle = DRRec1("Cycle").ToString
                                        MinBilling = DRRec1("MinBilling").ToString
                                        BillMonth = DLMonth.SelectedValue
                                        BillYear = DLYear.SelectedValue
                                        Mode = Trim(DRRec1("Mode").ToString)

                                        'Response.Write(BillAccID)
                                        strQuery = "Insert Into AdminSecureweb.dbo.tblBillAccounts (BillAccID, AccountID, Cycle, MinBilling, BillMonth, BillYear, BillCycle, Mode, ModDate) Values ('" & BillAccID & "', '" & AccountID & "','" & Cycle & "', '" & MinBilling & "', '" & BillMonth & "', '" & BillYear & "', '" & DLCycle.SelectedValue & "', '" & Mode & "', '" & Now & "') "
                                        cmdUp.CommandText = strQuery
                                        cmdUp.CommandType = CommandType.Text
                                        cmdUp.ExecuteNonQuery()

                                        strQuery = "DELETE FROM AdminETS.dbo.tblbillinglines WHERE BillAccID ='11111111-1111-1111-1111-111111111111' AND Transcriptionid in (Select TranscriptionID FROM AdminSecureweb.dbo.tblTranscriptionClientMain where AccountID = '" & DRRec1("AccountID").ToString & "'  and datediff(day,datemodified, '" & edate & "') between 0 and datediff(day,'" & sdate & "','" & edate & "')  ) "
                                        cmdUp.CommandText = strQuery
                                        cmdUp.CommandType = CommandType.Text
                                        cmdUp.ExecuteNonQuery()

                                        strQuery = "Select * from AdminSecureweb.dbo.tblGrpTemplates where AccID = '" & DRRec1("AccountID").ToString & "'  "
                                        Dim SQLCmdLC As New SqlCommand(strQuery, New SqlConnection(strConn))
                                        Try
                                            SQLCmdLC.Connection.Open()
                                            Dim DRRecLC As SqlDataReader = SQLCmdLC.ExecuteReader()
                                            If DRRecLC.HasRows = True Then
                                                While DRRecLC.Read
                                                    SubActID = DRRecLC("GrpTempID").ToString
                                                    SubActName = DRRecLC("GrpTempName").ToString

                                                    strQuery = "Select * from AdminSecureweb.dbo.BillDetails where AccountID = '" & DRRec1("AccountID").ToString & "' and SubActID = '" & SubActID & "'   "
                                                    Dim SQLCmd3 As New SqlCommand(strQuery, New SqlConnection(strConn))
                                                    Try
                                                        SQLCmd3.Connection.Open()
                                                        Dim DRRec3 As SqlDataReader = SQLCmd3.ExecuteReader()
                                                        If DRRec3.HasRows = True Then
                                                            UDFound = True
                                                            If DRRec3.Read Then


                                                                ''Response.Write(strQuery)
                                                                strQuery = "Insert Into AdminSecureweb.dbo.tblAccBillDetails (BillAccID, AccountID, SubActID, SubActName, Rate, MiscRate, StatRate, LCMethodID,ModDate) Values ('" & BillAccID & "', '" & AccountID & "', '" & SubActID & "', '" & SubActName & "','" & DRRec3("Rate") & "', '" & DRRec3("MiscRate") & "', '" & DRRec3("StatRate") & "', '" & DRRec3("LCMethodID").ToString & "', '" & Now & "') "
                                                                cmdUp.CommandText = strQuery
                                                                cmdUp.CommandType = CommandType.Text
                                                                cmdUp.ExecuteNonQuery()


                                                                strQuery = "Select  * from AdminETS.dbo.tblLCMethodology where TrackID = '" & DRRec3("LCMethodID").ToString & "' "
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
                                                                            RptBIUOnOff = DRRec4("RptBIUOnOff").ToString
                                                                            RptShiftedAll = DRRec4("RptShiftedAll").ToString
                                                                            RptDocVariable = DRRec4("RptDocVariable").ToString
                                                                            RptShifted = DRRec4("RptShifted").ToString
                                                                            RptSpaces = DRRec4("RptSpaces").ToString
                                                                            RptSCT = DRRec4("RptSCT").ToString
                                                                            CharsPerLines = DRRec4("CharsPerLines")
                                                                            BIUVal = DRRec4("BIUVal")
                                                                            BIUShiftedAll = DRRec4("BIUShiftedAll")
                                                                            CountMethod = DRRec4("CountMethod").ToString

                                                                            If DRRec4("isdefault").ToString.ToUpper = "TRUE" Then
                                                                                DefaultLC = True
                                                                            Else
                                                                                DefaultLC = False
                                                                            End If
                                                                            strQuery = "Select distinct IsNull(TB.Value, 0) AS TemplateDeduction,  T.*, M.Type,M.Location,CASE WHEN M.Priority = 1 AND (DATEDIFF(MINUTE, M.DateCreated, ISNULL(M.DateModified, GETDATE()))/60) < M.TAT THEN 'True' ELSE 'False' END as Priority,M.DictatorID,M.Jobnumber,   ROUND(CAST(datediff(s,0,M.duration) AS Float)*1.0/60,2) as Mins from AdminSecureweb.dbo.tblTranscriptionClientMain M INNER JOIN AdminSecureweb.dbo.tblAssignTemplates A ON A.TemplateID = M.TemplateID and A.AccID = M.AccountID Left Outer Join tblTransLC T ON T.TranscriptionID = M.TranscriptionID LEFT OUTER JOIN tbltemplatesLinesDeductionsForBilling TB ON M.TemplateID = TB.TemplateID  where T.TranscriptionID is not NULL and M.AccountID = '" & DRRec1("AccountID").ToString & "'   and datediff(day,M.datemodified, '" & edate & "') between 0 and datediff(day,'" & sdate & "','" & edate & "')   and A.GrpTempID = '" & SubActID & "' order by JobNumber"
                                                                            'strQuery = "Select distinct * from AdminSecureweb.dbo.tblTranscriptionClientMain M INNER JOIN AdminSecureweb.dbo.AssignDic A ON A.UserID = M.dictatorID and A.AccID = M.AccountID Left Outer Join tblTransLC T ON T.TranscriptionID = M.TranscriptionID where T.TranscriptionID is not NULL and M.AccountID = '" & DRRec1("AccountID").ToString & "'   and datediff(day,M.datemodified, '7/31/2010') between 0 and  30  and A.GrpDicID = '" & SubActID & "' order by JobNumber"
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
                                                                                        stdTempCharDed = DRRec5("TemplateDeduction")
                                                                                        Rate = DRRec3("Rate")

                                                                                        'If CountMethod = "CharsPerLine" Then
                                                                                        '    ReportLC = DRRec5("stdLC")
                                                                                        'End If
                                                                                        If CountMethod = "PerDictator" Then
                                                                                            ReportLC = DRRec5("stdLC")
                                                                                        End If

                                                                                        ''Response.Write(DRRec5("transcriptionid").ToString & "#")
                                                                                        If CountMethod = "PerReport" Then
                                                                                            ReportLC = 1
                                                                                        ElseIf CountMethod = "Minutes" Then
                                                                                            If String.IsNullOrEmpty(DRRec5("Mins")) Then
                                                                                                ReportLC = 0
                                                                                            Else
                                                                                                ReportLC = DRRec5("Mins")
                                                                                            End If
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
                                                                                                    If RptBIUOnOff = True Then
                                                                                                        ReportLC += DRRec5("NumBodyBIUOnOff") * BIUVal
                                                                                                    End If

                                                                                                    If RptShiftedAll = True Then
                                                                                                        ReportLC += DRRec5("NumBodyCharsWShiftAll") * BIUShiftedAll
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
                                                                                                    If RptShiftedAll = True Then
                                                                                                        ReportLC += DRRec5("HeaderWShiftAll") * BIUShiftedAll
                                                                                                    End If
                                                                                                    If RptBIUOnOff = True Then
                                                                                                        ReportLC += DRRec5("HeaderBIUCountOnOff") * BIUVal
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
                                                                                                    If RptBIUOnOff = True Then
                                                                                                        ReportLC += DRRec5("FooterBIUCountOnOff") * BIUVal
                                                                                                    End If
                                                                                                    If RptShiftedAll = True Then
                                                                                                        ReportLC += DRRec5("FooterWShiftAll") * BIUShiftedAll
                                                                                                    End If
                                                                                                    ''Response.Write(ReportLC & "#")

                                                                                                End If
                                                                                                If RptDocVariable = True Then
                                                                                                    ReportLC += DRRec5("DocVarCount")
                                                                                                End If
                                                                                                ReportLC = ReportLC - stdTempCharDed
                                                                                                If ReportLC < 0 Then
                                                                                                    ReportLC = 0
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

                                                                                        'Dim Row1 As New TableRow
                                                                                        'Dim Cell1 As New TableCell
                                                                                        'Dim Cell2 As New TableCell
                                                                                        'Dim Cell3 As New TableCell
                                                                                        'Dim Cell4 As New TableCell
                                                                                        'Dim Cell5 As New TableCell
                                                                                        'Dim Cell6 As New TableCell
                                                                                        'Dim Cell7 As New TableCell
                                                                                        'Dim Cell As New TableCell
                                                                                        'Cell.Text = i
                                                                                        'Cell1.Text = DRRec1("AccountName").ToString
                                                                                        'Cell2.Text = Trim(DRRec1("Mode").ToString)
                                                                                        'Cell3.Text = DRRec5("Jobnumber").ToString
                                                                                        'Cell4.Text = ReportLC
                                                                                        'Cell4.Text = ReportLC
                                                                                        'Cell5.Text = CountMethod
                                                                                        'Cell7.Text = SubActName
                                                                                        'Row1.Cells.Add(Cell)
                                                                                        'Row1.Cells.Add(Cell1)
                                                                                        'Row1.Cells.Add(Cell2)
                                                                                        'Row1.Cells.Add(Cell3)
                                                                                        'Row1.Cells.Add(Cell4)
                                                                                        'Row1.Cells.Add(Cell5)
                                                                                        'Row1.Cells.Add(Cell7)
                                                                                        'Table1.Rows.Add(Row1)
                                                                                        Amount = ReportLC * Rate
                                                                                        Type = DRRec5("Type").ToString

                                                                                        strQuery = "Insert Into AdminETS.dbo.tblBillingLines (BillAccID, SubActID, TranscriptionID, stdLCCount, unit, Rate, Amount, Type, Priority, updateDate) Values ('" & BillAccID & "', '" & SubActID & "', '" & TranscriptionID & "','" & stdLCCount & "', '" & ReportLC & "', '" & Rate & "', '" & Amount & "', '" & Type & "', 'False', '" & Now & "') "
                                                                                        cmdUp.CommandText = strQuery
                                                                                        cmdUp.CommandType = CommandType.Text
                                                                                        cmdUp.ExecuteNonQuery()

                                                                                        If DRRec5("Priority").ToString = "True" And Not CountMethod = "PerReport" Then
                                                                                            If StatChgPerReport Then
                                                                                                Rate = DRRec3("StatRate")
                                                                                                Amount = Rate
                                                                                                strQuery = "Insert Into AdminETS.dbo.tblBillingLines (BillAccID, SubActID, TranscriptionID, stdLCCount, unit, Rate, Amount, Type, Priority, updateDate) Values ('" & BillAccID & "', '" & SubActID & "', '" & TranscriptionID & "','" & stdLCCount & "', '" & 1 & "', '" & Rate & "', '" & Amount & "', '" & Type & "', 'True', '" & Now & "') "
                                                                                            Else
                                                                                                Rate = DRRec3("StatRate")
                                                                                                Amount = ReportLC * Rate
                                                                                                strQuery = "Insert Into AdminETS.dbo.tblBillingLines (BillAccID, SubActID, TranscriptionID, stdLCCount, unit, Rate, Amount, Type, Priority, updateDate) Values ('" & BillAccID & "', '" & SubActID & "', '" & TranscriptionID & "','" & stdLCCount & "', '" & ReportLC & "', '" & Rate & "', '" & Amount & "', '" & Type & "', 'True', '" & Now & "') "
                                                                                            End If
                                                                                            
                                                                                            cmdUp.CommandText = strQuery
                                                                                            cmdUp.CommandType = CommandType.Text
                                                                                            cmdUp.ExecuteNonQuery()
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
                                    If DLCycle.SelectedValue = 1 Then
                                        strQuery = "Delete from  AdminSecureweb.dbo.tblinvItemdet WHERE AccountID = '" & AccountID & "' and InvoiceID ='11111111-1111-1111-1111-111111111111'  and ISAuto=1 and servicedate='" & C1CurrEDate.AddDays(-1) & "' "
                                        cmdUp.CommandText = strQuery
                                        cmdUp.CommandType = CommandType.Text
                                        cmdUp.ExecuteNonQuery()
                                    Else
                                        strQuery = "Delete from  AdminSecureweb.dbo.tblinvItemdet WHERE AccountID = '" & AccountID & "' and InvoiceID ='11111111-1111-1111-1111-111111111111'  and ISAuto=1 and servicedate='" & C2CurrEDate & "' "
                                        cmdUp.CommandText = strQuery
                                        cmdUp.CommandType = CommandType.Text
                                        cmdUp.ExecuteNonQuery()
                                    End If
                                    If DLCycle.SelectedValue = 2 And DRRec1("IsAccFaxBilling").ToString And DRRec1("FaxPageCount") > 0 Then
                                        If Not DRRec1("AccFaxCharges") > 0 Then
                                            ErrFound = True
                                            ErrMessage = "Fax charges not set."
                                        End If
                                        If ErrFound = False Then
                                            strQuery = "Insert Into AdminSecureweb.dbo.tblinvItemdet (AccountID,InvoiceID, itemid, Descr, quantity, Amount,totamount,mode,servicedate, dateupdate, IsAuto) Values ('" & AccountID & "', '11111111-1111-1111-1111-111111111111', '548B3D50-3F0E-4A61-BFF9-115569721C55', 'Number of pages Faxed', '" & DRRec1("FaxPageCount") & "', convert(money," & DRRec1("AccFaxCharges") & "),  convert(money," & DRRec1("FaxPageCount") * DRRec1("AccFaxCharges") & "), 'VAS', '" & C2CurrEDate & "', '" & Now & "',1) "
                                            cmdUp.CommandText = strQuery
                                            cmdUp.CommandType = CommandType.Text
                                            cmdUp.ExecuteNonQuery()
                                        End If

                                    End If

                                    If DLCycle.SelectedValue = 2 And DRRec1("IsAccVPNBilling").ToString Then

                                        If Not DRRec1("AccVPNCharges") > 0 Then
                                            ErrFound = True
                                            ErrMessage = "VPN charges not set."
                                        End If
                                        If ErrFound = False Then
                                            strQuery = "Insert Into AdminSecureweb.dbo.tblinvItemdet (AccountID,InvoiceID, itemid, Descr, quantity, Amount,totamount,mode,servicedate, dateupdate, IsAuto) Values ('" & AccountID & "', '11111111-1111-1111-1111-111111111111', '781ED926-346B-48A2-80C0-9914D8ECC0BC', 'VPN Charges', '1', convert(money," & DRRec1("AccVPNCharges") & "),  convert(money," & DRRec1("AccVPNCharges") & "), 'VAS', '" & C2CurrEDate & "', '" & Now & "',1) "
                                            cmdUp.CommandText = strQuery
                                            cmdUp.CommandType = CommandType.Text
                                            cmdUp.ExecuteNonQuery()
                                        End If

                                    End If

                                    If DLCycle.SelectedValue = 2 And DRRec1("IsAccCutPasteBilling").ToString Then

                                        If Not DRRec1("AccCutPasteBilling") > 0 Then
                                            ErrFound = True
                                            ErrMessage = "Cut/Paste charges not set."
                                        End If
                                        If ErrFound = False Then
                                            strQuery = "Insert Into AdminSecureweb.dbo.tblinvItemdet (AccountID,InvoiceID, itemid, Descr, quantity, Amount,totamount,mode,servicedate, dateupdate, IsAuto) Values ('" & AccountID & "', '11111111-1111-1111-1111-111111111111', '16d2956b-e2c1-47b8-aff0-ad42b8d8cde8', 'Additional Cut and Paste charges', '1', convert(money," & DRRec1("AccCutPasteBilling") & "),  convert(money," & DRRec1("AccCutPasteBilling") & "), 'VAS', '" & C2CurrEDate & "', '" & Now & "',1) "
                                            cmdUp.CommandText = strQuery
                                            cmdUp.CommandType = CommandType.Text
                                            cmdUp.ExecuteNonQuery()
                                        End If

                                    End If
                                    If ErrFound = False Then
                                        MyTransAttr.Commit()
                                    Else
                                        MyTransAttr.Rollback()
                                    End If
                                Catch ex As Exception
                                    MyTransAttr.Rollback()
                                    ErrFound = True
                                    ErrMessage = ex.Message
                                Finally
                                    If myConnection.State = ConnectionState.Open Then
                                        myConnection.Close()
                                    End If
                                End Try
                            End If

                        End If

                        Dim Cll1 As New TableCell
                        Dim Cll2 As New TableCell
                        Dim Cll3 As New TableCell
                        Dim TRow As New TableRow


                        Cll2.Text = DRRec1("AccountName").ToString

                        If GenLC = True Then

                            j = j + 1
                            Cll1.Text = j
                            Cll3.Text = "Success"



                            TRow.Cells.Add(Cll1)
                            TRow.Cells.Add(Cll2)
                            TRow.Cells.Add(Cll3)
                            tblSuccess.Rows.Add(TRow)
                            InsertLog(AccountID, DLCycle.SelectedValue, DLMonth.SelectedValue, DLYear.SelectedValue, "Success", String.Empty)

                        ElseIf Regenerate = True Then
                            Cll3.Text = "<input type=button value='Remove Data' onclick=RemDetails1('" & BillAccID & "'); class='button'>"
                            ' Dim cbox As New CheckBox
                            Cll3.Text = "Success"
                            Dim HD As New HiddenField
                            ' cbox.ID = "Chk"

                            HD.ID = "hdnBillID"
                            HD.Value = BillAccID
                            n = n + 1
                            '  Cll1.Controls.Add(cbox)
                            Cll1.Text = "<input type=checkbox id='chk' name='chk' value='" & BillAccID & "' >"
                            ' Cll1.Controls.Add(HD)
                            TRow.Cells.Add(Cll1)
                            TRow.Cells.Add(Cll2)
                            TRow.Cells.Add(Cll3)
                            Table2.Rows.Add(TRow)
                        ElseIf ErrFound = True Then
                            k = k + 1
                            Cll1.Text = k
                            Cll3.Text = ErrMessage
                            TRow.Cells.Add(Cll1)
                            TRow.Cells.Add(Cll2)
                            TRow.Cells.Add(Cll3)
                            tblError.Rows.Add(TRow)
                            InsertLog(AccountID, DLCycle.SelectedValue, DLMonth.SelectedValue, DLYear.SelectedValue, "Failure", ErrMessage)
                        ElseIf UDFound = True Then
                            k = k + 1
                            Cll1.Text = k
                            Cll3.Text = "Unit rates are not assigned"
                            TRow.Cells.Add(Cll1)
                            TRow.Cells.Add(Cll2)
                            TRow.Cells.Add(Cll3)
                            tblError.Rows.Add(TRow)
                            InsertLog(AccountID, DLCycle.SelectedValue, DLMonth.SelectedValue, DLYear.SelectedValue, "Failure", "Unit rates are not assigned")
                        ElseIf LCMthFound = False Then
                            k = k + 1
                            Cll1.Text = k
                            Cll3.Text = "Linecount method is not assigned"
                            TRow.Cells.Add(Cll1)
                            TRow.Cells.Add(Cll2)
                            TRow.Cells.Add(Cll3)
                            tblError.Rows.Add(TRow)
                            InsertLog(AccountID, DLCycle.SelectedValue, DLMonth.SelectedValue, DLYear.SelectedValue, "Failure", "Linecount method is not assigned")
                        Else
                            m = m + 1
                            Cll1.Text = m
                            Cll3.Text = "No records found"
                            TRow.Cells.Add(Cll1)
                            TRow.Cells.Add(Cll2)
                            TRow.Cells.Add(Cll3)
                            tblNoRec.Rows.Add(TRow)
                            InsertLog(AccountID, DLCycle.SelectedValue, DLMonth.SelectedValue, DLYear.SelectedValue, "Success", "No Records Found")
                        End If


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
        'errLog:
        '        Dim CllErr1 As New TableCell
        '        Dim CllErr2 As New TableCell
        '        Dim CllErr3 As New TableCell
        '        Dim TRowErr As New TableRow


        '        CllErr2.Text = AccountName
        '        k = k + 1
        '        CllErr1.Text = k
        '        CllErr3.Text = ErrMessage
        '        TRowErr.Cells.Add(CllErr1)
        '        TRowErr.Cells.Add(CllErr2)
        '        TRowErr.Cells.Add(CllErr3)
        '        tblError.Rows.Add(TRowErr)
        If k > 0 Then
            tblError.Visible = True
        Else
            tblError.Visible = False
        End If
        If m > 0 Then
            tblNoRec.Visible = True
        Else
            tblNoRec.Visible = False
        End If
        If n > 0 Then
            Table2.Visible = True
        Else
            Table2.Visible = False
        End If
        If j > 0 Then
            tblSuccess.Visible = True
        Else
            tblSuccess.Visible = False
        End If
    End Sub




    Protected Sub Button1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button1.Click
        'Response.Write("clicked")
        'Response.Write(HReptID.Value)
        tblError.Visible = False
        tblNoRec.Visible = False
        tblSuccess.Visible = False
        Table2.Visible = False

        If Not HReptID.Value = String.Empty Then
            Dim BillCycle As Integer
            Dim BillMonth As Integer
            Dim BillYear As Integer
            Dim AccountID As String
            Dim splt() As String = Split(HReptID.Value, ",")
            Dim ErrFound As Boolean = False
            Dim ErrMessage As String = String.Empty
            Dim strConn As String
            strConn = System.Configuration.ConfigurationManager.AppSettings("ETSCon")
            Dim myConnection As New SqlConnection(strConn)
            Dim MyTransAttr As SqlTransaction
            Dim cmdUp As New SqlCommand()
            myConnection.Open()
            'EventLog1.WriteEntry("Attr3")
            MyTransAttr = myConnection.BeginTransaction()
            'EventLog1.WriteEntry("Attr5")
            cmdUp.Connection = myConnection
            cmdUp.Transaction = MyTransAttr
            cmdUp.CommandTimeout = 600
            For i As Integer = 0 To UBound(splt)
                Dim BillAccID As String = String.Empty
                BillAccID = splt(i)
                If Not String.IsNullOrEmpty(BillAccID) Then
                    Try

                        Dim strQuery As String
                        Dim InvRecFound As Boolean
                        Dim InvoiceID As String = String.Empty
                        InvRecFound = False

                        strQuery = "Select * from AdminSecureweb.dbo.tblBillAccounts where billAccID = '" & BillAccID & "' "
                        Dim SQLCmd As New SqlCommand(strQuery, New SqlConnection(strConn))
                        SQLCmd.CommandTimeout = 600
                        Try
                            SQLCmd.Connection.Open()
                            Dim DRRec As SqlDataReader = SQLCmd.ExecuteReader()
                            If DRRec.HasRows = True Then
                                If DRRec.Read = True Then
                                    AccountID = DRRec("AccountID").ToString
                                    BillMonth = DRRec("BillMonth").ToString
                                    BillCycle = DRRec("BillCycle").ToString
                                    BillYear = DRRec("BillYear").ToString
                                    If DRRec("InvoiceID").ToString = "" Then
                                        InvRecFound = False
                                    Else
                                        InvoiceID = DRRec("InvoiceID").ToString
                                        InvRecFound = True
                                    End If
                                End If
                            Else
                                InvRecFound = False
                            End If
                            DRRec.Close()
                        Catch ex As Exception
                            ErrMessage = "err1" & ex.Message
                            ErrFound = True
                            Exit For
                        Finally

                            If SQLCmd.Connection.State = System.Data.ConnectionState.Open Then
                                SQLCmd.Connection.Close()
                                SQLCmd = Nothing
                            End If
                        End Try

                        strQuery = "Delete from AdminSecureweb.dbo.tblBillAccounts where billAccID = '" & BillAccID & "' "
                        cmdUp.CommandText = strQuery
                        cmdUp.CommandType = CommandType.Text
                        cmdUp.ExecuteNonQuery()

                        strQuery = "Delete from AdminSecureweb.dbo.tblAccBillDetails where billAccID = '" & BillAccID & "' "
                        cmdUp.CommandText = strQuery
                        cmdUp.CommandType = CommandType.Text
                        cmdUp.ExecuteNonQuery()

                        strQuery = "Delete from AdminETS.dbo.tblBillingLines where billAccID = '" & BillAccID & "' "
                        cmdUp.CommandText = strQuery
                        cmdUp.CommandType = CommandType.Text
                        cmdUp.ExecuteNonQuery()


                        If InvRecFound = True Then

                            strQuery = "Delete from AdminSecureweb.dbo.tblInvItemdet where mode='Trans' and InvoiceID = '" & InvoiceID & "' "
                            cmdUp.CommandText = strQuery
                            cmdUp.CommandType = CommandType.Text
                            cmdUp.ExecuteNonQuery()

                            strQuery = "Delete from AdminSecureweb.dbo.tblInvItemdet where itemid ='25c7d577-967e-48ab-a62d-87fb6f420be1'  and InvoiceID = '" & InvoiceID & "' "
                            cmdUp.CommandText = strQuery
                            cmdUp.CommandType = CommandType.Text
                            cmdUp.ExecuteNonQuery()

                            strQuery = "update AdminSecureweb.dbo.tblInvItemdet set InvoiceID= '11111111-1111-1111-1111-111111111111'  where mode='VAS' and InvoiceID = '" & InvoiceID & "' "
                            cmdUp.CommandText = strQuery
                            cmdUp.CommandType = CommandType.Text
                            cmdUp.ExecuteNonQuery()


                            strQuery = "Delete from AdminSecureweb.dbo.Invupdata where TrackID = '" & InvoiceID & "' "
                            cmdUp.CommandText = strQuery
                            cmdUp.CommandType = CommandType.Text
                            cmdUp.ExecuteNonQuery()


                        End If

                        strQuery = "DELETE FROM [ADMINSecureweb].[dbo].[tblBillGeneratedDetails] WHERE [AccountID]= '" & AccountID & "' and [BillCycle] = '" & BillCycle & "' and [BillMonth] ='" & BillMonth & "' and [BillYear] ='" & BillYear & "' "
                        cmdUp.CommandText = strQuery
                        cmdUp.CommandType = CommandType.Text
                        cmdUp.ExecuteNonQuery()
                        strQuery = "INSERT INTO [ADMINSecureweb].[dbo].[tblBillGeneratedDetails]([AccountID] ,[BillCycle] ,[BillMonth] ,[BillYear]  ,[Status]  ,[ErrMessage],[DateUpdate])  VALUES ('" & AccountID & "', '" & BillCycle & "', '" & BillMonth & "', '" & BillYear & "', 'Removed', '', '" & Now & "') "
                        cmdUp.CommandText = strQuery
                        cmdUp.CommandType = CommandType.Text
                        cmdUp.ExecuteNonQuery()


                    Catch ex As Exception
                        ErrMessage = "err2#" & BillAccID & "#" & ex.Message
                        ErrFound = True
                        Exit For
                    End Try
                End If
            Next
            If ErrFound = False Then
                MyTransAttr.Commit()
                Label1.Visible = True
                Label1.Text = "Details have been removed successfully."
                If myConnection.State = ConnectionState.Open Then
                    myConnection.Close()
                End If
            Else
                MyTransAttr.Rollback()
                Label1.Visible = True
                Label1.Text = "Issue in removing details. Please contact Technical Support Team for more details." & ErrMessage
            End If
        Else
            Label1.Visible = True
            Label1.Text = "Issue in removing details. Please contact Technical Support Team for more details."
        End If


    End Sub

    Protected Sub InsertLog(ByVal AccountID As String, ByVal BillCycle As Integer, ByVal BillMonth As Integer, ByVal BillYear As Integer, ByVal Status As String, ByVal ErrMessage As String)
        Dim strConn As String
        strConn = System.Configuration.ConfigurationManager.AppSettings("ETSCon")
        Dim myConnection As New SqlConnection(strConn)
        Dim MyTransAttr As SqlTransaction
        Dim strQuery As String
        Dim cmdUp As New SqlCommand()
        myConnection.Open()
        MyTransAttr = myConnection.BeginTransaction()
        Try


            'EventLog1.WriteEntry("Attr3")

            'EventLog1.WriteEntry("Attr5")
            cmdUp.Connection = myConnection
            cmdUp.Transaction = MyTransAttr
            cmdUp.CommandTimeout = 600
            strQuery = "DELETE FROM [ADMINSecureweb].[dbo].[tblBillGeneratedDetails] WHERE [AccountID]= '" & AccountID & "' and [BillCycle] = '" & BillCycle & "' and [BillMonth] ='" & BillMonth & "' and [BillYear] ='" & BillYear & "' "
            cmdUp.CommandText = strQuery
            cmdUp.CommandType = CommandType.Text
            cmdUp.ExecuteNonQuery()
            strQuery = "INSERT INTO [ADMINSecureweb].[dbo].[tblBillGeneratedDetails]([AccountID] ,[BillCycle] ,[BillMonth] ,[BillYear]  ,[Status]  ,[ErrMessage],[DateUpdate])  VALUES ('" & AccountID & "', '" & BillCycle & "', '" & BillMonth & "', '" & BillYear & "', '" & Status & "', '" & ErrMessage & "', '" & Now & "') "
            cmdUp.CommandText = strQuery
            cmdUp.CommandType = CommandType.Text
            cmdUp.ExecuteNonQuery()
            MyTransAttr.Commit()
        Catch ex As Exception
            Response.Write(ex.Message)
            MyTransAttr.Rollback()
        End Try
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
            strQuery = "Insert Into AdminSecureweb.dbo.tblinvItemdet (AccountID,InvoiceID, itemid, Descr, quantity, Amount,totamount,mode,servicedate, dateupdate) Values ('" & AccID & "', '11111111-1111-1111-1111-111111111111', '" & ItemID & "', '" & Descr & "', '" & Quantity & "', convert(money," & amount & "),  convert(money," & Totamount & "), 'VAS', '" & ServiceDate & "', '" & Now & "')"
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
End Class




