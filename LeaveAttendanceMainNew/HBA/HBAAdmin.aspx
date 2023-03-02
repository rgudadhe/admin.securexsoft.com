<%@ Page Language="VB" AutoEventWireup="false" CodeFile="HBAAdmin.aspx.vb" Inherits="LeaveAttendanceMainNew_HBA_HBAAdmin" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="Ajax" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>HBA Admin</title>
    <LINK href= "../../styles/Default.css" type="text/css" rel="stylesheet">
    <LINK href= "../../Styles/Report.css" type="text/css" rel="stylesheet">
    <LINK href= "CalendarTitle.css" type="text/css" rel="stylesheet">
    <LINK href= "Tab.css" type="text/css" rel="stylesheet">
</head>
<body style="background-color:WhiteSmoke">
    <form id="frmHBA" runat="server">
    <div>
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        <Ajax:TabContainer ID="HBATabContainer" AutoPostBack=true runat=server Width="100%" ScrollBars=None >
            <Ajax:TabPanel ID="SendRequest" runat=server BorderColor=lightblue BorderStyle=Solid  BorderWidth=2>
                <HeaderTemplate>
                    <asp:Label ID="lblRequest" runat="server" Text="Register HBA Leave"></asp:Label>
                </HeaderTemplate>
                <ContentTemplate>
                    <center>
                        <asp:RequiredFieldValidator ID="RequiredFieldtxtStartDate" runat="server" ErrorMessage="Please Select Leave Start Date" Font-Names="Trebuchet MS" Font-Size=Small Font-Italic=true SetFocusOnError=true ControlToValidate="txtStartDate"></asp:RequiredFieldValidator> <BR>
                        <asp:RequiredFieldValidator ID="RequiredFieldtxtEndDate" runat="server" ErrorMessage="Please Select Leave End Date" Font-Names="Trebuchet MS" Font-Size=Small Font-Italic=true SetFocusOnError=true ControlToValidate="txtEndDate"></asp:RequiredFieldValidator> <BR>
                        <asp:RequiredFieldValidator ID="RequiredFieldReason" runat="server" ErrorMessage="Please Enter Reason for Leave" Font-Names="Trebuchet MS" Font-Size=Small Font-Italic=true SetFocusOnError=true ControlToValidate="TextArea1"></asp:RequiredFieldValidator> <BR>
                        <asp:CompareValidator ID="CompareValidator1" ControlToValidate="txtEndDate" ControlToCompare="txtStartDate" Type=Date Operator=GreaterThanEqual runat="server" Font-Names="Trebuchet MS" Font-Size=Small Font-Italic=true SetFocusOnError=true  ErrorMessage="LeaveEnd Date must be greater than LeaveStart Date"></asp:CompareValidator>
                    </center>
                    <center>
                        <table width="100%">
                            <tr>
                                <td>
                                    <asp:DropDownList ID="ddlHBA" Font-Names="Trebuchet MS" Font-Size="10px" runat="server">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                        </table>
                    </center>
                    <br />
                    <asp:Table ID="Table2" runat="server" GridLines="Both" HorizontalAlign="Center" Width="572px">
                        <asp:TableRow Font-Names="Trebuchet MS" CssClass="HeaderDiv" Font-Bold=true>
                            <asp:TableCell HorizontalAlign=Center  ColumnSpan=2>
                                Leave Registration
                            </asp:TableCell>
                        </asp:TableRow>
                
                        <asp:TableRow style="font-size: 10pt;  font-family: 'Trebuchet MS'; font-style: italic; color:Gray;" >
                            <asp:TableCell HorizontalAlign=Center ColumnSpan=2 >
                                Select Start Date : <asp:TextBox ID="txtStartDate" runat=server Width=70px Font-Names="Trebuchet MS"></asp:TextBox>    
                                                    <asp:ImageButton ID="imgSDate" CausesValidation=false ImageUrl="~/images/Calendar.gif" runat="server"/> &nbsp &nbsp
                                                    <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" Format="MM/dd/yyyy" TargetControlID="txtStartDate" PopupButtonID="imgSDate" BehaviorID="CalendarExtender1" Enabled="True">
                                                    </ajaxToolkit:CalendarExtender>
                                Select End Date :   <asp:TextBox ID="txtEndDate" runat=server Width=70px Font-Names="Trebuchet MS" ></asp:TextBox>
                                                    <asp:ImageButton ID="imgEDate" ImageUrl="~/images/Calendar.gif" CausesValidation=false runat="server" />
                                                    <ajaxToolkit:CalendarExtender ID="CalendarExtender2" runat="server" Format="MM/dd/yyyy" TargetControlID="txtEndDate" PopupButtonID="imgEDate" BehaviorID="CalendarExtender2" Enabled="True">
                                                    </ajaxToolkit:CalendarExtender>
                            </asp:TableCell>
                        </asp:TableRow>
                   
                               
                        <asp:TableRow style="font-size: 10pt;  font-family: 'Trebuchet MS'; font-style: italic; color:Gray;">
                            <asp:TableCell Width=150px HorizontalAlign=Right>
                                Reason : 
                            </asp:TableCell>
                            <asp:TableCell>
                                <textarea id="TextArea1" style="font-family:Trebuchet MS; font-size:small" rows="10" cols="65" runat=server></textarea>
                            </asp:TableCell>
                        </asp:TableRow>
                
                        <asp:TableRow style="font-size: 10pt;  font-family: 'Trebuchet MS'; font-style: italic;">
                            <asp:TableCell ColumnSpan=2 HorizontalAlign=Center>
                                <asp:Button ID="SendLR" runat="server" Font-Names="Trebuchet MS" Text="Register Leave" CssClass="Buttons" /> <BR>
                            </asp:TableCell>
                        </asp:TableRow>
                    </asp:Table><BR> <BR> <BR>
                </ContentTemplate>
            </Ajax:TabPanel>
            <Ajax:TabPanel ID="CancelRequest" runat=server BorderColor=lightblue BorderStyle=Solid BorderWidth=2>
                <HeaderTemplate>
                    <asp:Label ID="lblCanRequest" runat="server" Text="Cancel Leave"></asp:Label>
                </HeaderTemplate>
                <ContentTemplate>
                    <center>
                    <table width="100%">
                        <tr>
                            <td>
                                <asp:DropDownList ID="ddlHBA1" Font-Names="Trebuchet MS" Font-Size="10px" runat="server">
                                </asp:DropDownList>
                                &nbsp; &nbsp;
                                <asp:Button ID="btnCanHBA" runat="server" Text="Submit" Font-Names="Trebuchet MS" Font-Size="10px" CausesValidation="false" />
                            </td>
                        </tr>
                    </table>
                    </center>
                    <br />
                    <asp:Table ID="tblCancel" runat="server" BorderColor=LightBlue BorderWidth=1 Width=100% >
                        <asp:TableRow CssClass="HeaderDiv">
                            <asp:TableCell HorizontalAlign=Center>
                                Cancel Leave Registrtation
                            </asp:TableCell>
                        </asp:TableRow>
                        <asp:TableRow>
                            <asp:TableCell>
                                <asp:GridView ID="GridViewCancel" runat="server" Font-Names="Trebuchet MS" Font-Size=Small ShowFooter=true DataSourceID="SQLDataSrc" AutoGenerateColumns=false AllowSorting="false" AllowPaging="true" Width="100%" BackColor="FloralWhite">
                                    <AlternatingRowStyle BackColor="OldLace"   />
				                    <HeaderStyle Height=18 HorizontalAlign=Center CssClass="SMSelected" ForeColor=Beige />
                        	        <FooterStyle HorizontalAlign=Right  />  
                                    <Columns>
                                        <asp:TemplateField HeaderText="StartDate" HeaderStyle-HorizontalAlign=Center ItemStyle-VerticalAlign=Top  SortExpression="StartDate" ItemStyle-HorizontalAlign=Left ItemStyle-Width=5%> 
                                    		<ItemTemplate>
		                                	    <asp:Label ID="StartDate" runat="server" Text=<%#Eval("StartDate").ToShortDateString()%>></asp:Label>
                                    		</ItemTemplate>
                                	    </asp:TemplateField>
                                        <asp:TemplateField HeaderText="EndDate" HeaderStyle-HorizontalAlign=Center ItemStyle-VerticalAlign=Top SortExpression="EndDate" ItemStyle-HorizontalAlign=Left ItemStyle-Width=5%> 
                                    		<ItemTemplate>
		                                	    <asp:Label ID="StartDate" runat="server" Text=<%#Eval("EndDate").ToShortDateString()%>></asp:Label>
                                    		</ItemTemplate>
                                	    </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Application Date" HeaderStyle-HorizontalAlign=Center ItemStyle-VerticalAlign=Top SortExpression="AppDate" ItemStyle-HorizontalAlign=Left ItemStyle-Width=5%> 
                                    		<ItemTemplate>
		                                	    <asp:Label ID="StartDate" runat="server" Text=<%#Eval("AppDate").ToShortDateString() %>></asp:Label>
                                    		</ItemTemplate>
                                	    </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Reason" HeaderStyle-HorizontalAlign=Center SortExpression="Reason" ItemStyle-HorizontalAlign=Left > 
                                    		<ItemTemplate>
		                                	    <asp:Label ID="StartDate" runat="server" Text=<%#Eval("Reason") %>></asp:Label>
                                    		</ItemTemplate>
                                	    </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Cancel" HeaderStyle-HorizontalAlign=Center ItemStyle-HorizontalAlign=Left > 
                                    		<ItemTemplate>
		                                	    <a href="HBACancelDateSelection.aspx?LeaveID=<%# DataBinder.Eval(Container.DataItem, "LeaveID" )%>" onclick="window.open('HBACancelDateSelection.aspx?LeaveID=<%# DataBinder.Eval(Container.DataItem, "LeaveID" )%>','', 'width=450,height=240,status=1,scrollbars=1');return false;">Cancel</a>
                                    		</ItemTemplate>
                                	    </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </asp:TableCell>
                        </asp:TableRow>
                    </asp:Table>
                    <asp:SqlDataSource ID="SQLDataSrc" runat="server">
	                </asp:SqlDataSource>
                </ContentTemplate>
            </Ajax:TabPanel>
        </Ajax:TabContainer> 
    </div>
    </form>
</body>
</html>
