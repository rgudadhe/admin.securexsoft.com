Imports System.Data.SqlClient
Imports System.Data

Partial Class ChangePass
    Inherits BasePage
    Private clsUserInfo As ETS.BL.Users
    Protected Sub Button1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button1.Click
        'Try
        If TxtNPass.Text = TxtCPass.Text Then
            Dim EPass As New EncryPass.Encry
            Dim userpass As String
            Dim Newuserpass As String
            Dim ipaddress As String
            ipaddress = HttpContext.Current.Request.UserHostAddress
            Dim blSecurity As New ETS.BL.BALSecurity
            'Response.Write(clsUserInfo.UserName)
            userpass = EPass.encrypt(Session("LoginID").ToString.ToLower, TxtOPass.Text)

            clsUserInfo = CType(Session("clsUserInfo"), ETS.BL.Users)
            If clsUserInfo.Password = userpass Then
                Newuserpass = EPass.encrypt(Session("LoginID").ToString.ToLower, TxtNPass.Text)
                Dim clsu As New ETS.BL.Users
                clsu.UserID = Session("userID")
                clsu.Passchanged = Now
                clsu.Password = Newuserpass

                If clsu.UpdateUserDetails() = 1 Then
                    lblMsg.Text = "Password has been reset successfully."
                    'Response.Write("<script>self.close();</script>")
                Else
                    lblMsg.Text = "Failed changing password."
                    'Response.Write("<script>self.close();</script>")
                End If
            Else
                lblMsg.Text = "Old Password is not correct."
            End If

        Else
            lblMsg.Text = "Password does not match."
        End If
    End Sub

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        'Try
        '    Dim constr As String = ConfigurationManager.ConnectionStrings("ETSConnectionString").ConnectionString
        '    Dim activationCode As String = Guid.NewGuid().ToString()
        '    Using con As New SqlConnection(constr)
        '        Using cmd As New SqlCommand("INSERT INTO UserActivation VALUES(@UserId, @ActivationCode)")
        '            Using sda As New SqlDataAdapter()
        '                cmd.CommandType = CommandType.Text
        '                cmd.Parameters.AddWithValue("@UserId", Session("UserID").ToString)
        '                cmd.Parameters.AddWithValue("@ActivationCode", activationCode)
        '                cmd.Connection = con
        '                con.Open()
        '                cmd.ExecuteNonQuery()
        '                con.Close()
        '            End Using
        '        End Using
        '    End Using
        '    Response.Redirect("authorization/UserRegistration.aspx?ActivationCode=" & activationCode + "&uid=" & Session("UserID").ToString + "&vdate=" & FormatDateTime(Now, DateFormat.ShortDate).ToString + "&isInsight=1")
        'Catch ex As Exception
        '    Response.Write(ex.Message)
        'End Try
        'Response.Write(clsUserInfo.UserName)
    End Sub
End Class
