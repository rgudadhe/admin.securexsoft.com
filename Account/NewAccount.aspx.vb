Imports System.Data
Imports SASMTPLib
Imports System.IO

Partial Class Department_Default
    Inherits BasePage
    Public ProcFolder As String = Server.MapPath("../ETS_Files")
    Protected Sub Button1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button1.Click
        ' Try
        If TxtSTAT.SelectedValue <> 0 And TxtTAT.SelectedValue <> 0 Then
            If CInt(TxtSTAT.SelectedValue) > CInt(TxtTAT.SelectedValue) Then
                Response.Write("Normal TAT should not be less than TAT for STAT.")
                TxtSTAT.Focus()
                Exit Sub
            End If
        End If


        If TxtProtocolMins.Text = "" Then
            TxtProtocolMins.Text = 0
        End If

        If String.IsNullOrEmpty(Session("ContractorID").ToString) Then
            lblMsg.Text = "Session expires,please login again..."
            Exit Sub
        End If


        If CheckValidAccount() = True Then



            'Response.Write("Valid Account ")
            Dim strFolder As String
            strFolder = ProcFolder & "\CustVoice"

            If DLVoice.SelectedValue = "1" Then
                If System.IO.Directory.Exists(strFolder) Then
                    strFolder = strFolder & "\" & TxtFolder.Text
                    If Not System.IO.Directory.Exists(strFolder) Then
                        System.IO.Directory.CreateDirectory(strFolder)
                    End If
                End If
            End If

            Dim clsAct As ETS.BL.Accounts
            Try
                clsAct = New ETS.BL.Accounts
                Dim InstatnceID As Integer
                With clsAct
                    .ContractorID = Session("ContractorID").ToString
                    .AccountName = TxtAccountName.Text
                    .Description = TxtDescription.Text

                    If Not String.IsNullOrEmpty(DrpCategory.Text) Then
                        .Category = DrpCategory.Text.ToString
                        'Else
                        '    .Category = "NULL"
                    End If

                    If DLFaxPlus.SelectedValue = "1" Then
                        .FaxPlusService = True

                        .FaxPlusMode = DLFMode.SelectedValue

                    Else
                        .FaxPlusService = False
                        .FaxPlusMode = 0
                    End If
                    .PriContact = TxtPriContact.Text
                    .FacilityID = TxtFacilityID.Text
                    .PriEmail = TxtPriEmail.Text
                    .PriPhoneNo = txtPriPhone.Text
                    .PriFaxNo = txtPriFaxNo.Text
                    .SecContact = TxtSecContact.Text
                    .SecEmail = TxtSecEmail.Text
                    .SecPhoneNo = txtSecPhone.Text
                    .SecFaxNo = txtSecFaxNo.Text
                    .Address = TxtAdddress.Text
                    .RSSType = TxtRSSType.Text
                    .ProtocolMins = TxtProtocolMins.Text
                    .TAT = TxtTAT.Text
                    .STAT = TxtSTAT.Text
                    .TZDifference = DDLTZ.SelectedValue
                    .Time = txtTime.Text
                    .OfficialSite = TxtOfficialSite.Text
                    .Mode = DLMode.SelectedValue
                    .foldername = TxtFolder.Text
                    .BillActnumber = TxtBillNumber.Text
                    .City = TXTCity.Text
                    .State = TXTState.Text
                    .Country = DDLCntry.SelectedValue
                    .Zip = TXTZip.Text
                    .comments = txtComments.Text
                    .VFolder = DLVoice.SelectedValue
                    .PriTitle = TxtPriTitle.Text
                    .SecTitle = TxtSecTitle.Text
                    .InDirect = DLIndirect.SelectedValue
                    .AutoFaxExcp = DLFaxExcp.SelectedValue
                    .WebSite = DLWebsite.SelectedValue
                    'Changes Done by Rahul to make Default Fax Interface GFI
                    .FaxInterface = "B"
                    Dim clsCon As New ETS.BL.Contractor
                    With clsCon
                        .ContractorID = Session("ContractorID")
                        .getContractorDetails()
                        InstatnceID = .InstanceID
                    End With
                    clsCon = Nothing
                    If Not String.IsNullOrEmpty(ddlMIS.SelectedValue) Then
                        .MISRep = ddlMIS.SelectedValue
                    End If

                    If InstatnceID <= 0 Then
                        lblMsg.Text = "Instance is missing"
                    Else
                        .InstanceID = InstatnceID
                        If .InsertAccountDetails() = 1 Then
                            Dim varBody As String = "<font face='Trebuchet MS' size='2' color='#000080'>Dear Team</font><p><font color='#000080'><font face='Trebuchet MS' size='2'>New account has been setup on SecureXSoft.</font><br><font face='Trebuchet MS' size='2'>Account Name : " & TxtAccountName.Text & "</font><br></font></p><p><font color='#000080'><font face='Trebuchet MS' size='2'>Thanks,</font><br><font face='Trebuchet MS' size='2'>E-Dictate Support Team</font><br>"
                            Dim varSubject As String = "SecureXSoft - New Account : " & TxtAccountName.Text
                            Dim obj As MainModule
                            Try
                                obj = New MainModule
                                emailconfirmmail(TxtAccountName.Text)
                                ' If obj.NewAccPhyCreationMail("techsupport@edictate.com", "support@medofficepro.com", "caloysius@edictate.com,billing@medofficepro.com,mtsupport@edictate.com", varSubject, varBody) = True Then
                                lblMsg.Text = "Account has been added successfully"
                                'End If
                            Catch ex As Exception
                                lblMsg.Text = ex.Message
                            Finally
                                obj = Nothing
                            End Try
                        Else
                            lblMsg.Text = "Failed Adding Account"
                        End If
                        Panel1.Visible = False
                    End If
                End With
            Catch ex As Exception
                lblMsg.Text = ex.Message
            Finally
                clsAct = Nothing
            End Try
        End If
    End Sub

    Protected Sub emailconfirmmail(ByVal accname As String)
        Try


            Dim MAILER As New SASMTPLib.CoSMTPMail
            Dim Ufname As String = String.Empty
            Dim Uemail As String = String.Empty
            Dim btext As String = String.Empty

            Dim reader As New StreamReader(Server.MapPath("~/account/confirmation.htm"))
            Dim readFile As String = reader.ReadToEnd()
            Dim myString As String = ""
            myString = readFile

            btext = "Following account has been created.<br><br>Account name - " & accname.ToString & "<br> <br>Thanks you."
            
            myString = myString.Replace("$$BODY$$", btext)

            MAILER.FromName = "Do not reply"
            MAILER.FromAddress = "donotreply@medofficepro.com"
            MAILER.RemoteHost = "secure.emailsrvr.com"
            MAILER.UserName = "alert@edictate.com"
            MAILER.Password = "Welcome@medofficepro2011"
            MAILER.AddRecipient("billing@medofficepro.com")
            'MAILER.AddRecipient("vraut@edictate.com")
            MAILER.Priority = 1
            MAILER.Urgent = True
            ''body += "<br /><br />You've successfully verified your Secure-Scribe registered email-id."
            ''body += "<br /><br />Thanks,"
            ''body += "<br /><br />Customer Support Team"
            MAILER.HtmlText = myString
            MAILER.Subject = "New account alert"
            MAILER.SendMail()
            MAILER = Nothing
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        lblMsg.Text = String.Empty
        If Not IsPostBack Then
            DLFMode.Enabled = False
        End If
    End Sub

    Protected Sub DLFaxPlus_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles DLFaxPlus.SelectedIndexChanged
        If DLFaxPlus.SelectedValue = "1" Then
            DLFMode.Enabled = True
        Else
            DLFMode.Enabled = False
        End If
    End Sub
    Protected Function CheckValidAccount() As Boolean
        Dim clsAcc As ETS.BL.Accounts
        Dim DSAcc As New Data.DataSet
        Dim DV As New Data.DataView
        Try
            clsAcc = New ETS.BL.Accounts
            DSAcc = clsAcc.getAccountList(Session("WorkGroupID").ToString, Session("ContractorID").ToString, String.Empty)

            If DSAcc.Tables.Count > 0 Then
                If DSAcc.Tables(0).Rows.Count > 0 Then
                    DV = New Data.DataView(DSAcc.Tables(0), " AccountName='" & TxtAccountName.Text.ToString & "'  ", String.Empty, DataViewRowState.CurrentRows)

                    If DV.Count > 0 Then
                        lblMsg.Text = "Account Name already exists. Please try another Account Name."
                        Return False
                    End If

                    DV = New Data.DataView(DSAcc.Tables(0), " foldername = '" & TxtFolder.Text & "'  ", String.Empty, DataViewRowState.CurrentRows)

                    If DV.Count > 0 Then
                        lblMsg.Text = "Folder Name already exists. Please try another Folder Name."
                        Return False
                    End If

                    DV = New Data.DataView(DSAcc.Tables(0), " BillActnumber = '" & TxtBillNumber.Text & "'  ", String.Empty, DataViewRowState.CurrentRows)

                    If DV.Count > 0 Then
                        lblMsg.Text = "Billing account number already exists."
                        Return False
                    End If
                End If
            End If

        Catch ex As Exception
            Response.Write(ex.Message)
        Finally
            clsAcc = Nothing
            DSAcc = Nothing
            DV = Nothing
        End Try
        Return True
    End Function
End Class
