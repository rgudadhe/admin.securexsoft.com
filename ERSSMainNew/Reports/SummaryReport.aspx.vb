Imports AjaxControlToolkit
Imports System.Data
Imports MainModule
Partial Class SummaryReport
    Inherits BasePage
    Dim objMainModule As New MainModule
    Protected Sub btnSubmit_ServerClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSubmit.ServerClick
        Dim i As Integer
        Dim varStrIssueID As New ArrayList
        Dim varStrIssueName As New ArrayList

        Dim DSMain As New DataSet
        Dim DTGrp As New DataTable
        Dim DTSearchParam As New DataTable
        Dim objRecSummary As DataTableReader
        Dim objTicketDetailsRec As DataTableReader
        Dim DVTemp As DataView
        Dim clsERSS As ETS.BL.ERSS

        Try
            Table1.Visible = True

            clsERSS = New ETS.BL.ERSS()

            DTSearchParam = New DataTable
            DTSearchParam.Columns.Add(New DataColumn("ContractorID"))
            DTSearchParam.Columns.Add(New DataColumn("StartDate"))
            DTSearchParam.Columns.Add(New DataColumn("EndDate"))
            DTSearchParam.Columns.Add(New DataColumn("WorkGroupID"))

            Dim DR As Data.DataRow = DTSearchParam.NewRow

            DR("ContractorID") = Session("ContractorID").ToString
            DR("StartDate") = Request.Form("txtStartDate").ToString
            DR("EndDate") = Request.Form("txtEndDate").ToString
            DR("WorkGroupID") = Session("WorkGroupID").ToString

            DTSearchParam.Rows.Add(DR)

            DSMain = clsERSS.GetSummaryReport(DTSearchParam)
            DTGrp = GroupByMultiple(DSMain.Tables(0))

            objRecSummary = DTGrp.CreateDataReader
            If objRecSummary.HasRows Then
                While objRecSummary.Read
                    varStrIssueID.Add(objRecSummary.GetGuid(objRecSummary.GetOrdinal("IssueID")).ToString)
                    varStrIssueName.Add(objRecSummary.GetString(objRecSummary.GetOrdinal("IssueName")))
                End While
            End If

            objRecSummary.Close()

            For i = 0 To varStrIssueID.Count - 1
                Dim objLabel As New Label
                Dim objTable As New Table

                objLabel.Text = varStrIssueName(i).ToString
                objLabel.Font.Bold = True
                Dim varAjaxPane As AjaxControlToolkit.AccordionPane
                varAjaxPane = New AjaxControlToolkit.AccordionPane
                varAjaxPane.HeaderContainer.Controls.Add(objLabel)
                'varAjaxPane.HeaderCssClass = "accordionHeader"
                'varAjaxPane.CssClass = "accordionContent"
                varAjaxPane.HeaderContainer.BackColor = Drawing.Color.LightYellow
                MyAccordion.Panes.Add(varAjaxPane)

                objTable = New Table
                objTable.HorizontalAlign = HorizontalAlign.Center
                'objTable.GridLines = GridLines.Horizontal
                Dim objRowMain As New TableRow
                Dim objCellMain As New TableCell

                objCellMain.Text = "Ticket Details"
                objRowMain.CssClass = "HeaderDiv"
                objCellMain.ColumnSpan = 7
                objRowMain.Cells.Add(objCellMain)
                'objTable.Rows.Add(objRowMain)
                objCellMain.Font.Name = "Arial"
                objCellMain.Font.Italic = True
                objCellMain.HorizontalAlign = HorizontalAlign.Center

                Dim ObjRowHeader As TableRow = CreateTableHeaderCells()
                ObjRowHeader.CssClass = "alt1"
                objTable.Rows.Add(ObjRowHeader)
                '[SF_getERSSSummeryReport]
                DVTemp = New DataView(DSMain.Tables(0))
                DVTemp.RowFilter = "IssueID='" & varStrIssueID(i).ToString & "'"

                objTicketDetailsRec = DVTemp.ToTable().CreateDataReader()
                If objTicketDetailsRec.HasRows Then
                    While objTicketDetailsRec.Read
                        Dim varTblRow As New TableRow
                        Dim varTblCellTicketNo As New TableCell
                        Dim varTblCellDatePosted As New TableCell
                        Dim varTblCellIssueDetails As New TableCell
                        Dim varTblCellStatus As New TableCell
                        Dim varTblCellDept As New TableCell

                        varTblCellTicketNo.Text = objTicketDetailsRec.GetDecimal(objTicketDetailsRec.GetOrdinal("TicketNo"))
                        varTblCellIssueDetails.Text = objTicketDetailsRec.GetString(objTicketDetailsRec.GetOrdinal("Description"))
                        varTblCellDatePosted.Text = objTicketDetailsRec.GetDateTime(objTicketDetailsRec.GetOrdinal("DatePosted"))
                        varTblCellStatus.Text = objTicketDetailsRec.GetString(objTicketDetailsRec.GetOrdinal("Status"))

                        If Not objTicketDetailsRec.IsDBNull(objTicketDetailsRec.GetOrdinal("Name")) Then
                            varTblCellDept.Text = objTicketDetailsRec.GetString(objTicketDetailsRec.GetOrdinal("Name"))
                        Else
                            varTblCellDept.Text = "&nbsp"
                        End If

                        varTblRow.Font.Name = "Arial"
                        varTblRow.Font.Size = 8
                        varTblRow.Cells.Add(varTblCellTicketNo)
                        varTblRow.Cells.Add(varTblCellIssueDetails)
                        varTblRow.Cells.Add(varTblCellDatePosted)
                        varTblRow.Cells.Add(varTblCellStatus)
                        varTblRow.Cells.Add(varTblCellDept)

                        objTable.Rows.Add(varTblRow)
                    End While
                End If

                objTicketDetailsRec.Close()
                varAjaxPane.ContentContainer.Controls.Add(objTable)
            Next
        Catch ex As Exception

        Finally
            DSMain = Nothing
            DTGrp = Nothing
            DTSearchParam = Nothing
            objRecSummary = Nothing
            objTicketDetailsRec = Nothing
            DVTemp = Nothing
            clsERSS = Nothing
        End Try
    End Sub
    Protected Function CreateTableHeaderCells() As TableRow
        Dim ObjTicketNoCell As TableCell = New TableCell()
        ObjTicketNoCell.Width = 30
        ObjTicketNoCell.Text = "Ticket No #"
        ObjTicketNoCell.CssClass = "alt1"

        Dim ObjIDetailsCell As TableCell = New TableCell()
        ObjIDetailsCell.Width = 300
        ObjIDetailsCell.Text = "Issue Details"
        ObjIDetailsCell.CssClass = "alt1"

        Dim ObjDatePostedCell As TableCell = New TableCell()
        ObjDatePostedCell.Width = 100
        ObjDatePostedCell.Text = "Date Posted"
        ObjDatePostedCell.CssClass = "alt1"

        Dim ObjStatusCell As TableCell = New TableCell()
        ObjStatusCell.Width = 70
        ObjStatusCell.Text = "Status"
        ObjStatusCell.CssClass = "alt1"

        Dim ObjDeptCell As TableCell = New TableCell()
        ObjDeptCell.Width = 70
        ObjDeptCell.Text = "Department"
        ObjDeptCell.CssClass = "alt1"

        Dim ObjHeaderRow As TableRow = New TableRow()
        ObjHeaderRow.Cells.Add(ObjTicketNoCell)
        ObjHeaderRow.Cells.Add(ObjIDetailsCell)
        ObjHeaderRow.Cells.Add(ObjDatePostedCell)
        ObjHeaderRow.Cells.Add(ObjStatusCell)
        ObjHeaderRow.Cells.Add(ObjDeptCell)
        Return ObjHeaderRow
    End Function
    Function GroupByMultiple(ByVal i_dSourceTable As DataTable) As DataTable

        Dim dv As New DataView(i_dSourceTable)
        'Dim dtGroup As DataTable = dv.ToTable(True, i_sGroupByColumns)
        Dim dtGroup As DataTable = dv.ToTable(True, "IssueID", "IssueName")

        'dtGroup.Columns.Add("Count", GetType(Integer))

        'Dim sCondition As String
        'For Each dr As DataRow In dtGroup.Rows
        '    sCondition = ""

        '    For I = 0 To i_sGroupByColumns.Length - 1
        '        sCondition &= i_sGroupByColumns(I) & " = '" & dr(i_sGroupByColumns(I)) & "' "
        '        If I < i_sGroupByColumns.Length - 1 Then sCondition &= " AND "
        '    Next

        '    dr("Count") = i_dSourceTable.Compute("Count(" & i_sAggregateColumn & ")", sCondition)
        'Next

        Return dtGroup
    End Function
End Class
