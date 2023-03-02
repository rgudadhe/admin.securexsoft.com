
Partial Class PrevJobsResult
    Inherits BasePage
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try

            If String.IsNullOrEmpty(Request.QueryString("PIndex")) = False Or String.IsNullOrEmpty(Request.Form("SEARCH")) = False Then
                iMain.Visible = True
                DBind()
            ElseIf IsPostBack Then
                iMain.Visible = True
            Else
                iMain.Visible = False
            End If
        Catch ex As Exception
            'Response.Write(ex.Message & "hh")
        End Try
    End Sub
    Private Sub DBind()

        If String.IsNullOrEmpty(Request.QueryString("SortBy")) Then
            ViewState("SortBy") = "PatientDOS"
        Else
            ViewState("SortBy") = Request.QueryString("SortBy").ToString
        End If
        If String.IsNullOrEmpty(Request.QueryString("SortOrder")) Then
            ViewState("SortOrder") = " DESC"
        Else
            ViewState("SortOrder") = Request.QueryString("SortOrder").ToString
        End If
        If String.IsNullOrEmpty(Request.QueryString("PIndex")) Then
            ViewState("PIndex") = 0
        Else
            ViewState("PIndex") = Request.QueryString("PIndex").ToString
        End If


        If IsPostBack = False And String.IsNullOrEmpty(Request.Form("SEARCH")) = False Then
            'iMain.Visible = True
            'Dim WhereClause = "WHERE (A.ContractorID = '" & Session("ContractorID").ToString & "') AND (TM.Status = 1073741824) "
            'If Not Request("DDLAccounts").ToString = "" Then
            '    WhereClause = WhereClause & " and TM.AccountID='" & Request("DDLAccounts").ToString & "'"
            'End If
            'If Not Request("txtPFName").ToString = "" Then
            '    WhereClause = WhereClause & " and PFName.value like '" & Request("txtPFName").ToString & "'"
            'End If
            'If Not Request("txtPLName").ToString = "" Then
            '    WhereClause = WhereClause & " and PLName.value like '" & Request("txtPLName").ToString & "'"
            'End If
            'If Not Request("txtTemplate").ToString = "" Then
            '    WhereClause = WhereClause & " and T.TemplateName like '" & Request("txtTemplate").ToString & "'"
            'End If
            'Session("WhereClause") = WhereClause
        End If
        'Dim ConString As String = System.Configuration.ConfigurationManager.AppSettings("ETSCon")
        'Dim oConn As New Data.SqlClient.SqlConnection
        'oConn.ConnectionString = ConString
        Try
            '        oConn.Open()
            '        Dim SQLString As String = "SELECT DISTINCT TOP (50) TM.TranscriptionID,TM.JobNumber, TM.DateCreated AS 'DateDictated',PFName.Value+' '+PLName.value as PatientName " & _
            '", PDOB.value as PatientDOB,PDOS.value as PatientDOS,PMRN.value as PatientMRN " & _
            '",  P.FirstName + ' ' + P.LastName AS DictatorName, A.AccountName, T.TemplateName " & _
            '"FROM SecureWeb.dbo.tblTranscriptionClient AS TM INNER JOIN " & _
            '                  "tblPhysicians AS P ON TM.DictatorID = P.PhysicianID INNER JOIN " & _
            '                  "tblAccounts AS A ON TM.AccountID = A.AccountID LEFT OUTER JOIN " & _
            '                  "tblTemplates AS T ON TM.TemplateID = T.TemplateID LEFT OUTER JOIN " & _
            '                      "(SELECT     TranscriptionID, Value " & _
            '                        "FROM          tblTranscriptionAttributes " & _
            '                        "WHERE      (AttributeID = '20DA1AD1-7345-47F4-95B3-1FF97F68DBBB')) AS PFName ON  " & _
            '                  "TM.TranscriptionID = PFName.TranscriptionID LEFT OUTER JOIN " & _
            '                      "(SELECT     TranscriptionID, Value " & _
            '                        "FROM          tblTranscriptionAttributes AS tblTranscriptionAttributes_4 " & _
            '                        "WHERE      (AttributeID = 'E70DDD9E-D0F3-43F2-96EC-24A3B35C05D0')) AS PLName ON " & _
            '                  "TM.TranscriptionID = PLName.TranscriptionID LEFT OUTER JOIN " & _
            '                      "(SELECT     TranscriptionID, Value " & _
            '                        "FROM          tblTranscriptionAttributes AS tblTranscriptionAttributes_3 " & _
            '                        "WHERE      (AttributeID = 'B6293293-DC9B-481E-922A-8C2D242074FE')) AS PDOB ON TM.TranscriptionID = PDOB.TranscriptionID LEFT OUTER JOIN " & _
            '                      "(SELECT     TranscriptionID, Value " & _
            '                        "FROM          tblTranscriptionAttributes AS tblTranscriptionAttributes_2 " & _
            '                        "WHERE      (AttributeID = '5319B32F-349E-4C69-938E-F09B8F642230')) AS PDOS ON TM.TranscriptionID = PDOS.TranscriptionID LEFT OUTER JOIN " & _
            '                      "(SELECT     TranscriptionID, Value " & _
            '                        "FROM          tblTranscriptionAttributes AS tblTranscriptionAttributes_1 " & _
            '                        "WHERE      (AttributeID = '26771DA9-3465-402F-AD8B-C430E26E96B3')) AS PMRN ON TM.TranscriptionID = PMRN.TranscriptionID "


            '        SQLString = SQLString & Session("WhereClause") & " order by " & ViewState("SortBy") & ViewState("SortOrder")
            '        'Label1.Text = SQLString
            '        'Response.Write("Query :" & SQLString)
            '        'Response.End()
            '        Dim objDA As New System.Data.SqlClient.SqlDataAdapter(SQLString, oConn)
            Dim clsPRep As ETS.BL.PrevReports
            Dim DS As New Data.DataSet
            Try
                clsPRep = New ETS.BL.PrevReports
                DS = clsPRep.SearchPrevReports(Session("ContractorID"), Session("WorkGroupID"), Request("DDLAccounts").ToString, Request("txtPFName").ToString, Request("txtPLName").ToString, Request("txtTemplate").ToString, String.Empty, String.Empty, String.Empty, String.Empty, String.Empty, String.Empty)
                If DS.Tables.Count > 0 Then
                    If DS.Tables(0).Rows.Count > 0 Then
                        dlist.DataSource = DS
                        dlist.DataBind()
                    Else
                        iMain.Visible = False
                    End If
                Else
                    iMain.Visible = False
                End If
            Catch ex As Exception
                Response.Write(ex.Message)
            Finally
                clsPRep = Nothing
                DS = Nothing
            End Try

            'Dim objDS As New System.Data.DataSet()
            dlist.CurrentPageIndex = ViewState("PIndex").ToString
            'objDA.Fill(objDS, "PreRec")
            'dlist.DataSource = objDS
            'dlist.DataBind()
            'oConn.Close()
            'If objDS.Tables(0).Rows.Count <= 0 Then
            '    iMain.Visible = False
            'End If

        Catch ex As Exception
            'Response.Write(ex.Message)
            Label1.Text = ex.Message
        Finally
            'If oConn.State <> Data.ConnectionState.Closed Then
            '    oConn.Close()
            '    oConn = Nothing
            'End If
        End Try
    End Sub

    Protected Sub dlist_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles dlist.PageIndexChanged
        'dlist.CurrentPageIndex = e.NewPageIndex
        'DBind(ViewState("SortBy").ToString & ViewState("SortOrder").ToString())
        Response.Redirect("PrevJobsResult.aspx?SortBy=" & ViewState("SortBy").ToString & "&SortOrder=" & ViewState("SortOrder").ToString & "&PIndex=" & e.NewPageIndex.ToString, True)
    End Sub

    Protected Sub dlist_SortCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridSortCommandEventArgs) Handles dlist.SortCommand
        If String.IsNullOrEmpty(ViewState("SortOrder")) Then
            ViewState("SortOrder") = " ASC"
        ElseIf ViewState("SortOrder").ToString = " ASC" Then
            ViewState("SortOrder") = " DESC"
        Else
            ViewState("SortOrder") = " ASC"
        End If
        ViewState("SortBy") = e.SortExpression.ToString
        Response.Redirect("PrevJobsResult.aspx?SortBy=" & ViewState("SortBy").ToString & "&SortOrder=" & ViewState("SortOrder").ToString & "&PIndex=" & ViewState("PIndex").ToString, True)
        'DBind(e.SortExpression.ToString & ViewState("SortOrder").ToString)
    End Sub
    
End Class
