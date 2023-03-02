<%@ Page Language="VB" AutoEventWireup="false" CodeFile="AttendanceApprove.aspx.vb" Inherits="AttendanceApprove" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 1.0 Transitional/EN">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Untitled Page</title>
    <LINK href= "styles\Default.css" type="text/css" rel="stylesheet">
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:Table ID="Table3" runat="server" Height="1px" HorizontalAlign="Center" Width="734px">
            <asp:TableRow ID="TableRow1" runat="server">
                <asp:TableCell ID="TableCell1" runat="server" HorizontalAlign="Right">
                    <asp:ImageButton ID="ImgBtn" runat="server" ImageUrl="~/Images/logout.gif" />
                </asp:TableCell>
            </asp:TableRow>
        </asp:Table>
            <asp:Repeater ID="AttendanceApproval" runat="server" >
            <HeaderTemplate>
                
                <Table border=1 align="center" style="color:DarkBlue"  >

                        <tr class="HeaderDiv">
                            <th colspan=7>Employee Attendance Request</th>
                        </tr>
                        <tr class="SMSelected">
                            <th align=center>Name</th>
                            <th align=center>Attendance Date</th>
                            <th align=center>SignIn</th>
                            <th align=center>SignOut</th>
                            <th align=center>Reason</th>
                            <th align=center>Applied On</th>
                            <th align=center>Action</th>
                        </tr>

              </HeaderTemplate>            
            <ItemTemplate>
                <tr>
                    <td>
                        <asp:Label ID="EName" runat="server" Font-Names="Trebuchet MS" Font-Size=Smaller Text='<%#DataBinder.Eval(Container, "DataItem.FirstName") &" "& DataBinder.Eval(Container,"DataItem.LastName") %>'></asp:Label>
                    </td>

                    <td>
                        <asp:Label ID="txtAttDate" Font-Names="Trebuchet MS" Font-Size=Smaller runat="server" Text='<%#DataBinder.Eval(Container, "DataItem.AttDate").ToShortDateString() %>'></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="txtSignIn" Font-Names="Trebuchet MS" Font-Size=Smaller runat="server" Text='<%#DataBinder.Eval(Container, "DataItem.SignIn")%>'></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="txtSignOut" Font-Names="Trebuchet MS" Font-Size=Smaller runat="server" Text='<%#DataBinder.Eval(Container, "DataItem.SignOut") %>'></asp:Label>
                    </td>
                    <td>                                                 
                        <asp:Label ID="txtReason" Font-Names="Trebuchet MS" Font-Size=Smaller runat="server" Text='<%#DataBinder.Eval(Container, "DataItem.Reason") %>'></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="txtAppDate" Font-Names="Trebuchet MS" Font-Size=Smaller runat="server" Text='<%#DataBinder.Eval(Container, "DataItem.AppDate").ToShortDateString() %>'></asp:Label>
                    </td>                    
                    <td>
                        <FONT FACE="Trebuchet MS" SIZE="2">                         
                        <a href='AttendanceAction.aspx?Str=Approve&AttReqID=<%# DataBinder.Eval(Container.DataItem, "AttReqID" )%>'>Approve</a> /
                        <a href='AttendanceAction.aspx?Str=DisApprove&AttReqID=<%# DataBinder.Eval(Container.DataItem, "AttReqID" )%>'>Disapprove</a> 
                       </FONT>
                    </td>
                </tr>                
            </ItemTemplate>
            <FooterTemplate>
                </table>
            </FooterTemplate>
        </asp:Repeater>
        <br />
    
    </div>
    </form>
</body>
</html>
