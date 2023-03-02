<%@ Page Language="VB" AutoEventWireup="false" CodeFile="ImportLines.aspx.vb" Inherits="Billing_FileImport_ImportLines" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 1.0 Transitional/EN">


<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
 <title>Daily Team Report</title>
    <link rel="Stylesheet" type="text/css" href="../../App_Themes/Css/styles.css"/>
    <link rel="Stylesheet" type="text/css" href="../../App_Themes/Css/Common.css"/>
</head>
<body>
    <form id="form1" runat="server">
        <div id="body">
        <div id="cap"></div>
        <div id="main">
        <h1>Import Platform Units</h1>
     
               
               <table   id="Table1">
        <tr>
                <td style="text-align: left;" class="HeaderDiv" valign="top" >
                    Platform</td>
                        <td style="text-align: left;" class="HeaderDiv" valign="top" >
                    
                        <asp:DropDownList ID="DLPlatform" runat="server" AutoPostBack="true" CssClass ="common" >
                        </asp:DropDownList>
               </tr>
               
           
                </table> 
             <table id="Table2" width="40%">
        <tr>
                <td class="DEMO4" valign="top" colspan ="2" >
                    Click on Browse button
                        and select the file.</td>
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
          <asp:Table ID="TblDetails" runat="server" >
        <asp:TableRow>
        <asp:TableCell>Platform</asp:TableCell> 
        <asp:TableCell>Username</asp:TableCell> 
        <asp:TableCell>JobNumber</asp:TableCell> 
        <asp:TableCell>Level</asp:TableCell> 
        <asp:TableCell> Units</asp:TableCell> 
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

