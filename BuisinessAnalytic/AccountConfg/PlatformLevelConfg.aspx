<%@ Page Language="VB" AutoEventWireup="false" CodeFile="PlatformLevelConfg.aspx.vb" Inherits="UserPhyAssgn_Default" EnableViewState="True" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 1.0 Transitional/EN">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <link href= "~/App_Themes/Css/Main.css" type="text/css" rel="stylesheet"/>
    <link href= "~/App_Themes/Css/Styles.css" type="text/css" rel="stylesheet"/>
    <link href= "~/App_Themes/Css/Common.css" type="text/css" rel="stylesheet"/>
    <title>Account Location</title>
   <script type="text/javascript"  language="javascript">
   function addRowToTable()
{
  var tbl = document.getElementById('Table2');
  var lastRow = tbl.rows.length;
  var iteration = lastRow;
  var row = tbl.insertRow(lastRow);

  var cell1 = row.insertCell(0);
  var sel1 = document.getElementById('Level1').cloneNode(true);
  sel1.name = 'Level' + iteration;
  sel1.value='';
  cell1.appendChild(sel1);


    
  var cell2 = row.insertCell(1);
  var sel2 = document.getElementById('Name1').cloneNode(true);
  sel2.name = 'Name' + iteration;
  sel2.value='';
  cell2.appendChild(sel2);
  
  
 
  
  var cell4 = row.insertCell(3);
  var sel4 = document.getElementById('Factor1').cloneNode(true);
  sel4.name = 'Factor' + iteration;
  sel4.value='';
  cell4.appendChild(sel4);
  
 
  
  document.getElementById('HLoc').value=iteration;
  alert(iteration);
}
function removeRowFromTable()
{
  var tbl = document.getElementById('Table2');
  var lastRow = tbl.rows.length-1;
  if (lastRow > 1) 
  {
  tbl.deleteRow(lastRow);
   }
  //alert(lastRow);
document.getElementById('HLoc').value=lastRow-1;
}

</script>   
</head>
<body>
    <form id="form1" runat="server">
        <div id="body">
        <div id="cap"></div>
        <div id="main">
        <h1>Platform Levels</h1>
    <div>
        <asp:Panel ID="Panel1" runat="server" HorizontalAlign="Left">
            <table id="MainTable" width="80%">
       
                
            <tr>
                <td style="text-align: center" valign="top">
                    
                    <asp:Panel ID="Panel7" runat="server" Width="100%">
                    
                        <asp:Table ID="Table2" runat="server" Width="100%">
                            <asp:TableRow ID="TableRow3" runat="server" style="text-align: center">
                                <asp:TableCell ID="TableCell6" CssClass="alt1" runat="server">Level</asp:TableCell>
                                <asp:TableCell ID="TableCell7" CssClass="alt1" runat="server">Name</asp:TableCell>
                                <asp:TableCell ID="TableCell2" CssClass="alt1" runat="server">Factor</asp:TableCell>
                                
                            </asp:TableRow>
                            <asp:TableRow ID="TableRow4" runat="server">
                                <asp:TableCell ID="TableCell8" runat="server">
                                    <asp:TextBox ID="Level1" runat="server"></asp:TextBox>
                                                                    </asp:TableCell>
                                <asp:TableCell ID="TableCell9" runat="server">
                                    <asp:TextBox ID="Name1" runat="server"></asp:TextBox></asp:TableCell>
                                    
                                    <asp:TableCell ID="TableCell11" runat="server">
                                    <asp:TextBox ID="Factor1" runat="server"></asp:TextBox></asp:TableCell>
                                    
                            </asp:TableRow>
                        </asp:Table>
                    
                    <input id="Button1" type="button" value="Add Level" class="button" onclick="addRowToTable()"/>
                    
                    <input id="Button2" type="button" value="Remove Level" class="button" onclick="removeRowFromTable()"/>
                        </asp:Panel>
                        </td>
            </tr>
            <tr>
                <td colspan="2" style="text-align: center">
                    <asp:Button ID="BtnAssign" runat="server" Text="Submit" CssClass="button" /></td>
            </tr>
            
        </table>
        </asp:Panel>
        
    
    </div>
        <asp:HiddenField ID="GrpActState" runat="server" />
        <asp:HiddenField ID="ActState" runat="server" />
        <asp:HiddenField ID="HActID" runat="server" />
        <asp:HiddenField ID="HActName" runat="server" />
        <asp:HiddenField ID="hUname" runat="server" />
        <asp:HiddenField ID="TotAct" runat="server" />
        <asp:HiddenField ID="TotLvl" runat="server" />
        <asp:HiddenField ID="HLoc" runat="server" />
        <asp:HiddenField ID="hdnExistingIDs" runat="server" />
        <br />
        <br />
        <asp:Button ID="btnSubmit4" runat="server" Text="Submit" Visible="False" CssClass="button" />
       &nbsp;&nbsp;
        <asp:Button ID="BtnSubmit5" runat="server" Text="Submit" Visible="False" CssClass="button" />
        <asp:Button ID="btnsubmit3" runat="server" Text="Submit" Visible="False" CssClass="button" />
        </div> 
        </div> 
    </form>
</body>
</html>
