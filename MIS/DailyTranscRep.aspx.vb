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
        Dim strDate As String
        Dim strCategory As String
        Dim lHrs As Long
        Dim lMinutes As Long
        Dim lSeconds As Long
        Dim ATAT As String
        Dim MINTAT As String
        Dim MAXTAT As String
        Dim i As Integer
        strCategory = ""
        Dim selQuery As String
        Dim strQuery As String
        Dim MTLines As Long
        Dim MTPLines As Long
        Dim QALines As Long
        Dim BBLines As Long
        Dim PPQALines As Long
        Dim MTBLines As Long
        Dim QABLines As Long
        Dim Hourdiff As Integer
        Hourdiff = DateDiff(DateInterval.Day, StartDate, EndDate)
        MTLines = 0
        MTPLines = 0
        QALines = 0
        BBLines = 0
        PPQALines = 0
        MTBLines = 0
        QABLines = 0
        strConn = System.Configuration.ConfigurationManager.AppSettings("ETSCon")
        'strQuery = "Select DISTINCT  LevelName, LevelNo from tblproductionlevels where contractorid='" & Session("contractorid") & "' And Type ='" & Session("IsContractor").ToString & "' and LevelNo not in(1073741824)  order by LevelNo"
        ''Response.Write(strQuery)
        'Dim LvlA(0) As String
        'Dim Lvl(0) As String
        'Dim JoinQuery As String
        'Dim ClmCount As Integer = 0
        'SelQuery = ""
        'JoinQuery = ""
        'Dim Incr As Integer
        'Incr = 0
        'Dim CommPL As New SqlCommand(strQuery, New SqlConnection(strConn))
        'Try
        '    CommPL.Connection.Open()
        '    Dim DRPL As SqlDataReader = CommPL.ExecuteReader()
        '    If DRPL.HasRows Then
        '        While (DRPL.Read)

        '            ClmCount = ClmCount + 1
        '            Incr = Incr + 1
        '            If ClmCount = 1 Then
        '                selQuery = " ,TS" & Incr & ".USERNAME as '" & DRPL("LevelName").ToString & " ID', TS" & Incr & ".LINECOUNT as '" & DRPL("LevelName").ToString & " Lines',TS" & Incr & ".DATEMODIFIED as '" & DRPL("LevelName").ToString & " Date' "
        '                JoinQuery = " LEFT OUTER JOIN (SELECT TS.TRANSCRIPTIONID, U.USERNAME, TS.LINECOUNT, TS.DATEMODIFIED FROM TBLTRANSCRIPTIONSTATUS TS, TBLUSERS U WHERE TS.USERID = U.USERID AND TS.USERLEVEL = ' " & DRPL("LevelNO").ToString & "' ) AS TS" & Incr & "  ON TS" & Incr & ".TRANSCRIPTIONID = T.TRANSCRIPTIONID "
        '            Else
        '                selQuery = selQuery & " ,TS" & Incr & ".USERNAME as '" & DRPL("LevelName").ToString & " ID', TS" & Incr & ".LINECOUNT as '" & DRPL("LevelName").ToString & " Lines',TS" & Incr & ".DATEMODIFIED as '" & DRPL("LevelName").ToString & " Date' "
        '                JoinQuery = JoinQuery & " LEFT OUTER JOIN (SELECT TS.TRANSCRIPTIONID, U.USERNAME, TS.LINECOUNT, TS.DATEMODIFIED FROM TBLTRANSCRIPTIONSTATUS TS, TBLUSERS U WHERE TS.USERID = U.USERID AND TS.USERLEVEL = ' " & DRPL("LevelNO").ToString & "' ) AS TS" & Incr & "  ON TS" & Incr & ".TRANSCRIPTIONID = T.TRANSCRIPTIONID "
        '            End If
        '        End While
        '    End If
        '    DRPL.Close()
        'Finally
        '    If CommPL.Connection.State = Data.ConnectionState.Open Then
        '        CommPL.Connection.Close()
        '    End If
        'End Try

        'strQuery = "SELECT T.JobNumber, T.CUSTJOBID as 'Client Job#', A.ACCOUNTNAME as 'Account Name', P.FIRSTNAME + ' ' + P.LASTNAME AS Dictator, T.DATECREATED as 'Created Date', T.DATEMODIFIED as 'Post Date'"
        'strQuery = strQuery & selQuery
        'strQuery = strQuery & ", ISNULL(L.BILLINGLC, 0) AS 'Billing Lines' "
        'strQuery = strQuery & " FROM TBLTRANSCRIPTIONMAIN T LEFT JOIN TBLTEMPLATES TL ON T.TEMPLATEID = TL.TEMPLATEID "
        'strQuery = strQuery & " LEFT JOIN TBLACCOUNTS A ON T.ACCOUNTID = A.ACCOUNTID"
        'strQuery = strQuery & " LEFT JOIN TBLPHYSICIANS P ON T.DICTATORID = P.PHYSICIANID"
        'strQuery = strQuery & " LEFT JOIN TBLTRANSLC L ON L.TRANSCRIPTIONID = T.TRANSCRIPTIONID"
        'strQuery = strQuery & JoinQuery
        'strQuery = strQuery & " WHERE "
        ''strQuery = strQuery & "  T.STATUS in ('1073741824','262144') and  T.DATEMODIFIED BETWEEN '" & StartDate & "' and  '" & EndDate & "'"
        'strQuery = strQuery & "  T.STATUS in ('1073741824','262144') and  datediff(day, T.DateModified, '" & EndDate & "') between 0 and " & Hourdiff & " and T.Contractorid = '" & Session("contractorid").ToString & "'"
        'If DLAct.SelectedValue <> "" Then
        '    strQuery = strQuery & "  and T.Accountid='" & DLAct.SelectedValue & "' "
        'End If

        ''strQuery = strQuery & "  T.STATUS = '1073741824' and datediff(hh, T.Datemodified, getdate()) < 30 "
        ''strQuery = strQuery & " AND TS1.USERNAME IS NOT NULL"
        ''        strQuery = strQuery & " AND T.STATUS = '1073741824' "
        ''strQuery = strQuery & " ORDER BY JOBNUMBER "
        ''Response.Write(strQuery)
        ''Response.End()
        'Dim SQLCmdT As New SqlCommand(strQuery, New SqlConnection(strConn))
        'SQLCmdT.Connection.Open()
        Dim DS As New Data.DataSet
        Dim clsMIS As ETS.BL.MISReports
        Try
            clsMIS = New ETS.BL.MISReports
            Response.Write(EndDate.ToShortDateString.ToString & "#" & Hourdiff)
            DS = clsMIS.GetProductivityReportByParm(New System.Guid(Session("contractorID").ToString), Nothing, Session("IsContractor").ToString, Hourdiff, Now.ToShortDateString, Session("WorkGroupID").ToString, 1)

            If DS.Tables.Count > 0 Then
                If DS.Tables(0).Rows.Count > 0 Then
                    If RB.SelectedValue = "E" Then
                        Dim filename As String
                        filename = "Productivity Report " & Month(Now) & Day(Now) & Year(Now) & ".xls"
                        Dim attachment As String = "attachment; filename=" & filename
                        Response.ClearContent()
                        Response.AddHeader("content-disposition", attachment)
                        Response.ContentType = "application/ms-excel"
                        Dim sw As New StringWriter()
                        Dim htw As New HtmlTextWriter(sw)
                        Dim DG As New DataGrid
                        DG.DataSource = DS.Tables(0)
                        DG.DataBind()
                        DG.RenderControl(htw)
                        'MyDataGrid.RenderControl(htw)
                        Response.Write(sw.ToString())
                        Response.[End]()
                        MyDataGrid.Visible = False
                    Else
                        MyDataGrid.DataSource = DS.Tables(0)
                        MyDataGrid.DataBind()
                    End If
                End If
            End If
        Catch ex As Exception
            Response.Write(ex.Message)
        Finally
            clsMIS = Nothing
            DS.Dispose()
        End Try

        'Dim SQLCmdT As New SqlCommand("[ets].[dbo].[SF_ProductivityReport]", New SqlConnection(strConn))
        'SQLCmdT.CommandType = CommandType.StoredProcedure
        'SQLCmdT.Parameters.Add("@contractorID", SqlDbType.UniqueIdentifier)
        'SQLCmdT.Parameters("@contractorID").Value = New System.Guid(Session("contractorID").ToString)
        'SQLCmdT.Parameters.Add("@ActID", SqlDbType.VarChar)
        'SQLCmdT.Parameters("@ActID").Value = DLAct.SelectedValue
        'SQLCmdT.Parameters.Add("@IsContractor", SqlDbType.VarChar)
        'SQLCmdT.Parameters("@IsContractor").Value = Session("IsContractor").ToString
        'SQLCmdT.Parameters.Add("@Hourdiff", SqlDbType.Int)
        'SQLCmdT.Parameters("@Hourdiff").Value = Hourdiff
        'SQLCmdT.Parameters.Add("@EndDate", SqlDbType.SmallDateTime)
        'SQLCmdT.Parameters("@EndDate").Value = EndDate
        ''Response.Write(Session("contractorID").ToString & "','" & Session("IsContractor").ToString & "','" & Hourdiff & "','" & EndDate & "'")
        ''Response.End()
        'SQLCmdT.Connection.Open()
        'Dim DRRecT As SqlDataReader = SQLCmdT.ExecuteReader()

        


    End Sub

    Protected Sub ShowDictDetails(ByVal ActID As String, ByVal StartDate As Date, ByVal EndDate As Date, ByVal Export As Boolean)
        'Dim strConn As String
        'Dim strDate As String
        'Dim strCategory As String
        'Dim lHrs As Long
        'Dim lMinutes As Long
        'Dim lSeconds As Long
        'Dim ATAT As String
        'Dim MINTAT As String
        'Dim MAXTAT As String
        'Dim i As Integer
        'strCategory = ""
        'Dim selQuery As String
        'Dim strQuery As String
        'Dim MTLines As Long
        'Dim MTPLines As Long
        'Dim QALines As Long
        'Dim BBLines As Long
        'Dim PPQALines As Long
        'Dim Hourdiff As Integer
        'Hourdiff = DateDiff(DateInterval.Day, StartDate, EndDate)
        'MTLines = 0
        'MTPLines = 0
        'QALines = 0
        'BBLines = 0
        'PPQALines = 0
        'strConn = System.Configuration.ConfigurationManager.AppSettings("ETSCon")


        'strQuery = "SELECT T.JOBNUMBER, T.CUSTJOBID, A.ACCOUNTNAME, P.FIRSTNAME + ' ' + P.LASTNAME AS PNAME, T.DATECREATED, T.DATEMODIFIED, T.DURATION, T.PRIORITY"
        'strQuery = strQuery & " ,ISNULL(TS1.USERNAME, '') AS TS1ID,ISNULL(TS1.LINECOUNT, 0) AS TS1LINES, TS1.DATEMODIFIED AS TS1DATE"
        'strQuery = strQuery & " ,ISNULL(TS2.USERNAME, '') AS TS2ID,ISNULL(TS2.LINECOUNT, 0) AS TS2LINES, TS2.DATEMODIFIED AS TS2DATE"
        'strQuery = strQuery & " ,ISNULL(TS3.USERNAME, '') AS TS3ID,ISNULL(TS3.LINECOUNT, 0) AS TS3LINES, TS3.DATEMODIFIED AS TS3DATE"
        'strQuery = strQuery & " ,ISNULL(TS4.USERNAME, '') AS TS4ID,ISNULL(TS4.LINECOUNT, 0) AS TS4LINES, TS4.DATEMODIFIED AS TS4DATE"
        'strQuery = strQuery & " ,ISNULL(TS5.USERNAME, '') AS TS5ID,ISNULL(TS5.LINECOUNT, 0) AS TS5LINES, TS5.DATEMODIFIED AS TS5DATE"
        'strQuery = strQuery & " FROM TBLTRANSCRIPTIONMAIN T"
        'strQuery = strQuery & " LEFT JOIN TBLACCOUNTS A ON T.ACCOUNTID = A.ACCOUNTID"
        'strQuery = strQuery & " LEFT JOIN TBLPHYSICIANS P ON T.DICTATORID = P.PHYSICIANID"
        'strQuery = strQuery & " LEFT JOIN (SELECT TS.TRANSCRIPTIONID, U.USERNAME, TS.LINECOUNT, TS.DATEMODIFIED FROM TBLTRANSCRIPTIONSTATUS TS, TBLUSERS U"
        'strQuery = strQuery & " WHERE  TS.USERID = U.USERID AND TS.USERLEVEL = '1' AND TS.STATUS IN ('2') ) AS TS1 "
        'strQuery = strQuery & " ON TS1.TRANSCRIPTIONID = T.TRANSCRIPTIONID"
        'strQuery = strQuery & " LEFT JOIN (SELECT TS.TRANSCRIPTIONID, U.USERNAME, TS.LINECOUNT, TS.DATEMODIFIED FROM TBLTRANSCRIPTIONSTATUS TS, TBLUSERS U"
        'strQuery = strQuery & " WHERE  TS.USERID = U.USERID AND TS.USERLEVEL = '1' AND TS.STATUS NOT IN ('2')) AS TS2 "
        'strQuery = strQuery & " ON TS2.TRANSCRIPTIONID = T.TRANSCRIPTIONID"
        'strQuery = strQuery & " LEFT JOIN (SELECT TS.TRANSCRIPTIONID, U.USERNAME, TS.LINECOUNT, TS.DATEMODIFIED FROM TBLTRANSCRIPTIONSTATUS TS, TBLUSERS U"
        'strQuery = strQuery & " WHERE  TS.USERID = U.USERID AND TS.USERLEVEL = '2') AS TS3 "
        'strQuery = strQuery & " ON TS3.TRANSCRIPTIONID = T.TRANSCRIPTIONID"
        'strQuery = strQuery & " LEFT JOIN (SELECT TS.TRANSCRIPTIONID, U.USERNAME, TS.LINECOUNT, TS.DATEMODIFIED FROM TBLTRANSCRIPTIONSTATUS TS, TBLUSERS U"
        'strQuery = strQuery & " WHERE  TS.USERID = U.USERID AND TS.USERLEVEL = '8') AS TS4 "
        'strQuery = strQuery & " ON TS4.TRANSCRIPTIONID = T.TRANSCRIPTIONID"
        'strQuery = strQuery & " LEFT JOIN (SELECT TS.TRANSCRIPTIONID, U.USERNAME, TS.LINECOUNT, TS.DATEMODIFIED FROM TBLTRANSCRIPTIONSTATUS TS, TBLUSERS U"
        'strQuery = strQuery & " WHERE  TS.USERID = U.USERID AND TS.USERLEVEL = '8192') AS TS5 "
        'strQuery = strQuery & " ON TS5.TRANSCRIPTIONID = T.TRANSCRIPTIONID"
        'strQuery = strQuery & " WHERE "
        ''strQuery = strQuery & "  T.STATUS in ('1073741824','262144')  and  T.DATEMODIFIED  BETWEEN '" & StartDate & "' and  '" & EndDate & "'"
        'strQuery = strQuery & "  T.STATUS in ('1073741824','262144') and  datediff(day, T.DateModified, '" & EndDate & "') between 0 and " & Hourdiff
        ''strQuery = strQuery & " AND TS1.USERNAME IS NOT NULL"
        ''       strQuery = strQuery & " AND T.STATUS = '1073741824' "
        'strQuery = strQuery & " AND T.ACCOUNTID = '" & ActID & "' "
        'strQuery = strQuery & " ORDER BY JOBNUMBER "

        ''Response.Write(strQuery)
        ''Response.End()

        'Dim SQLCmdT As New SqlCommand(strQuery, New SqlConnection(strConn))
        'Try
        '    SQLCmdT.Connection.Open()
        '    Dim DRRecT As SqlDataReader = SQLCmdT.ExecuteReader()
        '    If DRRecT.HasRows Then
        '        While DRRecT.Read

        '            Dim Row1 As New TableRow
        '            Dim Cell0 As New TableCell
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
        '            Dim Cell11 As New TableCell
        '            Dim Cell12 As New TableCell
        '            Dim Cell13 As New TableCell
        '            Dim Cell14 As New TableCell
        '            Dim Cell15 As New TableCell
        '            Dim Cell16 As New TableCell
        '            Dim Cell17 As New TableCell
        '            Dim Cell18 As New TableCell
        '            Dim Cell19 As New TableCell
        '            Dim Cell20 As New TableCell
        '            Dim Cell21 As New TableCell
        '            Dim Cell22 As New TableCell
        '            Cell0.Text = DRRecT(0).ToString
        '            Cell1.Text = DRRecT(1).ToString
        '            Cell2.Text = DRRecT(2).ToString
        '            Cell3.Text = DRRecT(3).ToString
        '            Cell4.Text = DRRecT(4).ToString
        '            Cell5.Text = DRRecT(5).ToString
        '            Cell6.Text = DRRecT(6).ToString
        '            Cell7.Text = DRRecT(7).ToString
        '            Cell8.Text = DRRecT(8).ToString
        '            Cell9.Text = DRRecT(9).ToString
        '            Cell10.Text = DRRecT(10).ToString
        '            Cell11.Text = DRRecT(11).ToString
        '            Cell12.Text = DRRecT(12).ToString
        '            Cell13.Text = DRRecT(13).ToString
        '            Cell14.Text = DRRecT(14).ToString
        '            Cell15.Text = DRRecT(15).ToString
        '            Cell16.Text = DRRecT(16).ToString
        '            Cell17.Text = DRRecT(17).ToString
        '            Cell18.Text = DRRecT(18).ToString
        '            Cell19.Text = DRRecT(19).ToString
        '            Cell20.Text = DRRecT(20).ToString
        '            Cell21.Text = DRRecT(21).ToString
        '            Cell22.Text = DRRecT(22).ToString
        '            Row1.CssClass = "tblbg2"
        '            Row1.Cells.Add(Cell0)
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
        '            Row1.Cells.Add(Cell11)
        '            Row1.Cells.Add(Cell12)
        '            Row1.Cells.Add(Cell13)
        '            Row1.Cells.Add(Cell14)
        '            Row1.Cells.Add(Cell15)
        '            Row1.Cells.Add(Cell16)
        '            Row1.Cells.Add(Cell17)
        '            Row1.Cells.Add(Cell18)
        '            Row1.Cells.Add(Cell19)
        '            Row1.Cells.Add(Cell20)
        '            Row1.Cells.Add(Cell21)
        '            Row1.Cells.Add(Cell22)
        '            tblMins.Rows.Add(Row1)

        '            MTLines = MTLines + CInt(DRRecT(9).ToString)
        '            MTPLines = MTPLines + CInt(DRRecT(12).ToString)
        '            QALines = QALines + CInt(DRRecT(15).ToString)
        '            BBLines = BBLines + CInt(DRRecT(18).ToString)
        '            PPQALines = PPQALines + CInt(DRRecT(21).ToString)
        '        End While
        '    End If
        '    Dim RowT1 As New TableRow
        '    Dim CellT0 As New TableCell
        '    Dim CellT1 As New TableCell
        '    Dim CellT2 As New TableCell
        '    Dim CellT3 As New TableCell
        '    Dim CellT4 As New TableCell
        '    Dim CellT5 As New TableCell
        '    Dim CellT6 As New TableCell
        '    Dim CellT7 As New TableCell
        '    Dim CellT8 As New TableCell
        '    Dim CellT9 As New TableCell
        '    Dim CellT10 As New TableCell
        '    Dim CellT11 As New TableCell
        '    Dim CellT12 As New TableCell
        '    Dim CellT13 As New TableCell
        '    Dim CellT14 As New TableCell
        '    Dim CellT15 As New TableCell
        '    Dim CellT16 As New TableCell
        '    Dim CellT17 As New TableCell
        '    Dim CellT18 As New TableCell
        '    Dim CellT19 As New TableCell
        '    Dim CellT20 As New TableCell
        '    Dim CellT21 As New TableCell
        '    Dim CellT22 As New TableCell
        '    CellT0.ColumnSpan = 9
        '    CellT0.Text = "Total MT Lines"
        '    CellT0.HorizontalAlign = HorizontalAlign.Right
        '    CellT9.Text = MTLines
        '    CellT10.ColumnSpan = 2
        '    CellT10.Text = "Total MTPLUS Lines"
        '    CellT10.HorizontalAlign = HorizontalAlign.Right
        '    CellT12.Text = MTPLines
        '    CellT13.ColumnSpan = 2
        '    CellT13.Text = "Total QA Lines"
        '    CellT13.HorizontalAlign = HorizontalAlign.Right
        '    CellT15.Text = QALines
        '    CellT16.ColumnSpan = 2
        '    CellT16.Text = "Total BB Lines"
        '    CellT16.HorizontalAlign = HorizontalAlign.Right
        '    CellT18.Text = BBLines

        '    CellT20.ColumnSpan = 2
        '    CellT20.Text = "Total PPQA Lines"
        '    CellT20.HorizontalAlign = HorizontalAlign.Right
        '    CellT22.Text = PPQALines
        '    CellT19.Text = ""

        '    RowT1.CssClass = "tblbg2"
        '    RowT1.Font.Bold = True
        '    RowT1.Cells.Add(CellT0)
        '    RowT1.Cells.Add(CellT9)
        '    RowT1.Cells.Add(CellT10)
        '    RowT1.Cells.Add(CellT12)
        '    RowT1.Cells.Add(CellT13)
        '    RowT1.Cells.Add(CellT15)
        '    RowT1.Cells.Add(CellT16)
        '    RowT1.Cells.Add(CellT18)
        '    RowT1.Cells.Add(CellT20)
        '    RowT1.Cells.Add(CellT22)
        '    RowT1.Cells.Add(CellT19)
        '    tblMins.Rows.Add(RowT1)
        '    DRRecT.Close()
        'Finally
        '    If SQLCmdT.Connection.State = System.Data.ConnectionState.Open Then
        '        SQLCmdT.Connection.Close()
        '        SQLCmdT = Nothing
        '    End If
        'End Try


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
        If Not IsPostBack Then


            CalendarExtender1.SelectedDate = Now.AddDays(-1).ToShortDateString
            CalendarExtender2.SelectedDate = Now.ToShortDateString
            Dim sdate As Date
            Dim edate As Date
            Dim clsAcc As ETS.BL.Accounts
            Dim Ds As New Data.DataSet
            Try
                clsAcc = New ETS.BL.Accounts
                Ds = clsAcc.getAccountList(Session("WorkGroupID"), Session("ContractorID"), " AND MISRep = 'True'")
                If Ds.Tables.Count > 0 Then
                    If Ds.Tables(0).Rows.Count > 0 Then
                        DLAct.DataSource = Ds
                        DLAct.DataTextField = "Description"
                        DLAct.DataValueField = "AccountID"
                        DLAct.DataBind()
                    End If
                End If
            Catch ex As Exception
            Finally
                clsAcc = Nothing
                Ds = Nothing
            End Try

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
            'Finally
            '    If SQLCmd.Connection.State = System.Data.ConnectionState.Open Then
            '        SQLCmd.Connection.Close()
            '        SQLCmd = Nothing
            '    End If
            'End Try

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
        End If


    End Sub

    Protected Sub tblsubmit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles tblsubmit.Click
        sDate = Date.Parse(TXTSDate.Text)
        eDate = Date.Parse(TXTEDate.Text)
        ShowActDetails(sDate, eDate, False)
        '        Response.Write(sDate)
        '        Response.Write(eDate)
        'If RB.SelectedValue = "D" Then
        '    'tblMins.Visible = True
        '    'Table3.Visible = True
        '    If DLAct.SelectedValue <> "" Then
        '        Dim AccountID As String
        '        AccountID = DLAct.SelectedValue.ToString
        '        ShowDictDetails(AccountID, sDate, eDate, False)
        '    Else
        '        ShowActDetails(sDate, eDate, False)
        '    End If
        'Else
        '    'tblMins.Visible = True
        '    Table3.Visible = True
        '    ExpResult()

        'End If

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

   
End Class
