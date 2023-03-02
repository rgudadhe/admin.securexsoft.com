<%@ Page Language="VB" AutoEventWireup="false" CodeFile="ViewPhyAssment.aspx.vb" Inherits="UserPhyAssgn_Default" EnableViewState="True" %>





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
        <table id="MainTable" width="80%" style="font-size: 10pt; font-family: 'Trebuchet MS'; font-style: italic; color:Gray; " border ="2" cellpadding ="2" cellspacing ="2">
        <tr>
                <td style="width: 100%; text-align: center; height: 15px;" valign="top" colspan ="2" class="HeaderDiv">
                    <span style="font-family: Trebuchet MS; color: white;"><strong><em>MTDirect - View Dictator Assignment</em></strong></span></td>
               </tr>
                
            <tr>
                <td style="text-align: center;" valign="top">
                    
                    <asp:Panel ID="PnlActSearch" runat="server" Width="100%">
                    <table border="2" cellpadding="2" cellspacing ="2" style="color: gray; font-style: italic; " width="100%">
                    
                        <tr class="SMSelected" style="text-align: center">
                            <td colspan="2" style="text-align: center">
                                Account Search</td>
                        </tr>
                        <tr>
                            <td >
                                Account Name</td>
                            <td >
                                <asp:TextBox ID="TxtAname" runat="server"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td >
                                Account Number</td>
                            <td >
                                <asp:TextBox ID="TXtAnumber" runat="server"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td style="text-align: center; height: 30px;" colspan="2">
                                <asp:Button ID="BtnSubmit6" runat="server" Text="Submit" UseSubmitBehavior="False" /></td>
                        </tr>
                    </table>
                        </asp:Panel>
                    <asp:Panel ID="PnlActSelect" runat="server" Width="100%">
	<asp:Table ID="TblAccount" runat="server" BorderColor="Silver" CellPadding="2" CellSpacing="2" Font-Italic="True" Font-Names="Trebuchet MS" ForeColor="DimGray" BorderWidth="2px" GridLines="Both" Width="100%" Font-Size="Small">
                       
                        <asp:TableRow ID="TableRow1" runat="server" cssclass="SMSelected" style="text-align: center">
                            <asp:TableCell ID="TableCell1" runat="server"></asp:TableCell>
                            <asp:TableCell ID="TableCell2" runat="server">Account Name</asp:TableCell>
                            <asp:TableCell ID="TableCell3" runat="server">Account Number</asp:TableCell>
                        </asp:TableRow>
                    </asp:Table>
                    </asp:Panel> 
                   
                    <asp:Panel ID="Panel6" runat="server" Width="100%"><asp:Table ID="Table1" runat="server" BorderColor="Silver" CellPadding="2" CellSpacing="2" Font-Italic="True" Font-Names="Trebuchet MS" ForeColor="DimGray" BorderWidth="2px" GridLines="Both" Width="100%" Font-Size="Small">
                        
                        <asp:TableRow runat="server" cssclass="SMSelected" style="text-align: center">
                            <asp:TableCell runat="server">
                                <asp:CheckBox AutoPostBack="false" ID="ChkAll" runat="server" /></asp:TableCell>
                            <asp:TableCell runat="server">First Name</asp:TableCell>
                            <asp:TableCell runat="server">Last Name</asp:TableCell>
                            <asp:TableCell runat="server">Username</asp:TableCell>
                            
                        </asp:TableRow>
                    </asp:Table>
                    </asp:Panel>
                    <asp:Panel ID="Panel7" runat="server" Width="100%"><asp:Table ID="Table3" runat="server" BorderColor="Silver" CellPadding="2" CellSpacing="2" Font-Italic="True" Font-Names="Trebuchet MS" ForeColor="DimGray" BorderWidth="2px" GridLines="Both" Width="100%" Font-Size="Small">
                        
                        <asp:TableRow runat="server" cssclass="SMSelected" style="text-align: center">
                            <asp:TableCell runat="server"></asp:TableCell>
                            <asp:TableCell runat="server">First Name</asp:TableCell>
                            <asp:TableCell runat="server">Last Name</asp:TableCell>
                            <asp:TableCell runat="server">Username</asp:TableCell>
                           <asp:TableCell runat="server" ID="DirCell">Direct</asp:TableCell> 
                        </asp:TableRow>
                    </asp:Table>
                        </asp:Panel>
                </td>
            </tr>
            <tr>
                <td colspan="2" style="text-align: center">
                    <asp:Button ID="BtnAssign" runat="server" Text="Submit" OnClientClick="return ChkLvlPhy();" /></td>
            </tr>
            <tr>
                <td colspan="2" style="text-align: center">
                    <asp:Table ID="Table4" runat="server" BorderColor="Silver" CellPadding="2" CellSpacing="2" Font-Italic="True" Font-Names="Trebuchet MS" ForeColor="DimGray" BorderWidth="2px" GridLines="Both" Width="80%" Font-Size="Small">
                        
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
        <asp:HiddenField ID="hUname" runat="server" />
        <asp:HiddenField ID="TotPhy" runat="server" />
        <asp:HiddenField ID="HDirLevel" runat="server" />        
        <asp:HiddenField ID="LvlAssgned" runat="server" />
                <asp:HiddenField ID="TotAct" runat="server" />
        <asp:HiddenField ID="TotLvl" runat="server" />
        <br />
        <br />
        <asp:Button ID="btnSubmit4" runat="server" Text="Submit" Visible="False" />
        &nbsp;&nbsp;&nbsp;
        <asp:Button ID="BtnSubmit5" runat="server" Text="Submit" Visible="False" OnClientClick="return ChkLvl();" />
        <asp:Button ID="BtnSubmit7" runat="server" Text="Submit" Visible="False" />
        <asp:Button ID="btnsubmit3" runat="server" Text="Submit" Visible="False" OnClientClick="return ChkPhy();" />
    </form>
</body>
</html>
