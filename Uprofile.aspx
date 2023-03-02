<%@ Page Language="VB" AutoEventWireup="false" CodeFile="Uprofile.aspx.vb" Inherits="Uprofile" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 1.0 Transitional/EN">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
<link href="images/style.css" rel="stylesheet" type="text/css" />
    <title>Untitled Page</title>
   <script type="text/javascript" language="javascript">
var newwindow;
function popupPage()
{
    url="Uphoto.aspx";
    //alert(inpt);
    
	newwindow=window.open(url,'name','height=200,width=400, left=300, top=100');
	if (window.focus) {newwindow.focus()}
}

</script>   
   <script type="text/javascript"  language="JavaScript">

function poptastic()
{
    url="ChangePass.aspx";
    //alert(inpt);
    newwindow=window.open(url,'name','height=250,width=400, left=300, top=100');
	if (window.focus) {newwindow.focus()}
}

</script> 
</head>
<body>

<!-- wrap starts here -->
<div id="wrap" style="left: 0px; width: 696px; top: 0px">

				
			
	<!-- content-wrap starts -->
	<div id="content-wrap" class="three-col" style="width: 688px"  >	
	
		
		
		<div id="sidebar" style="text-align: center">
			
			 <table border="2" cellpadding="2" style="margin-top:50px; margin-left:20px;"  >
                        <tr>
                            <td style="background-color: #336699">
                                    <asp:Label ID="LblName" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="Small" ForeColor="White"></asp:Label>
                             </td> 
                        </tr>
                        <tr>
                            <td style="background-color: gainsboro;" >
                                    <asp:Image ID="Image1" runat="server" Height="100" Width="100" BorderWidth="2" BorderStyle="Groove"   />
                            </td>
                        </tr>
                     </table> 
				<asp:Image ID="Image2" runat="server" ImageUrl="~/images/editphoto.JPG" />
		<!-- sidebar ends -->		
		</div>
		
		
		<div id="main">
				
			
			  <form runat="server"  >
			  <table width="100%"><tr><td style="height: 30px"><asp:Button ID="Button2" CssClass="button"  runat="server" Text="Edit Profile" /></td><td style="width: 188px; height: 30px; text-align: right;"></td></tr></table>
                  
                 
			                   
            <h1>
                <img alt="" width="47" height="43" src="images/profile.jpg" />
                  Profile</h1>
            <table style="color: #ff9933" width="100%">
           
            <tr>
                <td style=" text-align: right; text-align: right; ">
                    <span style="font-size: 8pt; font-family: 'Arial', Cursive">
                    First Name</span></td>
                <td style=" text-align: left; ">
                    <asp:textbox ID="TxtFirstName" runat="server" Font-Names="Arial" ForeColor="ControlDarkDark" width="200px"></asp:textbox></td>
                    </tr>
            <tr>
                <td style=" text-align: right; text-align: right; ">
                    <span style="font-size: 8pt; font-family: 'Arial', Cursive">
                    Last Name</span></td>
                <td style=" text-align: left; ">
                    <asp:textbox ID="TxtLastName" runat="server" Font-Names="Arial" ForeColor="ControlDarkDark" width="200px"></asp:textbox></td>
            </tr>
            <tr>
                <td style=" text-align: right; text-align: right; ">
                    <span style="font-size: 8pt; font-family: 'Arial', Cursive">
                    E-Mail</span></td>
                <td style=" height: 21px; text-align: left; ">
                    <asp:textbox ID="TxtEmail" runat="server" Font-Names="Arial" ForeColor="ControlDarkDark" width="200px"></asp:textbox></td>
                    </tr>
                    <tr>
                <td style=" text-align: right; text-align: right; ">
                <span style="font-size: 8pt; font-family: 'Arial', Cursive">
                    Date Of Birth</span></td>
                <td style=" height: 26px; text-align: left; ">
                    <asp:textbox ID="TxtDOB" runat="server" Font-Names="Arial" ForeColor="ControlDarkDark" width="200px"></asp:textbox></td>
                    </tr>
                      <tr>
                <td style=" text-align: right; text-align: right; ">
                <span style="font-size: 8pt; font-family: 'Arial', Cursive">
                    Joining Date</span></td>
                <td style=" text-align: left; text-align: left; ">
                    <asp:textbox ID="TxtJoin" runat="server" Font-Names="Arial" ForeColor="ControlDarkDark" width="200px"></asp:textbox></td>
            </tr>
             <tr>
                <td style=" text-align: right; height: 26px; text-align: right; ">
                <span style="font-size: 8pt; font-family: 'Arial', Cursive">
                    Department</span></td>
                <td style=" height: 26px; text-align: left; ">
                    <asp:textbox ID="TxtDept" runat="server" Font-Names="Arial" ForeColor="ControlDarkDark" width="200px"></asp:textbox></td>
            </tr>
            <tr>
                <td style=" text-align: right; text-align: right; ">
                <span style="font-size: 8pt; font-family: 'Arial', Cursive">
                    Designation</span></td>
                <td style=" text-align: left; text-align: left; ">
                    <asp:textbox ID="TxtDesn" runat="server" Font-Names="Arial" ForeColor="ControlDarkDark" width="200px"></asp:textbox></td>
                  </tr>
            <tr> 
                <td style="text-align: right;  height: 21px;">
                    <span style="font-size: 8pt; font-family: Arial">Category</span></td>
                <td style="  text-align: left;  height: 21px;">
                    <asp:textbox ID="TxtCategory" runat="server" Font-Names="Arial" ForeColor="ControlDarkDark" width="200px"></asp:textbox></td>
            </tr>
            <tr>
                <td style=" height: 21px; text-align: right; ">
                    <span style="font-size: 8pt; font-family: 'Arial', Cursive">
                    Chat ID</span></td>
                <td style=" height: 21px; text-align: left; ">
                    <asp:textbox ID="TxtChatID" runat="server" Font-Names="Arial" ForeColor="ControlDarkDark" width="200px"></asp:textbox></td>
            </tr>
            <tr>
                <td style=" text-align: right; text-align: right; ">
                <span style="font-size: 8pt; font-family: 'Arial', Cursive">
                    Other E-Mail</span></td>
                <td style=" text-align: left;">
                    <asp:textbox ID="TxtNonOEmail" runat="server" Font-Names="Arial" ForeColor="ControlDarkDark" width="200px"></asp:textbox></td>
                    </tr>
          
                 <tr>
                <td style=" height: 21px; text-align: right; height: 30px; ">
                <span style="font-size: 8pt; font-family: 'Arial', Cursive">
                    Current Address</span>        </td>
                <td style=" height: 21px; height: 30px; text-align: left; ">
                    <asp:textbox ID="TxtAdd" runat="server" Font-Names="Arial" ForeColor="ControlDarkDark" width="200px"></asp:textbox></td>
            </tr>
       
            <tr>
                <td style=" text-align: right; text-align: right; ">
                <span style="font-size: 8pt; font-family: 'Arial', Cursive">
                    City</span></td>
                <td style=" height: 22px; text-align: left; ">
                    <asp:textbox ID="TxtCity" runat="server" Font-Names="Arial" ForeColor="ControlDarkDark" width="200px"></asp:textbox></td>
                    </tr>
            <tr>
                <td style=" text-align: right; height: 30px; text-align: right; ">
                <span style="font-size: 8pt; font-family: 'Arial', Cursive">
                    State
                    </span></td>
                <td style=" height: 21px; height: 30px; text-align: left; ">
                    <asp:textbox ID="TxtState" runat="server" Font-Names="Arial" ForeColor="ControlDarkDark" width="200px"></asp:textbox></td>
                    </tr>
                         <tr>
                <td style=" height: 22px; text-align: right; ">
                    <span style="font-size: 8pt; font-family: 'Arial', Cursive">
                    Country</span></td>
                <td style=" height: 22px; text-align: left; ">
                    <asp:textbox ID="TxtCountry" runat="server" Font-Names="Arial" ForeColor="ControlDarkDark" width="200px"></asp:textbox></td>
            </tr>
            
                 <tr>
                <td style=" height: 21px; text-align: right; height: 30px; ">
                <span style="font-size: 8pt; font-family: 'Arial', Cursive">
                    Permanent Address</span>        </td>
                <td style=" height: 21px; height: 30px; text-align: left; ">
                    <asp:textbox ID="TxtpAdd" runat="server" Font-Names="Arial" ForeColor="ControlDarkDark" width="200px"></asp:textbox></td>
            </tr>
       
            <tr>
                <td style=" text-align: right; text-align: right; ">
                <span style="font-size: 8pt; font-family: 'Arial', Cursive">
                    City</span></td>
                <td style=" height: 22px; text-align: left; ">
                    <asp:textbox ID="TxtpCity" runat="server" Font-Names="Arial" ForeColor="ControlDarkDark" width="200px"></asp:textbox></td>
                    </tr>
            <tr>
                <td style=" text-align: right; height: 30px; text-align: right; ">
                <span style="font-size: 8pt; font-family: 'Arial', Cursive">
                    State
                    </span></td>
                <td style=" height: 21px; height: 30px; text-align: left; ">
                    <asp:textbox ID="TxtpState" runat="server" Font-Names="Arial" ForeColor="ControlDarkDark" width="200px"></asp:textbox></td>
                    </tr>
                         <tr>
                <td style=" height: 22px; text-align: right; ">
                    <span style="font-size: 8pt; font-family: 'Arial', Cursive">
                    Country</span></td>
                <td style=" height: 22px; text-align: left; ">
                    <asp:textbox ID="TxtpCountry" runat="server" Font-Names="Arial" ForeColor="ControlDarkDark" width="200px"></asp:textbox></td>
            </tr>
       
            
            <tr>
                <td style=" height: 26px; text-align: right; ">
                <span style="font-size: 8pt; font-family: 'Arial', Cursive">
                    Tel#</span></td>
                <td style=" height: 26px; text-align: left; ">
                    <asp:textbox ID="TxtTel" runat="server" Font-Names="Arial" ForeColor="ControlDarkDark" width="200px"></asp:textbox></td>
            </tr>
            <tr>
                <td style=" text-align: right; text-align: right; ">
                <span style="font-size: 8pt; font-family: 'Arial', Cursive">
                    Cell#</span></td>
                <td style=" text-align: left; height: 26px; text-align: left; ">
                    <asp:textbox ID="TxtCell" runat="server" Font-Names="Arial" ForeColor="ControlDarkDark" width="200px"></asp:textbox></td>
                    </tr>
                     <tr>
                     <td style=" text-align: center; height: 26px; " colspan="2" >
                         <asp:Button ID="btnSubmit" CssClass="button"  runat="server" Text="Update" />
                     </td>
                     </tr>
           
            </table>
     				 </form>		
				
		</div>
		
	<!-- content-wrap ends-->	
	</div>
		
	<!-- footer starts -->			
	<div id="footer-wrap" style="width: 720px"></div>
	<!-- footer ends-->	
	
<!-- wrap ends here -->
</div>

</body>
</html>
