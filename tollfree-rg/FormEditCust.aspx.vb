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
            Dim hcmd As New SqlCommand("select distinct custname, custid from tblcustprofile order by custname asc", con)
            Dim hds As New DataSet()
            Dim hda As New SqlDataAdapter(hcmd)
            hda.Fill(hds)
            ddaccname.DataSource = hds
            ddaccname.DataTextField = "custname"
            ddaccname.DataValueField = "custid"
            ddaccname.DataBind()
            con.Close()
            hcmd.Cancel()
        End If
    End Sub
	
	 Protected Sub accname_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles ddaccname.SelectedIndexChanged
		Dim strcon As String = ConfigurationManager.ConnectionStrings("conn").ConnectionString
        Dim con As New SqlConnection(strcon)
        con.Open()
        Dim hcmd As New SqlCommand("Select distinct * from tblcustprofile where custid='" & ddaccname.SelectedValue & "'", con)
        Dim DR As SqlDataReader = hcmd.ExecuteReader()
        If DR.HasRows Then
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
                txtdoc1.Text = DR.GetValue(30).ToString
                txtdoc2.Text = DR.GetValue(31).ToString
                txtdoc3.Text = DR.GetValue(32).ToString

            End While
        End If
        con.Close()

        con.Open()
        Dim gcmd As New SqlCommand("Select distinct Id, fname from tblcustprofile where custid='" & ddaccname.SelectedValue & "'  and fname <> ''", con)
        GridView1.DataSource = gcmd.ExecuteReader()
        GridView1.DataBind()

        con.Close()
        con.Open()
        Dim gcmd2 As New SqlCommand("Select distinct Id, fname2 from tblcustprofile where custid='" & ddaccname.SelectedValue & "'  and fname2 <> ''", con)
        GridView2.DataSource = gcmd2.ExecuteReader()
        GridView2.DataBind()

        con.Close()
    End Sub
	
	Protected Sub Button1_Click(ByVal sender As Object, ByVal e As EventArgs) Handles Button1.Click
	     Try
            Dim strcon As String = ConfigurationManager.ConnectionStrings("conn").ConnectionString
            Dim con As New SqlConnection(strcon)
            con.Open()
            ' Response.Write(GridView1.Rows.Count)
            'Response.End()
			Dim contenttype As String = String.Empty
            contenttype = "application/vnd.ms-word"
          
                Dim filePath As String = FileUpload1.PostedFile.FileName
                Dim filename As String = Path.GetFileName(filePath)
                Dim fs As Stream = FileUpload1.PostedFile.InputStream
                Dim br As New BinaryReader(fs)
            Dim bytes As Byte() = br.ReadBytes(fs.Length)

            Dim filePath2 As String = FileUpload2.PostedFile.FileName
            Dim filename2 As String = Path.GetFileName(filePath2)
            Dim fs2 As Stream = FileUpload2.PostedFile.InputStream
            Dim br2 As New BinaryReader(fs2)
            Dim bytes2 As Byte() = br2.ReadBytes(fs2.Length)


            Dim cmd As New SqlCommand
            cmd.Connection = con
            cmd.CommandText = ("Update tblcustprofile Set billingid=@billingid, accid=@accid, phone=@phone, emailid=@emailid, locationwise=@locationwise, EMR=@EMR, vcapture=@vcapture, vtransfer=@vtransfer, pkeypad=@pkeypad, demographics=@demographics, reports=@reports, emrint=@emrint, faxplus=@faxplus, comment=@comment, tdemo=@tdemo, tvoice=@tvoice, treports=@treports, tsamples=@tsamples, trefphy=@trefphy, billpro=@billpro, splpro=@splpro, ppapro=@ppapro,atth=@atth,atth2=@atth2 ,contenttype=@contenttype,fname=@fname, fname2=@fname2, doc1=@doc1, doc2=@doc2, doc3=@doc3 where custid='" & txtCustid.Text & "'")

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
            cmd.Parameters.AddWithValue("@doc1", txtdoc1.Text)
            cmd.Parameters.AddWithValue("@doc2", txtdoc2.Text)
            cmd.Parameters.AddWithValue("@doc3", txtdoc3.Text)

            If GridView1.Rows.Count > 0 Then
               
                Using con1 As New SqlConnection(strcon)
                    Using cmd1 As New SqlCommand()
                        cmd1.CommandText = "select id, fName,fname2, atth,atth2, ContentType from tblcustprofile where custid=@custid"
                        cmd1.Parameters.AddWithValue("@custid", ddaccname.SelectedValue)
                        cmd1.Connection = con1
                        con1.Open()
                        Using sdr As SqlDataReader = cmd1.ExecuteReader()
                            sdr.Read()
                            If sdr("fname").ToString <> "" Then
                                bytes = DirectCast(sdr("atth"), Byte())
                                contenttype = sdr("ContentType").ToString()
                                filename = sdr("fName").ToString()
                            End If
                            If sdr("fname2").ToString <> "" Then
                                bytes2 = DirectCast(sdr("atth2"), Byte())
                                contenttype = sdr("ContentType").ToString()
                                filename2 = sdr("fName2").ToString()
                            End If
                        End Using
                        con1.Close()
                    End Using
                End Using
                cmd.Parameters.AddWithValue("@atth", bytes)
                cmd.Parameters.AddWithValue("@atth2", bytes2)

            Else
                cmd.Parameters.AddWithValue("@atth", bytes)
                cmd.Parameters.AddWithValue("@atth2", bytes2)
            End If

            cmd.Parameters.AddWithValue("@contenttype", contenttype)
            cmd.Parameters.AddWithValue("@fname", filename)
            cmd.Parameters.AddWithValue("@fname2", filename2)
           
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
    Protected Sub DeleteFile2(sender As Object, e As EventArgs)
        Dim strcon As String = ConfigurationManager.ConnectionStrings("conn").ConnectionString
        Dim con As New SqlConnection(strcon)
        con.Open()

        Dim cmd As New SqlCommand
        cmd.Connection = con
        cmd.CommandText = ("Update tblcustprofile Set atth2=NULL, fname2=NULL where custname='" & ddaccname.Text & "'")
        cmd.ExecuteNonQuery()
        Response.Write("Account Successfully Updated")
        con.Close()
        accname_SelectedIndexChanged(Nothing, Nothing)
    End Sub
	
End Class