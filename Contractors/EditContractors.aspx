<%@ Page Language="VB" Inherits="BasePage"%>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 1.0 Transitional/EN">

<script runat="server" type="text/VB">
    Private dsCon As New Data.DataSet
    Private Function getParentDetails(ByVal ID As String) As String
        Dim RetVal As String = String.Empty
      
        For Each dr As Data.DataRow In dsCon.Tables(0).Rows
            If dr("ContractorID").ToString = ID Then
                RetVal = dr("ContractorName")
                Exit For
            End If
        Next
        Return RetVal
    End Function
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs)
        If Not IsPostBack Then
            Dim clsCon As New ETS.BL.Contractor
            With clsCon
                If Session("IsOwner") = False Then
                    .ParentID = Session("ContractorID")
                End If
                
            End With
            dsCon = clsCon.getContractorList
            clsCon = Nothing
            rptCon.DataSource = dsCon
            rptCon.DataBind()
            
            
        End If
    End Sub

    Protected Sub Button1_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim btn As Button
        Dim txt As TextBox
        Dim chk As CheckBox
        Dim hdn As HiddenField
        Dim isDeleted As Integer
        Dim ddl As DropDownList
        Dim strConID, strConName, strDescription, strDelDate As String
        Dim InstanceID As Integer
        
        Try
            btn = CType(sender, Button)
            txt = btn.Parent.FindControl("txtName")
            strConName = txt.Text
            txt = btn.Parent.FindControl("txtDesc")
            strDescription = txt.Text
            
            chk = btn.Parent.FindControl("chkDelete")
            If chk.Checked Then
                isDeleted = 1
            Else
                isDeleted = 0
            End If
            hdn = btn.Parent.FindControl("chko")
            'Response.Write(hdn.Value & "</br>")
            If String.IsNullOrEmpty(hdn.Value) And isDeleted = 1 Then
                strDelDate = Now()
            Else
                strDelDate = hdn.Value
            End If
            hdn = btn.Parent.FindControl("ConID")
            strConID = hdn.Value
            ddl = btn.Parent.FindControl("DLInstance")
            InstanceID = ddl.SelectedIndex
            If strConID <> "" Then
                Dim ClsCon As New ETS.BL.Contractor
                With ClsCon
                    .ContractorID = strConID
                    .ContractorName = strConName.ToString
                    .Description = strDescription.ToString
                    .isDeleted = isDeleted
                    .InstanceID = InstanceID
                    If isDeleted Then
                        .DeleteDate = strDelDate
                    End If
                End With
                If ClsCon.UpdateContractorDetails() = 1 Then
                    Response.Redirect("editcontractors.aspx", True)
                Else
                    Response.Write("Update Failed")
                End If
            End If
        Catch ex As Exception
            'Response.Write(ex.Message)
        End Try
    End Sub

    
    
    Protected Sub rptCon_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.RepeaterItemEventArgs)
        If e.Item.ItemType = ListItemType.AlternatingItem Or e.Item.ItemType = ListItemType.Item Then
            
            Dim DDL As DropDownList = e.Item.FindControl("DLInstance")
            Dim hdn As HiddenField = DDL.FindControl("InstanceID")
            DDL.SelectedIndex = hdn.Value
        End If
    End Sub
</script>

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Edit Contractors</title>
    <link href= "../App_Themes/Css/styles.css" type="text/css" rel="stylesheet" />
    <link href= "../App_Themes/Css/Common.css" type="text/css" rel="stylesheet" />
    <link href="../App_Themes/Css/DataTable.css" rel="stylesheet" type="text/css" />
    <link href="../App_Themes/Css/TableSorter.css" rel="stylesheet" type="text/css" />
    <script src="../App_Themes/JS/jquery-1.4.2.min.js" type="text/javascript"></script>  
    <script src="../App_Themes/JS/jquery.dataTables.min.js" type="text/javascript"></script>  
    <script type="text/javascript" language="javascript">
    $(document).ready(function() {
				$('#tblCon').dataTable( {
					//"sPaginationType": "full_numbers"
                    "aoColumns": [
                            		{ "asSorting": [ "desc", "asc" ] },
		                            { "asSorting": [ "desc", "asc" ] },
		                            { "asSorting": [ "desc", "asc" ] },
		                            { "asSorting": [ "desc", "asc" ] },
		                            { "asSorting": [ "desc", "asc" ] },
		                            { "asSorting": [ "desc", "asc" ] }
	                              ] 
				} );
			} );
