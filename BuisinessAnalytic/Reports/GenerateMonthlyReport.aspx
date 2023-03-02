<%@ Page Language="VB" AutoEventWireup="false" CodeFile="GenerateMonthlyReport.aspx.vb" Inherits="Billing_FileImport_ImportLines" %>

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
        <h1>Generate Monthly Report</h1>
                   
               
           <table width="500">
            
                <tr >
                 <td style="text-align: center" width="30%" class="alt1">
                    Month
                 </td>
                <td style="text-align: center" width="30%" class="alt1">
                    Year
                </td>
               
               
           </tr>
            <tr >
                 <td style="text-align: center" width="30%" >
                  
                     <asp:DropDownList ID="DLMonth" runat="server" CssClass="common">
                                  </asp:DropDownList>
                 </td>
                <td style="text-align: center" width="30%">
                    
                        <asp:DropDownList ID="DLYear" runat="server" CssClass="common">
                          
                           </asp:DropDownList>
                </td>
               
               
           </tr>
        </table>
   
             <table id="Table2" width="40%">
       
        
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
         
    </div>
    </form>
</body>
</html>

