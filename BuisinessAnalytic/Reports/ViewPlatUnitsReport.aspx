<%@ Page Language="VB"  AutoEventWireup="false" CodeFile="ViewPlatUnitsReport.aspx.vb" Inherits="ViewPlatUnitsReport" %>

<%@ Register Assembly="System.Web.Extensions, Version=1.0.61025.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI" TagPrefix="asp" %>
<%@ Register Assembly="KMobile.Web" Namespace="KMobile.Web.UI.WebControls" TagPrefix="asp" %>
<%@ Register
    Assembly="AjaxControlToolkit"
    Namespace="AjaxControlToolkit"
    TagPrefix="ajaxToolkit" %>
   

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
<link href= "../../App_Themes/Css/styles.css" type="text/css" rel="stylesheet"/>
<link href= "../../App_Themes/Css/Common.css" type="text/css" rel="stylesheet"/>
 	

<script language="JavaScript" type="text/javascript">
function confirmdelete()
{
return confirm("Do you want to delete records?");

}
function changeAll() {
		if (document.form1.ChkAll.checked) {
			elval = true;
		} else
		 {
			elval = false;
			}
		for (var i=0;i<document.form1.elements.length;i++)
			{
			    //alert(document.form1.elements[i].name.substring(0,5)); 
			    if (document.form1.elements[i].name == 'DemoRec')
			       { 
			            
				        document.form1.elements[i].checked = elval;        
		        	    highlightRow(document.form1.elements[i]);
				    }
			}
}

function highlightRow(InputNode) {
	//alert(InputNode);
     var el = InputNode;
     while (el.nodeName.toLowerCase() != 'tr')
           el = el.parentNode;
           //alert(e1.parentnode);
 //    el.style.backgroundColor = (InputNode.checked) ? '#eee8aa' : '#d7dbdd';
    if(InputNode.checked)
     {
     el.style.backgroundColor='#eee8aa';
     ChVal = true;
     for (var i=0;i<document.form1.elements.length;i++)
			{
			//alert(document.form1.elements[i].value);
				if (document.form1.elements[i].name == 'DemoRec'  && document.form1.elements[i].checked==false)
				{
				
				ChVal = false;
				//alert(ChVal);
				}
				}
	 }
     else
     {
     el.style.backgroundColor='#d7dbdd';
      ChVal = false;
         for (var i=0;i<document.form1.elements.length;i++)
			{
			//alert(document.form1.elements[i].value);
				if (document.form1.elements[i].name== 'DemoRec' && document.form1.elements[i].checked == false)
				{
				
				ChVal = false;
				//alert(ChVal);
				}
				}

   }
   //alert(ChVal);   
	document.form1.ChkAll.checked=ChVal; 

}
</script>
    <title>View VAS Report</title>
</head>
<body style="text-align: center">
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        <div id="body">
        <div id="cap"></div>
        <div id="main">
        <h1>View Production Units </h1>
        <%--<asp:Panel ID="Panel3" runat="server" HorizontalAlign="Left">--%>
         <%--   <asp:Panel ID="Panel2" runat="server" width="100%">--%>
           <table width="500">
             <tr>
                <td colspan="3" style="text-align: center" class="HeaderDiv" >
                    Search 
                    <asp:ImageButton ID="Image1" runat="server" ImageUrl="~/App_Themes/Images/expand_blue.jpg" AlternateText="(Show Details...)"/>
                </td>
            </tr>
          </table> 
         <%-- </asp:Panel> 
          <asp:Panel ID="Panel1" runat="server" width="100%">--%>
           <table width="500">
                <tr >
                 <td style="text-align: center" width="30%" class="alt1">
                    Start Date
                 </td>
                <td style="text-align: center" width="30%" class="alt1">
                    End Date
                </td>
               <td style="text-align: center" width="30%" class="alt1">
                   Platform
                </td> 
           </tr>
            <tr >
                 <td style="text-align: center" width="30%" >
                  
                     <asp:TextBox ID="TxtSDate" runat="server" ></asp:TextBox>
                     <asp:ImageButton ID="imgSDate" CausesValidation="False" ImageUrl="~/images/Calendar.gif" runat="server"/> 
                                            <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" Format="MM/dd/yyyy" TargetControlID="TXTSDate" PopupButtonID="imgSDate" BehaviorID="CalendarExtender1" Enabled="True">
                                              </ajaxToolkit:CalendarExtender>
                 </td>
                <td style="text-align: center" width="30%">
                    
                       <asp:TextBox ID="TxtEDate" runat="server" ></asp:TextBox>
                     <asp:ImageButton ID="imgEDate" CausesValidation="False" ImageUrl="~/images/Calendar.gif" runat="server"/> 
                                            <ajaxToolkit:CalendarExtender ID="CalendarExtender2" runat="server" Format="MM/dd/yyyy" TargetControlID="TXTEDate" PopupButtonID="imgEDate" BehaviorID="CalendarExtender2s" Enabled="True">
                                              </ajaxToolkit:CalendarExtender>
                </td>
               <td style="text-align: center" width="30%">
                    <asp:DropDownList ID="DLAct" runat="server" CssClass="common">
                    </asp:DropDownList>
                </td> 
           </tr>
           <tr>
                <td style="text-align: center;" colspan="2" >
                    <asp:Button ID="btnSubmit" cssClass="button" runat="server" Text="Submit" />&nbsp;
                </td>
                  <td style="text-align: center;"  >
                    <asp:Button ID="btnDelete" cssClass="button" runat="server" Text="Remove" OnClientClick="return confirmdelete();"  />&nbsp;
                </td>
            </tr>
        </table>
   <%-- </asp:Panel>
     
     <ajaxToolkit:CollapsiblePanelExtender ID="cpeDemo" runat="Server"
        TargetControlID="Panel1"
        ExpandControlID="Panel2"
        CollapseControlID="Panel2" 
        Collapsed="False"
        TextLabelID="Label1"
        ImageControlID="Image1"    
        ExpandedText="(Hide Details...)"
        CollapsedText="(Show Details...)"
        ExpandedImage="~/App_Themes/Images/collapse_blue.jpg"
        CollapsedImage="~/App_Themes/Images/expand_blue.jpg"
        SuppressPostBack="true"
        /> --%>
        
        
         <br />
            <br />
             <asp:Table ID="Table1" Width="90%" runat="server" CssClass="common" >
             <asp:TableHeaderRow>
             <asp:TableHeaderCell ID="ChkCell"><input type="checkbox" name="ChkAll" onclick="changeAll();" /></asp:TableHeaderCell>
             <asp:TableHeaderCell ID="TableHeaderCell7" Text="UserName" ></asp:TableHeaderCell>
             <asp:TableHeaderCell ID="TableHeaderCell1" Text="JobNumber" ></asp:TableHeaderCell>
             <asp:TableHeaderCell ID="TableHeaderCell2" Text="Level"></asp:TableHeaderCell>
             <asp:TableHeaderCell ID="TableHeaderCell3" Text="Units"></asp:TableHeaderCell>
             <asp:TableHeaderCell ID="TableHeaderCell4" Text="Accounts"></asp:TableHeaderCell>
             <asp:TableHeaderCell ID="TableHeaderCell5" Text="Dictator"></asp:TableHeaderCell>
             <asp:TableHeaderCell ID="TableHeaderCell6" Text="Post Date"></asp:TableHeaderCell>
             </asp:TableHeaderRow>
        </asp:Table>
        <br />
        <asp:Button ID="Button1" runat="server" Text="Remove Records" CssClass="button"  />
           </div> 
           </div> 
    </form>
</body>
</html>
