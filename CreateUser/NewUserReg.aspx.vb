Imports System.Data
Imports System.Data.SqlClient
Imports System.Data.SqlDbType
Imports System.IO
Imports System.Net.Mail
Imports System.Net




Partial Class NewUserReg
    Inherits BasePage


    Public strConn As String = System.Configuration.ConfigurationManager.AppSettings("ETSCon")
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        tblResult.Visible = False
        If Not IsPostBack Then
            Dim LI As New ListItem
            Dim clsUC As New ETS.BL.UserCategories
            With clsUC
                .ContractorID = Session("ContractorID")
                Dim DSCate As DataSet = .getCategoryList
                txtCate.DataSource = DSCate
                txtCate.DataTextField = "Name"
                txtCate.DataValueField = "CategoryID"
                txtCate.DataBind()
                DSCate.Dispose()
                LI = New ListItem
                LI.Text = "Please Select"
                LI.Value = ""
                txtCate.Items.Insert(0, LI)
                LI = Nothing
                ' Response.Write("checked")
                If ChkUn.Checked Then
                    LblUN.Visible = False
                    TxtUname.Visible = False
                    ' Response.Write("checked")
                Else
                    LblUN.Visible = True
                    TxtUname.Visible = True
                    'Response.Write(" Not checked")
                End If

            End With
            clsUC = Nothing

            Dim clsDep As New ETS.BL.Department
            With clsDep
                .ContractorID = Session("ContractorID")
                ._WhereString.Append(" and (deleted is NULL or deleted = 'False')")
                Dim DSDep As DataSet = .getDepartmentList()
                TxtDept.DataSource = DSDep
                TxtDept.DataTextField = "Name"
                TxtDept.DataValueField = "DepartmentID"
                TxtDept.DataBind()
                DSDep.Dispose()
                LI = New ListItem
                LI.Text = "Please Select"
                LI.Value = ""
                TxtDept.Items.Insert(0, LI)
                LI = Nothing
            End With
            clsDep = Nothing
            Dim clsM As ETS.BL.Mentors
            Dim Ds As New Data.DataSet
            Try
                clsM = New ETS.BL.Mentors
                Ds = clsM.getMentorsListByWrkGrpID(Session("ContractorID").ToString, Session("WorkGroupID").ToString)
                If Ds.Tables.Count > 0 Then
                    If Ds.Tables(0).Rows.Count > 0 Then
                        DLmentor.DataSource = Ds
                        DLmentor.DataTextField = "Mentor"
                        DLmentor.DataValueField = "userid"
                        DLmentor.DataBind()
                    End If
                End If
            Catch ex As Exception
            Finally
                clsM = Nothing
                Ds = Nothing
            End Try
            Dim LI1 As New ListItem
            LI1.Text = "Not Set"
            LI1.Value = ""
            DLmentor.Items.Insert(0, LI1)
            LI1 = Nothing

            'Dim clsUsers As New ETS.BL.Users
            'With clsUsers
            '    .ContractorID = Session("ContractorID")
            '    .IsMentor = True
            '    Dim DSUsers As DataSet = .getUsersList()
            '    DSUsers.Tables(0).Columns.Add("UName", GetType(System.String), "Firstname + ' ' + Lastname")
            '    DLmentor.DataSource = DSUsers
            '    DLmentor.DataTextField = "UName"
            '    DLmentor.DataValueField = "userid"
            '    DLmentor.DataBind()
            '    DSUsers.Dispose()
            '    Dim LI1 As New ListItem
            '    LI1.Text = "Not Set"
            '    LI1.Value = ""
            '    DLmentor.Items.Insert(0, LI1)
            '    LI1 = Nothing
            'End With
            'clsUsers = Nothing
        End If
    End Sub
    Protected Function IsUsrExist(ByVal UName) As Boolean
        Dim clsUsr As ETS.BL.Users
        Dim DSExistUser As New Data.DataSet
        Dim varReturn As Boolean = False
        Try
            clsUsr = New ETS.BL.Users
            'clsUsr.ContractorID = IIf(Session("IsContractor") = 0, Session("ParentID").ToString, Session("ContractorID").ToString)
            clsUsr.UserName = UName.ToString
            DSExistUser = clsUsr.getUsersList
            If DSExistUser.Tables.Count > 0 Then
                If DSExistUser.Tables(0).Rows.Count > 0 Then
                    varReturn = True
                End If
            End If

        Catch ex As Exception
        Finally
            DSExistUser.Dispose()
            clsUsr = Nothing
        End Try
        Return varReturn
    End Function
    Protected Function IsUsrEmailExist(ByVal emailID) As Boolean
        Dim clsUsr As ETS.BL.Users
        Dim DSExistUser As New Data.DataSet
        Dim varReturn As Boolean = False
        Try
            clsUsr = New ETS.BL.Users
            'clsUsr.ContractorID = IIf(Session("IsContractor") = 0, Session("ParentID").ToString, Session("ContractorID").ToString)
            clsUsr.OfficialMailID = emailID.ToString
            DSExistUser = clsUsr.getUsersList
            If DSExistUser.Tables.Count > 0 Then
                If DSExistUser.Tables(0).Rows.Count > 0 Then
                    varReturn = True
                End If
            End If

        Catch ex As exception
        Finally
            DSExistUser.Dispose()
            clsUsr = Nothing
        End Try
        Return varReturn
    End Function
    Private Sub SendActivationEmail(ByVal userId As String, ByVal username As String)

        Dim body As String = "Hello " + TxtFirstName.Text + ","
        Dim MAILER As New SASMTPLib.CoSMTPMail

        Dim constr As String = ConfigurationManager.ConnectionStrings("ETSConnectionString").ConnectionString
        Dim activationCode As String = Guid.NewGuid().ToString()
        Using con As New SqlConnection(constr)
            Using cmd As New SqlCommand("INSERT INTO UserActivation VALUES(@UserId, @ActivationCode)")
                Using sda As New SqlDataAdapter()
                    cmd.CommandType = CommandType.Text
                    cmd.Parameters.AddWithValue("@UserId", userId)
                    cmd.Parameters.AddWithValue("@ActivationCode", activationCode)
                    cmd.Connection = con
                    con.Open()
                    cmd.ExecuteNonQuery()
                    con.Close()
                End Using
            End Using
        End Using


        Dim uname As String = lblUsername.Text
        Dim umsg As String = "Your account has been created successfully!"
        umsg += "<br /><br />Your user name is :" & username

        'Dim reader As New StreamReader(Server.MapPath("~/authorization/emailverification.htm"))
        'Dim readFile As String = reader.ReadToEnd()
        'Dim myString As String = ""
        'myString = readFile
        'myString = myString.Replace("$$UNAME$$", TxtFirstName.Text)
        'myString = myString.Replace("$$BODY$$", umsg.ToString & " <br /><br />Please click following link to verify you email address, update password and secret question answers.")
        'myString = myString.Replace("$$BTEXT$$", "Verify Email")
        'myString = myString.Replace("$$EVERIFY$$", "https://admin.securexsoft.com/authorization/UserRegistration.aspx?ActivationCode=" & activationCode + "&uid=" & userId + "&vdate=" & FormatDateTime(Now, DateFormat.ShortDate).ToString)
        'myString = myString.Replace("$$BODY2$$", "If you did not request this registration, or if you need additional assitance, please contact technical help desk.")


        'MAILER.FromName = "Technical Help Desk"
        'MAILER.FromAddress = "techsupport@edictate.com"
        'MAILER.RemoteHost = "secure.emailsrvr.com"
        'MAILER.UserName = "alert@edictate.com"
        '' MAILER.Password = "Welcome@medofficepro2011"
        'MAILER.Password = "Welcome@medofficepro2011"
        'MAILER.AddRecipient(TxtEmail.Text)

        'MAILER.Priority = 1
        'MAILER.Urgent = True

        'MAILER.HtmlText = myString.ToString()
        'MAILER.Subject = "Your account created successfully"
        'MAILER.SendMail()
        'MAILER = Nothing

        Dim message As New MailMessage()
        Dim fromName As String = "Do Not Reply"
        Dim from As String = "donotreply@medofficepro.com"
        Dim toAddress As String = TxtEmail.Text
        'Dim bccaddress As String = "sdoxreg@edictate.com"
        Dim smtpadd As String = "email-smtp.us-west-2.amazonaws.com"
        Dim smtpuname As String = "AKIA44IE6PBA24MEZW5P"
        Dim smtppass As String = "BLZJ9U1M6AILVx4FRA8E1CdRvOoRV9rx7/HBXEcNaeJ6"
        Dim port As Integer = 587
        Dim subject As String = "Your account created successfully"
        Dim configset As String = "ConfigSet"

        message.IsBodyHtml = True
        message.From = New MailAddress(from, fromName)
        message.To.Add(New MailAddress(toAddress))
        message.Subject = "Your account created successfully"


        Dim reader As New StreamReader(Server.MapPath("~/authorization/emailverification.htm"))
        Dim readFile As String = reader.ReadToEnd()
        Dim myString As String = ""
        myString = readFile
        myString = myString.Replace("$$UNAME$$", TxtFirstName.Text)
        myString = myString.Replace("$$BODY$$", umsg.ToString & " <br /><br />Please click following link to verify you email address, update password and secret question answers.")
        myString = myString.Replace("$$BTEXT$$", "Verify Email")
        myString = myString.Replace("$$EVERIFY$$", "https://admin.securexsoft.com/authorization/UserRegistration.aspx?ActivationCode=" & activationCode + "&uid=" & userId + "&vdate=" & FormatDateTime(Now, DateFormat.ShortDate).ToString)
        myString = myString.Replace("$$BODY2$$", "If you did not request this registration, or if you need additional assitance, please contact technical help desk.")

        message.IsBodyHtml = True
        message.Body = myString.ToString
        message.From = New MailAddress(from, fromName)
        message.To.Add(New MailAddress(toAddress))

        message.Subject = subject
        'message.Headers.Add("X-SES-CONFIGURATION-SET", configset)
        Dim client As New System.Net.Mail.SmtpClient(smtpadd, port)

        client.Credentials = New NetworkCredential(smtpuname, smtppass)
        client.EnableSsl = True
        client.Send(message)


    End Sub
    Protected Sub Button4_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button4.Click
        Dim clsUsers As ETS.BL.Users
        Dim DSExistUser As New Data.DataSet
        Dim varRecExist As Boolean = False
        Try
            clsUsers = New ETS.BL.Users

            'If IsUsrExist(Trim(TxtUname.Text)) = True Then
            '    Label1.Text = "Usersname already exists. Please try another username."
            '    Exit Sub
            'Else
            Dim uName As String = String.Empty


            Dim stream As IO.Stream
            Dim EPass As New EncryPass.Encry
            Dim userpass As String

            If ChkUn.Checked = True Then
                Dim Prefix As String = String.Empty
                Dim EmpNo As String = String.Empty
                If Not String.IsNullOrEmpty(txtCate.SelectedValue.ToString) Then
                    Prefix = GetPrefix(txtCate.SelectedValue.ToString)
                    EmpNo = CStr(GetEmpNo())
                    Response.Write(EmpNo)
                    uName = Prefix.Trim & EmpNo
                End If
            Else
                If Not String.IsNullOrEmpty(TxtUname.Text.ToString) Then
                    uName = TxtUname.Text.ToString
                    Response.Write(uName)
                End If
            End If

            If IsUsrExist(Trim(uName)) = True Then
                Label1.Text = "Usersname already exists. Please try another username."
                Exit Sub
            End If
            If IsUsrEmailExist(Trim(Request.Form("TxtEmail"))) = True Then
                Label1.Text = "Email ID is already in use. Please try another."
                Exit Sub
            End If
            userpass = EPass.encrypt(uName.ToLower, "sxfwelcome123")

            With clsUsers
                Dim var_UserID As String = String.Empty
                .ContractorID = IIf(Session("IsContractor") = 0, Session("ParentID").ToString, Session("ContractorID").ToString)
                .UserName = uName
                .Password = userpass
                var_UserID = Guid.NewGuid.ToString
                .UserID = var_UserID

                stream = FileUpload1.PostedFile.InputStream

                Dim uploadedFile(stream.Length) As Byte
                If FileUpload1.HasFile Then
                    If UCase(Right(FileUpload1.FileName, 3)) = "JPG" Or UCase(Right(FileUpload1.FileName, 3)) = "GIF" Then
                        stream.Read(uploadedFile, 0, stream.Length)
                    End If
                End If
                If Not String.IsNullOrEmpty(Request.Form("TxtFirstName")) Then
                    .FirstName = Request.Form("TxtFirstName")
                End If

                If Not String.IsNullOrEmpty(Request.Form("TxtLastName")) Then
                    .LastName = Request.Form("TxtLastName")
                End If

                If Not String.IsNullOrEmpty(Request.Form("TxtEmail")) Then
                    .OfficialMailID = Request.Form("TxtEmail")
                End If

                If Not String.IsNullOrEmpty(Request.Form("TxtNonOEmail")) Then
                    .OtherMailID = Request.Form("TxtNonOEmail")
                End If

                If Not String.IsNullOrEmpty(Request.Form("TxtChatID")) Then
                    .ChatID = Request.Form("TxtChatID")
                End If

                If Not String.IsNullOrEmpty(Request.Form("TxtAdd")) Then
                    .Address = Request.Form("TxtAdd")
                End If

                If Not String.IsNullOrEmpty(Request.Form("TxtCity")) Then
                    .City = Request.Form("TxtCity")
                End If

                If Not String.IsNullOrEmpty(Request.Form("TxtState")) Then
                    .State = Request.Form("TxtState")
                End If

                If Not String.IsNullOrEmpty(Request.Form("txtCountry")) Then
                    .Country = Request.Form("txtCountry")
                End If

                If Not String.IsNullOrEmpty(Request.Form("TxtCell")) Then
                    .CellNo = Request.Form("TxtCell")
                End If

                If Not String.IsNullOrEmpty(Request.Form("TxtTel")) Then
                    .PhoneNo = Request.Form("TxtTel")
                End If

                If Not String.IsNullOrEmpty(Request.Form("TxtDept")) Then
                    .DepartmentID = Request.Form("TxtDept")
                End If

                If Not String.IsNullOrEmpty(Request.Form("TxtDesi")) Then
                    .DesignationID = Request.Form("TxtDesi")
                End If

                If Not String.IsNullOrEmpty(Request.Form("txtCate")) Then
                    .CategoryID = Request.Form("txtCate")
                End If

                If Not String.IsNullOrEmpty(Request.Form("TxtpAdd")) Then
                    .PAddress = Request.Form("TxtpAdd")
                End If

                If Not String.IsNullOrEmpty(Request.Form("TxtpCity")) Then
                    .PCity = Request.Form("TxtpCity")
                End If

                If Not String.IsNullOrEmpty(Request.Form("TxtpState")) Then
                    .PState = Request.Form("TxtpState")
                End If

                If Not String.IsNullOrEmpty(Request.Form("txtpCountry")) Then
                    .PCountry = Request.Form("txtpCountry")
                End If

                'If Not String.IsNullOrEmpty(Request.Form("TRGLines")) Then
                '    .TRGLines = CInt(Request.Form("TRGLines"))
                'Else
                '    .TRGLines = 0
                'End If

                If Not String.IsNullOrEmpty(Request.Form("TxtDOB")) Then
                    .DateOfBirth = Request.Form("TxtDOB")
                End If

                If Not String.IsNullOrEmpty(Request.Form("TxtJoin")) Then
                    .DateJoined = Request.Form("TxtJoin")
                End If

                If Not String.IsNullOrEmpty(Request.Form("DLMentor")) Then
                    .MentorID = Request.Form("DLMentor")
                End If

                'If Not String.IsNullOrEmpty(Request.Form("DLPlatform")) Then
                '    .plataccid = Request.Form("DLPlatform")
                'End If
                .IsDeleted = False



                If .InsertUserDetails() = 1 Then
                    SendActivationEmail(var_UserID, uName)
                    tblResult.Visible = True
                    Label1.Text = "User has been created successfully. An activation email has been sent to user registered email id."
                    lblName.Text = TxtFirstName.Text & " " & TxtLastName.Text
                    lblUsername.Text = uName
                    lblPass.Text = "-----------"
                    PnlUser.Visible = False
                    If stream.Length > 0 Then
                        .UpdateImag(uploadedFile, var_UserID)
                    End If
                End If
            End With
            'End If
        Catch ex As Exception
            Response.Write(ex.Message)
        Finally
            clsUsers = Nothing
        End Try
    End Sub
    Protected Sub ChkUn_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ChkUn.CheckedChanged
        ' Response.Write("checked")
        If ChkUn.Checked Then
            LblUN.Visible = False
            TxtUname.Visible = False
            ' Response.Write("checked")
        Else
            LblUN.Visible = True
            TxtUname.Visible = True
            'Response.Write(" Not checked")
        End If
    End Sub
    Protected Function GetEmpNo() As Long
        Dim clsUsers As ETS.BL.Users
        Dim DSUsers As New Data.DataSet
        Dim varReturn As Long = 0
        Try
            clsUsers = New ETS.BL.Users
            clsUsers.ContractorID = Session("ContractorID")
            DSUsers = clsUsers.getUsersList
            If DSUsers.Tables.Count > 0 Then
                If DSUsers.Tables(0).Rows.Count > 0 Then
                    varReturn = DSUsers.Tables(0).Compute("max(empno)", String.Empty)
                End If
            End If
        Catch ex As Exception
            Response.Write(ex.Message)
        Finally
            clsUsers = Nothing
            DSUsers = Nothing
        End Try
        Return varReturn + 1
    End Function
    Protected Function GetPrefix(ByVal CategoryID As String) As String
        Dim varReturn As String = String.Empty
        Dim clsCate As ETS.BL.UserCategories
        Try
            clsCate = New ETS.BL.UserCategories(CategoryID)
            varReturn = clsCate.Prefix
        Catch ex As Exception
        Finally
            clsCate = Nothing
        End Try
        Return varReturn
    End Function
    Protected Sub TxtDept_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles TxtDept.SelectedIndexChanged
        TxtDesi.Items.Clear()
        If TxtDept.Items(0).Value = "" Then
            TxtDept.Items.RemoveAt(0)
        End If

        Dim clsUD As ETS.BL.Designations
        Try
            clsUD = New ETS.BL.Designations

            With clsUD
                .Deleted = False
                .DepartmentID = TxtDept.SelectedValue
                Dim DSDes As DataSet = .getDesignationList
                TxtDesi.DataSource = DSDes
                TxtDesi.DataTextField = "Name"
                TxtDesi.DataValueField = "DesignationID"
                TxtDesi.DataBind()
                DSDes.Dispose()
                TxtDesi.Items.Insert(0, New ListItem("Please Select", String.Empty))
            End With
        Catch ex As Exception
        Finally
            clsUD = Nothing
        End Try
    End Sub

    Protected Sub lnkAddAnother_Click(sender As Object, e As System.EventArgs) Handles lnkAddAnother.Click
        Response.Redirect("NewUserReg.aspx")
    End Sub
End Class
