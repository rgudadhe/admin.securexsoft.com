<%@ Control Language="vb" AutoEventWireup="false" Inherits="HierarGridDemoVB.Authors" CodeFile="History.ascx.vb" %>
<%@ Register
    Assembly="AjaxControlToolkit"
    Namespace="AjaxControlToolkit"
    TagPrefix="ajaxToolkit" %>
    
<link href= "../styles/Default.css" type="text/css" rel="stylesheet" />

<%--<div style="WIDTH: 100%; POSITION: relative; HEIGHT: 32px; left: 0px; top: 0px; border-right: navy thin solid; border-top: navy thin solid; border-left: navy thin solid; border-bottom: navy thin solid;" >--%>
<div style="border-right: navy thin solid; border-top: navy thin solid; border-left: navy thin solid; border-bottom: navy thin solid;">
  <table>
    <tr><td>
        <table id="tbl1" border="1">
        <tr>
        <td colspan="3" style="font-family: Arial; font-size: 8pt;">Accounts Details
        </td>
        </tr>
        <tr>
        <td class="SMSelected">Account Name</TD>
        <td class="SMSelected">Account Number</TD>
        <td class="SMSelected">Contractor</TD>
        </tr>           
        <tr>
        <td style="font-family: Arial; font-size: 8pt;"><%#DataBinder.Eval(CType(Container, DataGridItem).DataItem, "AccountName")%></td>
        <td style="font-family: Arial; font-size: 8pt;"><%#DataBinder.Eval(CType(Container, DataGridItem).DataItem, "AccountNo")%></td>
        <td style="font-family: Arial; font-size: 8pt;"><%#DataBinder.Eval(CType(Container, DataGridItem).DataItem, "ContractorName")%></td>
        </tr>        
        </table>
        </td>
        <td>
        <table id="Table1" border="1">
        <tr>
        <td colspan="3" style="font-family: Arial; font-size: 8pt;">Physicians Details
        </td>
        </tr>
        <tr>
        <td class="SMSelected">Physician Name</TD>
        <td class="SMSelected">PIN Number</TD>        
        <td class="SMSelected">Signature</TD>
        </tr> 
        <tr>
        <td style="font-family: Arial; font-size: 8pt;"><%#DataBinder.Eval(CType(Container, DataGridItem).DataItem, "DictatorName")%></td>
        <td style="font-family: Arial; font-size: 8pt;"><%#DataBinder.Eval(CType(Container, DataGridItem).DataItem, "PINNo")%></td>        
        <td style="font-family: Arial; font-size: 8pt;"><%#DataBinder.Eval(CType(Container, DataGridItem).DataItem, "SignedName")%></td>     
        </tr> 
        </table>
     
     </td></tr>
  </table>   
 
   
    <table border="1">
        <tr><td colspan="8" style="font-family: Arial; font-size: 8pt;"> Job Details &nbsp &nbsp &nbsp &nbsp</td></tr>
        <tr>
        <td class="SMSelected">Job Number</TD>
        <td class="SMSelected">Customer Job#</TD>        
        <td class="SMSelected">Status</TD>
        <td class="SMSelected">UserName</TD>        
        <td class="SMSelected">Date Created</TD>
        <td class="SMSelected">TAT</TD>
        <td class="SMSelected">Date Dictated</TD>
        <td class="SMSelected">Remaining</TD>        
        </tr> 
        <tr>
        <td style="font-family: Arial; font-size: 8pt;"><%#DataBinder.Eval(CType(Container, DataGridItem).DataItem, "JobNumber")%></td>
        <td style="font-family: Arial; font-size: 8pt;"><%#DataBinder.Eval(CType(Container, DataGridItem).DataItem, "CustJobID")%></td>  
        <td style="font-family: Arial; font-size: 8pt;">
           
        
            <asp:DropDownList ID="ddlStatus" runat="server"  >        
    </asp:DropDownList>
   
          
          
        <td style="font-family: Arial; font-size: 8pt;">
            <asp:Label ID="lblUserName" runat="server" ></asp:Label></td>        
        <td style="font-family: Arial; font-size: 8pt;"><%#DataBinder.Eval(CType(Container, DataGridItem).DataItem, "DateCreated")%></td>        
        <td style="font-family: Arial; font-size: 8pt;"><asp:TextBox ID="txtTAT" runat="server" Text='<%#DataBinder.Eval(CType(Container, DataGridItem).DataItem, "TAT")%>' MaxLength="4" Width="40px"></asp:TextBox></td>        
        <td style="font-family: Arial; font-size: 8pt;"><%#DataBinder.Eval(CType(Container, DataGridItem).DataItem, "DateDictated")%></td>                
        <td style="font-family: Arial; font-size: 8pt;"><%#datediffToMe(DataBinder.Eval(CType(Container, DataGridItem).DataItem, "TAT"), DataBinder.Eval(CType(Container, DataGridItem).DataItem, "SubmitDate"))%></td>                        
        </tr> 
        <tr>
        <td colspan="4" style="font-family: Arial; font-size: 8pt;" align="center">Assign To:&nbsp&nbsp&nbsp<asp:TextBox ID="txtAltUserName" runat="server"></asp:TextBox>
       
        </td>
        <td colspan="4" style="font-family: Arial; font-size: 8pt;" align="right"><asp:Button ID="btnSave" CssClass="button"  runat="server" Text="Save Changes" OnClick="SaveChanges"/>
        
        </td>
        </tr>
        </table>
 
    
    <hr style="border-right: navy thin solid; border-top: navy thin solid; border-left: navy thin solid; border-bottom: navy thin solid;" />                                
    <asp:Repeater ID="rptHistory" runat="server" > 
    <HeaderTemplate>  
    
        <table id="tbl1" style="font: 10pt verdana" cellpadding="1" cellspacing="1" border="1">
        <tr><td colspan="8" style="font-family: Arial; font-size: 8pt;"> CheckOut Log (Newest entries at the top)</td></tr>
        <TR>
        <TD class="SMSelected">Status</TD>
        <TD class="SMSelected">User Name</TD>
        <TD class="SMSelected">Assigned By</TD>        
        <TD class="SMSelected">Modified Date</TD>
        <TD class="SMSelected">LineCount</TD>
        <TD class="SMSelected">Template Name</TD>
        <TD class="SMSelected">version</TD>
        <TD class="SMSelected">IP</TD>
        <TD class="SMSelected">Downloaded</TD>
        </TR>
          
    </HeaderTemplate>
    
    <ItemTemplate>
        <tr>
        <td style="font-family: Arial; font-size: 8pt;"><%#getStatus(CInt(Container.DataItem("Status")))%></td>
        <td style="font-family: Arial; font-size: 8pt;"><%#IIf(IsDBNull(Container.DataItem("UserName")), "-", Container.DataItem("UserName").ToString)%></td>
        <td style="font-family: Arial; font-size: 8pt;"><%#IIf(IsDBNull(Container.DataItem("AssignedBy")), "-", Container.DataItem("AssignedBy").ToString)%></td>
        <td style="font-family: Arial; font-size: 8pt;"><%#IIf(IsDBNull(Container.DataItem("DateModified")),"-",Container.DataItem("DateModified").ToString)%></td>
        <td style="font-family: Arial; font-size: 8pt;"><%#IIf(IsDBNull(Container.DataItem("LineCount")), "0", Container.DataItem("LineCount").ToString)%></td>
        <td style="font-family: Arial; font-size: 8pt;"><%#IIf(IsDBNull(Container.DataItem("TemplateName")), "-", Container.DataItem("TemplateName").ToString)%></td>
        <td style="font-family: Arial; font-size: 8pt;">      
