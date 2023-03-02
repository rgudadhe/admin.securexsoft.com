<%@ Page Language="VB" AutoEventWireup="false" CodeFile="AttendanceRequest.aspx.vb" Inherits="AttendanceRequest" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 1.0 Transitional/EN">
<script language="javascript" type="text/javascript">			
    function calendarPicker(strTxtRef)
	{
		window.open('Control.aspx?field=' + strTxtRef +'','calendarPopup','titlebar=no,left=470,top=100,width=300,height=250,resizable=no');				
	}
/*	function CheckForm()
	{
	    alert("test");
	    alert(document.frmLeave.)
	    return false;
	}
*/	
</script>
<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Untitled Page</title>
    <LINK href= "styles\Default.css" type="text/css" rel="stylesheet">
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <center>
            <asp:Table ID="Table3" runat="server" Height="1px" HorizontalAlign="Center" Width="734px">
                <asp:TableRow ID="TableRow1" runat="server">
                    <asp:TableCell ID="TableCell1" runat="server" HorizontalAlign="Right">
                        <asp:ImageButton ID="ImgBtn" runat="server" ImageUrl="~/Images/logout.gif" />
                    </asp:TableCell>
                </asp:TableRow>
            </asp:Table>
            &nbsp;</center>
        <center>
            <asp:RequiredFieldValidator ID="txtAttDateValidator" runat="server" ErrorMessage="Please select Attendance Date " ControlToValidate="txtAttDate"></asp:RequiredFieldValidator><BR> 
            <asp:RequiredFieldValidator ID="txtInTimeValidator" runat="server" ErrorMessage="Please enter In-Time(HH:MM:SS) " ControlToValidate="txtInTime"></asp:RequiredFieldValidator><BR>
            <asp:RequiredFieldValidator ID="txtOutTimeValidator" runat="server" ErrorMessage="Please enter Out-Time(HH:MM:SS) " ControlToValidate="txtOutTime"></asp:RequiredFieldValidator><BR>
            <asp:RequiredFieldValidator ID="txtReasonValidator" runat="server" ErrorMessage="Please enter reason for late attendance " ControlToValidate="txtReason"></asp:RequiredFieldValidator><BR>
            <asp:RegularExpressionValidator  Display="None" ID="txtInTimeExpressionValidator" runat="server" ErrorMessage="Please enter valid In-Time" ControlToValidate="txtInTime" ValidationExpression="(0[1-9]|1[0-2]):[0-5][0-9]:[0-5][0-9] ([ap]m|[AP]M)"></asp:RegularExpressionValidator><BR>            
            <asp:RegularExpressionValidator  Display="None" ID="txtOutTimeExpressionValidator" runat="server" ErrorMessage="Please enter valid Out-Time" ControlToValidate="txtOutTime" ValidationExpression="(0[1-9]|1[0-2]):[0-5][0-9]:[0-5][0-9] ([ap]m|[AP]M)"></asp:RegularExpressionValidator>
        </center>
        <center>
            <asp:Table ID="Table1" runat="server" GridLines=Both Width="489px">
                <asp:TableRow runat="server">
                    <asp:TableCell runat="server" CssClass="HeaderDiv" ColumnSpan=2>
                        Attendance Request
                    </asp:TableCell>
                </asp:TableRow>
                <asp:TableRow runat="server">
                    <asp:TableCell Font-Names="Trebuchet MS" ColumnSpan=2 runat="server" HorizontalAlign=Center>
                        Select Attendance Date : <asp:TextBox ID="txtAttDate" runat=server Width=70px ReadOnly=True></asp:TextBox>    
                                                 <asp:HyperLink ID="imgSDate" runat="server" ImageUrl="~/Calendar.gif" ></asp:HyperLink>
                    </asp:TableCell>
                </asp:TableRow>
                <asp:TableRow runat="server">
                    <asp:TableCell Font-Names="Trebuchet MS" HorizontalAlign=Left runat="server">
                        In-Time : 
                        <asp:TextBox ID="txtInTime" runat="server" Font-Names="Trebuchet MS" Width=80px></asp:TextBox>
                        hh:mm:ss
                    </asp:TableCell>
                    <asp:TableCell Font-Names="Trebuchet MS" HorizontalAlign=Left runat="server">
                        Out-Time :
                        <asp:TextBox ID="txtOutTime" runat="server" Font-Names="Trebuchet MS" Width=80px></asp:TextBox>
                        hh:mm:ss
                    </asp:TableCell>
                </asp:TableRow>
                <asp:TableRow>
                    <asp:TableCell Font-Names="Trebuchet MS" ColumnSpan=2 HorizontalAlign=Left >
                        Reason :
                        <textarea id="txtReason" rows="10" cols="60" runat=server></textarea>
                    </asp:TableCell>
                </asp:TableRow>
                
                <asp:TableRow>
                    <asp:TableCell ColumnSpan=2 HorizontalAlign=Center>
                        <asp:Button ID="SendRequest" runat="server" Text="Send Request" />
                    </asp:TableCell>
                </asp:TableRow>                
            </asp:Table>
            
            <ol>
                <asp:Label ID="Label1" runat="server" Font-Names="Trebuchet MS" ForeColor=red Text="Please enter proper In-Time and Out-Time (hh:mm:ss AM/PM)"></asp:Label>
            </ol>
            
            
        </center>
    </div>
    <asp:ValidationSummary ID="ValidationSummary1" runat="server" HeaderText="Following error occurs:" ShowMessageBox="true" DisplayMode="BulletList" ShowSummary="False" /> 
    </form>
</body>
</html>
