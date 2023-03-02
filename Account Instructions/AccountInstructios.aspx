<%@ Page Language="VB" AutoEventWireup="false" CodeFile="AccountInstructios.aspx.vb" Inherits="AccountInstructions" %>
<%@ Register TagPrefix="DBWC" Namespace="DBauer.Web.UI.WebControls" Assembly="DBauer.Web.UI.WebControls.HierarGrid" %>
<%@ Register
    Assembly="AjaxControlToolkit"
    Namespace="AjaxControlToolkit"
    TagPrefix="ajaxToolkit" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 1.0 Transitional/EN">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Account Instructions</title>
    <link href= "../App_Themes/Css/styles.css" type="text/css" rel="stylesheet"/>
    <link href= "../App_Themes/Css/Common.css" type="text/css" rel="stylesheet"/>
    <script language="javascript" type="text/javascript">
    function btnOnClick()
    {        
//        var fso = new ActiveXObject( 'Scripting.FileSystemObject' );
//        if( fso.FileExists('C:\\Program Files\\ETS\\EditTemplate.exe') ) {
//            if(fso.GetFileVersion('C:\\Program Files\\ETS\\EditTemplate.exe')=='1.0.0.3') {
//                //alert(document.getElementById('DDLAccounts'));                     
                return true;
//            }else{
//                window.location='../TemplateEditor/Default.htm';
//                return false;
//            }           
//        
//        }else {        
//           window.location='../TemplateEditor/Default.htm';
//           //alert('False'); 
//           return false;
//        }
//        
    }
    function BackClicked()
    {
    document.all.oframe.close;
    }
function window_onunload() {
document.all.oframe.close;
}

    </script>    
    <script type="text/vbscript"  language="vbscript">   
Function DecryptText(strText,ByVal strPwd)
    Dim i , c 
    Dim strBuff 
    If strPwd <> "" And strText <> "" Then
        strPwd = UCase(strPwd)
        If Len(strPwd) Then
            For i = 1 To Len(strText)
                c = Asc(Mid(strText, i, 1))
                c = c - Asc(Mid(strPwd, (i Mod Len(strPwd)) + 1, 1))
                strBuff = strBuff & Chr(c And &HFF)
            Next
        Else
            strBuff = strText
        End If
        DecryptText = strBuff
    Else
        DecryptText = ""
    End If
End Function
Sub Load() 
 if not document.all.FileMgr.folderExists("C:\edictate\temp") then
    document.all.FileMgr.CreateFolder("C:\edictate\temp")
 end if
 document.all.oframe.open DecryptText(document.all.hdnReportPath.value,"webpath"), true
End Sub
sub AddNew()
document.all.oframe.showDialog(0)
end sub
sub OpenEx()
document.all.oframe.showDialog(1)
end sub
Sub SaveIt()
On Error Resume Next
    dim DocName,DocEx    
    DocName = Month(Now) & day(Now) & year(Now) & Hour(now) & Minute(Now) & Second(now) 
    if instr(1,document.all.oframe.ActiveDocument.name," ")>0 then   
        select case mid(document.all.oframe.ActiveDocument.name,1,instr(1,document.all.oframe.ActiveDocument.name," ")-1)
        case "Document"
            DocEx = ".doc"
        case "Worksheet"    
            DocEx= ".xls"
        case else
            DocEx= ".xls"       
        end select
    else
           DocEx= ".xls"      
    end if
    If Err.Number <> 0 then
        msgbox err.Description
        exit sub
    end if
    DocName=DocName & DocEx
    document.all.oframe.save "C:\edictate\temp\" & DocName , true
    document.all.oframe.close    
    
    document.all.SAXFile.AddFile "C:\edictate\temp\" & DocName , "File"	         
    document.all.SAXFile.AddFormElement "AccountID", document.all.hdnAccID.Value
    document.all.SAXFile.AddFormElement "DocType", DocEx
    document.all.SAXFile.CurrentURL = document.all.hdnURL.value &  "/Account Instructions/getInstructions.aspx"
    document.all.SAXFile.Start
    if document.all.SAXFile.response = "1" then
        document.all.FileMgr.DeleteFile "C:\edictate\temp\" & DocName
        MsgBox "Instructions has been set successfully!"
        'document.all.MultiView1.ActiveViewIndex = 0
        document.form1.submit()
    else
        MsgBox document.all.SAXFile.response           
    end if  
    Load() 
    on error goto 0
End sub
Sub SaveItC()
Call SaveIt()
end sub

