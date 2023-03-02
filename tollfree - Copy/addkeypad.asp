
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
Dim accrst
Dim accsqlstr 
dim keyname
dim crtby

dim achkrst
dim achkstr

dim keyn(12)
dim des(12)
dim i

set accrst=server.createobject("adodb.recordset")
accsqlstr="select * from tblkeypad"
accrst.open accsqlstr,dbcon,3,3

keyname=request.form("kname")
keyn(0)=request.form("k1")
keyn(1)=request.form("k2")
keyn(2)=request.form("k3")
keyn(3)=request.form("k4")
keyn(4)=request.form("k5")
keyn(5)=request.form("k6")
keyn(6)=request.form("k7")
keyn(7)=request.form("k8")
keyn(8)=request.form("k9")
keyn(9)=request.form("k10")
keyn(10)=request.form("ks")
keyn(11)=request.form("kh")

des(0)=request.form("d1")
des(1)=request.form("d2")
des(2)=request.form("d3")
des(3)=request.form("d4")
des(4)=request.form("d5")
des(5)=request.form("d6")
des(6)=request.form("d7")
des(7)=request.form("d8")
des(8)=request.form("d9")
des(9)=request.form("d10")
des(10)=request.form("ds")
des(11)=request.form("dh")

set achkrst=server.createobject("adodb.recordset")
achkstr="select * from tblkeypad where keypadname='" &trim(keyname) & "'"
achkrst.open achkstr,dbcon,3,3

if achkrst.recordcount <=0 then

for i= 0 to 11
accrst.Addnew
	accrst.fields("keyin")=keyn(i)
	accrst.fields("activity")=des(i)
	accrst.fields("keypadname")=keyname
next
accrst.update


accrst.Close
response.write("Keypad Successfully Added!")
Set accrst = Nothing


else

response.write("Keypad already exists")
achkrst.Close
Set achkrst = Nothing
accrst.Close
Set accrst = Nothing

end if

if err<>0 then
response.write("No update permissions!")
end if

%>
	
</html>