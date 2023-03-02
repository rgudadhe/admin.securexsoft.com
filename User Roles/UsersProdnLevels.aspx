<%@ Page Language="VB" AutoEventWireup="false" CodeFile="UsersProdnLevels.aspx.vb" Inherits="Admin_Levels_UsersAdminLevels" %>

<%@ Register Assembly="MREDKJNumericBox" Namespace="MREDKJ" TagPrefix="cc1" %>


<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">


<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Untitled Page</title>
    <LINK href= "../styles/Default.css" type="text/css" rel="stylesheet">
</head>
<body>
    <form id="form1" runat="server">
    <div style="text-align: center">
        
        <asp:Repeater ID="rptLevels" runat="server">
            <HeaderTemplate>
                <table border="1" style="vertical-align: middle; text-align: left;">                
                    <tr>
                    <td colspan=4 class="HeaderDiv">
                        <%#uName%>
                    </td>
                    </tr>
                    <tr bgcolor="#3399cc">
                        <th class="SMSelected">
                        </th> 
                        <th class="SMSelected">
                        Levels
                        </th>                                               
                         <th class="SMSelected">                           

                            View Limit                            
                         </th>
                         <th class="SMSelected">
                         Check-Out Limit
                         </th>
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
<asp:Button ID="Button1" runat="server" Text="Save Changes"/>
                    </td>
                </tr>
                </table>
            </FooterTemplate>
        </asp:Repeater>                
        <asp:Button ID="btnBack" runat="server" Text="<< Back to Search" OnClick="btnBack_Click"/>                        
        </div>
        
    </form>

</body>
</html>
