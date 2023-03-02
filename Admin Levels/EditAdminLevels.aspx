
<%@ Page Language="VB" Inherits="BasePage" %>


<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 1.0 Transitional/EN">

<script runat="server" type="text/VB">

    Private DSNavList As New Data.DataSet
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs)
        If Not IsPostBack Then
            BindIT()
        End If
    End Sub
    Private Function getNavDetails(ByVal ID As String) As String
        Dim RetVal As String = String.Empty
        For Each dr As Data.DataRow In DSNavList.Tables(0).Rows
            If dr("NavBarID").ToString = ID Then
                RetVal = dr("Details")
                Exit For
            End If
        Next
        Return RetVal
    End Function
    Private Function BindIT()
        
        Dim clsAL As New ETS.BL.AdminLevels
       
        Dim DSAL As Data.DataSet = clsAL.getAdminLevelList()
        Dim clsNavBar As New ETS.BL.NavBar
        DSNavList = clsNavBar.getNavBarList()
        clsNavBar = Nothing
        rptCon.DataSource = DSAL
        rptCon.DataBind()
        clsAL = Nothing
    End Function
    
        
    Private Function setVal(ByVal dt As String) As Boolean
        If String.IsNullOrEmpty(dt) Then
            setVal = False
        Else
            setVal = True
        End If
    End Function
    
    Protected Sub Button1_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim btn As ExImageButton
        Dim txt As TextBox
        Dim chk As CheckBox
        Dim hdn As HiddenField
        Dim hdnNB As HiddenField
        Dim isDeleted As Integer
        Dim strLvlNo, strLVLName, strDescription As String
        
        'Try
        btn = CType(sender, ExImageButton)
        txt = btn.Parent.FindControl("txtName")
        strLVLName = txt.Text
        txt = btn.Parent.FindControl("txtDesc")
        strDescription = txt.Text
        chk = btn.Parent.FindControl("chkDelete")
        hdnNB = btn.Parent.FindControl("hdnNavBar")
        If chk.Checked Then
            isDeleted = 1
        Else
            isDeleted = 0
        End If
        hdn = btn.Parent.FindControl("LevelNo")
        strLvlNo = hdn.Value
        If strLvlNo <> "" Then
            Dim clsAL As New ETS.BL.AdminLevels
            With clsAL
                .LevelNo = strLvlNo
                .LevelName = strLVLName
                .Description = strDescription
                .IsDeleted = isDeleted
                If hdnNB.Value <> "" Then
                    .NavBarID = hdnNB.Value
                End If
            End With
            Dim recAffected As Integer = clsAL.UpdateLevelDetails
             

            If recAffected > 0 Then
                MsgBox1.alert("Changes have been saved successfully!")
            Else
                MsgBox1.alert("Saving changes have failed!")
            End If
            BindIT()
        End If
    End Sub
    Protected Sub VeiwLinks_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim strLvlNo As String
        Dim hdn As HiddenField
        Dim btn As ExImageButton
        btn = CType(sender, ExImageButton)
        hdn = btn.Parent.FindControl("LevelNo")
        strLvlNo = hdn.Value
        Response.Redirect("EditAdminLevelLinks.aspx?lvlNo=" & strLvlNo, True)
    End Sub
    
    Protected Sub DDNavBar_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim ddList As DropDownList = CType(sender, DropDownList)
        If ddList.SelectedValue <> "" Then
            Dim lbl As Label = ddList.Parent.FindControl("lblNavBar")
            lbl.Text = ddList.SelectedItem.Text
            lbl.Visible = True
            Dim hdn As HiddenField = ddList.Parent.FindControl("hdnNavBar")
            hdn.Value = ddList.SelectedItem.Value
            'Dim btn As Button = ddList.FindControl("Button1")
            'btn.Enabled = True
            ddList.SelectedIndex = 0
            ddList.Visible = False
        End If
    End Sub
    Protected Sub iPopUp_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim btn As Button = CType(sender, Button)
        Dim ddlist As DropDownList = btn.Parent.FindControl("DDNavBar")
        Dim clsNavBar As New ETS.BL.NavBar
        DSNavList = clsNavBar.getNavBarList()
        clsNavBar = Nothing
        ddlist.Items.Clear()
        ddlist.DataSource = DSNavList
        ddlist.DataTextField = "Details"
        ddlist.DataValueField = "NavBarID"
        ddlist.DataBind()
        Dim LI1 As New ListItem
        LI1.Text = "Select Navigation Bar"
        LI1.Value = ""
        ddlist.Items.Insert(0, LI1)
        
            
        Dim lbl As Label = btn.Parent.FindControl("lblNavBar")
        If Not ddlist.Visible Then
            ddlist.Visible = True
            lbl.Visible = False
            btn.ToolTip = "Click to reset"
        Else
            ddlist.Visible = False
            lbl.Visible = True
            btn.ToolTip = "Click here to change Navigation Bar"
        End If
        'BindDD()
    End Sub
    
    
