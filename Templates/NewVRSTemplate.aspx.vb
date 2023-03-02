Namespace ets
    Partial Class Templates_NewTemplate
        Inherits BasePage

        Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
           
        End Sub

        Protected Sub btnAdd_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAdd.Click
            Dim clsTemp As New ets.BL.Templates
            With clsTemp
                .TemplateID = Guid.NewGuid.ToString
                .TemplateName = txtTemplateName.Text
                .TemplateType = txtTypeLetter.Text
                .TypeDesc = txtTypeName.Text
                .DateModified = Now
                .ContractorID = Session("ContractorID")
                .isVRS = True
                If .InsertTemplateDetails = 1 Then
                    Response.Redirect("TemplateAttributes.aspx?TempID=" & .TemplateID, True)
                Else
                    Response.Write("<script language='javascript'>alert('Error Message')</script>")
                End If
            End With
            clsTemp = Nothing
        End Sub
    End Class
End Namespace