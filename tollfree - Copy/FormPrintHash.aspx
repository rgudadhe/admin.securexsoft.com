<%@ Page Language="VB" AutoEventWireup="false" CodeFile="FormPrintHash.aspx.vb" Inherits="FormPrintHash"%>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>Toll-Free Phone System Instructions</title>
<script type="text/javascript" >
function printpg()
{
var printbutton=document.getElementById("btnprnt");

printbutton.style.visibility='hidden';

window.print();
printbutton.style.visibility='visible';

}
function printpg2()
{
var printbutton2=document.getElementById("btnprnt2");
printbutton2.style.visibility='hidden';
window.print();
printbutton2.style.visibility='visible';
}
</script>
<style>
@media print {
body {-webkit-print-color-adjust: exact;
background-position:center;
}
}
 table{
   vertical-align:top;
  }
</style>
</head>
<body>

<table>
<tr>
<td>
    <form id="form1" runat="server">
    <div>
    <h2 style="text-align: center; font-family:Arial">Toll-Free Phone System Instructions</h2>
    <h3 style="text-align: center; font-family:Arial"><asp:Label ID="Label4" runat="server" Text="" Width="600px"></asp:Label></h3>
     <p style="font-family:Arial; font-size:smaller">
        <b>>> </b>Dial Toll Free Number (866)-239-1729 or (800)-385-4418.<br />
        <b>>> </b>Enter the Physician ID followed by # sign<br />
        <b>>> </b>Enter the Password followed by # sign<br />
        <asp:Label ID="Label1" runat="server" Text="" Width="256px"></asp:Label><br />
        <asp:Label ID="Label2" runat="server" Text="" Width="256px"></asp:Label><br />
		<asp:Label ID="Label3" runat="server" Text="" Width="256px"></asp:Label><br />
	</p>

<table valign="top">
<tr>
        <td valign="top">
                    <asp:Repeater id="keytbl" runat="server">
                    <HeaderTemplate>
                    <table border="1" width="100%" style="font-family:Arial; font-size:smaller">
                    <tr>
                    <td colspan="2" align="center" style="background-color:Navy;"><strong style="color:White">Keypad</strong></td>
                    </tr>
                    <tr>
                    <th>Key#</th>
                    <th>Description</th>
                    </tr>
                    </HeaderTemplate>
                    <ItemTemplate>
                    <tr>
                    <td><%#Container.DataItem("keyin")%></td>
                    <td><%#Container.DataItem("activity")%></td>
                    </tr>
                    </ItemTemplate>

                    <FooterTemplate>
                    </table>
                    </FooterTemplate>
                    </asp:Repeater>
        </td>


        <td valign="top">
                    <asp:Repeater id="prompttb" runat="server">
                    <HeaderTemplate>
                    <table border="1" width="100%" style="font-family:Arial; font-size:smaller">
                    <tr>
                    <td colspan="3" align="center" style="background-color:Navy;"><strong style="color:White">Prompt</strong></td>
                    </tr>
                    <tr>
                    <th>Prompt</th>
                    <th>Key IN</th>
                    <th>Description</th>
                    </tr>
                    </HeaderTemplate>
                    <ItemTemplate>
                    <tr>
                    <td><%#Container.DataItem("prompt")%></td>
                    <td><%#Container.DataItem("keyin")%></td>
                    <td><%#Container.DataItem("description")%></td>
                    </tr>
                    </ItemTemplate>

                    <FooterTemplate>
                    </table>
                    </FooterTemplate>
                    </asp:Repeater>
           </td>

</tr>

<tr>
            <td valign="top">
                    <asp:Repeater id="accttb" runat="server">
                    <HeaderTemplate>
                    <table border="1" width="100%" style="font-family:Arial; font-size:smaller">
                    <tr>
                    <td colspan="4" align="center" style="background-color:Navy;"><strong style="color:White">Physician ID and Password</strong></td>
                    </tr>
                    <tr>
                    <th>Last Name</th>
                    <th>First Name</th>
                    <th>ID</th>
                    <th>Password</th>
                    </tr>
                    </HeaderTemplate>
                    <ItemTemplate>
                    <tr>
                    <td><%#Container.DataItem("diclname")%></td>
                    <td><%#Container.DataItem("dicfname")%></td>
                    <td><%#Container.DataItem("id")%></td>
                    <td><%#Container.DataItem("password")%></td>
                    </tr>
                    </ItemTemplate>

                    <FooterTemplate>
                    </table>
                    </FooterTemplate>
                    </asp:Repeater>
            </td>
</tr>

