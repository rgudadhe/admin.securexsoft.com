<%@ Page Language="VB" AutoEventWireup="false" CodeFile="ViewUnitDetails.aspx.vb" Inherits="Billing_LCMethods_LCMethodology" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
<link rel="stylesheet" type="text/css" href="../../App_Themes/Css/styles1.css"/>
<link rel="stylesheet" type="text/css" href="../../App_Themes/Css/Common.css"/>


    <title>Untitled Page</title>
</head>
<body>
    <form id="frmAcct" runat="server">
    <div id="body">
        <div id="cap"></div>
        <div id="main">
        <h1>View Account Rates </h1>
   
                    <asp:Label ID="lblDisp" runat="server" Font-Bold="True"    ForeColor="#C00000"></asp:Label>
         <br />
                  
        <%--<asp:Table ID="tblMain" runat="server" GridUnits="Both" Width="500">
        <asp:TableRow ID="Row1" HorizontalAlign="Center" runat="server"  CssClass="DEMO4" >
        <asp:TableCell ID="Cell1" CssClass="DEMO5" HorizontalAlign="Right" runat="server" >
        <span style="font-size: 8pt; font-family: Arial"><strong>
                Account Name: </strong></span>
         </asp:TableCell>
                  <asp:TableCell HorizontalAlign="Left" >

         <asp:DropDownList ID="DLAct" runat="server"   Font-Bold="True"   AutoPostBack="true"  >
         <asp:ListItem Text="Select Account" value="" selected="True" ></asp:ListItem>
                  </asp:DropDownList>
         </asp:TableCell>

        </asp:TableRow>
        </asp:Table>--%>
        <br />
        
        <asp:Table ID="Table1" runat="server" GridLines="Both" >
        
        <asp:TableRow  CssClass="tblbgbody">
         <asp:TableCell ID="TableCell111"     HorizontalAlign="Center" runat="server"  >
            Account Name
        </asp:TableCell> 
         <asp:TableCell ID="TableCell3"     HorizontalAlign="Center" runat="server"  >
            Billing Number
        </asp:TableCell> 
        <asp:TableCell ID="TableCell1"     HorizontalAlign="Center" runat="server"  >
            Mode
        </asp:TableCell> 
        <asp:TableCell ID="TableCell2"     HorizontalAlign="Center" runat="server"  >
            Sub Account Name
        </asp:TableCell> 
        <asp:TableCell   Font-Bold="True" HorizontalAlign="Right" >
            Minimum Billing 
        
        </asp:TableCell>
        
        <asp:TableCell   Font-Bold="True" HorizontalAlign="Right" >
            Billing 2X Per Month 
        </asp:TableCell>
               
        <asp:TableCell     Font-Bold="True" HorizontalAlign="Right"   >
            By WorkType 
        </asp:TableCell>
       
        
       
       
        <asp:TableCell ID="TableCell121"   HorizontalAlign="Center" runat="server" >
            Rate
        </asp:TableCell>
        <asp:TableCell ID="TableCell131"     HorizontalAlign="Center" runat="server" >
            Misc Rate
        </asp:TableCell> 
         <asp:TableCell ID="TableCell11"   HorizontalAlign="Center" runat="server" >
            Stat Rate
        </asp:TableCell> 
        <asp:TableCell ID="TableCell211"    HorizontalAlign="Center" runat="server" >
            UnitCount Method
        </asp:TableCell> 
        </asp:TableRow> 
      </asp:Table>
      
      
       
      
        
        <asp:HiddenField ID="HRecExist" runat="server" />
        <asp:HiddenField ID="HRecCount" runat="server" />
        <asp:HiddenField ID="HMode" runat="server" />
        <asp:HiddenField ID="HAccountID" runat="server" />
    </div>
    </div> 
    </form>
</body>
</html>
