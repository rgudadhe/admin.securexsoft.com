<%@ Page Language="VB" AutoEventWireup="false" CodeFile="OtherInfo.aspx.vb" Inherits="CreateUser_OtherInfo" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>User Other Information</title>
    <link href= "../App_Themes/Css/styles.css" type="text/css" rel="stylesheet" />
    <link href= "../App_Themes/Css/Common.css" type="text/css" rel="stylesheet" />
    <link href="../App_Themes/Css/Calendar.css" type="text/css" rel="stylesheet" />

    <script type="text/javascript" language="javascript">
        function Chk()
        {
            if(document.getElementById('ddlUsrs').value=='')
            {
                alert('Field User is required');
                return false;
            }
            if(document.getElementById('txtSSalary').value=='')
            {
                alert('Field Start Salary is required');
                return false;
            }
            if(document.getElementById('txtCSalary').value=='')
            {
                alert('Field Current Salary is required');
                return false;
            }
            if(document.getElementById('txtPF').value=='')
            {
                alert('Field PF Number is required');
                return false;
            }
            if(document.getElementById('txtPAN').value=='')
            {
                alert('Field PAN Number is required');
                return false;
            }
            if(document.getElementById('txtBankAccNo').value=='')
            {
                alert('Field Bank Account Number is required ');
                return false;
            }
            if(document.getElementById('txtBankName').value=='')
            {
                alert('Field Bank Name is required  ');
                return false;
            }
            if(document.getElementById('txtIFSC').value=='')
            {
                alert('Field Branch IFSC Code is required  ');
                return false;
            }
            return true;
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
    <div>
    <div id="body">
    <div id="cap"></div>
    <div id="main" style="text-align:left;  " >
    <h1> Personnel Information </h1>
        <%--<ajaxToolkit:Accordion ID="Accordion1" runat="server">
            <Panes>
                <ajaxToolkit:AccordionPane ID="Professional" runat="server">
                    <Header>--%>
                        <table width="100%">
                            <tr>
                                <td style="text-align:left; border:0">
                                    <asp:DropDownList ID="ddlUsrs" AutoPostBack="true" runat="server">
                                    </asp:DropDownList>
                                </td>
                                <td style="text-align:right; border:0">
                                    <asp:LinkButton ID="LnkExport" runat="server">Export All Users Data</asp:LinkButton>
                                </td>
                            </tr>
                        </table>
                        <table width="100%">
                            <tr>
                                <td colspan="4" class="alt1" style="text-align:left;">Professional Information</td>
                            </tr>
                        </table> 
                    <%--</Header>
                    <Content>--%>
                        <table width="100%">
                            <tr>
                                <td>Date Of Confirmation</td>
                                <td>
                                    <asp:TextBox ID="txtDtCon" runat="server"></asp:TextBox>
                                    <asp:ImageButton ID="imgSDate" CausesValidation="false" ImageUrl="~/App_Themes/Images/Calendar_scheduleHS.png" runat="server"/> &nbsp &nbsp
                                    <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" Format="MM/dd/yyyy" TargetControlID="txtDtCon" PopupButtonID="imgSDate" BehaviorID="CalendarExtender1" CssClass="cal_Theme1" Enabled="True">
                                    </ajaxToolkit:CalendarExtender>                                    
                                </td>
                                <td>Status Of Confirmation</td>
                                <td><asp:TextBox ID="txtConfirmStatus" runat="server"></asp:TextBox></td>
                            </tr>
                            <tr>
                                <td>Start Salary</td>
                                <td><asp:TextBox ID="txtSSalary" runat="server"></asp:TextBox></td>
                                <td>Increments Enjoyed</td>
                                <td><asp:TextBox ID="txtICount" runat="server"></asp:TextBox></td>
                            </tr>
                            <tr>
                                <td>Increments Description</td>
                                <td><asp:TextBox ID="txtIncrementsDesc" runat="server"></asp:TextBox></td>
                                <td>Current Salary</td>
                                <td><asp:TextBox ID="txtCSalary" runat="server"></asp:TextBox></td>
                            </tr>
                            <tr>
                                <td>Insurance Status</td>
                                <td><asp:TextBox ID="txtInsStatus" runat="server"></asp:TextBox></td>
                                <td>Insurance Code</td>
                                <td><asp:TextBox ID="txtInsCode" runat="server"></asp:TextBox></td>
                            </tr>
                            <tr>
                                <td>Family Insurance Details</td>
                                <td><asp:TextBox ID="txtInsDetails" runat="server"></asp:TextBox></td>
                                <td>PF #</td>
                                <td><asp:TextBox ID="txtPF" runat="server"></asp:TextBox></td>
                            </tr>
                            <tr>
                                <td>PAN Card #</td>
                                <td><asp:TextBox ID="txtPAN" runat="server"></asp:TextBox></td>
                                <td>Passport #</td>
                                <td><asp:TextBox ID="txtPassport" runat="server"></asp:TextBox></td>
                            </tr>
                            <tr>
                                <td>Bank A/C #</td>
                                <td><asp:TextBox ID="txtBankAccNo" runat="server"></asp:TextBox></td>
                                <td>Bank Name</td>
                                <td><asp:TextBox ID="txtBankName" runat="server"></asp:TextBox></td>
                            </tr>
                            <tr>
                                <td>Branh IFSC Code</td>
                                <td><asp:TextBox ID="txtIFSC" runat="server"></asp:TextBox></td>
                                <td>Induction Status</td>
                                <td><asp:TextBox ID="txtInductionStatus" runat="server"></asp:TextBox></td>
                                
                            </tr>
                            <tr>
                                <td>Induction By</td>
                                <td><asp:TextBox ID="txtInductionBy" runat="server"></asp:TextBox></td>
                                <td>Induction Conducted On</td>
                                <td>
                                    <asp:TextBox ID="txtInductionOn" runat="server"></asp:TextBox>
                                    <asp:ImageButton ID="imgEDate" ImageUrl="~/App_Themes/Images/Calendar_scheduleHS.png" CausesValidation="false" runat="server" />
                                    <ajaxToolkit:CalendarExtender ID="CalendarExtender2" runat="server" Format="MM/dd/yyyy" TargetControlID="txtInductionOn" PopupButtonID="imgEDate" BehaviorID="CalendarExtender2" CssClass="cal_Theme1" Enabled="True">
                                    </ajaxToolkit:CalendarExtender>
                                </td>
                                
                            </tr>
                            <tr>
                                <td>Documentation Verification Status</td>
                                <td>
                                    <asp:DropDownList ID="ddlVerify" runat="server">
                                        <asp:ListItem Text="No" Value="No"></asp:ListItem>
                                        <asp:ListItem Text="Yes" Value="Yes"></asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                                <td>Documents Verify By</td>
                                <td><asp:TextBox ID="txtDocVerify" runat="server"></asp:TextBox></td>
                                
                            </tr>
                            <tr>
                                <td>Access Card</td>
                                <td>
                                    <asp:DropDownList ID="ddlAccessCard" runat="server">
                                        <asp:ListItem Text="No" Value="No"></asp:ListItem>
                                        <asp:ListItem Text="Yes" Value="Yes"></asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                                <td>Employee I Card</td>
                                <td>
                                    <asp:DropDownList ID="ddlEmpICard" runat="server">
                                        <asp:ListItem Text="No" Value="No"></asp:ListItem>
                                        <asp:ListItem Text="Yes" Value="Yes"></asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                                
                            </tr>
                            <tr>
                                <td>Employment Status</td>
                                <td><asp:TextBox ID="txtEmploymentStatus" runat="server"></asp:TextBox></td>
                                <td colspan="2">&nbsp;</td>
                            </tr>
                        </table>
                    <%--</Content>
                </ajaxToolkit:AccordionPane>
                <ajaxToolkit:AccordionPane ID="Personal" runat="server">
                    <Header>--%>
                        <table width="100%">
                            <tr>
                                <td colspan="4" class="alt1" style="text-align:left;">Personal and Academics</td>
                            </tr>
                        </table>
                    <%--</Header>
                    <Content>--%>
                        <table width="100%">
                            <tr>
                                <td>Highest Education</td>
                                <td><asp:TextBox ID="txtHEdu" runat="server"></asp:TextBox></td>
                                <td>Specialization</td>
                                <td><asp:TextBox ID="txtSpec" runat="server"></asp:TextBox></td>
                            </tr>
                            <tr>
                                <td>Blood Group</td>
                                <td><asp:TextBox ID="txtBGroup" runat="server"></asp:TextBox></td>
                                <td>Marital Status</td>
                                <td>
                                    <asp:DropDownList ID="ddlMaritalStatus" runat="server">
                                        <asp:ListItem Text="Single" Value="Single"></asp:ListItem>
                                        <asp:ListItem Text="Married" Value="Married"></asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td>Hobbies</td>
                                <td><asp:TextBox ID="txtHobbies" runat="server"></asp:TextBox></td>
                                <td colspan="2"> &nbsp;</td>
                            </tr>
                        </table>
                    <%--</Content>
                    
                </ajaxToolkit:AccordionPane>
                <ajaxToolkit:AccordionPane ID="Supportive" runat="server">
                    <Header>--%>
                        <table width="100%">
                            <tr>
                                <td colspan="4" class="alt1" style="text-align:left; ">Supportive Information</td>
                            </tr>
                        </table>
                    <%--</Header>
                    <Content>--%>
                        <table width="100%">
                            <tr>
                                <td>Resource Requisition #</td>
                                <td><asp:TextBox ID="txtResReqNo" runat="server"></asp:TextBox></td>
                                <td>Resource Identification Date</td>
                                <td>
                                    <asp:TextBox ID="txtResIdeDt" runat="server"></asp:TextBox>
                                    <asp:ImageButton ID="ImageButton1" ImageUrl="~/App_Themes/Images/Calendar_scheduleHS.png" CausesValidation="false" runat="server" />
                                    <ajaxToolkit:CalendarExtender ID="CalendarExtender3" runat="server" Format="MM/dd/yyyy" TargetControlID="txtResIdeDt" PopupButtonID="ImageButton1" BehaviorID="CalendarExtender3" CssClass="cal_Theme1" Enabled="True">
                                    </ajaxToolkit:CalendarExtender>
                                </td>
                            </tr>
                            <tr>
                                <td>Date of Resource Joining</td>
                                <td>
                                    <asp:TextBox ID="txtResJoinDt" runat="server"></asp:TextBox>
                                    <asp:ImageButton ID="ImageButton3" CausesValidation="false" ImageUrl="~/App_Themes/Images/Calendar_scheduleHS.png" runat="server"/> &nbsp &nbsp
                                    <ajaxToolkit:CalendarExtender ID="CalendarExtender4" runat="server" Format="MM/dd/yyyy" TargetControlID="txtResJoinDt" PopupButtonID="ImageButton3" BehaviorID="CalendarExtender4" CssClass="cal_Theme1" Enabled="True">
                                    </ajaxToolkit:CalendarExtender>
                                </td>
                                <td>Source of Recruitment</td>
                                <td><asp:TextBox ID="txtSrcOfRec" runat="server"></asp:TextBox></td>
                            </tr>
                            <tr>
                                <td>Reffered By</td>
                                <td><asp:TextBox ID="txtRefBy" runat="server"></asp:TextBox></td>
                                <td>Special Achievement</td>
                                <td><asp:TextBox ID="txtSpeAch" runat="server"></asp:TextBox></td>
                            </tr>
                        </table>
                    <%--</Content>
                </ajaxToolkit:AccordionPane>
            </Panes>
        </ajaxToolkit:Accordion>--%> 
        
        <%--<asp:Panel ID="Panel1" runat="server" HorizontalAlign="Left">
            <asp:RequiredFieldValidator  Display="None" ID="RequiredFieldValidator1" runat="server" ErrorMessage="Field Start Salary is required " ControlToValidate="txtSSalary" ></asp:RequiredFieldValidator> <br />
            <asp:RequiredFieldValidator  Display="None" ID="RequiredFieldValidator2" runat="server" ErrorMessage="Field Current Salary is required " ControlToValidate="txtCSalary" ></asp:RequiredFieldValidator> <br />
            <asp:RequiredFieldValidator  Display="None" ID="RequiredFieldValidator3" runat="server" ErrorMessage="Field PF Number is required " ControlToValidate="txtPF" ></asp:RequiredFieldValidator> <br />
            <asp:RequiredFieldValidator  Display="None" ID="RequiredFieldValidator4" runat="server" ErrorMessage="Field PAN Number is required " ControlToValidate="txtPAN" ></asp:RequiredFieldValidator> <br />
            <asp:RequiredFieldValidator  Display="None" ID="RequiredFieldValidator5" runat="server" ErrorMessage="Field Bank Account Number is required " ControlToValidate="txtBankAccNo" ></asp:RequiredFieldValidator><br />
            <asp:RequiredFieldValidator  Display="None" ID="RequiredFieldValidator7" runat="server" ErrorMessage="Field Bank Name is required " ControlToValidate="txtBankName" ></asp:RequiredFieldValidator><br />
            <asp:RequiredFieldValidator  Display="None" ID="RequiredFieldValidator6" runat="server" ErrorMessage="Field Branch IFSC Code is required " ControlToValidate="txtIFSC" ></asp:RequiredFieldValidator><br />
         </asp:Panel>--%>
        
        <br />
        <asp:HiddenField ID="hdnUID" runat="server" />
        <div style="text-align:left">
            <asp:Label ID="lblMsg" runat="server" Text="" ForeColor="red" Font-Bold="true"></asp:Label>
        </div>
        <center>
            <asp:Button ID="btnUpdate" runat="server" Text="Update" CssClass="button" OnClientClick="javascript:return Chk();" />
        </center>    
        </div>
        </div>  
        <div>
            <asp:GridView ID="GrdViewData" runat="server" AutoGenerateColumns="false">
                <Columns>
                    <asp:BoundField HeaderText="Emp. Name" DataField="EName" />
                    <asp:BoundField HeaderText="User Name" DataField="UserName" />
                    <asp:BoundField HeaderText="Department" DataField="Department" />
                    <asp:BoundField HeaderText="Confirm Date" DataField="ConfirmDt" />
                    <asp:BoundField HeaderText="Confirm Status" DataField="ConfirmStatus" />
                    <asp:BoundField HeaderText="Start Salary" DataField="StartSalary" />
                    <asp:BoundField HeaderText="Increments Enjoyed" DataField="IncrementsEnjoyed" />
                    <asp:BoundField HeaderText="Increments Desc" DataField="IncrementsDesc" />
                    <asp:BoundField HeaderText="Current Salary" DataField="CurrentSalary" />
                    <asp:BoundField HeaderText="Insurance Status" DataField="InsuranceStatus" />
                    <asp:BoundField HeaderText="Insurance Code" DataField="InsuranceCode" />
                    <asp:BoundField HeaderText="Family Insurance Details" DataField="FamilyInsuranceDetails" />
                    <asp:BoundField HeaderText="PFNumber" DataField="PFNumber" />
                    <asp:BoundField HeaderText="PANNumber" DataField="PANNumber" />
                    <asp:BoundField HeaderText="Passport Number" DataField="PassportNumber" />
                    <asp:BoundField HeaderText="BankAcc Number" DataField="BankAccNumber" />
                    <asp:BoundField HeaderText="Bank Name" DataField="BankName" />
                    <asp:BoundField HeaderText="Branch IFSCCode" DataField="BranchIFSCCode" />
                    <asp:BoundField HeaderText="Induction Status" DataField="InductionStatus" />
                    <asp:BoundField HeaderText="Induction By" DataField="InductionBy" />
                    <asp:BoundField HeaderText="Induction On" DataField="InductionOn" />
                    <asp:BoundField HeaderText="Document Verify Status" DataField="DocVerifyStatus" />
                    <asp:BoundField HeaderText="Document Verify By" DataField="DocVerifyBy" />
                    <asp:BoundField HeaderText="Access Card" DataField="AccessCard" />
                    <asp:BoundField HeaderText="Emp. I Card" DataField="EmpCard" />
                    <asp:BoundField HeaderText="Employment Status" DataField="EmploymentStatus" />
                    <asp:BoundField HeaderText="Highest Education" DataField="HighestEdu" />
                    <asp:BoundField HeaderText="Specialization" DataField="Specialization" />
                    <asp:BoundField HeaderText="Blood Group" DataField="BloodGroup" />
                    <asp:BoundField HeaderText="Marital Status" DataField="MaritalStatus" />
                    <asp:BoundField HeaderText="Hobbies" DataField="Hobbies" />
                    <asp:BoundField HeaderText="Resource Requisition Number" DataField="ResReqNumber" />
                    <asp:BoundField HeaderText="Resource Identification Date" DataField="ResIdentificationDt" />
                    <asp:BoundField HeaderText="Date of Resource Joining" DataField="ResJoiningDate" />
                    <asp:BoundField HeaderText="Source of Recruitment" DataField="RecruitmentSrc" />
                    <asp:BoundField HeaderText="Reffered By" DataField="RefferedBy" />
                    <asp:BoundField HeaderText="Special Achievement" DataField="AchiInEdictate" />
                </Columns>
            </asp:GridView>            
        </div>  
    </div>
    <asp:ValidationSummary ID="ValidationSummary1" runat="server" HeaderText="Following error occurs:" ShowMessageBox="true" DisplayMode="BulletList" ShowSummary="False" /> </form>
</body>
</html>
