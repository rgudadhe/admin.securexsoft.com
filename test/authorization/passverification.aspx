<%@ Page Language="VB" AutoEventWireup="false" CodeFile="passverification.aspx.vb" Inherits="Passwordmodule_passverification" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <%--<link rel='stylesheet' type='text/css' href="../App_Themes/Css/StylesMain.css" />
    <script language="javascript" src="../App_Themes/JS/ToolTip.js" type="text/javascript"></script>--%>
    <style type="text/css">
input[type=text]
 {
    width: 100%;
    padding: 10px 20px;
    margin: 8px 0;
   
}

input[type=button], input[type=submit], input[type=reset] {
    background-color:#FF4500;
    border: none;
    color: white;
    padding: 16px 32px;
    text-decoration: none;
    margin: 4px 2px;
    cursor: pointer;
}

body
{
    font-family:Arial;
    font-size:small;
   
}
</style>
</head>
<body>
    <div>
        <form id="signup" runat="server" >
              
            <table style="margin:0 auto">
                <tr>
                    <td>
                        <h2>
                            Answer your Secret Questions</h2>
                    </td>
                </tr>
                <tr>
                    <td>

                        <asp:Label ID="lblq1" runat="server" Text=""></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td >
                        <asp:TextBox ID="txtans1" runat="server" Width="300" /><br />
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Answer is required "
                            ControlToValidate="txtans1"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td class="DEMO5">
                        <asp:Label ID="lblq2" runat="server" Text=""></asp:Label>
                    </td>
                </tr>
                <tr> 
                    <td class="DEMO5">
                        <asp:TextBox ID="txtans2" runat="server" Width="300" /><br />
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="Answer is required "
                            ControlToValidate="txtans2"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                                <tr>
                    <td class="DEMO5">
                        <asp:Label ID="lblq3" runat="server" Text=""></asp:Label>
                    </td>
                </tr>
                <tr> 
                    <td class="DEMO5">
                        <asp:TextBox ID="txtans3" runat="server" Width="300" /><br />
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="Answer is required "
                            ControlToValidate="txtans2"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td valign="middle" class="button"> 
                    <asp:Button ID="submit" Text="Submit" runat="server" /><br />
                    <asp:Label ID="Label2" runat="server" CssClass="Title1" ForeColor="#009933"></asp:Label>                         
                    </td>

                </tr>
                
            </table>
       
<%--        <asp:ValidationSummary ID="ValidationSummary1" runat="server" HeaderText="Following error occurs:"
            ShowMessageBox="true" DisplayMode="BulletList" ShowSummary="False" />--%>
        </form>
    </div>
</body>
</html>