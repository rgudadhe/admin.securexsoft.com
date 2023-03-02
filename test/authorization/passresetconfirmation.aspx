<%@ Page Language="VB" AutoEventWireup="false" CodeFile="passresetconfirmation.aspx.vb"
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
<form runat="server">
    <!-- //end-smoth-scrolling -->
    <!--banner start here-->
    <div id="main" style="text-align: center; background: rgb(255, 255, 255) none repeat scroll 0% 0%;
        padding: 0px; box-shadow: 0 0px 0px #777">
        <!--main content start-->
        <div class="main-content" style="text-align:center;">
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
                          
                               <h2>Your Password has been reset.</h2>
                            <br />
                            <p  style=" font-size: 17px;">
                                Please click on the link below to login</p>
                            <div class="continue-btn">
                            <asp:Button ID="login" runat="server" Text="Login" class="submit-button" Style="color: White;" />
                            </div>
                           
                        </div>
                        
                    </div>
                </div>
            </div>
        </div>
        <!--main content end--->
    </div>
    </form>
</body>
</html>
