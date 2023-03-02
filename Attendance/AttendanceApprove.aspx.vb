
Partial Class AttendanceApprove
    Inherits BasePage
    Dim ConString As String
    Dim oConn As New Data.SqlClient.SqlConnection
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim varStrSQLQuery As String

        Session("UserID") = "9218d9ce-373f-4ac9-8ac9-79bfc2e07452"
        ConString = System.Configuration.ConfigurationManager.AppSettings("ETSCon")
        oConn.ConnectionString = ConString
        Try
            oConn.Open()
            varStrSQLQuery = "SELECT A.AttReqID,U.FirstName,U.LastName,A.UserID,A.AttDate,A.SignIn,A.SignOut,A.Reason,A.AppDate FROM dbo.tblAttendanceRequest A INNER JOIN dbo.tblUsers U ON U.UserID=A.UserID WHERE A.ApproveBy='" & Session("UserID") & "' AND A.Status='Pending'"

            Dim oCommandAApprove As New Data.SqlClient.SqlCommand(varStrSQLQuery, oConn)
            Dim oRecAApprove As Data.SqlClient.SqlDataReader = oCommandAApprove.ExecuteReader()

            AttendanceApproval.DataSource = oRecAApprove
            AttendanceApproval.DataBind()

            oRecAApprove.Close()
            oRecAApprove = Nothing
            oCommandAApprove = Nothing
        Catch ex As Exception
            Response.Write(ex.Message)
        Finally
            If oConn.State <> Data.ConnectionState.Closed Then
                oConn.Close()
            End If
        End Try
    End Sub
    Protected Sub Page_Unload(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Unload
        If oConn.State <> Data.ConnectionState.Closed Then
            oConn.Close()
            oConn = Nothing
        End If
    End Sub
End Class
