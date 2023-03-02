<%@ Page Language="VB" AutoEventWireup="false" CodeFile="ChkProd.aspx.vb" Inherits="ChkProd" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 1.0 Transitional/EN">
<script type="text/javascript" language=javascript>


    function RemoveZeroIndex(obj)
    {
        if (obj.length > 0)
        {   
            if (obj.item(0).value =='')
            {
                obj.remove(0);
            }
        }
    }
    
    
</script>
<html xmlns="http://www.w3.org/1999/xhtml" >

<head runat="server">
    <title>Schedule Report</title>
    <link href= "../../App_Themes/Css/styles.css" type="text/css" rel="stylesheet" />
    <link href= "../../App_Themes/Css/Common.css" type="text/css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
	    <div>
            <asp:ScriptManager ID="ScriptManager1" runat="server">
                <Services>
                    
                </Services>
            </asp:ScriptManager>
            <center>
                <%--<asp:RequiredFieldValidator  Display="None" ID="RequiredFieldDropDownList1" ControlToValidate="DropDownHBA" Font-Names="Trebuchet MS" Font-Size=Small Font-Italic=true SetFocusOnError=true runat="server" ErrorMessage="Please select HBAID"></asp:RequiredFieldValidator><BR>--%>
                <asp:RequiredFieldValidator  Display="None" ID="RequiredFieldStartDate" runat="server" ErrorMessage="Please select weekstart" SetFocusOnError="True" ControlToValidate="WS"></asp:RequiredFieldValidator><BR>
                <asp:RequiredFieldValidator  Display="None" ID="RequiredFieldEndDate" runat="server"  SetFocusOnError="True" ErrorMessage="Please select weekend" ControlToValidate="WE"></asp:RequiredFieldValidator><BR><BR>
            </center>
            
            <asp:Table ID="Table1" runat="server" Width="292px" HorizontalAlign="Center">
                <asp:TableRow>
                    <asp:TableCell HorizontalAlign="Center" ColumnSpan="3" CssClass="HeaderDiv">
                        Schedule Report
                    </asp:TableCell>
                </asp:TableRow>
                <asp:TableRow ID="TableRow1" runat="server">
                    <asp:TableCell ID="TableCell2" width="120px" runat="server" CssClass="alt1">HBA ID</asp:TableCell>
                    <asp:TableCell ID="TableCell1" width="120px" runat="server" CssClass="alt1">WeekStart</asp:TableCell>
                    <asp:TableCell ID="TableCell3" width="120px" runat="server" CssClass="alt1">WeekEnd</asp:TableCell>
                </asp:TableRow>
                <asp:TableRow runat="server">
                    <asp:TableCell runat="server">
                        <%--<asp:DropDownList ID="DropDownHBA" Width=130px Font-Names="Trebuchet MS" runat="server" OnChange="RemoveZeroIndex(this);">
                            <asp:ListItem Value="" Text="Please Select"></asp:ListItem>
                        </asp:DropDownList>--%>
                        <%--<asp:TextBox ID="txtID" Font-Names="Trebuchet MS" Font-Size=Small runat="server" Width=150></asp:TextBox>
                        <ajaxToolkit:AutoCompleteExtender runat=server ID="AutoComplet" TargetControlID="txtID" ServiceMethod="GetUserList" ServicePath="AutoCompleteWebServices.asmx" MinimumPrefixLength="1" EnableCaching="true">
                        </ajaxToolkit:AutoCompleteExtender>--%>
                        <asp:TextBox runat="server" CssClass="common" ID="txtID" Width="150" autocomplete="off" />
                        <ajaxToolkit:AutoCompleteExtender
                            runat="server" 
                            ID="autoComplete1" 
                            TargetControlID="txtID"
                            ServicePath="../../users/autocomplete.asmx"
                            ServiceMethod="GetUserID"
                            MinimumPrefixLength="1" 
                            CompletionInterval="100"
                            EnableCaching="true"
                            CompletionSetCount="12" />
                    </asp:TableCell>
                    <asp:TableCell runat="server">
                        <asp:DropDownList ID="WS" CssClass="common" runat="server" Width="130px" OnChange="RemoveZeroIndex(this);">
                            <asp:ListItem Text="Please Select" Value=""></asp:ListItem>
                        </asp:DropDownList>
                    </asp:TableCell>
                    <asp:TableCell runat="server">
                        <asp:DropDownList ID="WE" CssClass="common" runat="server" Width="130px" OnChange="RemoveZeroIndex(this);">
                            <asp:ListItem Text="Please Select" Value=""></asp:ListItem>
                        </asp:DropDownList>
                    </asp:TableCell>
                </asp:TableRow>
                <asp:TableRow ID="TableRow2" runat="server" HorizontalAlign="Center">
                    <asp:TableCell ID="TableCell4" ColumnSpan="3" runat="server">
                        <center><asp:Button ID="submit" CausesValidation="true" runat="server" cssClass="button" Text="Submit" /></center>
                    </asp:TableCell>
                </asp:TableRow>
            </asp:Table>
            <br />
        &nbsp; &nbsp;&nbsp;<br />
        
        <asp:Table ID="Table3" runat="server" Width="100%">
            <asp:TableRow>
                <asp:TableCell BorderStyle="None">
                    <asp:Label ID="Label1" runat="server" CssClass="common"  Font-Italic="true" Text="">Current System DateTime : <%= Now %>  EST</asp:Label>            
                </asp:TableCell>
            </asp:TableRow>
            <asp:TableRow>
                <asp:TableCell HorizontalAlign="Left" BorderStyle="None">
                    <asp:Button ID="Button1" runat="server" CausesValidation="false" Text="Export Results" CssClass="button" />
                    </asp:TableCell>
            </asp:TableRow>
        </asp:Table>
        

        <asp:Table ID="Table2" runat="server" HorizontalAlign="Center" CssClass="common" Width="100%">
            <asp:TableRow>
                <asp:TableCell HorizontalAlign="Center" ColumnSpan="16" CssClass="HeaderDiv">
                    Schedule Details
                </asp:TableCell>
            </asp:TableRow>
            <asp:TableRow ID="TableRow3" runat="server">
                <asp:TableCell ID="TableCell5" CssClass="alt1" runat="server" >HBT ID</asp:TableCell>
                <asp:TableCell ID="TableCell20" CssClass="alt1" runat="server" >Emp Name</asp:TableCell>
                <asp:TableCell ID="TableCell18" CssClass="alt1" runat="server" >User Type</asp:TableCell>
                <asp:TableCell ID="TableCell6" CssClass="alt1" runat="server" >Mon</asp:TableCell>
                <asp:TableCell ID="TableCell7" CssClass="alt1" runat="server" >Tue</asp:TableCell>
                <asp:TableCell ID="TableCell8" CssClass="alt1" runat="server" >Wed</asp:TableCell>
                <asp:TableCell ID="TableCell9" CssClass="alt1" runat="server" >Thr</asp:TableCell>
                <asp:TableCell ID="TableCell10" CssClass="alt1" runat="server" >Fri</asp:TableCell>
                <asp:TableCell ID="TableCell11" CssClass="alt1" runat="server" >Sat</asp:TableCell>
                <asp:TableCell ID="TableCell12" CssClass="alt1" runat="server" >Sun</asp:TableCell>
                <asp:TableCell ID="TableCell13" CssClass="alt1" runat="server" >sTime</asp:TableCell>
                <asp:TableCell ID="TableCell14" CssClass="alt1" runat="server" >eTime</asp:TableCell>
                <asp:TableCell ID="TableCell15" CssClass="alt1" runat="server" >Week Start</asp:TableCell>
                <asp:TableCell ID="TableCell16" CssClass="alt1" runat="server" >Week End</asp:TableCell>
                <asp:TableCell ID="TableCell17" CssClass="alt1" runat="server" >Schedule Last Update(IST)</asp:TableCell>
                <asp:TableCell ID="Tablecell19" CssClass="alt1" runat="server">Platform </asp:TableCell>
                
            </asp:TableRow>
        </asp:Table>
        &nbsp;&nbsp;&nbsp;&nbsp;<br />
        &nbsp;&nbsp;
    
</div>
    <asp:ValidationSummary ID="ValidationSummary1" runat="server" HeaderText="Following error occurs:" ShowMessageBox="true" DisplayMode="BulletList" ShowSummary="False" /> </form>
</body>
</html>
