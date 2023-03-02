<%@ Page Language="VB" AutoEventWireup="false" CodeFile="ViewMTVHSData.aspx.vb" Inherits="Transcend_ViewMTVHSData" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>ViewVHSData</title>
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
    <h1>View MTVHS Data</h1>
    <div>
        <table width="100%"   >
             <tr>
                <td style="text-align: center" class="HeaderDiv">
                    Search Data
                </td>
            </tr>
            <tr>
                <td align="center">
                    <center>
                    <table width="40%">
                        <tr>
                            <td style="width:25%" align="center" class="alt1">
                                Start Date
                            </td>
                            <td style="width:25%" align="center" class="alt1">
                                End Date
                            </td>                            
                            <td style="width:10%" align="center" class="alt1">
                                &nbsp;
                            </td>                            
                        </tr>
                        <tr>
                            <td>
                                <asp:TextBox ID="txtStartDate" runat="server" TabIndex="6" Width="78px" CssClass="common" ></asp:TextBox><asp:ImageButton ID="ImgBntsDate" runat="server" CausesValidation="False" ImageUrl="~/images/Calendar_scheduleHS.png" />
                            </td>
                            <td>
                                <asp:TextBox ID="txtEndDate" runat="server" TabIndex="7" Width="78px" CssClass="common" ></asp:TextBox><asp:ImageButton ID="ImgBnteDate" runat="server" CausesValidation="False" ImageUrl="~/images/Calendar_scheduleHS.png" />
                            </td>
                            <td>
                                <asp:Button ID="btnSearch" runat="server" Text="Search" cssClass="button" CausesValidation="false" OnClientClick="javascript:return Chk();" />
                            </td>
                        </tr>
                    </table>
                    </center>
                </td>
            </tr>
            <tr>
                <td align="left">
                    <asp:Label ID="lblResponse" runat="server" Text="" ForeColor="red" Font-Bold="true" Font-Italic="true"></asp:Label>
                </td>
            </tr>
            <tr>
                <td align="left">
                    <asp:LinkButton ID="LnlExport" runat="server" CssClass="common" >Export List</asp:LinkButton>                    
                </td>
            </tr>
            <tr>
                <td align="center">
                    <asp:GridView ID="GrdViewData" runat="server" AllowPaging="true" AllowSorting="true" Font-Names="Trebuchet MS" Font-Size="9pt" AutoGenerateColumns="false" PageSize="10" Width="100%">
                    <Columns>
                        <asp:TemplateField HeaderText="Job#" SortExpression="JobNo" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left" HeaderStyle-CssClass="alt1">
                            <ItemTemplate>
                                <%#Eval("JobNumber")%>
                            </ItemTemplate>
                        </asp:TemplateField>
                        
                        <asp:TemplateField HeaderText="MTID" SortExpression="MTID" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left" HeaderStyle-CssClass="alt1">
                            <ItemTemplate>
                                <%#Eval("MTID")%>
                            </ItemTemplate>
                        </asp:TemplateField>
                        
                        <asp:TemplateField HeaderText="QAID" SortExpression="QAID" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left" HeaderStyle-CssClass="alt1">
                            <ItemTemplate>
                                <%#Eval("QAID")%>
                            </ItemTemplate>
                        </asp:TemplateField>
                        
                        <asp:TemplateField HeaderText="VHS Lines" SortExpression="Lines" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left" HeaderStyle-CssClass="alt1">
                            <ItemTemplate>
                                <%#Eval("Lines")%>
                            </ItemTemplate>
                        </asp:TemplateField>
                        
                        <asp:TemplateField HeaderText="QA Lines" SortExpression="QALines" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left" HeaderStyle-CssClass="alt1">
                            <ItemTemplate>
                                <%#Eval("QALines")%>
                            </ItemTemplate>
                        </asp:TemplateField>
                        
                        <asp:TemplateField HeaderText="Status" SortExpression="Status" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left" HeaderStyle-CssClass="alt1">
                            <ItemTemplate>
                                <%#Eval("Status")%>
                            </ItemTemplate>
                        </asp:TemplateField>
                        
                        <asp:TemplateField HeaderText="Transcription Date" SortExpression="TranscriptionDate" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left" HeaderStyle-CssClass="alt1">
                            <ItemTemplate>
                                <%#Eval("TranscriptionDate")%>
                            </ItemTemplate>
                        </asp:TemplateField>
                        
                        <asp:TemplateField HeaderText="Date Sent" SortExpression="UpdatedOn" HeaderStyle-HorizontalAlign="Center" HeaderStyle-CssClass="alt1">
                            <ItemTemplate>
                                <%#IIf(IsDBNull(Eval("UpdatedOn")), String.Empty, Eval("UpdatedOn").ToShortDateString())%>
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
				    <asp:BoundColumn HeaderText="Job#" DataField="JobNumber"></asp:BoundColumn>
				    <asp:BoundColumn HeaderText="MTID" DataField="MTID"></asp:BoundColumn>
				    <asp:BoundColumn HeaderText="QAID" DataField="QAID"></asp:BoundColumn>
				    <asp:BoundColumn HeaderText="VHS Lines" DataField="Lines"></asp:BoundColumn>
				    <asp:BoundColumn HeaderText="QA Lines" DataField="QALines"></asp:BoundColumn>
				    <asp:BoundColumn HeaderText="Status" DataField="Status"></asp:BoundColumn>
				    <asp:BoundColumn HeaderText="Transcription Date" DataField="TranscriptionDate"></asp:BoundColumn>
				    <asp:BoundColumn HeaderText="UpdatedOn" DataField="UpdatedOn"></asp:BoundColumn>
				</Columns>
			</asp:datagrid>
    </div>
    </div> 
    </div> 
    </form>
</body>
</html>
