Imports System.Data.SqlClient
Partial Class Billing_Reports_Default2
    Inherits BasePage

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try

        Dim strConn As String
            Dim strQuery As String
            Dim InvRecFound As Boolean
            Dim InvoiceID As String = ""
            InvRecFound = False
            strConn = System.Configuration.ConfigurationManager.AppSettings("ETSCon")

            strQuery = "Select InvoiceID from AdminSecureweb.dbo.tblBillAccounts where billAccID = '" & Request("BillAccID") & "' "
            Dim SQLCmd As New SqlCommand(strQuery, New SqlConnection(strConn))
            Try
                SQLCmd.Connection.Open()
                Dim DRRec As SqlDataReader = SQLCmd.ExecuteReader()
                If DRRec.HasRows = True Then
                    If DRRec.Read = True Then
                        If DRRec("InvoiceID").ToString = "" Then
                            InvRecFound = False
                        Else
                            InvoiceID = DRRec("InvoiceID").ToString
                            InvRecFound = True
                        End If
                    End If
                Else
                    InvRecFound = False
                End If
                DRRec.Close()
            Finally

                If SQLCmd.Connection.State = System.Data.ConnectionState.Open Then
                    SQLCmd.Connection.Close()
                    SQLCmd = Nothing
                End If
            End Try

            strQuery = "Delete from AdminSecureweb.dbo.tblBillAccounts where billAccID = '" & Request("BillAccID") & "' "
            Dim SQLCmdUp1 As New SqlCommand(strQuery, New SqlConnection(strConn))
            Try
                SQLCmdUp1.Connection.Open()
                SQLCmdUp1.ExecuteNonQuery()
            Finally

                If SQLCmdUp1.Connection.State = System.Data.ConnectionState.Open Then
                    SQLCmdUp1.Connection.Close()
                    SQLCmdUp1 = Nothing
                End If
            End Try

            ''Response.Write(strQuery)
            strQuery = "Delete from AdminSecureweb.dbo.tblAccBillDetails where billAccID = '" & Request("BillAccID") & "' "
            Dim SQLCmdUp2 As New SqlCommand(strQuery, New SqlConnection(strConn))
            Try
                SQLCmdUp2.Connection.Open()
                SQLCmdUp2.ExecuteNonQuery()
            Finally

                If SQLCmdUp2.Connection.State = System.Data.ConnectionState.Open Then
                    SQLCmdUp2.Connection.Close()
                    SQLCmdUp2 = Nothing
                End If
            End Try

            strQuery = "Delete from AdminETS.dbo.tblBillingLines where billAccID = '" & Request("BillAccID") & "' "
            Dim SQLCmdUp3 As New SqlCommand(strQuery, New SqlConnection(strConn))
            Try
                SQLCmdUp3.Connection.Open()
                SQLCmdUp3.ExecuteNonQuery()
            Finally

                If SQLCmdUp3.Connection.State = System.Data.ConnectionState.Open Then
                    SQLCmdUp3.Connection.Close()
                    SQLCmdUp3 = Nothing
                End If
            End Try


            If InvRecFound = True Then

                strQuery = "Delete from AdminSecureweb.dbo.tblInvItemdet where mode='Trans' and InvoiceID = '" & InvoiceID & "' "
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

                strQuery = "update AdminSecureweb.dbo.tblInvItemdet set InvoiceID= '11111111-1111-1111-1111-111111111111'  where mode='VAS' and InvoiceID = '" & InvoiceID & "' "
                Dim SQLCmdUp6 As New SqlCommand(strQuery, New SqlConnection(strConn))
                Try
                    SQLCmdUp6.Connection.Open()
                    SQLCmdUp6.ExecuteNonQuery()
                Finally

                    If SQLCmdUp6.Connection.State = System.Data.ConnectionState.Open Then
                        SQLCmdUp6.Connection.Close()
                        SQLCmdUp6 = Nothing
                    End If
                End Try

                strQuery = "Delete from AdminSecureweb.dbo.tblInvItemdet where itemid ='25c7d577-967e-48ab-a62d-87fb6f420be1'  and InvoiceID = '" & InvoiceID & "' "
                Dim SQLCmdUp7 As New SqlCommand(strQuery, New SqlConnection(strConn))
                Try
                    SQLCmdUp7.Connection.Open()
                    SQLCmdUp7.ExecuteNonQuery()
                Finally

                    If SQLCmdUp7.Connection.State = System.Data.ConnectionState.Open Then
                        SQLCmdUp7.Connection.Close()
                        SQLCmdUp7 = Nothing
                    End If
                End Try

                strQuery = "Delete from AdminSecureweb.dbo.Invupdata where TrackID = '" & InvoiceID & "' "
                Dim SQLCmdUp5 As New SqlCommand(strQuery, New SqlConnection(strConn))
                Try
                    SQLCmdUp5.Connection.Open()
                    SQLCmdUp5.ExecuteNonQuery()
                Finally

                    If SQLCmdUp5.Connection.State = System.Data.ConnectionState.Open Then
                        SQLCmdUp5.Connection.Close()
                        SQLCmdUp5 = Nothing
                    End If
                End Try

            Else

            End If

            Label1.Text = "Details have been removed successfully."


        Catch ex As Exception
            Label1.Text = "Issue in removing details. Please contact E-Dictate Support Team for more details." & Err.Description
        End Try

    End Sub
End Class
