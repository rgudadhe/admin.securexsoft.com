<%@ Page Language="VB" AutoEventWireup="false" CodeFile="ViewSubContractorAccountAssment.aspx.vb" Inherits="UserPhyAssgn_Default" EnableViewState="True" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 1.0 Transitional/EN">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>View Subcontractors Account Assment</title>
    <link href= "../App_Themes/Css/styles.css" type="text/css" rel="stylesheet"/>   
    <link href= "../App_Themes/Css/Common.css" type="text/css" rel="stylesheet"/>   
    <link href= "../App_Themes/Css/Main.css" type="text/css" rel="stylesheet"/>   
   <script type="text/javascript" language="javascript">
var newwindow;
function poptastic(inpt)
{
    url="removea.aspx?ContrID="+ inpt;
    //alert(inpt);
    
	newwindow=window.open(url,'name','height=100,width=400, left=300, top=100');
	if (window.focus) {newwindow.focus()}
}

</script>  
</head>
<body>
    <form id="form1" runat="server">
        <div id="body">
        <div id="cap"></div>
        <div id="main">
        <h1>View SubContractor - Accounts Assignment</h1>
            <asp:Panel ID="Panel3" runat="server" HorizontalAlign="Left">
                <div>
        <table id="MainTable" width="80%">
        <tr>
                <td style="width: 100%; text-align: center;" valign="top" colspan ="1" class="HeaderDiv">
                    View SubContractor - Account Assignment</td>
               </tr>
            <tr>
                <td style="width: 50%; text-align: center;" valign="top">
                    <asp:Panel ID="Panel1" runat="server" Width="100%">
                        <table width="50%" >
                            <tr  style="text-align: center">
                                <td colspan="2" class="alt1" style="text-align: center">
                                    SubContractor Search</td>
                            </tr>
                            <tr>
                                <td class="common" style="text-align: right" >
                                    SubContractor Name</td>
                                <td class="common" style="text-align: left" >
                                    <asp:TextBox ID="TxtUname" runat="server"></asp:TextBox></td>
                            </tr>
                            <tr>
                                <td style="text-align: center; height: 30px;" colspan="2">
                                    <asp:Button ID="BtnSubmit1" runat="server" Text="Submit" CssClass="button" UseSubmitBehavior="False" />
                                </td>
                            </tr>
                        </table>
                        </asp:Panel>
                    <asp:Panel ID="Panel2" runat="server" Width="100%">
                        <asp:Table ID="UserTable" runat="server" Width="100%">
                        <asp:TableRow ID="TableRow1" runat="server" style="text-align: center">
                            <asp:TableCell ID="TableCell1" runat="server" cssclass="alt1">&nbsp</asp:TableCell>
                            <asp:TableCell ID="TableCell2" runat="server" cssclass="alt1">SubContractor Name</asp:TableCell>
                             <asp:TableCell ID="TableCell3" runat="server" cssclass="alt1">Description</asp:TableCell> 
                          </asp:TableRow>
                        </asp:Table>
                       </asp:Panel>
                       
                </td>
            </tr>
            <tr>
                <td colspan="1" style="text-align: center">
                    <asp:Table  ID="Table4" runat="server" Width="100%">
                        <asp:TableRow ID="TableRow2" runat="server" style="text-align: center">
                            <asp:TableCell ID="TableCell4" runat="server" cssclass="alt1">User Name</asp:TableCell>
                            <asp:TableCell ID="TableCell5" runat="server" cssclass="alt1">Level</asp:TableCell>
                            <asp:TableCell ID="TableCell6" runat="server" cssclass="alt1">Account Name</asp:TableCell>
                            <asp:TableCell ID="TableCell7" runat="server" cssclass="alt1">Account Number</asp:TableCell>
                           <asp:TableCell ID="TableCell8" runat="server" cssclass="alt1">Remove</asp:TableCell> 
                        </asp:TableRow>
                    </asp:Table>
                </td>
            </tr>
        </table>
    
    </div>
            <asp:Label ID="MsgDisp" runat="server" CssClass="Title" ForeColor="#C00000" Height="16px" Width="496px"></asp:Label><br />
        <asp:HiddenField ID="PrdState" runat="server" />
        <asp:HiddenField ID="PhyState" runat="server" />
        <asp:HiddenField ID="HcontrID" runat="server" EnableViewState="False" />
        <asp:HiddenField ID="hUname" runat="server" />
        <asp:HiddenField ID="TotPhy" runat="server" />
        <asp:HiddenField ID="TotLvl" runat="server" />
        <br />
        <br />
        <asp:Button ID="btnSubmit4" CssClass="button" runat="server" Text="Submit" Visible="False" />
        
        <asp:Button ID="BtnSubmit5" CssClass="button" runat="server" Text="Submit" Visible="False" />
        <asp:Button ID="btnsubmit3" CssClass="button" runat="server" Text="Submit" Visible="False" />

            </asp:Panel>
    
        </div> 
        </div> 
    </form>
    
</body>
</html>
