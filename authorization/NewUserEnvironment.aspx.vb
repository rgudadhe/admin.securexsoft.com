Imports System.Data
Imports System.IO
Imports System.Data.SqlClient
'Imports System.Net.Http


Partial Class authorization_NewUserEnvironment
    Inherits System.Web.UI.Page
    Private Browser As String
    Private weburl As String = ConfigurationManager.AppSettings("URL")
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
        Dim SQLSTR As String = System.Configuration.ConfigurationManager.AppSettings("ETSCon")
        Dim oConn As New SqlConnection(SQLSTR)
        Dim Device As String
        Dim OS As String

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
    Public Function base64Decode(ByVal sData As String) As String
        Dim encoder As New System.Text.UTF8Encoding()
        Dim utf8Decode As System.Text.Decoder = encoder.GetDecoder()
        Dim todecode_byte As Byte() = Convert.FromBase64String(sData)
        Dim charCount As Integer = utf8Decode.GetCharCount(todecode_byte, 0, todecode_byte.Length)
        Dim decoded_char As Char() = New Char(charCount - 1) {}
        utf8Decode.GetChars(todecode_byte, 0, todecode_byte.Length, decoded_char, 0)
        Dim result As String = New [String](decoded_char)
        Return result
    End Function

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        Dim email As String = Request.QueryString("eid")
        email = base64Decode(email)
        txtemail.Text = email
        ' Response.Write(Session("userid"))
    End Sub

    Protected Sub btnSubmit_Click(sender As Object, e As EventArgs) Handles btnSubmit.Click
        Try


            Dim otp As Integer
            Dim uid As String = Session("userid")
            Dim odate As Date
            Dim hr As Integer
            Dim trusted As Integer
            otp = txtactode.Text.Trim
            'Response.Write(uid)
            'Response.Write(otp)

            Dim RowAffected As Integer
            Dim constr As String = ConfigurationManager.ConnectionStrings("ETSConnectionString").ConnectionString
            Dim oConn As SqlConnection = New SqlConnection(constr)
            Dim sqlCommand As SqlCommand = New SqlCommand
            Dim DS As New DataSet
            Dim DT As New Table
            'Try
            sqlCommand.CommandText = "Select * from UserOTP where userid=@userid and otpcode=@otpcode"
            sqlCommand.CommandType = CommandType.Text
            sqlCommand.Parameters.AddWithValue("@UserId", Session("userid"))
            sqlCommand.Parameters.AddWithValue("@otpcode", otp)
            sqlCommand.Connection = oConn
            Dim DA As SqlDataAdapter = New SqlDataAdapter(sqlCommand)
            DA.Fill(DS)
            DA.Dispose()
            oConn.Close()
            ' Response.Write(Session("userid"))
            'Response.Write(DS.Tables(0).Rows.Count)
            ''Response.End()
            If DS.Tables(0).Rows.Count > 0 Then

                odate = DS.Tables(0).Rows(0)("cdate")
                hr = (Now() - odate).Minutes
                '
                'Response.Write(hr)
                'Response.Write(odate)
                'Response.Write("count - " & DS.Tables(0).Rows.Count)
                'Response.End()

                If hr <= 240 Then
                    'Delete OTP from the table for the user

                    Using con As New SqlConnection(constr)
                        Using cmd As New SqlCommand("Delete UserOTP where userid=@UserId and otpcode=@otp")
                            Using sda As New SqlDataAdapter()
                                cmd.CommandType = CommandType.Text
                                cmd.Parameters.AddWithValue("@UserId", uid)
                                cmd.Parameters.AddWithValue("@otp", otp)
                                cmd.Connection = con
                                con.Open()
                                RowAffected = cmd.ExecuteNonQuery()
                                con.Close()
                            End Using
                        End Using
                    End Using

                    If RowAffected > 0 Then
                        InsertUserDeviceDetails(Session("userid"))
                        If CheckBox1.Checked = True Then
                            trusted = 1
                        Else
                            trusted = 0
                        End If
                        'GET USER OS, BROWSER AND DEVICE
                        Dim sqlCommand1 As New SqlCommand
                        sqlCommand1.CommandText = "SF_InsertUserTrustedDeviceDetails"
                        sqlCommand1.CommandType = CommandType.StoredProcedure
                        sqlCommand1.Connection = oConn
                        oConn.Open()
                        sqlCommand1.Parameters.AddWithValue("@userid", Session("userid").ToString)
                        sqlCommand1.Parameters.AddWithValue("@os", GetOS())
                        sqlCommand1.Parameters.AddWithValue("@browser", Browser)
                        sqlCommand1.Parameters.AddWithValue("@device", GetDevice())
                        sqlCommand1.Parameters.AddWithValue("@ip", Session("ipaddress"))
                        sqlCommand1.Parameters.AddWithValue("@trusted", trusted)
                        sqlCommand1.Parameters.AddWithValue("@app", "SXF")
                        sqlCommand1.ExecuteNonQuery()
                        Dim rlink As String = Request.QueryString("rlink")
                        rlink = base64Decode(rlink)
                        'Response.Write(rlink)
                        Response.Redirect(weburl & "/" & rlink)
                    End If
                Else
                    Label2.ForeColor = Drawing.Color.Red
                    Label2.Text = "Your Activation Code Expired!"
                    Exit Sub
                End If
            Else

                Label2.ForeColor = Drawing.Color.Red
                Label2.Text = "Wrong Activation Code!"
                Exit Sub
            End If
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Protected Sub btnresendotp_Click(sender As Object, e As EventArgs) Handles btnresendotp.Click
        Try
            Dim uid As String = Session("UserId").ToString
            sendOTPwithLink(uid)
            Label2.ForeColor = Drawing.Color.Red
            Label2.Text = "Activation Code resent"
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub
    Public Function GenerateOTP() As Integer
        Dim otp As Integer
        Dim rnd As New Random()
        otp = rnd.Next(1111, 10000)
        Return otp
    End Function
    Public Sub sendOTPwithLink(ByVal uid As String)
        Dim Ufname As String = String.Empty
        Dim ULname As String = String.Empty
        Dim acid As String = String.Empty
        Dim email As String = String.Empty
        Dim otp As Integer = GenerateOTP()
        Dim blUserDetails As New ETS.BL.BALSecurity
        Dim acdesc As String = String.Empty
        Dim uname As String = Request.QueryString("id")
        uname = base64Decode(uname)
        Dim Dt As DataTable = blUserDetails.getUserbyUsername(uname)
        If Dt.Rows.Count > 0 Then
            Dim DR As DataRow = Dt.Rows(0)
            Ufname = DR("Firstname").ToString
            email = DR("Officialmailid").ToString
            ULname = DR("Lastname").ToString

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


        

        Dim MAILER As New SASMTPLib.CoSMTPMail
        Dim reader As New StreamReader(Server.MapPath("~/authorization/NewUEnvOTP.htm"))
        Dim readFile As String = reader.ReadToEnd()
        Dim myString As String = ""
        myString = readFile
        myString = myString.Replace("$$UNAME$$", ULname)
        myString = myString.Replace("$$CODE$$", otp)
        'myString = myString.Replace("$$BTEXT$$", "Activation Code")
        'myString = myString.Replace("$$EVERIFY$$", "https://secureofficedev.securexsoft.com/authorization/PreUserRegistration2.aspx?activationcode=" & activationcode & "&uid=" & uid & "&emailid=" & email & "&vdate=" & Now() & "&acdes=" & acdesc.ToString)
        'myString = myString.Replace("$$BODY2$$", "<p>If you fail to register within 4 hr, the<b> Activation Code </b>will no longer be valid. And, you will have to start the process for receiving a new <b>Activation Code</b>. </p><p>If you need help, please contact our HelpDesk at support@medofficepro.com or call 866-510-1111 x 11.</p><br><br>Thanks,<br><b>MedOfficePro Support</b><br>T: 866-510-1111 x 11 <br>E: support@medofficepro.com<br>www.medofficepro.com<br><br><b>We Better the Business of Medicine")
        Dim body As String = "Hi " & Ufname & ","
        MAILER.FromName = "Technical Help Desk"
        MAILER.FromAddress = "techsupport@edictate.com"
        MAILER.RemoteHost = "secure.emailsrvr.com"
        MAILER.UserName = "alert@edictate.com"
        MAILER.Password = "Welcome@medofficepro2011"
        MAILER.AddRecipient(email)
        'MAILER.AddRecipient("vraut@edictate.com")
        MAILER.Priority = 1
        MAILER.Urgent = True

        MAILER.HtmlText = myString.ToString
        MAILER.Subject = "Authentication Code for SecureXFlow"
        MAILER.SendMail()
        MAILER = Nothing
    End Sub

    'Protected Sub btnmobileotp_Click(sender As Object, e As EventArgs) Handles btnmobileotp.Click
    '    Dim Ufname As String = String.Empty
    '    Dim ULname As String = String.Empty
    '    Dim acid As String = String.Empty
    '    Dim email As String = String.Empty
    '    Dim rlink As String = Request.QueryString("rlink")
    '    Dim uname As String = Request.QueryString("id")
    '    uname = base64Decode(uname)
    '    Dim uid As String = Session("UserId").ToString
    '    Dim blUserDetails As New ETS.BL.BALSecurity
    '    Dim Dt As DataTable = blUserDetails.getUserbyUsername(uname)
    '    If Dt.Rows.Count > 0 Then
    '        Dim DR As DataRow = Dt.Rows(0)
    '        Ufname = DR("FirstName").ToString
    '        email = DR("Officialmailid").ToString
    '        ULname = DR("Lastname").ToString

    '    End If

    '    Response.Redirect("NewUserEnvmobileotp.aspx?uid=" & uid & "&emailid=" & base64Encode(email) & "&rlink=" & rlink)
    'End Sub
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
End Class
