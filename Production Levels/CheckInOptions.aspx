<%@ Page Language="VB" AutoEventWireup="false" CodeFile="CheckInOptions.aspx.vb" Inherits="Admin_Levels_UsersAdminLevels" %>


<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 1.0 Transitional/EN">


<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Production Level</title>
    <link href= "../App_Themes/Css/styles.css" type="text/css" rel="stylesheet" />
    <link href= "../App_Themes/Css/Common.css" type="text/css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <div id="body">
            <div id="cap"></div>
            <div id="main">
            <h1>Production Levels</h1>
        <asp:Panel ID="Panel1" runat="server" HorizontalAlign="Left">
            
        <asp:Repeater ID="rptLevels" runat="server">
            <HeaderTemplate>
                <table>
                    <tr>                        
                         <td colspan="3" class="HeaderDiv">
                            Check-In Options For <%#LevelName%>                        
                         </td>
                    </tr>                
                    <tr>
                        <td class="alt1">Level Names
                        </td>                        
                        <td class="alt1">
                            Direct Options                         
                         </td>                         
                         <td class="alt1">
                            Indirect Options                         
                         </td>
                    </tr>
            </HeaderTemplate>
            <ItemTemplate>
                <tr>
                    <td class="common"><%#Container.DataItem("LevelName")%> 
                    <asp:HiddenField ID="LevelNo" runat="server" Value='<%#Container.DataItem("LevelNo") %>'/></td>
                    <td class="common"><asp:CheckBox ID="ckSelected" runat="server" Checked='<%#chkLevel(CheckInOption, Container.DataItem("LevelNo"))%>' AutoPostBack="true" /></td>                    
                    <td class="common"><asp:CheckBox ID="ckSelIndirect" runat="server" Checked='<%#chkLevel(CheckInOptionIndirect, Container.DataItem("LevelNo"))%>' AutoPostBack="true" /></td>                    
                </tr>
            </ItemTemplate>
            <AlternatingItemTemplate>
                <tr>
                    <td class="common"><%#Container.DataItem("LevelName")%> 
                    <asp:HiddenField ID="LevelNo" runat="server" Value='<%#Container.DataItem("LevelNo") %>'/></td>
                    <td class="common"><asp:CheckBox ID="ckSelected" runat="server" Checked='<%#chkLevel(CheckInOption, Container.DataItem("LevelNo"))%>' AutoPostBack="true" /></td>                    
                    <td class="common"><asp:CheckBox ID="ckSelIndirect" runat="server" Checked='<%#chkLevel(CheckInOptionIndirect, Container.DataItem("LevelNo"))%>' AutoPostBack="true" /></td>                    
                </tr>
            </AlternatingItemTemplate>
            <FooterTemplate>
                <tr>
                    <td colspan="2" style="text-align: center;">
                    </td>
                </tr>
                </table>
            </FooterTemplate>
        </asp:Repeater>
        
        </asp:Panel>
        </div> 
        </div> 
    </form>
</body>
</html>