</script>
</head>
<body onload="load()" language="javascript" onunload="return window_onunload()">
    <form id="form1" runat="server" EncType="Multipart/Form-Data" >
        <div id="body">
        <div id="cap"></div>
        <div id="main">
        <h1>Set Account Instructions</h1>
        <asp:Panel ID="Panel1" runat="server" HorizontalAlign="Left">
            <ajaxToolkit:ToolkitScriptManager ID="ScriptManager1" EnablePartialRendering="true" EnableScriptGlobalization="true" EnableScriptLocalization="true" runat="Server" />
    <%--<asp:UpdatePanel ID="pnlInstr" runat="server">
    <ContentTemplate>--%>
    <asp:MultiView ID="MultiView1" runat="server">
    <asp:View ID="vSearch" runat="server">
    <div>
    <p>  
    <br />
    <br />  
                  <asp:DropDownList ID="DDLAccounts" CssClass="common" runat="server" Width="264px" TabIndex="1" >                      
                  </asp:DropDownList>            
                  <asp:Button ID="btnGO" runat="server" Text="Search" CssClass="button" OnClientClick="return btnOnClick()" />        
    </p>    
    </div>  
    </asp:View>
    <asp:View ID="vDetails" runat="server">
        <div>
        <p></p>
        <br />
        <br /> 
        <table>
            <TR>
                <td align="center" class="HeaderDiv">
                    <asp:Label ID="lblAccName" CssClass="common" runat="server"></asp:Label>
                </td>                
            </tr>
            <tr id="TR" runat="server" visible="false">
            <td class="HeaderDiv"><asp:Label ID="lblResponse" runat="server" Visible="false"></asp:Label></td>
            </tr>           
            
        </table> 
        
        </div>      
        <table width="100%"> 
            <tr>
            <td class="alt1">File Format</td> 
            <td class="alt1">Date Modified</td>            
            <td class="alt1">Delete</td>            
            <td class="alt1">Action</td>
            </tr>        
            <tr> 
                <td>
                    <asp:Label ID="lblType" runat="server" CssClass="common" ></asp:label>                                                                 
                </td>            
            <td>
                <asp:Label ID="lblDateMod" runat="server" CssClass="common"></asp:label>                                                                 
            </td>            
            <td>
                <asp:Button ID="btndelete" runat="server" OnClick="btnDelete_Click" Font-Bold="true" CssClass="button" />
            </td>            
            <td><input id="Button1" onclick="SaveItC()" class="button" type="button" value="Save Instructions" />                                
            </td>
            </tr>
            <tr id="TRFile" runat="server"> 
                <td colspan="3" height="100%" style="text-align: left">
                <input id="btnAddNew" onclick="AddNew()" class="button" type="button" value="New Document" />
                
                <input id="btnOpenExisting" onclick="OpenEx()" class="button" type="button" value="Open Existing" />
                </td>
                <td height="100%" style="text-align: right">
                <asp:Button ID="btnBack" runat="server" CssClass="button" Width="100%" OnClientClick="BackClicked()" OnClick="btnBack_Click" Text="<< Back to Search" />
                </td> 
            </tr>                               
        </table>          
        <object id="oframe" classid="clsid:00989888-BB72-4e31-A7C6-5F819C24D2F7" height="100%"
                        width="100%">
                        <param name="BorderStyle" value="1" />
                        <param name="Titlebar" value="0" />
                        <param name="Menubar" value="0" />
                        <param name="_ExtentX" value="24844" />
                        <param name="_ExtentY" value="13547" />
                        <param name="BorderColor" value="-2147483632" />
                        <param name="BackColor" value="-2147483643" />
                        <param name="ForeColor" value="-2147483640" />
                        <param name="Toolbars" value="1" />
                    </object>       
    </asp:View>
     </asp:MultiView>                     
        <ajaxToolkit:ListSearchExtender ID="ListSearchExtender2" runat="server"
                TargetControlID="DDLAccounts" PromptCssClass="ListSearchExtenderPrompt">
        </ajaxToolkit:ListSearchExtender>
    <%--</ContentTemplate>
    </asp:UpdatePanel>--%>    
    <object classid="clsid:C3A57B60-C117-11D2-BD9B-00105A0A7E89" codebase="saxfile.cab" id="SAXFile">
	</object>
	<object classid="clsid:E7B62F4E-82F4-11D2-BD41-00105A0A7E89" 
        codebase="saxfile.cab" id="FileMgr">
    </object>	
    <MsgBox:msgBox id="MsgBox1" runat="server"></MsgBox:msgBox>
        <asp:HiddenField ID="hdnReportPath" runat="server" />
        <asp:HiddenField ID="hdnAccID" runat="server" />
        <asp:HiddenField ID="hdnURL" runat="server" />
        </asp:Panel>
    </div> 
    </div> 
    </form>
</body>
</html>
