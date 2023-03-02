Imports MainModule
Partial Class LeaveBalance
    Inherits BasePage
    Dim objMainModule As New MainModule
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Not Page.IsPostBack Then
            DropDownGroup.Items.Clear()
            Dim clsDept As ETS.BL.Department
            Dim DS As New Data.DataSet
            Dim DV As Data.DataView
            Try
                clsDept = New ETS.BL.Department
                clsDept.ContractorID = Session("ContractorID")
                DS = clsDept.getDepartmentList()


                If DS.Tables.Count > 0 Then
                    If DS.Tables(0).Rows.Count > 0 Then
                        DV = New Data.DataView(DS.Tables(0), "(Deleted IS NULL OR Deleted='false')", String.Empty, Data.DataViewRowState.CurrentRows)
                        If DV.ToTable().Rows.Count > 0 Then
                            DropDownGroup.DataSource = DV
                            DropDownGroup.DataValueField = "DepartmentID"
                            DropDownGroup.DataTextField = "Name"
                            DropDownGroup.DataBind()
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
            varLst.Text = "ALL"
            varLst.Value = "ALL"
            DropDownGroup.Items.Insert(0, varLst)
            FillData()
        End If
    End Sub
    Protected Sub BtnSubmit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtnSubmit.Click
        FillData()
    End Sub
    Protected Sub FillData()
        Dim varStrEmpName As String
        Dim varStrEmpNameAdd As String
        Dim varDblCL As Double
        Dim varDblEL As Double
        Dim varDblTL As Double
        Dim varStrWOff1 As String
        Dim varStrWOff2 As String
        Dim varStrDeptName As String
        Dim varStrDeptNameAdd As String
        Dim varStrUserID As String
        Dim varStrUserIDAdd As String
        Dim varTempStr As String
        Dim varTempStrAdd As String
        Dim varStrUserName As String
        Dim varStrUserNameAdd As String

        Dim clsBL As ETS.BL.LeaveBalance
        Dim DSLB As New Data.DataSet
        Dim DVLB As New Data.DataView
        Dim DSLBAdd As New Data.DataSet
        Dim DVLBAdd As New Data.DataView
        Dim oRecLB As Data.DataTableReader
        Dim oRecAddNew As Data.DataTableReader
        Dim varBolLB As Boolean = True
        Dim varBolLBAdd As Boolean = True

        Try
            Dim varStrDeptValue As String
            varStrDeptValue = DropDownGroup.Items.Item(DropDownGroup.SelectedIndex).Value.ToString

            clsBL = New ETS.BL.LeaveBalance
            DSLB = clsBL.GetLeaveBalanceRecords(Session("ContractorID"))

            If DSLB.Tables.Count > 0 Then
                If DSLB.Tables(0).Rows.Count > 0 Then
                    If Trim(UCase(varStrDeptValue)) = Trim(UCase("ALL")) Then
                        oRecLB = DSLB.Tables(0).CreateDataReader()
                    Else
                        DVLB = New Data.DataView(DSLB.Tables(0), "DepartmentID='" & varStrDeptValue & "'", String.Empty, Data.DataViewRowState.CurrentRows)
                        If DVLB.Count > 0 Then
                            oRecLB = DVLB.ToTable.CreateDataReader
                        Else
                            varBolLB = False
                        End If
                    End If
                    If varBolLB = True Then
                        If oRecLB.HasRows Then
                            While oRecLB.Read
                                varStrEmpName = oRecLB.GetString(oRecLB.GetOrdinal("FirstName")).ToString & " " & oRecLB.GetString(oRecLB.GetOrdinal("LastName")).ToString
                                varDblCL = oRecLB.GetDouble(oRecLB.GetOrdinal("CL"))
                                varDblEL = oRecLB.GetDouble(oRecLB.GetOrdinal("EL"))
                                varDblTL = oRecLB.GetDouble(oRecLB.GetOrdinal("TL"))
                                varStrDeptName = oRecLB.GetString(oRecLB.GetOrdinal("DeptName")).ToString
                                varStrUserID = oRecLB.GetGuid(oRecLB.GetOrdinal("UserID")).ToString
                                varStrUserName = oRecLB.GetString(oRecLB.GetOrdinal("UserName")).ToString

                                If oRecLB.IsDBNull(oRecLB.GetOrdinal("WeeklyOff1")) Then
                                    varStrWOff1 = String.Empty
                                Else
                                    varStrWOff1 = oRecLB.GetString(oRecLB.GetOrdinal("WeeklyOff1"))
                                End If
                                If oRecLB.IsDBNull(oRecLB.GetOrdinal("WeeklyOff2")) Then
                                    varStrWOff2 = String.Empty
                                Else
                                    varStrWOff2 = oRecLB.GetString(oRecLB.GetOrdinal("WeeklyOff2"))
                                End If



                                Dim tblrow As New TableRow

                                Dim cell1 As New TableCell
                                Dim cell2 As New TableCell
                                Dim cell3 As New TableCell
                                Dim cell4 As New TableCell
                                Dim cell5 As New TableCell
                                Dim cell6 As New TableCell
                                Dim cell7 As New TableCell
                                Dim cell8 As New TableCell
                                Dim cell9 As New TableCell

                                cell9.Text = varStrUserName
                                cell1.Text = varStrEmpName
                                cell2.Text = varStrDeptName
                                cell3.Text = varDblCL
                                cell4.Text = varDblEL
                                cell5.Text = varDblTL

                                If Not String.IsNullOrEmpty(varStrWOff1) Then
                                    cell6.Text = varStrWOff1
                                Else
                                    cell6.Text = "&nbsp;"
                                End If

                                If Not String.IsNullOrEmpty(varStrWOff2) Then
                                    cell7.Text = varStrWOff2
                                Else
                                    cell7.Text = "&nbsp;"
                                End If


                                cell1.HorizontalAlign = HorizontalAlign.Left
                                cell2.HorizontalAlign = HorizontalAlign.Left
                                cell9.HorizontalAlign = HorizontalAlign.Left

                                varTempStr = "<center><a href=""UpdateLeaveBalance.aspx?UserID='" & varStrUserID & "'&Action=Edit"">Edit</a></center>"

                                cell8.Text = varTempStr

                                tblrow.Cells.Add(cell9)
                                tblrow.Cells.Add(cell1)
                                tblrow.Cells.Add(cell2)
                                tblrow.Cells.Add(cell3)
                                tblrow.Cells.Add(cell4)
                                tblrow.Cells.Add(cell5)
                                tblrow.Cells.Add(cell6)
                                tblrow.Cells.Add(cell7)
                                tblrow.Cells.Add(cell8)

                                Table2.Rows.Add(tblrow)
                            End While
                        End If
                        oRecLB.Close()
                    End If
                End If
            End If

            DSLBAdd = clsBL.GetLeaveBalanceRecordsForAdd(Session("ContractorID"))
            If DSLBAdd.Tables.Count > 0 Then
                If DSLBAdd.Tables(0).Rows.Count > 0 Then
                    If Trim(UCase(varStrDeptValue)) = Trim(UCase("ALL")) Then
                        oRecAddNew = DSLBAdd.Tables(0).CreateDataReader()
                    Else
                        DVLBAdd = New Data.DataView(DSLBAdd.Tables(0), "DepartmentID='" & varStrDeptValue & "'", String.Empty, Data.DataViewRowState.CurrentRows)
                        If DVLBAdd.Count > 0 Then
                            oRecAddNew = DVLBAdd.ToTable.CreateDataReader
                        Else
                            varBolLBAdd = False
                        End If
                    End If
                    If varBolLBAdd = True Then
                        If oRecAddNew.HasRows Then
                            While oRecAddNew.Read
                                varStrUserNameAdd = oRecAddNew.GetString(oRecAddNew.GetOrdinal("UserName")).ToString
                                varStrEmpNameAdd = oRecAddNew.GetString(oRecAddNew.GetOrdinal("FirstName")).ToString & " " & oRecAddNew.GetString(oRecAddNew.GetOrdinal("LastName")).ToString
                                varStrDeptNameAdd = oRecAddNew.GetString(oRecAddNew.GetOrdinal("DeptName")).ToString
                                varStrUserIDAdd = oRecAddNew.GetGuid(oRecAddNew.GetOrdinal("UserID")).ToString

                                Dim tblrowAdd As New TableRow

                                Dim cell1Add As New TableCell
                                Dim cell2Add As New TableCell
                                Dim cell3Add As New TableCell
                                Dim cell4Add As New TableCell
                                Dim cell5Add As New TableCell
                                Dim cell6Add As New TableCell
                                Dim cell7Add As New TableCell
                                Dim cell8Add As New TableCell
                                Dim cell9Add As New TableCell

                                cell1Add.HorizontalAlign = HorizontalAlign.Left
                                cell2Add.HorizontalAlign = HorizontalAlign.Left
                                cell9Add.HorizontalAlign = HorizontalAlign.Left

                                cell9Add.Text = varStrUserNameAdd
                                cell1Add.Text = varStrEmpNameAdd
                                cell2Add.Text = varStrDeptNameAdd
                                cell3Add.Text = "&nbsp"
                                cell4Add.Text = "&nbsp"
                                cell5Add.Text = "&nbsp"
                                cell6Add.Text = "&nbsp"
                                cell7Add.Text = "&nbsp"

                                varTempStrAdd = "<center><a href=""UpdateLeaveBalance.aspx?UserID='" & varStrUserIDAdd & "'&Action=Add"">Add</a></center>"

                                cell8Add.Text = varTempStrAdd

                                tblrowAdd.Cells.Add(cell9Add)
                                tblrowAdd.Cells.Add(cell1Add)
                                tblrowAdd.Cells.Add(cell2Add)
                                tblrowAdd.Cells.Add(cell3Add)
                                tblrowAdd.Cells.Add(cell4Add)
                                tblrowAdd.Cells.Add(cell5Add)
                                tblrowAdd.Cells.Add(cell6Add)
                                tblrowAdd.Cells.Add(cell7Add)
                                tblrowAdd.Cells.Add(cell8Add)

                                Table2.Rows.Add(tblrowAdd)

                            End While
                        End If

                        oRecAddNew.Close()
                    End If
                End If
            End If
        Catch ex As Exception
        Finally
            clsBL = Nothing
            DSLB = Nothing
            DSLBAdd = Nothing
            DVLB = Nothing
            DVLBAdd = Nothing
            oRecLB = Nothing
            oRecAddNew = Nothing
        End Try
    End Sub
    Protected Sub btnExport_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnExport.Click
        Try
            Response.Clear()
            ' Set the content type to Excel.
            Dim filename = "Leave Balance Report.xls"
            Response.AddHeader("content-disposition", "attachment;filename=" & filename)

            Response.ContentType = "application/vnd.ms-excel"
            ' Remove the charset from the Content-Type header.
            Response.Charset = ""
            ' Turn off the view state.
            Me.EnableViewState = False
            FillData()

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
End Class
