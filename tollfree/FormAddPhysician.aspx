<%@ Page Language="VB" AutoEventWireup="false" CodeFile="FormAddPhysician.aspx.vb" Inherits="FormAddPhysician" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>Add a Physician</title>
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
<body style="font-size: 12pt">
      <form id="form1" runat="server">
	    <div id="body" style="height: 100%;" >
    <div id="cap"></div>
    <div id="main">
   <h1>Add new Physician</h1>
    <div>
    <table width="45%">
                <tr>
                <td colspan="2" style="text-align: center; height: 15px;" class="HeaderDiv" ">
                    Enter Details below</td>
                </tr>
                <tr>
                <td>Account Name</td><td>
                    <asp:DropDownList class="form-control select2"  ID="acct" runat="server" AppendDataBoundItems="True" AutoPostBack="true">
                    </asp:DropDownList></td>
                </tr>
                <tr>
                <td>Dictator Last Name</td><td>
                    <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox></td>
                </tr>
                <tr>
                <td>Dictator First Name</td><td>
                    <asp:TextBox ID="TextBox2" runat="server"></asp:TextBox></td>
                </tr>
                <tr>
                <td>Keypad</td><td>
                    <asp:DropDownList ID="kpad" runat="server" AppendDataBoundItems="True">
                    </asp:DropDownList></td>
                </tr>
                <tr>
                <td>ID</td><td>
                    <asp:TextBox ID="TextBox3" runat="server"></asp:TextBox></td>
                </tr>
                <tr>
                <td>Password</td><td>
                    <asp:TextBox ID="TextBox4" runat="server"></asp:TextBox></td>
                </tr>
                <tr>
                <td>Customer Type</td><td>
                    <asp:DropDownList ID="cust" runat="server" AppendDataBoundItems="True">
                    </asp:DropDownList></td>
                </tr>
        <tr>
            <td>Customer ID</td>
            <td>
                <asp:TextBox ID="txtcustid" runat="server"></asp:TextBox></td>
        </tr>
        <tr>
            <td>
                Partition</td>
            <td>
                 <asp:DropDownList ID="listpartition" runat="server" AppendDataBoundItems="True" AutoPostBack="true">
                 </asp:DropDownList>
                        </td>
        </tr>
        <tr>
            <td>Partition No</td>
            <td>
                 <asp:TextBox ID="txtpno" runat="server"></asp:TextBox>
                        </td>
        </tr>
                <tr>
                <td colspan=2 align="centre"> 
                    <asp:Button ID="Submit" runat="server" Text="Submit" CssClass="button" /></td>
                               </tr>    
    </table>
    </div>
    </form>
</body>
</html>
