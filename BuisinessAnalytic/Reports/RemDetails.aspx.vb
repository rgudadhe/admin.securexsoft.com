Imports System.Data.SqlClient
Partial Class Billing_Reports_Default2
    Inherits BasePage

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim strQuery As String
        Try

            Dim strConn As String

            Dim InvRecFound As Boolean
            Dim InvoiceID As String = ""
            InvRecFound = False
            strConn = System.Configuration.ConfigurationManager.AppSettings("ETSCon")


            If Request("autoid") <> "" Then
                strQuery = "Delete from AdminSecureweb.dbo.tblInvItemdet where autoID =" & Request("autoid") & " "
                '   Response.Write(strQuery)
                Dim SQLCmdUp4 As New SqlCommand(strQuery, New SqlConnection(strConn))
                Try
                    SQLCmdUp4.Connection.Open()
                    SQLCmdUp4.ExecuteNonQuery()
                Finally
                    If SQLCmdUp4.Connection.State = System.Data.ConnectionState.Open Then
                        SQLCmdUp4.Connection.Close()
                        SQLCmdUp4 = Nothing
                    End If
                End Try
            End If


            Label1.Text = "Details have been removed successfully."


        Catch ex As Exception
            Label1.Text = "Issue in removing details. Please contact E-Dictate Support Team for more details." & Err.Description
        End Try

    End Sub
End Class
