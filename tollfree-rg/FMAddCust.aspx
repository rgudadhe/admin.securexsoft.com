<%@ Page Language="vb" AutoEventWireup="false" CodeFile="FMAddCust.aspx.vb" Inherits="FMAddCust" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Add Customer Details</title>
   <link href= "../App_Themes/Css/Main.css" type="text/css" rel="stylesheet" />
    <link href= "../App_Themes/Css/Common.css" type="text/css" rel="stylesheet" />
    <link href= "../App_Themes/Css/Styles.css" type="text/css" rel="stylesheet" />
</head>
<body bgcolor="White">
    <form id="form1" runat="server">
	<div id="body" style="height: 100%;" >
    <div id="cap"></div>
    <div id="main">
	<h1>Add Customer Details</h1>
	<asp:Panel ID="Panel1" HorizontalAlign="Left" runat="server" >
    <div>
        <table cellspacing="0" width="50%">
            <tr>
                <td style="text-align: center; height: 15px;" colspan="2" class="HeaderDiv">
                    Customer Details</td>
            </tr>
            <tr>
                <td class="style6" bgcolor="#99CCFF">
                    Customer Name</td>
               <td class="style3">
                     <asp:TextBox ID="txtcustname" runat="server" width="300px"></asp:TextBox>
                    </asp:DropDownList>
                </td>
             </tr>
             <tr>
                <td class="style6" bgcolor="#99CCFF">
                    Customer Address</td>
                <td class="style3">
                    <asp:TextBox ID="txtcaddress" runat="server" Height="50px" Width="300px" TextMode="MultiLine"></asp:TextBox>
					
                </td>
            <tr>
                <td style="text-align: center; height: 15px;" colspan="2" class="HeaderDiv">
                    Agreement Terms</td>
            </tr>
            <tr>
                <td class="style6" bgcolor="#99CCFF">
                    Date of Agreement</td>
                <td class="style3">
                    <asp:TextBox ID="txtdoa" runat="server"></asp:TextBox>
					 [mm/dd/yyyy]
                </td>
            </tr>
            <tr>
                <td class="style6" bgcolor="#99CCFF">
                    Annual Rate Increase</td>
                <td class="style3">
                    <asp:TextBox ID="txtarate" runat="server"></asp:TextBox>
					 [%]
                </td>
            </tr>
            <tr>
               <td class="style6" bgcolor="#99CCFF">
                    Billing Frequency</td>
                <td class="style3">
                    <asp:TextBox ID="txtfrequency" runat="server"></asp:TextBox>
			    </td>
            </tr>
            <tr>
                <td class="style6" bgcolor="#99CCFF" >
                    Payment Term</td>
                <td class="style3">
                    <asp:TextBox ID="txtpterm" runat="server"></asp:TextBox>
					
                </td>
            </tr>
            <tr>
                <td class="style6" bgcolor="#99CCFF">
                    Finance Charges</td>
                <td class="style3">
                    <asp:TextBox ID="txtfcharge" runat="server"></asp:TextBox>
					[%]
                </td>
            </tr>
            <tr>
                <td class="style6" bgcolor="#99CCFF">
                    Agreement Term in Year</td>
                <td class="style3">
                    <asp:TextBox ID="txttyear" runat="server"></asp:TextBox>
					
                </td>
            </tr>
            <tr>
                <td class="style6" bgcolor="#99CCFF">
                    Agreement Term in Month</td>
                <td class="style3">
                    <asp:TextBox ID="txttmonth" runat="server"></asp:TextBox>
					
                </td>
            </tr>
            <tr>
                <td class="style6" bgcolor="#99CCFF">
                    Renewal Term in Year</td>
                <td class="style3">
                    <asp:TextBox ID="txtrtyear" runat="server"></asp:TextBox>
					
                </td>
            </tr>
            <tr>
                <td class="style6" bgcolor="#99CCFF">
                    Renewal Term in Months</td>
                <td class="style3">
                    <asp:TextBox ID="txtrtmonth" runat="server"></asp:TextBox>
					
                </td>
            </tr>
            <tr>
                <td class="style6" bgcolor="#99CCFF">
                    Notice Period</td>
                <td class="style3">
                    <asp:TextBox ID="txtnperiod" runat="server"></asp:TextBox>
					[days]
                </td>
            </tr>
            <tr>
                <td style="text-align: center; height: 15px;" colspan="2" class="HeaderDiv">
                    Rate Per Billing Unit</td>
            </tr>
            <tr>
                <td class="style8" bgcolor="#99CCFF">
                    Billing Unit</td>
                <td class="style9">
                    <asp:TextBox ID="txtbunit" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="style6" bgcolor="#99CCFF">
                    Rate per Billing Unit</td>
                <td class="style3">
                    <asp:TextBox ID="txtrate" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="style6" bgcolor="#99CCFF">
                    STAT rate</td>
                <td class="style3">
                    <asp:TextBox ID="txtsrate" runat="server"></asp:TextBox>
