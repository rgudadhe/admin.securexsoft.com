<%@ Page Language="VB" Inherits="BasePage"%>
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
        
        Dim ConString As String
        Dim SQLString As String
        ConString = System.Configuration.ConfigurationManager.AppSettings("ETSCon")
        Dim oConn As New Data.SqlClient.SqlConnection
        oConn.ConnectionString = ConString
        Try
            oConn.Open()
            If UserOption Then
                SQLString = "select UserID,FirstName+' '+LastName as UName,UserName from tblUsers where  (Isdeleted is NULL or Isdeleted = 'False') and FirstName+' '+LastName like '%" & UserName & "%' and ContractorID='" & Session("ContractorID") & "'"
            Else
                SQLString = "select UserID,FirstName+' '+LastName as UName,UserName from tblUsers where  (Isdeleted is NULL or Isdeleted = 'False') and UserName like '%" & UserName & "%' and ContractorID='" & Session("ContractorID") & "'"
            End If
            Dim oCommand As New Data.SqlClient.SqlCommand(SQLString, oConn)
            Dim oRec As Data.SqlClient.SqlDataReader = oCommand.ExecuteReader()
            If oRec.HasRows Then
                rptUsers.DataSource = oRec
                rptUsers.DataBind()
                oConn.Close()
                MultiView1.ActiveViewIndex = 1
            Else
                Response.Write("No Records Found!")
            End If
            oRec.Close()
        Catch ex As Exception
            Response.Write(ex.Message)
        Finally
            If oConn.State <> Data.ConnectionState.Closed Then
                oConn.Close()
                oConn = Nothing
            End If
        End Try
    End Function
    Protected Sub btnVeiw_click(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim btn As Button
        Dim hdn As HiddenField
        Dim selectedUserID As String = String.Empty
        btn = CType(sender, Button)
        hdn = btn.Parent.FindControl("UserID")
        selectedUserID = hdn.Value
        If Not String.IsNullOrEmpty(selectedUserID) Then
            Response.Redirect("UsersProdnLevels.aspx?UserID=" & selectedUserID & "&CriUser=" & txtUser.Text & "&CriOption=" & opName.Checked.ToString)
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

    Protected Sub opID_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs)
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
    <title>E-Dictate - The Best Transcription Solution</title>
<link href= "../styles/Default.css" type="text/css" rel="stylesheet"/>
</head>
<body style="text-align: center">
<form id="form1" runat="server"> 
<ajaxToolkit:ToolkitScriptManager runat="server" ID="ScriptManager1"/>    
<asp:UpdatePanel runat="server" ID="up2" >
        <ContentTemplate>        
        <asp:MultiView ID="MultiView1" runat="server">
            <asp:View ID="View1" runat="server">
                <table style="left: 15%; top: 15%; width: 272px;" border=1>
                    <tr>
                <td bgcolor="#3399cc" colspan="3" style="height: 21px; text-align: center; width: 254px;">
                    <div class="HeaderDiv">
                    Search User
                    </div></td>
                    </tr>
                    <tr class="smselected">
                <td bgcolor="#cccccc" style="width: 254px; height: 21px; text-align: center" colspan="3">
                    &nbsp;<asp:Label ID="Label1" runat="server" Font-Names="Tahoma" Font-Size="10pt" Text="By "></asp:Label><asp:RadioButton ID="opName" runat="server" Checked="True" Text="Name" Font-Names="Tahoma" Font-Size="10pt" GroupName="grpSearch" OnCheckedChanged="opName_CheckedChanged" AutoPostBack="True" Width="80px" /><asp:RadioButton ID="opID" runat="server" Text="User ID" Font-Names="Tahoma" Font-Size="10pt" GroupName="grpSearch" OnCheckedChanged="opID_CheckedChanged" AutoPostBack="True" Width="80px" /></td>
                    </tr>
                    <tr>
                        <td colspan="3" style="width: 254px; height: 21px; text-align: center">
                    <asp:TextBox ID="txtUser" runat="server" Font-Names="Tahoma" Font-Size="10pt"
                        Height="18px" Width="143px"></asp:TextBox>
                        <ajaxToolkit:AutoCompleteExtender ID="AutoC" 
                                  MinimumPrefixLength="1" 
                                  CompletionSetCount="10" 
                                  runat="server" 
                                  TargetControlID="txtUser"
                                  ServicePath="../users/autocomplete.asmx"
                                  ServiceMethod="GetCompletionList" EnableCaching="true"/>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="3" style="width: 254px; text-align: center">
                    <asp:Button ID="btnSearch" runat="server" Font-Names="Tahoma" Font-Size="10pt" Text="Search"
                        Width="124px" OnClick="btnSearch_Click" /></td>
                    </tr>
                </table>
            </asp:View>
            <asp:View ID="View2" runat="server">
     <asp:Repeater ID="rptUsers" runat="server" >
         <HeaderTemplate>
<table border="1"  id="myScrollTable">
            <TR bgcolor="#3399cc">            
            <TH> <div class="HeaderDiv">User Name</div></TH>
            <TH> <div class="HeaderDiv">User ID</div></th>            
            <TH> <div class="HeaderDiv">Action</div></TH>
            </TR>
</HeaderTemplate>

<ItemTemplate>
<tr>
            
            <td><%#Container.DataItem("UName")%></td>           
            <td><%#Container.DataItem("UserName")%></td>
            <td><asp:Button ID="btnVeiw" runat="server" Text="Edit Levels" OnClick="btnVeiw_click"/>
            <asp:HiddenField ID="UserID" runat="server" Value='<%#Container.DataItem("UserID")%>' />
            </td>
</tr>
</ItemTemplate>
<AlternatingItemTemplate>
<tr bgcolor="#cccccc">            
            <td><%#Container.DataItem("UName")%></td>           
            <td><%#Container.DataItem("UserName")%></td>
            <td><asp:Button ID="btnVeiw" runat="server" Text="Edit Levels" OnClick="btnVeiw_click"/>
            <asp:HiddenField ID="UserID" runat="server" Value='<%#Container.DataItem("UserID")%>' />
            </td>
</tr>
</AlternatingItemTemplate>
<FooterTemplate>
</table>
</FooterTemplate>

</asp:Repeater>  
                <asp:Button ID="btnBack" runat="server" OnClick="btnBack_Click" Text="<< Back to Search"
                    Width="120px" />
            </asp:View>            
        </asp:MultiView> 
</ContentTemplate>        
<Triggers>
<asp:AsyncPostBackTrigger ControlID="opID" EventName="CheckedChanged"/>                        
</Triggers>
</asp:UpdatePanel>        
           
    </form>
        
</body>
</html>

