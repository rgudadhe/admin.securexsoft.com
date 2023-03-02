<%@ Page Language="VB" AutoEventWireup="false" CodeFile="NewAccount.aspx.vb" Inherits="Department_Default" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 1.0 Transitional/EN">

<script runat="server">

        
</script>

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>New Account</title>
   
    <link href= "../App_Themes/Css/styles.css" type="text/css" rel="stylesheet" />
</head>
<body style="text-align: left" >
    <form id="form1" runat="server">
    <div id="body">
    <div id="cap"></div>
    <div id="main">
    <h1>New Account</h1>
    <asp:Panel ID="Panel1" runat="server" >
    
      
        <table width="100%">
            <tr>
                <td class="HeaderDiv" colspan="4"  style="text-align:center;" >
                  
                        Account Form</td>
            </tr>
            <tr style="color: gray; ">
                <td style="text-align: right; text-align: right;">
                        *Account Name</td>
                <td style="text-align: left; ">
                    <asp:TextBox ID="TxtAccountName" runat="server"></asp:TextBox>
                    </td>
                <td style="text-align: right; text-align: right;">
                    *Description</td>
                <td style="text-align: left;">
                    <asp:TextBox ID="TxtDescription" runat="server"></asp:TextBox></td>
            </tr>
            <tr style="color: gray; ">
                <td style="text-align: right; text-align: right;">
                *Processing Folder Name</td>
                <td style=" text-align: left; ">
                    <asp:TextBox ID="TxtFolder" runat="server"></asp:TextBox></td>
                <td style="text-align: right; text-align: right; ">
                *Billing Account Number</td>
                <td style="text-align: left; text-align: left; ">
                    <asp:TextBox ID="TxtBillNumber" runat="server"></asp:TextBox></td>
            </tr>
            <tr style="color: gray; ">
                <td style="text-align: right; text-align: right;">
                    Category</td>
                <td style="height: 21px; text-align: left; "><asp:DropDownList ID="DrpCategory" runat="server" DataTextField="Description" DataValueField="Category" DataSourceID="SqlDataSource1" Width="130px">
                   <asp:ListItem Text="Select Category" Value="" ></asp:ListItem>      
                </asp:DropDownList><asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:ETSConnectionString %>"
                    SelectCommand="SELECT [Category], [Description] FROM [tblActCategories] WHERE ([ContractorID] = @ContractorID)">
                    <SelectParameters>
                        <asp:SessionParameter Name="ContractorID" SessionField="ContractorID" Type="String" />
                    </SelectParameters>
                </asp:SqlDataSource>
                </td>
                <td style="height: 21px; text-align: right;">
                    RSS Type</td>
                <td style="height: 21px; text-align: left;">
                    <asp:DropDownList ID="TxtRSSType" runat="server" Width="130px">
                    <asp:ListItem Value="0" Text="Select RSS Type" ></asp:ListItem>                
                    <asp:ListItem Value="1" Text="Phone" ></asp:ListItem>
                    <asp:ListItem Value="2" Text="DVR"></asp:ListItem>
                    <asp:ListItem Value="4" Text="SmartPhone"></asp:ListItem>
                    <asp:ListItem Value="3" Text ="Multiple"></asp:ListItem>
                    </asp:DropDownList></td>
            </tr>
         
                 <tr style="color: gray; ">
                 <td style="height: 26px; text-align: right;">
                     <span>
                    Address</span></td>
                 <td style="height: 26px; text-align: left">
                    <asp:TextBox ID="TxtAdddress" runat="server" ></asp:TextBox></td>   
                    <td style="text-align: right" >
                    City</td>
                <td>
                    <asp:TextBox ID="TXTCity" runat="server"></asp:TextBox></td>
            </tr>
             <tr>
                <td style="text-align: right; height: 30px;">
                    State</td>
                <td style="height: 30px">
                    <asp:TextBox ID="TXTState" runat="server"></asp:TextBox></td>
                <td style="text-align: right; height: 30px;" >
                    Country</td>
                <td style="height: 30px">
                     <asp:DropDownList ID="DDLCntry" runat="server" Width="130px">
                    <asp:ListItem Value="" Text="Select Country" ></asp:ListItem>                
                    <asp:ListItem Value="US" Text="US"></asp:ListItem>
                    <asp:ListItem Value="Canada" Text="Canada"></asp:ListItem>
                    <asp:ListItem Value="UK" Text ="UK"></asp:ListItem>
                    <asp:ListItem Value="India" Text="India" ></asp:ListItem>
                    </asp:DropDownList></td>
                </tr>
            
                <tr>
                <td style="text-align: right;">
                    Zip</td>
                <td>
                    <asp:TextBox ID="TXTZip" runat="server"></asp:TextBox></td>
                <td style="text-align: right; height: 26px; text-align: right;">
                Official Website</td>
                <td style="height: 26px; text-align: left;">
                    <asp:TextBox ID="TxtOfficialSite" runat="server"></asp:TextBox></td>
                </tr>
                  <tr style="color: gray; ">
                <td style="text-align: right; text-align: right;">
                Expected Daily Minutes</td>
                <td style="text-align: left; height: 26px; text-align: left; ">
                    <asp:TextBox ID="TxtProtocolMins" runat="server"></asp:TextBox></td>
                 <td style="height: 26px; text-align: right">
                    Billing Mode</td>
                 <td style="height: 26px; text-align: left; ">
                     <asp:DropDownList ID="DLMode" runat="server" Width="130px">
                         <asp:ListItem Selected="True" Value="S">Standard</asp:ListItem>
                         <asp:ListItem Value="LC">By Location/Department</asp:ListItem>
                        <asp:ListItem Value="DC">By Dictator</asp:ListItem> 
                       <asp:ListItem Text="By Device" Value="DV"></asp:ListItem> 
         <asp:ListItem Text="By Template" Value="TW"></asp:ListItem> 
         <asp:ListItem Text="By TAT" Value="TT"></asp:ListItem> 
                     </asp:DropDownList></td>
            </tr> 
                <tr style="color: gray; ">
                <td style="text-align: right; text-align: right;">
                Default TAT</td>
                <td style="text-align: left; text-align: left; ">
                    <asp:DropDownList ID="TxtTAT" runat="server" Width="130px">
                        <asp:ListItem>2</asp:ListItem>
                        <asp:ListItem>4</asp:ListItem>
                        <asp:ListItem>6</asp:ListItem>
                        <asp:ListItem>8</asp:ListItem>
                        <asp:ListItem>12</asp:ListItem>
                        <asp:ListItem Selected="True">24</asp:ListItem>
                        <asp:ListItem>48</asp:ListItem>
                        <asp:ListItem>72</asp:ListItem>
                        <asp:ListItem>96</asp:ListItem>
                        <asp:ListItem>120</asp:ListItem>
                    </asp:DropDownList></td>
                <td style="width: 25%;text-align: right;">
                    Default STAT TAT</td>
                <td style=" text-align: left;">
                    <asp:DropDownList ID="TxtSTAT" runat="server" Width="130px">
                   <asp:ListItem>1</asp:ListItem>
                         <asp:ListItem>2</asp:ListItem>
                        <asp:ListItem  Selected="True">4</asp:ListItem>
                        <asp:ListItem>6</asp:ListItem>
                        <asp:ListItem>8</asp:ListItem>
                        <asp:ListItem>12</asp:ListItem>
                    </asp:DropDownList></td>
            </tr>
            <tr>
                <td  style="text-align: right" >
                   TimeZone
                </td>
                <td>
                                    <asp:DropDownList ID="DDLTZ" runat="server">
                                       <asp:ListItem Value="0" Text="Eastern Time (EST - Default)"></asp:ListItem>
                                       <asp:ListItem value="-1" text="Central Time (CST)"></asp:ListItem>
                                       <asp:ListItem value="-2" Text="Mountain Time (MST)"></asp:ListItem>
                                       <asp:ListItem value="-3" Text="Pacific Time (PST)"></asp:ListItem>
                                       <asp:ListItem value="-4" Text="Alaska Time (AKST)"></asp:ListItem>
                                       <asp:ListItem value="-6" Text="Hawaii Time"></asp:ListItem>
                                    </asp:DropDownList>
                                    
                  </td>
                  <td  style="text-align: right" >
                   DueTime(24Hrs)
                  </td>
                  <td>
                    <asp:TextBox ID="txtTime" runat="server" Text="" Width="20" MaxLength="2"></asp:TextBox>                                    
                  </td>
              </tr>                
               
          <tr>
                  <td style="text-align: right">
                      AutoFax</td>
                  <td>
                      <asp:DropDownList ID="DLFaxPlus" runat="server" Width="130px" AutoPostBack="True">
                          <asp:ListItem Selected="True" Value="0"   >No</asp:ListItem>
                          <asp:ListItem Value="1">Yes</asp:ListItem>
                      </asp:DropDownList></td>
                  <td style="text-align: right">
                      AutoFax Mode</td>
                  <td>
                      <asp:DropDownList ID="DLFMode" runat="server" Width="130px">
                          <asp:ListItem Selected="True" Value="0"  >Pending Signature</asp:ListItem>
                          <asp:ListItem  Value="1">Signed Report</asp:ListItem>
                           <asp:ListItem  Value="2">CC Only</asp:ListItem>
                          
                      </asp:DropDownList></td>
              </tr>
           <tr>
                  <td  style="text-align: right">
                      AutoFax - Transcription Exceptions</td>
                  <td >
                      <asp:DropDownList ID="DLFaxExcp" runat="server" Width="130px" AutoPostBack="True">
                          <asp:ListItem Value="0">Ignore</asp:ListItem>
                          <asp:ListItem Value="1">On Hold</asp:ListItem>
                      </asp:DropDownList></td>
                      <td  style="text-align: right">
                      FacilityID</td>
                  <td >
                  <asp:TextBox ID="TxtFacilityID" runat="server" ></asp:TextBox> 
                  </td> 
                      </tr> 
            <tr style="color: gray; ">
              
                
                       <td style="text-align: right; text-align: right; ">
                Dictation Source</td>
                <td style="text-align: left; text-align: left; ">
                <asp:DropDownList ID="DLVoice" runat="server" Width="130px">
                    <asp:ListItem Selected="True" Value="0">Default</asp:ListItem>
                    <asp:ListItem Value="1">Custom</asp:ListItem>
                </asp:DropDownList></td>
                <%--<td style="text-align: right; text-align: right; ">
                   *Instance
                </td>
                <td style="text-align: left; text-align: left; ">
                    <asp:DropDownList ID="DLInstance" runat="server" Width="130px">
                        <asp:ListItem Text="Please Select" Value="" Selected="True"></asp:ListItem>
                        <asp:ListItem Text="1" Value="1"></asp:ListItem>
                        <asp:ListItem Text="2" Value="2"></asp:ListItem>
                    </asp:DropDownList>
                </td>--%>
                <td style="height: 26px; text-align: right">
                    <span style="font-family: Arial">Process outside SecureXFlow</span></td>
                      <td style="text-align: left; text-align: left; ">
                <asp:DropDownList ID="DLIndirect" runat="server" Width="130px">
                    <asp:ListItem Selected="True"   Value="0">No</asp:ListItem>
                    <asp:ListItem Value="1">Yes</asp:ListItem>
                </asp:DropDownList></td>
            </tr>
            <tr style="color: gray; ">
                <td style="text-align: right;">MIS Report</td>
                <td>
                    <asp:DropDownList ID="ddlMIS" runat="server" Width="130px">
                        <asp:ListItem Value="">Please Select</asp:ListItem>
                        <asp:ListItem Value="True">Yes</asp:ListItem>
                        <asp:ListItem Value="False">No</asp:ListItem>
                    </asp:DropDownList>
                </td>
               
              
                          <td style="height: 26px; text-align: right">
                    <span style="font-family: Arial">Website</span></td>
                      <td style="text-align: left; text-align: left; ">
                <asp:DropDownList ID="DLWebsite" runat="server" Width="130px">
                    <asp:ListItem Value="SXF">SecureXFlow</asp:ListItem>
                    <asp:ListItem Value="NMBS">NMBS</asp:ListItem>
                </asp:DropDownList></td>
            </tr>

            <tr style="color: gray; ">
                <td style="height: 60px; text-align: right;">
                    Comments</td>
                <td colspan="3" style="height: 60px; text-align: left">
                    <asp:TextBox ID="txtComments" runat="server" Height="80px" TextMode="MultiLine" Width="392px"></asp:TextBox></td>
            </tr>
                
                  <tr style="color: gray; ">
                <td colspan="2" style="height: 21px; text-align: center"  class="alt">
                    Primary Contact Details
                </td>
                <td colspan="2" style="height: 21px; text-align: center"  class="alt">
                    Secondary Contact Details</td>
            </tr>
            <tr style="color: gray; ">
                <td style="text-align: right; text-align: right; height: 21px;">
                    Name</td>
                <td style=" text-align: left; height: 21px;">
                    <asp:TextBox ID="TxtPriContact" runat="server"></asp:TextBox></td>
                <td style="text-align: right; text-align: right; height: 21px;">
                Name</td>
                <td style="text-align: left; text-align: left; height: 21px;">
                    <asp:TextBox ID="TxtSecContact" runat="server"></asp:TextBox></td>
            </tr>
            <tr style="color: gray; ">
                <td style="text-align: right; text-align: right; height: 21px;">
                    Title</td>
                <td style=" text-align: left; height: 21px;">
                    <asp:TextBox ID="TxtPriTitle" runat="server"></asp:TextBox></td>
                <td style="text-align: right; text-align: right; height: 21px;">
                Title</td>
                <td style="text-align: left; text-align: left; height: 21px;">
                    <asp:TextBox ID="TxtSecTitle" runat="server"></asp:TextBox></td>
            </tr>
            <tr style="color: gray; ">
                <td style="text-align: right; text-align: right;">
                E-Mail</td>
                <td style="height: 22px; text-align: left; ">
                    <asp:TextBox ID="TxtPriEmail" runat="server"></asp:TextBox>
                </td>
                <td style="height: 22px; text-align: right;">
                    E-Mail</td>
                <td style="height: 22px; text-align: left;">
                    <asp:TextBox ID="TxtSecEmail" runat="server"></asp:TextBox></td>
            </tr>
            <tr style="color: gray; ">
                <td style="text-align: right; text-align: right;">
                    Phone Number</td>
                <td style="height: 22px; text-align: left; ">
                    <asp:TextBox ID="txtPriPhone" runat="server"></asp:TextBox></td>
                <td style="height: 22px; text-align: right;">
                    Phone Number</td>
                <td style="height: 22px; text-align: left;">
                    <asp:TextBox ID="txtSecPhone" runat="server"></asp:TextBox></td>
            </tr>
            <tr style="color: gray; ">
                <td style="text-align: right; height: 30px; text-align: right;">
                Fax</td>
                <td style="height: 21px; height: 30px; text-align: left; ">
                    <asp:TextBox ID="txtPriFaxNo" runat="server"></asp:TextBox></td>
                <td style="height: 21px; text-align: right; height: 30px;">
                Fax</td>
                <td style="height: 21px; height: 30px; text-align: left;">
                    <asp:TextBox ID="txtSecFaxNo" runat="server"></asp:TextBox></td>
            </tr> 
            <tr style="color: gray; ">
                <td style="text-align: center; height: 26px;" colspan="4">
                    <asp:Button ID="Button1" cssClass="button"  runat="server" Text="Submit" /></td>
            </tr>
        </table>
      
      <asp:Label ID="MsgDisp" runat="server" CssClass="Title" ForeColor="#C00000" Height="16px" ></asp:Label>
      <asp:RequiredFieldValidator  Display="None" ControlToValidate="TxtAccountName"  ID="RequiredFieldValidator1"  runat="server" ErrorMessage="Please enter Account Name" ></asp:RequiredFieldValidator><br />
      <asp:RequiredFieldValidator  Display="None" ControlToValidate="Txtdescription"  ID="RequiredFieldValidator4"  runat="server" ErrorMessage="Please enter Account Description" ></asp:RequiredFieldValidator><br />
      <asp:RequiredFieldValidator  Display="None" ControlToValidate="TxtFolder"  ID="RequiredFieldValidator2"  runat="server" ErrorMessage="Please enter Folder Name" ></asp:RequiredFieldValidator><br />
      <asp:RequiredFieldValidator  Display="None" ControlToValidate="TxtBillNumber"  ID="RequiredFieldValidator3"  runat="server" ErrorMessage="Please enter Billing Account Number" ></asp:RequiredFieldValidator> <br />
      <%--<asp:RequiredFieldValidator  Display="None" ControlToValidate="DLInstance"  ID="RequiredFieldValidator5"  runat="server" ErrorMessage="Please enter instance for account" ></asp:RequiredFieldValidator> <br />--%>
    </asp:Panel>
       <asp:Label ID="lblMsg" runat="server" ForeColor="#C00000" CssClass="Title"    ></asp:Label> 
       </div>
       </div>
        <div style="text-align:center">   
        <asp:ValidationSummary ID="ValidationSummary1" runat="server" HeaderText="Following error occurs:" ShowMessageBox="true" DisplayMode="BulletList" ShowSummary="False" />      
        <asp:RegularExpressionValidator  Display="None"
    id="RegTxtAccountName"  
    runat="server" 
    ControlToValidate="TxtAccountName" 
    ValidationExpression="^[0-9a-zA-Z-,. ]+$"
    ErrorMessage="Account Name - Please enter valid input."
   />
    <asp:RegularExpressionValidator  Display="None"
    id="RegTxtDescription"  
    runat="server" 
    ControlToValidate="TxtDescription" 
    ValidationExpression="^[0-9a-zA-Z-,. ]+$"
    ErrorMessage="Description - Please enter valid input."
   />
 <asp:RegularExpressionValidator  Display="None"
    id="RegTxtFolder"  
    runat="server" 
    ControlToValidate="TxtFolder" 
    ValidationExpression="^[0-9a-zA-Z]+$"
    ErrorMessage="Processing Folder Name - Please enter valid input."
   />
 <asp:RegularExpressionValidator  Display="None"
    id="RegTxtBillNumber"  
    runat="server" 
    ControlToValidate="TxtBillNumber" 
    ValidationExpression="^[0-9a-zA-Z-]+$"
    ErrorMessage="Billing Account Number - Please enter valid input."
   />
 <asp:RegularExpressionValidator  Display="None"
    id="RegTXTState"  
    runat="server" 
    ControlToValidate="TXTState" 
    ValidationExpression="^[0-9a-zA-Z-,. ]+$"
    ErrorMessage="State - Please enter valid input."
   />
 <asp:RegularExpressionValidator  Display="None"
    id="RegTXTZip"  
    runat="server" 
    ControlToValidate="TXTZip" 
    ValidationExpression="^[0-9-]+$"
    ErrorMessage="Zip - Please enter valid input."
   />
 <asp:RegularExpressionValidator  Display="None"
    id="RegTxtOfficialSite"  
    runat="server" 
    ControlToValidate="TxtOfficialSite" 
    ValidationExpression="^[a-zA-Z-,.@ ]+$"
    ErrorMessage="Official Website - Please enter valid input."
   />
 <asp:RegularExpressionValidator  Display="None"
    id="RegTxtProtocolMins"  
    runat="server" 
    ControlToValidate="TxtProtocolMins" 
    ValidationExpression="^[0-9]+$"
    ErrorMessage="Expected Daily Minutes - Please enter valid input."
   />
