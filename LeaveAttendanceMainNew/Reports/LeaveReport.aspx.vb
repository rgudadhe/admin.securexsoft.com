
Partial Class LeaveAttendanceMainNew_Reports_LeaveReport
    Inherits System.Web.UI.Page
    Protected Sub Submit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Submit.Click
        BindData()
    End Sub
    Protected Sub BindData()
        Dim clsLeave As ETS.BL.Leave
        Dim DS As New Data.DataSet
        Try
            clsLeave = New ETS.BL.Leave
            DS = clsLeave.GetLeaveReport(Session("ContractorID").ToString, Trim(txtStartDate.Text.ToString), Trim(txtEndDate.Text.ToString), Session("WorkGroupID"))
            If DS.Tables.Count > 0 Then
                If DS.Tables(0).Rows.Count > 0 Then
                    GridView1.DataSource = DS.Tables(0)
                    GridView1.DataBind()
                End If
            End If
            If GridView1.Rows.Count > 0 Then
                GridView1.ShowFooter = True
                GridView1.UseAccessibleHeader = True
                GridView1.HeaderRow.TableSection = TableRowSection.TableHeader
                GridView1.FooterRow.TableSection = TableRowSection.TableFooter
            End If
            Table3.Visible = True

        Catch ex As Exception
            Response.Write("Err:" & ex.Message)
        Finally
            clsLeave = Nothing
            DS.Dispose()
        End Try
    End Sub
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            Table3.Visible = False
        End If
    End Sub
    Public Overrides Sub VerifyRenderingInServerForm(ByVal control As Control)
    End Sub
    Protected Sub LinkButton1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles LinkButton1.Click
        Try
            Response.Clear()
            Dim filename = "Leave Report " & Now & " .xls"
            Response.AddHeader("content-disposition", "attachment;filename=" & filename)
            Response.ContentType = "application/vnd.ms-excel"
            Response.Charset = ""
            Me.EnableViewState = False
            Dim tw As New System.IO.StringWriter()
            Dim hw As New System.Web.UI.HtmlTextWriter(tw)
            GridView1.RenderControl(hw)
            Response.Write(tw.ToString())
            Response.End()
        Catch ex As Exception
        End Try
    End Sub
End Class
