Imports System.Data.SqlClient
Imports System.Data
Partial Class Department_Default
    Inherits BasePage

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        
        Dim strConn As String
        strConn = System.Configuration.ConfigurationManager.AppSettings("ETSCon")

        Dim strDeptValue As ArrayList = New ArrayList
        Dim strDescValue As ArrayList = New ArrayList
        Dim cmdIns As New SqlCommand("Select Userid, Username, Firstname, Lastname from DBO.tblUsers", New SqlConnection(strConn))
        Try
            cmdIns.Connection.Open()
            DispData.DataSource = cmdIns.ExecuteReader()
            DispData.DataBind()
        Finally
            If cmdIns.Connection.State = ConnectionState.Open Then
                cmdIns.Connection.Close()
            End If
        End Try
    End Sub
End Class
