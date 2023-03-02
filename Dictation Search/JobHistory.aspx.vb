
Partial Class Dictation_Search_JobHistory
    Inherits BasePage
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            Dim oRec As Data.DataTableReader
            Dim clsTrans As ETS.BL.Transcription
            Dim DSHistory As New Data.DataSet
            Dim DSLevels As New Data.DataSet
            Dim DSUserName As New Data.DataSet
            Dim DSAttributes As New Data.DataSet
            Try
                hdnTransID.Value = Request("MhdnTransID").ToString
                hdnStatus.Value = Request("MhdnStatus").ToString

                lblAccName.Text = Request("MhdnAccName").ToString
                lblAccNo.Text = Request("MhdnAccNo").ToString
                lblContractorName.Text = Request("MhdnContractorName").ToString
                lblDictatorName.Text = Request("MhdnDictatorName").ToString
                lblPINNo.Text = Request("MhdnPinNo").ToString
                lblSignedName.Text = Request("MhdnSignedName").ToString
                lblJobNo.Text = Request("MhdnJobNo").ToString
                lblCustJobNo.Text = Request("MhdnCustJobNo").ToString
                lblDtCreated.Text = Request("MhdnDtCreated").ToString
                lblDtDictated.Text = Request("MhdnDtDictated").ToString
                txtTAT.Text = Request("MhdnTAT").ToString
                lblRemaining.Text = Request("MhdnRemaining").ToString

                clsTrans = New ETS.BL.Transcription

                DSHistory = clsTrans.getTranscriptionHistory(hdnTransID.Value.ToString)
                If DSHistory.Tables.Count > 0 Then
                    If DSHistory.Tables(0).Rows.Count > 0 Then
                        rptHistory.DataSource = DSHistory
                        rptHistory.DataBind()
                    End If
                End If

                DSLevels = clsTrans.getLevelsByTransID(hdnStatus.Value.ToString, Session("ContractorID"))

                If DSLevels.Tables.Count > 0 Then
                    If DSLevels.Tables(0).Rows.Count > 0 Then
                        ddlStatus.DataSource = DSLevels
                        ddlStatus.DataTextField = "LevelName"
                        ddlStatus.DataValueField = "LevelNo"
                        ddlStatus.DataBind()
                        ddlStatus.SelectedIndex = 0
                    End If
                End If

                DSUserName = clsTrans.getUserNameByTransID(hdnTransID.Value.ToString, hdnStatus.Value.ToString)
                If DSUserName.Tables.Count > 0 Then
                    If DSUserName.Tables(0).Rows.Count > 0 Then
                        oRec = DSUserName.Tables(0).CreateDataReader
                        If oRec.HasRows Then
                            oRec.Read()
                            lblUserName.Text = oRec("UserName").ToString
                        End If
                        oRec.Close()
                    End If
                End If

                DSAttributes = clsTrans.getTransAttributes(hdnTransID.Value.ToString)
                If DSAttributes.Tables.Count > 0 Then
                    If DSAttributes.Tables(0).Rows.Count > 0 Then
                        grdExtended.DataSource = DSAttributes
                        grdExtended.DataBind()
                    End If
                End If
            Catch ex As Exception
            Finally
                clsTrans = Nothing
                DSHistory.Dispose()
                DSLevels.Dispose()
                DSUserName.Dispose()
                DSAttributes.Dispose()
                oRec = Nothing
            End Try
        End If
    End Sub
    Public Function getStatus(ByVal lvlNo As Integer) As String
        Dim varReturn As String = String.Empty
        If lvlNo = 1073741824 Then
            varReturn = "Finished"
        Else
            Dim clsTrans As ETS.BL.Transcription
            Try
                clsTrans = New ETS.BL.Transcription
                varReturn = clsTrans.getTransStatus(lvlNo, Session("ContractorID"))

            Catch ex As Exception
            Finally
                clsTrans = Nothing
            End Try
        End If
        Return varReturn
    End Function
    Protected Sub btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSave.Click
        Dim JobNumber As String = String.Empty
        Dim SQLString As String = String.Empty
        Dim CountUpdated, i As Integer
        Dim ConString As String
        Dim UserID As String = String.Empty
        Dim TAT As String = String.Empty
        Dim ChkValue As Integer
        Dim strResponse As String = String.Empty
        ConString = System.Configuration.ConfigurationManager.AppSettings("ETSCon")
        Dim oConn As New Data.SqlClient.SqlConnection
        Dim oCommand As New Data.SqlClient.SqlCommand
        Dim thisTransaction As Data.SqlClient.SqlTransaction
        Try
            oConn.ConnectionString = ConString
            oConn.Open()
            thisTransaction = oConn.BeginTransaction()
            Dim btn As Button = CType(sender, Button)
            Dim txt As TextBox = btn.Parent.FindControl("txtAltUserName")
            Dim UserName As String = txt.Text
            Dim ddl As DropDownList = btn.Parent.FindControl("ddlStatus")
            Dim NewLevel As String = ddl.SelectedValue.ToString
            Dim NewLevelName As String = ddl.SelectedItem.Text
            txt = btn.Parent.FindControl("txtTAT")
            If Not String.IsNullOrEmpty(txt.Text) Then
                TAT = txt.Text
            Else
                TAT = 0
            End If
            SQLString = "SELECT U.UserID FROM tblUsers AS U INNER JOIN tblUsersLevels AS UL ON U.UserID = UL.UserID where dbo.chkLevel(UL.ProductionLevel," & NewLevel & ")='true' and U.username = '" & UserName & "' and (U.isdeleted is null or U.isdeleted=0)"
            oCommand = New Data.SqlClient.SqlCommand(SQLString, oConn)
            oCommand.Transaction = thisTransaction
            Dim oRec As Data.SqlClient.SqlDataReader = oCommand.ExecuteReader()
            oRec.Read()
            If oRec.HasRows Then
                UserID = oRec("UserID").ToString
                ChkValue = 100
            Else
                strResponse = UserName & " can not be assigned to status " & NewLevelName & " " & vbNewLine
            End If
            oRec.Close()
            SQLString = "update tblTranscriptionMain set Status = " & CInt(NewLevel) + ChkValue & ", TAT=" & CInt(TAT) & " where TranscriptionID='" & hdnTransID.Value & "' and Status=" & CInt(hdnStatus.Value)
            'strResponse = strResponse & SQLString & "<BR>"
            oCommand = New Data.SqlClient.SqlCommand(SQLString, oConn)
            oCommand.Transaction = thisTransaction
            Dim RowsMain As Integer = oCommand.ExecuteNonQuery()
            If String.IsNullOrEmpty(UserID) Then
                SQLString = "insert into  tblTranscriptionLog(TranscriptionID,Status,DateModified,IP,AssignedBy) " & _
                                                                               "values('" & hdnTransID.Value & "'," & NewLevel + ChkValue & ",'" & Now() & "','" & Request.UserHostAddress() & "','" & Session("UserID") & "')"
                'strResponse = strResponse & SQLString & "<BR>"
            Else
                SQLString = "insert into  tblTranscriptionLog(TranscriptionID,UserID,UserLevel,Status,DateModified,IP,AssignedBy) " & _
                                                           "values('" & hdnTransID.Value & "','" & UserID & "'," & NewLevel & "," & NewLevel + ChkValue & ",'" & Now() & "','" & Request.UserHostAddress() & "','" & Session("UserID") & "')"
            End If

            oCommand = New Data.SqlClient.SqlCommand(SQLString, oConn)
            oCommand.Transaction = thisTransaction
            Dim rowsLog As Integer = oCommand.ExecuteNonQuery()
            Dim rowsCKStatus As Integer = 1
            If Not String.IsNullOrEmpty(UserID) Then
                SQLString = "DELETE FROM tblTranscriptionCKDStatus WHERE TranscriptionID='" & hdnTransID.Value & "' and Status= " & NewLevel + ChkValue
                'strResponse = strResponse & SQLString & "<BR>"
                oCommand = New Data.SqlClient.SqlCommand(SQLString, oConn)
                oCommand.Transaction = thisTransaction
                oCommand.ExecuteNonQuery()
                SQLString = "Insert Into tblTranscriptionCKDStatus(TranscriptionID,UserID,Status,DateModified) Values('" & hdnTransID.Value & "','" & UserID & "'," & NewLevel + ChkValue & ",'" & Now() & "')"
                strResponse = strResponse & SQLString & "<BR>"
                oCommand = New Data.SqlClient.SqlCommand(SQLString, oConn)
                oCommand.Transaction = thisTransaction
                rowsCKStatus = oCommand.ExecuteNonQuery()
            End If
            If RowsMain > 0 And rowsLog > 0 And rowsCKStatus > 0 Then
                'thisTransaction.Commit()
                If String.IsNullOrEmpty(strResponse) Then
                    strResponse = JobNumber & " has been successfully assigned to user " & UserName & " " & vbNewLine
                Else
                    strResponse = strResponse & vbNewLine & "Status: " & NewLevelName & " TAT: " & TAT
                End If
                'writeStatus(SQLString, "Successfull")
                CountUpdated = CountUpdated + 1
            Else
                thisTransaction.Rollback()
                strResponse = "Request Failed"
                'writeStatus(SQLString, "Failed")
            End If
            'Else
            '    oRec.Close()
            '    thisTransaction.Rollback()
            'End If

            lblMsg.Text = strResponse.ToString
            pnlData.Visible = False
            pnlMsg.Visible = True

        Catch ex As Exception
            thisTransaction.Rollback()
            strResponse = "Request Failed Error: " & ex.ToString
            'writeStatus(SQLString, "Failed")
        Finally
            oConn.Close()
        End Try
    End Sub
End Class
