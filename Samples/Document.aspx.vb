Imports System.Data
Imports System.Data.OleDb
Imports system.Data.SqlClient


Partial Class Transcription_document
    Inherits BasePage


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        
        If Not IsPostBack Then
            hdnURL.Value = WebAddress
            hdnReportPath.Value = EncryptText(System.Configuration.ConfigurationManager.AppSettings("URL") & "/ETS_Files/Transcriptions/" & Request.QueryString("WebPath") & ".doc", "webpath")
            hdnTransID.Value = Request.QueryString("WebPath")
        End If
    End Sub
    Function EncryptText(ByVal strText, ByVal strPwd)
        Dim i, c
        Dim strBuff
        If strPwd <> "" And strText <> "" Then
            strPwd = UCase(strPwd)
            If Len(strPwd) Then
                For i = 1 To Len(strText)
                    c = Asc(Mid(strText, i, 1))
                    c = c + Asc(Mid(strPwd, (i Mod Len(strPwd)) + 1, 1))
                    strBuff = strBuff & Chr(c And &HFF)
                Next
            Else
                strBuff = strText
            End If
            EncryptText = strBuff
        Else
            EncryptText = ""
        End If
    End Function
    Protected Sub btnDecline_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnDecline.Click
        Try
            Dim RetMessage As String
            Dim clsSamples As New ETS.BL.Samples
            If clsSamples.SetSample(hdnTransID.Value, Session("UserID").ToString, 1) > 0 Then
                RetMessage = "alert(""Sample has been removed successfully!"");"
            Else
                RetMessage = "alert(""Falsed removing sample!"");"
            End If
            Response.Write("<script language=""Javascript"">")
            Response.Write(RetMessage)
            Response.Write("</script>")
            Response.Write("<script language=""vbscript"">")
            Response.Write("window.navigate(""SetSamples.aspx"")")
            Response.Write("</script>")
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub
End Class
