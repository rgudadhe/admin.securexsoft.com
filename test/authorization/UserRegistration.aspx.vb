Imports System.Data
Imports System.Configuration
Imports System.Data.SqlClient
Imports EncryPass
Imports System.IO
Imports System.Security.Cryptography
Partial Class email
    Inherits System.Web.UI.Page
    Private ipaddress As String
    Private constr As String = ConfigurationManager.ConnectionStrings("ETSConnectionString").ConnectionString
    
    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        ipaddress = HttpContext.Current.Request.UserHostAddress
        Dim vdate As Date = Request.QueryString("vdate")
        Dim hr As Integer
        hr = (Now - vdate).Minutes
        Response.Write(hr)
        If (hr > 60) Then
            'Response.Write("inside")
            Dim blSecurity As New ETS.BL.BALSecurity
            blSecurity.UserActivationTempDelete(Request.QueryString("uid"), Request.QueryString("ActivationCode"))
            Response.Redirect("message.aspx")
            Exit Sub
        End If

        If Not Me.IsPostBack Then

            DropDownList3.Visible = False
            txtans3.Visible = False
            DropDownList2.Visible = False
            txtans2.Visible = False

            Dim username As String
            Dim useremail As String
            Dim uid As String = Request.QueryString("uid")
            Dim msg As String
            Dim activationCode As String = Request.QueryString("ActivationCode")

            Dim blSecurity As New ETS.BL.BALSecurity
            If blSecurity.UserActivationTempDelete(uid.ToString, activationCode) > 0 Then
                'Label1.ForeColor = Drawing.Color.Green
                'Label1.Text = "Your email-id verified successfully!"
            Else
                form1.Visible = False
                Response.Redirect("message.aspx")
            End If

            Dim blUsers As New ETS.BL.Users
            Dim ds As DataSet = blUsers.getUserDetails(uid.ToString)
            username = ds.Tables(0).Rows(0)(2).ToString
            useremail = ds.Tables(0).Rows(0)(7).ToString
            txtfname.Text = ds.Tables(0).Rows(0)(3).ToString
            txtlname.Text = ds.Tables(0).Rows(0)(5).ToString
            txtemail.Text = useremail.ToString
            txtuname.Text = username.ToString

            Dim dlSecurity As New ETS.DAL.DALSecurity
            Dim dt As DataTable = blSecurity.getQuestionBank
            'Response.Write(dt.Rows.Count)
            DropDownList1.DataSource = dt
            DropDownList1.DataTextField = "question"
            DropDownList1.DataValueField = "question"
            DropDownList1.DataBind()


            DropDownList1.Items.Insert(0, "Select Question 1")

        End If
        If IsPostBack Then
            If Not String.IsNullOrEmpty(TxtNewPass.Text.Trim()) Then
                TxtNewPass.Attributes.Add("value", TxtNewPass.Text)
            End If
            If Not String.IsNullOrEmpty(TxtCNewPass.Text.Trim()) Then
                TxtCNewPass.Attributes.Add("value", TxtCNewPass.Text)
            End If

        End If

    End Sub

    Private Sub emailconfirmmail(ByVal Fname As String, ByVal emailid As String)
        Dim uid As String = Request.QueryString("uid")
        Dim MAILER As New SASMTPLib.CoSMTPMail
        Dim Ufname As String = String.Empty
        Dim Uemail As String = String.Empty

        'Dim strcon As String = ConfigurationManager.ConnectionStrings("ETSConnectionString").ConnectionString
        'Dim con As New SqlConnection(strcon)
        'con.Open()
        'Dim cmd As New SqlCommand("select * from tblusers where userid='" & uid & "'", con)
        'Dim dr As SqlDataReader
        'dr = cmd.ExecuteReader()
        'dr.Read()
        'If dr.HasRows Then
        '    Ufname = dr.Item("First").ToString
        '    Uemail = dr.Item("emailaddress").ToString
        '    con.Close()
        'End If
        Ufname = Fname
        Uemail = emailid

        Dim reader As New StreamReader(Server.MapPath("~/authorization/confirmation.htm"))
        Dim readFile As String = reader.ReadToEnd()
        Dim myString As String = ""
        myString = readFile
        myString = myString.Replace("$$UNAME$$", Ufname)
        myString = myString.Replace("$$BODY$$", "You've successfully verified your Secure-Scribe registered email-id. Your secret question answers and password has also been updated successfully!. Please click <a href=""https://secureweb1.securexsoft.com"">here</a> to login.<br /><br /> If you have not updated this, please <a href=""""mailto:support@medofficepro.com"""">contact support</a>.")

        Dim body As String = "Hi " & Ufname & ","
        MAILER.FromName = "E-Dictate User Support"
        MAILER.FromAddress = "hr@edictate.com"
        MAILER.RemoteHost = "secure.emailsrvr.com"
        MAILER.UserName = "alert@edictate.com"
        MAILER.Password = "Welcome@medofficepro2011"
        MAILER.AddRecipient(Uemail)
        'MAILER.AddRecipient("vraut@edictate.com")
        MAILER.Priority = 1
        MAILER.Urgent = True
        'body += "<br /><br />You've successfully verified your Secure-Scribe registered email-id."
        'body += "<br /><br />Thanks,"
        'body += "<br /><br />Customer Support Team"
        MAILER.HtmlText = myString
        MAILER.Subject = Ufname & ", your email address successfully verified"
        MAILER.SendMail()
        MAILER = Nothing
    End Sub

    Protected Sub btn1_Click(sender As Object, e As System.EventArgs) Handles btn1.Click
        Try
            Dim message As String
            Dim uid As String = Request.QueryString("uid")
            Dim s1 As String = txtans1.Text
            Dim s2 As String = txtans2.Text
            Dim s3 As String = txtans3.Text

            If DropDownList1.Text = "Select Question 1" Then
                lblq1.ForeColor = Drawing.Color.Red
                lblq1.Text = "Please select question."
                Exit Sub
            ElseIf DropDownList2.Text = "Select Question 2" Then
                lblq2.ForeColor = Drawing.Color.Red
                lblq2.Text = "Please select question."
                Exit Sub
            ElseIf DropDownList3.Text = "Select Question 3" Then
                lblq3.ForeColor = Drawing.Color.Red
                lblq3.Text = "Please select question."
                Exit Sub
            End If

            Dim blUsers As New ETS.BL.Users
            Dim ds As DataSet = blUsers.getUserDetails(uid.ToString)
            Dim username As String = ds.Tables(0).Rows(0)(2).ToString
            Dim Fname As String = ds.Tables(0).Rows(0)(3).ToString
            Dim emailid As String = ds.Tables(0).Rows(0)(7).ToString

            Dim flg As Integer = 0
            Dim EPass As New EncryPass.Encry
            Dim Newuserpass As String


            Newuserpass = EPass.encrypt(username.ToLower, TxtNewPass.Text)

            Dim clsu As New ETS.BL.Users
            clsu.UserID = uid
            clsu.Passchanged = Now
            clsu.Password = Newuserpass
            If clsu.UpdateUserDetails() = 1 Then
                'Label2.Text = "Password has been reset successfully."
                'Response.Write("<script>self.close();</script>")
                flg = 1
            Else
                'Label2.Text = "Failed changing password."
                'Response.Write("<script>self.close();</script>")
            End If

            Dim blsecurity As New ETS.BL.BALSecurity
            blsecurity.InsertUserSecretQuestion(uid.ToString, DropDownList1.Text, s1, DropDownList2.Text, s2, DropDownList3.Text, s3)

            'Dim constr As String = ConfigurationManager.ConnectionStrings("ETSConnectionString").ConnectionString
            'Using con2 As New SqlConnection(constr)
            '    Using cmd2 As New SqlCommand("INSERT INTO tblusersecretq VALUES(@uname, @q1,@s1,@q2,@s2,@q3,@s3)")
            '        Using sda As New SqlDataAdapter()
            '            cmd2.CommandType = CommandType.Text
            '            cmd2.Parameters.AddWithValue("@uname", uid)
            '            cmd2.Parameters.AddWithValue("@q1", DropDownList1.Text)
            '            cmd2.Parameters.AddWithValue("@s1", s1)
            '            cmd2.Parameters.AddWithValue("@q2", DropDownList2.Text)
            '            cmd2.Parameters.AddWithValue("@s2", s2)
            '            cmd2.Parameters.AddWithValue("@q3", DropDownList3.Text)
            '            cmd2.Parameters.AddWithValue("@s3", s3)
            '            cmd2.Connection = con2
            '            con2.Open()
            '            cmd2.ExecuteNonQuery()
            '            con2.Close()
            '        End Using
            '    End Using
            'End Using
            If flg = 1 Then
                Dim logflg As Integer = 0
                Dim dlsecurity As New ETS.DAL.DALSecurity
                Dim action As String = "User Registered Successfully"

                '   dlsecurity.updateUserLog1(Now().ToString("MM/dd/yyyy"), txtuname.Text, action, uid.ToString, logflg)

                blsecurity.UpdateUserLog(Now(), txtuname.Text, action, uid.ToString, logflg, ipaddress)

                blsecurity.InsertUserPassword(uid.ToString, Now().ToString("MM/dd/yyyy"), base64Encode(TxtNewPass.Text.Trim()))

                'Using con As New SqlConnection(constr)
                '    Using cmd1 As New SqlCommand("INSERT INTO tbluserpasschange VALUES(@userid, @date,@password)")
                '        Using sda1 As New SqlDataAdapter()
                '            cmd1.CommandType = CommandType.Text
                '            cmd1.Parameters.AddWithValue("@date", Now())
                '            cmd1.Parameters.AddWithValue("@userid", uid)
                '            cmd1.Parameters.AddWithValue("@password", base64Encode(TxtNewPass.Text.Trim()))
                '            cmd1.Connection = con
                '            con.Open()
                '            cmd1.ExecuteNonQuery()
                '            con.Close()
                '        End Using
                '    End Using
                'End Using


                'emailconfirmmail(Fname, emailid)
                Response.Redirect("regconfirmation.aspx")
                'Label2.Text = "Congratulations! Your password and Secret answers are updated successfully!"
                'ScriptManager.RegisterStartupScript(Me, [GetType](), "showalert", "alert('Password and Secret answers updated successfully!');", True)
                btn1.Enabled = False
                txtans1.Text = ""
                txtans2.Text = ""
                txtans3.Text = ""
                TxtNewPass.Text = ""
                TxtCNewPass.Text = ""

            End If

        Catch ex As Exception
            Response.Write(ex.Message)
        End Try

    End Sub
    

    Protected Sub DropDownList1_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles DropDownList1.SelectedIndexChanged
        DropDownList2.Visible = True
        txtans2.Visible = True
        Dim q1 As String
        q1 = DropDownList1.Text
        Dim blsecurity As New ETS.BL.BALSecurity
        Dim dt As DataTable = blsecurity.getQuestionBankForOtherDD(q1)
        'Dim con1 As New SqlConnection(constr)
        'con1.Open()
        'Dim cmd As New SqlCommand("Select * from tblqbank1 where question <>" & "'" & q1.ToString & "'", con1)
        'Dim da As New SqlDataAdapter(cmd)
        'Dim ds As New DataSet()
        'da.Fill(ds)
        DropDownList2.DataSource = dt
        DropDownList2.DataTextField = "question"
        DropDownList2.DataValueField = "question"
        DropDownList2.DataBind()
        'con1.Close()
        DropDownList2.Items.Insert(0, "Select Question 2")
        txtans2.Focus()
    End Sub
    

    
    Protected Sub DropDownList2_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles DropDownList2.SelectedIndexChanged
        DropDownList3.Visible = True
        txtans3.Visible = True
        Dim q1 As String
        Dim q2 As String
        q1 = DropDownList1.Text
        q2 = DropDownList2.Text
        Dim blsecurity As New ETS.BL.BALSecurity
        Dim dt As DataTable = blsecurity.getQuestionBankForOtherDD2(q1, q2)
        'Dim con1 As New SqlConnection(constr)
        'con1.Open()
        'Dim cmd As New SqlCommand("Select * from tblqbank1 where question <>" & "'" & q1.ToString & "'" & " and question <> " & "'" & q2.ToString & "'", con1)
        'Dim da As New SqlDataAdapter(cmd)
        'Dim ds As New DataSet()
        'da.Fill(ds)
        DropDownList3.DataSource = dt
        DropDownList3.DataTextField = "question"
        DropDownList3.DataValueField = "question"
        DropDownList3.DataBind()
        DropDownList3.Items.Insert(0, "Select Question 3")
    End Sub

    
    Private Function base64Encode(ByVal sData As String) As String
        Try
            Dim encData_byte As Byte() = New Byte(sData.Length - 1) {}
            encData_byte = System.Text.Encoding.UTF8.GetBytes(sData)
            Dim encodedData As String = Convert.ToBase64String(encData_byte)
            Return (encodedData)
        Catch ex As Exception
            Throw (New Exception("Error in base64Encode" & ex.Message))
        End Try
    End Function

    
End Class
