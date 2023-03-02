<%@ Page Language="VB" AutoEventWireup="false" CodeFile="AccountLocAssment.aspx.vb" Inherits="UserPhyAssgn_Default" EnableViewState="True" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 1.0 Transitional/EN">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <link href= "../App_Themes/Css/Main.css" type="text/css" rel="stylesheet"/>
    <link href= "../App_Themes/Css/Styles.css" type="text/css" rel="stylesheet"/>
    <link href= "../App_Themes/Css/Common.css" type="text/css" rel="stylesheet"/>
    <title>Account Location</title>
   <script type="text/javascript"  language="javascript">
   function addRowToTable()
{
  var tbl = document.getElementById('Table2');
  var lastRow = tbl.rows.length;
  var iteration = lastRow;
  var row = tbl.insertRow(lastRow);

  var cell1 = row.insertCell(0);
  var sel1 = document.getElementById('LocCode1').cloneNode(true);
  sel1.name = 'LocCode' + iteration;
  sel1.value='';
  cell1.appendChild(sel1);


  var sel3 = document.createElement("input");
  sel3.setAttribute("type", "hidden");
  sel3.setAttribute("name",'LocID' + iteration);
  sel3.setAttribute("value", '');
  cell1.appendChild(sel3);
  
  var cell2 = row.insertCell(1);
  var sel2 = document.getElementById('LocName1').cloneNode(true);
  sel2.name = 'LocName' + iteration;
  sel2.value='';
  cell2.appendChild(sel2);
  
  
  document.getElementById('HLoc').value=iteration;
  //alert(iteration);
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
        <h1>Account Location Assignment</h1>
    <div>
        <asp:Panel ID="Panel1" runat="server" HorizontalAlign="Left">
            <table id="MainTable" width="80%">
        <tr>
                <td class="HeaderDiv" valign="top" colspan ="2" style="text-align: center">
                    Location Details</td>
               </tr>
                
            <tr>
                <td style="text-align: center" valign="top">
                    <asp:Panel  ID="Panel5" runat="server" Width="100%">
                    <table width="100%">
                    
                        <tr  style="text-align: center">
                            <td class="alt1" colspan="2" style="text-align: center; height: 24px;" >
                                Account Search</td>
                        </tr>
                        <tr>
                            <td style="width: 50%; text-align: right;">
                                Account Name</td>
                            <td style="width: 50%; text-align: left;">
                                <asp:TextBox ID="TxtAname" runat="server"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td style="text-align: center; " colspan="2">
                                <asp:Button ID="BtnSubmit2" runat="server" Text="Submit" UseSubmitBehavior="False" CssClass="button" /></td>
                        </tr>
                    </table>
                    <asp:RegularExpressionValidator  Display="None" 
    id="RegTxtAname"  
    runat="server" 
    ControlToValidate="TxtAname" 
    ValidationExpression="^[a-zA-Z-]+$"
    ErrorMessage="Account Name - Please enter valid input."
   />
        <asp:ValidationSummary ID="ValidationSummary1" runat="server" HeaderText="Following error occurs:" ShowMessageBox="true" DisplayMode="BulletList" ShowSummary="False" />      

                        </asp:Panel>
                    <asp:Panel ID="Panel6" runat="server" Width="100%"><asp:Table ID="Table1" runat="server" Width="100%">
                        <asp:TableRow ID="TableRow1" runat="server" style="text-align: center">
                            <asp:TableCell ID="TableCell1" CssClass="alt1" runat="server">&nbsp;</asp:TableCell>
                            <asp:TableCell ID="TableCell2" CssClass="alt1" runat="server">Account Name</asp:TableCell>
                            <asp:TableCell ID="TableCell3" CssClass="alt1" runat="server">Account Number</asp:TableCell>
                        </asp:TableRow>
                    </asp:Table>
                    </asp:Panel>
                    <asp:Panel ID="Panel7" runat="server" Width="100%"><asp:Table ID="Table3" runat="server" Width="100%" >
                       
                        <asp:TableRow ID="TableRow2" runat="server" style="text-align: center">
                            <asp:TableCell ID="TableCell4" CssClass="alt1" runat="server">Account Name</asp:TableCell>
                            <asp:TableCell ID="TableCell5" CssClass="alt1" runat="server">Account Number</asp:TableCell>
                        </asp:TableRow>
                    </asp:Table>
                    
                        <asp:Table ID="Table2" runat="server" Width="100%">
                            <asp:TableRow ID="TableRow3" runat="server" style="text-align: center">
                                <asp:TableCell ID="TableCell6" CssClass="alt1" runat="server">Location Code</asp:TableCell>
                                <asp:TableCell ID="TableCell7" CssClass="alt1" runat="server">Location Name</asp:TableCell>
                            </asp:TableRow>
                            <asp:TableRow ID="TableRow4" runat="server">
                                <asp:TableCell ID="TableCell8" runat="server">
                                    <asp:TextBox ID="LocCode1" runat="server"></asp:TextBox>
                                    <asp:HiddenField ID="hdnLocID1" runat="server" />
                                </asp:TableCell>
                                <asp:TableCell ID="TableCell9" runat="server">
                                    <asp:TextBox ID="LocName1" runat="server"></asp:TextBox></asp:TableCell>
                            </asp:TableRow>
                        </asp:Table>
                    
                    <input id="Button1" type="button" value="Add Location" class="button" onclick="addRowToTable()"/>
                    
                    <input id="Button2" type="button" value="Remove Location" class="button" onclick="removeRowFromTable()"/>
                        </asp:Panel>
                        </td>
            </tr>
            <tr>
                <td colspan="2" style="text-align: center">
                    <asp:Button ID="BtnAssign" runat="server" Text="Submit" CssClass="button" /></td>
            </tr>
            <tr>
                <td colspan="2" style="text-align: center">
                    <asp:Table ID="Table4" runat="server" Width="100%">
                        <asp:TableRow ID="TableRow5" runat="server" style="text-align: center">
                            <asp:TableCell ID="TableCell10" CssClass="alt1" runat="server">Account Name</asp:TableCell>
                            <asp:TableCell ID="TableCell11" CssClass="alt1" runat="server">Location Code</asp:TableCell>
                            <asp:TableCell ID="TableCell12" CssClass="alt1" runat="server">Location Code</asp:TableCell>
                        </asp:TableRow>
                    </asp:Table>
                </td>
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
