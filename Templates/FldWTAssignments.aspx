<%@ Page Language="VB" AutoEventWireup="false" CodeFile="FldWTAssignments.aspx.vb" Inherits="ets.Templates_EditTemplateAssignments" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 1.0 Transitional/EN">
<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>DVR Folder Settings</title>
    <link href= "../App_Themes/Css/Main.css" type="text/css" rel="stylesheet" />    
    <link href= "../App_Themes/Css/Styles.css" type="text/css" rel="stylesheet" />    
</head>
<body>
    <form id="form1" runat="server">
    <div id="body">
    <div id="cap"></div>
    <div id="main">
    <h1>DVR Folder Setting</h1>
    <div>
        <table style="width: 100%;">
            <tr>
                <td style="width: 100%; " class="HeaderDiv">
                    <asp:Label ID="lblCaption" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <td style="text-align:left" >
                    <asp:Label ID="iResponse" CssClass="Title" ForeColor="#C00000" runat="server"></asp:Label>
                </td> 
            </tr>
            <tr>
                <td >
                    <asp:Repeater ID="rptPhyTemp" runat="server">
                        <HeaderTemplate>
                            <table>                                
                                <tr>
                                    <td align="center" class="alt1">
                                        Template Name</th>
                                    <td align="center" class="alt1">
                                        Folder Name</th>                                                           
                                </tr>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <tr>
                                <td align="center" width=50%>
                                    <asp:Label ID="txtName" runat="server" Text='<%#Container.DataItem("TemplateName")%>'></asp:Label>
                                    <asp:HiddenField ID="TemplateID" runat="server" Value='<%#Container.DataItem("TemplateID")%>' />
                                </td>
                                <td align="center" width=50%>
                                    <asp:TextBox ID="txtFolder" runat="server" Text='<%#Container.DataItem("FolderName")%>' ></asp:TextBox>                                    
                                </td>                                                                                               
                            </tr>
                        </ItemTemplate>
                        <AlternatingItemTemplate>
                            <tr>
                                <td align="center" width=50%>
                                    <asp:Label ID="txtName" runat="server" Text='<%#Container.DataItem("TemplateName")%>'></asp:Label>
                                    <asp:HiddenField ID="TemplateID" runat="server" Value='<%#Container.DataItem("TemplateID")%>' />
                                </td>
                                <td align="center" width=50%>
                                    <asp:TextBox ID="txtFolder" runat="server" Text='<%#Container.DataItem("FolderName")%>'></asp:TextBox>                                    
                                </td>                                                                                                 
                            </tr>
                        </AlternatingItemTemplate>
                        <FooterTemplate>
                            <tr>
                            <td colspan="2" style="text-align:center;" >
                                <asp:Button CssClass="button"  ID="btnSave" runat="server" Text="Save Changes" OnClick="btnSave_Click"/>
                            </td>
                            </tr>
                            </table>
                        </FooterTemplate>
                    </asp:Repeater>
                    <asp:HiddenField ID="hdnPhyID" runat="server" />                    
                </td>
            </tr>
        </table>
    
    </div>
    </div>
    </div> 
    </form>
</body>
</html>
