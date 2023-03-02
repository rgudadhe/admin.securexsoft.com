
Partial Class LeaveAttendanceMainNew_Supervisor_EditShift
    Inherits BasePage
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            EName.Text = Request.QueryString("EName")
            SDate.Text = Request.QueryString("Dt")
            If Not Request.QueryString("SPre") = "NA" Then
                ddlShiftPrefix.Items.FindByValue(Trim(Request.QueryString("SPre"))).Selected = True
            End If


        End If
    End Sub
    Protected Function CheckShift() As Boolean
        Dim varReturn As Boolean = False

        Dim clsDR As ETS.BL.DutyRoster
        Try
            clsDR = New ETS.BL.DutyRoster
            clsDR.UserID = Trim(Request.QueryString("UserID"))
            clsDR.DutyDate = Trim(Request.QueryString("Dt"))

            clsDR.getDutyRosterDetails()
            If Not String.IsNullOrEmpty(clsDR.ShiftPrefix) Then
                varReturn = True
            End If

        Catch ex As Exception
            If Trim(UCase(ex.GetType.ToString)) <> Trim(UCase("System.Threading.ThreadAbortException")) Then
                Response.Write(ex.Message)
            End If
        Finally
            clsDR = Nothing
        End Try
        Return varReturn
    End Function
    Protected Sub btnSubmit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSubmit.Click
        Dim clsDR As ETS.BL.DutyRoster
        Dim varIntI As Integer = 0
        Try
            clsDR = New ETS.BL.DutyRoster
            clsDR.ShiftPrefix = Trim(ddlShiftPrefix.SelectedValue.ToString)
            clsDR.UserID = Trim(Request.QueryString("UserID"))
            clsDR.DutyDate = Trim(Request.QueryString("Dt"))
            clsDR.UpdateBy = Session("UserID").ToString
            clsDR.UpdateOn = Now
            If CheckShift() = True Then
                varIntI = clsDR.UpdateDutyRosterDetails()
            Else
                varIntI = clsDR.InsertDutyRosterDetails()
            End If

            If varIntI = 1 Then
                Response.Write("<center><font color=""#000080"">Shift updated sucessfully. !!!</font></center>")
                Response.Write("<center><a href=""../CloseWindow.aspx"">Close Window</a></center>")
                Response.End()
            End If
            
        Catch ex As Exception
            If Trim(UCase(ex.GetType.ToString)) <> Trim(UCase("System.Threading.ThreadAbortException")) Then
                Response.Write(ex.Message)
            End If
        Finally
            clsDR = Nothing
        End Try
    End Sub
End Class
