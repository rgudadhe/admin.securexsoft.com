<%@ Page Language="VB" AutoEventWireup="false" CodeFile="ViewPhyAssment.aspx.vb" Inherits="UserPhyAssgn_Default" EnableViewState="True" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 1.0 Transitional/EN">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>View Dictator Assignment</title>
    <link href= "../App_Themes/Css/styles.css" type="text/css" rel="stylesheet" /> 
    <link href= "../App_Themes/Css/Common.css" type="text/css" rel="stylesheet" /> 
    
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
        <div id="body">
        <div id="cap"></div>
        <div id="main">
        <h1>View/Edit Dictator Assignment</h1>
            <asp:Panel ID="Panel1" runat="server" HorizontalAlign="Left">
                <div>
        <table id="MainTable" width="80%">
        <tr>
                <td style="width: 100%; text-align: center; height: 15px;" valign="top" colspan ="2" class="HeaderDiv">
                    View Dictator Assignment
                </td>
               </tr>
                
            <tr>
                <td style="text-align: center;" valign="top">
                    
                    <asp:Panel ID="PnlActSearch" runat="server" Width="100%">
                    <table width="100%">
                    
                        <tr  style="text-align: center">
                            <td colspan="2" style="text-align: center" class="alt1">
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
                                <asp:Button CssClass="button"  ID="BtnSubmit6" runat="server" Text="Submit" UseSubmitBehavior="False" /></td>
                        </tr>
                    </table>
                        </asp:Panel>
                    <asp:Panel ID="PnlActSelect" runat="server" Width="100%">
	                    <asp:Table ID="TblAccount" runat="server"  Width="100%" >
                       
                        <asp:TableRow ID="TableRow1" runat="server" style="text-align: center">
                            <asp:TableCell ID="TableCell1" CssClass="alt1" runat="server">&nbsp</asp:TableCell>
                            <asp:TableCell ID="TableCell2" CssClass="alt1" runat="server">Account Name</asp:TableCell>
                            <asp:TableCell ID="TableCell3" CssClass="alt1" runat="server">Account Number</asp:TableCell>
                        </asp:TableRow>
                    </asp:Table>
                    </asp:Panel> 
                   
                    <asp:Panel ID="Panel6" runat="server" Width="100%"><asp:Table ID="Table1" runat="server" Width="100%">
                        
                        <asp:TableRow ID="TableRow2" runat="server" style="text-align: center">
                            <asp:TableCell ID="TableCell4" runat="server" CssClass="alt1">
                                <asp:CheckBox AutoPostBack="false" ID="ChkAll" runat="server" /></asp:TableCell>
                            <asp:TableCell ID="TableCell5" CssClass="alt1" runat="server">First Name</asp:TableCell>
                            <asp:TableCell ID="TableCell6" CssClass="alt1" runat="server">Last Name</asp:TableCell>
                            <asp:TableCell ID="TableCell7" CssClass="alt1" runat="server">Username</asp:TableCell>
                            
                        </asp:TableRow>
                    </asp:Table>
                    </asp:Panel>
                    <asp:Panel ID="Panel7" runat="server" Width="100%"><asp:Table ID="Table3" runat="server" BorderColor="Silver" CellPadding="2" CellSpacing="2" ForeColor="DimGray" BorderWidth="2px" GridLines="Both" Width="100%" Font-Size="Small">
                        
                        <asp:TableRow ID="TableRow3" runat="server" style="text-align: center">
                            <asp:TableCell ID="TableCell8" CssClass="alt1" runat="server">&nbsp</asp:TableCell>
                            <asp:TableCell ID="TableCell9" CssClass="alt1" runat="server">First Name</asp:TableCell>
                            <asp:TableCell ID="TableCell10" CssClass="alt1" runat="server">Last Name</asp:TableCell>
                            <asp:TableCell ID="TableCell11" CssClass="alt1" runat="server">Username</asp:TableCell>
                           <asp:TableCell runat="server" ID="DirCell">Direct</asp:TableCell> 
                        </asp:TableRow>
                    </asp:Table>
                        </asp:Panel>
                </td>
            </tr>
            <tr>
                <td colspan="2" style="text-align: center">
                    <asp:Button CssClass="button"  ID="BtnAssign" runat="server" Text="Submit" OnClientClick="return ChkLvlPhy();" /></td>
            </tr>
            <tr>
                <td colspan="2" style="text-align: center">
                    <asp:Table ID="Table4" runat="server"  Width="80%">
                        
                    </asp:Table>
                </td>
            </tr>
        </table>
        <asp:Label ID="MsgDisp" runat="server" ForeColor="#C00000" Height="16px" Width="496px" CssClass="Title"></asp:Label><br />
    
    </div>
        <asp:HiddenField ID="hdnSelActName" runat="server" />
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
        <asp:Button CssClass="button"  ID="btnSubmit4" runat="server" Text="Submit" Visible="False" />
        &nbsp;&nbsp;&nbsp;
        <asp:Button CssClass="button"  ID="BtnSubmit5" runat="server" Text="Submit" Visible="False" OnClientClick="return ChkLvl();" />
        <asp:Button CssClass="button"  ID="BtnSubmit7" runat="server" Text="Submit" Visible="False" />
        <asp:Button CssClass="button"  ID="btnsubmit3" runat="server" Text="Submit" Visible="False" OnClientClick="return ChkPhy();" />

            </asp:Panel>
    
        </div> 
        </div> 
    </form>
</body>
</html>
