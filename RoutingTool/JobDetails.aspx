<%@ Page Language="VB" AutoEventWireup="false" CodeFile="JobDetails.aspx.vb" Inherits="RoutingTool_JobDetails" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 1.0 Transitional/EN">
<%@ Register
    Assembly="AjaxControlToolkit"
    Namespace="AjaxControlToolkit"
    TagPrefix="ajaxToolkit" %>

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
<link href= "../App_Themes/Css/styles.css" type="text/css" rel="stylesheet" />
<link href= "../App_Themes/Css/Common.css" type="text/css" rel="stylesheet" />
    <title>Job Details</title>
</head>
<body>
    <form id="form1" runat="server">
    <div style="border-right: navy thin solid; border-top: navy thin solid; border-left: navy thin solid; border-bottom: navy thin solid; text-align:left">
  <asp:Repeater ID="Repeater1" runat="server">
  <HeaderTemplate>
  </HeaderTemplate>
   <ItemTemplate>
   <table>
    <tr><td>
        <table id="tbl1">
        <tr>
        <td colspan="3" class="HeaderDiv" >Accounts Details
        </td>
        </tr>
        <tr>
        <td class="alt1">Account Name</TD>
        <td class="alt1">Account Number</TD>
        <td class="alt1">Contractor</TD>
        </tr>   
        <tr>
        <td class="common"><%#Container.DataItem("AccountName")%></td>
        <td class="common"><%#Container.DataItem("AccountNo")%></td>
        <td class="common"><%#Container.DataItem("ContractorName")%></td>
        </tr>        
        </table>
      </td>
      <td>
        <table id="Table1">
        <tr>
        <td colspan="3" class="HeaderDiv">Physicians Details
        </td>
        </tr>
        <tr>
        <td class="alt1">Physician Name</TD>
        <td class="alt1">PIN Number</TD>        
        <td class="alt1">Signature</TD>
        </tr> 
        <tr>
        <td class="common"><%#Container.DataItem("FirstName") & " " & Container.DataItem("LastName")%></td>
        <td class="common"><%#Container.DataItem("PINNo")%></td>        
        <td class="common"><%#Container.DataItem("SignedName")%></td>     
        </tr> 
        </table>
     </td></tr>   
   </table>
    <table>
        <tr><td colspan="8" class="HeaderDiv" > Job Details</td></tr>
        <tr>
        <td class="alt1">Job Number</TD>
        <td class="alt1">Customer Job#</TD>        
        <td class="alt1">Status</TD>
        <td class="alt1">UserName</TD>        
        <td class="alt1">Date Created</TD>
        <td class="alt1">TAT</TD>
        <td class="alt1">Date Dictated</TD>
        <td class="alt1">Remaining</TD>        
        </tr> 
        <tr>
        <td class="common"><%#Container.DataItem("JobNumber")%></td>
        <td class="common"><%#Container.DataItem("CustJobID")%></td>        
        <td class="common">
            <asp:DropDownList ID="ddlStatus" runat="server" cssclass="common" DataSourceID="SqlDataSource1" DataTextField="LevelName" DataValueField="LevelNo">        
    </asp:DropDownList><asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:ETSConnectionString %>"
       SelectCommand='<%#hdnLevelDropDown.value%>'>
        <SelectParameters>
            <asp:ControlParameter ControlID="hdnStatus" Name="LevelNo" PropertyName="Value" Type="decimal" />
        </SelectParameters>
    </asp:SqlDataSource>
           </td>     
        <td class="common"><%#Container.DataItem("UserName")%></td>        
        <td class="common"><%#Container.DataItem("DateCreated")%></td>        
        <td class="common"><asp:TextBox ID="txtTAT" runat="server" Text='<%#Container.DataItem("TAT")%>' MaxLength="4" Width="40px"></asp:TextBox></td>        
        <td class="common"><%#Container.DataItem("DateDictated")%></td>                
        <td class="common"><%#datediffToMe(Container.DataItem("TAT"), Container.DataItem("SubmitDate"))%></td>                        
        </tr> 
        <tr>
        <td colspan="4" align="center">Assign To:&nbsp&nbsp&nbsp<asp:TextBox ID="txtAltUserName" runat="server"></asp:TextBox>
        <%--<ajaxToolkit:AutoCompleteExtender ID="AutoCompleteExtender1" 
                                  MinimumPrefixLength="1" 
                                  CompletionSetCount="10" 
                                  runat="server" 
                                  TargetControlID="txtAltUserName"
                                  ServicePath="../users/autocomplete.asmx"
                                  ServiceMethod="GetUserID" EnableCaching="true"/>--%>
        </td>
        <td colspan="4" align="right"><asp:Button ID="btnSave" runat="server" CssClass="button" Text="Save Changes" OnClick="SaveChanges"/>
        
        </td>
        </tr>
    </ItemTemplate>  
    <FooterTemplate>
    </table>
    </FooterTemplate>
    </asp:Repeater>
    <hr style="border-right: navy thin solid; border-top: navy thin solid; border-left: navy thin solid; border-bottom: navy thin solid;" />                                
    <asp:Repeater ID="rptHistory" runat="server" > 
    <HeaderTemplate>  
    
        <table id="tbl1" >
        <tr><td colspan="9" class="HeaderDiv"> CheckOut Log (Newest entries at the top)</td></tr>
        <TR>
        <TD class="alt1">Status</TD>
        <TD class="alt1">User Name</TD>
        <TD class="alt1">Assigned By</TD>        
        <TD class="alt1">Modified Date</TD>
        <TD class="alt1">LineCount</TD>
        <TD class="alt1">Template Name</TD>
        <TD class="alt1">version</TD>
        <TD class="alt1">IP</TD>
        <TD class="alt1">Downloaded</TD>
        </TR>
          
    </HeaderTemplate>
    
    <ItemTemplate>
        <tr>
        <td class="common"><%#getStatus(CInt(Container.DataItem("Status")))%></td>
        <td class="common"><%#IIf(IsDBNull(Container.DataItem("UserName")), "-", Container.DataItem("UserName").ToString)%></td>
        <td class="common"><%#IIf(IsDBNull(Container.DataItem("AssignedBy")), "-", Container.DataItem("AssignedBy").ToString)%></td>
        <td class="common"><%#IIf(IsDBNull(Container.DataItem("DateModified")),"-",Container.DataItem("DateModified").ToString)%></td>
        <td class="common"><%#IIf(IsDBNull(Container.DataItem("LineCount")), "0", Container.DataItem("LineCount").ToString)%></td>
        <td class="common"><%#IIf(IsDBNull(Container.DataItem("TemplateName")), "-", Container.DataItem("TemplateName").ToString)%></td>
        <td class="common">      
