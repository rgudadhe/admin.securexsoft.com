<%@ Page Language="VB" AutoEventWireup="false" CodeFile="AdminPage.aspx.vb" Inherits="AdminPage" %>
<%@ Register Assembly="WebChart" Namespace="WebChart" TagPrefix="Web" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Register
    Assembly="AjaxControlToolkit"
    Namespace="AjaxControlToolkit"
    TagPrefix="ajaxToolkit" %>
<html xmlns="http://www.w3.org/1999/xhtml" >
 <link href="App_Themes/Css/styles.css"type="text/css" rel="stylesheet" />
    <link href="App_Themes/Css/Common.css"type="text/css" rel="stylesheet" /><head id="Head1" runat="server">
    <link href= "../../styles/Default.css" type="text/css" rel="stylesheet"/>
    <title>Untitled Page</title>
</head>
<body style="font: 8pt 'Arial', Tahoma, arial, sans-serif; background-color:Silver; " >
    <form id="form1" runat="server">
    <div>
     <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager> 
        <table width="100%"  border=1 cellpadding=0  cellspacing =0 style="border-style:ridge; border-color:Black;border-collapse: collapse;  "  >
        <tr >
        <td width="50%"  style="vertical-align: top; border-style:ridge; border-color:Black;">
        <ajaxToolkit:Accordion ID="MyAccordion" runat="server" SelectedIndex="0" Width="100%" 
           FadeTransitions="true" FramesPerSecond="50"    
            TransitionDuration="150" AutoSize="None" RequireOpenedPane="false"    SuppressHeaderPostbacks="False">
           <Panes>
            
   <ajaxToolkit:AccordionPane ID="AccordionPane1" BorderStyle="Double" runat="server"  >
            
                <Header>
               
                    <table width="100%" cellspacing =0 cellpadding="4"    >
                        <tr  >
                           <td width="100%" style="background-color:#25587E; color:White; font-family:Arial; font-size:medium; text-align:left;  " >
                           <img src="images/alerts.png" width="15" /> ALERTS <span style="cursor:hand; color:white; ">| > |</span>
                            </td>
                        </tr>
                    </table>
               
                </Header>
               
                <Content>
                
                    
                       
                     <table   cellpadding="0" cellspacing="0"  id="Table5">
                       
                        <tr >
                             <td  >
                             <asp:Label ID="lblAlerts" runat="server" ></asp:Label>
                            </td>
                        </tr>
                                              
                       
                      
                   </table>   
                                              
                       
                      
                  
                   
               </Content>
            </ajaxToolkit:AccordionPane> 
            </Panes> 
            </ajaxToolkit:Accordion></td>
        <td width="50%" style="vertical-align: top; border-style:ridge; border-color:Black;">
        <ajaxToolkit:Accordion ID="Accordion1" runat="server" SelectedIndex="0" Width="100%" 
           FadeTransitions="true" FramesPerSecond="50"   
            TransitionDuration="150" AutoSize="None" RequireOpenedPane="false"    SuppressHeaderPostbacks="False">
           <Panes>
            
   <ajaxToolkit:AccordionPane ID="AccordionPane2" runat="server"  >
            
                <Header>
               
                    <table width="100%" cellspacing =0 cellpadding="4"    >
                        <tr  >
                           <td width="100%" style="background-color:#25587E; color:White; font-family:Arial; font-size:medium; text-align:left;  " >
                           <img alt="" style="display:none;FONT-SIZE: 8pt;" id="Img1"   src="rot.gif" width="17" height="17"><img src="images/bar.png" width="15" /> TRENDS <span style="cursor:hand; color:white; ">| > |</span>
                            </td>
                        </tr>
                    </table>
               
                </Header>
               
                <Content>
                
                    <table   border=1 id="Table1"  cellpadding=5  cellspacing =0 style="border-style:ridge; border-color:Black;border-collapse: collapse; margin-left:12px; margin-top:12px;  "  >
                        <tr >
                             <td  class='HeaderDiv' style="text-align:right ">
                         Track
                            </td>
                                <td  class='HeaderDiv'  style="text-align:left ">
                          <asp:DropDownList ID="DLTrend" runat="server" CssClass="common" AutoPostBack="true" >
                           <asp:ListItem Text="Units" Value="Units"></asp:ListItem>  
                           <asp:ListItem Text="Revenue" Selected="True"   Value="Revenue"></asp:ListItem>  
                           </asp:DropDownList>
                            </td>
                        </tr>
                               
                        <tr >
                             <td colspan="2" >
                          <Web:ChartControl ID="ChartControl1" width="600" runat="server" BorderStyle="Outset" BorderWidth="5px">
            </Web:ChartControl>
                            </td>
                        </tr>
                                              
                       
                      
                   </table>
                   
               </Content>
            </ajaxToolkit:AccordionPane> 
            </Panes> 
            </ajaxToolkit:Accordion></td>
        </tr>
         <tr height="100%">
        
        <td  colspan="2" style="vertical-align: top; border-style:ridge; border-color:Black;">
        <ajaxToolkit:Accordion ID="Accordion3" runat="server" SelectedIndex="0" Width="100%" 
           FadeTransitions="true" FramesPerSecond="50"   
            TransitionDuration="150" AutoSize="None" RequireOpenedPane="false"    SuppressHeaderPostbacks="False">
           <Panes>
            
   <ajaxToolkit:AccordionPane ID="AccordionPane4" runat="server"  >
            
                <Header>
               
                    <table width="100%" cellspacing =0 cellpadding="4"    >
                        <tr  >
                           <td width="100%" style="background-color:#25587E; color:White; font-family:Arial; font-size:medium; text-align:left;  " >
                           <img alt="" style="display:none;FONT-SIZE: 8pt;" id="Img3"   src="rot.gif" width="17" height="17"><img src="images/salerts.png" width="15" /> SYSTEM ALERTS <span style="cursor:hand; color:white; ">| > |</span>
                            </td>
                        </tr>
                    </table>
               
                </Header>
               
                <Content>
                
                    <table  width="100%" cellpadding="0" cellspacing="0"  id="Table3">
                       
                        <tr >
                             <td  >
                              <asp:Label ID="Lblupdate" runat="server" ></asp:Label>
                            </td>
                        </tr>
                                              
                       
                      
                   </table>
                   
               </Content>
            </ajaxToolkit:AccordionPane> 
            </Panes> 
            </ajaxToolkit:Accordion></td>
        </tr>
         <tr height="100%">
        <td width="100%" height="20%" style="vertical-align: top"  colspan="2">
        <ajaxToolkit:Accordion ID="Accordion4" runat="server" SelectedIndex="0" Width="100%" 
           FadeTransitions="true" FramesPerSecond="50"   
            TransitionDuration="150" AutoSize="None" RequireOpenedPane="false"    SuppressHeaderPostbacks="False">
           <Panes>
            
   <ajaxToolkit:AccordionPane ID="AccordionPane5" runat="server"  >
            
                <Header>
               
                    <table width="100%" cellspacing =0 cellpadding="4"    >
                        <tr  >
                           <td width="100%" style="background-color:#25587E; color:White; font-family:Arial; font-size:medium; text-align:left;  " >
                           <img alt="" style="display:none;FONT-SIZE: 8pt;" id="Img4"   src="rot.gif" width="17" height="17"><img src="images/tip.png" width="15" /> TIP OF THE WEEK <span style="cursor:hand; color:white; ">| > |</span>
                            </td>
                        </tr>
                    </table>
               
                </Header>
               
                <Content>
                
                    <table   width="100%" cellpadding="0" cellspacing="0"  id="Table4">
                       
                        <tr >
                             <td  >
                             <asp:Label ID="lblTip" runat="server" ></asp:Label>
                            </td>
                        </tr>
                                              
                       
                      
                   </table>
                   
               </Content>
            </ajaxToolkit:AccordionPane> 
            </Panes> 
            </ajaxToolkit:Accordion></td>
            </tr>
            <tr height="100%">
        <td width="100%" style="vertical-align: top" colspan="2" >
        <ajaxToolkit:Accordion ID="Accordion5" runat="server" SelectedIndex="0" Width="100%" 
           FadeTransitions="true" FramesPerSecond="50"   
            TransitionDuration="150" AutoSize="None" RequireOpenedPane="false"    SuppressHeaderPostbacks="False">
           <Panes>
            
   <ajaxToolkit:AccordionPane ID="AccordionPane6" runat="server"  >
            
                <Header>
               
                    <table width="100%" cellspacing =0 cellpadding="4"    >
                        <tr  >
                           <td width="100%" style="background-color:#25587E; color:White; font-family:Arial; font-size:medium; text-align:left;  " >
                           <img alt="" style="display:none;FONT-SIZE: 8pt;" id="Img5"   src="rot.gif" width="17" height="17"><img src="images/enhancements.png" width="15" /> ENHANCEMENTS <span style="cursor:hand; color:white; ">| > |</span> 
                            </td>
                        </tr>
                    </table>
               
                </Header>
               
                <Content>
                
                  <table  width="100%" cellpadding="0" cellspacing="0"  id="Table2">
                       
                        <tr >
                             <td  >
                             <asp:Label ID="LblEnhancement" runat="server" ></asp:Label>
                            </td>
                        </tr>
                                              
                       
                      
                   </table>
                   
               </Content>
            </ajaxToolkit:AccordionPane> 
            </Panes> 
            </ajaxToolkit:Accordion></td>
        </tr>
        </table>
   
    </div>
    </form>
</body>
</html>

