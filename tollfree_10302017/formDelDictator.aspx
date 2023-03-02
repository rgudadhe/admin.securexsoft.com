<%@ Page Language="VB" AutoEventWireup="false" CodeFile="formDelDictator.aspx.vb" Inherits="tollfree_formDelDictator" %>

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
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
        <br />
        <table class="style1">
            <tr>
                <td>
                    <asp:Label ID="Label1" runat="server" Font-Bold="True" Font-Names="Segoe UI" 
                        Font-Size="Small" Text="Select Account"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:DropDownList ID="DropDownList1" runat="server" AutoPostBack="True" 
                        DataSourceID="SqlDataSource1" DataTextField="Accname" DataValueField="custid" 
                        Font-Names="Segoe UI" Font-Size="Small">
                    </asp:DropDownList>
                    <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
                        ConnectionString="<%$ ConnectionStrings:tollfreeConnectionString %>" 
                        SelectCommand="SELECT DISTINCT [Accname], [custid] FROM [tbltollfree] ORDER BY [Accname]">
                    </asp:SqlDataSource>
                </td>
            </tr>
            <tr>
                <td>
                    &nbsp;</td>
            </tr>
            <tr>
                <td>
                    <asp:GridView ID="GridView1" runat="server" AllowPaging="True" 
                        AllowSorting="True" AutoGenerateColumns="False" CellPadding="4" 
                        DataSourceID="SqlDataSource2" EnableModelValidation="True" 
                        Font-Names="Segoe UI" Font-Size="Small" ForeColor="#333333" GridLines="None" 
                        PageSize="30" DataKeyNames="id">
                        <AlternatingRowStyle BackColor="White" />
                        <Columns>
                            <asp:CommandField ShowDeleteButton="True" ShowEditButton="True" />
                            <asp:BoundField DataField="Accname" HeaderText="Accname" 
                                SortExpression="Accname" />
                            <asp:BoundField DataField="diclname" HeaderText="diclname" 
                                SortExpression="diclname" />
                            <asp:BoundField DataField="dicfname" HeaderText="dicfname" 
                                SortExpression="dicfname" />
                            <asp:BoundField DataField="id" HeaderText="id" ReadOnly="True" 
                                SortExpression="id" />
                            <asp:BoundField DataField="password" HeaderText="password" 
                                SortExpression="password" />
                            <asp:BoundField DataField="partition" HeaderText="partition" 
                                SortExpression="partition" />
                            <asp:BoundField DataField="partitionno" HeaderText="partitionno" 
                                SortExpression="partitionno" />
                        </Columns>
                        <EditRowStyle BackColor="#2461BF" />
                        <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                        <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                        <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                        <RowStyle BackColor="#EFF3FB" />
                        <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                    </asp:GridView>
                    <asp:SqlDataSource ID="SqlDataSource2" runat="server" 
                        ConnectionString="<%$ ConnectionStrings:tollfreeConnectionString %>" 
                        DeleteCommand="DELETE FROM [tbltollfree] WHERE [id] = @id" 
                        InsertCommand="INSERT INTO [tbltollfree] ([Accname], [diclname], [dicfname], [id], [password], [partition], [partitionno]) VALUES (@Accname, @diclname, @dicfname, @id, @password, @partition, @partitionno)" 
                        SelectCommand="SELECT [Accname], [diclname], [dicfname], [id], [password], [partition], [partitionno] FROM [tbltollfree] WHERE ([custid] = @custid)" 
                        UpdateCommand="UPDATE [tbltollfree] SET [Accname] = @Accname, [diclname] = @diclname, [dicfname] = @dicfname, [password] = @password, [partition] = @partition, [partitionno] = @partitionno WHERE [id] = @id">
                        <DeleteParameters>
                            <asp:Parameter Name="id" Type="Decimal" />
                        </DeleteParameters>
                        <InsertParameters>
                            <asp:Parameter Name="Accname" Type="String" />
                            <asp:Parameter Name="diclname" Type="String" />
                            <asp:Parameter Name="dicfname" Type="String" />
                            <asp:Parameter Name="id" Type="Decimal" />
                            <asp:Parameter Name="password" Type="Decimal" />
                            <asp:Parameter Name="partition" Type="String" />
                            <asp:Parameter Name="partitionno" Type="Decimal" />
                        </InsertParameters>
                        <SelectParameters>
                            <asp:ControlParameter ControlID="DropDownList1" Name="custid" 
                                PropertyName="SelectedValue" Type="String" />
                        </SelectParameters>
                        <UpdateParameters>
                            <asp:Parameter Name="Accname" Type="String" />
                            <asp:Parameter Name="diclname" Type="String" />
                            <asp:Parameter Name="dicfname" Type="String" />
                            <asp:Parameter Name="password" Type="Decimal" />
                            <asp:Parameter Name="partition" Type="String" />
                            <asp:Parameter Name="partitionno" Type="Decimal" />
                            <asp:Parameter Name="id" Type="Decimal" />
                        </UpdateParameters>
                    </asp:SqlDataSource>
                </td>
            </tr>
        </table>
        <br />
    
    </div>
    </form>
</body>
</html>
