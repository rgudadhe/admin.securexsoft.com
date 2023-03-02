<%@ Page Language="VB" AutoEventWireup="false" CodeFile="CreateContractor.aspx.vb" Inherits="ets._Default" %>
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
    <title>Create Contractor</title>
    <link href= "../App_Themes/Css/styles.css" type="text/css" rel="stylesheet"/>
    <link href= "../App_Themes/Css/Common.css" type="text/css" rel="stylesheet"/>
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
        <h1>Create Contractor</h1>
    <ajaxToolkit:ToolkitScriptManager runat="server" ID="ScriptManager1"/>
            <asp:Panel ID="Panel1" runat="server" HorizontalAlign="Left" >
                <div style="margin-top:-75px;">            
        <asp:UpdatePanel  runat="server" ID="up2" >
                    <ContentTemplate> 
                    <table>
                        <tr>
                            <td class="alt1">
                                <asp:Label CssClass="common" ID="lblConName" runat="server" Text="*Contractor Name" Width="182px"></asp:Label>
                            </td>
                            <td class="alt1">
                                Details
                            </td>
                            <td class="alt1">
                               *Instance
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: center;">
                                <asp:TextBox ID="txtConName" runat="server" CssClass="common" CausesValidation="True"></asp:TextBox></td>
                            <td style="text-align: center;">
                                <%--<input id="txtConDetails" style="font-family: Tahoma, 'Trebuchet MS'; font-size: 10pt;" type="text" />--%>
                                <asp:TextBox ID="txtConDetails" runat="server" CssClass="common"></asp:TextBox>
                             </td>
                             <td style="text-align: center;">
                                <asp:DropDownList ID="DLInstance" runat="server" Width="130px">
                                    <asp:ListItem Text="Please Select" Value="" Selected="True"></asp:ListItem>
                                    <asp:ListItem Text="1" Value="1"></asp:ListItem>
                                    <asp:ListItem Text="2" Value="2"></asp:ListItem>
                                </asp:DropDownList>
                             </td>
                        </tr>                        
                        <asp:Panel ID="pnlSub" runat="server" Visible="false" >                                                                       
                        <tr>
                            <td style="text-align: center;" colspan="3">
                                <asp:CheckBox ID="chkIsSub" runat="server" Text="Set As Sub-Contractor" AutoPostBack="True" CssClass="common" OnCheckedChanged="chkIsSub_CheckedChanged"/></td>
                        </tr>                        
                        <div id="background" style="text-align: center; vertical-align: middle; line-height: 44px; padding: 12px; height: 44px; color: #FFFFFF;">
                        <tr>
                            <td class="alt1" colspan="2">

                                <asp:Label ID="lblContractors" CssClass="common" runat="server"
                                    Text="Select Contractor Name" Width="182px"></asp:Label>
                            </td>
                            <td class="alt1" >
                                <asp:DropDownList ID="cmbContractors" runat="server" CssClass="common" >                                    
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>                             
                            <td id="tdSubCon" colspan="3" style="height: 21px; text-align: center">
                                &nbsp;</td>
                        </tr>
                        </div>                        
                        </asp:Panel>
                        
                        <tr>
                            <td colspan="3" style="height: 21px; text-align: center;" id="btnSubmit">
                                <asp:Button ID="Button1" runat="server" Text="Submit" CssClass="button" OnClick="Button1_Click"/>
                                <ETSAnim:RIAnimator ID="RIAnimator1" runat="server" TriggerControlID="Button1" TargetControlID="PNL" TriggerEvent="Click" Animation="fadeIn" /> 
                                    <ajaxToolkit:ConfirmButtonExtender ID="ConfirmButtonExtender2" runat="server" 
                                    TargetControlID="Button1"
                                    OnClientCancel="cancelClick"
                                    DisplayModalPopupID="ModalPopupExtender1" />
                                    <br />
                                    <ajaxToolkit:ModalPopupExtender ID="ModalPopupExtender1" runat="server" TargetControlID="Button1" PopupControlID="PNL" OkControlID="ButtonOk" CancelControlID="ButtonCancel" BackgroundCssClass="modalBackground" />
                                    <div>
                                    <asp:Panel ID="PNL" runat="server" style="display:none; width:200px; background-color:White; border-width:2px; border-color:Black; border-style:solid; padding:20px;">
                                        Are you sure you want to create new contractor?
                                        <br /><br />
                                    <div style="text-align:right;">
                                        <asp:Button ID="ButtonOk" runat="server" Text="OK" />
                                        <asp:Button ID="ButtonCancel" runat="server" Text="Cancel" />
                                    </div>
                                    </asp:Panel>
                                    </div>
                                </td>
                        </tr>
                        <tr>
                            <td colspan="3" style="height: 21px; text-align: center">
                                &nbsp;<asp:RequiredFieldValidator  Display="None" ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtConName" 
                                    ErrorMessage="Contractor name can not be blank" SetFocusOnError="True"></asp:RequiredFieldValidator>
                                 <asp:RequiredFieldValidator  Display="None" ControlToValidate="DLInstance"  ID="RequiredFieldValidator5"  runat="server" ErrorMessage="Please enter instance for account" ></asp:RequiredFieldValidator>
                                    </td>                                    
                                                            </tr>
                    </table>
                        <asp:Label ID="iResponse" runat="server" CssClass="Title"></asp:Label>
                    </ContentTemplate>
           <Triggers>
           <asp:AsyncPostBackTrigger ControlID="chkIsSub" EventName="CheckedChanged"/>                        
           </Triggers>           
           </asp:UpdatePanel>                                    
           </div>
            </asp:Panel>
    
           </div> 
           </div>         
    <asp:ValidationSummary ID="ValidationSummary1" runat="server" HeaderText="Following error occurs:" ShowMessageBox="true" DisplayMode="BulletList" ShowSummary="False" /> </form>
</body>
</html>
