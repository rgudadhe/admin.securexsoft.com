<%@ Page Language="VB" %>
<%@ Register
    Assembly="AjaxControlToolkit"
    Namespace="AjaxControlToolkit"
    TagPrefix="ajaxToolkit" %>
<%@ Register
    Assembly="HHM.RIAnimation"
    Namespace="HHM.RIAnimation"
    TagPrefix="ETSAnim" %>



<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 1.0 Transitional/EN">

<script runat="server" type="text/VB"> 
    Dim GlobalDS As Data.DataTable
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs)
        
       
        Dim iStr As New StringBuilder
        
        Try
            If Not IsPostBack Then
                If Request("SettingID") <> "" Then
                    hdnSetting.Value = Request("SettingID")
                    lblMessage.Text = "File Import Process - <b>" & UCase(Request("SettingName"))
                    hdnSettingName.Value = UCase(Request("SettingName"))
                End If
                rptCon.Visible = False
                'Try
                                
                If Request("Res") = "1" Then
                    Dim HashT As New Hashtable
                    Dim arrX As New ArrayList
                    Dim HashCount As Integer
                    Dim i As Integer
                    If IsNumeric(Session("HashCount")) Then
                        HashCount = CInt(Session("HashCount"))
                    End If
                    For i = 0 To HashCount
                        Dim HashRPT As New Hashtable
                        HashRPT = Session("HashT" & i.ToString)
                        If String.IsNullOrEmpty(HashRPT("ANameID").ToString) = False And HashRPT("ANameID").ToString <> "55555555-5555-5555-5555-555555555555" Then
                            iStr.Append("'" & HashRPT("ANameID").ToString & "'" & ",")
                        End If
                        arrX.Add(HashRPT)
                    Next
                    
                    HashT = Session("HashT")
                    iStr.Append("'" & HashT("ANameID").ToString & "'")
                    arrX.Add(HashT)
                    rptCon.DataSource = arrX
                    rptCon.DataBind()
                    rptCon.Visible = True
                Else
                    Session("DTObj") = Nothing
                    Dim clsRSSS As New ETS.BL.RSSSettings
                    Dim DSRSSS As Data.DataSet = clsRSSS.getRSSSettings(hdnSetting.Value.ToString)
                    
                    rptCon.DataSource = DSRSSS
                    rptCon.DataBind()
                    If rptCon.Items.Count > 0 Then
                        rptCon.Visible = True
                    Else
                        rptCon.Visible = False
                       
                    End If
                    DSRSSS.Dispose()
                    clsRSSS = Nothing
                End If
             
            End If
            If Not Page.IsPostBack Then
                              
                If Session("DTObj") Is Nothing Then
                    Dim clsAtt As New ETS.BL.Attributes
                
                    With clsAtt
                        .ContractorID = Session("ContractorID")
                    End With
                    GlobalDS = clsAtt.GetCombineAttributeList(Request("SettingID").ToString)
                    Session("DTObj") = GlobalDS
                Else
                    GlobalDS = CType(Session("DTObj"), Data.DataTable)
                End If
                
                AName.DataSource = GlobalDS
                AName.DataTextField = "Caption"
                AName.DataValueField = "AttributeID"
                AName.DataBind()
                
                
            End If
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub
    
           
    
    Protected Sub btnAdd_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Try
            Dim txt As New TextBox
            Dim DDL As New DropDownList
            Dim x As Hashtable
            x = New Hashtable
            Dim btn As Button = CType(sender, Button)
            DDL = btn.Parent.FindControl("AName")
            If Not DDL.SelectedValue.ToString = "55555555-5555-5555-5555-555555555555" Then
                GlobalDS = CType(Session("DTObj"), Data.DataTable)
                Dim DR() As Data.DataRow = GlobalDS.Select("AttributeID='" & DDL.SelectedValue.ToString & "'")
                GlobalDS.Rows.Remove(DR(0))
                Session("DTObj") = GlobalDS
            End If
            x.Add("AName", DDL.SelectedItem)
            x.Add("ANameID", DDL.SelectedValue)
            DDL = btn.Parent.FindControl("AType")
            x.Add("AType", DDL.SelectedItem)
            txt = btn.Parent.FindControl("AFormat")
            x.Add("AFormat", txt.Text)
            txt = btn.Parent.FindControl("ADelr")
            x.Add("ADelr", txt.Text)
            Session.Add("HashT", x)
            Dim i As Integer = 0
            For Each ctl As RepeaterItem In rptCon.Items
                Dim rptH As New Hashtable
                Dim tx As Label
                Dim hdn As HiddenField
                tx = ctl.FindControl("AName")
                If Not String.IsNullOrEmpty(tx.Text) Then
                    rptH.Add("AName", tx.Text)
                End If
                hdn = ctl.FindControl("ANameID")
                If Not String.IsNullOrEmpty(hdn.Value) Then
                    rptH.Add("ANameID", hdn.Value)
                End If
            
                tx = ctl.FindControl("AType")
                If Not String.IsNullOrEmpty(tx.Text) Then
                    rptH.Add("AType", tx.Text)
                End If
                txt = ctl.FindControl("AFormat")
                If Not String.IsNullOrEmpty(txt.Text) Then
                    rptH.Add("AFormat", txt.Text)
                End If
                txt = ctl.FindControl("ADelr")
                If Not String.IsNullOrEmpty(txt.Text) Then
                    rptH.Add("ADelr", txt.Text)
                End If
                Session.Add("HashT" & i.ToString, rptH)
                i = i + 1
            Next
            Session.Add("HashCount", (i - 1).ToString)
            Response.Redirect("RSSSettings.aspx?Res=1&SettingID=" & hdnSetting.Value & "&SettingName=" & hdnSettingName.Value.ToString, True)
        Catch ex As Exception
            lblMessage.Text = ex.Message
        End Try
    End Sub

    Protected Sub AType_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim DDL As DropDownList
        DDL = CType(sender, DropDownList)
        If DDL.SelectedItem.Text = "Date" Or DDL.SelectedItem.Text = "Time" Then
            AFormat.Enabled = True
            AFormat.BackColor = Drawing.Color.Empty
        Else
            AFormat.Enabled = False
            AFormat.BackColor = Drawing.Color.LightGray
        End If
    End Sub

    Protected Sub btnSubmit_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Try
            
        
            Dim Order As Integer
               
            Dim DT As New Data.DataTable
            DT.Columns.Add("SettingID", GetType(System.String))
            DT.Columns.Add("AttributeID", GetType(System.String))
            DT.Columns.Add("Type", GetType(System.String))
            DT.Columns.Add("Format", GetType(System.String))
            DT.Columns.Add("Deleminator", GetType(System.String))
            DT.Columns.Add("Order", GetType(System.Int32))
            For Each ctl As RepeaterItem In rptCon.Items
                Dim ANameID As String
                Dim AType As String
                Dim AFormat As String
                Dim ADelr As String
                Dim hdn As New HiddenField
                Dim lbl As New Label
                hdn = ctl.FindControl("ANameID")
                ANameID = hdn.Value.ToString
                lbl = ctl.FindControl("AType")
                AType = lbl.Text
                'lbl = ctl.FindControl("AFormat")
                Dim txt As TextBox = ctl.FindControl("AFormat")
                AFormat = txt.Text
                txt = ctl.FindControl("ADelr")
                ADelr = txt.Text.ToString
                If ADelr = "" Then
                    ADelr = String.Empty
                End If
                If Not String.IsNullOrEmpty(ANameID) Then
                    
                    Dim DR As Data.DataRow = DT.NewRow
                    DR("SettingID") = hdnSetting.Value.ToString
                    DR("AttributeID") = ANameID
                    DR("Type") = AType
                    DR("Format") = AFormat
                    DR("Deleminator") = ADelr
                    DR("Order") = Order
                    DT.Rows.Add(DR)
                    Order = Order + 1
                End If
            Next
                        
            Dim clsRSSS As New ETS.BL.RSSSettings
            clsRSSS.SettingID = hdnSetting.Value.ToString
            'lblMessage.Text = lblMessage.Text & " " & clsRSSS.ADD_RSSSettings(DT)
            
            If clsRSSS.ADD_RSSSettings(DT) Then
                lblMessage.Text = "Settings has been updated successfully!"
            Else
                lblMessage.Text = "Failed saving settings!"
            End If
            
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub
    Protected Sub btnReset_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim clsRSSS As New ETS.BL.RSSSettings
        With clsRSSS
            .SettingID = hdnSetting.Value
            .DeleteRSSSeting()
        End With
        clsRSSS = Nothing
        Response.Redirect("RSSSettings.aspx?SettingID=" & hdnSetting.Value & "&SettingName=" & hdnSettingName.Value.ToString, True)
        
    End Sub
    
   
