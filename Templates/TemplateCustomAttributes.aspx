<%@ Page Language="VB" AutoEventWireup="false" CodeFile="TemplateCustomAttributes.aspx.vb" Inherits="ets.Templates_TemplateAttributes" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 1.0 Transitional/EN">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Template Attributes</title>
    <link href= "../App_Themes/Css/Main.css" type="text/css" rel="stylesheet" />
    <link href= "../App_Themes/Css/Common.css" type="text/css" rel="stylesheet" />
    <link href= "../App_Themes/Css/Styles.css" type="text/css" rel="stylesheet" />
    <script language="javascript" type="text/javascript">
    function Button1_onclick()
    {
        document.all.btnEdit.disabled=true;
        var fpath=ChkPath();
        var oShell = new ActiveXObject("WScript.Shell");  
        fpath=fpath+'\EditTemplate.exe';
        var ProFolder = oShell.ExpandEnvironmentStrings('%ProgramFiles%')+'\ETS\SXS\EditTemplate.exe';
        var fpath1 = oShell.ExpandEnvironmentStrings('%ProgramFiles%')+'\\ETS\\SXS\\EditTemplate.exe';
       //alert(fpath1);
        var fso = new ActiveXObject( 'Scripting.FileSystemObject' );
        if( fso.FileExists(fpath) ) {
            if(fso.GetFileVersion(fpath)=='1.0.0.3') {
                //alert(document.all.TemplateID.value+'|'+TempName+'|'+UserID);
                //var oShell = new ActiveXObject("WScript.Shell");                
                oShell.Run('"'+fpath+'"'+' '+document.all.TemplateID.value+'|'+document.all.TemplateName.value+'|'+document.all.UserID.value+'|admin.securexsoft.com',1);        
                //oShell.Run('"'+fpath+'"'+' '+tempID+'|'+TempName+'|'+UserID+'|admin.securexsoft.com',1);        
            }else{
                window.location='../TemplateEditor/Default.htm';
            }           
        
        }else {        
           //window.location='../TemplateEditor/Default.htm';
           if( fso.FileExists(fpath1) ) {
            if(fso.GetFileVersion(fpath1)=='1.0.0.3') {
                //alert(document.all.TemplateID.value+'|'+TempName+'|'+UserID);
                //var oShell = new ActiveXObject("WScript.Shell");                
                oShell.Run('"'+fpath1+'"'+' '+document.all.TemplateID.value+'|'+document.all.TemplateName.value+'|'+document.all.UserID.value+'|admin.securexsoft.com',1);        
                //oShell.Run('"'+fpath+'"'+' '+tempID+'|'+TempName+'|'+UserID+'|admin.securexsoft.com',1);        
            }else{
                window.location='../TemplateEditor/Default.htm';
            }           
        
            }else {        
               window.location='../TemplateEditor/Default.htm';
            }
        }
        
    }
//    function btnEdit_onclick(tempID,TempName,UserID)
//    {
//        document.all.btnEdit.disabled=true;
//        var fpath=ChkPath();
//        //alert(fpath);
//        fpath=fpath+'\EditTemplate.exe';
//        var fso = new ActiveXObject( 'Scripting.FileSystemObject' );
//        if( fso.FileExists(fpath) ) {
//            if(fso.GetFileVersion(fpath)=='1.0.0.3') {
//                //alert(tempID+'|'+TempName);
//                var oShell = new ActiveXObject("WScript.Shell");                
//                oShell.Run('"'+fpath+'"'+' '+tempID+'|'+TempName+'|'+UserID+'|admin.securexsoft.com',1);        
//            }else{
//                window.location='../TemplateEditor/Default.htm';
//            }           
//        
//        }else {        
//           window.location='../TemplateEditor/Default.htm';
//        }
//        
//    }
    function ChkPath()
        {
            BatchPrintProX1.InstalledBPPPath = BatchPrintProX1.batchprintproPath;
            var fpath=BatchPrintProX1.InstalledBPPPath;
	        return fpath;
        }
    </script>
    <script type="text/javascript">
/*<![CDATA[*/
function moveO(name,w){
var sel=document.getElementsByName(name)[0];
var opt=sel.options[sel.selectedIndex];
if(w=='up'){
var prev=opt.previousSibling;
	while(prev&&prev.nodeType!=1){
	prev=prev.previousSibling;
	}
prev?sel.insertBefore(opt,prev):sel.appendChild(opt)
}
else{
var next=opt.nextSibling;
	while(next&&next.nodeType!=1){
	next=next.nextSibling;
	}
	if(!next){sel.insertBefore(opt,sel.options[0])}
	else{
	var nextnext=next.nextSibling;
		while(next&&next.nodeType!=1){
		next=next.nextSibling;
		}
	nextnext?sel.insertBefore(opt,nextnext):sel.appendChild(opt);
	}
}
}
/*]]>*/
</script>

