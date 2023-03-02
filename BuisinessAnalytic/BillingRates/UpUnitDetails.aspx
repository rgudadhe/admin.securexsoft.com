<%@ Page Language="VB" AutoEventWireup="false" CodeFile="UpUnitDetails.aspx.vb" Inherits="UpUnitDetails" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">

<script type="text/javascript">

function sm_jump1(objSelect){
var objOpt = objSelect.options[objSelect.selectedIndex];
window.location.href='upvasdetails.asp?actid=' + document.frmAcct.aname.value;
}

function sm_jump2(objSelect){
var objOpt = objSelect.options[objSelect.selectedIndex];
window.location.href='upvasdetails.asp?actid=' + document.frmAcct.aname.value + '&smonth1=' + document.frmAcct.smonth.value;
}


function sm_jump(objSelect){
if(objSelect.options[objSelect.selectedIndex].value=='Any')
{ 
//alert('Hello');
var objname=objSelect.name;
var objlen=objSelect.name.length;
var strright=objlen-6;
var chval=Right(objSelect.name,strright);
var txAmt='txtAmt'+chval;
var sdate='sdate'+chval;
var txQua='txtQua'+chval;
var txDesc='txtDesc'+chval;
var txTtl='txtTtl'+chval;
//alert(sdate);
document.getElementById(sdate).style.visibility='hidden';
document.getElementById(txAmt).style.visibility='hidden';
document.getElementById(txQua).style.visibility='hidden';
document.getElementById(txDesc).style.visibility='hidden';
document.getElementById(txTtl).style.visibility='hidden';
document.getElementById(sdate).value='';
document.getElementById(txAmt).value=0;
document.getElementById(txDesc).value='';
document.getElementById(txQua).value=0;
document.getElementById(txTtl).value=0;
  var y=0;
  for (var x = 1; x <= document.getElementById('rcount').value; x++)
   {
    y=1*y+1*document.getElementById('txtTtl'+x).value;
   }
   
  var fvalue=Math.round(y*Math.pow(10,2))/Math.pow(10,2);
  document.getElementById('TotalAmt').innerHTML='$ ' + fvalue;
  document.getElementById('tamount').value=fvalue;
}
else
{
var objname=objSelect.name;
var objlen=objSelect.name.length;
var strright=objlen-6;
var chval=Right(objSelect.name,strright);
var txAmt='txtAmt'+chval;
var txQua='txtQua'+chval;
var txDesc='txtDesc'+chval;
var txTtl='txtTtl'+chval;
var sdate='sdate'+chval;
//alert(sdate);
document.getElementById(sdate).style.visibility='visible';
document.getElementById(txAmt).style.visibility='visible';
document.getElementById(txDesc).style.visibility='visible';
document.getElementById(txQua).style.visibility='visible';
document.getElementById(txTtl).style.visibility='visible';
var mydate= new Date();
var theyear=mydate.getFullYear()
var themonth=mydate.getMonth()+1
var thetoday=mydate.getDate()
document.getElementById(sdate).value=themonth+"/"+thetoday+"/"+theyear;


var arItem=objSelect.value;
var arrayItem=objSelect.value.split('#');
//alert(arrayItem[1])
var strAmt=arrayItem[1];
var strDesc=arrayItem[3];
document.getElementById(txAmt).value=strAmt;
document.getElementById(txDesc).value=strDesc;
document.getElementById(txQua).value=1;
var avalue=Math.round(arrayItem[1]*Math.pow(10,2))/Math.pow(10,2);
document.getElementById(txTtl).value=avalue.toFixed(2);
var y=0;
  for (var x = 1; x <= document.getElementById('rcount').value; x++)
   {
   	//alert(document.getElementById('txtTtl'+x).value);
    y=1*y+1*document.getElementById('txtTtl'+x).value;
    
   }
  var fvalue=Math.round(y*Math.pow(10,2))/Math.pow(10,2);
  document.getElementById('TotalAmt').innerHTML='$ ' + fvalue.toFixed(2);
  document.getElementById('tamount').value=fvalue.toFixed(2);

}
//var objOpt = objSelect.options[objSelect.selectedIndex];
//alert(objOpt.value);
}

