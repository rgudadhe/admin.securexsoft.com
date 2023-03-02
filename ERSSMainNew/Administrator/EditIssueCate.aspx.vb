Imports MainModule
Partial Class ERSSMain_EditIssueCate
    Inherits BasePage
    Dim objMainModule As New MainModule
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            Dim clsERSSIC As ETS.BL.ERSSIssueCategory
            Try
                clsERSSIC = New ETS.BL.ERSSIssueCategory
                clsERSSIC.CategoryID = Request.QueryString("ID")
                clsERSSIC.getIssueCategoryDetails()
                txtCateName.Text = clsERSSIC.CateName
                txtCateDesc.Text = clsERSSIC.Description
            Catch ex As Exception
            Finally
                clsERSSIC = Nothing
            End Try
        End If
    End Sub
    Protected Sub BtnDelete_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtnDelete.Click
        Dim clsERSSIC As ETS.BL.ERSSIssueCategory
        Dim RetVal As Integer
        Try
            clsERSSIC = New ETS.BL.ERSSIssueCategory
            With clsERSSIC
                .CategoryID = Request.QueryString("ID").ToString
                .IsDeleted = True
            End With
            RetVal = clsERSSIC.UpdateIssueCategoryDetails()
            If RetVal = 1 Then
                Response.Write("<center><font face=""Arial"" size=""2"" color=""#000080"">Category updated sucessfully !!!</font></center>")
                Response.Write("<center><a href=""CloseWindow.aspx"">Close Window</a></center>")
                Response.End()
            End If
        Catch ex As Exception
        Finally
            clsERSSIC = Nothing
        End Try
    End Sub
    Protected Sub BtnSubmit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtnSubmit.Click
        Dim varStrCateName As String
        Dim varStrCateDesc As String
        Dim varBolCheckPrefix As Boolean

        Dim DS As Data.DataSet
        Dim DV As Data.DataView
        Dim clsERSSIC As ETS.BL.ERSSIssueCategory

        Try
            varStrCateName = Replace(UCase(Trim(Request.Form("txtCateName"))), "'", "''")
            varStrCateDesc = Replace(Trim(Request.Form("txtCateDesc")), "'", "''")

            clsERSSIC = New ETS.BL.ERSSIssueCategory
            clsERSSIC.ContractorID = Session("ContractorID")

            DS = clsERSSIC.getIssueCategoryList()
            DV = New Data.DataView(DS.Tables(0))
            DV.RowFilter = "CateName='" & varStrCateName & "' AND CategoryID <>'" & Request.QueryString("ID") & "' AND (IsDeleted IS NULL OR IsDeleted=0) "

            If DV.ToTable().Rows.Count > 0 Then
                Response.Write("<script type=""text/javascript"" language=javascript> alert(""Category already exists"");window.location.href='EditIssueCate.aspx?ID=" & Request.QueryString("ID").ToString & "';</script>")
                Exit Sub
            Else
                varBolCheckPrefix = True
            End If

            Dim RetVal As Integer

            If varBolCheckPrefix = True Then

                With clsERSSIC
                    .CategoryID = Request.QueryString("ID").ToString
                    .CateName = varStrCateName
                    .Description = varStrCateDesc
                    .CreatedBy = Session("UserID").ToString
                    .CreationDate = Now()
                End With
                RetVal = clsERSSIC.UpdateIssueCategoryDetails()
                If RetVal = 1 Then
                    Response.Write("<center><font face=""Arial"" size=""2"" color=""#000080"">Category updated sucessfully !!!</font></center>")
                    Response.Write("<center><a href=""CloseWindow.aspx"">Close Window</a></center>")
                    Response.End()
                End If
            End If
        Catch ex As Exception
        Finally
            clsERSSIC = Nothing
            DS = Nothing
            DV = Nothing
        End Try
    End Sub
End Class
