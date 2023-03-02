<%@ Page Language="VB" AutoEventWireup="false" CodeFile="NewUserReg.aspx.vb" Inherits="Department_Default" %>

<%@ Register Assembly="System.Web.Extensions, Version=1.0.61025.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI" TagPrefix="asp" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 1.0 Transitional/EN">


<html xmlns="http://www.w3.org/1999/xhtml" >

<head runat="server">
<link href="../StyleSheet.css" rel="stylesheet" type="text/css" />
<link href= "../../../../styles/Default.css" type="text/css" rel="stylesheet"/>
    <title>Untitled Page</title>
</head>
<body>
    
    <form id="form1" runat="server">
   
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
 
       <ajaxToolkit:Accordion ID="MyAccordion" runat="server" SelectedIndex="0" Width="90%"
            HeaderCssClass="accordionHeader" HeaderSelectedCssClass="accordionHeaderSelected"
            ContentCssClass="accordionContent" FadeTransitions="true" FramesPerSecond="40" 
            TransitionDuration="250" AutoSize="None" RequireOpenedPane="false" SuppressHeaderPostbacks="true">
           <Panes>
            <ajaxToolkit:AccordionPane ID="AccordionPane1" runat="server">
                <Header>Personal Data Form</Header>
                <Content>
       <table id="Table1" border="2" cellpadding="2"  width="100%" style="font-size: 10pt; font-family: 'Trebuchet MS'; font-style: italic; color:Gray; " >
            <tr>
                <td colspan="4" style="text-align: center;" class="HeaderDiv">
                   <em><strong><span style="font-family: Trebuchet MS">User Registration Form</span></strong></em></td>
            </tr>
            <tr style="font-size: 10pt; font-style: italic; font-family: Trebuchet MS">
                <td style="width: 25%; text-align: right; text-align: right;">
                    <span>*First Name</span></td>
                <td style="width: 337px; text-align: left;">
                    <asp:TextBox ID="TxtFirstName" runat="server"></asp:TextBox></td>
                <td style="width: 25%; text-align: right; text-align: right;">
                    <span>*Last Name</span></td>
                <td style="width: 25%; text-align: left;">
                    <asp:TextBox ID="TxtLastName" runat="server"></asp:TextBox></td>
            </tr>
            <tr style="font-size: 10pt; font-style: italic; font-family: Trebuchet MS">
                <td style="width: 25%; text-align: right; text-align: right;">
                    <span>
                    E-Dictate Mail ID</span></td>
                <td style="width: 337px; height: 21px; text-align: left;">
                    <asp:TextBox ID="TxtEmail" runat="server"></asp:TextBox></td>
                <td style="width: 25%; height: 21px; text-align: right;">
                    <span>
                    Yahoo Chat ID</span></td>
                <td style="width: 25%; height: 21px; text-align: left;">
                    <asp:TextBox ID="TxtChatID" runat="server"></asp:TextBox></td>
            </tr>
            <tr style="font-size: 10pt; font-style: italic; font-family: Trebuchet MS">
                <td style="width: 25%; text-align: right; text-align: right;">
                <span>
                    Non E-Dictate Mail ID</span></td>
                <td style="width: 337px; text-align: left;">
                    <asp:TextBox ID="TxtNonOEmail" runat="server"></asp:TextBox></td>
                <td style="width: 25%; text-align: right; text-align: right;">
                <span>
                    Joining Date</span></td>
                <td style="width: 25%; text-align: left; text-align: left;">
                    <asp:TextBox ID="TxtJoin" runat="server"></asp:TextBox></td>
            </tr>
            <tr style="font-size: 10pt; font-style: italic; font-family: Trebuchet MS">
                <td style="width: 25%; text-align: right; text-align: right;">
                <span>
                    City</span></td>
                <td style="width: 337px; height: 22px; text-align: left;">
                    <asp:TextBox ID="TxtCity" runat="server"></asp:TextBox></td>
                <td style="width: 25%; height: 22px; text-align: right;">
                    <span>
                    Country</span></td>
                <td style="width: 25%; height: 22px; text-align: left;">
                    <asp:DropDownList ID="txtCountry" runat="server">
                        <asp:ListItem Selected="True">India</asp:ListItem>
                        <asp:ListItem>USA</asp:ListItem>
                        <asp:ListItem>UK</asp:ListItem>
                    </asp:DropDownList></td>
            </tr>
            <tr style="font-size: 10pt; font-style: italic; font-family: Trebuchet MS">
                <td style="width: 25%; text-align: right; height: 30px; text-align: right;">
                <span>
                    State
                    </span></td>
                <td style="width: 337px; height: 21px; height: 30px; text-align: left;">
                    <asp:TextBox ID="TxtState" runat="server"></asp:TextBox></td>
                <td style="width: 25%; height: 21px; text-align: right; height: 30px;">
                <span>
                    Address</span>        </td>
                <td style="width: 25%; height: 21px; height: 30px; text-align: left;">
                    <asp:TextBox ID="TxtAdd" runat="server"></asp:TextBox></td>
            </tr>
            <tr style="font-size: 10pt; font-style: italic; font-family: Trebuchet MS">
                <td style="width: 25%; text-align: right; text-align: right;">
                <span>
                    Date Of Birth</span></td>
                <td style="width: 337px; height: 26px; text-align: left;">
                <asp:TextBox runat="server" ID="TxtDOB"/>
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
                <td style="width: 25%; height: 26px; text-align: right;">
                <span>
                    Tel#</span></td>
                <td style="width: 25%; height: 26px; text-align: left;">
                    <asp:TextBox ID="TxtTel" runat="server"></asp:TextBox></td>
            </tr>
            <tr style="font-size: 10pt; font-style: italic; font-family: Trebuchet MS">
                <td style="width: 25%; text-align: right; text-align: right;">
                <span>
                    Cell#</span></td>
                <td style="width: 337px; text-align: left; height: 26px; text-align: left;">
                    <asp:TextBox ID="TxtCell" runat="server"></asp:TextBox>
                     <ajaxToolkit:FilteredTextBoxExtender
                        ID="FilteredTextBoxExtender1"
                        runat="server"
                        TargetControlID="TxtCell"
                    FilterType="Numbers" /></td>
                <td style="width: 25%; text-align: right; height: 26px; text-align: right;">
                <span>
                    Department</span></td>
                <td style="width: 25%; height: 26px; text-align: left;">
                    <asp:DropDownList ID="TxtDept" runat="server" >
                    </asp:DropDownList>
                    <ajaxToolkit:CascadingDropDown ID="CascadingDropDown1" runat="server" TargetControlID="TxtDept" Category="Make"  PromptText="Please select Department"  ServicePath="../AutoComplete.asmx" ServiceMethod="GetDepartment" PromptValue="PromptValue"/>
                    </td>
            </tr>
            <tr style="font-size: 10pt; font-style: italic; font-family: Trebuchet MS">
                <td style="width: 25%; text-align: right; text-align: right; ">
                <span>
                    Designation</span></td>
                <td style="width: 337px; text-align: left; text-align: left;"><asp:DropDownList ID="TxtDesi" runat="server" ></asp:DropDownList>
               <ajaxToolkit:CascadingDropDown ID="CascadingDropDown2" runat="server" TargetControlID="TxtDesi" Category="Model" PromptText="Please select Designation" ServicePath="../AutoComplete.asmx" ServiceMethod="GetDepartmentDesn" ParentControlID="TxtDept" LoadingText="Loading.." /> 
                <asp:SqlDataSource ID="DSDeptDesn" runat="server" ConnectionString="<%$ ConnectionStrings:ETSConnectionString %>"
                    SelectCommand="SELECT DISTINCT [DesignationID], [Name] FROM [tblDeptDesignations] WHERE ([DepartmentID] = @DepartmentID)">
                    <SelectParameters>
                        <asp:ControlParameter ControlID="TxtDept" Name="DepartmentID" PropertyName="SelectedValue"
                            Type="String" />
                    </SelectParameters>
                </asp:SqlDataSource>
                   </td>
                <td style="width: 25%;text-align: right;">
                    <span>Category</span></td>
                <td style="width: 25%;  text-align: left;"><asp:DropDownList ID="txtCate" runat="server">
                    <asp:ListItem Selected="True">Office Employee</asp:ListItem>
                    <asp:ListItem>HBE</asp:ListItem>
                    <asp:ListItem>HBA</asp:ListItem>
                    <asp:ListItem>Global HBA</asp:ListItem>
                    <asp:ListItem>Contractor</asp:ListItem>
                </asp:DropDownList></td>
            </tr>
            <tr style="font-size: 10pt; font-style: italic; font-family: Trebuchet MS">
                <td style="width: 25%; text-align: right">
                    <asp:CheckBox ID="ChkUn" runat="server" Checked="True" AutoPostBack="True" /></td>
                <td style="width: 337px;  text-align: left">
                    <span>Same as Employee Code</span></td>
                <td style="width: 25%;  text-align: right">
                    <span>
                        <asp:Label ID="LblUN" runat="server" Font-Names="Trebuchet MS" Font-Size="10pt"
                            Text="Username"></asp:Label></span></td>
                <td style="width: 25%; text-align: left;">
                    <asp:TextBox ID="TxtUname" runat="server"></asp:TextBox></td>
            </tr>
           
        </table>

      </Content>
            </ajaxToolkit:AccordionPane>
        
      <ajaxToolkit:AccordionPane ID="AccordionPane2" runat="server">
                <Header>Professional/Academic Details</Header>
                <Content>
        <table width="80%" border="2" cellpadding="2" style="font-size: 10pt; font-family: 'Trebuchet MS'; font-style: italic; color:Gray; ">
            <tr>
                <td colspan="4" style="text-align: center; " class="HeaderDiv">
                    <strong><em><span style="font-family: Trebuchet MS">User Registration
                        Form</span></em></strong></td>
            </tr>
            <tr style="font-size: 10pt; font-style: italic; font-family: Trebuchet MS">
                <td colspan="4" style="text-align: center;">
                    <strong>
                 Professional
                        Information</strong></td>
            </tr>
            <tr style="font-size: 10pt; font-family: Trebuchet MS; font-style: italic;">
                <td style="width: 162px; text-align: center;">
                    <span>Company Name</span></td>
                <td style="width: 89px; text-align: center;">
                    <span>Experience</span></td>
                <td style="width: 25%; text-align: right; text-align: center;">
                    <span>Designation</span></td>
                <td style="width: 25%; text-align: center;">
                    <span>Key Skills</span></td>
            </tr>
            <tr style="font-size: 10pt; font-family: Trebuchet MS; font-style: italic;">
                <td style="width: 162px; text-align: center;">
                    <asp:TextBox ID="TxtComp1" runat="server"></asp:TextBox></td>
                <td style="width: 25%; height: 21px; text-align: center;">
                    <asp:TextBox ID="TxtExp1" runat="server"></asp:TextBox></td>
                <td style="width: 25%; height: 21px; text-align: center;">
                    <asp:TextBox ID="TxtDesi1" runat="server"></asp:TextBox></td>
                <td style="width: 25%; height: 21px; text-align: center;">
                    <asp:TextBox ID="TxtSkill1" runat="server"></asp:TextBox></td>
            </tr>
            <tr style="font-size: 10pt; font-style: italic; font-family: Trebuchet MS">
                <td style="width: 162px; height: 20px; text-align: center;">
                    <asp:TextBox ID="TxtComp2" runat="server"></asp:TextBox></td>
                <td style="width: 25%; height: 20px; text-align: center;">
                    <asp:TextBox ID="TxtExp2" runat="server"></asp:TextBox></td>
                <td style="width: 25%; text-align: right; height: 20px; text-align: center;">
                    <asp:TextBox ID="TxtDesi2" runat="server"></asp:TextBox></td>
                <td style="width: 25%; text-align: left; height: 20px; text-align: center;">
                    <asp:TextBox ID="TxtSkill2" runat="server"></asp:TextBox></td>
            </tr>
            <tr style="font-size: 10pt; font-style: italic; font-family: Trebuchet MS">
                <td style="width: 162px; text-align: center;">
                    <asp:TextBox ID="TxtComp3" runat="server"></asp:TextBox></td>
                <td style="width: 25%; height: 22px; text-align: center;">
                    <asp:TextBox ID="TxtExp3" runat="server"></asp:TextBox></td>
                <td style="width: 25%; height: 22px; text-align: center;">
                    <asp:TextBox ID="TxtDesi3" runat="server"></asp:TextBox></td>
                <td style="width: 25%; height: 22px; text-align: center;">
                    <asp:TextBox ID="TxtSkill3" runat="server"></asp:TextBox></td>
            </tr>
            <tr style="font-size: 10pt; font-style: italic; font-family: Trebuchet MS">
                <td style="width: 162px; text-align: center;">
                    <asp:TextBox ID="TxtComp4" runat="server"></asp:TextBox></td>
                <td style="width: 25%; height: 21px; text-align: center;">
                    <asp:TextBox ID="TxtExp4" runat="server"></asp:TextBox></td>
                <td style="width: 25%; height: 21px; text-align: center;">
                    <asp:TextBox ID="TxtDesi4" runat="server"></asp:TextBox></td>
                <td style="width: 25%; height: 21px; text-align: center;">
                    <asp:TextBox ID="TxtSkill4" runat="server"></asp:TextBox></td>
            </tr>
            <tr style="font-size: 10pt; font-style: italic; font-family: Trebuchet MS">
                <td style="width: 162px; text-align: center;">
                    <asp:TextBox ID="TxtComp5" runat="server"></asp:TextBox></td>
                <td style="width: 25%; height: 26px; text-align: center;">
                    <asp:TextBox ID="TxtExp5" runat="server"></asp:TextBox></td>
                <td style="width: 25%; height: 26px; text-align: center;">
                    <asp:TextBox ID="TxtDesi5" runat="server"></asp:TextBox></td>
                <td style="width: 25%; height: 26px; text-align: center;">
                    <asp:TextBox ID="TxtSkill5" runat="server"></asp:TextBox></td>
            </tr>
            <tr style="font-size: 10pt; font-style: italic; font-family: Trebuchet MS">
                <td style="text-align: right; height: 26px;" colspan="4">
                </td>
            </tr>
            <tr style="font-size: 10pt; font-style: italic; font-family: Trebuchet MS">
                <td style="text-align: center; height: 24px; " colspan="4">
                    <strong>
                    Academic Details</strong></td>
            </tr>
            <tr style="font-size: 10pt; font-style: italic; font-family: Trebuchet MS">
                <td style="width: 162px; text-align: center">
                    <span>Qualification</span></td>
                <td style="width: 25%; height: 24px; text-align: center">
                    <span>Institute</span></td>
                <td style="width: 25%; height: 24px; text-align: center;">
                    <span>Year Of Passing</span></td>
                <td style="width: 25%; height: 24px; text-align: center;">
                    <span>Percentage</span></td>
            </tr>
            <tr style="font-size: 10pt; font-style: italic; font-family: Trebuchet MS">
                <td style="width: 162px; text-align: center">
                    <asp:TextBox ID="TxtQua1" runat="server"></asp:TextBox></td>
                <td style="width: 25%; height: 24px; text-align: center">
                    <asp:TextBox ID="TxtInst1" runat="server"></asp:TextBox></td>
                <td style="width: 25%; height: 24px; text-align: center;">
                    <asp:TextBox ID="TxtYrsPass1" runat="server"></asp:TextBox></td>
                <td style="width: 25%; height: 24px; text-align: center;">
                    <asp:TextBox ID="TxtPerc1" runat="server"></asp:TextBox></td>
            </tr>
            <tr style="font-size: 10pt; font-style: italic; font-family: Trebuchet MS">
                <td style="width: 162px; text-align: center">
                    <asp:TextBox ID="TxtQua2" runat="server"></asp:TextBox></td>
                <td style="width: 25%; height: 24px; text-align: center">
                    <asp:TextBox ID="TxtInst2" runat="server"></asp:TextBox></td>
                <td style="width: 25%; height: 24px; text-align: center;">
                    <asp:TextBox ID="TxtYrsPass2" runat="server"></asp:TextBox></td>
                <td style="width: 25%; height: 24px; text-align: center;">
                    <asp:TextBox ID="TxtPerc2" runat="server"></asp:TextBox></td>
            </tr>
            <tr style="font-size: 10pt; font-style: italic; font-family: Trebuchet MS">
                <td style="width: 162px; text-align: center">
                    <asp:TextBox ID="TxtQua3" runat="server"></asp:TextBox></td>
                <td style="width: 25%; height: 24px; text-align: center">
                    <asp:TextBox ID="TxtInst3" runat="server"></asp:TextBox></td>
                <td style="width: 25%; height: 24px; text-align: center;">
                    <asp:TextBox ID="TxtYrsPass3" runat="server"></asp:TextBox></td>
                <td style="width: 25%; height: 24px; text-align: center;">
                    <asp:TextBox ID="TxtPerc3" runat="server"></asp:TextBox></td>
            </tr>
            <tr style="font-size: 10pt; font-style: italic; font-family: Trebuchet MS">
                <td style="width: 162px; text-align: center">
                    <asp:TextBox ID="TxtQua4" runat="server"></asp:TextBox></td>
                <td style="width: 25%; height: 10px; text-align: center">
                    <asp:TextBox ID="TxtInst4" runat="server"></asp:TextBox></td>
                <td style="width: 25%; height: 10px; text-align: center;">
                    <asp:TextBox ID="TxtYrsPass4" runat="server"></asp:TextBox></td>
                <td style="width: 25%; height: 10px; text-align: center;">
                    <asp:TextBox ID="TxtPerc4" runat="server"></asp:TextBox></td>
            </tr>
            
            </table> 
             </Content>
            </ajaxToolkit:AccordionPane>
         <ajaxToolkit:AccordionPane ID="AccordionPane3" runat="server">
                <Header>Resume/Photo Upload</Header>
                <Content>
        
        <table width="80%" border="2" cellpadding="2" style="font-size: 10pt; font-family: 'Trebuchet MS'; font-style: italic; color:Gray; ">
            <tr>
                <td colspan="4" class="HeaderDiv" style="text-align: center"><I>User Registration Form</I></td>
            </tr>
                    <tr>
                        <td colspan="4" style="height: 24px; text-align: center" >
                        <span><strong>Upload Files</strong></span></td>
                </tr>
                <tr>
                    <td style="width: 25%; text-align: right;  text-align: right;">
                        <span>Submit Photo</span></td>
                    <td colspan="3" style=" text-align: left">
                        <asp:FileUpload ID="FileUpload1" runat="server" /></td>
                </tr>
           <tr>
            <td colspan="4">
            </td>
            </tr>
            </table>
        
            <asp:Label ID="Label2" runat="server" Text=""></asp:Label><br />
   
   </Content>
            </ajaxToolkit:AccordionPane>
             </Panes>
        </ajaxToolkit:Accordion>
      
        <table width="80%" border="2" cellpadding="2" style="font-size: 10pt; font-family: 'Trebuchet MS'; font-style: italic; color:Gray; ">
          
                <tr>
                    <td colspan="4" style="text-align: center">
                        <asp:Button ID="Button4" runat="server"  Text="Submit" />
                        <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional"  >
                        <ContentTemplate>
                        </ContentTemplate> 
                        <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="Button4" EventName="Click" />
                        
                           
                        </Triggers> 
                        </asp:UpdatePanel>
                        </td>
                </tr>
    
                </table>
           </form>  
    
</body>
</html>
