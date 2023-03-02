<%@ Page Language="VB" AutoEventWireup="false" CodeFile="EditTemplateAssignments.aspx.vb" Inherits="ets.Templates_EditTemplateAssignments" %>
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
    <h1>Template Assignments</h1>
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
                                        Template Name</td>
                                    <td align="center" class="alt1">
                                        TAT
                                    <td align="center" class="alt1">
                                        STAT</td>
                                    <td align="center" class="alt1">
                                        TimeZone</td>
                                    <td align="center" class="alt1">
                                        DueTime(24Hrs)</td>
                                    <td align="center" class="alt1">
                                        Sequence</td>
                                    <td align="center" class="alt1">
                                        Action</td>                         
                                </tr>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <tr>
                                <td align="center" width=70%>
                                    <asp:Label ID="txtName" runat="server" Text='<%#Container.DataItem("TemplateName")%>'></asp:Label>
                                    <asp:HiddenField ID="TemplateID" runat="server" Value='<%#Container.DataItem("TemplateID")%>' />
                                </td>
                                <td align="center" width=10%>
                                    <asp:DropDownList ID="DDLTAT" runat="server">
                                        <asp:ListItem Value="2" Text="2 Hrs"></asp:ListItem>
                                        <asp:ListItem Value="4" Text="4 Hrs"></asp:ListItem>
                                        <asp:ListItem Value="6" Text="6 Hrs"></asp:ListItem>
                                        <asp:ListItem Value="8" Text="8 Hrs"></asp:ListItem>
                                        <asp:ListItem Value="12" Text="12 Hrs"></asp:ListItem>
                                        <asp:ListItem Value="24" Text="24 Hrs"></asp:ListItem>
                                        <asp:ListItem Value="36" Text="36 Hrs"></asp:ListItem>
                                        <asp:ListItem Value="48" Text="48 Hrs"></asp:ListItem>
                                        <asp:ListItem Value="72" Text="72 Hrs"></asp:ListItem>
                                        <asp:ListItem Value="96" Text="96 Hrs"></asp:ListItem>
                                        <asp:ListItem Value="120" Text="120 Hrs"></asp:ListItem>
                                    </asp:DropDownList>
                                    
                                </td>
                                <td align="center" width=10%>
                                    <asp:TextBox ID="txtSTAT" runat="server" Text='<%#Container.DataItem("STAT")%>' Width="20" MaxLength="2"></asp:TextBox>                                    
                                </td>
                                <td align="center" width=10%>
                                    <asp:DropDownList ID="DDLTZ" runat="server">
                                       <asp:ListItem Value="0" Text="Eastern Time (EST - Default)"></asp:ListItem>
                                       <asp:ListItem value="-1" text="Central Time (CST)"></asp:ListItem>
                                       <asp:ListItem value="-2" Text="Mountain Time (MST)"></asp:ListItem>
                                       <asp:ListItem value="-3" Text="Pacific Time (PST)"></asp:ListItem>
                                       <asp:ListItem value="-4" Text="Alaska Time (AKST)"></asp:ListItem>
                                       <asp:ListItem value="-6" Text="Hawaii Time"></asp:ListItem>
                                    </asp:DropDownList>
                                    
                                </td>
                                <td align="center" width=10%>
                                    <asp:TextBox ID="txtTime" runat="server" Text='<%#Container.DataItem("Time")%>' Width="20" MaxLength="2"></asp:TextBox>                                    
                                </td>
                                <td align="center" width=10%>
                                    <asp:TextBox ID="txtWT" runat="server" Text='<%#Container.DataItem("WorkType")%>' Width="20" MaxLength="2"></asp:TextBox>                                    
                                </td>
                                <td align="center" width=10%>
                                    <asp:Button ID="btnRemove" runat="server" Text="Remove Template" OnClick="btnRemove_Click" CssClass="button" />
                                    <ETSAnim:RIAnimator ID="RIAnimator1" runat="server" TriggerControlID="btnRemove" TargetControlID="PNL" TriggerEvent="Click" Animation="FadeIn" /> 
                                    <ajaxToolkit:ConfirmButtonExtender ID="ConfirmButtonExtender2" runat="server" 
                                    TargetControlID="btnRemove"
                                    OnClientCancel="cancelClick"
                                    DisplayModalPopupID="ModalPopupExtender1" />
                                    <br />
                                    <ajaxToolkit:ModalPopupExtender ID="ModalPopupExtender1" runat="server" TargetControlID="btnRemove" PopupControlID="PNL" OkControlID="ButtonOk" CancelControlID="ButtonCancel" BackgroundCssClass="modalBackground" />
                                    <asp:Panel ID="PNL" runat="server" style="display:none; width:200px; background-color:White; border-width:2px; border-color:Black; border-style:solid; padding:20px;">
                                        Are you sure you want to remove the template?
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
                                    <asp:Label ID="txtName" runat="server" Text='<%#Container.DataItem("TemplateName")%>'></asp:Label>
                                    <asp:HiddenField ID="TemplateID" runat="server" Value='<%#Container.DataItem("TemplateID")%>' />
                                </td>
                                <td align="center" width=10%>
                                    <asp:DropDownList ID="DDLTAT" runat="server">
                                        <asp:ListItem Value="2" Text="2 Hrs"></asp:ListItem>
                                        <asp:ListItem Value="4" Text="4 Hrs"></asp:ListItem>
                                        <asp:ListItem Value="6" Text="6 Hrs"></asp:ListItem>
                                        <asp:ListItem Value="8" Text="8 Hrs"></asp:ListItem>
                                        <asp:ListItem Value="12" Text="12 Hrs"></asp:ListItem>
                                        <asp:ListItem Value="24" Text="24 Hrs"></asp:ListItem>
                                        <asp:ListItem Value="36" Text="36 Hrs"></asp:ListItem>
                                        <asp:ListItem Value="48" Text="48 Hrs"></asp:ListItem>
                                        <asp:ListItem Value="72" Text="72 Hrs"></asp:ListItem>
                                        <asp:ListItem Value="96" Text="96 Hrs"></asp:ListItem>
                                        <asp:ListItem Value="120" Text="120 Hrs"></asp:ListItem>
                                    </asp:DropDownList>
                                    
                                </td>
                                <td align="center" width=10%>
                                    <asp:TextBox ID="txtSTAT" runat="server" Text='<%#Container.DataItem("STAT")%>' Width="20" MaxLength="2"></asp:TextBox>                                    
                                </td>
                                <td align="center" width=10%>
                                    <asp:DropDownList ID="DDLTZ" runat="server">
                                       <asp:ListItem Value="0" Text="Eastern Time (EST - Default)"></asp:ListItem>
                                       <asp:ListItem value="-1" text="Central Time (CST)"></asp:ListItem>
                                       <asp:ListItem value="-2" Text="Mountain Time (MST)"></asp:ListItem>
                                       <asp:ListItem value="-3" Text="Pacific Time (PST)"></asp:ListItem>
                                       <asp:ListItem value="-4" Text="Alaska Time (AKST)"></asp:ListItem>
                                       <asp:ListItem value="-6" Text="Hawaii Time"></asp:ListItem>
                                    </asp:DropDownList>
                                    
                                </td>
                                <td align="center" width=10%>
                                    <asp:TextBox ID="txtTime" runat="server" Text='<%#Container.DataItem("Time")%>' Width="20" MaxLength="2"></asp:TextBox>                                    
                                </td>
                                <td align="center" width=10%>
                                    <asp:TextBox ID="txtWT" runat="server" Text='<%#Container.DataItem("WorkType")%>' Width="20" MaxLength="2"></asp:TextBox>                                    
                                </td>
                                <td align="center" width=10%>
                                    <asp:Button ID="btnRemove" runat="server" Text="Remove Template" OnClick="btnRemove_Click" CssClass="button" />
                                    <ETSAnim:RIAnimator ID="RIAnimator1" runat="server" TriggerControlID="btnRemove" TargetControlID="PNL" TriggerEvent="Click" Animation="FadeIn" /> 
                                    <ajaxToolkit:ConfirmButtonExtender ID="ConfirmButtonExtender2" runat="server" 
                                    TargetControlID="btnRemove"
                                    OnClientCancel="cancelClick"
                                    DisplayModalPopupID="ModalPopupExtender1" />
                                    <br />
                                    <ajaxToolkit:ModalPopupExtender ID="ModalPopupExtender1" runat="server" TargetControlID="btnRemove" PopupControlID="PNL" OkControlID="ButtonOk" CancelControlID="ButtonCancel" BackgroundCssClass="modalBackground" />
                                    <asp:Panel ID="PNL" runat="server" style="display:none; width:200px; background-color:White; border-width:2px; border-color:Black; border-style:solid; padding:20px;">
                                        Are you sure you want to remove the template?
                                        <br /><br />
                                    <div style="text-align:right;">
                                        <asp:Button ID="ButtonOk" runat="server" Text="OK" />
                                        <asp:Button ID="ButtonCancel" runat="server" Text="Cancel" />
                                    </div>
                                    </asp:Panel>
                                </td>                                                                
                            </tr>
                        </AlternatingItemTemplate>
                        <FooterTemplate>
                            <tr>
                            <td colspan="5" style="text-align:center">
                                <asp:Button ID="btnSave" runat="server" Text="Save Changes" OnClick="btnSave_Click" CssClass="button"/>
                                <ETSAnim:RIAnimator ID="RIAnimator1" runat="server" TriggerControlID="btnSave" TargetControlID="PNL" TriggerEvent="Click" Animation="FadeIn"/> 
                                <ajaxToolkit:ConfirmButtonExtender ID="ConfirmButtonExtender2" runat="server" 
                                    TargetControlID="btnSave"
                                    OnClientCancel="cancelClick"
                                    DisplayModalPopupID="ModalPopupExtender1" />
                                    <br />
                                <ajaxToolkit:ModalPopupExtender ID="ModalPopupExtender1" runat="server" TargetControlID="btnSave" PopupControlID="PNL" OkControlID="ButtonOk" CancelControlID="ButtonCancel" BackgroundCssClass="modalBackground" />
                                <asp:Panel ID="PNL" runat="server" style="display:none; width:200px; background-color:White; border-width:2px; border-color:Black; border-style:solid; padding:20px;">
                                    Are you sure you want to save the Changes?
                                <br /><br />
                                <div style="text-align:right;">
                                    <asp:Button ID="ButtonOk" runat="server" Text="OK" />
                                    <asp:Button ID="ButtonCancel" runat="server" Text="Cancel" />
                                </div>
                                </asp:Panel>
                            </td>
                            </tr>
                            </table>
                        </FooterTemplate>
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
