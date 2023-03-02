<%@ Page Language="VB" AutoEventWireup="false" CodeFile="UserAccountAssment.aspx.vb" Inherits="UserPhyAssgn_Default" EnableViewState="True" %>

<%@ Register Assembly="System.Web.Extensions, Version=1.0.61025.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI" TagPrefix="asp" %>



<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 1.0 Transitional/EN">
<LINK href= "../../styles/Default.css" type="text/css" rel="stylesheet">
<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Untitled Page</title>
    <script language="JavaScript" type="text/javascript" >
   
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
			    if (document.form1.elements[i].name.substring(0,9) == 'AccountID')
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
				if (document.form1.elements[i].name.substring(0,9) == 'AccountID'  && document.form1.elements[i].checked==false)
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
				if (document.form1.elements[i].name.substring(0,9) == 'AccountID' && document.form1.elements[i].checked == false)
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

   function ChkAct()
{
//alert('Hello');
var J = 0;
//alert('Hello');
for (counter = 1; counter < document.form1.elements.length ; counter++)
{

if (document.form1.elements[counter].checked && document.form1.elements[counter].name.substring(0,9) == 'AccountID')
{ 
J = J + 1; 
}
}

if (J == 0 )
{
alert("Please select Account!");
return false;
}
}


 function ChkLvlAct()
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

if (document.form1.elements[counter].checked && document.form1.elements[counter].name.substring(0,9) == 'AccountID')
{ 
i = i + 1; 
}
}

