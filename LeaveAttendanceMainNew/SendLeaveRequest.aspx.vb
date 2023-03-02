Imports MainModule
Partial Class SendLeaveRequest
    Inherits BasePage
    Dim objMainModule As New MainModule
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim varStrSQLQuery As String
        Dim varStrEmpName As String
        Dim varDateTodaydate As Date
        Dim varBolCheckDayLight As Boolean
        Dim varStrPri
        Dim varStrSec
        Dim varStrThi
        Dim varDtStartDate As Date
        Dim varDtEndDate As Date
        Dim varToMail
        Dim varSuperID
        Dim varBolWhileFlag As Boolean
        Dim varStrPMailID As String
        Dim varStrSMailID As String
        Dim varStrTMailID As String
        Dim varStrInsert As String
        Dim varStrDeptMail As String
        Dim varStrEmpMailCC As String
        Dim objMainModule As New MainModule
        Dim varDblCL As Double
        Dim varDblEL As Double
        Dim varDblTL As Double
        Dim varDblML As Double
        Dim varDblLWP As Double

        Dim varBDblCL As Double
        Dim varBDblEL As Double
        Dim varBDblTL As Double
        Dim varBDblML As Double
        Dim varBDblLWP As Double


        Dim varStrWOff1 As String
        Dim varStrWOff2 As String
        Dim varStrDtFrom As String
        Dim varStrDtTo As String
        Dim varStrReason As String
        Dim varStrLeaveType As String
        Dim varStrEmpState As String
        Dim varStrEmpDeptID As String
        Dim varDblDedcutCount As Double
        Dim varDblLeaveCount As Double
        Dim varBolAttCheck As Boolean
        Dim varprevCheckDateDayName As String
        Dim varnextCheckDateDayName As String
        Dim varStrWOffCheckDateCount As Integer
        Dim varIntAdjustWOffCount As Integer
        Dim varStrRefLeaveID As String
        Dim varIntLWPCount As Integer
        Dim varBolALFlag As Boolean
        Dim d
        Dim varDblLeaveBalLog As Double
        Dim varDblLeaveBalLogA As Double
        'Response.Write(DateDiff(DateInterval.Day, DateSerial(2007, 12, 29), DateSerial(Year(Now.Date), Month(Now.Date), Day(Now.Date))))
        'Response.End()

        Dim objConn As New Data.SqlClient.SqlConnection
        objConn = objMainModule.OpenConnection(objConn)

        Try
            'Session("UserID") = "9d9c3486-2d79-41f7-a4a4-2a5ddb4cecbe"
            varBolCheckDayLight = objMainModule.CheckDayLightSavings(Now())
            If varBolCheckDayLight = True Then
                varDateTodaydate = DateAdd(DateInterval.Hour, 9, Now())
                varDateTodaydate = DateAdd(DateInterval.Minute, 30, varDateTodaydate)
            Else
                varDateTodaydate = DateAdd(DateInterval.Hour, 10, Now())
                varDateTodaydate = DateAdd(DateInterval.Minute, 30, varDateTodaydate)
            End If

            Dim varTempDtStart As Date
            Dim varTempDtEnd As Date


            varStrDtFrom = DateSerial(Year(Request("txtStartDate")), Month(Request("txtStartDate")), Day(Request("txtStartDate")))
            varStrDtTo = DateSerial(Year(Request("txtEndDate")), Month(Request("txtEndDate")), Day(Request("txtEndDate")))
            varTempDtStart = DateSerial(Year(Request("txtStartDate")), Month(Request("txtStartDate")), Day(Request("txtStartDate")))
            varTempDtEnd = DateSerial(Year(Request("txtEndDate")), Month(Request("txtEndDate")), Day(Request("txtEndDate")))

            varStrReason = Request("textArea1")

            Dim oCommandDeptMail As New Data.SqlClient.SqlCommand("SELECT Name FROM DBO.tblDepartments WHERE DepartmentID=(SELECT DepartmentID FROM DBO.tblUsers WHERE UserID='" & Session("UserID") & "' AND ContractorID='" & Session("ContractorID").ToString & "' )", objConn)
            Dim oRecDeptMail As Data.SqlClient.SqlDataReader = oCommandDeptMail.ExecuteReader()

            If oRecDeptMail.HasRows Then
                While oRecDeptMail.Read
                    varStrDeptMail = oRecDeptMail.GetString(oRecDeptMail.GetOrdinal("Name"))
                End While
            End If
            oRecDeptMail.Close()
            oRecDeptMail = Nothing
            oCommandDeptMail = Nothing



            Dim oCommandLB As New Data.SqlClient.SqlCommand("SELECT CL,EL,TL,LWP,ML,WeeklyOff1,WeeklyOff2 FROM DBO.tblLeaveBalance  WHERE UserID='" & Session("UserID").ToString & "'", objConn)
            Dim oRecLB As Data.SqlClient.SqlDataReader = oCommandLB.ExecuteReader()

            If oRecLB.HasRows Then
                While oRecLB.Read
                    varDblCL = oRecLB.GetDouble(oRecLB.GetOrdinal("CL"))
                    varBDblCL = varDblCL
                    varDblEL = oRecLB.GetDouble(oRecLB.GetOrdinal("EL"))
                    varBDblEL = varDblEL
                    varDblTL = oRecLB.GetDouble(oRecLB.GetOrdinal("TL"))
                    varBDblTL = varDblTL
                    If Not oRecLB.IsDBNull(oRecLB.GetOrdinal("LWP")) Then
                        varDblLWP = oRecLB.GetDouble(oRecLB.GetOrdinal("LWP"))
                        varBDblLWP = varDblLWP
                    End If
                    If oRecLB.IsDBNull(oRecLB.GetOrdinal("WeeklyOff1")) Then
                        varStrWOff1 = ""
                    Else
                        varStrWOff1 = oRecLB.GetString(oRecLB.GetOrdinal("WeeklyOff1"))
                    End If
                    If oRecLB.IsDBNull(oRecLB.GetOrdinal("WeeklyOff2")) Then
                        varStrWOff2 = ""
                    Else
                        varStrWOff2 = oRecLB.GetString(oRecLB.GetOrdinal("WeeklyOff2"))
                    End If
                    If Not oRecLB.IsDBNull(oRecLB.GetOrdinal("ML")) Then
                        varDblML = oRecLB.GetDouble(oRecLB.GetOrdinal("ML"))
                        varBDblML = varDblML
                    End If

                    If varStrWOff1 = "" And varStrWOff2 = "" Then
                        varprevCheckDateDayName = ""
                        varnextCheckDateDayName = ""
                    ElseIf varStrWOff1 <> "" And varStrWOff2 = "" Then
                        varprevCheckDateDayName = objMainModule.WeekDayNameFromWeekDay(objMainModule.WeekDayFromWeekDayName(varStrWOff1) - 1)
                        varnextCheckDateDayName = objMainModule.WeekDayNameFromWeekDay(objMainModule.WeekDayFromWeekDayName(varStrWOff1) + 1)
                        varStrWOffCheckDateCount = 2
                    ElseIf varStrWOff1 = "" And varStrWOff2 <> "" Then
                        varprevCheckDateDayName = objMainModule.WeekDayNameFromWeekDay(objMainModule.WeekDayFromWeekDayName(varStrWOff2) - 1)
                        varnextCheckDateDayName = objMainModule.WeekDayNameFromWeekDay(objMainModule.WeekDayFromWeekDayName(varStrWOff2) + 1)
                        varStrWOffCheckDateCount = 2
                    ElseIf varStrWOff1 <> "" And varStrWOff2 <> "" Then
                        varprevCheckDateDayName = objMainModule.WeekDayNameFromWeekDay(objMainModule.WeekDayFromWeekDayName(varStrWOff2) - 2)
                        varnextCheckDateDayName = objMainModule.WeekDayNameFromWeekDay(objMainModule.WeekDayFromWeekDayName(varStrWOff2) + 2)
                        varStrWOffCheckDateCount = 3
                    End If
                End While
            End If

            oRecLB.Close()
            oRecLB = Nothing
            oCommandLB = Nothing
            varBolALFlag = False

            '*******Code Added by anil on 22th july 2008 bcoz vikram changing leave calculation policy********
            varDblLeaveCount = DateDiff(DateInterval.Day, varTempDtStart, varTempDtEnd) + 1
            varIntAdjustWOffCount = 0
            'Check for Attendance Conflict 
            While varTempDtEnd >= varTempDtStart
                'Response.Write(varTempDtStart)
                Dim varAttendance As Boolean

                Dim oCommandCheckAttendance As New Data.SqlClient.SqlCommand("SELECT SignIn,SignOut FROM DBO.tblAttendance WHERE UserID='" & Session("UserID").ToString & "' AND AttDate='" & varTempDtStart & "' AND Status='P'", objConn)
                Dim oRecCheckAttendance As Data.SqlClient.SqlDataReader = oCommandCheckAttendance.ExecuteReader()

                If oRecCheckAttendance.HasRows Then
                    varAttendance = True
                End If
                oRecCheckAttendance.Close()
                oRecCheckAttendance = Nothing
                oCommandCheckAttendance = Nothing

                'Delete attendance for that day
                If varAttendance And Trim(UCase(Request("DropLeaveType"))) <> Trim(UCase("LWPHL")) And Trim(UCase(Request("DropLeaveType"))) <> Trim(UCase("HL")) And Trim(UCase(Request("DropLeaveType"))) <> Trim(UCase("AHL")) And Trim(UCase(Request("DropLeaveType"))) <> Trim(UCase("ELHL")) And Trim(UCase(Request("DropLeaveType"))) <> Trim(UCase("CLHL")) Then
                    Dim DeleteCmdAttendance As New Data.SqlClient.SqlCommand
                    DeleteCmdAttendance.CommandType = Data.CommandType.Text
                    DeleteCmdAttendance.CommandText = "DELETE FROM DBO.tblAttendance WHERE UserID='" & Session("userID").ToString & "' AND AttDate='" & varTempDtStart & "'"
                    DeleteCmdAttendance.Connection = objConn
                    DeleteCmdAttendance.ExecuteNonQuery()
                    DeleteCmdAttendance = Nothing
                    varAttendance = False
                    Response.Write("<script type=""text/javascript"" language=javascript> alert(""You will be mark absent on " + varTempDtStart + " if your supervisior disapprove or if you cancel this leave application."");</script>")
                End If
                'Delete End
                'Check for Weekly Off consideration of leave
                Dim varWeekDayName As String
                Dim varCheckDate As String
                Dim varWorkDay As String
                varWeekDayName = WeekdayName(Weekday(varTempDtStart))

                Dim varStrTempWOffCheckDateCount As Integer
                varStrTempWOffCheckDateCount = "-" & varStrWOffCheckDateCount
                If Trim(UCase(varprevCheckDateDayName)) = Trim(UCase(varWeekDayName)) Then
                    varCheckDate = DateAdd(DateInterval.Day, varStrWOffCheckDateCount, varTempDtStart)
                    varWorkDay = "next"
                ElseIf Trim(UCase(varnextCheckDateDayName)) = Trim(UCase(varWeekDayName)) Then
                    varCheckDate = DateAdd(DateInterval.Day, varStrTempWOffCheckDateCount, varTempDtStart)
                    varWorkDay = "previous"
                End If

                If varCheckDate <> "" And varWorkDay <> "" Then
                    Dim oCommandCheckDate As New Data.SqlClient.SqlCommand("SELECT LeaveID FROM DBO.tblLeave WHERE UserID='" & Session("UserID") & "' AND Status <>'Not Approved' AND TypeOfLeave<>'HL' AND TypeOfLeave<>'AHL' AND TypeOfLeave<>'LWPHL' AND '" & varCheckDate & "' >= StartDate AND '" & varCheckDate & "' <= EndDate AND IsDeleted IS NULL", objConn)
                    Dim oRecCheckDate As Data.SqlClient.SqlDataReader = oCommandCheckDate.ExecuteReader()
                    If oRecCheckDate.HasRows Then
                        While oRecCheckDate.Read
                            varIntAdjustWOffCount = varIntAdjustWOffCount + 1
                            varStrRefLeaveID = oRecCheckDate.GetGuid(oRecCheckDate.GetOrdinal("LeaveID")).ToString
                            Response.Write("<script type=""text/javascript"" language=javascript> alert(""Your weekly Off mark as leave because you are on leave for " + varWorkDay + " working day."");</script>")
                        End While
                    End If
                    oRecCheckDate.Close()
                    oRecCheckDate = Nothing
                    oCommandCheckDate = Nothing
                End If

                'Weekly Off consideration of leave End
                varTempDtStart = DateAdd(DateInterval.Day, 1, varTempDtStart)
                varWeekDayName = ""
                varCheckDate = ""
                varWorkDay = ""
            End While


            varTempDtStart = DateSerial(Year(Request("txtStartDate")), Month(Request("txtStartDate")), Day(Request("txtStartDate")))
            varTempDtEnd = DateSerial(Year(Request("txtEndDate")), Month(Request("txtEndDate")), Day(Request("txtEndDate")))

            'Attendance conflict end

            Dim oCommandCheckOffDateInfo As New Data.SqlClient.SqlCommand("SELECT State,DepartmentID FROM DBO.tblUsers  WHERE UserID='" & Session("UserID") & "' AND ContractorID='" & Session("ContractorID").ToString & "' ", objConn)
            Dim oRecCheckOffDateInfo As Data.SqlClient.SqlDataReader = oCommandCheckOffDateInfo.ExecuteReader()

            If oRecCheckOffDateInfo.HasRows Then
                While oRecCheckOffDateInfo.Read
                    If Not oRecCheckOffDateInfo.IsDBNull(oRecCheckOffDateInfo.GetOrdinal("State")) Then
                        varStrEmpState = oRecCheckOffDateInfo.GetString(oRecCheckOffDateInfo.GetOrdinal("State"))
                    End If
                    If Not oRecCheckOffDateInfo.IsDBNull(oRecCheckOffDateInfo.GetOrdinal("DepartmentID")) Then
                        varStrEmpDeptID = oRecCheckOffDateInfo.GetGuid(oRecCheckOffDateInfo.GetOrdinal("DepartmentID")).ToString
                    End If
                End While
            End If
            oRecCheckOffDateInfo.Close()
            oRecCheckOffDateInfo = Nothing
            oCommandCheckOffDateInfo = Nothing

            'Response.Write("State:" & varStrEmpState)
            'Response.Write("Dept:" & varStrEmpDeptID)

            varDblDedcutCount = objMainModule.CheckOffDateCount(varStrDtFrom, varStrDtTo, varStrEmpDeptID, varStrEmpState)


            If Trim(UCase(Request("DropLeaveType"))) = Trim(UCase("CL")) Then
                varStrLeaveType = "Casual Leave "

                varDblCL = varDblCL - varDblLeaveCount
                varDblTL = varDblTL - varDblLeaveCount

                If varIntAdjustWOffCount > 0 Then
                    varDblCL = varDblCL - varIntAdjustWOffCount
                    varDblTL = varDblTL - varIntAdjustWOffCount
                End If

                If varDblDedcutCount > 0 Then
                    varDblCL = varDblCL + varDblDedcutCount
                    varDblTL = varDblTL + varDblDedcutCount
                End If

                varDblLeaveBalLog = varBDblCL
                varDblLeaveBalLogA = varDblCL
            ElseIf Trim(UCase(Request("DropLeaveType"))) = Trim(UCase("EL")) Or Trim(UCase(Request("DropLeaveType"))) = Trim(UCase("AL")) Then
                If Trim(UCase(Request("DropLeaveType"))) = Trim(UCase("EL")) Then
                    varStrLeaveType = "Earned Leave"
                ElseIf Trim(UCase(Request("DropLeaveType"))) = Trim(UCase("AL")) Then
                    varStrLeaveType = "Advance Leave"
                    varBolALFlag = True
                End If


                varDblEL = varDblEL - varDblLeaveCount
                varDblTL = varDblTL - varDblLeaveCount

                If varIntAdjustWOffCount > 0 Then
                    varDblEL = varDblEL - varIntAdjustWOffCount
                    varDblTL = varDblTL - varIntAdjustWOffCount
                End If

                If varDblDedcutCount > 0 Then
                    varDblEL = varDblEL + varDblDedcutCount
                    varDblTL = varDblTL + varDblDedcutCount
                End If
                varDblLeaveBalLog = varBDblEL
                varDblLeaveBalLogA = varDblEL
            ElseIf Trim(UCase(Request("DropLeaveType"))) = Trim(UCase("ML")) Then
                varStrLeaveType = "Maternity Leave"
                varDblML = varDblML + varDblLeaveCount
                varDblLeaveBalLog = varBDblML
                varDblLeaveBalLogA = varDblML
            ElseIf Trim(UCase(Request("DropLeaveType"))) = Trim(UCase("HL")) Or Trim(UCase(Request("DropLeaveType"))) = Trim(UCase("AHL")) Or Trim(UCase(Request("DropLeaveType"))) = Trim(UCase("ELHL")) Then
                If Trim(UCase(Request("DropLeaveType"))) = Trim(UCase("HL")) Then
                    varStrLeaveType = "Half Day Leave"
                ElseIf Trim(UCase(Request("DropLeaveType"))) = Trim(UCase("ELHL")) Then
                    varStrLeaveType = "Half Day Leave(Earned Leave)"
                ElseIf Trim(UCase(Request("DropLeaveType"))) = Trim(UCase("AHL")) Then
                    varStrLeaveType = "Advace Half Day Leave"
                    varBolALFlag = True
                End If

                varDblDedcutCount = varDblDedcutCount * 0.5
                Dim varDblHL As Double
                varDblHL = varDblLeaveCount * 0.5

                varDblEL = varDblEL - varDblHL
                varDblTL = varDblTL - varDblHL

                If varDblDedcutCount > 0 Then
                    varDblEL = varDblEL + varDblDedcutCount
                    varDblTL = varDblTL + varDblDedcutCount
                End If
                varDblLeaveBalLog = varBDblEL
                varDblLeaveBalLogA = varDblEL
            ElseIf Trim(UCase(Request("DropLeaveType"))) = Trim(UCase("CLHL")) Then
                varStrLeaveType = "Half Day Leave(Casual Leave)"

                varDblDedcutCount = varDblDedcutCount * 0.5
                Dim varDblHL As Double
                varDblHL = varDblLeaveCount * 0.5

                varDblCL = varDblCL - varDblHL
                varDblTL = varDblTL - varDblHL

                If varDblDedcutCount > 0 Then
                    varDblCL = varDblCL + varDblDedcutCount
                    varDblTL = varDblTL + varDblDedcutCount
                End If
                varDblLeaveBalLog = varBDblCL
                varDblLeaveBalLogA = varDblCL
            ElseIf Trim(UCase(Request("DropLeaveType"))) = Trim(UCase("CO")) Then
                'varStrLeaveType = "Comp. Off"
                'varDblEL = varDblEL + varDblLeaveCount
                'varDblTL = varDblTL + varDblLeaveCount
                'varDblLeaveBalLog = varBDblEL
                'varDblLeaveBalLogA = varDblEL

                'Changes as suman request for adding comp off in CL instead of EL on 23/03/2010
                varStrLeaveType = "Comp. Off"
                varDblCL = varDblCL + varDblLeaveCount
                varDblTL = varDblTL + varDblLeaveCount
                varDblLeaveBalLog = varBDblCL
                varDblLeaveBalLogA = varDblCL
                'End Changes for suman

            ElseIf Trim(UCase(Request("DropLeaveType"))) = Trim(UCase("LWP")) Then
                varStrLeaveType = "Leave Without Pay"
                varDblLWP = varDblLWP + varDblLeaveCount

                If varIntAdjustWOffCount > 0 Then
                    varDblLWP = varDblLWP + varIntAdjustWOffCount
                End If

                If varDblDedcutCount > 0 Then
                    varDblLWP = varDblLWP - varDblDedcutCount
                End If
                varDblLeaveBalLog = varBDblLWP
                varDblLeaveBalLogA = varDblLWP
            ElseIf Trim(UCase(Request("DropLeaveType"))) = Trim(UCase("LWPHL")) Then
                varStrLeaveType = "Half Day(Leave Without Pay)"

                varDblDedcutCount = varDblDedcutCount * 0.5
                Dim varDblHL As Double
                varDblHL = varDblLeaveCount * 0.5

                varDblLWP = varDblLWP + varDblHL

                If varDblDedcutCount > 0 Then
                    varDblLWP = varDblLWP - varDblDedcutCount
                End If
                varDblLeaveBalLog = varBDblLWP
                varDblLeaveBalLogA = varDblLWP
            End If

            varSuperID = objMainModule.GetSuperID(Session("UserID").ToString)

            If varSuperID = "" Then
                Response.Write("<center><font face=""Trebuchet MS"" size=""2"" color=""#000080"">Supervisor for the department has not been set.Please contact to HR Department.</font></center>")
                Response.Write("<center><a href=""LeaveRequest.aspx""><font face=""Trebuchet MS"" size=""2"">Back</font></a></center>")
                Response.End()
            End If

            'Update Leave Balance of Employee
            Dim varStrQuery As String
            Dim UpdateCmdLB As New Data.SqlClient.SqlCommand
            UpdateCmdLB.CommandType = Data.CommandType.Text
            UpdateCmdLB.CommandText = "UPDATE DBO.tblLeaveBalance SET CL=" & varDblCL & ",EL=" & varDblEL & ",TL=" & varDblTL & ",ML=" & varDblML & ",LWP=" & varDblLWP & " WHERE UserID='" & Session("userID").ToString & "'"
            UpdateCmdLB.Connection = objConn
            UpdateCmdLB.ExecuteNonQuery()
            UpdateCmdLB = Nothing

            While varTempDtEnd >= varTempDtStart
                If Trim(UCase(Request("DropLeaveType"))) = Trim(UCase("HL")) Or Trim(UCase(Request("DropLeaveType"))) = Trim(UCase("AHL")) Or Trim(UCase(Request("DropLeaveType"))) = Trim(UCase("LWPHL")) Or Trim(UCase(Request("DropLeaveType"))) = Trim(UCase("ELHL")) Or Trim(UCase(Request("DropLeaveType"))) = Trim(UCase("CLHL")) Then

                Else
                    Dim oCommandAttCheck As New Data.SqlClient.SqlCommand("SELECT Status FROM DBO.tblAttendance WHERE UserID='" & Session("UserID").ToString & "' AND MONTH(AttDate)=" & Month(varTempDtStart) & " AND YEAR(AttDate)=" & Year(varTempDtStart) & " AND DAY(AttDate)=" & Day(varTempDtStart) & "", objConn)
                    Dim oRecAttCheck As Data.SqlClient.SqlDataReader = oCommandAttCheck.ExecuteReader()
                    If oRecAttCheck.HasRows Then
                        While oRecAttCheck.Read
                            If Trim(UCase(oRecAttCheck.GetString(oRecAttCheck.GetOrdinal("Status")))) = Trim(UCase("P")) Then
                                varBolAttCheck = True
                            End If
                        End While
                    End If
                    oRecAttCheck.Close()
                    oRecAttCheck = Nothing
                    oCommandAttCheck = Nothing

                    If varBolAttCheck = True Then
                        If Trim(UCase(varStrLeaveType)) = Trim(UCase("LWP")) Then
                            varStrQuery = "UPDATE DBO.tblAttendance SET StatusFlag='OUT',SignOut='OUT' Status='A' WHERE UserID='" & Session("UserID") & "' AttDate='" & DateSerial(Year(varTempDtStart), Month(varTempDtStart), Day(varTempDtStart)) & "'"
                        Else
                            varStrQuery = "UPDATE DBO.tblAttendance SET StatusFlag='OUT',SignOut='OUT' Status='L' WHERE UserID='" & Session("UserID") & "' AttDate='" & DateSerial(Year(varTempDtStart), Month(varTempDtStart), Day(varTempDtStart)) & "'"
                        End If
                        Dim UpdateAttendanceCmd As New Data.SqlClient.SqlCommand
                        UpdateAttendanceCmd.CommandType = Data.CommandType.Text
                        UpdateAttendanceCmd.CommandText = varStrQuery
                        UpdateAttendanceCmd.Connection = objConn
                        UpdateAttendanceCmd.ExecuteNonQuery()
                        UpdateAttendanceCmd = Nothing
                    Else
                        If Trim(UCase(varStrQuery)) = Trim(UCase("LWP")) Then
                            varStrQuery = "INSERT INTO DBO.tblAttendance(UserID,AttDate,StatusFlag,Status) VALUES('" & Session("UserID") & "','" & DateSerial(Year(varTempDtStart), Month(varTempDtStart), Day(varTempDtStart)) & "','OUT','A')"
                        Else
                            varStrQuery = "INSERT INTO DBO.tblAttendance(UserID,AttDate,StatusFlag,Status) VALUES('" & Session("UserID") & "','" & DateSerial(Year(varTempDtStart), Month(varTempDtStart), Day(varTempDtStart)) & "','OUT','L')"
                        End If
                        Dim InsertAttendanceCmd As New Data.SqlClient.SqlCommand
                        InsertAttendanceCmd.CommandType = Data.CommandType.Text
                        InsertAttendanceCmd.CommandText = varStrQuery
                        InsertAttendanceCmd.Connection = objConn
                        InsertAttendanceCmd.ExecuteNonQuery()
                        InsertAttendanceCmd = Nothing
                    End If
                End If
                varTempDtStart = DateAdd(DateInterval.Day, 1, varTempDtStart)
                varStrQuery = ""
            End While

            'Response.Write(varDblDedcutCount)
            'Response.End()


            '**********anil ended************


            'Get all the information for the employee.
            Dim oCommand As New Data.SqlClient.SqlCommand("SELECT U.FirstName,U.LastName,U.OfficialMailID FROM DBO.tblLeaveBalance LB INNER JOIN DBO.tblUsers U ON LB.UserID=U.UserID WHERE U.UserID='" & Session("UserID").ToString & "' AND U.ContractorID='" & Session("ContractorID").ToString & "'  ", objConn)
            Dim oRec As Data.SqlClient.SqlDataReader = oCommand.ExecuteReader()

            If oRec.HasRows Then
                While oRec.Read
                    If Not oRec.IsDBNull(oRec.GetOrdinal("FirstName")) Then
                        varStrEmpName = oRec.GetString(oRec.GetOrdinal("FirstName"))
                    End If
                    If Not oRec.IsDBNull(oRec.GetOrdinal("LastName")) Then
                        varStrEmpName = varStrEmpName & " " & oRec.GetString(oRec.GetOrdinal("LastName"))
                    End If
                    If Not oRec.IsDBNull(oRec.GetOrdinal("OfficialMailID")) Then
                        varStrEmpMailCC = oRec.GetString(oRec.GetOrdinal("OfficialMailID"))
                    End If
                End While
            End If
            Dim Str As String


            oRec.Close()
            oRec = Nothing
            oCommand = Nothing

            'Get all the supervisor for the employee.
            Dim oCommandSV As New Data.SqlClient.SqlCommand("SELECT LevelNo,SuperVisorID FROM DBO.tblDeptSuperVisorAssign S INNER JOIN DBO.tblDepartments D ON  S.DepartmentID=D.DepartmentID INNER JOIN DBO.tblUsers U ON U.DepartmentID=D.DepartmentID WHERE U.UserID='" & Session("UserID").ToString & "' AND U.ContractorID='" & Session("ContractorID").ToString & "' ", objConn)
            Dim oRecSV As Data.SqlClient.SqlDataReader = oCommandSV.ExecuteReader()

            If oRecSV.HasRows Then
                While oRecSV.Read
                    If oRecSV.GetDecimal(oRecSV.GetOrdinal("LevelNo")) = CInt("1") Then
                        If Not oRecSV.IsDBNull(oRecSV.GetOrdinal("SuperVisorID")) Then
                            varStrPri = oRecSV(oRecSV.GetOrdinal("SuperVisorID")).ToString
                        End If
                    End If

                    If oRecSV.GetDecimal(oRecSV.GetOrdinal("LevelNo")) = CInt("2") Then
                        If Not oRecSV.IsDBNull(oRecSV.GetOrdinal("SuperVisorID")) Then
                            varStrSec = oRecSV(oRecSV.GetOrdinal("SuperVisorID")).ToString
                        End If
                    End If

                    If oRecSV.GetDecimal(oRecSV.GetOrdinal("LevelNo")) = CInt("3") Then
                        If Not oRecSV.IsDBNull(oRecSV.GetOrdinal("SuperVisorID")) Then
                            varStrThi = oRecSV(oRecSV.GetOrdinal("SuperVisorID")).ToString
                        End If
                    End If
                End While
            End If

            oRecSV.Close()
            oRecSV = Nothing
            oCommandSV = Nothing

            ''Store Mail ID of all SuperVisors
            'Dim oCommandMailID As New Data.SqlClient.SqlCommand("SELECT UserID,OfficialMailID FROM DBO.tblUsers U WHERE U.UserID IN ('" & varStrPri & "','" & varStrSec & "','" & varStrThi & "')", objMainModule.oConn)
            'Dim oRecMailID As Data.SqlClient.SqlDataReader = oCommandMailID.ExecuteReader()
            'If oRecMailID.HasRows Then
            '    While oRecMailID.Read
            '        If oRecMailID(oRecMailID.GetOrdinal("UserID")).ToString = varStrPri Then
            '            varStrPMailID = oRecMailID(oRecMailID.GetOrdinal("OfficialMailID"))
            '        ElseIf oRecMailID(oRecMailID.GetOrdinal("UserID")).ToString = varStrSec Then
            '            varStrSMailID = oRecMailID(oRecMailID.GetOrdinal("OfficialMailID"))
            '        ElseIf oRecMailID(oRecMailID.GetOrdinal("UserID")).ToString = varStrThi Then
            '            varStrTMailID = oRecMailID(oRecMailID.GetOrdinal("OfficialMailID"))
            '        End If
            '    End While
            'End If
            'oRecMailID.Close()
            'oRecMailID = Nothing
            'oCommandMailID = Nothing

            ''Check if primary Supervisor is on leave or not
            'Dim oCommandPri As New Data.SqlClient.SqlCommand("SELECT U.UserID,U.OfficialMailID,L.StartDate,L.EndDate FROM DBO.tblUsers U INNER JOIN DBO.tblLeave L  ON U.UserID = L.UserID WHERE U.UserID='" & varStrPri & "'", objMainModule.oConn)
            'Dim oRecPri As Data.SqlClient.SqlDataReader = oCommandPri.ExecuteReader()

            'If oRecPri.HasRows Then
            '    While oRecPri.Read
            '        If varBolWhileFlag = False Then
            '            varDtStartDate = oRecPri.GetDateTime(oRecPri.GetOrdinal("StartDate"))
            '            varDtEndDate = oRecPri.GetDateTime(oRecPri.GetOrdinal("EndDate"))
            '            While varDtEndDate >= varDtStartDate
            '                If DateDiff(DateInterval.Day, DateSerial(Year(varDtStartDate), Month(varDtStartDate), Day(varDtStartDate)), DateSerial(Year(varDateTodaydate), Month(varDateTodaydate), Day(varDateTodaydate))) = 0 Then
            '                    varToMail = ""
            '                    varBolWhileFlag = True
            '                    Exit While
            '                End If
            '                varDtStartDate = DateAdd(DateInterval.Day, 1, varDtStartDate)
            '            End While
            '        Else
            '            Exit While
            '        End If
            '    End While
            '    If varBolWhileFlag = False Then
            '        varSuperID = varStrPri
            '        varToMail = varStrPMailID
            '    Else
            '        varBolWhileFlag = False
            '    End If
            'Else
            '    varSuperID = varStrPri
            '    varToMail = varStrPMailID
            'End If

            'oRecPri.Close()
            'oRecPri = Nothing
            'oCommandPri = Nothing

            ''Check if Secondary Supervisor is on leave or not
            'If varToMail = "" Then
            '    Dim oCommandSec As New Data.SqlClient.SqlCommand("SELECT U.UserID,U.OfficialMailID,L.StartDate,L.EndDate FROM DBO.tblUsers U INNER JOIN DBO.tblLeave L  ON U.UserID = L.UserID WHERE U.UserID='" & varStrSec & "'", objMainModule.oConn)
            '    Dim oRecSec As Data.SqlClient.SqlDataReader = oCommandSec.ExecuteReader()

            '    If oRecSec.HasRows Then
            '        While oRecSec.Read
            '            If varBolWhileFlag = False Then
            '                varDtStartDate = oRecSec.GetDateTime(oRecSec.GetOrdinal("StartDate"))
            '                varDtEndDate = oRecSec.GetDateTime(oRecSec.GetOrdinal("EndDate"))
            '                While varDtEndDate >= varDtStartDate
            '                    If DateDiff(DateInterval.Day, DateSerial(Year(varDtStartDate), Month(varDtStartDate), Day(varDtStartDate)), DateSerial(Year(varDateTodaydate), Month(varDateTodaydate), Day(varDateTodaydate))) = 0 Then
            '                        varToMail = ""
            '                        varBolWhileFlag = True
            '                        Exit While
            '                    End If
            '                    varDtStartDate = DateAdd(DateInterval.Day, 1, varDtStartDate)
            '                End While
            '            Else
            '                Exit While
            '            End If
            '        End While
            '        If varBolWhileFlag = False Then
            '            varSuperID = varStrSec
            '            varToMail = varStrSMailID
            '        Else
            '            varBolWhileFlag = False
            '        End If
            '    Else
            '        varSuperID = varStrSec
            '        varToMail = varStrSMailID
            '    End If

            '    oRecSec.Close()
            '    oRecSec = Nothing
            '    oCommandSec = Nothing
            'End If

            ''Check if Thrid level Supervisor is on leave or not
            'If varToMail = "" Then
            '    Dim oCommandThi As New Data.SqlClient.SqlCommand("SELECT U.UserID,U.OfficialMailID,L.StartDate,L.EndDate FROM DBO.tblUsers U INNER JOIN DBO.tblLeave L  ON U.UserID = L.UserID WHERE U.UserID='" & varStrThi & "'", objMainModule.oConn)
            '    Dim oRecThi As Data.SqlClient.SqlDataReader = oCommandThi.ExecuteReader()

            '    If oRecThi.HasRows Then
            '        While oRecThi.Read
            '            If varBolWhileFlag = False Then
            '                varDtStartDate = oRecThi.GetDateTime(oRecThi.GetOrdinal("StartDate"))
            '                varDtEndDate = oRecThi.GetDateTime(oRecThi.GetOrdinal("EndDate"))
            '                While varDtEndDate >= varDtStartDate
            '                    If DateDiff(DateInterval.Day, DateSerial(Year(varDtStartDate), Month(varDtStartDate), Day(varDtStartDate)), DateSerial(Year(varDateTodaydate), Month(varDateTodaydate), Day(varDateTodaydate))) = 0 Then
            '                        varToMail = ""
            '                        varBolWhileFlag = True
            '                        Exit While
            '                    End If
            '                    varDtStartDate = DateAdd(DateInterval.Day, 1, varDtStartDate)
            '                End While
            '            Else
            '                Exit While
            '            End If
            '        End While
            '        If varBolWhileFlag = False Then
            '            varSuperID = varStrSec
            '            varToMail = varStrTMailID
            '        Else
            '            varBolWhileFlag = False
            '        End If
            '    Else
            '        varSuperID = varStrThi
            '        varToMail = varStrTMailID
            '    End If

            '    oRecThi.Close()
            '    oRecThi = Nothing
            '    oCommandThi = Nothing

            'End If
            

            varToMail = objMainModule.GetOfficialMailID(varSuperID)
            'Check if Leave type is Advance Leave then leave application approve by finance manager
            If varBolALFlag Then
                Dim oCommandFinance As New Data.SqlClient.SqlCommand("SELECT UserID,OfficialMailID FROM DBO.tblUsers WHERE UserID=(SELECT SuperVisorID FROM DBO.tblDeptSuperVisorAssign DA INNER JOIN DBO.tblDepartments D ON DA.DepartmentID=D.DepartmentID WHERE D.Name='Finance' AND DA.LevelNo=4) AND ContractorID='" & Session("ContractorID").ToString & "' ", objConn)
                Dim oRecFinance As Data.SqlClient.SqlDataReader = oCommandFinance.ExecuteReader()

                If oRecFinance.HasRows Then
                    While oRecFinance.Read
                        varSuperID = oRecFinance.GetGuid(oRecFinance.GetOrdinal("UserID")).ToString
                        varToMail = oRecFinance.GetString(oRecFinance.GetOrdinal("OfficialMailID")).ToString & "," & varToMail
                    End While
                End If
                oRecFinance.Close()
                oRecFinance = Nothing
                oCommandFinance = Nothing
            End If

            'End Advance leave application
            'Check if RefLeaveID contains value


            

            Dim varLeaveID As String
            varLeaveID = Guid.NewGuid().ToString
            If varStrRefLeaveID = "" Then
                varStrInsert = "INSERT INTO DBO.tblLeave(LeaveID,UserID,TypeOfLeave,StartDate,EndDate,Reason,Status,ApproveBy,AppDate) VALUES('" & varLeaveID & "','" & Session("UserID") & "','" & Trim(Request("DropLeaveType")) & "','" & DateSerial(Year(Request("txtStartDate")), Month(Request("txtStartDate")), Day(Request("txtStartDate"))) & "','" & DateSerial(Year(Request("txtEndDate")), Month(Request("txtEndDate")), Day(Request("txtEndDate"))) & "','" & Replace(Request("textArea1"), "'", "''") & "','Pending','" & varSuperID & "','" & varDateTodaydate & "')"
            ElseIf varStrRefLeaveID <> "" Then
                varStrInsert = "INSERT INTO DBO.tblLeave(LeaveID,UserID,TypeOfLeave,StartDate,EndDate,Reason,Status,ApproveBy,AppDate,WeeklyOffCount,RefLeaveID) VALUES('" & varLeaveID & "','" & Session("UserID") & "','" & Trim(Request("DropLeaveType")) & "','" & DateSerial(Year(Request("txtStartDate")), Month(Request("txtStartDate")), Day(Request("txtStartDate"))) & "','" & DateSerial(Year(Request("txtEndDate")), Month(Request("txtEndDate")), Day(Request("txtEndDate"))) & "','" & Replace(Request("textArea1"), "'", "''") & "','Pending','" & varSuperID & "','" & varDateTodaydate & "'," & varIntAdjustWOffCount & ",'" & varStrRefLeaveID & "')"
            End If

            
            

            Dim InsertCmd As New Data.SqlClient.SqlCommand
            InsertCmd.CommandType = Data.CommandType.Text
            InsertCmd.CommandText = varStrInsert
            InsertCmd.Connection = objConn
            InsertCmd.ExecuteNonQuery()
            InsertCmd = Nothing

            'Insert Log entry of leave application
            Dim InsertCmdLog As New Data.SqlClient.SqlCommand
            InsertCmdLog.CommandType = Data.CommandType.Text
            InsertCmdLog.CommandText = "INSERT INTO DBO.tblLeaveLog(LeaveID,OprBy,LeaveType,OprDesc,BBalance,ABalance,OprOn)VALUES('" & varLeaveID & "','" & varStrEmpName & "','" & Trim(Request("DropLeaveType")) & "',' Leave application request from " & Request("txtStartDate") & " to " & Request("txtEndDate") & " send '," & varDblLeaveBalLog & "," & varDblLeaveBalLogA & ",'" & varDateTodaydate & "')"
            InsertCmdLog.Connection = objConn
            InsertCmdLog.ExecuteNonQuery()
            InsertCmdLog = Nothing
            'end log entry of leave application

            'Leave Application Mail send to particular Supervisor.


            If Trim(UCase(varStrDeptMail)) = Trim(UCase("Software")) Then
                varStrDeptMail = "software@edictate.com"
            ElseIf Trim(UCase(varStrDeptMail)) = Trim(UCase("Technical")) Then
                varStrDeptMail = "techsupport@edictate.com"
            ElseIf Trim(UCase(varStrDeptMail)) = Trim(UCase("Production Support")) Then
                varStrDeptMail = "edi@edictate.com"
            ElseIf Trim(UCase(varStrDeptMail)) = Trim(UCase("MT Support")) Or Trim(UCase(varStrDeptMail)) = Trim(UCase("Training and Mentoring")) Or Trim(UCase(varStrDeptMail)) = Trim(UCase("Production - 1")) Or Trim(UCase(varStrDeptMail)) = Trim(UCase("Production - 2")) Then
                varStrDeptMail = "mtsupport@edictate.com"
            ElseIf Trim(UCase(varStrDeptMail)) = Trim(UCase("Customer Support")) Then
                varStrDeptMail = "support@edictate.com"
            ElseIf Trim(UCase(varStrDeptMail)) = Trim(UCase("Business Development")) Then
                varStrDeptMail = "support@edictate.com"
            End If

            Dim varCCMail As String
            Dim varMailSubject As String
            Dim varMailMatter As String
            Dim Text

            If String.IsNullOrEmpty(varStrLeaveType) Then
                If Trim(UCase(Request("DropLeaveType"))) = Trim(UCase("RL")) Then
                    varStrLeaveType = "Religious Leave"
                End If
            End If

            Text = "<font face=""Trebuchet MS"" size=""2"" color=""#000080"">" & varStrEmpName & " has applied for the " & varStrLeaveType
            Text = Text & " from " & varStrDtFrom & " to " & varStrDtTo & "<br>" & "Reason: " & varStrReason & "</FONT>"

            varCCMail = varStrDeptMail & "," & varStrEmpMailCC
            varMailSubject = "Leave Application of " & varStrEmpName
            varMailMatter = Text

            'If Trim(UCase(Session("UserID").ToString)) = Trim(UCase("A1ABBF5E-4869-4600-907F-01B6FAEEF377")) Then
            '    Response.Write("varStrEmpMailCC : " & varStrEmpMailCC & ",varToMail : " & varToMail & ",varCCmail : " & varCCMail & ",varMailSubject : " & varMailSubject & ",varMailMatter : " & varMailMatter)
            '    Response.End()
            'End If

            'If Trim(UCase(Session("UserID").ToString)) = Trim(UCase("48EDC145-615F-4CFE-A99E-8FEDF379805C")) Then
            '    Response.Write(varSuperID & varToMail & varStrInsert)
            '    Response.Write("varStrEmpMailCC : " & varStrEmpMailCC & ",varToMail : " & varToMail & ",varCCmail : " & varCCMail & ",varMailSubject : " & varMailSubject & ",varMailMatter : " & varMailMatter)
            '    Response.End()
            'End If

            If objMainModule.SendMail(varStrEmpMailCC, varToMail, varCCMail, varMailSubject, varMailMatter) Then
                Response.Write("<script type=""text/javascript"" language=javascript> alert(""Leave Application Sent!!!"");window.location.href='LeaveRequest.aspx';window.parent.LeftFrame.location.reload();</script>")
            End If

            'anil ended
        Catch ex As Exception
        Finally
            If objConn.State <> Data.ConnectionState.Closed Then
                objConn.Close()
                objConn = Nothing
            End If
        End Try
    End Sub
End Class