<asp:RegularExpressionValidator  Display="None"
    id="RegtxtTime"  
    runat="server" 
    ControlToValidate="txtTime" 
    ValidationExpression="^[0-9]+$"
    ErrorMessage="DueTime(24Hrs) - Please enter valid input."
   /> 
<asp:RegularExpressionValidator  Display="None"
    id="RegTxtPriContact"  
    runat="server" 
    ControlToValidate="TxtPriContact" 
    ValidationExpression="^[a-zA-Z-,. ]+$"
    ErrorMessage="Primary Contact Name - Please enter valid input."
   />
<asp:RegularExpressionValidator  Display="None"
    id="RegTxtSecContact"  
    runat="server" 
    ControlToValidate="TxtSecContact" 
    ValidationExpression="^[a-zA-Z-,. ]+$"
    ErrorMessage="Secondary Contact Name - Please enter valid input."
   />
<asp:RegularExpressionValidator  Display="None"
    id="RegTxtPriTitle"  
    runat="server" 
    ControlToValidate="TxtPriTitle" 
    ValidationExpression="^[a-zA-Z-,. ]+$"
    ErrorMessage="Primary Contact Title - Please enter valid input."
   />
<asp:RegularExpressionValidator  Display="None"
    id="RegTxtSecTitle"  
    runat="server" 
    ControlToValidate="TxtSecTitle" 
    ValidationExpression="^[a-zA-Z-,. ]+$"
    ErrorMessage="Secondary Contact Title - Please enter valid input."
   />

