<%@ Page Language="VB" AutoEventWireup="false" CodeFile="RoutingTool.aspx.vb" Inherits="RoutingTool_Default" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 1.0 Transitional/EN">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
<link href= "../App_Themes/Css/styles.css" type="text/css" rel="stylesheet"/>
<link href= "../App_Themes/Css/Common.css" type="text/css" rel="stylesheet"/>
<link href= "../App_Themes/Css/Routing.css" type="text/css" rel="stylesheet"/>

    <title>View & Route Jobs</title>
      
   <script language="javascript" type="text/javascript"    >

function refresh()
{
    //  This version of the refresh function will be invoked
    //  for browsers that support JavaScript version 1.2
    //
    
    //  The argument to the location.reload function determines
    //  if the browser should retrieve the document from the
    //  web-server.  In our example all we need to do is cause
    //  the JavaScript block in the document body to be
    //  re-evaluated.  If we needed to pull the document from
    //  the web-server again (such as where the document contents
    //  change dynamically) we would pass the argument as 'true'.
    //  
    window.location.reload( false );
}

</script>

</head>
<body>
   <form id="form1" runat="server">
        <div id="body">
        <div id="cap"></div>
        <div id="main">
        <h1>View/Route Jobs</h1>
       <asp:Panel ID="Panel1" runat="server" HorizontalAlign="Left">
        <asp:Table ID="Table2"  runat="server"  Width="100%" CssClass="common">
        </asp:Table>
        <input id="Button1" type="button" class="button" value="Refresh Page" onclick="refresh();"/><br />
       </asp:Panel>
       </div> 
       </div> 
    </form>
</body>
</html>
