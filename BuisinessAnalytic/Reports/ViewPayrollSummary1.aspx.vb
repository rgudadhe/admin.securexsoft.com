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
        Response.Write(EndDate & DayDiff)
        Dim DTSet2 As System.Data.DataSet = objrep.GetEMPWiseCPL(DayDiff, EndDate, New Guid(Session("contractorID").ToString))
        If DTSet2.Tables.Count > 0 Then
            If DTSet2.Tables(0).Rows.Count > 0 Then
                For Each DRRec As Data.DataRow In DTSet2.Tables(0).Rows
                    Dim ARow1 As New TableRow
                    Dim ACell1 As New TableCell
                    Dim ACell2 As New TableCell
                    Dim ACell3 As New TableCell
                    Dim ACell4 As New TableCell
                    Dim ACell5 As New TableCell
                    ARow1.HorizontalAlign = HorizontalAlign.Right
                    ARow1.Font.Size = "8"
                    ACell1.HorizontalAlign = HorizontalAlign.Left
                    ACell2.HorizontalAlign = HorizontalAlign.Right
                    ACell3.HorizontalAlign = HorizontalAlign.Right
                    ACell4.HorizontalAlign = HorizontalAlign.Right
                    Lines = DRRec("Lines")
                    HBACost = DRRec("EMPCost")
                    ACell1.Text = DRRec("uname").ToString
                    ACell2.Text = DRRec("username").ToString
                    ACell3.Text = FormatNumber(Lines, 0)
                    ACell4.Text = FormatNumber(HBACost, 2)
                    ACell5.Text = FormatNumber(HBACost / Lines, 2)
                    ACell1.CssClass = "alt6"
                    ARow1.Cells.Add(ACell1)
                    ARow1.Cells.Add(ACell2)
                    ARow1.Cells.Add(ACell3)
                    ARow1.Cells.Add(ACell4)
                    ARow1.Cells.Add(ACell5)
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
