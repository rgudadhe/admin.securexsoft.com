<%@ Page Language="VB" AutoEventWireup="false" CodeFile="AddressBlock.aspx.vb" Inherits="Samples_EditSamples" %>
<%@ Register TagPrefix="DBWC" Namespace="DBauer.Web.UI.WebControls" Assembly="DBauer.Web.UI.WebControls.HierarGrid" %>
<%@ Register
    Assembly="AjaxControlToolkit"
    Namespace="AjaxControlToolkit"
    TagPrefix="ajaxToolkit" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 1.0 Transitional/EN">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Address Block</title>
    <link href= "../App_Themes/Css/Main.css" type="text/css" rel="stylesheet"/>
    <link href= "../App_Themes/Css/Common.css" type="text/css" rel="stylesheet"/>
    <link href= "../App_Themes/Css/Styles.css" type="text/css" rel="stylesheet"/>
</head>
<body>

    <form id="form1" runat="server">
        <div id="body">
        <div id="cap"></div>
        <div id="main">
        <h1>Setup Address Block</h1>
    <ajaxToolkit:ToolkitScriptManager ID="ScriptManager1" EnablePartialRendering="true" EnableScriptGlobalization="true" EnableScriptLocalization="true" runat="Server" />
            <asp:Panel ID="Panel1" runat="server" HorizontalAlign="Left">
                <table width="60%">
                <tr>
                    <td class="HeaderDiv" style="text-align: center">
                    Address Block </td>
                </tr>
                <tr>
                    <td>
                        <asp:DropDownList ID="DDLAccounts" runat="server" Width="232px" TabIndex="1" AutoPostBack="true" />            
                        <asp:Button ID="btnGO" runat="server" CssClass="button"  Text="View Address Block" />
                        <asp:HiddenField ID="hdnAccID" runat="server" />
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:UpdatePanel ID="UpdatePanel1" runat="server" >
        <ContentTemplate>
        <asp:Panel id="Pan1" runat="server" Visible="false">
        <asp:Label ID="lblResponse" runat="server" CssClass="Title" ForeColor="#C00000" Visible="false"></asp:Label><br />
        <table width="100%">
        <tr>
        <td class="alt1" colspan="4" >            
          <asp:Label ID="lblHeader" runat="server" ></asp:Label>
        </td>
        </tr>
        <tr>
      <td class="TableBlock" >
      <asp:CheckBox ID="chkFName" runat="server"/>      
      First Name</td>
      <td class="TableBlock">      
      <asp:CheckBox ID="chkMName" runat="server"/>      
      Middle Name</td>
      <td class="TableBlock">      
	<asp:CheckBox ID="chkLName" runat="server"/>      
      Last Name<b><font size="4">,</font></b></td>
      <td class="TableBlock" style="width: 140px">
    <asp:CheckBox ID="chkdgree" runat="server"/>      Degree</td>
    </tr>
    <tr>
      <td class="TableBlock" colspan="4" >      
      <asp:CheckBox ID="chkAdd" runat="server"/>      
      Address</td>
    </tr>
    <tr>
      <td class="TableBlock" >
      <asp:CheckBox ID="chkCity" runat="server"/>            
      City</td>
      <td class="TableBlock" colspan="2" >      
      <asp:CheckBox ID="chkState" runat="server"/>      
      State</td>
      <td class="TableBlock" style="width: 140px" >
      <asp:CheckBox ID="chkZip" runat="server"/>      
      Zip code</td>
    </tr>
    <!--tr>
      <td width="117%" colspan="4" height="21">
      <font face="Trebuchet MS" size="1">
      <INPUT TYPE=CHECKBOX NAME=Country>
      </font><font face="Trebuchet MS" size="2"> 
      Country</font></td>
    </tr-->
    <tr>
      <td class="TableBlock" colspan="4" >      
      <asp:CheckBox ID="chkPhone" runat="server"/>           
      Phone #</td>
      </tr>
      <tr>
      <td class="TableBlock" colspan="4" >      
      <asp:CheckBox ID="chkFax" runat="server"/>      
      Fax #</td>
    </tr>
    <!--tr>
      <td width="117%" colspan="4" height="21">
      <font face="Trebuchet MS" size="1">
      <INPUT TYPE=CHECKBOX NAME=email>
      </font><font face="Trebuchet MS" size="2"> 
      Email ID</font></td>
    </tr>
    <tr-->
            <tr>
      <td colspan="4"> 
               <p align="left"> 
                <asp:CheckBox ID="chkPopStyle" runat="server" Text="Populate values in single line." Font-Bold="true"/> 
               </p>
              <p align="center"> 
                  <asp:Button ID="btnSet" runat="server" Text="Save Settings" CssClass="button" /> 
               </p>                 
	</td>
            </tr>
  </table>            
  </asp:Panel>
        </ContentTemplate>
        <Triggers>
        <asp:AsyncPostBackTrigger  ControlID="btnGO" EventName="Click" />        
        <asp:AsyncPostBackTrigger  ControlID="DDLAccounts" EventName="SelectedIndexChanged" />        
        </Triggers>
        
        </asp:UpdatePanel>
                    </td>
                </tr>
            </table>
            <br />
            
            </asp:Panel>
             
    <br />
                  
    
                                      
        
            

    
        
        <ajaxToolkit:ListSearchExtender ID="ListSearchExtender2" runat="server"
                TargetControlID="DDLAccounts" PromptCssClass="ListSearchExtenderPrompt">
            </ajaxToolkit:ListSearchExtender>
            </div> 
            </div> 
    </form>
          
     
</body>
</html>
