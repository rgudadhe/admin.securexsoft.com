<%@ Page Language="VB" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<script runat="server">

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs)
        iResponse.Text = Request("Message").ToString
        'Response.Write(Request("ConID").ToString)
    End Sub

    Protected Sub btnAddUser_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim clsUser As New ETS.BL.Users
        
        Dim EPass As New EncryPass.Encry
        With clsUser
            .UserID = System.Guid.NewGuid.ToString
            .UserName = TxtUname.Text
            .Password = EPass.encrypt(TxtUname.Text.ToLower, txtPass.Text)
            .FirstName = TxtFirstName.Text
            .LastName = TxtLastName.Text
            .OfficialMailID = TxtEmail.Text
            .OtherMailID = TxtNonOEmail.Text
            .ChatID = TxtChatID.Text
            .Address = TxtAdd.Text
            .City = TxtCity.Text
            .State = TxtState.Text
            .Country = txtCountry.TabIndex
            .DateJoined = TxtJoin.Text
            .CellNo = TxtCell.Text
            .PhoneNo = TxtTel.Text
            .ContractorID = Request("ConID").ToString
        End With
        
        If clsUser.AddContractorAdmin_submit(IIf(LCase(Request("isSubCon")) = "false", False, True)) Then
            'Response.Write(clsUser.Instance_ContractorAdmin_Setup(clsUser.UserID.ToString))
            If clsUser.Instance_ContractorAdmin_Setup(clsUser.UserID.ToString) = True Then
                iResponse.Text = "User has been added successfully."
                pnlForm.Visible = False
                
            End If
        Else
            iResponse.Text = "Adding user failed."
        End If
       
    End Sub
</script>


<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>Untitled Page</title>
    <link href= "../styles/Default.css" type="text/css" rel="stylesheet"/>

<script language="javascript" type="text/javascript">
<!--

function Button1_onclick() {
history.back()
}

