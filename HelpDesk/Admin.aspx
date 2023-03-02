<%@ Page Language="VB" AutoEventWireup="false" CodeFile="Admin.aspx.vb" Inherits="HelpDesk_Admin" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>HelpDesk Admin</title>
    <script type="text/javascript" language="javascript">
        function Load()
        {
            var displayWindow;
            displayWindow = window.open('', "HelpDesk");
            document.form1.target = "HelpDesk";
            //document.form1.action='https://sxf1.securexsoft.com/customerTroubleTicket/login.aspx'
            document.form1.action='https://helpdesk.securexsoft.com/LoginCS.aspx';
            document.form1.submit();
  	        document.form1.target = "_self";
  	        document.form1.action="Admin.aspx"; 
            return false;
        }
    </script>
</head>
<body onload="javascript:Load();">
    <form id="form1" runat="server">
    <div>
    <asp:HiddenField ID="hdnUserId" runat="server" />
    <asp:HiddenField ID="hdnFrom" runat="server" />
    </div>
    </form>
</body>
</html>