function CalcValue(objSelect){
var objname=objSelect.name;
var objlen=objSelect.name.length;
var strright=objlen-6;
var chval=Right(objSelect.name,strright);
var txAmt='txtAmt'+chval;
var txQua='txtQua'+chval;
//var txDesc='txtDesc'+chval;
var txTtl='txtTtl'+chval;
var cvalue = document.getElementById(txAmt).value*document.getElementById(txQua).value;
var avalue=Math.round(cvalue*Math.pow(10,2))/Math.pow(10,2);
document.getElementById(txTtl).value=avalue.toFixed(2);
//document.getElementById(txTtl).value=document.getElementById(txAmt).value*document.getElementById(txQua).value;
//var objOpt = objSelect.options[objSelect.selectedIndex];
//alert(objOpt.value);
var y=0;
  for (var x = 1; x <= document.getElementById('rcount').value; x++)
   {
    y=1*y+1*document.getElementById('txtTtl'+x).value;
   
   }
  
     var fvalue=Math.round(y*Math.pow(10,2))/Math.pow(10,2);
  document.getElementById('TotalAmt').innerHTML='$ ' + fvalue.toFixed(2);
   document.getElementById('tamount').value=fvalue.toFixed(2);
}



function Left(str, n){
	if (n <= 0)
	    return "";
	else if (n > String(str).length)
	    return str;
	else
	    return String(str).substring(0,n);
}
function Right(str, n){
    if (n <= 0)
       return "";
    else if (n > String(str).length)
       return str;
    else {
       var iLen = String(str).length;
       return String(str).substring(iLen, iLen - n);
    }
}
</script>

<script language="JavaScript" type="text/javascript">
<!--
// Last updated 2006-02-21
function addRowToTable()
{
  var tbl = document.getElementById('Table1');
  var lastRow = tbl.rows.length;
  // if there's no header row in the table, then iteration = lastRow + 1
  var iteration = lastRow-1;
  var row = tbl.insertRow(lastRow);
  
  
 
  
  // left cell
  var cellLeft = row.insertCell(0);
  var textNode = document.createTextNode(iteration);
  cellLeft.appendChild(textNode);
  
   // select cell
  var cellRightSel = row.insertCell(1);
 // var sel = document.createElement('select');
  var sel = document.getElementById('DLItem1').cloneNode(true);
  sel.name = 'DLItem' + iteration;
  // sel.options[0] = new Option('text zero', 'value0');
  // sel.options[1] = new Option('text one', 'value1');
  cellRightSel.appendChild(sel);

 // right cell
  var cellRight5 = row.insertCell(3);
  //var e5 = document.createElement('input');
  var e5 = document.getElementById('sdate1').cloneNode(true)
  e5.type = 'text';
  e5.name = 'sdate' + iteration;
  e5.id = 'sdate' + iteration;
  e5.size = 10;
  cellRight5.appendChild(e5);
  //alert('sdate' + iteration);
  document.getElementById('sdate' + iteration).value='';	
  document.getElementById('sdate' + iteration).style.visibility='hidden';  
  
 // right cell
  var cellRight = row.insertCell(4);
  //var el = document.createElement('input');
  var el = document.getElementById('txtQua1').cloneNode(true)
  el.type = 'text';
  el.name = 'txtQua' + iteration;
  el.id = 'txtQua' + iteration;
 // el.size = 16;
  cellRight.appendChild(el);
  document.getElementById('txtQua' + iteration).value=0;	
  document.getElementById('txtQua' + iteration).style.visibility='hidden';  
  
  // right cell
  var cellRight1 = row.insertCell(5);
 // var e2 = document.createElement('input');
  var e2 = document.getElementById('txtAmt1').cloneNode(true)
  e2.type = 'text';
  e2.name = 'txtAmt' + iteration;
  e2.id = 'txtAmt' + iteration;
 // e2.size = 16;
  cellRight1.appendChild(e2);
  document.getElementById('txtAmt' + iteration).value=0;  
  document.getElementById('txtAmt' + iteration).style.visibility='hidden';
 


  // right cell
  var cellRight3 = row.insertCell(6);
  var e3 = document.getElementById('txtTtl1').cloneNode(true)
  e3.type = 'text';
  e3.name = 'txtTtl' + iteration;
  e3.id = 'txtTtl' + iteration;
 // e3.size = 16;
  cellRight3.appendChild(e3);  
  document.getElementById('txtTtl' + iteration).value=0;   
  document.getElementById('txtTtl' + iteration).style.visibility='hidden';
//  document.getElementById('rcount').value=iteration; 
  
   // right cell
  var cellRight7 = row.insertCell(2);
  var e7 = document.getElementById('txtDesc1').cloneNode(true)
  e7.type = 'text';
  e7.name = 'txtDesc' + iteration;
  e7.id = 'txtDesc' + iteration;
 // e3.size = 16;
  cellRight7.appendChild(e7);  
  document.getElementById('txtDesc' + iteration).value=0;   
  document.getElementById('txtDesc' + iteration).style.visibility='hidden';
  document.getElementById('rcount').value=iteration; 
  
  var y=0;
//  alert(document.getElementById('txtTtl1').value);
	//alert(document.getElementById('txtTtl2').value);

  //alert(document.getElementById('rcount').value);

  for (var x = 1; x <= document.getElementById('rcount').value; x++)
   {
    y=1*y+1*document.getElementById('txtTtl'+x).value;
   }
   //alert(y);
 	var fvalue=Math.round(y*Math.pow(10,2))/Math.pow(10,2);
  document.getElementById('TotalAmt').innerHTML='$ ' + fvalue.toFixed(2);
   document.getElementById('tamount').value=fvalue.toFixed(2);
}
function removeRowFromTable()
{
  var tbl = document.getElementById('Table1');
  var lastRow = tbl.rows.length-1;
  if (lastRow > 2) 
  {
  tbl.deleteRow(lastRow);
  document.getElementById('rcount').value=lastRow-2;
  //alert(document.getElementById('rcount').value);
  //alert('txtQua'+document.getElementById('rcount').value);
//  document.getElementById('txtQua'+document.getElementById('rcount').value).value=0;
//  document.getElementById('txtAmt'+document.getElementById('rcount').value).value=0;
//  document.getElementById('txtTtl'+document.getElementById('rcount').value).value=0;  
  }
  var y=0;
  for (var x = 1; x <= document.getElementById('rcount').value; x++)
   {
    y=1*y+1*document.getElementById('txtTtl'+x).value;
   }
    var fvalue=Math.round(y*Math.pow(10,2))/Math.pow(10,2);
  document.getElementById('TotalAmt').innerHTML='$ ' + fvalue.toFixed(2);
   document.getElementById('tamount').value=fvalue.toFixed(2);
}


