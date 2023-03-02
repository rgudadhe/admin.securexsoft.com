<%@ Page Language="VB" AutoEventWireup="false" CodeFile="ViewGeneratedBillDetails.aspx.vb" Inherits="GenLC_New" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Generate Unit Count</title>
    <link href= "../../App_Themes/Css/styles.css" type="text/css" rel="stylesheet"/>
    <link href= "../../App_Themes/Css/Common.css" type="text/css" rel="stylesheet"/>

 
</head>
<body style="text-align: center">
    <form id="frmTrans" runat="server">
    <asp:HiddenField ID="HReptID" runat="server" />
        <div id="body">
        <div id="cap"></div>
        <div id="main">
        <h1>View Generate Unit Details</h1>
        <asp:Panel ID="Panel1" runat="server" HorizontalAlign="Left">
            <table width="80%">
                <tr>
                    <td colspan="5" class="HeaderDiv">
                        Account Activity Details 
                    </td>
                </tr>
                <tr>
                    <td class="alt1">
                        Account Name
                    </td>
                    <td class="alt1">
                        Status
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
                        
                        <asp:ListItem Text="All Accounts" Value=""></asp:ListItem> 
                    </asp:DropDownList>
                </td>
                 <td>
                    <asp:DropDownList ID="DLStatus" runat="server" CssClass="common">
                        <asp:ListItem Text="All" Value=""></asp:ListItem> 
                        <asp:ListItem Text="Failure" Value="Failure"></asp:ListItem> 
                        <asp:ListItem Text="Success" Value="Success"></asp:ListItem>
                        <asp:ListItem Text="Removed" Value="Success"></asp:ListItem>
                        <asp:ListItem Text="No Records" Value="NoRecords"></asp:ListItem> 
                    </asp:DropDownList>
                </td>
                <td>
                    <asp:DropDownList ID="DLCycle" runat="server" CssClass="common">
                        
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
                <td colspan="5" class="alt1">
                    <center>
                        <asp:Button ID="BtnSubmit" CssClass="button"  runat="server" Text="Submit" /> 
                    </center>
                </td>
            </tr>
        </table>
        <br />
         <asp:Label Visible="false"  ID="Label1" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="8" ForeColor="Red"></asp:Label>
       <%-- <asp:Table ID="Table1" runat="server" CssClass="common" Width="100%">
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
        </asp:Table>--%>
        <%--    <asp:Button ID="btnRemvoe" runat="server" Text="Remove Data" />--%>
            <asp:GridView ID="GridView1" runat="server" Width="600px">
            </asp:GridView>
        </asp:Panel>
        </div> 
        </div> 
    </form>
</body>
</html>
