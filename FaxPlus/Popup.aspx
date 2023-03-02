<%@ Page Language="VB" AutoEventWireup="false" CodeFile="Popup.aspx.vb" Inherits="FaxPlus_Popup" %>
<%@ Register TagPrefix="DBWC" Namespace="DBauer.Web.UI.WebControls" Assembly="DBauer.Web.UI.WebControls.HierarGrid" %>
<%@ Register
    Assembly="AjaxControlToolkit"
    Namespace="AjaxControlToolkit"
    TagPrefix="ajaxToolkit" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 1.0 Transitional/EN">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Set Reffering Physician </title>
    <link href= "../App_Themes/Css/styles.css" type="text/css" rel="stylesheet" />
    <link href= "../App_Themes/Css/Common.css" type="text/css" rel="stylesheet" />
    <link href="../App_Themes/Css/DataTable.css" rel="stylesheet" type="text/css" />
    <link href="../App_Themes/Css/TableSorter.css" rel="stylesheet" type="text/css" />
    <link href="../App_Themes/Css/Calendar.css" rel="stylesheet" type="text/css" />
    <script src="../App_Themes/JS/jquery-1.4.2.min.js" type="text/javascript"></script>  
    <script src="../App_Themes/JS/jquery.dataTables.min.js" type="text/javascript"></script>  
    
    <script type="text/javascript" language="javascript">
    $(document).ready(function() {
				$('#RPlist').dataTable( {
					//"sPaginationType": "full_numbers"
                    "aoColumns": [
                            		{ "asSorting": [ "desc", "asc" ] },
		                            { "asSorting": [ "desc", "asc" ] },
		                            { "asSorting": [ "desc", "asc" ] },
		                            { "asSorting": [ "desc", "asc" ] },
		                            { "asSorting": [ "desc", "asc" ] },
		                            { "asSorting": [ "desc", "asc" ] },
		                            { "asSorting": [ "desc", "asc" ] },
                            		{ "asSorting": [ "desc", "asc" ] }
	                              ] 
				} );
			} );
</script>
</head>

