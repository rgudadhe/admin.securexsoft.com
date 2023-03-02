<%@ Page Language="VB"  Inherits ="BasePage"%>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 1.0 Transitional/EN">
<script runat="server" type="text/VB">

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs)
        If Not IsPostBack Then
            BindIT()
        End If
    End Sub
    Private Function setVal(ByVal dt As String) As Boolean
        If String.IsNullOrEmpty(dt) Then
            setVal = False
        Else
            setVal = True
        End If
    End Function
    Private Function BindIT()
        Try
            Dim clsPro As New ETS.BL.ProductionLevels()
                
            With clsPro
                .ContractorID = Session("ContractorID").ToString
                .Type = True 'ddType.SelectedValue
            End With
            Dim DT As New Data.DataTable
            DT = clsPro.GetProdunctionLevelsByContactorAndTypeBySequence().Tables(0)
            
            rptCon.DataSource = DT
            rptCon.DataBind()
            clsPro = Nothing
            
        Catch ex As Exception
            Response.Write(ex.StackTrace)
        End Try
    End Function
    
    Protected Sub Button2_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim btn As Button
        Dim txt As TextBox
        Dim chk As CheckBox
        btn = CType(sender, Button)
        btn.Visible = False
        txt = btn.Parent.FindControl("txtName")
        txt.Enabled = False
        txt = btn.Parent.FindControl("txtDesc")
        txt.Enabled = False
        txt = btn.Parent.FindControl("txtSeq")
        txt.Enabled = False
        chk = btn.Parent.FindControl("chkDelete")
        chk.Enabled = False
        chk = btn.Parent.FindControl("chkErrMarking")
        chk.Enabled = False
        chk = btn.Parent.FindControl("chkFR")
        chk.Enabled = False
        chk = btn.Parent.FindControl("chkAudit")
        chk.Enabled = False
        btn = btn.Parent.FindControl("Button1")
        btn.Text = "Edit"
        BindIT()
    End Sub
    Protected Sub Button1_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim btn As Button
        Dim txt As TextBox
        Dim chk As CheckBox
        Dim hdn As HiddenField
        Dim isDeleted As Integer
        Dim ErrMarking As Integer
        Dim ForceRouting As Integer
        Dim JobsRouting As Integer
        Dim Auditing As Integer
        Dim strLvlNo, strLVLName, strDescription, strSequence As String
        
        'Try
        btn = CType(sender, Button)
        If btn.Text = "Save" Then
            
            btn.Text = "Edit"
            txt = btn.Parent.FindControl("txtName")
            strLVLName = txt.Text
            txt = btn.Parent.FindControl("txtDesc")
            strDescription = txt.Text
            chk = btn.Parent.FindControl("chkDelete")
            If chk.Checked Then
                isDeleted = 1
            Else
                isDeleted = 0
            End If
            chk = btn.Parent.FindControl("chkErrMarking")
            If chk.Checked Then
                ErrMarking = 1
            Else
                ErrMarking = 0
            End If
            chk = btn.Parent.FindControl("chkFR")
            If chk.Checked Then
                ForceRouting = 1
            Else
                ForceRouting = 0
            End If
            chk = btn.Parent.FindControl("chkJR")
            If chk.Checked Then
                JobsRouting = 1
            Else
                JobsRouting = 0
            End If
            chk = btn.Parent.FindControl("chkAudit")
            If chk.Checked Then
                Auditing = 1
            Else
                Auditing = 0
            End If
            hdn = btn.Parent.FindControl("LevelNo")
            strLvlNo = hdn.Value
            txt = btn.Parent.FindControl("txtSeq")
            strSequence = IIf(String.IsNullOrEmpty(txt.Text), 0, IIf(IsNumeric(txt.Text), txt.Text, 0))
            If strLvlNo <> "" Then
                Dim clsPL As New ETS.BL.ProductionLevels
                Dim SeqUpdate As Integer
                SeqUpdate = clsPL.getMaxLevel(Session("ContractorID").ToString)
                               
                With clsPL
                    .ContractorID = Session("ContractorID").ToString
                    .LevelNo = strLvlNo
                    .LevelName = strLVLName
                    .Description = strDescription
                    .IsDeleted = isDeleted
                    .ErrMarking = ErrMarking
                    .ForcedRouting = ForceRouting
                    .JobsRouting = JobsRouting
                    .Auditing = Auditing
                    .Sequence = strSequence
                    If ForceRouting Then
                        .CheckInOptions = 1073741824
                        .IndirectOptions = 0
                    End If
                End With
                Dim retval As String = clsPL.ProductionLevels_Save(SeqUpdate)
                if IsBool(retval) then
                    If CBool(retval) Then
                        BindIT()
                    End If
                Else
                    Response.Write(retval)
                End If
            End If
        ElseIf btn.Text = "Edit" Then
            btn.Attributes.Add("tooltiptitle", "Edit Level Details")
            tooltip1.AddTooltipControl(btn)
            
            hdn = btn.Parent.FindControl("LevelNo")
            txt = btn.Parent.FindControl("txtName")
            txt.Enabled = True
            txt = btn.Parent.FindControl("txtDesc")
            txt.Enabled = True
            txt = btn.Parent.FindControl("txtSeq")
            txt.Enabled = True  'IIf(hdn.Value = "1073741824", False, IIf(hdn.Value = "5", False, IIf(hdn.Value = "3", False, True)))
            chk = btn.Parent.FindControl("chkDelete")
            chk.Enabled = IIf(hdn.Value = "1073741824", False, IIf(hdn.Value = "5", False, IIf(hdn.Value = "3", False, True)))
            chk = btn.Parent.FindControl("chkErrMarking")
            chk.Enabled = IIf(hdn.Value = "1073741824", False, IIf(hdn.Value = "5", False, IIf(hdn.Value = "3", False, True)))
            chk = btn.Parent.FindControl("chkAudit")
            chk.Enabled = IIf(hdn.Value = "1073741824", False, IIf(hdn.Value = "5", False, IIf(hdn.Value = "3", False, True)))
            chk = btn.Parent.FindControl("chkFR")
            chk.Attributes.Add("tooltiptitle", "Force Routing")
            tooltip1.AddTooltipControl(chk)
            chk.Enabled = IIf(hdn.Value = "1073741824", False, IIf(hdn.Value = "5", False, IIf(hdn.Value = "3", False, True)))
            chk = btn.Parent.FindControl("chkJR")
            chk.Attributes.Add("tooltiptitle", "Jobs Routing")
            tooltip1.AddTooltipControl(chk)
            chk.Enabled = IIf(hdn.Value = "1073741824", False, IIf(hdn.Value = "5", False, IIf(hdn.Value = "3", False, True)))
            
            btn.Text = "Save"
            btn = btn.Parent.FindControl("Button2")
            btn.Visible = True
            btn.Attributes.Add("tooltiptitle", "Cancel Changes")
            tooltip1.AddTooltipControl(btn)
            
            btn = btn.Parent.FindControl("CheckInOptions")
            btn.Visible = False
                        
        End If
        
    End Sub
    Public Function IsBool(ByVal data As String) As Boolean
        Dim result As Boolean = True
        Try
            Boolean.Parse(data)
        Catch ex As Exception
            Return False
        End Try
        Return True
    End Function
    
    Protected Sub CheckInOptions_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim strLvlNo As String
        Dim hdn As HiddenField
        Dim btn As Button
        btn = CType(sender, Button)
        hdn = btn.Parent.FindControl("LevelNo")
        strLvlNo = hdn.Value
        If Not hdn.Value = "1073741824" Then
            Response.Redirect("CheckInOptions.aspx?LevelNo=" & strLvlNo, True)
        Else
            btn.Enabled = False
        End If
    End Sub
    
    
    Protected Sub ddType_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        BindIT()
    End Sub

    Protected Sub rptCon_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.RepeaterItemEventArgs)
        If e.Item.ItemType <> ListItemType.Item AndAlso e.Item.ItemType <> ListItemType.AlternatingItem Then
            Return
        End If
    
        Dim btn As Button = DirectCast(e.Item.FindControl("CheckInOptions"), Button)
        btn.Attributes.Add("tooltiptitle", "CheckIn Options")
        tooltip1.AddTooltipControl(btn)
        btn = DirectCast(e.Item.FindControl("Button1"), Button)
        btn.Attributes.Add("tooltiptitle", "Edit Level Details")
        tooltip1.AddTooltipControl(btn)
        btn = DirectCast(e.Item.FindControl("Button2"), Button)
        btn.Attributes.Add("tooltiptitle", "Cancel Changes")
        tooltip1.AddTooltipControl(btn)
        Dim chk As CheckBox = DirectCast(e.Item.FindControl("chkFR"), CheckBox)
        chk.Attributes.Add("tooltiptitle", "Force Routing")
        tooltip1.AddTooltipControl(chk)
        Dim chk1 As CheckBox = DirectCast(e.Item.FindControl("chkJR"), CheckBox)
        chk1.Attributes.Add("tooltiptitle", "Jobs Routing")
        tooltip1.AddTooltipControl(chk1)
    End Sub
