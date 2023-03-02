<%@ Page Language="VB" AutoEventWireup="false" CodeFile="UploadCertificate.aspx.vb" Inherits="Securefax_UploadCertificate" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="Ajax" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 1.0 Transitional/EN">
<script language="javascript" type="text/javascript">
    function Chk()
    {
        if (document.getElementById('FileUpload').value=='')
        {
            alert('Please select file to upload')
            return false;
        }
        return true;
    }
</script>
<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Upload Sertificate</title>
    <link href= "../styles/Default.css" type="text/css" rel="stylesheet"/>    
</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
    <div>
        <center>
            <asp:RegularExpressionValidator  Display="None"
             id="RegularExpressionValidator1" runat="server" 
             ErrorMessage="Only pfx file are allowed" ValidationExpression="^(([a-zA-Z]:)|(\\{2}\w+)\$?)(\\(\w[\w].*))+(.pfx)$" Font-Names="Trebuchet MS" Font-Size="12px" ControlToValidate="FileUpload">
            </asp:RegularExpressionValidator> 
        </center>
        <table id="Upload" runat="server" align=center border=1 width="80%">
            <tr class="SMSelected">
                <td>
                    Upload Certificate    
                </td>
            </tr>
            <tr>
                <td align="center">
                    <asp:FileUpload ID="FileUpload" Width="300px" runat="server" Font-Names="Trebuchet MS" Font-Size="12px"  />                        
                </td>
            </tr>
            <tr>
                <td align="center">
                    <asp:Button ID="btnSubmit" runat="server" Text="Submit" Font-Names="Trebuchet MS" Font-Size="12px" OnClientClick="javascript:return Chk();"  />
                </td>
            </tr>
        </table>
    </div>
    <asp:ValidationSummary ID="ValidationSummary1" runat="server" HeaderText="Following error occurs:" ShowMessageBox="true" DisplayMode="BulletList" ShowSummary="False" /> 
    </form>
</body>
</html>
