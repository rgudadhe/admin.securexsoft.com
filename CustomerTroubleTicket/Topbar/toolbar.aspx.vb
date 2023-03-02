
Partial Class Topbar_toolbar
    Inherits System.Web.UI.Page
    Public logopath As String
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Dim objConn As New Data.SqlClient.SqlConnection
        objConn = OpenConnection(objConn)
        
        Try
            'Get User Info
            Dim LogoFolder As String

            LogoFolder = "../../ETS_Files/Logos/"
            Dim SQLCmd1 As New Data.SqlClient.SqlCommand("SELECT * FROM dbo.tblContractorLogo where Contractorid='" & Session("contractorid").ToString & "' ", objConn)
            
            Dim DRRec1 As Data.SqlClient.SqlDataReader = SQLCmd1.ExecuteReader()
            If DRRec1.HasRows Then
                If (DRRec1.Read) Then
                    logopath = LogoFolder & DRRec1("logoid1").ToString & ".jpg"
                End If
            End If
            DRRec1.Close()


            'End User Info
        Catch ex As Exception
            Response.Write(ex.Message)
        Finally
            If objConn.State <> Data.ConnectionState.Closed Then
                objConn.Close()
                objConn = Nothing
            End If
        End Try
    End Sub
    Protected Function OpenConnection(ByRef Conn As Data.SqlClient.SqlConnection) As Data.SqlClient.SqlConnection
        Conn.ConnectionString = System.Configuration.ConfigurationManager.AppSettings("ConnectionString")
        Conn.Open()
        Return Conn
    End Function
End Class
