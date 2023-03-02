<%@ Page Language="VB" AutoEventWireup="false" CodeFile="FldWT_PhySearch.aspx.vb" Inherits="ets.Templates_TA_PhySearch" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 1.0 Transitional/EN">

<html xmlns="http://www.w3.org/1999/xhtml" >

<head runat="server">
    <title>DVR Folder Settings</title>
    <link href= "../App_Themes/Css/Main.css" type="text/css" rel="stylesheet" />    
    <link href= "../App_Themes/Css/Styles.css" type="text/css" rel="stylesheet" />    
</head>
<body>
    <form id="form1" runat="server">
    <div id="body">
    <div id="cap"></div>
    <div id="main">
    <h1>DVR Folder Setting</h1>
    <div>
        <asp:Panel ID="Panel1" HorizontalAlign="Left" runat="server">
            <table style="width:90%;">
                <tr>
                    <td class="HeaderDiv" colspan="3" style="text-align:center;">
                        Physician Search</td>
                </tr>
                <tr>
                    <td class="alt" style="text-align:center;">
                        First
                    </td>
                    <td class="alt"  style="text-align:center;">
                        Last
                    </td>
                    <td class="alt"  style="text-align:center;">
                        PIN
                    </td>
                </tr>
                <tr>
                    <td style="text-align:center;">
                        <asp:TextBox ID="txtPhyF" runat="server" Width="200px"></asp:TextBox>
                    </td>
                    <td style="text-align:center;">
                        <asp:TextBox ID="txtPhyL" runat="server" Width="200px"></asp:TextBox>
                    </td>
                    <td style="text-align:center;">
                        <asp:TextBox ID="txtPin" runat="server" Width="200px"></asp:TextBox>
                    </td>
                </tr>
                
                
                <tr>
                    <td colspan="3" style="text-align:center" >
                        <asp:Button ID="btnSearchPhy" CssClass="button"  runat="server" Text="Search Physician" /></td>
                </tr>
                         
                
                <tr>
                    <td colspan="3" >
                        <asp:Literal ID="Literal1" runat="server"></asp:Literal>
                    </td>
                </tr>
            </table>
        </asp:Panel>
        <asp:Panel ID="Panel2" HorizontalAlign="Left" runat="server">
            <asp:Repeater ID="rptPhy" runat="server">
                 <HeaderTemplate>
                    <table>
                    <tr bgcolor="#3399cc">            
                        <td class="alt1" align="center">Physician Name</td>            
                        <td class="alt1" align="center">Action</td>
                    </tr>
                 </HeaderTemplate>
                 <ItemTemplate>
                    <tr>            
                        <td width=60%><asp:Label ID="txtName" runat="server" Text='<%#Container.DataItem("PhyName") & "(" & Container.DataItem("AccountName") & ")"%>' ></asp:label>
                        <asp:HiddenField ID="PhyID" runat="server" Value='<%#Container.DataItem("PhysicianID")%>'/></td>            
                        <td align="center"><asp:Button ID="btnEdit" CssClass="button" runat="server" Text="View Assignments" OnClick="btnEdit_Click" Width="100%"/></td>
                    </tr>
                 </ItemTemplate>
                 <AlternatingItemTemplate>
                    <tr bgcolor="#cccccc">
                        <td width=60%><asp:Label ID="txtName" runat="server" Text='<%#Container.DataItem("PhyName") & "(" & Container.DataItem("PhyName") & ")"%>' ></asp:label>
                        <asp:HiddenField ID="PhyID" runat="server" Value='<%#Container.DataItem("PhysicianID")%>'/></td>            
                        <td align="center"><asp:Button ID="btnEdit" CssClass="button"  runat="server" Text="View Assignments" OnClick="btnEdit_Click" Width="100%"/></td>
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
    id="RegtxtPhyF"  
    runat="server" 
    ControlToValidate="txtPhyF" 
    ValidationExpression="^[a-zA-Z%]+$"
    ErrorMessage="First Name - Please enter valid input."
    >
</asp:RegularExpressionValidator>
<br />
                <asp:RegularExpressionValidator  Display="None"
    id="RegtxtPhyL"  
    runat="server" 
    ControlToValidate="txtPhyL" 
    ValidationExpression="^[a-zA-Z%]+$"
    ErrorMessage="Last Name - Please enter valid input."
    >
</asp:RegularExpressionValidator>
<br />
                <asp:RegularExpressionValidator  Display="None"
    id="RegtxtPint"  
    runat="server" 
    ControlToValidate="txtPin" 
    ValidationExpression="^[0-9%]+$"
    ErrorMessage="Pin - Please enter valid input."
    />
   </div> 
    </div> 
    
    <asp:ValidationSummary ID="ValidationSummary1" runat="server" HeaderText="Following error occurs:" ShowMessageBox="true" DisplayMode="BulletList" ShowSummary="False" /> </form>
</body>
</html>
