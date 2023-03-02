<%@ Page Language="VB" AutoEventWireup="false" CodeFile="formDelDictator.aspx.vb"
    Inherits="tollfree_formDelDictator" %>
<%@ Register
    Assembly="AjaxControlToolkit"
    Namespace="AjaxControlToolkit"
    TagPrefix="ajaxToolkit" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        .style1
        {
            width: 100%;
        }
    </style>
    <script type="text/javascript" src="https://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
<script type="text/javascript" src="scripts/jquery.blockUI.js"></script>
<script type="text/javascript">
    $(function () {
        BlockUI("dvGrid");
        $.blockUI.defaults.css = {};
    });
    function BlockUI(elementID) {
        var prm = Sys.WebForms.PageRequestManager.getInstance();
        prm.add_beginRequest(function () {
            $("#" + elementID).block({ message: '<div align = "center">' + '<img src="images/loadingAnim.gif"/></div>',
                css: {},
                overlayCSS: { backgroundColor: '#000000', opacity: 0.6, border: '3px solid #63B2EB' }
            });
        });
        prm.add_endRequest(function () {
            $("#" + elementID).unblock();
        });
    };
</script>
</head>
<body>
    <form id="form1" runat="server">
   <div>
   
    <asp:DropDownList ID="ddlAccounts" runat="server" AutoPostBack="true">
    <asp:ListItem Text="Select Account" Selected="True" Value="0" ></asp:ListItem>
    </asp:DropDownList>
   </div>
<div id="dvGrid" style="padding: 10px; width: 82%">
          <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="false"  OnRowDataBound="OnRowDataBound"
            DataKeyNames="id" OnRowEditing="OnRowEditing" OnRowCancelingEdit="GridView1_RowCancelingEdit"  PageSize = "200" AllowPaging ="true" OnPageIndexChanging = "OnPaging" OnRowUpdating="OnRowUpdating" OnRowDeleting="OnRowDeleting"
            EmptyDataText="No records has been added."
            Width="100%">
            <Columns>
             <asp:CommandField ButtonType="Link" ShowEditButton="true" ShowDeleteButton="true" ShowCancelButton="true"
                    ItemStyle-Width="150" />
                <asp:TemplateField HeaderText="First Name" ItemStyle-Width="150">
                    <ItemTemplate>
                        <asp:Label ID="lblFName" runat="server" Text='<%# Eval("dicfname") %>'></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:TextBox ID="txtFName" runat="server" Text='<%# Eval("dicfname") %>' Width="140"></asp:TextBox>
                    </EditItemTemplate>
                    
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Last Name" ItemStyle-Width="150">
                    <ItemTemplate>
                        <asp:Label ID="lblLName" runat="server" Text='<%# Eval("diclname") %>'></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:TextBox ID="txtLName" runat="server" Text='<%# Eval("diclname") %>' Width="140"></asp:TextBox>
                    </EditItemTemplate>
                    
                </asp:TemplateField>
                 <asp:TemplateField HeaderText="Keypad" ItemStyle-Width="150">
                    <ItemTemplate>
                        <asp:Label ID="lblKeypad" runat="server" Text='<%# Eval("keypad") %>'></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:TextBox ID="txtKeypad" runat="server" Text='<%# Eval("keypad") %>' Width="140"></asp:TextBox>
                    </EditItemTemplate>
                    
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Id" ItemStyle-Width="150">
                    <ItemTemplate>
                        <asp:Label ID="lblid" runat="server" Text='<%# Eval("id") %>'></asp:Label>
                    </ItemTemplate>
                  
                </asp:TemplateField>
                 <asp:TemplateField HeaderText="Partition" ItemStyle-Width="150">
                    <ItemTemplate>
                        <asp:Label ID="lblPartition" runat="server" Text='<%# Eval("partition") %>'></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:TextBox ID="txtPartition" runat="server" Text='<%# Eval("partition") %>' Width="140"></asp:TextBox>
                    </EditItemTemplate>
                    
                </asp:TemplateField>
               
            </Columns>
        </asp:GridView>
        <%--<table border="1" cellpadding="0" cellspacing="0" style="border-collapse: collapse">
            <tr>
                <td style="width: 150px">
                    Name:<br />
                    <asp:TextBox ID="txtName" runat="server" Width="140" />
                </td>
                <td style="width: 150px">
                    Country:<br />
                    <asp:TextBox ID="txtCountry" runat="server" Width="140" />
                </td>
                <td style="width: 150px">
                    <asp:Button ID="btnAdd" runat="server" Text="Add" OnClick="Insert" />
                </td>
            </tr>
        </table>--%>
  
</div>
    </form>
</body>
</html>
