<%@ Page Language="VB" AutoEventWireup="false" CodeFile="Document.aspx.vb" Inherits="Transcription_document" EnableViewStateMac="false"  %>

<%@ Register Assembly="System.Web.Extensions, Version=1.0.61025.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI" TagPrefix="asp" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 1.0 Transitional/EN">

<%@ Register
    Assembly="AjaxControlToolkit"
    Namespace="AjaxControlToolkit"
    TagPrefix="ajaxToolkit" %>

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>Sample Document</title>
    <link href= "../App_Themes/Css/styles.css" type="text/css" rel="stylesheet"/>
    <link href= "../App_Themes/Css/Common.css" type="text/css" rel="stylesheet"/>
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
End Sub
</script> 

<script type="text/vbscript"  language="vbscript">


Sub SaveIt()
    dim DocName
    DocName = Month(Now) & day(Now) & year(Now) & Hour(now) & Minute(Now) & Second(now)
    document.all.oframe.save "C:\edictate\temp\" & DocName & ".doc", true
    document.all.oframe.close
    document.all.SAXFile.AddFile "C:\edictate\temp\" & DocName & ".doc", "File"	     
    document.all.SAXFile.AddFormElement "KeyWords", document.all.txtKeyWords.value
    document.all.SAXFile.AddFormElement "TransID", document.all.hdnTransID.value
    document.all.SAXFile.AddFormElement "SampleName", document.all.txtSampleName.value
    document.all.SAXFile.CurrentURL = document.all.hdnURL.value & "/Samples/SaveSample.aspx"
    document.all.SAXFile.Start
    if document.all.SAXFile.response = "1" then
        MsgBox "Sample has been set successfully!"
    else
        MsgBox document.all.SAXFile.response           
    end if
    
    window.navigate("SetSamples.aspx") 
End sub

Sub SaveItC()
Call SaveIt()
end sub
</script>
   
<script language="javascript" type="text/javascript">
<!--

function Button1_onclick() {

}

// -->
</script>
</head>
<body  onload="load()" >

    <form id="form1" runat="server">
        <div id="body">
        <div id="cap"></div>
        <div id="main">
        <h1>Set Dictation Samples</h1>
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        <ajaxToolkit:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender1" runat="server"
            TargetControlID="txtKeyWords" WatermarkCssClass="watermark" WatermarkText="Please type Keywords for the Sample (Separated by SPACE)">
        </ajaxToolkit:TextBoxWatermarkExtender>
        <table id="Table1" style="width: 100%; height: 100%;">
            <tr>
                <td style="width: 942px; height: 25px;">
                    &nbsp;<asp:TextBox ID="txtKeyWords" CssClass="common" runat="server" Height="24px" Rows="10" Width="480px" TextMode="MultiLine"></asp:TextBox></td>
                <td>
                    Sample Name <BR>
                    <asp:TextBox ID="txtSampleName" CssClass="common" runat="server" Width="230px"></asp:TextBox>    
                </td>
                                   
            </tr>
            <tr>
                <td style="width: 50%; height: 25px; text-align: center">
                    <input id="Button1" onclick="SaveItC()" class="button" type="button" value="Set as a Sample" language="javascript" onclick="return Button1_onclick()" />
                </td>
                <td style="width: 50%; height: 25px; text-align: center">
                    <asp:Button ID="btnDecline" runat="server" CssClass="button" Text="Decline as improper" Width="100%" />
                </td>
            </tr>
            <tr>
                <td colspan="2" style="width: 942px; height: 800px">
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
                </td>
            </tr>
        </table>
        <object id="saxfile" classid="clsid:C3A57B60-C117-11D2-BD9B-00105A0A7E89">
        </object>
        &nbsp;<asp:HiddenField ID="hdnReportPath" runat="server" />
        <asp:HiddenField ID="hdnTransID" runat="server" />
        <asp:HiddenField ID="hdnURL" runat="server" />
        <br />
        </div> 
        </div> 
    </form>
</body>
</html>

