<%@ Page Language="VB" AutoEventWireup="false" CodeFile="ImportDutyRoster.aspx.vb" Inherits="LeaveAttendanceMainNew_Supervisor_ImportDutyRoster" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 1.0 Transitional/EN">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Import Duty Roster</title>
    <link href= "../../App_Themes/Css/styles.css" type="text/css" rel="stylesheet" />
    <link href= "../../App_Themes/Css/Common.css" type="text/css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
    
    <div style="text-align:left">
        			
    	<div>
        
        <asp:Table ID="tblMainPage" runat="server">
            <asp:TableRow><asp:TableCell HorizontalAlign="Left" CssClass="common" BorderStyle="None" BorderWidth="0"><a href="ViewRoster.aspx">View Duty Roster</a></asp:TableCell></asp:TableRow>
            <asp:TableRow><asp:TableCell BorderStyle="None" BorderWidth="0"><br /><br /></asp:TableCell></asp:TableRow>
            <asp:TableRow ID="TableRow1" runat="server">
                <asp:TableCell ID="TableCell1" HorizontalAlign="Left" BorderStyle="None" BorderWidth="0" CssClass="common" runat="server">
                    File format should be XLS having below columns and their Format:
                </asp:TableCell>
	        </asp:TableRow>
	        <asp:TableRow ID="TableRow2" runat="server"></asp:TableRow>
            <asp:TableRow ID="TableRow3" runat="server">
                <asp:TableCell ID="TableCell2" runat="server" BorderStyle="None" BorderWidth="0"> 
                    <asp:Table ID="tblFormat" runat="server" Width="90%">
                        <asp:TableRow ID="TableRow4" runat="server">
                            <asp:TableCell ID="TableCell3" runat="server" CssClass="alt1" ToolTip="Date (mm/dd/yyyy)" >
                                Date 
                            </asp:TableCell>
                            <asp:TableCell ID="TableCell4" runat="server" CssClass="alt1" ToolTip="First Shift">
                                I
                            </asp:TableCell>
                            <asp:TableCell ID="TableCell5" runat="server" CssClass="alt1" ToolTip="Second Shift">
                                II
                            </asp:TableCell>
                            <asp:TableCell ID="TableCell6" runat="server" CssClass="alt1" ToolTip="General Shift">
                                G
                            </asp:TableCell>
                            <asp:TableCell ID="TableCell7" runat="server" CssClass="alt1" ToolTip= "Night Shift">
                                N
                            </asp:TableCell>
                            <asp:TableCell ID="TableCell15" runat="server" CssClass="alt1" ToolTip="FullNight">
                                FN
                            </asp:TableCell>
                            <asp:TableCell ID="TableCell17" runat="server" CssClass="alt1" ToolTip="Weekly Off">
                                O
                            </asp:TableCell>                            
                        </asp:TableRow>
                        <%--<asp:TableRow ID="TableRow5" runat="server">
                            <asp:TableCell ID="TableCell8" runat="server">
                                Date
                            </asp:TableCell>
                            <asp:TableCell ID="TableCell9" runat="server">
                                Char
                            </asp:TableCell>
                            <asp:TableCell ID="TableCell10" runat="server">
                                Char
                            </asp:TableCell>
                            <asp:TableCell ID="TableCell11" runat="server">
                                Char
                            </asp:TableCell>
                            <asp:TableCell ID="TableCell12" runat="server">
                                Char
                            </asp:TableCell>
                            <asp:TableCell ID="TableCell16" runat="server">
                                Char
                            </asp:TableCell>
                            <asp:TableCell ID="TableCell18" runat="server">
                                Char
                            </asp:TableCell>                            
                        </asp:TableRow>--%>
                    </asp:Table>
                </asp:TableCell>
            </asp:TableRow>
            <asp:TableRow ID="TableRow6" runat="server"></asp:TableRow>
            <asp:TableRow ID="TableRow7" runat="server">
                <asp:TableCell ID="TableCell13" HorizontalAlign="Left" BorderStyle="None" BorderWidth="0" CssClass="common" runat="server"  >
                    Please mention ETS ID of the employees in respected shift column.
                    <BR>
                    If their are more than one employee please separted by comma(,) in shift column.<BR>
                    Click on Browse button and select the leave balance file on your Computer.<BR> Click on Upload button when done. 
                </asp:TableCell>
            </asp:TableRow>
            <asp:TableRow>
                <asp:TableCell BorderStyle="None" BorderWidth="0">
                    <asp:RequiredFieldValidator  Display="None" ID="RequiredFieldUpload" runat="server" ErrorMessage="Please select file to upload" ControlToValidate="FileUpload"></asp:RequiredFieldValidator>
                </asp:TableCell>
            </asp:TableRow>
            <asp:TableRow>
                <asp:TableCell ID="TableCell14" runat="server" BorderStyle="None" BorderWidth="0">
                    <asp:Table ID="tblUpload" runat="server" Width="80%">  
                        <asp:TableRow >
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
            <asp:TableRow CssClass="HeaderDiv">
                <asp:TableCell HorizontalAlign="Center" ColumnSpan="9" CssClass="HeaderDiv">
                    Data Imported From File
                </asp:TableCell>
            </asp:TableRow>
            <asp:TableRow>
                <asp:TableCell CssClass="alt1">Date</asp:TableCell>
                <asp:TableCell CssClass="alt1">First(I)</asp:TableCell>
                <asp:TableCell CssClass="alt1">Second(II)</asp:TableCell>
                <asp:TableCell CssClass="alt1">General(G)</asp:TableCell>
                <asp:TableCell CssClass="alt1">Night(N)</asp:TableCell>
                <asp:TableCell CssClass="alt1">FullNight(FN)</asp:TableCell>
                <asp:TableCell CssClass="alt1">Off(O)</asp:TableCell>
                <asp:TableCell CssClass="alt1">Imported</asp:TableCell>
            </asp:TableRow>
        </asp:Table>
       </div>
    </div>
    <asp:ValidationSummary ID="ValidationSummary1" runat="server" HeaderText="Following error occurs:" ShowMessageBox="true" DisplayMode="BulletList" ShowSummary="False" /> </form>
</body>
</html>
