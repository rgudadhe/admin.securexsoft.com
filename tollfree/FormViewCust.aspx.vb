Imports System
Imports System.Data
Imports System.Data.SqlClient
Imports System.Configuration
Partial Class FormViewCust
    Inherits BasePage

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
	If Not IsPostBack Then
            Dim strcon As String = ConfigurationManager.ConnectionStrings("conn").ConnectionString
            Dim con As New SqlConnection(strcon)
			Dim aname As String =  Session("username")
			'Response.Write("userid:" & aname)
            con.Open()
            'ddacct.Items.Add("Select here")
            Dim hcmd As New SqlCommand("Select distinct custname from tblcustprofile order by custname", con)
            Dim hds As New DataSet()
            Dim hda As New SqlDataAdapter(hcmd)
            hda.Fill(hds)
            ddacct.DataSource = hds
            ddacct.DataTextField = "custname"
            ddacct.DataValueField = "custname"
            ddacct.DataBind()
            con.Close()
            hcmd.Cancel()
        End If
    End Sub

	 Protected Sub ddacct_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles ddacct.SelectedIndexChanged
	 
		
		Dim strcon As String = ConfigurationManager.ConnectionStrings("conn").ConnectionString
        Dim con As New SqlConnection(strcon)
        con.Open()
        If ddacct.Text = "Select Here" Then
            lblbillid.Text = ""
            lblaccid.Text = ""
            lblphone.Text = ""
            lblemail.Text = ""
            lbllctn.Text = ""
            lblemr.Text = ""
            lblvc.Text = ""
            lblvt.Text = ""
            lblkeypad.Text = ""
            lbldemo.Text = ""
            lblreport.Text = ""
            lblemrint.Text = ""
            lblfplus.Text = ""
            lblcommt.Text = ""
			lbltdemo.Text = ""
			lbltvoice.Text = ""
			lbltreports.Text = ""
			lbltsamples.Text = ""
			lbltrefp.Text = ""
			lblbillp.Text = ""
			lblsplp.Text = ""
            lblppap.Text = ""
            lbldoc1.Text = ""
            lbldoc2.Text = ""
            lbldoc3.Text = ""

            con.Close()
        Else
            Dim hcmd As New SqlCommand("Select distinct * from tblcustprofile where custname='" & ddacct.Text & "'", con)
            Dim DR As SqlDataReader = hcmd.ExecuteReader()
			While DR.Read()
                lblbillid.Text = DR.GetValue(2).ToString
                lblaccid.Text = DR.GetValue(3).ToString
                lblphone.Text = DR.GetValue(4).ToString
                lblemail.Text = DR.GetValue(5).ToString
                lbllctn.Text = DR.GetValue(6).ToString
                lblemr.Text = DR.GetValue(7).ToString
                lblvc.Text = DR.GetValue(8).ToString
                lblvt.Text = DR.GetValue(9).ToString
                lblkeypad.Text = DR.GetValue(10).ToString
                lbldemo.Text = DR.GetValue(11).ToString
                lblreport.Text = DR.GetValue(12).ToString
                lblemrint.Text = DR.GetValue(13).ToString
                lblfplus.Text = DR.GetValue(14).ToString
                lblcommt.Text = DR.GetValue(15).ToString
				lbltdemo.Text = DR.GetValue(16).ToString
				lbltvoice.Text = DR.GetValue(17).ToString
				lbltreports.Text = DR.GetValue(18).ToString
				lbltsamples.Text = DR.GetValue(19).ToString
				lbltrefp.Text = DR.GetValue(20).ToString
				lblbillp.Text = DR.GetValue(21).ToString
				lblsplp.Text = DR.GetValue(22).ToString
				lblppap.Text = DR.GetValue(23).ToString
                lbldoc1.Text = DR.GetValue(30).ToString
                lbldoc2.Text = DR.GetValue(31).ToString
                lbldoc3.Text = DR.GetValue(32).ToString

            End While

            con.Close()
			con.open()
			
            Dim gcmd As New SqlCommand("Select distinct Id, fname from tblcustprofile where custname='" & ddacct.Text & "'  and fname <> ''", con)
			GridView1.DataSource = gcmd.ExecuteReader()
			GridView1.DataBind()
            con.Close()
            con.Open()

            Dim gcmd2 As New SqlCommand("Select distinct Id, fname2 from tblcustprofile where custname='" & ddacct.Text & "'  and fname2 <> ''", con)
            GridView2.DataSource = gcmd2.ExecuteReader()
            GridView2.DataBind()
            con.Close()
        End If
    End Sub
	
	Protected Sub DownloadFile(sender As Object, e As EventArgs)
	Dim id As Integer = Integer.Parse(TryCast(sender, LinkButton).CommandArgument)
    Dim bytes As Byte()
    Dim fileName As String, contentType As String
    
	Dim strcon As String = ConfigurationManager.ConnectionStrings("conn").ConnectionString
    'Dim con As New SqlConnection(strcon)
    'con.Open()
    Using con As New SqlConnection(strcon)
        Using cmd As New SqlCommand()
            cmd.CommandText = "select id, fName, atth, ContentType from tblcustprofile where id=@Id"
			cmd.Parameters.AddWithValue("@Id", id)
            cmd.Connection = con
            con.Open()
            Using sdr As SqlDataReader = cmd.ExecuteReader()
                sdr.Read()
                bytes = DirectCast(sdr("atth"), Byte())
                contentType = sdr("ContentType").ToString()
                fileName = sdr("fName").ToString()
            End Using
            con.Close()
        End Using
    End Using
    Response.Clear()
    Response.Buffer = True
    Response.Charset = ""
    Response.Cache.SetCacheability(HttpCacheability.NoCache)
    Response.ContentType = contentType
    Response.AppendHeader("Content-Disposition", "attachment; filename=" + fileName)
    Response.BinaryWrite(bytes)
    Response.Flush()
    Response.End()
    End Sub
    Protected Sub DownloadFile2(sender As Object, e As EventArgs)
        Dim id As Integer = Integer.Parse(TryCast(sender, LinkButton).CommandArgument)
        Dim bytes As Byte()
        Dim fileName As String, contentType As String

        Dim strcon As String = ConfigurationManager.ConnectionStrings("conn").ConnectionString
        'Dim con As New SqlConnection(strcon)
        'con.Open()
        Using con As New SqlConnection(strcon)
            Using cmd As New SqlCommand()
                cmd.CommandText = "select id, fName2, atth2, ContentType from tblcustprofile where id=@Id"
                cmd.Parameters.AddWithValue("@Id", id)
                cmd.Connection = con
                con.Open()
                Using sdr As SqlDataReader = cmd.ExecuteReader()
                    sdr.Read()
                    bytes = DirectCast(sdr("atth2"), Byte())
                    contentType = sdr("ContentType").ToString()
                    fileName = sdr("fName2").ToString()
                End Using
                con.Close()
            End Using
        End Using
        Response.Clear()
        Response.Buffer = True
        Response.Charset = ""
        Response.Cache.SetCacheability(HttpCacheability.NoCache)
        Response.ContentType = contentType
        Response.AppendHeader("Content-Disposition", "attachment; filename=" + fileName)
        Response.BinaryWrite(bytes)
        Response.Flush()
        Response.End()
    End Sub
	
End Class