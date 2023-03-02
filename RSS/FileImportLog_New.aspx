<%@ Page Language="VB" AutoEventWireup="false" CodeFile="FileImportLog_New.aspx.vb" Inherits="RSS_FileImportLog_New" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>File Import Log</title>
    <link href= "../App_Themes/Css/styles.css" type="text/css" rel="stylesheet" />
    <link href= "../App_Themes/Css/Common.css" type="text/css" rel="stylesheet" />
    <link href="../App_Themes/Css/DataTable.css" rel="stylesheet" type="text/css" />
    <link href="../App_Themes/Css/TableSorter.css" rel="stylesheet" type="text/css" />
    <link href="../App_Themes/Css/Calendar.css" rel="stylesheet" type="text/css" />
    <script src="../App_Themes/JS/jquery-1.4.2.min.js" type="text/javascript"></script>  
    <script src="../App_Themes/JS/jquery.dataTables.min.js" type="text/javascript"></script>  
    <script type="text/javascript" language="javascript">
        function listenaudio(openURL, TransID, Type, JobNumber, CustJobID) {
            Type = '.'+Type.split('.').pop();
            //alert(openURL);
            if (openURL == "Voice") {
                url = "../Dictation Search/SaveAsDictation.aspx?transid=" + TransID + "&Type=" + Type + "&JobNumber=" + JobNumber + "&CustJobID=" + CustJobID;
                newwindow = window.open(url, 'OpenVoice', "width=500,height=250, top=200, left=200, scrollbars=1,menubar=0,toolbar=0,location=0,status=0");
            }
            else {
                url = "../Dictation Search/ShowPDF.aspx?transid=" + TransID + "&Type=" + Type + "&JobNumber=" + JobNumber + "&CustJobID=" + CustJobID;
                newwindow = window.open(url, 'OpenPDF', "width=" + screen.width + ",height=" + screen.height + ", top=0, left=0, scrollbars=1,menubar=0,toolbar=0,location=0,status=0");
            }

            if (window.focus) { newwindow.focus() }
        }
 function CheckAllDataGridCheckBoxes(aspCheckBoxID, checkVal)
 {
  alert(checkVal);
  re = new RegExp(':' + aspCheckBoxID + '$')  //generated control name starts with a colon
  for(i = 0; i < form1.elements.length; i++)
  {
   elm = document.forms[0].elements[i]
   alert(elm.type);
   if (elm.type == 'checkbox')
   {
    //if (re.test(elm.name))
     elm.checked = checkVal
   }
  }
 }
 
        function chkALL(str)
        {
            for(i = 0; i < form1.elements.length; i++)
            {
                elm = document.forms[0].elements[i];
                if (elm.type == 'checkbox')
                {
                    elm.checked = str.checked;
                }
            }
        }
 
</script>
    <script type="text/javascript" language="javascript">
        function openPopup(str)
        {
            window.open('JobHistory.aspx?MD5Value='+str,'', 'width=550,height=240,status=1,scrollbars=1')
            return false;
        }
        function waitPreloadPage() { //DOM
if (document.getElementById){
document.getElementById('prepage').style.visibility='hidden';
}else{
if (document.layers){ //NS4
document.prepage.visibility = 'hidden';
}
else { //IE4
document.all.prepage.style.visibility = 'hidden';
}
}
}
    </script>
    
    <script type="text/javascript" language="javascript">
    $(document).ready(function() {
				$('#dlist').dataTable( {
					//"sPaginationType": "full_numbers"
                    "aoColumns": [
                                    { "bSortable": false },
                            		{ "bSortable": false },
		                            { "asSorting": [ "desc", "asc" ] },
		                            { "asSorting": [ "desc", "asc" ] },
		                            { "asSorting": [ "desc", "asc" ] },
		                            { "asSorting": [ "desc", "asc" ] },
		                            { "asSorting": [ "desc", "asc" ] },
		                            { "asSorting": [ "desc", "asc" ] },
                            		{ "asSorting": [ "desc", "asc" ] },
		                            { "asSorting": [ "desc", "asc" ] }
	                              ] 
				} );
			} );
