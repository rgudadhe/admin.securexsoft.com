Imports System.IO
Partial Class FaxPlus_SearchFaxCover
    Inherits System.Web.UI.Page
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Dim clsFAX As ETS.BL.FaxPlus
        Dim DSFAX As New Data.DataSet

        Dim varStrUserID As String = String.Empty
        Dim varStrCoverID As String = String.Empty
        Dim varStrEx As String = String.Empty
        Dim varStrFileName As String = String.Empty

        varStrCoverID = Request.Form("CoverID")
        'varStrCoverID = "7C219ADE-9999-43F7-954D-0BB1AB4D28CB"

        Try
            clsFAX = New ETS.BL.FaxPlus
            DSFAX = clsFAX.getFAXCoverPageInfo(varStrCoverID)
            If DSFAX.Tables.Count > 0 Then
                If DSFAX.Tables(0).Rows.Count > 0 Then
                    For Each DR As Data.DataRow In DSFAX.Tables(0).Rows
                        varStrUserID = Trim(DR("UserID").ToString)
                        varStrEx = Trim(DR("Ex").ToString)
                    Next
                End If
            End If


            varStrFileName = Server.MapPath("/ets_files/secureweb/_FaxCover") & "\" & varStrCoverID & varStrEx
            If File.Exists(varStrFileName) Then
                'Response.Write(System.Configuration.ConfigurationManager.AppSettings("Coverpage") & varStrFolderName & "/CoverPage/" & varStrCoverID & varStrEx & "#" & varStrUserID)
                Response.Write("/ets_files/secureweb/_FaxCover/" & varStrCoverID & varStrEx & "#" & varStrUserID)
            Else
                Response.Write(varStrFileName)
            End If

        Catch ex As Exception
            Response.Write(ex.Message)
        Finally
            clsFAX = Nothing
            DSFAX = Nothing
        End Try
    End Sub
End Class
