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

    Protected Sub ShowActDetails(ByVal StartDate As Date, ByVal EndDate As Date)
        Dim strConn As String
        Dim strDate As String
        Dim strCategory As String
        Dim mins As Long = 0
        Dim lines As Long = 0
        Dim numjobs As Long = 0

        Dim daydiff As Integer
        strCategory = ""
        Dim strQuery As String
        daydiff = DateDiff(DateInterval.Day, StartDate, EndDate)
        strConn = System.Configuration.ConfigurationManager.AppSettings("ETSCon")
        strQuery = "select A.AccountName, sum(L.stdLCCount) AS LINES, SUM(DATEDIFF(S, 0,DURATION)/60) AS MINS, Count(M.TranscriptionID) AS NumJobs from AdminSecureweb.dbo.tbldictationdetails M, tblBillingLines L, tblAccounts A "
        strQuery = strQuery & " WHERE (L.Priority is null or L.Priority = 'False') and M.TranscriptionID = L.TranscriptionID and A.AccountID=M.AccountID and A.contractorID = '" & Session("contractorID").ToString & "'  and  datediff(day,M.datemodified, '" & EndDate & "') between 0 and " & daydiff & "    "
        strQuery = strQuery & " GROUP BY A.ACCOUNTNAME ORDER BY A.ACCOUNTNAME  "
        'Response.Write(strQuery)
        'Response.End()

        Dim SQLCmdT As New SqlCommand(strQuery, New SqlConnection(strConn))
        Try
            SQLCmdT.Connection.Open()
            SQLCmdT.CommandTimeout = 1800
            Dim DRRecT As SqlDataReader = SQLCmdT.ExecuteReader()
            If DRRecT.HasRows Then
                While DRRecT.Read
                    Dim TROW As New TableRow
                    Dim Cell1 As New TableCell
                    Dim Cell2 As New TableCell
                    Dim Cell3 As New TableCell
                    Dim Cell4 As New TableCell
                    Cell1.Text = DRRecT("AccountName").ToString
                    Cell2.Text = DRRecT("NumJobs").ToString
                    Cell3.Text = DRRecT("Mins").ToString
                    Cell4.Text = DRRecT("Lines").ToString
                    numjobs = numjobs + DRRecT("NumJobs").ToString
                    lines = lines + DRRecT("Lines").ToString
                    mins = mins + DRRecT("Mins").ToString
                    TROW.Cells.Add(Cell1)
                    'TROW.Cells.Add(Cell2)
                    'TROW.Cells.Add(Cell3)
                    TROW.Cells.Add(Cell4)
                    tblMins.Rows.Add(TROW)





                End While
                Dim TROW1 As New TableRow
                Dim tCell1 As New TableCell
                Dim tCell2 As New TableCell
                Dim tCell3 As New TableCell
                Dim tCell4 As New TableCell
                TROW1.CssClass = "tblbg"
                tCell1.Text = "Total Lines"
                tCell2.Text = numjobs
                tCell3.Text = mins
                tCell4.Text = lines

                TROW1.Cells.Add(tCell1)
                'TROW1.Cells.Add(tCell2)
                'TROW1.Cells.Add(tCell3)
                TROW1.Cells.Add(tCell4)
                tblMins.Rows.Add(TROW1)
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
        Dim sdate As Date
        Dim edate As Date
        If Not IsPostBack Then
            TXTSDate.Text = Now.AddDays(-1).ToShortDateString
            'CalendarExtender1.SelectedDate = Now.AddDays(-1).ToShortDateString
            TXTEDate.Text = Now.ToShortDateString
            'CalendarExtender2.SelectedDate = Now.ToShortDateString

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


        ShowActDetails(sDate, eDate)

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
