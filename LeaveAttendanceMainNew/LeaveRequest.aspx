<%@ Page Language="VB" AutoEventWireup="false" CodeFile="LeaveRequest.aspx.vb" Inherits="LeaveRequest" Debug="true" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Assembly="System.Web.Extensions, Version=1.0.61025.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI" TagPrefix="asp" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 1.0 Transitional/EN">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head2" runat="server">
<link href= "../App_Themes/Css/styles.css" type="text/css" rel="stylesheet" />
<link href= "../App_Themes/Css/Common.css" type="text/css" rel="stylesheet" />
<link href="../App_Themes/Css/Calendar.css" type="text/css" rel="stylesheet" />
    <title>Leave Request</title>
</head>

<body>
    <form id="frmLeave" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        <div style=" text-align:left">
            <asp:Label ID="lblMsg" runat="server" ForeColor="Firebrick" Text="" Font-Size="10"></asp:Label>    
              <asp:Label ID="Label1" runat="server" ForeColor="Firebrick" Text="" Font-Size="10"></asp:Label>    
        </div>
        
        <div>
            <asp:Table ID="Table2" runat="server" HorizontalAlign="Left" Width="500px">
                <asp:TableRow>
                    <asp:TableCell HorizontalAlign="Center"  ColumnSpan="2" CssClass="HeaderDiv">
                        Leave Application
                    </asp:TableCell>
                </asp:TableRow>
                
                <asp:TableRow>
                    <asp:TableCell HorizontalAlign="Center" ColumnSpan="2">
                        <center>
                            Select Start Date : <asp:TextBox ID="txtStartDate" runat="server" Width="70px" ReadOnly="true"></asp:TextBox>    
                                                <asp:ImageButton ID="imgSDate" CausesValidation="false" ImageUrl="~/App_Themes/Images/Calendar_scheduleHS.png" runat="server"/> &nbsp &nbsp
                                                <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" Format="MM/dd/yyyy" TargetControlID="txtStartDate" PopupButtonID="imgSDate" BehaviorID="CalendarExtender1" CssClass="cal_Theme1" Enabled="True">
                                                </ajaxToolkit:CalendarExtender>
                            Select End Date : <asp:TextBox ID="txtEndDate" runat="server" Width="70px" ReadOnly="true"></asp:TextBox>
                                              <asp:ImageButton ID="imgEDate" ImageUrl="~/App_Themes/Images/Calendar_scheduleHS.png" CausesValidation="false" runat="server" />
                                              <ajaxToolkit:CalendarExtender ID="CalendarExtender2" runat="server" Format="MM/dd/yyyy" TargetControlID="txtEndDate" PopupButtonID="imgEDate" BehaviorID="CalendarExtender2" CssClass="cal_Theme1" Enabled="True">
                                              </ajaxToolkit:CalendarExtender>
                        </center>
                    </asp:TableCell>
                </asp:TableRow>
                   
                <asp:TableRow>
                    <asp:TableCell HorizontalAlign="Center" ColumnSpan="2">
                        <center>
                            Leave Type :
                            <asp:DropDownList ID="DropLeaveType" CssClass="common" runat="server" >
                                <asp:ListItem Text="------ Select Leave Type ------" Value=""></asp:ListItem>
                                <asp:ListItem Text="Casual Leave" Value="CL"></asp:ListItem>
                                <asp:ListItem Text="Comp Off" Value="CO"></asp:ListItem>
                                <asp:ListItem Text="Earned Leave" Value="EL"></asp:ListItem>
                                <asp:ListItem Text="Half Day(Earned Leave)" Value="ELHL"></asp:ListItem>
                                <asp:ListItem Text="Half Day(Casual Leave)" Value="CLHL"></asp:ListItem>
                                <asp:ListItem Text="Leave Without Pay" Value="LWP"></asp:ListItem>
                                <asp:ListItem Text="Leave Without Pay(Half Day)" Value="LWPHL"></asp:ListItem>
                            </asp:DropDownList>
                        </center>
                    </asp:TableCell>
                </asp:TableRow>
                
                <asp:TableRow>
                    <asp:TableCell Width="40px" HorizontalAlign="Right" VerticalAlign="top">
                        Reason : 
                    </asp:TableCell>
                    <asp:TableCell>
                        <textarea id="TextArea1" rows="8" cols="85" runat="server" class="common"></textarea>
                    </asp:TableCell>
                </asp:TableRow>
                
                <asp:TableRow>
                    <asp:TableCell ColumnSpan="2" HorizontalAlign="Center">
                        <center>
                            <asp:Button ID="SendLR" runat="server" Text="Send Leave Request" CssClass="button" />
                        </center>
                    </asp:TableCell>
                </asp:TableRow>
            </asp:Table>
            <asp:RequiredFieldValidator  Display="None" ID="RequiredFieldtxtStartDate" runat="server" SetFocusOnError="true" ErrorMessage="<b>Required Field Missing</b><br />Start Date is required." ControlToValidate="txtStartDate"  BorderStyle="None" ></asp:RequiredFieldValidator>
            <ajaxToolkit:ValidatorCalloutExtender runat="Server" ID="NReqE1" TargetControlID="RequiredFieldtxtStartDate"  />
            <asp:RequiredFieldValidator  Display="None" ID="RequiredFieldtxtEndDate" runat="server" SetFocusOnError="true" ErrorMessage="<b>Required Field Missing</b><br />End Date is required." ControlToValidate="txtEndDate"  BorderStyle="None" ></asp:RequiredFieldValidator>
            <ajaxToolkit:ValidatorCalloutExtender runat="Server" ID="NReqE2" TargetControlID="RequiredFieldtxtEndDate"  />
            <asp:RequiredFieldValidator  Display="None" ID="RequiredFieldLeaveType" runat="server" ErrorMessage="<b>Required Field Missing</b><br />Leave Type is required."  SetFocusOnError="true" ControlToValidate="DropLeaveType"  BorderStyle="None" ></asp:RequiredFieldValidator>
            <ajaxToolkit:ValidatorCalloutExtender runat="Server" ID="NReqE3" TargetControlID="RequiredFieldLeaveType"  />
            <asp:RequiredFieldValidator  Display="None" ID="RequiredFieldReason" runat="server" ErrorMessage="<b>Required Field Missing</b><br />Reason is required." SetFocusOnError="true" ControlToValidate="TextArea1"  BorderStyle="None" ></asp:RequiredFieldValidator>
            <ajaxToolkit:ValidatorCalloutExtender runat="Server" ID="NReqE4" TargetControlID="RequiredFieldReason" />
            <asp:CompareValidator ID="CompareValidator1" ControlToValidate="txtEndDate" ControlToCompare="txtStartDate" Type="Date" Font-Size="Small" Operator="GreaterThanEqual" runat="server"  SetFocusOnError="true" ErrorMessage="End Date must be greater than Start Date."  BorderStyle="None" ></asp:CompareValidator>
            <ajaxToolkit:ValidatorCalloutExtender runat="Server" ID="NReqE5" TargetControlID="CompareValidator1"  />
        </div>
    <asp:ValidationSummary ID="ValidationSummary1" runat="server" HeaderText="Following error occurs:" ShowMessageBox="true" DisplayMode="BulletList" ShowSummary="False" /> </form>
</body>
</html>
