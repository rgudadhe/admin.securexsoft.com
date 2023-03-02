<%@ Page Language="VB" AutoEventWireup="false" CodeFile="MenuPage2.aspx.vb" Inherits="MenuPage" %>

<%@ Register Assembly="System.Web.Extensions, Version=1.0.61025.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI" TagPrefix="asp" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<%@ Register
    Assembly="AjaxControlToolkit"
    Namespace="AjaxControlToolkit"
    TagPrefix="ajaxToolkit" %>
    
<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">

 <style type="text/css"  >
 div.scrollTableContainer {
height: 285px;
overflow: auto;
width: 970px;
margin: 15px 0 0 0;
position: relative;
}

/* The different widths below are due to the way the scroll bar is implamented */

/* All browsers (but especially IE) */
div.scrollTableContainer table {
width: 952px;
}

/* Modern browsers (but especially firefox ) */
html>/**/body div.scrollTableContainer table {
width: 970px;
}

/* Modern browsers (but especially firefox ) */
html>/**/body div.scrollTableContainer table>tbody {
overflow: auto;
height: 250px;
/*overflow-x: hidden; */
}

div.scrollTableContainer thead tr {
position:relative;
/*top: expression(offsetParent.scrollTop); IE5+ only*/
/* fixes the header being over too far in IE, doesn’t seem to affect FF */
left: 0px;
}

input.submit{
	background:url(images/submit_bg.gif) no-repeat 37px 0 #FFFFFF; color:#FF8000; border:none;
	width:63px; height:13px; float:right; margin:7px 0 0 0; padding:0 23px 0 0; cursor:pointer;
	font:normal 10px/13px Arial, Helvetica, sans-serif; text-transform:uppercase;}
	 
