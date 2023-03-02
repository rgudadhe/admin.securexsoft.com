<%@ Page Language="VB" AutoEventWireup="false" CodeFile="ViewTemplateAssigments.aspx.vb" Inherits="Templates_ViewTemplateAssigments" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Untitled Page</title>
    <link href= "../App_Themes/Css/Default.css" type="text/css" rel="stylesheet" />
    <link href= "../App_Themes/Css/Styles.css" type="text/css" rel="stylesheet" />
    <link href= "../App_Themes/Css/Common.css" type="text/css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
    <div id="body">
        <div id="cap"></div>
        <div id="main">
        <h1>Template Assignments</h1>
            <%--<div style="text-align:left;">
                <asp:Label ID="lblDisplay" runat="server" Text=""></asp:Label>    
            </div>--%>
            <table width="100%" border="0">
               <tr>
                  <td style="border:0">  <asp:label id="lblCurrentPage" runat="server"></asp:label></td>
               </tr>
               <tr>
                  <td style="border:0">
                        <asp:button id="cmdPrev" runat="server" text=" << "></asp:button>
                        <asp:button id="cmdNext" runat="server" text=" >> "></asp:button>
                  </td>
               </tr>
            </table>
            <asp:Repeater ID="rptParent" runat="server">
                <HeaderTemplate></HeaderTemplate>
                <ItemTemplate>
                    <table style="text-align:left " width="70%">
                        <tr>
                            <td style=" text-align:left;border:0;font-family:Trebuchet MS; font-size:10pt; font-weight:bold">
                                <%#Container.DataItem("PhyName")%>
                                <asp:HiddenField ID="RhdnPhyID" runat="server" Value='<%#Container.DataItem("PhyID")%>' />
                            </td>
                        </tr>
                        <tr>
                            <td style="border:0">
                                <asp:Repeater ID="rptChild" runat="server">
                                    <HeaderTemplate>
                                        <table>
                                            <tr>
                                                <td class="alt1">Template Name</td>
                                                <td class="alt1">TAT</td>
                                                <td class="alt1">STAT</td>
                                                <td class="alt1">TimeZone</td>
                                                <td class="alt1">DueTime(24Hrs)</td>
                                                <td class="alt1">Sequence</td>
                                                <td class="alt1">Action</td>
                                            </tr>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                            <tr>
                                                <td>
                                                    <%#Container.DataItem("TemplateName")%>
                                                    <asp:HiddenField ID="hdnTemplateID" runat="server" Value='<%#Container.DataItem("TemplateID").tostring()%>' />
                                                </td>
                                                <td >
                                                    <asp:DropDownList ID="DDLTAT" runat="server">
                                                     <asp:ListItem Value="2" Text="2 Hrs"></asp:ListItem>
                                                     <asp:ListItem Value="4" Text="4 Hrs"></asp:ListItem>
                                                     <asp:ListItem Value="6" Text="6 Hrs"></asp:ListItem>
                                                     <asp:ListItem Value="8" Text="8 Hrs"></asp:ListItem>
                                                        <asp:ListItem Value="12" Text="12 Hrs"></asp:ListItem>
                                                        <asp:ListItem Value="24" Text="24 Hrs"></asp:ListItem>
                                                        <asp:ListItem Value="36" Text="36 Hrs"></asp:ListItem>
                                                        <asp:ListItem Value="48" Text="48 Hrs"></asp:ListItem>
                                                        <asp:ListItem Value="72" Text="72 Hrs"></asp:ListItem>
                                                        <asp:ListItem Value="96" Text="96 Hrs"></asp:ListItem>
                                                        <asp:ListItem Value="120" Text="120 Hrs"></asp:ListItem>
                                                    </asp:DropDownList>
                                                    
                                                </td>
                                                <td><asp:TextBox ID="txtSTAT" runat="server" Text='<%#Container.DataItem("STAT")%>' Width="50"></asp:TextBox></td>
                                                <td>
                                                <asp:DropDownList ID="DDLTZ" runat="server">
                                                   <asp:ListItem Value="0" Text="Eastern Time (EST - Default)"></asp:ListItem>
                                                   <asp:ListItem value="-1" text="Central Time (CST)"></asp:ListItem>
                                                   <asp:ListItem value="-2" Text="Mountain Time (MST)"></asp:ListItem>
                                                   <asp:ListItem value="-3" Text="Pacific Time (PST)"></asp:ListItem>
                                                   <asp:ListItem value="-4" Text="Alaska Time (AKST)"></asp:ListItem>
                                                   <asp:ListItem value="-6" Text="Hawaii Time"></asp:ListItem>
                                                </asp:DropDownList>
                                                </td>
                                                <td><asp:TextBox ID="txtTime" runat="server" Text='<%#Container.DataItem("Time")%>' Width="20" MaxLength="2"></asp:TextBox>                                    </td>
                                                <td><asp:TextBox ID="txtSeq" runat="server" Text='<%#Container.DataItem("WorkType")%>' Width="50"></asp:TextBox></td>
                                                <td><asp:Button ID="btnRemove" runat="server" Text="Remove Template" CssClass="button" OnClick="btnRemove_click" CommandArgument='<%#Container.DataItem("TemplateID").tostring()%>' /></td>
                                            </tr>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        </table>
                                    </FooterTemplate>
                                </asp:Repeater>
                            </td>
                        </tr>
                        <tr>
                            <td style="border:0">
                                <asp:Button ID="btnSave" runat="server" Text="Save Changes" CssClass="button" OnClick="btnSave_click" CommandArgument='<%#Container.DataItem("PhyID")%>' /><br />
                            </td>
                        </tr>
                    </table>
                </ItemTemplate>
            </asp:Repeater>
            
            <asp:HiddenField ID="hdnPhyIDs" runat="server" />
        </div>
        </div>
    </form>
</body>
</html>
