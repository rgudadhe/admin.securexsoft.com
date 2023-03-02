<%@ Page Language="VB" AutoEventWireup="false" CodeFile="ExchangeRate.aspx.vb" Inherits="Billing_FileImport_ImportLines" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 1.0 Transitional/EN">
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Daily Team Report</title>
    <link rel="Stylesheet" type="text/css" href="../../App_Themes/Css/styles.css" />
    <link rel="Stylesheet" type="text/css" href="../../App_Themes/Css/Common.css" />
</head>
<body>
    <form id="form1" runat="server">
    <div id="body">
        <div id="cap">
        </div>
        <div id="main">
            <h1>
                Exchange Rate</h1>
            <table width="500">
                <tr>
                    <td style="text-align: center" width="30%" class="alt1">
                        Month
                    </td>
                    <td style="text-align: center" width="30%" class="alt1">
                        Year
                    </td>
                </tr>
                <tr>
                    <td style="text-align: center" width="30%">
                        <asp:DropDownList ID="DLMonth" runat="server" CssClass="common" AutoPostBack="true">
                            <asp:ListItem Text="January" Value="1"></asp:ListItem>
                            <asp:ListItem Text="February" Value="2"></asp:ListItem>
                            <asp:ListItem Text="March" Value="3"></asp:ListItem>
                            <asp:ListItem Text="April" Value="4"></asp:ListItem>
                            <asp:ListItem Text="May" Value="5"></asp:ListItem>
                            <asp:ListItem Text="June" Value="6"></asp:ListItem>
                            <asp:ListItem Text="July" Value="7"></asp:ListItem>
                            <asp:ListItem Text="August" Value="8"></asp:ListItem>
                            <asp:ListItem Text="September" Value="9"></asp:ListItem>
                            <asp:ListItem Text="October" Value="10"></asp:ListItem>
                            <asp:ListItem Text="November" Value="11"></asp:ListItem>
                            <asp:ListItem Text="December" Value="12"></asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td style="text-align: center" width="30%">
                        <asp:DropDownList ID="DLYear" runat="server" CssClass="common" AutoPostBack="true">
                            
                            <asp:ListItem Text="2010" Value="2010"></asp:ListItem>
                            <asp:ListItem Text="2011" Value="2011"></asp:ListItem>
                            <asp:ListItem Text="2012" Value="2012"></asp:ListItem>
                            <asp:ListItem Text="2013" Value="2013"></asp:ListItem>
                            <asp:ListItem Text="2014" Value="2014"></asp:ListItem>
                            <asp:ListItem Text="2015" Value="2015"></asp:ListItem>
                            <asp:ListItem Text="2016" Value="2016"></asp:ListItem>
                            <asp:ListItem Text="2017" Value="2017"></asp:ListItem>
                            <asp:ListItem Text="2018" Value="2018"></asp:ListItem>
							<asp:ListItem Text="2019" Value="2019"></asp:ListItem>
							<asp:ListItem Text="2020"  Value="2020"></asp:ListItem>
							<asp:ListItem Text="2021"  Value="2021"></asp:ListItem>
							  <asp:ListItem Text="2022" Selected="True" Value="2022"></asp:ListItem>
                            <asp:ListItem Text="2023" Value="2023"></asp:ListItem>
                            <asp:ListItem Text="2024" Value="2024"></asp:ListItem>
							
							
							
                        </asp:DropDownList>
                    </td>
                </tr>
            </table>
            <table id="Table2" width="40%">
                <tr>
                    <td class="DEMO4" valign="top">
                        Exchange Rate
                    </td>
                    <td class="DEMO4" valign="top">
                        <asp:TextBox ID="TXTExchRate" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="DEMO5" valign="top" colspan="2">
                        &nbsp;<asp:Button ID="Button1" runat="server" Text="Submit" CssClass="button" />
                    </td>
                </tr>
            </table>
        </div>
    </form>
</body>
</html>
