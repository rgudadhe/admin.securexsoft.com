<%@ Page Language="VB" AutoEventWireup="false" CodeFile="EditDayOff.aspx.vb" Inherits="EditDayOff" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 1.0 Transitional/EN">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Edit Off Day</title>
    <link href= "../../App_Themes/Css/styles.css" type="text/css" rel="stylesheet" />
    <link href= "../../App_Themes/Css/Common.css" type="text/css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        <center>
        <asp:Table ID="Table1" runat="server" Width="313px" >
            <asp:TableRow ID="TableRow1" runat="server" > 
                <asp:TableCell ID="TableCell1" HorizontalAlign="Center" ColumnSpan="2" runat="server" CssClass="HeaderDiv">
                    EDIT Off Date         
                </asp:TableCell>
            </asp:TableRow>
            <asp:TableRow ID="TableRow2" runat="server">
                <asp:TableCell ID="TableCell2" runat="server" HorizontalAlign="Right" CssClass="common">
                    Off Date
                </asp:TableCell>
                <asp:TableCell ID="TableCell3" runat="server" HorizontalAlign="Left">
                    <asp:TextBox ID="txtDate" CssClass="common" runat="server" Width="100px"></asp:TextBox>
                    <asp:ImageButton ID="imgSDate" CausesValidation="false" ImageUrl="~/App_Themes/Images/Calendar_scheduleHS.png" runat="server"/> &nbsp &nbsp
                    <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" Format="MM/dd/yyyy" TargetControlID="txtDate" PopupButtonID="imgSDate" BehaviorID="CalendarExtender1" Enabled="True">
                    </ajaxToolkit:CalendarExtender>
                </asp:TableCell>
            </asp:TableRow>
            <asp:TableRow ID="TableRow3" runat="server">
                <asp:TableCell ID="TableCell4" runat="server" HorizontalAlign="Right">
                    Description
                </asp:TableCell>
                <asp:TableCell ID="TableCell5" runat="server" HorizontalAlign="Left">
                    <textarea id="txtDescription" class="common" rows="5" cols="35" runat=server></textarea>
                </asp:TableCell>
            </asp:TableRow>
            <asp:TableRow>
                <asp:TableCell ColumnSpan="2" HorizontalAlign="Center">
                    <center>
                    <asp:Button ID="BtnSubmit" runat="server" Text="Update" CssClass="button"/>
                    <asp:Button ID="BtnDelete" runat="server" Text="Delete" CssClass="button"/>
                    </center>
                </asp:TableCell>
            </asp:TableRow>
        </asp:Table>
        <Br>
        <asp:RequiredFieldValidator  Display="None" ID="RequiredFieldValidator1" runat="server" ErrorMessage="Please select off date" SetFocusOnError="true" ControlToValidate="txtDate" ></asp:RequiredFieldValidator> <BR> 
        <asp:RequiredFieldValidator  Display="None" ID="RequiredFieldValidator2" runat="server" ErrorMessage="Please enter description for date" SetFocusOnError="true" ControlToValidate="txtDescription" ></asp:RequiredFieldValidator>  
        </center>
    </div>
    <asp:ValidationSummary ID="ValidationSummary1" runat="server" HeaderText="Following error occurs:" ShowMessageBox="true" DisplayMode="BulletList" ShowSummary="False" /> </form>
</body>
</html>
