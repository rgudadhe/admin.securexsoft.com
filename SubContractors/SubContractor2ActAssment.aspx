<%@ Page Language="VB" AutoEventWireup="false" CodeFile="SubContractor2ActAssment.aspx.vb" Inherits="UserPhyAssgn_Default" EnableViewState="True" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 1.0 Transitional/EN">

<html xmlns="http://www.w3.org/1999/xhtml" >

<head runat="server">
    <title>Accounts to Subcontractors</title>
    <link href= "../App_Themes/Css/styles.css" type="text/css" rel="stylesheet"/>   
    <link href= "../App_Themes/Css/Common.css" type="text/css" rel="stylesheet"/>   
         <script language="javascript" type="text/javascript"  >
   function changeAll() {
   var vmins1=0;
		if (document.form1.SelAct.checked) {
			elval = true;
			
		} else {
			elval = false;
		}
		for (var i=0;i<document.form1.elements.length;i++)
			{
			    document.form1.elements[i].checked = elval;
	    if (Left(document.form1.elements[i].name,7) == 'AccountID')
			{
			//alert('Hello');
			highlightRow(document.form1.elements[i]);
		
			
			}
			}
				
			}


function Left(str, n){
	if (n <= 0)
	    return "";
	else if (n > String(str).length)
	    return str;
	else
	    return String(str).substring(0,n);
}

function highlightRow(InputNode) {
	//alert(InputNode);
	var vmins1=0;
	var vjobs=0;
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
				if (Left(document.form1.elements[i].name,7) == 'AccountID' && document.form1.elements[i].checked==false)
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
				if (Left(document.form1.elements[i].name,7) == 'AccountID' && document.form1.elements[i].checked == false)
				{
				
				ChVal = false;
				//alert(ChVal);
				}
				
				}

   }
	document.form1.SelAct.checked=ChVal; 

}

   </script> 
