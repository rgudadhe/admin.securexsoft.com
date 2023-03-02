Imports MainModule
Partial Class ERSSMain_AddIssueCate
    Inherits BasePage
    Dim objMainModule As New MainModule
    Protected Sub BtnSubmit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtnSubmit.Click
        Dim varStrCateName As String
        Dim varStrCateDesc As String
        Dim varBolCheckPrefix As Boolean

        varStrCateName = Replace(UCase(Trim(Request.Form("txtCateName"))), "'", "''")
        varStrCateDesc = Replace(Trim(Request.Form("txtCateDesc")), "'", "''")
        Dim DS As Data.DataSet
        Dim DV As Data.DataView
        Dim clsERSSIC As ETS.BL.ERSSIssueCategory
        Try
            clsERSSIC = New ETS.BL.ERSSIssueCategory
            clsERSSIC.ContractorID = Session("ContractorID")

            DS = clsERSSIC.getIssueCategoryList()
            DV = New Data.DataView(DS.Tables(0))
            DV.RowFilter = "CateName='" & varStrCateName & "' AND (IsDeleted IS NULL OR IsDeleted=0) "

            If DV.ToTable().Rows.Count > 0 Then
                Response.Write("<script type=""text/javascript"" language=javascript> alert(""Category already exists"");window.location.href='AddIssueCate.aspx';</script>")
                Exit Sub
            Else
                varBolCheckPrefix = True
            End If

            Dim RetVal As Integer
            If varBolCheckPrefix = True Then
                With clsERSSIC
                    .CateName = varStrCateName
                    .Description = varStrCateDesc
                    .CreatedBy = Session("UserID").ToString
                    .CreationDate = Now
                End With
                RetVal = clsERSSIC.InsertIssueCategory()

                Response.Write("<center><font face=""Arial"" size=""2"" color=""#000080"">Category added sucessfully !!!</font></center>")
                Response.Write("<center><a href=""CloseWindow.aspx"">Close Window</a></center>")
                Response.End()
            End If
        Catch ex As Exception
        Finally
            clsERSSIC = Nothing
            DV = Nothing
            DS = Nothing
        End Try
    End Sub
End Class
