Imports MainModule
Imports System.Data
Partial Class SearchTicketsForm
    Inherits BasePage
    Dim objMainModule As New MainModule
    Protected Sub btnSubmit_ServerClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSubmit.ServerClick
        Server.Transfer("SearchTicketResults.aspx", True)
        'Response.Redirect("SearchTicketResults.aspx")
    End Sub
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim objConn As New Data.SqlClient.SqlConnection
        objConn = objMainModule.OpenConnection(objConn)
        Try
            If Not Page.IsPostBack Then
                Dim varListItem As New ListItem
                varListItem.Text = "Select Issue Type"
                varListItem.Value = ""
                Dim objSQLAdapter As Data.SqlClient.SqlDataAdapter = New Data.SqlClient.SqlDataAdapter("SELECT IssueID,IssueName FROM dbo.tblIssueType WHERE (IsDeleted='False' OR IsDeleted IS NULL) ", objConn)
                Dim objDataSet As New DataSet
                objSQLAdapter.Fill(objDataSet, "tblIssueType")
                DropDownIssueTypes.DataSource = objDataSet
                DropDownIssueTypes.DataTextField = "IssueName"
                DropDownIssueTypes.DataValueField = "IssueID"
                DropDownIssueTypes.DataBind()
                DropDownIssueTypes.Items.Insert(0, varListItem)
            End If
        Catch ex As Exception
        Finally
            If objConn.State <> ConnectionState.Closed Then
                objConn.Close()
                objConn = Nothing
            End If
        End Try
    End Sub
End Class
