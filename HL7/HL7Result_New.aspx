<%@ Page Language="VB" AutoEventWireup="false" CodeFile="HL7Result_New.aspx.vb" Inherits="FaxPlus_FaxPlusResult" EnableViewStateMac="false"  %>
<%@ Register TagPrefix="DBWC" Namespace="DBauer.Web.UI.WebControls" Assembly="DBauer.Web.UI.WebControls.HierarGrid" %>
<%@ Register
    Assembly="AjaxControlToolkit"
    Namespace="AjaxControlToolkit"
    TagPrefix="ajaxToolkit" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 1.0 Transitional/EN">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>HL7 Track Reports</title>
    <link href= "../App_Themes/Css/styles.css" type="text/css" rel="stylesheet" />
    <link href= "../App_Themes/Css/Common.css" type="text/css" rel="stylesheet" />
    <link href="../App_Themes/Css/DataTable.css" rel="stylesheet" type="text/css" />
    <link href="../App_Themes/Css/TableSorter.css" rel="stylesheet" type="text/css" />
    <script src="../App_Themes/JS/jquery-1.4.2.min.js" type="text/javascript"></script>  
    <script src="../App_Themes/JS/jquery.dataTables.min.js" type="text/javascript"></script>  
    
    <script type="text/javascript" language="javascript">
        function openPopup(varTransID)
        {
            var displayWindow;
            displayWindow = window.open('', "newWin4", "width=450,height=340, scrollbars=0,menubar=0,toolbar=0,location=0,status=1");
    		document.form1.target = "newWin4";
            document.form1.action="HL7History_New.aspx?TransID=" + varTransID
    		document.form1.submit();
  		    document.form1.target = "_self";
  		    document.form1.action="HL7Result_New.aspx"; 
  		    
            //var WinSettings = "center:yes;resizable:no;status:no;dialogHeight:540px;dialogWidth:850px";
            //var MyArgs = window.showModalDialog("JobHistory.aspx", myObject, WinSettings);
            
            //window.open('JobHistory.aspx?TransID='+str+'&Status='+str1,myObject, 'width=850,height=540,status=0,scrollbars=1')
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
		                            { "asSorting": [ "desc", "asc" ] }
	                              ] 
				} );
			} );
