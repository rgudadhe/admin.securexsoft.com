
Partial Class Transcend_ViewVHSLinesData
    Inherits BasePage
    Protected Sub btnSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        lblResponse.Text = String.Empty
        SearchData(Hsort.Value, Horder.Value)
    End Sub
    Protected Sub SearchData(ByVal Sort As String, ByVal Dir As String)
        If Not String.IsNullOrEmpty(txtStartDate.Text) And Not String.IsNullOrEmpty(txtEndDate.Text) Then
            Dim oConn As New Data.SqlClient.SqlConnection
            oConn.ConnectionString = System.Configuration.ConfigurationManager.AppSettings("ETSCon")

            Try
                Dim varQuery As String = String.Empty
                'changes in query as per request from naveen/sushil
                'varQuery = "SELECT CDescription,MTID AS 'TranscendID',Resource_Name AS 'UserName',vWJob AS 'JobNo',ISNULL(FirstName,'')+' '+ISNULL(LastName,'') AS 'Name',Length,PR_LINES,B.DateReturned,Status AS 'MTStatus',FinalStatus AS 'BillingStatus',TranscriptionMethod AS 'TMethod' FROM Transcend.dbo.tblBData B LEFT OUTER JOIN Transcend.dbo.tblMapping M ON B.MTID=M.ID LEFT OUTER JOIN Transcend.dbo.tblLinesData L ON B.vWJob=L.JobNO WHERE B.DateReturned BETWEEN '" & txtStartDate.Text & "' AND '" & DateAdd(DateInterval.Day, 1, CDate(txtEndDate.Text)) & "' ORDER BY " & Sort & " " & Dir & ""
                varQuery = "SELECT V.*,ISNULL(U.FirstName,'')+' '+ IsNULL(U.LastName,'') AS Name,UserName  FROM TRANSCEND.DBO.tblVHSLinesData V INNER JOIN DBO.tblUsers U ON V.UserID=U.UserID WHERE " & _
                " V.UpdatedOn BETWEEN '" & txtStartDate.Text & "' AND '" & DateAdd(DateInterval.Day, 1, CDate(txtEndDate.Text)) & "' " & IIf(String.IsNullOrEmpty(txtJobNo.Text.ToString), String.Empty, " AND V.JobNo=" & txtJobNo.Text.ToString & " ") & " ORDER BY " & Sort & " " & Dir & ""
                'Response.Write(varQuery)

                Dim objDA As New System.Data.SqlClient.SqlDataAdapter(varQuery, oConn)
                Dim objDS As New System.Data.DataSet

                objDA.Fill(objDS, "tblVHSLinesData")
                If objDS.Tables(0).Rows.Count > 0 Then
                    GrdViewData.DataSource = objDS
                    GrdViewData.DataBind()

                    lblLineCount.Text = String.Empty
                    lblLineCount.Text = "Total Lines : " & Format(CDbl(objDS.Tables(0).Compute("Sum(Lines)", "")), "00.00")
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

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'Response.Write(Server.MapPath("/Transcend_Files/"))
        lblResponse.Text = String.Empty
        If Hsort.Value = "" Then
            Hsort.Value = "UpdatedOn"
        End If
        If Horder.Value = "" Then
            Horder.Value = " DESC"
        End If
    End Sub
    Public Shared Sub DataGridToExcel(ByVal dgExport As DataGrid, ByVal response As HttpResponse, Optional ByVal argHeader As String = "", Optional ByVal argSubHead As String = "")
        Try
            Dim stringWrite As New System.IO.StringWriter()                 'create a string writer
            Dim htmlWrite As New System.Web.UI.HtmlTextWriter(stringWrite)  'create an htmltextwriter which uses the stringwriter
            Dim dg As New DataGrid()
            response.Clear()                                                'clean up the response.object
            response.Charset = ""

            Dim filename = "VHS Report " & Now & " .xls"
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
                objCmd = New Data.SqlClient.SqlCommand("DELETE FROM TRANSCEND.DBO.tblVHSLinesData WHERE JobNo=" & e.CommandArgument.ToString & "", oConn)
                If objCmd.ExecuteNonQuery = 1 Then
                    lblResponse.Text = String.Empty
                    lblResponse.Text = "Record Removed"
                    GrdViewData.DataSource = Nothing
                    GrdViewData.DataBind()
                    'lblResponse.Text = String.Empty
                    SearchData(Hsort.Value, Horder.Value)
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

    Protected Sub LnlExport_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles LnlExport.Click
        If GrdViewData.Rows.Count > 0 Then
            Dim SQLString As String = String.Empty
            Dim ConString As String = System.Configuration.ConfigurationManager.AppSettings("ETSCon")
            Dim oConn As New Data.SqlClient.SqlConnection
            oConn.ConnectionString = ConString
            Try
                oConn.Open()
                Dim varQueryString As String = String.Empty

                SQLString = "SELECT V.*,ISNULL(U.FirstName,'')+' '+ IsNULL(U.LastName,'') AS Name,UserName  FROM TRANSCEND.DBO.tblVHSLinesData V INNER JOIN DBO.tblUsers U ON V.UserID=U.UserID WHERE V.UpdatedOn BETWEEN '" & txtStartDate.Text & "' AND '" & DateAdd(DateInterval.Day, 1, CDate(txtEndDate.Text)) & "'" & IIf(String.IsNullOrEmpty(txtJobNo.Text.ToString), String.Empty, " AND V.JobNo=" & txtJobNo.Text.ToString & "")

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
End Class
