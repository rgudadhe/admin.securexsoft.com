<%@ Page Language="vb" AutoEventWireup="false" CodeFile="FormViewCust.aspx.vb" Inherits="FormViewCust" %>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>View Customer Profile</title>
	<link href= "../App_Themes/Css/Main.css" type="text/css" rel="stylesheet" />
    <link href= "../App_Themes/Css/Common.css" type="text/css" rel="stylesheet" />
    <link href= "../App_Themes/Css/Styles.css" type="text/css" rel="stylesheet" />
</head>
<body style="text-align: left">
    <form id="form1" runat="server">
	 <div id="body" style="height: 100%;" >
    <div id="cap"></div>
    <div id="main">
	<h1>View Customer Portfolio</h1>
    <asp:Panel ID="Panel1" HorizontalAlign="Left" runat="server" >
    <div>
		
        <table width="50%">
            <tr>
                <td style="text-align: center; height: 15px;" colspan="2" class="HeaderDiv">
                    Customer Details</td>
            </tr>
            <tr>
                <td class="style4">
                    Select Customer</td>
                <td class="style3">
                    <asp:DropDownList ID="ddacct" AppendDataBoundItems="True" 
                        AutoPostBack="true" runat="server">
						 <asp:ListItem Selected="True">Select Here</asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td class="style4">
                    Billing ID</td>
                <td class="style3">
                    <asp:Label ID="lblbillid" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="style4">
                    Account ID</td>
                <td class="style3">
                    <asp:Label ID="lblaccid" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="style4">
                    Primary Phone#</td>
                <td class="style3">
                    <asp:Label ID="lblphone" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="style4">
                    Primary Email ID</td>
                <td class="style3">
                    <asp:Label ID="lblemail" runat="server"></asp:Label>
                </td>
            </tr>
			 <tr>
                <td class="style4">
                    Billing Process</td>
                <td class="style3">
                    <asp:Label ID="lblbillp" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <td style="text-align: center; height: 15px;" class="HeaderDiv" colspan="2" >
                    Interface Details</td>
            </tr>
            <tr>
                <td class="style4">
                    Billing Method</td>
                <td class="style3">
                    <asp:Label ID="lbllctn" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="style4">
                    EMR
                </td>
                <td class="style3">
                    <asp:Label ID="lblemr" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="style4">
                    Voice Capture</td>
                <td class="style3">
                    <asp:Label ID="lblvc" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="style4">
                    Voice Transfer</td>
                <td class="style3">
                    <asp:Label ID="lblvt" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="style4">
                    Phone-In Keypad</td>
                <td class="style3">
                    <asp:Label ID="lblkeypad" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="style4">
                    Demographics</td>
                <td class="style3">
                    <asp:Label ID="lbldemo" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="style4">
                    Reports</td>
                <td class="style3">
                    <asp:Label ID="lblreport" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="style4">
                    EMR Interface</td>
                <td class="style3">
                    <asp:Label ID="lblemrint" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="style4">
                    Fax Plus</td>
                <td class="style3">
                    <asp:Label ID="lblfplus" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="style4">
                    Special Commt.</td>
                <td class="style3">
                    <asp:Label ID="lblcommt" TextMode="MultiLine" runat="server"></asp:Label>
                </td>
            </tr>
			<tr>
                <td style="text-align: center; height: 15px;" class="HeaderDiv" colspan="2" >
                    Technical Details</td>
            </tr>
			<tr>
                <td class="style4">
                    Demographics</td>
                <td>
                    <asp:Label ID="lbltdemo" runat="server" TextMode="MultiLine"></asp:Label>
                </td>
            </tr>
			<tr>
                <td class="style4">
                    Voice</td>
                <td class="style3">
                    <asp:Label ID="lbltvoice" runat="server"></asp:Label>
                </td>
            </tr>
			<tr>
                <td class="style4">
                    Reports</td>
                <td class="style3">
                    <asp:Label ID="lbltreports" runat="server"></asp:Label>
                </td>
            </tr>
			<tr>
                <td class="style4">
                    Samples</td>
                <td class="style3">
                    <asp:Label ID="lbltsamples" runat="server"></asp:Label>
                </td>
            </tr>
			<tr>
                <td class="style4">
                    Ref. Physician</td>
                <td class="style3">
                    <asp:Label ID="lbltrefp" runat="server"></asp:Label>
                </td>
            </tr>
			<tr>
                <td style="text-align: center; height: 15px;" class="HeaderDiv" colspan="2" >
                    Production Process</td>
            </tr>
			<tr>
                <td class="style4">
                    Special Process</td>
                <td class="style3">
                    <asp:Label ID="lblsplp" runat="server"></asp:Label>
                </td>
            </tr>
			<tr>
                <td class="style4">
                    PPA Process</td>
                <td class="style3">
                    <asp:Label ID="lblppap" runat="server"></asp:Label>
                </td>
            </tr>
			<tr>
                <td style="text-align: center; height: 15px;" class="HeaderDiv" colspan="2" >
                    Process Document</td>
            </tr>
			<tr>
                <td>
                    Download Document</td>
                <td class="style3">
                  
						<asp:GridView ID="GridView1" runat="server" HeaderStyle-BackColor="#3AC0F2" HeaderStyle-ForeColor="White" RowStyle-BackColor="#A1DCF2" AlternatingRowStyle-BackColor="White" AlternatingRowStyle-ForeColor="#000"    AutoGenerateColumns="false">
					<Columns>
						<asp:BoundField DataField="Fname" HeaderText="Document 1"/>
                      <%--  <asp:BoundField DataField="Fname2" HeaderText="Document 2"/>--%>
						<asp:TemplateField ItemStyle-HorizontalAlign = "Center">
					<ItemTemplate>
							<asp:LinkButton ID="lnkDownload" runat="server" Text="Download" OnClick="DownloadFile" CommandArgument='<%# Eval("Id") %>'></asp:LinkButton>
					</ItemTemplate>
						</asp:TemplateField>
					</Columns>
				</asp:GridView>
				  
                  	<asp:GridView ID="GridView2" runat="server" HeaderStyle-BackColor="#3AC0F2" HeaderStyle-ForeColor="White" RowStyle-BackColor="#A1DCF2" AlternatingRowStyle-BackColor="White" AlternatingRowStyle-ForeColor="#000"    AutoGenerateColumns="false">
					<Columns>
						<asp:BoundField DataField="Fname2" HeaderText="Document 2"/>
                      <%--  <asp:BoundField DataField="Fname2" HeaderText="Document 2"/>--%>
						<asp:TemplateField ItemStyle-HorizontalAlign = "Center">
					<ItemTemplate>
							<asp:LinkButton ID="lnkDownload" runat="server" Text="Download" OnClick="DownloadFile2" CommandArgument='<%# Eval("Id") %>'></asp:LinkButton>
					</ItemTemplate>
						</asp:TemplateField>
					</Columns>
				</asp:GridView>
				  
                </td>
            </tr>
              <tr>
                <td class="style8" bgcolor="#99CCFF">
                    Document URL 1</td>
                <td class="style9">
                    <asp:Label ID="lbldoc1" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="style8" bgcolor="#99CCFF">
                    Document URL 2</td>
                <td class="style9">
                    <asp:Label ID="lbldoc2" runat="server"></asp:Label>
                </td>
            </tr>
              <tr>
                <td class="style8" bgcolor="#99CCFF">
                    Document URL 3</td>
                <td class="style9">
                    <asp:Label ID="lbldoc3" runat="server"></asp:Label>
                    <a href="c:\text.txt">See</a>
                </td>
            </tr>
        </table>
    
    </div>
	    </asp:Panel>
    </form>
</body>
</html>
