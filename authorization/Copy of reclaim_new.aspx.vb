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

Partial Class reclaim_new
    Inherits System.Web.UI.Page
    Private ipaddress As String
    Private Ufname As String = String.Empty
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
                'Dim dt2 As DataTable = blSecurity.getusersecretquestion(uid, "Password Reset Request")
                If uid.ToString.Length = 36 Then
                    blSecurity.UserPasswordResetTemp(uid.ToString, resetCode)
                    sendpasswordresetmail(uid.ToString, resetCode)
                    Response.Redirect("passwordconfmsg.aspx?emailid=" & email)
                Else
                    lblMessage.ForeColor = Drawing.Color.Red
                    lblMessage.Text = "This email address does not match our records."
                End If

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
            Response.Write(DT.Rows.Count)
            If DT.Rows.Count > 0 Then
                Dim DR As DataRow = DT.Rows(0)
                Ufname = DR("FirstName").ToString
                username = DR("UserName").ToString
                Return SendMailer(username, Ufname, email)
                'Response.Redirect("usernameconfmsg.aspx?emailid=" & email)
            End If
        Catch ex As Exception
            Response.Write(ex.Message & " " & ex.StackTrace)
            Return False
        End Try
    End Function
    Public Function SendMailer(UserNAme As String, Ufname As String, email As String) As Boolean
        Dim MAILER As New SASMTPLib.CoSMTPMail
        Try
            Dim body As String
            Dim body2 As String

            body = "Your registered username for SecureXFlow application is:" & UserNAme
            body2 = "If you did not request this registration, or if you need additional assitance, please contact support desk at <b>techsupport@edictate.com</b> or by Skype <b>serversupportteam</b>"

            Dim reader As New StreamReader(Server.MapPath("../authorization/UsernameConfirmation.htm"))
            Dim readFile As String = reader.ReadToEnd()
            Dim myString As String = ""
            myString = readFile
            myString = myString.Replace("$$UNAME$$", Ufname)
            myString = myString.Replace("$$BODY$$", body.ToString)
            myString = myString.Replace("$$BODY2$$", body2.ToString)

            MAILER.FromName = "E-Dictate Support Desk"
            MAILER.FromAddress = "techsupport@edictate.com"
            MAILER.RemoteHost = "secure.emailsrvr.com"
            MAILER.UserName = "alert@edictate.com"
            MAILER.Password = "Welcome@medofficepro2011"
            MAILER.AddRecipient(email)

            MAILER.Priority = 1
            MAILER.Urgent = True
            MAILER.HtmlText = myString.ToString
            MAILER.Subject = Ufname & ", your Username is here"
            MAILER.SendMail()
            MAILER = Nothing
            Return True
        Catch ex As Exception
            Response.Write(ex.Message)
            Return False
        End Try
    End Function
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
        'body += "<br /><br />You recently requested a password reset."
        'body += "<br /><br />To change your Secure-Scribe password,"
        'body += "<a href = https://securewebstaging.securexsoft.com/PReset.aspx?resetCode=" & resetCode & "&uid=" & uid + ">Click</a>" + " here."
        'body += "<br /><br />Thanks,"
        'body += "<br /><br />Customer Support Team"
        MAILER.HtmlText = myString.ToString
        MAILER.Subject = Ufname & ", here's the link to reset your password"
        MAILER.SendMail()
        MAILER = Nothing
    End Sub
    




    Protected Sub form1_Load(sender As Object, e As System.EventArgs) Handles form1.Load
        ipaddress = HttpContext.Current.Request.UserHostAddress
    End Sub

    Protected Sub btnSubmit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSubmit.Click
        Dim emailid As String = String.Empty
        emailid = txtemail.Text
        If chkList.SelectedIndex = 0 Then
            If getusername(emailid) Then
                Response.Redirect("usernameconfmsg.aspx?emailid=" & emailid)
                'message = "Link to know your Username has been to your email address!"
                'lblMessage.ForeColor = Drawing.Color.Green
                'lblMessage.Text = "Your username has been sent to your email address. Check spam folder as well if you do not get it in five minutes."
                'txtemail.Text = ""
                'Response.Redirect("~/emaiValidation.aspx?Name=" & Ufname & "&emailID=" & emailid)
            Else
                lblMessage.ForeColor = Drawing.Color.Red
                lblMessage.Text = "This email address does not match our records."
            End If
        Else
            getpassword(emailid)
        End If

       

        'Response.Write(getusername(emailid))
        
    End Sub

    Protected Sub chkList_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkList.SelectedIndexChanged
        btnSubmit.Enabled = True
    End Sub
End Class
