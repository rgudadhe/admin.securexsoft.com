Imports MainModule
Partial Class LeaveAttendanceMainNew_Reports_Reports
    Inherits BasePage
    Dim objMainModule As New MainModule
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            LoadTableValuesStatusEmp()
        End If
    End Sub
    Private Sub LoadTableValuesStatusEmp()
        Try
            Dim varDtStartDate As Date
            Dim varDtEndDate As Date
            Dim varDtTempDate As Date
            Dim varIntMonth As Integer
            Dim varIntYear As Integer

            Dim varDtTempDate1 As Date

            Dim clsAtt As ETS.BL.Attendance
            Try
                clsAtt = New ETS.BL.Attendance
                varDtTempDate1 = clsAtt.GetDateAfterDayLightChecking(Now())
            Catch ex As Exception
                Response.Write(ex.Message)
            Finally
                clsAtt = Nothing
            End Try

            'varIntMonth = Month(Now())
            'varIntYear = Year(Now())

            varIntMonth = Month(varDtTempDate1)
            varIntYear = Year(varDtTempDate1)

            varDtStartDate = DateSerial(varIntYear, varIntMonth, 1)
            varDtTempDate = DateAdd(DateInterval.Month, 1, varDtStartDate)
            varDtEndDate = DateAdd(DateInterval.Day, -1, varDtTempDate)
            Dim varCurYear As Integer
            varCurYear = Year(Now)
            Dim varEmptyLst As New ListItem
            varEmptyLst.Value = ""
            varEmptyLst.Text = "--Year--"
            'DropDownYear.Items.Add(varEmptyLst)
            Dim varIntI As Long
            DropDownYearStatusEmp.Items.Clear()
            For varIntI = varCurYear - 2 To varCurYear + 2
                Dim varLst As New ListItem
                varLst.Value = varIntI
                varLst.Text = varIntI
                If varIntI = varIntYear Then
                    'varLst.Selected = True
                End If
                DropDownYearStatusEmp.Items.Add(varLst)
            Next
            Dim varIntJ As Integer
            Dim varMEmptyLst As New ListItem
            varMEmptyLst.Value = ""
            varMEmptyLst.Text = "--Month--"
            'DropDownMonth.Items.Add(varMEmptyLst)
            DropDownMonthStatusEmp.Items.Clear()
            For varIntJ = 1 To 12
                Dim varLst As New ListItem
                varLst.Value = varIntJ
                varLst.Text = MonthName(varIntJ)
                If varIntJ = varIntMonth Then
                    'varLst.Selected = True
                End If
                DropDownMonthStatusEmp.Items.Add(varLst)
            Next

            DropDownDept.Items.Clear()

            Dim clsDept As ETS.BL.Department
            Dim DS As New Data.DataSet
            Dim DV As Data.DataView
            Try
                clsDept = New ETS.BL.Department
                clsDept.ContractorID = Session("ContractorID")
                DS = clsDept.GetDepartmentLstByWrkGroupID(Session("ContractorID"), Session("WorkGroupID"), String.Empty)


                If DS.Tables.Count > 0 Then
                    If DS.Tables(0).Rows.Count > 0 Then
                        DV = New Data.DataView(DS.Tables(0), "(Deleted IS NULL OR Deleted='false')", String.Empty, Data.DataViewRowState.CurrentRows)
                        If DV.ToTable().Rows.Count > 0 Then
                            DropDownDept.DataSource = DV
                            DropDownDept.DataValueField = "DepartmentID"
                            DropDownDept.DataTextField = "Name"
                            DropDownDept.DataBind()
                        End If
                    End If
                End If
            Catch ex As Exception
                Response.Write(ex.Message)
            Finally
                clsDept = Nothing
                DS.Dispose()
                DV.Dispose()
            End Try

            Dim varEmptyLstDept As New ListItem
            varEmptyLstDept.Value = String.Empty
            varEmptyLstDept.Text = "Any"
            DropDownDept.Items.Insert(0, varEmptyLstDept)
            FillTableStatusEmp(varDtStartDate, varDtEndDate, String.Empty)
        Catch ex As Exception
            Response.Write(ex.Message)
            Response.End()
        End Try
    End Sub
    Protected Sub FillTableStatusEmp(ByVal StartDate As Date, ByVal EndDate As Date, ByVal Dept As String)
        Dim varTblRowMain As New TableRow
        Dim varDtSDate As Date
        Dim varDtEDate As Date
        Dim varDtTodayDate As Date
        Dim varStrQuery As String
        Dim varArrDate(31) As String
        Dim varArrLeaveDate(31) As String
        Dim varArrLeaveStatus(31) As String
        Dim varArrSignInStatus(31) As String
        Dim varArrSignOutStatus(31) As String

        Try
            Dim varIntForLoop As Integer
            For varIntForLoop = 0 To 31
                varArrLeaveDate(varIntForLoop) = Nothing
                varArrLeaveStatus(varIntForLoop) = Nothing
                varArrDate(varIntForLoop) = Nothing
                varArrSignInStatus(varIntForLoop) = Nothing
                varArrSignOutStatus(varIntForLoop) = Nothing
            Next
            Dim i As Integer

            Dim clsAtt As ETS.BL.Attendance
            Try
                clsAtt = New ETS.BL.Attendance
                varDtTodayDate = clsAtt.GetDateAfterDayLightChecking(Now())
            Catch ex As Exception
                Response.Write(ex.Message)
            Finally
                clsAtt = Nothing
            End Try

            Dim varIntMonth As Integer
            varIntMonth = Month(StartDate)
            Dim varIntYear As Integer
            varIntYear = Year(StartDate)

            DropDownMonthStatusEmp.SelectedIndex = -1
            DropDownYearStatusEmp.SelectedIndex = -1

            DropDownMonthStatusEmp.Items.FindByValue(varIntMonth).Selected = True
            DropDownYearStatusEmp.Items.FindByValue(varIntYear).Selected = True

            Dim clsLeave As ETS.BL.Leave
            Dim DsTemp As New Data.DataSet
            Dim objRecGetMonthlyLeaveStatus As Data.DataTableReader
            Try
                clsLeave = New ETS.BL.Leave
                'Response.Write(Dept.ToString)
                DsTemp = clsLeave.GetLeaveDetailsforMonthByDept(Dept.ToString, Month(StartDate), Month(StartDate), Year(StartDate), Year(StartDate), varDtTodayDate, Session("ContractorID").ToString, Session("WorkGroupID"))
                objRecGetMonthlyLeaveStatus = DsTemp.Tables(0).CreateDataReader

                If objRecGetMonthlyLeaveStatus.HasRows Then
                    While objRecGetMonthlyLeaveStatus.Read
                        Dim varDtStDate As Date
                        Dim varDtEdDate As Date
                        Dim varStrLeaveType As String
                        Dim varStrLeaveID As String
                        Dim varStrLeaveStatus As String
                        Dim varStrUserName As String
                        varStrLeaveType = ""
                        varStrUserName = ""


                        If Not objRecGetMonthlyLeaveStatus.IsDBNull(objRecGetMonthlyLeaveStatus.GetOrdinal("FirstName")) Then
                            varStrUserName = objRecGetMonthlyLeaveStatus.GetString(objRecGetMonthlyLeaveStatus.GetOrdinal("FirstName"))
                        End If
                        If Not objRecGetMonthlyLeaveStatus.IsDBNull(objRecGetMonthlyLeaveStatus.GetOrdinal("LastName")) Then
                            varStrUserName = varStrUserName & " " & objRecGetMonthlyLeaveStatus.GetString(objRecGetMonthlyLeaveStatus.GetOrdinal("LastName"))
                        End If

                        If Not objRecGetMonthlyLeaveStatus.IsDBNull(objRecGetMonthlyLeaveStatus.GetOrdinal("TypeOfLeave")) Then
                            If Trim(UCase(objRecGetMonthlyLeaveStatus.GetString(objRecGetMonthlyLeaveStatus.GetOrdinal("TypeOfLeave")))) = Trim(UCase("HL")) Then
                                varStrLeaveType = "(Half Day)"
                            ElseIf Trim(UCase(objRecGetMonthlyLeaveStatus.GetString(objRecGetMonthlyLeaveStatus.GetOrdinal("TypeOfLeave")))) = Trim(UCase("LWPHL")) Then
                                varStrLeaveType = "(Half Day - Leave Without Pay)"
                            ElseIf Trim(UCase(objRecGetMonthlyLeaveStatus.GetString(objRecGetMonthlyLeaveStatus.GetOrdinal("TypeOfLeave")))) = Trim(UCase("AHL")) Then
                                varStrLeaveType = "(Half Day - Advance Leave)"
                            ElseIf Trim(UCase(objRecGetMonthlyLeaveStatus.GetString(objRecGetMonthlyLeaveStatus.GetOrdinal("TypeOfLeave")))) = Trim(UCase("ELHL")) Then
                                varStrLeaveType = "(Half Day - Earned Leave)"
                            ElseIf Trim(UCase(objRecGetMonthlyLeaveStatus.GetString(objRecGetMonthlyLeaveStatus.GetOrdinal("TypeOfLeave")))) = Trim(UCase("CLHL")) Then
                                varStrLeaveType = "(Half Day - Casual Leave)"
                            End If
                        End If
                        varStrLeaveID = objRecGetMonthlyLeaveStatus.GetGuid(objRecGetMonthlyLeaveStatus.GetOrdinal("LeaveID")).ToString
                        varDtStDate = objRecGetMonthlyLeaveStatus.GetDateTime(objRecGetMonthlyLeaveStatus.GetOrdinal("StartDate"))
                        varDtEdDate = objRecGetMonthlyLeaveStatus.GetDateTime(objRecGetMonthlyLeaveStatus.GetOrdinal("EndDate"))

                        While varDtEdDate >= varDtStDate
                            If Month(varDtStDate) = Month(StartDate) And Year(varDtStDate) = Year(StartDate) Then
                                If varStrLeaveType <> "" Then
                                    varStrLeaveStatus = varStrUserName & "(Leave " & objRecGetMonthlyLeaveStatus.GetString(objRecGetMonthlyLeaveStatus.GetOrdinal("Status")) & varStrLeaveType & ")"
                                Else
                                    varStrLeaveStatus = varStrUserName & "(Leave " & objRecGetMonthlyLeaveStatus.GetString(objRecGetMonthlyLeaveStatus.GetOrdinal("Status")) & ")"
                                End If

                                If Not objRecGetMonthlyLeaveStatus.IsDBNull(objRecGetMonthlyLeaveStatus.GetOrdinal("TypeOfLeave")) Then
                                    If Trim(UCase(objRecGetMonthlyLeaveStatus.GetString(objRecGetMonthlyLeaveStatus.GetOrdinal("Status")))) = Trim(UCase("Not Approved")) Or Trim(UCase(objRecGetMonthlyLeaveStatus.GetString(objRecGetMonthlyLeaveStatus.GetOrdinal("TypeOfLeave")))) = Trim(UCase("LWP")) Or Trim(UCase(objRecGetMonthlyLeaveStatus.GetString(objRecGetMonthlyLeaveStatus.GetOrdinal("TypeOfLeave")))) = Trim(UCase("LWPHL")) Then
                                        varStrLeaveStatus = "<font face=""Trebuchet MS"" size=""2"" color=green>" & varStrLeaveStatus & "</font>"
                                    End If
                                End If

                                If varArrLeaveStatus(Day(varDtStDate)) <> "" Then
                                    varArrLeaveStatus(Day(varDtStDate)) = varArrLeaveStatus(Day(varDtStDate)) & "," & varStrLeaveStatus
                                Else
                                    varArrLeaveStatus(Day(varDtStDate)) = varStrLeaveStatus
                                End If

                                If varArrLeaveStatus(Day(varDtStDate)) <> "" Then
                                    varArrLeaveDate(Day(varDtStDate)) = varDtStDate
                                End If
                            Else
                                varArrLeaveDate(Day(varDtStDate)) = String.Empty
                            End If

                            varDtStDate = DateAdd(DateInterval.Day, 1, varDtStDate)
                        End While
                    End While
                End If
                objRecGetMonthlyLeaveStatus.Close()
            Catch ex As Exception
                Response.Write(ex.Message)
            Finally
                objRecGetMonthlyLeaveStatus = Nothing
                DsTemp.Dispose()
                clsLeave = Nothing
            End Try


            For i = 1 To 7
                Dim varStrDayName As String
                Dim varTblCellDayName As New TableCell
                varStrDayName = WeekdayName(i)
                varTblCellDayName.Width = 120
                varTblCellDayName.HorizontalAlign = HorizontalAlign.Center
                varTblCellDayName.Text = varStrDayName
                varTblCellDayName.CssClass = "alt1"
                varTblRowMain.Cells.Add(varTblCellDayName)
            Next

            tblMainCalendarStatusEmp.Rows.Add(varTblRowMain)

            varDtSDate = StartDate
            varDtEDate = EndDate
            Dim varWeekCount As Integer
            varWeekCount = 1
            lblMonthNameStatusEmp.Text = MonthName(Month(varDtSDate)) & Year(varDtSDate)

            While 6 >= varWeekCount
                If varDtEDate >= varDtSDate Then
                    Dim varTblRowWeekDayName As New TableRow
                    For i = 1 To 7
                        Dim varTblCellWeekDayName As New TableCell
                        varTblCellWeekDayName.Width = 120
                        If varDtEDate >= varDtSDate Then
                            Dim varStrWDayName As String
                            Dim varStrW1DayName As String
                            Dim varDtTempDate As Date
                            Dim varStrTemp As String
                            varStrWDayName = WeekdayName(i)
                            varStrW1DayName = WeekdayName(Weekday(varDtSDate))
                            varStrTemp = ""
                            If varArrLeaveDate(Day(varDtSDate)) <> "" Then
                                varDtTempDate = CDate(varArrLeaveDate(Day(varDtSDate)))
                            End If

                            If Trim(UCase(varStrWDayName)) = Trim(UCase(varStrW1DayName)) Then
                                varTblCellWeekDayName.Font.Name = "Arial"
                                varTblCellWeekDayName.Font.Size = 8
                                varTblCellWeekDayName.Height = 80
                                varTblCellWeekDayName.VerticalAlign = VerticalAlign.Top
                                varTblCellWeekDayName.HorizontalAlign = HorizontalAlign.Left
                                varStrTemp = "<font face=""Arial"" size=""2"">" & Day(varDtSDate) & "</font>"
                                'If DateDiff(DateInterval.Day, varDtSDate, varDtTempDate) = 0 Then
                                If varArrLeaveStatus(Day(varDtSDate)) <> "" Then
                                    varStrTemp = varStrTemp & "<BR>" & Trim(varArrLeaveStatus(Day(varDtSDate)))
                                Else
                                    varStrTemp = varStrTemp
                                End If
                                varTblCellWeekDayName.Text = varStrTemp
                                If DateDiff(DateInterval.Day, CDate(varDtSDate), CDate(varDtTodayDate)) = 0 And Day(varDtSDate) = Day(varDtTodayDate) And Month(varDtSDate) = Month(varDtTodayDate) And Year(varDtSDate) = Year(varDtTodayDate) Then
                                    varTblCellWeekDayName.BackColor = Drawing.Color.AliceBlue
                                End If
                                'Else
                                'varTblCellWeekDayName.Text = varStrTemp
                                'End If
                                varDtSDate = DateAdd(DateInterval.Day, 1, varDtSDate)
                            Else
                                varTblCellWeekDayName.BackColor = Drawing.Color.GhostWhite
                                varTblCellWeekDayName.Text = "&nbsp"
                            End If
                        Else
                            varTblCellWeekDayName.BackColor = Drawing.Color.GhostWhite
                            varTblCellWeekDayName.Text = "&nbsp"
                        End If
                        varTblRowWeekDayName.Cells.Add(varTblCellWeekDayName)
                    Next
                    tblMainCalendarStatusEmp.Rows.Add(varTblRowWeekDayName)
                    varWeekCount = varWeekCount + 1
                Else
                    Exit While
                End If
            End While
        Catch ex As Exception

        End Try
    End Sub
    Protected Sub DropDownDept_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles DropDownDept.SelectedIndexChanged
        Try
            Dim varStrMonthName As String
            Dim varIntMonth As Integer
            Dim varIntYear As Integer
            Dim varDtTempDate As Date
            Dim varDtTempDate1 As Date
            Dim varDtStartDate As Date
            Dim varDtEndDate As Date
            Dim varStrDept As String
            varStrDept = DropDownDept.Items(DropDownDept.SelectedIndex).Value.ToString
            varStrMonthName = Left(lblMonthNameStatusEmp.Text, Microsoft.VisualBasic.Len(lblMonthNameStatusEmp.Text) - 4)
            varIntYear = Right(lblMonthNameStatusEmp.Text, 4)
            varIntMonth = objMainModule.GetMonthFromMonthName(varStrMonthName)
            varDtTempDate = DateSerial(varIntYear, varIntMonth, 1)
            varDtStartDate = varDtTempDate
            varDtTempDate1 = DateAdd(DateInterval.Month, 1, varDtStartDate)
            varDtEndDate = DateAdd(DateInterval.Day, -1, varDtTempDate1)
            FillTableStatusEmp(varDtStartDate, varDtEndDate, varStrDept)
        Catch ex As Exception

        End Try
    End Sub
    Protected Sub btnGoStatusEmp_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnGoStatusEmp.Click
        Try
            Dim varIntYear As Long
            Dim varIntMonth As Integer
            varIntYear = 0
            varIntMonth = 0
            Dim varStrDept As String
            varStrDept = DropDownDept.Items(DropDownDept.SelectedIndex).Value.ToString
            If DropDownYearStatusEmp.SelectedItem.Value.ToString <> "" Then
                varIntYear = DropDownYearStatusEmp.SelectedItem.Value.ToString
            End If
            If DropDownMonthStatusEmp.SelectedItem.Value.ToString <> "" Then
                varIntMonth = DropDownMonthStatusEmp.SelectedItem.Value.ToString
            End If

            If varIntYear > 0 And varIntMonth > 0 Then
                Dim varDtStartDate As Date
                Dim varDtEndDate As Date
                Dim varDtTempDate As Date
                varDtStartDate = DateSerial(varIntYear, varIntMonth, 1)
                varDtTempDate = DateAdd(DateInterval.Month, 1, varDtStartDate)
                varDtEndDate = DateAdd(DateInterval.Day, -1, varDtTempDate)
                FillTableStatusEmp(varDtStartDate, varDtEndDate, varStrDept)
            Else

            End If
        Catch ex As Exception
        End Try
    End Sub
    Protected Sub BtnNextStatusEmp_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtnNextStatusEmp.Click
        Try
            Dim varStrMonthName As String
            Dim varIntMonth As Integer
            Dim varIntYear As Integer
            Dim varDtTempDate As Date
            Dim varDtTempDate1 As Date
            Dim varDtStartDate As Date
            Dim varDtEndDate As Date
            Dim varStrDept As String
            varStrDept = DropDownDept.Items(DropDownDept.SelectedIndex).Value.ToString
            varStrMonthName = Left(lblMonthNameStatusEmp.Text, Microsoft.VisualBasic.Len(lblMonthNameStatusEmp.Text) - 4)
            varIntYear = Right(lblMonthNameStatusEmp.Text, 4)
            varIntMonth = objMainModule.GetMonthFromMonthName(varStrMonthName)
            varDtTempDate = DateSerial(varIntYear, varIntMonth, 1)
            varDtStartDate = DateAdd(DateInterval.Month, 1, DateSerial(Year(varDtTempDate), Month(varDtTempDate), Day(varDtTempDate)))
            varDtTempDate1 = DateAdd(DateInterval.Month, 1, varDtStartDate)
            varDtEndDate = DateAdd(DateInterval.Day, -1, varDtTempDate1)
            FillTableStatusEmp(varDtStartDate, varDtEndDate, varStrDept)
        Catch ex As Exception

        End Try

    End Sub
    Protected Sub BtnPrevStatusEmp_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtnPrevStatusEmp.Click
        Try
            Dim varStrMonthName As String
            Dim varIntMonth As Integer
            Dim varIntYear As Integer
            Dim varDtTempDate As Date
            Dim varDtTempDate1 As Date
            Dim varDtStartDate As Date
            Dim varDtEndDate As Date
            Dim varStrDept As String
            varStrDept = DropDownDept.Items(DropDownDept.SelectedIndex).Value.ToString
            varStrMonthName = Left(lblMonthNameStatusEmp.Text, Microsoft.VisualBasic.Len(lblMonthNameStatusEmp.Text) - 4)
            varIntYear = Right(lblMonthNameStatusEmp.Text, 4)
            varIntMonth = objMainModule.GetMonthFromMonthName(varStrMonthName)
            varDtTempDate = DateSerial(varIntYear, varIntMonth, 1)
            varDtStartDate = DateAdd(DateInterval.Month, -1, DateSerial(Year(varDtTempDate), Month(varDtTempDate), Day(varDtTempDate)))
            varDtTempDate1 = DateAdd(DateInterval.Month, 1, varDtStartDate)
            varDtEndDate = DateAdd(DateInterval.Day, -1, varDtTempDate1)
            FillTableStatusEmp(varDtStartDate, varDtEndDate, varStrDept)
        Catch ex As Exception
        End Try
    End Sub
    Protected Sub ReportTabContainer_ActiveTabChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ReportTabContainer.ActiveTabChanged
        If ReportTabContainer.ActiveTabIndex = 3 Then
            LoadTableValuesStatusEmp()
        End If
    End Sub
End Class

