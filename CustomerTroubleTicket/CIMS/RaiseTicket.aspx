<%@ Page Language="VB" AutoEventWireup="false" CodeFile="RaiseTicket.aspx.vb" Inherits="CIMS_RaiseTicket" %>

<%@ Register Assembly="System.Web.Extensions, Version=1.0.61025.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI" TagPrefix="asp" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 1.0 Transitional/EN">
<%@ Register
    Assembly="AjaxControlToolkit"
    Namespace="AjaxControlToolkit"
    TagPrefix="ajaxToolkit" %>

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Raise Ticket</title>
    <link rel='stylesheet' type='text/css' href="../Main.css"/>
    <script language="javascript" type="text/javascript">
    function IMG1_onclick() 
    {
        window.close();
    }
</script>
</head>
<body style="color: #000000">
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
    <div>
        <table width="100%">
            <tr>
                <td class="Voice" width="100px" align="right" style="font-family:Arial; font-size:8pt">
                    Subject *    
                </td>
                <td class="Voice1">
                    <asp:TextBox ID="txtSubject" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt" Width=60%></asp:TextBox>
                    <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator5" ControlToValidate="txtSubject" Display="None" ErrorMessage="<b>Required Field Missing</b><br />Subject is required." />
                    <ajaxToolkit:ValidatorCalloutExtender runat="server" ID="ValidatorCalloutExtender5" TargetControlID="RequiredFieldValidator5" HighlightCssClass="validatorCalloutHighlight" /> 
                </td>
            </tr>
            <tr>
                <td class="Voice" width="100px" align="right" style="font-family:Arial; font-size:8pt">
                    Issue Type *                         
                </td>                
                <td class="Voice1"> 
                    <asp:DropDownList ID="DropDownIssueType" runat="server" Font-Names="Arial" Font-Size="8pt" Width=150>
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator1" ControlToValidate="DropDownIssueType" Display="None" ErrorMessage="<b>Required Field Missing</b><br />Issue Type is required." />
                    <ajaxToolkit:ValidatorCalloutExtender runat="server" ID="ValidatorCalloutExtender1" TargetControlID="RequiredFieldValidator1" HighlightCssClass="validatorCalloutHighlight" /> 
                </td>
            </tr>
            <tr>
                <td class="Voice" width="100px" align="right" valign="top" style="font-family:Arial; font-size:8pt;">
                    Issue Description *
                </td>
                <td class="Voice1">
                    <textarea id="txtIssueDesc" rows="4" cols="65" style="font-family:Arial; font-size:8pt" runat=server></textarea>
                    <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator2" ControlToValidate="txtIssueDesc" Display="None" ErrorMessage="<b>Required Field Missing</b><br />Issue Description is required." />
                    <ajaxToolkit:ValidatorCalloutExtender runat="server" ID="ValidatorCalloutExtender2" TargetControlID="RequiredFieldValidator2" HighlightCssClass="validatorCalloutHighlight" /> 
                </td>
            </tr>
            <tr>
                <td class="Voice" width="100px" align="right" valign="top" style="font-family:Arial; font-size:8pt;">
                    Priority *
                </td>
                <td class="Voice1">
                    <asp:DropDownList ID="DropDownPriority" Font-Names="Arial" Font-Size="8pt" runat="server" Width=150>
                        <asp:ListItem Text="Please Select" Value=""></asp:ListItem>
                        <asp:ListItem Text="Low" Value="Low"></asp:ListItem>
                        <asp:ListItem Text="Normal" Value="Normal"></asp:ListItem>
                        <asp:ListItem Text="High" Value="High"></asp:ListItem>
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator3" ControlToValidate="DropDownPriority" Display="None" ErrorMessage="<b>Required Field Missing</b><br />Priority is required." />
                    <ajaxToolkit:ValidatorCalloutExtender runat="server" ID="ValidatorCalloutExtender3" TargetControlID="RequiredFieldValidator3" HighlightCssClass="validatorCalloutHighlight" /> 
                </td>
            </tr>
            <%--<tr>
                <td class="Voice4" colspan="2">
                    <ajaxToolkit:Accordion ID="AttachmentSection" Width="100%" runat=server 
                        FadeTransitions="true" SelectedIndex=-1 FramesPerSecond="40" 
                        TransitionDuration="250" AutoSize="None" RequireOpenedPane="false" SuppressHeaderPostbacks="true">
                        <Panes>
                            <ajaxToolkit:AccordionPane runat=server ID="AccordionPaneID">
                                <Header>
                                    <strong><span style="color:Gray"><span style="color:Gray">Attachment</span> </span></strong>
                                    <asp:ImageButton ID="Image1" runat="server" ImageUrl="~/images/expand.jpg" CausesValidation="false" AlternateText="(Attach any screen shot...)"/><strong><span style="color: whitesmoke"> </span></strong>
                                </Header>
                                <Content>
                                    <asp:Table ID="tblAttach" runat="server" HorizontalAlign=center>
                                        <asp:TableRow>
                                            <asp:TableCell>
                                                <asp:FileUpload ID="FileUploadAttachment" Font-Names="Arial" runat="server" />
                                            </asp:TableCell>
                                        </asp:TableRow>
                                    </asp:Table>    
                                </Content>
                            </ajaxToolkit:AccordionPane>
                        </Panes>
                    </ajaxToolkit:Accordion>
                </td>
            </tr>--%>
        </table>  
        <br />  
        <center>
            <asp:Button ID="Button2" runat="server"  CssClass="button" Text="Submit"   /> &nbsp &nbsp
            <input id="IMG1" type="button" value="Close Window" onclick="return IMG1_onclick()" class="button"  />
        </center>
        
          <br />
        <span style="font-size: 8pt; font-family: Arial"><strong>
         * Necessary Field </strong></span>        
    </div>
    </form>
</body>
</html>
