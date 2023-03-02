<%@ Page Language="VB" AutoEventWireup="false" CodeFile="TemplatesLinesDeductions.aspx.vb" Inherits="EditUser" %>
<%@ Register Assembly="KMobile.Web" Namespace="KMobile.Web.UI.WebControls" TagPrefix="asp" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 1.0 Transitional/EN">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
<link href= "../../App_Themes/Css/styles.css" type="text/css" rel="stylesheet" />

    <title>Untitled Page</title>
   <script type="text/javascript" language="javascript">
window.history.forward(1);
var newwindow;
function poptastic(inpt)
{
    url="photo.aspx?UserID="+ inpt;
    //alert(inpt);
    
	newwindow=window.open(url,'name','height=200,width=400, left=300, top=100');
	if (window.focus) {newwindow.focus()}
}

function Open()
{
    url="OtherInfo.aspx?UID="+document.getElementById('hdnUserID').value
    newwindow1=window.open(url,'data','height=650,width=800');
}
</script>  



</head>
<body  >
 
    <form id="form1" runat="server" >
                    <div id="body">
    <div id="cap"></div>
    <div id="main">
    <h1>Templates Units Deductions </h1>
        <div style="text-align:left">
            <asp:Label ID="lblMsg" runat="server" Text="" ForeColor="red" Font-Bold="true"></asp:Label>
        </div>                    
       <table width="100%">
           
            <tr>
                <td style="text-align: right; " class="HeaderDiv"  >
                  
                    Template
                </td>
                <td style="text-align: left; "  class="HeaderDiv" >
                    <asp:DropDownList ID="DLAct"  runat="server" AutoPostBack="true">
                    </asp:DropDownList>
                </td>
            </tr>
            </table> 
            <asp:Panel runat="server" ID="Panel1">
            <table width="100%">
             <tr>
            <td>
                &nbsp;
            </td> 
            </tr>  
            <tr>
                
               <table width="100%">
               
                      
           
            
            
             <tr> 
                <td style="text-align: right;">
                    <span>Mode</span></td>
                <td style="  text-align: left;">
                 <asp:DropDownList ID="DLMode" runat="server"      >
                       <asp:ListItem Text="Units" Value="U"></asp:ListItem> 
                 <asp:ListItem Text="Percentage" Value="P"></asp:ListItem> 
                 <asp:ListItem Text="Fixed" Value="F"></asp:ListItem> 
                 
                 </asp:DropDownList>
        </td>  
            </tr>
             <tr> 
                <td style="text-align: right;">
                    <span>Value</span></td>
                <td style="  text-align: left;">
                    <asp:TextBox ID="TXTValue" runat="server"></asp:TextBox>
        </td>  
            </tr>
            
    
            </table>
        </td> 
        <%--<tr>
            <td style="text-align: left" colspan="2">
                Please <a href="#" onclick="javascript:Open();">click here</a> to update other information.
            </td>
        </tr>--%>
        </tr> 
       
            <tr>
            
                <td style="text-align: center" colspan="2">
                      <asp:Button ID="Button4" runat="server"  Text="Submit" CssClass="button"  /></td>
               
            </tr>
          
        
        </table> 
        
        </asp:Panel>
        <br />
         <br />
          <br />
        <div style="text-align: left;">
         <asp:CompleteGridView ID="MyDataGrid" runat="server" AllowSorting="True" 
                     
                    EnableViewState="False"  
                    PageSize="25" CountFormat="Displaying records <b>{0}</b> to <b>{1}</b> of <b>{2}</b>" CaptionAlign="Bottom" AutoGenerateEditButton="False"  AutoGenerateColumns="True" ShowInsertRow="True" ShowFilter="false"  >

                <FooterStyle BackColor="#5D7B9D" ForeColor="White" Font-Bold="True"></FooterStyle>
                <EditRowStyle BackColor="#999999" HorizontalAlign="Center"></EditRowStyle>
                <PagerStyle BackColor="LightSlateGray" BorderStyle="Groove" ForeColor="White" BorderColor="#E0E0E0" HorizontalAlign="Center"></PagerStyle>
                <PagerSettings PreviousPageText="Previous" PageButtonCount="25" Mode="NumericFirstLast" LastPageText="Last Page" FirstPageText="First Page" NextPageText="Next Page"></PagerSettings>
                
                          <Columns>
                         
                          </Columns>
                </asp:CompleteGridView> 
                </div> 
        <br />
        <br />
                
    
        <asp:HiddenField ID="hdnUserID" runat="server" />
    
        <asp:Label ID="Label1" runat="server" CssClass="Title" 
            ForeColor="#400000" Height="16px" Width="496px"></asp:Label>
        
    </div> 
    </div> 
    
    </form>
    
</body>
</html>
