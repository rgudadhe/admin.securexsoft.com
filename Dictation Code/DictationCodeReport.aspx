<%@ Page Language="VB" AutoEventWireup="false" CodeFile="DictationCodeReport.aspx.vb" Inherits="Dictation_Code_DictationCodeReport" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 1.0 Transitional/EN">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Dictattion Code Report</title>
    <link href= "../App_Themes/Css/Main.css" type="text/css" rel="stylesheet"/>
    <link href= "../App_Themes/Css/Common.css" type="text/css" rel="stylesheet"/>
    <link href= "../App_Themes/Css/Styles.css" type="text/css" rel="stylesheet"/>
</head>
<body>
    <form id="form1" runat="server">
    <div id="body">
    <div id="cap"></div>
    <div id="main">
    <h1>Dictation Code Report</h1>
    <div>
        <table  style="width: 100%; text-align: left;" >
        <tr>
            <td style="width: 100%; text-align: center;" valign="top" colspan ="4" class="HeaderDiv">
                    Search Physician</td>
               </tr>
            <tr>
                <td class="alt" style="text-align: center;">
                    Account Name
                </td>
                <td class="alt" style="text-align: center;">
                    First Name
                </td >
                <td class="alt" style="text-align: center;">
                    Last Name
                </td>
                <td class="alt" style="text-align: center;">
                    Dictation Code
                </td>
            </tr>
            <tr>
                <td style="text-align:center">
                    <asp:TextBox ID="txtAccName" runat="server" Width="200px"  Height="15px"></asp:TextBox>
                </td>
                <td style="text-align:center">
                    <asp:TextBox ID="txtFirst" runat="server" Width="100px"  Height="15px"></asp:TextBox>
                </td>
                <td style="text-align:center">
                    <asp:TextBox ID="txtLast" runat="server" Width="100px"  Height="15px"></asp:TextBox>
                </td>
                <td style="text-align:center">
                    <asp:TextBox ID="txtDictCode" runat="server" Width="100px"  Height="15px"></asp:TextBox>
                </td>
            </tr>
        
            <tr>
                <td colspan="4" style="text-align: center;">
                    <asp:Button ID="btnSearch" runat="server" CssClass="button"  Text="Search"  />
                </td>
            </tr>
        </table>
        <asp:Panel ID="Panel2" HorizontalAlign="Left" runat="server" Visible="false">
            <asp:Label ID="lblMsg" runat="server" Text="" CssClass="Title" ForeColor="#C00000"></asp:Label>
        </asp:Panel> 
        <br/><br/>
        <asp:Panel ID="Panel1" HorizontalAlign="Left" runat="server" Visible="false">
            <asp:LinkButton ID="LnkExport" runat="server" CssClass="common">Export Results</asp:LinkButton><br/>
            <asp:Repeater ID="rptDetails" runat="server">
                <HeaderTemplate>
                    <table>
                        <TR>
                            <TD class="alt1" style="text-align: center;">Account Name</TD>            
                            <TD class="alt1" style="text-align: center;">Account Number</TD>  
                            <TD class="alt1" style="text-align: center;"> Physician Name</TD>  
                            <TD class="alt1" style="text-align: center;">Dictation Code</TD>  
                            <TD class="alt1" style="text-align: center;">Pin No</TD>  
                        </TR>
                </HeaderTemplate>

                <ItemTemplate>
                    <tr class="common">
                        <td><%#Container.DataItem("AccountName")%></td>  
                        <td><%#Container.DataItem("AccountNo")%></td>  
                        <td><%#Container.DataItem("FirstName") & " " & Container.DataItem("LastName")%></td> 
                        <td><%#Container.DataItem("DictationCode")%></td>
                        <td><%#Container.DataItem("PINNo")%></td> 
                    </tr>
                </ItemTemplate>
                <AlternatingItemTemplate>
                    <tr class="common">
                        <td><%#Container.DataItem("AccountName")%></td>  
                        <td><%#Container.DataItem("AccountNo")%></td>  
                         <td><%#Container.DataItem("FirstName") & " " & Container.DataItem("LastName")%></td> 
                        <td><%#Container.DataItem("DictationCode")%></td>
                        <td><%#Container.DataItem("PINNo")%></td> 
                    </tr>
                </AlternatingItemTemplate>
                <FooterTemplate>
                    </table>
                </FooterTemplate>
            </asp:Repeater>
        </asp:Panel>
    </div>
    </div>
     <div style="text-align:left">        <asp:RegularExpressionValidator  Display="None"
    id="RegtxtAccName"  
    runat="server" 
    ControlToValidate="txtAccName" 
    ValidationExpression="^[0-9a-zA-Z -%]+$"
    ErrorMessage="Account Name - Please enter valid input."
    >
</asp:RegularExpressionValidator>
<br />
                <asp:RegularExpressionValidator  Display="None"
    id="RegtxtFirst"  
    runat="server" 
    ControlToValidate="txtFirst" 
    ValidationExpression="^[a-zA-Z%]+$"
    ErrorMessage="First Name - Please enter valid input."
    >
</asp:RegularExpressionValidator>
<br />
                <asp:RegularExpressionValidator  Display="None"
    id="RegtxtLast"  
    runat="server" 
    ControlToValidate="txtLast" 
    ValidationExpression="^[a-zA-Z%]+$"
    ErrorMessage="Last Name - Please enter valid input."
    /><br />
               <asp:RegularExpressionValidator  Display="None"
    id="RegtxtDictCode"  
    runat="server" 
    ControlToValidate="txtDictCode" 
    ValidationExpression="^[0-9a-zA-Z%]+$"
    ErrorMessage="Dictation code - Please enter valid input."
   />
   </div> 
    </div>
    <asp:ValidationSummary ID="ValidationSummary1" runat="server" HeaderText="Following error occurs:" ShowMessageBox="true" DisplayMode="BulletList" ShowSummary="False" /> </form>
</body>
</html>