// -->
</script>
</head>
<body>
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
    <div>
        <div><asp:Label ID="iResponse" runat="server" Text="Label"></asp:Label></div> 
        <asp:Panel ID="pnlForm" runat="server" Height="50px" Width="125px">        
                    <table id="Table2" border="2" cellpadding="2"  width="100%" style="font-size: 10pt; font-family: 'Trebuchet MS'; font-style: italic; color:Gray; " >
            <tr>
                <td colspan="4" style="text-align: center;" class="HeaderDiv">
                   <em><strong><span style="font-family: Trebuchet MS">Register Admin User</span></strong></em></td>
            </tr>
            <tr style="font-size: 10pt; font-style: italic; font-family: Trebuchet MS">
                <td style="width: 25%; text-align: right; text-align: right;">
                    <span>*First Name</span></td>
                <td style="width: 337px; text-align: left;">
                    <asp:TextBox ID="TxtFirstName" runat="server"></asp:TextBox></td>
                <td style="width: 25%; text-align: right; text-align: right;">
                    <span>*Last Name</span></td>
                <td style="width: 25%; text-align: left;">
                    <asp:TextBox ID="TxtLastName" runat="server"></asp:TextBox></td>
            </tr>
            <tr style="font-size: 10pt; font-style: italic; font-family: Trebuchet MS">
                <td style="width: 25%; text-align: right; text-align: right;">
                    <span>Mail ID 1</span></td>
                <td style="width: 337px; height: 21px; text-align: left;">
                    <asp:TextBox ID="TxtEmail" runat="server"></asp:TextBox></td>
                <td style="width: 25%; height: 21px; text-align: right;">
                    <span>
                    Yahoo Chat ID</span></td>
                <td style="width: 25%; height: 21px; text-align: left;">
                    <asp:TextBox ID="TxtChatID" runat="server"></asp:TextBox></td>
            </tr>
            <tr style="font-size: 10pt; font-style: italic; font-family: Trebuchet MS">
                <td style="width: 25%; text-align: right; text-align: right;">
                <span>Mail ID 2</span></td>
                <td style="width: 337px; text-align: left;">
                    <asp:TextBox ID="TxtNonOEmail" runat="server"></asp:TextBox></td>
                <td style="width: 25%; text-align: right; text-align: right;">
                <span>
                    Joining Date</span></td>
                <td style="width: 25%; text-align: left; text-align: left;">
                    <asp:TextBox ID="TxtJoin" runat="server"></asp:TextBox></td>
            </tr>
            <tr style="font-size: 10pt; font-style: italic; font-family: Trebuchet MS">
                <td style="width: 25%; text-align: right; text-align: right;">
                <span>
                    City</span></td>
                <td style="width: 337px; height: 22px; text-align: left;">
                    <asp:TextBox ID="TxtCity" runat="server"></asp:TextBox></td>
                <td style="width: 25%; height: 22px; text-align: right;">
                    <span>
                    Country</span></td>
                <td style="width: 25%; height: 22px; text-align: left;">
                    <asp:DropDownList ID="txtCountry" runat="server">
                        <asp:ListItem Selected="True">India</asp:ListItem>
                        <asp:ListItem>USA</asp:ListItem>
                        <asp:ListItem>UK</asp:ListItem>
                    </asp:DropDownList></td>
            </tr>
            <tr style="font-size: 10pt; font-style: italic; font-family: Trebuchet MS">
                <td style="width: 25%; text-align: right; height: 30px; text-align: right;">
                <span>
                    State
                    </span></td>
                <td style="width: 337px; height: 21px; height: 30px; text-align: left;">
                    <asp:TextBox ID="TxtState" runat="server"></asp:TextBox></td>
                <td style="width: 25%; height: 21px; text-align: right; height: 30px;">
                <span>
                    Address</span>        </td>
                <td style="width: 25%; height: 21px; height: 30px; text-align: left;">
                    <asp:TextBox ID="TxtAdd" runat="server"></asp:TextBox></td>
            </tr>
            <tr style="font-size: 10pt; font-style: italic; font-family: Trebuchet MS">
                <td style="width: 25%; text-align: right; text-align: right;">
                    Tel#</td>
                <td style="width: 337px; height: 26px; text-align: left;">
                    <asp:TextBox ID="TxtTel" runat="server"></asp:TextBox></td>
                <td style="width: 25%; height: 26px; text-align: right;">
                <span>
                    Cell#</span></td>
                <td style="width: 25%; height: 26px; text-align: left;">
                    <asp:TextBox ID="TxtCell" runat="server"></asp:TextBox><ajaxToolkit:FilteredTextBoxExtender
                        ID="FilteredTextBoxExtender1"
                        runat="server"
                        TargetControlID="TxtCell"
                    FilterType="Numbers" /></td>
            </tr>
            <tr style="font-size: 10pt; font-style: italic; font-family: Trebuchet MS">
                <td style="width: 25%; text-align: right; text-align: right;">
                <span>
                        <asp:Label ID="LblUN" runat="server" Font-Names="Trebuchet MS" Font-Size="10pt"
                            Text="Username"></asp:Label></span></td>
                <td style="width: 337px; text-align: left; height: 26px; text-align: left;">
                    &nbsp;<asp:TextBox ID="TxtUname" runat="server"></asp:TextBox></td>
                <td style="width: 25%; text-align: right; height: 26px; text-align: right;">
                    Password</td>
                <td style="width: 25%; height: 26px; text-align: left;">
                    <asp:TextBox ID="txtPass" runat="server" TextMode="Password"></asp:TextBox></td>
            </tr>
            
            <tr style="font-size: 10pt; font-style: italic; font-family: Trebuchet MS">
                <td colspan="2" style="text-align: right">
                        <span></span></td>
                <td style="text-align: left" colspan="2">
                    <span>
                        </span>
                    </td>
            </tr>
            <tr>
            <td colspan="4">
                <asp:Button ID="btnAddUser" runat="server" Text="Create Admin User" OnClick="btnAddUser_Click" />
            </td>            
            </tr>
        </table>                    
        </asp:Panel>                                 
        </div>
    </form>
</body>
</html>