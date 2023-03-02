<!-- #include file="setdbcon.inc"-->
<html>
<%
Session("un")=request.form("txt1")
%>
<head>
</head>
<body>
<%
Dim uname
Dim userid
Dim pwd
Dim rst
Dim sqlstr 
Dim m2
Dim m3

uname = request.form("txt1")
pwd = request.form("txt2")
  
	set rst = server.createobject("adodb.recordset")
	sqlstr = "select * from tbltollfreeuser where username = '" & uname & "' and password = '" & pwd & "'"
	rst.open sqlstr,dbcon,3,3
		if rst.recordcount > 0 then
        Response.Redirect("default.html") 
		else
		response.write m3
       end if
		dbcon.close
%>
</body>