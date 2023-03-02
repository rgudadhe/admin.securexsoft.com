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
    <title>Edit Document</title>
    <link href= "../App_Themes/Css/styles.css" type="text/css" rel="stylesheet"/>
    <link href= "../App_Themes/Css/Common.css" type="text/css" rel="stylesheet"/>
    <link href= "../App_Themes/Css/Routing.css" type="text/css" rel="stylesheet"/>

<script type="text/vbscript"  language="VBScript">
Function DecryptText(ByVal strText,ByVal strPwd)
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
    document.all.SAXFile.AddFormElement "SampleNo", document.all.hdnSampleNo.value    
    document.all.SAXFile.AddFormElement "PhyID", document.all.hdnPhyID.value    
    document.all.SAXFile.AddFormElement "PhyName", document.all.hdnPhyName.value        
    document.all.SAXFile.CurrentURL = document.all.hdnURL.value & "/Samples/SaveEditSample.aspx"
    document.all.SAXFile.Start

    dim response
    response=document.all.SAXFile.response
    response=CStr(Left(response,1))
    'response=CStr(trim(Mid(response,1,Instr(1,response,"<!DOCTYPE")-4)))
        
    if trim(response) = "1" then
        MsgBox "Sample has been set successfully!"
    else
        MsgBox document.all.SAXFile.response           
    end if
    window.navigate("EditSamples.aspx")    
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
        <h1>View/Edit Dictation Samples</h1>
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        <asp:Panel ID="Panel1" runat="server" HorizontalAlign="Left">
            <ajaxToolkit:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender1" runat="server"
            TargetControlID="txtKeyWords" WatermarkCssClass="watermark" WatermarkText="Please type Keywords for the Sample (Saperated by SPACE)">
        </ajaxToolkit:TextBoxWatermarkExtender>
        <table id="Table1" style="width: 100%;height: 100%; ">
            <tr>
                <td colspan="2" style="width: 942px; height: 42px; text-align: center">
                    &nbsp;<asp:TextBox ID="txtKeyWords" runat="server" Height="48px" Rows="10" Width="100%" TextMode="MultiLine"></asp:TextBox></td>
            </tr>
            <tr>
                <td colspan="2" style="height: 7px; text-align: center">
                    <input id="Button1" onclick="SaveItC()" class="button" type="button" value="Save Changes" language="javascript" />
                </td>
            </tr>
            <tr>
                <td colspan="2" style="width: 942px; height: 800px">
                    <object id="oframe" classid="clsid:00989888-BB72-4e31-A7C6-5F819C24D2F7" height="100%" style="width: 100%">
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
        <asp:HiddenField ID="hdnSampleNo" runat="server" />
        <asp:HiddenField ID="hdnPhyName" runat="server" />
        <asp:HiddenField ID="hdnPhyID" runat="server" />
        <asp:HiddenField ID="hdnURL" runat="server" />
        <br />
        <br />
        </asp:Panel>
        </div> 
        </div> 
    </form>
   </body>
</html>

