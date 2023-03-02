<%@ Page Language="VB" Inherits="BasePage" %>
<%@ Register
    Assembly="AjaxControlToolkit"
    Namespace="AjaxControlToolkit"
    TagPrefix="ajaxToolkit" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 1.0 Transitional/EN">

<script runat="server">

    Protected Sub btnSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        DoSearch(txtUser.Text, opName.Checked)
    End Sub
    Private Function DoSearch(ByVal UserName As String, ByVal UserOption As Boolean) As Boolean
        'Dim clsUsers As New ETS.BL.Users
        'With clsUsers
        '    .ContractorID = Session("ContractorID")
        '    ._WhereString.Append(" and (Isdeleted is NULL or Isdeleted = 'False') ")
        '    If UserOption Then
        '        ._WhereString.Append(" and FirstName+' '+LastName like '%" & UserName & "%'")
        '    Else
        '        ._WhereString.Append(" and UserName like '%" & UserName & "%'")
        '    End If
        '    Dim DSUsers As Data.DataSet = .getUsersList()
        '    DSUsers.Tables(0).Columns.Add("UName", GetType(System.String), "FirstName+' '+LastName")
        '    rptUsers.DataSource = DSUsers
        '    rptUsers.DataBind()
        '    DSUsers.Dispose()
        '    MultiView1.ActiveViewIndex = 1
        'End With
        'clsUsers = Nothing
        
        Dim clsUsers As ETS.BL.Users
        Dim DSUsers As New Data.DataSet
        Dim varWhere As String = String.Empty
        Try
            clsUsers = New ETS.BL.Users
            
            If UserOption Then
                varWhere = "  AND FirstName+' '+LastName like '%" & UserName & "%' "
            Else
                varWhere = " AND UserName like '%" & UserName & "%' "
            End If
            
            DSUsers = clsUsers.getUsersList(Session("ContractorID"), Session("WorkgroupID"), varWhere.ToString)
            If DSUsers.Tables.Count > 0 Then
                If DSUsers.Tables(0).Rows.Count > 0 Then
                    DSUsers.Tables(0).Columns.Add("UName", GetType(System.String), "FirstName+' '+LastName")
                    rptUsers.DataSource = DSUsers
                    rptUsers.DataBind()
                    MultiView1.ActiveViewIndex = 1
                End If
            End If
        Catch ex As Exception
            txtUser.Text = ex.Message
        Finally
            clsUsers = Nothing
            DSUsers = Nothing
        End Try
    End Function
    
    Protected Sub btnVeiw_click(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim btn As Button
        Dim hdn As HiddenField
        Dim selectedUserID As String = String.Empty
        Dim selectedUserName As String = String.Empty
        btn = CType(sender, Button)
        hdn = btn.Parent.FindControl("UserID")
        selectedUserID = hdn.Value
        hdn = btn.Parent.FindControl("UserName")
        selectedUserName = hdn.Value
        If Not String.IsNullOrEmpty(selectedUserID) Then
            Response.Redirect("UsersAdminLevels.aspx?UserID=" & selectedUserID & "&UserName=" & selectedUserName & "&CriUser=" & txtUser.Text & "&CriOption=" & opName.Checked.ToString)
        Else
            Response.Write("please select an User")
        End If
    End Sub
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs)
        If String.IsNullOrEmpty(Request("CriOption")) Then
            MultiView1.ActiveViewIndex = 0
        Else
            DoSearch(Request("CriUser"), Request("CriOption"))
            MultiView1.ActiveViewIndex = 1
        End If
    End Sub

    
    Protected Sub opName_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim op As RadioButton = CType(sender, RadioButton)
        Dim autoC As AutoCompleteExtender = op.Parent.FindControl("AutoC")
        autoC.MinimumPrefixLength = "1"
        autoC.CompletionSetCount = "10"
        autoC.TargetControlID = "txtUser"
        autoC.ServicePath = "../users/autocomplete.asmx"
        autoC.ServiceMethod = "GetCompletionList"
        autoC.EnableCaching = "true"
    End Sub

    Protected Sub opID_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles opID.CheckedChanged
        Dim op As RadioButton = CType(sender, RadioButton)
        Dim autoC As AutoCompleteExtender = op.Parent.FindControl("AutoC")
        autoC.MinimumPrefixLength = "1"
        autoC.CompletionSetCount = "10"
        autoC.TargetControlID = "txtUser"
        autoC.ServicePath = "../users/autocomplete.asmx"
        autoC.ServiceMethod = "GetUserID"
        autoC.EnableCaching = "true"
    End Sub

    Protected Sub btnBack_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        MultiView1.ActiveViewIndex = 0
    End Sub
</script>

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    
 <link href= "../App_Themes/Css/styles.css" type="text/css" rel="stylesheet" /></head>
