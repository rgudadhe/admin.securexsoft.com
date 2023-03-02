<%@ Page Language="VB" AutoEventWireup="false" CodeFile="DetailPastTicket.aspx.vb" Inherits="DetailPastTicket" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 1.0 Transitional/EN">
<script language="javascript" type="text/javascript">
    function CheckComment()
    {
        //alert('Test')
        alert(document.getElementByName("TextArea1"))
        if (document.form1.TextArea1.value=='') 
		{
			alert('Please Enter Comments.')
			document.form1.TextArea1.focus();
			return false;
		}
	    return false;
    } 
</script>
<html xmlns="http://www.w3.org/1999/xhtml" >

<head runat="server">
    <title>Ticket History</title>
    <%--<LINK href= "../../Styles/NewFolder/Accordin.css" type="text/css" rel="stylesheet">--%>
    <link href= "../App_Themes/Css/styles.css" type="text/css" rel="stylesheet" />
    <link href= "../App_Themes/Css/Common.css" type="text/css" rel="stylesheet" />
    
</head>

<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
    <div>
    <div style="text-align:left">
    
    <Font size=2 sTYLE="font-family:Arial" ><a href="JavaScript:history.go(-1)">Return To Ticket List</a></Font></div>
    <BR>
    <center>
        <asp:Table ID="Table1" runat="server" Width="90%">
            <asp:TableRow ID="TableRow1" runat="server" >
                <asp:TableCell ID="TableCell1" runat="server" HorizontalAlign="center" ColumnSpan="2" CssClass="HeaderDiv">
                    Ticket Details
                </asp:TableCell>
            </asp:TableRow>
            <asp:TableRow>
                <asp:TableCell HorizontalAlign="Left" Width="50%">
                    <asp:Label ID="Label3" runat="server" CssClass="common" Text="UserName : "></asp:Label>
                    <asp:Label ID="lblUserName" runat="server" CssClass="common" ></asp:Label>
                </asp:TableCell>
                <asp:TableCell HorizontalAlign="Left" Width="50%">
                    <asp:Label ID="Label5" runat="server" CssClass="common"  Text="Ticket No : "></asp:Label>
                    <asp:Label ID="lblTicketNo" runat="server" CssClass="common" Text=<%#Eval("TicketNo") %>></asp:Label>                    
                </asp:TableCell>
            </asp:TableRow>  
            <asp:TableRow>
                <asp:TableCell HorizontalAlign="Left" Width="50%">
                    <asp:Label ID="Label7" runat="server" CssClass="common" Text="Issue Type : "></asp:Label>
                    <asp:Label ID="lblIssueName" CssClass="common" runat="server" Text=<%#Eval("IssueName") %>></asp:Label>
                </asp:TableCell>
                <asp:TableCell HorizontalAlign="Left" Width="50%">
                    <asp:Label ID="Label11" CssClass="common" runat="server" Text="Priority : "></asp:Label>
                    <asp:Label ID="lblPriority" CssClass="common" runat="server"></asp:Label>
                </asp:TableCell>
            </asp:TableRow>
            <asp:TableRow>
                <asp:TableCell HorizontalAlign="Left" Width="50%">
                    <asp:Label ID="Label9" CssClass="common" runat="server" Text="Open Date/Time : "></asp:Label>
                    <asp:Label ID="lblDatePosted" CssClass="common" runat="server"></asp:Label>
                </asp:TableCell>
                <asp:TableCell HorizontalAlign="Left" Width="50%">
                    <asp:Label ID="Label1" runat="server" CssClass="common" Text="Closed Date/Time : "></asp:Label>
                    <asp:Label ID="lblDateClosed" runat="server" CssClass="common"></asp:Label>
                </asp:TableCell>
            </asp:TableRow>
        </asp:Table>
        <asp:Table ID="tblResponses" runat="server" Width="90%" CssClass="common">
            <asp:TableRow>
                <asp:TableCell HorizontalAlign="Left" CssClass="alt2">
                    Problem History :
                </asp:TableCell>
            </asp:TableRow>
        </asp:Table>
        <asp:Table ID="tblComments" runat="server" Width="90%">
            <asp:TableRow>
                <asp:TableCell Width="100%">
                    <ajaxToolkit:Accordion ID="MyAccordion" runat="server" Width="101%"
                               HeaderCssClass="accordionHeaderPastTickets" SelectedIndex="-1" FadeTransitions="true" FramesPerSecond="40" 
                                TransitionDuration="250" AutoSize="None"  RequireOpenedPane="false" SuppressHeaderPostbacks="true">
                        <Panes> 
                            <ajaxToolkit:AccordionPane ID="AddComments"  Width="90%" runat="server">
                                <Header>
					                <b><font face="Arial" size="2">Comment(<i> Click here to add comment</i> )</font></b>
                                </Header>
                                
                                <Content>
                                    <center><asp:RequiredFieldValidator  Display="None" ID="RequiredFieldComment" runat="server" ErrorMessage="Please enter comments"  SetFocusOnError="true" ControlToValidate="TextArea1"></asp:RequiredFieldValidator></center>
                                    <asp:Table ID="tblAddComments" Width="100%" runat="server" HorizontalAlign="Center">
                                        <asp:TableRow>
                                            <asp:TableCell HorizontalAlign="Center" BorderStyle="None">
                                                <textarea id="TextArea1" name="TextArea1" cols="90" rows="3" style="width:95% ; border-color:Purple" runat="server" class="common"></textarea>            
                                            </asp:TableCell>
                                        </asp:TableRow>
                                        <asp:TableRow>
                                            <asp:TableCell HorizontalAlign="Center" BorderStyle="None">
                                                <center>
                                                    <asp:Button ID="btnAdd" runat="server" Text="Add" CssClass="button"  UseSubmitBehavior="true"/>                                                
                                                </center>
                                            </asp:TableCell>
                                        </asp:TableRow>
                                    </asp:Table>
                                </Content>
                            </ajaxToolkit:AccordionPane>
                        </Panes>                        
                    </ajaxToolkit:Accordion>
                </asp:TableCell>
            </asp:TableRow>
        </asp:Table>
        <asp:Table ID="tblSolutionHistory" Width="90%" runat="server" CssClass="common">
            <asp:TableRow>
                <asp:TableCell HorizontalAlign="Left" CssClass="alt2">
                    Response History :
                </asp:TableCell>
            </asp:TableRow>
        </asp:Table>
        <BR>        
        <asp:Button ID="BtnReOpen" CssClass="button" runat="server" Text="ReOpen Ticket" UseSubmitBehavior="false" CausesValidation="false" /> &nbsp &nbsp
        <asp:Button ID="BtnClose" CssClass="button" runat="server" Text="Close Ticket" UseSubmitBehavior="false" CausesValidation="false" />
     </center>
    </div>
    <asp:ValidationSummary ID="ValidationSummary1" runat="server" HeaderText="Following error occurs:" ShowMessageBox="true" DisplayMode="BulletList" ShowSummary="False" /> </form>
</body>
</html>
