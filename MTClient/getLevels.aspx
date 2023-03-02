<%@ Page Language="VB" %>

<script runat="server">

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim ConString, SQLString As String
        Dim txtUserName As String = String.Empty
        Dim txtPassword As String = String.Empty
        ConString = System.Configuration.ConfigurationManager.AppSettings("ETSCon")
        Dim oConn As New Data.SqlClient.SqlConnection
        Try
            txtUserName = Request.Form("UserName")
            txtPassword = Request.Form("Password")
            oConn.ConnectionString = ConString
            oConn.Open()
            SQLString = "SELECT LevelNo, LevelName, CheckInOptions FROM tblProductionLevels order by LevelNo"
            Dim Adapter As New Data.SqlClient.SqlDataAdapter
            Adapter.SelectCommand = New Data.SqlClient.SqlCommand(SQLString, oConn)
            Dim myDs As New Data.DataSet
            Adapter.Fill(myDs, "LevelsInfo")
            Dim sw As New IO.StringWriter
            myDs.WriteXml(sw)
            Response.Write(sw.ToString)
        Catch ex As Exception

        End Try
    End Sub
</script>

