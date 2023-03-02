Imports MainModule
Partial Class CSMainPage
    Inherits System.Web.UI.Page
    Dim objMainModule As New MainModule
    Dim varBolGlobal As Boolean
    Protected Sub Page_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Init
        objMainModule.oConn.Open()
        If Trim(UCase(Session("Action"))) = Trim(UCase("True")) And Session("TID") <> "" Then
            AddControls(Session("TID"), Session("Forward"))
        End If
        If Session("DeptId") <> "" Then
            AddDepartments(Session("DeptID"))
            AddSearchResultTable()
        End If
    End Sub
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Request.QueryString("TID") <> "" Then
            tblView.Visible = False
            tblViewTicket.Visible = True
            GetTicketHistory(Request.QueryString("TID"))
        End If

        If Not Page.IsPostBack Then
            lblView.Text = "Open Tickets"
            DropDownView.Items(1).Selected = True
        End If
        BindData(GetTicketStatus(), "")
        BtnPrint.Attributes.Add("OnClick", "window.open('PrintTicket.aspx?TID=" & Request.QueryString("TID") & "');")
    End Sub
    Public Function GetTicketStatus() As String
        Dim varStrValue As String
        Dim varStrStatus As String
        Dim varStrTempQuery As String
        varStrValue = DropDownView.Items(DropDownView.SelectedIndex).Text.ToString
        If Trim(UCase(varStrValue)) = Trim(UCase("Open Tickets")) Then
            varStrStatus = "Open"
        ElseIf Trim(UCase(varStrValue)) = Trim(UCase("Closed Tickets")) Then
            varStrStatus = "Close"
        ElseIf Trim(UCase(varStrValue)) = Trim(UCase("Any")) Then
            varStrStatus = "Any"
        End If
        Return varStrStatus
    End Function
    Protected Sub BindData(ByVal StrStatus As String, ByVal CallFrom As String)
        Dim varStrStatus As String
        Dim varStrQueryString As String
        Dim varIntRecCount As Integer
        Dim varStrQueryCount As String
        varStrStatus = StrStatus

        If Trim(UCase(varStrStatus)) <> Trim(UCase("Any")) Then
            varStrQueryString = "SELECT C.*,A.AccountName FROM dbo.tblCustomerTickets C INNER JOIN dbo.tblAccounts A ON C.AccID=A.AccountID WHERE Status='" & varStrStatus & "'"
            varStrQueryCount = "SELECT Count(*) AS RecordCount FROM dbo.tblCustomerTickets WHERE Status='" & varStrStatus & "'"
        Else
            varStrQueryString = "SELECT C.*,A.AccountName FROM dbo.tblCustomerTickets C INNER JOIN dbo.tblAccounts A ON C.AccID=A.AccountID "
            varStrQueryCount = "SELECT Count(*) AS RecordCount FROM dbo.tblCustomerTickets "
        End If

        Dim objCmdRecCount As New Data.SqlClient.SqlCommand(varStrQueryCount, objMainModule.oConn)
        Dim objRecCount As Data.SqlClient.SqlDataReader = objCmdRecCount.ExecuteReader
        If objRecCount.HasRows Then
            While objRecCount.Read
                varIntRecCount = objRecCount(0).ToString
            End While
        End If
        objRecCount.Close()
        objRecCount = Nothing
        objCmdRecCount = Nothing

        lblTicketCount.Text = varIntRecCount

        SqlDataSourceTickets.ConnectionString = System.Configuration.ConfigurationManager.AppSettings("ETSCon")
        SqlDataSourceTickets.SelectCommand = varStrQueryString

        GridViewCustTickets.DataBind()

        Dim varIntOpenTickets As Integer
        Dim varIntCloseTickets As Integer
        Dim objCmdRecCountOpen As New Data.SqlClient.SqlCommand("SELECT Count(*) AS RecordCount FROM dbo.tblCustomerTickets WHERE Status='Open' ", objMainModule.oConn)
        Dim objRecCountOpen As Data.SqlClient.SqlDataReader = objCmdRecCountOpen.ExecuteReader
        If objRecCountOpen.HasRows Then
            While objRecCountOpen.Read
                varIntOpenTickets = objRecCountOpen(0).ToString
            End While
        End If

        objRecCountOpen.Close()
        objRecCountOpen = Nothing
        objCmdRecCountOpen = Nothing

        Dim objCmdRecCountClose As New Data.SqlClient.SqlCommand("SELECT Count(*) AS RecordCount FROM dbo.tblCustomerTickets WHERE Status='Close' AND AccID='" & Session("AccountID").ToString() & "' ", objMainModule.oConn)
        Dim objRecCountClose As Data.SqlClient.SqlDataReader = objCmdRecCountClose.ExecuteReader
        If objRecCountClose.HasRows Then
            While objRecCountClose.Read
                varIntCloseTickets = objRecCountClose(0).ToString
            End While
        End If
        objRecCountClose.Close()
        objRecCountClose = Nothing
        objCmdRecCountClose = Nothing

        lblLinkBtnCloseTickets.Text = varIntCloseTickets & " Close Tickets "
        lblLinkBtnOpenTickets.Text = varIntOpenTickets & " Open Tickets "

        If Trim(CallFrom) <> "" Then
            DropDownView.Items(DropDownView.SelectedIndex).Selected = False
            If Trim(UCase(CallFrom)) = Trim(UCase("Open")) Then
                DropDownView.Items.FindByValue("OT").Selected = True
            ElseIf Trim(UCase(CallFrom)) = Trim(UCase("Close")) Then
                DropDownView.Items.FindByValue("CT").Selected = True
            End If
        End If
    End Sub
    Protected Sub Page_Unload(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Unload
        objMainModule.oConn.Close()
        objMainModule.oConn = Nothing
    End Sub
    Protected Sub BtnAddIssueType_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtnAddIssueType.Click
        Dim varStrIssueType As String
        Dim varStrIssueDesc As String
        Dim varStrInsert As String
        Dim varBolInsertFlag As Boolean

        varStrIssueType = txtIssueName.Text.ToString
        varStrIssueDesc = txtDesc.InnerText.ToString
        varBolInsertFlag = False

        If varStrIssueType <> "" And varStrIssueDesc <> "" Then
            If CheckIssueName(varStrIssueType, "") Then
                varStrInsert = "INSERT INTO dbo.tblCustomerIssueType(IssueName,IssueDesc,DateModified,ModifiedBy)VALUES('" & varStrIssueType & "','" & varStrIssueDesc & "','" & Now() & "','" & Session("UserID") & "')"

                'Add Issueype
                Dim InsertCmd As New Data.SqlClient.SqlCommand
                InsertCmd.Connection = objMainModule.oConn
                InsertCmd.CommandType = Data.CommandType.Text
                InsertCmd.CommandText = varStrInsert
                InsertCmd.ExecuteNonQuery()
                InsertCmd = Nothing
                'End Add Issuetype
                varBolInsertFlag = True
                ClientScript.RegisterStartupScript(Me.GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Issue Name added successfully');</script>")
            Else
                ClientScript.RegisterStartupScript(Me.GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Issue Name already exists');</script>")
            End If
        End If
        If varBolInsertFlag Then
            Response.Redirect("IssueType.aspx", True)
        End If
    End Sub
    Protected Sub GridViewIssueTypes_RowDeleted(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewDeletedEventArgs) Handles GridViewIssueTypes.RowDeleted
        If Not e.Exception Is Nothing Then
            ClientScript.RegisterStartupScript(Me.GetType, "Message", "<SCRIPT LANGUAGE='javascript'>alert('" + e.Exception.Message.ToString().Replace("'", "") + "');</script>")
            e.ExceptionHandled = True
        End If
    End Sub
    Protected Sub GridViewIssueTypes_RowDeleting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewDeleteEventArgs) Handles GridViewIssueTypes.RowDeleting
        Dim varStrIssueID As String
        Dim varSQLDeleteQuery As String

        varStrIssueID = DirectCast(GridViewIssueTypes.Rows(e.RowIndex).FindControl("txtIssueID"), HiddenField).Value.ToString
        Try
            varSQLDeleteQuery = "UPDATE dbo.tblCustomerIssueType SET IsDeleted='True' WHERE IssueID='" & varStrIssueID & "'"
            sqlDataSource1.UpdateCommand = varSQLDeleteQuery
            sqlDataSource1.Update()

            ClientScript.RegisterStartupScript(Me.GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Issue Type deleted successfully');</script>")
        Catch ex As Exception

        End Try
    End Sub
    Protected Sub GridViewIssueTypes_RowEditing(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewEditEventArgs) Handles GridViewIssueTypes.RowEditing
        GridViewIssueTypes.EditIndex = e.NewEditIndex
    End Sub
    Protected Sub GridViewIssueTypes_RowUpdated(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewUpdatedEventArgs) Handles GridViewIssueTypes.RowUpdated
        If Not e.Exception Is Nothing Then
            ClientScript.RegisterStartupScript(Me.GetType, "Message", "<SCRIPT LANGUAGE='javascript'>alert('" + e.Exception.Message.ToString().Replace("'", "") + "');</script>")
            e.ExceptionHandled = True
        End If
    End Sub
    Protected Sub GridViewIssueTypes_RowUpdating(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewUpdateEventArgs) Handles GridViewIssueTypes.RowUpdating
        Dim varStrIssueName As String
        Dim varStrDesc As String
        Dim varSQLUpdateQuery As String
        Dim varStrIssueID As String

        varStrIssueID = DirectCast(GridViewIssueTypes.Rows(e.RowIndex).FindControl("txtIssueID"), HiddenField).Value.ToString
        varStrIssueName = DirectCast(GridViewIssueTypes.Rows(e.RowIndex).FindControl("txtIssueName"), TextBox).Text
        varStrDesc = DirectCast(GridViewIssueTypes.Rows(e.RowIndex).FindControl("txtIssueDesc"), TextBox).Text


        Try
            If CheckIssueName(varStrIssueName, varStrIssueID) Then
                varSQLUpdateQuery = "UPDATE dbo.tblCustomerIssueType SET IssueName='" & varStrIssueName & "',IssueDesc='" & varStrDesc & "' WHERE IssueID='" & varStrIssueID & "'"
                sqlDataSource1.UpdateCommand = varSQLUpdateQuery
                sqlDataSource1.Update()

                ClientScript.RegisterStartupScript(Me.GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Issue Type updated successfully');</script>")
            Else
                ClientScript.RegisterStartupScript(Me.GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Issue Type already exists');</script>")
            End If
        Catch ex As Exception

        End Try
    End Sub
    Public Function CheckIssueName(ByVal StrIssueName As String, ByVal IssueID As String) As Boolean
        Dim varStrQuery As String
        If IssueID <> "" Then
            varStrQuery = "SELECT IssueName FROM dbo.tblCustomerIssueType WHERE (IsDeleted IS NULL OR IsDeleted='False') AND IssueID<>'" & IssueID & "'"
        Else
            varStrQuery = "SELECT IssueName FROM dbo.tblCustomerIssueType WHERE (IsDeleted IS NULL OR IsDeleted='False')"
        End If

        Dim oCommandIssueNameCheck As New Data.SqlClient.SqlCommand(varStrQuery, objMainModule.oConn)
        Dim oRecIssueNameCheck As Data.SqlClient.SqlDataReader = oCommandIssueNameCheck.ExecuteReader
        If oRecIssueNameCheck.HasRows Then
            While oRecIssueNameCheck.Read
                If Trim(UCase(oRecIssueNameCheck.GetString(oRecIssueNameCheck.GetOrdinal("IssueName")))) = Trim(UCase(StrIssueName)) Then
                    Return False
                End If
            End While
        End If

        oRecIssueNameCheck.Close()
        oRecIssueNameCheck = Nothing
        oCommandIssueNameCheck = Nothing

        Return True
    End Function
    Protected Sub LinkBtnCloseTickets_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles LinkBtnCloseTickets.Click
        Dim varStrTStatus As String
        varStrTStatus = "Close"
        lblView.Text = varStrTStatus
        BindData(varStrTStatus, "Close")
    End Sub
    Protected Sub LinkBtnOpenTickets_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles LinkBtnOpenTickets.Click
        Dim varStrTStatus As String
        varStrTStatus = "Open"
        lblView.Text = varStrTStatus
        BindData(varStrTStatus, "Open")
    End Sub
    Protected Sub DropDownView_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles DropDownView.SelectedIndexChanged
        Dim varStrTStatus As String
        varStrTStatus = GetTicketStatus()
        lblView.Text = varStrTStatus
        BindData(varStrTStatus, "")
    End Sub
    Protected Sub GridViewCustTickets_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles GridViewCustTickets.RowCommand
        If e.CommandName = "Action" Then
            Dim varStrTicketID As String
            varStrTicketID = e.CommandArgument.ToString
            Response.Redirect("CSMainPage.aspx?TID=" & varStrTicketID & "")
        End If
    End Sub
    Protected Sub GetTicketHistory(ByVal TicketID As String)
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
        'varStrQuery = "SELECT * FROM ETS.dbo.tblCustomerTicketAction TA LEFT JOIN ETS.dbo.tblUsers U ON TA.ActionBy=U.UserID INNER JOIN ETS.dbo.tblDepartments D ON U.DepartmentID=U.DepartmentID WHERE TicketID='" & TicketID & "' AND D.Name='Support' AND DepartmentID IS NULL"
        Dim objCmdRes As New Data.SqlClient.SqlCommand("SELECT * FROM dbo.tblCustomerTicketAction WHERE TicketID='" & TicketID & "' AND ForwardDepartmentID IS NULL ORDER BY ActionDate DESC ", objMainModule.oConn)
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
                varStrQuery = "SELECT FirstName,LastName FROM dbo.tblUsers WHERE UserID='" & varArrUserName(varIntI) & "' AND IsDeleted IS NULL "
                Dim objCmd As New Data.SqlClient.SqlCommand(varStrQuery, objMainModule.oConn)
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
            ElseIf Trim(UCase(varArrActionType(varIntI))) = Trim(UCase("Added Comments")) Then
                'ConStringClient = System.Configuration.ConfigurationManager.AppSettings("ETSConClient")
                'objMainModule.oConn.ConnectionString = ConStringClient
                varStrQuery = "SELECT AccountName FROM dbo.tblAccounts WHERE AccountID='" & varArrUserName(varIntI) & "'"
                Dim objCmd As New Data.SqlClient.SqlCommand(varStrQuery, objMainModule.oConn)
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
                'varTblRow.BackColor = Drawing.Color.LightGray
                varTblRow.BackColor = Drawing.Color.WhiteSmoke
            Else
                'varTblRow.BackColor = Drawing.Color.LightBlue
            End If

            tblViewTicketHistory.Rows.Add(varTblRow)
        Next

        Dim varTblRowMainT As New TableRow
        Dim varTblCellMainFromT As New TableCell
        Dim varTblCellMainMsgT As New TableCell

        Dim objCmdMainT As New Data.SqlClient.SqlCommand("SELECT AccountName,TicketDetails,Subject,DatePosted FROM dbo.tblCustomerTickets CT INNER JOIN dbo.tblAccounts A ON CT.AccID=A.AccountID WHERE TicketID='" & TicketID & "'", objMainModule.oConn)
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
                lblTicketSubject.Text = varStrMainSubject
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
        tblViewTicketDetails.Visible = False
    End Sub
    Protected Sub BtnReply_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtnReply.Click
        Session("Forward") = False
        Session("Action") = "True"
        Session("TID") = Request.QueryString("TID")
        BtnSubmit.CommandArgument = "Reply"
        AddControls(Request.QueryString("TID"), False)
    End Sub
    Protected Sub AddControls(ByVal varStrTID As String, ByVal Flag As Boolean)
        Dim varTblRowStatus As New TableRow
        Dim varTblRowPriority As New TableRow
        Dim varTblRowSubject As New TableRow
        Dim varTblRowReply As New TableRow
        Dim varTblCellStatusLabel As New TableCell
        Dim varTblCellStatus As New TableCell
        Dim varTblCellPriorityLabel As New TableCell
        Dim varTblCellPriority As New TableCell
        Dim varTblCellSubjectLabel As New TableCell
        Dim varTblCellSubject As New TableCell
        Dim varCtrlStatusLabel As New Label
        Dim varCtrlPriorityLabel As New Label
        Dim varCtrlStatus As New System.Web.UI.WebControls.DropDownList
        Dim varCtrlPriority As New System.Web.UI.WebControls.DropDownList
        Dim varStrStatus As String
        Dim varStrPriority As String
        Dim varStrSubject As String
        Dim varIntTblWidth As Double
        Dim varIntI As Integer
        Dim varIntJ As Integer
        varIntTblWidth = 25

        Dim varTblInPlaceHolder As New Table

        Dim objTicketInfo As New Data.SqlClient.SqlCommand("SELECT Subject,Status,Priority FROM dbo.tblCustomerTickets WHERE TicketID='" & varStrTID & "'", objMainModule.oConn)
        Dim objTicketInfoRec As Data.SqlClient.SqlDataReader = objTicketInfo.ExecuteReader()
        If objTicketInfoRec.HasRows Then
            While objTicketInfoRec.Read
                varStrStatus = objTicketInfoRec.GetString(objTicketInfoRec.GetOrdinal("Status"))
                varStrPriority = objTicketInfoRec.GetString(objTicketInfoRec.GetOrdinal("Priority"))
                varStrSubject = objTicketInfoRec.GetString(objTicketInfoRec.GetOrdinal("Subject"))
            End While
        End If
        PlaceHolerID.Controls.Clear()
        objTicketInfoRec.Close()
        objTicketInfoRec = Nothing
        objTicketInfo = Nothing

        Dim varLstItemOpen As New ListItem
        Dim varLstItemClose As New ListItem
        varLstItemOpen.Text = "Open"
        varLstItemOpen.Value = "Open"

        varLstItemClose.Text = "Close"
        varLstItemClose.Value = "Close"

        varCtrlStatus.Items.Add(varLstItemOpen)
        varCtrlStatus.Items.Add(varLstItemClose)
        varCtrlStatus.Width = 120
        varCtrlStatus.ID = "DropDownStatusInDetails"
        varCtrlStatus.EnableViewState = True
        varTblRowStatus.ID = "varTblRowStatusID"

        varCtrlStatus.Font.Name = "Trebuchet MS"
        varTblCellStatus.Controls.Add(varCtrlStatus)

        varCtrlStatusLabel.Text = "Status :"
        varTblCellStatusLabel.Controls.Add(varCtrlStatusLabel)

        varTblCellStatusLabel.HorizontalAlign = HorizontalAlign.Right
        'varTblCellStatusLabel.Width = 100 Mod 25

        varTblCellStatus.HorizontalAlign = HorizontalAlign.Left
        'varTblCellStatus.Width = 100 Mod 25

        varTblRowStatus.Cells.Add(varTblCellStatusLabel)
        varTblRowStatus.Cells.Add(varTblCellStatus)

        varCtrlPriorityLabel.Text = "Priority :"
        varTblCellPriorityLabel.Controls.Add(varCtrlPriorityLabel)

        Dim varLstItemLow As New ListItem
        Dim varLstItemHigh As New ListItem
        Dim varLstItemNormal As New ListItem

        varLstItemLow.Text = "Low"
        varLstItemLow.Value = "Low"
        varLstItemHigh.Text = "High"
        varLstItemHigh.Value = "High"
        varLstItemNormal.Text = "Normal"
        varLstItemNormal.Value = "Normal"

        varCtrlPriority.Items.Add(varLstItemLow)
        varCtrlPriority.Items.Add(varLstItemNormal)
        varCtrlPriority.Items.Add(varLstItemHigh)

        varCtrlPriority.Width = 120
        varCtrlPriority.Font.Name = "Trebuchet MS"
        varCtrlPriority.ID = "DropDownPriorityInDetails"
        varCtrlPriority.EnableViewState = True
        varTblCellPriority.Controls.Add(varCtrlPriority)
        varTblCellPriorityLabel.HorizontalAlign = HorizontalAlign.Right
        'varTblCellPriorityLabel.Width = 

        varTblCellPriority.HorizontalAlign = HorizontalAlign.Left
        'varTblCellPriority.Width = 

        varTblRowStatus.Cells.Add(varTblCellPriorityLabel)
        varTblRowStatus.Cells.Add(varTblCellPriority)

        'tblViewTicketDetails.Rows.AddAt(0, varTblRowStatus)
        varTblInPlaceHolder.Rows.AddAt(0, varTblRowStatus)
        tblViewTicketDetails.Visible = True

        For varIntI = 0 To varCtrlPriority.Items.Count - 1
            If Trim(UCase(varCtrlPriority.Items(varIntI).Value.ToString)) = Trim(UCase(varStrPriority)) Then
                varCtrlPriority.Items(varIntI).Selected = True
            End If
        Next


        For varIntJ = 0 To varCtrlStatus.Items.Count - 1
            If Trim(UCase(varCtrlStatus.Items(varIntJ).Value.ToString)) = Trim(UCase(varStrStatus)) Then
                varCtrlStatus.Items(varIntJ).Selected = True
            End If
        Next
        'Response.Write(varStrStatus)

        Dim varCtrlSubjectLabel As New Label
        varCtrlSubjectLabel.Text = "Subject :"
        Dim varCtrlSubjectTextBox As New TextBox
        varCtrlSubjectTextBox.ID = "txtSubjectInDetails"
        varCtrlSubjectTextBox.EnableViewState = True

        varCtrlSubjectTextBox.Text = varStrSubject
        varCtrlSubjectTextBox.Font.Name = "Trebuchet MS"
        varCtrlSubjectTextBox.Width = 500
        varTblCellSubject.Controls.Add(varCtrlSubjectTextBox)
        varTblCellSubjectLabel.Controls.Add(varCtrlSubjectLabel)
        varTblCellSubjectLabel.HorizontalAlign = HorizontalAlign.Right
        varTblCellSubject.HorizontalAlign = HorizontalAlign.Left
        varTblCellSubject.ColumnSpan = 3
        varTblRowSubject.Cells.Add(varTblCellSubjectLabel)
        varTblRowSubject.Cells.Add(varTblCellSubject)
        'tblViewTicketDetails.Rows.AddAt(1, varTblRowSubject)
        varTblInPlaceHolder.Rows.AddAt(1, varTblRowSubject)

        Dim varTblRowMessage As New TableRow
        Dim varTblCellMessageLabel As New TableCell
        Dim varTblCellMessage As New TableCell
        Dim varCtrlMessageLabel As New Label
        Dim varCtrlMessageTextArea As New HtmlTextArea
        varCtrlMessageLabel.Text = "Message :"

        varTblCellMessageLabel.HorizontalAlign = HorizontalAlign.Right
        varTblCellMessageLabel.VerticalAlign = VerticalAlign.Top
        varTblCellMessageLabel.Controls.Add(varCtrlMessageLabel)

        varTblRowMessage.Cells.Add(varTblCellMessageLabel)
        varCtrlMessageTextArea.ID = "txtAreaMessageInDetails"
        varCtrlMessageTextArea.EnableViewState = True
        varCtrlMessageTextArea.Rows = 6
        varCtrlMessageTextArea.Cols = 70
        varTblCellMessage.Controls.Add(varCtrlMessageTextArea)
        varTblCellMessage.HorizontalAlign = HorizontalAlign.Left
        varTblCellMessage.ColumnSpan = 3

        varTblRowMessage.Cells.Add(varTblCellMessage)
        'tblViewTicketDetails.Rows.AddAt(2, varTblRowMessage)
        varTblInPlaceHolder.Rows.AddAt(2, varTblRowMessage)

        If Flag = True Or Session("Forward") Then
            Dim varTblRowForward As New TableRow
            Dim varTblCellForwardLabel As New TableCell
            Dim varTblCellForward As New TableCell
            Dim varCtrlForwardLabel As New Label
            Dim varCtrlDropDownForward As New DropDownList
            Dim varCtrlChkBoxLog As New CheckBox
            Dim varCtrlChkBoxMail As New CheckBox
            Dim varTblCellChkBoxLog As New TableCell
            Dim varTblCellChkBoxMail As New TableCell
            Dim varCtrlSpace As New Label

            varTblRowForward.Width = "30000"
            varCtrlSpace.Text = "&nbsp &nbsp "

            varCtrlDropDownForward.Font.Name = "Trebuchet MS"
            varCtrlForwardLabel.Text = "Move To :"
            varCtrlForwardLabel.EnableViewState = True
            varTblCellForwardLabel.Controls.Add(varCtrlForwardLabel)
            varTblCellForwardLabel.HorizontalAlign = HorizontalAlign.Right
            Dim objCmdDept As New Data.SqlClient.SqlCommand("SELECT DepartmentID,Name FROM dbo.tblDepartments WHERE Deleted<>'TRUE'", objMainModule.oConn)
            Dim objRecDept As Data.SqlClient.SqlDataReader = objCmdDept.ExecuteReader
            If objRecDept.HasRows Then
                While objRecDept.Read
                    Dim varLstItem As New ListItem
                    varLstItem.Text = objRecDept.GetString(objRecDept.GetOrdinal("Name"))
                    varLstItem.Value = objRecDept.GetGuid(objRecDept.GetOrdinal("DepartmentID")).ToString
                    varCtrlDropDownForward.Items.Add(varLstItem)
                End While
            End If
            objRecDept.Close()
            objRecDept = Nothing
            objCmdDept = Nothing
            varCtrlDropDownForward.ID = "DropDownDeptInDetails"
            varCtrlDropDownForward.EnableViewState = True

            varTblCellForward.Controls.Add(varCtrlDropDownForward)
            varTblCellForward.Controls.Add(varCtrlSpace)
            varCtrlChkBoxLog.ID = "ChkBoxLogInDetails"
            varCtrlChkBoxLog.Font.Name = "Trebuchet MS"
            varCtrlChkBoxLog.Text = "View Log entry to Customer"

            varCtrlChkBoxMail.ID = "ChkBoxMailInDetails"
            varCtrlChkBoxMail.Font.Name = "Trebuchet MS"
            varCtrlChkBoxMail.Text = "Send mail to Customer"

            'varTblCellForward.Controls.Add(varCtrlSpace)
            varTblCellChkBoxLog.Controls.Add(varCtrlChkBoxLog)
            'varTblCellForward.Controls.Add()
            'varTblCellForward.Controls.Add(varCtrlSpace)
            'varTblCellForward.Controls.Add(varCtrlChkBoxMail)
            varTblCellChkBoxMail.Controls.Add(varCtrlSpace)
            varTblCellChkBoxMail.Controls.Add(varCtrlChkBoxMail)

            varTblRowForward.Cells.Add(varTblCellForwardLabel)
            varTblRowForward.Cells.Add(varTblCellForward)
            varTblRowForward.Cells.Add(varTblCellChkBoxLog)
            varTblRowForward.Cells.Add(varTblCellChkBoxMail)
            varTblInPlaceHolder.Rows.AddAt(3, varTblRowForward)
            Session("Forward") = True
        End If

        Dim varTblRowAttach As New TableRow
        Dim varTblCellAttachLabel As New TableCell
        Dim varTblCellAttach As New TableCell
        Dim varCtrlFileUpload As New FileUpload
        Dim varCtrlFileUploadLabel As New Label

        varCtrlFileUploadLabel.Text = "Attach :"
        varCtrlFileUpload.Font.Name = "Trebuchet MS"
        varCtrlFileUpload.Width = 300
        varTblCellAttachLabel.Controls.Add(varCtrlFileUploadLabel)
        varTblCellAttach.Controls.Add(varCtrlFileUpload)
        varTblCellAttachLabel.HorizontalAlign = HorizontalAlign.Right
        varTblCellAttach.HorizontalAlign = HorizontalAlign.Left
        varTblCellAttach.ColumnSpan = 3
        varTblRowAttach.Cells.Add(varTblCellAttachLabel)
        varTblRowAttach.Cells.Add(varTblCellAttach)
        varCtrlFileUpload.ID = "FileUploadInDetails"
        varCtrlFileUpload.EnableViewState = True
        'tblViewTicketDetails.Rows.AddAt(3, varTblRowAttach)
        'varTblInPlaceHolder.Rows.AddAt(3, varTblRowAttach)
        BtnSubmit.Visible = True
        'varTblInPlaceHolder.GridLines = GridLines.Both
        PlaceHolerID.Controls.Add(varTblInPlaceHolder)
    End Sub
    Protected Sub BtnForward_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtnForward.Click
        Session("Action") = "True"
        Session("TID") = Request.QueryString("TID")
        BtnSubmit.CommandArgument = "Forward"
        AddControls(Request.QueryString("TID"), True)
    End Sub
    Protected Sub BtnLog_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtnLog.Click
        tblViewTicketDetails.Visible = True
        tblViewTicketDetails.Controls.Clear()
        BtnSubmit.Visible = False

        Dim varTblRowMain As New TableRow
        Dim varTblCellDateMain As New TableCell
        Dim varTblCellActionMain As New TableCell
        Dim varIntI As Integer
        varIntI = 1
        varTblCellDateMain.Width = 200
        varTblCellDateMain.Text = "Date"
        varTblCellActionMain.Text = "Action"
        varTblRowMain.CssClass = "ReportSMSelected"
        varTblRowMain.Cells.Add(varTblCellDateMain)
        varTblRowMain.Cells.Add(varTblCellActionMain)
        tblViewTicketDetails.Rows.Add(varTblRowMain)

        Dim varTblFirstRow As New TableRow
        Dim varTblFCellDate As New TableCell
        Dim varTblFCellAction As New TableCell
        Dim varStrDate As Date
        Dim varStrQuery As String
        varStrQuery = "SELECT DatePosted FROM dbo.tblCustomerTickets WHERE TicketID='" & Request.QueryString("TID") & "'"
        Dim objCmdMainLog As New Data.SqlClient.SqlCommand(varStrQuery, objMainModule.oConn)
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
        varTblFCellDate.Text = varStrDate.ToString
        varTblFCellAction.Text = "Ticket Created"

        varTblFirstRow.Cells.Add(varTblFCellDate)
        varTblFirstRow.Cells.Add(varTblFCellAction)
        varTblFirstRow.Font.Name = "Trebuchet MS"
        varTblFirstRow.Font.Size = 10

        tblViewTicketDetails.Rows.Add(varTblFirstRow)

        Dim cmdLog As New Data.SqlClient.SqlCommand("SELECT ActionName,ActionDate FROM dbo.tblCustomerTicketsLog WHERE TicketID='" & Request.QueryString("TID") & "'", objMainModule.oConn)
        Dim RecLog As Data.SqlClient.SqlDataReader = cmdLog.ExecuteReader

        If RecLog.HasRows Then
            While RecLog.Read
                varIntI = varIntI + 1
                Dim varTblRow As New TableRow
                Dim varTblDateCell As New TableCell
                Dim varTblActionCell As New TableCell
                varTblDateCell.Width = 200
                varTblDateCell.Text = RecLog.GetDateTime(RecLog.GetOrdinal("ActionDate")).ToString
                varTblActionCell.Text = RecLog.GetString(RecLog.GetOrdinal("ActionName"))
                varTblRow.Cells.Add(varTblDateCell)
                varTblRow.Cells.Add(varTblActionCell)
                If varIntI Mod 2 = 0 Then
                    varTblRow.BackColor = Drawing.Color.WhiteSmoke
                End If
                tblViewTicketDetails.Rows.Add(varTblRow)
            End While
        End If
        RecLog.Close()
        RecLog = Nothing
        cmdLog = Nothing
    End Sub
    Protected Sub BtnReloadTicketList_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtnReloadTicketList.Click
        tblViewTicketDetails.Visible = False
        Response.Redirect("CSMainPage.aspx")
    End Sub
    Protected Sub BtnSubmit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtnSubmit.Click
        Dim varStrStatus As String
        Dim varStrPriority As String
        Dim varStrSubject As String
        Dim varStrMessage As String
        Dim varStrDeptID As String
        Dim varStrLogChecked As Boolean
        Dim varStrMailChecked As Boolean
        Dim varStrFileUpload As String
        Dim varStrUpdate As String
        Dim varStrInsert As String

        Dim varCtrlDropDownStatus As New DropDownList
        Dim varCtrlDropDownPriority As New DropDownList
        Dim varCtrlDropDownDept As New DropDownList
        Dim varCtrlChkBoxLog As New CheckBox
        Dim varCtrlChkBoxMail As New CheckBox
        Dim varCtrlTextBoxSubject As New TextBox
        Dim varCtrlTextAreaMessage As New HtmlTextArea
        Dim varCtrlFileUpload As New FileUpload
        'Try
        Dim varBtnTemp As New Button
        Dim varStrCommandArgument As String
        varBtnTemp = DirectCast(sender, Button)
        varStrCommandArgument = varBtnTemp.CommandArgument.ToString()

        varCtrlDropDownStatus = PlaceHolerID.FindControl("DropDownStatusInDetails")
        varStrStatus = varCtrlDropDownStatus.Items(varCtrlDropDownStatus.SelectedIndex).Value.ToString

        varCtrlDropDownPriority = DirectCast(PlaceHolerID.FindControl("DropDownPriorityInDetails"), DropDownList)
        varStrPriority = varCtrlDropDownPriority.Items(varCtrlDropDownPriority.SelectedIndex).Value.ToString

        varCtrlTextBoxSubject = DirectCast(PlaceHolerID.FindControl("txtSubjectInDetails"), TextBox)
        varStrSubject = varCtrlTextBoxSubject.Text.ToString

        varCtrlTextAreaMessage = DirectCast(PlaceHolerID.FindControl("txtAreaMessageInDetails"), HtmlTextArea)
        varStrMessage = varCtrlTextAreaMessage.InnerText.ToString

        If Trim(UCase(varStrCommandArgument)) = Trim(UCase("Forward")) Then
            varCtrlDropDownDept = DirectCast(PlaceHolerID.FindControl("DropDownDeptInDetails"), DropDownList)
            varStrDeptID = varCtrlDropDownDept.Items(varCtrlDropDownDept.SelectedIndex).Value.ToString

            varCtrlChkBoxLog = DirectCast(PlaceHolerID.FindControl("ChkBoxLogInDetails"), CheckBox)
            varStrLogChecked = varCtrlChkBoxLog.Checked

            varCtrlChkBoxMail = DirectCast(PlaceHolerID.FindControl("ChkBoxMailInDetails"), CheckBox)
            varStrMailChecked = varCtrlChkBoxMail.Checked
        End If
        'varCtrlFileUpload = DirectCast(PlaceHolerID.FindControl("FileUploadInDetails"), FileUpload)
        'varStrFileUpload = varCtrlFileUpload.PostedFile.FileName.ToString

        If Trim(UCase(varStrCommandArgument)) = Trim(UCase("Forward")) Then
            varStrInsert = "INSERT INTO dbo.tblCustomerTicketAction (TicketID,Subject,ActionType,ActionBy,ActionDetails,ActionDate,ForwardDepartmentID) VALUES('" & Session("TID") & "','" & varStrSubject & "','Modified Ticket','" & Session("UserID") & "','" & varStrMessage & "','" & Now() & "','" & varStrDeptID & "')"
        Else
            varStrInsert = " INSERT INTO dbo.tblCustomerTicketAction (TicketID,Subject,ActionType,ActionBy,ActionDetails,ActionDate) VALUES('" & Session("TID") & "','" & varStrSubject & "','Modified Ticket','" & Session("UserID") & "','" & varStrMessage & "','" & Now() & "') "
        End If


        Dim InsertCmd As New Data.SqlClient.SqlCommand
        InsertCmd.Connection = objMainModule.oConn
        InsertCmd.CommandType = Data.CommandType.Text
        InsertCmd.CommandText = varStrInsert
        InsertCmd.ExecuteNonQuery()
        InsertCmd = Nothing

        varStrUpdate = "UPDATE dbo.tblCustomerTickets SET Priority='" & varStrPriority & "',Status='" & varStrStatus & "' WHERE TicketID='" & Session("TID") & "'"

        Dim UpdateCmd As New Data.SqlClient.SqlCommand
        UpdateCmd.Connection = objMainModule.oConn
        UpdateCmd.CommandType = Data.CommandType.Text
        UpdateCmd.CommandText = varStrUpdate
        UpdateCmd.ExecuteNonQuery()
        UpdateCmd = Nothing

        Dim varStrUserName As String
        Dim objCmd As New Data.SqlClient.SqlCommand("SELECT D.Name FROM dbo.tblDepartments D INNER JOIN dbo.tblUsers U ON U.DepartmentID=D.DepartmentID WHERE UserID='" & Session("UserID") & "'", objMainModule.oConn)
        Dim objRec As Data.SqlClient.SqlDataReader = objCmd.ExecuteReader
        If objRec.HasRows Then
            While objRec.Read
                If Not objRec.IsDBNull(objRec.GetOrdinal("Name")) Then
                    varStrUserName = objRec.GetString(objRec.GetOrdinal("Name"))
                End If
            End While
        End If
        objRec.Close()
        objRec = Nothing
        objCmd = Nothing

        Dim varStrInsertLog As String
        If Trim(UCase(varStrCommandArgument)) = Trim(UCase("Forward")) Then
            varStrInsertLog = " INSERT INTO dbo.tblCustomerTicketsLog(TicketID,ActionName,ActionBy,ActionDate,ViewLogToCustomer,SendMailToCustomer) VALUES('" & Session("TID") & "','Reply posted by " & varStrUserName & " & Department ','" & varStrUserName & "','" & Now() & "','" & varStrLogChecked & "','" & varStrMailChecked & "') "
        Else
            varStrInsertLog = " INSERT INTO dbo.tblCustomerTicketsLog(TicketID,ActionName,ActionBy,ActionDate) VALUES('" & Session("TID") & "','Reply posted by " & varStrUserName & " Department ','" & varStrUserName & "','" & Now() & "') "
        End If

        Dim InsertLog As New Data.SqlClient.SqlCommand
        InsertLog.Connection = objMainModule.oConn
        InsertLog.CommandType = Data.CommandType.Text
        InsertLog.CommandText = varStrInsertLog
        InsertLog.ExecuteNonQuery()
        InsertLog = Nothing

        ClientScript.RegisterStartupScript(Me.GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Ticket Update Sucessfully');</script>")
        Session("TID") = ""
        Session("Action") = ""
        Response.Redirect("CSMainPage.aspx")
        'Catch ex As Exception
        'MsgBox(ex.Message)
        'End Try
    End Sub
    Protected Sub CSTabContainer_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles CSTabContainer.Load
        If Not Page.IsPostBack Then
            Dim varIntI As Integer
            Dim varStartDate As Date
            Dim varEndDate As Date
            Dim varStrFMonth As Integer
            Dim varStrTMonth As Integer
            Dim varIntFDay As Integer
            Dim varIntTDay As Integer
            Dim varIntFYear As Integer
            Dim varIntTYear As Integer

            varStartDate = Now()
            varEndDate = DateAdd(DateInterval.Month, -1, varStartDate)
            'varStrFMonth = Left(MonthName(Month(varEndDate)), 3)
            'varStrTMonth = Left(MonthName(Month(varStartDate)), 3)
            varStrFMonth = Month(varEndDate)
            varStrTMonth = Month(varStartDate)
            varIntFDay = Day(varEndDate)
            varIntTDay = Day(varStartDate)
            varIntFYear = Year(varEndDate)
            varIntTYear = Year(varStartDate)
            DropDownFromMonth.Items.Clear()
            DropDownToMonth.Items.Clear()
            DropDownFromDay.Items.Clear()
            DropDownToDay.Items.Clear()
            DropDownFromYear.Items.Clear()
            DropDownToYear.Items.Clear()
            For varIntI = 1 To 12
                Dim varLstItemFrom As New ListItem
                Dim varLstItemTo As New ListItem
                varLstItemFrom.Value = varIntI
                varLstItemFrom.Text = Left(MonthName(varIntI), 3)
                varLstItemTo.Value = varIntI
                varLstItemTo.Text = Left(MonthName(varIntI), 3)
                If varLstItemFrom.Value = varStrFMonth Then
                    varLstItemFrom.Selected = True
                End If
                If varLstItemTo.Value = varStrTMonth Then
                    varLstItemTo.Selected = True
                End If
                DropDownFromMonth.Items.Add(varLstItemFrom)
                DropDownToMonth.Items.Add(varLstItemTo)
            Next

            For varIntI = 1 To 31
                Dim varLstItemFrom As New ListItem
                Dim varLstItemTo As New ListItem
                varLstItemFrom.Value = varIntI
                varLstItemFrom.Text = varIntI
                varLstItemTo.Value = varIntI
                varLstItemTo.Text = varIntI
                If varLstItemFrom.Value = varIntFDay Then
                    varLstItemFrom.Selected = True
                End If
                If varLstItemTo.Value = varIntTDay Then
                    varLstItemTo.Selected = True
                End If
                DropDownFromDay.Items.Add(varLstItemFrom)
                DropDownToDay.Items.Add(varLstItemTo)
            Next
            For varIntI = Year(Now) - 3 To Year(Now) + 3
                Dim varLstItemFrom As New ListItem
                Dim varLstItemTo As New ListItem
                varLstItemFrom.Value = varIntI
                varLstItemFrom.Text = varIntI
                varLstItemTo.Value = varIntI
                varLstItemTo.Text = varIntI
                If varLstItemFrom.Value = varIntFYear Then
                    varLstItemFrom.Selected = True
                End If
                If varLstItemTo.Value = varIntTYear Then
                    varLstItemTo.Selected = True
                End If
                DropDownFromYear.Items.Add(varLstItemFrom)
                DropDownToYear.Items.Add(varLstItemTo)
            Next

            DropDownDeptIDInSearch.Items.Clear()
            DropDownListDeptIDINTicketManagement.Items.Clear()

            Dim varLstItemZeroIndex As New ListItem
            varLstItemZeroIndex.Text = "Any"
            varLstItemZeroIndex.Value = "Any"
            DropDownDeptIDInSearch.Items.Add(varLstItemZeroIndex)
            DropDownListDeptIDINTicketManagement.Items.Add(varLstItemZeroIndex)

            Dim objCmdDept As New Data.SqlClient.SqlCommand("SELECT DepartmentID,Name FROM dbo.tblDepartments WHERE Deleted <>'TRUE'", objMainModule.oConn)
            Dim objRecDept As Data.SqlClient.SqlDataReader = objCmdDept.ExecuteReader()

            If objRecDept.HasRows Then
                While objRecDept.Read
                    Dim varLstItem As New ListItem
                    varLstItem.Value = objRecDept.GetGuid(objRecDept.GetOrdinal("DepartmentID")).ToString
                    varLstItem.Text = objRecDept.GetString(objRecDept.GetOrdinal("Name"))
                    DropDownDeptIDInSearch.Items.Add(varLstItem)
                    DropDownListDeptIDINTicketManagement.Items.Add(varLstItem)
                End While
            End If

            objRecDept.Close()
            objRecDept = Nothing
            objCmdDept = Nothing
            DropDownListDeptIDINTicketManagement.EnableViewState = True
        End If
    End Sub
    Protected Sub BtnDefault_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtnDefault.Click
        Dim varStrQuery As String
        Dim varStartDate As Date
        Dim varEndDate As Date
        Dim varIntI As Integer

        varEndDate = Now()
        varStartDate = DateAdd(DateInterval.Month, -1, varEndDate)

        varStrQuery = "SELECT TicketID,TicketNo,Subject,DatePosted,Priority,Status,DateSolved,A.AccountName FROM dbo.tblCustomerTickets T INNER JOIN dbo.tblAccounts A ON T.AccID=A.AccountID WHERE Status='Open' AND DatePosted BETWEEN '" & varStartDate & "' AND '" & varEndDate & "'"
        Dim objCmdSearch As New Data.SqlClient.SqlCommand(varStrQuery, objMainModule.oConn)
        Dim objRecSearch As Data.SqlClient.SqlDataReader = objCmdSearch.ExecuteReader

        If objRecSearch.HasRows Then
            While objRecSearch.Read
                Dim varTblRow As New TableRow
                Dim varTblCellTicketNo As New TableCell
                Dim varTblCellSubject As New TableCell
                Dim varTblCellDatePosted As New TableCell
                Dim varTblCellAccName As New TableCell
                Dim varTblCellPriority As New TableCell
                Dim varTblCellStatus As New TableCell
                Dim varTblCellAge As New TableCell
                Dim varDtSolvedDate As Date
                varTblCellTicketNo.Text = objRecSearch("TicketNo")
                If Not objRecSearch.IsDBNull(objRecSearch.GetOrdinal("Subject")) Then
                    'varTblCellSubject.Text = objRecSearch.GetString(objRecSearch.GetOrdinal("Subject"))
                    varTblCellSubject.Text = "<a href=CSMainPage.aspx?TID=" & objRecSearch.GetGuid(objRecSearch.GetOrdinal("TicketID")).ToString & ">" & objRecSearch.GetString(objRecSearch.GetOrdinal("Subject")) & "</a>"
                Else
                    varTblCellSubject.Text = "&nbsp"
                End If
                If Not objRecSearch.IsDBNull(objRecSearch.GetOrdinal("DatePosted")) Then
                    varTblCellDatePosted.Text = objRecSearch.GetDateTime(objRecSearch.GetOrdinal("DatePosted"))
                Else
                    varTblCellDatePosted.Text = "&nbsp"
                End If
                If Not objRecSearch.IsDBNull(objRecSearch.GetOrdinal("AccountName")) Then
                    varTblCellAccName.Text = objRecSearch.GetString(objRecSearch.GetOrdinal("AccountName"))
                Else
                    varTblCellAccName.Text = "&nbsp"
                End If
                If Not objRecSearch.IsDBNull(objRecSearch.GetOrdinal("Priority")) Then
                    varTblCellPriority.Text = objRecSearch.GetString(objRecSearch.GetOrdinal("Priority"))
                Else
                    varTblCellPriority.Text = "&nbsp"
                End If
                If Not objRecSearch.IsDBNull(objRecSearch.GetOrdinal("Status")) Then
                    varTblCellStatus.Text = objRecSearch.GetString(objRecSearch.GetOrdinal("Status"))
                Else
                    varTblCellStatus.Text = "&nbsp"
                End If

                If Not objRecSearch.IsDBNull(objRecSearch.GetOrdinal("DateSolved")) Then
                    varDtSolvedDate = objRecSearch.GetDateTime(objRecSearch.GetOrdinal("DateSolved"))
                Else
                    varDtSolvedDate = Now
                End If

                'varTblCellTicketNo.BorderColor = Drawing.Color.LightBlue
                'varTblCellTicketNo.BorderWidth = 1
                'varTblCellSubject.BorderColor = Drawing.Color.LightBlue
                'varTblCellSubject.BorderWidth = 1
                'varTblCellDatePosted.BorderColor = Drawing.Color.LightBlue
                'varTblCellDatePosted.BorderWidth = 1
                'varTblCellPriority.BorderColor = Drawing.Color.LightBlue
                'varTblCellPriority.BorderWidth = 1
                'varTblCellStatus.BorderColor = Drawing.Color.LightBlue
                'varTblCellStatus.BorderWidth = 1
                Dim varDblDiffDays As Double
                Dim varIntDiffDays As Integer
                Dim varIntDiffHrs As Integer

                'varIntDiffDays = DateDiff(DateInterval.Day, objRecSearch.GetDateTime(objRecSearch.GetOrdinal("DatePosted")), varDtSolvedDate)
                varIntDiffHrs = DateDiff(DateInterval.Hour, objRecSearch.GetDateTime(objRecSearch.GetOrdinal("DatePosted")), varDtSolvedDate)

                If varIntDiffHrs < 24 Then
                    varIntDiffDays = 0
                    varIntDiffHrs = varIntDiffHrs
                Else
                    varDblDiffDays = varIntDiffHrs / 24
                    'If Substring(0, varDblDiffDays.ToString().IndexOf(".")) <> "" Then
                    'If varDblDiffDays.ToString().Substring(0, varDblDiffDays.ToString().IndexOf(".")).Length > 0 Then
                    If varDblDiffDays.ToString().IndexOf(".") > 0 Then
                        varIntDiffDays = Convert.ToInt32(varDblDiffDays.ToString().Substring(0, varDblDiffDays.ToString().IndexOf(".")))
                    End If

                    'End If

                    'End If
                    varIntDiffHrs = varIntDiffHrs Mod 24
                End If

                varTblCellAge.Text = varIntDiffDays & "Days " & varIntDiffHrs & "Hrs"
                varTblRow.Cells.Add(varTblCellTicketNo)
                varTblRow.Cells.Add(varTblCellSubject)
                varTblRow.Cells.Add(varTblCellDatePosted)
                varTblRow.Cells.Add(varTblCellAccName)
                varTblRow.Cells.Add(varTblCellPriority)
                varTblRow.Cells.Add(varTblCellStatus)
                varTblRow.Cells.Add(varTblCellAge)
                If varIntI Mod 2 = 0 Then
                    varTblRow.BackColor = Drawing.Color.FloralWhite
                Else
                    varTblRow.BackColor = Drawing.Color.OldLace
                End If

                tblSearchResult.Rows.Add(varTblRow)
                varIntI = varIntI + 1
            End While
        End If
        objRecSearch.Close()
        objRecSearch = Nothing
        objCmdSearch = Nothing
    End Sub
    Protected Sub BtnSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtnSearch.Click
        Dim varStrQuery As String
        Dim varStrLookfor As String
        Dim varStrLookIn As String
        Dim varStrPriority As String
        Dim varStrDeptID As String
        Dim varDtStartDate As Date
        Dim varDtEndDate As Date
        Dim varStrStatus As String
        Dim varStrOrderBy As String
        Dim varStrDirection As String
        Dim varIntI As Integer

        Dim varStrLookforQuery As String
        Dim varStrPriorityQuery As String
        Dim varStrDatePostedQuery As String
        Dim varStrDeptQuery As String
        Dim varStrStatusQuery As String
        Dim varStrOrderByQuery As String

        varStrLookfor = Trim(txtLookfor.Text).ToString
        varStrLookIn = DropDownLookIn.Items(DropDownLookIn.SelectedIndex).Value.ToString

        If varStrLookfor <> "" And varStrLookIn <> "" Then
            If Trim(UCase(varStrLookIn)) = Trim(UCase("Any")) Then
                varStrLookforQuery = "(Subject LIKE '%" & varStrLookfor & "%' OR TicketDetails LIKE '%" & varStrLookfor & "%')"
            ElseIf Trim(UCase(varStrLookIn)) = Trim(UCase("Subject")) Then
                varStrLookforQuery = "Subject LIKE '%" & varStrLookfor & "%'"
            ElseIf Trim(UCase(varStrLookIn)) = Trim(UCase("Message")) Then
                varStrLookforQuery = "TicketDetails LIKE '%" & varStrLookfor & "%'"
            ElseIf Trim(UCase(varStrLookIn)) = Trim(UCase("AccName")) Then
                varStrLookforQuery = " A.AccountName LIKE '%" & varStrLookfor & "%'"
            End If
        Else
            varStrLookforQuery = ""
        End If

        varStrPriority = DropDownPrioritySearch.Items(DropDownPrioritySearch.SelectedIndex).Value.ToString
        If varStrPriority <> "" And Trim(UCase(varStrPriority)) <> Trim(UCase("Any")) Then
            varStrPriorityQuery = "Priority = '" & varStrPriority & "'"
        Else
            varStrPriorityQuery = ""
        End If

        varDtStartDate = DateSerial(DropDownFromYear.Items(DropDownFromYear.SelectedIndex).Value, DropDownFromMonth.Items(DropDownFromMonth.SelectedIndex).Value, DropDownFromDay.Items(DropDownFromDay.SelectedIndex).Value)
        varDtEndDate = DateSerial(DropDownToYear.Items(DropDownToYear.SelectedIndex).Value, DropDownToMonth.Items(DropDownToMonth.SelectedIndex).Value, DropDownToDay.Items(DropDownToDay.SelectedIndex).Value)


        If varDtStartDate.ToString <> "" And varDtEndDate.ToString <> "" Then
            varStrDatePostedQuery = " DatePosted >= '" & varDtStartDate & "' AND DatePosted <= '" & varDtEndDate & "'"
        Else
            varStrDatePostedQuery = ""
        End If


        varStrDeptID = DropDownDeptIDInSearch.Items(DropDownDeptIDInSearch.SelectedIndex).Value.ToString
        If varStrDeptID <> "" And Trim(UCase(varStrDeptID)) <> Trim(UCase("Any")) Then
            varStrDeptQuery = " TA.ForwardDepartmentID = '" & varStrDeptID & "'"
        Else
            varStrDeptQuery = ""
        End If

        varStrStatus = DropDownStatusInSearch.Items(DropDownStatusInSearch.SelectedIndex).Value.ToString
        If varStrStatus <> "" And Trim(UCase(varStrStatus)) <> Trim(UCase("Any")) Then
            varStrStatusQuery = "Status ='" & varStrStatus & "'"
        Else
            varStrStatus = ""
        End If

        varStrOrderBy = DropDownOrderByInSearch.Items(DropDownOrderByInSearch.SelectedIndex).Value.ToString
        varStrDirection = DropDownOrderByDirection.Items(DropDownOrderByDirection.SelectedIndex).Value.ToString
        If varStrOrderBy <> "" Then
            varStrOrderByQuery = "ORDER BY " & varStrOrderBy & " " & varStrDirection & ""
        End If

        If varStrDeptQuery = "" Then
            varStrQuery = "SELECT TicketID,TicketNo,Subject,DatePosted,Priority,Status,DateSolved,A.AccountName FROM dbo.tblCustomerTickets T INNER JOIN dbo.tblAccounts A ON T.AccID=A.AccountID WHERE T.TicketID IS NOT NULL "
        Else
            varStrQuery = "SELECT T.TicketID,TicketNo,T.Subject,DatePosted,Priority,Status,DateSolved,A.AccountName FROM dbo.tblCustomerTickets T INNER JOIN dbo.tblAccounts A ON T.AccID=A.AccountID LEFT OUTER JOIN dbo.tblCustomerTicketAction TA ON T.TicketID=TA.TicketID WHERE T.TicketID IS NOT NULL "
        End If


        If varStrLookforQuery <> "" Then
            varStrQuery = varStrQuery & " AND " & varStrLookforQuery
        End If
        If varStrPriorityQuery <> "" Then
            varStrQuery = varStrQuery & " AND " & varStrPriorityQuery
        End If
        If varStrDatePostedQuery <> "" Then
            varStrQuery = varStrQuery & " AND " & varStrDatePostedQuery
        End If
        If varStrDeptQuery <> "" Then
            varStrQuery = varStrQuery & " AND " & varStrDeptQuery
        End If
        If varStrStatusQuery <> "" Then
            varStrQuery = varStrQuery & " AND " & varStrStatusQuery
        End If
        If varStrOrderByQuery <> "" Then
            varStrQuery = varStrQuery & " " & varStrOrderByQuery
        End If

        Dim objCmdSearch As New Data.SqlClient.SqlCommand(varStrQuery, objMainModule.oConn)
        Dim objRecSearch As Data.SqlClient.SqlDataReader = objCmdSearch.ExecuteReader

        If objRecSearch.HasRows Then
            While objRecSearch.Read
                Dim varTblRow As New TableRow
                Dim varTblCellTicketNo As New TableCell
                Dim varTblCellSubject As New TableCell
                Dim varTblCellDatePosted As New TableCell
                Dim varTblCellAccName As New TableCell
                Dim varTblCellPriority As New TableCell
                Dim varTblCellStatus As New TableCell
                Dim varTblCellAge As New TableCell
                Dim varDtSolvedDate As Date
                varTblCellTicketNo.Text = objRecSearch("TicketNo")
                If Not objRecSearch.IsDBNull(objRecSearch.GetOrdinal("Subject")) Then
                    'varTblCellSubject.Text = objRecSearch.GetString(objRecSearch.GetOrdinal("Subject"))
                    varTblCellSubject.Text = "<a href=CSMainPage.aspx?TID=" & objRecSearch.GetGuid(objRecSearch.GetOrdinal("TicketID")).ToString & ">" & objRecSearch.GetString(objRecSearch.GetOrdinal("Subject")) & "</a>"
                Else
                    varTblCellSubject.Text = "&nbsp"
                End If
                If Not objRecSearch.IsDBNull(objRecSearch.GetOrdinal("DatePosted")) Then
                    varTblCellDatePosted.Text = objRecSearch.GetDateTime(objRecSearch.GetOrdinal("DatePosted"))
                Else
                    varTblCellDatePosted.Text = "&nbsp"
                End If

                If Not objRecSearch.IsDBNull(objRecSearch.GetOrdinal("AccountName")) Then
                    varTblCellAccName.Text = objRecSearch.GetString(objRecSearch.GetOrdinal("AccountName"))
                Else
                    varTblCellAccName.Text = "&nbsp"
                End If

                If Not objRecSearch.IsDBNull(objRecSearch.GetOrdinal("Priority")) Then
                    varTblCellPriority.Text = objRecSearch.GetString(objRecSearch.GetOrdinal("Priority"))
                Else
                    varTblCellPriority.Text = "&nbsp"
                End If
                If Not objRecSearch.IsDBNull(objRecSearch.GetOrdinal("Status")) Then
                    varTblCellStatus.Text = objRecSearch.GetString(objRecSearch.GetOrdinal("Status"))
                Else
                    varTblCellStatus.Text = "&nbsp"
                End If

                If Not objRecSearch.IsDBNull(objRecSearch.GetOrdinal("DateSolved")) Then
                    varDtSolvedDate = objRecSearch.GetDateTime(objRecSearch.GetOrdinal("DateSolved"))
                Else
                    varDtSolvedDate = Now
                End If

                Dim varDblDiffDays As Double
                Dim varIntDiffDays As Integer
                Dim varIntDiffHrs As Integer

                varIntDiffHrs = DateDiff(DateInterval.Hour, objRecSearch.GetDateTime(objRecSearch.GetOrdinal("DatePosted")), varDtSolvedDate)

                If varIntDiffHrs < 24 Then
                    varIntDiffDays = 0
                    varIntDiffHrs = varIntDiffHrs
                Else
                    varDblDiffDays = varIntDiffHrs / 24
                    'If varDblDiffDays.ToString().Substring(0, varDblDiffDays.ToString().IndexOf(".")).Length > 0 Then
                    If varDblDiffDays.ToString().IndexOf(".") > 0 Then
                        varIntDiffDays = Convert.ToInt32(varDblDiffDays.ToString().Substring(0, varDblDiffDays.ToString().IndexOf(".")))
                    End If
                    'End If
                    varIntDiffHrs = varIntDiffHrs Mod 24
                End If

                varTblCellAge.Text = varIntDiffDays & "Days " & varIntDiffHrs & "Hrs"

                varTblRow.Cells.Add(varTblCellTicketNo)
                varTblRow.Cells.Add(varTblCellSubject)
                varTblRow.Cells.Add(varTblCellDatePosted)
                varTblRow.Cells.Add(varTblCellAccName)
                varTblRow.Cells.Add(varTblCellPriority)
                varTblRow.Cells.Add(varTblCellStatus)
                varTblRow.Cells.Add(varTblCellAge)
                If varIntI Mod 2 = 0 Then
                    varTblRow.BackColor = Drawing.Color.FloralWhite
                Else
                    varTblRow.BackColor = Drawing.Color.OldLace
                End If

                tblSearchResult.Rows.Add(varTblRow)
                varIntI = varIntI + 1
            End While
        End If
        objRecSearch.Close()
        objRecSearch = Nothing
        objCmdSearch = Nothing
    End Sub
    Protected Sub BtnSearchTicketNo_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtnSearchTicketNo.Click
        Dim varIntTicketNo As String
        Dim varStrTicketID As String
        'Try
        varIntTicketNo = txtSearchTicketNo.Text
        If varIntTicketNo <> "" Then
            Dim objGetGUID As New Data.SqlClient.SqlCommand("SELECT TicketID FROM dbo.tblCustomerTickets WHERE TicketNo=" & varIntTicketNo & "", objMainModule.oConn)
            Dim objRecGUID As Data.SqlClient.SqlDataReader = objGetGUID.ExecuteReader
            If objRecGUID.HasRows Then
                While objRecGUID.Read
                    varStrTicketID = objRecGUID.GetGuid(objRecGUID.GetOrdinal("TicketID")).ToString
                End While
            End If
            objRecGUID.Close()
            objRecGUID = Nothing
            objGetGUID = Nothing

            If varStrTicketID <> "" Then
                Response.Redirect("CSMainPage.aspx?TID=" & varStrTicketID & "")
            End If
        End If
        'Catch ex As Exception
        'MsgBox(ex.Message)
        'End Try
    End Sub
    Protected Sub BtnSearchInTicketManagement_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtnSearchInTicketManagement.Click
        AddSearchResultTable()
    End Sub
    Protected Sub AddSearchResultTable()
        Dim varStrQuery As String
        Dim varStrLookfor As String
        Dim varStrLookIn As String
        Dim varStrDeptID As String

        Dim varStrLookforQuery As String
        Dim varStrDeptQuery As String
        Dim varIntI As Integer
        Dim varIntJ As Integer
        Dim varIntRowCountI As Integer
        'varIntJ = 0
        varIntRowCountI = tblSearchResultInTicketManagement.Rows.Count - 1
        If varIntRowCountI > 2 And Session("DeptID") <> "" Then
            For varIntJ = 2 To varIntRowCountI
                tblSearchResultInTicketManagement.Rows.RemoveAt(varIntRowCountI)

                varIntRowCountI = varIntRowCountI - 1
            Next
        ElseIf varIntRowCountI = 2 Then
            tblSearchResultInTicketManagement.Rows.RemoveAt(2)
        End If

        varStrLookfor = Trim(txtLookforInTicketManagement.Text).ToString
        varStrLookIn = DropDownLookINTicketManagement.Items(DropDownLookINTicketManagement.SelectedIndex).Value.ToString

        If varStrLookfor <> "" And varStrLookIn <> "" Then
            If Trim(UCase(varStrLookIn)) = Trim(UCase("Any")) Then
                varStrLookforQuery = " (UserName LIKE '%" & varStrLookfor & "%' OR FirstName LIKE '%" & varStrLookfor & "%' OR LastName LIKE '%" & varStrLookfor & "%') "
            ElseIf Trim(UCase(varStrLookIn)) = Trim(UCase("UserName")) Then
                varStrLookforQuery = " UserName LIKE '%" & varStrLookfor & "%' "
            ElseIf Trim(UCase(varStrLookIn)) = Trim(UCase("FirstName")) Then
                varStrLookforQuery = " FirstName LIKE '%" & varStrLookfor & "%' "
            ElseIf Trim(UCase(varStrLookIn)) = Trim(UCase("LastName")) Then
                varStrLookforQuery = " LastName LIKE '%" & varStrLookfor & "%' "
            End If
        Else
            varStrLookforQuery = ""
        End If

        varStrDeptID = DropDownListDeptIDINTicketManagement.Items(DropDownListDeptIDINTicketManagement.SelectedIndex).Value.ToString


        If varStrDeptID <> "" And Trim(UCase(varStrDeptID)) <> Trim(UCase("Any")) Then
            varStrDeptQuery = " U.DepartmentID = '" & varStrDeptID & "'"
        Else
            varStrDeptQuery = ""
        End If

        varStrQuery = "SELECT U.UserID,UserName,FirstName,LastName,Access,D.Name FROM dbo.tblUsers U INNER JOIN dbo.tblDepartments D ON U.DepartmentID=D.DepartmentID LEFT JOIN dbo.tblCIMSTicketsAccess TA ON U.UserID=TA.UserID WHERE (U.IsDeleted <> 'TRUE' OR U.IsDeleted IS NULL) "

        If varStrLookforQuery <> "" Then
            varStrQuery = varStrQuery & " AND " & varStrLookforQuery
        End If

        If varStrDeptQuery <> "" Then
            varStrQuery = varStrQuery & " AND " & varStrDeptQuery
        End If
        Dim varIntRowCount As Integer
        varIntRowCount = 2

        Dim varStrAccessStatus As String
        varStrAccessStatus = RadioButtonAccess.Items(RadioButtonAccess.SelectedIndex).Value.ToString

        Dim objCmdSearch As New Data.SqlClient.SqlCommand(varStrQuery, objMainModule.oConn)
        Dim objRecSearch As Data.SqlClient.SqlDataReader = objCmdSearch.ExecuteReader

        If objRecSearch.HasRows Then
            While objRecSearch.Read
                Dim varTblRow As New TableRow
                Dim varTblCellUserName As New TableCell
                Dim varTblCellName As New TableCell
                Dim varTblCellDepartment As New TableCell
                Dim varTblCellAccess As New TableCell
                Dim varTblCellChkBox As New TableCell
                Dim varCtrlChkBox As New CheckBox
                Dim varStrTempName As String
                Dim varStrAccess As String
                varStrAccess = ""
                'varCtrlChkBox.Checked = True
                varCtrlChkBox.AutoPostBack = False
                varCtrlChkBox.ID = "Chk_" & objRecSearch.GetGuid(objRecSearch.GetOrdinal("UserID")).ToString
                varCtrlChkBox.EnableViewState = True

                
                If Not objRecSearch.IsDBNull(objRecSearch.GetOrdinal("Access")) Then
                    varStrAccess = objRecSearch.GetString(objRecSearch.GetOrdinal("Access"))
                End If


                If varStrAccess <> "" Then
                    If Trim(UCase(varStrAccess)) = Trim(UCase("Update")) Then
                        varCtrlChkBox.Checked = True
                    ElseIf Trim(UCase(varStrAccess)) = Trim(UCase("Read")) And Trim(UCase(varStrAccessStatus)) = Trim(UCase("Read")) Then
                        varCtrlChkBox.Checked = True
                    End If
                End If

                varTblCellChkBox.Controls.Add(varCtrlChkBox)
                varTblCellChkBox.Width = 20

                If Not objRecSearch.IsDBNull(objRecSearch.GetOrdinal("UserName")) Then
                    varTblCellUserName.Text = objRecSearch.GetString(objRecSearch.GetOrdinal("UserName"))
                Else
                    varTblCellUserName.Text = "&nbsp"
                End If
                If Not objRecSearch.IsDBNull(objRecSearch.GetOrdinal("FirstName")) Then
                    varStrTempName = objRecSearch.GetString(objRecSearch.GetOrdinal("FirstName"))
                End If
                If Not objRecSearch.IsDBNull(objRecSearch.GetOrdinal("LastName")) Then
                    varStrTempName = varStrTempName & objRecSearch.GetString(objRecSearch.GetOrdinal("LastName"))
                End If
                If varStrTempName <> "" Then
                    varTblCellName.Text = varStrTempName
                Else
                    varTblCellName.Text = "&nbsp"
                End If
                If Not objRecSearch.IsDBNull(objRecSearch.GetOrdinal("Name")) Then
                    varTblCellDepartment.Text = objRecSearch.GetString(objRecSearch.GetOrdinal("Name"))
                Else
                    varTblCellDepartment.Text = "&nbsp"
                End If
                If Not objRecSearch.IsDBNull(objRecSearch.GetOrdinal("Access")) Then
                    varTblCellAccess.Text = objRecSearch.GetString(objRecSearch.GetOrdinal("Access"))
                Else
                    varTblCellAccess.Text = "&nbsp"
                End If
                varTblRow.Cells.Add(varTblCellChkBox)
                varTblRow.Cells.Add(varTblCellUserName)
                varTblRow.Cells.Add(varTblCellName)
                varTblRow.Cells.Add(varTblCellDepartment)
                varTblRow.Cells.Add(varTblCellAccess)
                If varIntI Mod 2 = 0 Then
                    varTblRow.BackColor = Drawing.Color.FloralWhite
                Else
                    varTblRow.BackColor = Drawing.Color.OldLace
                End If
                tblSearchResultInTicketManagement.Rows.AddAt(varIntRowCount, varTblRow)
                varStrTempName = ""
                varIntI = varIntI + 1
                varIntRowCount = varIntRowCount + 1
            End While
        End If
        objRecSearch.Close()
        objRecSearch = Nothing
        objCmdSearch = Nothing
        Session("DeptID") = varStrDeptID
        'varBolGlobal = False
    End Sub
    Protected Sub BtnSubmitInTicketManagement_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtnSubmitInTicketManagement.Click
        Dim varStrRadioButton As String
        Dim varCtrl As Control
        Dim varIntI As Integer
        Dim varStrTemp As String

        varStrRadioButton = RadioButtonAccess.Items(RadioButtonAccess.SelectedIndex).Value.ToString
        For varIntI = 2 To tblSearchResultInTicketManagement.Rows.Count - 1
            Dim varStrOprQuery As String
            Dim varTblRow As New TableRow
            Dim varTblCell As New TableCell
            varTblRow = tblSearchResultInTicketManagement.Rows(varIntI)
            varTblCell = varTblRow.Cells(0)
            For Each varCtrl In varTblCell.Controls

                Dim varStrUserID As String
                Dim varCtrlCheckBox As New CheckBox
                Dim varBolChk As String
                varBolChk = ""
                varCtrlCheckBox = DirectCast(varTblCell.FindControl(varCtrl.ID.ToString), CheckBox)
                varBolChk = varCtrlCheckBox.Checked
                varStrUserID = Mid(varCtrl.ID.ToString, 5)

                If varBolChk Then
                    
                    Dim objGetAccess As New Data.SqlClient.SqlCommand("SELECT Access FROM dbo.tblCIMSTicketsAccess WHERE UserID='" & varStrUserID.ToString & "'", objMainModule.oConn)
                    Dim objRecGetAccess As Data.SqlClient.SqlDataReader = objGetAccess.ExecuteReader()

                    If objRecGetAccess.HasRows Then
                        While objRecGetAccess.Read
                            varStrOprQuery = "UPDATE dbo.tblCIMSTicketsAccess SET Access='" & varStrRadioButton & "',ModifiedBy='" & Session("UserID") & "',ModifiedOn='" & Now() & "' WHERE UserID='" & varStrUserID & "'"
                        End While
                    Else
                        varStrOprQuery = "INSERT INTO dbo.tblCIMSTicketsAccess (UserID,Access,ModifiedBy,ModifiedOn) VALUES('" & varStrUserID & "','" & varStrRadioButton & "','" & Session("UserID") & "','" & Now() & "')"
                    End If
                    objRecGetAccess.Close()
                    objRecGetAccess = Nothing
                    objGetAccess = Nothing

                    Dim StrCmd As New Data.SqlClient.SqlCommand
                    StrCmd.Connection = objMainModule.oConn
                    StrCmd.CommandType = Data.CommandType.Text
                    StrCmd.CommandText = varStrOprQuery
                    StrCmd.ExecuteNonQuery()
                    StrCmd = Nothing

                End If
            Next
        Next
    End Sub
    Protected Sub AddDepartments(ByVal DeptID As String)
        Dim varLstItemZeroIndex As New ListItem
        DropDownListDeptIDINTicketManagement.Items.Clear()
        varLstItemZeroIndex.Text = "Any"
        varLstItemZeroIndex.Value = "Any"
        DropDownListDeptIDINTicketManagement.Items.Add(varLstItemZeroIndex)

        Dim objCmdDept As New Data.SqlClient.SqlCommand("SELECT DepartmentID,Name FROM dbo.tblDepartments WHERE Deleted <>'TRUE'", objMainModule.oConn)
        Dim objRecDept As Data.SqlClient.SqlDataReader = objCmdDept.ExecuteReader()

        If objRecDept.HasRows Then
            While objRecDept.Read
                Dim varLstItem As New ListItem
                varLstItem.Value = objRecDept.GetGuid(objRecDept.GetOrdinal("DepartmentID")).ToString
                varLstItem.Text = objRecDept.GetString(objRecDept.GetOrdinal("Name"))
                If Trim(UCase(DeptID)) = Trim(UCase(objRecDept.GetGuid(objRecDept.GetOrdinal("DepartmentID")).ToString)) Then
                    varLstItem.Selected = True
                End If
                DropDownListDeptIDINTicketManagement.Items.Add(varLstItem)
            End While
        End If

        objRecDept.Close()
        objRecDept = Nothing
        objCmdDept = Nothing
        Session("DeptID") = ""
    End Sub
    Protected Sub RadioButtonAccess_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles RadioButtonAccess.SelectedIndexChanged
        AddSearchResultTable()
    End Sub
    Protected Sub BtnRemoveInTicketManagement_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtnRemoveInTicketManagement.Click
        Dim varStrRadioButton As String
        Dim varCtrl As Control
        Dim varIntI As Integer

        varStrRadioButton = RadioButtonAccess.Items(RadioButtonAccess.SelectedIndex).Value.ToString
        For varIntI = 2 To tblSearchResultInTicketManagement.Rows.Count - 1
            Dim varTblRow As New TableRow
            Dim varTblCell As New TableCell
            varTblRow = tblSearchResultInTicketManagement.Rows(varIntI)
            varTblCell = varTblRow.Cells(0)
            For Each varCtrl In varTblCell.Controls

                Dim varStrUserID As String
                Dim varCtrlCheckBox As New CheckBox
                Dim varBolChk As String
                varBolChk = ""
                varCtrlCheckBox = DirectCast(varTblCell.FindControl(varCtrl.ID.ToString), CheckBox)
                varBolChk = varCtrlCheckBox.Checked
                varStrUserID = Mid(varCtrl.ID.ToString, 5)

                If varBolChk Then
                    Dim StrDelCmd As New Data.SqlClient.SqlCommand
                    StrDelCmd.Connection = objMainModule.oConn
                    StrDelCmd.CommandType = Data.CommandType.Text
                    StrDelCmd.CommandText = "DELETE dbo.tblCIMSTicketsAccess WHERE UserID='" & varStrUserID & "'"
                    StrDelCmd.ExecuteNonQuery()
                    StrDelCmd = Nothing
                End If
            Next
        Next
    End Sub
End Class
