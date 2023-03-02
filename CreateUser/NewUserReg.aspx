<%@ Page Language="VB" AutoEventWireup="false" CodeFile="NewUserReg.aspx.vb" Inherits="NewUserReg" %>

<%@ Register Assembly="System.Web.Extensions, Version=1.0.61025.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI" TagPrefix="asp" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 1.0 Transitional/EN">


<html xmlns="http://www.w3.org/1999/xhtml" >

<head runat="server">
<link href= "../App_Themes/Css/styles.css" type="text/css" rel="stylesheet" />
<link href= "../App_Themes/Css/Common.css" type="text/css" rel="stylesheet" />
    <title>New User</title>
</head>
<body>
      
    <form id="form1" runat="server">
   
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
 <div id="body">
    <div id="cap"></div>
    <div id="main">
    <h1>Add New User </h1>
        <div style="text-align:left ">
            <asp:Label ID="Label1" runat="server" CssClass="Title" Font-Italic="True"  ForeColor="#C00000"></asp:Label>
        </div>
     <asp:Panel ID="PnlUser" runat="server"  >
			
      
       <table id="Table1" width="100%"  >
       <tr >
                <td style=" text-align: center;" colspan="4" class="HeaderDiv"  >
                User Registration Form
                    </td>
               
            </tr>
           
            <tr >
                <td style="width: 25%; text-align: right; ">
                    <span>*First Name</span></td>
                <td style="width: 25%; text-align: left;">
                    <asp:TextBox ID="TxtFirstName" runat="server" Width="95%"></asp:TextBox></td>
                <td style="width: 25%; text-align: right; ">
                    <span>Middle Name</span></td>
                <td style="width: 25%; text-align: left;">
                    <asp:TextBox ID="TxtMiddleName" runat="server" Width="95%"></asp:TextBox></td>
            </tr>
               <tr >
                  <td style="width: 25%; text-align: right; ">
                    <span>*Last Name</span></td>
                <td style="width: 25%; text-align: left;">
                    <asp:TextBox ID="TxtLastName" runat="server" Width="95%"></asp:TextBox></td>
                <td style="width: 25%;text-align: right;">
                    <span>Category</span></td>
                <td style="width: 25%;  text-align: left;">
               <asp:DropDownList Width="100%"  ID="txtCate" runat="server" Font-Bold="False" >
               <asp:ListItem Text="Select Category" Value=""></asp:ListItem>  
                    </asp:DropDownList>
              </td>
            </tr>
            <tr >
                <td style="width: 25%; text-align: right; ">
                    <span>
                    *E-Mail</span></td>
                <td style="width: 25%;text-align: left;">
                    <asp:TextBox ID="TxtEmail" runat="server" Width="95%"></asp:TextBox></td>
                <td style="width: 25%; text-align: right;">
                    <span>
                    Yahoo Chat ID</span></td>
                <td style="width: 25%; text-align: left;">
                    <asp:TextBox ID="TxtChatID" runat="server" Width="95%"></asp:TextBox></td>
            </tr>
            <tr >
                <td style="width: 25%; text-align: right; ">
                <span>
                    Personal E-Mail</span></td>
                <td style="width: 25%; text-align: left;">
                    <asp:TextBox ID="TxtNonOEmail" runat="server" Width="95%"></asp:TextBox></td>
                <td style="width: 25%; text-align: right; ">
                <span>
                    Joining Date (MM/DD/YYYY)</span></td>
                <td style="width: 25%; text-align: left; text-align: left;">
                    <asp:TextBox ID="TxtJoin" runat="server" Width="95%"></asp:TextBox></td>
            </tr>
           
           <tr >
               <td colspan="4" rowspan="1" style="text-align: center" valign="top"  class="alt1">
                   <strong>Current
                    Address</strong></td>
           </tr>
            <tr >
                <td style="width: 25%; text-align: right; " rowspan="3" valign="top">
                <span>Address</span></td>
                <td style="width: 25%; text-align: left;" valign="top" rowspan="3">
                    <asp:TextBox ID="TxtAdd" runat="server" TextMode="MultiLine" Rows="5" Width="95%"></asp:TextBox></td>
                <td style="width: 25%; text-align: right;">
                    <span>
                    City</span></td>
                <td style="width: 25%; text-align: left;">
                    <asp:TextBox ID="TxtCity" runat="server" Width="95%"></asp:TextBox>
                </td>
            </tr>
            <tr >
                <td style="width: 25%;  text-align: right;">
                <span>
                    State</span></td>
                <td style="width: 25%; text-align: left;">
                    <asp:TextBox ID="TxtState" runat="server" Width="95%"></asp:TextBox>
                </td>
            </tr>
           <tr >
               <td style="width: 25%; text-align: right">
                    Country</td>
               <td style="width: 25%; text-align: left">
                    <asp:DropDownList Width="100%"  ID="txtCountry" runat="server" Font-Bold="False"  >
                        <asp:ListItem Selected="True">India</asp:ListItem>
                        <asp:ListItem>USA</asp:ListItem>
                        <asp:ListItem>UK</asp:ListItem>
			<asp:ListItem>Philippines</asp:ListItem>
                    </asp:DropDownList></td>
           </tr>
           <tr >
               <td colspan="4" rowspan="1" style="text-align: center" valign="top" class="alt1" >
                   <strong>Permanent
                    Address</strong></td>
           </tr>
             <tr >
                <td style="width: 25%; text-align: right; " rowspan="3" valign="top">
                <span>Address</span></td>
                <td style="width: 25%; text-align: left;" valign="top" rowspan="3">
                    <asp:TextBox ID="TxtpAdd" runat="server" TextMode="MultiLine" Rows="5"  Width="95%"></asp:TextBox></td>
                <td style="width: 25%; text-align: right;">
                    <span>
                    City</span></td>
                <td style="width: 25%; text-align: left;">
                    <asp:TextBox ID="TxtpCity" runat="server" Width="95%"></asp:TextBox>
                </td>
            </tr>
            <tr >
                <td style="width: 25%;  text-align: right;">
                <span>
                    State</span></td>
                <td style="width: 25%; text-align: left;">
                    <asp:TextBox ID="TxtpState" runat="server" Width="95%"></asp:TextBox>
                </td>
            </tr>
           <tr >
               <td style="width: 25%; text-align: right">
                    Country</td>
               <td style="width: 25%; text-align: left">
                    <asp:DropDownList Width="100%"  ID="txtpCountry" runat="server" Font-Bold="False" >
                        <asp:ListItem Selected="True">India</asp:ListItem>
                        <asp:ListItem>USA</asp:ListItem>
                        <asp:ListItem>UK</asp:ListItem>
			<asp:ListItem>Philippines</asp:ListItem>
                    </asp:DropDownList></td>
           </tr>
            <tr >
                <td style="width: 25%; text-align: right; ">
                <span>
                    Date Of Birth (MM/DD/YYYY)</span></td>
                <td style="width: 25%; text-align: left;">
                <asp:TextBox runat="server" ID="TxtDOB" Width="95%"/>
               <ajaxToolkit:CalendarExtender ID="defaultCalendarExtender" runat="server" TargetControlID="TxtDOB" /> 
                   <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender5" runat="server"
            TargetControlID="TxtDOB"
            Mask="99/99/9999"
            MessageValidatorTip="true"
            OnFocusCssClass="MaskedEditFocus"
            OnInvalidCssClass="MaskedEditError"
            MaskType="Date"
            DisplayMoney="Left"
            AcceptNegative="Left"
            ErrorTooltipEnabled="True" />
               
            </td>
                <td style="width: 25%;text-align: right;">
                <span>Landline#</span></td>
                <td style="width: 25%; text-align: left;">
                    <asp:TextBox ID="TxtTel" runat="server" Width="95%"></asp:TextBox></td>
            </tr>
            <tr >
                <td style="width: 25%; text-align: right; ">
                <span>
                    *Mobile#</span></td>
                <td style="width: 25%; text-align: left; text-align: left;">
                    <asp:TextBox ID="TxtCell" runat="server" Width="95%"></asp:TextBox>
                     <ajaxToolkit:FilteredTextBoxExtender
                        ID="FilteredTextBoxExtender1"
                        runat="server"
                        TargetControlID="TxtCell"
                    FilterType="Numbers" /></td>
                <td style="width: 25%; text-align: right; text-align: right;">
                <span>
                    Department</span></td>
                <td style="width: 25%; text-align: left;">
                    <asp:DropDownList Width="100%"  ID="TxtDept" AutoPostBack="true"  runat="server" Font-Bold="False" >
                    <asp:ListItem Text="Select Department" Value=""></asp:ListItem> 
                    </asp:DropDownList>
                    </td>
            </tr>
            <tr >
                <td style="width: 25%; text-align: right;  height: 25px;">
                <span>
                    Designation</span></td>
                <td style="width: 25%; text-align: left; text-align: left; height: 25px;">
                <asp:DropDownList Width="100%"  ID="TxtDesi" runat="server" Font-Bold="False"  >
                <asp:ListItem Text="Select Designation" Value=""></asp:ListItem>
                </asp:DropDownList>
               
                   </td>
                   <td style="text-align: right; height: 25px;">
                    Mentor</td>
                <td style="  text-align: left; height: 25px;">
                 <asp:DropDownList ID="DLmentor" runat="server" Width="152px">
                 </asp:DropDownList>
        </td>   
                
            </tr>
            <tr >
                <td style="width: 25%; text-align: right">
                    <asp:CheckBox ID="ChkUn" runat="server" Checked="True" AutoPostBack="True" /></td>
                <td style="width: 25%;  text-align: left">
                    <span>Same as Employee Code</span></td>
                <td style="width: 25%;  text-align: right">
                    <span>
                        <asp:Label ID="LblUN" runat="server" CssClass="common" 
                            Text="*Username"></asp:Label></span></td>
                <td style="width: 25%; text-align: left;">
                    <asp:TextBox ID="TxtUname" runat="server" Width="95%"></asp:TextBox></td>
            </tr>
           <%--<tr>
                 
         <td style="width: 25%; text-align: right;">
                    <span>
                    Target Lines</span></td>
                <td style="width: 25%; text-align: left;">
                    <asp:TextBox ID="TRGLines" runat="server" Width="95%"></asp:TextBox></td>
         
            </tr>--%>
        </table>

         
       
        <table width="100%" >
            
                    <tr>
                        <td colspan="4" style="text-align: center" class="alt1" >
                        <span><strong>Upload Files</strong></span></td>
                </tr>
                <tr>
                    <td style="width: 25%; text-align: right;  text-align: right;">
                        <span>Submit Photo</span></td>
                    <td colspan="3" style=" text-align: left">
                        <asp:FileUpload ID="FileUpload1" runat="server" /></td>
                </tr>
           
            </table>
        
            <asp:Label ID="Label2" runat="server" Text=""></asp:Label><br />
   
        <table width="100%">
          
                <tr>
                    <td colspan="4" style="text-align: center">
                        <asp:Button ID="Button4" runat="server"  Text="Submit" CssClass="button"  />
                      <%--<--  <--asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional"  >
                        <--ContentTemplate>
                        <--/ContentTemplate> 
                        <--Triggers>
                        <--asp:AsyncPostBackTrigger ControlID="Button4" EventName="Click" />
              
                           
                        <--/Triggers> 
                        <--/asp:UpdatePanel>--%>
                        </td>
                </tr>
    
                </table>
         <asp:Panel ID="Panel1" runat="server" HorizontalAlign="Left">
            <asp:RequiredFieldValidator  Display="None" ID="RequiredFieldValidator1" runat="server" ErrorMessage="Field First Name is required " ControlToValidate="TxtFirstName" ></asp:RequiredFieldValidator> <br />
            <asp:RequiredFieldValidator  Display="None" ID="RequiredFieldValidator2" runat="server" ErrorMessage="Field Last Name is required " ControlToValidate="TxtLastName" ></asp:RequiredFieldValidator> <br />
            <asp:RequiredFieldValidator  Display="None" ID="RequiredFieldValidator3" runat="server" ErrorMessage="Field Department is required " ControlToValidate="TxtDept" ></asp:RequiredFieldValidator> <br />
            <asp:RequiredFieldValidator  Display="None" ID="RequiredFieldValidator4" runat="server" ErrorMessage="Field Designation is required " ControlToValidate="TxtDesi" ></asp:RequiredFieldValidator> <br />
            <%--<asp:RequiredFieldValidator  Display="None" ID="RequiredFieldValidator5" runat="server" ErrorMessage="Field Category is required " ControlToValidate="txtCate" ></asp:RequiredFieldValidator><br />--%>
            <asp:RequiredFieldValidator  Display="None" ID="RequiredFieldValidator7" runat="server" ErrorMessage="Field Mobile# is required " ControlToValidate="TxtCell" ></asp:RequiredFieldValidator><br />
         </asp:Panel>
        
         
        </asp:Panel>
          
          <asp:Table HorizontalAlign="Left" ID="tblResult" runat="server" Width="50%" >
                <asp:TableRow>
                    <asp:TableCell CssClass="alt1" >User Full Name</asp:TableCell>
                    <asp:TableCell CssClass="alt1">Username</asp:TableCell>
                    <asp:TableCell CssClass="alt1">Password</asp:TableCell>
                </asp:TableRow>
                <asp:TableRow>
                    <asp:TableCell><asp:Label ID="lblName" runat="server"></asp:Label></asp:TableCell>
                    <asp:TableCell><asp:Label ID="lblUsername" runat="server"></asp:Label></asp:TableCell> 
                    <asp:TableCell><asp:Label ID="lblPass" runat="server"></asp:Label></asp:TableCell>
                </asp:TableRow>
                <asp:TableRow>
                    <asp:TableCell ColumnSpan="2">
                        <asp:LinkButton ID="lnkAddAnother" runat="server"><< Add another User</asp:LinkButton>
                    </asp:TableCell>
                </asp:TableRow>
            </asp:Table>
            </div>
            </div>
               
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
    <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator5" ControlToValidate="TxtEmail"
                                Display="None" ErrorMessage="<b>Required Field Missing</b><br />E-Mail is required." />
<asp:RegularExpressionValidator   Display="None"
    id="RegTxtEmaill"  
    runat="server" 
    ControlToValidate="TxtEmail" 
        ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"
    ErrorMessage="E-Mail - Please enter valid input."
   />
<asp:RegularExpressionValidator   Display="None"
    id="RegTxtUname"  
    runat="server" 
    ControlToValidate="TxtUname" 
    ValidationExpression="^[0-9a-zA-Z]+$"
    ErrorMessage="Username - Please enter valid input."
   />
<asp:CompareValidator   Display="None"
                      ControlToValidate="TxtJoin"
                     
                      ErrorMessage="Joining Date - Please enter valid input."
                      ID="CompareValidator3"
                      Operator="DataTypeCheck"
                      Type="Date"
                      runat="server" />



           <asp:ValidationSummary ID="ValidationSummary1" runat="server" HeaderText="Following error occurs:" ShowMessageBox="true" DisplayMode="BulletList" ShowSummary="False" /> </form>  
</body>
</html>
