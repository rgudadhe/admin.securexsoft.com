Partial Class LeaveCancelAction
    Inherits BasePage
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim clsLeave As ETS.BL.Leave
        Try
            clsLeave = New ETS.BL.Leave
            clsLeave.LeaveID = Request.QueryString("LeaveID")
            clsLeave.getLeaveDetails()
            'Response.Write(clsLeave.btn_CancelLeaveAction(Session("UserID").ToString))
            If clsLeave.btn_CancelLeaveAction(Session("UserID").ToString) = True Then
                Response.Write("<script type=""text/javascript"" language=javascript> alert("" Leave Canceled "");window.location.href='CancelApproval.aspx';</script>")
            Else
                Response.Write("<script type=""text/javascript"" language=javascript> window.location.href='CancelApproval.aspx';</script>")
            End If

        Catch ex As Exception
        Finally
            clsLeave = Nothing
        End Try
    End Sub
End Class
