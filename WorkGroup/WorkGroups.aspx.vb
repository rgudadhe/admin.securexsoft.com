
Partial Class WorkGroup_WorkGroups
    Inherits BasePage
    Protected Sub FillGrid()
        Dim clsWrkGrp As ETS.BL.WorkGroup
        Dim objDataSet As New Data.DataSet

        Try
            clsWrkGrp = New ETS.BL.WorkGroup
            clsWrkGrp.ContractorID = Session("ContractorID").ToString
            objDataSet = clsWrkGrp.getWrkGrpList

            If objDataSet.Tables(0).Rows.Count = 0 Then
                objDataSet.Tables(0).Constraints.Clear()
                For Each DC As Data.DataColumn In objDataSet.Tables(0).Columns
                    DC.AllowDBNull = True
                Next
                'Add blank row
                objDataSet.Tables(0).Columns(0).AllowDBNull = True
                objDataSet.Tables(0).Rows.Add(objDataSet.Tables(0).NewRow)

                GridViewMain.DataSource = objDataSet.Tables(0)
                GridViewMain.DataBind()
                GridViewMain.Rows(0).Visible = False
            Else
                GridViewMain.DataSource = objDataSet.Tables(0)
                GridViewMain.DataBind()
            End If

        Catch ex As Exception
            'Response.Write("Fill Grid :" & ex.Message)
        Finally
            objDataSet.Dispose()
            clsWrkGrp = Nothing
        End Try
    End Sub
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            FillGrid()
        End If
    End Sub
    Protected Sub GridViewMain_RowCancelingEdit(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCancelEditEventArgs) Handles GridViewMain.RowCancelingEdit
        GridViewMain.EditIndex = -1
        FillGrid()
    End Sub
    Protected Sub GridViewMain_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles GridViewMain.RowCommand
        If Trim(UCase(e.CommandSource.ToString)) = Trim(UCase("System.Web.UI.WebControls.LinkButton")) Then
            If Trim(UCase(e.CommandName)) = Trim(UCase("AddDesc")) Then
                Try
                    Dim varStrDesc As String = String.Empty
                    varStrDesc = Trim(DirectCast(GridViewMain.FooterRow.FindControl("txtDesc"), TextBox).Text)
                    If varStrDesc <> "" Then
                        If IsWorkGrpExist(String.Empty, Trim(varStrDesc)) = False Then
                            Dim clsWrkGrp As ETS.BL.WorkGroup
                            Try
                                clsWrkGrp = New ETS.BL.WorkGroup
                                clsWrkGrp.Description = varStrDesc
                                clsWrkGrp.ContractorID = Session("ContractorID")
                                If clsWrkGrp.InsertWrkGrpDetails() = 1 Then
                                    ClientScript.RegisterStartupScript(Me.GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Workgroup added successfully');window.location='WorkGroups.aspx'</script>")
                                End If
                            Catch ex As Exception
                                Response.Write(ex.Message)
                            Finally
                                clsWrkGrp = Nothing
                            End Try
                        Else
                            ClientScript.RegisterStartupScript(Me.GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Workgroup with similar name already exist,please try with different name');window.location='WorkGroups.aspx'</script>")
                        End If
                    End If
                Catch ex As Exception
                    Response.Write(ex.Message)
                End Try
            End If
        End If
    End Sub
    Protected Function IsWorkGrpExist(ByVal WorkGroupID As String, ByVal varDesc As String) As Boolean
        Dim clsWrkGrp As ETS.BL.WorkGroup
        Dim DSWrkGrp As New Data.DataSet
        Dim oRec As Data.DataTableReader
        Dim varReturn As Boolean = False
        Try
            clsWrkGrp = New ETS.BL.WorkGroup
            clsWrkGrp.Description = varDesc
            clsWrkGrp.ContractorID = Session("ContractorID").ToString
            DSWrkGrp = clsWrkGrp.getWrkGrpList()
            If DSWrkGrp.Tables.Count > 0 Then
                If DSWrkGrp.Tables(0).Rows.Count > 0 Then
                    If String.IsNullOrEmpty(WorkGroupID.ToString) Then
                        oRec = DSWrkGrp.Tables(0).CreateDataReader
                        If oRec.HasRows Then
                            While oRec.Read
                                If Trim(UCase(oRec("Description"))) = Trim(UCase(varDesc)) Then
                                    varReturn = True
                                    Exit While
                                End If
                            End While
                        End If
                    Else
                        Dim DV As Data.DataView
                        Try
                            DV = New Data.DataView(DSWrkGrp.Tables(0), "WorkGroupID <> '" & CStr(WorkGroupID.ToString) & "'", String.Empty, Data.DataViewRowState.CurrentRows)
                            If DV.Count > 0 Then
                                oRec = DV.ToTable.CreateDataReader
                                If oRec.HasRows Then
                                    While oRec.Read
                                        If Trim(UCase(oRec("Description"))) = Trim(UCase(varDesc)) Then
                                            varReturn = True
                                            Exit While
                                        End If
                                    End While
                                End If
                            End If
                        Catch ex As Exception
                            'Response.Write("IsExist :" & ex.Message)
                        Finally
                            DV = Nothing
                        End Try
                    End If
                End If
            End If

        Catch ex As Exception
            Response.Write(ex.Message)
        Finally
            clsWrkGrp = Nothing
            DSWrkGrp.Dispose()
        End Try

        Return varReturn
    End Function
    Protected Sub GridViewMain_RowDeleting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewDeleteEventArgs) Handles GridViewMain.RowDeleting
        Dim hdnTemp As HiddenField
        Dim varStrID As String = String.Empty
        hdnTemp = DirectCast(GridViewMain.Rows(e.RowIndex).FindControl("ID"), HiddenField)
        varStrID = hdnTemp.Value.ToString

        If Not String.IsNullOrEmpty(varStrID) Then
            Dim clsWrkGrp As ETS.BL.WorkGroup
            Try
                clsWrkGrp = New ETS.BL.WorkGroup
                clsWrkGrp.WorkGroupID = varStrID.ToString
                If clsWrkGrp.btnDeleteWrkGrp_Click() = True Then
                    FillGrid()
                    ClientScript.RegisterStartupScript(Me.GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Workgroup deleted successfully');window.location='WorkGroups.aspx'</script>")
                Else
                    FillGrid()
                    ClientScript.RegisterStartupScript(Me.GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Workgroup deleted failed');window.location='WorkGroups.aspx'</script>")
                End If

            Catch ex As Exception
                'Response.Write(ex.Message)
            Finally
                clsWrkGrp = Nothing
            End Try
        End If
    End Sub

    Protected Sub GridViewMain_RowEditing(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewEditEventArgs) Handles GridViewMain.RowEditing
        GridViewMain.EditIndex = e.NewEditIndex
        FillGrid()
    End Sub
    Protected Sub GridViewMain_RowUpdating(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewUpdateEventArgs) Handles GridViewMain.RowUpdating
        Dim varStrDesc As String
        Dim varStrID As String

        varStrID = DirectCast(GridViewMain.Rows(e.RowIndex).FindControl("ID"), HiddenField).Value.ToString
        varStrDesc = DirectCast(GridViewMain.Rows(e.RowIndex).FindControl("txtDescEdit"), TextBox).Text

        If IsWorkGrpExist(varStrID, Trim(varStrDesc)) = False Then
            Try
                Dim clsWrkGrp As ETS.BL.WorkGroup
                Try
                    clsWrkGrp = New ETS.BL.WorkGroup
                    clsWrkGrp.WorkGroupID = varStrID.ToString
                    clsWrkGrp.Description = varStrDesc.ToString
                    If clsWrkGrp.UpdateWrkGrpDetails() = 1 Then
                        ClientScript.RegisterStartupScript(Me.GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Description updated successfully');</script>")
                        FillGrid()
                        GridViewMain.EditIndex = -1
                    End If

                Catch ex As Exception
                    'Response.Write(ex.Message)
                Finally
                    clsWrkGrp = Nothing
                End Try
            Catch ex As Exception
                'Response.Write("Row Updating :" & ex.Message)
            End Try
        Else
            ClientScript.RegisterStartupScript(Me.GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Workgroup with similar name already exist,please try with different name');window.location='WorkGroups.aspx'</script>")
        End If
    End Sub
End Class
