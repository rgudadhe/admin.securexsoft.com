
Partial Class ForceRouting4UserResult
    Inherits BasePage
    Protected Sub Page_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Init
        If Not String.IsNullOrEmpty(Request.Form("SEARCH")) Then
            Server.Transfer("ForceRouting4UserResult.aspx")
        End If
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            Dim ConString As String = System.Configuration.ConfigurationManager.AppSettings("ETSCon")
            Dim oConn As New Data.SqlClient.SqlConnection
            oConn.ConnectionString = ConString
            Try
                oConn.Open()
                Dim SQLString As String = "SELECT LevelName,LevelNo FROM tblProductionLevels where IsDeleted=0 and levelNo<>1073741824 and Type =" & Session("IsContractor")
                Dim oCommand As New Data.SqlClient.SqlCommand(SQLString, oConn)
                Dim oRec As Data.SqlClient.SqlDataReader = oCommand.ExecuteReader
                If oRec.HasRows Then
                    ULevel.DataSource = oRec
                    ULevel.DataTextField = "LevelName"
                    ULevel.DataValueField = "LevelNo"
                    ULevel.DataBind()
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
