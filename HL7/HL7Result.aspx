<%@ Page Language="VB" AutoEventWireup="false" CodeFile="HL7Result.aspx.vb" Inherits="FaxPlus_FaxPlusResult" %>
<%@ Register TagPrefix="DBWC" Namespace="DBauer.Web.UI.WebControls" Assembly="DBauer.Web.UI.WebControls.HierarGrid" %>
<%@ Register
    Assembly="AjaxControlToolkit"
    Namespace="AjaxControlToolkit"
    TagPrefix="ajaxToolkit" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 1.0 Transitional/EN">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Untitled Page</title>
    <link href= "../styles/Default.css" type="text/css" rel="stylesheet"/>
</head>
<body>
    <form id="form1" runat="server">
    <div>
                                  
        <ajaxToolkit:ToolkitScriptManager ID="ScriptManager1" runat="server">
        </ajaxToolkit:ToolkitScriptManager>
        
                    <asp:UpdatePanel ID="update" runat="server">
            <ContentTemplate>
                <asp:Panel ID="iMain" runat="server" Height="" Visible="false" Width="100%" Wrap="False">
                    <table width="100%" height="100%">
                        <tr>
                            <td style="height: 197px">  
                            <asp:LinkButton ID="LinkButton1" runat="server" Font-Names="Verdana" Font-Size="8pt">Export Result</asp:LinkButton> 
                            <br />
                           <%-- </td>
                        </tr>
                        <tr>
                        <td style="height: 197px">  --%>
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
                                        <%--<asp:templatecolumn headertext="Signed" sortexpression="CStatus">
                                            <itemtemplate>									
                                            <asp:Label id="lblCStatus" runat="server" Text='<%#IIF(Container.DataItem("CStatus")=111,"Pending Signature","Signed")%>'></asp:Label>                                            
                                            </itemtemplate>
                                            <headerstyle width="20px" />
                                        </asp:templatecolumn>   --%>
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
        </asp:UpdatePanel>
        <asp:Button ID="BtnSetStatus" runat="server" Font-Names="Trebuchet MS" Font-Size="10" Text="Set Pending HL7 Status" />    
    </div>
    </form>
</body>
</html>
