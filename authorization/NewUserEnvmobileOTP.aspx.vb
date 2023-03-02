Imports System.Data
Imports System.Net.Http
Imports System.IO
Imports System.Data.SqlClient
Partial Class authorization_mobileOTP
    Inherits System.Web.UI.Page
    Private weburl As String = ConfigurationManager.AppSettings("Webaddress")

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        Dim emailid As String = Request.QueryString("emailid")
        Dim uid As String = Request.QueryString("uid")
        emailid = base64Decode(emailid)
        username.Text = emailid.ToString

    End Sub

    Public Function GenerateOTP() As Integer
        Dim otp As Integer
        Dim rnd As New Random()
        otp = rnd.Next(1111, 10000)
        Return otp
    End Function
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

    Protected Sub btnSubmit_Click(sender As Object, e As EventArgs) Handles btnSubmit.Click
        Dim emailid As String = Request.QueryString("emailid")
        Dim uid As String = Request.QueryString("uid")
        Dim otp As Integer = GenerateOTP()
        Dim mnumber As String = String.Empty
        Try

            Dim constr As String = ConfigurationManager.ConnectionStrings("ETSConnectionString").ConnectionString

            'Delete existing OTP from the table for the user
            Using con As New SqlConnection(constr)
                Using cmd As New SqlCommand("Delete UserOTP where userid=@UserId")
                    Using sda As New SqlDataAdapter()
                        cmd.CommandType = CommandType.Text
                        cmd.Parameters.AddWithValue("@UserId", uid)
                        cmd.Connection = con
                        con.Open()
                        cmd.ExecuteNonQuery()
                        con.Close()
                    End Using
                End Using
            End Using

            ' Insert new OTP to the table
            Using con As New SqlConnection(constr)
                Using cmd As New SqlCommand("INSERT INTO UserOTP VALUES(@UserId, @OTPCode,@cdate)")
                    Using sda As New SqlDataAdapter()
                        cmd.CommandType = CommandType.Text
                        cmd.Parameters.AddWithValue("@UserId", uid)
                        cmd.Parameters.AddWithValue("@OTPCode", otp)
                        cmd.Parameters.AddWithValue("@cdate", Now())
                        cmd.Connection = con
                        con.Open()
                        cmd.ExecuteNonQuery()
                        con.Close()
                    End Using
                End Using
            End Using
            mnumber = txtphone.Text
            If sendmobileotp(mnumber, "Your authentication code is " & otp) Then
                'Response.Write(sendmobileotp(mnumber, "Your Activation Code is " & otp))
                Dim rlink As String = Request.QueryString("rlink")
                'rlink = base64Decode(rlink)
                Response.Redirect("NewUserEnvironment.aspx?eid=" & emailid & "&rlink=" & rlink)
            Else
                Label2.Text = "error"
            End If
            'sendOTPwithLink(uid.ToString, acode, emailid, otp, acdes)

        Catch ex As Exception

        End Try
    End Sub
    Private Function sendmobileotp(ByVal mnumber As String, ByVal msg As String) As Boolean
        Using client = New HttpClient()


            Dim req = New List(Of KeyValuePair(Of String, String))()
            req.Add(New KeyValuePair(Of String, String)("user-id", "rgudadhe"))
            req.Add(New KeyValuePair(Of String, String)("api-key", "y0sWWzoKS4LK6JTT2kfx4SctLag5wDZg37M6yDOeiXMr2Xbm"))
            req.Add(New KeyValuePair(Of String, String)("number", mnumber))
            req.Add(New KeyValuePair(Of String, String)("message", msg))

            Dim content = New FormUrlEncodedContent(req)
            Dim response = client.PostAsync("https://neutrinoapi.com/sms-message", content)
            Do While Not response.Status = Threading.Tasks.TaskStatus.RanToCompletion


            Loop
            Return response.Status()
        End Using
    End Function
    Public Function base64Decode(ByVal sData As String) As String
        Dim encoder As New System.Text.UTF8Encoding()
        Dim utf8Decode As System.Text.Decoder = encoder.GetDecoder()
        Dim todecode_byte As Byte() = Convert.FromBase64String(sData)
        Dim charCount As Integer = utf8Decode.GetCharCount(todecode_byte, 0, todecode_byte.Length)
        Dim decoded_char As Char() = New Char(charCount - 1) {}
        utf8Decode.GetChars(todecode_byte, 0, todecode_byte.Length, decoded_char, 0)
        Dim result As String = New [String](decoded_char)
        Return result
    End Function

    
End Class
