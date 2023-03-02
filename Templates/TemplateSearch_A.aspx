<%@ Page Language="VB" AutoEventWireup="false" CodeFile="TemplateSearch_A.aspx.vb" Inherits="ets.Templates_TemplateSearchA" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 1.0 Transitional/EN">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Template Attributes</title>
    <link href= "../App_Themes/Css/Main.css" type="text/css" rel="stylesheet" />
    <link href= "../App_Themes/Css/Styles.css" type="text/css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
    <div id="body">
    <div id="cap"></div>
    <div id="main">
    <h1>Edit Template Attributes</h1>
        <asp:Panel ID="Panel1" HorizontalAlign="Left" runat="server">
            <table cellpadding="2" cellspacing="2" border="2"    >
            <tr>
                <td colspan="2" class="HeaderDiv" >
                    Template Search</td>
            </tr>
            <tr>
            <td >
                    Template Name
                    </td>
                <td >
                    <asp:TextBox ID="txtTemplateName" runat="server" Width="267px"></asp:TextBox>
                    </td>
            </tr>
            <tr>
                <td colspan="2" style="text-align:center">
                    <asp:Button ID="btnSearch" CssClass="button"  runat="server" Text="Search Template" /></td>
            </tr>
            
            </table><asp:RegularExpressionValidator Display="None"  
    id="RegtxtTemplateName"  
    runat="server" 
    ControlToValidate="txtTemplateName" 
    ValidationExpression="^[a-zA-Z-%]+$"
    ErrorMessage="Template Name - Please enter valid input."
   />
        <asp:ValidationSummary ID="ValidationSummary1" runat="server" HeaderText="Following error occurs:" ShowMessageBox="true" DisplayMode="BulletList" ShowSummary="False" />      
            
        </asp:Panel>
        <asp:Panel ID="Panel2" runat="server" HorizontalAlign="Left">
            <asp:Repeater ID="rptTemp" runat="server">
                     <HeaderTemplate>
                        <table>
                        <tr>            
                        <td class="alt1" align="center">Template Name</td>            
                        <td class="alt1" align="center">Edit Template Attributes</td>
                        <td class="alt1" align="center">Edit Custom Attributes</td>
                        </tr>
            </HeaderTemplate>

            <ItemTemplate>
            <tr>            
                        <td width="70%"><asp:Label ID="txtName" runat="server" Text='<%#Container.DataItem("TemplateName")%>' ></asp:label>
                        <asp:HiddenField ID="TemplateID" runat="server" Value='<%#Container.DataItem("TemplateID")%>'/></td>            
                        <td width="15%" align="center"><asp:Button ID="btnEdit" runat="server" Text="Edit" OnClick="btnEdit_Click" Width="100%" CssClass="button"/></td>
                         <td width="15%" align="center"><asp:Button ID="Button2" runat="server" Text="Edit" OnClick="btnCEdit_Click" Width="100%" CssClass="button"/></td>
            </tr>
            </ItemTemplate>
            <AlternatingItemTemplate>
            <tr bgcolor="#cccccc">
                        <td width="70%"><asp:Label ID="txtName" runat="server" Text='<%#Container.DataItem("TemplateName")%>' ></asp:label>
                        <asp:HiddenField ID="TemplateID" runat="server" Value='<%#Container.DataItem("TemplateID")%>'/></td>            
                        <td width="15%" align="center"><asp:Button ID="btnEdit" runat="server" Text="Edit" OnClick="btnEdit_Click" Width="100%" CssClass="button"/></td>
                        <td width="15%" align="center"><asp:Button ID="Button1" runat="server" Text="Edit" OnClick="btnCEdit_Click" Width="100%" CssClass="button"/></td>
            </tr>
            </AlternatingItemTemplate>
            <FooterTemplate>
            </table>
            </FooterTemplate>
            </asp:Repeater>
        </asp:Panel>
                       
                
                
    
                    <asp:Literal ID="iResponse" runat="server" ></asp:Literal>
           
    </div> 
    </div>     
    </form>
</body>
</html>
