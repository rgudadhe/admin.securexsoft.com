Imports System.Data
Imports System.Data.SqlClient
Partial Class ExcludeExceptions
    Inherits BasePage
    'Protected Sub BindPhysicianData()
    '    Dim DS As New Data.DataSet

    '    Dim clsPhy As ETS.BL.MModalDMAssignments
    '    Try
    '        clsPhy = New ETS.BL.MModalDMAssignments

    '        DS = clsPhy.GetPhysicianListByDM(DDLAcc.SelectedValue)

    '        If DS.Tables.Count > 0 Then
    '            If DS.Tables(0).Rows.Count > 0 Then
    '                DDLPhysicians.DataTextField = "PhyName"
    '                DDLPhysicians.DataValueField = "PhysicianID"
    '                DDLPhysicians.DataSource = DS
    '                DDLPhysicians.DataBind()
    '            End If
    '        End If
    '        DDLPhysicians.Items.Insert(0, New ListItem("Please Select", String.Empty))
    '    Catch ex As Exception
    '        lblMsg.Text = String.Empty
    '        lblMsg.Text = "Err :" & ex.Message
    '    Finally
    '        DS.Dispose()
    '        clsPhy = Nothing
    '    End Try
    'End Sub
    Protected Sub BindAccountData()
        Dim DS As New Data.DataSet

        Dim clsAccount As ETS.BL.Accounts
        Try
            clsAccount = New ETS.BL.Accounts
            clsAccount.ContractorID = Session("contractorID").ToString
            DS = clsAccount.getAccountList(Session("WorkGroupID").ToString, Session("contractorID").ToString, String.Empty)


            If DS.Tables.Count > 0 Then
                If DS.Tables(0).Rows.Count > 0 Then
                    DDLAcc.DataTextField = "AccountName"
                    DDLAcc.DataValueField = "AccountID"
                    DDLAcc.DataSource = DS
                    DDLAcc.DataBind()
                End If
            End If
            DDLAcc.Items.Insert(0, New ListItem("Please Select", String.Empty))
        Catch ex As Exception
            lblMsg.Text = String.Empty
            lblMsg.Text = "Err :" & ex.Message
        Finally
            DS.Dispose()
            clsAccount = Nothing
        End Try
    End Sub
   
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            BindAccountData()
        End If
    End Sub
    Protected Sub AssignedAccounts()
        If Not String.IsNullOrEmpty(DDLAcc.SelectedValue) Then
            Dim DS As New Data.DataSet
            Dim clsAccWrk As ETS.BL.TransExceptionsExclude

            Try
                clsAccWrk = New ETS.BL.TransExceptionsExclude
                'Response.Write(DDLPhysicians.SelectedValue & "#" & strDM(0) & "#" & strDM(1))
                clsAccWrk.AccountID = DDLAcc.SelectedValue
                clsAccWrk.ContractorID = Session("contractorid").ToString
                DS = clsAccWrk.getTranscriptionExceptionExcludesList
                If DS.Tables.Count > 0 Then
                    If DS.Tables(0).Rows.Count > 0 Then
                        'lstAssigned.DataTextField = "PhyName"
                        'lstAssigned.DataValueField = "PhysicianID"
                        'lstAssigned.DataSource = DS
                        'lstAssigned.DataBind()
                        For Each DR As DataRow In DS.Tables(0).Rows
                            Dim LI As New ListItem
                            LI.Text = DR("Comment").ToString
                            LI.Value = DR("ID").ToString

                            If DR("status") Then
                                lstAssigned.Items.Add(LI)
                            Else
                                lstAvailable.Items.Add(LI)
                            End If
                        Next
                    End If
                End If
            Catch ex As Exception
                lblMsg.Text = String.Empty
                lblMsg.Text = "Err :" & ex.Message
            Finally
                clsAccWrk = Nothing
                DS.Dispose()
            End Try
        End If
    End Sub
    'Protected Sub AvialbleAccounts()
    '    If Not String.IsNullOrEmpty(DDLDM.Items(DDLDM.SelectedIndex).Value.ToString) Then
    '        Dim DS As New Data.DataSet
    '        Dim clsAccWrk As ETS.BL.MModalDMAssignments
    '        Try
    '            clsAccWrk = New ETS.BL.MModalDMAssignments
    '            DS = clsAccWrk.GetAvailableMacros(DDLDM.SelectedValue, DDLAcc.SelectedValue)
    '            If DS.Tables.Count > 0 Then
    '                If DS.Tables(0).Rows.Count > 0 Then
    '                    lstAvailable.DataTextField = "PhyName"
    '                    lstAvailable.DataValueField = "PhysicianID"
    '                    lstAvailable.DataSource = DS
    '                    lstAvailable.DataBind()
    '                End If
    '            End If
    '        Catch ex As Exception
    '            lblMsg.Text = String.Empty
    '            lblMsg.Text = "Err :" & ex.Message
    '        Finally
    '            clsAccWrk = Nothing
    '            DS.Dispose()
    '        End Try
    '    End If
    'End Sub
    Protected Sub btnAdd_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnAdd.Click
        Try
            Dim intCt As Integer
            For intCt = lstAvailable.Items.Count - 1 To 0 Step -1 ' Looping Backwards
                If lstAvailable.Items(intCt).Selected Then
                    Dim LI As New ListItem
                    LI.Text = lstAvailable.Items(intCt).Text
                    LI.Value = lstAvailable.Items(intCt).Value
                    lstAssigned.Items.Add(LI)
                    lstAvailable.Items.Remove(lstAvailable.Items(intCt))
                End If
            Next
        Catch ex As Exception
            lblMsg.Text = String.Empty
            lblMsg.Text = "Err :" & ex.Message
        End Try
    End Sub
    Protected Sub btnRemove_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnRemove.Click
        Try
            Dim intCt As Integer
            For intCt = lstAssigned.Items.Count - 1 To 0 Step -1 ' Looping Backwards
                If lstAssigned.Items(intCt).Selected Then
                    Dim LI As New ListItem
                    LI.Text = lstAssigned.Items(intCt).Text
                    LI.Value = lstAssigned.Items(intCt).Value
                    lstAvailable.Items.Add(LI)
                    lstAssigned.Items.Remove(lstAssigned.Items(intCt))
                End If
            Next
        Catch ex As Exception
            lblMsg.Text = String.Empty
            lblMsg.Text = "Err :" & ex.Message
        End Try
    End Sub
   
    Protected Sub ddlAcc_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles DDLAcc.SelectedIndexChanged
        lblMsg.Text = String.Empty
        lstAssigned.Items.Clear()
        lstAvailable.Items.Clear()
        AssignedAccounts()
        'AvialbleAccounts()
    End Sub
    Protected Sub btmSubmit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btmSubmit.Click
        'If Not String.IsNullOrEmpty(DDLDM.Items(DDLDM.SelectedIndex).Value.ToString) Then
        '    Dim intCt As Integer
        '    Dim DT As New Data.DataTable
        '    DT.Columns.Add(New Data.DataColumn("PhysicianID"))

        '    Dim strDM() As String
        '    strDM = Split(DDLDM.SelectedValue, "#$")
        '    For intCt = lstAssigned.Items.Count - 1 To 0 Step -1 ' Looping Backwards
        '        Dim DR As Data.DataRow = DT.NewRow
        '        DR("PhysicianID") = lstAssigned.Items(intCt).Value.ToString

        '        DT.Rows.Add(DR)
        '    Next
        '    ' Dim DS As New Data.DataSet
        '    ' DS.Tables.Add(DT)
        '    ' DS.WriteXml("C:\admin.securexsoft.com\txt.xml")
        '    'If DT.Rows.Count > 0 Then
        '    Dim clsWrkGrpAcc As ETS.BL.MModalDMAssignments
        '    Try
        '        clsWrkGrpAcc = New ETS.BL.MModalDMAssignments

        '        If clsWrkGrpAcc.btnSubmitMacros_click(DT, String.Empty, DDLDM.SelectedValue) = True Then
        '            lblMsg.Text = String.Empty
        '            lblMsg.Text = "Assignment Updated"
        '            Exit Sub
        '        Else
        '            lblMsg.Text = String.Empty
        '            lblMsg.Text = "Assignment failed"
        '            Exit Sub
        '        End If
        '    Catch ex As Exception
        '        Response.Write(ex.Message)
        '    Finally
        '        clsWrkGrpAcc = Nothing
        '    End Try
        '    'End If
        'Else
        '    lblMsg.Text = String.Empty
        '    lblMsg.Text = "Please select workgroup"
        '    Exit Sub
        'End If
    End Sub

    ''Protected Sub DDLPhysicians_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles DDLPhysicians.SelectedIndexChanged
    ''    lblMsg.Text = String.Empty
    ''    lstAssigned.Items.Clear()
    ''    lstAvailable.Items.Clear()
    ''    DDLDM.Items.Clear()
    ''    BindDMData()
    ''End Sub
End Class
