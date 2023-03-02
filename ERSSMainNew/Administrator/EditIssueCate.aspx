<%@ Page Language="VB" AutoEventWireup="false" CodeFile="EditIssueCate.aspx.vb" Inherits="ERSSMain_EditIssueCate" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 1.0 Transitional/EN">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Edit Issue Category</title>
    <link href= "../../App_Themes/Css/styles.css" type="text/css" rel="stylesheet" />
    <link href= "../../App_Themes/Css/Common.css" type="text/css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <center>
        <asp:RequiredFieldValidator  Display="None" ID="RequiredFieldtxtCateName" runat="server" ErrorMessage="Please enter cate name " SetFocusOnError="true" ControlToValidate="txtCateName"></asp:RequiredFieldValidator>
        <BR><asp:RequiredFieldValidator  Display="None" ID="RequiredFieldCateDesc" runat="server" ErrorMessage="Please enter cate description " SetFocusOnError="true" ControlToValidate="txtCateDesc"></asp:RequiredFieldValidator>
        <asp:Table ID="tblMain" runat="server">
            <asp:TableRow>
                <asp:TableCell ColumnSpan=2 HorizontalAlign="Center" CssClass="alt1">
                    Edit Category
                </asp:TableCell>
            </asp:TableRow>
            <asp:TableRow>
                <asp:TableCell CssClass="common">
                    Cate Name
                </asp:TableCell>
                <asp:TableCell>
                    <asp:TextBox ID="txtCateName" runat="server" CssClass="common"></asp:TextBox>
                </asp:TableCell>
            </asp:TableRow>
            <asp:TableRow>
                <asp:TableCell CssClass="common">
                    Cate Description
                </asp:TableCell>
                <asp:TableCell>
                    <asp:TextBox ID="txtCateDesc" runat="server" CssClass="common"></asp:TextBox>
                </asp:TableCell>
            </asp:TableRow>
        </asp:Table>
        <BR>
        <BR>
            <asp:Button ID="BtnSubmit" runat="server" Text="Update" CssClass="button" />
            <asp:Button ID="BtnDelete" runat="server" Text="Delete" CssClass="button" />
        </center>
    </div>
    <asp:ValidationSummary ID="ValidationSummary1" runat="server" HeaderText="Following error occurs:" ShowMessageBox="true" DisplayMode="BulletList" ShowSummary="False" /> </form>
</body>
</html>
