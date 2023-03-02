<%@ Page Language="vb" AutoEventWireup="false" CodeFile="FormEditCust.aspx.vb" Inherits="FormEditCust" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Edit Customer</title>
	<link href= "../App_Themes/Css/Main.css" type="text/css" rel="stylesheet" />
    <link href= "../App_Themes/Css/Common.css" type="text/css" rel="stylesheet" />
    <link href= "../App_Themes/Css/Styles.css" type="text/css" rel="stylesheet" />
</head>
<body bgcolor="White">
    <form id="form1" runat="server">
	<div id="body" style="height: 100%;" >
    <div id="cap"></div>
    <div id="main">
	<h1>Edit Customer Portfolio</h1>
	<asp:Panel ID="Panel1" HorizontalAlign="Left" runat="server" >
    <div>
        <table cellspacing="0" width="50%">
            <tr>
                <td style="text-align: center; height: 15px;" colspan="2" class="HeaderDiv">
                    Customer Details</td>
            </tr>
            <tr>
                <td class="style6" bgcolor="#99CCFF">
                    Select Account</td>
                <td class="style3">
                    <asp:DropDownList ID="ddaccname" runat="server" AppendDataBoundItems="True" 
                        AutoPostBack="true" Height="16px" Width="391px" class="style7">
                        <asp:ListItem Selected="True" Text="Select Account"></asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td class="style6" bgcolor="#99CCFF">
                    Customer ID</td>
                <td class="style3">
                    <asp:TextBox ID="txtCustid" runat="server" class="style7" ReadOnly="True" AutoSize="True"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="style6" bgcolor="#99CCFF">
                    Billing ID</td>
                <td class="style3">
                    <asp:TextBox ID="txtbillid" runat="server" class="style7"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="style6" bgcolor="#99CCFF">
                    Account ID</td>
                <td class="style3">
                    <asp:TextBox ID="txtaccid" runat="server" class="style7"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="style6" bgcolor="#66CCFF">
                    Phone#</td>
                <td class="style3">
                    <asp:TextBox ID="txtphone" runat="server" class="style7"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="style6" bgcolor="#99CCFF">
                    Mail ID</td>
                <td class="style3">
                    <asp:TextBox ID="txtmailid" runat="server" class="style7"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="style6" bgcolor="#99CCFF">
                    Billing Process</td>
                <td class="style3">
                    <asp:TextBox ID="txtbillp" runat="server" Height="50px" Width="403px" TextMode="MultiLine"></asp:TextBox>
					
                </td>
            </tr>
            <tr>
                <td style="text-align: center; height: 15px;" colspan="2" class="HeaderDiv">
                    Interface Details</td>
            </tr>
            <tr>
                <td class="style6" bgcolor="#99CCFF">
                    Billing Method</td>
                <td class="style3">
                    <asp:TextBox ID="txtloc" runat="server"></asp:TextBox>
					 [Standard/Location-wise/Dictator-wise]
                </td>
            </tr>
            <tr>
                <td class="style6" bgcolor="#99CCFF">
                    EMR</td>
                <td class="style3">
                    <asp:TextBox ID="txtemr" runat="server"></asp:TextBox>
					[Interface Name like AllScript/SuccessEHS etc.]
                </td>
            </tr>
            <tr>
                <td class="style6" bgcolor="#99CCFF" >
                    Voice Capture</td>
                <td class="style3">
                    <asp:TextBox ID="txtvc" runat="server"></asp:TextBox>
					[DVR/Toll-Free/Form etc.]
                </td>
            </tr>
            <tr>
                <td class="style6" bgcolor="#99CCFF">
                    Voice Transfer</td>
                <td class="style3">
                    <asp:TextBox ID="txtvt" runat="server"></asp:TextBox>
					[SXF/SFTP etc.]
                </td>
            </tr>
            <tr>
                <td class="style6" bgcolor="#99CCFF">
                    Phone-In Keypad</td>
                <td class="style3">
                    <asp:TextBox ID="txtpk" runat="server"></asp:TextBox>
					[Toll-free keypad name like SXFKEYPAD 1 etc.]
                </td>
            </tr>
            <tr>
                <td class="style6" bgcolor="#99CCFF">
                    Demographics</td>
                <td class="style3">
                    <asp:TextBox ID="txtdemo" runat="server"></asp:TextBox>
					[Demographics from SXF/Mail/HL7 etc.]
                </td>
            </tr>
            <tr>
                <td class="style6" bgcolor="#99CCFF">
                    Reports</td>
                <td class="style3">
                    <asp:TextBox ID="txtrepo" runat="server"></asp:TextBox>
					[SXF/EMR/SFTP etc.]
                </td>
            </tr>
            <tr>
                <td class="style6" bgcolor="#99CCFF">
                    EMR Interface</td>
                <td class="style3">
                    <asp:TextBox ID="txtemri" runat="server"></asp:TextBox>
					[Auto/Manual]
                </td>
            </tr>
            <tr>
                <td class="style6" bgcolor="#99CCFF">
                    Fax Plus</td>
                <td class="style3">
                    <asp:TextBox ID="txtfplus" runat="server"></asp:TextBox>
					[Yes/No]
                </td>
            </tr>
            <tr>
                <td class="style6" bgcolor="#99CCFF">
                    Comments</td>
                <td class="style3">
                    <asp:TextBox ID="txtcomment" runat="server" Height="50px" Width="403px" TextMode="MultiLine"></asp:TextBox>
					[Comments if any]
                </td>
            </tr>
            <tr>
                <td style="text-align: center; height: 15px;" colspan="2" class="HeaderDiv">
                    Technical Details</td>
            </tr>
            <tr>
                <td class="style8" bgcolor="#99CCFF">
                    Demographics</td>
                <td class="style9">
                    <asp:TextBox ID="txtdemop" runat="server" Height="50px" Width="403px" TextMode="MultiLine"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="style6" bgcolor="#99CCFF">
                    Voice</td>
                <td class="style3">
                    <asp:TextBox ID="txtvp" runat="server" Height="50px" Width="403px" TextMode="MultiLine"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="style6" bgcolor="#99CCFF">
                    Reports</td>
                <td class="style3">
                    <asp:TextBox ID="txtrp" runat="server" Height="50px" Width="403px" TextMode="MultiLine"></asp:TextBox>
