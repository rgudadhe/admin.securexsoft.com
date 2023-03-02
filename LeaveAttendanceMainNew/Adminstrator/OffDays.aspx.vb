Imports MainModule
Imports System.Data
Partial Class OffDays
    Inherits BasePage
    Dim objMainModule As New MainModule
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            Dim clsOff As ETS.BL.NationalOffDays
            Dim DS As Data.DataSet
            Dim objRecGetDate As Data.DataTableReader
            Try
                clsOff = New ETS.BL.NationalOffDays
                clsOff.ContractorID = Session("ContractorID").ToString
                DS = clsOff.getOffDaysList
                If DS.Tables.Count > 0 Then
                    If DS.Tables(0).Rows.Count > 0 Then
                        objRecGetDate = DS.Tables(0).CreateDataReader
                        If objRecGetDate.HasRows Then
                            While objRecGetDate.Read
                                Dim varTblRowEdit As New TableRow
                                Dim varTblCellDate As New TableCell
                                Dim varTblCellDesc As New TableCell
                                Dim varTblCellEdit As New TableCell
                                Dim varStrTrackID As String = String.Empty

                                If Not objRecGetDate.IsDBNull(objRecGetDate.GetOrdinal("ID")) Then
                                    varStrTrackID = objRecGetDate.GetGuid(objRecGetDate.GetOrdinal("ID")).ToString
                                End If
                                If Not objRecGetDate.IsDBNull(objRecGetDate.GetOrdinal("OffDate")) Then
                                    varTblCellDate.Text = objRecGetDate.GetDateTime(objRecGetDate.GetOrdinal("OffDate")).ToShortDateString
                                Else
                                    varTblCellDate.Text = "&nbsp"
                                End If
                                If Not objRecGetDate.IsDBNull(objRecGetDate.GetOrdinal("Description")) Then
                                    varTblCellDesc.Text = objRecGetDate.GetString(objRecGetDate.GetOrdinal("Description"))
                                Else
                                    varTblCellDesc.Text = "&nbsp"
                                End If

                                varTblCellEdit.Text = "<a href="""" OnClick=""window.open('EditDayOff.aspx?ID=" & varStrTrackID & "','', 'width=450,height=240,status=1,scrollbars=1');return false;"" >EDIT</a>"


                                varTblRowEdit.Font.Name = "Arial"
                                varTblRowEdit.Font.Size = 8
                                varTblRowEdit.Cells.Add(varTblCellDate)
                                varTblRowEdit.Cells.Add(varTblCellDesc)
                                varTblRowEdit.Cells.Add(varTblCellEdit)
                                tblALL.Rows.Add(varTblRowEdit)
                            End While
                        End If

                    End If
                End If
            Catch ex As Exception
                Response.Write(ex.Message)
            Finally
                clsOff = Nothing
                DS.Dispose()
            End Try
            Dim varTblCellAdd As New TableCell
            Dim varTblRowAdd As New TableRow
            varTblCellAdd.ColumnSpan = 3
            varTblCellAdd.HorizontalAlign = HorizontalAlign.Right
            varTblCellAdd.Text = "<div style=""text-align:right""><a href="""" OnClick=""window.open('AddDayOff.aspx','', 'width=450,height=240,status=1,scrollbars=1');return false;"" >ADD</a></div>"
            varTblCellAdd.HorizontalAlign = HorizontalAlign.Right
            varTblRowAdd.Font.Size = 10
            varTblRowAdd.Cells.Add(varTblCellAdd)
            tblALL.Rows.Add(varTblRowAdd)
        End If
    End Sub
    'Protected Sub BtnSubmit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtnSubmit.Click
    'Dim varStrDeptID As String
    'varStrDeptID = Request.Form("DropDownGroup")

    'Dim objConn As New Data.SqlClient.SqlConnection
    'objConn = objMainModule.OpenConnection(objConn)

    'Try
    '    If Trim(UCase(varStrDeptID)) = Trim(UCase("ALL")) Then
    '        tblMain.Visible = False
    '        tblALL.Visible = True
    '        Dim varStrStateArray As New ArrayList
    '        Dim varStateCount As Integer

    '        Dim i As Integer
    '        varStrStateArray.Clear()
    '        varStateCount = 0
    '        Dim oCommand As New Data.SqlClient.SqlCommand("SELECT DISTINCT State FROM DBO.tblOffDays ", objConn)
    '        Dim oRec As Data.SqlClient.SqlDataReader = oCommand.ExecuteReader()
    '        If oRec.HasRows Then
    '            While oRec.Read
    '                varStrStateArray.Add(oRec.GetString(oRec.GetOrdinal("State")))
    '                varStateCount = varStateCount + 1
    '            End While
    '        End If
    '        oRec.Close()
    '        oRec = Nothing
    '        oCommand = Nothing

    '        Dim oCommandCountDept As New Data.SqlClient.SqlCommand("SELECT Count(*) FROM DBO.tblDepartments WHERE Deleted <> 'TRUE'", objConn)
    '        Dim oRecCountDept As Data.SqlClient.SqlDataReader = oCommandCountDept.ExecuteReader
    '        If oRecCountDept.HasRows Then
    '            While oRecCountDept.Read
    '                varStateCount = varStateCount * oRecCountDept(0)
    '            End While
    '        End If
    '        oRecCountDept.Close()
    '        oRecCountDept = Nothing
    '        oCommandCountDept = Nothing


    '        Dim varOffDateRemove As New ArrayList

    '        For i = 0 To varStrStateArray.Count - 1
    '            Dim varTrackID As New ArrayList
    '            Dim varOffDate As New ArrayList
    '            Dim varOffDateDesc As New ArrayList
    '            Dim varRemoveDate As String = String.Empty
    '            Dim varStrQuery As String

    '            If i = 0 Then
    '                'varRemoveDate = "()"
    '                varRemoveDate = "("
    '                varStrQuery = "SELECT ID,OffDate,Description FROM DBO.tblOffDays WHERE State='" & varStrStateArray(i) & "' AND OffDate <>''"
    '            Else
    '                'varRemoveDate = "("
    '                varStrQuery = "SELECT ID,OffDate,Description FROM DBO.tblOffDays WHERE State='" & varStrStateArray(i) & "' AND OffDate <>'' AND OffDate NOT IN " & varRemoveDate & ""
    '            End If

    '            varOffDate.Clear()


    '            Dim oCommandDate As New Data.SqlClient.SqlCommand(varStrQuery, objConn)
    '            Dim oRecDate As Data.SqlClient.SqlDataReader = oCommandDate.ExecuteReader()
    '            If oRecDate.HasRows Then
    '                While oRecDate.Read
    '                    varOffDate.Add(oRecDate.GetDateTime(oRecDate.GetOrdinal("OffDate")).ToShortDateString)
    '                    varOffDateDesc.Add(oRecDate.GetString(oRecDate.GetOrdinal("Description"))).ToString()
    '                    varTrackID.Add(oRecDate.GetGuid(oRecDate.GetOrdinal("ID")).ToString)
    '                End While
    '            End If
    '            oRecDate.Close()
    '            oRecDate = Nothing
    '            oCommandDate = Nothing
    '            Dim j As Integer
    '            Dim varDateCount As Integer
    '            Dim k As Integer
    '            varDateCount = 0
    '            If varOffDateRemove.Count > 0 Then
    '                For k = 0 To varOffDateRemove.Count - 1
    '                    If varOffDate(k) = varOffDateRemove(k) Then
    '                        varOffDate.Remove(varOffDate(k))
    '                    End If
    '                Next
    '            End If
    '            Dim varDateCounter As Integer
    '            varDateCounter = 0
    '            Dim varStrString As String
    '            For j = 0 To varOffDate.Count - 1
    '                Dim varRemoveFlag As String

    '                Dim varQuery As String

    '                If varStrString = "" Then
    '                    varQuery = "SELECT Count(OffDate) FROM DBO.tblOffDays WHERE OffDate ='" & varOffDate(j) & "' GROUP BY OffDate "
    '                Else
    '                    varQuery = "SELECT Count(OffDate) FROM DBO.tblOffDays WHERE OffDate ='" & varOffDate(j) & "' AND OffDate NOT IN ('" & varStrString & "') GROUP BY OffDate "
    '                End If
    '                'Response.Write(varQuery)
    '                Dim oCommandDateCount As New Data.SqlClient.SqlCommand(varQuery, objConn)
    '                Dim oRecDateCount As Data.SqlClient.SqlDataReader = oCommandDateCount.ExecuteReader()
    '                If oRecDateCount.HasRows Then
    '                    While oRecDateCount.Read
    '                        varDateCount = oRecDateCount(0)
    '                    End While
    '                End If
    '                oRecDateCount.Close()
    '                oRecDateCount = Nothing
    '                oCommandDateCount = Nothing
    '                'Response.Write("<BR>" & varDateCount & varStateCount)

    '                'If varDateCount = varStateCount Then
    '                If varDateCounter = 0 Then
    '                    varRemoveDate = varRemoveDate & "'" & varOffDate(j) & "'"
    '                Else
    '                    varRemoveDate = varRemoveDate & "," & "'" & varOffDate(j) & "'"
    '                End If
    '                'Response.Write(varRemoveDate)
    '                Dim varRowDate As New TableRow
    '                Dim varCellDate As New TableCell
    '                Dim varCellDesc As New TableCell
    '                Dim varCellEdit As New TableCell
    '                varRowDate.Font.Name = "Arial"
    '                varRowDate.Font.Size = 8
    '                varCellDate.Text = varOffDate(j).ToString
    '                varCellDesc.Text = varOffDateDesc(j).ToString
    '                varCellEdit.Text = "<a href="""" OnClick=""window.open('EditDayOff.aspx?ID=" & varTrackID(j) & "&Str=ALL&Dt=" & varOffDate(j) & "','', 'width=450,height=240,status=1,scrollbars=1');return false;"" >EDIT</a>"
    '                varRowDate.Cells.Add(varCellDate)
    '                varRowDate.Cells.Add(varCellDesc)
    '                varRowDate.Cells.Add(varCellEdit)
    '                tblALL.Rows.Add(varRowDate)
    '                varDateCounter = varDateCounter + 1
    '                varStrString = varOffDate(j)
    '                'End If
    '                varDateCount = 0
    '            Next
    '            varRemoveDate = varRemoveDate & ")"
    '            'Response.Write(varRemoveDate)

    '        Next
    '        Dim varRowAddDate As New TableRow
    '        Dim varCellAddDate As New TableCell
    '        varCellAddDate.ColumnSpan = 3
    '        varCellAddDate.HorizontalAlign = HorizontalAlign.Left
    '        varCellAddDate.Text = "<a href="""" OnClick=""window.open('AddDayOff.aspx?Str=ALL','', 'width=450,height=240,status=1,scrollbars=1');return false;"" >ADD</a>"
    '        varRowAddDate.Cells.Add(varCellAddDate)
    '        tblALL.Rows.Add(varRowAddDate)
    '    Else
    '        tblALL.Visible = False
    '        Dim varStrStateArray As New ArrayList
    '        Dim i As Integer

    '        Dim oCommand As New Data.SqlClient.SqlCommand("SELECT DISTINCT State FROM DBO.tblOffDays WHERE Department='" & varStrDeptID & "'", objConn)
    '        Dim oRec As Data.SqlClient.SqlDataReader = oCommand.ExecuteReader()
    '        If oRec.HasRows Then
    '            While oRec.Read
    '                varStrStateArray.Add(oRec.GetString(oRec.GetOrdinal("State")))
    '            End While
    '        End If
    '        oRec.Close()
    '        oRec = Nothing
    '        oCommand = Nothing

    '        For i = 0 To varStrStateArray.Count - 1
    '            Dim varRowState As New TableRow
    '            Dim varCellState As New TableCell
    '            Dim varCellOffDays As New TableCell
    '            Dim varStrTempTable As String
    '            Dim varStrAjaxString As String
    '            'varStrAjaxString = "<INPUT TYPE=TEXT ID=txtDate runat=server><input id=Button1  type=button value=button /> &nbsp &nbsp<ajaxToolkit:CalendarExtender ID=CalendarExtender1 runat=server Format=MM/dd/yyyy TargetControlID=txtDate PopupButtonID=Button1 BehaviorID=CalendarExtender1 Enabled=True></ajaxToolkit:CalendarExtender>"

    '            Dim varDateStr As String
    '            varDateStr = "<INPUT TYPE=TEXT ID=txtDate runat=server>"

    '            Dim varDescStr As String
    '            varDescStr = "<INPUT TYPE=TEXT ID=txtDesc runat=server>"

    '            Dim varADDStr As String
    '            'varADDStr = "<input id=btnAdd type=button value=ADD />"
    '            'varADDStr = "<a href=""AddDayOff.aspx?DeptID=" & varStrDeptID & "&State=" & varStrStateArray(i) & """>ADD</a>"
    '            varADDStr = "<a href="""" OnClick=""window.open('AddDayOff.aspx?DeptID=" & varStrDeptID & "&State=" & varStrStateArray(i) & "','', 'width=450,height=240,status=1,scrollbars=1');return false;"" >ADD</a>"
    '            Dim varCtrlTextboxDate As New TextBox
    '            varCtrlTextboxDate.ID = "txtDate"
    '            varCtrlTextboxDate.Font.Name = "Arial"

    '            Dim varCtrlTextboxDesc As New TextBox
    '            varCtrlTextboxDesc.ID = "txtDesc"
    '            varCtrlTextboxDesc.Font.Name = "Arial"

    '            varCellState.Text = varStrStateArray(i).ToString
    '            varStrTempTable = "<BR><TABLE border=1 width=90%><TR><TD align=center class=alt1>Date</TD><TD align=center class=alt1>Description</TD><TD style=width:300>&nbsp</TD></TR>"

    '            Dim oCommandDate As New Data.SqlClient.SqlCommand("SELECT ID,OffDate,Description FROM DBO.tblOffDays WHERE Department='" & varStrDeptID & "' AND State='" & varStrStateArray(i) & "' AND OffDate <>''", objConn)
    '            Dim oRecDate As Data.SqlClient.SqlDataReader = oCommandDate.ExecuteReader()
    '            If oRecDate.HasRows Then
    '                While oRecDate.Read
    '                    varStrTempTable = varStrTempTable & "<TR><TD>" & oRecDate.GetDateTime(oRecDate.GetOrdinal("OffDate")).ToShortDateString & "</TD><TD align=left>" & oRecDate.GetString(oRecDate.GetOrdinal("Description")) & "</TD><TD><a href="""" OnClick=""window.open('EditDayOff.aspx?ID=" & oRecDate.GetGuid(oRecDate.GetOrdinal("ID")).ToString & "','', 'width=450,height=240,status=1,scrollbars=1');return false;"" >EDIT</a></TD></TR>"
    '                End While
    '            End If
    '            oRecDate.Close()
    '            oRecDate = Nothing
    '            oCommandDate = Nothing

    '            varStrTempTable = varStrTempTable & "<TR><TD colspan=3 align=right>" & varADDStr & "</TD></TR></TABLE><BR>"
    '            varCellOffDays.Text = varStrTempTable

    '            varRowState.Cells.Add(varCellState)
    '            varRowState.Cells.Add(varCellOffDays)
    '            varRowState.Font.Name = "Trebuchet MS"
    '            varRowState.Font.Size = 10
    '            tblMain.Rows.Add(varRowState)


    '        Next
    '        Dim varRowAddState As New TableRow
    '        Dim varCellAddState As New TableCell
    '        varCellAddState.ColumnSpan = 2
    '        varCellAddState.HorizontalAlign = HorizontalAlign.Left
    '        varCellAddState.Text = "<a href="""" OnClick=""window.open('AddState.aspx?DeptID=" & varStrDeptID & "','', 'width=450,height=240,status=1,scrollbars=1');return false;"" >ADD</a>"
    '        varRowAddState.Cells.Add(varCellAddState)
    '        tblMain.Rows.Add(varRowAddState)
    '        tblMain.Visible = True
    '    End If
    'Catch ex As Exception
    '    'Response.Write(ex.Message)
    'Finally
    '    If objConn.State <> ConnectionState.Closed Then
    '        objConn.Close()
    '        objConn = Nothing
    '    End If
    'End Try
    'End Sub
End Class
