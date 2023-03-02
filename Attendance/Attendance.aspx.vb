Partial Class Attendance
    Inherits BasePage
    Dim ConString As String
    Dim tempDt As Date
    Dim oConn As New Data.SqlClient.SqlConnection
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim i As Int16
        Dim varStrSQLQuery As String
        Dim varDateTodaydate As Date
        Dim varBolCheckDayLight As Boolean
        Dim varStrSignInTime As String
        Dim varStrLeave As String
        Dim varStrBreakTime As String
        Dim varStrMessage As String
        Dim varBolCheckEndBreak As Boolean
        Dim varBolCheckSignOut As Boolean
        Dim varBolCheckLeave As Boolean

        i = 0
        Session("UserID") = "838d2a09-7374-4bbd-9a6a-f1ed1621c536"

        ConString = System.Configuration.ConfigurationManager.AppSettings("ETSCon")

        oConn.ConnectionString = ConString
        Try
            oConn.Open()

            varBolCheckEndBreak = False
            varBolCheckSignOut = False
            varBolCheckLeave = False
            varStrMessage = ""

            varBolCheckDayLight = CheckDayLightSavings(Now())
            If varBolCheckDayLight = True Then
                varDateTodaydate = DateAdd(DateInterval.Hour, 9, Now())
                varDateTodaydate = DateAdd(DateInterval.Minute, 30, varDateTodaydate)
            Else
                varDateTodaydate = DateAdd(DateInterval.Hour, 10, Now())
                varDateTodaydate = DateAdd(DateInterval.Minute, 30, varDateTodaydate)
            End If

            varStrSQLQuery = "SELECT * FROM tblAttendance WHERE UserID = '" & Session("UserID") & "' AND MONTH(AttDate)=" & Month(varDateTodaydate) & " AND YEAR(AttDate)=" & Year(varDateTodaydate) & " AND DAY(AttDate)=" & Day(varDateTodaydate) & ""

            Dim oCommand As New Data.SqlClient.SqlCommand(varStrSQLQuery, oConn)
            Dim oRec As Data.SqlClient.SqlDataReader = oCommand.ExecuteReader()

            If oRec.HasRows Then
                While oRec.Read
                    If oRec.IsDBNull(oRec.GetOrdinal("SignOut")) Then
                        varStrSignInTime = oRec.GetString(1)
                        varStrMessage = "<font face='Trebuchet MS' size='2' color='#000000'>You Have Signed in at " & varStrSignInTime & "</Font>"
                    Else
                        varStrSignInTime = oRec.GetString(1)
                        varStrMessage = "<font face='Trebuchet MS' size='2' color='#000000'>You Can't Sign In as you are already Signed Out</Font>"
                        varBolCheckSignOut = True
                    End If
                End While
            End If
            oRec.Close()
            oRec = Nothing
            oCommand = Nothing

            'Check wheather employee in on leave or not
            varStrLeave = "SELECT * FROM tblLeave WHERE UserID='" & Session("UserId") & "' AND StartDate <= '" & DateSerial(Year(varDateTodaydate), Month(varDateTodaydate), Day(varDateTodaydate)) & "' AND EndDate >= '" & DateSerial(Year(varDateTodaydate), Month(varDateTodaydate), Day(varDateTodaydate)) & "' AND Status ='Approved' AND TypeOfLeave <> 'Half Day'"
            Dim oCommandLeave As New Data.SqlClient.SqlCommand(varStrLeave, oConn)
            Dim oRecLeave As Data.SqlClient.SqlDataReader = oCommandLeave.ExecuteReader()

            If oRecLeave.HasRows Then
                While oRecLeave.Read
                    varStrMessage = "<font face='Trebuchet MS' size='2'>You Can't Sign In as you are on Leave</Font>"
                    varBolCheckLeave = True
                End While
            End If
            oRecLeave.Close()
            oRecLeave = Nothing
            oCommandLeave = Nothing

            If varStrMessage <> "" Then
                Dim tblRow As New TableRow
                Dim tblCell As New TableCell

                tblCell.ColumnSpan = 2
                tblCell.Text = varStrMessage
                tblCell.BorderStyle = BorderStyle.Inset
                tblCell.BorderWidth = 2

                tblRow.HorizontalAlign = HorizontalAlign.Left
                tblRow.Cells.Add(tblCell)
                Table1.Rows.Add(tblRow)
                'varBolCheckSignOut = True
            End If

            varStrSQLQuery = "SELECT * FROM tblBreak WHERE UserID = '" & Session("UserID") & "' AND MONTH (AttDate)=" & Month(varDateTodaydate) & " AND YEAR(AttDate)=" & Year(varDateTodaydate) & " AND DAY(AttDate)=" & Day(varDateTodaydate) & ""

            Dim oCommandBreak As New Data.SqlClient.SqlCommand(varStrSQLQuery, oConn)
            Dim oRecBreak As Data.SqlClient.SqlDataReader = oCommandBreak.ExecuteReader()

            If oRecBreak.HasRows Then
                While oRecBreak.Read

                    Dim vartblRowBreak As New TableRow
                    Dim vartblCellStartBreak As New TableCell
                    Dim vartblCellEndBreak As New TableCell
                    Table1.Rows.Add(vartblRowBreak)


                    vartblCellStartBreak.HorizontalAlign = HorizontalAlign.Left
                    vartblCellStartBreak.Text = "<font face='Trebuchet MS' size='2'>Your Break Starts at " & oRecBreak.GetString(1) & "</font>"
                    vartblCellStartBreak.BorderStyle = BorderStyle.Inset
                    vartblCellStartBreak.BorderWidth = 2
                    vartblCellStartBreak.Height = 14

                    vartblCellEndBreak.HorizontalAlign = HorizontalAlign.Left
                    If oRecBreak.IsDBNull(2) Then
                        vartblCellEndBreak.Text = "<font face='Trebuchet MS' size='2'>Your Break Ends at  </font>"
                        varBolCheckEndBreak = True
                    Else
                        vartblCellEndBreak.Text = "<font face='Trebuchet MS' size='2'>Your Break Ends at " & oRecBreak.GetString(2) & "</font>"
                    End If
                    vartblCellEndBreak.BorderStyle = BorderStyle.Inset
                    vartblCellEndBreak.BorderWidth = 2
                    vartblCellEndBreak.Height = 14

                    vartblRowBreak.Cells.Add(vartblCellStartBreak)
                    vartblRowBreak.Cells.Add(vartblCellEndBreak)


                End While

            End If
            oRecBreak.Close()
            oRecBreak = Nothing
            oCommandBreak = Nothing

            Dim tblCellTime As New TableCell

            tblCellTime.Font.Name = "Trebuchet MS"
            tblCellTime.BackColor = System.Drawing.ColorTranslator.FromHtml("#dbdbdb")
            tblCellTime.Text = varDateTodaydate
            tblCellTime.HorizontalAlign = HorizontalAlign.Right
            tblCellTime.BorderStyle = BorderStyle.Inset
            tblCellTime.BorderWidth = 2

            Table1.Rows(0).Cells.Add(tblCellTime)



            If varBolCheckLeave = False Then
                If varBolCheckEndBreak = True Then
                    cmdSignIn.Enabled = False
                    cmdEndBreak.Enabled = True
                    cmdSignOut.Enabled = False
                    cmdStartBreak.Enabled = False
                Else
                    If varStrSignInTime <> "" Then
                        If varBolCheckSignOut Then
                            cmdSignOut.Enabled = False
                            cmdStartBreak.Enabled = False
                            cmdSignIn.Enabled = False
                            cmdEndBreak.Enabled = False
                        Else

                            cmdSignOut.Enabled = True
                            cmdStartBreak.Enabled = True
                            cmdSignIn.Enabled = False
                            cmdEndBreak.Enabled = False
                        End If
                    Else
                        cmdSignOut.Enabled = False
                        cmdStartBreak.Enabled = False
                        cmdEndBreak.Enabled = False
                        cmdSignIn.Enabled = True
                    End If
                End If
            Else
                cmdSignOut.Enabled = False
                cmdStartBreak.Enabled = False
                cmdSignIn.Enabled = False
                cmdEndBreak.Enabled = False
            End If
        Catch ex As Exception
            Response.Write(ex.Message)
        Finally
            If oConn.State <> Data.ConnectionState.Closed Then
                oConn.Close()
            End If
        End Try
    End Sub
    Private Function CheckDayLightSavings(ByVal dtDateTime)
        Dim retVal, x, sTempDate
        If InStr(1, CStr(dtDateTime), ".") <> 0 Then
            dtDateTime = Left(dtDateTime, Len(dtDateTime) - 4)
        End If

        If IsDate(dtDateTime) Then
            'We know what to do with any dates within these months
            If Month(dtDateTime) <> 10 And Month(dtDateTime) <> 4 Then
                Select Case Month(dtDateTime)
                    Case 1
                        retVal = False
                    Case 2
                        retVal = False
                    Case 3
                        retVal = False
                    Case 5
                        retVal = True
                    Case 6
                        retVal = True
                    Case 7
                        retVal = True
                    Case 8
                        retVal = True
                    Case 9
                        retVal = True
                    Case 11
                        retVal = False
                    Case 12
                        retVal = False
                End Select
            Else
                'If the month is April, let's check to see if the date is before or after
                '2 AM on the first Sunday of the month
                If Month(dtDateTime) = 4 Then
                    If Day(dtDateTime) < 8 Then
                        For x = 1 To Day(dtDateTime)
                            sTempDate = CStr(Month(dtDateTime)) & "/" & x & "/" & CStr(Year(dtDateTime))
                            If Weekday(sTempDate) = 1 Then
                                If Day(sTempDate) < Day(dtDateTime) Then
                                    'First sunday in April has already passed, so we are now in DST
                                    retVal = True
                                    Exit For
                                Else
                                    'It's the first Sunday in April!
                                    'Let's see if it's past 2 AM. If there is no time part in dtDateTime (time part = "00:00:00"), 
                                    'we are going to assume it's past 2 AM
                                    If (Hour(dtDateTime) >= 2) Or (Hour(dtDateTime) = 0 And Minute(dtDateTime) = 0 And Second(dtDateTime) = 0) Then
                                        retVal = True
                                        Exit For
                                    Else
                                        retVal = False
                                    End If
                                End If
                            Else
                                retVal = False
                            End If
                        Next
                    Else
                        'we know what to do if the day is equal to or greater than the 8th
                        retVal = True
                    End If
                    'If the month is October, let's check to see if date is before or after
                    '2 AM on the last Sunday of the month
                ElseIf Month(dtDateTime) = 10 Then
                    'We know what to do if the day is less than then 25th
                    If Day(dtDateTime) < 25 Then
                        retVal = True
                    Else
                        For x = 25 To Day(dtDateTime)
                            sTempDate = CStr(Month(dtDateTime)) & "/" & x & "/" & CStr(Year(dtDateTime))
                            If Weekday(sTempDate) = 1 Then
                                If Day(sTempDate) < Day(dtDateTime) Then
                                    'last sunday in oct has already passed, so we aren't in DST anymore
                                    retVal = False
                                    Exit For
                                Else
                                    'It's the last Sunday in October!
                                    'Let's see if it's past 2 AM. If there is no time part in dtDateTime (time part = "00:00:00"), 
                                    'we are going to assume it's past 2 AM
                                    If (Hour(dtDateTime) >= 2) Or (Hour(dtDateTime) = 0 And Minute(dtDateTime) = 0 And Second(dtDateTime) = 0) Then
                                        retVal = False
                                        Exit For
                                    Else
                                        retVal = True
                                    End If
                                End If
                            Else
                                retVal = True
                            End If
                        Next
                    End If
                End If
            End If
        Else
            'if the string passed to the function is not a valid date, let's return false.
            retVal = False
        End If
        CheckDayLightSavings = retVal
    End Function
    Protected Sub cmdSignIn_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdSignIn.Click
        Dim varStrInsertstr As String
        Dim varStrUpdatestr As String
        Dim AttDate As Date
        Dim prevDt As Date
        Dim QS As String
        Dim LBS As String
        Dim varFlgSignOut As Boolean
        Dim varStrLeaveStat As String
        Dim varStrState As String
        Dim varIntCheckMonth As Integer

        varStrLeaveStat = ""

        'Check for employee signOut of previousday,if not then signOut automatically.

        prevDt = DateAdd(DateInterval.Day, -1, DateSerial(Year(tempDt), Month(tempDt), Day(tempDt)))
        QS = "SELECT * FROM dbo.tblAttendance WHERE UserID='" & Session("UserID") & "' AND MONTH(AttDate)=" & Month(prevDt) & " AND YEAR(AttDate)=" & Year(prevDt) & " AND DAY(AttDate)=" & Day(prevDt) & ""
        Try
            If oConn.State <> Data.ConnectionState.Open Then oConn.Open()
            Dim command As New Data.SqlClient.SqlCommand(QS, oConn)
            Dim Rec As Data.SqlClient.SqlDataReader = command.ExecuteReader()


            If Rec.HasRows Then
                While Rec.Read
                    If Trim(Rec(4)) = "IN" Then
                        If DateDiff("h", prevDt, DateAdd("h", 18, Rec.GetDateTime(Rec.GetOrdinal("SignIn")))) <= 0 Then
                            varFlgSignOut = True
                        End If
                    Else
                        varFlgSignOut = False
                    End If
                End While
            End If
            Rec.Close()
            Rec = Nothing
            command = Nothing

            If varFlgSignOut Then
                Dim UpdateCmd As New Data.SqlClient.SqlCommand
                UpdateCmd.CommandType = Data.CommandType.Text
                UpdateCmd.CommandText = "UPDATE dbo.tblAttendance SET SignOut='OUT',StatusFlag='OUT' WHERE UserID='" & Session("UserID") & "' AND MONTH(AttDate)=" & Month(prevDt) & " AND YEAR(AttDate)=" & Year(prevDt) & " AND DAY(AttDate)=" & Day(prevDt) & ""
                UpdateCmd.Connection = oConn
                UpdateCmd.ExecuteNonQuery()
            End If

            'Check for DayLight Settings between local time and server time 
            'Then add the respective time in date to get correct date

            If CheckDayLightSavings(Now()) Then
                AttDate = DateAdd(DateInterval.Hour, 9, Now())
                AttDate = DateAdd(DateInterval.Minute, 30, AttDate)
            Else
                AttDate = DateAdd(DateInterval.Hour, 10, Now())
                AttDate = DateAdd(DateInterval.Minute, 30, AttDate)
            End If

            If Hour(AttDate) > "6" And Hour(AttDate) < "12" Then
                varStrInsertstr = "INSERT INTO tblAttendance (UserID,SignIn,AttDate,StatusFlag,Status) VALUES('" & Session("UserID") & "','" & AttDate & "','" & DateSerial(Year(AttDate), Month(AttDate), Day(AttDate)) & "','IN','NP') "
            Else
                'Check if employee is on half day leave or not if yes then set Status HP otherwise P.

                If Trim(varStrLeaveStat) = "Half Day" Then
                    varStrInsertstr = "INSERT INTO tblAttendance (UserID,SignIn,AttDate,StatusFlag,Status) VALUES('" & Session("UserID") & "','" & AttDate & "','" & DateSerial(Year(AttDate), Month(AttDate), Day(AttDate)) & "','IN','HP') "
                Else
                    varStrInsertstr = "INSERT INTO tblAttendance (UserID,SignIn,AttDate,StatusFlag,Status) VALUES('" & Session("UserID") & "','" & AttDate & "','" & DateSerial(Year(AttDate), Month(AttDate), Day(AttDate)) & "','IN','P') "
                End If
            End If

            Dim InsertCmd As New Data.SqlClient.SqlCommand
            InsertCmd.CommandType = Data.CommandType.Text
            InsertCmd.CommandText = varStrInsertstr
            InsertCmd.Connection = oConn
            InsertCmd.ExecuteNonQuery()
            InsertCmd = Nothing
            'Check State of User for adding compoff for national holiday.
            Dim cmdCheckState As New Data.SqlClient.SqlCommand("SELECT State FROM dbo.tblUsers WHERE UserID='" & Session("UserID") & "'", oConn)
            Dim RecCheckState As Data.SqlClient.SqlDataReader = cmdCheckState.ExecuteReader()

            If RecCheckState.HasRows Then
                While RecCheckState.Read
                    varStrState = RecCheckState.GetString(RecCheckState.GetOrdinal("State"))
                End While
            End If

            RecCheckState.Close()
            RecCheckState = Nothing
            cmdCheckState = Nothing

            If UCase(Trim(varStrState)) = UCase(Trim("Karnataka")) Then
                varIntCheckMonth = 11
            ElseIf UCase(Trim(varStrState)) = UCase(Trim("Maharashtra")) Then
                varIntCheckMonth = 5
            End If


            'Check if AttDate is on national holiday,if yes then add respective compoff in earnleave of employee
            LBS = "SELECT CL,EL,TL FROM dbo.tblLeaveBalance WHERE UserID='" & Session("UserID") & "'"
            Dim cmdLBS As New Data.SqlClient.SqlCommand(LBS, oConn)
            Dim RecLBS As Data.SqlClient.SqlDataReader = cmdLBS.ExecuteReader()

            If RecLBS.HasRows Then
                Dim varDblEL As Double
                Dim varDblTL As Double
                While RecLBS.Read
                    If (Day(AttDate) = 15 And Month(AttDate) = 8) Or (Day(AttDate) = 26 And Month(AttDate) = 1) Or (Day(AttDate) = 2 And Month(AttDate) = 10) Or (Day(AttDate) = 1 And Month(AttDate) = varIntCheckMonth) Then
                        varDblEL = RecLBS.GetDouble(RecLBS.GetOrdinal("EL")) + 2
                        varDblTL = RecLBS.GetDouble(RecLBS.GetOrdinal("TL")) + 2

                        Dim UpdateLeaveCmd As New Data.SqlClient.SqlCommand
                        UpdateLeaveCmd.CommandType = Data.CommandType.Text
                        UpdateLeaveCmd.CommandText = "UPDATE tblLeaveBalance SET EL=" & varDblEL & ",TL =" & varDblTL & " WHERE UserID='" & Session("UserID") & "'"
                        UpdateLeaveCmd.Connection = oConn
                        UpdateLeaveCmd.ExecuteNonQuery()
                        UpdateLeaveCmd = Nothing
                    End If
                End While
                RecLBS.Close()
                cmdLBS = Nothing
            End If


            Dim tblRow As New TableRow
            Dim tblCell As New TableCell

            tblCell.ColumnSpan = 2
            tblCell.Font.Name = "Trebuchet MS"
            tblCell.Font.Size = 10
            tblCell.Text = "You have Signed in at " & AttDate
            tblCell.BorderStyle = BorderStyle.Inset
            tblCell.BorderWidth = 2

            tblRow.HorizontalAlign = HorizontalAlign.Left
            tblRow.Cells.Add(tblCell)

            Table1.Rows.Add(tblRow)
            cmdSignIn.Enabled = False
            cmdEndBreak.Enabled = False
            cmdStartBreak.Enabled = True
            cmdSignOut.Enabled = True

            'Code for leave carry forward in next year 

            QS = "SELECT * FROM dbo.tblLeaveStatus WHERE UpdateYear='" & Year(AttDate) & "'"

            Dim cmdCheckCarryForward As New Data.SqlClient.SqlCommand(QS, oConn)
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

                QS = "SELECT UserID,CL,EL,TL FROM dbo.tblLeaveBalance"

                Dim cmdCarryForward As New Data.SqlClient.SqlCommand(QS, oConn)
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
                    RecCarryForward.Close()
                    RecCarryForward = Nothing
                    cmdCarryForward = Nothing

                    Dim InsertLSCmd As New Data.SqlClient.SqlCommand
                    InsertLSCmd.CommandType = Data.CommandType.Text
                    InsertLSCmd.CommandText = "INSERT INTO dbo.tblLeaveStatus(UpdateYear,UpdateLeave) VALUES('" & Year(AttDate) & "',1) "
                    InsertLSCmd.Connection = oConn
                    InsertLSCmd.ExecuteNonQuery()
                    InsertLSCmd = Nothing
                End If

                Dim i As Int16

                For i = 0 To varUserID.Count - 1
                    Dim UpdateLBLCmd As New Data.SqlClient.SqlCommand
                    UpdateLBLCmd.CommandType = Data.CommandType.Text
                    UpdateLBLCmd.CommandText = "UPDATE tblLeaveBalance SET EL=" & CDbl(varEL(i)) & ",TL =" & CDbl(varTL(i)) & ",CL=" & CDbl(varCL(i)) & " WHERE UserID='" & varUserID(i) & "'"
                    UpdateLBLCmd.Connection = oConn
                    UpdateLBLCmd.ExecuteNonQuery()
                    UpdateLBLCmd = Nothing
                Next

            End If
        Catch ex As Exception
            Response.Write(ex.Message)
        Finally
            If oConn.State <> Data.ConnectionState.Closed Then
                oConn.Close()
            End If
        End Try
    End Sub
    Protected Sub cmdStartBreak_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdStartBreak.Click
        Dim AttDate
        Dim varStrInsertstr As String

        'Check for DayLight Settings between local time and server time 
        'Then add the respective time in date to get correct date

        'If CheckDayLightSavings(Now()) Then
        '    AttDate = DateAdd(DateInterval.Hour, 9, Now())
        '    AttDate = DateAdd(DateInterval.Minute, 30, AttDate)
        'Else
        '    AttDate = DateAdd(DateInterval.Hour, 10, Now())
        '    AttDate = DateAdd(DateInterval.Minute, 30, AttDate)
        'End If

        'check correct AttDate
        Try

            If oConn.State <> Data.ConnectionState.Open Then oConn.Open()
            Dim oCommandAttDate As New Data.SqlClient.SqlCommand("SELECT AttDate FROM dbo.tblAttendance WHERE UserID='" & Session("UserID") & "' AND StatusFlag='IN' AND Status='P' AND (SignOut<>'Out' OR SignOut IS NULL)", oConn)
            Dim oRecAttDate As Data.SqlClient.SqlDataReader = oCommandAttDate.ExecuteReader()

            If oRecAttDate.HasRows Then
                While oRecAttDate.Read
                    AttDate = oRecAttDate.GetDateTime(oRecAttDate.GetOrdinal("AttDate"))
                End While
            End If
            oRecAttDate.Close()
            oRecAttDate = Nothing
            oCommandAttDate = Nothing

            'Insert Break time of employee
            varStrInsertstr = "INSERT INTO tblBreak (UserID,sTime,Status,AttDate) VALUES('" & Session("UserID") & "','" & TimeSerial(Hour(AttDate), Minute(AttDate), Second(AttDate)) & "','START','" & DateSerial(Year(AttDate), Month(AttDate), Day(AttDate)) & "')"

            Dim InsertCmd As New Data.SqlClient.SqlCommand
            InsertCmd.CommandType = Data.CommandType.Text
            InsertCmd.CommandText = varStrInsertstr
            InsertCmd.Connection = oConn
            InsertCmd.ExecuteNonQuery()
            InsertCmd = Nothing

            Dim vartblRow As New TableRow
            Dim vartblSCell As New TableCell
            Dim vartblECell As New TableCell


            vartblSCell.Font.Name = "Trebuchet MS"
            vartblSCell.Text = "Your Break Starts at " & TimeSerial(Hour(AttDate), Minute(AttDate), Second(AttDate))
            vartblSCell.BorderStyle = BorderStyle.Inset
            vartblSCell.BorderWidth = 2
            vartblSCell.Font.Size = 10

            vartblECell.Font.Name = "Trebuchet MS"
            vartblECell.Text = "Your Break Ends at "
            vartblECell.BorderStyle = BorderStyle.Inset
            vartblECell.BorderWidth = 2
            vartblECell.Font.Size = 10

            vartblRow.HorizontalAlign = HorizontalAlign.Left
            vartblRow.Cells.Add(vartblSCell)
            vartblRow.Cells.Add(vartblECell)

            Table1.Rows.Add(vartblRow)

            cmdSignIn.Enabled = False
            cmdStartBreak.Enabled = False
            cmdSignOut.Enabled = False
            cmdEndBreak.Enabled = True
        Catch ex As Exception
            Response.Write(ex.Message)
        Finally
            If oConn.State <> Data.ConnectionState.Closed Then
                oConn.Close()
            End If
        End Try
    End Sub
    Protected Sub cmdEndBreak_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdEndBreak.Click
        Dim varStrUpdatestr

        'Check for DayLight Settings between local time and server time 
        'Then add the respective time in date to get correct date

        Dim AttDate
        If CheckDayLightSavings(Now()) Then
            AttDate = DateAdd(DateInterval.Hour, 9, Now())
            AttDate = DateAdd(DateInterval.Minute, 30, AttDate)
        Else
            AttDate = DateAdd(DateInterval.Hour, 10, Now())
            AttDate = DateAdd(DateInterval.Minute, 30, AttDate)
        End If
        varStrUpdatestr = "UPDATE tblBreak SET eTime='" & TimeSerial(Hour(AttDate), Minute(AttDate), Second(AttDate)) & "',Status='End' WHERE UserID = '" & Session("UserID") & "' AND AttDate= '" & DateSerial(Year(AttDate), Month(AttDate), Day(AttDate)) & "'"

        Dim UpdateCmd As New Data.SqlClient.SqlCommand
        UpdateCmd.CommandType = Data.CommandType.Text
        UpdateCmd.CommandText = varStrUpdatestr
        Try
            If oConn.State <> Data.ConnectionState.Open Then oConn.Open()
            UpdateCmd.Connection = oConn
            UpdateCmd.ExecuteNonQuery()
            UpdateCmd = Nothing

            Dim vartblRows = Table1.Rows.Count
            Table1.Rows.Item(vartblRows - 1).Cells(1).Font.Name = "Trebuchet MS"
            Table1.Rows.Item(vartblRows - 1).Cells(1).Font.Size = 10
            Table1.Rows.Item(vartblRows - 1).Cells(1).Text = "Your Break Ends at " & TimeSerial(Hour(AttDate), Minute(AttDate), Second(AttDate))

            cmdSignIn.Enabled = False
            cmdStartBreak.Enabled = True
            cmdSignOut.Enabled = True
            cmdEndBreak.Enabled = False
        Catch ex As Exception
            Response.Write(ex.Message)
        Finally
            If oConn.State <> Data.ConnectionState.Closed Then
                oConn.Close()
            End If
        End Try
    End Sub
    Protected Sub cmdSignOut_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdSignOut.Click
        Dim varStrUpdatestr
        Dim AttDate

        'Check for DayLight Settings between local time and server time 
        'Then add the respective time in date to get correct date

        If CheckDayLightSavings(Now()) Then
            AttDate = DateAdd(DateInterval.Hour, 9, Now())
            AttDate = DateAdd(DateInterval.Minute, 30, AttDate)
        Else
            AttDate = DateAdd(DateInterval.Hour, 10, Now())
            AttDate = DateAdd(DateInterval.Minute, 30, AttDate)
        End If
        varStrUpdatestr = "UPDATE dbo.tblAttendance SET SignOut='" & AttDate & "',StatusFlag='OUT' WHERE UserID = '" & Session("UserID") & "' AND StatusFlag='IN' AND MONTH(AttDate)=" & Month(AttDate) & " AND YEAR(AttDate)=" & Year(AttDate) & " AND DAY(AttDate)=" & Day(AttDate) & ""

        Dim UpdateCmd As New Data.SqlClient.SqlCommand
        UpdateCmd.CommandType = Data.CommandType.Text
        UpdateCmd.CommandText = varStrUpdatestr
        Try
            If oConn.State <> Data.ConnectionState.Open Then oConn.Open()
            UpdateCmd.Connection = oConn
            UpdateCmd.ExecuteNonQuery()
            UpdateCmd = Nothing

            'Sign Out for the day
            Response.Write("<center><font face='Trebuchet MS' size='2' color='#000000'>You have Signed Out for the day</Font>" + "<BR></center>")
            Response.End()
        Catch ex As Exception
            Response.Write(ex.Message)
        Finally
            If oConn.State <> Data.ConnectionState.Closed Then
                oConn.Close()
            End If
        End Try
    End Sub
    Protected Sub Page_Unload(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Unload
        If oConn.State <> Data.ConnectionState.Closed Then
            oConn.Close()
            oConn = Nothing
        End If
    End Sub
End Class
