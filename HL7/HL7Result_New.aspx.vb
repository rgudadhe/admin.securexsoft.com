
Partial Class FaxPlus_FaxPlusResult
    Inherits BasePage
    Dim objDS As New System.Data.DataSet()
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If String.IsNullOrEmpty(Request.QueryString("PIndex")) = False Or String.IsNullOrEmpty(Request.Form("SEARCH")) = False Then
                iMain.Visible = True
                DBind()
                BtnSetStatus.Visible = True
            ElseIf IsPostBack Then
                iMain.Visible = True
                BtnSetStatus.Visible = True
            Else
                iMain.Visible = False
                BtnSetStatus.Visible = False
            End If

        Catch ex As Exception
            Response.Write(ex.Message & "hh")
        End Try
    End Sub
    Private Sub DBind()
        Try
            If String.IsNullOrEmpty(Request.QueryString("SortBy")) Then
                ViewState("SortBy") = "JobNumber"
            Else
                ViewState("SortBy") = Request.QueryString("SortBy").ToString
            End If
            If String.IsNullOrEmpty(Request.QueryString("SortOrder")) Then
                ViewState("SortOrder") = " ASC"
            Else
                ViewState("SortOrder") = Request.QueryString("SortOrder").ToString
            End If
            If String.IsNullOrEmpty(Request.QueryString("PIndex")) Then
                ViewState("PIndex") = 0
            Else
                ViewState("PIndex") = Request.QueryString("PIndex").ToString
            End If


            If IsPostBack = False And String.IsNullOrEmpty(Request.Form("SEARCH")) = False Then
                iMain.Visible = True
            End If
            Try

                Dim clsHL7 As ETS.BL.HL7Reports
                Dim Ds As New Data.DataSet
                Dim Ds1 As New Data.DataSet
                Try
                    clsHL7 = New ETS.BL.HL7Reports
                    Ds = clsHL7.SearchHL7Reports(Request("FStatus").ToString, Request("Track").ToString, Request("Cust").ToString, Request("PFirst").ToString, Request("PLast").ToString, Request("PtName").ToString, Request("sDate").ToString, Request("eDate").ToString, Session("ContractorID"), Session("WorkGroupID"))
                    If Ds.Tables.Count > 0 Then
                        If Ds.Tables(0).Rows.Count > 0 Then
                            dlist.DataSource = Ds
                            dlist.DataBind()
                        Else
                            iMain.Visible = False
                        End If
                    Else
                        iMain.Visible = False
                    End If

                Catch ex As Exception
                    Response.Write(ex.Message)
                Finally
                    Ds = Nothing
                    Ds1 = Nothing
                    clsHL7 = Nothing
                End Try


            Catch ex As Exception
                Response.Write(ex.Message)


            End Try
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
        If dlist.Rows.Count > 0 Then
            dlist.ShowFooter = True
            dlist.UseAccessibleHeader = True
            dlist.HeaderRow.TableSection = TableRowSection.TableHeader
            dlist.FooterRow.TableSection = TableRowSection.TableFooter
        End If
    End Sub
    Protected Sub BtnSetStatus_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtnSetStatus.Click
        Dim DT As New Data.DataTable
        DT.Columns.Add("TransID", GetType(System.String))
        Dim i = 0
        For Each DR As GridViewRow In dlist.Rows
            Dim chk As CheckBox = DR.FindControl("ChkStatus")
            i = i + 1
            If Not chk Is Nothing Then
                If chk.Checked Then
                    Dim DRow As Data.DataRow = DT.NewRow
                    Dim hdn As HiddenField = chk.Parent.FindControl("hdnID")
                    DRow("TransID") = hdn.Value.ToString
                    DT.Rows.Add(DRow)
                    'Response.Write(i & " " & hdn.Value.ToString)
                End If
            End If
        Next
        If DT.Rows.Count > 0 Then
            Dim clsHL7 As ETS.BL.HL7Reports
            Try
                clsHL7 = New ETS.BL.HL7Reports
                If clsHL7.UpdateStatusFromHLTrackReports(DT) = True Then
                    Response.Write("<font face=Trebuchet MS size=4>Status updated sucessfully</font>")
                Else
                    Response.Write("<font face=Trebuchet MS size=4>Status updation failed</font>")
                End If
            Catch ex As Exception
                Response.Write(ex.Message)
            Finally
                clsHL7 = Nothing
            End Try
        End If
    End Sub
    Public Overrides Sub VerifyRenderingInServerForm(ByVal control As Control)
    End Sub
    Protected Sub LinkButton1_Click1(ByVal sender As Object, ByVal e As System.EventArgs) Handles LinkButton1.Click
        Response.Clear()
        Dim filename = "Dictation Status Report " & Now & " .xls"
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

    Protected Sub dlist_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles dlist.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim lnkHis As ImageButton
            lnkHis = DirectCast(e.Row.FindControl("btnHistory"), ImageButton)
            Dim hdntID As HiddenField
            hdntID = DirectCast(e.Row.FindControl("hdnTransID"), HiddenField)

            If Not lnkHis Is Nothing And Not hdntID Is Nothing Then
                lnkHis.Attributes.Add("onclick", "javascript:return openPopup('" & hdntID.Value.ToString & "')")
            End If
        End If
    End Sub
End Class
