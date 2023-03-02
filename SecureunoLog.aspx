<%@ Page Language="VB" AutoEventWireup="false" CodeFile="SecureunoLog.aspx.vb" Inherits="SecureunoLog" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>Secure-UNO Log</title>
    <link href= "../App_Themes/Css/styles.css" type="text/css" rel="stylesheet" />
    <link href= "../App_Themes/Css/Common.css" type="text/css" rel="stylesheet" />
    <link href="../App_Themes/Css/DataTable.css" rel="stylesheet" type="text/css" />
    <link href="../App_Themes/Css/TableSorter.css" rel="stylesheet" type="text/css" />
    <link href="../App_Themes/Css/Calendar.css" rel="stylesheet" type="text/css" />
    <script src="../App_Themes/JS/jquery-1.4.2.min.js" type="text/javascript"></script>  
    <script src="../App_Themes/JS/jquery.dataTables.min.js" type="text/javascript"></script>  
    
        
</head>
<body >

    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
    <div id="body">
        <div id="cap"></div>
        <div id="main" style="text-align:left ">
        <h1>Secure-UNO Log</h1>
            <table style="text-align:left">
                
                <tr>
                    
                    <td>
                        <asp:TextBox ID="sDate" runat="server" Width="55px"></asp:TextBox><asp:ImageButton ID="ImgBntsDate" runat="server" CausesValidation="False" ImageUrl="~/App_Themes/Images/Calendar_scheduleHS.png" />
                        <asp:RegularExpressionValidator  Display="None" ID="RegularExpressionValidator1" SetFocusOnError="true" runat="server" ErrorMessage="Invalid Start Date (MM/DD/YYYY)" ControlToValidate="sDate" ValidationExpression="\d{2}/\d{2}/\d{4}">*</asp:RegularExpressionValidator>
                        <ajaxToolkit:ValidatorCalloutExtender runat="Server" ID="ValidatorCalloutExtender2" TargetControlID="RegularExpressionValidator1" />    
                        <ajaxtoolkit:calendarextender id="CalendarExtender1" runat="server" popupbuttonid="ImgBntsDate" CssClass="cal_Theme1"
                        targetcontrolid="sDate" Format="MM/dd/yyyy"></ajaxtoolkit:calendarextender>
                    </td>
                    <td></td>
                    <td>
                        <asp:TextBox ID="eDate" Width="55px" runat="server"></asp:TextBox><asp:ImageButton ID="ImgBnteDate" runat="server" CausesValidation="False" ImageUrl="~/App_Themes/Images/Calendar_scheduleHS.png" />
                        <asp:RegularExpressionValidator  Display="None" ID="RegularExpressionValidator2" runat="server" ControlToValidate="eDate" SetFocusOnError="true"
                            ErrorMessage="Invalid End Date (MM/DD/YYYY)" ValidationExpression="\d{2}/\d{2}/\d{4}"></asp:RegularExpressionValidator>&nbsp;
                        <ajaxToolkit:ValidatorCalloutExtender runat="Server" ID="ValidatorCalloutExtender1" TargetControlID="RegularExpressionValidator2" />    
                        <ajaxtoolkit:calendarextender id="CalendarExtender2" Format="MM/dd/yyyy" runat="server" popupbuttonid="ImgBnteDate" CssClass="cal_Theme1"
                        targetcontrolid="eDate"></ajaxtoolkit:calendarextender>
                    </td>
                    
                    <td style="border:0">
                        <asp:Button ID="btnSearch" runat="server" Text="Search"  CssClass="button" />&nbsp;&nbsp; <asp:Button ID="btnExport" runat="server" Text="Export"  CssClass="button" /><br />
                     
                    </td>
                </tr>
                <tr>
                    <td colspan="7" style="text-align:left; border:0"><br /></td>
                </tr>
                <tr id="dlistRow" runat="server">
                    <td colspan="7" style="text-align:left; border:0">
 <asp:GridView ID="GridView1" HeaderStyle-BackColor="#3AC0F2" HeaderStyle-ForeColor="White"
    RowStyle-BackColor="#A1DCF2" AlternatingRowStyle-BackColor="White" AlternatingRowStyle-ForeColor="#000"
    runat="server" AutoGenerateColumns="false">
                 <Columns>
<asp:BoundField DataField="AccountName" HeaderText="Account Name" />   
<asp:BoundField DataField="Patient" HeaderText="Patient Name" />                 
<asp:BoundField DataField="LogDate" HeaderText="Date" />
<asp:BoundField DataField="VoiceNo" HeaderText="Customer Job#" />
<asp:BoundField DataField="Duration" HeaderText="Duration" />
<asp:BoundField DataField="Action" HeaderText="Status" />

</Columns>
<HeaderStyle BackColor="LightBlue" />
</asp:GridView>
                        
                    </td>
                </tr>
                <tr id="lblMsgRow" runat="server">
                    <td colspan="7" style="text-align:left; border:0">
                        <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label>
                    </td>
                </tr>
            </table>
        </div> 
        </div> 
       
    </form>
</body>
</html>
