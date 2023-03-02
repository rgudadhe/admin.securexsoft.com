<%@ Page Language="VB" Inherits ="BasePage"%>
<%@ Register
    Assembly="AjaxControlToolkit"
    Namespace="AjaxControlToolkit"
    TagPrefix="ajaxToolkit" %>


<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 1.0 Transitional/EN">
<script type="text/javascript">
    function customOpen(url) {
        var w = window.open(url, '', 'width=600,height=600,toolbar=0,status=0,location=0,menubar=0,directories=0,resizable=1,scrollbars=1');
        w.focus();

    }
</script>
<script runat="server" type="text/VB">
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs)
        'Response.Write(Session("ContractorID"))
        If Not IsPostBack Then
            reBind()
        Else
            
        End If
    End Sub
    Private Sub reBind()
      
        Dim clsAtt As New ETS.BL.Attributes
        Dim DSAtt As Data.DataSet = clsAtt.getEditAttributes(Session("ContractorID"))
        clsAtt = Nothing
        rptCon.DataSource = DSAtt
        rptCon.DataBind()
        DSAtt.Dispose()
            
    End Sub
        
    Protected Sub Button1_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim btn As Button
        Dim hdn As HiddenField
        Dim txt As TextBox
        Dim chk As CheckBox
        Dim AttribID, AttribName, AttribCap As String
        Dim AttribType As Integer
        Dim isDeleted As Boolean
        btn = CType(sender, Button)
        btn.Enabled = False
        hdn = btn.Parent.FindControl("hdnAttribID")
        AttribID = hdn.Value
        hdn = btn.Parent.FindControl("hdnTypeNo")
        AttribType = hdn.Value
        txt = btn.Parent.FindControl("txtAttribName")
        AttribName = txt.Text
        txt = btn.Parent.FindControl("txtCaption")
        AttribCap = txt.Text
        chk = btn.Parent.FindControl("chkDelete")
        If chk.Checked = True Then
            isDeleted = True
        Else
            isDeleted = False
        End If
            
        If AttribID <> "" Then
            Dim ClsAtt As New ETS.BL.Attributes
            With ClsAtt
                .AttributeID = AttribID
                If isDeleted Then
                    If .DeleteAttribute Then
                        reBind()
                    End If
                Else
                    .Name = AttribName
                    .Caption = AttribCap
                    .Type = AttribType
                            
                    If .UpdateAttributeDetails Then
                        reBind()
                    End If
                End If
            End With
        End If
    End Sub
    Protected Sub DDType_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim ddList As DropDownList = CType(sender, DropDownList)
        If ddList.SelectedValue <> "" Then
            Dim lbl As Label = ddList.Parent.FindControl("lblType")
            lbl.Text = ddList.SelectedItem.Text
            lbl.Visible = True
            Dim hdn As HiddenField = ddList.Parent.FindControl("hdnTypeNo")
            hdn.Value = ddList.SelectedItem.Value
            Dim btn As Button = ddList.FindControl("Button1")
            btn.Enabled = True
            ddList.SelectedIndex = 0
            ddList.Visible = False
            btn=ddList.FindControl("iPopUpOp")
            if ddList.SelectedItem.Value="5" then                
                btn.visible=true
            else
                btn.visible=false
            end if
        End If
    End Sub
    Protected Sub iPopUp_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim btn As Button = CType(sender, Button)
        Dim ddlist As DropDownList = btn.Parent.FindControl("DDType")
        Dim lbl As Label = btn.Parent.FindControl("lblType")
        If Not ddlist.Visible Then
            ddlist.Visible = True
            lbl.Visible = False
            btn.ToolTip = "Click to reset"
        Else
            ddlist.Visible = False
            lbl.Visible = True
            btn.ToolTip = "Click here to change Data Type"
        End If
    End Sub
     Protected Sub iPopUpOp_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Try
            Dim btn As Button = CType(sender, Button)
            Dim hdn As HiddenField = btn.Parent.FindControl("hdnAttribID")
        
            Dim url As String = "UpdateOptions.aspx?AttributeID=" & hdn.Value
            ScriptManager.RegisterClientScriptBlock(Me, Me.GetType(), "newpage", "customOpen('" + url + "');", True)
            'Dim newWin As String = "window.open('" & queryString & "');"
            'ClientScript.RegisterStartupScript(Me.GetType(), "pop", newWin, True)

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
        
    End Sub
    Private Function getDataType(ByVal TypeID As Integer) As String
        Select Case TypeID
            Case 0
                getDataType = "Text"
            Case 1
                getDataType = "Numeric"
            Case 2
                getDataType = "Date"
            Case 3
                getDataType = "Boolean"
			Case 4
                getDataType = "Raw"
            Case 5
                getDataType = "Options"
        End Select
    End Function
    Private Function getCanDelete(ByVal ID As Integer) As Boolean
        Select Case ID
            Case 0
                getCanDelete = False
            Case 1
                getCanDelete = True
        End Select
    End Function
    Sub change(ByVal sender As Object, ByVal e As EventArgs)
        Dim txt As TextBox
        txt = CType(sender, TextBox)
        Dim btn As Button = txt.Parent.FindControl("Button1")
        btn.Enabled = True
    End Sub
    Sub changeCHK(ByVal sender As Object, ByVal e As EventArgs)
        Dim chk As CheckBox
        chk = CType(sender, CheckBox)
        Dim btn As Button = chk.Parent.FindControl("Button1")
        btn.Enabled = True
    End Sub
