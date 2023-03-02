Imports System.Collections.Generic
Imports System.Linq
Imports System.Web
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports System.Data
Imports System.Net
Imports System.Text
Imports System.IO
Imports System.Web.Script.Serialization
'Imports System.IO
'Imports System.Data
Imports System.Data.SqlClient
Imports System.Configuration
Imports EncryPass

Imports System.Net.Mail

Partial Class reclaim_new
    Inherits System.Web.UI.Page
    Private ipaddress As String
    Private Ufname As String = String.Empty
    Private fl As Integer = 0
    Private Function getpassword(ByVal email As String) As Boolean
        Try
            Dim Ufname As String = String.Empty
            Dim username As String = String.Empty
            Dim uid As String = String.Empty
            Dim resetCode As String = Guid.NewGuid().ToString()

            Dim blSecurity As New ETS.BL.BALSecurity

            Dim DT As DataTable = blSecurity.getUserName(email, "Password Reset Request", , ipaddress)
            If DT.Rows.Count > 0 Then
                Dim DR As DataRow = DT.Rows(0)
                Ufname = DR("FirstName").ToString
                username = DR("UserName").ToString
                uid = DR("UserID").ToString

                Dim DT1 As DataTable = blSecurity.getUserSecretQuestions(uid)
                If DT1.Rows.Count > 0 Then
                    GoTo d
                Else
                    Response.Redirect("Errormsg.aspx")
                End If

                'Dim dt2 As DataTable = blSecurity.getusersecretquestion(uid, "Password Reset Request")
