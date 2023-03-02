Imports EncryPass
Imports System.Data.SqlClient
Imports System.Data
Imports System.IO
Partial Class emailtest
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'Try


        '    Dim MAILER As New SASMTPLib.CoSMTPMail
        '    Dim reader As New StreamReader(Server.MapPath("~/authorization/NewUEnvOTP_.htm"))
        '    Dim readFile As String = reader.ReadToEnd()
        '    Dim myString As String = ""
        '    myString = readFile
        '    myString = myString.Replace("$$UNAME$$", "Sushil")
        '    myString = myString.Replace("$$CODE$$", 1234)
        '    'myString = myString.Replace("$$BTEXT$$", "Activation Code")
        '    'myString = myString.Replace("$$EVERIFY$$", "https://secureofficedev.securexsoft.com/authorization/PreUserRegistration2.aspx?activationcode=" & activationcode & "&uid=" & uid & "&emailid=" & email & "&vdate=" & Now() & "&acdes=" & acdesc.ToString)
        '    'myString = myString.Replace("$$BODY2$$", "<p>If you fail to register within 4 hr, the<b> Activation Code </b>will no longer be valid. And, you will have to start the process for receiving a new <b>Activation Code</b>. </p><p>If you need help, please contact our HelpDesk at support@medofficepro.com or call 866-510-1111 x 11.</p><br><br>Thanks,<br><b>MedOfficePro Support</b><br>T: 866-510-1111 x 11 <br>E: support@medofficepro.com<br>www.medofficepro.com<br><br><b>We Better the Business of Medicine")
        '    Dim body As String = "Hi " & "Sushil" & ","
        '    MAILER.FromName = "MedOfficePro Do not reply"
        '    MAILER.Port = 587
        '    MAILER.FromAddress = "donotreply@edictate.com"
        '    MAILER.RemoteHost = "secure.emailsrvr.com"
        '    MAILER.UserName = "alert@edictate.com"
        '    MAILER.Password = "Welcome@medofficepro2011"
        '    MAILER.AddRecipient("sjagtap@medofficepro.com")
        '    'MAILER.AddRecipient("vraut@edictate.com")
        '    MAILER.Priority = 1
        '    MAILER.Urgent = True
        '    MAILER.HtmlText = myString.ToString
        '    MAILER.Subject = "Authentication Code for SecureXFlow"
        '    MAILER.SendMail()
        '    Response.Write(MAILER.Response())
        '    MAILER = Nothing
        'Catch ex As Exception
        '    Response.Write(ex.Message)
        'End Try
    End Sub
End Class
