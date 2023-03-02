Imports MainModule
Partial Class TechReports_TechDailyAttendance
    Inherits BasePage
    Dim objMainModule As New MainModule
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Request.Form("txtDateReport") <> "" Then
            txtDateReport.Text = Request.Form("txtDateReport")
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


        Dim clsUsr As ETS.BL.Users
        Dim DSUsrs As New Data.DataSet
        Dim DVUsrs As Data.DataView

        Try
            varDtReportDate = txtDateReport.Text

            clsUsr = New ETS.BL.Users
            DSUsrs = clsUsr.getUsersList(Session("ContractorID"), Session("WorkGroupID"), String.Empty)
            If DSUsrs.Tables.Count > 0 Then
                If DSUsrs.Tables(0).Rows.Count > 0 Then
                    DVUsrs = New Data.DataView(DSUsrs.Tables(0), "UserName LIKE 'e%'", String.Empty, Data.DataViewRowState.CurrentRows)
                    If DVUsrs.Count > 0 Then
                        Dim oRecUserID As Data.DataTableReader
                        oRecUserID = DVUsrs.ToTable().CreateDataReader
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
                    End If
                End If
            End If


            Table2.Visible = True
            Table3.Visible = True

            For varIntI = 0 To varArrUserID.Count - 1
                Dim varTblRow As New TableRow
                Dim varTblCellUserName As New TableCell
                Dim varTblCellEmpName As New TableCell
                Dim varTblCellStatus As New TableCell
                Dim varWOff1 As String = String.Empty
                Dim varWOff2 As String = String.Empty
                Dim varAttendanceStatus As String = String.Empty
                Dim varStrLeaveType As String = String.Empty
                Dim varLeaveStatus As String = String.Empty

                Dim clsLB As ETS.BL.LeaveBalance
                Try
                    clsLB = New ETS.BL.LeaveBalance
                    clsLB.UserID = varArrUserID(varIntI)
                    clsLB.getLeaveBalanceDetails()
                    varWOff1 = clsLB.WeeklyOff1
                    varWOff2 = clsLB.WeeklyOff2
                Catch ex As Exception
                    Response.Write(ex.Message)
                Finally
                    clsLB = Nothing
                End Try


                Dim varStrPost As String = String.Empty

                If Not String.IsNullOrEmpty(varArrDestID(varIntI)) Then
                    Dim clsDeptDesignation As ETS.BL.DeptDesignations
                    Try
                        clsDeptDesignation = New ETS.BL.DeptDesignations
                        clsDeptDesignation.DesignationID = varArrDestID(varIntI)
                        clsDeptDesignation.getDesignationDetails()
                        varStrPost = clsDeptDesignation.Name.ToString
                    Catch ex As Exception
                        Response.Write(ex.Message)
                    Finally
                        clsDeptDesignation = Nothing
                    End Try
                End If

                Dim clsAtt As ETS.BL.Attendance
                Try
                    clsAtt = New ETS.BL.Attendance
                    clsAtt.UserID = varArrUserID(varIntI)
                    clsAtt.AttDate = varDtReportDate
                    clsAtt.getAttendanceDetails()
                    varAttendanceStatus = clsAtt.Status
                Catch ex As Exception

                Finally
                    clsAtt = Nothing
                End Try

                If String.IsNullOrEmpty(varAttendanceStatus) = True Then
                    If Trim(UCase(WeekdayName(Weekday(varDtReportDate)))) = Trim(UCase(varWOff1)) Or Trim(UCase(WeekdayName(Weekday(varDtReportDate)))) = Trim(UCase(varWOff2)) Then
                        varAttendanceStatus = "O"
                    Else
                        If Trim(UCase(WeekdayName(Weekday(varDtReportDate)))) = Trim(UCase("Saturday")) And Trim(UCase(varStrPost)) = Trim(UCase("QA")) Then

                            Dim clsDS As ETS.BL.DutyRoster
                            Try
                                clsDS = New ETS.BL.DutyRoster
                                clsDS.UserID = varArrUserID(varIntI)
                                clsDS.DutyDate = varDtReportDate
                                clsDS.getDutyRosterDetails()

                                If Trim(UCase(clsDS.ShiftPrefix)) = Trim(UCase("N")) Then
                                    varAttendanceStatus = "O"
                                End If
                            Catch ex As Exception
                                Response.Write(ex.Message)
                            Finally
                                clsDS = Nothing
                            End Try
                        End If
                    End If
                End If

                If String.IsNullOrEmpty(varAttendanceStatus) = True Then
                    varAttendanceStatus = "A"
                End If

                Dim clsLeave As ETS.BL.Leave
                Dim DSLeave As New Data.DataSet
                Dim DVLeave As Data.DataView
                Dim objRecLeaveInfo As Data.DataTableReader
                Try
                    clsLeave = New ETS.BL.Leave
                    DSLeave = clsLeave.getLeaveListByUsr(varArrUserID(varIntI))
                    If DSLeave.Tables.Count > 0 Then
                        If DSLeave.Tables(0).Rows.Count > 0 Then
                            DVLeave = New Data.DataView(DSLeave.Tables(0), "Status='Approved' AND (StartDate >= '" & varDtReportDate & "' OR EndDate >='" & varDtReportDate & "') AND (StartDate <= '" & varDtReportDate & "' OR EndDate <='" & varDtReportDate & "') AND (TypeOfLeave IS NOT NULL AND TypeOfLeave <> 'CO') AND (IsDeleted = 0 or IsDeleted IS NULL)", String.Empty, Data.DataViewRowState.CurrentRows)
                            If DVLeave.Count > 0 Then
                                objRecLeaveInfo = DVLeave.ToTable.CreateDataReader
                                If objRecLeaveInfo.HasRows Then
                                    While objRecLeaveInfo.Read
                                        varStrLeaveType = objRecLeaveInfo.GetString(objRecLeaveInfo.GetOrdinal("TypeOfLeave"))
                                    End While
                                End If
                                objRecLeaveInfo.Close()
                            End If
                        End If
                    End If
                Catch ex As Exception
                    Response.Write(ex.Message)
                Finally
                    clsLeave = Nothing
                    DSLeave.Dispose()
                    DVLeave = Nothing
                    objRecLeaveInfo = Nothing
                End Try


                varTblRow.Font.Name = "Arial"
                varTblRow.Font.Size = 8
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
        Finally
            clsUsr = Nothing
            DSUsrs.Dispose()
            DVUsrs.Dispose()
        End Try
    End Sub
    Protected Sub btnExport_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnExport.Click
        Try
            Response.Clear()
            Dim filename = "Daily Attendance Report " & Now & " .xls"
            Response.AddHeader("content-disposition", "attachment;filename=" & filename)
            Response.ContentType = "application/vnd.ms-excel"
            Response.Charset = ""
            Me.EnableViewState = False
            Dim tw As New System.IO.StringWriter()
            Dim hw As New System.Web.UI.HtmlTextWriter(tw)
            filltable()
            Table3.RenderControl(hw)
            Response.Write(tw.ToString())
            Response.End()
        Catch ex As Exception
        End Try
    End Sub
    Public Overrides Sub VerifyRenderingInServerForm(ByVal control As Control)
    End Sub
End Class
