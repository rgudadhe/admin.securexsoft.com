<%@ Page Language="VB" AutoEventWireup="false" CodeFile="ViewDemoStatus.aspx.vb" Inherits="UserPhyAssgn_Default" EnableViewState="True" %>

<%@ Register Assembly="System.Web.Extensions, Version=1.0.61025.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI" TagPrefix="asp" %>



<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 1.0 Transitional/EN">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
<link href= "../../styles/Default.css" type="text/css" rel="stylesheet" />
    <title>Untitled Page</title>
   <style type="text/css" >
   
   tHead
{
  display : table-header-group;
}

</style> 

  <script language="JavaScript" type="text/javascript" >
   var newwindow;
function EditDet(LookUpID, TabName, AccID)
{
    url="ViewPatDet.aspx?LookUpID="+ LookUpID + "&TabName=" + TabName+ "&AccID=" + AccID;
//    alert(inpt);
    
	newwindow=window.open(url,'name','height=400,width=500, left=300, top=100, scrollbars=1');
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
			    if (document.form1.elements[i].name == 'DemoRec')
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
				if (document.form1.elements[i].name == 'DemoRec'  && document.form1.elements[i].checked==false)
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
				if (document.form1.elements[i].name== 'DemoRec' && document.form1.elements[i].checked == false)
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
   <script type="text/javascript"  language="javascript">
   function AddTHEAD(tableName)
{
   var table = document.getElementById(tableName); 
   //alert(table);
   if(table != null) 
   {
   //alert(table);
    var head = document.createElement("THEAD");
    head.style.display = "table-header-group";
    head.appendChild(table.rows[0]);
    table.insertBefore(head, table.childNodes[0]); 
   }
}


      function CheckDictCode()
      {
     var rcount = document.getElementById('HDictCode').value;
     for (i=1; i<=rcount; i++)
        {
      var DictCode = 'Dictcode' + i;
      if (document.getElementById(DictCode).value == '')
      {
      alert('Please enter Dictation Code');
      document.getElementById(DictCode).focus();
      return false;
      }
     }
      return true;
      }
      
   function addRowToTable()
{
  var tbl = document.getElementById('Table3');
  var lastRow = tbl.rows.length;
  var iteration = lastRow;
  var row = tbl.insertRow(lastRow);
  
  var cellLeft = row.insertCell(0);
  var textNode = document.createTextNode(iteration);
  cellLeft.appendChild(textNode);


  var cell1 = row.insertCell(1);
  var sel1 = document.getElementById('Dictcode1').cloneNode(true);
  sel1.name = 'Dictcode' + iteration;
  sel1.id = 'Dictcode' + iteration;
 sel1.value='';
  cell1.appendChild(sel1);



if (document.getElementById('HLocAcc').value == 'Yes')
{
var cell2 = row.insertCell(2);
var sel2 = document.getElementById('LocCode1').cloneNode(true);
 sel2.name = 'LocCode' + iteration;
 sel2.id = 'LocCode' + iteration; 
  //sel2.value='';
cell2.appendChild(sel2);
}

 document.getElementById('HDictCode').value=iteration;

}
function removeRowFromTable()
{
  var tbl = document.getElementById('Table3');
  var lastRow = tbl.rows.length-1;
  if (lastRow > 1) 
  {
  tbl.deleteRow(lastRow);
   }
  //alert(lastRow);
document.getElementById('HDictCode').value=lastRow-1;
}

</script>   
    <script type="text/javascript" language="javascript">
var newwindow;
function editDemo(inpt)
{
    url="EditConfgDemo.aspx?AccountID="+ inpt;
//    alert(inpt);
    
	newwindow=window.open(url,'name','height=600,width=500, left=300, top=100, scrollbars=1');
	if (window.focus) {newwindow.focus()}
}

function confgDemo(inpt)
{
    url="ConfgDemo.aspx?AccountID="+ inpt;
   // alert(inpt);
    
	newwindow=window.open(url,'name','height=600,width=500, left=300, top=100, scrollbars=1');
	if (window.focus) {newwindow.focus()}
}

function MTClientStatus(inpt)
{
    url="MTClientDisp.aspx?AccountID="+ inpt;
   // alert(inpt);
    
	newwindow=window.open(url,'name','height=600,width=500, left=300, top=100, scrollbars=1');
	if (window.focus) {newwindow.focus()}
}

function IMG1_onclick() {

}

function MainTable_onclick() {

}

</script>   

</head>
<body onmousedown="javascript: AddTHEAD('Table2')" style="text-align: center">
    <form id="form1" runat="server" defaultbutton="btnsubmit3" >
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager> 
    
        <asp:Panel ID="Panel1" runat="server" >
       
        <table id="MainTable" width="600" style="font-size: 10pt; font-family: 'Trebuchet MS'; font-style: italic;" border ="2" cellpadding ="2" cellspacing ="2" >
                    
                        <tr class="SMSelected">
                            <td colspan="6" style="text-align: center">
                                <b>View Demo Status</b></td>
                        </tr>
                        <tr class="SMSelected">
                            <td style="height: 20px;  text-align: center; ">
                                Name</td>
                            <td style=" height: 20px;text-align: center" colspan="2">
                                Start Submit Date</td>
                            <td style=" height: 20px;text-align: center; width: 172px;">
                                End Submit Date</td>
                        </tr>
                        <tr>
                            <td colspan="1" style="height: 30px; text-align: center; ">
                                <asp:DropDownList ID="ActList" runat="server" Font-Names="Trebuchet MS" Font-Size="Small">
                                </asp:DropDownList></td>
                            <td colspan="1" style="height: 30px; text-align: center">
                                  &nbsp;
         
                            </td>
                            <td colspan="1" style="height: 30px; text-align: center; width: 206px;">
                                    <asp:TextBox runat="server" ID="sDate" Width="64px"/>
        </td>
                            <td colspan="1" style="height: 30px; text-align: center; width: 172px;">
                                <asp:TextBox ID="eDate" runat="server" Width="80px"></asp:TextBox></td>
                        </tr>
            <tr>
                <td colspan="4" style="height: 30px; text-align: center">
        <asp:ImageButton ID="btnsubmit3" runat="server" ImageUrl="~/Images/p_submit1.jpg"  /></td>
            </tr>
                    </table>
        <br />
         </asp:Panel> 
        <asp:Table ID="Table4" runat="server"  Font-Names="Trebuchet MS" GridLines="Both"  
            Font-Size="Small" Width="100%">
        </asp:Table>
        &nbsp;<br />
        <asp:Label ID="DispBox" runat="server" Font-Bold="True" Font-Names="Trebuchet MS"
            Font-Size="Small" ForeColor="#C00000"></asp:Label>
        &nbsp; &nbsp; &nbsp;
        <br />
        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ControlToValidate="sDate" runat="server" ErrorMessage="Start Date is a must "></asp:RequiredFieldValidator>&nbsp;<br />
        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="eDate" runat="server" ErrorMessage="End Date is a must "></asp:RequiredFieldValidator>&nbsp;<br />
        <asp:HiddenField ID="HDirLevel" runat="server" />
        
    </form>
</body>
</html>
