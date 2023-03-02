
Partial Class Transcend_Accounts
    Inherits BasePage
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Hsort.Value = "" Then
                Hsort.Value = "AccName"
            End If
            If Horder.Value = "" Then
                Horder.Value = " DESC"
            End If

            If Not Page.IsPostBack Then
                FillData(Hsort.Value, Horder.Value)
            End If
        Catch ex As Exception
            'ClientScript.RegisterClientScriptBlock(Page.GetType, "Script", "<script language='javascript'>alert('" & ex.Message & "');</script>")
        End Try
    End Sub
    Protected Sub FillData(ByVal Sort As String, ByVal Dir As String)
        Dim objConn As New Data.SqlClient.SqlConnection
        Try
            objConn = OpenConnection(objConn)
            Dim varStrQuery As String = String.Empty
            Dim objDataSetReturn As Data.DataSet
            Dim objCmd As Data.SqlClient.SqlCommand
            Dim objDAtaAdapter As Data.SqlClient.SqlDataAdapter

            varStrQuery = "SELECT * FROM Transcend.dbo.tblAccounts WHERE IsDeleted IS NULL ORDER BY " & Sort & " " & Dir & " "

            objCmd = New Data.SqlClient.SqlCommand(varStrQuery, objConn)
            objDAtaAdapter = New Data.SqlClient.SqlDataAdapter(objCmd)
            objDataSetReturn = New Data.DataSet
            objDAtaAdapter.Fill(objDataSetReturn)

            If objDataSetReturn.Tables(0).Rows.Count > 0 Then
                GridViewStages.DataSource = objDataSetReturn.Tables(0)
                GridViewStages.DataBind()
            Else
                Dim t As Data.DataTable = objDataSetReturn.Tables(0).Clone

                For Each c As Data.DataColumn In t.Columns
                    c.AllowDBNull = True
                Next

                t.Rows.Add(t.NewRow())
                GridViewStages.DataSource = t
                GridViewStages.DataBind()
                GridViewStages.Rows(0).Visible = False
                GridViewStages.Rows(0).Controls.Clear()
            End If
        Catch ex As Exception
            'ClientScript.RegisterClientScriptBlock(Page.GetType, "Script", "<script language='javascript'>alert('" & ex.Message & "');</script>")
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
    Protected Sub GridViewStages_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles GridViewStages.PageIndexChanging
        FillData(Hsort.Value, Horder.Value)
        GridViewStages.PageIndex = e.NewPageIndex
        GridViewStages.DataBind()
    End Sub
    Protected Sub GridViewStages_RowCancelingEdit(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCancelEditEventArgs) Handles GridViewStages.RowCancelingEdit
        GridViewStages.EditIndex = -1
        FillData(Hsort.Value, Horder.Value)
    End Sub
    Protected Sub GridViewStages_RowUpdating(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewUpdateEventArgs) Handles GridViewStages.RowUpdating
        ErrLabel.Text = String.Empty
        Dim objConn As New Data.SqlClient.SqlConnection
        Try
            Dim varStrDocName As String = String.Empty
            Dim varStrAccID As String
            Dim varSQLUpdateQuery As String
            Dim varType As String = String.Empty

            Dim varAccName As TextBox
            varAccName = DirectCast(GridViewStages.Rows(e.RowIndex).FindControl("txtAccEdit"), TextBox)

            Dim varTempDDL As DropDownList
            varTempDDL = DirectCast(GridViewStages.Rows(e.RowIndex).FindControl("ddlType"), DropDownList)
            varType = varTempDDL.Items(varTempDDL.SelectedIndex).Value.ToString

            varStrDocName = varAccName.Text.ToString
            If String.IsNullOrEmpty(varStrDocName) Then
                ErrLabel.Text = String.Empty
                ErrLabel.Text = "Please enter account name "
                Exit Sub
            End If

            If String.IsNullOrEmpty(varType) Then
                ErrLabel.Text = String.Empty
                ErrLabel.Text = "Please select account type "
                Exit Sub
            End If

            Dim varCID As Long = 0
            If Not String.IsNullOrEmpty(DirectCast(GridViewStages.Rows(e.RowIndex).FindControl("txtCID"), TextBox).Text.ToString) Then
                varCID = CLng(DirectCast(GridViewStages.Rows(e.RowIndex).FindControl("txtCID"), TextBox).Text.ToString)
            End If


            If String.IsNullOrEmpty(varType) = False Then
                If Trim(UCase(varType)) = Trim(UCase("eScription")) Then
                    If varCID > 0 Then
                        ErrLabel.Text = String.Empty
                        ErrLabel.Text = "Customer ID only assign for BeyondTXT accounts "
                        Exit Sub
                    End If
                ElseIf Trim(UCase(varType)) = Trim(UCase("BeyondTXT")) Then
                    If varCID = 0 Then
                        ErrLabel.Text = String.Empty
                        ErrLabel.Text = "Please enter Customer ID for BeyondTXT accounts "
                        Exit Sub
                    End If
                End If
            End If

            Dim varDDL As HiddenField
            varDDL = DirectCast(GridViewStages.Rows(e.RowIndex).FindControl("AccID"), HiddenField)
            varStrAccID = varDDL.Value

            If CheckAccName(varStrDocName, True, varStrAccID.ToString) Then
                ErrLabel.Text = String.Empty
                ErrLabel.Text = "Account Name already exist,please user different account name "
                Exit Sub
            Else
                If CheckAccCID(varStrDocName, True, varStrAccID.ToString, varCID) Then
                    ErrLabel.Text = String.Empty
                    ErrLabel.Text = "Customer ID already exist,please user different customer id "
                    Exit Sub
                Else
                    objConn = OpenConnection(objConn)

                    varSQLUpdateQuery = "UPDATE Transcend.dbo.tblAccounts SET AccName='" & Replace(varStrDocName, "'", "''") & "',Type='" & varType.ToString & "'" & IIf(varCID > 0, ",CustomerID=" & varCID & "", String.Empty) & ",UpdatedBy='" & Session("UserID").ToString & "',UpdatedOn='" & Now() & "' WHERE AccID='" & varStrAccID & "' "
                    Dim UpdateCmd As New Data.SqlClient.SqlCommand
                    UpdateCmd.CommandType = Data.CommandType.Text
                    UpdateCmd.CommandText = varSQLUpdateQuery
                    UpdateCmd.Connection = objConn
                    UpdateCmd.ExecuteNonQuery()
                    UpdateCmd = Nothing

                    GridViewStages.EditIndex = -1
                    FillData(Hsort.Value, Horder.Value)
                    ErrLabel.Text = String.Empty
                    ErrLabel.Text = "Record updated "
                    Exit Sub
                End If
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
    Protected Sub GridViewStages_RowEditing(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewEditEventArgs) Handles GridViewStages.RowEditing
        GridViewStages.EditIndex = e.NewEditIndex
        FillData(Hsort.Value, Horder.Value)
    End Sub
    Protected Sub GridViewStages_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles GridViewStages.RowCommand
        ErrLabel.Text = String.Empty
        If Trim(UCase(e.CommandName)) = Trim(UCase("AddNew")) Then
            Dim objConn As New Data.SqlClient.SqlConnection

            Try
                Dim varStrDocName As String = String.Empty
                varStrDocName = DirectCast(GridViewStages.FooterRow.FindControl("txtAcc"), TextBox).Text

                Dim varType As String = String.Empty
                varType = DirectCast(GridViewStages.FooterRow.FindControl("FddlType"), DropDownList).SelectedValue.ToString

                If String.IsNullOrEmpty(varStrDocName) Then
                    ErrLabel.Text = String.Empty
                    ErrLabel.Text = "Please enter account name "
                    Exit Sub
                End If

                If String.IsNullOrEmpty(varType) Then
                    ErrLabel.Text = String.Empty
                    ErrLabel.Text = "Please select account type "
                    Exit Sub
                End If

                Dim varCID As Long = 0
                If Not String.IsNullOrEmpty(DirectCast(GridViewStages.FooterRow.FindControl("FtxtCID"), TextBox).Text.ToString) Then
                    varCID = CLng(DirectCast(GridViewStages.FooterRow.FindControl("FtxtCID"), TextBox).Text.ToString)
                End If

                If String.IsNullOrEmpty(varType) = False Then
                    If Trim(UCase(varType)) = Trim(UCase("eScription")) Then
                        If varCID > 0 Then
                            ErrLabel.Text = String.Empty
                            ErrLabel.Text = "Customer ID only assign for BeyondTXT accounts "
                            Exit Sub
                        End If
                    ElseIf Trim(UCase(varType)) = Trim(UCase("BeyondTXT")) Then
                        If varCID = 0 Then
                            ErrLabel.Text = String.Empty
                            ErrLabel.Text = "Please enter Customer ID for BeyondTXT accounts "
                            Exit Sub
                        End If
                    End If
                End If

                If CheckAccName(varStrDocName, False, String.Empty) Then
                    ErrLabel.Text = String.Empty
                    ErrLabel.Text = "Account Name already exist,please user different account name "
                    Exit Sub
                Else
                    If CheckAccCID(varStrDocName, False, String.Empty, varCID) Then
                        ErrLabel.Text = String.Empty
                        ErrLabel.Text = "Customer ID already exist,please user different customer id "
                        Exit Sub
                    Else
                        objConn = OpenConnection(objConn)
                        Dim varStrInsert As String
                        varStrInsert = "INSERT INTO Transcend.dbo.tblAccounts(AccName,Type " & IIf(varCID > 0, ",CustomerID", String.Empty) & ",UpdatedBy,UpdatedOn) VALUES('" & varStrDocName & "','" & varType.ToString & "'" & IIf(varCID > 0, "," & varCID & "", String.Empty) & ",'" & Session("UserID").ToString & "','" & Now() & "')"

                        Dim InsertCmd As New Data.SqlClient.SqlCommand
                        InsertCmd.CommandType = Data.CommandType.Text
                        InsertCmd.CommandText = varStrInsert
                        InsertCmd.Connection = objConn
                        InsertCmd.ExecuteNonQuery()
                        InsertCmd = Nothing
                        FillData(Hsort.Value, Horder.Value)

                        ErrLabel.Text = String.Empty
                        ErrLabel.Text = "Record submitted "
                        Exit Sub

                    End If
                End If
            Catch ex As Exception
                'Response.Write(ex.Message)
            Finally
                If objConn.State <> Data.ConnectionState.Closed Then
                    objConn.Close()
                    objConn = Nothing
                End If
            End Try
        End If
    End Sub
    Protected Sub GridViewStages_RowDeleting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewDeleteEventArgs) Handles GridViewStages.RowDeleting
        Dim objConn As New Data.SqlClient.SqlConnection
        objConn = OpenConnection(objConn)
        Try
            Dim varStrID As String
            Dim varSQLDeleteQuery As String

            Dim varDDL As HiddenField
            varDDL = DirectCast(GridViewStages.Rows(e.RowIndex).FindControl("AccIDDel"), HiddenField)
            varStrID = varDDL.Value

            varSQLDeleteQuery = "UPDATE Transcend.dbo.tblAccounts SET IsDeleted=1,UpdatedBy='" & Session("UserId").ToString & "',UpdatedOn='" & Now() & "' WHERE AccID='" & varStrID & "'"

            Dim UpdateCmd As New Data.SqlClient.SqlCommand
            UpdateCmd.CommandType = Data.CommandType.Text
            UpdateCmd.CommandText = varSQLDeleteQuery
            UpdateCmd.Connection = objConn
            UpdateCmd.ExecuteNonQuery()
            UpdateCmd = Nothing

            FillData(Hsort.Value, Horder.Value)

        Catch ex As Exception
            'ClientScript.RegisterStartupScript(Me.GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert(" & ex.Message & ");</script>")
        Finally
            If objConn.State <> Data.ConnectionState.Closed Then
                objConn.Close()
                objConn = Nothing
            End If
        End Try
    End Sub
    Protected Sub GridViewStages_Sorting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewSortEventArgs) Handles GridViewStages.Sorting
        Try
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
            FillData(Hsort.Value, Horder.Value)
        Catch ex As Exception
        End Try
    End Sub
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
    Public Function CheckAccName(ByVal AccName As String, ByVal Uflag As Boolean, ByVal UAccID As String) As Boolean
        Dim objConn As New Data.SqlClient.SqlConnection
        objConn = OpenConnection(objConn)
        Dim varReturn As Boolean = False
        Dim varQuery As String = String.Empty
        Try
            If Uflag Then
                varQuery = "SELECT Count(*) AS 'Count' FROM Transcend.dbo.tblAccounts WHERE AccName='" & AccName.ToString & "' AND IsDeleted IS NULL AND AccID<>'" & UAccID.ToString & "' "
            Else
                varQuery = "SELECT Count(*) AS 'Count' FROM Transcend.dbo.tblAccounts WHERE AccName='" & AccName.ToString & "' AND IsDeleted IS NULL "
            End If

            Dim objCmd As New Data.SqlClient.SqlCommand(varQuery, objConn)
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
            If objConn.State <> Data.ConnectionState.Closed Then
                objConn.Close()
                objConn = Nothing
            End If
        End Try
        Return varReturn
    End Function
    Public Function CheckAccCID(ByVal AccName As String, ByVal Uflag As Boolean, ByVal UAccID As String, ByVal CID As Long) As Boolean
        Dim objConn As New Data.SqlClient.SqlConnection
        objConn = OpenConnection(objConn)
        Dim varReturn As Boolean = False
        Dim varQuery As String = String.Empty
        Try
            If Uflag Then
                varQuery = "SELECT Count(*) AS 'Count' FROM Transcend.dbo.tblAccounts WHERE IsDeleted IS NULL AND CustomerID=" & CID & " AND AccID<>'" & UAccID.ToString & "' "
            Else
                varQuery = "SELECT Count(*) AS 'Count' FROM Transcend.dbo.tblAccounts WHERE IsDeleted IS NULL AND CustomerID=" & CID & ""
            End If

            Dim objCmd As New Data.SqlClient.SqlCommand(varQuery, objConn)
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
            If objConn.State <> Data.ConnectionState.Closed Then
                objConn.Close()
                objConn = Nothing
            End If
        End Try
        Return varReturn
    End Function
End Class
