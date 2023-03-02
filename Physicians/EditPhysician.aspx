<%@ Page Language="VB" AutoEventWireup="false" CodeFile="EditPhysician.aspx.vb" Inherits="Department_Default" %>



<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 1.0 Transitional/EN">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>View/Edit Dictator</title>
    <link href= "../App_Themes/Css/Main.css" type="text/css" rel="stylesheet" />
    <link href="../App_Themes/Css/Common.css" type="text/css" rel="stylesheet"  />
    <link href= "../App_Themes/Css/Styles.css" type="text/css" rel="stylesheet" />
    
   <script type="text/javascript"  language="javascript">
   function OnSelectedIndexChange()

{
//alert('hello');
document.getElementById('ImageButton1').disabled = true; 
//document.form1.ImageButton1.disabled = false;
//return false;
}


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
<script type="text/javascript">

    function disableApprove(chk) {
        alert('Hello');
        var ddl = document.getElementById('DDLFaxmode');
        var chk = document.getElementById('chkException');
        if (chk.checked) {
            ddl.disabled = true;
            chk.disabled = true;
        }
        else {
            ddl.disabled = false;
            chk.disabled = false;
        }


    }
    </script>


</head>
<body>
 
    <form id="form1" runat="server">
    <div id="body">
    <div id="cap"></div>
    <div id="main">
    <h1>Edit Dictator</h1>

                    <asp:Panel  ID="Panel5" runat="server" Width="100%">
                    <table  width="100%" >
                        <tr >
                            <td class="HeaderDiv" colspan="2" style="text-align: center">
                                <B>Account Search</B></td>
                        </tr>
                        <tr>
                            <td style="width: 50%; text-align: right;" >
                               Account Name</td>
                            <td style="width: 50%; text-align: left;">
                                <asp:TextBox ID="TxtAname" runat="server"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td style="text-align: center;" colspan="2">
                                <asp:Button ID="BtnSubmit2" CssClass="button"  runat="server" Text="Submit" UseSubmitBehavior="False" /></td>
                        </tr>
                    </table></asp:Panel>
                    
                    <asp:Panel ID="Panel6" runat="server" Width="100%" HorizontalAlign="Left">
                    <asp:Table ID="Table1" runat="server" Width="90%">
                        <asp:TableRow ID="TableRow1" runat="server"  >
                            <asp:TableCell ID="TableCell1" ColumnSpan="3"   runat="server" cssclass="HeaderDiv" style="text-align: center"><B>Account Search</B></asp:TableCell>
                        </asp:TableRow>
                        <asp:TableRow ID="TableRow2" runat="server" style="text-align: center">
                            <asp:TableCell ID="TableCell2" runat="server" CssClass="alt">&nbsp</asp:TableCell>
                            <asp:TableCell ID="TableCell3" runat="server" CssClass="alt">Account Name</asp:TableCell>
                            <asp:TableCell ID="TableCell4" runat="server" CssClass="alt">Account Number</asp:TableCell>
                        </asp:TableRow>
                    </asp:Table>
                    </asp:Panel>
                    
                    <asp:Panel ID="Panel1" runat="server" Width="100%" HorizontalAlign="Left">
                        <asp:Table ID="Table2" runat="server" Width="90%">
                            <asp:TableRow ID="TableRow3" runat="server" >
                                <asp:TableCell ID="TableCell5" runat="server" ColumnSpan="4" cssclass="HeaderDiv" style="text-align: center"><B>Dictator Search</B></asp:TableCell>
                            </asp:TableRow>
                            <asp:TableRow ID="TableRow4" runat="server" style="text-align: center">
                                <asp:TableCell ID="TableCell6" runat="server" CssClass="alt" >&nbsp;</asp:TableCell>
                                <asp:TableCell ID="TableCell7" runat="server" CssClass="alt" >First Name</asp:TableCell>
                                <asp:TableCell ID="TableCell8" runat="server" CssClass="alt" >Last Name</asp:TableCell>
                                <asp:TableCell ID="TableCell9" runat="server" CssClass="alt" >Username</asp:TableCell>
                            </asp:TableRow>
                        </asp:Table>
                    </asp:Panel>
                    <asp:Panel ID="Panel2" runat="server" Width="100%" HorizontalAlign="Left">
                    <asp:Label ID="DispBox" runat="server" CssClass="Title" ForeColor="#C00000"></asp:Label>
                                <table width="90%" style="text-align:left">
            <tr>
                <td colspan="4" style="text-align: center; height: 15px;" class="HeaderDiv">
                  
                      View Dictator Details</td>
            </tr>
            <tr>
                <td colspan="2" style="height: 25px; text-align: right">
                    Dictator Name</td>
                <td colspan="2" style="height: 25px; text-align: left">
                    <asp:DropDownList ID="DLPhyID"   runat="server" AutoPostBack="true">
                       
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td style="text-align: right; text-align: right;">
                    * First Name</td>
                <td style="text-align: left;">
                    <asp:TextBox ID="TxtFirstName" runat="server"></asp:TextBox></td>
                <td style="text-align: right; text-align: right;">
                    Middle Name</td>
                <td style="text-align: left;">
                    <asp:TextBox ID="TxtMiddleName" runat="server"></asp:TextBox></td>
            </tr>
            <tr>
                <td style="text-align: right; text-align: right;">
                    * Last Name</td>
                <td style=" text-align: left;">
                    <asp:TextBox ID="TxtLastName" runat="server"></asp:TextBox></td>
                <td style="text-align: right; text-align: right;">
                * Signed Name</td>
                <td style=" text-align: left;">
                    <asp:TextBox ID="TxtSignedName" runat="server"></asp:TextBox></td>
            </tr>
            <tr>
                
                
                <td style="text-align: right; text-align: right;">
                Speciality</td>
                <td style="text-align: left; text-align: left;"  >
                    <asp:TextBox ID="TxtSpeciality" runat="server"></asp:TextBox></td>
                    <td style="text-align: right;"  >
                     * Email</td>
                <td style="text-align: left;"  >
                    <asp:TextBox ID="TxtEmail" runat="server" ></asp:TextBox></td>
            </tr>
            
            <tr>
                <td style="text-align: right; text-align: right;"  >
                Phone Number&nbsp;</td>
                <td style=" text-align: left;"  >
                    <asp:TextBox ID="TxtPhoneno" runat="server"></asp:TextBox></td>
                <td style=" text-align: right;"  >
                Fax        </td>
                <td style=" text-align: left;"  >
                    <asp:TextBox ID="txtFax" runat="server"></asp:TextBox></td>
            </tr>
            <tr>
                
                <td style="text-align: right; text-align: right;"  >
                    Account Name</td>
                <td style=" text-align: left;"  >
                    <asp:DropDownList ID="ActID" runat="server"> </asp:DropDownList>
                </td>
                <td style=" text-align: right;"  >
                Provider ID        </td>
                <td style=" text-align: left;"  >
                    <asp:TextBox ID="TxProvID" runat="server"></asp:TextBox></td>
                <td >
                    </td>
            </tr>
              <tr>
                <td  style="text-align: right; vertical-align:top " >
                    Dictator Status  </td>
                <td style="vertical-align:top;">
                    <asp:DropDownList ID="DLStatus" runat="server">
                    <asp:ListItem Text="Active" Value="False">                   </asp:ListItem>
                    <asp:ListItem Text="Inactive" Value="True">                   </asp:ListItem>
                    </asp:DropDownList>
                    </td>
                    <td style="text-align: right; text-align: right; vertical-align:top">
                        Extend. Signed Name
                    </td>
                  <td rowspan="2" style="vertical-align: top; text-align: left">
                        <asp:TextBox ID="txtExSignedName" TextMode="MultiLine" Width="200" Height="60" runat="server"></asp:TextBox>
                  </td>
                    
                                       
                    
              </tr>              
                                    <tr>
                                        <td style="vertical-align: top; text-align: right">
                                            Category</td>
                                        <td style="vertical-align: top">
                                            <asp:DropDownList ID="DLCategory" runat="server">
                                            <asp:ListItem Text="None" Value="">                   </asp:ListItem>
                                            <asp:ListItem Text="A" Value="A">                   </asp:ListItem>
                                            <asp:ListItem Text="B" Value="B">                   </asp:ListItem>
                                            </asp:DropDownList></td>
                                        <td style="vertical-align: top; text-align: right">
                                        </td>
                                    </tr>
            
            <tr><td style="text-align: right; text-align: right;">
                  FaxPlus - Dictating Physician</td>
                <td style=" text-align: left;">
                    <%--<asp:CheckBox ID="chkFax" runat="server" onclick="disableApprove(this);" />--%>
                    <asp:CheckBox ID="chkFax" runat="server" AutoPostBack="true"  />
                </td>
                 <td style="text-align: right; text-align: right;">
                  FaxPlus Mode</td>
                <td style=" text-align: left;">
                    <asp:DropDownList ID="DDLFaxmode" AutoPostBack="true"    runat="server">
                    <asp:ListItem Text="Signed" Value="True">                   </asp:ListItem>
                    <asp:ListItem Text="Pending Signature" Value="False">                   </asp:ListItem>
                    </asp:DropDownList>
                      <asp:CheckBox ID="chkException" runat="server" Text="Exclude Exception"  />
                </td>
               </tr>
                <tr>
                  <td style="text-align: right; text-align: right;">
                  Signature Level</td>
                <td style=" text-align: left;">
                   <asp:DropDownList ID="DropDownList2" runat="server">
                    <asp:ListItem Text="1" Value="1">                   </asp:ListItem>
                    <asp:ListItem Text="2" Value="2">                   </asp:ListItem>
                    </asp:DropDownList>
                </td>
                 <td style="text-align: right; text-align: right;">
                  Is VRS</td>
                <td style=" text-align: left;">
                    <asp:DropDownList ID="DLDBStatus" runat="server">
                    <asp:ListItem Text="Yes" Value="True">                   </asp:ListItem>
                    <asp:ListItem Text="No" Value="False">                   </asp:ListItem>
                    </asp:DropDownList>
                    <asp:TextBox ID="txtExpQS" runat="server" Width="30px"></asp:TextBox>
                    <%--<asp:RegularExpressionValidator ID="RegularExpressionValidator1" ControlToValidate="txtExpQS" runat="server" ErrorMessage="Only Numbers allowed" ValidationExpression="^([0-9].[0-9])*$"></asp:RegularExpressionValidator>--%>
                    <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToValidate="txtExpQS" ErrorMessage="Value must be double"  Type="Double"></asp:CompareValidator>
                </td></tr>
                <tr>
               <td style="text-align: right; text-align: right;">
                    E-Signature Image
                    </td>
              <td style=" text-align: left;">
                    <asp:FileUpload ID="FileUpload1" runat="server" />
                    </td>
                    <td style="text-align: right; text-align: right;">
                 Exclude from AutoFax</td>
                <td style=" text-align: left;">
                    <asp:DropDownList ID="DDLAutoFax" runat="server">
                   
                    <asp:ListItem Text="No" Value="False">                   </asp:ListItem>
                     <asp:ListItem Text="Yes" Value="True">                   </asp:ListItem>
                    </asp:DropDownList>
                </td>
                    </tr> 
               <%-- <tr>
                 <td style="text-align: right; text-align: right;">
                    E-Signature Image Mode
                    </td>
             
                <td style=" text-align: left;">
                    <asp:DropDownList ID="DLSignMode" runat="server">
                   
                    <asp:ListItem Text="Finished" Value="0">                   </asp:ListItem>
                     <asp:ListItem Text="Signed" Value="1">                   </asp:ListItem>
                    </asp:DropDownList>
                </td>
                    </tr> --%>
                <tr>
                <td style="text-align: center;" colspan="4">
                    <asp:Button ID="ImageButton1" CssClass="button" runat="server" text="Submit" />
                    
                    
                </td>
            </tr>
        </table>
        <br />
                        <asp:Table ID="tblcodes" HorizontalAlign="Left" runat="server" Width="240px">
                        <asp:TableHeaderRow HorizontalAlign="Center" runat="server" >
                        <asp:TableCell ColumnSpan="2" runat="server" cssclass="HeaderDiv" >
                        Dictation Codes</asp:TableCell> </asp:TableHeaderRow>
                        </asp:Table>
        <asp:RequiredFieldValidator  Display="None" ID="RequiredFieldValidator1" runat="server" ControlToValidate="TxtFirstName"
            ErrorMessage="Please enter First Name" SetFocusOnError="True"></asp:RequiredFieldValidator><br />
        <asp:RequiredFieldValidator  Display="None" ID="RequiredFieldValidator2" runat="server" ControlToValidate="TxtLastName"
            ErrorMessage="Please enter Last Name" SetFocusOnError="True"></asp:RequiredFieldValidator><br />
        <asp:RequiredFieldValidator  Display="None" ID="RequiredFieldValidator3" runat="server" ControlToValidate="TxtSignedName"
            ErrorMessage="Please enter Signed Name" SetFocusOnError="True"></asp:RequiredFieldValidator><br />
        <asp:RequiredFieldValidator  Display="None" ID="RequiredFieldValidator4" runat="server" ControlToValidate="TxtEmail"
            ErrorMessage="Please enter E-Mail ID" SetFocusOnError="True"></asp:RequiredFieldValidator><br />            
        <%--<asp:RegularExpressionValidator  Display="None" ID="RegularExpressionValidator2" runat="server" ControlToValidate="TxtEmail"
            ErrorMessage="Please enter valid E-Mail ID"  ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></asp:RegularExpressionValidator><br />
