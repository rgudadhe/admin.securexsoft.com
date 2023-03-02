<%@ Page Language="VB" AutoEventWireup="false" CodeFile="profile.aspx.vb" Inherits="testets_profile" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 1.0 Transitional/EN">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
<link href="images/style.css" rel="stylesheet" type="text/css" />
    <title>Untitled Page</title>
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
<div id="wrap">

				
			
	<!-- content-wrap starts -->
	<div id="content-wrap" class="three-col"  >	
	
		
		
		<div id="sidebar">
			
			 <table border="2" cellpadding="2" style="margin-top:50px; margin-left:20px;"  >
                        <tr>
                            <td style="background-color: #336699">
                                    <asp:Label ID="LblName" runat="server" Font-Bold="True" Font-Names="Trebuchet MS" Font-Size="Small" ForeColor="White"></asp:Label>
                             </td> 
                        </tr>
                        <tr>
                            <td style="background-color: gainsboro;" >
                                    <asp:Image ID="Image1" runat="server" Height="100" Width="100" BorderWidth="2" BorderStyle="Groove"   />
                            </td>
                        </tr>
                     </table> 
				
		<!-- sidebar ends -->		
		</div>
		
		
		<div id="main">
				
			
			  <form runat="server"  >
                  &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                  &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                  &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                  &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp;
                  <asp:Panel ID="pnl" runat="server" HorizontalAlign="Right"   >
                  <input id="Button1" type="button" value="Change Password" onclick="poptastic()"  />
                  </asp:Panel> 
			                    </form>
            <h1>
                <img alt="" width="47" height="43" src="images/profile.jpg" />
                  Profile</h1>
            <table style="color: #ff9933" >
           
            <tr>
                <td style=" text-align: right; text-align: right; ">
                    <span style="font-size: 10pt; font-family: 'Trebuchet MS', Cursive">
                    First Name</span></td>
                <td style=" text-align: left; ">
                    <asp:Label ID="TxtFirstName" runat="server" Font-Names="Trebuchet MS" ForeColor="ControlDarkDark" ></asp:Label></td>
                    </tr>
            <tr>
                <td style=" text-align: right; text-align: right; ">
                    <span style="font-size: 10pt; font-family: 'Trebuchet MS', Cursive">
                    Last Name</span></td>
                <td style=" text-align: left; ">
                    <asp:Label ID="TxtLastName" runat="server" Font-Names="Trebuchet MS" ForeColor="ControlDarkDark" ></asp:Label></td>
            </tr>
            <tr>
                <td style=" text-align: right; text-align: right; ">
                    <span style="font-size: 10pt; font-family: 'Trebuchet MS', Cursive">
                    E-Dictate Mail ID</span></td>
                <td style=" height: 21px; text-align: left; ">
                    <asp:Label ID="TxtEmail" runat="server" Font-Names="Trebuchet MS" ForeColor="ControlDarkDark" ></asp:Label></td>
                    </tr>
            <tr>
                <td style=" height: 21px; text-align: right; ">
                    <span style="font-size: 10pt; font-family: 'Trebuchet MS', Cursive">
                    Yahoo Chat ID</span></td>
                <td style=" height: 21px; text-align: left; ">
                    <asp:Label ID="TxtChatID" runat="server" Font-Names="Trebuchet MS" ForeColor="ControlDarkDark" ></asp:Label></td>
            </tr>
            <tr>
                <td style=" text-align: right; text-align: right; ">
                <span style="font-size: 10pt; font-family: 'Trebuchet MS', Cursive">
                    Non E-Dictate Mail ID</span></td>
                <td style=" text-align: left;">
                    <asp:Label ID="TxtNonOEmail" runat="server" Font-Names="Trebuchet MS" ForeColor="ControlDarkDark" ></asp:Label></td>
                    </tr>
            <tr>
                <td style=" text-align: right; text-align: right; ">
                <span style="font-size: 10pt; font-family: 'Trebuchet MS', Cursive">
                    Joining Date</span></td>
                <td style=" text-align: left; text-align: left; ">
                    <asp:Label ID="TxtJoin" runat="server" Font-Names="Trebuchet MS" ForeColor="ControlDarkDark" ></asp:Label></td>
            </tr>
                 <tr>
                <td style=" height: 21px; text-align: right; height: 30px; ">
                <span style="font-size: 10pt; font-family: 'Trebuchet MS', Cursive">
                    Current Address</span>        </td>
                <td style=" height: 21px; height: 30px; text-align: left; ">
                    <asp:Label ID="TxtAdd" runat="server" Font-Names="Trebuchet MS" ForeColor="ControlDarkDark" ></asp:Label></td>
            </tr>
       
            <tr>
                <td style=" text-align: right; text-align: right; ">
                <span style="font-size: 10pt; font-family: 'Trebuchet MS', Cursive">
                    City</span></td>
                <td style=" height: 22px; text-align: left; ">
                    <asp:Label ID="TxtCity" runat="server" Font-Names="Trebuchet MS" ForeColor="ControlDarkDark" ></asp:Label></td>
                    </tr>
            <tr>
                <td style=" text-align: right; height: 30px; text-align: right; ">
                <span style="font-size: 10pt; font-family: 'Trebuchet MS', Cursive">
                    State
                    </span></td>
                <td style=" height: 21px; height: 30px; text-align: left; ">
                    <asp:Label ID="TxtState" runat="server" Font-Names="Trebuchet MS" ForeColor="ControlDarkDark" ></asp:Label></td>
                    </tr>
                         <tr>
                <td style=" height: 22px; text-align: right; ">
                    <span style="font-size: 10pt; font-family: 'Trebuchet MS', Cursive">
                    Country</span></td>
                <td style=" height: 22px; text-align: left; ">
                    <asp:Label ID="TxtCountry" runat="server" Font-Names="Trebuchet MS" ForeColor="ControlDarkDark"></asp:Label></td>
            </tr>
            
                 <tr>
                <td style=" height: 21px; text-align: right; height: 30px; ">
                <span style="font-size: 10pt; font-family: 'Trebuchet MS', Cursive">
                    Permanent Address</span>        </td>
                <td style=" height: 21px; height: 30px; text-align: left; ">
                    <asp:Label ID="TxtpAdd" runat="server" Font-Names="Trebuchet MS" ForeColor="ControlDarkDark" ></asp:Label></td>
            </tr>
       
            <tr>
                <td style=" text-align: right; text-align: right; ">
                <span style="font-size: 10pt; font-family: 'Trebuchet MS', Cursive">
                    City</span></td>
                <td style=" height: 22px; text-align: left; ">
                    <asp:Label ID="TxtpCity" runat="server" Font-Names="Trebuchet MS" ForeColor="ControlDarkDark" ></asp:Label></td>
                    </tr>
            <tr>
                <td style=" text-align: right; height: 30px; text-align: right; ">
                <span style="font-size: 10pt; font-family: 'Trebuchet MS', Cursive">
                    State
                    </span></td>
                <td style=" height: 21px; height: 30px; text-align: left; ">
                    <asp:Label ID="TxtpState" runat="server" Font-Names="Trebuchet MS" ForeColor="ControlDarkDark" ></asp:Label></td>
                    </tr>
                         <tr>
                <td style=" height: 22px; text-align: right; ">
                    <span style="font-size: 10pt; font-family: 'Trebuchet MS', Cursive">
                    Country</span></td>
                <td style=" height: 22px; text-align: left; ">
                    <asp:Label ID="TxtpCountry" runat="server" Font-Names="Trebuchet MS" ForeColor="ControlDarkDark"></asp:Label></td>
            </tr>
       
            <tr>
                <td style=" text-align: right; text-align: right; ">
                <span style="font-size: 10pt; font-family: 'Trebuchet MS', Cursive">
                    Date Of Birth</span></td>
                <td style=" height: 26px; text-align: left; ">
                    <asp:Label ID="TxtDOB" runat="server" Font-Names="Trebuchet MS" ForeColor="ControlDarkDark" ></asp:Label></td>
                    </tr>
            <tr>
                <td style=" height: 26px; text-align: right; ">
                <span style="font-size: 10pt; font-family: 'Trebuchet MS', Cursive">
                    Tel#</span></td>
                <td style=" height: 26px; text-align: left; ">
                    <asp:Label ID="TxtTel" runat="server" Font-Names="Trebuchet MS" ForeColor="ControlDarkDark" ></asp:Label></td>
            </tr>
            <tr>
                <td style=" text-align: right; text-align: right; ">
                <span style="font-size: 10pt; font-family: 'Trebuchet MS', Cursive">
                    Cell#</span></td>
                <td style=" text-align: left; height: 26px; text-align: left; ">
                    <asp:Label ID="TxtCell" runat="server" Font-Names="Trebuchet MS" ForeColor="ControlDarkDark" ></asp:Label></td>
                    </tr>
            <tr>
                <td style=" text-align: right; height: 26px; text-align: right; ">
                <span style="font-size: 10pt; font-family: 'Trebuchet MS', Cursive">
                    Department</span></td>
                <td style=" height: 26px; text-align: left; ">
                    <asp:Label ID="TxtDept" runat="server" Font-Names="Trebuchet MS" ForeColor="ControlDarkDark"></asp:Label></td>
            </tr>
            <tr>
                <td style=" text-align: right; text-align: right; ">
                <span style="font-size: 10pt; font-family: 'Trebuchet MS', Cursive">
                    Designation</span></td>
                <td style=" text-align: left; text-align: left; ">
                    <asp:Label ID="TxtDesn" runat="server" Font-Names="Trebuchet MS" ForeColor="ControlDarkDark"></asp:Label></td>
                  </tr>
            <tr> 
                <td style="text-align: right;  height: 21px;">
                    <span style="font-size: 10pt; font-family: Trebuchet MS">Category</span></td>
                <td style="  text-align: left;  height: 21px;">
                    <asp:Label ID="TxtCategory" runat="server" Font-Names="Trebuchet MS" ForeColor="ControlDarkDark"></asp:Label></td>
            </tr>
            </table>
       
							
				
		</div>
		
	<!-- content-wrap ends-->	
	</div>
		
	<!-- footer starts -->			
	<div id="footer-wrap"></div>
	<!-- footer ends-->	
	
<!-- wrap ends here -->
</div>

</body>
</html>
