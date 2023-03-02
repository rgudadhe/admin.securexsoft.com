Imports MainModule
Imports System.Data
Partial Class NewTicket
    Inherits BasePage
    Dim objMainModule As New MainModule
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim varDropDownCateItem As New ListItem
        varDropDownCateItem.Text = "Please Select"
        varDropDownCateItem.Value = ""
        If Not Page.IsPostBack Then
            Dim objConn As New Data.SqlClient.SqlConnection
            objConn = objMainModule.OpenConnection(objConn)

            Try
                Dim objSQLAdapter As Data.SqlClient.SqlDataAdapter = New Data.SqlClient.SqlDataAdapter("SELECT IssueName ,IssueID FROM dbo.tblCustomerIssueType WHERE (IsDeleted IS NULL OR IsDeleted ='False')", objConn)
                Dim objDataSet As New DataSet
                objSQLAdapter.Fill(objDataSet, "tblCustomerIssueType")
                DropDownIssueType.DataSource = objDataSet
                DropDownIssueType.DataTextField = "IssueName"
                DropDownIssueType.DataValueField = "IssueID"
                DropDownIssueType.DataBind()
                DropDownIssueType.Items.Insert(0, varDropDownCateItem)

            Catch ex As Exception
            Finally
                If objConn.State <> ConnectionState.Closed Then
                    objConn.Close()
                    objConn = Nothing
                End If
            End Try
        End If
    End Sub
    Protected Sub BtnAddNew_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtnAddNew.Click
        Dim varStrIssueID As String
        Dim varStrTicketDetails As String
        Dim varStrPriority As String
        Dim varStrFileAttach As String
        Dim varStrInsert As String

        varStrIssueID = DropDownIssueType.Items(DropDownIssueType.SelectedIndex).Value.ToString
        varStrTicketDetails = txtIssueDesc.InnerText.ToString
        varStrPriority = DropDownPriority.Items(DropDownPriority.SelectedIndex).Value.ToString
        If FileUploadAttachment.FileName.ToString <> "" Then
            varStrFileAttach = FileUploadAttachment.FileName.ToString
            varStrInsert = "INSERT INTO tblCustomerTickets(AccID,UserID,IssueID,TicketDetails,AttachFile,Priority,Status,DatePosted)VALUES('" & Session("AccID") & "','" & Session("UserID") & "','" & varStrIssueID & "','" & varStrTicketDetails & "','" & varStrFileAttach & "','" & varStrPriority & "','Open','" & Now() & "')"
        Else
            varStrInsert = "INSERT INTO tblCustomerTickets(AccID,UserID,IssueID,TicketDetails,Priority,Status,DatePosted)VALUES('" & Session("AccID") & "','" & Session("UserID") & "','" & varStrIssueID & "','" & varStrTicketDetails & "','" & varStrPriority & "','Open','" & Now() & "')"
        End If

        Dim objConn As New Data.SqlClient.SqlConnection
        objConn = objMainModule.OpenConnection(objConn)

        Try
            'Add Issueype
            Dim InsertCmd As New Data.SqlClient.SqlCommand
            InsertCmd.Connection = objConn
            InsertCmd.CommandType = Data.CommandType.Text
            InsertCmd.CommandText = varStrInsert
            InsertCmd.ExecuteNonQuery()
            InsertCmd = Nothing
            'End Add Issuetype
        Catch ex As Exception
        Finally
            If objConn.State <> ConnectionState.Closed Then
                objConn.Close()
                objConn = Nothing
            End If
        End Try
        
        ClientScript.RegisterStartupScript(Me.GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Ticket Raise successfully');</script>")
    End Sub
End Class
