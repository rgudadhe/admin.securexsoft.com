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
        'Response.Write(startdate & "#" & EndDate & "#" & DayDiff & "#" & Session("ContractorId").ToString)
        Dim DTSet2 As System.Data.DataSet = objrep.GetOverAllCPL(DayDiff, EndDate, New Guid(Session("ContractorId").ToString))
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
                    ARow1.HorizontalAlign = HorizontalAlign.Right
                    'ARow1.Font.Bold = True
                    'Response.Write(DRRec("AccountID").ToString)
                    ARow1.Font.Size = "8"
                    Lines = DRRec("Lines")
                    Revenue = DRRec("Revenue")
                    cost = DRRec("TotalCost")
                    RPL = FormatNumber(Revenue / Lines, 2)
                    CPL = FormatNumber(cost / Lines, 2)
                    If DRRec("AccountID").ToString = "11111111-1111-1111-1111-111111111111" Then
                        ACell1.Text = "<a href='ViewAccountWiseCPL.aspx?Month=" & DLMonth.SelectedValue & "&Year=" & DLYear.SelectedValue & "'>" & DRRec("AccountName").ToString & "</a>"
                    Else
                        ACell1.Text = "<a href='ViewOtherPlatformCPL.aspx?Month=" & DLMonth.SelectedValue & "&Year=" & DLYear.SelectedValue & "&AccountID=" & DRRec("AccountID").ToString & "'>" & DRRec("AccountName").ToString & "</a>"
                    End If

                    ACell2.Text = FormatNumber(Lines, 0)
                    ACell3.Text = FormatNumber(Revenue, 2)
                    ACell4.Text = FormatNumber(cost, 2)
                    ACell5.Text = FormatNumber(Revenue / Lines, 2)
                    ACell6.Text = FormatNumber(cost / Lines, 2)
                    ACell7.Text = FormatNumber(((Revenue - cost) / (Revenue)) * 100, 0) & " %"
                    ACell1.CssClass = "alt6"
                    ARow1.Cells.Add(ACell1)
                    ARow1.Cells.Add(ACell2)
                    ARow1.Cells.Add(ACell3)
                    ARow1.Cells.Add(ACell4)
                    ARow1.Cells.Add(ACell5)
                    ARow1.Cells.Add(ACell6)
                    ARow1.Cells.Add(ACell7)
                    tblMins.Rows.Add(ARow1)
                Next
            End If
        End If
    End Sub

 

   

   
    Protected Sub btnsubmit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnsubmit.Click
        ShowActDetails()
    End Sub
End Class
