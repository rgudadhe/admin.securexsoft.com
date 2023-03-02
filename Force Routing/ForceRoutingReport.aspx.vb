
Partial Class Force_Routing_ForceRoutingReport
    Inherits BasePage
    Protected Sub Page_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Init
        If Not String.IsNullOrEmpty(Request.Form("SEARCH")) Then
            Server.Transfer("ForceRoutingReportResult.aspx")
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

                Dim varlst As New ListItem
                varlst.Value = ""
                varlst.Text = "Please Select"
                ULevel.Items.Insert(0, varlst)
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
    Protected Sub ddlRouting_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlRouting.SelectedIndexChanged
        Try
            Dim varvalue As String = String.Empty
            varvalue = ddlRouting.Items(ddlRouting.SelectedIndex).Value.ToString
            If Not String.IsNullOrEmpty(varvalue) Then
                If Trim(UCase(varvalue)) = Trim(UCase("User")) Then
                    'Response.Write("Tst")
                    ULevel.SelectedIndex = -1
                    UserLeveltxt.Visible = True
                    TextBox1.Visible = True
                    UserLevel.Visible = True
                    ULevel.Visible = True
                Else
                    UserLeveltxt.Visible = False
                    TextBox1.Visible = False
                    UserLevel.Visible = False
                    ULevel.Visible = False
                End If
            End If
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub
End Class
