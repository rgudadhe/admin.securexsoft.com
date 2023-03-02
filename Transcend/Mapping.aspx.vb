
Partial Class Transcend_Mapping
    Inherits BasePage
    Protected Sub Page_LoadComplete(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.LoadComplete
        If Not Page.IsPostBack Then
            tblView.Visible = False
            'LoadData(ddlAccounts)
        End If
        If Hsort.Value = "" Then
            Hsort.Value = "TranscendID"
        End If
        If Horder.Value = "" Then
            Horder.Value = " DESC"
        End If
    End Sub
    Protected Sub btnSubmit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSubmit.Click
        ErrLabel.Text = String.Empty
        lblResponse.Text = String.Empty
        GrdViewData.DataSource = Nothing
        GrdViewData.DataBind()
        If String.IsNullOrEmpty(txtName.Text) And String.IsNullOrEmpty(txtSAccount.Text.ToString) And String.IsNullOrEmpty(ddlStatus.SelectedValue.ToString) Then
            lblResponse.Text = String.Empty
            lblResponse.Text = "Please enter name/account/status for search"
            Exit Sub
        Else
            DoSearch(txtName.Text, txtSAccount.Text.ToString, ddlStatus.SelectedValue.ToString, Hsort.Value, Horder.Value)
        End If
    End Sub
    Private Function DoSearch(ByVal UserName As String, ByVal AccName As String, ByVal Status As String, ByVal Sort As String, ByVal Dir As String) As Boolean
        lblResponse.Text = String.Empty

        Dim oConn As New Data.SqlClient.SqlConnection
        oConn.ConnectionString = System.Configuration.ConfigurationManager.AppSettings("ETSCon")

        Try

            oConn.Open()
            hdnUserID.Value = String.Empty
            'Get the UserID,UserName from give user
            Dim oCommand As New Data.SqlClient.SqlCommand("SELECT UserID,UserName FROM DBO.tblUsers where  UserID<>'11111111-1111-1111-1111-111111111111' AND FirstName+' '+LastName LIKE '" & UserName & "'", oConn)
            Dim oRec As Data.SqlClient.SqlDataReader = oCommand.ExecuteReader()

            If oRec.HasRows Then
                While oRec.Read
                    hdnUserID.Value = oRec("UserID").ToString
                    hdnResource_Name.Value = oRec("UserName").ToString
                End While
            End If
            oRec.Close()
            oRec = Nothing
            oCommand = Nothing
            'end 

            If String.IsNullOrEmpty(hdnUserID.Value) And String.IsNullOrEmpty(txtSAccount.Text.ToString) And String.IsNullOrEmpty(ddlStatus.SelectedValue.ToString) Then
                lblResponse.Text = String.Empty
                lblResponse.Text = "User/Account not found"
                Exit Function
            Else
                If CheckUserID(hdnUserID.Value.ToString) Or CheckAccounts(txtSAccount.Text.ToString) Or CheckStatus(ddlStatus.SelectedValue.ToString) Then
                Else
                    lblResponse.Text = String.Empty
                    lblResponse.Text = "Transcend ID not assigned "
                End If

                Dim varWhere As String = String.Empty
                If Not String.IsNullOrEmpty(UserName) Then
                    varWhere = " AND FirstName+' '+LastName LIKE '" & UserName & "'"
                End If
                If Not String.IsNullOrEmpty(AccName) Then
                    varWhere = varWhere & " AND M.Account LIKE '" & AccName & "%'"
                End If
                If Not String.IsNullOrEmpty(Status) Then
                    If Trim(UCase(Status)) = Trim(UCase("Active")) Then
                        varWhere = varWhere & " AND (U.IsDeleted=0 OR U.IsDeleted IS NULL) "
                    ElseIf Trim(UCase(Status)) = Trim(UCase("Inactive")) Then
                        varWhere = varWhere & " AND (U.IsDeleted=1) "
                    End If
                End If


                'Dim SQLString = "SELECT M.TrackID,U.UserID,ISNULL(FirstName,'')+' '+ISNULL(LastName,'') as UName,Resource_Name AS 'SecureITID',M.ID AS 'TranscendID',A.AccName AS 'Account',A.AccID,M.Name,M.User_Name,M.Password,M.Juniper_Password,M.Console_Password,M.ClearanceStatus,M.Mentor,M.QA FROM tblUsers U LEFT OUTER JOIN Transcend.dbo.tblMapping M ON U.UserID=M.UserID INNER JOIN Transcend.dbo.tblAccounts A ON M.Account=A.AccID where  (U.Isdeleted is NULL or U.Isdeleted = 'False') and (M.Isdeleted is NULL or M.Isdeleted = 'False') and  U.UserID<>'11111111-1111-1111-1111-111111111111' " & varWhere.ToString & " ORDER BY " & Sort & " " & Dir & " "
                Dim SQLString = "SELECT M.TrackID,U.UserID,ISNULL(FirstName,'')+' '+ISNULL(LastName,'') as UName,Resource_Name AS 'SecureITID',M.ID AS 'TranscendID',Account,M.Name,M.User_Name,M.Password,M.Juniper_Password,M.Console_Password,M.ClearanceStatus,M.Mentor,M.QA,M.Comment,M.AuditType,U.IsDeleted AS 'Status', ChartScriptID FROM DBO.tblUsers U INNER JOIN Transcend.dbo.tblMapping M ON U.UserID=M.UserID where   U.UserID<>'11111111-1111-1111-1111-111111111111' " & varWhere.ToString & " ORDER BY " & Sort & " " & Dir & " "
                'Response.Write(SQLString)
                Dim objDA As New System.Data.SqlClient.SqlDataAdapter(SQLString, oConn)
                Dim objDS As New System.Data.DataSet
                objDA.Fill(objDS, "tblMapping")

                Dim objDataTable As New System.Data.DataTable
                objDataTable = objDS.Tables(0)
                'Response.Write(objDataTable.Rows.Count)
                If objDataTable.Rows.Count > 0 Then
                    GrdViewData.DataSource = objDataTable
                    GrdViewData.DataBind()
                Else
                    Dim t As Data.DataTable = objDS.Tables(0).Clone
                    For Each c As Data.DataColumn In t.Columns
                        c.AllowDBNull = True
                    Next
                    t.Rows.Add(t.NewRow())
                    GrdViewData.DataSource = t
                    GrdViewData.DataBind()
                    GrdViewData.Rows(0).Visible = False
                    GrdViewData.Rows(0).Controls.Clear()
                End If
                tblView.Visible = True
            End If
        Catch ex As Exception
            'Response.Write(ex.Message)
        Finally
            If oConn.State <> Data.ConnectionState.Closed Then
                oConn.Close()
                oConn = Nothing
            End If
        End Try
    End Function
    Private Property GridViewSortDirection() As SortDirection
        Get
            If ViewState("sortDirection") Is Nothing Then
                ViewState("sortDirection") = SortDirection.Ascending
            End If
            Return DirectCast(ViewState("sortDirection"), SortDirection)
        End Get
        Set(ByVal value As SortDirection)
            ViewState("sortDirection") = value
        End Set
    End Property
    Protected Sub GrdViewData_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles GrdViewData.PageIndexChanging
        DoSearch(txtName.Text, txtSAccount.Text.ToString, ddlStatus.SelectedValue.ToString, Hsort.Value, Horder.Value)
        GrdViewData.PageIndex = e.NewPageIndex
        GrdViewData.DataBind()
    End Sub
    Protected Sub GrdViewData_Sorting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewSortEventArgs) Handles GrdViewData.Sorting
        Dim sortExpression As String = e.SortExpression
        ViewState("SortExpression") = sortExpression
        If GridViewSortDirection = SortDirection.Ascending Then
            GridViewSortDirection = SortDirection.Descending
            Hsort.Value = sortExpression
            Horder.Value = " DESC"
        Else
            GridViewSortDirection = SortDirection.Ascending
            Hsort.Value = sortExpression
            Horder.Value = " ASC"
        End If
        DoSearch(txtName.Text, txtSAccount.Text.ToString, ddlStatus.SelectedValue.ToString, Hsort.Value, Horder.Value)
    End Sub
    Protected Function CheckTransID(ByVal TransID As String) As Boolean
        Dim oConn As New Data.SqlClient.SqlConnection
        oConn.ConnectionString = System.Configuration.ConfigurationManager.AppSettings("ETSCon")
        Dim varReturn As Boolean = False
        Try
            oConn.Open()

            Dim objCmd As New Data.SqlClient.SqlCommand("SELECT Count(*) AS 'Count' FROM Transcend.dbo.tblMapping WHERE ID='" & TransID.ToString & "' AND IsDeleted IS NULL ", oConn)
            Dim objRec As Data.SqlClient.SqlDataReader = objCmd.ExecuteReader
            If objRec.HasRows Then
                While objRec.Read
                    If objRec("Count") > 0 Then
                        varReturn = True
                    End If
                End While
            End If
            objRec.Close()
            objRec = Nothing
            objCmd = Nothing

        Catch ex As Exception

        Finally
            If oConn.State <> Data.ConnectionState.Closed Then
                oConn.Close()
                oConn = Nothing
            End If
        End Try
        Return varReturn
    End Function
    Protected Function GetAccID(ByVal AccName As String) As String
        Dim oConn As New Data.SqlClient.SqlConnection
        oConn.ConnectionString = System.Configuration.ConfigurationManager.AppSettings("ETSCon")
        Dim varReturn As String = String.Empty
        Try
            oConn.Open()
            Dim objCmd As New Data.SqlClient.SqlCommand("SELECT TOP 1 AccID FROM Transcend.dbo.tblAccounts WHERE AccName='" & AccName.ToString & "' AND IsDeleted IS NULL ", oConn)
            Dim objRec As Data.SqlClient.SqlDataReader = objCmd.ExecuteReader
            If objRec.HasRows Then
                While objRec.Read
                    varReturn = objRec("AccId").ToString
                End While
            End If
            objRec.Close()
            objRec = Nothing
            objCmd = Nothing
        Catch ex As Exception
            'Response.Write(ex.Message)
        Finally
            If oConn.State <> Data.ConnectionState.Closed Then
                oConn.Close()
                oConn = Nothing
            End If
        End Try
        Return varReturn
    End Function
    Protected Sub LoadData(ByVal ddl As DropDownList)
        ddl.Items.Clear()
        Try
            Dim objDataAdapter As New Data.SqlClient.SqlDataAdapter("SELECT AccID,AccName FROM Transcend.dbo.tblAccounts WHERE IsDeleted IS NULL ORDER BY AccName ", System.Configuration.ConfigurationManager.AppSettings("ETSCon"))
            Dim objDS As New Data.DataSet
            objDataAdapter.Fill(objDS)
            ddl.DataSource = objDS.Tables(0)
            ddl.DataTextField = "AccName"
            ddl.DataValueField = "AccID"
            ddl.DataBind()
            ddl.Items.Insert(0, New ListItem("Please Select", String.Empty))
        Catch ex As Exception
        End Try
    End Sub
    Protected Function CheckUserID(ByVal UserID As String) As Boolean
        Dim oConn As New Data.SqlClient.SqlConnection
        oConn.ConnectionString = System.Configuration.ConfigurationManager.AppSettings("ETSCon")
        Dim varReturn As Boolean = False
        Try
            oConn.Open()

            Dim objCmd As New Data.SqlClient.SqlCommand("SELECT Count(*) AS 'Count' FROM Transcend.dbo.tblMapping WHERE UserID='" & UserID.ToString & "'", oConn)
            Dim objRec As Data.SqlClient.SqlDataReader = objCmd.ExecuteReader
            If objRec.HasRows Then
                While objRec.Read
                    If objRec("Count") > 0 Then
                        varReturn = True
                    End If
                End While
            End If
            objRec.Close()
            objRec = Nothing
            objCmd = Nothing

        Catch ex As Exception
        Finally
            If oConn.State <> Data.ConnectionState.Closed Then
                oConn.Close()
                oConn = Nothing
            End If
        End Try
        Return varReturn
    End Function
    Protected Function CheckAccounts(ByVal AccName As String) As Boolean
        Dim oConn As New Data.SqlClient.SqlConnection
        oConn.ConnectionString = System.Configuration.ConfigurationManager.AppSettings("ETSCon")
        Dim varReturn As Boolean = False
        Try
            oConn.Open()

            Dim objCmd As New Data.SqlClient.SqlCommand("SELECT Count(*) AS 'Count' FROM Transcend.dbo.tblMapping WHERE Account LIKE '" & AccName.ToString & "%'", oConn)
            Dim objRec As Data.SqlClient.SqlDataReader = objCmd.ExecuteReader
            If objRec.HasRows Then
                While objRec.Read
                    If objRec("Count") > 0 Then
                        varReturn = True
                    End If
                End While
            End If
            objRec.Close()
            objRec = Nothing
            objCmd = Nothing

        Catch ex As Exception
        Finally
            If oConn.State <> Data.ConnectionState.Closed Then
                oConn.Close()
                oConn = Nothing
            End If
        End Try
        Return varReturn
    End Function
    Public Shared Sub DataGridToExcel(ByVal dgExport As DataGrid, ByVal response As HttpResponse, Optional ByVal argHeader As String = "", Optional ByVal argSubHead As String = "")

        Dim stringWrite As New System.IO.StringWriter()                 'create a string writer
        Dim htmlWrite As New System.Web.UI.HtmlTextWriter(stringWrite)  'create an htmltextwriter which uses the stringwriter
        Dim dg As New DataGrid()
        response.Clear()                                                'clean up the response.object
        response.Charset = ""

        Dim filename = "Transcend Mapping List as on " & Now & " .xls"
        response.AddHeader("content-disposition", "attachment;filename=" & filename)

        response.ContentType = "application/vnd.ms-excel"               'set the response mime type for excel
        dg = dgExport                                                   'set the input datagrid = to the new dg grid
        'dg.GridLines = GridLines.Both                                    'no gridlines
        dg.HeaderStyle.Font.Bold = True                                 'header text bold
        dg.HeaderStyle.ForeColor = System.Drawing.Color.Crimson             'change colors etc...
        'dg.ItemStyle.ForeColor = System.Drawing.Color.Black
        dg.DataBind()                                                   'bind modified grid
        dg.RenderControl(htmlWrite)                                     'render datagrid to htmltextwriter
        'response.Write("<h4>" & argHeader & "</h4>")                    'output the html with header and footer
        'response.Write("<b>" & argSubHead & "</b>")
        response.Write(stringWrite.ToString())
        'response.Write("-- end of report --")
        response.End()
    End Sub
    Protected Sub LnlExport_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles LnlExport.Click
        Dim SQLString As String = String.Empty
        Dim ConString As String = System.Configuration.ConfigurationManager.AppSettings("ETSCon")
        Dim oConn As New Data.SqlClient.SqlConnection
        oConn.ConnectionString = ConString
        Try
            oConn.Open()
            Dim varQueryString As String = String.Empty

            'SQLString = "SELECT ISNULL(FirstName,'')+' '+ISNULL(LastName,'') as Name,U.UserName AS SecureIT_ID,TranscendID AS Transcend_ID FROM tblUsers U INNER JOIN Transcend.dbo.tblMapping M ON U.UserID=M.UserID where  (Isdeleted is NULL or Isdeleted = 'False') and U.UserID<>'11111111-1111-1111-1111-111111111111' ORDER BY FirstName,LastName "
            SQLString = "SELECT ISNULL(FirstName,'')+' '+ISNULL(LastName,'') as UName,Resource_Name AS 'SecureITID',M.ID AS 'TranscendID',Account,M.Name,M.User_Name,M.Password,M.Juniper_Password,M.Console_Password,M.ClearanceStatus,M.Mentor,M.QA,M.Comment,U.IsDeleted AS 'Status' FROM DBO.tblUsers U INNER JOIN Transcend.dbo.tblMapping M ON U.UserID=M.UserID WHERE  (M.Isdeleted is NULL or M.Isdeleted = 'False') and  U.UserID<>'11111111-1111-1111-1111-111111111111'"

            Dim objDA As New System.Data.SqlClient.SqlDataAdapter(SQLString, oConn)
            Dim objDS As New System.Data.DataSet()

            objDA.Fill(objDS)

            dgResultsData.DataSource = objDS.Tables(0)
            dgResultsData.DataBind()

            Dim mHeader As String = "Transcend Mapping List"
            Dim mSubHead As String = "Printed by: " & Session("UserName") & "<br>Data as at: " & Now()
            DataGridToExcel(dgResultsData, Response, mHeader, mSubHead)
            Response.End()
        Catch ex As Exception
            'Response.Write(ex.Message)
        Finally
            If oConn.State <> Data.ConnectionState.Closed Then
                oConn.Close()
                oConn = Nothing
            End If
        End Try
    End Sub
    Protected Sub GrdViewData_RowCancelingEdit(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCancelEditEventArgs) Handles GrdViewData.RowCancelingEdit
        GrdViewData.EditIndex = -1
        DoSearch(txtName.Text, txtSAccount.Text.ToString, ddlStatus.SelectedValue.ToString, Hsort.Value, Horder.Value)
    End Sub
    Protected Sub GrdViewData_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles GrdViewData.RowCommand
        If Trim(UCase(e.CommandName)) = Trim(UCase("Insert")) Then
            Dim oConn As New Data.SqlClient.SqlConnection
            oConn.ConnectionString = System.Configuration.ConfigurationManager.AppSettings("ETSCon")
            'If Not String.IsNullOrEmpty(hdnUserID.Value) And Not String.IsNullOrEmpty(hdnResource_Name.Value) Then
            Try
                Dim varInsert As New System.Text.StringBuilder
                Dim varValues As New System.Text.StringBuilder

                Dim varUserID As DropDownList
                varUserID = DirectCast(GrdViewData.FooterRow.FindControl("FddlSID"), DropDownList)

                If String.IsNullOrEmpty(varUserID.Items(varUserID.SelectedIndex).Value.ToString) Then
                    lblResponse.Text = String.Empty
                    lblResponse.Text = "Please select user name "
                    Exit Sub
                End If

                Dim varUserName As String = String.Empty
                varUserName = GetUserName(varUserID.Items(varUserID.SelectedIndex).Value.ToString)

                If Not String.IsNullOrEmpty(varInsert.ToString) Then
                    varInsert.Append(",UserID,Resource_Name")
                    varValues.Append(",'" & varUserID.Items(varUserID.SelectedIndex).Value.ToString & "','" & varUserName.ToString & "'")
                Else
                    varInsert.Append("UserID,Resource_Name")
                    varValues.Append("'" & varUserID.Items(varUserID.SelectedIndex).Value.ToString & "','" & varUserName.ToString & "'")
                End If

                'If Not String.IsNullOrEmpty(varInsert.ToString) Then
                '    varInsert.Append(",UserID")
                '    varValues.Append(",'" & hdnUserID.Value.ToString & "'")
                'Else
                '    varInsert.Append("UserID")
                '    varValues.Append("'" & hdnUserID.Value.ToString & "'")
                'End If

                'If Not String.IsNullOrEmpty(varInsert.ToString) Then
                '    varInsert.Append(",Resource_Name")
                '    varValues.Append(",'" & hdnResource_Name.Value.ToString & "'")
                'Else
                '    varInsert.Append("Resource_Name")
                '    varValues.Append("'" & hdnResource_Name.Value.ToString & "'")
                'End If

                Dim varTransID As String = String.Empty
                varTransID = DirectCast(GrdViewData.FooterRow.FindControl("FtxtTransID"), TextBox).Text.ToString

                If String.IsNullOrEmpty(varTransID) Then
                    lblResponse.Text = String.Empty
                    lblResponse.Text = "Transcend id should not be empty "
                    Exit Sub
                End If

                If Not CheckTransID(varTransID) Then
                    If Not String.IsNullOrEmpty(varTransID.ToString()) Then
                        If Not String.IsNullOrEmpty(varInsert.ToString) Then
                            varInsert.Append(",ID")
                            varValues.Append(",'" & varTransID.ToString & "'")
                        Else
                            varInsert.Append("ID")
                            varValues.Append("'" & varTransID.ToString & "'")
                        End If
                    End If

                    Dim varAccount As TextBox
                    varAccount = DirectCast(GrdViewData.FooterRow.FindControl("FtxtAccount"), TextBox)

                    If Not String.IsNullOrEmpty(varAccount.Text.ToString) Then
                        If Not String.IsNullOrEmpty(varInsert.ToString) Then
                            varInsert.Append(",Account")
                            varValues.Append(",'" & varAccount.Text.ToString & "'")
                        Else
                            varInsert.Append("Account")
                            varValues.Append("'" & varAccount.Text.ToString & "'")
                        End If
                    End If

                    Dim varName As String = String.Empty
                    varName = DirectCast(GrdViewData.FooterRow.FindControl("FtxtName"), TextBox).Text.ToString

                    If Not String.IsNullOrEmpty(varName.ToString()) Then
                        If Not String.IsNullOrEmpty(varInsert.ToString) Then
                            varInsert.Append(",Name")
                            varValues.Append(",'" & varName.ToString & "'")
                        Else
                            varInsert.Append("Name")
                            varValues.Append("'" & varName.ToString & "'")
                        End If
                    End If

                    Dim varUser_Name As String = String.Empty
                    varUser_Name = DirectCast(GrdViewData.FooterRow.FindControl("FtxtUser_Name"), TextBox).Text.ToString

                    If Not String.IsNullOrEmpty(varUser_Name.ToString()) Then
                        If Not String.IsNullOrEmpty(varInsert.ToString) Then
                            varInsert.Append(",User_Name")
                            varValues.Append(",'" & varUser_Name.ToString & "'")
                        Else
                            varInsert.Append("User_Name")
                            varValues.Append("'" & varUser_Name.ToString & "'")
                        End If
                    End If

                    Dim varPassword As String = String.Empty
                    varPassword = DirectCast(GrdViewData.FooterRow.FindControl("FtxtPassword"), TextBox).Text.ToString

                    If Not String.IsNullOrEmpty(varPassword.ToString()) Then
                        If Not String.IsNullOrEmpty(varInsert.ToString) Then
                            varInsert.Append(",Password")
                            varValues.Append(",'" & varPassword.ToString & "'")
                        Else
                            varInsert.Append("Password")
                            varValues.Append("'" & varPassword.ToString & "'")
                        End If
                    End If

                    Dim varJuniper_Password As String = String.Empty
                    varJuniper_Password = DirectCast(GrdViewData.FooterRow.FindControl("FtxtJuniper_Password"), TextBox).Text.ToString

                    If Not String.IsNullOrEmpty(varJuniper_Password.ToString()) Then
                        If Not String.IsNullOrEmpty(varInsert.ToString) Then
                            varInsert.Append(",Juniper_Password")
                            varValues.Append(",'" & varJuniper_Password.ToString & "'")
                        Else
                            varInsert.Append("Juniper_Password")
                            varValues.Append("'" & varJuniper_Password.ToString & "'")
                        End If
                    End If

                    Dim varConsole_Password As String = String.Empty
                    varConsole_Password = DirectCast(GrdViewData.FooterRow.FindControl("FtxtConsole_Password"), TextBox).Text.ToString

                    If Not String.IsNullOrEmpty(varConsole_Password.ToString()) Then
                        If Not String.IsNullOrEmpty(varInsert.ToString) Then
                            varInsert.Append(",Console_Password")
                            varValues.Append(",'" & varConsole_Password.ToString & "'")
                        Else
                            varInsert.Append("Console_Password")
                            varValues.Append("'" & varConsole_Password.ToString & "'")
                        End If
                    End If

                    Dim varCS As DropDownList
                    varCS = DirectCast(GrdViewData.FooterRow.FindControl("FddlCS"), DropDownList)

                    'If Not String.IsNullOrEmpty(varCS.Items(varCS.SelectedIndex).Value.ToString) Then
                    If Not String.IsNullOrEmpty(varInsert.ToString) Then
                        varInsert.Append(",ClearanceStatus")
                        varValues.Append(",'" & varCS.Items(varCS.SelectedIndex).Value.ToString & "'")
                    Else
                        varInsert.Append("ClearanceStatus")
                        varValues.Append("'" & varCS.Items(varCS.SelectedIndex).Value.ToString & "'")
                    End If
                    'End If

                    Dim varMentor As String = String.Empty
                    varMentor = DirectCast(GrdViewData.FooterRow.FindControl("FtxtMentor"), TextBox).Text.ToString

                    If Not String.IsNullOrEmpty(varMentor.ToString()) Then
                        If Not String.IsNullOrEmpty(varInsert.ToString) Then
                            varInsert.Append(",Mentor")
                            varValues.Append(",'" & varMentor.ToString & "'")
                        Else
                            varInsert.Append(",Mentor")
                            varValues.Append(",'" & varMentor.ToString & "'")
                        End If
                    End If

                    Dim varQA As String = String.Empty
                    varQA = DirectCast(GrdViewData.FooterRow.FindControl("FtxtQA"), TextBox).Text.ToString

                    If Not String.IsNullOrEmpty(varQA.ToString()) Then
                        If Not String.IsNullOrEmpty(varInsert.ToString) Then
                            varInsert.Append(",QA")
                            varValues.Append(",'" & varQA.ToString & "'")
                        Else
                            varInsert.Append("QA")
                            varValues.Append("'" & varQA.ToString & "'")
                        End If
                    End If


                    Dim varChartScriptID As String = String.Empty
                    varChartScriptID = DirectCast(GrdViewData.FooterRow.FindControl("FtxtChartScriptID"), TextBox).Text.ToString

                    If Not String.IsNullOrEmpty(varChartScriptID.ToString()) Then
                        If Not String.IsNullOrEmpty(varInsert.ToString) Then
                            varInsert.Append(",ChartScriptID")
                            varValues.Append(",'" & varChartScriptID.ToString & "'")
                        Else
                            varInsert.Append("ChartScriptID")
                            varValues.Append("'" & varChartScriptID.ToString & "'")
                        End If
                    End If

                    If Not String.IsNullOrEmpty(varInsert.ToString) Then
                        varInsert.Append(",UpdatedBy,UpdatedOn")
                        varValues.Append(",'" & Session("UserID").ToString & "','" & Now & "'")
                    Else
                        varInsert.Append("UpdatedBy,UpdatedOn")
                        varValues.Append("'" & Session("UserID").ToString & "','" & Now & "'")
                    End If

                    oConn.Open()
                    Dim varQuery As String = String.Empty
                    varQuery = "INSERT INTO Transcend.dbo.tblMapping (" & varInsert.ToString & ") VALUES (" & varValues.ToString & ")"

                    Dim objCmd As New Data.SqlClient.SqlCommand(varQuery, oConn)

                    If objCmd.ExecuteNonQuery = 1 Then
                        GrdViewData.EditIndex = -1
                        DoSearch(txtName.Text, txtSAccount.Text.ToString, ddlStatus.SelectedValue.ToString, Hsort.Value, Horder.Value)
                    End If
                Else
                    lblResponse.Text = String.Empty
                    lblResponse.Text = "Transcend id already assigned "
                    Exit Sub
                End If
            Catch ex As Exception
                Response.Write(ex.Message)
            Finally
                If oConn.State <> Data.ConnectionState.Closed Then
                    oConn.Close()
                    oConn = Nothing
                End If
            End Try
            'Else
            '    lblResponse.Text = String.Empty
            '    lblResponse.Text = "Please search only user wise and then assign transcend ids"
            '    Exit Sub
            'End If
        ElseIf Trim(UCase(e.CommandName)) = Trim(UCase("Delete")) Then
            Dim varTrackID As String = e.CommandArgument

            Dim oConn As New Data.SqlClient.SqlConnection
            oConn.ConnectionString = System.Configuration.ConfigurationManager.AppSettings("ETSCon")

            Try
                oConn.Open()

                Dim varQuery As String = String.Empty
                varQuery = "UPDATE Transcend.dbo.tblMapping SET IsDeleted=1,UpdatedBy='" & Session("UserID").ToString & "',UpdatedOn='" & Now() & "' WHERE TrackID='" & varTrackID.ToString & "'"

                Dim objCmd As New Data.SqlClient.SqlCommand(varQuery, oConn)
                If objCmd.ExecuteNonQuery = 1 Then
                    GrdViewData.EditIndex = -1
                    DoSearch(txtName.Text, txtSAccount.Text.ToString, ddlStatus.SelectedValue.ToString, Hsort.Value, Horder.Value)
                End If
                objCmd = Nothing
            Catch ex As Exception
            Finally
                If oConn.State <> Data.ConnectionState.Closed Then
                    oConn.Close()
                    oConn = Nothing
                End If
            End Try
        End If
    End Sub
    Protected Sub GrdViewData_RowDeleting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewDeleteEventArgs) Handles GrdViewData.RowDeleting
    End Sub
    Protected Sub GrdViewData_RowEditing(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewEditEventArgs) Handles GrdViewData.RowEditing
        GrdViewData.EditIndex = e.NewEditIndex
        DoSearch(txtName.Text, txtSAccount.Text.ToString, ddlStatus.SelectedValue.ToString, Hsort.Value, Horder.Value)
    End Sub
    Protected Sub GrdViewData_RowUpdating(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewUpdateEventArgs) Handles GrdViewData.RowUpdating
        If GrdViewData.Rows.Count > 0 Then
            Dim oConn As New Data.SqlClient.SqlConnection
            oConn.ConnectionString = System.Configuration.ConfigurationManager.AppSettings("ETSCon")
            Try
                Dim varhdnTrackID As New HiddenField
                varhdnTrackID = DirectCast(GrdViewData.Rows(e.RowIndex).FindControl("hdnTrackID"), HiddenField)
                Dim varUpdate As New System.Text.StringBuilder

                Dim varSecureItID As DropDownList
                varSecureItID = DirectCast(GrdViewData.Rows(e.RowIndex).FindControl("ddlSID"), DropDownList)

                Dim varUserName As String = String.Empty
                varUserName = GetUserName(varSecureItID.Items(varSecureItID.SelectedIndex).Value.ToString)

                If Not String.IsNullOrEmpty(varUpdate.ToString) Then
                    varUpdate.Append(",UserID='" & varSecureItID.Items(varSecureItID.SelectedIndex).Value.ToString & "',Resource_Name='" & varUserName.ToString & "'")
                Else
                    varUpdate.Append("UserID='" & varSecureItID.Items(varSecureItID.SelectedIndex).Value.ToString & "',Resource_Name='" & varUserName.ToString & "'")
                End If

                Dim varTransID As String = String.Empty
                varTransID = DirectCast(GrdViewData.Rows(e.RowIndex).FindControl("txtTransID"), TextBox).Text.ToString

                If String.IsNullOrEmpty(varTransID) Then
                    lblResponse.Text = String.Empty
                    lblResponse.Text = "Transcend id should not be empty "
                    Exit Sub
                End If

                If Not String.IsNullOrEmpty(varTransID.ToString()) Then
                    If Not String.IsNullOrEmpty(varUpdate.ToString) Then
                        varUpdate.Append(",ID='" & varTransID.ToString & "'")
                    Else
                        varUpdate.Append("ID='" & varTransID.ToString & "'")
                    End If
                End If

                Dim varAccount As TextBox
                varAccount = DirectCast(GrdViewData.Rows(e.RowIndex).FindControl("txtAccount"), TextBox)
                If Not String.IsNullOrEmpty(varAccount.Text.ToString) Then
                    If Not String.IsNullOrEmpty(varUpdate.ToString) Then
                        varUpdate.Append(",Account='" & varAccount.Text.ToString & "'")
                    Else
                        varUpdate.Append("Account='" & varAccount.Text.ToString & "'")
                    End If
                Else
                    If Not String.IsNullOrEmpty(varUpdate.ToString) Then
                        varUpdate.Append(",Account=NULL")
                    Else
                        varUpdate.Append("Account=NULL")
                    End If
                End If

                Dim varName As String = String.Empty
                varName = DirectCast(GrdViewData.Rows(e.RowIndex).FindControl("txtName"), TextBox).Text.ToString

                If Not String.IsNullOrEmpty(varName.ToString()) Then
                    If Not String.IsNullOrEmpty(varUpdate.ToString) Then
                        varUpdate.Append(",Name='" & varName.ToString & "'")
                    Else
                        varUpdate.Append("Name='" & varName.ToString & "'")
                    End If
                Else
                    If Not String.IsNullOrEmpty(varUpdate.ToString) Then
                        varUpdate.Append(",Name=NULL")
                    Else
                        varUpdate.Append("Name=NULL")
                    End If
                End If

                Dim varUser_Name As String = String.Empty
                varUser_Name = DirectCast(GrdViewData.Rows(e.RowIndex).FindControl("txtUser_Name"), TextBox).Text.ToString

                If Not String.IsNullOrEmpty(varUser_Name.ToString()) Then
                    If Not String.IsNullOrEmpty(varUpdate.ToString) Then
                        varUpdate.Append(",User_Name='" & varUser_Name.ToString & "'")
                    Else
                        varUpdate.Append("User_Name='" & varUser_Name.ToString & "'")
                    End If
                Else
                    If Not String.IsNullOrEmpty(varUpdate.ToString) Then
                        varUpdate.Append(",User_Name=NULL")
                    Else
                        varUpdate.Append("User_Name=NULL")
                    End If
                End If

                Dim varPassword As String = String.Empty
                varPassword = DirectCast(GrdViewData.Rows(e.RowIndex).FindControl("txtPassword"), TextBox).Text.ToString

                If Not String.IsNullOrEmpty(varPassword.ToString()) Then
                    If Not String.IsNullOrEmpty(varUpdate.ToString) Then
                        varUpdate.Append(",Password='" & varPassword.ToString & "'")
                    Else
                        varUpdate.Append("Password='" & varPassword.ToString & "'")
                    End If
                Else
                    If Not String.IsNullOrEmpty(varUpdate.ToString) Then
                        varUpdate.Append(",Password=NULL")
                    Else
                        varUpdate.Append("Password=NULL")
                    End If
                End If

                Dim varJuniper_Password As String = String.Empty
                varJuniper_Password = DirectCast(GrdViewData.Rows(e.RowIndex).FindControl("txtJuniper_Password"), TextBox).Text.ToString

                If Not String.IsNullOrEmpty(varJuniper_Password.ToString()) Then
                    If Not String.IsNullOrEmpty(varUpdate.ToString) Then
                        varUpdate.Append(",Juniper_Password='" & varJuniper_Password.ToString & "'")
                    Else
                        varUpdate.Append("Juniper_Password='" & varJuniper_Password.ToString & "'")
                    End If
                Else
                    If Not String.IsNullOrEmpty(varUpdate.ToString) Then
                        varUpdate.Append(",Juniper_Password=NULL")
                    Else
                        varUpdate.Append("Juniper_Password=NULL")
                    End If
                End If

                Dim varConsole_Password As String = String.Empty
                varConsole_Password = DirectCast(GrdViewData.Rows(e.RowIndex).FindControl("txtConsole_Password"), TextBox).Text.ToString

                If Not String.IsNullOrEmpty(varConsole_Password.ToString()) Then
                    If Not String.IsNullOrEmpty(varUpdate.ToString) Then
                        varUpdate.Append(",Console_Password='" & varConsole_Password.ToString & "'")
                    Else
                        varUpdate.Append("Console_Password='" & varConsole_Password.ToString & "'")
                    End If
                Else
                    If Not String.IsNullOrEmpty(varUpdate.ToString) Then
                        varUpdate.Append(",Console_Password=NULL")
                    Else
                        varUpdate.Append("Console_Password=NULL")
                    End If
                End If

                Dim varCS As DropDownList
                varCS = DirectCast(GrdViewData.Rows(e.RowIndex).FindControl("ddlCS"), DropDownList)

                'If Not String.IsNullOrEmpty(varCS.Items(varCS.SelectedIndex).Value.ToString) Then
                If Not String.IsNullOrEmpty(varUpdate.ToString) Then
                    varUpdate.Append(",ClearanceStatus='" & varCS.Items(varCS.SelectedIndex).Value.ToString & "'")
                Else
                    varUpdate.Append("ClearanceStatus='" & varCS.Items(varCS.SelectedIndex).Value.ToString & "'")
                End If
                'End If

                Dim varAType As DropDownList
                varAType = DirectCast(GrdViewData.Rows(e.RowIndex).FindControl("ddlAType"), DropDownList)

                'If varAType.Enabled Then
                '    'Response.Write(varAType.Enabled)
                '    'Response.Write(varAType.Items(varAType.SelectedIndex).Value.ToString)
                If String.IsNullOrEmpty(varAType.Items(varAType.SelectedIndex).Value.ToString) Then
                    varUpdate.Append(",AuditType=NULL")
                Else
                    varUpdate.Append(",AuditType='" & varAType.Items(varAType.SelectedIndex).Value.ToString & "'")
                End If
                'End If

                Dim varMentor As String = String.Empty
                varMentor = DirectCast(GrdViewData.Rows(e.RowIndex).FindControl("txtMentor"), TextBox).Text.ToString

                If Not String.IsNullOrEmpty(varMentor.ToString()) Then
                    If Not String.IsNullOrEmpty(varUpdate.ToString) Then
                        varUpdate.Append(",Mentor='" & varMentor.ToString & "'")
                    Else
                        varUpdate.Append("Mentor='" & varMentor.ToString & "'")
                    End If
                Else
                    If Not String.IsNullOrEmpty(varUpdate.ToString) Then
                        varUpdate.Append(",Mentor=NULL")
                    Else
                        varUpdate.Append("Mentor=NULL")
                    End If
                End If

                Dim varQA As String = String.Empty
                varQA = DirectCast(GrdViewData.Rows(e.RowIndex).FindControl("txtQA"), TextBox).Text.ToString

                If Not String.IsNullOrEmpty(varQA.ToString()) Then
                    If Not String.IsNullOrEmpty(varUpdate.ToString) Then
                        varUpdate.Append(",QA='" & varQA.ToString & "'")
                    Else
                        varUpdate.Append("QA='" & varQA.ToString & "'")
                    End If
                Else
                    If Not String.IsNullOrEmpty(varUpdate.ToString) Then
                        varUpdate.Append(",QA=NULL")
                    Else
                        varUpdate.Append("QA=NULL")
                    End If
                End If


                Dim varChartScriptID As String = String.Empty
                varChartScriptID = DirectCast(GrdViewData.Rows(e.RowIndex).FindControl("txtChartScriptID"), TextBox).Text.ToString

                If Not String.IsNullOrEmpty(varChartScriptID.ToString()) Then
                    If Not String.IsNullOrEmpty(varUpdate.ToString) Then
                        varUpdate.Append(",ChartScriptID='" & varChartScriptID.ToString & "'")
                    Else
                        varUpdate.Append("ChartScriptID='" & varChartScriptID.ToString & "'")
                    End If
                Else
                    If Not String.IsNullOrEmpty(varUpdate.ToString) Then
                        varUpdate.Append(",ChartScriptID=NULL")
                    Else
                        varUpdate.Append("ChartScriptID=NULL")
                    End If
                End If

                Dim varComment As String = String.Empty
                varComment = DirectCast(GrdViewData.Rows(e.RowIndex).FindControl("txtComment"), TextBox).Text.ToString

                If Not String.IsNullOrEmpty(varComment.ToString()) Then
                    If Not String.IsNullOrEmpty(varUpdate.ToString) Then
                        varUpdate.Append(",Comment='" & varComment.ToString & "'")
                    Else
                        varUpdate.Append("Comment='" & varComment.ToString & "'")
                    End If
                Else
                    If Not String.IsNullOrEmpty(varUpdate.ToString) Then
                        varUpdate.Append(",Comment=NULL")
                    Else
                        varUpdate.Append("Comment=NULL")
                    End If
                End If

                If Not String.IsNullOrEmpty(varUpdate.ToString) Then
                    varUpdate.Append(",UpdatedBy='" & Session("UserID").ToString & "',UpdatedOn='" & Now() & "'")
                Else
                    varUpdate.Append(",UpdatedBy='" & Session("UserID").ToString & "',UpdatedOn='" & Now() & "'")
                End If

                oConn.Open()
                Dim varQuery As String = String.Empty
                varQuery = "UPDATE Transcend.dbo.tblMapping SET " & varUpdate.ToString & " WHERE TrackID='" & varhdnTrackID.Value.ToString & "'"
                'Response.Write(varQuery.ToString)
                Dim objCmd As New Data.SqlClient.SqlCommand(varQuery, oConn)

                If objCmd.ExecuteNonQuery = 1 Then
                    GrdViewData.EditIndex = -1
                    DoSearch(txtName.Text, txtSAccount.Text.ToString, ddlStatus.SelectedValue.ToString, Hsort.Value, Horder.Value)
                End If
            Catch ex As Exception
                'Response.Write(ex.Message)
            Finally
                If oConn.State <> Data.ConnectionState.Closed Then
                    oConn.Close()
                    oConn = Nothing
                End If
            End Try
        End If
    End Sub
    Protected Sub SortDDL(ByVal list As Object)
        Try
            Dim li As New ListItem
            Dim sl As New SortedList
            For Each li In list.Items
                sl.Add(li.Text, li.Value)
            Next
            list.DataSource = sl
            list.DataTextField = "Key"
            list.DataValueField = "Value"
            list.DataBind()
            Dim varLstItem As New ListItem
            varLstItem.Text = "Please Select"
            varLstItem.Value = String.Empty
            list.Items.Insert(0, varLstItem)
        Catch ex As Exception
        End Try
    End Sub
    Protected Sub btnUpload_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnUpload.Click
        ErrLabel.Text = String.Empty
        lblResponse.Text = String.Empty
        If CheckExcel() Then
            Dim varFileName As String = String.Empty
            Dim varFileUploadPath As String = String.Empty

            Dim dtFormat = Format(Month(Now), "00") & "-" & Format(Day(Now), "00") & "-" & Format(Year(Now), "00") & " " & Format(Hour(Now), "00") & "-" & Format(Minute(Now), "00")
            varFileUploadPath = Server.MapPath("/ets_Files/") & "\Transcend\"

            Dim varTempDir As New IO.DirectoryInfo(varFileUploadPath)
            If varTempDir.Exists = False Then
                varTempDir.Create()
            End If
            varFileName = varFileUploadPath & "\" & Session("UserName").ToString & "_" & dtFormat.ToString & "_Mapping" & FileUpload.FileName
            FileUpload.PostedFile.SaveAs(varFileName)
            UpdateData(varFileName)
        End If
    End Sub
    Protected Function CheckExcel() As Boolean
        Try
            Dim varfileName As String = Server.HtmlEncode(FileUpload.FileName)
            Dim extension As String = System.IO.Path.GetExtension(varfileName)
            If (FileUpload.HasFile) Then
                If Trim(UCase(extension)) = Trim(UCase(".xls")) Or Trim(UCase(extension)) = Trim(UCase(".xlsx")) Or Trim(UCase(extension)) = Trim(UCase(".csv")) Then
                    Return True
                Else
                    ErrLabel.Text = "Please upload Only Excel document"
                    Return False
                End If
            Else
                ErrLabel.Text = "Please upload Only Excel document"
                Return False
            End If
        Catch ex As Exception
        End Try
    End Function
    Protected Sub UpdateData(ByVal varFileName As String)
        Dim myConnectionString As String
        Dim varNotUpdated As New System.Text.StringBuilder
        Dim varTransAlready As New System.Text.StringBuilder
        Dim varTransIDNULL As New System.Text.StringBuilder
        Dim varAccName As New System.Text.StringBuilder
        Dim varBolFormat As Boolean = True
        Dim varCountO As Long
        Dim varCount As Long

        Dim oConn As New Data.SqlClient.SqlConnection
        oConn.ConnectionString = System.Configuration.ConfigurationManager.AppSettings("ETSCon")

        Try
            myConnectionString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" & varFileName & ";Extended Properties=Excel 8.0"
            'myConnectionString = "provider=Microsoft.Jet.OLEDB.4.0; data source=" + varFileName + "; Extended Properties=""EXCEL 8.0;IMEX=1;HDR=Yes;ImportMixedTypes=Text"""
            Dim myDataAdapter As New System.Data.OleDb.OleDbDataAdapter("SELECT * FROM [Sheet1$]", myConnectionString)

            Dim myDataSet As New System.Data.DataSet()
            myDataAdapter.Fill(myDataSet, "ExcelInfo")

            Dim varIntRows, varIntCols As Integer

            varIntRows = myDataSet.Tables(0).Rows.Count
            varIntCols = myDataSet.Tables(0).Columns.Count

            varCountO = varIntRows
            'Response.Write(Trim(UCase(myDataSet.Tables(0).Columns(6).Caption.ToString)))
            'Response.Write(varIntCols)

            If varIntCols = 11 Then
                If Not Trim(UCase(myDataSet.Tables(0).Columns(0).Caption.ToString)) = Trim(UCase("Resource name")) Then
                    varBolFormat = False

                    ErrLabel.Text = String.Empty
                    ErrLabel.Text = "1Template format mistmatch,please refer template "
                    Exit Sub
                ElseIf Not Trim(UCase(myDataSet.Tables(0).Columns(1).Caption.ToString)) = Trim(UCase("Account")) Then
                    varBolFormat = False

                    ErrLabel.Text = String.Empty
                    ErrLabel.Text = "2Template format mistmatch,please refer template "
                    Exit Sub
                ElseIf Not Trim(UCase(myDataSet.Tables(0).Columns(2).Caption.ToString)) = Trim(UCase("ID")) Then
                    varBolFormat = False

                    ErrLabel.Text = String.Empty
                    ErrLabel.Text = "3Template format mistmatch,please refer template "
                    Exit Sub
                ElseIf Not Trim(UCase(myDataSet.Tables(0).Columns(3).Caption.ToString)) = Trim(UCase("Name")) Then
                    varBolFormat = False

                    ErrLabel.Text = String.Empty
                    ErrLabel.Text = "4Template format mistmatch,please refer template "
                    Exit Sub
                ElseIf Not Trim(UCase(myDataSet.Tables(0).Columns(4).Caption.ToString)) = Trim(UCase("User Name")) Then
                    varBolFormat = False

                    ErrLabel.Text = String.Empty
                    ErrLabel.Text = "5Template format mistmatch,please refer template "
                    Exit Sub
                ElseIf Not Trim(UCase(myDataSet.Tables(0).Columns(5).Caption.ToString)) = Trim(UCase("Password")) Then
                    varBolFormat = False

                    ErrLabel.Text = String.Empty
                    ErrLabel.Text = "6Template format mistmatch,please refer template "
                    Exit Sub
                ElseIf Not Trim(UCase(myDataSet.Tables(0).Columns(6).Caption.ToString)) = Trim(UCase("Juniper Password")) Then
                    varBolFormat = False

                    ErrLabel.Text = String.Empty
                    ErrLabel.Text = "7Template format mistmatch,please refer template "
                    Exit Sub
                ElseIf Not Trim(UCase(myDataSet.Tables(0).Columns(7).Caption.ToString)) = Trim(UCase("Console Password")) Then
                    varBolFormat = False

                    ErrLabel.Text = String.Empty
                    ErrLabel.Text = "8Template format mistmatch,please refer template "
                    Exit Sub
                ElseIf Not Trim(UCase(myDataSet.Tables(0).Columns(8).Caption.ToString)) = Trim(UCase("ClearanceStatus")) Then
                    varBolFormat = False

                    ErrLabel.Text = String.Empty
                    ErrLabel.Text = "9Template format mistmatch,please refer template "
                    Exit Sub
                ElseIf Not Trim(UCase(myDataSet.Tables(0).Columns(9).Caption.ToString)) = Trim(UCase("Mentor")) Then
                    varBolFormat = False

                    ErrLabel.Text = String.Empty
                    ErrLabel.Text = "10Template format mistmatch,please refer template "
                    Exit Sub
                ElseIf Not Trim(UCase(myDataSet.Tables(0).Columns(10).Caption.ToString)) = Trim(UCase("QA")) Then
                    varBolFormat = False

                    ErrLabel.Text = String.Empty
                    ErrLabel.Text = "11Template format mistmatch,please refer template "
                    Exit Sub
                End If
                Dim varLngNotFound As Long = 0
                Dim varLngTNULL As Long = 0
                If varBolFormat Then
                    oConn.Open()

                    For varIntRows = 0 To varIntRows - 1
                        If varLngNotFound = 10 Then
                            varLngNotFound = 0
                        End If
                        If varLngTNULL = 10 Then
                            varLngTNULL = 0
                        End If
                        If Not String.IsNullOrEmpty(Trim(myDataSet.Tables(0).Rows(varIntRows).Item(0).ToString)) Then
                            Dim varStrInsert As String = String.Empty
                            Dim varStrValues As String = String.Empty
                            Dim varUserID As String = String.Empty
                            Dim varTransID As String = String.Empty
                            Dim varAccID As String = String.Empty

                            'Get userid from resourec_name
                            Dim objCmd As New Data.SqlClient.SqlCommand("SELECT UserID FROM DBO.tblUsers WHERE (Isdeleted is NULL or Isdeleted = 'False') AND UserID<>'11111111-1111-1111-1111-111111111111' AND UserName='" & Trim(myDataSet.Tables(0).Rows(varIntRows).Item(0).ToString) & "'", New Data.SqlClient.SqlConnection(System.Configuration.ConfigurationManager.AppSettings("ETSCon")))
                            'Response.Write(objCmd.CommandText & "<BR>")
                            Try
                                objCmd.Connection.Open()

                                Dim objRec As Data.SqlClient.SqlDataReader = objCmd.ExecuteReader()
                                If objRec.HasRows Then
                                    While objRec.Read
                                        If Not objRec.IsDBNull(objRec.GetOrdinal("UserID")) Then
                                            varUserID = objRec.GetGuid(objRec.GetOrdinal("UserID")).ToString
                                        End If
                                    End While
                                End If
                                objRec.Close()
                                objRec = Nothing
                                'varUserID = objCmd.ExecuteScalar().ToString

                                'Response.Write(varUserID.ToString & "<BR>")

                            Catch ex As Exception
                                'Response.Write(ex.Message)
                            Finally
                                If objCmd.Connection.State <> Data.ConnectionState.Closed Then
                                    objCmd.Connection.Close()
                                    objCmd.Connection = Nothing
                                End If
                                objCmd = Nothing
                            End Try
                            '
                            'Response.Write(String.IsNullOrEmpty(varUserID) & "<BR>")
                            If Not String.IsNullOrEmpty(varUserID) Then
                                If Not String.IsNullOrEmpty(Trim(myDataSet.Tables(0).Rows(varIntRows).Item(2).ToString)) Then
                                    varTransID = CStr(Trim(myDataSet.Tables(0).Rows(varIntRows).Item(2).ToString))
                                End If

                                If Not String.IsNullOrEmpty(varTransID.ToString) Then
                                    varAccID = GetAccID(Trim(myDataSet.Tables(0).Rows(varIntRows).Item(1).ToString))
                                    'If Not String.IsNullOrEmpty(varAccID.ToString) Then
                                    'If Not CheckTransID(varTransID.ToString) And Not String.IsNullOrEmpty(varTransID.ToString) Then
                                    varStrInsert = "(UserID"
                                    varStrValues = "('" & varUserID.ToString & "'"
                                    For varIntT As Integer = 0 To varIntCols - 1
                                        If Not String.IsNullOrEmpty(myDataSet.Tables(0).Rows(varIntRows).Item(varIntT).ToString) Then
                                            If String.IsNullOrEmpty(varStrInsert) Then
                                                varStrInsert = "(" & Replace(Replace(Trim(myDataSet.Tables(0).Columns(varIntT).Caption.ToString), " ", "_"), "'", "''")
                                            Else
                                                varStrInsert = varStrInsert & "," & Replace(Replace(Trim(myDataSet.Tables(0).Columns(varIntT).Caption.ToString), " ", "_"), "'", "''")
                                            End If
                                            'If varIntT = 1 Then
                                            '    If String.IsNullOrEmpty(varStrValues) Then
                                            '        varStrValues = "('" & varAccID.ToString & "'"
                                            '    Else
                                            '        varStrValues = varStrValues & ",'" & varAccID.ToString & "'"
                                            '    End If
                                            'Else
                                            '    If String.IsNullOrEmpty(varStrValues) Then
                                            '        varStrValues = "('" & myDataSet.Tables(0).Rows(varIntRows).Item(varIntT).ToString & "'"
                                            '    Else
                                            '        varStrValues = varStrValues & ",'" & myDataSet.Tables(0).Rows(varIntRows).Item(varIntT).ToString & "'"
                                            '    End If
                                            'End If
                                            If String.IsNullOrEmpty(varStrValues) Then
                                                varStrValues = "('" & Replace(Trim(myDataSet.Tables(0).Rows(varIntRows).Item(varIntT).ToString), "'", "''") & "'"
                                            Else
                                                varStrValues = varStrValues & ",'" & Replace(Trim(myDataSet.Tables(0).Rows(varIntRows).Item(varIntT).ToString), "'", "''") & "'"
                                            End If
                                        End If
                                    Next

                                    If Not String.IsNullOrEmpty(varStrInsert) Then
                                        varStrInsert = varStrInsert & ",UpdatedBy,UpdatedOn)"
                                    End If
                                    If Not String.IsNullOrEmpty(varStrValues) Then
                                        varStrValues = varStrValues & ",'" & Session("UserID").ToString & "','" & Now() & "')"
                                    End If

                                    If Not String.IsNullOrEmpty(varStrInsert) And Not String.IsNullOrEmpty(varStrValues) Then
                                        Try
                                            'Response.Write("INSERT INTO Transcend.dbo.tblMapping " & varStrInsert & " VALUES " & varStrValues)
                                            Dim objCmd1 As New Data.SqlClient.SqlCommand("INSERT INTO Transcend.dbo.tblMapping " & varStrInsert & " VALUES " & varStrValues, oConn)

                                            If objCmd1.ExecuteNonQuery() > 0 Then
                                                varCount = varCount + 1
                                            End If

                                            objCmd = Nothing
                                        Catch ex As Exception
                                            'Response.Write(ex.Message)
                                        End Try
                                    End If
                                    'Else
                                    '    If String.IsNullOrEmpty(varTransAlready.ToString) Then
                                    '        varTransAlready.Append(myDataSet.Tables(0).Rows(varIntRows).Item(2).ToString)
                                    '    Else
                                    '        varTransAlready.Append("," & myDataSet.Tables(0).Rows(varIntRows).Item(2).ToString)
                                    '    End If
                                    'End If
                                    'Else
                                    '    If String.IsNullOrEmpty(varAccName.ToString) Then
                                    '        varAccName.Append(myDataSet.Tables(0).Rows(varIntRows).Item(1).ToString)
                                    '    Else
                                    '        varAccName.Append("," & myDataSet.Tables(0).Rows(varIntRows).Item(1).ToString)
                                    '    End If
                                    'End If
                                Else
                                    varLngTNULL += 1
                                    If String.IsNullOrEmpty(varTransIDNULL.ToString) Then
                                        varTransIDNULL.Append(myDataSet.Tables(0).Rows(varIntRows).Item(0).ToString)
                                    Else
                                        If varLngTNULL = 10 Then
                                            varTransIDNULL.Append(",<BR>" & myDataSet.Tables(0).Rows(varIntRows).Item(0).ToString)
                                        Else
                                            varTransIDNULL.Append("," & myDataSet.Tables(0).Rows(varIntRows).Item(0).ToString)
                                        End If
                                    End If
                                End If
                            Else
                                varLngNotFound += 1
                                If String.IsNullOrEmpty(varNotUpdated.ToString) Then
                                    varNotUpdated.Append(myDataSet.Tables(0).Rows(varIntRows).Item(0).ToString)
                                Else
                                    If varLngNotFound = 10 Then
                                        varNotUpdated.Append(",<BR>" & myDataSet.Tables(0).Rows(varIntRows).Item(0).ToString)
                                    Else
                                        varNotUpdated.Append("," & myDataSet.Tables(0).Rows(varIntRows).Item(0).ToString)
                                    End If
                                End If
                            End If
                        End If
                    Next
                End If
            Else
                ErrLabel.Text = String.Empty
                ErrLabel.Text = "Template format mistmatch,please refer template "
                Exit Sub
            End If
            ErrLabel.Text = String.Empty
            Dim varBolflag As Boolean = False
            If Not String.IsNullOrEmpty(varNotUpdated.ToString) Then
                varBolflag = True
            End If


            If Not String.IsNullOrEmpty(varTransIDNULL.ToString) Then
                If Not String.IsNullOrEmpty(varNotUpdated.ToString) Then
                    varNotUpdated.Append("<BR> TransID empty : " & varTransIDNULL.ToString)
                Else
                    varNotUpdated.Append("TransID empty : " & varTransIDNULL.ToString)
                End If
            End If

            If Not String.IsNullOrEmpty(varTransAlready.ToString) Then
                If Not String.IsNullOrEmpty(varNotUpdated.ToString) Then
                    varNotUpdated.Append("<BR> TransID already exist : " & varTransAlready.ToString)
                Else
                    varNotUpdated.Append("TransID already exist : " & varTransAlready.ToString)
                End If
            End If

            If Not String.IsNullOrEmpty(varAccName.ToString) Then
                If Not String.IsNullOrEmpty(varNotUpdated.ToString) Then
                    varNotUpdated.Append("<BR> Account Name not found : " & varAccName.ToString)
                Else
                    varNotUpdated.Append("Account Name not found : " & varAccName.ToString)
                End If
            End If

            If String.IsNullOrEmpty(varNotUpdated.ToString) Then
                ErrLabel.Text = varCount & " records updated out of " & varCountO
            Else
                ErrLabel.Text = varCount & " records updated out of " & varCountO & "<BR>" & IIf(varBolflag, "UserNames not found : ", String.Empty) & varNotUpdated.ToString
            End If
        Catch ex As Exception
            Response.Write(ex.Message)
        Finally
            If oConn.State <> Data.ConnectionState.Closed Then
                oConn.Close()
                oConn = Nothing
            End If
        End Try
    End Sub
    Protected Sub GrdViewData_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GrdViewData.RowDataBound
        Try
            If e.Row.RowType = DataControlRowType.DataRow Then
                Dim hdnUserId As HiddenField
                hdnUserId = DirectCast(e.Row.FindControl("hdnUserID"), HiddenField)

                Dim objDDL As DropDownList
                objDDL = DirectCast(e.Row.FindControl("ddlSID"), DropDownList)

                If Not objDDL Is Nothing Then
                    BindData(objDDL, False)

                    If Not hdnUserId Is Nothing Then
                        If Not String.IsNullOrEmpty(hdnUserId.Value.ToString) Then
                            objDDL.SelectedValue = hdnUserId.Value.ToString
                        End If
                    End If
                End If

                'Dim hdnAccId As HiddenField
                'hdnAccId = DirectCast(e.Row.FindControl("hdnAccID"), HiddenField)

                'Dim ddlA As DropDownList
                'ddlA = DirectCast(e.Row.FindControl("GddlAccounts"), DropDownList)

                'If Not ddlA Is Nothing Then
                '    LoadData(ddlA)
                '    If Not hdnAccId Is Nothing Then
                '        If Not String.IsNullOrEmpty(hdnAccId.Value.ToString) Then
                '            ddlA.SelectedValue = hdnAccId.Value.ToString
                '        End If
                '    End If
                'End If

                Dim objClearance As DropDownList
                objClearance = DirectCast(e.Row.FindControl("ddlCS"), DropDownList)

                If Not objClearance Is Nothing Then
                    If String.IsNullOrEmpty(objClearance.SelectedValue) Then
                        DirectCast(e.Row.FindControl("ddlAType"), DropDownList).Enabled = False
                    Else
                        If Trim(UCase(objClearance.SelectedValue)) = Trim(UCase("Yes")) Then
                            DirectCast(e.Row.FindControl("ddlAType"), DropDownList).Enabled = True
                        Else
                            DirectCast(e.Row.FindControl("ddlAType"), DropDownList).Enabled = False
                        End If
                    End If
                    objClearance.Attributes.Add("onchange", "javascript:StatusGridDropdown('" & objClearance.ClientID & "','" & DirectCast(e.Row.FindControl("ddlAType"), DropDownList).ClientID & "')")
                End If


            ElseIf e.Row.RowType = DataControlRowType.Footer Then
                Dim objDDL1 As New DropDownList
                objDDL1 = DirectCast(e.Row.FindControl("FddlSID"), DropDownList)

                If Not objDDL1 Is Nothing Then
                    BindData(objDDL1, True)
                End If

                'Dim ddlA1 As DropDownList
                'ddlA1 = DirectCast(e.Row.FindControl("GFddlAccounts"), DropDownList)

                'If Not ddlA1 Is Nothing Then
                '    LoadData(ddlA1)
                'End If
            End If
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub
    Protected Sub BindData(ByVal ddl As DropDownList, ByVal fflag As Boolean)
        ddl.Items.Clear()
        Dim oConn As New Data.SqlClient.SqlConnection
        oConn.ConnectionString = System.Configuration.ConfigurationManager.AppSettings("ETSCon")
        Try
            oConn.Open()
            Dim objCmd As New Data.SqlClient.SqlCommand("SELECT UserID,ISNULL(FirstName,'')+' '+ISNULL(LastName,'')+ ' ('+ UserName + ') ' AS 'UName' FROM DBO.tblUsers where  " & IIf(fflag = True, " (Isdeleted is NULL or Isdeleted = 'False') AND ", String.Empty) & " UserID<>'11111111-1111-1111-1111-111111111111' ORDER BY FirstName,LastName", oConn)
            Dim objRec As Data.SqlClient.SqlDataReader = objCmd.ExecuteReader
            If objRec.HasRows Then
                While objRec.Read
                    ddl.Items.Add(New ListItem(objRec("UName").ToString, objRec("UserID").ToString))
                End While
            End If
            objRec.Close()
            objRec = Nothing
            objCmd = Nothing

            If fflag Then
                ddl.Items.Insert(0, New ListItem("Please Select", String.Empty))
            End If
        Catch ex As Exception
            'Response.Write(ex.Message)
        Finally
            If oConn.State <> Data.ConnectionState.Closed Then
                oConn.Close()
                oConn = Nothing
            End If
        End Try
    End Sub
    Protected Function GetUserName(ByVal UserID As String) As String
        Dim varReturn As String = String.Empty
        Dim oConn As New Data.SqlClient.SqlConnection
        oConn.ConnectionString = System.Configuration.ConfigurationManager.AppSettings("ETSCon")
        Try
            oConn.Open()
            Dim objCmd As New Data.SqlClient.SqlCommand("SELECT UserName FROM DBO.tblUsers where  (Isdeleted is NULL or Isdeleted = 'False') and UserID<>'11111111-1111-1111-1111-111111111111' AND UserId='" & UserID.ToString & "'", oConn)
            Dim objRec As Data.SqlClient.SqlDataReader = objCmd.ExecuteReader
            If objRec.HasRows Then
                While objRec.Read
                    varReturn = objRec("UserName").ToString
                End While
            End If
            objRec.Close()
            objRec = Nothing
            objCmd = Nothing

            
        Catch ex As Exception
            'Response.Write(ex.Message)
        Finally
            If oConn.State <> Data.ConnectionState.Closed Then
                oConn.Close()
                oConn = Nothing
            End If
        End Try
        Return varReturn
    End Function
    Protected Function setStatus(ByVal Status) As String
        Dim varReturn As String = String.Empty
        Try
            If String.IsNullOrEmpty(Status) Then
                varReturn = "Active"
            Else
                If Status Then
                    varReturn = "Inactive"
                Else
                    varReturn = "Active"
                End If
            End If
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
        Return varReturn
    End Function
    Protected Function CheckStatus(ByVal Status As String) As Boolean
        Dim varWhere As String = String.Empty
        If Not String.IsNullOrEmpty(Status) Then
            If Trim(UCase(Status)) = Trim(UCase("Active")) Then
                varWhere = " WHERE (U.IsDeleted=0 OR U.IsDeleted IS NULL) "
            ElseIf Trim(UCase(Status)) = Trim(UCase("Active")) Then
                varWhere = " WHERE (U.IsDeleted=1) "
            End If
        End If
        Dim oConn As New Data.SqlClient.SqlConnection
        oConn.ConnectionString = System.Configuration.ConfigurationManager.AppSettings("ETSCon")
        Dim varReturn As Boolean = False
        Try
            oConn.Open()

            Dim objCmd As New Data.SqlClient.SqlCommand("SELECT Count(*) AS 'Count' FROM DBO.tblUsers U INNER JOIN Transcend.dbo.tblMapping M ON U.UserID=M.UserID" & varWhere.ToString, oConn)
            Dim objRec As Data.SqlClient.SqlDataReader = objCmd.ExecuteReader
            If objRec.HasRows Then
                While objRec.Read
                    If objRec("Count") > 0 Then
                        varReturn = True
                    End If
                End While
            End If
            objRec.Close()
            objRec = Nothing
            objCmd = Nothing

        Catch ex As Exception
        Finally
            If oConn.State <> Data.ConnectionState.Closed Then
                oConn.Close()
                oConn = Nothing
            End If
        End Try
        Return varReturn
    End Function
End Class
