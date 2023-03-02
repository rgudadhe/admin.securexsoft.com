<%@ Page Language="VB" Inherits="BasePage"%>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 1.0 Transitional/EN">
<script runat="server" type="text/VB">        
    Public strLevelName As String
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs)
        If Not IsPostBack Then
            bindIT()
        Else
            If Request.Form("hdnConfirm") = "1" Then
                Request.Form("hdnConfirm").Replace("1", "0")
                DeleteProcess()
            End If
        End If
    End Sub
    Private Function bindIT()
        Dim clsRSS As New ETS.BL.RSS
        clsRSS.ContractorID = Session("ContractorID").ToString
        Dim DS As New Data.DataSet
        DS = clsRSS.getRSSList
        rptCon.DataSource = DS.Tables(0)
        rptCon.DataBind()
        clsRSS = Nothing
        DS.Dispose()
    End Function
    
    Protected Sub Button1_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim btn As ExImageButton
        Dim txt As TextBox
        Dim chk As CheckBox
        Dim hdn As HiddenField
        Dim goAhead As Boolean
        Dim SettingID, SettingName, FolderPath As String
        Dim strMessage As String
        Try
            btn = CType(sender, ExImageButton)
            hdn = btn.Parent.FindControl("SettingID")
            SettingID = hdn.Value
            txt = btn.Parent.FindControl("SettingName")
            If Not String.IsNullOrEmpty(txt.Text) Then
                SettingName = txt.Text
                goAhead = True
            Else
                strMessage = "Process Name can not be blank"
                goAhead = False
            End If
            txt = btn.Parent.FindControl("FolderPath")
            If Not String.IsNullOrEmpty(txt.Text) Then
                FolderPath = txt.Text
                
                Dim iFileInfo As New System.IO.DirectoryInfo(Server.MapPath("\InBound\" & FolderPath))
                If Not IO.Directory.Exists(iFileInfo.FullName) Then
                    IO.Directory.CreateDirectory(iFileInfo.FullName)
                    
                    If IO.Directory.Exists(iFileInfo.FullName) = True Then
                        MsgBox1.alert("Process folder '" & FolderPath & "' has been created under Inbound path")
                        goAhead = True
                    Else
                        strMessage = "Please specify correct folder name!"
                        goAhead = False
                    End If
                Else
                    goAhead = True
                End If
            Else
                strMessage = "Inbound folder Path can not be blank"
                goAhead = False
            End If
            If goAhead Then
                Dim RetVal As Integer
                Dim clsRSS As New ETS.BL.RSS
                With clsRSS
                    '.SettingID = SettingID
                    '.DeleteRSSDetails()
                    .FolderPath = FolderPath
                                      
                    .SettingID = SettingID
                    .SettingName = SettingName
                                   
                    
                End With
                
                RetVal = clsRSS.UpdateRSSDetails
                
                If RetVal = 1 Then
                    strMessage = "Import Process " & SettingName & " updated successfully"
                    MsgBox1.alert(strMessage)
                Else
                    strMessage = "Updating Import Process " & SettingName & " failed"
                    MsgBox1.alert(strMessage)
                End If
                
                clsRSS = Nothing
            Else
flg:
                MsgBox1.alert(strMessage)
            End If
            bindIT()
        Catch ex As Exception
            MsgBox1.alert(ex.Message)
        End Try
    End Sub
    Protected Sub btnAdd_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim btn As ExImageButton
        Dim txt As TextBox
        Dim strMessage As String
       
        Dim SettingName, FolderPath, SettingID As String
        Dim goAhead As Boolean
        SettingName = String.Empty
        FolderPath = String.Empty
        SettingID = String.Empty
        Try
            btn = CType(sender, ExImageButton)
            txt = btn.Parent.FindControl("SettingName")
            If Not String.IsNullOrEmpty(txt.Text) Then
                SettingName = txt.Text
                goAhead = True
            Else
                strMessage = "Import Process Name can not be blank"
                goAhead = False
            End If
            'hdn = btn.Parent.FindControl("SettingID")
            'SettingID = hdn.Value
            txt = btn.Parent.FindControl("FolderPath")
            If Not String.IsNullOrEmpty(txt.Text) Then
                FolderPath = txt.Text
                Dim iFileInfo As New System.IO.DirectoryInfo(Server.MapPath("\InBound\" & FolderPath))
            
                If Not IO.Directory.Exists(iFileInfo.FullName) Then
                    IO.Directory.CreateDirectory(iFileInfo.FullName)
                    If IO.Directory.Exists(iFileInfo.FullName) Then
                        goAhead = True
                    Else
                        strMessage = "Please specify correct folder name"
                        goAhead = False
                    End If
                Else
                    goAhead = True
                End If
            Else
                strMessage = "Inbound Folder Path can not be blank"
                goAhead = False
            End If
            If goAhead Then
                Dim RetVal As Integer
                Dim clsRSS As New ETS.BL.RSS
                With clsRSS
                    
                    .FolderPath = FolderPath
                    Dim DSrsCount As Data.DataSet = .getRSSList
                    'MsgBox1.alert(DSrsCount.Tables.Count)
                    If DSrsCount.Tables(0).Rows.Count = 0 Then
                        .ContractorID = Session("ContractorID").ToString
                        .SettingName = SettingName
                        RetVal = clsRSS.InsertRSSDetails
                    Else
                        strMessage = "Please mention different folder path!"
                        GoTo flg
                    End If
                End With
                
                'Dim RetVal As Integer = clsRSS.InsertRSSDetails
                If RetVal = 1 Then
                    strMessage = "Import Process " & SettingName & " added successfully"
                    MsgBox1.alert(strMessage)
                Else
                    strMessage = "Adding Import Process " & SettingName & " failed"
                    MsgBox1.alert(strMessage)
                End If
                clsRSS = Nothing
            Else
flg:
                MsgBox1.alert(strMessage)
            End If
            bindIT()
        Catch ex As Exception
            MsgBox1.alert(ex.Message)
        End Try
        
    End Sub
    Protected Sub btnTest_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Try
            Dim btn As ExImageButton
            Dim txt As TextBox
            Dim hdn As HiddenField
            Dim SettingName As String = String.Empty
            Dim SettingID As String
            btn = CType(sender, ExImageButton)
            txt = btn.Parent.FindControl("SettingName")
            If Not String.IsNullOrEmpty(txt.Text) Then
                SettingName = txt.Text
            End If
            hdn = btn.Parent.FindControl("SettingID")
            If Not String.IsNullOrEmpty(hdn.Value) Then
                SettingID = hdn.Value
                Response.Redirect("TestRSSSettings.aspx?SettingID=" & SettingID & "&SettingName=" & SettingName, True)
            End If
        Catch ex As Exception
            MsgBox1.alert(ex.Message)
        End Try
    End Sub
    Private Function DeleteProcess()
        Dim strMessage As String
        Dim SettingID As String
        Dim SettingName As String
        Dim hdnPro As HiddenField
        SettingID = ViewState("SettingID").ToString
        SettingName = ViewState("SettingName").ToString
        
        Dim clsRSS As New ETS.BL.RSS
        With clsRSS
            .SettingID = SettingID
        End With
                
        Dim RetVal As Integer = clsRSS.DeleteRSSDetails
        If RetVal = 1 Then
            strMessage = "Import Process " & SettingName & " deleted successfully"
            MsgBox1.alert(strMessage)
            bindIT()
        Else
            strMessage = "Deleting Import Process " & SettingName & " failed"
            MsgBox1.alert(strMessage)
        End If
        
    End Function
    Protected Sub btnDelete_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim SettingName As String
        Dim btn As ExImageButton = CType(sender, ExImageButton)
        Dim txt As TextBox = btn.Parent.FindControl("SettingName")
        SettingName = txt.Text
        ViewState("SettingName") = SettingName
        Dim hdn As HiddenField = btn.Parent.FindControl("SettingID")
        ViewState("SettingID") = hdn.Value
        MsgBox1.confirm("Are you sure to delete Process " & SettingName & "?", "hdnConfirm")
    End Sub
    Protected Sub btnEdit_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim btn As ExImageButton
        Dim txt As TextBox
        Dim hdn As HiddenField
        Dim SettingName As String = String.Empty
        Dim SettingID As String
        btn = CType(sender, ExImageButton)
        txt = btn.Parent.FindControl("SettingName")
        If Not String.IsNullOrEmpty(txt.Text) Then
            SettingName = txt.Text
        End If
        hdn = btn.Parent.FindControl("SettingID")
        Response.Write("Value " & hdn.Value & "Setting Name " & SettingName)
        If Not String.IsNullOrEmpty(hdn.Value) Then
            SettingID = hdn.Value
            Response.Redirect("RSSSettings.aspx?SettingID=" & SettingID & "&SettingName=" & SettingName, True)
        End If
    End Sub

    Protected Sub rptCon_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.RepeaterItemEventArgs)
        If e.Item.ItemType <> ListItemType.Item AndAlso e.Item.ItemType <> ListItemType.AlternatingItem Then
            Return
        End If
    
        Dim btn As ExImageButton = DirectCast(e.Item.FindControl("Button1"), ExImageButton)
        btn.Attributes.Add("tooltip", "Click here to save the changesSave Changes")
        btn.Attributes.Add("tooltiptitle", "Save Changes")
        tooltip1.AddTooltipControl(btn)
        btn = DirectCast(e.Item.FindControl("btnDelete"), ExImageButton)
        btn.Attributes.Add("tooltiptitle", "Delete Process")
        tooltip1.AddTooltipControl(btn)
        
        
        
    End Sub
</script>

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>File Import Process</title>
    <link href= "../App_Themes/Css/styles.css" type="text/css" rel="stylesheet" />
    <link href= "../App_Themes/Css/Common.css" type="text/css" rel="stylesheet" />
</head>
<body>        
    <form id="form1" runat="server">
        <div id="body">
        <div id="cap"></div>
        <div id="main">
        <h1>Edit File Import Process</h1>
        <asp:Panel ID="Panel1" runat="server" HorizontalAlign="Left">
            <asp:Repeater ID="rptCon" runat="server" OnItemDataBound="rptCon_ItemDataBound">
         <HeaderTemplate>                 
    <table > 
            <tr>
            <td class="alt1">File Import Process</td>
            <td class="alt1">Folder Path</td>                        
            <td class="alt1">Action</td>
            </tr>
</HeaderTemplate>

<ItemTemplate>  
<tr>
            <td><asp:TextBox ID="SettingName" runat="server" Text='<%#Container.DataItem("SettingName")%>'></asp:TextBox>
            <asp:HiddenField ID="SettingID" runat="server" Value='<%#Container.DataItem("SettingID")%>' />            
            </td>
            <td><asp:TextBox ID="FolderPath" runat="server" Height="100%" Text='<%#Container.DataItem("FolderPath")%>'></asp:TextBox></td>                        
            <td>            
            <cc0:eximagebutton id="Button1" ToolTip="Click here to Save changes" runat="server" DisableImageURL="../App_Themes/Images/i_saveP.gif" Text="Save Changes" ImageUrl="../App_Themes/Images/i_save.gif" onclick="Button1_Click"></cc0:eximagebutton>
            <cc0:eximagebutton id="btnDelete" ToolTip="Click here to Delete Process" runat="server" DisableImageURL="../App_Themes/Images/i_deleteP.gif" Text="Delete Process" ImageUrl="../App_Themes/Images/i_delete.gif" onclick="btnDelete_Click"></cc0:eximagebutton>
            <cc0:eximagebutton id="btnEdit" ToolTip="Click here to Edit Process" runat="server" DisableImageURL="../App_Themes/Images/i_filterP.gif" Text="Edit Process" ImageUrl="../App_Themes/Images/i_filter.gif" onclick="btnEdit_Click"></cc0:eximagebutton>
            <cc0:eximagebutton id="btnTest" ToolTip="Click here to Test Process" runat="server" DisableImageURL="../App_Themes/Images/i_cancelP.gif" Text="Test Process" ImageUrl="../App_Themes/Images/i_cancel.gif" onclick="btnTest_Click"></cc0:eximagebutton>
            </td>
            
</tr>
</ItemTemplate>
<FooterTemplate>
<tr>
            <td><asp:TextBox ID="SettingName" runat="server" ></asp:TextBox></td>              
            <td><asp:TextBox ID="FolderPath" runat="server" ></asp:TextBox></td>                                    
            <td>
            <cc0:eximagebutton id="btnAdd" runat="server" DisableImageURL="../App_Themes/Images/i_newP.gif" Text="Add New Process" ImageUrl="../App_Themes/Images/i_new.gif" onclick="btnAdd_Click" ToolTip="Click here to add new process"></cc0:eximagebutton>
            <cc0:eximagebutton id="btnDelete" runat="server" DisableImageURL="../App_Themes/Images/i_deleteP.gif" Text="Delete Process" ImageUrl="../App_Themes/Images/i_delete.gif" onclick="btnDelete_Click" Enabled="false"></cc0:eximagebutton>
            <cc0:eximagebutton id="btnEdit"  runat="server" DisableImageURL="../App_Themes/Images/i_filterP.gif" Text="Edit Process" ImageUrl="../App_Themes/Images/i_filter.gif" onclick="btnEdit_Click" Enabled="false"></cc0:eximagebutton>
            <cc0:eximagebutton id="btnTest" runat="server" DisableImageURL="../App_Themes/Images/i_cancelP.gif" Text="Test Process" ImageUrl="../App_Themes/Images/i_cancel.gif" onclick="btnTest_Click" Enabled="false"></cc0:eximagebutton>
            </td>
</tr>
</table>
</FooterTemplate>

</asp:Repeater>
    <ajaxToolkit:ToolkitScriptManager runat="server" ID="ScriptManager1" />
     <dn:HoverTooltip runat="server" ID="tooltip1" SkinID="GoupBoxTooltip" />     
            
              
        <br />
        <asp:Literal ID="iResponse" runat="server"></asp:Literal>
        <MsgBox:msgBox ID="MsgBox1" runat="server" />
        <asp:HiddenField ID="hdnProID" runat="server" />
        </asp:Panel>
    </div> 
    </div> 
</form>    
</body>
</html>


