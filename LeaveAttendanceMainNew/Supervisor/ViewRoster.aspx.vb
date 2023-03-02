Imports MainModule
Partial Class LeaveAttendanceMainNew_Supervisor_ViewRoster
    Inherits BasePage
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Request.QueryString("Admin") <> "True" Then
            LinkImport.Visible = True
        End If

        If Not Page.IsPostBack Then

            Dim varLstItem As New ListItem
            varLstItem.Text = "Please Select"
            varLstItem.Value = ""

            DropDownMonth.Items.Insert(0, varLstItem)
            DropDownYear.Items.Insert(0, varLstItem)

            If Request.QueryString("Admin") = "True" Then
                DropDownDept.Items.Clear()
                Dim clsDept As ETS.BL.Department
                Dim DS As New Data.DataSet
                Dim DV As Data.DataView
                Try
                    clsDept = New ETS.BL.Department
                    clsDept.ContractorID = Session("ContractorID")
                    DS = clsDept.GetDepartmentLstByWrkGroupID(Session("ContractorID"), Session("WorkGroupID"), String.Empty)


                    If DS.Tables.Count > 0 Then
                        If DS.Tables(0).Rows.Count > 0 Then
                            DV = New Data.DataView(DS.Tables(0), "(Deleted IS NULL OR Deleted='false')", String.Empty, Data.DataViewRowState.CurrentRows)
                            If DV.ToTable().Rows.Count > 0 Then
                                DropDownDept.DataSource = DV
                                DropDownDept.DataValueField = "DepartmentID"
                                DropDownDept.DataTextField = "Name"
                                DropDownDept.DataBind()
                            End If
                        End If
                    End If
                Catch ex As Exception
                    Response.Write(ex.Message)
                Finally
                    clsDept = Nothing
                    DS.Dispose()
                    DV.Dispose()
                End Try
                Dim varLst As New ListItem
                varLst.Text = "Any"
                varLst.Value = ""
                DropDownDept.Items.Insert(0, varLst)
                tblDept.Visible = True
            End If

            For i As Long = Year(Now()) - 3 To Year(Now()) + 3
                Dim varLstYear As New ListItem
                varLstYear.Text = i
                varLstYear.Value = i
                DropDownYear.Items.Add(varLstYear)
            Next

            Table1.Visible = False
            Table3.Visible = False
        End If
    End Sub
    Protected Sub GetData(ByVal flg As Boolean)
        Dim varMonth, varYear
        Dim varDeptId As String = String.Empty
        Dim varSundayOff(31)
        Dim varSaturdayOff(31)
        Dim varAttendanceStatus(31)
        'Dim varArrDay()

        Dim DS As New Data.DataSet
        Dim DSUsrs As New Data.DataSet
        Dim DV As Data.DataView
        Dim DVSupervisor As Data.DataView
        Dim clsDS As ETS.BL.DeptSuperVisor
        Dim clsDR As ETS.BL.DutyRoster


        '        Try
        If Request.QueryString("Admin") = "True" Then
            varDeptId = DropDownDept.Items(DropDownDept.SelectedIndex).Value.ToString
        End If

        varMonth = DropDownMonth.Items(DropDownMonth.SelectedIndex).Value
        varYear = DropDownYear.Items(DropDownYear.SelectedIndex).Value

        'Calculate the number of days in month
        Dim vardtTempDate As Date
        Dim varIntMonthDay, varDaysInMonth

        vardtTempDate = DateAdd("m", 1, DateSerial(varYear, varMonth, 1))
        varIntMonthDay = Day(DateAdd("d", -1, vardtTempDate))
        varDaysInMonth = varIntMonthDay

        'Calculate the weekly off in particular month
        Dim varDtStartDate As Date, varDtTDate As Date, varDtEndDate As Date

        varDtStartDate = DateSerial(varYear, varMonth, 1)
        varDtTDate = DateAdd("m", 1, varDtStartDate)
        varDtEndDate = DateAdd("d", -1, varDtTDate)

        Dim varTempDtStartDate, varTempDtEndDate
        varTempDtStartDate = varDtStartDate
        varTempDtEndDate = varDtEndDate

        While varDtEndDate >= varDtStartDate
            If Trim(UCase("Sunday")) = Trim(UCase(WeekdayName(Weekday(varDtStartDate)))) Then
                varSundayOff(Day(varDtStartDate)) = Day(varDtStartDate)

            ElseIf Trim(UCase("Saturday")) = Trim(UCase(WeekdayName(Weekday(varDtStartDate)))) Then
                varSaturdayOff(Day(varDtStartDate)) = Day(varDtStartDate)
            End If

            varDtStartDate = DateAdd(DateInterval.Day, 1, varDtStartDate)

        End While
        Dim vartblRow As New TableRow

        Dim cellName As New TableCell
        cellName.Text = "Employee Name"
        cellName.CssClass = "alt1"
        vartblRow.Cells.Add(cellName)

        If Request.QueryString("Admin") = "True" Then
            Dim cellDept As New TableCell
            cellDept.Text = "Department"
            cellDept.CssClass = "alt1"
            vartblRow.Cells.Add(cellDept)
        End If


        For i As Integer = 1 To varDaysInMonth
            Dim cell As New TableCell
            cell.Width = 30
            cell.Text = i
            If i = varSundayOff(i) Then
                cell.ForeColor = Drawing.Color.Red
            ElseIf i = varSaturdayOff(i) Then
                cell.ForeColor = Drawing.Color.Green
            End If
            cell.CssClass = "alt1"
            vartblRow.Cells.Add(cell)
        Next
        Table1.Rows.Add(vartblRow)

        clsDS = New ETS.BL.DeptSuperVisor
        clsDS.SuperVisorID = Session("UserID").ToString
        DS = clsDS.getDepSuperVisorsList()
        Dim varTemp As String = String.Empty
        Dim varStrSuper As String = String.Empty
        Dim varUserDeptID As String = String.Empty
        varUserDeptID = GetDeptID()


        If DS.Tables.Count > 0 Then
            If DS.Tables(0).Rows.Count > 0 Then
                DV = New Data.DataView(DS.Tables(0)) ', "SuperVisorID='" & Session("UserID") & "' AND LevelNo > 1", String.Empty, Data.DataViewRowState.CurrentRows)
                For Each drv As Data.DataRowView In DV
                    Dim iguid As New Guid(drv("DepartmentID").ToString)
                    'Response.Write(iguid.ToString)
                    'Response.Write("- " & drv("DepartmentID").ToString & "<BR>")
                    If String.IsNullOrEmpty(varTemp.ToString) Then
                        varTemp = "'" & iguid.ToString & "'"
                    Else
                        varTemp = varTemp & ",'" & iguid.ToString & "'"
                    End If
                    DVSupervisor = New Data.DataView(DS.Tables(0), "DepartmentID='" & drv("DepartmentID").ToString & "' AND LevelNo <" & drv("LevelNo") & "", String.Empty, Data.DataViewRowState.CurrentRows)
                    If DVSupervisor.Table().Rows.Count > 0 Then
                        For Each drvs As Data.DataRowView In DVSupervisor
                            If String.IsNullOrEmpty(varStrSuper) Then
                                varStrSuper = "'" & drvs("SupervisorID").ToString & "'"
                            Else
                                varStrSuper = varStrSuper & ",'" & drvs("SupervisorID").ToString & "'"
                            End If
                        Next
                    End If
                Next
            End If
        End If

        'Response.Write("varStrSuper: " & varStrSuper.ToString)

        If Not String.IsNullOrEmpty(varTemp) Then
            varUserDeptID = "'" & varUserDeptID.ToString & "'," & varTemp.ToString
        Else
            varUserDeptID = "'" & varUserDeptID.ToString & "'"
        End If

        'If Not String.IsNullOrEmpty(varStrSuper.ToString) Then
        '    varStrSuper = varStrSuper.Substring(1, varStrSuper.Length - 2)
        'End If
        'If Not String.IsNullOrEmpty(varUserDeptID.ToString) Then
        '    varUserDeptID = varUserDeptID.Substring(0, varUserDeptID.Length - 1)
        'End If


        'varUserDeptID = varTemp.ToString
        'If Not String.IsNullOrEmpty(varUserDeptID.ToString) Then
        '    varUserDeptID = " AND DepartmentID IN (" & varUserDeptID.ToString & ") "
        'End If
        'Response.Write("UserID : " & Session("UserID").ToString & "<BR>")
        'Response.Write("Con : " & Session("ContractorID").ToString & "<BR>")
        'Response.Write(varUserDeptID.ToString)
        'Response.Write("<BR>Ids: " & varStrSuper.ToString)
        Dim varArrUserID As New ArrayList
        Dim varArrEmpName As New ArrayList
        Dim varArrDept As New ArrayList
        Dim DutyRecFound As Boolean = False
        clsDR = New ETS.BL.DutyRoster

        If Request.QueryString("Admin") = "True" Then
            DSUsrs = clsDR.GetDutyRosterUsrsForAdmin(True, Session("UserID"), DropDownDept.Items(DropDownDept.SelectedIndex).Value.ToString, Session("ContractorID").ToString, String.Empty, varUserDeptID.ToString)
        Else
            'Response.Write(Session("UserID").ToString & "<BR>" & String.Empty & "<BR>" & Session("ContractorID").ToString & "<BR>" & Trim(varStrSuper.ToString) & "<BR>" & Trim(varUserDeptID.ToString))
            DSUsrs = clsDR.GetDutyRosterUsrsForAdmin(False, Session("UserID").ToString, String.Empty, Session("ContractorID").ToString, Trim(varStrSuper.ToString), Trim(varUserDeptID.ToString))
        End If
        'Response.Write("Count : " & DSUsrs.Tables(0).Rows.Count)

        If DSUsrs.Tables.Count > 0 Then
            If DSUsrs.Tables(0).Rows.Count > 0 Then
                Dim oRecGroup As Data.DataTableReader
                Try
                    oRecGroup = DSUsrs.Tables(0).CreateDataReader
                    If oRecGroup.HasRows Then
                        While oRecGroup.Read
                            If Not oRecGroup.IsDBNull(oRecGroup.GetOrdinal("UserID")) Then
                                varArrUserID.Add(oRecGroup.GetGuid(oRecGroup.GetOrdinal("UserID")).ToString)
                            End If
                            If Not oRecGroup.IsDBNull(oRecGroup.GetOrdinal("EmpName")) Then
                                varArrEmpName.Add(oRecGroup.GetString(oRecGroup.GetOrdinal("EmpName")).ToString)
                            End If

                            If Request.QueryString("Admin") = "True" Then
                                If Not oRecGroup.IsDBNull(oRecGroup.GetOrdinal("Name")) Then
                                    varArrDept.Add(oRecGroup.GetString(oRecGroup.GetOrdinal("Name")))
                                End If
                            End If
                        End While
                    End If
                    oRecGroup.Close()
                Catch ex As Exception
                Finally
                    oRecGroup = Nothing
                End Try

                For empCount As Integer = 0 To varArrUserID.Count - 1
                    Dim varArrShift(varDaysInMonth)
                    Dim varArrShiftDay(varDaysInMonth)

                    For t As Integer = 1 To varDaysInMonth
                        varArrShiftDay(t) = DateSerial(varYear, varMonth, t).ToShortDateString
                        If t = varSundayOff(t) Then
                            varArrShift(t) = "O"
                        Else
                            varArrShift(t) = "I"
                        End If
                    Next

                    Dim varTblRowEmp As New TableRow
                    Dim varTblCellName As New TableCell
                    varTblCellName.Text = varArrEmpName(empCount)

                    varTblRowEmp.Cells.Add(varTblCellName)

                    If Request.QueryString("Admin") = "True" Then
                        Dim varTblCellDept As New TableCell
                        varTblCellDept.Text = varArrDept(empCount)
                        varTblRowEmp.Cells.Add(varTblCellDept)
                    End If

                    Table1.Rows.Add(varTblRowEmp)
                    Dim oRecShift As Data.DataTableReader
                    Dim DSTemp As New Data.DataSet
                    DutyRecFound = False
                    Try
                        DSTemp = clsDR.GetDutyRosterRecordsForMonthByUsrID(varArrUserID(empCount), varMonth, varYear)

                        If DSTemp.Tables.Count > 0 Then
                            If DSTemp.Tables(0).Rows.Count > 0 Then
                                DutyRecFound = True
                                oRecShift = DSTemp.Tables(0).CreateDataReader
                                If oRecShift.HasRows Then
                                    For t As Integer = 1 To varDaysInMonth
                                        varArrShift(t) = "NA"
                                    Next
                                    While oRecShift.Read
                                        If Not oRecShift.IsDBNull(oRecShift.GetOrdinal("ShiftPrefix")) Then
                                            varArrShift(Day(oRecShift("DutyDate"))) = oRecShift.GetString(oRecShift.GetOrdinal("ShiftPrefix"))
                                        Else
                                            varArrShift(Day(oRecShift("DutyDate"))) = "NA"
                                        End If
                                        varArrShiftDay(Day(oRecShift("DutyDate"))) = oRecShift.GetDateTime(oRecShift.GetOrdinal("DutyDate")).ToShortDateString
                                    End While
                                End If
                                oRecShift.Close()
                            End If
                        End If
                    Catch ex As Exception
                    Finally
                        oRecShift = Nothing
                        DSTemp.Dispose()
                    End Try

                    For i As Integer = 1 To varDaysInMonth
                        Dim varTblCellShift As New TableCell
                        If i = varSundayOff(i) Then
                            If varArrShift(i) = "" Then
                                varArrShift(i) = "O"
                            End If
                            varTblCellShift.ForeColor = Drawing.Color.Red
                        ElseIf i = varSaturdayOff(i) Then
                            varTblCellShift.ForeColor = Drawing.Color.Green
                        End If
                        'If varArrDay(i) = varArrShiftDay(i) Then
                        If Request.QueryString("Admin") = "True" Or flg = True Then
                            varTblCellShift.Text = varArrShift(i)
                        Else
                            varTblCellShift.Text = "<a href="""" OnClick=""window.open('EditShift.aspx?UserID=" & varArrUserID(empCount).ToString & "&Dt=" & DateSerial(varYear, varMonth, i).ToShortDateString & "&SPre=" & varArrShift(i).ToString & "&EName=" & varArrEmpName(empCount) & "','', 'width=450,height=240,status=1,scrollbars=1');return false;"" >" & varArrShift(i).ToString & "</a>"
                        End If

                        'varTblCellShift.Text = "<a href="""" OnClick=""window.open('EditShift.aspx?UserID=" & varArrUserID(empCount).ToString & "&Dt=" & DateSerial(varYear, varMonth, i).ToShortDateString & "&SPre=" & varArrShift(i).ToString & "&EName=" & varArrEmpName(empCount) & "','', 'width=450,height=240,status=1,scrollbars=1');return false;"" >" & varArrShift(i).ToString & "</a>"
                        
                        'End If
                        If DutyRecFound = True Then
                            varTblCellShift.BackColor = Drawing.Color.LightBlue
                        End If
                        If varArrShift(i) = "NA" Then
                            varTblCellShift.BackColor = Drawing.Color.Red
                            varTblCellShift.ForeColor = Drawing.Color.Yellow
                        End If
                        varTblRowEmp.Cells.Add(varTblCellShift)
                    Next

                    
                    Table1.Rows.Add(varTblRowEmp)
                    Table1.Visible = True
                    Table3.Visible = True
                Next

            End If
        End If
        'Catch ex As Exception
        '    Response.Write(ex.Message)
        'Finally
        '    clsDR = Nothing
        '    clsDS = Nothing
        '    DS.Dispose()
        '    DSUsrs.Dispose()
        '    DV = Nothing
        '    DVSupervisor = Nothing
        'End Try
    End Sub
    Protected Function GetDeptID() As String
        'Session("UserID") = "76D1FBAD-499D-466D-A56E-AE22FB509C21"
        Dim varReturn As String = String.Empty
        Dim clsUsr As ETS.BL.Users
        Try
            clsUsr = New ETS.BL.Users(Session("UserID").ToString)
            varReturn = clsUsr.DepartmentID.ToString
        Catch ex As Exception
            Response.Write(ex.Message)
        Finally
            clsUsr = Nothing
        End Try
        Return varReturn
    End Function
    Protected Sub Submit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Submit.Click
        GetData(False)
    End Sub
    Protected Sub ES_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ES.Click
        Try
            Response.Clear()
            ' Set the content type to Excel.
            Response.ContentType = "application/vnd.ms-excel"
            Dim filename = "DutyRoster(" & MonthName(Month(Now)) & "-" & Year(Now) & ").xls"
            Response.AddHeader("content-disposition", "attachment;filename=" & filename)

            ' Remove the charset from the Content-Type header.
            Response.Charset = ""
            ' Turn off the view state.
            Me.EnableViewState = False
            GetData(True)
            Dim tw As New System.IO.StringWriter()
            Dim hw As New System.Web.UI.HtmlTextWriter(tw)

            ' Get the HTML for the control.
            Table1.RenderControl(hw)

            ' Write the HTML back to the browser.
            Response.Write(tw.ToString())
            ' End the response.
            Response.End()
        Catch ex As Exception
        End Try
    End Sub
End Class
