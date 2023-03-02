<%@ Page Language="VB" AutoEventWireup="false" CodeFile="MenuPage2.aspx.vb" Inherits="MenuPage2" Debug="true"  %>

<%@ Register Assembly="System.Web.Extensions, Version=1.0.61025.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI" TagPrefix="asp" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 1.0 Transitional/EN">

<%@ Register
    Assembly="AjaxControlToolkit"
    Namespace="AjaxControlToolkit"
    TagPrefix="ajaxToolkit" %>
    
<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">

 <style type="text/css"  >
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
	font-size: 9px;
}

.styleO {color: #000000; FONT-FAMILY: Arial,San Serif; font-size: 8pt;cursor:hand;}
.styleN {color: #000000; FONT-FAMILY: Arial,San Serif; font-size: 8pt;font-weight: bold;cursor:hand;}
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
parent.document.all("BOM1").cols="205,*"
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
'ImageObj = "Image" & menuID
document.all.item("tr1").className = "styleO"
document.all.item("tr2").className = "styleO"
document.all.item("tr3").className = "styleO"
document.all.item(trmenu).className = "styleN"
End Sub
SUB ShowHideS(menuID)
On Error Resume Next
'menuObj = "Link" & menuID
dim trmenu
trmenu = "trS" & menuID
'ImageObj = "Image" & menuID
document.all.item("trS1").className = "styleO"
document.all.item("trS2").className = "styleO"
document.all.item(trmenu).className = "styleN"
End Sub
</script>
</head>

<body onload="showframe();" topmargin="0" leftmargin="0"   class="BodyStyle">
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
   <asp:Label ID="LblDate" runat="server" Font-Bold="True"  Font-Names="Arial" Font-Size="8" ></asp:Label><br />
    <asp:Label ID="LblAL" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="8" ForeColor="#FF8000"></asp:Label>
       <ajaxToolkit:Accordion ID="Accordion1" runat="server" SelectedIndex="0" Width="178" 
        FadeTransitions="true" FramesPerSecond="50"   
        TransitionDuration="150" AutoSize="None" RequireOpenedPane="False"     SuppressHeaderPostbacks="False">
            <Panes>
                <ajaxToolkit:AccordionPane ID="AccordionPane5" runat="server"   >
                    <Header>
                        <table width="178">
                            <tr>
                              <td style="text-align:middle; background-image:url(images/navigationtabN.jpg); height:33px;width:173;  " >
                                                    <p style="text-align:left"><span style="FONT-SIZE: 8pt; font-family:'Arial'; left:20; color:White;cursor:hand;  "><b>&nbsp;&nbsp;Navigation</b></span></p>
                                                    </td>
                            </tr>
                        </table>  
                    </Header>
                    <Content>
                        <ajaxToolkit:Accordion ID="MyAccordion" runat="server" SelectedIndex="0" Width="178" 
                            FadeTransitions="true" FramesPerSecond="50"   
                            TransitionDuration="150" AutoSize="None" RequireOpenedPane="false"    SuppressHeaderPostbacks="False">
                            <Panes>
                                <ajaxToolkit:AccordionPane ID="AccordionPane8" runat="server"  >
                                    <Header>
                                        <table width="178">
                                            <tr  onclick="ShowHide1(1)" >
                                                <td style="text-align:middle; background-image:url(images/voicefilestab.jpg); height:20px;width:173;  " >
                                                    <span  id="tr1" class="styleN"><img alt="" style="display:none;FONT-SIZE: 8pt;" id="Img7"   src="rot.gif" width="17" height="17"> Customer Care</span>
                                                </td>
                                            </tr>
                                        </table>
                                    </Header>
                                    <Content>
                                        <table width="173" border="0" cellpadding="0" cellspacing="0" style="border-collapse: collapse; border-color:#111111"  id="Table7">
                                            <tr onclick="ShowHideS(1)">
                                                <td style="cursor:hand; text-align:middle; width:173; background-image:url(images/voicefilesdropdown.jpg); height:20px; "   onclick="parent.frames['RFrame'].location='CIMS/CIMS.aspx';" >
                                                    <p style="text-align:left"><span style="left:20;"  id="trS1" class="styleN">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Trouble Ticket</p>
                                                </td>
                                            </tr>
                                            <tr  onclick="ShowHideS(2)">
                                                <td style="cursor:hand; text-align:middle; width:173; background-image:url(images/voicefilesdropdown.jpg); height:20px; "   onclick="window.open('https://www1.gotomeeting.com/en_US/island/download.tmpl?Action=rgoto&_sf=1', 'GoToMeeting');" >
                                                    <p style="text-align:left"><span style="left:20;"  id="trS2" class="styleO">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Online Support</p>
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
           <table width="178">
       <tr><a onclick="parent.location='login.aspx';" >
      <td style="text-align:middle; background-image:url(images/navigationtabN.jpg); height:33px;width:173;cursor:hand;  " >
                            <p style="text-align:left"><span style="FONT-SIZE: 8pt; font-family:'Arial'; left:20; color:White;  "><b>&nbsp;&nbsp;Logout</b></p>
                            </td>
    </A></TR>
    <tr>
      <td height=25 align=middle 
    background="images/tile_leftmenu.jpg" class=HEADING id=TD1 ><div align="center">
   <FONT face=Arial color=darkblue size=1 style="color: #ff8000"><strong>TroubleTicket Version 1.0 </strong>
       <br>
            <FONT face=Arial color=darkblue size=1>Copyright © <%=Year(now()) %> <br>
  SecureXSoft<br>
  <a 
      href="../PrivacyStatement-Securexsoft.pdf" 
      target=_new>Privacy Statement</A></div></TD>
    </TR>
    <tr>
      <td height=30 align=middle background="images/navigationbottomtab.jpg" class=HEADING id=TD2 >&nbsp;</TD>
    </TR>
        </table>
        
</td>
  </tr>
  </table>
  </td></tr></table>

 <table id="tbl2" border="0" cellpadding="0" cellspacing="0" style="border-collapse: collapse;" bordercolor="#111111" width="100%" >
  <tr>
  <td>
  <img src="images/NavBarN2.JPG" style="cursor:hand;" onclick="showframe();return false;" width="33" height="208" />
  </td>
  </tr>
  </table>       
     </form>
</body>
</html>
