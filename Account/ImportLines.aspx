<%@ Page Language="VB" AutoEventWireup="false" CodeFile="ImportLines.aspx.vb" Inherits="Account_PlatWeightage" %>

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
                    <span style="font-family: Trebuchet MS; color: white;"><strong><em>
                    Platform Lines</em></strong></span></td>
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
               
           
                </table> 
             <table id="Table2" width="40%" style="font-size: 10pt; font-family: 'Trebuchet MS'; font-style: italic;" border ="2" cellpadding ="2" cellspacing ="2">
        <tr>
                <td class="DEMO4" valign="top" colspan ="2" >
                    <span style="font-family: Trebuchet MS; color: white;"><strong><em>Click on Browse button
                        and select the file.</em></strong></span></td>
               </tr>
                
            <tr>
                <td class="DEMO5" valign="top">
                    
                        <asp:FileUpload ID="FileUpload1" runat="server" />
        
                    
            </td>
            </tr>
        
        <tr>
                <td class="DEMO5" valign="top">
                    &nbsp;<asp:Button ID="Button1" runat="server" Text="Submit" CssClass="button" /></td>
            </tr>
            <tr>
                <td colspan="2" style="text-align: center">
                <p>
        <asp:Label ID="Label2" runat="server"></asp:Label></p>
                    </td>
            </tr>
           
        </table>
        
        <br />
          <asp:Table ID="TblDetails" runat="server" Font-Names="Trebuchet MS" Font-Size="Small" CellPadding="2" CellSpacing="2" GridLines="Both">
        <asp:TableRow>
        <asp:TableCell>Platform</asp:TableCell> 
        <asp:TableCell>Username</asp:TableCell> 
        <asp:TableCell>JobNumber</asp:TableCell> 
        <asp:TableCell>Level</asp:TableCell> 
        <asp:TableCell> Lines</asp:TableCell> 
        <asp:TableCell>Dictator</asp:TableCell> 
        <asp:TableCell>PostDate</asp:TableCell> 
       
        <asp:TableCell>STATUS</asp:TableCell> 
        
        </asp:TableRow>
        
        </asp:Table>     
        <br />
        <a href="Platform Lines - MM DD YYYY.xls"><span style="font-size: 10pt; font-family: Trebuchet MS">
            <strong>Click here</strong></span></a><span style="font-size: 10pt; font-family: Trebuchet MS"><strong>
                to download template</strong></span>
    </div>
    </form>
</body>
</html>
