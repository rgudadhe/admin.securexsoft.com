Imports System.Data
Imports System.Data.SqlClient
Imports System.Configuration

Partial Class FormNewAccount
    Inherits System.Web.UI.Page

    Protected Sub Button1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button1.Click
        Dim strcon As String = ConfigurationManager.ConnectionStrings("conn").ConnectionString
        Dim con As New SqlConnection(strcon)
        con.Open()
        Dim cmd As New SqlCommand("Insert into tblaccounts (accname, custid, ctype) values(@accname,@custid,@ctype)", con)
        cmd.Parameters.AddWithValue("@accname", TextBox1.Text)
		cmd.Parameters.AddWithValue("@custid", TextBox2.Text)
		cmd.Parameters.AddWithValue("@ctype", TextBox3.Text)
        cmd.ExecuteNonQuery()
		Response.write("Account Successfully Updated")
        con.Close()
    End Sub
	 Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
       
    End Sub
End Class
