<%@ Page Language="VB" Inherits="BasePage"%>


<LINK href= "../../styles/Default.css" type="text/css" rel="stylesheet">

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 1.0 Transitional/EN">

<script runat="server" type="text/VB">   
    
    
    Public strLevelName As String
    Public MaxLevel As Double
    
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs)
        LevelNo.Value = CInt(Request("lvlNo"))
        If Not IsPostBack Then
            BindIT()
        End If
    End Sub
    Private Function BindIT()
        Dim clsAL As New ETS.BL.AdminLevels
        clsAL.getAdminLevelDetails(CInt(Request("lvlNo")))
        strLevelName = clsAL.LevelName
        clsAL = Nothing
        Dim clsALL As New ETS.BL.AdminLevelLinks
        clsALL.LevelNo = CInt(Request("lvlNo"))
        Dim DSALL As Data.DataSet = clsALL.getLinkList()
        clsALL = Nothing
        rptCon.DataSource = DSALL
        rptCon.DataBind()
    End Function
    
    Protected Sub Button1_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim btn As ExImageButton
        Dim txt As TextBox
        Dim chk As CheckBox
        Dim hdn As HiddenField
        Dim isDeleted As Integer
        Dim goAhead As Boolean
        Dim strLinkID, strLink_Caption, strLink_Path, oLink_Caption As String
        Try
            btn = CType(sender, ExImageButton)
            hdn = btn.Parent.FindControl("LinkID")
            strLinkID = hdn.Value
            hdn = btn.Parent.FindControl("oLink_Caption")
            oLink_Caption = hdn.Value
            txt = btn.Parent.FindControl("Link_Caption")
            If Not String.IsNullOrEmpty(txt.Text) Then
                strLink_Caption = txt.Text
                goAhead = True
            Else
                MsgBox1.alert("Link Caption can not be blank")
                goAhead = False
            End If
            txt = btn.Parent.FindControl("Link_Path")
            If Not String.IsNullOrEmpty(txt.Text) Then
                strLink_Path = txt.Text
                Dim iFileInfo As New System.IO.FileInfo(AppDomain.CurrentDomain.BaseDirectory & strLink_Path)
                If Not iFileInfo.Exists Then
                    MsgBox1.alert("Please specify correct path and file name")
                    goAhead = False
                Else
                    goAhead = True
                End If
            Else
                MsgBox1.alert("Link Path can not be blank")
                goAhead = False
            End If
            chk = btn.Parent.FindControl("chkDelete")
            If chk.Checked Then
                isDeleted = 1
            Else
                isDeleted = 0
            End If
            If goahead Then
               
                Try
                    Dim clsALL As New ETS.BL.AdminLevelLinks
                    With clsALL
                        .LevelNo = CInt(LevelNo.Value)
                        .LinkID = strLinkID
                    End With
                    If isDeleted = 1 Then
                        If clsALL.DeleteLink() = 1 Then
                            MsgBox1.alert("Link has been deleted successfully!")
                        Else
                            MsgBox1.alert("Deleting link failed!")
                        End If
                    Else
                        clsALL.Link_Caption = strLink_Caption
                        clsALL.Link_Path = strLink_Path
                        If clsALL.UpdateLinkDetails() = 1 Then
                            MsgBox1.alert("Link has been updated successfully!")
                        Else
                            MsgBox1.alert("Updating link failed!")
                        End If
                    End If
                   
                Catch ex As Exception
                    Response.Write(ex.Message)
                
                End Try
                BindIT()
            Else
                BindIT()
            End If
        Catch ex As Exception
            MsgBox1.alert(ex.Message)
        End Try
    End Sub
    Protected Sub btnAdd_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim btn As ExImageButton
        Dim txt As TextBox
        Dim strLink_Caption, strLink_Path As String
        Dim goAhead As Boolean
        strLink_Caption = String.Empty
        strLink_Path = String.Empty
        'Try
        btn = CType(sender, ExImageButton)
        txt = btn.Parent.FindControl("Link_Caption")
        If Not String.IsNullOrEmpty(txt.Text) Then
            strLink_Caption = txt.Text
            goAhead = True
        Else
            MsgBox1.alert("Link Caption can not be blank")
            goAhead = False
        End If
                
        txt = btn.Parent.FindControl("Link_Path")
        If Not String.IsNullOrEmpty(txt.Text) Then
            strLink_Path = txt.Text
            Dim iFileInfo As New System.IO.FileInfo(AppDomain.CurrentDomain.BaseDirectory & strLink_Path)
            If Not iFileInfo.Exists Then
                MsgBox1.alert("Please specify correct path and file name")
                goAhead = False
            Else
                goAhead = True
            End If
        Else
            MsgBox1.alert("Link Path can not be blank")
            goAhead = False
        End If
        If goAhead Then
            'ConString = System.Configuration.ConfigurationManager.AppSettings("ETSCon")
            'Dim oConn As New Data.SqlClient.SqlConnection
            'oConn.ConnectionString = ConString
            'Try
                   
            Dim clsALL As New ETS.BL.AdminLevelLinks
            clsALL.LevelNo = CInt(Request("lvlNo"))
            Dim DSALL As Data.DataSet = clsALL.getLinkList()
            Dim lvl = DSALL.Tables(0).Compute("MAX(LinkID)", "LevelNo='" & LevelNo.Value & "'")
            MaxLevel = IIf(IsDBNull(lvl), 0, lvl)
            'Response.Write(MaxLevel & " " & LevelNo.Value)
            'Response.End()
            If MaxLevel = 1073741824 Then
                Dim DR() As Data.DataRow
                Dim LL As Integer = 1
                Do While Not LL = 1073741824
                    DR = DSALL.Tables(0).Select("LinkID=" & LL & "")
                    'Response.Write(UBound(DR) & " " & LL & "<BR>")
                    If UBound(DR) = -1 Then
                        MaxLevel = LL
                        Exit Do
                    End If
                    LL = LL + LL
                Loop
            Else
                MaxLevel = MaxLevel + MaxLevel
            End If
            If MaxLevel = 0 Then
                MaxLevel = 1
            End If
            
            With clsALL
                .Link_Caption = strLink_Caption
                .Link_Path = strLink_Path
                .LinkID = MaxLevel
            End With
            If clsALL.InsertNewLink() = 1 Then
                MsgBox1.alert("Link has been added successfully!")
            End If
                    
            clsALL = Nothing
                                      
            BindIT()
            'Catch ex As Exception
            '    Response.Write(ex.Message)
            'End Try
        End If
        'Catch ex As Exception
        '    MsgBox1.alert(ex.Message)
        'End Try
        
    End Sub

    
   
