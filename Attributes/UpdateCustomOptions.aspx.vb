Imports System.Data
Imports System.Data.SqlClient
Partial Class UpdateOptions
    Inherits System.Web.UI.Page
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load
        Try

    
            If Not IsPostBack Then
                lblCaption.Text = Request("Caption").ToString
                Dim strConn As String

                strConn = System.Configuration.ConfigurationManager.AppSettings("ETSCon")
                Dim cmdIns1 As New SqlCommand("SELECT * FROM [AdminETS].[dbo].[tblAccounts] Order By AccountName ", New SqlConnection(strConn))
                cmdIns1.CommandType = CommandType.Text
                cmdIns1.Connection.Open()
                Dim DT1 As New DataTable
                Dim DRRec1 As SqlDataReader = cmdIns1.ExecuteReader()
                DT1.Load(DRRec1)
                DLAccounts.DataSource = DT1
                DLAccounts.DataTextField = "AccountName"
                DLAccounts.DataValueField = "AccountID"
                DLAccounts.DataBind()

                BindData()
            End If
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Private Sub BindData()

        Dim strConn As String

        strConn = System.Configuration.ConfigurationManager.AppSettings("ETSCon")
        Dim cmdIns1 As New SqlCommand("SELECT * FROM [ADMINETS].[dbo].[tblCustomAttrValues] WHERE AttributeID='" & Request("AttributeID").ToString & "' and AccountID ='" & DLAccounts.SelectedValue & "'   ", New SqlConnection(strConn))
        cmdIns1.CommandType = CommandType.Text
        cmdIns1.Connection.Open()
        Dim DTAttOp As New DataTable
        Dim DRRec1 As SqlDataReader = cmdIns1.ExecuteReader()
        DTAttOp.Load(DRRec1)
        If DTAttOp.Rows.Count > 0 Then
            For Each oRec As DataRow In DTAttOp.Rows
                lstAssignOptions.Items.Add(oRec("Values"))
            Next
        End If
        DTAttOp.Dispose()
    End Sub

    Protected Sub btnAdd_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAdd.Click
        If txtNewOption.Text.Trim <> "" Then
            lstAssignOptions.Items.Add(txtNewOption.Text)
            txtNewOption.Text = ""
        End If
    End Sub

    Protected Sub btnRemove_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnRemove.Click
        Dim intCt As Integer
        For intCt = lstAssignOptions.Items.Count - 1 To 0 Step -1 ' Looping Backwards
            If lstAssignOptions.Items(intCt).Selected Then
                lstAssignOptions.Items.Remove(lstAssignOptions.Items(intCt))
                Exit For
            End If
        Next
    End Sub

    Protected Sub btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSave.Click
        Try

     
        Dim strConn As String

        strConn = System.Configuration.ConfigurationManager.AppSettings("ETSCon")
        Dim DT As New DataTable
        Dim cmdIns As New SqlCommand("DELETE FROM [ADMINETS].[dbo].[tblCustomAttrValues]  WHERE AttributeID='" & Request("AttributeID").ToString & "' and AccountID ='" & DLAccounts.SelectedValue & "' ", New SqlConnection(strConn))
        ' Response.Write(" SELECT Top 1 PhyID from secureweb.dbo.tblphyassignment where userid ='" & Session("userID").ToString & "' ")

        cmdIns.Connection.Open()
        cmdIns.ExecuteNonQuery()

        If cmdIns.Connection.State = ConnectionState.Open Then
            cmdIns.Connection.Close()
        End If

        For Each item As ListItem In lstAssignOptions.Items
                Dim cmdIns1 As New SqlCommand("INSERT INTO [ADMINETS].[dbo].[tblCustomAttrValues]  (AttributeID, AccountID, [Values])  Values ('" & Request("AttributeID").ToString & "','" & DLAccounts.SelectedValue & "', '" & item.Text & "') ", New SqlConnection(strConn))
            ' Response.Write(" SELECT Top 1 PhyID from secureweb.dbo.tblphyassignment where userid ='" & Session("userID").ToString & "' ")

            cmdIns1.Connection.Open()
            cmdIns1.ExecuteNonQuery()

            If cmdIns1.Connection.State = ConnectionState.Open Then
                cmdIns1.Connection.Close()
            End If
        Next

            lblMessage.Text = "Options updated successfully!"
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Protected Sub btnMoveUp_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnMoveUp.Click
        Dim lstItem As ListItem = lstAssignOptions.SelectedItem
        If Not lstItem Is Nothing Then
            Dim index As Integer = lstAssignOptions.Items.IndexOf(lstItem)
            If index <> 0 Then
                lstAssignOptions.Items.Remove(lstItem)
                lstAssignOptions.Items.Insert(index - 1, lstItem)
                lstAssignOptions.Items(index - 1).Selected = True
            End If
        End If
        
    End Sub

    Protected Sub btnMoveDown_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnMoveDown.Click
        Dim lstItem As ListItem = lstAssignOptions.SelectedItem
        If Not lstItem Is Nothing Then
            Dim index As Integer = lstAssignOptions.Items.IndexOf(lstItem)
            If index <> (lstAssignOptions.Items.Count - 1) Then
                lstAssignOptions.Items.Remove(lstItem)
                lstAssignOptions.Items.Insert(index + 1, lstItem)
                lstAssignOptions.Items(index + 1).Selected = True
            End If
        End If
    End Sub

    Protected Sub DLAccounts_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles DLAccounts.SelectedIndexChanged
        BindData()
    End Sub
End Class
