Imports MainModule
Partial Class IssueType
    Inherits System.Web.UI.Page
    Dim objMainModule As New MainModule
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        objMainModule.oConn.Open()
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
                varStrInsert = "INSERT INTO dbo.tblCustomerIssueType(IssueName,IssueDesc,DateModified,ModifiedBy)VALUES('" & varStrIssueType & "','" & varStrIssueDesc & "','" & Now() & "','" & Session("UserID").ToString() & "')"

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
End Class
