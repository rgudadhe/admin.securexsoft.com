Imports System.Data.SqlClient
Partial Class CategoryDet
    Inherits BasePage

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            Dim clsUCate As New ETS.BL.UserCategories(Request.QueryString("CatID"))
            With clsUCate
                TxtName.Text = .Name
                TxtDesc.Text = .Description
                TxtPrefix.Text = .Prefix
                CategoryID.Value = .CategoryID
            End With
            clsUCate = Nothing
            Button3.Visible = False

        End If

    End Sub

    Protected Sub Button1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button1.Click
        Dim clsUCate As New ETS.BL.UserCategories()
        With clsUCate
            .CategoryID = CategoryID.Value
            .Name = TxtName.Text
            .Description = TxtDesc.Text
            .Prefix = TxtPrefix.Text
            If .UpdateCategoryDetails > 0 Then
                MsgDisp.Text = "Record has been udpated successfully."
            Else
                MsgDisp.Text = "Failed updating record."
            End If
        End With
        clsUCate = Nothing
    End Sub

    Protected Sub Button2_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button2.Click
        Dim clsUCate As New ETS.BL.UserCategories()
        With clsUCate
            .CategoryID = CategoryID.Value
            .Deleted = True
            If .UpdateCategoryDetails > 0 Then
                MsgDisp.Text = "Record has been deleted successfully."
            Else
                MsgDisp.Text = "Failed deleting record."
            End If
        End With
        clsUCate = Nothing
    End Sub
End Class