</script>
</head>
<body>
    <form id="form1" runat="server">
        <asp:Panel ID="Panel1" runat="server" HorizontalAlign="Left">
            <div id="body" style="width: 790px">
            <div id="cap"></div>
            <div id="main">
            <h1>Edit Contractors</h1>
            <asp:Repeater ID="rptCon" runat="server" OnItemDataBound="rptCon_ItemDataBound">
         <HeaderTemplate>
<table width="100%" id="tblCon">
            <thead>
            <th class="Header">Name</th>
            <th class="Header">Description</th>
            <th class="Header">Level</th>
            <th class="Header">InstanceID</th>
            <th class="Header">Delete</th>
            <th class="Header">Date Created</th>
            <th class="Header">Action</th>
            
            </thead>
</HeaderTemplate>

<ItemTemplate>
<tr>
            <td><asp:TextBox ID="txtName" runat="server" Text='<%#Container.DataItem("ContractorName")%>' Width='150px'></asp:TextBox><asp:HiddenField runat="server" ID="ConID" Value='<%#Container.DataItem("ContractorID")%>' /> </td>
            <td><asp:TextBox ID="txtDesc" runat="server" Text='<%#Container.DataItem("Description")%>' Width='170px'></asp:TextBox></td>
            <td><%#IIf(String.IsNullOrEmpty(Container.DataItem("ParentID").ToString), "Contractor", "Sub-Contractor (" & getParentDetails(Container.DataItem("ParentID").ToString) & ")")%></td>            
            <td><asp:DropDownList ID="DLInstance" runat="server" Width="50px">
                        <asp:ListItem Text="" Value=""></asp:ListItem>
                        <asp:ListItem Text="1" Value="1"></asp:ListItem>
                        <asp:ListItem Text="2" Value="2"></asp:ListItem>
                    </asp:DropDownList><asp:HiddenField ID="InstanceID" runat="server" Value='<%#Container.DataItem("InstanceID")%>' />
            </td>
            <td><asp:CheckBox ID="chkDelete" runat="server" Checked='<%#Container.DataItem("isdeleted")%>' /><asp:HiddenField ID="chko" runat="server" Value='<%#Container.DataItem("DeleteDate")%>' /></td>
            <td><asp:TextBox ID="txtCreatedDate" runat="server" Text='<%#Container.DataItem("CreateDate")%>' Enabled="false"></asp:TextBox></td>
            <td><asp:Button ID="Button1" CssClass="button" runat="server" Text="Save Changes" OnClick="Button1_Click" /> </td>
</tr>
</ItemTemplate>
<AlternatingItemTemplate>
<tr>

            <td><asp:TextBox ID="txtName" runat="server" Text='<%#Container.DataItem("ContractorName")%>' Width='150px'></asp:TextBox><asp:HiddenField runat="server" ID="ConID" Value='<%#Container.DataItem("ContractorID")%>' /> </td>
            <td><asp:TextBox ID="txtDesc" runat="server" Text='<%#Container.DataItem("Description")%>' Width='170px'></asp:TextBox></td>
            <td><%#IIf(String.IsNullOrEmpty(Container.DataItem("ParentID").ToString), "Contractor", "Sub-Contractor (" & getParentDetails(Container.DataItem("ParentID").ToString) & ")")%></td>            
            <td><asp:DropDownList ID="DLInstance" runat="server" Width="50px">
                        <asp:ListItem Text="" Value=""></asp:ListItem>
                        <asp:ListItem Text="1" Value="1"></asp:ListItem>
                        <asp:ListItem Text="2" Value="2"></asp:ListItem>
                    </asp:DropDownList><asp:HiddenField ID="InstanceID" runat="server" Value='<%#Container.DataItem("InstanceID")%>' /></td>
            <td><asp:CheckBox ID="chkDelete" runat="server" Checked='<%#Container.DataItem("isdeleted")%>' /><asp:HiddenField ID="chko" runat="server" Value='<%#Container.DataItem("DeleteDate")%>' /></td>
            <td><asp:TextBox ID="txtCreatedDate" runat="server" Text='<%#Container.DataItem("CreateDate")%>' Enabled="false"></asp:TextBox></td>
            <td><asp:Button ID="Button1" CssClass="button" runat="server" Text="Save Changes" OnClick="Button1_Click" /> </td>
</tr>
</AlternatingItemTemplate>
<FooterTemplate>
</table>
</FooterTemplate>

</asp:Repeater>
</div> 
</div> 
        </asp:Panel>
              
</form>    
    
</body>
</html>


