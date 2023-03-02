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
		function addprompt()
			{
			document.prompt.method="post";
			document.prompt.action="addprompt.asp";
			document.prompt.submit();
			}	
		</script>
</head>

<body id="public">

<div id="container">



<form name="prompt" class="wufoo" action="" method="">

	<div class="info">
	<h2>Add New Prompt</h2>
	<div>Missing Prompts for the accounts can be added here</div>
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
       sqlstr="select accname from tblaccounts order by accname"
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
'dbcon.close
else
response.write err
end if
%>
		</select>
		</div>
	</li>
	

	<li>
	<label class="desc">Select Keys, Prompts and Description</label>
		<span>
		<label>Key#</label>
			<select name="k1" class="field select" style="width:4em">
				<option value="none" selected>None</option>
				<option value="1">1</option>
				<option value="2">2</option>
				<option value="3">3</option>
				<option value="4">4</option>
				<option value="5">5</option>
				<option value="6">6</option>
				<option value="7">7</option>
				<option value="8">8</option>
				<option value="9">9</option>
				<option value="10">10</option>
				<option value="11">11</option>
				<option value="12">12</option>
				<option value="13">13</option>
			</select>
		
		</span>
		
		<span>
		<label>Prompts</label>
		<select name="p1" class="field select" style="width:7em">
			<option value="none" selected>None</option>
			<option value="Location">Location</option>
			<option value="MRN">MRN</option>
			<option value="Visit ID">Visit ID</option>
			<option value="Work Type">Worktype</option>
		</select>
		</span>
		
		<span>
		<label>Description</label>
				<input name="d1" class="field text large" type="text" maxlength="255">
		</span>		
	</li>
	<li>
		<span>
			<select name="k2" class="field select" style="width:4em">
				<option value="none" selected>None</option>
				<option value="1">1</option>
				<option value="2">2</option>
				<option value="3">3</option>
				<option value="4">4</option>
				<option value="5">5</option>
				<option value="6">6</option>
				<option value="7">7</option>
				<option value="8">8</option>
				<option value="9">9</option>
				<option value="10">10</option>
				<option value="11">11</option>
				<option value="12">12</option>
				<option value="13">13</option>
			</select>
		
		</span>
		
		<span>
	
		<select name="p2" class="field select" style="width:7em">
			<option value="none" selected>None</option>
			<option value="Location">Location</option>
			<option value="MRN">MRN</option>
			<option value="Visit ID">Visit ID</option>
			<option value="Work Type">Worktype</option>
		</select>
		</span>
		
		<span>
		
				<input name="d2" class="field text large" type="text" maxlength="255">
		</span>		
	</li>
	<li>
		<span>
		
			<select name="k3" class="field select" style="width:4em">
				<option value="none" selected>None</option>
				<option value="1">1</option>
				<option value="2">2</option>
				<option value="3">3</option>
				<option value="4">4</option>
				<option value="5">5</option>
				<option value="6">6</option>
				<option value="7">7</option>
				<option value="8">8</option>
				<option value="9">9</option>
				<option value="10">10</option>
				<option value="11">11</option>
				<option value="12">12</option>
				<option value="13">13</option>
			</select>
		
		</span>
		
		<span>
		
		<select name="p3" class="field select" style="width:7em">
			<option value="none" selected>None</option>
			<option value="Location">Location</option>
			<option value="MRN">MRN</option>
			<option value="Visit ID">Visit ID</option>
			<option value="Work Type">Worktype</option>
		</select>
		</span>
		
		<span>
		
				<input name="d3" class="field text large" type="text" maxlength="255">
		</span>		
	</li>
	<li>
		<span>
	
			<select name="k4" class="field select" style="width:4em">
				<option value="none" selected>None</option>
				<option value="1">1</option>
				<option value="2">2</option>
				<option value="3">3</option>
				<option value="4">4</option>
				<option value="5">5</option>
				<option value="6">6</option>
				<option value="7">7</option>
				<option value="8">8</option>
				<option value="9">9</option>
				<option value="10">10</option>
				<option value="11">11</option>
				<option value="12">12</option>
				<option value="13">13</option>
			</select>
		
		</span>
		
		<span>
		
		<select name="p4" class="field select" style="width:7em">
			<option value="none" selected>None</option>
			<option value="Location">Location</option>
			<option value="MRN">MRN</option>
			<option value="Visit ID">Visit ID</option>
			<option value="Work Type">Worktype</option>
		</select>
		</span>
		
		<span>
			<input name="d4" class="field text large" type="text" maxlength="255">
		</span>		
	</li>
	<li>
	<span>
			<select name="k5" class="field select" style="width:4em">
				<option value="none" selected>None</option>
				<option value="1">1</option>
				<option value="2">2</option>
				<option value="3">3</option>
				<option value="4">4</option>
				<option value="5">5</option>
				<option value="6">6</option>
				<option value="7">7</option>
				<option value="8">8</option>
				<option value="9">9</option>
				<option value="10">10</option>
				<option value="11">11</option>
				<option value="12">12</option>
				<option value="13">13</option>
			</select>
		
		</span>
		
		<span>
	
		<select name="p5" class="field select" style="width:7em">
			<option value="none" selected>None</option>
			<option value="Location">Location</option>
			<option value="MRN">MRN</option>
			<option value="Visit ID">Visit ID</option>
			<option value="Work Type">Worktype</option>
		</select>
		</span>
		
		<span>
			<input name="d5" class="field text large" type="text" maxlength="255">
		</span>		
	</li>
	<li>
	<span>
		
			<select name="k6" class="field select" style="width:4em">
				<option value="none" selected>None</option>
				<option value="1">1</option>
				<option value="2">2</option>
				<option value="3">3</option>
				<option value="4">4</option>
				<option value="5">5</option>
				<option value="6">6</option>
				<option value="7">7</option>
				<option value="8">8</option>
				<option value="9">9</option>
				<option value="10">10</option>
				<option value="11">11</option>
				<option value="12">12</option>
				<option value="13">13</option>
			</select>
		
		</span>
		
		<span>
		
		<select name="p6" class="field select" style="width:7em">
			<option value="none" selected>None</option>
			<option value="Location">Location</option>
			<option value="MRN">MRN</option>
			<option value="Visit ID">Visit ID</option>
			<option value="Work Type">Worktype</option>
		</select>
		</span>
		
		<span>
		
				<input name="d6" class="field text large" type="text" maxlength="255">
		</span>		
	</li>
