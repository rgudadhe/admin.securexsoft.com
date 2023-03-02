<%
dim dbcon, dbcon1, dbpath, dbconstring
'Provider=SQLOLEDB; Driver={SQL Server}; Server=SXFDEV\SXFMAIN;Initial Catalog=Tollfree;Persist Security Info=True;User ID=tollfree;Password=try!t2009

'Data Source=sxfdev\sxfmain;Initial Catalog=tollfree;User ID=tollfree;Password=try!t2009" name="tollfree"
'dbpath = "c:\admin.securexsoft.com\tollfree\main.mdb"
'dbpath = server.mappath("main.mdb")
'dbconstring = "Provider=Microsoft.Jet.OLEDB.4.0;Password='';data source=" & dbpath

dbconstring="DRIVER={SQL Server};SERVER=SXFDEV\SXFMAIN;UID=tollfree;" & _ 
"PWD=try!t2009;DATABASE=tollfree"
set dbcon = Server.createobject("adodb.connection")
set dbcon1 = Server.createobject("adodb.connection")
dbcon.open dbconstring
dbcon1.open dbconstring
%>