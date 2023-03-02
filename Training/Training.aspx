<%@ Page Language="VB" AutoEventWireup="false" CodeFile="Training.aspx.vb" Inherits="Training_Result" %>
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
    <ajaxToolkit:ToolkitScriptManager ID="ScriptManager1" EnablePartialRendering="true" EnableScriptGlobalization="true" EnableScriptLocalization="true" runat="Server" />
    <div>
    <p>  
    <br />
    <br />  
                  <asp:DropDownList ID="DDLPhysicians" runat="server" Width="256px" TabIndex="1" />            
                  <asp:Button ID="btnGO" runat="server" Text="Search Reports" />
    </p>    
    </div>                                 
        <table>
        <tr>
            <td>           
            
            <DBWC:HierarGrid id="dlist" DataKeyField="TranscriptionID" allowsorting="True" autogeneratecolumns="False" style="Z-INDEX: 101" runat="server" BorderColor="#999999" BorderStyle="None" BorderWidth="1px" BackColor="White" CellPadding="3" gridlines="Vertical" horizontalalign="Left" HeaderStyle-ForeColor="White" AllowPaging="True" PageSize="50">
				    <SelectedItemStyle Font-Bold="True" ForeColor="White" BackColor="#008A8C"></SelectedItemStyle>
				        <AlternatingItemStyle BackColor ="Gainsboro" >		
				        </AlternatingItemStyle>
				        <ItemStyle  BackColor="Beige" Font-Names="Verdana" Font-Size="8pt" Wrap="False" ></ItemStyle>
				        <HeaderStyle CssClass="SMParentSelected" ForeColor="White"></HeaderStyle>				
				        <Columns>
				            <asp:templatecolumn>
				            <headertemplate>
				            									
				                     <input id="chkAll" type="checkbox" onclick="CheckAllDataGridCheckBoxes('chkItem',this.checked)"/>					                     
				            
</headertemplate>	                    				            				            
							<itemtemplate>
																					    
                                <asp:HiddenField ID="hdnID" Value='<%#Container.DataItem("TranscriptionID")%>' runat="server" />                                
                                <asp:HiddenField ID="hdnType" Value='<%#Container.DataItem("Type")%>' runat="server" />   
                               <asp:LinkButton id="LinkButton1" runat="server" onclick="LinkButton1_Click">Edit</asp:LinkButton>
				            
</itemtemplate>            
				            
				            
							<headerstyle width="20px" />
							</asp:templatecolumn>				                 
				<asp:boundcolumn datafield="PhysicianName"	sortexpression="PhysicianName" headertext="Physician Name">
                    <itemstyle horizontalalign="Right" />
                </asp:boundcolumn>
                <asp:boundcolumn datafield="AccountName"	sortexpression="AccountName" headertext="Account Name" >
                    <itemstyle horizontalalign="Right" />
                </asp:boundcolumn> 				                               
                <asp:boundcolumn datafield="JobNumber"	sortexpression="JobNumber" headertext="Job Number">
                    <itemstyle horizontalalign="Right" />
                </asp:boundcolumn>
               <asp:boundcolumn datafield="CustJobID"	sortexpression="CustJobID" headertext="Voice Job#">
                    <itemstyle horizontalalign="Right" />
                </asp:boundcolumn>
                 <asp:boundcolumn datafield="duration"	sortexpression="duration" headertext="Duration">
                    <itemstyle horizontalalign="Right" />
                </asp:boundcolumn>
				</Columns>				
			</DBWC:HierarGrid>
            </td>
            </tr>			
            </table>
            
 
    
        
        <ajaxToolkit:ListSearchExtender ID="ListSearchExtender2" runat="server"
                TargetControlID="DDLPhysicians" PromptCssClass="ListSearchExtenderPrompt">
            </ajaxToolkit:ListSearchExtender>
    </form>
          
     
</body>
</html>
