Imports MainModule
Imports System.Data
Partial Class NewTicket
    Inherits System.Web.UI.Page
    Dim objMainModule As New MainModule
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        objMainModule.oConn.Open()
        Dim varDropDownCateItem As New ListItem
        varDropDownCateItem.Text = "Please Select"
        varDropDownCateItem.Value = ""
        If Not Page.IsPostBack Then
            Dim objSQLAdapter As Data.SqlClient.SqlDataAdapter = New Data.SqlClient.SqlDataAdapter("SELECT IssueName ,IssueID FROM dbo.tblCustomerIssueType WHERE (IsDeleted IS NULL OR IsDeleted ='False')", objMainModule.oConn)
            Dim objDataSet As New DataSet
            objSQLAdapter.Fill(objDataSet, "tblCustomerIssueType")
            DropDownIssueType.DataSource = objDataSet
            DropDownIssueType.DataTextField = "IssueName"
            DropDownIssueType.DataValueField = "IssueID"
            DropDownIssueType.DataBind()
            DropDownIssueType.Items.Insert(0, varDropDownCateItem)
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
            varStrInsert = "INSERT INTO tblCustomerTickets(AccID,UserID,IssueID,TicketDetails,AttachFile,Priority,Status,DatePosted)VALUES('" & Session("AccountID").ToString() & "','" & Session("UserID").ToString() & "','" & varStrIssueID & "','" & varStrTicketDetails & "','" & varStrFileAttach & "','" & varStrPriority & "','Open','" & Now() & "')"
        Else
            varStrInsert = "INSERT INTO tblCustomerTickets(AccID,UserID,IssueID,TicketDetails,Priority,Status,DatePosted)VALUES('" & Session("AccountID").ToString() & "','" & Session("UserID").ToString() & "','" & varStrIssueID & "','" & varStrTicketDetails & "','" & varStrPriority & "','Open','" & Now() & "')"
        End If


        'Add Issueype
        Dim InsertCmd As New Data.SqlClient.SqlCommand
        InsertCmd.Connection = objMainModule.oConn
        InsertCmd.CommandType = Data.CommandType.Text
        InsertCmd.CommandText = varStrInsert
        InsertCmd.ExecuteNonQuery()
        InsertCmd = Nothing
        'End Add Issuetype
        ClientScript.RegisterStartupScript(Me.GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Ticket Raised successfully');</script>")
    End Sub
    Protected Sub Page_Unload(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Unload
        objMainModule.oConn.Close()
        objMainModule.oConn = Nothing
    End Sub
End Class
