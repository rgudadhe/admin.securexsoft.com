Imports System.ServiceProcess
Imports System
Imports System.Data
Imports Edanmo.TaskScheduler
Imports System.Management
Partial Class Services
    Inherits System.Web.UI.Page
    Private TaskMon As Edanmo.TaskScheduler.TaskCollection
    Private varServerName As String = String.Empty
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'If Not Page.IsPostBack Then
        varServerName = Trim(Request.QueryString("Server"))
        lblServerName.Text = varServerName
        BindData(varServerName)
        'End If
    End Sub
    Protected Sub BindData(ByVal ServerName As String)
        Dim dtSrvName As DataTable = New DataTable()

        dtSrvName.Columns.Add(New DataColumn("ServiceName", Type.GetType("System.String")))

        'dtSrvName.Columns.Add(New DataColumn("ServiceType", Type.GetType("System.String")))
        dtSrvName.Columns.Add(New DataColumn("Status", Type.GetType("System.String")))

        'Code to check log on type of the services
        'Dim query As New System.Management.SelectQuery("select name, startname from Win32_Service")
        'Dim i As Integer = 0
        'Using searcher As New System.Management.ManagementObjectSearcher(query)
        '    For Each service As ManagementObject In searcher.[Get]()
        '        i = i + 1
        '        Response.Write(i & " : Name: {0} - Logon : {1} " & "," & service("Name") & "," & service("startname") & "<BR>")
        '    Next
        'End Using
        'End check log on type

        Dim arrSrvCtrl() As System.ServiceProcess.ServiceController
        arrSrvCtrl = System.ServiceProcess.ServiceController.GetServices(ServerName)

        Dim tmpSC As System.ServiceProcess.ServiceController
        For Each tmpSC In arrSrvCtrl
            'Response.Write(tmpSC.ServiceProcess.ServiceAccount.User & "<BR>")

            'If Trim(UCase(tmpSC.ServiceType.ToString())) = Trim(UCase("Win32OwnProcess")) Then
            '    Dim drSrvName As DataRow = dtSrvName.NewRow()
            '    drSrvName(0) = tmpSC.DisplayName.ToString()
            '    'drSrvName(1) = tmpSC.ServiceType.ToString()
            '    drSrvName(1) = tmpSC.Status.ToString() 'IIf(LCase(tmpSC.Status.ToString()) = "stopped", "Link", tmpSC.Status.ToString())
            '    dtSrvName.Rows.Add(drSrvName)
            'End If

            If (tmpSC.DisplayName = "FaxPlusService" Or tmpSC.DisplayName = "FileImport" Or tmpSC.DisplayName = "DemoService" Or tmpSC.DisplayName = "LCEngine Service") Then
                Dim drSrvName As DataRow = dtSrvName.NewRow()
                drSrvName(0) = tmpSC.DisplayName.ToString()
                drSrvName(1) = tmpSC.Status.ToString() 'IIf(LCase(tmpSC.Status.ToString()) = "stopped", "Link", tmpSC.Status.ToString())
                dtSrvName.Rows.Add(drSrvName)
            End If
        Next
        DataGrid2.DataSource = dtSrvName
        DataGrid2.DataBind()
    End Sub
    'Protected Sub ddlServerName_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlServerName.SelectedIndexChanged
    '    Try
    '        Dim varStr As String = String.Empty
    '        varStr = GetServerName()
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
                BindData(varStr)
            End If
        Catch ex As Exception
            'Response.Write(ex.Message)
        End Try
    End Sub
    Protected Sub DataGrid2_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles DataGrid2.RowDataBound
        If Trim(UCase(e.Row.RowType.ToString)) = Trim(UCase("DataRow")) Then
            If Trim(UCase(e.Row.Cells(1).Text.ToString)) = Trim(UCase("Stopped")) Then
                e.Row.ForeColor = Drawing.Color.Chocolate
            End If
        End If
    End Sub
End Class
