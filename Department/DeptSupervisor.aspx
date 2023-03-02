<%@ Page Language="VB" AutoEventWireup="false" CodeFile="DeptSupervisor.aspx.vb" Inherits="UserPhyAssgn_Default" EnableViewState="True" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 1.0 Transitional/EN">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Assing Supervisor</title>
    <link href= "../App_Themes/Css/styles.css" type="text/css" rel="stylesheet" />
    <link href= "../App_Themes/Css/Common.css" type="text/css" rel="stylesheet" />
   <script type="text/javascript"  language="javascript">
      function CheckDictCode()
      {
        var rcount = document.getElementById('HSuperID').value;
        
        for (i=1; i<=rcount; i++)
        {
            var DictCode = 'Supervisor' + i;
            if (document.getElementById(DictCode).value == '')
            {
                alert('Please enter super visor');
                document.getElementById(DictCode).focus();
                return false;
            }
            
            for (j=1; j<=rcount; j++)
            {
                if (j!=i)
                {
                    var SID = 'Supervisor' + j;
                    if (document.getElementById(SID).value == document.getElementById(DictCode).value)
                    {
                        alert('Please select different supervisor')
                        document.getElementById(SID).focus();
                        return false;
                    }
                }
            }
        }
        return true;
      }
      
      
      function SuperChk(str,j)
      {
        
        var rcount = document.getElementById('HSuperID').value;
        for (i=1; i<=rcount; i++)
        {
            if (j!=i)
            {
                var DictCode = 'Supervisor' + i;
                alert(DictCode)
                if (document.getElementById(DictCode).value == str)
                {
                    alert('Please select different supervisor')
                    document.getElementById(DictCode).focus();
                    return false;
                }
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
//document.getElementById('HDictCode').value=lastRow-1;
document.getElementById('HSuperID').value=lastRow-1;
}

</script>   
</head>
<body>
    <form id="form1" runat="server">
        <div id="body">
        <div id="cap"></div>
        <div id="main">
        <h1>Assign Supervisor</h1>
        <asp:Panel ID="Panel2" runat="server" HorizontalAlign="Left">
            <div>
        <table id="MainTable" width="80%">
        <tr>
                <td valign="top" colspan ="2" class="HeaderDiv">
                    Department - Supervisor Assignment
                </td>
               </tr>
                
            <tr>
                <td style="text-align: center" valign="top">
                    <asp:Panel  ID="Panel5" runat="server" Width="100%">
                    <table width="100%">
                    
                        <tr style="text-align: center">
                            <td colspan="2" class="alt1" >
                               Department Search</td>
                        </tr>
                        <tr>
                            <td style="width: 50%; text-align: right;">
                                Department Name</td>
                            <td style="width: 50%; text-align: left;">
                                <asp:TextBox ID="TxtDname" CssClass="common" runat="server"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td style="text-align: center; height: 30px;" colspan="2">
                                <asp:Button ID="BtnSubmit2" runat="server" Text="Submit" UseSubmitBehavior="False" CssClass="button" /></td>
                        </tr>
                    </table>
                        </asp:Panel>
                    <asp:Panel ID="Panel6" runat="server" Width="100%"><asp:Table ID="Table1" runat="server"  Width="100%">
                        
                        <asp:TableRow ID="TableRow1" runat="server" style="text-align: center">
                            <asp:TableCell ID="TableCell1" runat="server" CssClass="alt1">&nbsp</asp:TableCell>
                            <asp:TableCell ID="TableCell2" runat="server" CssClass="alt1">Department Name</asp:TableCell>
                        </asp:TableRow>
                    </asp:Table>
                    </asp:Panel>
                    
                    <asp:Panel ID="Panel1" runat="server" Width="100%">
                        <asp:Table ID="Table2" runat="server" Width="100%">
                            
                            <asp:TableRow ID="TableRow2" runat="server">
                                <asp:TableCell ID="TableCell3" runat="server" CssClass="alt1">Department Name</asp:TableCell>
                                <asp:TableCell ID="TableCell4" runat="server" CssClass="alt1">Description</asp:TableCell>
                            </asp:TableRow>
                        </asp:Table>
                        <br />
                        <asp:Table ID="Table3" runat="server" Width="60%">
                            <asp:TableRow ID="TableRow3" runat="server"  style="text-align: center">
                                <asp:TableCell ID="TableCell5" runat="server" ColumnSpan="2" CssClass="alt1">Supervisor Details</asp:TableCell>
                            </asp:TableRow>
                        </asp:Table>
                       
                        <br />
                        <input id="Button1" class="button" onclick="addRowToTable()" type="button" value="Add Supervisor" /><input
                            id="Button2" class="button" onclick="removeRowFromTable()" type="button" value="Remove Supervisor" /></asp:Panel>
                    &nbsp;
                        </td>
            </tr>
            <tr>
                <td colspan="2" style="text-align: center">
                    <asp:Button ID="BtnAssign" runat="server" CssClass="button" Text="Submit" OnClientClick="javascript:return CheckDictCode();" /></td>
            </tr>
            <tr>
                <td colspan="2" style="text-align: center">
                    <asp:Table ID="Table4" runat="server" Width="60%">
                    </asp:Table>
                </td>
            </tr>
        </table>
        <br />
        <asp:Label ID="DispBox" runat="server" ForeColor="#C00000"></asp:Label></div>
        <asp:HiddenField ID="GrpActState" runat="server" />
        <asp:HiddenField ID="DeptState" runat="server" />
        <asp:HiddenField ID="HDeptID" runat="server" /><asp:HiddenField ID="HDictID" runat="server" />
        <asp:HiddenField ID="hUname" runat="server" />
        <asp:HiddenField ID="TotAct" runat="server" />
        <asp:HiddenField ID="TotLvl" runat="server" />
        <asp:HiddenField ID="HSuperID" runat="server" /><asp:HiddenField ID="HLocAcc" runat="server" />
        <br />
        <br />
        <asp:Button ID="btnSubmit4" CssClass="button" runat="server" Text="Submit" Visible="False" />
       
        <asp:Button ID="BtnSubmit5" CssClass="button" runat="server" Text="Submit" Visible="False" />
        <asp:Button ID="btnsubmit3" CssClass="button" runat="server" Text="Submit" Visible="False" />
        </asp:Panel>
        </div> 
        </div> 
    </form>
</body>
</html>
