<%@ Page Language="VB" Inherits="BasePage"%>
<%@ Register
    Assembly="AjaxControlToolkit"
    Namespace="AjaxControlToolkit"
    TagPrefix="ajaxToolkit" %>    
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 1.0 Transitional/EN">

<script runat="server" type="text/VB">
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs)
        If Not IsPostBack Then
            Dim ConString As String
            Dim SQLString As String
            ConString = System.Configuration.ConfigurationManager.AppSettings("ETSCon")
            Dim oConn As New Data.SqlClient.SqlConnection
            Try
                oConn.ConnectionString = ConString
                oConn.Open()
                'Session("IsOwner") = "True"
                SQLString = "SELECT sc.ContractorID AS SubConID, sc.ContractorName AS SubConName, C.ContractorID as ConID, C.ContractorName as ConName " & _
                            ",CPL.LevelName as ConLevelName,CPL.LevelNo as ConNo " & _
                            ",SPL.LevelName as SubConLevelName,SPL.LevelNo as SubConNo " & _
                            "from (SELECT ContractorID , ContractorName FROM tblContractor where ParentID= '" & Session("ContractorID") & "') as sC " & _
                            "cross join (SELECT ContractorID , ContractorName  FROM tblContractor where ContractorID= '" & Session("ContractorID").ToString & "') as C " & _
                            "left Outer join tblSbLvlMapping as MapLvl on Sc.ContractorID=MapLvl.SubConID " & _
                            "left outer join (Select LevelNo,LevelName,ContractorID from tblProductionLevels) as CPL on C.ContractorID=CPL.ContractorID and MapLvl.ConLevel=CPL.LevelNo " & _
                            "left outer join (Select LevelNo,LevelName,ContractorID from tblProductionLevels) as SPL on C.ContractorID=SPL.ContractorID and MapLvl.SubLevel=SPL.LevelNo"

                
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
        Else
            
        End If
    End Sub
    Protected Sub Button1_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim btn As Button
        Dim hdn As HiddenField
        Dim strSubConID As String
        Dim SubLevelNo, ConLevelNo As Integer
        
        Try
            btn = CType(sender, Button)
            btn.Enabled = False
            hdn = btn.Parent.FindControl("SubConID")
            strSubConID = hdn.Value
            hdn = btn.Parent.FindControl("hdnSubConNo")
            SubLevelNo = hdn.Value
            hdn = btn.Parent.FindControl("hdnConNo")
            ConLevelNo = hdn.Value
            If strSubConID <> "" Then
                Dim ConString As String
                Dim SQLString As String
                Dim recAffected As Integer
                ConString = System.Configuration.ConfigurationManager.AppSettings("ETSCon")
                Dim oConn As New Data.SqlClient.SqlConnection
                oConn.ConnectionString = ConString
                Try
                    oConn.Open()
                    SQLString = "update tblSbLvlMapping set ConLevel=" & ConLevelNo & ",SubLevel=" & SubLevelNo & " where SubConID='" & strSubConID & "'"
                    Dim oCommand As New Data.SqlClient.SqlCommand(SQLString, oConn)
                    recAffected = oCommand.ExecuteNonQuery()
                    If recAffected > 0 Then
                        Response.Redirect("SubLevelMapping.aspx", True)
                    Else
                        SQLString = "insert into tblSbLvlMapping(SubConID,ConLevel,SubLevel) values('" & strSubConID & "'," & ConLevelNo & "," & SubLevelNo & ")"
                        oCommand = New Data.SqlClient.SqlCommand(SQLString, oConn)
                        recAffected = oCommand.ExecuteNonQuery()
                        Response.Redirect("SubLevelMapping.aspx", True)
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
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub
    Protected Sub DDSubConNo_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim ddList As DropDownList = CType(sender, DropDownList)
        If ddList.SelectedValue <> "" Then
            Dim lbl As Label = ddList.Parent.FindControl("lblSubConName")
            lbl.Text = ddList.SelectedItem.Text
            lbl.Visible = True
            Dim hdn As HiddenField = ddList.Parent.FindControl("hdnSubConNo")
            hdn.Value = ddList.SelectedItem.Value
            Dim btn As Button = ddList.FindControl("Button1")
            btn.Enabled = True
            ddList.SelectedIndex = 0
            ddList.Visible = False
        End If
    End Sub
    Protected Sub DDConNo_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim ddList As DropDownList = CType(sender, DropDownList)
        If ddList.SelectedValue <> "" Then
            Dim lbl As Label = ddList.Parent.FindControl("lblConName")
            lbl.Text = ddList.SelectedItem.Text
            lbl.Visible = True
            Dim hdn As HiddenField = ddList.Parent.FindControl("hdnConNo")
            hdn.Value = ddList.SelectedItem.Value
            Dim btn As Button = ddList.FindControl("Button1")
            btn.Enabled = True
            ddList.SelectedIndex = 0
            ddList.Visible = False
        End If
    End Sub

    Protected Sub iPopUp_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim btn As Button = CType(sender, Button)
        Dim ddlist As DropDownList = btn.Parent.FindControl("DDSubConNo")
        Dim lbl As Label = btn.Parent.FindControl("lblSubConName")
        If Not ddlist.Visible Then
            ddlist.Visible = True
            lbl.Visible = False
            btn.ToolTip = "Click to reset"
        Else
            ddlist.Visible = False
            lbl.Visible = True
            btn.ToolTip = "Click here to change Level for sub-Contracotor"
        End If
    End Sub

    Protected Sub iPopUp1_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim btn As Button = CType(sender, Button)
        Dim ddlist As DropDownList = btn.Parent.FindControl("DDConNo")
        Dim lbl As Label = btn.Parent.FindControl("lblConName")
        If Not ddlist.Visible Then
            ddlist.Visible = True
            lbl.Visible = False
            btn.ToolTip = "Click to reset"
        Else
            ddlist.Visible = False
            lbl.Visible = True
            btn.ToolTip = "Click here to change Level for Contracotor"
        End If
    End Sub
