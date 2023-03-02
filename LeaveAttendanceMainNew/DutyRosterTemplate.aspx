<%@ Page Language="VB" AutoEventWireup="false" CodeFile="DutyRosterTemplate.aspx.vb" Inherits="LeaveAttendanceMainNew_DutyRosterTemplate" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>DutyRoster Templates</title>
    <link href= "../App_Themes/Css/styles.css" type="text/css" rel="stylesheet" />
    <link href= "../App_Themes/Css/Common.css" type="text/css" rel="stylesheet" />
    <script type="text/javascript" language="javascript" >
        function Valid()
        {
            if (document.getElementById('txtName').value=='')
            {
                alert('Please enter template name');
                return false;
            }
            if (document.getElementById('ddlShift').value=='')            
            {
                alert('Please select shift');
                return false;
            }
            
            if (ChkCheckBoxes())
                return true;
            else
                return false;
        }
        
        function ChkCheckBoxes()
        {
            var count;
            count=0; 
            for(i = 0; i < form1.elements.length; i++)
            {
                elm = document.forms[0].elements[i]
                if (elm.type == 'checkbox')
                {
                    if (elm.checked)
                        count=count+1;    
                }
            }
            if (count==0)
            {
                alert('Please select at least one weekly off');
                return false;
            }
            
            if (count>2)
            {
                alert('Only select maximum two weekly off');
                return false;
            }
            
            if (count>0&&count<=2)
                return true;
            else
                return false;
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        <div id="body">
            <div id="cap"></div>
            <div id="main">
                <h1>DutyRoster Templates</h1>
                <div style="text-align:left">
                    <asp:Label ID="lblStatus" Font-Names="Trebuchet MS" Font-Size="9" ForeColor="Red" runat="server" Text=""></asp:Label>
                </div>
                
                <div style="text-align: center">
                    <asp:Panel ID="Panel2" runat="server" width="100%">
                        <table width="100%">
                            <tr>
                                <td class="HeaderDiv" style="text-align: center" >
                                    Add Templates
                                    <asp:ImageButton ID="Image1" runat="server" ImageUrl="~/App_Themes/Images/expand_blue.jpg" />
                                </td>
                            </tr>
                        </table> 
                   </asp:Panel>
                   <asp:Panel ID="Panel3" runat="server" width="100%" >
                        <table width="100%">
                            <tr>
                                <td>Name : </td>
                                <td><asp:TextBox ID="txtName" runat="server"></asp:TextBox></td>
                            </tr>
                            <tr>
                                <td>WeeklyOffs :</td>
                                <td>
                                    <asp:CheckBoxList ID="chkWOff" runat="server">
                                        <asp:ListItem Text="Monday" Value="1"></asp:ListItem>
                                        <asp:ListItem Text="Tuesday" Value="2"></asp:ListItem>
                                        <asp:ListItem Text="Wenesday" Value="4"></asp:ListItem>
                                        <asp:ListItem Text="Thrusday" Value="8"></asp:ListItem>
                                        <asp:ListItem Text="Friday" Value="16"></asp:ListItem>
                                        <asp:ListItem Text="Saturday" Value="32"></asp:ListItem>
                                        <asp:ListItem Text="Sunday" Value="64"></asp:ListItem>
                                    </asp:CheckBoxList>
                                </td>
                            </tr>
                            <tr>
                                <td>Shift :</td>
                                <td>
                                    <asp:DropDownList ID="ddlShift" runat="server">
                                        <asp:ListItem Text="I" Value="I"></asp:ListItem>
                                        <asp:ListItem Text="II" Value="II"></asp:ListItem>
                                        <asp:ListItem Text="III" Value="III"></asp:ListItem>
                                        <asp:ListItem Text="N" Value="N"></asp:ListItem>
                                        <asp:ListItem Text="FN" Value="FN"></asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2" style="text-align:center">
                                    <asp:Button ID="btnAdd" runat="server" Text="Add Template" CssClass="button" OnClientClick="javascript:return Valid();" />
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                    <ajaxToolkit:CollapsiblePanelExtender ID="cpesetting" runat="Server"
                            Collapsed="true"
                            TargetControlID="Panel3"
                            ExpandControlID="Panel2"
                            CollapseControlID="Panel2" 
                            ImageControlID="Image1"    
                            ExpandedText="(Hide Details...)"
                            CollapsedText="(Show Details...)"
                            ExpandedImage="../App_Themes/Images/collapse_blue.jpg"
                            CollapsedImage="../App_Themes/Images/expand_blue.jpg"
                            SuppressPostBack="true" EnableViewState="true" 
                    />
                    
                </div>  
                <asp:Panel ID="Panel1" runat="server" HorizontalAlign="Left">
                    <div>
                        <asp:Table ID="tblCancel" runat="server" Width="100%">
                            <asp:TableRow>
                                <asp:TableCell CssClass="HeaderDiv" HorizontalAlign="Center">
                                   DutyRoster Templates
                                </asp:TableCell>
                            </asp:TableRow>
                            <asp:TableRow>
                                <asp:TableCell>
                                    <asp:GridView ID="GridViewMain" EnableSortingAndPagingCallbacks=false runat="server" AutoGenerateColumns=False AllowPaging=true AllowSorting=true ShowFooter=true ShowHeader=true EmptyDataText="No Records found"  Width=100% EnableViewState=true>
                                        <AlternatingRowStyle BackColor="OldLace" />
                                        <Columns>
                                            <asp:TemplateField ItemStyle-Width=1 HeaderStyle-CssClass="alt1">
                                                <ItemTemplate>
                                                    <asp:HiddenField ID="TemplateID" Value='<%#Eval("TemplateID") %>' runat=server  />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField ItemStyle-Width=500 HeaderText="Name" SortExpression="Name" HeaderStyle-CssClass="alt1">
                                                <ItemTemplate>
                                                        <asp:Label ID="lblName" Text='<%#Eval("Name") %>' runat="server"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="WeeklyOffs" HeaderStyle-HorizontalAlign=Center ItemStyle-Width=500 HeaderStyle-CssClass="alt1">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblWeeklyOffs" Text='<%# Eval("WO") %>' CssClass="common" runat="server"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Delete" HeaderStyle-HorizontalAlign=Center HeaderStyle-CssClass="alt1">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="linkDelete" CommandName='DeleteTemplate' CommandArgument='<%#Eval("TemplateID").ToString()%>' runat="server" CausesValidation="false" CssClass="common">Delete</asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                </asp:TableCell>
                            </asp:TableRow>
                        </asp:Table>
                    </div>
                </asp:Panel>
            </div>
        </div> 
    </form>
</body>
</html>
