Imports System.Data.SqlClient
Imports System.Data
Imports System.IO
Imports System.Net.Mail
Imports System.Net

Partial Class ChangePass
    Inherits BasePage
    Private Function getpassword(ByVal email As String) As Boolean
        Try
            Dim Ufname As String = String.Empty
            Dim username As String = String.Empty
            Dim uid As String = String.Empty
            Dim resetCode As String = Guid.NewGuid().ToString()

            Dim blSecurity As New ETS.BL.BALSecurity

            Dim DT As DataTable = blSecurity.getUserName(email, "Password Reset Request", , HttpContext.Current.Request.UserHostAddress)
            If DT.Rows.Count > 0 Then
                Dim DR As DataRow = DT.Rows(0)
                Ufname = DR("FirstName").ToString
                username = DR("UserName").ToString
                uid = DR("UserID").ToString


                blSecurity.UserActivationTemp(uid.ToString, resetCode)


                SendActivationEmail(Ufname, email, resetCode, uid.ToString)
                Response.Redirect("https://admin.securexsoft.com/authorization/emailvalidation.aspx?emailid=" & email.ToString + "&Ufname=" & Ufname.ToString)


                '                Dim DT1 As DataTable = blSecurity.getUserSecretQuestions(uid)
                '                If DT1.Rows.Count > 0 Then
                '                    GoTo d
                '                Else
                '                    Response.Redirect("Errormsg.aspx")
                '                End If

                '                'Dim dt2 As DataTable = blSecurity.getusersecretquestion(uid, "Password Reset Request")
                'd:              If uid.ToString.Length = 36 Then
                '                    blSecurity.UserPasswordResetTemp(uid.ToString, resetCode)
                '                    sendpasswordresetmail(uid.ToString, resetCode)
                '                    Server.Transfer("..\authorization\passwordconfmsg.aspx?emailid=" & email)
                '                Else
                '                    lblMsg.ForeColor = Drawing.Color.Red
                '                    lblMsg.Text = "This email address does not match our records."
                '                End If
            Else
                Return False
            End If
        Catch ex As Exception
            Response.Write(ex.Message & " " & ex.StackTrace)
            Return False
        End Try
    End Function
    Private Sub SendActivationEmail(ByVal UFname As String, ByVal Email As String, ByVal ActivationCode As String, ByVal UserID As String)

        'Dim MAILER As New SASMTPLib.CoSMTPMail

        'Dim reader As New StreamReader(Server.MapPath("~/authorization/emailverification.htm"))
        'Dim readFile As String = reader.ReadToEnd()
        'Dim myString As String = ""
        'myString = readFile
        'myString = myString.Replace("$$UNAME$$", UFname)
        'myString = myString.Replace("$$BODY$$", "<br />Please click on the link below to complete the registration process.")
        'myString = myString.Replace("$$BTEXT$$", "Activate")
        'myString = myString.Replace("$$EVERIFY$$", "https://admin.securexsoft.com/authorization/UserRegistration.aspx?ActivationCode=" & ActivationCode + "&uid=" & UserID + "&vdate=" & Now().Date)
        'myString = myString.Replace("$$BODY2$$", "If you did not request this registration, or if you need additional assitance, please contact tehnical help desk.")
        'MAILER.FromName = "Technical Help Desk"
        'MAILER.FromAddress = "techsupport@edictate.com"
        'MAILER.RemoteHost = "secure.emailsrvr.com"
        'MAILER.UserName = "alert@edictate.com"
        'MAILER.Password = "Welcome@medofficepro2011"
        'MAILER.AddRecipient(Email)
        ''MAILER.AddRecipient("vraut@edictate.com")
        'MAILER.Priority = 1
        'MAILER.Urgent = True
        ''body += "<br /><br />Please click the following link to verify your email id"
        ' ''body += "<br /><a href = '" + Request.Url.AbsoluteUri.Replace("createUser.aspx", Convert.ToString("email.aspx?ActivationCode=") & activationCode) + "'>Click here to activate your account.</a>"
        ''body += "<br /><a href = https://securewebstaging.securexsoft.com/User%20Management/email.aspx?ActivationCode=" & activationCode + "&uid=" & userId + ">Click here to verify your emailid.</a>"
        ' ''body += Request.Url.AbsolutePath.Replace
        ''body += "<br /><br />Thanks"
        ''body += "<br /><br />Customer Support Team"
        'MAILER.HtmlText = myString.ToString()
        'MAILER.Subject = "Your email address verification request"
        'MAILER.SendMail()
        'MAILER = Nothing

        Dim message As New MailMessage()
        Dim fromName As String = "Do Not Reply"
        Dim from As String = "donotreply@medofficepro.com"
        Dim toAddress As String = Email.ToString
        'Dim bccaddress As String = "sdoxreg@edictate.com"
        Dim smtpadd As String = "email-smtp.us-west-2.amazonaws.com"
        Dim smtpuname As String = "AKIA44IE6PBA24MEZW5P"
        Dim smtppass As String = "BLZJ9U1M6AILVx4FRA8E1CdRvOoRV9rx7/HBXEcNaeJ6"
        Dim port As Integer = 587
        Dim subject As String = "Your email address verification request"
        Dim configset As String = "ConfigSet"

        message.IsBodyHtml = True
        message.From = New MailAddress(from, fromName)
        message.To.Add(New MailAddress(toAddress))
        message.Subject = "Your email address verification request"


        Dim reader As New StreamReader(Server.MapPath("~/authorization/emailverification.htm"))
        Dim readFile As String = reader.ReadToEnd()
        Dim myString As String = ""
        myString = readFile
        myString = myString.Replace("$$UNAME$$", UFname)
        myString = myString.Replace("$$BODY$$", "<br />Please click on the link below to complete the registration process.")
        myString = myString.Replace("$$BTEXT$$", "Activate")
        myString = myString.Replace("$$EVERIFY$$", "https://admin.securexsoft.com/authorization/UserRegistration.aspx?ActivationCode=" & ActivationCode + "&uid=" & UserID + "&vdate=" & Now().Date)
        myString = myString.Replace("$$BODY2$$", "If you did not request this registration, or if you need additional assitance, please contact tehnical help desk.")
        message.IsBodyHtml = True
        message.Body = myString.ToString
        message.From = New MailAddress(from, fromName)
        message.To.Add(New MailAddress(toAddress))
        message.CC.Add("hr@edictate.com")

        message.Subject = subject
        'message.Headers.Add("X-SES-CONFIGURATION-SET", configset)
        Dim client As New System.Net.Mail.SmtpClient(smtpadd, port)

        client.Credentials = New NetworkCredential(smtpuname, smtppass)
        client.EnableSsl = True
        client.Send(message)


    End Sub
    Public Sub sendpasswordresetmail(ByVal uid As String, resetcode As String)

        Dim Ufname As String = String.Empty
        Dim email As String = String.Empty
        Dim blUserDetails As New ETS.BL.Users
        Dim DS As DataSet = blUserDetails.getUserDetails(uid.ToString)

        Ufname = DS.Tables(0).Rows(0)(3).ToString
        email = DS.Tables(0).Rows(0)(7).ToString


        Dim MAILER As New SASMTPLib.CoSMTPMail
        Dim reader As New StreamReader(Server.MapPath("~/authorization/emailverification.htm"))
        Dim readFile As String = reader.ReadToEnd()
        Dim myString As String = ""
        myString = readFile
        myString = myString.Replace("$$UNAME$$", Ufname)
        myString = myString.Replace("$$BODY$$", "Please clik on the Reset button below to reset your password. You will need to answer the sercurity question you have setup during the registration process.")
        myString = myString.Replace("$$BTEXT$$", "Reset")
        myString = myString.Replace("$$EVERIFY$$", "https://admin.securexsoft.com/authorization/PReset.aspx?resetCode=" & resetcode & "&uid=" & uid)
        myString = myString.Replace("$$BODY2$$", "If you did not request this registration, or if you need additional assitance, please contact support desk at <b>techsupport@edictate.com</b> or by Skype <b>serversupportteam</b>")
        Dim body As String = "Hi " & Ufname & ","
        MAILER.FromName = "E-Dictate Support Desk"
        MAILER.FromAddress = "techsupport@edictate.com"
        MAILER.RemoteHost = "secure.emailsrvr.com"
        MAILER.UserName = "alert@edictate.com"
        MAILER.Password = "Welcome@medofficepro2011"
        MAILER.AddRecipient(email)
        'MAILER.AddRecipient("vraut@edictate.com")
        MAILER.Priority = 1
        MAILER.Urgent = True

        MAILER.HtmlText = myString.ToString
        MAILER.Subject = Ufname & ", here's the link to reset your password"
        MAILER.SendMail()
        MAILER = Nothing
    End Sub
    Protected Sub Button1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button1.Click
        Try
            Dim emailId As String = DLUser.Items(DLUser.SelectedIndex).Value.ToString
			response.write(emailid)
            If emailId <> "" Then
                If getpassword(emailId) Then

                Else
                    lblMsg.ForeColor = Drawing.Color.Red
                    lblMsg.Text = "This email address does not match our records."
                End If
            Else
                lblMsg.ForeColor = Drawing.Color.Red
                lblMsg.Text = "Users email address is blank or invalid."
            End If



            'If TxtNPass.Text = TxtCPass.Text Then
            '    Dim clsUsr As ETS.BL.Users
            '    Dim UserName As String = String.Empty
            '    Dim varTemp() As String
            '    Dim EPass As New EncryPass.Encry
            '    Dim Newuserpass As String
            '    Try
            '        varTemp = Trim(DLUser.Items(DLUser.SelectedIndex).Text.ToString).Split("(")
            '        UserName = Mid(varTemp(1), 1, varTemp(1).IndexOf(")"))
            '        clsUsr = New ETS.BL.Users
            '        clsUsr.UserID = DLUser.Items(DLUser.SelectedIndex).Value.ToString
            '        Newuserpass = EPass.encrypt(UserName.ToLower, TxtNPass.Text)
            '        clsUsr.Password = Newuserpass
            '        clsUsr.Passchanged = Now

            '        If clsUsr.UpdateUserDetails = 1 Then
            '            lblMsg.Text = "Password has been reset successfully."
            '        Else
            '            lblMsg.Text = "Password updation failed."
            '        End If

            '    Catch ex As Exception
            '        Response.Write(ex.Message)
            '    Finally
            '        clsUsr = Nothing
            '    End Try
            'Else
            '    lblMsg.Text = "Password does not match."
            'End If
        Catch ex As Exception
            Response.Write(ex.Message)
            Response.Write(" Please contact system administrator for more details. ")

        End Try

    End Sub
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            Dim clsUsrs As ETS.BL.Users
            Dim DV As New Data.DataView
            Dim DS As New Data.DataSet
            Try
                clsUsrs = New ETS.BL.Users
                'clsUsrs.ContractorID = Session("ContractorID")
                'clsUsrs._WhereString.Append(" AND (IsDeleted is NULL or Isdeleted = 'False')")
                DS = clsUsrs.getUsersList(Session("ContractorID"), Session("WorkGroupID"), String.Empty)
                If DS.Tables.Count > 0 Then
                    If DS.Tables(0).Rows.Count > 0 Then
                        DS.Tables(0).Columns.Add("BindName", GetType(System.String), "FirstName + ' '+ LastName + ' (' + UserName + ')'")
                        DV = New Data.DataView(DS.Tables(0), String.Empty, "FirstName,LastName", DataViewRowState.CurrentRows)

                        DLUser.DataSource = DV
                        DLUser.DataValueField = "OfficialMailID"
                        DLUser.DataTextField = "BindName"
                        DLUser.DataBind()
                    End If
                End If
                DLUser.Items.Insert(0, New ListItem("Please Select", String.Empty))
            Catch ex As Exception
                Response.Write(ex.Message)
            Finally

                clsUsrs = Nothing
                DS.Dispose()
                DV.Dispose()
            End Try
        End If
    End Sub
End Class
