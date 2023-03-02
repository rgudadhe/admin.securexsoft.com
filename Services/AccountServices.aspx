<%@ Page Language="VB" ValidateRequest="false" AutoEventWireup="false" CodeFile="AccountServices.aspx.vb" Inherits="AccountServices" %>
<%@ Register
    Assembly="AjaxControlToolkit"
    Namespace="AjaxControlToolkit"
    TagPrefix="ajaxToolkit" %>
    
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 1.0 Transitional/EN">
<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Untitled Page</title>
    <link href= "../styles/Default.css" type="text/css" rel="stylesheet"/>    
    <script language="javascript">
    function SetUniqueRadioButton(nameregex, current)
{
   re = new RegExp(nameregex);
   for(i = 0; i < document.forms[0].elements.length; i++)
   {
      elm = document.forms[0].elements[i]
      if (elm.type == 'radio')
      {
         if (re.test(elm.name))
         {
            elm.checked = false;
         }
      }
   }
   current.checked = true;
}

    </script>
</head>
<body >
    <form id="form1" runat="server" EncType="Multipart/Form-Data" >
    <ajaxToolkit:ToolkitScriptManager ID="ScriptManager1" EnablePartialRendering="true" EnableScriptGlobalization="true" EnableScriptLocalization="true" runat="Server" />
    <%--<asp:UpdatePanel ID="pnlInstr" runat="server">
    <ContentTemplate>--%>
    <asp:MultiView ID="MultiView1" runat="server">
    <asp:View ID="vSearch" runat="server">
    <div>
    <p>  
    <br />
    <br />  
                  <asp:DropDownList ID="DDLAccounts" runat="server" Width="264px" TabIndex="1" >                      
                  </asp:DropDownList>            
                  <asp:Button ID="btnGO" runat="server" Text="Search"  />        
    </p>    
    </div>  
    </asp:View>
    <asp:View ID="vDetails" runat="server">
        <div style="text-align: center">
        
        <asp:Repeater ID="rptLevels" runat="server">
            <HeaderTemplate>
                <table border="1" style="vertical-align: middle; text-align: left;">                
                    <tr>
                    <td colspan=4 class="HeaderDiv">
                        <%#uName%>
                    </td>
                    </tr>
                    <tr bgcolor="#3399cc">
                        <th class="SMSelected">
                        </th> 
                        <th class="SMSelected">
                            Service Name
                        </th>                                               
                         <th class="SMSelected">                           
                            Description                            
                         </th>
                         <th class="SMSelected">                           
                            Set Default
                         </th>                         
                    </tr>
            </HeaderTemplate>
            <ItemTemplate>
                <tr>
                   <td><asp:CheckBox ID="ckSelected" runat="server" Checked='<%#Container.DataItem("ckSelected")%>' /></td>
                    <td><%#Container.DataItem("ServiceName")%> 
                        <asp:HiddenField ID="ID" runat="server" Value='<%#Container.DataItem("ServiceID") %>'/>
                    </td>
                    <td><%#Container.DataItem("ServiceDesc")%> </td>
                    <td><asp:RadioButton ID="rdoDefault" runat="server" Checked='<%#Container.DataItem("IsDefault")%>'/> </td>
                </tr>
            </ItemTemplate>
            <AlternatingItemTemplate>
                <tr bgcolor="#cccccc">
                    <td><asp:CheckBox ID="ckSelected" runat="server" Checked='<%#Container.DataItem("ckSelected")%>' /></td>
                    <td><%#Container.DataItem("ServiceName")%> 
                        <asp:HiddenField ID="ID" runat="server" Value='<%#Container.DataItem("ServiceID") %>'/>
                    </td>
                    <td><%#Container.DataItem("ServiceDesc")%> </td>
                    <td><asp:RadioButton ID="rdoDefault" runat="server" Checked='<%#Container.DataItem("IsDefault")%>'/> </td>
                </tr>
            </AlternatingItemTemplate>
            <FooterTemplate>                
                </table>
            </FooterTemplate>
        </asp:Repeater>
        
        <asp:Button ID="btnSave" runat="server" Text="Save Changes" UseSubmitBehavior="true"/><asp:Button ID="btnBack" runat="server" Text="<< Back to Search"/>                        
        
        </div>      
    </asp:View>
     </asp:MultiView>                     
        <ajaxToolkit:ListSearchExtender ID="ListSearchExtender2" runat="server"
                TargetControlID="DDLAccounts" PromptCssClass="ListSearchExtenderPrompt">
        </ajaxToolkit:ListSearchExtender>
        	
    <MsgBox:msgBox id="MsgBox1" runat="server"></MsgBox:msgBox>        
        <asp:HiddenField ID="hdnAccID" runat="server" />
    </form>
</body>
</html>
