<%@ Page Language="VB" AutoEventWireup="false" CodeFile="EditShift.aspx.vb" Inherits="LeaveAttendanceMainNew_Supervisor_EditShift" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Edit Shift</title>
    <link href= "../../App_Themes/Css/styles.css" type="text/css" rel="stylesheet" />
    <link href= "../../App_Themes/Css/Common.css" type="text/css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:Table ID="tblMain" runat="server" HorizontalAlign="Center" Width="81%">
            <asp:TableRow>
                <asp:TableCell ColumnSpan="2" CssClass="HeaderDiv">
                    <center>Edit Shift</center>
                </asp:TableCell>
            </asp:TableRow>
            <asp:TableRow>
                <asp:TableCell>
                    Employee Name
                </asp:TableCell>
                <asp:TableCell ID="EName" runat="server">
                    
                </asp:TableCell>
            </asp:TableRow>
            <asp:TableRow>
                <asp:TableCell>
                    Shift Date
                </asp:TableCell>
                <asp:TableCell ID="SDate" runat="server">
                    
                </asp:TableCell>
            </asp:TableRow>
            <asp:TableRow>
                <asp:TableCell>
                    Shift Prefix
                </asp:TableCell>
                <asp:TableCell>
                    <asp:DropDownList ID="ddlShiftPrefix" runat="server">
                        <asp:ListItem Text="I" Value="I"></asp:ListItem>
                        <asp:ListItem Text="II" Value="II"></asp:ListItem>
                        <asp:ListItem Text="G" Value="G"></asp:ListItem>
                        <asp:ListItem Text="N" Value="N"></asp:ListItem>
                        <asp:ListItem Text="FN" Value="FN"></asp:ListItem>                            
                        <asp:ListItem Text="O" Value="O"></asp:ListItem>
                    </asp:DropDownList>                    
                </asp:TableCell>
            </asp:TableRow>
            <asp:TableRow>
                <asp:TableCell ColumnSpan="2" HorizontalAlign="Center">
                    <center>
                        <asp:Button ID="btnSubmit" runat="server" Text="Submit" CssClass="button" />
                    </center>
                </asp:TableCell>
            </asp:TableRow>
        </asp:Table> 
    </div>
    </form>
</body>
</html>
