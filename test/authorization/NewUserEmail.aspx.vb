
Imports System.Data
Imports System.Configuration
Imports System.Data.SqlClient
Imports System.IO


Partial Class User_Management_NewUserEmail
    Inherits System.Web.UI.Page
    Private flag As Integer = 0

    Protected Sub submit_Click(sender As Object, e As System.EventArgs) Handles submit.Click

        Dim userid As String = Request.QueryString("uid")
        Dim activationCode As String = Guid.NewGuid().ToString()
        Try
            Dim blSecurity As New ETS.BL.BALSecurity
            'Dim dt As DataTable = blSecurity.Checkuseremailaddress(txtEmail.Text)
            'If dt.Rows.Count > 0 Then
            '    lblMessage.ForeColor = Drawing.Color.Red
            '    lblMessage.Text = "This email ID is already used. Please use another email id."
            '    Exit Sub
            'End If
            blSecurity.UpdateUserOfficialEMailID(userid.ToString, txtEmail.Text)

            blSecurity.UserActivationTemp(userid.ToString, activationCode)
            'Response.Write(blSecurity.UserActivationTemp(userid.ToString, activationCode))

            Dim blUsers As New ETS.BL.Users
            Dim ds As DataSet = blUsers.getUserDetails(userid.ToString)
            Dim UFname As String = ds.Tables(0).Rows(0)(3).ToString
            Dim Email As String = ds.Tables(0).Rows(0)(7).ToString
            SendActivationEmail(UFname, Email, activationCode, userid.ToString)
            Response.Redirect("emailvalidation.aspx?emailid=" & Email.ToString + "&Ufname=" & UFname.ToString)
            'lblMessage.ForeColor = Drawing.Color.Green
            'lblMessage.Text = "Verification mail has been sent your above email address."
            submit.Enabled = False
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try


    End Sub

    'Protected Sub submit_Load(sender As Object, e As System.EventArgs) Handles submit.Load
    '    If Not IsPostBack Then
    '        ScriptManager.RegisterStartupScript(Me, [GetType](), "showalert", "alert('Your email address is not updated in our records. Please update your email address.');", True)
    '    End If

    'End Sub

    Private Sub SendActivationEmail(ByVal UFname As String, ByVal Email As String, ByVal ActivationCode As String, ByVal UserID As String)

        Dim MAILER As New SASMTPLib.CoSMTPMail

        Dim reader As New StreamReader(Server.MapPath("~/authorization/emailverification.htm"))
        Dim readFile As String = reader.ReadToEnd()
        Dim myString As String = ""
        myString = readFile
        myString = myString.Replace("$$UNAME$$", UFname)
        myString = myString.Replace("$$BODY$$", "<br />Please click on the Activate button below to complete the registration process.")
        myString = myString.Replace("$$BTEXT$$", "Activate")
        myString = myString.Replace("$$EVERIFY$$", "https://adminstaging.securexsoft.com/authorization/UserRegistration.aspx?ActivationCode=" & ActivationCode + "&uid=" & UserID + "&vdate=" & Now())
        myString = myString.Replace("$$BODY2$$", "If you did not request this registration, or if you need additional assitance, please contact support desk at <b>techsupport@edictate.com</b> or by Skype <b>serversupportteam</b>")
        MAILER.FromName = "E-Dictate Support Desk"
        MAILER.FromAddress = "techsupport@edictate.com"
        MAILER.RemoteHost = "secure.emailsrvr.com"
        MAILER.UserName = "alert@edictate.com"
        MAILER.Password = "Welcome@medofficepro2011"
        MAILER.AddRecipient(Email)
        'MAILER.AddRecipient("vraut@edictate.com")
        MAILER.Priority = 1
        MAILER.Urgent = True
        'body += "<br /><br />Please click the following link to verify your email id"
        ''body += "<br /><a href = '" + Request.Url.AbsoluteUri.Replace("createUser.aspx", Convert.ToString("email.aspx?ActivationCode=") & activationCode) + "'>Click here to activate your account.</a>"
        'body += "<br /><a href = https://securewebstaging.securexsoft.com/User%20Management/email.aspx?ActivationCode=" & activationCode + "&uid=" & userId + ">Click here to verify your emailid.</a>"
        ''body += Request.Url.AbsolutePath.Replace
        'body += "<br /><br />Thanks"
        'body += "<br /><br />Customer Support Team"
        MAILER.HtmlText = myString.ToString()
        MAILER.Subject = UFname & ", your email address verification request"
        MAILER.SendMail()
        MAILER = Nothing

    End Sub

    Protected Sub signup_Load(sender As Object, e As System.EventArgs) Handles signup.Load
        If Not IsPostBack Then
            'ScriptManager.RegisterStartupScript(Me, [GetType](), "showalert", "alert('Your email address is not updated in our records. Please update your email address.');", True)
            'Dim UFname As String = Request.QueryString("ufname").ToString
            'Dim ULname As String = Request.QueryString("ulname").ToString
            'Dim UEmailID As String = Request.QueryString("emailid").ToString
            'Dim UName As String = Request.QueryString("uname").ToString

            txtUname.Text = Request.QueryString("uname").ToString
            txtFname.Text = Request.QueryString("ufname").ToString
            txtLname.Text = Request.QueryString("ulname").ToString
            txtEmail.Text = Request.QueryString("emailid").ToString
        End If
    End Sub

   
End Class
