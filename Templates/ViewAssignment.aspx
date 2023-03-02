<%@ Page Language="VB" AutoEventWireup="false" CodeFile="ViewAssignment.aspx.vb" Inherits="Templates_ViewAssignment" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>View Template Assignments</title>
    <link href= "../App_Themes/Css/Main.css" type="text/css" rel="stylesheet" />    
    <link href= "../App_Themes/Css/Styles.css" type="text/css" rel="stylesheet" />    

   <script language="JavaScript" type="text/javascript" >
   var newwindow;
function poptastic(inpt)
{
    url="removep.aspx?TrackID="+ inpt;
    //alert(inpt);
    
	newwindow=window.open(url,'name','height=100,width=400, left=300, top=100');
	if (window.focus) {newwindow.focus()}
}

   function changeTmpAll() {
  // alert('document.form1.ChPhyAll.checked');
		if (document.form1.ChTmpAll.checked) {
			elval = true;
		} else
		 {
			elval = false;
			}
		for (var i=0;i<document.form1.elements.length;i++)
			{
			    //alert(document.form1.elements[i].name.substring(0,5)); 
			    if (document.form1.elements[i].name.substring(0,10) == 'TemplateID')
			       { 
			            
				        document.form1.elements[i].checked = elval;        
		        	    highlightRow(document.form1.elements[i]);
				    }
			}
}

 function changePhyAll() {
  // alert('document.form1.ChPhyAll.checked');
		if (document.form1.ChPhyAll.checked) {
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
		        	    highlightRow2(document.form1.elements[i]);
				    }
			}
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
				if (document.form1.elements[i].name.substring(0,10) == 'TemplateID'  && document.form1.elements[i].checked==false)
				{
				
				ChVal = false;
				//alert(ChVal);
				}
				}
	 }
     else
     {
     el.style.backgroundColor='#d7dbdd';
     document.form1.ChTmpAll.checked=false; 
      ChVal = false;
         for (var i=0;i<document.form1.elements.length;i++)
			{
			//alert(document.form1.elements[i].value);
				if (document.form1.elements[i].name.substring(0,10) == 'TemplateID' && document.form1.elements[i].checked == false)
				{
				
				ChVal = false;
				//alert(ChVal);
				}
				}

   }
  
	document.form1.ChTmpAll.checked=ChVal; 
//	if(ChVal = 'false')
//	{
//	 //alert(ChVal);   
//	document.form1.ChPhyAll.checked=ChVal; 
//	}

}

function highlightRow2(InputNode) {
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
     document.form1.ChPhyAll.checked=false; 
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
  
	document.form1.ChPhyAll.checked=ChVal; 
//	if(ChVal = 'false')
//	{
//	 //alert(ChVal);   
//	document.form1.ChPhyAll.checked=ChVal; 
//	}

}

function changeAllDirect() {
		if (document.form1.ChkDirect.checked) {
			elval = true;
		} else
		 {
			elval = false;
			}
		for (var i=0;i<document.form1.elements.length;i++)
			{
			    //alert(document.form1.elements[i].name.substring(0,6)); 
			    if (document.form1.elements[i].name.substring(0,6) == 'Direct')
			       { 
			            
				        document.form1.elements[i].checked = elval;        
		        	    highlightRow(document.form1.elements[i]);
				    }
			}
}
function highlightRowDir(InputNode) {
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
				if (document.form1.elements[i].name.substring(0,6) == 'Direct'  && document.form1.elements[i].checked==false)
				{
				
				ChVal = false;
				//alert(ChVal);
				}
				}
	 }
     else
     {
     el.style.backgroundColor='#d7dbdd';
     document.form1.ChPhyAll.checked=false; 
      ChVal = false;
         for (var i=0;i<document.form1.elements.length;i++)
			{
			//alert(document.form1.elements[i].value);
				if (document.form1.elements[i].name.substring(0,6) == 'Direct' && document.form1.elements[i].checked == false)
				{
				
				ChVal = false;
				//alert(ChVal);
				}
				}

   }
  
	document.form1.ChkDirect.checked=ChVal; 
//	if(ChVal = 'false')
//	{
//	 //alert(ChVal);   
//	document.form1.ChPhyAll.checked=ChVal; 
//	}

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
alert("Please select physician!");
return false;
}
}

function ChkAct()
{
//alert('Hello');
var J = 0;
//alert('Hello');
for (counter = 1; counter < document.form1.elements.length ; counter++)
{

if (document.form1.elements[counter].checked && document.form1.elements[counter].name.substring(0,5) == 'ActID')
{ 
J = J + 1; 
}
}
if (J == 0 )
{
alert("Please select account!");
return false;
}
}


