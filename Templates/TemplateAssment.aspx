<%@ Page Language="VB" AutoEventWireup="false" CodeFile="TemplateAssment.aspx.vb" Inherits="Templates_TemplateAssment" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>Template Assignments</title>
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
    <div id="body">
    <div id="cap"></div>
    <div id="main">
    <h1>Assign Account-Template</h1>
    <div>
        <table id="MainTable" width="100%" style="font-size: 10pt; font-family: 'Trebuchet MS'; font-style: italic; color:Gray; " cellpadding ="2" cellspacing ="2">
        <tr>
                <td style="width: 100%; text-align: center; height: 15px;" valign="top" colspan ="2" class="HeaderDiv">
                    <span style="font-family: Trebuchet MS;"><strong><em>Template Assignment</em></strong></span></td>
               </tr>
                
            <tr>
                <td style="width: 30%; text-align: center;" valign="top">
                  
                        <asp:Panel ID="PnlActSearch" runat="server" Width="100%">
                    <table cellpadding="2" cellspacing ="2" style="color: gray; font-style: italic; " width="100%">
                    
                        <tr  style="text-align: center">
                            <td colspan="2" class="alt1" style="text-align: center">
                                Account Search</td>
                        </tr>
                        <tr>
                            <td >
                                Account Name</td>
                            <td >
                                <asp:TextBox ID="TxtAname" runat="server"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td style="text-align: center; height: 30px;" colspan="2">
                                <asp:Button ID="BtnSubmit6" runat="server" Text="Submit" UseSubmitBehavior="False" CssClass="button" /></td>
                        </tr>
                    </table>
                     <div style="text-align:left">        <asp:RegularExpressionValidator  Display="None"
    id="RegtxtAcc"  
    runat="server" 
    ControlToValidate="txtAName" 
    ValidationExpression="^[0-9a-zA-Z-%]+$"
    ErrorMessage="Account Name - Please enter valid input."
   />
</div> 
                        </asp:Panel>
                    <asp:Panel ID="PnlActSelect" runat="server" Width="100%">
	<asp:Table ID="TblAccount" runat="server" BorderColor="Silver" CellPadding="2" CellSpacing="2" Font-Italic="True" Font-Names="Trebuchet MS" ForeColor="DimGray" Width="100%" Font-Size="Small">
                       
                        <asp:TableRow ID="TableRow1" runat="server" style="text-align: center">
                            <asp:TableCell ID="TableCell1" cssclass="alt1" runat="server">&nbsp;</asp:TableCell>
                            <asp:TableCell ID="TableCell2" cssclass="alt1" runat="server">Account Name</asp:TableCell>
                            <asp:TableCell ID="TableCell3" cssclass="alt1" runat="server">Account Number</asp:TableCell>
                        </asp:TableRow>
                    </asp:Table>
                    </asp:Panel> 
                   
                    <asp:Panel ID="Panel6" runat="server" Width="100%" ScrollBars="Auto" Height="500">
                    <asp:Table ID="Table1" runat="server" CellPadding="2" CellSpacing="2" Font-Italic="True" Font-Names="Trebuchet MS" ForeColor="DimGray" Width="100%" Font-Size="Small">
                        <asp:TableRow ID="TableRow3" runat="server" cssclass="SMSelected" style="text-align: center">
                        <asp:TableCell ID="TableCell6" runat="server" ColumnSpan="5" >
                            <asp:Label ID="lblActName2" Font-Names="Trebuchet MS" ForeColor="white"  runat="server"></asp:Label></asp:TableCell>
                        </asp:TableRow> 
                        <asp:TableRow ID="TableRow2" runat="server" style="text-align: center">
                            <asp:TableCell ID="TableCell5" cssclass="alt1" runat="server">&nbsp;
                                </asp:TableCell>
                            <asp:TableCell ID="TableCell7" cssclass="alt1" runat="server">First Name</asp:TableCell>
                            <asp:TableCell ID="TableCell9" cssclass="alt1" runat="server">Last Name</asp:TableCell>
                            <asp:TableCell ID="TableCell10" cssclass="alt1" runat="server">Username</asp:TableCell>
                            
                        </asp:TableRow>
                    </asp:Table>
                    <br />
                    
                    
                    </asp:Panel>
                    <asp:Panel ID="Panel7" runat="server" Width="100%"><asp:Table ID="Table3" runat="server" BorderColor="Silver" CellPadding="2" CellSpacing="2" Font-Italic="True" Font-Names="Trebuchet MS" ForeColor="DimGray" BorderWidth="2px" GridLines="Both" Width="100%" Font-Size="Small">
                    <asp:TableRow ID="TableRow6" runat="server" cssclass="SMSelected" style="text-align: center">
                        <asp:TableCell ID="TableCell12" runat="server" ColumnSpan="5" >
                            <asp:Label ID="lblActName1" Font-Names="Trebuchet MS" ForeColor="white"  runat="server"></asp:Label></asp:TableCell>
                        </asp:TableRow> 
                        <asp:TableRow ID="TableRow7" runat="server" cssclass="SMSelected" style="text-align: center">
                            
                            <asp:TableCell ID="TableCell11" runat="server">
                                 </asp:TableCell>
                            <asp:TableCell ID="TableCell14" runat="server">First Name</asp:TableCell>
                            <asp:TableCell ID="TableCell15" runat="server">Last Name</asp:TableCell>
                            <asp:TableCell ID="TableCell16" runat="server">Username</asp:TableCell>
                           
                        </asp:TableRow>
                    </asp:Table>
                        </asp:Panel>
                </td>
                <td style="text-align: center; width: 70%;" valign="top">
                      
                    <iframe name="myframe" width="100%" height="500" ></iframe>
                </td>
            </tr>
            <tr>
                <td colspan="2" style="text-align: center">
                    <asp:Button ID="BtnAssign" runat="server" Text="Submit" OnClientClick="return ChkLvlPhy();" /></td>
            </tr>
            <tr>
                <td colspan="2" style="text-align: center">
                    <asp:Table ID="Table4" runat="server" BorderColor="Silver" CellPadding="2" CellSpacing="2" Font-Italic="True" Font-Names="Trebuchet MS" ForeColor="DimGray" BorderWidth="2px" GridLines="Both" Width="70%" Font-Size="Small">
                     
                       
                    </asp:Table>
                </td>
            </tr>
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
        <asp:Button ID="btnSubmit4" runat="server" Text="Submit" Visible="False" CssClass="button" />
        &nbsp;&nbsp;&nbsp;
        <asp:Button ID="BtnSubmit5" runat="server" Text="Submit" Visible="False" OnClientClick="return ChkLvl();" CssClass="button" />
        <asp:Button ID="BtnSubmit7" runat="server" Text="Submit" Visible="False" OnClientClick="return ChkAct();" CssClass="button" />
        <asp:Button ID="BtnSubmit8" runat="server" Text="Submit" Visible="False" OnClientClick="return ChkTmp();" CssClass="button" />
        <asp:Button ID="btnsubmit3" runat="server" Text="Submit" Visible="False" OnClientClick="return ChkPhy();" CssClass="button" />
        </div> 
        </div> 
      <asp:ValidationSummary ID="ValidationSummary1" runat="server" HeaderText="Following error occurs:" ShowMessageBox="true" DisplayMode="BulletList" ShowSummary="False" />   

    </form>
</body>
</html>
