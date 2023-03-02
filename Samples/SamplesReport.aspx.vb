Imports System
Imports System.Data
Partial Class Samples_SamplesReport
    Inherits BasePage
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        
        Dim clsSamples As New ETS.BL.Samples
        Dim DSSamples As DataSet
        With clsSamples
            DSSamples = .getSampleReport(Session("ContractorID").ToString, Session("WorkGroupID").ToString)
        End With
        clsSamples = Nothing
        Dim DR() As DataRow
        DR = DSSamples.Tables(0).Select("Counter>0")
        rptDetails.DataSource = DR
        rptDetails.DataBind()
        DR = DSSamples.Tables(0).Select("Counter=0")
        rptDetails1.DataSource = DR
        rptDetails1.DataBind()
        DSSamples.Dispose()
        DR = Nothing
    End Sub
    Protected Sub LnkExport_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles LnkExport.Click
        ExportResult(rptDetails, "Samples Available " & Now & " .xls")
    End Sub
    Protected Sub ExportResult(ByVal Ctrl As Repeater, ByVal filename As String)
        Dim stringWrite As New System.IO.StringWriter()
        Dim htmlWrite As New System.Web.UI.HtmlTextWriter(stringWrite)
        Dim dg As New DataGrid()
        Response.Clear()
        Response.Charset = ""

        Response.AddHeader("content-disposition", "attachment;filename=" & filename)
        Response.ContentType = "application/vnd.ms-excel"

        Ctrl.RenderControl(htmlWrite)
        Response.Write(stringWrite.ToString())
        Response.End()
    End Sub
    Protected Sub LnkExport1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles LnkExport1.Click
        ExportResult(rptDetails1, "Samples NOT Available " & Now & " .xls")
    End Sub
End Class
