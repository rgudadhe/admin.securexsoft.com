<%@ Page Language="VB" AutoEventWireup="false" CodeFile="ExceptionJobHistory.aspx.vb" Inherits="FaxPlus_ExceptionJobHistory" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Exception Job History</title>
    <link href= "../App_Themes/Css/styles.css" type="text/css" rel="stylesheet" />
    <link href= "../App_Themes/Css/Common.css" type="text/css" rel="stylesheet" />
</head>
<body>
  <%--  <form id="form1" runat="server">
    <div style="text-align:left" >

<table> 
<tr><td class="HeaderDiv">Physician Address</td>
</tr>
<tr>
<td>
<asp:TextBox ID="txtAddress" runat="server" Rows="3" TextMode="MultiLine" Width="304px" Text='<%#DataBinder.Eval(CType(Me.BindingContainer, DataGridItem).DataItem, "RPADD").ToString%>' ReadOnly="true"></asp:TextBox>  
    <asp:Button ID="Button1" runat="server" Text="Set another Physician" CssClass="button" />
</td>
</tr>
</table>

<table>  
<tr>
<td class="alt1">First Name</td>
<td class="alt1">Middle Name</td>
<td class="alt1">Last Name</td>
<td class="alt1">Degree</td>
</tr>
<tr>
<td><asp:TextBox ID="txtFName" runat="server" ></asp:TextBox></td>     
<td><asp:TextBox ID="txtMName" runat="server"></asp:TextBox></td>
<td><asp:TextBox ID="txtLName" runat="server"></asp:TextBox></td>
<td><asp:TextBox ID="txtDegree" runat="server"></asp:TextBox></td>
</tr>
<tr>
<td class="alt" colspan="2">Address</td>
<td class="alt">City</td>
<td class="alt">State</td>
</tr>
<tr>
<td colspan="2" rowspan="3"><asp:TextBox ID="txtadd" runat="server" Rows="3" TextMode="MultiLine" Height="64px" Width="312px"></asp:TextBox></td>
<td><asp:TextBox ID="txtCity" runat="server"></asp:TextBox></td>
<td><asp:TextBox ID="txtState" runat="server"></asp:TextBox></td>
</tr>
<tr>
<td class="alt">Zip Code</td>
<td class="alt">Country</td>
</tr>
<tr>
<td style="height: 10px"><asp:TextBox ID="txtZip" runat="server"></asp:TextBox></td>
<td style="height: 10px"><asp:TextBox ID="txtCountry" runat="server"></asp:TextBox></td>
</tr>
<tr>
<td class="alt">Phone#</td>
<td class="alt">Fax#</td>
<td class="alt">E-Mail</td>
    <td rowspan="2" style="text-align: center">
        <asp:Button ID="btnSet" runat="server" Text="Update Record" CssClass="button" /></td>                            
        </tr>
        <tr>
<td><asp:TextBox ID="txtPhone" runat="server"></asp:TextBox></td>
<td><asp:TextBox ID="txtFax" runat="server"></asp:TextBox></td>
<td><asp:TextBox ID="txtEmail" runat="server"></asp:TextBox></td>
</tr>
</table>

<asp:Label id="CurrentTab" runat="server" Visible="false"></asp:Label><asp:Label id="Messages" runat="server" Visible="false"></asp:Label>
<asp:HiddenField ID="hdnTrans" runat="server" />
<asp:HiddenField ID="hdnRP" runat="server" />
</div>
    </form>--%>
       <form id="form1" runat="server">
    <div style="text-align:left" >
        <asp:Label ID="lblMsg" runat="server" ForeColor="Red" Font-Names="Trebuchet MS" Font-Size="10" Text=""></asp:Label>
<table> 
<tr><td class="HeaderDiv">Physician Address</td>
</tr>
<tr>
<td>
<asp:TextBox ID="txtAddress" runat="server" Rows="3" TextMode="MultiLine" Width="304px" Text='<%#DataBinder.Eval(CType(Me.BindingContainer, DataGridItem).DataItem, "RPADD").ToString%>' ReadOnly="true"></asp:TextBox>  
    <asp:Button ID="Button1" runat="server" Text="Set another Physician" CssClass="button" />
</td>
</tr>
</table>

<table>  
<tr>
<td class="alt1">First Name</td>
<td class="alt1">Middle Name</td>
<td class="alt1">Last Name</td>
<td class="alt1">Degree</td>
</tr>
<tr>
<td><asp:TextBox ID="txtFName" runat="server" ></asp:TextBox></td>     
<td><asp:TextBox ID="txtMName" runat="server"></asp:TextBox></td>
<td><asp:TextBox ID="txtLName" runat="server"></asp:TextBox></td>
<td><asp:TextBox ID="txtDegree" runat="server"></asp:TextBox></td>
</tr>
<tr>
<td class="alt" colspan="2">Address</td>
<td class="alt">City</td>
<td class="alt">State</td>
</tr>
<tr>
<td colspan="2" rowspan="3"><asp:TextBox ID="txtadd" runat="server" Rows="3" TextMode="MultiLine" Height="64px" Width="312px"></asp:TextBox></td>
<td><asp:TextBox ID="txtCity" runat="server"></asp:TextBox></td>
<td><asp:TextBox ID="txtState" runat="server"></asp:TextBox></td>
</tr>
<tr>
<td class="alt">Zip Code</td>
<td class="alt">Country</td>
</tr>
<tr>
<td style="height: 10px"><asp:TextBox ID="txtZip" runat="server"></asp:TextBox></td>
<td style="height: 10px"><asp:TextBox ID="txtCountry" runat="server"></asp:TextBox></td>
</tr>
<tr>
<td class="alt">Phone#</td>
<td class="alt">Fax#</td>
<td class="alt">E-Mail</td>
    <td rowspan="2" style="text-align: center">
        <asp:Button ID="btnSet" runat="server" Text="Update Record" CssClass="button" /></td>                            
        </tr>
        <tr>
<td><asp:TextBox ID="txtPhone" runat="server"></asp:TextBox></td>
<td><asp:TextBox ID="txtFax" runat="server"></asp:TextBox></td>
<td><asp:TextBox ID="txtEmail" runat="server"></asp:TextBox></td>
</tr>
</table>

<asp:Label id="CurrentTab" runat="server" Visible="false"></asp:Label><asp:Label id="Messages" runat="server" Visible="false"></asp:Label>
<asp:HiddenField ID="hdnTrans" runat="server" />
<asp:HiddenField ID="hdnRP" runat="server" />
<asp:HiddenField ID="hdnfrom" runat="server" />
<asp:HiddenField ID="hdnRecordID" runat="server" />
</div>
    </form>
</body>
</html>
