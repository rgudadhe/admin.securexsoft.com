<%@ Page Language="VB" AutoEventWireup="false" CodeFile="TicketMainPage.aspx.vb" EnableEventValidation="true" Inherits="TicketMainPage" Debug= "true" %>

<%@ Register Assembly="System.Web.Extensions, Version=1.0.61025.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI" TagPrefix="asp" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 1.0 Transitional/EN">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Active Tickets</title>
    <link href= "../../App_Themes/Css/styles.css" type="text/css" rel="stylesheet" />
    <link href= "../../App_Themes/Css/Common.css" type="text/css" rel="stylesheet" />
    <link href="../../App_Themes/Css/DataTable.css" rel="stylesheet" type="text/css" />
    <link href="../../App_Themes/Css/TableSorter.css" rel="stylesheet" type="text/css" />
    <script src="../../App_Themes/JS/jquery-1.4.2.min.js" type="text/javascript"></script>  
    <script src="../../App_Themes/JS/jquery.dataTables.min.js" type="text/javascript"></script>  

    <script language="javascript" src="../../App_Themes/JS/tooltip.js" type="text/javascript"></script>
    <script language="javascript" type="text/javascript">
        function Test(str)
        {
            //alert(str)
        }
        function expandcollapse(obj,row)
        {
            var div = document.getElementById(obj);
            var img = document.getElementById('img' + obj);
        
            if (div.style.display == "none")
            {
                div.style.display = "block";
                if (row == 'alt')
                {
                    img.src = "minus.gif";
                }
                else
                {
                    img.src = "minus.gif";
                }
                img.alt = "Close to view Issue Types";
            }
            else
            {
                div.style.display = "none";
                if (row == 'alt')
                {
                    img.src = "plus.gif";
                }
                else
                {
                    img.src = "plus.gif";
                }
                img.alt = "Expand to show Issue Types";
            }
        }
     </script>   
</head>
<script type="text/javascript" language="javascript">
    $(document).ready(function() {
				$('#GridViewTickets').dataTable( {
					//"sPaginationType": "full_numbers"
                    "aoColumns": [
                            		{ "asSorting": [ "desc", "asc" ] },
		                            { "asSorting": [ "desc", "asc" ] },
		                            { "asSorting": [ "desc", "asc" ] },
		                            { "asSorting": [ "desc", "asc" ] },
		                            { "asSorting": [ "desc", "asc" ] },
                            		{ "asSorting": [ "desc", "asc" ] },
		                            { "asSorting": [ "desc", "asc" ] },
		                            { "asSorting": [ "desc", "asc" ] }
		                            
	                              ] 
				} );
			} );
</script>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
    <div>
        <center><asp:Label ID="lblTickets" runat="server" CssClass="common" Text="" Visible="false"></asp:Label></center>
        <asp:Table ID="tblStatus"  runat="server" Width="100%" CssClass="common" >
                    <asp:TableRow>
                        <asp:TableCell BorderStyle="None">
                            <asp:DropDownList ID="ddlStatus" runat="server" AutoPostBack="true" CssClass="common" Width="150" OnSelectedIndexChanged="ddlStatus_SelectedIndexChanged" >
                                <asp:ListItem Text="Open" Value="Open" ></asp:ListItem>
                                <asp:ListItem Text="Close" Value="Close"></asp:ListItem>
                                <asp:ListItem Text="All" Value=""></asp:ListItem>
                            </asp:DropDownList>                
                        </asp:TableCell>
                    </asp:TableRow>
                </asp:Table>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <asp:Table ID="tblCancel" runat="server" Width="100%" >
                    <asp:TableRow>
                        <asp:TableCell HorizontalAlign="Center" CssClass="HeaderDiv">
                            Active Tickets
                        </asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow>
                        <asp:TableCell HorizontalAlign="Center" BorderStyle="None">
                            <asp:GridView ID="GridViewTickets" Width="100%" runat="server"
                                AutoGenerateColumns="false" ShowFooter="true">
                                <AlternatingRowStyle BackColor="PaleGoldenrod"   />
                                <Columns>
                                    <asp:TemplateField HeaderText="Ticket No" HeaderStyle-CssClass="Header">
                                        <ItemTemplate>
                                            <%--<asp:Label ID="lblTicketID" runat="server" Text=<%#Eval("TicketNo") %>></asp:Label>--%>
                                            <asp:LinkButton ID="ActionTicket" CommandName="ActionTicket" CommandArgument='<%#Eval("TicketID").ToString() & "#" & Eval("Forward") %>' runat="server"><%#Eval("TicketNo") %></asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="CateName" ItemStyle-HorizontalAlign="Left" HeaderStyle-CssClass="Header">
                                        <ItemTemplate>
                                            <asp:Label ID="lblCateName" runat="server" Text=<%#Eval("CateName")%>></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="IssueName" ItemStyle-HorizontalAlign="Left" HeaderStyle-CssClass="Header">
                                        <ItemTemplate>
                                            <asp:Label ID="iblIssueName" runat="server" Text=<%#Eval("IssueName")%>></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="IssueDescription" ItemStyle-HorizontalAlign="Left" HeaderStyle-CssClass="Header">
                                        <ItemTemplate>
                                            <asp:Label ID="iblIssueDetails" runat="server" Text=<%#ValidateString(Eval("Description").ToString())%>></asp:Label>
                                        </ItemTemplate>    
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Priority" HeaderStyle-CssClass="Header">
                                        <ItemTemplate>
                                            <asp:Label ID="lblPriority" runat="server" Text=<%#Eval("Priority") %>></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Date Posted" HeaderStyle-CssClass="Header">
                                        <ItemTemplate>
                                            <asp:Label ID="lblDataPosted" runat="server" Text=<%#Eval("DatePosted").ToShortDateString() %>></asp:Label>                                                
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Raise By" ItemStyle-HorizontalAlign="Left" HeaderStyle-CssClass="Header"> 
                                        <ItemTemplate>
                                            <asp:Label ID="Label1" runat="server" Text=<%#Eval("FirstName") &" "& Eval("LastName") %>></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Age(HH:MM)" ItemStyle-HorizontalAlign="Left" HeaderStyle-CssClass="Header"> 
                                        <ItemTemplate>
                                            <%--<asp:Label ID="LabelAge" runat="server" Text=<%#Eval("DateDiffHr") &" Hrs "& Eval("DateDiffMin") & " mins " %>></asp:Label>--%>
                                            <asp:Label ID="Label2" runat="server" Text=<%#Eval("DateDiffHr") & ":" & Eval("DateDiffMin") %>></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>                                    
                                    <%--<asp:TemplateField>
                                        <ItemTemplate>
                                            <asp:LinkButton ID="ActionTicket" CommandName="ActionTicket" CommandArgument='<%#Eval("TicketID").ToString() & "#" & Eval("Forward") %>' runat="server">Action</asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>--%>
                                </Columns>           
                            </asp:GridView>
                            <%--<asp:SqlDataSource ID="SQLDataSrcTickets" runat="server"></asp:SqlDataSource>--%>
                        </asp:TableCell>
                    </asp:TableRow>
                </asp:Table>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
    <div style='position:absolute; visibility:hidden; z-index:1000;' id='ToolTip'></div>
    </form>
</body>
</html>
