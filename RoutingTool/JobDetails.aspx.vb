
Partial Class RoutingTool_JobDetails
    Inherits System.Web.UI.Page
    Public JobNumber As String = String.Empty
    Public CustJobID As String = String.Empty
    Public Status As String = String.Empty
    Public duration As String = String.Empty
    Public SubmitDate As String = String.Empty
    Public TAT As String = String.Empty
    Public DueDate As String = String.Empty
    Public Priority As String = String.Empty
    Public PName As String = String.Empty
    Public AccountName As String = String.Empty
    Public AccountNo As String = String.Empty
    Public UserName As String = String.Empty
    Public DDLSQL As String
    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        If Not IsPostBack Then
            Dim ConString As String = System.Configuration.ConfigurationManager.AppSettings("ETSCon")
            Dim oConn As New Data.SqlClient.SqlConnection
            oConn.ConnectionString = ConString
            oConn.Open()
            Dim oRec As Data.SqlClient.SqlDataReader
            Try
                'hdnTransID.Value = DataBinder.Eval(CType(Me.BindingContainer, DataGridItem).DataItem, "TranscriptionID").ToString
                'hdnStatus.Value = DataBinder.Eval(CType(Me.BindingContainer, DataGridItem).DataItem, "Status").ToString
                If Request("transid") <> "" Then
                    hdnTransID.Value = Request("transid")
                End If
                If Request("status") <> "" Then
                    hdnStatus.Value = Request("status")
                End If
                Dim SQLString As String = "SELECT DISTINCT U.UserName, TL.Status, TL.LineCount, TL.DateModified, TL.version, TL.IP,TL.Downloaded, T.TemplateName, " & _
                "AU.UserName AS AssignedBy FROM tblUsers AS U RIGHT OUTER JOIN " & _
                "tblTranscriptionLog AS TL ON U.UserID = TL.UserID LEFT OUTER JOIN " & _
                "tblUsers AS AU ON TL.AssignedBy = AU.UserID LEFT OUTER JOIN " & _
                "tblTemplates AS T ON TL.TemplateID = T.TemplateID " & _
                "WHERE (TL.TranscriptionID = '" & hdnTransID.Value & "') order by TL.DateModified desc"
                hdnres.Value = SQLString
                Dim oCommand As Data.SqlClient.SqlCommand = New Data.SqlClient.SqlCommand(SQLString, oConn)

                oRec = oCommand.ExecuteReader
                rptHistory.DataSource = oRec
                rptHistory.DataBind()
                oRec.Close()
                Dim strSql As New StringBuilder
                'SQLString = "SELECT LevelNo, 'Pending'+' '+LevelName as LevelName,1 as Seq FROM tblProductionLevels WHERE (LevelNo <> " & hdnStatus.Value.ToString & " and LevelNo<>1073741824) and (IsDeleted=0 or IsDeleted is null) and type=1 " & _
                '            "union SELECT LevelNo+100 as LevelNo, 'CheckedOut'+' '+LevelName as LevelName,0 as Seq FROM tblProductionLevels WHERE (LevelNo+100 = " & hdnStatus.Value.ToString & ") and (IsDeleted=0 or IsDeleted is null) and type=1 "
                'If Not hdnStatus.Value.ToString = "1073741824" Then
                '    strSql.Append("SELECT LevelNo,'Pending'+' '+LevelName as LevelName,0 as Seq FROM tblProductionLevels WHERE (LevelNo = " & hdnStatus.Value.ToString & ") and (IsDeleted=0 or IsDeleted is null) and type=1 union ")
                '    strSql.Append(SQLString)
                '    strSql.Append("union SELECT LevelNo , LevelName as LevelName,1 as Seq FROM tblProductionLevels WHERE (LevelNo = 1073741824) and (IsDeleted=0 or IsDeleted is null) and type=1 order by Seq")
                'Else
                '    strSql.Append(SQLString)
                '    strSql.Append("union SELECT LevelNo , LevelName as LevelName,0 as Seq FROM tblProductionLevels WHERE (LevelNo = 1073741824) and (IsDeleted=0 or IsDeleted is null) and type=1 order by Seq")
                'End If
                strSql.Append("SELECT LevelNo,Status as LevelName,0 as Seq FROM dbo.getStatus() WHERE LevelNo = " & hdnStatus.Value.ToString & _
                              " UNION " & _
                              "SELECT LevelNo,Status as LevelName,1 as Seq FROM dbo.getStatus() WHERE (LevelNo <> " & hdnStatus.Value.ToString & " and LevelNo in(select LevelNo from tblProductionLevels where type=1) and LevelNo<>(SELECT Audit FROM tblRSSStatus where iscontractor=1)) order by Seq")
                hdnLevelDropDown.Value = strSql.ToString
                strSql = Nothing
                SQLString = "SELECT DISTINCT TM.JobNumber, TM.CustJobID, TM.Status, TM.duration,TM.SubmitDate,TM.Type,TM.TranscriptioniD, TM.DateCreated, TM.TAT, TM.DateDictated, TM.Priority, P.FirstName, P.LastName, A.AccountName, A.AccountNo,TL.UserName, P.SignedName, P.PINNo, c.ContractorName " & _
                            "FROM tblTranscriptionMain AS TM INNER JOIN tblPhysicians AS P ON TM.DictatorID = P.PhysicianID INNER JOIN " & _
                            "tblAccounts AS A ON TM.AccountID = A.AccountID INNER JOIN " & _
                            "tblContractor AS c ON TM.ContractorID = c.ContractorID INNER JOIN " & _
                            "tblDictationCodes AS DC ON P.PhysicianID = DC.PhysicianID LEFT OUTER JOIN " & _
                            "(SELECT     TLog.TranscriptionID, U.UserName FROM tblUsers AS U RIGHT OUTER JOIN " & _
                            "tblTranscriptionLog AS TLog ON U.UserID = TLog.UserID WHERE      (TLog.TranscriptionID = '" & hdnTransID.Value & "') " & _
                            "AND (TLog.DateModified = (SELECT     MAX(DateModified) AS Expr1 FROM tblTranscriptionLog WHERE (TranscriptionID = '" & hdnTransID.Value & "')))) AS TL ON TL.TranscriptionID = TM.TranscriptionID " & _
                            "WHERE (TM.TranscriptionID IS NOT NULL) AND (TM.TranscriptionID = '" & hdnTransID.Value & "')"
                oCommand = New Data.SqlClient.SqlCommand(SQLString, oConn)
                oRec = oCommand.ExecuteReader
                Repeater1.DataSource = oRec
                Repeater1.DataBind()
                oRec.Close()

                SQLString = "SELECT TransA.Value, A.Caption FROM tblTranscriptionAttributes AS TransA INNER JOIN " & _
                  "tblTranscriptionMain AS TM ON TransA.TranscriptionID = TM.TranscriptionID INNER JOIN " & _
                  "tblAttributes AS A ON TransA.AttributeID = A.AttributeID where TM.TranscriptionID='" & hdnTransID.Value & "'"
                oCommand = New Data.SqlClient.SqlCommand(SQLString, oConn)
                oRec = oCommand.ExecuteReader
                grdExtended.DataSource = oRec
                grdExtended.DataBind()
                oRec.Close()
                oConn.Close()
            Catch ex As Exception
                Response.Write("Error: " & ex.ToString)
            Finally

            End Try
        End If

    End Sub

    Private Sub Page_DataBind(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.DataBinding
        '        Dim dgi As DataGridItem = CType(Me.BindingContainer, DataGridItem)
        '        Dim ds As DataSet = CType(dgi.DataItem, DataSet)
    End Sub
    Public Function getStatus(ByVal lvlNo As Integer) As String
        If lvlNo = 1073741824 Then
            getStatus = "Finished"
        Else
            Dim ConString As String
            Dim SQLString As String
            ConString = System.Configuration.ConfigurationManager.AppSettings("ETSCon")
            Dim oConn As New Data.SqlClient.SqlConnection
            Try
                oConn.ConnectionString = ConString
                oConn.Open()
                SQLString = "SELECT 'Pending '+ +LevelName as Status FROM [ETS].[dbo].[tblProductionLevels] where IsDeleted<>1 and LevelNo =" & lvlNo & _
                            "union SELECT 'Checked Out '+ +LevelName as Status FROM [ETS].[dbo].[tblProductionLevels] where IsDeleted<>1 and LevelNo =" & lvlNo - 100
                Dim oCommand As New Data.SqlClient.SqlCommand(SQLString, oConn)
                Dim oRec As Data.SqlClient.SqlDataReader = oCommand.ExecuteReader()
                oRec.Read()
                If oRec.HasRows Then
                    getStatus = oRec("Status")
                End If
                oRec.Close()
                oConn.Close()
            Catch ex As Exception
                Response.Write("Err " & ex.Message)
            End Try
        End If
    End Function
    Function datediffToWords(ByVal d1, ByVal d2)
        Dim minutes As Integer
        Dim Word As String
        minutes = DateDiff(DateInterval.Minute, d1, d2)
        If minutes <= 0 Then
            Word = DateDiff(DateInterval.Hour, d1, d2)
        Else
            Word = ""
            If minutes >= 24 * 60 Then
                Word = Word & _
                minutes \ (24 * 60) & " d, "
            End If
            minutes = minutes Mod (24 * 60)
            If minutes >= 60 Then
                Word = Word & minutes \ (60) & " h, "
            End If
            minutes = minutes Mod 60
            Word = Word & minutes & " m."
        End If
        datediffToWords = Word
    End Function
    Function datediffToMe(ByVal d1 As Double, ByVal d2 As Date) As String
        Dim DueDate As Date
        DueDate = DateAdd(DateInterval.Hour, d1, d2)
        Return DateDiff(DateInterval.Hour, Now(), DueDate).ToString
    End Function

    'Protected Sub ddlStatus_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs)
    '    Dim ddl As DropDownList = CType(sender, DropDownList)
    '    Response.Write(ddl.SelectedValue)

    'End Sub

    Protected Sub SaveChanges(ByVal sender As Object, ByVal e As System.EventArgs)
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
            SQLString = "SELECT U.UserID FROM tblUsers AS U INNER JOIN tblUsersLevels AS UL ON U.UserID = UL.UserID where dbo.chkLevel(UL.ProductionLevel," & NewLevel & ")='true' and U.username = '" & UserName & "' and U.ContractorID='" & Session("ContractorID") & "'"
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
            oCommand = New Data.SqlClient.SqlCommand(SQLString, oConn)
            oCommand.Transaction = thisTransaction
            Dim RowsMain As Integer = oCommand.ExecuteNonQuery()
            If String.IsNullOrEmpty(UserID) Then
                SQLString = "insert into  tblTranscriptionLog(TranscriptionID,Status,DateModified,IP,AssignedBy) " & _
                                                                               "values('" & hdnTransID.Value & "'," & NewLevel + ChkValue & ",'" & Now() & "','" & Request.UserHostAddress() & "','" & Session("UserID") & "')"
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
                oCommand = New Data.SqlClient.SqlCommand(SQLString, oConn)
                oCommand.Transaction = thisTransaction
                oCommand.ExecuteNonQuery()
                SQLString = "Insert Into tblTranscriptionCKDStatus(TranscriptionID,UserID,Status,DateModified) Values('" & hdnTransID.Value & "','" & UserID & "'," & NewLevel + ChkValue & ",'" & Now() & "')"
                oCommand = New Data.SqlClient.SqlCommand(SQLString, oConn)
                oCommand.Transaction = thisTransaction
                rowsCKStatus = oCommand.ExecuteNonQuery()
            End If
            If RowsMain > 0 And rowsLog > 0 And rowsCKStatus > 0 Then
                thisTransaction.Commit()
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
        Catch ex As Exception
            thisTransaction.Rollback()
            strResponse = "Request Failed Error: " & ex.ToString
            'writeStatus(SQLString, "Failed")
        Finally
            oConn.Close()
        End Try
        'Dim pnl As Panel = Parent.Page.FindControl("iMessage")
        'pnl.Visible = True
        'Dim lbl As Label = Parent.Page.FindControl("lblMessage")
        'lbl.Text = strResponse
        'pnl = Parent.Page.FindControl("iMain")
        'pnl.Visible = False
    End Sub

End Class
