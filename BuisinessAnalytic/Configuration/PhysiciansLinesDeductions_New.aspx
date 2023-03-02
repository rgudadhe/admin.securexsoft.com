<%@ Page Language="VB" AutoEventWireup="false" CodeFile="PhysiciansLinesDeductions_New.aspx.vb" Inherits="BuisinessAnalytic_Configuration_Default" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 1.0 Transitional/EN">
<%@ Register Assembly="KMobile.Web" Namespace="KMobile.Web.UI.WebControls" TagPrefix="asp" %>

<%@ Register
    Assembly="AjaxControlToolkit"
    Namespace="AjaxControlToolkit"
    TagPrefix="ajaxToolkit" %>

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
<link href= "../../App_Themes/Css/styles.css" type="text/css" rel="stylesheet" />

    <title>Untitled Page</title>
   <script type="text/javascript" language="javascript">
window.history.forward(1);
var newwindow;
function poptastic(inpt)
{
    url="photo.aspx?UserID="+ inpt;
    //alert(inpt);
    
	newwindow=window.open(url,'name','height=200,width=400, left=300, top=100');
	if (window.focus) {newwindow.focus()}
}

function Open()
{
    url="OtherInfo.aspx?UID="+document.getElementById('hdnUserID').value
    newwindow1=window.open(url,'data','height=650,width=800');
}
</script>  



</head>
<body  >
 
    <form id="form1" runat="server" >
                    <div id="body">
    <div id="cap"></div>
    <div id="main">
    <h1>Physicians Units Deductions </h1>
        <div style="text-align:left">
           
        <a href="ShowPhysiciansLinesDetails.aspx">Show all Physicians details</a>
        <br />               
       <table width="500px" style="text-align: left; ">
           
            <tr>
             <td style="text-align: right; " class="HeaderDiv"  >
                  
                    Account
                </td>
                <td style="text-align: left; "  class="HeaderDiv" >
                    <asp:DropDownList ID="DLAct"  runat="server" AutoPostBack="true">
                    </asp:DropDownList>
                </td>
               
            </tr>
            </table> 
            </div>     
            <asp:Panel runat="server" ID="Panel1">
            <table width="100%">
             <tr>
            <td>
                <asp:Label ID="lblMsg" runat="server" Text="" ForeColor="red" Font-Bold="true"></asp:Label>
            </td> 
            </tr>  
            <tr>
                <td>
               <table width="100%">
               
                      
           
            
            
             <tr> 
                <td style="text-align: left;">
                  <asp:CompleteGridView ID="MyDataGrid" runat="server" AllowSorting="False" 
                    DataKeyNames="physicianID"  
                    EnableViewState="False"  
                    Width="400" PageSize="25" CountFormat="Displaying records <b>{0}</b> to <b>{1}</b> of <b>{2}</b>" CaptionAlign="Bottom" AutoGenerateEditButton="True"  AutoGenerateColumns="False" ShowInsertRow="True" ShowFilter="false"  >

                <FooterStyle BackColor="#5D7B9D" ForeColor="White" Font-Bold="True"></FooterStyle>
                <EditRowStyle BackColor="#999999" HorizontalAlign="Center"></EditRowStyle>
                <PagerStyle BackColor="LightSlateGray" BorderStyle="Groove" ForeColor="White" BorderColor="#E0E0E0" HorizontalAlign="Center"></PagerStyle>
                <PagerSettings PreviousPageText="Previous" PageButtonCount="25" Mode="NumericFirstLast" LastPageText="Last Page" FirstPageText="First Page" NextPageText="Next Page"></PagerSettings>
                
                          <Columns>
                          <asp:TemplateField HeaderText="Name" HeaderStyle-CssClass="alt1">
                           <ItemTemplate>                    <asp:Label ID="lblUName" runat="server" Text='<%# Eval("Physician") %>' />  
                                       </ItemTemplate>   
                                                  
                                       </asp:TemplateField> 
                                        
                          
                              <%--<asp:BoundField DataField="uname"   HeaderText="Name" SortExpression="uname" HeaderStyle-CssClass="alt1" />--%>
                              <asp:BoundField DataField="LPM" ControlStyle-Width="50" ControlStyle-BorderStyle="Groove" ControlStyle-BackColor="yellow"   HeaderText="LPM" SortExpression="LPM" HeaderStyle-CssClass="alt1" />
                              <asp:BoundField DataField="Percentage" ControlStyle-Width="50" ControlStyle-BorderStyle="Groove" ControlStyle-BackColor="yellow"   HeaderText="Percentage" SortExpression="Percentage" HeaderStyle-CssClass="alt1" />
                              <asp:BoundField DataField="Fixed" ControlStyle-Width="50" ControlStyle-BorderStyle="Groove" ControlStyle-BackColor="yellow"   HeaderText="Fixed" SortExpression="Fixed" HeaderStyle-CssClass="alt1" />
                              <asp:BoundField DataField="Units"  ControlStyle-Width="50" ControlStyle-BorderStyle="Groove" ControlStyle-BackColor="yellow"  HeaderText="Units" SortExpression="Units" HeaderStyle-CssClass="alt1" />
                              
                             <%-- <asp:BoundField DataField="Mode" HeaderText="Mode" SortExpression="Mode" HeaderStyle-CssClass="alt1" />--%>
                          </Columns>
                </asp:CompleteGridView>
        </td>  
            </tr>
            
    
            </table>
        </td> 
        <%--<tr>
            <td style="text-align: left" colspan="2">
                Please <a href="#" onclick="javascript:Open();">click here</a> to update other information.
            </td>
        </tr>--%>
        </tr> 
       
           
          
        
        </table> 
        
        </asp:Panel> 
        
                
    
        <asp:HiddenField ID="hdnUserID" runat="server" />
    
        <asp:Label ID="Label1" runat="server" CssClass="Title" 
            ForeColor="#400000" Height="16px" Width="496px"></asp:Label>
        
    </div> 
    </div> 
    <asp:Label ID="lblDisp" runat="server" CssClass="Title" ForeColor="#C00000"></asp:Label>&nbsp; 
    </form>
    
</body>
</html>
