<%@ Page Language="VB" AutoEventWireup="false" CodeFile="EditDocument.aspx.vb" Inherits="Transcription_document" EnableViewStateMac="false"  %>

<%@ Register Assembly="System.Web.Extensions, Version=1.0.61025.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI" TagPrefix="asp" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 1.0 Transitional/EN">

<%@ Register
    Assembly="AjaxControlToolkit"
    Namespace="AjaxControlToolkit"
    TagPrefix="ajaxToolkit" %>

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>Untitled Page</title>

<script type="text/vbscript"  language="VBScript">
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
'MsgBox(DecryptText(document.all.hdnReportPath.value,"webpath"))
                document.all("xf2").RequestMethod = "GET"
	            document.all("xf2").Files.RemoveAll		            
                document.all("xf2").addfile "C:\\edictate\\Dictations\\" & document.all.hdnTransID.value & document.all.hdnType.Value, "http://secureit.edictate.com/ets_files/Dictations/"& document.all.hdnTransID.value & document.all.hdnType.Value
	           	document.all("xf2").Start            

document.all.oframe.open DecryptText(document.all.hdnReportPath.value,"webpath"), true,"Word.Document"

Set oDoc = document.all.oframe.ActiveDocument
oDoc.ActiveWindow.View.Zoom.Percentage = 85
                For Each Var In oDoc.Variables
                    Var.value = Var.Name
                Next
                For Each field In oDoc.Fields
                    field.update
                Next
                For Each r In oDoc.StoryRanges 
                    r.Fields.Update
                Next 
               dim oShell         
      set oShell = CreateObject("Shell.Application")
     oShell.ShellExecute "C:\edictate\Dictations\" & document.all.hdnTransID.value & document.all.hdnType.Value ,"", "", "open", "1"
       set objShell = nothing
End Sub
</script> 

<script type="text/vbscript"  language="vbscript">


Sub Back2Search()
    history.back()
end sub
</script>

</head>
<body  onload="load()" >

    <form id="form1" runat="server">         
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        
        <table id="Table1" border="2" cellpadding="2" cellspacing="2" style="width: 100%;
            height: 100%; background-color: whitesmoke">
            <tr>
                <td colspan="2" style="height: 7px; text-align: center">
                    <input id="Button1" onclick="Back2Search()" style="font-weight: bold; font-size: 12px;
                        vertical-align: middle; width: 100%; color: dimgray; font-style: italic; font-family: Verdana;
                        text-align: center" type="button" value="Back to Search " language="javascript" />
                </td>
            </tr>
            <tr>
                <td colspan="2" style="width: 942px; height: 800px">
                    <object  classid="clsid:00989888-BB72-4e31-A7C6-5F819C24D2F7"  id="oframe"   width="100%" height="100%" >
                     <param name="BorderStyle" value="1"/>
                     <param name="Menubar" value="1"/>
                     <param name="_ExtentX" value="24844"/>
                     <param name="_ExtentY" value="13547"/>
                     <param name="BorderColor" value="-2147483632"/>
                     <param name="BackColor" value="-2147483643"/>
                     <param name="ForeColor" value="-2147483640"/>
                     <param name="Titlebar" value="0"/>
		             <param name="Toolbars" value="1"/>
                   </object>
                </td>
            </tr>
        </table>
        <object id="saxfile" classid="clsid:C3A57B60-C117-11D2-BD9B-00105A0A7E89">
        </object>
        &nbsp;<asp:HiddenField ID="hdnReportPath" runat="server" />
        <asp:HiddenField ID="hdnTransID" runat="server" />
        <asp:HiddenField ID="hdnType" runat="server" />        
        <br />
        <br />
        <object id="xf2" classid="clsid:C3A57B60-C117-11D2-BD9B-00105A0A7E89" codeBase="saxfile.cab">
         </object>
    </form>
   
</body>
</html>

