Imports MainModule
Imports System.Data
Partial Class SearchTicketResults
    Inherits BasePage
    Dim varMainStr As String
    Dim varSortDir As String
    Dim objMainModule As New MainModule
    Dim varOldSortDir As String
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim varWhere As String = String.Empty
        Dim varStartDate As String = String.Empty
        Dim varEndDate As String = String.Empty
        Dim varState As String
        Dim varIssueID As String
        Dim varTicketNo As String
        Dim varUserID As String
        'Response.Write("Testing Purpose,please ignore it.")

        Try
            If Not Page.IsPostBack Then
                Dim varMainStr1 As String = String.Empty
                Dim varMainStr2 As String = String.Empty

                varMainStr1 = " SELECT T.*,U.FirstName,U.LastName,U.UserName,I.IssueName,CateName,(DATEDIFF(n,DatePosted,getdate())%60) AS DateDiffMin,DATEDIFF(hh,DatePosted,getdate()) AS DateDiffHr,'No' AS 'Forward' FROM dbo.tblTickets T INNER JOIN dbo.tblUsers U ON T.UserID=U.UserID INNER JOIN dbo.tblIssueType I ON T.IssueID=I.IssueID INNER JOIN dbo.tblIssueCategory IC ON IC.CategoryID=I.CategoryID LEFT JOIN dbo.tblERSSTicketsAccess TA ON T.IssueID=TA.IssueID WHERE TA.UserID='" & Session("UserID").ToString & "' AND T.DepartmentID IS NULL AND U.ContractorID='" & Session("ContractorID").ToString & "' "
                varMainStr2 = " UNION SELECT T.*,U.FirstName,U.LastName,U.UserName,I.IssueName,CateName,(DATEDIFF(n,DatePosted,getdate())%60) AS DateDiffMin,DATEDIFF(hh,DatePosted,getdate()) AS DateDiffHr,'Yes' AS 'Forward' FROM dbo.tblTickets T INNER JOIN dbo.tblUsers U ON T.UserID=U.UserID INNER JOIN dbo.tblIssueType I ON T.IssueID=I.IssueID INNER JOIN dbo.tblIssueCategory IC ON IC.CategoryID=I.CategoryID WHERE U.ContractorID='" & Session("ContractorID").ToString & "' AND (T.UserAssignID='" & Session("UserID").ToString & "' OR T.DepartmentId=(SELECT DepartmentID FROM tblUsers WHERE UserId='" & Session("UserID").ToString & "') ) "

                'varMainStr = "(SELECT T.*,U.FirstName,U.LastName,U.UserName,I.IssueName,CateName,(DATEDIFF(n,DatePosted,getdate())%60) AS DateDiffMin,DATEDIFF(hh,DatePosted,getdate()) AS DateDiffHr,'No' AS 'Forward' FROM dbo.tblTickets T INNER JOIN dbo.tblUsers U ON T.UserID=U.UserID INNER JOIN dbo.tblIssueType I ON T.IssueID=I.IssueID INNER JOIN dbo.tblIssueCategory IC ON IC.CategoryID=I.CategoryID LEFT JOIN dbo.tblERSSTicketsAccess TA ON T.IssueID=TA.IssueID WHERE TA.UserID='" & Session("UserID") & "' AND T.DepartmentID IS NULL UNION SELECT T.*,U.FirstName,U.LastName,U.UserName,I.IssueName,CateName,(DATEDIFF(n,DatePosted,getdate())%60) AS DateDiffMin,DATEDIFF(hh,DatePosted,getdate()) AS DateDiffHr,'Yes' AS 'Forward' FROM dbo.tblTickets T INNER JOIN dbo.tblUsers U ON T.UserID=U.UserID INNER JOIN dbo.tblIssueType I ON T.IssueID=I.IssueID INNER JOIN dbo.tblIssueCategory IC ON IC.CategoryID=I.CategoryID WHERE (T.UserAssignID='" & Session("UserID") & "' OR T.DepartmentId=(SELECT DepartmentID FROM tblUsers WHERE UserId='" & Session("UserID") & "')) ) ORDER BY DatePosted DESC "
                'varMainStr = " SELECT T.*,U.FirstName,U.LastName,U.UserName,I.IssueName FROM dbo.tblTickets T INNER JOIN dbo.tblUsers U ON T.UserID=U.UserID INNER JOIN dbo.tblIssueType I ON T.IssueID=I.IssueID LEFT JOIN dbo.tblERSSTicketsAccess TA ON T.IssueID=TA.IssueID "
                'varWhere = "  WHERE T.TicketID IS NOT NULL AND TA.UserID='" & Session("UserID") & "'"


                If Request.Form("txtStartDate") <> "" Then
                    varStartDate = Request.Form("txtStartDate").ToString
                End If

                If Request.Form("txtEndDate") <> "" Then
                    varEndDate = Request.Form("txtEndDate").ToString
                End If

                If String.IsNullOrEmpty(varStartDate) = False And String.IsNullOrEmpty(varEndDate) = True Then
                    varWhere = varWhere & " AND  T.DatePosted >= '" & varStartDate & "'"
                End If
                If String.IsNullOrEmpty(varStartDate) = True And String.IsNullOrEmpty(varEndDate) = False Then
                    varWhere = varWhere & " AND T.DatePosted <= '" & varEndDate & "'"
                End If
                If String.IsNullOrEmpty(varStartDate) = False And String.IsNullOrEmpty(varEndDate) = False Then
                    varWhere = varWhere & " AND T.DatePosted between '" & varStartDate & "' AND '" & varEndDate & "'"
                End If

                If Request.Form("DropDownStatus") <> "" Then
                    varState = Request.Form("DropDownStatus")
                    varWhere = varWhere & "  AND T.Status='" & varState & "' "
                End If

                If Request.Form("DropDownIssueTypes") <> "" Then
                    varIssueID = Request.Form("DropDownIssueTypes")
                    varWhere = varWhere & " AND T.IssueID='" & varIssueID & "' "
                End If

                If Request.Form("txtTicketNo") <> "" Then
                    varTicketNo = Request.Form("txtTicketNo")
                    varWhere = varWhere & " AND T.TicketNo=" & varTicketNo & ""
                End If

                If Request.Form("txtUserID") <> "" Then
                    varUserID = Request.Form("txtUserID")
                    varWhere = varWhere & " AND T.UserID='" & varUserID & "' "
                End If

                varMainStr1 = varMainStr1 & varWhere
                varMainStr2 = varMainStr2 & varWhere

                varMainStr = "(" & varMainStr1 & varMainStr2 & ")  ORDER BY DatePosted DESC  "

                Session("varMainStr") = varMainStr

                FillGrid(varMainStr)
                varOldSortDir = ""
            End If
        Catch ex As Exception
        End Try
    End Sub
    Protected Sub FillGrid(ByVal SQLString As String)
        Dim objConn As New Data.SqlClient.SqlConnection
        objConn = objMainModule.OpenConnection(objConn)
        Try
            Dim objSQLAdapter As Data.SqlClient.SqlDataAdapter = New Data.SqlClient.SqlDataAdapter(SQLString, objConn)
            Dim objDataSet As New DataSet
            objSQLAdapter.Fill(objDataSet, "tblTickets")
            GridViewSearchResults.DataSource = objDataSet
            GridViewSearchResults.DataSource = objDataSet
            GridViewSearchResults.DataBind()
        Catch ex As Exception
        Finally
            If objConn.State <> ConnectionState.Closed Then
                objConn.Close()
                objConn = Nothing
            End If
        End Try
    End Sub
    Protected Sub GridViewSearchResults_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles GridViewSearchResults.RowCommand
        Try
            If e.CommandName = "ActionTicket" Then
                Dim varStrTicketID As String
                varStrTicketID = e.CommandArgument.ToString
                Response.Redirect("ActionTicket.aspx?ID=" & varStrTicketID & "&From=Search")
            End If
        Catch ex As Exception
        End Try
    End Sub
    Protected Sub GridViewSearchResults_Sorting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewSortEventArgs) Handles GridViewSearchResults.Sorting
        Try
            Dim varSortString As String
            Dim varSQLString As String

            If varSortDir = String.Empty Then
                GetSortDirection()
            End If

            varSortString = e.SortExpression
            varSQLString = Session("varMainStr") & " ORDER BY  " & varSortString & " " & GetSortDirection()
            FillGrid(varSQLString)
        Catch ex As Exception
        End Try
    End Sub
    Public Property gvSortDir()
        Get
            Return IIf(ViewState("SortDirection") = Nothing, "ASC", ViewState("SortDirection"))
        End Get
        Set(ByVal value)
            ViewState("SortDirection") = value
        End Set
    End Property
    'This procedure returns the Sort Direction
    Public Function GetSortDirection() As String
        Try
            Select Case (gvSortDir())
                Case "ASC"
                    gvSortDir = "DESC"
                Case "DESC"
                    gvSortDir = "ASC"
            End Select
            Return gvSortDir()
        Catch ex As Exception
        End Try
    End Function
    Protected Sub GridViewSearchResults_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles GridViewSearchResults.PageIndexChanging
        Try
            GridViewSearchResults.PageIndex = e.NewPageIndex
            FillGrid(Session("varMainStr"))
        Catch ex As Exception
        End Try
    End Sub
    Protected Sub Page_Unload(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Unload
        'Session("varMainStr") = Nothing
    End Sub
End Class
