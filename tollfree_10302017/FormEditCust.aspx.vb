Imports System
Imports System.Data
Imports System.Data.SqlClient
Imports System.Configuration
Imports System.IO
Partial Public Class FormEditCust
    Inherits BasePage

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Not IsPostBack Then
            Dim strcon As String = ConfigurationManager.ConnectionStrings("conn").ConnectionString
            Dim con As New SqlConnection(strcon)
            Dim aname As String = Session("username")
            'Response.Write("userid:" & aname)
            con.Open()
            'ddacct.Items.Add("Select here")
            Dim hcmd As New SqlCommand("Select distinct custname from tblcustprofile order by custname", con)
            Dim hds As New DataSet()
            Dim hda As New SqlDataAdapter(hcmd)
            hda.Fill(hds)
            ddaccname.DataSource = hds
            ddaccname.DataTextField = "custname"
            ddaccname.DataValueField = "custname"
            ddaccname.DataBind()
            con.Close()
            hcmd.Cancel()
        End If
    End Sub
	
	 Protected Sub accname_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles ddaccname.SelectedIndexChanged
		Dim strcon As String = ConfigurationManager.ConnectionStrings("conn").ConnectionString
        Dim con As New SqlConnection(strcon)
        con.Open()
        Dim hcmd As New SqlCommand("Select distinct * from tblcustprofile where custname='" & ddaccname.Text & "'", con)
        Dim DR As SqlDataReader = hcmd.ExecuteReader()
        While DR.Read()
            txtCustid.Text = DR.GetValue(0).ToString
            txtbillid.Text = DR.GetValue(2).ToString
            txtaccid.Text = DR.GetValue(3).ToString
            txtphone.Text = DR.GetValue(4).ToString
            txtmailid.Text = DR.GetValue(5).ToString
            txtbillp.Text = DR.GetValue(21).ToString
            txtloc.Text = DR.GetValue(6).ToString
            txtemr.Text = DR.GetValue(7).ToString
            txtvc.Text = DR.GetValue(8).ToString
            txtvt.Text = DR.GetValue(9).ToString
            txtpk.Text = DR.GetValue(10).ToString
            txtdemo.Text = DR.GetValue(11).ToString
            txtrepo.Text = DR.GetValue(12).ToString
            txtemri.Text = DR.GetValue(13).ToString
            txtfplus.Text = DR.GetValue(14).ToString
            txtcomment.Text = DR.GetValue(15).ToString
            txtdemop.Text = DR.GetValue(16).ToString
            txtvp.Text = DR.GetValue(17).ToString
            txtrp.Text = DR.GetValue(18).ToString
            txtsp.Text = DR.GetValue(19).ToString
            txtrpp.Text = DR.GetValue(20).ToString
            txtspp.Text = DR.GetValue(22).ToString
            txtppap.Text = DR.GetValue(23).ToString
        End While
        con.Close()
		
			con.open()
			Dim gcmd As New SqlCommand("Select distinct Id, fname from tblcustprofile where custname='" & ddaccname.Text & "'  and fname <> ''" , con)
			GridView1.DataSource = gcmd.ExecuteReader()
			GridView1.DataBind()
			con.Close()
    End Sub
	
	Protected Sub Button1_Click(ByVal sender As Object, ByVal e As EventArgs) Handles Button1.Click
	     Try
            Dim strcon As String = ConfigurationManager.ConnectionStrings("conn").ConnectionString
            Dim con As New SqlConnection(strcon)
            con.Open()
			
			Dim contenttype As String = String.Empty
			contenttype = "application/vnd.ms-word"
			Dim filePath As String = FileUpload1.PostedFile.FileName
			Dim filename As String = Path.GetFileName(filePath)
			Dim fs As Stream = FileUpload1.PostedFile.InputStream
			Dim br As New BinaryReader(fs)
			Dim bytes As Byte() = br.ReadBytes(fs.Length)
		
            Dim cmd As New SqlCommand
			cmd.Connection = con
            cmd.CommandText = ("Update tblcustprofile Set billingid=@billingid, accid=@accid, phone=@phone, emailid=@emailid, locationwise=@locationwise, EMR=@EMR, vcapture=@vcapture, vtransfer=@vtransfer, pkeypad=@pkeypad, demographics=@demographics, reports=@reports, emrint=@emrint, faxplus=@faxplus, comment=@comment, tdemo=@tdemo, tvoice=@tvoice, treports=@treports, tsamples=@tsamples, trefphy=@trefphy, billpro=@billpro, splpro=@splpro, ppapro=@ppapro,atth=@atth,contenttype=@contenttype,fname=@fname where custid='" & txtCustid.Text & "'")
			
            cmd.Parameters.AddWithValue("@billingid", txtbillid.Text)
            cmd.Parameters.AddWithValue("@accid", txtaccid.Text)
            cmd.Parameters.AddWithValue("@phone", txtphone.Text)
            cmd.Parameters.AddWithValue("@emailid", txtmailid.Text)
            cmd.Parameters.AddWithValue("@locationwise", txtloc.Text)
            cmd.Parameters.AddWithValue("@EMR", txtemr.Text)
            cmd.Parameters.AddWithValue("@vcapture", txtvc.Text)
            cmd.Parameters.AddWithValue("@vtransfer", txtvt.Text)
            cmd.Parameters.AddWithValue("@pkeypad", txtpk.Text)
            cmd.Parameters.AddWithValue("@demographics", txtdemo.Text)
            cmd.Parameters.AddWithValue("@reports", txtrepo.Text)
            cmd.Parameters.AddWithValue("@emrint", txtemri.Text)
            cmd.Parameters.AddWithValue("@faxplus", txtfplus.Text)
            cmd.Parameters.AddWithValue("@comment", txtcomment.Text)
            cmd.Parameters.AddWithValue("@tdemo", txtdemop.Text)
            cmd.Parameters.AddWithValue("@tvoice", txtvp.Text)
            cmd.Parameters.AddWithValue("@treports", txtrp.Text)
            cmd.Parameters.AddWithValue("@tsamples", txtsp.Text)
            cmd.Parameters.AddWithValue("@trefphy", txtrpp.Text)
            cmd.Parameters.AddWithValue("@billpro", txtbillp.Text)
            cmd.Parameters.AddWithValue("@splpro", txtspp.Text)
            cmd.Parameters.AddWithValue("@ppapro", txtppap.Text)
			cmd.Parameters.AddWithValue("@atth", bytes)
			cmd.Parameters.AddWithValue("@contenttype", contenttype)
			cmd.Parameters.AddWithValue("@fname", filename)
            cmd.ExecuteNonQuery()
            Response.Write("Account Successfully Updated")
            con.Close()
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
		accname_SelectedIndexChanged(Nothing, Nothing)	
    End Sub
	
	Protected Sub DeleteFile(sender As Object, e As EventArgs)
	Dim strcon As String = ConfigurationManager.ConnectionStrings("conn").ConnectionString
    Dim con As New SqlConnection(strcon)
    con.Open()
	
	Dim cmd As New SqlCommand
	cmd.Connection = con
    cmd.CommandText = ("Update tblcustprofile Set atth=NULL,contenttype=NULL, fname=NULL where custname='" &ddaccname.Text & "'")
	           
			cmd.ExecuteNonQuery()
            Response.Write("Account Successfully Updated")
            con.Close()
		accname_SelectedIndexChanged(Nothing, Nothing)	
	End Sub
	
End Class