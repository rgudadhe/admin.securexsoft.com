<!-- #include file="setdbcon.inc"-->
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN"
"http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head>

<title>

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
<script type="text/javascript" src="scripts/wufoo.js"></script>

<!-- CSS -->
<link rel="stylesheet" href="css/structure.css" type="text/css" />
<link rel="stylesheet" href="css/form.css" type="text/css" />
<link rel="stylesheet" href="css/theme.css" type="text/css" />

<link rel="canonical" href="http://www.wufoo.com/gallery/designs/template.html">
<script langauge="javascript">
		function inst()
			{
			document.ai.method="post";
			document.ai.action="vinst.asp";
			document.ai.submit();
			}	
		</script>
</head>

<body id="public">

<div id="container">



<form name="ai" class="wufoo" action="" method="">

	<div class="info">
	<h2>View Accountview Physician Details</h2>
	<div>Accountiwse Physician details can be viewed from this location.</div>
	</div>

	<ul>

	<li>
	<label class="desc">Account Name</label>
		<div>
		<select class="field select addr" name="acc">
		<%
dim arst
Dim err
dim aname

err="No Records found"
set arst = server.createobject("adodb.recordset")
       sqlstr="select distinct accname from tbltollfree order by accname"
       arst.open sqlstr, dbcon, 3,3
       arst.Movefirst()
        if arst.recordcount > 0 then
do while NOT arst.EOF
 aname=arst.fields("accname")
%>
<option><%=aname%></option>
<%     
arst.movenext 
loop
arst.close
dbcon.close
else
response.write err
end if
%>
		</select>
		</div>
	</li>

	<li class="buttons">
		<input onclick="inst()" class="btTxt" type="submit" value="Submit" />
	</li>
	</ul>

</form>

</div><!--container-->


</body>

</html>