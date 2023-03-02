<%@ Page Language="VB" AutoEventWireup="false" CodeFile="ShowComparePDF.aspx.vb" Inherits="Dictation_Search_ShowPDF" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
 <link href= "../App_Themes/Css/Main.css" type="text/css" rel="stylesheet" />
    <link href= "../App_Themes/Css/Common.css" type="text/css" rel="stylesheet" />
    <link href= "../App_Themes/Css/Styles.css" type="text/css" rel="stylesheet" />
    <title>Untitled Page</title>
    <script language="javascript" type="text/javascript">
	function Load()
	{
		document.getElementById('Pdf').Load("https://sxf1.securexsoft.com/dictation search/Test.Pdf#toolbar=0");
		document.getElementById('zoomN').value="75"
	}
	</script>
	<script>

document.oncontextmenu=new Function("return false"); 
</script>
</head>
<body >
    <form id="form1" runat="server">
    <asp:HiddenField ID="HMcID" runat="server" />
         <asp:HiddenField ID="HAccID" runat="server" />
    <div>
        <asp:Button ID="btnProcess" CssClass="button"  OnClientClick="return confirm('Do you want to process this macro?')"  runat="server" Text="Process" />
             <asp:Label ID="Label1" runat="server"   CssClass="Title1" ForeColor="Maroon"></asp:Label>  
    <asp:Panel runat="server" ID="OpenPnl">
        <table style="width: 100%;">
            <tr>
                <td style="width: 250PX; vertical-align: top; ">
                   <table style="border-spacing: 5px !important;" Class="table table-bordered table-hover" >
                                                   <tr>
                                                       <td  style="color:Black !important;background-color:#EBF0F6 !important;font-weight:bold !important;">
                                                            Assigned Dictators
                                                       </td>
                                                   </tr>
                                         </table>
                               <div style="width: 100%; height: calc(100vh - 500px) !important; overflow-y:scroll;">
                                  
                                     <table style="border-spacing: 5px !important;" Class="table table-bordered table-hover" >         
                                                    <asp:Repeater ID="Repeater1" runat="server" EnableViewState="True">
                                                        <ItemTemplate>

                                                            <tr>
                                                                <tr>
                                                                <td style="text-align: left !important;width: 20px !important;">

                                                                      <input type="checkbox"  id="checkbox1"  checked='<%# Eval("Checked")%>'  value='<%# Eval("PhysicianID")%>' runat="server" />
                                                    <asp:HiddenField ID="HPhysicianID" Value='<%# Eval("PhysicianID")%>' runat="server" />
                                                                       

                                                                </td>
                                                            
                                                                <td style="text-align: left !important; ">

                                                                  <%#Eval("Physician")%>
                                                                       

                                                                </td>
                                                            </tr>
                                                            </tr>
                                                             
                                                        </ItemTemplate>
                                                    </asp:Repeater></table> </div> 
                     <table style="border-spacing: 5px !important;" Class="table table-bordered table-hover">
                                                   <tr style="color:Black !important;background-color:#EBF0F6 !important;font-weight:bold !important;">
                                                       <td  style="color:Black !important;background-color:#EBF0F6 !important;font-weight:bold !important;">
                                                           Comments
                                                       </td>
                                                   </tr></table>
                                <div style="width: 100%; height: calc(100vh - 400px) !important; overflow-y:scroll;">
                                <table style="border-spacing: 5px !important;" Class="table table-bordered table-hover">
                                                 
                                                    <asp:Repeater ID="Repeater2" runat="server" EnableViewState="True">
                                                        <ItemTemplate>

                                                            <tr>
                                                               <%-- <td style="text-align: left !important; padding: 5px;">
                                                                    <%#Eval("UNAME")%>  (<%#Eval("DateUpdated")%>): <br />
                                                                    <span class="val-label" style="font-size: 12px !important; "><%#Eval("Comments")%></span>
                                                                    
                                                                </td>--%>
                                                                <td style="text-align: left !important; padding: 5px;">

                                                                    <span class="val-label" style="font-size: 12px !important; "> <%#Eval("UNAME")%>  (<%#Eval("DateUpdated")%>): <br /></span>
                                                                    <div class="val" style="font-weight: bold !important; color: #339AE5 !important;">
                                                                        <span  style="font-size:12px;"><%#Eval("Comments")%></span>
                                                                        
                                                                    </div>
                                                                </td>
                                                            </tr>
                                                        </ItemTemplate>
                                                    </asp:Repeater>

                                   
                                    
                                </table>  </div>
                </td>
                <td>
                    <object data="<% =MediaURL%>"type="<% =MediaType %>" width="100%" height="800px"> 
             </object>
                </td>
            </tr>
           
        </table>
 
             </asp:Panel> 
    </div>
    </form>
</body>
</html>
