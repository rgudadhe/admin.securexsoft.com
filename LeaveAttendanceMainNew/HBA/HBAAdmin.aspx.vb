Partial Class LeaveAttendanceMainNew_HBA_HBAAdmin
    Inherits BasePage
    Dim objMainModule As New MainModule
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            BindHBA()
            'Table2.Visible = False
        End If
    End Sub
    Protected Sub BindHBA()
        Dim varStrTemp As String = "SELECT ISNULL(FirstName,'') +' '+ ISNULL(LastName,'') AS 'HBAName',UserID FROM DBO.tblUsers U WHERE UserName LIKE 'hba%' AND (ISDeleted IS NULL OR ISDeleted=0) AND (Active IS NULL OR Active=0)  ORDER BY FirstName,LastName "

        Dim objConn As New Data.SqlClient.SqlConnection
        objConn = objMainModule.OpenConnection(objConn)

        Dim objCmd As New Data.SqlClient.SqlCommand(varStrTemp, objConn)
        Dim objRec As Data.SqlClient.SqlDataReader = objCmd.ExecuteReader

        If objRec.HasRows Then
            While objRec.Read
                ddlHBA.Items.Add(New ListItem(objRec("HBAName").ToString, objRec("UserID").ToString))
                ddlHBA1.Items.Add(New ListItem(objRec("HBAName").ToString, objRec("UserID").ToString))
            End While
        End If

        objRec.Close()
        objRec = Nothing
        objCmd = Nothing

        ddlHBA.Items.Insert(0, New ListItem("Please Select", String.Empty))
        ddlHBA1.Items.Insert(0, New ListItem("Please Select", String.Empty))
    End Sub
    Protected Sub SendLR_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles SendLR.Click
        If Not String.IsNullOrEmpty(ddlHBA.Items(ddlHBA.SelectedIndex).Value.ToString) Then
            Dim varDtReqSDate As Date
            Dim varDtReqEDate As Date
            Dim varUserID As String = String.Empty

            Dim varFlag As Boolean
            varDtReqSDate = txtStartDate.Text.ToString
            varDtReqEDate = txtEndDate.Text.ToString
            varUserID = ddlHBA.Items(ddlHBA.SelectedIndex).Value.ToString
            Dim objConn As New Data.SqlClient.SqlConnection
            objConn = objMainModule.OpenConnection(objConn)
            Try
                Dim oCommandDateRange As New Data.SqlClient.SqlCommand("SELECT * FROM tblLeave WHERE UserID='" & varUserID & "' AND Status<>'Not Approved' AND IsDeleted is NULL", objConn)
                Dim oRecDateRange As Data.SqlClient.SqlDataReader = oCommandDateRange.ExecuteReader()
                Dim varsDate As Date
                Dim vareDate As Date

                If oRecDateRange.HasRows Then
                    While oRecDateRange.Read
                        varsDate = oRecDateRange.GetDateTime(oRecDateRange.GetOrdinal("StartDate"))
                        vareDate = oRecDateRange.GetDateTime(oRecDateRange.GetOrdinal("EndDate"))
                        While vareDate >= varsDate
                            If varsDate >= varDtReqSDate And varsDate <= varDtReqEDate Then
                                varFlag = True
                                Response.Write("<script type=""text/javascript"" language=javascript> alert(""Leave already registered for this date range"");window.location.href='HBAAdmin.aspx';</script>")
                            End If
                            varsDate = DateAdd(DateInterval.Day, 1, varsDate)
                        End While
                    End While
                End If
                oRecDateRange.Close()
                oRecDateRange = Nothing
                oCommandDateRange = Nothing
                If Not varFlag Then
                    'Server.Transfer("HBASendLeaveRequest.aspx", True)

                    Dim varDateTodaydate As Date
                    Dim varBolCheckDayLight As Boolean
                    Dim varStrDtFrom As String
                    Dim varStrDtTo As String
                    Dim varStrReason As String
                    Dim varStrInsert As String
                    Dim varStrUserName As String = String.Empty

                    varBolCheckDayLight = objMainModule.CheckDayLightSavings(Now())
                    If varBolCheckDayLight = True Then
                        varDateTodaydate = DateAdd(DateInterval.Hour, 9, Now())
                        varDateTodaydate = DateAdd(DateInterval.Minute, 30, varDateTodaydate)
                    Else
                        varDateTodaydate = DateAdd(DateInterval.Hour, 10, Now())
                        varDateTodaydate = DateAdd(DateInterval.Minute, 30, varDateTodaydate)
                    End If

                    varStrDtFrom = txtStartDate.Text.ToString
                    varStrDtTo = txtEndDate.Text.ToString
                    varStrReason = Replace(TextArea1.InnerText.ToString, "'", "''")

                    Dim oCommandEmpInfo As New Data.SqlClient.SqlCommand("SELECT UserID,FirstName,LastName,OfficialMailID,OtherMailID FROM DBO.tblUsers WHERE UserID='" & varUserID & "'", objConn)
                    Dim oRecEmpInfo As Data.SqlClient.SqlDataReader = oCommandEmpInfo.ExecuteReader()
                    Dim varMailAdd As String = String.Empty
                    Dim varMailTo As String = String.Empty
                    If oRecEmpInfo.HasRows Then
                        While oRecEmpInfo.Read
                            If Not oRecEmpInfo.IsDBNull(oRecEmpInfo.GetOrdinal("FirstName")) Then
                                varStrUserName = oRecEmpInfo.GetString(oRecEmpInfo.GetOrdinal("FirstName"))
                            End If
                            If Not oRecEmpInfo.IsDBNull(oRecEmpInfo.GetOrdinal("LastName")) Then
                                varStrUserName = varStrUserName & " " & oRecEmpInfo.GetString(oRecEmpInfo.GetOrdinal("LastName"))
                            End If
                            If Not oRecEmpInfo.IsDBNull(oRecEmpInfo.GetOrdinal("OfficialMailID")) Then
                                varMailAdd = oRecEmpInfo.GetString(oRecEmpInfo.GetOrdinal("OfficialMailID"))
                            End If
                            If Not oRecEmpInfo.IsDBNull(oRecEmpInfo.GetOrdinal("OtherMailID")) Then
                                varMailAdd = varMailAdd & "," & oRecEmpInfo.GetString(oRecEmpInfo.GetOrdinal("OtherMailID"))
                            End If
                        End While
                    End If
                    oRecEmpInfo.Close()
                    oRecEmpInfo = Nothing
                    oCommandEmpInfo = Nothing

                    varStrInsert = "INSERT INTO DBO.tblLeave(UserID,StartDate,EndDate,Reason,Status,AppDate) VALUES('" & varUserID & "','" & varStrDtFrom & "','" & varStrDtTo & "','" & varStrReason & "','Approved','" & varDateTodaydate & "')"
                    Dim InsertCmd As New Data.SqlClient.SqlCommand
                    InsertCmd.CommandType = Data.CommandType.Text
                    InsertCmd.CommandText = varStrInsert
                    InsertCmd.Connection = objConn
                    InsertCmd.ExecuteNonQuery()
                    InsertCmd = Nothing

                    Dim varMailSubject As String
                    Dim Text
                    If varMailAdd <> "" Then
                        varMailTo = objMainModule.varStrHBAToMail & "," & varMailAdd
                    Else
                        varMailTo = objMainModule.varStrHBAToMail
                    End If

                    varMailSubject = "Leave Registration of " + varStrUserName + "(HBA)"
                    Text = "<font face=""Trebuchet MS"" size=""2"" color=""#000080"">" & varStrUserName & " has register leave "
                    Text = Text & " from " & varStrDtFrom & " to " & varStrDtTo & "<br>" & "Reason: " & varStrReason & "</FONT>"
                    If objMainModule.SendMail(objMainModule.varStrHBAFromMail, varMailTo, "", varMailSubject, Text) Then
                        Response.Write("<script type=""text/javascript"" language=javascript> alert(""Leave Register Sucessfully!!!"");window.location.href='HBAAdmin.aspx';</script>")
                    End If
                End If
            Catch ex As Exception
            Finally
                If objConn.State <> Data.ConnectionState.Closed Then
                    objConn.Close()
                    objConn = Nothing
                End If
            End Try
        Else
            Response.Write("<script type=""text/javascript"" language=javascript> alert(""Please select HBA"");window.location.href='HBAAdmin.aspx';</script>")
        End If
    End Sub
    Protected Sub btnCanHBA_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCanHBA.Click
        FillData()
    End Sub
    Protected Sub FillData()
        If Not String.IsNullOrEmpty(ddlHBA1.Items(ddlHBA1.SelectedIndex).Value.ToString) Then
            Dim varHBAID As String = String.Empty
            varHBAID = ddlHBA1.Items(ddlHBA1.SelectedIndex).Value.ToString
            SQLDataSrc.ConnectionString = System.Configuration.ConfigurationManager.AppSettings("ETSCon")
            SQLDataSrc.SelectCommand = "SELECT LeaveID,StartDate,EndDate,Reason,AppDate FROM DBO.tblLeave WHERE UserID ='" & varHBAID.ToString & "' AND Status <> 'Not Approved' AND  (CancelStatus <> 'CancelRequest' or CancelStatus IS NULL or CancelStatus ='Cancel') AND (IsDeleted <>'True' or IsDeleted IS NULL) ORDER BY AppDate"
        Else
            Response.Write("<script type=""text/javascript"" language=javascript> alert(""Please select HBA"");window.location.href='HBAAdmin.aspx';</script>")
        End If
    End Sub
    Protected Sub GridViewCancel_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles GridViewCancel.PageIndexChanging
        FillData()
    End Sub
End Class
