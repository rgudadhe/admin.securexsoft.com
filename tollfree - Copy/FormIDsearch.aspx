<%@ Page Language="VB" AutoEventWireup="false" CodeFile="FormIDsearch.aspx.vb" Inherits="FormIDsearch" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Untitled Page</title>
</head>
<body>
  <table style="vertical-align:middle">
<tr>
<td align="Left">
    <form id="form1" runat="server">
    <div style="vertical-align:middle">
    <h2 style="text-align: center; font-family:Arial">Account-wise Dictator Details</h2>
    <h3 style="text-align: center; font-family:Arial"></h3><table>
<tr>
        <td>
                    <asp:Repeater id="viewiddet" runat="server">
                    <HeaderTemplate>
                    <table border="1" width="100%" style="font-family:Arial; font-size:smaller">
                    <tr>
                    <td colspan="8" align="center" style="background-color:Navy;"><strong style="color:White">Physician Details</strong></td>
                    </tr>
                    <tr>
                    <th>Account Name</th>
                    <th>Last Name</th>
                    <th>First Name</th>
                    <th>Keypad</th>
                    <th>ID</th>
                    <th>Password</th>
					<th>Partition</th>
					<th>Partition No</th>
                    </tr>
                    </HeaderTemplate>
                    <ItemTemplate>
                    <tr>
                    <td><%#Container.DataItem("accname")%></td>
                    <td><%#Container.DataItem("diclname")%></td>
                    <td><%#Container.DataItem("dicfname")%></td>
                    <td><%#Container.DataItem("keypad")%></td>
                    <td><%#Container.DataItem("id")%></td>
                    <td><%#Container.DataItem("password")%></td>
					<td><%#Container.DataItem("partition")%></td>
					<td><%#Container.DataItem("partitionno")%></td>
                    </tr>
                    </ItemTemplate>

                    <FooterTemplate>
                    </table>
                    </FooterTemplate>
                    </asp:Repeater>
        </td>
     </tr>
     <tr>
         <asp:Label ID="Label1" runat="server" Text="" Font-Names="Arial" Font-Size="Small" Font-Bold="true" ForeColor="Red"></asp:Label>
     </tr>
 </table>
 </div>
 </form>
 </td>
</tr>
</table>
</body>
</html>
