Partial Class LeaveAction
    Inherits BasePage
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then

            If Trim(UCase(Request("Str"))) = Trim(UCase("Approve")) Then
                Table1.Visible = False
                Dim clsLeave As ETS.BL.Leave
                Try
                    clsLeave = New ETS.BL.Leave
                    clsLeave.LeaveID = Request("LeaveID")
                    clsLeave.getLeaveDetails()
                    'Response.Write(clsLeave.btn_LeaveApproval(Session("UserID")))
                    If clsLeave.btn_LeaveApproval(Session("UserID")) = True Then
                        Response.Write("<script type=""text/javascript"" language=javascript> alert("" Leave Approved "");window.location.href='LeaveApproval.aspx';</script>")
                    Else
                        Response.Write("<script type=""text/javascript"" language=javascript> window.location.href='LeaveApproval.aspx';</script>")
                    End If

                Catch ex As Exception
                    Response.Write("<script type=""text/javascript"" language=javascript>alert(" & ex.Message & "); window.location.href='LeaveApproval.aspx';</script>")
                Finally
                    clsLeave = Nothing
                End Try
            End If
        End If
    End Sub
    Protected Sub btnDisapprove_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnDisApprove.Click
        Dim clsLeave As ETS.BL.Leave
        Try
            clsLeave = New ETS.BL.Leave
            clsLeave.LeaveID = Request.QueryString("LeaveID")
            clsLeave.getLeaveDetails()
            clsLeave.DisReason = txtReason.InnerText.ToString
            'Response.Write(Request.QueryString("LeaveID") & "#" & clsLeave.btn_LeaveDisApproval(Session("UserID")))
            If clsLeave.btn_LeaveDisApproval(Session("UserID")) = True Then
                Response.Write("<script type=""text/javascript"" language=javascript> alert("" Leave Not Approved "");window.location.href='LeaveApproval.aspx';</script>")
            Else
                Response.Write("<script type=""text/javascript"" language=javascript> window.location.href='LeaveApproval.aspx';</script>")
            End If

        Catch ex As Exception
            Response.Write(ex.Message)
        Finally
            clsLeave = Nothing
        End Try
    End Sub
End Class
