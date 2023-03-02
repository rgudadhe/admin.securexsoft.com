
Partial Class UploadNewsLetter
    Inherits BasePage
    Protected Sub Button1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button1.Click
        Try
            Dim edate As Date
            Dim FName As String

            Dim FileName As String = Server.MapPath("ETS_Files") & "\NewsLetter\e-Connect " & DLMonth.SelectedItem.Text & " " & DLYear.SelectedValue & ".pdf"
            Dim varTempDir As New System.IO.DirectoryInfo(Server.MapPath("ETS_Files") & "\NewsLetter")

            If varTempDir.Exists = False Then
                varTempDir.Create()
            End If

            FName = "e-Connect " & DLMonth.SelectedItem.Text & " " & DLYear.SelectedValue
            edate = Convert.ToDateTime("" & DLMonth.SelectedValue & "/1/" & DLYear.SelectedValue & "")
            FileUpload1.SaveAs(FileName)

            If System.IO.File.Exists(FileName) Then
                Dim RetVal As Integer = 0
                Dim clsNL As New ETS.BL.NewsLetter
                With clsNL
                    .ContractorID = Session("ContractorID").ToString
                    .filename = FName.ToString
                    .dateupdated = Now
                    .eDate = edate
                    .userid = Session("userid").ToString
                End With

                RetVal = clsNL.InsertNewsLetterDetails()
                clsNL = Nothing
                If RetVal = 1 Then
                    iresponse.Text = "NewsLetter has been added successfully."
                Else
                    iresponse.Text = "Failed adding NewsLetter details"
                End If
            Else
                iresponse.Text = "Failed to upload news letter"
            End If

        Catch ex As Exception
        End Try
    End Sub
End Class
