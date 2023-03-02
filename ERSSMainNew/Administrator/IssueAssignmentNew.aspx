<%@ Page Language="VB" AutoEventWireup="false" CodeFile="IssueAssignmentNew.aspx.vb" Inherits="IssueAssignmentNew" TraceMode="SortByCategory"  %>
<%@ Register Assembly="System.Web.Extensions, Version=1.0.61025.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI" TagPrefix="ajax" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 1.0 Transitional/EN">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>IssueAssignment</title>
    <script language=javascript type="text/javascript">
        function ReadChk(str)
        {   
            var div = document.getElementById("Chk_Remove#" + str);
            if(div != null)
            {
                if(div.checked)
                {
                    div.checked=false;
                }                
            }
        }
        function UpdateChk(str)
        {
            if(document.getElementById("Chk_Update#" + str).checked)
            {
                var div = document.getElementById("Chk_Remove#" + str);
                if(div != null)
                {
                    if(div.checked)
                    {
                        div.checked=false;
                    }                
                }
            
                var div1 = document.getElementById("Chk_Read#" + str);
                if(div1 != null)
                {
                    if(!div1.checked)
                    {
                        div1.checked=true;
                    }                
                }
            }
            else
            {
                var div1 = document.getElementById("Chk_Read#" + str);
                if(div1 != null)
                {
                    if(div1.checked)
                    {
                        div1.checked=false;
                    }                
                }    
            }
        }
        function RemoveChk(str)
        {
            var div = document.getElementById("Chk_Read#" + str);
            if(div != null)
            {
                if(div.checked)
                {
                    div.checked=false;
                }                
            }
            var div1 = document.getElementById("Chk_Update#" + str);
            if(div1 != null)
            {
                if(div1.checked)
                {
                    div1.checked=false;
                }                
            }
        }
    </script>
    <link href= "../../App_Themes/Css/styles.css" type="text/css" rel="stylesheet" />
    <link href= "../../App_Themes/Css/Common.css" type="text/css" rel="stylesheet" />
    
</head>

<body>
    <form id="form1" runat="server">
    <div>
        <ajax:ScriptManager ID="ScriptManager1" runat="server">
        </ajax:ScriptManager>
        <ajax:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <center><asp:Label ID="lblResule" runat="server" Text="" CssClass="common" ForeColor="red"></asp:Label></center>
                <asp:Table ID="tblSubmit" runat="server" Width="700px" HorizontalAlign="Center">
                    <asp:TableRow>
                        <asp:TableCell ColumnSpan="2" CssClass="alt1">
                            Assign Users
                        </asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow>
                        <asp:TableCell HorizontalAlign="Left" Width="30%">
                            <asp:Label ID="Label1" runat="server" CssClass="common" Text="Select Issue Type "></asp:Label>
                        </asp:TableCell>                                            
                        <asp:TableCell HorizontalAlign="Left" >
                            <asp:DropDownList ID="DropDownIssues" CssClass="common" runat="server" Width="250" AutoPostBack ="true" EnableViewState="true">
                            </asp:DropDownList>
                            &nbsp
                            <asp:RequiredFieldValidator  Display="None" ID="RequiredFieldIssues" runat="server" SetFocusOnError="true" ErrorMessage="Please select Issue Type" ControlToValidate="DropDownIssues" ></asp:RequiredFieldValidator>            
                        </asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow>
                        <asp:TableCell HorizontalAlign="Left" Width="30%">
                            <asp:Label ID="Label2" runat="server" CssClass="common" Text="Select Department Name "></asp:Label>
                        </asp:TableCell>
                        <asp:TableCell HorizontalAlign="Left">                        
                            <asp:DropDownList ID="DropDownDept" runat="server" Width="250" EnableViewState="true" CssClass="common">
                            </asp:DropDownList>
                            &nbsp
                            <asp:RequiredFieldValidator  Display="None" ID="RequiredFieldDept" runat="server" SetFocusOnError="true" ErrorMessage="Please select Department" ControlToValidate="DropDownDept" ></asp:RequiredFieldValidator>
                        </asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow>
                        <asp:TableCell HorizontalAlign="Center" ColumnSpan="2">
                            <center>
                                <asp:Button ID="BtnSubmit" runat="server" CssClass="button" Text="Submit" />
                            </center>
                        </asp:TableCell>
                    </asp:TableRow>
                </asp:Table><BR>
                <hr><BR>
                
                <asp:Table ID="tblComments" runat="server" Width="95%" HorizontalAlign="Center">
                    <asp:TableRow>
                        <asp:TableCell HorizontalAlign="Left" Width="100%" BorderStyle="None">
                            <ajaxToolkit:Accordion ID="Accordion" runat="server" HeaderCssClass="accordionHeaderPastTickets" SelectedIndex="-1" FadeTransitions="true" FramesPerSecond="40" 
                                        TransitionDuration="250" AutoSize="None"  RequireOpenedPane="false" SuppressHeaderPostbacks="true">
                                <Panes>
                                    <ajaxToolkit:AccordionPane ID="AccordionPaneExist" runat=server >
                                    <Header>
					                    <b><i>Clik here to View Assignments</i></b>
                                    </Header>
                                    <Content>
                                        <asp:Table ID="tblExistingAssignment" runat="server" HorizontalAlign="Center" Width="80%">
                                            <asp:TableRow>
                                                <asp:TableCell HorizontalAlign="Center" ColumnSpan="3" CssClass="HeaderDiv">
                                                    Existing Assignments
                                                </asp:TableCell>
                                            </asp:TableRow>
                                            <asp:TableRow>
                                                <asp:TableCell CssClass="alt1">
                                                    Department
                                                </asp:TableCell>
                                                <asp:TableCell CssClass="alt1">
                                                    User Name
                                                </asp:TableCell>
                                                <asp:TableCell CssClass="alt1">
                                                    Access
                                                </asp:TableCell>
                                            </asp:TableRow>
                                        </asp:Table>
                                    </Content>
                                </ajaxToolkit:AccordionPane> 
                             </Panes>
                        </ajaxToolkit:Accordion>
                    </asp:TableCell>
                </asp:TableRow>
            </asp:Table>
                
                
                <BR>
                <asp:Table ID="tblSearchResultInTicketManagement" runat="server" Width="95%" HorizontalAlign="Center" EnableViewState="true">
