<%@ Page Language="VB" AutoEventWireup="false" CodeFile="FileImportLogResultNew.aspx.vb" Inherits="ets.FileImportResultNew" EnableViewStateMac="false"  %>
<%@ Register TagPrefix="DBWC" Namespace="DBauer.Web.UI.WebControls" Assembly="DBauer.Web.UI.WebControls.HierarGrid" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 1.0 Transitional/EN">

<html xmlns="http://www.w3.org/1999/xhtml" >

<head runat="server">
    <title>File Import Log</title>
    <link href= "../App_Themes/Css/Main.css" type="text/css" rel="stylesheet" />
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
    <div>
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        <asp:Panel ID="Panel1" runat="server" Height="100%" Width="100%">
        <DBWC:HierarGrid id="dlist" CssClass="SMSelected" DataKeyField="RecordID" allowsorting="True" autogeneratecolumns="False" style="Z-INDEX: 101" runat="server" BorderColor="#999999" BorderStyle="None" BorderWidth="1px" BackColor="White" CellPadding="3" TemplateCachingBase="Tablename" gridlines="Vertical" horizontalalign="Left" HeaderStyle-ForeColor="White" LoadControlMode="UserControl">
				    <SelectedItemStyle Font-Bold="True" ForeColor="White" BackColor="#008A8C"></SelectedItemStyle>
				        <AlternatingItemStyle BackColor ="Gainsboro" >		
				        </AlternatingItemStyle>
				        <ItemStyle  BackColor="Beige"  Wrap="True" ></ItemStyle>
				        <HeaderStyle CssClass="SMSelected"></HeaderStyle>				
				        <Columns>
				            <asp:templatecolumn>
				            <headertemplate>				            									
				                     <input id="chkAll" type="checkbox" onclick="CheckAllDataGridCheckBoxes('chkItem',this.checked)"/>	
				            </headertemplate>	                    
							<itemtemplate>									
							    <asp:CheckBox ID="chkJob" runat="server"    />
                                <asp:HiddenField ID="hdnID" Value='<%#Container.DataItem("RecordID")%>' runat="server" />
                                <asp:HiddenField ID="hdnFileName" Value='<%#Container.DataItem("FileName")%>' runat="server" />
                                <asp:HiddenField ID="hdnCJobNumber" Value='<%#Container.DataItem("CJobNumber")%>' runat="server" />
                                <asp:HiddenField ID="hdnProcessID" Value='<%#Container.DataItem("ProcessID")%>' runat="server" />
				            </itemtemplate>
							<headerstyle width="20px" />
							</asp:templatecolumn>		
							                                     
				<asp:boundcolumn datafield="CJobNumber"	sortexpression="CJobNumber" headertext="Cust. Job#">
                    <itemstyle horizontalalign="Right" />
                </asp:boundcolumn>
                <asp:templatecolumn headertext="Client" sortexpression="FileName">
                    <itemtemplate>
                        <asp:label ID="lblClient" text='<%#mid(Container.DataItem("FileName"),1,InStr(Container.DataItem("FileName"),"_")-1)%>' runat="server" />
                    </itemtemplate><headerstyle width="20px" />
                </asp:templatecolumn>				
                
                 <asp:templatecolumn headertext="MD5Value" sortexpression="MD5Value">
                    <itemtemplate>
                        <asp:label ID="lblMD5Value" text='<%#Container.DataItem("MD5Value")%>' runat="server" />
                    </itemtemplate><headerstyle width="20px" />
                </asp:templatecolumn>
                
                 <asp:templatecolumn headertext="Status" sortexpression="FileName">
                    <itemtemplate>
                        <asp:label ID="lblStatus" text='<%#getStatus(Container.DataItem("Status").ToString)%>' runat="server" />
                    </itemtemplate><headerstyle width="20px" />
                </asp:templatecolumn>                                
                
				<asp:boundcolumn datafield="UserName"	sortexpression="UserName" headertext="User Name">
                    <itemstyle horizontalalign="Right" />
                </asp:boundcolumn>
                <asp:boundcolumn datafield="SettingName"	sortexpression="SettingName" headertext="Process Name">
                    <itemstyle horizontalalign="Right" />
                </asp:boundcolumn>					
                <asp:boundcolumn datafield="DateProcessed"	sortexpression="DateProcessed" headertext="Date Processed">
                    <itemstyle horizontalalign="Right" />
                </asp:boundcolumn>
                <asp:boundcolumn datafield="Version" sortexpression="Version" headertext="Version">
                    <itemstyle horizontalalign="Right" />
                </asp:boundcolumn>
				</Columns>
				
			</DBWC:HierarGrid>
            
        </asp:Panel>        
        <asp:Literal ID="Literal1" runat="server"></asp:Literal></div>    
        <BR>
        <asp:Button ID="btnReImport" runat="server" Text="Re-Import" CssClass="button" Visible="false" />
        <asp:HiddenField ID="Hsort" runat="server" />
        <asp:HiddenField ID="Horder" runat="server" />
        <asp:HiddenField ID="HdnWhereClause" runat="server" />
    </form>
</body>
</html>
