<%@ Page Language="VB" AutoEventWireup="false" CodeFile="DictaResult_SPN.aspx.vb" Inherits="ets.Dictation_Search_DictaResult" %>
<%@ Register TagPrefix="DBWC" Namespace="DBauer.Web.UI.WebControls" Assembly="DBauer.Web.UI.WebControls.HierarGrid" %>
<%@ Register
    Assembly="AjaxControlToolkit"
    Namespace="AjaxControlToolkit"
    TagPrefix="ajaxToolkit" %>

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>Dictation Status</title> 
    <link href= "../App_Themes/Css/styles.css" type="text/css" rel="stylesheet" />
    <link href= "../App_Themes/Css/Common.css" type="text/css" rel="stylesheet" />
    <link href="../App_Themes/Css/DataTable.css" rel="stylesheet" type="text/css" />
    <link href="../App_Themes/Css/TableSorter.css" rel="stylesheet" type="text/css" />
    <script src="../App_Themes/JS/jquery-1.4.2.min.js" type="text/javascript"></script>  
    <script src="../App_Themes/JS/jquery.dataTables.min.js" type="text/javascript"></script>  
    <script type="text/javascript" language="javascript">
        function openPopup(varTransID,varStatus,varAccName,varAccNo,ContractorName,varDictatorName,varPINNo,SignedName,varJobNo,varCustJobNo,varDtCreated,varTAT,varDtDictated,varRemaining)
        {
//            var aForm;
//            aForm = form1.elements;
//            var myObject = new Object();
//            myObject.TransID = varTransID;
//            myObject.Status = varStatus;
//            myObject.AccName = varAccName;
//            myObject.AccNo = varAccNo;
//            myObject.AccNo = ContractorName;
//            myObject.DictatorName = varDictatorName;
//            myObject.PINNo = varPINNo;
//            myObject.SignedName = SignedName;

        

              document.getElementById('MhdnTransID').value=varTransID;
              document.getElementById('MhdnStatus').value=varStatus;
              document.getElementById('MhdnAccName').value=varAccName;
              document.getElementById('MhdnAccNo').value=varAccNo;
              document.getElementById('MhdnContractorName').value=ContractorName;
              document.getElementById('MhdnDictatorName').value=varDictatorName;
              document.getElementById('MhdnPinNo').value=varPINNo;
              document.getElementById('MhdnSignedName').value=SignedName;
              document.getElementById('MhdnJobNo').value=varJobNo;
              document.getElementById('MhdnCustJobNo').value=varCustJobNo;
              document.getElementById('MhdnDtCreated').value=varDtCreated;
              document.getElementById('MhdnTAT').value=varTAT;
              document.getElementById('MhdnDtDictated').value=varDtDictated;
              document.getElementById('MhdnRemaining').value=varRemaining;
            var displayWindow;
            displayWindow = window.open('', "newWin4", "width=850,height=540, scrollbars=0,menubar=0,toolbar=0,location=0,status=1");
    		document.form1.target = "newWin4";
            document.form1.action="JobHistory.aspx" 
    		document.form1.submit();
  		    document.form1.target = "_self";
  		    document.form1.action="DictaResult_SPN.aspx"; 
  		    
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
		                            { "asSorting": [ "desc", "asc" ] },
		                            { "asSorting": [ "desc", "asc" ] }
	                              ] 
				} );
			} );
</script>

<script type="text/javascript" language="javascript">
 function CheckAllDataGridCheckBoxes(aspCheckBoxID, checkVal)
 {
  re = new RegExp(':' + aspCheckBoxID + '$')  //generated control name starts with a colon
  for(i = 0; i < form1.elements.length; i++)
  {
   elm = document.forms[0].elements[i]
   if (elm.type == 'checkbox')
   {
    //if (re.test(elm.name))
     elm.checked = checkVal
   }
  }
 }
 
</script>

