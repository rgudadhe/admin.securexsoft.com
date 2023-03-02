<%@ Page Language="VB" AutoEventWireup="false" CodeFile="ViewUserStatus.aspx.vb" Inherits="RoutingTool_Default" %>

<%@ Register Assembly="System.Web.Extensions, Version=1.0.61025.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI" TagPrefix="asp" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>


<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 1.0 Transitional/EN">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
<link href= "../App_Themes/Css/Routing.css" type="text/css" rel="stylesheet"/>
<link href= "../App_Themes/Css/Common.css" type="text/css" rel="stylesheet"/>
<link href= "../App_Themes/Css/Styles.css" type="text/css" rel="stylesheet"/>
<script type="text/javascript"   src="sortable.js"></script>
    <title>View User Status</title>
    <script language="javascript" type="text/javascript"    >

function refresh()
{
    window.location.reload();
}

</script>
</head>
<body>
   <form id="form1" runat="server">
   
       <asp:ScriptManager ID="ScriptManager1" runat="server">
       </asp:ScriptManager>
       <div id="body">
        <div id="cap"></div>
        <div id="main">
        <h1>View User Status</h1>
      <asp:Panel ID="Panel1" runat="server" Width="100%" HorizontalAlign="Left">
                         <asp:Table ID="Table2"  runat="server"  style="text-align: center">

                            <asp:TableRow ID="TableRow1" CssClass="noScroll"   runat="server" style="text-align: center" VerticalAlign="Top">
                       
                                <asp:TableCell ID="TableCell1" CssClass="alt1"  runat="server">User Name</asp:TableCell >
                                <asp:TableCell ID="TableCell2" CssClass="alt1"  runat="server">User ID</asp:TableCell >
                                <asp:TableCell ID="TableCell3" CssClass="alt1"  runat="server">SchMins</asp:TableCell >
                                 <asp:TableCell ID="TableCell6" CssClass="alt1"  runat="server">STime</asp:TableCell >
                                  <asp:TableCell ID="TableCell7" CssClass="alt1" runat="server">ETime</asp:TableCell >
                                <asp:TableCell runat="server" CssClass="bk" ID="CellMdone" ><asp:Label  ID="LDone" runat="server"></asp:Label></asp:TableCell >
                                <asp:TableCell  runat="server" CssClass="bk" ID="CellCout" ><asp:Label ID="LOut" runat="server" ></asp:Label></asp:TableCell >
                                <asp:TableCell ID="TableCell4" CssClass="alt1"  runat="server" >Pending</asp:TableCell >
                                <asp:TableCell ID="TableCell5" CssClass="alt1"   runat="server">Direct Mins</asp:TableCell >
                                
                            </asp:TableRow> 

                            
                    
                            </asp:Table>
                        
                        
                        
                    </asp:Panel>  
            <asp:Panel ID="Panel2" runat="server" HorizontalAlign="Left">
                <table class="common" style="text-align:left" >
               <tr>
                   <td  >
                  Level: 
                   </td>
                   <td>
                    <asp:DropDownList ID="DLLevel" runat="server" AutoPostBack="true" CssClass="common">
                   
                    </asp:DropDownList> 
                   </td>
              
               </tr>
              
           </table>
           <input id="Button1" type="button" class="button" value="Refresh Page" onclick="refresh();"/>
            </asp:Panel>               
        <br />
        <br />
       <asp:HiddenField ID="HSelLvl" runat="server" />
      <asp:HiddenField ID="CurrDate" runat="server" /> 
        <br />
        </div> 
        </div> 
    </form>
</body>
</html>

