<%@ Page Language="VB" AutoEventWireup="false" CodeFile="TemplateSearch.aspx.vb" Inherits="ets.Templates_TemplateSearh" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 1.0 Transitional/EN">
<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
<title>Template Search</title>
<link href= "../App_Themes/Css/Main.css" type="text/css" rel="stylesheet" />
<link href= "../App_Themes/Css/Styles.css" type="text/css" rel="stylesheet" />
<script language="javascript" type="text/javascript">
    function btnEdit_onclick(tempID,TempName,UserID)
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
                //alert(tempID+'|'+TempName);
                              
                oShell.Run('"'+fpath+'"'+' '+tempID+'|'+TempName+'|'+UserID+'|admin.securexsoft.com',1);        
            }else{
                window.location='../TemplateEditor/Default.htm';
            }           
        
        }else {  
           //fpath=oShell.ExpandEnvironmentStrings('%ProgramFiles%')+'\ETS\SXS\EditTemplate.exe';      
           
           if( fso.FileExists(fpath1) ) {
            if(fso.GetFileVersion(fpath1)=='1.0.0.3') {
                //alert(tempID+'|'+TempName);
                //alert('"'+fpath1+'"'+' '+tempID+'|'+TempName+'|'+UserID+'|admin.securexsoft.com')
                              
                oShell.Run('"'+fpath1+'"'+' '+tempID+'|'+TempName+'|'+UserID+'|admin.securexsoft.com',1);        
            }else{
                window.location='../TemplateEditor/Default.htm';
            }           
        
        }else {  
           //fpath=oShell.ExpandEnvironmentStrings('%ProgramFiles%')+'\ETS\SXS\EditTemplate.exe';      
           window.location='../TemplateEditor/Default.htm';
        }
        }
        
    }
    function ChkPath()
        {
            BatchPrintProX1.InstalledBPPPath = BatchPrintProX1.batchprintproPath;
            var fpath=BatchPrintProX1.InstalledBPPPath;
	        return fpath;
        }
    </script>
    <script language="vbscript" type="text/javascript" >
    </script>  
</head>
<body>
    <form id="form1" runat="server">
    <div id="body">
    <div id="cap"></div>
    <div id="main">
    <h1>Edit Template</h1>
    <ajaxToolkit:ToolkitScriptManager runat="server" ID="ScriptManager1" />
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
        <asp:Panel ID="Panel1" runat="server" HorizontalAlign="Left">
            <table>
            <tr>
                <td colspan="2" class="HeaderDiv" >
                    Template Search</td>
            </tr>
            <tr>
                <td >
                    Template Name
                </td>
                <td >
                    <asp:TextBox ID="txtTemplateName" runat="server" Width="267px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td colspan="2" style="text-align:center" >
                    <asp:Button ID="btnSearch" CssClass="button"  runat="server" Text="Search Template" />
                </td>
            </tr>
            
            </table>
            <asp:RegularExpressionValidator Display="None"  
    id="RegtxtTemplateName"  
    runat="server" 
    ControlToValidate="txtTemplateName" 
    ValidationExpression="^[0-9a-zA-Z- %]+$"
    ErrorMessage="Template Name - Please enter valid input."
   />
        <asp:ValidationSummary ID="ValidationSummary1" runat="server" HeaderText="Following error occurs:" ShowMessageBox="true" DisplayMode="BulletList" ShowSummary="False" />      
   

        </asp:Panel>
        <asp:Panel ID="Panel2" runat="server" HorizontalAlign="Left">
            <asp:Repeater ID="rptTemp" runat="server">
                     <HeaderTemplate>
                        <table>
                        <tr>            
                            <td class="alt1">Template Name</td>
                            <td class="alt1">Template Type</td>
                            <td class="alt1">Action</td>           
                        </tr>
                        </HeaderTemplate>

            <ItemTemplate>
            <tr bgcolor="#cccccc">         
                        <td><asp:TextBox ID="txtName" runat="server" Text='<%#Container.DataItem("TemplateName")%>' Enabled=false></asp:TextBox><asp:Button ID="iPopUp1" runat="server" Text="..." OnClick="iPopUp1_Click" ToolTip="Click here to edit Template Name" />
                        <asp:HiddenField ID="TemplateID" runat="server" Value='<%#Container.DataItem("TemplateID")%>'/></td>
                        <td width="250px">
                        <asp:Label ID="lblType" runat="server" Width="90%" Text='<%#String.Concat(Container.DataItem("TypeDesc")," {<b> " , Container.DataItem("TemplateType"), "</B> }")%>' ></asp:label>                         
                        <asp:TextBox ID="txttypeDesc" runat="server" Text='<%#Container.DataItem("TypeDesc")%>' Visible="false"></asp:TextBox>            
                        <asp:TextBox ID="txtType" runat="server" Text='<%#Container.DataItem("TemplateType")%>' Visible="false"  Width="40"></asp:TextBox>
                        <asp:Button ID="iPopUp" runat="server" Text="..." OnClick="iPopUp_Click" ToolTip="Click here to change Template Type" />            
                        </td>
                        <td>
                        
                        <cc0:eximagebutton Enabled="false" id="Button1" ToolTip= "Save Changes"  runat="server" DisableImageURL="../App_Themes/Images/i_saveP.gif" Text="Save Changes" ImageUrl="../App_Themes/Images/i_save.gif" onclick="Button1_Click"></cc0:eximagebutton>            
                        <cc0:eximagebutton id="btnDelete"  ToolTip="Delete Template" runat="server" DisableImageURL="../App_Themes/Images/i_deleteP.gif" Text="Delete Template" ImageUrl="../App_Themes/Images/i_delete.gif" onclick="btnDelete_Click" ></cc0:eximagebutton>            
                        <INPUT TYPE="image" id="btnEdit" SRC="../App_Themes/Images/i_filter.gif" title="Edit Template" onclick="return btnEdit_onclick('<%#Container.DataItem("TemplateID")%>','<%#Container.DataItem("TemplateName")%>','<%#Session("UserID")%>')" language="javascript" width="37" height="37">
                        
                        </td>         
            </tr>
            </ItemTemplate>
            <FooterTemplate>
            </table>
            </FooterTemplate>
            </asp:Repeater>
        </asp:Panel>
        
    
                    <asp:Literal ID="iResponse" runat="server" ></asp:Literal>
                    
        </ContentTemplate>        
        </asp:UpdatePanel>
        </div> 
        </div> 
    </form>
    <object id="BatchPrintProX1"  CODEBASE ="../TemplateEditor/BatchPrintProX.cab" style="LEFT: 0px; TOP: 0px" height=0 width=0
    classid="CLSID:36299202-09EF-4ABF-ADB9-47C599DBE778"><PARAM NAME="_Version" VALUE="65536"><PARAM NAME="_ExtentX" VALUE="9260"><PARAM NAME="_ExtentY" VALUE="1323"><PARAM NAME="_StockProps" VALUE="0"></OBJECT>
</body>
</html>