</script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
                                  
        <ajaxToolkit:ToolkitScriptManager ID="ScriptManager1" runat="server">
        </ajaxToolkit:ToolkitScriptManager>
            <table width="100%" style="text-align:left">
                <tr>
                    <td style="border:0">
                        <asp:Panel ID="iMain" runat="server" Width="100%" Visible="false" Wrap="False">
                            <table style="border:0">
                                <tr>
                                    <td style="border:0">
                                        <asp:LinkButton ID="LinkButton1" runat="server"  >Export Result</asp:LinkButton> 
                                    </td>
                                </tr>                                
                                <tr>
                                    <td style="width:100%; border:0">
                                        <asp:GridView ID="dlist" runat="server" AutoGenerateColumns="false" Width="100%">
                                            <Columns>
                                                <asp:TemplateField HeaderStyle-CssClass="header">
                                                    <ItemTemplate>
                                                        <%--<asp:LinkButton ID="lnkHistory" CommandName="History" CommandArgument='<%#Container.DataItem("TranscriptionID").ToString() & "|" & Container.DataItem("RPID").ToString()%>' runat="server">History</asp:LinkButton>--%>
                                                        <asp:ImageButton ID="btnHistory" runat="server" ImageUrl="~/App_Themes/Images/his.png" ToolTip="Job history"   />                                                
                                                        <asp:HiddenField ID="hdnTransID" Value='<%#Container.DataItem("TranscriptionID")%>' runat="server" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderStyle-CssClass="header">
                                                    <itemtemplate>									
                                                        <asp:CheckBox ID="ChkStatus" runat="server"></asp:CheckBox>
                                                        <asp:HiddenField ID="hdnID" Value='<%#Container.DataItem("TranscriptionID")%>' runat="server" />
                                                    </itemtemplate>
                                                    <headerstyle width="1px" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Status" HeaderStyle-CssClass="header">
                                                    <itemtemplate>									
                                                        <asp:Label id="lblStatus" runat="server" Text='<%#IIF(isdbnull(Container.DataItem("StatusDesc")),IIF(Container.DataItem("CStatus")=111,"Pending Signature","Pending HL7"),Container.DataItem("StatusDesc"))%>'></asp:Label>                                            
                                                    </itemtemplate>
                                                    <headerstyle width="20px" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Physician" HeaderStyle-CssClass="header">
                                                    <itemtemplate>									
                                                        <asp:Label id="lblPhysician" runat="server" Text='<%#Container.DataItem("Physician")%>'></asp:Label>                                          
                                                    </itemtemplate>
                                                    <headerstyle width="20px" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Submit Date" HeaderStyle-CssClass="header">
                                                    <ItemTemplate>
                                                        <%#Container.DataItem("SubmitDate")%>                                                        
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Date Finished" HeaderStyle-CssClass="header">
                                                    <ItemTemplate>
                                                        <%#Container.DataItem("DateFinished")%>                                                        
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Job#" HeaderStyle-CssClass="header">
                                                    <ItemTemplate>
                                                        <%#Container.DataItem("JobNumber")%>                                                        
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Client Job#" HeaderStyle-CssClass="header">
                                                    <ItemTemplate>
                                                        <%#Container.DataItem("CustJobID")%>                                                        
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Patient Name" HeaderStyle-CssClass="header">
                                                    <ItemTemplate>
                                                        <%#Container.DataItem("PtName")%>                                                        
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Date Of Service" HeaderStyle-CssClass="header">
                                                    <ItemTemplate>
                                                        <%#Container.DataItem("DOS")%>                                                        
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="MRN" HeaderStyle-CssClass="header">
                                                    <ItemTemplate>
                                                        <%#Container.DataItem("MRN")%>                                                        
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView> 
                                    </td>
                                </tr>
                            </table>
                        </asp:Panel>
                    </td>
                </tr>                 
            </table>    
            
                    <%--<asp:UpdatePanel ID="update" runat="server">
            <ContentTemplate>
                <asp:Panel ID="iMain" runat="server" Height="" Visible="false" Width="100%" Wrap="False">
                    <table width="100%" height="100%">
                        <tr>
                            <td style="height: 197px">  
                            <asp:LinkButton ID="LinkButton1" runat="server" Font-Names="Verdana" Font-Size="8pt">Export Result</asp:LinkButton> 
                            <br />
                           
                                <DBWC:HierarGrid ID="dlist" DataKeyField="TranscriptionID" allowsorting="True" autogeneratecolumns="False" style="Z-INDEX: 101" runat="server" BorderColor="#999999" BorderStyle="None" BorderWidth="0px" BackColor="White" CellPadding="3" TemplateCachingBase="Tablename" gridlines="Vertical" horizontalalign="Left" HeaderStyle-ForeColor="White" AllowPaging="True" Height="100%" Width="100%">
                                    <SelectedItemStyle Font-Bold="True" ForeColor="White" BackColor="#008A8C"></SelectedItemStyle>
				        <AlternatingItemStyle BackColor ="Gainsboro" >		
				        </AlternatingItemStyle>
				        <ItemStyle  BackColor="Beige" Font-Names="Verdana" Font-Size="8pt" Wrap="False" ></ItemStyle>
				        <HeaderStyle CssClass="SMParentSelected" ForeColor="White" ></HeaderStyle>				
                                    <Columns>
                                        <asp:templatecolumn>
                                            <headertemplate>        									
			                     		    </headertemplate>
                                            <itemtemplate>
                                                <asp:HiddenField ID="hdnTransID" Value='<%#Container.DataItem("TranscriptionID")%>' runat="server" />                              
				                            </itemtemplate>
                                            <headerstyle width="5px" />
                                        </asp:templatecolumn>  
                                        <asp:templatecolumn>
                                            <itemtemplate>									
                                                <asp:CheckBox ID="ChkStatus" runat="server"></asp:CheckBox>
                                                <asp:HiddenField ID="hdnID" Value='<%#Container.DataItem("TranscriptionID")%>' runat="server" />
                                            </itemtemplate>
                                            <headerstyle width="1px" />
                                        </asp:templatecolumn>                                                                            
                                        <asp:templatecolumn headertext="Status" sortexpression="Status">
                                            <itemtemplate>									
                                            <asp:Label id="lblStatus" runat="server" Text='<%#IIF(isdbnull(Container.DataItem("StatusDesc")),IIF(Container.DataItem("CStatus")=111,"Pending Signature","Pending HL7"),Container.DataItem("StatusDesc"))%>'></asp:Label>                                            
                                            </itemtemplate>
                                            <headerstyle width="20px" />
                                        </asp:templatecolumn>
                                        <asp:templatecolumn headertext="Signed" sortexpression="CStatus">
                                            <itemtemplate>									
                                            <asp:Label id="lblCStatus" runat="server" Text='<%#IIF(Container.DataItem("CStatus")=111,"Pending Signature","Signed")%>'></asp:Label>                                            
                                            </itemtemplate>
                                            <headerstyle width="20px" />
                                        </asp:templatecolumn>  
                                        <asp:templatecolumn headertext="Physician" sortexpression="Physician">
                                            <itemtemplate>									
                                            <asp:Label id="lblPhysician" runat="server" Text='<%#Container.DataItem("Physician")%>'></asp:Label>                                          
                                            </itemtemplate>
                                            <headerstyle width="20px" />
                                        </asp:templatecolumn>                                     
                                        <asp:boundcolumn datafield="SubmitDate" headertext="Submit Date" sortexpression="SubmitDate">
                                            <itemstyle horizontalalign="Right" />
                                        </asp:boundcolumn>
                                        <asp:boundcolumn datafield="DateFinished" headertext="Date Finished" sortexpression="DateFinished">
                                            <itemstyle horizontalalign="Right" />
                                        </asp:boundcolumn>
                                        <asp:boundcolumn datafield="JobNumber" headertext="Job Number" sortexpression="JobNumber">
                                            <itemstyle horizontalalign="Right" />
                                        </asp:boundcolumn>
                                        <asp:boundcolumn datafield="CustJobID" headertext="Client Job#" sortexpression="CustJobID">
                                            <itemstyle horizontalalign="Right" />
                                        </asp:boundcolumn>                                      
                                        <asp:boundcolumn datafield="PtName" headertext="Patient Name" sortexpression="PtName">
                                            <itemstyle horizontalalign="Right" />
                                             </asp:boundcolumn>     
                                       <asp:boundcolumn datafield="DOS" headertext="Date Of Service" sortexpression="DOS">
                                            <itemstyle horizontalalign="Right" />
                                             </asp:boundcolumn>     
                                        <asp:boundcolumn datafield="MRN" headertext="MRN" sortexpression="MRN">
                                            <itemstyle horizontalalign="Right" />
                                        </asp:boundcolumn>                                        
                                    </Columns>
                                    <PagerStyle Mode="NumericPages" />
                                </DBWC:HierarGrid>
                            </td>
                        </tr>
                    </table>                 
                  </asp:Panel>
            </ContentTemplate>
        </asp:UpdatePanel>--%>
        <div style="text-align:left;">
            <asp:Button ID="BtnSetStatus" runat="server" Font-Names="Trebuchet MS" Font-Size="10" Text="Set Pending HL7 Status" />    
        </div>
    </div>
    </form>
</body>
</html>
