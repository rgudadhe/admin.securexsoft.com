Imports MainModule
Partial Class HBALeaves_HBASendLeaveRequest
    Inherits BasePage
    Dim objMainModule As New MainModule
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim varDateTodaydate As Date
        Dim varBolCheckDayLight As Boolean
        Dim varStrDtFrom As String
        Dim varStrDtTo As String
        Dim varStrReason As String
        Dim varStrInsert As String
        Dim varStrUserName As String
        Dim objConn As New Data.SqlClient.SqlConnection
        objConn = objMainModule.OpenConnection(objConn)
        Try
            varBolCheckDayLight = objMainModule.CheckDayLightSavings(Now())
            If varBolCheckDayLight = True Then
                varDateTodaydate = DateAdd(DateInterval.Hour, 9, Now())
                varDateTodaydate = DateAdd(DateInterval.Minute, 30, varDateTodaydate)
            Else
                varDateTodaydate = DateAdd(DateInterval.Hour, 10, Now())
                varDateTodaydate = DateAdd(DateInterval.Minute, 30, varDateTodaydate)
            End If
            varStrDtFrom = DateSerial(Year(Request("txtStartDate")), Month(Request("txtStartDate")), Day(Request("txtStartDate")))
            varStrDtTo = DateSerial(Year(Request("txtEndDate")), Month(Request("txtEndDate")), Day(Request("txtEndDate")))
            varStrReason = Request("textArea1")

            Dim oCommandEmpInfo As New Data.SqlClient.SqlCommand("SELECT UserID,FirstName,LastName FROM DBO.tblUsers WHERE UserID='" & Session("UserID").ToString & "' AND ContractorID='" & Session("ContractorID").ToString & "'  ", objConn)
            Dim oRecEmpInfo As Data.SqlClient.SqlDataReader = oCommandEmpInfo.ExecuteReader()

            If oRecEmpInfo.HasRows Then
                While oRecEmpInfo.Read
                    varStrUserName = oRecEmpInfo.GetString(oRecEmpInfo.GetOrdinal("FirstName")) & " " & oRecEmpInfo.GetString(oRecEmpInfo.GetOrdinal("LastName"))
                End While
            End If
            oRecEmpInfo.Close()
            oRecEmpInfo = Nothing
            oCommandEmpInfo = Nothing

            varStrInsert = "INSERT INTO DBO.tblLeave(UserID,StartDate,EndDate,Reason,Status,AppDate) VALUES('" & Session("UserID") & "','" & DateSerial(Year(Request("txtStartDate")), Month(Request("txtStartDate")), Day(Request("txtStartDate"))) & "','" & DateSerial(Year(Request("txtEndDate")), Month(Request("txtEndDate")), Day(Request("txtEndDate"))) & "','" & Replace(Request("textArea1"), "'", "''") & "','Approved','" & varDateTodaydate & "')"
            Dim InsertCmd As New Data.SqlClient.SqlCommand
            InsertCmd.CommandType = Data.CommandType.Text
            InsertCmd.CommandText = varStrInsert
            InsertCmd.Connection = objConn
            InsertCmd.ExecuteNonQuery()
            InsertCmd = Nothing

            Dim varMailSubject As String
            Dim Text
            varMailSubject = "Leave Mail of " + varStrUserName + "(HBA)"
            Text = "<font face=""Trebuchet MS"" size=""2"" color=""#000080"">" & varStrUserName & " has register leave "
            Text = Text & " from " & varStrDtFrom & " to " & varStrDtTo & "<br>" & "Reason: " & varStrReason & "</FONT>"
            If objMainModule.SendMail(objMainModule.varStrHBAFromMail, objMainModule.varStrHBAToMail, "", varMailSubject, Text) Then
                Response.Write("<script type=""text/javascript"" language=javascript> alert(""Leave Register Sucessfully!!!"");window.location.href='MainPage.aspx';</script>")
            End If
        Catch ex As Exception
        Finally
            If objConn.State <> Data.ConnectionState.Closed Then
                objConn.Close()
                objConn = Nothing
            End If
        End Try
    End Sub
End Class
