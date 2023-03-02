Imports MainModule
Partial Class LeaveAttendanceMainNew_Employee_Employee
    Inherits BasePage
    Dim objMainModule As New MainModule
    Dim varStrState As String
    Dim varStrDeptID As String
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Request.QueryString("Dt") <> "" And Trim(UCase(Request.QueryString("Opr"))) = Trim(UCase("SignIn")) Then
            Dim varDtDate As Date
            Dim varStrInsertstr As String
            Dim varStrLeaveType As String
            Dim varStrState As String
            Dim varStrDeptID As String
            Dim AttDate As Date
            Dim LBS As String

            Dim objConn As New Data.SqlClient.SqlConnection
            objConn = objMainModule.OpenConnection(objConn)

            Try
                varDtDate = Request.QueryString("Dt")

                'Check for DayLight Settings between local time and server time 
                'Then add the respective time in date to get correct date

                If objMainModule.CheckDayLightSavings(Now()) Then
                    AttDate = DateAdd(DateInterval.Hour, 9, Now())
                    AttDate = DateAdd(DateInterval.Minute, 30, AttDate)
                Else
                    AttDate = DateAdd(DateInterval.Hour, 10, Now())
                    AttDate = DateAdd(DateInterval.Minute, 30, AttDate)
                End If

                'Check user on half day or not 
                Dim oCommand As New Data.SqlClient.SqlCommand("SELECT TypeOfLeave FROM DBO.tblLeave WHERE UserID='" & Session("UserID") & "' AND StartDate <= '" & DateSerial(Year(AttDate), Month(AttDate), Day(AttDate)) & "' AND EndDate >= '" & DateSerial(Year(AttDate), Month(AttDate), Day(AttDate)) & "' AND Status ='Approved'", objConn)
                Dim oRec As Data.SqlClient.SqlDataReader = oCommand.ExecuteReader()
                If oRec.HasRows Then
                    While oRec.Read
                        If Not oRec.IsDBNull(oRec.GetOrdinal("TypeOfLeave")) Then
                            varStrLeaveType = oRec.GetString(oRec.GetOrdinal("TypeOfLeave"))
                        End If
                    End While
                End If
                oRec.Close()
                oRec = Nothing
                oCommand = Nothing
                'end check half day

                'Check if employee is on half day leave or not if yes then set Status HP otherwise P.

                If Trim(UCase(varStrLeaveType)) = Trim(UCase("HL")) Or Trim(UCase(varStrLeaveType)) = Trim(UCase("LWPHL")) Or Trim(UCase(varStrLeaveType)) = Trim(UCase("AHL")) Then
                    varStrInsertstr = "INSERT INTO tblAttendance (UserID,SignIn,AttDate,StatusFlag,Status) VALUES('" & Session("UserID") & "','" & AttDate & "','" & DateSerial(Year(AttDate), Month(AttDate), Day(AttDate)) & "','IN','HP') "
                Else
                    varStrInsertstr = "INSERT INTO tblAttendance (UserID,SignIn,AttDate,StatusFlag,Status) VALUES('" & Session("UserID") & "','" & AttDate & "','" & DateSerial(Year(AttDate), Month(AttDate), Day(AttDate)) & "','IN','P') "
                End If
                'End If

                Dim InsertCmd As New Data.SqlClient.SqlCommand
                InsertCmd.CommandType = Data.CommandType.Text
                InsertCmd.CommandText = varStrInsertstr
                InsertCmd.Connection = objConn
                InsertCmd.ExecuteNonQuery()
                InsertCmd = Nothing

                'Check State of User for adding compoff for national holiday.
                Dim cmdCheckState As New Data.SqlClient.SqlCommand("SELECT State,DepartmentID FROM DBO.tblUsers WHERE UserID='" & Session("UserID") & "' AND ContractorID='" & Session("ContractorID").ToString & "'  ", objConn)
                Dim RecCheckState As Data.SqlClient.SqlDataReader = cmdCheckState.ExecuteReader()

                If RecCheckState.HasRows Then
                    While RecCheckState.Read
                        If Not RecCheckState.IsDBNull(RecCheckState.GetOrdinal("State")) Then
                            varStrState = RecCheckState.GetString(RecCheckState.GetOrdinal("State"))
                            varStrDeptID = RecCheckState.GetGuid(RecCheckState.GetOrdinal("DepartmentID")).ToString
                        End If
                    End While
                End If

                RecCheckState.Close()
                RecCheckState = Nothing
                cmdCheckState = Nothing

                If objMainModule.CheckOffDayAttendance(AttDate, varStrDeptID, varStrState) Then
                    LBS = "SELECT CL,EL,TL FROM DBO.tblLeaveBalance WHERE UserID='" & Session("UserID") & "'"
                    Dim cmdLBS As New Data.SqlClient.SqlCommand(LBS, objConn)
                    Dim RecLBS As Data.SqlClient.SqlDataReader = cmdLBS.ExecuteReader()

                    If RecLBS.HasRows Then
                        Dim varDblEL As Double
                        Dim varDblTL As Double
                        While RecLBS.Read

                            If Trim(UCase(varStrLeaveType)) = Trim(UCase("HL")) Or Trim(UCase(varStrLeaveType)) = Trim(UCase("AHL")) Or Trim(UCase(varStrLeaveType)) = Trim(UCase("LWPHL")) Then
                                varDblEL = RecLBS.GetDouble(RecLBS.GetOrdinal("EL")) + 1
                                varDblTL = RecLBS.GetDouble(RecLBS.GetOrdinal("TL")) + 1
                            Else
                                varDblEL = RecLBS.GetDouble(RecLBS.GetOrdinal("EL")) + 2
                                varDblTL = RecLBS.GetDouble(RecLBS.GetOrdinal("TL")) + 2
                            End If

                            Dim UpdateLeaveCmd As New Data.SqlClient.SqlCommand
                            UpdateLeaveCmd.CommandType = Data.CommandType.Text
                            UpdateLeaveCmd.CommandText = "UPDATE tblLeaveBalance SET EL=" & varDblEL & ",TL =" & varDblTL & " WHERE UserID='" & Session("UserID") & "'"
                            UpdateLeaveCmd.Connection = objConn
                            UpdateLeaveCmd.ExecuteNonQuery()
                            UpdateLeaveCmd = Nothing

                        End While
                    End If

                    RecLBS.Close()
                    RecLBS = Nothing
                    cmdLBS = Nothing
                End If
                'end national holiday checking

                'Code for leave carry forward in next year 
                Dim QS As String
                QS = "SELECT * FROM DBO.tblLeaveStatus WHERE UpdateYear='" & Year(AttDate) & "'"

                Dim cmdCheckCarryForward As New Data.SqlClient.SqlCommand(QS, objConn)
                Dim RecCheckCarryForward As Data.SqlClient.SqlDataReader = cmdCheckCarryForward.ExecuteReader()
                Dim varBolCheckLS As Boolean
                varBolCheckLS = False

                If RecCheckCarryForward.HasRows Then
                    While RecCheckCarryForward.Read
                        varBolCheckLS = True
                    End While
                End If

                RecCheckCarryForward.Close()
                RecCheckCarryForward = Nothing
                cmdCheckCarryForward = Nothing

                If Not varBolCheckLS Then
                    Dim varUserID As New ArrayList
                    Dim varCL As New ArrayList
                    Dim varEL As New ArrayList
                    Dim varTL As New ArrayList

                    'QS = "SELECT UserID,CL,EL,TL FROM DBO.tblLeaveBalance"
                    QS = " SELECT L.UserID,CL,EL,TL FROM DBO.tblLeaveBalance L INNER JOIN DBO.tblUsers U ON L.UserID=U.UserID WHERE U.ContractorID='" & Session("ContractorID").ToString & "' "

                    Dim cmdCarryForward As New Data.SqlClient.SqlCommand(QS, objConn)
                    Dim RecCarryForward As Data.SqlClient.SqlDataReader = cmdCarryForward.ExecuteReader()

                    If RecCarryForward.HasRows And Day(AttDate) = "1" And Month(AttDate) = "1" Then
                        Dim varDblEL As Double
                        Dim varDblTL As Double
                        Dim varDblCL As Double

                        While RecCarryForward.Read
                            If RecCarryForward.GetDouble(RecCarryForward.GetOrdinal("CL")) > 0 Then
                                varDblCL = 0
                            Else
                                varDblCL = RecCarryForward.GetDouble(RecCarryForward.GetOrdinal("CL"))
                            End If

                            If RecCarryForward.GetDouble(RecCarryForward.GetOrdinal("EL")) > 42 Then
                                varDblEL = 42
                            Else
                                varDblEL = RecCarryForward.GetDouble(RecCarryForward.GetOrdinal("EL"))
                            End If

                            varDblTL = varDblCL + varDblEL

                            varUserID.Add(RecCarryForward.GetGuid(RecCarryForward.GetOrdinal("UserID")).ToString)
                            varCL.Add(varDblCL.ToString)
                            varEL.Add(varDblEL.ToString)
                            varTL.Add(varDblTL.ToString)

                        End While

                        Dim InsertLSCmd As New Data.SqlClient.SqlCommand
                        InsertLSCmd.CommandType = Data.CommandType.Text
                        InsertLSCmd.CommandText = "INSERT INTO DBO.tblLeaveStatus(UpdateYear,UpdateLeave) VALUES('" & Year(AttDate) & "',1) "
                        InsertLSCmd.Connection = objConn
                        InsertLSCmd.ExecuteNonQuery()
                        InsertLSCmd = Nothing
                    End If
                    RecCarryForward.Close()
                    RecCarryForward = Nothing
                    cmdCarryForward = Nothing

                    Dim i As Int16

                    For i = 0 To varUserID.Count - 1
                        Dim UpdateLBLCmd As New Data.SqlClient.SqlCommand
                        UpdateLBLCmd.CommandType = Data.CommandType.Text
                        UpdateLBLCmd.CommandText = "UPDATE tblLeaveBalance SET EL=" & CDbl(varEL(i)) & ",TL =" & CDbl(varTL(i)) & ",CL=" & CDbl(varCL(i)) & " WHERE UserID='" & varUserID(i) & "'"
                        UpdateLBLCmd.Connection = objConn
                        UpdateLBLCmd.ExecuteNonQuery()
                        UpdateLBLCmd = Nothing
                    Next
                End If

                'Check Date if 1st day of quater then add respetive leaves.
                Dim varBolChkLQ As Boolean
                Dim varAddCL As Double
                Dim varAddEL As Double

                Dim varUserIDQU As New ArrayList
                Dim varCLQU As New ArrayList
                Dim varELQU As New ArrayList
                Dim varTLQU As New ArrayList

                varBolChkLQ = False
                varAddCL = 0
                varAddEL = 0

                QS = "SELECT * FROM DBO.tblLeaveStatusQuater WHERE QuaterDate='" & Request.QueryString("Dt") & "'"
                Dim objChkLQ As New Data.SqlClient.SqlCommand(QS, objConn)
                Dim objRecChkLQ As Data.SqlClient.SqlDataReader = objChkLQ.ExecuteReader
                If objRecChkLQ.HasRows Then
                    While objRecChkLQ.Read
                        varBolChkLQ = True
                    End While
                End If
                objRecChkLQ.Close()
                objRecChkLQ = Nothing
                objChkLQ = Nothing

                If Not varBolChkLQ Then
                    Dim objChkDate As New Data.SqlClient.SqlCommand("SELECT CL EL FROM DBO.tblLeavePolicy WHERE Day=" & Day(Request.QueryString("Dt")) & " AND Month='" & MonthName(Month(Request.QueryString("Dt"))) & "'", objConn)
                    Dim objRecChkDate As Data.SqlClient.SqlDataReader = objChkDate.ExecuteReader
                    If objRecChkDate.HasRows Then
                        While objRecChkDate.Read
                            If Not objRecChkDate.IsDBNull(objRecChkDate.GetOrdinal("CL")) Then
                                varAddCL = objRecChkDate.GetDouble(objRecChkDate.GetOrdinal("CL"))
                            End If
                            If Not objRecChkDate.IsDBNull(objRecChkDate.GetOrdinal("EL")) Then
                                varAddEL = objRecChkDate.GetDouble(objRecChkDate.GetOrdinal("EL"))
                            End If
                        End While
                    End If
                    objRecChkDate.Close()
                    objRecChkDate = Nothing
                    objChkDate = Nothing

                    If varAddCL > 0 And varAddEL > 0 Then
                        'QS = "SELECT UserID,CL,EL,TL FROM DBO.tblLeaveBalance"
                        QS = "SELECT L.UserID,CL,EL,TL FROM DBO.tblLeaveBalance L INNER JOIN DBO.tblUsers U ON L.UserID=U.UserID WHERE U.ContractorID='" & Session("ContractorID").ToString & "'"
                        Dim cmdCarryForwardQU As New Data.SqlClient.SqlCommand(QS, objConn)
                        Dim RecCarryForwardQU As Data.SqlClient.SqlDataReader = cmdCarryForwardQU.ExecuteReader()

                        If RecCarryForwardQU.HasRows Then
                            Dim varDblELQU As Double
                            Dim varDblTLQU As Double
                            Dim varDblCLQU As Double

                            While RecCarryForwardQU.Read
                                varDblCLQU = RecCarryForwardQU.GetDouble(RecCarryForwardQU.GetOrdinal("CL")) + varAddCL
                                varDblELQU = RecCarryForwardQU.GetDouble(RecCarryForwardQU.GetOrdinal("EL")) + varAddEL
                                varDblTLQU = varDblCLQU + varDblELQU

                                varUserIDQU.Add(RecCarryForwardQU.GetGuid(RecCarryForwardQU.GetOrdinal("UserID")).ToString)
                                varCLQU.Add(varDblCLQU.ToString)
                                varELQU.Add(varDblELQU.ToString)
                                varTLQU.Add(varDblTLQU.ToString)
                            End While

                            RecCarryForwardQU.Close()
                            RecCarryForwardQU = Nothing
                            cmdCarryForwardQU = Nothing

                            Dim InsertLSCmd As New Data.SqlClient.SqlCommand
                            InsertLSCmd.CommandType = Data.CommandType.Text
                            InsertLSCmd.CommandText = "INSERT INTO DBO.tblLeaveStatusQuater(QuaterDate,UpdateLeave) VALUES('" & Request.QueryString("Dt") & "',1) "
                            InsertLSCmd.Connection = objConn
                            InsertLSCmd.ExecuteNonQuery()
                            InsertLSCmd = Nothing


                        End If

                        Dim j As Int16

                        For j = 0 To varUserIDQU.Count - 1
                            Dim UpdateLBLCmd As New Data.SqlClient.SqlCommand
                            UpdateLBLCmd.CommandType = Data.CommandType.Text
                            UpdateLBLCmd.CommandText = "UPDATE tblLeaveBalance SET EL=" & CDbl(varELQU(j)) & ",TL =" & CDbl(varTLQU(j)) & ",CL=" & CDbl(varCLQU(j)) & " WHERE UserID='" & varUserIDQU(j) & "'"
                            UpdateLBLCmd.Connection = objConn
                            UpdateLBLCmd.ExecuteNonQuery()
                            UpdateLBLCmd = Nothing
                        Next
                    End If
                End If
            Catch ex As Exception
            Finally
                If objConn.State <> Data.ConnectionState.Closed Then
                    objConn.Close()
                    objConn = Nothing
                End If
            End Try
        End If

        If Request.QueryString("Dt") <> "" And Trim(UCase(Request.QueryString("Opr"))) = Trim(UCase("SignOut")) Then
            Dim varStrUpdatestr
            Dim AttDate

            Dim objConn As New Data.SqlClient.SqlConnection
            objConn = objMainModule.OpenConnection(objConn)

            Try
                'Check for DayLight Settings between local time and server time 
                'Then add the respective time in date to get correct date

                If objMainModule.CheckDayLightSavings(Now()) Then
                    AttDate = DateAdd(DateInterval.Hour, 9, Now())
                    AttDate = DateAdd(DateInterval.Minute, 30, AttDate)
                Else
                    AttDate = DateAdd(DateInterval.Hour, 10, Now())
                    AttDate = DateAdd(DateInterval.Minute, 30, AttDate)
                End If
                varStrUpdatestr = "UPDATE DBO.tblAttendance SET SignOut='" & AttDate & "',StatusFlag='OUT' WHERE UserID = '" & Session("UserID") & "' AND StatusFlag='IN' AND AttDate='" & Request.QueryString("Dt") & "'"

                Dim UpdateCmd As New Data.SqlClient.SqlCommand
                UpdateCmd.CommandType = Data.CommandType.Text
                UpdateCmd.CommandText = varStrUpdatestr
                UpdateCmd.Connection = objConn
                UpdateCmd.ExecuteNonQuery()
                UpdateCmd = Nothing
            Catch ex As Exception
            Finally
                If objConn.State <> Data.ConnectionState.Closed Then
                    objConn.Close()
                    objConn = Nothing
                End If
            End Try
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
            varIntMonth = Month(Now())
            varIntYear = Year(Now())
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

        Dim objConn As New Data.SqlClient.SqlConnection
        objConn = objMainModule.OpenConnection(objConn)

        Try
            Dim varIntForLoop As Integer
            For varIntForLoop = 0 To 31
                varArrLeaveStatus(varIntForLoop) = Nothing
                varArrDate(varIntForLoop) = Nothing
                varArrSignInStatus(varIntForLoop) = Nothing
                varArrSignOutStatus(varIntForLoop) = Nothing
            Next
            Dim i As Integer
            If objMainModule.CheckDayLightSavings(Now()) = True Then
                varDtTodayDate = DateAdd(DateInterval.Hour, 9, Now())
                varDtTodayDate = DateAdd(DateInterval.Minute, 30, varDtTodayDate)
            Else
                varDtTodayDate = DateAdd(DateInterval.Hour, 10, Now())
                varDtTodayDate = DateAdd(DateInterval.Minute, 30, varDtTodayDate)
            End If
            Dim objGetMonthlyLeaveStatus As New Data.SqlClient.SqlCommand("SELECT TypeOfLeave, StartDate, EndDate, Status FROM DBO.tblLeave WHERE UserID='" & Session("UserID") & "' AND MONTH(StartDate) = " & Month(StartDate) & " OR MONTH(EndDate) =" & Month(StartDate) & " AND (IsDeleted <>'True' or IsDeleted IS NULL)  ORDER BY StartDate ", objConn)
            Dim objRecGetMonthlyLeaveStatus As Data.SqlClient.SqlDataReader = objGetMonthlyLeaveStatus.ExecuteReader()

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

                    If Trim(UCase(objRecGetMonthlyLeaveStatus.GetString(objRecGetMonthlyLeaveStatus.GetOrdinal("Status")))) = Trim(UCase("Pending")) Then
                        varStrLeaveStatus = "Leave application pending"
                    Else
                        If Not objRecGetMonthlyLeaveStatus.IsDBNull(objRecGetMonthlyLeaveStatus.GetOrdinal("TypeOfLeave")) Then
                            varStrLeaveStatus = objRecGetMonthlyLeaveStatus.GetString(objRecGetMonthlyLeaveStatus.GetOrdinal("TypeOfLeave"))
                        Else
                            varStrLeaveStatus = ""
                        End If
                    End If

                    varDtStDate = objRecGetMonthlyLeaveStatus.GetDateTime(objRecGetMonthlyLeaveStatus.GetOrdinal("StartDate"))
                    varDtEdDate = objRecGetMonthlyLeaveStatus.GetDateTime(objRecGetMonthlyLeaveStatus.GetOrdinal("EndDate"))
                    While varDtEdDate >= varDtStDate
                        varArrLeaveStatus(Day(varDtStDate)) = varStrLeaveStatus
                        varDtStDate = DateAdd(DateInterval.Day, 1, varDtStDate)
                    End While
                End While
            End If
            objRecGetMonthlyLeaveStatus.Close()
            objRecGetMonthlyLeaveStatus = Nothing
            objGetMonthlyLeaveStatus = Nothing
            Dim objGetAttendanceStatus As New Data.SqlClient.SqlCommand("SELECT SignIn,SignOut,AttDate,Status FROM DBO.tblAttendance WHERE UserID='" & Session("UserID") & "' AND Month(AttDate)=" & Month(StartDate) & " ORDER BY AttDate ", objConn)
            Dim objRecGetAttendanceStatus As Data.SqlClient.SqlDataReader = objGetAttendanceStatus.ExecuteReader()
            If objRecGetAttendanceStatus.HasRows Then
                While objRecGetAttendanceStatus.Read
                    Dim varStrSplit As Date
                    Dim varStrSplit1 As Date
                    If Not objRecGetAttendanceStatus.IsDBNull(objRecGetAttendanceStatus.GetOrdinal("AttDate")) Then
                        If Trim(UCase(objRecGetAttendanceStatus.GetString(objRecGetAttendanceStatus.GetOrdinal("Status")))) = Trim(UCase("P")) Then
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
            objRecGetAttendanceStatus = Nothing
            objGetAttendanceStatus = Nothing

            For i = 1 To 7
                Dim varStrDayName As String
                Dim varTblCellDayName As New TableCell
                varStrDayName = WeekdayName(i)
                varTblCellDayName.Width = 120
                varTblCellDayName.HorizontalAlign = HorizontalAlign.Center
                varTblCellDayName.Text = varStrDayName
                varTblRowMain.Cells.Add(varTblCellDayName)
            Next
            varTblRowMain.CssClass = "SMSelected"
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
                                varTblCellWeekDayName.Font.Name = "Trebuchet MS"
                                varTblCellWeekDayName.Font.Size = 10
                                varTblCellWeekDayName.Height = 80
                                varTblCellWeekDayName.VerticalAlign = VerticalAlign.Top
                                varTblCellWeekDayName.HorizontalAlign = HorizontalAlign.Left
                                If varArrSignInStatus(Day(varDtSDate)) <> "" Then
                                    If varArrSignOutStatus(Day(varDtSDate)) <> "" Then
                                        varStrTemp = Day(varDtSDate) & "<BR><table><tr><td align=right>In:</td><td align=left>" & varArrSignInStatus(Day(varDtSDate)) & "</td></tr><tr><td align=right>Out:</td><td align=right>" & varArrSignOutStatus(Day(varDtSDate)) & "</td></tr></table>"
                                    Else
                                        If DateDiff(DateInterval.Day, CDate(varDtSDate), CDate(varDtTodayDate)) = 0 And Day(varDtSDate) = Day(varDtTodayDate) And Month(varDtSDate) = Month(varDtTodayDate) And Year(varDtSDate) = Year(varDtTodayDate) Then
                                            varStrTemp = Day(varDtSDate) & "<BR><table><tr><td align=right>In:</td><td align=left>" & varArrSignInStatus(Day(varDtSDate)) & "</td></tr><tr><td><a href=Employee.aspx?Opr=SignOut&Dt=" & varDtSDate & ">Out</a></td></tr></table>"
                                        Else
                                            varStrTemp = Day(varDtSDate) & "<BR><table><tr><td align=right>In:</td><td align=left>" & varArrSignInStatus(Day(varDtSDate)) & "</td></tr></table>"
                                        End If
                                    End If
                                ElseIf varArrLeaveStatus(Day(varDtSDate)) <> "" Then
                                    varStrTemp = Day(varDtSDate) & "<BR>" & varArrLeaveStatus(Day(varDtSDate))
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
            If objConn.State <> Data.ConnectionState.Closed Then
                objConn.Close()
                objConn = Nothing
            End If
        End Try
    End Sub
    Private Sub LoadTableValuesStatus()
        Try
            Dim varDtStartDate As Date
            Dim varDtEndDate As Date
            Dim varDtTempDate As Date
            Dim varIntMonth As Integer
            Dim varIntYear As Integer
            varIntMonth = Month(Now())
            varIntYear = Year(Now())
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

        Dim objConn As New Data.SqlClient.SqlConnection
        objConn = objMainModule.OpenConnection(objConn)

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

            If objMainModule.CheckDayLightSavings(Now()) = True Then
                varDtTodayDate = DateAdd(DateInterval.Hour, 9, Now())
                varDtTodayDate = DateAdd(DateInterval.Minute, 30, varDtTodayDate)
            Else
                varDtTodayDate = DateAdd(DateInterval.Hour, 10, Now())
                varDtTodayDate = DateAdd(DateInterval.Minute, 30, varDtTodayDate)
            End If
            Dim varIntMonth As Integer
            varIntMonth = Month(StartDate)
            Dim varIntYear As Integer
            varIntYear = Year(StartDate)

            DropDownMonthStatus.SelectedIndex = -1
            DropDownYearStatus.SelectedIndex = -1

            DropDownMonthStatus.Items.FindByValue(varIntMonth).Selected = True
            DropDownYearStatus.Items.FindByValue(varIntYear).Selected = True

            varStrQuery = "SELECT StartDate, EndDate, Status FROM DBO.tblLeave WHERE UserID='" & Session("UserID") & "' AND (MONTH(StartDate) = " & Month(StartDate) & " OR MONTH(EndDate) =" & Month(StartDate) & ") AND (IsDeleted IS NULL OR IsDeleted='False')  ORDER BY StartDate "
            Dim objGetMonthlyLeaveStatus As New Data.SqlClient.SqlCommand(varStrQuery, objConn)
            Dim objRecGetMonthlyLeaveStatus As Data.SqlClient.SqlDataReader = objGetMonthlyLeaveStatus.ExecuteReader()

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
                        varStrLeaveStatus = "<font face=""Trebuchet MS"" size=""2"">" & varStrLeaveStatus & "</font>"


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
            objRecGetMonthlyLeaveStatus = Nothing
            objGetMonthlyLeaveStatus = Nothing

            For i = 1 To 7
                Dim varStrDayName As String
                Dim varTblCellDayName As New TableCell
                varStrDayName = WeekdayName(i)
                varTblCellDayName.Width = 120
                varTblCellDayName.HorizontalAlign = HorizontalAlign.Center
                varTblCellDayName.Text = varStrDayName
                varTblRowMain.Cells.Add(varTblCellDayName)
            Next
            varTblRowMain.CssClass = "SMSelected"
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
                                varTblCellWeekDayName.Font.Name = "Trebuchet MS"
                                varTblCellWeekDayName.Font.Size = 10
                                varTblCellWeekDayName.Height = 70
                                varTblCellWeekDayName.VerticalAlign = VerticalAlign.Top
                                varTblCellWeekDayName.HorizontalAlign = HorizontalAlign.Left
                                varStrTemp = "<b><font face=""Trebuchet MS"" size=""2"">" & Day(varDtSDate) & "</font></b>"
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
                    tblMainCalendarStatus.Rows.Add(varTblRowWeekDayName)
                    varWeekCount = varWeekCount + 1
                Else
                    Exit While
                End If
            End While
        Catch ex As Exception
            If objConn.State <> Data.ConnectionState.Closed Then
                objConn.Close()
                objConn = Nothing
            End If
        End Try
    End Sub
    Protected Sub btnGo_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnGo.Click
        Dim varIntYear As Long
        Dim varIntMonth As Integer
        varIntYear = 0
        varIntMonth = 0

        Try
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
        Catch ex As Exception
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
            LoadTableValuesStatus()
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
            LoadTableValuesStatus()
        Catch ex As Exception
        End Try
    End Sub
    Protected Sub btnGoStatus_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnGoStatus.Click
        Dim varIntYear As Long
        Dim varIntMonth As Integer
        varIntYear = 0
        varIntMonth = 0

        Try
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
        Catch ex As Exception

        End Try


    End Sub
    Protected Sub BtnPrevStatus_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtnPrevStatus.Click
        Try
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
        Catch ex As Exception
        End Try
    End Sub
    Protected Sub BtnNextStatus_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtnNextStatus.Click
        Try
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
        Catch ex As Exception
        End Try
    End Sub
    Protected Sub EmpTabContainer_ActiveTabChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles EmpTabContainer.ActiveTabChanged
        Try
            If EmpTabContainer.ActiveTabIndex = 2 Then
                LoadTableValuesStatus()
            ElseIf EmpTabContainer.ActiveTabIndex = 0 Then
                LoadTableValues()
            End If
        Catch ex As Exception
        End Try
    End Sub
End Class
