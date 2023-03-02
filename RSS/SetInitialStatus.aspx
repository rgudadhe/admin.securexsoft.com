<%@ Page Language="VB" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 1.0 Transitional/EN">
<LINK href= "../styles/Default.css" type="text/css" rel="stylesheet">
<script runat="server">

    Protected Sub PPPopUp_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        lblPPStatus.Visible = False
        DDLPPStatus.Visible = True
        PPPopUp.Visible = False
    End Sub

    Protected Sub IPPopUp_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        lblIPStatus.Visible = False
        DDLIPStatus.Visible = True
        IPPopUp.Visible = False
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim ConString As String
        Dim SQLString As String
        Dim oCommand As New Data.SqlClient.SqlCommand
        Dim oRec As Data.SqlClient.SqlDataReader
        ConString = System.Configuration.ConfigurationManager.AppSettings("ETSCon")
        Dim oConn As New Data.SqlClient.SqlConnection
        oConn.ConnectionString = ConString
        Try
            oConn.Open()
            If Not IsPostBack Then
                SQLString = "select 'Please select' as Description, 0 as LevelNo from tblProductionLevels union select Description,LevelNo from tblProductionLevels where Type=1 order by levelNo"
                oCommand = New Data.SqlClient.SqlCommand(SQLString, oConn)
                oRec = oCommand.ExecuteReader()
                DDLIPStatus.DataSource = oRec
                DDLIPStatus.DataTextField = "Description"
                DDLIPStatus.DataValueField = "LevelNo"
                DDLIPStatus.DataBind()
                oRec.Close()
        
        
                oCommand = New Data.SqlClient.SqlCommand(SQLString, oConn)
                oRec = oCommand.ExecuteReader()
                DDLPPStatus.DataSource = oRec
                DDLPPStatus.DataTextField = "Description"
                DDLPPStatus.DataValueField = "LevelNo"
                DDLPPStatus.DataBind()
                oRec.Close()
            
                SQLString = "select 'Please select' as Description, 0 as LevelNo from tblProductionLevels union select Description,LevelNo from tblProductionLevels where Type=0 order by levelNo"
                oCommand = New Data.SqlClient.SqlCommand(SQLString, oConn)
                oRec = oCommand.ExecuteReader()
                DDLSIPStatus.DataSource = oRec
                DDLSIPStatus.DataTextField = "Description"
                DDLSIPStatus.DataValueField = "LevelNo"
                DDLSIPStatus.DataBind()
                oRec.Close()
            
                oCommand = New Data.SqlClient.SqlCommand(SQLString, oConn)
                oRec = oCommand.ExecuteReader()
                DDLSPPStatus.DataSource = oRec
                DDLSPPStatus.DataTextField = "Description"
                DDLSPPStatus.DataValueField = "LevelNo"
                DDLSPPStatus.DataBind()
                oRec.Close()
               
                SQLString = "SELECT RSS.ISContractor, PL.Description, RSS.PreProduction as LevelNo,1 as PP " & _
                            "FROM tblRSSStatus AS RSS INNER JOIN " & _
                            "tblProductionLevels AS PL ON RSS.PreProduction = PL.LevelNo " & _
                            "union  " & _
                            "SELECT RSS.ISContractor, PL.Description, RSS.IntialProduction as LevelNo, 0 as PP " & _
                            "FROM tblRSSStatus AS RSS INNER JOIN " & _
                            "tblProductionLevels AS PL ON RSS.IntialProduction = PL.LevelNo"


                oCommand = New Data.SqlClient.SqlCommand(SQLString, oConn)
                oRec = oCommand.ExecuteReader()
                oRec.Read()
                If oRec.HasRows Then
                    If oRec("ISContractor") Then
                        If oRec("PP").ToString = "1" Then
                            lblPPStatus.Text = oRec("Description").ToString
                            hdnPPStatus.Value = oRec("LevelNo").ToString
                        Else
                            lblIPStatus.Text = oRec("Description").ToString
                            hdnIPStatus.Value = oRec("LevelNo").ToString
                        End If
                    Else
                        If oRec("PP").ToString = "1" Then
                            lblSPPStatus.Text = oRec("Description").ToString
                            hdnSPPStatus.Value = oRec("LevelNo").ToString
                        Else
                            lblSIPStatus.Text = oRec("Description").ToString
                            hdnSIPStatus.Value = oRec("LevelNo").ToString
                        End If
                    End If
                    'lblPPStatus.Text = oRec("ISContractor").ToString & " " & oRec("PP").ToString & " " & oRec("LevelNo") & "<BR>"
                    Do While oRec.Read
                        If oRec("ISContractor") Then
                            If oRec("PP").ToString = "1" Then
                                lblPPStatus.Text = oRec("Description").ToString
                                hdnPPStatus.Value = oRec("LevelNo").ToString
                            Else
                                lblIPStatus.Text = oRec("Description").ToString
                                hdnIPStatus.Value = oRec("LevelNo").ToString
                            End If
                        Else
                            If oRec("PP").ToString = "1" Then
                                lblSPPStatus.Text = oRec("Description").ToString
                                hdnSPPStatus.Value = oRec("LevelNo").ToString
                            Else
                                lblSIPStatus.Text = oRec("Description").ToString
                                hdnSIPStatus.Value = oRec("LevelNo").ToString
                            End If
                        End If
                        'lblPPStatus.Text = lblPPStatus.Text & oRec("ISContractor").ToString & " " & oRec("PP").ToString & " " & oRec("LevelNo") & "<BR>"
                    Loop
                End If
        
                oRec.Close()
                
            End If
        Catch ex As Exception
            Response.Write(ex.Message)
        Finally
            If oConn.State <> Data.ConnectionState.Closed Then
                oConn.Close()
                oConn = Nothing
            End If
        End Try
    End Sub
    Protected Sub DDLPPStatus_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        lblPPStatus.Text = DDLPPStatus.SelectedItem.Text
        hdnPPStatus.Value = DDLPPStatus.SelectedValue
        DDLPPStatus.Visible = False
        PPPopUp.Visible = True
        lblPPStatus.Visible = True
    End Sub
    Protected Sub DDLIPStatus_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        lblIPStatus.Text = DDLIPStatus.SelectedItem.Text
        hdnIPStatus.Value = DDLIPStatus.SelectedValue
        DDLIPStatus.Visible = False
        IPPopUp.Visible = True
        lblIPStatus.Visible = True
    End Sub

    Protected Sub SPPPopUp_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        lblSPPStatus.Visible = False
        DDLSPPStatus.Visible = True
        SPPPopup.Visible = False
    End Sub

    Protected Sub SIPPopUp_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        lblSIPStatus.Visible = False
        DDLSIPStatus.Visible = True
        SIPPopup.Visible = False
    End Sub
    Protected Sub DDLSPPStatus_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        lblSPPStatus.Text = DDLSPPStatus.SelectedItem.Text
        hdnSPPStatus.Value = DDLSPPStatus.SelectedValue
        DDLSPPStatus.Visible = False
        SPPPopup.Visible = True
        lblSPPStatus.Visible = True
    End Sub
    Protected Sub DDLSIPStatus_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        lblSIPStatus.Text = DDLSIPStatus.SelectedItem.Text
        hdnSIPStatus.Value = DDLSIPStatus.SelectedValue
        DDLSIPStatus.Visible = False
        SIPPopup.Visible = True
        lblSIPStatus.Visible = True
    End Sub

    Protected Sub btnSubmit_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim goAhead As Boolean = True
        If hdnPPStatus.Value = "0" Then
            iResponse.Text = "Contractors Pre-Production status can not be blank"
            goAhead = False
        End If
        If hdnIPStatus.Value = "0" Then
            iResponse.Text = "Contractors Initial Production status can not be blank"
            goAhead = False
        End If
        If hdnSPPStatus.Value = "0" Then
            iResponse.Text = "Sub-Contractors Pre-Production status can not be blank"
            goAhead = False
        End If
        If hdnSIPStatus.Value = "0" Then
            iResponse.Text = "Sub-Contractors Pre-Production status can not be blank"
            goAhead = False
        End If
        If goAhead Then
            Dim ConString As String
            Dim SQLString As String
            ConString = System.Configuration.ConfigurationManager.AppSettings("ETSCon")
            Dim oConn As New Data.SqlClient.SqlConnection
            oConn.ConnectionString = ConString
            Try
                oConn.Open()
                SQLString = "delete from tblRSSStatus"
                Dim oCommand As New Data.SqlClient.SqlCommand(SQLString, oConn)
                oCommand.ExecuteNonQuery()
            
                SQLString = "insert into tblRSSStatus(ISContractor,PreProduction,IntialProduction) " & _
                            "values(1," & hdnPPStatus.Value & "," & hdnIPStatus.Value & ")"
                oCommand = New Data.SqlClient.SqlCommand(SQLString, oConn)
                oCommand.ExecuteNonQuery()
            
                SQLString = "insert into tblRSSStatus(ISContractor,PreProduction,IntialProduction) " & _
                            "values(0," & hdnSPPStatus.Value & "," & hdnSIPStatus.Value & ")"
                oCommand = New Data.SqlClient.SqlCommand(SQLString, oConn)
                oCommand.ExecuteNonQuery()
                iResponse.Text = "Settings updated successfully"
            Catch ex As Exception
                Response.Write(ex.Message)
            Finally
                If oConn.State <> Data.ConnectionState.Closed Then
                    oConn.Close()
                    oConn = Nothing
                End If
            End Try
        End If
    End Sub
