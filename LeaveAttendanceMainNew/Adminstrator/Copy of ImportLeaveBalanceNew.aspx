<%@ Page Language="VB" AutoEventWireup="false" CodeFile="Copy of ImportLeaveBalanceNew.aspx.vb" Inherits="ImportLeaveBalanceNew" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 1.0 Transitional/EN">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Import Leave Balance sheet</title>
    <link href= "../../App_Themes/Css/styles.css" type="text/css" rel="stylesheet" />
    <link href= "../../App_Themes/Css/Common.css" type="text/css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
    	<div style="text-align:left">
        <asp:Table ID="tblMainPage" runat="server">
            <asp:TableRow ID="TableRow1" runat="server">
                <asp:TableCell ID="TableCell1" HorizontalAlign="Left" BorderStyle="None" BorderWidth="0" runat="server">
                    File format should be XLS having below columns and their Format:
                </asp:TableCell>
	        </asp:TableRow>
	        <asp:TableRow runat="server"></asp:TableRow>
            <asp:TableRow runat="server">
                <asp:TableCell runat="server" BorderStyle="None" BorderWidth="0"> 
                    <asp:Table ID="tblFormat" runat="server" CssClass="common" Width="90%">
                        <asp:TableRow CssClass="SMSelected" runat="server">
                            <asp:TableCell runat="server" CssClass="alt1">
                                ID 
                            </asp:TableCell>
                            <asp:TableCell runat="server" CssClass="alt1">
                                CasualLeaves
                            </asp:TableCell>
                            <asp:TableCell runat="server" CssClass="alt1">
                                EarnedLeaves
                            </asp:TableCell>
                            <asp:TableCell runat="server" CssClass="alt1">
                                WeeklyOff1
                            </asp:TableCell>
                            <asp:TableCell runat="server" CssClass="alt1">
                                WeeklyOff2
                            </asp:TableCell>
                        </asp:TableRow>
                        <asp:TableRow runat="server">
                            <asp:TableCell runat="server">
                                Char
                            </asp:TableCell>
                            <asp:TableCell runat="server">
                                Number
                            </asp:TableCell>
                            <asp:TableCell runat="server">
                                Number
                            </asp:TableCell>
                            <asp:TableCell runat="server">
                                Char
                            </asp:TableCell>
                            <asp:TableCell runat="server">
                                Char
                            </asp:TableCell>
                        </asp:TableRow>
                    </asp:Table>
                </asp:TableCell>
            </asp:TableRow>
            <asp:TableRow runat="server"></asp:TableRow>
            <asp:TableRow runat="server">
                <asp:TableCell runat="server" BorderStyle="None" BorderWidth="0">
                    Click on Browse button and select the leave balance file on your Computer.<BR> Click on Upload button when done. 
                </asp:TableCell>
            </asp:TableRow>
            <asp:TableRow>
                <asp:TableCell BorderStyle="None" BorderWidth="0">
                    <asp:RequiredFieldValidator  Display="None" ID="RequiredFieldUpload" runat="server" ErrorMessage="Please select file to upload" ControlToValidate="FileUpload"></asp:RequiredFieldValidator>
                </asp:TableCell>
            </asp:TableRow>
            <asp:TableRow>
                <asp:TableCell runat="server" BorderStyle="None" BorderWidth="0">
                    <asp:Table ID="tblUpload" runat="server" Width="80%">  
                        <asp:TableRow>
                            <asp:TableCell CssClass="HeaderDiv">
                                File Upload
                            </asp:TableCell>
                        </asp:TableRow>
                        <asp:TableRow>
                            <asp:TableCell>
                                <asp:FileUpload ID="FileUpload" CssClass="common" Width="90%" runat="server" />
                            </asp:TableCell>
                        </asp:TableRow>
                        <asp:TableRow>
                            <asp:TableCell HorizontalAlign="Center">
                                <center>
                                    <asp:Button ID="btnUpload" runat="server" Text="Upload" CssClass="button" />
                                </center>
                            </asp:TableCell>
                        </asp:TableRow>
                    </asp:Table>
                </asp:TableCell>
            </asp:TableRow>
        </asp:Table>
        <asp:Table ID="tblDataImported" runat="server" Width="100%">
            <asp:TableRow>
                <asp:TableCell HorizontalAlign="Center" ColumnSpan="9" CssClass="HeaderDiv">
                    Data Imported From File
                </asp:TableCell>
            </asp:TableRow>
            <asp:TableRow>
                <asp:TableCell CssClass="HeaderDiv">&nbsp</asp:TableCell>
                <asp:TableCell ColumnSpan="4" HorizontalAlign="center" CssClass="HeaderDiv">
                    Before Data Imported
                </asp:TableCell>
                <asp:TableCell ColumnSpan="4" HorizontalAlign="center" CssClass="HeaderDiv">
                    After Data Imported
                </asp:TableCell>
            </asp:TableRow>
            <asp:TableRow>
                <asp:TableCell CssClass="alt1">Employee Name</asp:TableCell>
                <asp:TableCell CssClass="alt1">CL</asp:TableCell>
                <asp:TableCell CssClass="alt1">EL</asp:TableCell>
                <asp:TableCell CssClass="alt1">WOff1</asp:TableCell>
                <asp:TableCell CssClass="alt1">WOff2</asp:TableCell>
                <asp:TableCell CssClass="alt1">CL</asp:TableCell>
                <asp:TableCell CssClass="alt1">EL</asp:TableCell>
                <asp:TableCell CssClass="alt1">WOff1</asp:TableCell>
                <asp:TableCell CssClass="alt1">WOff2</asp:TableCell>
            </asp:TableRow>
        </asp:Table>
       </div>
    <asp:ValidationSummary ID="ValidationSummary1" runat="server" HeaderText="Following error occurs:" ShowMessageBox="true" DisplayMode="BulletList" ShowSummary="False" /> </form>
</body>
</html>