<body>
    <form id="form1" runat="server">
    <div style="text-align:left">
    <ajaxToolkit:ToolkitScriptManager id="ScriptManager1" runat="Server" EnableScriptLocalization="true" EnableScriptGlobalization="true" EnablePartialRendering="true"></ajaxToolkit:ToolkitScriptManager>
    <table>
        <tr>
            <td colspan="4" class="HeaderDiv">Physician Address</td>
        </tr>
        <tr>
            <td class="alt">First Name</td>
            <td class="alt">Last Name</td>                        
            <td class="alt">&nbsp</td>
            <td class="alt">&nbsp</td>
        </tr>
        <tr>
            <td>
                <asp:TextBox ID="txtF" runat="server"></asp:TextBox>
            </td>
            <td>
                <asp:TextBox ID="txtL" runat="server"></asp:TextBox>
            </td>                        
            <td>
                <asp:Button ID="btnSearch" CssClass="button" runat="server" Text="Search" OnClick="btnSearch_Click" />
            </td>
            <td>
                <asp:Button ID="Button1" CssClass="button" runat="server" Text="Done" />
            </td>
        </tr>
   </table>
   <asp:GridView ID="RPlist" runat="server" AutoGenerateColumns="false">
        <Columns>
            <asp:TemplateField HeaderStyle-CssClass="Header">
                <ItemTemplate>
                    <asp:Button id="btnAttach" runat="server" Text="attache" onclick="Attache" CssClass="button"></asp:Button>
                                <asp:HiddenField ID="hdnPhyID" Value='<%#Container.DataItem("PhyID")%>' runat="server" />                                
				                <ajaxToolkit:ConfirmButtonExtender ID="ConfirmButtonExtender2" runat="server" 
                        TargetControlID="btnAttach"
                        OnClientCancel="cancelClick"
                        DisplayModalPopupID="ModalPopupExtender1" />
                    <br />
                    <ajaxToolkit:ModalPopupExtender ID="ModalPopupExtender1" runat="server" TargetControlID="btnAttach" PopupControlID="PNL" OkControlID="ButtonOk" CancelControlID="ButtonCancel" BackgroundCssClass="modalBackground" />
                    <asp:Panel ID="PNL" runat="server" style="display:none; width:200px; background-color:White; border-width:2px; border-color:Black; border-style:solid; padding:20px;">
                        Are you sure you want to set the physician?
                        <br /><br />
                        <div style="text-align:right;">
                            <asp:Button ID="ButtonOk" runat="server" Text="OK" />
                            <asp:Button ID="ButtonCancel" runat="server" Text="Cancel" />
                        </div>
                    </asp:Panel>    
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Physician Name" HeaderStyle-CssClass="Header">
                <ItemTemplate>
                    <%#IIf(IsDBNull(Container.DataItem("Physician")), String.Empty, Container.DataItem("Physician"))%>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Address" HeaderStyle-CssClass="Header">
                <ItemTemplate>
                    <%#IIf(IsDBNull(Container.DataItem("Address")), String.Empty, Container.DataItem("Address"))%>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="City" HeaderStyle-CssClass="Header">
                <ItemTemplate>
                    <%#IIf(IsDBNull(Container.DataItem("PhyCity")), String.Empty, Container.DataItem("PhyCity"))%>
                </ItemTemplate>
            </asp:TemplateField>            
            <asp:TemplateField HeaderText="State" HeaderStyle-CssClass="Header">
                <ItemTemplate>
                    <%#IIf(IsDBNull(Container.DataItem("PhyState")), String.Empty, Container.DataItem("PhyState"))%>
                </ItemTemplate>
            </asp:TemplateField>            
            <asp:TemplateField HeaderText="Phone#" HeaderStyle-CssClass="Header">
                <ItemTemplate>
                    <%#IIf(IsDBNull(Container.DataItem("PhoneNO")), String.Empty, Container.DataItem("PhoneNO"))%>
                </ItemTemplate>
            </asp:TemplateField>              
            <asp:TemplateField HeaderText="Fax#" HeaderStyle-CssClass="Header">
                <ItemTemplate>
                    <%#IIf(IsDBNull(Container.DataItem("FaxNO")), String.Empty, Container.DataItem("FaxNO"))%>
                </ItemTemplate>
            </asp:TemplateField> 
            <asp:TemplateField HeaderText="Email" HeaderStyle-CssClass="Header">
                <ItemTemplate>
                    <%#IIf(IsDBNull(Container.DataItem("Email")), String.Empty, Container.DataItem("Email"))%>
                </ItemTemplate>
            </asp:TemplateField>                       
        </Columns>         
   </asp:GridView> 
   
   
                    <%--<DBWC:HierarGrid id="RPlist" DataKeyField="PhyID" allowsorting="True" autogeneratecolumns="False" style="Z-INDEX: 101" runat="server" BorderColor="#999999" BorderStyle="None" BorderWidth="1px" BackColor="White" CellPadding="3"  gridlines="None" horizontalalign="Left" HeaderStyle-ForeColor="White" Font-Names="Verdana" Font-Size="5pt" >
                    <SelectedItemStyle Font-Bold="True" ForeColor="White" BackColor="#008A8C"></SelectedItemStyle>
                    <AlternatingItemStyle BackColor ="Gainsboro" >		
				        </AlternatingItemStyle>
                    <ItemStyle  BackColor="Beige" Font-Names="Verdana" Font-Size="8pt" Wrap="False" ></ItemStyle>
                    <HeaderStyle CssClass="SMParentSelected" ForeColor="White"></HeaderStyle>				
                    <Columns>
				            <asp:templatecolumn>				            
							<itemtemplate>
									
							    <asp:Button id="btnAttach" runat="server" Text="attache" onclick="Attache"></asp:Button>
                                <asp:HiddenField ID="hdnPhyID" Value='<%#Container.DataItem("PhyID")%>' runat="server" />                                
				                <ajaxToolkit:ConfirmButtonExtender ID="ConfirmButtonExtender2" runat="server" 
                        TargetControlID="btnAttach"
                        OnClientCancel="cancelClick"
                        DisplayModalPopupID="ModalPopupExtender1" />
                    <br />
                    <ajaxToolkit:ModalPopupExtender ID="ModalPopupExtender1" runat="server" TargetControlID="btnAttach" PopupControlID="PNL" OkControlID="ButtonOk" CancelControlID="ButtonCancel" BackgroundCssClass="modalBackground" />
                    <asp:Panel ID="PNL" runat="server" style="display:none; width:200px; background-color:White; border-width:2px; border-color:Black; border-style:solid; padding:20px;">
                        Are you sure you want to set the physician?
                        <br /><br />
                        <div style="text-align:right;">
                            <asp:Button ID="ButtonOk" runat="server" Text="OK" />
                            <asp:Button ID="ButtonCancel" runat="server" Text="Cancel" />
                        </div>
                    </asp:Panel>
</itemtemplate>
							<headerstyle width="20px" />
							</asp:templatecolumn>							
							<asp:boundcolumn DataField="Physician" HeaderText="Physician Name" SortExpression="Physician"/>
                            <asp:boundcolumn DataField="Address" HeaderText="Address" SortExpression="Address" />
                            <asp:boundcolumn DataField="PhyCity" HeaderText="City" SortExpression="PhyCity" />
                            <asp:boundcolumn DataField="PhyState" HeaderText="State" SortExpression="PhyState" />
                            <asp:boundcolumn DataField="PhoneNO" HeaderText="Phone#" SortExpression="PhoneNO" />
                            <asp:boundcolumn DataField="FaxNO" HeaderText="Fax#" SortExpression="FaxNO" />
                            <asp:boundcolumn DataField="Email" HeaderText="Email" SortExpression="E-Mail ID" />
                        </Columns>
                    </DBWC:HierarGrid>--%>
                    
                    
                    <asp:Literal ID="lit1" runat="server" Visible="false"></asp:Literal>                                       
</div>        
    </form>
</body>
</html>
