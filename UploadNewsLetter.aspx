<%@ Page Language="VB" AutoEventWireup="false" CodeFile="UploadNewsLetter.aspx.vb" Inherits="UploadNewsLetter" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 1.0 Transitional/EN">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <link href="App_Themes/Css/styles.css"type="text/css" rel="stylesheet" />
    <link href="App_Themes/Css/Common.css"type="text/css" rel="stylesheet" />
    <title>New NewsLetter</title>
</head>
<body>
    <form id="form1" runat="server">
        <asp:Panel ID="Panel1" runat="server" HorizontalAlign="Left">
            <div id="body">
        <div id="cap"></div>
        <div id="main">
        <h1>News Letter</h1>
            <div>
     <table width="80%">
            <tr>
                <td colspan="2" class="HeaderDiv">
                    New NewsLetter
                </td>
            </tr>
            <tr>
                <td style="text-align: right" class="common">
                    Month/Year</td>
                <td style="text-align: left;" class="common">
                    <asp:DropDownList ID="DLMonth" runat="server" CssClass="common">
                    <asp:ListItem Text="January" value="1"></asp:ListItem> 
                    <asp:ListItem Text="February" value="2"></asp:ListItem>
                    <asp:ListItem Text="March"  value="3"></asp:ListItem>
                    <asp:ListItem Text="April"  value="4"></asp:ListItem>
                    <asp:ListItem Text="May"  value="5"></asp:ListItem>
                    <asp:ListItem Text="June" value="6"></asp:ListItem>
                    <asp:ListItem Text="July" value="7" ></asp:ListItem>
                    <asp:ListItem Text="August" value="8"></asp:ListItem>
                    <asp:ListItem Text="Septermber" value="9"></asp:ListItem>
                    <asp:ListItem Text="October" value="10"></asp:ListItem>
                    <asp:ListItem Text="November" value="11"></asp:ListItem>
                    <asp:ListItem Text="December" value="12"></asp:ListItem>
                    </asp:DropDownList> 
                    <asp:DropDownList ID="DLYear" runat="server" CssClass="common">
                    <asp:ListItem Text="2007"></asp:ListItem> 
                    <asp:ListItem Text="2008"></asp:ListItem> 
                    <asp:ListItem Text="2009"></asp:ListItem> 
                    <asp:ListItem Text="2010"></asp:ListItem> 
                    <asp:ListItem Text="2011"></asp:ListItem> 
                    <asp:ListItem Text="2012"></asp:ListItem> 
                    <asp:ListItem Text="2013"></asp:ListItem> 
                    </asp:DropDownList></td>
            </tr>
            <tr>
                <td style="text-align: right" valign="top" class="common">
                    NewsLetter</td>
                <td style="text-align: left;">
                    <asp:FileUpload ID="FileUpload1" runat="server" CssClass="common" Width="496px"  /></td>
            </tr>
            <tr>
                <td style="text-align: center; " colspan="2">
                    <asp:Button ID="Button1" runat="server" Text="Submit" CssClass="button"   /></td>
            </tr>
        </table>
        <br />
        <asp:Label ID="iresponse" runat="server" CssClass="common" Text=""></asp:Label><br />
        </div>
        </div> 
        </div> 
        </asp:Panel>
    </form>
</body>
</html>
