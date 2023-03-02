<%@ Page Language="VB" AutoEventWireup="false" CodeFile="DictationStatusReport.aspx.vb" Inherits="Dictation_Code_DictationStatusReport" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Dictation Status Report</title>
    <link href= "../App_Themes/Css/Common.css" type="text/css" rel="stylesheet"/>
    <link href= "../App_Themes/Css/Styles.css" type="text/css" rel="stylesheet"/>

    <script language="javascript" type="text/javascript">
        function ChkDays()
        {
            if (document.getElementById('ddlDays').value=='')
            {
                alert('Please select days')
                return false;
            }
            return true;
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div id="body">
        <div id="cap"></div>
        <div id="main">
        <h1>Inactive Dictators</h1>
    <div>
        <asp:Panel ID="Panel2" runat="server" HorizontalAlign="Left">
            <table>
            <tr>
                <td class="alt1">
                    No. of Days
                </td>
                <td class="alt1"> 
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td>
                    <asp:DropDownList ID="ddlDays" runat="server" CssClass="common"  Width="150px">
                        <asp:ListItem Text="Please Select" Value=""></asp:ListItem>
                        <asp:ListItem Text="7" Value="7"></asp:ListItem>
                        <asp:ListItem Text="15" Value="15"></asp:ListItem>
                        <asp:ListItem Text="30" Value="30"></asp:ListItem>
                        <asp:ListItem Text="60" Value="60"></asp:ListItem>
                        <asp:ListItem Text="90" Value="90"></asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td>
                    <asp:Button ID="btnSubmit" CssClass="button" runat="server" Text="Submit" Font-Names="Trebuchet MS" Font-Size="12px" OnClientClick="javascript:return ChkDays();" />
                </td>
            </tr>
        </table>
        </asp:Panel>
        
        <br /><br />
        <asp:Panel ID="Panel1" HorizontalAlign="Left" runat="server" Visible="false">
            <asp:LinkButton ID="LnkExport" runat="server">Export Results</asp:LinkButton><br />
            <asp:Repeater ID="rptDetails" runat="server">
                <HeaderTemplate>
                    <table border="1">
                        <tr>
                            <td class="alt1">Account Name</td>            
                            <td class="alt1">Account Number</td>  
                            <td class="alt1">Physician Name</td>  
                            <td class="alt1">Dictation Code</td>  
                            <td class="alt1">Pin No</td>
                            <td class="alt1">Last Dictation Date</td>  
                        </tr>
                </HeaderTemplate>

                <ItemTemplate>
                    <tr class="common">
                        <td><%#Container.DataItem("AccountName")%></td>  
                        <td><%#Container.DataItem("AccountNo")%></td>  
                        <td><%#Container.DataItem("PhyName")%></td> 
                        <td><%#Container.DataItem("DictationCode")%></td>
                        <td><%#Container.DataItem("PINNo")%></td> 
                        <td><%#Container.DataItem("LastDictationDate")%></td> 
                    </tr>
                </ItemTemplate>
                <AlternatingItemTemplate>
                    <tr bgcolor="#cccccc" class="common" >
                        <td><%#Container.DataItem("AccountName")%></td>  
                        <td><%#Container.DataItem("AccountNo")%></td>  
                        <td><%#Container.DataItem("PhyName")%></td> 
                        <td><%#Container.DataItem("DictationCode")%></td>
                        <td><%#Container.DataItem("PINNo")%></td>
                        <td><%#Container.DataItem("LastDictationDate")%></td>  
                    </tr>
                </AlternatingItemTemplate>
                <FooterTemplate>
                    </table>
                </FooterTemplate>
            </asp:Repeater>
        </asp:Panel>
    </div>
    </div>
    </div> 
    </form>
</body>
</html>
