Namespace ets
    Partial Class FileImportResult
        Inherits BasePage
        Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
            If Not String.IsNullOrEmpty(Request.Form("btnSearch")) Then
                rptBindPhy()
            End If
        End Sub

        Private Function rptBindPhy() As Boolean
            Dim ConString As String
            Dim SQLString As String
            Dim WhereClause As String = ""
            ConString = System.Configuration.ConfigurationManager.AppSettings("ETSCon")
            Dim oConn As New Data.SqlClient.SqlConnection
            Try
                oConn.ConnectionString = ConString
                oConn.Open()
                SQLString = "SELECT FIL.RecordID, FIL.MD5Value, FIL.CJobNumber, FIL.FileName, FIL.Status, FIL.Error, FIL.DateProcessed, FIL.version,U.FirstName + ' ' + U.LastName AS UserName, RSS.SettingName" & _
                " FROM tblFileImportLog AS FIL INNER JOIN tblUsers AS U ON FIL.UserID = U.UserID INNER JOIN (select SettingID,SettingName from tblRSS union select '11111111-1111-1111-1111-111111111111' as SettingID,'Voice Recorder' as SettingName from tblRSS) AS RSS ON FIL.ProcessID = RSS.SettingID"

                WhereClause = " where FIL.RecordID is not null"
                'FIL.version in (select max(version) from tblFileImportLog where MD5value=FIL.MD5value)"
                If Request("txtCJNum") <> "" Then
                    WhereClause = WhereClause & " and CJobNumber like '" & Request("txtCJNum") & "'"
                End If
                If Request("txtMD5") <> "" Then
                    WhereClause = WhereClause & " and MD5Value like '" & Request("txtMD5") & "'"
                End If
                If Request("txtClient") <> "" Then
                    WhereClause = WhereClause & " and FileName like '%" & Request("txtClient") & "%'"
                End If
                If Request("ddlStatus") <> "" Then
                    If Request("ddlStatus") = "2" Then
                        WhereClause = WhereClause & " and Status=0 and Error like 'Duplicate'"
                    ElseIf Request("ddlStatus") = "0" Then
                        WhereClause = WhereClause & " and md5value in (select md5value from tblfileimportlog where status=0 and md5value not in (select md5value from tblfileimportlog where status=1 group by md5value) group by md5value) "
                    Else
                        WhereClause = WhereClause & " and Status=" & Request("ddlStatus")
                    End If

                End If
                If Request("sDate") <> "" And Request("eDate") = "" Then
                    WhereClause = WhereClause & " and DateProcessed >='" & Request("sDate") & "'"
                ElseIf Request("sDate") = "" And Request("eDate") <> "" Then
                    WhereClause = WhereClause & " and DateProcessed <='" & Request("sDate") & "'"
                ElseIf Request("sDate") <> "" And Request("eDate") <> "" Then
                    WhereClause = WhereClause & " and DateProcessed between '" & Request("sDate") & "' and '" & Request("eDate") & "'"
                End If
                SQLString = SQLString & WhereClause & " order by FIL.MD5Value,FIL.version,FIL.DateProcessed"
                ',FIL.version
                'Response.Write(SQLString)
                'Response.End()

                Dim oCommand As New Data.SqlClient.SqlCommand(SQLString, oConn)
                Dim oRec As Data.SqlClient.SqlDataReader = oCommand.ExecuteReader()
                rptPhy.DataSource = oRec
                rptPhy.DataBind()
                oConn.Close()
                rptBindPhy = True
            Catch ex As Exception
                rptBindPhy = False
            Finally
                If oConn.State <> Data.ConnectionState.Closed Then
                    oConn.Close()
                    oConn = Nothing
                End If
            End Try
        End Function


        Protected Sub LinkButton1_Click(ByVal sender As Object, ByVal e As System.EventArgs)
            Dim lnk As LinkButton
            Dim hdn As HiddenField
            Dim lbl As Label
            Dim RecordID As String = String.Empty
            Dim MD5Value As String = String.Empty
            Dim BackUpFolder As String = String.Empty
            Dim InBoundPath As String = String.Empty
            lnk = CType(sender, LinkButton)
            If Not lnk.Text = "Re-Imported" Then
                Dim ConString As String
                Dim SQLString As String
                ConString = System.Configuration.ConfigurationManager.AppSettings("ETSCon")
                Dim oConn As New Data.SqlClient.SqlConnection
                Try
                    oConn.ConnectionString = ConString
                    oConn.Open()
                    hdn = lnk.Parent.FindControl("RecID")
                    RecordID = hdn.Value
                    lbl = lnk.Parent.FindControl("lblMD5")
                    MD5Value = lbl.Text
                    If Not String.IsNullOrEmpty(RecordID) Then
                        SQLString = "update tblFileImportLog set flgReImport=1 where MD5Value='" & MD5Value & "'"
                        Dim oCommand As New Data.SqlClient.SqlCommand(SQLString, oConn)
                        If oCommand.ExecuteNonQuery > 0 Then
                            SQLString = "SELECT FIL.FileName,FIL.CJobNumber,FIL.ProcessID,RSS.FolderPath, " & _
                                        "(select max(version) from tblFileImportLog where MD5Value='" & MD5Value & "') as MAxVersion " & _
                                        "FROM tblFileImportLog AS FIL LEFT OUTER JOIN " & _
                                        "tblRSS AS RSS ON FIL.ProcessID = RSS.SettingID " & _
                                        "WHERE (FIL.RecordID = '" & RecordID & "')"

                            oCommand = New Data.SqlClient.SqlCommand(SQLString, oConn)
                            Dim oRec As Data.SqlClient.SqlDataReader = oCommand.ExecuteReader
                            oRec.Read()
                            If oRec.HasRows Then
                                BackUpFolder = Server.MapPath("/ETS_Files") & "/BackUp"
                               
                                If oRec("ProcessID").ToString = "11111111-1111-1111-1111-111111111111" Then
                                    InBoundPath = Server.MapPath("/ETS_Files") & "/DSSInBound"
                                Else
                                    InBoundPath = Server.MapPath("/ETS_Files") & "/InBound/" & oRec("FolderPath")
                                End If

                                If Not String.IsNullOrEmpty(BackUpFolder) Then
                                    Dim oFile As New IO.FileInfo(IO.Path.Combine(BackUpFolder, RecordID & IO.Path.GetExtension(oRec("FileName"))))
                                    If oFile.Exists Then
                                        If Not String.IsNullOrEmpty(InBoundPath) Then
                                            oFile.CopyTo(IO.Path.Combine(InBoundPath, oRec("FileName")), True)
                                            Dim CJobNumber As String = oRec("CJobNumber")
                                            Dim ProcessID As String = oRec("ProcessID").ToString
                                            Dim MAxVersion As Integer = CInt(oRec("MAxVersion"))
                                            Dim FileName As String = oRec("FileName")
                                            If Not oRec.IsClosed Then oRec.Close()
                                            SQLString = "Insert into tblFileImportLog(RecordID,MD5Value,CJobNumber,FileName,Status,Error,DateProcessed,UserID,ProcessID,version,flgReImport) " & _
                                                        "Values('" & Guid.NewGuid.ToString & "','" & MD5Value & "','" & CJobNumber & "','" & FileName & "',NULL,'Re-Import','" & Now & "','" & Session("UserID") & "','" & ProcessID & "'," & MAxVersion + 1 & ",1)"
                                            oCommand = New Data.SqlClient.SqlCommand(SQLString, oConn)
                                            If oCommand.ExecuteNonQuery > 0 Then
                                                lnk.Font.Bold = True
                                                lnk.Text = "Re-Imported"
                                                lnk.Enabled = False
                                            End If
                                        End If
                                    Else
                                        Literal1.Text = "File Not exist"
                                    End If
                                Else
                                    Literal1.Text = "BackUp folder not found"
                                End If
                            Else
                                Literal1.Text = "File details not found"
                            End If
                            If Not oRec.IsClosed Then oRec.Close()
                        Else
                            Literal1.Text = "Cant set Record to re-import"
                        End If
                    Else
                        Literal1.Text = "Record Not Found"
                    End If
                    oConn.Close()
                Catch ex As Exception
                    Literal1.Text = ex.Message '"Exception Occured during process"
                Finally
                    If oConn.State <> Data.ConnectionState.Closed Then
                        oConn.Close()
                        oConn = Nothing
                    End If
                End Try
            End If
        End Sub
        Public Function getStatus(ByVal blnStatus) As String
            If String.IsNullOrEmpty(blnStatus) Then
                getStatus = "Pending Re-Import"
                'Dim lnk As LinkButton = rptPhy.FindControl("LinkButton1")
                'lnk.Visible = False
            Else
                If blnStatus Then
                    getStatus = "Imported"
                Else
                    getStatus = "Failed"
                End If
            End If
        End Function

    End Class
End Namespace