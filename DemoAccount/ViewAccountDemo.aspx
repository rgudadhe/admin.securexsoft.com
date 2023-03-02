<%@ Page Language="VB" AutoEventWireup="false" CodeFile="ViewAccountDemo.aspx.vb" Inherits="UserPhyAssgn_Default" EnableViewState="True" %>

<%@ Register Assembly="System.Web.Extensions, Version=1.0.61025.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI" TagPrefix="asp" %>



<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 1.0 Transitional/EN">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <link href= "../App_Themes/Css/Main.css" type="text/css" rel="stylesheet" />
    <link href= "../App_Themes/Css/Styles.css" type="text/css" rel="stylesheet" />
    <title>View Account Demo</title>
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
<body onmousedown="javascript: AddTHEAD('Table2')">
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager> 
        <div id="body">
    <div id="cap"></div>
    <div id="main">
    <h1>View Account Demo</h1>
        <asp:Panel ID="Panel2" runat="server" HorizontalAlign="Left">
            <asp:Panel ID="Panel1" runat="server" >
       
        <table id="MainTable" width="100%" >
                    
                        <tr>
                            <td colspan="9" style="text-align: center" class="HeaderDiv">
                                Account Search
                            </td>
                        </tr>
                        <tr>
                            <td style="height: 20px;  text-align: center; " class="alt1">
                                Account Name</td>
                            <td style="height: 20px; text-align: center" class="alt1">
                                PFirstName</td>
                            <td style=" height: 20px;text-align: center" class="alt1">
                                PLastName</td>
                            <td style="height: 20px; text-align: center" class="alt1">
                            Medical Record Number</td>
                            <td style=" height: 20px;text-align: center" colspan="2" class="alt1">
                                DtOfServ</td>
                            <td style=" height: 20px;text-align: center" class="alt1">
                                APhyName</td>
                        </tr>
                        <tr>
                            <td colspan="1" style="height: 30px; text-align: center; ">
                                <asp:DropDownList ID="ActList" runat="server" CssClass="common">
                                </asp:DropDownList></td>
                            <td colspan="1" style="height: 30px; text-align: center">
                                <asp:TextBox ID="PFirstName" runat="server" Width="80px" CssClass="common"></asp:TextBox></td>
                            <td colspan="1" style=" height: 30px; text-align: center">
                                <asp:TextBox ID="PLastName" runat="server" Width="88px" CssClass="common"></asp:TextBox></td>
                            <td colspan="1" style=" height: 30px; text-align: center">
                                <asp:TextBox ID="MedRN" runat="server" Width="64px" CssClass="common"></asp:TextBox></td>
                            <td colspan="1" style="height: 30px; text-align: center">
                                  <asp:TextBox runat="server" ID="DTOfServ1" Width="72px" CssClass="common"/>&nbsp;
         
                            </td>
                            <td colspan="1" style="height: 30px; text-align: center; ">
                                    <asp:TextBox runat="server" ID="DTOfServ2" Width="64px" CssClass="common"/>
        </td>
                            <td colspan="1" style="height: 30px; text-align: center">
                                <asp:TextBox ID="APhyName" runat="server" Width="80px" CssClass="common"></asp:TextBox></td>
                        </tr>
            <tr>
                <td colspan="7" style="height: 30px; text-align: center" class="alt1">
                    <asp:Button ID="Submit3" runat="server" Text="Submit" CssClass="button" />
                </td>
            </tr>
                    </table>
        <asp:Label ID="DispBox" runat="server" CssClass="Title" ForeColor="#C00000"></asp:Label>
        <asp:HiddenField ID="HActID" runat="server" />
        <asp:HiddenField ID="HTabName" runat="server" />
        &nbsp;&nbsp;&nbsp;
               
      &nbsp;<br />
         </asp:Panel>
         <asp:Table ID="Table1" runat="server" CssClass="common"  Width="100%">
        </asp:Table>
        <br />
        <asp:Button ID="Button1" runat="server" Text="Remove Records" CssClass="button"  />
        </asp:Panel>
        </div> 
        </div> 
    </form>
</body>
</html>
