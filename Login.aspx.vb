Imports EncryPass
Imports System.Data.SqlClient
Imports System.Data
Imports System.IO
Imports System.Net.Mail
Imports System.Net

Partial Class Loginnew
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
        Session("UserID") = Nothing
        InitializeComponent()
    End Sub

#End Region

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        If Not Request.IsSecureConnection Then

            Dim redirectUrl As String = Request.Url.ToString().Replace("http:", "https:")
            Response.Redirect(redirectUrl)
        End If
        Response.Cache.SetExpires(DateTime.Now)
        username.Focus()
    End Sub


    Protected Sub btnsubmit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSubmit.Click

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

                blSececurity.ClrearUsersFailedAttempts(username.Text)
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
                    'If days > 90 Then
                    '    Response.Redirect("~/authorization/PReset90.aspx?uid=" & Session("UserID").ToString)
                    'End If


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

                    Try
                        'GET USER OS, BROWSER AND DEVICE


                        Dim SQLSTR As String = ConfigurationManager.ConnectionStrings("ETSConnectionString").ConnectionString
                        Dim oConn As New SqlConnection(SQLSTR)
                        Dim DS1 As New DataSet
                        Dim DS2 As New DataSet
                        Dim browser As String
                        Dim trustd As String = String.Empty
                        Dim device As String
                        Dim os As String
                        Dim ip As String
                        Dim brow As String

                        If Request.UserAgent.ToString.IndexOf("crios", StringComparison.OrdinalIgnoreCase) >= 0 Then
                            browser = "Chrome"
                            '    BrowserVersion = String.Empty
                        Else
                            browser = Request.Browser.Browser
                            '   BrowserVersion = Request.Browser.Version
                        End If
                        Dim sqlCommand1 As New SqlCommand
                        sqlCommand1.CommandText = "SF_getUserDeviceDetails"
                        sqlCommand1.CommandType = CommandType.StoredProcedure
                        sqlCommand1.Connection = oConn
                        oConn.Open()
                        sqlCommand1.Parameters.AddWithValue("@userid", Session("userid").ToString)
                        sqlCommand1.Parameters.AddWithValue("@os", GetOS())
                        sqlCommand1.Parameters.AddWithValue("@browser", browser)
                        sqlCommand1.Parameters.AddWithValue("@device", GetDevice())
                        sqlCommand1.Parameters.AddWithValue("@ip", ipaddress)
                        sqlCommand1.Parameters.AddWithValue("@app", "SXF")
                        Dim DA As SqlDataAdapter = New SqlDataAdapter(sqlCommand1)
                        DA.Fill(DS1)
                        DA.Dispose()
                        oConn.Close()
                        Response.Write(DS1.Tables(0).Rows.Count)
                        ' Response.End()
                        If DS1.Tables(0).Rows.Count > 0 Then
                            trustd = DS1.Tables(0).Rows(0)("trusted")
                            If trustd = 1 Then
                                InsertUserDeviceDetails(Session("userid").ToString)
                                Response.Redirect("main.htm")
                            Else
                                GoTo h
                            End If
                        Else
