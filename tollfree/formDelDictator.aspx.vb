Imports System.Data
Imports System.Configuration
Imports System.Data.SqlClient

Partial Class tollfree_formDelDictator
    Inherits System.Web.UI.Page

   
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Not Me.IsPostBack Then
            Try
                Me.LoadAccounts()
            Catch ex As Exception
                Response.Write(ex.Message)
            End Try

        End If
    End Sub
    Private Sub BindGrid(ByVal custid As String)
        Dim constr1 As String = ConfigurationManager.ConnectionStrings("tollfreeConnectionString").ConnectionString
        Dim query1 As String = "SELECT dicfname,diclname,keypad,id,partition FROM tbltollfree where custid='" & custid & "'"
        Using con1 As SqlConnection = New SqlConnection(constr1)
            Using sda1 As SqlDataAdapter = New SqlDataAdapter(query1, con1)
                Using dt As DataTable = New DataTable()
                    sda1.Fill(dt)
                    GridView1.DataSource = dt
                    GridView1.DataBind()
                End Using
            End Using
            con1.Close()
        End Using

    End Sub
    Private Sub LoadAccounts()
        Dim constr As String = ConfigurationManager.ConnectionStrings("tollfreeConnectionString").ConnectionString
        Dim query As String = "SELECT distinct accname, custid FROM tbltollfree order by accname asc"
        Using con As SqlConnection = New SqlConnection(constr)
            Using sda As SqlDataAdapter = New SqlDataAdapter(query, con)
                Using dt As DataTable = New DataTable()
                    sda.Fill(dt)
                    ddlAccounts.DataSource = dt
                    ddlAccounts.DataTextField = "Accname"
                    ddlAccounts.DataValueField = "custid"
                    ddlAccounts.DataBind()
                End Using
            End Using
            con.Close()
        End Using

    End Sub
    Protected Sub OnRowEditing(ByVal sender As Object, ByVal e As GridViewEditEventArgs)
        GridView1.EditIndex = e.NewEditIndex
        Me.BindGrid(ddlAccounts.SelectedValue)
    End Sub
    Protected Sub OnRowUpdating(ByVal sender As Object, ByVal e As GridViewUpdateEventArgs)
        Dim row As GridViewRow = GridView1.Rows(e.RowIndex)
        Dim id As Integer = (TryCast(row.FindControl("lblid"), Label)).Text
        Dim dicfname As String = (TryCast(row.FindControl("txtFName"), TextBox)).Text
        Dim diclname As String = (TryCast(row.FindControl("txtLName"), TextBox)).Text
        Dim keypad As String = (TryCast(row.FindControl("txtKeypad"), TextBox)).Text
        Dim Partition As String = (TryCast(row.FindControl("txtPartition"), TextBox)).Text
        Dim query As String = "UPDATE tbltollfree SET dicfname=@dicfname, diclname=@diclname, keypad=@keypad, partition=@partition WHERE id=" & id
        Dim constr As String = ConfigurationManager.ConnectionStrings("tollfreeConnectionString").ConnectionString
        Using con As SqlConnection = New SqlConnection(constr)
            Using cmd As SqlCommand = New SqlCommand(query)
                cmd.Parameters.AddWithValue("@dicfname", dicfname)
                cmd.Parameters.AddWithValue("@diclname", diclname)
                cmd.Parameters.AddWithValue("@keypad", keypad)
                cmd.Parameters.AddWithValue("@Partition", Partition)
                cmd.Connection = con
                con.Open()
                cmd.ExecuteNonQuery()
                con.Close()
            End Using
        End Using

        GridView1.EditIndex = -1
        Me.BindGrid(ddlAccounts.SelectedValue)
    End Sub
    Protected Sub OnRowCancelingEdit(ByVal sender As Object, ByVal e As EventArgs)
        GridView1.EditIndex = -1
        Me.BindGrid(ddlAccounts.SelectedValue)
    End Sub
    Protected Sub OnRowDeleting(ByVal sender As Object, ByVal e As GridViewDeleteEventArgs)
        Dim id As Integer = Convert.ToInt32(GridView1.DataKeys(e.RowIndex).Values(0))
        Dim query As String = "DELETE FROM tbltollfree WHERE id=@id"
        Dim constr As String = ConfigurationManager.ConnectionStrings("tollfreeConnectionString").ConnectionString
        Using con As SqlConnection = New SqlConnection(constr)
            Using cmd As SqlCommand = New SqlCommand(query)
                cmd.Parameters.AddWithValue("@id", id)
                cmd.Connection = con
                con.Open()
                cmd.ExecuteNonQuery()
                con.Close()
            End Using
        End Using

        Me.BindGrid(ddlAccounts.SelectedValue)
    End Sub
    Protected Sub OnRowDataBound(ByVal sender As Object, ByVal e As GridViewRowEventArgs)
        If e.Row.RowType = DataControlRowType.DataRow AndAlso e.Row.RowIndex <> GridView1.EditIndex Then
            TryCast(e.Row.Cells(0).Controls(2), LinkButton).Attributes("onclick") = "return confirm('Do you want to delete this row?');"

        End If
    End Sub
    Protected Sub OnPaging(ByVal sender As Object, ByVal e As GridViewPageEventArgs)
        GridView1.PageIndex = e.NewPageIndex
        Me.BindGrid(ddlAccounts.SelectedValue)
    End Sub

    Protected Sub GridView1_RowCancelingEdit(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCancelEditEventArgs) Handles GridView1.RowCancelingEdit
        GridView1.EditIndex = -1
        Me.BindGrid(ddlAccounts.SelectedValue)
    End Sub

    Protected Sub ddlAccounts_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlAccounts.SelectedIndexChanged
        If ddlAccounts.SelectedValue <> "0" Then
            Me.BindGrid(ddlAccounts.SelectedValue)
        End If
    End Sub
End Class
