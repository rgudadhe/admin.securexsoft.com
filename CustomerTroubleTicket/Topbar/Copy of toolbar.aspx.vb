
Partial Class Topbar_toolbar
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Dim objConn As New Data.SqlClient.SqlConnection
        objConn = OpenConnection(objConn)
        Try
            'Get User Info
            Dim objCmd As New Data.SqlClient.SqlCommand("select U.Password from SecureWeb.dbo.tblUsers U LEFT JOIN dbo.tblaccounts A ON  A.AccountID=U.AccID LEFT JOIN dbo.tblGrpAccounts A1 ON  A1.GrpActID=U.AccID where U.UserID = '" & Session("UserID").ToString & "'", objConn)
            Dim objRec As Data.SqlClient.SqlDataReader = objCmd.ExecuteReader

            If objRec.HasRows Then
                While objRec.Read
                    If Not objRec.IsDBNull(objRec.GetOrdinal("password")) Then
                        hdnPwd.Value = objRec.GetString(objRec.GetOrdinal("password"))
                    End If
                End While
            End If
            objRec.Close()
            objRec = Nothing
            objCmd = Nothing

            hdnUserId.Value = Session("userLogin").ToString

            'End User Info
        Catch ex As Exception
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
