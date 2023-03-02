Imports System.Data.Sqlclient
Imports System.Data
Partial Class MIS_DailyMins
    Inherits BasePage
    Protected Sub ShowActDetails()
        Dim objrep As New ETS.BL.BusinessAnalytics
        Dim EndDate As Date
        Dim startdate As Date
        Dim DayDiff As Integer
        Dim Lines As Long
        Dim Revenue As Long
        Dim cost As Long
        Dim RPL As Long
        Dim CPL As Long
        If Month(Now) = DLMonth.SelectedValue And Year(Now) = DLYear.SelectedValue Then
            EndDate = Now.ToShortDateString
            startdate = Month(EndDate) & "/01/" & Year(EndDate)
            DayDiff = DateDiff(DateInterval.Day, startdate, EndDate)
        Else
            startdate = DLMonth.SelectedValue & "/1/" & DLYear.SelectedValue
            EndDate = startdate.AddMonths(1).AddDays(-1)
            DayDiff = DateDiff(DateInterval.Day, startdate, EndDate)
        End If
        'Response.Write(startdate & "#" & EndDate & DayDiff)
        Dim DTSet2 As System.Data.DataSet = objrep.GetSSFAccountWiseCPL(DayDiff, EndDate, New Guid(Session("ContractorId").ToString))
        If DTSet2.Tables.Count > 0 Then
            If DTSet2.Tables(0).Rows.Count > 0 Then
                For Each DRRec As Data.DataRow In DTSet2.Tables(0).Rows
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
                    Dim ACell11 As New TableCell
                    Dim ACell12 As New TableCell


                    'ARow1.HorizontalAlign = HorizontalAlign.Right
                    'ARow1.Font.Bold = True
                    'ARow1.Font.Size = "8"
                    ACell1.HorizontalAlign = HorizontalAlign.Left
                    ACell2.HorizontalAlign = HorizontalAlign.Right
                    ACell3.HorizontalAlign = HorizontalAlign.Right
                    ACell4.HorizontalAlign = HorizontalAlign.Right
                    ACell5.HorizontalAlign = HorizontalAlign.Right
                    ACell6.HorizontalAlign = HorizontalAlign.Right
                    ACell7.HorizontalAlign = HorizontalAlign.Right
                    ACell8.HorizontalAlign = HorizontalAlign.Right
                    ACell9.HorizontalAlign = HorizontalAlign.Right
                    ACell10.HorizontalAlign = HorizontalAlign.Right
                    ACell11.HorizontalAlign = HorizontalAlign.Right
                    ACell12.HorizontalAlign = HorizontalAlign.Right
                    Lines = DRRec("Lines")
                    Revenue = DRRec("Revenue")
                    'cost = DRRec("TotalCost")
                    'RPL = FormatNumber(Revenue / Lines, 2)
                    'CPL = FormatNumber(cost / Lines, 2)

                    ACell1.Text = DRRec("AccountName").ToString
                    ACell2.Text = FormatNumber(Revenue, 2)
                    ACell3.Text = FormatNumber(Lines, 0)
                    ACell4.Text = FormatNumber(Revenue / Lines, 2)
                    ACell5.Text = FormatNumber(DRRec("EmpLines"), 0)
                    ACell6.Text = FormatNumber(DRRec("HBALines"), 0)
                    ACell7.Text = FormatNumber(DRRec("EMPCost"), 2)
                    ACell8.Text = FormatNumber(DRRec("HBACost"), 2)
                    ACell9.Text = FormatNumber(0, 2)
                    ACell10.Text = DRRec("EMPCost") + DRRec("HBACost")
                    ACell11.Text = FormatNumber(DRRec("Revenue") - (DRRec("EMPCost") + DRRec("HBACost")), 2)
                    ACell12.Text = FormatNumber(((DRRec("Revenue") - (DRRec("EMPCost") + DRRec("HBACost"))) / DRRec("Revenue")) * 100, 0)

                    ACell1.CssClass = "alt6"

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
                    ARow1.Cells.Add(ACell11)
                    ARow1.Cells.Add(ACell12)

                    tblMins.Rows.Add(ARow1)
                Next
            End If
        End If
    End Sub

 

   

   
    Protected Sub btnsubmit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnsubmit.Click
        ShowActDetails()
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            If Request("month") <> "" And Request("Year") <> "" Then
                For i As Integer = 0 To DLMonth.Items.Count - 1
                    If DLMonth.Items(i).Value = Request("Month") Then
                        DLMonth.Items(i).Selected = True
                    End If
                Next
                For i As Integer = 0 To DLYear.Items.Count - 1
                    If DLYear.Items(i).Value = Request("Year") Then
                        DLYear.Items(i).Selected = True
                    End If
                Next
                ShowActDetails()
            End If
        End If
    End Sub
End Class
