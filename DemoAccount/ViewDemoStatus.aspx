<%@ Page Language="VB" AutoEventWireup="false" CodeFile="ViewDemoStatus.aspx.vb" Inherits="UserPhyAssgn_Default" EnableViewState="True" %>

<%@ Register Assembly="System.Web.Extensions, Version=1.0.61025.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI" TagPrefix="asp" %>
<%@ Register Assembly="KMobile.Web" Namespace="KMobile.Web.UI.WebControls" TagPrefix="asp" %>


<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 1.0 Transitional/EN">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <link href= "../App_Themes/Css/Main.css" type="text/css" rel="stylesheet" />
    <link href= "../App_Themes/Css/Styles.css" type="text/css" rel="stylesheet" />
    <title>View Demo Import Status</title>
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
        <h1>Demo Import Status</h1>
        <asp:Panel ID="Panel2" runat="server" HorizontalAlign="Left">
            <asp:Panel ID="Panel1" runat="server" >
       
        <table id="MainTable" width="100%">
                    
                        <tr>
                            <td  style="text-align: center" colspan="4" class="HeaderDiv" >
                                <b>Demo Status</b></td>
                        </tr>
                        <tr>
                            <td style="height: 20px;  text-align: center; " class="alt1">
                                Account Name</td>
                            
                            <td style=" height: 20px;text-align: center" colspan="2" class="alt1">
                                Status</td>
                            <td style=" height: 20px;text-align: center" colspan="2" class="alt1">
                                Submit Date</td>
                        </tr>
                        <tr>
                            <td colspan="1" style="height: 30px; text-align: center; ">
                                <asp:DropDownList ID="ActList" runat="server">
                                </asp:DropDownList></td>
                            <td colspan="1" style="height: 30px; text-align: center">
                                <asp:DropDownList ID="DLstatus" runat="server">
                                <asp:ListItem Text="Select" Value="" Selected="True"   ></asp:ListItem> 
                                <asp:ListItem Text="New Demo"></asp:ListItem> 
                                <asp:ListItem Text="Imported"></asp:ListItem> 
                                <asp:ListItem Text="Not Imported"></asp:ListItem> 
                                </asp:DropDownList>
                                </td>
                            
                            <td colspan="1" style="height: 30px; text-align: center">
                                  <asp:TextBox runat="server" ID="TxtDate1" Width="72px"/>&nbsp;
                                     </td>
                            <td colspan="1" style="height: 30px; text-align: center; ">
                                    <asp:TextBox runat="server" ID="TxtDate2" Width="64px"/>
        </td>
                        </tr>
            <tr>
                <td colspan="4" style="height: 30px; text-align: center">
                    <asp:Button ID="Submit3" CssClass="button" runat="server" Text="Submit" />
                    
                </td>
            </tr>
                    </table>
        <asp:Label ID="DispBox" runat="server" CssClass="Title" ForeColor="#C00000"></asp:Label>
        <asp:HiddenField ID="HActID" runat="server" />
        <asp:HiddenField ID="HTabName" runat="server" />
        &nbsp;&nbsp;&nbsp;
               
      &nbsp;<br />
              <asp:Label ID="lblDetails" runat="server" CssClass="Title" ForeColor="#C00000"></asp:Label><br />
   <br />
         </asp:Panel>
         <asp:CompleteGridView  ID="MyDataGrid" runat="server" AutoGenerateColumns="False" 
                    AllowPaging="True" AllowSorting="True" 
                                        Width="100%" PageSize="25" CountFormat="Displaying records <b>{0}</b> to <b>{1}</b> of <b>{2}</b>" ShowCount="True" Font-Italic="False" CaptionAlign="Bottom" SortAscendingImageUrl="~/App_Themes/Images/asc.gif" SortDescendingImageUrl="~/App_Themes/Images/desc.gif" ShowInsertRow="True" >
                    <FooterStyle BackColor="#5D7B9D" ForeColor="White" Font-Bold="True"></FooterStyle>
                    <PagerStyle BackColor="LightSlateGray" BorderStyle="Groove" ForeColor="White" BorderColor="#E0E0E0" HorizontalAlign="Center" CssClass="TransStatus1"></PagerStyle>
                    <PagerSettings PreviousPageText="Previous" LastPageImageUrl="~/App_Themes/Images/Last.GIF" PreviousPageImageUrl="~/App_Themes/Images/Prev.GIF" FirstPageImageUrl="~/App_Themes/Images/First.GIF" NextPageImageUrl="~/App_Themes/Images/next.GIF" PageButtonCount="25" Mode="NextPreviousFirstLast" LastPageText="Last Page" FirstPageText="First Page" NextPageText="Next Page"></PagerSettings>
                        <Columns>
                            <asp:BoundField DataField="AccountName" HeaderText="Account Name" SortExpression="AccountName" HeaderStyle-CssClass="alt1" />
                            <asp:BoundField DataField="DemoFileName" HeaderText="Demo FileName" SortExpression="DemoFileName" HeaderStyle-CssClass="alt1" />
                            <asp:BoundField DataField="DemoDesc" HeaderText="Description" SortExpression="DemoDesc" HeaderStyle-CssClass="alt1" />
                            <asp:BoundField DataField="Status" HeaderText="Status" SortExpression="Status" HeaderStyle-CssClass="alt1" />
                            <asp:BoundField DataField="UpdatedDate" HeaderText="Submit Date" SortExpression="UpdatedDate" HeaderStyle-CssClass="alt1" />
                           </Columns>
                </asp:CompleteGridView>
                 <br />
        <asp:Button ID="Button1" runat="server" Text="Remove Records" CssClass="button"  />

        </asp:Panel>
        </div> 
        </div> 
    </form>
</body>
</html>
