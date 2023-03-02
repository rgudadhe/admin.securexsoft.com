Imports System.Web.Mail
Imports System.Net.Mail.SmtpClient
Imports System.Net.Mail
Imports System.Net.NetworkCredential
Partial Class AttendanceAction
    Inherits BasePage
    Dim ConString As String
    Dim oConn As New Data.SqlClient.SqlConnection
    Dim varStrMailFrom As String
    Dim varStrToMail As String
    Dim varDtTodayDate As Date
    Dim varStrUserID As String
    Dim varDtAttDate As Date
    Dim varDtTempDate As Date
    Dim varDtTempAttDate As Date
    Dim varStrMailTo As String
    Dim varStrSignIn As String
    Dim varStrSignOut As String
    Dim varInTime As DateTime
    Dim varOutTime As DateTime
    Dim varITime As DateTime
    Dim varOTime As DateTime
    Dim varTimeSpan As TimeSpan
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        ConString = System.Configuration.ConfigurationManager.AppSettings("ETSCon")
        oConn.ConnectionString = ConString
        Try
            oConn.Open()

            Dim oCommandMailFrom As New Data.SqlClient.SqlCommand("SELECT OfficialMailID FROM dbo.tblUsers U WHERE UserID='" & Session("UserID") & "'", oConn)
            Dim oRecMailFrom As Data.SqlClient.SqlDataReader = oCommandMailFrom.ExecuteReader()

            If oRecMailFrom.HasRows Then
                While oRecMailFrom.Read
                    varStrMailFrom = oRecMailFrom.GetString(oRecMailFrom.GetOrdinal("OfficialMailID"))
                End While
            End If

            oRecMailFrom.Close()
            oRecMailFrom = Nothing
            oCommandMailFrom = Nothing

            Dim oCommandAttReqInfo As New Data.SqlClient.SqlCommand("SELECT A.UserID,A.AttDate,A.SignIn,A.SignOut,U.OfficialMailID FROM dbo.tblAttendanceRequest A  INNER JOIN dbo.tblUsers U ON U.UserID=A.UserID WHERE AttReqID='" & Request("AttReqID") & "'", oConn)
            Dim oRecAttReqInfo As Data.SqlClient.SqlDataReader = oCommandAttReqInfo.ExecuteReader()

            If oRecAttReqInfo.HasRows Then
                While oRecAttReqInfo.Read
                    varStrUserID = oRecAttReqInfo.GetGuid(oRecAttReqInfo.GetOrdinal("UserID")).ToString
                    varDtAttDate = oRecAttReqInfo.GetDateTime(oRecAttReqInfo.GetOrdinal("AttDate"))
                    varStrMailTo = oRecAttReqInfo.GetString(oRecAttReqInfo.GetOrdinal("OfficialMailID"))
                    varStrSignIn = oRecAttReqInfo.GetString(oRecAttReqInfo.GetOrdinal("SignIn"))
                    varStrSignOut = oRecAttReqInfo.GetString(oRecAttReqInfo.GetOrdinal("SignOut"))
                End While
            End If

            oRecAttReqInfo.Close()
            oRecAttReqInfo = Nothing
            oCommandAttReqInfo = Nothing

            If Trim(UCase(Request("Str"))) = Trim(UCase("Approve")) Then
                Table1.Visible = False

                varInTime = varStrSignIn
                varOutTime = varStrSignOut

                varITime = varDtAttDate & " " & TimeSerial(Hour(varInTime), Minute(varInTime), Second(varInTime))

                If varOutTime > varInTime Then
                    varOTime = varDtAttDate & " " & TimeSerial(Hour(varOutTime), Minute(varOutTime), Second(varOutTime))
                ElseIf varInTime > varOutTime Then
                    varOTime = DateAdd(DateInterval.Day, 1, varDtAttDate) & " " & TimeSerial(Hour(varOutTime), Minute(varOutTime), Second(varOutTime))
                End If


                If CheckDayLightSavings(Now()) = True Then
                    varDtTodayDate = DateAdd(DateInterval.Hour, 9, Now())
                    varDtTodayDate = DateAdd(DateInterval.Minute, 30, varDtTodayDate)
                Else
                    varDtTodayDate = DateAdd(DateInterval.Hour, 10, Now())
                    varDtTodayDate = DateAdd(DateInterval.Minute, 30, varDtTodayDate)
                End If

                'Update Attendance Request of Employee
                Dim UpdateCmdAttReq As New Data.SqlClient.SqlCommand
                UpdateCmdAttReq.CommandType = Data.CommandType.Text
                UpdateCmdAttReq.CommandText = "UPDATE dbo.tblAttendanceRequest SET Status='Approved',ApproveDate='" & varDtTodayDate & "' WHERE AttReqID='" & Request.QueryString("AttReqID") & "'"
                UpdateCmdAttReq.Connection = oConn
                UpdateCmdAttReq.ExecuteNonQuery()
                UpdateCmdAttReq = Nothing

                'Insert Attendance of Employee
                Dim InsertCmdAttReq As New Data.SqlClient.SqlCommand
                InsertCmdAttReq.CommandType = Data.CommandType.Text
                InsertCmdAttReq.CommandText = "INSERT INTO dbo.tblAttendance (UserID,SignIn,SignOut,AttDate,StatusFlag,Status) VALUES('" & varStrUserID & "','" & varITime & "','" & varOTime & "','" & varDtAttDate & "','OUT','P')"
                InsertCmdAttReq.Connection = oConn
                InsertCmdAttReq.ExecuteNonQuery()
                InsertCmdAttReq = Nothing

                'Sending mail to employee after leave approval

                Dim MailMsgLA As New System.Net.Mail.MailMessage
                Dim objsmtpLA As New SmtpClient("smtp.edictateindia.com")
                objsmtpLA.Credentials = New System.Net.NetworkCredential("edictatemail", "ed1ctatema!l")

                MailMsgLA.From = New MailAddress(varStrMailFrom)
                MailMsgLA.To.Add(varStrMailTo)
                MailMsgLA.Bcc.Add("apagare@edictate.com")

                MailMsgLA.Subject = "Attendance is sanctioned"

                Dim Text
                Text = "<font face=""Trebuchet MS"" size=""2"" color=""#000080"">Your attendance "
                Text = Text & " for " & varDtAttDate & " is sanctioned. </FONT>"

                MailMsgLA.Body = Text
                MailMsgLA.IsBodyHtml = True

                objsmtpLA.DeliveryMethod = SmtpDeliveryMethod.PickupDirectoryFromIis

                Response.Write("From : ")
                Response.Write(MailMsgLA.From)
                Response.Write("<BR>")
                Response.Write("To : ")
                Response.Write(MailMsgLA.To)
                Response.Write("<BR>")
                Response.Write("Bcc : ")
                Response.Write(MailMsgLA.Bcc)
                Response.Write("<BR>")
                Response.Write("Body : ")
                Response.Write(MailMsgLA.Body)
                Response.Write("<BR>")
                Response.End()
                'Commented by anil for testing purpose.

                'objsmtp.Send(MailMsg)
                Response.Redirect("AttendanceApprove.aspx")

                'anil ended

            Else
                Table1.Visible = True
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
    Protected Sub btnDisapprove_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnDisapprove.Click

        If CheckDayLightSavings(Now()) = True Then
            varDtTodayDate = DateAdd(DateInterval.Hour, 9, Now())
            varDtTodayDate = DateAdd(DateInterval.Minute, 30, varDtTodayDate)
        Else
            varDtTodayDate = DateAdd(DateInterval.Hour, 10, Now())
            varDtTodayDate = DateAdd(DateInterval.Minute, 30, varDtTodayDate)
        End If

        'Update Attendance Request of Employee
        Dim UpdateCmdAttReq As New Data.SqlClient.SqlCommand
        UpdateCmdAttReq.CommandType = Data.CommandType.Text
        UpdateCmdAttReq.CommandText = "UPDATE dbo.tblAttendanceRequest SET Status='DisApproved',ApproveDate='" & varDtTodayDate & "',DisReason='" & Request.Form("txtReason") & "' WHERE AttReqID='" & Request.QueryString("AttReqID") & "'"
        Try

            If oConn.State <> Data.ConnectionState.Open Then oConn.Open()
            UpdateCmdAttReq.Connection = oConn
            UpdateCmdAttReq.ExecuteNonQuery()
            UpdateCmdAttReq = Nothing

            'Insert Attendance of Employee
            Dim InsertCmdAttReq As New Data.SqlClient.SqlCommand
            InsertCmdAttReq.CommandType = Data.CommandType.Text
            InsertCmdAttReq.CommandText = "INSERT INTO dbo.tblAttendance (UserID,AttDate,StatusFlag,Status) VALUES('" & varStrUserID & "','" & varDtAttDate & "','OUT','A')"
            InsertCmdAttReq.Connection = oConn
            InsertCmdAttReq.ExecuteNonQuery()
            InsertCmdAttReq = Nothing

            Dim MailMsg As New System.Net.Mail.MailMessage
            Dim objsmtp As New SmtpClient("smtp.edictateindia.com")
            objsmtp.Credentials = New System.Net.NetworkCredential("edictatemail", "ed1ctatema!l")

            MailMsg.From = New MailAddress(varStrMailFrom)
            MailMsg.To.Add(varStrMailTo)
            MailMsg.Bcc.Add("apagare@edictate.com")

            MailMsg.Subject = "Attendance is Not Sanctioned"

            Dim Text
            Text = "<font face=""Trebuchet MS"" size=""2"" color=""#000080"">Your Attendance "
            Text = Text & " On " & varDtAttDate & " is not sanctioned. </FONT>"

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
        Catch ex As Exception
            Response.Write(ex.Message)
        Finally
            If oConn.State <> Data.ConnectionState.Closed Then
                oConn.Close()
            End If
        End Try
            
    End Sub
End Class
