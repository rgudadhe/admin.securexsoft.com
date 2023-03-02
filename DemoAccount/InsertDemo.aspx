<%@ Page Language="VB" AutoEventWireup="false" CodeFile="InsertDemo.aspx.vb" Inherits="UserPhyAssgn_Default" EnableViewState="True" %>

<%@ Register Assembly="System.Web.Extensions, Version=1.0.61025.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI" TagPrefix="asp" %>


<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 1.0 Transitional/EN">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    
    <link href= "../App_Themes/Css/Common.css" type="text/css" rel="stylesheet" />
    <link href= "../App_Themes/Css/Main.css" type="text/css" rel="stylesheet" />
    <link href= "../App_Themes/Css/styles.css" type="text/css" rel="stylesheet" />
    <title>Insert Demo</title>
   <script type="text/javascript"  language="javascript">
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

</script>   

</head>
<body>
    <form id="form1" runat="server" defaultbutton="BtnSubmit2" >
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager> 
        <div id="body">
        <div id="cap"></div>
        <div id="main">
        <h1>Demo Account Details</h1>
    <div>
                    <asp:Panel  ID="Panel5" runat="server" Width="100%">
                    <table style="text-align:left"  width="100%">
                    
                        <tr>
                            <td colspan="2" style="text-align: center" class="HeaderDiv">
                                <B>Account Search</B></td>
                        </tr>
                        <tr>
                            <td style="width: 50%; text-align: right; height: 30px;">
                                Account Name</td>
                            <td style="width: 50%; text-align: left; height: 30px;">
                                <asp:TextBox ID="TxtAname" runat="server"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td style="text-align: center; height: 30px;" colspan="2">
                                <asp:Button ID="BtnSubmit2" runat="server" Text="Submit" CssClass="button" />
                            </td>
                        </tr>
                    </table></asp:Panel>
                    <asp:Panel ID="Panel6" runat="server" Width="100%">
                        <asp:Table ID="Table1" runat="server" Width="100%">
                            <asp:TableRow runat="server" style="text-align: center">
                                <asp:TableCell ColumnSpan="4" cssclass="HeaderDiv"   runat="server"><B>Account Search</B></asp:TableCell>
                            </asp:TableRow>
                            <asp:TableRow runat="server" style="text-align: center">
                                <asp:TableCell runat="server" CssClass="alt" >&nbsp;</asp:TableCell>
                                <asp:TableCell runat="server" CssClass="alt" >Account Name</asp:TableCell>
                                <asp:TableCell runat="server" CssClass="alt" >Account Number</asp:TableCell>
                            </asp:TableRow>
                        </asp:Table>
                    </asp:Panel>
                    
                    <asp:Panel ID="Panel2" runat="server" Width="100%">
                        <asp:Table ID="Table5" runat="server" Width="100%">
                            <asp:TableRow runat="server" style="text-align: center">
                                <asp:TableCell runat="server" cssclass="HeaderDiv" ColumnSpan="3">Account Details</asp:TableCell>
                            </asp:TableRow>
                            <asp:TableRow runat="server">
                                <asp:TableCell runat="server" CssClass="alt">Account Name</asp:TableCell>
                                <asp:TableCell runat="server" CssClass="alt">Description</asp:TableCell>
                                <asp:TableCell runat="server" CssClass="alt">Account Number</asp:TableCell>
                            </asp:TableRow>
                        </asp:Table>
                        <br />
                        <asp:Table ID="Table3" runat="server" CssClass="common">
                        </asp:Table>
                        &nbsp;
                    </asp:Panel>
                    <asp:Button ID="Assign" runat="server" Text="Submit" CssClass="button" />
                    
            
                    <asp:Table ID="Table4" runat="server" CssClass="common" Width="60%">
                    </asp:Table>
                
        <br />
        <asp:Label ID="DispBox" runat="server" CssClass="Title" ForeColor="#C00000"></asp:Label></div>
        <asp:HiddenField ID="GrpActState" runat="server" />
        <asp:HiddenField ID="ActState" runat="server" />
        <asp:HiddenField ID="HActID" runat="server" />
        <asp:HiddenField ID="HFoldName" runat="server" />
        <asp:HiddenField ID="HDictID" runat="server" />
        <asp:HiddenField ID="hUname" runat="server" />
        <asp:HiddenField ID="TotAct" runat="server" />
        <asp:HiddenField ID="TotLvl" runat="server" />
        <asp:HiddenField ID="DemoFieldText" runat="server" />
        <asp:HiddenField ID="DemoFieldValue" runat="server" />
        <asp:HiddenField ID="HDictCode" runat="server" /><asp:HiddenField ID="HLocAcc" runat="server" />
        <br />
        
        <div style="text-align:left">
            <asp:Label ID="Label1" runat="server" CssClass="Title" ForeColor="#C00000" ></asp:Label>
        </div>
        
            <asp:Button ID="submit3" runat="server" Text="Submit" CssClass="button" Visible="False" />
        
        </div> 
        </div>
        
    </form>
</body>
</html>
