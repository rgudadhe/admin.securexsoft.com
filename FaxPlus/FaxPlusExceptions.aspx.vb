Imports System
Imports System.Data
Partial Class FaxPlus_FaxPlusExceptions
    Inherits BasePage
    Protected Sub btnGO_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnGO.Click
        DoBind()
    End Sub
    Public Function DoBind() As Boolean
        Dim CLSFP As ETS.BL.FaxPlusExceptions
        Dim DS As New DataSet
        Try
            Dim DT As New DataTable
            DT.Columns.Add("Comments", GetType(System.String))
            DT.Columns.Add("ContractorID", GetType(System.String))
            DT.Columns.Add("JobNumber", GetType(System.Int32))
            DT.Columns.Add("CustJobID", GetType(System.String))
            DT.Columns.Add("StartDate", GetType(System.String), "")
            DT.Columns.Add("EndDate", GetType(System.String), "")
            DT.Columns.Add("AccountName", GetType(System.String))
            DT.Columns.Add("FirstName", GetType(System.String))
            DT.Columns.Add("LastName", GetType(System.String))
            Dim DR As DataRow = DT.NewRow
            DR("ContractorID") = Session("ContractorID").ToString
            'Session("WhereClause") = String.Empty
            If DDLEx.SelectedValue.ToString <> "All" Then
                DR("Comments") = DDLEx.SelectedValue.ToString
            End If
            If Not String.IsNullOrEmpty(Track.Text.ToString) Then
                DR("JobNumber") = IIf(IsNumeric(Track.Text.ToString), CInt(Track.Text.ToString), 0)
            End If
            If Not String.IsNullOrEmpty(Cust.Text.ToString) Then
                DR("CustJobID") = Cust.Text.ToString
            End If
            If Not String.IsNullOrEmpty(AName.Text.ToString) Then
                DR("AccountName") = AName.Text.ToString
            End If
            If Not String.IsNullOrEmpty(sDate.Text.ToString) Then
                DR("StartDate") = sDate.Text.ToString
            End If
            If Not String.IsNullOrEmpty(eDate.Text.ToString) Then
                DR("EndDate") = eDate.Text.ToString
            End If
            DT.Rows.Add(DR)
            CLSFP = New ETS.BL.FaxPlusExceptions

            DS = CLSFP.getFaxPlusExeptions(DT)
            If DS.Tables(0).Rows.Count > 0 Then
                dlist.DataSource = DS
                dlist.DataBind()
                dlist.ShowFooter = True
                dlist.UseAccessibleHeader = True
                dlist.HeaderRow.TableSection = TableRowSection.TableHeader
                dlist.FooterRow.TableSection = TableRowSection.TableFooter
            End If
            'End If
            
            LnlExport.Visible = True
        Catch ex As Exception
            Response.Write(ex.Message)
        Finally
            CLSFP = Nothing
            DS.Dispose()
        End Try
    End Function
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            LnlExport.Visible = False
        End If
        If Request.QueryString("Result") = "1" Then
            DoBind()
        End If
    End Sub
    Public Overrides Sub VerifyRenderingInServerForm(ByVal control As Control)
    End Sub
    Protected Sub LnlExport_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles LnlExport.Click
        Response.Clear()
        Dim filename = "Exception Report " & Now & " .xls"
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
        If Trim(UCase(e.CommandName)) = Trim(UCase("Delete")) Then
            If Not String.IsNullOrEmpty(e.CommandArgument) Then
                Dim varStrTransID As String = String.Empty
                Dim varStrRPID As String = String.Empty
                Dim varStrArg() As String = Split(e.CommandArgument, "|")
                varStrTransID = varStrArg(0)
                varStrRPID = varStrArg(1)
                If Not String.IsNullOrEmpty(varStrTransID) Then
                    Dim CLSFP As New ETS.BL.FaxPlusExceptions()
                    With CLSFP
                        .TranscriptionID = varStrTransID
                        .RPID = varStrRPID
                        If .DeleteFPEx > 0 Then
                            Response.Write("<script language='javascript'>alert('Record deleted successfully');</script>")
                        Else
                            Response.Write("<script language='javascript'>alert('Deleting record failed');</script>")
                        End If
                    End With
                    DoBind()
                End If
            End If
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