<asp:RegularExpressionValidator  Display="None"
    id="RegTxtPriEmail"  
    runat="server" 
    ControlToValidate="TxtPriEmail" 
    
    ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"
    ErrorMessage="Primary Contact E-Mail - Please enter valid input."
   />
<asp:RegularExpressionValidator  Display="None"
    id="RegTxtSecEmail"  
    runat="server" 
    ControlToValidate="TxtSecEmail" 
    ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"
    ErrorMessage="Secondary Contact E-Mail - Please enter valid input."
   />

<asp:RegularExpressionValidator  Display="None"
    id="RegtxtPriPhone"  
    runat="server" 
    ControlToValidate="txtPriPhone" 
    ValidationExpression="^[0-9-]+$"
    ErrorMessage="Primary Contact Phone Number - Please enter valid input."
   />
<asp:RegularExpressionValidator  Display="None"
    id="RegtxtSecPhone"  
    runat="server" 
    ControlToValidate="txtSecPhone" 
    ValidationExpression="^[0-9-]+$"
    ErrorMessage="Secondary Contact Phone Number - Please enter valid input."
   />

<asp:RegularExpressionValidator  Display="None"
    id="RegtxtPriFaxNo"  
    runat="server" 
    ControlToValidate="txtPriFaxNo" 
    ValidationExpression="^[0-9-]+$"
    ErrorMessage="Primary Contact Fax Number - Please enter valid input."
   />
<asp:RegularExpressionValidator  Display="None"
    id="RegtxtSecFaxNo"  
    runat="server" 
    ControlToValidate="txtSecFaxNo" 
    ValidationExpression="^[0-9-]+$"
    ErrorMessage="Secondary Contact Fax Number - Please enter valid input."
   />
</div>

    </form>
    
</body>
</html>
