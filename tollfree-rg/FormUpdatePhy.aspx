<%@ Page Language="VB" AutoEventWireup="false" CodeFile="FormUpdatePhy.aspx.vb" Inherits="FormUpdatePhy" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<<head id="Head1" runat="server">
    <title>Edit Physician</title>
	<link href= "../App_Themes/Css/Main.css" type="text/css" rel="stylesheet" />
    <link href= "../App_Themes/Css/Common.css" type="text/css" rel="stylesheet" />
    <link href= "../App_Themes/Css/Styles.css" type="text/css" rel="stylesheet" />
</head>
<body style="font-size: 12pt">
    <form id="form1" runat="server">
	 <div id="body" style="height: 100%;" >
	 <div id="cap"></div>
	<div id="main">
   <h1>Edit Physician Physician</h1>
    <div>
	<asp:DropDownList ID="acct" runat="server" AutoPostBack="true" AppendDataBoundItems="True"></asp:DropDownList>
	

	
            <asp:GridView ID="GridView1" runat="server" Width="550px" AutoGenerateColumns="false"
    AlternatingRowStyle-BackColor="AliceBlue" HeaderStyle-BackColor="ActiveCaption" ShowFooter="true" Font-Names="Arial" Font-Size="X-Small" PageSize = "10">
    <Columns>
        <asp:TemplateField HeaderText="Account Name">
            <ItemTemplate>
                <asp:Label ID="Label1" runat="server" Text='<%# Eval("accname") %>'></asp:Label>
                
            </ItemTemplate>
            <FooterTemplate>
                <asp:TextBox ID="txtaccname" runat="server" Text=""/>
            </FooterTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="Last Name">
            <ItemTemplate>
                <asp:Label ID="Label2" runat="server" Text='<%#Eval("diclname")%>'></asp:Label>
                
            </ItemTemplate>
            <EditItemTemplate>
            <asp:TextBox ID="txtlname" runat="server" Text='<%#Eval("diclname")%>'></asp:TextBox>
            </EditItemTemplate>
            <FooterTemplate>
                <asp:TextBox ID="txtlname" runat="server" />
            </FooterTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="First Name">
            <ItemTemplate>
            <asp:Label ID="Label3" runat="server" Text='<%#Eval("dicfname")%>'></asp:Label>
               
            </ItemTemplate>
            <EditItemTemplate>
            <asp:TextBox ID="txtfname" runat="server" Text='<%#Eval("dicfname")%>'></asp:TextBox>
            </EditItemTemplate>
            <FooterTemplate>
                <asp:TextBox ID="txtfname" runat="server"></asp:TextBox>
            </FooterTemplate>
        </asp:TemplateField>
          <asp:TemplateField HeaderText="Keypad">
            <ItemTemplate>
             <asp:Label ID="Label4" runat="server" Text='<%#Eval("Keypad")%>'></asp:Label>
               
            </ItemTemplate>
             <EditItemTemplate>
            <asp:TextBox ID="txtkeypad" runat="server" Text='<%#Eval("Keypad")%>'></asp:TextBox>
            </EditItemTemplate>
            <FooterTemplate>
                <asp:TextBox ID="txtkeypad" runat="server"></asp:TextBox>
            </FooterTemplate>
        </asp:TemplateField>
          <asp:TemplateField HeaderText="Id">
            <ItemTemplate>
            <asp:Label ID="Label5" runat="server" Text='<%#Eval("id")%>'></asp:Label>
             <asp:HiddenField ID="iddel" Value='<%#Eval("id").ToString %>' runat="server" />    
            </ItemTemplate>
             <EditItemTemplate>
            <asp:TextBox ID="txtid" runat="server" Text='<%#Eval("id")%>'></asp:TextBox>
            </EditItemTemplate>
            <FooterTemplate>
                <asp:TextBox ID="txtid" runat="server"></asp:TextBox>
            </FooterTemplate>
        </asp:TemplateField>
         <asp:TemplateField HeaderText="Password">
            <ItemTemplate>
            <asp:Label ID="Label6" runat="server" Text='<%#Eval("password")%>'></asp:Label>
                
            </ItemTemplate>
             <EditItemTemplate>
            <asp:TextBox ID="txtpassword" runat="server" Text='<%#Eval("password")%>'></asp:TextBox>
            </EditItemTemplate>
            <FooterTemplate>
                <asp:TextBox ID="txtpassword" runat="server"></asp:TextBox>
            </FooterTemplate>
        </asp:TemplateField>
         <asp:TemplateField HeaderText="CType">
            <ItemTemplate>
            <asp:Label ID="Label7" runat="server" Text='<%#Eval("ctype")%>'></asp:Label>
                
            </ItemTemplate>
            <FooterTemplate>
                <asp:TextBox ID="txtctype" runat="server"></asp:TextBox>
            </FooterTemplate>
        </asp:TemplateField>
         <asp:TemplateField HeaderText="CustID">
            <ItemTemplate>
            <asp:Label ID="Label8" runat="server" Text='<%#Eval("custid")%>'></asp:Label>
                
            </ItemTemplate>
            <FooterTemplate>
                <asp:TextBox ID="txtcustid" runat="server"></asp:TextBox>
            </FooterTemplate>
        </asp:TemplateField>
         <asp:TemplateField HeaderText="Partition">
            <ItemTemplate>
            <asp:Label ID="Label9" runat="server" Text='<%#Eval("partition")%>'></asp:Label>
            
                
            </ItemTemplate>
            <EditItemTemplate>
            <asp:TextBox ID="txtpartition" runat="server" Text='<%#Eval("partition")%>'></asp:TextBox>
            </EditItemTemplate>
            <FooterTemplate>
                <asp:TextBox ID="txtpartition" runat="server"></asp:TextBox>
            </FooterTemplate>
        </asp:TemplateField>
         <asp:TemplateField HeaderText="PartitionNo">
            <ItemTemplate>
            <asp:Label ID="Label10" runat="server" Text='<%#Eval("partitionno")%>'></asp:Label>
                
            </ItemTemplate>
            <EditItemTemplate>
            <asp:TextBox ID="txtpno" runat="server" Text='<%#Eval("partitionno")%>'></asp:TextBox>
            </EditItemTemplate>
            <FooterTemplate>
                <asp:TextBox ID="txtpno" runat="server"></asp:TextBox>
            </FooterTemplate>
        </asp:TemplateField>
            <asp:TemplateField>
			 <EditItemTemplate> 
              <asp:LinkButton ID="LinkButton1" runat="server" CssClass="common" CausesValidation="False" CommandName="Update" Text="Update"></asp:LinkButton> 
              <asp:LinkButton ID="LinkButton2" runat="server" CssClass="common" CausesValidation="False" CommandName="Cancel" Text="Cancel"></asp:LinkButton> 
             </EditItemTemplate> 
            <ItemTemplate>
			 <asp:LinkButton ID="LinkButton1" runat="server" CssClass="common" CausesValidation="False" CommandName="Edit" Text="Edit"></asp:LinkButton> 
            </ItemTemplate>
            <FooterTemplate>
                <asp:Button ID="btnAdd" runat="server" Text="Add" OnClick="Add" CommandName = "Footer" />
            </FooterTemplate>
        </asp:TemplateField>
		
	</Columns>
    <AlternatingRowStyle BackColor="AliceBlue" />
   </asp:GridView>
          
 <Triggers>
</Triggers>          
    </div>
    </form>
</body>
</html>
