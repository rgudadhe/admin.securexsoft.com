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
        Dim strCategory As String = String.Empty
        Dim CurrSDate As Date
        Dim CurrEDate As Date
        Dim PrvSDate As Date
        Dim PrvEDate As Date
        Dim PrvWDays As Integer
        Dim CurrWDays As Integer
        Dim InpDate As Date
        Dim Altrow As Boolean = False
        If TxtDate.Text = String.Empty Then
            strDate = Now.ToShortDateString
        Else
            strDate = TxtDate.Text
        End If
        InpDate = Date.Parse(strDate)
        CurrSDate = Month(InpDate) & "/1/" & Year(InpDate)
        CurrEDate = InpDate
        CurrWDays = WorkingDays(CurrSDate, CurrEDate)
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
        Dim t2 As New Table
        t2.Style("width") = "100%"
        t2.BorderWidth = 2
        t2.GridLines = GridLines.Both
        strConn = System.Configuration.ConfigurationManager.AppSettings("ETSConOne")
        Dim STAvgPreMins As Integer = 0
        Dim STAvgCurrMins As Integer = 0
        Dim STDayMins1 As Integer = 0
        Dim STDayMins2 As Integer = 0
        Dim STDayMins3 As Integer = 0
        Dim STDayMins4 As Integer = 0
        Dim STDayMins5 As Integer = 0
        Dim STDayMins6 As Integer = 0
        Dim STDayMins7 As Integer = 0
        Dim I As Integer = 0
        Dim TAvgPreMins As Integer = 0
        Dim TAvgCurrMins As Integer = 0
        Dim TDayMins1 As Integer = 0
        Dim TDayMins2 As Integer = 0
        Dim TDayMins3 As Integer = 0
        Dim TDayMins4 As Integer = 0
        Dim TDayMins5 As Integer = 0
        Dim TDayMins6 As Integer = 0
        Dim TDayMins7 As Integer = 0
        Dim DS As New Data.DataSet
        Dim clsMIS As ETS.BL.MISReports
        Try
            clsMIS = New ETS.BL.MISReports
            DS = clsMIS.GetDMRPostBrkUpByParm(InpDate, CurrSDate, CurrEDate.AddDays(1), New System.Guid(Session("contractorID").ToString), Session("WorkGroupID").ToString, DLInstance.SelectedValue)
            If DS.Tables.Count > 0 Then
                If DS.Tables(0).Rows.Count > 0 Then
                    For Each DRRec1 As DataRow In DS.Tables(0).Rows
                        I = I + 1
                        If I = 1 Then
                            strCategory = DRRec1("CateDescr").ToString
                            Dim Row2 As New TableRow
                            Row2.HorizontalAlign = HorizontalAlign.Center
                            'Row2.CssClass = "tblbgbody"
                            Row2.Font.Bold = True
                            Row2.Font.Italic = True
                            Row2.ForeColor = Drawing.Color.White
                            Row2.Font.Size = "8"
                            Dim CatCell As New TableCell
                            CatCell.ColumnSpan = "10"
                            CatCell.CssClass = "HeaderDiv"
                            CatCell.HorizontalAlign = HorizontalAlign.Center
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
                                ACell1.Style.Add("text-align", "left")
                                ACell2.Text = FormatNumber(STAvgPreMins, 0).Replace(",", "")
                                ACell2.Style.Add("text-align", "right")
                                ACell3.Text = FormatNumber(STAvgCurrMins, 0).Replace(",", "")
                                ACell3.Style.Add("text-align", "right")
                                ACell4.Text = FormatNumber(STDayMins1, 0).Replace(",", "")
                                ACell4.Style.Add("text-align", "right")
                                ACell5.Text = FormatNumber(STDayMins2, 0).Replace(",", "")
                                ACell5.Style.Add("text-align", "right")
                                ACell6.Text = FormatNumber(STDayMins3, 0).Replace(",", "")
                                ACell6.Style.Add("text-align", "right")
                                ACell7.Text = FormatNumber(STDayMins4, 0).Replace(",", "")
                                ACell7.Style.Add("text-align", "right")
                                ACell8.Text = FormatNumber(STDayMins5, 0).Replace(",", "")
                                ACell8.Style.Add("text-align", "right")
                                ACell9.Text = FormatNumber(STDayMins6, 0).Replace(",", "")
                                ACell9.Style.Add("text-align", "right")
                                ACell10.Text = FormatNumber(STDayMins7, 0).Replace(",", "")
                                ACell10.Style.Add("text-align", "right")

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
                                ARow1.CssClass = "gridalt2"
                                ARow1.BackColor = Drawing.Color.Navy
                                ARow1.ForeColor = Drawing.Color.White
                                tblMins.Rows.Add(ARow1)

                                strCategory = DRRec1("CateDescr").ToString
                                Dim Row2 As New TableRow
                                Row2.HorizontalAlign = HorizontalAlign.Center
                                'Row2.CssClass = "tblbgbody"
                                Row2.Font.Bold = True
                                Row2.Font.Italic = True
                                Row2.ForeColor = Drawing.Color.White
                                Row2.Font.Size = "8"
                                Dim CatCell As New TableCell
                                CatCell.CssClass = "HeaderDiv"
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
                        If Altrow = False Then
                            Row1.CssClass = "gridalt2"
                            Altrow = True
                        Else
                            Row1.CssClass = "gridalt1"
                            Altrow = False

                        End If

                        If CHZero.Checked = False Then
                            If DRRec1(2).ToString <> "0" And DRRec1(3).ToString <> "0" Then
                                tblMins.Rows.Add(Row1)
                            End If
                        Else
                            tblMins.Rows.Add(Row1)
                        End If
                    Next
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
                    A2Row1.CssClass = "gridalt2"
                    A2Cell1.Text = "SubTotal"
                    A2Cell1.Style.Add("text-align", "left")
                    A2Row1.Font.Bold = True
                    A2Cell2.Text = FormatNumber(STAvgPreMins, 0).Replace(",", "")
                    A2Cell2.Style.Add("text-align", "right")
                    A2Cell3.Text = FormatNumber(STAvgCurrMins, 0).Replace(",", "")
                    A2Cell3.Style.Add("text-align", "right")
                    A2Cell4.Text = FormatNumber(STDayMins1, 0).Replace(",", "")
                    A2Cell4.Style.Add("text-align", "right")
                    A2Cell5.Text = FormatNumber(STDayMins2, 0).Replace(",", "")
                    A2Cell5.Style.Add("text-align", "right")
                    A2Cell6.Text = FormatNumber(STDayMins3, 0).Replace(",", "")
                    A2Cell6.Style.Add("text-align", "right")
                    A2Cell7.Text = FormatNumber(STDayMins4, 0).Replace(",", "")
                    A2Cell7.Style.Add("text-align", "right")
                    A2Cell8.Text = FormatNumber(STDayMins5, 0).Replace(",", "")
                    A2Cell8.Style.Add("text-align", "right")
                    A2Cell9.Text = FormatNumber(STDayMins6, 0).Replace(",", "")
                    A2Cell9.Style.Add("text-align", "right")
                    A2Cell10.Text = FormatNumber(STDayMins7, 0).Replace(",", "")
                    A2Cell10.Style.Add("text-align", "right")

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
                    A2Row1.BackColor = Drawing.Color.Navy
                    A2Row1.ForeColor = Drawing.Color.White
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
                    A1Row1.CssClass = "gridalt2"
                    A1Row1.Font.Bold = True

                    A1Cell1.Text = "Total"
                    A1Cell1.Style.Add("text-align", "left")
                    A1Cell2.Text = FormatNumber(TAvgPreMins, 0).Replace(",", "")
                    A1Cell2.Style.Add("text-align", "right")
                    A1Cell3.Text = FormatNumber(TAvgCurrMins, 0).Replace(",", "")
                    A1Cell3.Style.Add("text-align", "right")
                    A1Cell4.Text = FormatNumber(TDayMins1, 0).Replace(",", "")
                    A1Cell4.Style.Add("text-align", "right")
                    A1Cell5.Text = FormatNumber(TDayMins2, 0).Replace(",", "")
                    A1Cell5.Style.Add("text-align", "right")
                    A1Cell6.Text = FormatNumber(TDayMins3, 0).Replace(",", "")
                    A1Cell6.Style.Add("text-align", "right")
                    A1Cell7.Text = FormatNumber(TDayMins4, 0).Replace(",", "")
                    A1Cell7.Style.Add("text-align", "right")
                    A1Cell8.Text = FormatNumber(TDayMins5, 0).Replace(",", "")
                    A1Cell8.Style.Add("text-align", "right")
                    A1Cell9.Text = FormatNumber(TDayMins6, 0).Replace(",", "")
                    A1Cell9.Style.Add("text-align", "right")
                    A1Cell10.Text = FormatNumber(TDayMins7, 0).Replace(",", "")
                    A1Cell10.Style.Add("text-align", "right")
                    'A1Row1.BackColor = Drawing.Color.Navy
                    'A1Row1.ForeColor = Drawing.Color.White
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
                    A1Row1.BackColor = Drawing.Color.Navy
                    A1Row1.ForeColor = Drawing.Color.White
                    tblMins.Rows.AddAt(3, A1Row1)
                End If
            End If

        Catch ex As Exception
            Response.Write(ex.Message)
        Finally
            clsMIS = Nothing
            DS.Dispose()
        End Try

        tblMins.Style.Add("CellPadding", "2")
    End Sub



    Protected Sub ShowDictDetails(ByVal ActID As String, ByVal inpDate As Date)
        R1Cell1.Text = "Dictator"
        tblDtls.Text = "Dictator Details"
        Dim strConn As String
        Dim strCategory As String = String.Empty
        Dim t2 As New Table
        t2.Style("width") = "100%"
        t2.BorderWidth = 2
        t2.GridLines = GridLines.Both
        Dim I As Integer = 0
        strConn = System.Configuration.ConfigurationManager.AppSettings("ETSConOne")
        Dim CurrSDate As Date
        Dim CurrEDate As Date
        Dim PrvWDays As Integer
        Dim CurrWDays As Integer
        CurrSDate = Month(inpDate) & "/1/" & Year(inpDate)
        CurrEDate = inpDate
        CurrWDays = WorkingDays(CurrSDate, CurrEDate)
        Dim PrvSDate As Date
        Dim PrvEDate As Date
        PrvSDate = DateAdd(DateInterval.Month, -1, CurrSDate)
        PrvEDate = DateAdd(DateInterval.Day, -1, CurrSDate)
        PrvWDays = WorkingDays(PrvSDate, PrvEDate)
        Dim TAvgPreMins As Integer = 0
        Dim TAvgCurrMins As Integer = 0
        Dim TDayMins1 As Integer = 0
        Dim TDayMins2 As Integer = 0
        Dim TDayMins3 As Integer = 0
        Dim TDayMins4 As Integer = 0
        Dim TDayMins5 As Integer = 0
        Dim TDayMins6 As Integer = 0
        Dim TDayMins7 As Integer = 0
        Dim DS As New Data.DataSet
        Dim clsMIS As ETS.BL.MISReports
        Dim altrow As Boolean = False
        Try
            clsMIS = New ETS.BL.MISReports
            DS = clsMIS.GetDMRPostDictByParm(inpDate, New System.Guid(ActID), CurrSDate, CurrEDate.AddDays(1), DLInstance.SelectedValue)
            If DS.Tables.Count > 0 Then
                If DS.Tables(0).Rows.Count > 0 Then
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
                    For Each DRRec1 As DataRow In DS.Tables(0).Rows
                        TAvgPreMins += DRRec1(2).ToString
                        TAvgCurrMins += DRRec1(3).ToString
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
                        Cell1.Text = DRRec1(1).ToString
                        Cell1.HorizontalAlign = HorizontalAlign.Left
                        Cell2.CssClass = "tblbg3"
                        Cell3.CssClass = "tblbg4"
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
                        Row1.CssClass = "algrid2"
                        Row1.Cells.Add(Cell1)
                        Cell2.Style.Add("text-align", "left")
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
                        If altrow = False Then
                            Row1.CssClass = "gridalt2"
                            altrow = True
                        Else
                            Row1.CssClass = "gridalt1"
                            altrow = False

                        End If
                        tblMins.Rows.Add(Row1)
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
                    A1Row1.HorizontalAlign = HorizontalAlign.Center
                    A1Row1.CssClass = "gridalt2"
                    A1Row1.Font.Bold = True

                    A1Cell1.Text = "Total"
                    A1Cell1.Style.Add("text-align", "left")
                    A1Cell2.Text = FormatNumber((TAvgPreMins / 60) / PrvWDays, 0).Replace(",", "")
                    A1Cell2.Style.Add("text-align", "right")
                    A1Cell3.Text = FormatNumber((TAvgCurrMins / 60) / CurrWDays, 0).Replace(",", "")
                    A1Cell3.Style.Add("text-align", "right")
                    A1Cell4.Text = FormatNumber(TDayMins1 / 60, 0).Replace(",", "")
                    A1Cell4.Style.Add("text-align", "right")
                    A1Cell5.Text = FormatNumber(TDayMins2 / 60, 0).Replace(",", "")
                    A1Cell5.Style.Add("text-align", "right")
                    A1Cell6.Text = FormatNumber(TDayMins3 / 60, 0).Replace(",", "")
                    A1Cell6.Style.Add("text-align", "right")
                    A1Cell7.Text = FormatNumber(TDayMins4 / 60, 0).Replace(",", "")
                    A1Cell7.Style.Add("text-align", "right")
                    A1Cell8.Text = FormatNumber(TDayMins5 / 60, 0).Replace(",", "")
                    A1Cell8.Style.Add("text-align", "right")
                    A1Cell9.Text = FormatNumber(TDayMins6 / 60, 0).Replace(",", "")
                    A1Cell9.Style.Add("text-align", "right")
                    A1Cell10.Text = FormatNumber(TDayMins7 / 60, 0).Replace(",", "")
                    A1Cell10.Style.Add("text-align", "right")

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
                    A1Row1.BackColor = Drawing.Color.Navy
                    A1Row1.ForeColor = Drawing.Color.White
                    tblMins.Rows.AddAt(3, A1Row1)
                End If
            End If
        Catch ex As Exception
            Response.Write(ex.Message)
        Finally
            clsMIS = Nothing
            DS.Dispose()
        End Try

    End Sub
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            TxtDate.Text = Now.ToShortDateString
            'CalendarExtender1.SelectedDate = TxtDate.Text
            Dim strconn As String
            'strconn = System.Configuration.ConfigurationManager.AppSettings("ETSConOne")
            'Dim SQLCmd As New SqlCommand("Select * from DBO.tblaccounts where (isdeleted is null or isdeleted = 'False')  and MISRep = 'True' and contractorid = '" & Session("contractorid").ToString & "' order by description", New SqlConnection(strconn))
            ''Response.Write("Select * from DBO.tblaccounts where (isdeleted is null or isdeleted = 'False')  and MISRep = 'True' and contractorid = '" & Session("contractorid").ToString & "' order by description")
            ''Response.Write(strconn)
            'Try
            '    SQLCmd.Connection.Open()
            '    Dim DRRec As SqlDataReader = SQLCmd.ExecuteReader()
            '    If DRRec.HasRows = True Then
            '        While (DRRec.Read)
            '            'Response.Write("description: " & DRRec("Description").ToString)
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
                Dim InpDate As Date
                'If TxtDate.Text = "" Then
                InpDate = Date.Parse(Request("InpDate"))
                TxtDate.Text = Request("InpDate")
                'Else
                '    InpDate = Date.Parse(TxtDate.Text)
                'End If
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
        If Request("showDict") = "Yes" Then
            Dim AccountID As String
            Dim InpDate As Date
            'If TxtDate.Text = "" Then
            InpDate = Date.Parse(Request("InpDate"))
            TxtDate.Text = Request("InpDate")
            'Else
            '    InpDate = Date.Parse(TxtDate.Text)
            'End If
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
        Else
            ShowActDetails()
        End If

        tblMins.RenderControl(htw)
        Response.Write(sw.ToString())
        Response.[End]()

    End Sub
End Class
