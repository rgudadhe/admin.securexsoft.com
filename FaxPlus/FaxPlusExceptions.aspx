<%@ Page Language="VB" AutoEventWireup="false" CodeFile="FaxPlusExceptions.aspx.vb" Inherits="FaxPlus_FaxPlusExceptions" %>
<%@ Register TagPrefix="DBWC" Namespace="DBauer.Web.UI.WebControls" Assembly="DBauer.Web.UI.WebControls.HierarGrid" %>
<%@ Register
    Assembly="AjaxControlToolkit"
    Namespace="AjaxControlToolkit"
    TagPrefix="ajaxToolkit" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 1.0 Transitional/EN">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Exception Report</title>
    <link href= "../App_Themes/Css/styles.css" type="text/css" rel="stylesheet" />
    <link href= "../App_Themes/Css/Common.css" type="text/css" rel="stylesheet" />
    <link href="../App_Themes/Css/DataTable.css" rel="stylesheet" type="text/css" />
    <link href="../App_Themes/Css/TableSorter.css" rel="stylesheet" type="text/css" />
    <link href="../App_Themes/Css/Calendar.css" rel="stylesheet" type="text/css" />
    <script src="../App_Themes/JS/jquery-1.4.2.min.js" type="text/javascript"></script>  
    <script src="../App_Themes/JS/jquery.dataTables.min.js" type="text/javascript"></script>  
    
    <script type="text/javascript" language="javascript">
    function openPopup(str,str1)
    {
        window.open('ExceptionJobHistory.aspx?TransID='+str+'&RPID='+str1,'', 'width=640,height=330,status=0,scrollbars=1')
        return false;
    }
</script>

<script type="text/javascript" language="javascript">
    $(document).ready(function() {
				$('#dlist').dataTable( {
					//"sPaginationType": "full_numbers"
                    "aoColumns": [
                            		{ "asSorting": [ "desc", "asc" ] },
		                            { "asSorting": [ "desc", "asc" ] },
		                            { "asSorting": [ "desc", "asc" ] },
		                            { "asSorting": [ "desc", "asc" ] },
		                            { "asSorting": [ "desc", "asc" ] },
		                            { "asSorting": [ "desc", "asc" ] },
		                            { "asSorting": [ "desc", "asc" ] },
                            		{ "asSorting": [ "desc", "asc" ] },
		                            { "asSorting": [ "desc", "asc" ] },
		                            { "asSorting": [ "desc", "asc" ] },
		                            { "asSorting": [ "desc", "asc" ] },
		                            { "asSorting": [ "desc", "asc" ] },
		                            { "asSorting": [ "desc", "asc" ] }
	                              ] 
				} );
			} );
