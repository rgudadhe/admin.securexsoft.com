<%@ Page Language="VB" AutoEventWireup="false" CodeFile="EdActDetails.aspx.vb" Inherits="Account_EdActDetails" %>
<%@ Register Assembly="KMobile.Web" Namespace="KMobile.Web.UI.WebControls" TagPrefix="asp" %>
<%@ Register
    Assembly="AjaxControlToolkit"
    Namespace="AjaxControlToolkit"
    TagPrefix="ajaxToolkit" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 1.0 Transitional/EN">

<html xmlns="http://www.w3.org/1999/xhtml" >

<head runat="server">
<link href= "../styles/styles.css" type="text/css" rel="stylesheet"/>
    <link href= "../App_Themes/Css/Common.css" type="text/css" rel="stylesheet"/>
    <title>Edit Account</title>
</head>
<body style="height:1000; ">
    <form id="form1" runat="server">
    <div id="body">
    <div id="cap"></div>
    <div id="main">
    <h1>Edit Account Details</h1> 
  
     <div style="text-align:left;" >        
   <strong>Account Status: </strong><asp:DropDownList ID="DLActive" runat="server" AutoPostBack="true"  >
        <asp:ListItem Text="Active"></asp:ListItem>  
          <asp:ListItem Text="Inactive"></asp:ListItem>  
        </asp:DropDownList> 
        <strong>Instance ID : </strong>
         <asp:DropDownList ID="DLInstance" runat="server" AutoPostBack="true" >
            <asp:ListItem Text="1" Value="1"></asp:ListItem>
            <asp:ListItem Text="2" Value="2"></asp:ListItem>
         </asp:DropDownList>
        </div> 
      
                        <asp:CompleteGridView  ID="MyDataGrid" runat="server" AutoGenerateColumns="False" 
                    AllowPaging="True" CellPadding="4" AllowSorting="True" CssClass="common"
                    Width="100%" GridLines="Both"     
                    ForeColor="#333333"   PageSize="500" CountFormat="Displaying records <b>{0}</b> to <b>{1}</b> from <b>{2}</b>" ShowCount="False" Font-Italic="False" CaptionAlign="Bottom" ShowInsertRow="True"  SortAscendingImageUrl="" SortDescendingImageUrl="" HorizontalAlign="Left" >
                            <Columns>
                                 
                                <asp:HyperlinkField DataTextField="AccountName" DataNavigateUrlFields="AccountID" DataNavigateUrlFormatString="actdetails.aspx?accountid={0}"
  HeaderText="Account Name" SortExpression="AccountName" ItemStyle-HorizontalAlign="Left"  />
                                <asp:BoundField DataField="AccountNo" HeaderText="Account Number" SortExpression="AccountNo" />
                                <asp:BoundField DataField="foldername" HeaderText="Folder Name" SortExpression="foldername" />
                                <asp:BoundField DataField="BillActnumber" HeaderText="Billing Account Number" SortExpression="BillActnumber" />
                                <asp:BoundField DataField="OfficeID" HeaderText="SDox Office ID" SortExpression="OfficeID" />
                                <asp:BoundField DataField="ProtocolMins" HeaderText="Expected Daily Minutes" SortExpression="ProtocolMins" />
                                
                            </Columns>
                            <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                            <HeaderStyle HorizontalAlign="Center"  BackColor="#5D7B9D" cssclass="Title"   />
                            <EditRowStyle BackColor="#999999" />
                            <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                            <AlternatingRowStyle BackColor="White" ForeColor="#284775" HorizontalAlign="Center" />
                            <RowStyle BackColor="#F7F6F3" ForeColor="#333333" HorizontalAlign="Center" />
                            <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                        
                </asp:CompleteGridView>
               <br />
        
        
        
       
    </div>
   </div> 
    </form>
</body>
</html>
