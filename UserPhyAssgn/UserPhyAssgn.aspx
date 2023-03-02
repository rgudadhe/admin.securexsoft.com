<%@ Page Language="VB" AutoEventWireup="false" CodeFile="UserPhyAssgn.aspx.vb" Inherits="TempPhyAssgn_TempPhyAssgn" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 1.0 Transitional/EN">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Untitled Page</title>
</head>
<body>
    <form method="post" id="form1" runat="server">
   
   <%  
       Dim prdstate As String
       Dim phystate As String
       If Request("prdstate") <> "" Then
           prdstate = Request("prdstate")
       Else
           prdstate = 0
       End If
       If Request("phystate") <> "" Then
           phystate = Request("phystate")
       Else
           phystate = 0
       End If
       
       If (Request("SubData") <> "") Then
       
       Else
           Response.Write("<TABLE border=2><TR>" & vbCrLf)
           displayProdLevelSearch(prdstate)
           displayPhySearch(phystate)
           Response.Write("</TR></TABLE>" & vbCrLf)
           If prdstate = 2 And phystate = 2 Then
               Response.Write("<INPUT TYPE=""SUBMIT"" NAME=""SubData"" VALUE=""Assign"">")
           End If
           
           
           
       End If%>
    
    <div>
      
   
     </div>
    </form>
</body>
</html>
