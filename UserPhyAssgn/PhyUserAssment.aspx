<%@ Page Language="VB" AutoEventWireup="false" CodeFile="PhyUserAssment.aspx.vb" Inherits="UserPhyAssgn_Default" EnableViewState="True" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 1.0 Transitional/EN">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
<link href= "../App_Themes/Css/styles.css" type="text/css" rel="stylesheet" />
    <title>Users-to-Dictators</title>
   <script language="JavaScript" type="text/javascript" >
   var newwindow;
function poptastic(inpt)
{
    url="removep.aspx?TrackID="+ inpt;
    //alert(inpt);
    
	newwindow=window.open(url,'name','height=100,width=400, left=300, top=100');
	if (window.focus) {newwindow.focus()}
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
		        	    highlightRow(document.form1.elements[i]);
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
  
	document.form1.ChkAll.checked=ChVal; 
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
    <div id="body">
    <div id="cap"></div>
    <div id="main">
    <h1>Users-to-Dictators</h1>
        <table id="MainTable" width="100%" >
        <tr>
                <td style="width: 100%; text-align: center; height: 15px;" valign="top" colspan ="2" class="HeaderDiv">
                    
               User Dictator Assignment</td>
               </tr>
                
            <tr>
                <td style="width: 50%; text-align: center;" valign="top">
                    <asp:Panel ID="Panel1" runat="server" Width="100%">
                        <table   width="100%" >
                        
                        
                            <tr  style="text-align: center">
                                <td colspan="2" style="text-align: center" class="SMSelected">
                                    User Search</td>
                            </tr>
                            <tr>
                                <td style="text-align: right;" >
                                    Username</td>
                                <td >
                                    <asp:TextBox ID="TxtUname" runat="server"></asp:TextBox></td>
                            </tr>
                           <tr>
                                <td style="text-align: right;"  >
                                    First name</td>
                                <td >
                                    <asp:TextBox ID="TxtFname" runat="server"></asp:TextBox></td>
                            </tr>
                           <tr>
                                <td style="text-align: right;"  >
                                    Last name</td>
                                <td >
                                    <asp:TextBox ID="TxtLname" runat="server"></asp:TextBox></td>
                            </tr>  
                            <tr>
                                <td style="text-align: center; height: 30px;" colspan="2">
                                    <asp:Button  CssClass="button"  ID="BtnSubmit1" runat="server" Text="Submit" UseSubmitBehavior="False" />&nbsp;
                                </td>
                            </tr>
                        </table>
                        </asp:Panel>
                    <asp:Panel ID="Panel2" runat="server" Width="100%"><asp:Table ID="UserTable" runat="server" BorderColor="Silver"   Width="100%" Font-Size="Small">
                       
                        <asp:TableRow runat="server" style="text-align: center">
                            <asp:TableCell runat="server"  cssclass="SMSelected">&nbsp;</asp:TableCell>
                            <asp:TableCell runat="server"  cssclass="SMSelected">First Name</asp:TableCell>
                            <asp:TableCell runat="server"  cssclass="SMSelected">Last Name</asp:TableCell>
                            <asp:TableCell runat="server"  cssclass="SMSelected">Username</asp:TableCell>
                        </asp:TableRow>
                        </asp:Table>
   
                    </asp:Panel>
                    <asp:Panel ID="Panel3" runat="server" Width="100%">
                    <asp:Table ID="TblUserstatus" Width="100%" runat="server"     >
                     
                        <asp:TableRow runat="server" style="text-align: center">
                            <asp:TableCell runat="server"  cssclass="SMSelected">&nbsp</asp:TableCell>
                            <asp:TableCell runat="server"  cssclass="SMSelected">User Role ( <asp:Label ID="lblname1" runat="server" Text="Label"  ForeColor="black" ></asp:Label> )</asp:TableCell>
                        </asp:TableRow>
                    </asp:Table>
                        </asp:Panel>
                    <asp:Panel ID="Panel4" runat="server" Width="100%"><asp:Table ID="Table2" runat="server" BorderColor="Silver"  Width="100%" Font-Size="Small">
                            <asp:TableRow runat="server"  style="text-align: center">
                            <asp:TableCell runat="server" cssclass="SMSelected">&nbsp</asp:TableCell>
                            <asp:TableCell runat="server" cssclass="SMSelected">User Role ( <asp:Label ID="lblname2" runat="server" Text="Label"   ForeColor="black"></asp:Label> )</asp:TableCell>
                        </asp:TableRow>
                    </asp:Table>
                        </asp:Panel>
                </td>
                <td style="text-align: center; width: 402px;" valign="top">
                    
                    <asp:Panel ID="PnlActSearch" runat="server" Width="100%">
                    <table   width="100%">
                    
                        <tr  style="text-align: center">
                            <td  class="SMSelected" colspan="2" style="text-align: center">
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
                                <asp:Button CssClass="button"  ID="BtnSubmit6" runat="server" Text="Submit" UseSubmitBehavior="False" /></td>
                        </tr>
                    </table>
                        </asp:Panel>
                    <asp:Panel ID="PnlActSelect" runat="server" Width="100%">
	<asp:Table ID="TblAccount" runat="server" BorderColor="Silver"   Width="100%" >
                       
                        <asp:TableRow ID="TableRow1" runat="server" style="text-align: center">
                            <asp:TableCell ID="TableCell1" runat="server"  cssclass="SMSelected">&nbsp</asp:TableCell>
                            <asp:TableCell ID="TableCell2" runat="server"  cssclass="SMSelected">Account Name</asp:TableCell>
                            <asp:TableCell ID="TableCell3" runat="server"  cssclass="SMSelected">Account Number</asp:TableCell>
                        </asp:TableRow>
                    </asp:Table>
                    </asp:Panel> 
                   
                    <asp:Panel ID="Panel6" runat="server" Width="100%">
                    <asp:Table ID="Table1" runat="server" BorderColor="Silver"  Width="100%" >
                        <asp:TableRow ID="TableRow3" runat="server" style="text-align: center">
                        <asp:TableCell ID="TableCell6" runat="server" ColumnSpan="5" style="text-align: center"    cssclass="SMSelected">
                            <asp:Label ID="lblActName2" ForeColor="black"  runat="server" ></asp:Label></asp:TableCell>
                        </asp:TableRow> 
                        <asp:TableRow runat="server" cssclass="SMSelected" style="text-align: center">
                            <asp:TableCell runat="server" cssclass="SMSelected">
                                <asp:CheckBox AutoPostBack="false" ID="ChkAll" runat="server" /></asp:TableCell>
                            <asp:TableCell runat="server"  cssclass="SMSelected">First Name</asp:TableCell>
                            <asp:TableCell runat="server"  cssclass="SMSelected">Last Name</asp:TableCell>
                            <asp:TableCell runat="server"  cssclass="SMSelected">Username</asp:TableCell>
                            <asp:TableCell runat="server" ID="DirCell"  cssclass="SMSelected"><asp:CheckBox AutoPostBack="false" ID="ChkDirect" Text="Direct"  runat="server" /></asp:TableCell> 
                        </asp:TableRow>
                    </asp:Table>
                    <br />
                    <asp:Table ID="Table5" runat="server" BorderColor="Silver"   Width="100%" >
                        <asp:TableRow ID="TableRow4" runat="server" cssclass="SMSelected" style="text-align: left">
                        <asp:TableCell ID="TableCell7" runat="server" ColumnSpan="5" >
                            <%--<asp:CheckBox ID="ChPhyAll" runat="server" />  --%>
                            <%--<asp:Label ID="LblAll"   runat="server" Text="Automatically assign new dictator added to this account."></asp:Label>--%>
                            </asp:TableCell>
                        </asp:TableRow> 
                       
                    </asp:Table>
                    
                    </asp:Panel>
                    <asp:Panel ID="Panel7" runat="server" Width="100%"><asp:Table ID="Table3" runat="server" BorderColor="Silver"  Width="100%" >
                    <asp:TableRow ID="TableRow2" runat="server" cssclass="SMSelected" style="text-align: center">
                        <asp:TableCell ID="TableCell5" runat="server" ColumnSpan="5" >
                            <asp:Label ID="lblActName1"  ForeColor="black"  runat="server"></asp:Label></asp:TableCell>
                        </asp:TableRow> 
                        <asp:TableRow runat="server" cssclass="SMSelected" style="text-align: center">
                            
                            <asp:TableCell runat="server">&nbsp</asp:TableCell>
                            <asp:TableCell runat="server">First Name</asp:TableCell>
                            <asp:TableCell runat="server">Last Name</asp:TableCell>
                            <asp:TableCell runat="server">Username</asp:TableCell>
                           
                        </asp:TableRow>
                    </asp:Table>
                        </asp:Panel>
                </td>
            </tr>
            <tr>
                <td colspan="2" style="text-align: center">
                    <asp:Button ID="BtnAssign" CssClass="button" runat="server" Text="Submit" OnClientClick="return ChkLvlPhy();" /></td>
            </tr>
            <tr>
                <td colspan="2" style="text-align: center">
                    <asp:Table ID="Table4" runat="server" BorderColor="Silver"  Width="60%" >
                     
                       
                    </asp:Table>
                </td>
            </tr>
        </table>
        <asp:Label ID="MsgDisp" runat="server" Font-Bold="True" 
            Font-Size="Small" ForeColor="#C00000" Height="16px" Width="496px"></asp:Label><br />
    
    </div>
        <asp:HiddenField ID="PrdState" runat="server" />
        <asp:HiddenField ID="PhyState" runat="server" />
        <asp:HiddenField ID="HUserID" runat="server" />
        <asp:HiddenField ID="HActID" runat="server" />
        <asp:HiddenField ID="hUname" runat="server" />
        <asp:HiddenField ID="HUEMail" runat="server" />
                <asp:HiddenField ID="HDirLevel" runat="server" />
        <asp:HiddenField ID="TotPhy" runat="server" />
        <asp:HiddenField ID="LvlAssgned" runat="server" />
                <asp:HiddenField ID="TotAct" runat="server" />
        <asp:HiddenField ID="TotLvl" runat="server" />
        <br />
        <br />
        <asp:Button ID="btnSubmit4" CssClass="button"  runat="server" Text="Submit" Visible="False" />
        &nbsp;&nbsp;&nbsp;
        <asp:Button ID="BtnSubmit5" CssClass="button" runat="server" Text="Submit" Visible="False" OnClientClick="return ChkLvl();" />
        <asp:Button ID="BtnSubmit7" CssClass="button" runat="server" Text="Submit" Visible="False" OnClientClick="return ChkAct();" />
        <asp:Button ID="btnsubmit3" CssClass="button" runat="server" Text="Submit" Visible="False" OnClientClick="return ChkPhy();" />
    </form>
</body>
</html>
