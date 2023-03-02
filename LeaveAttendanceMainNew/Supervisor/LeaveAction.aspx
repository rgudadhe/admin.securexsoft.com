<%@ Page Language="VB" AutoEventWireup="false" CodeFile="LeaveAction.aspx.vb" Inherits="LeaveAction" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 1.0 Transitional/EN">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Leave Aprrove/DisApprove</title>
    <link href= "../../App_Themes/Css/styles.css" type="text/css" rel="stylesheet" />
    <link href= "../../App_Themes/Css/Common.css" type="text/css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <center>
            <asp:RequiredFieldValidator  Display="None" ID="RequiredFieldValidator1" runat="server" ErrorMessage="Please Enter Reason for Leave DisApprove" ControlToValidate="txtReason" ></asp:RequiredFieldValidator>
        </center>
        <asp:Table ID="Table1" runat="server" HorizontalAlign="Center" >
            <asp:TableRow HorizontalAlign="Center" runat="server"  >
                <asp:TableCell ColumnSpan="2" HorizontalAlign="Center" runat="server" CssClass="HeaderDiv">Leave Disapprove Reason</asp:TableCell>
            </asp:TableRow>
            <asp:TableRow>
                <asp:TableCell CssClass="common">
                    Reason
                </asp:TableCell>
                <asp:TableCell>
                    <textarea id="txtReason" class="common" rows="10" cols="50" runat="server"></textarea>
                </asp:TableCell>
            </asp:TableRow>
            <asp:TableRow>
                <asp:TableCell HorizontalAlign="Center" ColumnSpan="2">
                    <asp:Button ID="btnDisApprove" runat="server" Text="DisApprove" CssClass="button" />
                    <br>
                    <FONT FACE="Arial" SIZE="2">                         
                        <a href="LeaveApproval.aspx">BACK</a>
                    </FONT>
                </asp:TableCell>
            </asp:TableRow>
        </asp:Table>
        <%--<asp:HiddenField ID="hdnSupervisorName" runat="server" />
        <asp:HiddenField ID="hdnSupervisorMail" runat="server" />
        <asp:HiddenField ID="hdnStartDate" runat="server" />
        <asp:HiddenField ID="hdnEndDate" runat="server" />
        <asp:HiddenField ID="hdnUserID" runat="server" />
        <asp:HiddenField ID="hdnUserMailID" runat="server" />--%>
    </div>
    <asp:ValidationSummary ID="ValidationSummary1" runat="server" HeaderText="Following error occurs:" ShowMessageBox="true" DisplayMode="BulletList" ShowSummary="False" /> </form>
</body>
</html>
