Partial Class Transcend_HBA
    Inherits System.Web.UI.Page
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not Page.IsPostBack Then
                'BindData()
                If String.IsNullOrEmpty(hdnUserID.Value) Then
                    If Not String.IsNullOrEmpty(Session("UserID").ToString) Then
                        hdnUserID.Value = Session("UserID").ToString
                    End If
                End If
            End If

            If Hsort.Value = "" Then
                Hsort.Value = "UpdatedOn"
            End If
            If Horder.Value = "" Then
                Horder.Value = " DESC"
            End If
        Catch ex As Exception
        End Try
    End Sub
    
    Protected Sub btnSubmit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSubmit.Click
        lblLineCount.Text = String.Empty
        lblMsg.Text = String.Empty
        Dim ConString As String
        'If ddlQStatus.SelectedIndex > 0 Then
        '    lblMsg.Text = String.Empty
        '    lblMsg.Text = "Please select only one status either mtstatus or qastatus "
        '    EmptyFields()
        '    Exit Sub
        'End If
        ConString = System.Configuration.ConfigurationManager.AppSettings("ETSCon")
        Dim oConn As New Data.SqlClient.SqlConnection
        oConn.ConnectionString = ConString
        Try
            If Not String.IsNullOrEmpty(hdnUserID.Value) Then
                If CheckTransID() Then
                    'If CheckJobID() Then
                    'Response.Write("<script language='javascript'>alert('Job No details already exist,please check job no');</script>")
                    'lblMsg.Text = String.Empty
                    'lblMsg.Text = "Job No details already exist,please check job no"
                    'Exit Sub
                    'Else
                    
                    Dim varJobNo As String = String.Empty
                    varJobNo = Trim(txtJobNo.Text)

                   


                    Dim varStatus As String = String.Empty
                    Dim varQStatus As String = String.Empty
                   
                    If ddlQStatus.SelectedIndex > 0 Then
                        varQStatus = ddlQStatus.Items(ddlQStatus.SelectedIndex).Value.ToString
                    End If

                   

                    If Not String.IsNullOrEmpty(varQStatus) Then
                        If CheckQAStatus() Then
                            lblMsg.Text = String.Empty
                            lblMsg.Text = "QA Status already updated for this job no"
                            Exit Sub
                        End If
                    End If

                    If Not String.IsNullOrEmpty(varQStatus) Then
                        'Dim varFinalStatus As String = String.Empty
                        'varFinalStatus = ddlStatus.Items(ddlStatus.SelectedIndex).Value.ToString


                        Dim varQuery As String = String.Empty
                        'varQuery = "INSERT INTO TRANSCEND.DBO.tblLinesData (UserID" & IIf(String.IsNullOrEmpty(varAccName), String.Empty, ",AccID") & " ,JobNo " & IIf(varLines > 0, ",Lines", String.Empty) & ",Status "& IIf(String.IsNullOrEmpty(varVRS),String.Empty,",VRS") &" ,UpdatedOn ) VALUES ('" & hdnUserID.Value.ToString & "'" & IIf(String.IsNullOrEmpty(varAccName), String.Empty, "," & "'" & varAccName.ToString & "'") & " ,'" & varJobNo.ToString & "'" & IIf(varLines > 0, "," & varLines & "", String.Empty) & ",'" & varStatus.ToString & "'" & IIf(String.IsNullOrEmpty(varVRS),String.Empty,"," & varVRS.ToString &" ) "" & ,'" & '" & varFinalStatus.ToString & "','" & Now() & "')"
                        varQuery = "INSERT INTO TRANSCEND.DBO.tblLinesData (UserID,JobNo,QAStatus, UpdatedOn ) VALUES ('" & hdnUserID.Value.ToString & "','" & varJobNo.ToString & "','" & varQStatus & "','" & Now() & "')"
                        oConn.Open()

                        Dim objCmd As New Data.SqlClient.SqlCommand(varQuery, oConn)
                        If objCmd.ExecuteNonQuery = 1 Then
                            'Response.Write("<script language='javascript'>alert('Record submitted ');window.location.href='HBA.aspx';</script>")
                            lblMsg.Text = String.Empty
                            lblMsg.Text = "Record submitted "
                            EmptyFields()
                            Exit Sub
                        End If
                    Else
                        lblMsg.Text = String.Empty
                        lblMsg.Text = "Please select status either mtstatus or qastatus "
                        EmptyFields()
                        Exit Sub
                    End If
                    'End If
                Else
                    'Response.Write("<script language='javascript'>alert('Transcend id not assigned,please contact to edictate mtsupport');</script>")
                    lblMsg.Text = String.Empty
                    lblMsg.Text = "Transcend id not assigned,please contact to edictate mtsupport"
                    Exit Sub
                End If
            Else
                'Response.Write("<script language='javascript'>alert('Please login again');</script>")
                lblMsg.Text = String.Empty
                lblMsg.Text = "Please login again"
                Exit Sub
            End If
        Catch ex As Exception
            'Response.Write(ex.Message)
        Finally
            If oConn.State <> Data.ConnectionState.Closed Then
                oConn.Close()
                oConn = Nothing
            End If
        End Try
    End Sub
    Protected Sub EmptyFields()
        Try
           
            txtJobNo.Text = String.Empty
        Catch ex As Exception
        End Try
    End Sub
    Protected Function CheckTransID() As Boolean
        Dim varReturn As Boolean = False
        Dim oConn As New Data.SqlClient.SqlConnection
        oConn.ConnectionString = System.Configuration.ConfigurationManager.AppSettings("ETSCon")

        Try
            oConn.Open()
            Dim objCmd As New Data.SqlClient.SqlCommand("SELECT ID AS 'TranscendID' FROM TRANSCEND.DBO.tblMapping WHERE UserID='" & hdnUserID.Value.ToString & "'", oConn)
            Dim objRec As Data.SqlClient.SqlDataReader = objCmd.ExecuteReader

            If objRec.HasRows Then
                While objRec.Read
                    If Not objRec.IsDBNull(objRec.GetOrdinal("TranscendID")) Then
                        varReturn = True
                        Exit While
                    End If
                End While
            End If

            objRec.Close()
            objRec = Nothing
            objCmd = Nothing

        Catch ex As Exception
        Finally
            If oConn.State <> Data.ConnectionState.Closed Then
                oConn.Close()
                oConn = Nothing
            End If
        End Try
        Return varReturn
    End Function
    Protected Function CheckMTStatus() As Boolean
        Dim varReturn As Boolean = False
        Dim oConn As New Data.SqlClient.SqlConnection
        oConn.ConnectionString = System.Configuration.ConfigurationManager.AppSettings("ETSCon")

        Try
            oConn.Open()
            Dim objCmd As New Data.SqlClient.SqlCommand("SELECT Count(*) AS 'Count' FROM TRANSCEND.DBO.tblLinesData WHERE JobNo='" & Trim(txtJobNo.Text.ToString) & "' AND Status IS NOT NULL", oConn)
            'Response.Write(objCmd.CommandText)
            Dim objRec As Data.SqlClient.SqlDataReader = objCmd.ExecuteReader

            If objRec.HasRows Then
                While objRec.Read
                    If Not objRec.IsDBNull(objRec.GetOrdinal("Count")) Then
                        If objRec("Count") > 0 Then
                            varReturn = True
                        End If
                    End If
                End While
            End If

            objRec.Close()
            objRec = Nothing
            objCmd = Nothing

        Catch ex As Exception
        Finally
            If oConn.State <> Data.ConnectionState.Closed Then
                oConn.Close()
                oConn = Nothing
            End If
        End Try
        Return varReturn
    End Function
    Protected Function CheckQAStatus() As Boolean
        Dim varReturn As Boolean = False
        Dim oConn As New Data.SqlClient.SqlConnection
        oConn.ConnectionString = System.Configuration.ConfigurationManager.AppSettings("ETSCon")

        Try
            oConn.Open()
            Dim objCmd As New Data.SqlClient.SqlCommand("SELECT Count(*) AS 'Count' FROM TRANSCEND.DBO.tblLinesData WHERE JobNo='" & Trim(txtJobNo.Text.ToString) & "' AND QAStatus IS NOT NULL", oConn)
            'Response.Write(objCmd.CommandText)
            Dim objRec As Data.SqlClient.SqlDataReader = objCmd.ExecuteReader

            If objRec.HasRows Then
                While objRec.Read
                    If Not objRec.IsDBNull(objRec.GetOrdinal("Count")) Then
                        If objRec("Count") > 0 Then
                            varReturn = True
                        End If
                    End If
                End While
            End If

            objRec.Close()
            objRec = Nothing
            objCmd = Nothing

        Catch ex As Exception
        Finally
            If oConn.State <> Data.ConnectionState.Closed Then
                oConn.Close()
                oConn = Nothing
            End If
        End Try
        Return varReturn
    End Function
    Protected Function CheckJobID() As Boolean
        Dim varReturn As Boolean = False
        Dim oConn As New Data.SqlClient.SqlConnection
        oConn.ConnectionString = System.Configuration.ConfigurationManager.AppSettings("ETSCon")

        Try
            oConn.Open()
            Dim objCmd As New Data.SqlClient.SqlCommand("SELECT Count(*) AS 'Count' FROM TRANSCEND.DBO.tblLinesData WHERE JobNo='" & Trim(txtJobNo.Text.ToString) & "'", oConn)
            'Response.Write(objCmd.CommandText)
            Dim objRec As Data.SqlClient.SqlDataReader = objCmd.ExecuteReader

            If objRec.HasRows Then
                While objRec.Read
                    If Not objRec.IsDBNull(objRec.GetOrdinal("Count")) Then
                        If objRec("Count") > 0 Then
                            varReturn = True
                        End If
                    End If
                End While
            End If

            objRec.Close()
            objRec = Nothing
            objCmd = Nothing

        Catch ex As Exception
        Finally
            If oConn.State <> Data.ConnectionState.Closed Then
                oConn.Close()
                oConn = Nothing
            End If
        End Try
        Return varReturn
    End Function
    Protected Sub btnSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        lblLineCount.Text = String.Empty
        lblMsg.Text = String.Empty
        SearchData(Hsort.Value, Horder.Value)
    End Sub
    Protected Sub SearchData(ByVal Sort As String, ByVal Dir As String)
        If Not String.IsNullOrEmpty(txtStartDate.Text) And Not String.IsNullOrEmpty(txtEndDate.Text) Then
            If Not String.IsNullOrEmpty(hdnUserID.Value) Then
                Dim oConn As New Data.SqlClient.SqlConnection
                oConn.ConnectionString = System.Configuration.ConfigurationManager.AppSettings("ETSCon")

                Try
                    Dim varQuery As String = String.Empty
                    varQuery = " SELECT L.JobNo,L.QAStatus, L.UpdatedON FROM TRANSCEND.DBO.tblLinesData L WHERE UserID ='" & hdnUserID.Value.ToString & "' AND L.UpdatedOn BETWEEN '" & txtStartDate.Text & "' AND '" & DateAdd(DateInterval.Day, 1, CDate(txtEndDate.Text)).ToString & "'" & _
                               " ORDER BY " & Sort & " " & Dir & " "
                    'Response.Write(varQuery)
                    'Dim objDA As New System.Data.SqlClient.SqlDataAdapter("SELECT L.*,PR_Lines,AccName FROM TRANSCEND.DBO.tblLinesData L INNER JOIN TRANSCEND.DBO.tblAccounts A ON L.AccID=A.AccID LEFT OUTER JOIN TRANSCEND.DBO.tblBData B ON L.JobNo=B.VWJob WHERE UserID='" & hdnUserID.Value.ToString & "' AND L.UpdatedOn BETWEEN '" & txtStartDate.Text & "' AND '" & DateAdd(DateInterval.Day, 1, CDate(txtEndDate.Text)).ToString & "' ORDER BY " & Sort & " " & Dir & "", oConn)
                    'Response.Write("SELECT L.*,PR_Lines FROM TRANSCEND.DBO.tblLinesData L LEFT OUTER JOIN TRANSCEND.DBO.tblBData B ON L.JobNo=B.VWJob WHERE UserID='" & hdnUserID.Value.ToString & "' AND L.UpdatedOn BETWEEN '" & txtStartDate.Text & "' AND '" & txtEndDate.Text & "' ORDER BY " & Sort & " " & Dir & "")
                    Dim objDA As New System.Data.SqlClient.SqlDataAdapter(varQuery, oConn)
                    Dim objDS As New System.Data.DataSet

                    objDA.Fill(objDS, "tblLinesData")

                    If objDS.Tables(0).Rows.Count > 0 Then
                        GrdViewData.DataSource = objDS
                        GrdViewData.DataBind()
                        'Dim varLng As Double = 0.0

                        'For Each dRow As Data.DataRow In objDS.Tables(0).Rows
                        '    If Not dRow.IsNull("Lines") Or Not dRow.IsNull("PR_Lines") Then
                        '        varLng = varLng + SetLines(dRow("Lines").ToString, dRow("PR_Lines").ToString, dRow("Status").ToString)
                        '    End If
                        'Next
                        'lblLineCount.Text = String.Empty
                        'lblLineCount.Text = "Total Lines : " & Format(CDbl(varLng), "00.00")
                    Else
                        lblMsg.Text = String.Empty
                        lblMsg.Text = "Record no found"
                    End If

                    objDS = Nothing
                    objDA = Nothing

                Catch ex As Exception
                    'Response.Write(ex.Message)
                Finally
                    If oConn.State <> Data.ConnectionState.Closed Then
                        oConn.Close()
                        oConn = Nothing
                    End If
                End Try
            Else
                lblMsg.Text = String.Empty
                lblMsg.Text = "Please login again"
                Exit Sub
            End If
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
    Protected Function SetLines(ByVal Lines, ByVal PR_Lines, ByVal Status) As String
        Dim varReturn As String = String.Empty
        Try
            If Not String.IsNullOrEmpty(PR_Lines) Then
                'If Trim(UCase(Status)) = Trim(UCase("QAB")) Then
                '    varReturn = Format(CDbl(PR_Lines * 0.5), "00.00")
                'Else
                '    varReturn = Format(CDbl(PR_Lines), "00.00")
                'End If
                varReturn = Format(CDbl(PR_Lines), "00.00")
            Else
                If Not String.IsNullOrEmpty(CStr(Lines)) Then
                    'If Trim(UCase(Status)) = Trim(UCase("QAB")) Then
                    '    varReturn = Format(CDbl(Lines * 0.5), "00.00")
                    'Else
                    '    varReturn = Format(CDbl(Lines), "00.00")
                    'End If
                    varReturn = Format(CDbl(Lines), "00.00")
                End If
            End If
            If String.IsNullOrEmpty(Lines) And String.IsNullOrEmpty(PR_Lines) Then
                varReturn = String.Empty
            End If

        Catch ex As Exception
            'Response.Write(ex.Message)
        End Try
        Return varReturn
    End Function
    Protected Function SetVRS(ByVal VRS As String) As String
        Dim varReturn As String = String.Empty
        Try
            If Not String.IsNullOrEmpty(VRS) Then
                If VRS Then
                    varReturn = "Yes"
                Else
                    varReturn = "No"
                End If
            End If
        Catch ex As Exception
        End Try
        Return varReturn
    End Function
    Protected Sub LnlExport_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles LnlExport.Click
        If GrdViewData.Rows.Count > 0 Then
            Dim SQLString As String = String.Empty
            Dim ConString As String = System.Configuration.ConfigurationManager.AppSettings("ETSCon")
            Dim oConn As New Data.SqlClient.SqlConnection
            oConn.ConnectionString = ConString
            Try
                oConn.Open()
                Dim varQueryString As String = String.Empty

                'SQLString = "SELECT L.*,PR_Lines,AccName FROM TRANSCEND.DBO.tblLinesData L INNER JOIN TRANSCEND.DBO.tblAccounts A ON L.AccID=A.AccID LEFT OUTER JOIN TRANSCEND.DBO.tblBData B ON L.JobNo=B.VWJob WHERE UserID='" & hdnUserID.Value.ToString & "' AND L.UpdatedOn BETWEEN '" & txtStartDate.Text & "' AND '" & DateAdd(DateInterval.Day, 1, CDate(txtEndDate.Text)).ToString & "'"

                SQLString = " SELECT L.JobNo,A.AccID,L.Lines,L.Status,L.QAStatus,L.VRS,AccName, NULL AS 'PR_Lines',L.UpdatedON FROM TRANSCEND.DBO.tblLinesData L INNER JOIN TRANSCEND.DBO.tblAccounts A ON L.AccID=A.AccID WHERE UserID ='" & hdnUserID.Value.ToString & "' AND L.UpdatedOn BETWEEN '" & txtStartDate.Text & "' AND '" & DateAdd(DateInterval.Day, 1, CDate(txtEndDate.Text)).ToString & "'"
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
End Class
