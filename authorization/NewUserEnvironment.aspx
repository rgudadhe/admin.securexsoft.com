 <%@ Page Language="VB" AutoEventWireup="false" CodeFile="NewUserEnvironment.aspx.vb" Inherits="authorization_NewUserEnvironment" %>

<!DOCTYPE html>

<html lang="en">
<head>
    <meta charset="utf-8">
    <meta http-equiv="X-UA-Compatible" content="IE=edge">
    <meta name="viewport" content="width=device-width, shrink-to-fit=no, initial-scale=1">
    <meta name="description" content="">
    <meta name="author" content="">
    <link rel="shortcut icon" href="/img/Logo.jpg" type="image/x-icon" />
    <title>SecureXSoft</title>

    <!-- Bootstrap Core CSS -->
    <link href="../rpage_css_plugins/bootstrap/css/bootstrap.min.css" rel="stylesheet">
    <!-- google font CSS -->
    <link href="https://fonts.googleapis.com/css?family=Lato:300,400,700" rel="stylesheet">
    <!-- FontAwesome CSS -->
    <link href="../rpage_css_plugins/font-awesome/css/font-awesome.css" rel="stylesheet">
    <!-- Bootstrap Material Design -->
    <link href="../rpage_css_plugins/bootstrap-material-design/css/bootstrap-material-design.css" rel="stylesheet">
    <link href="../rpage_css_plugins/bootstrap-material-design/css/ripples.min.css" rel="stylesheet">

    <!-- Material Design fonts -->
    <link rel="stylesheet" href="https://fonts.googleapis.com/css?family=Roboto:300,400,500,700" type="text/css">
    <link href="https://fonts.googleapis.com/icon?family=Material+Icons" rel="stylesheet">
    <!-- Checkbox Radio Button CSS -->
    <link rel="stylesheet" href="../rpage_css_plugins/Uniform-3.0/dist/css/uniform.default.css" type="text/css" />
    <!-- Mat CSS -->
    <link href="../rpage_css_plugins/css/mdl-log.css" rel="stylesheet">
    <!-- Custom CSS -->
    <link href="../rpage_css_plugins/css/style.css" rel="stylesheet">
    <!-- Responsive CSS -->
    <link href="../rpage_css_plugins/css/responsive.css" rel="stylesheet">
    <!-- HTML5 Shim and Respond.js IE8 support of HTML5 elements and media queries -->
    <!-- WARNING: Respond.js doesn't work if you view the page via file:// -->
    <!--[if lt IE 9]>
        <script src="https://oss.maxcdn.com/libs/html5shiv/3.7.0/html5shiv.js"></script>
        <script src="https://oss.maxcdn.com/libs/respond.js/1.4.2/respond.min.js"></script>
    <![endif]-->
    <style>
        @font-face {
            font-family: 'Material Icons';
            font-style: normal;
            font-weight: 400;
            src: url("../font/material-icons/MaterialIcons-Regular.eot"); /* For IE6-8 */
            src: local('Material Icons'), local('MaterialIcons-Regular'), url("../font/material-icons/MaterialIcons-Regular.woff2") format('woff2'), url("../font/material-icons/MaterialIcons-Regular.woff") format('woff'), url("../font/material-icons/MaterialIcons-Regular.ttf") format('truetype');
        }

        .material-icons {
            font-family: 'Material Icons';
            font-weight: normal;
            font-style: normal;
            font-size: 24px; /* Preferred icon size */
            display: inline-block;
            line-height: 1;
            text-transform: none;
            letter-spacing: normal;
            word-wrap: normal;
            white-space: nowrap;
            direction: ltr;
            /* Support for all WebKit browsers. */
            -webkit-font-smoothing: antialiased;
            /* Support for Safari and Chrome. */
            text-rendering: optimizeLegibility;
            /* Support for Firefox. */
            -moz-osx-font-smoothing: grayscale;
            /* Support for IE. */
            font-feature-settings: 'liga';
        }
    </style>
</head>
<body class="login">
    <div class="container-fluid">
        <div class="container">
            <div class="well">
                <div class="log-header">
                    <a href="#">
                        <img src="../Images/secureXFlow_01.png" alt="SecureXFlow" class="img-responsive"></a>
                </div>
                <form id="form1" class="bs-component" runat="server">
                    <label> <span>Enter the code sent on your email address</span></label>
                    <br />
                    <div class="group">
                       
                    </div>
                    <div class="group">
                        <%--<input required="" type="text">--%>
                        <asp:TextBox ID="txtemail" runat="server" onkeypress="return false;"></asp:TextBox>
                        <span class="highlight" ></span>
                        <span class="bar"></span>
                        <label><i class="material-icons">person</i> <span>Email Address</span></label>

                    </div>
                     
                    <div class="group">
                        <%--<input required="" type="password">--%>
                        <asp:TextBox ID="txtactode" runat="server"></asp:TextBox>
                        <span class="highlight"></span>
                        <span class="bar"></span>
                        <label><i class="material-icons">lock</i> <span>Authentication Code</span></label>

                    </div>

                    <div class="login-btn-con">
                        <div class="row">
                             <div class="col-xs-12 text-center">
                                   <asp:CheckBox ID="CheckBox1" runat="server" Text=" &nbsp; I sign in frequently from this device. Don't ask me for a code." />
                               
                                 </div>
                            <div class="col-xs-12 text-center">
                           
                              <br />
                                <asp:Button ID="btnSubmit" class="mdl-button btn" runat="server" Text="Submit" />
                                <%--<button class="mdl-button btn" >Login</button>--%>
                            </div>
                            <br />
                            <div class="col-xs-12 text-center">
                                <asp:LinkButton ID="btnresendotp" runat="server">Resend Activation Code</asp:LinkButton>&nbsp;<%--| Not Received Activation Code? <asp:LinkButton ID="btnmobileotp" runat="server">Click here</asp:LinkButton> <br />--%>
                              
                                <br />
                                <asp:Label ID="Label2" runat="server" Text=""></asp:Label>
                            </div>
                        </div>
                    </div>
                </form>
            </div>
        </div>
    </div>
    <!--Footer Start-->
    <footer>
        <div class="container">
            <div class="text-center">&copy; Copyright 2018 | MedOfficePro, Inc | All Rights Reserved | Version 1.250 </div>
        </div>
    </footer>
    <!--Footer End-->

    <!-- jQuery -->
    <script src="js/jquery.min.js"></script>
    <!-- Bootstrap JS -->
    <script src="plugins/bootstrap/js/bootstrap.min.js"></script>
    <!-- Checkbox - Radio Button JS -->
    <script src="plugins/Uniform-3.0/src/js/jquery.uniform.js" type="text/javascript"></script>
    <!-- Material Design - JS -->
    <script src="plugins/bootstrap-material-design/js/ripples.min.js"></script>
    <script src="plugins/bootstrap-material-design/js/material.min.js"></script>

    <script>
        $(function () {
            $.material.init();
        });
    </script>

   

</body>
</html>
