<%@ Page Language="VB" AutoEventWireup="false" CodeFile="Supervisor.aspx.vb" Inherits="Department_Supervisor" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 1.0 Transitional/EN">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Supervisor</title>
    <link href= "../App_Themes/Css/styles.css" type="text/css" rel="stylesheet" />
    <link href="../App_Themes/Css/Common.css" type="text/css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <div id="body">
        <div id="cap"></div>
        <div id="main">
        <h1>Edit Supervisor</h1>
        <asp:Panel ID="Panel1" runat="server" HorizontalAlign="Left">
            <div>
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        
        <asp:Table ID="Table1" runat="server" Width="70%">
            <asp:TableRow ID="TableRow1" runat="server">
                <asp:TableCell ID="TableCell1" runat="server" CssClass="alt1">
                    Select Department 
                </asp:TableCell>
                <asp:TableCell ID="TableCell2" runat="server" CssClass="alt1" >
                    Select User
                </asp:TableCell>
             </asp:TableRow>
             <asp:TableRow>
                <asp:TableCell>
                    <asp:DropDownList ID="DropDownDept" CssClass="common" runat="server" Width="200" AutoPostBack="true">
                    </asp:DropDownList>
                </asp:TableCell>
                <asp:TableCell>
                    <asp:DropDownList ID="DropDownUsers" CssClass="common" runat="server" Width=200>
                    </asp:DropDownList>
                </asp:TableCell>
            </asp:TableRow>
            <asp:TableRow>
                <asp:TableCell ColumnSpan="2" HorizontalAlign="Center">
                    <center>
                    <asp:Button ID="BtnAdd" CssClass="button" runat="server" Text="Add Supervisor" />
                    </center>
                </asp:TableCell>
            </asp:TableRow>
        </asp:Table>      
        <asp:RequiredFieldValidator  Display="None" ID="RequiredFieldValidator1" runat="server" ErrorMessage="Please select department" ControlToValidate="DropDownDept" ></asp:RequiredFieldValidator>
        <asp:RequiredFieldValidator  Display="None" ID="RequiredFieldValidator2" runat="server" ErrorMessage="Please select user" ControlToValidate="DropDownUsers" ></asp:RequiredFieldValidator>
        <BR><BR>
        <hr />
        <asp:Table ID="tblList" runat="server" Width=80%>
            <asp:TableRow ID="TableRow2" runat="server" CssClass="HeaderDiv">
                <asp:TableCell HorizontalAlign=Center CssClass="HeaderDiv" ColumnSpan=4>
                    Existing Supervior
                </asp:TableCell>
            </asp:TableRow>
            <asp:TableRow>
                <asp:TableCell CssClass="alt1" HorizontalAlign=Center>
                    EmpID
                </asp:TableCell>
                <asp:TableCell CssClass="alt1" HorizontalAlign=Center>
                    Name
                </asp:TableCell>
                <asp:TableCell CssClass="alt1" HorizontalAlign=Center>
                    Department Name
                </asp:TableCell>
                <asp:TableCell CssClass="alt1" HorizontalAlign=Center>
                    &nbsp
                </asp:TableCell>
            </asp:TableRow>
        </asp:Table>
    </div>
        </asp:Panel>
    </div> 
    </div> 
        <asp:HiddenField ID="hdnSupList" runat="server" />
    <asp:ValidationSummary ID="ValidationSummary1" runat="server" HeaderText="Following error occurs:" ShowMessageBox="true" DisplayMode="BulletList" ShowSummary="False" /> </form>
</body>
</html>
