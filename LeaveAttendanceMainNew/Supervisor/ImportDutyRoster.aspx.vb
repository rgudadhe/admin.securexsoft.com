Imports MainModule

Partial Class LeaveAttendanceMainNew_Supervisor_ImportDutyRoster
    Inherits BasePage
    Dim varObjMainModule As New MainModule
    Dim varStrEmp As String = String.Empty
    Dim varStrTempEmp As String = String.Empty
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Not Page.IsPostBack Then
            tblDataImported.Visible = False
        End If
    End Sub
    Protected Sub btnUpload_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnUpload.Click
        Dim varFileUploadPath As String
        Dim varupdateCounter As Long = 0
        Dim objDS As New Data.DataSet
        Dim objConn As New Data.OleDb.OleDbConnection
        Dim sConnection As String = String.Empty
        Dim dtSheetName As New Data.DataTable
        Dim clsDS As ETS.BL.DutyRoster
        'Try
        If Trim(UCase(System.IO.Path.GetExtension(Server.HtmlEncode(FileUpload.FileName)))) = Trim(UCase((".XLS"))) Then
            Dim dtFormat = Month(Now) & "_" & Day(Now) & "_" & Year(Now) & "_" & Hour(Now) & "_" & Minute(Now)
            varFileUploadPath = Server.MapPath("/ets_Files/") & "\UploadDutyRosterFiles\" & Session("UserName") & "\" & dtFormat & "\"
            Dim varTempDir As New IO.DirectoryInfo(varFileUploadPath)
            If varTempDir.Exists = False Then
                varTempDir.Create()

            End If

            FileUpload.PostedFile.SaveAs(varFileUploadPath & "\" & FileUpload.FileName)
            sConnection = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" & varFileUploadPath & "\" & FileUpload.FileName & ";Extended Properties=Excel 8.0;"

            objConn.ConnectionString = sConnection
            objConn.Open()

            'dtSheetName = objConn.GetOleDbSchemaTable(Data.OleDb.OleDbSchemaGuid.Tables, Nothing)
            Dim myDataAdapter As New System.Data.OleDb.OleDbDataAdapter("SELECT * FROM [Sheet1$]", objConn)
            myDataAdapter.Fill(objDS, "ExcelInfo")
            myDataAdapter.Dispose()
            'Response.Write(objDS.Tables(0).Rows(0).Item(0).ToString)
            'Response.End()
            clsDS = New ETS.BL.DutyRoster
            'Response.Write(clsDS.btn_UploadDutyRoster(objDS, Session("UserID").ToString, Session("ContractorID").ToString))
            If clsDS.btn_UploadDutyRoster(objDS, Session("UserID").ToString, Session("ContractorID").ToString) = True Then
                Response.Write("<script type=""text/javascript"" language=javascript> alert(""DutyRoster Imported sucessfully!!!"");window.location.href='ImportDutyRoster.aspx';</script>")
            Else
                Response.Write("<script type=""text/javascript"" language=javascript> alert(""Failed importing DutyRoster"");window.location.href='ImportDutyRoster.aspx';</script>")
            End If

        End If
        'Catch ex As Exception
        '    Response.Write(ex.Message)
        'Finally
        '    clsDS = Nothing
        '    objDS.Dispose()
        '    dtSheetName.Dispose()
        If objConn.State <> Data.ConnectionState.Closed Then
            objConn.Close()
            objConn = Nothing
        End If
        'End Try
    End Sub
End Class
