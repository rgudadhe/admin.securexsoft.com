<%@ Page Language="VB" AutoEventWireup="false" CodeFile="ViewUAssignment.aspx.vb" Inherits="UserPhyAssgn_Default" EnableViewState="True" %>
<%@ Register Assembly="KMobile.Web" Namespace="KMobile.Web.UI.WebControls" TagPrefix="asp" %>
<%@ Register
    Assembly="AjaxControlToolkit"
    Namespace="AjaxControlToolkit"
    TagPrefix="ajaxToolkit" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 1.0 Transitional/EN">
<LINK href= "../../styles/Default.css" type="text/css" rel="stylesheet">
<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Untitled Page</title>
   <script language="JavaScript" type="text/javascript" >
   var newwindow;
function poptastic(inpt)
{
    url="removep.aspx?TrackID="+ inpt;
    //alert(inpt);
    
	newwindow=window.open(url,'name','height=100,width=400, left=300, top=100');
	if (window.focus) {newwindow.focus()}
}


   function changeAll() {
		if (document.form1.ChkAll.checked) {
			elval = true;
		} else
		 {
			elval = false;
			}
		for (var i=0;i<document.form1.elements.length;i++)
			{
			    //alert(document.form1.elements[i].name.substring(0,5)); 
			    if (document.form1.elements[i].name.substring(0,5) == 'PhyID')
			       { 
			            
				        document.form1.elements[i].checked = elval;        
		        	    highlightRow(document.form1.elements[i]);
				    }
			}
}

function highlightRow(InputNode) {
	//alert(InputNode);
     var el = InputNode;
     while (el.nodeName.toLowerCase() != 'tr')
           el = el.parentNode;
           //alert(e1.parentnode);
 //    el.style.backgroundColor = (InputNode.checked) ? '#eee8aa' : '#d7dbdd';
    if(InputNode.checked)
     {
     el.style.backgroundColor='#eee8aa';
     ChVal = true;
     for (var i=0;i<document.form1.elements.length;i++)
			{
			//alert(document.form1.elements[i].value);
				if (document.form1.elements[i].name.substring(0,5) == 'PhyID'  && document.form1.elements[i].checked==false)
				{
				
				ChVal = false;
				//alert(ChVal);
				}
				}
	 }
     else
     {
     el.style.backgroundColor='#d7dbdd';
      ChVal = false;
         for (var i=0;i<document.form1.elements.length;i++)
			{
			//alert(document.form1.elements[i].value);
				if (document.form1.elements[i].name.substring(0,5) == 'PhyID' && document.form1.elements[i].checked == false)
				{
				
				ChVal = false;
				//alert(ChVal);
				}
				}

   }
   //alert(ChVal);   
	document.form1.ChkAll.checked=ChVal; 

}

   function ChkLvl()
{
//alert('Hello');
var J = 0;
//alert('Hello');
for (counter = 1; counter < document.form1.elements.length ; counter++)
{

if (document.form1.elements[counter].checked && document.form1.elements[counter].name.substring(0,7) == 'LevelNo')
{ 
J = J + 1; 
}
}

if (J == 0 )
{
alert("Please select level!");
return false;
}

}

   function ChkPhy()
{
//alert('Hello');
var J = 0;
//alert('Hello');
for (counter = 1; counter < document.form1.elements.length ; counter++)
{

if (document.form1.elements[counter].checked && document.form1.elements[counter].name.substring(0,5) == 'PhyID')
{ 
J = J + 1; 
}
}

if (J == 0 )
{
alert("Please select Physician!");
return false;
}
}


 function ChkLvlPhy()
{
//alert('Hello');
var J = 0;
//alert('Hello');
for (counter = 1; counter < document.form1.elements.length ; counter++)
{

if (document.form1.elements[counter].checked && document.form1.elements[counter].name.substring(0,7) == 'LevelNo')
{ 
J = J + 1; 
}
}

if (J == 0 )
{
alert("Please select level!");
return false;
}

//alert('Hello');
var i = 0;
//alert('Hello');
for (counter = 1; counter < document.form1.elements.length ; counter++)
{

if (document.form1.elements[counter].checked && document.form1.elements[counter].name.substring(0,5) == 'PhyID')
{ 
i = i + 1; 
}
}

if (i == 0 )
{
alert("Please select Physician!");
return false;
}
}
</script> 
</head>
<body>
    <form id="form1" runat="server">
    
    <div>
    <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager> 
         
       <table border="1" cellpadding="2" style="font-family: Trebuchet MS; font-size: 9pt" width="100%" id="Table2" bgcolor="#F0F9FB">
        <tr>
            <td class="HeaderDiv" colspan="4" style="height: 15px; text-align: center" valign="top">
                    <span style="font-family: Trebuchet MS; color: white;"><strong>View Account Assignment</strong></span></td>
               </tr>
                
                    
                        <tr>
                            <td style="width: 50%; height: 23px;" >
                                <strong>
                                Account</strong></td>
                            <td style="width: 50%; height: 23px;" >
                                <asp:Label ID="lblAct" runat="server" Font-Bold="True" Font-Names="Trebuchet MS"
                                    Font-Size="10pt" ForeColor="Maroon"></asp:Label></td>
                           
                        
                        </tr>
                    </table>
         <ajaxToolkit:Accordion ID="MyAccordion" runat="server"  Width="100%"  SelectedIndex="-1" 
           FadeTransitions="false" FramesPerSecond="50"    
            TransitionDuration="150" AutoSize="None" RequireOpenedPane="false"    SuppressHeaderPostbacks="False">
           <Panes>
            
    <ajaxToolkit:AccordionPane ID="AccordionPane1" runat="server"   >
            
                <Header>
               <table border="1" cellpadding="2" style="font-family: Trebuchet MS; font-size: 9pt" width="100%" id="Table1" bgcolor="#F0F9FB">
  <tr>
    <td width="50%">MT Name - <asp:Label ID="lblusername" runat="server" Font-Bold="True" Font-Names="Trebuchet MS"
                                    Font-Size="10pt" ForeColor="Maroon"></asp:Label></td>
    <td width="50%">MT Name - <asp:Label ID="lblID" runat="server" Font-Bold="True" Font-Names="Trebuchet MS"
                                    Font-Size="10pt" ForeColor="Maroon"></asp:Label></td>
  </tr>
                   </table> 
               
                </Header>
               
                <Content>
                
