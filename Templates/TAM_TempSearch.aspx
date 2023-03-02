<%@ Page Language="VB" AutoEventWireup="false" CodeFile="TAM_TempSearch.aspx.vb" Inherits="ets.Templates_TATempSearch" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 1.0 Transitional/EN">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Templates Assignments</title>
    <link href= "../App_Themes/Css/Main.css" type="text/css" rel="stylesheet" />
    <link href= "../App_Themes/Css/Styles.css" type="text/css" rel="stylesheet" />
    <script language="javascript" src="../App_Themes/JS/tooltip.js" type="text/javascript"></script>
</head>
<body>
    <form id="form1" runat="server">
        <div id="body">
        <div id="cap"></div>
        <div id="main">
        <h1>Template Assignments</h1>
        <asp:Panel ID="Panel1" runat="server" HorizontalAlign="Left">
            <table width="50%">
                <tr>
                    <td class="HeaderDiv">
                        <asp:Label ID="lblPhyName" runat="server" Text=""></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td  class="alt">
                        Search Template
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:TextBox ID="txtTemplateName" runat="server" Width="267px"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td style="text-align:center">
                        <asp:Button ID="btnSearch" runat="server" Text="Search Template" CssClass="button" /></td>
                    </tr>
            </table>
        </asp:Panel>        
        <asp:Panel ID="Panel2" runat="server" HorizontalAlign="Left">
            <asp:Repeater ID="rptTemp" runat="server">
                 <HeaderTemplate>
                    <table>
                        <tr>            
                        <td class="alt1" align="center">&nbsp</td>
                        <td class="alt1" align="center">Template Name</td>
                        </tr>
                 </HeaderTemplate>

        <ItemTemplate>
        <tr>        
                    <td><asp:CheckBox ID="chkSel" runat="server">
                    </asp:CheckBox> </td>    
                    <td width=80%><asp:Label ID="txtName" runat="server" Text='<%#Container.DataItem("TemplateName")%>' ></asp:label>
                    <asp:HiddenField ID="TemplateID" runat="server" Value='<%#Container.DataItem("TemplateID")%>'/></td>            

        </tr>
        </ItemTemplate>
        <AlternatingItemTemplate>
        <tr bgcolor="#cccccc">
        <td><asp:CheckBox ID="chkSel" runat="server">
                    </asp:CheckBox> </td>    
                    <td width=80%><asp:Label ID="txtName" runat="server" Text='<%#Container.DataItem("TemplateName")%>' ></asp:label>
                    <asp:HiddenField ID="TemplateID" runat="server" Value='<%#Container.DataItem("TemplateID")%>'/></td>            
        </tr>
        </AlternatingItemTemplate>
        <FooterTemplate>
            <td colspan=2>
                <asp:Button ID="btnSel" CssClass="button" runat="server" Text="Assign Selected" OnClick="btnSel_Click"/>
            </td>
        </table>
        </FooterTemplate>
        </asp:Repeater>
        </asp:Panel>             
                
    
        <asp:Literal ID="iResponse" runat="server" ></asp:Literal>                    
        <asp:HiddenField ID="hdnPhyID" runat="server" />
        </div> 
        </div> 
        <div style='position:absolute; visibility:hidden; z-index:1000;' id='ToolTip'></div>
    </form>
</body>
</html>
