Imports MainModule
Partial Class USLeaveRequest
    Inherits BasePage
    Dim objMainModule As New MainModule
    Dim varStrState As String
    Dim varStrDeptID As String
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'Response.Write(Session("UserId").ToString)
        If Not Page.IsPostBack Then
            Dim clsUsr As ETS.BL.Users
            Dim clsLeave As ETS.BL.Leave
            Try
                clsUsr = New ETS.BL.Users(Session("UserID").ToString)
                If Not String.IsNullOrEmpty(clsUsr.Gender.ToString) Then
                    If Trim(UCase(clsUsr.Gender.ToString)) = Trim(UCase("FeMale")) Then
                        Dim varListItemML As New ListItem
                        varListItemML.Text = "Maternity Leave "
                        varListItemML.Value = "ML"
                        DropLeaveType.Items.Add(varListItemML)
                    End If
                End If
                clsLeave = New ETS.BL.Leave
                clsLeave.ContractorID = Session("ContractorID").ToString
                'Response.Write(Session("UserID").ToString & "Supervisor ID:" & clsLeave.GetSupervisorID(Session("UserID").ToString))
                'Response.Write(clsLeave.CheckGuid("76d1fbad-499d-466d-a56e-ae22fb509c21"))

                'If clsLeave.IsReligiousLeaveAvialByUsr(Session("UserID").ToString, Session("ContractorID").ToString) = False Then
                '    Dim varLstRL As New ListItem
                '    varLstRL.Text = "Religious Leave"
                '    varLstRL.Value = "RL"
                '    DropLeaveType.Items.Add(varLstRL)
                'End If
            Catch ex As exception
                Response.Write(ex.Message)
            Finally
                clsUsr = Nothing
                clsLeave = Nothing
            End Try
        End If
    End Sub
    Protected Sub SendLR_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles SendLR.Click
        Dim clsLeave As ETS.BL.Leave
        Try
            clsLeave = New ETS.BL.Leave
            clsLeave.UserID = Session("UserID").ToString
            clsLeave.ContractorID = Session("ContractorID").ToString
            clsLeave.StartDate = Request("txtStartDate")
            clsLeave.EndDate = Request("txtEndDate")
            clsLeave.TypeOfLeave = Request("DropLeaveType").ToString
            clsLeave.Reason = Replace(Request("textArea1"), "'", "''")
            'Response.Write(Request("txtStartDate"))

            Dim varReturn As String = String.Empty
            varReturn = clsLeave.btn_SendLeaveRequest()
            Response.Write(varReturn)
            'Label1.Text = varReturn.ToString
            lblMsg.Text = "Leave Application Sent testing " & varReturn.ToString
            If Not String.IsNullOrEmpty(varReturn) Then
                Dim varTemp() As String
                Dim varStrTemp As String = String.Empty
                varTemp = Split(varReturn.ToString, "<BR>")
                If Trim(UCase(varTemp(0))) = Trim(UCase("True")) Then
                    lblMsg.Text = "Leave Application Sent " & varTemp(0).ToString.Replace("True", String.Empty)
                Else
                    lblMsg.Text = varTemp(0).ToString.Replace("False", String.Empty)
                End If
            End If

        Catch ex As exception
            Response.Write("Error ON The page :" & ex.Message)
        Finally
            clsLeave = Nothing
        End Try
    End Sub
End Class
