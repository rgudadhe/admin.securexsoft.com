Imports system.io
Imports System.Net
Imports Microsoft.Office.Interop
Partial Class Dictation_Search_ShowPDF
    Inherits System.Web.UI.Page
    Public MediaURL As String
    Public WebAddress As String = System.Configuration.ConfigurationManager.AppSettings("URL")
    Public MediaType As String
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try

            Dim Val As String = Request("McID")
            'Response.Write(Val)
            Dim Str() As String = Val.Split("|")
            Dim Version As String = Str(1).Trim
            Dim McID As String = Str(0).Trim
            MediaURL = WebAddress & "/ETS_Files/Macros/Rendered/Versions/" & McID & "_" & Version & ".pdf"
            'Response.Write(MediaURL)
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub
End Class
