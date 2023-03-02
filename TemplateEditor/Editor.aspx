<%@ Page Language="VB" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 1.0 Transitional/EN">

<script runat="server">

    Protected Sub Button1_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        
        
        
    End Sub
</script>

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Untitled Page</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <!-- web page script -->

        <script language="VBScript">
 Dim bDocOpen

 Sub oframe_OnDocumentOpened(str, obj)
    Dim s, s2
    On Error Resume Next
    bDocOpen = True

    if len(str) = 0 then
      str = "New Document"
    else
      Dim x
      x = InStr(str, "\")
      if x then
         do
            str = mid(str, x+1)
            x = Instr(str, "\")
         loop while x > 0
      else
         x = Instr(str, "/")
         if x Then
           do
              str = mid(str, x+1)
              x = Instr(str, "/")
           loop while x > 0
         end if
      end if
    end if

    s = obj.Application.Name
    if len(s) = 0 then s = "Unknown Server"

    document.all.tstat.InnerHTML = "File: " & str & "<br>Server: " & s
 End Sub

 Sub oframe_OnDocumentClosed()
   bDocOpen = False
   document.all.tstat.InnerHTML = "Try another file."
 End Sub

 Sub NewDoc()
   On Error Resume Next
   oframe.showdialog 0 'dsoDialogNew
   if err.number then
      MsgBox "Unable to Create New Object: " & err.description
   end if
 End Sub

 Sub OpenDoc()
   On Error Resume Next
   document.all.oframe.showdialog 1 'dsoDialogOpen
    if err.number then
      MsgBox "Unable to Open Document: " & err.description
   end if
 End Sub

 Sub OpenWebDoc()
   Dim sUrl
   On Error Resume Next
   sUrl = InputBox("Type the URL to a file on a Web Folder...", ,"http://[server]/[folder]/test.doc")
   If Len(sUrl) Then
     oframe.open sUrl, true
     if err.number then
       MsgBox "Unable to Open URL: " & err.description
     end if
   End If
 End Sub

 Sub SaveCopyDoc()
   On Error Resume Next
   If Not bDocOpen Then
      MsgBox "You do not have a document open."
   Else
      oframe.showdialog 3 'dsoDialogSaveCopy
   End If
 End Sub

 Sub ChgLayout()
   On Error Resume Next
   If Not bDocOpen Then
      MsgBox "You do not have a document open."
   Else
      oframe.showdialog 5 'dsoDialogPageSetup
   End If
 End Sub

 Sub PrintDoc()
   On Error Resume Next
   If Not bDocOpen Then
      MsgBox "You do not have a document open."
   Else
      oframe.printout True
   End If
 End Sub

 Sub CloseDoc()
   On Error Resume Next
   If Not bDocOpen Then
      MsgBox "You do not have a document open."
   Else
      oframe.close
   End If
 End Sub

 Sub ToggleTitlebar()
   Dim x
   On Error Resume Next
   x = oframe.Titlebar
   oframe.Titlebar = Not x
 End Sub

 Sub ToggleToolbars()
   Dim x
   On Error Resume Next
   x = oframe.Toolbars
   oframe.Toolbars = Not x
 End Sub

 Sub ToggleMenubar()
   Dim x
   On Error Resume Next
   x = oframe.Menubar
   oframe.Menubar = Not x
   oframe.Activate
 End Sub
        </script>

        <!--titlebar start-->
        <!--titlebar end-->
        <!--main body start-->
        <table border="0" cellpadding="0" cellspacing="0" style="height: 100%" width="100%">
            <tr>
                <td valign="top" width="10">
                </td>
                <td valign="top" width="175">
                    <!--left-side start-->
                    <table bgcolor="#f1f1f1" border="0" cellpadding="0" cellspacing="0" width="175">
                        <!-- Load a Document -->
                        <tr>
                            <td bgcolor="#ffcc00" colspan="2" valign="top">
                                <img height="10" src="lefttopcurve.gif" width="10" /></td>
                            <td bgcolor="#ffcc00" class="fontSize1" rowspan="2" valign="middle" width="155">
                                <b>Load a Document</b></td>
                            <td bgcolor="#ffcc00" colspan="2" valign="top">
                                <img height="10" src="righttopcurve.gif" width="10" /></td>
                        </tr>
                        <tr>
                            <td bgcolor="#d6d3d6" width="1">
                            </td>
                            <td bgcolor="#ffcc00" height="9" width="9">
                            </td>
                            <td bgcolor="#ffcc00" height="10" width="9">
                            </td>
                            <td bgcolor="#d6d3d6" width="1">
                            </td>
                        </tr>
                        <tr>
                            <td bgcolor="#d6d3d6" width="1" style="height: 369px">
                            </td>
                            <td width="9" style="height: 369px">
                            </td>
                            <td class="fontSize1" width="155" style="height: 369px">
                                <br />
                                <div class="fakehlink" onclick="NewDoc">
                                    &nbsp;</div>
                                <br />
                                <asp:Button ID="Button1" runat="server" Text="Button" OnClick="Button1_Click" /></td>
                            <td width="9" style="height: 369px">
                            </td>
                            <td bgcolor="#d6d3d6" width="1" style="height: 369px">
                            </td>
                        </tr>
                        <!-- Perform Document Task -->
                        <!-- Control Appearance -->
                        <tr>
                            <td colspan="5">
                                <img height="10" src="bottomcurve.gif" width="175" /></td>
                        </tr>
                    </table>
                    <p class="fontSize1">
                        <span id="tstat">Click on the document icon on the titlebar to show the file menu, or
                            use the commands above to perform an action.</span>
                        <!--left-side end-->
                    </p>
                </td>
                <td valign="top" width="10">
                </td>
                <td valign="top" id="TD1" runat="server">
                    <!--right-side start-->
                    <!--right-side end-->
                    <table border="0" cellpadding="0" cellspacing="0" height="100%" width="100%">
                        <tr>
                            <td style="width: 1px" valign="top">
                            </td>
                            <td colspan="2" valign="top">
                                
                            </td>
                        </tr>
                        <tr>
                            <td height="15" style="width: 1px" valign="top">
                            </td>
                            <td class="fontSize1" colspan="2" valign="bottom">
                                .</td>
                        </tr>
                    </table>
    <object id="oframe" name="OFrame" classid="clsid:00460182-9E5E-11d5-B7C8-B8269041DD57" height="100%"
                                    width="100%">
                                    <param name="BorderStyle" value="1">
                                    <param name="TitlebarColor" value="52479">
                                    <param name="TitlebarTextColor" value="0">
                                    <param name="Menubar" value="1">
                                </object>
                    
                    </td>
                <td valign="top" width="10">
                </td>
            </tr>
        </table>
        <!--main body end-->
    </div>
    </form>
</body>
</html>
