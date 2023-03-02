Imports System.IO
Imports System.Data
Imports System.Data.SqlClient
Imports System.Configuration
Imports EncryPass
Imports ETSSW.BLL
Imports System.Security.Cryptography
Partial Class Passwordmodule_preset
    Inherits System.Web.UI.Page
    Public flag As Integer
    Private ipaddress As String
    Private value As Integer
    Private ans As String = String.Empty

    Protected Sub btn1_Click(sender As Object, e As System.EventArgs) Handles btn1.Click
        Label2.Text = ""
        Label3.Text = ""

        Dim blsecurity As New ETS.BL.BALSecurity
        Dim username As String = ""
        Dim msg As String
        Dim uid As String = Request.QueryString("uid")
        'Dim uid As String = "a164cb3f-c29b-4f8f-afc8-67f626b5f05d"
        ipaddress = HttpContext.Current.Request.UserHostAddress
        Label2.Text = String.Empty
        If Not TxtNewPass.Text = TxtCNewPass.Text Then
            Label2.Text = "Password does not match."
            Exit Sub
        Else
            Dim pcheck As DataTable = blsecurity.getPasswordCheck(TxtNewPass.Text)
            If pcheck.Rows.Count > 1 Then
                Label2.Text = "Dictionary words for passwords are not allowed."
                Exit Sub
            End If
            'Response.Write(value)
            'Response.Write(ans)
            'Response.Write("txtans is" & txtans1.Text)
            If ans = txtans1.Text Then
                GoTo here
            Else
                Label3.ForeColor = Drawing.Color.Red
                Label3.Text = "Does not match our records. Please answer correctly or contact helpdesk at techsupport@edictate.com or Skype ID serversupportteam"
                Exit Sub
            End If
            'Dim constr As String = ConfigurationManager.ConnectionStrings("ETSConnectionString").ConnectionString
            'Dim objUser As New ETSSW.BLL.SWUsersBLL
            'Dim conpcheck As New SqlConnection(constr)
            'conpcheck.Open()
            'Dim cmdpcheck As New SqlCommand("Select Top 5 Password from tbluserpasschange where userid='" & uid & "'", conpcheck)
            'Dim drpcheck As SqlDataReader
            'drpcheck = cmdpcheck.ExecuteReader()

