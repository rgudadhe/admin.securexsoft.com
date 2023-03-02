<%@ Page Language="VB" AutoEventWireup="false" CodeFile="FormViewDetails.aspx.vb" Inherits="FormViewDetails" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Accountwise Physician Details</title>
	<link href= "../App_Themes/Css/Main.css" type="text/css" rel="stylesheet" />
    <link href= "../App_Themes/Css/Common.css" type="text/css" rel="stylesheet" />
    <link href= "../App_Themes/Css/Styles.css" type="text/css" rel="stylesheet" />
</head>
<body>
   
   <form id="form1" runat="server">
    <div id="body" style="height: 100%;" >
    <div id="cap"></div>
    <div id="main">
   <h1>Account-wise Dictator Details</h1>
 
                    <asp:Repeater id="viewdet" runat="server">
                    <HeaderTemplate>
                    <table width="85%">
                    <tr>
                    <td colspan="8" style="text-align: center; height: 15px;" class="HeaderDiv" ><strong style="color:black">Physician Details</strong></td>
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
 </div>
  </div>
 </form>

</body>
</html>