</head>
<body>
    <form id="form1" runat="server">
        <div id="body">
        <div id="cap"></div>
        <div id="main">
        <h1>SubContractor - Accounts Assignment</h1>
            <asp:Panel ID="Panel4" runat="server" HorizontalAlign="Left">
                <div>
        <table id="MainTable" width="80%">
        <tr>
                <td style="width: 100%;" class="HeaderDiv" valign="top" colspan ="2">
                    SubContractor - Accounts Assignment
                </td>
               </tr>
                
            <tr>
                <td style="width: 50%; text-align: center;" valign="top">
                    <asp:Panel ID="Panel1" runat="server" Width="100%">
                        <table width="100%" >
                            <tr   style="text-align: center">
                                <td class="alt1" colspan="2" style="text-align: center">
                                   SubContractor Search
                                </td>
                            </tr>
                            <tr>
                                <td class="common">
                                    SubContractor Name
                                </td>
                                <td style="width: 3px">
                                    <asp:TextBox ID="TxtSubContractorname" runat="server" CssClass="common"></asp:TextBox></td>
                            </tr>
                            <tr >
                                <td style="text-align: center; height: 30px;" colspan="2">
                                    <asp:Button ID="BtnSubmit1" runat="server" Text="Submit" CssClass="button" UseSubmitBehavior="False" />&nbsp;
                                </td>
                            </tr>
                        </table>
                        </asp:Panel>
                    <asp:Panel ID="Panel2" runat="server" Width="100%">
                        <asp:Table ID="TblSubContractorSeach" runat="server"  Width="100%">
                        <asp:TableRow ID="TableRow1" runat="server" style="text-align: center">
                            <asp:TableCell ID="cell2" runat="server" ColumnSpan="2" CssClass="alt1" >SubContractor Name</asp:TableCell>
                        </asp:TableRow>
                       
                        </asp:Table>
   
                    </asp:Panel>
                    <asp:Panel ID="Panel3" runat="server" Width="100%">
                    <asp:Table ID="TblSubcontractorstatus" runat="server" Width="100%">
                        <asp:TableRow ID="TableRow2" runat="server" style="text-align: center">
                            <asp:TableCell ID="TableCell1" runat="server" cssclass="alt1"  ColumnSpan="1" style="text-align: center"><B>SubContractor Name</B></asp:TableCell>
                        </asp:TableRow>
                       
                    </asp:Table>
                        </asp:Panel>
                   
                </td>
                <td style="text-align: center" valign="top">
                    <asp:Panel  ID="Panel5" runat="server" Width="100%">
                    <table width="100%">
                    
                        <tr  style="text-align: center">
                            <td colspan="2" class="alt1" style="text-align: center">
                                <B>Account Search</B></td>
                        </tr>
                        <tr>
                            <td style="width: 7px" class="common">
                                Accountname</td>
                            <td style="width: 3px">
                                <asp:TextBox ID="TxtAname" CssClass="common" runat="server"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td style="text-align: center; height: 30px;" colspan="2">
                                <asp:Button ID="BtnSubmit2" runat="server" Text="Submit" CssClass="button" UseSubmitBehavior="False" /></td>
                        </tr>
                    </table>
                        </asp:Panel>
                    <asp:Panel ID="Panel6" runat="server" Width="100%">
                    <asp:Table ID="Table1" runat="server" Width="100%">
                      
                        <asp:TableRow ID="TableRow3" runat="server" style="text-align: center">
                            <asp:TableCell ID="TableCell2" runat="server" CssClass="alt1"> <input onclick="javascript:changeAll();" id="SelAct" type="checkbox" /></asp:TableCell>
                            <asp:TableCell ID="TableCell3" runat="server" cssclass="alt1">Account Name</asp:TableCell>
                            <asp:TableCell ID="TableCell4" runat="server" cssclass="alt1">Account Number</asp:TableCell>
                        </asp:TableRow>
                    </asp:Table>
                    </asp:Panel>
                    <asp:Panel ID="Panel7" runat="server" Width="100%">
                    <asp:Table ID="Table3" runat="server" Width="100%">
                      
                        <asp:TableRow ID="TableRow4" runat="server" style="text-align: center">
                            <asp:TableCell ID="TableCell5" runat="server" cssclass="alt1">&nbsp</asp:TableCell>
                            <asp:TableCell ID="TableCell6" runat="server" cssclass="alt1">Account Name</asp:TableCell>
                            <asp:TableCell ID="TableCell7" runat="server" cssclass="alt1">Account Number</asp:TableCell>
                        </asp:TableRow>
                    </asp:Table>
                        </asp:Panel>
                </td>
            </tr>
            <tr>
                <td colspan="2" style="text-align: center">
                    <asp:Button ID="BtnAssign" runat="server" CssClass="button" Text="Submit" /></td>
            </tr>
            <tr>
                <td colspan="2" style="text-align: center">
                    <asp:Table ID="Table4" runat="server" Width="100%">
                        <asp:TableRow ID="TableRow5" runat="server" style="text-align: center">
                            <asp:TableCell ID="TableCell8" runat="server" CssClass="alt1">SubContractor Name</asp:TableCell>
                            <asp:TableCell ID="TableCell9" runat="server" CssClass="alt1">Account Name</asp:TableCell>
                            <asp:TableCell ID="TableCell10" runat="server" CssClass="alt1">Account Number</asp:TableCell>
                        </asp:TableRow>
                    </asp:Table>
                </td>
            </tr>
        </table>
        <br />
        <asp:Label ID="DispBox" runat="server" CssClass="Title" ForeColor="#C00000"></asp:Label></div>
        <asp:HiddenField ID="SubContractorState" runat="server" />
        <asp:HiddenField ID="ActState" runat="server" />
        <asp:HiddenField ID="HSubcontractorID" runat="server" />
        <asp:HiddenField ID="hUname" runat="server" />
        <asp:HiddenField ID="TotAct" runat="server" />
        <asp:HiddenField ID="TotLvl" runat="server" />
        <br />
        <br />
        <asp:Button ID="btnSubmit4" runat="server" CssClass="button" Text="Submit" Visible="False" />
       &nbsp;&nbsp;
        <asp:Button ID="BtnSubmit5" runat="server" CssClass="button" Text="Submit" Visible="False" />
        <asp:Button ID="btnsubmit3" runat="server" CssClass="button" Text="Submit" Visible="False" />
            </asp:Panel>
    
        </div> 
        </div> 
    </form>
</body>
</html>
