<%@ Page Language="VB" AutoEventWireup="false" CodeFile="EditCategory.aspx.vb" Inherits="Department_Default" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 1.0 Transitional/EN">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <link href= "../App_Themes/Css/Main.css" type="text/css" rel="stylesheet" />
    <link href= "../App_Themes/Css/Common.css" type="text/css" rel="stylesheet" />
    <link href= "../App_Themes/Css/Styles.css" type="text/css" rel="stylesheet" />
    <title>Edit Category</title>
</head>
<body>
    <form id="form1" runat="server">
        <div id="body">
        <div id="cap"></div>
        <div id="main">
        <h1>Edit Account Category</h1>
            <asp:Panel ID="Panel1" runat="server" HorizontalAlign="Left">
                <table width="50%">
            <tr>
                <td colspan="2" class="HeaderDiv" style="text-align: center">
                   Category Details</td>
            </tr>
            
            <tr>
                <td style="width: 25%; text-align: right; text-align: right;">
                    Category Name</td>
                <td style="width: 25%; text-align: left;">
                    <asp:DropDownList ID="DLCate" runat="server" Width="248px" AutoPostBack="True" >
                    
                    </asp:DropDownList></td>
                    </tr>
                    </table><br />
                    <asp:Panel ID="PnlDesc" runat="server" Height="50px" Width="125px">
        
                    <table  width="60%">
                        <tr>
                            <td class="alt1" colspan="2">
                                Category Data
                            </td>
                        </tr>
                         <tr>
                <td style="width: 25%; text-align: right; text-align: right;">
                    Description</td>
                <td style="width: 25%; text-align: left;">
                    <asp:TextBox ID="txtCategory" runat="server" Width="248px"></asp:TextBox> 
                   </td>
            </tr>
                    <tr>
                <td style="width: 25%; text-align: right; text-align: right;">
                    Priority</td>
                <td style="width: 25%; text-align: left;">
                    <asp:TextBox ID="txtPriority" runat="server"  Width="248px"></asp:TextBox> 
                   </td>
            </tr>
                  <tr>
                <td style="width: 25%; text-align: right; text-align: right;">
                    Status</td>
                <td style="width: 25%; text-align: left;">
                    
                    <asp:DropDownList ID="DLStatus" runat="server"  Width="248px">
                    <asp:ListItem Text="Active" value="False"></asp:ListItem>  
                    <asp:ListItem Text="Inactive" value="True"></asp:ListItem>  
                     </asp:DropDownList></td>
            </tr>
            <tr>
                <td style="text-align: center; height: 26px;" colspan="4">
                    <asp:Button ID="Button1" CssClass="button"  runat="server"  Text="Submit" />
                    
                    
                </td>
            </tr>
        </table>
        </asp:Panel><br />
                     <asp:Label ID="MsgDisp" runat="server" CssClass="Title"
            ForeColor="#C00000" Height="16px" Width="496px"></asp:Label>&nbsp;

            </asp:Panel>
       
        </div> 
        </div> 
        <br />
        <br />
        <br />
        <br />
    </form>
    
</body>
</html>