function roundNumber(num, dec) {
	var result = Math.round(num*Math.pow(10,dec))/Math.pow(10,dec);
	return result;
}
function openInNewWindow(frm)
{
  // open a blank window
  var aWindow = window.open('', 'TableAddRowNewWindow',
   'scrollbars=yes,menubar=yes,resizable=yes,toolbar=no,width=400,height=400');
   
  // set the target to the blank window
  frm.target = 'TableAddRowNewWindow';
  
  // submit
  frm.submit();
}
function validateRow(frm)
{
  var chkb = document.getElementById('chkValidate');
  if (chkb.checked) {
    var tbl = document.getElementById('Table1');
    var lastRow = tbl.rows.length - 1;
    var i;
    for (i=1; i<=lastRow; i++) {
      var aRow = document.getElementById('txtRow' + i);
      if (aRow.value.length <= 0) {
        alert('Row ' + i + ' is empty');
        return;
      }
    }
  }
  openInNewWindow(frm);
}
//-->
</script>
    <title>Update Unit Details</title>
    <link rel="stylesheet" type="text/css" href="../../App_Themes/Css/styles.css"/>
    <link rel="stylesheet" type="text/css" href="../../App_Themes/Css/Common.css"/>
</head>
<body>
    <form id="frmAcct" runat="server">
        <div id="body">
        <div id="cap"></div>
        <div id="main">
        <h1>Update Unit Details</h1>
        <asp:Panel ID="Panel1" runat="server" HorizontalAlign="Left">
            <div>
        <div style="text-align:left">
            <asp:Label ID="lblDisp" runat="server" CssClass="Title" ForeColor="#C00000"></asp:Label>
        </div>
        <br />
        <asp:Table ID="tblMain" runat="server" Width="500">
            <asp:TableRow ID="Row1" HorizontalAlign="Center" runat="server">
                <asp:TableCell ID="Cell1" HorizontalAlign="Right" runat="server" BorderStyle="None" >
                    <div style="text-align:right">
                        Account Name
                    </div>
                </asp:TableCell>
                <asp:TableCell HorizontalAlign="Left" BorderStyle="None">
                     <asp:DropDownList ID="DLAct" runat="server" CssClass="common" AutoPostBack="true"  >
                        <asp:ListItem Text="Select Account" value="" selected="True" ></asp:ListItem>
                     </asp:DropDownList>
                </asp:TableCell>
            </asp:TableRow>
        </asp:Table>
        <br />
        <asp:Table ID="Table3" runat="server" Width="250">
            <asp:TableRow>
                <asp:TableCell CssClass="alt5" HorizontalAlign="Right" >
                    <div style="text-align:right">
                        Billing 2X Per Month 
                    </div>
                </asp:TableCell>
                <asp:TableCell HorizontalAlign="Left" CssClass="alt5" >
                    <asp:DropDownList ID="DLCycle" runat="server" CssClass="common">
                        <asp:ListItem Text="Yes" Value="True"></asp:ListItem> 
                        <asp:ListItem Text="No" Value="False"></asp:ListItem> 
                    </asp:DropDownList>
                </asp:TableCell>
            </asp:TableRow> 
            <asp:TableRow ID="WTRow">
                <asp:TableCell HorizontalAlign="Right" CssClass="alt5" >
                    <div style="text-align:right">
                        By WorkType 
                    </div>
                </asp:TableCell>
                <asp:TableCell HorizontalAlign="Left" CssClass="alt5" >
                    <asp:DropDownList ID="DLWType" AutoPostBack="true" runat="server" CssClass="common">
                        <asp:ListItem Text="Yes" Value="True"></asp:ListItem> 
                        <asp:ListItem Text="No" Value="False"></asp:ListItem> 
                    </asp:DropDownList>
                </asp:TableCell>
            </asp:TableRow> 
            <asp:TableRow>
                <asp:TableCell HorizontalAlign="Right" CssClass="alt5">
                    <div style="text-align:right">
                        Minimum Billing 
                    </div>
                </asp:TableCell>
                <asp:TableCell   HorizontalAlign="Left" CssClass="alt5"  >
                    <asp:TextBox ID="TxMBilling" runat="server" Width="50" CssClass="common"></asp:TextBox>
                </asp:TableCell>
            </asp:TableRow> 
        </asp:Table>
        <br />
        <asp:Table ID="Table1" runat="server" Width="80%">
            <asp:TableRow ID="TableRow2" runat="server">
                <asp:TableCell ID="TableCell4" runat="server" ColumnSpan="4" CssClass="HeaderDiv">
                    VAS Product Details
                </asp:TableCell> 
            </asp:TableRow>
            <asp:TableRow ID="TableRow3" runat="server">
                <asp:TableCell ID="TableCell11" runat="server" CssClass="alt1" Width="55%">
                    Account Name
                </asp:TableCell> 
                <asp:TableCell ID="TableCell12" CssClass="alt1" runat="server" Width="15%">
                    Rate
                </asp:TableCell>
                <asp:TableCell ID="TableCell13" CssClass="alt1" runat="server" Width="15%">
                    Misc Rate
                </asp:TableCell> 
                <asp:TableCell ID="TableCell1" CssClass="alt1" runat="server" Width="15%">
                    Stat Rate
                </asp:TableCell> 
                <asp:TableCell ID="TableCell2" CssClass="alt1" runat="server" Width="15%">
                    UnitCount Method
                </asp:TableCell> 
             </asp:TableRow>
          </asp:Table>
          <asp:Table ID="Table4" runat="server" Width="80%">
            <asp:TableRow ID="TableRow1" HorizontalAlign="Center" runat="server">
        <asp:TableCell ID="TableCell3" CssClass="HeaderDiv" runat="server" ColumnSpan="5" >
            VAS Product Details
        </asp:TableCell> 
                </asp:TableRow>
       <asp:TableRow ID="TableRow4" runat="server">
        <asp:TableCell ID="TableCell5" runat="server"  Width="55%">
            Consultation    
        </asp:TableCell> 
        <asp:TableCell ID="TableCell6"  runat="server" Width="15%">
            <asp:TextBox ID="Consult" runat="server" Width="50" CssClass="common"    ></asp:TextBox>
        </asp:TableCell>
                   
      </asp:TableRow>
      <asp:TableRow ID="TableRow5" runat="server"  >
        <asp:TableCell ID="TableCell7" runat="server"  Width="55%">
            History And Physical    
        </asp:TableCell> 
        <asp:TableCell ID="TableCell8" runat="server" Width="15%">
            <asp:TextBox ID="HP" runat="server" Width="50" CssClass="common"     ></asp:TextBox>
        </asp:TableCell>
                   
      </asp:TableRow>
      <asp:TableRow ID="TableRow6" runat="server" >
        <asp:TableCell ID="TableCell9"  runat="server"  Width="55%">
            Discharge Summary   
        </asp:TableCell> 
        <asp:TableCell ID="TableCell10"  runat="server" Width="15%">
            <asp:TextBox ID="Discharge" runat="server" Width="50" CssClass="common"     ></asp:TextBox>
        </asp:TableCell>
                   
      </asp:TableRow>
      <asp:TableRow ID="TableRow7" runat="server" >
        <asp:TableCell ID="TableCell14"  runat="server" CssClass="common"  Width="55%">
            IME    
        </asp:TableCell> 
        <asp:TableCell ID="TableCell15"  runat="server" Width="15%">
            <asp:TextBox ID="IME" runat="server" Width="50" cssclass="common"    ></asp:TextBox>
        </asp:TableCell>
                   
      </asp:TableRow>
      <asp:TableRow ID="TableRow8" runat="server">
        <asp:TableCell ID="TableCell16" CssClass="common" runat="server"  Width="55%">
            Letter    
        </asp:TableCell> 
        <asp:TableCell ID="TableCell17"  CssClass="common" runat="server" Width="15%">
            <asp:TextBox ID="Letter" runat="server" Width="50"   CssClass="common" ></asp:TextBox>
        </asp:TableCell>
                   
      </asp:TableRow>
      <asp:TableRow ID="TableRow9" runat="server"  >
        <asp:TableCell ID="TableCell18"  CssClass="common" runat="server"  Width="55%">
            Progress Note    
        </asp:TableCell> 
        <asp:TableCell ID="TableCell19"  runat="server" Width="15%">
            <asp:TextBox ID="PrNote" runat="server" Width="50"   CssClass="common"  ></asp:TextBox>
        </asp:TableCell>
                   
      </asp:TableRow>
      <asp:TableRow ID="TableRow10" runat="server" >
        <asp:TableCell ID="TableCell20"   CssClass="common" runat="server"  Width="55%">
            Operative Note    
        </asp:TableCell> 
        <asp:TableCell ID="TableCell21"  CssClass="common" runat="server" Width="15%">
            <asp:TextBox ID="OpNote" runat="server" Width="50"   CssClass="common"  ></asp:TextBox>
        </asp:TableCell>
                   
      </asp:TableRow>
      <asp:TableRow ID="TableRow11" runat="server"  >
        <asp:TableCell ID="TableCell22" CssClass="common"  runat="server"  Width="55%">
            Psych Eval    
        </asp:TableCell> 
        <asp:TableCell ID="TableCell23"  runat="server" Width="15%">
            <asp:TextBox ID="PsychEval" runat="server" Width="50"   CssClass="common"></asp:TextBox>
        </asp:TableCell>
                   
      </asp:TableRow>
      <asp:TableRow ID="TableRow14" runat="server" >
        <asp:TableCell ID="TableCell28" CssClass="common"  runat="server"  Width="55%">
            Rate    
        </asp:TableCell> 
        <asp:TableCell ID="TableCell29"  runat="server" Width="15%">
            <asp:TextBox ID="Rate" runat="server" Width="50" CssClass="common"></asp:TextBox>
        </asp:TableCell>
                   
      </asp:TableRow>
      <asp:TableRow ID="TableRow12" runat="server" >
        <asp:TableCell ID="TableCell24" CssClass="common" runat="server"  Width="55%">
            STAT Rate    
        </asp:TableCell> 
        <asp:TableCell ID="TableCell25" CssClass="common"  runat="server" Width="15%">
            <asp:TextBox ID="StatRate" runat="server" Width="50"   CssClass="common"  ></asp:TextBox>
        </asp:TableCell>
                   
      </asp:TableRow>
      <asp:TableRow ID="TableRow13" runat="server">
        <asp:TableCell ID="TableCell26"  CssClass="common" runat="server"  Width="55%">
            UnitCount Method    
        </asp:TableCell> 
        <asp:TableCell ID="TableCell27"  runat="server" Width="15%">
            <asp:DropDownList ID="DLDefLC" AutoPostBack="true"   runat="server"  CssClass="common" >
            
            </asp:DropDownList>
        </asp:TableCell>
                   
      </asp:TableRow>
      </asp:Table>
      
        <asp:Table ID="Table2" runat="server" Width="80%">
        <asp:TableRow>
        <asp:TableCell HorizontalAlign="center" >
            <center>
                <asp:Button runat="server" ID="SubButton" Text="Submit"  CssClass="button"  />        
            </center>
        
              </asp:TableCell> </asp:TableRow> 
        </asp:Table>
        <asp:HiddenField ID="HRecExist" runat="server" />
        <asp:HiddenField ID="HRecCount" runat="server" />
        <asp:HiddenField ID="HMode" runat="server" />
        <asp:HiddenField ID="HAccountID" runat="server" />
    </div>
        </asp:Panel>
        </div> 
        </div> 
    </form>
</body>
</html>
