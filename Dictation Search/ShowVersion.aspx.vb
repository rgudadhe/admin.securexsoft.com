Imports System.Data
Imports System.Data.OleDb
Imports system.Data.SqlClient


Partial Class Transcription_document
    Inherits BasePage
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            Dim URL As String = System.Configuration.ConfigurationManager.AppSettings("URL")
            hdnReportPath.Value = EncryptText(URL & "/ETS_Files/Transcriptions/" & Request.QueryString("DocID"), "webpath")
            hdnTransID.Value = Request.QueryString("WebPath")
            Dim obj As New ETS.BL.DynamicSQL()
            obj.ExecViewAuditTrail("SecureWeb.dbo.tbltranscriptionClient", "transcriptionid", Request.QueryString("DocID").Replace(".doc", ""))
            If IO.File.Exists(Server.MapPath("/ETS_Files") & "/Transcriptions/" & Request.QueryString("DocID")) Then
                hdnTransID.Value = "Yes"
            Else
                hdnTransID.Value = "No"
            End If
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
End Class
