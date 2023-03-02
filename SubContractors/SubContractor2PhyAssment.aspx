<%@ Page Language="VB" AutoEventWireup="false" CodeFile="SubContractor2PhyAssment.aspx.vb" Inherits="UserPhyAssgn_Default" EnableViewState="True" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 1.0 Transitional/EN">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Subcontractors to Physicians Assignment</title>
    <link href= "../App_Themes/Css/styles.css" type="text/css" rel="stylesheet" />
    <link href= "../App_Themes/Css/Common.css" type="text/css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <div id="body">
        <div id="cap"></div>
        <div id="main">
        <h1>SubContractor - Dictators Assignment</h1>
        <asp:Panel ID="Panel4" runat="server" HorizontalAlign="Left">
            <div>
        <table id="MainTable" width="80%">
        <tr>
                <td style="width: 100%; text-align: center;" class="HeaderDiv" valign="top" colspan ="2">
                    SubContractor - Dictators Assignment
                </td>
               </tr>
                
            <tr>
                <td style="width: 50%; text-align: center;" valign="top">
                    <asp:Panel ID="Panel1" runat="server" Width="100%">
                        <table width="100%" >
                        
                        
                            <tr style="text-align: center">
                                <td colspan="2" class="alt1" style="text-align: center">
                                    SubContractor Search</td>
                            </tr>
                            <tr>
                                <td class="common">
                                    SubContractor Name</td>
                                <td style="width: 3px">
                                    <asp:TextBox ID="TxtSubContractorname" CssClass="common" runat="server"></asp:TextBox></td>
                            </tr>
                            <tr>
                                <td style="text-align: center; height: 30px;" colspan="2">
                                    <asp:Button ID="BtnSubmit1" runat="server" CssClass="button" Text="Submit" UseSubmitBehavior="False" />
                                </td>
                            </tr>
                        </table>
                        </asp:Panel>
                    <asp:Panel ID="Panel2" runat="server" Width="100%">
                    <asp:Table ID="TblSubContractorSeach" runat="server" Width="100%">
                        <asp:TableRow ID="TableRow1" runat="server" style="text-align: center">
                            <asp:TableCell ID="TableCell1" runat="server" cssclass="alt1">&nbsp</asp:TableCell>
                            <asp:TableCell ID="TableCell2" runat="server" cssclass="alt1">SubContractor Name</asp:TableCell>
                        </asp:TableRow>
                        </asp:Table>
   
                    </asp:Panel>
                    <asp:Panel ID="Panel3" runat="server" Width="100%">
                    <asp:Table ID="TblSubcontractorstatus" runat="server" Width="100%">
                        <asp:TableRow ID="TableRow2" runat="server" style="text-align: center">
                            <asp:TableCell ID="TableCell3" runat="server" cssclass="alt1">SubContractor Name</asp:TableCell>
                        </asp:TableRow>
                    </asp:Table>
                        </asp:Panel>
                   
                </td>
                <td style="text-align: center" valign="top">
                    <asp:Panel ID="Panel5" runat="server" Width="100%">
                        <table width="100%">
                            <tr style="text-align: center">
                                <td colspan="2" class="alt1" style="text-align: center">
                                    Dictator Search</td>
                            </tr>
                            <tr>
                                <td style="width: 3px" class="common">
                                    Username</td>
                                <td style="width: 3px">
                                    <asp:TextBox ID="Txtpname" CssClass="common" runat="server"></asp:TextBox></td>
                            </tr>
                            <tr>
                                <td colspan="2" style="height: 30px; text-align: center">
                                    <asp:Button ID="BtnSubmit2" runat="server" Text="Submit" CssClass="button" UseSubmitBehavior="False" /></td>
                            </tr>
                        </table>
                    </asp:Panel>
                    <asp:Panel ID="Panel6" runat="server" Width="100%">
                        <asp:Table ID="Table1" runat="server" Width="100%">
                            <asp:TableRow ID="TableRow3" runat="server" style="text-align: center">
                                <asp:TableCell ID="TableCell4" runat="server" cssclass="alt1">&nbsp</asp:TableCell>
                                <asp:TableCell ID="TableCell5" runat="server" cssclass="alt1">First Name</asp:TableCell>
                                <asp:TableCell ID="TableCell6" runat="server" cssclass="alt1">Last Name</asp:TableCell>
                                <asp:TableCell ID="TableCell7" runat="server" cssclass="alt1">Username</asp:TableCell>
                            </asp:TableRow>
                        </asp:Table>
                    </asp:Panel>
                    <asp:Panel ID="Panel7" runat="server" Width="100%">
                        <asp:Table ID="Table3" runat="server" Width="100%">
                            <asp:TableRow ID="TableRow4" runat="server" cssclass="SMSelected" style="text-align: center">
                                <asp:TableCell ID="TableCell8" runat="server" cssclass="alt1">First Name</asp:TableCell>
                                <asp:TableCell ID="TableCell9" runat="server" cssclass="alt1">Last Name</asp:TableCell>
                                <asp:TableCell ID="TableCell10" runat="server" cssclass="alt1">Username</asp:TableCell>
                            <asp:TableCell ID="TableCell15" runat="server" cssclass="alt1">Pin</asp:TableCell>
                            </asp:TableRow>
                        </asp:Table>
                    </asp:Panel>
                    </td>
            </tr>
            <tr>
                <td colspan="2" style="text-align: center">
                    <asp:Button ID="BtnAssign" runat="server" CssClass="button" Text="Submit" /></td>
            </tr>
            <tr>
                <td colspan="2" style="text-align: center">
                    <asp:Table ID="Table4" runat="server" Width="100%">
                        <asp:TableRow ID="TableRow5" runat="server" style="text-align: center">
                            <asp:TableCell ID="TableCell11" runat="server" cssclass="alt1">SubContractor Name</asp:TableCell>
                            <asp:TableCell ID="TableCell12" runat="server" cssclass="alt1">Dictator Name</asp:TableCell>
                            <asp:TableCell ID="TableCell13" runat="server" cssclass="alt1">Username</asp:TableCell>
                            <asp:TableCell ID="TableCell14" runat="server" cssclass="alt1">Account Name</asp:TableCell>
                        </asp:TableRow>
                    </asp:Table>
                </td>
            </tr>
        </table>
        <br />
        <asp:Label ID="DispBox" runat="server" Font-Bold="True" Font-Names="Trebuchet MS"
            Font-Size="Small" ForeColor="#C00000"></asp:Label></div>
        <asp:HiddenField ID="SubContractorState" runat="server" />
        <asp:HiddenField ID="PhyState" runat="server" />
        <asp:HiddenField ID="HSubcontractorID" runat="server" />
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