</script>
    <script type="text/javascript" language="javascript">
        function Confirmation()
        {
            if (confirm('Are you sure you want to delete this record?'))
                return true;
            else
                return false;
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <ajaxToolkit:ToolkitScriptManager ID="ScriptManager1" EnablePartialRendering="true" EnableScriptGlobalization="true" EnableScriptLocalization="true" runat="Server" />
    <div style="text-align:left">
                <table style="text-align:left">
                    <tr>
                        <td class="alt1">
                            Exception
                        </td>
                        <td class="alt1">
                            Tracking Job#
                        </td>
                        <td class="alt1">
                            Cutomer Job#
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:DropDownList ID="DDLEx" runat="server" Width="264px" TabIndex="1" Font-Names="Trebuchet MS" Font-Size="12px" >
                                <asp:ListItem Selected="True" Value="All">All</asp:ListItem>
                                <asp:ListItem Value="Physician not listed in database">Physician not listed in database</asp:ListItem>
                                <asp:ListItem Value="Physician name different from database">Physician name different from database</asp:ListItem>
                                <asp:ListItem Value="Credentials unavailable/mismatch">Credentials unavailable/mismatch</asp:ListItem>
                                <asp:ListItem Value="Address not available">Address not available</asp:ListItem>
                                <asp:ListItem Value="Address does not match with database"></asp:ListItem>
                                <asp:ListItem Value="Fax number does not match"></asp:ListItem>
                                <asp:ListItem Value="Fax number not available"></asp:ListItem>
                            </asp:DropDownList>
                        </td>
                        <td>
                            <asp:TextBox ID="Track" runat="server" TabIndex="1" Width="130px" Font-Names="Trebuchet MS" Font-Size="12px"></asp:TextBox>
                        </td>
                        <td>
                            <asp:TextBox ID="Cust" runat="server" TabIndex="2" Width="130px" Font-Names="Trebuchet MS" Font-Size="12px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="alt">
                            Account Name
                        </td>
                        <td class="alt">
                            Start Date
                        </td>
                        <td class="alt">
                            End Date
                        </td>
                    </tr>
                    <tr>
                        <td style="height: 2px;width:264px">
                            <asp:TextBox ID="AName" runat="server" TabIndex="9" Width="264px" Font-Names="Trebuchet MS" Font-Size="12px"></asp:TextBox>
                        </td>
                        <td style="height: 2px">
                            <asp:TextBox ID="sDate" runat="server" TabIndex="6" Width="120px" Font-Names="Trebuchet MS" Font-Size="12px"></asp:TextBox>
                            <asp:ImageButton ID="ImgBntsDate" runat="server" CausesValidation="False" ImageUrl="~/images/Calendar_scheduleHS.png" />
                        </td>
                        <td style="height: 2px">
                            <asp:TextBox ID="eDate" runat="server" TabIndex="7" Width="120px" Font-Names="Trebuchet MS" Font-Size="12px"></asp:TextBox>
                            <asp:ImageButton ID="ImgBnteDate" runat="server" CausesValidation="False" ImageUrl="~/images/Calendar_scheduleHS.png" />
                        </td>
                    </tr>
                </table> 
<ajaxtoolkit:calendarextender id="CalendarExtender1" runat="server" popupbuttonid="ImgBntsDate"
                        targetcontrolid="sDate" CssClass="cal_Theme1"></ajaxtoolkit:calendarextender>
                    <ajaxtoolkit:calendarextender id="CalendarExtender2" runat="server" popupbuttonid="ImgBnteDate"
                        targetcontrolid="eDate" CssClass="cal_Theme1"></ajaxtoolkit:calendarextender>                               
                  <asp:Button ID="btnGO" runat="server" class="button" Text="Search" />
    </div>  
        <%--<asp:UpdatePanel ID="update" runat="server">
            <ContentTemplate>   --%>                        
        <table>
        <tr>
            <td style="border:0">
                <asp:LinkButton ID="LnlExport"  runat="server" style="font-family:Trebuchet MS; font-size:small;" >Export Result</asp:LinkButton>                     
            </td>
        </tr>
        <tr>
            <td style="border:0">           
                <asp:GridView ID="dlist" runat="server" AutoGenerateColumns="false" OnRowDeleting="dlist_RowDeleting">
                    <Columns>
                        <asp:TemplateField HeaderStyle-CssClass="header">
                            <ItemTemplate>
                                <%--<asp:LinkButton ID="lnkHistory" CommandName="History" CommandArgument='<%#Container.DataItem("TranscriptionID").ToString() & "|" & Container.DataItem("RPID").ToString()%>' runat="server">History</asp:LinkButton>--%>
                                <asp:ImageButton ID="btnHistory" runat="server" ImageUrl="~/App_Themes/Images/his.png" ToolTip="Job history"   />                                                
                                <asp:HiddenField ID="hdnTransID" Value='<%#Container.DataItem("TranscriptionID")%>' runat="server" />
                                <asp:HiddenField ID="hdnRPID" Value='<%#Container.DataItem("RPID")%>' runat="server" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField headertext="Job Number" HeaderStyle-CssClass="Header">
                            <itemtemplate>
        	                    <%#IIf(IsDBNull(Container.DataItem("JobNumber")), String.Empty, Container.DataItem("JobNumber"))%>
        	                </itemtemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Physician Name" HeaderStyle-CssClass="Header">
            	            <itemtemplate>
            	                <%#IIf(IsDBNull(Container.DataItem("RPName")), String.Empty , Container.DataItem("RPName"))%>
            	            </itemtemplate>
            	        </asp:TemplateField>
                        <asp:TemplateField headertext="Comments" HeaderStyle-CssClass="Header">
        	                <itemtemplate>
        	                    <%#IIf(IsDBNull(Container.DataItem("Comments")), String.Empty, Container.DataItem("Comments"))%>
        	                </itemtemplate>
        	            </asp:TemplateField>
        	            <asp:TemplateField headertext="Exception Date" HeaderStyle-CssClass="Header">
            	            <itemtemplate>
            	                <%#IIf(IsDBNull(Container.DataItem("DateAvaialble")), String.Empty, Container.DataItem("DateAvaialble"))%>
            	            </itemtemplate>
            	        </asp:TemplateField>
                        <asp:TemplateField headertext="Raised By" HeaderStyle-CssClass="Header">
            	            <itemtemplate>
            	                <%#IIf(IsDBNull(Container.DataItem("UserName")), String.Empty, Container.DataItem("UserName"))%>
            	            </itemtemplate>
            	        </asp:TemplateField>
                        <asp:TemplateField headertext="AccountName" HeaderStyle-CssClass="Header">
            	            <itemtemplate>
            	                <%#IIf(IsDBNull(Container.DataItem("AccountName")), String.Empty, Container.DataItem("AccountName"))%>
            	            </itemtemplate>
            	        </asp:TemplateField>
                        <asp:TemplateField headertext="Client Job#" HeaderStyle-CssClass="Header">
            	            <itemtemplate>
            	                <%#IIf(IsDBNull(Container.DataItem("CustJobID")), String.Empty, Container.DataItem("CustJobID"))%>
            	            </itemtemplate>
            	        </asp:TemplateField>
            	        
            	        <asp:TemplateField headertext="PatientName" HeaderStyle-CssClass="Header">
            	            <itemtemplate>
            	                <%#IIf(IsDBNull(Container.DataItem("PatientName")), String.Empty, Container.DataItem("PatientName"))%>
            	            </itemtemplate>
            	        </asp:TemplateField>
            	        
            	        <asp:TemplateField headertext="DtOfServ" HeaderStyle-CssClass="Header">
            	            <itemtemplate>
            	                <%#IIf(IsDBNull(Container.DataItem("DtOfServ")), String.Empty, Container.DataItem("DtOfServ"))%>
            	            </itemtemplate>
            	        </asp:TemplateField>
                         
            	        <asp:TemplateField headertext="PDOB" HeaderStyle-CssClass="Header">
            	            <itemtemplate>
            	                <%#IIf(IsDBNull(Container.DataItem("PDOB")), String.Empty, Container.DataItem("PDOB"))%>
            	            </itemtemplate>
            	        </asp:TemplateField>
            	        
            	        <asp:TemplateField headertext="LocName" HeaderStyle-CssClass="Header">
            	            <itemtemplate>
            	                <%#IIf(IsDBNull(Container.DataItem("LocName")), String.Empty, Container.DataItem("LocName"))%>
            	            </itemtemplate>
            	        </asp:TemplateField>  
                        <asp:TemplateField headertext="" HeaderStyle-CssClass="Header">
            	            <itemtemplate>
            	                <asp:LinkButton ID="LnkDelete" CommandName="Delete" CommandArgument='<%#Container.DataItem("TranscriptionID").tostring & "|" & Container.DataItem("RPID").tostring%>' runat="server" OnClientClick="javascript:return Confirmation();" >Delete</asp:LinkButton>
            	            </itemtemplate> 
            	        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </td>
            </tr>			
           </table>   
           <%--</ContentTemplate>
			</asp:UpdatePanel>  --%>          
        <%--<ajaxToolkit:ListSearchExtender ID="ListSearchExtender2" runat="server"
                TargetControlID="DDLEx" PromptCssClass="ListSearchExtenderPrompt">
        </ajaxToolkit:ListSearchExtender>--%>
        <asp:datagrid id="dgResultsData"
				allowpaging="false" allowsorting="false" 
				autogeneratecolumns="true"  GridLines="Both" 
				runat="server" Font-Names="Trebuchet MS" Font-Size="12px">
			</asp:datagrid>
        
    </div>
    </form>
</body>
</html>
