Imports MainModule
Partial Class LeaveAttendanceMainNew_Employee_LeftRequestFrame
    Inherits BasePage
    Dim objMainModule As New MainModule
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim objConn As New Data.SqlClient.SqlConnection
        objConn = objMainModule.OpenConnection(objConn)
        Try
            'If Page.IsPostBack Then
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

            'Checking for Leave Without pay days
            Dim varCountLWP As Double = 0
            Dim objChk As New Data.SqlClient.SqlCommand("SELECT TypeOfLeave,datediff(d,StartDate,EndDate) +1 AS [Count] FROM DBO.tblLeave WHERE UserID='" & Session("UserID").ToString & "' AND (TypeOfLeave='LWP' OR TypeOfLeave='LWPHL') AND StartDate >='" & Month(Now) & "/1/" & Year(Now) & "' AND (IsDeleted IS NULL OR IsDeleted=0) ", objConn)
            Dim objRecChk As Data.SqlClient.SqlDataReader = objChk.ExecuteReader
            If objRecChk.HasRows Then
                While objRecChk.Read
                    If Not objRecChk.IsDBNull(objRecChk.GetOrdinal("TypeOfLeave")) Then
                        If Trim(UCase(objRecChk("TypeOfLeave"))) = Trim(UCase("LWP")) Then
                            varCountLWP = varCountLWP + objRecChk("Count")
                        ElseIf Trim(UCase(objRecChk("TypeOfLeave"))) = Trim(UCase("LWPHL")) Then
                            varCountLWP = varCountLWP + (objRecChk("Count") * 0.5)
                        End If
                    End If
                End While
            End If
            objRecChk.Close()
            objRecChk = Nothing
            objChk = Nothing

            If varCountLWP > 0 Then
                LWPRow.Visible = True
                lblLWP.Text = varCountLWP
            End If
            'end checking


            'End If

        Catch ex As Exception
        Finally
            If objConn.State <> Data.ConnectionState.Closed Then
                objConn.Close()
                objConn = Nothing
            End If
        End Try
    End Sub
End Class
