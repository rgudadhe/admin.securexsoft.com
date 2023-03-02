
<!-- #include file="setdbcon.inc"-->
<!DOCTYPE html>
<html>
<head>

<title>

</title>

<!-- Meta Tags -->
<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />

<!-- JavaScript -->
<script type="text/javascript" src="scripts/wufoo.js"></script>

<!-- CSS -->
<link rel="stylesheet" href="css/structure.css" type="text/css" />
<link rel="stylesheet" href="css/form.css" type="text/css" />
<link rel="stylesheet" href="css/theme.css" type="text/css" />

<link rel="canonical" href="http://www.wufoo.com/gallery/designs/template.html">

</head>
<%
Dim phyrst
Dim physqlstr 
dim aname
dim kname
dim dlname
dim dfname
dim did
dim dpwd
dim dsys

dim achkrst
dim achkstr


set phyrst=server.createobject("adodb.recordset")
physqlstr="select * from tbltollfree"
phyrst.open physqlstr,dbcon,3,3

aname=request.form("acc")
kname=request.form("key")
dlname=request.form("dlname")
dfname=request.form("dfname")
did=request.form("did")
dpwd=request.form("dpwd")
dsys=request.form("sys")

phyrst.Addnew
	phyrst.fields("accname")=aname
	phyrst.fields("diclname")=dlname
	phyrst.fields("dicfname")=dfname
	phyrst.fields("system")=dsys
	phyrst.fields("keypad")=kname
	phyrst.fields("id")=did
	phyrst.fields("password")=dpwd
	
phyrst.update
phyrst.Close
response.write("Physician Successfully Updated!")
Set phyrst = Nothing

if err<>0 then
response.write("No update permissions!")
end if

%>
	
</html>