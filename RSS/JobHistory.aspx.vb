
Partial Class RSS_JobHistory
    Inherits System.Web.UI.Page
    Public Function getStatus(ByVal blnStatus) As String
        If String.IsNullOrEmpty(blnStatus) Then
            getStatus = "Pending Re-Import"
            'Dim lnk As LinkButton = rptPhy.FindControl("LinkButton1")
            'lnk.Visible = False
        Else
            If blnStatus Then
                getStatus = "Imported"
            Else
                getStatus = "Failed"
            End If
        End If
    End Function
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            If Not String.IsNullOrEmpty(Request.QueryString("MD5Value").ToString) Then
                hdnMD5Value.Value = Request.QueryString("MD5Value").ToString
                BindData()
            End If
        End If
    End Sub
    Protected Sub BindData()
        Dim CLSFIL As ETS.BL.FileImportLog
        Dim DS As New Data.DataSet
        Try
            CLSFIL = New ETS.BL.FileImportLog
            DS = CLSFIL.getFileImportHistory(Session("ContractorID").ToString, hdnMD5Value.Value.ToString)
            If DS.Tables.Count > 0 Then
                If DS.Tables(0).Rows.Count > 0 Then
                    rptHistory.DataSource = DS
                    rptHistory.DataBind()
                End If
            End If
        Catch ex As Exception
            Response.Write(ex.Message)
        Finally
            CLSFIL = Nothing
            DS = Nothing
        End Try
    End Sub
End Class