</table>  
        <p style="font-size: small; font-family: Arial">
            NOTE: Whenever you decide to use the floating ID where a physician needs to dictate and a permanent ID has not yet been assigned, please ask the Dictator to identify his/her full name and credentials before dictating and send an email to support@medofficepro.com providing full name and credentials of the physician so that an appropriate signature block can be added to reports. Also, please note we typically require one business day (or 24 hrs) to create a new Physician ID and Password.</p>
        <asp:Button ID="btnprnt" UseSubmitBehavior="false"  runat="server" Text="Print this page" OnClientClick="printpg();return false;"/>
    </div>
    </form>
    
</td>
</tr>   
</table>
 

<table style="vertical-align:middle">
<tr>
<td align="Left">
    <form id="form2" runat="server">
    <div style="vertical-align:middle">
    <h2 style="text-align: center; font-family:Arial">Toll-Free Phone System Instructions</h2>
    <h3 style="text-align: center; font-family:Arial"><asp:Label ID="Label5" runat="server" Text="" Width="600px"></asp:Label></h3>
    <p style="font-family:Arial; font-size:smaller">
    Step 1         Dial Toll Free Number (800)-801-9270 or (866)-890-5003.<br />
    Step 2         Enter 4-digit Physician ID.<br />
    Step 3         Enter Password.<br />
    Step 4         Start dictating after you hear the beep.<br />
    </p>
  
<table>
<tr>
        <td>
                    <asp:Repeater id="nkeytbl" runat="server">
                    <HeaderTemplate>
                    <table border="1" width="100%" style="font-family:Arial; font-size:smaller">
                    <tr>
                    <td colspan="2" align="center" style="background-color:Navy;"><strong style="color:White">Keypad</strong></td>
                    </tr>
                    <tr>
                    <th>Key#</th>
                    <th>Description</th>
                    </tr>
                    </HeaderTemplate>
                    <ItemTemplate>
                    <tr>
                    <td><%#Container.DataItem("keyin")%></td>
                    <td><%#Container.DataItem("activity")%></td>
                    </tr>
                    </ItemTemplate>

                    <FooterTemplate>
                    </table>
                    </FooterTemplate>
                    </asp:Repeater>
        </td>


        <td valign="top">
                    <asp:Repeater id="nprompttb" runat="server">
                    <HeaderTemplate>
                    <table border="1" width="100%" style="font-family:Arial; font-size:smaller">
                    <tr>
                    <td colspan="3" align="center" style="background-color:Navy;"><strong style="color:White">Prompt</strong></td>
                    </tr>
                    <tr>
                    <th>Prompt</th>
                    <th>Key IN</th>
                    <th>Description</th>
                    </tr>
                    </HeaderTemplate>
                    <ItemTemplate>
                    <tr>
                    <td><%#Container.DataItem("prompt")%></td>
                    <td><%#Container.DataItem("keyin")%></td>
                    <td><%#Container.DataItem("description")%></td>
                    </tr>
                    </ItemTemplate>

                    <FooterTemplate>
                    </table>
                    </FooterTemplate>
                    </asp:Repeater>
           </td>

</tr>

<tr>
            <td valign="top">
                    <asp:Repeater id="naccttb" runat="server">
                    <HeaderTemplate>
                    <table border="1" width="100%" style="font-family:Arial; font-size:smaller">
                    <tr>
                    <td colspan="4" align="center" style="background-color:Navy;"><strong style="color:White">Physician ID and Password</strong></td>
                    </tr>
                    <tr>
                    <th>Last Name</th>
                    <th>First Name</th>
                    <th>ID</th>
                    <th>Password</th>
                    </tr>
                    </HeaderTemplate>
                    <ItemTemplate>
                    <tr>
                    <td><%#Container.DataItem("diclname")%></td>
                    <td><%#Container.DataItem("dicfname")%></td>
                    <td><%#Container.DataItem("id")%></td>
                    <td><%#Container.DataItem("password")%></td>
                    </tr>
                    </ItemTemplate>

                    <FooterTemplate>
                    </table>
                    </FooterTemplate>
                    </asp:Repeater>
            </td>
</tr>

</table>  
        <p style="font-size: small; font-family: Arial">
            NOTE: Whenever you decide to use the floating ID where a physician needs to dictate and a permanent ID has not yet been assigned, please ask the Dictator to identify his/her full name and credentials before dictating and send an email to support@medofficepro.com providing full name and credentials of the physician so that an appropriate signature block can be added to reports. Also, please note we typically require one business day (or 24 hrs) to create a new Physician ID and Password.</p>
        <asp:Button ID="btnprnt2" UseSubmitBehavior="false"  runat="server" Text="Print this page" OnClientClick="printpg2();return false;"/>
    </div>
    </form>
    
</td>
</tr>   
</table> 


</body>
</html>
