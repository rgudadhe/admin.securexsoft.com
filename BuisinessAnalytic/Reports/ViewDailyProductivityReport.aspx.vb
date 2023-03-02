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


Partial Class DailyTeamReport
    Inherits BasePage
    Protected Sub ShowActDetails()
        Dim strConn As String = System.Configuration.ConfigurationManager.AppSettings("ETSCon")
        Dim strDate As String
        Dim strCategory As String = String.Empty
        Dim InpDate As Date
        
        strDate = Request("TransDate")

        InpDate = Date.Parse(strDate)
        'Lbldate.Text = InpDate.ToShortDateString
        Dim CurrSDate As Date
        Dim CurrEDate As Date
        Dim CurrWDays As Integer
        CurrSDate = Month(InpDate) & "/1/" & Year(InpDate)
        CurrEDate = DateAdd(DateInterval.Day, 1, InpDate)
        CurrWDays = WorkingDays(CurrSDate, CurrEDate)
        Dim DayDiff As Integer
        DayDiff = DateDiff(DateInterval.Day, CurrSDate, InpDate)
        Dim I As Integer = 0
        Dim STMTLines As Integer = 0
        Dim STMTPLines As Integer = 0
        Dim STQALines As Integer = 0
        Dim STPPQALines As Integer = 0
        Dim STMTBLines As Integer = 0
        Dim STQABLines As Integer = 0
        Dim STQABSELines As Integer = 0
        Dim STMTDMTLines As Integer = 0
        Dim STMTDMTPLines As Integer = 0
        Dim STMTDQALines As Integer = 0
        Dim STMTDPPQALines As Integer = 0
        Dim STMTDMTBLines As Integer = 0
        Dim STMTDQABLines As Integer = 0
        Dim STMTDQABSELines As Integer = 0
        Dim SDTotalLines As Integer = 0
        Dim SMTotalLines As Integer = 0
        Dim SMTotalTrg As Integer = 0
        Dim SDTotalTrg As Integer = 0
        Dim TAvgPreMins As Integer = 0
        Dim TAvgCurrMins As Integer = 0
        Dim TDayMins1 As Integer = 0
        Dim TDayMins2 As Integer = 0
        Dim TDayMins3 As Integer = 0
        Dim TDayMins4 As Integer = 0
        Dim TDayMins5 As Integer = 0
        Dim TDayMins6 As Integer = 0
        Dim TDayMins7 As Integer = 0
        Dim DailyTLines As Long = 0
        Dim MonTLines As Long = 0
        Dim DailyTrg As Long = 0
        Dim MonTrg As Long = 0
        Dim EmpRec As Boolean = False
        Dim objrep As New ETS.BL.BusinessAnalytics
        Dim DTSet2 As System.Data.DataSet
        DTSet2 = objrep.GetProductivityReportByUserID(New Guid(Session("contractorID").ToString), DayDiff, InpDate, New Guid(Request("userID")))
        'Response.Write(Session("contractorID").ToString & "','" & DayDiff & "','" & InpDate & "','" & Request("userID"))
        'Response.Write("#" & DTSet2.Tables.Count)
        If DTSet2.Tables.Count > 0 Then
            'Response.Write("#" & DTSet2.Tables(0).Rows.Count)
            If DTSet2.Tables(0).Rows.Count > 0 Then
                For Each DRRecT As Data.DataRow In DTSet2.Tables(0).Rows
                    'If DRRecT.HasRows Then
                    'While DRRecT.Read
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
                    Dim Cell20 As New TableCell
                    Dim Cell21 As New TableCell
                    Dim Cell22 As New TableCell
                    Dim Cell23 As New TableCell
                    Dim Cell24 As New TableCell
                    Dim Cell25 As New TableCell
                    Dim Cell26 As New TableCell
                    Row1.HorizontalAlign = HorizontalAlign.Right

                    If IsDBNull(DRRecT("TrgLines").ToString) Then
                        DailyTrg = 500
                    Else
                        If DRRecT("TrgLines").ToString = "" Then
                            DailyTrg = 500
                        ElseIf CInt(DRRecT("TrgLines").ToString) > 0 Then
                            DailyTrg = DRRecT("TrgLines").ToString
                        Else
                            DailyTrg = 500
                        End If
                    End If

                    Cell1.Text = DRRecT("TransDate").ToString

                    'Cell5.Text = DRRecT(4).ToString
                    'Cell6.Text = DRRecT(5).ToString
                    'Cell7.Text = DRRecT(6).ToString
                    'Cell8.Text = DRRecT(7).ToString
                    'Cell9.Text = DRRecT(8).ToString
                    'Cell10.Text = DRRecT(9).ToString
                    'Cell11.Text = DRRecT(10).ToString

                    Cell19.Text = DRRecT(4).ToString
                    Cell20.Text = DRRecT(5).ToString
                    Cell21.Text = DRRecT(6).ToString
                    Cell22.Text = DRRecT(7).ToString
                    Cell23.Text = DRRecT(8).ToString
                    Cell24.Text = DRRecT(9).ToString
                    Cell25.Text = DRRecT(10).ToString
                    DailyTLines = CInt(DRRecT(4).ToString) + (CInt(DRRecT(5).ToString) * 1.75) + CInt(DRRecT(6).ToString) + (CInt(DRRecT(7).ToString) * 1.5) + (CInt(DRRecT(8).ToString) * 0.5) + (CInt(DRRecT(9).ToString) * 1.5) + CInt(DRRecT(10).ToString)
                    'MonTLines = CInt(DRRecT(4).ToString) + (CInt(DRRecT(5).ToString) * 1.75) + CInt(DRRecT(6).ToString) + (CInt(DRRecT(7).ToString) * 1.5) + (CInt(DRRecT(8).ToString) * 0.5) + (CInt(DRRecT(9).ToString) * 1.5) + CInt(DRRecT(10).ToString)
                    
                    If DRRecT("DNAME").ToString = "EMPLOYEE" Then
                        EmpRec = True
                        Cell12.Text = DailyTrg
                        Cell13.Text = DailyTLines
                        Cell14.Text = FormatNumber((DailyTLines / DailyTrg) * 100, 0) & "%"

                    Else
                        Cell12.Text = "-"
                        Cell13.Text = "-"
                        Cell14.Text = "-"

                    End If

                    Cell17.Text = "-"
                    Cell18.Text = "-"

                    Cell1.HorizontalAlign = HorizontalAlign.Left
                    Cell2.HorizontalAlign = HorizontalAlign.Left
                    Cell4.HorizontalAlign = HorizontalAlign.Left

                    'STMTLines += DRRecT(4).ToString
                    'STMTPLines += DRRecT(5).ToString
                    'STQALines += DRRecT(6).ToString
                    'STPPQALines += DRRecT(8).ToString
                    'STMTBLines += DRRecT(9).ToString
                    'STQABLines += DRRecT(7).ToString
                    'STQABSELines += DRRecT(10).ToString
                    STMTDMTLines += DRRecT(4).ToString
                    STMTDMTPLines += DRRecT(5).ToString
                    STMTDQALines += DRRecT(6).ToString
                    STMTDMTBLines += DRRecT(7).ToString
                    STMTDQABLines += DRRecT(8).ToString
                    STMTDQABSELines += DRRecT(9).ToString
                    STMTDPPQALines += DRRecT(10).ToString

                    SDTotalLines += DailyTLines
                    SDTotalTrg += DailyTrg
                    SMTotalLines += MonTLines
                    SMTotalTrg += MonTrg
                    'Row1.CssClass = "tblbg2"
                    Row1.ForeColor = Drawing.Color.Black
                    Row1.Cells.Add(Cell1)
                    'Row1.Cells.Add(Cell2)
                    'Row1.Cells.Add(Cell4)
                    'Row1.Cells.Add(Cell5)
                    'Row1.Cells.Add(Cell6)
                    'Row1.Cells.Add(Cell7)
                    'Row1.Cells.Add(Cell10)
                    'Row1.Cells.Add(Cell8)
                    'Row1.Cells.Add(Cell11)
                    'Row1.Cells.Add(Cell9)
                    'Row1.Cells.Add(Cell12)
                    'Row1.Cells.Add(Cell3)
                    'Row1.Cells.Add(Cell14)
                    'Row1.Cells.Add(Cell17)
                    Row1.Cells.Add(Cell19)
                    Row1.Cells.Add(Cell20)
                    Row1.Cells.Add(Cell21)
                    Row1.Cells.Add(Cell22)
                    Row1.Cells.Add(Cell23)
                    Row1.Cells.Add(Cell24)
                    Row1.Cells.Add(Cell25)
                    Row1.Cells.Add(Cell12)
                    Row1.Cells.Add(Cell13)
                    Row1.Cells.Add(Cell14)
                    'Row1.Cells.Add(Cell18)
                    tblMins.Rows.Add(Row1)
                    'End While
                Next
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
                Dim A1CellM1 As New TableCell
                Dim A1CellM2 As New TableCell
                Dim A1CellM3 As New TableCell
                Dim A1CellM4 As New TableCell
                Dim A1CellM5 As New TableCell
                A1CellM1.ColumnSpan = 1
                '    ACellM1.Text = "-"
                A1CellM2.Text = "-"
                A1CellM3.Text = "-"
                A1CellM4.Text = "-"
                A1CellM5.Text = "-"
                A1Row1.HorizontalAlign = HorizontalAlign.Right
                '   A1Row1.CssClass = "tblbg"
                ' A1Row1.Font.Bold = True
                A1CellM1.Text = "Total1"
                A1Cell1.Text = STMTLines
                A1Cell2.Text = STMTPLines
                A1Cell3.Text = STQALines
                A1Cell4.Text = STMTBLines
                A1Cell5.Text = STQABLines
                A1Cell6.Text = STQABSELines
                A1Cell7.Text = STPPQALines
                A1Cell8.Text = SDTotalLines
                A1Cell9.Text = SDTotalTrg
                
                A1Cell12.Text = STMTDMTLines
                A1Cell13.Text = STMTDMTPLines
                A1Cell14.Text = STMTDQALines
                A1Cell15.Text = STMTDMTBLines
                A1Cell16.Text = STMTDQABLines
                A1Cell17.Text = STMTDQABSELines
                A1Cell18.Text = STMTDPPQALines
                ' Response.Write(STMTDPPQALines)
                If EmpRec = True Then
                    A1Cell10.Text = SDTotalTrg
                    A1Cell11.Text = SDTotalLines
                    A1CellM4.Text = FormatNumber((SDTotalLines / SDTotalTrg) * 100, 0) & "%"
                Else
                    A1Cell10.Text = "-"
                    A1Cell11.Text = "-"
                    A1CellM4.Text = "-"
                End If

                A1Row1.Cells.Add(A1CellM1)
                'A1Row1.Cells.Add(A1Cell1)
                'A1Row1.Cells.Add(A1Cell2)
                'A1Row1.Cells.Add(A1Cell3)
                'A1Row1.Cells.Add(A1Cell4)
                'A1Row1.Cells.Add(A1Cell5)
                'A1Row1.Cells.Add(A1Cell6)
                'A1Row1.Cells.Add(A1Cell7)
                'A1Row1.Cells.Add(A1Cell8)
                'A1Row1.Cells.Add(A1Cell9)
                'A1Row1.Cells.Add(A1CellM2)
                'A1Row1.Cells.Add(A1CellM3)
                A1Row1.Cells.Add(A1Cell12)
                A1Row1.Cells.Add(A1Cell13)
                A1Row1.Cells.Add(A1Cell14)
                A1Row1.Cells.Add(A1Cell15)
                A1Row1.Cells.Add(A1Cell16)
                A1Row1.Cells.Add(A1Cell17)
                A1Row1.Cells.Add(A1Cell18)
                A1Row1.Cells.Add(A1Cell10)
                A1Row1.Cells.Add(A1Cell11)
                A1Row1.Cells.Add(A1CellM4)
                'A1Row1.Cells.Add(A1CellM5)
                A1Row1.CssClass = "alt1"
                'tblMins.Rows.Add(A1Row1)

            End If
            End If



    End Sub







    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        ShowActDetails()
        Dim strURL As String = "userphoto.aspx?UserID=" & Request("UserID")

        Image1.ImageUrl = strURL
        TxtFirstName.ReadOnly = False
        TxtLastName.ReadOnly = False
        TxtEmail.ReadOnly = False
        TxtDOB.ReadOnly = False
        TxtJoin.ReadOnly = False
        TxtAdd.ReadOnly = False
        TxtCell.ReadOnly = False
        TxtTel.ReadOnly = False
        TxtDept.ReadOnly = False
        TxtDesn.ReadOnly = False
        TxtCategory.ReadOnly = False
        Dim strConn As String = System.Configuration.ConfigurationManager.AppSettings("ETSCon")
        Dim SQLCmd As New SqlCommand("Select U.*, D.Name as Deptname, D1.Name as Desnname, C.Name as Category  from tblUsers U LEFT OUTER JOIN tblDepartments D ON D.DepartmentID = U.DepartmentID LEFT OUTER JOIN tblusersCategory C ON C.CategoryID = U.CategoryID  LEFT OUTER JOIN tblDeptDesignations D1 ON D1.DesignationID = U.DesignationID where UserID='" & Request("UserID") & "'", New SqlConnection(strConn))
        Try
            SQLCmd.Connection.Open()
            Dim DRRec As SqlDataReader = SQLCmd.ExecuteReader()
            If DRRec.Read = True Then



                TxtFirstName.Text = UCase(DRRec("firstname").ToString)
                TxtLastName.Text = UCase(DRRec("lastname").ToString)
                TxtEmail.Text = UCase(DRRec("OfficialMailID").ToString)
                
                TxtAdd.Text = DRRec("Address").ToString
               
                If Not IsDate(DRRec("DatOfBirth").ToString) Then
                    TxtDOB.Text = ""
                Else
                    TxtDOB.Text = FormatDateTime(DRRec("DatOfBirth").ToString, DateFormat.ShortDate)
                End If
                If Not IsDate(DRRec("DateJoined").ToString) Then
                    TxtJoin.Text = ""
                Else
                    TxtJoin.Text = FormatDateTime(DRRec("DateJoined").ToString, DateFormat.ShortDate)
                End If

                TxtCell.Text = DRRec("CellNo").ToString
                TxtTel.Text = DRRec("PhoneNo").ToString
                TxtDept.Text = DRRec("Deptname").ToString
                TxtDesn.Text = DRRec("Desnname").ToString
                TxtCategory.Text = DRRec("Category").ToString
                'Dim DepartmentID = DRRec("DepartmentID").ToString
                'Dim DesignationID = DRRec("DesignationID").ToString

            End If
            DRRec.Close()
        Finally
            If SQLCmd.Connection.State = System.Data.ConnectionState.Open Then
                SQLCmd.Connection.Close()
                SQLCmd = Nothing
            End If
        End Try
    End Sub

    
    Function WorkingDays(ByVal StartDate As Date, ByVal EndDate As Date) As Integer

        Dim intCount As Integer
        intCount = 0
        Do While StartDate <= EndDate
            Select Case Weekday(StartDate)
                Case "1"
                    intCount = intCount
                Case "7"
                    intCount = intCount + 1
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
        filename = "Daily Team Report " & Month(Now) & Day(Now) & Year(Now) & ".xls"
        Dim attachment As String = "attachment; filename=" & filename
        Response.ClearContent()
        Response.AddHeader("content-disposition", attachment)
        Response.ContentType = "application/ms-excel"
        Dim sw As New StringWriter()
        Dim htw As New HtmlTextWriter(sw)

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
        'tblMins.Rows(0).ForeColor = Drawing.Color.DarkOrange
        'tblMins.Rows(1).ForeColor = Drawing.Color.DarkOrange
        'tblMins.Rows(2).ForeColor = Drawing.Color.DarkOrange
        'Dim i As Integer
        'For i = 3 To tblMins.Rows.Count - 1
        '    tblMins.Rows(i).Font.Size = "10"
        '    'tblMins.Rows(i).BackColor = Drawing.Color.WhiteSmoke
        'Next
        'tblMins.Rows(0).BackColor = Drawing.Color.LightSlateGray
        'tblMins.Rows(1).BackColor = Drawing.Color.LightSlateGray
        'tblMins.Rows(2).BackColor = Drawing.Color.LightSlateGray
        'tblMins.Rows(0).ForeColor = Drawing.Color.WhiteSmoke
        'tblMins.Rows(1).ForeColor = Drawing.Color.WhiteSmoke
        'tblMins.Rows(2).ForeColor = Drawing.Color.WhiteSmoke
        'tblMins.ForeColor = Drawing.Color.Black
        'tblMins.Rows(0).Font.Bold = True
        'tblMins.Rows(1).Font.Bold = True
        'tblMins.Rows(2).Font.Bold = True
        'tblMins.Rows(0).Font.Italic = True
        'tblMins.Rows(1).Font.Italic = True
        'tblMins.Rows(2).Font.Italic = True
        'tblMins.Font.Size = "9.5"
        'tblMins.GridLines = GridLines.Both
        'tblMins.ForeColor = Drawing.Color.DarkGray
        tblMins.RenderControl(htw)
        'MyDataGrid.RenderControl(htw)
        Response.Write(sw.ToString())
        Response.[End]()

    End Sub

End Class
