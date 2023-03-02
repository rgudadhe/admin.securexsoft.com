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
printbutton.style.visibility='hidden';
window.print();
printbutton.style.visibility='visible';
}
</script>

</head>

<body>
<center>
<form>
	<div>
	<h2 align="center"><font face="Arial">Account-wise Physician Details</font></h2>
	
	<% if (request.form("D3")="all") then
	%>
			<h3 align="center"><font face="Arial"><U>All Accounts</U></font></h3>
		<%	else %>
			<h3 align="center"><font face="Arial"><U><%=request.form("D3")%></U></font></h3>
	<% end if %>
	</div>
      <table border="1" cellpadding="0" cellspacing="0" style="border-collapse: collapse; font-family:Arial; font-size:8pt" bordercolor="#111111" width="70%" height="48">
      <tr>
        <td colspan="6" height="16" width="923">
        <p align="center"><b><font size="2" face="Arial">Dictator Toll Free Details</font></b></td>
      </tr>
      <tr>
			<b>
				<td bgcolor="#000080" align="left" width="153" height="16" style="-webkit-print-color-adjust: exact;">
                <strong><font size="2" color="#FFFFFF">Account Name</font></strong></td>
            <td bgcolor="#000080" align="left" width="154" height="16" style="-webkit-print-color-adjust: exact;"><strong>
            <font size="2" color="#FFFFFF">Last Name</font></strong></td>
            <td bgcolor="#000080" align="left" width="154" height="16" style="-webkit-print-color-adjust: exact;"><font color="#FFFFFF" face="Arial" size="2"><strong>
            First Name</strong></font></td>
            <td bgcolor="#000080" align="left" width="154" height="16" style="-webkit-print-color-adjust: exact;"><strong>
            <font size="2" color="#FFFFFF">Keypad</font></strong></td>
            <td bgcolor="#000080" align="left" width="154" height="16" style="-webkit-print-color-adjust: exact;"><strong>
            <font size="2" color="#FFFFFF">ID</font></strong></td>
            <td bgcolor="#000080" align="left" width="154" height="16" style="-webkit-print-color-adjust: exact;"><font color="#FFFFFF" face="Arial" size="2"><strong>Password</strong></font></td>
			</b>
			</tr>
			<%
					Dim prmrst
					Dim prmstr 
					dim aname
					dim accname
					dim dlname
					dim dfname
					dim dkeypad
					dim did
					dim dpwd

					m2="No records found"
					aname = request.form("d3")
					set prmrst=server.createobject("adodb.recordset")
					if aname="all" then
					prmstr="select * from tbltollfree order by accname"
					else
					prmstr="select * from tbltollfree where accname='" & trim(aname) & "'"
					end if
					prmrst.open prmstr,dbcon,3,3

					if prmrst.recordcount > 0 then
					do while NOT prmrst.EOF

				    accname=prmrst.fields("accname")
					dlname=prmrst.fields("diclname")
					dfname=prmrst.fields("dicfname")
					dkeypad=prmrst.fields("keypad")
					did=prmrst.fields("id")
					dpwd=prmrst.fields("password")                                           
			%>

			<tr>
					<td height="16" width="153"><a accname="<%=accname%>"><%=accname%></a></td>
					<td height="16" width="154"><a diclname="<%=dlname%>"><%=dlname%></a></td>
					<td height="16" width="154"><a dicfname="<%=dfname%>"><%=dfname%></a></td>
					<td height="16" width="154"><a keypad="<%=dkeypad%>"><%=dkeypad%></a></td>
					<td align="right" height="16" width="154"><a id="<%=did%>"><%=did%></a></td>
					<td align="right" height="16" width="154"><a password="<%=dpwd%>"><%=dpwd%></a></td>

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
		<input id="printpagebutton" onclick="printpage()" class="btTxt" type="button" value="Print this page" style="font-family: Arial; font-size: 10pt; font-weight: bold" />
		&nbsp;
		<input id="bbutton" onclick="history.go(-1)" type="button" value="Back" style="font-family: Arial; font-size: 10pt; font-weight: bold" />
	</p>

</form> 
</center>
</body>

</html>