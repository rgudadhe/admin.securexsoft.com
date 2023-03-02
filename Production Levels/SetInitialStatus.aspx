<%@ Page Language="VB" Inherits="BasePage"%>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 1.0 Transitional/EN">
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
       
        Try
            
            If Not IsPostBack Then
                Dim sExpr As String
                Dim drRows() As Data.DataRow
                Dim clsPL As New ETS.BL.ProductionLevels
                With clsPL
                    .ContractorID = Session("ContractorID").ToString
                    .Type = True
                    Dim DSCAL As Data.DataSet = .getPLevelList
                    Dim DR As Data.DataRow = DSCAL.Tables(0).NewRow
                    DR("Description") = "Please select"
                    DR("LevelNo") = 0
                    DR("Type") = 1
                    DSCAL.Tables(0).Rows.Add(DR)
                    Dim DT As Data.DataTable = DSCAL.Tables(0).Clone
                    sExpr = "(Type=1)"
                    drRows = DSCAL.Tables(0).Select(sExpr, "LevelNo")
                    For Each rw As Data.DataRow In drRows
                        DT.ImportRow(rw)
                    Next
                    
                    DDLIPStatus.DataSource = DT
                    DDLIPStatus.DataTextField = "Description"
                    DDLIPStatus.DataValueField = "LevelNo"
                    DDLIPStatus.DataBind()
            
                    DDLPPStatus.DataSource = DT
                    DDLPPStatus.DataTextField = "Description"
                    DDLPPStatus.DataValueField = "LevelNo"
                    DDLPPStatus.DataBind()
           
                    DDLFRStatus.DataSource = DT
                    DDLFRStatus.DataTextField = "Description"
                    DDLFRStatus.DataValueField = "LevelNo"
                    DDLFRStatus.DataBind()
            
                    DDLAStatus.DataSource = DT
                    DDLAStatus.DataTextField = "Description"
                    DDLAStatus.DataValueField = "LevelNo"
                    DDLAStatus.DataBind()
                
                    DT.Dispose()
                    'DT.Rows.Clear()
                    'DR = DSCAL.Tables(0).NewRow
                    'DR("Description") = "Please select"
                    'DR("LevelNo") = 0
                    'DR("Type") = 0
                    'DSCAL.Tables(0).Rows.Add(DR)
                    'sExpr = "(Type=0)"
                    'drRows = DSCAL.Tables(0).Select(sExpr, "LevelNo")
                    'For Each rw As Data.DataRow In drRows
                    '    DT.ImportRow(rw)
                    'Next
                    
                    'DDLSIPStatus.DataSource = DT
                    'DDLSIPStatus.DataTextField = "Description"
                    'DDLSIPStatus.DataValueField = "LevelNo"
                    'DDLSIPStatus.DataBind()
            
                    'DDLSPPStatus.DataSource = DT
                    'DDLSPPStatus.DataTextField = "Description"
                    'DDLSPPStatus.DataValueField = "LevelNo"
                    'DDLSPPStatus.DataBind()
            
                    'DDLSFRStatus.DataSource = DT
                    'DDLSFRStatus.DataTextField = "Description"
                    'DDLSFRStatus.DataValueField = "LevelNo"
                    'DDLSFRStatus.DataBind()
            
                    'DDLSAStatus.DataSource = DT
                    'DDLSAStatus.DataTextField = "Description"
                    'DDLSAStatus.DataValueField = "LevelNo"
                    'DDLSAStatus.DataBind()
                    
                    DSCAL.Dispose()
                End With
                
                
                Dim clsRSS As New ETS.BL.RSSStatus
                With clsRSS
                    .ContractorID = Session("ContractorID").ToString
                    .ISContractor = True
                    .getRSSStatus()
                   
                    
                    clsPL = New ETS.BL.ProductionLevels
                    clsPL.ContractorID = Session("ContractorID").ToString
                    clsPL.LevelNo = .PreProduction
                    clsPL.getPLevelDetails()
                    lblPPStatus.Text = clsPL.Description
                    hdnPPStatus.Value = clsPL.LevelNo
                    
                   
                    clsPL = New ETS.BL.ProductionLevels
                    clsPL.ContractorID = Session("ContractorID").ToString
                    clsPL.LevelNo = .IntialProduction
                    clsPL.getPLevelDetails()
                    lblIPStatus.Text = clsPL.Description
                    hdnIPStatus.Value = clsPL.LevelNo
                        
                    clsPL = New ETS.BL.ProductionLevels
                    clsPL.ContractorID = Session("ContractorID").ToString
                    clsPL.LevelNo = .ForceRouting
                    clsPL.getPLevelDetails()
                    lblFRStatus.Text = clsPL.Description
                    hdnFRStatus.Value = clsPL.LevelNo
                        
                    clsPL = New ETS.BL.ProductionLevels
                    clsPL.ContractorID = Session("ContractorID").ToString
                    clsPL.LevelNo = .Audit
                    clsPL.getPLevelDetails()
                    lblAStatus.Text = clsPL.Description
                    hdnAStatus.Value = clsPL.LevelNo
                End With
                'clsRSS = New ETS.BL.RSSStatus
                'With clsRSS
                '    .ContractorID = Session("ContractorID").ToString
                '    .ISContractor = False
                '    .getRSSStatus()
                    
                '    clsPL = New ETS.BL.ProductionLevels
                '    clsPL.ContractorID = Session("ContractorID").ToString
                '    clsPL.LevelNo = .PreProduction
                '    clsPL.getPLevelDetails()
                '    lblSPPStatus.Text = clsPL.Description
                '    hdnSPPStatus.Value = clsPL.LevelNo
                        
                '    clsPL = New ETS.BL.ProductionLevels
                '    clsPL.ContractorID = Session("ContractorID").ToString
                '    clsPL.LevelNo = .IntialProduction
                '    clsPL.getPLevelDetails()
                '    lblSIPStatus.Text = clsPL.Description
                '    hdnSIPStatus.Value = clsPL.LevelNo
                        
                '    clsPL = New ETS.BL.ProductionLevels
                '    clsPL.ContractorID = Session("ContractorID").ToString
                '    clsPL.LevelNo = .ForceRouting
                '    clsPL.getPLevelDetails()
                '    lblSFRStatus.Text = clsPL.Description
                '    hdnSFRStatus.Value = clsPL.LevelNo
                        
                '    clsPL = New ETS.BL.ProductionLevels
                '    clsPL.ContractorID = Session("ContractorID").ToString
                '    clsPL.LevelNo = .Audit
                '    clsPL.getPLevelDetails()
                '    lblSAStatus.Text = clsPL.Description
                '    hdnSAStatus.Value = clsPL.LevelNo
                'End With
                        
                clsPL = Nothing
                clsRSS = Nothing
                                
            End If
        Catch ex As Exception
            Response.Write(ex.Message)
           
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
    
    'Protected Sub SPPPopUp_Click(ByVal sender As Object, ByVal e As System.EventArgs)
    '    lblSPPStatus.Visible = False
    '    DDLSPPStatus.Visible = True
    '    SPPPopup.Visible = False
    'End Sub
    'Protected Sub SIPPopUp_Click(ByVal sender As Object, ByVal e As System.EventArgs)
    '    lblSIPStatus.Visible = False
    '    DDLSIPStatus.Visible = True
    '    SIPPopup.Visible = False
    'End Sub
    'Protected Sub SFRPopUp_Click(ByVal sender As Object, ByVal e As System.EventArgs)
    '    lblSFRStatus.Visible = False
    '    DDLSFRStatus.Visible = True
    '    SFRPopUp.Visible = False
    'End Sub
    'Protected Sub saPopUp_Click(ByVal sender As Object, ByVal e As System.EventArgs)
    '    lblSAStatus.Visible = False
    '    DDLSAStatus.Visible = True
    '    SAPopUp.Visible = False
    'End Sub
    
    
    'Protected Sub DDLSPPStatus_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs)
    '    lblSPPStatus.Text = DDLSPPStatus.SelectedItem.Text
    '    hdnSPPStatus.Value = DDLSPPStatus.SelectedValue
    '    DDLSPPStatus.Visible = False
    '    SPPPopup.Visible = True
    '    lblSPPStatus.Visible = True
    'End Sub
    'Protected Sub DDLSIPStatus_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs)
    '    lblSIPStatus.Text = DDLSIPStatus.SelectedItem.Text
    '    hdnSIPStatus.Value = DDLSIPStatus.SelectedValue
    '    DDLSIPStatus.Visible = False
    '    SIPPopup.Visible = True
    '    lblSIPStatus.Visible = True
    'End Sub
    'Protected Sub DDLSFRStatus_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs)
    '    lblSFRStatus.Text = DDLSFRStatus.SelectedItem.Text
    '    hdnSFRStatus.Value = DDLSFRStatus.SelectedValue
    '    DDLSFRStatus.Visible = False
    '    SFRPopUp.Visible = True
    '    lblSFRStatus.Visible = True
    'End Sub
    'Protected Sub DDLSAStatus_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs)
    '    lblSAStatus.Text = DDLSAStatus.SelectedItem.Text
    '    hdnSAStatus.Value = DDLSAStatus.SelectedValue
    '    DDLSAStatus.Visible = False
    '    SAPopUp.Visible = True
    '    lblSAStatus.Visible = True
    'End Sub
    

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
        'If hdnSPPStatus.Value = "0" Then
        '    iResponse.Text = "Sub-Contractors Pre-Production Level can not be blank"
        '    goAhead = False
        'End If
        'If hdnSIPStatus.Value = "0" Then
        '    iResponse.Text = "Sub-Contractors Pre-Production Level can not be blank"
        '    goAhead = False
        'End If
        'If hdnSFRStatus.Value = "0" Then
        '    iResponse.Text = "Sub-Contractors Default Force Routing Level can not be blank"
        '    goAhead = False
        'End If
        'If hdnSAStatus.Value = "0" Then
        '    iResponse.Text = "Sub-Contractors Audit Level can not be blank"
        '    goAhead = False
        'End If
        
        If goAhead Then
           
            Try
                Dim clsRSS As New ETS.BL.RSSStatus
                'With clsRSS
                '    .ISContractor = False
                '    .PreProduction = hdnSPPStatus.Value
                '    .IntialProduction = hdnSIPStatus.Value
                '    .ForceRouting = hdnSFRStatus.Value
                '    .Audit = hdnSAStatus.Value
                '    .ContractorID = Session("ContractorID").ToString
                'End With
                Dim SubConFieldNames As New StringBuilder  '= clsRSS._FielsNames
                Dim SubConFieldVals As New StringBuilder '= clsRSS._FieldValues
                'clsRSS = Nothing
                'clsRSS = New ETS.BL.RSSStatus
                With clsRSS
                    .ISContractor = True
                    .PreProduction = hdnPPStatus.Value
                    .IntialProduction = hdnIPStatus.Value
                    .ForceRouting = hdnFRStatus.Value
                    .Audit = hdnAStatus.Value
                    .ContractorID = Session("ContractorID").ToString
                End With
                Dim RetVal As Boolean = clsRSS.setInitialStatus(SubConFieldNames, SubConFieldVals)
                If RetVal Then
                    iResponse.Text = "Settings updated successfully"
                Else
                    iResponse.Text = "Failed updating settings"
                End If
               
            Catch ex As Exception
                iResponse.Text = "Error occured while updating Settings " & ex.Message
            End Try
        End If
    End Sub
    
