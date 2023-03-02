<%@ Page Language="VB" AutoEventWireup="false" CodeFile="UpdateCustomOptions.aspx.vb" Inherits="UpdateOptions" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>Manage Attributes</title>
   <link href= "../App_Themes/Css/styles.css" type="text/css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
   <div id="body">
    <div id="cap"></div>
    <div id="main">
   
    <h1>Manage Attribute Options </h1> 
     <span style="text-align: left !imoportant;" > 
    Caption: <asp:Label ID="lblCaption" runat="server" Text=""></asp:Label><br /><br />
    </span>
        <asp:DropDownList ID="DLAccounts" AutoPostBack ="true"  runat="server">
        </asp:DropDownList>          
        <asp:Panel ID="Panel1" runat="server" HorizontalAlign="Left">
           <table width="449">
       
          <tr>			  
    		<td>				 
				<asp:ListBox ID="lstAssignOptions" runat="server" EnableViewState="True"  AutoPostBack="false"  SelectionMode="Single" Height="400px" Width="400px" ></asp:ListBox>			 	
			</td>
			<td>
                <asp:LinkButton ID="btnMoveUp" runat="server">Move Up</asp:LinkButton>
                <br />
                <br />
                <asp:LinkButton ID="btnMoveDown" runat="server">Move Down</asp:LinkButton>
                <br />
                <br />
                <br />
                <br />
			    <asp:Button CssClass="button"  ID="btnRemove" runat="server" Text="Remove" />				 
		       <%--<asp:ImageButton ID="btnRemove" runat="server" ImageUrl="~/App_Themes/Images/left.jpg" />--%>
          </td>
		  </tr>	
		  <tr>
		     <td >                                            
                 <asp:TextBox ID="txtNewOption" runat="server" Width="100%"></asp:TextBox>                
			</td>
			<td>
			<asp:Button CssClass="button"  ID="btnAdd" runat="server" Text="Add New" />
			  <%--<asp:ImageButton ID="btnAdd" runat="server" ImageUrl="~/App_Themes/Images/right.jpg" />--%>
			</td>
		  </tr>	            
			
        </table> 
        <asp:Button CssClass="button"  ID="btnSave" runat="server" Text="Save Changes" />            
        &nbsp;<br />
            <asp:Label ID="lblMessage" runat="server" Font-Bold="True" ForeColor="Red"></asp:Label>
        <br />
        
        </asp:Panel>
  </div>
  </div>
  <asp:HiddenField ID="hdnSelAsiignLevel" runat="server" />
    <asp:ValidationSummary ID="ValidationSummary1" runat="server" HeaderText="Following error occurs:" ShowMessageBox="true" DisplayMode="BulletList" ShowSummary="False" /> </form>
    
</body>
</html>
