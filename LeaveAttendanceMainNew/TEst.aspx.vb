Imports System.Net.Mail.SmtpClient
Imports System.Net.Mail
Imports System.Net.NetworkCredential
Partial Class LeaveAttendanceMainNew_TEst
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Response.Write(SendMail("apagare@edictate.com", "apagare@edictate.com", "apagare@edictate.com", "Test Mail", "This is tesing "))
    End Sub
    Public Function SendMail(ByVal From As String, ByVal ToMail As String, ByVal CC As String, ByVal Subject As String, ByVal MailMatter As String) As String
        Dim MailMsg As New System.Net.Mail.MailMessage
        Dim varStrTo As String = String.Empty
        Dim objsmtp As New SmtpClient("secure.emailsrvr.com")

        Try
            objsmtp.Credentials = New System.Net.NetworkCredential("admin@edictate.com", "welc0me")
            MailMsg.From = New MailAddress(From)

            Dim varToMail() As String
            If ToMail <> "" Then
                If ToMail.IndexOf(",") > 0 Then
                    varToMail = ToMail.Split(",")

                    For i As Integer = 0 To UBound(varToMail)
                        If i = 0 Then
                            MailMsg.To.Add(Trim(varToMail(i)))
                        Else
                            MailMsg.CC.Add(Trim(varToMail(i)))
                        End If
                    Next
                Else
                    MailMsg.To.Add(Trim(ToMail))
                End If
            End If

            Dim varCCMail() As String

            If CC <> "" Then
                If CC.IndexOf(",") > 0 Then
                    varCCMail = CC.Split(",")
                    For j As Integer = 0 To UBound(varCCMail)
                        MailMsg.CC.Add(Trim(varCCMail(j)))
                    Next
                Else
                    MailMsg.CC.Add(Trim(CC))
                End If
            End If


            MailMsg.Subject = "SecureXSoft Testing : " & Subject
            MailMsg.IsBodyHtml = True
            MailMsg.Body = MailMatter


            objsmtp.Send(MailMsg)
            objsmtp.DeliveryMethod = SmtpDeliveryMethod.PickupDirectoryFromIis
            Return True

        Catch ex As Exception
            Return ex.Message
        Finally
            MailMsg.Dispose()
            MailMsg = Nothing
            objsmtp = Nothing
        End Try
    End Function
End Class
