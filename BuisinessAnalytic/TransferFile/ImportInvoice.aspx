<%@ Page Language="VB" AutoEventWireup="false" CodeFile="ImportInvoice.aspx.vb" Inherits="ImportInvoice" %>

<%@ Register Assembly="System.Web.Extensions, Version=1.0.61025.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI" TagPrefix="asp" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<%@ Register
    Assembly="AjaxControlToolkit"
    Namespace="AjaxControlToolkit"
    TagPrefix="ajaxToolkit" %>
    
<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>Import Invoice</title>
    <link href= "../../App_Themes/Css/styles.css" type="text/css" rel="stylesheet"/>
    <link href= "../../App_Themes/Css/Common.css" type="text/css" rel="stylesheet"/>

</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager> 
        <div id="body">
        <div id="cap"></div>
        <div id="main">
        <h1>Import Invoice</h1>
        <asp:Panel ID="Panel1" runat="server" HorizontalAlign="Left">
            <div>
        <table id="MainTable" width="40%">
        <tr  class="tblbg">
                <td  valign="top" colspan ="2" class="alt1" >
                    Click on Browse button and select the file.
                </td>
               </tr>
                
            <tr>
                <td  valign="top">
                        <asp:FileUpload ID="FileUpload1" runat="server" />
        
           </td>
            </tr>
        
        <tr  class="tblbg">
                <td  valign="top" style="text-align: center">
                    &nbsp;<asp:Button ID="Button1" runat="server" Text="Submit" CssClass="button" /></td>
            </tr>
            <tr>
                <td colspan="2" style="text-align: center; border:0">
                <p>
        <asp:Label ID="Label2" runat="server" CssClass="common" ></asp:Label></p>
                    </td>
            </tr>
           
        </table>
        <br />
        
        
        <asp:Label ID="Label1" runat="server" CssClass="Title" ></asp:Label>&nbsp;<br />
        <br />
        <asp:Table ID="TblDetails" runat="server" CssClass="common" >
        <asp:TableRow>
        <asp:TableCell CssClass="alt1">Acount</asp:TableCell> 
        <asp:TableCell CssClass="alt1">Number</asp:TableCell> 
        <asp:TableCell CssClass="alt1">TYPE</asp:TableCell> 
        <asp:TableCell CssClass="alt1">DATE</asp:TableCell> 
        <asp:TableCell CssClass="alt1">Ref #</asp:TableCell> 
        <asp:TableCell CssClass="alt1">AMOUNT</asp:TableCell> 
        <asp:TableCell CssClass="alt1">COMMENTS</asp:TableCell> 
        <asp:TableCell CssClass="alt1">MONTH</asp:TableCell> 
        <asp:TableCell CssClass="alt1">YEAR</asp:TableCell> 
        <asp:TableCell CssClass="alt1">CYCLE</asp:TableCell> 
        <asp:TableCell CssClass="alt1">STATUS</asp:TableCell> 
        
        </asp:TableRow>
        
        </asp:Table>
     
        <br />
        <asp:Label ID="DispBox" runat="server" CssClass="Title" ForeColor="#C00000"></asp:Label>
            
        </div>
        <br />
         <a href="Account Activity - MM DD YYYY.xls"><span style="color: #ff0000;" class="common">
            <strong>Click here</strong></span></a><span style="color: #ff0000"><span class="common"><strong>
                to download template</strong></span> </span>
               <asp:HiddenField ID="GrpActState" runat="server" />
        <asp:HiddenField ID="ActState" runat="server" />
        <asp:HiddenField ID="HActID" runat="server" />
        <asp:HiddenField ID="HFoldName" runat="server" />
        <asp:HiddenField ID="HDictID" runat="server" />
        <asp:HiddenField ID="hUname" runat="server" />
        <asp:HiddenField ID="TotAct" runat="server" />
        <asp:HiddenField ID="TotLvl" runat="server" />
        <asp:HiddenField ID="DemoFieldText" runat="server" />
        <asp:HiddenField ID="DemoFieldValue" runat="server" />
        <asp:HiddenField ID="HDictCode" runat="server" /><asp:HiddenField ID="HLocAcc" runat="server" />
        </asp:Panel>
           </div> 
           </div> 
            
    
        
    </form>
</body>
</html>
