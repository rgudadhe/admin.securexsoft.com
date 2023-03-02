
Partial Class Force_Routing_ForceRoutingReport
    Inherits BasePage
    Protected Sub Page_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Init
        If Not String.IsNullOrEmpty(Request.Form("SEARCH")) Then
            Server.Transfer("ForceRoutingReportResultNew.aspx")
        End If
    End Sub
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            Dim clsPL As ETS.BL.ProductionLevels
            Dim DS As New Data.DataSet
            Dim DV As New Data.DataView
            Try
                clsPL = New ETS.BL.ProductionLevels
                clsPL.Type = Session("IsContractor")
                clsPL.IsDeleted = 0
                DS = clsPL.getPLevelList
                If DS.Tables.Count > 0 Then
                    If DS.Tables(0).Rows.Count > 0 Then
                        DV = New Data.DataView(DS.Tables(0), " levelNo<>1073741824 ", "Sequence", Data.DataViewRowState.CurrentRows)
                        If DV.Count > 0 Then
                            ULevel.DataSource = DV
                            ULevel.DataTextField = "LevelName"
                            ULevel.DataValueField = "LevelNo"
                            ULevel.DataBind()
                        End If
                    End If
                End If
                Dim varlst As New ListItem
                varlst.Value = ""
                varlst.Text = "Please Select"
                ULevel.Items.Insert(0, varlst)

            Catch ex As Exception
                Response.Write(ex.Message)
            Finally
                clsPL = Nothing
            End Try
        End If
    End Sub
    Protected Sub ddlRouting_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlRouting.SelectedIndexChanged
        Try
            Dim varvalue As String = String.Empty
            varvalue = ddlRouting.Items(ddlRouting.SelectedIndex).Value.ToString
            If Not String.IsNullOrEmpty(varvalue) Then
                If Trim(UCase(varvalue)) = Trim(UCase("User")) Then
                    'Response.Write("Tst")
                    ULevel.SelectedIndex = -1
                    UserLeveltxt.Visible = True
                    UserLevel.Visible = True
                    ULevel.Visible = True
                Else
                    UserLeveltxt.Visible = False
                    UserLevel.Visible = False
                    ULevel.Visible = False
                End If
            End If
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub
End Class
