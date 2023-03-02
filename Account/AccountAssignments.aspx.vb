
Partial Class Account_AccountAssignments
    Inherits BasePage
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            BindLevels()
        End If
    End Sub
    Protected Sub btnSeachUsr_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSeachUsr.Click
        BindUsers()
        If GrdUsr.Rows.Count > 0 And GrdAccounts.Rows.Count > 0 Then
            btnAssign.Visible = True
        Else
            btnAssign.Visible = False
        End If
    End Sub
    Protected Sub BindLevels()
        Dim clsPL As ETS.BL.ProductionLevels
        Dim DSPL As New Data.DataSet
        Dim DVPL As Data.DataView
        Try
            clsPL = New ETS.BL.ProductionLevels
            clsPL.ContractorID = IIf(Session("IsContractor") = 0, Session("ParentID").ToString, Session("ContractorID").ToString)
            clsPL.Type = Session("IsContractor")
            DSPL = clsPL.getPLevelList()
            If DSPL.Tables.Count > 0 Then
                If DSPL.Tables(0).Rows.Count > 0 Then
                    DVPL = New Data.DataView(DSPL.Tables(0), "LevelNo not in(1073741824,5,3)", "LevelName", Data.DataViewRowState.CurrentRows)
                    If DVPL.Count > 0 Then
                        ddlLevels.DataSource = DVPL
                        ddlLevels.DataTextField = "LevelName"
                        ddlLevels.DataValueField = "LevelNo"
                        ddlLevels.DataBind()
                    End If
                End If
            End If
        Catch ex As Exception
            Response.Write(ex.Message)
        Finally
            clsPL = Nothing
            DSPL.Dispose()
            DVPL.Dispose()
        End Try
    End Sub
    Protected Sub BindUsers()
        Dim clsUsr As ETS.BL.Users
        Dim DSUsers As New Data.DataSet
        Dim DVUsers As Data.DataView
        Try
            clsUsr = New ETS.BL.Users
            clsUsr.ContractorID = Session("ContractorID").ToString
            DSUsers = clsUsr.getUsersList()
            If DSUsers.Tables.Count > 0 Then
                If DSUsers.Tables(0).Rows.Count > 0 Then
                    DSUsers.Tables(0).Columns.Add("EmpName", GetType(System.String), "FirstName +' '+ LastName")
                    DVUsers = New Data.DataView(DSUsers.Tables(0), "(FirstName like '" & txtUsr.Text.ToString & "%' OR LastName like '" & txtUsr.Text.ToString & "%')", "FirstName,LastName", Data.DataViewRowState.CurrentRows)
                    If DVUsers.Count > 0 Then
                        GrdUsr.DataSource = DVUsers
                        GrdUsr.DataBind()
                    Else
                        GrdUsr.DataSource = Nothing
                        GrdUsr.DataBind()
                    End If

                End If
            End If
        Catch ex As Exception
            Response.Write(ex.Message)
        Finally
            clsUsr = Nothing
            DSUsers.Dispose()
            DVUsers.Dispose()
        End Try
    End Sub
    Protected Sub BindAccounts()
        Dim clsAcc As ETS.BL.Accounts
        Dim DSAcc As New Data.DataSet
        Dim DVAcc As Data.DataView

        Try
            clsAcc = New ETS.BL.Accounts
            clsAcc.ContractorID = Session("ContractorID").ToString
            DSAcc = clsAcc.getAccountList()
            If DSAcc.Tables.Count > 0 Then
                If DSAcc.Tables(0).Rows.Count > 0 Then
                    DVAcc = New Data.DataView(DSAcc.Tables(0), "(AccountName like '" & txtAcc.Text.ToString & "%')", "AccountName", Data.DataViewRowState.CurrentRows)
                    If DVAcc.Count > 0 Then
                        GrdAccounts.DataSource = DVAcc
                        GrdAccounts.DataBind()
                    Else
                        GrdAccounts.DataSource = Nothing
                        GrdAccounts.DataBind()
                    End If
                End If
            End If

        Catch ex As Exception
            Response.Write(ex.Message)
        Finally
            clsAcc = Nothing
            DSAcc.Dispose()
            DVAcc.Dispose()
        End Try
    End Sub
    Protected Sub btnSearchAcc_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSearchAcc.Click
        BindAccounts()
        If GrdUsr.Rows.Count > 0 And GrdAccounts.Rows.Count > 0 Then
            btnAssign.Visible = True
        Else
            btnAssign.Visible = False
        End If
    End Sub
    Protected Sub btnAssign_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAssign.Click
        Dim DTAcc As New Data.DataTable
        DTAcc.Columns.Add(New Data.DataColumn("AccID"))

        For Each r As GridViewRow In GrdAccounts.Rows
            Dim chk As CheckBox = DirectCast(r.FindControl("chkAcc"), CheckBox)
            If chk.Checked Then
                Dim Hdn As HiddenField = DirectCast(r.FindControl("hdnAccID"), HiddenField)
                If Not Hdn Is Nothing Then
                    If Not String.IsNullOrEmpty(Hdn.Value) Then
                        Dim DRAcc As Data.DataRow = DTAcc.NewRow
                        DRAcc("AccID") = Hdn.Value.ToString
                        DTAcc.Rows.Add(DRAcc)
                    End If
                End If
            End If
        Next

        Dim DTUsr As New Data.DataTable
        DTUsr.Columns.Add(New Data.DataColumn("UsrID"))
        For Each r1 As GridViewRow In GrdUsr.Rows
            Dim chk1 As CheckBox = DirectCast(r1.FindControl("chkUsr"), CheckBox)
            If chk1.Checked Then
                Dim Hdn1 As HiddenField = DirectCast(r1.FindControl("hdnUserID"), HiddenField)
                If Not Hdn1 Is Nothing Then
                    If Not String.IsNullOrEmpty(Hdn1.Value) Then
                        Dim DRUsr As Data.DataRow = DTUsr.NewRow
                        DRUsr("UsrID") = Hdn1.Value.ToString
                        DTUsr.Rows.Add(DRUsr)
                    End If
                End If
            End If
        Next

        If String.IsNullOrEmpty(ddlLevels.SelectedValue.ToString) Then
            lblMsg.Text = String.Empty
            lblMsg.Text = "Please select level"
            Exit Sub
        End If

        If DTAcc.Rows.Count <= 0 Then
            lblMsg.Text = String.Empty
            lblMsg.Text = "Please select accounts to assign"
            Exit Sub
        End If
        If DTUsr.Rows.Count <= 0 Then
            lblMsg.Text = String.Empty
            lblMsg.Text = "Please select user to assign"
            Exit Sub
        End If

        Dim clsAcc As ETS.BL.Accounts
        Try
            clsAcc = New ETS.BL.Accounts
            If clsAcc.btn_AccountAssignToUsrs(DTAcc, DTUsr, CInt(ddlLevels.SelectedValue.ToString)) = True Then
                lblMsg.Text = String.Empty
                lblMsg.Text = "Assignment sucessful"
                Exit Sub
            Else
                lblMsg.Text = String.Empty
                lblMsg.Text = "Assignment failed"
                Exit Sub
            End If
        Catch ex As Exception
            Response.Write(ex.Message)
        Finally
            clsAcc = Nothing
        End Try


    End Sub
End Class
