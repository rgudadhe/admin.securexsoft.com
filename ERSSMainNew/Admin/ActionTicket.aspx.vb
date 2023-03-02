Imports MainModule
Imports System.Data
Partial Class ActionTicket
    Inherits BasePage
    Dim objMainModule As New MainModule
    Dim varStrTicketID As String
    Dim varIntNo As Integer
    Dim varStrCCEmpMail As String = String.Empty
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim varStrQuery As String
        Dim varStrActionType As String
        Dim varTicketStatus As String
        Dim varStrUserAssign As String
        Dim varStrActionDept As String
        varStrUserAssign = String.Empty
        varTicketStatus = String.Empty
        varStrActionDept = String.Empty
        Dim varStrForward As String = String.Empty
        Dim varListItem As New ListItem
        varListItem.Text = "Please Select"
        varListItem.Value = ""
        'Response.Write(Request.QueryString("From"))
        varStrForward = Request.QueryString("Forward")

        

        If Trim(UCase(Request.QueryString("From"))) = Trim(UCase("Search")) Then
            'LnkBtn.PostBackUrl = "SearchTicketResults.aspx"
            LnkBtn.PostBackUrl = "javascript:history.go(-1);"
        ElseIf Trim(UCase(Request.QueryString("From"))) = Trim(UCase("Active")) Then
            'LnkBtn.PostBackUrl = "TicketMainPage.aspx"
            LnkBtn.PostBackUrl = "javascript:history.go(-1);"
        End If

        Try
            If Not Page.IsPostBack Then
                BindIssueTypes()

                varStrTicketID = Trim(Request.QueryString("ID")).ToString
                Dim clsERSS As ETS.BL.ERSS
                Dim clsERSSTA As ETS.BL.ERSSTicketsAccess
                Dim DS As New Data.DataSet
                Dim DSAction As New Data.DataSet
                Dim objRecIssueDetails As DataTableReader
                Dim objRecActionDetails As DataTableReader

                Try
                    clsERSS = New ETS.BL.ERSS
                    DS = clsERSS.GetERSSTicketInfoByTicketID(varStrTicketID.ToString, Session("ContractorID"))
                    objRecIssueDetails = DS.Tables(0).CreateDataReader

                    If objRecIssueDetails.HasRows Then
                        While objRecIssueDetails.Read
                            lblCustName.Text = objRecIssueDetails.GetString(objRecIssueDetails.GetOrdinal("FirstName")) & " " & objRecIssueDetails.GetString(objRecIssueDetails.GetOrdinal("LastName"))
                            lblUserName.Text = objRecIssueDetails.GetString(objRecIssueDetails.GetOrdinal("UserNAme"))

                            If Not objRecIssueDetails.IsDBNull(objRecIssueDetails.GetOrdinal("OfficialMailID")) Then
                                varStrCCEmpMail = objRecIssueDetails.GetString(objRecIssueDetails.GetOrdinal("OfficialMailID"))
                            End If

                            If varStrCCEmpMail = "" Then
                                If Not objRecIssueDetails.IsDBNull(objRecIssueDetails.GetOrdinal("OtherMailID")) Then
                                    varStrCCEmpMail = objRecIssueDetails.GetString(objRecIssueDetails.GetOrdinal("OtherMailID"))
                                End If
                            End If

                            lblTicketNo.Text = objRecIssueDetails.GetValue(objRecIssueDetails.GetOrdinal("TicketNo")).ToString
                            'lblIssueName.Text = objRecIssueDetails.GetString(objRecIssueDetails.GetOrdinal("IssueName")).ToString
                            lblDatePosted.Text = objRecIssueDetails.GetDateTime(objRecIssueDetails.GetOrdinal("DatePosted")).ToString
                            lblPriority.Text = objRecIssueDetails.GetString(objRecIssueDetails.GetOrdinal("Priority")).ToString
                            tblCellIssueDetails.Text = objRecIssueDetails.GetString(objRecIssueDetails.GetOrdinal("Description")).ToString
                            varTicketStatus = objRecIssueDetails.GetString(objRecIssueDetails.GetOrdinal("Status"))
                            If Not objRecIssueDetails.IsDBNull(objRecIssueDetails.GetOrdinal("DepartmentID")) Then
                                varStrActionDept = objRecIssueDetails.GetGuid(objRecIssueDetails.GetOrdinal("DepartmentID")).ToString
                                hdnDeptID.Value = varStrActionDept.ToString
                            End If
                            If Not objRecIssueDetails.IsDBNull(objRecIssueDetails.GetOrdinal("UserAssignID")) Then
                                varStrUserAssign = objRecIssueDetails.GetGuid(objRecIssueDetails.GetOrdinal("UserAssignID")).ToString
                                hdnAssignedID.Value = varStrUserAssign.ToString
                            End If
                            If Not objRecIssueDetails.IsDBNull(objRecIssueDetails.GetOrdinal("IssueID")) Then
                                hdnIssueID.Value = objRecIssueDetails.GetGuid(objRecIssueDetails.GetOrdinal("IssueID")).ToString
                            End If
                        End While
                    End If
                    objRecIssueDetails.Close()

                    DSAction = clsERSS.GetERSSTicketActionDetailsMain(varStrTicketID.ToString, Session("ContractorID").ToString)

                    If DSAction.Tables.Count > 0 Then
                        If DSAction.Tables(0).Rows.Count > 0 Then
                            objRecActionDetails = DSAction.Tables(0).CreateDataReader
                            If objRecActionDetails.HasRows Then
                                tblResponses.Visible = True
                                While objRecActionDetails.Read
                                    Dim varTempStr As String
                                    Dim varTempStrTable As String
                                    Dim varTblRowResponse As New TableRow
                                    Dim varTblRowDesc As New TableRow
                                    Dim vartblResponseByCell As New TableCell
                                    Dim vartblResponseTimeCell As New TableCell
                                    Dim vartblResponseDescCell As New TableCell
                                    Dim varStrTempDept As String = String.Empty
                                    Dim varStrTempAssign As String = String.Empty

                                    If Not objRecActionDetails.IsDBNull(objRecActionDetails.GetOrdinal("Name")) Then
                                        varStrTempDept = objRecActionDetails.GetString(objRecActionDetails.GetOrdinal("Name"))
                                    End If

                                    If Not objRecActionDetails.IsDBNull(objRecActionDetails.GetOrdinal("UserName")) Then
                                        varStrTempAssign = objRecActionDetails.GetString(objRecActionDetails.GetOrdinal("UserName"))
                                    End If

                                    If Trim(UCase(objRecActionDetails.GetString(objRecActionDetails.GetOrdinal("ActionType")))) = Trim(UCase("Modified Ticket")) Then
                                        'If Not objRecActionDetails.IsDBNull(objRecActionDetails.GetOrdinal("UserName")) Then
                                        '    varTempStrTable = "<TABLE width=100%><TR><TD align=RIGHT width=20% valign=top>Description : </TD><TD style=color:Black>" & objRecActionDetails.GetString(objRecActionDetails.GetOrdinal("ActionDetails")).ToString & "</TD></TR><TR><TD align=RIGHT width=20% valign=top>Group Assigned : </TD><TD style=color:Black>" & objRecActionDetails.GetString(objRecActionDetails.GetOrdinal("Name")) & "</TD></TR><TR><TD align=RIGHT width=20% valign=top>User Assigned : </TD><TD style=color:Black>" & objRecActionDetails.GetString(objRecActionDetails.GetOrdinal("UserName")) & "</TD></TR></TABLE>"
                                        'Else
                                        '    varTempStrTable = "<TABLE width=100%><TR><TD align=RIGHT width=20% valign=top>Description : </TD><TD style=color:Black>" & objRecActionDetails.GetString(objRecActionDetails.GetOrdinal("ActionDetails")).ToString & "</TD></TR><TR><TD align=RIGHT width=20% valign=top>Group Assigned : </TD><TD style=color:Black>" & objRecActionDetails.GetString(objRecActionDetails.GetOrdinal("Name")) & "</TD></TR></TABLE>"
                                        'End If
                                        If varStrTempDept <> "" And varStrTempAssign <> "" Then
                                            varTempStrTable = "<TABLE width=100%><TR><TD style=border:0 align=RIGHT width=20% valign=top><div style=text-align:right>Description :</div></TD><TD style=color:Black;border:0>" & objRecActionDetails.GetString(objRecActionDetails.GetOrdinal("ActionDetails")).ToString & "</TD></TR><TR><TD style=border:0 align=RIGHT width=20% valign=top><div style=text-align:right>Group Assigned :</div></TD><TD style=color:Black;border:0>" & varStrTempDept & "</TD></TR><TR><TD style=border:0 align=RIGHT width=20% valign=top><div style=text-align:right>User Assigned :</div></TD><TD style=color:Black;border:0>" & varStrTempAssign & "</TD></TR></TABLE>"
                                        ElseIf varStrTempDept <> "" And varStrTempAssign = "" Then
                                            varTempStrTable = "<TABLE width=100%><TR><TD style=border:0 align=RIGHT width=20% valign=top><div style=text-align:right>Description :</div></TD><TD style=color:Black;border:0>" & objRecActionDetails.GetString(objRecActionDetails.GetOrdinal("ActionDetails")).ToString & "</TD></TR><TR><TD style=border:0 align=RIGHT width=20% valign=top><div style=text-align:right>Group Assigned :</div></TD><TD style=color:Black;border:0>" & varStrTempDept & "</TD></TR></TABLE>"
                                        Else
                                            varTempStrTable = "<TABLE width=100%><TR><TD style=border:0 align=RIGHT width=20% valign=top><div style=text-align:right>Description :</div></TD><TD style=color:Black;border:0>" & objRecActionDetails.GetString(objRecActionDetails.GetOrdinal("ActionDetails")).ToString & "</TD></TR></TABLE>"
                                        End If


                                        varStrActionType = "Responded By : "
                                    ElseIf Trim(UCase(objRecActionDetails.GetString(objRecActionDetails.GetOrdinal("ActionType")))) = Trim(UCase("Added Comments")) Then
                                        varTempStrTable = "<TABLE width=100%><TR><TD style=border:0 align=RIGHT width=20% valign=top><div style=text-align:right>Description :</div></TD><TD style=color:Black;border:0>" & objRecActionDetails.GetString(objRecActionDetails.GetOrdinal("Comments")).ToString & "</TD></TR></TABLE>"
                                        varStrActionType = "Comments Added : "
                                    End If

                                    varTempStr = objRecActionDetails.GetString(objRecActionDetails.GetOrdinal("FirstName")) & " " & objRecActionDetails.GetString(objRecActionDetails.GetOrdinal("LastName")) & " on " & objRecActionDetails.GetDateTime(objRecActionDetails.GetOrdinal("ActionTime")) & ""
                                    vartblResponseByCell.Text = "<b>" & varStrActionType & varTempStr & "(EST)</b>"
                                    vartblResponseByCell.ColumnSpan = 2
                                    'vartblResponseDescCell.Text = "Description : " & objRecActionDetails.GetString(objRecActionDetails.GetOrdinal("ActionDetails")).ToString
                                    vartblResponseDescCell.Text = varTempStrTable

                                    varTblRowResponse.Font.Size = 8
                                    varTblRowResponse.HorizontalAlign = HorizontalAlign.Left
                                    varTblRowResponse.Cells.Add(vartblResponseByCell)
                                    vartblResponseByCell.BackColor = Drawing.Color.LightYellow
                                    tblResponses.Rows.Add(varTblRowResponse)

                                    varTblRowDesc.Font.Size = 8
                                    varTblRowDesc.HorizontalAlign = HorizontalAlign.Left
                                    varTblRowDesc.Cells.Add(vartblResponseDescCell)
                                    tblResponses.Rows.Add(varTblRowDesc)

                                    'If varStrActionDept = "" Then
                                    '    varStrActionDept = objRecActionDetails.GetGuid(objRecActionDetails.GetOrdinal("Department")).ToString
                                    'End If
                                    'If varStrUserAssign = "" Then
                                    '    varStrUserAssign = objRecActionDetails.GetGuid(objRecActionDetails.GetOrdinal("UserAssign")).ToString
                                    'End If
                                    varTempStrTable = ""
                                    varStrActionType = ""
                                End While
                            End If
                            objRecActionDetails.Close()
                        End If
                    End If

                    BindDept()
                    DropDownForward.Items.Insert(0, varListItem)

                    If varTicketStatus <> "" Then
                        DropDownStatus.Items.FindByValue(Trim(varTicketStatus)).Selected = True
                    End If

                    If varStrActionDept <> "" Then
                        DropDownForward.Items.FindByValue(Trim(varStrActionDept)).Selected = True
                        BindDeptUsrs(varStrActionDept)
                    End If
                    If varStrUserAssign <> "" Then
                        DropDownList2.Items.FindByValue(Trim(varStrUserAssign)).Selected = True
                    Else
                        DropDownList2.Items.Insert(0, varListItem)
                    End If

                    If Not String.IsNullOrEmpty(hdnIssueID.Value.ToString) Then
                        ddlIssueType.Items.FindByValue(Trim(hdnIssueID.Value.ToString)).Selected = True
                    End If

                    'Check Access of issue type if ticket was not forwarded to particular user or department
                    If Trim(UCase(varStrForward)) = Trim(UCase("No")) Then
                        Dim varStrAccess As String
                        varStrAccess = ""
                        clsERSSTA = New ETS.BL.ERSSTicketsAccess
                        clsERSSTA.getTicketAccessDetails(varStrTicketID.ToString, Session("UserID").ToString)
                        varStrAccess = clsERSSTA.Access.ToString

                        If varStrAccess <> "" Then
                            If Trim(UCase(varStrAccess)) = Trim(UCase("Update")) Then
                                BtnSubmit.Enabled = True
                            End If
                        End If
                    End If
                    'End Access

                Catch ex As Exception
                    Response.Write(ex.Message)
                Finally
                    DS.Dispose()
                    DSAction.Dispose()
                    objRecIssueDetails = Nothing
                    objRecActionDetails = Nothing
                    clsERSS = Nothing
                    clsERSSTA = Nothing
                End Try
            End If
        Catch ex As Exception
        End Try
    End Sub
    Protected Sub DropDownForward_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles DropDownForward.SelectedIndexChanged
        Try
            DropDownList2.Items.Clear()
            Dim varStrDeptID As String
            varStrDeptID = DropDownForward.Items(DropDownForward.SelectedIndex).Value.ToString
            BindDeptUsrs(varStrDeptID)
            Dim varListItem As New ListItem
            varListItem.Text = "Please Select"
            varListItem.Value = ""
            DropDownList2.Items.Insert(0, varListItem)
        Catch ex As Exception
        End Try
    End Sub
    Protected Sub BindDept()
        Dim clsDept As ETS.BL.Department
        Dim DS As New Data.DataSet
        Try
            clsDept = New ETS.BL.Department
            'clsDept.ContractorID = Session("ContractorID").ToString
            DS = clsDept.GetDepartmentLstByWrkGroupID(Session("ContractorID"), Session("WorkGroupID"), String.Empty)
            If DS.Tables.Count > 0 Then
                If DS.Tables(0).Rows.Count > 0 Then
                    DropDownForward.DataSource = DS
                    DropDownForward.DataTextField = "Name"
                    DropDownForward.DataValueField = "DepartmentID"
                    DropDownForward.DataBind()
                End If
            End If
        Catch ex As Exception
        Finally
            DS.Dispose()
            clsDept = Nothing
        End Try
    End Sub
    Protected Sub BindDeptUsrs(ByVal varStrDeptID)
        Dim clsUsr As ETS.BL.Users
        Dim DS As New Data.DataSet
        Try
            If varStrDeptID <> "" Then
                clsUsr = New ETS.BL.Users
                Dim objDataSetDept As New DataSet

                DS = clsUsr.getUsersListByDeptName(Session("ContractorID").ToString, varStrDeptID.ToString)
                If DS.Tables.Count > 0 Then
                    If DS.Tables(0).Rows.Count > 0 Then
                        DropDownList2.DataSource = DS.Tables(0)
                        DropDownList2.DataTextField = "EName"
                        DropDownList2.DataValueField = "UserID"
                        DropDownList2.DataBind()
                    End If
                End If
            End If
        Catch ex As Exception
        Finally
            clsUsr = Nothing
            DS.Dispose()
        End Try
    End Sub
    Protected Sub BtnSubmit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtnSubmit.Click
        Dim varStrTicketID As String
        Dim varStrDeptID As String
        Dim varStrUserAssignedID As String
        Dim varStrStatus As String
        Dim varStrResponded As String
        Dim varStrActionDetails As String

        Dim varStrTicketUpdate As String = String.Empty
        Dim varStrActionInsert As String = String.Empty

        Dim objConn As New Data.SqlClient.SqlConnection
        objConn = objMainModule.OpenConnection(objConn)
        Dim clsERSSTA As ETS.BL.ERSSTicketAction
        Dim clsUsr As ETS.BL.Users
        Dim clsUsrOwner As ETS.BL.Users
        Dim clsTicket As ETS.BL.ERSSTicket
        Dim clsERSS As ETS.BL.ERSS
        Dim DS As New Data.DataSet
        Dim objRecGetMailID As DataTableReader
        Try
            varStrTicketID = Request.QueryString("ID").ToString
            varStrDeptID = Request.Form("DropDownForward")
            varStrUserAssignedID = Request.Form("DropDownList2")
            varStrStatus = Request.Form("DropDownStatus")
            varStrResponded = "Yes"
            varStrActionDetails = Replace(Request.Form("txtActionDetails").ToString, "'", "''")

            clsERSSTA = New ETS.BL.ERSSTicketAction
            clsERSSTA.TicketID = varStrTicketID
            clsERSSTA.ActionType = "Modified Ticket"
            clsERSSTA.ActionBy = Session("UserId").ToString
            clsERSSTA.ActionDetails = varStrActionDetails.ToString
            clsERSSTA.ActionTime = Now
            If varStrDeptID <> "" And varStrUserAssignedID <> "" Then
                clsERSSTA.Department = varStrDeptID
                clsERSSTA.UserAssign = varStrUserAssignedID.ToString
            ElseIf varStrDeptID <> "" And varStrUserAssignedID = "" Then
                clsERSSTA.Department = varStrDeptID
            End If
            Dim RetVal As Boolean
            RetVal = clsERSSTA.btn_Submit_Click(varStrStatus, varStrDeptID, varStrUserAssignedID, ddlIssueType.Items(ddlIssueType.SelectedIndex).Value.ToString, hdnIssueID.Value.ToString)

            If RetVal Then
                Dim varStrMailFrom As String = String.Empty
                Dim varStrEmpName As String = String.Empty
                Dim varStrDept As String = String.Empty

                clsUsr = New ETS.BL.Users(Session("UserID").ToString)
                varStrEmpName = clsUsr.FirstName
                varStrEmpName = varStrEmpName & " " & clsUsr.LastName
                varStrDept = clsUsr.DepartmentID
                varStrMailFrom = clsUsr.OfficialMailID.ToString
                If String.IsNullOrEmpty(varStrMailFrom) Then
                    varStrMailFrom = clsUsr.OtherMailID.ToString
                End If

                'Get mail Id of ticket owner

                Dim varTempUserID As String = String.Empty
                Dim varStrTOwner As String = String.Empty
                clsTicket = New ETS.BL.ERSSTicket
                clsTicket.TicketID = varStrTicketID.ToString
                clsTicket.getTicketsDetails()
                clsUsrOwner = New ETS.BL.Users(clsTicket.UserID.ToString)
                varStrTOwner = clsUsrOwner.OfficialMailID.ToString

                If String.IsNullOrEmpty(varStrTOwner) Then
                    varStrTOwner = clsUsrOwner.OtherMailID.ToString
                End If

                Dim varStrMailSubject As String = String.Empty
                Dim varStrMailMatter As String = String.Empty
                Dim varStrToMail As String = String.Empty
                Dim varStrCCMail As String = String.Empty
                Dim varStrFromMail As String = String.Empty

                clsERSS = New ETS.BL.ERSS

                If Trim(UCase(varStrStatus)) = Trim(UCase("Open")) Then
                    If Trim(UCase(varStrDeptID.ToString)) = Trim(UCase(hdnDeptID.Value.ToString)) Then
                        If Trim(UCase(varStrUserAssignedID.ToString)) = Trim(UCase(hdnAssignedID.Value.ToString)) Then
                            varStrMailSubject = "ERSS - Reply Ticket (#" & lblTicketNo.Text & "#)"
                            varStrMailMatter = "<font size=2 face='" & "Arial" & "'color=Black><b><I>Date Posted :- </I></b>" & Now() & " (EST)" & "<BR><BR>" & "<b><I>Action :- </I></b>Ticket Reply<BR><BR>" & "<b><I>Action By :- </I></b>" & varStrEmpName & "<BR><BR>" & "<b><I>Issue Type :- </I></b>" & ddlIssueType.Items(ddlIssueType.SelectedIndex).Text.ToString & "<BR><BR>" & "<BR><BR>" & "<b><I>Issue Details :- </I></b>" & varStrActionDetails & "<BR><BR>" & "<B>PLEASE DO NOT RESPOND TO THIS EMAIL AND ALSO IGNORE THIS MAIL IF YOU NO ACCESS TO RESPOND THE TICKET. THIS IS JUST A NOTIFICATION SO THAT YOU CAN RESPOND TO THIS ISSUE IMMEDIATELY. TO SEE DETAILS AND RESPOND TO THIS ISSUES, LOG INTO <a href=https://SECUREIT.EDICTATE.COM>SECUREIT.EDICTATE.COM</a></b></font>"
                            varStrToMail = varStrTOwner
                            varStrFromMail = varStrMailFrom
                        ElseIf Trim(UCase(varStrUserAssignedID.ToString)) <> Trim(UCase(hdnAssignedID.Value.ToString)) Then
                            Dim varStrSubQuery As String = String.Empty
                            If Not String.IsNullOrEmpty(varStrUserAssignedID) Then
                                varStrSubQuery = " AND UserID='" & varStrUserAssignedID & "'"
                            End If
                            DS = clsERSS.GetERSSTicketsOfficalMailID(varStrSubQuery, varStrDeptID, Session("ContractorID").ToString, Session("UserID").ToString)

                            If DS.Tables.Count > 0 Then
                                If DS.Tables(0).Rows.Count > 0 Then
                                    objRecGetMailID = DS.Tables(0).CreateDataReader
                                    If objRecGetMailID.HasRows Then
                                        While objRecGetMailID.Read
                                            If Not objRecGetMailID.IsDBNull(objRecGetMailID.GetOrdinal("OfficialMailID")) Then
                                                If String.IsNullOrEmpty(varStrToMail) Then
                                                    varStrToMail = objRecGetMailID.GetString(objRecGetMailID.GetOrdinal("OfficialMailID"))
                                                Else
                                                    varStrToMail = varStrToMail & "," & objRecGetMailID.GetString(objRecGetMailID.GetOrdinal("OfficialMailID"))
                                                End If
                                            End If
                                        End While
                                    End If
                                    objRecGetMailID.Close()
                                End If
                            End If
                            varStrMailSubject = "ERSS - Forward Ticket (#" & lblTicketNo.Text & "#) to " & DropDownForward.Items(DropDownForward.SelectedIndex).Text.ToString & " department "
                            varStrMailMatter = "<font size=2 face='" & "Arial" & "'color=Black><b><I>Date Posted :- </I></b>" & Now() & " (EST)" & "<BR><BR>" & "<b><I>Action :- </I></b>Ticket Forward<BR><BR>" & "<b><I>Action By :- </I></b>" & varStrEmpName & "<BR><BR>" & "<b><I>Issue Type :- </I></b>" & ddlIssueType.Items(ddlIssueType.SelectedIndex).Text.ToString & "<BR><BR>" & "<BR><BR>" & "<b><I>Issue Details :- </I></b>" & varStrActionDetails & "<BR><BR>" & "<B>PLEASE DO NOT RESPOND TO THIS EMAIL AND ALSO IGNORE THIS MAIL IF YOU NO ACCESS TO RESPOND THE TICKET. THIS IS JUST A NOTIFICATION SO THAT YOU CAN RESPOND TO THIS ISSUE IMMEDIATELY. TO SEE DETAILS AND RESPOND TO THIS ISSUES, LOG INTO <a href=https://SECUREIT.EDICTATE.COM>SECUREIT.EDICTATE.COM</a></b></font>"

                            varStrCCMail = varStrTOwner
                            varStrFromMail = varStrMailFrom
                        End If
                    Else
                        Dim varStrSubQuery As String = String.Empty
                        If Not String.IsNullOrEmpty(varStrUserAssignedID.ToString) Then
                            varStrSubQuery = IIf(Trim(UCase(varStrUserAssignedID.ToString)) <> Trim(UCase(hdnAssignedID.Value.ToString)), " AND UserID='" & varStrUserAssignedID & "'", String.Empty)
                        End If
                        DS = clsERSS.GetERSSTicketsOfficalMailID(varStrSubQuery, varStrDeptID, Session("ContractorID").ToString, Session("UserID").ToString)

                        If DS.Tables.Count > 0 Then
                            If DS.Tables(0).Rows.Count > 0 Then
                                objRecGetMailID = DS.Tables(0).CreateDataReader

                                If objRecGetMailID.HasRows Then
                                    While objRecGetMailID.Read
                                        If Not objRecGetMailID.IsDBNull(objRecGetMailID.GetOrdinal("OfficialMailID")) Then
                                            If String.IsNullOrEmpty(varStrToMail) Then
                                                varStrToMail = objRecGetMailID.GetString(objRecGetMailID.GetOrdinal("OfficialMailID"))
                                            Else
                                                varStrToMail = varStrToMail & "," & objRecGetMailID.GetString(objRecGetMailID.GetOrdinal("OfficialMailID"))
                                            End If
                                        End If
                                    End While
                                End If
                                objRecGetMailID.Close()
                            End If
                        End If

                        varStrMailSubject = "ERSS - Forward Ticket (#" & lblTicketNo.Text & "#) to " & DropDownForward.Items(DropDownForward.SelectedIndex).Text.ToString & " department "
                        varStrMailMatter = "<font size=2 face='" & "Arial" & "'color=Black><b><I>Date Posted :- </I></b>" & Now() & " (EST)" & "<BR><BR>" & "<b><I>Action :- </I></b>Ticket Forward<BR><BR>" & "<b><I>Action By :- </I></b>" & varStrEmpName & "<BR><BR>" & "<b><I>Issue Type :- </I></b>" & ddlIssueType.Items(ddlIssueType.SelectedIndex).Text.ToString & "<BR><BR>" & "<BR><BR>" & "<b><I>Issue Details :- </I></b>" & varStrActionDetails & "<BR><BR>" & "<B>PLEASE DO NOT RESPOND TO THIS EMAIL AND ALSO IGNORE THIS MAIL IF YOU NO ACCESS TO RESPOND THE TICKET. THIS IS JUST A NOTIFICATION SO THAT YOU CAN RESPOND TO THIS ISSUE IMMEDIATELY. TO SEE DETAILS AND RESPOND TO THIS ISSUES, LOG INTO <a href=https://SECUREIT.EDICTATE.COM>SECUREIT.EDICTATE.COM</a></b></font>"

                        varStrCCMail = varStrTOwner
                        varStrFromMail = varStrMailFrom
                    End If
                ElseIf Trim(UCase(varStrStatus)) = Trim(UCase("Close")) Then
                    varStrMailSubject = "ERSS - Ticket Closed (#" & lblTicketNo.Text & "#)"
                    varStrMailMatter = "<font size=2 face='" & "Arial" & "'color=Black><b><I>Date Posted :- </I></b>" & Now() & " (EST)" & "<BR><BR>" & "<b><I>Action :- </I></b>Ticket Closed<BR><BR>" & "<b><I>Action By :- </I></b>" & varStrEmpName & "<BR><BR>" & "<b><I>Issue Type :- </I></b>" & ddlIssueType.Items(ddlIssueType.SelectedIndex).Text.ToString & "<BR><BR>" & "<BR><BR>" & "<b><I>Issue Details :- </I></b>" & varStrActionDetails & "<BR><BR>" & "<B>PLEASE DO NOT RESPOND TO THIS EMAIL AND ALSO IGNORE THIS MAIL IF YOU NO ACCESS TO RESPOND THE TICKET. THIS IS JUST A NOTIFICATION SO THAT YOU CAN RESPOND TO THIS ISSUE IMMEDIATELY. TO SEE DETAILS AND RESPOND TO THIS ISSUES, LOG INTO <a href=https://SECUREIT.EDICTATE.COM>SECUREIT.EDICTATE.COM</a></b></font>"
                    varStrToMail = varStrTOwner
                    varStrFromMail = varStrMailFrom
                End If

                If Not String.IsNullOrEmpty(varStrToMail) Then
                    If objMainModule.ERSSSendMail(varStrFromMail, varStrToMail, varStrCCMail, varStrMailSubject, varStrMailMatter) Then
                        Response.Write("<script type=""text/javascript"" language=javascript> alert(""Ticket has been updated sucessfully !!!"");history.go(-2);</script>")
                    End If
                Else
                    Response.Write("<script type=""text/javascript"" language=javascript> alert(""Ticket has been updated sucessfully !!!"");history.go(-2);</script>")
                End If
            End If
        Catch ex As Exception
            Response.Write(ex.Message)
        Finally
            If objConn.State <> ConnectionState.Closed Then
                objConn.Close()
                objConn = Nothing
            End If
            DS.Dispose()
            clsUsr = Nothing
            clsERSSTA = Nothing
            clsUsrOwner = Nothing
            objRecGetMailID = Nothing
        End Try
    End Sub
    Protected Sub BindIssueTypes()
        Dim clsERSSIT As ETS.BL.ERSSIssueType
        Dim DS As New Data.DataSet
        Dim DV As Data.DataView
        Try
            clsERSSIT = New ETS.BL.ERSSIssueType
            DS = clsERSSIT.getIssueTypeList()
            If DS.Tables.Count > 0 Then
                If DS.Tables(0).Rows.Count > 0 Then
                    DV = New Data.DataView(DS.Tables(0))
                    DV.RowFilter = "(IsDeleted IS NULL OR IsDeleted=0)"
                    ddlIssueType.DataSource = DV
                    ddlIssueType.DataTextField = "IssueName"
                    ddlIssueType.DataValueField = "IssueID"
                    ddlIssueType.DataBind()
                End If
            End If
        Catch ex As Exception
        Finally
            DS.Dispose()
            DV.Dispose()
            clsERSSIT = Nothing
        End Try
    End Sub
End Class
