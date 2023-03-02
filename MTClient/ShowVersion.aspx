<%@ Page Language="VB" AutoEventWireup="false" CodeFile="ShowVersion.aspx.vb" Inherits="Transcription_document" EnableViewStateMac="false"  %>

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
'MsgBox DecryptText(document.all.hdnReportPath.value,"webpath")
'window.moveTo(0,0)
'window.resizeTo(screen.availWidth, screen.availHeight)

if document.all.hdnTransID.value="Yes" then
    document.all.oframe.open DecryptText(document.all.hdnReportPath.value,"webpath"), true,"Word.Document"

    Set oDoc = document.all.oframe.ActiveDocument    
                For Each Var In oDoc.Variables
                    Var.value = Var.Name
                Next
                For Each field In oDoc.Fields
                    field.update
                Next
                For Each r In oDoc.StoryRanges 
                    r.Fields.Update
               Next             
               oDoc.ActiveWindow.View.Zoom.Percentage = 100
               'oDoc.Protect 2, , ""            
else
    msgbox ("No transcription found")
    CloseIT()
    window.close
end if               
End Sub
sub CloseIT()
document.all.oframe.close
end sub

Sub SaveItC()
call CloseIT()
window.close
end sub
</script>
   
</head>
<body  onload="load()" >

    <form id="form1" runat="server">  

        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        &nbsp;
 
    
    <table border="2" cellpadding="2" cellspacing="2"  id="AutoNumber1" style="width:100%;height:100%; background-color: whitesmoke;">
  <tr >
        <td style="height:25px; text-align: center;" colspan=2 width="100%">
  
            <input style="FONT-WEIGHT: bold; FONT-SIZE: 12px; VERTICAL-ALIGN: middle; COLOR: dimgray; FONT-STYLE: italic; TEXT-ALIGN: center; width: 100%; font-family: Verdana;" id="Button6"  onclick="SaveItC()" type="button" value="Close Window" /></td>             
        
  </tr>
  <tr>
    <td style="height:800px" colspan="2" width="100%">
       
   <object  classid="clsid:00460182-9E5E-11D5-B7C8-B8269041DD57"  id="oframe"   height="100%" width="100%">
         <param name="BorderStyle" value="1"/>
         <param name="Titlebar" value="0"/>
         <param name="Menubar" value="0"/>
         <param name="_ExtentX" value="24844"/>
         <param name="_ExtentY" value="13547"/>
         <param name="BorderColor" value="-2147483632"/>
         <param name="BackColor" value="-2147483643"/>
         <param name="ForeColor" value="-2147483640"/>
         <param name="Titlebar" value="1"/>
		 <param name="Toolbars" value="1"/>
       </object></td>
  </tr>
</table>




 <object classid="clsid:C3A57B60-C117-11D2-BD9B-00105A0A7E89" id="SAXFile">
	</object>


      &nbsp;<asp:HiddenField ID="hdnReportPath" runat="server" />
        <asp:HiddenField ID="hdnTransID" runat="server" />
        <br />
        
    </form>
   
</body>
</html>

