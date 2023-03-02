<%@ Page Language="VB" AutoEventWireup="false" CodeFile="sxf1.aspx.vb" Inherits="Navigation_sxf1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title></title>
    <script type="text/javascript" language="javascript">
        function GetData()
        {
            var displayWindow;
            displayWindow = window.open('', "newWin4");
            document.form1.target = "newWin4";
            document.form1.action = 'https://sxf1.securexsoft.com/Navigation/NavigationInstance.aspx'
            document.form1.submit();
  	        document.form1.target = "_self";
  	        document.form1.action="sxf1.aspx"; 
            return false;
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:HiddenField ID="hdnUserID" runat="server" />
    </div>
    </form>
</body>
</html>
