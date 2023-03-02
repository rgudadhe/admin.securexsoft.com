
Partial Class FaxPlus_FaxHistory
    Inherits System.Web.UI.UserControl

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim ConString As String = System.Configuration.ConfigurationManager.AppSettings("ETSCon")
        Dim oConn As New Data.SqlClient.SqlConnection
        oConn.ConnectionString = ConString
        Try
            oConn.Open()
            Dim SQLString As String = "SELECT U.LastName + ' ' + U.UserName + '(' + U.FirstName + ')' AS UserName, HLS.StatusDesc, HLH.DateModified " & _
            "FROM tblUsers AS U INNER JOIN tblHl7History AS HLH ON U.UserID = HLH.UserID INNER JOIN tblHL7Status AS HLS ON HLH.Status = HLS.StatusID " & _
            "where HLH.TranscriptionID='" & hdnTransID.Value.ToString & "' " & _
            "ORDER BY HLH.DateModified DESC"
            Dim oCommand As New Data.SqlClient.SqlCommand(SQLString, oConn)
            Dim oRec As Data.SqlClient.SqlDataReader = oCommand.ExecuteReader
            rptHistory.DataSource = oRec
            rptHistory.DataBind()
            oRec.Close()
        Catch ex As Exception
            Response.Write(ex.Message)
        Finally
            If oConn.State <> Data.ConnectionState.Closed Then
                oConn.Close()
                oConn = Nothing
            End If
        End Try
    End Sub
End Class
