<%@ Page Language="VB" AutoEventWireup="false" CodeFile="UpdatePhoneNo.aspx.vb" Inherits="UpdatePhoneNo" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../App_Themes/Css/Main.css" type="text/css" rel="stylesheet" />
    <link href="../App_Themes/Css/Common.css" type="text/css" rel="stylesheet" />
    <link href="../App_Themes/Css/Styles.css" type="text/css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">

    <div id="body" style="height: 100%;">
        <div id="cap">
        </div>
        <div id="main">
            <h1>
                Update User Phone number for SCA Arise STAT
            </h1>
        </div>
        <table width="40%">
            <tr>
                <td colspan="2" style="text-align: center">
                    Select User
                </td>
            </tr>
            <tr>
                <td>
                    User
                </td>
                <td>
                    <asp:DropDownList ID="DropDownList1" runat="server" AutoPostBack="true">
                        <asp:ListItem Text="Select User" Selected="True"></asp:ListItem>
                        <asp:ListItem Text="Macchindra" Value="9503270537"></asp:ListItem>
                        <asp:ListItem Text="Vijay" Value="9975023241"></asp:ListItem>
                        <asp:ListItem Text="Sameer" Value="9975614697"></asp:ListItem>
                        <asp:ListItem Text="Abhinandan" Value="9764870899"></asp:ListItem>
                        <asp:ListItem Text="Rahul" Value="9881374471"></asp:ListItem>
                        <asp:ListItem Text="Amol Bodhale" Value="8446441177"></asp:ListItem>
                        <asp:ListItem Text="Arokiyadass Chinniya" Value="9860181429"></asp:ListItem>
                        <asp:ListItem Text="Dharmendra Sarabi" Value="9766322196"></asp:ListItem>
                        <asp:ListItem Text="Dinkar Bhosale" Value="9325266773"></asp:ListItem>
                        <asp:ListItem Text="Umesh Jadhav" Value="9850966210"></asp:ListItem>
                        
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td>
                    Phone No
                </td>
                <td>
                    <asp:TextBox ID="txtphone" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    Date
                </td>
                <td>
                    <asp:Calendar ID="Calendar1" runat="server" BackColor="White" 
                        BorderColor="#3366CC" BorderWidth="1px" CellPadding="1" 
                        DayNameFormat="Shortest" Font-Names="Verdana" Font-Size="8pt" 
                        ForeColor="#003399" Height="200px" Width="220px">
                        <DayHeaderStyle BackColor="#99CCCC" ForeColor="#336666" Height="1px" />
                        <NextPrevStyle Font-Size="8pt" ForeColor="#CCCCFF" />
                        <OtherMonthDayStyle ForeColor="#999999" />
                        <SelectedDayStyle BackColor="#009999" Font-Bold="True" ForeColor="#CCFF99" />
                        <SelectorStyle BackColor="#99CCCC" ForeColor="#336666" />
                        <TitleStyle BackColor="#003399" BorderColor="#3366CC" BorderWidth="1px" 
                            Font-Bold="True" Font-Size="10pt" ForeColor="#CCCCFF" Height="25px" />
                        <TodayDayStyle BackColor="#99CCCC" ForeColor="White" />
                        <WeekendDayStyle BackColor="#CCCCFF" />
                    </asp:Calendar>
                  
                    </td>
            </tr>
            <tr>
                <td colspan="2" style="text-align: center">
                    <asp:Button ID="Button1" runat="server" Text="Update" CssClass="button" />
                </td>
            </tr>
            <tr>
                <td colspan="2" style="text-align: center">
                    <asp:Label ID="Label1" runat="server" Text=""></asp:Label>
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
