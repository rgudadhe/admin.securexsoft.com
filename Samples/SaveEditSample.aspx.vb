
Partial Class Samples_SaveEditSample
    Inherits BasePage

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        
        Try

            Dim strFileName As String
            Dim upl As New SoftArtisans.Net.FileUp(Context)
            Dim SampleID As String = upl.Form("TransID").ToString
            strFileName = Server.MapPath("/ETS_Files") & "/Samples/" & SampleID & ".doc"
            If Not IO.Directory.Exists(Server.MapPath("/ETS_Files") & "/Samples") Then
                IO.Directory.CreateDirectory(Server.MapPath("/ETS_Files") & "/Samples")
            End If
            If Not Len(upl.Form("TransID")) = 36 Then
                Response.Write("Please login and try again")
                Response.End()
            End If
            Dim KeyWords As String = Replace(upl.Form("KeyWords").ToString, "'", "''")
            KeyWords = Replace(KeyWords, ",", "")
            KeyWords = Replace(KeyWords, """", "")

            upl.Form("File").saveas(strFileName)


            Dim OFM As New IO.FileInfo(strFileName)
            If OFM.Exists Then
                If OFM.Length = upl.Form("File").TotalBytes Then

                    Try
                        Dim clsSamples As New ETS.BL.Samples
                        With clsSamples
                            .SampleID = SampleID.ToString
                            .KeyWords = KeyWords
                            .DateModified = Now().ToString
                            If .UpdateSample() > 0 Then
                                Response.Write("1")
                            Else
                                Response.Write("0")
                            End If
                        End With
                        clsSamples = Nothing
                        
                    Catch ex As Exception
                        Response.Write(ex.Message)
                   
                    End Try
                Else
                    Response.Write("Sample Not Recieved")
                End If
            Else
                Response.Write(strFileName)
            End If
        Catch ex As Exception
            Response.Write("ERROR :" & ex.Message)
            
        End Try
    End Sub
End Class
