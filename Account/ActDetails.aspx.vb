Imports System.Data
Imports System.Data.SqlClient
Imports System.IO

Partial Class Account_ActDetails
    Inherits BasePage
    Public ProcFolder As String = Server.MapPath("../ETS_Files")
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Label1.Visible = False
        hdnastatus.Value = ""

        If Not IsPostBack Then
          
            Dim MapDemoAccID As String = String.Empty
            Dim MapRefAccID As String = String.Empty
            AccID.Value = Request("AccountID").ToString
            Dim DSActCate As DataSet
            Dim clsAC As New ETS.BL.ActCategories
            With clsAC
                .ContractorID = Session("ContractorID").ToString
                DSActCate = .getActCateList()
            End With
            clsAC = Nothing
            DrpCategory.DataSource = DSActCate
            DrpCategory.DataTextField = "Description"
            DrpCategory.DataValueField = "Category"
            DrpCategory.DataBind()
            DSActCate.Dispose()
            Dim LI1 As New ListItem
            LI1.Text = "Not Set"
            LI1.Value = String.Empty
            DrpCategory.Items.Insert(0, LI1)
            LI1 = Nothing
            Dim DSMapAct As DataSet
            Dim clsAct As New ETS.BL.Accounts
            With clsAct
                .ContractorID = Session("ContractorID").ToString
                '._WhereString.Append(" and AccountID not in ('" & Request("AccountID").ToString & "') and (IsDeleted is null or IsDeleted=0)")
                ._WhereString.Append(" and AccountID not in ('" & Request("AccountID").ToString & "') ")
                DSMapAct = .getAccountList()
            End With
            Dim DR() As DataRow = DSMapAct.Tables(0).Select("DemoConfg=1")
            Dim DT As DataTable = DSMapAct.Tables(0).Clone
            For Each DR1 As DataRow In DR
                DT.ImportRow(DR1)
            Next
            clsAct = Nothing
            MapActID.DataSource = DT
            MapActID.DataTextField = "AccountName"
            MapActID.DataValueField = "AccountID"
            MapActID.DataBind()

            LI1 = New ListItem
            LI1.Text = "Not Set"
            LI1.Value = String.Empty
            MapActID.Items.Insert(0, LI1)
            LI1 = Nothing

            MapRActID.DataSource = DSMapAct
            MapRActID.DataTextField = "AccountName"
            MapRActID.DataValueField = "AccountID"
            MapRActID.DataBind()

            LI1 = New ListItem
            LI1.Text = "Not Set"
            LI1.Value = ""
            MapRActID.Items.Insert(0, LI1)
            LI1 = Nothing

            DSMapAct.Dispose()
            '***

            clsAct = New ETS.BL.Accounts
            With clsAct
                .AccountID = Request("AccountID").ToString
                .getAccountDetails()

                MapDemoAccID = .MapDemoAccID
                MapRefAccID = .MapRefAccID
                TxtActName.Text = .AccountName
                TxtDescr.Text = .Description
                TxtFacilityID.Text = .FacilityID
                LI1 = DrpCategory.Items.FindByValue(.Category)
                If Not LI1 Is Nothing Then
                    LI1.Selected = True
                End If
                LI1 = Nothing
                LI1 = DrpRSSType.Items.FindByValue(.RSSType)
                If Not LI1 Is Nothing Then
                    LI1.Selected = True
                End If
                LI1 = Nothing
                LI1 = DLMode.Items.FindByValue(.Mode.ToString.Trim)
                If Not LI1 Is Nothing Then
                    LI1.Selected = True
                End If
                LI1 = Nothing
                LI1 = DLTAT.Items.FindByValue(.TAT)
                If Not LI1 Is Nothing Then
                    LI1.Selected = True
                End If
                LI1 = Nothing
                LI1 = DLSTAT.Items.FindByValue(.STAT)
                If Not LI1 Is Nothing Then
                    LI1.Selected = True
                End If
                LI1 = Nothing
                LI1 = DDLTZ.Items.FindByValue(.TZDifference)
                If Not LI1 Is Nothing Then
                    LI1.Selected = True
                End If
                LI1 = Nothing
                If Not IsDBNull(.Time) Then
                    txtTime.Text = .Time
                Else
                    txtTime.Text = 0
                End If
                LI1 = DDLCntry.Items.FindByValue(.Country)
                If Not LI1 Is Nothing Then
                    LI1.Selected = True
                End If
                LI1 = Nothing
                LI1 = MapActID.Items.FindByValue(.MapDemoAccID)
                If Not LI1 Is Nothing Then
                    LI1.Selected = True
                End If
                LI1 = Nothing
                LI1 = MapRActID.Items.FindByValue(.MapRefAccID)
                If Not LI1 Is Nothing Then
                    LI1.Selected = True
                End If
                LI1 = Nothing
                'Response.Write(.IsDeleted)
                If .IsDeleted = True Then
                    DLStatus.Items(1).Selected = True
                Else
                    DLStatus.Items(0).Selected = True
                End If
                hdnastatus.Value = DLStatus.SelectedItem.ToString
                ' Response.Write("Interface:" & .FaxInterface.ToString.Trim())

                If .FaxInterface.ToString.Trim = "E" Then
                    DLFaxInterFace.Items(0).Selected = False
                    DLFaxInterFace.Items(1).Selected = True
                Else
                    DLFaxInterFace.Items(1).Selected = False
                    DLFaxInterFace.Items(0).Selected = True
                End If
                DLWebsite.Items.FindByValue(.WebSite.ToString.Trim).Selected = True
                'DLStatus.Items(.IsDeleted).Selected = True
                DLVoice.Items(IIf(.VFolder, 1, 0)).Selected = True
                DLIndirect.Items(IIf(.InDirect, 1, 0)).Selected = True
                DLEMR.Items(IIf(.IsEMRAccount, 1, 0)).Selected = True
                DLSignature.Items(IIf(.IsSignatureAccount, 1, 0)).Selected = True
                DLFaxPlus.Items(IIf(.FaxPlusService, 1, 0)).Selected = True
                DLFaxExcp.Items(IIf(.AutoFaxExcp, 1, 0)).Selected = True
                DDLStopDict.Items(IIf(.IsStopDictation, 1, 0)).Selected = True
                DLFMode.Enabled = .FaxPlusService
                DDLSDOXInterface.Items(IIf(.IsSDOXInterface, 1, 0)).Selected = True
                'Response.Write(.IsSDOXInterface)
                'txtOfficeID.Text = .OfficeID.ToString
                If DDLSDOXInterface.SelectedValue = "1" Then
                    txtOfficeID.Enabled = True
                    txtOfficeID.Text = .OfficeID
                    txtEffectiveDate.Text = .EffectiveDate
                Else
                    ' txtOfficeID.Enabled = False
                    If .OfficeID = 0 Then
                        txtOfficeID.Text = String.Empty
                    Else
                        txtOfficeID.Text = .OfficeID
                    End If

                    txtEffectiveDate.Text = .EffectiveDate
                End If
                If .FaxPlusMode.ToString = 0 Then
                    DLFMode.Items(0).Selected = True
                ElseIf .FaxPlusMode.ToString = 1 Then
                    DLFMode.Items(1).Selected = True
                ElseIf .FaxPlusMode.ToString = 2 Then
                    DLFMode.Items(2).Selected = True
                Else
                    DLFMode.Items(0).Selected = True

                End If

                If .IsInterface Then
                    DLInterface.Items(0).Selected = False
                    DLInterface.Items(1).Selected = True
                    DLHL7.Enabled = True
                    DLSigned.Enabled = True
                    If .IsHL7 Then
                        DLHL7.Items(0).Selected = False
                        DLHL7.Items(1).Selected = True
                    Else
                        DLHL7.Items(1).Selected = False
                        DLHL7.Items(0).Selected = True

                    End If
                    If .IsSigned Then
                        DLSigned.Items(0).Selected = False
                        DLSigned.Items(1).Selected = True
                    Else
                        DLSigned.Items(1).Selected = False
                        DLSigned.Items(0).Selected = True

                    End If
                Else
                    DLInterface.Items(1).Selected = False
                    DLInterface.Items(0).Selected = True
                    DLHL7.Items(1).Selected = False
                    DLHL7.Items(0).Selected = True
                    DLSigned.Items(1).Selected = False
                    DLSigned.Items(0).Selected = True
                    DLHL7.Enabled = False
                    DLSigned.Enabled = False
                End If

                If .IsArchEnabled Then
                    DLArchive.Items(1).Selected = True
                    DLArchive.Items(0).Selected = False
                Else
                    DLArchive.Items(1).Selected = False
                    DLArchive.Items(0).Selected = True
                End If
                If .HideTransReportsinUNO Then
                    DLHideUnSignedRep.Items(1).Selected = True
                    DLHideUnSignedRep.Items(0).Selected = False
                Else
                    DLHideUnSignedRep.Items(1).Selected = False
                    DLHideUnSignedRep.Items(0).Selected = True
                End If
                If .ShowReProcess Then
                    DLReProcess.Items(0).Selected = False
                    DLReProcess.Items(1).Selected = True
                Else
                    DLReProcess.Items(1).Selected = False
                    DLReProcess.Items(0).Selected = True
                End If
                TxtPMins.Text = .ProtocolMins
                TxtPCN.Text = .PriContact
                TxtPE.Text = .PriEmail
                TxtPriTitle.Text = .PriTitle
                TxtSecTitle.Text = .SecTitle
                TxtSCN.Text = .SecContact
                TxtSE.Text = .SecEmail
                txtPPh.Text = .PriPhoneNo
                txtPFax.Text = .PriFaxNo
                txtSPh.Text = .SecPhoneNo
                txtSFax.Text = .SecFaxNo
                TXTADD.Text = .Address
                TXTCity.Text = .City
                TXTState.Text = .State
                TXTZip.Text = .Zip
                TXTOS.Text = .OfficialSite
                TXTBCNM.Text = .BillContName
                TXTBCNO.Text = .BillContNO
                TXTBADD.Text = .BillAddress
                TXTBCity.Text = .BillCity
                TXTBState.Text = .BillState
                TXTBCntry.Text = .BillCntry
                TXTBZip.Text = .BillZip
                TXTBFax.Text = .BillFax
                TXTBEmail.Text = .BillEmail
                TXTBOS.Text = .BillOfficialSite
                TXTTCNM.Text = .TechContName
                TXTTCNO.Text = .TechContNO
                TXTTADD.Text = .TechAddress
                TXTTCity.Text = .TechCity
                TXTTCntry.Text = .TechCntry
                TXTTZip.Text = .TechZip
                TXTTFax.Text = .TechFax
                TXTTEMail.Text = .TechEmail
                TXTTOS.Text = .TechOfficialSite
                TxtFolder.Text = .foldername
                txtComments.Text = .comments
                TxtBillNumber.Text = .BillActnumber
                ddlMIS.SelectedValue = .MISRep
                txtEMR.Text = .EMR
                txtIntGroup.Text = .IntGroup
                'DLInstance.SelectedValue = .InstanceID
            End With
            GetAccountResetDay(Request("AccountID").ToString)
            clsAct = Nothing
        End If
    End Sub



    Protected Sub ImageButton1_Click1(ByVal sender As Object, ByVal e As System.EventArgs) Handles ImageButton1.Click
        Try
            If Not String.IsNullOrEmpty(TxtFacilityID.Text) Then
                Dim strConn As String

                strConn = System.Configuration.ConfigurationManager.AppSettings("ETSCon")
                Dim DT As New DataTable
                Dim cmdIns As New SqlCommand("select * from adminets.dbo.tblAccounts where FacilityID ='" & TxtFacilityID.Text & "' ", New SqlConnection(strConn))
                cmdIns.Connection.Open()
                Dim DRRec As SqlDataReader = cmdIns.ExecuteReader()
                DT.Load(DRRec)
                DRRec.Close()
                If cmdIns.Connection.State = ConnectionState.Open Then
                    cmdIns.Connection.Close()
                End If
                If DT.Rows.Count > 1 Then
                    Response.Write("Duplicate facility IDs.")

                    Exit Sub
                End If
            End If

        Catch ex As Exception
            Response.Write(ex.Message)

        End Try
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
        Dim clsAct As New ETS.BL.Accounts
        With clsAct

            .AccountID = Request("AccountID").ToString
            .FacilityID = TxtFacilityID.Text
            .PriContact = TxtPCN.Text
            .PriEmail = TxtPE.Text
            .PriPhoneNo = txtPPh.Text
            .PriFaxNo = txtPFax.Text
            .SecContact = TxtSCN.Text
            .SecEmail = TxtSE.Text
            .SecPhoneNo = txtSPh.Text
            .SecFaxNo = txtSFax.Text
            .Address = TXTADD.Text
            .City = TXTCity.Text
            .State = TXTState.Text
            .Country = DDLCntry.SelectedValue
            .Zip = TXTZip.Text
            .OfficialSite = TXTOS.Text
            .BillContName = TXTBCNM.Text
            .BillContNO = TXTBCNO.Text
            .BillAddress = TXTBADD.Text
            .BillCity = TXTBCity.Text
            .BillState = TXTBState.Text
            .BillCntry = TXTBCntry.Text
            .BillZip = TXTBZip.Text
            .BillFax = TXTBFax.Text
            .BillEmail = TXTBEmail.Text
            .BillOfficialSite = TXTBOS.Text
            .TechContName = TXTTCNM.Text
            .TechContNO = TXTTCNO.Text
            .TechAddress = TXTTADD.Text
            .TechCity = TXTTCity.Text
            .TechCntry = TXTTCntry.Text
            .TechZip = TXTTZip.Text
            .TechFax = TXTTFax.Text
            .TechEmail = TXTTEMail.Text
            .TechOfficialSite = TXTTOS.Text
            .IsDeleted = DLStatus.SelectedValue
            .InDirect = DLIndirect.SelectedValue
            .IsEMRAccount = DLEMR.SelectedValue
            .IsSignatureAccount = DLSignature.SelectedValue
            .IsStopDictation = DDLStopDict.SelectedValue
            .BillActnumber = TxtBillNumber.Text
            .foldername = TxtFolder.Text
            .comments = txtComments.Text
            .AccountName = TxtActName.Text
            .Description = TxtDescr.Text
            .PriTitle = TxtPriTitle.Text
            .SecTitle = TxtSecTitle.Text
            .FaxInterface = DLFaxInterFace.SelectedValue
            .EMR = txtEMR.Text
            .IntGroup = txtIntGroup.Text
            .WebSite = DLWebsite.SelectedValue
            .ShowReProcess = DLReProcess.SelectedValue
            .IsArchEnabled = DLArchive.SelectedValue
            .HideTransReportsinUNO = DLHideUnSignedRep.SelectedValue
            If DrpCategory.SelectedValue = "" Then
                .Category = Guid.NewGuid.ToString
            Else
                .Category = DrpCategory.SelectedValue
            End If

            'If Not String.IsNullOrEmpty(MapActID.SelectedValue) Then
            '    .MapDemoAccID = MapActID.SelectedValue
            'End If
            'If Not String.IsNullOrEmpty(MapRActID.SelectedValue) Then
            '    .MapRefAccID = MapRActID.SelectedValue
            'End If
            .IsSDOXInterface = DDLSDOXInterface.SelectedValue
            ' .OfficeID = txtOfficeID.Text
            If DDLSDOXInterface.SelectedValue = "1" Then
                If Not IsNumeric(txtOfficeID.Text) Then
                    Label1.Text = "Office ID should be numeric."
                    Label1.Visible = True
                End If
                .OfficeID = txtOfficeID.Text
                If Not IsDate(txtEffectiveDate.Text) Then
                    Label1.Text = "Effective Date should be valid date."
                    Label1.Visible = True
                End If
                If IsDate(txtEffectiveDate.Text) Then
                    Dim dt As Date = txtEffectiveDate.Text
                    .EffectiveDate = dt.ToString("MM/dd/yyyy")
                Else
                    .EffectiveDate = Now.ToString("MM/dd/yyyy")

                End If
            Else
                'Response.Write(txtOfficeID.Text)
                'Response.Write(IsNumeric(txtOfficeID.Text))

                If IsNumeric(txtOfficeID.Text) Then
                    .OfficeID = txtOfficeID.Text
                Else
                    .OfficeID = Nothing
                End If

                If IsDate(txtEffectiveDate.Text) Then
                    Dim dt As Date = txtEffectiveDate.Text
                    .EffectiveDate = dt.ToString("MM/dd/yyyy")
                Else
                    ' .EffectiveDate = Nothing

                End If
            End If
            If DLFaxPlus.SelectedValue = "1" Then
                .FaxPlusService = True
                .FaxPlusMode = DLFMode.SelectedValue
                'If DLFMode.SelectedValue = "1" Then
                '    .FaxPlusMode = True
                'Else
                '    .FaxPlusMode = False
                'End If
            Else
                .FaxPlusService = False
                .FaxPlusMode = 0
            End If
            If DLInterface.SelectedValue Then
                .IsInterface = DLInterface.SelectedValue
                .IsHL7 = DLHL7.SelectedValue
                .IsSigned = DLSigned.SelectedValue
            Else
                .IsInterface = 0
                .IsHL7 = 0
                .IsSigned = 0
            End If
            'Response.Write(.FaxPlusService & " " & .FaxPlusMode)

            .RSSType = DrpRSSType.SelectedValue
            If TxtPMins.Text = "" Then
                .ProtocolMins = 0
            Else
                .ProtocolMins = TxtPMins.Text
            End If
            .Mode = DLMode.SelectedValue

            .TAT = DLTAT.SelectedValue
            .VFolder = DLVoice.SelectedValue
            .STAT = DLSTAT.SelectedValue
            .TZDifference = DDLTZ.SelectedValue
            .Time = txtTime.Text
            .MISRep = ddlMIS.SelectedValue
            '.InstanceID = DLInstance.SelectedValue
            .AutoFaxExcp = DLFaxExcp.SelectedValue
            If String.IsNullOrEmpty(MapActID.SelectedValue) Then
                If String.IsNullOrEmpty(._UpdateString.ToString) Then
                    ._UpdateString.Append(" MapDemoAccID = NULL ")
                Else
                    ._UpdateString.Append(", MapDemoAccID = NULL ")
                End If
            Else
                .MapDemoAccID = MapActID.SelectedValue
            End If

            If String.IsNullOrEmpty(MapRActID.SelectedValue) Then
                If String.IsNullOrEmpty(._UpdateString.ToString) Then
                    ._UpdateString.Append(" MapRefAccID = NULL ")
                Else
                    ._UpdateString.Append(", MapRefAccID = NULL ")
                End If
            Else
                .MapRefAccID = MapRActID.SelectedValue
            End If

            'Response.Write("Update :" & .GetAccUpdateQuery)


            If .UpdateAccountDetails = 1 Then
                emailconfirmmail(TxtActName.Text)
                UpdatePassResetDay(Request("AccountID").ToString, DLPReset.SelectedValue.ToString)
                Label1.Text = "Account details have been updated successfully."
                Label1.Visible = True
            Else
                Label1.Text = "Failed updating Account details."
                Label1.Visible = True
            End If
        End With
        clsAct = Nothing
    End Sub

    Protected Sub DLFaxPlus_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles DLFaxPlus.SelectedIndexChanged
        If DLFaxPlus.SelectedValue = "1" Then
            DLFMode.Enabled = True
        Else
            DLFMode.Enabled = False
        End If
    End Sub

    Protected Sub DLInterface_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles DLInterface.SelectedIndexChanged
        If DLInterface.SelectedValue Then
            DLHL7.Enabled = True
            DLSigned.Enabled = True
        Else
            DLHL7.Enabled = False
            DLSigned.Enabled = False
        End If
    End Sub

    Protected Sub DDLSDOXInterface_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles DDLSDOXInterface.SelectedIndexChanged
        'If DDLSDOXInterface.SelectedValue = "1" Then
        '    txtOfficeID.Enabled = True
        'Else
        '    txtOfficeID.Enabled = False
        'End If
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
            If DLStatus.SelectedItem.Text = "Inactive" Then
                btext = "Following account has been made <b>Inactive</b>.<br><br>Account name - " & accname.ToString & "<br> <br>Thanks you."
            ElseIf DLStatus.SelectedItem.Text = "Active" Then
                btext = "Following account has been modified.<br><br>Account name - " & accname.ToString & "<br><br>Thanks you."
            End If
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
            MAILER.Subject = "Account update alert"
            MAILER.SendMail()
            MAILER = Nothing
        Catch ex As exception
            Response.Write(ex.message)
        End Try
    End Sub

    Private Sub UpdatePassResetDay(ByVal accountid As String, ByVal resetday As String)
        If resetday <> "0" Then
            Dim constr As String = ConfigurationManager.ConnectionStrings("ETSConnectionString").ConnectionString
            Using con As New SqlConnection(constr)
                Using cmd As New SqlCommand("Update tblaccounts set PassReset=@PassReset where AccountId=@AccID")
                    Using sda As New SqlDataAdapter()
                        cmd.CommandType = CommandType.Text
                        cmd.Parameters.AddWithValue("@PassReset", resetday)
                        cmd.Parameters.AddWithValue("@AccID", accountid)
                        cmd.Connection = con
                        con.Open()
                        cmd.ExecuteNonQuery()
                        con.Close()
                    End Using
                End Using
            End Using
        End If
    End Sub

    Private Sub GetAccountResetDay(ByVal accountid As String)
        Dim strcon As String = ConfigurationManager.ConnectionStrings("ETSConnectionString").ConnectionString
        Dim con As New SqlConnection(strcon)
        con.Open()
        Dim hcmd As New SqlCommand("Select PassReset from tblaccounts where AccountID='" & accountid & "'", con)
        Dim DR As SqlDataReader = hcmd.ExecuteReader()

        While DR.Read()
            'Response.Write("value" & DR.GetValue(0).ToString)
            If DR.GetValue(0).ToString = "" Then
                DLPReset.Items.Insert(0, New ListItem("--Select Days--", "0"))
            Else
                DLPReset.SelectedValue = DR.GetValue(0).ToString
            End If
        End While
    End Sub
End Class
