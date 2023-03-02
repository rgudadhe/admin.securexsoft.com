<%@ Page Language="VB" AutoEventWireup="false" CodeFile="HL7Status.aspx.vb" Inherits="FaxPlus_FaxPlusStatus" %>
<%@ Register TagPrefix="DBWC" Namespace="DBauer.Web.UI.WebControls" Assembly="DBauer.Web.UI.WebControls.HierarGrid" %>
<%@ Register
    Assembly="AjaxControlToolkit"
    Namespace="AjaxControlToolkit"
    TagPrefix="ajaxToolkit" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 1.0 Transitional/EN">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Untitled Page</title>
    <link href= "../styles/Default.css" type="text/css" rel="stylesheet"/>
</head>
<body>
    <form id="Form1" method="post" target="HL7Result" runat="server" >
    <div>
        <ajaxtoolkit:toolkitscriptmanager id="ScriptManager1" runat="server"> </ajaxtoolkit:toolkitscriptmanager>
        <asp:UpdatePanel ID="up2" runat="server">
            <ContentTemplate>                
                <table style="background-color: whitesmoke">
                    <tr>
                        <td style="height: 25px">
                            <asp:TextBox ID="TextBox1" runat="server" BorderStyle="None" CssClass="SearchCol"
                                Height="20" ReadOnly="true" TabIndex="100" Text="Tracking Job#"></asp:TextBox>
                        </td>
                        <td style="height: 25px">
                            <asp:TextBox ID="TextBox2" runat="server" BorderStyle="None" CssClass="SearchCol"
                                Height="20" ReadOnly="true" TabIndex="100" Text="Cutomer Job#"></asp:TextBox>
                        </td>
                        <td style="height: 25px">
                            <asp:TextBox ID="TextBox3" runat="server" BorderStyle="None" CssClass="SearchCol"
                                Height="20" ReadOnly="true" TabIndex="100" Text="Dictator First"></asp:TextBox>
                        </td>
                        <td style="height: 25px">
                            <asp:TextBox ID="TextBox4" runat="server" BorderStyle="None" CssClass="SearchCol"
                                Height="20" ReadOnly="true" TabIndex="100" Text="Dictator Last"></asp:TextBox>
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
                        <td style="height: 19px">
                            <asp:TextBox ID="TextBox5" runat="server" BorderStyle="None" CssClass="SearchCol"
                                Height="20" ReadOnly="true" TabIndex="100" Text="Start Date"></asp:TextBox>
                        </td>
                        <td style="height: 19px">
                            <asp:TextBox ID="TextBox6" runat="server" BorderStyle="None" CssClass="SearchCol"
                                Height="20" ReadOnly="true" TabIndex="100" Text="End Date"></asp:TextBox>
                        </td>
                        <td style="height: 19px">
                            &nbsp;<asp:TextBox ID="TextBox8" runat="server" BorderStyle="None" CssClass="SearchCol"
                                Height="20" ReadOnly="true" TabIndex="100" Text="Patient Name"></asp:TextBox></td>
                        <td style="height: 19px">
                            &nbsp;<asp:TextBox ID="TextBox9" runat="server" BorderStyle="None" CssClass="SearchCol"
                                Height="20" ReadOnly="true" TabIndex="100" Text=" Status"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td style="height: 2px">
                            <asp:TextBox ID="sDate" runat="server" TabIndex="6" Width="120px"></asp:TextBox>
                            <asp:ImageButton ID="ImgBntsDate" runat="server" CausesValidation="False" ImageUrl="~/images/Calendar_scheduleHS.png" />
                        </td>
                        <td style="height: 2px">
                            <asp:TextBox ID="eDate" runat="server" TabIndex="7" Width="120px"></asp:TextBox>
                            <asp:ImageButton ID="ImgBnteDate" runat="server" CausesValidation="False" ImageUrl="~/images/Calendar_scheduleHS.png" />
                        </td>
                        <td style="height: 2px">
                            <asp:TextBox ID="PtName" runat="server" TabIndex="9" Width="130px"></asp:TextBox></td>
                        <td style="height: 2px">
                            <asp:DropDownList ID="FStatus" runat="server" TabIndex="11" Width="140px">
                            </asp:DropDownList></td>
                    </tr>
                    
                    <ajaxtoolkit:calendarextender id="CalendarExtender1" runat="server" popupbuttonid="ImgBntsDate"
                        targetcontrolid="sDate"></ajaxtoolkit:calendarextender>
                    <ajaxtoolkit:calendarextender id="CalendarExtender2" runat="server" popupbuttonid="ImgBnteDate"
                        targetcontrolid="eDate"></ajaxtoolkit:calendarextender>                    
                </table>
            </ContentTemplate>
        </asp:UpdatePanel>
        <table>
            <tr>
                <td colspan="5">
                    <input name="SEARCH" type="submit" value="Search" />
                </td>
            </tr>
        </table>
        <iframe id="HL7Result" frameborder="0" name="HL7Result" src="HL7Result.aspx" style="width: 100%; height: 352px"
            ></iframe>    
    </div>
    </form>
</body>
</html>
