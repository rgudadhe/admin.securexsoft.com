Imports System.Data.SqlClient

Partial Class Billing_LCMethods_LCMethodology
    Inherits BasePage

   

   

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim strConn As String
        strConn = System.Configuration.ConfigurationManager.AppSettings("ETSCon")
        Dim SQLCmd As New SqlCommand("ADMINSecureweb.dbo.SF_getUSerDetailsForBilling", New SqlConnection(strConn))
        SQLCmd.CommandType = Data.CommandType.StoredProcedure
        Try
            SQLCmd.Connection.Open()
            Dim DRRec As SqlDataReader = SQLCmd.ExecuteReader()
            Dim DT As New System.Data.DataTable
            DT.Load(DRRec)
            MyDataGrid.DataSource = DT
            MyDataGrid.DataBind()
            '        SQLCmd.Connection.Close()

        Finally
            If SQLCmd.Connection.State = System.Data.ConnectionState.Open Then
                SQLCmd.Connection.Close()
                SQLCmd = Nothing
            End If
        End Try
    End Sub

   
   
End Class