<body style="text-align: center">
<form id="form1" runat="server">   
<div id="body">
    <div id="cap"></div>
    <div id="main">
    <h1>Manage User Access Levels  </h1> 
<ajaxToolkit:ToolkitScriptManager runat="server" ID="ScriptManager1"/>    
        <asp:Panel ID="Panel1" runat="server" HorizontalAlign="Left">
           
        <asp:MultiView ID="MultiView1" runat="server">
        
            <asp:View ID="View1" runat="server">
                <table style="left: 15%; top: 15%" border=1>
                    <tr>
                <td bgcolor="#3399cc" class="HeaderDiv" colspan="3" style="height: 21px; text-align: center; width: 203px;">
                   
                    Search User
                   
                    </td>
                    </tr>
                    <tr>
                <td class="alt1" bgcolor="#cccccc" style="width: 203px; height: 21px; text-align: center" colspan="3">
                    &nbsp;<asp:Label ID="Label1" runat="server"  Text="By "></asp:Label><asp:RadioButton ID="opName" runat="server" Checked="True" Text="Name"  GroupName="grpSearch" OnCheckedChanged="opName_CheckedChanged" AutoPostBack="true" /><asp:RadioButton ID="opID" runat="server" Text="User ID"  GroupName="grpSearch" OnCheckedChanged="opID_CheckedChanged" AutoPostBack="true" /></td>
                    </tr>
                    <tr>
                        <td colspan="3" style="width: 203px; height: 21px; text-align: center">
                    <asp:TextBox ID="txtUser" runat="server" 
                        Height="18px" Width="143px"></asp:TextBox>
                        <ajaxToolkit:AutoCompleteExtender ID="AutoC" 
                                  MinimumPrefixLength="1" 
                                  CompletionSetCount="10" 
                                  runat="server" 
                                  TargetControlID="txtUser"
                                  ServicePath="../users/autocomplete.asmx"
                                  ServiceMethod="GetCompletionList"
                                  EnableCaching="true"/>
                        
                        </td>
                    </tr>
                    <tr>
                        <td colspan="3" style="width: 203px; text-align: center">
                    <asp:Button ID="btnSearch" CssClass="button"  runat="server"  Text="Search"
                        Width="124px" OnClick="btnSearch_Click" /></td>
                    </tr>
                    <tr>
                        <td colspan="3" style="width: 203px; text-align: center">
                            <asp:Label ID="lblResponse" runat="server"></asp:Label></td>
                    </tr>
                </table>
            </asp:View>
            
            
            <asp:View ID="View2" runat="server">
     <asp:Repeater ID="rptUsers" runat="server" >
         <HeaderTemplate>
<table >
<tr>
            
            <td class="HeaderDiv" colspan="4" style="text-align:center;"  >User Details</td>           
            
</tr>
            <tr>            
            <td class="alt1">User Name</td>
            <td class="alt1">User ID</td>            
            <td class="alt1">Action</td>
            </tr>
</HeaderTemplate>

<ItemTemplate>
<tr>
            
            <td><%#Container.DataItem("UName")%></td>           
            <td><%#Container.DataItem("UserName")%></td>
            <td><asp:Button CssClass="button" ID="btnView" runat="server" Text="Edit Levels" OnClick="btnVeiw_click"/>
            <asp:HiddenField ID="UserID" runat="server" Value='<%#Container.DataItem("UserID")%>' />
            <asp:HiddenField ID="UserName" runat="server" Value='<%#Container.DataItem("UName") & "(" & Container.DataItem("UserName") & ")"%>' />
            </td>
</tr>
</ItemTemplate>
<AlternatingItemTemplate>
<tr bgcolor="#cccccc">            
            <td><%#Container.DataItem("UName")%></td>           
            <td><%#Container.DataItem("UserName")%></td>
            <td><asp:Button ID="btnView" CssClass="button" runat="server" Text="Edit Levels" OnClick="btnVeiw_click"/>
            <asp:HiddenField ID="UserID" runat="server" Value='<%#Container.DataItem("UserID")%>' />
             <asp:HiddenField ID="UserName" runat="server" Value='<%#Container.DataItem("UName") & "(" & Container.DataItem("UserName") & ")"%>' />
            </td>
</tr>
</AlternatingItemTemplate>
<FooterTemplate>
</table>
</FooterTemplate>

</asp:Repeater>  
                <asp:Button ID="btnBack"  CssClass="button"  runat="server" OnClick="btnBack_Click" Text="<< Back to Search"
                    Width="120px" />
            </asp:View>            
        </asp:MultiView>    
        </asp:Panel>

</div>
</div>

    <asp:ValidationSummary ID="ValidationSummary1" runat="server" HeaderText="Following error occurs:" ShowMessageBox="true" DisplayMode="BulletList" ShowSummary="False" /> </form>        
</body>
</html>