--%>
                    </asp:Panel>
             <table width="100%" style="text-align:left">         
            <tr>
                <td colspan="2" style="text-align: center">
                    <asp:Button ID="BtnAssign" runat="server" Text="Submit" OnClientClick="return CheckDictCode();" /></td>
            </tr>
            </table>
            
                    <asp:Table ID="Table4" runat="server" HorizontalAlign="Left" Width="60%" >
                    </asp:Table>
            
        <br />
        <div style="text-align:left;">  
                     <asp:Image ID="Image1" Visible="false"   runat="server" /></div> 
        <asp:HiddenField ID="GrpActState" runat="server" />
        <asp:HiddenField ID="ActState" runat="server" />
        <asp:HiddenField ID="HActID" runat="server" />
        <asp:HiddenField ID="HDictID" runat="server" />
        <asp:HiddenField ID="hUname" runat="server" />
        <asp:HiddenField ID="TotAct" runat="server" />
        <asp:HiddenField ID="TotLvl" runat="server" />
        <asp:HiddenField ID="HDictCode" runat="server" /><asp:HiddenField ID="HLocAcc" runat="server" />
        <asp:Button CssClass="button" ID="btnSubmit4" runat="server" Text="Submit" Visible="False" />
        <asp:Button CssClass="button" ID="BtnSubmit5" runat="server" Text="Submit" Visible="False" />
        <asp:Button CssClass="button" ID="btnsubmit3" runat="server" Text="Submit" Visible="False" />
        </div> 
        </div>
       
 <div style="text-align:left">        <asp:RegularExpressionValidator  Display="None"
    id="RegtxtAcc"  
    runat="server" 
    ControlToValidate="txtAName" 
    ValidationExpression="^[0-9a-zA-Z- %]+$"
    ErrorMessage="Account Name - Please enter valid input."
   />
 
 <asp:RegularExpressionValidator  Display="None"
    id="RegTxtSpeciality"  
    runat="server" 
    ControlToValidate="TxtSpeciality" 
    ValidationExpression="^[a-zA-Z ]+$"
    ErrorMessage="Speciality - Please enter valid input."
   />
 <%--<asp:RegularExpressionValidator  Display="None"
    id="RegTxtEmail"  
    runat="server" 
    ControlToValidate="TxtEmail" 
    ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"
    ErrorMessage="EMail - Please enter valid input."
   />--%>
 <asp:RegularExpressionValidator  Display="None"
    id="RegTxtPhoneno"  
    runat="server" 
    ControlToValidate="TxtPhoneno" 
    ValidationExpression="^[0-9-]+$"
    ErrorMessage="Phone Number - Please enter valid input."
   />
 <asp:RegularExpressionValidator  Display="None"
    id="RegtxtFax"  
    runat="server" 
    ControlToValidate="txtFax" 
    ValidationExpression="^[0-9-]+$"
    ErrorMessage="Fax - Please enter valid input."
   />
</div> 
    <asp:ValidationSummary ID="ValidationSummary1" runat="server" HeaderText="Following error occurs:" ShowMessageBox="true" DisplayMode="BulletList" ShowSummary="False" /> </form>

    
</body>
</html>
