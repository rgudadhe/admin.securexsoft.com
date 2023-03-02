<%@ Page Language="VB" AutoEventWireup="false" CodeFile="DicWise.aspx.vb" Inherits="DicWise" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Untitled Page</title>
    <link href="../../App_Themes/Css/styles.css" type="text/css" rel="stylesheet" />
    <link href="../../App_Themes/Css/Common.css" type="text/css" rel="stylesheet" />
    <style type="text/css">
        input.button
        {
            font: bold 8pt Arial, Sans-serif;
            height: 24px;
            margin: 0;
            padding: 2px 3px;
            color: #ffffff;
            background: #E56717;
            border: 1px solid #dadada;
        }
    </style>
    <style type="text/css">
        tr.tblbg
        {
            background: #e7e6e6 url(../images/tbbg.jpg) repeat;
        }
        tr.tblbg1
        {
            background: #e7e6e6 url(../images/tbbg.jpg) repeat;
        }
        tr.tblbg2
        {
            background: #e7e6e6 url(../images/tbbg2.jpg) repeat;
            padding-left: 8px;
            padding-right: 8px;
            text-align: center;
            border-left: 1px solid #f4f4f4;
            border-bottom: solid 2px #fff;
            color: #333;
        }
        /* links */
        a, a:visited
        {
            color: #000000;
            background: inherit;
            text-decoration: none;
            font: 8pt 'Arial' , Tahoma, arial, sans-serif;
        }
        a:hover
        {
            color: #000000;
            background: inherit;
            padding-bottom: 0;
            border-bottom: 2px solid #dbd5c5;
            font: 8pt 'Arial' , Tahoma, arial, sans-serif;
        }
        
        tr.tblbgbody
        {
            background: #e7e6e6 url(../images/bgorange1.JPG) repeat;
            padding-left: 8px;
            padding-right: 8px;
            text-align: center;
            border-left: 1px solid #f4f4f4;
            border-bottom: solid 2px #fff;
            color: #333;
        }
        input.button
        {
            font: bold 8pt Arial, Sans-serif;
            height: 24px;
            margin: 0;
            padding: 2px 3px;
            color: #ffffff;
            background: #E56717;
            border: 1px solid #dadada;
        }
    </style>
    <style type="text/css">
        input.button
        {
            font: bold 8pt Arial, Sans-serif;
            height: 24px;
            margin: 0;
            padding: 2px 3px;
            color: #ffffff;
            background: #E56717;
            border: 1px solid #dadada;
        }
    </style>
