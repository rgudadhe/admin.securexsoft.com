Imports System.Web.Mail
Imports System.Net.Mail.SmtpClient
Imports System.Net.Mail
Imports System.Net.NetworkCredential
Imports MainModule
Partial Class USCancelDateSelection
    Inherits BasePage
    Dim varStrLeaveID As String
    Dim varStrStatus As String
    Dim varStrLeaveType As String
    Dim varMailFrom As String
    Dim varDtSDate As Date
    Dim varDtEDate As Date
    Dim varDtTodayDate As Date
    Dim varBolALFlag As Boolean
    Dim varStrFName As String
    Dim varStrLName As String
    Dim varobjMainModule As New MainModule
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            Dim clsLeave As ETS.BL.Leave
            Try
                varStrLeaveID = Request.QueryString("LeaveID")
                clsLeave = New ETS.BL.Leave
                clsLeave.LeaveID = varStrLeaveID
                clsLeave.getLeaveDetails()
                txtStartDate.Text = CDate(clsLeave.StartDate).ToShortDateString
                txtEndDate.Text = CDate(clsLeave.EndDate).ToShortDateString
            Catch ex As exception
                Response.Write(ex.Message)
            Finally
                clsLeave = Nothing
            End Try
        End If
    End Sub
    Protected Sub Submit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Submit.Click
        Dim clsLeave As ETS.BL.Leave
        Try
            clsLeave = New ETS.BL.Leave
            Dim varReturn As String = String.Empty
            clsLeave.LeaveID = Request.QueryString("LeaveID").ToString
            clsLeave.getLeaveDetails()
            varReturn = clsLeave.btn_CancelLeaveRequest(Trim(Request("txtStartDate").ToString), Trim(Request("txtEndDate").ToString))
            'Response.Write(varReturn.ToString)
            If Not String.IsNullOrEmpty(varReturn) Then
                Dim varTemp() As String
                Dim varStrTemp As String = String.Empty
                varTemp = Split(varReturn.ToString, "<BR>")
                If Trim(UCase(varTemp(0))) = Trim(UCase("True")) Then
                    lblMsg.Text = varReturn.ToString.Replace("True<BR>", String.Empty)
                    btnClose.Visible = True
                    tblMain.Visible = False
                Else
                    lblMsg.Text = varReturn.ToString.Replace("False<BR>", String.Empty)
                End If
            End If
        Catch ex As exception
            Response.Write(ex.Message)
        Finally
            clsLeave = Nothing
        End Try
    End Sub
End Class
