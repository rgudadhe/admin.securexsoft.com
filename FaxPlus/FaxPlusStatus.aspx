<%@ Page Language="VB" AutoEventWireup="false" CodeFile="FaxPlusStatus.aspx.vb" Inherits="FaxPlus_FaxPlusStatus" %>
<%@ Register TagPrefix="DBWC" Namespace="DBauer.Web.UI.WebControls" Assembly="DBauer.Web.UI.WebControls.HierarGrid" %>
<%@ Register
    Assembly="AjaxControlToolkit"
    Namespace="AjaxControlToolkit"
    TagPrefix="ajaxToolkit" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 1.0 Transitional/EN">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Fax Plus Status</title>
    <link href= "../App_Themes/Css/styles.css" type="text/css" rel="stylesheet"/>
    <link href= "../App_Themes/Css/Common.css" type="text/css" rel="stylesheet"/>
    <link href= "../App_Themes/Css/Calendar.css" type="text/css" rel="stylesheet" />
</head>
<body>
    <form id="Form1" method="post" target="FaxPlusResult" runat="server" >
        <div id="body">
        <div id="cap"></div>
        <div id="main">
        <h1>FaxPlus Job Status</h1>
            <asp:Panel ID="Panel1" runat="server" HorizontalAlign="Left">
                <div>
        <ajaxtoolkit:toolkitscriptmanager id="ScriptManager1" runat="server"> </ajaxtoolkit:toolkitscriptmanager>
        <asp:UpdatePanel ID="up2" runat="server">
            <ContentTemplate>                
                <table>
                    <tr>
                        <td colspan="4" class="HeaderDiv">
                            Job Details
                        </td>
                    </tr>
                    <tr>
                        <td class="alt">
                            Tracking Job#
                        </td>
                        <td class="alt">
                            Cutomer Job#
                        </td>
                        <td class="alt">
                            Dictator First
                        </td>
                        <td class="alt">
                            Dictator Last
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:TextBox ID="Track" runat="server" TabIndex="1" Width="130px"></asp:TextBox></td>
                        <td>
                            <asp:TextBox ID="Cust" runat="server" TabIndex="2" Width="130px"></asp:TextBox></td>
                        <td>
                            <asp:TextBox ID="PFirst" runat="server" TabIndex="3" Width="130px"></asp:TextBox></td>
                        <td>
                            <asp:TextBox ID="PLast" runat="server" TabIndex="4" Width="130px"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td class="alt">
                            Start Date
                        </td>
                        <td class="alt">
                            End Date
                        </td>
                        <td class="alt">
                            Account Name
                        </td> 
                        <td class="alt">
                            Fax Status
                        </td> 
                    </tr>
                    <tr>
                        <td>
                            <asp:TextBox ID="sDate" runat="server" TabIndex="6" Width="120px"></asp:TextBox>
                            <asp:ImageButton ID="ImgBntsDate" runat="server" CausesValidation="False" ImageUrl="~/App_Themes/Images/Calendar_scheduleHS.png" />
                        </td>
                        <td>
                            <asp:TextBox ID="eDate" runat="server" TabIndex="7" Width="120px"></asp:TextBox>
                            <asp:ImageButton ID="ImgBnteDate" runat="server" CausesValidation="False" ImageUrl="~/App_Themes/Images/Calendar_scheduleHS.png" />
                        </td>
                        <td>
                            <asp:TextBox ID="AccName" runat="server" TabIndex="9" Width="130px"></asp:TextBox></td>
                        <td>
                            <asp:DropDownList ID="FStatus" runat="server" TabIndex="11" Width="140px">
                                <asp:ListItem Selected="True">Any</asp:ListItem>
                                <asp:ListItem Value="0">Pending Fax</asp:ListItem>
                                <asp:ListItem Value="1">Finished</asp:ListItem>
                            </asp:DropDownList></td>
                    </tr>
                    
                    <ajaxtoolkit:calendarextender id="CalendarExtender1" runat="server" popupbuttonid="ImgBntsDate" CssClass="cal_Theme1"
                        targetcontrolid="sDate"></ajaxtoolkit:calendarextender>
                    <ajaxtoolkit:calendarextender id="CalendarExtender2" runat="server" popupbuttonid="ImgBnteDate" CssClass="cal_Theme1"
                        targetcontrolid="eDate"></ajaxtoolkit:calendarextender>                    
                </table>
            </ContentTemplate>
        </asp:UpdatePanel>
        <table>
            <tr>
                <td colspan="5">
                    <input name="SEARCH" type="submit" class="button" value="Search" />
                </td>
            </tr>
        </table>
        <iframe id="FaxPlusResult" frameborder="0" name="FaxPlusResult" src="FaxPlusResult.aspx" style="width: 100%; height: 368px"
            ></iframe>    
    </div>
            </asp:Panel>
    
    </div> 
    </div> 
    </form>
</body>
</html>