</head>
<body>
    <form id="form1" runat="server">
    <ajaxToolkit:ToolkitScriptManager runat="server" ID="ScriptManager1" />
    
    <table>
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
                        <tr>
                            <td style="border:0">
                                <asp:UpdatePanel ID="Update" runat="server">
                                    <ContentTemplate>
                                            <asp:DropDownList ID="lstOptions" runat="server" OnSelectedIndexChanged="lstOptions_SelectedIndexChanged" AutoPostBack="true">
                                                <asp:ListItem Selected="True">Please Select</asp:ListItem>
                                                <asp:ListItem Value="1">Update Status</asp:ListItem>
                                                <asp:ListItem Value="2">Assign to Users</asp:ListItem>
                                                <asp:ListItem Value="3">Change TAT</asp:ListItem>
                                                <asp:ListItem Value="4">Set As Sample</asp:ListItem>
                                                <asp:ListItem Value="5">Change Physician</asp:ListItem>  
                                            </asp:DropDownList>
                                        
                                        <asp:DropDownList ID="lstStatus" runat="server" DataSourceID="SqlDataSource1" DataTextField="LevelName" DataValueField="LevelNo" Visible="false">
                                        </asp:DropDownList>
                                        <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:ETSConnectionString %>"
                                            SelectCommand="SELECT 0 as LevelNo,'Select Status' as LevelName union SELECT LevelNo,LevelName FROM tblProductionLevels where IsDeleted<>1  and type=@IsContractor and LevelNo not in (SELECT Audit FROM tblRSSStatus where iscontractor=@IsContractor and ContractorID=case @IsContractor when 1 then @ContractorID else @ParentID end) and (LevelNo in (3,5)  or ContractorID=case @IsContractor when 1 then @ContractorID else @ParentID end)">
                                            <SelectParameters>
                                                <asp:SessionParameter Name="IsContractor" SessionField="IsContractor" />
                                                <asp:SessionParameter Name="ParentID" SessionField="ParentID" />
                                                <asp:SessionParameter Name="ContractorID" SessionField="ContractorID" />
                                            </SelectParameters>
                                        </asp:SqlDataSource>   
                                        <asp:TextBox ID="txtTAT" runat="server" Visible="false"></asp:TextBox>    
                                        <asp:DropDownList ID="lstLevel" runat="server"  DataTextField="LevelName" DataValueField="LevelNo" Visible="false" OnSelectedIndexChanged="lstLevel_SelectedIndexChanged" AutoPostBack="true">
                                        </asp:DropDownList>
                                        <asp:TextBox ID="txtUser" runat="server" Visible="False" Width="350Px"></asp:TextBox>    
                                        <asp:TextBox ID="txtPhy" runat="server" Visible="False" Width="350Px"></asp:TextBox>     
                                        <asp:Button CssClass="button" ID="btnStatus" runat="server" Text="GO" OnClick="btnStatus_Click" OnClientClick="return confirm('Are you certain you want to perform this action?');" UseSubmitBehavior="True" CausesValidation="False"/>
                                        <asp:HiddenField ID="intCurrIndex" Visible="False" Runat="server" />
                                        <asp:HiddenField ID="intPageSize" Visible="False" Runat="server" />
                                        <asp:HiddenField ID="intRecordCount"  Visible="False" Runat="server" /> 
                                        <asp:HiddenField ID="hdnWhereClause" Visible="False" Runat="server" />
                                        <asp:HiddenField ID="hdnIsUserCri" Visible="False" Runat="server" />
                                        <asp:HiddenField ID="hdnOrderBy" Visible="False" Runat="server" />
                                        <asp:HiddenField ID="hdnSortOrder" Visible="False" Runat="server" />
                                        <asp:HiddenField ID="hdnSortItem" Visible="False" Runat="server" />
                                         <asp:Panel ID="iMessage" runat="server" Height="" Width="100%" Visible="false">
                                                 <asp:Table ID="tblUp" runat="server">
                                                    </asp:Table>     
                                                    <asp:Label ID="lblMessage" runat="server" Text="Label"></asp:Label>
                                         <br />
                                                    <asp:Button CssClass="button"  ID="btnBack" runat="server" Text="<< Back to Result" />
                                                </asp:Panel>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </td>
                        </tr>
                    </table>
                </asp:Panel>                
            </td>
        </tr>
    </table>
    
    
        
        <asp:UpdateProgress ID="UpdateProgress1" runat="server">
        <ProgressTemplate>
        Please wait...
        </ProgressTemplate>
        </asp:UpdateProgress>
        <asp:HiddenField ID="MhdnTransID" runat="server" />
        <asp:HiddenField ID="MhdnStatus" runat="server" />
        <asp:HiddenField ID="MhdnAccName" runat="server" />
        <asp:HiddenField ID="MhdnAccNo" runat="server" />
        <asp:HiddenField ID="MhdnContractorName" runat="server" />
        <asp:HiddenField ID="MhdnDictatorName" runat="server" />
        <asp:HiddenField ID="MhdnPinNo" runat="server" />
        <asp:HiddenField ID="MhdnSignedName" runat="server" />
        <asp:HiddenField ID="MhdnJobNo" runat="server" />
        <asp:HiddenField ID="MhdnCustJobNo" runat="server" />
        <asp:HiddenField ID="MhdnDtCreated" runat="server" />
        <asp:HiddenField ID="MhdnTAT" runat="server" />
        <asp:HiddenField ID="MhdnDtDictated" runat="server" />
        <asp:HiddenField ID="MhdnRemaining" runat="server" />
    </form>
    
    
</body>
</html>
