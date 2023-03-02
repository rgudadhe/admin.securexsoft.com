
Partial Class LeaveAttendanceMainNew_Employee_EmployeeDutyRoster
    Inherits BasePage
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then

            Dim varLstItem As New ListItem
            varLstItem.Text = "Please Select"
            varLstItem.Value = ""

            DropDownMonth.Items.Insert(0, varLstItem)
            DropDownYear.Items.Insert(0, varLstItem)

            For i As Long = Year(Now()) - 3 To Year(Now()) + 3
                Dim varLstYear As New ListItem
                varLstYear.Text = i
                varLstYear.Value = i
                DropDownYear.Items.Add(varLstYear)
            Next
            Table1.Visible = False
        End If
    End Sub
    Protected Sub Submit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Submit.Click
        GetData()
    End Sub
    Protected Sub GetData()
        Dim varMonth, varYear
        Dim varDeptId As String = String.Empty
        Dim varSundayOff(31)
        Dim varSaturdayOff(31)
        Dim varAttendanceStatus(31)
        'Dim varArrDay()

        Dim clsDS As ETS.BL.DutyRoster
        Dim clsUsr As ETS.BL.Users
        Dim DS As Data.DataSet
        Dim oRecShift As Data.DataTableReader
        Try
            varMonth = DropDownMonth.Items(DropDownMonth.SelectedIndex).Value
            varYear = DropDownYear.Items(DropDownYear.SelectedIndex).Value


            'Calculate the number of days in month
            Dim vardtTempDate As Date
            Dim varIntMonthDay, varDaysInMonth

            vardtTempDate = DateAdd("m", 1, DateSerial(varYear, varMonth, 1))
            varIntMonthDay = Day(DateAdd("d", -1, vardtTempDate))
            varDaysInMonth = varIntMonthDay

            'Calculate the weekly off in particular month
            Dim varDtStartDate As Date, varDtTDate As Date, varDtEndDate As Date

            varDtStartDate = DateSerial(varYear, varMonth, 1)
            varDtTDate = DateAdd("m", 1, varDtStartDate)
            varDtEndDate = DateAdd("d", -1, varDtTDate)

            Dim varTempDtStartDate, varTempDtEndDate
            varTempDtStartDate = varDtStartDate
            varTempDtEndDate = varDtEndDate

            While varDtEndDate >= varDtStartDate
                If Trim(UCase("Sunday")) = Trim(UCase(WeekdayName(Weekday(varDtStartDate)))) Then
                    varSundayOff(Day(varDtStartDate)) = Day(varDtStartDate)

                ElseIf Trim(UCase("Saturday")) = Trim(UCase(WeekdayName(Weekday(varDtStartDate)))) Then
                    varSaturdayOff(Day(varDtStartDate)) = Day(varDtStartDate)
                End If

                'varArrDay(Day(varDtStartDate)) = varDtStartDate.ToShortDateString
                varDtStartDate = DateAdd(DateInterval.Day, 1, varDtStartDate)

            End While
            Dim vartblRow As New TableRow

            Dim cellName As New TableCell
            cellName.Text = "Employee Name"
            cellName.CssClass = "alt1"
            vartblRow.Cells.Add(cellName)


            For i As Integer = 1 To varDaysInMonth
                Dim cell As New TableCell
                cell.Width = 30
                cell.Text = i
                If i = varSundayOff(i) Then
                    cell.ForeColor = Drawing.Color.Red
                ElseIf i = varSaturdayOff(i) Then
                    cell.ForeColor = Drawing.Color.Green
                End If
                cell.CssClass = "alt1"
                vartblRow.Cells.Add(cell)
            Next
            'vartblRow.CssClass = "SMSelected"
            Table1.Rows.Add(vartblRow)

            Dim varArrShift(varDaysInMonth)
            Dim varArrShiftDay(varDaysInMonth)

            For t As Integer = 1 To varDaysInMonth
                varArrShiftDay(t) = DateSerial(varYear, varMonth, t).ToShortDateString
                If t = varSundayOff(t) Then
                    varArrShift(t) = "O"
                Else
                    varArrShift(t) = "I"
                End If
            Next

            Dim varTblRowEmp As New TableRow
            Dim varTblCellName As New TableCell

            'Get Employee Name
            clsUsr = New ETS.BL.Users(Session("UserID").ToString)
            varTblCellName.Text = clsUsr.FirstName & " " & clsUsr.LastName




            varTblRowEmp.Cells.Add(varTblCellName)


            Table1.Rows.Add(varTblRowEmp)
            clsDS = New ETS.BL.DutyRoster
            DS = clsDS.GetDutyRosterRecordsForMonthByUsrID(Session("UserId").ToString, varMonth, varYear)
            If DS.Tables.Count > 0 Then
                If DS.Tables(0).Rows.Count > 0 Then
                    oRecShift = DS.Tables(0).CreateDataReader
                    If oRecShift.HasRows Then
                        While oRecShift.Read
                            If Not oRecShift.IsDBNull(oRecShift.GetOrdinal("ShiftPrefix")) Then
                                varArrShift(Day(oRecShift("DutyDate"))) = oRecShift.GetString(oRecShift.GetOrdinal("ShiftPrefix"))
                            End If
                            varArrShiftDay(Day(oRecShift("DutyDate"))) = oRecShift.GetDateTime(oRecShift.GetOrdinal("DutyDate")).ToShortDateString
                        End While
                    End If

                    oRecShift.Close()
                End If
            End If

            For i As Integer = 1 To varDaysInMonth
                Dim varTblCellShift As New TableCell
                If i = varSundayOff(i) Then
                    If varArrShift(i) = "" Then
                        varArrShift(i) = "O"
                    End If
                    varTblCellShift.ForeColor = Drawing.Color.Red
                ElseIf i = varSaturdayOff(i) Then
                    varTblCellShift.ForeColor = Drawing.Color.Green
                End If
                'If varArrDay(i) = varArrShiftDay(i) Then
                varTblCellShift.Text = varArrShift(i)
                'End If

                varTblRowEmp.Cells.Add(varTblCellShift)
            Next
            Table1.Rows.Add(varTblRowEmp)
            Table1.Visible = True

        Catch ex As Exception
            'Response.Write(ex.Message)
        Finally
            clsDS = Nothing
            clsUsr = Nothing
            DS.Dispose()
            oRecShift = Nothing
        End Try
    End Sub
End Class
