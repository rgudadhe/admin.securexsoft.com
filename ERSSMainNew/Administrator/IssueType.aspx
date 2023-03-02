<%@ Page Language="VB" AutoEventWireup="false" CodeFile="IssueType.aspx.vb" Inherits="ERSSMain_IssueType" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 1.0 Transitional/EN">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Issue Types</title>
    <link href="../../App_Themes/Css/styles.css" rel="stylesheet" type="text/css" />
    <link href="../../App_Themes/Css/Common.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        <div>
	        <asp:Table ID="Table2" runat="server" HorizontalAlign="Center" Width="80%">
	            <asp:TableRow>
	                <asp:TableCell HorizontalAlign="Left">
                        <asp:DropDownList ID="DropDownIssueCate" runat="server" Width="50%" AutoPostBack="true" CssClass="common">
                        </asp:DropDownList>    
	                </asp:TableCell>
	            </asp:TableRow>
	        </asp:Table>
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    <asp:Table ID="table1" runat="server" HorizontalAlign="Center" Width="80%">
	                    <asp:TableRow>
	                        <asp:TableCell ColumnSpan="5" HorizontalAlign="Center" CssClass="HeaderDiv">
	                            Issue Types
	                        </asp:TableCell>
	                    </asp:TableRow>
	                    <asp:TableRow Height="20">
	                        <asp:TableCell CssClass="alt1">
        	                    Issue Name
	                        </asp:TableCell>
	                        <asp:TableCell CssClass="alt1">
	                            Description
	                        </asp:TableCell>
	                        <%--<asp:TableCell>
	                            Mode
	                        </asp:TableCell>
	                        <asp:TableCell>
	                            Copy To Team
	                        </asp:TableCell>--%>
	                        <asp:TableCell CssClass="alt1">
	                         &nbsp;
	                        </asp:TableCell>
	                    </asp:TableRow>
	                </asp:Table>
                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="DropDownIssueCate" EventName="SelectedIndexChanged" />
                </Triggers>
            </asp:UpdatePanel>
        </div>
    </form>
</body>
</html>
