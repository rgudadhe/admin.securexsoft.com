<%@ Page Language="VB" AutoEventWireup="false" CodeFile="GroupBillAccountAssment.aspx.vb" Inherits="UserPhyAssgn_Default" EnableViewState="True" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 1.0 Transitional/EN">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Billing Group Account Assignment</title>
    <link href= "../App_Themes/Css/Main.css" type="text/css" rel="stylesheet"/>
    <link href= "../App_Themes/Css/Common.css" type="text/css" rel="stylesheet"/>
    <link href= "../App_Themes/Css/Styles.css" type="text/css" rel="stylesheet"/>
</head>
<body>
    <form id="form1" runat="server">
        <div id="body">
        <div id="cap"></div>
        <div id="main">
        <h1>Billing Group Account Assignment</h1>
    <div>
        <table width="100%">
        <tr>
                <td style="width: 100%; text-align: center;" class="HeaderDiv" valign="top" colspan ="2">
                    Billing Group Account - Accounts Assignment</td>
               </tr>
                
            <tr>
                <td style="width: 50%; text-align: center;" valign="top">
                    <asp:Panel ID="Panel1" runat="server" Width="100%">
                        <table width="100%" >
                        
                        
                            <tr style="text-align: center">
                                <td colspan="2" class="alt1" style="text-align: center">
                                    Group Search</td>
                            </tr>
                            <tr>
                                <td>
                                    Billing Group Account Name</td>
                                <td style="width: 3px">
                                    <asp:TextBox ID="TxtGrpname" runat="server"></asp:TextBox></td>
                            </tr>
                            <tr>
                                <td style="text-align: center; height: 30px;" colspan="2">
                                    <asp:Button CssClass="button"  ID="BtnSubmit1" runat="server" Text="Submit" UseSubmitBehavior="False" />&nbsp;
                                </td>
                            </tr>
                        </table>
                        </asp:Panel>
                    <asp:Panel ID="Panel2" runat="server" Width="100%"><asp:Table ID="TblGrpSeach" runat="server" Width="100%" >
                      
                        <asp:TableRow runat="server" style="text-align: center">
                            <asp:TableCell CssClass="alt1" runat="server">&nbsp;</asp:TableCell>
                            <asp:TableCell CssClass="alt1" runat="server">Billing Group Account Name</asp:TableCell>
                            <asp:TableCell CssClass="alt1" runat="server">Billing Group Account Number</asp:TableCell>
                        </asp:TableRow>
                        </asp:Table>
   
                    </asp:Panel>
                    <asp:Panel ID="Panel3" runat="server" Width="100%"><asp:Table ID="TblGrpstatus" runat="server" Width="100%">
                        
                        <asp:TableRow runat="server" style="text-align: center">
                            <asp:TableCell CssClass="alt1" runat="server">Billing Group Account Name</asp:TableCell>
                            <asp:TableCell CssClass="alt1" runat="server">Billing Group Account Number</asp:TableCell>
                        </asp:TableRow>
                    </asp:Table>
                        </asp:Panel>
                   
                </td>
                <td style="text-align: center" valign="top">
                    <asp:Panel  ID="Panel5" runat="server" Width="100%">
                    <table width="100%">
                    
                        <tr style="text-align: center">
                            <td class="alt1"  colspan="2" style="text-align: center">
                                Account Search</td>
                        </tr>
                        <tr>
                            <td style="width: 7px">
                                Accountname</td>
                            <td style="width: 3px">
                                <asp:TextBox ID="TxtAname" runat="server"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td style="text-align: center; height: 30px;" colspan="2">
                                <asp:Button CssClass="button" ID="BtnSubmit2" runat="server" Text="Submit" UseSubmitBehavior="False" /></td>
                        </tr>
                    </table>
                        </asp:Panel>
                    <asp:Panel ID="Panel6" runat="server" Width="100%"><asp:Table ID="Table1" runat="server" Width="100%">
                      
                        <asp:TableRow runat="server" style="text-align: center" >
                            <asp:TableCell CssClass="alt1"  runat="server">&nbsp</asp:TableCell>
                            <asp:TableCell CssClass="alt1" runat="server">Account Name</asp:TableCell>
                            <asp:TableCell CssClass="alt1" runat="server">Account Number</asp:TableCell>
                        </asp:TableRow>
                    </asp:Table>
                    </asp:Panel>
                    <asp:Panel ID="Panel7" runat="server" Width="100%"><asp:Table ID="Table3" runat="server" Width="100%">
                        <asp:TableRow runat="server" style="text-align: center">
                            <asp:TableCell CssClass="alt1" runat="server">&nbsp</asp:TableCell>
                            <asp:TableCell CssClass="alt1" runat="server">Account Name</asp:TableCell>
                            <asp:TableCell CssClass="alt1" runat="server">Account Number</asp:TableCell>
                        </asp:TableRow>
                    </asp:Table>
                        </asp:Panel>
                </td>
            </tr>
            <tr>
                <td colspan="2" style="text-align: center">
                    <asp:Button ID="BtnAssign" CssClass="button" runat="server" Text="Submit" /></td>
            </tr>
            <tr>
                <td colspan="2" style="text-align: center">
                    <asp:Table ID="Table4" runat="server" Width="100%">
                        <asp:TableRow runat="server" style="text-align: center">
                            <asp:TableCell CssClass="alt1" runat="server">Billing Group Account Name</asp:TableCell>
                            <asp:TableCell CssClass="alt1" runat="server">Billing Group Account Number</asp:TableCell>
                            <asp:TableCell CssClass="alt1" runat="server">Account Name</asp:TableCell>
                            <asp:TableCell CssClass="alt1" runat="server">Account Number</asp:TableCell>
                        </asp:TableRow>
                    </asp:Table>
                </td>
            </tr>
        </table>
    
    </div>
        <asp:HiddenField ID="GrpActState" runat="server" />
        <asp:HiddenField ID="ActState" runat="server" />
        <asp:HiddenField ID="HGrpActID" runat="server" />
        <asp:HiddenField ID="HGrpActName" runat="server" />
        <asp:HiddenField ID="hUname" runat="server" />
        <asp:HiddenField ID="TotAct" runat="server" />
        <asp:HiddenField ID="TotLvl" runat="server" />
        <br />
        <br />
        <asp:Label ID="MsgDisp" runat="server" CssClass="Title"  ForeColor="#C00000" Height="16px" Width="496px"></asp:Label>
        <asp:Button ID="btnSubmit4" CssClass="button" runat="server" Text="Submit" Visible="False" />
       &nbsp;&nbsp;
        <asp:Button ID="BtnSubmit5" CssClass="button" runat="server" Text="Submit" Visible="False" />
        <asp:Button ID="btnsubmit3" CssClass="button" runat="server" Text="Submit" Visible="False" />
        </div> 
        </div> 
       <%--  <asp:RegularExpressionValidator  Display="None" 
    id="RegTxtGrpname"  
    runat="server" 
    ControlToValidate="TxtGrpname" 
    ValidationExpression="^[a-zA-Z-]+%+$"
    ErrorMessage="Billing Group Account Name - Please enter valid input."
   />

 <asp:RegularExpressionValidator  Display="None" 
    id="RegTxtAname"  
    runat="server" 
    ControlToValidate="TxtAname" 
    ValidationExpression="^[a-zA-Z-]+%+$"
    ErrorMessage="Account Name - Please enter valid input."
   />--%>
        <asp:ValidationSummary ID="ValidationSummary1" runat="server" HeaderText="Following error occurs:" ShowMessageBox="true" DisplayMode="BulletList" ShowSummary="False" />      

    </form>
</body>
</html>
