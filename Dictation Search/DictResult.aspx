<%@ Page Language="VB" AutoEventWireup="false" CodeFile="DictResult.aspx.vb" Inherits="Dictation_Search_DictResult" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Dictation Status</title> 
    <link href= "../App_Themes/Css/styles.css" type="text/css" rel="stylesheet" />
    <link href= "../App_Themes/Css/Common.css" type="text/css" rel="stylesheet" />
    <link href="../App_Themes/Css/DataTable.css" rel="stylesheet" type="text/css" />
    <link href="../App_Themes/Css/TableSorter.css" rel="stylesheet" type="text/css" />
    <script src="../App_Themes/JS/jquery-1.4.2.min.js" type="text/javascript"></script>  
    <script src="../App_Themes/JS/jquery.dataTables.min.js" type="text/javascript"></script>  
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
		                            { "asSorting": [ "desc", "asc" ] }
	                              ] 
				} );
			} );
</script>
    <script language="javascript" type="text/javascript">
//        function Display(obj)
//        {
//            if (obj.value==1)
//            {
//                 hidecntrls();
//                 document.getElementById("lstStatus").style.visibility="visible";
//            }
//            if (obj.value==2)
//            {
//                hidecntrls();
//                document.getElementById("lstLevel").style.visibility="visible";
//                document.getElementById("txtUser").style.visibility="visible";
//            }
//            if (obj.value==3)
//            {
//                hidecntrls();
//                document.getElementById("txtTAT").style.visibility="visible";
//            }
//            if (obj.value==4)
//            {
//                hidecntrls();
//            }
//            if (obj.value==5)
//            {
//                hidecntrls();
//                document.getElementById("txtPhy").style.visibility="visible";
//            }
//        }
//        function hidecntrls()
//        {
//            document.getElementById("lstStatus").style.visibility="hidden";
//            document.getElementById("lstLevel").style.visibility="hidden";
//            document.getElementById("lstStatus").style.visibility="hidden";
//            document.getElementById("txtUser").style.visibility="hidden";
//            document.getElementById("txtPhy").style.visibility="hidden";
//            document.getElementById("txtTAT").style.visibility="hidden";
//        }
        function HideGrid()
        {
            var res;
            res=confirm('Are you certain you want to perform this action?')
            
            if (res)
            {
                document.getElementById("DivMain").style.visibility="hidden";
                return true;
            }
            else
            {
                return false;
            }    
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        <div id="Opr" runat="server" visible="false">
        <table width="100%" style="text-align:left">
            <tr>
                <td style="border:0; width:auto">
                    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                        <ContentTemplate>
                            <asp:DropDownList ID="lstOptions" runat="server" AutoPostBack="true" OnSelectedIndexChanged="lstOptions_SelectedIndexChanged">
                                <asp:ListItem Selected="True" Value="0">Please Select</asp:ListItem>
                                <asp:ListItem Value="1">Update Status</asp:ListItem>
                                <asp:ListItem Value="2">Assign to Users</asp:ListItem>
                                <asp:ListItem Value="3">Change TAT</asp:ListItem>
                                <asp:ListItem Value="4">Set As Sample</asp:ListItem>
                                <asp:ListItem Value="5">Change Physician</asp:ListItem>  
                            </asp:DropDownList>
                            <asp:DropDownList ID="lstStatus" runat="server" DataTextField="LevelName" DataValueField="LevelNo" Visible="false">
                            </asp:DropDownList>
                            <asp:DropDownList ID="lstLevel" runat="server" Width="200" AutoPostBack="true" Visible="false">
                            </asp:DropDownList>
                            <asp:TextBox ID="txtUser" runat="server" Visible="false" Width="200"></asp:TextBox>
                            <asp:TextBox ID="txtTAT" runat="server" Width="100" Visible="false"></asp:TextBox>
                            <asp:TextBox ID="txtPhy" runat="server" Width="150" Visible="false"></asp:TextBox>    
                            <asp:Button CssClass="button" ID="btnStatus" runat="server" Text="GO" OnClick="UpdateStatus" OnClientClick="javascript:return HideGrid();" UseSubmitBehavior="True" CausesValidation="False" />
                            <ajaxToolkit:AutoCompleteExtender ID="AutoCompletPhy" runat="server" MinimumPrefixLength="1" EnableCaching="true" CompletionSetCount="10" TargetControlID="txtPhy" ServicePath = "../users/autocomplete.asmx" ServiceMethod = "GetPhyNames"></ajaxToolkit:AutoCompleteExtender>
                            <br />
                            <asp:Label ID="Label1" runat="server" Text=""></asp:Label>
                            <asp:Label ID="Label2" runat="server" Text=""></asp:Label>
                       </ContentTemplate>
                    </asp:UpdatePanel>
                </td>
                </tr> 
        </table>
        </div>
        <div id="DivMain" visible="true" runat="server">
            <asp:Panel ID="Panel1" runat="server">
                <table id="tMain" runat="server" style="z-index:1;">
                    <tr>
                        <td style="width:100%; border:0">
                            <asp:LinkButton ID="LinkButton1" runat="server"  >Export Result</asp:LinkButton>                     
                        </td>
                    </tr>
                    <tr>
                        <td style="width:100%; border:0">
                            <asp:GridView ID="dlist" runat="server" AutoGenerateColumns="false" Width="100%">
                                <Columns>
                                    <asp:TemplateField HeaderStyle-CssClass="header">
                                        <ItemTemplate>
                                            <asp:ImageButton ID="btnHistory" runat="server" ImageUrl="~/App_Themes/Images/his.png" ToolTip="Job history"   />                                                
                                            <asp:HiddenField ID="hdnTransID" Value='<%#Container.DataItem("TranscriptionID")%>' runat="server" />
                                            <asp:HiddenField ID="hdnStatus" Value='<%#Container.DataItem("Status")%>' runat="server" />
                                            <asp:HiddenField ID="hdnAccName" Value='<%#Container.DataItem("AccountName")%>' runat="server" />
                                            <asp:HiddenField ID="hdnAccNo" Value='<%#Container.DataItem("AccountNo")%>' runat="server" />
                                            <asp:HiddenField ID="hdnContractorName" Value='<%#Container.DataItem("ContractorName")%>' runat="server" />
                                            <asp:HiddenField ID="hdnDictatorName" Value='<%#Container.DataItem("DictatorName")%>' runat="server" />
                                            <asp:HiddenField ID="hdnPinNo" Value='<%#Container.DataItem("PINNo")%>' runat="server" />
                                            <asp:HiddenField ID="hdnSignedName" Value='<%#Container.DataItem("SignedName")%>' runat="server" />
                                            <asp:HiddenField ID="hdnJobNo" Value='<%#Container.DataItem("JobNumber")%>' runat="server" />
                                            <asp:HiddenField ID="hdnCustJobNo" Value='<%#Container.DataItem("CustJobID")%>' runat="server" />
                                            <asp:HiddenField ID="hdnDtCreated" Value='<%#Container.DataItem("DateCreated")%>' runat="server" />
                                            <asp:HiddenField ID="hdnTAT" Value='<%#Container.DataItem("TAT")%>' runat="server" />
                                            <asp:HiddenField ID="hdnDtDictated" Value='<%#Container.DataItem("DateDictated")%>' runat="server" />
                                            <asp:HiddenField ID="hdnRemaining" Value='<%#datediffToMe(Container.DataItem("TAT"), Container.DataItem("SubmitDate"))%>' runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField>
                                        <headertemplate>				            									
	                                         <input id="Checkbox1" type="checkbox" onclick="CheckAllDataGridCheckBoxes('chkItem',this.checked)"/>
	                                    </headertemplate>
	                                    <itemtemplate>									
				                            <asp:CheckBox ID="chkJob" runat="server"  checked='<%#IIf(request.querystring("chk") = "True", True, False)%>' />
                                            <asp:HiddenField ID="hdnID" Value='<%#Container.DataItem("TranscriptionID")%>' runat="server" />
	                                    </itemtemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField headertext="Tracking#" HeaderStyle-CssClass="Header">
                                        <itemtemplate>									
                                            <asp:HyperLink ID="HyperLink1" NavigateUrl='<%#"ShowVersion.aspx?DocID=" & Container.DataItem("TranscriptionID").tostring & ".doc"%>' Text='<%#Container.DataItem("JobNumber")%>' Target="_blank" runat="server" tooltip="Click here to view transcription" />&nbsp<asp:HyperLink ID="HyperLink2" imageURL="../images/VolumeNormalBlue.png" NavigateUrl='<%#"SaveasDictation.aspx?DocID=" & Container.DataItem("TranscriptionID").tostring & Container.DataItem("Type") & "&JobID=" & Container.DataItem("JobNumber").tostring & Container.DataItem("Type")%>' Target="_blank" runat="server" tooltip="Click here to download dictation" />
                                        </itemtemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Cust. Job#" HeaderStyle-CssClass="Header">
                                        <ItemTemplate>
                                            <%#Container.DataItem("CustJobID")%>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Status" HeaderStyle-CssClass="Header">
                                        <ItemTemplate>
                                            <%#Container.DataItem("StatusName")%>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Duration" HeaderStyle-CssClass="Header">
                                        <ItemTemplate>
                                            <%#Container.DataItem("Duration")%>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Submit Date" HeaderStyle-CssClass="Header">
                                        <ItemTemplate>
                                            <%#Container.DataItem("SubmitDate").ToShortDateString()%>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="TAT" HeaderStyle-CssClass="Header">
                                        <ItemTemplate>
                                            <%#Container.DataItem("TAT")%>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Dictator Name" HeaderStyle-CssClass="Header">
                                        <ItemTemplate>
                                            <%#Container.DataItem("DictatorName")%>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Account Name" HeaderStyle-CssClass="Header">
                                        <ItemTemplate>
                                            <%#Container.DataItem("AccountName")%>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Template Name" HeaderStyle-CssClass="Header">
                                        <ItemTemplate>
                                            <%#Container.DataItem("Templatename")%>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Priority" HeaderStyle-CssClass="Header">
                                        <ItemTemplate>
                                            <%#Container.DataItem("Priority")%>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </td>
                    </tr>
                </table>
            </asp:Panel>
            <%--<asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:ETSConnectionString %>"
                                SelectCommand="SELECT 0 as LevelNo,'Select Status' as LevelName union SELECT LevelNo,LevelName FROM tblProductionLevels where IsDeleted<>1  and type=@IsContractor and LevelNo not in (SELECT Audit FROM tblRSSStatus where iscontractor=@IsContractor and ContractorID=case @IsContractor when 1 then @ContractorID else @ParentID end) and (LevelNo in (3,5)  or ContractorID=case @IsContractor when 1 then @ContractorID else @ParentID end)">
                                <SelectParameters>
                                    <asp:SessionParameter Name="IsContractor" SessionField="IsContractor" />
                                    <asp:SessionParameter Name="ParentID" SessionField="ParentID" />
                                    <asp:SessionParameter Name="ContractorID" SessionField="ContractorID" />
                                </SelectParameters>
                            </asp:SqlDataSource>--%>
        </div>
    </form>
</body>
</html>
