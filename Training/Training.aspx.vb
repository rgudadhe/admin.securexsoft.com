
Partial Class Training_Result
    Inherits BasePage
    Protected Sub form1_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles form1.Load
        If Not IsPostBack Then
            ViewState("SortOrder") = " ASC"
        End If
    End Sub
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            Dim clsOLT As ETS.BL.OnlineTraining
            Dim DS As New Data.DataSet
            Try
                clsOLT = New ETS.BL.OnlineTraining
                DS = clsOLT.GetTraningLogPhysicians(Session("WorkGroupID").ToString, Session("contractorID").ToString)
                If DS.Tables.Count > 0 Then
                    If DS.Tables(0).Rows.Count > 0 Then
                        DDLPhysicians.DataSource = DS
                        DDLPhysicians.DataTextField = "PhysicianName"
                        DDLPhysicians.DataValueField = "PhyID"
                        DDLPhysicians.DataBind()
                    End If
                End If

                DDLPhysicians.Items.Insert(0, New ListItem(String.Empty, String.Empty))
            Catch ex As Exception
                Response.Write(ex.Message)
            Finally
                clsOLT = Nothing
                DS = Nothing
            End Try
        End If
    End Sub
    Protected Sub btnGO_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnGO.Click
        DoBind("JobNumber" & ViewState("SortOrder").ToString)
    End Sub
    Private Function DoBind(ByVal SortBy As String) As Boolean
        Dim clsOLT As ETS.BL.OnlineTraining
        Dim Ds As New Data.DataSet
        Dim Dv As New Data.DataView
        Try
            clsOLT = New ETS.BL.OnlineTraining
            Ds = clsOLT.GetTraningLogPhysiciansSamplesRecords(DDLPhysicians.SelectedValue.ToString)
            If Ds.Tables.Count > 0 Then
                If Ds.Tables(0).Rows.Count > 0 Then
                    Dv = New Data.DataView(Ds.Tables(0), String.Empty, SortBy, Data.DataViewRowState.CurrentRows)
                    If Dv.Count > 0 Then
                        dlist.DataSource = Dv
                        dlist.DataBind()
                    End If
                End If
            End If
        Catch ex As Exception
            Response.Write(ex.Message)
        Finally
            clsOLT = Nothing
            Ds = Nothing
            Dv = Nothing
        End Try
    End Function

    Protected Sub dlist_SortCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridSortCommandEventArgs) Handles dlist.SortCommand
        If String.IsNullOrEmpty(ViewState("SortOrder")) Then
            ViewState("SortOrder") = " ASC"
        ElseIf ViewState("SortOrder").ToString = " ASC" Then
            ViewState("SortOrder") = " DESC"
        Else
            ViewState("SortOrder") = " ASC"
        End If
        DoBind(e.SortExpression.ToString & ViewState("SortOrder").ToString)

    End Sub

    Protected Sub dlist_TemplateSelection(ByVal sender As Object, ByVal e As DBauer.Web.UI.WebControls.HierarGridTemplateSelectionEventArgs) Handles dlist.TemplateSelection
        '    e.TemplateFilename = "SampleKeyWords.ascx"
    End Sub
    Protected Sub LinkButton1_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim lnk As LinkButton = CType(sender, LinkButton)
        Dim hdn As HiddenField = lnk.FindControl("hdnID")
        Dim hdnType As HiddenField = lnk.FindControl("hdnType")
        Dim clsOLT As ETS.BL.OnlineTraining
        Try
            clsOLT = New ETS.BL.OnlineTraining
            clsOLT.TranscriptionID = hdn.Value.ToString
            clsOLT.UserID = Session("UserID")
            clsOLT.DateEdited = Now
            If clsOLT.InsertTrainingLog = 1 Then
                Response.Redirect("EditDocument.aspx?WebPath=" & hdn.Value.ToString & "&Type=" & hdnType.Value.ToString, True)
            End If
        Catch ex As Exception
            Response.Write(ex.Message)
        Finally
            clsOLT = Nothing
        End Try
    End Sub
End Class
