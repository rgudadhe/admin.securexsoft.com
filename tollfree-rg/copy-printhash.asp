<!-- #include file="setdbcon.inc"-->
<html>

<head>
<meta http-equiv="Content-Language" content="en-us">
<meta name="GENERATOR" content="Microsoft FrontPage 5.0">
<meta name="ProgId" content="FrontPage.Editor.Document">
<meta http-equiv="Content-Type" content="text/html; charset=windows-1252">
<title>Toll Free Instruction</title>

<script type="text/javascript" >
function printpage()
{
var printbutton=document.getElementById("printpagebutton");
var backbutton=document.getElementById("bbutton");
printbutton.style.visibility='hidden';
backbutton.style.visibility='hidden';
window.print();
printbutton.style.visibility='visible';
backbutton.style.visibility='visible';
}
</script>

</head>

<body>
<table width="90%" border="0" align="center">
<tr>
<td>
<form>
	<div>
	<h2 align="center"><font face="Arial">Toll-Free Phone System Instructions</font></h2>
		<h3 class="desc" align="center"><font face="Arial"><U><%=request.form("D1")%></U></font></h3>

	</div>
	
		
		<P>
		<b><font face="Arial" size="2">Step 1&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Dial Toll Free Number (866)-239-1729 or (800)-385-4418.</font></b><br>
		<b><font face="Arial" size="2">Step 2&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Enter the Physician ID followed by # sign</font></b><br>
		<b><font face="Arial" size="2">Step 3&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Enter the Password followed by # sign</font></b><br>
		<b><font face="Arial" size="2">Step 4&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Enter Work Type followed by # sign</font></b><br>
		<b><font face="Arial" size="2">Step 5&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Enter Visit ID followed by # sign<br></font></b></P>
	

<table border="0" cellpadding="0" cellspacing="0" style="border-collapse: collapse; valign: top;" bordercolor="#111111" width="83%">
  <tr>
    <td width="100%">&nbsp;<table border="0" cellpadding="0" cellspacing="0" style="border-collapse: collapse" bordercolor="#111111" width="858">
      <tr>
        <td width="338">
        <table border="1" cellpadding="0" cellspacing="0" style="border-collapse: collapse; valign: top; font-family:Arial; font-size:8pt" bordercolor="#111111" width="100%">
        <tr>
        <th colspan="3" dir="ltr" width="506"><font face="Arial" size="2">Keypad</font></th>
        </tr>
          <b>
				<td class="style2" bgcolor="#000080" dir="ltr" align="center" style="-webkit-print-color-adjust: exact;" width="152">
                <font color="#FFFFFF" face="Arial" size="2"><Strong>Key#</strong></font></td>
        <td class="style1" bgcolor="#000080" dir="ltr" align="left" style="-webkit-print-color-adjust: exact;" width="346">
        <font color="#FFFFFF" face="Arial" size="2"><strong>Description</strong></font></td>
		</b>
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
 
 		

				do while NOT keyrst.EOF
				keyn=keyrst.fields("keyin")
				kdes=keyrst.fields("activity")
	            if kdes="None" then
				keyrst.movenext
				else
			%>

			<tr>
					<td align="center" width="152"><a keyin="<%=keyn%>"><%=keyn%></a>&nbsp;</td>
					<td class="style1" width="346"><a activity="<%=kdes%>"><%=kdes%></a>&nbsp;</td>
			</tr>

			<%     
				keyrst.movenext 
				end if
				loop
				keyrst.close
				Else 
				response.write m2 
				end if
			%>
        </table>
        </td>
		<td width="4">&nbsp;</td>
        <td width="516" valign="top">
        
        <table border="1" cellpadding="0" cellspacing="0" style="border-collapse: collapse; valign: top; font-family:Arial; font-size:8pt" bordercolor="#111111" width="314" >
        <%
			Dim prmrst
			Dim prmstr 
			dim aname
			dim pmpt
			dim kn
			dim des
			m2="Prompts Not Available"
			aname = request.form("D1")

			set prmrst=server.createobject("adodb.recordset")
			prmstr="select * from tblprompts where accname='" & trim(aname) & "'"
			prmrst.open prmstr,dbcon,3,3

			if prmrst.recordcount > 0 then
		%>	
			
			<tr>
        <th colspan="3" dir="ltr" width="312"><font size="2" face="Arial">Prompt</font></th>
        </tr>
		 <tr>
          	<td bgcolor="#000080" dir="ltr" align="left" style="-webkit-print-color-adjust: exact;" width="77">
            <font color="#FFFFFF" size="2" face="Arial"><Strong>Prompt</strong></font></td>
            <td align="left" bgcolor="#000080" dir="ltr" style="-webkit-print-color-adjust: exact;" width="66">
            <font color="#FFFFFF" size="2" face="Arial"><strong>Key In</strong></font></td>
            <td bgcolor="#000080" dir="ltr" align="left" style="-webkit-print-color-adjust: exact;" width="167"><font color="#FFFFFF" size="2" face="Arial"><strong>Description</strong></font></td>
          </tr>
		  
		 <% 
 			do while NOT prmrst.EOF
		    pmpt=prmrst.fields("prompt")
			kn=prmrst.fields("keyin")
			des=prmrst.fields("description")
		%>
		
			<tr>
					<td class="style1" dir="ltr" width="77"><a prompt="<%=pmpt%>"><%=pmpt%></a>&nbsp;</td>
					<td align="center" dir="ltr" width="66"><a keyin="<%=kn%>"><%=kn%></a>&nbsp;</td>
					<td class="style1" dir="ltr" width="167"><a description="<%=des%>"><%=des%></a>&nbsp;</td>

			</tr>

		<%     
				prmrst.movenext 
				loop
				prmrst.close
				Else
			%>
				
 
			<%
				end if
			%>
        </table>
        </td>
      </tr>
    </table>
    </td>
  </tr>
