<%@ Page Language="VB" AutoEventWireup="false" CodeFile="ImportLog.aspx.vb" Inherits="FaxPlus_ImportLog" %>


<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 1.0 Transitional/EN">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Untitled Page</title>
</head>
<body>
    <form id="Form1"
      method="post"
      encType="multipart/form-data"
      runat="server">
    <div>
        <br />
        &nbsp;<input id="File1" runat="server" name="File1" type="file" />
        <br />
        <asp:Button ID="cmdUpload" runat="server" Text="Upload" /><br />
        <br />
        <asp:Label ID="lblMessage" runat="server" Font-Italic="True" Font-Names="Verdana" Font-Size="Smaller"></asp:Label><br />
    
    </div>
    </form>
</body>
</html>
