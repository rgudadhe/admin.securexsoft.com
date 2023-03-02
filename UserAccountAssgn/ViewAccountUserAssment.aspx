<%@ Page Language="VB" AutoEventWireup="false" CodeFile="ViewAccountUserAssment.aspx.vb" Inherits="UserPhyAssgn_Default" EnableViewState="True" %>





<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 1.0 Transitional/EN">
<LINK href= "../../styles/Default.css" type="text/css" rel="stylesheet">
<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Untitled Page</title>
    <script type="text/javascript" language="javascript">
var newwindow;
function poptastic(inpt)
{
    url="removea.aspx?TrackID="+ inpt;
    //alert(inpt);
    
	newwindow=window.open(url,'name','height=100,width=400, left=300, top=100');
	if (window.focus) {newwindow.focus()}
}

 function ChkLvl()
{
//alert('Hello');
var J = 0;
//alert('Hello');
for (counter = 1; counter < document.form1.elements.length ; counter++)
{

if (document.form1.elements[counter].checked && document.form1.elements[counter].name.substring(0,7) == 'LevelNo')
{ 
J = J + 1; 
}
}

if (J == 0 )
{
alert("Please select level!");
return false;
}

}
</script>   
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <table id="MainTable" width="80%" style="font-size: 10pt; font-family: 'Trebuchet MS'; font-style: italic; color:Gray; " border ="2" cellpadding ="2" cellspacing ="2">
        <tr>
                <td style="width: 100%; text-align: center;" valign="top" colspan ="1" class="HeaderDiv">
                    <span style="font-family: Trebuchet MS; color: white;"><strong><em>View User
                        Account Assignment</em></strong></span></td>
               </tr>
                
            <tr>
                <td style="width: 50%; text-align: center;" valign="top">
                    <asp:Panel ID="Panel1" runat="server" Width="100%">
                        <table border="2" cellpadding="2" cellspacing ="2" style="color: gray; font-style: italic; font-family: 'Trebuchet MS'" width="50%" >
                        
                   
                            <tr class="SMSelected" style="text-align: center">
                                <td colspan="2" style="text-align: center">
                                    User Search</td>
                            </tr>
                            <tr>
                                <td style="text-align: right" >
                                    Username</td>
                                <td style="text-align: left" >
                                    <asp:TextBox ID="TxtUname" runat="server"></asp:TextBox></td>
                            </tr>
                            <tr>
                                <td style="text-align: center; height: 30px;" colspan="2">
                                    <asp:Button ID="BtnSubmit1" runat="server" Text="Submit" UseSubmitBehavior="False" />
                                </td>
                            </tr>
                        </table>
                        </asp:Panel>
                    <asp:Panel ID="Panel2" runat="server" Width="100%"><asp:Table ID="UserTable" runat="server" BorderColor="Silver" CellPadding="2" CellSpacing="2" Font-Italic="True" Font-Names="Trebuchet MS" ForeColor="DimGray" BorderWidth="2px" GridLines="Both" Width="100%" Font-Size="Small">
                        
                        <asp:TableRow runat="server" cssclass="SMSelected" style="text-align: center">
                            <asp:TableCell runat="server"></asp:TableCell>
                            <asp:TableCell runat="server">First Name</asp:TableCell>
                            <asp:TableCell runat="server">Last Name</asp:TableCell>
                            <asp:TableCell runat="server">Username</asp:TableCell>
                        </asp:TableRow>
                        </asp:Table>
   
                    </asp:Panel>
                    <asp:Panel ID="Panel3" runat="server" Width="100%"><asp:Table ID="TblUserstatus" runat="server" BorderColor="Silver" CellPadding="2" CellSpacing="2" Font-Italic="True" Font-Names="Trebuchet MS" ForeColor="DimGray" BorderWidth="2px" GridLines="Both" Width="100%" Font-Size="Small">
                      
                        <asp:TableRow runat="server" cssclass="SMSelected" style="text-align: center">
                            <asp:TableCell runat="server"></asp:TableCell>
                            <asp:TableCell runat="server">Production Level</asp:TableCell>
                        </asp:TableRow>
                    </asp:Table>
                        </asp:Panel>
                    
                </td>
            </tr>
            <tr>
                <td colspan="1" style="text-align: center">
                    <asp:Table  ID="Table4" runat="server" BorderColor="Silver" CellPadding="2" CellSpacing="2" Font-Italic="True" Font-Names="Trebuchet MS" ForeColor="DimGray" BorderWidth="2px" GridLines="Both" Width="100%" Font-Size="Small">
                        <asp:TableRow runat="server" cssclass="SMSelected" style="text-align: center">
                            <asp:TableCell runat="server">User Name</asp:TableCell>
                            <asp:TableCell runat="server">Level</asp:TableCell>
                            <asp:TableCell runat="server">Account Name</asp:TableCell>
                            <asp:TableCell runat="server">Account Number</asp:TableCell>
                           <asp:TableCell ID="TableCell1" runat="server">Remove</asp:TableCell> 
                        </asp:TableRow>
                    </asp:Table>
                </td>
            </tr>
        </table>
    
    </div>
        <asp:Label ID="MsgDisp" runat="server" Font-Bold="True" Font-Names="Trebuchet MS"
            Font-Size="Small" ForeColor="#C00000" Height="16px" Width="496px"></asp:Label><br />
        <asp:HiddenField ID="PrdState" runat="server" />
        <asp:HiddenField ID="PhyState" runat="server" />
        <asp:HiddenField ID="HUserID" runat="server" />
        <asp:HiddenField ID="hUname" runat="server" />
        <asp:HiddenField ID="TotPhy" runat="server" />
        <asp:HiddenField ID="TotLvl" runat="server" />
        <br />
        <br />
        <asp:Button ID="btnSubmit4" runat="server" Text="Submit" Visible="False" />
        
        <asp:Button ID="BtnSubmit5" runat="server" Text="Submit" Visible="False"  OnClientClick="return ChkLvl();" />
        <asp:Button ID="btnsubmit3" runat="server" Text="Submit" Visible="False" />
    </form>
</body>
</html>
