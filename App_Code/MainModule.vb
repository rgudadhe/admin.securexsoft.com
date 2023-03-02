Imports System
Imports Microsoft.VisualBasic
Imports System.Web.Mail
Imports System.Net.Mail.SmtpClient
Imports System.Net.Mail
Imports System.Net.NetworkCredential


Public Class MainModule
    Public varStrAdminCC As String
    Public varStrBcc As String
    Public varStrHBAFromMail As String
    Public varStrHBAToMail As String
    Public varAttendanceReport As String
    Public ConString As String
    Public oConn As New Data.SqlClient.SqlConnection
    'Public Function CheckDayLightSavings(ByVal dtDateTime)
    '    Dim retVal, x, sTempDate
    '    If InStr(1, CStr(dtDateTime), ".") <> 0 Then
    '        dtDateTime = Left(dtDateTime, Len(dtDateTime) - 4)
    '    End If

    '    If IsDate(dtDateTime) Then
    '        'We know what to do with any dates within these months
    '        If Month(dtDateTime) <> 10 And Month(dtDateTime) <> 4 Then
    '            Select Case Month(dtDateTime)
    '                Case 1
    '                    retVal = False
    '                Case 2
    '                    retVal = False
    '                Case 3
    '                    retVal = False
    '                Case 5
    '                    retVal = True
    '                Case 6
    '                    retVal = True
    '                Case 7
    '                    retVal = True
    '                Case 8
    '                    retVal = True
    '                Case 9
    '                    retVal = True
    '                Case 11
    '                    retVal = False
    '                Case 12
    '                    retVal = False
    '            End Select
    '        Else
    '            'If the month is April, let's check to see if the date is before or after
    '            '2 AM on the first Sunday of the month
    '            If Month(dtDateTime) = 4 Then
    '                If Day(dtDateTime) < 8 Then
    '                    For x = 1 To Day(dtDateTime)
    '                        sTempDate = CStr(Month(dtDateTime)) & "/" & x & "/" & CStr(Year(dtDateTime))
    '                        If Weekday(sTempDate) = 1 Then
    '                            If Day(sTempDate) < Day(dtDateTime) Then
    '                                'First sunday in April has already passed, so we are now in DST
    '                                retVal = True
    '                                Exit For
    '                            Else
    '                                'It's the first Sunday in April!
    '                                'Let's see if it's past 2 AM. If there is no time part in dtDateTime (time part = "00:00:00"), 
    '                                'we are going to assume it's past 2 AM
    '                                If (Hour(dtDateTime) >= 2) Or (Hour(dtDateTime) = 0 And Minute(dtDateTime) = 0 And Second(dtDateTime) = 0) Then
    '                                    retVal = True
    '                                    Exit For
    '                                Else
    '                                    retVal = False
    '                                End If
    '                            End If
    '                        Else
    '                            retVal = False
    '                        End If
    '                    Next
    '                Else
    '                    'we know what to do if the day is equal to or greater than the 8th
    '                    retVal = True
    '                End If
    '                'If the month is October, let's check to see if date is before or after
    '                '2 AM on the last Sunday of the month
    '            ElseIf Month(dtDateTime) = 10 Then
    '                'We know what to do if the day is less than then 25th
    '                If Day(dtDateTime) < 25 Then
    '                    retVal = True
    '                Else
    '                    For x = 25 To Day(dtDateTime)
    '                        sTempDate = CStr(Month(dtDateTime)) & "/" & x & "/" & CStr(Year(dtDateTime))
    '                        If Weekday(sTempDate) = 1 Then
    '                            If Day(sTempDate) < Day(dtDateTime) Then
    '                                'last sunday in oct has already passed, so we aren't in DST anymore
    '                                retVal = False
    '                                Exit For
    '                            Else
    '                                'It's the last Sunday in October!
    '                                'Let's see if it's past 2 AM. If there is no time part in dtDateTime (time part = "00:00:00"), 
    '                                'we are going to assume it's past 2 AM
    '                                If (Hour(dtDateTime) >= 2) Or (Hour(dtDateTime) = 0 And Minute(dtDateTime) = 0 And Second(dtDateTime) = 0) Then
    '                                    retVal = False
    '                                    Exit For
    '                                Else
    '                                    retVal = True
    '                                End If
    '                            End If
    '                        Else
    '                            retVal = True
    '                        End If
    '                    Next
    '                End If
    '            End If
    '        End If
    '    Else
    '        'if the string passed to the function is not a valid date, let's return false.
    '        retVal = False
    '    End If
    '    CheckDayLightSavings = retVal
    'End Function
    Public Function CheckDayLightSavings(ByVal dtDateTime)
        Dim retVal As Boolean

        Dim varTempdate As Date
        If IsDate(dtDateTime) Then
            varTempdate = dtDateTime

            If varTempdate.IsDaylightSavingTime = True Then
                retVal = True
            ElseIf varTempdate.IsDaylightSavingTime = False Then
                retVal = False
            End If
        End If
        CheckDayLightSavings = retVal
    End Function
    Public Sub New()
        varStrAdminCC = "smurkute@edictate.com"
        varStrBcc = "apagare@edictate.com"
        varAttendanceReport = "C:\AttendanceReport\"
        varStrHBAFromMail = "mtsupport@edictate.com"
        varStrHBAToMail = "mtsupport@edictate.com"
        ConString = System.Configuration.ConfigurationManager.AppSettings("ETSCon")
        oConn.ConnectionString = ConString
    End Sub
    Public Function CheckforCompOff(ByVal Str As String)
        If UCase(Trim(Str)) = UCase(Trim("Karnataka")) Then
            CheckforCompOff = 11
        ElseIf UCase(Trim(Str)) = UCase(Trim("Maharashtra")) Then
            CheckforCompOff = 5
        End If
    End Function
    Public Function GetSuperID(ByVal UserID As String) As String
        Dim varStrPri As String
        Dim varStrSec As String
        Dim varStrThi As String
        Dim varIntLevel As Integer
        Dim varDtStartDate As Date
        Dim varDtEndDate As Date
        Dim varDateTodayDate As Date
        Dim varSuperID
        Dim varBolWhileFlag As Boolean
        Dim objConn As New Data.SqlClient.SqlConnection
        objConn = OpenConnection(objConn)

        Try
            varIntLevel = 0
            'calculate the todays date 
            If CheckDayLightSavings(Now()) Then
                varDateTodayDate = DateAdd(DateInterval.Hour, 9, Now())
                varDateTodayDate = DateAdd(DateInterval.Minute, 30, varDateTodayDate)
            Else
                varDateTodayDate = DateAdd(DateInterval.Hour, 10, Now())
                varDateTodayDate = DateAdd(DateInterval.Minute, 30, varDateTodayDate)
            End If
            'end calculation of current date
            'Check User level for sending leave application to valid supervisor
            Dim oCommandSuperLevel As New Data.SqlClient.SqlCommand("SELECT LevelNo FROM dbo.tblDeptSuperVisorAssign S INNER JOIN dbo.tblDepartments D ON  S.DepartmentID=D.DepartmentID INNER JOIN dbo.tblUsers U ON U.DepartmentID=D.DepartmentID WHERE S.SuperVisorID='" & UserID & "' AND U.UserID='" & UserID & "' AND (U.IsDeleted IS NULL OR U.IsDeleted =0) ", objConn)
            Dim oRecSuperLevel As Data.SqlClient.SqlDataReader = oCommandSuperLevel.ExecuteReader()
            If oRecSuperLevel.HasRows Then
                While oRecSuperLevel.Read
                    varIntLevel = oRecSuperLevel.GetDecimal(oRecSuperLevel.GetOrdinal("LevelNo"))
                End While
            End If
            oRecSuperLevel.Close()
            oRecSuperLevel = Nothing
            oCommandSuperLevel = Nothing
            'end check user level

            Dim oCommandSV As New Data.SqlClient.SqlCommand("SELECT LevelNo,SuperVisorID FROM dbo.tblDeptSuperVisorAssign S INNER JOIN dbo.tblDepartments D ON  S.DepartmentID=D.DepartmentID INNER JOIN dbo.tblUsers U ON U.DepartmentID=D.DepartmentID WHERE U.UserID='" & UserID & "' AND LevelNo > " & varIntLevel & " AND (U.IsDeleted IS NULL OR U.IsDeleted =0) ", objConn)
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
            'Check primary supervisor is on leave or not 
            If varStrPri <> "" And varSuperID = "" Then
                Dim oCommandPri As New Data.SqlClient.SqlCommand("SELECT U.UserID,U.OfficialMailID,L.StartDate,L.EndDate FROM dbo.tblUsers U INNER JOIN dbo.tblLeave L  ON U.UserID = L.UserID WHERE U.UserID='" & varStrPri & "' AND (L.IsDeleted IS NULL OR L.IsDeleted =0) AND (U.IsDeleted IS NULL OR U.IsDeleted =0) ", objConn)
                Dim oRecPri As Data.SqlClient.SqlDataReader = oCommandPri.ExecuteReader()

                If oRecPri.HasRows Then
                    While oRecPri.Read
                        If varBolWhileFlag = False Then
                            varDtStartDate = oRecPri.GetDateTime(oRecPri.GetOrdinal("StartDate"))
                            varDtEndDate = oRecPri.GetDateTime(oRecPri.GetOrdinal("EndDate"))
                            While varDtEndDate >= varDtStartDate
                                If DateDiff(DateInterval.Day, DateSerial(Year(varDtStartDate), Month(varDtStartDate), Day(varDtStartDate)), DateSerial(Year(varDateTodayDate), Month(varDateTodayDate), Day(varDateTodayDate))) = 0 Then
                                    varSuperID = ""
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
                    Else
                        varBolWhileFlag = False
                    End If
                Else
                    varSuperID = varStrPri
                End If

                oRecPri.Close()
                oRecPri = Nothing
                oCommandPri = Nothing
            End If
            'Check seconday supervisor is on leave or not 
            If varStrSec <> "" And varSuperID = "" Then
                Dim oCommandSec As New Data.SqlClient.SqlCommand("SELECT U.UserID,U.OfficialMailID,L.StartDate,L.EndDate FROM dbo.tblUsers U INNER JOIN dbo.tblLeave L  ON U.UserID = L.UserID WHERE U.UserID='" & varStrSec & "' AND (L.IsDeleted IS NULL OR L.IsDeleted =0) AND (U.IsDeleted IS NULL OR U.IsDeleted =0) ", objConn)
                Dim oRecSec As Data.SqlClient.SqlDataReader = oCommandSec.ExecuteReader()

                If oRecSec.HasRows Then
                    While oRecSec.Read
                        If varBolWhileFlag = False Then
                            varDtStartDate = oRecSec.GetDateTime(oRecSec.GetOrdinal("StartDate"))
                            varDtEndDate = oRecSec.GetDateTime(oRecSec.GetOrdinal("EndDate"))
                            While varDtEndDate >= varDtStartDate
                                If DateDiff(DateInterval.Day, DateSerial(Year(varDtStartDate), Month(varDtStartDate), Day(varDtStartDate)), DateSerial(Year(varDateTodayDate), Month(varDateTodayDate), Day(varDateTodayDate))) = 0 Then
                                    varSuperID = ""
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
                    Else
                        varBolWhileFlag = False
                    End If
                Else
                    varSuperID = varStrSec
                End If
                oRecSec.Close()
                oRecSec = Nothing
                oCommandSec = Nothing
            End If
            'Check third supervisor is on leave or not 
            If varStrThi <> "" And varSuperID = "" Then
                Dim oCommandThi As New Data.SqlClient.SqlCommand("SELECT U.UserID,U.OfficialMailID,L.StartDate,L.EndDate FROM dbo.tblUsers U INNER JOIN dbo.tblLeave L  ON U.UserID = L.UserID WHERE U.UserID='" & varStrThi & "' AND (L.IsDeleted IS NULL OR L.IsDeleted =0) AND (U.IsDeleted IS NULL OR U.IsDeleted =0) ", objConn)
                Dim oRecThi As Data.SqlClient.SqlDataReader = oCommandThi.ExecuteReader()

                If oRecThi.HasRows Then
                    While oRecThi.Read
                        If varBolWhileFlag = False Then
                            varDtStartDate = oRecThi.GetDateTime(oRecThi.GetOrdinal("StartDate"))
                            varDtEndDate = oRecThi.GetDateTime(oRecThi.GetOrdinal("EndDate"))
                            While varDtEndDate >= varDtStartDate
                                If DateDiff(DateInterval.Day, DateSerial(Year(varDtStartDate), Month(varDtStartDate), Day(varDtStartDate)), DateSerial(Year(varDateTodayDate), Month(varDateTodayDate), Day(varDateTodayDate))) = 0 Then
                                    varSuperID = ""
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
                    Else
                        varBolWhileFlag = False
                    End If
                Else
                    varSuperID = varStrThi
                End If

                oRecThi.Close()
                oRecThi = Nothing
                oCommandThi = Nothing
            End If
            If varSuperID = "" Then
                varSuperID = varStrThi
            End If
            GetSuperID = varSuperID
        Catch ex As System.Exception
        Finally
            If objConn.State <> Data.ConnectionState.Closed Then
                objConn.Close()
                objConn = Nothing
            End If
        End Try
    End Function
    Public Function GetOfficialMailID(ByVal Str As String) As String
        Dim varStrMailID As String
        Dim objConn As New Data.SqlClient.SqlConnection
        objConn = OpenConnection(objConn)
        Try
            Dim oCommand As New Data.SqlClient.SqlCommand("SELECT OfficialMailID FROM dbo.tblUsers WHERE UserID='" & Str & "' AND (IsDeleted IS NULL OR IsDeleted =0) ", objConn)
            Dim oRec As Data.SqlClient.SqlDataReader = oCommand.ExecuteReader()
            If oRec.HasRows Then
                While oRec.Read
                    varStrMailID = oRec.GetString(oRec.GetOrdinal("OfficialMailID")).ToString
                End While
            End If
            oRec.Close()
            oRec = Nothing
            oCommand = Nothing
            GetOfficialMailID = varStrMailID
        Catch ex As System.Exception
        Finally
            If objConn.State <> Data.ConnectionState.Closed Then
                objConn.Close()
                objConn = Nothing
            End If
        End Try
    End Function
    Public Function CheckOffDayAttendance(ByVal CheckDate As Date, ByVal DepartmentID As String, ByVal State As String) As Boolean
        Dim varStrOffDate As New ArrayList
        Dim i As Integer
        Dim varTempDate As Date

        Dim objConn As New Data.SqlClient.SqlConnection
        objConn = OpenConnection(objConn)

        Try
            Dim oCommand As New Data.SqlClient.SqlCommand("SELECT OffDate,Description FROM dbo.tblOffDays WHERE State='" & State & "' AND Department='" & DepartmentID & "'", objConn)
            Dim oRec As Data.SqlClient.SqlDataReader = oCommand.ExecuteReader()
            If oRec.HasRows Then
                While oRec.Read
                    varStrOffDate.Add(oRec.GetDateTime(oRec.GetOrdinal("OffDate")).ToShortDateString)
                End While
            End If
            oRec.Close()
            oRec = Nothing
            oCommand = Nothing
            For i = 0 To varStrOffDate.Count - 1
                varTempDate = varStrOffDate(i)
                If (Day(CheckDate) = Day(varTempDate) And Month(CheckDate) = Month(varTempDate)) Then
                    Return True
                End If
            Next
            Return False
        Catch ex As System.Exception
        Finally
            If objConn.State <> Data.ConnectionState.Closed Then
                objConn.Close()
                objConn = Nothing
            End If
        End Try
    End Function
    Public Function CheckOffDateCount(ByVal StartDate As Date, ByVal EndDate As Date, ByVal DepartmentID As String, ByVal State As String) As Double
        Dim varStrOffDate As New ArrayList
        Dim i As Integer
        Dim varTempDate As Date
        Dim varOffDateCount As Double

        Dim objConn As New Data.SqlClient.SqlConnection
        objConn = OpenConnection(objConn)

        Try
            Dim oCommand As New Data.SqlClient.SqlCommand("SELECT OffDate,Description FROM dbo.tblOffDays WHERE State='" & State & "' AND Department='" & DepartmentID & "'", objConn)
            Dim oRec As Data.SqlClient.SqlDataReader = oCommand.ExecuteReader()
            If oRec.HasRows Then
                While oRec.Read
                    varStrOffDate.Add(oRec.GetDateTime(oRec.GetOrdinal("OffDate")).ToShortDateString)
                End While
            End If
            oRec.Close()
            oRec = Nothing
            oCommand = Nothing
            varOffDateCount = 0
            While EndDate >= StartDate
                For i = 0 To varStrOffDate.Count - 1
                    varTempDate = varStrOffDate(i)
                    If (Day(StartDate) = Day(varTempDate) And Month(StartDate) = Month(varTempDate)) Then
                        varOffDateCount = varOffDateCount + 1
                    End If
                Next
                StartDate = DateAdd(DateInterval.Day, 1, StartDate)
            End While
            CheckOffDateCount = varOffDateCount
        Catch ex As System.Exception

        Finally
            If objConn.State <> Data.ConnectionState.Closed Then
                objConn.Close()
                objConn = Nothing
            End If
        End Try
    End Function
    Public Function WeekDayFromWeekDayName(ByVal Str As String) As Integer
        Select Case Str
            Case "Sunday"
                Return 1
            Case "Monday"
                Return 2
            Case "Tuesday"
                Return 3
            Case "Wednesday"
                Return 4
            Case "Thursday"
                Return 5
            Case "Friday"
                Return 6
            Case "Saturday"
                Return 7
        End Select
    End Function
    Public Function GetMonthFromMonthName(ByVal Str As String) As Integer
        Select Case Str
            Case "January"
                Return 1
            Case "February"
                Return 2
            Case "March"
                Return 3
            Case "April"
                Return 4
            Case "May"
                Return 5
            Case "June"
                Return 6
            Case "July"
                Return 7
            Case "August"
                Return 8
            Case "September"
                Return 9
            Case "October"
                Return 10
            Case "November"
                Return 11
            Case "December"
                Return 12
        End Select
    End Function
    Public Function WeekDayNameFromWeekDay(ByVal Str As Integer) As String
        Select Case Str
            Case 1
                Return "Sunday"
            Case 2
                Return "Monday"
            Case 3
                Return "Tuesday"
            Case 4
                Return "Wednesday"
            Case 5
                Return "Thursday"
            Case 6
                Return "Friday"
            Case 7, 0
                Return "Saturday"
        End Select
    End Function
    Public Function SendMail(ByVal From As String, ByVal ToMail As String, ByVal CC As String, ByVal Subject As String, ByVal MailMatter As String) As Boolean
        Try
            'Dim varToMail(10) As String
            'Dim varCCMail(10) As String
            'Dim i As Integer
            'Dim MailMsg As New System.Net.Mail.MailMessage

            'Dim objsmtp As New SmtpClient("secure.emailsrvr.com")
            'objsmtp.Credentials = New System.Net.NetworkCredential("admin@edictate.com", "welc0me")

            'MailMsg.From = New MailAddress(From)

            'If ToMail <> "" Then
            '    If ToMail.IndexOf(",") > 0 Then
            '        varToMail = ToMail.Split(",")

            '        For i = 0 To UBound(varToMail)
            '            If i = 0 Then
            '                MailMsg.To.Add(Trim(varToMail(i)))
            '            Else
            '                MailMsg.CC.Add(Trim(varToMail(i)))
            '            End If
            '        Next
            '    Else
            '        MailMsg.To.Add(ToMail)
            '    End If
            'End If

            'If CC <> "" Then
            '    If CC.IndexOf(",") > 0 Then
            '        varCCMail = CC.Split(",")

            '        For i = 0 To UBound(varCCMail)
            '            MailMsg.CC.Add(Trim(varCCMail(i)))
            '        Next
            '    Else
            '        MailMsg.CC.Add(Trim(CC))
            '    End If
            'End If

            'MailMsg.Bcc.Add(varStrBcc)
            'MailMsg.Subject = Subject
            'MailMsg.Body = MailMatter
            'MailMsg.IsBodyHtml = True

            'objsmtp.DeliveryMethod = SmtpDeliveryMethod.PickupDirectoryFromIis


            'objsmtp.Send(MailMsg)


            Dim iMail As New SASMTPLib.CoSMTPMail()
            'iMail.RemoteHost = "smtp.edictateindia.com"
            'iMail.UserName = "edictatemail"
            'iMail.Password = "ed1ctatema!l"

            iMail.RemoteHost = "secure.emailsrvr.com"
            iMail.UserName = "alert@edictate.com"
            iMail.Password = "Welcome@medofficepro2011"

            'iMail.Port = 25
            iMail.FromAddress = From

            Dim varToMail() As String
            If ToMail <> "" Then
                If ToMail.IndexOf(",") > 0 Then
                    varToMail = ToMail.Split(",")

                    For i As Integer = 0 To UBound(varToMail)
                        If i = 0 Then
                            iMail.AddRecipient(Trim(varToMail(i)), Trim(varToMail(i)))
                        Else
                            iMail.AddCC(Trim(varToMail(i)), Trim(varToMail(i)))
                        End If
                    Next
                Else
                    iMail.AddRecipient(Trim(ToMail), Trim(ToMail))
                End If
            End If

            'iMail.AddRecipient(ToMail, ToMail)
            Dim varCCMail() As String

            If CC <> "" Then
                If CC.IndexOf(",") > 0 Then
                    varCCMail = CC.Split(",")
                    For j As Integer = 0 To UBound(varCCMail)
                        iMail.AddCC(Trim(varCCMail(j)), Trim(varCCMail(j)))
                    Next
                Else
                    iMail.AddCC(Trim(CC), Trim(CC))
                End If
            End If

            iMail.AddCC(varStrAdminCC, varStrAdminCC)
            'iMail.AddCC(CC, CC)
            iMail.ReturnReceipt = False
            iMail.Subject = Subject
            iMail.HtmlText = MailMatter
            iMail.SendMail()

            Return True

        Catch ex As System.Exception
            Return False
        End Try
    End Function

    Public Function ERSSSendMail(ByVal From As String, ByVal ToMail As String, ByVal CC As String, ByVal Subject As String, ByVal MailMatter As String) As Boolean
        Try

            'Dim varToMail() As String
            'Dim varCCMail() As String
            'Dim i As Integer
            'Dim MailMsg As New System.Net.Mail.MailMessage

            'Dim objsmtp As New SmtpClient("secure.emailsrvr.com")
            'objsmtp.Credentials = New System.Net.NetworkCredential("admin@edictate.com", "welc0me")

            'MailMsg.From = New MailAddress(From)
            'varToMail = ToMail.Split(",")

            'For i = 0 To UBound(varToMail)
            '    'If i = 0 Then
            '    '    MailMsg.To.Add(varToMail(i))
            '    'Else
            '    '    MailMsg.CC.Add(varToMail(i))
            '    'End If
            '    If varToMail(i) <> "" Then
            '        MailMsg.To.Add(varToMail(i))
            '    End If
            'Next

            'If CC <> "" Then
            '    varCCMail = CC.Split(",")
            '    For i = 0 To UBound(varCCMail)
            '        If varCCMail(i) <> "" Then
            '            MailMsg.CC.Add(varCCMail(i))
            '        End If
            '    Next
            'End If

            'MailMsg.Subject = Subject
            'MailMsg.Body = MailMatter
            'MailMsg.IsBodyHtml = True

            'objsmtp.DeliveryMethod = SmtpDeliveryMethod.PickupDirectoryFromIis


            'objsmtp.Send(MailMsg)

            Dim iMail As New SASMTPLib.CoSMTPMail()

            'iMail.RemoteHost = "smtp.edictateindia.com"
            'iMail.UserName = "edictatemail"
            'iMail.Password = "ed1ctatema!l"

            iMail.RemoteHost = "secure.emailsrvr.com"
            iMail.UserName = "alert@edictate.com"
            iMail.Password = "Welcome@medofficepro2011"

            'iMail.Port = 25
            iMail.FromAddress = From

            Dim varToMail() As String

            If ToMail <> "" Then
                If ToMail.IndexOf(",") > 0 Then
                    varToMail = ToMail.Split(",")

                    For i As Integer = 0 To UBound(varToMail)
                        iMail.AddRecipient(Trim(varToMail(i)), Trim(varToMail(i)))
                    Next
                Else
                    iMail.AddRecipient(Trim(ToMail), Trim(ToMail))
                End If
            End If

            'iMail.AddRecipient(ToMail, ToMail)
            Dim varCCMail() As String

            If CC <> "" Then
                If CC.IndexOf(",") > 0 Then
                    varCCMail = CC.Split(",")
                    For j As Integer = 0 To UBound(varCCMail)
                        iMail.AddCC(Trim(varCCMail(j)), Trim(varCCMail(j)))
                    Next
                Else
                    iMail.AddCC(Trim(CC), Trim(CC))
                End If
            End If

            'iMail.AddCC(CC, CC)
            iMail.ReturnReceipt = False
            iMail.Subject = Subject
            iMail.HtmlText = MailMatter
            'iMail.SendMail()

            Return True

        Catch ex As System.Exception
            'Response.Write(ex.Message)
            'MsgBox(ex.Message)
        End Try
    End Function
    Public Function NewAccPhyCreationMail(ByVal From As String, ByVal ToMail As String, ByVal CC As String, ByVal Subject As String, ByVal MailMatter As String) As Boolean
        Try
            Dim iMail As New SASMTPLib.CoSMTPMail()

            iMail.RemoteHost = "secure.emailsrvr.com"
            iMail.UserName = "alert@edictate.com"
            iMail.Password = "Welcome@medofficepro2011"

            'iMail.Port = 25
            iMail.FromAddress = From

            Dim varToMail() As String

            If ToMail <> "" Then
                If ToMail.IndexOf(",") > 0 Then
                    varToMail = ToMail.Split(",")

                    For i As Integer = 0 To UBound(varToMail)
                        iMail.AddRecipient(Trim(varToMail(i)), Trim(varToMail(i)))
                    Next
                Else
                    iMail.AddRecipient(Trim(ToMail), Trim(ToMail))
                End If
            End If

            'iMail.AddRecipient(ToMail, ToMail)
            Dim varCCMail() As String

            If CC <> "" Then
                If CC.IndexOf(",") > 0 Then
                    varCCMail = CC.Split(",")
                    For j As Integer = 0 To UBound(varCCMail)
                        iMail.AddCC(Trim(varCCMail(j)), Trim(varCCMail(j)))
                    Next
                Else
                    iMail.AddCC(Trim(CC), Trim(CC))
                End If
            End If

            iMail.ReplyTo = From
            iMail.Priority = SASMTPLib.MAILPRIORITY.HIGH_P
            iMail.ReturnReceipt = True
            iMail.Urgent = True
            iMail.Subject = Subject
            iMail.HtmlText = MailMatter
            iMail.SendMail()

            Return True

        Catch ex As System.Exception
            'Response.Write(ex.Message)
            'MsgBox(ex.Message)
        End Try
    End Function
    Public Function OpenConnection(ByRef Conn As Data.SqlClient.SqlConnection) As Data.SqlClient.SqlConnection
        Try
            Conn.ConnectionString = System.Configuration.ConfigurationManager.AppSettings("ETSCon")
            Conn.Open()
            Return Conn
        Catch ex As System.Exception
        End Try
    End Function
End Class