if (i == 0 )
{
alert("Please select Account!");
return false;
}
}
</script>  
</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager> 
    <div>
        <table id="MainTable" width="100%" style="font-size: 10pt; font-family: 'Trebuchet MS'; font-style: italic; color:Gray; " border ="2" cellpadding ="2" cellspacing ="2">
        <tr>
                <td style="width: 100%; text-align: center;" valign="top" colspan ="2" class="HeaderDiv">
                    <span style="font-family: Trebuchet MS; color: white;"><strong><em>
               User Account Assignment</em></strong></span></td>
               </tr>
                
            <tr>
                <td style="width: 50%; text-align: center;" valign="top">
                    <asp:Panel ID="Panel1" runat="server" Width="100%">
                        <table border="2" cellpadding="2" cellspacing ="2" style="color: gray; font-style: italic; font-family: 'Trebuchet MS'" width="100%" >
                        
                        
                            <tr class="SMSelected" style="text-align: center">
                                <td colspan="2" style="text-align: center">
                                    User Search</td>
                            </tr>
                            <tr>
                                <td style="width: 3px">
                                    Username</td>
                                <td style="width: 3px">
                                    <asp:TextBox ID="TxtUname" runat="server"></asp:TextBox></td>
                                    
                                    <ajaxToolkit:AutoCompleteExtender 
        ID="AutoCompleteExtender1" 
        runat="server" 
        TargetControlID="TxtUname"
        ServicePath="../AutoComplete.asmx"
        ServiceMethod="GetUserID"
        
                MinimumPrefixLength="1" 
                CompletionInterval="1"
                EnableCaching="true"
                CompletionSetCount="20"
             
                DelimiterCharacters=";, :">
                 </ajaxToolkit:AutoCompleteExtender>
                            </tr>
                            <tr>
                                <td style="text-align: center; height: 30px;" colspan="2">
                                    <asp:Button ID="BtnSubmit1" runat="server" Text="Submit" UseSubmitBehavior="False" />&nbsp;
                                </td>
                            </tr>
                        </table>
                        </asp:Panel>
                    <asp:Panel ID="Panel2" runat="server" Width="100%"><asp:Table ID="UserTable" runat="server" BorderColor="Silver" CellPadding="2" CellSpacing="2" Font-Italic="True" Font-Names="Trebuchet MS" ForeColor="DimGray" BorderWidth="2px" GridLines="Both" Width="100%" Font-Size="Small">
                       
                        <asp:TableRow runat="server" cssclass="SMSelected" style="text-align: center">
                            <asp:TableCell runat="server"></asp:TableCell>
                            <asp:TableCell runat="server">First Name</asp:TableCell>
                            <asp:TableCell runat="server">Last Name</asp:TableCell>
                            <asp:TableCell runat="server">Username</asp:TableCell>
                        </asp:TableRow>
                        </asp:Table>
   
                    </asp:Panel>
                    <asp:Panel ID="Panel3" runat="server" Width="100%"><asp:Table ID="TblUserstatus" runat="server" BorderColor="Silver" CellPadding="2" CellSpacing="2" Font-Italic="True" Font-Names="Trebuchet MS" ForeColor="DimGray" BorderWidth="2px" GridLines="Both" Width="100%" Font-Size="Small">
                       
                        <asp:TableRow runat="server" cssclass="SMSelected" style="text-align: center">
                            <asp:TableCell runat="server"></asp:TableCell>
                            <asp:TableCell runat="server">Production Level</asp:TableCell>
                        </asp:TableRow>
                    </asp:Table>
                        </asp:Panel>
                    <asp:Panel ID="Panel4" runat="server" Width="100%"><asp:Table ID="Table2" runat="server" BorderColor="Silver" CellPadding="2" CellSpacing="2" Font-Italic="True" Font-Names="Trebuchet MS" ForeColor="DimGray" BorderWidth="2px" GridLines="Both" Width="100%" Font-Size="Small">
                        
                        <asp:TableRow runat="server" cssclass="SMSelected" style="text-align: center">
                            <asp:TableCell runat="server"></asp:TableCell>
                            <asp:TableCell runat="server">Production Level</asp:TableCell>
                        </asp:TableRow>
                    </asp:Table>
                        </asp:Panel>
                </td>
                <td style="text-align: center" valign="top">
                    <asp:Panel ID="Panel5" runat="server" Width="100%">
                    <table border="2" cellpadding="2" cellspacing ="2" style="color: gray; font-style: italic; " width="100%">
                    
                        <tr class="SMSelected" style="text-align: center">
                            <td colspan="2" style="text-align: center">
                                Account Search</td>
                        </tr>
                        <tr>
                            <td style="width: 7px">
                                Accountname</td>
                            <td style="width: 3px">
                                <asp:TextBox ID="TxtAname" runat="server"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td style="text-align: center; height: 30px;" colspan="2">
                                <asp:Button ID="BtnSubmit2" runat="server" Text="Submit" UseSubmitBehavior="False" /></td>
                        </tr>
                    </table>
                        </asp:Panel>
                    <asp:Panel ID="Panel6" runat="server" Width="100%"><asp:Table ID="Table1" runat="server" BorderColor="Silver" CellPadding="2" CellSpacing="2" Font-Italic="True" Font-Names="Trebuchet MS" ForeColor="DimGray" BorderWidth="2px" GridLines="Both" Width="100%" Font-Size="Small">
                       
                        <asp:TableRow runat="server" cssclass="SMSelected" style="text-align: center">
                            <asp:TableCell runat="server">   <asp:CheckBox AutoPostBack="false" ID="ChkAll" runat="server" /></asp:TableCell>
                            <asp:TableCell runat="server">Account Name</asp:TableCell>
                            <asp:TableCell runat="server">Account Number</asp:TableCell>
                        </asp:TableRow>
                    </asp:Table>
                    </asp:Panel>
                    <asp:Panel ID="Panel7" runat="server" Width="100%"><asp:Table ID="Table3" runat="server" BorderColor="Silver" CellPadding="2" CellSpacing="2" Font-Italic="True" Font-Names="Trebuchet MS" ForeColor="DimGray" BorderWidth="2px" GridLines="Both" Width="100%" Font-Size="Small">
                      
                        <asp:TableRow runat="server" cssclass="SMSelected" style="text-align: center">
                            <asp:TableCell runat="server"></asp:TableCell>
                            <asp:TableCell runat="server">Account Name</asp:TableCell>
                            <asp:TableCell runat="server">Account Number</asp:TableCell>
                        </asp:TableRow>
                    </asp:Table>
                        </asp:Panel>
                </td>
            </tr>
            <tr>
                <td colspan="2" style="text-align: center">
                    <asp:Button ID="BtnAssign" runat="server" Text="Submit" OnClientClick="return ChkLvlAct();" /></td>
            </tr>
            <tr>
                <td colspan="2" style="text-align: center">
                    <asp:Table ID="Table4" runat="server" BorderColor="Silver" CellPadding="2" CellSpacing="2" Font-Italic="True" Font-Names="Trebuchet MS" ForeColor="DimGray" BorderWidth="2px" GridLines="Both" Width="100%" Font-Size="Small">
                        <asp:TableRow runat="server" cssclass="SMSelected" style="text-align: center">
                            <asp:TableCell runat="server">User Name</asp:TableCell>
                            <asp:TableCell runat="server">Level</asp:TableCell>
                            <asp:TableCell runat="server">Account Name</asp:TableCell>
                            <asp:TableCell runat="server">Account Number</asp:TableCell>
                        </asp:TableRow>
                    </asp:Table>
                </td>
            </tr>
        </table>
    
    </div>
        <asp:HiddenField ID="PrdState" runat="server" />
        <asp:HiddenField ID="ActState" runat="server" />
        <asp:HiddenField ID="HUserID" runat="server" />
        <asp:HiddenField ID="hUname" runat="server" />
        <asp:HiddenField ID="TotAct" runat="server" />
        <asp:HiddenField ID="TotLvl" runat="server" />
        <br />
        <br />
        <asp:Button ID="btnSubmit4" runat="server" Text="Submit" Visible="False" />
        &nbsp;&nbsp;&nbsp;
        <asp:Button ID="BtnSubmit5" runat="server" Text="Submit" Visible="False" OnClientClick="return ChkLvl();" />
        <asp:Button ID="btnsubmit3" runat="server" Text="Submit" Visible="False" OnClientClick="return ChkAct();" />
    </form>
</body>
</html>