</script>

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Setup Initial Levels</title>
    <link  href= "../App_Themes/Css/styles.css" type="text/css" rel="stylesheet" />
    <link  href= "../App_Themes/Css/Common.css" type="text/css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <div id="body">
        <div id="cap"></div>
        <div id="main">
        <h1>Setup Roles</h1>
        <asp:Panel ID="Panel1" runat="server" HorizontalAlign="Left">
            <table>           
            <tr>
                <td  class="HeaderDiv" colspan="4">
                    Production Levels Initial Status - Contractor
                </td>
                
            </tr>            
            <tr>
                <td  class="alt1" style="width: 25%">
                Pre-Production Level</td>
                <td  class="alt1" style="width: 25%">                    Initial Production Level</td>
                <td class="alt1" style="width: 25%">
                    Default Force Routing Level</td><td class="alt1" style="width: 200px">
                        Audit Level</td>
            </tr>
            <tr>
                <td style="width: 210px">
                    <asp:Label ID="lblPPStatus" runat="server" ></asp:Label>
                    <asp:Button ID="PPPopUp" CssClass="button" runat="server" Text="..." ToolTip="Click here to change Status" OnClick="PPPopUp_Click" />
                    <asp:DropDownList ID="DDLPPStatus" runat="server" Visible="false" OnSelectedIndexChanged="DDLPPStatus_SelectedIndexChanged" AutoPostBack="true">
                    </asp:DropDownList>
                    <asp:HiddenField ID="hdnPPStatus" runat="server" />                    
                </td>
                <td style="width: 253px">
                    <asp:Label ID="lblIPStatus" runat="server" ></asp:Label>
                    <asp:Button ID="IPPopUp" CssClass="button" runat="server" Text="..." ToolTip="Click here to change Status" OnClick="IPPopUp_Click" />
                    <asp:DropDownList ID="DDLIPStatus" runat="server" Visible="false" OnSelectedIndexChanged="DDLIPStatus_SelectedIndexChanged" AutoPostBack="true">
                    </asp:DropDownList>                    
                    <asp:HiddenField ID="hdnIPStatus" runat="server" />
                </td>
                <td style="width: 189px">
                    <asp:Label ID="lblFRStatus" runat="server"></asp:Label>
                    <asp:Button ID="FRPopUp" CssClass="button" runat="server" Text="..." ToolTip="Click here to change Status" OnClick="FRPopUp_Click" />
                    <asp:DropDownList ID="DDLFRStatus" runat="server" Visible="false" OnSelectedIndexChanged="DDLFRStatus_SelectedIndexChanged" AutoPostBack="true">
                    </asp:DropDownList>
                    <asp:HiddenField ID="hdnFRStatus" runat="server" />
                </td><td style="width: 200px">
                    <asp:Label ID="lblAStatus" runat="server"></asp:Label>
                    <asp:Button ID="APopUp" CssClass="button" runat="server" Text="..." ToolTip="Click here to change Status" OnClick="APopUp_Click" />
                    <asp:DropDownList ID="DDLAStatus" runat="server" Visible="false" OnSelectedIndexChanged="DDLAStatus_SelectedIndexChanged" AutoPostBack="true">
                    </asp:DropDownList>
                    <asp:HiddenField ID="hdnAStatus" runat="server" />
                </td>
            </tr>
            <tr>
            <td colspan="4">&nbsp</td>
            </tr>
            <%--<tr>
                <td  class="HeaderDiv" colspan="4">
                    Production Levels Initial Status - Sub-Contractor
                </td>                
                
            </tr>            
            <tr>
                <td  class="alt1" style="width: 210px">
                Pre-Production Level</td>
                <td  class="alt1" style="width: 253px">                    Initial Production Level</td>
                <td class="alt1" style="width: 189px">
                    Default Force Routing Level</td><td class="alt1" style="width: 200px">
                        Audit Level</td>
            </tr>
            <tr>
                <td style="width: 210px">
                    <asp:Label ID="lblSPPStatus" runat="server" ></asp:Label>
                    <asp:Button ID="SPPPopup" CssClass="button" runat="server" Text="..." ToolTip="Click here to change Status" OnClick="SPPPopUp_Click" />
                    <asp:DropDownList ID="DDLSPPStatus" runat="server" Visible="false" OnSelectedIndexChanged="DDLSPPStatus_SelectedIndexChanged" AutoPostBack="true">
                    </asp:DropDownList>
                    <asp:HiddenField ID="hdnSPPStatus" runat="server" />                    
                </td>
                <td style="width: 253px">
                    <asp:Label ID="lblSIPStatus" runat="server" ></asp:Label>
                    <asp:Button ID="SIPPopup" CssClass="button" runat="server" Text="..." ToolTip="Click here to change Status" OnClick="SIPPopUp_Click" />
                    <asp:DropDownList ID="DDLSIPStatus" runat="server" Visible="false" OnSelectedIndexChanged="DDLSIPStatus_SelectedIndexChanged" AutoPostBack="true">
                    </asp:DropDownList>                    
                    <asp:HiddenField ID="hdnSIPStatus" runat="server" />
                </td>
                <td style="width: 189px">
                    <asp:Label ID="lblSFRStatus" runat="server"></asp:Label><asp:Button ID="SFRPopUp" CssClass="button" runat="server" Text="..." ToolTip="Click here to change Status" OnClick="SFRPopUp_Click" /><asp:DropDownList ID="DDLSFRStatus" runat="server" Visible=false OnSelectedIndexChanged="DDLSFRStatus_SelectedIndexChanged" AutoPostBack="true">
                        </asp:DropDownList><asp:HiddenField ID="hdnSFRStatus" runat="server" />
                </td><td style="width: 200px">
                    <asp:Label ID="lblSAStatus" runat="server"></asp:Label><asp:Button ID="SAPopUp" CssClass="button" runat="server" Text="..." ToolTip="Click here to change Status" OnClick="SAPopUp_Click" /><asp:DropDownList ID="DDLSAStatus" runat="server" Visible=false OnSelectedIndexChanged="DDLSAStatus_SelectedIndexChanged" AutoPostBack="true">
                        </asp:DropDownList><asp:HiddenField ID="hdnSAStatus" runat="server" />
                </td>
            </tr>--%>
            <tr>
            <td colspan="4">
            <asp:Button ID="btnSubmit" runat="server" Text="Save Changes" CssClass="button" OnClick="btnSubmit_Click" />
                <asp:Literal ID="iResponse" runat="server"></asp:Literal>
            </td>
                
            </tr>                
            </table>
        </asp:Panel>
        </div> 
        </div> 
    </form>
</body>
</html>
