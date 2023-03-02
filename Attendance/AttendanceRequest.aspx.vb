Imports System.Web.Mail
Imports System.Net.Mail.SmtpClient
Imports System.Net.Mail
Imports System.Net.NetworkCredential
Partial Class AttendanceRequest
    Inherits BasePage
    Dim ConString As String
    Dim oConn As New Data.SqlClient.SqlConnection
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        ConString = System.Configuration.ConfigurationManager.AppSettings("ETSCon")
        oConn.ConnectionString = ConString
        Try
            oConn.Open()
            Session("UserID") = "838d2a09-7374-4bbd-9a6a-f1ed1621c536"

            If Not Page.IsPostBack Then
                imgSDate.NavigateUrl = "javascript:calendarPicker('document.all." + txtAttDate.ClientID.ToString() + "');"

            End If
        Catch ex As Exception
            Response.Write(ex.Message)
        Finally
            If oConn.State <> Data.ConnectionState.Closed Then
                oConn.Close()
            End If
        End Try
    End Sub
    Protected Sub SendRequest_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles SendRequest.Click
        Dim varStrEmpName As String
        Dim varDateTodaydate As Date
        Dim varBolCheckDayLight As Boolean
        Dim varDtAttDate As Date
        Dim varBolAttendanceCheck As Boolean
        Dim varBolAttendanceReq As Boolean
        Dim varDtStartDate As Date
        Dim varDtEndDate As Date
        Dim varStrInsert As String
        Dim varStrPri
        Dim varStrSec
        Dim varStrThi
        Dim varToMail
        Dim varSuperID
        Dim varBolWhileFlag As Boolean
        Dim varStrPMailID As String
        Dim varStrSMailID As String
        Dim varStrTMailID As String
        Dim varStrEmpMailCC As String


        varBolAttendanceCheck = False
        varBolAttendanceReq = False

        varBolCheckDayLight = CheckDayLightSavings(Now())
        If varBolCheckDayLight = True Then
            varDateTodaydate = DateAdd(DateInterval.Hour, 9, Now())
            varDateTodaydate = DateAdd(DateInterval.Minute, 30, varDateTodaydate)
        Else
            varDateTodaydate = DateAdd(DateInterval.Hour, 10, Now())
            varDateTodaydate = DateAdd(DateInterval.Minute, 30, varDateTodaydate)
        End If

        varDtAttDate = Request.Form("txtAttDate")
        Try

            If oConn.State <> Data.ConnectionState.Open Then oConn.Open()
            Dim oCommandCheckAttendance As New Data.SqlClient.SqlCommand("SELECT * FROM dbo.tblAttendance WHERE UserID='" & Session("UserID") & "'AND AttDate='" & DateSerial(Year(varDtAttDate), Month(varDtAttDate), Day(varDtAttDate)) & "'", oConn)
            Dim oRecCheckAttendance As Data.SqlClient.SqlDataReader = oCommandCheckAttendance.ExecuteReader()

            If oRecCheckAttendance.HasRows Then
                While oRecCheckAttendance.Read
                    varBolAttendanceCheck = True
                    Exit While
                End While
            End If

            oRecCheckAttendance.Close()
            oRecCheckAttendance = Nothing
            oCommandCheckAttendance = Nothing

            If varBolAttendanceCheck = False Then
                Dim oCommandChkAttendanceRequest As New Data.SqlClient.SqlCommand("SELECT * FROM dbo.tblAttendanceRequest WHERE UserID='" & Session("UserID") & "'AND AttDate='" & DateSerial(Year(varDtAttDate), Month(varDtAttDate), Day(varDtAttDate)) & "'", oConn)
                Dim oRecChkAttendanceRequest As Data.SqlClient.SqlDataReader = oCommandChkAttendanceRequest.ExecuteReader()

                If oRecChkAttendanceRequest.HasRows Then
                    While oRecChkAttendanceRequest.Read
                        varBolAttendanceReq = True
                        Exit While
                    End While
                End If
                oRecChkAttendanceRequest.Close()
                oRecChkAttendanceRequest = Nothing
                oCommandChkAttendanceRequest = Nothing
            Else
                Response.Write("<script type=""text/javascript"" language=javascript> alert(""You have already send Attendance for this day !!!"");window.location.href='AttendanceRequest.aspx';</script>")
            End If



            If varBolAttendanceReq = True Then
                Response.Write("<script type=""text/javascript"" language=javascript> alert(""You have already send Attendance for this day !!!"");window.location.href='AttendanceRequest.aspx';</script>")
            End If


            If varBolAttendanceCheck = False And varBolAttendanceReq = False Then

                'Get all the information for the employee.
                Dim oCommand As New Data.SqlClient.SqlCommand("SELECT U.FirstName,U.LastName,U.OfficialMailID FROM dbo.tblLeaveBalance LB INNER JOIN dbo.tblUsers U ON LB.UserID=U.UserID WHERE U.UserID='" & Session("UserID") & "'", oConn)
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
                Dim oCommandSV As New Data.SqlClient.SqlCommand("SELECT LevelNo,SuperVisorID FROM dbo.tblDeptSuperVisorAssign S INNER JOIN dbo.tblDepartments D ON  S.DepartmentID=D.DepartmentID INNER JOIN dbo.tblUsers U ON U.DepartmentID=D.DepartmentID WHERE U.UserID='" & Session("UserID") & "'", oConn)
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

                'Store Mail ID of all SuperVisors
                Dim oCommandMailID As New Data.SqlClient.SqlCommand("SELECT UserID,OfficialMailID FROM dbo.tblUsers U WHERE U.UserID IN ('" & varStrPri & "','" & varStrSec & "','" & varStrThi & "')", oConn)
                Dim oRecMailID As Data.SqlClient.SqlDataReader = oCommandMailID.ExecuteReader()
                If oRecMailID.HasRows Then
                    While oRecMailID.Read
                        If oRecMailID(oRecMailID.GetOrdinal("UserID")).ToString = varStrPri Then
                            varStrPMailID = oRecMailID(oRecMailID.GetOrdinal("OfficialMailID"))
                        ElseIf oRecMailID(oRecMailID.GetOrdinal("UserID")).ToString = varStrSec Then
                            varStrSMailID = oRecMailID(oRecMailID.GetOrdinal("OfficialMailID"))
                        ElseIf oRecMailID(oRecMailID.GetOrdinal("UserID")).ToString = varStrThi Then
                            varStrTMailID = oRecMailID(oRecMailID.GetOrdinal("OfficialMailID"))
                        End If
                    End While
                End If
                oRecMailID.Close()
                oRecMailID = Nothing
                oCommandMailID = Nothing

                'Check if primary Supervisor is on leave or not
                Dim oCommandPri As New Data.SqlClient.SqlCommand("SELECT U.UserID,U.OfficialMailID,L.StartDate,L.EndDate FROM dbo.tblUsers U INNER JOIN dbo.tblLeave L  ON U.UserID = L.UserID WHERE U.UserID='" & varStrPri & "'", oConn)
                Dim oRecPri As Data.SqlClient.SqlDataReader = oCommandPri.ExecuteReader()

                If oRecPri.HasRows Then
                    While oRecPri.Read
                        If varBolWhileFlag = False Then
                            varDtStartDate = oRecPri.GetDateTime(oRecPri.GetOrdinal("StartDate"))
                            varDtEndDate = oRecPri.GetDateTime(oRecPri.GetOrdinal("EndDate"))
                            While varDtEndDate >= varDtStartDate
                                If DateDiff(DateInterval.Day, DateSerial(Year(varDtStartDate), Month(varDtStartDate), Day(varDtStartDate)), DateSerial(Year(varDateTodaydate), Month(varDateTodaydate), Day(varDateTodaydate))) = 0 Then
                                    varToMail = ""
                                    varBolWhileFlag = True
                                    Exit While
                                End If
                                varDtStartDate = DateAdd(DateInterval.Day, 1, varDtStartDate)
                            End While
                        Else
                            Exit While
                        End If
                    End While
                    If varBolWhileFlag = False Then
                        varSuperID = varStrPri
                        varToMail = varStrPMailID
                    Else
                        varBolWhileFlag = False
                    End If
                Else
                    varSuperID = varStrPri
                    varToMail = varStrPMailID
                End If

                oRecPri.Close()
                oRecPri = Nothing
                oCommandPri = Nothing

                'Check if Secondary Supervisor is on leave or not
                If varToMail = "" Then
                    Dim oCommandSec As New Data.SqlClient.SqlCommand("SELECT U.UserID,U.OfficialMailID,L.StartDate,L.EndDate FROM dbo.tblUsers U INNER JOIN dbo.tblLeave L  ON U.UserID = L.UserID WHERE U.UserID='" & varStrSec & "'", oConn)
                    Dim oRecSec As Data.SqlClient.SqlDataReader = oCommandSec.ExecuteReader()

                    If oRecSec.HasRows Then
                        While oRecSec.Read
                            If varBolWhileFlag = False Then
                                varDtStartDate = oRecSec.GetDateTime(oRecSec.GetOrdinal("StartDate"))
                                varDtEndDate = oRecSec.GetDateTime(oRecSec.GetOrdinal("EndDate"))
                                While varDtEndDate >= varDtStartDate
                                    If DateDiff(DateInterval.Day, DateSerial(Year(varDtStartDate), Month(varDtStartDate), Day(varDtStartDate)), DateSerial(Year(varDateTodaydate), Month(varDateTodaydate), Day(varDateTodaydate))) = 0 Then
                                        varToMail = ""
                                        varBolWhileFlag = True
                                        Exit While
                                    End If
                                    varDtStartDate = DateAdd(DateInterval.Day, 1, varDtStartDate)
                                End While
                            Else
                                Exit While
                            End If
                        End While
                        If varBolWhileFlag = False Then
                            varSuperID = varStrSec
                            varToMail = varStrSMailID
                        Else
                            varBolWhileFlag = False
                        End If
                    Else
                        varSuperID = varStrSec
                        varToMail = varStrSMailID
                    End If

                    oRecSec.Close()
                    oRecSec = Nothing
                    oCommandSec = Nothing
                End If

                'Check if Thrid level Supervisor is on leave or not
                If varToMail = "" Then
                    Dim oCommandThi As New Data.SqlClient.SqlCommand("SELECT U.UserID,U.OfficialMailID,L.StartDate,L.EndDate FROM dbo.tblUsers U INNER JOIN dbo.tblLeave L  ON U.UserID = L.UserID WHERE U.UserID='" & varStrThi & "'", oConn)
                    Dim oRecThi As Data.SqlClient.SqlDataReader = oCommandThi.ExecuteReader()

                    If oRecThi.HasRows Then
                        While oRecThi.Read
                            If varBolWhileFlag = False Then
                                varDtStartDate = oRecThi.GetDateTime(oRecThi.GetOrdinal("StartDate"))
                                varDtEndDate = oRecThi.GetDateTime(oRecThi.GetOrdinal("EndDate"))
                                While varDtEndDate >= varDtStartDate
                                    If DateDiff(DateInterval.Day, DateSerial(Year(varDtStartDate), Month(varDtStartDate), Day(varDtStartDate)), DateSerial(Year(varDateTodaydate), Month(varDateTodaydate), Day(varDateTodaydate))) = 0 Then
                                        varToMail = ""
                                        varBolWhileFlag = True
                                        Exit While
                                    End If
                                    varDtStartDate = DateAdd(DateInterval.Day, 1, varDtStartDate)
                                End While
                            Else
                                Exit While
                            End If
                        End While
                        If varBolWhileFlag = False Then
                            varSuperID = varStrSec
                            varToMail = varStrTMailID
                        Else
                            varBolWhileFlag = False
                        End If
                    Else
                        varSuperID = varStrThi
                        varToMail = varStrTMailID
                    End If

                    oRecThi.Close()
                    oRecThi = Nothing
                    oCommandThi = Nothing

                End If


                varStrInsert = "INSERT INTO dbo.tblAttendanceRequest(UserID,AttDate,SignIn,SignOut,Reason,ApproveBy,Status,AppDate) VALUES('" & Session("UserID") & "','" & DateSerial(Year(Request("txtAttDate")), Month(Request("txtAttDate")), Day(Request("txtAttDate"))) & "','" & Request("txtInTime") & "','" & Request("txtOutTime") & "','" & Request("txtReason") & "','" & varSuperID & "','Pending','" & varDateTodaydate & "')"

                Dim InsertCmd As New Data.SqlClient.SqlCommand
                InsertCmd.CommandType = Data.CommandType.Text
                InsertCmd.CommandText = varStrInsert
                InsertCmd.Connection = oConn
                InsertCmd.ExecuteNonQuery()
                InsertCmd = Nothing

                Dim objsmtp As New SmtpClient("smtp.edictateindia.com")
                objsmtp.Credentials = New System.Net.NetworkCredential("edictatemail", "ed1ctatema!l")

                Dim MailMsg As New System.Net.Mail.MailMessage
                MailMsg.From = New MailAddress(varStrEmpMailCC)

                MailMsg.To.Add(varToMail)
                MailMsg.CC.Add(varStrEmpMailCC)
                MailMsg.Bcc.Add("apagare@edictate.com")


                MailMsg.Subject = "Attendance Request of " & varStrEmpName

                Dim Text
                Text = "<font face=""Trebuchet MS"" size=""2"" color=""#000080"">" & varStrEmpName & " has requested for the " & varDtAttDate
                Text = Text & " attendance. " & "<br>" & "Reason: " & Request("txtReason") & "</FONT>"


                MailMsg.Body = Text
                MailMsg.IsBodyHtml = True

                objsmtp.DeliveryMethod = SmtpDeliveryMethod.PickupDirectoryFromIis


                Response.Write("From : ")
                Response.Write(MailMsg.From)
                Response.Write("<BR>")
                Response.Write("To : ")
                Response.Write(MailMsg.To)
                Response.Write("<BR>")
                Response.Write("Bcc : ")
                Response.Write(MailMsg.Bcc)
                Response.Write("<BR>")
                Response.Write("Body : ")
                Response.Write(MailMsg.Body)
                Response.Write("<BR>")
                Response.End()
                'Commented by anil for testing purpose.
                'objsmtp.Send(MailMsg)
                'Response.Write("<script type=""text/javascript"" language=javascript> alert(""Attendance Request Sent!!!"");window.location.href='AttendanceRequest.aspx';</script>")
                'anil ended

            End If
        Catch ex As Exception
            Response.Write(ex.Message)
        Finally
            If oConn.State <> Data.ConnectionState.Closed Then
                oConn.Close()
            End If
        End Try
    End Sub
    Function CheckTimeFormat(ByVal txtTime, ByVal txtControl)
        Dim varStrSpaceSplit
        Dim varStrDotSplit
        Dim varTimeFormat As String

        varStrSpaceSplit = Split(Trim(txtTime), " ")
        If UBound(varStrSpaceSplit) = 1 Then
            varTimeFormat = UCase(varStrSpaceSplit(1))
            varStrDotSplit = Split(Trim(varStrSpaceSplit(0)), ":")

            If Trim(UCase(varTimeFormat)) = Trim(UCase("AM")) Or Trim(UCase(varTimeFormat)) = Trim(UCase("PM")) Then

                If CInt(varStrDotSplit(0)) > 12 Then
                    If Trim(UCase(txtControl)) = Trim(UCase("In-Time")) Then
                        Response.Write("<script type=""text/javascript"" language=javascript> alert(""Please enter valid In-Time"");</script>")
                        Return False
                    End If
                    If Trim(UCase(txtControl)) = Trim(UCase("Out-Time")) Then
                        Response.Write("<script type=""text/javascript"" language=javascript> alert(""Please enter valid Out-Time"");</script>")
                        Return False
                    End If
                End If

                If UBound(varStrDotSplit) = 2 Then
                    If CInt(varStrDotSplit(1)) > 59 Then
                        If Trim(UCase(txtControl)) = Trim(UCase("In-Time")) Then
                            Response.Write("<script type=""text/javascript"" language=javascript> alert(""Please enter valid In-Time"");</script>")
                            Return False
                        End If
                        If Trim(UCase(txtControl)) = Trim(UCase("Out-Time")) Then
                            Response.Write("<script type=""text/javascript"" language=javascript> alert(""Please enter valid Out-Time"");</script>")
                            Return False
                        End If
                    End If

                    If CInt(varStrDotSplit(2)) > 59 Then
                        If Trim(UCase(txtControl)) = Trim(UCase("In-Time")) Then
                            Response.Write("<script type=""text/javascript"" language=javascript> alert(""Please enter valid In-Time"");</script>")
                            Return False
                        End If
                        If Trim(UCase(txtControl)) = Trim(UCase("Out-Time")) Then
                            Response.Write("<script type=""text/javascript"" language=javascript> alert(""Please enter valid Out-Time"");</script>")
                            Return False
                        End If
                    End If
                Else
                    If Trim(UCase(txtControl)) = Trim(UCase("In-Time")) Then
                        Response.Write("<script type=""text/javascript"" language=javascript> alert(""Please enter valid In-Time"");</script>")
                        Return False
                    End If
                    If Trim(UCase(txtControl)) = Trim(UCase("Out-Time")) Then
                        Response.Write("<script type=""text/javascript"" language=javascript> alert(""Please enter valid Out-Time"");</script>")
                        Return False
                    End If
                End If
            Else
                If Trim(UCase(txtControl)) = Trim(UCase("In-Time")) Then
                    Response.Write("<script type=""text/javascript"" language=javascript> alert(""Please enter valid In-Time"");</script>")
                    Return False
                End If
                If Trim(UCase(txtControl)) = Trim(UCase("Out-Time")) Then
                    Response.Write("<script type=""text/javascript"" language=javascript> alert(""Please enter valid Out-Time"");</script>")
                    Return False
                End If
            End If
        Else
            If Trim(UCase(txtControl)) = Trim(UCase("In-Time")) Then
                Response.Write("<script type=""text/javascript"" language=javascript> alert(""Please enter valid In-Time"");txtInTime.focus();</script>")
                Return False
            End If
            If Trim(UCase(txtControl)) = Trim(UCase("Out-Time")) Then
                Response.Write("<script type=""text/javascript"" language=javascript> alert(""Please enter valid Out-Time"");</script>")
                Return False
            End If
        End If
    End Function
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
End Class
