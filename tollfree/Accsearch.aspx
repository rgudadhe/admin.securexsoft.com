<%@ Page Language="VB" AutoEventWireup="false" CodeFile="Accsearch.aspx.vb" Inherits="tollfree_Accsearch" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<title>jQuery Show Records Found Message in AutoComplete</title>
<link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.2/css/bootstrap.min.css" />
<script type="text/javascript" src="https://ajax.googleapis.com/ajax/libs/jquery/1.9.1/jquery.min.js"></script>
<script type="text/javascript" src="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.2/js/bootstrap.min.js"></script>
<link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/select2/4.0.6-rc.0/css/select2.min.css" />
<script type="text/javascript" src="https://cdnjs.cloudflare.com/ajax/libs/select2/4.0.6-rc.0/js/select2.min.js"></script>
<style type="text/css">
    .select2-container .select2-selection--single {
        height: 34px !important;
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
<form id="form1" runat="server">
    <div class="container">
        <div class="row">
            <div class="col-md-4">
                <label>Select</label><%--<asp:DropDownList  class="form-control select2" ID="ddlCustomers" runat="server">
                </asp:DropDownList>
                --%>
                <select class="form-control select2" runat="server" id="ddlCustomers">
                </select>
                <%--<asp:Button ID="Button1" runat="server" CssClass="btn btn-primary" Text="Submit" OnClick="OnSubmit" />--%>
            </div>
        </div>
        <asp:HiddenField ID="hfCustomerId" runat="server" />
    </div>
</form>
</body>


</html>

