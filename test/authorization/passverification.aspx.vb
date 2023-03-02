Imports System.IO
Imports System.Data
Imports System.Data.SqlClient
Imports System.Configuration
Imports EncryPass

Partial Class Passwordmodule_passverification
    Inherits System.Web.UI.Page
    Dim ans1 As String
    Dim ans2 As String
    Dim ans3 As String

    Protected Sub signup_Load(sender As Object, e As System.EventArgs) Handles signup.Load
        Dim uid As String = Request.QueryString("uid")
        Dim blSecurity As New ETS.BL.BALSecurity
        Dim DT As DataTable = blSecurity.getUserSecretQuestions(uid)
        If DT.Rows.Count > 0 Then
            Dim DR As DataRow = DT.Rows(0)
            lblq1.Text = DR.Item("q1").ToString
            lblq2.Text = DR.Item("q2").ToString
            lblq3.Text = DR.Item("q3").ToString
            ans1 = DR.Item("s1").ToString
            ans2 = DR.Item("s2").ToString
            ans3 = DR.Item("s3").ToString
        End If

        'Dim constr As String = ConfigurationManager.ConnectionStrings("ETSConnectionString").ConnectionString
        'Dim con As New SqlConnection(constr)
        'con.Open()
        'Dim cmd1 As New SqlCommand("select * from tblusersecretq where uid='" & uid & "'", con)
        'Dim dr As SqlDataReader
        'dr = cmd1.ExecuteReader()
        'dr.Read()
        'If dr.HasRows Then
        '    lblq1.Text = dr.Item("q1").ToString
        '    lblq2.Text = dr.Item("q2").ToString
        '    lblq3.Text = dr.Item("q3").ToString
        '    ans1 = dr.Item("s1").ToString
        '    ans2 = dr.Item("s2").ToString
        '    ans3 = dr.Item("s3").ToString
        'End If
        'con.Close()
    End Sub

    Protected Sub submit_Click(sender As Object, e As System.EventArgs) Handles submit.Click

        Dim uid As String = Request.QueryString("uid")
        Dim resetCode As String = Guid.NewGuid().ToString()
        Dim blSecurity As New ETS.BL.BALSecurity

        If ans1 = txtans1.Text And ans2 = txtans2.Text And ans3 = txtans3.Text Then
            Try
                blSecurity.UserPasswordResetTemp(uid.ToString, resetCode)
                sendpasswordresetmail(uid.ToString, resetCode)
                Label2.ForeColor = Drawing.Color.Green
                Label2.Text = "Congratulations! Your password reset link has been sent to your <br /> email address."
                txtans1.Text = ""
                txtans2.Text = ""
                txtans3.Text = ""
                submit.Enabled = False
            Catch ex As Exception
                Response.Write(ex.Message)
            End Try
           
        Else
            Label2.Text = "Answers does not match. Please try again!"
        End If
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
        myString = myString.Replace("$$BODY$$", "Please click following link to reset your password. If you have not requested this, please <a href=""mailto:support@medofficepro.com""> contact support </a>.")
        myString = myString.Replace("$$BTEXT$$", "Reset Password")
        myString = myString.Replace("$$EVERIFY$$", "https://adminstaging.securexsoft.com/authorization/PReset.aspx?resetCode=" & resetcode & "&uid=" & uid)

        Dim body As String = "Hi " & Ufname & ","
        MAILER.FromName = "E-Dictate User Support"
        MAILER.FromAddress = "hr@edictate.com"
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
End Class
