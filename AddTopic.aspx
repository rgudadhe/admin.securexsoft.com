<%@ Page Language="VB" AutoEventWireup="false" CodeFile="AddTopic.aspx.vb" Inherits="testets_AddTopic" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 1.0 Transitional/EN">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
<link href="images/style.css" rel="stylesheet" type="text/css" />
    <title>Untitled Page</title>
</head>
<body>

<!-- wrap starts here -->
<div id="wrap">

				
			
	<!-- content-wrap starts -->
	<div id="content-wrap" class="three-col"  >	
	
		
		<div id="sidebar">
			
			
			<ul class="sidemenu">				
				<li><a href="Forums.aspx">Discussion Forum</a></li>
				<li><a href="AddTopic.aspx">Add New Topic</a></li>
		</ul>	
				
		<!-- sidebar ends -->		
		</div>

		
		
		
		<div id="main">
		<form runat="server">
		    <h1><span style='color: #ff9933'><img src="images/icon1.jpg" width="48" height="43" />&nbsp;Forum</span></h1>
            <br />
            <table>
            <tr>
            <td>
            Forum :
            </td>
            <td>
                <asp:DropDownList ID="DLForum" runat="server" Width="465px" Font-Italic="True" Font-Names="Trebuchet MS">
               <asp:ListItem Text="Select forum"></asp:ListItem>  
                </asp:DropDownList>
            &nbsp;<br />
            </td>
            </tr>
            <tr>
            <td>
            Topic :
            </td>
            <td>
            <asp:TextBox ID="TxtTopic" runat="server" Width="465px"></asp:TextBox>&nbsp;<br />
            </td>
            </tr>
            <tr>
            <td>
            Description
            </td>
            <td>
            <asp:TextBox ID="TxtDescr" runat="server" Width="465px" TextMode="MultiLine" Height="120px" ></asp:TextBox>&nbsp;<br /></td>
            </tr>
            <tr>
            <td colspan="2" style="text-align:center; height: 30px;" >
                <asp:Button ID="Submit" runat="server" Text="Submit" CssClass="button"  />
            
            </td>
            </tr>
            </table>
            </form>  
		</div>
	<!-- content-wrap ends-->	
	</div>
		
	<!-- footer starts -->			
	<div id="footer-wrap"></div>
	<!-- footer ends-->	
	
<!-- wrap ends here -->
</div>

</body>
</html>
