Imports System.Web.Mail
Imports System.Net.Mail.SmtpClient
Imports System.Net.Mail
Imports System.Net.NetworkCredential
Partial Class _Default
    Inherits System.Web.UI.Page

    'Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
    '    Try
    '        Dim MailMsg As New System.Net.Mail.MailMessage

    '        Dim varStrTicketNo = 25

    '        'Dim objsmtp As New SmtpClient("secure.emailsrvr.com")
    '        'objsmtp.Credentials = New System.Net.NetworkCredential("apagare", "welc0me")

    '        Dim objsmtp As New SmtpClient("secure.emailsrvr.com")
    '        objsmtp.Credentials = New System.Net.NetworkCredential("apagare@edictate.com", "welc0me")

    '        MailMsg.From = New MailAddress("support@edictate.com")

    '        MailMsg.To.Add("apagare@edictate.com")

    '        Dim varStrMailBody As String
    '        varStrMailBody = "<font size=""2"" face=""Trebuchet MS"" color=""Blue"">Dear Valued Customer,<BR><BR>Thank you for contacting us using Customer Trouble Ticket. The ticket# for your query/issue is " & varStrTicketNo & " .We will resolve the same and get back to you at the earliest.<BR><BR>Thank you for your patience and support.<BR><BR><BR><BR><font size=3 color=Gray><B>E-Dictate Customer Support Team<BR>The Best Value Transcription Solution</B></font></font>"

    '        MailMsg.Subject = "Ticket # :" & varStrTicketNo & " Date Of Submit :" & Now() & ""
    '        MailMsg.Body = varStrMailBody
    '        MailMsg.IsBodyHtml = True


    '        objsmtp.Send(MailMsg)


    '        objsmtp.DeliveryMethod = SmtpDeliveryMethod.PickupDirectoryFromIis

    '        'End mail sending 

    '        Response.Write("<center><font face=""Arial"" size=""2"" color=""#000080"">Ticket raised successfully</font></center>")
    '        Response.Write("<center><a href=""ClosedWindow.aspx""><font face=""Arial"" size=""2"">Close window</font></a></center>")
    '        Response.End()
    '    Catch ex As Exception

    '    End Try
    'End Sub
End Class