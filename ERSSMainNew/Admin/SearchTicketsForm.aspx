<%@ Page Language="VB" AutoEventWireup="false" CodeFile="SearchTicketsForm.aspx.vb" Inherits="SearchTicketsForm" %>
<%@ Register Assembly="System.Web.Extensions, Version=1.0.61025.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI" TagPrefix="asp" %>
<%@ Register
    Assembly="AjaxControlToolkit"
    Namespace="AjaxControlToolkit"
    TagPrefix="ajaxToolkit" %>



<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 1.0 Transitional/EN">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Untitled Page</title>
    <script language=javascript type="text/javascript">
        function Remove(str)
        {
            if (str.value !='') 
            {
                if (str.options[0].value == '')
                    str.removeChild(str.options[0]);
            }
        }
    </script>
    <LINK href= "../../Styles/Default.css" type="text/css" rel="stylesheet">
    <LINK href= "../../Styles/Report.css" type="text/css" rel="stylesheet">
</head>
<body>

    <form id="form1" action="SearchTicketResults.aspx" method="post" target="bottom" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
    <div>
        <asp:Table ID="tblMain" runat="server" Width=100%>
            <asp:TableRow CssClass="HeaderDiv">
                <asp:TableCell HorizontalAlign=Center>
                    Search Tickets
                </asp:TableCell>
            </asp:TableRow>
        </asp:Table>
        <asp:Table ID="tblMainSearch" HorizontalAlign=Center runat="server" GridLines=Both Width=85%>
            <asp:TableRow Font-Names="Trebuchet MS" Font-Size=Smaller CssClass="SMSelected" >
                <asp:TableCell HorizontalAlign=Center Width="30%">
                    Start Date
                </asp:TableCell>
                <asp:TableCell HorizontalAlign=Center Width="30%">
                    End Date
                </asp:TableCell>
                <asp:TableCell HorizontalAlign=Center>
                    Status
                </asp:TableCell>
            </asp:TableRow>
            <asp:TableRow Font-Names="Trebuchet MS" Font-Size=Smaller >
                <asp:TableCell Width="30%">
                    <asp:TextBox ID="txtStartDate" Font-Names="Trebuchet MS" runat="server"></asp:TextBox>
                    <asp:ImageButton ID="imgSDate"  ImageUrl="~/images/Calendar.gif" runat="server"/>
                    <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" Format="MM/dd/yyyy" TargetControlID="txtStartDate" PopupButtonID="imgSDate">
                    </ajaxToolkit:CalendarExtender>
                </asp:TableCell>
                <asp:TableCell Width="30%">
                    <asp:TextBox ID="txtEndDate" Font-Names="Trebuchet MS" runat="server"></asp:TextBox>
                    <asp:ImageButton ID="imgEDate" ImageUrl="~/images/Calendar.gif" runat="server" />
                    <ajaxToolkit:CalendarExtender ID="CalendarExtender2" runat="server" Format="MM/dd/yyyy" TargetControlID="txtEndDate" PopupButtonID="imgEDate">
                    </ajaxToolkit:CalendarExtender>
                </asp:TableCell>
                <asp:TableCell>
                    <asp:DropDownList ID="DropDownStatus" Font-Names="Trebuchet MS" Font-Size=Small runat="server" Width=98%>
                        <asp:ListItem Text="Select Status" Value=""></asp:ListItem>
                        <asp:ListItem Text="Open" Value="Open"></asp:ListItem>
                        <asp:ListItem Text="Close" Value="Close"></asp:ListItem>
                    </asp:DropDownList>
                </asp:TableCell>
            </asp:TableRow>
            <asp:TableRow Font-Names="Trebuchet MS" Font-Size=Smaller CssClass="SMSelected">
                <asp:TableCell HorizontalAlign=Center>
                    Ticket No.
                </asp:TableCell>
                <asp:TableCell HorizontalAlign=Center>
                    User ID
                </asp:TableCell>
                <asp:TableCell HorizontalAlign=Center>
                    Issue Type
                </asp:TableCell>
            </asp:TableRow>
            <asp:TableRow Font-Names="Trebuchet MS" Font-Size=Smaller>
                <asp:TableCell>
                    <asp:TextBox ID="txtTicketNo" Font-Names="Trebuchet MS" runat="server" Width=98%></asp:TextBox>
                </asp:TableCell>
                <asp:TableCell>
                    <asp:TextBox ID="txtUserID" runat="server" Font-Names="Trebuchet MS" Width=98%></asp:TextBox>
                </asp:TableCell>
                <asp:TableCell>
                    <asp:DropDownList ID="DropDownIssueTypes" Font-Names="Trebuchet MS" Font-Size=small runat="server" Width=98%>
                    </asp:DropDownList>
                </asp:TableCell>
            </asp:TableRow>
        </asp:Table>
        <center>
            <asp:RegularExpressionValidator  Display="None" ID="RegularExpressiontxtTicketNo" ErrorMessage="Ticket No Accept only numbers"  runat="server" Font-Names="Trebuchet MS" SetFocusOnError=true Font-Size=Small Font-Bold=true  Font-Italic=true ControlToValidate="txtTicketNo" ValidationExpression="[1-9]{1}[0-9]*"></asp:RegularExpressionValidator>
        </center>
        <center>
            <input id="btnSubmit" type="submit" runat=server value="submit" style="font-family:Trebuchet MS" class="Buttons" />
        </center>
        <asp:ValidationSummary ID="ValidationSummary1" runat="server" HeaderText="Following error occurs:" ShowMessageBox="true" DisplayMode="BulletList" ShowSummary="False" /> 
    </div>
    </form>
</body>
</html>
