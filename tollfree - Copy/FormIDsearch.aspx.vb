Imports System.Data
Imports System.Data.SqlClient
Imports System.Configuration
Partial Class FormIDsearch
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim strcon As String = ConfigurationManager.ConnectionStrings("conn").ConnectionString
        Dim con As New SqlConnection(strcon)
        Dim pid As Integer

        pid = Request.QueryString("pid")
        con.Open()
        Dim cmd As New SqlCommand("select * from tbltollfree where id='" & pid & "'", con)
        Dim ds As New DataSet()
        Dim da As New SqlDataAdapter(cmd)
        da.Fill(ds)
        If ds.Tables(0).Rows.Count > 0 Then
            viewiddet.DataSource = ds
            viewiddet.DataBind()
            con.Close()
        Else
            Label1.Text = "No Record Found!"
        End If

    End Sub
End Class
