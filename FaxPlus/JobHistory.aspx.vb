Imports System.Data
Partial Class FaxPlus_JobHistory
    Inherits BasePage
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim clsFP As ETS.BL.FaxPlus
        Dim DS As New DataSet
        Try
            clsFP = New ETS.BL.FaxPlus
            DS = clsFP.GetFaxPlusHistory(Request.QueryString("TransID").ToString, Request.QueryString("RPID").ToString)

            If DS.Tables.Count > 0 Then
                If DS.Tables(0).Rows.Count > 0 Then
                    rptHistory.DataSource = DS
                    rptHistory.DataBind()
                Else

                End If
            Else

            End If
        Catch ex As Exception
            Response.Write(ex.Message)
        Finally
            clsFP = Nothing
            DS.Dispose()
        End Try
    End Sub
End Class
