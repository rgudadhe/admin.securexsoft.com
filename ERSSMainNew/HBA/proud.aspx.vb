Imports MainModule
Partial Class proud
    Inherits BasePage
    Dim objMainModule As New MainModule
    Public varStrDate(6), varStrMins(6)
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            Dim objConn As New Data.SqlClient.SqlConnection
            objConn = objMainModule.OpenConnection(objConn)
            Try
                Dim varLstItemCurrentWeek As New ListItem
                Dim varLstItemNextWeek As New ListItem
                Dim varIntI As Integer

                Dim cmdUserID As New Data.SqlClient.SqlCommand("SELECT UserName,FirstName,LastName FROM dbo.tblUsers WHERE UserID='" & Session("UserID") & "' AND ContractorID='" & Session("ContractorID").ToString & "' AND (IsDeleted IS NULL OR IsDeleted = 0 )", objConn)
                Dim readerUserID As Data.SqlClient.SqlDataReader = cmdUserID.ExecuteReader()

                If readerUserID.HasRows Then
                    While readerUserID.Read
                        lblUserID.Text = readerUserID.GetString(readerUserID.GetOrdinal("UserName")).ToString
                    End While
                End If

                readerUserID.Close()
                readerUserID = Nothing
                cmdUserID = Nothing
                Dim varStrCurWeek As String = String.Empty
                If CurrentWeek() <> "" Then
                    Dim varStrSplit() As String
                    varStrSplit = CurrentWeek().ToString.Split("/")
                    Dim varStrSplitT() As String
                    varStrSplitT = varStrSplit(2).ToString.Split("-")
                    varStrCurWeek = varStrSplit(0) & "/" & varStrSplit(1) & " to " & varStrSplitT(1) & "/" & varStrSplit(3)
                End If

                Dim varStrNexWeek As String = String.Empty
                If NextWeek() <> "" Then
                    Dim varStrSplit1() As String
                    varStrSplit1 = NextWeek().ToString.Split("/")
                    Dim varStrSplitT1() As String
                    varStrSplitT1 = varStrSplit1(2).ToString.Split("-")
                    varStrNexWeek = varStrSplit1(0) & "/" & varStrSplit1(1) & " to " & varStrSplitT1(1) & "/" & varStrSplit1(3)
                End If
                varLstItemCurrentWeek.Text = "Current Week " & varStrCurWeek
                'varLstItemCurrentWeek.Text = "Current Week " & CurrentWeek()
                varLstItemCurrentWeek.Value = CurrentWeek()
                varLstItemNextWeek.Text = "Next Week " & varStrNexWeek
                'varLstItemNextWeek.Text = "Next Week " & NextWeek()
                varLstItemNextWeek.Value = NextWeek()

                DropDownWeek.Items.Add(varLstItemCurrentWeek)
                DropDownWeek.Items.Add(varLstItemNextWeek)
                For varIntI = 1 To 12
                    Dim varTempLstItem As New ListItem
                    varTempLstItem.Text = varIntI
                    varTempLstItem.Value = varIntI
                    DropDownStartTime.Items.Add(varTempLstItem)
                    DropDownEndTime.Items.Add(varTempLstItem)
                    varTempLstItem = Nothing
                Next
            Catch ex As Exception
                If objConn.State <> Data.ConnectionState.Closed Then
                    objConn.Close()
                    objConn = Nothing
                End If
            End Try
        End If
    End Sub
    Public Function CurrentWeek()
        Try
            Dim nowDayOfWeek = Now.Date().DayOfWeek
            Dim nowDay = Now.Date.Day()
            Dim nowMonth = Now.Date.Month()
            Dim nowYear = Now.Date.Year()
            If nowYear < 2000 Then
                nowYear = nowYear + 1900
            Else
                nowYear = nowYear + 0
            End If
            Dim weekStartDate = DateSerial(nowYear, nowMonth, nowDay - (nowDayOfWeek - 1))
            Dim weekEndDate = DateSerial(nowYear, nowMonth, nowDay + (7 - nowDayOfWeek))
            CurrentWeek = FormatDateTime(weekStartDate) & "-" & FormatDateTime(weekEndDate)
        Catch ex As Exception
        End Try
    End Function
    Public Function NextWeek()
        Try
            Dim nowDayOfWeek = Now.Date().DayOfWeek
            Dim nowDay = Now.Date.Day()
            Dim nowMonth = Now.Date.Month()
            Dim nowYear = Now.Date.Year()
            If nowYear < 2000 Then
                nowYear = nowYear + 1900
            Else
                nowYear = nowYear + 0
            End If
            Dim weekStartDate = DateSerial(nowYear, nowMonth, nowDay - (nowDayOfWeek - 1) + 7)
            Dim weekEndDate = DateSerial(nowYear, nowMonth, nowDay + (7 - nowDayOfWeek) + 7)
            NextWeek = FormatDateTime(weekStartDate) & "-" & FormatDateTime(weekEndDate)
        Catch ex As Exception
        End Try
    End Function
    Protected Sub BtnSend_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtnSend.Click
        Dim varStrQuery As String
        Dim varStrCheck As String
        Dim varStrDropDownWeek As String
        Dim varStrSplitSpace, varStrSplitDash
        Dim varStrWeekStart As String
        Dim varStrWeekEnd As String
        Dim varStrTempWeekStart As Date
        Dim varStrTempWeekEnd As Date
        Dim varIntI As Integer
        Dim varIntJ As Integer
        Dim varBolUpdate As Boolean

        Dim clsSchedule As ETS.BL.ProductionSchedule
        Dim clsUsr As ETS.BL.Users
        Dim DSTemp As New Data.DataSet
        Try
            varStrDropDownWeek = Request.Form("DropDownWeek").ToString
            varStrSplitSpace = Split(varStrDropDownWeek, " ")
            varStrSplitDash = Split(varStrSplitSpace(UBound(varStrSplitSpace)), "-")

            varStrWeekStart = varStrSplitDash(0)
            varStrWeekEnd = varStrSplitDash(1)

            varStrMins(0) = Request.Form("txtMon")
            varStrMins(1) = Request.Form("txtTue")
            varStrMins(2) = Request.Form("txtWed")
            varStrMins(3) = Request.Form("txtThr")
            varStrMins(4) = Request.Form("txtFri")
            varStrMins(5) = Request.Form("txtSat")
            varStrMins(6) = Request.Form("txtSun")

            varStrTempWeekStart = varStrWeekStart
            varStrTempWeekEnd = varStrWeekEnd

            varIntI = 0
            While varStrTempWeekEnd >= varStrTempWeekStart
                varStrDate(varIntI) = varStrTempWeekStart
                varStrTempWeekStart = DateAdd(DateInterval.Day, 1, varStrTempWeekStart)
                varIntI = varIntI + 1
            End While

            clsSchedule = New ETS.BL.ProductionSchedule

            With clsSchedule
                .userID = Session("UserID").ToString

                DSTemp = .getScheduleDetailsByWeek(varStrWeekStart, varStrWeekEnd)
                If DSTemp.Tables.Count > 0 Then
                    If DSTemp.Tables(0).Rows.Count > 0 Then
                        varBolUpdate = True
                    Else
                        varBolUpdate = False
                    End If
                Else
                    varBolUpdate = False
                End If

                .startTime = Request.Form("DropDownStartTime") & Request.Form("DropDownStartFormat")
                .endTime = Request.Form("DropDownEndTime") & Request.Form("DropDownEndFormat")
                .lastUpdate = Now
            End With
            Dim ret_Val As Boolean
            ret_Val = clsSchedule.btn_Submit_Click(varStrDate, varStrMins, varBolUpdate)
            If ret_Val Then
                'Checking for user information for mail
                clsUsr = New ETS.BL.Users(Session("UserID"))
                Dim varStrEName As String = String.Empty
                Dim varStrEMail As String = String.Empty
                Dim varStrUserName As String = String.Empty
                varStrEName = IIf(String.IsNullOrEmpty(clsUsr.FirstName.ToString), String.Empty, clsUsr.FirstName.ToString) & " " & IIf(String.IsNullOrEmpty(clsUsr.LastName.ToString), String.Empty, clsUsr.LastName.ToString)
                varStrEMail = IIf(String.IsNullOrEmpty(clsUsr.OfficialMailID.ToString), String.Empty, clsUsr.LastName.ToString)
                If String.IsNullOrEmpty(varStrEMail) Then
                    varStrEMail = IIf(String.IsNullOrEmpty(clsUsr.OtherMailID.ToString), String.Empty, clsUsr.OtherMailID.ToString)
                End If
                varStrUserName = clsUsr.UserName.ToString
                ''end information

                Dim varStrFrom As String = String.Empty
                Dim varStrTo As String = String.Empty
                Dim varStrCC As String = String.Empty
                Dim varStrSubject As String = String.Empty
                Dim varStrMatter As String = String.Empty

                varStrFrom = objMainModule.varStrHBAFromMail
                varStrTo = objMainModule.varStrHBAToMail

                If Not String.IsNullOrEmpty(varStrEMail) Then
                    varStrCC = varStrEMail
                End If

                varStrSubject = "Production Schedule of " & varStrEName

                If varBolUpdate = True Then
                    varStrMatter = "<font size=2 face='" & "Trebuchet MS" & "'color=#000099>" & varStrEName & " has update production schedule for " & varStrWeekStart & " to " & varStrWeekEnd & " <BR><BR><b><I>Date Posted :&nbsp </I></b>" & Now() & " (EST)" & "<BR><BR>" & "<b><I>ID :&nbsp </I></b>" & varStrUserName & "<BR><BR><b><I>mon :&nbsp </I></b>" & Request.Form("txtMon") & "&nbsp &nbsp &nbsp &nbsp &nbsp <b><I>tue :&nbsp </I></b>" & Request.Form("txtTue") & "<BR><BR><b><I>wed :&nbsp </I></b>" & Request.Form("txtWed") & "&nbsp &nbsp &nbsp &nbsp &nbsp <b><I>thr :&nbsp </I></b>" & Request.Form("txtThr") & "<BR><BR><b><I>fri :&nbsp </I></b>" & Request.Form("txtFri") & "&nbsp &nbsp &nbsp &nbsp &nbsp &nbsp &nbsp<b><I>sat :&nbsp </I></b>" & Request.Form("txtSat") & "<BR><BR><b><I>sun :&nbsp </I></b>" & Request.Form("txtSun") & "&nbsp &nbsp &nbsp &nbsp &nbsp<BR><BR><b><I>starttime :&nbsp </I></b>" & Request.Form("DropDownStartTime") & Request.Form("DropDownStartFormat") & "&nbsp &nbsp &nbsp &nbsp &nbsp <b><I>endtime :&nbsp </I></b>" & Request.Form("DropDownEndTime") & Request.Form("DropDownEndFormat") & "<BR><BR><BR><BR>" & "<B>PLEASE DO NOT RESPOND TO THIS EMAIL. THIS IS JUST A NOTIFICATION MAIL. TO SEE DETAILS,LOG INTO <a href=http://SECUREIT.EDICTATE.COM>SECUREIT.EDICTATE.COM</a></b></font>"
                    If objMainModule.ERSSSendMail(varStrFrom, varStrTo, varStrCC, varStrSubject, varStrMatter) Then
                        Page.ClientScript.RegisterStartupScript(Me.GetType(), "alert", "<script language=javascript>alert('Schedule Updated Successfully'); window.location='proud.aspx';</script>")
                    End If
                Else
                    varStrMatter = "<font size=2 face='" & "Trebuchet MS" & "'color=#000099>" & varStrEName & " has send production schedule for " & varStrWeekStart & " to " & varStrWeekEnd & " <BR><BR><b><I>Date Posted :&nbsp </I></b>" & Now() & " (EST)" & "<BR><BR>" & "<b><I>ID :&nbsp </I></b>" & varStrUserName & "<BR><BR><b><I>mon :&nbsp </I></b>" & Request.Form("txtMon") & "&nbsp &nbsp &nbsp &nbsp &nbsp <b><I>tue :&nbsp </I></b>" & Request.Form("txtTue") & "<BR><BR><b><I>wed :&nbsp </I></b>" & Request.Form("txtWed") & "&nbsp &nbsp &nbsp &nbsp &nbsp <b><I>thr :&nbsp </I></b>" & Request.Form("txtThr") & "<BR><BR><b><I>fri :&nbsp </I></b>" & Request.Form("txtFri") & "&nbsp &nbsp &nbsp &nbsp &nbsp &nbsp &nbsp<b><I>sat :&nbsp </I></b>" & Request.Form("txtSat") & "<BR><BR><b><I>sun :&nbsp </I></b>" & Request.Form("txtSun") & "&nbsp &nbsp &nbsp &nbsp &nbsp<BR><BR><b><I>starttime :&nbsp </I></b>" & Request.Form("DropDownStartTime") & Request.Form("DropDownStartFormat") & "&nbsp &nbsp &nbsp &nbsp &nbsp <b><I>endtime :&nbsp </I></b>" & Request.Form("DropDownEndTime") & Request.Form("DropDownEndFormat") & "<BR><BR><BR><BR>" & "<B>PLEASE DO NOT RESPOND TO THIS EMAIL. THIS IS JUST A NOTIFICATION MAIL. TO SEE DETAILS,LOG INTO <a href=http://SECUREIT.EDICTATE.COM>SECUREIT.EDICTATE.COM</a></b></font>"
                    If objMainModule.ERSSSendMail(varStrFrom, varStrTo, varStrCC, varStrSubject, varStrMatter) Then
                        Page.ClientScript.RegisterStartupScript(Me.GetType(), "alert", "<script language=javascript>alert('Schedule Sent Successfully'); window.location='proud.aspx';</script>")
                    End If
                End If
            End If

        Catch ex As Exception
            Response.Write(ex.Message)
        Finally
            DSTemp.Dispose()
            clsSchedule = Nothing
            clsUsr = Nothing
        End Try
    End Sub
    Protected Sub DropDownWeek_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles DropDownWeek.SelectedIndexChanged
        Dim varStrDropDownWeek As String
        Dim varStrSplitSpace, varStrSplitDash
        Dim varStrWeekStart As String
        Dim varStrWeekEnd As String
        Dim varStrQuery As String
        Dim varStrStartTime As String
        Dim varStrEndTime As String
        Dim varStrStartTimeAvail As Integer
        Dim varStrStartTimeFormatAvail As String
        Dim varStrEndTimeAvail As Integer
        Dim varStrEndTimeFormatAvail As String
        Dim varStrMinsAvail(6)
        Dim varIntJ As Integer
        Dim varBolFlag As Boolean

        Dim clsSchedule As ETS.BL.ProductionSchedule
        Dim DS As New Data.DataSet
        Dim Reader As Data.DataTableReader
        Try
            varStrDropDownWeek = DropDownWeek.Items(DropDownWeek.SelectedIndex).Value.ToString

            If varStrDropDownWeek <> "" Then
                varStrSplitSpace = Split(varStrDropDownWeek, " ")
                varStrSplitDash = Split(varStrSplitSpace(UBound(varStrSplitSpace)), "-")

                varStrWeekStart = varStrSplitDash(0)
                varStrWeekEnd = varStrSplitDash(1)
            End If

            clsSchedule = New ETS.BL.ProductionSchedule
            clsSchedule.userID = Session("UserID").ToString
            DS = clsSchedule.getScheduleDetailsByWeek(varStrWeekStart, varStrWeekEnd)

            Reader = DS.Tables(0).CreateDataReader
            varIntJ = 0

            If Reader.HasRows Then
                While Reader.Read
                    varStrMinsAvail(varIntJ) = Reader.GetDecimal(Reader.GetOrdinal("Mins"))
                    varStrStartTime = Reader.GetString(Reader.GetOrdinal("startTime"))
                    varStrEndTime = Reader.GetString(Reader.GetOrdinal("endTime"))
                    varIntJ = varIntJ + 1
                    varBolFlag = True
                End While
            End If
            Reader.Close()

            ReSet()
            RemoveReadOnlyText()
            If varBolFlag Then
                txtMon.Text = varStrMinsAvail(0)
                txtTue.Text = varStrMinsAvail(1)
                txtWed.Text = varStrMinsAvail(2)
                txtThr.Text = varStrMinsAvail(3)
                txtFri.Text = varStrMinsAvail(4)
                txtSat.Text = varStrMinsAvail(5)
                txtSun.Text = varStrMinsAvail(6)

                If Len(Trim(varStrStartTime)) = 4 Then
                    varStrStartTimeAvail = Left(Trim(varStrStartTime), 2)
                ElseIf Len(Trim(varStrStartTime)) = 3 Then
                    varStrStartTimeAvail = Left(Trim(varStrStartTime), 1)
                End If
                varStrStartTimeFormatAvail = Right(Trim(varStrStartTime), 2)

                If Len(Trim(varStrEndTime)) = 4 Then
                    varStrEndTimeAvail = Left(Trim(varStrEndTime), 2)
                ElseIf Len(Trim(varStrEndTime)) = 3 Then
                    varStrEndTimeAvail = Left(Trim(varStrEndTime), 1)
                End If
                varStrEndTimeFormatAvail = Right(Trim(varStrEndTime), 2)

                DropDownStartTime.SelectedValue = varStrStartTimeAvail
                DropDownStartFormat.SelectedValue = varStrStartTimeFormatAvail
                DropDownEndTime.SelectedValue = varStrEndTimeAvail
                DropDownEndFormat.SelectedValue = varStrEndTimeFormatAvail

                ReadOnlyText(varStrWeekStart)
            End If
        Catch ex As Exception
        Finally
            clsSchedule = Nothing
            Reader = Nothing
            DS.Dispose()
        End Try
    End Sub
    Protected Sub ReSet()
        txtMon.Text = "00"
        txtTue.Text = "00"
        txtWed.Text = "00"
        txtThr.Text = "00"
        txtFri.Text = "00"
        txtSat.Text = "00"
        txtSun.Text = "00"
        DropDownStartTime.SelectedIndex = 0
        DropDownStartFormat.SelectedIndex = 0
        DropDownEndTime.SelectedIndex = 0
        DropDownEndFormat.SelectedIndex = 0
    End Sub
    Protected Sub ReadOnlyText(ByVal TempDate As Date)
        Dim varIntDiff As Integer
        varIntDiff = DateDiff(DateInterval.Day, TempDate, Now())
        If varIntDiff >= 0 Then
            txtMon.ReadOnly = True
        End If
        If varIntDiff >= 1 Then
            txtTue.ReadOnly = True
        End If
        If varIntDiff >= 2 Then
            txtWed.ReadOnly = True
        End If
        If varIntDiff >= 3 Then
            txtThr.ReadOnly = True
        End If
        If varIntDiff >= 4 Then
            txtFri.ReadOnly = True
        End If
        If varIntDiff >= 5 Then
            txtSat.ReadOnly = True
        End If
        If varIntDiff >= 6 Then
            txtSun.ReadOnly = True
        End If
    End Sub
    Protected Sub RemoveReadOnlyText()
        txtMon.ReadOnly = False
        txtTue.ReadOnly = False
        txtWed.ReadOnly = False
        txtThr.ReadOnly = False
        txtFri.ReadOnly = False
        txtSat.ReadOnly = False
        txtSun.ReadOnly = False
    End Sub
End Class
