Imports System.Reflection
Imports MainModule
Partial Class LeaveAttendanceMainNew_Employee_Employee
    Inherits BasePage
    Dim objMainModule As New MainModule
    Dim varStrState As String
    Dim varStrDeptID As String
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Trim(UCase(Session("UserID").ToString)) = Trim(UCase("A1ABBF5E-4869-4600-907F-01B6FAEEF377")) Then
            'Response.Write("Leave application pending(HL)".Contains("LWPHL"))
            'Session("UserID") = "3DDEF488-7581-4433-B6BA-96A679F1E427"
            'Response.Write(Server.MapPath("/ETS_Files/"))
            'Response.Write(Server.MapPath("../ETS_Files"))

        End If

        If Request.QueryString("Dt") <> "" And Trim(UCase(Request.QueryString("Opr"))) = Trim(UCase("SignIn")) Then
            Dim clsAttendance As ETS.BL.Attendance

            Try
                clsAttendance = New ETS.BL.Attendance
                With clsAttendance
                    .UserID = Session("UserID").ToString
                    .ContractorID = Session("ContractorID").ToString
                End With
                If clsAttendance.btn_SignIn() = True Then
                    Response.Redirect("Employee.aspx")
                Else
                    Response.Redirect("Employee.aspx")
                End If
            Catch ex As Exception
            Finally
                clsAttendance = Nothing
            End Try
            Dim isreadonly As PropertyInfo = GetType(System.Collections.Specialized.NameValueCollection).GetProperty("IsReadOnly", BindingFlags.Instance Or BindingFlags.NonPublic)

            ' make collection editable
            isreadonly.SetValue(Me.Request.QueryString, False, Nothing)

            ' remove
            Me.Request.QueryString.Remove("Opr")
            Me.Request.QueryString.Remove("Dt")
            'Request.QueryString.Clear()
        End If

        If Request.QueryString("Dt") <> "" And Trim(UCase(Request.QueryString("Opr"))) = Trim(UCase("SignOut")) Then

            Dim clsAttendance As ETS.BL.Attendance
            Try
                clsAttendance = New ETS.BL.Attendance
                clsAttendance.UserID = Session("UserID").ToString
                clsAttendance.ContractorID = Session("ContractorID").ToString
                If clsAttendance.btn_SignOut(Request.QueryString("Dt")) = True Then
                    Response.Redirect("Employee.aspx")
                End If
            Catch ex As Exception
            Finally
                clsAttendance = Nothing
            End Try
            Dim isreadonly As PropertyInfo = GetType(System.Collections.Specialized.NameValueCollection).GetProperty("IsReadOnly", BindingFlags.Instance Or BindingFlags.NonPublic)

            ' make collection editable
            isreadonly.SetValue(Me.Request.QueryString, False, Nothing)

            ' remove
            Me.Request.QueryString.Remove("Opr")
            Me.Request.QueryString.Remove("Dt")
            'Request.QueryString.Clear()
        End If
        If Not Page.IsPostBack Then
            LoadTableValues()
            LoadTableValuesStatus()
        End If
    End Sub
    Private Sub LoadTableValues()
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
            Finally
                clsAtt = Nothing
            End Try
            

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
            DropDownYear.Items.Clear()
            For varIntI = varCurYear - 2 To varCurYear + 2
                Dim varLst As New ListItem
                varLst.Value = varIntI
                varLst.Text = varIntI
                If varIntI = varIntYear Then
                    'varLst.Selected = True
                End If
                DropDownYear.Items.Add(varLst)
            Next
            Dim varIntJ As Integer
            Dim varMEmptyLst As New ListItem
            varMEmptyLst.Value = ""
            varMEmptyLst.Text = "--Month--"
            'DropDownMonth.Items.Add(varMEmptyLst)
            DropDownMonth.Items.Clear()
            For varIntJ = 1 To 12
                Dim varLst As New ListItem
                varLst.Value = varIntJ
                varLst.Text = MonthName(varIntJ)
                If varIntJ = varIntMonth Then
                    'varLst.Selected = True
                End If
                DropDownMonth.Items.Add(varLst)
            Next
            FillTable(varDtStartDate, varDtEndDate)
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub
    Protected Sub FillTable(ByVal StartDate As Date, ByVal EndDate As Date)
        Dim varTblRowMain As New TableRow
        Dim varDtSDate As Date
        Dim varDtEDate As Date
        Dim varDtTodayDate As Date
        Dim varArrDate(31) As String
        Dim varArrLeaveStatus(31) As String
        Dim varArrSignInStatus(31) As String
        Dim varArrSignOutStatus(31) As String

        Dim clsLeave As ETS.BL.Leave
        Dim DSLeave As New Data.DataSet
        Dim clsAttendance As ETS.BL.Attendance
        Dim DSAttendance As New Data.DataSet
        Dim objRecGetMonthlyLeaveStatus As Data.DataTableReader
        Dim objRecGetAttendanceStatus As Data.DataTableReader


        Try
            Dim varIntForLoop As Integer
            For varIntForLoop = 0 To 31
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


            clsLeave = New ETS.BL.Leave
            DSLeave = clsLeave.GetLeaveDetailsforMonthByUsr(Session("UserID").ToString, Month(StartDate), Month(StartDate), Year(StartDate), Year(StartDate), varDtTodayDate, Session("ContractorID").ToString)

            objRecGetMonthlyLeaveStatus = DSLeave.Tables(0).CreateDataReader


            Dim varIntMonth As Integer
            varIntMonth = Month(StartDate)
            Dim varIntYear As Integer
            varIntYear = Year(StartDate)


            DropDownMonth.SelectedIndex = -1
            DropDownYear.SelectedIndex = -1

            DropDownMonth.Items.FindByValue(varIntMonth).Selected = True
            DropDownYear.Items.FindByValue(varIntYear).Selected = True


            If objRecGetMonthlyLeaveStatus.HasRows Then
                While objRecGetMonthlyLeaveStatus.Read
                    Dim varDtStDate As Date
                    Dim varDtEdDate As Date
                    Dim varStrLeaveStatus As String
                    Dim varStrLType As String = String.Empty

                    If Trim(UCase(objRecGetMonthlyLeaveStatus.GetString(objRecGetMonthlyLeaveStatus.GetOrdinal("Status")))) = Trim(UCase("Pending")) Then
                        varStrLeaveStatus = "Leave application pending"

                        If Not objRecGetMonthlyLeaveStatus.IsDBNull(objRecGetMonthlyLeaveStatus.GetOrdinal("TypeOfLeave")) Then
                            If Trim(UCase(objRecGetMonthlyLeaveStatus.GetString(objRecGetMonthlyLeaveStatus.GetOrdinal("TypeOfLeave")))) = Trim(UCase("HL")) Or Trim(UCase(objRecGetMonthlyLeaveStatus.GetString(objRecGetMonthlyLeaveStatus.GetOrdinal("TypeOfLeave")))) = Trim(UCase("LWPHL")) Or Trim(UCase(objRecGetMonthlyLeaveStatus.GetString(objRecGetMonthlyLeaveStatus.GetOrdinal("TypeOfLeave")))) = Trim(UCase("ELHL")) Or Trim(UCase(objRecGetMonthlyLeaveStatus.GetString(objRecGetMonthlyLeaveStatus.GetOrdinal("TypeOfLeave")))) = Trim(UCase("CLHL")) Then
                                varStrLeaveStatus = varStrLeaveStatus & "(" & objRecGetMonthlyLeaveStatus.GetString(objRecGetMonthlyLeaveStatus.GetOrdinal("TypeOfLeave")) & ")"
                            End If
                        End If
                    Else
                        If Not objRecGetMonthlyLeaveStatus.IsDBNull(objRecGetMonthlyLeaveStatus.GetOrdinal("TypeOfLeave")) Then
                            varStrLeaveStatus = objRecGetMonthlyLeaveStatus.GetString(objRecGetMonthlyLeaveStatus.GetOrdinal("TypeOfLeave"))
                        Else
                            varStrLeaveStatus = ""
                        End If
                    End If

                    If Not objRecGetMonthlyLeaveStatus.IsDBNull(objRecGetMonthlyLeaveStatus.GetOrdinal("TypeOfLeave")) Then
                        varStrLType = objRecGetMonthlyLeaveStatus.GetString(objRecGetMonthlyLeaveStatus.GetOrdinal("TypeOfLeave"))
                    End If

                    varDtStDate = objRecGetMonthlyLeaveStatus.GetDateTime(objRecGetMonthlyLeaveStatus.GetOrdinal("StartDate"))
                    varDtEdDate = objRecGetMonthlyLeaveStatus.GetDateTime(objRecGetMonthlyLeaveStatus.GetOrdinal("EndDate"))

                    While varDtEdDate >= varDtStDate
                        If varStrLType.Contains("HL") Then
                            If Month(varDtStDate) = Month(StartDate) And Year(varDtStDate) = Year(StartDate) Then
                                If String.IsNullOrEmpty(varArrLeaveStatus(Day(varDtStDate))) Then
                                    varArrLeaveStatus(Day(varDtStDate)) = varStrLeaveStatus.ToString
                                Else
                                    varArrLeaveStatus(Day(varDtStDate)) = varArrLeaveStatus(Day(varDtStDate)) & "," & varStrLeaveStatus.ToString
                                End If
                            Else
                                If String.IsNullOrEmpty(varArrLeaveStatus(Day(varDtStDate))) Then
                                    varArrLeaveStatus(Day(varDtStDate)) = String.Empty
                                End If
                            End If
                        Else
                            If Month(varDtStDate) = Month(StartDate) And Year(varDtStDate) = Year(StartDate) And String.IsNullOrEmpty(varArrLeaveStatus(Day(varDtStDate))) Then
                                varArrLeaveStatus(Day(varDtStDate)) = varStrLeaveStatus.ToString
                            Else
                                If String.IsNullOrEmpty(varArrLeaveStatus(Day(varDtStDate))) Then
                                    varArrLeaveStatus(Day(varDtStDate)) = String.Empty
                                End If
                            End If
                        End If

                        varDtStDate = DateAdd(DateInterval.Day, 1, varDtStDate)
                    End While
                End While
            End If
            objRecGetMonthlyLeaveStatus.Close()

            clsAttendance = New ETS.BL.Attendance
            DSAttendance = clsAttendance.GetAttendanceforMonthByUsr(Session("UserID").ToString, Month(StartDate), Year(StartDate), Session("ContractorID").ToString)

            objRecGetAttendanceStatus = DSAttendance.Tables(0).CreateDataReader

            If objRecGetAttendanceStatus.HasRows Then
                While objRecGetAttendanceStatus.Read
                    Dim varStrSplit As Date
                    Dim varStrSplit1 As Date
                    If Not objRecGetAttendanceStatus.IsDBNull(objRecGetAttendanceStatus.GetOrdinal("AttDate")) Then
                        If Trim(UCase(objRecGetAttendanceStatus.GetString(objRecGetAttendanceStatus.GetOrdinal("Status")))) = Trim(UCase("P")) Or Trim(UCase(objRecGetAttendanceStatus.GetString(objRecGetAttendanceStatus.GetOrdinal("Status")))) = Trim(UCase("HP")) Or Trim(UCase(objRecGetAttendanceStatus.GetString(objRecGetAttendanceStatus.GetOrdinal("Status")))) = Trim(UCase("HPLWP")) Then
                            varStrSplit = CDate(objRecGetAttendanceStatus.GetString(objRecGetAttendanceStatus.GetOrdinal("SignIn")))
                            varArrSignInStatus(Day(objRecGetAttendanceStatus.GetDateTime(objRecGetAttendanceStatus.GetOrdinal("AttDate")))) = varStrSplit.ToShortTimeString
                            If Not objRecGetAttendanceStatus.IsDBNull(objRecGetAttendanceStatus.GetOrdinal("SignOut")) Then
                                varStrSplit1 = CDate(objRecGetAttendanceStatus.GetString(objRecGetAttendanceStatus.GetOrdinal("SignOut")))
                                varArrSignOutStatus(Day(objRecGetAttendanceStatus.GetDateTime(objRecGetAttendanceStatus.GetOrdinal("AttDate")))) = varStrSplit1.ToShortTimeString
                            End If
                        End If
                    End If
                End While
            End If
            objRecGetAttendanceStatus.Close()

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
            tblMainCalendar.Rows.Add(varTblRowMain)

            varDtSDate = StartDate
            varDtEDate = EndDate
            Dim varWeekCount As Integer
            varWeekCount = 1
            lblMonthName.Text = MonthName(Month(varDtSDate)) & Year(varDtSDate)



            While 6 >= varWeekCount
                If varDtEDate >= varDtSDate Then
                    Dim varTblRowWeekDayName As New TableRow
                    For i = 1 To 7
                        Dim varTblCellWeekDayName As New TableCell
                        varTblCellWeekDayName.Width = 120
                        If varDtEDate >= varDtSDate Then
                            Dim varStrWDayName As String
                            Dim varStrW1DayName As String
                            Dim varStrTemp As String
                            varStrWDayName = WeekdayName(i)
                            varStrW1DayName = WeekdayName(Weekday(varDtSDate))
                            If Trim(UCase(varStrWDayName)) = Trim(UCase(varStrW1DayName)) Then
                                varTblCellWeekDayName.Font.Name = "Arial"
                                varTblCellWeekDayName.Font.Size = 8
                                varTblCellWeekDayName.Font.Bold = True
                                varTblCellWeekDayName.Height = 80
                                varTblCellWeekDayName.VerticalAlign = VerticalAlign.Top
                                varTblCellWeekDayName.HorizontalAlign = HorizontalAlign.Left

                                'If varArrSignInStatus(Day(varDtSDate)) <> "" Then
                                '    If varArrSignOutStatus(Day(varDtSDate)) <> "" Then
                                '        If varArrLeaveStatus(Day(varDtSDate)).Contains("HL") Or varArrLeaveStatus(Day(varDtSDate)).Contains("LWPHL") Then
                                '            varStrTemp = Day(varDtSDate) & "<BR>" & varArrLeaveStatus(Day(varDtSDate)) & "<BR><table><tr><td align=right>In:</td><td align=left>" & varArrSignInStatus(Day(varDtSDate)) & "</td></tr><tr><td align=right>Out:</td><td align=right>" & varArrSignOutStatus(Day(varDtSDate)) & "</td></tr></table>"
                                '        Else
                                '            varStrTemp = Day(varDtSDate) & "<BR><table><tr><td align=right>In:</td><td align=left>" & varArrSignInStatus(Day(varDtSDate)) & "</td></tr><tr><td align=right>Out:</td><td align=right>" & varArrSignOutStatus(Day(varDtSDate)) & "</td></tr></table>"
                                '        End If
                                '    Else
                                '        If DateDiff(DateInterval.Day, CDate(varDtSDate), CDate(varDtTodayDate)) = 0 And Day(varDtSDate) = Day(varDtTodayDate) And Month(varDtSDate) = Month(varDtTodayDate) And Year(varDtSDate) = Year(varDtTodayDate) Then
                                '            If varArrLeaveStatus(Day(varDtSDate)).Contains("HL") Or varArrLeaveStatus(Day(varDtSDate)).Contains("LWPHL") Then
                                '                varStrTemp = Day(varDtSDate) & "<BR>" & varArrLeaveStatus(Day(varDtSDate)) & "<BR><table><tr><td align=right>In:</td><td align=left>" & varArrSignInStatus(Day(varDtSDate)) & "</td></tr><tr><td><a href=Employee.aspx?Opr=SignOut&Dt=" & varDtSDate & ">Out</a></td></tr></table>"
                                '            Else
                                '                varStrTemp = Day(varDtSDate) & "<BR><table><tr><td align=right>In:</td><td align=left>" & varArrSignInStatus(Day(varDtSDate)) & "</td></tr><tr><td><a href=Employee.aspx?Opr=SignOut&Dt=" & varDtSDate & ">Out</a></td></tr></table>"
                                '            End If
                                '        Else
                                '            If varArrLeaveStatus(Day(varDtSDate)).Contains("HL") Or varArrLeaveStatus(Day(varDtSDate)).Contains("LWPHL") Then
                                '                varStrTemp = Day(varDtSDate) & "<BR>" & varArrLeaveStatus(Day(varDtSDate)) & "<BR><table><tr><td align=right>In:</td><td align=left>" & varArrSignInStatus(Day(varDtSDate)) & "</td></tr></table>"
                                '            Else
                                '                varStrTemp = Day(varDtSDate) & "<BR><table><tr><td align=right>In:</td><td align=left>" & varArrSignInStatus(Day(varDtSDate)) & "</td></tr></table>"
                                '            End If
                                '        End If
                                '    End If
                                'ElseIf varArrLeaveStatus(Day(varDtSDate)) <> "" Then
                                '    varStrTemp = Day(varDtSDate) & "<BR>" & varArrLeaveStatus(Day(varDtSDate))
                                'ElseIf DateDiff(DateInterval.Day, CDate(varDtSDate), CDate(varDtTodayDate)) = 0 And Day(varDtSDate) = Day(varDtTodayDate) And Month(varDtSDate) = Month(varDtTodayDate) And Year(varDtSDate) = Year(varDtTodayDate) Then
                                '    varStrTemp = Day(varDtSDate) & "<BR><table><tr><td><a href=Employee.aspx?Opr=SignIn&Dt=" & varDtSDate & ">In</a></td></tr></table>"
                                'Else
                                '    varStrTemp = Day(varDtSDate)
                                'End If

                                If varArrSignInStatus(Day(varDtSDate)) <> "" Then
                                    If varArrSignOutStatus(Day(varDtSDate)) <> "" Then
                                        If varArrLeaveStatus(Day(varDtSDate)) <> "" Then
                                            varStrTemp = Day(varDtSDate) & "<BR>" & varArrLeaveStatus(Day(varDtSDate)) & "<BR><table><tr><td align=right>In:</td><td align=left>" & varArrSignInStatus(Day(varDtSDate)) & "</td></tr><tr><td align=right>Out:</td><td align=right>" & varArrSignOutStatus(Day(varDtSDate)) & "</td></tr></table>"
                                        Else
                                            varStrTemp = Day(varDtSDate) & "<BR><table><tr><td align=right>In:</td><td align=left>" & varArrSignInStatus(Day(varDtSDate)) & "</td></tr><tr><td align=right>Out:</td><td align=right>" & varArrSignOutStatus(Day(varDtSDate)) & "</td></tr></table>"
                                        End If
                                    Else
                                        If DateDiff(DateInterval.Day, CDate(varDtSDate), CDate(varDtTodayDate)) = 0 And Day(varDtSDate) = Day(varDtTodayDate) And Month(varDtSDate) = Month(varDtTodayDate) And Year(varDtSDate) = Year(varDtTodayDate) Then
                                            If varArrLeaveStatus(Day(varDtSDate)) <> "" Then
                                                varStrTemp = Day(varDtSDate) & "<BR>" & varArrLeaveStatus(Day(varDtSDate)) & "<BR><table><tr><td align=right>In:</td><td align=left>" & varArrSignInStatus(Day(varDtSDate)) & "</td></tr><tr><td><a href=Employee.aspx?Opr=SignOut&Dt=" & varDtSDate & ">Out</a></td></tr></table>"
                                            Else
                                                varStrTemp = Day(varDtSDate) & "<BR><table><tr><td align=right>In:</td><td align=left>" & varArrSignInStatus(Day(varDtSDate)) & "</td></tr><tr><td><a href=Employee.aspx?Opr=SignOut&Dt=" & varDtSDate & ">Out</a></td></tr></table>"
                                            End If
                                        Else
                                            If varArrLeaveStatus(Day(varDtSDate)) <> "" Then
                                                varStrTemp = Day(varDtSDate) & "<BR>" & varArrLeaveStatus(Day(varDtSDate)) & "<BR><table><tr><td align=right>In:</td><td align=left>" & varArrSignInStatus(Day(varDtSDate)) & "</td></tr></table>"
                                            Else
                                                varStrTemp = Day(varDtSDate) & "<BR><table><tr><td align=right>In:</td><td align=left>" & varArrSignInStatus(Day(varDtSDate)) & "</td></tr></table>"
                                            End If
                                        End If
                                    End If
                                ElseIf varArrLeaveStatus(Day(varDtSDate)) <> "" Then
                                    If (varArrLeaveStatus(Day(varDtSDate)).Contains("HL") Or varArrLeaveStatus(Day(varDtSDate)).Contains("LWPHL") Or varArrLeaveStatus(Day(varDtSDate)).Contains("CLHL") Or varArrLeaveStatus(Day(varDtSDate)).Contains("ELHL")) And DateDiff(DateInterval.Day, CDate(varDtSDate), CDate(varDtTodayDate)) = 0 And Day(varDtSDate) = Day(varDtTodayDate) And Month(varDtSDate) = Month(varDtTodayDate) And Year(varDtSDate) = Year(varDtTodayDate) Then
                                        varStrTemp = Day(varDtSDate) & "<BR>" & varArrLeaveStatus(Day(varDtSDate)) & "<BR><table><tr><td><a href=Employee.aspx?Opr=SignIn&Dt=" & varDtSDate & ">In</a></td></tr></table>"
                                    Else
                                        varStrTemp = Day(varDtSDate) & "<BR>" & varArrLeaveStatus(Day(varDtSDate))
                                    End If
                                ElseIf DateDiff(DateInterval.Day, CDate(varDtSDate), CDate(varDtTodayDate)) = 0 And Day(varDtSDate) = Day(varDtTodayDate) And Month(varDtSDate) = Month(varDtTodayDate) And Year(varDtSDate) = Year(varDtTodayDate) Then
                                    varStrTemp = Day(varDtSDate) & "<BR><table><tr><td><a href=Employee.aspx?Opr=SignIn&Dt=" & varDtSDate & ">In</a></td></tr></table>"
                                Else
                                    varStrTemp = Day(varDtSDate)
                                End If
                                varTblCellWeekDayName.Text = varStrTemp
                                varDtSDate = DateAdd(DateInterval.Day, 1, varDtSDate)
                            Else
                                varTblCellWeekDayName.Text = "&nbsp"
                            End If
                        Else
                            varTblCellWeekDayName.Text = "&nbsp"
                        End If
                        varTblRowWeekDayName.Cells.Add(varTblCellWeekDayName)
                    Next
                    tblMainCalendar.Rows.Add(varTblRowWeekDayName)
                    varWeekCount = varWeekCount + 1
                Else
                    Exit While
                End If
            End While
        Catch ex As Exception
            Response.Write(ex.Message)
        Finally
            objRecGetMonthlyLeaveStatus = Nothing
            objRecGetAttendanceStatus = Nothing
            DSLeave.Dispose()
            DSAttendance.Dispose()
            clsLeave = Nothing
            clsAttendance = Nothing
        End Try
    End Sub
    Private Sub LoadTableValuesStatus()
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
            DropDownYearStatus.Items.Clear()
            For varIntI = varCurYear - 2 To varCurYear + 2
                Dim varLst As New ListItem
                varLst.Value = varIntI
                varLst.Text = varIntI
                If varIntI = varIntYear Then
                    'varLst.Selected = True
                End If
                DropDownYearStatus.Items.Add(varLst)
            Next
            Dim varIntJ As Integer
            Dim varMEmptyLst As New ListItem
            varMEmptyLst.Value = ""
            varMEmptyLst.Text = "--Month--"
            'DropDownMonth.Items.Add(varMEmptyLst)
            DropDownMonthStatus.Items.Clear()
            For varIntJ = 1 To 12
                Dim varLst As New ListItem
                varLst.Value = varIntJ
                varLst.Text = MonthName(varIntJ)
                If varIntJ = varIntMonth Then
                    'varLst.Selected = True
                End If
                DropDownMonthStatus.Items.Add(varLst)
            Next
            FillTableStatus(varDtStartDate, varDtEndDate)
        Catch ex As Exception
            Response.Write(ex.Message)
            Response.End()
        End Try
    End Sub
    Protected Sub FillTableStatus(ByVal StartDate As Date, ByVal EndDate As Date)
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
        Dim varArrLeaveID(31) As String

        Dim clsLeave As ETS.BL.Leave
        Dim DSLeave As New Data.DataSet
        Dim objRecGetMonthlyLeaveStatus As Data.DataTableReader

        Try
            Dim varIntForLoop As Integer
            For varIntForLoop = 0 To 31
                varArrLeaveDate(varIntForLoop) = Nothing
                varArrLeaveStatus(varIntForLoop) = Nothing
                varArrDate(varIntForLoop) = Nothing
                varArrSignInStatus(varIntForLoop) = Nothing
                varArrSignOutStatus(varIntForLoop) = Nothing
                varArrLeaveID(varIntForLoop) = Nothing
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

            DropDownMonthStatus.SelectedIndex = -1
            DropDownYearStatus.SelectedIndex = -1

            DropDownMonthStatus.Items.FindByValue(varIntMonth).Selected = True
            DropDownYearStatus.Items.FindByValue(varIntYear).Selected = True

            clsLeave = New ETS.BL.Leave
            DSLeave = clsLeave.GetLeaveDetailsforMonthByUsr(Session("UserID").ToString, Month(StartDate), Month(StartDate), Year(StartDate), Year(StartDate), varDtTodayDate, Session("ContractorID").ToString)

            objRecGetMonthlyLeaveStatus = DSLeave.Tables(0).CreateDataReader


            If objRecGetMonthlyLeaveStatus.HasRows Then
                While objRecGetMonthlyLeaveStatus.Read
                    Dim varDtStDate As Date
                    Dim varDtEdDate As Date
                    Dim varStrLeaveID As String
                    Dim varStrLeaveStatus As String


                    varDtStDate = objRecGetMonthlyLeaveStatus.GetDateTime(objRecGetMonthlyLeaveStatus.GetOrdinal("StartDate"))
                    varDtEdDate = objRecGetMonthlyLeaveStatus.GetDateTime(objRecGetMonthlyLeaveStatus.GetOrdinal("EndDate"))
                    While varDtEdDate >= varDtStDate
                        If Month(varDtStDate) = Month(StartDate) And Year(varDtStDate) = Year(StartDate) Then
                            varStrLeaveStatus = "Leave " & objRecGetMonthlyLeaveStatus.GetString(objRecGetMonthlyLeaveStatus.GetOrdinal("Status"))
                            varStrLeaveStatus = "<font face=""Arial"" size=""2"">" & varStrLeaveStatus & "</font>"
                            varStrLeaveStatus = "<a href="""" OnClick=""window.open('LeaveApplicatiionLog.aspx?LeaveID=" & Trim(objRecGetMonthlyLeaveStatus.GetGuid(objRecGetMonthlyLeaveStatus.GetOrdinal("LeaveID")).ToString) & "','', 'width=650,height=440,status=1,scrollbars=1');return false;"" >" & varStrLeaveStatus & "</a>"

                            If varArrLeaveStatus(Day(varDtStDate)) <> "" Then
                                varArrLeaveStatus(Day(varDtStDate)) = varArrLeaveStatus(Day(varDtStDate)) & "," & varStrLeaveStatus
                            Else
                                varArrLeaveStatus(Day(varDtStDate)) = varStrLeaveStatus
                            End If

                            If varArrLeaveStatus(Day(varDtStDate)) <> "" Then
                                varArrLeaveDate(Day(varDtStDate)) = varDtStDate
                            End If
                            varArrLeaveID(Day(varDtStDate)) = objRecGetMonthlyLeaveStatus.GetGuid(objRecGetMonthlyLeaveStatus.GetOrdinal("LeaveID")).ToString

                        End If
                        varDtStDate = DateAdd(DateInterval.Day, 1, varDtStDate)
                    End While
                End While
            End If
            objRecGetMonthlyLeaveStatus.Close()

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
            'varTblRowMain.CssClass = "SMSelected"
            tblMainCalendarStatus.Rows.Add(varTblRowMain)

            varDtSDate = StartDate
            varDtEDate = EndDate
            Dim varWeekCount As Integer
            varWeekCount = 1
            lblMonthNameStatus.Text = MonthName(Month(varDtSDate)) & Year(varDtSDate)

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
                                varTblCellWeekDayName.Height = 70
                                varTblCellWeekDayName.VerticalAlign = VerticalAlign.Top
                                varTblCellWeekDayName.HorizontalAlign = HorizontalAlign.Left
                                varStrTemp = "<b><font face=""Arial"" size=""2"">" & Day(varDtSDate) & "</font></b>"
                                If DateDiff(DateInterval.Day, varDtSDate, varDtTempDate) = 0 Then
                                    If varArrLeaveStatus(Day(varDtSDate)) <> "" Then
                                        'varStrTemp = varStrTemp & "<BR>" & "<a href="""" OnClick=""window.open('LeaveApplicatiionLog.aspx?LeaveID=" & Trim(varArrLeaveID(Day(varDtSDate))) & "','', 'width=650,height=440,status=1,scrollbars=1');return false;"" >" & Trim(varArrLeaveStatus(Day(varDtSDate))) & "</a>"
                                        varStrTemp = varStrTemp & "<BR>" & Trim(varArrLeaveStatus(Day(varDtSDate)))
                                    Else
                                        varStrTemp = varStrTemp
                                    End If
                                    varTblCellWeekDayName.Text = varStrTemp
                                    If DateDiff(DateInterval.Day, CDate(varDtSDate), CDate(varDtTodayDate)) = 0 And Day(varDtSDate) = Day(varDtTodayDate) And Month(varDtSDate) = Month(varDtTodayDate) And Year(varDtSDate) = Year(varDtTodayDate) Then
                                        varTblCellWeekDayName.BackColor = Drawing.Color.AliceBlue
                                    End If
                                Else
                                    'varTblCellWeekDayName.Text = "<a href=LeaveApplicationLog.aspx?LeaveID=" & Trim(varArrLeaveID(Day(varDtSDate))) & ">" & varStrTemp & "</a>"
                                    varTblCellWeekDayName.Text = varStrTemp
                                End If
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
                    tblMainCalendarStatus.Rows.Add(varTblRowWeekDayName)
                    varWeekCount = varWeekCount + 1
                Else
                    Exit While
                End If
            End While
        Catch ex As Exception

        Finally
            clsLeave = Nothing
            objRecGetMonthlyLeaveStatus = Nothing
            DSLeave.Dispose()
        End Try
    End Sub
    Protected Sub btnGo_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnGo.Click
        Dim varIntYear As Long
        Dim varIntMonth As Integer
        varIntYear = 0
        varIntMonth = 0

        If DropDownYear.SelectedItem.Value.ToString <> "" Then
            varIntYear = DropDownYear.SelectedItem.Value.ToString
        End If
        If DropDownMonth.SelectedItem.Value.ToString <> "" Then
            varIntMonth = DropDownMonth.SelectedItem.Value.ToString
        End If

        'MsgBox(varIntYear)
        'MsgBox(varIntMonth)

        If varIntYear > 0 And varIntMonth > 0 Then
            Dim varDtStartDate As Date
            Dim varDtEndDate As Date
            Dim varDtTempDate As Date
            varDtStartDate = DateSerial(varIntYear, varIntMonth, 1)
            varDtTempDate = DateAdd(DateInterval.Month, 1, varDtStartDate)
            varDtEndDate = DateAdd(DateInterval.Day, -1, varDtTempDate)
            FillTable(varDtStartDate, varDtEndDate)
        Else

        End If
        LoadTableValuesStatus()
    End Sub
    Protected Sub BtnNext_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtnNext.Click
        Dim varStrMonthName As String
        Dim varIntMonth As Integer
        Dim varIntYear As Integer
        Dim varDtTempDate As Date
        Dim varDtTempDate1 As Date
        Dim varDtStartDate As Date
        Dim varDtEndDate As Date
        varStrMonthName = Left(lblMonthName.Text, Microsoft.VisualBasic.Len(lblMonthName.Text) - 4)
        varIntYear = Right(lblMonthName.Text, 4)
        varIntMonth = objMainModule.GetMonthFromMonthName(varStrMonthName)
        varDtTempDate = DateSerial(varIntYear, varIntMonth, 1)
        varDtStartDate = DateAdd(DateInterval.Month, 1, DateSerial(Year(varDtTempDate), Month(varDtTempDate), Day(varDtTempDate)))
        varDtTempDate1 = DateAdd(DateInterval.Month, 1, varDtStartDate)
        varDtEndDate = DateAdd(DateInterval.Day, -1, varDtTempDate1)
        FillTable(varDtStartDate, varDtEndDate)
        LoadTableValuesStatus()
    End Sub
    Protected Sub BtnPrev_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtnPrev.Click
        Dim varStrMonthName As String
        Dim varIntMonth As Integer
        Dim varIntYear As Integer
        Dim varDtTempDate As Date
        Dim varDtTempDate1 As Date
        Dim varDtStartDate As Date
        Dim varDtEndDate As Date
        varStrMonthName = Left(lblMonthName.Text, Microsoft.VisualBasic.Len(lblMonthName.Text) - 4)
        varIntYear = Right(lblMonthName.Text, 4)
        varIntMonth = objMainModule.GetMonthFromMonthName(varStrMonthName)
        varDtTempDate = DateSerial(varIntYear, varIntMonth, 1)
        varDtStartDate = DateAdd(DateInterval.Month, -1, DateSerial(Year(varDtTempDate), Month(varDtTempDate), Day(varDtTempDate)))
        varDtTempDate1 = DateAdd(DateInterval.Month, 1, varDtStartDate)
        varDtEndDate = DateAdd(DateInterval.Day, -1, varDtTempDate1)
        FillTable(varDtStartDate, varDtEndDate)
        LoadTableValuesStatus()
    End Sub
    Protected Sub btnGoStatus_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnGoStatus.Click
        Dim varIntYear As Long
        Dim varIntMonth As Integer
        varIntYear = 0
        varIntMonth = 0

        If DropDownYearStatus.SelectedItem.Value.ToString <> "" Then
            varIntYear = DropDownYearStatus.SelectedItem.Value.ToString
        End If
        If DropDownMonthStatus.SelectedItem.Value.ToString <> "" Then
            varIntMonth = DropDownMonthStatus.SelectedItem.Value.ToString
        End If

        If varIntYear > 0 And varIntMonth > 0 Then
            Dim varDtStartDate As Date
            Dim varDtEndDate As Date
            Dim varDtTempDate As Date
            varDtStartDate = DateSerial(varIntYear, varIntMonth, 1)
            varDtTempDate = DateAdd(DateInterval.Month, 1, varDtStartDate)
            varDtEndDate = DateAdd(DateInterval.Day, -1, varDtTempDate)
            FillTableStatus(varDtStartDate, varDtEndDate)
        Else

        End If
        LoadTableValues()
    End Sub
    Protected Sub BtnPrevStatus_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtnPrevStatus.Click
        Dim varStrMonthName As String
        Dim varIntMonth As Integer
        Dim varIntYear As Integer
        Dim varDtTempDate As Date
        Dim varDtTempDate1 As Date
        Dim varDtStartDate As Date
        Dim varDtEndDate As Date
        varStrMonthName = Left(lblMonthNameStatus.Text, Microsoft.VisualBasic.Len(lblMonthNameStatus.Text) - 4)
        varIntYear = Right(lblMonthNameStatus.Text, 4)
        varIntMonth = objMainModule.GetMonthFromMonthName(varStrMonthName)
        varDtTempDate = DateSerial(varIntYear, varIntMonth, 1)
        varDtStartDate = DateAdd(DateInterval.Month, -1, DateSerial(Year(varDtTempDate), Month(varDtTempDate), Day(varDtTempDate)))
        varDtTempDate1 = DateAdd(DateInterval.Month, 1, varDtStartDate)
        varDtEndDate = DateAdd(DateInterval.Day, -1, varDtTempDate1)
        FillTableStatus(varDtStartDate, varDtEndDate)
        LoadTableValues()
    End Sub
    Protected Sub BtnNextStatus_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtnNextStatus.Click
        Dim varStrMonthName As String
        Dim varIntMonth As Integer
        Dim varIntYear As Integer
        Dim varDtTempDate As Date
        Dim varDtTempDate1 As Date
        Dim varDtStartDate As Date
        Dim varDtEndDate As Date
        varStrMonthName = Left(lblMonthNameStatus.Text, Microsoft.VisualBasic.Len(lblMonthNameStatus.Text) - 4)
        varIntYear = Right(lblMonthNameStatus.Text, 4)
        varIntMonth = objMainModule.GetMonthFromMonthName(varStrMonthName)
        varDtTempDate = DateSerial(varIntYear, varIntMonth, 1)
        varDtStartDate = DateAdd(DateInterval.Month, 1, DateSerial(Year(varDtTempDate), Month(varDtTempDate), Day(varDtTempDate)))
        varDtTempDate1 = DateAdd(DateInterval.Month, 1, varDtStartDate)
        varDtEndDate = DateAdd(DateInterval.Day, -1, varDtTempDate1)
        FillTableStatus(varDtStartDate, varDtEndDate)
        LoadTableValues()
    End Sub
    Protected Sub EmpTabContainer_ActiveTabChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles EmpTabContainer.ActiveTabChanged
        'Response.Write(EmpTabContainer.ActiveTabIndex)
        'Response.End()
        If EmpTabContainer.ActiveTabIndex = 2 Then
            LoadTableValuesStatus()
        ElseIf EmpTabContainer.ActiveTabIndex = 0 Then
            LoadTableValues()
        End If
    End Sub
End Class
