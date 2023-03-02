<%@ Page Language="VB" AutoEventWireup="false" CodeFile="UsersProdnLevels.aspx.vb" Inherits="Admin_Levels_UsersAdminLevels" %>

<%@ Register Assembly="MREDKJNumericBox" Namespace="MREDKJ" TagPrefix="cc1" %>


<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">


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
    <h1>Manage User Roles </h1>
        <asp:Panel ID="Panel1" runat="server" HorizontalAlign="Left">
            <asp:Label ID="Label1" runat="server" Text="" Font-Names="Trebuchet MS" Font-Size="10pt" ForeColor="Firebrick" Font-Bold="true" ></asp:Label><br /><br />
            <asp:Repeater ID="rptLevels" runat="server">
            <HeaderTemplate>
                <table  style="vertical-align: middle; text-align: left;" border="0">                
                    <tr>
                        <td class="alt1">
                            &nbsp;
                        </td> 
                        <td class="alt1">
                        User Role
                        </td>                                               
                         <td class="alt1">                           
                            View Limit                            
                         </td>
                         <td class="alt1">
                         Check-Out Limit
                         </td>
                    </tr>
            </HeaderTemplate>
            <ItemTemplate>
                <tr>
                   <td><asp:CheckBox ID="ckSelected" runat="server" Checked='<%#chkLevel(SelectedUserLevel, Container.DataItem("LevelNo"))%>' /></td>
                    <td><%#Container.DataItem("LevelName")%> 
                        <asp:HiddenField ID="LevelNo" runat="server" Value='<%#Container.DataItem("LevelNo") %>'/>
                    </td>
                    <td align="center">                        
                        <cc1:NumericTextBox ID="txtViewLimit" runat="server" text='<%#getViewLimit(Container.DataItem("LevelNo"))%>' MaxLength="3 " AllowNegative="false" AllowDecimal="false" Width="30"></cc1:NumericTextBox>                        
                    </td>
                    <td align="center">                        
                        <cc1:NumericTextBox ID="txtChkOutLimit" runat="server" text='<%#getChkOutLimit(Container.DataItem("LevelNo"))%>' MaxLength="3 " AllowNegative="false" AllowDecimal="false" Width="30"></cc1:NumericTextBox>
                    </td>
                </tr>
            </ItemTemplate>
            <AlternatingItemTemplate>
                <tr bgcolor="#cccccc">
                    <td ><asp:CheckBox ID="ckSelected" runat="server" Checked='<%#chkLevel(SelectedUserLevel, Container.DataItem("LevelNo"))%>' /></td>
                    <td><%#Container.DataItem("LevelName")%> 
                        <asp:HiddenField ID="LevelNo" runat="server" Value='<%#Container.DataItem("LevelNo") %>'/>
                    </td>
                    <td align="center">                        
                        <cc1:NumericTextBox ID="txtViewLimit" runat="server" text='<%#getViewLimit(Container.DataItem("LevelNo"))%>' MaxLength="3 " AllowNegative="false" AllowDecimal="false" Width="30"></cc1:NumericTextBox>                        
                    </td>
                    <td align="center">                        
                        <cc1:NumericTextBox ID="txtChkOutLimit" runat="server" text='<%#getChkOutLimit(Container.DataItem("LevelNo"))%>' MaxLength="3 " AllowNegative="false" AllowDecimal="false" Width="30"></cc1:NumericTextBox>
                    </td>
                </tr>
            </AlternatingItemTemplate>
            <FooterTemplate>
                <tr>                    
                    <td colspan="4" style="text-align: center;">
<asp:CheckBox ID="chkSetSamples" runat="server"  Checked='<%#CanSetSamples%>'  Text="Can Set Samples"/>                     
<asp:Button ID="Button1"  CssClass="button" runat="server" Text="Save Changes"/>
                    </td>
                </tr>
                </table>
            </FooterTemplate>
        </asp:Repeater>
            <asp:Button ID="btnBack" CssClass="button"  runat="server" Text="<< Back to Search" OnClick="btnBack_Click"/>
        </asp:Panel>
        </div>
        </div> 
    </form>

</body>
</html>
