Imports System.Data.SqlClient
Imports System.Data
Imports System.IO
Imports System.Drawing

Partial Class SecureunoLog
    Inherits System.Web.UI.Page
    Private Sub BindGrid()
        Try


            ' Response.Write(sDate.Text & eDate.Text)
            Dim strConnString As String = ConfigurationManager.ConnectionStrings("ETSConnectionString").ConnectionString
            Using con As New SqlConnection(strConnString)

                Using cmd As New SqlCommand("SF_getSecureUNOLog")
                    cmd.CommandType = CommandType.StoredProcedure
                    cmd.Parameters.Add("@sdate", SqlDbType.Date).Value = sDate.Text
                    cmd.Parameters.Add("@edate", SqlDbType.Date).Value = eDate.Text
                    Using sda As New SqlDataAdapter()
                        cmd.Connection = con
                        sda.SelectCommand = cmd
                        Using dt As New DataSet()
                            sda.Fill(dt)
                            GridView1.DataSource = dt
                            GridView1.DataBind()
                        End Using
                    End Using
                End Using
            End Using
            '  Response.Write(GridView1.Rows.Count)
        Catch ex As Exception
            Response.Write(ex.StackTrace)
        End Try
    End Sub


    Protected Sub btnSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        If sDate.Text <> "" Or eDate.Text <> "" Then
            lblMsg.Text = ""
            Me.BindGrid()

        Else
            lblMsg.Text = "Please select start date and end date."

            Exit Sub
        End If
        ' Response.Write(sDate.Text & eDate.Text)
    End Sub
    

    Protected Sub btnExport_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnExport.Click
        Response.Clear()
        Response.Buffer = True
        Response.AddHeader("content-disposition", "attachment;filename=GridViewExport.xls")
        Response.Charset = ""
        Response.ContentType = "application/vnd.ms-excel"
        Using sw As New StringWriter()


            Dim hw As New HtmlTextWriter(sw)

            'To Export all pages  
            GridView1.AllowPaging = False
            Me.BindGrid()

            GridView1.HeaderRow.BackColor = Color.White
            For Each cell As TableCell In GridView1.HeaderRow.Cells
                cell.BackColor = GridView1.HeaderStyle.BackColor
            Next
            For Each row As GridViewRow In GridView1.Rows
                row.BackColor = Color.White
                For Each cell As TableCell In row.Cells
                    If row.RowIndex Mod 2 = 0 Then
                        cell.BackColor = GridView1.AlternatingRowStyle.BackColor
                    Else
                        cell.BackColor = GridView1.RowStyle.BackColor
                    End If
                    cell.CssClass = "textmode"
                Next
            Next

            GridView1.RenderControl(hw)
            'style to format numbers to string  
            Dim style As String = "<style> .textmode { } </style>"
            Response.Write(style)
            Response.Output.Write(sw.ToString())
            Response.Flush()
            Response.[End]()
        End Using
    End Sub
    Public Overrides Sub VerifyRenderingInServerForm(ByVal control As Control)
        ' Verifies that the control is rendered
    End Sub
    Protected Sub OnPageIndexChanging(ByVal sender As Object, ByVal e As GridViewPageEventArgs)
        GridView1.PageIndex = e.NewPageIndex
        Me.BindGrid()
    End Sub
End Class
