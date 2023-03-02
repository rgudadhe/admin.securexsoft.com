<%@ Page Language="VB" AutoEventWireup="false" CodeFile="AccountAssignments.aspx.vb" Inherits="Account_AccountAssignments" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Account Assignment</title>
    <link href= "../App_Themes/Css/styles.css" type="text/css" rel="stylesheet" />
    <link href= "../App_Themes/Css/Default.css" type="text/css" rel="stylesheet" />
    <link href= "../App_Themes/Css/Common.css" type="text/css" rel="stylesheet" />
    <script type="text/javascript" language="javascript">
        function chkALL(str)
        {
            for(i = 0; i < form1.elements.length; i++)
            {
                elm = document.forms[0].elements[i]
                if (elm.type == 'checkbox')
                {
                    if (elm.id.indexOf("chkUsr")>0)
                        elm.checked = str.checked;
                }
            }
        }
        function chkALLAcc(str)
        {
            for(i = 0; i < form1.elements.length; i++)
            {
                elm = document.forms[0].elements[i]
                if (elm.type == 'checkbox')
                {
                    if (elm.id.indexOf("chkAcc")>0)
                        elm.checked = str.checked;
                }
            }
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <table width="100%" border="0">
            <tr>
                <td colspan="2" align="left" valign="top" style="border:0">
                    Level : 
                    <asp:DropDownList ID="ddlLevels" runat="server" Width="150" AppendDataBoundItems="true">
                        <asp:ListItem Text="Please Select" Value=""></asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td style="width:50%;border:0">
                    Account : 
                    <asp:TextBox ID="txtAcc" runat="server"></asp:TextBox>
                    <asp:Button ID="btnSearchAcc" runat="server" Text="Search Account" CssClass="button" />
                </td>
                <td style="width:50%;border:0">
                    User : 
                    <asp:TextBox ID="txtUsr" runat="server"></asp:TextBox>
                    <asp:Button ID="btnSeachUsr" runat="server" Text="Search User" CssClass="button" />
                </td>
            </tr>
            <tr>
                <td style="border:0" colspan="2" align="left" valign="top">
                    <asp:Label ID="lblMsg" runat="server" Text="" CssClass="Title" ForeColor="Firebrick"></asp:Label>
                </td>
            </tr>
            <tr>
                <td style="text-align:left; border:0" valign="top" align="left">
                    <asp:GridView ID="GrdAccounts" runat="server" AutoGenerateColumns="false" Width="95%">
                        <Columns>
                            <asp:TemplateField headertext="" HeaderStyle-CssClass="alt1" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center">
                                <HeaderTemplate>
                                    <asp:CheckBox ID="ChkALLAcc" runat="server" OnClick="chkALLAcc(this);" />
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <center>
                                        <asp:CheckBox ID="chkAcc" runat="server" />
                                    </center>
                                    <asp:HiddenField ID="hdnAccID" Value=<%#Eval("AccountID").ToString()%> runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField headertext="Account Name" HeaderStyle-CssClass="alt1">
                                <ItemTemplate>
                                    <asp:Label id="lblAcc" runat="server" Text='<%#Eval("AccountName")%>'></asp:Label>    
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>                        
                    </asp:GridView>
                </td>
                <td style="text-align:left; border:0" valign="top" align="left">
                    <asp:GridView ID="GrdUsr" runat="server" AutoGenerateColumns="false" Width="95%">
                        <Columns>
                            <asp:TemplateField headertext="" HeaderStyle-CssClass="alt1" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center">
                                <HeaderTemplate>
                                    <asp:CheckBox ID="ChkALL" runat="server" OnClick="chkALL(this);" />
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <center>
                                        <asp:CheckBox ID="chkUsr" runat="server" />
                                    </center>
                                    <asp:HiddenField ID="hdnUserID" Value=<%#Eval("UserID").ToString()%> runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField headertext="Emp/HBA Name" HeaderStyle-CssClass="alt1">
                                <ItemTemplate>
                                    <asp:Label id="lblUsr" runat="server" Text='<%#Eval("EmpName")%>'></asp:Label>    
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </td>
            </tr>
        </table>
        <center>
            <asp:Button ID="btnAssign" runat="server" Text="Assign Account" CssClass="button" Visible="false" />
        </center>
    </div>
    </form>
</body>
</html>
