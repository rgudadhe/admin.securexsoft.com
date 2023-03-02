<%@ Page Language="VB" AutoEventWireup="false" CodeFile="toolbar.aspx.vb" Inherits="Topbar_toolbar" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML><HEAD>
<meta http-equiv="Content-Language" content="en-us">
<TITLE>Secure-Fax</TITLE>
<META name=description 
>
<script language="javascript">
<!-- hide script from old browsers

//detect browser:
browserName = navigator.appName;
browserVer = parseInt(navigator.appVersion);
if (browserName == "Netscape" && browserVer >= 3) browserVer = "1";
else if (browserName == "Microsoft Internet Explorer" && browserVer == 4) browserVer = "1";
else browserVer = "2";

//preload images:
if (browserVer == 1) {
//Compose1 = new Image(32,32);
//Compose1.src = "Compose1.png";
//Compose2 = new Image(32,32);
//Compose2.src = "Compose2.png";
//Inbox1 = new Image(32,32);
//Inbox1.src = "Inbox1.png";
//Inbox2 = new Image(32,32);
//Inbox2.src = "Inbox2.png";
//OutBox1 = new Image(32,32);
//OutBox1.src = "OutBox1.png";
//OutBox2 = new Image(32,32);
//OutBox2.src = "OutBox2.png";
//Sent1 = new Image(32,32);
//Sent1.src = "Sent1.png";
//Sent2 = new Image(32,32);
//Sent2.src = "Sent2.png";
//Addbook1 = new Image(32,32);
//Addbook1.src = "Addbook1.png";
//Addbook2 = new Image(32,32);
//Addbook2.src = "Addbook2.png";
//Help1 = new Image(32,32);
//Help1.src = "Help1.png";
//Help2 = new Image(32,32);
//Help2.src = "Help2.png";
TroubleTicket1 = new Image(48,48);
TroubleTicket1.src = "TroubleTicket1.png";
TroubleTicket2 = new Image(48,48);
TroubleTicket2.src = "TroubleTicket2.png";

Support1 = new Image(48,48);
Support1.src = "Support1.png";
Support2 = new Image(48,48);
Support2.src = "Support2.png";

}

//image swapping function:
function hiLite(imgDocID, imgObjName, comment) {
if (browserVer == 1) {
document.images[imgDocID].src = eval(imgObjName + ".src");
document.images[imgDocID].alt = comment;
//window.status = comment;
 return true;
}}
//end hiding -->

function Open()
{
    //var color='<%= Session("userLogin").ToString %>'

   //alert('Test');
//    //alert(varcolor)
//    var MySessionvalue='';
//    MySessionvalue =  "<%=Session("userLogin")%>" ; 
//    
//    alert(MySessionvalue);

    //var a;
    //a = <%=Session("userLogin")%>;
    //alert(a);

    return false;
}
</script>
<STYLE type=text/css>BODY {
	FONT-FAMILY: 'Trebuchet MS', 'Arial'; FONT-SIZE: 12px
}
HTML {
	WIDTH: 100%; HEIGHT: 100%
}
H1 {
	BORDER-BOTTOM: 0px; BORDER-LEFT: 0px; PADDING-BOTTOM: 5px; PADDING-LEFT: 0px; PADDING-RIGHT: 0px; COLOR: #29537e; FONT-SIZE: 22px; BORDER-TOP: 0px; FONT-WEIGHT: bold; BORDER-RIGHT: 0px; PADDING-TOP: 20px
}
A {
	COLOR: #000; TEXT-DECORATION: none
}
A:hover {
	TEXT-DECORATION: underline
}
#copyright {
	MARGIN: 0px 20px 20px; COLOR: #000; FONT-SIZE: 10px
}
.copyrightTd {
	PADDING-LEFT: 20px; FONT-FAMILY: Tahoma, Arial, Helvetica; COLOR: #848484; FONT-SIZE: 10px
}
.bottomMenuTd {
	PADDING-RIGHT: 20px; FONT-FAMILY: Trebuchet MS, Tahoma, Arial, Helvetica; COLOR: #000000; FONT-SIZE: 11px
}
</STYLE>

<META name=GENERATOR content="Microsoft FrontPage 5.0"></HEAD>
<BODY BACKGROUND-COLOR: #ffffff" leftMargin=20 topMargin=0 marginheight="0" marginwidth="20">
<!--<LINK rel=stylesheet type=text/css href="toolbar.css">-->
<SCRIPT type=text/javascript src="toolbar.js"></SCRIPT>

