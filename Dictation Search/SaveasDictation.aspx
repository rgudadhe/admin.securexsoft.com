<%@ Page Language="VB" AutoEventWireup="false" CodeFile="SaveasDictation.aspx.vb" Inherits="Dictation_Search_SaveasDictation" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 1.0 Transitional/EN">

<html xmlns="http://www.w3.org/1999/xhtml" >

<head runat="server">
  <link href= "../App_Themes/Css/Main.css" type="text/css" rel="stylesheet" />
    <link href= "../App_Themes/Css/Common.css" type="text/css" rel="stylesheet" />
    <link href= "../App_Themes/Css/Styles.css" type="text/css" rel="stylesheet" />
    <title>Untitled Page</title>
    <script>

document.oncontextmenu=new Function("return false"); 
</script>


</head>
<body >
    <form id="form1" runat="server">
     <div id="body">
        <div id="cap"></div>
        <div id="main">
        <h1> Job Details</h1>
    
     <asp:Table ID="Table2" runat="server" CellSpacing="2" CellPadding="2"   
            Width="100%" CssClass="common" HorizontalAlign="Center">
            
            <asp:TableRow ID="TRow1" runat="server" HorizontalAlign="Center">
                <asp:TableCell ID="TableCell2" runat="server" ColumnSpan="6">
                <object id="mediaplayer" classid="clsid:22d6f312-b0f6-11d0-94ab-0080c74c7e95" codebase="http://activex.microsoft.com/activex/controls/mplayer/en/nsmp2inf.cab#version=5,1,52,701" standby="loading microsoft windows media player components..." type="application/x-oleobject" width="320" height="310">
<param name="filename" value="./test.wmv">
     <param name="animationatstart" value="true">
     <param name="transparentatstart" value="true">
     <param name="autostart" value="true">
     <param name="showcontrols" value="true">
     <param name="ShowStatusBar" value="true">
     <param name="windowlessvideo" value="true">
     <embed src="./test.wmv" autostart="true" showcontrols="true" showstatusbar="1" bgcolor="white" width="320" height="110">
</object>
  </asp:TableCell>
            </asp:TableRow>
            </asp:Table> 
   <asp:Table ID="Table1" runat="server" CellSpacing="2" CellPadding="2"   
            Width="100%" CssClass="common" HorizontalAlign="Center">       
            <asp:TableRow ID="TableRow2" runat="server" HorizontalAlign="Center" BorderStyle="Solid" BorderWidth="1"   >
                <asp:TableCell ID="TableCell4" runat="server" HorizontalAlign="Center"  CssClass="alt1"  BorderStyle="Solid" BorderWidth="1"  >Job#</asp:TableCell>
                <asp:TableCell ID="TableCell3" runat="server" HorizontalAlign="Center"  CssClass="alt1"  BorderStyle="Solid" BorderWidth="1"  >Voice Job#</asp:TableCell>
                <asp:TableCell ID="TableCell1" runat="server" HorizontalAlign="Center"  CssClass="alt1"  BorderStyle="Solid" BorderWidth="1"  >File Type</asp:TableCell>
               
               
            </asp:TableRow>
            <asp:TableRow ID="TableRow3" runat="server" HorizontalAlign="Center"  BorderStyle="Solid" BorderWidth="1" >
               <asp:TableCell ID="TCell1"  runat="server" HorizontalAlign="Center"  CssClass="alt"  BorderStyle="Solid" BorderWidth="1"   ></asp:TableCell>
               <asp:TableCell ID="TCell2" runat="server" HorizontalAlign="Center"  CssClass="alt"  BorderStyle="Solid" BorderWidth="1"   ></asp:TableCell>
               <asp:TableCell ID="TCell3" runat="server" HorizontalAlign="Center"  CssClass="alt"  BorderStyle="Solid" BorderWidth="1"   ></asp:TableCell>
               
            </asp:TableRow>
            </asp:Table> 
            
             <br />
            
            <input type="button" class="button" value="Download File" onclick="javascript:window.location='<%=MediaURL %>'" />
          <%--  <a href="<%=MediaURL%>">Download</a> Click on link and select Save As Target option--%>
          <%--<asp:Label ID="Label2" runat="server"   CssClass="Title1" ForeColor="Maroon"></asp:Label><asp:LinkButton
              ID="LinkButton1" runat="server" >LinkButton</asp:LinkButton>--%>
            <asp:Label ID="Label1" runat="server"   CssClass="Title1" ForeColor="Maroon"></asp:Label>
              </div></div> 
            </form></body></html> 