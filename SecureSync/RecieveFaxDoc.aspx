<%@ Page Language="VB" %>
<%@ Import Namespace="SoftArtisans.Net" %>
<script runat="server">
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim oFileUp As New FileUp(Context)
        Try
            Server.ScriptTimeout = 1200
            Session.Timeout = 1200
            
            Dim strCustJob As String = String.Empty
            Dim strFileEx As String = String.Empty
            Dim AccID As String = String.Empty
            Dim strQuery As String = String.Empty
            Dim CustVRec As Boolean = False
            Dim ConString As String = System.Configuration.ConfigurationManager.AppSettings("ETSCon")
          
            oFileUp.Path = Server.MapPath("/ETS_Files/GFIFaxDoc")
            oFileUp.SaveAs(Request.Form("DictName") & ".pdf")
            Dim oFile As New IO.FileInfo(Server.MapPath("/ETS_Files/GFIFaxDoc") & "/" & Request.Form("DictName") & ".pdf")
            If oFile.Exists Then
                If oFile.Length.ToString = Request.Form("DictSize") Then
                    Response.Write("1")
                  
                End If
            Else
                Response.Write("0")
            End If
            ' oFileUp.Dispose()
        Catch ex As Exception
            Response.Write("4")
        Finally
            oFileUp.Dispose()
        End Try
    End Sub
</script>