</script>

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>RSS Settings</title>
    <link href= "../App_Themes/Css/styles.css" type="text/css" rel="stylesheet" />
    <link href= "../App_Themes/Css/Common.css" type="text/css" rel="stylesheet" />
    <script type='text/javascript'>
    function cancelClick() {
        var label = $get('ctl00_SampleContent_Label1');
        
    }
    </script>    
</head>
<body>
    <form id="form1" runat="server">
    <ajaxToolkit:ToolkitScriptManager runat="Server" EnablePartialRendering="true" ID="ScriptManager1" />           
        <div id="body">
        <div id="cap"></div>
        <div id="main">
        <h1>File Import Process</h1>
        <asp:Panel ID="Panel2" runat="server" HorizontalAlign="Left">
                    <asp:HiddenField ID="hdnSetting" runat="server" /> 
                    <asp:HiddenField ID="hdnSettingName" runat="server" />                                       
                    <asp:Label id="lblMessage" CssClass="common" runat="server"/>
            <table width="100%"> 
                                <tr><td style="width: 443px; height: 160px;border:0" >
                                       <asp:Repeater ID="rptCon" runat="server">
                                        <HeaderTemplate>              
                                        <table> 
                                            <tr>
                                            <td class="alt1">Attribute Name</td>
                                            <td class="alt1">Type</td>
                                            <td class="alt1">Format</td>                        
                                            <td class="alt1">Deliminator</td>                        
                                            </tr>
                                        </HeaderTemplate>
                                        <ItemTemplate>  
                                            <tr>
                                            <td><asp:Label ID="AName" runat="server" Text='<%#Container.DataItem("AName")%>'></asp:Label>
                                                 <asp:HiddenField ID="ANameID" runat="server" Value='<%#Container.DataItem("ANameID")%>'/>
                                             </td>            
                                             <td><asp:Label ID="AType" runat="server" Text='<%#Container.DataItem("AType")%>'></asp:Label></td>          
                                                <td><asp:TextBox ID="AFormat" runat="server" Text='<%#Container.DataItem("AFormat")%>' Enabled='<%#iif(Container.DataItem("AType").tostring="Date",true,iif(Container.DataItem("AType").tostring="Time",true,false))%>' Font-Bold="true" Width="90px"></asp:TextBox></td>          
                                                <td><asp:TextBox ID="ADelr" runat="server" Text='<%#Container.DataItem("ADelr")%>' Width="20PX" MaxLength="1"></asp:TextBox></td>
                                             </tr>
                                         </ItemTemplate>
                                        <AlternatingItemTemplate>
                                                <tr bgcolor="#cccccc">
                                                <td><asp:Label ID="AName" runat="server" Text='<%#Container.DataItem("AName")%>'></asp:Label>
                                                <asp:HiddenField ID="ANameID" runat="server" Value='<%#Container.DataItem("ANameID")%>'/></td>
                                                <td><asp:Label ID="AType" runat="server" Text='<%#Container.DataItem("AType")%>'></asp:Label></td>          
                                                <td><asp:TextBox ID="AFormat" runat="server" Text='<%#Container.DataItem("AFormat")%>' Enabled='<%#iif(Container.DataItem("AType").tostring="Date",true,iif(Container.DataItem("AType").tostring="Time",true,false))%>'  Font-Bold="true" Width="90px"></asp:TextBox></td>          
                                                <td><asp:TextBox ID="ADelr" runat="server" Text='<%#iif(isdbnull(Container.DataItem("ADelr")),string.empty,Container.DataItem("ADelr"))%>' Width="20PX" MaxLength="1"></asp:TextBox></td>
                                                </tr>
                                        </AlternatingItemTemplate>
                                        <FooterTemplate>
                                                <tr>
                                                <td colspan="2">
                                                    <center>
                                                    <asp:Button ID="btnSubmit" CssClass="button" runat="server" Text="Set Mapping" OnClick="btnSubmit_Click" CausesValidation="false" CommandName="Confirmation" OnClientClick="return confirm('Are you certain you want to set mappings?');"/>                                                              
                                                    </center>
                                                </td>
                                                <td colspan="2">
                                                <center>
                                                <asp:Button ID="btnReset" CssClass="button" runat="server" Text="Re-set Mapping" OnClick="btnReset_Click" CausesValidation="false" CommandName="Confirmation" OnClientClick="return confirm('Are you certain you want to re-set mappings?');"/>
                                                </center>
                                                
                                            </td></tr>
                                            </table>                                            
                                        </FooterTemplate>
                                        </asp:Repeater> 
                                    </td>
                                  </tr>
                            </table>
                            <hr />
                            <asp:Panel ID="Panel1" runat="server" Width="100%">        
                                <table align="left">
                                <tr>
                                    <td class="alt1">Select Attributee</td>
                                    <td class="alt1">Select Type</td>            
                                    <td class="alt1">Select Format</td>            
                                    <td class="alt1">Select Deliminator</td>                        
                                </tr>
                                <tr>
                                    <td>
                                        <asp:DropDownList runat="server" CssClass="common" ID="AName" >            
                                        </asp:DropDownList>
                                    </td>              
                                    <td style="height: 26px; width: 118px;">            
                                    <asp:DropDownList runat="server" ID="AType" OnSelectedIndexChanged="AType_SelectedIndexChanged" CssClass="common" AutoPostBack="True">            
                                        <asp:ListItem Selected="True">String</asp:ListItem>
                                        <asp:ListItem>Number</asp:ListItem>
                                        <asp:ListItem>Date</asp:ListItem>
                                        <asp:ListItem>Time</asp:ListItem>
                                    </asp:DropDownList>           
                                    </td>
                                    <td >
                                        <asp:TextBox ID="AFormat" runat="server" Enabled="false" CssClass="common" ></asp:TextBox>
                                    </td>           
                                    <td><asp:TextBox ID="ADelr" runat="server" Columns="1" CssClass="common"  MaxLength="1" ></asp:TextBox></td>            
           
                                    </tr>
                                    <tr>
                                    <td colspan="4">
                                        <center>
                                    <asp:Button ID="btnAdd" runat="server" CssClass="button" Text="Map Element" OnClick="btnAdd_Click" />
                                    </center>
                                    <asp:RegularExpressionValidator  Display="None" ID="RegularExpressionValidator1" runat="server" ControlToValidate="ADelr"
                                       ErrorMessage="Invalid Deliminator" ValidationExpression=".*[-_@#$%^&].*"></asp:RegularExpressionValidator><br />
                                        </td>
                                    </tr>
                                    </table>
                            </asp:Panel> 
        </asp:Panel>
            
             </div> 
             </div> 
<asp:ValidationSummary ID="ValidationSummary1" runat="server" HeaderText="Following error occurs:" ShowMessageBox="true" DisplayMode="BulletList" ShowSummary="False" /> </form>        
</body>
</html>


