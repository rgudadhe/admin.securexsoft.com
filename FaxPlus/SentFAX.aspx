<%@ Page Language="VB" AutoEventWireup="false" CodeFile="SentFAX.aspx.vb" Inherits="FaxPlus_SentFAX" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Sent FAX</title>
    <link href= "../App_Themes/Css/styles.css" type="text/css" rel="stylesheet" />
    <link href= "../App_Themes/Css/Common.css" type="text/css" rel="stylesheet" />
    <link href="../App_Themes/Css/DataTable.css" rel="stylesheet" type="text/css" />
    <link href="../App_Themes/Css/TableSorter.css" rel="stylesheet" type="text/css" />
    <link href="../App_Themes/Css/Calendar.css" rel="stylesheet" type="text/css" />
    <script src="../App_Themes/JS/jquery-1.4.2.min.js" type="text/javascript"></script>  
    <script src="../App_Themes/JS/jquery.dataTables.min.js" type="text/javascript"></script>  
    <script type="text/javascript" language="javascript">
        $(document).ready(function() {
				$('#GridViewMain').dataTable( {
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
		                            { "asSorting": [ "desc", "asc" ] }
	                              ] ,
                    "aaSorting": [[ 1, "asc" ]]
				} );
			} );
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
    <div id="body">
        <div id="cap"></div>
        <div id="main">
        <h1>Sent Faxes</h1>
            <asp:Table ID="tblMain" runat="server" Width="100%">
                <%--<asp:TableRow CssClass="ReportHeaderDiv"> 
                    <asp:TableCell HorizontalAlign=Center>
                        Sent
                    </asp:TableCell>
                </asp:TableRow>--%>
                
                <asp:TableRow>
                    <asp:TableCell BorderStyle="None" BorderWidth="0">
                        <asp:DropDownList ID="ddlAccounts" runat="server" Font-Names="Trebuchet MS">
                        </asp:DropDownList>
                        
                        Start Date : 
                        <asp:TextBox ID="txtStartDate" runat="server" Width="60"></asp:TextBox>
                        End Date :
                        <asp:TextBox ID="txtEndDate" runat="server" Width="60"></asp:TextBox>
                        &nbsp;&nbsp;&nbsp;
                        <asp:Button ID="btnSubmit" runat="server" Text="Submit" Font-Names="Trebuchet MS" Font-Size="8" CssClass="button" />
                        
                        <ajaxtoolkit:calendarextender id="CalendarExtender1" runat="server" CssClass="cal_Theme1"
                        targetcontrolid="txtStartDate"></ajaxtoolkit:calendarextender>
                    <ajaxtoolkit:calendarextender id="CalendarExtender2" runat="server" CssClass="cal_Theme1"
                        targetcontrolid="txtEndDate"></ajaxtoolkit:calendarextender>                    
                    </asp:TableCell>
                </asp:TableRow>
                <asp:TableRow>
                    <asp:TableCell BorderStyle="None" BorderWidth="0">
                                <asp:LinkButton ID="LnkExport" runat="server"  >Export Result</asp:LinkButton> 
                   </asp:TableCell>
                 </asp:TableRow>
                <asp:TableRow>
                    <asp:TableCell BorderStyle="None" BorderWidth="0">
                        <asp:GridView ID="GridViewMain" runat="server" AutoGenerateColumns="false" Width="100%" >
				            
  
				            <Columns>
					            <asp:TemplateField HeaderText="Job#" HeaderStyle-CssClass="alt1">
                                    <ItemTemplate  >
                                      	<asp:HiddenField ID="JobID" Value=<%# Eval("RecordID") %>  runat="server" />
                                      	<asp:HiddenField ID="hdnType" Value=<%# Eval("Type")%>  runat="server" />
                                      	<asp:Label ID="lblJobNumber" runat="server" Text=<%#Eval("Jobnumber") %>></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="CustJob#" HeaderStyle-CssClass="alt1">
                                    <ItemTemplate>
                                        <asp:Label ID="lblCustJobNumber" runat="server" Text=<%#Eval("CustJobID") %>></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="To" HeaderStyle-HorizontalAlign="Center" SortExpression="PhyName" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="10%" HeaderStyle-CssClass="alt1"> 
                                    <ItemTemplate>
		                                <asp:Label ID="lblPhyfName" runat="server" Text=<%#Eval("PhyName") %>></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Fax" HeaderStyle-HorizontalAlign="Center" SortExpression="FAXNO" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="12%" HeaderStyle-Width="12%" HeaderStyle-CssClass="alt1"> 
                                    <ItemTemplate>
                                     	<label id="lblFaxNo" name="lblFaxNo" runat="server"><%#IIf(Eval("Type").ToString = "Fax", ChkFaxNo1(Eval("FAXNO").ToString), Eval("FAXNO").ToString)%></label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Status" HeaderStyle-HorizontalAlign="Center" SortExpression="Description" ItemStyle-Width="15%" HeaderStyle-Width="10%" HeaderStyle-CssClass="alt1">  
                                   	<ItemTemplate>
                                       	<asp:Label ID="lblStatus" runat="server" Text=<%#IIF(Eval("StatusID")=1,"Sent successful","Sent failed") %>></asp:Label>
                                   	</ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Sender" HeaderStyle-HorizontalAlign="Center" SortExpression="Sender" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="5%" HeaderStyle-CssClass="alt1"> 
                                  	<ItemTemplate>
                                      	<%#Replace(ReplaceSubject(Eval("Sender").ToString()), "vbCrLf", String.Empty)%>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <%--<asp:TemplateField HeaderText="Note" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="25%" HeaderStyle-CssClass="alt1"> 
                                  	<ItemTemplate>
                                  	    <%#SetNote(CStr(Eval("Subject").ToString()), Eval("RecordID").ToString)%>
                                      	
                                    </ItemTemplate>
                                </asp:TemplateField>  --%>                                                              
                                <asp:TemplateField HeaderText="Sent date" HeaderStyle-HorizontalAlign="Center" SortExpression="PostedTime" ItemStyle-Width="10%" HeaderStyle-CssClass="alt1"> 
                                   	<ItemTemplate>
                                       	<asp:Label ID="lblPostedTime" runat="server" Text=<%#DataBinder.Eval(Container.DataItem, "PostedTime", "{0:yyyy-MM-dd}") %>></asp:Label>
                                   	</ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Account Name" HeaderStyle-HorizontalAlign="Center" HeaderStyle-CssClass="alt1">
                                	    <ItemTemplate>
                                	        <asp:Label ID="lblAccName" runat="server" Text=<%#Eval("AccountName") %>></asp:Label>
                                	    </ItemTemplate>
                                	</asp:TemplateField>
                                <asp:TemplateField HeaderText="Page_Count" HeaderStyle-HorizontalAlign="Center" SortExpression="DocumentPageCount" ItemStyle-Width="5%" HeaderStyle-CssClass="alt1"> 
                                   	<ItemTemplate>
                                       	<asp:Label ID="lblDocumentPageCount" runat="server" Text='<%#IIf(Eval("DocumentPageCount") = 0, "0", Eval("DocumentPageCount") + 1)%>'></asp:Label>
                                   	</ItemTemplate>
                                </asp:TemplateField>                                                                
                            </Columns>  
                        </asp:GridView>
                    </asp:TableCell>
                </asp:TableRow>
            </asp:Table>
            <asp:Table ID="Table1" BorderWidth="0" BorderStyle="None" runat="server" Width=100%>
                <asp:TableRow>
                    <asp:TableCell VerticalAlign=Bottom HorizontalAlign=Left BorderStyle="None">
                        <asp:Label ID="lblMessage" Font-Names="Trebuchet MS" Font-Size="8pt" ForeColor="Firebrick" runat="server"></asp:Label>
                  </asp:TableCell>
                </asp:TableRow>
            </asp:Table>
        </div> 
        </div> 
    </form>
</body>
</html>
