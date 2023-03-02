<%@ Page Language="VB" AutoEventWireup="false" CodeFile="AddNewVAS.aspx.vb" Inherits="Billing_LCMethods_AddNewVAS" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Register Assembly="KMobile.Web" Namespace="KMobile.Web.UI.WebControls" TagPrefix="asp" %>

<%@ Register
    Assembly="AjaxControlToolkit"
    Namespace="AjaxControlToolkit"
    TagPrefix="ajaxToolkit" %>
    
<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
     <link rel="Stylesheet" type="text/css" href="../../App_Themes/Css/styles.css"/>
    <link rel="Stylesheet" type="text/css" href="../../App_Themes/Css/Common.css"/>
    <title>New VAS</title>
</head>
<body>
    <form id="form1" runat="server">
        <div id="body">
        <div id="cap"></div>
        <div id="main">
        <h1>New VAS</h1>
        <asp:Panel ID="Panel1" runat="server" HorizontalAlign="Left">
            <div>
    
              <table width="300">
            
     
          
            <tr>
                <td class="alt5">
                    Item *
                </td>
                <td class="alt5">
                    <asp:TextBox ID="txtItem" runat="server" CssClass="common" Width="294px"></asp:TextBox>&nbsp;
                         
                            </td>
            </tr>
                  <tr>
                      <td>
                          Description</td>
                      <td>
                          <asp:TextBox ID="txtDesc" runat="server" Width="294px"></asp:TextBox></td>
                  </tr>
            
             <tr>
                <td>
                    Rate *</td>
                <td>
                    <asp:TextBox ID="txtRate" runat="server" CssClass="common" Width="295px"></asp:TextBox>&nbsp;
                      
                            </td>
            </tr>
             <tr>
                <td style="text-align: center; height: 23px;" colspan="2" class="alt1">
                  <asp:Button ID="Button2" runat="server"  CssClass="button" Text="Submit"   />
                </td>
            </tr>
        </table>
    </div>
        </asp:Panel>
        </div> 
        </div> 
    </form>
</body>
</html>
