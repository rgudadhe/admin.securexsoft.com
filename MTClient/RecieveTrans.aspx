<%@ Page Language="VB" %>
<%@ Import Namespace="SoftArtisans.Net" %>
<script runat="server">
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim oFileUp As New FileUp(Context)
        Try
            
            If Not IO.Directory.Exists(Server.MapPath("../ETS_Files/Temp")) Then
                IO.Directory.CreateDirectory(Server.MapPath("../ETS_Files/Temp"))
            End If
            oFileUp.Path = Server.MapPath("../ETS_Files/Temp")
            oFileUp.SaveAs(oFileUp.ShortFilename)
            oFileUp.Dispose()
            Dim oFile As New IO.FileInfo(Server.MapPath("../ETS_Files/Temp") & "/" & Request.Form("TransID") & ".doc")
            If oFile.Exists Then
                Response.Write("1")
            Else
                Response.Write("0")
            End If
        Catch ex As Exception
            Response.Write(ex.Message)
        
        End Try
    End Sub
</script>

