Imports System
Imports System.Data

Imports System.Web.Util
Partial Class Transcription_SaveReport
    Inherits System.Web.UI.Page
    
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        
        Dim TempCount As Integer = 1

        
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
            Dim KeyWords As String = Replace(upl.Form("KeyWords").ToString, "'", "")
            KeyWords = Replace(KeyWords, ",", "")
            KeyWords = Replace(KeyWords, """", "")
            
            upl.Form("File").saveas(strFileName)
            Dim samno As Integer

            Dim OFM As New IO.FileInfo(strFileName)
            If OFM.Exists Then
                If OFM.Length = upl.Form("File").TotalBytes Then
                    Dim clsSamples As New ETS.BL.Samples
                    With clsSamples
                        .SampleID = SampleID
                        Dim DS As DataSet = .getSampleList()
                        If DS.Tables.Count > 0 Then
                            If DS.Tables(0).Rows.Count > 0 Then

                                samno = DS.Tables(0).Compute("MAX(SampleNo)", "") + 1

                            End If
                        End If
                       
                        If .SaveSample(upl.Form("SampleName").ToString, KeyWords, Session("UserID"), Session("ContractorID").ToString) Then
                            Response.Write(1)
                        End If

                    End With

                   
                Else
                    Response.Write("Sample Not Recieved")
                End If
            Else
                Response.Write(strFileName)
            End If

        Catch ex As Exception
            Response.Write(ex.Message)

        End Try
    End Sub
    
End Class
