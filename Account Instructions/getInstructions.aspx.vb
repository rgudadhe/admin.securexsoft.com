
Partial Class Account_Instructions_getInstructionst
    Inherits BasePage
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim clsAI As ETS.BL.AccountInstructions

        Try
            Dim upl As New SoftArtisans.Net.FileUp(Context)
            Dim AccID As String = upl.Form("AccountID").ToString
            Dim DocType As String = upl.Form("DocType").ToString
            Dim strFileName As String = Server.MapPath("/ETS_Files") & "/Instructions/" & AccID & DocType.ToString
            If Not IO.Directory.Exists(Server.MapPath("/ETS_Files") & "/Instructions") Then
                IO.Directory.CreateDirectory(Server.MapPath("/ETS_Files") & "/Instructions")
            End If

            clsAI = New ETS.BL.AccountInstructions

            clsAI.AccountID = AccID.ToString
            clsAI.Format = DocType.ToString
            clsAI.DateModified = Now
            clsAI.UserID = Session("UserID").ToString




            If clsAI.SetAccountInstructions_btnClicked() = True Then
                upl.Form("File").saveas(strFileName)
                Dim OFM As New IO.FileInfo(strFileName)
                If OFM.Exists Then
                    If OFM.Length = upl.Form("File").TotalBytes Then
                        Response.Write("1")
                    Else
                        Response.Write("0")
                    End If
                Else
                    Response.Write("0")
                End If
            Else
                Response.Write("0")
            End If
        Catch ex As Exception
        Finally
            clsAI = Nothing
        End Try
    End Sub
End Class
