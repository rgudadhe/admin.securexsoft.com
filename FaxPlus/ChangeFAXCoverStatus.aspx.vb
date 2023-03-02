
Partial Class FaxPlus_ChangeFAXCoverStatus
    Inherits System.Web.UI.Page
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim clsFAX As ETS.BL.FaxPlus
        Dim varStrCoverID As String = String.Empty
        Try
            clsFAX = New ETS.BL.FaxPlus
            varStrCoverID = Request.Form("CoverID")
            'varstrCoverID = "7C219ADE-9999-43F7-954D-0BB1AB4D28CB"
            'Response.write(clsFAX.UpdateFAXCoverPageStatus(varStrCoverID.ToString))
            If Not String.IsNullOrEmpty(varStrCoverID.ToString) Then
                If clsFAX.UpdateFAXCoverPageStatus(varStrCoverID.ToString) = True Then
                    Response.Write("1")
                Else
                    Response.Write("0")
                End If
            End If
        Catch ex As Exception
            Response.Write(ex.message)
        Finally
            clsFAX = Nothing
        End Try
    End Sub
End Class
