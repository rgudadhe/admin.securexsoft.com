<%@ Page Language="VB" AutoEventWireup="false" CodeFile="PredefinedComments.aspx.vb" Inherits="PredefinedComments" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 1.0 Transitional/EN">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Predefined Comments</title>
    <link href= "../App_Themes/Css/styles.css" type="text/css" rel="stylesheet" />
    <link href= "../App_Themes/Css/Common.css" type="text/css" rel="stylesheet" />

</head>
<body>
    <form id="form1" runat="server">
        <div id="body">
        <div id="cap"></div>
        <div id="main">
        <h1>Predefined Comments</h1>
        <asp:Panel ID="Panel1" runat="server" HorizontalAlign="Left">
            <div>
                <asp:Table ID="tblCancel" runat="server" Width="96%">
                    <asp:TableRow>
                        <asp:TableCell CssClass="HeaderDiv" HorizontalAlign=Center>
                           Predefined Comments
                        </asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow>
                        <asp:TableCell>
                            <asp:GridView ID="GridViewMain" EnableSortingAndPagingCallbacks=false runat="server" AutoGenerateColumns=False AllowPaging=true AllowSorting=true ShowFooter=true ShowHeader=true EmptyDataText="No Records found"  Width=100% OnRowUpdating="GridViewMain_RowUpdating" OnRowEditing="GridViewMain_RowEditing" EnableViewState=true>
                                <AlternatingRowStyle BackColor="OldLace" />
                                <Columns>
                                    <asp:TemplateField ItemStyle-Width=1 HeaderStyle-CssClass="alt1">
                                        <ItemTemplate>
                                            <asp:HiddenField ID="Id" Value='<%#Eval("ID") %>' runat=server  />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField ItemStyle-Width=5 HeaderText="SrNo" SortExpression="SrNo" HeaderStyle-CssClass="alt1">
                                        <ItemTemplate>
                                            <asp:Label ID="lblSrNo" Text='<%# Eval("SrNo") %>' Font-Names="Trebuchet MS" Font-Size=Small runat="server"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Comment" HeaderStyle-HorizontalAlign=Center SortExpression="Comment" ItemStyle-Width=500 HeaderStyle-CssClass="alt1">
                                        <ItemTemplate>
                                            <asp:Label ID="lblCateName" Text='<%# Eval("Comment") %>' CssClass="common" runat="server"></asp:Label>
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:TextBox ID="txtCommentEdit" Text='<%# Eval("Comment") %>' runat="server" CssClass="common" Width=500></asp:TextBox>
                                        </EditItemTemplate>
                                        <FooterTemplate>
                                            <asp:TextBox ID="txtComment" runat="server" Width="98%" CssClass="common" EnableViewState=true ></asp:TextBox>
                                            <asp:RequiredFieldValidator  Display="None" ID="RequiredFieldCateName" runat="server" ErrorMessage="Please enter Comment" ControlToValidate="txtComment" ></asp:RequiredFieldValidator>
                                            &nbsp &nbsp &nbsp &nbsp &nbsp &nbsp &nbsp &nbsp &nbsp &nbsp &nbsp &nbsp &nbsp &nbsp &nbsp &nbsp &nbsp &nbsp &nbsp &nbsp &nbsp &nbsp &nbsp &nbsp &nbsp &nbsp &nbsp &nbsp &nbsp &nbsp &nbsp &nbsp &nbsp &nbsp &nbsp &nbsp &nbsp &nbsp &nbsp &nbsp &nbsp &nbsp<asp:LinkButton ID="linkAddCate" CommandName="AddComment" runat="server">Add</asp:LinkButton>
                                        </FooterTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Status" SortExpression="IsDeleted" HeaderStyle-HorizontalAlign=Center HeaderStyle-CssClass="alt1">
                                        <ItemTemplate>
                                            <asp:Label ID="lblStatus" Text='<%#SetStatus(Eval("IsDeleted"))%>' runat="server" Width="20%" CssClass="common"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    
                                    <asp:CommandField HeaderText="Edit" HeaderStyle-HorizontalAlign=Center CausesValidation=false ShowEditButton="True" HeaderStyle-CssClass="alt1" />
                                    
                                    <asp:TemplateField HeaderText="Delete" HeaderStyle-HorizontalAlign=Center HeaderStyle-CssClass="alt1">
                                        <ItemTemplate>
                                            <%--<asp:LinkButton ID="linkDeleteCate" CommandName=<%#IIf(IsDBNULL(Eval("IsDeleted")) OR (Eval("IsDeleted")=0),"Deleted","Active") %> runat="server" CausesValidation=false><%#IIf(IsDBNull(Eval("IsDeleted")) Or (Eval("IsDeleted") = 0), "Deleted", "Active")%></asp:LinkButton>--%>
                                            <asp:LinkButton ID="linkDeleteCate" CommandName='<%#SetStatus(Eval("IsDeleted")) %>' runat="server" CausesValidation="false" CssClass="common"><%#SetStatus(Eval("IsDeleted"))%></asp:LinkButton>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <%--<asp:LinkButton ID="linkAddCate" CommandName="AddComment" runat="server">Add</asp:LinkButton>--%>
                                        </FooterTemplate>    
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </asp:TableCell>
                    </asp:TableRow>
                </asp:Table>
            </div>
        </asp:Panel>
        </div> 
        </div> 
    <asp:ValidationSummary ID="ValidationSummary1" runat="server" HeaderText="Following error occurs:" ShowMessageBox="true" DisplayMode="BulletList" ShowSummary="False" /> </form>
</body>
</html>