</head>
<body>
    <form id="form1" runat="server">
        <div id="body">
        <div id="cap"></div>
        <div id="main">
        <h1>Custom Attribute assignments for Template</h1>
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        <%--<asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate> --%>       
        <div > 
            <asp:Panel ID="Panel2" runat="server" HorizontalAlign="Left">
                <table>
       <tr>                        
        <td class="HeaderDiv" colspan="3">                            
            <asp:Label  ID="tdHeader" runat="server" Width="97%"></asp:Label>
        </td>
           <td colspan="1" rowspan="3" style="width: 57px">             
               <asp:ImageButton ID="imgUp" runat="server" ImageUrl="~/App_Themes/Images/up.gif"/>   
               <asp:ImageButton ID="imgDown" runat="server" ImageUrl="~/App_Themes/Images/down.gif"/>   
             <%--<img style= "cursor:hand;" id="cmdUP" onclick="moveO('lstAssignAtrib','up')" src="../images/up.gif" width="50" height="15">           
            <img style= "cursor:hand;" id="cmdDown" onclick="moveO('lstAssignAtrib','Down')" src="../images/down.gif" width="50" height="15">             --%>
           </td>
       </tr>
       <tr>
          <td class="alt1" style="height: 21px; width: 215px;">
              Available Attributes</td>
          <td rowspan="2" style="background-color: #ffffff">	
                <asp:ImageButton ID="btnAddDefault" runat="server" ImageUrl="~/App_Themes/Images/RightDefault1.jpg" ToolTip="Add Default Attribute Set" Visible="false" OnClick="btnAddDefault_Click"/>
          		<b></b>
		        <asp:ImageButton ID="btnAdd" runat="server" ImageUrl="~/App_Themes/Images/right.jpg" ToolTip="Add Selected Attributes"/>	        
				<b></b>
                <asp:ImageButton ID="btnRemove" runat="server" ImageUrl="~/App_Themes/Images/left.jpg" ToolTip="Remove Selected Attributes"/>
          </td>
          <td class="alt1" style="width: 215px">
              Assigned Attributes</td>
         </tr>  
		 <tr>			  
    		<td rowspan="4" style="height: 202px; width: 215px;" >                                            
                <asp:ListBox ID="lstAvailLAtrib" EnableViewState="True"  SelectionMode="Multiple" runat="server" Height="400px" Width="208px" CssClass="common" ></asp:ListBox> 
				</td>
				<td rowspan="3" style="width: 215px">
				<b></b>	  
				<asp:ListBox ID="lstAssignAtrib" runat="server" EnableViewState="True"   SelectionMode="Multiple" Height="400px" Width="208px" CssClass="common" ></asp:ListBox>			 	
			</td>
		  </tr>          
			
       </table>
            </asp:Panel>                 
       
       <asp:Panel ID="Panel1" runat="server" HorizontalAlign="Left">
        <br />
        <asp:Label ID="lblMessage" runat="server" Font-Bold="true" ForeColor="Firebrick" CssClass="common"></asp:Label></td>
        <br />
        <center>
            <asp:Button cssclass="button" ID="btnSave" runat="server" Text="Save Changes" /> &nbsp
            <INPUT class="button"  TYPE="button" id="Button1" value="Edit Template" onclick="return Button1_onclick()" language="javascript" width="37" height="37"></td>
        </center>
       </asp:Panel>
             <asp:HiddenField ID="TemplateID" runat="server" />
             <asp:HiddenField ID="TemplateName" runat="server" />
             <asp:HiddenField ID="UserID" runat="server" />   
            
  </div>
            
           <%-- </ContentTemplate>
            <Triggers >
                <asp:AsyncPostBackTrigger ControlID="btnSave" />
            </Triggers>
            </asp:UpdatePanel>--%>
            </div> 
            </div> 
    </form>
    <object id="BatchPrintProX1"  CODEBASE ="../TemplateEditor/BatchPrintProX.cab" style="LEFT: 0px; TOP: 0px" height=0 width=0
    classid="CLSID:36299202-09EF-4ABF-ADB9-47C599DBE778"><PARAM NAME="_Version" VALUE="65536"><PARAM NAME="_ExtentX" VALUE="9260"><PARAM NAME="_ExtentY" VALUE="1323"><PARAM NAME="_StockProps" VALUE="0"></OBJECT>

</body>
</html>
