Imports System.Data
Imports System.Data.SqlClient
Imports System.Configuration
Partial Class FormViewDetails
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
       Dim strcon As String = ConfigurationManager.ConnectionStrings("conn").ConnectionString
        Dim con As New SqlConnection(strcon)
        Dim aname As String = Request.QueryString("Name")
        'Response.Write(aname)
        con.Open()
        If aname = "All" Then
            Dim cmd As New SqlCommand("select * from tbltollfree order by id", con)
			Dim ds As New DataSet()
			Dim da As New SqlDataAdapter(cmd)
			 da.Fill(ds)
			viewdet.DataSource = ds
			viewdet.DataBind()
        Else
            Dim cmd As New SqlCommand("select * from tbltollfree where custid='" & aname & "' order by id", con)
			Dim ds As New DataSet()
			Dim da As New SqlDataAdapter(cmd)
			 da.Fill(ds)
			viewdet.DataSource = ds
			viewdet.DataBind()
        End If
        
       
        con.Close()
       
    End Sub
End Class
