<%@ Page Language="VB" AutoEventWireup="false" CodeFile="IssueType.aspx.vb" Inherits="IssueType" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 1.0 Transitional/EN">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Untitled Page</title>
    <LINK href= "Styles/Default.css" type="text/css" rel="stylesheet">
    <LINK href= "Styles/Accordin.css" type="text/css" rel="stylesheet">
    <LINK href="Styles/Report.css" type="text/css" rel="stylesheet">
</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
    <div>
        <asp:Table ID="Table2" Width="100%" Font-Names="Trebuchet MS" runat="server" GridLines=Horizontal>
            <asp:TableRow>
                <asp:TableCell>
                    <asp:GridView ID="GridViewIssueTypes" runat="server" AllowSorting=true AllowPaging=True
                     Width=100% BackColor="#eaeaea" ShowFooter=true Font-Names="Trebuchet MS" Font-Size=Small AutoGenerateColumns=false DataSourceID="sqlDataSource1" DataKeyNames="IssueID" OnRowEditing="GridViewIssueTypes_RowEditing" OnRowUpdating="GridViewIssueTypes_RowUpdating" OnRowDeleting = "GridViewIssueTypes_RowDeleting">
                        <HeaderStyle HorizontalAlign=Center  CssClass="ReportHeaderDiv" ForeColor=white />
                        <AlternatingRowStyle Backcolor="#F7F7F7" />
                        <SelectedRowStyle BackColor="#123456" />
                        <FooterStyle BackColor=white />
                        <Columns>
                            <asp:TemplateField>
                               <ItemTemplate>
                                   <asp:HiddenField ID="txtIssueID" runat="server" Value='<%#Eval("IssueID") %>' /> 
                               </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Issue Name" SortExpression="IssueName" HeaderStyle-CssClass="ReportHeaderDiv" HeaderStyle-HorizontalAlign=Center HeaderStyle-Width="25%">
                                <ItemTemplate><%#Eval("IssueName")%></ItemTemplate>
                                <EditItemTemplate>
                                    <asp:TextBox ID="txtIssueName" Text='<%# Eval("IssueName")%>' Font-Names="Trebuchet Ms" runat="server" Width="95%"></asp:TextBox>
                                </EditItemTemplate>    
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Description" SortExpression="IssueDesc" HeaderStyle-CssClass="ReportHeaderDiv" HeaderStyle-HorizontalAlign=Center HeaderStyle-Width="60%">
                                <ItemTemplate><%#Eval("IssueDesc")%></ItemTemplate>
                                <EditItemTemplate>
                                    <asp:TextBox ID="txtIssueDesc" Text='<%# Eval("IssueDesc")%>' Font-Names="Trebuchet Ms" runat="server" Width="95%"></asp:TextBox>
                                </EditItemTemplate>
                            </asp:TemplateField>
                            <asp:CommandField HeaderText="Edit" CausesValidation=false ShowEditButton="True"/>
                            <asp:TemplateField HeaderText="Delete" HeaderStyle-Width="10%">
                                <ItemTemplate>
                                    <asp:LinkButton ID="linkDeleteCate" CommandName="Delete" runat="server" CausesValidation=false>Delete</asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                 </asp:TableCell>
             </asp:TableRow>     
             <asp:TableRow>
                <asp:TableCell>
                    <ajaxToolkit:Accordion ID="MyAccordion" runat="server" Width="100%"
                                HeaderCssClass="accordionHeader" ContentCssClass="accordionContent" FadeTransitions="true" SelectedIndex=-1 FramesPerSecond="40" 
                                TransitionDuration="250" AutoSize="None" RequireOpenedPane="false" SuppressHeaderPostbacks="true">
                                <Panes>
                                    <ajaxToolkit:AccordionPane ID="AddIssueTypePane" runat=server>
                                        <Header>Add New Issue Type</Header>
                                        <Content>
                                            <asp:Table ID="tblAddIssueType" HorizontalAlign=Center runat="server" Font-Names="Trebuchet MS" Font-Size=Small GridLines=Both Width=82%>
                                                <asp:TableRow>
                                                    <asp:TableCell Width=10% HorizontalAlign=Right>
                                                        <asp:Label ID="lblIssueName" runat="server" Text="Issue Name"></asp:Label> 
                                                    </asp:TableCell>
                                                    <asp:TableCell Width="75%">
                                                        <asp:TextBox ID="txtIssueName" runat="server" Font-Names="Trebuchet MS" Width=50%></asp:TextBox> &nbsp &nbsp
                                                        <asp:RequiredFieldValidator ID="RequiredFieldIssueType" Font-Names="Trebuchet Ms" Font-Bold=true Font-Italic=True runat="server" ErrorMessage="Please enter Issue Type" ControlToValidate="txtIssueName"></asp:RequiredFieldValidator>
                                                    </asp:TableCell>
                                                </asp:TableRow>
                                                <asp:TableRow>
                                                    <asp:TableCell Width=10% VerticalAlign=Top HorizontalAlign=Right>
                                                        <asp:Label ID="lblDescription" runat="server" Text="Description"></asp:Label>
                                                    </asp:TableCell>
                                                    <asp:TableCell Width="75%">
                                                        <textarea id="txtDesc" rows="4" style="font-family:Trebuchet MS" cols="75" runat=server></textarea> &nbsp
                                                        <asp:RequiredFieldValidator ID="RequiredFieldIssueDesc" Font-Names="Trebuchet Ms" Font-Bold=true Font-Italic=True runat="server" ErrorMessage="Please enter Issue Description" ControlToValidate="txtDesc"></asp:RequiredFieldValidator>
                                                    </asp:TableCell>
                                                </asp:TableRow>
                                                <asp:TableRow>
                                                    <asp:TableCell ColumnSpan=2 HorizontalAlign=center> 
                                                        <asp:Button ID="BtnAddIssueType" runat="server" Font-Names="Trebuchet MS" Font-Size=small Text="Add" UseSubmitBehavior=true />
                                                    </asp:TableCell>
                                                </asp:TableRow>
                                            </asp:Table>
                                        </Content>
                                    </ajaxToolkit:AccordionPane>
                                </Panes>
                    </ajaxToolkit:Accordion>
                  </asp:TableCell>
              </asp:TableRow>   
         </asp:Table>
        <asp:SqlDataSource ID="sqlDataSource1" runat=server ConnectionString="<%$ ConnectionStrings:ETSConnectionString %>" SelectCommand="SELECT * FROM dbo.tblCustomerIssueType where IsDeleted IS NULL" >
        </asp:SqlDataSource>
    </div>
    </form>
</body>
</html>
