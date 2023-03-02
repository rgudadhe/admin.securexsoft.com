Imports MainModule
Partial Class ImportLeaveBalanceLog
    Inherits BasePage
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            tblResult.Visible = False
            Dim i As Integer
            Dim j As Integer
            Dim varYear As Double
            Dim varListItem As New ListItem
            varListItem.Text = "Please Select"
            varListItem.Value = ""
            For i = 1 To 12
                DropDownMonth.Items.Add(MonthName(i))
                DropDownMonth.Items.FindByText(MonthName(i)).Value = i
            Next
            varYear = Year(Now())
            For j = varYear - 3 To varYear + 3
                DropDownYear.Items.Add(j)
                DropDownYear.Items.FindByText(j).Value = j
            Next
            DropDownMonth.Items.Insert(0, varListItem)
            DropDownYear.Items.Insert(0, varListItem)
        End If
    End Sub
    Protected Sub btnSubmit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSubmit.Click
        Dim varMonth As Integer
        Dim varYear As Integer
        Dim varStrQuery As String

        varMonth = Request.Form("DropDownMonth")
        varYear = Request.Form("DropDownYear")

        Dim clsLB As ETS.BL.LeaveBalance
        Dim objDS As New Data.DataSet
        Dim oRecLBLog As Data.DataTableReader
        Try
            clsLB = New ETS.BL.LeaveBalance
            objDS = clsLB.GetLeaveBalanceImportLog(Session("ContractorID").ToString, varMonth, varYear)
            If objDS.Tables.Count > 0 Then
                If objDS.Tables(0).Rows.Count > 0 Then
                    oRecLBLog = objDS.Tables(0).CreateDataReader

                    If oRecLBLog.HasRows Then
                        While oRecLBLog.Read
                            Dim varTblCellUserName As New TableCell
                            Dim varTblCellUpdateOn As New TableCell
                            Dim varTblCellFileName As New TableCell
                            Dim varTblCellDetails As New TableCell
                            Dim varTblRow As New TableRow
                            varTblCellUserName.Text = oRecLBLog.GetString(oRecLBLog.GetOrdinal("FirstName")) & " " & oRecLBLog.GetString(oRecLBLog.GetOrdinal("LastName"))
                            varTblCellUpdateOn.Text = oRecLBLog.GetDateTime(oRecLBLog.GetOrdinal("UpdatedDate")).ToString
                            varTblCellFileName.Text = oRecLBLog.GetString(oRecLBLog.GetOrdinal("FileName"))
                            varTblCellDetails.Text = "<a href=ImportLogDetails.aspx?TrackID=" & oRecLBLog.GetGuid(oRecLBLog.GetOrdinal("TrackID")).ToString & ">Details</a>"

                            varTblRow.Cells.Add(varTblCellUserName)
                            varTblRow.Cells.Add(varTblCellUpdateOn)
                            varTblRow.Cells.Add(varTblCellFileName)
                            varTblRow.Cells.Add(varTblCellDetails)

                            tblResult.Rows.Add(varTblRow)
                            tblResult.Visible = True
                        End While
                        oRecLBLog.Close()


                        Label3.Visible = False
                    Else
                        'Response.Write("<center><font face=""Trebuchet MS"" size=""2"" color=""#000080"">Log not available.</font></center><BR>")
                        Label3.Visible = True
                    End If
                End If
            End If
        Catch ex As Exception
            Response.Write(ex.Message)
        Finally
            clsLB = Nothing
            objDS.Dispose()
            oRecLBLog = Nothing
        End Try
    End Sub
End Class
