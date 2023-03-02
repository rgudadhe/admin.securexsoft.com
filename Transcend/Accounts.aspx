<%@ Page Language="VB" AutoEventWireup="false" CodeFile="Accounts.aspx.vb" Inherits="Transcend_Accounts" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="Ajax" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Untitled Page</title>
    <link href= "../App_Themes/Css/Main.css" type="text/css" rel="stylesheet" />
    <link href="../App_Themes/Css/Common.css" type="text/css" rel="stylesheet"  />
    <link href= "../App_Themes/Css/Styles.css" type="text/css" rel="stylesheet" />
    <link href= "../App_Themes/Css/Calendar.css" type="text/css" rel="stylesheet" />
    <script type="text/javascript" language="javascript">
        function Enable(str)
        {
            alert(str);
            //alert(document.getElementById(str).value);
            //alert(document.getElementById('txtCID'));
            var box = $find('txtCID');
            alert(box);
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        <div id="body">
    <div id="cap"></div>
    <div id="main">
    <h1>Accounts</h1>
    <div style="text-align:left">
        <asp:Table ID="tblMain" runat="server" BorderColor="LightBlue" BorderWidth="0" Width="95%" BorderStyle="None">
            <asp:TableRow>
                <asp:TableCell>
                    <asp:GridView ID="GridViewStages" ShowFooter="true" AutoGenerateColumns="false" CssClass="common"  runat="server" Width="100%" AllowSorting="true" AllowPaging="true" PageSize="20" >
                        <Columns>
                            <asp:TemplateField HeaderText="AccName"  SortExpression="AccName" ItemStyle-Width="50%" ItemStyle-HorizontalAlign="Left" HeaderStyle-CssClass="alt1"> 
                                <HeaderStyle BackColor="#F7F7F7" HorizontalAlign="Center"/>
                                <FooterStyle HorizontalAlign="Left"/>
                                <EditItemTemplate> 
                                    <asp:TextBox ID="txtAccEdit" CssClass="common"  runat="server" Text='<%# Eval("AccName") %>' Width="97%"></asp:TextBox> 
                                    <asp:HiddenField ID="AccID" Value='<%#Eval("AccID").ToString %>' runat="server"  />                                
                                </EditItemTemplate> 
                                <FooterTemplate> 
                                    <asp:TextBox ID="txtAcc" CssClass="common" runat="server" Width="97%" ></asp:TextBox> 
                                </FooterTemplate> 
                                <ItemTemplate> 
                                    <asp:Label ID="Label3" CssClass="common" runat="server" Text='<%# Eval("AccName") %>' Width="97%"></asp:Label> 
                                    <asp:HiddenField ID="AccIDDel" Value='<%#Eval("AccID").ToString %>' runat="server"  />                                
                                </ItemTemplate> 
                            </asp:TemplateField>  
                            <asp:TemplateField HeaderText="Type" SortExpression="Type" ItemStyle-Width="20%" ItemStyle-HorizontalAlign="Left" HeaderStyle-CssClass="alt1">
                                <ItemTemplate>
                                    <asp:Label ID="Label5" CssClass="common" runat="server" Text='<%# Eval("Type") %>' Width="97%"></asp:Label> 
                                </ItemTemplate>                                
                                <EditItemTemplate>
                                    <asp:DropDownList ID="ddlType" runat="server" CssClass="common" SelectedValue= '<%#IIf(ISDBNULL(Eval("Type").ToString),"",Eval("Type").ToString)%>'>
                                        <asp:ListItem Text="Please Select" Value=""></asp:ListItem>
                                        <asp:ListItem Text="BeyondTXT" Value="BeyondTXT"></asp:ListItem>
                                        <asp:ListItem Text="eScription" Value="eScription"></asp:ListItem>
                                    </asp:DropDownList>  
                                </EditItemTemplate>
                                <FooterTemplate>
                                    <asp:DropDownList ID="FddlType" runat="server" CssClass="common" >
                                        <asp:ListItem Text="Please Select" Value=""></asp:ListItem>
                                        <asp:ListItem Text="BeyondTXT" Value="BeyondTXT"></asp:ListItem>
                                        <asp:ListItem Text="eScription" Value="eScription"></asp:ListItem>
                                    </asp:DropDownList> 
                                </FooterTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="CustomerID" SortExpression="CustomerID" ItemStyle-Width="20%" ItemStyle-HorizontalAlign="Left" HeaderStyle-CssClass="alt1">
                                <ItemTemplate>
                                    <asp:Label ID="Label6"  runat="server" Text='<%# Eval("CustomerID") %>' Width="97%"></asp:Label>                                     
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:TextBox ID="txtCID" CssClass="common"  runat="server" Text='<%# Eval("CustomerID") %>' Width="95%"></asp:TextBox> 
                                </EditItemTemplate>
                                <FooterTemplate>
                                    <asp:TextBox ID="FtxtCID" CssClass="common"  runat="server" Width="95%"></asp:TextBox> 
                                </FooterTemplate>                                                                
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Edit" ShowHeader="False">
                                <ItemStyle Width="12%" /> 
                                <FooterStyle Width="12%" />
                                <EditItemTemplate> 
                                    <asp:LinkButton ID="LinkButton1" runat="server" CssClass="common" CausesValidation="False" CommandName="Update" Text="Update"></asp:LinkButton> 
                                    <asp:LinkButton ID="LinkButton2" runat="server" CssClass="common" CausesValidation="False" CommandName="Cancel" Text="Cancel"></asp:LinkButton> 
                                </EditItemTemplate> 
                                <FooterTemplate> 
                                    <asp:LinkButton ID="LinkButton2" runat="server" CssClass="common" CausesValidation="True" CommandName="AddNew" Text="Add New"></asp:LinkButton> 
                                </FooterTemplate> 
                                <ItemTemplate> 
                                    <asp:LinkButton ID="LinkButton1" runat="server" CssClass="common" CausesValidation="False" CommandName="Edit" Text="Edit"></asp:LinkButton> 
                                </ItemTemplate> 
                            </asp:TemplateField> 
                            <asp:TemplateField HeaderText="Delete">
                                <ItemTemplate>
                                    <asp:LinkButton ID="LnkDelete" runat="server" CausesValidation="False" CommandArgument='<%#Eval("AccID").ToString %>' CommandName="Delete" Text="Delete" />                    
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </asp:TableCell>
            </asp:TableRow>
        </asp:Table>
        <table>
            <tr>
                <td>
                    <asp:Label ID="ErrLabel" runat="server" Text="" Font-Names="Trebuchet MS" Font-Size="10pt" Font-Italic="true" Font-Bold="true" ForeColor="red"></asp:Label>
                </td>
            </tr>
        </table>
    </div>
    </div>
    </div> 
    <asp:HiddenField ID="Hsort" runat="server" />
    <asp:HiddenField ID="Horder" runat="server" />
    </form>
</body>
</html>
