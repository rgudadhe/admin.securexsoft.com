
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
dim accname
dim crtby

dim achkrst
dim achkstr

dim keyn(13)
dim prompt(13)
dim des(13)
dim i

set accrst=server.createobject("adodb.recordset")
accsqlstr="select * from tblprompts"
accrst.open accsqlstr,dbcon,3,3

accname=request.form("acc")

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
keyn(10)=request.form("k11")
keyn(11)=request.form("k12")
keyn(12)=request.form("k13")

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
des(10)=request.form("d11")
des(11)=request.form("d12")
des(12)=request.form("d13")

prompt(0)=request.form("p1")
prompt(1)=request.form("p2")
prompt(2)=request.form("p3")
prompt(3)=request.form("p4")
prompt(4)=request.form("p5")
prompt(5)=request.form("p6")
prompt(6)=request.form("p7")
prompt(7)=request.form("p8")
prompt(8)=request.form("p9")
prompt(9)=request.form("p10")
prompt(10)=request.form("p11")
prompt(11)=request.form("p12")
prompt(12)=request.form("p13")

set achkrst=server.createobject("adodb.recordset")
achkstr="select * from tblprompts where accname='" & trim(accname) & "'"
achkrst.open achkstr,dbcon,3,3

if achkrst.recordcount <=0 then

i=0
While not prompt(i)="none"
accrst.Addnew
	accrst.fields("accname")=accname
	accrst.fields("prompt")=prompt(i)
	accrst.fields("keyin")=keyn(i)
	accrst.fields("Description")=des(i)
	i=i+1
Wend

accrst.update
accrst.Close
response.write("Prompt Successfully Added!")
Set accrst = Nothing


else

response.write("Prompt for this account already exists")
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