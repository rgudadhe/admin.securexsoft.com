Imports MainModule
Partial Class ImportLogDetails
    Inherits BasePage
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Not Page.IsPostBack Then
            Dim varStrTrackID As String
            varStrTrackID = Request.QueryString("TrackID")

            Dim DS As New Data.DataSet
            Dim clsLB As ETS.BL.LeaveBalance
            Dim oRecLBLogDetails As Data.DataTableReader

            Try
                clsLB = New ETS.BL.LeaveBalance
                DS = clsLB.GetLeaveBalanceImportDetails(Session("ContractorID").ToString, varStrTrackID.ToString)
                If DS.Tables.Count > 0 Then
                    If DS.Tables(0).Rows.Count > 0 Then
                        oRecLBLogDetails = DS.Tables(0).CreateDataReader
                        If oRecLBLogDetails.HasRows Then
                            While oRecLBLogDetails.Read
                                Dim varTblRow As New TableRow
                                varTblRow.Font.Size = 10
                                Dim varTblCellUserName As New TableCell
                                varTblCellUserName.Text = oRecLBLogDetails.GetString(oRecLBLogDetails.GetOrdinal("FirstName")) & " " & oRecLBLogDetails.GetString(oRecLBLogDetails.GetOrdinal("LastName"))

                                Dim varTblCellBCL As New TableCell
                                varTblCellBCL.Text = oRecLBLogDetails.GetDouble(oRecLBLogDetails.GetOrdinal("BCL"))

                                Dim varTblCellBEL As New TableCell
                                varTblCellBEL.Text = oRecLBLogDetails.GetDouble(oRecLBLogDetails.GetOrdinal("BEL"))

                                Dim varTblCellBWOff1 As New TableCell
                                If Not oRecLBLogDetails.IsDBNull(oRecLBLogDetails.GetOrdinal("BWOff1")) Then
                                    varTblCellBWOff1.Text = oRecLBLogDetails.GetString(oRecLBLogDetails.GetOrdinal("BWOff1"))
                                Else
                                    varTblCellBWOff1.Text = "&nbsp"
                                End If


                                Dim varTblCellBWOff2 As New TableCell
                                If Not oRecLBLogDetails.IsDBNull(oRecLBLogDetails.GetOrdinal("BWOff2")) Then
                                    varTblCellBWOff2.Text = oRecLBLogDetails.GetString(oRecLBLogDetails.GetOrdinal("BWOff2"))
                                Else
                                    varTblCellBWOff2.Text = "&nbsp"
                                End If

                                Dim varTblCellCL As New TableCell
                                varTblCellCL.Text = oRecLBLogDetails.GetDouble(oRecLBLogDetails.GetOrdinal("ACL"))

                                Dim varTblCellEL As New TableCell
                                varTblCellEL.Text = oRecLBLogDetails.GetDouble(oRecLBLogDetails.GetOrdinal("AEL"))

                                Dim varTblCellWOff1 As New TableCell
                                If Not oRecLBLogDetails.IsDBNull(oRecLBLogDetails.GetOrdinal("AWOff1")) Then
                                    varTblCellWOff1.Text = oRecLBLogDetails.GetString(oRecLBLogDetails.GetOrdinal("AWOff1"))
                                Else
                                    varTblCellWOff1.Text = "&nbsp"
                                End If


                                Dim varTblCellWOff2 As New TableCell
                                If Not oRecLBLogDetails.IsDBNull(oRecLBLogDetails.GetOrdinal("AWOff2")) Then
                                    varTblCellWOff2.Text = oRecLBLogDetails.GetString(oRecLBLogDetails.GetOrdinal("AWOff2"))
                                Else
                                    varTblCellWOff2.Text = "&nbsp"
                                End If

                                Dim varTblCellUName As New TableCell
                                varTblCellUName.Text = oRecLBLogDetails.GetString(oRecLBLogDetails.GetOrdinal("UserName"))

                                varTblRow.Cells.Add(varTblCellUName)
                                varTblRow.Cells.Add(varTblCellUserName)
                                varTblRow.Cells.Add(varTblCellBCL)
                                varTblRow.Cells.Add(varTblCellBEL)
                                varTblRow.Cells.Add(varTblCellBWOff1)
                                varTblRow.Cells.Add(varTblCellBWOff2)
                                varTblRow.Cells.Add(varTblCellCL)
                                varTblRow.Cells.Add(varTblCellEL)
                                varTblRow.Cells.Add(varTblCellWOff1)
                                varTblRow.Cells.Add(varTblCellWOff2)
                                tblDataImported.Rows.Add(varTblRow)
                            End While
                        End If
                        oRecLBLogDetails.Close()
                    End If
                End If
                'Dim oCommandLBLogDetails As New Data.SqlClient.SqlCommand("SELECT *,U.FirstName,U.LastName,U.UserName FROM DBO.tblPrevLeaveBalance PLB INNER JOIN DBO.tblUsers U ON PLB.UserID=U.UserID WHERE TrackID='" & varStrTrackID & "' AND U.ContractorID='" & Session("ContractorID").ToString & "' ", objConn)
                'Dim oRecLBLogDetails As Data.SqlClient.SqlDataReader = oCommandLBLogDetails.ExecuteReader()


                'oRecLBLogDetails = Nothing
                'oCommandLBLogDetails = Nothing
            Catch ex As Exception
                Response.Write(ex.Message)
            Finally
                clsLB = Nothing
                DS.Dispose()
                oRecLBLogDetails = Nothing
            End Try
        End If
    End Sub
End Class
