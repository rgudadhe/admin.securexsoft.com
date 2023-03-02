<%@ Page Language="VB" AutoEventWireup="false" CodeFile="AccountAssignNew.aspx.vb" Inherits="AccountAssignNew" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="Ajax" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 1.0 Transitional/EN">
<script language="javascript" type="text/javascript">
function onlyNumbers(evt)
{
	var e = event || evt; // for trans-browser compatibility
	var charCode = e.which || e.keyCode;

	if (charCode > 31 && (charCode < 48 || charCode > 57))
		return false;

	return true;

}


    function Test()
    {
        var varTempName;
        var varTempNo;
        for(i=0;i<document.frmManage.elements.length;i++)
        {
            if(document.frmManage.elements[i].name.indexOf("txtNo")>-1)
            {
                varTempNo=document.frmManage.elements[i].name;
            }
        }
        var Chk = document.getElementById(varTempNo).value.match(/^\d{10}$/)

        if (document.getElementById(varTempNo).value == '' || Chk == null)
        {
            alert('Please enter 10 digit fax number');
            document.getElementById(varTempNo).select();
            document.getElementById(varTempNo).focus();
            return false;
        }
        return true;
    }
    
    function Chk()
    {
        var varTempName;
        var varTempNo;
        for(i=0;i<document.frmManage.elements.length;i++)
        {
            if(document.frmManage.elements[i].name.indexOf("txtNo")>-1)
            {
                varTempNo=document.frmManage.elements[i].name;
            }
        }
        if(document.getElementById(varTempNo).value == '')
        {
            alert('Please enter PGI code')
            return false;
        }
        return true;
    }
    function SetValue()
    {
        var str =''
        var count=0;
        for (i = 0; i < document.frmManage.lstAssigned.options.length; i++) 
        {
            if (document.frmManage.lstAssigned.options[i].selected) 
            {
                if (str=='')
                {
                    str = document.frmManage.lstAssigned.options[i].value + ''
                }
                else
                {
                    str = str + ',' + document.frmManage.lstAssigned.options[i].value + ''
                }
                count = count + 1
            }
        }
        str = str + ''
        //alert(str)
        if (count > 0)
        {
            document.getElementById('hdnIndex').value= str
        }
        else
        {
            alert('Please select fax no')
            return false;
        }
        return true;
    }
</script>
<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Assigned Fax No</title>
    <link href= "../styles/Default.css" type="text/css" rel="stylesheet"/>    
</head>
<body>
    <form id="frmManage" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
    <div>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <table border="0" width="100%" cellpadding="2" style="border-collapse: collapse" bordercolor="#111111" align=center>
                    <%--<tr background="Images/settings.jpg">
                        <th colspan="2" align="center" background="Images/settings.jpg" height="27" style="border-bottom-style: none; border-bottom-width: medium">
                        <p align="left"><span class="style15"><font face="Trebuchet MS" size="2">Assigned Fax No</font></span></th>
                    </tr >--%>
