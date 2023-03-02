<%@ Page Language="VB" Inherits="BasePage"%>

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
    Protected Sub frPopUp_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        lblFRStatus.Visible = False
        DDLFRStatus.Visible = True
        FRPopUp.Visible = False
    End Sub
    Protected Sub APopUp_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        lblAStatus.Visible = False
        DDLAStatus.Visible = True
        APopUp.Visible = False
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim ConString As String
        Dim SQLString As String
        
        ConString = System.Configuration.ConfigurationManager.AppSettings("ETSCon")
        Dim oConn As New Data.SqlClient.SqlConnection
        oConn.ConnectionString = ConString
        Try
            oConn.Open()
            If Not IsPostBack Then
                SQLString = "select 'Please select' as Description, 0 as LevelNo union select Description,LevelNo from tblProductionLevels where Type=1 and (ContractorID='" & Session("ContractorID") & "') order by levelNo"
                Dim objDA As New System.Data.SqlClient.SqlDataAdapter(SQLString, oConn)
                Dim objDS As New System.Data.DataSet()
                objDA.Fill(objDS)
                       
                DDLIPStatus.DataSource = objDS
                DDLIPStatus.DataTextField = "Description"
                DDLIPStatus.DataValueField = "LevelNo"
                DDLIPStatus.DataBind()
            
                DDLPPStatus.DataSource = objDS
                DDLPPStatus.DataTextField = "Description"
                DDLPPStatus.DataValueField = "LevelNo"
                DDLPPStatus.DataBind()
           
                DDLFRStatus.DataSource = objDS
                DDLFRStatus.DataTextField = "Description"
                DDLFRStatus.DataValueField = "LevelNo"
                DDLFRStatus.DataBind()
            
                DDLAStatus.DataSource = objDS
                DDLAStatus.DataTextField = "Description"
                DDLAStatus.DataValueField = "LevelNo"
                DDLAStatus.DataBind()
            
                objDA.Dispose()
                objDS.Dispose()
            
                SQLString = "select 'Please select' as Description, 0 as LevelNo union select Description,LevelNo from tblProductionLevels where Type=0 and (ContractorID='" & Session("ContractorID") & "') order by levelNo"
                objDA = New System.Data.SqlClient.SqlDataAdapter(SQLString, oConn)
                objDS = New System.Data.DataSet()
                objDA.Fill(objDS)
            
                DDLSIPStatus.DataSource = objDS
                DDLSIPStatus.DataTextField = "Description"
                DDLSIPStatus.DataValueField = "LevelNo"
                DDLSIPStatus.DataBind()
            
                DDLSPPStatus.DataSource = objDS
                DDLSPPStatus.DataTextField = "Description"
                DDLSPPStatus.DataValueField = "LevelNo"
                DDLSPPStatus.DataBind()
            
                DDLSFRStatus.DataSource = objDS
                DDLSFRStatus.DataTextField = "Description"
                DDLSFRStatus.DataValueField = "LevelNo"
                DDLSFRStatus.DataBind()
            
                DDLSAStatus.DataSource = objDS
                DDLSAStatus.DataTextField = "Description"
                DDLSAStatus.DataValueField = "LevelNo"
                DDLSAStatus.DataBind()
            
               
                SQLString = "SELECT RSS.ISContractor, PL.Description, RSS.PreProduction as LevelNo,1 as PP " & _
                            "FROM tblRSSStatus AS RSS INNER JOIN " & _
                            "tblProductionLevels AS PL ON RSS.PreProduction = PL.LevelNo where RSS.ContractorID='" & Session("ContractorID") & "' and PL.ContractorID='" & Session("ContractorID") & "'" & _
                            "union  " & _
                            "SELECT RSS.ISContractor, PL.Description, RSS.IntialProduction as LevelNo, 0 as PP " & _
                            "FROM tblRSSStatus AS RSS INNER JOIN " & _
                            "tblProductionLevels AS PL ON RSS.IntialProduction = PL.LevelNo  where RSS.ContractorID='" & Session("ContractorID") & "' and PL.ContractorID='" & Session("ContractorID") & "'" & _
                            "union  " & _
                            "SELECT RSS.ISContractor, PL.Description, RSS.ForceRouting as LevelNo, 2 as PP " & _
                            "FROM tblRSSStatus AS RSS INNER JOIN " & _
                            "tblProductionLevels AS PL ON RSS.ForceRouting = PL.LevelNo  where RSS.ContractorID='" & Session("ContractorID") & "' and PL.ContractorID='" & Session("ContractorID") & "'" & _
                            "union  " & _
                            "SELECT RSS.ISContractor, PL.Description, RSS.Audit as LevelNo, 3 as PP " & _
                            "FROM tblRSSStatus AS RSS INNER JOIN " & _
                            "tblProductionLevels AS PL ON RSS.Audit = PL.LevelNo  where RSS.ContractorID='" & Session("ContractorID") & "' and PL.ContractorID='" & Session("ContractorID") & "'"
                'Response.Write(SQLString)
                Dim oCommand As New Data.SqlClient.SqlCommand(SQLString, oConn)
                Dim oRec As Data.SqlClient.SqlDataReader = oCommand.ExecuteReader()
                'oRec.Read()
                If oRec.HasRows Then
                    'lblPPStatus.Text = oRec("ISContractor").ToString & " " & oRec("PP").ToString & " " & oRec("LevelNo") & "<BR>"
                    Do While oRec.Read
                        If oRec("ISContractor") Then
                            If oRec("PP").ToString = "1" Then
                                lblPPStatus.Text = oRec("Description").ToString
                                hdnPPStatus.Value = oRec("LevelNo").ToString
                            ElseIf oRec("PP").ToString = "0" Then
                                lblIPStatus.Text = oRec("Description").ToString
                                hdnIPStatus.Value = oRec("LevelNo").ToString
                            ElseIf oRec("PP").ToString = "2" Then
                                lblFRStatus.Text = oRec("Description").ToString
                                hdnFRStatus.Value = oRec("LevelNo").ToString
                            Else
                                lblAStatus.Text = oRec("Description").ToString
                                hdnAStatus.Value = oRec("LevelNo").ToString
                            End If
                        Else
                            If oRec("PP").ToString = "1" Then
                                lblSPPStatus.Text = oRec("Description").ToString
                                hdnSPPStatus.Value = oRec("LevelNo").ToString
                            ElseIf oRec("PP").ToString = "0" Then
                                lblSIPStatus.Text = oRec("Description").ToString
                                hdnSIPStatus.Value = oRec("LevelNo").ToString
                            ElseIf oRec("PP").ToString = "2" Then
                                lblSFRStatus.Text = oRec("Description").ToString
                                hdnSFRStatus.Value = oRec("LevelNo").ToString
                            Else
                                lblSAStatus.Text = oRec("Description").ToString
                                hdnSAStatus.Value = oRec("LevelNo").ToString
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
    Protected Sub DDLfrStatus_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        lblFRStatus.Text = DDLFRStatus.SelectedItem.Text
        hdnFRStatus.Value = DDLFRStatus.SelectedValue
        DDLFRStatus.Visible = False
        FRPopUp.Visible = True
        lblFRStatus.Visible = True
    End Sub
    Protected Sub DDLAStatus_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        lblAStatus.Text = DDLAStatus.SelectedItem.Text
        hdnAStatus.Value = DDLAStatus.SelectedValue
        DDLAStatus.Visible = False
        APopUp.Visible = True
        lblAStatus.Visible = True
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
    Protected Sub SFRPopUp_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        lblSFRStatus.Visible = False
        DDLSFRStatus.Visible = True
        SFRPopUp.Visible = False
    End Sub
    Protected Sub saPopUp_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        lblSAStatus.Visible = False
        DDLSAStatus.Visible = True
        SAPopUp.Visible = False
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
    Protected Sub DDLSFRStatus_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        lblSFRStatus.Text = DDLSFRStatus.SelectedItem.Text
        hdnSFRStatus.Value = DDLSFRStatus.SelectedValue
        DDLSFRStatus.Visible = False
        SFRPopUp.Visible = True
        lblSFRStatus.Visible = True
    End Sub
    Protected Sub DDLSAStatus_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        lblSAStatus.Text = DDLSAStatus.SelectedItem.Text
        hdnSAStatus.Value = DDLSAStatus.SelectedValue
        DDLSAStatus.Visible = False
        SAPopUp.Visible = True
        lblSAStatus.Visible = True
    End Sub
    

    Protected Sub btnSubmit_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim goAhead As Boolean = True
        If hdnPPStatus.Value = "0" Then
            iResponse.Text = "Contractors Pre-Production Level can not be blank"
            goAhead = False
        End If
        If hdnIPStatus.Value = "0" Then
            iResponse.Text = "Contractors Initial Production Level can not be blank"
            goAhead = False
        End If
        If hdnFRStatus.Value = "0" Then
            iResponse.Text = "Contractors Default Force Routing Level can not be blank"
            goAhead = False
        End If
        If hdnAStatus.Value = "0" Then
            iResponse.Text = "Contractors Audit Level can not be blank"
            goAhead = False
        End If
        If hdnSPPStatus.Value = "0" Then
            iResponse.Text = "Sub-Contractors Pre-Production Level can not be blank"
            goAhead = False
        End If
        If hdnSIPStatus.Value = "0" Then
            iResponse.Text = "Sub-Contractors Pre-Production Level can not be blank"
            goAhead = False
        End If
        If hdnSFRStatus.Value = "0" Then
            iResponse.Text = "Sub-Contractors Default Force Routing Level can not be blank"
            goAhead = False
        End If
        If hdnSAStatus.Value = "0" Then
            iResponse.Text = "Sub-Contractors Audit Level can not be blank"
            goAhead = False
        End If
        
        If goAhead Then
            Dim ConString As String
            Dim SQLString As String
            Dim thisTransaction As Data.SqlClient.SqlTransaction
            ConString = System.Configuration.ConfigurationManager.AppSettings("ETSCon")
            Dim oConn As New Data.SqlClient.SqlConnection
            Try
                oConn.ConnectionString = ConString
                oConn.Open()
                thisTransaction = oConn.BeginTransaction()
                SQLString = "delete from tblRSSStatus where contractorid='" & Session("ContractorID").ToString & "'"
                Dim oCommand As New Data.SqlClient.SqlCommand(SQLString, oConn)
                oCommand.Transaction = thisTransaction
                oCommand.ExecuteNonQuery()
                
                SQLString = "insert into tblRSSStatus(ISContractor,PreProduction,IntialProduction,ForceRouting,Audit,ContractorID) " & _
                            "values(1," & hdnPPStatus.Value & "," & hdnIPStatus.Value & "," & hdnFRStatus.Value & "," & hdnAStatus.Value & ",'" & Session("ContractorID").ToString & "')"
                oCommand = New Data.SqlClient.SqlCommand(SQLString, oConn)
                oCommand.Transaction = thisTransaction
                oCommand.ExecuteNonQuery()
            
                SQLString = "insert into tblRSSStatus(ISContractor,PreProduction,IntialProduction,ForceRouting,Audit,ContractorID) " & _
                            "values(0," & hdnSPPStatus.Value & "," & hdnSIPStatus.Value & "," & hdnSFRStatus.Value & "," & hdnSAStatus.Value & ",'" & Session("ContractorID").ToString & "')"
                oCommand = New Data.SqlClient.SqlCommand(SQLString, oConn)
                oCommand.Transaction = thisTransaction
                oCommand.ExecuteNonQuery()
                
                thisTransaction.Commit()
                oConn.Close()
                iResponse.Text = "Settings updated successfully"
            Catch ex As Exception
                iResponse.Text = "Error occured while updating Settings " & ex.Message
                If Not thisTransaction Is Nothing Then
                    thisTransaction.Rollback()
                End If
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
    <table style="left: 2%; position: absolute; top: 10%;" border=1>           
            <tr>
                <td  class="HeaderDiv" colspan=4>
                    Production Levels Initial Status - Contractor
                </td>
                <%--<td class="HeaderDiv" colspan="1" style="width: 189px">
                </td><td class="HeaderDiv" colspan="1" style="width: 200px">
                </td>--%>
                
            </tr>            
            <tr>
                <td  class="SMSelected" style="width: 25%">
                Pre-Production Level</td>
                <td  class="SMSelected" style="width: 25%">                    Initial Production Level</td>
                <td class="SMSelected" style="width: 25%">
                    Default Force Routing Level</td><td class="SMSelected" style="width: 200px">
                        Audit Level</td>
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
                <td style="width: 189px">
                    <asp:Label ID="lblFRStatus" runat="server"></asp:Label>
                    <asp:Button ID="FRPopUp" runat="server" Text="..." ToolTip="Click here to change Status" OnClick="FRPopUp_Click" />
                    <asp:DropDownList ID="DDLFRStatus" runat="server" Visible=false OnSelectedIndexChanged="DDLFRStatus_SelectedIndexChanged" AutoPostBack="true">
                    </asp:DropDownList>
                    <asp:HiddenField ID="hdnFRStatus" runat="server" />
                </td><td style="width: 200px">
                    <asp:Label ID="lblAStatus" runat="server"></asp:Label>
                    <asp:Button ID="APopUp" runat="server" Text="..." ToolTip="Click here to change Status" OnClick="APopUp_Click" />
                    <asp:DropDownList ID="DDLAStatus" runat="server" Visible=false OnSelectedIndexChanged="DDLAStatus_SelectedIndexChanged" AutoPostBack="true">
                    </asp:DropDownList>
                    <asp:HiddenField ID="hdnAStatus" runat="server" />
                </td>
            </tr>
            <tr>
            <td colspan=4>&nbsp</td>
            </tr>
            <tr>
                <td  class="HeaderDiv" colspan=4>
                    Production Levels Initial Status - Sub-Contractor
                </td>                
                
            </tr>            
            <tr>
                <td  class="SMSelected" style="width: 210px">
                Pre-Production Level</td>
                <td  class="SMSelected" style="width: 253px">                    Initial Production Level</td>
                <td class="SMSelected" style="width: 189px">
                    Default Force Routing Level</td><td class="SMSelected" style="width: 200px">
                        Audit Level</td>
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
                <td style="width: 189px">
                    <asp:Label ID="lblSFRStatus" runat="server"></asp:Label><asp:Button ID="SFRPopUp" runat="server" Text="..." ToolTip="Click here to change Status" OnClick="SFRPopUp_Click" /><asp:DropDownList ID="DDLSFRStatus" runat="server" Visible=false OnSelectedIndexChanged="DDLSFRStatus_SelectedIndexChanged" AutoPostBack="true">
                        </asp:DropDownList><asp:HiddenField ID="hdnSFRStatus" runat="server" />
                </td><td style="width: 200px">
                    <asp:Label ID="lblSAStatus" runat="server"></asp:Label><asp:Button ID="SAPopUp" runat="server" Text="..." ToolTip="Click here to change Status" OnClick="SAPopUp_Click" /><asp:DropDownList ID="DDLSAStatus" runat="server" Visible=false OnSelectedIndexChanged="DDLSAStatus_SelectedIndexChanged" AutoPostBack="true">
                        </asp:DropDownList><asp:HiddenField ID="hdnSAStatus" runat="server" />
                </td>
            </tr>
            <tr>
            <td colspan=4>
            <asp:Button ID="btnSubmit" runat="server" Text="Save Changes" OnClick="btnSubmit_Click" />
                <asp:Literal ID="iResponse" runat="server"></asp:Literal>
            </td>
                
            </tr>                
            </table>
    </form>
</body>
</html>
