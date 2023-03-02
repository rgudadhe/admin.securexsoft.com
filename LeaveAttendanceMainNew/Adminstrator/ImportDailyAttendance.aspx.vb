Imports MainModule
Partial Class ImportDailyAttendance
    Inherits BasePage
    Dim varObjMainModule As New MainModule
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
        Try
            If Trim(UCase(System.IO.Path.GetExtension(Server.HtmlEncode(FileUpload.FileName)))) = Trim(UCase((".XLS"))) Then
                Dim dtFormat = Month(Now) & "_" & Day(Now) & "_" & Year(Now) & "_" & Hour(Now) & "_" & Minute(Now)
                varFileUploadPath = Server.MapPath("/ets_Files/") & "\UploadAttendance\" & Session("UserName") & "\" & dtFormat & "\"
                Dim varTempDir As New IO.DirectoryInfo(varFileUploadPath)
                If varTempDir.Exists = False Then
                    varTempDir.Create()
                End If

                FileUpload.PostedFile.SaveAs(varFileUploadPath & "\" & FileUpload.FileName)
                sConnection = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" & varFileUploadPath & "\" & FileUpload.FileName & ";Extended Properties=Excel 8.0;"

                objConn.ConnectionString = sConnection
                objConn.Open()

                Dim myDataAdapter As New System.Data.OleDb.OleDbDataAdapter("SELECT * FROM [Sheet1$]", objConn)
                myDataAdapter.Fill(objDS, "ExcelInfo")

                'Response.Write(objDS.Tables(0).Rows.Count & Session("ContractorID"))
                Dim clsAtt As ETS.BL.Attendance
                Try
                    clsAtt = New ETS.BL.Attendance
                    If clsAtt.btn_UploadAttendance(objDS, Session("ContractorID")) = True Then
                        Response.Write("<script type=""text/javascript"" language=javascript> alert(""File Imported Sucessfully!!!"");</script>")
                    End If
                Catch ex As Exception
                    Response.Write("Err:" & ex.Message)
                Finally
                    clsAtt = Nothing
                End Try
                myDataAdapter.Dispose()
            End If
        Catch ex As Exception
            Response.Write(ex.Message)
        Finally
            clsDS = Nothing
            objDS.Dispose()
            dtSheetName.Dispose()
            If objConn.State <> Data.ConnectionState.Closed Then
                objConn.Close()
                objConn = Nothing
            End If
        End Try
    End Sub
End Class
