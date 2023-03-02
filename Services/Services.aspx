<%@ Page Language="VB" Inherits="BasePage"%>


<LINK href= "../../styles/Default.css" type="text/css" rel="stylesheet">

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 1.0 Transitional/EN">

<script runat="server" type="text/VB">   
    
    Public strLevelName As String
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs)
        If Not IsPostBack Then
            BindIT()
        End If
    End Sub
    Private Function BindIT()
        Dim clsServices As ETS.BL.Services
        Dim Ds As New Data.DataSet
        Try
            clsServices = New ETS.BL.Services
            Ds = clsServices.getServicesList
            If Ds.Tables.Count > 0 Then
                If Ds.Tables(0).Rows.Count > 0 Then
                    rptCon.DataSource = Ds
                    rptCon.DataBind()
                End If
            End If
        Catch ex As Exception
            Response.Write(ex.Message)
        Finally
            Ds = Nothing
            clsServices = Nothing
        End Try
    End Function
    
    Protected Sub Button1_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim btn As ExImageButton
        Dim txt As TextBox
        Dim chk As CheckBox
        Dim HDN As HiddenField
        Dim recAffected As Integer
        Dim ID, Name, Desc, URL As String
        Dim IsActive, isDeleted, goAhead As Boolean
        Name = String.Empty
        Desc = String.Empty
        URL = String.Empty
        ID = String.Empty
        Try
            btn = CType(sender, ExImageButton)
            HDN = btn.Parent.FindControl("ID")
            ID = HDN.Value
            
            txt = btn.Parent.FindControl("Name")
            If Not String.IsNullOrEmpty(txt.Text) Then
                Name = txt.Text
                goAhead = True
            Else
                MsgBox1.alert("Service Name can not be blank")
                goAhead = False
            End If
                
            txt = btn.Parent.FindControl("Desc")
            If Not String.IsNullOrEmpty(txt.Text) Then
                Desc = txt.Text
                'goAhead = True            
            End If
            
            btn = CType(sender, ExImageButton)
            txt = btn.Parent.FindControl("URL")
            If Not String.IsNullOrEmpty(txt.Text) Then
                URL = txt.Text
                goAhead = True
            Else
                MsgBox1.alert("Service URL can not be blank")
                goAhead = False
            End If
            chk = btn.Parent.FindControl("IsActive")
            IsActive = chk.Checked
            chk = btn.Parent.FindControl("chkDelete")
            isDeleted = chk.Checked
            If goAhead Then
                Dim strMessage As String
                Dim clsSer As ETS.BL.Services
                Try
                    clsSer = New ETS.BL.Services
                    clsSer.ServiceID = ID
                    If isDeleted = True Then
                        recAffected = clsSer.DeleteService
                        strMessage = "Service " & Name & " deleted successfully"
                    Else
                        clsSer.ServiceName = Name
                        clsSer.ServiceDesc = Desc
                        clsSer.ISActive = IsActive
                        clsSer.ServiceURL = URL
                        recAffected = clsSer.UpdateServiceDetails
                        strMessage = "Service " & Name & " updated successfully"
                    End If
                    
                    If recAffected > 0 Then
                        MsgBox1.alert(strMessage) '("Changes have been updated successfully!")
                    Else
                        MsgBox1.alert("Updating changes failed!")
                    End If
                Catch ex As Exception
                    Response.Write(ex.Message)
                Finally
                    clsSer = Nothing
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
        Dim chk As CheckBox
        Dim recAffected As Integer
        Dim Name, Desc, URL As String
        Dim IsActive, goAhead As Boolean
        Name = String.Empty
        Desc = String.Empty
        URL = String.Empty
        Try
            btn = CType(sender, ExImageButton)
            txt = btn.Parent.FindControl("Name")
            If Not String.IsNullOrEmpty(txt.Text) Then
                Name = txt.Text
                goAhead = True
            Else
                MsgBox1.alert("Service Name can not be blank")
                goAhead = False
            End If
                
            txt = btn.Parent.FindControl("Desc")
            If Not String.IsNullOrEmpty(txt.Text) Then
                Desc = txt.Text
                'goAhead = True            
            End If
            
            btn = CType(sender, ExImageButton)
            txt = btn.Parent.FindControl("URL")
            If Not String.IsNullOrEmpty(txt.Text) Then
                URL = txt.Text
                goAhead = True
            Else
                MsgBox1.alert("Service URL can not be blank")
                goAhead = False
            End If
            chk = btn.Parent.FindControl("IsActive")
            IsActive = chk.Checked
            
            If goAhead Then
                Dim clsSer As ETS.BL.Services
                Try
                    clsSer = New ETS.BL.Services
                    clsSer.ServiceName = Name
                    clsSer.ServiceDesc = Desc
                    clsSer.ISActive = IsActive
                    clsSer.ServiceURL = URL
                    
                    recAffected = clsSer.InsertService
                    If recAffected > 0 Then
                        MsgBox1.alert("Service has been added successfully!")
                    End If
                    BindIT()
                Catch ex As Exception
                    Response.Write(ex.Message)
                Finally
                    clsSer = Nothing
                End Try
            End If
        Catch ex As Exception
            MsgBox1.alert(ex.Message)
        End Try
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
                    Edit Services
                </div>    
                </th>                
            </tr>            
        </table> 
        <br>
        <br>
    <table border="1"> 
            <TR bgcolor="#3399cc">
            <TH><div class="HeaderDiv" align="center">Service Name</div></TH>
            <TH><div class="HeaderDiv" align="center">Description</div></th>            
            <TH><div class="HeaderDiv" align="center">IsActive</div></th>            
            <TH><div class="HeaderDiv" align="center">URL</div></th>            
            <TH><div class="HeaderDiv" align="center">Delete</div></th>
            <TH><div class="HeaderDiv" align="center"></div></th>            
            </TR>