</script>

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <link href= "../App_Themes/Css/styles.css" type="text/css" rel="stylesheet" />
    <link href= "../App_Themes/Css/Common.css" type="text/css" rel="stylesheet" />
    <title>Edit Production Levels</title>
</head>
<body>
    <form id="form1" runat="server"> 
        <div id="body">
        <div id="cap"></div>
        <div id="main">
        <h1>Edit User Role</h1>
        <asp:Panel ID="Panel1" runat="server" HorizontalAlign="Left">
            <ajaxToolkit:ToolkitScriptManager runat="Server" ID="ScriptManager1" />
     <dn:HoverTooltip runat="server" ID="tooltip1" SkinID="GoupBoxTooltip">     
        </dn:HoverTooltip>
       <%-- <asp:DropDownList ID="ddType" runat="server" OnSelectedIndexChanged="ddType_SelectedIndexChanged" CssClass="common" AutoPostBack="True">
            <asp:ListItem Selected="True" Value="1">Contractors Levels</asp:ListItem>
            <asp:ListItem Value="0">Sub-Contractors Levels</asp:ListItem>
        </asp:DropDownList>--%>
        <%--<asp:UpdatePanel ID="up1" runat="server">
            <ContentTemplate>--%>
            
            <asp:Repeater ID="rptCon" runat="server" OnItemDataBound="rptCon_ItemDataBound">
         <HeaderTemplate>
