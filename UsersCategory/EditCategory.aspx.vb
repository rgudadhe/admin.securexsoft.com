Imports System.Data
Partial Class Department_Default
    Inherits BasePage

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim DSUCate As New dataset
        Dim clsUCate As New ETS.BL.UserCategories
        With clsUCate
            .ContractorID = Session("ContractorID").ToString
            ._WhereString.Append(" and (deleted is NULL or deleted = 'False')")
            DSUCate = .getCategoryList()
        End With
        clsUCate = Nothing
        DispData.DataSource = DSUCate
        DispData.DataBind()
        DSUCate.Dispose()
    End Sub

End Class
