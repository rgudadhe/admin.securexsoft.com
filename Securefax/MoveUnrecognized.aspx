<%@ Page Language="VB" AutoEventWireup="false" CodeFile="MoveUnrecognized.aspx.vb" Inherits="Services_MoveUnrecognized" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 1.0 Transitional/EN">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Untitled Page</title>
    <link href= "../styles/Default.css" type="text/css" rel="stylesheet"/>    
    <script language="javascript" type="text/javascript">
        function Chk()
        {
            if (document.getElementById('ddlAcc').value=='')
            {
                alert('Please select account')    
                document.getElementById('ddlAcc').focus();
                return false;
            }
            return true;
        }
        function chkALL(str)
        {
            for(i = 0; i < form1.elements.length; i++)
            {
                elm = document.forms[0].elements[i]
                if (elm.type == 'checkbox')
                {
                    elm.checked = str.checked;
                }
            }
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
    <div>
        <table border=1 style="font-family:Trebuchet MS; font-size:12px ">
            <tr class="SMSelected">
                <td align="center">
                    Sender Fax No
                </td>
                <td colspan=2 align="center">
                    Recieved Date
                </td>
                <td></td>
            </tr>
            <tr>
                <td>
                    <asp:TextBox ID="txtFaxNo" runat="server" Height="15px" Font-Names="Trebuchet MS" Font-Size="12px"></asp:TextBox>
                </td>
                <td>
                    <asp:TextBox ID="txtStartDate" runat="server" Width="65px" Font-Names="Trebuchet MS" Font-Size=Small Height="15px"></asp:TextBox><asp:ImageButton ID="ImgBntsDate" runat="server" CausesValidation="False" ImageUrl="~/images/Calendar_scheduleHS.png" />
                </td>                        
                <td>
                    <asp:TextBox ID="txtEndDate" runat="server" Width="65px" Font-Names="Trebuchet MS" Font-Size=Small Height="15px"></asp:TextBox><asp:ImageButton ID="ImgBnteDate" runat="server" CausesValidation="False" ImageUrl="~/images/Calendar_scheduleHS.png" />
                </td>
                <td>
                    <asp:Button ID="btnSubmit"  runat="server" Text="Submit" Font-Names="Trebuchet MS" Font-Size="12px" />
                </td>
            </tr>
            <ajaxtoolkit:calendarextender id="CalendarExtender1" runat="server" popupbuttonid="ImgBntsDate"
                targetcontrolid="txtStartDate"></ajaxtoolkit:calendarextender>
            <ajaxtoolkit:calendarextender id="CalendarExtender2" runat="server" popupbuttonid="ImgBnteDate"
                targetcontrolid="txtEndDate"></ajaxtoolkit:calendarextender>                    
        </table><BR><BR>
        <asp:Table ID="tblRes" runat="server" Width="75%" GridLines="Both" Font-Names="Trebuchet MS" Font-Size="12px">
        </asp:Table><BR>
        <asp:Table ID="tblAcc" runat="server" Width="75%" Font-Names="Trebuchet Ms" Font-Size="12px">
            <asp:TableRow>
                <asp:TableCell HorizontalAlign="Center" >
                    <%--<asp:DropDownList ID="ddlAcc" runat="server" Font-Names="Trebuchet MS" Font-Size="12px" Width="50%">
                    </asp:DropDownList>--%>&nbsp
                    <asp:Button ID="btnMove" runat="server" Text="Move Files" Font-Names="Trebuchet MS" Font-Size="12px" />
                </asp:TableCell>
            </asp:TableRow>
        </asp:Table>
    </div>
    </form>
</body>
</html>
