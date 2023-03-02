<%@ Page Language="VB"  Inherits ="BasePage"%>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 1.0 Transitional/EN">
<script runat="server" type="text/VB">

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs)
        
        If Not IsPostBack Then
            Dim strConn As String = System.Configuration.ConfigurationManager.AppSettings("ETSCon")
            Dim oConn As New Data.SqlClient.SqlConnection(strConn)
            Try
                oConn.Open()
                Dim SQLCmd As New System.Data.SqlClient.SqlCommand("Select * from DBO.tblcontractor", oConn)

                Dim DRRec As System.Data.SqlClient.SqlDataReader = SQLCmd.ExecuteReader()
                If DRRec.HasRows = True Then
                    While DRRec.Read
                        Dim LI As New ListItem
                        LI.Text = DRRec("ContractorName").ToString
                        LI.Value = DRRec("ContractorID").ToString
                        DLContractor.Items.Add(LI)
                    End While
                End If
                DRRec.Close()
                If DLContractor.Items.Count > 0 Then
                    DLContractor.Items(0).Selected = True
                End If

            Catch ex As Exception
                Response.Write(ex.Message)
            Finally
                If oConn.State <> Data.ConnectionState.Open Then
                    oConn.Close()
                    oConn = Nothing
                End If
            End Try
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
        Dim ConString As String
        Dim SQLString As String
        ConString = System.Configuration.ConfigurationManager.AppSettings("ETSCon")
        Dim oConn As New Data.SqlClient.SqlConnection
        Try
            oConn.ConnectionString = ConString
            oConn.Open()
                
            SQLString = "select * from tblProductionLevels where Type=" & ddType.SelectedValue & " and ContractorID='" & DLContractor.SelectedValue & "' order by Sequence" 'IsDeleted =0 and
            Dim oCommand As New Data.SqlClient.SqlCommand(SQLString, oConn)
            Dim oRec As Data.SqlClient.SqlDataReader = oCommand.ExecuteReader()
            rptCon.DataSource = oRec
            rptCon.DataBind()
        Catch ex As Exception
            Response.Write(ex.Message)
        Finally
            If oConn.State <> Data.ConnectionState.Closed Then
                oConn.Close()
                oConn = Nothing
            End If
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
                Dim ConString As String
                Dim SQLString As String
                Dim recAffected As Integer
                ConString = System.Configuration.ConfigurationManager.AppSettings("ETSCon")
                Dim oConn As New Data.SqlClient.SqlConnection
                oConn.ConnectionString = ConString
                Try
                    oConn.Open()
                    SQLString = "update tblProductionLevels set LevelName='" & strLVLName & "', Description='" & strDescription & "',IsDeleted=" & isDeleted & ",ErrMarking=" & ErrMarking & _
                                ", ForcedRouting=" & ForceRouting & ", JobsRouting=" & JobsRouting & ", Auditing=" & Auditing & ", Sequence=" & strSequence
                    Dim WhereClause As String = " where LevelNo='" & strLvlNo & "'  and ContractorID='" & DLContractor.SelectedValue & "' "
                    If ForceRouting = 1 Then
                        SQLString = SQLString & ", CheckInOptions=1073741824, IndirectOptions=null"
                    End If
                    SQLString = SQLString & WhereClause
                    Dim oCommand As New Data.SqlClient.SqlCommand(SQLString, oConn)
                    recAffected = oCommand.ExecuteNonQuery()
                
                    SQLString = "UPDATE tblProductionLevels set Sequence=(SELECT max(Sequence)+1 as TopSeq " & _
                                "FROM tblProductionLevels where LevelNo<>1073741824 and ContractorID='" & DLContractor.SelectedValue & "') where LevelNo=1073741824 and ContractorID='" & DLContractor.SelectedValue & "'"
                    oCommand = New Data.SqlClient.SqlCommand(SQLString, oConn)
                    oCommand.ExecuteNonQuery()
                
                    If recAffected > 0 Then
                        BindIT()
                        'Response.Redirect("EditProductionLevels.aspx", True)
                    End If
                Catch ex As Exception
                    Response.Write(ex.Message)
                Finally
                    If oConn.State <> Data.ConnectionState.Closed Then
                        oConn.Close()
                        oConn = Nothing
                    End If
                End Try
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
            'btn.Attributes.Add("tooltiptitle", "CheckIn Options")
            'tooltip1.AddTooltipControl(btn)
            
        End If
        'Catch ex As Exception
        '    Response.Write(ex.Message)
        'End Try
    End Sub
    
    Protected Sub CheckInOptions_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim strLvlNo As String
        Dim hdn As HiddenField
        Dim btn As Button
        btn = CType(sender, Button)
        hdn = btn.Parent.FindControl("LevelNo")
        strLvlNo = hdn.Value
        If Not hdn.Value = "1073741824" Then
            Response.Redirect("CheckInOptions.aspx?LevelNo=" & strLvlNo & "&contractorID=" & DLContractor.SelectedValue, True)
        Else
            btn.Enabled = False
        End If
    End Sub
    
    Protected Sub DLContractor_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        BindIT()
    End Sub
    
    Protected Sub ddType_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        
        Dim ConString As String
        Dim SQLString As String
        ConString = System.Configuration.ConfigurationManager.AppSettings("ETSCon")
        Dim oConn As New Data.SqlClient.SqlConnection
        Try
            oConn.ConnectionString = ConString
            oConn.Open()
            SQLString = "select * from tblProductionLevels where IsDeleted =0 and Type=" & ddType.SelectedValue & " and ContractorID='" & DLContractor.SelectedValue & "' order by Sequence"
            Dim oCommand As New Data.SqlClient.SqlCommand(SQLString, oConn)
            Dim oRec As Data.SqlClient.SqlDataReader = oCommand.ExecuteReader()
            rptCon.DataSource = oRec
            rptCon.DataBind()
            oConn.Close()
        Catch ex As Exception
            Response.Write(ex.Message)
        Finally
            If oConn.State <> Data.ConnectionState.Closed Then
                oConn.Close()
                oConn = Nothing
            End If
        End Try
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
    <LINK href= "../styles/Main.css" type="text/css" rel="stylesheet">
    <title>Untitled Page</title>
