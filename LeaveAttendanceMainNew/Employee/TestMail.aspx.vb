Imports Microsoft.VisualBasic
Imports System.Web.Mail
Imports System.Net.Mail.SmtpClient
Imports System.Net.Mail
Imports System.Net.NetworkCredential
Imports sasmtp_dotnetproxy
Partial Class LeaveAttendanceMainNew_Employee_TestMail
    Inherits System.Web.UI.Page
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim varStrEmpMailCC = "apagare@edictate.com"
        Dim varToMail = "apagare@edictate.com,apagare@edictate.com"
        Dim varCCmail = "apagare@edictate.com,apagare@edictate.com,apagare@edictate.com"
        Dim varMailSubject = "Leave Application of Anil Pagare"
        Dim varMailMatter = "Anil Pagare has applied for the Casual Leave from 10/15/2009 to 10/15/2009 <BR> Reason: This is testing purpose,please ignore it."
        ''Response.Write(varToMail)
        SendMail(varStrEmpMailCC, varToMail, varCCmail, varMailSubject, varMailMatter)
        'Dim objMainModule As New MainModule
        'Dim objConn As New Data.SqlClient.SqlConnection
        'Try
        '    objConn = objMainModule.OpenConnection(objConn)

        '    Dim varStrToMail As String = String.Empty

        '    Dim objGetMailID As New Data.SqlClient.SqlCommand("SELECT OfficialMailID FROM DBO.tblUsers U INNER JOIN DBO.tblERSSTicketsAccess TA ON U.UserID=TA.UserID WHERE TA.IssueID IN (SELECT IssueID FROM DBO.tblTickets WHERE TicketID='2B361ED7-5248-44E7-AD6D-C81A3FFA8DB5')", objConn)
        '    Dim objRecGetMailID As Data.SqlClient.SqlDataReader = objGetMailID.ExecuteReader
        '    If objRecGetMailID.HasRows Then
        '        While objRecGetMailID.Read
        '            If Not objRecGetMailID.IsDBNull(objRecGetMailID.GetOrdinal("OfficialMailID")) And Not String.IsNullOrEmpty(objRecGetMailID("OfficialMailID")) Then
        '                If String.IsNullOrEmpty(varStrToMail) Then
        '                    varStrToMail = objRecGetMailID.GetString(objRecGetMailID.GetOrdinal("OfficialMailID"))
        '                Else
        '                    varStrToMail = varStrToMail & "," & objRecGetMailID.GetString(objRecGetMailID.GetOrdinal("OfficialMailID"))
        '                End If
        '            End If
        '            Response.Write(varStrToMail & "<BR>")
        '        End While
        '    End If
        '    objRecGetMailID.Close()
        '    objRecGetMailID = Nothing
        '    objGetMailID = Nothing

        'Catch ex As Exception
        'Finally
        '    If objConn.State <> Data.ConnectionState.Closed Then
        '        objConn.Close()
        '        objConn = Nothing
        '    End If
        'End Try



    End Sub
    Public Function SendMail(ByVal From As String, ByVal ToMail As String, ByVal CC As String, ByVal Subject As String, ByVal MailMatter As String) As Boolean
        Try
            'Dim varToMail(10) As String
            'Dim varCCMail(10) As String
            'Dim i As Integer
            'Dim MailMsg As New System.Net.Mail.MailMessage

            'Dim objsmtp As New SmtpClient("secure.emailsrvr.com")
            'objsmtp.Credentials = New System.Net.NetworkCredential("admin@edictate.com", "welc0me")

            'MailMsg.From = New MailAddress(From)

            'If ToMail <> "" Then
            '    If ToMail.IndexOf(",") > 0 Then
            '        varToMail = ToMail.Split(",")

            '        For i = 0 To UBound(varToMail)
            '            If i = 0 Then
            '                MailMsg.To.Add(Trim(varToMail(i)))
            '            Else
            '                MailMsg.CC.Add(Trim(varToMail(i)))
            '            End If
            '        Next
            '    Else
            '        MailMsg.To.Add(ToMail)
            '    End If
            'End If

            'If CC <> "" Then
            '    If CC.IndexOf(",") > 0 Then
            '        varCCMail = CC.Split(",")

            '        For i = 0 To UBound(varCCMail)
            '            MailMsg.CC.Add(Trim(varCCMail(i)))
            '        Next
            '    Else
            '        MailMsg.CC.Add(Trim(CC))
            '    End If
            'End If

            ''MailMsg.Bcc.Add(varStrBcc)
            'MailMsg.Subject = Subject
            'MailMsg.Body = MailMatter
            'MailMsg.IsBodyHtml = True

            'objsmtp.DeliveryMethod = SmtpDeliveryMethod.PickupDirectoryFromIis

            'Response.Write(MailMsg.To.ToString & "<BR>")
            'Response.Write(MailMsg.From.ToString & "<BR>")
            'Response.Write(MailMsg.CC.ToString)
            'objsmtp.Send(MailMsg)


            Dim iMail As New SASMTPLib.CoSMTPMail()


            'iMail.RemoteHost = "smtp.edictateindia.com"
            'iMail.UserName = "edictatemail"
            'iMail.Password = "ed1ctatema!l"

            iMail.RemoteHost = "secure.emailsrvr.com"
            iMail.UserName = "admin@edictate.com"
            iMail.Password = "welc0me"

            'iMail.Port = 25
            iMail.FromAddress = From

            Dim varToMail() As String

            If ToMail <> "" Then
                If ToMail.IndexOf(",") > 0 Then
                    varToMail = ToMail.Split(",")

                    For i As Integer = 0 To UBound(varToMail)
                        iMail.AddRecipient(Trim(varToMail(i)), Trim(varToMail(i)))
                    Next
                Else
                    iMail.AddRecipient(Trim(ToMail), Trim(ToMail))
                End If
            End If

            'iMail.AddRecipient(ToMail, ToMail)
            Dim varCCMail() As String

            If CC <> "" Then
                If CC.IndexOf(",") > 0 Then
                    varCCMail = CC.Split(",")
                    For j As Integer = 0 To UBound(varCCMail)
                        iMail.AddCC(Trim(varCCMail(j)), Trim(varCCMail(j)))
                    Next
                Else
                    iMail.AddCC(Trim(CC), Trim(CC))
                End If
            End If

            'iMail.AddCC(CC, CC)
            iMail.AddBCC("apagare@edictate.com", "apagare@edictate.com")
            iMail.ReturnReceipt = False
            iMail.Subject = Subject
            iMail.HtmlText = MailMatter
            'iMail.SendMail()

            Return True

            'Return True

        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Function
End Class
