<%@ Page Language="VB" AutoEventWireup="false" CodeFile="NewUserEmail.aspx.vb" Inherits="User_Management_NewUserEmail" %>

<%@ Register Assembly="System.Web.Extensions, Version=1.0.61025.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI" TagPrefix="asp" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<html>
<head><meta http-equiv="x-ua-compatible" content="IE=9">
    <title></title>
    <link href="../css/bootstrap.css" rel="stylesheet" type="text/css" media="all">
    <!-- jQuery (necessary for Bootstrap's JavaScript plugins) -->
    <script src="js/jquery-1.11.0.min.js"></script>
    <!-- Custom Theme files -->
    <link href="../css/style.css" rel="stylesheet" type="text/css" media="all" />
    <link href="font-awesome/css/font-awesome.min.css" rel="stylesheet" type="text/css"
        media="all" />
    <!-- Custom Theme files -->
    <meta name="viewport" content="width=device-width, initial-scale=1">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <script type="application/x-javascript"> addEventListener("load", function() { setTimeout(hideURLbar, 0); }, false); function hideURLbar(){ window.scrollTo(0,1); }>
    </script>
    <meta name="keywords" content="Medicative Responsive web template, Bootstrap Web Templates, Flat Web Templates, AndriodCompatible web template, Smartphone Compatible web template, free webdesigns for Nokia, Samsung, LG, SonyErricsson, Motorola web design" />
    <!--Google Fonts-->
    <link href='http://fonts.googleapis.com/css?family=Roboto:500,900,100,300,700,400'
        rel='stylesheet' type='text/css'>
    <link href='http://fonts.googleapis.com/css?family=Cantata+One' rel='stylesheet'
        type='text/css'>
    <!-- start-smoth-scrolling -->
    <script src="http://ajax.googleapis.com/ajax/libs/jquery/2.1.1/jquery.min.js"></script>
    <script type="text/javascript" src="js/move-top.js"></script>
    <script type="text/javascript" src="js/easing.js"></script>
    <script type="text/javascript">
        jQuery(document).ready(function ($) {
            $(".scroll").click(function (event) {
                event.preventDefault();
                $('html,body').animate({ scrollTop: $(this.hash).offset().top }, 1000);
            });
        });
    </script>
</head>
<body>
    <div>
        <form id="signup" runat="server">
        <div class="main-content" style="text-align: left;">
            <div class="emailvalidation" style="text-align: left;">
                <div class="container" style="text-align: left;">
                    <div style="background: #ffffff url('../images/registration-bg.jpg') no-repeat; width: 905px;
                        height: 577px; margin: 0 auto; padding: 20px">
                        
                        <div class="activate">
                        <br />
                     
                            <h2>
                                Registration</h2>
                        </div>
                        <div class="step1">
                            <div class="step1-top-content">
                                <p>
                                    <span style="font-size: 17px;"></span>Medofficepro, Inc. and all its subsidiaries
                                    have updated security for users access to all the applications offered by the company.
                                    If you are seeing this message, you have not completed this new registration requirement
                                    for this application. Please complete the following resigration in order to proceed
                                    further and access your account. Thanks for your cooperation.
                                </p>
                            </div>
                            <div class="form2">
                                <div class="row">
                                    <div class="col-md-6">
                                        <div class="form-group">
                                            <label for="usr">
                                                First Name</label>
                                            <asp:TextBox ID="txtFname" runat="server" class="form-control" disabled="disabled"></asp:TextBox>
                                            <%--<input name="txtFName" type="text" id="txtFname" disabled="disabled" class="form-control" />--%>
                                        </div>
                                    </div>
                                    <div class="col-md-6">
                                        <div class="form-group">
                                            <label for="usr">
                                                Last Name</label>
                                            <asp:TextBox ID="txtLname" runat="server" class="form-control" disabled="disabled"></asp:TextBox>
                                            <%-- <input name="txtOfficeName" type="text" id="txtLname" class="form-control" />--%>
                                        </div>
                                    </div>
                                    <div class="col-md-6">
                                        <div class="form-group">
                                            <label for="usr">
                                                Email</label>
                                            <asp:TextBox ID="txtEmail" runat="server" class="form-control"></asp:TextBox><br />
                                            <asp:Label ID="lblMessage" runat="server" Text="" />
                                            <%--<input name="txtAddress" type="text" id="txtEmail" class="form-control" />--%>
                                            <asp:RegularExpressionValidator Display="Dynamic" ID="RegularExpressionValidator5"
                                                ErrorMessage="Please enter vaild email address." class="lblErrorMsg" ForeColor="Red"
                                                ValidationExpression="^([a-zA-Z0-9_\-\.]+)@([a-zA-Z0-9_\-\.]+)\.([a-zA-Z]{2,5})$"
                                                ControlToValidate="txtEmail" runat="server"></asp:RegularExpressionValidator>
                                        </div>
                                    </div>
                                    <div class="col-md-6">
                                        <div class="form-group">
                                            <label for="usr">
                                                Username</label>
                                            <asp:TextBox ID="txtUname" runat="server" class="form-control" disabled="disabled"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <br />
                        <div class="continue-btn">
                            <asp:Button ID="submit" runat="server" Text="Submit" class="submit-button" Style="color: White;" />
                            <%--<input type="submit" name="submit" value="Submit" id="submit" class="submit-button"
                                                    style="color: White;" />--%>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <%--<table style="margin: 0 auto">
                        <tr>
                            <td>
                                <h3>
                                    Update your email address
                                </h3>
                            </td>
                        </tr>
                        <tr>
                            <td class="DEMO5">
                                <asp:ScriptManager ID="ScriptManager1" runat="server">
                                </asp:ScriptManager>
                                <asp:Label ID="Label1" runat="server" Text="Your new email address"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="DEMO5">
                                <asp:TextBox ID="txtEmail" runat="server" Width="280" /><br />
                                <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator5" ControlToValidate="txtEmail"
                                    Display="None" ErrorMessage="<b>Required Field Missing</b><br />E-Mail is required." /><br />
                                <asp:RegularExpressionValidator Display="Dynamic" ID="RegularExpressionValidator5"
                                    ErrorMessage="Please enter vaild email address." ForeColor="Red" ValidationExpression="^([a-zA-Z0-9_\-\.]+)@([a-zA-Z0-9_\-\.]+)\.([a-zA-Z]{2,5})$"
                                    ControlToValidate="txtEmail" runat="server"></asp:RegularExpressionValidator>
                                <ajaxToolkit:ValidatorCalloutExtender runat="Server" ID="ValidatorCalloutExtender5"
                                    TargetControlID="RequiredFieldValidator5" HighlightCssClass="validatorCalloutHighlight" />
                            </td>
                        </tr>
                        <tr>
                            <td valign="middle" class="button">
                                <asp:Button ID="submit" Text="Submit" runat="server" /><br />
                                <asp:Label ID="lblMessage" runat="server" Text="" />
                            </td>
                        </tr>
                    </table>--%>
        </form>
    </div>
</body>
</html>
