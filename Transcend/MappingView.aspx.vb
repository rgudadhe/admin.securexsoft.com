
Partial Class Transcend_MappingView
    Inherits BasePage
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

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
    Private Function DoSearch(ByVal UserName As String, ByVal AccName As String, ByVal Status As String, ByVal Sort As String, ByVal Dir As String) As Boolean
        lblResponse.Text = String.Empty

        Dim oConn As New Data.SqlClient.SqlConnection
        oConn.ConnectionString = System.Configuration.ConfigurationManager.AppSettings("ETSCon")

        Try

            oConn.Open()
            hdnUserID.Value = String.Empty
            'Get the UserID,UserName from give user
            Dim oCommand As New Data.SqlClient.SqlCommand("SELECT UserID,UserName FROM DBO.tblUsers where UserID<>'11111111-1111-1111-1111-111111111111' AND FirstName+' '+LastName LIKE '" & UserName & "'", oConn)
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
                    Dim SQLString = "SELECT M.TrackID,U.UserID,U.OfficialMailID,U.ChatID,U.OtherMailID,ISNULL(FirstName,'')+' '+ISNULL(LastName,'') as UName,Resource_Name AS 'SecureITID',M.ID AS 'TranscendID',Account,M.Name,M.User_Name,M.Password,M.Juniper_Password,M.Console_Password,M.ClearanceStatus,M.Mentor,M.QA,M.Comment,M.AuditType,U.IsDeleted AS 'Status', M.ChartScriptID FROM DBO.tblUsers U INNER JOIN Transcend.dbo.tblMapping M ON U.UserID=M.UserID where   U.UserID<>'11111111-1111-1111-1111-111111111111' " & varWhere.ToString & " ORDER BY " & Sort & " " & Dir & " "
                    Dim objDA As New System.Data.SqlClient.SqlDataAdapter(SQLString, oConn)
                    Dim objDS As New System.Data.DataSet
                    objDA.Fill(objDS, "tblMapping")

                    Dim objDataTable As New System.Data.DataTable
                    objDataTable = objDS.Tables(0)

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
                Else
                    lblResponse.Text = String.Empty
                    lblResponse.Text = "Records not found"
                    Exit Function
                End If
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
    Protected Function CheckUserID(ByVal UserID As String) As Boolean
        Dim oConn As New Data.SqlClient.SqlConnection
        oConn.ConnectionString = System.Configuration.ConfigurationManager.AppSettings("ETSCon")
        Dim varReturn As Boolean = False
        Try
            oConn.Open()

            Dim objCmd As New Data.SqlClient.SqlCommand("SELECT Count(*) AS 'Count' FROM Transcend.dbo.tblMapping WHERE UserID='" & UserID.ToString & "' AND IsDeleted IS NULL ", oConn)
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

            'SQLString = "SELECT ISNULL(FirstName,'')+' '+ISNULL(LastName,'') as UName,Resource_Name AS 'SecureITID',M.ID AS 'TranscendID',A.AccName AS 'Account',M.Name,M.User_Name,M.Password,M.Juniper_Password,M.Console_Password,M.ClearanceStatus,M.Mentor,M.QA FROM tblUsers U INNER JOIN Transcend.dbo.tblMapping M ON U.UserID=M.UserID INNER JOIN Transcend.dbo.tblAccounts A ON M.Account=A.AccID where  (U.Isdeleted is NULL or U.Isdeleted = 'False') and (M.Isdeleted is NULL or M.Isdeleted = 'False') and  U.UserID<>'11111111-1111-1111-1111-111111111111'"
            SQLString = "SELECT ISNULL(FirstName,'')+' '+ISNULL(LastName,'') as UName,U.OfficialMailID,U.OtherMailID,U.ChatID,Resource_Name AS 'SecureITID',M.ID AS 'TranscendID',Account,M.Name,M.User_Name,M.Password,M.Juniper_Password,M.Console_Password,M.ClearanceStatus,M.Mentor,M.QA,M.Comment,M.AuditType,U.IsDeleted AS 'Status', M.ChartScriptID FROM DBO.tblUsers U INNER JOIN Transcend.dbo.tblMapping M ON U.UserID=M.UserID WHERE  (M.Isdeleted is NULL or M.Isdeleted = 'False') and  U.UserID<>'11111111-1111-1111-1111-111111111111'"

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
        End Try
        Return varReturn
    End Function
End Class

