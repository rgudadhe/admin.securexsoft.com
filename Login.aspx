<%@ Page Language="VB" AutoEventWireup="false" CodeFile="Login.aspx.vb" Inherits="Loginnew" %>

<!DOCTYPE HTML>



<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server"><meta http-equiv="x-ua-compatible" content="IE=9">
    <title></title>
	<link id="Link2" rel="shortcut icon" href="logo.jpg" type="image/x-icon" />
    <link href="css/login.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/js-image-slider.js" type="text/javascript"></script>
    <script src="../Scripts/mcVideoPlugin.js" type="text/javascript"></script>
   <link href="css/generic.css" rel="stylesheet" type="text/css" />

    </head>
<body >
    
       
  
    <form id="frmLogin" runat="server">

        <div id="login-wapper">
<div class="wapper-inner" style="margin-top:-100px !important;">
<div class="login-main" >
<asp:Label ID="lblErrorMsg" runat="server" Visible="false" CssClass="lblErrorMsg" ForeColor="Red" Font-Bold="true" Text="This User Is Already LoggedIn"></asp:Label>
<asp:ValidationSummary ID="valsSummary" runat="server" CssClass="lblErrorMsg" DisplayMode="List"
                                EnableViewState="false" ForeColor="Orange" runat="server" ></asp:ValidationSummary>
                            <asp:RequiredFieldValidator ID="valrEmail" runat="server" Display="None" ControlToValidate="username"
                                ErrorMessage="Login ID Required" EnableViewState="False" ForeColor="Orange" ></asp:RequiredFieldValidator>
                            <asp:RequiredFieldValidator ID="valrPassword" runat="server" Display="None" ControlToValidate="password"
                                ErrorMessage="Password Required" EnableViewState="False" ForeColor="Orange"  ></asp:RequiredFieldValidator>
                            <%--<asp:RequiredFieldValidator ID="valrOfficeId" runat="server" Display="None" ControlToValidate="txtOfficeId"
                                ErrorMessage="Office Name Required" EnableViewState="False" ForeColor="Orange" ></asp:RequiredFieldValidator>--%>
                            <asp:CustomValidator ID="valcInvalid" runat="server" CssClass="lblErrorMsg" Display="None"
                                ErrorMessage="Invalid Login Details" EnableViewState="False" ForeColor="Orange" ></asp:CustomValidator>
           
<span class="main"></span>

<div class="inside-content">
<div class="divider">
 <asp:Label ID="Label1" runat="server" EnableViewState="False" SkinID="lblLogin"
                                Text="LOGIN ID"></asp:Label>  
       
         <asp:TextBox ID="username" runat="server"></asp:TextBox>
         
</div>

<div class="divider">
  <asp:Label ID="lblPassword" runat="server" EnableViewState="False" SkinID="lblLogin"
                                Text="PASSWORD"></asp:Label>
  <asp:TextBox ID="password" runat="server" TextMode="Password"></asp:TextBox>
    <asp:Label ID="Label2" runat="server" Text="" Font-Size="Small"></asp:Label>
       
</div>

<%--<div class="divider">
<asp:Label ID="lblOffceId" runat="server" EnableViewState="False" SkinID="lblLogin"
                                Text="OFFICE KEY"></asp:Label>
         <asp:TextBox ID="txtOfficeId" runat="server" MaxLength="50"></asp:TextBox>
         <asp:RegularExpressionValidator ID="RegularExpressionValidator1" ControlToValidate="txtOfficeId" runat="server" ErrorMessage="Only Numbers allowed" ValidationExpression="\d+"></asp:RegularExpressionValidator>
</div>--%>

<div class="divider" style="text-align:center;">
  <asp:Button ID="btnSubmit" runat="server" Text=""  EnableViewState="False"  class="login-button" value="" style="border:0px; height:50px; background-color:transparent; box-shadow:none; width:60%;"  ></asp:Button>
       
      </div>

<div  class="divider-footer">
<div style="float:left;"><a href="javascript:var w=window.open('http://www.medofficepro.com/system-requirements.html', '_blank', 'width=1024,height=750');" onclick="" style="font-family:Arial ; font-size:10pt;">System Requirements</a></div> 
<div style="float:right;"><a href="javascript:var w=window.open('http://www.medofficepro.com/support-policy.html', '_blank', 'width=1024,height=750');" style="margin-left:17px; font-family:Arial; font-size:10pt; right: auto;">Support Policy</a></div>


</div>
<div class="divider" style="text-align:center; margin-bottom:0px;">
</div>
<%--<a href="mailto:support@medofficepro.com">Forgot Password?</a>--%>
<div style="text-align:center;"><a href="authorization/reclaim_new.aspx"  style="color:Orange; font-family:Arial ; font-size:10pt;">Forgot Password?</a></div>
</div>
<div class="bottom"></div>


</div>
<section >

<asp:Image BackColor="Transparent"  CssClass="HeaderLogo" Height="76px" width="295px" ID="Image1" runat="server"  ImageUrl="../images/SecureXFlow_01.png" />

</section>
<aside>
<div class="slider-bar">


  <div id="sliderFrame">
        <div id="slider">
          
             <a href="http://www.medofficepro.com/contact-us.htm" target="_blank">
                <img src="../Images/SecureFaxLoginSlider.jpg"  alt="Image3" />
            </a>
        </div>
    </div>



</div>
</aside>
<footer>
<asp:Image ID="Image3" runat="server" ImageUrl="https://data.securexsoft.com/Assets/Images/solution-for-health.jpg" />
</div>
</div>
</form>
</body>
</html>