</script>

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>SubContractor Mapping</title>
    <link href= "../App_Themes/Css/styles.css" type="text/css" rel="stylesheet" />
    <link href= "../App_Themes/Css/Common.css" type="text/css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <div id="body">
        <div id="cap"></div>
        <div id="main">
        <h1>SubContractor - Mapping</h1>
<ajaxToolkit:ToolkitScriptManager runat="server" ID="ScriptManager1"/>    
            <asp:Panel ID="Panel1" HorizontalAlign="Left" runat="server">
                <asp:UpdatePanel runat="server" ID="up2" >
        <ContentTemplate>                
        <asp:Repeater ID="rptCon" runat="server" >
         <HeaderTemplate>
<table>
            <tr>
            <td colspan ="2" class="HeaderDiv">Sub-Contractor</td>            
            <td colspan="2" class="HeaderDiv">Contractor</td>            
            <td class="HeaderDiv"></td>            
            </tr>
            <tr>
            <td class="alt1">Name</td>            
            <td class="alt1">Level</td>
            <td class="alt1">Name</td>            
            <td class="alt1">Level</td>
            <td class="alt1">Action</td>
            </tr>
</HeaderTemplate>

<ItemTemplate>
<tr>
            <td class="common"><%#Container.DataItem("SubConName")%><asp:HiddenField runat="server" ID="SubConID" Value='<%#Container.DataItem("SubConID")%>' /> </td>
            <td class="common">            
            <asp:DropDownList ID="DDSubConNo" runat="server" DataSourceID="SqlDataSource1" DataTextField="LevelName" DataValueField="LevelNo" OnSelectedIndexChanged="DDSubConNo_SelectedIndexChanged" AutoPostBack="true" Visible="false" Width="142" Height="22">            
            <asp:ListItem Selected="True" Value="">Please Select</asp:ListItem>
            </asp:DropDownList><asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:ETSConnectionString %>"            
            SelectCommand="SELECT [LevelNo], [LevelName] FROM [tblProductionLevels] WHERE (([Type] = 0) AND ([IsDeleted] = 0 or [IsDeleted] IS NULL) AND ([ContractorID] = @ContractorID)) UNION SELECT  0 AS LevelNo,'Please Select' AS LevelName">            
                <SelectParameters>
                    <asp:SessionParameter Name="ContractorID" SessionField="ContractorID" Type="string" />
                </SelectParameters>
            </asp:SqlDataSource> 
            
            <asp:Label ID="lblSubConName" runat="server" Width="150px" Text='<%#Container.DataItem("SubConLevelName")%>'></asp:Label>            
            <asp:HiddenField ID="hdnSubConNo" runat="server" Value='<%#Container.DataItem("SubConNo")%>'/> 
                <asp:Button CssClass="button" ID="iPopUp" runat="server" Text="..." OnClick="iPopUp_Click" ToolTip="Click here to change Level for sub-Contracotor" />
                
            </td>
            <td class="common"><%#Container.DataItem("ConName")%><asp:HiddenField runat="server" ID="ConID" Value='<%#Container.DataItem("ConID")%>' /> </td>
            <td class="common">            
            <asp:DropDownList ID="DDConNo" runat="server" DataSourceID="SqlDataSource2" DataTextField="LevelName" DataValueField="LevelNo" OnSelectedIndexChanged="DDConNo_SelectedIndexChanged" AutoPostBack="true" Visible="false" Width="142" Height="22">
            <asp:ListItem Selected="True" Value="">Please Select</asp:ListItem>
            </asp:DropDownList><asp:SqlDataSource ID="SqlDataSource2" runat="server" ConnectionString="<%$ ConnectionStrings:ETSConnectionString %>"            
            SelectCommand="SELECT [LevelNo], [LevelName] FROM [tblProductionLevels] WHERE (([Type] = 1) AND ([IsDeleted] = 0 or [IsDeleted] IS NULL) AND ([ContractorID] = @ContractorID)) UNION SELECT  0 AS LevelNo,'Please Select' AS LevelName">            
                <SelectParameters>
                    <asp:SessionParameter Name="ContractorID" SessionField="ContractorID" Type="string" />
                </SelectParameters>
            </asp:SqlDataSource>           
            <asp:Label ID="lblConName" runat="server" Width="150px" Text='<%#Container.DataItem("ConLevelName")%>'></asp:Label>            
            <asp:HiddenField ID="hdnConNo" runat="server" Value='<%#Container.DataItem("ConNo")%>'/>            
                <asp:Button CssClass="button" ID="iPopUp1" runat="server" Text="..." OnClick="iPopUp1_Click" ToolTip="Click here to change Level for Contracotor" />
            </td>   
        <td>
            <asp:Button ID="Button1" runat="server" CssClass="button" Text="Map Levels" OnClick="Button1_Click" Enabled="false"/>
        </td>         
</tr>
</ItemTemplate>
<FooterTemplate>
</table>
</FooterTemplate>
</asp:Repeater> 
</ContentTemplate>        
</asp:UpdatePanel>
            </asp:Panel>

</div> 
</div>  
</form>

</body>
</html>


