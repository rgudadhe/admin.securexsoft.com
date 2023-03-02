Imports System
Imports System.Data
Imports System.Data.SqlClient
Imports System.Configuration

Partial Class FormNewAccount
    Inherits BasePage

    Protected Sub Button1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button1.Click
        Dim strcon As String = ConfigurationManager.ConnectionStrings("conn").ConnectionString
        Dim con As New SqlConnection(strcon)
        con.Open()
        Dim cmd As New SqlCommand("Insert into tblaccounts (accname, custid, ctype) values(@accname,@custid,@ctype)", con)
        cmd.Parameters.AddWithValue("@accname", ddaccname.Text)
		cmd.Parameters.AddWithValue("@custid", txtaccid.Text)
		cmd.Parameters.AddWithValue("@ctype", txtacctype.Text)
        cmd.ExecuteNonQuery()
		Response.write("Account Successfully Updated")
        con.Close()
    End Sub

	Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
		 If Not IsPostBack Then
            Dim strcon As String = ConfigurationManager.ConnectionStrings("conn").ConnectionString
            Dim con As New SqlConnection(strcon)
            Dim aname As String = Session("username")
            'Response.Write("userid:" & aname)
            con.Open()
            'ddacct.Items.Add("Select here")
           Dim hcmd As New SqlCommand("select tm.ACCOUNTNAME from tblaccountsmain tm where tm.accountid NOT IN(SELECT CUSTID FROM tbltollfree) AND (TM.ISDELETED IS NULL OR TM.ISDELETED=0) order by accountname",con)
            Dim hds As New DataSet()
            Dim hda As New SqlDataAdapter(hcmd)
            hda.Fill(hds)
            ddaccname.DataSource = hds
            ddaccname.DataTextField = "accountname"
            ddaccname.DataValueField = "accountname"
            ddaccname.DataBind()
            con.Close()
            hcmd.Cancel()
        End If
	End Sub
	
	Protected Sub accname_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles ddaccname.SelectedIndexChanged
		Dim strcon As String = ConfigurationManager.ConnectionStrings("conn").ConnectionString
        Dim con As New SqlConnection(strcon)
        con.Open()
        Dim hcmd As New SqlCommand("Select distinct * from tblaccountsmain where accountname='" & ddaccname.Text & "'", con)
        Dim DR As SqlDataReader = hcmd.ExecuteReader()
        While DR.Read()
            txtaccid.Text = DR.GetValue(0).ToString
        End While
        con.Close()
    End Sub
	
	
End Class
