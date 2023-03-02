<%@ Page Language="VB" AutoEventWireup="false" CodeFile="SummarySentFAX.aspx.vb" Inherits="FaxPlus_SummarySentFAX" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Register Assembly="KMobile.Web" Namespace="KMobile.Web.UI.WebControls" TagPrefix="asp" %>
<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Sent FAX</title>
    <link href= "../App_Themes/Css/styles.css" type="text/css" rel="stylesheet" />
    <link href= "../App_Themes/Css/Common.css" type="text/css" rel="stylesheet" />
    <link href="../App_Themes/Css/DataTable.css" rel="stylesheet" type="text/css" />
    <link href="../App_Themes/Css/TableSorter.css" rel="stylesheet" type="text/css" />
    <link href="../App_Themes/Css/Calendar.css" rel="stylesheet" type="text/css" />
    <script src="../App_Themes/JS/jquery-1.4.2.min.js" type="text/javascript"></script>  
    <script src="../App_Themes/JS/jquery.dataTables.min.js" type="text/javascript"></script>  
    <script type="text/javascript" language="javascript">
        $(document).ready(function() {
				$('#GridViewMain').dataTable( {
                    "aoColumns": [
                                    { "asSorting": [ "desc", "asc" ] },
                            		{ "asSorting": [ "desc", "asc" ] },
		                            { "asSorting": [ "desc", "asc" ] },
		                            { "asSorting": [ "desc", "asc" ] },
		                            { "asSorting": [ "desc", "asc" ] },
		                            { "asSorting": [ "desc", "asc" ] },
                            		{ "asSorting": [ "desc", "asc" ] },
		                            { "asSorting": [ "desc", "asc" ] },
		                            { "asSorting": [ "desc", "asc" ] },
		                            { "asSorting": [ "desc", "asc" ] }
	                              ] ,
                    "aaSorting": [[ 1, "asc" ]]
				} );
			} );
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
    <div id="body">
        <div id="cap"></div>
        <div id="main">
        <h1>Summary Sent Faxes</h1>
            <asp:Table ID="tblMain" runat="server" Width="100%">
                <%--<asp:TableRow CssClass="ReportHeaderDiv"> 
                    <asp:TableCell HorizontalAlign=Center>
                        Sent
                    </asp:TableCell>
                </asp:TableRow>--%>
                
                <asp:TableRow>
                    <asp:TableCell BorderStyle="None" BorderWidth="0">
                        <asp:DropDownList ID="ddlAccounts" runat="server" Font-Names="Trebuchet MS">
                        </asp:DropDownList>
                        
                        Start Date : 
                        <asp:TextBox ID="txtStartDate" runat="server" Width="60"></asp:TextBox>
                        End Date :
                        <asp:TextBox ID="txtEndDate" runat="server" Width="60"></asp:TextBox>
                        &nbsp;&nbsp;&nbsp;
                        <asp:Button ID="btnSubmit" runat="server" Text="Submit" Font-Names="Trebuchet MS" Font-Size="8" CssClass="button" />
                        
                        <ajaxtoolkit:calendarextender id="CalendarExtender1" runat="server" CssClass="cal_Theme1"
                        targetcontrolid="txtStartDate"></ajaxtoolkit:calendarextender>
                    <ajaxtoolkit:calendarextender id="CalendarExtender2" runat="server" CssClass="cal_Theme1"
                        targetcontrolid="txtEndDate"></ajaxtoolkit:calendarextender>                    
                    </asp:TableCell>
                </asp:TableRow>
                <asp:TableRow>
                    <asp:TableCell BorderStyle="None" BorderWidth="0">
                                <asp:LinkButton ID="LnkExport" runat="server"  >Export Result</asp:LinkButton> 
                   </asp:TableCell>
                 </asp:TableRow>
                <asp:TableRow>
                    <asp:TableCell BorderStyle="None" BorderWidth="0">
                        <asp:CompleteGridView ID="MyDataGrid" runat="server" AutoGenerateColumns="True" 
                     cellspacing="2" CellPadding="2" 
                    
                     EnableViewState="False"   
                     Width="100%"   CountFormat="Displaying records <b>{0}</b> to <b>{1}</b> of <b>{2}</b>" CaptionAlign="Bottom"   ShowCount="False"  >
                <AlternatingRowStyle cssclass="gridalt2"  ></AlternatingRowStyle>
                <RowStyle cssclass="gridalt1"  ></RowStyle>
                </asp:CompleteGridView>
                    </asp:TableCell>
                </asp:TableRow>
            </asp:Table>
            <asp:Table ID="Table1" BorderWidth="0" BorderStyle="None" runat="server" Width=100%>
                <asp:TableRow>
                    <asp:TableCell VerticalAlign=Bottom HorizontalAlign=Left BorderStyle="None">
                        <asp:Label ID="lblMessage" Font-Names="Trebuchet MS" Font-Size="8pt" ForeColor="Firebrick" runat="server"></asp:Label>
                  </asp:TableCell>
                </asp:TableRow>
            </asp:Table>
        </div> 
        </div> 
    </form>
</body>
</html>
