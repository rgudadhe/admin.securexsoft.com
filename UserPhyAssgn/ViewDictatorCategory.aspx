<%@ Page Language="VB" AutoEventWireup="false" CodeFile="ViewDictatorCategory.aspx.vb" Inherits="UserPhyAssgn_Default" EnableViewState="True" %>
<%@ Register Assembly="KMobile.Web" Namespace="KMobile.Web.UI.WebControls" TagPrefix="asp" %>
<%@ Register
    Assembly="AjaxControlToolkit"
    Namespace="AjaxControlToolkit"
    TagPrefix="ajaxToolkit" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 1.0 Transitional/EN">
<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>Untitled Page</title>
    
    <link href= "../App_Themes/Css/styles.css" type="text/css" rel="stylesheet" />
    <link href= "../App_Themes/Css/Common.css" type="text/css" rel="stylesheet" />
    <link href="../App_Themes/Css/DataTable.css" rel="stylesheet" type="text/css" />
    <link href="../App_Themes/Css/TableSorter.css" rel="stylesheet" type="text/css" />
    <script src="../App_Themes/JS/jquery-1.4.2.min.js" type="text/javascript"></script>  
    <script src="../App_Themes/JS/jquery.dataTables.min.js" type="text/javascript"></script>  

<script type="text/javascript" language="javascript">
    $(document).ready(function() {
				$('#GridView1').dataTable( {
					//"sPaginationType": "full_numbers"
                    "aoColumns": [
                            		{ "asSorting": [ "desc", "asc" ] },
		                            { "asSorting": [ "desc", "asc" ] },
		                            { "asSorting": [ "desc", "asc" ] }
	                              ] 
				} );
			} );
</script>
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
                    <span style="font-family: Trebuchet MS;"><strong>View Category of Dictators</strong></span></td>
               </tr>
       </table> 
        <table>
            <tr>
                <td style="width: 154px">
                    <span style="font-size: 10pt; font-family: Trebuchet MS"><strong>Category:</strong></span>
                </td>
                <td style="width: 150px">
                    <asp:DropDownList ID="DropDownList1" runat="server" Font-Names="Arial" Font-Size="10pt">
                        <asp:ListItem Selected="True">A</asp:ListItem>
                        <asp:ListItem>B</asp:ListItem>
                    </asp:DropDownList></td>
                <td style="width: 179px">
                    <span style="font-size: 10pt; font-family: Arial"><strong>Account Name</strong></span></td>
                <td style="width: 179px">
                    <asp:DropDownList ID="DropDownList2" runat="server"  DataTextField="AccountName" DataValueField="AccountID" Font-Names="Arial" Font-Size="10pt">
                        <asp:ListItem Selected="True">A</asp:ListItem>
                        <asp:ListItem>B</asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td style="width: 45px">
                    <asp:Button ID="Button1" runat="server" Text="Go" /></td>
            </tr>
        </table>
        <br />
        <table width="100%" style="text-align:left">
            <tr>    
                <td style="border:0">
                    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="false" Width="100%">
                           <Columns>
                            <asp:BoundField DataField="AccountName" HeaderText="Account Name" HeaderStyle-CssClass="Header" />
                            <asp:BoundField DataField="Physician Name" HeaderText="Physician Name" HeaderStyle-CssClass="Header" />
                            <asp:BoundField DataField="Category" HeaderText="Category" HeaderStyle-CssClass="Header" />
                           </Columns> 
                    </asp:GridView>
                </td>
            </tr>
        </table>
       
          </div>    
    </form>
</body>
</html>
