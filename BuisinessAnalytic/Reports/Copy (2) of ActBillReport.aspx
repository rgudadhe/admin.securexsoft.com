<%@ Page Language="VB" AutoEventWireup="false" CodeFile="Copy (2) of ActBillReport.aspx.vb" Inherits="MIS_DailyMins" %>

<%@ Register Assembly="System.Web.Extensions, Version=1.0.61025.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI" TagPrefix="asp" %>
<%@ Register Assembly="KMobile.Web" Namespace="KMobile.Web.UI.WebControls" TagPrefix="asp" %>
<%@ Register
    Assembly="AjaxControlToolkit"
    Namespace="AjaxControlToolkit"
    TagPrefix="ajaxToolkit" %>
   

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
<link href= "../../App_Themes/Css/styles.css" type="text/css" rel="stylesheet"/>
<link href= "../../App_Themes/Css/Common.css" type="text/css" rel="stylesheet"/>
<%--<style type="text/css" >
tr.tblbg {
	background: #e7e6e6 url(../images/tbbg.jpg) repeat;
	font: 8pt 'Arial', Tahoma, arial, sans-serif;
}
tr.tblbg1 {
	background: #e7e6e6 url(../images/tbbg.jpg) repeat;
	font: 8pt 'Arial', Tahoma, arial, sans-serif;
}
tr.tblbg2 {
	background: #e7e6e6 url(../images/tbbg2.jpg) repeat;
	padding-left: 8px;
	padding-right: 8px;	
	text-align: center;
	border-left: 1px solid #f4f4f4;
	border-bottom: solid 2px #fff;
	color: #333;
	font: 8pt 'Arial', Tahoma, arial, sans-serif;
}
/* links */
a, a:visited {	
	color: #000000; 
	background: inherit;
	text-decoration: none;		
	font: 8pt 'Arial', Tahoma, arial, sans-serif;
}
a:hover {
	color: #000000;
	background: inherit;
	padding-bottom: 0;
	border-bottom: 2px solid #dbd5c5;
	font: 8pt 'Arial', Tahoma, arial, sans-serif;
}

tr.tblbgbody {
	background: #e7e6e6 url(../images/bgorange1.JPG) repeat;
	
	padding-left: 8px;
	padding-right: 8px;	
	text-align: center;
	border-left: 1px solid #f4f4f4;
	border-bottom: solid 2px #fff;
	color: #333;
}
input.button { 
	font: bold 8pt Arial, Sans-serif; 
	height: 24px;
	margin: 0;
	padding: 2px 3px; 
	color: #ffffff;
	background: #E56717;
	border: 1px solid #dadada;
}

</style>--%>
    <title>Untitled Page</title>
</head>
<body style="text-align: center">
    <form id="form1" runat="server">
  
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
           
        
        
        <asp:Table ID="tblMins" runat="server" Font-Bold="False" Font-Italic="False" Font-Names="Arial" Font-Size="8"  Width="100%" BorderColor="Silver" GridUnits="both"  HorizontalAlign="Center" CssClass="tblbg" >
        <asp:TableRow ID="TableRow1" runat="server" HorizontalAlign="Center" CssClass="tblbgbody" ForeColor = "White" Font-Size="8"   >
                <asp:TableCell runat="server" ID="tblDtls" CssClass="tblbgbody" ColumnSpan="6"  Font-Bold="True"     >Invoice Summary</asp:TableCell>
                </asp:TableRow>
               
            <asp:TableRow runat="server" HorizontalAlign="Center" CssClass="tblbg"   Font-Bold="True"    >
                <asp:TableCell runat="server" ID="TableCell9">Date</asp:TableCell>
               <asp:TableCell runat="server" ID="TableCell2">Service</asp:TableCell> 
                <asp:TableCell runat="server" ID="TableCell8">Activity</asp:TableCell>
                <asp:TableCell runat="server" ID="R1Cell2">Quantity</asp:TableCell>
                <asp:TableCell runat="server" ID="R1Cell3">Rate</asp:TableCell>
                <asp:TableCell runat="server" ID="TableCell1">Amount</asp:TableCell>
            </asp:TableRow>
           
        </asp:Table>
      <br />
        <asp:Label ID="lblActivity"  runat="server"  CssClass="tblbgbody" ForeColor = "Black" Font-Size="8"   Font-Bold="True" Font-Names="Arial Unicode MS"></asp:Label>
        <br />
      <br />

 <asp:Table ID="tblDict" runat="server" Font-Bold="False" Font-Italic="False" Font-Names="Arial" Font-Size="8"  Width="50%" BorderColor="Silver" GridUnits="both"  HorizontalAlign="Center" CssClass="tblbg" >
      
                            <asp:TableRow ID="TableRow4" runat="server" HorizontalAlign="Center" CssClass="tblbgbody" ForeColor = "White" Font-Size="8"   >
                <asp:TableCell runat="server" ID="TableCell3" CssClass="tblbgbody" ColumnSpan="6"    >DictatorWise Details</asp:TableCell>
                </asp:TableRow>
            <asp:TableRow ID="TableRow3" runat="server" HorizontalAlign="Center" CssClass="tblbg"  >
                <asp:TableCell runat="server" ID="TableCell4">Dictator Name</asp:TableCell>
               <asp:TableCell runat="server" ID="TableCell5">Units</asp:TableCell>
               <asp:TableCell runat="server" ID="TableCell21">Amount</asp:TableCell>
               <asp:TableCell runat="server" ID="TableCell20">Number of Records</asp:TableCell> 
            </asp:TableRow>
           
        </asp:Table>
        
        <br />
      

 <asp:Table ID="tblReports" runat="server" Font-Bold="False" Font-Italic="False" Font-Names="Arial" Font-Size="8"  Width="100%" BorderColor="Silver" GridUnits="both"  HorizontalAlign="Center" CssClass="tblbg" >
      
                            <asp:TableRow ID="TableRow2" runat="server" HorizontalAlign="Center" CssClass="tblbgbody" ForeColor = "White" Font-Size="8"   >
                <asp:TableCell runat="server" ID="TableCell6"  ColumnSpan="7"  >JobWise Details (<asp:Label ID="lblDict" runat="server" CssClass="tblbgbody"  Font-Size="8"  ></asp:Label>)</asp:TableCell>
                </asp:TableRow>
            <asp:TableRow ID="TableRow5" runat="server" HorizontalAlign="Center" CssClass="tblbg"  >
                <asp:TableCell runat="server" ID="TableCell7">Dictator Name</asp:TableCell>
               <asp:TableCell runat="server" ID="TableCell10">Job#</asp:TableCell> 
               <asp:TableCell runat="server" ID="TableCell11">Voice Job#</asp:TableCell>
               <asp:TableCell runat="server" ID="TableCell15">Patient</asp:TableCell>
                <asp:TableCell runat="server" ID="TableCell19">Template</asp:TableCell>
                <asp:TableCell runat="server" ID="TableCell16">Date of Dictation</asp:TableCell> 
               <asp:TableCell runat="server" ID="TableCell12">Date of Transcription</asp:TableCell> 
               <asp:TableCell runat="server" ID="TableCell13">Units</asp:TableCell> 
               <asp:TableCell runat="server" ID="TableCell17">Rate</asp:TableCell> 
               <asp:TableCell runat="server" ID="TableCell18">Amount</asp:TableCell> 
               <asp:TableCell runat="server" ID="TableCell14">STAT</asp:TableCell> 
               
            </asp:TableRow>
           
        </asp:Table>
   
    </form>
</body>
</html>

