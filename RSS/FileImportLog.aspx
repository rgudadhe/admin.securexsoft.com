<%@ Page Language="VB" AutoEventWireup="false" CodeFile="FileImportLog.aspx.vb" Inherits="ets.FileImportLog" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 1.0 Transitional/EN">
<LINK href= "../styles/Default.css" type="text/css" rel="stylesheet">
<html xmlns="http://www.w3.org/1999/xhtml" >

<head runat="server">
    <title>Untitled Page</title>    

</head>
<body>
    <form id="form1" runat="server" method="post" target="LogResult">
    <asp:Panel ID="Panel2" runat="server" Height="15%" Width="100%">
        <table style="left: 2%; position: static; top: 4%; width: 550px;">
            <tr>
                <td class="HeaderDiv" style="width: 305px;" >
                    Customer Job# </td>
                <td class="HeaderDiv" style="width: 192px;" >
                    MD5 Value </td>    
                <td class="HeaderDiv" style="width: 103px" >
                    Status </td>    
             </TR>
              <tr>
                <td style="width: 305px">
                    <asp:TextBox ID="txtCJNum" runat="server"></asp:TextBox></td>
                <td style="width: 192px">
                    <asp:TextBox ID="txtMD5" runat="server"></asp:TextBox></td>
                <td style="width: 103px">
                    <asp:DropDownList ID="ddlStatus" runat="server">
                        <asp:ListItem Selected="True" Value="" >Any</asp:ListItem>
                        <asp:ListItem Value="1">Imported</asp:ListItem>
                        <asp:ListItem Value="0">Not Imported</asp:ListItem>
                        <asp:ListItem Value="2">Duplicate</asp:ListItem>
                    </asp:DropDownList></td>
                </TR>
             <tr>
                 <td class="HeaderDiv" style="width: 305px" >
                    Start Date </td>           
                 <td class="HeaderDiv" width=50%>
                    End Date</td>   
                 <td class="HeaderDiv" width=50%>
                    Client</td>      
            </tr>
           
                <tr>                    
                 <td style="width: 305px; height: 45px;">
                     <asp:TextBox ID="sDate" runat="server"></asp:TextBox>
                    <asp:RegularExpressionValidator  Display="None" ID="RegularExpressionValidator1" runat="server" ErrorMessage="Invalid Start Date (MM/DD/YYYY)" ControlToValidate="sDate" ValidationExpression="\d{2}/\d{2}/\d{4}">*</asp:RegularExpressionValidator></td>           
                 <td style="width: 192px; height: 45px">
                     &nbsp;<asp:TextBox ID="eDate" runat="server"></asp:TextBox>
                     <br />
                    <asp:RegularExpressionValidator  Display="None" ID="RegularExpressionValidator2" runat="server" ControlToValidate="eDate"
                        ErrorMessage="Invalid End Date (MM/DD/YYYY)" ValidationExpression="\d{2}/\d{2}/\d{4}">*</asp:RegularExpressionValidator>&nbsp;
                 </td>   
                 <td style="width: 103px; height: 45px">
                     <asp:TextBox ID="txtClient" runat="server"></asp:TextBox>&nbsp;<br />
                    <asp:CompareValidator ID="CompareValidator1" runat="server" ErrorMessage="End date can not be less than Start Date" ControlToCompare="sDate" ControlToValidate="eDate" Operator="GreaterThanEqual" Type="Date">*</asp:CompareValidator></td>   
            </tr>
            <tr>
                <td colspan="5" style="width: 26161px; height: 21px">
                    &nbsp;&nbsp;
                </td>
            </tr>
            <tr>
                <td colspan="5" style="width: 26161px; height: 26px">
                    <asp:Button ID="btnSearch" runat="server" Text="Search" />
                    <asp:ValidationSummary ID="ValidationSummary1" runat="server" EnableClientScript="true" DisplayMode="BulletList"/>
                </td>
            </tr>
            
            <tr>
                <td colspan="5" style="width: 26161px">
                    <asp:Literal ID="Literal1" runat="server"></asp:Literal>
                </td>
            </tr>
        </table>    
        </asp:Panel> 
        <iframe style="width: 855px; height: 338px;" frameborder="0" name="LogResult" src="FileImportLogResult.aspx"></iframe>
        </form>       
                
</body>
</html>
