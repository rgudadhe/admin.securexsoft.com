Imports MainModule
Partial Class CIMSUserPage
    Inherits System.Web.UI.Page
    Dim objMainModule As New MainModule
    Protected Sub Page_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Init
        objMainModule.oConn.Open()
        If Trim(UCase(Session("Action"))) = Trim(UCase("True")) And Session("TID") <> "" Then
            AddControls(Session("TID"))
        End If
    End Sub
    Protected Sub AddControls(ByVal varStrTID As String)
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
        PlaceHolerID.Controls.Add(varTblInPlaceHolder)
    End Sub
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Session("UserID") = "d3a37823-5cf1-4e72-b68d-3ad428cedf6d"
        If Request.QueryString("TID") <> "" Then
            tblView.Visible = False
            tblViewTicket.Visible = True
            Dim varStrAccess As String
            varStrAccess = ""
            Dim objCmdGetAccess As New Data.SqlClient.SqlCommand("SELECT Access FROM dbo.tblCIMSTicketsAccess WHERE UserID='" & Session("UserID") & "'", objMainModule.oConn)
            Dim objRecGetAccess As Data.SqlClient.SqlDataReader = objCmdGetAccess.ExecuteReader()
            If objRecGetAccess.HasRows Then
                While objRecGetAccess.Read
                    varStrAccess = objRecGetAccess.GetString(objRecGetAccess.GetOrdinal("Access"))
                End While
            End If
            objRecGetAccess.Close()
            objRecGetAccess = Nothing
            objCmdGetAccess = Nothing

            If Trim(UCase(varStrAccess)) <> Trim(UCase("Update")) Then
                BtnAction.Visible = False
            End If

            GetTicketHistory(Request.QueryString("TID"))
        End If
        If Not Page.IsPostBack Then
            lblView.Text = "Open Tickets"
            DropDownView.Items(1).Selected = True
        End If
        BindData(GetTicketStatus(), "")
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
        'varStrQuery = "SELECT * FROM dbo.tblCustomerTicketAction TA LEFT JOIN dbo.tblUsers U ON TA.ActionBy=U.UserID INNER JOIN dbo.tblDepartments D ON U.DepartmentID=U.DepartmentID WHERE TicketID='" & TicketID & "' AND D.Name='Support' AND DepartmentID IS NULL"
        Dim objCmdRes As New Data.SqlClient.SqlCommand("SELECT * FROM dbo.tblCustomerTicketAction WHERE TicketID='" & TicketID & "' ORDER BY ActionDate DESC ", objMainModule.oConn)
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
                varStrQuery = "SELECT AccountName FROM dbo.tblAccounts WHERE AccountID='" & varArrUserName(varIntI) & "'"
                Dim objCmd As New Data.SqlClient.SqlCommand(varStrQuery, objMainModule.oConn)
                Dim objRec As Data.SqlClient.SqlDataReader = objCmd.ExecuteReader
                If objRec.HasRows Then
                    While objRec.Read
                        If Not objRec.IsDBNull(objRec.GetOrdinal("AccName")) Then
                            varStrUserName = objRec.GetString(objRec.GetOrdinal("AccName"))
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
                varTblCellMainFromT.Text = objRecMainT.GetString(objRecMainT.GetOrdinal("AccName"))
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
        Dim varStrDeptID As String
        varStrStatus = StrStatus

        Dim objGetDept As New Data.SqlClient.SqlCommand("SELECT DepartmentID FROM dbo.tblUsers WHERE UserID='" & Session("UserID") & "'", objMainModule.oConn)
        Dim objRecGetDept As Data.SqlClient.SqlDataReader = objGetDept.ExecuteReader
        If objRecGetDept.HasRows Then
            While objRecGetDept.Read
                If Not objRecGetDept.IsDBNull(objRecGetDept.GetOrdinal("DepartmentID")) Then
                    varStrDeptID = objRecGetDept.GetGuid(objRecGetDept.GetOrdinal("DepartmentID")).ToString
                End If
            End While
        End If
        objRecGetDept.Close()
        objRecGetDept = Nothing
        objGetDept = Nothing

        If Trim(UCase(varStrStatus)) <> Trim(UCase("Any")) Then
            varStrQueryString = "SELECT T.TicketID,TicketNo,T.Subject,TicketDetails,DatePosted,Priority FROM dbo.tblCustomerTickets T LEFT JOIN dbo.tblCustomerTicketAction TA ON T.TicketID = TA.TicketID WHERE Status='" & varStrStatus & "' AND TA.ForwardDepartmentID='" & varStrDeptID & "'"
            'varStrQueryCount = "SELECT Count(*) AS RecordCount FROM dbo.tblCustomerTickets WHERE Status='" & varStrStatus & "' AND AccID='" & Session("AccountID") & "'"
            varStrQueryCount = "SELECT Count(*) AS RecordCount FROM dbo.tblCustomerTickets T LEFT JOIN dbo.tblCustomerTicketAction TA ON T.TicketID = TA.TicketID WHERE Status='" & varStrStatus & "' AND TA.ForwardDepartmentID='" & varStrDeptID & "'"
        Else
            varStrQueryString = "SELECT T.TicketID,TicketNo,T.Subject,TicketDetails,DatePosted,Priority FROM dbo.tblCustomerTickets T LEFT JOIN dbo.tblCustomerTicketAction TA ON T.TicketID = TA.TicketID WHERE TA.ForwardDepartmentID='" & varStrDeptID & "'"
            'varStrQueryCount = "SELECT Count(*) AS RecordCount FROM dbo.tblCustomerTickets WHERE AccID='" & Session("AccountID") & "'"
            varStrQueryCount = "SELECT Count(*) AS RecordCount FROM dbo.tblCustomerTickets T LEFT JOIN dbo.tblCustomerTicketAction TA ON T.TicketID = TA.TicketID WHERE TA.ForwardDepartmentID='" & varStrDeptID & "'"
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
        Dim objCmdRecCountOpen As New Data.SqlClient.SqlCommand("SELECT Count(*) AS RecordCount FROM dbo.tblCustomerTickets T LEFT JOIN dbo.tblCustomerTicketAction TA ON T.TicketID = TA.TicketID WHERE Status='Open' AND TA.ForwardDepartmentID='" & varStrDeptID & "'", objMainModule.oConn)
        Dim objRecCountOpen As Data.SqlClient.SqlDataReader = objCmdRecCountOpen.ExecuteReader
        If objRecCountOpen.HasRows Then
            While objRecCountOpen.Read
                varIntOpenTickets = objRecCountOpen(0).ToString
            End While
        End If

        objRecCountOpen.Close()
        objRecCountOpen = Nothing
        objCmdRecCountOpen = Nothing

        Dim objCmdRecCountClose As New Data.SqlClient.SqlCommand("SELECT Count(*) AS RecordCount FROM dbo.tblCustomerTickets T LEFT JOIN dbo.tblCustomerTicketAction TA ON T.TicketID = TA.TicketID WHERE Status='Close' AND TA.ForwardDepartmentID='" & varStrDeptID & "'", objMainModule.oConn)
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
    Protected Sub LinkBtnOpenTickets_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles LinkBtnOpenTickets.Click
        Dim varStrTStatus As String
        varStrTStatus = "Open"
        lblView.Text = varStrTStatus
        BindData(varStrTStatus, "Open")
    End Sub
    Protected Sub LinkBtnCloseTickets_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles LinkBtnCloseTickets.Click
        Dim varStrTStatus As String
        varStrTStatus = "Close"
        lblView.Text = varStrTStatus
        BindData(varStrTStatus, "Close")
    End Sub
    Protected Sub GridViewCustTickets_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles GridViewCustTickets.RowCommand
        If e.CommandName = "Action" Then
            Dim varStrTicketID As String
            varStrTicketID = e.CommandArgument.ToString
            Response.Redirect("CIMSUserPage.aspx?TID=" & varStrTicketID & "")
        End If
    End Sub
    Protected Sub BtnReloadTicketList_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtnReloadTicketList.Click
        tblViewTicketDetails.Visible = False
        Response.Redirect("CIMSUserPage.aspx")
    End Sub
    Protected Sub BtnAction_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtnAction.Click
        Session("Action") = "True"
        Session("TID") = Request.QueryString("TID")
        AddControls(Request.QueryString("TID"))
    End Sub
    Protected Sub BtnSubmit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtnSubmit.Click
        Dim varStrStatus As String
        Dim varStrPriority As String
        Dim varStrSubject As String
        Dim varStrMessage As String
        Dim varStrFileUpload As String
        Dim varStrUpdate As String
        Dim varStrInsert As String
        Dim varCtrlDropDownStatus As New DropDownList
        Dim varCtrlDropDownPriority As New DropDownList
        Dim varCtrlTextBoxSubject As New TextBox
        Dim varCtrlTextAreaMessage As New HtmlTextArea
        Dim varCtrlFileUpload As New FileUpload
        'Try

        varCtrlDropDownStatus = PlaceHolerID.FindControl("DropDownStatusInDetails")
        varStrStatus = varCtrlDropDownStatus.Items(varCtrlDropDownStatus.SelectedIndex).Value.ToString


        varCtrlDropDownPriority = DirectCast(PlaceHolerID.FindControl("DropDownPriorityInDetails"), DropDownList)
        varStrPriority = varCtrlDropDownPriority.Items(varCtrlDropDownPriority.SelectedIndex).Value.ToString

        varCtrlTextBoxSubject = DirectCast(PlaceHolerID.FindControl("txtSubjectInDetails"), TextBox)
        varStrSubject = varCtrlTextBoxSubject.Text.ToString

        varCtrlTextAreaMessage = DirectCast(PlaceHolerID.FindControl("txtAreaMessageInDetails"), HtmlTextArea)
        varStrMessage = varCtrlTextAreaMessage.InnerText.ToString

        'varCtrlFileUpload = DirectCast(PlaceHolerID.FindControl("FileUploadInDetails"), FileUpload)
        'varStrFileUpload = varCtrlFileUpload.PostedFile.FileName.ToString

        varStrInsert = "INSERT INTO dbo.tblCustomerTicketAction (TicketID,Subject,ActionType,ActionBy,ActionDetails,ActionDate) VALUES('" & Session("TID") & "','" & varStrSubject & "','Modified Ticket','" & Session("UserID") & "','" & varStrMessage & "','" & Now() & "')"

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

        ClientScript.RegisterStartupScript(Me.GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Ticket Update Sucessfully');</script>")
        Session("TID") = ""
        Session("Action") = ""
        Response.Redirect("CIMSUserPage.aspx")
    End Sub
    Protected Sub CIMSUserTabContainer_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles CIMSUserTabContainer.Load
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
        End If
    End Sub
    Protected Sub BtnDefault_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtnDefault.Click
        Dim varStrQuery As String
        Dim varStartDate As Date
        Dim varEndDate As Date
        Dim varIntI As Integer
        Dim varStrDeptID As String

        Dim objGetDept As New Data.SqlClient.SqlCommand("SELECT DepartmentID FROM dbo.tblUsers WHERE UserID='" & Session("UserID") & "'", objMainModule.oConn)
        Dim objRecGetDept As Data.SqlClient.SqlDataReader = objGetDept.ExecuteReader
        If objRecGetDept.HasRows Then
            While objRecGetDept.Read
                If Not objRecGetDept.IsDBNull(objRecGetDept.GetOrdinal("DepartmentID")) Then
                    varStrDeptID = objRecGetDept.GetGuid(objRecGetDept.GetOrdinal("DepartmentID")).ToString
                End If
            End While
        End If
        objRecGetDept.Close()
        objRecGetDept = Nothing
        objGetDept = Nothing


        varEndDate = Now()
        varStartDate = DateAdd(DateInterval.Month, -1, varEndDate)

        varStrQuery = "SELECT T.TicketID,TicketNo,T.Subject,DatePosted,Priority,Status,DateSolved FROM dbo.tblCustomerTickets T LEFT JOIN dbo.tblCustomerTicketAction TA ON T.TicketID = TA.TicketID WHERE  TA.ForwardDepartmentID='" & varStrDeptID & "' AND DatePosted BETWEEN '" & varStartDate & "' AND '" & varEndDate & "'"
        Dim objCmdSearch As New Data.SqlClient.SqlCommand(varStrQuery, objMainModule.oConn)
        Dim objRecSearch As Data.SqlClient.SqlDataReader = objCmdSearch.ExecuteReader

        If objRecSearch.HasRows Then
            While objRecSearch.Read
                Dim varTblRow As New TableRow
                Dim varTblCellTicketNo As New TableCell
                Dim varTblCellSubject As New TableCell
                Dim varTblCellDatePosted As New TableCell
                Dim varTblCellPriority As New TableCell
                Dim varTblCellStatus As New TableCell
                Dim varTblCellAge As New TableCell
                Dim varDtSolvedDate As Date
                varTblCellTicketNo.Text = objRecSearch("TicketNo")
                If Not objRecSearch.IsDBNull(objRecSearch.GetOrdinal("Subject")) Then
                    'varTblCellSubject.Text = objRecSearch.GetString(objRecSearch.GetOrdinal("Subject"))
                    varTblCellSubject.Text = "<a href=CIMsUserPage.aspx?TID=" & objRecSearch.GetGuid(objRecSearch.GetOrdinal("TicketID")).ToString & ">" & objRecSearch.GetString(objRecSearch.GetOrdinal("Subject")) & "</a>"
                Else
                    varTblCellSubject.Text = "&nbsp"
                End If
                If Not objRecSearch.IsDBNull(objRecSearch.GetOrdinal("DatePosted")) Then
                    varTblCellDatePosted.Text = objRecSearch.GetDateTime(objRecSearch.GetOrdinal("DatePosted"))
                Else
                    varTblCellDatePosted.Text = "&nbsp"
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
                    If varDblDiffDays.ToString().IndexOf(".") > 0 Then
                        varIntDiffDays = Convert.ToInt32(varDblDiffDays.ToString().Substring(0, varDblDiffDays.ToString().IndexOf(".")))
                    End If

                    varIntDiffHrs = varIntDiffHrs Mod 24
                End If

                varTblCellAge.Text = varIntDiffDays & "Days " & varIntDiffHrs & "Hrs"
                varTblRow.Cells.Add(varTblCellTicketNo)
                varTblRow.Cells.Add(varTblCellSubject)
                varTblRow.Cells.Add(varTblCellDatePosted)
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
        Dim varDtStartDate As Date
        Dim varDtEndDate As Date
        Dim varStrStatus As String
        Dim varStrOrderBy As String
        Dim varStrDirection As String
        Dim varIntI As Integer

        Dim varStrLookforQuery As String
        Dim varStrPriorityQuery As String
        Dim varStrDatePostedQuery As String
        Dim varStrStatusQuery As String
        Dim varStrOrderByQuery As String

        Dim varStrDeptID As String

        Dim objGetDept As New Data.SqlClient.SqlCommand("SELECT DepartmentID FROM dbo.tblUsers WHERE UserID='" & Session("UserID") & "'", objMainModule.oConn)
        Dim objRecGetDept As Data.SqlClient.SqlDataReader = objGetDept.ExecuteReader
        If objRecGetDept.HasRows Then
            While objRecGetDept.Read
                If Not objRecGetDept.IsDBNull(objRecGetDept.GetOrdinal("DepartmentID")) Then
                    varStrDeptID = objRecGetDept.GetGuid(objRecGetDept.GetOrdinal("DepartmentID")).ToString
                End If
            End While
        End If
        objRecGetDept.Close()
        objRecGetDept = Nothing
        objGetDept = Nothing

        varStrLookfor = Trim(txtLookfor.Text).ToString
        varStrLookIn = DropDownLookIn.Items(DropDownLookIn.SelectedIndex).Value.ToString

        If varStrLookfor <> "" And varStrLookIn <> "" Then
            If Trim(UCase(varStrLookIn)) = Trim(UCase("Any")) Then
                varStrLookforQuery = "(T.Subject LIKE '%" & varStrLookfor & "%' OR TicketDetails LIKE '%" & varStrLookfor & "%')"
            ElseIf Trim(UCase(varStrLookIn)) = Trim(UCase("Subject")) Then
                varStrLookforQuery = "T.Subject LIKE '%" & varStrLookfor & "%'"
            ElseIf Trim(UCase(varStrLookIn)) = Trim(UCase("Message")) Then
                varStrLookforQuery = "TicketDetails LIKE '%" & varStrLookfor & "%'"
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
            varStrDatePostedQuery = "DatePosted BETWEEN '" & varDtStartDate & "' AND '" & varDtEndDate & "'"
        Else
            varStrDatePostedQuery = ""
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

        varStrQuery = "SELECT T.TicketID,TicketNo,T.Subject,DatePosted,Priority,Status,DateSolved FROM dbo.tblCustomerTickets T LEFT JOIN dbo.tblCustomerTicketAction TA ON T.TicketID = TA.TicketID WHERE  TA.ForwardDepartmentID='" & varStrDeptID & "' "

        If varStrLookforQuery <> "" Then
            varStrQuery = varStrQuery & " AND " & varStrLookforQuery
        End If
        If varStrPriorityQuery <> "" Then
            varStrQuery = varStrQuery & " AND " & varStrPriorityQuery
        End If
        If varStrDatePostedQuery <> "" Then
            varStrQuery = varStrQuery & " AND " & varStrDatePostedQuery
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
                Dim varTblCellPriority As New TableCell
                Dim varTblCellStatus As New TableCell
                Dim varTblCellAge As New TableCell
                Dim varDtSolvedDate As Date
                varTblCellTicketNo.Text = objRecSearch("TicketNo")
                If Not objRecSearch.IsDBNull(objRecSearch.GetOrdinal("Subject")) Then
                    'varTblCellSubject.Text = objRecSearch.GetString(objRecSearch.GetOrdinal("Subject"))
                    varTblCellSubject.Text = "<a href=CIMSUserPage.aspx?TID=" & objRecSearch.GetGuid(objRecSearch.GetOrdinal("TicketID")).ToString & ">" & objRecSearch.GetString(objRecSearch.GetOrdinal("Subject")) & "</a>"
                Else
                    varTblCellSubject.Text = "&nbsp"
                End If
                If Not objRecSearch.IsDBNull(objRecSearch.GetOrdinal("DatePosted")) Then
                    varTblCellDatePosted.Text = objRecSearch.GetDateTime(objRecSearch.GetOrdinal("DatePosted"))
                Else
                    varTblCellDatePosted.Text = "&nbsp"
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
End Class
