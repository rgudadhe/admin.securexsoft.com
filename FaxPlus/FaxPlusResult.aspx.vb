Imports System
Imports System.Data
Partial Class FaxPlus_FaxPlusResult
    Inherits BasePage
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If String.IsNullOrEmpty(Request.Form("SEARCH")) = False Then
                'iMain.Visible = True
                DBind()
            ElseIf IsPostBack Then
                'iMain.Visible = True
            Else
                'iMain.Visible = False
            End If
            If dlist.Rows.Count > 0 Then
                dlist.ShowFooter = True
                dlist.UseAccessibleHeader = True
                dlist.HeaderRow.TableSection = TableRowSection.TableHeader
                dlist.FooterRow.TableSection = TableRowSection.TableFooter
            End If
        Catch ex As Exception
            Response.Write(ex.Message & "hh")
        End Try
    End Sub
    Private Sub DBind()
        If IsPostBack = False And String.IsNullOrEmpty(Request.Form("SEARCH")) = False Then
            'iMain.Visible = True
            Dim DT As New DataTable
            DT.Columns.Add("Status", GetType(System.Int32))
            DT.Columns.Add("ContractorID", GetType(System.String))
            DT.Columns.Add("JobNumber", GetType(System.Int32))
            DT.Columns.Add("CustJobID", GetType(System.String))
            DT.Columns.Add("StartDate", GetType(System.String))
            DT.Columns.Add("EndDate", GetType(System.String))
            DT.Columns.Add("AccountName", GetType(System.String))
            DT.Columns.Add("FirstName", GetType(System.String))
            DT.Columns.Add("LastName", GetType(System.String))
            DT.Columns.Add("WorkGroupID", GetType(System.String))
            Dim DR As DataRow = DT.NewRow
            DR("ContractorID") = Session("ContractorID").ToString
            'Session("WhereClause") = String.Empty
            If Not Request("FStatus").ToString = "Any" Then
                DR("Status") = IIf(IsNumeric(Request("FStatus").ToString), CInt(Request("FStatus").ToString), 2)
            Else
                DR("Status") = 2
            End If
            If Not String.IsNullOrEmpty(Request("Track").ToString) Then
                DR("JobNumber") = IIf(IsNumeric(Request("Track").ToString), CInt(Request("Track").ToString), 0)
            End If
            If Not String.IsNullOrEmpty(Request("Cust").ToString) Then
                DR("CustJobID") = Request("Cust").ToString
            End If
            If Not String.IsNullOrEmpty(Request("PFirst").ToString) Then
                DR("FirstName") = Request("PFirst").ToString
            End If
            If Not String.IsNullOrEmpty(Request("PLast").ToString) Then
                DR("LastName") = Request("PLast").ToString
            End If
            If Not String.IsNullOrEmpty(Request("AccName").ToString) Then
                DR("AccountName") = Request("AccName").ToString
            End If
            If Not String.IsNullOrEmpty(Request("sDate").ToString) Then
                DR("StartDate") = Request("sDate").ToString
            End If
            If Not String.IsNullOrEmpty(Request("eDate").ToString) Then
                DR("EndDate") = Request("eDate").ToString
            End If
            DR("WorkGroupID") = Session("WorkGroupID").ToString
            DT.Rows.Add(DR)
            Dim CLSFP As New ETS.BL.FaxPlus()

            Dim DS As DataSet = CLSFP.getFaxPlusReport(DT)
            If DS.Tables.Count > 0 Then
                If DS.Tables(0).Rows.Count > 0 Then
                    dlist.DataSource = DS
                    dlist.DataBind()
                End If
            End If
            CLSFP = Nothing
            DS.Dispose()
        End If
    End Sub
    Public Overrides Sub VerifyRenderingInServerForm(ByVal control As Control)
    End Sub
    Protected Sub LnlExport_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles LnlExport.Click
        Response.Clear()
        Dim filename = "FaxPlus Report " & Now & " .xls"
        Response.AddHeader("content-disposition", "attachment;filename=" & filename)
        Response.ContentType = "application/vnd.ms-excel"
        Response.Charset = ""
        Me.EnableViewState = False
        Dim tw As New System.IO.StringWriter()
        Dim hw As New System.Web.UI.HtmlTextWriter(tw)
        dlist.RenderControl(hw)
        Response.Write(tw.ToString())
        Response.End()
    End Sub
    Protected Sub dlist_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles dlist.RowCommand
        If Trim(UCase(e.CommandName)) = Trim(UCase("Delete")) And Not String.IsNullOrEmpty(e.CommandArgument) Then
            Dim clsFP As ETS.BL.FaxPlus
            Try
                clsFP = New ETS.BL.FaxPlus
                clsFP.TranscriptionID = Trim(e.CommandArgument)

                If clsFP.DeleteFaxPlusDetails() > 0 Then
                    ScriptManager.RegisterClientScriptBlock(Me, Me.GetType(), "msg", "<script language='javascript'>alert('Record deleted successfully');window.location.reload();</script>", False)
                End If
            Catch ex As Exception
                clsFP = Nothing
            End Try
        End If
    End Sub
    Protected Sub dlist_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles dlist.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim lnkHis As ImageButton
            lnkHis = DirectCast(e.Row.FindControl("btnHistory"), ImageButton)
            Dim hdntID As HiddenField
            hdntID = DirectCast(e.Row.FindControl("hdnTransID"), HiddenField)
            Dim hdnRID As HiddenField
            hdnRID = DirectCast(e.Row.FindControl("hdnRPID"), HiddenField)
            If Not lnkHis Is Nothing And Not hdntID Is Nothing And Not hdnRID Is Nothing Then
                lnkHis.Attributes.Add("onclick", "javascript:return openPopup('" & hdntID.Value.ToString & "','" & hdnRID.Value.ToString & "')")
            End If
        End If
    End Sub
    Protected Sub dlist_RowDeleting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewDeleteEventArgs) Handles dlist.RowDeleting

    End Sub
End Class
