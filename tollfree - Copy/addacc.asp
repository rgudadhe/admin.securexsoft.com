
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
dim accnm
dim crtby

dim achkrst
dim achkstr


set accrst=server.createobject("adodb.recordset")
accsqlstr="select * from tblaccounts"
accrst.open accsqlstr,dbcon,3,3

accnm=request.form("name")


set achkrst=server.createobject("adodb.recordset")
achkstr="select * from tblaccounts where accname='" &trim(accnm) & "'"
achkrst.open achkstr,dbcon,3,3

if achkrst.recordcount <=0 then

accrst.Addnew
	accrst.fields("accname")=accnm
accrst.update

accrst.Close
response.write("Account Successfully Added!")
Set accrst = Nothing


else

response.write("Account already exists")
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