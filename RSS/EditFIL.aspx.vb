
Partial Class RSS_EditFIL
    Inherits System.Web.UI.Page
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            txtfilename.Text = Trim(Request.QueryString("FileName")).ToString.Replace("@", "#").ToString
            hdnRecID.Value = Trim(Request.QueryString("RecID").ToString)
            hdnMD5Value.Value = Trim(Request.QueryString("MD5").ToString)
        End If
    End Sub
    Protected Sub btnSubmit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSubmit.Click

        Dim clsFIL As ETS.BL.FileImportLog
        Try
            clsFIL = New ETS.BL.FileImportLog
            'Response.Write(hdnRecID.Value.ToString & " " & hdnMD5Value.Value.ToString & " " & Trim(txtfilename.Text) & " " & Server.MapPath("/ETS_Files") & " " & Server.MapPath("/ETS_Files") & " " & Session("UserID").ToString)
            Response.Write(clsFIL.btnReImport_Click(hdnRecID.Value.ToString, hdnMD5Value.Value.ToString, Trim(txtfilename.Text), Server.MapPath("/ETS_Files"), Server.MapPath("/ETS_Files"), Session("UserID").ToString))
        Catch ex As Exception
            Response.Write(ex.Message)
        Finally
            clsFIL = Nothing
        End Try

        ''Dim RecordID As String = String.Empty
        ''Dim MD5Value As String = String.Empty
        ''Dim BackUpFolder As String = String.Empty
        ''Dim InBoundPath As String = String.Empty


        ''If Not String.IsNullOrEmpty(Request.QueryString("RecID")) Then
        ''    Dim ConString As String
        ''    Dim SQLString As String
        ''    ConString = System.Configuration.ConfigurationManager.AppSettings("ETSCon")
        ''    Dim oConn As New Data.SqlClient.SqlConnection
        ''    Try
        ''        oConn.ConnectionString = ConString
        ''        oConn.Open()

        ''        RecordID = Trim(Request.QueryString("RecID"))

        ''        MD5Value = Trim(Request.QueryString("MD5"))

        ''        If Not String.IsNullOrEmpty(RecordID) Then
        ''            SQLString = "update tblFileImportLog set flgReImport=1 where MD5Value='" & MD5Value & "'"
        ''            Dim oCommand As New Data.SqlClient.SqlCommand(SQLString, oConn)
        ''            If oCommand.ExecuteNonQuery > 0 Then
        ''                SQLString = "SELECT FIL.FileName,FIL.CJobNumber,FIL.ProcessID,RSS.FolderPath, " & _
        ''                            "(select max(version) from tblFileImportLog where MD5Value='" & MD5Value & "') as MAxVersion " & _
        ''                            "FROM tblFileImportLog AS FIL LEFT OUTER JOIN " & _
        ''                            "tblRSS AS RSS ON FIL.ProcessID = RSS.SettingID " & _
        ''                            "WHERE (FIL.RecordID = '" & RecordID & "')"

        ''                oCommand = New Data.SqlClient.SqlCommand(SQLString, oConn)
        ''                Dim oRec As Data.SqlClient.SqlDataReader = oCommand.ExecuteReader
        ''                oRec.Read()
        ''                If oRec.HasRows Then
        ''                    BackUpFolder = Server.MapPath("/ETS_Files") & "/BackUp"
        ''                    If oRec("ProcessID").ToString = "11111111-1111-1111-1111-111111111111" Then
        ''                        InBoundPath = Server.MapPath("/ETS_Files") & "/DSSInBound"
        ''                    Else
        ''                        InBoundPath = Server.MapPath("/ETS_Files") & "/InBound/" & oRec("FolderPath")
        ''                    End If

        ''                    If Not String.IsNullOrEmpty(BackUpFolder) Then
        ''                        Dim oFile As New IO.FileInfo(IO.Path.Combine(BackUpFolder, RecordID & IO.Path.GetExtension(oRec("FileName"))))
        ''                        If oFile.Exists Then
        ''                            Dim varFileName As String = String.Empty
        ''                            varFileName = Trim(txtfilename.Text)
        ''                            If Not String.IsNullOrEmpty(InBoundPath) Then
        ''                                oFile.CopyTo(IO.Path.Combine(InBoundPath, varFileName), True)
        ''                                Dim CJobNumber As String = oRec("CJobNumber")
        ''                                Dim ProcessID As String = oRec("ProcessID").ToString
        ''                                Dim MAxVersion As Integer = CInt(oRec("MAxVersion"))
        ''                                Dim FileName As String = varFileName
        ''                                If Not oRec.IsClosed Then oRec.Close()
        ''                                SQLString = "Insert into tblFileImportLog(RecordID,MD5Value,CJobNumber,FileName,Status,Error,DateProcessed,UserID,ProcessID,version,flgReImport) " & _
        ''                                            "Values('" & Guid.NewGuid.ToString & "','" & MD5Value & "','" & CJobNumber & "','" & FileName & "',NULL,'Re-Import','" & Now & "','" & Session("UserID") & "','" & ProcessID & "'," & MAxVersion + 1 & ",1)"
        ''                                oCommand = New Data.SqlClient.SqlCommand(SQLString, oConn)

        ''                                If oCommand.ExecuteNonQuery > 0 Then
        ''                                    'lnk.Font.Bold = True
        ''                                    'lnk.Text = "Re-Imported"
        ''                                    'lnk.Enabled = False

        ''                                    Response.Write("Re-Imported successfully")
        ''                                End If
        ''                            End If
        ''                        Else
        ''                            Response.Write("File Not exist")
        ''                        End If
        ''                    Else
        ''                        Response.Write("BackUp folder not found")
        ''                    End If
        ''                Else
        ''                    Response.Write("File details not found")
        ''                End If
        ''                If Not oRec.IsClosed Then oRec.Close()
        ''            Else
        ''                Response.Write("Cant set Record to re-import")
        ''            End If
        ''        Else
        ''            Response.Write("Record Not Found")
        ''        End If
        ''        oConn.Close()
        ''    Catch ex As Exception
        ''        Response.Write(ex.Message) '"Exception Occured during process"
        ''    Finally
        ''        If oConn.State <> Data.ConnectionState.Closed Then
        ''            oConn.Close()
        ''            oConn = Nothing
        ''        End If
        ''    End Try
        ''End If
    End Sub
End Class
