<%@ Page Language="VB" AutoEventWireup="false" CodeFile="MAMenuPage.aspx.vb" Inherits="MenuPage2" %>

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

.style17 {color: #000000; FONT-FAMILY: Arial,San Serif; font-size: 8pt;cursor:hand;}
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
     <script language="JavaScript">
function hideFrame(){
parent.document.all("BOM1").cols="25,*"
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
parent.document.all("BOM1").cols="235,*"
document.getElementById('tbl1').style.display = '';
document.getElementById('tbl2').style.display = 'none';
}
</script>

</head>

<body onload="showframe();" topmargin="0" leftmargin="0"   class="BodyStyle">
    <form id="form1" runat="server">
    <%--<asp:Button ID="Button1" Text="hide menu" UseSubmitBehavior="false" OnClientClick="hideFrame();return false;" CssClass="submit"  runat="server"  /> --%>
   <table id="tbl1">
   <tr><td>
      <table id="tbl4" border="0" cellpadding="0" cellspacing="0" style="text-align:center"   width="100%">
      <tr>
       <td width="100%">

   <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager> 
   <asp:Label ID="LblDate" runat="server" Font-Bold="True"  Font-Names="Arial" Font-Size="8" ></asp:Label><br />
    <asp:Label ID="LblAL" runat="server" Font-Bold="True"  Font-Names="Arial" Font-Size="8" ForeColor="#FF8000"></asp:Label>
   <table>
  
    <tr>
      <td style="width:173"><img alt="" src="images/navigationtab.jpg" width="173" height="33"/></td>
    </tr>
   </table>  

  <ajaxToolkit:Accordion ID="MyAccordion" runat="server" SelectedIndex="0" Width="178" 
           FadeTransitions="true" FramesPerSecond="50"   
            TransitionDuration="150" AutoSize="None" RequireOpenedPane="false"    SuppressHeaderPostbacks="False">
           <Panes>
            
  
         
                        
                <ajaxToolkit:AccordionPane ID="AccordionPane2" runat="server"  >
            
                <Header>
               
                    <table width="178">
                        <tr>
                           <td style="text-align:middle; background-image:url(images/transcriptiontab.jpg); height:20px;width:173;  " >
                           <span class="style17"><img alt="" style="display:none;FONT-SIZE: 8pt;" id="Img1"   src="rot.gif" width="17" height="17"> <b>Transcription Status</b></span>
                            </td>
                        </tr>
                    </table>
               
                </Header>
               
                <Content>
                
                    <table width="173" border="0" cellpadding="0" cellspacing="0" style="border-collapse: collapse; border-color:#111111"  id="Table1">
                       
                        <tr>
                             <td style="text-align:middle; width:173; background-image:url(images/transcriptiondropdown.jpg); height:20px; "   onclick="parent.frames['RFrame'].location='Transcription/TransMAActStatus.aspx';" >
                                <p style="text-align:left"><span style="FONT-SIZE: 8pt; font-family:'Arial'; left:20;"><b>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Search Transcription</b></p>
                            </td>
                        </tr>             
                       
                    
                    
                        <tr>
                             <td style="text-align:middle; width:173; background-image:url(images/transcriptiondropdown.jpg); height:20px; "   onclick="parent.frames['RFrame'].location='Transcription/ViewActTransLog.aspx';" >
                                <p style="text-align:left"><span style="FONT-SIZE: 8pt; font-family:'Arial'; left:20;"><b>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;View Transaction Log</b></p>
                            </td>
                        </tr>
                        
                        
                        
                   </table>
                   
               </Content>
            </ajaxToolkit:AccordionPane>
        
              
         
              <ajaxToolkit:AccordionPane ID="AccordionPane6" runat="server"  >
            
                <Header>
               
                    <table width="178">
                        <tr>
                           <td style="text-align:middle; background-image:url(images/settingstab.jpg); height:20px;width:173;  " >
                           <span class="style17"><img alt="" style="display:none;FONT-SIZE: 8pt;" id="Img5"   src="rot.gif" width="17" height="17"> <b>Master Admin Access</b></span>
                            </td>
                        </tr>
                        
                    </table>
               
                </Header>
               
                <Content>
                
                    <table width="173" border="0" cellpadding="0" cellspacing="0" style="border-collapse: collapse; border-color:#111111"  id="Table5">
                       
                                               
                         <tr>
                             <td style="text-align:middle; width:173; background-image:url(images/settingsdropdown.jpg); height:20px; "   onclick="parent.frames['RFrame'].location='setting/ChPass.aspx';" >
                                <p style="text-align:left"><span style="FONT-SIZE: 8pt; font-family:'Arial'; left:20;"><b>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Change Password</b></p>
                            </td>
                        </tr> 
                            <tr>
                             <td style="text-align:middle; width:173; background-image:url(images/adminaccessdropdown.jpg); height:20px; "   onclick="parent.frames['RFrame'].location='User Management/CreateMActUser.aspx';" >
                                                             <p style="text-align:left"><span style="FONT-SIZE: 8pt; font-family:'Arial'; left:20;"><b>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Create Account Admin</b></p>
                            </td>
                        </tr>     
                         <tr>
                             <td style="text-align:middle; width:173; background-image:url(images/settingsdropdown.jpg); height:20px; "   onclick="parent.frames['RFrame'].location='User Management/ResetPass.aspx';" >
                                <p style="text-align:left"><span style="FONT-SIZE: 8pt; font-family:'Arial'; left:20;"><b>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Reset Password</b></p>
                            </td>
                        </tr>     
                       
                   
                   </table>
                   
               </Content>
            </ajaxToolkit:AccordionPane>
                <ajaxToolkit:AccordionPane ID="AccordionPane1" runat="server"  >
            
                <Header>
               
                    <table width="178">
                        <tr>
                           <td style="text-align:middle; background-image:url(images/settingstab.jpg); height:20px;width:173;  " >
                           <span class="style17"><img alt="" style="display:none;FONT-SIZE: 8pt;" id="Img2"   src="rot.gif" width="17" height="17"> <b>Account Activity</b></span>
                            </td>
                        </tr>
                        
                    </table>
               
                </Header>
               
                <Content>
                
                    <table width="173" border="0" cellpadding="0" cellspacing="0" style="border-collapse: collapse; border-color:#111111"  id="Table2">
                             <tr>
                             <td style="text-align:middle; width:173; background-image:url(images/settingsdropdown.jpg); height:20px; "   onclick="parent.frames['RFrame'].location='accountactivity/minvdetails.aspx';" >
                                <p style="text-align:left"><span style="FONT-SIZE: 8pt; font-family:'Arial'; left:20;"><b>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Account Activity</b></p>
                            </td>
                        </tr>         
                   
                   </table>
                   
               </Content>
          
            </ajaxToolkit:AccordionPane>
            </Panes>
        </ajaxToolkit:Accordion>
           <table>
       <tr><a onclick="parent.location='logout.htm';" >
      <td width=171 height=25 align=middle     background="images/logouttab.jpg" class=HEADING id=head10 onclick=ShowHide(10) Style="Cursor:hand;">&nbsp;</TD>
    </A></TR>
    <tr>
      <td height=25 align=middle 
    background="images/tile_leftmenu.jpg" class=HEADING id=TD1 onclick=ShowHide(10)><div align="center"><br>
            <FONT face=Arial color=darkblue size=1>Copyright © 2000-2007 <br>
  E-Dictate, LLC.<br>
  <a 
      href="https://www.edictate.com/new/PrivacyStatement.pdf" 
      target=_new>Privacy Statement</A></div></TD>
    </TR>
    <tr>
      <td height=30 align=middle background="images/navigationbottomtab.jpg" class=HEADING id=TD2 onclick=ShowHide(10)>&nbsp;</TD>
    </TR>
        </table>
        
</td>
  </tr>
  </table>
  </td><td style="vertical-align:top;"   >
  <table id="Tble2" border="0" cellpadding="0" cellspacing="0" style="border-collapse: collapse; background-image: url(images/nav_bar_ext.jpg);" bordercolor="#111111" width="100%" >
  <tr>
  <td style="vertical-align:top;"   >
  <img src="images/nbar2.gif" style="cursor:hand;" onclick="hideFrame();return false;" width="25" height="259" />
  </td>
  </tr>
  </table>   
  </td></tr></table>

 <table id="tbl2" border="0" cellpadding="0" cellspacing="0" style="border-collapse: collapse; background-image: url(images/nav_bar_ext.jpg);" bordercolor="#111111" width="100%" >
  <tr>
  <td>
  <img src="images/nbar1.gif" style="cursor:hand;" onclick="showframe();return false;" width="25" height="259" />
  </td>
  </tr>
  </table>       
     </form>
</body>
</html>