<%--                    <tr align=left>
                        <td style="font-family:Trebuchet MS; font-size:12px; height: 29px;" colspan=2 > 
                            &nbsp; &nbsp; &nbsp;&nbsp;&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                            &nbsp; &nbsp; &nbsp;&nbsp;&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                            &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                            &nbsp;<input id="RadAll" name="Rad" runat="server"  type="radio" onclick="clickCheck(this);" /> ALL
                            <input id="RadFax" name="Rad" runat="server"  type="radio" onclick="clickCheckFAX(this);" /> Fax Service
                            
                            <asp:RadioButton ID="RadAll" GroupName="Rad" runat="server" Font-Names="Trebuchet MS" Font-Size="12px" Text="ALL" AutoPostBack=true  />
                            <asp:RadioButton ID="RadFAX" GroupName="Rad" runat="server" Font-Names="Trebuchet MS" Font-Size="12px" Text="Only Fax Service" AutoPostBack=true />
                            
                            <asp:HiddenField ID="hdnFax" runat="server" />
                            <asp:HiddenField ID="hdnChk" runat="server" />
                        </td>
                    </tr>--%>
                    <tr align=center>
                        <td style=" font-family:Trebuchet MS; font-size:12px;" align="center">
                            Account Name 
                            <asp:DropDownList ID="DropDownAccount" runat="server" Font-Names="Trebuchet MS" Font-Size="12px" Width=300px AutoPostBack=true>
                            </asp:DropDownList>
                        </td>
                        <%--<td width="200px" align=left>
                            <asp:CheckBox ID="chkFAXService" Text="Fax Service Account" Font-Names="Trebuchet MS" Font-Size="12px" runat="server" AutoPostBack=true EnableViewState=true   />
                        </td>--%>
                    </tr>
                 </table>
                 <BR>
                <asp:Table ID="Table3" runat="server" Width=40% HorizontalAlign=Center>
                    <asp:TableRow>
                        <asp:TableCell>
                            <Ajax:Accordion ID="Accordion" runat="server" HeaderCssClass="accordionHeaderPastTickets" SelectedIndex=-1 FadeTransitions="true" FramesPerSecond="40" 
                                                TransitionDuration="250" AutoSize=None  RequireOpenedPane="false" SuppressHeaderPostbacks="true">
                                <Panes>
                                    <Ajax:AccordionPane ID="AccordionPaneNew" runat=server>
                                        <%--<Header>
					                        <font face="Trebuchet MS" style="font-family:Trebuchet MS; font-size:12px; color:Blue; font-style:italic; cursor:hand; border-bottom-style:solid; border-color:Violet; background-image:'~/Images/blank.bmp';">Click here to add new contact</font>    
                                        </Header>--%>
                                        <Header>
                                            <asp:ImageButton ID="ImageButton1" ImageUrl="~/Images/NewContact1.bmp" CausesValidation=false runat="server" />
                                        </Header>
                                        <Content>
                                            <asp:Table ID="Table1" runat="server" GridLines=Both Width=100%>
                                                <asp:TableRow CssClass="SMSelected">
                                                    <asp:TableCell Font-Names="Trebuchet MS" Font-Size="12px" Font-Italic=true Width=40% HorizontalAlign=Center>
                                                        PGI Code
                                                    </asp:TableCell>
                                                    <asp:TableCell></asp:TableCell>
                                                </asp:TableRow>
                                                <asp:TableRow>
                                                    <asp:TableCell Width=90%>
                                                        <asp:TextBox ID="txtNo" name="txtNo" Font-Names="Trebuchet MS" Font-Size="12px" runat="server" Width=90% onkeypress="return onlyNumbers();"></asp:TextBox>
                                                    </asp:TableCell>
                                                    <asp:TableCell Width=90%>
                                                        <asp:ImageButton ID="btnAdd" runat="server" ImageUrl="~/Images/add.jpg" OnClientClick="javascript:return Chk()" />
                                                    </asp:TableCell>                                
                                                </asp:TableRow>
                                            </asp:Table>
                                        </Content>
                                    </Ajax:AccordionPane>
                                </Panes>    
                            </Ajax:Accordion>    
                        </asp:TableCell>
                    </asp:TableRow>
                </asp:Table>
                <asp:Table ID="tblMain" runat="server" Width=40% Height="200px" HorizontalAlign=Center GridLines=Both >
                    <asp:TableRow ID="TableRow1" CssClass="HeaderDiv" runat="server">
                        <asp:TableCell ID="TableCell1" ColumnSpan=2 HorizontalAlign=Center runat="server" Font-Names="Trebuchet MS" Font-Size="12px">
                            Assigned Fax No
                        </asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow ID="TableRow2" runat="server">
                        <asp:TableCell ID="TableCell4" runat="server" HorizontalAlign=Center Font-Names="Trebuchet MS" Font-Size="12px" Width=90% >
                            <asp:ListBox ID="lstAssigned" SelectionMode=Multiple runat="server" Font-Names="Trebuchet MS" Height=180 Font-Size="12px" Width=90% EnableViewState=true >
                            </asp:ListBox>
                        </asp:TableCell>
                        <asp:TableCell>
                            <asp:ImageButton ID="btnRemove" runat="server" ImageUrl="~/Images/del.bmp" OnClientClick="javascript:return SetValue()" />
                            <asp:HiddenField ID="hdnIndex" runat="server" />
                        </asp:TableCell>
                    </asp:TableRow>
                </asp:Table>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
    </form>
</body>
</html>
