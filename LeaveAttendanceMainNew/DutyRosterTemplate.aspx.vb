
Partial Class LeaveAttendanceMainNew_DutyRosterTemplate
    Inherits System.Web.UI.Page
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            BindData()
        End If
    End Sub
    Protected Sub BindData()
        Dim DS As New Data.DataSet
        Dim clsDT As ETS.BL.DutyRosterTemplates
        Dim DV As New Data.DataView
        Try
            clsDT = New ETS.BL.DutyRosterTemplates
            clsDT.ContractorID = Session("ContractorID").ToString
            DS = clsDT.getDutyRosterTemplatesList()
            If DS.Tables.Count > 0 Then
                If DS.Tables(0).Rows.Count > 0 Then
                    DV = New Data.DataView(DS.Tables(0), "(IsDeleted IS NULL OR IsDeleted=0)", "Name", Data.DataViewRowState.CurrentRows)
                    If DV.Count > 0 Then
                        GridViewMain.DataSource = DV
                        GridViewMain.DataBind()
                    End If
                End If
            End If
        Catch ex As Exception
            Response.Write(ex.Message)
        Finally
            clsDT = Nothing
            DS = Nothing
        End Try
    End Sub
    Protected Sub btnAdd_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAdd.Click
        Dim varWO As Integer = 0
        varWO = GetWO()
        If CheckTemplateExist() = False Then
            If varWO > 0 Then
                Dim clsDT As ETS.BL.DutyRosterTemplates
                Try
                    clsDT = New ETS.BL.DutyRosterTemplates
                    clsDT.TemplateID = Guid.NewGuid.ToString
                    clsDT.Name = txtName.Text.ToString
                    clsDT.WO = varWO
                    clsDT.Shift = ddlShift.SelectedValue.ToString
                    clsDT.ContractorID = Session("ContractorID").ToString
                    clsDT.UpdatedBy = Session("UserID").ToString
                    clsDT.UpdatedOn = Now

                    If clsDT.InsertDutyRosterDetails() = 1 Then
                        lblStatus.Text = "Template created..."
                        BindData()
                        ClearCtrls()
                        cpesetting.Collapsed = True
                    End If

                Catch ex As Exception
                    Response.Write(ex.Message)
                Finally
                    clsDT = Nothing
                End Try
            Else
                lblStatus.Text = "Please select weekly off"
                Exit Sub
            End If
        Else
            lblStatus.Text = "Template with similar name already exist!!!"
            Exit Sub
        End If
    End Sub
    Protected Sub ClearCtrls()
        txtName.Text = String.Empty
        For Each MyItem As ListItem In chkWOff.Items
            MyItem.Selected = False
        Next
        ddlShift.SelectedIndex = 0
    End Sub
    Public Function GetWO() As Integer
        Dim varReturn As Integer = 0
        Try
            Dim MyItem As ListItem
            For Each MyItem In chkWOff.Items
                If MyItem.Selected = True Then
                    varReturn = varReturn + CInt(MyItem.Value)
                End If
            Next
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
        Return varReturn
    End Function
    Protected Function CheckTemplateExist() As Boolean
        Dim IsExist As Boolean = False

        Dim clsDT As ETS.BL.DutyRosterTemplates
        Dim DS As New Data.DataSet
        Dim DV As New Data.DataView
        Try
            clsDT = New ETS.BL.DutyRosterTemplates
            clsDT.Name = Trim(txtName.Text.ToString)
            clsDT.ContractorID = Session("ContractorID").ToString
            DS = clsDT.getDutyRosterTemplatesList


            If DS.Tables.Count > 0 Then
                If DS.Tables(0).Rows.Count > 0 Then
                    DV = New Data.DataView(DS.Tables(0), "(IsDeleted IS NULL OR IsDeleted=0)", "", Data.DataViewRowState.CurrentRows)
                    If DV.Count > 0 Then
                        IsExist = True
                    End If
                End If
            End If
        Catch ex As Exception
            Response.Write(ex.Message)
        Finally
            clsDT = Nothing
            DS = Nothing
        End Try
        Return IsExist
    End Function
    '   DECLARE @chkLevel BIT
    'SET @chkLevel = 0
    'IF (@AdminLevel & @Lvl)= @Lvl
    'BEGIN
    '           SET @chkLevel = 1
    '   END

    Protected Sub GridViewMain_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles GridViewMain.RowCommand
        If Trim(UCase(e.CommandName)) = Trim(UCase("DeleteTemplate")) Then
            Dim varTemplateID As String = String.Empty
            varTemplateID = e.CommandArgument.ToString
            If Not String.IsNullOrEmpty(varTemplateID.ToString) Then
                Dim clsDT As ETS.BL.DutyRosterTemplates
                Try
                    clsDT = New ETS.BL.DutyRosterTemplates
                    clsDT.TemplateID = varTemplateID.ToString
                    clsDT.ContractorID = Session("ContractorID").ToString
                    clsDT.IsDeleted = True

                    If clsDT.UpdateDutyRosterTemplateDetails() = 1 Then
                        lblStatus.Text = "Template deleted..."
                        BindData()
                        ClearCtrls()
                        cpesetting.Collapsed = True
                    End If
                Catch ex As Exception
                    Response.Write(ex.Message)
                Finally
                    clsDT = Nothing
                End Try
            End If
        End If
    End Sub
End Class
