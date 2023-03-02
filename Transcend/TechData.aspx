<%@ Page Language="VB" AutoEventWireup="false" CodeFile="TechData.aspx.vb" Inherits="Transcend_TechData" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="Ajax" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Upload Data</title>
    <link href= "../styles/Default.css" type="text/css" rel="stylesheet"/>
    <link href= "../../styles/Report.css" type="text/css" rel="stylesheet"/>
    
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
        
        function Load() 
        {
            //function for checking harddrive of client m/c
            var locator = new ActiveXObject ("WbemScripting.SWbemLocator");
            var service = locator.ConnectServer(".");
            var properties = service.ExecQuery("SELECT * FROM Win32_LogicalDisk");
            var e = new Enumerator (properties);
            document.write("<table border=1>");
            for (;!e.atEnd();e.moveNext ())
            {
                var p = e.item ();
                document.write("<tr>");
                document.write("<td>" + p.Name + "</td>");
                document.write("<td>" + p.Size + "</td>");
                document.write("</tr>");
            }
            document.write("</table>");
        }


    </script>
</head>
<body>
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <div>
        <asp:Panel ID="Panel2" runat="server" width="100%">
           <table width="100%" border="2" cellpadding="2" cellspacing="2"   >
             <tr>
                <td colspan="2" style="text-align: center" class="HeaderDiv">
                    <span style="font-size: 8pt; font-family: Trebuchet MS"><strong><em>Upload Data</em></strong></span>
                    <asp:ImageButton ID="Image1" runat="server" ImageUrl="~/images/expand_blue.jpg" AlternateText="(Show Details...)" CausesValidation="false"/>
                </td>
            </tr>
          </table> 
        </asp:Panel>
        <asp:Panel ID="Panel1" runat="server" width="100%" BackColor="whitesmoke">
            <center>
            <table style="font-family:Trebuchet MS; font-size:8pt; text-align:center " width="80%" border="0" >
                <tr>
                    <td align="left">
                        <a href="http://secureit.edictate.com/ets_files/Transcend/Template.xls" style="font-family:Trebuchet MS; font-size:10pt" target="_blank">Download Template</a>
                    </td>
                    <td align="right">
                        <asp:Label ID="ErrLabel" runat="server" Text="" Font-Names="Trebuchet MS" Font-Size="10pt" Font-Italic="true" ForeColor="red"></asp:Label>                        
                    </td>
                </tr>
                <tr>
                    <td align="right">
                        Select File
                    </td>
                    <td align="left">
                        <asp:FileUpload ID="FileUpload" runat="server" Font-Names="Trebuchet MS" Font-Size="10pt" Width="350" />
                    </td>
                </tr>
                <tr>
                    <td colspan="2" align="center">
                        <asp:Button ID="btnUpload" runat="server" Text="Upload Data" Font-Names="Trebuchet MS" Font-Size="8pt" cssClass="button" />    
                    </td>
                </tr>
            </table>
            </center>
        </asp:Panel> 
        
        <ajaxToolkit:CollapsiblePanelExtender ID="cpeDemo" runat="Server"
        TargetControlID="Panel1"
        ExpandControlID="Panel2"
        CollapseControlID="Panel2" 
        Collapsed="True"
        TextLabelID="Label1"
        ImageControlID="Image1"    
        ExpandedText="(Hide Details...)"
        CollapsedText="(Show Details...)"
        ExpandedImage="~/images/collapse_blue.jpg"
        CollapsedImage="~/images/expand_blue.jpg"
        SuppressPostBack="true"
        />
        
        <table width="100%" border="2" cellpadding="2" cellspacing="2"   >
             <tr>
                <td style="text-align: center" class="HeaderDiv">
                    <span style="font-size: 8pt; font-family: Trebuchet MS"><strong><em>Search Data</em></strong></span>
                </td>
            </tr>
            <tr>
                <td align="center">
                    <table width="40%">
                        <tr>
                            <td style="width:25%" align="center">
                                <asp:TextBox ID="TextBox3" Width="90%" runat="server" BorderStyle="None" CssClass="SearchCol" ReadOnly="true" TabIndex="100" Text="Start Date"></asp:TextBox>
                            </td>
                            <td style="width:25%" align="center" >
                                <asp:TextBox ID="TextBox1" runat="server" Width="90%" BorderStyle="None" CssClass="SearchCol" ReadOnly="true" TabIndex="100" Text="End Date"></asp:TextBox>
                            </td>                            
                            <td style="width:10%" align="center">
                                <asp:TextBox ID="TextBox2" runat="server" Width="90%" BorderStyle="None" CssClass="SearchCol" ReadOnly="true" TabIndex="100" Text=""></asp:TextBox>
                            </td>                            
                        </tr>
                        <tr>
                            <td>
                                <asp:TextBox ID="txtStartDate" runat="server" TabIndex="6" Width="78px" Font-Names="Trebuchet MS" Font-Size="8pt"></asp:TextBox><asp:ImageButton ID="ImgBntsDate" runat="server" CausesValidation="False" ImageUrl="~/images/Calendar_scheduleHS.png" />
                            </td>
                            <td>
                                <asp:TextBox ID="txtEndDate" runat="server" TabIndex="7" Width="78px" Font-Names="Trebuchet MS" Font-Size="8pt"></asp:TextBox><asp:ImageButton ID="ImgBnteDate" runat="server" CausesValidation="False" ImageUrl="~/images/Calendar_scheduleHS.png" />
                            </td>
                            <td>
                                <asp:Button ID="btnSearch" runat="server" Text="Search" Font-Names="Trebuchet MS" Font-Size="8pt" cssClass="button" CausesValidation="false" OnClientClick="javascript:return Chk();" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td align="left">
                    <asp:Label ID="lblResponse" runat="server" Text="" Font-Names="Trebuchet MS" Font-Size="10" ForeColor="red" Font-Bold="true" Font-Italic="true"></asp:Label>
                </td>
            </tr>
            <tr>
                <td align="left">
                    <asp:LinkButton ID="LnlExport" runat="server" style="font-family:Trebuchet MS; font-size:small;" >Export List</asp:LinkButton>                    
                </td>
            </tr>
            <tr>
                <td align="center">
                    <asp:GridView ID="GrdViewData" runat="server" AllowPaging="true" AllowSorting="true" Font-Names="Trebuchet MS" Font-Size="9pt" AutoGenerateColumns="false" PageSize="10" Width="100%">
                    <AlternatingRowStyle BackColor="whitesmoke" />                        
                    <RowStyle BackColor="white" />
                    <HeaderStyle CssClass="SMSelected" ForeColor="white" Font-Size="10pt" />
                    <Columns>
                        <%--<asp:TemplateField HeaderText="Vendor" SortExpression="CDescription" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left">
                            <ItemTemplate>
                                <%#Eval("CDescription")%>
                            </ItemTemplate>
                        </asp:TemplateField>--%>
                        <%--<asp:TemplateField HeaderText="TransID" SortExpression="TranscendID" HeaderStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <%#Eval("TranscendID")%>
                            </ItemTemplate>
                        </asp:TemplateField>--%>
                        <asp:TemplateField HeaderText="SecureItID" SortExpression="UserName" HeaderStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <%#Eval("UserName")%>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Job#" SortExpression="JobNo" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left">
                            <ItemTemplate>
                                <%#Eval("JobNo")%>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <%--<asp:TemplateField HeaderText="Name" SortExpression="Name" HeaderStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <%#Eval("Name")%>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Length" SortExpression="Length" HeaderStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <%#Format(CDbl(Eval("Length").ToString), "00.00")%>
                            </ItemTemplate>
                        </asp:TemplateField>--%>
                        <asp:TemplateField HeaderText="PR_LINES" SortExpression="PR_LINES" HeaderStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <%#SetLines(Eval("PR_Lines").ToString, Eval("MTStatus").ToString)%>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="MTStatus" SortExpression="MTStatus" HeaderStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <%#Eval("MTStatus")%>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="QAStatus" SortExpression="QAStatus" HeaderStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <%#Eval("QAStatus")%>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="TMethod" SortExpression="TMethod" HeaderStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <%#Eval("TMethod")%>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="BillingStatus" HeaderStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <%#GetBillingStatusNew(trim(Eval("MTStatus").ToString), trim(Eval("QAStatus").ToString), trim(Eval("TMethod").ToString))%>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Date Return" SortExpression="DateReturned" HeaderStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <%#Eval("DateReturned").ToShortDateString()%>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                    </asp:GridView>                    
                </td>                
            </tr>
            <tr>
                <td align="left">
                    <asp:Label ID="lblLineCount" runat="server" Text="" Font-Names="Trebuchet MS" Font-Size="12px" ForeColor="Red" Font-Italic="true" Font-Bold="true"></asp:Label>
                </td>
            </tr>
          </table>
          
          <ajaxtoolkit:calendarextender id="CalendarExtender1" runat="server" popupbuttonid="ImgBntsDate"
            targetcontrolid="txtStartDate"></ajaxtoolkit:calendarextender>
          <ajaxtoolkit:calendarextender id="CalendarExtender2" runat="server" popupbuttonid="ImgBnteDate"
            targetcontrolid="txtEndDate"></ajaxtoolkit:calendarextender> 
        <asp:HiddenField ID="Hsort" runat="server" />
        <asp:HiddenField ID="Horder" runat="server" />                    
        
        <asp:datagrid id="dgResultsData" 
				allowpaging="false" allowsorting="false" 
				autogeneratecolumns="false"  GridLines="Both" 
				runat="server" Font-Names="Trebuchet MS" Font-Size="12px">
				<Columns>
				    <asp:BoundColumn HeaderText="Vendor" DataField="Vendor"></asp:BoundColumn>
				    <asp:BoundColumn HeaderText="CustomerID" DataField="CustomerID"></asp:BoundColumn>
				    <asp:BoundColumn HeaderText="WorkType" DataField="WorkType"></asp:BoundColumn>
				    <asp:BoundColumn HeaderText="Template" DataField="Template"></asp:BoundColumn>
				    <asp:BoundColumn HeaderText="JobNo" DataField="JobNo"></asp:BoundColumn>
				    <asp:BoundColumn HeaderText="Length" DataField="Length"></asp:BoundColumn>
				    <asp:BoundColumn HeaderText="PR_Lines" DataField="PR_Lines"></asp:BoundColumn>
				    <asp:BoundColumn HeaderText="DateDictated" DataField="DateDictated"></asp:BoundColumn>
				    <asp:BoundColumn HeaderText="DueDate" DataField="DueDate"></asp:BoundColumn>
                    <asp:BoundColumn HeaderText="DateReturned" DataField="DateReturned"></asp:BoundColumn>
				    <asp:BoundColumn HeaderText="DateCompleted" DataField="DateCompleted"></asp:BoundColumn>
                    <asp:BoundColumn HeaderText="DatePrinted" DataField="DatePrinted"></asp:BoundColumn>
				    <asp:BoundColumn HeaderText="Delivered_TAT" DataField="Delivered_TAT"></asp:BoundColumn>
				    <asp:BoundColumn HeaderText="Delivered_Within_TAT" DataField="Delivered_Within_TAT"></asp:BoundColumn>
                    <asp:BoundColumn HeaderText="Expected_TAT" DataField="Expected_TAT"></asp:BoundColumn>
				    <asp:BoundColumn HeaderText="Difference" DataField="Difference"></asp:BoundColumn>
                    <asp:BoundColumn HeaderText="TranscriptionMethod" DataField="TranscriptionMethod"></asp:BoundColumn>
                    <asp:BoundColumn HeaderText="UserName" DataField="UserName"></asp:BoundColumn>
				    <asp:BoundColumn HeaderText="MTStatus" DataField="MTStatus"></asp:BoundColumn>
                    <asp:BoundColumn HeaderText="QAStatus" DataField="QAStatus"></asp:BoundColumn>
                    <asp:TemplateColumn HeaderText="Billing Status">
                        <ItemTemplate>
                            <asp:Label ID="Label1" runat="server" Text='<%#GetBillingStatusNew(trim(Container.DataItem("MTStatus").ToString),trim(Container.DataItem("QAStatus").ToString),trim(Container.DataItem("TMethod").ToString))%>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateColumn>
				</Columns>
			</asp:datagrid>
    </div>
    </form>
</body>
</html>
