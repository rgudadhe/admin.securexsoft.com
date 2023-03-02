Imports System.Data
Imports System.Data.OleDb
Imports MainModule
Partial Class ImportLeaveBalanceNew
    Inherits BasePage
    Dim varObjMainModule As New MainModule
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            tblDataImported.Visible = False
        End If
    End Sub
    Protected Sub btnUpload_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnUpload.Click
        Dim varFileUploadPath As String
        Dim varStrTemp() As String
        Dim varStrFolder As String
        Dim iFilePath As String = String.Empty


        If Trim(UCase(System.IO.Path.GetExtension(Server.HtmlEncode(FileUpload.FileName)))) = Trim(UCase((".XLS"))) Then
            varStrTemp = Split(Now(), " ")
            varStrFolder = Replace(varStrTemp(0), "/", "-")
            varFileUploadPath = Server.MapPath("/ets_Files/") & "\LeaveBalanceFiles\" & varStrFolder & "\"
            Dim varTempDir As New IO.DirectoryInfo(varFileUploadPath)
            If varTempDir.Exists = False Then
                varTempDir.Create()
            End If
            iFilePath = varFileUploadPath & "\" & FileUpload.FileName
            FileUpload.PostedFile.SaveAs(iFilePath)

            If System.IO.File.Exists(iFilePath) = True Then
                Dim myConnectionString As String
                Dim myConnection As OleDbConnection
                Dim myCommand As OleDbCommand
                Dim myDataReader As OleDbDataReader

                Try
                    myConnectionString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" & iFilePath & ";Extended Properties=Excel 8.0;"
                    myConnection = New OleDbConnection(myConnectionString)
                    myConnection.Open()
                    myCommand = New OleDbCommand("SELECT ID,CasualLeaves,EarnedLeaves,WeeklyOff1,WeeklyOff2 FROM [Sheet1$]", myConnection)

                    myDataReader = myCommand.ExecuteReader(CommandBehavior.CloseConnection)
                    'myDataReader.Read()
                    Dim DTSend As New Data.DataTable
                    If myDataReader.HasRows Then

                        DTSend.Columns.Add(New DataColumn("ID", GetType(System.String)))
                        DTSend.Columns.Add(New DataColumn("CasualLeaves", GetType(System.Double)))
                        DTSend.Columns.Add(New DataColumn("EarnedLeaves", GetType(System.Double)))
                        DTSend.Columns.Add(New DataColumn("WeeklyOff1", GetType(System.String)))
                        DTSend.Columns.Add(New DataColumn("WeeklyOff2", GetType(System.String)))
                        Do While myDataReader.Read
                            'Response.Write("Test")
                            Dim DRRec As DataRow = DTSend.NewRow
                            If Not String.IsNullOrEmpty(myDataReader("ID")) Then
                                DRRec("ID") = myDataReader("ID").ToString
                            End If

                            If Not String.IsNullOrEmpty(myDataReader("CasualLeaves")) Then
                                DRRec("CasualLeaves") = CDbl(myDataReader("CasualLeaves"))
                            Else
                                DRRec("CasualLeaves") = 0
                            End If
                            If Not String.IsNullOrEmpty(myDataReader("EarnedLeaves")) Then
                                DRRec("EarnedLeaves") = CDbl(myDataReader("EarnedLeaves"))
                            Else
                                DRRec("EarnedLeaves") = 0
                            End If

                            If Not String.IsNullOrEmpty(myDataReader("WeeklyOff1")) Then
                                DRRec("WeeklyOff1") = myDataReader("WeeklyOff1").ToString
                            End If

                            If Not String.IsNullOrEmpty(myDataReader("WeeklyOff2").ToString) And myDataReader("WeeklyOff2").ToString <> "" Then
                                DRRec("WeeklyOff2") = Trim(myDataReader("WeeklyOff2").ToString)
                            End If


                            DTSend.Rows.Add(DRRec)
                            'TotalRec = TotalRec + 1
                        Loop
                    End If

                    'Response.Write("Testing ")
                    'Response.End()

                    If DTSend.Rows.Count > 0 Then
                        Dim clsLB As ETS.BL.LeaveBalance
                        Dim DTRecStatus As New Data.DataTable
                        Try
                            clsLB = New ETS.BL.LeaveBalance
                            'Response.Write("<BR>" & clsLB.btn_Import_Click(DTSend, Session("ContractorID"), Guid.NewGuid.ToString, Session("UserID"), FileUpload.FileName))
                            DTRecStatus = clsLB.btn_Import_Click(DTSend, Session("ContractorID"), Guid.NewGuid.ToString, Session("UserID"), FileUpload.FileName)
                            If DTRecStatus.Rows.Count > 0 Then
                                Response.Write(DTRecStatus.Rows(0).Item(0).ToString)
                                For Each DRTemp As Data.DataRow In DTRecStatus.Rows
                                    Dim varTblRow As New TableRow
                                    varTblRow.Font.Size = 10
                                    Dim varTblCellUserName As New TableCell
                                    varTblCellUserName.Text = DRTemp("EmpName")

                                    Dim varTblCellBCL As New TableCell
                                    varTblCellBCL.Text = DRTemp("BCL")

                                    Dim varTblCellBEL As New TableCell
                                    varTblCellBEL.Text = DRTemp("BEL")

                                    Dim varTblCellBWOff1 As New TableCell
                                    If String.IsNullOrEmpty(DRTemp("BWOff1")) Then
                                        varTblCellBWOff1.Text = "&nbsp"
                                    Else
                                        varTblCellBWOff1.Text = DRTemp("BWOff1")
                                    End If


                                    Dim varTblCellBWOff2 As New TableCell
                                    If String.IsNullOrEmpty(DRTemp("BWOff2")) Then
                                        varTblCellBWOff1.Text = "&nbsp"
                                    Else
                                        varTblCellBWOff1.Text = DRTemp("BWOff2")
                                    End If


                                    Dim varTblCellCL As New TableCell
                                    varTblCellCL.Text = DRTemp("CL")

                                    Dim varTblCellEL As New TableCell
                                    varTblCellEL.Text = DRTemp("EL")

                                    Dim varTblCellWOff1 As New TableCell
                                    If String.IsNullOrEmpty(DRTemp("WOff1")) Then
                                        varTblCellWOff1.Text = "&nbsp"
                                    Else
                                        varTblCellWOff1.Text = DRTemp("WOff1")
                                    End If


                                    Dim varTblCellWOff2 As New TableCell
                                    If String.IsNullOrEmpty(DRTemp("WOff2")) Then
                                        varTblCellWOff2.Text = "&nbsp"
                                    Else
                                        varTblCellWOff2.Text = DRTemp("WOff2")
                                    End If

                                    varTblRow.Cells.Add(varTblCellUserName)
                                    varTblRow.Cells.Add(varTblCellBCL)
                                    varTblRow.Cells.Add(varTblCellBEL)
                                    varTblRow.Cells.Add(varTblCellBWOff1)
                                    varTblRow.Cells.Add(varTblCellBWOff2)
                                    varTblRow.Cells.Add(varTblCellCL)
                                    varTblRow.Cells.Add(varTblCellEL)
                                    varTblRow.Cells.Add(varTblCellWOff1)
                                    varTblRow.Cells.Add(varTblCellWOff2)
                                    tblDataImported.Rows.Add(varTblRow)
                                Next
                                tblDataImported.Visible = True
                                tblMainPage.Visible = False
                                Response.Write("<script type=""text/javascript"" language=javascript> alert(""File Imported Sucessfully!!!"");</script>")
                            Else
                                Response.Write("<script type=""text/javascript"" language=javascript> alert(""File Imported failed"");window.location.href='ImportLeaveBalance.aspx';</script>")
                            End If

                        Catch ex As Exception
                            Response.Write(ex.Message)
                        Finally
                            clsLB = Nothing
                            DTRecStatus = Nothing
                        End Try
                    End If

                Catch ex As Exception
                    Response.Write(ex.Message)
                    If myConnection.State Then
                        myConnection.Close()
                    End If
                Finally
                    If Not myDataReader Is Nothing Then
                        myDataReader.Close()
                    End If
                    If Not myConnection Is Nothing Then
                        myConnection.Close()
                    End If
                End Try
            End If
        Else
            Response.Write("<script type=""text/javascript"" language=javascript> alert(""File format should be XLS"");window.location.href='ImportLeaveBalance.aspx';</script>")
        End If


        'Dim varFileUploadPath As String
        'Dim varStrTemp() As String
        'Dim varStrFolder As String
        'If Trim(UCase(System.IO.Path.GetExtension(Server.HtmlEncode(FileUpload.FileName)))) = Trim(UCase((".XLS"))) Then
        '    'Response.Write(FileUpload.FileName)
        '    'Response.Write(Server.MapPath(Request.ServerVariables("Dir_Info")) & "\Upload Files\" & Now() & "\" & FileUpload.FileName)
        '    varStrTemp = Split(Now(), " ")
        '    varStrFolder = Replace(varStrTemp(0), "/", "-")
        '    varFileUploadPath = Server.MapPath("/ets_Files/") & "\LeaveBalanceFiles\" & varStrFolder & "\"
        '    Dim varTempDir As New IO.DirectoryInfo(varFileUploadPath)
        '    If varTempDir.Exists = False Then
        '        varTempDir.Create()
        '    End If

        '    Dim objEXL As Excel.Application
        '    Dim objEXLWB As Excel.Workbook
        '    Dim objEXLSheet As Excel.Worksheet

        '    Dim objConn As New Data.SqlClient.SqlConnection
        '    objConn = varObjMainModule.OpenConnection(objConn)

        '    Try
        '        FileUpload.PostedFile.SaveAs(varFileUploadPath & "\" & FileUpload.FileName)
        '        'Dim objTable As New Table
        '        'Response.Write(varFileUploadPath & FileUpload.FileName)
        '        'Response.End()
        '        objEXL = New Excel.Application
        '        objEXLWB = objEXL.Workbooks.Open(varFileUploadPath & FileUpload.FileName)
        '        objEXLSheet = objEXLWB.Sheets(1)

        '        Dim varIntRows, varIntCols As Integer
        '        Dim varIntR As Integer

        '        Dim varStrEtsID As String
        '        Dim varStrUserID As String
        '        Dim varStrEmpName As String
        '        Dim varCL As Double
        '        Dim varEL As Double
        '        Dim varBCL As Double
        '        Dim varBEL As Double

        '        Dim varTL As Double
        '        Dim varStrWOff1 As String
        '        Dim varStrWOff2 As String
        '        Dim varStrBWOff1 As String
        '        Dim varStrBWOff2 As String
        '        Dim varStrInsert As String

        '        Dim varStrTrackID As String

        '        varIntRows = objEXLSheet.UsedRange.Rows.Count
        '        varIntCols = objEXLSheet.UsedRange.Columns.Count
        '        varStrTrackID = Guid.NewGuid.ToString

        '        'Response.Write(varIntRows & varIntCols)
        '        'Response.End()

        '        ' Scroll through all the rows and columns retrieving values.
        '        For varIntR = 2 To varIntRows
        '            'For varIntC = 1 To varIntCols
        '            varStrEtsID = CType(objEXLSheet.Cells(varIntR, 1), Excel.Range).Text.ToString
        '            'Response.Write(varStrEtsID)
        '            Dim oCommandID As New Data.SqlClient.SqlCommand("SELECT UserID,FirstName,LastName FROM DBO.tblUsers WHERE UserName ='" & varStrEtsID & "' AND ContractorID='" & Session("ContractorID").ToString & "' AND (IsDeleted IS NULL OR IsDeleted = 0) ", objConn)
        '            Dim oRecID As Data.SqlClient.SqlDataReader = oCommandID.ExecuteReader()
        '            'Response.Write(oCommandID.CommandText)
        '            If oRecID.HasRows Then
        '                While oRecID.Read
        '                    varStrUserID = oRecID.GetGuid(oRecID.GetOrdinal("UserID")).ToString
        '                    varStrEmpName = oRecID.GetString(oRecID.GetOrdinal("FirstName")).ToString & " " & oRecID.GetString(oRecID.GetOrdinal("LastName")).ToString
        '                End While
        '            End If
        '            oRecID.Close()
        '            oRecID = Nothing
        '            oCommandID = Nothing

        '            'Response.Write(varStrUserID & "<BR>")

        '            If Not String.IsNullOrEmpty(varStrUserID) Then
        '                If CType(objEXLSheet.Cells(varIntR, 2), Excel.Range).Text <> "" Then
        '                    varCL = CType(objEXLSheet.Cells(varIntR, 2), Excel.Range).Text
        '                Else
        '                    varCL = 0
        '                End If
        '                If CType(objEXLSheet.Cells(varIntR, 3), Excel.Range).Text <> "" Then
        '                    varEL = CType(objEXLSheet.Cells(varIntR, 3), Excel.Range).Text
        '                Else
        '                    varEL = 0
        '                End If

        '                varTL = CDbl(varCL) + CDbl(varEL)
        '                If CType(objEXLSheet.Cells(varIntR, 4), Excel.Range).Text <> "" Then
        '                    varStrWOff1 = CType(objEXLSheet.Cells(varIntR, 4), Excel.Range).Text
        '                Else
        '                    varStrWOff1 = ""
        '                End If
        '                If CType(objEXLSheet.Cells(varIntR, 5), Excel.Range).Text <> "" Then
        '                    varStrWOff2 = CType(objEXLSheet.Cells(varIntR, 5), Excel.Range).Text
        '                Else
        '                    varStrWOff2 = ""
        '                End If

        '                Dim oCommandLB As New Data.SqlClient.SqlCommand("SELECT * FROM DBO.tblLeaveBalance WHERE UserID ='" & varStrUserID & "'", objConn)
        '                Dim oRecLB As Data.SqlClient.SqlDataReader = oCommandLB.ExecuteReader()

        '                Dim varStrInsertWOff As String
        '                Dim varExistRecord As Boolean = False
        '                If oRecLB.HasRows Then
        '                    While oRecLB.Read
        '                        varBCL = oRecLB.GetDouble(oRecLB.GetOrdinal("CL"))
        '                        varBEL = oRecLB.GetDouble(oRecLB.GetOrdinal("EL"))
        '                        If Not oRecLB.IsDBNull(oRecLB.GetOrdinal("WeeklyOff1")) Then
        '                            varStrBWOff1 = oRecLB.GetString(oRecLB.GetOrdinal("WeeklyOff1"))
        '                        Else
        '                            varStrBWOff1 = ""
        '                        End If
        '                        If Not oRecLB.IsDBNull(oRecLB.GetOrdinal("WeeklyOff2")) Then
        '                            varStrBWOff2 = oRecLB.GetString(oRecLB.GetOrdinal("WeeklyOff2"))
        '                        Else
        '                            varStrBWOff2 = ""
        '                        End If
        '                        varExistRecord = True
        '                    End While
        '                End If
        '                oRecLB.Close()
        '                oRecLB = Nothing
        '                oCommandLB = Nothing
        '                'Response.Write(varExistRecord & "<BR>")
        '                'Response.End()
        '                If varExistRecord Then
        '                    Dim varStrUpdateLB As String
        '                    varStrUpdateLB = "UPDATE DBO.tblLeaveBalance SET CL=" & varCL & ",EL=" & varEL & ",TL=" & varTL & ",WeeklyOff1='" & varStrWOff1 & "',WeeklyOff2='" & varStrWOff2 & "',LastModified='" & Now() & "' WHERE UserID='" & varStrUserID & "'"
        '                    Dim UpdateLB As New Data.SqlClient.SqlCommand
        '                    UpdateLB.CommandType = Data.CommandType.Text
        '                    UpdateLB.CommandText = varStrUpdateLB
        '                    UpdateLB.Connection = objConn
        '                    UpdateLB.ExecuteNonQuery()
        '                    UpdateLB = Nothing

        '                    varStrInsert = "INSERT INTO DBO.tblPrevLeaveBalance(TrackID,UserID,BCL,BEL,BWOff1,BWOff2,ACL,AEL,AWOff1,AWOff2,DateCreated)VALUES('" & varStrTrackID & "','" & varStrUserID & "'," & varBCL & "," & varBEL & ",'" & varStrBWOff1 & "','" & varStrBWOff2 & "'," & varCL & "," & varEL & ",'" & varStrWOff1 & "','" & varStrWOff2 & "','" & Now() & "')"
        '                    Dim InsertCmdPreLeaveBal As New Data.SqlClient.SqlCommand
        '                    InsertCmdPreLeaveBal.CommandType = Data.CommandType.Text
        '                    InsertCmdPreLeaveBal.CommandText = varStrInsert
        '                    InsertCmdPreLeaveBal.Connection = objConn
        '                    InsertCmdPreLeaveBal.ExecuteNonQuery()
        '                    InsertCmdPreLeaveBal = Nothing



        '                    If Trim(UCase(varStrBWOff1)) <> Trim(UCase(varStrWOff1)) Or Trim(UCase(varStrBWOff2)) <> Trim(UCase(varStrWOff2)) Then
        '                        Dim varStrDelete

        '                        varStrDelete = "DELETE DBO.tblPrevWeeklyOffs WHERE UserID='" & varStrUserID & "'"
        '                        Dim DeleteCmd As New Data.SqlClient.SqlCommand
        '                        DeleteCmd.CommandType = Data.CommandType.Text
        '                        DeleteCmd.CommandText = varStrDelete
        '                        DeleteCmd.Connection = objConn
        '                        DeleteCmd.ExecuteNonQuery()
        '                        DeleteCmd = Nothing

        '                        varStrInsertWOff = "INSERT INTO DBO.tblPrevWeeklyOffs (UserID,WOff1,WOff2,UpdateDate)VALUES('" & varStrUserID & "','" & varStrWOff1 & "','" & varStrWOff2 & "','" & Now() & "')"
        '                        Dim InsertCmdWOff As New Data.SqlClient.SqlCommand
        '                        InsertCmdWOff.CommandType = Data.CommandType.Text
        '                        InsertCmdWOff.CommandText = varStrInsertWOff
        '                        InsertCmdWOff.Connection = objConn
        '                        InsertCmdWOff.ExecuteNonQuery()
        '                        InsertCmdWOff = Nothing
        '                    End If
        '                Else
        '                    Dim varStrInsertLB As String
        '                    varStrInsertLB = "INSERT INTO DBO.tblLeaveBalance(UserID,CL,EL,TL,WeeklyOff1,WeeklyOff2,LastModified) VALUES('" & varStrUserID & "'," & varCL & "," & varEL & "," & varTL & ",'" & varStrWOff1 & "','" & varStrWOff2 & "','" & Now() & "')"
        '                    'Response.Write(varStrInsertLB & "<BR>")
        '                    'Response.End()
        '                    Dim InsLB As New Data.SqlClient.SqlCommand
        '                    InsLB.CommandType = Data.CommandType.Text
        '                    InsLB.CommandText = varStrInsertLB
        '                    InsLB.Connection = objConn
        '                    InsLB.ExecuteNonQuery()
        '                    InsLB = Nothing
        '                End If


        '                Dim varTblRow As New TableRow
        '                varTblRow.Font.Size = 10
        '                Dim varTblCellUserName As New TableCell
        '                varTblCellUserName.Text = varStrEmpName

        '                Dim varTblCellBCL As New TableCell
        '                varTblCellBCL.Text = varBCL

        '                Dim varTblCellBEL As New TableCell
        '                varTblCellBEL.Text = varBEL

        '                Dim varTblCellBWOff1 As New TableCell
        '                If varStrBWOff1 <> "" Then
        '                    varTblCellBWOff1.Text = varStrBWOff1
        '                Else
        '                    varTblCellBWOff1.Text = "&nbsp"
        '                End If


        '                Dim varTblCellBWOff2 As New TableCell
        '                If varStrBWOff2 <> "" Then
        '                    varTblCellBWOff2.Text = varStrBWOff2
        '                Else
        '                    varTblCellBWOff2.Text = "&nbsp"
        '                End If


        '                Dim varTblCellCL As New TableCell
        '                varTblCellCL.Text = varCL

        '                Dim varTblCellEL As New TableCell
        '                varTblCellEL.Text = varEL

        '                Dim varTblCellWOff1 As New TableCell
        '                If varStrWOff1 <> "" Then
        '                    varTblCellWOff1.Text = varStrWOff1
        '                Else
        '                    varTblCellWOff1.Text = "&nbsp"
        '                End If


        '                Dim varTblCellWOff2 As New TableCell
        '                If varStrWOff2 <> "" Then
        '                    varTblCellWOff2.Text = varStrWOff2
        '                Else
        '                    varTblCellWOff2.Text = "&nbsp"
        '                End If


        '                varTblRow.Cells.Add(varTblCellUserName)
        '                varTblRow.Cells.Add(varTblCellBCL)
        '                varTblRow.Cells.Add(varTblCellBEL)
        '                varTblRow.Cells.Add(varTblCellBWOff1)
        '                varTblRow.Cells.Add(varTblCellBWOff2)
        '                varTblRow.Cells.Add(varTblCellCL)
        '                varTblRow.Cells.Add(varTblCellEL)
        '                varTblRow.Cells.Add(varTblCellWOff1)
        '                varTblRow.Cells.Add(varTblCellWOff2)
        '                tblDataImported.Rows.Add(varTblRow)

        '                'MsgBox(CType(objEXLSheet.Cells(varIntR, varIntC), Excel.Range).Text)
        '                'Next
        '                'Response.End()
        '                varCL = 0
        '                varEL = 0
        '                varStrWOff1 = ""
        '                varStrWOff2 = ""
        '                varBCL = 0
        '                varBEL = 0
        '                varStrBWOff1 = ""
        '                varStrBWOff2 = ""
        '                varTL = 0
        '                varStrEmpName = ""
        '                varStrUserID = ""
        '            Else
        '                Dim varTblRowE As New TableRow
        '                varTblRowE.Font.Size = 10
        '                Dim varTblCellUserNameE As New TableCell
        '                varTblCellUserNameE.Text = "Secure IT ID mis-match : " & varStrEtsID & ""
        '                varTblCellUserNameE.ColumnSpan = 9
        '                varTblRowE.Cells.Add(varTblCellUserNameE)
        '                tblDataImported.Rows.Add(varTblRowE)
        '            End If
        '        Next

        '        varStrInsert = "INSERT INTO DBO.tblLeaveBalanceImportLog (TrackID,UpdatedBy,FileName,UpdatedDate)VALUES('" & varStrTrackID & "','" & Session("UserID") & "','" & FileUpload.FileName & "','" & Now() & "')"
        '        Dim InsertCmd As New Data.SqlClient.SqlCommand
        '        InsertCmd.CommandType = Data.CommandType.Text
        '        InsertCmd.CommandText = varStrInsert
        '        InsertCmd.Connection = objConn
        '        InsertCmd.ExecuteNonQuery()
        '        InsertCmd = Nothing

        '        'Me.Controls.Add(objTable)
        '        'Response.End()
        '        tblDataImported.Visible = True
        '        tblMainPage.Visible = False

        '        Response.Write("<script type=""text/javascript"" language=javascript> alert(""File Imported Sucessfully!!!"");</script>")
        '        'Response.End()
        '    Catch ex As Exception
        '        Response.Write(ex.Message)
        '        Response.End()
        '    Finally
        '        objEXLWB.Close(False)
        '        objEXL.Quit()
        '        objEXLSheet = Nothing
        '        objEXLWB = Nothing
        '        objEXL = Nothing

        '        If objConn.State <> Data.ConnectionState.Closed Then
        '            objConn.Close()
        '            objConn = Nothing
        '        End If
        '    End Try
        'Else
        '    Response.Write("<script type=""text/javascript"" language=javascript> alert(""File format should be XLS"");window.location.href='ImportLeaveBalance.aspx';</script>")
        'End If
    End Sub
End Class
