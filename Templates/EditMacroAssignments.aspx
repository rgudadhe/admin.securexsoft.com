<%@ Page Language="VB" AutoEventWireup="false" CodeFile="EditMacroAssignments.aspx.vb" Inherits="ets.Templates_EditTemplateAssignments" %>
<%@ Register
    Assembly="AjaxControlToolkit"
    Namespace="AjaxControlToolkit"
    TagPrefix="ajaxToolkit" %>
<%@ Register
    Assembly="HHM.RIAnimation"
    Namespace="HHM.RIAnimation"
    TagPrefix="ETSAnim" %> 
    
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 1.0 Transitional/EN">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Template Assignments</title>
    <link href= "../App_Themes/Css/Default.css" type="text/css" rel="stylesheet" />
    <link href= "../App_Themes/Css/Styles.css" type="text/css" rel="stylesheet" />
    <link href= "../App_Themes/Css/Common.css" type="text/css" rel="stylesheet" />
    <script type='text/javascript'>
    function cancelClick() {
        var label = $get('ctl00_SampleContent_Label1');
        
    }
</script>
</head>
<body>
    <form id="form1" runat="server">
    <div id="body">
    <div id="cap"></div>
    <div id="main">
    <h1>Macro Assignments</h1>
    <ajaxToolkit:ToolkitScriptManager runat="Server" EnablePartialRendering="true" ID="ScriptManager1" />
    <div>
   
        <asp:Panel ID="Panel1" runat="server" HorizontalAlign="Left">
            <asp:Label ID="iResponse" runat="server" Text="" CssClass="Title"></asp:Label><br /><br />
        </asp:Panel>
        <asp:Panel ID="Panel2" runat="server" HorizontalAlign="Left">
          <table width="70%">
            <tr>
                <td class="HeaderDiv">
                    <asp:Label ID="lblCaption" runat="server" ></asp:Label>            
                </td>
            </tr>
          </table>
          <asp:Repeater ID="rptPhyTemp" runat="server">
                        <HeaderTemplate>
                            <table width="70%">  
                                <tr>
                                    <td align="center" class="alt1">
                                        Description</td>
                                   
                                </tr>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <tr>
                                <td align="center" width=70%>
                                    <asp:Label ID="txtName" runat="server" Text='<%#Container.DataItem("Description")%>'></asp:Label>
                                    <asp:HiddenField ID="McID" runat="server" Value='<%#Container.DataItem("McID")%>' />
                                </td>
                                
                                <td align="center" width=10%>
                                    <asp:Button ID="btnRemove" runat="server" Text="Remove Macro" OnClick="btnRemove_Click" CssClass="button" />
                                    <ETSAnim:RIAnimator ID="RIAnimator1" runat="server" TriggerControlID="btnRemove" TargetControlID="PNL" TriggerEvent="Click" Animation="FadeIn" /> 
                                    <ajaxToolkit:ConfirmButtonExtender ID="ConfirmButtonExtender2" runat="server" 
                                    TargetControlID="btnRemove"
                                    OnClientCancel="cancelClick"
                                    DisplayModalPopupID="ModalPopupExtender1" />
                                    <br />
                                    <ajaxToolkit:ModalPopupExtender ID="ModalPopupExtender1" runat="server" TargetControlID="btnRemove" PopupControlID="PNL" OkControlID="ButtonOk" CancelControlID="ButtonCancel" BackgroundCssClass="modalBackground" />
                                    <asp:Panel ID="PNL" runat="server" style="display:none; width:200px; background-color:White; border-width:2px; border-color:Black; border-style:solid; padding:20px;">
                                        Are you sure you want to remove the macro?
                                        <br /><br />
                                    <div style="text-align:right;">
                                        <asp:Button ID="ButtonOk" runat="server" Text="OK" />
                                        <asp:Button ID="ButtonCancel" runat="server" Text="Cancel" />
                                    </div>
                                    </asp:Panel>
                                </td>                                                                
                            </tr>
                        </ItemTemplate>
                        <AlternatingItemTemplate>
                            <tr bgcolor="#cccccc">
                                <td align="center" width=70%>
                                    <asp:Label ID="txtName" runat="server" Text='<%#Container.DataItem("Description")%>'></asp:Label>
                                    <asp:HiddenField ID="McID" runat="server" Value='<%#Container.DataItem("McID")%>' />
                                </td>
                                
                                <td align="center" width=10%>
                                    <asp:Button ID="btnRemove" runat="server" Text="Remove Macro" OnClick="btnRemove_Click" CssClass="button" />
                                    <ETSAnim:RIAnimator ID="RIAnimator1" runat="server" TriggerControlID="btnRemove" TargetControlID="PNL" TriggerEvent="Click" Animation="FadeIn" /> 
                                    <ajaxToolkit:ConfirmButtonExtender ID="ConfirmButtonExtender2" runat="server" 
                                    TargetControlID="btnRemove"
                                    OnClientCancel="cancelClick"
                                    DisplayModalPopupID="ModalPopupExtender1" />
                                    <br />
                                    <ajaxToolkit:ModalPopupExtender ID="ModalPopupExtender1" runat="server" TargetControlID="btnRemove" PopupControlID="PNL" OkControlID="ButtonOk" CancelControlID="ButtonCancel" BackgroundCssClass="modalBackground" />
                                    <asp:Panel ID="PNL" runat="server" style="display:none; width:200px; background-color:White; border-width:2px; border-color:Black; border-style:solid; padding:20px;">
                                        Are you sure you want to remove the Macro?
                                        <br /><br />
                                    <div style="text-align:right;">
                                        <asp:Button ID="ButtonOk" runat="server" Text="OK" />
                                        <asp:Button ID="ButtonCancel" runat="server" Text="Cancel" />
                                    </div>
                                    </asp:Panel>
                                </td>                                                                
                            </tr>
                        </AlternatingItemTemplate>
                       
                    </asp:Repeater>  
        </asp:Panel>
        
                    
                
        <asp:HiddenField ID="hdnPhyID" runat="server" />
   
    </div>
    </div>
    </div> 
    <asp:HiddenField ID="test" runat="server" />
    </form>
</body>
</html>
