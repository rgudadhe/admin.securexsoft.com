Imports System.Data
Imports System.Data.SqlClient
Imports System.Data.SqlDbType




Partial Class Department_Default
    Inherits BasePage


    
    
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
    End Sub

    
    
    
    
   

    
    
    Protected Sub Button1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button1.Click
        Dim RecFound As String
        Dim strQuery As String


        RecFound = "No"
        Dim strConn As String
        strConn = System.Configuration.ConfigurationManager.AppSettings("ETSCon")
        Dim oConn As New Data.SqlClient.SqlConnection(strConn)
        Try
            oConn.Open()
            strQuery = "Insert Into TblActCategories ( Description, Priority) Values ( '" & TxtCategory.Text & "','" & TxtPriority.Text & "')"
            Dim cmdUp As New SqlCommand(strQuery, oConn)
            cmdUp.ExecuteNonQuery()
            TxtCategory.Text = ""
            TxtPriority.Text = ""
            Response.Write("Record has been inserted into database successfully")
            Response.End()
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
