<%@ Page Language="VB" AutoEventWireup="false" CodeFile="Copy of HBA.aspx.vb" Inherits="Transcend_HBA" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="Ajax" %>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>HBA Send Data</title>
    <link href= "../App_Themes/Css/Main.css" type="text/css" rel="stylesheet" />
    <link href="../App_Themes/Css/Common.css" type="text/css" rel="stylesheet"  />
    <link href= "../App_Themes/Css/Styles.css" type="text/css" rel="stylesheet" />
    <link href= "../App_Themes/Css/Calendar.css" type="text/css" rel="stylesheet" />

    <script type="text/javascript" language="javascript">
        function onlyNumbers(evt)
        {
	        var e = event || evt; // for trans-browser compatibility
	        var charCode = e.which || e.keyCode;
	        if ([e.keyCode||e.which]==46) //this is to allow .
                return true;
	        if (charCode > 31 && (charCode < 48 || charCode > 57))
        		return false;
	        return true;
        }
        function Chk()
        {
            if (document.getElementById('txtStartDate').value=='')
            {
                alert('Please enter start date')
                return false;
            }
            if (document.getElementById('txtEndDate').value=='')
            {
                alert('Please enter end date')
                return false;
            }
            return true;
        }
        
        function chk1()
        {
            var flag=false;
            if (document.getElementById('txtJobNo').value=='')
            {
                alert('Please enter JobNo')
                return false;
            }
            if (document.getElementById('ddlStatus').value!='')
                flag=true;
            if (flag!=true)
            {
                if (document.getElementById('ddlQStatus').value=='')
                {
                    alert('Please select MT/QA Status');
                    return false;
                }
            }
            else
            {
                if (document.getElementById('ddlQStatus').value!='')
                {
                    alert('Please select only MT status or QA status not both');
                    return false;
                }                                        
            }
            if (document.getElementById('ddlAccounts').value=='')
            {
                alert('Please select account')
                return false;    
            }
            if (document.getElementById('ddlVRS').value=='')
            {
                alert('Please select VRS for e-Scription')
                return false;
            }
            if (document.getElementById('txtLines').value=='')
            {
                alert('Please enter lines')
                return false;
            }
            return true;
        }
        
        function Change()
        {

            var v;
            v=document.getElementById('ddlPlat').value;

            if(v=='e-Scription')
            {
                if (document.getElementById("eScr").style.visibility == 'hidden')
                    document.getElementById("eScr").style.visibility = 'visible';
            }
            else
            {
                document.getElementById("eScr").style.visibility = 'hidden';
            }
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        <div id="body">
    <div id="cap"></div>
    <div id="main">
    <h1>eScription</h1>
    <div>
    <div>
        <asp:Panel ID="Panel2" runat="server" width="100%">
           <table width="100%">
             <tr>
                <td colspan="2" style="text-align: center" class="HeaderDiv">
                    Send Data
                    <asp:ImageButton ID="Image1" runat="server" ImageUrl="~/images/expand_blue.jpg" AlternateText="(Show Details...)" CausesValidation="false"/>
                </td>
            </tr>
          </table> 
        </asp:Panel>
        <asp:Panel ID="Panel1" runat="server" width="100%" BackColor="whitesmoke">
            <table class="common" width="100%" border="0">
                <tr>
                    <td align="right">
                        Job#
                    </td>
                    <td align="left">
                        <asp:TextBox ID="txtJobNo" runat="server" onkeypress="return onlyNumbers();"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" Display="None" ErrorMessage="<b>Required Field Missing</b><br />Job# is required." Font-Italic="true" SetFocusOnError="true" ControlToValidate="txtJobNo"></asp:RequiredFieldValidator>&nbsp
                        <ajax:ValidatorCalloutExtender runat="Server" ID="ValidatorCalloutExtender1" TargetControlID="RequiredFieldValidator1" HighlightCssclass="validatorCalloutHighlight" />
                    </td>
                    <td align="right">
                        MT Status                    
                    </td>
                    <td align="left">
                        <asp:DropDownList ID="ddlStatus" runat="server" >
                            <asp:ListItem Text="Please Select" Value=""></asp:ListItem>
                            <asp:ListItem Text="Blank Busting" Value="Blank Busting"></asp:ListItem>                    
                            <asp:ListItem Text="Direct" Value="Direct"></asp:ListItem>                    
                            <asp:ListItem Text="Indirect" Value="Indirect"></asp:ListItem>
                            <asp:ListItem Text="Training Review" Value="Training Review"></asp:ListItem>                                                
                        </asp:DropDownList>
                    </td>
                    <td align="right">
                        QA Status
                    </td>
                    <td align="left">
                        <asp:DropDownList ID="ddlQStatus" runat="server">
                            <asp:ListItem Text="Please Select" Value=""></asp:ListItem>
                            <asp:ListItem Text="QA" Value="QA"></asp:ListItem>
                            <asp:ListItem Text="QAB" Value="QAB"></asp:ListItem>
                            <asp:ListItem Text="QATR" Value="QATR"></asp:ListItem>
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr id="eScr">
                    <td align="right">
                        Account Name                    
                    </td>
                    <td align="left">
                        <asp:DropDownList ID="ddlAccounts" runat="server">
                        </asp:DropDownList>
                    </td>
                    <td align="right">
                        VRS
                    </td>
                    <td align="left">
                        <asp:DropDownList ID="ddlVRS" runat="server">
                            <asp:ListItem Text="Please Select" Value=""></asp:ListItem>
                            <asp:ListItem Text="Yes" Value="1"></asp:ListItem>                    
                            <asp:ListItem Text="No" Value="0"></asp:ListItem> 
                        </asp:DropDownList>
                    </td>
                    <td align="right">
                        Lines
                    </td>
                    <td align="left">
                        <asp:TextBox ID="txtLines" runat="server" onkeypress="return onlyNumbers();"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td align="center" colspan="6" style="text-align:center">
                        <asp:Button ID="btnSubmit" runat="server" Text="Submit" cssClass="button" OnClientClick="javascript:return chk1();" />                                    
                    </td>                    
                </tr>
            </table>
        </asp:Panel> 
        
        <ajaxToolkit:CollapsiblePanelExtender ID="cpeDemo" runat="Server"
        TargetControlID="Panel1"
        ExpandControlID="Panel2"
        CollapseControlID="Panel2" 
        Collapsed="False"
        TextLabelID="Label1"
        ImageControlID="Image1"    
        ExpandedText="(Hide Details...)"
        CollapsedText="(Show Details...)"
        ExpandedImage="~/images/collapse_blue.jpg"
        CollapsedImage="~/images/expand_blue.jpg"
        SuppressPostBack="true"
        /> 
        <table width="100%">
            <tr>
                <td align="left">
                    <asp:Label ID="lblMsg" runat="server" Text="" Font-Names="Trebuchet MS" Font-Size="12px" ForeColor="Red" Font-Italic="true" Font-Bold="true"></asp:Label>
                </td>
            </tr>
        </table>
        <table width="100%">
             <tr>
                <td style="text-align: center" class="HeaderDiv">
                    Search Data
                </td>
            </tr>
            <tr>
                <td align="center" style="text-align:center">
                    <table width="40%" style="text-align:center " border="0">
                        <tr>
                            <td style="width:25%" align="center" class="alt1">
                                Start Date
                            </td>
                            <td style="width:25%" align="center" class="alt1" >
                                End Date
                            </td>                            
                            <td style="width:10%" align="center" class="alt1">
                                &nbsp;
                            </td>                            
                        </tr>
                        <tr>
                            <td>
                                <asp:TextBox ID="txtStartDate" runat="server" TabIndex="6" Width="78px" CssClass="common" ></asp:TextBox><asp:ImageButton ID="ImgBntsDate" runat="server" CausesValidation="False" ImageUrl="~/images/Calendar_scheduleHS.png" />
                            </td>
                            <td>
                                <asp:TextBox ID="txtEndDate" runat="server" TabIndex="7" Width="78px" CssClass="common" ></asp:TextBox><asp:ImageButton ID="ImgBnteDate" runat="server" CausesValidation="False" ImageUrl="~/images/Calendar_scheduleHS.png" />
                            </td>
                            <td>
                                <asp:Button ID="btnSearch" runat="server" Text="Search" cssClass="button" CausesValidation="false" OnClientClick="javascript:return Chk();" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td align="left">
                    <asp:LinkButton ID="LnlExport" runat="server" CssClass="common" CausesValidation="false" >Export List</asp:LinkButton>                    
                </td>
            </tr>
            <tr>
                <td align="center">
                    <asp:GridView ID="GrdViewData" runat="server" AllowPaging="true" AllowSorting="true" AutoGenerateColumns="false" PageSize="10" Width="80%">
                    <Columns>
                        <asp:TemplateField HeaderText="Account Name" SortExpression="AccName" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left" HeaderStyle-CssClass="alt1">
                            <ItemTemplate>
                                <%#Eval("AccName")%>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Job#" SortExpression="JobNo" HeaderStyle-HorizontalAlign="Center" HeaderStyle-CssClass="alt1">
                            <ItemTemplate>
                                <%#Eval("JobNo")%>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Lines" SortExpression="Lines" HeaderStyle-HorizontalAlign="Center" HeaderStyle-CssClass="alt1">
                            <ItemTemplate>
                                <asp:Label ID="lblLines" runat="server" Text='<%#SetLines(Eval("Lines").ToString, Eval("PR_Lines").ToString, Eval("QAStatus").ToString)%>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="MTStatus" SortExpression="Status" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left" HeaderStyle-CssClass="alt1">
                            <ItemTemplate>
                                <%#Eval("Status")%>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="QAStatus" SortExpression="QAStatus" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left" HeaderStyle-CssClass="alt1">
                            <ItemTemplate>
                                <%#Eval("QAStatus")%>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="VRS" SortExpression="VRS" HeaderStyle-HorizontalAlign="Center" HeaderStyle-CssClass="alt1">
                            <ItemTemplate>
                                <%#SetVRS(Eval("VRS").ToString)%>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Date Sent" SortExpression="UpdatedOn" HeaderStyle-HorizontalAlign="Center" HeaderStyle-CssClass="alt1">
                            <ItemTemplate>
                                <%#Eval("UpdatedOn").ToShortDateString()%>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                    </asp:GridView>                    
                </td>                
            </tr>
            <tr>
                <td align="left">
                    <asp:Label ID="lblLineCount" runat="server" Text="" ForeColor="Red" Font-Italic="true" Font-Bold="true"></asp:Label>
                </td>
            </tr>
          </table>
          
          <ajaxtoolkit:calendarextender id="CalendarExtender1" runat="server" popupbuttonid="ImgBntsDate" CssClass="cal_Theme1"
            targetcontrolid="txtStartDate"></ajaxtoolkit:calendarextender>
          <ajaxtoolkit:calendarextender id="CalendarExtender2" runat="server" popupbuttonid="ImgBnteDate" CssClass="cal_Theme1"
            targetcontrolid="txtEndDate"></ajaxtoolkit:calendarextender>        
        <asp:HiddenField ID="hdnUserID" runat="server" />
        <asp:HiddenField ID="Hsort" runat="server" />
        <asp:HiddenField ID="Horder" runat="server" />
        <asp:datagrid id="dgResultsData" 
				allowpaging="false" allowsorting="false" 
				autogeneratecolumns="false"  GridLines="Both" 
				runat="server" Font-Names="Trebuchet MS" Font-Size="12px">
				<Columns>
				    <asp:BoundColumn HeaderText="Account Name" DataField="AccName"></asp:BoundColumn>
				    <asp:BoundColumn HeaderText="Job#" DataField="JobNo"></asp:BoundColumn>
				    <asp:TemplateColumn HeaderText="Lines" >
				        <ItemTemplate>
                            <asp:Label ID="lblLines" runat="server" Text='<%#SetLines(Container.DataItem("Lines").ToString, Container.DataItem("PR_Lines").ToString, Container.DataItem("QAStatus").ToString)%>'></asp:Label>
                        </ItemTemplate>
				    </asp:TemplateColumn>
				    <asp:BoundColumn HeaderText="MTStatus" DataField="Status"></asp:BoundColumn>
				    <asp:BoundColumn HeaderText="QAStatus" DataField="QAStatus"></asp:BoundColumn>
				    <asp:TemplateColumn HeaderText="VRS">
				        <ItemTemplate>
				            <%#SetVRS(Container.DataItem("VRS").ToString)%>
				        </ItemTemplate>
				    </asp:TemplateColumn>
				    <asp:TemplateColumn HeaderText="Date Sent">
				        <ItemTemplate>
				            <%#Container.DataItem("UpdatedOn").ToShortDateString()%>
				        </ItemTemplate>
				    </asp:TemplateColumn>
				</Columns>
			</asp:datagrid>
    </div>
    </div> 
    </div> 
    </form>
</body>
</html>
