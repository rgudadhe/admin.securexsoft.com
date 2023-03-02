<%@ Page Language="VB" AutoEventWireup="false" CodeFile="EditCustomAttributes.aspx.vb" Inherits="EditCustomAttributes" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>Manage Attributes</title>
   <link href= "../App_Themes/Css/styles.css" type="text/css" rel="stylesheet" />
   <script type="text/javascript">
       function customOpen(url) {
           var w = window.open(url, '', 'width=600,height=600,toolbar=0,status=0,location=0,menubar=0,directories=0,resizable=1,scrollbars=1');
           w.focus();

       }
</script>
</head>
<body>
    <form id="form1" runat="server">
    <div id="body">
    <div id="cap"></div>
    <div id="main">
    <h1>Edit Custom Attributes</h1>
<ajaxToolkit:ToolkitScriptManager runat="server" ID="ScriptManager1"/>    
<%--<asp:UpdatePanel runat="server" ID="up2">
        <ContentTemplate>  --%>          
            <asp:Panel ID="Panel1" runat="server" HorizontalAlign="Left">
                <asp:Repeater ID="rptCon" runat="server"  >
                         <HeaderTemplate>
                            <table>            
                            <tr>
                                <td colspan="4" class="HeaderDiv">
                                    Attributes Details
                                </td>
                            </tr>
                            <tr>
                                <td class="alt1">Attribute Name</td>            
                                <td class="alt1">Caption</td>
                                <td class="alt1">Type</td>            
                              <td class="alt1">Delete</td>  
                            </tr>
                </HeaderTemplate>

                <ItemTemplate>
                <tr>
                            <td><asp:TextBox ID="txtAttribName" runat="server" Text='<%#Container.DataItem("Name")%>' ontextchanged="change" AutoPostBack="true"></asp:TextBox><asp:HiddenField runat=server ID="hdnAttribID" Value='<%#Container.DataItem("AttributeID")%>'/>
                            <asp:HiddenField runat=server ID="HidCaption" Value='<%#Container.DataItem("Caption")%>'/> </td>            
                            <td><asp:TextBox ID="txtCaption" runat="server" Text='<%#Container.DataItem("Caption")%>' ontextchanged="change" AutoPostBack="true"></asp:TextBox></td>
                            <td><asp:DropDownList ID="DDType" runat="server"  OnSelectedIndexChanged="DDType_SelectedIndexChanged" AutoPostBack="true" Visible="false">            
                            <asp:ListItem Selected="True" Value="">Please Select</asp:ListItem>
                            <asp:ListItem Value="TextBox">TextBox</asp:ListItem>
                            <asp:ListItem Value="DropDown">DropDown</asp:ListItem>
                          <%--  <asp:ListItem Value="2">DateTime</asp:ListItem>
                            <asp:ListItem Value="3">Boolean</asp:ListItem>
                            <asp:ListItem Value="4">Raw</asp:ListItem>
                            <asp:ListItem Value="5">Options</asp:ListItem>--%>
                            </asp:DropDownList>            
                         <asp:Label ID="lblType" runat="server" Width="150px" Text='<%#Container.DataItem("ControlType")%>'></asp:Label>        
                             <asp:HiddenField ID="hdnTypeNo" runat="server" Value='<%#Container.DataItem("ControlType") %>'/>   
                                    
                          <%--  <asp:Button ID="iPopUp" CssClass="button" runat="server" Text="..." OnClick="iPopUp_Click" Height="18" ToolTip="Click here to change Data Type"  />--%>
                            <asp:Button ID="iPopUpOp" CssClass="button" runat="server" Text="..." OnClick="iPopUpOp_Click" Height="18" ToolTip="Click here to edit options" Visible='<%#iif(Container.DataItem("ControlType")="DropDown",True,False)%>' />
                            </td>
                       <td>
                                <asp:CheckBox ID="chkDelete" Checked='<%#Container.DataItem("Deleted")%>'  runat="server"  OnCheckedChanged="changeCHK" AutoPostBack="true"/>
                            </td>            
                        
                </tr>
                </ItemTemplate>
              
                <FooterTemplate>
                </table>
                </FooterTemplate>
                </asp:Repeater>
            </asp:Panel>
         
<%--</ContentTemplate>        
</asp:UpdatePanel> --%>
</div> 
</div> 
</form>
</body>
</html>
