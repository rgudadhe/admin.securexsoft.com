
Partial Class UpdateLeaveBalance
    Inherits BasePage
    Dim varStrUserID As String
    Dim varStrEmpName As String
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim varDblCL As Double
        Dim varDblEL As Double
        Dim varStrWOff1 As String = String.Empty
        Dim varStrWOff2 As String = String.Empty
        varStrUserID = Replace(Request.QueryString("UserID").ToString, "'", "")

        WeekOff2.Enabled = True
        If Not Page.IsPostBack Then

            Dim clsLB As ETS.BL.LeaveBalance
            Dim clsUsr As ETS.BL.Users
            Try
                If Trim(UCase(Request.QueryString("Action"))) = Trim(UCase("Edit")) Then
                    clsUsr = New ETS.BL.Users(varStrUserID.ToString)
                    varStrEmpName = clsUsr.FirstName & " " & clsUsr.LastName
                    clsLB = New ETS.BL.LeaveBalance
                    clsLB.UserID = varStrUserID.ToString
                    clsLB.getLeaveBalanceDetails()
                    varDblCL = clsLB.CL
                    varDblEL = clsLB.EL

                    varStrWOff1 = clsLB.WeeklyOff1
                    varStrWOff2 = clsLB.WeeklyOff2

                    If Not String.IsNullOrEmpty(varStrWOff2) Then
                        WeekOff2.Enabled = True
                        Dim ckCtrl As HtmlInputCheckBox = CType(Me.FindControl("CheckBox1"), HtmlInputCheckBox)
                        ckCtrl.Checked = True
                    End If


                    EmpName.Text = varStrEmpName
                    CL.Text = varDblCL
                    EL.Text = varDblEL
                    If varStrWOff1 <> "" Then
                        WeekOff1.Text = varStrWOff1
                    Else
                        WeekOff1.SelectedIndex = 0
                    End If
                    If varStrWOff2 <> "" Then
                        WeekOff2.Text = varStrWOff2
                    Else
                        WeekOff2.SelectedIndex = 0
                    End If

                ElseIf Trim(UCase(Request.QueryString("Action"))) = Trim(UCase("Add")) Then

                    Button1.Text = "ADD"

                    clsUsr = New ETS.BL.Users(varStrUserID)
                    varStrEmpName = clsUsr.FirstName & " " & clsUsr.LastName

                    EmpName.Text = varStrEmpName
                    CL.Text = 0.0
                    EL.Text = 0.0

                End If
            Catch ex As Exception
                Response.Write(ex.Message)
            Finally
                clsUsr = Nothing
                clsLB = Nothing
            End Try
        End If
    End Sub
    Protected Sub Button1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button1.Click
        Dim varDblCL As Double
        Dim varDblEL As Double
        Dim varDblTL As Double
        Dim varStrWOff1 As String
        Dim varStrWOff2 As String
        Dim varStrEName As String
        Dim clsUsr As ETS.BL.Users
        Dim clsLB As ETS.BL.LeaveBalance

        Try
            clsUsr = New ETS.BL.Users(varStrUserID.ToString)
            varStrEmpName = clsUsr.FirstName & " " & clsUsr.LastName
            clsLB = New ETS.BL.LeaveBalance
            clsLB.UserID = varStrUserID.ToString

            varDblCL = Request.Form("CL")
            clsLB.CL = varDblCL
            varDblEL = Request.Form("EL")
            clsLB.EL = varDblEL
            varDblTL = CDbl(varDblCL) + CDbl(varDblEL)
            clsLB.TL = varDblTL
            varStrWOff1 = Request.Form("WeekOff1")
            clsLB.WeeklyOff1 = varStrWOff1

            If Checkbox1.Checked = True And Request.Form("WeekOff2") <> "" Then
                varStrWOff2 = Request.Form("WeekOff2")
                clsLB.WeeklyOff2 = varStrWOff2
            End If
            If Checkbox1.Checked = False Then
                varStrWOff2 = String.Empty
                clsLB.WeeklyOff2 = varStrWOff2
            End If

            If Trim(UCase(Button1.Text)) = Trim(UCase("ADD")) Then
                
                If clsLB.InsertLeaveBalanceDetails() = 1 Then
                    Response.Redirect("LeaveBalance.aspx")
                End If

                Response.Write("<center><font face=""Trebuchet MS"" size=""2"" color=""#000080"">Leave Balance of " & varStrEName & " added sucessfully !!!.</font></center><BR>")
                Response.Write("<center><a href=""LeaveBalance.aspx"">BACK</a></center>")
                Response.End()
            ElseIf Trim(UCase(Button1.Text)) = Trim(UCase("UPDATE")) Then
                
                If clsLB.UpdateLeaveBalanceDetails() = 1 Then
                    Response.Redirect("LeaveBalance.aspx")
                End If

                Response.Write("<center><font face=""Trebuchet MS"" size=""2"" color=""#000080"">Leave Balance of " & varStrEName & " Updated sucessfully !!!.</font></center><BR>")
                Response.Write("<center><a href=""LeaveBalance.aspx"">BACK</a></center>")
                Response.End()
            End If
        Catch ex As Exception
            Response.Write(ex.Message)
        Finally
            clsUsr = Nothing
            clsLB = Nothing
        End Try
    End Sub
    Protected Sub BtnCancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtnCancel.Click
        Response.Redirect("LeaveBalance.aspx")
    End Sub
End Class
