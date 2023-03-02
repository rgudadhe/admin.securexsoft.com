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
    Private AccountID As String
    Protected Sub ShowActDetails(ByVal ActID As String, ByVal StartDate As Date, ByVal EndDate As Date, ByVal Export As Boolean)
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
        Dim j As Integer
        i = 0

        strCategory = ""
        Dim strQuery As String
        strQuery = "Select Levelname, LevelNo from tblProductionLevels where JobsRouting = 'True' and LevelNo not in ('1073741824', '2147483647') "
        Dim CMUsr As New SqlCommand(strQuery, New SqlConnection(strConn))
        Try
            CMUsr.Connection.Open()
            Dim DRRec As SqlDataReader = CMUsr.ExecuteReader()
            If DRRec.HasRows Then
                While (DRRec.Read)
                    If i = 0 Then

                        lblStatus.Text = DRRec("LevelName").ToString & "</td>"

                    Else
                        lblStatus.Text = lblStatus.Text & "<td>" & DRRec("LevelName").ToString & "</td>"
                    End If
                    i = i + 1
                End While
            End If
            DRRec.Close()
        Finally
            If CMUsr.Connection.State = System.Data.ConnectionState.Open Then
                CMUsr.Connection.Close()
                CMUsr = Nothing
            End If
        End Try
        lblStatus.Text = lblStatus.Text & "<td>Total"
        strConn = System.Configuration.ConfigurationManager.AppSettings("ETSCon")
        strQuery = "SELECT A.ACCOUNTID, A.ACCOUNTNAME, ISNULL(T.SECS, 0) AS TOTRECSECS, ISNULL(T.RECCOUNT, 0) AS TOTRECCOUNT, ISNULL(T1.SECS, 0) AS TOTRECSECS1, ISNULL(T1.RECCOUNT, 0) AS TOTRECCOUNT1  "
        strQuery = strQuery & " , ISNULL(T2.SECS, 0) AS TOTRECSECS2, ISNULL(T2.RECCOUNT, 0) AS TOTRECCOUNT2  "
        strQuery = strQuery & " , ISNULL(T3.SECS, 0) AS TOTRECSECS3, ISNULL(T3.RECCOUNT, 0) AS TOTRECCOUNT3  "
        strQuery = strQuery & " , ISNULL(T4.SECS, 0) AS TOTRECSECS4, ISNULL(T4.RECCOUNT, 0) AS TOTRECCOUNT4  "
        strQuery = strQuery & " , ISNULL(T.AVGTAT, 0) AS AVGTAT , ISNULL(T.MINTAT, 0) AS MINTAT , ISNULL(T.MAXTAT, 0) AS MAXTAT  "
        strQuery = strQuery & " FROM TBLACCOUNTS A "
        strQuery = strQuery & " LEFT JOIN (SELECT ACCOUNTID, COUNT(JOBNUMBER) AS RECCOUNT,  SUM(DATEDIFF(S, 0,DURATION)) AS SECS, AVG(convert(numeric,DATEDIFF(S, DATECREATED, DATEMODIFIED))) AS AVGTAT, MIN(convert(numeric,DATEDIFF(S, DATECREATED, DATEMODIFIED))) AS MINTAT , MAX(convert(numeric,DATEDIFF(S, DATECREATED, DATEMODIFIED))) AS MAXTAT FROM TBLTRANSCRIPTIONMAIN "
        strQuery = strQuery & " WHERE STATUS = '1073741824' AND DATECREATED BETWEEN '" & StartDate & "' AND '" & EndDate & "'  GROUP BY ACCOUNTID ) AS T "
        strQuery = strQuery & " ON A.ACCOUNTID = T.ACCOUNTID "
        strQuery = strQuery & " LEFT JOIN (SELECT ACCOUNTID, COUNT(JOBNUMBER) AS RECCOUNT,  SUM(DATEDIFF(S, 0,DURATION)) AS SECS  FROM TBLTRANSCRIPTIONMAIN "
        strQuery = strQuery & " WHERE STATUS = '1073741824' AND DATECREATED BETWEEN '" & StartDate & "' AND '" & EndDate & "'   AND DATEDIFF(HH, DATECREATED, DATEMODIFIED) <= 24 "
        strQuery = strQuery & " GROUP BY ACCOUNTID ) AS T1 "
        strQuery = strQuery & " ON A.ACCOUNTID = T1.ACCOUNTID "
        strQuery = strQuery & " LEFT JOIN (SELECT ACCOUNTID, COUNT(JOBNUMBER) AS RECCOUNT,  SUM(DATEDIFF(S, 0,DURATION)) AS SECS  FROM TBLTRANSCRIPTIONMAIN "
        strQuery = strQuery & " WHERE STATUS = '1073741824' AND DATECREATED BETWEEN '" & StartDate & "' AND '" & EndDate & "'    "
        strQuery = strQuery & " AND DATEDIFF(HH, DATECREATED, DATEMODIFIED) <= 48 GROUP BY ACCOUNTID ) AS T2 "
        strQuery = strQuery & " ON A.ACCOUNTID = T2.ACCOUNTID "
        strQuery = strQuery & " LEFT JOIN (SELECT ACCOUNTID, COUNT(JOBNUMBER) AS RECCOUNT,  SUM(DATEDIFF(S, 0,DURATION)) AS SECS  FROM TBLTRANSCRIPTIONMAIN "
        strQuery = strQuery & " WHERE STATUS = '1073741824' AND DATECREATED BETWEEN '" & StartDate & "' AND '" & EndDate & "'    "
        strQuery = strQuery & " AND DATEDIFF(HH, DATECREATED, DATEMODIFIED) <= 72 GROUP BY ACCOUNTID ) AS T3 "
        strQuery = strQuery & " ON A.ACCOUNTID = T3.ACCOUNTID "
        strQuery = strQuery & " LEFT JOIN (SELECT ACCOUNTID, COUNT(JOBNUMBER) AS RECCOUNT,  SUM(DATEDIFF(S, 0,DURATION)) AS SECS  FROM TBLTRANSCRIPTIONMAIN "
        strQuery = strQuery & " WHERE STATUS = '1073741824' AND DATECREATED BETWEEN '" & StartDate & "' AND '" & EndDate & "'   AND DATEDIFF(HH, DATECREATED, DATEMODIFIED) > 72 "
        strQuery = strQuery & " GROUP BY ACCOUNTID ) AS T4  "
        strQuery = strQuery & " ON A.ACCOUNTID = T4.ACCOUNTID "
        'Response.Write(strQuery)
        'Response.End()

        Dim SQLCmdT As New SqlCommand(strQuery, New SqlConnection(strConn))
        Try
            SQLCmdT.Connection.Open()
            Dim DRRecT As SqlDataReader = SQLCmdT.ExecuteReader()
            If DRRecT.HasRows Then
                While DRRecT.Read
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
                        If Export = False Then
                            Cell1.Text = "<a href='DailyTAT.aspx?AccountID=" & DRRecT(0).ToString & "&showDict=Yes&startdate=" & StartDate & "&EndDate=" & EndDate & "'>" & DRRecT(1).ToString & "</a>"
                        Else
                            Cell1.Text = DRRecT(1).ToString
                        End If

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
                        tblMins.Rows.Add(Row1)

                    End If
                End While
            End If
            DRRecT.Close()
        Finally
            If SQLCmdT.Connection.State = System.Data.ConnectionState.Open Then
                SQLCmdT.Connection.Close()
                SQLCmdT = Nothing
            End If
        End Try

    End Sub

  


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            Dim sdate As Date
            Dim edate As Date
            Dim strconn As String
            strconn = System.Configuration.ConfigurationManager.AppSettings("ETSCon")
            Dim strQuery As String
            strQuery = "Select Levelname, LevelNo from tblProductionLevels where JobsRouting = 'True' and LevelNo not in ('1073741824', '2147483647') "
            Dim CMUsr As New SqlCommand(strQuery, New SqlConnection(strconn))
            Try
                CMUsr.Connection.Open()
                Dim DRRec1 As SqlDataReader = CMUsr.ExecuteReader()
                If DRRec1.HasRows Then
                    While (DRRec1.Read)
                        Dim LItem As New ListItem
                        LItem.Text = DRRec1("LevelName").ToString
                        LItem.Value = DRRec1("LevelNo").ToString
                        DLStatus.Items.Add(LItem)
                    End While
                End If
                DRRec1.Close()
            Finally
                If CMUsr.Connection.State = System.Data.ConnectionState.Open Then
                    CMUsr.Connection.Close()
                    CMUsr = Nothing
                End If
            End Try

            Dim SQLCmd As New SqlCommand("Select * from tblaccounts order by description", New SqlConnection(strconn))

            SQLCmd.Connection.Open()
            Dim DRRec As SqlDataReader = SQLCmd.ExecuteReader()
            If DRRec.Read = True Then
                While (DRRec.Read)
                    Dim LI As New ListItem
                    LI.Text = DRRec("Description").ToString
                    LI.Value = DRRec("AccountID").ToString
                    DLAct.Items.Add(LI)
                End While
            End If
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
                ShowActDetails(AccountID, sdate, edate, False)
            End If

        End If


    End Sub

    Protected Sub tblsubmit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles tblsubmit.Click
        sDate = Date.Parse(TXTSDate.Text)
        eDate = Date.Parse(TXTEDate.Text)

        If DLAct.SelectedValue <> "" Then

            AccountID = DLAct.SelectedValue.ToString
            ShowActDetails(AccountID, sDate, eDate, False)
        Else
            ShowActDetails(AccountID, sDate, eDate, False)
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
        ShowActDetails(DLAct.SelectedValue, sDate, eDate, True)

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

    Protected Sub DLStatus_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles DLStatus.SelectedIndexChanged
        DLUsers.Items.Clear()
        Dim LI As New ListItem
        LI.Value = ""
        LI.Text = "Select User"
        DLUsers.Items.Add(LI)
        If DLStatus.SelectedValue <> "" Then
            Dim strQuery As String
            strQuery = "Select distinct  U.FirstName, U.FirstName + ' ' + U.LastName + '(' + U.username + ')' as uname, U.UserID from tblUsers  U, tblUsersLevels UL  where (U.Isdeleted is NULL) or (U.Isdeleted = 'False') AND  UL.UserID = U.USerID and dbo.chkLevel(UL.ProductionLevel, " & DLStatus.SelectedValue & ")='True'   order by U.firstname"
            Dim strconn As String
            strconn = System.Configuration.ConfigurationManager.AppSettings("ETSCon")
            Dim CMUsr As New SqlCommand(strQuery, New SqlConnection(strconn))
            Try
                CMUsr.Connection.Open()
                Dim DRRec1 As SqlDataReader = CMUsr.ExecuteReader()
                If DRRec1.HasRows Then
                    While (DRRec1.Read)
                        Dim LItem As New ListItem
                        LItem.Text = DRRec1("uname").ToString
                        LItem.Value = DRRec1("userid").ToString
                        DLUsers.Items.Add(LItem)
                    End While
                End If
                DRRec1.Close()
            Finally
                If CMUsr.Connection.State = System.Data.ConnectionState.Open Then
                    CMUsr.Connection.Close()
                    CMUsr = Nothing
                End If
            End Try
        End If
    End Sub
End Class