<table>
            <tr>
            <td class="alt1">LevelName</td>            
            <td class="alt1">Description</td>            
            <td class="alt1">Del</td>
            <td class="alt1">Err.Marking</td>
            <td class="alt1">JobsRouting</td>
            <td class="alt1">ForceRouting</td>                        
            <td class="alt1">Auditing</td>
            <td class="alt1">Seq</td>
            <td class="alt1">Action</td>
            
            </tr>
</HeaderTemplate>

<ItemTemplate>


<tr>
            <td align="center" class="common"><asp:TextBox Enabled="false" ID="txtName" runat="server" Text='<%#Container.DataItem("LevelName")%>' Width="100PX"></asp:TextBox><asp:HiddenField runat="server" ID="LevelNo" Value='<%#Container.DataItem("LevelNo")%>'/> </td>
            <td align="center" class="common"><asp:TextBox Enabled="false" ID="txtDesc" runat="server" Text='<%#Container.DataItem("Description")%>'></asp:TextBox></td>            
            <td align="center" class="common"><asp:CheckBox ID="chkDelete" runat="server" Checked='<%#Container.DataItem("isdeleted")%>' Enabled="false"/></td> <%--<%#iif(Container.DataItem("LevelNo")="1073741824",False,True)%>'--%>
            <td align="center" class="common"><asp:CheckBox Enabled="false" ID="chkErrMarking" runat="server" Checked='<%#Container.DataItem("ErrMarking")%>' /></td>
            <td align="center" class="common"><asp:CheckBox Enabled="false" ID="chkJR" runat="server" Checked='<%#iif(isdbnull(Container.DataItem("JobsRouting")),0,Container.DataItem("JobsRouting"))%>' ToolTip="Only JobsRouting Levels will be available in Routing Tool "/></td>
            <td align="center" class="common"><asp:CheckBox Enabled="false" ID="chkFR" runat="server" Checked='<%#Container.DataItem("ForcedRouting")%>' ToolTip="Once the Level set for Force Routing, all the CheckIn options assigned will be removed and only have Finish as Default option"/></td>
            <td align="center" class="common"><asp:CheckBox Enabled="false" ID="chkAudit" runat="server" Checked='<%#Container.DataItem("Auditing")%>'/></td>
            <td align="center" class="common"><asp:TextBox Enabled="false" ID="txtSeq" runat="server" Text='<%#Container.DataItem("Sequence")%>' MaxLength="3" Width="20"></asp:TextBox></td>
            <td align="center" class="common"><asp:Button ID="Button1" CssClass="button" runat="server" Text="Edit" OnClick="Button1_Click" CommandName="Confirmation" CausesValidation="false" OnClientClick="return confirm('Are you certain you want to update information?');"  ToolTip="Click here to save the changes"  /> 
            <asp:Button ID="Button2" CssClass="button" runat="server" Text="Cancel" OnClick="Button2_Click" CommandName="Confirmation" CausesValidation="false" Visible="false" ToolTip="Click here to cancel changes"/> 
            <asp:Button ID="CheckInOptions" CssClass="button" runat="server" Text="*" OnClick="CheckInOptions_Click" ToolTip="Click here to view checkIn options for the level" />
            </td>            
