<%@ Page Language="VB" AutoEventWireup="false" CodeFile="PrevJobsResult.aspx.vb" Inherits="PrevJobsResult" %>
<%@ Register TagPrefix="DBWC" Namespace="DBauer.Web.UI.WebControls" Assembly="DBauer.Web.UI.WebControls.HierarGrid" %>
<%@ Register
    Assembly="AjaxControlToolkit"
    Namespace="AjaxControlToolkit"
    TagPrefix="ajaxToolkit" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 1.0 Transitional/EN">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Previous Job Result</title>
    <link href= "../App_Themes/Css/Default.css" type="text/css" rel="stylesheet"/>
    <link href= "../App_Themes/Css/Styles.css" type="text/css" rel="stylesheet"/>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <ajaxToolkit:ToolkitScriptManager ID="ScriptManager1" runat="server">
        </ajaxToolkit:ToolkitScriptManager>
        <%--<asp:UpdatePanel ID="update" runat="server">
            <ContentTemplate>--%>
                <asp:Panel ID="iMain" runat="server" Height="" HorizontalAlign="Left" Visible="false" Width="100%" Wrap="False">
                    <table width="100%" height="100%">
                        <tr>
                            <td style="height: 197px">                                
                                <DBWC:HierarGrid ID="dlist"  allowsorting="True" autogeneratecolumns="False" style="Z-INDEX: 101" runat="server" BorderColor="#999999" TemplateCachingBase="Tablename" horizontalalign="Left" HeaderStyle-ForeColor="White" AllowPaging="False" Height="100%" Width="100%" >
                                    <SelectedItemStyle Font-Bold="True" ForeColor="White" BackColor="#008A8C"></SelectedItemStyle>
				        <AlternatingItemStyle BackColor ="Gainsboro" >		
				        </AlternatingItemStyle>
				        <ItemStyle  BackColor="Beige" Font-Names="Verdana" Font-Size="8pt" Wrap="False" ></ItemStyle>
				        <HeaderStyle CssClass="alt1"></HeaderStyle>				
                                    <Columns>
                                        <asp:templatecolumn>
                                            <headertemplate></headertemplate>
                                            <itemtemplate>
                                                <asp:HiddenField ID="hdnTransID" Value='<%#Container.DataItem("TranscriptionID")%>' runat="server" />                                
                                                <asp:HyperLink ID="HyperLink2" Text='<%#Container.DataItem("JobNumber")%>' NavigateUrl='<%#"SaveasDictation.aspx?DocID=" & Container.DataItem("TranscriptionID").tostring & "&JobID=" & Container.DataItem("JobNumber").tostring %>' Target="_blank" runat="server" tooltip="Click here to view transcription" />
                                                <%--<asp:HyperLink ID="HyperLink1" NavigateUrl='<%#"ShowVersion.aspx?DocID=" & Container.DataItem("TranscriptionID").tostring & ".doc"%>' Text='<%#Container.DataItem("JobNumber")%>' Target="_blank" runat="server" tooltip="Click here to view transcription" />--%>
                                            </itemtemplate>
                                            <headerstyle width="20px" />
                                        </asp:templatecolumn>                                                                              
                                        <asp:templatecolumn headertext="DateDictated" sortexpression="DateDictated">
                                            <itemtemplate>									
                                        <asp:Label id="lblDateDictated" runat="server" Text='<%#Container.DataItem("DateDictated")%>'></asp:Label>                                            
                                            </itemtemplate>
                                            <headerstyle width="20px" />
                                        </asp:templatecolumn>
                                        <asp:templatecolumn headertext="PatientName" sortexpression="PatientName">
                                            <itemtemplate>									
                                        <asp:Label id="lblPatientName" runat="server" Text='<%#Container.DataItem("PatientName")%>'></asp:Label>                                            
                                            </itemtemplate>
                                            <headerstyle width="20px" />
                                        </asp:templatecolumn>   
                                        <asp:templatecolumn headertext="DOS" sortexpression="PatientDOS">
                                            <itemtemplate>									
                                            <asp:Label id="lblPatientDOS" runat="server" Text='<%#Container.DataItem("PatientDOS")%>'></asp:Label>
                                            </itemtemplate>
                                            <headerstyle width="20px" />
                                        </asp:templatecolumn>                                     
                                        <asp:boundcolumn datafield="DictatorName" headertext="DictatorName" sortexpression="DictatorName">
                                            <itemstyle horizontalalign="Right" />
                                        </asp:boundcolumn>
                                        <asp:boundcolumn datafield="AccountName" headertext="AccountName" sortexpression="AccountName">
                                            <itemstyle horizontalalign="Right" />
                                        </asp:boundcolumn>
                                        <asp:boundcolumn datafield="TemplateName" headertext="TemplateName" sortexpression="TemplateName">
                                            <itemstyle horizontalalign="Right" />
                                        </asp:boundcolumn>                                                                             
                                    </Columns>
                                    <PagerStyle Mode="NumericPages" />
                                </DBWC:HierarGrid>
                            </td>
                        </tr>
                    </table>                 
                  </asp:Panel>               
            <%--</ContentTemplate>
        </asp:UpdatePanel>--%>
        <asp:Label ID="Label1" runat="server" Text="Label"></asp:Label>
    </div>
    </form>
</body>
</html>
