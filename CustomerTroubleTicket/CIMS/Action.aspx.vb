
Partial Class CIMS_Action
    Inherits BasePage
    Public Function OpenConnection(ByRef Conn As Data.SqlClient.SqlConnection) As Data.SqlClient.SqlConnection
        Try
            Conn.ConnectionString = System.Configuration.ConfigurationManager.AppSettings("ETSCon")
            Conn.Open()
            Return Conn
        Catch ex As Exception
        End Try
    End Function
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            GetTicketHistory(Trim(Request.QueryString("ID")))
            GetLog()
            GetActionInfo()
            IMG2.Attributes.Add("OnClick", "window.open('PrintTicket.aspx?TID=" & Request.QueryString("ID") & "');")
        End If
    End Sub
    Protected Sub GetTicketHistory(ByVal TicketID As String)
        Dim objConn As New Data.SqlClient.SqlConnection
        objConn = OpenConnection(objConn)
        Try
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

            For varIntI = 0 To varArrUserName.Count - 1
                Dim varTblRow As New TableRow
                Dim varTblFromCell As New TableCell
                Dim varTblMsgCell As New TableCell
                Dim varStrUserName As String

                If Trim(UCase(varArrActionType(varIntI))) = Trim(UCase("Modified Ticket")) Then
                    varStrQuery = "SELECT FirstName,LastName FROM dbo.tblUsers WHERE UserID='" & varArrUserName(varIntI) & "' AND (IsDeleted IS NULL OR IsDeleted=0) "
                    Dim objCmd As New Data.SqlClient.SqlCommand(varStrQuery, objConn)
                    Dim objRec As Data.SqlClient.SqlDataReader = objCmd.ExecuteReader
                    If objRec.HasRows Then
                        While objRec.Read
                            If Not objRec.IsDBNull(objRec.GetOrdinal("FirstName")) Then
                                varStrUserName = objRec.GetString(objRec.GetOrdinal("FirstName"))
                            End If
                            If Not objRec.IsDBNull(objRec.GetOrdinal("LastName")) Then
                                varStrUserName = varStrUserName & " " & objRec.GetString(objRec.GetOrdinal("LastName"))
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

                varStrTempTable = "<table><tr><td>Subject : " & varArrSubject(varIntI) & "</td></tr><tr><td>" & varArrDetails(varIntI) & "</td></tr><tr><td>Date : " & varArrActionDate(varIntI) & "</td></tr></table>"
                varTblFromCell.Text = varStrUserName
                varTblFromCell.VerticalAlign = VerticalAlign.Top
                varTblMsgCell.Text = varStrTempTable
                varTblRow.Cells.Add(varTblFromCell)
                varTblRow.Cells.Add(varTblMsgCell)

                If varIntI Mod 2 = 0 Then
                    varTblRow.BackColor = System.Drawing.ColorTranslator.FromHtml("#F7F6F3")
                    varTblRow.ForeColor = System.Drawing.ColorTranslator.FromHtml("#284775")
                Else
                    varTblRow.BackColor = Drawing.Color.White
                    varTblRow.ForeColor = System.Drawing.ColorTranslator.FromHtml("#333333")
                End If

                tblViewTicketHistory.Rows.Add(varTblRow)
            Next

            Dim varTblRowMainT As New TableRow
            Dim varTblCellMainFromT As New TableCell
            Dim varTblCellMainMsgT As New TableCell

            Dim objCmdMainT As New Data.SqlClient.SqlCommand("SELECT AccountName,TicketDetails,Subject,DatePosted FROM dbo.tblCustomerTickets CT INNER JOIN dbo.tblAccounts A ON CT.AccID=A.AccountID WHERE TicketID='" & TicketID & "'", objConn)
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
                        varStrMainDate = objRecMainT.GetDateTime(objRecMainT.GetOrdinal("DatePosted"))
                    Else
                        varStrMainDate = ""
                    End If
                    varTblCellMainFromT.VerticalAlign = VerticalAlign.Top
                    varTblCellMainFromT.Text = objRecMainT.GetString(objRecMainT.GetOrdinal("AccountName"))
                    varStrTempTable1 = "<table><tr><td>Subject : " & varStrMainSubject & "</td></tr><tr><td>" & varStrMainMessage & "</td></tr><tr><td>Date : " & varStrMainDate & "</td></tr></table>"
                    varTblCellMainMsgT.Text = varStrTempTable1
                    'lblTicketSubject.Text = varStrMainSubject
                End While
            End If
            objRecMainT.Close()
            objRecMainT = Nothing
            objCmdMainT = Nothing
            varTblRowMainT.Cells.Add(varTblCellMainFromT)
            varTblRowMainT.Cells.Add(varTblCellMainMsgT)
            If varIntI Mod 2 = 0 Then
                varTblRowMainT.BackColor = Drawing.Color.WhiteSmoke
            Else
                'varTblRowMainT.BackColor = Drawing.Color.LightBlue
            End If
            tblViewTicketHistory.Rows.Add(varTblRowMainT)

            'tblViewTicketDetails.Visible = False

        Catch ex As Exception
        Finally
            If objConn.State <> Data.ConnectionState.Closed Then
                objConn.Close()
                objConn = Nothing
            End If
        End Try
    End Sub
    Protected Sub GetLog()
        Dim varIntI As Integer
        varIntI = 1

        Dim objConn As New Data.SqlClient.SqlConnection
        objConn = OpenConnection(objConn)

        Try
            Dim varTblFirstRow As New HtmlTableRow
            Dim varTblFCellDate As New HtmlTableCell
            Dim varTblFCellAction As New HtmlTableCell
            Dim varStrDate As Date
            Dim varStrQuery As String
            varStrQuery = "SELECT DatePosted FROM dbo.tblCustomerTickets WHERE TicketID='" & Request.QueryString("ID") & "'"
            Dim objCmdMainLog As New Data.SqlClient.SqlCommand(varStrQuery, objConn)
            Dim objRecMainLog As Data.SqlClient.SqlDataReader = objCmdMainLog.ExecuteReader()
            If objRecMainLog.HasRows Then
                While objRecMainLog.Read
                    varStrDate = objRecMainLog.GetDateTime(objRecMainLog.GetOrdinal("DatePosted"))
                End While
            End If

            objRecMainLog.Close()
            objRecMainLog = Nothing
            objCmdMainLog = Nothing

            varTblFCellDate.Width = 200
            varTblFCellDate.InnerHtml = varStrDate.ToString
            varTblFCellAction.InnerHtml = "Ticket Created"
            varTblFCellDate.Attributes.Add("class", "Voice1")
            varTblFCellAction.Attributes.Add("class", "Voice1")

            varTblFirstRow.Cells.Add(varTblFCellDate)
            varTblFirstRow.Cells.Add(varTblFCellAction)

            tblLog.Rows.Add(varTblFirstRow)

            Dim cmdLog As New Data.SqlClient.SqlCommand("SELECT ActionName,ActionDate FROM dbo.tblCustomerTicketsLog WHERE TicketID='" & Request.QueryString("ID") & "' ORDER BY ActionDate", objConn)
            Dim RecLog As Data.SqlClient.SqlDataReader = cmdLog.ExecuteReader

            If RecLog.HasRows Then
                While RecLog.Read
                    varIntI = varIntI + 1
                    Dim varTblRow As New HtmlTableRow
                    Dim varTblDateCell As New HtmlTableCell
                    Dim varTblActionCell As New HtmlTableCell
                    varTblDateCell.Width = 200
                    varTblDateCell.InnerHtml = RecLog.GetDateTime(RecLog.GetOrdinal("ActionDate")).ToString
                    varTblActionCell.InnerHtml = RecLog.GetString(RecLog.GetOrdinal("ActionName"))
                    varTblRow.Cells.Add(varTblDateCell)
                    varTblRow.Cells.Add(varTblActionCell)
                    varTblDateCell.Attributes.Add("class", "Voice1")
                    varTblActionCell.Attributes.Add("class", "Voice1")
                    tblLog.Rows.Add(varTblRow)
                End While
            End If
            RecLog.Close()
            RecLog = Nothing
            cmdLog = Nothing
        Catch ex As Exception
        Finally
            If objConn.State <> Data.ConnectionState.Closed Then
                objConn.Close()
                objConn = Nothing
            End If
        End Try
    End Sub
    Protected Sub GetActionInfo()
        Dim objConn As New Data.SqlClient.SqlConnection
        objConn = OpenConnection(objConn)

        Try
            Dim varStrStatus As String = String.Empty
            Dim varStrPriority As String = String.Empty
            Dim varStrSubject As String = String.Empty

            Dim objTicketInfo As New Data.SqlClient.SqlCommand("SELECT Subject,Status,Priority FROM dbo.tblCustomerTickets WHERE TicketID='" & Request.QueryString("ID") & "'", objConn)
            Dim objTicketInfoRec As Data.SqlClient.SqlDataReader = objTicketInfo.ExecuteReader()
            If objTicketInfoRec.HasRows Then
                While objTicketInfoRec.Read
                    If Not objTicketInfoRec.IsDBNull(objTicketInfoRec.GetOrdinal("Status")) Then
                        varStrStatus = objTicketInfoRec.GetString(objTicketInfoRec.GetOrdinal("Status"))
                    End If
                    If Not objTicketInfoRec.IsDBNull(objTicketInfoRec.GetOrdinal("Priority")) Then
                        varStrPriority = objTicketInfoRec.GetString(objTicketInfoRec.GetOrdinal("Priority"))
                    End If

                    If Not objTicketInfoRec.IsDBNull(objTicketInfoRec.GetOrdinal("Subject")) Then
                        varStrSubject = objTicketInfoRec.GetString(objTicketInfoRec.GetOrdinal("Subject"))
                    End If

                End While
            End If
            objTicketInfoRec.Close()
            objTicketInfoRec = Nothing
            objTicketInfoRec = Nothing

            If Not String.IsNullOrEmpty(varStrSubject) Then
                txtSubject.Text = varStrSubject
            End If
            If Not String.IsNullOrEmpty(varStrPriority) Then
                ddPriority.Items.FindByValue(varStrPriority).Selected = True
            End If
            'Response.Write(varStrStatus)
            If Not String.IsNullOrEmpty(varStrStatus) Then
                ddStatus.Items.FindByValue(varStrStatus).Selected = True
            End If

        Catch ex As Exception
            'Response.Write(ex.Message)
        Finally
            If objConn.State <> Data.ConnectionState.Closed Then
                objConn.Close()
                objConn = Nothing
            End If
        End Try

    End Sub

    Protected Sub Button2_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button2.Click
        Dim objConn As New Data.SqlClient.SqlConnection
        objConn = OpenConnection(objConn)

        Try
            Dim varStrStatus As String
            Dim varStrPriority As String
            Dim varStrSubject As String
            Dim varStrMessage As String
            Dim varStrUpdate As String
            Dim varStrInsert As String


            varStrStatus = Request("ddStatus")
            varStrPriority = Request("ddPriority")
            varStrSubject = Replace(Request("txtSubject"), "'", "''")
            varStrMessage = Replace(Request("txtMessage"), "'", "''")


            varStrInsert = "INSERT INTO dbo.tblCustomerTicketAction (TicketID,Subject,ActionType,ActionBy,ActionDetails,ActionDate) VALUES('" & Request.QueryString("ID") & "','" & varStrSubject & "','Added Comments','" & Session("AccID").ToString & "','" & varStrMessage & "','" & Now() & "')"
            'Response.Write(varStrInsert)

            Dim InsertCmd As New Data.SqlClient.SqlCommand
            InsertCmd.Connection = objConn
            InsertCmd.CommandType = Data.CommandType.Text
            InsertCmd.CommandText = varStrInsert
            InsertCmd.ExecuteNonQuery()
            InsertCmd = Nothing

            varStrUpdate = "UPDATE dbo.tblCustomerTickets SET Priority='" & varStrPriority & "',Status='" & varStrStatus & "' WHERE TicketID='" & Request.QueryString("ID") & "'"
            'Response.Write(varStrUpdate)

            Dim UpdateCmd As New Data.SqlClient.SqlCommand
            UpdateCmd.Connection = objConn
            UpdateCmd.CommandType = Data.CommandType.Text
            UpdateCmd.CommandText = varStrUpdate
            UpdateCmd.ExecuteNonQuery()
            UpdateCmd = Nothing

            Dim varStrUserName As String = String.Empty
            Dim objCmd As New Data.SqlClient.SqlCommand("SELECT AccountName FROM dbo.tblAccounts WHERE AccountID='" & Session("AccID").ToString() & "'", objConn)
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

            Dim varStrInsertLog As String
            varStrInsertLog = "INSERT INTO dbo.tblCustomerTicketsLog(TicketID,ActionName,ActionBy,ActionDate) VALUES('" & Request.QueryString("ID") & "','Reply posted by " & Replace(varStrUserName, "'", "''") & "','" & Replace(varStrUserName, "'", "''") & "','" & Now() & "')"
            'Response.Write(varStrInsertLog)

            Dim InsertLog As New Data.SqlClient.SqlCommand
            InsertLog.Connection = objConn
            InsertLog.CommandType = Data.CommandType.Text
            InsertLog.CommandText = varStrInsertLog
            InsertLog.ExecuteNonQuery()
            InsertLog = Nothing


            Response.Write("<SCRIPT LANGUAGE='javascript'>alert('Ticket updated successfully');window.location.href='Action.aspx?ID=" & Request.QueryString("ID") & "'</script>")

        Catch ex As Exception
            'Response.Write(ex.Message)
        Finally
            If objConn.State <> Data.ConnectionState.Closed Then
                objConn.Close()
                objConn = Nothing
            End If
        End Try
    End Sub
End Class
