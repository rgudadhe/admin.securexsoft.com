Partial Class PrintTicket
    Inherits System.Web.UI.Page
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim objConn As New Data.SqlClient.SqlConnection
        objConn = OpenConnection(objConn)

        Dim varArrUserName As New ArrayList
        Dim varArrSubject As New ArrayList
        Dim varArrDetails As New ArrayList
        Dim varArrDateTime As New ArrayList
        Dim varArrUserID As New ArrayList
        Dim varArrAccID As New ArrayList
        Dim varArrActionType As New ArrayList
        Dim varArrActionDate As New ArrayList
        Dim varStrQuery As String
        Dim varStrActionType As String
        Dim varIntI As Integer
        Dim ConStringClient As String
        Dim varStrMainSubject As String
        Dim varStrMainMessage As String
        Dim varStrMainDate As String
        Dim TicketID As String
        TicketID = Request.QueryString("TID").ToString

        Try
            'varStrQuery = "SELECT * FROM dbo.tblCustomerTicketAction TA LEFT JOIN dbo.tblUsers U ON TA.ActionBy=U.UserID INNER JOIN dbo.tblDepartments D ON U.DepartmentID=U.DepartmentID WHERE TicketID='" & TicketID & "' AND D.Name='Support' AND DepartmentID IS NULL"
            Dim objCmdRes As New Data.SqlClient.SqlCommand("SELECT * FROM dbo.tblCustomerTicketAction WHERE TicketID='" & TicketID & "' AND ForwardDepartmentID IS NULL ORDER BY ActionDate DESC ", objConn)
            Dim objRecRes As Data.SqlClient.SqlDataReader = objCmdRes.ExecuteReader
            If objRecRes.HasRows Then
                While objRecRes.Read
                    If Not objRecRes.IsDBNull(objRecRes.GetOrdinal("ActionType")) Then
                        varStrActionType = objRecRes.GetString(objRecRes.GetOrdinal("ActionType"))
                        varArrActionType.Add(varStrActionType)
                    End If
                    If Not objRecRes.IsDBNull(objRecRes.GetOrdinal("ActionBy")) Then
                        'varArrUserName.Add(GetUserName(varStrActionType, objRecRes.GetGuid(objRecRes.GetOrdinal("ActionBy")).ToString))
                        varArrUserName.Add(objRecRes.GetGuid(objRecRes.GetOrdinal("ActionBy")).ToString)
                    End If
                    If Not objRecRes.IsDBNull(objRecRes.GetOrdinal("Subject")) Then
                        varArrSubject.Add(objRecRes.GetString(objRecRes.GetOrdinal("Subject")))
                    Else
                        varArrSubject.Add("")
                    End If
                    If Not objRecRes.IsDBNull(objRecRes.GetOrdinal("ActionDetails")) Then
                        varArrDetails.Add(objRecRes.GetString(objRecRes.GetOrdinal("ActionDetails")))
                    Else
                        varArrDetails.Add("")
                    End If
                    If Not objRecRes.IsDBNull(objRecRes.GetOrdinal("ActionDate")) Then
                        varArrActionDate.Add(objRecRes.GetDateTime(objRecRes.GetOrdinal("ActionDate")))
                    End If
                End While
            End If
            objRecRes.Close()
            objRecRes = Nothing
            objCmdRes = Nothing

            Dim varTblRowHRMain As New TableRow
            Dim varTblHRCellMain As New TableCell
            varTblHRCellMain.ColumnSpan = 2
            varTblHRCellMain.Text = "<HR>"
            varTblRowHRMain.Cells.Add(varTblHRCellMain)

            Dim varBTblRowHRMain As New TableRow
            Dim varBTblHRCellMain As New TableCell
            varBTblHRCellMain.ColumnSpan = 2
            varBTblHRCellMain.Text = "<HR>"
            varBTblRowHRMain.Cells.Add(varTblHRCellMain)

            For varIntI = 0 To varArrUserName.Count - 1
                Dim varTblRow As New TableRow
                Dim varTblRowHR As New TableRow
                Dim varTblFromCell As New TableCell
                Dim varTblMsgCell As New TableCell
                Dim varTblHRCell As New TableCell
                Dim varStrUserName As String
                varTblHRCell.ColumnSpan = 2
                varTblHRCell.Text = "<HR>"

                varTblRowHR.Cells.Add(varTblHRCell)
                If Trim(UCase(varArrActionType(varIntI))) = Trim(UCase("Modified Ticket")) Then
                    varStrQuery = "SELECT FirstName,LastName FROM dbo.tblUsers WHERE UserID='" & varArrUserName(varIntI) & "' AND IsDeleted IS NOT NULL "
                    Dim objCmd As New Data.SqlClient.SqlCommand(varStrQuery, objConn)
                    Dim objRec As Data.SqlClient.SqlDataReader = objCmd.ExecuteReader
                    If objRec.HasRows Then
                        While objRec.Read
                            If Not objRec.IsDBNull(objRec.GetOrdinal("FirstName")) Then
                                varStrUserName = objRec.GetString(objRec.GetOrdinal("FirstName"))
                            End If
                            If Not objRec.IsDBNull(objRec.GetOrdinal("LastName")) Then
                                varStrUserName = varStrUserName & objRec.GetString(objRec.GetOrdinal("LastName"))
                            End If
                        End While
                    End If
                    objRec.Close()
                    objRec = Nothing
                    objCmd = Nothing
                    'Request from edictate cs department to disply E-Dictate Support Team instead of employee names on 26/12/2009
                    varStrUserName = "E-Dictate Support Team"
                    'Request End
                ElseIf Trim(UCase(varArrActionType(varIntI))) = Trim(UCase("Added Comments")) Then
                    'ConStringClient = System.Configuration.ConfigurationManager.AppSettings("ETSConClient")
                    'objMainModule.oConn.ConnectionString = ConStringClient
                    varStrQuery = "SELECT AccountName FROM dbo.tblAccounts WHERE AccountID='" & varArrUserName(varIntI) & "'"
                    Dim objCmd As New Data.SqlClient.SqlCommand(varStrQuery, objConn)
                    Dim objRec As Data.SqlClient.SqlDataReader = objCmd.ExecuteReader
                    If objRec.HasRows Then
                        While objRec.Read
                            If Not objRec.IsDBNull(objRec.GetOrdinal("AccountName")) Then
                                varStrUserName = objRec.GetString(objRec.GetOrdinal("AccountName"))
                            End If
                        End While
                    End If
                    objRec.Close()
                    objRec = Nothing
                    objCmd = Nothing
                End If
                Dim varStrTempTable As String

                varStrTempTable = "<table><tr><td>Subject : " & varArrSubject(varIntI) & "<hr></td></tr><tr><td>" & varArrDetails(varIntI) & "</td></tr><tr><td>Date : " & varArrActionDate(varIntI) & "</td></tr></table>"
                varTblFromCell.Text = varStrUserName
                varTblFromCell.Font.Bold = True
                varTblFromCell.VerticalAlign = VerticalAlign.Top
                varTblMsgCell.Text = varStrTempTable
                varTblRow.Cells.Add(varTblFromCell)
                varTblRow.Cells.Add(varTblMsgCell)
                If varIntI Mod 2 = 0 Then
                    'varTblRow.BackColor = Drawing.Color.LightGray
                Else
                    'varTblRow.BackColor = Drawing.Color.LightBlue
                End If
                tblMain.Rows.Add(varTblRowHR)
                tblMain.Rows.Add(varTblRow)
                'tblMain.Rows.Add(varTblRowHR)
            Next

            Dim varTblRowMainT As New TableRow
            Dim varTblCellMainFromT As New TableCell
            Dim varTblCellMainMsgT As New TableCell

            Dim objCmdMainT As New Data.SqlClient.SqlCommand("SELECT AccountName,TicketNo,TicketDetails,Subject,DatePosted FROM dbo.tblCustomerTickets CT INNER JOIN dbo.tblAccounts A ON CT.AccID=A.AccountID WHERE TicketID='" & TicketID & "'", objConn)
            Dim objRecMainT As Data.SqlClient.SqlDataReader = objCmdMainT.ExecuteReader
            Dim varStrTempTable1 As String
            If objRecMainT.HasRows Then
                While objRecMainT.Read
                    If Not objRecMainT.IsDBNull(objRecMainT.GetOrdinal("Subject")) Then
                        varStrMainSubject = objRecMainT.GetString(objRecMainT.GetOrdinal("Subject"))
                    Else
                        varStrMainSubject = ""
                    End If
                    If Not objRecMainT.IsDBNull(objRecMainT.GetOrdinal("TicketDetails")) Then
                        varStrMainMessage = objRecMainT.GetString(objRecMainT.GetOrdinal("TicketDetails"))
                    Else
                        varStrMainMessage = ""
                    End If
                    If Not objRecMainT.IsDBNull(objRecMainT.GetOrdinal("DatePosted")) Then
                        varStrMainDate = objRecMainT.GetDateTime(objRecMainT.GetOrdinal("DatePosted")).ToShortDateString
                    Else
                        varStrMainDate = ""
                    End If
                    varTblCellMainFromT.VerticalAlign = VerticalAlign.Top
                    varTblCellMainFromT.Text = objRecMainT.GetString(objRecMainT.GetOrdinal("AccountName"))
                    varTblCellMainFromT.Font.Bold = True
                    varStrTempTable1 = "<table><tr><td>Subject : " & varStrMainSubject & "<hr></td></tr><tr><td>" & varStrMainMessage & "</td></tr><tr><td>Date : " & varStrMainDate & "</td></tr></table>"
                    varTblCellMainMsgT.Text = varStrTempTable1
                    tlbSubject.Text = "<B>Subject : </B>" & varStrMainSubject
                    TicketNo.Text = objRecMainT("TicketNo")
                    DateCreated.Text = varStrMainDate
                End While
            End If

            objRecMainT.Close()
            objRecMainT = Nothing
            objCmdMainT = Nothing
            varTblRowMainT.Cells.Add(varTblCellMainFromT)
            varTblRowMainT.Cells.Add(varTblCellMainMsgT)
            If varIntI Mod 2 = 0 Then
                'varTblRowMainT.BackColor = Drawing.Color.LightGray
            Else
                'varTblRowMainT.BackColor = Drawing.Color.LightBlue
            End If
            tblMain.Rows.Add(varBTblRowHRMain)
            tblMain.Rows.Add(varTblRowMainT)
            tblMain.Rows.Add(varTblRowHRMain)
        Catch ex As Exception
        Finally
            If objConn.State <> Data.ConnectionState.Closed Then
                objConn.Close()
                objConn = Nothing
            End If
        End Try
    End Sub
    Public Function OpenConnection(ByRef Conn As Data.SqlClient.SqlConnection) As Data.SqlClient.SqlConnection
        Try
            Conn.ConnectionString = System.Configuration.ConfigurationManager.AppSettings("ETSCon")
            Conn.Open()
            Return Conn
        Catch ex As Exception
        End Try
    End Function
End Class
