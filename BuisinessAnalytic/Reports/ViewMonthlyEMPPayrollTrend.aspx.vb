Imports System.Data.Sqlclient
Imports System.Data
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports System.Web.UI.WebControls.WebParts
Imports System.Web.UI.HtmlControls
Imports System.IO

Partial Class ViewUnitsTrend
    Inherits BasePage
    Protected Sub ShowActDetails()
        Dim objrep As New ETS.BL.BusinessAnalytics
        Dim EndDate1 As Date
        Dim startdate1 As Date
        Dim EndDate2 As Date
        Dim startdate2 As Date
        Dim DayDiff As Integer
        Dim MonthDiff As Integer
        Dim Lines As Long
        Dim Revenue As Long
        Dim cost As Long
        Dim RPL As Long
        Dim CPL As Long
        Dim CompYear As String = String.Empty
        Dim CompMonth As String = String.Empty
        'If Month(Now) = DLMonth.SelectedValue And Year(Now) = DLYear.SelectedValue Then
        '    EndDate = Now.ToShortDateString
        '    startdate = Month(EndDate) & "/01/" & Year(EndDate)
        '    DayDiff = DateDiff(DateInterval.Day, startdate, EndDate)
        'Else
        '    startdate = DLMonth.SelectedValue & "/1/" & DLYear.SelectedValue
        '    EndDate = startdate.AddMonths(1).AddDays(-1)
        '    DayDiff = DateDiff(DateInterval.Day, startdate, EndDate)
        'End If
        startdate1 = DLMonth1.SelectedValue & "/1/" & DLYear1.SelectedValue
        startdate2 = DLMonth2.SelectedValue & "/1/" & DLYear2.SelectedValue
        EndDate2 = startdate2.AddMonths(1).AddDays(-1)
        DayDiff = DateDiff(DateInterval.Day, startdate1, EndDate2)
        MonthDiff = DateDiff(DateInterval.Month, startdate1, EndDate2)
        Dim ParmValues As String = String.Empty
        ParmValues = "[" & startdate1.ToString("M/dd/yyyy") & "]"
        For I As Integer = 1 To MonthDiff

            ParmValues = ParmValues & ",[" & startdate1.AddMonths(I).ToString("M/dd/yyyy") & "]"
        Next

        Dim CompDate As Date
        If CheckBox1.Checked = True Then
            CompDate = DLMonth.SelectedValue & "/1/" & DLYear.SelectedValue
            ParmValues = ParmValues & ",[CompMonth]"
            CompMonth = DLMonth.SelectedValue
            CompYear = DLYear.SelectedValue
        End If
        'Response.Write(ParmValues)
        Dim DTSet2 As System.Data.DataSet = objrep.GetEMPMonthlyPayrollTrends(New Guid(Session("ContractorId").ToString), DayDiff, EndDate2, Nothing, ParmValues, CheckBox1.Checked, CompMonth, CompYear, DLTrend.SelectedValue)

        'Response.Write(ParmValues & "#" & EndDate2 & "#" & DayDiff & "#" & Session("ContractorId").ToString)

        If DTSet2.Tables.Count > 0 Then
            If DTSet2.Tables(0).Rows.Count > 0 Then
                Dim dt As Date
                For Each dc As DataColumn In DTSet2.Tables(0).Columns
                    If IsDate(dc.ColumnName) Then
                        dt = dc.ColumnName
                        dc.ColumnName = dt.ToString("MMM yyyy")
                        'Response.Write(dc.ColumnName)
                    End If

                Next
                If CheckBox1.Checked = True Then
                    DTSet2.Tables(0).Columns((DTSet2.Tables(0).Columns.Count - 1)).ColumnName = CompDate.ToString(" - MMM yyyy")
                End If
                Dim ColTotal() As Long
                Dim ClmCount As Integer = 0
                Dim ColNumValue As Long
                For I As Integer = 0 To DTSet2.Tables(0).Rows.Count - 1
                    ClmCount = 0
                    For j As Integer = 4 To DTSet2.Tables(0).Columns.Count - 1
                        'ClmCount += 1
                        If I = 0 Then
                            ReDim Preserve ColTotal(j)
                        End If
                        'Response.Write(MyDataGrid.Rows(I).Cells(j).Text())
                        If DTSet2.Tables(0).Rows(I).Item(j).ToString <> "" Then
                            If IsNumeric(DTSet2.Tables(0).Rows(I).Item(j).ToString) Then
                                ColNumValue = DTSet2.Tables(0).Rows(I).Item(j).ToString
                                ColTotal(j) = ColTotal(j) + ColNumValue
                                'Response.Write(FormatNumber(ColNumValue, 0, , TriState.False, ))
                                'DTSet2.Tables(0).Rows(I).Item(j) = FormatNumber(ColNumValue, 0, , TriState.False, ).ToString
                            Else
                                DTSet2.Tables(0).Rows(I).Item(j) = 0
                            End If
                        Else

                            DTSet2.Tables(0).Rows(I).Item(j) = 0
                        End If
                        If IsNumeric(DTSet2.Tables(0).Rows(I).Item(j).ToString) Then
                            Try


                                DTSet2.Tables(0).Rows(I).Item(j) = FormatNumber(DTSet2.Tables(0).Rows(I).Item(j).ToString, 0)
                            Catch ex As Exception
                                'Response.Write(DTSet2.Tables(0).Rows(I).Item(j).ToString())
                            End Try
                        End If

                    Next

                Next
                Dim DTRow As DataRow = DTSet2.Tables(0).NewRow
                DTRow(3) = "Total"
                For j As Integer = 4 To DTSet2.Tables(0).Columns.Count - 1
                    DTRow(j) = FormatNumber(ColTotal(j), 0)
                Next
                DTSet2.Tables(0).Rows.InsertAt(DTRow, 0)
                If CheckBox1.Checked = True Then
                    If CheckBox2.Checked Or CheckBox3.Checked = True Then

                        Dim ColComp1 As Integer = DTSet2.Tables(0).Columns.Count - 1
                        Dim ColComp2 As Integer = DTSet2.Tables(0).Columns.Count - 2
                        Dim ColValue1 As Long
                        Dim ColValue2 As Long
                        Dim CompPerc As Integer
                        Dim CompValue As Long

                        Dim NewCol1 As Integer = DTSet2.Tables(0).Columns.Count
                        Dim NewCol2 As Integer = DTSet2.Tables(0).Columns.Count + 1
                        If CheckBox2.Checked Then
                            Dim DTCol As New DataColumn
                            DTCol.ColumnName = "Difference"
                            DTSet2.Tables(0).Columns.Add(DTCol)
                        End If
                        If CheckBox3.Checked Then
                            Dim DTCol As New DataColumn
                            DTCol.ColumnName = "Percentage"
                            DTSet2.Tables(0).Columns.Add(DTCol)
                        End If

                        'Response.Write(NewCol)
                        'Response.Write(ColComp1)
                        'Response.Write(ColComp2)
                        For I As Integer = 0 To DTSet2.Tables(0).Rows.Count - 1
                            If IsNumeric(DTSet2.Tables(0).Rows(I).Item(ColComp2)) Then
                                ColValue2 = DTSet2.Tables(0).Rows(I).Item(ColComp2)
                            Else
                                ColValue2 = 0
                            End If
                            If IsNumeric(DTSet2.Tables(0).Rows(I).Item(ColComp1)) Then
                                ColValue1 = DTSet2.Tables(0).Rows(I).Item(ColComp1)
                            Else
                                ColValue1 = 0
                            End If
                            If ColValue1 = 0 And ColValue2 = 0 Then
                                CompPerc = 0
                            ElseIf ColValue1 = 0 And ColValue2 > 0 Then
                                CompPerc = 100
                            ElseIf ColValue1 > 0 And ColValue2 = 0 Then
                                CompPerc = -100
                            Else
                                CompPerc = (ColValue2 / ColValue1) * 100
                            End If
                            CompValue = ColValue2 - ColValue1
                            If CheckBox2.Checked And CheckBox3.Checked Then
                                If CheckBox4.Checked Then
                                    DTSet2.Tables(0).Rows(I).Item(NewCol1) = FormatNumber(CompValue, 0, , TriState.True, )
                                Else
                                    DTSet2.Tables(0).Rows(I).Item(NewCol1) = FormatNumber(CompValue, 0, , TriState.False, )
                                End If
                                DTSet2.Tables(0).Rows(I).Item(NewCol2) = CompPerc & " %"

                            ElseIf CheckBox2.Checked Then
                                If CheckBox4.Checked Then
                                    DTSet2.Tables(0).Rows(I).Item(NewCol1) = FormatNumber(CompValue, 0, , TriState.True, )
                                Else
                                    DTSet2.Tables(0).Rows(I).Item(NewCol1) = FormatNumber(CompValue, 0, , TriState.False, )
                                End If
                            ElseIf CheckBox3.Checked Then
                                DTSet2.Tables(0).Rows(I).Item(NewCol1) = CompPerc & " %"
                            End If

                            'ClmCount = 0
                            'For j As Integer = 2 To DTSet2.Tables(0).Columns.Count - 1
                            '    'ClmCount += 1
                            '    If I = 0 Then
                            '        ReDim ColTotal(j)
                            '    End If
                            '    'Response.Write(MyDataGrid.Rows(I).Cells(j).Text())
                            '    If DTSet2.Tables(0).Rows(I).Item(j).ToString <> "" Then
                            '        If IsNumeric(DTSet2.Tables(0).Rows(I).Item(j).ToString) Then
                            '            ColTotal(j) = ColTotal(j) + DTSet2.Tables(0).Rows(I).Item(j).ToString
                            '        End If
                            '    End If

                            'Next

                        Next






                    End If
                End If

                For Each dc As DataColumn In DTSet2.Tables(0).Columns
                    'dt is source datatable 
                    Dim bf As New BoundField()

                    bf.DataField = dc.ColumnName
                    bf.HeaderText = dc.ColumnName
                    bf.SortExpression = dc.ColumnName
                    'gvAllReports is GridView 
                    MyDataGrid.Columns.Add(bf)

                Next
                Dim Source As DataView = DTSet2.Tables(0).DefaultView
                'Source.Sort = sortExpression + direction
                'If Source.Table.Rows.Count > 0 Then
                '    MenuPnl.Visible = True
                '    PLPage.Visible = True
                '    pnl1.Visible = True
                'End If

                'Source.Sort = sortExpression + direction
                MyDataGrid.DataSource = Source
                MyDataGrid.DataBind()
                AttachColumn()
            End If
        End If



    End Sub
    Protected Sub AttachColumn()
        For Each objRow As GridViewRow In MyDataGrid.Rows
            If objRow.RowIndex = 0 Then
                objRow.Font.Bold = True
                objRow.BackColor = Drawing.Color.Navy
                objRow.ForeColor = Drawing.Color.White
            Else
                'If objRow.Cells(0).Text = "11111111-1111-1111-1111-111111111111" Then
                '    objRow.Cells(1).Text = "<a href='ViewAccountWiseCPL.aspx?Month=" & DLMonth1.SelectedValue & "&Year=" & DLYear1.SelectedValue & "'>" & objRow.Cells(1).Text & "</a>"
                'Else
                '    objRow.Cells(1).Text = "<a href='ViewOtherPlatformCPL.aspx?Month=" & DLMonth1.SelectedValue & "&Year=" & DLYear1.SelectedValue & "&AccountID=" & objRow.Cells(0).Text & "'>" & objRow.Cells(1).Text & "</a>"
                'End If
            End If
        Next
        'Dim dt As Date
        For I As Integer = 2 To MyDataGrid.Columns.Count - 1
            'MyDataGrid.Columns(I).ItemStyle.CssClass = "alt3"
        Next
        MyDataGrid.HeaderRow.HorizontalAlign = HorizontalAlign.Center
        MyDataGrid.Columns(1).ItemStyle.HorizontalAlign = HorizontalAlign.Left
        MyDataGrid.Columns(2).ItemStyle.HorizontalAlign = HorizontalAlign.Left
        MyDataGrid.Columns(3).ItemStyle.HorizontalAlign = HorizontalAlign.Left
        'MyDataGrid.Columns(1).ItemStyle.CssClass = "alt2"
        'MyDataGrid.AlternatingRowStyle.CssClass = "alt2"
        'MyDataGrid.RowStyle.CssClass = "alt3"
        '    If IsDate(MyDataGrid.Columns(I).HeaderText) Then
        '        Response.Write(MyDataGrid.Columns(I).HeaderText)
        '        dt = MyDataGrid.Columns(I).HeaderText
        '        MyDataGrid.Columns(I).HeaderText = dt.ToString("MMM yyyy")
        '        Response.Write(MyDataGrid.Columns(I).HeaderText)
        '    End If
        'Next
        MyDataGrid.Columns(0).Visible = False
        'Dim dgvRow As New MyDataGrid.
        'MyDataGrid.UpdateRow(0, True)
    End Sub






    Protected Sub btnsubmit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnsubmit.Click
        ShowActDetails()
    End Sub

    'Protected Sub MyDataGrid_PreRender(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyDataGrid.PreRender

    '    Dim dt As Date
    '    For I As Integer = 0 To MyDataGrid.Columns.Count - 1
    '        If IsDate(MyDataGrid.Columns(I).HeaderText) Then
    '            Response.Write(MyDataGrid.Columns(I).HeaderText)
    '            dt = MyDataGrid.Columns(I).HeaderText
    '            MyDataGrid.Columns(I).HeaderText = dt.ToString("MMM yyyy")
    '        End If
    '    Next

    'End Sub

    'Protected Sub MyDataGrid_RowCreated(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles MyDataGrid.RowCreated
    '    If e.Row.RowType = DataControlRowType.Header Then
    '        Dim dt As Date
    '        For I As Integer = 0 To e.Row.Cells.Count - 1
    '            If IsDate(e.Row.Cells(I).Text) Then
    '                Response.Write(e.Row.Cells(I).Text)
    '                dt = e.Row.Cells(I).Text
    '                e.Row.Cells(I).Text = dt.ToString("MMM yyyy")
    '            End If
    '        Next
    '    End If
    '    'MyDataGrid.DataBind()
    'End Sub

    'Protected Sub MyDataGrid_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles MyDataGrid.RowDataBound
    '    If e.Row.RowType = DataControlRowType.Header Then
    '        Dim dt As Date
    '        For I As Integer = 0 To e.Row.Cells.Count - 1
    '            If IsDate(e.Row.Cells(I).Text) Then
    '                Response.Write(e.Row.Cells(I).Text)
    '                dt = e.Row.Cells(I).Text
    '                e.Row.Cells(I).Text = dt.ToString("MMM yyyy")
    '            End If
    '        Next
    '    End If
    '    'MyDataGrid.Columns(0).Visible = False
    'End Sub
    Protected Sub Button1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button1.Click
        Dim filename As String
        filename = "Monthly Employee  Payroll Trend " & Month(Now) & Day(Now) & Year(Now) & ".xls"
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
        ShowActDetails()
        Dim Table1 As New Table
        Table1.GridLines = GridLines.Both
        Table1.Font.Name = "Trebuchet MS"
        Table1.Font.Size = 8
        'Table1.CssClass = "common"
        'Table1.Font.Italic = True
        Dim x As Integer
        If (Not (MyDataGrid.HeaderRow) Is Nothing) Then
            Dim TRow1 As New TableRow
            For x = 1 To MyDataGrid.HeaderRow.Cells.Count - 1
                If MyDataGrid.Columns(x).Visible = True Then
                    Dim TCell1 As New TableCell
                    TCell1.Text = MyDataGrid.HeaderRow.Cells(x).Text
                    TCell1.Font.Bold = True
                    TCell1.BackColor = Drawing.Color.Gray
                    TCell1.ForeColor = Drawing.Color.White
                    TRow1.Cells.Add(TCell1)
                End If
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
            For i = 1 To row.Cells.Count - 1
                If MyDataGrid.Columns(i).Visible = True Then
                    Dim TCell1 As New TableCell
                    TCell1.Text = row.Cells(i).Text
                    TRow1.Cells.Add(TCell1)
                End If
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

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            GetMyMonthList(DLMonth1, True)
            GetMyYearList(DLYear1, True)
            GetMyMonthList(DLMonth2, True)
            GetMyYearList(DLYear2, True)
            GetMyMonthList(DLMonth, True)
            GetMyYearList(DLYear, True)
            DLMonth1.SelectedIndex = DLMonth1.Items.IndexOf(DLMonth1.Items.FindByValue(Now.AddMonths(-1).Month))
            DLYear1.SelectedIndex = DLYear1.Items.IndexOf(DLYear1.Items.FindByValue(Now.AddMonths(-1).Year))
            DLMonth2.SelectedIndex = DLMonth2.Items.IndexOf(DLMonth2.Items.FindByValue(Now.Month))
            DLYear2.SelectedIndex = DLYear2.Items.IndexOf(DLYear2.Items.FindByValue(Now.Year))
        End If
    End Sub
End Class
