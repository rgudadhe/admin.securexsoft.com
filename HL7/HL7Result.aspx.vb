
Partial Class FaxPlus_FaxPlusResult
    Inherits BasePage
    Dim objDS As New System.Data.DataSet()
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try

            If String.IsNullOrEmpty(Request.QueryString("PIndex")) = False Or String.IsNullOrEmpty(Request.Form("SEARCH")) = False Then
                iMain.Visible = True
                DBind()
                BtnSetStatus.Visible = True
            ElseIf IsPostBack Then
                iMain.Visible = True
                BtnSetStatus.Visible = True
            Else
                iMain.Visible = False
                BtnSetStatus.Visible = False
            End If

        Catch ex As Exception
            Response.Write(ex.Message & "hh")
        End Try
    End Sub
    Private Sub DBind()
        Try
            If String.IsNullOrEmpty(Request.QueryString("SortBy")) Then
                ViewState("SortBy") = "JobNumber"
            Else
                ViewState("SortBy") = Request.QueryString("SortBy").ToString
            End If
            If String.IsNullOrEmpty(Request.QueryString("SortOrder")) Then
                ViewState("SortOrder") = " ASC"
            Else
                ViewState("SortOrder") = Request.QueryString("SortOrder").ToString
            End If
            If String.IsNullOrEmpty(Request.QueryString("PIndex")) Then
                ViewState("PIndex") = 0
            Else
                ViewState("PIndex") = Request.QueryString("PIndex").ToString
            End If


            If IsPostBack = False And String.IsNullOrEmpty(Request.Form("SEARCH")) = False Then

                iMain.Visible = True
                'Session("WhereClause") = String.Empty
                'If Not String.IsNullOrEmpty(Request("FStatus")) Then
                '    If Request("FStatus").ToString = "-1" Then
                '        Session("WhereClause") = Session("WhereClause") & " and (TM.HL7 <> 111 or TM.HL7 is null)"
                '    Else
                '        Session("WhereClause") = Session("WhereClause") & " and TM.HL7 = " & Request("FStatus")
                '    End If
                'End If
                'If Not String.IsNullOrEmpty(Request("Track").ToString) Then
                '    Session("WhereClause") = Session("WhereClause") & " and TM.JobNumber = " & Request("Track").ToString
                'End If
                'If Not String.IsNullOrEmpty(Request("Cust").ToString) Then
                '    Session("WhereClause") = Session("WhereClause") & " and TM.CustJobID Like '" & Request("Cust").ToString & "'"
                'End If
                'If Not String.IsNullOrEmpty(Request("PFirst").ToString) Then
                '    Session("WhereClause") = Session("WhereClause") & " and P.FirstName Like '" & Request("PFirst").ToString & "'"
                'End If
                'If Not String.IsNullOrEmpty(Request("PLast").ToString) Then
                '    Session("WhereClause") = Session("WhereClause") & " and P.LastName Like '" & Request("PLast").ToString & "'"
                'End If
                'If Not String.IsNullOrEmpty(Request("PtName").ToString) Then
                '    Session("WhereClause") = Session("WhereClause") & " and PFName.value+' '+PLName.value Like '" & Request("PtName").ToString & "'"
                'End If
                'If String.IsNullOrEmpty(Request("sDate").ToString) = False And String.IsNullOrEmpty(Request("eDate").ToString) = True Then
                '    Session("WhereClause") = Session("WhereClause") & " and TM.SubmitDate >= '" & Request("sDate").ToString & "'"
                'End If
                'If String.IsNullOrEmpty(Request("sDate").ToString) = True And String.IsNullOrEmpty(Request("eDate").ToString) = False Then
                '    Session("WhereClause") = Session("WhereClause") & " and TM.SubmitDate <= '" & Request("eDate").ToString & "'"
                'End If
                'If String.IsNullOrEmpty(Request("sDate").ToString) = False And String.IsNullOrEmpty(Request("eDate").ToString) = False Then
                '    Session("WhereClause") = Session("WhereClause") & " and TM.SubmitDate between '" & Request("sDate").ToString & "' and '" & Request("eDate").ToString & "'"
                'End If
            End If
            'Dim ConString As String = System.Configuration.ConfigurationManager.AppSettings("ETSCon")
            'Dim oConn As New Data.SqlClient.SqlConnection
            'oConn.ConnectionString = ConString
            Try
                '        oConn.Open()
                '        Dim SQLString As String = "SELECT TM.TranscriptionID,TM.JobNumber, TM.CustJobID,TM.SubmitDate, HLS.StatusDesc,HLH.DateModified as DateFinished,ISNULL(TM.Cstatus, '111') AS CStatus, " & _
                '"P.FirstName + ' ' + P.LastName AS Physician,PFName.value+' '+PLName.value as PtName,DOS.Value as DOS,MRN.value as MRN,TType.TemplateType " & _
                '"FROM tblTranscriptionMain AS TM INNER JOIN " & _
                '"tblPhysicians AS P ON TM.DictatorID = P.PhysicianID LEFT OUTER JOIN " & _
                '"tblHL7Status AS HLS on TM.HL7 = HLS.StatusID left outer join " & _
                '"(SELECT TranscriptionID,datemodified FROM tblHl7History H1 where status=111 and datemodified=(select max(datemodified) from tblHl7History H2 where H1.TranscriptionID=H2.TranscriptionID)) AS HLH on TM.TranscriptionID = HLH.TranscriptionID LEFT OUTER JOIN " & _
                '"attributevalue_pfirstname as PFName " & _
                '"on TM.TranscriptionID = PFName.TranscriptionID left outer join  " & _
                '"attributevalue_plastname as PLName " & _
                '"on TM.TranscriptionID = PLName.TranscriptionID left outer join  " & _
                '"attributevalue_dtofserv as DOS " & _
                '"on TM.TranscriptionID = DOS.TranscriptionID left outer join  " & _
                '"attributevalue_MedRN as MRN " & _
                '"on TM.TranscriptionID = MRN.TranscriptionID left outer join  " & _
                '"(SELECT TemplateID,TemplateType FROM tblTemplates) as TType on TM.TemplateID = TType.TemplateID " & _
                '"LEFT OUTER JOIN tblAccounts AS A ON TM.AccountID=A.AccountID " & _
                '"WHERE     (TM.Status = '1073741824') AND (TM.CStatus = 222) AND A.HL7feed=1 and TM.contractorID='" & Session("ContractorID") & "' and A.contractorID='" & Session("ContractorID") & "' "
                '        '",PDOS.value as DOS,PMRN.value as MRN, PDOB.value as DOB " & _

                '        '"(SELECT transcriptionid,Value FROM tblTranscriptionAttributes " & _
                '        '"where Attributeid='F0DEDE91-4B4B-4788-8837-FF7B73F9DFEA') as PDOB " & _
                '        '"on TM.TranscriptionID = PDOB.TranscriptionID left outer join " & _
                '        '"(SELECT transcriptionid,Value FROM tblTranscriptionAttributes " & _
                '        '"where Attributeid='E26FB2AB-BBC1-4A81-B35F-8F02DA6CD4B8') as PDOS " & _
                '        '"on TM.TranscriptionID = PDOS.TranscriptionID left outer join " & _
                '        '"(SELECT transcriptionid,Value FROM tblTranscriptionAttributes " & _
                '        '"where Attributeid='4f193db2-4c47-43c2-9752-e5506a5495a7') as PMRN " & _
                '        '"on TM.TranscriptionID = PMRN.TranscriptionID left outer join " & _
                '        SQLString = SQLString & Session("WhereClause")
                '        'SQLString = SQLString & " order by " & ViewState("SortBy") & ViewState("SortOrder")
                '        SQLString = "select * from (" & SQLString & ") as s where templatetype not like '%L%'" & " order by " & ViewState("SortBy") & ViewState("SortOrder")
                '        Session("SQLString") = SQLString
                '        'Response.Write(SQLString)
                '        'Response.End()
                '        Dim objDA As New System.Data.SqlClient.SqlDataAdapter(SQLString, oConn)

                Dim clsHL7 As ETS.BL.HL7Reports
                Dim Ds As New Data.DataSet
                Dim Ds1 As New Data.DataSet
                Try
                    clsHL7 = New ETS.BL.HL7Reports
                    Ds = clsHL7.SearchHL7Reports(Request("FStatus").ToString, Request("Track").ToString, Request("Cust").ToString, Request("PFirst").ToString, Request("PLast").ToString, Request("PtName").ToString, Request("sDate").ToString, Request("eDate").ToString, Session("ContractorID"), Session("WorkGroupID"))
                    If Ds.Tables.Count > 0 Then
                        If Ds.Tables(0).Rows.Count > 0 Then
                            'Response.Write(Ds.Tables(0).Rows.Count)
                            'objDS = Ds
                            'Ds1 = Ds

                            'Dim dc1 As New System.Data.DataColumn
                            'Dim dc2 As New System.Data.DataColumn
                            dlist.CurrentPageIndex = ViewState("PIndex").ToString

                            'dc1 = Ds.Tables(0).Columns("TranscriptionID")
                            'dc2 = Ds1.Tables(0).Columns("TranscriptionID")
                            'Dim dr As System.Data.DataRelation = New System.Data.DataRelation("Dic", dc1, dc2, False)
                            'objDS.Relations.Add(dr)
                            'dlist.TemplateControl.LoadTemplate("HL7History.ascx")
                            dlist.DataSource = Ds
                            dlist.DataBind()
                            dlist.RowExpanded.CollapseAll()
                        Else
                            iMain.Visible = False
                        End If
                    Else
                        iMain.Visible = False
                    End If

                Catch ex As Exception
                    Response.Write(ex.Message)
                Finally
                    Ds = Nothing
                    Ds1 = Nothing
                    clsHL7 = Nothing
                End Try

                
                'If objDS.Tables(0).Rows.Count <= 0 Then

                'End If
            Catch ex As Exception
                Response.Write(ex.Message)
            Finally
                'If oConn.State <> Data.ConnectionState.Closed Then
                '    oConn.Close()
                '    oConn = Nothing
                'End If
            End Try
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Protected Sub dlist_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles dlist.PageIndexChanged
        'dlist.CurrentPageIndex = e.NewPageIndex
        'DBind(ViewState("SortBy").ToString & ViewState("SortOrder").ToString())
        Response.Redirect("HL7Result.aspx?SortBy=" & ViewState("SortBy").ToString & "&SortOrder=" & ViewState("SortOrder").ToString & "&PIndex=" & e.NewPageIndex.ToString, True)

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
        Response.Redirect("HL7Result.aspx?SortBy=" & ViewState("SortBy").ToString & "&SortOrder=" & ViewState("SortOrder").ToString & "&PIndex=" & ViewState("PIndex").ToString, True)
        'DBind(e.SortExpression.ToString & ViewState("SortOrder").ToString)
    End Sub
    Protected Sub dlist_TemplateSelection(ByVal sender As Object, ByVal e As DBauer.Web.UI.WebControls.HierarGridTemplateSelectionEventArgs) Handles dlist.TemplateSelection
        'e.TemplateFilename = "HL7History.ascx"
    End Sub
    Protected Sub BtnSetStatus_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtnSetStatus.Click

        Dim ConString As String = System.Configuration.ConfigurationManager.AppSettings("ETSCon")
        Dim oConn As New Data.SqlClient.SqlConnection
        oConn.ConnectionString = ConString

        Try
            oConn.Open()
            For Each item As DataGridItem In dlist.Items
                Dim chk As CheckBox = item.FindControl("ChkStatus")
                If chk.Checked Then

                    Dim hdn As HiddenField
                    hdn = DirectCast(item.FindControl("hdnTransID"), HiddenField)

                    Dim varStrQuery As String = String.Empty
                    varStrQuery = "update tbltranscriptionmain set HL7=null where transcriptionid='" & hdn.Value & "'"

                    Dim objCmd As New Data.SqlClient.SqlCommand(varStrQuery, oConn)
                    objCmd.ExecuteNonQuery()



                    'Response.Write(varStrQuery & "<BR>")
                End If
            Next

            Response.Write("<font face=Trebuchet MS size=4>Status updated sucessfully</font>")
            Response.End()
        Catch ex As Exception
            If Trim(UCase(ex.Message)) <> Trim(UCase("Thread was being aborted.")) Then
                Response.Write(ex.Message)
            End If
        Finally
            If oConn.State <> Data.ConnectionState.Closed Then
                oConn.Close()
                oConn = Nothing
            End If
        End Try
    End Sub

    

    Protected Sub LinkButton1_Click1(ByVal sender As Object, ByVal e As System.EventArgs) Handles LinkButton1.Click
        Response.Redirect("ExportResult.aspx", True)

    End Sub
    

    
    
End Class
