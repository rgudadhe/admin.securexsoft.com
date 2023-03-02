<%@ Page Language="VB" AutoEventWireup="false" CodeFile="WorkGroups.aspx.vb" Inherits="WorkGroup_WorkGroups" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>WorkGroups</title>
    <link href= "../App_Themes/Css/Default.css" type="text/css" rel="stylesheet" />
    <link href= "../App_Themes/Css/Common.css" type="text/css" rel="stylesheet" />
    <link href= "../App_Themes/Css/Styles.css" type="text/css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <div id="body">
        <div id="cap"></div>
        <div id="main">
        <h1>Add/Edit WorkGroup</h1>
    
    <div style="text-align:left ">
        <asp:Table ID="tblCancel" runat="server" Width="96%" BorderStyle="None" CssClass="common">
            <asp:TableRow CssClass="HeaderDiv" >
                <asp:TableCell CssClass="HeaderDiv">
                   WorkGroups
                </asp:TableCell>
            </asp:TableRow>
            <asp:TableRow>
                <asp:TableCell>
                    <asp:GridView ID="GridViewMain" runat="server" AutoGenerateColumns="False" ShowFooter="true" ShowHeader="true" EmptyDataText="No Records found" Width="100%" OnRowUpdating="GridViewMain_RowUpdating" OnRowEditing="GridViewMain_RowEditing" EnableViewState="true" >
                        <Columns>
                            <asp:TemplateField ItemStyle-Width=1 HeaderStyle-CssClass="alt1">
                                <ItemTemplate>
                                    <asp:HiddenField ID="Id" Value='<%#Eval("WorkGroupID") %>' runat="server"  />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Description" ItemStyle-Width="500" HeaderStyle-CssClass="alt1">
                                <ItemTemplate>
                                    <asp:Label ID="lblDesc" Text='<%# Eval("Description") %>' CssClass="common" runat="server"></asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:TextBox ID="txtDescEdit" Text='<%# Eval("Description") %>' runat="server" Width="500" CssClass="common"></asp:TextBox>
                                </EditItemTemplate>
                                <FooterTemplate>
                                    <asp:TextBox ID="txtDesc" runat="server" Width="98%" EnableViewState="true" CssClass="common" ></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldCateName" runat="server" ErrorMessage="Please enter Workgroup description" ControlToValidate="txtDesc"></asp:RequiredFieldValidator>
                                    &nbsp &nbsp &nbsp &nbsp &nbsp &nbsp &nbsp &nbsp &nbsp &nbsp &nbsp &nbsp &nbsp &nbsp &nbsp &nbsp &nbsp &nbsp &nbsp &nbsp &nbsp &nbsp &nbsp &nbsp &nbsp &nbsp &nbsp &nbsp &nbsp &nbsp &nbsp &nbsp &nbsp &nbsp &nbsp &nbsp &nbsp &nbsp &nbsp &nbsp &nbsp &nbsp<asp:LinkButton ID="linkAddCate" CommandName="AddDesc" runat="server">Add</asp:LinkButton>
                                </FooterTemplate>
                            </asp:TemplateField>
                            <asp:CommandField HeaderText="Edit" HeaderStyle-CssClass="alt1" CausesValidation=false ShowEditButton="True" />
                            
                            <asp:TemplateField HeaderText="Delete" HeaderStyle-CssClass="alt1">
                                <ItemTemplate>
                                    <%--<asp:LinkButton ID="linkDeleteCate" CommandName=<%#IIf(IsDBNULL(Eval("IsDeleted")) OR (Eval("IsDeleted")=0),"Deleted","Active") %> runat="server" CausesValidation=false><%#IIf(IsDBNull(Eval("IsDeleted")) Or (Eval("IsDeleted") = 0), "Deleted", "Active")%></asp:LinkButton>--%>
                                    <asp:LinkButton ID="linkDeleteCate" CommandName="Delete" runat="server" CausesValidation=false>Delete</asp:LinkButton>
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
    </div> 
    </div> 
    </form>
</body>
</html>
