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
    Private sDate As Date
    Private eDate As Date

    Protected Sub ShowActDetails(ByVal StartDate As Date, ByVal EndDate As Date, ByVal Export As Boolean)
        Dim strConn As String
        Dim strCategory As String = String.Empty
        Dim lHrs As Long
        Dim lMinutes As Long
        Dim lSeconds As Long
        Dim ATAT As String
        Dim MINTAT As String
        Dim MAXTAT As String
        Dim altrow As Boolean = False
        strConn = System.Configuration.ConfigurationManager.AppSettings("ETSCon")
        Dim DS As New Data.DataSet
        Dim clsMIS As ETS.BL.MISReports
        Try
            clsMIS = New ETS.BL.MISReports
            'Response.Write(StartDate & "','" & EndDate & "','" & Session("contractorID").ToString & "','" & Session("WorkGroupID").ToString)
            DS = clsMIS.GetDTRPostByParm(StartDate, EndDate, New System.Guid(Session("contractorID").ToString), Session("WorkGroupID").ToString, DLInstance.SelectedValue)
            If DS.Tables.Count > 0 Then
                If DS.Tables(0).Rows.Count > 0 Then
                    For Each DRRecT As DataRow In DS.Tables(0).Rows
                        If DRRecT(3).ToString > 0 Then
                            lSeconds = DRRecT(12).ToString
                            lHrs = Int(lSeconds / 3600)
                            lMinutes = (Int(lSeconds / 60)) - (lHrs * 60)
                            lSeconds = Int(lSeconds Mod 60)
                            If lSeconds = 60 Then
                                lMinutes = lMinutes + 1
                                lSeconds = 0
                            End If

                            If lMinutes = 60 Then
                                lMinutes = 0
                                lHrs = lHrs + 1
                            End If

                            ATAT = Format(lHrs, "00") & ":" & _
                              Format(lMinutes, "00") & ":" & _
                              Format(lSeconds, "00")

                            lSeconds = DRRecT(13).ToString
                            lHrs = Int(lSeconds / 3600)
                            lMinutes = (Int(lSeconds / 60)) - (lHrs * 60)
                            lSeconds = Int(lSeconds Mod 60)
                            If lSeconds = 60 Then
                                lMinutes = lMinutes + 1
                                lSeconds = 0
                            End If

                            If lMinutes = 60 Then
                                lMinutes = 0
                                lHrs = lHrs + 1
                            End If

                            MINTAT = Format(lHrs, "00") & ":" & _
                              Format(lMinutes, "00") & ":" & _
                              Format(lSeconds, "00")

                            lSeconds = DRRecT(14).ToString
                            lHrs = Int(lSeconds / 3600)
                            lMinutes = (Int(lSeconds / 60)) - (lHrs * 60)
                            lSeconds = Int(lSeconds Mod 60)
                            If lSeconds = 60 Then
                                lMinutes = lMinutes + 1
                                lSeconds = 0
                            End If

                            If lMinutes = 60 Then
                                lMinutes = 0
                                lHrs = lHrs + 1
                            End If

                            MAXTAT = Format(lHrs, "00") & ":" & _
                              Format(lMinutes, "00") & ":" & _
                              Format(lSeconds, "00")
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
                            Dim Cell19 As New TableCell
                            If Export = False Then
                                Cell1.Text = "<a href='DailyTATPost.aspx?AccountID=" & DRRecT(0).ToString & "&showDict=Yes&startdate=" & StartDate & "&EndDate=" & EndDate & "'>" & DRRecT(1).ToString & "</a>"
                            Else
                                Cell1.Text = DRRecT(1).ToString
                            End If
                            Cell1.HorizontalAlign = HorizontalAlign.Left

                            Cell2.Text = FormatNumber((DRRecT(3).ToString), 0)
                            Cell3.Text = FormatNumber((DRRecT(5).ToString), 0)
                            Cell4.Text = FormatNumber((DRRecT(7).ToString), 0)
                            Cell5.Text = FormatNumber((DRRecT(9).ToString), 0)
                            Cell6.Text = FormatNumber((DRRecT(11).ToString), 0)
                            Cell7.Text = ATAT
                            Cell8.Text = MINTAT
                            Cell9.Text = MAXTAT
                            Cell10.Text = FormatNumber((DRRecT(2).ToString / 60), 0)
                            Cell11.Text = FormatNumber((DRRecT(4).ToString / 60), 0)
                            Cell12.Text = FormatNumber((DRRecT(6).ToString / 60), 0)
                            Cell13.Text = FormatNumber((DRRecT(8).ToString / 60), 0)
                            Cell14.Text = FormatNumber((DRRecT(10).ToString / 60), 0)
                            Cell15.Text = Convert.ToInt32(FormatNumber((DRRecT(5).ToString), 0) * 100 / FormatNumber((DRRecT(3).ToString), 0)) & "%"
                            Cell16.Text = Convert.ToInt32(FormatNumber((DRRecT(7).ToString), 0) * 100 / FormatNumber((DRRecT(3).ToString), 0)) & "%"
                            Cell17.Text = Convert.ToInt32(FormatNumber((DRRecT(9).ToString), 0) * 100 / FormatNumber((DRRecT(3).ToString), 0)) & "%"
                            Cell18.Text = Convert.ToInt32(FormatNumber((DRRecT(11).ToString), 0) * 100 / FormatNumber((DRRecT(3).ToString), 0)) & "%"

                            Cell7.Text = ATAT
                            Cell19.Text = DRRecT("Median").ToString

                            Row1.CssClass = "tblbg2"
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
                            Row1.Cells.Add(Cell11)
                            Row1.Cells.Add(Cell12)
                            Row1.Cells.Add(Cell13)
                            Row1.Cells.Add(Cell14)
                            Row1.Cells.Add(Cell15)
                            Row1.Cells.Add(Cell16)
                            Row1.Cells.Add(Cell17)
                            Row1.Cells.Add(Cell18)
                            Row1.Cells.Add(Cell19)
                            If altrow = False Then
                                Row1.CssClass = "gridalt2"
                                altrow = True
                            Else
                                Row1.CssClass = "gridalt1"
                                altrow = False

                            End If
                            tblMins.Rows.Add(Row1)

                        End If
                    Next
                End If
            End If
        Catch ex As Exception
            Response.Write(ex.Message)
        Finally
            clsMIS = Nothing
            DS.Dispose()
        End Try



    End Sub

    Protected Sub ShowDictDetails(ByVal ActID As String, ByVal StartDate As Date, ByVal EndDate As Date, ByVal Export As Boolean)
        Dim strConn As String
        Dim lHrs As Long
        Dim lMinutes As Long
        Dim lSeconds As Long
        Dim ATAT As String
        Dim MINTAT As String
        Dim MAXTAT As String
        Dim altrow As Boolean = False
        strConn = System.Configuration.ConfigurationManager.AppSettings("ETSCon")
        Dim DS As New Data.DataSet
        Dim clsMIS As ETS.BL.MISReports
        Try
            clsMIS = New ETS.BL.MISReports
            DS = clsMIS.GetDTRPostDictByParm(StartDate, EndDate, New System.Guid(ActID), DLInstance.SelectedValue)
            If DS.Tables.Count > 0 Then
                If DS.Tables(0).Rows.Count > 0 Then
                    For Each DRRecT As DataRow In DS.Tables(0).Rows
                        If DRRecT(3).ToString > 0 Then

                            lSeconds = DRRecT(12).ToString

                            lHrs = Int(lSeconds / 3600)
                            lMinutes = (Int(lSeconds / 60)) - (lHrs * 60)
                            lSeconds = Int(lSeconds Mod 60)


                            ' Response.Write(Format(lMinutes, "00") & "#")

                            If lSeconds = 60 Then
                                lMinutes = lMinutes + 1
                                lSeconds = 0
                            End If

                            If lMinutes = 60 Then
                                lMinutes = 0
                                lHrs = lHrs + 1
                            End If

                            ATAT = Format(lHrs, "00") & ":" & _
                              Format(lMinutes, "00") & "." & _
                              Format(lSeconds, "00")

                            lSeconds = DRRecT(13).ToString
                            lHrs = Int(lSeconds / 3600)
                            lMinutes = (Int(lSeconds / 60)) - (lHrs * 60)
                            lSeconds = Int(lSeconds Mod 60)
                            If lSeconds = 60 Then
                                lMinutes = lMinutes + 1
                                lSeconds = 0
                            End If

                            If lMinutes = 60 Then
                                lMinutes = 0
                                lHrs = lHrs + 1
                            End If

                            MINTAT = Format(lHrs, "00") & ":" & _
                              Format(lMinutes, "00") & ":" & _
                              Format(lSeconds, "00")

                            lSeconds = DRRecT(14).ToString
                            lHrs = Int(lSeconds / 3600)
                            lMinutes = (Int(lSeconds / 60)) - (lHrs * 60)
                            lSeconds = Int(lSeconds Mod 60)
                            If lSeconds = 60 Then
                                lMinutes = lMinutes + 1
                                lSeconds = 0
                            End If

                            If lMinutes = 60 Then
                                lMinutes = 0
                                lHrs = lHrs + 1
                            End If

                            MAXTAT = Format(lHrs, "00") & ":" & _
                              Format(lMinutes, "00") & ":" & _
                              Format(lSeconds, "00")

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
                            Dim Cell19 As New TableCell
                            Cell1.HorizontalAlign = HorizontalAlign.Left
                            Cell1.Text = DRRecT(1).ToString
                            Cell2.Text = FormatNumber((DRRecT(3).ToString), 0)
                            Cell3.Text = FormatNumber((DRRecT(5).ToString), 0)
                            Cell4.Text = FormatNumber((DRRecT(7).ToString), 0)
                            Cell5.Text = FormatNumber((DRRecT(9).ToString), 0)
                            Cell6.Text = FormatNumber((DRRecT(11).ToString), 0)
                            Cell7.Text = ATAT
                            Cell8.Text = MINTAT
                            Cell9.Text = MAXTAT
                            Cell10.Text = FormatNumber((DRRecT(2).ToString / 60), 0)
                            Cell11.Text = FormatNumber((DRRecT(4).ToString / 60), 0)
                            Cell12.Text = FormatNumber((DRRecT(6).ToString / 60), 0)
                            Cell13.Text = FormatNumber((DRRecT(8).ToString / 60), 0)
                            Cell14.Text = FormatNumber((DRRecT(10).ToString / 60), 0)
                            Cell15.Text = Convert.ToInt32(FormatNumber((DRRecT(5).ToString), 0) * 100 / FormatNumber((DRRecT(3).ToString), 0)) & "%"
                            Cell16.Text = Convert.ToInt32(FormatNumber((DRRecT(7).ToString), 0) * 100 / FormatNumber((DRRecT(3).ToString), 0)) & "%"
                            Cell17.Text = Convert.ToInt32(FormatNumber((DRRecT(9).ToString), 0) * 100 / FormatNumber((DRRecT(3).ToString), 0)) & "%"
                            Cell18.Text = Convert.ToInt32(FormatNumber((DRRecT(11).ToString), 0) * 100 / FormatNumber((DRRecT(3).ToString), 0)) & "%"
                            Cell7.Text = ATAT
                            Cell19.Text = DRRecT("median").ToString

                            Row1.CssClass = "tblbg2"
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
                            Row1.Cells.Add(Cell11)
                            Row1.Cells.Add(Cell12)
                            Row1.Cells.Add(Cell13)
                            Row1.Cells.Add(Cell14)
                            Row1.Cells.Add(Cell15)
                            Row1.Cells.Add(Cell16)
                            Row1.Cells.Add(Cell17)
                            Row1.Cells.Add(Cell18)
                            Row1.Cells.Add(Cell19)
                            If altrow = False Then
                                Row1.CssClass = "gridalt2"
                                altrow = True
                            Else
                                Row1.CssClass = "gridalt1"
                                altrow = False

                            End If
                            tblMins.Rows.Add(Row1)

                        End If
                    Next
                End If
            End If
        Catch ex As Exception
            Response.Write(ex.Message)
        Finally
            clsMIS = Nothing
            DS.Dispose()
        End Try




    End Sub

    'Protected Sub ShowDictDetails(ByVal ActID As String, ByVal inpDate As Date, ByVal EndDate As Date)
    '    R1Cell1.Text = "Dictator"
    '    tblDtls.Text = "Dictator Details"
    '    Dim strConn As String
    '    Dim strCategory As String
    '    strCategory = ""


    '    Dim t2 As New Table
    '    t2.Style("width") = "100%"
    '    t2.BorderWidth = 2
    '    t2.GridLines = GridLines.Both
    '    Dim I As Integer
    '    I = 0
    '    strConn = System.Configuration.ConfigurationManager.AppSettings("ETSCon")
    '    Dim strQuery As String
    '    Dim CurrSDate As Date
    '    Dim CurrEDate As Date
    '    Dim PrvWDays As Integer
    '    Dim CurrWDays As Integer
    '    CurrSDate = Month(inpDate) & "/1/" & Year(inpDate)
    '    If Month(CurrSDate) = Month(Now) Then
    '        CurrEDate = Now.ToShortDateString
    '    Else
    '        CurrEDate = DateAdd(DateInterval.Day, -1, DateAdd(DateInterval.Month, 1, CurrSDate))
    '    End If
    '    CurrWDays = WorkingDays(CurrSDate, CurrEDate)
    '    Dim PrvSDate As Date
    '    Dim PrvEDate As Date

    '    PrvSDate = DateAdd(DateInterval.Month, -1, CurrSDate)
    '    PrvEDate = DateAdd(DateInterval.Day, -1, CurrSDate)
    '    PrvWDays = WorkingDays(PrvSDate, PrvEDate)

    '    Dim TAvgPreMins As Integer
    '    Dim TAvgCurrMins As Integer
    '    Dim TDayMins1 As Integer
    '    Dim TDayMins2 As Integer
    '    Dim TDayMins3 As Integer
    '    Dim TDayMins4 As Integer
    '    Dim TDayMins5 As Integer
    '    Dim TDayMins6 As Integer
    '    Dim TDayMins7 As Integer
    '    TAvgPreMins = 0
    '    TAvgCurrMins = 0
    '    TDayMins1 = 0
    '    TDayMins2 = 0
    '    TDayMins3 = 0
    '    TDayMins4 = 0
    '    TDayMins5 = 0
    '    TDayMins6 = 0
    '    TDayMins7 = 0



    '    strQuery = "Select A.Accountid, U.firstname + ' ' + U.Lastname as uname, IsNull(PM.Mins,0) AS PrevMonthMins, IsNull(CM.Mins,0) AS CurrMonthMins, IsNull(T1.Mins, 0) as DayMins1, IsNull(T2.Mins,0) as DayMins2, IsNull(T3.Mins,0) as DayMins3, IsNull(T4.Mins,0) as DayMins4, IsNull(T5.Mins,0) as DayMins5, IsNull(T6.Mins,0) as DayMins6, IsNull(T7.Mins,0) as DayMins7 "
    '    strQuery = strQuery & " from tblaccounts A "
    '    strQuery = strQuery & " LEFT OUTER JOIN (Select * from tblPhysicians) U"
    '    strQuery = strQuery & " on U.accountid = A.accountid "
    '    strQuery = strQuery & " LEFT OUTER JOIN (Select DictatorID, sum(datediff(s,0,duration)) AS Mins from tbltranscriptionmain where Day(submitdate) = '" & Day(inpDate) & "' and Month(submitdate) = '" & Month(inpDate) & "' and Year(submitdate) =  '" & Year(inpDate) & "'  and accountid = '" & ActID & "'  Group By DictatorID) T1"
    '    strQuery = strQuery & " on T1.DictatorID = U.PhysicianID "
    '    strQuery = strQuery & " LEFT OUTER JOIN (Select DictatorID, sum(datediff(s,0,duration)) AS Mins from tbltranscriptionmain where Day(submitdate) = '" & Day(DateAdd(DateInterval.Day, -1, inpDate)) & "' and Month(submitdate) = '" & Month(DateAdd(DateInterval.Day, -1, inpDate)) & "' and Year(submitdate) = '" & Year(DateAdd(DateInterval.Day, -1, inpDate)) & "'   and accountid = '" & ActID & "' Group By DictatorID) T2"
    '    strQuery = strQuery & " on T2.DictatorID = U.PhysicianID "
    '    strQuery = strQuery & " LEFT OUTER JOIN (Select DictatorID, sum(datediff(s,0,duration)) AS Mins from tbltranscriptionmain where Day(submitdate) = '" & Day(DateAdd(DateInterval.Day, -2, inpDate)) & "' and Month(submitdate) = '" & Month(DateAdd(DateInterval.Day, -2, inpDate)) & "'  and Year(submitdate) = '" & Year(DateAdd(DateInterval.Day, -2, inpDate)) & "'  and accountid = '" & ActID & "'  Group By DictatorID) T3"
    '    strQuery = strQuery & " on T3.DictatorID = U.PhysicianID "
    '    strQuery = strQuery & " LEFT OUTER JOIN (Select DictatorID, sum(datediff(s,0,duration)) AS Mins from tbltranscriptionmain where Day(submitdate) = '" & Day(DateAdd(DateInterval.Day, -3, inpDate)) & "' and Month(submitdate) = '" & Month(DateAdd(DateInterval.Day, -3, inpDate)) & "'  and Year(submitdate) = '" & Year(DateAdd(DateInterval.Day, -3, inpDate)) & "'  and accountid = '" & ActID & "'  Group By DictatorID) T4"
    '    strQuery = strQuery & " on T4.DictatorID = U.PhysicianID "
    '    strQuery = strQuery & " LEFT OUTER JOIN (Select DictatorID, sum(datediff(s,0,duration)) AS Mins from tbltranscriptionmain where Day(submitdate) = '" & Day(DateAdd(DateInterval.Day, -4, inpDate)) & "' and Month(submitdate) = '" & Month(DateAdd(DateInterval.Day, -4, inpDate)) & "'  and Year(submitdate) = '" & Year(DateAdd(DateInterval.Day, -4, inpDate)) & "'  and accountid = '" & ActID & "'  Group By DictatorID) T5"
    '    strQuery = strQuery & " on T5.DictatorID = U.PhysicianID "
    '    strQuery = strQuery & " LEFT OUTER JOIN (Select DictatorID, sum(datediff(s,0,duration)) AS Mins from tbltranscriptionmain where Day(submitdate) = '" & Day(DateAdd(DateInterval.Day, -5, inpDate)) & "' and Month(submitdate) = '" & Month(DateAdd(DateInterval.Day, -5, inpDate)) & "'  and Year(submitdate) = '" & Year(DateAdd(DateInterval.Day, -5, inpDate)) & "'  and accountid = '" & ActID & "'  Group By DictatorID) T6"
    '    strQuery = strQuery & " on T6.DictatorID = U.PhysicianID "
    '    strQuery = strQuery & " LEFT OUTER JOIN (Select DictatorID, sum(datediff(s,0,duration)) AS Mins from tbltranscriptionmain where Day(submitdate) = '" & Day(DateAdd(DateInterval.Day, -6, inpDate)) & "' and Month(submitdate) = '" & Month(DateAdd(DateInterval.Day, -6, inpDate)) & "'  and Year(submitdate) = '" & Year(DateAdd(DateInterval.Day, -6, inpDate)) & "'  and accountid = '" & ActID & "'  Group By DictatorID) T7"
    '    strQuery = strQuery & " on T7.DictatorID = U.PhysicianID "
    '    strQuery = strQuery & " LEFT OUTER JOIN (Select DictatorID, sum(datediff(s,0,duration)) AS Mins from tbltranscriptionmain where Month(submitdate) = '" & Month(DateAdd(DateInterval.Month, -1, inpDate)) & "' and Year(submitdate) = '" & Year(DateAdd(DateInterval.Month, -1, inpDate)) & "' and accountid = '" & ActID & "'  Group By DictatorID) PM"
    '    strQuery = strQuery & " on PM.DictatorID = U.PhysicianID "
    '    strQuery = strQuery & " LEFT OUTER JOIN (Select DictatorID, sum(datediff(s,0,duration)) AS Mins from tbltranscriptionmain where Month(submitdate) = '" & Month(inpDate) & "' and Year(submitdate) = '" & Year(inpDate) & "'  and accountid = '" & ActID & "'  Group By DictatorID) CM"
    '    strQuery = strQuery & " on  CM.DictatorID = U.PhysicianID where A.accountid = '" & ActID & "' order by U.firstname "

    '    'Response.Write(strQuery)

    '    Dim SQLCmd1 As New SqlCommand(strQuery, New SqlConnection(strConn))
    '    SQLCmd1.Connection.Open()
    '    Dim DRRec1 As SqlDataReader = SQLCmd1.ExecuteReader()


    '    If DRRec1.HasRows Then


    '        R1Cell4.Text = inpDate
    '        R1Cell5.Text = DateAdd(DateInterval.Day, -1, inpDate)
    '        R1Cell6.Text = DateAdd(DateInterval.Day, -2, inpDate)
    '        R1Cell7.Text = DateAdd(DateInterval.Day, -3, inpDate)
    '        R1Cell8.Text = DateAdd(DateInterval.Day, -4, inpDate)
    '        R1Cell9.Text = DateAdd(DateInterval.Day, -5, inpDate)
    '        R1Cell10.Text = DateAdd(DateInterval.Day, -6, inpDate)
    '        R2Cell1.Text = "Name"
    '        R2Cell3.Text = inpDate.ToString("MMM") & " " & inpDate.Year
    '        R2Cell2.Text = DateAdd(DateInterval.Month, -1, inpDate).ToString("MMM") & " " & DateAdd(DateInterval.Month, -1, inpDate).Year
    '        R2Cell2.ToolTip = PrvWDays
    '        R2Cell3.ToolTip = CurrWDays
    '        R2Cell4.Text = inpDate.ToString("ddd")
    '        R2Cell5.Text = DateAdd(DateInterval.Day, -1, inpDate).ToString("ddd")
    '        R2Cell6.Text = DateAdd(DateInterval.Day, -2, inpDate).ToString("ddd")
    '        R2Cell7.Text = DateAdd(DateInterval.Day, -3, inpDate).ToString("ddd")
    '        R2Cell8.Text = DateAdd(DateInterval.Day, -4, inpDate).ToString("ddd")
    '        R2Cell9.Text = DateAdd(DateInterval.Day, -5, inpDate).ToString("ddd")
    '        R2Cell10.Text = DateAdd(DateInterval.Day, -6, inpDate).ToString("ddd")
    '        While (DRRec1.Read)





    '            TAvgPreMins += FormatNumber((DRRec1(2).ToString / 60) / PrvWDays, 0)
    '            TAvgCurrMins += FormatNumber((DRRec1(3).ToString / 60) / CurrWDays, 0)
    '            TDayMins1 += FormatNumber(DRRec1(4), 0)
    '            TDayMins2 += FormatNumber(DRRec1(5), 0)
    '            TDayMins3 += FormatNumber(DRRec1(6), 0)
    '            TDayMins4 += FormatNumber(DRRec1(7), 0)
    '            TDayMins5 += FormatNumber(DRRec1(8), 0)
    '            TDayMins6 += FormatNumber(DRRec1(9), 0)
    '            TDayMins7 += FormatNumber(DRRec1(10), 0)

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
    '            Cell1.Text = DRRec1(1).ToString
    '            Cell2.Text = FormatNumber((DRRec1(2).ToString / 60) / PrvWDays, 0)
    '            Cell3.Text = FormatNumber((DRRec1(3).ToString / 60) / CurrWDays, 0)
    '            Cell2.Text = FormatNumber((DRRec1(2).ToString / 60) / PrvWDays, 0)
    '            Cell3.Text = FormatNumber((DRRec1(3).ToString / 60) / CurrWDays, 0)

    '            Cell4.Text = FormatNumber(DRRec1(4).ToString / 60, 0)
    '            Cell5.Text = FormatNumber(DRRec1(5).ToString / 60, 0)
    '            Cell6.Text = FormatNumber(DRRec1(6).ToString / 60, 0)
    '            Cell7.Text = FormatNumber(DRRec1(7).ToString / 60, 0)
    '            Cell8.Text = FormatNumber(DRRec1(8).ToString / 60, 0)
    '            Cell9.Text = FormatNumber(DRRec1(9).ToString / 60, 0)
    '            Cell10.Text = FormatNumber(DRRec1(10).ToString / 60, 0)
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
    '        A1Cell2.Text = FormatNumber(TAvgPreMins, 0)
    '        A1Cell3.Text = FormatNumber(TAvgCurrMins, 0)
    '        A1Cell4.Text = FormatNumber(TDayMins1 / 60, 0)
    '        A1Cell5.Text = FormatNumber(TDayMins2 / 60, 0)
    '        A1Cell6.Text = FormatNumber(TDayMins3 / 60, 0)
    '        A1Cell7.Text = FormatNumber(TDayMins4 / 60, 0)
    '        A1Cell8.Text = FormatNumber(TDayMins5 / 60, 0)
    '        A1Cell9.Text = FormatNumber(TDayMins6 / 60, 0)
    '        A1Cell10.Text = FormatNumber(TDayMins7 / 60, 0)

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
    'End Sub



    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim sdate As Date
        Dim edate As Date
        If Not IsPostBack Then
            Lbl1.Text = ""
            TXTSDate.Text = Now.AddDays(-1).ToShortDateString
            'CalendarExtender1.SelectedDate = Now.AddDays(-1).ToShortDateString
            TXTEDate.Text = Now.ToShortDateString
            'CalendarExtender2.SelectedDate = Now.ToShortDateString
            'Dim strconn As String
            'strconn = System.Configuration.ConfigurationManager.AppSettings("ETSCon")
            'Dim SQLCmd As New SqlCommand("Select * from tblaccounts where (isdeleted is null or isdeleted = 'False')  and MISRep = 'True' and contractorid = '" & Session("contractorid").ToString & "' order by description", New SqlConnection(strconn))
            'Try
            '    SQLCmd.Connection.Open()
            '    Dim DRRec As SqlDataReader = SQLCmd.ExecuteReader()
            '    If DRRec.HasRows = True Then
            '        While (DRRec.Read)
            '            Dim LI As New ListItem
            '            LI.Text = DRRec("Description").ToString
            '            LI.Value = DRRec("AccountID").ToString
            '            DLAct.Items.Add(LI)
            '        End While
            '    End If
            '    DRRec.Close()
            'Finally
            '    If SQLCmd.Connection.State = System.Data.ConnectionState.Open Then
            '        SQLCmd.Connection.Close()
            '        SQLCmd = Nothing
            '    End If
            'End Try
            Dim clsAcc As ETS.BL.Accounts
            Dim Ds As New Data.DataSet
            Try
                clsAcc = New ETS.BL.Accounts
                Ds = clsAcc.getAccountList(Session("WorkGroupID"), Session("ContractorID"), " ")
                If Ds.Tables.Count > 0 Then
                    If Ds.Tables(0).Rows.Count > 0 Then
                        DLAct.DataSource = Ds
                        DLAct.DataTextField = "AccountName"
                        DLAct.DataValueField = "AccountID"
                        DLAct.DataBind()
                    End If
                End If
            Catch ex As Exception
            Finally
                clsAcc = Nothing
                Ds = Nothing
            End Try

            If Request("showDict") = "Yes" Then
                Dim AccountID As String
                sdate = Date.Parse(Request("startDate"))
                edate = Date.Parse(Request("endDate"))
                TXTSDate.Text = Request("startDate")
                TXTEDate.Text = Request("EndDate")
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
                ShowDictDetails(AccountID, sdate, edate, False)
            End If
        Else
            If Request("txtsdate") <> "" Then
                CalendarExtender1.SelectedDate = Request("txtsdate")
            End If
            If Request("txtedate") <> "" Then
                CalendarExtender2.SelectedDate = Request("txtedate")
            End If
            'TXTSDate.Text = Request("txtsdate")
            'TXTEDate.Text = Request("txtedate")

        End If


    End Sub

    Protected Sub tblsubmit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles tblsubmit.Click
        Lbl1.Text = ""
        If TXTSDate.Text = "" Then
            Lbl1.Text = "Please enter start submit date."
            TXTSDate.Focus()
            Exit Sub
        ElseIf Not IsDate(TXTSDate.Text) Then
            Lbl1.Text = "Invalid date."
            TXTSDate.Focus()
            Exit Sub
        ElseIf TXTEDate.Text = "" Then
            Lbl1.Text = "Please enter end submit date."
            TXTEDate.Focus()
            Exit Sub
        ElseIf Not IsDate(TXTEDate.Text) Then
            Lbl1.Text = "Invalid date."
            TXTEDate.Focus()
            Exit Sub
        End If
        sDate = Date.Parse(TXTSDate.Text)
        eDate = Date.Parse(TXTEDate.Text)

        If DLAct.SelectedValue <> "" Then
            Dim AccountID As String
            AccountID = DLAct.SelectedValue.ToString
            ShowDictDetails(AccountID, sDate, eDate, False)
        Else
            ShowActDetails(sDate, eDate, False)
        End If
        'If TXTSDate.Text = "" Then

        '    TXTSDate.Text = Now.ToShortDateString

        'End If
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
        'Dim TxCell As String
        'Response.Write(sDate)
        'Response.Write(eDate)

        sDate = Date.Parse(TXTSDate.Text)
        eDate = Date.Parse(TXTEDate.Text)
        If DLAct.SelectedValue <> "" Then
            Dim AccountID As String
            AccountID = DLAct.SelectedValue.ToString
            ShowDictDetails(AccountID, sDate, eDate, True)
        Else
            ShowActDetails(sDate, eDate, True)
        End If
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
        tblMins.Rows(0).ForeColor = Drawing.Color.DarkOrange
        tblMins.Rows(1).ForeColor = Drawing.Color.DarkOrange
        tblMins.Rows(2).ForeColor = Drawing.Color.DarkOrange
        tblMins.ForeColor = Drawing.Color.Black
        tblMins.Rows(0).Font.Bold = True
        tblMins.Rows(1).Font.Bold = True
        tblMins.Rows(2).Font.Bold = True
        tblMins.Rows(0).Font.Italic = True
        tblMins.Rows(1).Font.Italic = True
        tblMins.Rows(2).Font.Italic = True
        tblMins.Font.Size = "9.5"
        tblMins.GridLines = GridLines.Both
        tblMins.RenderControl(htw)
        'MyDataGrid.RenderControl(htw)
        Response.Write(sw.ToString())
        Response.[End]()

    End Sub
End Class
