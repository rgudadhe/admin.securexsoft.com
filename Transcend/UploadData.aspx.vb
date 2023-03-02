Imports System.Data.SqlClient
Imports System.Data
Partial Class Transcend_MTVHSData1
    Inherits BasePage

    Protected Sub btnUpload_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnUpload.Click
        lblResponse.Text = String.Empty
        ErrLabel.Text = String.Empty
        If CheckExcel() Then
            Dim varFileName As String = String.Empty
            Dim varFileUploadPath As String = String.Empty

            Dim dtFormat = Format(Month(Now), "00") & "-" & Format(Day(Now), "00") & "-" & Format(Year(Now), "00") & " " & Format(Hour(Now), "00") & "-" & Format(Minute(Now), "00")
            varFileUploadPath = Server.MapPath("/ets_Files/") & "\Transcend\EScription\"

            Dim varTempDir As New IO.DirectoryInfo(varFileUploadPath)
            If varTempDir.Exists = False Then
                varTempDir.Create()
            End If
            varFileName = varFileUploadPath & "\" & Session("UserName").ToString & "_" & dtFormat.ToString & "_" & FileUpload.FileName
            FileUpload.PostedFile.SaveAs(varFileName)
            Response.Write("Data uploaded")
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
        Catch ex As exception
        End Try
    End Function
    Protected Function DeleteRecords() As Boolean
        Dim oConn As New Data.SqlClient.SqlConnection
        oConn.ConnectionString = System.Configuration.ConfigurationManager.AppSettings("ETSCon")
        Dim varReturn As Boolean = False
        Try
            oConn.Open()


            Dim objCmd1 As New Data.SqlClient.SqlCommand("DELETE Transcend.dbo.tblESCription WHERE Month(DateTranscribed) = '" & DLMonth1.SelectedValue & "' and Year(DateTranscribed) = '" & DLYear1.SelectedValue & "' ", oConn)
            objCmd1.ExecuteNonQuery()


        Catch ex As Exception

        Finally
            If oConn.State <> Data.ConnectionState.Closed Then
                oConn.Close()
                oConn = Nothing
            End If
        End Try
        Return varReturn
    End Function
    Protected Sub UpdateData(ByVal varFileName As String)
        'If DeleteRecords() = False Then
        '    Exit Sub
        'End If
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
            Response.Write(varIntCols)
            If varIntCols = 59 Then
                If Not Trim(UCase(myDataSet.Tables(0).Columns(0).Caption.ToString)) = Trim(UCase("Dictation ID")) Then
                    varBolFormat = False

                    ErrLabel.Text = String.Empty
                    ErrLabel.Text = "1Template format mistmatch,please refer template "
                    Exit Sub
                ElseIf Not Trim(UCase(myDataSet.Tables(0).Columns(3).Caption.ToString)) = Trim(UCase("Date Transcribed")) Then
                    varBolFormat = False

                    ErrLabel.Text = String.Empty
                    ErrLabel.Text = "2Template format mistmatch,please refer template "
                    Exit Sub
                ElseIf Not Trim(UCase(myDataSet.Tables(0).Columns(9).Caption.ToString)) = Trim(UCase("Transcriptionist")) Then
                    varBolFormat = False

                    ErrLabel.Text = String.Empty
                    ErrLabel.Text = "3Template format mistmatch,please refer template "
                    Exit Sub
                ElseIf Not Trim(UCase(myDataSet.Tables(0).Columns(18).Caption.ToString)) = Trim(UCase("Line Count")) Then
                    varBolFormat = False

                    ErrLabel.Text = String.Empty
                    ErrLabel.Text = "4Template format mistmatch,please refer template "
                    Exit Sub
                End If

                If varBolFormat Then
                    oConn.Open()

                    For varIntRows = 0 To varIntRows - 1
                        If Not myDataSet.Tables(0).Rows(varIntRows).Item(1).ToString = String.Empty And InStr(myDataSet.Tables(0).Rows(varIntRows).Item(9).ToString.ToUpper, "MOP") > 0 Then
                            Dim varStrInsert As String = String.Empty
                            Dim varStrValues As String = String.Empty
                            Dim varStrUserID As String = String.Empty
                            Dim DictationID As String = String.Empty
                            Dim DictationDuration As String = String.Empty
                            Dim DateDictated As Date
                            Dim DateTranscribed As Date
                            Dim DateTimeTranscribed As Date
                            Dim TurnAroundTime As String = String.Empty
                            Dim MTTurnAroundTime As String = String.Empty
                            Dim TranscriptionDuration As String = String.Empty
                            Dim TranscriptionRate As String = String.Empty
                            Dim Transcriptionist As String = String.Empty
                            Dim WorkType As String = String.Empty
                            Dim Provider As String = String.Empty
                            Dim Specialty As String = String.Empty
                            Dim DictationRate As String = String.Empty
                            Dim TranscriptionMethod As String = String.Empty
                            Dim TranscriptionGroup As String = String.Empty
                            Dim SpeakerCode As String = String.Empty
                            Dim TranscriptionistLogin As String = String.Empty
                            Dim LineCount As Double
                            Dim BusinessEntity As String = String.Empty
                            Dim BusinessEntityLevel1 As String = String.Empty
                            Dim BusinessEntityLevel2 As String = String.Empty
                            Dim admitDate As Date
                            Dim dischargeDate As Date
                            Dim dictated As Date
                            Dim tatEnd As Date
                            Dim problemStart1 As Date
                            Dim enteredBy1 As String = String.Empty
                            Dim problemEnd1 As Date
                            Dim actionBy1 As String = String.Empty
                            Dim problemStart2 As Date
                            Dim enteredBy2 As String = String.Empty
                            Dim problemEnd2 As Date
                            Dim actionBy2 As String = String.Empty
                            Dim problemStart3 As Date
                            Dim enteredBy3 As String = String.Empty
                            Dim problemEnd3 As Date
                            Dim actionBy3 As String = String.Empty
                            Dim problemStart4 As Date
                            Dim enteredBy4 As String = String.Empty
                            Dim problemEnd4 As Date
                            Dim actionBy4 As String = String.Empty
                            Dim problemStart5 As Date
                            Dim enteredBy5 As String = String.Empty
                            Dim problemEnd5 As Date
                            Dim actionBy5 As String = String.Empty
                            Dim DictationhasbeenPended As String
                            Dim AdmitToDictationTime As String
                            Dim TranscribedToPendTime As String = String.Empty
                            Dim AdmitToTatEnd As String = String.Empty
                            Dim ExtendedTAT As String = String.Empty
                            Dim MTExtendedTAT As String = String.Empty
                            Dim BusinessEntityDataField1 As String = String.Empty
                            Dim BusinessEntityDataField2 As String = String.Empty
                            Dim BusinessEntityDataField3 As String = String.Empty
                            Dim utteranceGroupID As String = String.Empty
                            Dim sequenceNumber As String = String.Empty
                            Dim priority As String = String.Empty
                            Dim Account As String = String.Empty
                            DictationID = myDataSet.Tables(0).Rows(varIntRows).Item(0).ToString
                            IsJobNumberExist(DictationID)
                            varStrValues = varStrValues & "('" & DictationID & "'"

                            DictationDuration = myDataSet.Tables(0).Rows(varIntRows).Item(1).ToString.Replace("'", "''")
                            varStrValues = varStrValues & ",'" & DictationDuration & "'"

                            If IsDate(myDataSet.Tables(0).Rows(varIntRows).Item(2).ToString) Then
                                DateDictated = myDataSet.Tables(0).Rows(varIntRows).Item(2).ToString
                                varStrValues = varStrValues & ",'" & DateDictated & "'"
                            Else
                                varStrValues = varStrValues & ",NULL"
                            End If

                            If IsDate(myDataSet.Tables(0).Rows(varIntRows).Item(3).ToString) Then
                                DateTranscribed = myDataSet.Tables(0).Rows(varIntRows).Item(3).ToString
                                varStrValues = varStrValues & ",'" & DateTranscribed & "'"
                            Else
                                varStrValues = varStrValues & ",NULL"
                            End If
                            If IsDate(myDataSet.Tables(0).Rows(varIntRows).Item(4).ToString) Then
                                DateTimeTranscribed = myDataSet.Tables(0).Rows(varIntRows).Item(4).ToString
                                varStrValues = varStrValues & ",'" & DateTimeTranscribed & "'"
                            Else
                                varStrValues = varStrValues & ",NULL"
                            End If

                            TurnAroundTime = myDataSet.Tables(0).Rows(varIntRows).Item(5).ToString
                            varStrValues = varStrValues & ",'" & TurnAroundTime & "'"

                            MTTurnAroundTime = myDataSet.Tables(0).Rows(varIntRows).Item(6).ToString
                            varStrValues = varStrValues & ",'" & MTTurnAroundTime & "'"

                            TranscriptionDuration = myDataSet.Tables(0).Rows(varIntRows).Item(7).ToString
                            varStrValues = varStrValues & ",'" & TranscriptionDuration & "'"

                            TranscriptionRate = myDataSet.Tables(0).Rows(varIntRows).Item(8).ToString
                            varStrValues = varStrValues & ",'" & TranscriptionRate & "'"

                            Transcriptionist = myDataSet.Tables(0).Rows(varIntRows).Item(9).ToString.Replace("'", "''")
                            varStrValues = varStrValues & ",'" & Transcriptionist & "'"

                            WorkType = myDataSet.Tables(0).Rows(varIntRows).Item(10).ToString.Replace("'", "''")
                            varStrValues = varStrValues & ",'" & WorkType & "'"

                            Provider = myDataSet.Tables(0).Rows(varIntRows).Item(11).ToString.Replace("'", "''")
                            varStrValues = varStrValues & ",'" & Provider & "'"

                            Specialty = myDataSet.Tables(0).Rows(varIntRows).Item(12).ToString.Replace("'", "''")
                            varStrValues = varStrValues & ",'" & Specialty & "'"

                            DictationRate = myDataSet.Tables(0).Rows(varIntRows).Item(13).ToString
                            varStrValues = varStrValues & ",'" & DictationRate & "'"


                            TranscriptionMethod = myDataSet.Tables(0).Rows(varIntRows).Item(14).ToString.Replace("'", "''")
                            varStrValues = varStrValues & ",'" & TranscriptionMethod & "'"

                            TranscriptionGroup = myDataSet.Tables(0).Rows(varIntRows).Item(15).ToString.Replace("'", "''")
                            varStrValues = varStrValues & ",'" & TranscriptionGroup & "'"

                            SpeakerCode = myDataSet.Tables(0).Rows(varIntRows).Item(16).ToString.Replace("'", "''")
                            varStrValues = varStrValues & ",'" & SpeakerCode & "'"

                            TranscriptionistLogin = myDataSet.Tables(0).Rows(varIntRows).Item(17).ToString.Replace("'", "''")
                            varStrValues = varStrValues & ",'" & TranscriptionistLogin & "'"

                            LineCount = myDataSet.Tables(0).Rows(varIntRows).Item(18).ToString
                            varStrValues = varStrValues & ",'" & LineCount & "'"

                            BusinessEntity = myDataSet.Tables(0).Rows(varIntRows).Item(19).ToString.Replace("'", "''")
                            varStrValues = varStrValues & ",'" & BusinessEntity & "'"

                            BusinessEntityLevel1 = myDataSet.Tables(0).Rows(varIntRows).Item(20).ToString.Replace("'", "''")
                            varStrValues = varStrValues & ",'" & BusinessEntityLevel1 & "'"

                            BusinessEntityLevel2 = myDataSet.Tables(0).Rows(varIntRows).Item(21).ToString.Replace("'", "''")
                            varStrValues = varStrValues & ",'" & BusinessEntityLevel2 & "'"

                            If IsDate(myDataSet.Tables(0).Rows(varIntRows).Item(22).ToString) Then
                                admitDate = myDataSet.Tables(0).Rows(varIntRows).Item(22).ToString
                                varStrValues = varStrValues & ",'" & admitDate & "'"
                            Else
                                varStrValues = varStrValues & ",NULL"
                            End If
                            If IsDate(myDataSet.Tables(0).Rows(varIntRows).Item(23).ToString) Then
                                dischargeDate = myDataSet.Tables(0).Rows(varIntRows).Item(23).ToString
                                varStrValues = varStrValues & ",'" & dischargeDate & "'"
                            Else
                                varStrValues = varStrValues & ",NULL"
                            End If
                            If IsDate(myDataSet.Tables(0).Rows(varIntRows).Item(24).ToString) Then
                                dictated = myDataSet.Tables(0).Rows(varIntRows).Item(24).ToString
                                varStrValues = varStrValues & ",'" & dictated & "'"
                            Else
                                varStrValues = varStrValues & ",NULL"
                            End If

                            If IsDate(myDataSet.Tables(0).Rows(varIntRows).Item(25).ToString) Then
                                tatEnd = myDataSet.Tables(0).Rows(varIntRows).Item(25).ToString
                                varStrValues = varStrValues & ",'" & tatEnd & "'"
                            Else
                                varStrValues = varStrValues & ",NULL"
                            End If
                            If IsDate(myDataSet.Tables(0).Rows(varIntRows).Item(26).ToString) Then
                                problemStart1 = myDataSet.Tables(0).Rows(varIntRows).Item(26).ToString
                                varStrValues = varStrValues & ",'" & problemStart1 & "'"
                            Else
                                varStrValues = varStrValues & ",NULL"
                            End If

                            enteredBy1 = myDataSet.Tables(0).Rows(varIntRows).Item(27).ToString.Replace("'", "''")
                            varStrValues = varStrValues & ",'" & enteredBy1 & "'"
                            If IsDate(myDataSet.Tables(0).Rows(varIntRows).Item(28).ToString) Then
                                problemEnd1 = myDataSet.Tables(0).Rows(varIntRows).Item(28).ToString
                                varStrValues = varStrValues & ",'" & problemEnd1 & "'"
                            Else
                                varStrValues = varStrValues & ",NULL"
                            End If

                            actionBy1 = myDataSet.Tables(0).Rows(varIntRows).Item(29).ToString.Replace("'", "''")
                            varStrValues = varStrValues & ",'" & actionBy1 & "'"

                            If IsDate(myDataSet.Tables(0).Rows(varIntRows).Item(30).ToString) Then
                                problemStart2 = myDataSet.Tables(0).Rows(varIntRows).Item(30).ToString
                                varStrValues = varStrValues & ",'" & problemStart2 & "'"
                            Else
                                varStrValues = varStrValues & ",NULL"
                            End If

                            enteredBy2 = myDataSet.Tables(0).Rows(varIntRows).Item(31).ToString.Replace("'", "''")
                            varStrValues = varStrValues & ",'" & enteredBy2 & "'"

                            If IsDate(myDataSet.Tables(0).Rows(varIntRows).Item(32).ToString) Then
                                problemEnd2 = myDataSet.Tables(0).Rows(varIntRows).Item(32).ToString
                                varStrValues = varStrValues & ",'" & problemEnd2 & "'"
                            Else
                                varStrValues = varStrValues & ",NULL"
                            End If

                            actionBy2 = myDataSet.Tables(0).Rows(varIntRows).Item(33).ToString.Replace("'", "''")
                            varStrValues = varStrValues & ",'" & actionBy2 & "'"

                            If IsDate(myDataSet.Tables(0).Rows(varIntRows).Item(34).ToString) Then
                                problemStart3 = myDataSet.Tables(0).Rows(varIntRows).Item(34).ToString
                                varStrValues = varStrValues & ",'" & problemStart3 & "'"
                            Else
                                varStrValues = varStrValues & ",NULL"
                            End If
                            enteredBy3 = myDataSet.Tables(0).Rows(varIntRows).Item(35).ToString.Replace("'", "''")
                            varStrValues = varStrValues & ",'" & enteredBy3 & "'"
                            If IsDate(myDataSet.Tables(0).Rows(varIntRows).Item(36).ToString) Then
                                problemEnd3 = myDataSet.Tables(0).Rows(varIntRows).Item(36).ToString
                                varStrValues = varStrValues & ",'" & problemEnd3 & "'"
                            Else
                                varStrValues = varStrValues & ",NULL"
                            End If

                            actionBy3 = myDataSet.Tables(0).Rows(varIntRows).Item(37).ToString.Replace("'", "''")
                            varStrValues = varStrValues & ",'" & actionBy3 & "'"

                            If IsDate(myDataSet.Tables(0).Rows(varIntRows).Item(38).ToString) Then
                                problemStart4 = myDataSet.Tables(0).Rows(varIntRows).Item(38).ToString
                                varStrValues = varStrValues & ",'" & problemStart4 & "'"
                            Else
                                varStrValues = varStrValues & ",NULL"
                            End If

                            enteredBy4 = myDataSet.Tables(0).Rows(varIntRows).Item(39).ToString.Replace("'", "''")
                            varStrValues = varStrValues & ",'" & enteredBy4 & "'"

                            If IsDate(myDataSet.Tables(0).Rows(varIntRows).Item(40).ToString) Then
                                problemEnd4 = myDataSet.Tables(0).Rows(varIntRows).Item(40).ToString
                                varStrValues = varStrValues & ",'" & problemEnd4 & "'"
                            Else
                                varStrValues = varStrValues & ",NULL"
                            End If

                            actionBy4 = myDataSet.Tables(0).Rows(varIntRows).Item(41).ToString.Replace("'", "''")
                            varStrValues = varStrValues & ",'" & actionBy4 & "'"

                            If IsDate(myDataSet.Tables(0).Rows(varIntRows).Item(42).ToString) Then
                                problemStart5 = myDataSet.Tables(0).Rows(varIntRows).Item(42).ToString
                                varStrValues = varStrValues & ",'" & problemStart5 & "'"
                            Else
                                varStrValues = varStrValues & ",NULL"
                            End If
                            enteredBy5 = myDataSet.Tables(0).Rows(varIntRows).Item(43).ToString.Replace("'", "''")
                            varStrValues = varStrValues & ",'" & enteredBy5 & "'"

                            If IsDate(myDataSet.Tables(0).Rows(varIntRows).Item(44).ToString) Then
                                problemEnd5 = myDataSet.Tables(0).Rows(varIntRows).Item(44).ToString
                                varStrValues = varStrValues & ",'" & problemEnd5 & "'"
                            Else
                                varStrValues = varStrValues & ",NULL"
                            End If

                            actionBy5 = myDataSet.Tables(0).Rows(varIntRows).Item(45).ToString.Replace("'", "''")
                            varStrValues = varStrValues & ",'" & actionBy5 & "'"

                            DictationhasbeenPended = myDataSet.Tables(0).Rows(varIntRows).Item(46).ToString.Replace("'", "''")
                            varStrValues = varStrValues & ",'" & DictationhasbeenPended & "'"

                            AdmitToDictationTime = myDataSet.Tables(0).Rows(varIntRows).Item(47).ToString.Replace("'", "''")
                            varStrValues = varStrValues & ",'" & AdmitToDictationTime & "'"

                            TranscribedToPendTime = myDataSet.Tables(0).Rows(varIntRows).Item(48).ToString.Replace("'", "''")
                            varStrValues = varStrValues & ",'" & TranscribedToPendTime & "'"

                            AdmitToTatEnd = myDataSet.Tables(0).Rows(varIntRows).Item(49).ToString.Replace("'", "''")
                            varStrValues = varStrValues & ",'" & AdmitToTatEnd & "'"

                            ExtendedTAT = myDataSet.Tables(0).Rows(varIntRows).Item(50).ToString
                            varStrValues = varStrValues & ",'" & ExtendedTAT & "'"

                            MTExtendedTAT = myDataSet.Tables(0).Rows(varIntRows).Item(51).ToString
                            varStrValues = varStrValues & ",'" & MTExtendedTAT & "'"

                            BusinessEntityDataField1 = myDataSet.Tables(0).Rows(varIntRows).Item(52).ToString.Replace("'", "''")
                            varStrValues = varStrValues & ",'" & BusinessEntityDataField1 & "'"

                            BusinessEntityDataField2 = myDataSet.Tables(0).Rows(varIntRows).Item(53).ToString.Replace("'", "''")
                            varStrValues = varStrValues & ",'" & BusinessEntityDataField2 & "'"

                            BusinessEntityDataField3 = myDataSet.Tables(0).Rows(varIntRows).Item(54).ToString.Replace("'", "''")
                            varStrValues = varStrValues & ",'" & BusinessEntityDataField3 & "'"

                            utteranceGroupID = myDataSet.Tables(0).Rows(varIntRows).Item(55).ToString.Replace("'", "''")
                            varStrValues = varStrValues & ",'" & utteranceGroupID & "'"

                            sequenceNumber = myDataSet.Tables(0).Rows(varIntRows).Item(56).ToString.Replace("'", "''")
                            varStrValues = varStrValues & ",'" & sequenceNumber & "'"

                            priority = myDataSet.Tables(0).Rows(varIntRows).Item(57).ToString
                            varStrValues = varStrValues & ",'" & priority & "'"

                            Account = myDataSet.Tables(0).Rows(varIntRows).Item(58).ToString
                            varStrValues = varStrValues & ",'" & Account & "')"


                            varStrInsert = "([DictationID],[DictationDuration],[DateDictated],[DateTranscribed],[DateTimeTranscribed],[TurnAroundTime],[MTTurnAroundTime],[TranscriptionDuration],[TranscriptionRate(LinesPerHour)],[Transcriptionist],[WorkType],[Provider],[Specialty],[DictationRate(LineCountPerHour)],[TranscriptionMethod],[TranscriptionGroup],[SpeakerCode],[TranscriptionistLogin],[LineCount],[BusinessEntity],[BusinessEntityLevel1],[BusinessEntityLevel2],[admitDate],[dischargeDate],[dictated],[tatEnd],[problemStart1],[enteredBy1],[problemEnd1],[actionBy1],[problemStart2],[enteredBy2],[problemEnd2],[actionBy2],[problemStart3],[enteredBy3],[problemEnd3],[actionBy3],[problemStart4],[enteredBy4],[problemEnd4],[actionBy4],[problemStart5],[enteredBy5],[problemEnd5],[actionBy5],[DictationhasbeenPended],[AdmitToDictationTime],[TranscribedToPendTime],[AdmitToTatEnd],[ExtendedTAT],[MTExtendedTAT],[BusinessEntityDataField1],[BusinessEntityDataField2],[BusinessEntityDataField3],[utteranceGroupID],[sequenceNumber],[priority],[Account])"
                            '    varStrValues = "('" & varStrUserID & "','" & myDataSet.Tables(0).Rows(varIntRows).Item(0).ToString & "'," & Trim(myDataSet.Tables(0).Rows(varIntRows).Item(1).ToString) & ",'" & Trim(myDataSet.Tables(0).Rows(varIntRows).Item(2).ToString) & "','" & CDate(Trim(myDataSet.Tables(0).Rows(varIntRows).Item(3).ToString)) & "'"
                            'Response.Write(varStrInsert)
                            'Response.Write(varStrValues)
                            If Not String.IsNullOrEmpty(varStrInsert) And Not String.IsNullOrEmpty(varStrValues) Then
                                Try
                                    Dim objCmd As New Data.SqlClient.SqlCommand("INSERT INTO Transcend.dbo.tblEScription " & varStrInsert & " VALUES " & varStrValues, oConn)

                                    If objCmd.ExecuteNonQuery() > 0 Then
                                        varCount = varCount + 1
                                    End If

                                    objCmd = Nothing
                                Catch ex As Exception
                                    Response.Write(ex.Message)
                                End Try
                            End If


                        End If
                    Next
                End If
            Else
                ErrLabel.Text = String.Empty
                ErrLabel.Text = "Template format mistmatch,please refer template "
                Exit Sub
            End If
            UpdateLines()
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
        Dim objCmd As New Data.SqlClient.SqlCommand("SELECT UserID FROM Transcend.Dbo.tblUsers WHERE UserName='" & UserName.ToString & "'", New Data.SqlClient.SqlConnection(System.Configuration.ConfigurationManager.AppSettings("ETSCon")))
        Try
            objCmd.Connection.Open()
            Dim oRec As Data.SqlClient.SqlDataReader = objCmd.ExecuteReader
            If oRec.HasRows Then
                While oRec.Read
                    varReturn = oRec("UserID").ToString
                End While
            End If
        Catch ex As exception
            Response.Write(ex.Message)
        Finally
            If objCmd.Connection.State <> Data.ConnectionState.Closed Then
                objCmd.Connection.Close()
                objCmd.Connection = Nothing
            End If
        End Try
        Return varReturn
    End Function
    Protected Sub IsJobNumberExist(ByVal varJobNumber As String)
        Dim varReturn As Boolean = False
        Dim objCmd As New Data.SqlClient.SqlCommand("SELECT Count(*) as 'Count' FROM TRANSCEND.Dbo.tblEScription WHERE DictationID='" & varJobNumber.ToString & "'", New Data.SqlClient.SqlConnection(System.Configuration.ConfigurationManager.AppSettings("ETSCon")))
        Try
            objCmd.Connection.Open()
            Dim oRec As Data.SqlClient.SqlDataReader = objCmd.ExecuteReader
            If oRec.HasRows Then
                While oRec.Read
                    If oRec("Count") > 0 Then
                        varReturn = True
                        Dim objCmd1 As New Data.SqlClient.SqlCommand("DELETE FROM Transcend.dbo.tblEScription  WHERE DictationID='" & varJobNumber.ToString & "'", New Data.SqlClient.SqlConnection(System.Configuration.ConfigurationManager.AppSettings("ETSCon")))
                        objCmd1.Connection.Open()
                        If objCmd1.ExecuteNonQuery() > 0 Then
                            'Response.Write("Deleted")
                        End If
                        If objCmd1.Connection.State <> Data.ConnectionState.Closed Then
                            objCmd1.Connection.Close()
                            objCmd1.Connection = Nothing
                        End If

                    End If
                End While
            End If
        Catch ex As exception
            Response.Write(ex.Message)
        Finally
            If objCmd.Connection.State <> Data.ConnectionState.Closed Then
                objCmd.Connection.Close()
                objCmd.Connection = Nothing
            End If
        End Try

    End Sub

    Protected Sub UpdateLines()
        Try

       
            Dim strConn As String = System.Configuration.ConfigurationManager.AppSettings("ETSCon")
            Dim strQuery As String
            Dim oConn As New Data.SqlClient.SqlConnection(strConn)
            Try
                oConn.Open()
                strQuery = "[Transcend].[dbo].[InsertEscriptionUserLines]"
                Dim SQLCmd3 As New SqlCommand(strQuery, oConn)
                SQLCmd3.CommandType = CommandType.StoredProcedure
                SQLCmd3.Parameters.Add("@PostMonth", SqlDbType.Int)
                SQLCmd3.Parameters.Add("@PostYear", SqlDbType.Int)
                SQLCmd3.Parameters("@PostMonth").Value = DLMonth1.SelectedValue
                SQLCmd3.Parameters("@PostYear").Value = DLYear1.SelectedValue
                SQLCmd3.ExecuteNonQuery()

            Catch ex As exception
                Response.Write(ex.Message)
            Finally
                If oConn.State <> ConnectionState.Closed Then
                    oConn.Close()
                    oConn = Nothing
                End If
            End Try
        Catch ex As exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            GetMyMonthList(DLMonth1, True)
            GetMyYearList(DLYear1, True)
        End If
    End Sub
End Class
