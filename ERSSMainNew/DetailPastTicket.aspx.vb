Imports MainModule
Imports System.Data
Partial Class DetailPastTicket
    Inherits BasePage
    Dim objMainModule As New MainModule
    Dim varStrTicketID As String
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            Dim varStrTempSolTable As String
            Dim varStrTicketStatus As String
            Dim varTblRowDesc As New TableRow
            Dim varTblCellDesc As New TableCell
            Dim varStrTempTable As String
            Dim varTblRowComment As New TableRow
            Dim varTblCellComment As New TableCell
            Dim varTblRowSol As New TableRow
            Dim varTblCellSol As New TableCell


            Dim varStrComment(10) As String
            Dim varStrCommentTime(10) As String
            Dim varStrAction(10) As String
            Dim varStrActionTime(10) As String
            Dim varStrActionName(10) As String
            Dim i As Integer
            Dim j As Integer
            Dim a As Integer
            Dim b As Integer
            i = -1
            j = -1


            Dim varTempCommentString As String

            Dim clsERSS As ETS.BL.ERSS
            Dim DS As New Data.DataSet
            Dim DSAction As New Data.DataSet
            Dim objRecIssueDetails As DataTableReader
            Dim objRecAction As DataTableReader

            Try
                clsERSS = New ETS.BL.ERSS()
                varStrTicketID = Trim(Request.QueryString("TID")).ToString
                DSAction = clsERSS.GetERSSTicketActionDetails(varStrTicketID, Session("ContractorID").ToString)

                objRecAction = DSAction.Tables(0).CreateDataReader
                If objRecAction.HasRows Then
                    While objRecAction.Read
                        If Trim(UCase(objRecAction.GetString(objRecAction.GetOrdinal("ActionType")))) = Trim(UCase("Added Comments")) Then
                            i = i + 1
                            varStrComment(i) = objRecAction.GetString(objRecAction.GetOrdinal("Comments"))
                            varStrCommentTime(i) = objRecAction.GetDateTime(objRecAction.GetOrdinal("ActionTime")).ToString
                        ElseIf Trim(UCase(objRecAction.GetString(objRecAction.GetOrdinal("ActionType")))) = Trim(UCase("Modified Ticket")) Then
                            j = j + 1
                            varStrAction(j) = objRecAction.GetString(objRecAction.GetOrdinal("ActionDetails"))
                            varStrActionTime(j) = objRecAction.GetDateTime(objRecAction.GetOrdinal("ActionTime")).ToString
                            varStrActionName(j) = objRecAction.GetString(objRecAction.GetOrdinal("FirstName")) & " " & objRecAction.GetString(objRecAction.GetOrdinal("LastName"))
                        End If
                    End While
                End If
                objRecAction.Close()

                varStrTempSolTable = ""
                DS = clsERSS.GetERSSTicketInfoByTicketID(varStrTicketID, Session("ContractorID").ToString)

                objRecIssueDetails = DS.Tables(0).CreateDataReader()
                If objRecIssueDetails.HasRows Then
                    While objRecIssueDetails.Read
                        lblUserName.Text = objRecIssueDetails.GetString(objRecIssueDetails.GetOrdinal("UserNAme"))
                        varStrTicketStatus = objRecIssueDetails.GetString(objRecIssueDetails.GetOrdinal("Status"))

                        lblTicketNo.Text = objRecIssueDetails.GetValue(objRecIssueDetails.GetOrdinal("TicketNo")).ToString
                        lblIssueName.Text = objRecIssueDetails.GetString(objRecIssueDetails.GetOrdinal("IssueName")).ToString
                        lblDatePosted.Text = objRecIssueDetails.GetDateTime(objRecIssueDetails.GetOrdinal("DatePosted")).ToString
                        lblPriority.Text = objRecIssueDetails.GetString(objRecIssueDetails.GetOrdinal("Priority")).ToString

                        If Trim(UCase(objRecIssueDetails.GetString(objRecIssueDetails.GetOrdinal("Status")).ToString)) = Trim(UCase("Close")) Then
                            lblDateClosed.Text = objRecIssueDetails.GetDateTime(objRecIssueDetails.GetOrdinal("DateSolved")).ToString
                        End If

                        varStrTempTable = "<BR><TABLE width=85% align=center><TR><TD align=RIGHT width=20% valign=top class='alt5'><div style=text-align:right><b>Description :</b></div></TD><TD class='alt5'>" & objRecIssueDetails.GetString(objRecIssueDetails.GetOrdinal("Description")).ToString & "</TD></TR>"

                        If i >= 0 Then
                            For a = 0 To i
                                varTempCommentString = varTempCommentString & "<font face=Arial color=""#880000"">" & varStrCommentTime(a) & "</font><BR>" & varStrComment(a) & "<BR><BR>"
                            Next
                            varStrTempTable = varStrTempTable & "<TR><TD align=RIGHT width=20% valign=top rowspan=" & i & "><div style=text-align:right><b>Comments :</b></div></TD><TD>" & varTempCommentString & "</TD></TR></TABLE><BR>"
                        Else
                            varStrTempTable = varStrTempTable & "</TABLE><BR>"
                        End If


                        If j >= 0 Then
                            varStrTempSolTable = "<BR><TABLE width=85% align=center><TR style=font-family:Arial><TD width=20% align=center class=alt5><div style=text-align:center><b>Response By</b></div></TD><TD align=center class=alt5><div style=text-align:center><b>Description</b></div></TD></TR>"
                            For b = 0 To j
                                varStrTempSolTable = varStrTempSolTable & "<TR style=font-family:Arial><TD valign=top>" & varStrActionName(b) & "</TD><TD><font face=Arial color=""#880000"">" & varStrActionTime(b) & "</font><BR>" & varStrAction(b) & "</TD></TR>"
                            Next
                            varStrTempSolTable = varStrTempSolTable & "</TABLE><BR>"
                        End If

                        varTblCellDesc.Text = varStrTempTable
                        varTblRowDesc.Cells.Add(varTblCellDesc)

                        varTblRowDesc.Font.Size = 10
                        varTblRowDesc.HorizontalAlign = HorizontalAlign.Left

                        tblResponses.Rows.Add(varTblRowDesc)

                        If varStrTempSolTable <> "" Then
                            varTblCellSol.Text = varStrTempSolTable
                            varTblRowSol.Cells.Add(varTblCellSol)

                            varTblRowSol.Font.Size = 10
                            varTblRowSol.HorizontalAlign = HorizontalAlign.Left

                            tblSolutionHistory.Rows.Add(varTblRowSol)
                        End If

                    End While
                End If
                objRecIssueDetails.Close()
                If Trim(UCase(varStrTicketStatus)) = Trim(UCase("Open")) Then
                    BtnReOpen.Enabled = False
                ElseIf Trim(UCase(varStrTicketStatus)) = Trim(UCase("Close")) Then
                    BtnClose.Enabled = False
                    'btnAdd.Enabled = False
                End If
            Catch ex As Exception
                Response.Write(ex.Message)
            Finally
                clsERSS = Nothing
                objRecAction = Nothing
                objRecIssueDetails = Nothing
                DS.Dispose()
                DSAction.Dispose()
            End Try
        End If
        
    End Sub
    Protected Sub btnAdd_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAdd.Click
        Dim objConn As New Data.SqlClient.SqlConnection
        objConn = objMainModule.OpenConnection(objConn)
        Dim clsERSSTicket As ETS.BL.ERSSTicket
        Dim clsERSSTA As ETS.BL.ERSSTicketAction
        Try
            Dim varStrDeptID As Guid
            Dim varStrUserID As Guid
            Dim varStrTicketID As String
            Dim varStrTextArea As String

            varStrTextArea = Replace(TextArea1.InnerText.ToString(), "'", "''")

            If varStrTextArea <> "" Then
                varStrTicketID = Request.QueryString("TID").ToString
                clsERSSTicket = New ETS.BL.ERSSTicket()
                clsERSSTicket.TicketID = varStrTicketID
                clsERSSTicket.getTicketsDetails()

                If Not String.IsNullOrEmpty(clsERSSTicket.DepartmentID) Then
                    varStrDeptID = New Guid(clsERSSTicket.DepartmentID.ToString)
                End If

                If Not String.IsNullOrEmpty(clsERSSTicket.UserAssignID) Then
                    varStrUserID = New Guid(clsERSSTicket.UserAssignID.ToString)
                End If

                clsERSSTA = New ETS.BL.ERSSTicketAction
                With clsERSSTA
                    .TicketID = varStrTicketID.ToString
                    .ActionType = "Added Comments"
                    .ActionBy = Session("UserID").ToString
                    .Comments = varStrTextArea
                    .ActionTime = Now()
                    .Department = varStrDeptID.ToString
                    .UserAssign = varStrUserID.ToString
                End With

                Dim RetVal As Integer
                RetVal = clsERSSTA.InsertTicketActionDetails()

                If RetVal = 1 Then
                    Response.Write("<script type=""text/javascript"" language=javascript> alert(""Comments has been updated !!!!"");window.location.href='PastTickets.aspx'</script>")
                Else
                    Response.Write("<script type=""text/javascript"" language=javascript> alert(""Comments updation failed"");window.location.href='PastTickets.aspx'</script>")
                End If
            End If

        Catch ex As Exception
        Finally
            clsERSSTA = Nothing
            clsERSSTicket = Nothing
        End Try
    End Sub
    Protected Sub BtnClose_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtnClose.Click
        Dim clsERSSTicket As ETS.BL.ERSSTicket
        Try

            clsERSSTicket = New ETS.BL.ERSSTicket
            clsERSSTicket.TicketID = Request.QueryString("TID").ToString
            clsERSSTicket.Status = "Close"
            clsERSSTicket.DateSolved = Now()

            If clsERSSTicket.UpdateTicketDetails() = 1 Then
                Response.Write("<script type=""text/javascript"" language=javascript> alert(""Ticket has been closed !!!!"");window.location.href='PastTickets.aspx'</script>")
            End If

        Catch ex As Exception
        Finally
            clsERSSTicket = Nothing
        End Try
    End Sub
    Protected Sub BtnReOpen_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtnReOpen.Click
        Dim clsERSSTicket As ETS.BL.ERSSTicket
        Try

            clsERSSTicket = New ETS.BL.ERSSTicket
            clsERSSTicket.TicketID = Request.QueryString("TID").ToString
            clsERSSTicket.Status = "Open"
            clsERSSTicket.DateSolved = "NULL"

            If clsERSSTicket.UpdateTicketDetails() = 1 Then
                Response.Write("<script type=""text/javascript"" language=javascript> alert(""Ticket has been re-opened !!!!"");window.location.href='PastTickets.aspx'</script>")
            End If
        Catch ex As Exception
        Finally
            clsERSSTicket = Nothing
        End Try
    End Sub
End Class
