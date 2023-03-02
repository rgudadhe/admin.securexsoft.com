<%@ Page Language="VB" AutoEventWireup="false" CodeFile="DemoActConfg.aspx.vb" Inherits="UserPhyAssgn_Default" EnableViewState="True" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 1.0 Transitional/EN">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <link href= "../App_Themes/Css/Main.css" type="text/css" rel="stylesheet" />
    <link href= "../App_Themes/Css/Styles.css" type="text/css" rel="stylesheet" />
    <title>Demo Account Configuration</title>
      
    <script type="text/javascript" language="javascript">
var newwindow;
function editDemo(inpt)
{
    url="EditConfgDemo.aspx?AccountID="+ inpt;
//    alert(inpt);
    
	newwindow=window.open(url,'name','height=600,width=500, left=300, top=100, scrollbars=1');
	if (window.focus) {newwindow.focus()}
}

function confgDemo(inpt)
{
    url="ConfgDemo.aspx?AccountID="+ inpt;
   // alert(inpt);
    
	newwindow=window.open(url,'name','height=600,width=500, left=300, top=100, scrollbars=1');
	if (window.focus) {newwindow.focus()}
}

function MTClientStatus(inpt)
{
    url="MTClientDisp.aspx?AccountID="+ inpt;
   // alert(inpt);
    
	newwindow=window.open(url,'name','height=600,width=500, left=300, top=100, scrollbars=1');
	if (window.focus) {newwindow.focus()}
}

</script>   

</head>
<body>
    <form id="form1" runat="server">
    <div id="body">
    <div id="cap"></div>
    <div id="main">
    <h1>Demo Account Configuration</h1>
        <%--<table id="MainTable" width="100%">
            <tr>
                <td style="text-align: center" valign="top">--%>
                    <asp:Panel  ID="Panel5" runat="server" Width="100%">
                    <table width="100%">
                    
                        <tr>
                            <td class="HeaderDiv" colspan="2" style="text-align: center">
                                <B>Account Search</B></td>
                        </tr>
                        <tr>
                            <td style="width: 50%; text-align: right; height: 30px;">
                                Account Name</td>
                            <td style="width: 50%; text-align: left; height: 30px;">
                                <asp:TextBox ID="TxtAname" runat="server"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td style="text-align: center; height: 30px;" colspan="2">
                                <asp:Button ID="BtnSubmit2" runat="server" Text="Submit" UseSubmitBehavior="False" CssClass="button" /></td>
                        </tr>
                    </table></asp:Panel>
                    <asp:Panel ID="Panel6" runat="server" Width="100%">
                    <asp:Table id="mytable" runat="server" Width="100%" >
                        <asp:TableRow runat="server" >
                            <asp:TableCell ColumnSpan="5" cssclass="HeaderDiv" style="text-align: center" runat="server"><B>Account Search</B></asp:TableCell>
                        </asp:TableRow>
                        <asp:TableRow runat="server" style="text-align: center">
                            <asp:TableCell runat="server" cssclass="alt" >Account Name</asp:TableCell>
                            <asp:TableCell runat="server" cssclass="alt">Account Number</asp:TableCell>
                            <asp:TableCell ID="TableCell3" runat="server" cssclass="alt">Mapping Account</asp:TableCell>
                           <asp:TableCell ID="TableCell2" cssclass="alt">Configuration</asp:TableCell>
                           <asp:TableCell ID="TableCell1" runat="server" cssclass="alt">MT Client Status</asp:TableCell> 
                        </asp:TableRow>
                    </asp:Table>
                    </asp:Panel>
                        <%--</td>
            </tr>
        </table>--%>
        <br />
        <asp:Label ID="DispBox" runat="server" CssClass="Title" ForeColor="#C00000"></asp:Label>
        
        <asp:HiddenField ID="GrpActState" runat="server" />
        <asp:HiddenField ID="ActState" runat="server" />
        <asp:HiddenField ID="HActID" runat="server" /><asp:HiddenField ID="HDictID" runat="server" />
        <asp:HiddenField ID="hUname" runat="server" />
        <asp:HiddenField ID="TotAct" runat="server" />
        <asp:HiddenField ID="TotLvl" runat="server" />
        <asp:HiddenField ID="HDictCode" runat="server" /><asp:HiddenField ID="HLocAcc" runat="server" />
        </div> 
        </div> 
        
    </form>
</body>
</html>
