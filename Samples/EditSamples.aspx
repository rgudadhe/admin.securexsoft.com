<%@ Page Language="VB" AutoEventWireup="false" CodeFile="EditSamples.aspx.vb" Inherits="Samples_EditSamples" %>
<%@ Register TagPrefix="DBWC" Namespace="DBauer.Web.UI.WebControls" Assembly="DBauer.Web.UI.WebControls.HierarGrid" %>
<%@ Register
    Assembly="AjaxControlToolkit"
    Namespace="AjaxControlToolkit"
    TagPrefix="ajaxToolkit" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 1.0 Transitional/EN">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Edit Samples</title>
    <link href= "../App_Themes/Css/styles.css" type="text/css" rel="stylesheet"/>
    <link href= "../App_Themes/Css/Common.css" type="text/css" rel="stylesheet"/>
    <link href= "../App_Themes/Css/Routing.css" type="text/css" rel="stylesheet"/>
   
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
        <div id="body">
        <div id="cap"></div>
        <div id="main">
        <h1>Edit Dictation Samples</h1>
    <ajaxToolkit:ToolkitScriptManager ID="ScriptManager1" EnablePartialRendering="true" EnableScriptGlobalization="true" EnableScriptLocalization="true" runat="Server" />
        <asp:Panel ID="Panel1" runat="server" HorizontalAlign="Left">
            <div>
    <p>  
                  <asp:DropDownList ID="DDLPhysicians" runat="server" Width="256px" TabIndex="1" />  &nbsp
                  <asp:Button ID="btnGO" CssClass="button" runat="server" Text="Search Samples" />
    </p>    
    </div>
    <table>
        <tr>
            <td>           
            
            <DBWC:HierarGrid id="dlist" DataKeyField="SampleID" allowsorting="True" autogeneratecolumns="False" style="Z-INDEX: 101" runat="server" TemplateCachingBase="Tablename" horizontalalign="Left" LoadControlMode="UserControl" PageSize="30">
				    <SelectedItemStyle Font-Bold="True" ForeColor="White" BackColor="#008A8C"></SelectedItemStyle>
				        <AlternatingItemStyle BackColor ="Gainsboro" >		
				        </AlternatingItemStyle>
				        <ItemStyle  BackColor="Beige" CssClass="common" Wrap="False" ></ItemStyle>
				        <HeaderStyle ForeColor="Black" Font-Bold="true" />
				        <Columns>
				            <asp:templatecolumn HeaderStyle-CssClass="alt1" HeaderStyle-Font-Bold="true">
				            <headertemplate>
				            									
				                     <input id="chkAll" type="checkbox" onclick="CheckAllDataGridCheckBoxes('chkItem',this.checked)"/>					                     
				            
</headertemplate>	                    				            				            
							<itemtemplate>
																
							    <asp:CheckBox ID="chkJob"  runat="server"  checked=<%#IIf(request.querystring("chk") = "True", True, False)%>/>
                                <asp:HiddenField ID="hdnID" Value='<%#Container.DataItem("SampleID")%>' runat="server" />                                
                                <asp:LinkButton id="LinkButton1" runat="server"     onclick="LinkButton1_Click">Edit</asp:LinkButton>
				            
</itemtemplate>            
				            
				            
							<headerstyle width="20px" />
							</asp:templatecolumn>				                 
				<asp:boundcolumn HeaderStyle-CssClass="alt1" HeaderStyle-Font-Bold="true" datafield="PhysicianName"	sortexpression="PhysicianName" headertext="Physician Name">
                    <itemstyle horizontalalign="Right" />
                </asp:boundcolumn>
                <asp:boundcolumn HeaderStyle-CssClass="alt1" HeaderStyle-Font-Bold="true" datafield="SampleName"	sortexpression="SampleName" headertext="Sample Name" >
                    <itemstyle horizontalalign="Right" />
                </asp:boundcolumn> 				                               
                <asp:boundcolumn HeaderStyle-CssClass="alt1" HeaderStyle-Font-Bold="true" datafield="DateModified"	sortexpression="DateModified" headertext="Date Set">
                    <itemstyle horizontalalign="Right" />
                </asp:boundcolumn>                             
				</Columns>				
			</DBWC:HierarGrid>
            </td>
            </tr>			
            </table>
            <asp:Button ID="btnDelete" runat="server" CssClass="button" Text="Remove Selected" Visible="false" />
        <ajaxToolkit:ListSearchExtender ID="ListSearchExtender2" runat="server"
                TargetControlID="DDLPhysicians" PromptCssClass="ListSearchExtenderPrompt">
            </ajaxToolkit:ListSearchExtender>
        </asp:Panel>
            <asp:GridView ID="GridView1" runat="server">
                <Columns>
                    <asp:BoundField HeaderStyle-Font-Bold="true" />
                </Columns>
            </asp:GridView>
        </div> 
        </div> 
    </form>
</body>
</html>
