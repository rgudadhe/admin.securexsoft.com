<%@ Page Language="VB" AutoEventWireup="false" CodeFile="UserProfile.aspx.vb" Inherits="EditUser" %>

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
    <h1>View\Edit User Details </h1>
        <div style="text-align:left">
            <asp:Label ID="lblMsg" runat="server" Text="" ForeColor="red" Font-Bold="true"></asp:Label>
        </div>                    
       <table width="100%">
           
            <tr>
                <td style="text-align: right; " class="HeaderDiv"  >
                  
                    Username 
                </td>
                <td style="text-align: left; "  class="HeaderDiv" >
                    <asp:DropDownList ID="DropDownList1"  runat="server" AutoPostBack="true">
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
                <td style="vertical-align:top;"  >
                    <table border="2" cellpadding="2"  width="50px">
                        <tr>
                            <td style="background-color: #336699">
                                    <asp:Label ID="LblName" runat="server" Font-Bold="True"  Font-Size="Small" ForeColor="White"></asp:Label>
                             </td> 
                        </tr>
                        <tr>
                            <td style="background-color: gainsboro;" >
                                    <asp:Image ID="Image1" runat="server" Height="100" Width="100" BorderWidth="2" BorderStyle="Groove"   />
                            </td>
                        </tr>
                     </table> 
                    <asp:Image ID="Image2" runat="server" ImageUrl="~/images/editphoto.JPG" /></td>
               <td>
               <table width="100%">
               <tr>
                <td style=" text-align: center;" class="alt1" colspan="2"   >
                    User Details</td>
                    </tr>
                      
           
            
            
             <tr> 
                <td style="text-align: right;">
                    <span>Mode</span></td>
                <td style="  text-align: left;">
                 <asp:DropDownList ID="DLMode" runat="server"      >
                       <asp:ListItem Text="Employee" Value="EMPLOYEE"></asp:ListItem> 
                 <asp:ListItem Text="Contractor" Value="HBA"></asp:ListItem> 
                 
                 </asp:DropDownList>
        </td>  
            </tr>
             <tr> 
                <td style="text-align: right;">
                    <span>PlatForm</span></td>
                <td style="  text-align: left;">
                 <asp:DropDownList ID="DLPlatForm" runat="server"      >
                 </asp:DropDownList>
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
                
    
        <asp:HiddenField ID="hdnUserID" runat="server" />
        <asp:HiddenField ID="HRecFound" runat="server" />
    
        <asp:Label ID="Label1" runat="server" CssClass="Title" 
            ForeColor="#400000" Height="16px" Width="496px"></asp:Label>
        
    </div> 
    </div> 
    
    </form>
    
</body>
</html>
