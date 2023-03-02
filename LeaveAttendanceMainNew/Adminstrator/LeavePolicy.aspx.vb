Imports MainModule
Partial Class LeavePolicy
    Inherits BasePage
    Dim objMainModule As New MainModule
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            Dim varTblRowAdd As New TableRow
            Dim varTblCellAdd As New TableCell

            Dim clsLP As ETS.BL.LeavePolicy
            Dim DS As Data.DataSet
            Dim objRecGetDate As Data.DataTableReader
            Try
                clsLP = New ETS.BL.LeavePolicy
                clsLP.ContractorID = Session("ContractorID").ToString
                DS = clsLP.getLeavePolicyList
                If DS.Tables.Count > 0 Then
                    If DS.Tables(0).Rows.Count > 0 Then
                        objRecGetDate = DS.Tables(0).CreateDataReader

                        If objRecGetDate.HasRows Then
                            While objRecGetDate.Read
                                Dim varTblRowEdit As New TableRow
                                Dim varTblCellDay As New TableCell
                                Dim varTblCellMonth As New TableCell
                                Dim varTblCellCL As New TableCell
                                Dim varTblCellEL As New TableCell
                                Dim varTblCellEdit As New TableCell
                                Dim varStrTrackID As String

                                If Not objRecGetDate.IsDBNull(objRecGetDate.GetOrdinal("TrackID")) Then
                                    varStrTrackID = objRecGetDate.GetGuid(objRecGetDate.GetOrdinal("TrackID")).ToString
                                End If
                                If Not objRecGetDate.IsDBNull(objRecGetDate.GetOrdinal("Day")) Then
                                    varTblCellDay.Text = objRecGetDate.GetInt32(objRecGetDate.GetOrdinal("Day"))
                                Else
                                    varTblCellDay.Text = "&nbsp"
                                End If
                                If Not objRecGetDate.IsDBNull(objRecGetDate.GetOrdinal("Month")) Then
                                    varTblCellMonth.Text = objRecGetDate.GetString(objRecGetDate.GetOrdinal("Month"))
                                Else
                                    varTblCellMonth.Text = "&nbsp"
                                End If
                                If Not objRecGetDate.IsDBNull(objRecGetDate.GetOrdinal("CL")) Then
                                    varTblCellCL.Text = objRecGetDate.GetDouble(objRecGetDate.GetOrdinal("CL"))
                                Else
                                    varTblCellCL.Text = "&nbsp"
                                End If
                                If Not objRecGetDate.IsDBNull(objRecGetDate.GetOrdinal("EL")) Then
                                    varTblCellEL.Text = objRecGetDate.GetDouble(objRecGetDate.GetOrdinal("EL"))
                                Else
                                    varTblCellEL.Text = "&nbsp"
                                End If

                                varTblCellEdit.Text = "<a href="""" OnClick=""window.open('LeavePolicyAddEdit.aspx?TrackID=" & varStrTrackID & "','', 'width=450,height=240,status=1,scrollbars=1');return false;"" >EDIT</a>"


                                varTblRowEdit.Font.Name = "Arial"
                                varTblRowEdit.Font.Size = 8
                                varTblRowEdit.Cells.Add(varTblCellDay)
                                varTblRowEdit.Cells.Add(varTblCellMonth)
                                varTblRowEdit.Cells.Add(varTblCellCL)
                                varTblRowEdit.Cells.Add(varTblCellEL)
                                varTblRowEdit.Cells.Add(varTblCellEdit)
                                tblLeavePolicy.Rows.Add(varTblRowEdit)
                            End While
                        End If
                        objRecGetDate.Close()
                    End If
                End If
                varTblCellAdd.ColumnSpan = 5
                varTblCellAdd.HorizontalAlign = HorizontalAlign.Right
                varTblCellAdd.Text = "<div style=""text-align:right""><a href="""" OnClick=""window.open('LeavePolicyAddEdit.aspx','', 'width=450,height=240,status=1,scrollbars=1');return false;"" >ADD</a></div>"
                varTblCellAdd.HorizontalAlign = HorizontalAlign.Right
                varTblRowAdd.Font.Size = 10
                varTblRowAdd.Cells.Add(varTblCellAdd)
                tblLeavePolicy.Rows.Add(varTblRowAdd)
            Catch ex As Exception
                Response.Write(ex.Message)
            Finally
                objRecGetDate = Nothing
                DS.Dispose()
                clsLP = Nothing
            End Try

        End If
    End Sub
End Class
