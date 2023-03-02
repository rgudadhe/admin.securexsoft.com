<%@ Page Language="VB" AutoEventWireup="false" CodeFile="preset90.aspx.vb" Inherits="Passwordmodule_preset" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>Secure-Fax</title>
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
    <style type="text/css">
        .style1
        {
            color: #999999;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div class="main-content">
        <div class="features">
            <div class="container">
                <div style="background: #ffffff url('../images/UserReg-bg.jpg') no-repeat; width: 905px;
                    height: 680px; margin: 0 auto; padding: 20px">
                    <div class="activate">
                        <h2>
                            Reset Password</h2>
                    </div>
                    <div class="para">
                        <p>
                            Your password has not been changed in last 90 days, please change your password now to proceed further. 
                            If you have any questions about this policy. please contact helpdesk at <a>support@medofficepro.com</a> or Skype ID <b>serversupportteam</b>.</p>
                    </div>
            
                <div class="form2">
                    <div class="row">
                       
                        <div class="col-md-4">
                            <div class="form-group">
                                <label for="usr">
                                   Current Password</label>
                                <asp:TextBox ID="txtCPass" runat="server" TextMode="Password"  class="form-control"></asp:TextBox><br />
                                <asp:Label ID="Label1" runat="server" Text=""></asp:Label>
                                 <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Current Password is required "
                                    ControlToValidate="txtCPass"></asp:RequiredFieldValidator><br />
                            </div>
                        </div>
                        <div class="col-md-4">
                            <div class="form-group">
                                <label for="usr">
                                    New Password</label>
                                <asp:TextBox ID="TxtNewPass" TextMode="Password" runat="server" class="form-control" /><br />
                                <asp:RegularExpressionValidator Display="Dynamic" ID="RegularExpressionValidator2"
                                    ErrorMessage="Password does not meet requirements" ForeColor="Red" ValidationExpression="^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[$@$!%*?&])[A-Za-z\d$@$!%*?&]{8,}"
                                    ControlToValidate="TxtNewPass" runat="server"></asp:RegularExpressionValidator>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="New Password is required "
                                    ControlToValidate="TxtNewPass"></asp:RequiredFieldValidator><br />
                            </div>
                        </div>
                        <div class="col-md-4">
                            <div class="form-group">
                                <label for="usr">
                                    Re-enter Password</label>
                                <asp:TextBox ID="TxtCNewPass" TextMode="Password" runat="server" class="form-control" /><br />
                                <asp:Label ID="Label2" runat="server" Text=""></asp:Label>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ErrorMessage="Confirm Password is required "
                                    ControlToValidate="TxtCNewPass"></asp:RequiredFieldValidator><br />
                                <asp:CompareValidator ID="CompareValidator1" ControlToValidate="TxtCNewPass" ControlToCompare="TxtNewPass"
                                    ValueToCompare="=" runat="server" ErrorMessage="Password does not match"></asp:CompareValidator>
                            </div>
                        </div>
                        <div class="col-md-4">
                            <div class="style1" style="margin-left: 450px; width: 400px;">
                                <p>
                                    Password has to meet all the following reqirements.</p>
                                <ul>
                                    <li>Minimum of 8 characters</li>
                                    <li>Combination of letters and numbers</li>
                                    <li>At least one Upper case character</li>
                                    <li>At least one special character like #,!,@, &</li>
                                    <li>Password cannot same as your last five passwords</li>
                                    <li>Password will be reset every six months.</li>
                                </ul>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-4">
                            <div class="form-group">
                                <div class="style1" style="text-align: center;">
                                    &nbsp; &nbsp; &nbsp;
                                </div>
                            </div>
                        </div>
                        <div class="col-md-4">
                            <div class="form-group">
                                <div class="style1" style="text-align: center; margin-top: 60px;">
                                    <asp:Button ID="btn1" runat="server" Text="Submit" class="submit-button" Style="color: White;" /><br />
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    
    </form>
</body>
</html>