</script>
</head>
<body onload="waitPreloadPage();">
<div id="prepage" style="position:absolute; font-family:arial; font-size:16; left:35%; top:20%; background-color:white; "> 
<table><tr><td style="border:0"><img alt="" src="../App_Themes/Images/loading.gif" width="200" height="200" /></td></tr></table>
</div>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
    <div id="body">
        <div id="cap"></div>
        <div id="main" style="text-align:left ">
        <h1>File Import Log</h1>
            <table style="text-align:left">
                <tr>
                    <td class="alt1">Customer Job#</td>
                    <td class="alt1">MD5 Value</td>
                    <td class="alt1">Status</td>
                    <td class="alt1">Start Date</td>
                    <td class="alt1">End Date</td>
                    <td class="alt1">File Name</td>
                    <td style="border:0">&nbsp;</td>
                </tr>
                <tr>
                    <td><asp:TextBox ID="txtCJNum" runat="server"></asp:TextBox></td>
                    <td><asp:TextBox ID="txtMD5" runat="server"></asp:TextBox></td>
                    <td>
                        <asp:DropDownList ID="ddlStatus" runat="server" Width="70px">
                            <asp:ListItem Selected="True" Value="" >Any</asp:ListItem>
                            <asp:ListItem Value="1">Imported</asp:ListItem>
                            <asp:ListItem Value="0">Not Imported</asp:ListItem>
                            <asp:ListItem Value="2">Duplicate</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td>
                        <asp:TextBox ID="sDate" runat="server" Width="55px"></asp:TextBox><asp:ImageButton ID="ImgBntsDate" runat="server" CausesValidation="False" ImageUrl="~/App_Themes/Images/Calendar_scheduleHS.png" />
                        <asp:RegularExpressionValidator  Display="None" ID="RegularExpressionValidator1" SetFocusOnError="true" runat="server" ErrorMessage="Invalid Start Date (MM/DD/YYYY)" ControlToValidate="sDate" ValidationExpression="\d{2}/\d{2}/\d{4}">*</asp:RegularExpressionValidator>
                        <ajaxToolkit:ValidatorCalloutExtender runat="Server" ID="ValidatorCalloutExtender2" TargetControlID="RegularExpressionValidator1" />    
                        <ajaxtoolkit:calendarextender id="CalendarExtender1" runat="server" popupbuttonid="ImgBntsDate" CssClass="cal_Theme1"
                        targetcontrolid="sDate" Format="MM/dd/yyyy"></ajaxtoolkit:calendarextender>
                    </td>
                    <td>
                        <asp:TextBox ID="eDate" Width="55px" runat="server"></asp:TextBox><asp:ImageButton ID="ImgBnteDate" runat="server" CausesValidation="False" ImageUrl="~/App_Themes/Images/Calendar_scheduleHS.png" />
                        <asp:RegularExpressionValidator  Display="None" ID="RegularExpressionValidator2" runat="server" ControlToValidate="eDate" SetFocusOnError="true"
                            ErrorMessage="Invalid End Date (MM/DD/YYYY)" ValidationExpression="\d{2}/\d{2}/\d{4}"></asp:RegularExpressionValidator>&nbsp;
                        <ajaxToolkit:ValidatorCalloutExtender runat="Server" ID="ValidatorCalloutExtender1" TargetControlID="RegularExpressionValidator2" />    
                        <ajaxtoolkit:calendarextender id="CalendarExtender2" Format="MM/dd/yyyy" runat="server" popupbuttonid="ImgBnteDate" CssClass="cal_Theme1"
                        targetcontrolid="eDate"></ajaxtoolkit:calendarextender>
                    </td>
                    <td>
                        <asp:TextBox ID="txtClient" runat="server"></asp:TextBox>
                        <asp:CompareValidator ID="CompareValidator1" runat="server" Display="None" ErrorMessage="End date can not be less than Start Date" ControlToCompare="sDate" ControlToValidate="eDate" Operator="GreaterThanEqual" SetFocusOnError="true" Type="Date"></asp:CompareValidator>
                        <ajaxToolkit:ValidatorCalloutExtender runat="Server" ID="NReqE1" TargetControlID="CompareValidator1" />
                    </td>
                    <td style="border:0">
                        <asp:Button ID="btnSearch" runat="server" Text="Search" CssClass="button" /><br />
                    </td>
                </tr>
                <tr>
                    <td colspan="7" style="text-align:left; border:0"><br /></td>
                </tr>
                <tr id="dlistRow" runat="server">
                    <td colspan="7" style="text-align:left; border:0">


                        <asp:GridView ID="dlist" runat="server" AutoGenerateColumns="False" Width="99%">
                            <Columns>
                                <asp:TemplateField HeaderStyle-BackColor="Transparent">
                                    <ItemTemplate>
                                            <asp:ImageButton ID="btnHistory" runat="server" ImageUrl="~/App_Themes/Images/his.png" ToolTip="Job history"   />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderStyle-BackColor="Transparent">
                                    <headertemplate>				            									
				                        <input id="chkAll" type="checkbox" style=" background-color:Transparent" onclick="chkALL(this);"/>	
				                    </headertemplate>	                    
							        <itemtemplate>									
							            <asp:CheckBox ID="chkJob" runat="server"    />
                                        <asp:HiddenField ID="hdnID" Value='<%#Container.DataItem("RecordID")%>' runat="server" />
                                        <asp:HiddenField ID="hdnFileName" Value='<%#Container.DataItem("FileName")%>' runat="server" />
                                        <asp:HiddenField ID="hdnCJobNumber" Value='<%#Container.DataItem("CJobNumber")%>' runat="server" />
                                        <asp:HiddenField ID="hdnProcessID" Value='<%#Container.DataItem("ProcessID")%>' runat="server" />
				                    </itemtemplate>
							        <headerstyle width="20px" />                                    
                                </asp:TemplateField>
                                <asp:TemplateField HeaderStyle-CssClass="header" headertext="Cust. Job#">
                                    <ItemTemplate>
                                        <%#IIf(IsDBNull(Container.DataItem("CJobNumber")), String.Empty, Container.DataItem("CJobNumber"))%>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderStyle-CssClass="header" HeaderText="Client">
                                    <ItemTemplate>
                                        <asp:label ID="lblClient" text='<%#mid(Container.DataItem("FileName"),1,iif(InStr(Container.DataItem("FileName"),"_")>0,InStr(Container.DataItem("FileName"),"_"),IIF(InStr(Container.DataItem("FileName"),"-")>0,InStr(Container.DataItem("FileName"),"-"),1))-1)%>' runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField headertext="MD5Value" HeaderStyle-CssClass="header">
                                    <Itemtemplate>
                                        <asp:label ID="lblMD5Value" text='<%#Container.DataItem("MD5Value")%>' runat="server" />
                                    </Itemtemplate>
                                </asp:TemplateField>
                                <asp:TemplateField headertext="File Name" HeaderStyle-CssClass="header">
                                    <Itemtemplate>
                                        <asp:label ID="lblFileName" text='<%#Container.DataItem("FileName")%>' runat="server" />
                                        <img src="../images/VolumeNormalBlue.png" style="cursor:hand; "  alt="" ondblclick="javascript:listenaudio('Voice', '<%#Container.DataItem("RecordID")%>','<%#Container.DataItem("FileName")%>','<%#Container.DataItem("CJobNumber")%>', '<%#Container.DataItem("FileName")%>')" />
                                    </Itemtemplate>
                                </asp:TemplateField>
                                <asp:TemplateField headertext="Status" HeaderStyle-CssClass="header">
                                    <Itemtemplate>
                                        <asp:label ID="lblStatus" text='<%#getStatus(Container.DataItem("Status").ToString)%>' runat="server" />
                                    </Itemtemplate>
                                </asp:TemplateField>
                                <asp:TemplateField headertext="User Name" HeaderStyle-CssClass="header">
                                    <ItemTemplate>
                                        <%#IIf(IsDBNull(Container.DataItem("UserName")), String.Empty, Container.DataItem("UserName"))%>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField headertext="Process Name" HeaderStyle-CssClass="header">
                                    <ItemTemplate>
                                        <%#IIf(IsDBNull(Container.DataItem("SettingName")), String.Empty, Container.DataItem("SettingName"))%>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField headertext="Date Processed" HeaderStyle-CssClass="header">
                                    <ItemTemplate>
                                        <%#IIf(IsDBNull(Container.DataItem("DateProcessed")), String.Empty, Container.DataItem("DateProcessed"))%>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField headertext="Version" HeaderStyle-CssClass="header">
                                    <ItemTemplate>
                                        <%#IIf(IsDBNull(Container.DataItem("Version")), String.Empty, Container.DataItem("Version"))%>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </td>
                </tr>
                <tr id="lblMsgRow" runat="server">
                    <td colspan="7" style="text-align:left; border:0">
                        <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label>
                    </td>
                </tr>
            </table>
        </div> 
        </div> 
        <br />
        <asp:ValidationSummary ID="ValidationSummary1" runat="server" HeaderText="Following error occurs:" ShowMessageBox="true" DisplayMode="BulletList" ShowSummary="False" /> 
        <div style="text-align:left">
            <asp:Button ID="btnReimport" CssClass="button" runat="server" Text="Re-Import" />
        </div>
    </form>
</body>
</html>
