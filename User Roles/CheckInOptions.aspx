<%@ Page Language="VB" AutoEventWireup="false" CodeFile="CheckInOptions.aspx.vb" Inherits="Admin_Levels_UsersAdminLevels" %>
<LINK href= "../styles/Main.css" type="text/css" rel="stylesheet">

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 1.0 Transitional/EN">


<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Untitled Page</title>
</head>
<body>
    <form id="form1" runat="server">
    <div style="text-align: center">
        
        <asp:Repeater ID="rptLevels" runat="server">
            <HeaderTemplate>
                <table border="2" cellpadding="2" width="400">
                    <tr >                        
                         <th colspan="3" >
                            
                             
                            Check-In Options For <%#LevelName%>                        
                         <div>
                         </th>
                    </tr>                
                    <tr >
                        <th >
                            Level Names</div>
                        </th>                        
                        <th >
                            
                             
                            Direct Options                         
                         </th>                         
                         <th >
                            
                             
                            Indirect Options                         
                         </th>
                    </tr>
            </HeaderTemplate>
            <ItemTemplate>
                <tr>
                    <td
                            ><%#Container.DataItem("LevelName")%> 
                    <asp:HiddenField ID="LevelNo" runat="server" Value='<%#Container.DataItem("LevelNo") %>'/></td>
                    <td 
                            ><asp:CheckBox ID="ckSelected" runat="server" Checked='<%#chkLevel(CheckInOption, Container.DataItem("LevelNo"))%>' AutoPostBack="true" /></td>                    
                    <td 
                            ><asp:CheckBox ID="ckSelIndirect" runat="server" Checked='<%#chkLevel(CheckInOptionIndirect, Container.DataItem("LevelNo"))%>' AutoPostBack="true" /></td>                    
                </tr>
            </ItemTemplate>
            <AlternatingItemTemplate>
                <tr bgcolor="#cccccc">
                    <td 
                            ><%#Container.DataItem("LevelName")%> 
                    <asp:HiddenField ID="LevelNo" runat="server" Value='<%#Container.DataItem("LevelNo") %>'/></td>
                    <td 
                            ><asp:CheckBox ID="ckSelected" runat="server" Checked='<%#chkLevel(CheckInOption, Container.DataItem("LevelNo"))%>' AutoPostBack="true" /></td>                    
                    <td 
                            ><asp:CheckBox ID="ckSelIndirect" runat="server" Checked='<%#chkLevel(CheckInOptionIndirect, Container.DataItem("LevelNo"))%>' AutoPostBack="true" /></td>                    
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
        &nbsp; &nbsp;</div>
        <asp:HiddenField ID="HContractor" runat="server" /> 
    </form>
    
</body>
</html>
