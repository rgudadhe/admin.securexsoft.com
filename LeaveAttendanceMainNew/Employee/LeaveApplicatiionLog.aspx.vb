Imports MainModule
Partial Class LeaveAttendanceMainNew_LeaveApplicatiionLog
    Inherits System.Web.UI.Page
    Dim objMainModule As New MainModule
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim objConn As New Data.SqlClient.SqlConnection
        objConn = objMainModule.OpenConnection(objConn)

        Try
            If Not Page.IsPostBack Then
                Dim objGetData As New Data.SqlClient.SqlCommand("SELECT * FROM DBO.tblLeaveLog LG WHERE LeaveID='" & Request.QueryString("LeaveID") & "' ORDER BY OprOn DESC", objConn)
                Dim objRecGetData As Data.SqlClient.SqlDataReader = objGetData.ExecuteReader
                If objRecGetData.HasRows Then
                    While objRecGetData.Read
                        Dim varTblRow As New TableRow
                        Dim varTblCellLeaveType As New TableCell
                        Dim varTblCellActionBy As New TableCell
                        Dim varTblCellActionOn As New TableCell
                        Dim varTblCellActionDetails As New TableCell
                        Dim varTblCellLB As New TableCell
                        Dim varTblCellLA As New TableCell

                        If Not objRecGetData.IsDBNull(objRecGetData.GetOrdinal("LeaveType")) Then
                            varTblCellLeaveType.Text = objRecGetData.GetString(objRecGetData.GetOrdinal("LeaveType"))
                        End If

                        If Not objRecGetData.IsDBNull(objRecGetData.GetOrdinal("OprOn")) Then
                            varTblCellActionOn.Text = objRecGetData.GetDateTime(objRecGetData.GetOrdinal("OprOn"))
                        End If

                        If Not objRecGetData.IsDBNull(objRecGetData.GetOrdinal("OprDesc")) Then
                            varTblCellActionDetails.Text = objRecGetData.GetString(objRecGetData.GetOrdinal("OprDesc"))
                        End If

                        If Not objRecGetData.IsDBNull(objRecGetData.GetOrdinal("OprBy")) Then
                            varTblCellActionBy.Text = objRecGetData.GetString(objRecGetData.GetOrdinal("OprBy"))
                        End If

                        If Not objRecGetData.IsDBNull(objRecGetData.GetOrdinal("BBalance")) Then
                            varTblCellLB.Text = objRecGetData.GetDouble(objRecGetData.GetOrdinal("BBalance"))
                        End If

                        If Not objRecGetData.IsDBNull(objRecGetData.GetOrdinal("ABalance")) Then
                            varTblCellLA.Text = objRecGetData.GetDouble(objRecGetData.GetOrdinal("ABalance"))
                        End If


                        varTblRow.Cells.Add(varTblCellLeaveType)
                        varTblRow.Cells.Add(varTblCellActionBy)
                        varTblRow.Cells.Add(varTblCellActionOn)
                        varTblRow.Cells.Add(varTblCellActionDetails)
                        varTblRow.Cells.Add(varTblCellLB)
                        varTblRow.Cells.Add(varTblCellLA)
                        tblMain.Rows.Add(varTblRow)
                    End While
                End If
            End If
        Catch ex As Exception
            Response.Write(ex.Message)
        Finally
            If objConn.State <> Data.ConnectionState.Closed Then
                objConn.Close()
                objConn = Nothing
            End If
        End Try
    End Sub
End Class
