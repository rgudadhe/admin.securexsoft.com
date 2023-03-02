<%@ Page Language="VB" AutoEventWireup="false" CodeFile="proud.aspx.vb" Inherits="proud" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 1.0 Transitional/EN">
<script type="text/javascript" language=javascript>


    function RemoveSelectItemsWeek()
    {
        if (document.getElementById("DropDownWeek").length > 0)
        {   
            if (document.getElementById("DropDownWeek").item(0).value =='')
            {
                document.getElementById("DropDownWeek").remove(0);
            }
        }
    }
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
    <title>Send Production Schedule</title>
    <link href= "../../App_Themes/Css/styles.css" type="text/css" rel="stylesheet" />
    <link href= "../../App_Themes/Css/Common.css" type="text/css" rel="stylesheet" />
</head>

<body>
    <form id="form1" runat="server">
	    <div>
            <asp:ScriptManager ID="ScriptManager1" runat="server">
            </asp:ScriptManager>
            <div>
            <center>
                &nbsp;&nbsp;
                <asp:Table ID="Table1" runat="server" Width="720px">
                    <asp:TableRow runat="server">
                        <asp:TableCell HorizontalAlign="Center" ColumnSpan="2" runat="server" CssClass="HeaderDiv">
                            HBA Schedule
                        </asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow runat="server">
                        <asp:TableCell HorizontalAlign="Left" CssClass="common" runat="server" >
                            Select Week &nbsp 
                            <asp:DropDownList ID="DropDownWeek" Width="250" CssClass="common"  AutoPostBack="true" OnSelectedIndexChanged="DropDownWeek_SelectedIndexChanged" runat="server" OnChange="RemoveSelectItemsWeek()" >
                                <asp:ListItem Value="" Text="Please Select"></asp:ListItem>
                            </asp:DropDownList>&nbsp
                            <asp:RequiredFieldValidator  Display="None" ID="RequiredFieldWeek" runat="server" ErrorMessage="Please select week" SetFocusOnError="true" ControlToValidate="DropDownWeek"></asp:RequiredFieldValidator> 
                        </asp:TableCell>
                        <asp:TableCell HorizontalAlign="Right" CssClass="common" runat="server">
                            Add OR Update mins/day                            
                        </asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow runat="server">
                        <asp:TableCell ColumnSpan="2" runat="server">
                            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                <ContentTemplate>
                                    <asp:Table ID="Table2" runat="server" Width="100%">
                                        <asp:TableRow>
                                            <asp:TableCell HorizontalAlign="Right" CssClass="common" Width="5%">
                                                User ID :
                                            </asp:TableCell>
                                            <asp:TableCell Width="5%" CssClass="common" HorizontalAlign="Left">
                                                <asp:Label ID="lblUserID" runat="server" Text="" CssClass="common" ></asp:Label>
                                            </asp:TableCell>
                                            <asp:TableCell Width="40%" CssClass="common">
                                                &nbsp
                                            </asp:TableCell>
                                            <asp:TableCell HorizontalAlign="Right" CssClass="common" Width="5%">
                                                Monday :
                                            </asp:TableCell>
                                            <asp:TableCell Width="5%" >
                                                <asp:TextBox ID="txtMon" CssClass="common" runat="server" Width="50" Text="00" onBlur="if (this.value==''){this.value='00';}" onFocus="if (this.value=='00')this.value='';"></asp:TextBox> 
                                            </asp:TableCell>
                                            <asp:TableCell HorizontalAlign="Left" VerticalAlign="Top" Width="40%">
                                                <asp:RequiredFieldValidator  Display="None" ID="RequiredFieldMon" runat="server" SetFocusOnError="true" ErrorMessage="Please enter schedule for monday" ControlToValidate="txtMon"></asp:RequiredFieldValidator><Br>
                                                <asp:RegularExpressionValidator  Display="None" ID="RegularExpressionMon" runat="server" SetFocusOnError="true" ErrorMessage="Accept only numbers" ControlToValidate="txtMon" ValidationExpression="([0-9]*)" ></asp:RegularExpressionValidator>
                                            </asp:TableCell>
                                        </asp:TableRow>
                                        <asp:TableRow>
                                            <asp:TableCell HorizontalAlign="Right" Width="5%" CssClass="common">
                                                Tuesday :
                                            </asp:TableCell>
                                            <asp:TableCell HorizontalAlign="Left" Width="5%">
                                                <asp:TextBox ID="txtTue" Width="50" runat="server" CssClass="common" Text="00" onBlur="if (this.value==''){this.value='00';}" onFocus="if (this.value=='00')this.value='';"></asp:TextBox>
                                            </asp:TableCell>
                                            <asp:TableCell HorizontalAlign="Left" VerticalAlign="Top" width="40%">
                                                <asp:RequiredFieldValidator  Display="None" ID="RequiredFieldTue" runat="server" SetFocusOnError="true" ErrorMessage="Please enter schedule for tuesday" ControlToValidate="txtTue"></asp:RequiredFieldValidator><Br>
                                                <asp:RegularExpressionValidator  Display="None" ID="RegularExpressionTue" runat="server" SetFocusOnError="true" ErrorMessage="Accept only numbers" ControlToValidate="txtTue" ValidationExpression="([0-9]*)"></asp:RegularExpressionValidator>
                                            </asp:TableCell>
                                            <asp:TableCell HorizontalAlign="Right" CssClass="common" Width=5%>
                                                Wednesday :
                                            </asp:TableCell>
                                            <asp:TableCell HorizontalAlign="Left" Width="5%">
                                                <asp:TextBox ID="txtWed" Width="50" runat="server" CssClass="common" Text="00" onBlur="if (this.value==''){this.value='00';}" onFocus="if (this.value=='00')this.value='';"></asp:TextBox>
                                            </asp:TableCell>
                                            <asp:TableCell HorizontalAlign="Left" VerticalAlign="Top" Width="40%">
                                                <asp:RequiredFieldValidator  Display="None" ID="RequiredFieldWed" runat="server" SetFocusOnError="true" ErrorMessage="Please enter schedule for wenesday" ControlToValidate="txtWed"></asp:RequiredFieldValidator><Br>
                                                <asp:RegularExpressionValidator  Display="None" ID="RegularExpressionWed" runat="server" SetFocusOnError="true" ErrorMessage="Accept only numbers" ControlToValidate="txtWed" ValidationExpression="([0-9]*)"></asp:RegularExpressionValidator>
                                            </asp:TableCell>
                                        </asp:TableRow>
                                        <asp:TableRow>
                                            <asp:TableCell HorizontalAlign="Right" CssClass="common" Width="5%">
                                                Thursday :
                                            </asp:TableCell>
                                            <asp:TableCell HorizontalAlign="Left" Width="5%">
                                                <asp:TextBox ID="txtThr" Width="50" runat="server" Text="00" CssClass="common" onBlur="if (this.value==''){this.value='00';}" onFocus="if (this.value=='00')this.value='';"></asp:TextBox>
                                            </asp:TableCell>
                                            <asp:TableCell HorizontalAlign="Left" VerticalAlign="Top" Width="40%">
                                                <asp:RequiredFieldValidator  Display="None" ID="RequiredFieldThr" runat="server" SetFocusOnError="true" ErrorMessage="Please enter schedule for thrusday" ControlToValidate="txtThr"></asp:RequiredFieldValidator><Br>
                                                <asp:RegularExpressionValidator  Display="None" ID="RegularExpressionThr" runat="server" SetFocusOnError="true" ErrorMessage="Accept only numbers" ControlToValidate="txtThr" ValidationExpression="([0-9]*)"></asp:RegularExpressionValidator>
                                            </asp:TableCell>
                                            <asp:TableCell HorizontalAlign="Right" CssClass="common" Width="5%">
                                                Friday :
                                            </asp:TableCell>
                                            <asp:TableCell HorizontalAlign="Left" Width="5%">
                                                <asp:TextBox ID="txtFri" Width="50" runat="server" Text="00" CssClass="common" onBlur="if (this.value==''){this.value='00';}" onFocus="if (this.value=='00')this.value='';"></asp:TextBox>
                                            </asp:TableCell>
                                            <asp:TableCell HorizontalAlign="Left" VerticalAlign="Top" Width="40%">
                                                <asp:RequiredFieldValidator  Display="None" ID="RequiredFieldFri" runat="server" SetFocusOnError="true" ErrorMessage="Please enter schedule for friday" ControlToValidate="txtFri"></asp:RequiredFieldValidator><Br>
                                                <asp:RegularExpressionValidator  Display="None" ID="RegularExpressionFri" runat="server" SetFocusOnError="true" ErrorMessage="Accept only numbers" ControlToValidate="txtFri" ValidationExpression="([0-9]*)"></asp:RegularExpressionValidator>
                                            </asp:TableCell>
                                        </asp:TableRow>
                                        <asp:TableRow>
                                            <asp:TableCell HorizontalAlign="Right" Width="5%" CssClass="common">
                                                Saturday :
                                            </asp:TableCell>
                                            <asp:TableCell HorizontalAlign="Left" Width="5%">
                                                <asp:TextBox ID="txtSat" Width="50" CssClass="common" runat="server" Text="00" onBlur="if (this.value==''){this.value='00';}" onFocus="if (this.value=='00')this.value='';"></asp:TextBox>
                                            </asp:TableCell>
                                            <asp:TableCell HorizontalAlign="Left" VerticalAlign="Top" Width="40%">
                                                <asp:RequiredFieldValidator  Display="None" ID="RequiredFieldSat" runat="server" SetFocusOnError="true" ErrorMessage="Please enter schedule for saturday" ControlToValidate="txtSat"></asp:RequiredFieldValidator><Br>
                                                <asp:RegularExpressionValidator  Display="None" ID="RegularExpressionSat" runat="server"  SetFocusOnError="true" ErrorMessage="Accept only numbers" ControlToValidate="txtSat" ValidationExpression="([0-9]*)"></asp:RegularExpressionValidator>
                                            </asp:TableCell>
                                            <asp:TableCell HorizontalAlign="Right" Width="5%" CssClass="common">
                                                Sunday :
                                            </asp:TableCell>
                                            <asp:TableCell HorizontalAlign="Left" Width="5%">
                                                <asp:TextBox ID="txtSun" Width="50" CssClass="common" runat="server" Text="00" onBlur="if (this.value==''){this.value='00';}" onFocus="if (this.value=='00')this.value='';"></asp:TextBox>
                                            </asp:TableCell>
                                            <asp:TableCell HorizontalAlign="Left" VerticalAlign="Top" Width="40%">
                                                <asp:RequiredFieldValidator  Display="None" ID="RequiredFieldSun" runat="server" SetFocusOnError="true" ErrorMessage="Please enter schedule for sunday" ControlToValidate="txtSun"></asp:RequiredFieldValidator><Br>
                                                <asp:RegularExpressionValidator  Display="None" ID="RegularExpressionSun" runat="server" SetFocusOnError="true" ErrorMessage="Accept only numbers" ControlToValidate="txtSun" ValidationExpression="([0-9]*)"></asp:RegularExpressionValidator>
                                            </asp:TableCell>
                                        </asp:TableRow>
                                        <asp:TableRow Height="40" VerticalAlign="Top">
                                            <asp:TableCell HorizontalAlign="Left" CssClass="common"  ColumnSpan="3">
                                                Start Time :
                                                <asp:DropDownList ID="DropDownStartTime" Width="100" CssClass="common" runat="server" OnChange="RemoveZeroIndex(this);">
                                                    <asp:ListItem Value="" Text="Please Select"></asp:ListItem>
                                                </asp:DropDownList> &nbsp &nbsp &nbsp
                                                <asp:DropDownList ID="DropDownStartFormat" Width="100" CssClass="common" runat="server" OnChange="RemoveZeroIndex(this);">
                                                    <asp:ListItem Value="" Text="Please Select"></asp:ListItem>
                                                    <asp:ListItem Value="AM" Text="AM"></asp:ListItem>
                                                    <asp:ListItem Value="PM" Text="PM"></asp:ListItem>
                                                </asp:DropDownList><BR> &nbsp &nbsp &nbsp &nbsp
                                                <asp:RequiredFieldValidator  Display="None" ID="RequiredFieldStartTime" runat="server" ErrorMessage="Please select start time" SetFocusOnError="true" ControlToValidate="DropDownStartTime"></asp:RequiredFieldValidator> &nbsp
                                                <asp:RequiredFieldValidator  Display="None" ID="RequiredFieldStartTimeFormat" runat="server" ErrorMessage="Please select time format" SetFocusOnError="true"  ControlToValidate="DropDownStartFormat"></asp:RequiredFieldValidator>
                                            </asp:TableCell>
                                            <asp:TableCell HorizontalAlign="Left" ColumnSpan="3" CssClass="common">
                                                End Time :
                                                <asp:DropDownList ID="DropDownEndTime" runat="server" Width="100" CssClass="common" OnChange="RemoveZeroIndex(this);">
                                                    <asp:ListItem Value="" Text="Please Select" ></asp:ListItem>
                                                </asp:DropDownList>&nbsp &nbsp &nbsp
                                                <asp:DropDownList ID="DropDownEndFormat" runat="server" Width="100" CssClass="common" OnChange="RemoveZeroIndex(this);">
                                                    <asp:ListItem Value="" Text="Please Select"></asp:ListItem>
                                                    <asp:ListItem Value="AM" Text="AM"></asp:ListItem>
                                                    <asp:ListItem Value="PM" Text="PM"></asp:ListItem>
                                                </asp:DropDownList><BR> &nbsp &nbsp &nbsp &nbsp
                                                <asp:RequiredFieldValidator  Display="None" ID="RequiredFieldEndTime" runat="server" ErrorMessage="Please select end time" SetFocusOnError="true" ControlToValidate="DropDownEndTime"></asp:RequiredFieldValidator> &nbsp
                                                <asp:RequiredFieldValidator  Display="None" ID="RequiredFieldEndTimeFormat" runat="server" ErrorMessage="Please select time format" SetFocusOnError="true"  ControlToValidate="DropDownEndFormat"></asp:RequiredFieldValidator>
                                            </asp:TableCell>
                                        </asp:TableRow>
                                    </asp:Table>
                                </ContentTemplate>
                                <Triggers>
                                    <asp:AsyncPostBackTrigger ControlID="DropDownWeek" EventName="SelectedIndexChanged" />
                                </Triggers>
                            </asp:UpdatePanel>
                        </asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow>
                        <asp:TableCell ColumnSpan="2">
                            <center>
                            <asp:Button ID="BtnSend" runat="server" Text="Submit" CssClass="button" /> &nbsp
                            <asp:Button ID="BtnClear" CausesValidation="false" runat="server" Text="Clear" CssClass="button" Visible="false" />
                            </center>
                        </asp:TableCell>
                    </asp:TableRow>
                </asp:Table>
            </center>
    </div>
		<!-- content-wrap ends-->	
	</div>
    <asp:ValidationSummary ID="ValidationSummary1" runat="server" HeaderText="Following error occurs:" ShowMessageBox="true" DisplayMode="BulletList" ShowSummary="False" /> </form>
</body>
</html>
