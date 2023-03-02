
Partial Class Transcend_TechData
    Inherits BasePage
    Protected Sub btnUpload_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnUpload.Click
        lblResponse.Text = String.Empty
        ErrLabel.Text = String.Empty
        If CheckExcel() Then
            Dim varFileName As String = String.Empty
            Dim varFileUploadPath As String = String.Empty

            Dim dtFormat = Format(Month(Now), "00") & "-" & Format(Day(Now), "00") & "-" & Format(Year(Now), "00") & " " & Format(Hour(Now), "00") & "-" & Format(Minute(Now), "00")
            varFileUploadPath = Server.MapPath("/ets_Files/") & "\Transcend\"
            'varFileUploadPath = Server.MapPath("/Transcend_Files/")
            'varFileUploadPath = Server.MapPath("/ETS_Files").ToString & "\"

            'Dim varTempDir As New IO.DirectoryInfo(varFileUploadPath)
            'If varTempDir.Exists = False Then
            '    varTempDir.Create()
            'End If
            varFileName = varFileUploadPath & "\" & Session("UserName").ToString & "_" & dtFormat.ToString & "_" & FileUpload.FileName
            FileUpload.PostedFile.SaveAs(varFileName)
            hdnFileName.Value = varFileName

            ErrLabel.Text = String.Empty
            ErrLabel.Text = "File uploaded sucessfully,please click on Generate Data button "
            BtnGenerate.Enabled = True
            Exit Sub
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

        Dim oConn As New Data.SqlClient.SqlConnection
        oConn.ConnectionString = System.Configuration.ConfigurationManager.AppSettings("ETSCon")
        Dim oleDBConnection As New System.Data.OleDb.OleDbConnection
        Dim myDataAdapter As System.Data.OleDb.OleDbDataAdapter
        Dim varVWJob As String = String.Empty
        Try
            myConnectionString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" & varFileName & ";Extended Properties=Excel 8.0;"

            oleDBConnection.ConnectionString = myConnectionString
            myDataAdapter = New System.Data.OleDb.OleDbDataAdapter("SELECT * FROM [Sheet1$]", oleDBConnection)


            Dim myDataSet As New System.Data.DataSet()
            myDataAdapter.Fill(myDataSet, "ExcelInfo")

            Dim varIntRows, varIntCols As Integer

            varIntRows = myDataSet.Tables(0).Rows.Count
            varIntCols = myDataSet.Tables(0).Columns.Count

            varCountO = varIntRows
            

            If varIntCols = 20 Then
                If Not Trim(UCase(myDataSet.Tables(0).Columns(0).Caption.ToString)) = Trim(UCase("CDescription")) Then
                    varBolFormat = False

                    ErrLabel.Text = String.Empty
                    ErrLabel.Text = "1Template format mistmatch,please refer template "
                    Exit Sub
                ElseIf Not Trim(UCase(myDataSet.Tables(0).Columns(1).Caption.ToString)) = Trim(UCase("MTID")) Then
                    varBolFormat = False

                    ErrLabel.Text = String.Empty
                    ErrLabel.Text = "2Template format mistmatch,please refer template "
                    Exit Sub
                ElseIf Not Trim(UCase(myDataSet.Tables(0).Columns(2).Caption.ToString)) = Trim(UCase("FirstName")) Then
                    varBolFormat = False

                    ErrLabel.Text = String.Empty
                    ErrLabel.Text = "3Template format mistmatch,please refer template "
                    Exit Sub
                ElseIf Not Trim(UCase(myDataSet.Tables(0).Columns(3).Caption.ToString)) = Trim(UCase("LastName")) Then
                    varBolFormat = False

                    ErrLabel.Text = String.Empty
                    ErrLabel.Text = "4Template format mistmatch,please refer template "
                    Exit Sub
                ElseIf Not Trim(UCase(myDataSet.Tables(0).Columns(4).Caption.ToString)) = Trim(UCase("CustomerID")) Then
                    varBolFormat = False

                    ErrLabel.Text = String.Empty
                    ErrLabel.Text = "5Template format mistmatch,please refer template "
                    Exit Sub
                ElseIf Not Trim(UCase(myDataSet.Tables(0).Columns(5).Caption.ToString)) = Trim(UCase("WorkType")) Then
                    varBolFormat = False

                    ErrLabel.Text = String.Empty
                    ErrLabel.Text = "6Template format mistmatch,please refer template "
                    Exit Sub
                ElseIf Not Trim(UCase(myDataSet.Tables(0).Columns(6).Caption.ToString)) = Trim(UCase("Description")) Then
                    varBolFormat = False

                    ErrLabel.Text = String.Empty
                    ErrLabel.Text = "7Template format mistmatch,please refer template "
                    Exit Sub
                ElseIf Not Trim(UCase(myDataSet.Tables(0).Columns(7).Caption.ToString)) = Trim(UCase("VWJob")) Then
                    varBolFormat = False

                    ErrLabel.Text = String.Empty
                    ErrLabel.Text = "8Template format mistmatch,please refer template "
                    Exit Sub
                ElseIf Not Trim(UCase(myDataSet.Tables(0).Columns(8).Caption.ToString)) = Trim(UCase("Length")) Then
                    varBolFormat = False

                    ErrLabel.Text = String.Empty
                    ErrLabel.Text = "9Template format mistmatch,please refer template "
                    Exit Sub
                ElseIf Not Trim(UCase(myDataSet.Tables(0).Columns(9).Caption.ToString)) = Trim(UCase("PR Lines")) Then
                    varBolFormat = False

                    ErrLabel.Text = String.Empty
                    ErrLabel.Text = "10Template format mistmatch,please refer template "
                    Exit Sub
                ElseIf Not Trim(UCase(myDataSet.Tables(0).Columns(10).Caption.ToString)) = Trim(UCase("DateDictated")) Then
                    varBolFormat = False

                    ErrLabel.Text = String.Empty
                    ErrLabel.Text = "11Template format mistmatch,please refer template "
                    Exit Sub
                ElseIf Not Trim(UCase(myDataSet.Tables(0).Columns(11).Caption.ToString)) = Trim(UCase("DueDate")) Then
                    varBolFormat = False

                    ErrLabel.Text = String.Empty
                    ErrLabel.Text = "Template format mistmatch,please refer template "
                    Exit Sub
                ElseIf Not Trim(UCase(myDataSet.Tables(0).Columns(12).Caption.ToString)) = Trim(UCase("DateReturned")) Then
                    varBolFormat = False

                    ErrLabel.Text = String.Empty
                    ErrLabel.Text = "Template format mistmatch,please refer template "
                    Exit Sub
                ElseIf Not Trim(UCase(myDataSet.Tables(0).Columns(13).Caption.ToString)) = Trim(UCase("DateCompleted")) Then
                    varBolFormat = False

                    ErrLabel.Text = String.Empty
                    ErrLabel.Text = "Template format mistmatch,please refer template "
                    Exit Sub
                ElseIf Not Trim(UCase(myDataSet.Tables(0).Columns(14).Caption.ToString)) = Trim(UCase("DatePrinted")) Then
                    varBolFormat = False

                    ErrLabel.Text = String.Empty
                    ErrLabel.Text = "Template format mistmatch,please refer template "
                    Exit Sub
                ElseIf Not Trim(UCase(myDataSet.Tables(0).Columns(15).Caption.ToString)) = Trim(UCase("Delivered TAT")) Then
                    varBolFormat = False

                    ErrLabel.Text = String.Empty
                    ErrLabel.Text = "Template format mistmatch,please refer template "
                    Exit Sub
                ElseIf Not Trim(UCase(myDataSet.Tables(0).Columns(16).Caption.ToString)) = Trim(UCase("Delivered Within TAT")) Then
                    varBolFormat = False

                    ErrLabel.Text = String.Empty
                    ErrLabel.Text = "Template format mistmatch,please refer template "
                    Exit Sub
                ElseIf Not Trim(UCase(myDataSet.Tables(0).Columns(17).Caption.ToString)) = Trim(UCase("Expected TAT")) Then
                    varBolFormat = False

                    ErrLabel.Text = String.Empty
                    ErrLabel.Text = "Template format mistmatch,please refer template "
                    Exit Sub
                ElseIf Not Trim(UCase(myDataSet.Tables(0).Columns(18).Caption.ToString)) = Trim(UCase("Difference")) Then
                    varBolFormat = False

                    ErrLabel.Text = String.Empty
                    ErrLabel.Text = "Template format mistmatch,please refer template "
                    Exit Sub
                ElseIf Not Trim(UCase(myDataSet.Tables(0).Columns(19).Caption.ToString)) = Trim(UCase("TranscriptionMethod")) Then
                    varBolFormat = False

                    ErrLabel.Text = String.Empty
                    ErrLabel.Text = "Template format mistmatch,please refer template "
                    Exit Sub
                End If

                If varBolFormat Then
                    oConn.Open()
                    For varIntRows = 0 To varIntRows - 1
                        Dim varStrInsert As String = String.Empty
                        Dim varStrValues As String = String.Empty
                        For varIntT As Integer = 0 To varIntCols - 1
                            If Not String.IsNullOrEmpty(myDataSet.Tables(0).Rows(varIntRows).Item(varIntT).ToString) Then
                                If String.IsNullOrEmpty(varStrInsert) Then
                                    varStrInsert = "(" & Replace(Trim(myDataSet.Tables(0).Columns(varIntT).Caption.ToString), " ", "_")
                                Else
                                    varStrInsert = varStrInsert & "," & Replace(Trim(myDataSet.Tables(0).Columns(varIntT).Caption.ToString), " ", "_")
                                End If

                                If String.IsNullOrEmpty(varStrValues) Then
                                    varStrValues = "('" & myDataSet.Tables(0).Rows(varIntRows).Item(varIntT).ToString & "'"
                                Else
                                    If varIntT = 9 Then
                                        varStrValues = varStrValues & "," & Format(CDbl(myDataSet.Tables(0).Rows(varIntRows).Item(varIntT).ToString), "00.00") & ""
                                    Else
                                        varStrValues = varStrValues & ",'" & myDataSet.Tables(0).Rows(varIntRows).Item(varIntT).ToString & "'"
                                    End If
                                End If
                            End If
                        Next

                        If Not String.IsNullOrEmpty(varStrInsert) Then
                            varStrInsert = varStrInsert & ",UpdatedBy,UpdatedOn)"
                        End If
                        If Not String.IsNullOrEmpty(varStrValues) Then
                            varStrValues = varStrValues & ",'" & Session("UserID").ToString & "','" & Now() & "')"
                        End If

                        If Not String.IsNullOrEmpty(varStrInsert) And Not String.IsNullOrEmpty(varStrValues) Then
                            'Response.Write("INSERT INTO Transcend.dbo.tblBData " & varStrInsert & " VALUES " & varStrValues)
                            Try
                                Dim objCmd As New Data.SqlClient.SqlCommand("INSERT INTO Transcend.dbo.tblBData " & varStrInsert & " VALUES " & varStrValues, oConn)

                                If objCmd.ExecuteNonQuery() > 0 Then
                                    varCount = varCount + 1
                                End If

                                objCmd = Nothing
                            Catch ex As Exception
                                'Response.Write(ex.Message)
                            End Try
                        End If
                    Next
                End If
            Else
                ErrLabel.Text = String.Empty
                ErrLabel.Text = "Template format mistmatch,please refer template "
                Exit Sub
            End If
            ErrLabel.Text = String.Empty
            ErrLabel.Text = varCount & " records updated out of " & varCountO
            BtnGenerate.Enabled = False

        Catch ex As Exception
            Response.Write("Error : " & ex.Message)
        Finally
            If oConn.State <> Data.ConnectionState.Closed Then
                oConn.Close()
                oConn = Nothing
            End If
            If oleDBConnection.State <> Data.ConnectionState.Closed Then
                oleDBConnection.Close()
                oleDBConnection = Nothing
            End If
            myDataAdapter.Dispose()
        End Try
    End Sub
    Protected Function CheckJob(ByVal JobNumber As String) As Boolean
        Dim oConn As New Data.SqlClient.SqlConnection
        oConn.ConnectionString = System.Configuration.ConfigurationManager.AppSettings("ETSCon")
        Dim varReturn As Boolean = False
        Try
            oConn.Open()

            Dim objCmd As New Data.SqlClient.SqlCommand("SELECT Count(*) AS 'Count' FROM Transcend.dbo.tblBData WHERE VWJob='" & JobNumber.ToString & "'  ", oConn)
            Dim objRec As Data.SqlClient.SqlDataReader = objCmd.ExecuteReader
            If objRec.HasRows Then
                While objRec.Read
                    If objRec("Count") > 0 Then
                        varReturn = True
                    End If
                End While
            End If
            objRec.Close()
            objRec = Nothing
            objCmd = Nothing
            If varReturn = True Then
                Dim objCmd1 As New Data.SqlClient.SqlCommand("DELETE Transcend.dbo.tblBData WHERE VWJob='" & JobNumber.ToString & "'", oConn)
                objCmd1.ExecuteNonQuery()
            End If

        Catch ex As Exception

        Finally
            If oConn.State <> Data.ConnectionState.Closed Then
                oConn.Close()
                oConn = Nothing
            End If
        End Try
        Return varReturn
    End Function
    Protected Sub SearchData(ByVal Sort As String, ByVal Dir As String)
        If Not String.IsNullOrEmpty(txtStartDate.Text) And Not String.IsNullOrEmpty(txtEndDate.Text) Then
            Dim oConn As New Data.SqlClient.SqlConnection
            oConn.ConnectionString = System.Configuration.ConfigurationManager.AppSettings("ETSCon")

            Try
                Dim varQuery As String = String.Empty
                'changes in query as per request from naveen/sushil
                'varQuery = "SELECT CDescription,MTID AS 'TranscendID',Resource_Name AS 'UserName',vWJob AS 'JobNo',ISNULL(FirstName,'')+' '+ISNULL(LastName,'') AS 'Name',Length,PR_LINES,B.DateReturned,Status AS 'MTStatus',FinalStatus AS 'BillingStatus',TranscriptionMethod AS 'TMethod' FROM Transcend.dbo.tblBData B LEFT OUTER JOIN Transcend.dbo.tblMapping M ON B.MTID=M.ID LEFT OUTER JOIN Transcend.dbo.tblLinesData L ON B.vWJob=L.JobNO WHERE B.DateReturned BETWEEN '" & txtStartDate.Text & "' AND '" & DateAdd(DateInterval.Day, 1, CDate(txtEndDate.Text)) & "' ORDER BY " & Sort & " " & Dir & ""
                varQuery = "SELECT Distinct CDescription,M.Resource_Name AS MTID,M.ID AS MLSID,U.UserName AS QAID,vWJob AS 'JobNo',Length,PR_LINES,B.DateReturned,Status,TranscriptionMethod AS 'TMethod' FROM Transcend.dbo.tblBData B LEFT OUTER JOIN Transcend.dbo.tblLinesDataQA L ON B.vWJob=L.JobNO LEFT OUTER JOIN DBO.tblusers U ON L.UserID=U.UserID LEFT OUTER JOIN Transcend.dbo.tblMapping M ON M.ID=B.MTID WHERE M.Resource_Name <> 'E0003' AND (M.IsDeleted = 0 OR M.Isdeleted is null) AND " & _
                " Datediff (Day, B.DateReturned,'" & txtEndDate.Text & "') BETWEEN 0 AND DateDiff(Day, '" & txtStartDate.Text & "', '" & txtEndDate.Text & "') " & IIf(String.IsNullOrEmpty(txtJobNo.Text.ToString), String.Empty, " AND L.JobNo=" & txtJobNo.Text.ToString & " ") & " ORDER BY " & Sort & " " & Dir & ""
                'Response.Write(varQuery)
                'Response.End()


                Dim objDA As New System.Data.SqlClient.SqlDataAdapter(varQuery, oConn)
                Dim objDS As New System.Data.DataSet

                objDA.Fill(objDS, "tblLinesData")
                If objDS.Tables(0).Rows.Count > 0 Then
                    GrdViewData.DataSource = objDS
                    GrdViewData.DataBind()

                    lblLineCount.Text = String.Empty
                    lblLineCount.Text = "Total Lines : " & Format(CDbl(objDS.Tables(0).Compute("Sum(PR_Lines)", "")), "00.00")
                Else
                    lblResponse.Text = String.Empty
                    lblResponse.Text = "No Records found"
                    GrdViewData.DataSource = Nothing
                    GrdViewData.DataBind()
                    lblLineCount.Text = String.Empty
                End If

                objDS = Nothing
                objDA = Nothing
            Catch ex As Exception
                Response.Write(ex.Message)
            Finally
                If oConn.State <> Data.ConnectionState.Closed Then
                    oConn.Close()
                    oConn = Nothing
                End If
            End Try
        Else
            lblResponse.Text = String.Empty
            lblResponse.Text = "Please select date criteria"
            Exit Sub
        End If
    End Sub
    Private Property GridViewSortDirection() As SortDirection
        Get
            If ViewState("sortDirection") Is Nothing Then
                ViewState("sortDirection") = SortDirection.Ascending
            End If
            Return DirectCast(ViewState("sortDirection"), SortDirection)
        End Get
        Set(ByVal value As SortDirection)
            ViewState("sortDirection") = value
        End Set
    End Property
    Protected Sub GrdViewData_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles GrdViewData.PageIndexChanging
        SearchData(Hsort.Value, Horder.Value)
        GrdViewData.PageIndex = e.NewPageIndex
        GrdViewData.DataBind()
    End Sub
    Protected Sub GrdViewData_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles GrdViewData.RowCommand
        If Trim(UCase(e.CommandName)) = Trim(UCase("Remove")) And Not String.IsNullOrEmpty(e.CommandArgument.ToString) Then
            Dim ConString As String = System.Configuration.ConfigurationManager.AppSettings("ETSCon")
            Dim oConn As New Data.SqlClient.SqlConnection
            oConn.ConnectionString = ConString
            Try
                oConn.Open()
                Dim objCmd As Data.SqlClient.SqlCommand
                objCmd = New Data.SqlClient.SqlCommand("DELETE FROM TRANSCEND.DBO.tblLinesDataQA WHERE JobNo=" & e.CommandArgument.ToString & "", oConn)
                If objCmd.ExecuteNonQuery = 1 Then
                    lblResponse.Text = String.Empty
                    lblResponse.Text = "Record Removed"
                    GrdViewData.DataSource = Nothing
                    GrdViewData.DataBind()
                    lblResponse.Text = String.Empty

                End If
            Catch ex As Exception
                'Response.Write(ex.Message)
            Finally
                If oConn.State <> Data.ConnectionState.Closed Then
                    oConn.Close()
                    oConn = Nothing
                End If
            End Try
        End If
    End Sub
    Protected Sub GrdViewData_Sorting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewSortEventArgs) Handles GrdViewData.Sorting
        Dim sortExpression As String = e.SortExpression
        ViewState("SortExpression") = sortExpression
        If GridViewSortDirection = SortDirection.Ascending Then
            GridViewSortDirection = SortDirection.Descending
            Hsort.Value = sortExpression
            Horder.Value = " DESC"
        Else
            GridViewSortDirection = SortDirection.Ascending
            Hsort.Value = sortExpression
            Horder.Value = " ASC"
        End If
        SearchData(Hsort.Value, Horder.Value)
    End Sub
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'Response.Write(Server.MapPath("/Transcend_Files/"))
        lblResponse.Text = String.Empty
        ErrLabel.Text = String.Empty
        If Hsort.Value = "" Then
            Hsort.Value = "DateReturned"
        End If
        If Horder.Value = "" Then
            Horder.Value = " DESC"
        End If
    End Sub
    Protected Sub btnSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        lblResponse.Text = String.Empty
        ErrLabel.Text = String.Empty
        SearchData(Hsort.Value, Horder.Value)
    End Sub
    Public Shared Sub DataGridToExcel(ByVal dgExport As DataGrid, ByVal response As HttpResponse, Optional ByVal argHeader As String = "", Optional ByVal argSubHead As String = "")
        Try
            Dim stringWrite As New System.IO.StringWriter()                 'create a string writer
            Dim htmlWrite As New System.Web.UI.HtmlTextWriter(stringWrite)  'create an htmltextwriter which uses the stringwriter
            Dim dg As New DataGrid()
            response.Clear()                                                'clean up the response.object
            response.Charset = ""

            Dim filename = "Transcend Report " & Now & " .xls"
            response.AddHeader("content-disposition", "attachment;filename=" & filename)

            response.ContentType = "application/vnd.ms-excel"               'set the response mime type for excel
            dg = dgExport                                                   'set the input datagrid = to the new dg grid
            'dg.GridLines = GridLines.Both                                    'no gridlines
            dg.HeaderStyle.Font.Bold = True                                 'header text bold
            dg.HeaderStyle.ForeColor = System.Drawing.Color.Crimson             'change colors etc...
            'dg.ItemStyle.ForeColor = System.Drawing.Color.Black
            dg.DataBind()                                                   'bind modified grid
            dg.RenderControl(htmlWrite)                                     'render datagrid to htmltextwriter
            'response.Write("<h4>" & argHeader & "</h4>")                    'output the html with header and footer
            'response.Write("<b>" & argSubHead & "</b>")
            response.Write(stringWrite.ToString())
            'response.Write("-- end of report --")
            response.End()
        Catch ex As Exception
        End Try
    End Sub
    Protected Sub LnlExport_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles LnlExport.Click
        If GrdViewData.Rows.Count > 0 Then
            Dim SQLString As String = String.Empty
            Dim ConString As String = System.Configuration.ConfigurationManager.AppSettings("ETSCon")
            Dim oConn As New Data.SqlClient.SqlConnection
            oConn.ConnectionString = ConString
            Try
                oConn.Open()
                Dim varQueryString As String = String.Empty

                SQLString = "SELECT CDescription AS 'Vendor',CustomerID,WorkType,Description AS 'Template',vWJob AS 'JobNo',Length,PR_LINES,DateDictated,DueDate,B.DateReturned,DateCompleted,DatePrinted,Delivered_TAT,Delivered_Within_TAT,Expected_TAT,Difference,TranscriptionMethod,M.Resource_Name AS MTID,M.ID AS 'MLSID',U.UserName AS QAID,Status,L.UpdatedOn AS 'QADate' ,TranscriptionMethod AS 'TMethod' FROM Transcend.dbo.tblBData B LEFT OUTER JOIN Transcend.dbo.tblLinesDataQA L ON B.vWJob=L.JobNO LEFT OUTER JOIN DBO.tblusers U ON L.UserID=U.UserID LEFT OUTER JOIN Transcend.dbo.tblMapping M ON M.ID=B.MTID WHERE M.Resource_Name <> 'E0003' AND (M.IsDeleted = 0 OR M.Isdeleted is null) AND B.DateReturned BETWEEN '" & txtStartDate.Text & "' AND '" & DateAdd(DateInterval.Day, 1, CDate(txtEndDate.Text)) & "'" & IIf(String.IsNullOrEmpty(txtJobNo.Text.ToString), String.Empty, " AND L.JobNo=" & txtJobNo.Text.ToString & "")

                Dim objDA As New System.Data.SqlClient.SqlDataAdapter(SQLString, oConn)
                Dim objDS As New System.Data.DataSet()

                objDA.Fill(objDS)

                dgResultsData.DataSource = objDS.Tables(0)
                dgResultsData.DataBind()

                Dim mHeader As String = "Transcend Mapping List"
                Dim mSubHead As String = "Printed by: " & Session("UserName") & "<br>Data as at: " & Now()
                DataGridToExcel(dgResultsData, Response, mHeader, mSubHead)
                Response.End()
            Catch ex As Exception
                'Response.Write(ex.Message)
            Finally
                If oConn.State <> Data.ConnectionState.Closed Then
                    oConn.Close()
                    oConn = Nothing
                End If
            End Try
        Else
            Exit Sub
        End If
    End Sub
    Protected Function SetLines(ByVal PR_Lines, ByVal Status) As String
        Dim varReturn As String = String.Empty
        Try
            If Not String.IsNullOrEmpty(PR_Lines) Then
                If Trim(UCase(Status)) = Trim(UCase("QA")) Then
                    varReturn = Format(CDbl(PR_Lines * 2), "00.00")
                Else
                    varReturn = Format(CDbl(PR_Lines), "00.00")
                End If
            End If
        Catch ex As Exception
            'Response.Write(ex.Message)
        End Try
        Return varReturn
    End Function
    Protected Function GetBillingStatus(ByVal MTStatus, ByVal BStatus, ByVal TMethod) As String
        Dim varReturn As String = String.Empty
        Try
            varReturn = BStatus
            If Not String.IsNullOrEmpty(BStatus) Then
                If Trim(UCase(BStatus)) = Trim(UCase("MT+")) Then
                    If Not String.IsNullOrEmpty(TMethod) And Trim(UCase(TMethod)) = Trim(UCase("E")) Then
                        varReturn = "QA"
                    End If
                End If
            End If
        Catch ex As Exception
        End Try
        Return varReturn
    End Function
    Protected Function GetBillingStatusNew(ByVal MTStatus, ByVal QAStatus, ByVal TMethod) As String
        'Response.Write("M:" & MTStatus & " Q:" & QAStatus & " T:" & TMethod & "<BR>")
        Dim varReturn As String = String.Empty
        Try
            varReturn = MTStatus
            If Not String.IsNullOrEmpty(MTStatus) Then
                If Trim(UCase(TMethod)) = Trim(UCase("B")) Then
                    Select Case MTStatus
                        Case "Blank Busting"
                            varReturn = "MTB"
                        Case "Direct"
                            varReturn = "MT+"
                        Case "Full Review/Indirect"
                            varReturn = "MT"
                        Case "Training Review"
                            varReturn = "MT"
                        Case "Indirect"
                            varReturn = "MT"
                    End Select
                ElseIf Trim(UCase(TMethod)) = Trim(UCase("E")) Then
                    Select Case MTStatus
                        Case "Blank Busting"
                            varReturn = "QABSE"
                        Case "Direct"
                            varReturn = "QA"
                        Case "Full Review/Indirect"
                            varReturn = "MT"
                        Case "Training Review"
                            varReturn = "MT"
                        Case "Indirect"
                            varReturn = "MT"
                    End Select
                End If
            Else
                varReturn = QAStatus
            End If
        Catch ex As Exception
        End Try
        Return varReturn
    End Function
    Protected Sub BtnGenerate_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtnGenerate.Click
        If System.IO.File.Exists(hdnFileName.Value.ToString) And Not String.IsNullOrEmpty(hdnFileName.Value.ToString) Then
            UpdateData(hdnFileName.Value.ToString)
        Else
            ErrLabel.Text = String.Empty
            ErrLabel.Text = "File not uploaded "
            Exit Sub
        End If
    End Sub
End Class