<li>
	<span>
		
			<select name="k7" class="field select" style="width:4em">
				<option value="none" selected>None</option>
				<option value="1">1</option>
				<option value="2">2</option>
				<option value="3">3</option>
				<option value="4">4</option>
				<option value="5">5</option>
				<option value="6">6</option>
				<option value="7">7</option>
				<option value="8">8</option>
				<option value="9">9</option>
				<option value="10">10</option>
				<option value="11">11</option>
				<option value="12">12</option>
				<option value="13">13</option>
			</select>
		
		</span>
		
		<span>

		<select name="p7" class="field select" style="width:7em">
			<option value="none" selected>None</option>
			<option value="Location">Location</option>
			<option value="MRN">MRN</option>
			<option value="Visit ID">Visit ID</option>
			<option value="Work Type">Worktype</option>
		</select>
		</span>
		
		<span>
		
				<input name="d7" class="field text large" type="text" maxlength="255">
		</span>		
	</li>
	<li>
	<span>
		
			<select name="k8" class="field select" style="width:4em">
				<option value="none" selected>None</option>
				<option value="1">1</option>
				<option value="2">2</option>
				<option value="3">3</option>
				<option value="4">4</option>
				<option value="5">5</option>
				<option value="6">6</option>
				<option value="7">7</option>
				<option value="8">8</option>
				<option value="9">9</option>
				<option value="10">10</option>
				<option value="11">11</option>
				<option value="12">12</option>
				<option value="13">13</option>
			</select>
		
		</span>
		
		<span>
	
		<select name="p8" class="field select" style="width:7em">
			<option value="none" selected>None</option>
			<option value="Location">Location</option>
			<option value="MRN">MRN</option>
			<option value="Visit ID">Visit ID</option>
			<option value="Work Type">Worktype</option>
		</select>
		</span>
		
		<span>
		
				<input name="d8" class="field text large" type="text" maxlength="255">
		</span>		
	</li>
	<li>
	
		<span>
		
			<select name="k9" class="field select" style="width:4em">
				<option value="none" selected>None</option>
				<option value="1">1</option>
				<option value="2">2</option>
				<option value="3">3</option>
				<option value="4">4</option>
				<option value="5">5</option>
				<option value="6">6</option>
				<option value="7">7</option>
				<option value="8">8</option>
				<option value="9">9</option>
				<option value="10">10</option>
				<option value="11">11</option>
				<option value="12">12</option>
				<option value="13">13</option>
			</select>
		
		</span>
		
		<span>
		
		<select name="p9" class="field select" style="width:7em">
		<option value="none" selected>None</option>
			<option value="Location">Location</option>
			<option value="MRN">MRN</option>
			<option value="Visit ID">Visit ID</option>
			<option value="Work Type">Worktype</option>
		</select>
		</span>
		
		<span>
		
				<input name="d9" class="field text large" type="text" maxlength="255">
		</span>		
	</li>
