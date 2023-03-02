<%@ Page Language="VB" AutoEventWireup="false" CodeFile="ImportDailyAttendance.aspx.vb" Inherits="ImportDailyAttendance" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 1.0 Transitional/EN">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Import Daily Attendance</title>
    <link href= "../../App_Themes/Css/styles.css" type="text/css" rel="stylesheet" />
    <link href= "../../App_Themes/Css/Common.css" type="text/css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
    <div style="text-align:left">
            <asp:Table ID="tblMainPage" runat="server" Width="664px" Height="168px">
                <asp:TableRow ID="TableRow1" runat="server">
                    <asp:TableCell ID="TableCell1" HorizontalAlign="Left" runat="server" BorderStyle="None" BorderWidth="0">
                        File format should be XLS having below columns and their Format:
                    </asp:TableCell>
                </asp:TableRow>
                <asp:TableRow ID="TableRow2" runat="server"></asp:TableRow>
                <asp:TableRow ID="TableRow3" runat="server">
                    <asp:TableCell ID="TableCell2" runat="server" BorderStyle="None" BorderWidth="0"> 
                        <asp:Table ID="tblFormat" runat="server" Width="90%">
                            <asp:TableRow ID="TableRow4" runat="server" CssClass="common">
                                <asp:TableCell ID="TableCell3" runat="server" CssClass="alt1">
                                    ID 
                                </asp:TableCell>
                                <asp:TableCell ID="TableCell5" runat="server" CssClass="alt1">
                                    Attendance Date
                                </asp:TableCell>
                                <asp:TableCell ID="TableCell6" runat="server" CssClass="alt1">
                                    Sign In
                                </asp:TableCell>
                            </asp:TableRow>
                            <asp:TableRow ID="TableRow5" runat="server">
                                <asp:TableCell ID="TableCell8" runat="server">
                                    Char
                                </asp:TableCell>
                                <asp:TableCell ID="TableCell10" runat="server">
                                    Date
                                </asp:TableCell>
                                <asp:TableCell ID="TableCell11" runat="server">
                                    Char
                                </asp:TableCell>
                            </asp:TableRow>
                        </asp:Table>
                    </asp:TableCell>
                </asp:TableRow>
                <asp:TableRow ID="TableRow6" runat="server"></asp:TableRow>
                <asp:TableRow ID="TableRow7" runat="server"></asp:TableRow>
                <asp:TableRow ID="TableRow8" runat="server"></asp:TableRow>
                <asp:TableRow ID="TableRow9" runat="server"></asp:TableRow>
                <asp:TableRow ID="TableRow10" runat="server"></asp:TableRow>
                <asp:TableRow ID="TableRow11" runat="server"></asp:TableRow>
                <asp:TableRow ID="TableRow12" runat="server">
                    <asp:TableCell ID="TableCell13" HorizontalAlign="Left" CssClass="common" BorderStyle="None" BorderWidth="0" runat="server"  >
                        Click on Browse button and select the daily attendance file on your Computer. Click on Upload button when done. 
                    </asp:TableCell>
                </asp:TableRow>
                <asp:TableRow></asp:TableRow>
                <asp:TableRow></asp:TableRow>
                <asp:TableRow></asp:TableRow>
                <asp:TableRow>
                    <asp:TableCell BorderStyle="None" BorderWidth="0">
                        <asp:RequiredFieldValidator  Display="None" ID="RequiredFieldUpload" runat="server" ErrorMessage="Please select file to upload" ControlToValidate="FileUpload"></asp:RequiredFieldValidator>
                    </asp:TableCell>
                </asp:TableRow>
                <asp:TableRow>
                    <asp:TableCell ID="TableCell14" runat="server" BorderStyle="None" BorderWidth="0">
                        <asp:Table ID="tblUpload" runat="server" Width="60%">  
                            <asp:TableRow>
                                <asp:TableCell CssClass="HeaderDiv">
                                    File Upload
                                </asp:TableCell>
                            </asp:TableRow>
                            <asp:TableRow>
                                <asp:TableCell>
                                    <asp:FileUpload ID="FileUpload" Width="90%" runat="server" CssClass="common" />
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
            <asp:Table ID="tblDataImported" runat="server" Width="90%" CssClass="common">
                <asp:TableRow>
                    <asp:TableCell HorizontalAlign="Center" ColumnSpan="5" CssClass="HeaderDiv">
                        Data Imported From File
                    </asp:TableCell>
                </asp:TableRow>
                <asp:TableRow>
                    <asp:TableCell CssClass="alt1">ETS ID</asp:TableCell>
                    <asp:TableCell CssClass="alt1">Employee Name</asp:TableCell>
                    <asp:TableCell CssClass="alt1">Attendance Date</asp:TableCell>
                    <asp:TableCell CssClass="alt1">Sign In</asp:TableCell>
                    <asp:TableCell CssClass="alt1">Imported</asp:TableCell>
                </asp:TableRow>
            </asp:Table>
    </div>
    <asp:ValidationSummary ID="ValidationSummary1" runat="server" HeaderText="Following error occurs:" ShowMessageBox="true" DisplayMode="BulletList" ShowSummary="False" /> </form>
</body>
</html>
