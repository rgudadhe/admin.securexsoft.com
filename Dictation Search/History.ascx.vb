Namespace HierarGridDemoVB

Partial  Class Authors
    Inherits System.Web.UI.UserControl

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region
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

                    Dim SQLString As String = "SELECT DISTINCT U.UserName, TL.Status, TL.LineCount, TL.DateModified, TL.version, TL.IP,CASE WHEN TL.Downloaded=1 THEN 'Yes' ELSE 'No' END AS 'Downloaded' , T.TemplateName, " & _
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
                    
                    'strSql.Append("SELECT LevelNo,Status as 'LevelName',0 as Seq FROM dbo.getStatus() WHERE LevelNo = " & hdnStatus.Value.ToString & _
                    '              " UNION " & _
                    '              "SELECT LevelNo,Status as 'LevelName',1 as Seq FROM dbo.getStatus() WHERE (LevelNo <> " & hdnStatus.Value.ToString & " and LevelNo in(select LevelNo from tblProductionLevels where type=1) and LevelNo<>(SELECT Audit FROM tblRSSStatus where iscontractor=1)) order by Seq")
                    strSql.Append("SELECT LevelNo,Status as 'LevelName',0 as Seq FROM dbo.getStatus() WHERE LevelNo = " & hdnStatus.Value.ToString & " and (ContractorID='" & Session("ContractorID") & "' or ContractorID='11111111-1111-1111-1111-111111111111') " & _
                             " UNION " & _
                             "SELECT LevelNo,Status as 'LevelName',1 as Seq FROM dbo.getStatus() WHERE (LevelNo <> " & hdnStatus.Value.ToString & " and LevelNo in(select LevelNo from tblProductionLevels where type=1 and (ContractorID='" & Session("ContractorID") & "' or LevelNo in (3,5)) and LevelNo<>(SELECT Audit FROM tblRSSStatus where iscontractor=1 and ContractorID='" & Session("ContractorID") & "'))) and (ContractorID='" & Session("ContractorID") & "' or ContractorID='11111111-1111-1111-1111-111111111111') order by Seq")
                    oCommand = New Data.SqlClient.SqlCommand(strSql.ToString, oConn)
                    oRec = oCommand.ExecuteReader
                    If oRec.HasRows Then
                        ddlStatus.DataSource = oRec
                        ddlStatus.DataTextField = "LevelName"
                        ddlStatus.DataValueField = "LevelNo"
                        ddlStatus.DataBind()
                    End If
                    oRec.Close()

                    strSql = Nothing

                    SQLString = "select isnull(U.UserName,'') as UserName from tblTranscriptionLog as TL left outer join tblusers as u on TL.UserID=U.UserID where TL.TranscriptionID = '" & hdnTransID.Value & "' and TL.Status=" & hdnStatus.Value.ToString

                    oCommand = New Data.SqlClient.SqlCommand(SQLString, oConn)
                    oRec = oCommand.ExecuteReader
                    If oRec.HasRows Then
                        oRec.Read()
                        lblUserName.Text = oRec("UserName").ToString
                    End If
                    oRec.Close()


                    SQLString = "SP_getTransAttributes"
                    oCommand = New Data.SqlClient.SqlCommand(SQLString, oConn)
                    Dim oParam As New Data.SqlClient.SqlParameter("@TransID", Data.SqlDbType.VarChar, 36)
                    oParam.Value = hdnTransID.Value
                    oCommand.Parameters.Add(oParam)
                    oCommand.CommandType = Data.CommandType.StoredProcedure
                    grdExtended.DataSource = oCommand.ExecuteReader
                    grdExtended.DataBind()
                    oRec.Close()
                    oConn.Close()
                Catch ex As Exception
                    Response.Write(ex.ToString)
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
                    SQLString = "SELECT 'Pending '+ +LevelName as Status FROM [ETS].[dbo].[tblProductionLevels] where ContractorID='" & Session("ContractorID") & "' and IsDeleted<>1 and LevelNo =" & lvlNo & _
                                "union SELECT 'Checked Out '+ +LevelName as Status FROM [ETS].[dbo].[tblProductionLevels] where ContractorID='" & Session("ContractorID") & "' and IsDeleted<>1 and LevelNo =" & lvlNo - 100
                    Dim oCommand As New Data.SqlClient.SqlCommand(SQLString, oConn)
                    Dim oRec As Data.SqlClient.SqlDataReader = oCommand.ExecuteReader()
                    oRec.Read()
                    If oRec.HasRows Then
                        getStatus = oRec("Status")
                    End If
                    oRec.Close()
                    oConn.Close()
                Catch ex As Exception

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
                strResponse = strResponse & SQLString & "<BR>"
                oCommand = New Data.SqlClient.SqlCommand(SQLString, oConn)
                oCommand.Transaction = thisTransaction
                Dim RowsMain As Integer = oCommand.ExecuteNonQuery()
                If String.IsNullOrEmpty(UserID) Then
                    SQLString = "insert into  tblTranscriptionLog(TranscriptionID,Status,DateModified,IP,AssignedBy) " & _
                                                                                   "values('" & hdnTransID.Value & "'," & NewLevel + ChkValue & ",'" & Now() & "','" & Request.UserHostAddress() & "','" & Session("UserID") & "')"
                    strResponse = strResponse & SQLString & "<BR>"
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
                    strResponse = strResponse & SQLString & "<BR>"
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
            Catch ex As Exception
                thisTransaction.Rollback()
                strResponse = "Request Failed Error: " & ex.ToString
                'writeStatus(SQLString, "Failed")
            Finally
                oConn.Close()
            End Try
            Dim pnl As Panel = Parent.Page.FindControl("iMessage")
            pnl.Visible = True
            Dim lbl As Label = Parent.Page.FindControl("lblMessage")
            lbl.Text = strResponse
            pnl = Parent.Page.FindControl("iMain")
            pnl.Visible = False
        End Sub
    End Class

End Namespace
