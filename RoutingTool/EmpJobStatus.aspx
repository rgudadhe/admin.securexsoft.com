<%@ Page Language="VB" AutoEventWireup="false" CodeFile="EmpJobStatus.aspx.vb" Inherits="RoutingTool_Default" EnableViewState="false"  %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 1.0 Transitional/EN">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
<style type="text/css" media="screen">

/*====================================================
	- HTML Table Filter stylesheet
=====================================================*/
@import "filtergrid.css";

/*====================================================
	- General html elements
=====================================================*/
body{ 
	margin:15px; padding:15px; border:1px solid #666;
	font-family:Arial, Helvetica, sans-serif; font-size:88%; 
}
h2{ margin-top: 50px; }
caption{ margin:10px 0 0 5px; padding:10px; text-align:left; }
pre{ font-size:13px; margin:5px; padding:5px; background-color:#f4f4f4; border:1px solid #ccc;  }
.mytable{
	width:100%; font-size:12px;
	border:1px solid #ccc;
}
th{ background-color:#003366; color:#FFF; padding:2px; border:1px solid #ccc; }
td{ padding:2px; border-bottom:1px solid #ccc; border-right:1px solid #ccc; }
</style>
<script language="javascript" type="text/javascript" src="tablefilter.js"></script>
<link href= "../../styles/Default.css" type="text/css" rel="stylesheet"/>
    <title>Untitled Page</title>
      <script language="javascript" type="text/javascript"  >
   function changeAll() {
   var vmins1=0;
		if (document.form1.SelJob.checked) {
		    
			elval = true;
			
		} else {
			elval = false;
		}
		for (var i=0;i<document.form1.elements.length;i++)
			{
			if (document.form1.elements[i].parentNode.parentNode.style.display=='')
			{
			    document.form1.elements[i].checked = elval;
	    if (Left(document.form1.elements[i].name,7) == 'TransID')
			{
			//alert('Hello');
			highlightRow(document.form1.elements[i]);
		
			
			}
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
//     el.style.backgroundColor='#EBDDE2';
     ChVal = true;
     for (var i=0;i<document.form1.elements.length;i++)
			{
			//alert(document.form1.elements[i].value);
				if (Left(document.form1.elements[i].name,7) == 'TransID' && document.form1.elements[i].checked==false)
				{
				
				ChVal = false;
				//alert(ChVal);
				}
				//alert(document.form1.elements[i].nodeName.style.visibility);
				if (Left(document.form1.elements[i].name,7) == 'TransID' && document.form1.elements[i].checked==true )
				{
				//alert(document.form1.elements[i].parentNode.parentNode.style.display);
				//alert(document.form1.elements[i].parentNode.parentNode.style.visibility);
				//alert(document.form1.elements[i].name + ':' + document.form1.elements[i].style.display); 
                var vmins = document.form1.elements[i].value.split('#');
				var vmins2 = vmins[1];
				vmins1 = parseFloat(vmins1) + parseInt(vmins2);
				vjobs++;
				
				//alert(vmins1);
				}
				
				}
	 }
     else
     {
//     el.style.backgroundColor='#FFFFFF';
      ChVal = false;
         for (var i=0;i<document.form1.elements.length;i++)
			{
			//alert(document.form1.elements[i].value);
				if (Left(document.form1.elements[i].name,7) == 'TransID' && document.form1.elements[i].checked == false)
				{
				
				ChVal = false;
				//alert(ChVal);
				}
				if (Left(document.form1.elements[i].name,7) == 'TransID' && document.form1.elements[i].checked==true)
				{
				
                var vmins = document.form1.elements[i].value.split('#');
				var vmins2 = vmins[1];
				vmins1 = parseFloat(vmins1) + parseInt(vmins2);
				vjobs++;
				//alert(vmins1);
				}
				}

   }
   document.getElementById("lblmins").innerHTML=Math.round((vmins1/60));
   document.getElementById("lbljobs").innerHTML=vjobs;
   //alert(ChVal);   
	document.form1.SelJob.checked=ChVal; 

}


function unselect()
{
for (var i=0;i<document.form1.elements.length;i++)
		{
		    document.form1.elements[i].checked = false;
	    	}
}

      </script>
 <style>
<!--
.TableR
{
   padding:2px; border-right:4px solid #afb5b8; border-top:4px solid #fff; background:#FEFDFE; border-left:4px solid #fff; COLOR: #000;
    BORDER-BOTTOM: 4px solid #afb5b8;
    TEXT-ALIGN: center;
    TEXT-TRANSFORM: uppercase;
    }
.noScroll 
{ 
     position:relative; 
     top:expression(this.offsetParent.scrollTop); 
} 
.CommRow
{ 
	
    padding:2px; border-right:1px solid #afb5b8; border-top:1px solid #fff; background:#B32D00; border-left:1px solid #fff;
    BORDER-BOTTOM: 1px solid #afb5b8;    
   TEXT-TRANSFORM: uppercase;

    COLOR: #000000;
    TEXT-ALIGN: center;
} 

.TDNormal{style="border-left-stylesolid"; border-left-width:1; border-right-style:solid; border-right-width:1; border-bottom-style:solid; border-bottom-width:1; font-family:Trebuchet MS; font-variant:normal; font-size:8pt; color="#000080";text-transform:capitalize;}
.TDComm{style="border-left-stylesolid"; border-left-width:1; border-right-style:solid; border-right-width:1; border-bottom-style:solid; border-bottom-width:1; font-family:Trebuchet MS; font-variant:normal; font-size:8pt; text-transform:capitalize;}


a:link.nav   { font-family: Arial,San Serif; font-size: 11px; text-decoration: none }
a:visited.nav { font-family: Arial,San Serif; font-size: 11px; text-decoration: none }
a:active.nav { font-family: Arial,San Serif; font-size: 11px; text-decoration: none }
a:hover.nav  { font-family: Arial,San Serif; font-size: 11px; text-decoration: none; color: #EBDDE2 }
.HEADING     { cursor: hand; font-family: Arial,San Serif; font-size: 12px; color: #333333; 
               background-color: #616D7E; font-weight: none; 
               border: 1px solid #000000;  }
.LINKSOFF    { display: none; font-family: Arial,San Serif; font-size: 12px; color: #000080 }
.LINKSON     { display: inline; font-family: Arial,San Serif; font-size: 12px; color: #000080 }
input.btnhov{
   border-top-color:#c63;
   border-left-color:#c63;
   border-right-color:#930;
   border-bottom-color:#930;}
input.btn{
   color:#050;
   font-family:'trebuchet ms',helvetica,sans-serif;
   font-size:10px;
   font-weight:bold;
   background-color:#fed;
   border:1px solid;
   border-top-color:#696;
   border-left-color:#696;
   border-right-color:#363;
   border-bottom-color:#363;
   width:100px;
   height:18px;   
   filter:progid:DXImageTransform.Microsoft.Gradient      (GradientType=0,StartColorStr='#ffffffff',EndColorStr='#ffeeddaa')
-->
</style>

</head>
<body style="margin-top: 0px; padding-top: 0px; background-color: white"  onload="unselect();" >
   <form id="form1" runat="server">
      
            <table id="Table1" border="2" cellpadding="2" cellspacing="2" style="font-size: 10pt;
                font-style: italic; font-family: 'Trebuchet MS'; background-color: ivory;" width="100%"  class="noScroll">
                <tr class="noScroll">
                    <td class="HeaderDiv" colspan="2" style="width: 100%; text-align: center" valign="top">
                        <span style="color: white; font-family: Trebuchet MS"><strong><em>Routing Tool</em></strong></span></td>
                </tr>
               <tr>
               <td>
               <table id="Table3" border="2" cellpadding="2" cellspacing="2" style="font-size: 10pt;
                font-style: italic; font-family: 'Trebuchet MS'" width="100%">
               <tr>
               <td style="width: 15%" >
                   Account Name: -
               </td>
                   <td  style="width: 35%" >
                       <asp:Label ID="LblActName" runat="server" Font-Bold="True" Font-Names="Trebuchet MS"
                           Font-Size="Small" ForeColor="#FF8000"></asp:Label></td>
                   <td style="width: 15%" >
                        Total Mins</td>
                   <td style="width: 35%" >
                       <asp:Label ID="LblTotmins" runat="server" Font-Bold="True" Font-Names="Trebuchet MS"
                           Font-Size="Small" ForeColor="#FF8000"></asp:Label></td>
               </tr>
                <tr>
                    <td style="height: 24px" >
                       Status</td>
                    <td style="height: 24px">
                        <asp:Label ID="Lblstatus" runat="server" Font-Bold="True" Font-Names="Trebuchet MS"
                            Font-Size="Small" ForeColor="#FF8000"></asp:Label></td>
                    <td style="height: 24px">
                        Pending Mins</td>
                    <td style="height: 24px">
                        <asp:Label ID="LblPendMins" runat="server" Font-Bold="True" Font-Names="Trebuchet MS"
                            Font-Size="Small" ForeColor="#FF8000"></asp:Label></td>
               </tr>
               </Table> </td></tr> 
                <tr>
                    <td style="text-align: left" valign="top">
                         <asp:Table ID="Table2"  runat="server" GridLines="Both"   BorderColor="Silver" style="text-align: center" Font-Italic="True" Font-Names="Trebuchet MS" ForeColor="DimGray" Width="100%" Font-Size="Small" >
                            <asp:TableRow ID="TableRow1"   runat="server" BackColor="gray" ForeColor="white"    style="text-align: center" VerticalAlign="Top">
                                <asp:TableCell ID="TableCell13" runat="server">User Name</asp:TableCell >
                                <asp:TableCell ID="TableCell14"  runat="server">User ID</asp:TableCell >
                                <asp:TableCell ID="TableCell15"  runat="server">SchMins</asp:TableCell >
                                 <asp:TableCell ID="TableCell16" runat="server">STime</asp:TableCell >
                                 <asp:TableCell ID="TableCell17"  runat="server">ETime</asp:TableCell >
                                <asp:TableCell runat="server"  ID="CellMdone" ></asp:TableCell >
                                <asp:TableCell  runat="server" ID="CellCout" ></asp:TableCell >
                                <asp:TableCell ID="TableCell18"  runat="server" >Pending</asp:TableCell >
                                <asp:TableCell ID="TableCell19"   runat="server">Direct Mins</asp:TableCell >
                            </asp:TableRow> 

                            
                    
                            </asp:Table>
                        <br />
                        <strong>Selected Mins : &nbsp;</strong><asp:Label ID="lblmins" runat="server" Font-Bold="True"
                            Font-Italic="False" Font-Names="Trebuchet MS" Font-Size="Small" ForeColor="#C00000"></asp:Label>
                        &nbsp; &nbsp; &nbsp; <strong>Selected Jobs : &nbsp;</strong><asp:Label ID="lbljobs"
                            runat="server" Font-Bold="True" Font-Italic="False" Font-Names="Trebuchet MS"
                            Font-Size="Small" ForeColor="#C00000"></asp:Label></td>
                            
                        </tr>
                        </table> 
       &nbsp;<br />
       
       <asp:Button ID="showRec" runat="server" Text="Show Recrods" Font-Names="Trebuchet MS" Font-Size="10pt" />
       <br />
                        <table id="Table5" border="2" cellpadding="2" cellspacing="2" style="font-size: 10pt;
                            font-style: italic; font-family: 'Trebuchet MS'" width="100%">
                            <tr>
                                <td class="HeaderDiv" colspan="2" style="width: 100%; text-align: center; height: 15px;" valign="top">
                                    <span style="color: white; font-family: Trebuchet MS"><strong><em>Dictation Details</em></strong></span></td>
                            </tr>
                            <tr>
                                <td style="text-align: center" valign="top">
                                    <asp:Table ID="Table4" runat="server" BorderColor="Silver" BorderWidth="2px" CellPadding="2"
                                        CellSpacing="2" Font-Italic="True" Font-Names="Trebuchet MS" Font-Size="Small"
                                        ForeColor="DimGray" GridLines="Both" Width="100%" cssClass="mytable">
                                        <asp:TableRow runat="server" cssclass="SMSelected" style="text-align: center">
                                            <asp:TableCell runat="server">
                            <input onclick="javascript:changeAll();" id="SelJob" type="checkbox" /></asp:TableCell>
                                            <asp:TableCell ID="TableCell1" runat="server">Job#</asp:TableCell>
                                            <asp:TableCell ID="TableCell2" runat="server">Status</asp:TableCell>
                                            <asp:TableCell ID="TableCell11" runat="server">
                                                <asp:Label ID="LDone" runat="server" ></asp:Label></asp:TableCell>
                                            <asp:TableCell ID="TableCell3" runat="server">TAT</asp:TableCell>
                                            <asp:TableCell ID="TableCell4" runat="server">Priority</asp:TableCell>
                                            <asp:TableCell ID="TableCell5" runat="server">Duration</asp:TableCell>
                                            <asp:TableCell ID="TableCell6" runat="server">Submit Date</asp:TableCell>
                                            <asp:TableCell ID="TableCell7" runat="server">Due Date</asp:TableCell>
                                            <asp:TableCell ID="TableCell12" runat="server">Remaining TAT</asp:TableCell>
                                            <asp:TableCell ID="TableCell8" runat="server">Account Name</asp:TableCell>
                                            <asp:TableCell ID="TableCell9" runat="server">Physician Name</asp:TableCell>
                                            <asp:TableCell ID="TableCell20" runat="server">Category</asp:TableCell>
                                            <asp:TableCell ID="TableCell10" runat="server">Direct</asp:TableCell>
                                        </asp:TableRow>
                                    </asp:Table>
                                </td>
                            </tr>
                            <tr>
                                <td style="text-align: left">
                                    <span style="color: #ff0033"><strong>Total Jobs :</strong>
                                        <asp:Label ID="lblTotJobs" runat="server" Font-Bold="True"></asp:Label>
                                        &nbsp; <strong>Total Mins:
                                            <asp:Label ID="LblTMins" runat="server" Font-Bold="True"></asp:Label>
                                            &nbsp; Direct Mins:
                                            <asp:Label ID="LblDMins" runat="server" Font-Bold="True"></asp:Label></strong></span></td>
                            </tr>
                            <tr>
                                <td style="text-align: center">
                                    <span style="color: white; font-family: Trebuchet MS"><strong><em>Dictation Details
                                    <asp:Button ID="submit" runat="server" Text="Assign Jobs"  /></em></strong></span></td>
                            </tr>
                        </table>
                        <br />
                        <div style="text-align:left">
                            <asp:Label ID="lblStatusMsg" runat="server" Text="" ForeColor="Firebrick" Font-Names="Trebuchet MS" Font-Size="10pt" Font-Bold="true"></asp:Label>
                        </div>
                        
                <script language="javascript" type="text/javascript">
//<![CDATA[
	var fnsFilters = {
		sort_select: true,
		loader: true,
		col_<%=int1%>: "select", 
		col_<%=int2%>: "select", 
		on_change: true,
		display_all_text: " [ Show all ] ",
		rows_counter: true,
		btn_reset: true,
		alternate_rows: true,
		btn_reset_text: "Clear",
		col<%=int1%>_width: ["220px",null,"280px"]
	}
	var fnsFilters1 = {
		sort_select: true,
		loader: true,
		col_10: "select", 
		on_change: true,
		display_all_text: " [ Show all ] ",
		rows_counter: true,
		btn_reset: true,
		alternate_rows: true,
		btn_reset_text: "Clear",
		col9_width: ["220px",null,"280px"]
	}
	setFilterGrid("Table4",fnsFilters);
//	setFilterGrid("Table4",fnsFilters1);
//]]>
</script>
        <br />
       &nbsp;<br />
       <asp:HiddenField ID="HAccID" runat="server" />
       <br />
       <asp:HiddenField ID="TotJobs" runat="server" />
        <br /><asp:HiddenField ID="ProLevel" runat="server" />
       <br />
       <asp:HiddenField ID="HUserID" runat="server" />
    </form>
</body>
</html>