<asp:HyperLink ID="HyperLink1"
NavigateUrl='<%#"ShowVersion.aspx?DocID=" & hdnTransID.Value & ".doc." & Container.DataItem("version").tostring%>'
Text='<%#IIf(IsDBNull(Container.DataItem("version")),"-",Container.DataItem("version"))%>'
Target="_blank"
runat="server" Visible='<%#IIf(IsDBNull(Container.DataItem("version")),false ,true)%>' />


        </td>
        <td class="common"><%#IIf(IsDBNull(Container.DataItem("IP")), "-", Container.DataItem("IP").ToString)%></td>
        <td class="common"><%#IIf(IsDBNull(Container.DataItem("Downloaded")), "False", Container.DataItem("Downloaded").ToString)%></td>
        </tr>
    </ItemTemplate>
     
    <FooterTemplate>    
     </Table> 
    </FooterTemplate>   
    </asp:Repeater>
    <hr style="border-right: navy thin solid; border-top: navy thin solid; border-left: navy thin solid; border-bottom: navy thin solid;" />    
    <table id="tbl1">         
        <tr><td class="HeaderDiv"> Demographic Information</td></tr>
    </table>  
        
    <asp:GridView ID="grdExtended" runat="server" AutoGenerateColumns="False">  
        <Columns>
            <asp:BoundField DataField="Caption" HeaderText="Field Name" ReadOnly="True" />
            <asp:BoundField DataField="Value" HeaderText="Field Value" />
        </Columns>
        <HeaderStyle CssClass="alt1" />
        <RowStyle cssclass="common"  />
        <EditRowStyle  cssclass="common" />
        <AlternatingRowStyle  cssclass="common" />    
    </asp:GridView>                       
    <asp:HiddenField ID="hdnTransID" runat="server" />
    <asp:HiddenField ID="hdnStatus" runat="server" />
    <asp:HiddenField ID="hdnSQL" runat="server" />    
    <asp:HiddenField ID="hdnLevelDropDown" runat="server" />
    <asp:HiddenField ID="hdnres" runat="server" />
    </div>
    <div style="text-align:left">
        <input id="Button1" class="button" type="button" value="<< Back" onclick="history.go(-1);" /> 
    </div>
        
    </form>
</body>
</html>
