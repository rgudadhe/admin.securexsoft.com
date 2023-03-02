<%@ Page Language="VB" AutoEventWireup="false" CodeFile="ViewMacroAssignments.aspx.vb" Inherits="MacroAssignments" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Register Assembly="KMobile.Web" Namespace="KMobile.Web.UI.WebControls" TagPrefix="asp" %>
<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Document Model Assignments </title>
    <link href= "../App_Themes/Css/Default.css" type="text/css" rel="stylesheet" />
    <link href= "../App_Themes/Css/Common.css" type="text/css" rel="stylesheet" />
    <link href= "../App_Themes/Css/Styles.css" type="text/css" rel="stylesheet" />

</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        <div id="body">
        <div id="cap"></div>
        <div id="main">
        <h1>Macro Assignments </h1>
        <div style="text-align:left">
            <table style="text-align:left " border="0">
        <tr>
            <td style="border:0">
                <div>
                    
                            <table class="common" border="0">
                                <tr>
                                    <td colspan="1" style="border:0">
                                        <asp:DropDownList ID="DDLAcc" Width="300" runat="server" CssClass="common" AutoPostBack="true">
                                        </asp:DropDownList>
                                    </td>
                                   
                                    
                                </tr>
                                </table>
                                <table class="common" border="0">
                               
                                <tr>
                                    <td style="border:0">
                                       <div style="text-align:left;">
                                            <asp:Label ID="lblMsg" runat="server" Text="" CssClass="common" ForeColor="Firebrick"></asp:Label>
                                       </div> 
                                    </td>
                                </tr>
                            </table>
                     
                </div>
            </td>
        </tr>
        <tr>
            <td style="border:0">
                <div style="text-align:center">
                    <asp:Button ID="btmSubmit" runat="server" Text="Submit" CssClass="button" />
                </div>
            </td>
        </tr>
    </table>
    <div>
    <asp:CompleteGridView ID="MyDataGrid" runat="server" AutoGenerateColumns="True" 
                     cellspacing="2" CellPadding="2" 
                     
                     EnableViewState="False"   
                      CountFormat="Displaying records <b>{0}</b> to <b>{1}</b> of <b>{2}</b>" CaptionAlign="Bottom"   ShowCount="False"  >
                <AlternatingRowStyle cssclass="gridalt2"  ></AlternatingRowStyle>
                <RowStyle cssclass="gridalt1"  ></RowStyle>
                </asp:CompleteGridView></div>
        </div>
    </div> 
    </div> 
    </form>
</body>
</html>
