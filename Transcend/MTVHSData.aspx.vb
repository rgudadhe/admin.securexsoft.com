
Partial Class Transcend_MTVHSData
    Inherits BasePage

    Protected Sub btnUpload_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnUpload.Click
        lblResponse.Text = String.Empty
        ErrLabel.Text = String.Empty
        If CheckExcel() Then
            Dim varFileName As String = String.Empty
            Dim varFileUploadPath As String = String.Empty

            Dim dtFormat = Format(Month(Now), "00") & "-" & Format(Day(Now), "00") & "-" & Format(Year(Now), "00") & " " & Format(Hour(Now), "00") & "-" & Format(Minute(Now), "00")
            varFileUploadPath = Server.MapPath("/ets_Files/") & "\Transcend\MTVHSData\"

            Dim varTempDir As New IO.DirectoryInfo(varFileUploadPath)
            If varTempDir.Exists = False Then
                varTempDir.Create()
            End If
            varFileName = varFileUploadPath & "\" & Session("UserName").ToString & "_" & dtFormat.ToString & "_" & FileUpload.FileName
            FileUpload.PostedFile.SaveAs(varFileName)
            UpdateData(varFileName)
        End If
    End Sub
    Protected Function CheckExcel() As Boolean
        Try
            Dim varfileName As String = Server.HtmlEncode(FileUpload.FileName)
            Dim extension As String = System.IO.Path.GetExtension(varfileName)
            If (FileUpload.HasFile) Then
                If Trim(UCase(extension)) = Trim(UCase(".xls")) Or Trim(UCase(extension)) = Trim(UCase(".xlsx")) Or Trim(UCase(extension)) = Trim(UCase(".csv")) Then
                    Return True
                Else
                    ErrLabel.Text = "Please upload Only Excel document"
                    Return False
                End If
            Else
                ErrLabel.Text = "Please upload Only Excel document"
                Return False
            End If
        Catch ex As Exception
        End Try
    End Function
    Protected Sub UpdateData(ByVal varFileName As String)
        Dim myConnectionString As String
        Dim varBolFormat As Boolean = True
        Dim varCountO As Long
        Dim varCount As Long
        Dim varJobNumberList As New StringBuilder
        Dim varUserIDNotExist As New StringBuilder

        Dim oConn As New Data.SqlClient.SqlConnection
        oConn.ConnectionString = System.Configuration.ConfigurationManager.AppSettings("ETSCon")

        Dim oleDBConnection As New System.Data.OleDb.OleDbConnection
        Try
            myConnectionString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" & varFileName & ";Extended Properties=Excel 8.0;"
            oleDBConnection.ConnectionString = myConnectionString

            Dim myDataAdapter As New System.Data.OleDb.OleDbDataAdapter("SELECT * FROM [Sheet1$]", oleDBConnection)

            Dim myDataSet As New System.Data.DataSet()
            myDataAdapter.Fill(myDataSet, "ExcelInfo")

            Dim varIntRows, varIntCols As Integer

            varIntRows = myDataSet.Tables(0).Rows.Count
            varIntCols = myDataSet.Tables(0).Columns.Count

            varCountO = varIntRows

            If varIntCols = 4 Then
                If Not Trim(UCase(myDataSet.Tables(0).Columns(0).Caption.ToString)) = Trim(UCase("MT ID")) Then
                    varBolFormat = False

                    ErrLabel.Text = String.Empty
                    ErrLabel.Text = "1Template format mistmatch,please refer template "
                    Exit Sub
                ElseIf Not Trim(UCase(myDataSet.Tables(0).Columns(1).Caption.ToString)) = Trim(UCase("Job Number")) Then
                    varBolFormat = False

                    ErrLabel.Text = String.Empty
                    ErrLabel.Text = "2Template format mistmatch,please refer template "
                    Exit Sub
                ElseIf Not Trim(UCase(myDataSet.Tables(0).Columns(2).Caption.ToString)) = Trim(UCase("Lines")) Then
                    varBolFormat = False

                    ErrLabel.Text = String.Empty
                    ErrLabel.Text = "3Template format mistmatch,please refer template "
                    Exit Sub
                ElseIf Not Trim(UCase(myDataSet.Tables(0).Columns(3).Caption.ToString)) = Trim(UCase("Transcription Date")) Then
                    varBolFormat = False

                    ErrLabel.Text = String.Empty
                    ErrLabel.Text = "4Template format mistmatch,please refer template "
                    Exit Sub
                End If
            
                If varBolFormat Then
                    oConn.Open()

                    For varIntRows = 0 To varIntRows - 1
                        Dim varStrInsert As String = String.Empty
                        Dim varStrValues As String = String.Empty
                        Dim varStrUserID As String = String.Empty

                        varStrUserID = GetUserID(Trim(myDataSet.Tables(0).Rows(varIntRows).Item(0).ToString))

                        If Not String.IsNullOrEmpty(varStrUserID.ToString) Then
                            If IsJobNumberExist(Trim(myDataSet.Tables(0).Rows(varIntRows).Item(1).ToString)) Then
                                If String.IsNullOrEmpty(varJobNumberList.ToString) Then
                                    varJobNumberList.Append(Trim(myDataSet.Tables(0).Rows(varIntRows).Item(1).ToString))
                                Else
                                    varJobNumberList.Append("," & Trim(myDataSet.Tables(0).Rows(varIntRows).Item(1).ToString))
                                End If
                            Else
                                varStrInsert = "(UserID,MTID,JobNumber,Lines,TranscriptionDate"
                                varStrValues = "('" & varStrUserID & "','" & myDataSet.Tables(0).Rows(varIntRows).Item(0).ToString & "'," & Trim(myDataSet.Tables(0).Rows(varIntRows).Item(1).ToString) & ",'" & Trim(myDataSet.Tables(0).Rows(varIntRows).Item(2).ToString) & "','" & CDate(Trim(myDataSet.Tables(0).Rows(varIntRows).Item(3).ToString)) & "'"

                                If Not String.IsNullOrEmpty(varStrInsert) Then
                                    varStrInsert = varStrInsert & ",UpdatedBy,UpdatedOn)"
                                End If
                                If Not String.IsNullOrEmpty(varStrValues) Then
                                    varStrValues = varStrValues & ",'" & Session("UserID").ToString & "','" & Now() & "')"
                                End If

                                If Not String.IsNullOrEmpty(varStrInsert) And Not String.IsNullOrEmpty(varStrValues) Then
                                    Try
                                        Dim objCmd As New Data.SqlClient.SqlCommand("INSERT INTO Transcend.dbo.tblMTVHSData " & varStrInsert & " VALUES " & varStrValues, oConn)

                                        If objCmd.ExecuteNonQuery() > 0 Then
                                            varCount = varCount + 1
                                        End If

                                        objCmd = Nothing
                                    Catch ex As Exception
                                        Response.Write(ex.Message)
                                    End Try
                                End If
                            End If
                        Else
                            If String.IsNullOrEmpty(varUserIDNotExist.ToString) Then
                                varUserIDNotExist.Append(Trim(myDataSet.Tables(0).Rows(varIntRows).Item(0).ToString))
                            Else
                                varUserIDNotExist.Append("," & Trim(myDataSet.Tables(0).Rows(varIntRows).Item(0).ToString))
                            End If
                        End If
                    Next
                End If
            Else
                ErrLabel.Text = String.Empty
                ErrLabel.Text = "Template format mistmatch,please refer template "
                Exit Sub
            End If
            lblResponse.Text = String.Empty
            lblResponse.Text = varCount & " records updated out of " & varCountO & IIf(String.IsNullOrEmpty(varUserIDNotExist.ToString), String.Empty, "<BR>User Name not found : " & varUserIDNotExist.ToString) & IIf(String.IsNullOrEmpty(varJobNumberList.ToString), String.Empty, "<BR>Job Number already exist : " & varJobNumberList.ToString)
            myDataAdapter.Dispose()
        Catch ex As Exception
            Response.Write(ex.Message)
        Finally
            If oConn.State <> Data.ConnectionState.Closed Then
                oConn.Close()
                oConn = Nothing
            End If
            If oleDBConnection.State <> Data.ConnectionState.Closed Then
                oleDBConnection.Close()
                oleDBConnection = Nothing
            End If
        End Try
    End Sub
    Protected Function GetUserID(ByVal UserName As String) As String
        Dim varReturn As String = String.Empty
        Dim objCmd As New Data.SqlClient.SqlCommand("SELECT UserID FROM DBO.tblUsers WHERE UserName='" & UserName.ToString & "'", New Data.SqlClient.SqlConnection(System.Configuration.ConfigurationManager.AppSettings("ETSCon")))
        Try
            objCmd.Connection.Open()
            Dim oRec As Data.SqlClient.SqlDataReader = objCmd.ExecuteReader
            If oRec.HasRows Then
                While oRec.Read
                    varReturn = oRec("UserID").ToString
                End While
            End If
        Catch ex As Exception
            Response.Write(ex.Message)
        Finally
            If objCmd.Connection.State <> Data.ConnectionState.Closed Then
                objCmd.Connection.Close()
                objCmd.Connection = Nothing
            End If
        End Try
        Return varReturn
    End Function
    Protected Function IsJobNumberExist(ByVal varJobNumber As String) As Boolean
        Dim varReturn As Boolean = False
        Dim objCmd As New Data.SqlClient.SqlCommand("SELECT Count(*) as 'Count' FROM TRANSCEND.Dbo.tblMTVHSData WHERE JobNumber='" & varJobNumber.ToString & "'", New Data.SqlClient.SqlConnection(System.Configuration.ConfigurationManager.AppSettings("ETSCon")))
        Try
            objCmd.Connection.Open()
            Dim oRec As Data.SqlClient.SqlDataReader = objCmd.ExecuteReader
            If oRec.HasRows Then
                While oRec.Read
                    If oRec("Count") > 0 Then
                        varReturn = True
                    End If
                End While
            End If
        Catch ex As Exception
            Response.Write(ex.Message)
        Finally
            If objCmd.Connection.State <> Data.ConnectionState.Closed Then
                objCmd.Connection.Close()
                objCmd.Connection = Nothing
            End If
        End Try
        Return varReturn
    End Function
End Class
