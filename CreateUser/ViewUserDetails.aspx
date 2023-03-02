<%@ Page Language="VB" AutoEventWireup="false" CodeFile="ViewUserDetails.aspx.vb" Inherits="UserPhyAssgn_Default" EnableViewState="True" %>

<%@ Register Assembly="System.Web.Extensions, Version=1.0.61025.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI" TagPrefix="asp" %>
<%@ Register Assembly="KMobile.Web" Namespace="KMobile.Web.UI.WebControls" TagPrefix="asp" %>


<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 1.0 Transitional/EN">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Search User Details</title>
    <link href= "../App_Themes/Css/styles.css" type="text/css" rel="stylesheet" />
    <link href= "../App_Themes/Css/Common.css" type="text/css" rel="stylesheet" />
    <link href="../App_Themes/Css/DataTable.css" rel="stylesheet" type="text/css" />
    <link href="../App_Themes/Css/TableSorter.css" rel="stylesheet" type="text/css" />
    <script src="../App_Themes/JS/jquery-1.4.2.min.js" type="text/javascript"></script>  
    <script src="../App_Themes/JS/jquery.dataTables.min.js" type="text/javascript"></script>  
<script type="text/javascript" language="javascript">
    $(document).ready(function() {
				$('#MyDataGrid').dataTable( {
                    "aoColumns": [
                            		{ "asSorting": [ "desc", "asc" ] },
		                            { "asSorting": [ "desc", "asc" ] },
		                            { "asSorting": [ "desc", "asc" ] },
		                            { "asSorting": [ "desc", "asc" ] },
		                            { "asSorting": [ "desc", "asc" ] },
                            		{ "asSorting": [ "desc", "asc" ] },
		                            { "asSorting": [ "desc", "asc" ] },
		                            { "asSorting": [ "desc", "asc" ] },
		                            { "asSorting": [ "desc", "asc" ] },
		                            { "asSorting": [ "desc", "asc" ] },
                            		{ "asSorting": [ "desc", "asc" ] },
		                            { "asSorting": [ "desc", "asc" ] },
		                            { "asSorting": [ "desc", "asc" ] },
		                            { "asSorting": [ "desc", "asc" ] },
		                            { "asSorting": [ "desc", "asc" ] },
                            		{ "asSorting": [ "desc", "asc" ] },
		                            { "asSorting": [ "desc", "asc" ] },
		                            { "asSorting": [ "desc", "asc" ] },
		                            { "asSorting": [ "desc", "asc" ] },
		                            { "asSorting": [ "desc", "asc" ] },
		                            { "asSorting": [ "desc", "asc" ] }
	                              ] ,
                    "aaSorting": [[ 1, "asc" ]]
				} );
			} );
</script>
    
