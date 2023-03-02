<!DOCTYPE html>
<%@ Page Language="VB" AutoEventWireup="false" CodeFile="reclaim_new.aspx.vb" Inherits="reclaim_new" %>

<%@ Register Assembly="System.Web.Extensions, Version=1.0.61025.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI" TagPrefix="asp" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<html>
<head id="Head1" runat="server">
<meta http-equiv="x-ua-compatible" content="IE=9">
    <title>SecureXFlow</title>
    <link href="../css/login.css" rel="stylesheet" type="text/css" />
    <link href="../css/generic.css" rel="stylesheet" type="text/css" />
      <script type="text/javascript" language="javascript">

          function DisableBackButton() {
              window.history.forward()
          }
          DisableBackButton();
          window.onload = DisableBackButton;
          window.onpageshow = function (evt) { if (evt.persisted) DisableBackButton() }
          window.onunload = function () { void (0) }
</script>
</head>
<body>
    <form id="frmLogin" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <div id="login-wapper">
        <div class="wapper-inner" style="margin-top: -100px !important;">
            <div class="login-main">
                <asp:Label ID="lblMessage" runat="server" Visible="false" CssClass="lblErrorMsg"
                    Text="This User Is Already LoggedIn"></asp:Label>
               
                <span class="reclaim"></span>
                <div class="inside-content">
                    <div class="divider">
                        <asp:Label ID="Label1" runat="server" EnableViewState="False" SkinID="lblLogin" Text="EMAIL ID"></asp:Label>
                        <asp:TextBox ID="txtemail" runat="server"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Email ID is required"
                            ControlToValidate="txtemail"></asp:RequiredFieldValidator>
                        <asp:RegularExpressionValidator Display="None" ID="txtemailvalidator" ErrorMessage="Please enter vaild email address."
                            class="lblErrorMsg" ForeColor="Red" ValidationExpression="^([a-zA-Z0-9_\-\.]+)@([a-zA-Z0-9_\-\.]+)\.([a-zA-Z]{2,5})$"
                            ControlToValidate="txtEmail" runat="server"></asp:RegularExpressionValidator>
                    </div>
                    <div class="divider">
                        <asp:RadioButtonList ID="chkList" runat="server">
                            <asp:ListItem Text="Forgot Username" Selected="True" Value="0"></asp:ListItem>
                            <asp:ListItem Text="Forgot Password" Value="1"></asp:ListItem>
                        </asp:RadioButtonList>
                        <br />
                        <asp:Label ID="Label2" runat="server" Text="" Font-Size="Small"></asp:Label>
                    </div>
                  
                    <div class="divider" style="text-align: center;">
                        <asp:Button ID="btnSubmit" runat="server" Text="Continue" ForeColor="White" Font-Size="18px"
                            Font-Bold="true" EnableViewState="False" class="submit-button" value="" Style="border: 0px;
                            height: 50px; background-color: transparent; box-shadow: none; width: 60%;">
                        </asp:Button>
                    </div>
                    <div class="divider-footer">
                        <div style="float: left;">
                            <a href="javascript:var w=window.open('http://securexsoft.com/System-Requirements.html', '_blank', 'width=1024,height=750');"
                                onclick="" style="font-family: Arial; font-size: 10pt;">System Requirements</a></div>
                        <div style="float: right;">
                            <a href="javascript:var w=window.open('http://www.medofficepro.com/support-policy.html', '_blank', 'width=1024,height=750');"
                                style="margin-left: 17px; font-family: Arial; font-size: 10pt; right: auto;">Support
                                Policy</a></div>
                    </div>
                    <div class="divider" style="text-align: center; margin-bottom: 0px;">
                    </div>
                    <%--<a href="mailto:support@medofficepro.com">Forgot Password?</a>--%>
                    <div style="text-align: center;">
                    </div>
                </div>
                <div class="bottom">
                </div>
            </div>
            <section>

<asp:Image BackColor="Transparent"  CssClass="HeaderLogo" Height="76px" width="295px" ID="Image1" runat="server"  ImageUrl="../../images/SecureXFlow_01.png" />

</section>
            <aside>
<div class="slider-bar">


  <div id="sliderFrame">
        <div id="slider">
          
             <a href="http://www.medofficepro.com/contact-us.htm" target="_blank">
                <img src="../../Images/SecureFaxLoginSlider.jpg"  alt="Image3" />
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
