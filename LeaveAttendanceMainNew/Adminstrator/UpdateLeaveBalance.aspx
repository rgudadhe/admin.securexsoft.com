<%@ Page Language="VB" AutoEventWireup="false" CodeFile="UpdateLeaveBalance.aspx.vb" Inherits="UpdateLeaveBalance" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 1.0 Transitional/EN">
<script language=javascript type="text/javascript" >
    function Enable(Value)
    {
        if (Value)
        {
            document.getElementById('WeekOff2').setAttribute("disabled" , false);
        }
        else
        {
            document.getElementById('WeekOff2').setAttribute("disabled" , true);
        } 
    }
    function Checked()
    {
        document.getElementById('Checkbox1').setAttribute("checked",checked);
    }

</script>

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Edit Leave Balance</title>
    <link href= "../../App_Themes/Css/styles.css" type="text/css" rel="stylesheet" />
    <link href= "../../App_Themes/Css/Common.css" type="text/css" rel="stylesheet" />

</head>
<body>
    <form id="form1" runat="server">
        <div style="text-align:left">
	        <a class="common" href="JavaScript:history.go(-1)">Return to list</a>
	    </div>
    	<div>
            <center>
                <asp:CompareValidator ID="CompareValidator1" ControlToValidate="WeekOff2" ControlToCompare="WeekOff1" Type="String" Operator="NotEqual" runat="server" ErrorMessage="Both WeeklyOffs On Same Day Not Allowed!!" ></asp:CompareValidator>
            </center>
            <center>
                <asp:Table ID="Table1" runat="server" Width="422px">
                    <asp:TableRow runat="server">
                        <asp:TableCell ColumnSpan ="2" runat="server" HorizontalAlign="Center" CssClass="HeaderDiv">
                            Employee LeaveBalance Summary
                        </asp:TableCell>         
                    </asp:TableRow>
                    <asp:TableRow runat="server">
                        <asp:TableCell HorizontalAlign="Left" CssClass="common" Width="150px" runat="server">
                            Employee Name
                        </asp:TableCell>
                        <asp:TableCell HorizontalAlign="Left" CssClass="common" runat="server">
                            <asp:TextBox ID="EmpName" runat="server" Width="250px" ReadOnly=True></asp:TextBox>
                        </asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow runat="server">
                        <asp:TableCell HorizontalAlign="Left" CssClass="common" Width="150px" runat="server">
                            Casual Leaves
                        </asp:TableCell>
                        <asp:TableCell HorizontalAlign="Left" runat="server">
                            <asp:TextBox ID="CL" CssClass="common" runat="server" Width="110px"></asp:TextBox> &nbsp 
                            <asp:RegularExpressionValidator  Display="None" ID="RegularExpressionValidator1" ControlToValidate="CL" ValidationExpression="(\+|-)?([0-9]+\.?[0-9]*|\.[0-9]+)([eE](\+|-)?[0-9]+)?" runat="server" ErrorMessage="Accept only numeric"></asp:RegularExpressionValidator>
                        </asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow runat="server">
                        <asp:TableCell HorizontalAlign="Left" CssClass="common" Width="150px" runat="server">
                            Earned Leaves
                        </asp:TableCell>
                        <asp:TableCell HorizontalAlign="Left" runat="server">
                            <asp:TextBox ID="EL" CssClass="common" runat="server" Width="110px"></asp:TextBox> &nbsp 
                            <asp:RegularExpressionValidator  Display="None" ID="RegularExpressionValidator2" ControlToValidate="EL" ValidationExpression="(\+|-)?([0-9]+\.?[0-9]*|\.[0-9]+)([eE](\+|-)?[0-9]+)?" runat="server" ErrorMessage="Accept only numeric"></asp:RegularExpressionValidator>
                        </asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow runat="server">
                        <asp:TableCell HorizontalAlign="Left" CssClass="common" Width="150px" runat="server">
                            WeeklyOff1
                        </asp:TableCell>
                        <asp:TableCell HorizontalAlign="Left" CssClass="common" runat="server">
                            <asp:DropDownList ID="WeekOff1" runat="server" CssClass="common">
                                <asp:ListItem Value="Sunday" Text="Sunday"></asp:ListItem>
                                <asp:ListItem Value="Monday" Text="Monday"></asp:ListItem>
                                <asp:ListItem Value="Tuesday" Text="Tuesday"></asp:ListItem>
                                <asp:ListItem Value="Wendesday" Text="Wendesday"></asp:ListItem>
                                <asp:ListItem Value="Thrusday" Text="Thrusday"></asp:ListItem>
                                <asp:ListItem Value="Friday" Text="Friday"></asp:ListItem>
                                <asp:ListItem Value="Saturday" Text="Saturday"></asp:ListItem>
                            </asp:DropDownList>
                        </asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow runat="server">
                        <asp:TableCell HorizontalAlign="Left" CssClass="common" Width="150px" runat="server">
                            WeeklyOff2
                            <input id="Checkbox1" type="checkbox" runat="server" onClick="javascript:Enable(true)" />
                        </asp:TableCell>
                        <asp:TableCell HorizontalAlign="Left" runat="server" CssClass="common">
                            <asp:DropDownList ID="WeekOff2" runat="server" CssClass="common">
                                <asp:ListItem Value="Sunday" Text="Sunday"></asp:ListItem>
                                <asp:ListItem Value="Monday" Text="Monday"></asp:ListItem>
                                <asp:ListItem Value="Tuesday" Text="Tuesday"></asp:ListItem>
                                <asp:ListItem Value="Wendesday" Text="Wendesday"></asp:ListItem>
                                <asp:ListItem Value="Thrusday" Text="Thrusday"></asp:ListItem>
                                <asp:ListItem Value="Friday" Text="Friday"></asp:ListItem>
                                <asp:ListItem Value="Saturday" Text="Saturday" Selected="True"></asp:ListItem>
                            </asp:DropDownList>
                        </asp:TableCell>
                    </asp:TableRow>
                </asp:Table>
                <BR/>
                <BR/>
                <asp:Button ID="Button1" runat="server" Text="UPDATE" CssClass="button" /> 
                &nbsp &nbsp   <asp:Button ID="BtnCancel" runat="server" CssClass="button" Text="CANCEL"/>
            </center>
        </div> 
    <asp:ValidationSummary ID="ValidationSummary1" runat="server" HeaderText="Following error occurs:" ShowMessageBox="true" DisplayMode="BulletList" ShowSummary="False" /> </form>
</body>
</html>