</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager> 
    <div id="body">
    <div id="cap"></div>
    <div id="main">
    <h1>Search User Data</h1>
        <asp:Panel ID="Panel1" runat="server"  width="100%" >
       
        <table id="MainTable" width="100%"  >
                    
                        <tr>
                            <td colspan="6" style="text-align: center" class="HeaderDiv">
                                <b>Search User</b></td>
                        </tr>
                        <tr>
                            <td class="alt1" >
                                Department Name</td>
                            <td class="alt1">
                                Username</td>
                            <td class="alt1">
                                First Name</td>
                            <td class="alt1">
                                Last Name</td>
                                <td class="alt1">
                                Mentor</td>
                                   
                        </tr>
                        <tr>
                            <td colspan="1" style="text-align: center; ">
                                <asp:DropDownList ID="DeptList" runat="server"  >
                                </asp:DropDownList></td>
                            <td colspan="1" style="text-align: center">
                                <asp:TextBox ID="Username" runat="server" Width="80px"></asp:TextBox></td>
                            <td colspan="1" style="text-align: center">
                                <asp:TextBox ID="FirstName" runat="server" Width="80px"></asp:TextBox></td>
                            <td colspan="1" style="text-align: center">
                                <asp:TextBox ID="LastName" runat="server" Width="88px"></asp:TextBox></td>
                                 <td colspan="1" style=" text-align: center">
                                     <asp:DropDownList ID="DLmentor" runat="server"       ForeColor="#C04000" Width="152px">
                 </asp:DropDownList>
                 </td>
                         
                        </tr>
            <tr>
                <td colspan="6" style="text-align: center">
       
                    <asp:Button ID="btnSubmit" CssClass="button"  runat="server" Text="Submit" /></td>
            </tr>
                    </table>
        <asp:Label ID="DispBox" runat="server" Font-Bold="True" 
             ForeColor="#C00000"></asp:Label>
        <asp:HiddenField ID="HActID" runat="server" />
        <asp:HiddenField ID="HTabName" runat="server" />
        &nbsp;&nbsp;&nbsp;
               
        
         </asp:Panel>
            <table id="tblMain" runat="server">
                <tr>
                    <td style="border:0">
                        <asp:LinkButton ID="LnlExport" runat="server">Export Result</asp:LinkButton>
                    </td>
                </tr>
                <tr>
                    <td style="border:0">
                        <asp:GridView  ID="MyDataGrid" runat="server" AutoGenerateColumns="False" Width="100%">
                        <Columns>
                            <asp:BoundField DataField="firstname" HeaderText="First Name" HeaderStyle-CssClass="Header" />
                            <asp:BoundField DataField="lastname" HeaderText="Last Name" HeaderStyle-CssClass="Header" />
                           <asp:BoundField  DataField="username" HeaderText="Username" HeaderStyle-CssClass="Header" /> 
                           <asp:TemplateField HeaderText="Category" HeaderStyle-CssClass="Header">
                                <ItemTemplate>
                                    <asp:Label ID="Label4" runat="server" Text='<%#iif(isdbnull(DataBinder.Eval(Container.DataItem, "CategoryID")),"",GetCategoryName(DataBinder.Eval(Container.DataItem, "CategoryID").ToString()))%>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                           <asp:TemplateField HeaderText="Department" HeaderStyle-CssClass="Header">
                                <ItemTemplate>
                                    <asp:Label ID="Label5" runat="server" Text='<%#iif(isdbnull(DataBinder.Eval(Container.DataItem, "DepartmentID")),"",GetDeptName(DataBinder.Eval(Container.DataItem, "DepartmentID").ToString()))%>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Designation" HeaderStyle-CssClass="Header">
                                <ItemTemplate>
                                    <asp:Label ID="Label6" runat="server" Text='<%#IIf(ISDBNULL(DataBinder.Eval(Container.DataItem, "DesID")),"",GetDesignationName(DataBinder.Eval(Container.DataItem, "DesID").ToString()))%>'/>
                                </ItemTemplate>
                            </asp:TemplateField>  
                           <asp:BoundField DataField="address" HeaderText="Address" HeaderStyle-CssClass="Header" />
                           <asp:BoundField DataField="city" HeaderText="City" HeaderStyle-CssClass="Header" />
                           <asp:BoundField DataField="state" HeaderText="State" HeaderStyle-CssClass="Header" />
                           <asp:BoundField DataField="country" HeaderText="Country" HeaderStyle-CssClass="Header" /> 
                             <asp:BoundField DataField="ChatID" HeaderText="ChatID" HeaderStyle-CssClass="Header" /> 
                               <asp:BoundField DataField="OtherMailID" HeaderText="Other E-Mail" HeaderStyle-CssClass="Header" /> 
                                <asp:BoundField DataField="OfficialMailID" HeaderText="E-Mail" HeaderStyle-CssClass="Header" />  
                              <asp:BoundField DataField="PhoneNo" HeaderText="PhoneNo" HeaderStyle-CssClass="Header" />  
                              <asp:BoundField DataField="CellNo" HeaderText="CellNo" HeaderStyle-CssClass="Header" />  
                           <asp:TemplateField HeaderText="Mentor" HeaderStyle-CssClass="Header">
                                <ItemTemplate>
                                    <asp:Label ID="Label3" runat="server" Text='<%#GetMentorName(DataBinder.Eval(Container.DataItem, "mentorID").toString())%>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField> 
                           <%-- <asp:TemplateField HeaderText="Platform" HeaderStyle-CssClass="Header">
                                <ItemTemplate>
                                    <asp:Label ID="Label1" runat="server" Text='<%#iif(isdbnull(DataBinder.Eval(Container.DataItem, "plataccid")),"Direct Accounts","")%>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField> --%>
                            
                            <asp:BoundField DataField="DateOfBirth" HeaderText="DateOfBirth" HeaderStyle-CssClass="Header" /> 
                            <asp:BoundField DataField="DateJoined" HeaderText="Joining Date" HeaderStyle-CssClass="Header" /> 
                            <asp:BoundField DataField="DateTerminated" HeaderText="Termination Date" HeaderStyle-CssClass="Header" />  
                            <asp:TemplateField HeaderText="Status" HeaderStyle-CssClass="Header">
                                <ItemTemplate>
                                    <asp:Label ID="Label2" runat="server" Text='<%#SetStatus(DataBinder.Eval(Container.DataItem, "IsDeleted"))%>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField> 
                            
                        </Columns>
                </asp:GridView>
                    </td>
                </tr>
                <%--<tr>
                    <td style="border:0">
                        <div style="text-align:left;">
        <asp:Panel ID="PLPage" runat="server" Height="20px" HorizontalAlign="Left" Width="125px">
            <asp:Table ID="Table3" runat="server" 
                Style="text-align: left">
                <asp:TableRow ID="TableRow1" runat="server">
                    <asp:TableCell ID="TCell1" runat="server">
                        <asp:RadioButtonList ID="RBPage" runat="server" 
                            
                            RepeatColumns="2" Width="200px">
                            <asp:ListItem Value="CP">Current Page</asp:ListItem>
                            <asp:ListItem Value="AP">All Pages</asp:ListItem>
                        </asp:RadioButtonList>
                    </asp:TableCell>
                    <asp:TableCell ID="TCell2" runat="server">
                        <asp:Button ID="Button1" runat="server"  CssClass="button" Text="Export Result" />
                    </asp:TableCell>
                </asp:TableRow>
            </asp:Table>
        </asp:Panel>
        </div>
                    </td>
                </tr>--%>
            </table>
            
            
         
          
        </div> 
        </div> 
        
    </form>
</body>
</html>