</table>
<br>
      <table border="1" cellpadding="0" cellspacing="0" style="border-collapse: collapse; font-family:Arial; font-size:8pt" bordercolor="#111111" width="49%">
      <tr>
        <td width="100%" colspan="4">
        <p align="center"><b><font size="2" face="Arial">Physician ID and Password</font></b></td>
      </tr>
      <tr>
			<b>
				<td bgcolor="#000080" align="left" style="-webkit-print-color-adjust: exact;">
                <font color="#FFFFFF" face="Arial" size="2"><Strong>Last Name</strong></font></td>
            <td bgcolor="#000080" align="left" style="-webkit-print-color-adjust: exact;"><font color="#FFFFFF" face="Arial" size="2"><strong>
            First Name</strong></font></td>
            <td bgcolor="#000080" align="left" style="-webkit-print-color-adjust: exact;"><strong>
            <font face="Arial" size="2" color="#FFFFFF">ID</font></strong></td>
            <td bgcolor="#000080" align="Left" style="-webkit-print-color-adjust: exact;"><font color="#FFFFFF" face="Arial" size="2"><strong>Password</strong></font></td>
			</b>
			</tr>
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
 				do while NOT achkrst.EOF
					plname=achkrst.fields("diclname")
					pfname=achkrst.fields("dicfname")
					pid=achkrst.fields("id")
					ppwd=achkrst.fields("password")
                                           
			%>

			<tr>
					<td><a diclname="<%=plname%>"><%=plname%></a>&nbsp;</td>
					<td><a dicfname="<%=pfname%>"><%=pfname%></a>&nbsp;</td>
					<td align="right"><a id="<%=pid%>"><%=pid%></a>&nbsp;</td>
					<td align="right"><a password="<%=ppwd%>"><%=ppwd%></a>&nbsp;</td>
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
		&nbsp;
		<input id="bbutton" onclick="history.go(-1)" type="button" value="Back" style="font-family: Arial; font-size: 10pt; font-weight: bold" />
	</p>

</form>
</td>
</tr> 
</table>
</body>

</html>