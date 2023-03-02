<%
dim dbcon, dbcon1, dbpath, dbconstring
dbpath = "d:\tollfree\main.mdb"
'dbpath = server.mappath("main.mdb")
dbconstring = "Provider=Microsoft.Jet.OLEDB.4.0;Password='';data source=" & dbpath
set dbcon = Server.createobject("adodb.connection")
set dbcon1 = Server.createobject("adodb.connection")
dbcon.open dbconstring
dbcon1.open dbconstring
%>