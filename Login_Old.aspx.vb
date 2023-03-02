Imports EncryPass
Imports System.Data.SqlClient
Imports System.Data

Partial Class Login
    Inherits System.Web.UI.Page
    Private clsUserInfo As ETS.BL.Users
    Private ipaddress As String
#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub


    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        If Not Request.IsSecureConnection Then
            Dim redirectUrl As String = Request.Url.ToString().Replace("http:", "https:")
            Response.Redirect(redirectUrl)
        End If
        username.Focus()
    End Sub


    Protected Sub btnsubmit_Click(sender As Object, e As System.EventArgs) Handles btnSubmit.Click

        Dim blSececurity As New ETS.BL.BALSecurity
        Dim FailedAttempts As Integer = blSececurity.getUserFailedAttempts(username.Text)
        If FailedAttempts >= 3 Then
            lblErrorMsg.Text = "Your account has been locked. Please try Forgot Password link to unlock your account."
            lblErrorMsg.Visible = True
            Exit Sub

        End If

        clsUserInfo = New ETS.BL.Users(username.Text, password.Text)

        With clsUserInfo
            If .IsExists Then


                lblErrorMsg.Text = clsUserInfo.UserID.ToString
                lblErrorMsg.Visible = True
                Session("UserID") = clsUserInfo.UserID.ToString
                Session("clsUserInfo") = clsUserInfo
                'Response.Write(Session("clsUserInfo"))
                ' Exit Sub
                '***
                Dim DTUserPassCheck As DataTable = blSececurity.getUserLastPasswordChangeDate(Session("UserID"))

                If DTUserPassCheck.Rows.Count > 0 Then
                    Dim DRUserPassCheck As DataRow = DTUserPassCheck.Rows(0)
                    Dim dmodified As Date
                    Dim days As Integer
                    dmodified = DRUserPassCheck.Item("dmodified")
                    'Response.Write(dmodified)
                    days = (Now - dmodified).Days
                    'Response.Write(days)
                    'Exit Sub
                    If days > 90 Then
                        Response.Redirect("~/authorization/PReset90.aspx?uid=" & Session("UserID").ToString)
                    End If


                End If
                Dim DT As DataTable = blSececurity.getuserregistration(Session("UserID"))
                blSececurity.UpdateUserLog(Now(), .UserName, "User Loggedin Successfully", Session("UserID"), 0, ipaddress)
                'If blSececurity.getuserregistration(Session("UserID") then
                If DT.Rows.Count > 0 Then

                Else
                    Response.Redirect("~/authorization/NewUserEmail.aspx?uid=" & Session("UserID") & "&ufname=" & .FirstName & "&ulname=" & .LastName & "&uname=" & .UserName & "&emailid=" & .OfficialMailID)
                End If

                Session("UserName") = .FirstName & " " & .LastName
                Session("ContractorID") = .ContractorID
                Session("AdminLevel") = .AdminLevel
                Session("AccessLevel") = .AccessLevel
                Session("OwnerID") = .OwnerID
                Session("LoginID") = .UserName
                Session("DeptID") = .DepartmentID
                Session("ParentID") = IIf(String.IsNullOrEmpty(.ParentID), .ContractorID, .ParentID)
                Session("IsOwner") = .IsOwner
                Session("WorkGroupID") = .WorkGroupID
                If .IsOwner Then
                    Session("OwnerID") = .UserID
                End If
                If String.IsNullOrEmpty(.ParentID) Then
                    Session("IsContractor") = 1
                Else
                    Session("IsContractor") = 0
                End If

                '****
                If password.Text.Length < 8 Then
                    If (Not ClientScript.IsStartupScriptRegistered("OpenPopup")) Then
                        Page.ClientScript.RegisterStartupScript(Me.GetType(), "OpenPopup", "OpenModalPopup();", True)
                    End If
                    Exit Sub
                Else
                    'If clsUserInfo.UpdateLastLogin() = 1 Then
                    'Dim clsU As New ETS.BL.Users
                    'clsU.Lastlogin = Now
                    'clsU.UserID = Session("UserID")

                    'If clsU.UpdateUserDetails() = 1 Then

                    '    Response.Redirect("main.htm")
                    'Else
                    '    lblMessage.Text = "Login Failed"
                    'End If
                    'clsU = Nothing
                    Dim clsU As New ETS.BL.UsersLastLogin
                    clsU._WhereString.Append(" Where userid ='" & Session("UserID") & "' ")
                    Dim DSUsers As System.Data.DataSet = clsU.getUsersLastLogin()
                    If DSUsers.Tables(0).Rows.Count > 0 Then
                        clsU.UpdateLastLogin(Session("UserID"))
                    Else
                        clsU.Lastlogin = Now
                        clsU.UserID = Session("UserID")
                        clsU.InsertUserLastLogin()
                    End If
                    clsU = Nothing
                    Response.Redirect("main.htm")

                End If
            Else
                Label2.ForeColor = Drawing.Color.Red
                Label2.Text = "Incorret Username OR Password."
                Dim bludetails As New ETS.BL.BALSecurity
                Dim dt As DataTable = bludetails.getUserbyUsername(username.Text)
                Response.Write(dt.Rows.Count)
                If dt.Rows.Count > 0 Then
                    Dim dr As DataRow = dt.Rows(0)
                    Dim uid As String = dr("UserID").ToString
                    blSececurity.UpdateUserLog(Now(), .UserName, "User Login Failed", uid, 1, ipaddress)
                End If

            End If
        End With
        clsUserInfo = Nothing


    End Sub
    Public Function userregistrationcheck(ByVal userid As String) As Boolean

        Dim strConn As String = System.Configuration.ConfigurationManager.AppSettings("ETSCon")
        Dim strQuery As String = "Select * from tblusersecretq where uid='" & userid.ToString & "'"
        Dim userregchk As New SqlCommand(strQuery, New SqlConnection(strConn))
        Try
            userregchk.Connection.Open()
            Dim DRRec As SqlDataReader = userregchk.ExecuteReader()
            If DRRec.HasRows Then
                Return False
            Else
                Return True
            End If
        Catch ex As Exception
            Return False
            '    Response.Write(ex.Message)
        End Try
    End Function

    Protected Sub form1_Load(sender As Object, e As System.EventArgs) Handles frmLogin.Load
        Dim msg As String = Request.QueryString("msg")
        Label1.ForeColor = Drawing.Color.Green
        Label1.Font.Bold = True
        Label1.Text = msg
        ipaddress = HttpContext.Current.Request.UserHostAddress
        'Response.Write(HttpContext.Current.Request.UserHostAddress)
    End Sub


End Class


