<%@ Page Language="VB" AutoEventWireup="false" CodeFile="PrevJobsSearch.aspx.vb" Inherits="PrevJobsSearch" %>
<%@ Register TagPrefix="DBWC" Namespace="DBauer.Web.UI.WebControls" Assembly="DBauer.Web.UI.WebControls.HierarGrid" %>
<%@ Register
    Assembly="AjaxControlToolkit"
    Namespace="AjaxControlToolkit"
    TagPrefix="ajaxToolkit" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 1.0 Transitional/EN">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Previous Job Search</title>
    <link href= "../App_Themes/Css/Default.css" type="text/css" rel="stylesheet"/>
    <link href= "../App_Themes/Css/Styles.css" type="text/css" rel="stylesheet"/>
</head>
<body>
    <form id="Form1" method="post" target="FaxPlusResult" runat="server" >
        <div id="body">
        <div id="cap"></div>
        <div id="main">
        <h1>Search Prevoios Reports</h1>

    <div>
        <ajaxtoolkit:toolkitscriptmanager id="ScriptManager1" runat="server"> </ajaxtoolkit:toolkitscriptmanager>
        <%--<asp:UpdatePanel ID="up2" runat="server">
            <ContentTemplate>--%>                
                <table>
                    <tr>
                        <td class="alt1">
                            Account Name
                        </td>
                        <td class="alt1">
                            Patient First
                        </td>
                        <td class="alt1">
                            Patient Last
                        </td>
                        <td class="alt1">
                            Templates
                        </td>
                        <td class="alt1">
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:DropDownList ID="DDLAccounts" CssClass="common" runat="server" Width="264px" TabIndex="1">                      
                  </asp:DropDownList></td>
                        <td>
                            <asp:TextBox ID="txtPFName" runat="server" CssClass="common" TabIndex="2" Width="130px" ></asp:TextBox></td>
                        <td>
                            <asp:TextBox ID="txtPLName" runat="server" CssClass="common" TabIndex="3" Width="130px"></asp:TextBox></td>
                        <td>
                        <asp:TextBox ID="txtTemplate" runat="server" CssClass="common" TabIndex="3" Width="130px"></asp:TextBox></td>
                        <td>
                            <input name="SEARCH" type="submit" class="button" value="Search" />
                        </td>
                    </tr>
                                      
                </table>
            <%--</ContentTemplate>            
        </asp:UpdatePanel>--%>
        <iframe id="FaxPlusResult" frameborder="0" name="FaxPlusResult" src="PrevJobsResult.aspx" style="width: 100%; height: 368px"
            ></iframe>    
    </div>
        </div> 
        </div> 
    </form>
</body>
</html>