</head>
<body>
    <form id="form1" runat="server">    
     <ajaxToolkit:ToolkitScriptManager runat="Server" ID="ScriptManager1" />
     <dn:HoverTooltip runat="server" ID="tooltip1" SkinID="GoupBoxTooltip">     
        </dn:HoverTooltip>
         <table   border="2" cellpadding="2" width="100%">
               <tr>
                <td colspan="4" class="HeaderDiv" style="text-align: center">
                                         View/Edit User Roles</td>
            </tr>
            </table> 
        <table>
        <tr>
        <td class="SMSelected">Contractor</td>
        <td><asp:DropDownList ID="DLContractor" runat="server" OnSelectedIndexChanged="DLContractor_SelectedIndexChanged" AutoPostBack="True">
            
        </asp:DropDownList></td></tr>
        <tr>
        <td  class="SMSelected">Type</td>
        <td><asp:DropDownList ID="ddType" runat="server" OnSelectedIndexChanged="ddType_SelectedIndexChanged" AutoPostBack="True">
            <asp:ListItem Selected="True" Value="1">Contractors</asp:ListItem>
            <asp:ListItem Value="0">Sub-Contractors</asp:ListItem>
        </asp:DropDownList></td></tr></table>
        
        <%--<asp:UpdatePanel ID="up1" runat="server">
            <ContentTemplate>--%>
            
            <asp:Repeater ID="rptCon" runat="server" OnItemDataBound="rptCon_ItemDataBound">
         <HeaderTemplate>
<table border="1">
            <TR>
            <TH class="SearchCol">LevelName</TH>            
            <TH class="SearchCol">Description</th>            
            <TH class="SearchCol">Del</th>
            <TH class="SearchCol">Err.Marking</th>
            <TH class="SearchCol">JobsRouting</th>
            <TH class="SearchCol">ForceRouting</th>                        
            <TH class="SearchCol">Auditing</th>
            <TH class="SearchCol">Seq</th>
            <TH class="SearchCol">Action</th>
            
            </TR>
</HeaderTemplate>

<ItemTemplate>


