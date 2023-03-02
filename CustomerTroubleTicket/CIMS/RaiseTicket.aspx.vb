Imports System.Web.Mail
Imports System.Net.Mail.SmtpClient
Imports System.Net.Mail
Imports System.Net.NetworkCredential
Imports System.Data
Partial Class CIMS_RaiseTicket
    Inherits BasePage
    Public Function OpenConnection(ByRef Conn As Data.SqlClient.SqlConnection) As Data.SqlClient.SqlConnection
        Try
            Conn.ConnectionString = System.Configuration.ConfigurationManager.AppSettings("ETSCon")
            Conn.Open()
            Return Conn
        Catch ex As Exception
        End Try
    End Function
    Protected Sub Button2_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button2.Click
        Dim objConn As New Data.SqlClient.SqlConnection
        objConn = OpenConnection(objConn)

        Try
            Dim varStrIssueID As String
            Dim varStrTicketDetails As String
            Dim varStrPriority As String
            Dim varStrFileAttach As String
            Dim varStrInsert As String
            Dim varStrSubject As String
            Dim varStrTID As String

            varStrTID = Guid.NewGuid.ToString

            varStrSubject = Replace(txtSubject.Text.ToString, "'", "''")
            varStrIssueID = DropDownIssueType.Items(DropDownIssueType.SelectedIndex).Value.ToString
            varStrTicketDetails = Replace(txtIssueDesc.InnerText.ToString, "'", "''")
            varStrPriority = DropDownPriority.Items(DropDownPriority.SelectedIndex).Value.ToString

            varStrInsert = "INSERT INTO dbo.tblCustomerTickets(TicketID,Subject,AccID,UserID,IssueID,TicketDetails,Priority,Status,DatePosted)VALUES('" & varStrTID & "','" & varStrSubject & "','" & Session("AccID").ToString() & "','" & Session("UserID").ToString() & "','" & varStrIssueID & "','" & varStrTicketDetails & "','" & varStrPriority & "','Open','" & Now() & "')"

            Dim InsertCmd As New Data.SqlClient.SqlCommand
            InsertCmd.Connection = objConn
            InsertCmd.CommandType = Data.CommandType.Text
            InsertCmd.CommandText = varStrInsert
            InsertCmd.ExecuteNonQuery()
            InsertCmd = Nothing


            'Mail Send to Customer for new ticket


            Dim varStrUserName As String = String.Empty
            Dim varStrPmail As String = String.Empty
            Dim varStrSmail As String = String.Empty

            Dim objCmd As New Data.SqlClient.SqlCommand("SELECT AccountName,PriEmail,SecEMail FROM dbo.tblAccounts WHERE AccountID='" & Session("AccID").ToString & "'", objConn)
            Dim objRec As Data.SqlClient.SqlDataReader = objCmd.ExecuteReader

            If objRec.HasRows Then
                While objRec.Read
                    If Not objRec.IsDBNull(objRec.GetOrdinal("AccountName")) Then
                        varStrUserName = objRec.GetString(objRec.GetOrdinal("AccountName"))
                    End If
                    If Not objRec.IsDBNull(objRec.GetOrdinal("PriEmail")) Then
                        varStrPmail = objRec.GetString(objRec.GetOrdinal("PriEmail"))
                    End If
                    If Not objRec.IsDBNull(objRec.GetOrdinal("SecEmail")) Then
                        varStrSmail = objRec.GetString(objRec.GetOrdinal("SecEmail"))
                    End If
                End While
            End If
            objRec.Close()
            objRec = Nothing
            objCmd = Nothing



            Dim varStrTicketNo As Long

            Dim objCmdTNO As New Data.SqlClient.SqlCommand("SELECT TicketNo FROM dbo.tblCustomerTickets WHERE TicketID='" & varStrTID & "'", objConn)
            Dim objRecTNO As Data.SqlClient.SqlDataReader = objCmdTNO.ExecuteReader
            If objRecTNO.HasRows Then
                While objRecTNO.Read
                    varStrTicketNo = objRecTNO("TicketNo")
                End While
            End If
            objRecTNO.Close()
            objRecTNO = Nothing
            objCmdTNO = Nothing

            'Get Tel No of the user
            Dim varPhone As String = String.Empty

            Dim objCmdTelNO As New Data.SqlClient.SqlCommand("select Tel from SecureWeb.dbo.tblUsers where UserID='" & Session("UserID").ToString & "'", objConn)
            Dim objRecTelNO As Data.SqlClient.SqlDataReader = objCmdTelNO.ExecuteReader
            If objRecTelNO.HasRows Then
                While objRecTelNO.Read
                    If Not objRecTelNO.IsDBNull(objRecTelNO.GetOrdinal("Tel")) Then
                        varPhone = objRecTelNO("Tel")
                    End If
                End While
            End If
            objRecTelNO.Close()
            objRecTelNO = Nothing
            objCmdTelNO = Nothing

            'End gettting information

            Dim body As String = String.Empty
            Dim varAccName = varStrUserName
            Dim varUserName = Session("uname").ToString
            Dim varEmail = Session("EMailAddress").ToString
            Dim varDescription = varStrTicketDetails.ToString


            Dim MailMsg As New System.Net.Mail.MailMessage

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

            MailMsg.Bcc.Add("support@edictate.com")

            'Dim varStrMailBody As String
            'varStrMailBody = "<font size=""2"" face=""Trebuchet MS"" color=""Blue"">Dear Valued Customer,<BR><BR>Thank you for contacting us using Customer Trouble Ticket. The ticket# for your query/issue is " & varStrTicketNo & " .We will resolve the same and get back to you at the earliest.<BR><BR>Thank you for your patience and support.<BR><BR><BR><BR><font size=3 color=Gray><B>E-Dictate Customer Support Team<BR>The Best Value Transcription Solution</B></font></font>"

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
                objsmtp.Send(MailMsg)
            End If

            objsmtp.DeliveryMethod = SmtpDeliveryMethod.PickupDirectoryFromIis

            'End mail sending 

            Response.Write("<center><font face=""Arial"" size=""2"" color=""#000080"">Ticket raised successfully</font></center>")
            Response.Write("<center><a href=""ClosedWindow.aspx""><font face=""Arial"" size=""2"">Close window</font></a></center>")
            Response.End()
            'Exit Sub
        Catch ex As Exception
            If Trim(UCase(ex.Message)) <> Trim(UCase("Thread was being aborted.")) Then
                'Response.Write(ex.Message)
            End If
        Finally
            If objConn.State <> Data.ConnectionState.Closed Then
                objConn.Close()
                objConn = Nothing
            End If
        End Try
    End Sub
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim objConn As New Data.SqlClient.SqlConnection
        objConn = OpenConnection(objConn)
        Try
            If Not Page.IsPostBack Then
                Dim varDropDownCateItem As New ListItem
                varDropDownCateItem.Text = "Please Select"
                varDropDownCateItem.Value = ""

                Dim objSQLAdapter As Data.SqlClient.SqlDataAdapter = New Data.SqlClient.SqlDataAdapter("SELECT IssueName ,IssueID FROM dbo.tblCustomerIssueType WHERE (IsDeleted IS NULL OR IsDeleted ='False')", objConn)
                Dim objDataSet As New DataSet
                objSQLAdapter.Fill(objDataSet, "tblCustomerIssueType")
                DropDownIssueType.DataSource = objDataSet
                DropDownIssueType.DataTextField = "IssueName"
                DropDownIssueType.DataValueField = "IssueID"
                DropDownIssueType.DataBind()
                DropDownIssueType.Items.Insert(0, varDropDownCateItem)

            End If
        Catch ex As Exception
        Finally
            If objConn.State <> Data.ConnectionState.Closed Then
                objConn.Close()
                objConn = Nothing
            End If
        End Try
    End Sub
End Class