&nbsp;</td>
            </tr>
            <tr>
                <td class="style6" bgcolor="#99CCFF">
                    Samples</td>
                <td class="style3">
                    <asp:TextBox ID="txtsp" runat="server" Height="50px" Width="403px" TextMode="MultiLine"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="style6" bgcolor="#99CCFF">
                    Ref. Physician</td>
                <td class="style3">
                    <asp:TextBox ID="txtrpp" runat="server" Height="50px" Width="403px" TextMode="MultiLine"></asp:TextBox>
                </td>
            </tr>
            			 <tr>
                <td style="text-align: center; height: 15px;" colspan="2" class="HeaderDiv">
                    Production Process</td>
            </tr>
            <tr>
                <td class="style8" bgcolor="#99CCFF">
                    Special Process</td>
                <td class="style9">
                    <asp:TextBox ID="txtspp" runat="server" Height="50px" Width="403px" TextMode="MultiLine"></asp:TextBox>
                </td>
            </tr>
			<tr>
                <td class="style8" bgcolor="#99CCFF">
                    PPA Process</td>
                <td class="style9">
                    <asp:TextBox ID="txtppap" runat="server" Height="50px" Width="403px" TextMode="MultiLine"></asp:TextBox>
                </td>
            </tr>
			
			<tr>
                <td style="text-align: center; height: 15px;" colspan="2" class="HeaderDiv">
                    Process Document</td>
			</tr>
			<tr>
                <td class="style8" bgcolor="#99CCFF">
                    Upload Document</td>
                <td class="style9">
                   <asp:FileUpload ID="FileUpload1" runat="server" /> [.doc/.docx ONLY]
					<br/>
					[* NOTE - Uploaded New Document will replace existing process document]
                </td>
            </tr>
            <tr>
                <td class="style8" bgcolor="#99CCFF">
                    Upload Document 2</td>
                <td class="style9">
                   <asp:FileUpload ID="FileUpload2" runat="server" /> [.doc/.docx ONLY]
					<br/>
					[* NOTE - Uploaded New Document will replace existing process document]
                </td>
            </tr>
			<tr>
                <td>
                    Attached Document</td>
                <td class="style3">
                  
						<asp:GridView ID="GridView1" runat="server" HeaderStyle-BackColor="#3AC0F2" HeaderStyle-ForeColor="White" RowStyle-BackColor="#A1DCF2" AlternatingRowStyle-BackColor="White" AlternatingRowStyle-ForeColor="#000"    AutoGenerateColumns="false">
					<Columns>
						<asp:BoundField DataField="Fname" HeaderText="Document 1"/>
                        	
						<asp:TemplateField ItemStyle-HorizontalAlign = "Center">
					<ItemTemplate>
							
							<asp:LinkButton ID="lnkDelete" runat="server" Text="Delete" OnClick="DeleteFile"></asp:LinkButton>
					</ItemTemplate>
						</asp:TemplateField>
					</Columns>
				</asp:GridView>
				  
                  <asp:GridView ID="GridView2" runat="server" HeaderStyle-BackColor="#3AC0F2" HeaderStyle-ForeColor="White" RowStyle-BackColor="#A1DCF2" AlternatingRowStyle-BackColor="White" AlternatingRowStyle-ForeColor="#000"    AutoGenerateColumns="false">
					<Columns>
						<asp:BoundField DataField="Fname2" HeaderText="Document 2"/>
                        	
						<asp:TemplateField ItemStyle-HorizontalAlign = "Center">
					<ItemTemplate>
							
							<asp:LinkButton ID="lnkDelete" runat="server" Text="Delete" OnClick="DeleteFile2"></asp:LinkButton>
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
                    <asp:TextBox ID="txtdoc1" runat="server"  Width="403px" ></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="style8" bgcolor="#99CCFF">
                    Document URL 2</td>
                <td class="style9">
                    <asp:TextBox ID="txtdoc2" runat="server"  Width="403px" ></asp:TextBox>
                </td>
            </tr>
              <tr>
                <td class="style8" bgcolor="#99CCFF">
                    DocumentURL 3</td>
                <td class="style9">
                    <asp:TextBox ID="txtdoc3" runat="server" Width="403px" ></asp:TextBox>
                </td>
            </tr>
			<tr>
                <td style="text-align: center;" colspan="2">
                    <asp:Button ID="Button1" runat="server" CssClass="button"  Text="Submit" />
                </td>
            </tr>
        </table>
    
    </div>
	</asp:Panel>
    </form>
</body>

</html>
