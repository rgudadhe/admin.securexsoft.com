    <%@ Page Language="VB" AutoEventWireup="false" CodeFile="FaxPlusResult.aspx.vb" Inherits="FaxPlus_FaxPlusResult" %>
<%@ Register TagPrefix="DBWC" Namespace="DBauer.Web.UI.WebControls" Assembly="DBauer.Web.UI.WebControls.HierarGrid" %>
<%@ Register
    Assembly="AjaxControlToolkit"
    Namespace="AjaxControlToolkit"
    TagPrefix="ajaxToolkit" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 1.0 Transitional/EN">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Faxplus Job Status</title>
    <link href= "../App_Themes/Css/styles.css" type="text/css" rel="stylesheet" />
    <link href= "../App_Themes/Css/Common.css" type="text/css" rel="stylesheet" />
    <link href="../App_Themes/Css/DataTable.css" rel="stylesheet" type="text/css" />
    <link href="../App_Themes/Css/TableSorter.css" rel="stylesheet" type="text/css" />
    <script src="../App_Themes/JS/jquery-1.4.2.min.js" type="text/javascript"></script>  
    <script src="../App_Themes/JS/jquery.dataTables.min.js" type="text/javascript"></script>  

</head>
<script type="text/javascript" language="javascript">
    function openPopup(str,str1)
    {
        window.open('JobHistory.aspx?TransID='+str+'&RPID='+str1,'', 'width=550,height=240,status=0,scrollbars=1')
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
		                            { "asSorting": [ "desc", "asc" ] },
		                            { "asSorting": [ "desc", "asc" ] },
		                            { "asSorting": [ "desc", "asc" ] },
		                            { "asSorting": [ "desc", "asc" ] }
	                              ] 
				} );
			} );
</script>
<body>
    <form id="form1" runat="server">
        <asp:Panel ID="Panel1" runat="server" HorizontalAlign="Left">
           
        <table>
            <tr>
                <td>
                    <asp:LinkButton ID="LnlExport" Visible="True" runat="server">Export Result</asp:LinkButton>                     
                </td>
            </tr>
        </table>
                                
                               
                                <asp:GridView ID="dlist" runat="server" AutoGenerateColumns="false" OnRowDeleting="dlist_RowDeleting" >
                                    <Columns>
                                        <asp:TemplateField HeaderStyle-CssClass="header">
                                            <ItemTemplate>
                                                <%--<asp:LinkButton ID="lnkHistory" CommandName="History" CommandArgument='<%#Container.DataItem("TranscriptionID").ToString() & "|" & Container.DataItem("RPID").ToString()%>' runat="server">History</asp:LinkButton>--%>
                                                <asp:ImageButton ID="btnHistory" runat="server" ImageUrl="~/App_Themes/Images/his.png" ToolTip="Job history"   />                                                
                                                <asp:HiddenField ID="hdnTransID" Value='<%#Container.DataItem("TranscriptionID")%>' runat="server" />
                                                <asp:HiddenField ID="hdnRPID" Value='<%#Container.DataItem("RPID")%>' runat="server" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField headertext="Status" HeaderStyle-CssClass="header">
                                            <ItemTemplate>
                                                <asp:Label id="lblStatus" runat="server" Text='<%#IIF(Container.DataItem("Status")=0,"Pending Fax","Finished")%>'></asp:Label>    
                                            </ItemTemplate>    
                                        </asp:TemplateField>
                                        <asp:TemplateField headertext="Mode" HeaderStyle-CssClass="Header">
                                            <ItemTemplate>
                                                <asp:Label id="lblMode" runat="server" Text='<%#IIF(Container.DataItem("FaxPlusMode")=0,"Finished Reports","Approved Reports")%>'></asp:Label>    
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField headertext="Status Info" HeaderStyle-CssClass="Header">
                                            <ItemTemplate>
                                                <asp:Label id="lblStatusInfo" runat="server" Text='<%#IIF(Container.DataItem("Status")=0,IIF(Container.DataItem("FaxPlusMode")=True,IIF(Container.DataItem("CStatus")=222,"-","Pending Approval"),"-"),"-")%>'></asp:Label>    
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField headertext="Date Available" HeaderStyle-CssClass="Header">
                                            <ItemTemplate>
                                                <%#Eval("DateAvailable")%>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField headertext="Date Finished" HeaderStyle-CssClass="Header">
                                            <ItemTemplate>
                                                <%#Eval("DateFinished")%>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField headertext="Job Number" HeaderStyle-CssClass="Header">
                                            <ItemTemplate>
                                                <%#Eval("JobNumber")%>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField headertext="Client Job#" HeaderStyle-CssClass="Header">
                                            <ItemTemplate>
                                                <%#Eval("CustJobID")%>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField headertext="Ref. Name" HeaderStyle-CssClass="Header">
                                            <ItemTemplate>
                                                <%#Eval("RefName")%>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField headertext="Dictator Name" HeaderStyle-CssClass="Header">
                                            <ItemTemplate>
                                                <%#Eval("Physician")%>
                                            </ItemTemplate>
                                        </asp:TemplateField>                                                                                
                                        <asp:TemplateField headertext="Account Name" HeaderStyle-CssClass="Header">
                                            <ItemTemplate>
                                                <%#Eval("AccountName")%>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField headertext="Patient Name" HeaderStyle-CssClass="Header">
                                            <ItemTemplate>
                                                <%#Eval("PatientName")%>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField headertext="DtOfServ" HeaderStyle-CssClass="Header">
                                            <ItemTemplate>
                                                <%#Eval("DtOfServ")%>
                                            </ItemTemplate>
                                        </asp:TemplateField> 
                                        <asp:TemplateField headertext="PDOB" HeaderStyle-CssClass="Header">
                                            <ItemTemplate>
                                                <%#Eval("PDOB")%>
                                            </ItemTemplate>
                                        </asp:TemplateField> 
                                        <asp:TemplateField headertext="LocName" HeaderStyle-CssClass="Header">
                                            <ItemTemplate>
                                                <%#Eval("LocName")%>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField headertext="Delete" HeaderStyle-CssClass="Header">
                                            <itemtemplate>
                                                <asp:LinkButton ID="LnkDelete" runat="server" CausesValidation="False" CommandName="Delete"  CommandArgument='<%#Container.DataItem("TranscriptionID")%>'  OnClientClick='return confirm("Are you sure you want to delete this entry?");' Text="Delete" />
                                            </itemtemplate>
                                        </asp:TemplateField>                                           
                                    </Columns>
                                </asp:GridView>
        
  
        </asp:Panel>
    
    </form>
</body>
</html>
