
Partial Class FaxPlus_FaxPlusStatus
    Inherits BasePage
    Protected Sub Page_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Init

        If Not String.IsNullOrEmpty(Request.Form("SEARCH")) Then
            Server.Transfer("HL7Result_New.aspx")
        End If
        If Not IsPostBack Then
            Dim ConString As String = System.Configuration.ConfigurationManager.AppSettings("ETSCon")
            Dim oConn As New Data.SqlClient.SqlConnection
            oConn.ConnectionString = ConString
            Try
                oConn.Open()
                Dim SQLString As String = "SELECT null as StatusID,'Any' as StatusDesc FROM tblHL7Status union SELECT -1 as StatusID,'Not Finished' as StatusDesc FROM tblHL7Status union SELECT StatusID,StatusDesc FROM tblHL7Status order by StatusID"
                Dim oCommand As New Data.SqlClient.SqlCommand(SQLString, oConn)
                Dim oRec As Data.SqlClient.SqlDataReader = oCommand.ExecuteReader
                If oRec.HasRows Then
                    FStatus.DataSource = oRec
                    FStatus.DataTextField = "StatusDesc"
                    FStatus.DataValueField = "StatusID"
                    FStatus.DataBind()
                End If
                oRec.Close()
            Catch ex As Exception
                Response.Write(ex.Message)
            Finally
                If oConn.State <> Data.ConnectionState.Closed Then
                    oConn.Close()
                    oConn = Nothing
                End If
            End Try
        End If
        
    End Sub
End Class
