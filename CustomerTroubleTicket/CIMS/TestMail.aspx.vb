Imports System.Web.Mail
Imports System.Net.Mail.SmtpClient
Imports System.Net.Mail
Imports System.Net.NetworkCredential
Partial Class CIMS_TestMail
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim MailMsg As New System.Net.Mail.MailMessage
        Dim varStrPmail As String = String.Empty
        Dim varStrSmail As String = String.Empty

        Dim varStrTicketDetails As String = String.Empty
        Dim varStrPriority As String
        Dim varStrFileAttach As String
        Dim varStrInsert As String
        Dim varStrSubject As String
        Dim varStrTID As String
        Dim varStrUserName As String = String.Empty

        Dim varAccName = String.Empty
        Dim varUserName = String.Empty
        Dim varEmail = String.Empty
        Dim varPhone = String.Empty
        Dim varDescription = "This is testing ticket mail,please ignore it."

        Dim varStrTicketNo As Long

        varStrPmail = "apagare@edictate.com"
        varStrSmail = "apagare@edictate.com"

        Dim objsmtp As New SmtpClient("secure.emailsrvr.com")
        objsmtp.Credentials = New System.Net.NetworkCredential("apagare@edictate.com", "welc0me")

        MailMsg.From = New MailAddress("support@edictate.com")

        If Not String.IsNullOrEmpty(varStrPmail) Then
            MailMsg.To.Add(varStrPmail)
        Else
            If Not String.IsNullOrEmpty(varStrSmail) Then
                If Trim(UCase(varStrPmail)) <> Trim(UCase(varStrSmail)) Then
                    MailMsg.To.Add(varStrSmail)
                End If
            End If
        End If


        'Dim varStrMailBody As String
        'varStrMailBody = "<font size=""2"" face=""Trebuchet MS"" color=""Blue"">Dear Valued Customer,<BR><BR>Thank you for contacting us using Customer Trouble Ticket. The ticket# for your query/issue is " & varStrTicketNo & " .We will resolve the same and get back to you at the earliest.<BR><BR>Thank you for your patience and support.<BR><BR><BR><BR><font size=3 color=Gray><B>E-Dictate Customer Support Team<BR>The Best Value Transcription Solution</B></font></font>"
        Dim body As String = String.Empty

        body = body & "  "

        body = body & "<font face=Verdana size=2pt color=#000096>Dear Valued Customer, <BR><BR> Thank you for contacting E-Dictate Customer Support Team. Your Trouble Ticket has been received and is being resolved.</font>"
        body = body & "<BR><hr style=""height:8pt"" color=#f0f8ff  />"
        body = body & "<BR><font face=Verdana size=2pt color=#000096>Please mention the following Ticket number when contacting E-Dictate: </font> <BR> "
        body = body & "<BR><font face=Verdana size=2pt color=#0caae0><b>Ticket Number : </b>" & varStrTicketNo & "</font><BR>"
        body = body & "<BR><font face=Verdana size=2pt color=#000096>The following was submitted to E-Dictate: </font> <BR> "
        body = body & "<BR><font face=Verdana size=2pt color=#0caae0><b>Clinic/Office : </b>" & varAccName & "</font><BR>"
        body = body & "<BR><font face=Verdana size=2pt color=#0caae0><b>Name : </b>" & varUserName & "</font><BR>"
        body = body & "<font face=Verdana size=2pt color=#0caae0><b>Email : </b>" & varEmail & "</font><BR>"
        body = body & "<font face=Verdana size=2pt color=#0caae0><b>Phone : </b>" & varPhone & "</font><BR>"
        body = body & "<BR><table><tr><td valign=top><font face=Verdana size=2pt color=#0caae0><b>Description : </b></font></td><td><font face=Verdana size=2pt color=#0caae0>" & varDescription & "</font></td></tr></table></font><BR>"
        body = body & "<BR><font face=""Trebuchet MS"" size=2pt color=#000096>Thank you for your patience and support.</font><BR>"

        body = body & "<BR><font face=""Trebuchet MS"" size=2pt color=#7b68ee><b>E-Dictate Customer Support Team</b></font><BR>"
        body = body & "<font face=""Arial"" size=2pt color=#7b68ee><b>Tel:</b></font><font face=""Trebuchet MS"" size=2pt color=#000096>866.510.1111 Ext 11</font><font face=""Arial"" size=2pt color=#7b68ee><b> Fax:</b></font><font face=""Trebuchet MS"" size=2pt color=#000096>1.866.800.4020</font>"
        body = body & "<BR><font face=""Trebuchet MS"" size=2pt color=#7b68ee><b>E: </b><a href=""mailto:support@edictate.com"">support@edictate.com</a></font><BR>"

        body = body & "<BR><font face=""Trebuchet MS"" size=2pt color=#7b68ee><b>E-Dictate, L.L.C</b></font>"
        body = body & "<BR><font face=""Arial"" size=""0.5"" color=#b1aeb7>1627 Williamsburg Executive Suites</br>Suite 206</br>Palatine, IL 60067</br></font>"
        body = body & "<font face=""Arial"" size=""0.5"" color=#7b68ee><a href=""http://www.edictate.com"" target=""_blank"">http://www.edictate.com</a></br><i>The Best Value Transcription Solution</i></br></font>"

        body = body & "<BR><BR><font face=""Arial"" size=""0.5"" color=#b1aeb7><i>This message and any attachments may contain information that is protected by law as privileged and confidential, and is transmitted for the sole use of the intended recipient(s).If you are not the intended recipient, you are hereby notified that any use, dissemination, copying or retention of this e-mail or the information contained herein is strictly prohibited. If you have received this e-mail in error, please immediately notify the sender by e-mail and permanently delete this e-mail. </i></font><BR>"


        MailMsg.Subject = "Ticket # :" & varStrTicketNo & " Date Of Submit :" & Now() & ""
        MailMsg.Body = body
        MailMsg.IsBodyHtml = True


        If Not String.IsNullOrEmpty(varStrPmail) Or Not String.IsNullOrEmpty(varStrSmail) Then
            'objsmtp.Send(MailMsg)
        End If

        objsmtp.DeliveryMethod = SmtpDeliveryMethod.PickupDirectoryFromIis

        'End mail sending 
    End Sub
End Class
