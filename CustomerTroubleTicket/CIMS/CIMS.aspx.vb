
Partial Class CIMS_CIMS
    Inherits BasePage
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'If Not Page.IsPostBack Then
        Try
            'cpesetting.Collapsed = True
            Dim varStrQueryString As String = String.Empty
            varStrQueryString = "SELECT * FROM dbo.tblCustomerTickets WHERE Status='Open' AND AccID='" & Session("AccID").ToString() & "' ORDER BY TicketNo "
            SqlDataSource1.SelectCommand = varStrQueryString
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
        'End If
    End Sub
    Protected Sub Button2_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button2.Click
        GetData()
    End Sub
    Protected Sub MyDataGrid_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles MyDataGrid.RowDataBound
        'Response.Write(e.Row.Cells(1).Text.ToString)
        'If Trim(UCase(e.Row.Cells(4).Text)) = Trim(UCase("Close")) Then
        '    e.Row.Cells(5).Text = "Closed"
        'End If

    End Sub

    Protected Sub DropDownStatusInSearch_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles DropDownStatusInSearch.SelectedIndexChanged
        GetData()
    End Sub
    Protected Sub GetData()
        Try
            Dim varStrStatus As String = String.Empty
            Dim varLngTicketNo As Long
            Dim varStrSDate As String = String.Empty
            Dim varStrEDate As String = String.Empty
            Dim varStrPriority As String = String.Empty
            Dim varWhereClause As String = String.Empty

            Dim varStrQueryString As String = String.Empty
            varStrQueryString = "SELECT * FROM dbo.tblCustomerTickets WHERE AccID='" & Session("AccID").ToString() & "'"

            If Not String.IsNullOrEmpty(Request("DropDownStatusInSearch")) And Not Trim(UCase(Request("DropDownStatusInSearch"))) = Trim(UCase("Any")) Then
                varStrStatus = Request("DropDownStatusInSearch")
            End If


            If Not String.IsNullOrEmpty(Request("txtSearchTicketNo")) Then
                varLngTicketNo = Request("txtSearchTicketNo")
            End If

            varStrSDate = Request("TXTDate1")
            varStrEDate = Request("TXTDate2")

            If Not String.IsNullOrEmpty(Request("DropDownPrioritySearch")) And Not Trim(UCase(Request("DropDownPrioritySearch"))) = Trim(UCase("Any")) Then
                varStrPriority = Request("DropDownPrioritySearch")
            End If


            If Not String.IsNullOrEmpty(varStrStatus) Then
                varWhereClause = varWhereClause & " AND Status ='" & varStrStatus & "' "
            End If

            If varLngTicketNo > 0 Then
                varWhereClause = varWhereClause & " AND TicketNo =" & varLngTicketNo & " "
            End If

            If String.IsNullOrEmpty(varStrEDate) = False Then
                Dim varTDate As Date
                varTDate = DateAdd(DateInterval.Day, 1, CDate(varStrEDate))

                varStrEDate = varTDate
            End If

            If String.IsNullOrEmpty(varStrSDate) = False And String.IsNullOrEmpty(varStrEDate) = True Then
                If IsDate(varStrSDate) Then
                    varWhereClause = varWhereClause & " AND DatePosted >='" + varStrSDate + "'"
                End If
            End If

            If String.IsNullOrEmpty(varStrSDate) = True And String.IsNullOrEmpty(varStrEDate) = False Then
                If IsDate(varStrEDate) Then
                    varWhereClause = varWhereClause & " AND DatePosted <='" + varStrEDate + "'"
                End If
            End If

            If String.IsNullOrEmpty(varStrSDate) = False And String.IsNullOrEmpty(varStrEDate) = False Then
                If IsDate(varStrSDate) And IsDate(varStrEDate) Then
                    varWhereClause = varWhereClause & " AND DatePosted between '" + varStrSDate + "'and '" + varStrEDate + "'"
                End If
            End If

            If Not String.IsNullOrEmpty(varStrPriority) Then
                varWhereClause = varWhereClause & " AND Priority ='" & varStrPriority & "' "
            End If

            varStrQueryString = varStrQueryString & varWhereClause & "order by TicketNo "
            'Response.Write(varStrQueryString)

            SqlDataSource1.SelectCommand = varStrQueryString
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub
End Class
