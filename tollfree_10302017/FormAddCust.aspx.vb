Imports System
Imports System.Data
Imports System.Data.SqlClient
Imports System.Configuration
Imports System.IO
Partial Class FormAddCust
    Inherits BasePage
	
	Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try

            If Not IsPostBack Then
                Dim strcon As String = ConfigurationManager.ConnectionStrings("conn").ConnectionString
                Dim con As New SqlConnection(strcon)
                Dim aname As String = Session("username")
                'Response.Write("userid:" & aname)
                con.Open()
                'ddacct.Items.Add("Select here")
                Dim hcmd As New SqlCommand("select tm.ACCOUNTNAME from tblaccountsmain tm where tm.accountid NOT IN(SELECT CUSTID FROM tblcustprofile) AND (TM.ISDELETED IS NULL OR TM.ISDELETED=0) order by accountname", con)
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

        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub
	
	Protected Sub Button1_Click(ByVal sender As Object, ByVal e As EventArgs) Handles Button1.Click
		
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
		
        Dim cmd As New SqlCommand("Insert into tblcustprofile (custid,custname,billingid,accid,phone,emailid,locationwise,emr,vcapture,vtransfer,pkeypad,demographics,reports,emrint,faxplus,comment,tdemo,tvoice,treports,tsamples,trefphy,billpro,splpro,ppapro,atth,contenttype,fname) values(@custid,@custname,@billingid,@accid,@phone,@emailid,@locationwise,@emr,@vcapture,@vtransfer,@pkeypad,@demographics,@reports,@emrint,@faxplus,@comment,@tdemo,@tvoice,@treports,@tsamples,@trefphy,@billpro,@splpro,@ppapro,@atth,@contenttype,@fname)", con)
		
			cmd.Parameters.AddWithValue("@custid", txtcustid.Text)        
			cmd.Parameters.AddWithValue("@custname", ddaccname.Text)
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
		
    End Sub
	
	Protected Sub accname_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles ddaccname.SelectedIndexChanged
		Dim strcon As String = ConfigurationManager.ConnectionStrings("conn").ConnectionString
        Dim con As New SqlConnection(strcon)
        con.Open()
        Dim hcmd As New SqlCommand("Select distinct * from tblaccountsmain where accountname='" & ddaccname.Text & "'", con)
        Dim DR As SqlDataReader = hcmd.ExecuteReader()
        While DR.Read()
            txtCustid.Text = DR.GetValue(0).ToString
            txtbillid.Text = DR.GetValue(4).ToString
            txtaccid.Text = DR.GetValue(2).ToString
            txtphone.Text = DR.GetValue(14).ToString
            txtmailid.Text = DR.GetValue(10).ToString
        End While
        con.Close()
    End Sub
	
	
	End Class