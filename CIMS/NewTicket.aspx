<%@ Page Language="VB" AutoEventWireup="false" CodeFile="NewTicket.aspx.vb" Inherits="NewTicket" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 1.0 Transitional/EN">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Untitled Page</title>
    <link href="Styles/Report.css" rel=stylesheet />
    <link href="Styles/Accordin.css" rel=stylesheet />
</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
    <div>
        <center>
            <asp:Table ID="Table1" runat="server" Width="80%" GridLines=Both>
                <asp:TableRow CssClass="ReportHeaderDiv">
                    <asp:TableCell ColumnSpan=2 HorizontalAlign=Center>
                       Report New Issue
                    </asp:TableCell>
                </asp:TableRow>
                <asp:TableRow runat="server">
                    <asp:TableCell runat="server" HorizontalAlign=Right VerticalAlign=Top Width=20%>
                        <asp:Label ID="lblIssueType" Font-Names="Trebuchet MS" Font-Size=Small Font-Italic=True ForeColor=Gray runat="server" Text="Issue Type :"></asp:Label>
                    </asp:TableCell>
                    <asp:TableCell HorizontalAlign="Left">
                        <asp:DropDownList ID="DropDownIssueType" runat="server" Font-Names="Trebuchet MS" Width=150>
                        </asp:DropDownList> &nbsp &nbsp
                        <asp:RequiredFieldValidator ID="RequiredFieldIssueType" runat="server" ErrorMessage="Please select issue type"  Font-Size=small Font-Names="Trebuchet MS" Font-Bold=true Font-Italic=true SetFocusOnError=true ControlToValidate="DropDownIssueType"></asp:RequiredFieldValidator>
                    </asp:TableCell>
                </asp:TableRow>
                <asp:TableRow>
                    <asp:TableCell Font-Names="Trebuchet MS" HorizontalAlign=Right VerticalAlign="top" Width="20%" ForeColor=Gray Font-Italic=true Font-Size=small >
                        Issue Description : 
                    </asp:TableCell>
                    <asp:TableCell HorizontalAlign="Left">
                        <textarea id="txtIssueDesc" rows="4" cols="65" style="font-family:Trebuchet MS" runat=server></textarea>&nbsp &nbsp    
                        <asp:RequiredFieldValidator ID="RequiredFieldIssueDesc" runat="server" ErrorMessage="Please enter issue description" Font-Size=Small Font-Names="Trebuchet MS" Font-Bold=true Font-Italic=true SetFocusOnError=true ControlToValidate="txtIssueDesc"></asp:RequiredFieldValidator>
                    </asp:TableCell>
                </asp:TableRow>
                <asp:TableRow>
                    <asp:TableCell HorizontalAlign=Right ForeColor=Gray>
                        <asp:Label ID="lblPriority" runat="server" Font-Names="Trebuchet MS" Font-Italic=True Font-Size=small Text="Priority :"></asp:Label>
                    </asp:TableCell>
                    <asp:TableCell HorizontalAlign=Left>
                        <asp:DropDownList ID="DropDownPriority" Font-Names="Trebuchet MS" Font-Size=Small runat="server" Width=150>
                            <asp:ListItem Text="Please Select" Value=""></asp:ListItem>
                            <asp:ListItem Text="Low" Value="Low"></asp:ListItem>
                            <asp:ListItem Text="Normal" Value="Normal"></asp:ListItem>
                            <asp:ListItem Text="High" Value="High"></asp:ListItem>
                        </asp:DropDownList>&nbsp &nbsp
                        <asp:RequiredFieldValidator ID="RequiredFieldPriority" runat="server" Font-Names="Trebuchet MS" Font-Bold=true Font-Italic=True Font-Size=small ErrorMessage="Please select priority" ControlToValidate="DropDownPriority"></asp:RequiredFieldValidator>
                    </asp:TableCell>
                </asp:TableRow>
                <asp:TableRow>
                    <asp:TableCell ColumnSpan=2 HorizontalAlign=Left>
                        <ajaxToolkit:Accordion ID="AttachmentSection" Width="100%" runat=server 
                                HeaderCssClass="accordionHeader" ContentCssClass="accordionContent" FadeTransitions="true" SelectedIndex=-1 FramesPerSecond="40" 
                                TransitionDuration="250" AutoSize="None" RequireOpenedPane="false" SuppressHeaderPostbacks="true">
                                <Panes>
                                    <ajaxToolkit:AccordionPane runat=server ID="AccordionPaneID">
                                        <Header>Attachment</Header>
                                        <Content>
                                            <asp:Table ID="tblAttach" runat="server" HorizontalAlign=center>
                                                <asp:TableRow>
                                                    <asp:TableCell>
                                                        <asp:FileUpload ID="FileUploadAttachment" Font-Names="Trebuchet MS" runat="server" />
                                                    </asp:TableCell>
                                                </asp:TableRow>
                                            </asp:Table>    
                                        </Content>
                                    </ajaxToolkit:AccordionPane>
                                </Panes>
                        </ajaxToolkit:Accordion>
                    </asp:TableCell>
                </asp:TableRow>
                <asp:TableRow>
                    <asp:TableCell ColumnSpan=2 HorizontalAlign=center>
                        <asp:Button ID="BtnAddNew" runat="server" Font-Names="Trebuchet MS" UseSubmitBehavior=true CausesValidation=true Text="Report New Issue" />
                    </asp:TableCell>
                </asp:TableRow>
            </asp:Table>
          </center>
    </div>
    </form>
</body>
</html>
