
Partial Class HL7_HL7History_New
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim varTransID As String = Request.QueryString("TransID").ToString
        If Not String.IsNullOrEmpty(varTransID) Then
            Dim clsHL7 As ETS.BL.HL7Reports
            Dim DS As New Data.DataSet
            Try
                clsHL7 = New ETS.BL.HL7Reports
                DS = clsHL7.SearchHL7ReportHistory(varTransID.ToString)
                If DS.Tables.Count > 0 Then
                    If DS.Tables(0).Rows.Count > 0 Then
                        rptHistory.DataSource = DS.Tables(0)
                        rptHistory.DataBind()
                    End If
                End If
            Catch ex As Exception
                Response.Write(ex.Message)
            Finally
                clsHL7 = Nothing
            End Try
        End If
    End Sub
End Class
