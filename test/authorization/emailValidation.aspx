<%@ Page Language="VB" AutoEventWireup="false" CodeFile="emailValidation.aspx.vb"
    Inherits="emaiValidation" %>

<!DOCTYPE HTML>
<html>
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
            color: #FF3300;
        }
    </style>
</head>
<body>
    <!-- //end-smoth-scrolling -->
    <!--banner start here-->
    <div id="main" style="text-align: center; background: rgb(255, 255, 255) none repeat scroll 0% 0%;
        padding: 0px; box-shadow: 0 0px 0px #777">
        <!--main content start-->
        <div class="main-content" style="text-align: center;">
            <div class="emailvalidation" style="text-align: center;">
                <div class="container" style="text-align: center;">
                    <div style="background: #ffffff url('../images/registration-bg.jpg') no-repeat; width: 905px;
                        height: 577px; margin: 0 auto; padding: 20px">
                        <div class="activate">
                            <br />
                            <br />
                            <br />
                            <br />
                            <br />
                            <br />
                            <br />
                            <h2>
                                We've sent an email to
                                <asp:Label ID="lblemail" runat="server" ForeColor="#438cb7" Font-Underline="true"></asp:Label></h2>
                            <br />
                            <p style="font-size: 17px;">
                                Please check your Inbox for an email from techsupport@edictate.com.</p>
                            <p style="font-size: 17px;">
                                Click on Activate button in that mail to proceed further.</p>
                            <p style="font-size: 17px;">
                                The link is valid for 1 hour.
                            </p>
                        </div>
                        <div style="border: 5px solid Orange; width: 750px; margin: 55px; padding: 10px;
                            text-align: Left;">
                            <p class="style1">
                                <b>Did not get an email?</b></p>
                            <p>
                                <b>> Check SPAM folder in your email.</b></p>
                            <p>
                                <b>> Contact Support Desk at techsupport@edictate.com or Skype ID serversupportteam.</b></p>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <!--main content end--->
    </div>
</body>
</html>
