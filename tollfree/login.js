function lgin()
{
var usr;
var pass;
usr=document.loginf.uid.value;
pass=document.loginf.pass.value;
alert(usr);
alert(pass);
if((usr=="") || (pass==""))
{
alert("Enter Valid Username and Password");
document.loginf.uid.focus();
return;
}
else
{
document.loginf.method="post";
document.loginf.action="login.asp";
document.loginf.submit();
}
}