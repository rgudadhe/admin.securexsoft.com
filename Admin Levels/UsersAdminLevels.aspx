<%@ Page Language="VB" AutoEventWireup="false" CodeFile="UsersAdminLevels.aspx.vb" Inherits="Admin_Levels_UsersAdminLevels" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 1.0 Transitional/EN">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Untitled Page</title>
   <link href= "../App_Themes/Css/styles.css" type="text/css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
   <div id="body">
    <div id="cap"></div>
    <div id="main">
    <h1>Manage User Access Levels  </h1>            
        <asp:Panel ID="Panel1" runat="server" HorizontalAlign="Left">
           <table width="449">
       <tr>                        
        <td colspan="4" class="HeaderDiv">                            
        <asp:Label ID="lblUser" runat="server" Width="100%"></asp:Label>
        </td>
       </tr>
       <tr>
          <td class="alt1" style="height: 21px; width: 215px;">
              Available Levels</td>
          <td rowspan="2">				 
		        <asp:ImageButton ID="btnAdd" runat="server" ImageUrl="~/App_Themes/Images/right.jpg" />
				<b></b>
                <asp:ImageButton ID="btnRemove" runat="server" ImageUrl="~/App_Themes/Images/left.jpg" />
          </td>
          <td class="alt1">
              Assigned Levels
           </td>
          <td class="alt1">
              <asp:Label ID="lblLinkCaption" runat="server"></asp:Label>             
          </td>    
         </tr>  
		 <tr>			  
    		<td rowspan="4" style="height: 202px; width: 215px;" >                                            
                <asp:ListBox ID="lstAvailLevels" EnableViewState="True"  SelectionMode="Multiple" runat="server" Height="400px" Width="208px" ></asp:ListBox> 
			</td>
			<td rowspan="3">
				<b></b>	  
				<asp:ListBox ID="lstAssignLevels" runat="server" EnableViewState="True"  AutoPostBack="true"  SelectionMode="Single" Height="400px" Width="208px" ></asp:ListBox>			 	
			</td>
			<td rowspan="3" class="alt1" valign="top">
				<b></b>	  
				<table>
				<tr height="385Px">
				<th valign="top">
				<asp:CheckBoxList TextAlign="Right" ID="chkbLinks" runat="server" Width="208px" AutoPostBack="true" CssClass="common">                
                </asp:CheckBoxList>
				</th>
				</tr>
				<tr>
				<td>
                    <%--<asp:CheckBox ID="chkAll" runat="server" Text="Select All" AutoPostBack="true"/>--%>
                    <asp:LinkButton ID="lnkCheckAll" runat="server"  >Check All</asp:LinkButton>                    
                    &nbsp &nbsp
                    <asp:LinkButton ID="lnkUnCheckAll" runat="server" >UnCheck All</asp:LinkButton>
				</td>
				</tr>
				</table>
                
			</td>
		  </tr>		            
			
        </table> 
        <asp:Button CssClass="button"  ID="btnSave" runat="server" Text="Save Changes" />            
        <asp:Button  CssClass="button" ID="btnBack" runat="server" OnClick="btnBack_Click" Text="<< Back to List"
            Width="120px" />&nbsp;<br />
        <MsgBox:msgBox ID="MsgBox1" runat="server" />
        <br />
        <asp:HiddenField ID="hdnCriUser" runat="server" />
        <asp:HiddenField ID="hdnCriOption" runat="server" />
        <asp:HiddenField ID="hdnIsSuperAdmin" runat="server" />
        </asp:Panel>
  </div>
  </div>
  <asp:HiddenField ID="hdnSelAsiignLevel" runat="server" />
    <asp:ValidationSummary ID="ValidationSummary1" runat="server" HeaderText="Following error occurs:" ShowMessageBox="true" DisplayMode="BulletList" ShowSummary="False" /> </form>
    
</body>
</html>