</head>
<body style="text-align: center">
    <form id="form1" runat="server">
    <div style="text-align: center">
        <table width="100%">
            <tr>
                <td colspan="5" style="background-image: url('../../../images/demographics.jpg');
                    height: 30px; text-align: left">
                    <span style="font-size: 8pt; font-family: Arial"><strong>Dictatorwise Account<span
                        style="font-size: 8pt"><span style="font-family: Arial">&nbsp;</span></span></strong></span>
                </td>
            </tr>
        </table>
    </div>
    <table width="500" border="2" cellpadding="2" cellspacing="2">
        <tr class="tblbg">
            <td colspan="2" style="text-align: center">
                <span style="font-size: 8pt; font-family: Arial"><strong>Search Account Details</strong></span>
            </td>
        </tr>
        <tr class="tblbg">
            <td style="text-align: center">
                <span style="font-size: 8pt; font-family: Arial"><strong>Account Name</strong></span>
            </td>
            <td style="text-align: center">
                <asp:DropDownList ID="DLAct" runat="server" Font-Names="Arial" Font-Size="8" Font-Bold="True"
                    AutoPostBack="True">
                    <asp:ListItem Text="Any" Value="" Selected="True"></asp:ListItem>
                </asp:DropDownList>
            </td>
        </tr>
    </table>
    <br />
    <asp:Table ID="Table1" runat="server" Width="70%" BorderWidth="2" GridUnits="Both"
        CellPadding="2" CellSpacing="2">
        <asp:TableRow CssClass="tblbg">
            <asp:TableCell Font-Names="Arial" Font-Size="8" ColumnSpan="4" Font-Bold="True">Existing Dictators Group 
            </asp:TableCell>
        </asp:TableRow>
        <asp:TableRow CssClass="tblbg2">
            <asp:TableCell Font-Names="Arial" Font-Size="8">Group Name 
            </asp:TableCell>
            <asp:TableCell Font-Names="Arial" Font-Size="8">Description 
            </asp:TableCell>
            <asp:TableCell Font-Names="Arial" Font-Size="8">Dictator Name 
            </asp:TableCell>
            <asp:TableCell Font-Names="Arial" Font-Size="8">Remove 
            </asp:TableCell>
        </asp:TableRow>
    </asp:Table>
    <br />
    <asp:Table ID="Table2" runat="server" Width="70%" GridUnits="Both" CellPadding="2"
        CellSpacing="2" Font-Names="Arial" Font-Size="8">
        <asp:TableRow CssClass="tblbgbody">
            <asp:TableCell Font-Names="Arial" Font-Size="8" Font-Bold="True" ColumnSpan="6">Create Dictators Group 
            </asp:TableCell>
        </asp:TableRow>
        <asp:TableRow CssClass="tblbg">
            <asp:TableCell Font-Names="Arial" Font-Size="8" HorizontalAlign="Right">Dictators Group Name
            </asp:TableCell>
            <asp:TableCell Font-Names="Arial" Font-Size="8" HorizontalAlign="Left">
                <asp:TextBox ID="TxtGrpDicName" runat="server"></asp:TextBox>
            </asp:TableCell>
            <asp:TableCell Font-Names="Arial" Font-Size="8" HorizontalAlign="Right">Description
            </asp:TableCell>
            <asp:TableCell Font-Names="Arial" Font-Size="8" HorizontalAlign="Left">
                <asp:TextBox ID="TxtDesc" runat="server"></asp:TextBox>
            </asp:TableCell>
            <asp:TableCell Font-Names="Arial" Font-Size="8" HorizontalAlign="Right">Separate Invoice
            </asp:TableCell>
            <asp:TableCell Font-Names="Arial" Font-Size="8" HorizontalAlign="Left">
                <asp:DropDownList ID="DLSepInvoice" runat="server" CssClass="common">
                    <asp:ListItem Text="No" Value="0"></asp:ListItem>
                    <asp:ListItem Text="Yes" Value="1"></asp:ListItem>
                </asp:DropDownList>
            </asp:TableCell>
        </asp:TableRow>
    </asp:Table>
    <asp:Table ID="Table5" runat="server" Width="70%" GridUnits="Both" CellPadding="2"
        CellSpacing="2" Font-Names="Arial" Font-Size="8">
        <asp:TableRow CssClass="tblbg">
            <asp:TableCell Font-Names="Arial" Font-Size="8" ColumnSpan="2">Available Dictators
            </asp:TableCell>
            <asp:TableCell Font-Names="Arial" Font-Size="8" ColumnSpan="2">Assigned Dictators
            </asp:TableCell>
        </asp:TableRow>
        <asp:TableRow CssClass="tblbg2">
            <asp:TableCell Font-Names="Arial" Font-Size="8">
                <asp:ListBox ID="ListBox1" EnableViewState="True" SelectionMode="Multiple" runat="server"
                    Height="144px" Width="208px" Font-Names="Arial" Font-Size="8" ForeColor="Firebrick">
                </asp:ListBox>
            </asp:TableCell>
            <asp:TableCell Font-Names="Arial" Font-Size="8" Width="8">
                <asp:ImageButton ID="ImageButton3" runat="server" ImageUrl="~/Images/right.jpg" />
            </asp:TableCell>
            <asp:TableCell Font-Names="Arial" Font-Size="8" Width="8">
                <asp:ImageButton ID="ImageButton2" runat="server" ImageUrl="~/Images/left.jpg" />
            </asp:TableCell>
            <asp:TableCell Font-Names="Arial" Font-Size="8">
                <asp:ListBox ID="ListBox2" runat="server" EnableViewState="True" SelectionMode="Multiple"
                    Height="144px" Width="208px" Font-Names="Arial" Font-Size="8" ForeColor="Firebrick">
                </asp:ListBox>
            </asp:TableCell>
        </asp:TableRow>
        <asp:TableRow CssClass="tblbg">
            <asp:TableCell Font-Names="Arial" Font-Size="8" ColumnSpan="4">
                <asp:Button ID="btnAssign" CssClass="button" runat="server" Text="Submit" />
            </asp:TableCell>
        </asp:TableRow>
    </asp:Table>
    <hr />
    <asp:Panel ID="Panel1" runat="server">
        <asp:Table ID="TblDetails" runat="server" CssClass="common">
            <asp:TableHeaderRow>
                <asp:TableHeaderCell CssClass="alt1">New dictators</asp:TableHeaderCell>
            </asp:TableHeaderRow>
            <asp:TableRow>
                <asp:TableCell CssClass="alt1">Account</asp:TableCell>
                <asp:TableCell CssClass="alt1">Dictator</asp:TableCell>
            </asp:TableRow>
        </asp:Table>
    </asp:Panel>
    <asp:HiddenField ID="hactname" runat="server" />
    <asp:HiddenField ID="hphyname" runat="server" />
    </form>
</body>
</html>
