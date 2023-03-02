<%@ Page Language="vb" AutoEventWireup="false" CodeFile="LoginOld.aspx.vb" Inherits="Login" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 1.0 Transitional/EN">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<meta http-equiv="Content-Type" content="text/html; charset=iso-8859-1" />
<meta name="verify-v1" content="YQn9DHjceEmGngB0QDHGQbvq8mQDHKKJ20gxkzleCyA=" />
<title>SecureXFlow</title>
<link href="ETSstyle.css" rel="stylesheet" type="text/css" />
  <script language="javascript">

function checkVersion() {
checkIE();
}
function OpenModalPopup() 
{
//var myObject = new Object();
//var WinSettings = "center:yes;resizable:no;dialogHeight:300px;dialogWidth:400px;"
var MyArgs = window.open("ChangePass.aspx", 'name','height=300,width=400');
if (window.focus) {MyArgs.focus()}

//self.close();
}
</script>

<script language="vbscript" type="text/vbscript" >
Sub checkIE()
	'msgbox "Hello"
	On Error Resume Next 
	set OFM1 = createobject("scripting.filesystemobject")
    if Err.Number="429" then
     window.navigate("\iesetup\default.htm")
    End If
    
    
End Sub    
</script>

    
</head>
<body >

<div id="topPan">
	
	<p>
        <span style="font-size: 26pt; font-family: Niagara Engraved">SecureXFlow<span style="font-size: 21pt; font-family: Niagara Engraved"><sup>TM</sup></span> </span><br /><br />
	Secure Workflow Management</p>
		
  <div id="topContactPan">
  </div>
	<div id="topMenuPan">
	 
	  
	  
	
	</div>
</div>

<div id="bodyPan">
  <div id="bodyLeftPan" style="width: 584px; text-align: center">
  	
	<div id="bodyLeftNextPan" >
	<div id="bodyPannel" style="text-align: center" >
	<!-- login form start-->
			<form id="Form1" method="post" action="#" class="search" runat="server"  >
			
			    <h2>
                  &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; Login <span>area</span></h2>
			  <label>your name</label><asp:TextBox ID="name" runat="server" width="158" Font-Italic="True" Font-Names="Trebuchet MS"></asp:TextBox>
			  <label>password</label><asp:TextBox ID="password" runat="server" width="158" TextMode="Password"  Font-Italic="True" Font-Names="Trebuchet MS"></asp:TextBox>
			  <p>&nbsp;</p>
			  <table width="100%"><tr><td style="text-align:left;"><%--<asp:Button ID="Button2" Text="Customer" CssClass="submit" width="100px"  runat="server"  />--%></td><td style="text-align:right;"><asp:Button ID="Button1" Text="Admin" CssClass="submit"  runat="server"  /></td></tr></table>
                
               <p><asp:Label ID="lblMessage" runat="server" ></asp:Label>
         </p>
        
			</form>
			</div>
         
	</div>
  </div>
  

 
</div>

<div align="center" >
<br>
<br>
<br>
<br> 
<span id="cdSiteSeal1"><script type="text/javascript" src="//tracedseals.starfieldtech.com/siteseal/get?scriptId=cdSiteSeal1&amp;cdSealType=Seal1&amp;sealId=55e4ye7y7mb73e6c2d3d8f824b61ec5khfy7mb7355e4ye743f60b6e16e94ec67"></script></span>
</div>
</body>
</html>
