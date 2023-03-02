<%@ Page Language="VB" AutoEventWireup="false" CodeFile="MTVHSData.aspx.vb" Inherits="Transcend_MTVHSData" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>MTVHSData</title>
    <link href= "../App_Themes/Css/Main.css" type="text/css" rel="stylesheet" />
    <link href="../App_Themes/Css/Common.css" type="text/css" rel="stylesheet"  />
    <link href= "../App_Themes/Css/Styles.css" type="text/css" rel="stylesheet" />
    <link href= "../App_Themes/Css/Calendar.css" type="text/css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        <div id="body">
    <div id="cap"></div>
    <div id="main">
    <h1>Upload MTVHS Data</h1>
    <div>
        <asp:Panel ID="Panel2" runat="server" width="100%">
           <table width="100%">
             <tr>
                <td colspan="2" style="text-align: center" class="HeaderDiv">
                    Upload Data
                </td>
            </tr>
          </table> 
        </asp:Panel>
        <asp:Panel ID="Panel1" runat="server" width="100%">
            <center>
            <table style="text-align:center " width="80%" border="0" >
                <tr>
                    <td align="left" style="border:0">
                        <a href="https://secureit.edictate.com/ets_files/Transcend/MTVHSTemplate.xls" class="common" target="_blank">Download Template</a>
                    </td>
                    <td align="right" style="border:0">
                        <asp:Label ID="ErrLabel" runat="server" Text="" CssClass="common"  Font-Italic="true" ForeColor="red"></asp:Label>                        
                    </td>
                </tr>
                <tr>
                    <td align="right" style="border:0">
                        Select File
                    </td>
                    <td align="left" style="border:0">
                        <asp:FileUpload ID="FileUpload" runat="server" CssClass="common" Width="350" />
                    </td>
                </tr>
                <tr>
                    <td colspan="2" align="center" style="border:0">
                        <center>
                            <asp:Button ID="btnUpload" runat="server" Text="Upload Data" cssClass="button" />    
                        </center>
                    </td>
                </tr>
            </table>
            </center>
        </asp:Panel> 
        
        <table width="100%">
            <tr>
                <td align="left" style="border:0">
                    <asp:Label ID="lblResponse" runat="server" Text="" CssClass="common" ForeColor="red" Font-Bold="true" Font-Italic="true"></asp:Label>
                </td>
            </tr>
        </table>
    </div>
    </div> 
    </div> 
    </form>
</body>
</html>
