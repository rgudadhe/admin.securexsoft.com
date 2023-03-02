<%@ Page Language="VB" AutoEventWireup="false" CodeFile="ViewPhyUserAssment.aspx.vb" Inherits="UserPhyAssgn_Default" EnableViewState="True" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 1.0 Transitional/EN">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>View User Assignments</title>
    <link href= "../App_Themes/Css/styles.css" type="text/css" rel="stylesheet" />
    <link href= "../App_Themes/Css/Common.css" type="text/css" rel="stylesheet" />
      <script type="text/javascript" language="javascript">
var newwindow;
function poptastic(inpt)
{
    url="removep.aspx?TrackID="+ inpt;
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
        <div id="body">
        <div id="cap"></div>
        <div id="main">
        <h1>View/Edit User Assments</h1>
        <asp:Panel ID="Panel4" runat="server" HorizontalAlign="Left">
            <div>
        <table id="MainTable" width="100%">
        <tr>
                <td style="width: 100%; text-align: center;" valign="top" colspan ="1" class="HeaderDiv">
                    View/Edit User Assignments 
               </td>
               </tr>
                
            <tr>
                <td style="width: 50%; text-align: center;" valign="top">
                    <asp:Panel ID="Panel1" runat="server" Width="100%" HorizontalAlign="Left">
                        <table width="50%" >
                        
                   
                            <tr style="text-align: center">
                                <td colspan="2" style="text-align: center" class="alt1">
                                    User Search</td>
                            </tr>
                            <tr>
                                <td style="text-align: left" >
                                    Username</td>
                                <td style="text-align: left" >
                                    <asp:TextBox ID="TxtUname" runat="server"></asp:TextBox></td>
                            </tr>
                           <tr>
                                <td  style="text-align: left" >
                                    First name</td>
                                <td  style="text-align: left" >
                                    <asp:TextBox ID="TxtFname" runat="server"></asp:TextBox></td>
                            </tr>
                           <tr>
                                <td  style="text-align: left" >
                                    Last name</td>
                                <td  style="text-align: left">
                                    <asp:TextBox ID="TxtLname" runat="server"></asp:TextBox></td>
                            </tr>  
                            <tr>
                                <td style="text-align: center; height: 30px;" colspan="2">
                                    <asp:Button CssClass="button"  ID="BtnSubmit1" runat="server" Text="Submit" UseSubmitBehavior="False" />
                                </td>
                            </tr>
                        </table>
                        </asp:Panel>
                    <asp:Panel ID="Panel2" runat="server" Width="100%" HorizontalAlign="Left">
                    <asp:Table ID="UserTable" runat="server" Width="100%" >
                        
                        <asp:TableRow ID="TableRow1" runat="server" style="text-align: center">
                            <asp:TableCell ID="TableCell1" CssClass="alt1" runat="server">&nbsp</asp:TableCell>
                            <asp:TableCell ID="TableCell2" CssClass="alt1" runat="server">First Name</asp:TableCell>
                            <asp:TableCell ID="TableCell3" CssClass="alt1" runat="server">Last Name</asp:TableCell>
                            <asp:TableCell ID="TableCell4" CssClass="alt1" runat="server">Username</asp:TableCell>
                        </asp:TableRow>
                        </asp:Table>
   
                    </asp:Panel>
                    <asp:Panel ID="Panel3" runat="server" Width="100%"><asp:Table ID="TblUserstatus" runat="server" Width="100%" >
                        
                        <asp:TableRow ID="TableRow2" runat="server" style="text-align: center">
                            <asp:TableCell ID="TableCell5" CssClass="alt1" runat="server">&nbsp</asp:TableCell>
                            <asp:TableCell ID="TableCell6" CssClass="alt1" runat="server">User Role</asp:TableCell>
                        </asp:TableRow>
                    </asp:Table>
                        </asp:Panel>
                    
                </td>
            </tr>
            <tr>
                <td colspan="1" style="text-align: center">
                    <asp:Table  ID="Table4" runat="server" Width="100%" >
                        <asp:TableRow ID="TableRow3" runat="server" cssclass="" style="text-align: center">
                    
                        </asp:TableRow>
                    </asp:Table>
                </td>
            </tr>
        </table>
        <asp:Label ID="MsgDisp" runat="server" Font-Bold="True" 
             ForeColor="#C00000" Height="16px" Width="496px"></asp:Label></div>
             <asp:HiddenField ID="PrdState" runat="server" />
        <asp:HiddenField ID="PhyState" runat="server" />
        <asp:HiddenField ID="HUserID" runat="server" />
        <asp:HiddenField ID="hUname" runat="server" />
        <asp:HiddenField ID="TotPhy" runat="server" />
        <asp:HiddenField ID="TotLvl" runat="server" />
         <asp:HiddenField ID="HDirLevel" runat="server" />
        <br />
        <br />
        <asp:Button CssClass="button"  ID="btnSubmit4" runat="server" Text="Submit" Visible="False" />
        
        <asp:Button CssClass="button"  ID="BtnSubmit5" runat="server" Text="Submit" Visible="False"  OnClientClick="return ChkLvl();" />
        <asp:Button CssClass="button"  ID="btnsubmit3" runat="server" Text="Submit" Visible="False" />
        </asp:Panel>
        </div> 
        </div> 
    </form>
</body>
</html>