<tr>
            <td align="center"><asp:TextBox Enabled="false" ID="txtName" runat="server" Text='<%#Container.DataItem("LevelName")%>' Width="100PX"></asp:TextBox><asp:HiddenField runat=server ID="LevelNo" Value='<%#Container.DataItem("LevelNo")%>'/> </td>
            <td align="center"><asp:TextBox Enabled="false" ID="txtDesc" runat="server" Text='<%#Container.DataItem("Description")%>'></asp:TextBox></td>            
            <td align="center"><asp:CheckBox ID="chkDelete" runat="server" Checked='<%#Container.DataItem("isdeleted")%>' Enabled="false"/></td> <%--<%#iif(Container.DataItem("LevelNo")="1073741824",False,True)%>'--%>
            <td align="center"><asp:CheckBox Enabled="false" ID="chkErrMarking" runat="server" Checked='<%#Container.DataItem("ErrMarking")%>' /></td>
            <td align="center"><asp:CheckBox Enabled="false" ID="chkJR" runat="server" Checked='<%#iif(isdbnull(Container.DataItem("JobsRouting")),0,Container.DataItem("JobsRouting"))%>' ToolTip="Only JobsRouting Levels will be available in Routing Tool "/></td>
            <td align="center"><asp:CheckBox Enabled="false" ID="chkFR" runat="server" Checked='<%#Container.DataItem("ForcedRouting")%>' ToolTip="Once the Level set for Force Routing, all the CheckIn options assigned will be removed and only have Finish as Default option"/></td>
            <td align="center"><asp:CheckBox Enabled="false" ID="chkAudit" runat="server" Checked='<%#Container.DataItem("Auditing")%>'/></td>
            <td align="center"><asp:TextBox Enabled="false" ID="txtSeq" runat="server" Text='<%#Container.DataItem("Sequence")%>' MaxLength="3" Width="20"></asp:TextBox></td>
            <td align="center"><asp:Button ID="Button1" runat="server" Text="Edit" OnClick="Button1_Click" CommandName="Confirmation" CausesValidation="false" OnClientClick="return confirm('Are you certain you want to update information?');"  ToolTip="Click here to save the changes"  /> 
            <asp:Button ID="Button2" runat="server" Text="Cancel" OnClick="Button2_Click" CommandName="Confirmation" CausesValidation="false" Visible="false" ToolTip="Click here to cancel changes"/> 
            <asp:Button ID="CheckInOptions" runat="server" Text="*" OnClick="CheckInOptions_Click" ToolTip="Click here to view checkIn options for the level" />
            </td>            
</tr>

</ItemTemplate>
<AlternatingItemTemplate>
<tr bgcolor="#cccccc">
            <td align="center"><asp:TextBox Enabled="false" ID="txtName" runat="server" Text='<%#Container.DataItem("LevelName")%>' Width="100PX"></asp:TextBox><asp:HiddenField runat=server ID="LevelNo" Value='<%#Container.DataItem("LevelNo")%>'/> </td>
            <td align="center"><asp:TextBox Enabled="false" ID="txtDesc" runat="server" Text='<%#Container.DataItem("Description")%>'></asp:TextBox></td>            
            <td align="center"><asp:CheckBox ID="chkDelete" runat="server" Checked='<%#Container.DataItem("isdeleted")%>' Enabled="false"/></td> <%--<%#iif(Container.DataItem("LevelNo")="1073741824",False,True)%>'--%>
            <td align="center"><asp:CheckBox Enabled="false" ID="chkErrMarking" runat="server" Checked='<%#Container.DataItem("ErrMarking")%>' /></td>
            <td align="center"><asp:CheckBox Enabled="false" ID="chkJR" runat="server" Checked='<%#iif(isdbnull(Container.DataItem("JobsRouting")),0,Container.DataItem("JobsRouting"))%>' ToolTip="Only JobsRouting Levels will be available in Routing Tool "/></td>
            <td align="center"><asp:CheckBox Enabled="false" ID="chkFR" runat="server" Checked='<%#Container.DataItem("ForcedRouting")%>' ToolTip="Once the Level set for Force Routing, all the CheckIn options assigned will be removed and only have Finish as Default option"/></td>
            <td align="center"><asp:CheckBox Enabled="false" ID="chkAudit" runat="server" Checked='<%#Container.DataItem("Auditing")%>'/></td>
            <td align="center"><asp:TextBox Enabled="false" ID="txtSeq" runat="server" Text='<%#Container.DataItem("Sequence")%>' MaxLength="3" Width="20"></asp:TextBox></td>            
            <td align="center"><asp:Button ID="Button1" runat="server" Text="Edit" OnClick="Button1_Click" CommandName="Confirmation" CausesValidation="false" OnClientClick="return confirm('Are you certain you want to update information?');"  ToolTip="Click here to save the changes"  /> 
            <asp:Button ID="Button2" runat="server" Text="Cancel" OnClick="Button2_Click" CommandName="Confirmation" CausesValidation="false" Visible="false" ToolTip="Click here to cancel changes"/> 
            <asp:Button ID="CheckInOptions" runat="server" Text="*" OnClick="CheckInOptions_Click" ToolTip="Click here to view checkIn options for the level" />
            </td> 
</tr>
</AlternatingItemTemplate>
<FooterTemplate>
</table>
</FooterTemplate>

</asp:Repeater>
<%--</ContentTemplate>
        </asp:UpdatePanel>   --%>   
</form>    
    
</body>
</html>


