<%@ Page Language="VB" %>
<%@ Import Namespace="SoftArtisans.Net" %>



<script runat="server">

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim RecAffected As Integer
        Try
            Dim oFileUp As New FileUp(Context)
            oFileUp.Path = Server.MapPath("/ETS_Files/Templates")
            oFileUp.SaveAs(oFileUp.ShortFilename)
            oFileUp.Dispose()
            Dim oFile As New IO.FileInfo(Server.MapPath("/ETS_Files/Templates") & "/" & Request.QueryString("TempID") & ".doc")
            Dim oFile1 As New IO.FileInfo(Server.MapPath("/ETS_Files/Templates") & "/" & Request.QueryString("TempID") & ".docx")
            Dim oFile2 As New IO.FileInfo(Server.MapPath("/ETS_Files/Templates") & "/" & Request.QueryString("TempID") & ".rtf")
            If oFile.Exists Or oFile1.Exists Or oFile2.Exists Then
                Dim MTC As New MTClientService
                RecAffected = MTC.AuditTemplates(Request.QueryString("TempID"), Request.QueryString("UserID"), True)
                If RecAffected > 0 Then
                    Response.Write("1")
                Else
                    Response.Write("0")
                End If
            Else
                Response.Write("2")
            End If
        Catch ex As Exception
            Response.Write(ex.message)
        End Try
    End Sub
</script>

