<%@ Page Language="VB" AutoEventWireup="false" CodeFile="MappingView.aspx.vb" Inherits="Transcend_MappingView" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Transcend Mapping</title>
    <link href= "../App_Themes/Css/Main.css" type="text/css" rel="stylesheet" />
    <link href="../App_Themes/Css/Common.css" type="text/css" rel="stylesheet"  />
    <link href= "../App_Themes/Css/Styles.css" type="text/css" rel="stylesheet" />
    <link href= "../App_Themes/Css/Calendar.css" type="text/css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
    <ajaxToolkit:ToolkitScriptManager runat="server" ID="ScriptManager1"/>    
    <div id="body">
    <div id="cap"></div>
    <div id="main">
    <h1>Mapping View</h1>
    <div>
        <table width="70%">
            <tr>
                <td style="width:30%" align="center" class="alt1">
                    Search User
                </td>
                <td style="width:30%" align="center" class="alt1">
                    Search Account
                </td>
                <td style="width:30%" align="center" class="alt1">
                    Search Status
                </td>
                <td style="width:10%" class="alt1">
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td style="width:30%" >
                    <asp:TextBox ID="txtName" runat="server" TabIndex="1" Width="95%" CssClass="common" ></asp:TextBox>
                    <ajaxToolkit:AutoCompleteExtender ID="AutoC" 
                                  MinimumPrefixLength="1" 
                                  CompletionSetCount="10" 
                                  runat="server" 
                                  TargetControlID="txtName"
                                  ServicePath="../users/autocomplete.asmx"
                                  ServiceMethod="GetCompletionAllList"
                                  EnableCaching="true"/>
                </td>
                <td style="width:30%">
                    <asp:TextBox ID="txtSAccount" CssClass="common" runat="server" ></asp:TextBox>                                        
                </td>
                <td style="width:30%">
                    <asp:DropDownList ID="ddlStatus" CssClass="common" runat="server">
                        <asp:ListItem Text="Please Select" Value=""></asp:ListItem>
                        <asp:ListItem Text="Active" Value="Active"></asp:ListItem>
                        <asp:ListItem Text="Inactive" Value="Inactive"></asp:ListItem>
                    </asp:DropDownList>                    
                </td>
                <td style="width:10%" align="center">
                    <asp:Button ID="btnSubmit" runat="server" Text="Search" CssClass="button"  />
                </td>
            </tr>
            <tr>
                <td colspan="4">
                    <asp:LinkButton ID="LnlExport" runat="server" CssClass="common" >Export Mapping List</asp:LinkButton>
                </td>
            </tr>
        </table>
        <asp:Label ID="lblResponse" runat="server" Text="" CssClass="common" ></asp:Label>
        <br />
        <asp:Table ID="tblView" runat="server" Width="60%" GridLines="None">
            <asp:TableRow>
                <asp:TableCell>
                    <asp:GridView ID="GrdViewData" runat="server" AllowPaging="true" AllowSorting="true" Font-Names="Trebuchet MS" Font-Size="9pt" AutoGenerateColumns="false" PageSize="10" Width="100%" ShowFooter="true" ShowHeader="true">
                    <Columns>
                        <asp:TemplateField HeaderText="Emp/HBA_Name" SortExpression="UName" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left" HeaderStyle-CssClass="alt1">
                            <ItemTemplate>
                                <%#Eval("UName").ToString%>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Status" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left" HeaderStyle-CssClass="alt1">
                            <ItemTemplate>
                                <%#setStatus(Eval("Status").ToString)%>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="SecureITID" SortExpression="SecureITID" HeaderStyle-HorizontalAlign="Center" HeaderStyle-CssClass="alt1">
                            <ItemTemplate>
                                <%#Eval("SecureITID").ToString%>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="TranscendID" SortExpression="TranscendID" HeaderStyle-HorizontalAlign="Center" HeaderStyle-CssClass="alt1">
                            <ItemTemplate>
                                <%#Eval("TranscendID").ToString%>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Account" SortExpression="Account" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left" HeaderStyle-CssClass="alt1">
                            <ItemTemplate>
                                <%#Eval("Account").ToString%>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Name" SortExpression="Name" HeaderStyle-HorizontalAlign="Center" HeaderStyle-CssClass="alt1">
                            <ItemTemplate>
                                <%#Eval("Name").ToString%>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="User_Name" SortExpression="User_Name" HeaderStyle-HorizontalAlign="Center" HeaderStyle-CssClass="alt1">
                            <ItemTemplate>
                                <%#Eval("User_Name").ToString%>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Password" SortExpression="Password" HeaderStyle-HorizontalAlign="Center" HeaderStyle-CssClass="alt1">
                            <ItemTemplate>
                                <%#Eval("Password").ToString%>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Juniper_Password" SortExpression="Juniper_Password" HeaderStyle-HorizontalAlign="Center" HeaderStyle-CssClass="alt1">
                            <ItemTemplate>
                                <%#Eval("Juniper_Password").ToString%>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Console_Password" SortExpression="Console_Password" HeaderStyle-HorizontalAlign="Center" HeaderStyle-CssClass="alt1">
                            <ItemTemplate>
                                <%#Eval("Console_Password")%>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="ClearanceStatus" SortExpression="ClearanceStatus" HeaderStyle-HorizontalAlign="Center" HeaderStyle-CssClass="alt1">
                            <ItemTemplate>
                                <%#Eval("ClearanceStatus").ToString%>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Mentor" SortExpression="Mentor" HeaderStyle-HorizontalAlign="Center" HeaderStyle-CssClass="alt1">
                            <ItemTemplate>
                                <%#Eval("Mentor")%>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="QA" SortExpression="QA" HeaderStyle-HorizontalAlign="Center" HeaderStyle-CssClass="alt1">
                            <ItemTemplate>
                                <%#Eval("QA").ToString%>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Comment" SortExpression="Comment" HeaderStyle-HorizontalAlign="Center" HeaderStyle-CssClass="alt1">
                            <ItemTemplate>
                                <%#Eval("Comment").ToString%>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Audit Type" SortExpression="AuditType" HeaderStyle-HorizontalAlign="Center" HeaderStyle-CssClass="alt1">
                            <ItemTemplate>
                                <%#Eval("AuditType").ToString%>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="OfficialMailID" SortExpression="OfficialMailID" HeaderStyle-HorizontalAlign="Center" HeaderStyle-CssClass="alt1">
                            <ItemTemplate>
                                <%#Eval("OfficialMailID").ToString%>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="OtherMailID" SortExpression="OtherMailID" HeaderStyle-HorizontalAlign="Center" HeaderStyle-CssClass="alt1">
                            <ItemTemplate>
                                <%#Eval("OtherMailID").ToString%>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="ChatID" SortExpression="ChatID" HeaderStyle-HorizontalAlign="Center" HeaderStyle-CssClass="alt1">
                            <ItemTemplate>
                                <%#Eval("ChatID").ToString%>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="ChartScriptID" SortExpression="ChartScriptID" HeaderStyle-HorizontalAlign="Center" HeaderStyle-CssClass="alt1">
                            <ItemTemplate>
                                <%#Eval("ChartScriptID").ToString%>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                    </asp:GridView>
                </asp:TableCell>
            </asp:TableRow>     
        </asp:Table>
        <br />
        <asp:HiddenField ID="hdnUserID" runat="server" />
        <asp:HiddenField ID="hdnResource_Name" runat="server" />
        <asp:datagrid id="dgResultsData" 
				allowpaging="false" allowsorting="false" 
				autogeneratecolumns="false"  GridLines="Both" 
				runat="server" Font-Names="Trebuchet MS" Font-Size="12px">
				<Columns>
				    <asp:BoundColumn DataField="UName" HeaderText="Emp/HBA Name"></asp:BoundColumn>
				    <asp:TemplateColumn HeaderText="Status">
				        <ItemTemplate>
				            <%#setStatus(Container.DataItem("Status").ToString)%>
				        </ItemTemplate>
				    </asp:TemplateColumn>
				    <asp:BoundColumn DataField="SecureITID" HeaderText="SecureITID"></asp:BoundColumn>
				    <asp:BoundColumn DataField="TranscendID" HeaderText="TranscendID"></asp:BoundColumn>
				    <asp:BoundColumn DataField="Account" HeaderText="Account"></asp:BoundColumn>
				    <asp:BoundColumn DataField="Name" HeaderText="Name"></asp:BoundColumn>
				    <asp:BoundColumn DataField="User_Name" HeaderText="User_Name"></asp:BoundColumn>
				    <asp:BoundColumn DataField="Password" HeaderText="Password"></asp:BoundColumn>
				    <asp:BoundColumn DataField="Juniper_Password" HeaderText="Juniper_Password"></asp:BoundColumn>
				    <asp:BoundColumn DataField="Console_Password" HeaderText="Console_Password"></asp:BoundColumn>
				    <asp:BoundColumn DataField="ClearanceStatus" HeaderText="ClearanceStatus"></asp:BoundColumn>
				    <asp:BoundColumn DataField="Mentor" HeaderText="Mentor"></asp:BoundColumn>
				    <asp:BoundColumn DataField="QA" HeaderText="QA"></asp:BoundColumn>
				    <asp:BoundColumn DataField="Comment" HeaderText="Comment"></asp:BoundColumn>
				    <asp:BoundColumn DataField="AuditType" HeaderText="AuditType"></asp:BoundColumn>
				    <asp:BoundColumn DataField="OfficialMailID" HeaderText="OfficialMailID"></asp:BoundColumn>
				    <asp:BoundColumn DataField="OtherMailID" HeaderText="OtherMailID"></asp:BoundColumn>
				    <asp:BoundColumn DataField="ChatID" HeaderText="ChatID"></asp:BoundColumn>
				</Columns>
			</asp:datagrid>
          <asp:HiddenField ID="Hsort" runat="server" />
        <asp:HiddenField ID="Horder" runat="server" />
        </div> 
        </div> 
    </div>
    </form>
</body>
</html>
