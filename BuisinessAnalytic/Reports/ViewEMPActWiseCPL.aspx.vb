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
        Dim EMPCost As Long

        If Month(Now) = DLMonth.SelectedValue And Year(Now) = DLYear.SelectedValue Then
            EndDate = Now.ToShortDateString
            startdate = Month(EndDate) & "/01/" & Year(EndDate)
            DayDiff = DateDiff(DateInterval.Day, startdate, EndDate)
        Else
            startdate = DLMonth.SelectedValue & "/1/" & DLYear.SelectedValue
            EndDate = startdate.AddMonths(1).AddDays(-1)
            DayDiff = DateDiff(DateInterval.Day, startdate, EndDate)
        End If
        'Response.Write(DLAct.SelectedValue & "#" & EndDate & DayDiff)
        Dim DTSet2 As System.Data.DataSet = objrep.GetEMPActWiseCPL(DayDiff, EndDate, New Guid(DLUser.SelectedValue))
        If DTSet2.Tables.Count > 0 Then
            If DTSet2.Tables(0).Rows.Count > 0 Then
                For Each DRRec As Data.DataRow In DTSet2.Tables(0).Rows
                    Dim ARow1 As New TableRow
                    Dim ACell1 As New TableCell
                    Dim ACell2 As New TableCell
                    Dim ACell3 As New TableCell
                    Dim ACell4 As New TableCell

                    ARow1.HorizontalAlign = HorizontalAlign.Right
                    ARow1.Font.Size = "8"
                    ACell1.HorizontalAlign = HorizontalAlign.Left
                    ACell2.HorizontalAlign = HorizontalAlign.Right
                    ACell3.HorizontalAlign = HorizontalAlign.Right
                    ACell4.HorizontalAlign = HorizontalAlign.Right
                    ACell1.CssClass = "alt6"
                    Lines = DRRec("Lines")
                    EMPCost = DRRec("EMPCost")
                    ACell1.Text = DRRec("Accountname").ToString
                    'ACell2.Text = DRRec("username").ToString
                    ACell3.Text = FormatNumber(Lines, 0)
                    ACell2.Text = FormatNumber(EMPCost, 2)
                    ACell4.Text = FormatNumber(EMPCost / Lines, 2)
                    ARow1.Cells.Add(ACell1)
                    ARow1.Cells.Add(ACell2)
                    ARow1.Cells.Add(ACell3)
                    ARow1.Cells.Add(ACell4)

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
            GetMyMonthList(DLMonth, True)
            GetMyYearList(DLYear, True)
            Dim objUser As New ETS.BL.Users
            Dim DTSet1 As System.Data.DataSet = objUser.getUsersList(Session("contractorID").ToString)
            'DTSet1.WriteXml("\\onlinemtr\d$\users.xml")

            If DTSet1.Tables.Count > 0 Then

                Dim dv As DataView
                dv = New DataView
                With dv
                    .Table = DTSet1.Tables(0)
                    .AllowDelete = True
                    .AllowEdit = True
                    .AllowNew = True
                    .RowFilter = "prodmode='Employee'"
                    .Sort = "FirstName Asc"
                End With

                'Dim DTView As New DataView(DTSet1.Tables(0), "Indirect='True'", "AccountName Asc", DataViewRowState.ModifiedCurrent)
                DLUser.DataSource = dv
                DLUser.DataTextField = "UName"
                DLUser.DataValueField = "UserID"
                DLUser.DataBind()
            End If
            objUser = Nothing
        End If

    End Sub
End Class
