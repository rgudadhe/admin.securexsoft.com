Imports MainModule
Partial Class LeaveAttendanceMainNew_Employee_LeftRequestFrame
    Inherits BasePage
    Dim objMainModule As New MainModule
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'If Page.IsPostBack Then
        Dim objConn As New Data.SqlClient.SqlConnection
        objConn = objMainModule.OpenConnection(objConn)
        Try
            Dim objCmd As New Data.SqlClient.SqlCommand("SELECT CL,EL,TL FROM DBO.tblLeaveBalance WHERE UserID='" & Session("UserID") & "'", objConn)
            Dim objRec As Data.SqlClient.SqlDataReader = objCmd.ExecuteReader()
            If objRec.HasRows Then
                While objRec.Read
                    If Not objRec.IsDBNull(objRec.GetOrdinal("CL")) Then
                        lblCL.Text = objRec.GetDouble(objRec.GetOrdinal("CL"))
                    Else
                        lblCL.Text = 0
                    End If
                    If Not objRec.IsDBNull(objRec.GetOrdinal("EL")) Then
                        lblEL.Text = objRec.GetDouble(objRec.GetOrdinal("EL"))
                    Else
                        lblEL.Text = 0
                    End If
                    If Not objRec.IsDBNull(objRec.GetOrdinal("TL")) Then
                        lblTL.Text = objRec.GetDouble(objRec.GetOrdinal("TL"))
                    Else
                        lblTL.Text = 0
                    End If
                End While
            Else
                lblCL.Text = 0
                lblEL.Text = 0
                lblTL.Text = 0
            End If
            objRec.Close()
            objRec = Nothing
            objCmd = Nothing
        Catch ex As Exception
        Finally
            If objConn.State <> Data.ConnectionState.Closed Then
                objConn.Close()
                objConn = Nothing
            End If
        End Try
        'End If
    End Sub
End Class
