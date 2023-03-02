Imports System.Data.Sqlclient
Imports System.Data
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports System.Web.UI.WebControls.WebParts
Imports System.Web.UI.HtmlControls
Imports System.IO
Partial Class ViewUnitsTrend
    Inherits BasePage
    Dim CurrSDate As Date
    Dim CurrEDate As Date
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
        Dim strDate As String
        Dim strCategory As String = String.Empty
        Dim InpDate As Date
        
        'InpDate = Date.Parse(strDate)
       
        Dim CurrWDays As Integer
        CurrSDate = txtsdate.text
        CurrEDate = txtedate.text
        'CurrWDays = WorkingDays(CurrSDate, CurrEDate)


        DayDiff = DateDiff(DateInterval.Day, CurrSDate, CurrEDate)


        Dim DTSet2 As System.Data.DataSet = objrep.GetMTDHBAProductivityReportbyuser(New Guid(session("UserID").Tostring), CurrSDate, CurrEDate)

        'Response.Write(ParmValues & "#" & EndDate2 & "#" & DayDiff & "#" & Session("ContractorId").ToString)
        If DTSet2.Tables.Count > 0 Then
            If DTSet2.Tables(0).Rows.Count > 0 Then

                Dim ColTotal() As Long
                Dim ClmCount As Integer = 0
                Dim ColNumValue As Long
                For I As Integer = 0 To DTSet2.Tables(0).Rows.Count - 1
                    ClmCount = 0
                    For j As Integer = 5 To DTSet2.Tables(0).Columns.Count - 1
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
                'Dim DTRow As DataRow = DTSet2.Tables(0).NewRow
                'DTRow(4) = "Total"
                'For j As Integer = 5 To DTSet2.Tables(0).Columns.Count - 1
                '    DTRow(j) = FormatNumber(ColTotal(j), 0)
                'Next
                'DTSet2.Tables(0).Rows.InsertAt(DTRow, 0)



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

            objRow.Font.Bold = True
            objRow.BackColor = Drawing.Color.White
            'objRow.ForeColor = Drawing.Color.White
            objRow.Cells(1).Text = "<a href='ViewProductivityMTDHBAReportbyUserN.aspx?userid=" & objRow.Cells(0).Text & "&StartDate=" & TxtSDate.Text & "&EndDate=" & TxtEDate.Text & "'>" & objRow.Cells(1).Text & "</a>"
            'MyDataGrid.Columns(I).ItemStyle.CssClass = "alt3"
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

    Protected Sub tblsubmit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles tblsubmit.Click
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
        filename = "Daily Productivity " & Month(Now) & Day(Now) & Year(Now) & ".xls"
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

    Protected Sub MyDataGrid_RowCreated(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles MyDataGrid.RowCreated
        If e.Row.RowType = DataControlRowType.Header Then
            
        End If

    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            TxtEDate.Text = Now.ToShortDateString
            TxtSDate.Text = Now.ToShortDateString

        End If
    End Sub
End Class