.LINKSOFF {
	DISPLAY: none; FONT-SIZE: 12px; COLOR: #000080; FONT-FAMILY: Arial,San Serif
}
.LINKSON {
	DISPLAY: inline; FONT-SIZE: 12px; COLOR: #000080; FONT-FAMILY: Arial,San Serif
}
INPUT.btnhov {
	BORDER-LEFT-COLOR: #c63; BORDER-BOTTOM-COLOR: #930; BORDER-TOP-COLOR: #c63; BORDER-RIGHT-COLOR: #930
}
.style3 {
	FONT-FAMILY: Arial,San Serif
	font-weight: bold;
	font-size: 8pt;
}
.dates {
	FONT-FAMILY: Arial,San Serif
	font-weight: bold;
	font-size: 8pt;
}
.styleN {color: #000000; FONT-FAMILY: Arial,San Serif; font-size: 8pt;cursor:hand;font-weight: bold;}
.style17 {color: #000000; FONT-FAMILY: Arial,San Serif; font-size: 8pt;cursor:hand;}
.styleO {color: #000000; FONT-FAMILY: Arial,San Serif; font-size: 8pt;cursor:hand;}
.style19 {color: #999999; FONT-FAMILY: Arial,San Serif; font-size: 8pt;cursor:hand;}
.style18 {DISPLAY: none; FONT-SIZE: 8pt; COLOR: #000000;FONT-FAMILY: Arial,San Serif
}
a:visited {
	color: #666666;
	text-decoration: none;
}
a:link {
	color: #999999;
	text-decoration: none;
}
.style22 {font-size: 11px}
a:hover {
	text-decoration: underline;
	color: #333333;
}
a:active {
	text-decoration: none;
	color: #666666;
}
.style24 {color: #666666}
body { margin-left: 0px; margin-top: 0px; margin-right: 0px;
margin-bottom:0px; }

     
</style>



    <title>Untitled Page</title>
 
     <script language="JavaScript" type="text/javascript"  >
//     function CheckFolders1()
//     {
//CheckFolders();
//}

function OpenService(str,flag,sname)
{
    if (str!='')
    {
        if (flag)
        {
            window.open (str,"mywindow1");   
        }
        else
        {
            window.open ("Redirect.aspx?flag="+flag+"&URL="+str+"&sname="+sname,"RFrame");
        }
        
  	}
  	
    return false;
}


function Open()
{
    var displayWindow;
    displayWindow = window.open('', "newWin4");
    document.form1.target = "newWin4";
    document.form1.action='https://secureit.edictate.com/customerTroubleTicket/login.aspx'
    document.form1.submit();
  	document.form1.target = "_self";
  	document.form1.action="MenuPage2.aspx"; 
    return false;
}
function hideFrame(){
parent.document.all("BOM1").cols="33,*"
document.getElementById('tbl1').style.display = 'none';
document.getElementById('tbl2').style.display = '';
//if (document.getElementById('Button1').value == 'hide menu')
//{
//parent.document.all("BOM1").cols="25,*"
//document.getElementById('tbl1').style.display = 'none';
//document.getElementById('tbl2').style.display = '';
//document.getElementById('Button1').value = 'show menu';
//document.getElementById('Button1').style.display = 'none';
//}
//else
//{
//parent.document.all("BOM1").cols="235,*"
//document.getElementById('tbl1').style.display = '';
//document.getElementById('tbl2').style.display = 'none';
//document.getElementById('Button1').value = 'hide menu';
//document.getElementById('Button1').style.display = '';
//}
}
</script>
<script language="JavaScript">
function showframe(){
parent.document.all("BOM1").cols="208,*"
document.getElementById('tbl1').style.display = '';
document.getElementById('tbl2').style.display = 'none';
}
</script>
<script language="vbscript" type="text/vbscript" >
SUB ShowHide1(menuID)
On Error Resume Next
'menuObj = "Link" & menuID
dim trmenu
trmenu = "tr" & menuID
For i = 0 to document.all.hTDMIncr.value
document.all.item("tr" & i).className = "styleO"
Next
'ImageObj = "Image" & menuID
'document.all.item("tr1").className = "styleO"
'document.all.item("tr2").className = "styleO"
'document.all.item("tr3").className = "styleO"
'document.all.item("tr4").className = "styleO"
'document.all.item("tr5").className = "styleO"
'document.all.item("tr6").className = "styleO"
document.all.item(trmenu).className = "styleN"
End Sub
Sub CheckFolders()

On error resume next
Dim objFSO, dir
Set objFSO = CreateObject("Scripting.FileSystemObject") 
dir = "c:\edictate"
             If Not objFSO.FolderExists(dir) Then
                objFSO.CreateFolder(dir)
            End If
            dir = "c:\edictate\Voice"
            If Not objFSO.FolderExists(dir) Then
                objFSO.CreateFolder(dir)
            End If
            dir = "c:\edictate\Feedback"
            If Not objFSO.FolderExists(dir) Then
                objFSO.CreateFolder(dir)
            End If
            dir = "c:\edictate\Demo"
            If Not objFSO.FolderExists(dir) Then
                objFSO.CreateFolder(dir)
            End If
            dir = "c:\edictate\Reports"
            If Not objFSO.FolderExists(dir) Then
                objFSO.CreateFolder(dir)
            End If
            dir = "c:\edictate\Temp"
            If Not objFSO.FolderExists(dir) Then
                objFSO.CreateFolder(dir)
            End If

            dir = "c:\edictate\Backup"
            If Not objFSO.FolderExists(dir) Then
                objFSO.CreateFolder(dir)
            End If

            dir = "c:\edictate\Backup\Reports"
            If Not objFSO.FolderExists(dir) Then
                objFSO.CreateFolder(dir)
            End If
            dir = "c:\edictate\Backup\Voice"
            If Not objFSO.FolderExists(dir) Then
                objFSO.CreateFolder(dir)
            End If
            dir = "c:\edictate\Backup\Demo"
            If Not objFSO.FolderExists(dir) Then
                objFSO.CreateFolder(dir)
            End If
            dir = "c:\edictate\Backup\Feedback"
            If Not objFSO.FolderExists(dir) Then
                objFSO.CreateFolder(dir)
            End If
            dir = "c:\edictate\Log"
            If Not objFSO.FolderExists(dir) Then
                objFSO.CreateFolder(dir)
            End If
            dir = "c:\edictate\AppFiles"
            If Not objFSO.FolderExists(dir) Then
                objFSO.CreateFolder(dir)
            End If
            
End Sub
SUB ShowHideS(menuID)
On Error Resume Next
'menuObj = "Link" & menuID
dim trmenu
trmenu = "trS" & menuID
'var j=0
'for (var i=0;i<document.form1.hTDSIncr.value;i++)
'{
'document.all.item("trS" & i).className = "styleO"
'}
For i = 0 to document.all.hTDSIncr.value
document.all.item("trS" & i).className = "styleO"
Next
'ImageObj = "Image" & menuID
'document.all.item("trS1").className = "styleO"
'document.all.item("trS2").className = "styleO"
'document.all.item("trS3").className = "styleO"
'document.all.item("trS4").className = "styleO"
'document.all.item("trS5").className = "styleO"
'document.all.item("trS6").className = "styleO"
'document.all.item("trS7").className = "styleO"
'document.all.item("trS8").className = "styleO"
'document.all.item("trS9").className = "styleO"
'document.all.item("trS10").className = "styleO"
'document.all.item("trS11").className = "styleO"
'document.all.item("trS12").className = "styleO"
'document.all.item("trS13").className = "styleO"
'document.all.item("trS14").className = "styleO"
'document.all.item("trS15").className = "styleO"
'document.all.item("trS16").className = "styleO"
'document.all.item("trS17").className = "styleO"
'document.all.item("trS18").className = "styleO"
'document.all.item("trS19").className = "styleO"
'document.all.item("trS20").className = "styleO"
'document.all.item("trS21").className = "styleO"
'document.all.item("trS22").className = "styleO"
'document.all.item("trS23").className = "styleO"
'document.all.item("trS24").className = "styleO"
'document.all.item("trS25").className = "styleO"
document.all.item(trmenu).className = "styleN"
End Sub
</script>
</head>

<body onload="showframe();CheckFolders();" topmargin="0" leftmargin="0"   class="BodyStyle">
    <form id="form1" runat="server">
    <%--<asp:Button ID="Button1" Text="hide menu" UseSubmitBehavior="false" OnClientClick="hideFrame();return false;" CssClass="submit"  runat="server"  /> --%>
    
   <table id="tbl1">
   <tr><td style="text-align:right"><img src="Images/system-log-out.png"  style="cursor:hand;"  onclick="hideFrame();return false;" /></td></tr>
   <tr><td>
      <table id="tbl4" border="0" cellpadding="0" cellspacing="0" style="text-align:center"   width="100%">
      <tr>
       <td width="100%">

   <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager> 
   <asp:Label ID="LblDate" runat="server" Font-Bold="True" Font-Italic="True" Font-Names="Arial" Font-Size="8" ForeColor="Gray"></asp:Label><br />
    <asp:Label ID="LblAL" runat="server" Font-Bold="True" Font-Italic="True" Font-Names="Arial" Font-Size="8" ForeColor="#FF8000"></asp:Label>
   
           <asp:Table ID="Table1" runat="server" >
           <asp:TableRow>
           <asp:TableCell ID="Cell1">
           </asp:TableCell> </asp:TableRow> 
           </asp:Table>
         
   <ajaxToolkit:Accordion ID="Accor" runat="server" SelectedIndex="0" Width="178" 
            
            TransitionDuration="150" AutoSize="None" RequireOpenedPane="False"     SuppressHeaderPostbacks="False">
           <Panes>
      
         <ajaxToolkit:AccordionPane ID="AccordionPane5" runat="server"   >
            
                <Header>
                  
   <table width="178">
   
    <tr>
      <td style="text-align:middle; background-image:url('images/NavBar_SecureXSoft.jpg'); height:33px;width:173;background-repeat:round;" >
                            <p style="text-align:left"><span style="FONT-SIZE: 8pt; font-family:'Arial'; left:20; color:White;cursor:pointer;"  ><b>&nbsp;&nbsp;My Home</b></span></p>
                            </td>
    </tr>
    
   </table>  
    </Header>
               
                <Content>
                 <table width="178">
                        <tr onclick="ShowHide1(1)" >
                           <td style="cursor:hand; text-align:middle; background-image:url('Images/transcriptiontab.jpg'); height:20px;width:173;background-repeat:round;"  onclick="parent.frames['right'].location='LandingPage.aspx';">
                           <span id="tr1" class="style17"><img alt="" style="display:none;FONT-SIZE: 8pt;" id="Img1"   src="rot.gif" width="17" height="17">HomePage</span>
                            </td>
                        </tr>
                        <%--<tr onclick="ShowHide1(1)" >
                           <td style="cursor:hand; text-align:middle; background-image:url('Images/transcriptiontab.jpg'); height:20px;width:173" onclick="parent.frames['right'].location='Navigation/sxf1.aspx';" >
                           <span id="Span2" class="styleN"><img alt="" style="display:none;FONT-SIZE: 8pt;" id="Img2"   src="rot.gif" width="17" height="17">WorkFlow Access (sxf1.securexsoft.com)</span>
                            </td>
                        </tr>--%>
                    </table>
                    
  <ajaxToolkit:Accordion ID="MyAccordion" runat="server" SelectedIndex="0" Width="178" 
           FadeTransitions="true" FramesPerSecond="50"   
            TransitionDuration="150" AutoSize="None" RequireOpenedPane="false"    SuppressHeaderPostbacks="False">
           <Panes>
          <ajaxToolkit:AccordionPane ID="AccordionPane2" runat="server"  >
            
                <Header>
               
                    <table width="178">
                        <tr onclick="ShowHide1(2)" >
                           <td style="cursor:hand; text-align:middle; background-image:url('Images/transcriptiontab.jpg'); height:20px;width:173;background-repeat:round;" >
                           <span id="tr2" class="style17"><img alt="" style="display:none;FONT-SIZE: 8pt;" id="Img2"   src="rot.gif" width="17" height="17">WorkFlow Access</span>
                            </td>
                        </tr>
                    </table>
               
                </Header>
               
                <Content>
                
                    <table width="173" border="0" cellpadding="0" cellspacing="0" style="border-collapse: collapse; border-color:#111111"  id="Table2">
                       
                        <tr onclick="ShowHideS(1)">
                             <td style="cursor:hand; text-align:middle; width:173; background-image:url('Images/transcriptiondropdown.jpg'); height:20px;background-repeat: no-repeat;   background-position: center;"   onclick="parent.frames['right'].location='Navigation/sxf1.aspx';" >
                                <p style="text-align:left"><span style="left:20;cursor:pointer;"  id="trS1"  class="styleO">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;sxf1.SecureXSoft.com</p>
                            </td>
                        </tr>

                        
                   </table>
                   
               </Content>
            </ajaxToolkit:AccordionPane>  
   <ajaxToolkit:AccordionPane ID="AccordionPane1" runat="server"  >
            
                <Header>
               
                    <table width="178">
                        <tr onclick="ShowHide1(3)" >
                           <td style="cursor:hand; text-align:middle; background-image:url('Images/transcriptiontab.jpg'); height:20px;width:173;background-repeat:round;" >
                           <span id="tr3" class="style17"><img alt="" style="display:none;FONT-SIZE: 8pt;" id="Img8"   src="rot.gif" width="17" height="17">My Setup</span>
                            </td>
                        </tr>
                    </table>
               
                </Header>
               
                <Content>
                
                    <table width="173" border="0" cellpadding="0" cellspacing="0" style="border-collapse: collapse; border-color:#111111"  id="Table8">
                       
                        <tr onclick="ShowHideS(2)">
                             <td style="cursor:hand; text-align:middle; width:173; background-image:url('Images/transcriptiondropdown.jpg'); height:20px;background-repeat: no-repeat;   background-position: center;"   onclick="parent.frames['right'].location='uProfile.aspx';" >
                                <p style="text-align:left"><span style="left:20;cursor:pointer;"  id="trS2" class="styleO">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Edit Profile</p>
                            </td>
                        </tr>
                                              
                        <tr onclick="ShowHideS(3)">
                             <td style="cursor:hand; text-align:middle; width:173; background-image:url('Images/transcriptiondropdown.jpg'); height:20px;background-repeat: no-repeat;   background-position: center;"   onclick="parent.frames['right'].location='ChangePass.aspx';" >
                                <p style="text-align:left"><span style="left:20;cursor:pointer;"  id="trS3" class="styleN">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Edit Password</p>
                            </td>
                        </tr>
                        
                   </table>
                   
               </Content>
            </ajaxToolkit:AccordionPane>
           </Panes>
        </ajaxToolkit:Accordion>
         </Content>
            </ajaxToolkit:AccordionPane>
         </Panes>
            </ajaxToolkit:Accordion>
        <table width="178" >
       <tr>
       <td onclick="parent.parent.location='logout.aspx';" target="_top" style="text-align:middle; background-image:url(images/NavBar_SecureXSoft.jpg); height:33px;width:173;background-repeat:round;" ><p style="text-align:left"><span style="color: #C3FDB8" class='style17'>&nbsp;&nbsp;<b>Logout</b></span> </p></td> 
      <%--<td width=171 height=25 align=middle  target="_top" onclick="parent.parent.location='logout.aspx';"  background="images/NavBar_SecureXSoft.jpg" class="style17" id=head10 onclick=ShowHide(10) Style="Cursor:hand;">&nbsp;Logout</TD>--%>
   </TR>
    
	<tr>
      <td height=25 align=middle 
    background="images/tile_leftmenu.jpg" class=HEADING id=TD1><div align="center"><br>
	<FONT face=Arial MS color=darkblue size=1>SxF-Admin 5.6<br>
            <FONT face=Arial MS color=darkblue size=1>Copyright ©<%=Year(Now)%> <br>
  MedOfficePro LLC<br>
  <a 
      href="PrivacyStatement-Securexsoft.pdf" 
      target=_new>Privacy Statement</A></div></TD>
    </TR>
    <tr>
      <td height=30 align=middle background="images/navigationbottomtab.jpg" class=HEADING id=TD2 onclick=ShowHide(10)>&nbsp;</TD>
    </TR>
        </table>
		<!--FONT face=Arial MS color=darkblue size=3><a href="http://assist.medofficepro.com" target="_blank">Support Desk</a></Font-->
 </td></tr></table>
  </td></tr></table>
   
 <table id="tbl2" border="0" cellpadding="0" cellspacing="0" style="border-collapse: collapse;" bordercolor="#111111" width="100%" >
  <tr>
  <td>
  <img src="Images/NavBar_SS.JPG" style="cursor:hand;" onclick="showframe();return false;" width="33" height="208" />
  </td>
  </tr>
  </table>  
  <asp:HiddenField ID="hTDSIncr" runat="server" />  
  <asp:HiddenField ID="hTDMIncr" runat="server" />       
     </form>
</body>
</html>
