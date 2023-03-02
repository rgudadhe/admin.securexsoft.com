Imports MainModule
Partial Class HBALeaves_CancelDateSelection
    Inherits BasePage
    Dim objMainModule As New MainModule
    Dim varStrLeaveID As String
    Dim varDtTodayDate As Date
    Dim varDtSDate As Date
    Dim varDtEDate As Date
    Dim varStrStatus As String
    Dim varMailFrom As String
    Dim varStrFName As String
    Dim varStrLName As String
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        varStrLeaveID = Request.QueryString("LeaveID")


        Dim clsAtt As ETS.BL.Attendance
        Try
            clsAtt = New ETS.BL.Attendance
            varDtTodayDate = clsAtt.GetDateAfterDayLightChecking(Now())
        Catch ex As Exception
        Finally
            clsAtt = Nothing
        End Try

        Dim clsLeave As ETS.BL.Leave

        Try
            clsLeave = New ETS.BL.Leave
            clsLeave.LeaveID = varStrLeaveID
            clsLeave.getLeaveDetails()
            varDtSDate = clsLeave.StartDate
            varDtEDate = clsLeave.EndDate
            txtStartDate.Text = varDtSDate
            txtEndDate.Text = varDtEDate
        Catch ex As Exception
        Finally
            clsLeave = Nothing
        End Try

        If DateDiff("d", varDtEDate, varDtTodayDate) > 30 Then
            Response.Write("<center>You have no access to cancel this leave</center><BR>")
            Response.Write("<center><a href=""CloseWindow.aspx"">Close Window</a></center>")
            Response.End()
        End If

        txtStartDate.Text = varDtSDate
        txtEndDate.Text = varDtEDate
    End Sub
    Protected Sub Submit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Submit.Click
        Dim clsLeave As ETS.BL.Leave

        Try
            clsLeave = New ETS.BL.Leave

            clsLeave.LeaveID = Request.QueryString("LeaveID").ToString
            clsLeave.getLeaveDetails()

            Dim varReturn As String = String.Empty
            varReturn = clsLeave.btn_HBACancelLeaveRequest(Request.Form("txtStartDate"), Request.Form("txtEndDate"))

            If Not String.IsNullOrEmpty(varReturn) Then
                Dim varTemp() As String
                Dim varStrTemp As String = String.Empty
                varTemp = Split(varReturn.ToString, "<BR>")
                If Trim(UCase(varTemp(0))) = Trim(UCase("True")) Then
                    Response.Write("<center><font face=""Arial"" size=""2pt"" color=""#000080"">Your Leave from " & Request.Form("txtStartDate") & " to " & Request.Form("txtEndDate") & " has been Canceled.</font></center>")
                    Response.Write("<center><a href=""../CloseWindow.aspx"">Close Window</a></center>")
                    Response.End()
                Else
                    Response.Write("<center><font face=""Arial"" size=""2pt"" color=""#000080"">Your Leave from " & Request.Form("txtStartDate") & " to " & Request.Form("txtEndDate") & " has been not Canceled.</font></center>")
                    Response.Write("<center><a href=""../CloseWindow.aspx"">Close Window</a></center>")
                    Response.End()
                End If
            End If

        Catch ex As Exception
            Response.Write(ex.Message)
        Finally
            clsLeave = Nothing
        End Try
    End Sub
End Class
