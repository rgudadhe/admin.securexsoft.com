Imports System.Data
Partial Class TATReport
    Inherits BasePage
    Protected Sub btnSubmit_ServerClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSubmit.ServerClick
        Dim clsERSS As New ETS.BL.ERSS
        Dim DTSearchParam As New DataTable
        Dim DS As New Data.DataSet
        Try
            clsERSS = New ETS.BL.ERSS()

            DTSearchParam = New DataTable
            DTSearchParam.Columns.Add(New DataColumn("ContractorID"))
            DTSearchParam.Columns.Add(New DataColumn("StartDate"))
            DTSearchParam.Columns.Add(New DataColumn("EndDate"))
            DTSearchParam.Columns.Add(New DataColumn("Hrs"))
            DTSearchParam.Columns.Add(New DataColumn("WorkGroupID"))

            Dim DR As Data.DataRow = DTSearchParam.NewRow

            DR("ContractorID") = Session("ContractorID").ToString
            DR("StartDate") = Request.Form("txtStartDate").ToString
            DR("EndDate") = Request.Form("txtEndDate").ToString
            DR("Hrs") = clsERSS.GetHrs(Request.Form("DropDownStatus").ToString)
            DR("WorkGroupID") = Session("WorkGroupID").ToString
            DTSearchParam.Rows.Add(DR)
            DS = clsERSS.GetTATReport(DTSearchParam)

            If DS.Tables.Count > 0 Then
                If DS.Tables(0).Rows.Count > 0 Then
                    TATReport1.DataSource = DS.Tables(0)
                    TATReport1.DataBind()
                Else
                    lblTickets.Text = "No Tickets availble !!!"
                    lblTickets.Visible = True
                End If
            Else
                lblTickets.Text = "No Tickets availble !!!"
                lblTickets.Visible = True
            End If

        Catch ex As Exception
            Response.Write(ex.Message)
        Finally
            clsERSS = Nothing
            DTSearchParam.Dispose()
            DS.Dispose()
        End Try
    End Sub
End Class
