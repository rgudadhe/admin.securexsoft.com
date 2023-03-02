Imports System.Data
Partial Class Department_Default
    Inherits BasePage

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            PnlDesc.Visible = False
            Dim DSActCate As New DataSet
            Dim clsActCate As New ETS.BL.ActCategories
            With clsActCate
                .ContractorID = Session("ContractorID").ToString
                DSActCate = .getActCateList
            End With
            clsActCate = Nothing
            DLCate.DataSource = DSActCate
            DLCate.DataTextField = "Description"
            DLCate.DataValueField = "Category"
            DLCate.DataBind()
            DSActCate.Dispose()
            Dim LI As New ListItem("Select Category", "")
            DLCate.Items.Insert(0, LI)
            LI = Nothing
        End If
    End Sub

    Protected Sub Button1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button1.Click
        Dim clsActCate As New ETS.BL.ActCategories
        With clsActCate
            .Category = DLCate.SelectedValue
            .Description = txtCategory.Text
            .Priority = txtPriority.Text
            .isdeleted = DLStatus.SelectedIndex
            If .UpdateActCateDetails() >= 1 Then
                Response.Write("Record has been updated into database successfully")
            Else
                Response.Write("Failed updating record")
            End If
        End With
        clsActCate = Nothing
    End Sub

    Protected Sub DLCate_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles DLCate.SelectedIndexChanged
        If DLCate.Items(0).Value = "" Then
            DLCate.Items.RemoveAt(0)
        End If
        PnlDesc.Visible = True
        Dim clsActCate As New ETS.BL.ActCategories
        With clsActCate
            .Category = DLCate.SelectedValue
            .getActCateDetails()
            txtCategory.Text = .Description
            txtPriority.Text = .Priority
            If .isdeleted Then
                DLStatus.Items(1).Selected = True
                DLStatus.Items(0).Selected = False
            Else
                DLStatus.Items(1).Selected = False
                DLStatus.Items(0).Selected = True
            End If

        End With
        clsActCate = Nothing
    End Sub
End Class
