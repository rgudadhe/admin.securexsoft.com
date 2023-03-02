<%@ Page Language="VB" AutoEventWireup="false" CodeFile="Copy of reclaim_new.aspx.vb" Inherits="reclaim_new" %>

<%@ Register Assembly="System.Web.Extensions, Version=1.0.61025.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI" TagPrefix="asp" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <link id="Link1" runat="server" rel="shortcut icon" href="/sxsofticon.png" type="image/x-icon" />
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>SecureXFlow Login Page</title>
    <%--<script language="javascript">
function checkVersion() {
checkIE();
}
</script>--%>
    <meta name="description" content="" />
    <meta name="keywords" content="" />
    <meta name="author" content="FocusMX.com" />
    <meta name="robots" content="index,follow" />
    <link href="../css/styles_reclaim.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/AC_RunActiveContent.js" type="text/javascript"></script>
    <!--[if IE 6]>
<script src="DD_belatedPNG_0.0.8a-min.js"></script>
<script>
DD_belatedPNG.fix('.png_bg,.form-main, img');

</script>
<![endif]-->
    <script type="text/javascript" language="javascript">
        function checkVersion() {
            checkIE();
        }
        function OpenModalPopup() {
            //var myObject = new Object();
            //var WinSettings = "center:yes;resizable:no;dialogHeight:300px;dialogWidth:400px;"
            var MyArgs = window.open("ChangePass.aspx", 'name', 'height=300,width=400');
            if (window.focus) { MyArgs.focus() }

            //self.close();
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <div id="main">
        <div id="wraper">
            <div id="logo">
                <asp:Image BackColor="Transparent"  CssClass="HeaderLogo" Height="76px" width="295px" ID="Image1" runat="server"  ImageUrl="../images/SecureXFlow_01.png" />
            </div>
            <div class="clear">
            </div>
            <div style="position: relative; clear: both;">
                <div class="flash">
                    <script type="text/javascript">
                        AC_FL_RunContent('codebase', 'swflash.cab', 'width', '908', 'wmode', 'transparent', 'height', '342', 'title', 'Securexsoft', 'src', '../includes/flash', 'quality', 'high', 'pluginspage', 'https://www.adobe.com/shockwave/download/download.cgi?P1_Prod_Version=ShockwaveFlash', 'movie', '../includes/flash'); //end AC code
                    </script>
                    <noscript>
                        <object classid="clsid:D27CDB6E-AE6D-11cf-96B8-444553540000" codebase="swflash.cab"
                            width="908" height="342" title="Securexsoft">
                            <param name="movie" value="../includes/flash.swf" />
                            <param name="quality" value="high" />
                            <param name="wmode" value="transparent" />
                        </object>
                    </noscript>
                </div>
                <div class="form-main">
                    <form action="" method="get">
                    <div style="position: absolute; width: 256px; height: 27px; left: 68px; top: 116px;">
                        <asp:TextBox runat="server" ID="txtemail" CssClass="input-1"></asp:TextBox><br />
                        <asp:Label ID="lblMessage" runat="server" Width="300px" Height="40px" Font-Size="Smaller"
                            Font-Bold="true" />
                        <asp:RegularExpressionValidator Display="None" ID="txtemailvalidator" ErrorMessage="Please enter vaild email address."
                            class="lblErrorMsg" ForeColor="Red" ValidationExpression="^([a-zA-Z0-9_\-\.]+)@([a-zA-Z0-9_\-\.]+)\.([a-zA-Z]{2,5})$"
                            ControlToValidate="txtEmail" runat="server"></asp:RegularExpressionValidator>
                        <ajaxToolkit:ValidatorCalloutExtender runat="Server" ID="NReqE1" TargetControlID="txtemailvalidator"
                            HighlightCssClass="validatorCalloutHighlight">
                            <Animations>
        <OnShow>
        <Sequence>
        <HideAction Visible="true" />
        <FadeIn Duration="1" MinimumOpacity="0" MaximumOpacity="1" />
        </Sequence>
        </OnShow>
        <OnHide>
        <Sequence>
        <FadeOut Duration="1" MinimumOpacity="0" MaximumOpacity="1" />
        <HideAction Visible="false" />
        </Sequence>
        </OnHide>
                            </Animations>
                        </ajaxToolkit:ValidatorCalloutExtender>
                    </div>
                    <div style="position: absolute; width: 256px; height: 27px; left: 68px; top: 160px;">
                        <asp:Label ID="LBL" runat="server" Text="Please enter your e-mail address to begin the process"
                            CssClass="info"></asp:Label>
                    </div>
                    <div style="position: absolute; width: 256px; height: 57px; left: 68px; top: 210px;">
                        <asp:RadioButtonList ID="chkList" runat="server" CssClass="info">
                            <asp:ListItem Text="Forgot UserName" Value="0"></asp:ListItem>
                            <asp:ListItem Text="Forgot Password" Value="1"></asp:ListItem>
                        </asp:RadioButtonList>
                    </div>
                    <div style="position: absolute; width: 172px; height: 10px; left: 68px; top: 265px;">
                        <%--<a href="#">
                            <asp:ImageButton runat="server" ID="btnfusername" ImageUrl="../images/forgot username_NEW.jpg"
                                Width="172" Height="40" /></a>--%>
                        <asp:Button ID="btnSubmit" runat="server" Text="SUBMIT" ForeColor="White" class="submit-button"
                            OnClick="btnSubmit_Click"></asp:Button>
                    </div>
                    <div style="position: absolute; width: 172px; height: 47px; left: 68px; top: 325px;">
                        <%--<a href="#">
                            <asp:ImageButton runat="server" ID="btnfpassword" ImageUrl="../images/forgot password_NEW.jpg"
                                Width="172" Height="40" /></a>
                            <br />--%>
                    </div>
                    <asp:RequiredFieldValidator runat="server" ID="EmailIDREQ" ControlToValidate="txtemail"
                        Display="None" ErrorMessage="<b>Required Field Missing</b><br />EmailID is required." />
                    <ajaxToolkit:ValidatorCalloutExtender runat="Server" ID="NReqE" TargetControlID="EmailIDREQ"
                        HighlightCssClass="validatorCalloutHighlight" />
                    <asp:RequiredFieldValidator runat="server" ID="UPREQ" ControlToValidate="chklist"
                        Display="None" ErrorMessage="<b>Required Field Missing</b><br />Please select option." />
                    </form>
                </div>
            </div>
            <div class="clear">
            </div>
            <div class="solution-health">
                <img src="../Images/solution-for-health.jpg" alt="Solutions for Healthcare and More"
                    width="338" height="37" /></div>
            <div class="clear">
            </div>
            <div class="copy">
                Copyright SecureXSoft, All Rights Reserved 2014</div>
        </div>
        <div class="clear">
        </div>
    </div>
    </form>
    <div align="center">
        <br>
        <br>
        <br>
        <br>
        <span id="cdSiteSeal1">
            <script type="text/javascript" src="//tracedseals.starfieldtech.com/siteseal/get?scriptId=cdSiteSeal1&amp;cdSealType=Seal1&amp;sealId=55e4ye7y7mb73e6c2d3d8f824b61ec5khfy7mb7355e4ye743f60b6e16e94ec67"></script>
        </span>
    </div>
</body>
</html>