<asp:HyperLink ID="HyperLink1"
NavigateUrl='<%#"ShowVersion.aspx?DocID=" & hdnTransID.Value & ".doc." & Container.DataItem("version").tostring%>'
Text='<%#IIf(IsDBNull(Container.DataItem("version")),"-",Container.DataItem("version"))%>'
Target="_blank"
runat="server" Visible='<%#IIf(IsDBNull(Container.DataItem("version")),false ,true)%>' />


        </td>
        <td style="font-family: Arial; font-size: 8pt;"><%#IIf(IsDBNull(Container.DataItem("IP")), "-", Container.DataItem("IP").ToString)%></td>
        <td style="font-family: Arial; font-size: 8pt;"><%#IIf(IsDBNull(Container.DataItem("Downloaded")), "False", Container.DataItem("Downloaded").ToString)%></td>
        </tr>
    </ItemTemplate>
     
    <FooterTemplate>    
     </Table> 
    </FooterTemplate>   
    </asp:Repeater>
    <hr style="border-right: navy thin solid; border-top: navy thin solid; border-left: navy thin solid; border-bottom: navy thin solid;" />    
    <table id="tbl1" style="font: 10pt verdana" cellpadding="1" cellspacing="1" border="1">         
        <tr><td style="font-family: Arial; font-size: 8pt;"> Demographic Information</td></tr>
        <%--<tr><td><%#DataBinder.Eval(CType(Container, DataGridItem).DataItem, "JobNumber") %></td></tr>--%>
    </table>  
        
    <asp:GridView ID="grdExtended" runat="server" AutoGenerateColumns="False">  
        <Columns>
            <asp:BoundField DataField="Caption" HeaderText="Field Name" ReadOnly="True" />
            <asp:BoundField DataField="Value" HeaderText="Field Value" />
        </Columns>
        <HeaderStyle CssClass="SMSelected" />
        <RowStyle Font-Names="Verdana" Font-Size="8pt" />
        <EditRowStyle Font-Names="Verdana" Font-Size="8pt" />
        <AlternatingRowStyle Font-Names="Verdana" Font-Size="8pt" />    
    </asp:GridView>                       
    <asp:HiddenField ID="hdnTransID" runat="server" Value='<%#DataBinder.Eval(CType(Container, DataGridItem).DataItem, "TranscriptionID") %>'/>
    <asp:HiddenField ID="hdnStatus" runat="server" Value='<%#DataBinder.Eval(CType(Container, DataGridItem).DataItem, "Status") %>'/>
    <asp:HiddenField ID="hdnSQL" runat="server" Value='<%#DataBinder.Eval(CType(Container, DataGridItem).DataItem, "Status") %>'/>    
    <asp:HiddenField ID="hdnLevelDropDown" runat="server" />
    <asp:HiddenField ID="hdnres" runat="server" />
	</DIV>
	
