
Partial Class Dictation_Search_SaveasDictation
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim filePath As String = Server.MapPath("/ETS_Files") & "/Transcriptions/" & Request.QueryString("DocID") & ".doc"
        Response.Write(filePath)
        Dim targetFile As System.IO.FileInfo = New System.IO.FileInfo(filePath)

        If targetFile.Exists Then

            Response.Clear()
            Response.AddHeader("Content-Disposition", "attachment; filename=" + Request.QueryString("JobID") & ".doc")
            Response.AddHeader("Content-Length", targetFile.Length.ToString)
            Response.ContentType = "application/octet-stream"
            Response.WriteFile(targetFile.FullName)

            Dim clsPRep As ETS.BL.PrevReports
            Try
                clsPRep = New ETS.BL.PrevReports
                clsPRep.JobNumber_Click_From_PrevReports(Request.QueryString("DocID").ToString, Session("UserID").ToString)
            Catch ex As Exception
                Response.Write(ex.Message)
            Finally
                clsPRep = Nothing
            End Try

            'Dim objCmd As New Data.SqlClient.SqlCommand("INSERT INTO tblPrevReportsLog (TranscriptionID,OprBy,OprOn) VALUES('" & Request.QueryString("DocID").ToString & "','" & Session("UserID").ToString & "','" & Now() & "') ", New Data.SqlClient.SqlConnection(System.Configuration.ConfigurationManager.AppSettings("ETSCon")))

            'Try
            '    objCmd.Connection.Open()
            '    objCmd.ExecuteNonQuery()
            'Catch ex As Exception
            'Finally
            '    If objCmd.Connection.State <> Data.ConnectionState.Closed Then
            '        objCmd.Connection.Close()
            '        objCmd.Connection = Nothing
            '        objCmd = Nothing
            '    End If
            'End Try

        End If

    End Sub
End Class