d:
                If uid.ToString.Length = 36 Then
                    blSecurity.UserPasswordResetTemp(uid.ToString, resetCode)
                    sendpasswordresetmail(uid.ToString, resetCode)
                    Server.Transfer("passwordconfmsg.aspx?emailid=" & email)
                Else
                    lblMessage.ForeColor = Drawing.Color.Red
                    lblMessage.Text = "This email address does not match our records."
                End If
            Else
                Return False
            End If
        Catch ex As Exception
            Response.Write(ex.Message & " " & ex.StackTrace)
            Return False
        End Try
    End Function

    Private Function getusername(ByVal email As String) As Boolean
        Try

            Dim blSecurity As New ETS.BL.BALSecurity


            Dim username As String = String.Empty

            Dim DT As DataTable = blSecurity.getUserName(email, "Requested UserName", , ipaddress)
            'Response.Write(DT.Rows.Count)
            If DT.Rows.Count > 0 Then
                Dim DR As DataRow = DT.Rows(0)
                Ufname = DR("FirstName").ToString
                username = DR("UserName").ToString
                Return SendMailer(username, Ufname, email)
                'Response.Redirect("usernameconfmsg.aspx?emailid=" & email)
            Else
                Return False
            End If
        Catch ex As Exception
            Response.Write(ex.Message & " " & ex.StackTrace)
            Return False
        End Try
    End Function
    Public Function SendMailer(ByVal UserNAme As String, ByVal Ufname As String, ByVal email As String) As Boolean
        Dim MAILER As New SASMTPLib.CoSMTPMail
        Try
            Dim body As String
            Dim body2 As String

            body = "Your registered username for SecureXFlow application is: <b>" & UserNAme & "</b>"
            body2 = "If you did not request this registration, or if you need additional assitance, please contact technical help desk."

            'Dim reader As New StreamReader(Server.MapPath("../authorization/UsernameConfirmation.htm"))
            'Dim readFile As String = reader.ReadToEnd()
            'Dim myString As String = ""
            'myString = readFile
            'myString = myString.Replace("$$UNAME$$", Ufname)
            'myString = myString.Replace("$$BODY$$", body.ToString)
            'myString = myString.Replace("$$BODY2$$", body2.ToString)

            'MAILER.FromName = "Technical Help Desk"
            'MAILER.FromAddress = "alert@edictate.com"
            'MAILER.RemoteHost = "email-smtp.us-west-2.amazonaws.com"
            'MAILER.UserName = "AKIA44IE6PBA24MEZW5P"
            'MAILER.Password = "BLZJ9U1M6AILVx4FRA8E1CdRvOoRV9rx7/HBXEcNaeJ6"
            'MAILER.AddRecipient(email)
            'MAILER.Port = 587
            'MAILER.Priority = 1
            'MAILER.Urgent = True
            'MAILER.HtmlText = myString.ToString
            'MAILER.Subject = "Your Username for SecureXFlow application"
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
            Dim subject As String = "Your Username for SecureXFlow application"
            Dim configset As String = "ConfigSet"

            message.IsBodyHtml = True
            message.From = New MailAddress(from, fromName)
            message.To.Add(New MailAddress(toAddress))
            message.Subject = "Username for Secure-Patient"


            Dim reader As New StreamReader(Server.MapPath("../authorization/UsernameConfirmation.htm"))
            Dim readFile As String = reader.ReadToEnd()
            Dim myString As String = ""
            myString = readFile
            myString = myString.Replace("$$UNAME$$", Ufname)
            myString = myString.Replace("$$BODY$$", body.ToString)
            myString = myString.Replace("$$BODY2$$", body2.ToString)

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
            Return True
        Catch ex As Exception
            Response.Write(ex.Message)
            Return False
        End Try
    End Function
    Public Sub sendpasswordresetmail(ByVal uid As String, ByVal resetcode As String)

        Dim Ufname As String = String.Empty
        Dim email As String = String.Empty
        Dim blUserDetails As New ETS.BL.Users
        Dim DS As DataSet = blUserDetails.getUserDetails(uid.ToString)

        Ufname = DS.Tables(0).Rows(0)(3).ToString
        email = DS.Tables(0).Rows(0)(7).ToString


        'Dim MAILER As New SASMTPLib.CoSMTPMail
        'Dim reader As New StreamReader(Server.MapPath("~/authorization/emailverification.htm"))
        'Dim readFile As String = reader.ReadToEnd()
        'Dim myString As String = ""
        'myString = readFile
        'myString = myString.Replace("$$UNAME$$", Ufname)
        'myString = myString.Replace("$$BODY$$", "Please clik on the link below to reset your password. You will need to answer the sercurity question you have setup during the registration process.")
        'myString = myString.Replace("$$BTEXT$$", "Reset")
        'myString = myString.Replace("$$EVERIFY$$", "https://admin.securexsoft.com/authorization/PReset.aspx?resetCode=" & resetcode & "&uid=" & uid)
        'myString = myString.Replace("$$BODY2$$", "If you did not request this registration, or if you need additional assitance, please contact technical help desk")
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
        ''body += "<br /><br />You recently requested a password reset."
        ''body += "<br /><br />To change your Secure-Scribe password,"
        ''body += "<a href = https://securewebstaging.securexsoft.com/PReset.aspx?resetCode=" & resetCode & "&uid=" & uid + ">Click</a>" + " here."
        ''body += "<br /><br />Thanks,"
        ''body += "<br /><br />Customer Support Team"
        'MAILER.HtmlText = myString.ToString
        'MAILER.Subject = "Your link to reset the password for SecureXFlow application"
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
        Dim subject As String = "Your link to reset the password for SecureXFlow application"
        Dim configset As String = "ConfigSet"

        message.IsBodyHtml = True
        message.From = New MailAddress(from, fromName)
        message.To.Add(New MailAddress(toAddress))
        message.Subject = "Your link to reset the password for SecureXFlow application"


        Dim reader As New StreamReader(Server.MapPath("~/authorization/emailverification.htm"))
        Dim readFile As String = reader.ReadToEnd()
        Dim myString As String = ""
        myString = readFile
        myString = myString.Replace("$$UNAME$$", Ufname)
        myString = myString.Replace("$$BODY$$", "Please clik on the link below to reset your password. You will need to answer the sercurity question you have setup during the registration process.")
        myString = myString.Replace("$$BTEXT$$", "Reset")
        myString = myString.Replace("$$EVERIFY$$", "https://admin.securexsoft.com/authorization/PReset.aspx?resetCode=" & resetcode & "&uid=" & uid)
        myString = myString.Replace("$$BODY2$$", "If you did not request this registration, or if you need additional assitance, please contact technical help desk")

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





    Protected Sub form1_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        ipaddress = HttpContext.Current.Request.UserHostAddress
    End Sub

    Protected Sub btnSubmit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSubmit.Click
        Dim emailid As String = String.Empty
        emailid = txtemail.Text

        If chkList.SelectedIndex = 0 Then

            If getusername(emailid) Then

                Server.Transfer("usernameconfmsg.aspx?emailid=" & emailid)
                'message = "Link to know your Username has been to your email address!"
                'lblMessage.ForeColor = Drawing.Color.Green
                'lblMessage.Text = "Your username has been sent to your email address. Check spam folder as well if you do not get it in five minutes."
                'txtemail.Text = ""
                'Response.Redirect("~/emaiValidation.aspx?Name=" & Ufname & "&emailID=" & emailid)
            Else
                Label2.ForeColor = Drawing.Color.Red
                Label2.Text = "This email address does not match our records."
            End If
        Else

            If getpassword(emailid) Then
            Else
                Label2.ForeColor = Drawing.Color.Red
                Label2.Text = "This email address does not match our records."
            End If



        End If



        'Response.Write(getusername(emailid))

    End Sub

    Protected Sub chkList_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkList.SelectedIndexChanged
        btnSubmit.Enabled = True
    End Sub


End Class