</script>

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Untitled Page</title>
</head>
<body>
    <form id="form1" runat="server">
    <table style="left: 2%; width: 718px; position: absolute; top: 10%;" border=1>           
            <tr>
                <td  class="HeaderDiv" colspan=2>
                    FileImport Initial Status - Contractor
                </td>
                <td class="HeaderDiv" colspan="1">
                </td>
                
            </tr>            
            <tr>
                <td  class="SMSelected" style="width: 33.3%">
                Pre-Production Status
                </td>
                <td  class="SMSelected" style="width: 33.3%">                    Initial Production Status
                </td>
                <td class="SMSelected" style="width: 33.3%">
                </td>
            </tr>
            <tr>
                <td style="width: 210px">
                    <asp:Label ID="lblPPStatus" runat="server" ></asp:Label>
                    <asp:Button ID="PPPopUp" runat="server" Text="..." ToolTip="Click here to change Status" OnClick="PPPopUp_Click" />
                    <asp:DropDownList ID="DDLPPStatus" runat="server" Visible=false OnSelectedIndexChanged="DDLPPStatus_SelectedIndexChanged" AutoPostBack="true">
                    </asp:DropDownList>
                    <asp:HiddenField ID="hdnPPStatus" runat="server" />                    
                </td>
                <td style="width: 253px">
                    <asp:Label ID="lblIPStatus" runat="server" ></asp:Label>
                    <asp:Button ID="IPPopUp" runat="server" Text="..." ToolTip="Click here to change Status" OnClick="IPPopUp_Click" />
                    <asp:DropDownList ID="DDLIPStatus" runat="server" Visible=false OnSelectedIndexChanged="DDLIPStatus_SelectedIndexChanged" AutoPostBack="true">
                    </asp:DropDownList>                    
                    <asp:HiddenField ID="hdnIPStatus" runat="server" />
                </td>
                <td>
                </td>
            </tr>
            <tr>
            <td colspan=2>&nbsp</td>
                <td colspan="1">
                </td>
            </tr>
            <tr>
                <td  class="HeaderDiv" colspan=2>
                    FileImport Initial Status - Sub-Contractor
                </td>
                <td class="HeaderDiv" colspan="1">
                </td>
                
            </tr>            
            <tr>
                <td  class="SMSelected" style="width: 210px">
                Pre-Production Status
                </td>
                <td  class="SMSelected" style="width: 253px">                    Initial Production Status
                </td>
                <td class="SMSelected">
                </td>
            </tr>
            <tr>
                <td style="width: 210px">
                    <asp:Label ID="lblSPPStatus" runat="server" ></asp:Label>
                    <asp:Button ID="SPPPopup" runat="server" Text="..." ToolTip="Click here to change Status" OnClick="SPPPopUp_Click" />
                    <asp:DropDownList ID="DDLSPPStatus" runat="server" Visible=false OnSelectedIndexChanged="DDLSPPStatus_SelectedIndexChanged" AutoPostBack="true">
                    </asp:DropDownList>
                    <asp:HiddenField ID="hdnSPPStatus" runat="server" />                    
                </td>
                <td style="width: 253px">
                    <asp:Label ID="lblSIPStatus" runat="server" ></asp:Label>
                    <asp:Button ID="SIPPopup" runat="server" Text="..." ToolTip="Click here to change Status" OnClick="SIPPopUp_Click" />
                    <asp:DropDownList ID="DDLSIPStatus" runat="server" Visible=false OnSelectedIndexChanged="DDLSIPStatus_SelectedIndexChanged" AutoPostBack="true">
                    </asp:DropDownList>                    
                    <asp:HiddenField ID="hdnSIPStatus" runat="server" />
                </td>
                <td>
                </td>
            </tr>
            <tr>
            <td colspan=2>
            <asp:Button ID="btnSubmit" runat="server" Text="Save Changes" OnClick="btnSubmit_Click" />
                <asp:Literal ID="iResponse" runat="server"></asp:Literal>
            </td>
                <td colspan="1">
                </td>
            </tr>                
            </table>
    </form>
</body>
</html>
