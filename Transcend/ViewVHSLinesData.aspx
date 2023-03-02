<%@ Page Language="VB" AutoEventWireup="false" CodeFile="ViewVHSLinesData.aspx.vb" Inherits="Transcend_ViewVHSLinesData" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>View VHS Lines Data</title>
        <link href= "../App_Themes/Css/Main.css" type="text/css" rel="stylesheet" />
    <link href="../App_Themes/Css/Common.css" type="text/css" rel="stylesheet"  />
    <link href= "../App_Themes/Css/Styles.css" type="text/css" rel="stylesheet" />
    <link href= "../App_Themes/Css/Calendar.css" type="text/css" rel="stylesheet" />

    <script type="text/javascript" language="javascript">
        function Chk()
        {
            if (document.getElementById('txtStartDate').value=='')
            {
                alert('Please enter start date')
                return false;
            }
            if (document.getElementById('txtEndDate').value=='')
            {
                alert('Please enter end date')
                return false;
            }
            return true;
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        <div id="body">
    <div id="cap"></div>
    <div id="main">
    <h1>View Chartscript Data</h1>
    <div>
        <table width="100%" border="0">
             <tr>
                <td style="text-align: center" class="HeaderDiv">
                    Search Data
                </td>
            </tr>
            <tr>
                <td align="center" style="text-align: center">
                    <table width="40%">
                        <tr>
                            <td style="width:25%" align="center" class="alt1">
                                JobNo
                            </td>
                            <td style="width:25%" align="center" class="alt1">
                                Start Date
                            </td>
                            <td style="width:25%" align="center" class="alt1" >
                                End Date
                            </td>                            
                            <td style="width:10%" align="center" class="alt1">
                                &nbsp;
                            </td>                            
                        </tr>
                        <tr>
                            <td>
                                <asp:TextBox ID="txtJobNo" runat="server" TabIndex="6" Width="78px" CssClass="common" ></asp:TextBox>
                            </td>
                            <td>
                                <asp:TextBox ID="txtStartDate" runat="server" TabIndex="6" Width="78px" CssClass="common"></asp:TextBox><asp:ImageButton ID="ImgBntsDate" runat="server" CausesValidation="False" CssClass="cal_Theme1" ImageUrl="~/images/Calendar_scheduleHS.png" />
                            </td>
                            <td>
                                <asp:TextBox ID="txtEndDate" runat="server" TabIndex="7" Width="78px" CssClass="common"></asp:TextBox><asp:ImageButton ID="ImgBnteDate" runat="server" CausesValidation="False" ImageUrl="~/images/Calendar_scheduleHS.png" CssClass="cal_Theme1" />
                            </td>
                            <td>
                                <asp:Button ID="btnSearch" runat="server" Text="Search" cssClass="button" CausesValidation="false" OnClientClick="javascript:return Chk();" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td align="left">
                    <asp:Label ID="lblResponse" runat="server" Text="" CssClass="common" ForeColor="red" Font-Bold="true" Font-Italic="true"></asp:Label>
                </td>
            </tr>
            <tr>
                <td align="left" style="border:0">
                    <asp:LinkButton ID="LnlExport" runat="server" CssClass="common"  >Export List</asp:LinkButton>                    
                </td>
            </tr>
            <tr>
                <td align="center" style="border:0">
                    <asp:GridView ID="GrdViewData" runat="server" AllowPaging="true" AllowSorting="true" Font-Names="Trebuchet MS" Font-Size="9pt" AutoGenerateColumns="false" PageSize="10" Width="100%">
                    <Columns>
                        <asp:TemplateField HeaderText="Name" SortExpression="Name" HeaderStyle-HorizontalAlign="Center" HeaderStyle-CssClass="alt1">
                            <ItemTemplate>
                                <%#Eval("Name")%>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="UserName" SortExpression="UserName" HeaderStyle-HorizontalAlign="Center" HeaderStyle-CssClass="alt1">
                            <ItemTemplate>
                                <%#Eval("UserName")%>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Job#" SortExpression="JobNo" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left" HeaderStyle-CssClass="alt1">
                            <ItemTemplate>
                                <%#Eval("JobNo")%>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="MTID" SortExpression="MTID" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left" HeaderStyle-CssClass="alt1">
                            <ItemTemplate>
                                <%#Eval("MTID")%>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="LINES" SortExpression="LINES" HeaderStyle-HorizontalAlign="Center" HeaderStyle-CssClass="alt1">
                            <ItemTemplate>
                                <%#Format(CDbl(Eval("Lines").ToString), "00.00")%>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Status" SortExpression="Status" HeaderStyle-HorizontalAlign="Center" HeaderStyle-CssClass="alt1">
                            <ItemTemplate>
                                <%#Eval("Status")%>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Date Updated" SortExpression="UpdatedOn" HeaderStyle-HorizontalAlign="Center" HeaderStyle-CssClass="alt1">
                            <ItemTemplate>
                                <%#Eval("UpdatedOn").ToShortDateString()%>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Remove" HeaderStyle-HorizontalAlign="Center" HeaderStyle-CssClass="alt1">
                            <ItemTemplate>
                                <asp:LinkButton ID="LnkRemove" runat="server" CssClass="common" CausesValidation="false" CommandArgument='<%#Eval("JobNo").ToString()%>' CommandName="Remove" OnClientClick="javascript:return confirm('Are you sure to delete this entry ?');" >Remove</asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                    </asp:GridView>                    
                </td>                
            </tr>
            <tr>
                <td align="left">
                    <asp:Label ID="lblLineCount" runat="server" Text="" CssClass="common" ForeColor="Red" Font-Italic="true" Font-Bold="true"></asp:Label>
                </td>
            </tr>
          </table>
          <ajaxtoolkit:calendarextender id="CalendarExtender1" runat="server" popupbuttonid="ImgBntsDate" CssClass="cal_Theme1"
            targetcontrolid="txtStartDate"></ajaxtoolkit:calendarextender>
          <ajaxtoolkit:calendarextender id="CalendarExtender2" runat="server" popupbuttonid="ImgBnteDate" CssClass="cal_Theme1"
            targetcontrolid="txtEndDate"></ajaxtoolkit:calendarextender> 
        <asp:HiddenField ID="Hsort" runat="server" />
        <asp:HiddenField ID="Horder" runat="server" />
        <asp:datagrid id="dgResultsData" 
				allowpaging="false" allowsorting="false" 
				autogeneratecolumns="false"  GridLines="Both" 
				runat="server" Font-Names="Trebuchet MS" Font-Size="12px">
				<Columns>
				    <asp:BoundColumn HeaderText="Name" DataField="Name"></asp:BoundColumn>
				    <asp:BoundColumn HeaderText="UserName" DataField="UserName"></asp:BoundColumn>
				    <asp:BoundColumn HeaderText="JobNo" DataField="JobNo"></asp:BoundColumn>
				    <asp:BoundColumn HeaderText="MTID" DataField="MTID"></asp:BoundColumn>
				    <asp:BoundColumn HeaderText="Lines" DataField="Lines"></asp:BoundColumn>
				    <asp:BoundColumn HeaderText="Status" DataField="Status"></asp:BoundColumn>
				    <asp:BoundColumn HeaderText="Date Updated" DataField="UpdatedOn"></asp:BoundColumn>
				</Columns>
			</asp:datagrid>
    </div>
    </div> 
    </div> 
    </form>
</body>
</html>
