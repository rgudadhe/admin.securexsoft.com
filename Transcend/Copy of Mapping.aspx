<%@ Page Language="VB" AutoEventWireup="false" CodeFile="Copy of Mapping.aspx.vb" Inherits="Transcend_Mapping" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Transcend Mapping</title>
    <link href= "../App_Themes/Css/Main.css" type="text/css" rel="stylesheet" />
    <link href="../App_Themes/Css/Common.css" type="text/css" rel="stylesheet"  />
    <link href= "../App_Themes/Css/Styles.css" type="text/css" rel="stylesheet" />
    <script language="javascript" type="text/javascript">
        function StatusGridDropdown(csd,atypeid)
        {
           if(document.getElementById(csd).value!='')
           {
                if (document.getElementById(csd).value=='Yes')
                {
                    document.getElementById(atypeid).disabled =false;
                }
                else
                {
                    document.getElementById(atypeid).selectedIndex = 0;
                    document.getElementById(atypeid).disabled =true;
                }
           }
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <ajaxToolkit:ToolkitScriptManager runat="server" ID="ScriptManager1"/>    
    <div id="body">
    <div id="cap"></div>
    <div id="main">
    <h1>Mapping</h1>
    <div>
    <asp:Panel ID="Panel2" runat="server" width="100%">
           <table width="100%" >
             <tr>
                <td colspan="2" style="text-align: center" class="HeaderDiv">
                    Upload Mapping Sheet
                    <asp:ImageButton ID="Image1" runat="server" ImageUrl="~/images/expand_blue.jpg" AlternateText="(Show Details...)" CausesValidation="false"/>
                </td>
            </tr>
          </table> 
        </asp:Panel>
        <asp:Panel ID="Panel1" runat="server" width="100%">
            <center>
            <table style="text-align:center " width="80%" border="0" >
                <tr>
                    <td align="left" style="border:0">
                        <a href="https://secureit.edictate.com/ets_files/Transcend/MappingTemplate.xls" class="common" target="_blank">Download Template</a>
                    </td>
                </tr>
                <tr>
                    <td align="right" style="border:0">
                        Select File
                    </td>
                    <td align="left" style="border:0">
                        <asp:FileUpload ID="FileUpload" runat="server" Width="350" />
                    </td>
                </tr>
                <tr>
                    <td colspan="2" align="left" style="border:0">
                        <asp:Label ID="ErrLabel" runat="server" Text="" CssClass="common" Font-Italic="true" ForeColor="red"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td colspan="2" align="center" style="border:0">
                        <center>
                            <asp:Button ID="btnUpload" runat="server" Text="Upload Data" cssClass="button" />    
                        </center>
                    </td>
                </tr>
            </table>
            </center>
        </asp:Panel> 
        
        <ajaxToolkit:CollapsiblePanelExtender ID="cpeDemo" runat="Server"
        TargetControlID="Panel1"
        ExpandControlID="Panel2"
        CollapseControlID="Panel2" 
        Collapsed="True"
        TextLabelID="Label1"
        ImageControlID="Image1"    
        ExpandedText="(Hide Details...)"
        CollapsedText="(Show Details...)"
        ExpandedImage="~/images/collapse_blue.jpg"
        CollapsedImage="~/images/expand_blue.jpg"
        SuppressPostBack="true"
        />
        <table width="70%" border="0">
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
                <td style="width:10%" class="alt1" >
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
                                  ServiceMethod="GetCompletionList"
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
                    <asp:LinkButton ID="LnlExport" runat="server" CssClass="common"  >Export Mapping List</asp:LinkButton>
                </td>
            </tr>
        </table>
        <asp:Label ID="lblResponse" runat="server" Text="" CssClass="common" ></asp:Label>
        <br />
        <asp:Table ID="tblView" runat="server" Width="60%" CssClass="common" BorderStyle="None" BorderWidth="0" >
            <asp:TableRow>
                <asp:TableCell>
                    <asp:GridView ID="GrdViewData" runat="server" AllowPaging="true" AllowSorting="true" CssClass="common" AutoGenerateColumns="false" PageSize="10" Width="100%" OnRowDeleting="GrdViewData_RowDeleting" OnRowCancelingEdit="GrdViewData_RowCancelingEdit" OnRowEditing="GrdViewData_RowEditing" OnRowUpdating="GrdViewData_RowUpdating" ShowFooter="true" ShowHeader="true">
                    <Columns>
                        <asp:TemplateField HeaderText="Emp/HBA_Name" SortExpression="UName" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left" HeaderStyle-CssClass="alt1">
                            <ItemTemplate>
                                <%#Eval("UName").ToString%>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:Label ID="lblUName" runat="server" Text='<%#Bind("UName")%>'></asp:Label>
                                <asp:HiddenField ID="hdnTrackID" Value='<%#Bind("TrackID")%>' runat="server" />
                                <asp:HiddenField ID="hdnUserID" Value='<%#Bind("UserID")%>' runat="server" />
                            </EditItemTemplate>
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
                            <EditItemTemplate>
                                <asp:DropDownList ID="ddlSID" runat="server" CssClass="common" HeaderStyle-CssClass="alt1">
                                </asp:DropDownList>
                            </EditItemTemplate>
                            <FooterTemplate>
                                <asp:DropDownList ID="FddlSID" runat="server" CssClass="common"  HeaderStyle-CssClass="alt1">
                                </asp:DropDownList>
                            </FooterTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="TranscendID" SortExpression="TranscendID" HeaderStyle-HorizontalAlign="Center" HeaderStyle-CssClass="alt1">
                            <ItemTemplate>
                                <%#Eval("TranscendID").ToString%>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:TextBox ID="txtTransID" CssClass="common" runat="server" Text='<%#Bind("TranscendID")%>'></asp:TextBox>
                            </EditItemTemplate>
                            <FooterTemplate>
                                <asp:TextBox ID="FtxtTransID" CssClass="common" runat="server"></asp:TextBox>
                            </FooterTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Account" SortExpression="Account" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left" HeaderStyle-CssClass="alt1">
                            <ItemTemplate>
                                <%#Eval("Account").ToString%>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:TextBox ID="txtAccount" CssClass="common" runat="server" Text='<%#Bind("Account")%>'></asp:TextBox>
                            </EditItemTemplate>
                            <FooterTemplate>
                                <asp:TextBox ID="FtxtAccount" CssClass="common" runat="server"></asp:TextBox>
                            </FooterTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Name" SortExpression="Name" HeaderStyle-HorizontalAlign="Center" HeaderStyle-CssClass="alt1">
                            <ItemTemplate>
                                <%#Eval("Name").ToString%>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:TextBox ID="txtName" CssClass="common" runat="server" Text='<%#Bind("Name")%>'></asp:TextBox>
                            </EditItemTemplate>
                            <FooterTemplate>
                                <asp:TextBox ID="FtxtName" CssClass="common" runat="server"></asp:TextBox>
                            </FooterTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="User_Name" SortExpression="User_Name" HeaderStyle-HorizontalAlign="Center" HeaderStyle-CssClass="alt1">
                            <ItemTemplate>
                                <%#Eval("User_Name").ToString%>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:TextBox ID="txtUser_Name" CssClass="common" runat="server" Text='<%#Bind("User_Name")%>'></asp:TextBox>
                            </EditItemTemplate>
                            <FooterTemplate>
                                <asp:TextBox ID="FtxtUser_Name" CssClass="common" runat="server"></asp:TextBox>
                            </FooterTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Password" SortExpression="Password" HeaderStyle-HorizontalAlign="Center" HeaderStyle-CssClass="alt1">
                            <ItemTemplate>
                                <%#Eval("Password").ToString%>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:TextBox ID="txtPassword" CssClass="common" runat="server" Text='<%#Bind("Password")%>'></asp:TextBox>
                            </EditItemTemplate>
                            <FooterTemplate>
                                <asp:TextBox ID="FtxtPassword" CssClass="common" runat="server" ></asp:TextBox>
                            </FooterTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Juniper_Password" SortExpression="Juniper_Password" HeaderStyle-HorizontalAlign="Center" HeaderStyle-CssClass="alt1">
                            <ItemTemplate>
                                <%#Eval("Juniper_Password").ToString%>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:TextBox ID="txtJuniper_Password" CssClass="common" runat="server" Text='<%#Bind("Juniper_Password")%>'></asp:TextBox>
                            </EditItemTemplate>
                            <FooterTemplate>
                                <asp:TextBox ID="FtxtJuniper_Password" CssClass="common" runat="server" ></asp:TextBox>
                            </FooterTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Console_Password" SortExpression="Console_Password" HeaderStyle-HorizontalAlign="Center" HeaderStyle-CssClass="alt1">
                            <ItemTemplate>
                                <%#Eval("Console_Password")%>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:TextBox ID="txtConsole_Password" CssClass="common" runat="server" Text='<%#Bind("Console_Password")%>'></asp:TextBox>
                            </EditItemTemplate>
                            <FooterTemplate>
                                <asp:TextBox ID="FtxtConsole_Password" CssClass="common" runat="server" ></asp:TextBox>
                            </FooterTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="ClearanceStatus" SortExpression="ClearanceStatus" HeaderStyle-HorizontalAlign="Center" HeaderStyle-CssClass="alt1">
                            <ItemTemplate>
                                <%#Eval("ClearanceStatus").ToString%>
                            </ItemTemplate>
                            <EditItemTemplate> 
                                <asp:DropDownList ID="ddlCS" runat="server" CssClass="common" SelectedValue= '<%#IIf(ISDBNULL(Eval("ClearanceStatus").ToString),"",Eval("ClearanceStatus").ToString)%>'> 
                                    <asp:ListItem Value="" Text="Please Select"></asp:ListItem>
                                    <asp:ListItem Value="Yes" Text="Yes"></asp:ListItem>
                                    <asp:ListItem Value="No" Text="No"></asp:ListItem>
                                </asp:DropDownList> 
                            </EditItemTemplate>
                            <FooterTemplate>
                                <asp:DropDownList ID="FddlCS" runat="server" CssClass="common"  > 
                                    <asp:ListItem Value="" Text="Please Select"></asp:ListItem>
                                    <asp:ListItem Value="Yes" Text="Yes"></asp:ListItem>
                                    <asp:ListItem Value="No" Text="No"></asp:ListItem>
                                </asp:DropDownList>
                            </FooterTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="AuditType" SortExpression="AuditType" HeaderStyle-HorizontalAlign="Center" HeaderStyle-CssClass="alt1">
                            <ItemTemplate>
                                <%#Eval("AuditType").ToString%>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:DropDownList ID="ddlAType" runat="server" CssClass="common" SelectedValue= '<%#IIf(ISDBNULL(Eval("AuditType").ToString),"",Eval("AuditType").ToString)%>'> 
                                    <asp:ListItem Value="" Text="Please Select"></asp:ListItem>
                                    <asp:ListItem Value="Regular" Text="Regular"></asp:ListItem>
                                    <asp:ListItem Value="Daily" Text="Daily"></asp:ListItem>
                                </asp:DropDownList> 
                            </EditItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Mentor" SortExpression="Mentor" HeaderStyle-HorizontalAlign="Center" HeaderStyle-CssClass="alt1">
                            <ItemTemplate>
                                <%#Eval("Mentor")%>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:TextBox ID="txtMentor" CssClass="common" runat="server" Text='<%#Bind("Mentor")%>'></asp:TextBox>
                            </EditItemTemplate>
                            <FooterTemplate>
                                <asp:TextBox ID="FtxtMentor" CssClass="common" runat="server" ></asp:TextBox>
                            </FooterTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="QA" SortExpression="QA" HeaderStyle-HorizontalAlign="Center" HeaderStyle-CssClass="alt1">
                            <ItemTemplate>
                                <%#Eval("QA").ToString%>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:TextBox ID="txtQA" CssClass="common" runat="server" Text='<%#Bind("QA")%>'></asp:TextBox>
                            </EditItemTemplate>
                            <FooterTemplate>
                                <asp:TextBox ID="FtxtQA" CssClass="common" runat="server" ></asp:TextBox>
                            </FooterTemplate>
                        </asp:TemplateField>
                         <asp:TemplateField HeaderText="ChartScriptID" SortExpression="ChartScriptID" HeaderStyle-HorizontalAlign="Center" HeaderStyle-CssClass="alt1">
                            <ItemTemplate>
                                <%#Eval("ChartScriptID").ToString%>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:TextBox ID="txtChartScriptID" CssClass="common" runat="server" Text='<%#Bind("ChartScriptID")%>'></asp:TextBox>
                            </EditItemTemplate>
                            <FooterTemplate>
                                <asp:TextBox ID="FtxtChartScriptID" CssClass="common" runat="server" ></asp:TextBox>
                            </FooterTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Comment" SortExpression="Comment" HeaderStyle-HorizontalAlign="Center" HeaderStyle-CssClass="alt1">
                            <ItemTemplate>
                                <%#Eval("Comment").ToString%>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:TextBox ID="txtComment" CssClass="common" runat="server" Text='<%#Bind("Comment")%>'></asp:TextBox>
                            </EditItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Edit" ShowHeader="False" HeaderStyle-HorizontalAlign="Left" HeaderStyle-CssClass="alt1"> 
                            <EditItemTemplate> 
                                <asp:LinkButton ID="lbkUpdate" runat="server" CausesValidation="True" CommandName="Update" Text="Update"></asp:LinkButton> 
                                <asp:LinkButton ID="lnkCancel" runat="server" CausesValidation="False" CommandName="Cancel" Text="Cancel"></asp:LinkButton> 
                            </EditItemTemplate> 
                            <FooterTemplate> 
                                <asp:LinkButton ID="lnkAdd" runat="server" CausesValidation="False" CommandName="Insert" Text="Add"></asp:LinkButton> 
                            </FooterTemplate> 
                            <ItemTemplate> 
                                <asp:LinkButton ID="lnkEdit" runat="server" CausesValidation="False" CommandName="Edit" Text="Edit"></asp:LinkButton> 
                            </ItemTemplate> 
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Delete" HeaderStyle-CssClass="alt1">
                            <ItemTemplate>
                                <asp:LinkButton ID="LnkDelete" runat="server" CausesValidation="False" CommandArgument='<%#Eval("TrackID").ToString %>' CommandName="Delete" Text="Delete" />                    
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
				</Columns>
			</asp:datagrid>
    </div>
    </div></div> 
    <asp:HiddenField ID="Hsort" runat="server" />
        <asp:HiddenField ID="Horder" runat="server" />
    </form>
</body>
</html>