</script>

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
<link href= "../App_Themes/Css/styles.css" type="text/css" rel="stylesheet" />
<link href= "../App_Themes/Css/Common.css" type="text/css" rel="stylesheet" />
    <title>Edit Administractor Level</title>
</head>
<body>
    <form id="form1" runat="server">    
        <div id="body">
        <div id="cap"></div>
        <div id="main">
        <h1>Edit Administractor Levels</h1>
        <asp:Panel ID="Panel1" runat="server" HorizontalAlign="Left">
            <asp:Repeater ID="rptCon" runat="server">
         <HeaderTemplate>
<table>
            <tr>
            <td class="alt1">Level Name</td>            
            <td class="alt1">Description</td> 
            <td class="alt1">Navigation Bar</td>            
            <td class="alt1">Delete</td>            
            <td class="alt1">Action</td>            
            </tr>
</HeaderTemplate>

<ItemTemplate>
<tr>
            <td><asp:TextBox ID="txtName" runat="server" Text='<%#Container.DataItem("LevelName")%>' ></asp:TextBox><asp:HiddenField runat="server" ID="LevelNo" Value='<%#Container.DataItem("LevelNo")%>' /> </td>
            <td><asp:TextBox Width="250Px" ID="txtDesc" runat="server" Text='<%#Container.DataItem("Description")%>'></asp:TextBox></td>            
            <td><asp:DropDownList ID="DDNavBar" runat="server"  OnSelectedIndexChanged="DDNavBar_SelectedIndexChanged" AutoPostBack="true" Visible="false">            
                </asp:DropDownList>         
            <asp:Label ID="lblNavBar" runat="server" Width="150px" Text='<%#getNavDetails(Container.DataItem("NavBarID").tostring)%>'></asp:Label>        
            <asp:HiddenField ID="hdnNavBar" runat="server" Value='<%#Container.DataItem("NavBarID") %>'/>          
            <asp:Button ID="iPopUp" Enabled='<%#iif(Container.DataItem("LevelNo")=2147483647,False,True)%>' CssClass="button" runat="server" Text="..." OnClick="iPopUp_Click" ToolTip="Click here to change Navigation Bar"  />
            </td>
            <td><asp:CheckBox ID="chkDelete" runat="server" Checked='<%#Container.DataItem("isdeleted")%>' Enabled='<%#iif(cint(Container.DataItem("LevelNo"))=2147483647,False,True)%>'/></td>
            <td>
            <cc0:eximagebutton id="Button1" runat="server" DisableImageURL="../App_Themes/Images/i_saveP.gif" Text="Save Changes" ImageUrl="../App_Themes/Images/i_save.gif" onclick="Button1_Click"></cc0:eximagebutton>
            <cc0:eximagebutton id="VeiwLinks" runat="server" DisableImageURL="../App_Themes/Images/i_searchP.gif" Text="View Links" ImageUrl="../App_Themes/Images/i_searchP.gif" onclick="VeiwLinks_Click"></cc0:eximagebutton>
            </td>            
</tr>
</ItemTemplate>
<FooterTemplate>
</table>
</FooterTemplate>

</asp:Repeater>
        <MsgBox:msgBox ID="MsgBox1" runat="server" />   
        </asp:Panel>
        </div> 
        </div> 
</form>  
</body>
</html>


