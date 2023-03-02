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
</head>

<body id="public">

<div id="container">

<form action="" method="">

	</form>

	<div>
	<h2 align="center"><font face="Arial">Toll-Free Phone System Instructions</font></h2>
	<h3 class="desc" align="center" face="Arial"><U><%=request.form("D1")%></U></h3>
	</div>
	
	<ul>
	
		<P>&nbsp;</P>
		<P><b><font face="Arial" size="2">Step 1&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Dial Toll Free Number (866)-239-1729 or (800)-385-4418.</font></b></P>
		<P><b><font face="Arial" size="2">Step 2&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Enter the Physician ID followed by # sign</font></b></P>
		<P><b><font face="Arial" size="2">Step 3&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Enter the Password followed by # sign</font></b></P>
		<P><b><font face="Arial" size="2">Step 4&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Enter Work Type followed by # sign</font></b></P>
		<P><b><font face="Arial" size="2">Step 5&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Enter Visit ID followed by # sign<br>
&nbsp;</font></b></P>
	
	<p>
		<font face="Arial">
		<label class="desc"><strong><U><font size="2">PROMPTS</font></U></strong></label><font size="2">
        </font>
		<div style="width: 970; height: 369"></p>
        &nbsp;
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
 			<table border="1" width="300" style="border-collapse: collapse; font-family:Arial; font-size:8pt" bordercolor="#111111" cellpadding="0" cellspacing="0">
			<tr>
			<b>
				<td bgcolor="#000080">
                <font color="#FFFFFF" size="2" face="Arial"><Strong>Prompt</strong></font></td>
            <td align="center" bgcolor="#000080">
            <font color="#FFFFFF" size="2" face="Arial"><strong>Key In</strong></font></td>
            <td bgcolor="#000080"><font color="#FFFFFF" size="2" face="Arial"><strong>Description</strong></font></td>
			</b>
			</tr>
		<%
			do while NOT prmrst.EOF
		     pmpt=prmrst.fields("prompt")
			kn=prmrst.fields("keyin")
			des=prmrst.fields("description")
		%>

			<tr>
					<td class="style1"><a prompt="<%=pmpt%>"><%=pmpt%></a>&nbsp;</td>
					<td class="style1"><a keyin="<%=kn%>"><%=kn%></a>&nbsp;</td>
					<td class="style1"><a description="<%=des%>"><%=des%></a>&nbsp;</td>

			</tr>

		<%     
				prmrst.movenext 
				loop
				prmrst.close
				Else 
				response.write m2 
				end if
			%>
			
			
	</table>
		<p>
			<u><b><font size="2">KEYPAD MAPPING</font></b></u></font><font size="2">
            </font>
			</p>
	
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
 		<table border="1" width="300" style="border-collapse: collapse; font-family:Arial; font-size:8pt" bordercolor="#111111" cellpadding="0" cellspacing="0">
		<tr>
		<b>
				<td class="style2" bgcolor="#000080">
                <font color="#FFFFFF" face="Arial" size="2"><Strong>Key#</strong></font></td>
        <td class="style1" bgcolor="#000080">
        <font color="#FFFFFF" face="Arial" size="2"><strong>Description</strong></font></td>
		</b>
		</tr>
		<%
				do while NOT keyrst.EOF
				keyn=keyrst.fields("keyin")
				kdes=keyrst.fields("activity")
	                                           
			%>

			<tr>
					<td class="style1"><a keyin="<%=keyn%>"><%=keyn%></a>&nbsp;</td>
					<td class="style1"><a activity="<%=kdes%>"><%=kdes%></a>&nbsp;</td>
			</tr>

			<%     
				keyrst.movenext 
				loop
				keyrst.close
				Else 
				response.write m2 
				end if
			%>

</table>
	<p>
		<font face="Arial" size="2">
		<label><strong><U>PHYSICIAN ID AND PASSWORD</U></strong></label> </font>
		<div style="width: 970; height: 369"></p>
	&nbsp;
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
			 <table border="1" width="500" style="border-collapse: collapse; font-family:Arial; font-size:8pt" bordercolor="#111111" cellpadding="0" cellspacing="0">
			<tr>
			<b>
				<td bgcolor="#000080">
                <font color="#FFFFFF" face="Arial" size="2"><Strong>Physician Last Name</strong></font></td>
            <td bgcolor="#000080"><font color="#FFFFFF" face="Arial" size="2"><strong>Physician First Name</strong></font></td>
            <td bgcolor="#000080"><font color="#FFFFFF" face="Arial" size="2"><strong>Physician ID</strong></font></td>
            <td bgcolor="#000080"><font color="#FFFFFF" face="Arial" size="2"><strong>Password</strong></font></td>
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
					<td class="style1"><a diclname="<%=plname%>"><%=plname%></a>&nbsp;</td>
					<td class="style1"><a dicfname="<%=pfname%>"><%=pfname%></a>&nbsp;</td>
					<td class="style1"><a id="<%=pid%>"><%=pid%></a>&nbsp;</td>
					<td class="style1"><a password="<%=ppwd%>"><%=ppwd%></a>&nbsp;</td>
			</tr>

			<%     
					achkrst.movenext 
					loop
					achkrst.close
					Else 
					response.write m2 
					end if
			%>
</table>
	<p align="justify"><font face="Arial" size="2"><b><U>NOTE</U>: Whenever you decide to use the floating ID where a physician needs to dictate and a permanent ID has not 
	yet been assigned, please ask the Dictator to identify his/her full name and credentials before dictating and send 
	an email to support@medofficepro.com providing full name and credentials of the physician so that an appropriate signature 
	block can be added to reports. Also, please note we typically require one business day (or 24 hrs) to create a 
	new Physician ID and Password. </b></font></p>
	<p>
		<input id="printpagebutton" onclick="printpage()" class="btTxt" type="button" value="Print this page" style="font-family: Arial; font-size: 10pt; font-weight: bold" />
	</p>
</div>	
</form>
</div>
</ul>
</ol>
</div>
</body>
</html>