<%@ Page Language="VB" AutoEventWireup="false" CodeFile="MTClientDisp.aspx.vb" Inherits="DemoAccount_ConfgDemo" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 1.0 Transitional/EN">
<html xmlns="http://www.w3.org/1999/xhtml" >

<head runat="server">
    <title>MTClient Configuration</title>
    <link href= "../App_Themes/Css/styles.css" type="text/css" rel="stylesheet" />
    <link href= "../App_Themes/Css/Common.css" type="text/css" rel="stylesheet" />

   <script type="text/javascript" language="javascript">
var newwindow;
function poptastic(inpt, inpt1)
{
    url="removea.aspx?RecordID="+ inpt + "&ActID="+ inpt1;
    //alert(inpt);
    
	newwindow=window.open(url,'name','height=100,width=400, left=300, top=100');
	if (window.focus) {newwindow.focus()}
}

</script>   
    
</head>
<body>
    <form id="form1" runat="server">
    <div style="text-align: center">
        <asp:Table ID="Table5" runat="server" Width="100%">
            <asp:TableRow runat="server" Style="text-align: center">
                <asp:TableCell runat="server" ColumnSpan="3" CssClass="HeaderDiv">Account Details</asp:TableCell>
            </asp:TableRow>
            <asp:TableRow runat="server">
                <asp:TableCell runat="server" CssClass="alt1">Account Name</asp:TableCell>
                <asp:TableCell runat="server" CssClass="alt1">Description</asp:TableCell>
                <asp:TableCell runat="server" CssClass="alt1">Account Number</asp:TableCell>
            </asp:TableRow>
        </asp:Table>
         <asp:Table ID="Table2" runat="server" Width="100%" >
             <asp:TableRow ID="TableRow37" runat="server" Style="text-align: center">
                <asp:TableCell ID="TableCell71" runat="server" ColumnSpan="3" CssClass="HeaderDiv">Assigned Attribute Details</asp:TableCell>
            </asp:TableRow>
            <asp:TableRow ID="TableRow38" runat="server">
                <asp:TableCell ID="TableCell72" runat="server" Width="60%" CssClass="alt1" >Attribute Name</asp:TableCell>
                <asp:TableCell ID="TableCell73" runat="server" Width="20%" CssClass="alt1">Size</asp:TableCell>
                 <asp:TableCell ID="TableCell74" runat="server" Width="20%" CssClass="alt1">MTClient Status</asp:TableCell>
                  
            </asp:TableRow>
            </asp:Table>
            <br />
        <br />
        <br />
        <div style="text-align:center">
            <asp:Button ID="btnConfigure" runat="server" Text="Configure Demo" CssClass="button" />
        </div>
        <br />
        <br />
        <asp:HiddenField ID="ACount" runat="server" />
        <asp:HiddenField ID="HActID" runat="server" /><asp:HiddenField ID="HFoldName" runat="server" />
        <asp:HiddenField ID="HCHBx" runat="server" />
        
    </div> 
    </form>
</body>
</html>
