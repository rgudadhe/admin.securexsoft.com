Imports System.DateTime
Imports System.TimeSpan
Partial Class EmpInOut
    Inherits System.Web.UI.Page
    Dim objMainModule As New MainModule
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'Response.Write(Request.QueryString("UserID"))
        'Response.Write(Request.QueryString("AttDate"))

        Dim varStrEmpName As String
        Dim varStrTotalBreakTime As String
        Dim varStrInTime As String = String.Empty
        Dim varStrOutTime As String = String.Empty
        Dim varTblRow As New TableRow
        Dim varTblRowIn As New TableRow
        Dim varTblRowOut As New TableRow
        Dim varTblRowBreak As New TableRow
        Dim varTblRowTotalWorkedTime As New TableRow
        Dim varTblCellName As New TableCell
        Dim varTblCellAttDate As New TableCell
        Dim varTimeSTime As DateTime
        Dim varTimeETime As DateTime
        Dim varTempTime As DateTime
        Dim varTempTimeSpan As TimeSpan
        Dim varTimeSpan As TimeSpan
        Dim varBolFlag As Boolean

        Dim objConn As New Data.SqlClient.SqlConnection
        objConn = objMainModule.OpenConnection(objConn)

        Try
            Dim oCommandAttTime As New Data.SqlClient.SqlCommand("SELECT U.FirstName,U.LastName,A.SignIn,A.SignOut FROM DBO.tblAttendance A INNER JOIN DBO.tblUsers U ON A.UserID = U.UserID WHERE A.UserID='" & Request.QueryString("UserID") & "' AND AttDate='" & Request.QueryString("AttDate") & "'", objConn)
            Dim oRecAttTime As Data.SqlClient.SqlDataReader = oCommandAttTime.ExecuteReader()

            If oRecAttTime.HasRows Then
                While oRecAttTime.Read
                    varStrInTime = oRecAttTime(oRecAttTime.GetOrdinal("SignIn"))
                    varTimeSTime = oRecAttTime(oRecAttTime.GetOrdinal("SignIn"))
                    If Not oRecAttTime.IsDBNull(oRecAttTime.GetOrdinal("SignOut")) Then
                        varStrOutTime = oRecAttTime(oRecAttTime.GetOrdinal("SignOut"))
                        If Trim(UCase(varStrOutTime)) = Trim(UCase("OUT")) Then
                            varTimeETime = DateAdd(DateInterval.Hour, 8, CDate(oRecAttTime(oRecAttTime.GetOrdinal("SignIn"))))
                        Else
                            varTimeETime = oRecAttTime(oRecAttTime.GetOrdinal("SignOut"))
                        End If
                        varBolFlag = True
                    Else
                        varBolFlag = False
                    End If
                    varStrEmpName = oRecAttTime(oRecAttTime.GetOrdinal("FirstName")) & " " & oRecAttTime(oRecAttTime.GetOrdinal("LastName"))
                End While
            End If

            oRecAttTime.Close()
            oRecAttTime = Nothing
            oCommandAttTime = Nothing

            If varBolFlag Then
                varTempTimeSpan = varTimeETime.Subtract(varTimeSTime)
                varTempTime = varTempTimeSpan.ToString
            End If




            'Response.Write(varTempTimeSpan)
            'Response.Write("<BR>")

            varTblCellName.Text = "Employee Name : " & varStrEmpName
            varTblCellName.ForeColor = Drawing.Color.Red
            'varTblCellName.Font.Bold = True

            varTblCellAttDate.Text = "Attendance Date :" & Request.QueryString("AttDate")
            varTblCellAttDate.ForeColor = Drawing.Color.Red
            'varTblCellAttDate.Font.Bold = True

            varTblRow.Cells.Add(varTblCellName)
            varTblRow.Cells.Add(varTblCellAttDate)

            Table1.Rows.Add(varTblRow)

            Dim varTblCellInText As New TableCell
            Dim varTblCellIn As New TableCell
            Dim varTblCellOutText As New TableCell
            Dim varTblCellOut As New TableCell
            Dim varTblCellBreakText As New TableCell
            Dim varTblCellBreak As New TableCell
            Dim varTblCellTotalWorkText As New TableCell
            Dim varTblCellTotalWork As New TableCell

            varTblCellInText.Text = "In Time "
            varTblCellIn.Text = varStrInTime

            varTblRowIn.Cells.Add(varTblCellInText)
            varTblRowIn.Cells.Add(varTblCellIn)

            Table1.Rows.Add(varTblRowIn)

            varTblCellOutText.Text = "Out Time "
            
            varTblCellOut.Text = IIf(String.IsNullOrEmpty(varStrOutTime), "&nbsp;", varStrOutTime)

            varTblRowOut.Cells.Add(varTblCellOutText)
            varTblRowOut.Cells.Add(varTblCellOut)

            Table1.Rows.Add(varTblRowOut)

            Dim oCommandSecDuration As New Data.SqlClient.SqlCommand("SELECT Sum(DateDiff(s,sTime,eTime)) As TotalSec FROM DBO.tblBreak WHERE UserID='" & Request.QueryString("UserID") & "' AND AttDate='" & Request.QueryString("AttDate") & "'", objConn)
            Dim oRecSecDuration As Data.SqlClient.SqlDataReader = oCommandSecDuration.ExecuteReader()

            If oRecSecDuration.HasRows Then
                While oRecSecDuration.Read
                    If Not oRecSecDuration.IsDBNull(oRecSecDuration.GetOrdinal("TotalSec")) Then
                        varStrTotalBreakTime = Duration(oRecSecDuration(oRecSecDuration.GetOrdinal("TotalSec")))
                    Else
                        varStrTotalBreakTime = "00:00:00"
                    End If
                End While
            End If

            oRecSecDuration.Close()
            oRecSecDuration = Nothing
            oCommandSecDuration = Nothing

            If varBolFlag Then
                varTimeSpan = varTempTime.Subtract(varStrTotalBreakTime)
            End If

            'Response.Write(varTimeSpan)
            'Response.Write("<BR>")

            varTblCellBreakText.Text = "Total Break Time (HH:MM:SS)"
            varTblCellBreak.Text = varStrTotalBreakTime

            varTblRowBreak.Cells.Add(varTblCellBreakText)
            varTblRowBreak.Cells.Add(varTblCellBreak)

            'Table1.Rows.Add(varTblRowBreak)

            varTblCellTotalWorkText.Text = "Total Worked (HH:MM:SS)"
            varTblCellTotalWork.Text = varTimeSpan.ToString

            varTblRowTotalWorkedTime.Cells.Add(varTblCellTotalWorkText)
            varTblRowTotalWorkedTime.Cells.Add(varTblCellTotalWork)

            Table1.Rows.Add(varTblRowTotalWorkedTime)
        Catch ex As Exception
        Finally
            If objConn.State <> Data.ConnectionState.Closed Then
                objConn.Close()
                objConn = Nothing
            End If
        End Try
    End Sub
    Function Duration(ByVal Sec)
        Dim hours, minutes, seconds

        If Sec <= 0 Then
            Duration = "00:00:00"
            Exit Function
        End If
        hours = 0

        seconds = Math.Abs(Sec)
        minutes = Sec / 60


        If minutes > 60 Then
            hours = minutes / 60
        Else
            hours = "00"
        End If


        hours = Int(hours)
        minutes = Int(minutes) Mod 60
        seconds = seconds Mod 60


        If hours > 0 Then
            If Len(hours) = 1 Then
                If hours > 0 Then
                    hours = Int("0") & hours
                End If
            End If
        Else
            hours = "00"
        End If

        Duration = hours & ":" & Microsoft.VisualBasic.Right("00" & minutes, 2) & ":" & Microsoft.VisualBasic.Right("00" & seconds, 2)

    End Function
End Class
