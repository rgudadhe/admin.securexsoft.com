<%@ Page Language="VB" AutoEventWireup="false" CodeFile="ViewActAssment.aspx.vb" Inherits="UserPhyAssgn_Default" EnableViewState="True" %>
<%@ Register Assembly="KMobile.Web" Namespace="KMobile.Web.UI.WebControls" TagPrefix="asp" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 1.0 Transitional/EN">
<LINK href= "../../styles/Default.css" type="text/css" rel="stylesheet">
<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Untitled Page</title>
   <script language="JavaScript" type="text/javascript" >
   var newwindow;
function ViewDet(inpt1, inpt2)
{
    url="document.aspx?HReportID="+ inpt;
    //alert(inpt);
    
	newwindow=window.open(url,'OpenRep',"width="+screen.width+",height="+screen.height+", scrollbars=1,menubar=0,toolbar=0,location=0,status=0");
	if (window.focus) {newwindow.focus()}
}

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
        <table id="MainTable" width="80%" style="font-size: 10pt; font-family: 'Trebuchet MS'; font-style: italic; color:Gray; " border ="2" cellpadding ="2" cellspacing ="2">
        <tr>
            <td class="HeaderDiv" colspan="3" style="height: 15px; text-align: center" valign="top">
                    <span style="font-family: Trebuchet MS; color: white;"><strong>View Account Assignment</strong></span></td>
               </tr>
                
                    
                        <tr style="font-style: italic">
                            <td style="width: 229px" >
                                <strong>
                                Account Name</strong></td>
                            <td style="width: 293px" >
                                <asp:DropDownList ID="DLAct" runat="server" Font-Names="Trebuchet MS" Font-Size="10pt">
                                </asp:DropDownList></td>
                            <td style="width: 293px">
                                <asp:DropDownList ID="DLShow" runat="server" Font-Names="Trebuchet MS" Font-Size="10pt">
                                <asp:ListItem Text="Show All Records" Selected="True"   Value="A"></asp:ListItem> 
                                 <asp:ListItem Text="Group By MT"   Value="G"></asp:ListItem> 
                                </asp:DropDownList></td>
                        </tr>
                        <tr  class="tblbg2">
                            <td colspan="4" style="text-align: center">
                       <span ><strong>Result Display Setting</strong></span></td>
               </tr>
               <tr  class="tblbg2">
               <td  style="text-align: center; width: 229px;" >
                      </td>
                   <td colspan="3" style="text-align: left">
                       <asp:RadioButtonList ID="RB" runat="server" Font-Names="Trebuchet MS" Font-Size="Small"  >
                  <asp:ListItem Text = "Display result on screen"  Value ="D"></asp:ListItem> 
                  <asp:ListItem Text = "Export result as excel" Selected="True" Value ="E"></asp:ListItem> 
                       </asp:RadioButtonList> 
                   </td>
               </tr>
                        <tr style="font-style: italic">
                            <td colspan="3" style="height: 30px; text-align: center">
                                <asp:Button ID="BtnSubmit6" runat="server" Text="Submit" UseSubmitBehavior="False" /></td>
                        </tr>
                    </table>
         
                <asp:CompleteGridView ID="GridView1" runat="server" AutoGenerateColumns="True" 
                    AllowPaging="False" CellPadding="2" AllowSorting="False" 
                    Font-Names="Arial" 
                    Font-Size="9" EnableViewState="False"  width="80%"
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