function ChkTmp()
{
//alert('Hello');
var J = 0;
//alert('Hello');
for (counter = 1; counter < document.form1.elements.length ; counter++)
{

if (document.form1.elements[counter].checked && document.form1.elements[counter].name.substring(0,10) == 'TemplateID')
{ 
J = J + 1; 
}
}
if (J == 0 )
{
alert("Please select Template!");
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

if (document.form1.elements[counter].checked && document.form1.elements[counter].name.substring(0,10) == 'TemplateID')
{ 
J = J + 1; 
}
}

if (J == 0 )
{
alert("Please select Template!");
return false;
}

////alert('Hello');
//var i = 0;
////alert('Hello');
//for (counter = 1; counter < document.form1.elements.length ; counter++)
//{

//if (document.form1.elements[counter].checked && document.form1.elements[counter].name.substring(0,5) == 'PhyID')
//{ 
//i = i + 1; 
//}
//}

//if (i == 0 )
//{
//alert("Please select Physician!");
//return false;
//}
}
</script> 
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <table id="MainTable" width="100%" style="font-size: 10pt; font-family: 'Trebuchet MS'; font-style: italic; color:Gray; " cellpadding ="2" cellspacing ="2">
        
                <tr>
                <td style="width: 100%; text-align: center; height: 15px;" valign="top" colspan ="2" class="HeaderDiv"  >
                    <asp:Label ID="lblCaption" runat="server"></asp:Label>
                </td>
            </tr> 
            <tr>
                <td style="width: 50%; text-align: center;" valign="top">
                    <table cellpadding="2" cellspacing="2" width="449">
                        <tr>
                            <td class="alt1" style=" height: 21px">
                                Available Templates</td>
                            <td rowspan="2" style="background-color: #ffffff">
                                <asp:ImageButton ID="btnAdd" runat="server" ImageUrl="~/Images/right.jpg" />
                                <b></b>
                                <asp:ImageButton ID="btnRemove" runat="server" ImageUrl="~/Images/left.jpg" />
                            </td>
                            <td class="alt1" colspan="2" >
                                Assigned Templates
                            </td>
                           
                            
                        </tr>
                        <tr>
                            <td rowspan="4" style=" height: 312px">
                                <asp:ListBox ID="lstAvailTmps" runat="server" EnableViewState="True" Font-Names="Trebuchet MS"
                                    Font-Size="9" ForeColor="Firebrick" Height="299px" SelectionMode="Multiple" 
                                    ></asp:ListBox>
                            </td>
                            <td rowspan="3" style="height: 312px">
                                <b></b>
                                <asp:ListBox ID="lstAssignTmps" runat="server"  EnableViewState="True"
                                    Font-Names="Trebuchet MS" Font-Size="9" ForeColor="Firebrick" Height="299px"
                                    SelectionMode="Single" ></asp:ListBox>
                            </td>
                            <td rowspan="3" style="height: 312px">
                                <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/images/up.gif" />
                                <asp:ImageButton ID="ImageButton2" runat="server" ImageUrl="~/images/down.gif" /></td>
                            
                        </tr>
                    </table>
                   
                    
                </td>
            </tr>
            <tr>
                <td colspan="2" style="text-align: center">
                    <asp:Button ID="BtnAssign" runat="server" Text="Submit" CssClass="button" /></td>
            </tr>
            <%--<tr>
                <td colspan="2" style="text-align: center">
                    <asp:Table ID="Table4" runat="server" BorderColor="Silver" CellPadding="2" CellSpacing="2" Font-Italic="True" Font-Names="Trebuchet MS" ForeColor="DimGray" BorderWidth="2px" GridLines="Both" Width="70%" Font-Size="Small">
                     
                       
                    </asp:Table>
                </td>
            </tr>--%>
        </table>
        <asp:Label ID="MsgDisp" runat="server" Font-Bold="True" Font-Names="Trebuchet MS"
            Font-Size="Small" ForeColor="#C00000" Height="16px" Width="496px"></asp:Label><br />
    
    </div>
        <asp:HiddenField ID="PrdState" runat="server" />
        <asp:HiddenField ID="PhyState" runat="server" />
        <asp:HiddenField ID="HUserID" runat="server" />
        <asp:HiddenField ID="HActID" runat="server" />
        <asp:HiddenField ID="HTmpID" runat="server" />
        <asp:HiddenField ID="HPhyID" runat="server" />
        <asp:HiddenField ID="hUname" runat="server" />
        <asp:HiddenField ID="HUEMail" runat="server" />
                <asp:HiddenField ID="HDirLevel" runat="server" />
        <asp:HiddenField ID="TotPhy" runat="server" />
        <asp:HiddenField ID="LvlAssgned" runat="server" />
                <asp:HiddenField ID="TotAct" runat="server" />
        <asp:HiddenField ID="TotLvl" runat="server" />
        <br />
        <br />
        <asp:Button ID="btnSubmit4" runat="server" Text="Submit" Visible="False" />
        &nbsp;&nbsp;&nbsp;
        <asp:Button ID="BtnSubmit5" runat="server" Text="Submit" Visible="False"  />
        <asp:Button ID="BtnSubmit7" runat="server" Text="Submit" Visible="False"  />
        <asp:Button ID="BtnSubmit8" runat="server" Text="Submit" Visible="False"  />
        <asp:Button ID="btnsubmit3" runat="server" Text="Submit" Visible="False"  />
    </form>
</body>
</html>