here:
            Dim dtpcheck As DataTable = blsecurity.getUserTop5Passwords(uid)
            For Each row As DataRow In dtpcheck.Rows
                Dim pass As String = String.Empty
                pass = base64Decode(dtpcheck.Rows(0)("password").ToString.Trim)
                'Response.Write(pass & "<br/>")

                If TxtNewPass.Text = pass.ToString Then
                    Label2.Text = "You have alrady used this password previously, <br />please try using another password."
                    Exit Sub
                End If
            Next

            Dim blUserDetails As New ETS.BL.Users
            Dim DS As DataSet = blUserDetails.getUserDetails(uid.ToString)

            username = DS.Tables(0).Rows(0)(2).ToString
            Dim UFname As String = DS.Tables(0).Rows(0)(3).ToString
            Dim email As String = DS.Tables(0).Rows(0)(7).ToString

            Dim EPass As New EncryPass.Encry
            Dim Newuserpass As String

            Newuserpass = EPass.encrypt(username.ToLower, TxtNewPass.Text)

            Dim clsu As New ETS.BL.Users
            clsu.UserID = uid
            clsu.Passchanged = Now
            clsu.Password = Newuserpass
            If clsu.UpdateUserDetails() = 1 Then
                Label2.Text = "Password has been reset successfully."
                'passconfirmmail(UFname, email)
                'Response.Write("<script>self.close();</script>")
                blsecurity.UpdateUserLog(Now(), username.ToString, "Password Reset Successfully", uid.ToString, 0, ipaddress)
                blsecurity.InsertUserPassword(uid.ToString, Now().ToString("MM/dd/yyyy"), base64Encode(TxtNewPass.Text.Trim()))
                Response.Redirect("passresetconfirmation.aspx")
            Else
                Label2.Text = "Failed changing password."
                'Response.Write("<script>self.close();</script>")
            End If
        End If
        ''Exit Sub
        'If objUser.UpdatePassword(uid, TxtNewPass.Text) Then
        '    Label2.Text = "Congratulations! Your password has been reset successfully."
        '    msg = "Congratulations! Your password has been reset successfully."
        '    passconfirmmail()
        '    btn1.Enabled = False
        '    Dim DT As DataTable = objUser.GetSWlUserDatabyUserID(New Guid(uid))
        '    If DT.Rows.Count > 0 Then
        '        Dim DR As DataRow = DT.Rows(0)
        '        username = DR("UserName").ToString
        '    End If
        '    Using con As New SqlConnection(constr)
        '        Using cmd1 As New SqlCommand("INSERT INTO tbluserlog VALUES(@date, @uname,@action,@userid,@logflag)")
        '            Using sda1 As New SqlDataAdapter()
        '                cmd1.CommandType = CommandType.Text
        '                cmd1.Parameters.AddWithValue("@date", Now())
        '                cmd1.Parameters.AddWithValue("@uname", username)
        '                cmd1.Parameters.AddWithValue("@action", "Password Reset Successfully")
        '                cmd1.Parameters.AddWithValue("@userid", uid)
        '                cmd1.Parameters.AddWithValue("@logflag", 0)
        '                cmd1.Connection = con
        '                con.Open()
        '                cmd1.ExecuteNonQuery()
        '                con.Close()
        '            End Using
        '        End Using
        '    End Using
        '    Using con As New SqlConnection(constr)
        '        Using cmd1 As New SqlCommand("INSERT INTO tbluserpasschange VALUES(@userid, @date,@password)")
        '            Using sda1 As New SqlDataAdapter()
        '                cmd1.CommandType = CommandType.Text
        '                cmd1.Parameters.AddWithValue("@date", Now())
        '                cmd1.Parameters.AddWithValue("@userid", uid)
        '                cmd1.Parameters.AddWithValue("@password", base64Encode(TxtNewPass.Text.Trim()))
        '                cmd1.Connection = con
        '                con.Open()
        '                cmd1.ExecuteNonQuery()
        '                con.Close()
        '            End Using
        '        End Using
        '    End Using
        '    Response.Redirect("~/login.aspx?msg=" & msg)
        '    Else
        '    Label2.Text = "Error in updating password. Please try again"
        'End If



    End Sub
    Private Sub passconfirmmail(UFname As String, Email As String)
        Dim MAILER As New SASMTPLib.CoSMTPMail
        'Dim objUser As New SWUsersBLL
        'Dim DT As DataTable = objUser.GetSWlUserDatabyUserID(New Guid(Request.QueryString("uid")))
        'If DT.Rows.Count > 0 Then
        '    Dim DR As DataRow = DT.Rows(0)
        '    Ufname = DR("First").ToString
        '    Uemail = DR("emailaddress").ToString
        'End If

        Dim reader As New StreamReader(Server.MapPath("~/authorization/confirmation.htm"))
        Dim readFile As String = reader.ReadToEnd()
        Dim myString As String = ""
        myString = readFile
        myString = myString.Replace("$$UNAME$$", Ufname)
        myString = myString.Replace("$$BODY$$", "You've successfully changed your Secure-Scribe password. Please click <a href=""https://adminstaging.securexsoft.com"">here</a> to login.")
        Dim body As String = "Hi " & Ufname & ","
        MAILER.FromName = "E-Dictate User Support"
        MAILER.FromAddress = "hr@edictate.com"
        MAILER.RemoteHost = "secure.emailsrvr.com"
        MAILER.UserName = "alert@edictate.com"
        MAILER.Password = "Welcome@medofficepro2011"
        MAILER.AddRecipient(Email)
        'MAILER.AddRecipient("vraut@edictate.com")
        MAILER.Priority = 1
        MAILER.Urgent = True
        'body += "<br /><br />You've successfully changed your Secure-Scribe password."
        'body += "<br /><br />Thanks,"
        'body += "<br /><br />Customer Support Team"
        MAILER.HtmlText = myString.ToString
        MAILER.Subject = Ufname & ", your password was successfully reset"
        MAILER.SendMail()
        MAILER = Nothing
    End Sub

    'Protected Sub form1_Load(sender As Object, e As System.EventArgs) Handles form1.Load
    '    ipaddress = HttpContext.Current.Request.UserHostAddress
    '    If Not IsPostBack Then
    '        form1.Visible = False
    '        'Dim constr As String = ConfigurationManager.ConnectionStrings("ETSConnectionString").ConnectionString
    '        Dim resetcode As String = Request.QueryString("resetcode")
    '        Dim uid As String = Request.QueryString("uid")
    '        Dim lpage As Integer = 0
    '        lpage = Request.QueryString("lpage")
    '        'Response.Write("Uid" & uid)
    '        'Response.Write("Resetcode" & resetcode)
    '        If lpage <> 1 Then
    '            'Using con As New SqlConnection(constr)
    '            '    Using cmd As New SqlCommand("DELETE FROM tblpreset WHERE resetcode = @resetcode")
    '            '        Using sda As New SqlDataAdapter()
    '            '            cmd.CommandType = CommandType.Text
    '            '            cmd.Parameters.AddWithValue("@resetcode", resetcode)
    '            '            cmd.Connection = con
    '            '            con.Open()
    '            Dim blSecurity As New ETS.BL.BALSecurity
    '            Dim rowsAffected As Integer = blSecurity.UserPasswordResetTempDelete(uid.ToString, resetcode)
    '            'con.Close()
    '            If rowsAffected = 1 Then
    '                flag = 1
    '                form1.Visible = True
    '            Else
    '                form1.Visible = False
    '                Response.Write("Invalid URL!")
    '            End If
    '            '        End Using
    '            '    End Using
    '            'End Using
    '        Else
    '            form1.Visible = True
    '        End If
    '    End If
    '    'Response.Write(flag)
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

    Protected Sub form1_Load(sender As Object, e As System.EventArgs) Handles form1.Load
        Dim blSecurity As New ETS.BL.BALSecurity
        Dim uid As String = Request.QueryString("uid")
        Dim random As New Random()
        value = random.Next(1, 4)
        Dim DT As DataTable = blSecurity.getUserSecretQuestions(uid)

        If DT.Rows.Count > 0 Then
            Dim DR As DataRow = DT.Rows(0)
            Label1.Text = DR.Item("q" & value).ToString
            ans = DR.Item("s" & value).ToString
        End If
        'Response.Write(ans)


        'Response.Write(random.Next(1, 4))

        If Not IsPostBack Then
            form1.Visible = False

            ipaddress = HttpContext.Current.Request.UserHostAddress
            Dim resetcode As String = Request.QueryString("resetcode")

            'Dim uid As String = "a164cb3f-c29b-4f8f-afc8-67f626b5f05d"
           

            Dim rowsAffected As Integer = blSecurity.UserPasswordResetTempDelete(uid.ToString, resetcode)

            If rowsAffected = 1 Then
                flag = 1
                form1.Visible = True
            Else
                form1.Visible = False
                Response.Redirect("message.aspx")
                Exit Sub
            End If


            Dim blUsers As New ETS.BL.Users
            Dim ds As DataSet = blUsers.getUserDetails(uid.ToString)
            txtuname.Text = ds.Tables(0).Rows(0)(2).ToString
            txtemail.Text = ds.Tables(0).Rows(0)(7).ToString
            txtfname.Text = ds.Tables(0).Rows(0)(3).ToString
            txtlname.Text = ds.Tables(0).Rows(0)(5).ToString
        End If


    End Sub
End Class
