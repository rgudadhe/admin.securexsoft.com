Imports System.Data
Imports System.Data.SqlClient
Imports System.Data.SqlDbType
Imports MainModule
Partial Class ERSSMainNew_NewTicket
    Inherits BasePage
    Dim objMainModule As New MainModule
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Not Page.IsPostBack Then
            Dim clsERSSCate As ETS.BL.ERSSIssueCategory
            Dim DS As New Data.DataSet
            Dim DV As New Data.DataView

            Try
                Dim varDropDownCateItem As New ListItem
                varDropDownCateItem.Text = "Please Select"
                varDropDownCateItem.Value = ""

                clsERSSCate = New ETS.BL.ERSSIssueCategory
                clsERSSCate.ContractorID = Session("ContractorID").ToString

                DS = clsERSSCate.getIssueCategoryList()
                DV = New Data.DataView(DS.Tables(0))
                DV.RowFilter = "(IsDeleted IS NULL OR IsDeleted=0)"

                DropDownCate.DataSource = DV
                DropDownCate.DataTextField = "CateName"
                DropDownCate.DataValueField = "CategoryID"
                DropDownCate.DataBind()
                DropDownCate.Items.Insert(0, varDropDownCateItem)
                DropDownList2.Items.Insert(0, varDropDownCateItem)
            Catch ex As Exception
                Response.Write(ex.Message)
            Finally
                clsERSSCate = Nothing
                DS.Dispose()
                DV.Dispose()
            End Try
        End If
    End Sub
    Protected Sub DropDownCate_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles DropDownCate.SelectedIndexChanged
        Dim varStrCateID As String
        Dim objDropDownList As New DropDownList
        Dim varDropDownCateItem As New ListItem

        Dim clsERSSIT As ETS.BL.ERSSIssueType
        Dim DS As Data.DataSet
        Dim DV As Data.DataView

        Try
            varDropDownCateItem.Text = "Please Select"
            varDropDownCateItem.Value = ""
            objDropDownList = UpdatePanel1.FindControl("DropDownList2")
            objDropDownList.Items.Clear()

            varStrCateID = DropDownCate.Items(DropDownCate.SelectedIndex).Value.ToString
            If varStrCateID <> "" Then
                clsERSSIT = New ETS.BL.ERSSIssueType
                clsERSSIT.ContractorID = Session("ContractorID").ToString
                clsERSSIT.CategoryID = varStrCateID.ToString
                DS = clsERSSIT.getIssueTypeList()
                DV = New Data.DataView(DS.Tables(0))
                DV.RowFilter = "(IsDeleted IS NULL OR IsDeleted=0)"

                objDropDownList.DataSource = DV
                objDropDownList.DataTextField = "IssueName"
                objDropDownList.DataValueField = "IssueID"
                objDropDownList.DataBind()

            End If
            objDropDownList.Items.Insert(0, varDropDownCateItem)
        Catch ex As Exception
            Response.Write(ex.Message)
        Finally
            clsERSSIT = Nothing
            DS.Dispose()
            DV.Dispose()
        End Try
    End Sub
    Protected Sub BtnSubmit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtnSubmit.Click
        Dim varStrEmpMailID As String = String.Empty
        Dim varIntTicketNo As Integer
        Dim varStrTID As String

        Dim varStrMailSubject As String
        Dim varStrMailMatter As String

        Dim varStrCCMail As String
        Dim varStrFromMail As String
        Dim varStrEmpName As String = String.Empty
        Dim clsERSSTicket As ETS.BL.ERSSTicket
        Dim clsERSSTicketTemp As ETS.BL.ERSSTicket
        Dim clsUser As ETS.BL.Users
        Dim DS As New Data.DataSet
        Dim DSUser As New Data.DataSet
        Dim objRecGetMailID As Data.DataTableReader
        Dim objRecGetEmpMailID As Data.DataTableReader

        Try
            Dim varStrToMail As String = String.Empty
            varStrTID = Guid.NewGuid.ToString
            clsERSSTicket = New ETS.BL.ERSSTicket
            clsUser = New ETS.BL.Users

            DS = clsERSSTicket.GetUsrsOfficalMailIDByIssueType(Request.Form("DropDownList2").ToString, Session("ContractorID").ToString, Session("WorkGroupID"))

            If DS.Tables.Count > 0 Then
                If DS.Tables(0).Rows.Count > 0 Then
                    objRecGetMailID = DS.Tables(0).CreateDataReader()
                    If objRecGetMailID.HasRows Then
                        While objRecGetMailID.Read
                            If Not objRecGetMailID.IsDBNull(objRecGetMailID.GetOrdinal("OfficialMailID")) And Not String.IsNullOrEmpty(objRecGetMailID("OfficialMailID")) Then
                                If String.IsNullOrEmpty(varStrToMail) Then
                                    varStrToMail = objRecGetMailID.GetString(objRecGetMailID.GetOrdinal("OfficialMailID"))
                                Else
                                    varStrToMail = varStrToMail & "," & objRecGetMailID.GetString(objRecGetMailID.GetOrdinal("OfficialMailID"))
                                End If
                            End If
                            'Response.Write(varStrToMail & "<BR>")
                        End While
                    End If
                    objRecGetMailID.Close()
                    If Not String.IsNullOrEmpty(varStrToMail.ToString) Then
                        DSUser = clsUser.getUserDetails(Session("UserID").ToString)
                        objRecGetEmpMailID = DSUser.Tables(0).CreateDataReader
                        If objRecGetEmpMailID.HasRows Then
                            While objRecGetEmpMailID.Read
                                If Not objRecGetEmpMailID.IsDBNull(objRecGetEmpMailID.GetOrdinal("FirstName")) Then
                                    varStrEmpName = objRecGetEmpMailID.GetString(objRecGetEmpMailID.GetOrdinal("FirstName"))
                                End If
                                If Not objRecGetEmpMailID.IsDBNull(objRecGetEmpMailID.GetOrdinal("LastName")) Then
                                    varStrEmpName = varStrEmpName & " " & objRecGetEmpMailID.GetString(objRecGetEmpMailID.GetOrdinal("LastName"))
                                End If

                                If Not objRecGetEmpMailID.IsDBNull(objRecGetEmpMailID.GetOrdinal("OfficialMailID")) Then
                                    varStrEmpMailID = objRecGetEmpMailID.GetString(objRecGetEmpMailID.GetOrdinal("OfficialMailID"))
                                End If
                                If varStrEmpMailID = String.Empty Then
                                    If Not objRecGetEmpMailID.IsDBNull(objRecGetEmpMailID.GetOrdinal("OtherMailId")) Then
                                        varStrEmpMailID = objRecGetEmpMailID.GetString(objRecGetEmpMailID.GetOrdinal("OtherMailID"))
                                    End If
                                End If
                            End While
                        End If
                        objRecGetEmpMailID.Close()

                        With clsERSSTicket
                            .TicketID = varStrTID.ToString
                            .UserID = Session("UserID").ToString
                            .IssueID = Request.Form("DropDownList2")
                            .Description = Replace(Request.Form("TextAreaIssueDetails"), "'", "''")
                            .Priority = Request.Form("DropDownPriority")
                            .Status = "Open"
                            .DatePosted = Now()
                        End With
                        If clsERSSTicket.InsertTicketDetails() = 1 Then
                            clsERSSTicketTemp = New ETS.BL.ERSSTicket
                            clsERSSTicketTemp.TicketID = varStrTID.ToString
                            clsERSSTicketTemp.getTicketsDetails()
                            varIntTicketNo = clsERSSTicketTemp.TicketNo
                        End If

                        varStrMailSubject = "ERSS - New Ticket (#" & varIntTicketNo & "#)"
                        varStrMailMatter = "<font size=2 face='" & "Trebuchet MS" & "'color=GRAY><b><I>Date Posted :- </I></b>" & Now() & " (EST)" & "<BR><BR>" & "<b><I>Action :- </I></b>Ticket Raised<BR><BR>" & "<b><I>Action By :- </I></b>" & varStrEmpName & "<BR><BR>" & "<b><I>Issue Type :- </I></b>" & DropDownList2.Items(DropDownList2.SelectedIndex).Text.ToString & "<BR><BR>" & "<BR><BR>" & "<b><I>Issue Details :- </I></b>" & Request.Form("TextAreaIssueDetails") & "<BR><BR>" & "<B>PLEASE DO NOT RESPOND TO THIS EMAIL AND ALSO IGNORE THIS MAIL IF YOU NO ACCESS TO RESPOND THE TICKET. THIS IS JUST A NOTIFICATION SO THAT YOU CAN RESPOND TO THIS ISSUE IMMEDIATELY. TO SEE DETAILS AND RESPOND TO THIS ISSUES, LOG INTO <a href=https://SECUREIT.EDICTATE.COM>SECUREIT.EDICTATE.COM</a></b></font>"

                        varStrCCMail = varStrEmpMailID
                        varStrFromMail = varStrEmpMailID

                        If objMainModule.ERSSSendMail(varStrFromMail, varStrToMail, varStrCCMail, varStrMailSubject, varStrMailMatter) Then
                            Response.Write("<center><font face=""Arial"" size=""2"" color=""#000080"">Ticket Raised Successfully !!!</font></center>")
                            Response.Write("<center><a href=""NewTicket.aspx"">Back</a></center>")
                            Response.End()
                        End If
                    Else
                        Response.Write("<center><font face=""Arial"" size=""2"" color=""#000080"">Assignment of issue type not availble,please contact to ERSS-Admin</font></center>")
                        Response.Write("<center><a href=""NewTicket.aspx"">Back</a></center>")
                        Response.End()
                    End If
                Else
                    Response.Write("<center><font face=""Arial"" size=""2"" color=""#000080"">Assignment of issue type not availble,please contact to ERSS-Admin</font></center>")
                    Response.Write("<center><a href=""NewTicket.aspx"">Back</a></center>")
                    Response.End()
                End If
            Else
                Response.Write("<center><font face=""Arial"" size=""2"" color=""#000080"">Assignment of issue type not availble,please contact to ERSS-Admin</font></center>")
                Response.Write("<center><a href=""NewTicket.aspx"">Back</a></center>")
                Response.End()
            End If
            
        Catch ex As Exception
            If Trim(UCase(ex.GetBaseException.GetType.Name)) = Trim(UCase("ThreadAbortException")) Then
                Return
            Else
                'Response.Write(ex.Message)
                'Response.End()
            End If
        Finally
            clsERSSTicket = Nothing
            clsERSSTicketTemp = Nothing
            DS.Dispose()
            DSUser.Dispose()
            objRecGetMailID = Nothing
            objRecGetEmpMailID = Nothing
        End Try
    End Sub
End Class
