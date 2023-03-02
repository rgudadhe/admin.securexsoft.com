<!-- #include file="setdbcon.inc"-->
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN"
"http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head>

<title>
Toll Free Inst.
</title>

<!--

Hey friend! This file shows you how
the CSS Theme you downloaded looks with
some example HTML form markup.

Check out the README.txt for more info.

-->

<!-- Meta Tags -->
<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />

<!-- JavaScript -->
<script type="text/javascript" >
function printpage()
{
var printbutton=document.getElementById("printpagebutton");
printbutton.style.visibility='hidden';
window.print();
printbutton.style.visibility='visible';
}

</script>

<!-- CSS -->
<link rel="stylesheet" href="css/structure.css" type="text/css" />
<link rel="stylesheet" href="css/form.css" type="text/css" />
<link rel="stylesheet" href="css/theme.css" type="text/css" />

<link rel="canonical" href="http://www.wufoo.com/gallery/designs/template.html">

</head>

<body id="public">

<div id="container">

<form action="" method="">

	<div class="info">
	<h2 align="center">Toll-Free Phone System Instructions</h2>
	<h3 class="desc"align="center"><U><%=request.form("D1")%></U></h3>
	</div>
	
	<ul>
	
	<li>
		<label class="desc">Step 1		Dial Toll Free Number (866)-239-1729 or (800)-385-4418.</label>
		<label class="desc">Step 2		Enter the Physician ID followed by # sign</label>
		<label class="desc">Step 3		Enter the Password followed by # sign</label>
		<label class="desc">Step 4		Enter Work Type followed by # sign</label>
		<label class="desc">Step 5		Enter Visit ID followed by # sign</label>
	</li>
	
	<li>
		<label class="desc"><strong><U>PROMPTS</U></strong></label>
	</li>
	<li>
	<div>
<%
Dim prmrst
Dim prmstr 
dim aname
dim pmpt
dim kn
dim des


m2="No Prompts Available"
aname = request.form("D1")

set prmrst=server.createobject("adodb.recordset")
prmstr="select * from tblprompts where accname='" & trim(aname) & "'"
prmrst.open prmstr,dbcon,3,3


    
 if prmrst.recordcount > 0 then
 
 %>
 <table border="1" width="300">
<tr>
<b>
<td class="style2"><Strong>Prompt</strong></td><td class="style1" align="center"><strong>Key In</strong></td>
	<td class="style1"><strong>Description</strong></td>
</b>
</tr>
<%
do while NOT prmrst.EOF

    pmpt=prmrst.fields("prompt")
	kn=prmrst.fields("keyin")
	des=prmrst.fields("description")
	
                                           
%>

<tr>
<td class="style1"><a prompt="<%=pmpt%>"><%=pmpt%></a></td>
<td class="style1"><a keyin="<%=kn%>"><%=kn%></a></td>
<td class="style1"><a description="<%=des%>"><%=des%></a></td>

</tr>

<%     
prmrst.movenext 
loop
prmrst.close
Else 
response.write m2 
end if%>
</table>
	<li>
		<label class="desc"><STRONG><U>KEYPAD</U></STRONG></label>
	</li>


<%
Dim keyrst
Dim keystr 
dim keyn
dim kdes
dim acn

m2="No Keypad Available"
acn = request.form("D1")

set keyrst=server.createobject("adodb.recordset")
keystr="SELECT DISTINCT tblKeypad.keyin, tblKeypad.activity FROM tbltollfree INNER JOIN tblKeypad ON tbltollfree.keypad=tblKeypad.Keypadname WHERE  (((tbltollfree.accname)='" & trim(acn) & "'))"
keyrst.open keystr,dbcon,3,3


    
 if keyrst.recordcount > 0 then
 
 %>
 <table border="1" width="300">
<tr>
<b>
<td class="style2"><Strong>Key#</strong></td><td class="style1"><strong>Description</strong></td>
</b>
</tr>
<%
do while NOT keyrst.EOF

    keyn=keyrst.fields("keyin")
	kdes=keyrst.fields("activity")
	                                           
%>

<tr>
<td class="style1"><a keyin="<%=keyn%>"><%=keyn%></a></td>
<td class="style1"><a activity="<%=kdes%>"><%=kdes%></a></td>
</tr>

<%     
keyrst.movenext 
loop
keyrst.close
Else 
response.write m2 
end if%>


</table>

	<li>
		<label class="desc"><STRONG><U>PHYSICIAN ID AND PASSWORD</U></STRONG></label>
	</li>
	
<%
Dim achkrst
Dim achstr 
dim accname
dim plname
dim pfname
dim pid
dim ppwd

m2="No Physicians found"
accname = request.form("d1")

set achkrst=server.createobject("adodb.recordset")
achkstr="select * from tbltollfree where accname='" & trim(accname) & "' order by diclname"
achkrst.open achkstr,dbcon,3,3


    
 if achkrst.recordcount > 0 then
 
 %>
 <table border="1" width="500">
<tr>
<b>
<td class="style2"><Strong>Physician Last Name</strong></td><td class="style1"><strong>Physician First Name</strong></td>
	<td class="style1"><strong>Physician ID</strong></td><td class="style1">
	<strong>Password</strong></td>
</b>
</tr>
<%
do while NOT achkrst.EOF

   plname=achkrst.fields("diclname")
	pfname=achkrst.fields("dicfname")
	pid=achkrst.fields("id")
	ppwd=achkrst.fields("password")
                                           
%>

<tr>
<td class="style1"><a diclname="<%=plname%>"><%=plname%></a></td>
<td class="style1"><a dicfname="<%=pfname%>"><%=pfname%></a></td>
<td class="style1"><a id="<%=pid%>"><%=pid%></a></td>
<td class="style1"><a password="<%=ppwd%>"><%=ppwd%></a></td>
</tr>

<%     
achkrst.movenext 
loop
achkrst.close
Else 
response.write m2 
end if%>
</table>
<li>
	<p align="justify"><b><U>NOTE</U>: Whenever you decide to use the floating ID where a physician needs to dictate and a permanent ID has not 
	yet been assigned, please ask the Dictator to identify his/her full name and credentials before dictating and send 
	an email to support@medofficepro.com providing full name and credentials of the physician so that an appropriate signature 
	block can be added to reports. Also, please note we typically require one business day (or 24 hrs) to create a 
	new Physician ID and Password. </b></p>
	</li>
	<li class="buttons">
		<input id="printpagebutton" onclick="printpage()" class="btTxt" type="button" value="Print this page" />
	</li>
</div>	
</li>
	

</form>


</div><!--container-->


</body>

</html>