<%@ Page Language="VB" AutoEventWireup="false" CodeFile="ImportLogo.aspx.vb" Inherits="Login_CreateUser" %>

<%@ Register Assembly="System.Web.Extensions, Version=1.0.61025.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI" TagPrefix="asp" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<%@ Register
    Assembly="AjaxControlToolkit"
    Namespace="AjaxControlToolkit"
    TagPrefix="ajaxToolkit" %>
    
<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>Import Constractor</title>
    <link href= "../App_Themes/Css/styles.css" type="text/css" rel="stylesheet" />
    <link href= "../App_Themes/Css/Common.css" type="text/css" rel="stylesheet" />

</head>
<body>
    <form id="form1" runat="server">
        <div id="body">
        <div id="cap"></div>
        <div id="main">
        <h1>Import Contractor Logo</h1>
        <asp:Panel ID="Panel1" runat="server" HorizontalAlign="Left">
            <div>
             <table  id="Table2" width="300">
        <tr>
                <td   colspan ="2" class="alt1">
                    Click on Browse button
                        and select the file</td>
               </tr>
                <tr>
                <td>
                    
                        Contractor
                           
            </td>
             <td  >
                    
                        <asp:DropDownList ID="DLContractor" runat="server" CssClass="common" >
                     </asp:DropDownList>
                           
            </td>
            </tr> 
            <tr class="tblbg2">
                <td  colspan ="2">
                    
                    <b>Image 85x64</b>  <asp:FileUpload ID="FileUpload1" Width="300"  runat="server" CssClass="common"  />
        
                    
            </td>
            </tr>
            <tr class="tblbg2">
                <td  colspan ="2">
                    
                    <b>Image 184x118</b>     <asp:FileUpload ID="FileUpload2" Width="300"  runat="server" CssClass="common"  />
        
                    
            </td>
            </tr>
        
        <tr class="tblbg">
                <td  style="text-align: center" colspan="2" >
                    &nbsp;<asp:Button ID="Button1" runat="server" Text="Submit" CssClass="button" /></td>
            </tr>
            <tr>
                <td colspan="2" style="text-align: left">
                <p>
        <asp:Label ID="Label2" runat="server" CssClass="common" ></asp:Label></p>
                   
                     <p>
        <asp:Label ID="Label3" runat="server" CssClass="common"></asp:Label></p>
                    </td> 
            </tr>
           
        </table>
        
        
        
    </div>
    <div style="text-align:left">
        <asp:RequiredFieldValidator  Display="None" ID="RequiredFieldValidator1" runat="server" ErrorMessage="Please upload logo imag in 85*64 format" SetFocusOnError="true" ControlToValidate="FileUpload1"></asp:RequiredFieldValidator><br />
        <asp:RequiredFieldValidator  Display="None" ID="RequiredFieldValidator2" runat="server" ErrorMessage="Please upload logo imag in 184x118 format" SetFocusOnError="true" ControlToValidate="FileUpload2"></asp:RequiredFieldValidator><br />
        <asp:Label ID="Label1" runat="server" CssClass="Title" ForeColor="Red" ></asp:Label>
    </div>
        </asp:Panel>        
    </div> 
    </div> 
    <asp:ValidationSummary ID="ValidationSummary1" runat="server" HeaderText="Following error occurs:" ShowMessageBox="true" DisplayMode="BulletList" ShowSummary="False" /> </form>
</body>
</html>
