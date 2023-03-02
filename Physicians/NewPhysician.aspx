<%@ Page Language="VB" AutoEventWireup="false" CodeFile="NewPhysician.aspx.vb" Inherits="Department_Default" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 1.0 Transitional/EN">


<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>New Dictator</title>
    <link href= "../App_Themes/Css/Main.css" type="text/css" rel="stylesheet" />
    <link href= "../App_Themes/Css/Common.css" type="text/css" rel="stylesheet" />
    <link href= "../App_Themes/Css/Styles.css" type="text/css" rel="stylesheet" />

</head>
<body style="text-align: left" >
    <form id="form1" runat="server">
    <div id="body" style="height: 100%;" >
    <div id="cap"></div>
    <div id="main">
    <h1>New Dictator</h1>
    <asp:Panel ID="Panel1" HorizontalAlign="Left" runat="server" >
        <table width="80%" >
            <tr>
                <td colspan="4" style="text-align: center; height: 15px;" class="HeaderDiv">
                    Dictator Form</td>
            </tr>
            <tr>
                <td style="text-align: right; text-align: right;">
                    *First Name</td>
                <td style="text-align: left;">
                    <asp:TextBox ID="TxtFirstName" runat="server"></asp:TextBox></td>
                <td style="text-align: right; text-align: right;">
                    Middle Name</td>
                <td style="text-align: left;">
                    <asp:TextBox ID="TxtMiddleName" runat="server"></asp:TextBox></td>
            </tr>
            <tr>
                <td style="text-align: right; text-align: right;">
                    *Last Name</td>
                <td style=" text-align: left;">
                    <asp:TextBox ID="TxtLastName" runat="server"></asp:TextBox></td>
                <td style="text-align: right; text-align: right;">
                *Signed Name</td>
                <td style=" text-align: left;">
                    <asp:TextBox ID="TxtSignedName" runat="server"></asp:TextBox></td>
            </tr>
            <tr>
                
                <td style="text-align: right; text-align: right;">
                Speciality</td>
                <td style="text-align: left; text-align: left;">
                    <asp:TextBox ID="TxtSpeciality" runat="server"></asp:TextBox></td>
                     <td style="height: 22px; text-align: right;">
                     *Email</td>
                <td style="height: 22px; text-align: left;">
                    <asp:TextBox ID="TxtEmail" runat="server" Width="144px"></asp:TextBox></td>
            </tr>
           
            <tr>
                <td style="text-align: right;text-align: right;">
                Phone Number&nbsp;</td>
                <td style=" text-align: left;">
                    <asp:TextBox ID="TxtPhoneno" runat="server"></asp:TextBox></td>
                <td style=" text-align: right;">
                Fax        </td>
                <td style="text-align: left;">
                    <asp:TextBox ID="txtFax" runat="server"></asp:TextBox></td>
            </tr>
            <tr>
                
                <td style="text-align: right; text-align: right; vertical-align:top">
                    Extend. Signed Name
                </td>
                <td style="text-align: left; vertical-align:top">
                    <asp:TextBox ID="txtExSignedName" TextMode="MultiLine" Width="290" Height="60" runat="server"></asp:TextBox>
                </td>
           
                
                    <td style=" text-align: right; vertical-align:top  ">
                Provider ID        </td>
                <td style="  text-align: left; vertical-align:top ">
                    <asp:TextBox ID="TxProvID" runat="server"></asp:TextBox></td>
        
            </tr>
            <tr>
                <td style="text-align: right; text-align: right;">
                    *Account Name</td>
                <td style=" text-align: left;" colspan="3">
                    <asp:DropDownList ID="ActID" runat="server">
                        <asp:ListItem Text="Select Account"  Value="" Selected="True"></asp:ListItem>   
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                
                <td style="text-align: right; text-align: right;">
                  FaxPlus - Dictating Physician</td>
                <td style=" text-align: left;">
                    <asp:CheckBox ID="chkFax" runat="server" />
                </td>
           
                 <td style="text-align: right; text-align: right;">
                  Exclude from AutoFax</td>
                <td style=" text-align: left;">
                   <asp:DropDownList ID="DDLAutoFax" runat="server">
                   
                    <asp:ListItem Text="No" Value="False">                   </asp:ListItem>
                     <asp:ListItem Text="Yes" Value="True">                   </asp:ListItem>
                    </asp:DropDownList>
                </td>
                   
        
            </tr>
            <tr>
                <td style="text-align: center;" colspan="4">
                    <asp:Button ID="Button1" runat="server" CssClass="button" Text="Submit" />
                </td>
            </tr>
        </table>
         <asp:Label ID="MsgDisp" runat="server" CssClass="Title" ForeColor="#C00000" Height="16px" Width="496px"></asp:Label>
        <br />    
        <asp:RequiredFieldValidator  Display="None" ID="RequiredFieldValidator1" runat="server" ControlToValidate="TxtFirstName"
            ErrorMessage="Please enter First Name" SetFocusOnError="True"></asp:RequiredFieldValidator><br />
        <asp:RequiredFieldValidator  Display="None" ID="RequiredFieldValidator2" runat="server" ControlToValidate="TxtLastName"
            ErrorMessage="Please enter Last Name" SetFocusOnError="True"></asp:RequiredFieldValidator><br />
        <asp:RequiredFieldValidator  Display="None" ID="RequiredFieldValidator3" runat="server" ControlToValidate="TxtSignedName"
            ErrorMessage="Please enter Signed Name" ></asp:RequiredFieldValidator><br />
            <asp:RequiredFieldValidator  Display="None" ID="RequiredFieldValidator5" runat="server" ControlToValidate="TxtEmail"
            ErrorMessage="Please enter E-Mail Address" ></asp:RequiredFieldValidator><br />
              <asp:RequiredFieldValidator  Display="None" ID="RequiredFieldValidator4" runat="server" ControlToValidate="ActID"
            ErrorMessage="Please select account" ></asp:RequiredFieldValidator><br />
        
    </asp:Panel>
       </div> 
       </div> 
       
 <div style="text-align:left">       
    <asp:RegularExpressionValidator  Display="None"
    id="RegTxtFirstName"  
    runat="server" 
    ControlToValidate="TxtFirstName" 
    ValidationExpression="^[0-9a-zA-Z ]+$"
    ErrorMessage="First Name - Please enter valid input."
   />
 <asp:RegularExpressionValidator  Display="None"
    id="RegularExpressionValidator1"  
    runat="server" 
    ControlToValidate="TxtMiddleName" 
    ValidationExpression="^[0-9a-zA-Z.]+$"
    ErrorMessage="Middle Name - Please enter valid input."
   />
 <asp:RegularExpressionValidator  Display="None"
    id="RegTxtLastName"  
    runat="server" 
    ControlToValidate="TxtLastName" 
    ValidationExpression="^[0-9a-zA-Z ]+$"
    ErrorMessage="Last Name - Please enter valid input."
   />
 
 <asp:RegularExpressionValidator  Display="None"
    id="RegTxtSpeciality"  
    runat="server" 
    ControlToValidate="TxtSpeciality" 
    ValidationExpression="^[a-zA-Z ]+$"
    ErrorMessage="Speciality - Please enter valid input."
   />
 <%--<asp:RegularExpressionValidator  Display="None"
    id="RegTxtEmail"  
    runat="server" 
    ControlToValidate="TxtEmail" 
    ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"
    ErrorMessage="EMail - Please enter valid input."
   />--%>
 <asp:RegularExpressionValidator  Display="None"
    id="RegTxtPhoneno"  
    runat="server" 
    ControlToValidate="TxtPhoneno" 
    ValidationExpression="^[0-9-]+$"
    ErrorMessage="Phone Number - Please enter valid input."
   />
 <asp:RegularExpressionValidator  Display="None"
    id="RegtxtFax"  
    runat="server" 
    ControlToValidate="txtFax" 
    ValidationExpression="^[0-9-]+$"
    ErrorMessage="Fax - Please enter valid input."
   />
</div>
    <asp:ValidationSummary ID="ValidationSummary1" runat="server" HeaderText="Following error occurs:" ShowMessageBox="true" DisplayMode="BulletList" ShowSummary="False" /> </form>
    
</body>
</html>
