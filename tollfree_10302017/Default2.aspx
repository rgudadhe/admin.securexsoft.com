<%@ Import Namespace="System.Data.OleDb" %>

<script runat="server">
sub Page_Load
dim dbconn,sql,dbcomm,dbread
        dbconn = New OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;data source=" & Server.MapPath("main.mdb"))
dbconn.Open()
        sql = "SELECT * FROM tblkeypad"
dbcomm=New OleDbCommand(sql,dbconn)
dbread=dbcomm.ExecuteReader()
customers.DataSource=dbread
customers.DataBind()
dbread.Close()
dbconn.Close()
end sub
</script>

<html xmlns="http://www.w3.org/1999/xhtml">
<head></head>
<body>

<form id="Form1" runat="server">
<asp:Repeater id="customers" runat="server">

<HeaderTemplate>
<table border="1" width="100%">
<tr>
<th>KeyIN</th>
<th>Activity</th>
<th>Keypad name</th>
</tr>
</HeaderTemplate>

<ItemTemplate>
<tr>
<td><%#Container.DataItem("keyin")%></td>
<td><%#Container.DataItem("activity")%></td>
<td><%#Container.DataItem("keypadname")%></td>

</tr>
</ItemTemplate>

<FooterTemplate>
</table>
</FooterTemplate>

</asp:Repeater>
</form>

</body>
</html>