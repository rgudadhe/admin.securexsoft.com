<%@ Page Language="VB" AutoEventWireup="false" CodeFile="ImportUserCost.aspx.vb" Inherits="Billing_FileImport_ImportLines" %>

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
        <h1>Import Users Cost</h1>
     
               
               
          
   
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
        <asp:TableCell>Name</asp:TableCell> 
        <asp:TableCell>UserID</asp:TableCell> 
        <asp:TableCell>Cost</asp:TableCell> 
        <asp:TableCell>Currency</asp:TableCell> 
        <asp:TableCell>Target</asp:TableCell> 
        
        <asp:TableCell>STATUS</asp:TableCell> 
        
        </asp:TableRow>
        
        </asp:Table>     
        <br />
        <a href="UserCost - MM DD YYYY.xls"><span style="font-size: 10pt; font-family: Trebuchet MS">
            <strong>Click here</strong></span></a><span style="font-size: 10pt; font-family: Trebuchet MS"><strong>
                to download template</strong></span>
    </div>
    </form>
</body>
</html>

