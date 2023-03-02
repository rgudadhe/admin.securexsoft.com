Imports System.Data
Partial Class ERSSMainNew_Admin_SearchTicketsNew
    Inherits BasePage
    Dim objMainModule As New MainModule
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            Dim varListItem As New ListItem
            varListItem.Text = "Please select"
            varListItem.Value = ""
            Dim clsERSSIC As ETS.BL.ERSSIssueCategory
            Dim DS As New Data.DataSet
            Dim DV As Data.DataView
            Try
                clsERSSIC = New ETS.BL.ERSSIssueCategory
                clsERSSIC.ContractorID = Session("ContractorID").ToString
                DS = clsERSSIC.getIssueCategoryList()
                DV = New Data.DataView(DS.Tables(0))
                DV.RowFilter = "(IsDeleted IS NULL OR IsDeleted=0)"

                ddlICate.DataSource = DV
                ddlICate.DataTextField = "CateName"
                ddlICate.DataValueField = "CategoryID"
                ddlICate.DataBind()
            Catch ex As Exception
            Finally
                clsERSSIC = Nothing
                DS.Dispose()
                DV.Dispose()
            End Try
            ddlICate.Items.Insert(0, varListItem)
            ddlIType.Items.Insert(0, varListItem)
            ViewState("Sort") = " ORDER BY DatePosted DESC "
        End If
    End Sub
    Protected Sub ddlICate_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlICate.SelectedIndexChanged
        Dim varCateID As String = String.Empty
        varCateID = ddlICate.Items(ddlICate.SelectedIndex).Value.ToString
        ddlIType.Items.Clear()
        Dim varListItem As New ListItem
        varListItem.Text = "Please select"
        varListItem.Value = ""
        If Not String.IsNullOrEmpty(varCateID) Then
            Dim clsERSSIT As ETS.BL.ERSSIssueType
            Dim DS As New Data.DataSet
            Dim DV As Data.DataView
            Try
                clsERSSIT = New ETS.BL.ERSSIssueType
                clsERSSIT.CategoryID = varCateID.ToString
                clsERSSIT.ContractorID = Session("ContractorID").ToString

                DS = clsERSSIT.getIssueTypeList
                DV = New Data.DataView(DS.Tables(0))
                DV.RowFilter = "(IsDeleted IS NULL OR IsDeleted=0)"

                ddlIType.DataSource = DV
                ddlIType.DataTextField = "IssueName"
                ddlIType.DataValueField = "IssueID"
                ddlIType.DataBind()

            Catch ex As Exception
            Finally
                clsERSSIT = Nothing
                DS.Dispose()
                DV.Dispose()
            End Try
        End If
        ddlIType.Items.Insert(0, varListItem)
    End Sub
    Protected Sub btnSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        FillGrid(String.Empty)
    End Sub
    Protected Sub FillGrid(ByVal Sorting As String)
        Dim varStartDate As String = String.Empty
        Dim varEndDate As String = String.Empty
        Dim varState As String
        Dim varIssueID As String
        Dim varCateID As String
        Dim varTicketNo As String
        Dim varFName As String
        Dim varLName As String
        Dim clsERSS As ETS.BL.ERSS
        Dim DS As New Data.DataSet
        Dim DTSearchParam As New DataTable
        DTSearchParam = New DataTable
        Try
            GridViewSearchResults.DataSource = Nothing
            DTSearchParam.Columns.Add(New DataColumn("sDate"))
            DTSearchParam.Columns.Add(New DataColumn("eDate"))
            DTSearchParam.Columns.Add(New DataColumn("Status"))
            DTSearchParam.Columns.Add(New DataColumn("IssueID"))
            DTSearchParam.Columns.Add(New DataColumn("CategoryID"))
            DTSearchParam.Columns.Add(New DataColumn("TicketNo"))
            DTSearchParam.Columns.Add(New DataColumn("FName"))
            DTSearchParam.Columns.Add(New DataColumn("LName"))
            DTSearchParam.Columns.Add(New DataColumn("UserId"))
            DTSearchParam.Columns.Add(New DataColumn("ContractorID"))
            DTSearchParam.Columns.Add(New DataColumn("WorkGroupID"))
            Dim DR As Data.DataRow = DTSearchParam.NewRow

            If Not String.IsNullOrEmpty(txtStartDate.Text.ToString) Then
                varStartDate = txtStartDate.Text.ToString
                DR("sDate") = CDate(varStartDate)
            End If

            If Not String.IsNullOrEmpty(txtEndDate.Text.ToString) Then
                varEndDate = txtEndDate.Text.ToString
                DR("eDate") = CDate(varEndDate)
            End If

            If Not String.IsNullOrEmpty(ddlStatus.Items(ddlStatus.SelectedIndex).Value.ToString) Then
                varState = ddlStatus.Items(ddlStatus.SelectedIndex).Value.ToString
                DR("Status") = varState
            End If

            If Not String.IsNullOrEmpty(ddlIType.Items(ddlIType.SelectedIndex).Value.ToString) Then
                varIssueID = ddlIType.Items(ddlIType.SelectedIndex).Value.ToString
                DR("IssueID") = varIssueID
            End If

            If Not String.IsNullOrEmpty(ddlICate.Items(ddlICate.SelectedIndex).Value.ToString) Then
                varCateID = ddlICate.Items(ddlICate.SelectedIndex).Value.ToString
                DR("CategoryID") = varCateID
            End If

            If Not String.IsNullOrEmpty(txtTicketNo.Text.ToString) Then
                varTicketNo = txtTicketNo.Text.ToString
                DR("TicketNo") = varTicketNo
            End If

            If Not String.IsNullOrEmpty(txtFName.Text.ToString) Then
                varFName = txtFName.Text.ToString
                DR("FName") = varFName
            End If

            If Not String.IsNullOrEmpty(txtLName.Text.ToString) Then
                varLName = txtLName.Text.ToString
                DR("LName") = varLName
            End If

            Session("UserID") = "A1ABBF5E-4869-4600-907F-01B6FAEEF377"
            DR("UserId") = Session("UserID").ToString
            DR("ContractorID") = Session("ContractorID").ToString
            DR("WorkGroupID") = Session("WorkGroupID").ToString

            DTSearchParam.Rows.Add(DR)
            clsERSS = New ETS.BL.ERSS()


            DS = clsERSS.GetERSSTicketsBySearch(DTSearchParam)

            If DS.Tables.Count > 0 Then
                If DS.Tables(0).Rows.Count > 0 Then
                    GridViewSearchResults.DataSource = DS
                    GridViewSearchResults.DataBind()
                    tblResult.Visible = True
                End If
            End If


        Catch ex As Exception
            Response.Write(ex.Message)
        Finally
            clsERSS = Nothing
            DTSearchParam.Dispose()
            DS.Dispose()
        End Try
    End Sub
    Protected Sub GridViewSearchResults_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles GridViewSearchResults.PageIndexChanging
        GridViewSearchResults.PageIndex = e.NewPageIndex
        FillGrid(ViewState("Sort").ToString)
        GridViewSearchResults.DataBind()
    End Sub
    Protected Sub GridViewSearchResults_Sorting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewSortEventArgs) Handles GridViewSearchResults.Sorting
        Dim varStrSort As String = String.Empty
        Dim varStrDir As String = String.Empty

        varStrSort = e.SortExpression.ToString

        If String.IsNullOrEmpty(ViewState("SortDirection")) Then
            varStrDir = "ASC"
        Else
            If Trim(UCase(ViewState("SortDirection"))) = Trim(UCase("ASC")) Then
                varStrDir = "DESC"
            ElseIf Trim(UCase(ViewState("SortDirection"))) = Trim(UCase("DESC")) Then
                varStrDir = "ASC"
            End If
        End If

        ViewState("Sort") = " ORDER BY " & varStrSort & " " & varStrDir
        'Response.Write(ViewState("Sort"))
        FillGrid(ViewState("Sort").ToString)
        ViewState("SortDirection") = varStrDir
    End Sub
    Protected Function ValidateString(ByVal [String] As Object) As String
        If Not String.IsNullOrEmpty([String].ToString) Then
            Dim varStr As String = String.Empty
            If ([String].ToString().Length > 100) Then
                varStr = CStr([String].ToString())
                varStr = varStr.Replace(Convert.ToChar(34), " ")
                Return [String].ToString().Substring(0, 100) + " <label style=""font-family:Trebuchet MS; color:Blue; cursor:hand ""  onmouseover=""tip_it('ToolTip','Description','" & varStr.ToString & "'); "" onmouseout=""hideIt('ToolTip');""> more >></label>"
            Else
                varStr = CStr([String].ToString())
                varStr = varStr.Replace(Convert.ToChar(34), " ")
                Return [String].ToString()
            End If
        Else
            Return String.Empty
        End If
    End Function
End Class
