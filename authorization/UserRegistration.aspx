<%@ Page Language="VB" AutoEventWireup="false" CodeFile="UserRegistration.aspx.vb"
    Inherits="email" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
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
    <style type="text/css">
        .style1
        {
            color: #999999;
        }
    </style>
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
    <form id="form1" runat="server">
    <asp:Panel ID="Panel1" runat="server">
     <div class="main-content">
        <div class="features">
            <div class="container">
                <div style="background: #ffffff url('../images/UserReg-bg.jpg') no-repeat; width: 905px;
                    height: 680px; margin: 0 auto; padding: 20px">
                    <div class="activate">
                        <br />
                        <h2>
                            Reset Password and Complete Registration</h2>
                    </div>
                    <div class="form2">
                        <div class="row">
                            <div class="col-md-4">
                                <div class="form-group">
                                    <label for="usr">
                                        First Name*</label>
                                    <asp:TextBox ID="txtfname" runat="server" ReadOnly="true" class="form-control"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-md-4">
                                <div class="form-group">
                                    <label for="usr">
                                        Last Name*</label>
                                    <asp:TextBox ID="txtlname" runat="server" ReadOnly="true" class="form-control"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-md-4">
                                <div class="form-group">
                                    <label for="usr">
                                        Email Address*</label>
                                    <asp:TextBox ID="txtemail" runat="server" ReadOnly="true" class="form-control"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-md-4">
                                <div class="form-group">
                                    <label for="usr">
                                        User Name</label>
                                    <asp:TextBox ID="txtuname" runat="server" ReadOnly="true" class="form-control"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-md-4">
                                <div class="form-group">
                                    <label for="usr">
                                        Password</label>
                                    <asp:TextBox ID="TxtNewPass" TextMode="Password" runat="server" class="form-control" /><br />
                                    <asp:RegularExpressionValidator Display="Dynamic" ID="RegularExpressionValidator2"
                                        ErrorMessage="Password does not meet requirements" ForeColor="Red" ValidationExpression="^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[$@$#$!%*?&])[A-Za-z\d$@$#$!%*?&]{8,}"
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
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ErrorMessage="Confirm Password is required "
                                        ControlToValidate="TxtCNewPass"></asp:RequiredFieldValidator><br />
                                    <asp:CompareValidator ID="CompareValidator1" ControlToValidate="TxtCNewPass" ControlToCompare="TxtNewPass"
                                        ValueToCompare="=" runat="server" ErrorMessage="Password does not match"></asp:CompareValidator>
                                </div>
                            </div>
                            <div class="col-md-6">
                                <div class="form-group">
                                    <asp:DropDownList ID="DropDownList1" runat="server" AutoPostBack="true" class="form-control">
                                        <asp:ListItem Text="Select Question 1" Selected="True"></asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:Label ID="lblq1" runat="server" Text="" Font-Size="Small"></asp:Label>
                                    <br />
                                    <asp:TextBox ID="txtans1" runat="server" class="form-control" /><br />
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Answer is required "
                                        ControlToValidate="txtans1"></asp:RequiredFieldValidator>
                                    <asp:DropDownList ID="DropDownList2" runat="server" AutoPostBack="true" class="form-control">
                                        <asp:ListItem Text="Select Question 2" Selected="True"></asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:Label ID="lblq2" runat="server" Text="" Font-Size="Small"></asp:Label>
                                    <br />
                                    <asp:TextBox ID="txtans2" runat="server" class="form-control" /><br />
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="Answer is required "
                                        ControlToValidate="txtans2"></asp:RequiredFieldValidator>
                                    <asp:DropDownList ID="DropDownList3" runat="server" AutoPostBack="true" class="form-control">
                                        <asp:ListItem Text="Select Question 3" Selected="True"></asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:Label ID="lblq3" runat="server" Text="" Font-Size="Small"></asp:Label>
                                    <br />
                                    <asp:TextBox ID="txtans3" runat="server" class="form-control" /><br />
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ErrorMessage="Answer is required "
                                        ControlToValidate="txtans3"></asp:RequiredFieldValidator>
                                </div>
                            </div>
                            <div class="col-md-6">
                                <div class="style1">
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
                            <div class="col-md-6">
                                <div class="style1" style="text-align: center;">
                                    <p style="height: 160px;">
                                    </p>
                                    <asp:Button ID="btn1" runat="server" Text="Submit" class="submit-button" Style="color: White;" /><br />
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    </asp:Panel>
    <asp:Panel ID="Panel2" runat="server" Visible="false"  >
     <asp:Label ID="Label1" runat="server" Text=""></asp:Label>
     <div class="activate;">
                        <br />
                        <br />
                        <br />
                        <br />
                        <h2 style="font-size:medium;">
                            Congratulations! <br /><br />Your password and Secret answers are updated successfully!</h2>
<br />
                        <br />
                        <br />
                        <br />
                        <br />
                        <br />
                        <br />
                        <br />
                        <br />
                        <br />
                        <br />
                        <br />
                        <br />
                        <br />
                        <br />
                        <br />
                        <br />
                        <br />
                        <br />
                        <br />
                    </div>
    </asp:Panel>
   
    </form>
    
</body>
</html>
