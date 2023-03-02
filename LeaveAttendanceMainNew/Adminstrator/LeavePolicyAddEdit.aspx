<%@ Page Language="VB" AutoEventWireup="false" CodeFile="LeavePolicyAddEdit.aspx.vb" Inherits="LeavePolicyAddEdit" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 1.0 Transitional/EN">
<script language=javascript type="text/javascript">
     function isNumberdotKey(myfield, evt)
    {
        myfield=document.getElementById(myfield.id).value
        var charCode = (evt.which) ? evt.which : event.keyCode
        var keychar = String.fromCharCode(charCode);
        var sindex=myfield.indexOf(".");

        if(!((charCode>47 && charCode<58)||(charCode==46) || (charCode==8))) return false;

        if(sindex >= 0) 
        {   
            if(charCode==46) return false;                
        }   
        return true;
    }
</script>

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>LeavePolicyAddEdit</title>
    <link href= "../../App_Themes/Css/styles.css" type="text/css" rel="stylesheet" />
    <link href= "../../App_Themes/Css/Common.css" type="text/css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:Table ID="tblMain" runat="server" HorizontalAlign="Center" Width="81%">
            <asp:TableRow HorizontalAlign="Center" runat="server">
                <asp:TableCell ColumnSpan="2" HorizontalAlign="Center" runat="server" CssClass="HeaderDiv" >
                    <asp:Label ID="lblPolicy" runat="server"></asp:Label>
                </asp:TableCell>
            </asp:TableRow>
            <asp:TableRow runat="server">
                <asp:TableCell runat="server" CssClass="common">
                    Day
                </asp:TableCell>
                <asp:TableCell runat="server">
                    <asp:TextBox ID="txtDay" onkeypress="return isNumberdotKey(this,event);" runat="server" Width="80px" CssClass="common"></asp:TextBox>
                    &nbsp
                    <asp:RequiredFieldValidator  Display="None" ID="RequiredFieldtxtDay" runat="server" ErrorMessage="Please enter day" ControlToValidate="txtDay" SetFocusOnError="True" ></asp:RequiredFieldValidator>
                </asp:TableCell>
            </asp:TableRow>
            <asp:TableRow runat="server">
                <asp:TableCell CssClass="common" runat="server">
                    Month
                </asp:TableCell>
                <asp:TableCell runat="server">
                    <asp:DropDownList ID="DropDownMonth" runat="server" CssClass="common">
                        <asp:ListItem Text="Please Select" Value=""></asp:ListItem>
                        <asp:ListItem Text="January" Value="January"></asp:ListItem>
                        <asp:ListItem Text="February" Value="February"></asp:ListItem>
                        <asp:ListItem Text="March" Value="March"></asp:ListItem>
                        <asp:ListItem Text="April" Value="April"></asp:ListItem>
                        <asp:ListItem Text="May" Value="May"></asp:ListItem>
                        <asp:ListItem Text="June" Value="June"></asp:ListItem>
                        <asp:ListItem Text="July" Value="July"></asp:ListItem>
                        <asp:ListItem Text="August" Value="August"></asp:ListItem>
                        <asp:ListItem Text="September" Value="September"></asp:ListItem>
                        <asp:ListItem Text="October" Value="October"></asp:ListItem>
                        <asp:ListItem Text="November" Value="November"></asp:ListItem>
                        <asp:ListItem Text="December" Value="December"></asp:ListItem>
                    </asp:DropDownList>
                    &nbsp
                    <asp:RequiredFieldValidator  Display="None" ID="RequiredFieldMonth" runat="server" ErrorMessage="Please select month" ControlToValidate="DropDownMonth" SetFocusOnError="True" ></asp:RequiredFieldValidator>
                </asp:TableCell>
            </asp:TableRow>
            <asp:TableRow runat="server">
                <asp:TableCell runat="server" CssClass="common">
                    CL                     
                </asp:TableCell>
                <asp:TableCell runat="server">
                    <asp:TextBox ID="txtCL" CssClass="common" runat="server" onkeypress="return isNumberdotKey(this,event);"  Width="80px"></asp:TextBox>
                    &nbsp
                    <asp:RequiredFieldValidator  Display="None" ID="RequiredFieldCL" runat="server" ErrorMessage="Please enter CL" ControlToValidate="txtCL" SetFocusOnError="True" ></asp:RequiredFieldValidator>
                </asp:TableCell>
            </asp:TableRow>
            <asp:TableRow runat="server">
                <asp:TableCell CssClass="common" runat="server">
                    EL
                </asp:TableCell>
                <asp:TableCell runat="server">
                    <asp:TextBox ID="txtEL" runat="server" onkeypress="return isNumberdotKey(this,event);" Width="80px" CssClass="common"></asp:TextBox>
                    &nbsp
                    <asp:RequiredFieldValidator  Display="None" ID="RequiredFieldEL" runat="server" ErrorMessage="Please enter EL" ControlToValidate="txtEL" SetFocusOnError=True></asp:RequiredFieldValidator>
                </asp:TableCell>
            </asp:TableRow>
            <asp:TableRow runat="server">
                <asp:TableCell ColumnSpan=2 HorizontalAlign="Center" runat="server">
                    <center>
                        <asp:Button ID="BtnSubmit" runat="server" Text="Submit" CssClass="button" /> &nbsp
                        <asp:Button ID="BtnDelete" runat="server" Text="Delete" CssClass="button" Visible="false"  />
                    </center>
                </asp:TableCell>
            </asp:TableRow>
        </asp:Table>    
    </div>
    <asp:ValidationSummary ID="ValidationSummary1" runat="server" HeaderText="Following error occurs:" ShowMessageBox="true" DisplayMode="BulletList" ShowSummary="False" /> </form>
</body>
</html>
