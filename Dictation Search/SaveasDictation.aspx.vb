
Partial Class Dictation_Search_SaveasDictation
    Inherits System.Web.UI.Page
    Public MediaURL As String
    Public WebAddress As String = System.Configuration.ConfigurationManager.AppSettings("URL")
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim FileType As String
        Dim TransID As String
        FileType = Request.QueryString("Type")
        TransID = Request.QueryString("TransID")
        'Dim obj As New ETS.BL.Transcription
        'obj.
        TCell1.Text = Request.QueryString("JobNumber")
        TCell2.Text = Request.QueryString("CustjobID")
        TCell3.Text = FileType
        Dim filePath As String = Server.MapPath("/ETS_Files") & "\BackUp\" & TransID & FileType
        Dim wavfilePath As String = Server.MapPath("/ETS_Files") & "\BackUp\" & TransID & ".wav"
        Dim TempfilePath As String = Server.MapPath("../sxfvoice") & "\" & TransID & ".wav"
        'Response.Write(filePath)
        'Response.Write("Combine Path :" & IO.Path.Combine(Server.MapPath("/ETS_Files/BackUp"), TransID.ToString & FileType))
        Dim FInfo As New System.IO.FileInfo(filePath)
        If FInfo.Exists Then
            If FileType.ToUpper = ".DSS" Then
                If IO.File.Exists(wavfilePath) Then
                    IO.File.Copy(wavfilePath, TempfilePath, True)
                    MediaURL = WebAddress & "/sxfvoice/" & TransID & ".wav"
                Else
                    Try
                    MediaURL = WebAddress & "/ETS_Files/BackUp/" & TransID & ".dss"

                    TRow1.Visible = True

                    Dim DestFPath As String
                    Dim TarFPath As String
                    'DestFPath = "D:\secureweb\temp\" & TransID & fileType
                    'TarFPath = "D:\secureweb\temp\" & TransID & ".wav"
                    'MediaURL = WebAddress & "\temp\" & TransID & ".wav"
                    DestFPath = "D:\dsscon\DS2\" & TransID & FileType
                    'DestFPath = Server.MapPath("../vtemp") & "\" & TransID & fileType
                    TarFPath = Server.MapPath("../sxfvoice") & "\" & TransID & ".wav"
                    'MediaURL = WebAddress & "/sxfvoice/" & TransID & ".wav"
                    Dim IntWait As Integer
                    Dim TFileInfo As New System.IO.FileInfo(TarFPath)
                    If Not TFileInfo.Exists Then
                        Dim DFileInfo As New System.IO.FileInfo(DestFPath)
                        If Not DFileInfo.Exists Then
                            FInfo.CopyTo(DestFPath)
                        End If
                        Dim fLen As Integer = System.Convert.ToInt32(FInfo.Length)
                        If fLen > 100 Then
                            IntWait = (fLen / 100) + 2000
                        ElseIf fLen > 100000 Then
                            IntWait = (fLen / 100) + 6000
                        End If
                        Dim process1 As New System.Diagnostics.Process()
                        'process1.StartInfo.FileName = "cmd.exe"
                        'process1.StartInfo.Arguments = "/c schtasks.exe /run /tn ""dsscon"" "
                        'process1.Start()
                        'process1.WaitForExit()
                        'process1.Close()
                        'process1.Dispose()
                        'Response.Write("C:\Program Files (x86)\SecureXSoft\OlyDSS2WavSetup\OlyDSS2Wav.exe" & """" & DestFPath & """ """ & TarFPath & """  ")


                        process1.StartInfo.FileName = "C:\Program Files (x86)\SecureXSoft\OlyDSS2WavSetup\OlyDSS2Wav.exe"
                        process1.StartInfo.Arguments = """" & DestFPath & """ """ & TarFPath & """  "
                        'Process1.StartInfo.FileName = "C:\Program Files (x86)\DSS2Wave\dss2wav.exe"
                        'Process1.StartInfo.Arguments = """" & DestinationPath & """ """ & FInfo.FullName & """  "
                        process1.Start()
                            process1.WaitForExit(10000)


                   
                    Else
                    MediaURL = WebAddress & "/sxfvoice/" & TransID & ".wav"
                    'Exit Sub
                    End If
                    'Response.Write(TarFPath)
                    'Response.Write(System.IO.File.Exists(TarFPath))
                    If Not System.IO.File.Exists(TarFPath) Then
                        System.Threading.Thread.Sleep(20000)
                    Else
                        MediaURL = WebAddress & "/sxfvoice/" & TransID & ".wav"
                        ' Exit Sub
                    End If
                    'Response.Write(System.IO.File.Exists(TarFPath))
                    If Not System.IO.File.Exists(TarFPath) Then
                        If System.IO.File.Exists(DestFPath) Then
                            System.IO.File.Delete(DestFPath)
                            DestFPath = Server.MapPath("../sxfvoice") & "\" & TransID & FileType
                            If Not System.IO.File.Exists(DestFPath) Then
                                FInfo.CopyTo(DestFPath)

                            End If
                            MediaURL = WebAddress & "/sxfvoice/" & TransID & ".dss"
                        End If
                    Else
                        MediaURL = WebAddress & "/sxfvoice/" & TransID & ".wav"
                        ' Exit Sub
                        End If
                    Catch ex As Exception
                        Response.Write(ex.Message)
                    End Try
                End If
                'ElseIf FileType.ToUpper = ".TIF" Then
                '    'TRow2.Visible = True
                '    MediaURL = WebAddress & "/ETS_Files/dictations/" & TransID & ".tif"
            ElseIf FileType.ToUpper = ".MP3" Then
                'TRow1.Visible = True
                MediaURL = WebAddress & "/ETS_Files/BackUp/" & TransID & ".MP3"
            Else
                'TRow1.Visible = True
                If IO.File.Exists(wavfilePath) Then
                    IO.File.Copy(wavfilePath, TempfilePath, True)
                    MediaURL = WebAddress & "/sxfvoice/" & TransID & ".wav"
                End If
            End If
        Else
            Label1.Text = "Record not found"
        End If
        'Response.Write(MediaURL)
    End Sub
End Class
