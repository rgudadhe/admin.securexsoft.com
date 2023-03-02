Imports System
Imports System.Data
Partial Class Samples_EditSamples
    Inherits BasePage

    Protected Sub form1_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles form1.Load
        If Not IsPostBack Then
            ViewState("SortOrder") = " ASC"
        End If
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            Try
                Dim DSPhy As DataSet
                Dim clsPhy As New ETS.BL.Physicians
                With clsPhy
                    DSPhy = .getPhysiciansList(Session("ContractorID").ToString, Session("WorkGroupID").ToString)
                End With
                clsPhy = Nothing
                DSPhy.Tables(0).Columns.Add(New DataColumn("PhysicianName", GetType(System.String), "FirstName+' '+LastName"))
                DDLPhysicians.DataSource = DSPhy
                DSPhy.Dispose()
                DDLPhysicians.DataTextField = "PhysicianName"
                DDLPhysicians.DataValueField = "PhysicianID"
                DDLPhysicians.DataBind()
                Dim LI As New ListItem("", Guid.NewGuid.ToString)
                DDLPhysicians.Items.Insert(0, LI)
                LI.Selected = True
            Catch ex As Exception
                Response.Write(ex.Message)

            End Try
        End If

    End Sub

    Protected Sub btnGO_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnGO.Click
        DoBind("SampleName" & ViewState("SortOrder").ToString)
    End Sub
    Private Function DoBind(ByVal SortBy As String) As Boolean
        Try
            Dim clsSample As New ETS.BL.Samples
            Dim DSSamples As DataSet = clsSample.getPhysicianSamples(DDLPhysicians.SelectedValue)
            clsSample = Nothing
            Dim dc1 As New System.Data.DataColumn
            Dim dc2 As New System.Data.DataColumn
            Dim DT2 As DataTable = DSSamples.Tables(0).Copy
            DSSamples.Tables(0).TableName = "SampleDetails"
            DT2.TableName = "KeyWords"
            DSSamples.Tables.Add(DT2)
            dc1 = DSSamples.Tables(0).Columns("SampleID")
            dc2 = DSSamples.Tables(1).Columns("SampleID")
            Dim dRel As System.Data.DataRelation = New System.Data.DataRelation("Dic", dc1, dc2, False)
            DSSamples.Relations.Add(dRel)

            dlist.TemplateControl.LoadTemplate("SampleKeyWords.ascx")
            dlist.DataSource = DSSamples
            dlist.DataBind()

            If DSSamples.Tables(0).Rows.Count > 0 Then
                btnDelete.Visible = True
            End If
            DSSamples.Dispose()

        Catch ex As Exception
            Response.Write(ex.Message)

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
        e.TemplateFilename = "SampleKeyWords.ascx"
    End Sub

    Protected Sub btnDelete_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnDelete.Click
       
        Try
           
            For Each item As DataGridItem In dlist.Items
                Dim chk As CheckBox = item.FindControl("chkJob")
                If chk.Checked Then
                    Dim hdn As HiddenField = chk.FindControl("hdnID")
                    If Not String.IsNullOrEmpty(hdn.Value.ToString) Then
                        Dim clsSample As New ETS.BL.Samples
                        With clsSample
                            .SampleID = hdn.Value.ToString
                            If .DeleteSample > 0 Then
                                item.Cells(2).Font.Strikeout = True
                            End If
                        End With
                        clsSample = Nothing
                    End If
                End If
            Next
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Protected Sub LinkButton1_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim lnk As LinkButton = CType(sender, LinkButton)
        Dim hdn As HiddenField = lnk.FindControl("hdnID")
        Response.Redirect("EditDocument.aspx?WebPath=" & hdn.Value.ToString, True)
    End Sub
End Class
