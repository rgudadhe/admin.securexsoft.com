
Partial Class CSMO_Uploader_SaveUploadfile
    Inherits System.Web.UI.Page
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            Dim upl As New SoftArtisans.Net.FileUp(Context)
            Dim varFName As String = String.Empty
            Dim varFileTempName As String = String.Empty
            Dim varFileName As String = String.Empty
            Dim varFileSize As Long = 0
            Dim varUploadedFileSize As Long = 0

            varFName = upl.Form("FileName")
            varFileSize = CLng(upl.Form("FileSize"))
            Dim varDir As New System.IO.DirectoryInfo(IO.Path.Combine(Server.MapPath("/ets_files/"), "CSMO Uploader"))

            If Not varDir.Exists Then
                varDir.Create()
            End If

            Dim varDirTemp As New System.IO.DirectoryInfo(IO.Path.Combine(Server.MapPath("/ets_files/"), "CSMO Uploader\Temp"))

            If Not varDirTemp.Exists Then
                varDirTemp.Create()
            End If


            varFileName = IO.Path.Combine(varDir.FullName.ToString, varFName)
            varFileTempName = IO.Path.Combine(varDirTemp.FullName.ToString, varFName)

            
            upl.Form("File").saveas(varFileTempName)
            varUploadedFileSize = GetFileSize(varFileTempName)

            If varFileSize = varUploadedFileSize Then

                System.IO.File.Move(varFileTempName, varFileName)

                If System.IO.File.Exists(varFileName.ToString) = True Then
                    Dim FILENAME As String = IO.Path.Combine(varDirTemp.FullName.ToString, Day(Now) & Month(Now) & Year(Now) & ".txt")

                    'Get a StreamWriter class that can be used to write to the file
                    Dim objStreamWriter As IO.StreamWriter
                    objStreamWriter = IO.File.AppendText(FILENAME)

                    'Append the the end of the string, "A user viewed this demo at: "
                    'followed by the current date and time
                    objStreamWriter.WriteLine(varFileName.ToString & DateTime.Now.ToString())

                    'Close the stream
                    objStreamWriter.Close()

                    Response.Write("1")
                Else
                    Response.Write("0")
                End If
            Else
                Response.Write("0")
            End If
        Catch ex As Exception
            Response.Write("0")
        End Try
    End Sub
    Private Function GetFileSize(ByVal MyFilePath As String) As Long
        Try
            Dim MyFile As New System.IO.FileInfo(MyFilePath)
            Dim FileSize As Long = MyFile.Length
            Return FileSize
        Catch ex As Exception
            Return 0
        End Try
    End Function
End Class
