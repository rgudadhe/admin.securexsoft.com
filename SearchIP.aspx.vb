Imports System.Data.SqlClient
Imports System.Data

Partial Class SearchIP
    Inherits BasePage
    Private Sub SQLONEBindGrid()
        Dim strConnString As String = ConfigurationManager.ConnectionStrings("ETSConnectionString").ConnectionString
        Using con As New SqlConnection(strConnString)

            Using cmd As New SqlCommand("SearchIP")
                cmd.CommandType = CommandType.StoredProcedure
                cmd.Parameters.Add("@sdate", SqlDbType.NVarChar).Value = TxtSDate.Text
                cmd.Parameters.Add("@edate", SqlDbType.NVarChar).Value = TxtEDate.Text
                cmd.Parameters.Add("@ip", SqlDbType.NVarChar).Value = txtip.Text

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

    End Sub
    Private Sub MOPSDOXBindGrid()
        Dim strConnString As String = ConfigurationManager.ConnectionStrings("SDoxConnectionString").ConnectionString
        Using con As New SqlConnection(strConnString)

            Using cmd As New SqlCommand("SearchIP")
                cmd.CommandType = CommandType.StoredProcedure
                cmd.Parameters.Add("@sdate", SqlDbType.NVarChar).Value = TxtSDate.Text
                cmd.Parameters.Add("@edate", SqlDbType.NVarChar).Value = TxtEDate.Text
                cmd.Parameters.Add("@ip", SqlDbType.NVarChar).Value = txtip.Text

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

    End Sub

    Protected Sub btnSubmit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSubmit.Click
        Try
            If DLAct.Text = "SQLONE" Then
                Me.SQLONEBindGrid()
            Else
                Me.MOPSDOXBindGrid()
            End If
            '  Response.Write(TxtSDate.Text & TxtEDate.Text & txtip.Text)

        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub
    Protected Sub OnPageIndexChanging(ByVal sender As Object, ByVal e As GridViewPageEventArgs)
        GridView1.PageIndex = e.NewPageIndex
        If DLAct.Text = "SQLONE" Then
            Me.SQLONEBindGrid()
        Else
            Me.MOPSDOXBindGrid()
        End If
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        TxtSDate.Text = Today.AddDays(-2)
        TxtEDate.Text = Today()
    End Sub
End Class