&nbsp;</td>
            </tr>
            <tr>
                <td class="style6" bgcolor="#99CCFF">
                    Minimum Billing</td>
                <td class="style3">
                    <asp:TextBox ID="txtmbilling" runat="server" ></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td style="text-align: center; height: 15px;" colspan="2" class="HeaderDiv">
                    Rate with Guranteed Discount</td>
            </tr>
            
            <tr>
                <td class="style6" bgcolor="#99CCFF">
                    Rate</td>
                <td class="style3">
                    <asp:TextBox ID="txtdrate" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="style8" bgcolor="#99CCFF">
                    STAT Rate</td>
                <td class="style9">
                    <asp:TextBox ID="txtdsrate" runat="server"></asp:TextBox>
                </td>
            </tr>
            
            <tr>
                <td style="text-align: center; height: 15px;" colspan="2" class="HeaderDiv">
                    EMR Interface Charges</td>
            </tr>
            
			<tr>
                <td class="style8" bgcolor="#99CCFF">
                    Receiving HL7 Rate</td>
                <td class="style9">
                    <asp:TextBox ID="txtrhl7rate" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="style8" bgcolor="#99CCFF">
                    Sending HL7 Rate</td>
                <td class="style9">
                    <asp:TextBox ID="txtshl7rate" runat="server"></asp:TextBox>
                </td>
            </tr>
             <tr>
                <td class="style8" bgcolor="#99CCFF">
                    Monthly Maintainance Rate</td>
                <td class="style9">
                    <asp:TextBox ID="txtmmaintrate" runat="server"></asp:TextBox>
                </td>
            </tr>
			<tr>
                <td style="text-align: center; height: 15px;" colspan="2" class="HeaderDiv">
                    Setup and Support Charges</td>
			</tr>
			 <tr>
                <td class="style8" bgcolor="#99CCFF">
                    Template Setup</td>
                <td class="style9">
                    <asp:TextBox ID="txttsetuprate" runat="server"></asp:TextBox>
                </td>
            </tr>
             <tr>
                <td class="style8" bgcolor="#99CCFF">
                    Physician Setup</td>
                <td class="style9">
                    <asp:TextBox ID="txtpsetuprate" runat="server"></asp:TextBox>
                </td>
            </tr>
			 <tr>
                <td class="style8" bgcolor="#99CCFF">
                    Telephone Support</td>
                <td class="style9">
                    <asp:TextBox ID="txttelsupprate" runat="server"></asp:TextBox>
                </td>
            </tr>
             <tr>
                <td class="style8" bgcolor="#99CCFF">
                   Onsite Support</td>
                <td class="style9">
                    <asp:TextBox ID="txtsitesupprate" runat="server"></asp:TextBox>
                </td>
            </tr>
             <tr>
                <td class="style8" bgcolor="#99CCFF">
                    Onsite Travel Cost</td>
                <td class="style9">
                    <asp:TextBox ID="txtsitetrvct" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td style="text-align: center; height: 15px;" colspan="2" class="HeaderDiv">
                    FaxPlus Charges</td>
			</tr>
			<tr>
                <td class="style8" bgcolor="#99CCFF">
                    FAX Plus</td>
                <td class="style9">
                    <asp:TextBox ID="txtfxplus" runat="server"></asp:TextBox>
                </td>
            </tr>
			 <tr>
                <td style="text-align: center;" colspan="2">
                    <asp:Button ID="Button1" runat="server" CssClass="button"  Text="Submit" />
                </td>
            </tr>
        </table>
    
    </div>
	</asp:Panel>
    </form>
</body>
</html>
