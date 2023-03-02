
Partial Class testets_Default3
    Inherits BasePage
    Protected Sub Button2_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button2.Click
        BindData()
    End Sub

    Protected Sub MyDataGrid_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles MyDataGrid.RowCommand
        If Trim(UCase(e.CommandName)) = Trim(UCase("Remove")) Then
            Dim clsCOO As ETS.BL.COO
            Try
                clsCOO = New ETS.BL.COO
                clsCOO.TrackID = Trim(e.CommandArgument)
                If clsCOO.DeleteCOOArticle = 1 Then
                    BindData()
                End If
            Catch ex As Exception
                Response.Write(ex.Message)
            Finally
                clsCOO = Nothing
            End Try
        End If
    End Sub
    Protected Sub BindData()
        Dim SubDate1 As String
        Dim SubDate2 As String
        If TxtDate1.Text <> "" And IsDate(TxtDate1.Text) Then
            SubDate1 = TxtDate1.Text
        Else
            SubDate1 = "1/1/2006"
        End If

        If TxtDate2.Text <> "" And IsDate(TxtDate2.Text) Then
            SubDate2 = TxtDate2.Text
        Else
            SubDate2 = Now()
        End If

        Dim clsCOO As ETS.BL.COO
        Dim DS As New Data.DataSet
        Dim DV As New Data.DataView
        Try
            clsCOO = New ETS.BL.COO
            DS = clsCOO.getCOOArticleList
            If DS.Tables.Count > 0 Then
                If DS.Tables(0).Rows.Count > 0 Then
                    DV = New Data.DataView(DS.Tables(0), "DateDisp >= '" & SubDate1 & "' and DateDisp <='" & SubDate2 & "'", "datedisp desc", Data.DataViewRowState.CurrentRows)
                    If DV.Count > 0 Then
                        MyDataGrid.DataSource = DV
                        MyDataGrid.DataBind()
                    End If
                End If
            End If
        Catch ex As Exception
            Response.Write(ex.Message)
        Finally
            clsCOO = Nothing
        End Try
    End Sub
End Class
