<%@ Page Language="VB" AutoEventWireup="false" CodeFile="TA_PhySearch.aspx.vb" Inherits="ets.Templates_TA_PhySearch" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 1.0 Transitional/EN">

<html xmlns="http://www.w3.org/1999/xhtml" >

<head runat="server">
    <title>Template Assignments</title>
    <link href= "../App_Themes/Css/Main.css" type="text/css" rel="stylesheet"/>
    <link href= "../App_Themes/Css/Styles.css" type="text/css" rel="stylesheet" />
    <script type="text/javascript">
 <!--
 function getRadioButtonListValues()
 {
  var radioButtonListRef = document.getElementsByTagName('radio');

  for (var i=0; i<radioButtonListRef.rows.length; i++)
  {
   for (var j=0; j<radioButtonListRef.rows[i].cells.length; j++)
   {
    var listControl = radioButtonListRef.rows[i].cells[j].childNodes[0];

    alert('#' + i + ',' + j + ' checked? ' + listControl.checked);
   }
  }

 }
 // -->
 </script>

</head>
<body>
    <form id="form1" runat="server">
    <div id="body">
    <div id="cap"></div>
    <div id="main">
    <h1>New Assignments</h1>
    <div>
        <asp:Panel ID="Panel1" runat="server" HorizontalAlign="Left">
            <table style="width: 90%; text-align: left;">
            <tr>
                <td class="HeaderDiv" colspan="3" style="text-align:center;">
                    Physician Search</td>
            </tr>
            <tr>
                <td class="alt"  style="text-align:center;">
                    First Name
                </td>
                <td class="alt" style="text-align:center;">
                    Last Name
                </td>
                <td class="alt" style="text-align:center;">
                    Account Name
                </td>
                
            </tr>
            <tr>
                <td style="text-align:center;">
                    <asp:TextBox ID="txtPhyF" runat="server" Width="200px"></asp:TextBox>
                </td>
                <td style="text-align:center;">
                    <asp:TextBox ID="txtPhyL" runat="server" Width="200px"></asp:TextBox>
                </td>
                <td style="text-align:center;">
                    <asp:TextBox ID="txtPin" runat="server" Width="200px"></asp:TextBox>
                </td>
            </tr>
            
            
            <tr>
                <td colspan="3" style="text-align:center" >
                    <asp:Button ID="btnSearchPhy" CssClass="button"  runat="server" Text="Search Physician" /></td>
            </tr>
                     
            
            <tr>
                <td colspan="3" >
                    <asp:Literal ID="Literal2" runat="server"></asp:Literal>
                </td>
            </tr>
        </table>
         <asp:RegularExpressionValidator  Display="None"  
    id="RegtxtPhyF"  
    runat="server" 
    ControlToValidate="txtPhyF" 
    ValidationExpression="^[a-zA-Z-%]+$"
    ErrorMessage="Physician First - Please enter valid input."
   />
<asp:RegularExpressionValidator Display="None"  
    id="RegtxtPhyL"  
    runat="server" 
    ControlToValidate="txtPhyL" 
    ValidationExpression="^[a-zA-Z-%]+$"
    ErrorMessage="Physician Last - Please enter valid input."
   />
<asp:RegularExpressionValidator Display="None"  
    id="RegtxtPin"  
    runat="server" 
    ControlToValidate="txtPin" 
    ValidationExpression="^[a-zA-Z-%]+$"
    ErrorMessage="Account Name - Please enter valid input."
   />
        <asp:ValidationSummary ID="ValidationSummary1" runat="server" HeaderText="Following error occurs:" ShowMessageBox="true" DisplayMode="BulletList" ShowSummary="False" />      
   
        </asp:Panel>
        <asp:Panel ID="Panel2" runat="server" HorizontalAlign="Left">
            <asp:Repeater ID="rptPhy" runat="server">
         <HeaderTemplate>
<table>
            <tr>            
            <td class="alt1" align="center">Physician Name</td>            
            <td class="alt1" align="center">Account Name</td>
            <td class="alt1" align="center">Action</td>
            </tr>
</HeaderTemplate>

<ItemTemplate>
<tr>            
            <td ><asp:Label ID="txtName" runat="server" Text='<%#Container.DataItem("FirstName") & " " & Container.DataItem("LastName")%>' ></asp:label>
            <asp:HiddenField ID="PhyID" runat="server" Value='<%#Container.DataItem("PhysicianID")%>'/></td>
            <td  align="center"><asp:Label ID="Label1" runat="server" Text='<%#Container.DataItem("AccountName")%>' ></asp:label></td>            
            <td  align="center"><asp:Button  CssClass="button" ID="btnEdit" runat="server" Text="Select" OnClick="btnEdit_Click" /></td>
</tr>
</ItemTemplate>
<AlternatingItemTemplate>
<tr bgcolor="#cccccc">
             <td ><asp:Label ID="txtName" runat="server" Text='<%#Container.DataItem("FirstName") & " " & Container.DataItem("LastName")%>' ></asp:label>
            <asp:HiddenField ID="PhyID" runat="server" Value='<%#Container.DataItem("PhysicianID")%>'/></td>
            <td  align="center"><asp:Label ID="Label1" runat="server" Text='<%#Container.DataItem("AccountName")%>' ></asp:label></td>            
            <td  align="center"><asp:Button CssClass="button"  ID="btnEdit" runat="server" Text="Select" OnClick="btnEdit_Click" /></td>
</tr>
</AlternatingItemTemplate>
<FooterTemplate>
</table>
</FooterTemplate>
</asp:Repeater>
        <br />
        <br />
        <asp:Literal ID="Literal1" runat="server"></asp:Literal>
        </asp:Panel>
    </div>
    </div> 
    </div> 
    </form>
</body>
</html>