</script>

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Edit Attributes</title>
    <link href= "../App_Themes/Css/Main.css" type="text/css" rel="stylesheet" />
    <link href= "../App_Themes/Css/Styles.css" type="text/css" rel="stylesheet" />
</head>
<body>

    <form id="form1" runat="server">
    <div id="body">
    <div id="cap"></div>
    <div id="main">
    <h1>Edit Attributes</h1>
<ajaxToolkit:ToolkitScriptManager runat="server" ID="ScriptManager1"/>    
<asp:UpdatePanel runat="server" ID="up2">
        <ContentTemplate>            
            <asp:Panel ID="Panel1" runat="server" HorizontalAlign="Left">
                <asp:Repeater ID="rptCon" runat="server"  >
                         <HeaderTemplate>
                            <table>            
                            <tr>
                                <td colspan="5" class="HeaderDiv">
                                    Attributes Details
                                </td>
                            </tr>
                            <tr>
                                <td class="alt1">Attribute Name</td>            
                                <td class="alt1">Caption</td>
                                <td class="alt1">Type</td>            
                                <td class="alt1">Delete</td>
                                <td class="alt1">Action</td>
                            </tr>
                </HeaderTemplate>

                <ItemTemplate>
                <tr>
                            <td><asp:TextBox ID="txtAttribName" runat="server" Text='<%#Container.DataItem("Name")%>' ontextchanged="change" AutoPostBack="true"></asp:TextBox><asp:HiddenField runat=server ID="hdnAttribID" Value='<%#Container.DataItem("AttributeID")%>'/> </td>            
                            <td><asp:TextBox ID="txtCaption" runat="server" Text='<%#Container.DataItem("Caption")%>' ontextchanged="change" AutoPostBack="true"></asp:TextBox></td>
                            <td><asp:DropDownList ID="DDType" runat="server"  OnSelectedIndexChanged="DDType_SelectedIndexChanged" AutoPostBack="true" Visible="false">            
                            <asp:ListItem Selected="True" Value="">Please Select</asp:ListItem>
                            <asp:ListItem Value="0">String</asp:ListItem>
                            <asp:ListItem Value="1">Numeric</asp:ListItem>
                            <asp:ListItem Value="2">DateTime</asp:ListItem>
                            <asp:ListItem Value="3">Boolean</asp:ListItem>
                            <asp:ListItem Value="4">Raw</asp:ListItem>
                            <asp:ListItem Value="5">Options</asp:ListItem>
                            </asp:DropDownList>            
                            <asp:Label ID="lblType" runat="server" Width="150px" Text='<%#getDataType(Container.DataItem("Type"))%>'></asp:Label>        
                            <asp:HiddenField ID="hdnTypeNo" runat="server" Value='<%#Container.DataItem("Type") %>'/>          
                            <asp:Button ID="iPopUp" CssClass="button" runat="server" Text="..." OnClick="iPopUp_Click" Height="18" ToolTip="Click here to change Data Type" Enabled='<%#iif(Container.DataItem("IsDefault"),False,True)%>' />
                            <asp:Button ID="iPopUpOp" CssClass="button" runat="server" Text="..." OnClick="iPopUpOp_Click" Height="18" ToolTip="Click here to edit options" Visible='<%#iif(Container.DataItem("Type")=5,True,False)%>' />
                            </td>
                            <td>
                                <asp:CheckBox ID="chkDelete" runat="server" Enabled='<%#Container.DataItem("CanDelete")%>' OnCheckedChanged="changeCHK" AutoPostBack="true"/>
                            </td>            
                        <td>
                            <asp:Button ID="Button1" CssClass="button" runat="server" Text="Save Changes" OnClick="Button1_Click" Enabled="false"/>
                        </td>         
                </tr>
                </ItemTemplate>
                <AlternatingItemTemplate>
                <tr bgcolor="#cccccc">
                            <td><asp:TextBox ID="txtAttribName" runat="server" Text='<%#Container.DataItem("Name")%>' ontextchanged="change" AutoPostBack="true"></asp:TextBox><asp:HiddenField runat=server ID="hdnAttribID" Value='<%#Container.DataItem("AttributeID")%>' /> </td>            
                            <td><asp:TextBox ID="txtCaption" runat="server" Text='<%#Container.DataItem("Caption")%>' ontextchanged="change" AutoPostBack="true"></asp:TextBox></td>
                            <td><asp:DropDownList ID="DDType" runat="server"  OnSelectedIndexChanged="DDType_SelectedIndexChanged" AutoPostBack="true" Visible="false">            
                            <asp:ListItem Selected="True" Value="">Please Select</asp:ListItem>
                            <asp:ListItem Value="0">String</asp:ListItem>
                            <asp:ListItem Value="1">Numeric</asp:ListItem>
                            <asp:ListItem Value="2">DateTime</asp:ListItem>
                            <asp:ListItem Value="3">Boolean</asp:ListItem>
							<asp:ListItem Value="4">Raw</asp:ListItem>
							<asp:ListItem Value="5">Options</asp:ListItem>
                            </asp:DropDownList>            
                            <asp:Label ID="lblType" runat="server" Width="150px" Text='<%#getDataType(Container.DataItem("Type"))%>'></asp:Label>        
                            <asp:HiddenField ID="hdnTypeNo" runat="server" Value='<%#Container.DataItem("Type") %>'/>                    
                            <asp:Button ID="iPopUp" CssClass="button" runat="server" Text="..." OnClick="iPopUp_Click" Height="18" ToolTip="Click here to change Data Type" Enabled='<%#iif(Container.DataItem("IsDefault"),False,True)%>'/>
                            <asp:Button ID="iPopUpOp" CssClass="button" runat="server" Text="..." OnClick="iPopUpOp_Click" Height="18" ToolTip="Click here to edit options" Visible='<%#iif(Container.DataItem("Type")=5,True,False)%>' />
                            </td>
                            <td>
                                <asp:CheckBox ID="chkDelete" runat="server" Enabled='<%#Container.DataItem("CanDelete")%>' OnCheckedChanged="changeCHK" AutoPostBack="true"/>
                            </td>            
                        <td>
                            <asp:Button ID="Button1" runat="server" Text="Save Changes" CssClass="button" OnClick="Button1_Click" Enabled="false"/>
                        </td>                    
                </tr>
                </AlternatingItemTemplate>
                <FooterTemplate>
                </table>
                </FooterTemplate>
                </asp:Repeater>
            </asp:Panel>
         
</ContentTemplate>        
</asp:UpdatePanel> 
</div> 
</div> 
</form>

</body>
</html>


