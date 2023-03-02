
Partial Class ViewForums
    Inherits BasePage
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            BindData()
        End If
    End Sub
    Protected Sub BindData()
        Try
            Dim clsForum As New ETS.BL.Forum
            clsForum.ContractorID = Session("ContractorID").ToString

            Dim DS As New Data.DataSet
            DS = clsForum.getForumList
            DS.Tables(0).DefaultView.Sort = "Details"
            
            MyDataGrid.DataSource = DS
            MyDataGrid.DataBind()
            clsForum = Nothing
            If MyDataGrid.Rows.Count > 0 Then
                MyDataGrid.ShowFooter = True
                MyDataGrid.UseAccessibleHeader = True
                MyDataGrid.HeaderRow.TableSection = TableRowSection.TableHeader
                MyDataGrid.FooterRow.TableSection = TableRowSection.TableFooter
            End If
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub
    Protected Sub MyDataGrid_RowCancelingEdit(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCancelEditEventArgs) Handles MyDataGrid.RowCancelingEdit
        MyDataGrid.EditIndex = -1
        BindData()
    End Sub
    Protected Sub MyDataGrid_RowEditing(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewEditEventArgs) Handles MyDataGrid.RowEditing
        MyDataGrid.EditIndex = e.NewEditIndex
        BindData()
    End Sub
    Protected Sub MyDataGrid_RowUpdated(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewUpdatedEventArgs) Handles MyDataGrid.RowUpdated
        MyDataGrid.EditIndex = -1
        BindData()
    End Sub
    Protected Sub MyDataGrid_RowUpdating(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewUpdateEventArgs) Handles MyDataGrid.RowUpdating
        Try
            Dim varForumId As String = String.Empty
            varForumId = DirectCast(MyDataGrid.Rows(e.RowIndex).FindControl("hdnForumID"), HiddenField).Value.ToString
            Dim varDetails As String = String.Empty
            varDetails = DirectCast(MyDataGrid.Rows(e.RowIndex).FindControl("txtDetails"), TextBox).Text.ToString
            Dim isDeleted As Boolean
            isDeleted = DirectCast(MyDataGrid.Rows(e.RowIndex).FindControl("isDeleted"), CheckBox).Checked

            Dim clsForum As New ETS.BL.Forum
            With clsForum
                .ForumID = varForumId
                .Details = varDetails
                .isDeleted = isDeleted
            End With

            Dim retVal As Integer = clsForum.UpdateForumDetails
            'Response.Write(retVal)
            If retVal = 1 Then
                BindData()
            End If
            MyDataGrid.EditIndex = -1
            clsForum = Nothing
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub
End Class
