Imports System.Data.SqlClient
Imports System.Data
Partial Class Billing_Reports_Default2
    Inherits BasePage

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'Response.Write("ok")
        '  Response.Write(Request("BillAccID"))
        Dim splt() As String = Split(Request("BillAccID"), ",")
        Dim ErrFound As Boolean = False
        Dim ErrMessage As String = String.Empty
        Dim strConn As String
        strConn = System.Configuration.ConfigurationManager.AppSettings("ETSCon")
        Dim myConnection As New SqlConnection(strConn)
        Dim MyTransAttr As SqlTransaction
        Dim cmdUp As New SqlCommand()
        myConnection.Open()
        'EventLog1.WriteEntry("Attr3")
        MyTransAttr = myConnection.BeginTransaction()
        'EventLog1.WriteEntry("Attr5")
        cmdUp.Connection = myConnection
        cmdUp.Transaction = MyTransAttr
        cmdUp.CommandTimeout = 600
        For i As Integer = 0 To UBound(splt)
            Dim BillAccID As String = String.Empty
            BillAccID = splt(i)
            If Not String.IsNullOrEmpty(BillAccID) Then
                Try

                    Dim strQuery As String
                    Dim InvRecFound As Boolean
                    Dim InvoiceID As String = String.Empty
                    InvRecFound = False

                    strQuery = "Select InvoiceID from AdminSecureweb.dbo.tblBillAccounts where billAccID = '" & BillAccID & "' "
                    Dim SQLCmd As New SqlCommand(strQuery, New SqlConnection(strConn))
                    SQLCmd.CommandTimeout = 600
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
                    Catch ex As Exception
                        ErrMessage = "err1" & ex.Message
                        ErrFound = True
                        Exit For
                    Finally

                        If SQLCmd.Connection.State = System.Data.ConnectionState.Open Then
                            SQLCmd.Connection.Close()
                            SQLCmd = Nothing
                        End If
                    End Try

                    strQuery = "Delete from AdminSecureweb.dbo.tblBillAccounts where billAccID = '" & BillAccID & "' "
                    cmdUp.CommandText = strQuery
                    cmdUp.CommandType = CommandType.Text
                    cmdUp.ExecuteNonQuery()

                    strQuery = "Delete from AdminSecureweb.dbo.tblAccBillDetails where billAccID = '" & BillAccID & "' "
                    cmdUp.CommandText = strQuery
                    cmdUp.CommandType = CommandType.Text
                    cmdUp.ExecuteNonQuery()

                    strQuery = "Delete from AdminETS.dbo.tblBillingLines where billAccID = '" & BillAccID & "' "
                    cmdUp.CommandText = strQuery
                    cmdUp.CommandType = CommandType.Text
                    cmdUp.ExecuteNonQuery()


                    If InvRecFound = True Then

                        strQuery = "Delete from AdminSecureweb.dbo.tblInvItemdet where mode='Trans' and InvoiceID = '" & InvoiceID & "' "
                        cmdUp.CommandText = strQuery
                        cmdUp.CommandType = CommandType.Text
                        cmdUp.ExecuteNonQuery()

                        strQuery = "Delete from AdminSecureweb.dbo.tblInvItemdet where itemid ='25c7d577-967e-48ab-a62d-87fb6f420be1'  and InvoiceID = '" & InvoiceID & "' "
                        cmdUp.CommandText = strQuery
                        cmdUp.CommandType = CommandType.Text
                        cmdUp.ExecuteNonQuery()

                        strQuery = "update AdminSecureweb.dbo.tblInvItemdet set InvoiceID= '11111111-1111-1111-1111-111111111111'  where mode='VAS' and InvoiceID = '" & InvoiceID & "' "
                        cmdUp.CommandText = strQuery
                        cmdUp.CommandType = CommandType.Text
                        cmdUp.ExecuteNonQuery()


                        strQuery = "Delete from AdminSecureweb.dbo.Invupdata where TrackID = '" & InvoiceID & "' "
                        cmdUp.CommandText = strQuery
                        cmdUp.CommandType = CommandType.Text
                        cmdUp.ExecuteNonQuery()


                    End If




                Catch ex As Exception
                    ErrMessage = "err2#" & BillAccID & "#" & ex.Message
                    ErrFound = True
                    Exit For
                End Try
            End If
        Next
        If ErrFound = False Then
            MyTransAttr.Commit()
            Label1.Text = "Details have been removed successfully."

        Else
            MyTransAttr.Rollback()
            Label1.Text = "Issue in removing details. Please contact Technical Support Team for more details." & ErrMessage
        End If

    End Sub

End Class
