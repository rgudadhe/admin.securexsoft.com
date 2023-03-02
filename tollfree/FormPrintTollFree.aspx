<%@ Page Language="VB" AutoEventWireup="false" CodeFile="FormPrintTollFree.aspx.vb" Inherits="FormPrintTollFree" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>Toll Free Instruction</title>

	<link href= "../App_Themes/Css/Main.css" type="text/css" rel="stylesheet" />
    <link href= "../App_Themes/Css/Common.css" type="text/css" rel="stylesheet" />
    <link href= "../App_Themes/Css/Styles.css" type="text/css" rel="stylesheet" />
        <link href= "../App_Themes/Css/select2.min.css" type="text/css" rel="stylesheet" />
       <script type="text/javascript" src="../App_Themes/JS/select2.min.js"></script>
       
       <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.2/css/bootstrap.min.css" />
<script type="text/javascript" src="https://ajax.googleapis.com/ajax/libs/jquery/1.9.1/jquery.min.js"></script>
<script type="text/javascript" src="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.2/js/bootstrap.min.js"></script>
<link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/select2/4.0.6-rc.0/css/select2.min.css" />
<script type="text/javascript" src="https://cdnjs.cloudflare.com/ajax/libs/select2/4.0.6-rc.0/js/select2.min.js"></script>
<style type="text/css">
    .select2-container .select2-selection--single {
        height: 25px !important;
 
    }
 
    .select2-container--default .select2-selection--single {
        border: 1px solid #ccc !important;
        border-radius: 0px !important;
       
    }
</style>
<script type="text/javascript">
    $(function() {
        $('.select2').select2();
        $('.select2').on('change', function() {
            $('[id*=hfCustomerId]').val($(this).val());
        });
    });
</script>
</head>
<body>
<center>
    <form id="form1" runat="server">
	<div id="body" style="height: 100%;" >
    <div id="cap"></div>
    <div id="main">
    <div>
   <h1>Toll Free Instruction</h1>
 

   <table width="70%">
   
        <tr>
            <td colspan="4" style="text-align: center; height: 15px;" class="HeaderDiv">
                <strong><span style="color: Black">Select Account from Partitions Below</span></strong></td>
        </tr>
        <tr>
        
        <td colspan="4" style="text-align: center; height: 15px; font-weight:bold;">HASH Partition <span style="font-size: 8pt">[866-239-1729 / 800-385-4418</span>]</td>
        </tr>
        <tr>
        <td colspan="2" style="text-align: right; height: 15px;">Account Name</td>
            <td colspan="2">
                <asp:DropDownList class="form-control select2"  ID="hashact" runat="server" AppendDataBoundItems="true">
                </asp:DropDownList></td>
            
        </tr>
        <tr>
         <td colspan="4" style="text-align: center;">
                <asp:Button ID="Button1" runat="server" Text="   Print   " CssClass="button" /></td>
        </tr>
        <tr>
        <td colspan="4" style="text-align: center; height: 15px; font-weight:bold">No HASH Partition <span style="font-size: 8pt">[800-801-9270 / 866-890-5003]</span></td>
        </tr>
        <tr>
            <td  colspan="2" align="center" style="text-align: right; height: 15px;">Account Name</td>
            <td colspan="2"><asp:DropDownList ID="nhashact" class="form-control select2"  runat="server" AppendDataBoundItems="true">
                </asp:DropDownList></td>
            
        </tr>
        <tr style="font-size:smaller; font-weight:bold">
            
        </tr>
        <tr>
           
            <td colspan="4" style="text-align: center;">
                <asp:Button ID="Button2" runat="server" Text="   Print   " CssClass="button" /></td>
        </tr>
        <tr>
            <td colspan="4" style="text-align: center; height: 15px;" class="HeaderDiv"><strong><span style="color: black">Account wise Physician Details from Below</span></strong></td>
        </tr>
        <tr>
            <td colspan="2" style="text-align: right; height: 15px;">Account Name</td>
            <td colspan="2" align="left"><asp:DropDownList ID="ddlCustomers" runat="server"  class="form-control select2" AppendDataBoundItems="true">
                </asp:DropDownList>
                <br />
                <%-- <select class="form-control select2" runat="server" id="ddlCustomers">
                </select>--%>
                
                </td>
        </tr>
        <tr>
            <td colspan="4" style="text-align: center;">
                <asp:Button ID="Button3" runat="server" Text="   View   " CssClass="button"/></td>
             
        </tr>
       
        <tr>
            <td colspan="4" style="text-align: center; height: 15px;" class="HeaderDiv"><strong><span style="color: black">
                Search ID</span></strong></td>
             
        </tr>
       
        <tr>
            <td colspan="2" style="text-align: right; height: 15px;">
                Enter ID</td>
             
            <td colspan="2" align="left">
                <asp:TextBox ID="txtpid" runat="server" style="text-align: left"></asp:TextBox>
                                  </td>
             
        </tr>
       
        <tr>
            <td colspan="4" style="text-align: center;">
                <asp:Button ID="Button4" runat="server" Text="Search" CssClass="button" />
            </td>
             
        </tr>
       
    </table>
    
    </div>
	</div>
    </form>
</center>
</body>
<script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>

<script type="text/javascript">

    $(function() {

    $("[id*=Button3]").click(function() {

        var ddlFruits = $("[id*=vact]");

        var selectedText = vact.find("option:selected").text();

        var selectedValue = vact.val();

            alert("Selected Text: " + selectedText + " Value: " + selectedValue);

            return false;

        });

    });

</script>


</html>
