<%@ Page Language="VB" AutoEventWireup="false" CodeFile="HL7Status_New.aspx.vb" Inherits="FaxPlus_FaxPlusStatus" %>
<%@ Register TagPrefix="DBWC" Namespace="DBauer.Web.UI.WebControls" Assembly="DBauer.Web.UI.WebControls.HierarGrid" %>
<%@ Register
    Assembly="AjaxControlToolkit"
    Namespace="AjaxControlToolkit"
    TagPrefix="ajaxToolkit" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 1.0 Transitional/EN">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>HL7 Track Reports</title>
    <link href= "../App_Themes/Css/styles.css" type="text/css" rel="stylesheet" />
    <link href= "../App_Themes/Css/Common.css" type="text/css" rel="stylesheet" />
</head>
<body>
    <form id="Form1" method="post" target="HL7ResultNew" runat="server" >
        <ajaxtoolkit:toolkitscriptmanager id="ScriptManager1" runat="server"> </ajaxtoolkit:toolkitscriptmanager>
        <div id="body">
            <div id="cap"></div>
            <div id="main" style="text-align:left ">
                <h1>HL7 Track Reports</h1>
                <asp:UpdatePanel runat="server" ID="UpdatePanel1" >
                    <ContentTemplate>
                        <table style="text-align:left">
                            <tr>
                                <td class="alt1">Tracking Job#</td>
                                <td class="alt1">Cutomer Job#</td>
                                <td class="alt1">Dictator First</td>
                                <td class="alt1">Dictator Last</td>
                            </tr>
                            <tr>
                                <td><asp:TextBox ID="Track" runat="server" TabIndex="1" Width="130px"></asp:TextBox></td>                        
                                <td><asp:TextBox ID="Cust" runat="server" TabIndex="2" Width="130px"></asp:TextBox></td>
                                <td><asp:TextBox ID="PFirst" runat="server" TabIndex="3" Width="130px"></asp:TextBox></td>
                                <td><asp:TextBox ID="PLast" runat="server" TabIndex="4" Width="130px"></asp:TextBox></td>
                            </tr>
                            <tr>
                                <td class="alt1">Start Date</td>
                                <td class="alt1">End Date</td>
                                <td class="alt1">Patient Name</td>
                                <td class="alt1">Status</td>
                            </tr>
                            <tr>
                                <td><asp:TextBox ID="sDate" runat="server" TabIndex="6" Width="120px"></asp:TextBox><asp:ImageButton ID="ImgBntsDate" runat="server" CausesValidation="False" ImageUrl="~/images/Calendar_scheduleHS.png" /></td>
                                <td><asp:TextBox ID="eDate" runat="server" TabIndex="7" Width="120px"></asp:TextBox><asp:ImageButton ID="ImgBnteDate" runat="server" CausesValidation="False" ImageUrl="~/images/Calendar_scheduleHS.png" /></td>
                                <td><asp:TextBox ID="PtName" runat="server" TabIndex="9" Width="130px"></asp:TextBox></td>
                                <td><asp:DropDownList ID="FStatus" runat="server" TabIndex="11" Width="140px"></asp:DropDownList></td>
                            </tr>
                        </table>
                        <ajaxtoolkit:calendarextender id="CalendarExtender1" runat="server" popupbuttonid="ImgBntsDate"
                        targetcontrolid="sDate"></ajaxtoolkit:calendarextender>
                        <ajaxtoolkit:calendarextender id="CalendarExtender2" runat="server" popupbuttonid="ImgBnteDate"
                        targetcontrolid="eDate"></ajaxtoolkit:calendarextender> 
                    </ContentTemplate>
                </asp:UpdatePanel>
                <table style="text-align:left">
                    <tr>
                        <td style="border:0">
                            <input name="SEARCH" type="submit" value="Search" />
                        </td>
                    </tr>
                </table> 
            </div> 
        </div>
        
        <iframe id="HL7ResultNew" frameborder="0" name="HL7ResultNew" src="HL7Result_New.aspx" style="width: 100%; height: 352px"
            ></iframe>    
    </form>
</body>
</html>
