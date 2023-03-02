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
        Dim HBACost As Long
        
        If Month(Now) = DLMonth.SelectedValue And Year(Now) = DLYear.SelectedValue Then
            EndDate = Now.ToShortDateString
            startdate = Month(EndDate) & "/01/" & Year(EndDate)
            DayDiff = DateDiff(DateInterval.Day, startdate, EndDate)
        Else
            startdate = DLMonth.SelectedValue & "/1/" & DLYear.SelectedValue
            EndDate = startdate.AddMonths(1).AddDays(-1)
            DayDiff = DateDiff(DateInterval.Day, startdate, EndDate)
        End If
        Dim ExchRate As Long = 45.5

        'Response.Write(DLAct.SelectedValue & "#" & EndDate & DayDiff)
        Dim DTSet2 As System.Data.DataSet = objrep.GetEMPPayrollReport(New Guid(Session("contractorID").ToString), DayDiff, EndDate, ExchRate)
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

                   


                    ARow1.HorizontalAlign = HorizontalAlign.Right
                    'ARow1.Font.Bold = True
                    ARow1.Font.Size = "8"
                    ACell1.HorizontalAlign = HorizontalAlign.Left
                    ACell2.HorizontalAlign = HorizontalAlign.Right
                    ACell3.HorizontalAlign = HorizontalAlign.Right
                    ACell4.HorizontalAlign = HorizontalAlign.Right
                    'Lines = DRRec("Lines")
                    'HBACost = DRRec("HBACost")
                    'cost = DRRec("TotalCost")
                    'RPL = FormatNumber(Revenue / Lines, 2)
                    'CPL = FormatNumber(cost / Lines, 2)
                    ACell1.Text = DRRec("uname").ToString
                    ACell2.Text = DRRec("username").ToString
                    ACell3.Text = DRRec("MTLines").ToString
                    ACell4.Text = DRRec("MTPlusLines").ToString
                    ACell5.Text = DRRec("QALines").ToString
                    ACell6.Text = DRRec("QABSELines").ToString
                    ACell7.Text = DRRec("MTBLines").ToString
                    ACell8.Text = DRRec("QABLines").ToString
                    ACell9.Text = DRRec("DailyPoints").ToString
                    ACell10.Text = DRRec("Target").ToString
                    ACell11.Text = DRRec("Salary").ToString



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
            
        End If

    End Sub
End Class
