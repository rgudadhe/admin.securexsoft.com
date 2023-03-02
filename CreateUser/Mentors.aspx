<%@ Page Language="VB" AutoEventWireup="false" CodeFile="Mentors.aspx.vb" Inherits="CreateUser_Mentors" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Mentors</title>
    <link href= "../App_Themes/Css/styles.css" type="text/css" rel="stylesheet" />
    <link href= "../App_Themes/Css/Common.css" type="text/css" rel="stylesheet" />

</head>
<body>
    <form id="form1" runat="server">
        <div id="body">
            <div id="cap"></div>
            <div id="main">
                <h1>Add Mentors</h1>
                <asp:Panel ID="Panel1" runat="server" HorizontalAlign="Left">
                    <div style="text-align:left">
                        <asp:Label ID="lblMsg" runat="server" Text="" CssClass="common" ForeColor="Red"></asp:Label>
                        <asp:Table ID="tblCancel" runat="server" Width="500">
                            <asp:TableRow>
                                <asp:TableCell CssClass="HeaderDiv" HorizontalAlign="Center">
                                   Mentors
                                </asp:TableCell>
                            </asp:TableRow>
                            <asp:TableRow>
                                <asp:TableCell>
                                    <asp:GridView ID="GridViewMain" EnableSortingAndPagingCallbacks="false" runat="server" AutoGenerateColumns="False" AllowPaging="true" AllowSorting="true" ShowFooter="true" ShowHeader="true" EmptyDataText="No Records found"  Width="100%" EnableViewState="true" PageSize="25">
                                        <AlternatingRowStyle BackColor="OldLace" />
                                        <Columns>
                                            <asp:TemplateField ItemStyle-Width="1" HeaderStyle-CssClass="alt1">
                                                <ItemTemplate>
                                                    <asp:HiddenField ID="UserId" Value='<%#Eval("UserID") %>' runat="server"  />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField ItemStyle-Width="5" HeaderText="SrNo" SortExpression="SrNo" HeaderStyle-CssClass="alt1">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblSrNo" Text='<%# Container.DataItemIndex + 1 %> ' runat="server"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Mentor" HeaderStyle-HorizontalAlign="Center" SortExpression="Comment" ItemStyle-Width="500" HeaderStyle-CssClass="alt1">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblMentor" Text='<%# Eval("Mentor") %>' CssClass="common" runat="server"></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:DropDownList ID="ddlUsrs" EnableViewState="true" runat="server">
                                                    </asp:DropDownList>
                                                    <asp:RequiredFieldValidator  Display="None" ID="RequiredFieldCateName" runat="server" ErrorMessage="Please User" ControlToValidate="ddlUsrs" ></asp:RequiredFieldValidator>
                                                    &nbsp &nbsp &nbsp &nbsp &nbsp &nbsp &nbsp &nbsp &nbsp &nbsp &nbsp &nbsp &nbsp &nbsp &nbsp &nbsp &nbsp &nbsp &nbsp &nbsp &nbsp &nbsp &nbsp &nbsp &nbsp &nbsp &nbsp &nbsp &nbsp &nbsp &nbsp &nbsp &nbsp &nbsp &nbsp &nbsp &nbsp &nbsp &nbsp &nbsp &nbsp &nbsp<asp:LinkButton ID="linkAddUsr" CommandName="AddUsr" runat="server">Add</asp:LinkButton>
                                                </FooterTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Delete Mentor" HeaderStyle-HorizontalAlign="Center" HeaderStyle-CssClass="alt1">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="DeleteM" CommandName="DeleteM" runat="server" CausesValidation="false">Delete</asp:LinkButton> 
                                                </ItemTemplate>
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
