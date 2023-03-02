<%@ Page Language="VB" AutoEventWireup="false" CodeFile="RoutingToolNew.aspx.vb" Inherits="RoutingTool_RoutingToolNew" EnableViewStateMac="false" Debug="true" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Routing Tool</title>
    <%--<link href= "../App_Themes/Css/styles.css" type="text/css" rel="stylesheet"/>--%>
    <link href= "../App_Themes/Css/Common.css" type="text/css" rel="stylesheet"/>
    <link href= "../App_Themes/Css/Default.css" type="text/css" rel="stylesheet"/>
    <link href= "../App_Themes/Css/Routing.css" type="text/css" rel="stylesheet"/>
    <script type="text/javascript" language="javascript">
        function ChkLevel()
        {
            if (document.getElementById('ddlLevels').value=='')
            {
                alert('Please select level');
                return false;
            }
            return true;
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div id="body">
        <div id="cap"></div>
        <div id="main">
        <h1>View/Route Jobs</h1>
        <div style="text-align:left">
            <%--<table  border="0">
                <tr>
                    <td style="border:0">
                        Select Level 
                    </td>
                    <td style="border:0">
                        <asp:DropDownList ID="ddlLevels" runat="server" CssClass="common" Width="200">
                        </asp:DropDownList>
                    </td>
                    <td style="border:0">
                        <asp:Button ID="btnSubmit" runat="server" Text="Submit" CssClass="button" OnClientClick="javascript:return ChkLevel();" />                    
                    </td>
                </tr>
            </table>
            <br />--%>
            <table id="tblMain" runat="server" border="1" style="font-family:Trebuchet MS; font-size:10pt;" width="50%">
                <tr style=" text-align:center; font-weight:bold ; color:White;background-image:url(../App_Themes/Images/background_parentselected.gif)">
                    <td>
                        Level Name
                    </td>
                    <td>
                        Pending Mins
                    </td>
                    <td>
                        Assigned Mins
                    </td>
                </tr>
            </table>
        </div>
       <asp:Panel ID="Panel1" runat="server" HorizontalAlign="Left">
        <asp:Table ID="Table2"  runat="server"  Width="100%" CssClass="common">
        </asp:Table>
       </asp:Panel>
       </div> 
       </div> 
    </form>
</body>
</html>
