<%@ Page Language="VB" AutoEventWireup="false" CodeFile="GenLC_11222020.aspx.vb" Inherits="GenLC_New" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Generate Unit Count</title>
    <link href= "../../App_Themes/Css/styles.css" type="text/css" rel="stylesheet"/>
    <link href= "../../App_Themes/Css/Common.css" type="text/css" rel="stylesheet"/>
<script type="text/javascript" src="https://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
<script type="text/javascript">
    function ShowProgress() {
        setTimeout(function() {
            var modal = $('<div />');
            modal.addClass("modal");
            $('body').append(modal);
            var loading = $(".loading");
            loading.show();
            var top = Math.max($(window).height() / 2 - loading[0].offsetHeight / 2, 0);
            var left = Math.max($(window).width() / 2 - loading[0].offsetWidth / 2, 0);
            loading.css({ top: top, left: left });
        }, 200);
    }
    $('form').live("submit", function() {
        ShowProgress();
    });
</script>
<style type="text/css">
    .modal
    {
        position: fixed;
        top: 0;
        left: 0;
        background-color: black;
        z-index: 99;
        opacity: 0.8;
        filter: alpha(opacity=80);
        -moz-opacity: 0.8;
        min-height: 100%;
        width: 100%;
    }
    .loading
    {
        font-family: Arial;
        font-size: 10pt;
        border: 5px solid #67CFF5;
        width: 200px;
        height: 220px;
        display: none;
        position: fixed;
        background-color: White;
        z-index: 999;
    }
</style>
   <script type="text/javascript"  language="JavaScript">
       function RemDetails1(BillAccID) {
           newwindow = window.open('RemLCDetails.aspx?BillAccID=' + BillAccID, 'name', 'height=200,width=400, left=300, top=100, scrollbars=1');
           	        if (window.focus) {newwindow.focus()}
       }
function RemDetails() {
  //  alert('load');
   document.frmTrans.HReptID.value='';
var checkbox_choices = 0;
var j = 0;
var str;
for (var i=0;i<document.frmTrans.elements.length;i++) {
   // alert(document.frmTrans.elements[i].name);
    if (Right(document.frmTrans.elements[i].name, 3) == 'chk' && document.frmTrans.elements[i].checked==true) {
      //  alert(document.frmTrans.elements[i].value);
        if (j==0)
        {
            str = document.frmTrans.elements[i].value;
        }
        else
        {
            str = str + ',' + document.frmTrans.elements[i].value;
        } 
    checkbox_choices = checkbox_choices + 1; 
    j=j+1;
    }
}
//alert(str);
    if (checkbox_choices > 0 ) {
        document.frmTrans.HReptID.value = str;
        return true;
//        newwindow = window.open('RemLCDetails.aspx?BillAccID=' + str, 'name', 'height=200,width=400, left=300, top=100, scrollbars=1');
//	        if (window.focus) {newwindow.focus()}
    }
    else
    {
        alert("Please select reports to Open!");
        return false;
    }
} //alert('RemLCDetails.aspx?BillAccID='+BillAccID+'&BillCycle='+BillCycle+'&BillMonth='+BillMonth+'&BillYear='+BillYear);

function Right(str, n) {
    if (n <= 0)
        return "";
    else if (n > String(str).length)
        return str;
    else {
        var iLen = String(str).length;
        return String(str).substring(iLen, iLen - n);
    }
}
function changeAll() {
    if (document.frmTrans.Checkbox1.checked) {
        elval = true;
    } else {
        elval = false;
    }
    for (var i = 0; i < document.frmTrans.elements.length; i++) {
        document.frmTrans.elements[i].checked = elval;
        if (Right(document.frmTrans.elements[i].name, 3) == 'chk') {
            highlightRow(document.frmTrans.elements[i]);
        }
    }

}

