<%@ Page Language="VB" AutoEventWireup="false" CodeFile="ViewBillingUserDetails.aspx.vb" Inherits="Billing_LCMethods_LCMethodology" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Register Assembly="KMobile.Web" Namespace="KMobile.Web.UI.WebControls" TagPrefix="asp" %>
<%@ Register Assembly="System.Web.Extensions, Version=1.0.61025.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI" TagPrefix="asp" %>

<%@ Register
    Assembly="AjaxControlToolkit"
    Namespace="AjaxControlToolkit"
    TagPrefix="ajaxToolkit" %>
    
<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
<link rel="stylesheet" type="text/css" href="../../App_Themes/Css/Styles.css"/>
<link rel="stylesheet" type="text/css" href="../../App_Themes/Css/Common.css"/>
<style type="text/css" >
tr.tblbg {
	background: #e7e6e6 url(../images/tbbg.jpg) repeat;
}
tr.tblbg1 {
	background: #e7e6e6 url(../images/tbbg.jpg) repeat;
}
tr.tblbg2 {
	background: #e7e6e6 url(../images/tbbg2.jpg) repeat;
	padding-left: 8px;
	padding-right: 8px;	
	text-align: center;
	border-left: 1px solid #f4f4f4;
	border-bottom: solid 2px #fff;
	color: #333;
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

</style>

    <title>Untitled Page</title>
</head>
<body>
 
    <form id="form1" runat="server">
<asp:ScriptManager ID="SCRIPTMANAGER1" runat="server"  ></asp:ScriptManager>      
          <div id="body">
        <div id="cap"></div>
        <div id="main">
        <h1>View SXF User Details</h1>
  
 <br /> 
           
     
    
        
        
  
                    <asp:Label ID="lblDisp" runat="server" Font-Bold="True"   Font-Names="Arial" Font-Size="8" ForeColor="#C00000"></asp:Label>&nbsp;
                    
                    
                      <asp:CompleteGridView ID="MyDataGrid" runat="server" CellPadding="2" AllowSorting="False" 
                    Font-Names="Arial"   
                    Font-Size="8" EnableViewState="False"  
                   CellSpacing="2" PageSize="25" CountFormat="Displaying records <b>{0}</b> to <b>{1}</b> from <b>{2}</b>" Font-Italic="False" CaptionAlign="Bottom" ShowCount="False"  SortAscendingImageUrl="" SortDescendingImageUrl="" AutoGenerateColumns="True"  >

                <FooterStyle BackColor="#5D7B9D" ForeColor="White" Font-Bold="True" CssClass="TransStatus"></FooterStyle>
                <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" Font-Names="Arial" Font-Size="8" HorizontalAlign="Center" VerticalAlign="Middle" CssClass="TransStatus"></HeaderStyle>
                <EditRowStyle BackColor="#999999" Font-Bold="True" Font-Italic="False" Font-Names="Arial" Font-Size="8" ForeColor="Black" HorizontalAlign="Center"></EditRowStyle>
                <PagerStyle BackColor="LightSlateGray" BorderStyle="Groove" ForeColor="White" BorderColor="#E0E0E0" HorizontalAlign="Center" CssClass="TransStatus"></PagerStyle>
                <AlternatingRowStyle BackColor="White" ForeColor="#284775" HorizontalAlign="Center" VerticalAlign="Middle"></AlternatingRowStyle>
                <RowStyle BackColor="#F7F6F3" ForeColor="#333333" HorizontalAlign="Center" VerticalAlign="Middle"></RowStyle>
                <PagerSettings PreviousPageText="Previous" PageButtonCount="25" Mode="NumericFirstLast" LastPageText="Last Page" FirstPageText="First Page" NextPageText="Next Page"></PagerSettings>
                <SelectedRowStyle BackColor="#E2DED6" ForeColor="#333333" Font-Bold="True"></SelectedRowStyle>
                          <Columns>
                             <%-- <asp:BoundField DataField="AccountName" HeaderText="Account Name" SortExpression="AccountName" />
                              <asp:BoundField DataField="Descr" HeaderText="Description" SortExpression="Descr" />
                              <asp:BoundField DataField="quantity" HeaderText="Quantity" SortExpression="quantity" />
                              <asp:BoundField DataField="Amount" HeaderText="Rate" SortExpression="Amount" />
                              <asp:BoundField DataField="TotAmount" HeaderText="Amount" SortExpression="TotAmount" />
                              <asp:BoundField DataField="ServiceDate" HeaderText="ServiceDate" SortExpression="ServiceDate" />--%>
                          </Columns>
                </asp:CompleteGridView>
    </div></div> 
    </form>
</body>
</html>
