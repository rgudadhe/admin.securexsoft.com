Imports System
Imports System.Data
Partial Class Profuction_Levels_ProductionLevels
    Inherits BasePage

    Protected Sub cmdAdd_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdAdd.Click
        Dim goAhead As Boolean = True
        Dim strMessage As String
        Dim ClsPL As New ETS.BL.ProductionLevels
        ClsPL.ContractorID = Session("ContractorID")
        Dim DSAL As DataSet = ClsPL.getPLevelList()
        Dim MaxLevel As Integer = 1
        If DSAL.Tables.Count > 0 Then
            If DSAL.Tables(0).Rows.Count > 0 Then
                Response.Write(DSAL.Tables(0).Rows(0).Item(0).ToString)
                If Not IsDBNull(DSAL.Tables(0).Rows(0).Item("LevelNo")) Then
                    MaxLevel = IIf(IsDBNull(DSAL.Tables(0).Compute("MAX(LevelNo)", "LevelNo<>1073741824")), 1, DSAL.Tables(0).Compute("MAX(LevelNo)", "LevelNo<>1073741824"))
                End If

            End If
        End If

        If MaxLevel >= 536870912 Then
            strMessage = "Production Level limits reached!"
        Else
            If DSAL.Tables(0).Compute("count(LevelName)", "LevelName = '" & txtLevelName.Text & "'") = 0 Then
                With ClsPL
                    .LevelName = txtLevelName.Text
                    .LevelNo = IIf(MaxLevel + MaxLevel = 0, 1, MaxLevel + MaxLevel)
                    .Description = txtLevelDesc.Text
                    .Type = True   'cmbType.SelectedValue
                End With

                Dim RetVal As Integer = ClsPL.InsertNewLevel
                ClsPL = Nothing
                If RetVal = 1 Then
                    strMessage = "Production Level " & txtLevelName.Text & " added successfully"
                    Response.Write("<script language=JavaScript>alert('" & strMessage & "');</script>")
                    Response.Redirect("CheckInOptions.aspx?LevelNo=" & MaxLevel + MaxLevel, True)
                Else
                    strMessage = "Adding Production Level " & txtLevelName.Text & " failed"
                End If
            Else
                strMessage = "Production Level with name " & txtLevelName.Text & " is alreadt exist"
            End If
        End If
        Response.Write("<script language=JavaScript>alert('" & strMessage & "');</script>")
    End Sub
End Class
