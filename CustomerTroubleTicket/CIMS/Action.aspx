<%@ Page Language="VB" AutoEventWireup="false" CodeFile="Action.aspx.vb" Inherits="CIMS_Action" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 1.0 Transitional/EN">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Action</title>
    <link rel='stylesheet' type='text/css' href="../Main.css"/>
    <link href="../StyleSheet.css" rel="stylesheet" type="text/css" />
    <script language="javascript" type="text/javascript">
        function ShowLog()
        {
            var Div;
            Div=document.getElementById("Log");
            if (Div.style.display == "none")
            {
                Div.style.display="block";
                var ADiv;
                ADiv=document.getElementById("Action");
                if (ADiv.style.display == "block")
                    ADiv.style.display = "none"
                                
            }
            else
            {
                Div.style.display="none";
            }
            return true;
        }
        
        function ShowAction()
        {
            var Div;
            Div=document.getElementById("Action");
            if (Div.style.display == "none")
            {
                Div.style.display="block";
                var ADiv;
                ADiv=document.getElementById("Log");
                if (ADiv.style.display == "block")
                    ADiv.style.display = "none"
                                
            }
            else
            {
                Div.style.display="none";
            }
            return true;
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <table width="100%">
            <tr>
                <td colspan="3" style="background-image:url(../images/voicefiles.jpg); height:30px; text-align: left;">
                    <span style="font-size: 8pt; font-family: Arial"><strong>Trouble Ticket<span
                        style="font-size: 8pt"><span style="font-family: Times New Roman">&nbsp;</span></span></strong>
                    </span>
                </td>
            </tr>
        </table>
        <div>
            <input id="IMG1" runat="server" type="button" value="Action" onclick="javascript:return ShowAction();" class="button"  />
            <input id="IMG2" runat="server" type="button" value="Print" class="button"  />
            <input id="IMG3" runat="Server" type="button" value="Log" onclick="javascript:return ShowLog();" class="button"  />
            <input id="IMG4" runat="server" type="button" value="Return to list" onclick="javascript:window.location.href='CIMS.aspx'" class="button"  />
        </div>
        <table runat="server" width="100%">
            <tr>
                <td>
                    <div id="Action" runat="server" style="display:none">
                        <table id="tblAction" runat="server" width="70%" style="font-family:Arial; font-size:8pt">
                            <tr>
                                <td class="Voice" width="15%">
                                    Status    
                                </td>
                                <td class="Voice1" width="35%">
                                    <asp:DropDownList ID="ddStatus" runat="server" Width="70" Font-Names="Arial" Font-Size="8pt">
                                        <asp:ListItem Value="Open" Text="Open"></asp:ListItem>
                                        <asp:ListItem Value="Closed" Text="Closed"></asp:ListItem>
                                    </asp:DropDownList>    
                                </td>    
                                <td class="Voice" width="15%">
                                    Priority
                                </td>
                                <td class="Voice1" width="35%">
                                    <asp:DropDownList ID="ddPriority" runat="server" Width="70" Font-Names="Arial" Font-Size="8pt">
                                        <asp:ListItem Value="Low" Text="Low"></asp:ListItem>
                                        <asp:ListItem Value="Normal" Text="Normal"></asp:ListItem>
                                        <asp:ListItem Value="High" Text="High"></asp:ListItem>
                                    </asp:DropDownList>    
                                </td>                                
                            </tr>
                            <tr>
                                <td class="Voice">
                                    Subject
                                </td>
                                <td class="Voice1" colspan=3>
                                    <asp:TextBox ID="txtSubject" runat="server" Width="300" Font-Names="Arial" Font-Size="8pt"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="Voice" valign="top">
                                    Action
                                </td>
                                <td class="Voice1" colspan=3>
                                    <textarea id="txtMessage" runat="server" cols="80" rows="5" style="font-family:Arial; font-size:8pt"></textarea>
                                </td>
                            </tr>
                            <tr>
                                <td class="Voice1" colspan=4 align="center">
                                    <center><asp:Button ID="Button2" runat="server"  CssClass="button" Text="Submit"   /></center>    
                                </td>
                            </tr>
                        </table>    
                    </div>
                </td>
            </tr>
            <tr>
                <td>
                    <div id="Log" runat="server" style="display:none">                        
                        <table id="tblLog" runat="server" width="70%" style="font-family:Arial; font-size:8pt">
                            <tr style="font-family:Arial; font-size:8pt; font-weight:bold ">
                                <td align="center" class="Voice1" width="30%">
                                    <center>Date</center>                                        
                                </td>
                                <td align="center" class="Voice1" width="70%">
                                    <center>Action</center>                                        
                                </td>    
                            </tr>
                        </table>    
                    </div>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Table ID="tblViewTicketHistory" runat="server" Width=80% BorderStyle=solid BorderWidth=1 Font-Names="Arial" Font-Size="8pt" GridLines="Both">
                        <asp:TableRow CssClass="Voice5" Font-Names="Arial" Font-Size="8pt" Font-Bold="true">
                            <asp:TableCell Width=20%>
                                From
                            </asp:TableCell>
                            <asp:TableCell>
                                Message
                            </asp:TableCell>
                        </asp:TableRow>
                    </asp:Table>                    
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
