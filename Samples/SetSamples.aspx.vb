
Partial Class Samples_SetSamples
    Inherits BasePage

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Not IsPostBack Then
            Dim clsSamples As New ETS.BL.Samples
            Dim objDS As System.Data.DataSet = clsSamples.getSamplesToSet(Session("ContractorID").ToString, Session("WorkGroupID").ToString)
            ViewState("objDS") = objDS
            dlist.DataSource = objDS
            dlist.DataBind()
        End If        
        
    End Sub

    Protected Sub LinkButton1_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim btn As LinkButton = CType(sender, LinkButton)
        Dim hdn As HiddenField = btn.FindControl("hdnID")
        Response.Redirect("Document.aspx?WebPath=" & hdn.Value.ToString, True)
    End Sub

    Protected Sub dlist_SortCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridSortCommandEventArgs) Handles dlist.SortCommand
        ViewState("SortItem") = e.SortExpression.ToString()
        If String.IsNullOrEmpty(ViewState("SortOrder")) Then
            ViewState("SortOrder") = " ASC"
        ElseIf ViewState("SortOrder").ToString = " ASC" Then
            ViewState("SortOrder") = " DESC"
        Else
            ViewState("SortOrder") = " ASC"
        End If
        Response.Redirect("SetSamples.aspx?SortItem=" & ViewState("SortItem").ToString & "&SortOrder=" & ViewState("SortOrder"), True)
    End Sub
End Class