<table border="0" cellpadding="0" cellspacing="0" style="border-collapse: collapse" bordercolor="#111111" width="100%" height="65">
  <tr>
    <td align="left" valign="bottom" rowspan="4" height="29" width="86" ><img src="<%=logopath%>" width="85" height="64"></td>
    <td align="left" valign="bottom" rowspan="4" width="112" height="29" >
    <img border="0" src="barstart.PNG" width="112" height="29"></td>
    
  </tr>
  <tr>
    
    
      <td width="100%" colspan="9" style="height: 22px">
      </td>
    <!--<td width="100%" height="22" rowspan="2" align="right" valign="bottom">    
    <a href="javascript:history.go(-1);" target="RFrame" >
    <img border="0" src="backimg.jpg" width="63" height="19"></a>
    <img border="0" src="bar.PNG" width="100%" height="7">
    </td>-->
  </tr>
  <tr>       
    
    <!--<td width="60" align="center" style="height: 22px"><p>
        <a href="../ComposeFAX.aspx" target="RFrame" onMouseOver="hiLite('Compose','Compose2','Compose')"  onMouseOut="hiLite('Compose','Compose1','Compose')"><img name="Compose" src="Compose1.png" border=0 height=32 style="width: 32px"></a>&nbsp;
        </p>
    </td>      
    <td width="60" align="center" style="height: 22px"><p>
        <a href="../InBoxN.aspx" target="RFrame" onMouseOver="hiLite('Inbox','Inbox2','Inbox')"  onMouseOut="hiLite('Inbox','Inbox1','Inbox')"><img name="Inbox" src="Inbox1.png" border=0 width=32 height=32></a>&nbsp;            
        </p>
    </td>
    
    <td width="60" align="center" style="height: 1px"><p>
        <a href="../OutBox.aspx" target="RFrame" onMouseOver="hiLite('OutBox','OutBox2','OutBox')"  onMouseOut="hiLite('OutBox','OutBox1','OutBox')"><img name="OutBox" src="OutBox.png" border=0 width=32 height=32></a>&nbsp;
        </p>
    </td>      
    <td width="60" align="center" style="height: 22px"><p>
         <a href="../SentFax.aspx" target="RFrame" onMouseOver="hiLite('Sent','Sent2','Sent')"  onMouseOut="hiLite('Sent','Sent1','')"><img name="Sent" src="Sent1.png" border=0 width=32 height=32></a>&nbsp;
         </p>
    </td>-->
    <!--<td width="60" align="center" style="height: 22px"><p>        
         <a href="javascript:void(0)" target="RFrame" onMouseOver="hiLite('Addbook','Addbook2','Addres Book')"  onMouseOut="hiLite('Addbook','Addbook1','Address Book')"><img name="Addbook" src="Addbook1.png" border=0 width=32 height=32></a>&nbsp;
         </p>   
    </td>-->      
    <td width="60" align="center" style="height: 22px"><p>        
         <a href="../CIMS/CIMS.aspx" target="RFrame" onMouseOver="hiLite('TroubleTicket','TroubleTicket2','Trouble Ticket')"  onMouseOut="hiLite('TroubleTicket','TroubleTicket1','Trouble Ticket')"><img name="TroubleTicket" src="TroubleTicket1.png" border=0 width=48 height=48></a>&nbsp;&nbsp;&nbsp;
         </p>
    </td>
    <td width="60" align="center" style="height: 22px"><p>        
         <a href="https://www1.gotomeeting.com/en_US/island/download.tmpl?Action=rgoto&_sf=1" target="_blank" onMouseOver="hiLite('Support','Support2','Online Support')"  onMouseOut="hiLite('Support','Support1','Online Support')"><img name="Support" src="Support1.png" border=0 width=48 height=48></a>&nbsp;&nbsp;
         </p>
    </td>
    <!--<td width="60" align="center" style="height: 1px"><p>        
         <a href="javascript:void(0)" target="RFrame" onMouseOver="hiLite('Help','Help2','Help')"  onMouseOut="hiLite('Help','Help1','Help')"><img name="Help" src="Help1.png" border=0 width=32 height=32></a>    
         </p>
    </td>-->
        <td width="100%" align="right" valign="bottom" style="height: 22px">    
            <a href="javascript:history.go(-1);" onMouseOver="hiLite('Back','Back2','Back')"  onMouseOut="hiLite('Back','Back1','Back')"><img name="Back" src="Back1.png" border=0 width=48 height=48></a>&nbsp;&nbsp;&nbsp;
        </td>
  </tr>
  <tr>
    <td align="left" valign="bottom" colspan="11" style="height: 9px"><img border="0" src="bar.PNG" width="100%" height="7"></td>
  </tr>
</table>

</BODY></HTML>