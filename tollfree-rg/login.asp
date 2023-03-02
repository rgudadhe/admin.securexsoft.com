
<html>
<head>
<title>Login Page</title>
<script type="text/javascript">
function lgin()
{
document.loginf.method="post";
document.loginf.action="login1.asp";
document.loginf.submit();
}
</script>
</head>
<body>
<center>
<form name="loginf">

<p class="main" align="center">&nbsp;</p>

<p class="main" align="center">&nbsp;</p>

<p class="main" align="center">&nbsp;</p>

<p class="main" align="center">
<img border="0" src="logo.jpg" width="374" height="123"></p>

<div align="center">
  <center>
  <table border="0" cellpadding="0" cellspacing="0" style="border-collapse: collapse" bordercolor="#111111" width="40%" height="92">
    <tr>
      <td width="100%" colspan="2" height="23" bgcolor="#000066">
      <p align="center"><b><font face="Arial" color="#FFFFFF">Enter Login details below</font></b></td>
    </tr>
    <tr>
      <td width="50%" height="24" bgcolor="#6699FF">
      <p align="right"><font face="Arial" size="2">Username&nbsp;&nbsp; </font></td>
      <td width="50%" height="24" bgcolor="#6699FF"><font face="Arial"><input type="text" value="" name="txt1" size="20"></font></td>
    </tr>
    <tr>
      <td width="50%" height="24" bgcolor="#6699FF">
      <p align="right"><font face="Arial" size="2">Password&nbsp;&nbsp; </font></td>
      <td width="50%" height="24" bgcolor="#6699FF"><font face="Arial"><input type="password" value="" name="txt2" size="20"></font></td>
    </tr>
    <tr>
      <td width="50%" height="24" bgcolor="#99CCFF">
      <p align="right"><font face="Arial">
      <input type="button" value="Login" name="B1" size="20" onclick="lgin()" style="font-family: Arial; font-size: 10pt">&nbsp; </font></td>
      <td width="50%" height="24" bgcolor="#99CCFF"><font face="Arial">
      &nbsp;<input type="reset" value="Reset" name="B2" size="20" onclick="lgin()" style="font-family: Arial; font-size: 10pt"></font></td>
    </tr>
  </table>
  </center>
</div>
</form>
</center>
</body>
</html>