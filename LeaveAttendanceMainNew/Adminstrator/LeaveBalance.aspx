<%@ Page Language="VB" AutoEventWireup="false" CodeFile="LeaveBalance.aspx.vb" Inherits="LeaveBalance" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 1.0 Transitional/EN">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Leave Balance</title>
    <link href= "../../App_Themes/Css/styles.css" type="text/css" rel="stylesheet" />
    <link href= "../../App_Themes/Css/Common.css" type="text/css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
    	<div>
            <center>
                <asp:Table ID="Table1" runat="server" Width="273px">
                    <asp:TableRow runat="server" >
                        <asp:TableCell HorizontalAlign="Center" runat="server" CssClass="HeaderDiv" ColumnSpan="2">
                            Select Department
                        </asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow>
                        <asp:TableCell HorizontalAlign="Left" Width="100px" runat="server" CssClass="common">
                            Department
                        </asp:TableCell>
                        <asp:TableCell HorizontalAlign="Left" CssClass="common" runat="server">
                            <asp:DropDownList ID="DropDownGroup" runat="server" CssClass="common">
                            </asp:DropDownList>
                        </asp:TableCell>
                    </asp:TableRow> 
                    <asp:TableRow>
                        <asp:TableCell ColumnSpan="2" HorizontalAlign="Center">
                            <center>
                                <asp:Button ID="BtnSubmit" CssClass="button" runat="server" Text="Submit" />
                            </center>
                        </asp:TableCell>
                    </asp:TableRow>
                </asp:Table>
            </center>
            <br />
            <br />
            <center>
	            <asp:Table ID="Table3" runat="server" Width="100%" CssClass="common">
                    <asp:TableRow HorizontalAlign="Left">
                        <asp:TableCell HorizontalAlign="Left" BorderStyle="None" BorderWidth="0">
                            <asp:LinkButton ID="btnExport" CssClass="common" runat="server">Export Results</asp:LinkButton>
                        </asp:TableCell>
                    </asp:TableRow>
                </asp:Table> 
                 <asp:Table ID="Table2" runat="server" Width="100%">
                    <asp:TableRow HorizontalAlign="Center">
                        <asp:TableCell HorizontalAlign="Center" ColumnSpan="9" CssClass="HeaderDiv">
                            Leave Balance Sheet Of Employees
                        </asp:TableCell>
                    </asp:TableRow>
                    <asp:TableHeaderRow ID="TableHeaderRow1" runat="server">
                        <asp:TableHeaderCell ID="TableHeaderCell9" CssClass="alt1" runat="server">
			                ID
                        </asp:TableHeaderCell>
                        <asp:TableHeaderCell ID="TableHeaderCell1" CssClass="alt1" runat="server">
                            Employee Name
                        </asp:TableHeaderCell>
                        <asp:TableHeaderCell ID="TableHeaderCell2" CssClass="alt1" runat="server">
                            Department
                        </asp:TableHeaderCell>
                        <asp:TableHeaderCell ID="TableHeaderCell3" CssClass="alt1" runat="server" >
                            CL
                        </asp:TableHeaderCell>                  
                        <asp:TableHeaderCell ID="TableHeaderCell4" CssClass="alt1" runat="server" >
                            EL
                        </asp:TableHeaderCell>                    
                        <asp:TableHeaderCell ID="TableHeaderCell5" CssClass="alt1" runat="server" >
                            TL
                        </asp:TableHeaderCell>                    
                        <asp:TableHeaderCell ID="TableHeaderCell6" CssClass="alt1" runat="server" >
                            WOff1
                        </asp:TableHeaderCell>
                        <asp:TableHeaderCell ID="TableHeaderCell7" CssClass="alt1" runat="server" >
                            WOff2
                        </asp:TableHeaderCell>
                        <asp:TableHeaderCell ID="TableHeaderCell8" CssClass="alt1" runat="server">
                            Edit
                        </asp:TableHeaderCell>                                                            
                    </asp:TableHeaderRow>
                </asp:Table>
            </center>
       </div>
    </form>
</body>
</html>
