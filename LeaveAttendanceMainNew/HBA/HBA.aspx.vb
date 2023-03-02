Imports MainModule
Partial Class LeaveAttendanceMainNew_HBA
    Inherits BasePage
    Dim objMainModule As New MainModule
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            LoadTableValues()
        End If
    End Sub
    Protected Sub FillTable(ByVal StartDate As Date, ByVal EndDate As Date)
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

        Dim objConn As New Data.SqlClient.SqlConnection
        objConn = objMainModule.OpenConnection(objConn)

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


            'varStrQuery = "SELECT StartDate, EndDate, Status FROM DBO.tblLeave WHERE UserID='" & Session("UserID") & "' AND (MONTH(StartDate) = " & Month(StartDate) & " OR MONTH(EndDate) =" & Month(StartDate) & ") AND (IsDeleted IS NULL OR IsDeleted='False')  ORDER BY StartDate "

            DropDownMonth.SelectedIndex = -1
            DropDownYear.SelectedIndex = -1

            DropDownMonth.Items.FindByValue(varIntMonth).Selected = True
            DropDownYear.Items.FindByValue(varIntYear).Selected = True

            clsLeave = New ETS.BL.Leave
            DSLeave = clsLeave.GetLeaveDetailsforMonthByUsr(Session("UserID").ToString, Month(StartDate), Month(StartDate), Year(StartDate), Year(StartDate), varDtTodayDate, Session("ContractorID").ToString)

            objRecGetMonthlyLeaveStatus = DSLeave.Tables(0).CreateDataReader


            'Dim objGetMonthlyLeaveStatus As New Data.SqlClient.SqlCommand(varStrQuery, objConn)
            'Dim objRecGetMonthlyLeaveStatus As Data.SqlClient.SqlDataReader = objGetMonthlyLeaveStatus.ExecuteReader()

            If objRecGetMonthlyLeaveStatus.HasRows Then
                While objRecGetMonthlyLeaveStatus.Read
                    Dim varDtStDate As Date
                    Dim varDtEdDate As Date
                    Dim varStrLeaveID As String
                    Dim varStrLeaveStatus As String


                    varDtStDate = objRecGetMonthlyLeaveStatus.GetDateTime(objRecGetMonthlyLeaveStatus.GetOrdinal("StartDate"))
                    varDtEdDate = objRecGetMonthlyLeaveStatus.GetDateTime(objRecGetMonthlyLeaveStatus.GetOrdinal("EndDate"))
                    While varDtEdDate >= varDtStDate

                        varStrLeaveStatus = "Leave " & objRecGetMonthlyLeaveStatus.GetString(objRecGetMonthlyLeaveStatus.GetOrdinal("Status"))
                        varStrLeaveStatus = "<font face=""Arial"" size=""2pt"">" & varStrLeaveStatus & "</font>"


                        If varArrLeaveStatus(Day(varDtStDate)) <> "" Then
                            varArrLeaveStatus(Day(varDtStDate)) = varArrLeaveStatus(Day(varDtStDate)) & "," & varStrLeaveStatus
                        Else
                            varArrLeaveStatus(Day(varDtStDate)) = varStrLeaveStatus
                        End If

                        If varArrLeaveStatus(Day(varDtStDate)) <> "" Then
                            varArrLeaveDate(Day(varDtStDate)) = varDtStDate
                        End If
                        varDtStDate = DateAdd(DateInterval.Day, 1, varDtStDate)
                    End While
                End While
            End If
            objRecGetMonthlyLeaveStatus.Close()
            'objRecGetMonthlyLeaveStatus = Nothing
            'objGetMonthlyLeaveStatus = Nothing

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
                                varTblCellWeekDayName.Font.Size = 10
                                varTblCellWeekDayName.Height = 70
                                varTblCellWeekDayName.VerticalAlign = VerticalAlign.Top
                                varTblCellWeekDayName.HorizontalAlign = HorizontalAlign.Left
                                varStrTemp = "<b><font face=""Arial"" size=""2pt"">" & Day(varDtSDate) & "</font></b>"
                                If DateDiff(DateInterval.Day, varDtSDate, varDtTempDate) = 0 Then
                                    If varArrLeaveStatus(Day(varDtSDate)) <> "" Then
                                        varStrTemp = varStrTemp & "<BR>" & Trim(varArrLeaveStatus(Day(varDtSDate)))
                                    Else
                                        varStrTemp = varStrTemp
                                    End If
                                    varTblCellWeekDayName.Text = varStrTemp
                                    If DateDiff(DateInterval.Day, CDate(varDtSDate), CDate(varDtTodayDate)) = 0 And Day(varDtSDate) = Day(varDtTodayDate) And Month(varDtSDate) = Month(varDtTodayDate) And Year(varDtSDate) = Year(varDtTodayDate) Then
                                        varTblCellWeekDayName.BackColor = Drawing.Color.AliceBlue
                                    End If
                                Else
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
                    tblMainCalendar.Rows.Add(varTblRowWeekDayName)
                    varWeekCount = varWeekCount + 1
                Else
                    Exit While
                End If
            End While
        Catch ex As Exception
        Finally
            'If objConn.State <> Data.ConnectionState.Closed Then
            '    objConn.Close()
            '    objConn = Nothing
            'End If
            objRecGetMonthlyLeaveStatus = Nothing
            DSLeave.Dispose()
            clsLeave = Nothing
        End Try
    End Sub
    Protected Sub BtnNext_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtnNext.Click
        Try
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
        Catch ex As Exception
        End Try
    End Sub
    Protected Sub BtnPrev_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtnPrev.Click
        Try
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
        Catch ex As Exception
        End Try
    End Sub
    Protected Sub btnGo_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnGo.Click

        Try
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
        Catch ex As Exception

        End Try


    End Sub
    Protected Sub SendLR_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles SendLR.Click
        Dim varDtReqSDate As Date
        Dim varDtReqEDate As Date
        Dim varFlag As Boolean
        varDtReqSDate = txtStartDate.Text.ToString
        varDtReqEDate = txtEndDate.Text.ToString
        Dim clsLeave As ETS.BL.Leave
        Try
            clsLeave = New ETS.BL.Leave
            clsLeave.UserID = Session("UserID").ToString
            clsLeave.ContractorID = Session("ContractorID").ToString
            clsLeave.StartDate = varDtReqSDate
            clsLeave.EndDate = varDtReqEDate
            clsLeave.Status = "Approved"
            clsLeave.Reason = Replace(TextArea1.InnerText.ToString, "'", "''")

            Dim varReturn As String = String.Empty
            varReturn = clsLeave.btn_HBASendLeaveRequest()
            Response.Write(varReturn)

            If Not String.IsNullOrEmpty(varReturn) Then
                Dim varTemp() As String
                Dim varStrTemp As String = String.Empty
                varTemp = Split(varReturn.ToString, "<BR>")
                If Trim(UCase(varTemp(0))) = Trim(UCase("True")) Then
                    Response.Write("<script type=""text/javascript"" language=javascript> alert(""Leave Register Sucessfully!!!"");window.location.href='HBA.aspx';</script>")
                Else
                    Response.Write("<script type=""text/javascript"" language=javascript> alert(""Leave Registion failed"");window.location.href='HBA.aspx';</script>")
                End If
            End If

        Catch ex As Exception
            Response.Write(ex.Message)
        Finally
            clsLeave = Nothing
        End Try
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
            Response.End()
        End Try
    End Sub
    Protected Sub HBATabContainer_ActiveTabChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles HBATabContainer.ActiveTabChanged
        If HBATabContainer.ActiveTabIndex = 2 Then
            LoadTableValues()
        End If
    End Sub
End Class
