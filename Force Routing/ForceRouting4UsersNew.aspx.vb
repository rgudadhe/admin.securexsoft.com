
Partial Class ForceRouting4UserResult
    Inherits BasePage
    Protected Sub Page_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Init
        If Not String.IsNullOrEmpty(Request.Form("SEARCH")) Then
            Server.Transfer("ForceRouting4UserResultNew.aspx")
        End If
    End Sub
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            Dim clsPL As ETS.BL.ProductionLevels
            Dim Ds As New Data.DataSet
            Dim DV As New Data.DataView
            Try
                clsPL = New ETS.BL.ProductionLevels
                clsPL.Type = Session("IsContractor")
                clsPL.JobsRouting = 0
                Ds = clsPL.getPLevelList
                If Ds.Tables.Count > 0 Then
                    If Ds.Tables(0).Rows.Count > 0 Then
                        DV = New Data.DataView(Ds.Tables(0), " levelNo<>1073741824 ", String.Empty, Data.DataViewRowState.CurrentRows)
                        If DV.Count > 0 Then
                            ULevel.DataSource = DV
                            ULevel.DataTextField = "LevelName"
                            ULevel.DataValueField = "LevelNo"
                            ULevel.DataBind()
                        End If
                    End If
                End If
            Catch ex As Exception
                Response.Write(ex.Message)
            Finally
                Ds = Nothing
                clsPL = Nothing
                DV = Nothing
            End Try
        End If
    End Sub
End Class
