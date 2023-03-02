Imports System.Data
Imports System.Data.OleDb
Imports system.Data.SqlClient


Partial Class Transcription_document
    Inherits BasePage


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        
        If Not IsPostBack Then
            hdnURL.Value = WebAddress
            hdnReportPath.Value = EncryptText(System.Configuration.ConfigurationManager.AppSettings("URL") & "/ETS_Files/Samples/" & Request.QueryString("WebPath") & ".doc", "webpath")
            hdnTransID.Value = Request.QueryString("WebPath")
            getKeyWords()
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
    Private Function getKeyWords()
        Try
            Dim clsSamples As New ETS.BL.Samples
            With clsSamples
                .SampleID = hdnTransID.Value
                .getSampleDetails()
                txtKeyWords.Text = .KeyWords
                hdnSampleNo.Value = .SampleNo
                hdnPhyID.Value = .PhyID
                hdnPhyName.Value = .Name
            End With
            clsSamples = Nothing

        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Function
End Class