<li>
	
		<span>
		
			<select name="k10" class="field select" style="width:4em">
				<option value="none" selected>None</option>
				<option value="1">1</option>
				<option value="2">2</option>
				<option value="3">3</option>
				<option value="4">4</option>
				<option value="5">5</option>
				<option value="6">6</option>
				<option value="7">7</option>
				<option value="8">8</option>
				<option value="9">9</option>
				<option value="10">10</option>
				<option value="11">11</option>
				<option value="12">12</option>
				<option value="13">13</option>
			</select>
		
		</span>
		
		<span>
		
		<select name="p10" class="field select" style="width:7em">
			<option value="none" selected>None</option>
			<option value="Location">Location</option>
			<option value="MRN">MRN</option>
			<option value="Visit ID">Visit ID</option>
			<option value="Work Type">Worktype</option>
		</select>
		</span>
		
		<span>
		
				<input name="d10" class="field text large" type="text" maxlength="255">
		</span>		
	</li>
<li>
	
		<span>
		
			<select name="k11" class="field select" style="width:4em">
				<option value="none" selected>None</option>
				<option value="1">1</option>
				<option value="2">2</option>
				<option value="3">3</option>
				<option value="4">4</option>
				<option value="5">5</option>
				<option value="6">6</option>
				<option value="7">7</option>
				<option value="8">8</option>
				<option value="9">9</option>
				<option value="10">10</option>
				<option value="11">11</option>
				<option value="12">12</option>
				<option value="13">13</option>
			</select>
		
		</span>
		
		<span>
		
		<select name="p11" class="field select" style="width:7em">
			<option value="none" selected>None</option>
			<option value="Location">Location</option>
			<option value="MRN">MRN</option>
			<option value="Visit ID">Visit ID</option>
			<option value="Work Type">Worktype</option>
		</select>
		</span>
		
		<span>
		
				<input name="d11" class="field text large" type="text" maxlength="255">
		</span>		
	</li>
	<li>
	
		<span>

			<select name="k12" class="field select" style="width:4em">
				<option value="none" selected>None</option>
				<option value="1">1</option>
				<option value="2">2</option>
				<option value="3">3</option>
				<option value="4">4</option>
				<option value="5">5</option>
				<option value="6">6</option>
				<option value="7">7</option>
				<option value="8">8</option>
				<option value="9">9</option>
				<option value="10">10</option>
				<option value="11">11</option>
				<option value="12">12</option>
				<option value="13">13</option>
			</select>
		
		</span>
		
		<span>
		
		<select name="p12" class="field select" style="width:7em">
			<option value="none" selected>None</option>
			<option value="Location">Location</option>
			<option value="MRN">MRN</option>
			<option value="Visit ID">Visit ID</option>
			<option value="Work Type">Worktype</option>
		</select>
		</span>
		
		<span>
		
				<input name="d12" class="field text large" type="text" maxlength="255">
		</span>		
	</li>
	

	

	<li class="buttons">
		<input onclick="addprompt()" class="btTxt" type="submit" value="Submit" />
	</li>
	</ul>

</form>

</div><!--container-->


</body>

</html>