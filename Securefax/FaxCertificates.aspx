<%@ Page Language="VB" AutoEventWireup="false" CodeFile="FaxCertificates.aspx.vb" Inherits="Services_FaxCertificates" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 1.0 Transitional/EN">
<script language="javascript" type="text/javascript">
    function Chk()
    {
        if (document.getElementById('DropDownAccount').value=='')
        {
            alert('Please select account')
            return false;
        }
        return true;
    }
</script>
<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Fax Certificates</title>
    <link href= "../styles/Default.css" type="text/css" rel="stylesheet"/>    
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <%--<a href="../LeaveAttendanceMainNew/CloseWindow.aspx"></a>--%>
        <table align="center" border=1>
            <tr>
                <td style=" font-family:Trebuchet MS; font-size:12px;" align="center">
                    Account Name 
                    <asp:DropDownList ID="DropDownAccount" runat="server" Font-Names="Trebuchet MS" Font-Size="12px" Width=300px>
                    </asp:DropDownList>
                </td>
                <td>
                    <asp:Button ID="btnSubmit" runat="server" Text="Submit" Font-Names="Trebuchet MS" Font-Size="12px" OnClientClick="javascript:return Chk();"  />
                </td>
            </tr>
        </table><hr />
        <asp:Table ID="tblUsers" runat="server" GridLines="Both" Visible="true" HorizontalAlign="Center" Width="70%" Font-Names="Trebuchet Ms" Font-Size="12px">
            <asp:TableRow CssClass="SMSelected">
                <asp:TableCell HorizontalAlign="Center">
                    UserName
                </asp:TableCell>
                <asp:TableCell HorizontalAlign="Center">
                    Last updated 
                </asp:TableCell>
                <asp:TableCell HorizontalAlign="Center">
                    Action
                </asp:TableCell>
            </asp:TableRow>
        </asp:Table>
    </div>
    </form>
</body>
</html>