</tr>

</ItemTemplate>
<AlternatingItemTemplate>
<tr>
            <td align="center" class="common"><asp:TextBox Enabled="false" ID="txtName" runat="server" Text='<%#Container.DataItem("LevelName")%>' Width="100PX"></asp:TextBox><asp:HiddenField runat="server" ID="LevelNo" Value='<%#Container.DataItem("LevelNo")%>'/> </td>
            <td align="center" class="common"><asp:TextBox Enabled="false" ID="txtDesc" runat="server" Text='<%#Container.DataItem("Description")%>'></asp:TextBox></td>            
            <td align="center" class="common"><asp:CheckBox ID="chkDelete" runat="server" Checked='<%#Container.DataItem("isdeleted")%>' Enabled="false"/></td> <%--<%#iif(Container.DataItem("LevelNo")="1073741824",False,True)%>'--%>
            <td align="center" class="common"><asp:CheckBox Enabled="false" ID="chkErrMarking" runat="server" Checked='<%#Container.DataItem("ErrMarking")%>' /></td>
            <td align="center" class="common"><asp:CheckBox Enabled="false" ID="chkJR" runat="server" Checked='<%#iif(isdbnull(Container.DataItem("JobsRouting")),0,Container.DataItem("JobsRouting"))%>' ToolTip="Only JobsRouting Levels will be available in Routing Tool "/></td>
            <td align="center" class="common"><asp:CheckBox Enabled="false" ID="chkFR" runat="server" Checked='<%#Container.DataItem("ForcedRouting")%>' ToolTip="Once the Level set for Force Routing, all the CheckIn options assigned will be removed and only have Finish as Default option"/></td>
            <td align="center" class="common"><asp:CheckBox Enabled="false" ID="chkAudit" runat="server" Checked='<%#Container.DataItem("Auditing")%>'/></td>
            <td align="center" class="common"><asp:TextBox Enabled="false" ID="txtSeq" runat="server" Text='<%#Container.DataItem("Sequence")%>' MaxLength="3" Width="20"></asp:TextBox></td>            
            <td align="center" class="common"><asp:Button ID="Button1" CssClass="button" runat="server" Text="Edit" OnClick="Button1_Click" CommandName="Confirmation" CausesValidation="false" OnClientClick="return confirm('Are you certain you want to update information?');"  ToolTip="Click here to save the changes"  /> 
            <asp:Button ID="Button2" CssClass="button" runat="server" Text="Cancel" OnClick="Button2_Click" CommandName="Confirmation" CausesValidation="false" Visible="false" ToolTip="Click here to cancel changes"/> 
            <asp:Button ID="CheckInOptions" CssClass="button" runat="server" Text="*" OnClick="CheckInOptions_Click" ToolTip="Click here to view checkIn options for the level" />
            </td> 
</tr>
</AlternatingItemTemplate>
<FooterTemplate>
</table>
</FooterTemplate>

</asp:Repeater>
        </asp:Panel>   
     </div> 
     </div>  
<%--<ontentTemplate>
        </asp:UpdatePanel>   --%>   
</form>    
    
</body>
</html>