</HeaderTemplate>

<ItemTemplate>  
<tr bgcolor="#cccccc">
            <td><asp:TextBox ID="Name" runat="server" Text='<%#Container.DataItem("ServiceName")%>'></asp:TextBox>
            <asp:HiddenField ID="ID" runat="server" Value='<%#Container.DataItem("ServiceID")%>' />           
            </td>
            <td><asp:TextBox ID="Desc" runat="server" Text='<%#Container.DataItem("ServiceDesc")%>'></asp:TextBox></td>            
            <td><asp:CheckBox ID="IsActive" runat="server" Checked='<%#Container.DataItem("IsActive")%>' /></td>
            <td><asp:TextBox ID="URL" runat="server" Text='<%#Container.DataItem("ServiceURL")%>'></asp:TextBox></td>                                    
            <td><asp:CheckBox ID="chkDelete" runat="server" /></td>            
            <td><cc0:eximagebutton id="Button1" runat="server" DisableImageURL="../images/toolbar/i_saveP.gif" Text="Save Changes" ImageUrl="../images/toolbar/i_save.gif" onclick="Button1_Click"></cc0:eximagebutton></td>
</tr>
</ItemTemplate>
<FooterTemplate>
<tr bgcolor="#cccccc">
            <td><asp:TextBox ID="Name" runat="server"></asp:TextBox></td>
            <td><asp:TextBox ID="Desc" runat="server" ></asp:TextBox></td>            
            <td><asp:CheckBox ID="IsActive" runat="server" /></td>
            <td><asp:TextBox ID="URL" runat="server"></asp:TextBox></td>                                    
            <td><asp:CheckBox ID="chkDelete" runat="server" Checked=false Enabled=false/></td>            
            <td>
            <cc0:eximagebutton id="btnAdd" runat="server" DisableImageURL="../images/toolbar/i_newP.gif" Text="Add New Service" ImageUrl="../images/toolbar/i_new.gif" onclick="btnAdd_Click"></cc0:eximagebutton>
            </td>
</tr>
</table>
</FooterTemplate>

</asp:Repeater>   
        <MsgBox:msgBox ID="MsgBox1" runat="server" />   
</form>    
    
</body>
</html>


