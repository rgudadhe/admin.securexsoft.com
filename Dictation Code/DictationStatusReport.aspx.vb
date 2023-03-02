Imports System
Imports System.Data
Partial Class Dictation_Code_DictationStatusReport
    Inherits BasePage
    Protected Sub GetResult()
        Dim varIntDays As Integer = 0
        varIntDays = ddlDays.Items(ddlDays.SelectedIndex).Value
        If varIntDays > 0 Then
            Dim clsPhy As New ETS.BL.Physicians
            Dim DSInActivePhy As DataSet = clsPhy.getInactiveDictators(Session("contractorid").ToString, varIntDays, Session("WorkGroupID"))
            clsPhy = Nothing
            rptDetails.DataSource = DSInActivePhy
            rptDetails.DataBind()
            DSInActivePhy.Dispose()
            If rptDetails.Items.Count > 0 Then
                Panel1.Visible = True
            Else
                Response.Write("<script language='javascript'>alert('No Records found');window.location.href='DictationCodeReport.aspx';</script>")
            End If
        End If
    End Sub
    Protected Sub btnSubmit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSubmit.Click
        GetResult()
    End Sub
    Protected Sub LnkExport_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles LnkExport.Click
        Dim stringWrite As New System.IO.StringWriter()                 'create a string writer
        Dim htmlWrite As New System.Web.UI.HtmlTextWriter(stringWrite)  'create an htmltextwriter which uses the stringwriter
        Response.Clear()                                                'clean up the response.object
        Response.Charset = ""

        Dim filename = "Dictation Status Report " & Now & " .xls"
        Response.AddHeader("content-disposition", "attachment;filename=" & filename)
        GetResult()
        Response.ContentType = "application/vnd.ms-excel"               'set the response mime type for excel
        rptDetails.RenderControl(htmlWrite)                                     'render datagrid to htmltextwriter
        Response.Write(stringWrite.ToString())
        'response.Write("-- end of report --")
        Response.End()
    End Sub
End Class
