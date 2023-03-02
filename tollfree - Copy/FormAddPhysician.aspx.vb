Imports System
Imports System.Data
Imports System.Data.SqlClient
Imports System.Configuration
Partial Class FormAddPhysician
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
		 
            Dim strcon As String = ConfigurationManager.ConnectionStrings("conn").ConnectionString
            Dim con As New SqlConnection(strcon)
            con.Open()
            Dim hcmd As New SqlCommand("Select distinct accname from tblaccounts order by accname", con)
            Dim hds As New DataSet()
            Dim hda As New SqlDataAdapter(hcmd)
            hda.Fill(hds)
            acct.DataSource = hds
            acct.DataTextField = "accname"
            acct.DataValueField = "accname"
            acct.DataBind()
            con.Close()

            con.Open()
            Dim nhcmd As New SqlCommand("select distinct keypadname from tblkeypad order by keypadname", con)
            Dim nhds As New DataSet()
            Dim nhda As New SqlDataAdapter(nhcmd)
            nhda.Fill(nhds)
            kpad.DataSource = nhds
            kpad.DataTextField = "keypadname"
            kpad.DataValueField = "keypadname"
            kpad.DataBind()
            con.Close()
            nhcmd.Cancel()

            con.Open()
            Dim cmd As New SqlCommand("select distinct ctype from tblaccounts order by ctype", con)
            Dim ds As New DataSet()
            Dim da As New SqlDataAdapter(cmd)
            da.Fill(ds)
            cust.DataSource = ds
            cust.DataTextField = "ctype"
            cust.DataValueField = "ctype"
            cust.DataBind()
            con.Close()
            cmd.Cancel()

            con.Open()
            Dim pcmd As New SqlCommand("select partition from tblpartitions order by partition", con)
            Dim pds As New DataSet()
            Dim pda As New SqlDataAdapter(pcmd)
            pda.Fill(pds)
            listpartition.DataSource = pds
            listpartition.DataTextField = "partition"
            listpartition.DataValueField = "partition"
            listpartition.DataBind()
            con.Close()
            pcmd.Cancel()
    End Sub

    Protected Sub Submit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Submit.Click
        Dim strcon As String = ConfigurationManager.ConnectionStrings("conn").ConnectionString
        Dim con As New SqlConnection(strcon)
        Try
            con.Open()
            Dim cmd As New SqlCommand("Insert into tbltollfree values('" + acct.Text + "','" + TextBox1.Text + "','" + TextBox2.Text + "','ChartVox','" + kpad.Text + "','" + TextBox3.Text + "','" + TextBox4.Text + "','" + cust.Text + "','" + txtcustid.Text + "','" + listpartition.text + "','" + txtpno.text + "')", con)
            cmd.ExecuteNonQuery()
            con.Close()
            Response.Write("Record has been updated Successfully")

        Catch ex As Exception

            Response.Write(ex)
        End Try

    End Sub

    Protected Sub acct_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles acct.SelectedIndexChanged

        Dim strcon1 As String = ConfigurationManager.ConnectionStrings("conn").ConnectionString
        Dim con1 As New SqlConnection(strcon1)
        Dim acname As String
        Dim custid As String
        acname = acct.SelectedItem.Text
        'Response.Write(acname)
        con1.Open()
        Dim acmd As New SqlCommand("Select custid from tblaccounts where accname='" & acname & "'", con1)
        Dim ads As New DataSet()
        Dim ada As New SqlDataAdapter(acmd)
        ada.Fill(ads)
        custid = acmd.ExecuteScalar.ToString
        txtcustid.Text = custid
        txtcustid.ReadOnly = True
    End Sub
	
	Protected Sub listpartition_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles listpartition.SelectedIndexChanged
        Dim strcon2 As String = ConfigurationManager.ConnectionStrings("conn").ConnectionString
        Dim con2 As New SqlConnection(strcon2)
        Dim pname As String
        Dim pno As String
        pname = listpartition.SelectedItem.Text
        'Response.Write(acname)
        con2.Open()
        Dim acmd As New SqlCommand("Select partitionno from tblpartitions where partition='" & pname & "'", con2)
        Dim ads As New DataSet()
        Dim ada As New SqlDataAdapter(acmd)
        ada.Fill(ads)
        pno = acmd.ExecuteScalar
        txtpno.Text = pno
        txtpno.ReadOnly = True
    End Sub
End Class