h:                          Dim email As String = String.Empty
                            Dim bludetails As New ETS.BL.BALSecurity
                            Dim dt1 As DataTable = bludetails.getUserbyUsername(username.Text)
                            'Response.Write(dt.Rows.Count)
                            If dt1.Rows.Count > 0 Then
                                Dim dr As DataRow = dt1.Rows(0)
                                email = dr("OfficialmailId").ToString
                            End If
                            sendOTPwithLink(Session("userid").ToString)
                            Response.Redirect("~/authorization/NewUserEnvironment.aspx?rlink=" & base64Encode("main.htm") & "&eid=" & base64Encode(email.ToString) & "&id=" & base64Encode(username.Text))
                        End If

                    Catch es As Exception
                        Response.Write(es.Message)
                    End Try




                    'Response.Redirect("main.htm")

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
    Private Function base64Encode(ByVal sData As String) As String
        Try
            Dim encData_byte As Byte() = New Byte(sData.Length - 1) {}
            encData_byte = System.Text.Encoding.UTF8.GetBytes(sData)
            Dim encodedData As String = Convert.ToBase64String(encData_byte)
            Return (encodedData)
        Catch ex As Exception
            Throw (New Exception("Error in base64Encode" & ex.Message))
        End Try
    End Function
    Public Sub sendOTPwithLink(ByVal uid As String)
        Dim Ufname As String = String.Empty
        Dim ULname As String = String.Empty
        Dim acid As String = String.Empty
        Dim email As String = String.Empty
        Dim otp As Integer = GenerateOTP()
        Dim blUserDetails As New ETS.BL.BALSecurity
        Dim acdesc As String = String.Empty
        Dim Dt As DataTable = blUserDetails.getUserbyUsername(username.Text)
        If Dt.Rows.Count > 0 Then
            Dim DR As DataRow = Dt.Rows(0)
            Ufname = DR("FirstName").ToString
            email = DR("OfficialmailId").ToString
            ULname = DR("LastName").ToString

        End If

        Dim constr As String = ConfigurationManager.ConnectionStrings("ETSConnectionString").ConnectionString

        'Delete existing OTP from the table for the user
        Using con As New SqlConnection(constr)
            Using cmd As New SqlCommand("Delete UserOTP where userid=@UserId")
                Using sda As New SqlDataAdapter()
                    cmd.CommandType = CommandType.Text
                    cmd.Parameters.AddWithValue("@UserId", uid)
                    cmd.Connection = con
                    con.Open()
                    cmd.ExecuteNonQuery()
                    con.Close()
                End Using
            End Using
        End Using

        ' Insert new OTP to the table
        Using con As New SqlConnection(constr)
            Using cmd As New SqlCommand("INSERT INTO UserOTP VALUES(@UserId, @OTPCode,@cdate)")
                Using sda As New SqlDataAdapter()
                    cmd.CommandType = CommandType.Text
                    cmd.Parameters.AddWithValue("@UserId", uid)
                    cmd.Parameters.AddWithValue("@OTPCode", otp)
                    cmd.Parameters.AddWithValue("@cdate", Now())
                    cmd.Connection = con
                    con.Open()
                    cmd.ExecuteNonQuery()
                    con.Close()
                End Using
            End Using
        End Using



        'Dim MAILER As New SASMTPLib.CoSMTPMail
        'Dim reader As New StreamReader(Server.MapPath("~/authorization/NewUEnvOTP_.htm"))
        'Dim readFile As String = reader.ReadToEnd()
        'Dim myString As String = ""
        'myString = readFile
        'myString = myString.Replace("$$UNAME$$", ULname)
        'myString = myString.Replace("$$CODE$$", otp)
        ''myString = myString.Replace("$$BTEXT$$", "Activation Code")
        ''myString = myString.Replace("$$EVERIFY$$", "https://secureofficedev.securexsoft.com/authorization/PreUserRegistration2.aspx?activationcode=" & activationcode & "&uid=" & uid & "&emailid=" & email & "&vdate=" & Now() & "&acdes=" & acdesc.ToString)
        ''myString = myString.Replace("$$BODY2$$", "<p>If you fail to register within 4 hr, the<b> Activation Code </b>will no longer be valid. And, you will have to start the process for receiving a new <b>Activation Code</b>. </p><p>If you need help, please contact our HelpDesk at support@medofficepro.com or call 866-510-1111 x 11.</p><br><br>Thanks,<br><b>MedOfficePro Support</b><br>T: 866-510-1111 x 11 <br>E: support@medofficepro.com<br>www.medofficepro.com<br><br><b>We Better the Business of Medicine")
        'Dim body As String = "Hi " & Ufname & ","
        'MAILER.FromName = "Technical Help Desk"
        'MAILER.FromAddress = "techsupport@edictate.com"
        'MAILER.RemoteHost = "secure.emailsrvr.com"
        'MAILER.UserName = "alert@edictate.com"
        'MAILER.Password = "Welcome@medofficepro2011"
        'MAILER.AddRecipient(email)
        ''MAILER.AddRecipient("vraut@edictate.com")
        'MAILER.Priority = 1
        'MAILER.Urgent = True
        'MAILER.HtmlText = myString.ToString
        'MAILER.Subject = "Authentication Code for SecureXFlow"
        'MAILER.SendMail()
        'MAILER = Nothing


        Dim message As New MailMessage()
        Dim fromName As String = "Do Not Reply"
        Dim from As String = "donotreply@medofficepro.com"
        Dim toAddress As String = email.ToString
        'Dim bccaddress As String = "sdoxreg@edictate.com"
        Dim smtpadd As String = "email-smtp.us-west-2.amazonaws.com"
        Dim smtpuname As String = "AKIA44IE6PBA24MEZW5P"
        Dim smtppass As String = "BLZJ9U1M6AILVx4FRA8E1CdRvOoRV9rx7/HBXEcNaeJ6"
        Dim port As Integer = 587
        Dim subject As String = "Authentication Code for SecureXFlow"
        Dim configset As String = "ConfigSet"

        message.IsBodyHtml = True
        message.From = New MailAddress(from, fromName)
        message.To.Add(New MailAddress(toAddress))
        message.Subject = "Authentication Code for SecureXFlow"""


        Dim reader As New StreamReader(Server.MapPath("~/authorization/NewUEnvOTP_.htm"))
        Dim readFile As String = reader.ReadToEnd()
        Dim myString As String = ""
        myString = readFile
        myString = myString.Replace("$$UNAME$$", ULname)
        myString = myString.Replace("$$CODE$$", otp)
        message.IsBodyHtml = True
        message.Body = myString.ToString
        message.From = New MailAddress(from, fromName)
        message.To.Add(New MailAddress(toAddress))

        message.Subject = subject
        'message.Headers.Add("X-SES-CONFIGURATION-SET", configset)
        Dim client As New System.Net.Mail.SmtpClient(smtpadd, port)

        client.Credentials = New NetworkCredential(smtpuname, smtppass)
        client.EnableSsl = True
        client.Send(message)


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

    Protected Sub form1_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles frmLogin.Load
        Dim msg As String = Request.QueryString("msg")
        Label1.ForeColor = Drawing.Color.Green
        Label1.Font.Bold = True
        Label1.Text = msg
        ipaddress = HttpContext.Current.Request.UserHostAddress
        Session("ipaddress") = ipaddress
        'Response.Write(HttpContext.Current.Request.UserHostAddress)
    End Sub
    Public Function GetOS() As String
        Dim MyAgent As String = Request.UserAgent.ToString().ToLower
        If MyAgent.IndexOf("windows nt 10.0") >= 0 Then
            Return "windows 10"
        ElseIf MyAgent.IndexOf("windows nt 6.3") >= 0 Then
            Return "windows 8.1"
        ElseIf MyAgent.IndexOf("windows nt 6.2") >= 0 Then
            Return "windows 8"
        ElseIf MyAgent.IndexOf("windows nt 6.1") >= 0 Then
            Return "windows 7"
        ElseIf MyAgent.IndexOf("windows nt 6.0") >= 0 Then
            Return "windows vista"
        ElseIf MyAgent.IndexOf("windows nt 5.2") >= 0 Then
            Return "windows server 2003"
        ElseIf MyAgent.IndexOf("windows nt 5.1") >= 0 Then
            Return "windows xp"
        ElseIf MyAgent.IndexOf("windows nt 5.01") >= 0 Then
            Return "windows 2000 (sp1)"
        ElseIf MyAgent.IndexOf("windows nt 5.0") >= 0 Then
            Return "windows 2000"
        ElseIf MyAgent.IndexOf("windows nt 4.5") >= 0 Then
            Return "windows nt 4.5"
        ElseIf MyAgent.IndexOf("windows nt 4.0") >= 0 Then
            Return "windows nt 4.0"
        ElseIf MyAgent.IndexOf("win 9x 4.90") >= 0 Then
            Return "windows me"
        ElseIf MyAgent.IndexOf("windows 98") >= 0 Then
            Return "windows 98"
        ElseIf MyAgent.IndexOf("windows 95") >= 0 Then
            Return "windows 95"
        ElseIf MyAgent.IndexOf("windows ce") >= 0 Then
            Return "windows ce"
        ElseIf (MyAgent.Contains("ipad")) Then
            Return String.Format("ipad os {0}", GetMobileVersion(MyAgent, "os"))
        ElseIf (MyAgent.Contains("iphone")) Then
            Return String.Format("iphone os {0}", GetMobileVersion(MyAgent, "os"))
        ElseIf (MyAgent.Contains("linux") AndAlso MyAgent.Contains("kfapwi")) Then
            Return "kindle fire"
        ElseIf (MyAgent.Contains("rim tablet") OrElse (MyAgent.Contains("bb") AndAlso MyAgent.Contains("mobile"))) Then
            Return "black berry"
        ElseIf (MyAgent.ToLower.Contains("windows phone")) Then
            Return String.Format("windows phone {0}", GetMobileVersion(MyAgent, "windows phone"))
        ElseIf (MyAgent.Contains("mac os")) Then
            Return "mac os"
        ElseIf MyAgent.ToLower.IndexOf("android") >= 0 Then
            Return String.Format("android {0}", GetMobileVersion(MyAgent.ToLower, "android"))
        Else
            Return "os is unknown."
        End If

    End Function
    Public Function GetDevice() As String
        Dim MyAgent As String = Request.UserAgent.ToString().ToLower
        If MyAgent.IndexOf("windows nt 10.0") >= 0 Then
            Return "Desktop"
        ElseIf MyAgent.IndexOf("windows nt 6.3") >= 0 Then
            Return "Desktop"
        ElseIf MyAgent.IndexOf("windows nt 6.2") >= 0 Then
            Return "Desktop"
        ElseIf MyAgent.IndexOf("windows nt 6.1") >= 0 Then
            Return "Desktop"
        ElseIf MyAgent.IndexOf("windows nt 6.0") >= 0 Then
            Return "Desktop"
        ElseIf MyAgent.IndexOf("windows nt 5.2") >= 0 Then
            Return "Desktop"
        ElseIf MyAgent.IndexOf("windows nt 5.1") >= 0 Then
            Return "Desktop"
        ElseIf MyAgent.IndexOf("windows nt 5.01") >= 0 Then
            Return "Desktop"
        ElseIf MyAgent.IndexOf("windows nt 5.0") >= 0 Then
            Return "Desktop"
        ElseIf MyAgent.IndexOf("windows nt 4.5") >= 0 Then
            Return "Desktop"
        ElseIf MyAgent.IndexOf("windows nt 4.0") >= 0 Then
            Return "Desktop"
        ElseIf MyAgent.IndexOf("win 9x 4.90") >= 0 Then
            Return "Desktop"
        ElseIf MyAgent.IndexOf("windows 98") >= 0 Then
            Return "Desktop"
        ElseIf MyAgent.IndexOf("windows 95") >= 0 Then
            Return "Desktop"
        ElseIf MyAgent.IndexOf("windows ce") >= 0 Then
            Return "Desktop"
        ElseIf (MyAgent.Contains("ipad")) Then
            Return "IPad"
        ElseIf (MyAgent.Contains("iphone")) Then
            Return "IPhone"
        ElseIf (MyAgent.Contains("linux") AndAlso MyAgent.Contains("kfapwi")) Then
            Return "kindle fire"
        ElseIf (MyAgent.Contains("rim tablet") OrElse (MyAgent.Contains("bb") AndAlso MyAgent.Contains("mobile"))) Then
            Return "black berry"
        ElseIf (MyAgent.ToLower.Contains("windows phone")) Then
            Return "windows phone"
        ElseIf (MyAgent.Contains("mac os")) Then
            Return "MacBook"
        ElseIf MyAgent.ToLower.IndexOf("android") >= 0 Then
            Return "Android Phone"
        Else
            Return "os is unknown."
        End If

    End Function
    Private Function GetMobileVersion(userAgent As String, device As String) As String
        Dim ReturnValue As String = String.Empty
        Dim RawVersion As String = userAgent.Substring(userAgent.IndexOf(device) + device.Length).TrimStart()
        For Each character As Char In RawVersion
            If IsNumeric(character) Then
                ReturnValue &= character
            ElseIf (character = "." OrElse character = "_") Then
                ReturnValue &= "."
            Else
                Exit For
            End If
        Next
        Return ReturnValue
    End Function

    Protected Sub InsertUserDeviceDetails(UserID As String)
        Dim strConn As String
        Dim SQLSTR As String = System.Configuration.ConfigurationManager.AppSettings("ETSConnectionString")
        Dim oConn As New SqlConnection(SQLSTR)
        Dim Device As String
        Dim OS As String
        Dim Browser As String
        Dim BrowserVersion As String
        Dim Ret As Integer = 0
        Try
            Device = GetDevice()
            OS = GetOS()
            If Request.UserAgent.ToString.IndexOf("crios", StringComparison.OrdinalIgnoreCase) >= 0 Then
                Browser = "Chrome"
                BrowserVersion = String.Empty
            Else
                Browser = Request.Browser.Browser
                BrowserVersion = Request.Browser.Version
            End If
            Dim sqlCommand As New SqlCommand
            sqlCommand.CommandText = "SF_InsertUserDeviceDetails"
            sqlCommand.CommandType = CommandType.StoredProcedure
            oConn.Open()
            sqlCommand.Connection = oConn
            sqlCommand.Parameters.AddWithValue("@device", Device)
            sqlCommand.Parameters.AddWithValue("@OS", OS)
            sqlCommand.Parameters.AddWithValue("@browser", Browser)
            sqlCommand.Parameters.AddWithValue("@version", BrowserVersion)
            sqlCommand.Parameters.AddWithValue("@UserID", UserID)
            sqlCommand.Parameters.AddWithValue("@Website", "SecureXFlow V2.0")
            sqlCommand.ExecuteNonQuery()
            Dim sqlCommand1 As New SqlCommand
            sqlCommand1.CommandText = "SF_InsertUserDeviceHistoryDetails"
            sqlCommand1.CommandType = CommandType.StoredProcedure
            sqlCommand1.Connection = oConn
            sqlCommand1.Parameters.AddWithValue("@device", Device)
            sqlCommand1.Parameters.AddWithValue("@OS", OS)
            sqlCommand1.Parameters.AddWithValue("@browser", Browser)
            sqlCommand1.Parameters.AddWithValue("@version", BrowserVersion)
            sqlCommand1.Parameters.AddWithValue("@UserID", UserID)
            sqlCommand1.Parameters.AddWithValue("@Website", "SecureXFlow V2.0")
            sqlCommand1.ExecuteNonQuery()
        Catch ex As Exception
            Response.Write(ex.Message)
        Finally
            If oConn.State = ConnectionState.Open Then
                oConn.Close()
            End If
        End Try
    End Sub
    Public Function GenerateOTP() As Integer
        Dim otp As Integer
        Dim rnd As New Random()
        otp = rnd.Next(1111, 10000)
        Return otp
    End Function

End Class


