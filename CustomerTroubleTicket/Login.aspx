<%@ Page Language="VB" AutoEventWireup="false" CodeFile="Login.aspx.vb" Inherits="Login" EnableViewStateMac="false"  Debug="true"   %>

<%@ Register Assembly="System.Web.Extensions, Version=1.0.61025.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI" TagPrefix="asp" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 1.0 Transitional/EN">
<%@ Register
    Assembly="AjaxControlToolkit"
    Namespace="AjaxControlToolkit"
    TagPrefix="ajaxToolkit" %>

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
<style type="text/css">
<!--
body {
	margin-left: 0px;
	margin-top: 0px;
	margin-right: 0px;
	margin-bottom: 0px;
	background-color: #E2EAF1;
}
-->
</style>
<link rel="SHORTCUT ICON" href="favicon.ico"/>
    <title>E-Dictate SecureWeb Login Page</title>
    
    
</head>
<body>
<form id="form1" runat="server"  > 
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
   <table height="132%" border="0" align="center" cellpadding="0" cellspacing="0" bgcolor="#FFFFFF">
  <tr>
    <td height="20%"><img alt="" src="Images/elogoN.JPG" width="192" height="114"/></td>
    <td valign="bottom"><img alt="" src="../images/login_img_02.jpg" width="214" height="114"/></td>
    <td valign="bottom"><img alt="" src="../images/login_img_03.jpg" width="187" height="114"/></td>
    <td valign="bottom"><img alt="" src="../images/login_img_04.jpg" width="161" height="114"/></td>
    <td valign="bottom"><img alt="" src="../images/login_img_05.jpg" width="24" height="55"/></td>
  </tr>
  <tr>
    <td height="10%"><img alt="" src="../images/login_img_06.jpg" width="192" height="59"/></td>
    <td><img alt="" src="../images/login_img_07.jpg" width="214" height="59"/></td>
    <td><img alt="" src="../images/login_img_08.jpg" width="187" height="59"/></td>
    <td><img alt="" src="../images/login_img_09.jpg" width="161" height="59"/></td>
    <td><img alt="" src="../images/login_img_10.jpg" width="24" height="59"/></td>
  </tr>
  <tr>
    <td height="23%"><img alt="" src="../images/login_img_11.jpg" width="192" height="137"/></td>
    <td><img alt="" src="../images/login_img_12.jpg" width="214" height="137"/></td>
    <td colspan="2" background="../images/login_img_13.jpg"><table width="100%" height="137"  border="0" cellpadding="0" cellspacing="0">
      <tr>
        <td width="100" height="22">&nbsp;</td>
        <td width="10">&nbsp;</td>
        <td colspan="2">&nbsp;</td>
      </tr>
      <tr>
        <td>&nbsp;</td>
        <td>&nbsp;</td>
        <td colspan="2">
            <asp:TextBox ID="username" runat="server" Width="120"  ></asp:TextBox></td>
      </tr>
      <tr>
        <td>&nbsp;</td>
        <td>&nbsp;</td>
        <td colspan="2">&nbsp;</td>
      </tr>
      <tr>
        <td>&nbsp;</td>
        <td>&nbsp;</td>
        <td colspan="2"><asp:TextBox ID="Password" runat="server" TextMode="Password"  Width="120" ></asp:TextBox></td>
      </tr>
      <tr>
        <td style="height: 24px">&nbsp;</td>
        <td style="height: 24px">&nbsp;</td>
        <td align="right" valign="top" style="height: 24px"><a href="#">
            <asp:ImageButton ID="ImageButton1" ImageUrl ="~/Images/login_button.jpg" runat="server" width="47" height="18" /></a></td>
        <td valign="top" style="height: 24px">&nbsp;</td>
      </tr>
    </table></td>
    <td><img alt="" src="../images/login_img_14.jpg" width="24" height="137"></td>
  </tr>
  <tr>
    <td height="27%"><img alt="" src="../images/login_img_15.jpg" width="192" height="156"/></td>
    <td><img alt="" src="../images/login_img_16.jpg" width="214" height="156"/></td>
    <td><img alt="" src="../images/login_img_17.jpg" width="187" height="156"/></td>
    <td><img alt="" src="../images/login_img_18.jpg" width="161" height="156"/></td>
    <td><img alt="" src="../images/login_img_19.jpg" width="24" height="156"/></td>
  </tr>
  <tr>
    <td height="20%"><img alt="" src="../images/login_img_20.jpg" width="192" height="118"/></td>
    <td><img alt="" src="../images/login_img_21.jpg" width="214" height="118" border="0" usemap="#Map"/></td>
    <td><img alt="" src="../images/login_img_22.jpg" width="187" height="118" border="0" usemap="#Map2"/></td>
    <td><img alt="" src="../images/login_img_23.jpg" width="161" height="118"/></td>
    <td><img alt="" src="../images/login_img_24.jpg" width="24" height="118"/></td>
  </tr>
</table>
<asp:RequiredFieldValidator runat="server" ID="UNReq"
            ControlToValidate="username"
            Display="None"
            ErrorMessage="<b>Required Field Missing</b><br />Username is required." />
        <ajaxToolkit:ValidatorCalloutExtender runat="Server" ID="NReqE"
            TargetControlID="UNReq"
            HighlightCssClass="validatorCalloutHighlight" />
            
            <asp:RequiredFieldValidator runat="server" ID="PWReq"
            ControlToValidate="Password"
            Display="None"
            ErrorMessage="<b>Required Field Missing</b><br />Password is required." />
        <ajaxToolkit:ValidatorCalloutExtender runat="Server" ID="ValidatorCalloutExtender1"
            TargetControlID="PWReq"
            HighlightCssClass="validatorCalloutHighlight" />
</form>
<map id="Map">
  <area alt="" shape="rect" coords="151,100,214,113" href="../images/PrivacyStatement.pdf" target="_blank"/>
</map>
<map id="Map2">
  <area alt="" shape="rect" coords="-1,100,32,113" href="../images/PrivacyStatement.pdf" target="_blank"/>
</map>

 
            
</body>
</html>