</script>

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Untitled Page</title>
</head>
<body>
    <form id="form1" runat="server">
        
        <br />
        <br />
    
    
        <asp:Repeater ID="rptCon" runat="server" >
         <HeaderTemplate>
         <table border="1">
            <TR bgcolor="#3399cc">
                <th align="center">
                <div class="HeaderDiv" align="center">
                    <%#"Available Links For Admin Level:" & strLevelName%>
                </div>    
                </th>                
            </tr>            
        </table> 
        <br>
        <br>
    <table border="1"> 
            <TR bgcolor="#3399cc">
            <TH><div class="HeaderDiv" align="center">Link Caption</div></TH>
            <TH><div class="HeaderDiv" align="center">Link Path</div></th>            
            <TH><div class="HeaderDiv" align="center">Delete</div></th>            
            <TH><div class="HeaderDiv" align="center">Action</div></th>
            </TR>
</HeaderTemplate>

<ItemTemplate>  
<tr bgcolor="#cccccc">
            <td><asp:TextBox ID="Link_Caption" runat="server" Text='<%#Container.DataItem("Link_Caption")%>'></asp:TextBox>
            <asp:HiddenField ID="LinkID" runat="server" Value='<%#Container.DataItem("LinkID")%>'/>              
            <asp:HiddenField ID="oLink_Caption" runat="server" Value='<%#Container.DataItem("Link_Caption")%>' />
            </td>
            <td><asp:TextBox ID="Link_Path" runat="server" Width="300px" Text='<%#Container.DataItem("Link_Path")%>'></asp:TextBox></td>            
            <td><asp:CheckBox ID="chkDelete" runat="server" /></td>            
            <td><%--<asp:Button ID="Button1" runat="server" Text="Save Changes" OnClick="Button1_Click" CausesValidation="false" CommandName="Confirmation" OnClientClick="return confirm('Are you certain you want to update the changes?');" /> --%>
                <cc0:eximagebutton id="Button1" runat="server" DisableImageURL="../images/toolbar/i_saveP.gif" Text="Save Changes" ImageUrl="../images/toolbar/i_save.gif" onclick="Button1_Click"></cc0:eximagebutton>
            </td>
</tr>
</ItemTemplate>
<FooterTemplate>
<tr bgcolor="#cccccc">            
            <td><asp:TextBox ID="Link_Caption" runat="server" ></asp:TextBox></td>              
            <td><asp:TextBox ID="Link_Path" runat="server" Width="300px"></asp:TextBox></td>            
            <td><asp:CheckBox ID="chkDelete" runat="server" Checked=false Enabled=false/></td>            
            <td>
            <cc0:eximagebutton id="btnAdd" runat="server" DisableImageURL="../images/toolbar/i_newP.gif" Text="Add New Link" ImageUrl="../images/toolbar/i_new.gif" onclick="btnAdd_Click"></cc0:eximagebutton>
            </td>
</tr>
</table>
</FooterTemplate>

</asp:Repeater> 
<asp:HiddenField ID="LevelNo" runat="server" />  
        <MsgBox:msgBox ID="MsgBox1" runat="server" />   
</form>    
    
</body>
</html>


