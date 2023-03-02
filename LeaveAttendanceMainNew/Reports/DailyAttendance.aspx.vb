Imports MainModule
Partial Class LeaveSanctioned
    Inherits BasePage
    Dim objMainModule As New MainModule
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'If txtDateReport.Text <> "" Then
        '    txtDateReport.Text = txtDateReport.Text
        'End If
        If Page.IsPostBack Then
            If Request.Form("txtDateReport") <> "" Then
                txtDateReport.Text = Request.Form("txtDateReport")
            End If
        End If
    End Sub
    Protected Sub BtnSubmit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtnSubmit.Click
        filltable()
    End Sub
    Protected Sub filltable()

        Dim varArrUserID As New ArrayList
        Dim varArrUserName As New ArrayList
        Dim varArrDestID As New ArrayList
        Dim varArrUserLoginName As New ArrayList
        Dim varDtReportDate As Date
        Dim varStrTemp As String
        Dim varIntI As Integer

        Dim objConn As New Data.SqlClient.SqlConnection
        objConn = objMainModule.OpenConnection(objConn)

        Try
            varDtReportDate = txtDateReport.Text
            'varDtReportDate = "1/28/2010"

            'ViewState("txtReport") = Request.Form("txtDateReport")
            'Response.Write(varDtReportDate)
            'Response.End()
            'Dim oCommandUserID As New Data.SqlClient.SqlCommand("SELECT UserID,UserName,FirstName,LastName,DesignationID FROM DBO.tblUsers U INNER JOIN DBO.tblDepartments D ON U.DepartmentID=D.DepartmentID  WHERE (IsDeleted IS NULL OR IsDeleted <>'True') AND D.Name='Production'", objConn)
            Dim oCommandUserID As New Data.SqlClient.SqlCommand("SELECT UserID,UserName,FirstName,LastName,DesignationID FROM DBO.tblUsers U INNER JOIN DBO.tblDepartments D ON U.DepartmentID=D.DepartmentID  WHERE (IsDeleted IS NULL OR IsDeleted = 0) AND U.UserName LIKE 'e%' AND ContractorID='" & Session("ContractorID").ToString & "' ORDER BY FirstName,LastName", objConn)

            Dim oRecUserID As Data.SqlClient.SqlDataReader = oCommandUserID.ExecuteReader()
            If oRecUserID.HasRows Then
                While oRecUserID.Read
                    varStrTemp = oRecUserID.GetString(oRecUserID.GetOrdinal("FirstName")) & " " & oRecUserID.GetString(oRecUserID.GetOrdinal("LastName"))
                    varArrUserID.Add(oRecUserID.GetGuid(oRecUserID.GetOrdinal("UserID")).ToString)
                    varArrUserName.Add(varStrTemp)
                    If Not oRecUserID.IsDBNull(oRecUserID.GetOrdinal("DesignationID")) Then
                        varArrDestID.Add(oRecUserID.GetGuid(oRecUserID.GetOrdinal("DesignationID")).ToString)
                    Else
                        varArrDestID.Add(String.Empty)
                    End If

                    varArrUserLoginName.Add(oRecUserID.GetString(oRecUserID.GetOrdinal("UserName")))
                End While
            End If
            oRecUserID.Close()
            oRecUserID = Nothing
            oCommandUserID = Nothing
            Table2.Visible = True
            Table3.Visible = True

            For varIntI = 0 To varArrUserID.Count - 1
                Dim varTblRow As New TableRow
                Dim varTblCellUserName As New TableCell
                Dim varTblCellEmpName As New TableCell
                Dim varTblCellStatus As New TableCell
                Dim varWOff1 As String
                Dim varWOff2 As String
                Dim varAttendanceStatus As String
                Dim varStrLeaveType As String
                Dim varLeaveStatus As String

                Dim objWeeklyOff As New Data.SqlClient.SqlCommand("SELECT WeeklyOff1,WeeklyOff2 FROM DBO.tblLeaveBalance WHERE UserID='" & varArrUserID(varIntI) & "'", objConn)
                Dim objRecWeeklyOff As Data.SqlClient.SqlDataReader = objWeeklyOff.ExecuteReader
                If objRecWeeklyOff.HasRows Then
                    While objRecWeeklyOff.Read
                        If Not objRecWeeklyOff.IsDBNull(objRecWeeklyOff.GetOrdinal("WeeklyOff1")) Then
                            varWOff1 = objRecWeeklyOff.GetString(objRecWeeklyOff.GetOrdinal("WeeklyOff1"))
                        Else
                            varWOff1 = ""
                        End If
                        If Not objRecWeeklyOff.IsDBNull(objRecWeeklyOff.GetOrdinal("WeeklyOff2")) Then
                            varWOff2 = objRecWeeklyOff.GetString(objRecWeeklyOff.GetOrdinal("WeeklyOff2"))
                        Else
                            varWOff2 = ""
                        End If
                    End While
                End If
                objRecWeeklyOff.Close()
                objRecWeeklyOff = Nothing
                objWeeklyOff = Nothing

                Dim varStrPost As String = String.Empty

                If Not String.IsNullOrEmpty(varArrDestID(varIntI)) Then
                    Dim objPost As New Data.SqlClient.SqlCommand("SELECT Name FROM DBO.tblDeptDesignations WHERE DesignationID='" & varArrDestID(varIntI) & "'", objConn)
                    Dim objRecPost As Data.SqlClient.SqlDataReader = objPost.ExecuteReader
                    If objRecPost.HasRows Then
                        While objRecPost.Read
                            If Not objRecPost.IsDBNull(objRecPost.GetOrdinal("Name")) Then
                                varStrPost = objRecPost.GetString(objRecPost.GetOrdinal("Name"))
                            End If
                        End While
                    End If
                    objRecPost.Close()
                    objRecPost = Nothing
                    objPost = Nothing
                End If

                Dim objCmdAttendanceStatus As New Data.SqlClient.SqlCommand("SELECT Status FROM DBO.tblAttendance WHERE UserID='" & varArrUserID(varIntI) & "' AND AttDate='" & varDtReportDate & "'", objConn)
                Dim objRecAttendanceStatus As Data.SqlClient.SqlDataReader = objCmdAttendanceStatus.ExecuteReader
                If objRecAttendanceStatus.HasRows Then
                    While objRecAttendanceStatus.Read
                        If Not objRecAttendanceStatus.IsDBNull(objRecAttendanceStatus.GetOrdinal("Status")) Then
                            varAttendanceStatus = objRecAttendanceStatus.GetString(objRecAttendanceStatus.GetOrdinal("Status"))
                        Else
                            varAttendanceStatus = ""
                        End If
                    End While
                End If
                objRecAttendanceStatus.Close()
                objRecAttendanceStatus = Nothing
                objCmdAttendanceStatus = Nothing

                If varAttendanceStatus = "" Then
                    If Trim(UCase(WeekdayName(Weekday(varDtReportDate)))) = Trim(UCase(varWOff1)) Or Trim(UCase(WeekdayName(Weekday(varDtReportDate)))) = Trim(UCase(varWOff2)) Then
                        varAttendanceStatus = "O"
                    Else
                        If Trim(UCase(WeekdayName(Weekday(varDtReportDate)))) = Trim(UCase("Saturday")) And Trim(UCase(varStrPost)) = Trim(UCase("QA")) Then
                            Dim objDutyRoster As New Data.SqlClient.SqlCommand("SELECT ShiftPrefix FROM DBO.tblDutyRoster WHERE UserID='" & varArrUserID(varIntI) & "' AND DutyDate='" & varDtReportDate & "'", objConn)
                            Dim objRecDutyRoster As Data.SqlClient.SqlDataReader = objDutyRoster.ExecuteReader
                            If objRecDutyRoster.HasRows Then
                                While objRecDutyRoster.Read
                                    If Trim(UCase(objRecDutyRoster.GetString(objRecDutyRoster.GetOrdinal("ShiftPrefix")))) = Trim(UCase("N")) Then
                                        varAttendanceStatus = "O"
                                    End If
                                End While
                            End If
                            objRecDutyRoster.Close()
                            objRecDutyRoster = Nothing
                            objDutyRoster = Nothing
                        End If
                    End If
                End If

                If varAttendanceStatus = "" Then
                    varAttendanceStatus = "A"
                End If

                Dim objCmdLeaveInfo As New Data.SqlClient.SqlCommand("SELECT TypeOfLeave FROM DBO.tblLeave WHERE UserID='" & varArrUserID(varIntI) & "' AND '" & varDtReportDate & "' BETWEEN StartDate AND EndDate AND IsDeleted<>'TRUE'", objConn)
                Dim objRecLeaveInfo As Data.SqlClient.SqlDataReader = objCmdLeaveInfo.ExecuteReader
                If objRecLeaveInfo.HasRows Then
                    While objRecLeaveInfo.Read
                        varStrLeaveType = objRecLeaveInfo.GetString(objRecLeaveInfo.GetOrdinal("TypeOfLeave"))
                    End While
                End If
                objRecLeaveInfo.Close()
                objRecLeaveInfo = Nothing
                objCmdLeaveInfo = Nothing

                varTblRow.Font.Name = "Trebuchet MS"
                varTblRow.Font.Size = 10
                varTblCellUserName.Text = varArrUserLoginName(varIntI)
                varTblCellEmpName.Text = varArrUserName(varIntI)
                varTblCellEmpName.HorizontalAlign = HorizontalAlign.Left
                varTblRow.Cells.Add(varTblCellUserName)
                varTblRow.Cells.Add(varTblCellEmpName)

                If Trim(UCase(varAttendanceStatus)) = Trim(UCase("L")) Or Trim(UCase(varAttendanceStatus)) = Trim(UCase("HP")) Then
                    If Trim(UCase(varStrLeaveType)) <> Trim(UCase("LWP")) Then
                        If Trim(UCase(varStrLeaveType)) = Trim(UCase("HL")) Or Trim(UCase(varStrLeaveType)) = Trim(UCase("LWPHL")) Or Trim(UCase(varStrLeaveType)) = Trim(UCase("AHL")) Or Trim(UCase(varStrLeaveType)) = Trim(UCase("ELHL")) Or Trim(UCase(varStrLeaveType)) = Trim(UCase("CLHL")) Then
                            varLeaveStatus = "HP"
                        Else
                            varLeaveStatus = "L"
                        End If
                    End If
                Else
                    varLeaveStatus = varAttendanceStatus
                End If
                varTblCellStatus.Text = varLeaveStatus
                varTblRow.Cells.Add(varTblCellStatus)

                Table3.Rows.Add(varTblRow)
                varLeaveStatus = ""
                varAttendanceStatus = ""
                varWOff1 = ""
                varWOff2 = ""
            Next
        Catch ex As Exception
            Response.Write(ex.Message)
            'Response.End()
        Finally
            If objConn.State <> Data.ConnectionState.Closed Then
                objConn.Close()
                objConn = Nothing
            End If
        End Try
    End Sub
    Protected Sub btnExport_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnExport.Click
        Try
            Response.Clear()
            ' Set the content type to Excel.
            Dim filename = "Daily Attendance Report " & MonthName(Month(Now)) & " " & Year(Now) & ".xls"
            Response.AddHeader("content-disposition", "attachment;filename=" & filename)

            Response.ContentType = "application/vnd.ms-excel"
            ' Remove the charset from the Content-Type header.
            Response.Charset = ""
            ' Turn off the view state.
            Me.EnableViewState = False

            Dim tw As New System.IO.StringWriter()
            Dim hw As New System.Web.UI.HtmlTextWriter(tw)
            filltable()

            ' Get the HTML for the control.
            Table1.RenderControl(hw)

            'form1.Controls.Clear()
            'FillTable(Trim(DropDownMonth.Items(DropDownMonth.SelectedIndex).Value.ToString), Trim(DropDownYear.Items(DropDownYear.SelectedIndex).Value.ToString), True, False)
            'form1.Controls.Add(Table1)
            'Dim sb As New System.Text.StringBuilder()
            'Dim htWriter As New HtmlTextWriter(New System.IO.StringWriter(sb))
            'form1.RenderControl(htWriter)

            ' Write the HTML back to the browser.
            Response.Write(tw.ToString())
            ' End the response.
            Response.End()
        Catch ex As Exception
            Response.Write(ex.Message)
            Response.End()
        End Try

    End Sub
    Public Overloads Overrides Sub VerifyRenderingInServerForm(ByVal control As Control)
        Exit Sub
    End Sub
End Class
