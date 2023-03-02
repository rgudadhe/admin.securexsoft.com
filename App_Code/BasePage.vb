
Imports Microsoft.VisualBasic
Imports System.Data.SqlClient
Imports System.Web.UI.WebControls
Imports System.Web.UI.WebControls.WebParts
Imports System.Web.UI.HtmlControls
Imports System
Imports System.Configuration
Imports System.IO
Imports System.Web
Imports System.Web.Security
Imports System.Web.UI

Imports System.Web.Util

Public Class BasePage
    Inherits System.Web.UI.Page
    Public WebAddress As String = System.Configuration.ConfigurationManager.AppSettings("URL")
    Public ChkDLSav As Boolean
    Public ServTime As DateTime
    Public ProcTime As Date
    Public SysTime As Date
    Public ProcStartDate As Date
    Public ProcEndDate As Date
    Public ServStartDate As Date
    Public ServEndDate As Date


    Protected Overloads Overrides Sub OnInit(ByVal e As EventArgs)
        'Session.Abandon()
        'Session.Abandon()
        MyBase.OnInit(e)
        If Session("UserID") = "" Then
            Response.Redirect(WebAddress & "\PopupLogin.aspx")
        End If
        If Context.Session IsNot Nothing Then
            'Tested and the IsNewSession is more advanced then simply checking if 
            ' a cookie is present, it does take into account a session timeout, because 
            ' I tested a timeout and it did show as a new session 
            If Session.IsNewSession Then
                ' If it says it is a new session, but an existing cookie exists, then it must 
                ' have timed out (can't use the cookie collection because even on first 
                ' request it already contains the cookie (request and response 
                ' seem to share the collection) 
                Dim szCookieHeader As String = Request.Headers("Cookie")
                If (szCookieHeader IsNot Nothing) AndAlso (szCookieHeader.IndexOf("ASP.NET_SessionId") >= 0) Then
                    Response.Redirect(WebAddress & "\PopupLogin.aspx")
                    
                End If
            End If
            'Response.AppendHeader("Refresh", Convert.ToString((Session.Timeout * 3600)) & "; url=" & "../PopupLogin.aspx")
            'InjectSessionExpireScript()
        End If
        ServTime = Now()
        If CheckDayLightSavings(ServTime.ToShortDateString) Then
            'SysTime = ServTime.AddHours(10)
            'SysTime = SysTime.AddMinutes(30)
            'ProcTime = SysTime
            'ProcStartDate = ProcTime.ToShortDateString
            'ProcEndDate = ProcStartDate.AddDays(1)
            'ServStartDate = ProcTime.AddHours(-9)
            'ServStartDate = ServStartDate.AddMinutes(-30)
            'ServEndDate = ServStartDate.AddDays(1)
            SysTime = ServTime.AddHours(9)
            SysTime = SysTime.AddMinutes(30)
            'ProcTime = SysTime.AddHours(-3)
            ProcTime = SysTime
            ProcStartDate = ProcTime.ToShortDateString
            ProcEndDate = ProcStartDate.AddDays(1)
            ServStartDate = ProcStartDate.AddHours(-9)
            ServStartDate = ServStartDate.AddMinutes(-30)
            ServEndDate = ServStartDate.AddDays(1)

        Else
            SysTime = ServTime.AddHours(10)
            SysTime = SysTime.AddMinutes(30)
            'ProcTime = SysTime.AddHours(-3)
            ProcTime = SysTime
            ProcStartDate = ProcTime.ToShortDateString
            ProcEndDate = ProcStartDate.AddDays(1)
            ServStartDate = ProcStartDate.AddHours(-10)
            ServStartDate = ServStartDate.AddMinutes(-30)
            ServEndDate = ServStartDate.AddDays(1)
        End If

    
      

    End Sub
    Public Sub GetMyMonthList(ByVal MyddlMonthList As DropDownList, ByVal SetCurruntMonth As Boolean)
        Dim month As DateTime = Convert.ToDateTime("1/1/2000")
        For i As Integer = 0 To 11
            Dim NextMont As DateTime = month.AddMonths(i)
            Dim list As New ListItem()
            list.Text = NextMont.ToString("MMMM")
            list.Value = NextMont.Month.ToString()
            MyddlMonthList.Items.Add(list)
        Next
        If SetCurruntMonth = True Then
            MyddlMonthList.Items.FindByValue(DateTime.Now.Month.ToString()).Selected = True
        End If
    End Sub

    Public Sub GetMyYearList(ByVal MyddlYearList As DropDownList, ByVal SetCurruntYear As Boolean)
        Dim Year As DateTime = Now.AddYears(-4)
        For i As Integer = 0 To 4
            Dim NextMont As DateTime = Year.AddYears(i)
            Dim list As New ListItem()
            list.Text = NextMont.Year.ToString()
            list.Value = NextMont.Year.ToString()
            MyddlYearList.Items.Add(list)
        Next
        If SetCurruntYear = True Then
            MyddlYearList.Items.FindByValue(DateTime.Now.Year.ToString()).Selected = True
        End If
    End Sub

    Private Function CheckDayLightSavings(ByVal dtDateTime)
        'Author: Olin Hamilton
        '
        'Email:  olin@oct.net
        '
        'Pupose: This function returns true if a passed datetime value is in Daylight Savings time,
        '		 Returns false if the datetime value is in Standard Time
        '
        'Usage:  Use a date, or datetime formatted string as the parameter
        '		 If the parameter is not a valid date, the function will return False
        '		 If no Time part is passed with the string, it will assume the time is after
        '		 2 AM, so would return true on the first sunday of April, and False on the last Sunday in October.
        '		 Since a null time part for a datetime value defaults to exactly "00:00:00", if a date is passed with 
        '		 a time value equal to "00:00:00" the function will assume that the time value was not passed
        '		 and that the time is after 2 Am.  This may cause unexpected results if you are passing a valid datetime
        '		 value with a time value of exactly midnight.  Unfortunately, I could not think of any other way to handle
        '		 no time values, and figured it was best to consider it past 2AM.  
        '		 However, you can control this limitation by checking the time value for the date before passing it
        '		 to this function, and if it has a time value of "00:00:00", increasing it by one second will cause
        '		 the function to accurately return false if the date is the start day for DST,
        '		 or return True if it's the end day for DST.
        '		 If there are any questions, suggestions, comments, or modifications made, 
        '        please send me an email letting me know.
        Dim retVal As Boolean = False

        ''if the date time has the milliseconds, clean them off
        'If InStr(1, CStr(dtDateTime), ".") <> 0 Then
        '    dtDateTime = Left(dtDateTime, Len(dtDateTime) - 4)
        'End If
        ''If the passed string is a valid date, let's begin checking, otherwise
        ''just return False.
        'If IsDate(dtDateTime) Then
        '    'We know what to do with any dates within these months
        '    If Month(dtDateTime) <> 10 And Month(dtDateTime) <> 4 Then
        '        Select Case Month(dtDateTime)
        '            'Jan
        '            Case 1
        '                retVal = False
        '                'Feb
        '            Case 2
        '                retVal = False
        '                'Mar
        '            Case 3
        '                RetVal = False
        '                'May
        '            Case 5
        '                retVal = True
        '                'Jun
        '            Case 6
        '                retVal = True
        '                'Jul
        '            Case 7
        '                retVal = True
        '                'Aug
        '            Case 8
        '                retVal = True
        '                'Sep
        '            Case 9
        '                retVal = True
        '                'Nov
        '            Case 11
        '                retVal = False
        '                'Dec
        '            Case 12
        '                retVal = False
        '        End Select
        '    Else
        '        'If the month is April, let's check to see if the date is before or after
        '        '2 AM on the first Sunday of the month
        '        If Month(dtDateTime) = 4 Then
        '            If Day(dtDateTime) < 8 Then
        '                For x = 1 To Day(dtDateTime)
        '                    sTempDate = CStr(Month(dtDateTime)) & "/" & x & "/" & CStr(Year(dtDateTime))
        '                    If Weekday(sTempDate) = 1 Then
        '                        If Day(sTempDate) < Day(dtDateTime) Then
        '                            'First sunday in April has already passed, so we are now in DST
        '                            retVal = True
        '                            Exit For
        '                        Else
        '                            'It's the first Sunday in April!
        '                            'Let's see if it's past 2 AM. If there is no time part in dtDateTime (time part = "00:00:00"), 
        '                            'we are going to assume it's past 2 AM
        '                            If (Hour(dtDateTime) >= 2) Or (Hour(dtDateTime) = 0 And Minute(dtDateTime) = 0 And Second(dtDateTime) = 0) Then
        '                                retVal = True
        '                                Exit For
        '                            Else
        '                                retVal = False
        '                            End If
        '                        End If
        '                    Else
        '                        retVal = False
        '                    End If
        '                Next
        '            Else
        '                'we know what to do if the day is equal to or greater than the 8th
        '                retval = True
        '            End If
        '            'If the month is October, let's check to see if date is before or after
        '            '2 AM on the last Sunday of the month
        '        ElseIf Month(dtDateTime) = 10 Then
        '            'We know what to do if the day is less than then 25th
        '            If Day(dtDateTime) < 25 Then
        '                retval = True
        '            Else
        '                For x = 25 To Day(dtDateTime)
        '                    sTempDate = CStr(Month(dtDateTime)) & "/" & x & "/" & CStr(Year(dtDateTime))
        '                    If Weekday(sTempDate) = 1 Then
        '                        If Day(sTempDate) < Day(dtDateTime) Then
        '                            'last sunday in oct has already passed, so we aren't in DST anymore
        '                            retVal = False
        '                            Exit For
        '                        Else
        '                            'It's the last Sunday in October!
        '                            'Let's see if it's past 2 AM. If there is no time part in dtDateTime (time part = "00:00:00"), 
        '                            'we are going to assume it's past 2 AM
        '                            If (Hour(dtDateTime) >= 2) Or (Hour(dtDateTime) = 0 And Minute(dtDateTime) = 0 And Second(dtDateTime) = 0) Then
        '                                retVal = False
        '                                Exit For
        '                            Else
        '                                retVal = True
        '                            End If
        '                        End If
        '                    Else
        '                        retVal = True
        '                    End If
        '                Next
        '            End If
        '        End If
        '    End If
        'Else
        '    'if the string passed to the function is not a valid date, let's return false.
        '    retVal = False
        'End If
        'CheckDayLightSavings = retVal
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
    Protected Function chkLevel(ByVal AdminLevel As Long, ByVal Level As Long) As Boolean
        If (AdminLevel And Level) = Level Then
            chkLevel = True
        Else
            chkLevel = False
        End If
    End Function

End Class

