<%@ Page Language="VB" AutoEventWireup="false" CodeFile="JobHistory.aspx.vb" Inherits="Dictation_Search_JobHistory" EnableViewState="true" EnableViewStateMac="false" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Job History</title>
    <link href= "../App_Themes/Css/styles.css" type="text/css" rel="stylesheet" />
    <link href= "../App_Themes/Css/Common.css" type="text/css" rel="stylesheet" />
    <script type="text/javascript" language="javascript">
//        var oMyObject = window.dialogArguments;
//        //document.getElementById('hdnTransID').value = oMyObject.TransID;
//        //document.getElementById('hdnStatus').value = oMyObject.Status;
//        
//        alert(oMyObject.TransID);
//        alert(oMyObject.Status);
//        function GetValues()
//        {
//            document.getElementById('hdnTransID').value = oMyObject.TransID;
//            document.getElementById('hdnStatus').value = oMyObject.Status;
//            
//            alert("After : " + document.getElementById('hdnTransID').value);
//            alert("After : " +document.getElementById('hdnStatus').value);

//        }
    </script>
</head>
<body>
    <form id="form1"  runat="server">
    <div>
        <asp:Panel ID="pnlData" runat="server">
            <table width="100%" border="0">
                <tr>
                    <td style="width:50%; border:0">
                        <table id="tbl1" width="100%">
                            <tr>
                                <td colspan="3" class="HeaderDiv">
                                    Accounts Details
                                </td>
                            </tr>
                            <tr>
                                <td class="alt1">Account Name</td>
                                <td class="alt1">Account Number</td>
                                <td class="alt1">Contractor</td>
                            </tr>           
                            <tr>
                                <td><asp:Label ID="lblAccName" CssClass="common" runat="server" Text=""></asp:Label></td>
                                <td><asp:Label ID="lblAccNo" CssClass="common" runat="server" Text=""></asp:Label></td>
                                <td><asp:Label ID="lblContractorName" CssClass="common" runat="server" Text="Label"></asp:Label></td>
                            </tr>        
                        </table>
                    </td>
                    <td style="width:50%; border:0">
                        <table id="Table1" width="100%">
                            <tr>
                                <td colspan="3" class="HeaderDiv" >Physicians Details</td>
                            </tr>
                            <tr>
                                <td class="alt1">Physician Name</td>
                                <td class="alt1">PIN Number</td>        
                                <td class="alt1">Signature</td>
                            </tr> 
                            <tr>
                                <td><asp:Label ID="lblDictatorName" CssClass="common" runat="server" Text=""></asp:Label></td>
                                <td><asp:Label ID="lblPINNo" CssClass="common" runat="server" Text=""></asp:Label></td>
                                <td><asp:Label ID="lblSignedName" CssClass="common" runat="server" Text=""></asp:Label></td>
                            </tr> 
                        </table>
                    </td>
                 </tr>
                 <tr>
                    <td colspan="2" style="text-align:left; border:0">
                        <table width="100%">
                            <tr><td class="HeaderDiv" colspan="8">Job Details</td></tr>
                            <tr>
                                <td class="alt1">Job Number</td>
                                <td class="alt1">Customer Job#</td>        
                                <td class="alt1">Status</td>
                                <td class="alt1">UserName</td>        
                                <td class="alt1">Date Created</td>
                                <td class="alt1">TAT</td>
                                <td class="alt1">Date Dictated</td>
                                <td class="alt1">Remaining</td>        
                            </tr> 
                            <tr>
                                <td><asp:Label ID="lblJobNo" runat="server" Text="" CssClass="common"></asp:Label></td>
                                <td><asp:Label ID="lblCustJobNo" runat="server" Text="" CssClass="common"></asp:Label></td>  
                                <td><asp:DropDownList ID="ddlStatus" runat="server" CssClass="common"></asp:DropDownList></td> 
                                <td><asp:Label ID="lblUserName" runat="server" CssClass="common"></asp:Label></td>        
                                <td><asp:Label ID="lblDtCreated" runat="server" Text="" CssClass="common"></asp:Label></td>        
                                <td><asp:TextBox ID="txtTAT" runat="server" Text="" MaxLength="4" Width="40px"></asp:TextBox></td>        
                                <td><asp:Label ID="lblDtDictated" runat="server" Text=""></asp:Label></td>                
                                <td><asp:Label ID="lblRemaining" runat="server" Text=""></asp:Label></td>                        
                            </tr> 
                            <tr>
                                <td colspan="8" align="left">
                                    Assign To:&nbsp&nbsp&nbsp<asp:TextBox ID="txtAltUserName" CssClass="common" runat="server"></asp:TextBox>
                                    &nbsp; &nbsp; &nbsp;<asp:Button ID="btnSave" CssClass="button"  runat="server" Text="Save Changes" />
                                </td>
                            </tr>
                        </table>
                    </td>
                 </tr>
                 <tr>
                    <td colspan="2" style="border:0">
                        <asp:Repeater ID="rptHistory" runat="server" > 
                            <HeaderTemplate>  
                                <table id="tbl1">
                                    <tr><td colspan="9" style="border:0"><b>CheckOut Log (Newest entries at the top)</b></td></tr>
                                    <tr>
                                        <td class="alt1">Status</td>
                                        <td class="alt1">User Name</td>
                                        <td class="alt1">Assigned By</td>        
                                        <td class="alt1">Modified Date</td>
                                        <td class="alt1">LineCount</td>
                                        <td class="alt1">Template Name</td>
                                        <td class="alt1">version</td>
                                        <td class="alt1">IP</td>
                                        <td class="alt1">Downloaded</td>
                                    </tr>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <tr>
                                    <td><%#getStatus(CInt(Container.DataItem("Status")))%></td>
                                    <td><%#IIf(IsDBNull(Container.DataItem("UserName")), "-", Container.DataItem("UserName").ToString)%></td>
                                    <td><%#IIf(IsDBNull(Container.DataItem("AssignedBy")), "-", Container.DataItem("AssignedBy").ToString)%></td>
                                    <td><%#IIf(IsDBNull(Container.DataItem("DateModified")),"-",Container.DataItem("DateModified").ToString)%></td>
                                    <td><%#IIf(IsDBNull(Container.DataItem("LineCount")), "0", Container.DataItem("LineCount").ToString)%></td>
                                    <td><%#IIf(IsDBNull(Container.DataItem("TemplateName")), "-", Container.DataItem("TemplateName").ToString)%></td>
                                    <td><asp:HyperLink ID="HyperLink1" NavigateUrl='<%#"ShowVersion.aspx?DocID=" & hdnTransID.Value & ".doc." & Container.DataItem("version").tostring%>' Text='<%#IIf(IsDBNull(Container.DataItem("version")),"-",Container.DataItem("version"))%>' Target="_blank" runat="server" Visible='<%#IIf(IsDBNull(Container.DataItem("version")),false ,true)%>' /></td>
                                    <td><%#IIf(IsDBNull(Container.DataItem("IP")), "-", Container.DataItem("IP").ToString)%></td>
                                    <td><%#IIf(IsDBNull(Container.DataItem("Downloaded")), "False", Container.DataItem("Downloaded").ToString)%></td>
                                </tr>
                            </ItemTemplate>
                            <FooterTemplate>    
                             </Table> 
                            </FooterTemplate>   
                        </asp:Repeater>
                    </td>
                 </tr>
                 <tr>
                    <td colspan="2" style="border:0">
                        <table>
                            <tr>
                                <td class="alt1" style="border:0">
                                    Demographic Information
                                </td>
                            </tr>
                            <tr>
                                <td style="width:100%; border:0">
                                    <asp:GridView ID="grdExtended" runat="server" AutoGenerateColumns="False" Width="100%">  
                                        <Columns>
                                            <asp:BoundField DataField="Caption" HeaderText="Field Name" ReadOnly="True" HeaderStyle-CssClass="alt1" />
                                            <asp:BoundField DataField="Value" HeaderText="Field Value" HeaderStyle-CssClass="alt1" />
                                        </Columns>
                                    </asp:GridView>
                                </td>
                            </tr>
                        </table>
                    </td>
                 </tr>
            </table>
        </asp:Panel>
        <asp:Panel ID="pnlMsg" runat="server" HorizontalAlign="Left" Visible="false">
            <center>
                <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label>
                <br /><br />
                <asp:Button ID="btnClose" runat="server" Text="Close Window" CssClass="button" OnClientClick="javascript:opener.window.location.reload();window.close();" />
            </center>
        </asp:Panel>
        <asp:HiddenField ID="hdnTransID" runat="server" />
        <asp:HiddenField ID="hdnStatus" runat="server" />
        <asp:HiddenField ID="hdnSQL" runat="server" />    
        <asp:HiddenField ID="hdnLevelDropDown" runat="server" />
        <asp:HiddenField ID="hdnres" runat="server" />
        <asp:HiddenField ID="hdnAccName" runat="server" />
        <asp:HiddenField ID="hdnAccNo" runat="server" />
        <asp:HiddenField ID="hdnContractorName" runat="server" />
        <asp:HiddenField ID="hdnDictatorName" runat="server" />
        <asp:HiddenField ID="hdnPinNo" runat="server" />
        <asp:HiddenField ID="hdnSignedName" runat="server" />
	</div>
    </form>
</body>
</html>
