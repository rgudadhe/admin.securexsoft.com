Partial Class VHSLinesData
    Inherits System.Web.UI.Page
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not Page.IsPostBack Then
                If String.IsNullOrEmpty(hdnUserID.Value) Then
                    If Not String.IsNullOrEmpty(Session("UserID").ToString) Then
                        hdnUserID.Value = Session("UserID").ToString
                    End If
                End If
                BindVHSUserNames()
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
    Protected Sub BindVHSUserNames()
        Dim ConString As String

        ConString = System.Configuration.ConfigurationManager.AppSettings("ETSCon")
        Dim oConn As New Data.SqlClient.SqlConnection
        oConn.ConnectionString = ConString
        Try
            oConn.Open()
            Dim objCmd As New Data.SqlClient.SqlCommand("SELECT M.User_Name from DBO.tblUsers U INNER JOIN TRANSCEND.DBO.tblMapping M ON U.UserId=M.UserID WHERE Account='VHS' group by User_Name ORDER BY User_Name", oConn)
            Dim objRec As Data.SqlClient.SqlDataReader = objCmd.ExecuteReader
            If objRec.HasRows Then
                ddlVHS.DataSource = objRec
                ddlVHS.DataTextField = "User_Name"
                ddlVHS.DataValueField = "User_Name"
                ddlVHS.DataBind()
            End If
            objRec.Close()
            objRec = Nothing
            objCmd = Nothing
            ddlVHS.Items.Insert(0, New ListItem("Please Select", String.Empty))
        Catch ex As Exception
        Finally
            If oConn.State <> Data.ConnectionState.Closed Then
                oConn.Close()
                oConn = Nothing
            End If
        End Try
    End Sub
    Protected Sub btnSubmit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSubmit.Click
        lblLineCount.Text = String.Empty
        lblMsg.Text = String.Empty
        Dim ConString As String

        ConString = System.Configuration.ConfigurationManager.AppSettings("ETSCon")
        Dim oConn As New Data.SqlClient.SqlConnection
        oConn.ConnectionString = ConString
        Try
            If Not String.IsNullOrEmpty(hdnUserID.Value) Then
                If CheckTransID() Then

                    Dim varStatus As String = String.Empty

                    If ddlStatus.SelectedIndex > 0 Then
                        varStatus = ddlStatus.Items(ddlStatus.SelectedIndex).Value.ToString
                    End If

                    If Not String.IsNullOrEmpty(varStatus) Then
                        If CheckMTStatus() Then
                            lblMsg.Text = String.Empty
                            lblMsg.Text = "Status already updated for this job no"
                            Exit Sub
                        End If
                    End If

                    Dim varJobNo As String = String.Empty
                    varJobNo = Trim(txtJobNo.Text)

                    Dim varMTID As String = String.Empty
                    varMTID = Trim(ddlVHS.Items(ddlVHS.SelectedIndex).Value.ToString)

                    Dim varLines As String = String.Empty
                    varLines = Trim(txtLines.Text)


                    If Not String.IsNullOrEmpty(varStatus) Then
                        Dim varQuery As String = String.Empty
                        varQuery = "INSERT INTO TRANSCEND.DBO.tblVHSLinesData (UserID,JobNo,MTID,Lines,Status,UpdatedOn) VALUES ('" & hdnUserID.Value.ToString & "'," & varJobNo.ToString & ",'" & varMTID.ToString & "'," & varLines.ToString & ",'" & varStatus.ToString & "','" & Now() & "')"
                        oConn.Open()

                        Dim objCmd As New Data.SqlClient.SqlCommand(varQuery, oConn)
                        If objCmd.ExecuteNonQuery = 1 Then
                            lblMsg.Text = String.Empty
                            lblMsg.Text = "Record submitted "
                            EmptyFields()
                            Exit Sub
                        End If
                    Else
                        lblMsg.Text = String.Empty
                        lblMsg.Text = "Please select status status"
                        EmptyFields()
                        Exit Sub
                    End If
                    'End If
                Else
                    lblMsg.Text = String.Empty
                    lblMsg.Text = "Transcend id not assigned,please contact to edictate mtsupport"
                    Exit Sub
                End If
            Else
                lblMsg.Text = String.Empty
                lblMsg.Text = "Please login again"
                Exit Sub
            End If
        Catch ex As Exception
            Response.Write(ex.Message)
        Finally
            If oConn.State <> Data.ConnectionState.Closed Then
                oConn.Close()
                oConn = Nothing
            End If
        End Try
    End Sub
    Protected Sub EmptyFields()
        Try
            ddlStatus.SelectedIndex = -1
            txtJobNo.Text = String.Empty
            txtLines.Text = String.Empty
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
            Dim objCmd As New Data.SqlClient.SqlCommand("SELECT Count(*) AS 'Count' FROM TRANSCEND.DBO.tblVHSLinesData WHERE JobNo='" & Trim(txtJobNo.Text.ToString) & "' AND Status IS NOT NULL", oConn)
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
                    varQuery = " SELECT JobNo,MTID,Status,Lines,UpdatedON FROM TRANSCEND.DBO.tblVHSLinesData WHERE UserID='" & hdnUserID.Value.ToString & "' AND UpdatedOn BETWEEN '" & txtStartDate.Text & "' AND '" & DateAdd(DateInterval.Day, 1, CDate(txtEndDate.Text)).ToString & "'  " & _
                               " ORDER BY " & Sort & " " & Dir & " "

                    Dim objDA As New System.Data.SqlClient.SqlDataAdapter(varQuery, oConn)
                    Dim objDS As New System.Data.DataSet

                    objDA.Fill(objDS, "tblVHSLinesData")

                    If objDS.Tables(0).Rows.Count > 0 Then
                        GrdViewData.DataSource = objDS
                        GrdViewData.DataBind()
                        Dim varLng As Double = 0.0

                        For Each dRow As Data.DataRow In objDS.Tables(0).Rows
                            If Not dRow.IsNull("Lines") Then
                                varLng = varLng + Format(CDbl(dRow("Lines")), "00.00")
                            End If
                        Next
                        lblLineCount.Text = String.Empty
                        lblLineCount.Text = "Total Lines : " & Format(CDbl(varLng), "00.00")
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

                SQLString = " SELECT JobNo,MTID,Status,Lines,UpdatedON FROM TRANSCEND.DBO.tblVHSLinesData WHERE UserID='" & hdnUserID.Value.ToString & "' AND UpdatedOn BETWEEN '" & txtStartDate.Text & "' AND '" & DateAdd(DateInterval.Day, 1, CDate(txtEndDate.Text)).ToString & "'  "

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

            Dim filename = "VHS Lines Report " & Now & " .xls"
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
