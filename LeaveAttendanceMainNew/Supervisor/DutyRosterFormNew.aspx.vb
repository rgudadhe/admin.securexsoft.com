Imports MainModule
Partial Class TechReports_DutyRosterFormNew
    Inherits BasePage
    Dim objMainModule As New MainModule
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim varLngYear As Long
        Dim varLstItem As New ListItem
        Dim i As Integer
        Dim j As Long
        varLstItem.Text = "Please Select"
        varLstItem.Value = ""
        lblStatus.Text = ""
        varLngYear = Year(Now())

        If Not Page.IsPostBack Then
            For i = 1 To 12
                Dim varLstMonthItem As New ListItem
                varLstMonthItem.Text = MonthName(i)
                varLstMonthItem.Value = i
                DropDownMonth.Items.Add(varLstMonthItem)
            Next
            For j = varLngYear - 3 To varLngYear + 3
                Dim varLstYearItem As New ListItem
                varLstYearItem.Text = j
                varLstYearItem.Value = j
                DropDownYear.Items.Add(varLstYearItem)
            Next
            DropDownMonth.Items.Insert(0, varLstItem)
            DropDownYear.Items.Insert(0, varLstItem)
        End If
    End Sub
    Protected Sub BtnSubmit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtnSubmit.Click
        GetShiftData()
    End Sub
    Protected Sub GetShiftData()
        Dim varStrEmpName As String
        Dim varIntI As Integer
        Dim varArrUserID As New ArrayList
        Dim varArrUserName As New ArrayList
        Dim varDtStartDate As Date
        Dim varDtEndDate As Date
        varIntI = 0
        RadioShift.Items.Clear()
        Dim objConn As New Data.SqlClient.SqlConnection
        objConn = objMainModule.OpenConnection(objConn)
        Try
            Dim objGetShift As New Data.SqlClient.SqlCommand("SELECT ShiftName,ShiftPrefix FROM DBO.tblShift WHERE IsDeleted IS NULL", objConn)
            Dim objRecGetShift As Data.SqlClient.SqlDataReader = objGetShift.ExecuteReader
            If objRecGetShift.HasRows Then
                While objRecGetShift.Read
                    Dim varTblCellShiftName As New TableCell
                    Dim varStrShiftName As String
                    Dim varStrShiftPrefix As String
                    If Not objRecGetShift.IsDBNull(objRecGetShift.GetOrdinal("ShiftName")) Then
                        varStrShiftName = objRecGetShift.GetString(objRecGetShift.GetOrdinal("ShiftName"))
                    End If
                    If Not objRecGetShift.IsDBNull(objRecGetShift.GetOrdinal("ShiftPrefix")) Then
                        varStrShiftPrefix = objRecGetShift.GetString(objRecGetShift.GetOrdinal("ShiftPrefix"))
                    End If

                    If varStrShiftPrefix <> "" And varStrShiftName <> "" Then
                        varTblCellShiftName.Text = objRecGetShift.GetString(objRecGetShift.GetOrdinal("ShiftName"))
                        tblRowShift.Cells.Add(varTblCellShiftName)
                        Dim varLstItem As New ListItem
                        varLstItem.Text = varStrShiftName
                        varLstItem.Value = varStrShiftPrefix
                        RadioShift.Items.Add(varLstItem)
                        varIntI = varIntI + 1
                    End If
                End While
            End If

            objRecGetShift.Close()
            objRecGetShift = Nothing
            objGetShift = Nothing
            tblMainResult.Rows(0).Cells(0).ColumnSpan = varIntI + 3
            tblMainResult.Rows(1).Cells(0).ColumnSpan = varIntI + 3

            Dim oCommandGroup As New Data.SqlClient.SqlCommand("SELECT UserID,FirstName,LastName FROM DBO.tblUsers U WHERE DepartmentID =(SELECT DepartmentID FROM DBO.tblUsers WHERE UserID='" & Session("UserID") & "') AND UserName LIKE 'e%' AND (U.ISDeleted IS NULL OR U.ISDeleted=0) AND (Active IS NULL OR Active=0) AND U.ContractorID='" & Session("ContractorID").ToString & "' ", objConn)
            Dim oRecGroup As Data.SqlClient.SqlDataReader = oCommandGroup.ExecuteReader()
            If oRecGroup.HasRows Then
                While oRecGroup.Read
                    If Not oRecGroup.IsDBNull(oRecGroup.GetOrdinal("FirstName")) Then
                        varStrEmpName = oRecGroup.GetString(oRecGroup.GetOrdinal("FirstName"))
                    End If
                    If Not oRecGroup.IsDBNull(oRecGroup.GetOrdinal("LastName")) Then
                        varStrEmpName = varStrEmpName & " " & oRecGroup.GetString(oRecGroup.GetOrdinal("LastName"))
                    End If
                    varArrUserID.Add(oRecGroup.GetGuid(oRecGroup.GetOrdinal("UserID")).ToString)
                    varArrUserName.Add(varStrEmpName)
                    varStrEmpName = ""
                End While
            End If
            oRecGroup.Close()
            oRecGroup = Nothing
            oCommandGroup = Nothing


            Dim j As Integer
            varDtStartDate = DropDownStartWeek.Items(DropDownStartWeek.SelectedIndex).Value.ToString
            varDtEndDate = DropDownEndWeek.Items(DropDownEndWeek.SelectedIndex).Value.ToString
            ViewState("EmpCount") = varArrUserID.Count
            For j = 0 To varArrUserID.Count - 1
                Dim varTblRowEmp As New TableRow
                Dim varTblCellEmpName As New TableCell
                Dim varTblCellChk As New TableCell
                Dim count As Integer
                varTblCellEmpName.Text = varArrUserName(j)
                Dim varChkBox As New CheckBox

                varChkBox.ID = "UserID" & j
                varChkBox.InputAttributes.Add("value", varArrUserID(j))
                varTblCellChk.Controls.Add(varChkBox)

                varTblRowEmp.Cells.Add(varTblCellChk)
                varTblRowEmp.Cells.Add(varTblCellEmpName)

                For count = 2 To tblMainResult.Rows(2).Cells.Count - 1
                    Dim varTblCellCount As New TableCell
                    varTblCellCount.Text = GetShiftCount(varArrUserID(j), varDtStartDate, varDtEndDate, tblMainResult.Rows(2).Cells(count).Text)
                    varTblRowEmp.Cells.Add(varTblCellCount)
                Next
                tblMainResult.Rows.Add(varTblRowEmp)
            Next
            tblMainResult.Visible = True
            BtnUpdateShift.Visible = True
        Catch ex As Exception
        Finally
            If objConn.State <> Data.ConnectionState.Closed Then
                objConn.Close()
                objConn = Nothing
            End If
        End Try
    End Sub
    Public Function GetShiftPrefix(ByVal Dt As Date) As String
        Dim varStrString As String
        Dim objConn As New Data.SqlClient.SqlConnection
        objConn = objMainModule.OpenConnection(objConn)
        Try
            Dim objCommandCheckShift As New Data.SqlClient.SqlCommand("SELECT Count(*) FROM DBO.tblDutyRoster D INNER JOIN DBO.tblShift S ON D.ShiftPrefix=S.ShiftPrefix WHERE UserID='" & Request.QueryString("UserID") & "' AND DutyDate='" & Dt & "'", objConn)
            Dim objRecCheckShift As Data.SqlClient.SqlDataReader = objCommandCheckShift.ExecuteReader()
            If objRecCheckShift.HasRows Then
                While objRecCheckShift.Read
                    varStrString = objRecCheckShift.GetDateTime(objRecCheckShift.GetOrdinal("DutyDate"))
                    varStrString = varStrString & "," & objRecCheckShift.GetString(objRecCheckShift.GetOrdinal("ShiftPrefix"))
                    varStrString = varStrString & "," & objRecCheckShift.GetString(objRecCheckShift.GetOrdinal("ShiftName"))
                End While
            Else
                varStrString = ""
            End If

            objRecCheckShift.Close()
            objRecCheckShift = Nothing
            objCommandCheckShift = Nothing
            Return varStrString
        Catch ex As Exception
        Finally
            If objConn.State <> Data.ConnectionState.Closed Then
                objConn.Close()
                objConn = Nothing
            End If
        End Try
    End Function
    Public Function GetShiftCount(ByVal UserID As String, ByVal StartDt As Date, ByVal EndDt As Date, ByVal ShiftName As String) As Integer
        Dim objConn As New Data.SqlClient.SqlConnection
        objConn = objMainModule.OpenConnection(objConn)
        Try
            Dim objGetCount As New Data.SqlClient.SqlCommand("SELECT Count(*) FROM DBO.tblDutyRoster WHERE DutyDate >= '" & StartDt & "' AND DutyDate <= '" & EndDt & "' AND UserID='" & UserID & "' AND ShiftPrefix = (SELECT ShiftPrefix FROM DBO.tblShift WHERE ShiftName='" & ShiftName & "')", objConn)
            Dim objRecCount As Data.SqlClient.SqlDataReader = objGetCount.ExecuteReader()
            If objRecCount.HasRows Then
                While objRecCount.Read
                    GetShiftCount = objRecCount(0)
                End While
            End If
            objRecCount.Close()
            objRecCount = Nothing
            objGetCount = Nothing
        Catch ex As Exception
        Finally
            If objConn.State <> Data.ConnectionState.Closed Then
                objConn.Close()
                objConn = Nothing
            End If
        End Try
    End Function
    Protected Sub Submit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Submit.Click
        Dim varIntMonth As Integer
        Dim varIntYear As Integer
        Dim varDtTempDate As Date
        Dim varDtStartDate As Date
        Dim varDtEndDate As Date
        Dim FlagFirst As Boolean
        Dim varIntWeek As Integer
        Dim varIntWeekEnd As Integer
        Dim WeekStart(6)
        Dim WeekEnd(6)
        Dim i, j


        Try
            varIntMonth = DropDownMonth.Items(DropDownMonth.SelectedIndex).Value.ToString
            varIntYear = DropDownYear.Items(DropDownYear.SelectedIndex).Value.ToString

            If varIntMonth > 0 And varIntYear > 0 Then
                varDtStartDate = DateSerial(varIntYear, varIntMonth, 1)
                varDtTempDate = DateAdd(DateInterval.Month, 1, varDtStartDate)
                varDtEndDate = DateAdd(DateInterval.Day, -1, varDtTempDate)


                If Trim(UCase(WeekdayName(Weekday(varDtStartDate)))) <> Trim(UCase("Monday")) Then
                    varIntWeek = -(Weekday(varDtStartDate, Microsoft.VisualBasic.FirstDayOfWeek.Monday) - 1)
                    varDtStartDate = DateAdd(DateInterval.Day, varIntWeek, varDtStartDate)
                End If

                If Trim(UCase(WeekdayName(Weekday(varDtEndDate)))) <> Trim(UCase("Sunday")) And Trim(UCase(WeekdayName(Weekday(varDtEndDate)))) <> Trim(UCase("Saturday")) Then
                    varIntWeekEnd = 6 - Weekday(varDtEndDate, Microsoft.VisualBasic.FirstDayOfWeek.Monday)
                    varDtEndDate = DateAdd(DateInterval.Day, varIntWeekEnd, varDtEndDate)
                End If

                DropDownStartWeek.Items.Clear()
                DropDownEndWeek.Items.Clear()
                While varDtEndDate >= varDtStartDate
                    If WeekdayName(Weekday(DateSerial(Year(varDtStartDate), Month(varDtStartDate), Day(varDtStartDate)))) = "Monday" Then
                        WeekStart(j) = DateSerial(Year(varDtStartDate), Month(varDtStartDate), Day(varDtStartDate))
                        If FlagFirst = False Then
                            FlagFirst = True
                        End If
                        DropDownStartWeek.Items.Add(WeekStart(j))
                    End If
                    If WeekdayName(Weekday(DateSerial(Year(varDtStartDate), Month(varDtStartDate), Day(varDtStartDate)))) = "Saturday" And FlagFirst = True Then
                        WeekEnd(j) = DateSerial(Year(varDtStartDate), Month(varDtStartDate), Day(varDtStartDate))
                        DropDownEndWeek.Items.Add(WeekEnd(j))
                    End If
                    varDtStartDate = DateAdd(DateInterval.Day, 1, varDtStartDate)
                End While
                BtnSubmit.Enabled = True
            End If
        Catch ex As Exception
        End Try
    End Sub
    Protected Sub BtnUpdateShift_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtnUpdateShift.Click
        Dim varStartDate As Date
        Dim varEndDate As Date
        Dim varTStartDate As Date
        Dim varTEndDate As Date
        Dim varCount As Integer
        Dim varStrShiftPrefix As String
        Dim varBolUpdate As Boolean
        Dim varSDateSplit() As String
        Dim varEDateSplit() As String
        Dim varCountMain As Integer
        Dim varUpdateEmp As Integer

        Dim objConn As New Data.SqlClient.SqlConnection
        objConn = objMainModule.OpenConnection(objConn)

        Try
            varSDateSplit = Split(Request.Form("DropDownStartWeek"), "/")
            varEDateSplit = Split(Request.Form("DropDownEndWeek"), "/")
            varStartDate = DateSerial(varSDateSplit(2), varSDateSplit(0), varSDateSplit(1))
            varEndDate = DateSerial(varEDateSplit(2), varEDateSplit(0), varEDateSplit(1))
            varTStartDate = varStartDate
            varTEndDate = varEndDate
            varStrShiftPrefix = Request.Form("RadioShift")
            varCountMain = ViewState("EmpCount")
            For varCount = 0 To varCountMain
                Dim varChkValue As String
                varChkValue = Request.Form("UserID" & varCount)
                varStartDate = varTStartDate
                varEndDate = varTEndDate
                If varChkValue <> "" Then
                    While varEndDate >= varStartDate
                        Dim varStrCommand As String
                        Dim objCommandShift As New Data.SqlClient.SqlCommand("SELECT ShiftPrefix FROM DBO.tblDutyRoster WHERE UserID='" & varChkValue & "' AND DutyDate='" & varStartDate & "'", objConn)
                        Dim objRecShift As Data.SqlClient.SqlDataReader = objCommandShift.ExecuteReader()
                        If objRecShift.HasRows Then
                            varBolUpdate = True
                        End If
                        objRecShift.Close()
                        objRecShift = Nothing
                        objCommandShift = Nothing

                        If varBolUpdate = True Then
                            varStrCommand = "UPDATE DBO.tblDutyRoster SET ShiftPrefix='" & varStrShiftPrefix & "',UpdateOn='" & Now() & "',UpdateBy='" & Session("UserID") & "' WHERE UserID='" & varChkValue & "' AND DutyDate='" & varStartDate & "'"
                        Else
                            varStrCommand = "INSERT INTO DBO.tblDutyRoster (UserID,DutyDate,ShiftPrefix,UpdateOn,UpdateBy) VALUES('" & varChkValue & "','" & varStartDate & "','" & varStrShiftPrefix & "','" & Now() & "','" & Session("UserID") & "')"
                        End If

                        Dim Cmd As New Data.SqlClient.SqlCommand
                        Cmd.CommandType = Data.CommandType.Text
                        Cmd.CommandText = varStrCommand
                        Cmd.Connection = objConn
                        Cmd.ExecuteNonQuery()
                        Cmd = Nothing

                        varBolUpdate = False
                        varStartDate = DateAdd(DateInterval.Day, 1, varStartDate)
                        varUpdateEmp = varUpdateEmp + 1
                    End While
                End If
                varChkValue = ""
            Next
            If varUpdateEmp > 0 Then
                'Response.Write("<script type=""text/javascript"" language=javascript> alert(""Shift Update sucessfully !!!"");</script>")
                lblStatus.Text = "Shift Update sucessfully !!!"
                GetShiftData()
            End If
        Catch ex As Exception
        Finally
            If objConn.State <> Data.ConnectionState.Closed Then
                objConn.Close()
                objConn = Nothing
            End If
        End Try
    End Sub
End Class