<table border="1" cellpadding="2" style="font-family: Trebuchet MS; font-size: 9pt" width="100%" id="AutoNumber1" >
  <tr>
    <td width="50%"><b>Address</b></td>
    <td width="50%"><asp:Label ID="lblAddress" runat="server" Font-Bold="True" Font-Names="Trebuchet MS"
                                    Font-Size="10pt" ForeColor="Maroon"></asp:Label></td>
  </tr>
  <tr>
    <td width="50%"><b>City</b></td>
    <td width="50%"><asp:Label ID="lblCity" runat="server" Font-Bold="True" Font-Names="Trebuchet MS"
                                    Font-Size="10pt" ForeColor="Maroon"></asp:Label></td>
  </tr>
  <tr>
    <td width="50%"><b>State</b></td>
    <td width="50%"><asp:Label ID="lblState" runat="server" Font-Bold="True" Font-Names="Trebuchet MS"
                                    Font-Size="10pt" ForeColor="Maroon"></asp:Label></td>
  </tr>
  <tr>
    <td width="50%"><b>ChatID</b></td>
    <td width="50%"><asp:Label ID="lblChatID" runat="server" Font-Bold="True" Font-Names="Trebuchet MS"
                                    Font-Size="10pt" ForeColor="Maroon"></asp:Label></td>
  </tr>
  <tr>
    <td width="50%"><b>OfficialMailID</b></td>
    <td width="50%"><asp:Label ID="lblOfficialMailID" runat="server" Font-Bold="True" Font-Names="Trebuchet MS"
                                    Font-Size="10pt" ForeColor="Maroon"></asp:Label></td>
  </tr>
  <tr>
    <td width="50%"><b>PhoneNo</b></td>
    <td width="50%"><asp:Label ID="lblPhoneNo" runat="server" Font-Bold="True" Font-Names="Trebuchet MS"
                                    Font-Size="10pt" ForeColor="Maroon"></asp:Label></td>
  </tr>
  <tr>
    <td width="50%"><b>CellNo</b></td>
    <td width="50%"><asp:Label ID="lblCellNo" runat="server" Font-Bold="True" Font-Names="Trebuchet MS"
                                    Font-Size="10pt" ForeColor="Maroon"></asp:Label></td>
  </tr>
    <tr>
    <td width="50%"><b>Joining Date</b></td>
    <td width="50%"><asp:Label ID="lbljoin" runat="server" Font-Bold="True" Font-Names="Trebuchet MS"
                                    Font-Size="10pt" ForeColor="Maroon"></asp:Label></td>
  </tr>

  <tr>
    <td width="50%"><b>Mentor</b></td>
    <td width="50%"><asp:Label ID="lblMentor" runat="server" Font-Bold="True" Font-Names="Trebuchet MS"
                                    Font-Size="10pt" ForeColor="Maroon"></asp:Label></td>
  </tr>
</table>
                   
               </Content>
            </ajaxToolkit:AccordionPane>
            </Panes> 
            </ajaxToolkit:Accordion> 
                <asp:CompleteGridView ID="GridView1" runat="server" AutoGenerateColumns="True" 
                    AllowPaging="False" CellPadding="2" AllowSorting="False" 
                    Font-Names="Arial" 
                    Font-Size="9" EnableViewState="False"  width="100%"
                     Font-Italic="False" CaptionAlign="Bottom"  DataMember="DefaultView"  >

                <FooterStyle BackColor="#5D7B9D" ForeColor="White" Font-Bold="True" CssClass="TransStatus"></FooterStyle>
                <HeaderStyle BackColor="#5D7B9D"  Font-Bold="True" ForeColor="white"   Font-Names="Arial" Font-Size="9" HorizontalAlign="Center" VerticalAlign="Middle" CssClass="TransStatus"></HeaderStyle>
                <EditRowStyle BackColor="#999999"></EditRowStyle>
                <PagerStyle BackColor="LightSlateGray" BorderStyle="Groove"  BorderColor="#E0E0E0" HorizontalAlign="Center" CssClass="TransStatus" ForeColor="black"></PagerStyle>
                <AlternatingRowStyle BackColor="White" ForeColor="#284775" HorizontalAlign="Left" VerticalAlign="Middle"></AlternatingRowStyle>
                <RowStyle BackColor="#F7F6F3" ForeColor="#333333" HorizontalAlign="Left" VerticalAlign="Middle"></RowStyle>
                <PagerSettings PreviousPageText="Previous" PageButtonCount="25" Mode="NumericFirstLast" LastPageText="Last Page" FirstPageText="First Page" NextPageText="Next Page"></PagerSettings>
                <SelectedRowStyle BackColor="#E2DED6" ForeColor="#333333" Font-Bold="True"></SelectedRowStyle>
                </asp:CompleteGridView>
               
               
                  
          </div>    
    </form>
</body>
</html>
