<%@ Page Language="VB" AutoEventWireup="false" CodeFile="VHSLinesData.aspx.vb" Inherits="VHSLinesData" %>
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
            if (document.getElementById('ddlVHS').value=='')
            {
                alert('Please enter MTID')
                return false;
            }
            if (document.getElementById('txtLines').value=='')
            {
                alert('Please enter Lines')
                return false;
            }
            if (document.getElementById('ddlStatus').value=='')
            {
                alert('Please select Status');
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
        <div id="body">
    <div id="cap"></div>
    <div id="main">
    <h1>Chartscript</h1>
    <div>
        <asp:Panel ID="Panel2" runat="server" width="100%">
           <table width="100%"   >
             <tr>
                <td colspan="2" style="text-align: center" class="HeaderDiv">
                    Send Data
                    <asp:ImageButton ID="Image1" runat="server" ImageUrl="~/images/expand_blue.jpg" AlternateText="(Show Details...)" CausesValidation="false"/>
                </td>
            </tr>
          </table> 
        </asp:Panel>
        <asp:Panel ID="Panel1" runat="server" width="100%">
            <table  width="100%">
                <tr>
                    <td align="right">
                        Job#
                    </td>
                    <td align="left">
                        <asp:TextBox ID="txtJobNo" runat="server" CssClass="common" onkeypress="return onlyNumbers();"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" Display="None" ErrorMessage="<b>Required Field Missing</b><br />Job# is required." CssClass="common"  Font-Italic="true" SetFocusOnError="true" ControlToValidate="txtJobNo"></asp:RequiredFieldValidator>&nbsp
                        <ajax:ValidatorCalloutExtender runat="Server" ID="ValidatorCalloutExtender1" TargetControlID="RequiredFieldValidator1" HighlightCssclass="validatorCalloutHighlight" />
                    </td>
                    <td align="right">
                        MTID
                    </td>
                    <td align="left">
                        <asp:DropDownList ID="ddlVHS" runat="server" CssClass="common" >
                        </asp:DropDownList>
                    </td>
                    <td align="right">
                        Lines
                    </td>
                    <td align="left">
                        <asp:TextBox ID="txtLines" runat="server" onkeypress="return onlyNumbers();"></asp:TextBox>
                    </td>
                    <td align="right">
                        Status                    
                    </td>
                    <td align="left">
                        <asp:DropDownList ID="ddlStatus" runat="server" CssClass="common"  >
                            <asp:ListItem Text="Please Select" Value=""></asp:ListItem>
                            <asp:ListItem Text="Blank Busting" Value="Blank Busting"></asp:ListItem>                    
                            <asp:ListItem Text="QA" Value="QA"></asp:ListItem>                    
                            <asp:ListItem Text="Training Review" Value="Training Review"></asp:ListItem>
                            <asp:ListItem Text="Transcription" Value="Transcription"></asp:ListItem>
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td align="center" colspan="8">
                        <center>
                            <asp:Button ID="btnSubmit" runat="server" Text="Submit" cssClass="button" OnClientClick="javascript:return chk1();" />                                    
                        </center>
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
                    <asp:Label ID="lblMsg" runat="server" Text="" CssClass="common" ForeColor="Red" Font-Italic="true" Font-Bold="true"></asp:Label>
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
                <td align="center">
                    <center>
                    <table width="40%">
                        <tr>
                            <td style="width:25%" align="center" class="alt1">
                                Start Date
                            </td>
                            <td style="width:25%" align="center" class="alt1">
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
                    </center>
                </td>
            </tr>
            <tr>
                <td align="left">
                    <asp:LinkButton ID="LnlExport" runat="server" CssClass="common" CausesValidation="false" >Export List</asp:LinkButton>                    
                </td>
            </tr>
            <tr>
                <td align="center">
                    <asp:GridView ID="GrdViewData" runat="server" AllowPaging="true" AllowSorting="true" CssClass="common" AutoGenerateColumns="false" PageSize="10" Width="80%">
                    <Columns>
                        <asp:TemplateField HeaderText="Job#" SortExpression="JobNo" HeaderStyle-HorizontalAlign="Center" HeaderStyle-CssClass="alt1">
                            <ItemTemplate>
                                <%#Eval("JobNo")%>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="MTID" SortExpression="MTID" HeaderStyle-HorizontalAlign="Center" HeaderStyle-CssClass="alt1">
                            <ItemTemplate>
                                <%#Eval("MTID")%>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Lines" SortExpression="Lines" HeaderStyle-HorizontalAlign="Center" HeaderStyle-CssClass="alt1">
                            <ItemTemplate>
                                <asp:Label ID="lblLines" runat="server" Text='<%#Eval("Lines").ToString%>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Status" SortExpression="Status" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left" HeaderStyle-CssClass="alt1">
                            <ItemTemplate>
                                <%#Eval("Status")%>
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
                    <asp:Label ID="lblLineCount" runat="server" Text="" CssClass="common" ForeColor="Red" Font-Italic="true" Font-Bold="true"></asp:Label>
                </td>
            </tr>
          </table>
          
          <ajaxtoolkit:calendarextender id="CalendarExtender1" runat="server" popupbuttonid="ImgBntsDate" CssClass="cal_Theme1"
            targetcontrolid="txtStartDate"></ajaxtoolkit:calendarextender>
          <ajaxtoolkit:calendarextender id="CalendarExtender2" runat="server" popupbuttonid="ImgBnteDate"  CssClass="cal_Theme1"
            targetcontrolid="txtEndDate"></ajaxtoolkit:calendarextender>        
        <asp:HiddenField ID="hdnUserID" runat="server" />
        <asp:HiddenField ID="Hsort" runat="server" />
        <asp:HiddenField ID="Horder" runat="server" />
        <asp:datagrid id="dgResultsData" 
				allowpaging="false" allowsorting="false" 
				autogeneratecolumns="false"  GridLines="Both" 
				runat="server" Font-Names="Trebuchet MS" Font-Size="12px">
				<Columns>
				    <asp:BoundColumn HeaderText="Job#" DataField="JobNo"></asp:BoundColumn>
				    <asp:BoundColumn HeaderText="MTID" DataField="MTID"></asp:BoundColumn>
				    <asp:TemplateColumn HeaderText="Lines" >
				        <ItemTemplate>
                            <asp:Label ID="lblLines" runat="server" Text='<%# Container.DataItem("Lines").ToString%>'></asp:Label>
                        </ItemTemplate>
				    </asp:TemplateColumn>
				    <asp:BoundColumn HeaderText="Status" DataField="Status"></asp:BoundColumn>
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
