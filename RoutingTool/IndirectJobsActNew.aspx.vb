Imports System.Data.SqlClient
Partial Class RoutingTool_IndirectJobsActNew
    Inherits System.Web.UI.Page
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        MTStatus()
        ActStatus()
    End Sub
    Sub ActStatus()
        Dim clsRo As ETS.BL.Routing
        Dim DS As New Data.DataSet
        Dim DV As New Data.DataView
        Dim Totmins As Long
        Dim Totjobs As Long
        Try
            clsRo = New ETS.BL.Routing
            Dim varIntialProdLevel As Integer = Session("IntialProductionLevel")
            DS = clsRo.RoutingToolIndirectJobsByAcc(Session("ContractorId").ToString, Session("WorkgroupID").ToString, varIntialProdLevel)
            If DS.Tables.Count > 0 Then
                If DS.Tables(0).Rows.Count > 0 Then
                    Totmins = DS.Tables(0).Compute("Sum(Mins)", String.Empty) / 60
                    Totjobs = DS.Tables(0).Compute("Sum(reccount)", String.Empty)
                    DV = New Data.DataView(DS.Tables(0), String.Empty, "AccountName", Data.DataViewRowState.CurrentRows)
                    If DV.Count > 0 Then
                        rptDetailsForAcc.DataSource = DV
                        rptDetailsForAcc.DataBind()
                    End If
                    LblTMins.Text = CInt(Totmins)
                    lblTotJobs.Text = Totjobs
                Else
                    LblTMins.Text = 0
                    lblTotJobs.Text = 0
                End If
            Else
                LblTMins.Text = 0
                lblTotJobs.Text = 0
            End If
        Catch ex As Exception
            Response.Write(ex.Message)
        Finally
            clsRo = Nothing
            DS.Dispose()
        End Try
    End Sub
    Sub MTStatus()
        Dim Totmins As Double
        Dim Totjobs As Integer
        Dim Totimins As Double
        Dim Totijobs As Integer

        Totmins = 0
        Totjobs = 0
        Totimins = 0
        Totijobs = 0

        Dim clsRo As ETS.BL.Routing
        Dim DS As New Data.DataSet
        Try
            clsRo = New ETS.BL.Routing
            Dim varIntialProdLevel As Integer = Session("IntialProductionLevel")
            DS = clsRo.RoutingToolIndirectJobsByMT(Session("ContractorID"), Session("WorkGroupID"), varIntialProdLevel)
            If DS.Tables.Count > 0 Then
                If DS.Tables(0).Rows.Count > 0 Then
                    Totmins = DS.Tables(0).Compute("Sum(Mins)", String.Empty) / 60
                    Totjobs = DS.Tables(0).Compute("Sum(reccount)", String.Empty)
                    Totimins = DS.Tables(0).Compute("Sum(indMins)", String.Empty) / 60
                    Totijobs = DS.Tables(0).Compute("Sum(indJobs)", String.Empty)
                    rptDetailsForMT.DataSource = DS.Tables(0)
                    rptDetailsForMT.DataBind()

                    LblTMins1.Text = CInt(Totmins)
                    lblTotJobs1.Text = Totjobs
                    LblTiMins1.Text = CInt(Totimins)
                    lblTotiJobs1.Text = Totijobs
                Else
                    LblTMins1.Text = 0
                    lblTotJobs1.Text = 0
                    LblTiMins1.Text = 0
                    lblTotiJobs1.Text = 0
                End If
            Else
                LblTMins1.Text = 0
                lblTotJobs1.Text = 0
                LblTiMins1.Text = 0
                lblTotiJobs1.Text = 0
            End If
        Catch ex As Exception
            Response.Write(ex.Message)
        Finally
            clsRo = Nothing
            DS.Dispose()
        End Try
    End Sub
End Class
