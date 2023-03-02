Imports System.Net
Imports System.Collections.Generic
Imports System.IO
Imports System.Linq
Partial Class Account_AthenaHealthAPIBrowser
    Inherits System.Web.UI.Page
    Private baseUrl As String = "https://api.athenahealth.com/"
    Dim key As String = ConfigurationSettings.AppSettings("key")
    Dim secret As String = ConfigurationSettings.AppSettings("secret")
    Dim version As String = ConfigurationSettings.AppSettings("version")
    Private DestinationPath As String = ConfigurationSettings.AppSettings("DestinationPath").ToString
    Dim token As String = ""

    Protected Sub form1_Load(sender As Object, e As EventArgs) Handles form1.Load
        Dim url As String = PathJoin(baseUrl, version, practiceid, "/providers")

        Dim request As WebRequest
        ' Create the request, add in the auth header
        request = WebRequest.Create(url)
        request.Method = "GET"
        request.Headers("Authorization") = Convert.ToString("Bearer ") & token
    End Sub
    Public Shared Function GetToken() As Boolean
        Try


            ' Easier to keep track of OAuth prefixes
                Dim auth_prefixes As New Dictionary(Of String, String)() From {{"v1", "/oauth"},{"preview1", "/oauthpreview"},{"openpreview1", "/oauthopenpreview"}}


            ' Basic access authentication
                Dim parameters As New Dictionary(Of String, String)() From {{"grant_type", "client_credentials"}            }

            ' Create and set up a request
            Dim request As WebRequest = WebRequest.Create(PathJoin(baseUrl, auth_prefixes(version), "/token"))
            request.Method = "POST"
            request.ContentType = "application/x-www-form-urlencoded"

            ' Make sure to add the Authorization header
            Dim auth As String = System.Convert.ToBase64String(UTF8.GetBytes(Convert.ToString(key & Convert.ToString(":")) & secret))
            request.Headers("Authorization") = Convert.ToString("Basic ") & auth

            ' Encode the parameters, convert it to bytes (because that's how the streams want it)
            Dim encoded As String = UrlEncode(parameters)
            Dim UTF8 As Encoding = System.Text.Encoding.GetEncoding("utf-8")
            Dim content As Byte() = UTF8.GetBytes(encoded)

            ' Write the parameters to the body
            Dim writer As Stream = request.GetRequestStream()
            writer.Write(content, 0, content.Length)
            writer.Close()

            ' Get the response, read it out, and decode it
            Dim response As WebResponse = request.GetResponse()
            Dim receive As Stream = response.GetResponseStream()
            Dim reader As New StreamReader(receive, UTF8)
            Dim authorization As Linq.JObject = Linq.JObject.Parse(reader.ReadToEnd())

            ' Make sure to grab the token!
            token = authorization("access_token")
            Console.WriteLine(token)

            ' And always remember to close the readers and streams
            response.Close()

        Catch ex As Exception
            Console.WriteLine(ex.Message)

        End Try
    End Function
    Public Shared Function UrlEncode(ByVal dict As Dictionary(Of String, String)) As String
        Return String.Join("&", dict.[Select](Function(kvp) WebUtility.UrlEncode(kvp.Key) + "=" + WebUtility.UrlEncode(kvp.Value)).ToList())
    End Function

    ' A useful function for joining paths into URLs
    Public Shared Function PathJoin(ByVal ParamArray args As String()) As String
        Return String.Join("/", args.[Select](Function(arg) arg.Trim(New Char() {"/"c})).Where(Function(arg) Not [String].IsNullOrEmpty(arg)))
    End Function
End Class

