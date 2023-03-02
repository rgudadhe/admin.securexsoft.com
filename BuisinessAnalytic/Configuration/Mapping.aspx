<%@ Page Language="VB" AutoEventWireup="false" CodeFile="Mapping.aspx.vb" Inherits="Transcend_Mapping" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Transcend Mapping</title>
    <link rel="stylesheet" type="text/css" href="../../App_Themes/Css/styles1.css"/>
<link rel="stylesheet" type="text/css" href="../../App_Themes/Css/Common.css"/>
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
    
    <asp:Panel ID="Panel2" HorizontalAlign="Left"  runat="server" width="100%">
         
       <%-- <table width="70%" border="0">
          
            <tr>
                <td colspan="4">
                    <asp:LinkButton ID="LnlExport" runat="server" CssClass="common"  >Export Mapping List</asp:LinkButton>
                </td>
            </tr>
        </table>--%>
        <asp:Label ID="lblResponse" runat="server" Text="" CssClass="common" ></asp:Label>
        <br />
        <asp:Table ID="tblView" runat="server" Width="60%" CssClass="common" BorderStyle="None" BorderWidth="0" >
            <asp:TableRow>
                <asp:TableCell>
                    <asp:GridView ID="GrdViewData" runat="server" AllowPaging="false" AllowSorting="true" CssClass="common" AutoGenerateColumns="false" PageSize="10" Width="100%" OnRowDeleting="GrdViewData_RowDeleting" OnRowCancelingEdit="GrdViewData_RowCancelingEdit" OnRowEditing="GrdViewData_RowEditing" OnRowUpdating="GrdViewData_RowUpdating" ShowFooter="true" ShowHeader="true">
                    <Columns>
                        <asp:TemplateField HeaderText="UserName" SortExpression="UserName" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left" HeaderStyle-CssClass="alt1">
                            <ItemTemplate>
                                <%#Eval("UserName").ToString%>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:TextBox ID="txtUserName" CssClass="common" runat="server" Text='<%#Bind("UserName")%>'></asp:TextBox>
                                <asp:HiddenField ID="hdnAutoID" Value='<%#Bind("AutoID")%>' runat="server" />
                             </EditItemTemplate>
                             <FooterTemplate>
                                <asp:TextBox ID="FtxtUserName" CssClass="common" runat="server"></asp:TextBox>
                            </FooterTemplate>
                        </asp:TemplateField>
                        
                        <asp:TemplateField HeaderText="MTID" SortExpression="MTID" HeaderStyle-HorizontalAlign="Center" HeaderStyle-CssClass="alt1">
                            <ItemTemplate>
                                <%#Eval("MTID").ToString%>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:TextBox ID="txtMTID" CssClass="common" runat="server" Text='<%#Bind("MTID")%>'></asp:TextBox>
                            </EditItemTemplate>
                            <FooterTemplate>
                                <asp:TextBox ID="FtxtMTID" CssClass="common" runat="server"></asp:TextBox>
                            </FooterTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="QAID" SortExpression="QAID" HeaderStyle-HorizontalAlign="Center" HeaderStyle-CssClass="alt1">
                            <ItemTemplate>
                                <%#Eval("QAID").ToString%>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:TextBox ID="txtQAID" CssClass="common" runat="server" Text='<%#Bind("QAID")%>'></asp:TextBox>
                            </EditItemTemplate>
                            <FooterTemplate>
                                <asp:TextBox ID="FtxtQAID" CssClass="common" runat="server"></asp:TextBox>
                            </FooterTemplate>
                        </asp:TemplateField>
                        
                        <asp:TemplateField HeaderText="QABID" SortExpression="QABID" HeaderStyle-HorizontalAlign="Center" HeaderStyle-CssClass="alt1">
                            <ItemTemplate>
                                <%#Eval("QABID").ToString%>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:TextBox ID="txtQABID" CssClass="common" runat="server" Text='<%#Bind("QABID")%>'></asp:TextBox>
                            </EditItemTemplate>
                            <FooterTemplate>
                                <asp:TextBox ID="FtxtQABID" CssClass="common" runat="server"></asp:TextBox>
                            </FooterTemplate>
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
                                <asp:LinkButton ID="LnkDelete" runat="server" CausesValidation="False" CommandArgument='<%#Eval("AutoID").ToString %>' CommandName="Delete" Text="Delete" />                    
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                    </asp:GridView>
                </asp:TableCell>
            </asp:TableRow>     
        </asp:Table>
        <br />
       </asp:Panel>
       </div>
       </div>
       </form>
</body>
</html>
