<%@ Page Language="VB" AutoEventWireup="false" CodeFile="DeptDesnDet.aspx.vb" Inherits="Department_Default" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 1.0 Transitional/EN">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Untitled Page</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:Label ID="MsgDisp" runat="server" Font-Bold="True" Font-Names="Trebuchet MS" Font-Size="Medium"
            ForeColor="#400000" Height="16px" Width="496px"></asp:Label>
        <table style="text-align: center; background-color: whitesmoke; border-right: wheat thin solid; border-top: wheat thin solid; border-left: wheat thin solid; border-bottom: wheat thin solid;" width="100%">
            <tr>
                <td colspan="4" style="text-align: left; background-color: gainsboro;">
                    <span style="font-size: 0.8em; font-family: Trebuchet MS"><strong style="font-family: Trebuchet MS, Serif">Department Details</strong></span></td>
            </tr>
            <tr>
                <td style="text-align: right">
                    <span style="font-size: 10pt; font-family: 'Trebuchet MS', Cursive; position: static;">*Department
                        Name</span></td>
                <td style="text-align: left;">
                    <asp:TextBox ID="TxtDeptName" runat="server"></asp:TextBox>
                    </td>
            </tr>
            <tr>
                <td style="text-align: right">
                    <span style="font-size: 10pt; font-family: Trebuchet MS">Description</span></td>
                <td style="text-align: left;">
                    <asp:TextBox ID="TxtDeptDesc" runat="server"></asp:TextBox></td>
            </tr>
            <tr>
                <td style="text-align: center; " colspan="4">
                    <asp:Button ID="Button1" runat="server" BackColor="SaddleBrown" BorderStyle="Double"
                        ForeColor="Beige" Text="Submit" />
                    <asp:Button ID="Button2" runat="server" BackColor="SaddleBrown" BorderStyle="Double"
                        ForeColor="Beige" Text="Delete" />
                    <asp:Button ID="Button3" runat="server" BackColor="SaddleBrown" BorderStyle="Double"
                        ForeColor="Beige" Text="Close Window" OnClientClick="window.close();window.opener.location.reload();"  />
                    <asp:HiddenField ID="DeptID" runat="server" />
                </td>
            </tr>
        </table>
        <br />
        <br />
        <br />
        <br />
        
    <asp:RequiredFieldValidator id="valRequired" runat="server" ControlToValidate="TxtDeptName"
                    ErrorMessage="You must enter a value into Department Name" Font-Bold="True" Font-Italic="True" Font-Names="Trebuchet MS" Font-Size="Small">
                    
</asp:RequiredFieldValidator>
    </div>
    </form>
</body>
</html>