function highlightRow(InputNode) {
    var el = InputNode;
    while (el.nodeName.toLowerCase() != 'tr')
        el = el.parentNode;
    if (InputNode.checked) {
        el.style.backgroundColor = '#eee8aa';
        ChVal = true;
        for (var i = 0; i < document.frmTrans.elements.length; i++) {
            if (Right(document.frmTrans.elements[i].name, 3) == 'chk' && document.frmTrans.elements[i].checked == false) {

                ChVal = false;
            }
        }
    }
    else {
        el.style.backgroundColor = '#d7dbdd';
        ChVal = false;
        for (var i = 0; i < document.frmTrans.elements.length; i++) {
            if (Right(document.frmTrans.elements[i].name, 3) == 'chk' && document.frmTrans.elements[i].checked == false) {
                ChVal = false;
            }
        }

    }
    document.frmTrans.Checkbox1.checked = ChVal;

}
</script> 
</head>
<body style="text-align: center">
    <form id="frmTrans" runat="server">
   <%--  <img src="loader.gif" alt="" />--%>
    <asp:HiddenField ID="HReptID" runat="server" />
        <div id="body">
        <div id="cap"></div>
        <div id="main">
        <h1>Generate Unit Count</h1>
        <asp:Panel ID="Panel1" runat="server" HorizontalAlign="Left">
            <table width="80%">
                <tr>
                    <td colspan="4" class="HeaderDiv">
                        Account Activity Details 
                    </td>
                </tr>
                <tr>
                    <td class="alt1">
                        Account Name
                    </td>
                    <td class="alt1">
                        Cycle
                    </td>
                    <td class="alt1">
                        Month
                    </td>
                    <td class="alt1">
                        Year
                    </td> 
                </tr>
                <tr>
                <td>
                    <asp:DropDownList ID="DLAct" runat="server" CssClass="common">
                        <asp:ListItem Text="Select Account" Value=""></asp:ListItem> 
                        <asp:ListItem Text="All Accounts" Value="All"></asp:ListItem> 
                    </asp:DropDownList>
                </td>
                <td>
                    <asp:DropDownList ID="DLCycle" runat="server" CssClass="common">
                        <asp:ListItem Text="Select Cycle" Value=""></asp:ListItem> 
                        <asp:ListItem Text="Cycle1" Value="1"></asp:ListItem> 
                        <asp:ListItem Text="Cycle2" Value="2"></asp:ListItem> 
                    </asp:DropDownList>
                </td>
                <td>
                     <asp:DropDownList ID="DLMonth" runat="server" CssClass="common">
                       
                    </asp:DropDownList>
                </td>
                <td>
                    <asp:DropDownList ID="DLYear" runat="server" CssClass="common">
                       
                    </asp:DropDownList>
                </td> 
            </tr>
            <tr>
                <td colspan="4" class="alt1">
                    <center>
                        <asp:Button ID="btnSubmit" CssClass="button"  runat="server" Text="Submit" /> 
                    </center>
                </td>
            </tr>
        </table>
        <br />
         <asp:Label Visible="false"  ID="Label1" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="8" ForeColor="Red"></asp:Label>
       <%-- <asp:Table ID="Table1" runat="server" CssClass="common" Width="100%">
            <asp:TableRow ID="TableHeaderRow2" runat="server">
                <asp:TableCell ID="TableCell1" CssClass="alt1" runat="server">
                    Sr No
                </asp:TableCell> 
                <asp:TableCell ID="TableCell2" CssClass="alt1" runat="server">
                    Account Name
                </asp:TableCell> 
                <asp:TableCell ID="TableCell6" CssClass="alt1" runat="server">
                    Mode
                </asp:TableCell> 
                <asp:TableCell ID="TableCell7" CssClass="alt1" runat="server">
                    Report#
                </asp:TableCell>
                <asp:TableCell ID="TableCell8" CssClass="alt1" runat="server">
                    Units 
                </asp:TableCell>
                <asp:TableCell ID="TableCell9" CssClass="alt1" runat="server">
                    Method
                </asp:TableCell>
                <asp:TableCell ID="TableCell10" CssClass="alt1" runat="server">
                    LocName
                </asp:TableCell>
            </asp:TableRow>
        </asp:Table>--%>
        <%--    <asp:Button ID="btnRemvoe" runat="server" Text="Remove Data" />--%>
        <asp:Table ID="tblError" runat="server"  Width="600px" Visible="false"  >
         <asp:TableRow ID="TableRow4" runat="server">
                <asp:TableCell ID="TableCell13" ColumnSpan="3" CssClass="alt1" HorizontalAlign="Center"  BackColor="Red" ForeColor="White"   runat="server">
                   EXCEPTION DETAILS
                </asp:TableCell>
            </asp:TableRow>
            <asp:TableRow ID="TableRow1" runat="server">
                <asp:TableCell ID="TableCell1" CssClass="alt1" runat="server">
                   Sr. No.
                </asp:TableCell> 
                <asp:TableCell ID="TableCell2" CssClass="alt1" runat="server">
                    Account Name
                </asp:TableCell> 
                <asp:TableCell ID="TableCell6" CssClass="alt1" runat="server">
                    Error
                </asp:TableCell>
            </asp:TableRow>
        </asp:Table>
        <asp:Table ID="tblSuccess" runat="server"  Width="600px"  Visible="false">
        <asp:TableRow ID="TableRow5" runat="server">
                <asp:TableCell ID="TableCell14" ColumnSpan="3" HorizontalAlign="Center" CssClass="alt1"   runat="server">
                  GENERATED UNIT DETAILS
                </asp:TableCell>
            </asp:TableRow>
            <asp:TableRow ID="TableRow2" runat="server">
                <asp:TableCell ID="TableCell7" CssClass="alt1" runat="server">
                    Sr. No.
                </asp:TableCell> 
                <asp:TableCell ID="TableCell8" CssClass="alt1" runat="server">
                    Account Name
                </asp:TableCell> 
                <asp:TableCell ID="TableCell9" CssClass="alt1" runat="server">
                    Status
                </asp:TableCell>
            </asp:TableRow>
        </asp:Table>
           
        <asp:Table ID="Table2" runat="server"  Width="600px" Visible="false">
        <asp:TableRow ID="TableRow8" runat="server">
                <asp:TableCell ID="TableCell17" ColumnSpan="3"   runat="server">
                 <%--  <input id="Button1" type="button" value="Remove Data" class="button"  onclick="RemDetails('')" />--%>
                    <asp:Button ID="Button1" runat="server" Text="Remove Data" CssClass="button"  OnClientClick="return RemDetails();"  />
                </asp:TableCell>
            </asp:TableRow>
         <asp:TableRow ID="TableRow6" runat="server">
                <asp:TableCell ID="TableCell15" ColumnSpan="3" CssClass="alt1"  BackColor="Yellow" ForeColor="Black" runat="server">
                  ACCOUNT DETAILS
                </asp:TableCell>
            </asp:TableRow>
            <asp:TableRow ID="TableHeaderRow1" runat="server">
                <asp:TableCell ID="TableCell3" CssClass="alt1" runat="server">
                    <input id="Checkbox1" type="checkbox" onclick="changeAll()" />
                </asp:TableCell> 
                <asp:TableCell ID="TableCell4" CssClass="alt1" runat="server">
                    Account Name
                </asp:TableCell> 
                <asp:TableCell ID="TableCell5" CssClass="alt1" runat="server">
                    Status
                </asp:TableCell>
            </asp:TableRow>
        </asp:Table>
         <asp:Table ID="tblNoRec" runat="server" Width="600px"  Visible="false">
          <asp:TableRow ID="TableRow7" runat="server">
                <asp:TableCell ID="TableCell16"  ColumnSpan="3" HorizontalAlign="Center" CssClass="alt1"   runat="server">
                  ACCOUNT DETAILS
                </asp:TableCell>
            </asp:TableRow>
            <asp:TableRow ID="TableRow3" runat="server">
                <asp:TableCell ID="TableCell10" CssClass="alt1" runat="server">
                   Sr. No.
                </asp:TableCell> 
                <asp:TableCell ID="TableCell11" CssClass="alt1" runat="server">
                    Account Name
                </asp:TableCell> 
                <asp:TableCell ID="TableCell12" CssClass="alt1" runat="server">
                    Status
                </asp:TableCell>
            </asp:TableRow>
        </asp:Table>
        </asp:Panel>
        </div> 
        </div>
        <div class="loading" align="center">
   
    <br />
    <img src="200.gif" alt="" />
</div> 
    </form>
</body>
</html>
