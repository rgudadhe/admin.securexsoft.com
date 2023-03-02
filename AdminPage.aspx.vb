Imports System.Data.Sqlclient
Imports System.Drawing
Imports System.Data
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports System.Web.UI.WebControls.WebParts
Imports System.Web.UI.HtmlControls
Imports System.IO
Imports WebChart

Partial Class AdminPage
    Inherits BasePage

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        GetAlerts()
        GetSystemAlerts()
        GetTipOftheWeek()
        GetEnhancement()
        ShowActDetails()
    End Sub
    Protected Sub GetAlerts()

        Try

        lblAlerts.Text = "<table  width='100%'>"
            Dim DSMapAct As DataSet
            Dim clsAct As New ETS.BL.Accounts
            'With clsAct
            '    .ContractorID = Session("ContractorID").ToString
            '    ._WhereString.Append("  and (IsDeleted is null or IsDeleted=0)")
            '    DSMapAct = .getAccountList()
            'End With
            With clsAct
                '.ContractorID = Session("ContractorID").ToString

                '._WhereString.Append("  and (IsDeleted is null or IsDeleted=0)")
                DSMapAct = .getAccountList(session("WorkGroupID").tostring, session("ContractorID").tostring, String.Empty)
            End With
            Dim DT As DataTable = DSMapAct.Tables(0).Clone
            lblAlerts.Text = lblAlerts.Text & "<TR> <TD class='HeaderDiv' style='text-align:left;'><img src='images/flag.gif'><b>Total Active Accounts </b></TD><TD><a href='/Account/EdActDetails.aspx'> " & DSMapAct.Tables(0).Rows.Count & "</a></TD></TR>"
            clsAct = Nothing
            Dim clsPhy As New ETS.BL.Physicians
            Dim DSPhy As DataSet = clsPhy.getPhysiciansList(Session("ContractorID"), Session("WorkgroupID"), Nothing)
            lblAlerts.Text = lblAlerts.Text & "<TR> <TD class='HeaderDiv' style='text-align:left;'><img src='images/flag.gif'><b>Total Active Providers </b></TD><TD><a href='/Physicians/PhysicianList.aspx'> " & DSPhy.Tables(0).Rows.Count & "</a></TD></TR>"
            clsPhy = Nothing

            lblAlerts.Text = lblAlerts.Text & "</table>"
        Catch ex As Exception
            response.write(ex.message)
        End Try

    End Sub
    Protected Sub GetSystemAlerts()
        Lblupdate.Text = String.Empty
        Dim clsHome3 As ETS.BL.HomePage
        Dim DSUpdates As New Data.DataSet
        Dim DRRec3 As Data.DataTableReader
        Dim updatedate As Date
        Try
            clsHome3 = New ETS.BL.HomePage
            DSUpdates = clsHome3.GetSystemAlertsListByContractorID(Session("ContractorID").ToString)
            If DSUpdates.Tables.Count > 0 Then
                If DSUpdates.Tables(0).Rows.Count > 0 Then
                    DRRec3 = DSUpdates.Tables(0).CreateDataReader
                    If DRRec3.HasRows = True Then
                        Lblupdate.Text = "<table  width='100%'>"
                        While (DRRec3.Read)
                            updatedate = DRRec3("DateUpdated").ToString
                            Lblupdate.Text = Lblupdate.Text & "<TR> <TD class='HeaderDiv' style='text-align:left;'><img src='images/flag.gif'><b><font color='Red'>" & updatedate.ToString("MM-dd-yyyy") & "</font></b></TD></TR><TR><TD>" & DRRec3("Alert").ToString & "</TD></TR>"
                        End While
                        Lblupdate.Text = Lblupdate.Text & "</table>"
                    End If
                    DRRec3.Close()
                End If
            End If

        Catch ex As Exception
            Response.Write(ex.Message)
        Finally
            clsHome3 = Nothing
            DSUpdates = Nothing
            DRRec3 = Nothing
        End Try
    End Sub
    Protected Sub GetTipOftheWeek()
        lblTip.Text = String.Empty
        Dim clsHome3 As ETS.BL.HomePage
        Dim DSUpdates As New Data.DataSet
        Dim DRRec3 As Data.DataTableReader
        Try
            clsHome3 = New ETS.BL.HomePage
            DSUpdates = clsHome3.GetTipOftheWeekListByContractorID(Session("ContractorID").ToString)
            If DSUpdates.Tables.Count > 0 Then

                If DSUpdates.Tables(0).Rows.Count > 0 Then

                    DRRec3 = DSUpdates.Tables(0).CreateDataReader
                    If DRRec3.HasRows = True Then
                        lblTip.Text = "<table  width='100%'>"
                        While (DRRec3.Read)
                            'Response.Write("<img src='images/flag.gif'><b><font color='Red'>" & DRRec3("Tip").ToString & "</font></b><br />" & DRRec3("Description").ToString & "<br />")
                            lblTip.Text = lblTip.Text & "<TR> <TD class='HeaderDiv' style='text-align:left;'> <img src='images/flag.gif'><b><font color='Red'>" & DRRec3("Tip").ToString & "</font></b></TD></TR><TR><TD>" & DRRec3("Description").ToString & "</TD></TR>"
                        End While
                        lblTip.Text = lblTip.Text & "</table>"
                    End If
                    DRRec3.Close()
                End If
            End If

        Catch ex As Exception
            Response.Write(ex.Message)
        Finally
            clsHome3 = Nothing
            DSUpdates = Nothing
            DRRec3 = Nothing
        End Try
    End Sub
    Protected Sub GetEnhancement()
        LblEnhancement.Text = String.Empty
        Dim clsHome3 As ETS.BL.HomePage
        Dim DSUpdates As New Data.DataSet
        Dim DRRec3 As Data.DataTableReader
        Dim ReleaseDate As Date
        Try
            clsHome3 = New ETS.BL.HomePage
            DSUpdates = clsHome3.GetEnhancementListByContractorID(Session("ContractorID").ToString)
            LblEnhancement.Text = "<table  width='100%'> <tr class='tblbg2'><td class='HeaderDiv'> Release Date</td><td class='HeaderDiv'>Enhancement</td><td class='HeaderDiv'>Description</td></tr>"
            If DSUpdates.Tables.Count > 0 Then
                If DSUpdates.Tables(0).Rows.Count > 0 Then
                    DRRec3 = DSUpdates.Tables(0).CreateDataReader
                    If DRRec3.HasRows = True Then
                        While (DRRec3.Read)
                            ReleaseDate = DRRec3("ReleaseDate").ToString
                            LblEnhancement.Text = LblEnhancement.Text & "<TR ><TD><font color='Red'><b>" & ReleaseDate.ToString("MM-dd-yyyy") & "</font></b></TD><TD>" & DRRec3("Enhancement").ToString & "</TD><TD>" & DRRec3("Description").ToString & "</TD></TR>"
                        End While

                    End If
                    DRRec3.Close()
                End If
            End If
            LblEnhancement.Text = LblEnhancement.Text & "</TD>"
        Catch ex As Exception
            Response.Write(ex.Message)
        Finally
            clsHome3 = Nothing
            DSUpdates = Nothing
            DRRec3 = Nothing
        End Try
    End Sub
    Protected Sub ShowActDetails()
        Dim objrep As New ETS.BL.BusinessAnalytics
        Dim startdate1 As Date
        Dim EndDate2 As Date
        Dim startdate2 As Date
        Dim startdate1_1 As Date
        Dim EndDate2_1 As Date
        Dim startdate2_1 As Date
        Dim DayDiff As Integer
        Dim MonthDiff As Integer
        Dim DayDiff_1 As Integer
        Dim MonthDiff_1 As Integer
        Dim CompYear As String = String.Empty
        Dim CompMonth As String = String.Empty
        startdate1 = 1 & "/1/" & Now.Year
        startdate2 = Now.Month & "/1/" & Now.Year
        EndDate2 = startdate2.AddMonths(1).AddDays(-1)
        DayDiff = DateDiff(DateInterval.Day, startdate1, EndDate2)
        MonthDiff = DateDiff(DateInterval.Month, startdate1, EndDate2)
        Dim ParmValues As String = String.Empty
        ParmValues = "[" & startdate1.ToString("MM/dd/yyyy") & "]"
        For I As Integer = 1 To MonthDiff

            ParmValues = ParmValues & ",[" & startdate1.AddMonths(I).ToString("MM/dd/yyyy") & "]"
        Next

        startdate1_1 = 1 & "/1/" & Now.AddYears(-1).Year
        startdate2_1 = "12/1/" & Now.AddYears(-1).Year
        EndDate2_1 = startdate2_1.AddMonths(1).AddDays(-1)
        DayDiff_1 = DateDiff(DateInterval.Day, startdate1_1, EndDate2_1)
        MonthDiff_1 = DateDiff(DateInterval.Month, startdate1_1, EndDate2_1)
        Dim ParmValues_1 As String = String.Empty
        ParmValues_1 = "[" & startdate1_1.ToString("MM/dd/yyyy") & "]"
        For I As Integer = 1 To MonthDiff_1

            ParmValues_1 = ParmValues_1 & ",[" & startdate1_1.AddMonths(I).ToString("MM/dd/yyyy") & "]"
        Next

        Dim CompDate As Date
        'If CheckBox1.Checked = True Then
        '    CompDate = DLMonth.SelectedValue & "/1/" & DLYear.SelectedValue
        '    ParmValues = ParmValues & ",[CompMonth]"
        '    CompMonth = DLMonth.SelectedValue
        '    CompYear = DLYear.SelectedValue
        'End If
        'Response.Write(ParmValues)
        Dim DTSet2 As System.Data.DataSet = objrep.GetMonthlyChart(New Guid(Session("ContractorId").ToString), DayDiff, EndDate2, Nothing, ParmValues, False, CompMonth, CompYear, DLTrend.SelectedValue, Session("WorkGroupID").ToString)
        Dim DTSet3 As System.Data.DataSet = objrep.GetMonthlyChart(New Guid(Session("ContractorId").ToString), DayDiff_1, EndDate2_1, Nothing, ParmValues_1, False, CompMonth, CompYear, DLTrend.SelectedValue, Session("WorkGroupID").ToString)

        'Response.Write(ParmValues & "#" & EndDate2 & "#" & DayDiff & "#" & Session("ContractorId").ToString)

        If DTSet2.Tables.Count > 0 Then
            'Dim ds As DataSet = GetDataSet()
            Dim dt As Date
            For Each dc As DataRow In DTSet2.Tables(0).Rows
                If IsDate(dc(0)) Then
                    dt = dc(0)
                    dc(0) = dt.ToString("MMM")
                    'Response.Write(dc(0))
                End If

            Next
            Dim view As DataView = DTSet2.Tables(0).DefaultView
            ChartControl1.ChartTitle.Text = "Monthly Chart"
            ChartControl1.ChartTitle.ForeColor = Color.White
            Dim chart As New WebChart.ColumnChart
            chart.Fill.Color = Color.CadetBlue
            chart.MaxColumnWidth = 24
            chart.Line.Color = Color.SteelBlue
            chart.Legend = Now.Year & "(K)"
            chart.DataSource = view
            chart.DataXValueField = "ProcMonth"
            chart.DataYValueField = DLTrend.SelectedValue
            chart.DataBind()
            chart.ShowLineMarkers = True
            chart.ShowLegend = True
            chart.Shadow.Color = Color.FromArgb(70, Color.DarkGray)
            chart.Shadow.Visible = True
            ChartControl1.Charts.Add(chart)
            ChartControl1.ControlStyle.Height = 180
            ChartControl1.RedrawChart()


        End If
        If DTSet3.Tables.Count > 0 Then
            'Dim ds As DataSet = GetDataSet()
            Dim dt As Date
            For Each dc As DataRow In DTSet3.Tables(0).Rows
                If IsDate(dc(0)) Then
                    dt = dc(0)
                    dc(0) = dt.ToString("MMM")
                    'Response.Write(dc.ColumnName)
                End If

            Next
            Dim view As DataView = DTSet3.Tables(0).DefaultView
            ChartControl1.ChartTitle.Text = "Monthly Chart"
            ChartControl1.ChartTitle.ForeColor = Color.White
            Dim chart As New WebChart.ColumnChart
            chart.Fill.Color = Color.Red
            chart.MaxColumnWidth = 24
            chart.Line.Color = Color.SteelBlue
            chart.Legend = Now.AddYears(-1).Year & "(K)"
            chart.DataSource = view
            chart.DataXValueField = "ProcMonth"
            chart.DataYValueField = DLTrend.SelectedValue

            chart.DataBind()
            chart.ShowLineMarkers = True

            chart.ShowLegend = True
            chart.Shadow.Color = Color.FromArgb(70, Color.DarkGray)
            chart.Shadow.Visible = True
            ChartControl1.Charts.Add(chart)
            ChartControl1.ControlStyle.Height = 230
            ChartControl1.XAxisFont.ForeColor = Color.Blue
            ChartControl1.YAxisFont.ForeColor = Color.CadetBlue
            ChartControl1.ShowYValues = True
            ChartControl1.ShowXValues = True
            ChartControl1.RedrawChart()

        End If
        ' End If



    End Sub
End Class
