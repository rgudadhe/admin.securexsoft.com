Imports System.Data.Sqlclient
Imports System.Data
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports System.Web.UI.WebControls.WebParts
Imports System.Web.UI.HtmlControls
Imports System
Imports System.Configuration
Imports System.IO
Imports System.Web
Imports System.Web.Security
Partial Class MIS_DailyMins
    Inherits BasePage
    Protected Sub ShowActDetails()
        R1Cell1.Text = "Account"
        tblDtls.Text = "Account Details"
        Dim strConn As String
        Dim strDate As String
        Dim strCategory As String
        strCategory = ""
        Dim InpDate As Date
        If TxtDate.Text = "" Then
            strDate = Now.ToShortDateString
        Else
            strDate = TxtDate.Text
        End If


        InpDate = Date.Parse(strDate)


        Dim CurrSDate As Date
        Dim CurrEDate As Date
        Dim PrvWDays As Integer
        Dim CurrWDays As Integer
        CurrSDate = Month(InpDate) & "/1/" & Year(InpDate)
        'If Month(CurrSDate) = Month(Now) Then
        '    CurrEDate = Now.ToShortDateString
        'Else
        '    CurrEDate = DateAdd(DateInterval.Day, -1, DateAdd(DateInterval.Month, 1, CurrSDate))
        'End If
        CurrEDate = InpDate
        CurrWDays = WorkingDays(CurrSDate, CurrEDate)
        Dim PrvSDate As Date
        Dim PrvEDate As Date

        PrvSDate = DateAdd(DateInterval.Month, -1, CurrSDate)
        PrvEDate = DateAdd(DateInterval.Day, -1, CurrSDate)
        PrvWDays = WorkingDays(PrvSDate, PrvEDate)

        R1Cell4.Text = InpDate
        R1Cell5.Text = DateAdd(DateInterval.Day, -1, InpDate)
        R1Cell6.Text = DateAdd(DateInterval.Day, -2, InpDate)
        R1Cell7.Text = DateAdd(DateInterval.Day, -3, InpDate)
        R1Cell8.Text = DateAdd(DateInterval.Day, -4, InpDate)
        R1Cell9.Text = DateAdd(DateInterval.Day, -5, InpDate)
        R1Cell10.Text = DateAdd(DateInterval.Day, -6, InpDate)
        R2Cell1.Text = "Description"
        R2Cell3.Text = InpDate.ToString("MMM") & " " & InpDate.Year
        R2Cell2.Text = DateAdd(DateInterval.Month, -1, InpDate).ToString("MMM") & " " & DateAdd(DateInterval.Month, -1, InpDate).Year
        R2Cell2.ToolTip = PrvWDays
        R2Cell3.ToolTip = CurrWDays
        R2Cell4.Text = InpDate.ToString("ddd")
        R2Cell5.Text = DateAdd(DateInterval.Day, -1, InpDate).ToString("ddd")
        R2Cell6.Text = DateAdd(DateInterval.Day, -2, InpDate).ToString("ddd")
        R2Cell7.Text = DateAdd(DateInterval.Day, -3, InpDate).ToString("ddd")
        R2Cell8.Text = DateAdd(DateInterval.Day, -4, InpDate).ToString("ddd")
        R2Cell9.Text = DateAdd(DateInterval.Day, -5, InpDate).ToString("ddd")
        R2Cell10.Text = DateAdd(DateInterval.Day, -6, InpDate).ToString("ddd")
        ' Response.Write(CurrWDays & "#" & PrvWDays)
        '  Response.Write(CurrSDate & "#" & CurrEDate & "#")
        '  Response.Write(PrvSDate & "#" & PrvEDate)
        Dim t2 As New Table
        t2.Style("width") = "100%"
        t2.BorderWidth = 2
        t2.GridLines = GridLines.Both
        Dim I As Integer
        I = 0
        strConn = System.Configuration.ConfigurationManager.AppSettings("ETSCon")
        Dim strQuery As String
        Dim STAvgPreMins As Integer
        Dim STAvgCurrMins As Integer
        Dim STDayMins1 As Integer
        Dim STDayMins2 As Integer
        Dim STDayMins3 As Integer
        Dim STDayMins4 As Integer
        Dim STDayMins5 As Integer
        Dim STDayMins6 As Integer
        Dim STDayMins7 As Integer
        STAvgPreMins = 0
        STAvgCurrMins = 0
        STDayMins1 = 0
        STDayMins2 = 0
        STDayMins3 = 0
        STDayMins4 = 0
        STDayMins5 = 0
        STDayMins6 = 0
        STDayMins7 = 0
        Dim TAvgPreMins As Integer
        Dim TAvgCurrMins As Integer
        Dim TDayMins1 As Integer
        Dim TDayMins2 As Integer
        Dim TDayMins3 As Integer
        Dim TDayMins4 As Integer
        Dim TDayMins5 As Integer
        Dim TDayMins6 As Integer
        Dim TDayMins7 As Integer
        TAvgPreMins = 0
        TAvgCurrMins = 0
        TDayMins1 = 0
        TDayMins2 = 0
        TDayMins3 = 0
        TDayMins4 = 0
        TDayMins5 = 0
        TDayMins6 = 0
        TDayMins7 = 0

        'strQuery = "Select Sum(IsNull(PM.Mins,0)) AS PrevMonthMins, Sum(IsNull(CM.Mins,0)) AS CurrMonthMins, Sum(IsNull(T1.Mins, 0)) as DayMins1, Sum(IsNull(T2.Mins,0)) as DayMins2, Sum(IsNull(T3.Mins,0)) as DayMins3, Sum(IsNull(T4.Mins,0)) as DayMins4, Sum(IsNull(T5.Mins,0)) as DayMins5, Sum(IsNull(T6.Mins,0)) as DayMins6, Sum(IsNull(T7.Mins,0)) as DayMins7, 'Direct' as ActStatus  "
        'strQuery = strQuery & " from tblaccounts A "
        'strQuery = strQuery & " LEFT OUTER JOIN (Select * from tblActCategories) C"
        'strQuery = strQuery & " on C.category = A.Category "
        'strQuery = strQuery & " LEFT OUTER JOIN (Select AccountID, sum(datediff(s,0,duration)) AS Mins from tbltranscriptionmain where status='1073741824' and Day(DateModified) = '" & Day(InpDate) & "' and Month(DateModified) = '" & Month(InpDate) & "' and Year(DateModified) =  '" & Year(InpDate) & "' Group By AccountID) T1"
        'strQuery = strQuery & " on T1.accountid = A.Accountid "
        'strQuery = strQuery & " LEFT OUTER JOIN (Select AccountID, sum(datediff(s,0,duration)) AS Mins from tbltranscriptionmain where status='1073741824' and Day(DateModified) = '" & Day(DateAdd(DateInterval.Day, -1, InpDate)) & "' and Month(DateModified) = '" & Month(DateAdd(DateInterval.Day, -1, InpDate)) & "' and Year(DateModified) = '" & Year(DateAdd(DateInterval.Day, -1, InpDate)) & "'  Group By AccountID) T2"
        'strQuery = strQuery & " on T2.accountid = A.Accountid "
        'strQuery = strQuery & " LEFT OUTER JOIN (Select AccountID, sum(datediff(s,0,duration)) AS Mins from tbltranscriptionmain where status='1073741824' and Day(DateModified) = '" & Day(DateAdd(DateInterval.Day, -2, InpDate)) & "' and Month(DateModified) = '" & Month(DateAdd(DateInterval.Day, -2, InpDate)) & "'  and Year(DateModified) = '" & Year(DateAdd(DateInterval.Day, -2, InpDate)) & "'  Group By AccountID) T3"
        'strQuery = strQuery & " on T3.accountid = A.Accountid "
        'strQuery = strQuery & " LEFT OUTER JOIN (Select AccountID, sum(datediff(s,0,duration)) AS Mins from tbltranscriptionmain where status='1073741824' and Day(DateModified) = '" & Day(DateAdd(DateInterval.Day, -3, InpDate)) & "' and Month(DateModified) = '" & Month(DateAdd(DateInterval.Day, -3, InpDate)) & "'  and Year(DateModified) = '" & Year(DateAdd(DateInterval.Day, -3, InpDate)) & "'  Group By AccountID) T4"
        'strQuery = strQuery & " on T4.accountid = A.Accountid "
        'strQuery = strQuery & " LEFT OUTER JOIN (Select AccountID, sum(datediff(s,0,duration)) AS Mins from tbltranscriptionmain where status='1073741824' and Day(DateModified) = '" & Day(DateAdd(DateInterval.Day, -4, InpDate)) & "' and Month(DateModified) = '" & Month(DateAdd(DateInterval.Day, -4, InpDate)) & "'  and Year(DateModified) = '" & Year(DateAdd(DateInterval.Day, -4, InpDate)) & "'  Group By AccountID) T5"
        'strQuery = strQuery & " on T5.accountid = A.Accountid "
        'strQuery = strQuery & " LEFT OUTER JOIN (Select AccountID, sum(datediff(s,0,duration)) AS Mins from tbltranscriptionmain where status='1073741824' and Day(DateModified) = '" & Day(DateAdd(DateInterval.Day, -5, InpDate)) & "' and Month(DateModified) = '" & Month(DateAdd(DateInterval.Day, -5, InpDate)) & "'  and Year(DateModified) = '" & Year(DateAdd(DateInterval.Day, -5, InpDate)) & "'  Group By AccountID) T6"
        'strQuery = strQuery & " on T6.accountid = A.Accountid "
        'strQuery = strQuery & " LEFT OUTER JOIN (Select AccountID, sum(datediff(s,0,duration)) AS Mins from tbltranscriptionmain where status='1073741824' and Day(DateModified) = '" & Day(DateAdd(DateInterval.Day, -6, InpDate)) & "' and Month(DateModified) = '" & Month(DateAdd(DateInterval.Day, -6, InpDate)) & "'  and Year(DateModified) = '" & Year(DateAdd(DateInterval.Day, -6, InpDate)) & "'  Group By AccountID) T7"
        'strQuery = strQuery & " on T7.accountid = A.Accountid "
        'strQuery = strQuery & " LEFT OUTER JOIN (Select AccountID, sum(datediff(s,0,duration)) AS Mins from tbltranscriptionmain where status='1073741824' and Month(DateModified) = '" & Month(DateAdd(DateInterval.Month, -1, InpDate)) & "' and Year(DateModified) = '" & Year(DateAdd(DateInterval.Month, -1, InpDate)) & "' Group By AccountID) PM"
        'strQuery = strQuery & " on PM.accountid = A.Accountid "
        'strQuery = strQuery & " LEFT OUTER JOIN (Select AccountID, sum(datediff(s,0,duration)) AS Mins from tbltranscriptionmain where status='1073741824' and DateModified >= '" & CurrSDate & "' and DateModified < '" & CurrEDate.AddDays(1) & "' Group By AccountID) CM"
        'strQuery = strQuery & " on CM.accountid = A.Accountid where A.contractorID ='" & Session("contractorID").ToString & "' and (A.indirect = 'False' or A.Indirect is null)  and (A.isdeleted is null or a.isdeleted = 'False')   and MISRep = 'True'    "
        'strQuery = strQuery & " UNION Select Sum(IsNull(PM.Mins,0)) AS PrevMonthMins, Sum(IsNull(CM.Mins,0)) AS CurrMonthMins, Sum(IsNull(T1.Mins, 0)) as DayMins1, Sum(IsNull(T2.Mins,0)) as DayMins2, Sum(IsNull(T3.Mins,0)) as DayMins3, Sum(IsNull(T4.Mins,0)) as DayMins4, Sum(IsNull(T5.Mins,0)) as DayMins5, Sum(IsNull(T6.Mins,0)) as DayMins6, Sum(IsNull(T7.Mins,0)) as DayMins7, 'InDirect' as ActStatus  "
        'strQuery = strQuery & " from tblaccounts A "
        'strQuery = strQuery & " LEFT OUTER JOIN (Select * from tblActCategories) C"
        'strQuery = strQuery & " on C.category = A.Category "
        'strQuery = strQuery & " LEFT OUTER JOIN (Select AccountID, sum(punits) AS Mins from tblIndirectActUnits where Day(SubmitDate) = '" & Day(InpDate) & "' and Month(SubmitDate) = '" & Month(InpDate) & "' and Year(SubmitDate) =  '" & Year(InpDate) & "' Group By AccountID) T1"
        'strQuery = strQuery & " on T1.accountid = A.Accountid "
        'strQuery = strQuery & " LEFT OUTER JOIN (Select AccountID, sum(punits) AS Mins from tblIndirectActUnits where Day(SubmitDate) = '" & Day(DateAdd(DateInterval.Day, -1, InpDate)) & "' and Month(SubmitDate) = '" & Month(DateAdd(DateInterval.Day, -1, InpDate)) & "' and Year(Submitdate) = '" & Year(DateAdd(DateInterval.Day, -1, InpDate)) & "'  Group By AccountID) T2"
        'strQuery = strQuery & " on T2.accountid = A.Accountid "
        'strQuery = strQuery & " LEFT OUTER JOIN (Select AccountID, sum(punits) AS Mins from tblIndirectActUnits where Day(SubmitDate) = '" & Day(DateAdd(DateInterval.Day, -2, InpDate)) & "' and Month(Submitdate) = '" & Month(DateAdd(DateInterval.Day, -2, InpDate)) & "'  and Year(Submitdate) = '" & Year(DateAdd(DateInterval.Day, -2, InpDate)) & "'  Group By AccountID) T3"
        'strQuery = strQuery & " on T3.accountid = A.Accountid "
        'strQuery = strQuery & " LEFT OUTER JOIN (Select AccountID, sum(punits) AS Mins from tblIndirectActUnits where Day(SubmitDate) = '" & Day(DateAdd(DateInterval.Day, -3, InpDate)) & "' and Month(Submitdate) = '" & Month(DateAdd(DateInterval.Day, -3, InpDate)) & "'  and Year(Submitdate) = '" & Year(DateAdd(DateInterval.Day, -3, InpDate)) & "'  Group By AccountID) T4"
        'strQuery = strQuery & " on T4.accountid = A.Accountid "
        'strQuery = strQuery & " LEFT OUTER JOIN (Select AccountID, sum(punits) AS Mins from tblIndirectActUnits where Day(SubmitDate) = '" & Day(DateAdd(DateInterval.Day, -4, InpDate)) & "' and Month(Submitdate) = '" & Month(DateAdd(DateInterval.Day, -4, InpDate)) & "'  and Year(Submitdate) = '" & Year(DateAdd(DateInterval.Day, -4, InpDate)) & "'  Group By AccountID) T5"
        'strQuery = strQuery & " on T5.accountid = A.Accountid "
        'strQuery = strQuery & " LEFT OUTER JOIN (Select AccountID, sum(punits) AS Mins from tblIndirectActUnits where Day(SubmitDate) = '" & Day(DateAdd(DateInterval.Day, -5, InpDate)) & "' and Month(Submitdate) = '" & Month(DateAdd(DateInterval.Day, -5, InpDate)) & "'  and Year(Submitdate) = '" & Year(DateAdd(DateInterval.Day, -5, InpDate)) & "'  Group By AccountID) T6"
        'strQuery = strQuery & " on T6.accountid = A.Accountid "
        'strQuery = strQuery & " LEFT OUTER JOIN (Select AccountID, sum(punits) AS Mins from tblIndirectActUnits where Day(SubmitDate) = '" & Day(DateAdd(DateInterval.Day, -6, InpDate)) & "' and Month(Submitdate) = '" & Month(DateAdd(DateInterval.Day, -6, InpDate)) & "'  and Year(Submitdate) = '" & Year(DateAdd(DateInterval.Day, -6, InpDate)) & "'  Group By AccountID) T7"
        'strQuery = strQuery & " on T7.accountid = A.Accountid "
        'strQuery = strQuery & " LEFT OUTER JOIN (Select AccountID, sum(punits) AS Mins from tblIndirectActUnits where Month(SubmitDate) = '" & Month(DateAdd(DateInterval.Month, -1, InpDate)) & "' and Year(Submitdate) = '" & Year(DateAdd(DateInterval.Month, -1, InpDate)) & "' Group By AccountID) PM"
        'strQuery = strQuery & " on PM.accountid = A.Accountid "
        'strQuery = strQuery & " LEFT OUTER JOIN (Select AccountID, sum(punits) AS Mins from tblIndirectActUnits where submitdate >= '" & CurrSDate & "' and submitdate < '" & CurrEDate.AddDays(1) & "' Group By AccountID) CM"
        'strQuery = strQuery & " on CM.accountid = A.Accountid  where A.contractorID ='" & Session("contractorID").ToString & "' and A.Indirect = 'True'  and (A.isdeleted is null or A.isdeleted = 'False')   and MISRep = 'True'   order by Actstatus "
        ''Response.Write(strQuery)
        ''Response.End()

        'Dim SQLCmdT As New SqlCommand(strQuery, New SqlConnection(strConn))
        'Try
        '    SQLCmdT.Connection.Open()
        '    Dim DRRecT As SqlDataReader = SQLCmdT.ExecuteReader()
        '    If DRRecT.HasRows Then
        '        While DRRecT.Read

        '            If DRRecT("ActStatus").ToString = "InDirect" Then
        '                If IsDBNull(DRRecT(0)) Then
        '                    TAvgPreMins += 0
        '                Else
        '                    TAvgPreMins += FormatNumber((DRRecT(0).ToString) / PrvWDays, 0).Replace(",", "")
        '                End If
        '                If IsDBNull(DRRecT(1)) Then
        '                    TAvgCurrMins += 0
        '                Else
        '                    TAvgCurrMins += FormatNumber((DRRecT(1).ToString) / CurrWDays, 0).Replace(",", "")
        '                End If

        '                If IsDBNull(DRRecT(2)) Then
        '                    TDayMins1 += 0
        '                Else
        '                    TDayMins1 += FormatNumber(DRRecT(2), 0).Replace(",", "")
        '                End If

        '                If IsDBNull(DRRecT(3)) Then
        '                    TDayMins2 += 0
        '                Else
        '                    TDayMins2 += FormatNumber(DRRecT(3), 0).Replace(",", "")
        '                End If

        '                If IsDBNull(DRRecT(4)) Then
        '                    TDayMins3 += 0
        '                Else
        '                    TDayMins3 += FormatNumber(DRRecT(4), 0).Replace(",", "")
        '                End If

        '                If IsDBNull(DRRecT(5)) Then
        '                    TDayMins4 += 0
        '                Else
        '                    TDayMins4 += FormatNumber(DRRecT(5), 0).Replace(",", "")
        '                End If

        '                If IsDBNull(DRRecT(6)) Then
        '                    TDayMins5 += 0
        '                Else
        '                    TDayMins5 += FormatNumber(DRRecT(6), 0).Replace(",", "")
        '                End If

        '                If IsDBNull(DRRecT(7)) Then
        '                    TDayMins6 += 0
        '                Else
        '                    TDayMins6 += FormatNumber(DRRecT(7), 0).Replace(",", "")
        '                End If


        '                If IsDBNull(DRRecT(8)) Then
        '                    TDayMins7 += 0
        '                Else
        '                    TDayMins7 += FormatNumber(DRRecT(8), 0).Replace(",", "")
        '                End If

        '            Else
        '                If IsDBNull(DRRecT(0)) Then
        '                    TAvgPreMins += 0
        '                Else
        '                    TAvgPreMins += FormatNumber((DRRecT(0).ToString / 60) / PrvWDays, 0).Replace(",", "")
        '                End If
        '                If IsDBNull(DRRecT(1)) Then
        '                    TAvgCurrMins += 0
        '                Else
        '                    TAvgCurrMins += FormatNumber((DRRecT(1).ToString / 60) / CurrWDays, 0).Replace(",", "")
        '                End If

        '                If IsDBNull(DRRecT(2)) Then
        '                    TDayMins1 += 0
        '                Else
        '                    TDayMins1 += FormatNumber(DRRecT(2) / 60, 0).Replace(",", "")
        '                End If

        '                If IsDBNull(DRRecT(3)) Then
        '                    TDayMins2 += 0
        '                Else
        '                    TDayMins2 += FormatNumber(DRRecT(3) / 60, 0).Replace(",", "")
        '                End If

        '                If IsDBNull(DRRecT(4)) Then
        '                    TDayMins3 += 0
        '                Else
        '                    TDayMins3 += FormatNumber(DRRecT(4) / 60, 0).Replace(",", "")
        '                End If

        '                If IsDBNull(DRRecT(5)) Then
        '                    TDayMins4 += 0
        '                Else
        '                    TDayMins4 += FormatNumber(DRRecT(5) / 60, 0).Replace(",", "")
        '                End If

        '                If IsDBNull(DRRecT(6)) Then
        '                    TDayMins5 += 0
        '                Else
        '                    TDayMins5 += FormatNumber(DRRecT(6) / 60, 0).Replace(",", "")
        '                End If

        '                If IsDBNull(DRRecT(7)) Then
        '                    TDayMins6 += 0
        '                Else
        '                    TDayMins6 += FormatNumber(DRRecT(7) / 60, 0).Replace(",", "")
        '                End If


        '                If IsDBNull(DRRecT(8)) Then
        '                    TDayMins7 += 0
        '                Else
        '                    TDayMins7 += FormatNumber(DRRecT(8) / 60, 0).Replace(",", "")
        '                End If


        '            End If
        '            Dim Row1 As New TableRow
        '            Dim Cell1 As New TableCell
        '            Dim Cell2 As New TableCell
        '            Dim Cell3 As New TableCell
        '            Dim Cell4 As New TableCell
        '            Dim Cell5 As New TableCell
        '            Dim Cell6 As New TableCell
        '            Dim Cell7 As New TableCell
        '            Dim Cell8 As New TableCell
        '            Dim Cell9 As New TableCell
        '            Dim Cell10 As New TableCell
        '            If DRRecT("ActStatus").ToString = "InDirect" Then
        '                Cell1.HorizontalAlign = HorizontalAlign.Left
        '                Cell1.Text = "Indirect Accounts"
        '                If IsDBNull(DRRecT(0)) Then
        '                    Cell2.Text = "0"
        '                Else
        '                    Cell2.Text = FormatNumber((DRRecT(0).ToString) / PrvWDays, 0).Replace(",", "")
        '                End If
        '                If IsDBNull(DRRecT(1)) Then
        '                    Cell3.Text = "0"
        '                Else
        '                    Cell3.Text = FormatNumber((DRRecT(1).ToString) / CurrWDays, 0).Replace(",", "")
        '                End If

        '                Cell2.ToolTip = "Working Days: " & PrvWDays
        '                Cell3.ToolTip = "Working Days: " & CurrWDays
        '                Cell2.CssClass = "tblbg3"
        '                Cell3.CssClass = "tblbg4"
        '                If IsDBNull(DRRecT(2)) Then
        '                    Cell4.Text = "0"
        '                Else
        '                    Cell4.Text = FormatNumber(DRRecT(2).ToString, 0).Replace(",", "")
        '                End If

        '                If IsDBNull(DRRecT(3)) Then
        '                    Cell5.Text = "0"
        '                Else
        '                    Cell5.Text = FormatNumber(DRRecT(3).ToString, 0).Replace(",", "")
        '                End If

        '                If IsDBNull(DRRecT(4)) Then
        '                    Cell6.Text = "0"
        '                Else
        '                    Cell6.Text = FormatNumber(DRRecT(4).ToString, 0).Replace(",", "")
        '                End If

        '                If IsDBNull(DRRecT(5)) Then
        '                    Cell7.Text = "0"
        '                Else
        '                    Cell7.Text = FormatNumber(DRRecT(5).ToString, 0).Replace(",", "")
        '                End If

        '                If IsDBNull(DRRecT(1)) Then
        '                    Cell8.Text = "0"
        '                Else
        '                    Cell8.Text = FormatNumber(DRRecT(2).ToString, 0).Replace(",", "")
        '                End If

        '                If IsDBNull(DRRecT(6)) Then
        '                    Cell9.Text = "0"
        '                Else
        '                    Cell9.Text = FormatNumber(DRRecT(6).ToString, 0).Replace(",", "")
        '                End If

        '                If IsDBNull(DRRecT(7)) Then
        '                    Cell10.Text = "0"
        '                Else
        '                    Cell10.Text = FormatNumber(DRRecT(7).ToString, 0).Replace(",", "")
        '                End If


        '            Else
        '                Cell1.HorizontalAlign = HorizontalAlign.Left
        '                Cell1.Text = "Direct Accounts"
        '                If IsDBNull(DRRecT(0)) Then
        '                    Cell2.Text = "0"
        '                Else
        '                    Cell2.Text = FormatNumber((DRRecT(0).ToString / 60) / PrvWDays, 0).Replace(",", "")
        '                End If
        '                If IsDBNull(DRRecT(1)) Then
        '                    Cell3.Text = "0"
        '                Else
        '                    Cell3.Text = FormatNumber((DRRecT(1).ToString / 60) / CurrWDays, 0).Replace(",", "")
        '                End If


        '                Cell2.ToolTip = "Working Days: " & PrvWDays
        '                Cell3.ToolTip = "Working Days: " & CurrWDays
        '                Cell2.CssClass = "tblbg3"
        '                Cell3.CssClass = "tblbg4"
        '                If IsDBNull(DRRecT(2)) Then
        '                    Cell4.Text = "0"
        '                Else
        '                    Cell4.Text = FormatNumber(DRRecT(2).ToString / 60, 0).Replace(",", "")
        '                End If

        '                If IsDBNull(DRRecT(3)) Then
        '                    Cell5.Text = "0"
        '                Else
        '                    Cell5.Text = FormatNumber(DRRecT(3).ToString / 60, 0).Replace(",", "")
        '                End If

        '                If IsDBNull(DRRecT(4)) Then
        '                    Cell6.Text = "0"
        '                Else
        '                    Cell6.Text = FormatNumber(DRRecT(4).ToString / 60, 0).Replace(",", "")
        '                End If

        '                If IsDBNull(DRRecT(5)) Then
        '                    Cell7.Text = "0"
        '                Else
        '                    Cell7.Text = FormatNumber(DRRecT(5).ToString / 60, 0).Replace(",", "")
        '                End If

        '                If IsDBNull(DRRecT(1)) Then
        '                    Cell8.Text = "0"
        '                Else
        '                    Cell8.Text = FormatNumber(DRRecT(2).ToString / 60, 0).Replace(",", "")
        '                End If

        '                If IsDBNull(DRRecT(6)) Then
        '                    Cell9.Text = "0"
        '                Else
        '                    Cell9.Text = FormatNumber(DRRecT(6).ToString / 60, 0).Replace(",", "")
        '                End If

        '                If IsDBNull(DRRecT(7)) Then
        '                    Cell10.Text = "0"
        '                Else
        '                    Cell10.Text = FormatNumber(DRRecT(7).ToString / 60, 0).Replace(",", "")
        '                End If


        '            End If
        '            Row1.CssClass = "tblbg2"
        '            Row1.Cells.Add(Cell1)
        '            Row1.Cells.Add(Cell2)
        '            Row1.Cells.Add(Cell3)
        '            Row1.Cells.Add(Cell4)
        '            Row1.Cells.Add(Cell5)
        '            Row1.Cells.Add(Cell6)
        '            Row1.Cells.Add(Cell7)
        '            Row1.Cells.Add(Cell8)
        '            Row1.Cells.Add(Cell9)
        '            Row1.Cells.Add(Cell10)
        '            tblMins.Rows.Add(Row1)
        '        End While
        '        Dim A1Row1 As New TableRow
        '        Dim A1Cell1 As New TableCell
        '        Dim A1Cell2 As New TableCell
        '        Dim A1Cell3 As New TableCell
        '        Dim A1Cell4 As New TableCell
        '        Dim A1Cell5 As New TableCell
        '        Dim A1Cell6 As New TableCell
        '        Dim A1Cell7 As New TableCell
        '        Dim A1Cell8 As New TableCell
        '        Dim A1Cell9 As New TableCell
        '        Dim A1Cell10 As New TableCell
        '        A1Row1.HorizontalAlign = HorizontalAlign.Center
        '        A1Row1.CssClass = "tblbgbody"
        '        A1Row1.Font.Bold = True

        '        A1Cell1.Text = "Total"
        '        A1Cell2.Text = FormatNumber(TAvgPreMins, 0).Replace(",", "")
        '        A1Cell3.Text = FormatNumber(TAvgCurrMins, 0).Replace(",", "")
        '        A1Cell4.Text = FormatNumber(TDayMins1, 0).Replace(",", "")
        '        A1Cell5.Text = FormatNumber(TDayMins2, 0).Replace(",", "")
        '        A1Cell6.Text = FormatNumber(TDayMins3, 0).Replace(",", "")
        '        A1Cell7.Text = FormatNumber(TDayMins4, 0).Replace(",", "")
        '        A1Cell8.Text = FormatNumber(TDayMins5, 0).Replace(",", "")
        '        A1Cell9.Text = FormatNumber(TDayMins6, 0).Replace(",", "")
        '        A1Cell10.Text = FormatNumber(TDayMins7, 0).Replace(",", "")

        '        A1Row1.Cells.Add(A1Cell1)
        '        A1Row1.Cells.Add(A1Cell2)
        '        A1Row1.Cells.Add(A1Cell3)
        '        A1Row1.Cells.Add(A1Cell4)
        '        A1Row1.Cells.Add(A1Cell5)
        '        A1Row1.Cells.Add(A1Cell6)
        '        A1Row1.Cells.Add(A1Cell7)
        '        A1Row1.Cells.Add(A1Cell8)
        '        A1Row1.Cells.Add(A1Cell9)
        '        A1Row1.Cells.Add(A1Cell10)
        '        tblMins.Rows.Add(A1Row1)

        '    End If
        '    DRRecT.Close()
        'Finally
        '    If SQLCmdT.Connection.State = System.Data.ConnectionState.Open Then
        '        SQLCmdT.Connection.Close()
        '        SQLCmdT = Nothing
        '    End If
        'End Try

        TAvgPreMins = 0
        TAvgCurrMins = 0
        TDayMins1 = 0
        TDayMins2 = 0
        TDayMins3 = 0
        TDayMins4 = 0
        TDayMins5 = 0
        TDayMins6 = 0
        TDayMins7 = 0

        strQuery = "Select A.Accountid, A.Description, IsNull(PM.Mins,0) AS PrevMonthMins, IsNull(CM.Mins,0) AS CurrMonthMins, IsNull(T1.Mins, 0) as DayMins1, IsNull(T2.Mins,0) as DayMins2, IsNull(T3.Mins,0) as DayMins3, IsNull(T4.Mins,0) as DayMins4, IsNull(T5.Mins,0) as DayMins5, IsNull(T6.Mins,0) as DayMins6, IsNull(T7.Mins,0) as DayMins7, C.priority, C.Description as CateDescr, A.Indirect "
        strQuery = strQuery & " from tblaccounts A "
        strQuery = strQuery & " LEFT OUTER JOIN (Select * from tblActCategories) C"
        strQuery = strQuery & " on C.category = A.Category "
        strQuery = strQuery & " LEFT OUTER JOIN (Select AccountID, sum(datediff(s,0,duration)) AS Mins from tbltranscriptionmain where status='1073741824' and Day(DateModified) = '" & Day(InpDate) & "' and Month(DateModified) = '" & Month(InpDate) & "' and Year(DateModified) =  '" & Year(InpDate) & "' Group By AccountID) T1"
        strQuery = strQuery & " on T1.accountid = A.Accountid "
        strQuery = strQuery & " LEFT OUTER JOIN (Select AccountID, sum(datediff(s,0,duration)) AS Mins from tbltranscriptionmain where status='1073741824' and Day(DateModified) = '" & Day(DateAdd(DateInterval.Day, -1, InpDate)) & "' and Month(DateModified) = '" & Month(DateAdd(DateInterval.Day, -1, InpDate)) & "' and Year(DateModified) = '" & Year(DateAdd(DateInterval.Day, -1, InpDate)) & "'  Group By AccountID) T2"
        strQuery = strQuery & " on T2.accountid = A.Accountid "
        strQuery = strQuery & " LEFT OUTER JOIN (Select AccountID, sum(datediff(s,0,duration)) AS Mins from tbltranscriptionmain where status='1073741824' and Day(DateModified) = '" & Day(DateAdd(DateInterval.Day, -2, InpDate)) & "' and Month(DateModified) = '" & Month(DateAdd(DateInterval.Day, -2, InpDate)) & "'  and Year(DateModified) = '" & Year(DateAdd(DateInterval.Day, -2, InpDate)) & "'  Group By AccountID) T3"
        strQuery = strQuery & " on T3.accountid = A.Accountid "
        strQuery = strQuery & " LEFT OUTER JOIN (Select AccountID, sum(datediff(s,0,duration)) AS Mins from tbltranscriptionmain where status='1073741824' and Day(DateModified) = '" & Day(DateAdd(DateInterval.Day, -3, InpDate)) & "' and Month(DateModified) = '" & Month(DateAdd(DateInterval.Day, -3, InpDate)) & "'  and Year(DateModified) = '" & Year(DateAdd(DateInterval.Day, -3, InpDate)) & "'  Group By AccountID) T4"
        strQuery = strQuery & " on T4.accountid = A.Accountid "
        strQuery = strQuery & " LEFT OUTER JOIN (Select AccountID, sum(datediff(s,0,duration)) AS Mins from tbltranscriptionmain where status='1073741824' and Day(DateModified) = '" & Day(DateAdd(DateInterval.Day, -4, InpDate)) & "' and Month(DateModified) = '" & Month(DateAdd(DateInterval.Day, -4, InpDate)) & "'  and Year(DateModified) = '" & Year(DateAdd(DateInterval.Day, -4, InpDate)) & "'  Group By AccountID) T5"
        strQuery = strQuery & " on T5.accountid = A.Accountid "
        strQuery = strQuery & " LEFT OUTER JOIN (Select AccountID, sum(datediff(s,0,duration)) AS Mins from tbltranscriptionmain where status='1073741824' and Day(DateModified) = '" & Day(DateAdd(DateInterval.Day, -5, InpDate)) & "' and Month(DateModified) = '" & Month(DateAdd(DateInterval.Day, -5, InpDate)) & "'  and Year(DateModified) = '" & Year(DateAdd(DateInterval.Day, -5, InpDate)) & "'  Group By AccountID) T6"
        strQuery = strQuery & " on T6.accountid = A.Accountid "
        strQuery = strQuery & " LEFT OUTER JOIN (Select AccountID, sum(datediff(s,0,duration)) AS Mins from tbltranscriptionmain where status='1073741824' and Day(DateModified) = '" & Day(DateAdd(DateInterval.Day, -6, InpDate)) & "' and Month(DateModified) = '" & Month(DateAdd(DateInterval.Day, -6, InpDate)) & "'  and Year(DateModified) = '" & Year(DateAdd(DateInterval.Day, -6, InpDate)) & "'  Group By AccountID) T7"
        strQuery = strQuery & " on T7.accountid = A.Accountid "
        strQuery = strQuery & " LEFT OUTER JOIN (Select AccountID, sum(datediff(s,0,duration)) AS Mins from tbltranscriptionmain where status='1073741824' and Month(DateModified) = '" & Month(DateAdd(DateInterval.Month, -1, InpDate)) & "' and Year(DateModified) = '" & Year(DateAdd(DateInterval.Month, -1, InpDate)) & "' Group By AccountID) PM"
        strQuery = strQuery & " on PM.accountid = A.Accountid "
        strQuery = strQuery & " LEFT OUTER JOIN (Select AccountID, sum(datediff(s,0,duration)) AS Mins from tbltranscriptionmain where status='1073741824' and DateModified >= '" & CurrSDate & "' and DateModified < '" & CurrEDate.AddDays(1) & "'  Group By AccountID) CM"
        strQuery = strQuery & " on CM.accountid = A.Accountid where A.contractorID ='" & Session("contractorID").ToString & "' and (A.indirect = 'False' or A.Indirect is null)  and (A.isdeleted is null or a.isdeleted = 'False') and A.MISRep = 'True'     "
        strQuery = strQuery & " UNION Select A.Accountid, A.Description, IsNull(PM.Mins,0) AS PrevMonthMins, IsNull(CM.Mins,0) AS CurrMonthMins, IsNull(T1.Mins, 0) as DayMins1, IsNull(T2.Mins,0) as DayMins2, IsNull(T3.Mins,0) as DayMins3, IsNull(T4.Mins,0) as DayMins4, IsNull(T5.Mins,0) as DayMins5, IsNull(T6.Mins,0) as DayMins6, IsNull(T7.Mins,0) as DayMins7, C.priority, C.Description as CateDescr, A.Indirect "
        strQuery = strQuery & " from tblaccounts A "
        strQuery = strQuery & " LEFT OUTER JOIN (Select * from tblActCategories) C"
        strQuery = strQuery & " on C.category = A.Category "
        strQuery = strQuery & " LEFT OUTER JOIN (Select AccountID, sum(punits) AS Mins from tblIndirectActUnits where Day(SubmitDate) = '" & Day(InpDate) & "' and Month(SubmitDate) = '" & Month(InpDate) & "' and Year(SubmitDate) =  '" & Year(InpDate) & "' Group By AccountID) T1"
        strQuery = strQuery & " on T1.accountid = A.Accountid "
        strQuery = strQuery & " LEFT OUTER JOIN (Select AccountID, sum(punits) AS Mins from tblIndirectActUnits where Day(SubmitDate) = '" & Day(DateAdd(DateInterval.Day, -1, InpDate)) & "' and Month(SubmitDate) = '" & Month(DateAdd(DateInterval.Day, -1, InpDate)) & "' and Year(Submitdate) = '" & Year(DateAdd(DateInterval.Day, -1, InpDate)) & "'  Group By AccountID) T2"
        strQuery = strQuery & " on T2.accountid = A.Accountid "
        strQuery = strQuery & " LEFT OUTER JOIN (Select AccountID, sum(punits) AS Mins from tblIndirectActUnits where Day(SubmitDate) = '" & Day(DateAdd(DateInterval.Day, -2, InpDate)) & "' and Month(Submitdate) = '" & Month(DateAdd(DateInterval.Day, -2, InpDate)) & "'  and Year(Submitdate) = '" & Year(DateAdd(DateInterval.Day, -2, InpDate)) & "'  Group By AccountID) T3"
        strQuery = strQuery & " on T3.accountid = A.Accountid "
        strQuery = strQuery & " LEFT OUTER JOIN (Select AccountID, sum(punits) AS Mins from tblIndirectActUnits where Day(SubmitDate) = '" & Day(DateAdd(DateInterval.Day, -3, InpDate)) & "' and Month(SubmitDate) = '" & Month(DateAdd(DateInterval.Day, -3, InpDate)) & "'  and Year(SubmitDate) = '" & Year(DateAdd(DateInterval.Day, -3, InpDate)) & "'  Group By AccountID) T4"
        strQuery = strQuery & " on T4.accountid = A.Accountid "
        strQuery = strQuery & " LEFT OUTER JOIN (Select AccountID, sum(punits) AS Mins from tblIndirectActUnits where Day(SubmitDate) = '" & Day(DateAdd(DateInterval.Day, -4, InpDate)) & "' and Month(SubmitDate) = '" & Month(DateAdd(DateInterval.Day, -4, InpDate)) & "'  and Year(SubmitDate) = '" & Year(DateAdd(DateInterval.Day, -4, InpDate)) & "'  Group By AccountID) T5"
        strQuery = strQuery & " on T5.accountid = A.Accountid "
        strQuery = strQuery & " LEFT OUTER JOIN (Select AccountID, sum(punits) AS Mins from tblIndirectActUnits where Day(SubmitDate) = '" & Day(DateAdd(DateInterval.Day, -5, InpDate)) & "' and Month(SubmitDate) = '" & Month(DateAdd(DateInterval.Day, -5, InpDate)) & "'  and Year(SubmitDate) = '" & Year(DateAdd(DateInterval.Day, -5, InpDate)) & "'  Group By AccountID) T6"
        strQuery = strQuery & " on T6.accountid = A.Accountid "
        strQuery = strQuery & " LEFT OUTER JOIN (Select AccountID, sum(punits) AS Mins from tblIndirectActUnits where Day(SubmitDate) = '" & Day(DateAdd(DateInterval.Day, -6, InpDate)) & "' and Month(SubmitDate) = '" & Month(DateAdd(DateInterval.Day, -6, InpDate)) & "'  and Year(SubmitDate) = '" & Year(DateAdd(DateInterval.Day, -6, InpDate)) & "'  Group By AccountID) T7"
        strQuery = strQuery & " on T7.accountid = A.Accountid "
        strQuery = strQuery & " LEFT OUTER JOIN (Select AccountID, sum(punits) AS Mins from tblIndirectActUnits where Month(SubmitDate) = '" & Month(DateAdd(DateInterval.Month, -1, InpDate)) & "' and Year(SubmitDate) = '" & Year(DateAdd(DateInterval.Month, -1, InpDate)) & "' Group By AccountID) PM"
        strQuery = strQuery & " on PM.accountid = A.Accountid "
        strQuery = strQuery & " LEFT OUTER JOIN (Select AccountID, sum(punits) AS Mins from tblIndirectActUnits where  submitdate >= '" & CurrSDate & "' and submitdate < '" & CurrEDate.AddDays(1) & "'  Group By AccountID) CM"
        strQuery = strQuery & " on CM.accountid = A.Accountid  where   A.contractorID ='" & Session("contractorID").ToString & "' and A.Indirect = 'True'  and (A.isdeleted is null or A.isdeleted = 'False') and A.MISRep = 'True'  order by priority, A.description"
        'Response.Write(strQuery)
        'Response.End()


        Dim SQLCmd1 As New SqlCommand(strQuery, New SqlConnection(strConn))
        Try
            SQLCmd1.Connection.Open()
            Dim DRRec1 As SqlDataReader = SQLCmd1.ExecuteReader()
            If DRRec1.HasRows Then

                While (DRRec1.Read)
                    I = I + 1
                    If I = 1 Then
                        strCategory = DRRec1("CateDescr").ToString
                        Dim Row2 As New TableRow
                        Row2.HorizontalAlign = HorizontalAlign.Center
                        Row2.CssClass = "tblbgbody"
                        Row2.Font.Bold = True
                        Row2.Font.Italic = True
                        Row2.ForeColor = Drawing.Color.White
                        Row2.Font.Size = "10"
                        Dim CatCell As New TableCell
                        CatCell.ColumnSpan = "10"
                        CatCell.CssClass = "HeaderDiv"
                        CatCell.Text = DRRec1("CateDescr").ToString
                        Row2.Cells.Add(CatCell)
                        tblMins.Rows.Add(Row2)
                    Else
                        If strCategory <> DRRec1("CateDescr").ToString Then
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
                            ARow1.HorizontalAlign = HorizontalAlign.Center
                            'ARow1.CssClass = "tblbg"
                            ARow1.Font.Bold = True
                            ACell1.Text = "SubTotal"
                            ACell1.CssClass = "alt1"
                            ACell2.Text = FormatNumber(STAvgPreMins, 0).Replace(",", "")
                            ACell2.CssClass = "alt2"
                            ACell3.Text = FormatNumber(STAvgCurrMins, 0).Replace(",", "")
                            ACell3.CssClass = "alt2"
                            ACell4.Text = FormatNumber(STDayMins1, 0).Replace(",", "")
                            ACell4.CssClass = "alt2"
                            ACell5.Text = FormatNumber(STDayMins2, 0).Replace(",", "")
                            ACell5.CssClass = "alt2"
                            ACell6.Text = FormatNumber(STDayMins3, 0).Replace(",", "")
                            ACell6.CssClass = "alt2"
                            ACell7.Text = FormatNumber(STDayMins4, 0).Replace(",", "")
                            ACell7.CssClass = "alt2"
                            ACell8.Text = FormatNumber(STDayMins5, 0).Replace(",", "")
                            ACell8.CssClass = "alt2"
                            ACell9.Text = FormatNumber(STDayMins6, 0).Replace(",", "")
                            ACell9.CssClass = "alt2"
                            ACell10.Text = FormatNumber(STDayMins7, 0).Replace(",", "")
                            ACell10.CssClass = "alt2"


                            ARow1.Cells.Add(ACell1)
                            ARow1.Cells.Add(ACell2)
                            ARow1.Cells.Add(ACell3)
                            ARow1.Cells.Add(ACell4)
                            ARow1.Cells.Add(ACell5)
                            ARow1.Cells.Add(ACell6)
                            ARow1.Cells.Add(ACell7)
                            ARow1.Cells.Add(ACell8)
                            ARow1.Cells.Add(ACell9)
                            ARow1.Cells.Add(ACell10)
                            tblMins.Rows.Add(ARow1)

                            strCategory = DRRec1("CateDescr").ToString
                            Dim Row2 As New TableRow
                            Row2.HorizontalAlign = HorizontalAlign.Center
                            'Row2.CssClass = "tblbgbody"
                            Row2.Font.Bold = True
                            Row2.Font.Italic = True
                            Row2.ForeColor = Drawing.Color.White
                            Row2.Font.Size = "10"
                            Dim CatCell As New TableCell
                            CatCell.ColumnSpan = "10"
                            CatCell.Text = DRRec1("CateDescr").ToString
                            Row2.Cells.Add(CatCell)
                            tblMins.Rows.Add(Row2)

                            STAvgPreMins = 0
                            STAvgCurrMins = 0
                            STDayMins1 = 0
                            STDayMins2 = 0
                            STDayMins3 = 0
                            STDayMins4 = 0
                            STDayMins5 = 0
                            STDayMins6 = 0
                            STDayMins7 = 0
                        End If
                    End If
                    If DRRec1("Indirect").ToString = "True" Then
                        STAvgPreMins += FormatNumber(DRRec1(2).ToString / PrvWDays, 0).Replace(",", "")
                        STAvgCurrMins += FormatNumber(DRRec1(3).ToString / CurrWDays, 0).Replace(",", "")
                        STDayMins1 += FormatNumber(DRRec1(4), 0).Replace(",", "")
                        STDayMins2 += FormatNumber(DRRec1(5), 0).Replace(",", "")
                        STDayMins3 += FormatNumber(DRRec1(6), 0).Replace(",", "")
                        STDayMins4 += FormatNumber(DRRec1(7), 0).Replace(",", "")
                        STDayMins5 += FormatNumber(DRRec1(8), 0).Replace(",", "")
                        STDayMins6 += FormatNumber(DRRec1(9), 0).Replace(",", "")
                        STDayMins7 += FormatNumber(DRRec1(10), 0).Replace(",", "")
                        TAvgPreMins += FormatNumber((DRRec1(2).ToString) / PrvWDays, 0).Replace(",", "")
                        TAvgCurrMins += FormatNumber((DRRec1(3).ToString) / CurrWDays, 0).Replace(",", "")
                        TDayMins1 += FormatNumber(DRRec1(4), 0).Replace(",", "")
                        TDayMins2 += FormatNumber(DRRec1(5), 0).Replace(",", "")
                        TDayMins3 += FormatNumber(DRRec1(6), 0).Replace(",", "")
                        TDayMins4 += FormatNumber(DRRec1(7), 0).Replace(",", "")
                        TDayMins5 += FormatNumber(DRRec1(8), 0).Replace(",", "")
                        TDayMins6 += FormatNumber(DRRec1(9), 0).Replace(",", "")
                        TDayMins7 += FormatNumber(DRRec1(10), 0).Replace(",", "")

                    Else
                        STAvgPreMins += FormatNumber(DRRec1(2).ToString / 60 / PrvWDays, 0).Replace(",", "")
                        STAvgCurrMins += FormatNumber(DRRec1(3).ToString / 60 / CurrWDays, 0).Replace(",", "")
                        STDayMins1 += FormatNumber(DRRec1(4) / 60, 0).Replace(",", "")
                        STDayMins2 += FormatNumber(DRRec1(5) / 60, 0).Replace(",", "")
                        STDayMins3 += FormatNumber(DRRec1(6) / 60, 0).Replace(",", "")
                        STDayMins4 += FormatNumber(DRRec1(7) / 60, 0).Replace(",", "")
                        STDayMins5 += FormatNumber(DRRec1(8) / 60, 0).Replace(",", "")
                        STDayMins6 += FormatNumber(DRRec1(9) / 60, 0).Replace(",", "")
                        STDayMins7 += FormatNumber(DRRec1(10) / 60, 0).Replace(",", "")
                        TAvgPreMins += FormatNumber((DRRec1(2).ToString / 60) / PrvWDays, 0).Replace(",", "")
                        TAvgCurrMins += FormatNumber((DRRec1(3).ToString / 60) / CurrWDays, 0).Replace(",", "")
                        TDayMins1 += FormatNumber(DRRec1(4) / 60, 0).Replace(",", "")
                        TDayMins2 += FormatNumber(DRRec1(5) / 60, 0).Replace(",", "")
                        TDayMins3 += FormatNumber(DRRec1(6) / 60, 0).Replace(",", "")
                        TDayMins4 += FormatNumber(DRRec1(7) / 60, 0).Replace(",", "")
                        TDayMins5 += FormatNumber(DRRec1(8) / 60, 0).Replace(",", "")
                        TDayMins6 += FormatNumber(DRRec1(9) / 60, 0).Replace(",", "")
                        TDayMins7 += FormatNumber(DRRec1(10) / 60, 0).Replace(",", "")
                    End If
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
                    If DRRec1("Indirect").ToString = "True" Then
                        Cell1.HorizontalAlign = HorizontalAlign.Left
                        Cell1.Text = DRRec1(1).ToString
                        Cell2.Text = FormatNumber((DRRec1(2).ToString) / PrvWDays, 0).Replace(",", "")
                        Cell3.Text = FormatNumber((DRRec1(3).ToString) / CurrWDays, 0).Replace(",", "")
                        Cell2.ToolTip = "Working Days: " & PrvWDays
                        Cell3.ToolTip = "Working Days: " & CurrWDays
                        Cell4.Text = FormatNumber(DRRec1(4).ToString, 0).Replace(",", "")
                        Cell5.Text = FormatNumber(DRRec1(5).ToString, 0).Replace(",", "")
                        Cell6.Text = FormatNumber(DRRec1(6).ToString, 0).Replace(",", "")
                        Cell7.Text = FormatNumber(DRRec1(7).ToString, 0).Replace(",", "")
                        Cell8.Text = FormatNumber(DRRec1(8).ToString, 0).Replace(",", "")
                        Cell9.Text = FormatNumber(DRRec1(9).ToString, 0).Replace(",", "")
                        Cell10.Text = FormatNumber(DRRec1(10).ToString, 0).Replace(",", "")
                    Else
                        Cell1.HorizontalAlign = HorizontalAlign.Left
                        Cell1.Text = "<a href='DailyMinsPost.aspx?showDict=Yes&accountid=" & DRRec1(0).ToString & "&InpDate=" & InpDate & "'>" & DRRec1(1).ToString & "</a>"
                        Cell2.Text = FormatNumber((DRRec1(2).ToString / 60) / PrvWDays, 0).Replace(",", "")
                        Cell3.Text = FormatNumber((DRRec1(3).ToString / 60) / CurrWDays, 0).Replace(",", "")
                        Cell2.ToolTip = "Working Days: " & PrvWDays
                        Cell3.ToolTip = "Working Days: " & CurrWDays
                        Cell4.Text = FormatNumber(DRRec1(4).ToString / 60, 0).Replace(",", "")
                        Cell5.Text = FormatNumber(DRRec1(5).ToString / 60, 0).Replace(",", "")
                        Cell6.Text = FormatNumber(DRRec1(6).ToString / 60, 0).Replace(",", "")
                        Cell7.Text = FormatNumber(DRRec1(7).ToString / 60, 0).Replace(",", "")
                        Cell8.Text = FormatNumber(DRRec1(8).ToString / 60, 0).Replace(",", "")
                        Cell9.Text = FormatNumber(DRRec1(9).ToString / 60, 0).Replace(",", "")
                        Cell10.Text = FormatNumber(DRRec1(10).ToString / 60, 0).Replace(",", "")
                    End If
                    'Row1.CssClass = "tblbg2"
                    'Cell2.CssClass = "tblbg3"
                    'Cell3.CssClass = "tblbg4"
                    Row1.Cells.Add(Cell1)
                    Row1.Cells.Add(Cell2)
                    Cell2.Style.Add("text-align", "right")
                    Row1.Cells.Add(Cell3)
                    Cell3.Style.Add("text-align", "right")
                    Row1.Cells.Add(Cell4)
                    Cell4.Style.Add("text-align", "right")
                    Row1.Cells.Add(Cell5)
                    Cell5.Style.Add("text-align", "right")
                    Row1.Cells.Add(Cell6)
                    Cell6.Style.Add("text-align", "right")
                    Row1.Cells.Add(Cell7)
                    Cell7.Style.Add("text-align", "right")
                    Row1.Cells.Add(Cell8)
                    Cell8.Style.Add("text-align", "right")
                    Row1.Cells.Add(Cell9)
                    Cell9.Style.Add("text-align", "right")
                    Row1.Cells.Add(Cell10)
                    Cell10.Style.Add("text-align", "right")
                    tblMins.Rows.Add(Row1)

                End While
                Dim A2Row1 As New TableRow
                Dim A2Cell1 As New TableCell
                Dim A2Cell2 As New TableCell
                Dim A2Cell3 As New TableCell
                Dim A2Cell4 As New TableCell
                Dim A2Cell5 As New TableCell
                Dim A2Cell6 As New TableCell
                Dim A2Cell7 As New TableCell
                Dim A2Cell8 As New TableCell
                Dim A2Cell9 As New TableCell
                Dim A2Cell10 As New TableCell
                A2Row1.HorizontalAlign = HorizontalAlign.Center
                'A2Row1.CssClass = "tblbg"
                A2Cell1.Text = "SubTotal"
                A2Cell1.CssClass = "alt1"
                A2Row1.Font.Bold = True
                A2Cell2.Text = FormatNumber(STAvgPreMins, 0).Replace(",", "")
                A2Cell2.CssClass = "alt2"
                A2Cell3.Text = FormatNumber(STAvgCurrMins, 0).Replace(",", "")
                A2Cell3.CssClass = "alt2"
                A2Cell4.Text = FormatNumber(STDayMins1, 0).Replace(",", "")
                A2Cell4.CssClass = "alt2"
                A2Cell5.Text = FormatNumber(STDayMins2, 0).Replace(",", "")
                A2Cell5.CssClass = "alt2"
                A2Cell6.Text = FormatNumber(STDayMins3, 0).Replace(",", "")
                A2Cell6.CssClass = "alt2"
                A2Cell7.Text = FormatNumber(STDayMins4, 0).Replace(",", "")
                A2Cell7.CssClass = "alt2"
                A2Cell8.Text = FormatNumber(STDayMins5, 0).Replace(",", "")
                A2Cell8.CssClass = "alt2"
                A2Cell9.Text = FormatNumber(STDayMins6, 0).Replace(",", "")
                A2Cell9.CssClass = "alt2"
                A2Cell10.Text = FormatNumber(STDayMins7, 0).Replace(",", "")
                A2Cell10.CssClass = "alt2"

                A2Row1.Cells.Add(A2Cell1)
                A2Row1.Cells.Add(A2Cell2)
                A2Row1.Cells.Add(A2Cell3)
                A2Row1.Cells.Add(A2Cell4)
                A2Row1.Cells.Add(A2Cell5)
                A2Row1.Cells.Add(A2Cell6)
                A2Row1.Cells.Add(A2Cell7)
                A2Row1.Cells.Add(A2Cell8)
                A2Row1.Cells.Add(A2Cell9)
                A2Row1.Cells.Add(A2Cell10)
                tblMins.Rows.Add(A2Row1)
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
                A1Row1.HorizontalAlign = HorizontalAlign.Center
                A1Row1.CssClass = "tblbgbody"
                A1Row1.Font.Bold = True

                A1Cell1.Text = "Total"
                A1Cell1.CssClass = "alt1"
                A1Cell2.Text = FormatNumber(TAvgPreMins, 0).Replace(",", "")
                A1Cell2.CssClass = "alt2"
                A1Cell3.Text = FormatNumber(TAvgCurrMins, 0).Replace(",", "")
                A1Cell3.CssClass = "alt2"
                A1Cell4.Text = FormatNumber(TDayMins1, 0).Replace(",", "")
                A1Cell4.CssClass = "alt2"
                A1Cell5.Text = FormatNumber(TDayMins2, 0).Replace(",", "")
                A1Cell5.CssClass = "alt2"
                A1Cell6.Text = FormatNumber(TDayMins3, 0).Replace(",", "")
                A1Cell6.CssClass = "alt2"
                A1Cell7.Text = FormatNumber(TDayMins4, 0).Replace(",", "")
                A1Cell7.CssClass = "alt2"
                A1Cell8.Text = FormatNumber(TDayMins5, 0).Replace(",", "")
                A1Cell8.CssClass = "alt2"
                A1Cell9.Text = FormatNumber(TDayMins6, 0).Replace(",", "")
                A1Cell9.CssClass = "alt2"
                A1Cell10.Text = FormatNumber(TDayMins7, 0).Replace(",", "")
                A1Cell10.CssClass = "alt2"

                A1Row1.Cells.Add(A1Cell1)
                A1Row1.Cells.Add(A1Cell2)
                A1Row1.Cells.Add(A1Cell3)
                A1Row1.Cells.Add(A1Cell4)
                A1Row1.Cells.Add(A1Cell5)
                A1Row1.Cells.Add(A1Cell6)
                A1Row1.Cells.Add(A1Cell7)
                A1Row1.Cells.Add(A1Cell8)
                A1Row1.Cells.Add(A1Cell9)
                A1Row1.Cells.Add(A1Cell10)
                tblMins.Rows.Add(A1Row1)
            End If
            DRRec1.Close()
        Finally
            If SQLCmd1.Connection.State = System.Data.ConnectionState.Open Then
                SQLCmd1.Connection.Close()
                SQLCmd1 = Nothing
            End If
        End Try

    End Sub



    Protected Sub ShowDictDetails(ByVal ActID As String, ByVal inpDate As Date)
        R1Cell1.Text = "Dictator"
        tblDtls.Text = "Dictator Details"
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
        Dim CurrSDate As Date
        Dim CurrEDate As Date
        Dim PrvWDays As Integer
        Dim CurrWDays As Integer
        CurrSDate = Month(inpDate) & "/1/" & Year(inpDate)
        'If Month(CurrSDate) = Month(Now) Then
        '    CurrEDate = Now.ToShortDateString
        'Else
        '    CurrEDate = DateAdd(DateInterval.Day, -1, DateAdd(DateInterval.Month, 1, CurrSDate))
        'End If
        CurrEDate = inpDate
        CurrWDays = WorkingDays(CurrSDate, CurrEDate)
        Dim PrvSDate As Date
        Dim PrvEDate As Date

        PrvSDate = DateAdd(DateInterval.Month, -1, CurrSDate)
        PrvEDate = DateAdd(DateInterval.Day, -1, CurrSDate)
        PrvWDays = WorkingDays(PrvSDate, PrvEDate)

        Dim TAvgPreMins As Integer
        Dim TAvgCurrMins As Integer
        Dim TDayMins1 As Integer
        Dim TDayMins2 As Integer
        Dim TDayMins3 As Integer
        Dim TDayMins4 As Integer
        Dim TDayMins5 As Integer
        Dim TDayMins6 As Integer
        Dim TDayMins7 As Integer
        TAvgPreMins = 0
        TAvgCurrMins = 0
        TDayMins1 = 0
        TDayMins2 = 0
        TDayMins3 = 0
        TDayMins4 = 0
        TDayMins5 = 0
        TDayMins6 = 0
        TDayMins7 = 0



        strQuery = "Select A.Accountid, U.firstname + ' ' + U.Lastname as uname, IsNull(PM.Mins,0) AS PrevMonthMins, IsNull(CM.Mins,0) AS CurrMonthMins, IsNull(T1.Mins, 0) as DayMins1, IsNull(T2.Mins,0) as DayMins2, IsNull(T3.Mins,0) as DayMins3, IsNull(T4.Mins,0) as DayMins4, IsNull(T5.Mins,0) as DayMins5, IsNull(T6.Mins,0) as DayMins6, IsNull(T7.Mins,0) as DayMins7 "
        strQuery = strQuery & " from tblaccounts A "
        strQuery = strQuery & " LEFT OUTER JOIN (Select * from tblPhysicians) U"
        strQuery = strQuery & " on U.accountid = A.accountid "
        strQuery = strQuery & " LEFT OUTER JOIN (Select DictatorID, sum(datediff(s,0,duration)) AS Mins from tbltranscriptionmain where status='1073741824' and Day(DateModified) = '" & Day(inpDate) & "' and Month(DateModified) = '" & Month(inpDate) & "' and Year(DateModified) =  '" & Year(inpDate) & "'  and accountid = '" & ActID & "'  Group By DictatorID) T1"
        strQuery = strQuery & " on T1.DictatorID = U.PhysicianID "
        strQuery = strQuery & " LEFT OUTER JOIN (Select DictatorID, sum(datediff(s,0,duration)) AS Mins from tbltranscriptionmain where status='1073741824' and Day(DateModified) = '" & Day(DateAdd(DateInterval.Day, -1, inpDate)) & "' and Month(DateModified) = '" & Month(DateAdd(DateInterval.Day, -1, inpDate)) & "' and Year(DateModified) = '" & Year(DateAdd(DateInterval.Day, -1, inpDate)) & "'   and accountid = '" & ActID & "' Group By DictatorID) T2"
        strQuery = strQuery & " on T2.DictatorID = U.PhysicianID "
        strQuery = strQuery & " LEFT OUTER JOIN (Select DictatorID, sum(datediff(s,0,duration)) AS Mins from tbltranscriptionmain where status='1073741824' and Day(DateModified) = '" & Day(DateAdd(DateInterval.Day, -2, inpDate)) & "' and Month(DateModified) = '" & Month(DateAdd(DateInterval.Day, -2, inpDate)) & "'  and Year(DateModified) = '" & Year(DateAdd(DateInterval.Day, -2, inpDate)) & "'  and accountid = '" & ActID & "'  Group By DictatorID) T3"
        strQuery = strQuery & " on T3.DictatorID = U.PhysicianID "
        strQuery = strQuery & " LEFT OUTER JOIN (Select DictatorID, sum(datediff(s,0,duration)) AS Mins from tbltranscriptionmain where status='1073741824' and Day(DateModified) = '" & Day(DateAdd(DateInterval.Day, -3, inpDate)) & "' and Month(DateModified) = '" & Month(DateAdd(DateInterval.Day, -3, inpDate)) & "'  and Year(DateModified) = '" & Year(DateAdd(DateInterval.Day, -3, inpDate)) & "'  and accountid = '" & ActID & "'  Group By DictatorID) T4"
        strQuery = strQuery & " on T4.DictatorID = U.PhysicianID "
        strQuery = strQuery & " LEFT OUTER JOIN (Select DictatorID, sum(datediff(s,0,duration)) AS Mins from tbltranscriptionmain where status='1073741824' and Day(DateModified) = '" & Day(DateAdd(DateInterval.Day, -4, inpDate)) & "' and Month(DateModified) = '" & Month(DateAdd(DateInterval.Day, -4, inpDate)) & "'  and Year(DateModified) = '" & Year(DateAdd(DateInterval.Day, -4, inpDate)) & "'  and accountid = '" & ActID & "'  Group By DictatorID) T5"
        strQuery = strQuery & " on T5.DictatorID = U.PhysicianID "
        strQuery = strQuery & " LEFT OUTER JOIN (Select DictatorID, sum(datediff(s,0,duration)) AS Mins from tbltranscriptionmain where status='1073741824' and Day(DateModified) = '" & Day(DateAdd(DateInterval.Day, -5, inpDate)) & "' and Month(DateModified) = '" & Month(DateAdd(DateInterval.Day, -5, inpDate)) & "'  and Year(DateModified) = '" & Year(DateAdd(DateInterval.Day, -5, inpDate)) & "'  and accountid = '" & ActID & "'  Group By DictatorID) T6"
        strQuery = strQuery & " on T6.DictatorID = U.PhysicianID "
        strQuery = strQuery & " LEFT OUTER JOIN (Select DictatorID, sum(datediff(s,0,duration)) AS Mins from tbltranscriptionmain where status='1073741824' and Day(DateModified) = '" & Day(DateAdd(DateInterval.Day, -6, inpDate)) & "' and Month(DateModified) = '" & Month(DateAdd(DateInterval.Day, -6, inpDate)) & "'  and Year(DateModified) = '" & Year(DateAdd(DateInterval.Day, -6, inpDate)) & "'  and accountid = '" & ActID & "'  Group By DictatorID) T7"
        strQuery = strQuery & " on T7.DictatorID = U.PhysicianID "
        strQuery = strQuery & " LEFT OUTER JOIN (Select DictatorID, sum(datediff(s,0,duration)) AS Mins from tbltranscriptionmain where status='1073741824' and Month(DateModified) = '" & Month(DateAdd(DateInterval.Month, -1, inpDate)) & "' and Year(DateModified) = '" & Year(DateAdd(DateInterval.Month, -1, inpDate)) & "' and accountid = '" & ActID & "'  Group By DictatorID) PM"
        strQuery = strQuery & " on PM.DictatorID = U.PhysicianID "
        strQuery = strQuery & " LEFT OUTER JOIN (Select DictatorID, sum(datediff(s,0,duration)) AS Mins from tbltranscriptionmain where status='1073741824' and DateModified >= '" & CurrSDate & "' and DateModified < '" & CurrEDate.AddDays(1) & "'  and accountid = '" & ActID & "'  Group By DictatorID) CM"
        strQuery = strQuery & " on  CM.DictatorID = U.PhysicianID where A.accountid = '" & ActID & "' order by U.firstname "

        'Response.Write(strQuery)

        Dim SQLCmd1 As New SqlCommand(strQuery, New SqlConnection(strConn))
        Try
            SQLCmd1.Connection.Open()
            Dim DRRec1 As SqlDataReader = SQLCmd1.ExecuteReader()


            If DRRec1.HasRows Then


                R1Cell4.Text = inpDate
                R1Cell5.Text = DateAdd(DateInterval.Day, -1, inpDate)
                R1Cell6.Text = DateAdd(DateInterval.Day, -2, inpDate)
                R1Cell7.Text = DateAdd(DateInterval.Day, -3, inpDate)
                R1Cell8.Text = DateAdd(DateInterval.Day, -4, inpDate)
                R1Cell9.Text = DateAdd(DateInterval.Day, -5, inpDate)
                R1Cell10.Text = DateAdd(DateInterval.Day, -6, inpDate)
                R2Cell1.Text = "Name"
                R2Cell3.Text = inpDate.ToString("MMM") & " " & inpDate.Year
                R2Cell2.Text = DateAdd(DateInterval.Month, -1, inpDate).ToString("MMM") & " " & DateAdd(DateInterval.Month, -1, inpDate).Year
                R2Cell2.ToolTip = PrvWDays
                R2Cell3.ToolTip = CurrWDays
                R2Cell4.Text = inpDate.ToString("ddd")
                R2Cell5.Text = DateAdd(DateInterval.Day, -1, inpDate).ToString("ddd")
                R2Cell6.Text = DateAdd(DateInterval.Day, -2, inpDate).ToString("ddd")
                R2Cell7.Text = DateAdd(DateInterval.Day, -3, inpDate).ToString("ddd")
                R2Cell8.Text = DateAdd(DateInterval.Day, -4, inpDate).ToString("ddd")
                R2Cell9.Text = DateAdd(DateInterval.Day, -5, inpDate).ToString("ddd")
                R2Cell10.Text = DateAdd(DateInterval.Day, -6, inpDate).ToString("ddd")
                While (DRRec1.Read)





                    TAvgPreMins += FormatNumber((DRRec1(2).ToString / 60) / PrvWDays, 0).Replace(",", "")
                    TAvgCurrMins += FormatNumber((DRRec1(3).ToString / 60) / CurrWDays, 0).Replace(",", "")
                    TDayMins1 += FormatNumber(DRRec1(4), 0).Replace(",", "")
                    TDayMins2 += FormatNumber(DRRec1(5), 0).Replace(",", "")
                    TDayMins3 += FormatNumber(DRRec1(6), 0).Replace(",", "")
                    TDayMins4 += FormatNumber(DRRec1(7), 0).Replace(",", "")
                    TDayMins5 += FormatNumber(DRRec1(8), 0).Replace(",", "")
                    TDayMins6 += FormatNumber(DRRec1(9), 0).Replace(",", "")
                    TDayMins7 += FormatNumber(DRRec1(10), 0).Replace(",", "")

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
                    Cell1.HorizontalAlign = HorizontalAlign.Left
                    Cell1.Text = DRRec1(1).ToString
                    Cell2.Text = FormatNumber((DRRec1(2).ToString / 60) / PrvWDays, 0).Replace(",", "")
                    Cell3.Text = FormatNumber((DRRec1(3).ToString / 60) / CurrWDays, 0).Replace(",", "")
                    Cell2.Text = FormatNumber((DRRec1(2).ToString / 60) / PrvWDays, 0).Replace(",", "")
                    Cell3.Text = FormatNumber((DRRec1(3).ToString / 60) / CurrWDays, 0).Replace(",", "")

                    Cell4.Text = FormatNumber(DRRec1(4).ToString / 60, 0).Replace(",", "")
                    Cell5.Text = FormatNumber(DRRec1(5).ToString / 60, 0).Replace(",", "")
                    Cell6.Text = FormatNumber(DRRec1(6).ToString / 60, 0).Replace(",", "")
                    Cell7.Text = FormatNumber(DRRec1(7).ToString / 60, 0).Replace(",", "")
                    Cell8.Text = FormatNumber(DRRec1(8).ToString / 60, 0).Replace(",", "")
                    Cell9.Text = FormatNumber(DRRec1(9).ToString / 60, 0).Replace(",", "")
                    Cell10.Text = FormatNumber(DRRec1(10).ToString / 60, 0).Replace(",", "")
                    Row1.CssClass = "tblbg2"
                    Cell2.CssClass = "tblbg3"
                    Cell3.CssClass = "tblbg4"
                    Row1.Cells.Add(Cell1)
                    Row1.Cells.Add(Cell2)
                    Row1.Cells.Add(Cell3)
                    Row1.Cells.Add(Cell4)
                    Row1.Cells.Add(Cell5)
                    Row1.Cells.Add(Cell6)
                    Row1.Cells.Add(Cell7)
                    Row1.Cells.Add(Cell8)
                    Row1.Cells.Add(Cell9)
                    Row1.Cells.Add(Cell10)
                    tblMins.Rows.Add(Row1)

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
                A1Row1.HorizontalAlign = HorizontalAlign.Center
                A1Row1.CssClass = "tblbgbody"
                A1Row1.Font.Bold = True

                A1Cell1.Text = "Total"
                A1Cell2.Text = FormatNumber(TAvgPreMins, 0).Replace(",", "")
                A1Cell3.Text = FormatNumber(TAvgCurrMins, 0).Replace(",", "")
                A1Cell4.Text = FormatNumber(TDayMins1 / 60, 0).Replace(",", "")
                A1Cell5.Text = FormatNumber(TDayMins2 / 60, 0).Replace(",", "")
                A1Cell6.Text = FormatNumber(TDayMins3 / 60, 0).Replace(",", "")
                A1Cell7.Text = FormatNumber(TDayMins4 / 60, 0).Replace(",", "")
                A1Cell8.Text = FormatNumber(TDayMins5 / 60, 0).Replace(",", "")
                A1Cell9.Text = FormatNumber(TDayMins6 / 60, 0).Replace(",", "")
                A1Cell10.Text = FormatNumber(TDayMins7 / 60, 0).Replace(",", "")

                A1Row1.Cells.Add(A1Cell1)
                A1Row1.Cells.Add(A1Cell2)
                A1Row1.Cells.Add(A1Cell3)
                A1Row1.Cells.Add(A1Cell4)
                A1Row1.Cells.Add(A1Cell5)
                A1Row1.Cells.Add(A1Cell6)
                A1Row1.Cells.Add(A1Cell7)
                A1Row1.Cells.Add(A1Cell8)
                A1Row1.Cells.Add(A1Cell9)
                A1Row1.Cells.Add(A1Cell10)
                tblMins.Rows.Add(A1Row1)
            End If
            DRRec1.Close()
        Finally
            If SQLCmd1.Connection.State = System.Data.ConnectionState.Open Then
                SQLCmd1.Connection.Close()
                SQLCmd1 = Nothing
            End If
        End Try

    End Sub



    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            TxtDate.Text = Now.ToShortDateString
            'CalendarExtender1.SelectedDate = TxtDate.Text
            Dim strconn As String
            strconn = System.Configuration.ConfigurationManager.AppSettings("ETSCon")
            Dim SQLCmd As New SqlCommand("Select * from tblaccounts  where  contractorID ='" & Session("contractorID").ToString & "' and (isdeleted is null or isdeleted = 'False')   and MISRep = 'True' order by description", New SqlConnection(strconn))
            Try
                SQLCmd.Connection.Open()
                Dim DRRec As SqlDataReader = SQLCmd.ExecuteReader()
                If DRRec.HasRows = True Then
                    While (DRRec.Read)
                        Dim LI As New ListItem
                        LI.Text = DRRec("Description").ToString
                        LI.Value = DRRec("AccountID").ToString
                        DLAct.Items.Add(LI)
                    End While
                End If
                DRRec.Close()
            Finally
                If SQLCmd.Connection.State = System.Data.ConnectionState.Open Then
                    SQLCmd.Connection.Close()
                    SQLCmd = Nothing
                End If
            End Try

            If Request("showDict") = "Yes" Then
                Dim AccountID As String
                Dim InpDate As Date
                If TxtDate.Text = "" Then
                    InpDate = Date.Parse(Request("InpDate"))
                    TxtDate.Text = Request("InpDate")
                Else
                    InpDate = Date.Parse(TxtDate.Text)
                End If
                Dim i As Integer
                For i = 0 To DLAct.Items.Count - 1

                    DLAct.Items(i).Selected = False

                Next
                For i = 1 To DLAct.Items.Count - 1
                    If DLAct.Items(i).Value = Request("AccountID") Then
                        DLAct.Items(i).Selected = True
                        Exit For
                    End If
                Next

                AccountID = Request("AccountID").ToString
                ShowDictDetails(AccountID, InpDate)
            End If

        End If


    End Sub

    Protected Sub tblsubmit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles tblsubmit.Click
        If DLAct.SelectedValue <> "" Then
            Dim AccountID As String
            Dim InpDate As Date
            If TxtDate.Text = "" Then
                InpDate = Date.Parse(Now.ToShortDateString)
            Else
                InpDate = Date.Parse(TxtDate.Text)
            End If

            AccountID = DLAct.SelectedValue.ToString
            ShowDictDetails(AccountID, InpDate)
        Else
            ShowActDetails()
        End If
        If TxtDate.Text = "" Then

            TxtDate.Text = Now.ToShortDateString

        End If
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

    Protected Sub Button1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button1.Click
        Dim filename As String
        filename = "Demo Log " & Month(Now) & Day(Now) & Year(Now) & ".xls"
        Dim attachment As String = "attachment; filename=" & filename
        Response.ClearContent()
        Response.AddHeader("content-disposition", attachment)
        Response.ContentType = "application/ms-excel"
        Dim sw As New StringWriter()
        Dim htw As New HtmlTextWriter(sw)
        Dim TxCell As String
        ShowActDetails()
        'Dim Table1 As New Table
        'Table1.BackColor = Drawing.Color.Snow
        'Table1.ForeColor = Drawing.Color.Firebrick
        'Table1.GridLines = GridLines.Both
        'Table1.Font.Name = "Trebuchet MS"
        'Table1.Font.Italic = True
        'Table1.Font.Size = "9"
        'Table1.Font.Underline = False

        'Dim x As Integer
        ''If (Not (MyDataGrid.HeaderRow) Is Nothing) Then
        ''    Dim TRow1 As New TableRow
        ''    For x = 0 To MyDataGrid.HeaderRow.Cells.Count - 1
        ''        If MyDataGrid.Columns(x).Visible = True Then
        ''            Dim TCell1 As New TableCell
        ''            TCell1.Text = MyDataGrid.HeaderRow.Cells(x).Text
        ''            TCell1.Font.Bold = True
        ''            TCell1.BackColor = Drawing.Color.Gray
        ''            TCell1.ForeColor = Drawing.Color.White
        ''            TRow1.Cells.Add(TCell1)
        ''        End If
        ''    Next
        ''    Table1.Rows.Add(TRow1)
        ''End If
        'Dim i As Integer
        'Dim k As Integer
        'k = 0


        'For Each row As TableRow In tblMins.Rows
        '    k = k + 1
        '    Dim TRow1 As New TableRow


        '    For i = 0 To row.Cells.Count - 1
        '        '   If tblMins.Columns(i).Visible = True Then

        '        Dim TCell1 As New TableCell
        '        TxCell = row.Cells(i).Text
        '        If k = 1 Then
        '            TCell1.ColumnSpan = 10
        '            TCell1.HorizontalAlign = HorizontalAlign.Center
        '        Else
        '            If i = 0 Then
        '                Response.Write(TxCell)
        '                TCell1.HorizontalAlign = HorizontalAlign.Left
        '                TCell1.Font.Bold = True
        '                TCell1.Font.Underline = False
        '            Else
        '                TCell1.HorizontalAlign = HorizontalAlign.Center
        '            End If
        '        End If


        '        TCell1.Text = TxCell
        '        TRow1.Cells.Add(TCell1)
        '        'End If
        '    Next
        '    Table1.Rows.Add(TRow1)
        '    'If MyDataGrid.AllowPaging = True And MyDataGrid.PageSize = k Then
        '    '    Exit For
        '    'End If
        'Next
        tblMins.RenderControl(htw)
        'MyDataGrid.RenderControl(htw)
        Response.Write(sw.ToString())
        Response.[End]()

    End Sub
End Class
