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
		function addphy()
			{
			document.prompt.method="post";
			document.prompt.action="addphy.asp";
			document.prompt.submit();
			}	
		</script>
</head>

<body id="public">

<div id="container">



<form name="prompt" class="wufoo" action="" method="">
<li>
</li>
	<div class="info">
	<h2>Add New Physician</h2>
	<div>New Physicians can be updated here</div>
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
			<label class="desc">Keypad</label>
					<div>
					<select class="field select addr" name="key">
					<%
							dim krst
							Dim kerr
							dim kname
							kerr="No Records found"
							set krst = server.createobject("adodb.recordset")
								   sqlstr="select distinct keypadname from tblkeypad order by keypadname"
								   krst.open sqlstr, dbcon, 3,3
								   krst.Movefirst()
									if krst.recordcount > 0 then
							do while NOT krst.EOF
							 kname=krst.fields("keypadname")
							%>
							<option><%=kname%></option>
							<%     
							krst.movenext 
							loop
							krst.close
							'dbcon.close
							else
							response.write kerr
							end if
					%>
					</select>
					</div>	
				
	</li>
	<li>
		<label class="desc">Dictator Last Name</label>
			<div>
			<input name="dlname" class="field text medium" type="text" maxlength="255" value=""/>
			</div>
	</li>
	<li>
		<label class="desc">Dictator First Name</label>
			<div>
			<input name="dfname" class="field text medium" type="text" maxlength="255" value=""/>
			</div>
	</li>
	<li>
		<label class="desc">Dictator ID</label>
			<div>
			<input name="did" class="field text medium" type="text" maxlength="255" value=""/>
			</div>
	</li>
	<li>
		<label class="desc">Dictator Password</label>
			<div>
			<input name="dpwd" class="field text medium" type="text" maxlength="255" value=""/>
			</div>
	</li>
	<li>
	<span>
		<label class="desc">Dictation System</label>
		<select name="sys" class="field select" style="width:10em">
		<option value="ChartVOX" selected>ChartVOX</option>
		</select>
	</span>
	</li>

	<li class="buttons">
		<input onclick="addphy()" class="btTxt" type="submit" value="Submit" />
	</li>
	</ul>

	
	
	
</form>

</div><!--container-->


</body>

</html>