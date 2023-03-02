<%@ Page Language="VB" AutoEventWireup="false" CodeFile="Copy of GenLC.aspx.vb" Inherits="GenLC" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Generate Unit Count</title>
    <link href= "../../App_Themes/Css/styles.css" type="text/css" rel="stylesheet"/>
    <link href= "../../App_Themes/Css/Common.css" type="text/css" rel="stylesheet"/>

   <script type="text/javascript"  language="JavaScript">
function RemDetails(BillAccID,BillCycle,BillMonth,BillYear)
{
    //alert('RemLCDetails.aspx?BillAccID='+BillAccID+'&BillCycle='+BillCycle+'&BillMonth='+BillMonth+'&BillYear='+BillYear);
   	newwindow=window.open('RemLCDetails.aspx?BillAccID='+BillAccID+'&BillCycle='+BillCycle+'&BillMonth='+BillMonth+'&BillYear='+BillYear,'name','height=200,width=400, left=300, top=100, scrollbars=1');
	if (window.focus) {newwindow.focus()}
}
</script> 
</head>
<body style="text-align: center">
    <form id="form1" runat="server">
        <div id="body">
        <div id="cap"></div>
        <div id="main">
        <h1>Generate Unit Count</h1>
        <asp:Panel ID="Panel1" runat="server" HorizontalAlign="Left">
            <table width="80%">
                <tr>
                    <td colspan="4" class="HeaderDiv">
                        Account Activity Details 
                    </td>
                </tr>
                <tr>
                    <td class="alt1">
                        Account Name
                    </td>
                    <td class="alt1">
                        Cycle
                    </td>
                    <td class="alt1">
                        Month
                    </td>
                    <td class="alt1">
                        Year
                    </td> 
                </tr>
                <tr>
                <td>
                    <asp:DropDownList ID="DLAct" runat="server" CssClass="common">
                        <asp:ListItem Text="Select Account" Value=""></asp:ListItem> 
                        <asp:ListItem Text="All Accounts" Value="All"></asp:ListItem> 
                    </asp:DropDownList>
                </td>
                <td>
                    <asp:DropDownList ID="DLCycle" runat="server" CssClass="common">
                        <asp:ListItem Text="Select Cycle" Value=""></asp:ListItem> 
                        <asp:ListItem Text="Cycle1" Value="1"></asp:ListItem> 
                        <asp:ListItem Text="Cycle2" Value="2"></asp:ListItem> 
                    </asp:DropDownList>
                </td>
                <td>
                     <asp:DropDownList ID="DLMonth" runat="server" CssClass="common">
                       
                    </asp:DropDownList>
                </td>
                <td>
                    <asp:DropDownList ID="DLYear" runat="server" CssClass="common">
                       
                    </asp:DropDownList>
                </td> 
            </tr>
            <tr>
                <td colspan="4" class="alt1">
                    <center>
                        <asp:Button ID="BtnSubmit" CssClass="button"  runat="server" Text="Submit" /> 
                    </center>
                </td>
            </tr>
        </table>
        <asp:Table ID="Table1" runat="server" CssClass="common" Width="100%">
            <asp:TableRow ID="TableHeaderRow2" runat="server">
                <asp:TableCell ID="TableCell1" CssClass="alt1" runat="server">
                    Sr No
                </asp:TableCell> 
                <asp:TableCell ID="TableCell2" CssClass="alt1" runat="server">
                    Account Name
                </asp:TableCell> 
                <asp:TableCell ID="TableCell6" CssClass="alt1" runat="server">
                    Mode
                </asp:TableCell> 
                <asp:TableCell ID="TableCell7" CssClass="alt1" runat="server">
                    Report#
                </asp:TableCell>
                <asp:TableCell ID="TableCell8" CssClass="alt1" runat="server">
                    Units 
                </asp:TableCell>
                <asp:TableCell ID="TableCell9" CssClass="alt1" runat="server">
                    Method
                </asp:TableCell>
                <asp:TableCell ID="TableCell10" CssClass="alt1" runat="server">
                    LocName
                </asp:TableCell>
            </asp:TableRow>
        </asp:Table>
        <asp:Table ID="Table2" runat="server" Width="100%">
            <asp:TableRow ID="TableHeaderRow1" runat="server">
                <asp:TableCell ID="TableCell3" CssClass="alt1" runat="server">
                    Sr No
                </asp:TableCell> 
                <asp:TableCell ID="TableCell4" CssClass="alt1" runat="server">
                    Account Name
                </asp:TableCell> 
                <asp:TableCell ID="TableCell5" CssClass="alt1" runat="server">
                    Generate LC
                </asp:TableCell>
            </asp:TableRow>
        </asp:Table>
        </asp:Panel>
        </div> 
        </div> 
    </form>
</body>
</html>
