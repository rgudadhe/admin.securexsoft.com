<%@ Page Language="VB" AutoEventWireup="false" CodeFile="DeptSupervisor.aspx.vb" Inherits="UserPhyAssgn_Default" EnableViewState="True" %>





<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 1.0 Transitional/EN">
<LINK href= "../../styles/Default.css" type="text/css" rel="stylesheet">
<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Untitled Page</title>
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
  var textNode = document.createTextNode('Supervisor' + iteration);
  cellLeft.appendChild(textNode);


  var cell1 = row.insertCell(1);
  var sel1 = document.getElementById('Supervisor1').cloneNode(true);
  sel1.name = 'Supervisor' + iteration;
  sel1.id = 'Supervisor' + iteration;
 sel1.value='';
  cell1.appendChild(sel1);





 document.getElementById('HSuperID').value=iteration;

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
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <table id="MainTable" width="80%" style="font-size: 10pt; font-family: 'Trebuchet MS'; font-style: italic; color:Gray; " border ="2" cellpadding ="2" cellspacing ="2">
        <tr>
                <td style="width: 100%; text-align: center;" valign="top" colspan ="2" class="HeaderDiv">
                    <span style="font-family: Trebuchet MS; color: white;"><strong><em>Department
                        - Supervisor Assignment</em></strong></span></td>
               </tr>
                
            <tr>
                <td style="text-align: center" valign="top">
                    <asp:Panel  ID="Panel5" runat="server" Width="100%">
                    <table border="2" cellpadding="2" cellspacing ="2" style="color: gray; font-style: italic; " width="100%">
                    
                        <tr class="SMSelected" style="text-align: center">
                            <td colspan="2" style="text-align: center; height: 24px;">
                               <b> Department Search</b></td>
                        </tr>
                        <tr>
                            <td style="width: 50%; text-align: right;">
                                Department Name</td>
                            <td style="width: 50%; text-align: left;">
                                <asp:TextBox ID="TxtDname" runat="server"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td style="text-align: center; height: 30px;" colspan="2">
                                <asp:Button ID="BtnSubmit2" runat="server" Text="Submit" UseSubmitBehavior="False" /></td>
                        </tr>
                    </table>
                        </asp:Panel>
                    <asp:Panel ID="Panel6" runat="server" Width="100%"><asp:Table ID="Table1" runat="server" BorderColor="Silver" CellPadding="2" CellSpacing="2" Font-Italic="True" Font-Names="Trebuchet MS" ForeColor="DimGray" BorderWidth="2px" GridLines="Both" Width="100%" Font-Size="Small">
                        
                        <asp:TableRow runat="server" cssclass="SMSelected" style="text-align: center">
                            <asp:TableCell runat="server"></asp:TableCell>
                            <asp:TableCell runat="server">Department Name</asp:TableCell>
                        </asp:TableRow>
                    </asp:Table>
                    </asp:Panel>
                    
                    <asp:Panel ID="Panel1" runat="server" Width="100%">
                        <asp:Table ID="Table2" runat="server" BorderColor="Silver" BorderWidth="2px" CellPadding="2"
                            CellSpacing="2" Font-Italic="True" Font-Names="Trebuchet MS" Font-Size="Small"
                            ForeColor="DimGray" GridLines="Both" Width="100%">
                            
                            <asp:TableRow runat="server" cssclass="SMSelected" style="text-align: center">
                                <asp:TableCell runat="server">Department Name</asp:TableCell>
                                <asp:TableCell runat="server">Description</asp:TableCell>
                            </asp:TableRow>
                        </asp:Table>
                        <br />
                        <asp:Table ID="Table3" runat="server" GridLines="Both" Width="60%">
                            <asp:TableRow runat="server"  cssclass="SMSelected" style="text-align: center">
                                <asp:TableCell runat="server" ColumnSpan="2"><B>Supervisor Details</B></asp:TableCell>
                            </asp:TableRow>
                        </asp:Table>
                       
                        <br />
                        <input id="Button1" onclick="addRowToTable()" type="button" value="Add Supervisor" /><input
                            id="Button2" onclick="removeRowFromTable()" type="button" value="Remove Supervisor" /></asp:Panel>
                    &nbsp;
                        </td>
            </tr>
            <tr>
                <td colspan="2" style="text-align: center">
                    <asp:Button ID="BtnAssign" runat="server" Text="Submit" OnClientClick="return CheckDictCode();" /></td>
            </tr>
            <tr>
                <td colspan="2" style="text-align: center">
                    <asp:Table ID="Table4" runat="server" BorderColor="Silver" CellPadding="2" CellSpacing="2" Font-Italic="True" Font-Names="Trebuchet MS" ForeColor="DimGray" BorderWidth="2px" GridLines="Both" Width="60%" Font-Size="Small">
                    </asp:Table>
                </td>
            </tr>
        </table>
        <br />
        <asp:Label ID="DispBox" runat="server" Font-Bold="True" Font-Names="Trebuchet MS"
            Font-Size="Small" ForeColor="#C00000"></asp:Label></div>
        <asp:HiddenField ID="GrpActState" runat="server" />
        <asp:HiddenField ID="DeptState" runat="server" />
        <asp:HiddenField ID="HDeptID" runat="server" /><asp:HiddenField ID="HDictID" runat="server" />
        <asp:HiddenField ID="hUname" runat="server" />
        <asp:HiddenField ID="TotAct" runat="server" />
        <asp:HiddenField ID="TotLvl" runat="server" />
        <asp:HiddenField ID="HSuperID" runat="server" /><asp:HiddenField ID="HLocAcc" runat="server" />
        <br />
        <br />
        <asp:Button ID="btnSubmit4" runat="server" Text="Submit" Visible="False" />
       
        <asp:Button ID="BtnSubmit5" runat="server" Text="Submit" Visible="False" />
        <asp:Button ID="btnsubmit3" runat="server" Text="Submit" Visible="False" />
    </form>
</body>
</html>
