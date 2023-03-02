Imports MainModule
Partial Class LeaveSanctioned
    Inherits BasePage
    Dim objMainModule As New MainModule
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If txtDateReport.Text <> "" Then
            txtDateReport.Text = txtDateReport.Text
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

        Dim varStrTemp As String
        Dim varIntI As Integer
        Dim varDateDiff As Double
        Dim varDtStartDate As Date
        Dim varDtEndDate As Date
        Dim varDtTempStartDate As Date
        Dim varDtTempEndDate As Date
        Dim varLeaveCount As Double
        Dim varLeaveLWPCount As Double

        Dim clsUsr As ETS.BL.Users
        Dim DSUsrs As New Data.DataSet
        Dim DV As Data.DataView
        Try
            varDtEndDate = CDate(txtDateReport.Text)
            varDtStartDate = DateSerial(Year(varDtEndDate), Month(varDtEndDate), 1)
            varDtTempStartDate = varDtStartDate
            varDtTempEndDate = varDtEndDate
            txtDateReport.Text = varDtEndDate
            clsUsr = New ETS.BL.Users

            DSUsrs = clsUsr.getUsersList(Session("ContractorID"), Session("WorkGroupID"), String.Empty)
            If DSUsrs.Tables.Count > 0 Then
                If DSUsrs.Tables(0).Rows.Count > 0 Then
                    DV = New Data.DataView(DSUsrs.Tables(0), "UserName LIKE 'e%'", String.Empty, Data.DataViewRowState.CurrentRows)
                    If DV.Count > 0 Then
                        Dim oRecUserID As Data.DataTableReader
                        oRecUserID = DV.ToTable().CreateDataReader

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
                Dim varTblCellLeaveSanction As New TableCell
                Dim varTblCellLeaveWithoutPay As New TableCell
                Dim varTblCellWeeklyOff As New TableCell
                Dim varWOff1 As String = String.Empty
                Dim varWOff2 As String = String.Empty
                Dim varIntNext As Integer
                Dim varIntPrev As Integer
                Dim varWeeklyOffSearch As New ArrayList
                Dim varDutyRosterWeeklyOffSearch As New ArrayList

                Dim varWeeklyOffCount As Integer
                Dim varWeeklyOffDeductCount As Integer

                varWeeklyOffSearch.Clear()
                varWeeklyOffCount = 0
                varWeeklyOffDeductCount = 0
                varLeaveCount = 0
                varLeaveLWPCount = 0
                varTblRow.Font.Name = "Arial"
                varTblRow.Font.Size = 8
                varTblCellUserName.Text = varArrUserLoginName(varIntI)
                varTblCellEmpName.Text = varArrUserName(varIntI)
                varTblCellEmpName.HorizontalAlign = HorizontalAlign.Left
                varTblRow.Cells.Add(varTblCellUserName)
                varTblRow.Cells.Add(varTblCellEmpName)

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
                
                If varWOff1 <> "" And varWOff2 <> "" Then
                    varIntNext = 1
                    varIntPrev = -2
                Else
                    varIntNext = 1
                    varIntPrev = -1
                End If

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


                Dim varDutyRosterWOff As New StringBuilder
                Dim varEmptyWOff As Boolean = True

                Dim clsDR As ETS.BL.DutyRoster
                Dim DSW As New Data.DataSet
                Dim DVW As New Data.DataView
                Try
                    clsDR = New ETS.BL.DutyRoster
                    clsDR.UserID = varArrUserID(varIntI)
                    clsDR.ShiftPrefix = "O"
                    DSW = clsDR.getDutyRosterList()

                    If DSW.Tables.Count > 0 Then
                        If DSW.Tables(0).Rows.Count > 0 Then
                            'DVW = New Data.DataView(DSW.Tables(0), "  DATEDIFF(day,DutyDate, '" & varDtEndDate & "') BETWEEN 0 and DATEDIFF(day,'" & varDtStartDate & "','" & varDtEndDate & "') ", " DutyDate ", Data.DataViewRowState.CurrentRows)
                            DVW = New Data.DataView(DSW.Tables(0), "  (DutyDate >= '" & varDtStartDate & "' and DutyDate <= '" & varDtEndDate & "') ", " DutyDate ", Data.DataViewRowState.CurrentRows)
                            If DVW.Count > 0 Then
                                For Each objRecWeekOff As Data.DataRow In DVW.ToTable.Rows
                                    If String.IsNullOrEmpty(varDutyRosterWOff.ToString) Then
                                        varDutyRosterWOff.Append(objRecWeekOff("DutyDate"))
                                    Else
                                        varDutyRosterWOff.Append("," & objRecWeekOff("DutyDate"))
                                    End If
                                    varWeeklyOffCount = varWeeklyOffCount + 1
                                    varDutyRosterWeeklyOffSearch.Add(objRecWeekOff("DutyDate"))
                                    varEmptyWOff = False
                                Next
                            End If
                        End If
                    End If
                Catch ex As Exception
                    'Response.Write(ex.Message)
                Finally
                    clsDR = Nothing
                    DSW = Nothing
                    DVW = Nothing
                End Try

                'If varArrUserID(varIntI).ToString.ToUpper = "C7CC8141-7C0A-4E87-8B6B-CB64BCC2D98C" Then
                '    Response.Write("weekly off" & varWeeklyOffCount)
                'End If
                If varEmptyWOff = True Then
                    While varDtEndDate >= varDtStartDate
                        If Trim(UCase(varWOff1)) = Trim(UCase(WeekdayName(Weekday(varDtStartDate)))) Then
                            varWeeklyOffCount = varWeeklyOffCount + 1
                            varDutyRosterWeeklyOffSearch.Add(varDtStartDate)
                        ElseIf Trim(UCase(varWOff2)) = Trim(UCase(WeekdayName(Weekday(varDtStartDate)))) Then
                            varWeeklyOffCount = varWeeklyOffCount + 1
                            varDutyRosterWeeklyOffSearch.Add(varDtStartDate)
                        End If
                        varDtStartDate = DateAdd(DateInterval.Day, 1, varDtStartDate)
                    End While
                End If

                'While varDtEndDate >= varDtStartDate
                '    If Trim(UCase(WeekdayName(Weekday(varDtStartDate)))) = Trim(UCase(varWOff1)) Then
                '        varWeeklyOffCount = varWeeklyOffCount + 1
                '        varWeeklyOffSearch.Add(varDtStartDate)
                '    ElseIf Trim(UCase(WeekdayName(Weekday(varDtStartDate)))) = Trim(UCase(varWOff2)) Then
                '        varWeeklyOffCount = varWeeklyOffCount + 1
                '        varWeeklyOffSearch.Add(varDtStartDate)
                '    End If

                '    'If Trim(UCase(WeekdayName(Weekday(varDtStartDate)))) = Trim(UCase("Saturday")) And Trim(UCase(varStrPost)) = Trim(UCase("Senior Editor")) Then
                '    If Trim(UCase(WeekdayName(Weekday(varDtStartDate)))) = Trim(UCase("Saturday")) Then
                '        If Trim(UCase(varArrUserID(varIntI))) = Trim(UCase("0B7AA385-86F1-494C-A7F9-65E042873268")) Then
                '            'Response.Write(varWOff1 & "," & varWOff2)
                '            'Response.Write("SELECT ShiftPrefix FROM DBO.tblDutyRoster WHERE UserID='" & varArrUserID(varIntI) & "' AND DutyDate='" & varDtStartDate & "'")
                '        End If

                '        Dim clsDS As ETS.BL.DutyRoster
                '        Try
                '            clsDS = New ETS.BL.DutyRoster
                '            clsDS.UserID = varArrUserID(varIntI)
                '            clsDS.DutyDate = varDtStartDate
                '            clsDS.getDutyRosterDetails()

                '            If Trim(UCase(clsDS.ShiftPrefix)) = Trim(UCase("N")) Or Trim(UCase(clsDS.ShiftPrefix)) = Trim(UCase("FN")) Or Trim(UCase(clsDS.ShiftPrefix)) = Trim(UCase("O")) Then
                '                varWeeklyOffCount = varWeeklyOffCount + 1
                '            End If
                '        Catch ex As Exception
                '            Response.Write(ex.Message)
                '        Finally
                '            clsDS = Nothing
                '        End Try
                '    End If
                '    varDtStartDate = DateAdd(DateInterval.Day, 1, varDtStartDate)
                'End While

                varDtStartDate = varDtTempStartDate
                varDtEndDate = varDtTempEndDate

                Dim clsLeave As ETS.BL.Leave
                Dim DSLeave As New Data.DataSet
                Dim DVLeave As Data.DataView
                Dim objRecLeave As Data.DataTableReader
                Try
                    clsLeave = New ETS.BL.Leave
                    DSLeave = clsLeave.getLeaveListByUsr(varArrUserID(varIntI))
                    If DSLeave.Tables.Count > 0 Then
                        If DSLeave.Tables(0).Rows.Count > 0 Then
                            DVLeave = New Data.DataView(DSLeave.Tables(0), "Status='Approved' AND (StartDate >= '" & varDtStartDate & "' OR EndDate >='" & varDtStartDate & "') AND (StartDate <= '" & varDtEndDate & "' OR EndDate <='" & varDtEndDate & "') AND (TypeOfLeave IS NOT NULL AND TypeOfLeave <> 'CO') AND (IsDeleted = 0 or IsDeleted IS NULL)", String.Empty, Data.DataViewRowState.CurrentRows)
                            If DVLeave.Count > 0 Then
                                objRecLeave = DVLeave.ToTable.CreateDataReader

                                If objRecLeave.HasRows Then
                                    While objRecLeave.Read
                                        Dim varDBSDate As Date
                                        Dim varDBEDate As Date
                                        Dim varTempDBSDate As Date
                                        Dim varTempDBEDate As Date

                                        If Not objRecLeave.IsDBNull(objRecLeave.GetOrdinal("StartDate")) And Not objRecLeave.IsDBNull(objRecLeave.GetOrdinal("EndDate")) Then
                                            If Month(objRecLeave.GetDateTime(objRecLeave.GetOrdinal("StartDate"))) = Month(varDtStartDate) And Year(objRecLeave.GetDateTime(objRecLeave.GetOrdinal("StartDate"))) = Year(varDtStartDate) Then
                                                varDateDiff = DateDiff(DateInterval.Day, objRecLeave.GetDateTime(objRecLeave.GetOrdinal("StartDate")), objRecLeave.GetDateTime(objRecLeave.GetOrdinal("EndDate"))) + 1
                                            Else
                                                Dim TD1 As Date = objRecLeave.GetDateTime(objRecLeave.GetOrdinal("StartDate"))
                                                Dim TD2 As Date = objRecLeave.GetDateTime(objRecLeave.GetOrdinal("EndDate"))

                                                While TD2 >= TD1
                                                    If Month(TD1) = Month(varDtStartDate) And Year(TD1) = Year(varDtStartDate) Then
                                                        varDateDiff = varDateDiff + 1
                                                    End If
                                                    TD1 = DateAdd(DateInterval.Day, 1, TD1)
                                                End While
                                            End If
                                            varDBSDate = objRecLeave.GetDateTime(objRecLeave.GetOrdinal("StartDate"))
                                            varDBEDate = objRecLeave.GetDateTime(objRecLeave.GetOrdinal("EndDate"))
                                        End If

                                        varTempDBSDate = varDBSDate
                                        varTempDBEDate = varDBEDate

                                        While varDBEDate >= varDBSDate
                                            If DateDiff(DateInterval.Day, varDBSDate, varDtEndDate) < 0 Then
                                                varDateDiff = varDateDiff - 1
                                            End If
                                            'If Trim(UCase(WeekdayName(Weekday(varDBSDate)))) = Trim(UCase(varWOff1)) Or Trim(UCase(WeekdayName(Weekday(varDBSDate)))) = Trim(UCase(varWOff2)) Then
                                            '    Dim objTempObject As Object
                                            '    objTempObject = varDBSDate
                                            '    varWeeklyOffSearch.Remove(objTempObject)
                                            'End If
                                            If varDutyRosterWOff.ToString.IndexOf(varDBSDate) >= 0 Then
                                                Dim objTempObject As Object
                                                objTempObject = varDBSDate
                                                varDutyRosterWeeklyOffSearch.Remove(objTempObject)
                                            End If
                                            varDBSDate = DateAdd(DateInterval.Day, 1, varDBSDate)
                                            varDBSDate = DateAdd(DateInterval.Day, 1, varDBSDate)
                                        End While

                                        varDBSDate = varTempDBSDate
                                        varDBEDate = varTempDBEDate

                                        If Trim(UCase(objRecLeave.GetString(objRecLeave.GetOrdinal("TypeOfLeave")))) = Trim(UCase("LWP")) Or Trim(UCase(objRecLeave.GetString(objRecLeave.GetOrdinal("TypeOfLeave")))) = Trim(UCase("LWPHL")) Then
                                            If Trim(UCase(objRecLeave.GetString(objRecLeave.GetOrdinal("TypeOfLeave")))) = Trim(UCase("LWPHL")) Then
                                                varDateDiff = varDateDiff * 0.5
                                            End If
                                            varLeaveLWPCount = varLeaveLWPCount + varDateDiff
                                        Else
                                            If Trim(UCase(objRecLeave.GetString(objRecLeave.GetOrdinal("TypeOfLeave")))) = Trim(UCase("HL")) Or Trim(UCase(objRecLeave.GetString(objRecLeave.GetOrdinal("TypeOfLeave")))) = Trim(UCase("AHL")) Or Trim(UCase(objRecLeave.GetString(objRecLeave.GetOrdinal("TypeOfLeave")))) = Trim(UCase("CLHL")) Or Trim(UCase(objRecLeave.GetString(objRecLeave.GetOrdinal("TypeOfLeave")))) = Trim(UCase("ELHL")) Then
                                                varDateDiff = varDateDiff * 0.5
                                            End If
                                            varLeaveCount = varLeaveCount + varDateDiff
                                        End If

                                        varDBSDate = varTempDBSDate
                                        varDBEDate = varTempDBEDate

                                        varDateDiff = 0

                                    End While
                                End If
                                objRecLeave.Close()

                            End If


                        End If

                    End If
                Catch ex As Exception
                    Response.Write(ex.Message)
                Finally
                    clsLeave = Nothing
                    DSLeave.Dispose()
                    objRecLeave = Nothing
                End Try

                varDtStartDate = varDtTempStartDate
                varDtEndDate = varDtTempEndDate

                While varDtEndDate >= varDtStartDate
                    If varDutyRosterWOff.ToString.IndexOf(varDtStartDate.ToShortDateString) >= 0 Then

                        'Dim objWeeklyOffCheck As New Data.SqlClient.SqlCommand("SELECT * FROM DBO.tblLeave WHERE UserID='" & varArrUserID(varIntI) & "' AND Status='Approved' AND '" & varDtStartDate & "' BETWEEN StartDate AND EndDate AND TypeOfLeave <>'LWP' AND TypeOfLeave<>'LWPHL' AND TypeOFLeave<>'HL' AND TypeOfLeave<>'AHL' AND TypeOfLeave<>'ELHL' AND TypeOfLeave<>'CLHL' AND TypeOfLeave IS NOT NULL ", objConn)
                        'Dim objRecWeeklyOffCheck As Data.SqlClient.SqlDataReader = objWeeklyOffCheck.ExecuteReader
                        'If objRecWeeklyOffCheck.HasRows Then
                        '    varWeeklyOffCount = varWeeklyOffCount - 1
                        'End If
                        'objRecWeeklyOffCheck.Close()
                        'objRecWeeklyOffCheck = Nothing
                        'objWeeklyOffCheck = Nothing

                        Dim clsLeaveWCheck As ETS.BL.Leave
                        Dim DSWCheck As New Data.DataSet
                        Dim DVCheck As New Data.DataView
                        Dim objWeeklyOffCheck As Data.DataTableReader
                        Try
                            clsLeaveWCheck = New ETS.BL.Leave
                            DSWCheck = clsLeave.getLeaveListByUsr(varArrUserID(varIntI))
                            If DSWCheck.Tables.Count > 0 Then
                                If DSWCheck.Tables(0).Rows.Count > 0 Then
                                    DVCheck = New Data.DataView(DSWCheck.Tables(0), " Status='Approved' AND '" & varDtStartDate & "' BETWEEN StartDate AND EndDate AND TypeOfLeave <>'LWP' AND TypeOfLeave<>'LWPHL' AND TypeOFLeave<>'HL' AND TypeOfLeave<>'AHL' AND TypeOfLeave<>'ELHL' AND TypeOfLeave<>'CLHL' AND TypeOfLeave IS NOT NULL  ", "", Data.DataViewRowState.CurrentRows)
                                    If DVCheck.Count > 0 Then
                                        objWeeklyOffCheck = DVCheck.ToTable.CreateDataReader
                                        If objWeeklyOffCheck.HasRows Then
                                            varWeeklyOffCount = varWeeklyOffCount - 1
                                        End If
                                    End If
                                End If
                            End If
                        Catch ex As Exception
                            'Response.Write(ex.Message)
                        Finally
                            clsLeaveWCheck = Nothing
                            DSWCheck = Nothing
                        End Try
                    End If
                    varDtStartDate = DateAdd(DateInterval.Day, 1, varDtStartDate)
                End While



                Dim clsLeaveMain As ETS.BL.Leave
                Dim DSLeaveMain As New Data.DataSet
                Dim objRecWeeklyOffCheck As Data.DataTableReader
                Dim objRecWNext As Data.DataTableReader
                Dim objRecPrev As Data.DataTableReader
                Try
                    clsLeaveMain = New ETS.BL.Leave
                    DSLeaveMain = clsLeaveMain.getLeaveListByUsr(varArrUserID(varIntI))

                    'If varWOff1 <> "" Then
                    '    While varDtEndDate >= varDtStartDate
                    '        If Trim(UCase(WeekdayName(Weekday(varDtStartDate)))) = Trim(UCase(varWOff1)) Then
                    '            objRecWeeklyOffCheck = New Data.DataView(DSLeaveMain.Tables(0), "Status='Approved' AND StartDate >= '" & varDtStartDate.ToShortDateString.ToString & "' AND EndDate <= '" & varDtStartDate.ToShortDateString.ToString & "' AND TypeOfLeave <>'LWP' AND TypeOfLeave<>'LWPHL' AND TypeOFLeave<>'HL' AND TypeOfLeave<>'AHL' AND TypeOfLeave<>'ELHL' AND TypeOfLeave<>'CLHL' AND TypeOfLeave IS NOT NULL", String.Empty, Data.DataViewRowState.CurrentRows).ToTable.CreateDataReader
                    '            If objRecWeeklyOffCheck.HasRows Then
                    '                varWeeklyOffCount = varWeeklyOffCount - 1
                    '            End If
                    '            objRecWeeklyOffCheck.Close()
                    '        End If
                    '        varDtStartDate = DateAdd(DateInterval.Day, 1, varDtStartDate)
                    '    End While
                    'End If

                    'varDtStartDate = varDtTempStartDate
                    'varDtEndDate = varDtTempEndDate

                    'If varWOff2 <> "" Then
                    '    While varDtEndDate >= varDtStartDate
                    '        If Trim(UCase(WeekdayName(Weekday(varDtStartDate)))) = Trim(UCase(varWOff2)) Then
                    '            objRecWeeklyOffCheck = New Data.DataView(DSLeaveMain.Tables(0), "Status='Approved' AND StartDate >= '" & varDtStartDate.ToShortDateString.ToString & "' AND EndDate <= '" & varDtStartDate.ToShortDateString.ToString & "' AND TypeOfLeave <>'LWP' AND TypeOfLeave<>'LWPHL' AND TypeOFLeave<>'HL' AND TypeOfLeave<>'CLHL' AND TypeOfLeave<>'ELHL' AND TypeOfLeave<>'AHL' AND TypeOfLeave IS NOT NULL", String.Empty, Data.DataViewRowState.CurrentRows).ToTable.CreateDataReader
                    '            If objRecWeeklyOffCheck.HasRows Then
                    '                varWeeklyOffCount = varWeeklyOffCount - 1
                    '            End If
                    '            objRecWeeklyOffCheck.Close()
                    '        End If
                    '        varDtStartDate = DateAdd(DateInterval.Day, 1, varDtStartDate)
                    '    End While
                    'End If

                    varDtStartDate = varDtTempStartDate
                    varDtEndDate = varDtTempEndDate

                    While varDtEndDate >= varDtStartDate
                        Dim varBolNext As Boolean
                        If Trim(UCase(WeekdayName(Weekday(varDtStartDate)))) = Trim(UCase(varWOff1)) Or Trim(UCase(WeekdayName(Weekday(varDtStartDate)))) = Trim(UCase(varWOff2)) Then
                            Dim varObjectTemp As Object
                            varObjectTemp = varDtStartDate

                            If varWeeklyOffSearch.BinarySearch(varObjectTemp) > 0 Then
                                Dim varTempDate As Date
                                varTempDate = DateAdd(DateInterval.Day, varIntNext, varDtStartDate)
                                objRecWNext = New Data.DataView(DSLeaveMain.Tables(0), "Status='Approved' AND StartDate >= '" & varTempDate.ToShortDateString.ToString & "' AND EndDate <= '" & varTempDate.ToShortDateString.ToString & "' AND TypeOfLeave<>'LWP' AND TypeOfLeave<>'LWPHL' AND TypeOfLeave <>'HL' AND TypeOfLeave<>'AHL' AND TypeOfLeave<>'ELHL' AND TypeOfLeave<>'CLHL' AND TypeOfLeave IS NOT NULL AND (IsDeleted = 0 or IsDeleted IS NULL)", String.Empty, Data.DataViewRowState.CurrentRows).ToTable.CreateDataReader
                                If objRecWNext.HasRows Then
                                    varBolNext = True
                                End If
                                objRecWNext.Close()

                                If varBolNext Then
                                    Dim varTempDate1 As Date
                                    varTempDate1 = DateAdd(DateInterval.Day, varIntPrev, varDtStartDate)
                                    objRecPrev = New Data.DataView(DSLeaveMain.Tables(0), " Status='Approved' AND StartDate >= '" & varTempDate1.ToShortDateString.ToString & "' AND EndDate <= '" & varTempDate1.ToShortDateString.ToString & "' AND TypeOfLeave<>'LWP' AND TypeOfLeave<>'LWPHL' AND TypeOfLeave <>'HL' AND TypeOfLeave<>'ELHL' AND TypeOfLeave<>'CLHL' AND TypeOfLeave<>'AHL' AND TypeOfLeave IS NOT NULL AND (IsDeleted = 0 or IsDeleted IS NULL) ", String.Empty, Data.DataViewRowState.CurrentRows).ToTable.CreateDataReader
                                    If objRecPrev.HasRows Then
                                        varWeeklyOffDeductCount = varWeeklyOffDeductCount + -(varIntPrev)
                                    End If
                                    objRecPrev.Close()
                                End If
                            End If
                        End If
                        varBolNext = False
                        varDtStartDate = DateAdd(DateInterval.Day, 1, varDtStartDate)
                    End While

                Catch ex As Exception
                    'Response.Write(ex.Message)
                Finally
                    clsLeaveMain = Nothing
                    DSLeaveMain = Nothing
                    objRecWeeklyOffCheck = Nothing
                    objRecWNext = Nothing
                    objRecPrev = Nothing
                End Try




                varDtStartDate = varDtTempStartDate
                varDtEndDate = varDtTempEndDate

                If varLeaveCount > 0 Or varLeaveLWPCount > 0 Then
                    If varWeeklyOffDeductCount > 0 Then
                        If varLeaveCount > 0 Then
                            varLeaveCount = varLeaveCount + varWeeklyOffDeductCount
                        ElseIf varLeaveLWPCount > 0 Then
                            varLeaveLWPCount = varLeaveLWPCount + varWeeklyOffDeductCount
                        End If
                        varWeeklyOffCount = varWeeklyOffCount - varWeeklyOffDeductCount
                    End If
                End If
                varWOff1 = ""
                varWOff2 = ""
                varStrPost = ""

                varTblCellLeaveSanction.Text = varLeaveCount
                varTblCellLeaveWithoutPay.Text = varLeaveLWPCount
                varTblCellWeeklyOff.Text = varWeeklyOffCount
                varTblRow.Cells.Add(varTblCellLeaveSanction)
                varTblRow.Cells.Add(varTblCellLeaveWithoutPay)
                varTblRow.Cells.Add(varTblCellWeeklyOff)
                Table2.Rows.Add(varTblRow)
            Next
        Catch ex As Exception
            'Response.Write(ex.Message)
        Finally
            clsUsr = Nothing
            DSUsrs = Nothing
            DV = Nothing
        End Try
    End Sub
    Protected Sub btnExport_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnExport.Click
        Try
            Response.Clear()
            ' Set the content type to Excel.
            Dim filename = "Leave Sanctioned Report.xls"
            Response.AddHeader("content-disposition", "attachment;filename=" & filename)
            Response.ContentType = "application/vnd.ms-excel"
            ' Remove the charset from the Content-Type header.
            Response.Charset = ""
            ' Turn off the view state.
            Me.EnableViewState = False
            filltable()
            Dim tw As New System.IO.StringWriter()
            Dim hw As New System.Web.UI.HtmlTextWriter(tw)

            ' Get the HTML for the control.
            Table2.RenderControl(hw)

            ' Write the HTML back to the browser.
            Response.Write(tw.ToString())
            ' End the response.
            Response.End()
        Catch ex As Exception
        End Try
    End Sub
    Public Overloads Overrides Sub VerifyRenderingInServerForm(ByVal control As Control)
        Exit Sub
    End Sub
End Class
