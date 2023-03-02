<%@ Page Language="VB" AutoEventWireup="false" CodeFile="formaddprompt.aspx.vb" Inherits="tollfree_formaddprompt" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
        <asp:DropDownList ID="DropDownList1" runat="server" AutoPostBack="True" 
            DataSourceID="SqlDataSource1" DataTextField="Accname" DataValueField="custid">
        </asp:DropDownList>
        <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
            ConnectionString="<%$ ConnectionStrings:tollfreeConnectionString %>" 
            SelectCommand="SELECT * FROM [tblaccounts]"></asp:SqlDataSource>
        <asp:FormView ID="FormView1" runat="server" DataSourceID="SqlDataSource2" 
            EnableModelValidation="True">
            <EditItemTemplate>
                Accname:
                <asp:TextBox ID="AccnameTextBox" runat="server" Text='<%# Bind("Accname") %>' />
                <br />
                Prompt:
                <asp:TextBox ID="PromptTextBox" runat="server" Text='<%# Bind("Prompt") %>' />
                <br />
                KeyIn:
                <asp:TextBox ID="KeyInTextBox" runat="server" Text='<%# Bind("KeyIn") %>' />
                <br />
                Description:
                <asp:TextBox ID="DescriptionTextBox" runat="server" 
                    Text='<%# Bind("Description") %>' />
                <br />
                custid:
                <asp:TextBox ID="custidTextBox" runat="server" Text='<%# Bind("custid") %>' />
                <br />
                <asp:LinkButton ID="UpdateButton" runat="server" CausesValidation="True" 
                    CommandName="Update" Text="Update" />
                &nbsp;<asp:LinkButton ID="UpdateCancelButton" runat="server" 
                    CausesValidation="False" CommandName="Cancel" Text="Cancel" />
            </EditItemTemplate>
            <InsertItemTemplate>
                Accname:
                <asp:TextBox ID="AccnameTextBox" runat="server" Text='<%# Bind("Accname") %>' />
                <br />
                Prompt:
                <asp:TextBox ID="PromptTextBox" runat="server" Text='<%# Bind("Prompt") %>' />
                <br />
                KeyIn:
                <asp:TextBox ID="KeyInTextBox" runat="server" Text='<%# Bind("KeyIn") %>' />
                <br />
                Description:
                <asp:TextBox ID="DescriptionTextBox" runat="server" 
                    Text='<%# Bind("Description") %>' />
                <br />
                custid:
                <asp:TextBox ID="custidTextBox" runat="server" Text='<%# Bind("custid") %>' />
                <br />
                <asp:LinkButton ID="InsertButton" runat="server" CausesValidation="True" 
                    CommandName="Insert" Text="Insert" />
                &nbsp;<asp:LinkButton ID="InsertCancelButton" runat="server" 
                    CausesValidation="False" CommandName="Cancel" Text="Cancel" />
            </InsertItemTemplate>
            <ItemTemplate>
                Accname:
                <asp:Label ID="AccnameLabel" runat="server" Text='<%# Bind("Accname") %>' />
                <br />
                Prompt:
                <asp:Label ID="PromptLabel" runat="server" Text='<%# Bind("Prompt") %>' />
                <br />
                KeyIn:
                <asp:Label ID="KeyInLabel" runat="server" Text='<%# Bind("KeyIn") %>' />
                <br />
                Description:
                <asp:Label ID="DescriptionLabel" runat="server" 
                    Text='<%# Bind("Description") %>' />
                <br />
                custid:
                <asp:Label ID="custidLabel" runat="server" Text='<%# Bind("custid") %>' />
                <br />

            </ItemTemplate>
        </asp:FormView>
        <asp:SqlDataSource ID="SqlDataSource2" runat="server" 
            ConnectionString="<%$ ConnectionStrings:tollfreeConnectionString %>" 
            SelectCommand="SELECT * FROM [tblPrompts] WHERE ([custid] = @custid)">
            <SelectParameters>
                <asp:ControlParameter ControlID="DropDownList1" Name="custid" 
                    PropertyName="SelectedValue" Type="String" />
            </SelectParameters>
        </asp:SqlDataSource>
        <br />
    
    </div>
    </form>
</body>
</html>
