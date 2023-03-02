Imports MainModule
Partial Class TechReports_AddShift
    Inherits BasePage
    Dim objMainModule As New MainModule
    Protected Sub BtnSubmit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtnSubmit.Click
        Dim varShiftPrefix As String
        Dim varShiftName As String
        Dim varBolCheckPrefix As Boolean
        Dim varBolCheckShiftName As Boolean
        Dim varStrInsert As String

        Dim objConn As New Data.SqlClient.SqlConnection
        objConn = objMainModule.OpenConnection(objConn)

        Try
            varShiftPrefix = UCase(Trim(Request.Form("txtPrefix")))
            varShiftName = Trim(Request.Form("txtShiftName"))

            Dim oCommandCheck As New Data.SqlClient.SqlCommand("SELECT * FROM DBO.tblShift WHERE ShiftPrefix='" & varShiftPrefix & "'", objConn)
            Dim oRecCheck As Data.SqlClient.SqlDataReader = oCommandCheck.ExecuteReader()
            If oRecCheck.HasRows Then
                Response.Write("<script type=""text/javascript"" language=javascript> alert(""Prefix already exists"");window.location.href='AddShift.aspx';</script>")
                Exit Sub
            Else
                varBolCheckPrefix = True
            End If
            oRecCheck.Close()
            oRecCheck = Nothing
            oCommandCheck = Nothing

            Dim oCommandShiftNameCheck As New Data.SqlClient.SqlCommand("SELECT * FROM DBO.tblShift WHERE ShiftName='" & varShiftName & "'", objConn)
            Dim oRecShiftNameCheck As Data.SqlClient.SqlDataReader = oCommandShiftNameCheck.ExecuteReader()
            If oRecShiftNameCheck.HasRows Then
                Response.Write("<script type=""text/javascript"" language=javascript> alert(""ShiftName already exists"");window.location.href='AddShift.aspx';</script>")
                Exit Sub
            Else
                varBolCheckShiftName = True
            End If
            oRecShiftNameCheck.Close()
            oRecShiftNameCheck = Nothing
            oCommandShiftNameCheck = Nothing

            If varBolCheckPrefix = True And varBolCheckShiftName = True Then
                varStrInsert = "INSERT INTO DBO.tblShift(ShiftName,ShiftPrefix,UpdateBy,UpdateOn) VALUES('" & varShiftName & "','" & varShiftPrefix & "','" & Session("UserID") & "','" & Now() & "')"
                Dim InsertCmd As New Data.SqlClient.SqlCommand
                InsertCmd.CommandType = Data.CommandType.Text
                InsertCmd.CommandText = varStrInsert
                InsertCmd.Connection = objConn
                InsertCmd.ExecuteNonQuery()
                InsertCmd = Nothing

                Response.Write("<center><font face=""Trebuchet MS"" size=""2"" color=""#000080"">Shift added sucessfully !!!</font></center>")
                Response.Write("<center><a href=""../CloseWindow.aspx"">Close Window</a></center>")
                Response.End()
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
