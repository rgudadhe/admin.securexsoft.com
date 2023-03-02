Imports System.ServiceProcess
Imports System
Imports System.Data
Imports Edanmo.TaskScheduler
Partial Class Schedulars
    Inherits System.Web.UI.Page
    Private TaskMon As Edanmo.TaskScheduler.TaskCollection
    Private varServerName As String = String.Empty
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'If Not Page.IsPostBack Then
        varServerName = Trim(Request.QueryString("Server"))
        lblServerName.Text = varServerName
        'BindData(varServerName)
        'End If
    End Sub
    Protected Sub BindData(ByVal ServerName As String)
        TaskMon = New Edanmo.TaskScheduler.TaskCollection()
        TaskMon.TargetComputer = "\\" & ServerName
        'Response.Write(TaskMon.TargetComputer.ToString)
        Dim dtSchdName As DataTable = New DataTable()

        dtSchdName.Columns.Add(New DataColumn("SchedularName", Type.GetType("System.String")))
        dtSchdName.Columns.Add(New DataColumn("Schedule", Type.GetType("System.String")))
        dtSchdName.Columns.Add(New DataColumn("LastRunTime", Type.GetType("System.String")))
        dtSchdName.Columns.Add(New DataColumn("NextRunTime", Type.GetType("System.String")))
        dtSchdName.Columns.Add(New DataColumn("Difference", Type.GetType("System.String")))
        dtSchdName.Columns.Add(New DataColumn("State", Type.GetType("System.String")))



        For i As Integer = 0 To TaskMon.Count - 1
            Dim varStrState As String = String.Empty
            Dim drSchdName As DataRow = dtSchdName.NewRow()
            drSchdName(0) = Mid(TaskMon.Item(i).ApplicationName, InStrRev(TaskMon.Item(i).ApplicationName, "\") + 1)
            drSchdName(1) = TaskMon.Item(i).Triggers(0).Text
            drSchdName(2) = TaskMon.Item(i).LastRunTime.ToString("F")
            drSchdName(3) = TaskMon.Item(i).NextRunTime.ToString("F")
            drSchdName(4) = ""
            drSchdName(5) = TaskMon.Item(i).State

            dtSchdName.Rows.Add(drSchdName)

        Next

        'DataGrid2.Visible = True

        DataGrid2.DataSource = dtSchdName

        DataGrid2.DataBind()
    End Sub
    Protected Sub DataGrid2_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles DataGrid2.RowDataBound
        If Trim(UCase(e.Row.RowType.ToString)) = Trim(UCase("DataRow")) Then
            Dim varDt As Date
            varDt = e.Row.Cells(2).Text
            e.Row.Cells(2).Text = varDt & "&nbsp"

            Dim varDt1 As Date
            varDt1 = e.Row.Cells(3).Text
            e.Row.Cells(3).Text = varDt1

            If Trim(UCase(e.Row.Cells(5).Text.ToString)) <> Trim(UCase("Disabled")) Then
                e.Row.Cells(4).Text = DateDiff(DateInterval.Minute, varDt, varDt1)
            End If

            If Trim(UCase(e.Row.Cells(5).Text.ToString)) = Trim(UCase("Disabled")) Then
                e.Row.ForeColor = Drawing.Color.Chocolate
            ElseIf Trim(UCase(e.Row.Cells(5).Text.ToString)) = Trim(UCase("Running")) Then
                e.Row.ForeColor = Drawing.Color.Green
                e.Row.Font.Bold = True
            End If

        End If
    End Sub
    Protected Sub DataGrid2_Sorting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewSortEventArgs) Handles DataGrid2.Sorting
        ' BindData()
    End Sub
    'Protected Sub ddlServerName_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlServerName.SelectedIndexChanged
    '    Try
    '        Dim varStr As String = String.Empty
    '        varStr = varServerName
    '        If Not String.IsNullOrEmpty(varStr) Then
    '            BindData(varStr)
    '        End If
    '    Catch ex As Exception
    '        Response.Write(ex.Message)
    '    End Try
    'End Sub
    'Protected Function GetServerName() As String
    '    Dim varStrServerName As String = String.Empty
    '    varStrServerName = ddlServerName.Items(ddlServerName.SelectedIndex).Value.ToString
    '    Return varStrServerName
    'End Function
    Protected Sub UpdateTimer_Tick(ByVal sender As Object, ByVal e As EventArgs)
        'DateStampLabel.Text = DateTime.Now.ToString()
        Try
            Dim varStr As String = String.Empty
            varStr = varServerName
            If Not String.IsNullOrEmpty(varStr) Then
                'BindData(varStr)
            End If
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub
End Class
