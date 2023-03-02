<%@ Page Language="VB" AutoEventWireup="false" CodeFile="EditIssueType.aspx.vb" Inherits="ERSSMain_EditIssueType" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 1.0 Transitional/EN">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Edit Issue Type</title>
    <link href= "../../App_Themes/Css/styles.css" type="text/css" rel="stylesheet" />
    <link href= "../../App_Themes/Css/Common.css" type="text/css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <center>
            <asp:RequiredFieldValidator  Display="None" ID="RequiredFieldValidator1" runat="server" ErrorMessage="Please enter issue name" ControlToValidate="txtIssueName" SetFocusOnError="true"></asp:RequiredFieldValidator> <BR>
<%--            <asp:RequiredFieldValidator  Display="None" ID="RequiredFieldValidator3" runat="server" ErrorMessage="Please select mode" ControlToValidate="DropDownAssignmentModeAdd" Font-Names="Trebuchet MS" SetFocusOnError=true Font-Bold=true Font-Italic=True Font-Size=Small></asp:RequiredFieldValidator> <BR>
            <asp:RequiredFieldValidator  Display="None" ID="RequiredFieldValidator4" runat="server" ErrorMessage="Please select copyto" ControlToValidate="DropDownCopyToAdd" Font-Names="Trebuchet MS" SetFocusOnError=true Font-Bold=true Font-Italic=True Font-Size=Small></asp:RequiredFieldValidator> <BR>
--%>            <asp:Table ID="tblMain" runat="server">
                <asp:TableRow>
                    <asp:TableCell ColumnSpan="2" HorizontalAlign="Center" CssClass="alt1">
                        Edit IssueType
                    </asp:TableCell>
                </asp:TableRow>
                <asp:TableRow>
                    <asp:TableCell CssClass="common">
                        Issue Name
                    </asp:TableCell>
                    <asp:TableCell HorizontalAlign="Left">
                        <asp:TextBox ID="txtIssueName" runat="server" CssClass="common" ></asp:TextBox>
                    </asp:TableCell>
                </asp:TableRow>
                <asp:TableRow>
                    <asp:TableCell VerticalAlign="Top" CssClass="common">
                        Issue Desc
                    </asp:TableCell>
                    <asp:TableCell >
                        <textarea id="txtIssueDesc" runat="server" rows="6" class="common" cols="30" ></textarea>
                    </asp:TableCell>
                </asp:TableRow>
<%--                <asp:TableRow>
                    <asp:TableCell HorizontalAlign=Left>
                        Mode
                    </asp:TableCell>
                    <asp:TableCell HorizontalAlign=Left>
                        <asp:DropDownList ID="DropDownAssignmentModeAdd" Font-Names="Trebuchet MS" runat="server" Width="90%">
                            <asp:ListItem Text="Please Select" Value=""></asp:ListItem>
                            <asp:ListItem Text="Issue Type-User Assignment" Value="Group"></asp:ListItem>
                            <asp:ListItem Text="Customer-User Assignment" Value="Team"></asp:ListItem>
                         </asp:DropDownList>
                    </asp:TableCell>
                </asp:TableRow>
                <asp:TableRow>
                    <asp:TableCell HorizontalAlign=Left>
                        Copy To
                    </asp:TableCell>
                    <asp:TableCell HorizontalAlign=Left>
                        <asp:DropDownList ID="DropDownCopyToAdd" Font-Names="Trebuchet MS" runat="server" Width="90%">
                            <asp:ListItem Text="Please select" Value=""></asp:ListItem>
                            <asp:ListItem Text="Yes" Value="True"></asp:ListItem>
                            <asp:ListItem Text="No" Value="False"></asp:ListItem>
                         </asp:DropDownList>
                    </asp:TableCell>
                </asp:TableRow>
--%>            </asp:Table><BR>
                <asp:Button ID="BtnSubmit" runat="server" CssClass="button" Text="Update"   />  &nbsp &nbsp &nbsp
                <asp:Button ID="BtnDelete" runat="server"  Text="Delete" CssClass="button"  />
        </center>
    </div>
    <asp:ValidationSummary ID="ValidationSummary1" runat="server" HeaderText="Following error occurs:" ShowMessageBox="true" DisplayMode="BulletList" ShowSummary="False" /> </form>
</body>
</html>
