<%@ Page Language="VB" AutoEventWireup="false" CodeFile="EmployeeDetails.aspx.vb" Inherits="Billing_LCMethods_LCMethodology" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Register Assembly="KMobile.Web" Namespace="KMobile.Web.UI.WebControls" TagPrefix="asp" %>

<%@ Register
    Assembly="AjaxControlToolkit"
    Namespace="AjaxControlToolkit"
    TagPrefix="ajaxToolkit" %>
    
<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
<link rel="stylesheet" type="text/css" href="../../App_Themes/Css/Styles.css"/>
<link rel="stylesheet" type="text/css" href="../../App_Themes/Css/Common.css"/>
<title>View VAS Details</title>
</head>
<body>
    <form id="form1" runat="server">
        <div id="body">
        <div id="cap"></div>
        <div id="main">
        <h1>View Employee Targets </h1>
        <asp:Panel ID="Panel1" runat="server" HorizontalAlign="Left">
            <div>
     
                    <asp:Label ID="lblDisp" runat="server" CssClass="Title" ForeColor="#C00000"></asp:Label>&nbsp;
                    
                    
                      <asp:CompleteGridView ID="MyDataGrid" runat="server" AllowSorting="True" 
                    DataKeyNames="userid"  
                    EnableViewState="False"  
                    Width="400" PageSize="25" CountFormat="Displaying records <b>{0}</b> to <b>{1}</b> of <b>{2}</b>" CaptionAlign="Bottom" AutoGenerateEditButton="True"  AutoGenerateColumns="False" ShowInsertRow="True" ShowFilter="false"  >

                <FooterStyle BackColor="#5D7B9D" ForeColor="White" Font-Bold="True"></FooterStyle>
                <EditRowStyle BackColor="#999999" HorizontalAlign="Center"></EditRowStyle>
                <PagerStyle BackColor="LightSlateGray" BorderStyle="Groove" ForeColor="White" BorderColor="#E0E0E0" HorizontalAlign="Center"></PagerStyle>
                <PagerSettings PreviousPageText="Previous" PageButtonCount="25" Mode="NumericFirstLast" LastPageText="Last Page" FirstPageText="First Page" NextPageText="Next Page"></PagerSettings>
                
                          <Columns>
                          <asp:TemplateField HeaderText="Name" HeaderStyle-CssClass="alt1">
                           <ItemTemplate>                    <asp:Label ID="lblUName" runat="server" Text='<%# Eval("uname") %>' />  
                                       </ItemTemplate>   
                                                  
                                       </asp:TemplateField> 
                                        <asp:TemplateField HeaderText="ID" HeaderStyle-CssClass="alt1">
                           <ItemTemplate>                    <asp:Label ID="lblID" runat="server" Text='<%# Eval("username") %>' />  
                                       </ItemTemplate>   
                                                  
                                       </asp:TemplateField> 
                          
                              <%--<asp:BoundField DataField="uname"   HeaderText="Name" SortExpression="uname" HeaderStyle-CssClass="alt1" />--%>
                              <asp:BoundField DataField="Target"   HeaderText="Target" SortExpression="Target" HeaderStyle-CssClass="alt1" />
                              <asp:BoundField DataField="Currency"  HeaderText="Currency" SortExpression="Currency" HeaderStyle-CssClass="alt1" />
                              <asp:BoundField DataField="Salary" HeaderText="Salary"  SortExpression="Salary" HeaderStyle-CssClass="alt1" >
                                  <%--<TargetStyle HorizontalAlign="Right" />--%>
                              </asp:BoundField>
                             <%-- <asp:BoundField DataField="Mode" HeaderText="Mode" SortExpression="Mode" HeaderStyle-CssClass="alt1" />--%>
                          </Columns>
                </asp:CompleteGridView>
      <%--  <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:SecureWebConnectionString %>"
           
            updatecommand="UPDATE TargetDETAILS SET Target=@Target, Description=@Description, Rate=@Rate, mode=@mode where Targetid=@Targetid"
          
            Insertcommand="Insert Into TargetDETAILS (Target, Description, Rate, mode) Values  (@Target, @Description, @Rate, @mode)"
            >
        </asp:SqlDataSource>--%>
    </div>
        </asp:Panel>
        </div> 
        </div> 
    </form>
</body>
</html>
