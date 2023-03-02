<%@ Page Language="VB" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 1.0 Transitional/EN">



<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
<script type="text/javascript"  language="JavaScript">

function poptastic()
{
    url="<%=System.Configuration.ConfigurationManager.AppSettings("URL")%>/relogin.aspx";
    //alert(inpt);
    
	newwindow=window.open(url,'name','height=200,width=400, left=300, top=100');
	if (window.focus) {newwindow.focus()}
}

</script>
    <title>Untitled Page</title>
</head>
<body  onload="poptastic()">
    <form id="form1" runat="server">
    <div>
     <span style="font-size: 10pt; color: gray; font-family: Trebuchet MS"><strong><em>
 

    Session expired, please login again. Please check that cookies are enabled in your web browser.
        <br />
        This error usually means one of three things:
        <br />
        <br />
        1. Your session was idle for 10 minutes. In which case, log in again.
        <br />
        <br />
        2. Your web browser (Netscape, Internet Explorer, Firefox, etc.) is blocking session
        cookies from https://admin.securexsoft.com. SecureXSoft uses "Cookies" to keep track of
        your session. If cookies are blocked, SecureXSoft&nbsp; does not know who you are,
        and so logs out. Too enable cookies check your browsers options and/or "cookie manager,"
        or visit the helpdesk.
        <br />
        <br />
        3. Finally, this error could also be caused by a web proxy - if you are behind such
        a proxy, you must either disable use of the proxy or ask your proxy administrator
        to allow https://admin.securexsoft.com to send you cookies. </em></strong></span>
    </div>
    </form>
</body>
</html>
