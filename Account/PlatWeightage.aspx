<%@ Page Language="VB" AutoEventWireup="false" CodeFile="PlatWeightage.aspx.vb" Inherits="Account_PlatWeightage" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 1.0 Transitional/EN">
<LINK href= "../../styles/Default.css" type="text/css" rel="stylesheet">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Untitled Page</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
     <table id="MainTable" width="100%" style="font-size: 10pt; font-family: 'Trebuchet MS'; font-style: italic; color:Gray; " border ="2" cellpadding ="2" cellspacing ="2">
        <tr>
                <td style="text-align: left;" class="HeaderDiv" valign="top" colspan ="2">
                    <span style="font-family: Trebuchet MS; color: white;"><strong><em>Group
                        Account - Accounts Assignment</em></strong></span></td>
               </tr>
                </table>
               
               <table   id="Table1" style="font-size: 10pt; font-family: 'Trebuchet MS'; font-style: italic; color:Gray; " border ="2" cellpadding ="2" cellspacing ="2">
        <tr>
                <td style="text-align: left;" class="SMSelected" valign="top" >
                    <span style="font-family: Trebuchet MS; color: white;"><strong><em>Group
                        Platform</em></strong></span></td>
                        <td style="text-align: left;" class="SMSelected" valign="top" >
                    <span style="font-family: Trebuchet MS; color: white;"><strong><em>
                        <asp:DropDownList ID="DLPlatform" runat="server" AutoPostBack="true" >
                        </asp:DropDownList></em></strong></span></td>
               </tr>
               
               <tr>
                <td style="text-align: left;" class="SMSelected" valign="top" >
                    <span style="font-family: Trebuchet MS; color: white;"><strong><em>MT Lines</em></strong></span></td>
                        <td style="text-align: left;" class="SMSelected" valign="top" >
                    <span style="font-family: Trebuchet MS; color: white;"><strong><em>
                        <asp:TextBox Width="50"  ID="TextBox1" runat="server"></asp:TextBox></em></strong></span></td>
               </tr>
               <tr>
                <td style="text-align: left;" class="SMSelected" valign="top" >
                    <span style="font-family: Trebuchet MS; color: white;"><strong><em>MT Plus Lines</em></strong></span></td>
                        <td style="text-align: left;" class="SMSelected" valign="top" >
                    <span style="font-family: Trebuchet MS; color: white;"><strong><em>
                        <asp:TextBox Width="50" ID="TextBox2" runat="server"></asp:TextBox></em></strong></span></td>
                                       </tr>
             <tr>
                <td style="text-align: left;" class="SMSelected" valign="top" >
                    <span style="font-family: Trebuchet MS; color: white;"><strong><em>QA Lines</em></strong></span></td>
                        <td style="text-align: left;" class="SMSelected" valign="top" >
                    <span style="font-family: Trebuchet MS; color: white;"><strong><em>
                        <asp:TextBox Width="50" ID="TextBox3" runat="server"></asp:TextBox></em></strong></span></td>
               </tr><tr>
                <td style="text-align: left;" class="SMSelected" valign="top" >
                    <span style="font-family: Trebuchet MS; color: white;"><strong><em>QAB Lines</em></strong></span></td>
                        <td style="text-align: left;" class="SMSelected" valign="top">
                    <span style="font-family: Trebuchet MS; color: white;"><strong><em>
                        <asp:TextBox Width="50" ID="TextBox4" runat="server"></asp:TextBox></em></strong></span></td>
               </tr><tr>
                <td style="text-align: left;" class="SMSelected" valign="top" >
                    <span style="font-family: Trebuchet MS; color: white;"><strong><em>QABSE Lines</em></strong></span></td>
                        <td style="text-align: left;" class="SMSelected" valign="top">
                    <span style="font-family: Trebuchet MS; color: white;"><strong><em>
                        <asp:TextBox Width="50" ID="TextBox5" runat="server"></asp:TextBox></em></strong></span></td>
               </tr><tr>
                <td style="text-align: left;" class="SMSelected" valign="top" >
                    <span style="font-family: Trebuchet MS; color: white;"><strong><em>PPQA Lines</em></strong></span></td>
                        <td style="text-align: left;" class="SMSelected" valign="top" >
                    <span style="font-family: Trebuchet MS; color: white;"><strong><em>
                        <asp:TextBox Width="50" ID="TextBox6" runat="server"></asp:TextBox></em></strong></span></td>
               </tr>
               <tr>
                <td style="text-align: center;" class="SMSelected" valign="top" colspan ="2">
                   <asp:Button ID="BtnSubmit1" runat="server" Text="Submit"  /> 
                  </td>
               </tr>
                </table> 
                
    </div>
    </form>
</body>
</html>
