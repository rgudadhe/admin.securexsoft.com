<!-- #include file="setdbcon.inc"-->
<html>

<head>
<meta name="GENERATOR" content="Microsoft FrontPage 5.0">
<meta name="ProgId" content="FrontPage.Editor.Document">
<meta http-equiv="Content-Type" content="text/html; charset=windows-1252">
<title>Toll Free Instructions</title>
<script type="text/javascript">
	
	function printhash()
	{
		document.inst.method="post";
		document.inst.action="printhash.asp";
		document.inst.submit();
	}
	function printnohash()
	{
		document.inst.method="post";
		document.inst.action="printnohash.asp";
		document.inst.submit();
	}
	function viewdet()
	{
		document.inst.method="post";
		document.inst.action="viewdet.asp";
		document.inst.submit();
	}

</script>
</head>

<body>
<div align="center" style="width: 1156; height: 474">
<center>
<h1><font face="Arial" size="4">Toll Free Instruction</font></h1>
<form name="inst">
 <table border="1" width="83%" height="55" bordercolordark="#C0C0C0" bordercolorlight="#C0C0C0" style="border-collapse: collapse" bordercolor="#000099" cellpadding="0" cellspacing="0">
            <tr>
                <td colspan="4" class="HeaderDiv" style="text-align: center" bgcolor="#99CCFF" height="24">
                        <b><font face="Arial" size="2">Select Account 
                        from Partitions Below</font></b></td>
            </tr>
            <tr>
                <td style="width: 25%; text-align: right; text-align: center;" height="25" colspan="2" bgcolor="#CCCCFF">
                    <b><font face="Arial" size="2">HASH Partition
                    <font color="#003366">[</font></font><font color="#003366"><font face="Arial" size="1">866-239-1729/800-385-4418</font><font face="Arial" size="2">]</font></font></b></td>
                <td style="width: 25%; text-align: right; text-align: center;" height="25" colspan="2" bgcolor="#CCCCFF">
                    <b><font face="Arial" size="2">No HASH Partition
                    <font color="#003366">[</font></font><font color="#003366"><font face="Arial" size="1">800-801-9270/866-890-5003</font><font face="Arial" size="2">]</font></font></b></td>
            </tr>

            <tr>
                <td style="width: 25%; text-align: right; text-align: right;" height="25">
                    <font face="Arial" size="2">
                    <span>Account Name&nbsp;&nbsp; </span></font></td>
                <td style="width: 25%; text-align: left;" height="25">
				<span>
                    <select size="1" name="D1" width="350" style="width: 350px; font-family:Arial; font-size:8pt">
                    <%
						dim arst
						Dim err
						dim aname
						dim ahash
							err="No Records found"
							set arst = server.createobject("adodb.recordset")
				   			sqlstr="select distinct accname from tbltollfree where ctype='HASH' order by accname "
						   	arst.open sqlstr, dbcon, 3,3
						   	arst.Movefirst()
							if arst.recordcount > 0 then
							do while NOT arst.EOF
			 				aname=arst.fields("accname")
					%>
                    <option Value="<%=aname%>"><%=aname%></option>
                    <%     
							arst.movenext 
							loop
							arst.close
							'dbcon.close
							else
										response.write err
							end if
					%>
                    </select></span></td>
                <td style="width: 25%; text-align: right; text-align: right;" height="25">
                    <font face="Arial" size="2">Account Name&nbsp;&nbsp; </font></td>
                <td style="width: 25%; text-align: left;" height="25">
                  <span>  <select size="1" name="D2" width="350" style="width: 350px;  font-family:Arial; font-size:8pt">
                    <%
						dim narst
						Dim nerr
						dim naname
						dim nahash
						nerr="No Records found"
						set narst = server.createobject("adodb.recordset")
						sqlstr="select distinct accname from tbltollfree where ctype='noHASH' order by accname "
				   		narst.open sqlstr, dbcon, 3,3
				 		narst.Movefirst()
						if narst.recordcount > 0 then
						do while NOT narst.EOF
			 			naname=narst.fields("accname")
                    %>
						<option Value="<%=naname%>"><%=naname%></option>
					<%     
						narst.movenext 
						loop
						narst.close
						'dbcon.close
						else
						response.write nerr
						end if
					%>
                    </select</span>></td>
            </tr>

            <tr>
                <td style="width: 25%; text-align: right; text-align: right;" height="25" colspan="2">
                    <p style="text-align: center">
                    <input type="button" onclick="printhash()" value="&nbsp; Print &nbsp; " name="B1" style="font-family: Trebuchet MS; font-size: 10pt; font-weight: bold"></td>
                <td style="width: 25%; text-align: right; text-align: center;" height="25" colspan="2">
                    <input type="button" onclick="printnohash()" value="&nbsp; Print &nbsp; " name="B5" style="font-family: Trebuchet MS; font-size: 10pt; font-weight: bold"></td>
            </tr>
              <tr>
                <td style="width: 25%; text-align: right; text-align: right;" height="25" colspan="4" bgcolor="#99CCFF">
                    <p style="text-align: center" dir="ltr"><b>
                    <font face="Arial" size="2">View Account wise Physician 
                    Details from Below</font></b></td>
            </tr>

              <tr>
                <td style="width: 25%; text-align: right; text-align: right;" height="25" colspan="2">
                    <font face="Arial" size="2">Account Name&nbsp;&nbsp; </font></td>
                <td style="width: 25%; text-align: right; text-align: center;" height="25" colspan="2">
                    <p style="text-align: left">
                    <select size="1" name="D3" width="350" style="width: 350px; font-family:Arial; font-size:8pt">
					<option value="all">All</option>
                    <%
						dim vrst
						Dim verr
						dim vname
						dim vhash
							verr="No Records found"
							set vrst = server.createobject("adodb.recordset")
				   			sqlstr="select distinct accname from tbltollfree order by accname"
						   	vrst.open sqlstr, dbcon, 3,3
						   	vrst.Movefirst()
							if vrst.recordcount > 0 then
							do while NOT vrst.EOF
			 				vname=vrst.fields("accname")
					%>
                    <option Value="<%=vname%>"><%=vname%></option>
                    <%     
							vrst.movenext 
							loop
							vrst.close
							dbcon.close
							else
										response.write verr
							end if
					%>
                    </select></td>
            </tr>

              <tr>
                <td style="width: 25%; text-align: right; text-align: right;" height="25" colspan="4">
                    <p style="text-align: center">
                    <input type="button" onclick="viewdet()" value="&nbsp; View &nbsp; " name="B6" style="font-family: Trebuchet MS; font-size: 10pt; font-weight: bold"></td>
            </tr>

</table>


</center>
</div>
</div>
</form>
</body>

</html>
