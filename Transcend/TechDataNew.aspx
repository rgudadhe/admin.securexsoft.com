<%@ Page Language="VB" AutoEventWireup="false" CodeFile="TechDataNew.aspx.vb" Inherits="Transcend_TechData" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="Ajax" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Upload Data</title>
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
    <div id="body">
    <div id="cap"></div>
    <div id="main">
    <h1>Upload Data</h1>
    <div>
        <asp:Panel ID="Panel2" runat="server" width="100%">
           <table width="100%">
             <tr>
                <td colspan="2" style="text-align: center" class="HeaderDiv">
                    Upload Data
                    <asp:ImageButton ID="Image1" runat="server" ImageUrl="~/images/expand_blue.jpg" AlternateText="(Show Details...)" CausesValidation="false"/>
                </td>
            </tr>
          </table> 
        </asp:Panel>
        <asp:Panel ID="Panel1" runat="server" width="100%">
            <center>
            <table style="text-align:center " width="80%" border="0" >
                <tr>
                    <td align="left" style="border:0">
                        <a href="../ets_files/Transcend/Template.xls"  class="common"  target="_blank">Download Template</a>
                    </td>
                    <td align="right" style="border:0">
                        <asp:Label ID="ErrLabel" runat="server" Text="" Font-Italic="true" ForeColor="red"></asp:Label>                        
                    </td>
                </tr>
                <tr>
                    <td align="right" style="border:0">
                        Select File
                    </td>
                    <td align="left" style="border:0">
                        <asp:FileUpload ID="FileUpload" runat="server" Width="350" />
                    </td>
                </tr>
                <tr>
                    <td colspan="2" align="center" style="border:0">
                        <center>
                            <asp:Button ID="btnUpload" runat="server" Text="Upload Data" cssClass="button" />    
                            <asp:Button ID="BtnGenerate" runat="server" Text="Generate Data" cssClass="button" Enabled="false" />    
                        </center>
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
        
        <table width="100%">
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
                            <td style="width:25%" align="center" class="alt1" >
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
                                <asp:TextBox ID="txtStartDate" runat="server" TabIndex="6" Width="78px" CssClass="common" ></asp:TextBox><asp:ImageButton ID="ImgBntsDate" runat="server" CausesValidation="False" ImageUrl="~/images/Calendar_scheduleHS.png" />
                            </td>
                            <td>
                                <asp:TextBox ID="txtEndDate" runat="server" TabIndex="7" Width="78px" CssClass="common" ></asp:TextBox><asp:ImageButton ID="ImgBnteDate" runat="server" CausesValidation="False" ImageUrl="~/images/Calendar_scheduleHS.png" />
                            </td>
                            <td>
                                <asp:Button ID="btnSearch" runat="server" Text="Search"  cssClass="button" CausesValidation="false" OnClientClick="javascript:return Chk();" />
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
                    <asp:LinkButton ID="LnlExport" runat="server" CssClass="common"  >Export List</asp:LinkButton>                    
                </td>
            </tr>
            <tr>
                <td align="center" style="border:0">
                    <asp:GridView ID="GrdViewData" runat="server" AllowPaging="true" AllowSorting="true" CssClass="common" AutoGenerateColumns="false" PageSize="10" Width="100%">
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
                        <asp:TemplateField HeaderText="MTID" SortExpression="MTID" HeaderStyle-HorizontalAlign="Center" HeaderStyle-CssClass="alt1">
                            <ItemTemplate>
                                <%#Eval("MTID")%>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="MLSID" SortExpression="MLSID" HeaderStyle-HorizontalAlign="Center" HeaderStyle-CssClass="alt1">
                            <ItemTemplate>
                                <%#Eval("MLSID")%>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="QAID" SortExpression="QAID" HeaderStyle-HorizontalAlign="Center" HeaderStyle-CssClass="alt1">
                            <ItemTemplate>
                                <%#Eval("QAID")%>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Job#" SortExpression="JobNo" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left" HeaderStyle-CssClass="alt1">
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
                        <asp:TemplateField HeaderText="PR_LINES" SortExpression="PR_LINES" HeaderStyle-HorizontalAlign="Center" HeaderStyle-CssClass="alt1">
                            <ItemTemplate>
                                <%#Eval("PR_Lines").ToString%>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Status" SortExpression="Status" HeaderStyle-HorizontalAlign="Center" HeaderStyle-CssClass="alt1">
                            <ItemTemplate>
                                <%#Eval("Status")%>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="TMethod" SortExpression="TMethod" HeaderStyle-HorizontalAlign="Center" HeaderStyle-CssClass="alt1">
                            <ItemTemplate>
                                <%#Eval("TMethod")%>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Date Return" SortExpression="DateReturned" HeaderStyle-HorizontalAlign="Center" HeaderStyle-CssClass="alt1">
                            <ItemTemplate>
                                <%#Eval("DateReturned").ToShortDateString()%>
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
          <ajaxtoolkit:calendarextender id="CalendarExtender2" runat="server" popupbuttonid="ImgBnteDate"  CssClass="cal_Theme1"
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
                    <asp:BoundColumn HeaderText="MTID" DataField="MTID"></asp:BoundColumn>
                    <asp:BoundColumn HeaderText="MLSID" DataField="MLSID"></asp:BoundColumn>
                    <asp:BoundColumn HeaderText="QAID" DataField="QAID"></asp:BoundColumn>
				    <asp:BoundColumn HeaderText="Status" DataField="Status"></asp:BoundColumn>
				    <asp:BoundColumn HeaderText="QA Date" DataField="QADate"></asp:BoundColumn>
				</Columns>
			</asp:datagrid>
        <asp:HiddenField ID="hdnFileName" runat="server" />
    </div>
    </div> 
    </div> 
    </form>
</body>
</html>
