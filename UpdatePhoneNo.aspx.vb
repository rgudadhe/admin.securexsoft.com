Imports System.Xml
Imports System.IO
Imports System.Configuration
Imports System.Net.Mail
Imports System.Net
Partial Class UpdatePhoneNo
    Inherits System.Web.UI.Page

    Protected Sub Button1_Click(sender As Object, e As System.EventArgs) Handles Button1.Click
        If IsPostBack Then
            Dim uname As String = DropDownList1.SelectedItem.ToString()
            Dim pno As String = txtphone.Text
            Dim updateby As String = Session("Username").ToString()
            If DropDownList1.SelectedItem.ToString = "Select User" Then
                Label1.Text = "Please select user."
            Else
                'Dim appPath As String = System.IO.Path.GetDirectoryName("\\sdoxmirth\d$\Phone Alert")
                'Dim configFile As String = System.IO.Path.Combine(appPath, "PhoneAlert.exe.config")
                'Dim configFileMap As New ExeConfigurationFileMap()
                'configFileMap.ExeConfigFilename = configFile
                'Dim config As System.Configuration.Configuration = ConfigurationManager.OpenMappedExeConfiguration(configFileMap, ConfigurationUserLevel.None)
                'config.AppSettings.Settings("Phone1").Value = "91" & txtphone.Text
                'config.Save()

                Dim xmldoc As XmlDocument = New XmlDocument()
                Dim phonenode As XmlNode = Nothing
                xmldoc.Load("\\sdoxmirth\d$\Phone Alert Nexmo\PhoneSettings.xml")
                phonenode = xmldoc.SelectSingleNode("//PhoneNumbers/Phone1")
                phonenode.InnerText = "91" & txtphone.Text
                xmldoc.Save("\\sdoxmirth\d$\Phone Alert Nexmo\PhoneSettings.xml")

                Label1.Text = "Phone number of " & DropDownList1.SelectedItem.ToString() & "[" & txtphone.Text & "] is selected for SCA Arise TAT."
                Dim dt As String
                dt = Calendar1.SelectedDate
                sendalertmail(uname, pno, dt)
                logs(updateby)
            End If


        End If


    End Sub
    Sub logs(ByVal uname As String)
        Dim strFile As String = "\\sqlbackup\d$\PhoneAlert\logs\Log_" & DateTime.Today.ToString("dd-MMM-yyyy") & ".txt"

        Dim sw As StreamWriter
        Dim fs As FileStream = Nothing

        If (Not File.Exists(strFile)) Then
            Try
                'Dim strFile As String = String.Format("C:\ErrorLog_{0}.txt", DateTime.Today.ToString("dd-MMM-yyyy"))
                File.AppendAllText(strFile, String.Format("Contact file updated by/at:- {0} {1}", uname, DateTime.Now))
                'sw.WriteLine("Start File Upload Log for today")

            Catch ex As Exception
                MsgBox("Error Creating Log File")
            End Try
        Else
            sw = File.AppendText(strFile)
            sw.WriteLine(vbCrLf & "Contact file updated by/at:- {0} {1} ", uname, DateTime.Now)
            sw.Close()
        End If
    End Sub

    Protected Sub form1_Load(sender As Object, e As System.EventArgs) Handles form1.Load
        Label1.ForeColor = Drawing.Color.Red
        Label1.Font.Bold = True
        'Response.Write(Session("Username").ToString())
    End Sub

    Public Sub sendalertmail(ByVal username As String, ByVal pnumber As String, ByVal dt As String)
        'Dim MAILER As New SASMTPLib.CoSMTPMail
        'MAILER.FromName = "Christopher Aloysius"
        'MAILER.Subject = "STAT Coverage Phone Update"
        'MAILER.FromAddress = "caloysius@medofficepro.com"
        'MAILER.RemoteHost = "secure.emailsrvr.com"
        'MAILER.UserName = "alert@edictate.com"
        'MAILER.Password = "Welcome@medofficepro2011"
        'MAILER.AddRecipient("sbucmtsppt@edictate.com")
        'MAILER.AddCC("sjagtap@edictate.com")
        'MAILER.AddCC("caloysius@medofficepro.com")
        ''MAILER.AddRecipient("vraut@edictate.com")
        'MAILER.Priority = 1
        'MAILER.Urgent = True
        'MAILER.HtmlText = "<p><font face='Arial' color='#000080' size='2'>Dear Team, <BR><BR>Following user has been added to STAT coverage.<BR><BR><b> User:-" & username & "<BR>Phone No:- " & pnumber & "<BR>Date:- " & dt & " </b><BR><BR>Regards, <BR><BR>Christopher Aloysius</font></p>"

        'MAILER.SendMail()
        'MAILER = Nothing

        Dim message As New MailMessage()
        Dim fromName As String = "Do Not Reply"
        Dim from As String = "donotreply@medofficepro.com"
        Dim toAddress As String = "sbucmtsppt@edictate.com"
        'Dim bccaddress As String = "sdoxreg@edictate.com"
        Dim smtpadd As String = "email-smtp.us-west-2.amazonaws.com"
        Dim smtpuname As String = "AKIA44IE6PBA24MEZW5P"
        Dim smtppass As String = "BLZJ9U1M6AILVx4FRA8E1CdRvOoRV9rx7/HBXEcNaeJ6"
        Dim port As Integer = 587
        Dim subject As String = "STAT Coverage Phone Update"
        Dim configset As String = "ConfigSet"

        message.IsBodyHtml = True
        message.From = New MailAddress(from, fromName)
        message.To.Add(New MailAddress(toAddress))
        message.Subject = "STAT Coverage Phone Update"


        'Dim reader As New StreamReader(Server.MapPath("../authorization/UsernameConfirmation.htm"))
        'Dim readFile As String = reader.ReadToEnd()
        'Dim myString As String = ""
        'myString = readFile
        'myString = myString.Replace("$$UNAME$$", Ufname)
        'myString = myString.Replace("$$BODY$$", body.ToString)
        'myString = myString.Replace("$$BODY2$$", body2.ToString)

        message.IsBodyHtml = True
        message.Body = "<p><font face='Arial' color='#000080' size='2'>Dear Team, <BR><BR>Following user has been added to STAT coverage.<BR><BR><b> User:-" & username & "<BR>Phone No:- " & pnumber & "<BR>Date:- " & dt & " </b><BR><BR>Regards, <BR><BR>Christopher Aloysius</font></p>"
        message.From = New MailAddress(from, fromName)
        message.To.Add(New MailAddress(toAddress))
        ' message.Bcc.Add(New MailAddress("sjagtap@medofficepro.com"))
        message.Subject = subject
        'message.Headers.Add("X-SES-CONFIGURATION-SET", configset)
        Dim client As New System.Net.Mail.SmtpClient(smtpadd, port)

        client.Credentials = New NetworkCredential(smtpuname, smtppass)
        client.EnableSsl = True
        client.Send(message)


    End Sub

    Protected Sub DropDownList1_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles DropDownList1.SelectedIndexChanged
        txtphone.Text = DropDownList1.SelectedValue
    End Sub
End Class
