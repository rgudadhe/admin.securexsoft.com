<%@ Page Language="VB" %>

<script runat="server">

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim ConString, SQLString As String
        Dim txtUserName As String = String.Empty
        Dim txtPassword As String = String.Empty
        Dim tblName As String
        ConString = System.Configuration.ConfigurationManager.AppSettings("ETSCon")
        Dim oConn As New Data.SqlClient.SqlConnection
        Try
            txtUserName = Request.Form("UserName")
            txtPassword = Request.Form("Password")
            oConn.ConnectionString = ConString
            oConn.Open()
            If chkUser(txtUserName, oConn) Then
                tblName = "LoginInfo"
                SQLString = "SELECT U.UserID, U.FirstName + ' ' + U.LastName AS Name, UL.ProductionLevel " & _
                            "FROM tblUsers AS U INNER JOIN " & _
                            "tblUsersLevels AS UL ON U.UserID = UL.UserID " & _
                            "WHERE (U.UserName = '" & txtUserName & "') AND (U.Password = '" & txtPassword & "') AND (U.IsDeleted IS NULL)"
            Else
                tblName = "UserFailed"
                SQLString = "select distinct 'Please check username' as errString from tblUsers"
            End If
            
            Dim Adapter As New Data.SqlClient.SqlDataAdapter
            Adapter.SelectCommand = New Data.SqlClient.SqlCommand(SQLString, oConn)
            Dim myDs As New Data.DataSet
            Adapter.Fill(myDs, tblName)
            Dim sw As New IO.StringWriter
            myDs.WriteXml(sw)
            Response.Write(sw.ToString)
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub
    Private Function chkUser(ByVal strUser As String, ByVal objCon As Data.SqlClient.SqlConnection) As Boolean
        Dim oCommand As New Data.SqlClient.SqlCommand("select count(*) as uCount from tblUsers where UserName = '" & strUser & "'", objCon)
        Dim oRec As Data.SqlClient.SqlDataReader = oCommand.ExecuteReader
        oRec.Read()
        If oRec.HasRows Then
            If CInt(oRec("uCount").ToString) > 0 Then
                chkUser = True
            Else
                chkUser = False
            End If
        Else
            chkUser = False
        End If
            oRec.Close()
    End Function
</script>

