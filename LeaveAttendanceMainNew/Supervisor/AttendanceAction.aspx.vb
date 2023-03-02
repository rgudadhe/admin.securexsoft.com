Imports MainModule
Partial Class AttendanceAction
    Inherits BasePage
    Dim varStrMailFrom As String
    Dim varStrToMail As String
    Dim varDtTodayDate As Date
    Dim varStrUserID As String
    Dim varDtAttDate As Date
    Dim varDtTempDate As Date
    Dim varDtTempAttDate As Date
    Dim varStrMailTo As String
    Dim varStrSignIn As String
    Dim varStrSignOut As String
    Dim varInTime As DateTime
    Dim varOutTime As DateTime
    Dim varITime As DateTime
    Dim varOTime As DateTime
    Dim varTimeSpan As TimeSpan
    Dim objMainModule As New MainModule
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            If Trim(UCase(Request("Str"))) = Trim(UCase("Approve")) Then
                'Session("UserID") = "67D011B2-60DE-45E5-B865-96A175784B05"
                Dim clsAttReq As ETS.BL.AttendanceRequest
                Try
                    Table1.Visible = False
                    clsAttReq = New ETS.BL.AttendanceRequest
                    clsAttReq.AttReqID = Request.QueryString("AttReqID")
                    clsAttReq.getAttendanceRequestDetails()
                    If clsAttReq.btn_AttendanceApprove(Session("UserId")) = True Then
                        Response.Write("<script type=""text/javascript"" language=javascript> alert("" Attendance Sanctioned "");window.location.href='AttendanceApproval.aspx';</script>")
                    Else
                        Response.Write("<script type=""text/javascript"" language=javascript> window.location.href='AttendanceApproval.aspx';</script>")
                    End If

                Catch ex As Exception
                Finally
                    clsAttReq = Nothing
                End Try
            End If
        End If
    End Sub
    Protected Sub btnDisapprove_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnDisapprove.Click
        ' Session("UserID") = "67D011B2-60DE-45E5-B865-96A175784B05"
        Dim clsAttReq As ETS.BL.AttendanceRequest
        Try
            clsAttReq = New ETS.BL.AttendanceRequest
            clsAttReq.AttReqID = Request.QueryString("AttReqID")
            clsAttReq.getAttendanceRequestDetails()
            clsAttReq.DisReason = Replace(Request.Form("txtReason"), "'", "''")
            If clsAttReq.btn_AttendanceDisApprove(Session("UserId")) = True Then
                Response.Write("<script type=""text/javascript"" language=javascript> alert("" Attendance Not Sanctioned "");window.location.href='AttendanceApproval.aspx';</script>")
            Else
                Response.Write("<script type=""text/javascript"" language=javascript> window.location.href='AttendanceApproval.aspx';</script>")
            End If
        Catch ex As Exception
        Finally
            clsAttReq = Nothing
        End Try
    End Sub
End Class