<%--                    <asp:TableRow>
                        <asp:TableCell ColumnSpan=6 Width=100%>
                            <asp:RadioButtonList ID="RadioButtonAccess" Width=50% AutoPostBack=true Font-Names="Trebuchet MS" runat="server">
                                <asp:ListItem Value="Read" Selected=True>Read</asp:ListItem>
                                <asp:ListItem Value="Update">Update</asp:ListItem>
                            </asp:RadioButtonList> 
                        </asp:TableCell>
                    </asp:TableRow>--%>
                    <asp:TableRow Height="15">
                        <asp:TableCell HorizontalAlign="Center" CssClass="alt1">
                            UserName
                        </asp:TableCell>
                        <asp:TableCell HorizontalAlign="Center" CssClass="alt1"> 
                            Name
                        </asp:TableCell>
                        <asp:TableCell HorizontalAlign="Center" CssClass="alt1">
                            Department
                        </asp:TableCell>
                        <asp:TableCell Width="20%" CssClass="alt1">
                            Current Access
                        </asp:TableCell>
                        <asp:TableCell HorizontalAlign="Center" Width="120" CssClass="alt1">
                            Set Access
                        </asp:TableCell>
                    </asp:TableRow>
                </asp:Table><BR>                            
                <asp:Table ID="tblSearchTableInTicketManagement" runat="server" Width="95%" HorizontalAlign="Center">
                    <asp:TableRow>
                        <asp:TableCell HorizontalAlign="Center" BorderStyle="None">
                            <center>
                            <asp:Button ID="BtnSubmitInTicketManagement" CssClass="button" runat="server" CausesValidation="false" Text="Update Access" Visible="false" /> &nbsp
                            </center>
                            <%--<asp:Button ID="BtnRemoveInTicketManagement" CssClass="Buttons" runat="server" CausesValidation=false Font-Names="Trebuchet MS" Text="Remove Access" />--%>
                        </asp:TableCell>
                    </asp:TableRow>
                </asp:Table>
            </ContentTemplate>
            <Triggers>
                <ajax:AsyncPostBackTrigger ControlID="DropDownIssues" EventName="SelectedIndexChanged" />
            </Triggers>
        </ajax:UpdatePanel>
    </div>
    <asp:ValidationSummary ID="ValidationSummary1" runat="server" HeaderText="Following error occurs:" ShowMessageBox="true" DisplayMode="BulletList" ShowSummary="False" /> </form>
</body>
</html>
