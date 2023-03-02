<%@ Page Language="VB" AutoEventWireup="false" CodeFile="ActionTicket.aspx.vb" Inherits="ActionTicket" %>

<%@ Register Assembly="System.Web.Extensions, Version=1.0.61025.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI" TagPrefix="asp" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 1.0 Transitional/EN">
<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Ticket Action</title>
    <link href= "../../App_Themes/Css/styles.css" type="text/css" rel="stylesheet" />
    <link href= "../../App_Themes/Css/Common.css" type="text/css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
    <div>
        <div style="text-align:left">
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:LinkButton ID="LnkBtn" CausesValidation="false" CssClass="common" runat="server">Return to list</asp:LinkButton></div>
    <center>
        <asp:Table ID="Table1" runat="server" Width="90%">
            <asp:TableRow  runat="server">
                <asp:TableCell HorizontalAlign="Center" runat="server" ColumnSpan="2" CssClass="HeaderDiv">
                    Ticket Details
                </asp:TableCell>
            </asp:TableRow>
            <asp:TableRow>
                <asp:TableCell HorizontalAlign="Left" Width="50%">
                    <asp:Label ID="Lab" runat="server" CssClass="common" Text="Customer Name : "></asp:Label>
                    <asp:Label ID="lblCustName" runat="server" CssClass="common" ></asp:Label>
                </asp:TableCell>
                <asp:TableCell HorizontalAlign="Left" Width="50%">
                    <asp:Label ID="Label3" runat="server" CssClass="common" Text="UserName : "></asp:Label>
                    <asp:Label ID="lblUserName" runat="server" CssClass="common" ></asp:Label>
                </asp:TableCell>
            </asp:TableRow>  
            <asp:TableRow>
                <asp:TableCell HorizontalAlign="Left" Width="50%">
                    <asp:Label ID="Label5" runat="server" CssClass="common" Text="Ticket No : "></asp:Label>
                    <asp:Label ID="lblTicketNo" runat="server" CssClass="common" Text=<%#Eval("TicketNo") %>></asp:Label>
                </asp:TableCell>
                <asp:TableCell HorizontalAlign="Left" Width="50%">
                    <asp:Label ID="Label7" CssClass="common" runat="server" Text="Issue Type : "></asp:Label>
                    <%--<asp:Label ID="lblIssueName" Font-Names="Trebuchet MS" ForeColor=GRAY runat="server" Text=<%#Eval("IssueName") %>></asp:Label>--%>
                    <asp:DropDownList ID="ddlIssueType" runat="server" CssClass="common">
                    </asp:DropDownList>
                </asp:TableCell>
            </asp:TableRow>
            <asp:TableRow>
                <asp:TableCell HorizontalAlign="Left" Width="50%">
                    <asp:Label ID="Label9" CssClass="common" runat="server" Text="Date Created : "></asp:Label>
                    <asp:Label ID="lblDatePosted" CssClass="common" runat="server"></asp:Label>
                </asp:TableCell>
                <asp:TableCell HorizontalAlign="Left" Width="50%">
                    <asp:Label ID="Label11" CssClass="common" runat="server" Text="Priority : "></asp:Label>
                    <asp:Label ID="lblPriority" CssClass="common" runat="server"></asp:Label>
                </asp:TableCell>
            </asp:TableRow>
            <asp:TableRow>
                <asp:TableCell ColumnSpan="2" HorizontalAlign="Left" CssClass="alt2">
                    <asp:Label ID="lblIssueDetails" CssClass="common" runat="server" Text="Issue Details : "></asp:Label>
                </asp:TableCell>
            </asp:TableRow>
            <asp:TableRow>
                <asp:TableCell ID="tblCellIssueDetails" HorizontalAlign="Left" ColumnSpan="2">
                </asp:TableCell>
            </asp:TableRow>            
        </asp:Table>
        <asp:Table ID="tblResponses" runat="server" Width="90%" Visible="false">
            <asp:TableRow>
                <asp:TableCell HorizontalAlign="Left" CssClass="alt2">
                    Response History :
                </asp:TableCell>
            </asp:TableRow>
        </asp:Table>
        <asp:Table ID="tblAction" runat="server" Width="90%">
            <asp:TableRow>
                <asp:TableCell ColumnSpan="2" CssClass="alt2">
                    Action Details :
                </asp:TableCell>
            </asp:TableRow>
            <asp:TableRow>
                <asp:TableCell ColumnSpan="2" HorizontalAlign="Left">
                   <textarea id="txtActionDetails" name="txtActionDetails"  style="width:97%" rows="4" runat="server" class="common"  ></textarea> 
                </asp:TableCell>
            </asp:TableRow>
            <asp:TableRow>
                <asp:TableCell Width="20%" HorizontalAlign="Left">
                    <asp:Label ID="lblStatus" runat="server" CssClass="common" Text="Status : "></asp:Label>
                </asp:TableCell>
                <asp:TableCell HorizontalAlign="Left">
                    <asp:DropDownList ID="DropDownStatus" CssClass="common" runat="server" Width="40%">
                        <asp:ListItem Text="Open" Value="Open"></asp:ListItem>
                        <asp:ListItem Text="Close" Value="Close"></asp:ListItem>
                    </asp:DropDownList>
                </asp:TableCell>
            </asp:TableRow>
            <asp:TableRow>
                <asp:TableCell Width="20%" HorizontalAlign="Left">
                    <asp:Label ID="lblForward" CssClass="common" runat="server" Text="Forward To : "></asp:Label>
                </asp:TableCell>
                <asp:TableCell HorizontalAlign="Left">
                    <asp:DropDownList ID="DropDownForward" AutoPostBack="true" CssClass="common" runat="server" Width="40%">
                    </asp:DropDownList>
                </asp:TableCell>
            </asp:TableRow>
            <asp:TableRow>
                <asp:TableCell Width="20%" HorizontalAlign="Left">
                    <asp:Label ID="lblUserForward" runat="server" CssClass="common" Text="UserID :"></asp:Label>
                </asp:TableCell>
                <asp:TableCell HorizontalAlign="Left" runat="server">
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server" RenderMode="Inline">
                        <ContentTemplate>
                            <asp:DropDownList ID="DropDownList2" runat="server" Width="40%" CssClass="common">
                            </asp:DropDownList>&nbsp;<br />
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="DropDownForward" EventName="SelectedIndexChanged" />
                        </Triggers>
                    </asp:UpdatePanel>
                </asp:TableCell>
            </asp:TableRow>
        </asp:Table>
        <BR>
        <asp:RequiredFieldValidator  Display="None" ID="RequiredFieldActionDetails" SetFocusOnError="true" runat="server" ErrorMessage="Please enter Action Details" ControlToValidate="txtActionDetails" ></asp:RequiredFieldValidator>
      </center>
        <center>
        <BR>
            <asp:Button ID="BtnSubmit" runat="server" Text="Submit" CssClass="button" />
        </center>
        <asp:HiddenField ID="hdnIssueID" runat="server" />
        <asp:HiddenField ID="hdnDeptID" runat="server" />
        <asp:HiddenField ID="hdnAssignedID" runat="server" />
    </div>
    <asp:ValidationSummary ID="ValidationSummary1" runat="server" HeaderText="Following error occurs:" ShowMessageBox="true" DisplayMode="BulletList" ShowSummary="False" /> </form>
</body>
</html>
