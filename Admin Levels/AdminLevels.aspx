<%@ Page Language="VB" AutoEventWireup="false" CodeFile="AdminLevels.aspx.vb" Inherits="_Default" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 1.0 Transitional/EN">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Administractor Level</title>
    <link href= "../App_Themes/Css/styles.css" type="text/css" rel="stylesheet" />
    <link href= "../App_Themes/Css/Common.css" type="text/css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <div id="body">
        <div id="cap"></div>
        <div id="main">
        <h1>Administractor Levels</h1>
        <asp:Panel ID="Panel1" runat="server" HorizontalAlign="Left">
            <asp:Label ID="lblLinks" CssClass="common" runat="server" ></asp:Label>
            <asp:MultiView ID="mvAdminLevels" runat="server">        	
		<asp:View ID="viewLevels" runat="server">
            <table style="width: 201px">
                <tr>
                    <td class="alt1">
                    
                        Level Name
                    
                        </td>

                    <td class="alt1">
                    
                        Description
                    
                    </td>
                </tr>
                <tr >
                    <td>
                        <asp:TextBox ID="txtLevelName" runat="server" Height="18px" Width="111px" CssClass="common" ></asp:TextBox>
                    </td>
                    
                    <td>
                        <asp:TextBox ID="txtLevelDesc" runat="server" CssClass="common"
                            Width="111px"></asp:TextBox></td>
                </tr>
                <tr>
                    <td colspan="2">
                        <center>
                        <asp:Button ID="cmdNext1" runat="server" Text="Add" Width="124px" CssClass="button"/></td>
                        </center>
                </tr>
            </table>
		</asp:View>
        
    <asp:View ID="viewLinks" runat="server">
    <table style="width: 201px">
        <tr>
            <td class="alt1">
            
               Link Caption
            
             </td>
            <td class="alt1">
               Link Path
            </td>
        </tr>
        <tr>
            <td>
                <asp:TextBox ID="txtLinkCap" runat="server" Width="111px" CssClass="common"></asp:TextBox></td>
            
            <td>
                <asp:TextBox ID="txtLinkPath" runat="server" CssClass="common" Width="111px"></asp:TextBox></td>
        </tr>
        <tr>
            <td colspan="2">
                <asp:HiddenField ID="hdnNewLevel" runat="server" />
                <center>
                    <asp:Button ID="btnAddNew" runat="server" Text="Add Link" OnClientClick="AddNewRow" CssClass="button" Width="86px" />
        
        <asp:Button ID="btnFinish" runat="server" Text="Finish" CssClass="button"  Width="86px" /></center></td>
        </tr>
    </table>
        </asp:View>             
    </asp:MultiView>
   
        </asp:Panel>
    
   </div> 
   </div>          
    
    </form>
</body>
</html>
