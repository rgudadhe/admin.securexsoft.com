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
</script>

<!-- CSS -->
<link rel="stylesheet" href="css/structure.css" type="text/css" />
<link rel="stylesheet" href="css/form.css" type="text/css" />
<link rel="stylesheet" href="css/theme.css" type="text/css" />

<link rel="canonical" href="http://www.wufoo.com/gallery/designs/template.html">

</head>

<body id="public">

<div id="container">

<form class="wufoo" action="" method="">

	<div class="info">
	
	<h2 align="center">Accountwise Physician Details</h2>
	</div>

	<ul>
	
	<li>
	<div>
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
prmstr="select * from tbltollfree where accname='" & trim(aname) & "'"
prmrst.open prmstr,dbcon,3,3


    
 if prmrst.recordcount > 0 then
 
 %>
 <table border="1" width="600">
<tr>
<b>
<td class="style2"><Strong>Account Name</strong></td><td class="style1"><strong>Dictator LName</strong></td>
	<td class="style1"><strong>Dictator FName</strong></td><td class="style1"><strong>Keypad</strong></td>
	<td class="style1"><strong>Dictator ID</strong></td><td class="style1"><strong>Dictator Password</strong></td>
</b>
</tr>
<%
do while NOT prmrst.EOF

    accname=prmrst.fields("accname")
	dlname=prmrst.fields("diclname")
	dfname=prmrst.fields("dicfname")
	dkeypad=prmrst.fields("keypad")
	did=prmrst.fields("id")
	dpwd=prmrst.fields("password")
	
                                           
%>

<tr>
<td class="style1"><a accname="<%=accname%>"><%=accname%></a></td>
<td class="style1"><a diclname="<%=dlname%>"><%=dlname%></a></td>
<td class="style1"><a dicfname="<%=dfname%>"><%=dfname%></a></td>
<td class="style1"><a keypad="<%=dkeypad%>"><%=dkeypad%></a></td>
<td class="style1"><a id="<%=did%>"><%=did%></a></td>
<td class="style1"><a password="<%=dpwd%>"><%=dpwd%></a></td>

</tr>

<%     
prmrst.movenext 
loop
prmrst.close
Else 
response.write m2 
end if%>
</table>
</div>	
</li>
	

</form>


</div><!--container-->


</body>

</html>