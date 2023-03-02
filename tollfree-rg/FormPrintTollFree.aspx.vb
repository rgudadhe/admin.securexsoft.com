Imports System.Data
Imports System.Data.SqlClient
Imports System.Configuration
Imports System.Web.UI.Page
Imports System

Partial Class FormPrintTollFree
     Inherits BasePage

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
		vact.Items.Add("All")
		vact.Items.FindByText("All").Value = "All"
		'Response.write(session("LoginID"))
		Session.Add("val", "0")
        Dim strcon As String = ConfigurationManager.ConnectionStrings("conn").ConnectionString
        Dim con As New SqlConnection(strcon)
        con.Open()
        Dim hcmd As New SqlCommand("Select distinct accname, custid from tbltollfree where ctype='hash' order by accname", con)
        Dim hds As New DataSet()
        Dim hda As New SqlDataAdapter(hcmd)
        hda.Fill(hds)
        hashact.DataSource = hds
        hashact.DataTextField = "accname"
        hashact.DataValueField = "custid"
        hashact.DataBind()
        con.Close()
        hcmd.Cancel()

        con.Open()
        Dim nhcmd As New SqlCommand("Select distinct accname, custid from tbltollfree where ctype='nohash' order by accname", con)
        Dim nhds As New DataSet()
        Dim nhda As New SqlDataAdapter(nhcmd)
        nhda.Fill(nhds)
        nhashact.DataSource = nhds
        nhashact.DataTextField = "accname"
        nhashact.DataValueField = "custid"
        nhashact.DataBind()
        con.Close()
        nhcmd.Cancel()

        con.Open()
        Dim vcmd As New SqlCommand("Select distinct accname, custid from tbltollfree order by accname", con)
        Dim vds As New DataSet()
        Dim vda As New SqlDataAdapter(vcmd)
        vda.Fill(vds)
        vact.DataSource = vds
        vact.DataTextField = "accname"
        vact.DataValueField = "custid"
        vact.DataBind()
        con.Close()
        vcmd.Cancel()
    End Sub
    Protected Sub Button1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button1.Click
        Dim v As String
        v = 1
        Response.Redirect("FormPrintHash.aspx?val=" + v + "&acc=" + HttpUtility.UrlEncode(hashact.SelectedValue.ToString))
    End Sub

    Protected Sub Button2_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button2.Click
        Dim vl As String
        vl = 2
        Response.Redirect("FormPrintHash.aspx?val=" + vl + "&acc=" + HttpUtility.UrlEncode(nhashact.SelectedValue.ToString))

    End Sub

    Protected Sub Button3_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button3.Click
        Response.Redirect("FormViewDetails.aspx?Name=" + HttpUtility.UrlEncode(vact.SelectedValue.ToString))
    End Sub
	
	 Protected Sub Button4_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button4.Click
        Response.Redirect("Formidsearch.aspx?pid=" + txtpid.Text)
    End Sub
End Class
