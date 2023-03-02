<%@ Page Language="VB" AutoEventWireup="false" CodeFile="ImportLeaveBalanceLog.aspx.vb" Inherits="ImportLeaveBalanceLog" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 1.0 Transitional/EN">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Import Leave Balance log</title>
    <link href= "../../App_Themes/Css/styles.css" type="text/css" rel="stylesheet" />
    <link href= "../../App_Themes/Css/Common.css" type="text/css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <center>
                <asp:Table ID="tblMain" runat="server" Width="524px">
                    <asp:TableRow  runat="server">
                        <asp:TableCell CssClass="HeaderDiv" runat="server">
                            Employee LeaveStatus Log Report
                        </asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow runat="server">
                        <asp:TableCell HorizontalAlign="Left" runat="server">
                            <asp:Label ID="Label1" runat="server" CssClass="common" Text="Select Month"></asp:Label> &nbsp
                            <asp:DropDownList ID="DropDownMonth" CssClass="common" runat="server" Width="25%">
                            </asp:DropDownList>&nbsp&nbsp
                            <asp:Label ID="Label2" runat="server" CssClass="common" Text="Select Year"></asp:Label>&nbsp
                            <asp:DropDownList ID="DropDownYear" CssClass="common" runat="server" Width="25%">
                            </asp:DropDownList>&nbsp
                            <asp:Button ID="btnSubmit" runat="server" Text="Submit" Font-Names="Trebuchet MS" CssClass="Buttons" />
                        </asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow runat="server">
                        <asp:TableCell HorizontalAlign="Left" runat="server" >
                            &nbsp &nbsp &nbsp &nbsp &nbsp &nbsp &nbsp &nbsp &nbsp &nbsp &nbsp &nbsp<asp:RequiredFieldValidator  Display="None" ID="RequiredFieldMonth" runat="server" SetFocusOnError="True" ErrorMessage="Please select month" ControlToValidate="DropDownMonth"></asp:RequiredFieldValidator>
                            &nbsp &nbsp &nbsp &nbsp &nbsp &nbsp &nbsp &nbsp &nbsp &nbsp &nbsp <asp:RequiredFieldValidator  Display="None" ID="RequiredFieldYear" runat="server" SetFocusOnError="True" ErrorMessage="Please select year" ControlToValidate="DropDownYear"></asp:RequiredFieldValidator>
                        </asp:TableCell>
                    </asp:TableRow>
                </asp:Table>
                <br/>
                <br/>
                    <asp:Table ID="tblResult" runat="server" Width="559px">
                        <asp:TableRow runat="server">
                            <asp:TableCell ColumnSpan="4" runat="server" CssClass="HeaderDiv" HorizontalAlign="Center">
                                Log History
                            </asp:TableCell>
                        </asp:TableRow>
                        <asp:TableRow runat="server">
                            <asp:TableCell runat="server" CssClass="alt1">
                                UserName
                            </asp:TableCell >
                            <asp:TableCell runat="server" CssClass="alt1">
                                Update On
                            </asp:TableCell>
                            <asp:TableCell runat="server" CssClass="alt1">
                                FileName
                            </asp:TableCell>
                            <asp:TableCell runat="server" CssClass="alt1">
                                Details
                            </asp:TableCell>
                        </asp:TableRow>
                    </asp:Table>
                    <BR><BR>
                    <asp:Label ID="Label3" runat="server" Text="Log not available." CssClass="Title" Visible="false"></asp:Label>
            </center>
		</div>
    <asp:ValidationSummary ID="ValidationSummary1" runat="server" HeaderText="Following error occurs:" ShowMessageBox="true" DisplayMode="BulletList" ShowSummary="False" /> </form>
</body>
</html>
