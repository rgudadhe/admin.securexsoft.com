<%@ Page Language="VB" AutoEventWireup="false" CodeFile="EditUserEmail.aspx.vb" Inherits="EditUserEmail" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 1.0 Transitional/EN">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
<link href= "../App_Themes/Css/styles.css" type="text/css" rel="stylesheet" />

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

function hide() {
try
{
      //alert('hello1');
            //document.getElementById('Panel1').style.visibility == "hidden";
            document.getElementById('Panel1').style.display = 'none';
        //    alert('hello');
         //   document.getElementById('Button4').disabled = "false";
            //return true;
            }
            catch(e)
            {
            //return true;
            }
           
    }



</script>  



</head>
<body  >
 
    <form id="form1" runat="server" >
                    <div id="body">
    <div id="cap"></div>
    <div id="main">
    <h1>Edit User Data </h1>
        <div style="text-align:left">
            <asp:Label ID="lblMsg" runat="server" Text="" ForeColor="red" Font-Bold="true"></asp:Label>
        </div>                    
       <table width="100%">
           
            <tr>
                <td style="text-align: right; " class="HeaderDiv"  >
                  
                    Username 
                </td>
                <td style="text-align: left; "  class="HeaderDiv" >
                    <asp:DropDownList ID="DropDownList1"  runat="server" AutoPostBack="true"  >
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
                            <td  >
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
           
            <tr>
                <td style=" text-align: right; text-align: right;" >
                    <span >
                    First Name</span></td>
                <td style=" text-align: left;">
                    <asp:TextBox ID="TxtFirstName" runat="server"  ForeColor="ControlDarkDark" ></asp:TextBox></td>
                    </tr>
                       <tr>
                <td style=" text-align: right; text-align: right;">
                    <span >
                    Middle Name</span></td>
                <td style=" text-align: left;">
                    <asp:TextBox ID="TxtMiddleName" runat="server"  ForeColor="ControlDarkDark" ></asp:TextBox></td>
            </tr>
            <tr>
                <td style=" text-align: right; text-align: right;">
                    <span >
                    Last Name</span></td>
                <td style=" text-align: left;">
                    <asp:TextBox ID="TxtLastName" runat="server"  ForeColor="ControlDarkDark" ></asp:TextBox></td>
            </tr>
            <tr>
                <td style=" text-align: right; text-align: right;">
                    <span >
                    E-Mail</span></td>
                <td style=" text-align: left;">
                    <asp:TextBox ID="TxtEmail" runat="server"  ForeColor="ControlDarkDark"></asp:TextBox>
                     <asp:RegularExpressionValidator  Display="None" ControlToValidate="TxtEmail" SetFocusOnError="true"  ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" Text="Invalid E-Mail." ID="RegularExpressionValidator1" runat="server" ErrorMessage="RegularExpressionValidator" Font-Bold="true" Font-Names="Arial" Font-Size="12px"></asp:RegularExpressionValidator>
                    </td>
                    </tr>
            <tr>
                <td style=" text-align: right;">
                    <span >
                    Yahoo Chat ID</span></td>
                <td style=" text-align: left;">
                    <asp:TextBox ID="TxtChatID" runat="server"  ForeColor="ControlDarkDark" ></asp:TextBox>&nbsp;
                    </td>
            </tr>
            <tr>
                <td style=" text-align: right; text-align: right;">
                <span >
                    Personal E-Mail</span>
                    
                    </td>
                <td style="width: 89px; text-align: left;">
                    <asp:TextBox ID="TxtNonOEmail" runat="server"  ForeColor="ControlDarkDark" ></asp:TextBox>&nbsp;
                    </td>
                    </tr>
                    
            <tr>
                <td style=" text-align: right; text-align: right;">
                <span >
                    Joining Date
                    (MM/DD/YYYY)</span></td>
                <td style=" text-align: left; text-align: left;">
                    <asp:TextBox ID="TxtJoin" runat="server"  ForeColor="ControlDarkDark" ></asp:TextBox></td>
            </tr>
                
            <tr>
                <td style=" text-align: right; text-align: right;">
                <span >
                    Termination Date
                    (MM/DD/YYYY)</span></td>
                <td style=" text-align: left; text-align: left;">
                    <asp:TextBox ID="TxtTerm" runat="server"  ForeColor="ControlDarkDark" ></asp:TextBox></td>
            </tr>
            
               <tr>
                <td style=" height: 21px; text-align: right; height: 30px;">
                <span >
                    Current Address</span>        </td>
                <td style=" height: 21px; height: 30px; text-align: left;">
                    <asp:TextBox ID="TxtAdd" runat="server"  ForeColor="ControlDarkDark"  TextMode="MultiLine" ></asp:TextBox></td>
            </tr>
            <tr>
                <td style=" text-align: right; text-align: right;">
                <span >
                    City</span></td>
                <td style=" text-align: left;">
                    <asp:TextBox ID="TxtCity" runat="server"  ForeColor="ControlDarkDark" ></asp:TextBox></td>
                    </tr>
                 <tr>
                <td style=" text-align: right; text-align: right;">
                <span >
                    State
                    </span></td>
                <td style=" text-align: left;">
                    <asp:TextBox ID="TxtState" runat="server"  ForeColor="ControlDarkDark" ></asp:TextBox></td>
                    </tr>    
              <tr>
                <td style="  text-align: right;">
                    <span >
                    Country</span></td>
                <td style=" text-align: left;">
                    <asp:DropDownList ID="txtCountry" runat="server"  ForeColor="ControlDarkDark" >
                        <asp:ListItem Selected="True"  value ="" Text="Select Country" />  
                        <asp:ListItem >India</asp:ListItem>
                        <asp:ListItem>USA</asp:ListItem>
                        <asp:ListItem>UK</asp:ListItem>
			<asp:ListItem>Philippines</asp:ListItem>
                    </asp:DropDownList></td>
            </tr>      
                     <tr>
                <td style=" height: 21px; text-align: right; height: 30px;">
                <span >
                    Permanent Address</span>        </td>
                <td style=" height: 21px; height: 30px; text-align: left;">
                    <asp:TextBox ID="TxtpAdd"  runat="server"  ForeColor="ControlDarkDark" TextMode="MultiLine"  ></asp:TextBox></td>
            </tr>
            <tr>
                <td style=" text-align: right; text-align: right;">
                <span >
                    City</span></td>
                <td style="text-align: left;">
                    <asp:TextBox ID="TxtpCity" runat="server"  ForeColor="ControlDarkDark" ></asp:TextBox></td>
                    </tr>
                 <tr>
                <td style=" text-align: right; text-align: right;">
                <span >
                    State
                    </span></td>
                <td style=" text-align: left;">
                    <asp:TextBox ID="TxtpState" runat="server"  ForeColor="ControlDarkDark" ></asp:TextBox></td>
                    </tr>  
                     
            <tr>
                <td style="text-align: right;">
                    <span >
                    Country</span></td>
                <td style=" text-align: left;">
                    <asp:DropDownList ID="txtpCountry" runat="server"  ForeColor="ControlDarkDark" >
                        <asp:ListItem Selected="True" value ="" Text="Select Country" />  
                        <asp:ListItem >India</asp:ListItem>
                        <asp:ListItem>USA</asp:ListItem>
			<asp:ListItem>Philippines</asp:ListItem>
                        <asp:ListItem>UK</asp:ListItem>
                    </asp:DropDownList></td>
            </tr>
          
         
            <tr>
                <td style=" text-align: right; text-align: right;">
                <span >
                    Date Of Birth (MM/DD/YYYY)</span></td>
                <td style=" text-align: left;">
                    <asp:TextBox ID="TxtDOB" runat="server"  ForeColor="ControlDarkDark" ></asp:TextBox></td>
                    </tr>
            <tr>
                <td style="  text-align: right;">
                <span >
                    Tel#</span></td>
                <td style="  text-align: left;">
                    <asp:TextBox ID="TxtTel" runat="server"  ForeColor="ControlDarkDark" ></asp:TextBox></td>
            </tr>
            <tr>
                <td style=" text-align: right; text-align: right;">
                <span >
                    Mobile#</span></td>
                <td style=" text-align: left;text-align: left;">
                    <asp:TextBox ID="TxtCell" runat="server"  ForeColor="ControlDarkDark" ></asp:TextBox></td>
                    </tr>
                      <%--<tr>
                <td style=" text-align: right; text-align: right;">
                <span >
                    Target Lines</span></td>
                <td style=" text-align: left; text-align: left;">
                    <asp:TextBox ID="TRGLines" runat="server"  ForeColor="ControlDarkDark" ></asp:TextBox></td>
                    </tr>--%>
            <tr>
                <td style=" text-align: right; text-align: right;">
                <span >
                    Department</span></td>
                <td style=" text-align: left;">
                    <asp:DropDownList ID="TxtDept" runat="server"  AppendDataBoundItems="true"  DataTextField="Name" DataValueField="DepartmentID" AutoPostBack="True"   ForeColor="ControlDarkDark" >
                    </asp:DropDownList>
       
        </td>
            </tr>
            <tr>
                <td style=" text-align: right; text-align: right; ">
                <span >
                    Designation</span></td>
                <td style=" text-align: left; text-align: left;"><asp:DropDownList ID="TxtDesi"   ForeColor="ControlDarkDark"  runat="server"  >
               <asp:ListItem Selected="True" value="" Text="Select Designation" />   
                </asp:DropDownList>
                   </td>
                  </tr>
            <tr> 
                <td style="text-align: right;">
                    <span >Category</span></td>
                <td style="  text-align: left;">
                 <asp:DropDownList ID="txtCate" runat="server"     ForeColor="ControlDarkDark" >
                 </asp:DropDownList>
        </td>  
            </tr>
             <tr> 
                <td style="text-align: right;">
                    <span>User Status</span></td>
                <td style="  text-align: left;">
                 <asp:DropDownList ID="DLStatus" runat="server"      >
                 <asp:ListItem Text="Active User" Value="False"></asp:ListItem> 
                 <asp:ListItem Text="Deleted User" Value="True"></asp:ListItem> 
                 </asp:DropDownList>
        </td>  
            </tr>
             <tr> 
                <td style="text-align: right;">
                    <span >Mentor</span></td>
                <td style="  text-align: left;">
                 <asp:DropDownList ID="DLmentor" runat="server"      >
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
                <tr>
                           
                <td style="text-align: center" colspan="2">
                      <asp:Button ID="Button1" runat="server"  Text="Rename Email" CssClass="button"  /></td>

           </tr>
          
        
        </table> 
        
        </asp:Panel> 
       
        <div style="text-align:left">
       
         <asp:Label ID="Label1" runat="server" CssClass="Title" 
            ForeColor="#400000" Height="16px" Width="496px"></asp:Label>
    </div>        
    
        <asp:HiddenField ID="hdnUserID" runat="server" />
    
       

      <asp:RegularExpressionValidator   Display="None"
    id="RegTxtFirstName"  
    runat="server" 
    ControlToValidate="TxtFirstName" 
    ValidationExpression="^[a-zA-Z ]+$"
    SetFocusOnError="true" 
    EnableClientScript="true" 
       ErrorMessage="First Name - Please enter valid input."
    />
      <asp:RegularExpressionValidator   Display="None"
    id="RegTxtMiddleName"  
    runat="server" 
    ControlToValidate="TxtMiddleName" 
    ValidationExpression="^[a-zA-Z ]+$"
    SetFocusOnError="true" 
    ErrorMessage="Middle Name - Please enter valid input."
    />
      <asp:RegularExpressionValidator   Display="None"
    id="RegTxtLastName"  
    runat="server" 
    ControlToValidate="TxtLastName" 
    ValidationExpression="^[a-zA-Z ]+$"
    SetFocusOnError="true" 
    ErrorMessage="Last Name - Please enter valid input."
    />
