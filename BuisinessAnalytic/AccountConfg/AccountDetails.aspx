<%@ Page Language="VB" AutoEventWireup="false" CodeFile="AccountDetails.aspx.vb" Inherits="Billing_LCMethods_LCMethodology" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <link rel="stylesheet" type="text/css" href="../../App_Themes/Css/Styles.css"/>
<link rel="stylesheet" type="text/css" href="../../App_Themes/Css/Common.css"/>
    <title>Account Details</title>
</head>
<body>
    <form id="form1" runat="server">
        <div id="body">
        <div id="cap"></div>
        <div id="main">
        <h1>View Account Details</h1>
        <asp:Panel ID="Panel1" runat="server" HorizontalAlign="Left">
            <div>
                <asp:Label ID="lblDisp"  runat="server" CssClass="Title" ForeColor="#C00000"></asp:Label>
                <table width="500">
                    <tr>
                        <td colspan="2" class="HeaderDiv">
                            Search Account Details
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right" >
                            Account Name
                        </td>
                        <td style="text-align: left" >
                            <asp:DropDownList ID="DLAct" runat="server" CssClass="common" AutoPostBack="True" >
                                <asp:ListItem Text="Any" value="" selected="True" ></asp:ListItem>
                            </asp:DropDownList>
                        </td> 
                    </tr>
                </table> 
                <asp:Table ID="Table2" runat="server" Width="90%">
       
             
       <asp:TableRow ID="TableRow2" HorizontalAlign="Left" runat="server">
        <asp:TableCell ID="TableCell4"  HorizontalAlign="Right" runat="server" Width="25%" CssClass="alt5" >
            Clinic\Hospital Name
        </asp:TableCell> 
        <asp:TableCell ID="TableCell5"  HorizontalAlign="Left" runat="server" Width="25%" CssClass="alt5" >
        <asp:TextBox ID="TxtDescr" runat="server"  CssClass="common" />
        </asp:TableCell>
        <asp:TableCell ID="TableCell6"  HorizontalAlign="Right" runat="server" Width="25%"  CssClass="alt5" >
                 Group
        </asp:TableCell> 
        <asp:TableCell ID="TableCell10"  HorizontalAlign="Left" runat="server" Width="25%" CssClass="alt5" >
        <asp:DropDownList ID="DLGroup" runat="server" CssClass="common" >
            </asp:DropDownList>
        </asp:TableCell>
        </asp:TableRow>
       
       <asp:TableRow ID="TableRow3" HorizontalAlign="Left" runat="server">
        <asp:TableCell ID="TableCell11"  HorizontalAlign="Right" runat="server" Width="25%" >
            Category
        </asp:TableCell> 
        <asp:TableCell ID="TableCell12"  HorizontalAlign="Left" runat="server" Width="25%" >
            <asp:DropDownList ID="DLCategory" runat="server" CssClass="common" >
            </asp:DropDownList>
        </asp:TableCell>
        <asp:TableCell ID="TableCell13"  HorizontalAlign="Right" runat="server" Width="25%" >
            Mode
        </asp:TableCell> 
        <asp:TableCell ID="TableCell14"  HorizontalAlign="Left" runat="server" Width="25%" >
         <asp:DropDownList ID="DLMode" runat="server" CssClass="common" >
         <asp:ListItem Text="Not Set" Value=""></asp:ListItem> 
         <asp:ListItem Text="LocationWise" Value="LC"></asp:ListItem> 
         <asp:ListItem Text="DictatorWise" Value="DC"></asp:ListItem> 
         <asp:ListItem Text="DeviceWise" Value="DV"></asp:ListItem> 
         <asp:ListItem Text="TemplateWise" Value="TW"></asp:ListItem> 
         <asp:ListItem Text="TATWise" Value="TT"></asp:ListItem> 
         </asp:DropDownList>
        </asp:TableCell>
        </asp:TableRow>
       
       <asp:TableRow ID="TableRow6" HorizontalAlign="Left" runat="server">
        <asp:TableCell ID="TableCell15"  HorizontalAlign="Right" runat="server" Width="25%" >
            Account Number
        </asp:TableCell> 
        <asp:TableCell ID="TableCell16"  HorizontalAlign="Left" runat="server" Width="25%" >
        <asp:TextBox ID="TxtActNumber" runat="server"  CssClass="common" />
        </asp:TableCell>
        <asp:TableCell ID="TableCell17"  HorizontalAlign="Right" runat="server" Width="25%" >
            Billing Account Number
        </asp:TableCell> 
        <asp:TableCell ID="TableCell18"  HorizontalAlign="Left" runat="server" Width="25%" >
        <asp:TextBox ID="TxtBillNumber" runat="server"  CssClass="common" />
        </asp:TableCell>
        </asp:TableRow>
       
       <asp:TableRow ID="TableRow7" HorizontalAlign="Left" runat="server">
        <asp:TableCell ID="TableCell19"  HorizontalAlign="Right" runat="server" Width="25%" >
            Contact Person
        </asp:TableCell> 
        <asp:TableCell ID="TableCell20"  HorizontalAlign="Left" runat="server" Width="25%" >
        <asp:TextBox ID="TxtContPerson" runat="server"  CssClass="common"  />
        </asp:TableCell>
        <asp:TableCell ID="TableCell21"  HorizontalAlign="Right" runat="server" Width="25%" >
           Telephone
        </asp:TableCell> 
        <asp:TableCell ID="TableCell22"  HorizontalAlign="Left" runat="server" Width="25%" >
        <asp:TextBox ID="TxtTel" runat="server" CssClass="common"  />
        </asp:TableCell>
        </asp:TableRow>
       
       <asp:TableRow ID="TableRow8" HorizontalAlign="Left" runat="server">
        <asp:TableCell ID="TableCell23"  HorizontalAlign="Right" runat="server" Width="25%" >
            E-Mail Address
        </asp:TableCell> 
        <asp:TableCell ID="TableCell24"  HorizontalAlign="Left" runat="server" Width="25%" >
        <asp:TextBox ID="TxtEMail" runat="server"  CssClass="common" />
        </asp:TableCell>
        <asp:TableCell ID="TableCell25"  HorizontalAlign="Right" runat="server" Width="25%" >
            E-Fax
        </asp:TableCell> 
        <asp:TableCell ID="TableCell26"  HorizontalAlign="Left" runat="server" Width="25%" >
        <asp:TextBox ID="TxtEFax" runat="server" CssClass="common"  />
        </asp:TableCell>
        </asp:TableRow>
       
       <asp:TableRow ID="TableRow9" HorizontalAlign="Left" runat="server">
        <asp:TableCell ID="TableCell27"  HorizontalAlign="Right" runat="server" Width="25%" >
            Address
        </asp:TableCell> 
        <asp:TableCell ID="TableCell28"  HorizontalAlign="Left" runat="server" Width="25%" >
        <asp:TextBox ID="TxtAddress" runat="server" CssClass="common" />
        </asp:TableCell>
        <asp:TableCell ID="TableCell29"  HorizontalAlign="Right" runat="server" Width="25%" >
            City
        </asp:TableCell> 
        <asp:TableCell ID="TableCell30"  HorizontalAlign="Left" runat="server" Width="25%" >
        <asp:TextBox ID="TxtCity" runat="server"  CssClass="common" />
        </asp:TableCell>
        </asp:TableRow>
       
       <asp:TableRow ID="TableRow10" HorizontalAlign="Left" runat="server">
        <asp:TableCell ID="TableCell31"  HorizontalAlign="Right" runat="server" Width="25%" >
            State
        </asp:TableCell> 
        <asp:TableCell ID="TableCell32"  HorizontalAlign="Left" runat="server" Width="25%" >
        <asp:TextBox ID="TxtState" runat="server"  CssClass="common" />
        </asp:TableCell>
        <asp:TableCell ID="TableCell33"  HorizontalAlign="Right" runat="server" Width="25%" >
            Country
        </asp:TableCell> 
        <asp:TableCell ID="TableCell34"  HorizontalAlign="Left" runat="server" Width="25%" >
        <asp:TextBox ID="TxtCntry" runat="server"  CssClass="common" />
        </asp:TableCell>
        </asp:TableRow>
        
        <asp:TableRow ID="TableRow11" runat="server">
        <asp:TableCell ID="TableCell35"  HorizontalAlign="Right" runat="server" Width="25%" >
            Zip
        </asp:TableCell> 
        <asp:TableCell ID="TableCell36"  HorizontalAlign="Left" runat="server" Width="25%" >
        <asp:TextBox ID="TxtZip" runat="server" CssClass="common"  />
        </asp:TableCell>
        <asp:TableCell ID="TableCell37"  HorizontalAlign="Right" runat="server" Width="25%" >
            Delivery Method
        </asp:TableCell> 
        <asp:TableCell ID="TableCell38"  HorizontalAlign="Left" runat="server" Width="25%" >
         <asp:DropDownList ID="DLDelv" runat="server"  CssClass="common" >
        <asp:ListItem Text="Not Set" Value=""></asp:ListItem> 
         <asp:ListItem Text="E-Mail" Value="EMail"></asp:ListItem> 
         <asp:ListItem Text="E-Fax" Value="EFax"></asp:ListItem> 

            </asp:DropDownList>
         </asp:TableCell>
        </asp:TableRow>
        
          <asp:TableRow ID="TableRow12"  HorizontalAlign="Left" runat="server">
        <asp:TableCell ID="TableCell39"  HorizontalAlign="Right" runat="server" Width="25%" >
            Payment Term
        </asp:TableCell> 
        <asp:TableCell ID="TableCell40"  runat="server" Width="25%" >
         <asp:DropDownList ID="DLTerm" runat="server" CssClass="common" >
         <asp:ListItem Text="Not Set" Value=""></asp:ListItem> 
         <asp:ListItem Text="NET 10" Value="10"></asp:ListItem> 
         <asp:ListItem Text="NET 15" Value="15"></asp:ListItem> 
         <asp:ListItem Text="NET 30" Value="30"></asp:ListItem> 
         <asp:ListItem Text="NET 45" Value="45"></asp:ListItem> 
         <asp:ListItem Text="NET 60" Value="60"></asp:ListItem> 
            </asp:DropDownList>
        </asp:TableCell>
        <asp:TableCell ID="TableCell1"  HorizontalAlign="Right" runat="server" Width="25%" >
            Groupwise Invoices
        </asp:TableCell> 
        <asp:TableCell ID="TableCell2"  HorizontalAlign="Left" runat="server" Width="25%" >
         <asp:DropDownList ID="DLSepInvoice" runat="server" CssClass="common" >
         <asp:ListItem Text="No" Value="0"></asp:ListItem> 
         <asp:ListItem Text="Yes" Value="1"></asp:ListItem> 
         
         </asp:DropDownList>
        </asp:TableCell>
        </asp:TableRow>  
        
              
        <asp:TableRow ID="TableRow5" HorizontalAlign="Center" runat="server">
        <asp:TableCell ID="TableCell9" ColumnSpan="4" runat="server" >
            <center>
            <asp:Button ID="Button2" runat="server"  CssClass="button" Text="Submit"   />
            </center>
        </asp:TableCell>
        </asp:TableRow>
        </asp:Table>
       

        <asp:HiddenField ID="HUserID" runat="server" />
    </div>
        </asp:Panel>
        </div> 
        </div> 
    </form>
</body>
</html>