<asp:RegularExpressionValidator   Display="None"
    id="RegTxtEmaill"  
    runat="server" 
    ControlToValidate="TxtEmail" 
        ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"
    ErrorMessage="E-Mail - Please enter valid input."
   />

<asp:CompareValidator   Display="None"
                      ControlToValidate="TxtJoin"
                     
                      ErrorMessage="Joining Date - Please enter valid input."
                      ID="CompareValidator3"
                      Operator="DataTypeCheck"
                      Type="Date"
                      runat="server" />


       <asp:RequiredFieldValidator  Display="None" ID="RequiredFieldValidator1" runat="server" ErrorMessage="Field First Name is required " ControlToValidate="TxtFirstName" ></asp:RequiredFieldValidator> <br />
            <asp:RequiredFieldValidator  Display="None" ID="RequiredFieldValidator2" runat="server" ErrorMessage="Field Last Name is required " ControlToValidate="TxtLastName" ></asp:RequiredFieldValidator> <br />
            <asp:RequiredFieldValidator  Display="None" ID="RequiredFieldValidator3" runat="server" ErrorMessage="Field Department is required " ControlToValidate="TxtDept" ></asp:RequiredFieldValidator> <br />
            <asp:RequiredFieldValidator  Display="None" ID="RequiredFieldValidator4" runat="server" ErrorMessage="Field Designation is required " ControlToValidate="TxtDesi" ></asp:RequiredFieldValidator> <br />
            <%--<asp:RequiredFieldValidator  Display="None" ID="RequiredFieldValidator5" runat="server" ErrorMessage="Field Category is required " ControlToValidate="txtCate" ></asp:RequiredFieldValidator><br />--%>
            <asp:RequiredFieldValidator  Display="None" ID="RequiredFieldValidator7" runat="server" ErrorMessage="Field Mobile# is required " ControlToValidate="TxtCell" ></asp:RequiredFieldValidator><br />  
    <asp:ValidationSummary ID="ValidationSummary1" runat="server" HeaderText="Following error occurs:" ShowMessageBox="true" DisplayMode="BulletList" ShowSummary="False" /> 
    
    </form>
    
</body>
</html>
