Imports System
Imports System.Data
Namespace ets
    Partial Class FileImportResultNew
        Inherits BasePage
        Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
            If Not Page.IsPostBack Then
                If Hsort.Value = "" Then
                    Hsort.Value = " DateProcessed"
                End If
                If Horder.Value = "" Then
                    Horder.Value = " DESC"
                End If
                If Not String.IsNullOrEmpty(Request.Form("btnSearch")) Then
                    rptBindPhy(Hsort.Value, Horder.Value)
                End If
            End If
        End Sub
        Private Function rptBindPhy(ByVal sortExpression As String, ByVal direction As String) As Boolean
            Try
                Dim clsFIL As New ets.BL.FileImportLog
                Dim DSFIL As DataSet = clsFIL.getFileImportLog(Session("ContractorID").ToString, Request("txtCJNum").ToString, Request("txtMD5").ToString, Request("txtClient").ToString, Request("ddlStatus").ToString, Request("sDate").ToString, Request("eDate").ToString)
                clsFIL = Nothing
                Dim dc1 As New System.Data.DataColumn
                Dim dc2 As New System.Data.DataColumn
                Dim DT2 As DataTable = DSFIL.Tables(0).Copy
                DSFIL.Tables(0).TableName = "ImportLog"
                DT2.TableName = "History"
                DSFIL.Tables.Add(DT2)
                dc1 = DSFIL.Tables(0).Columns("RecordID")
                dc2 = DSFIL.Tables(1).Columns("RecordID")
                Dim dRel As System.Data.DataRelation = New System.Data.DataRelation("Dic", dc1, dc2, False)
                DSFIL.Relations.Add(dRel)
                dlist.TemplateControl.LoadTemplate("History.ascx")
                dlist.DataSource = DSFIL
                dlist.DataBind()
                DSFIL.Dispose()
                rptBindPhy = True
                If dlist.Items.Count > 0 Then
                    btnReImport.Visible = True
                End If
            Catch ex As Exception
                Response.Write(ex.Message)
                rptBindPhy = False
            End Try
        End Function
        Protected Function ReImport(ByVal ProcessID As String, ByVal CJobNumber As String, ByVal FileName As String, ByVal RecID As String, ByVal MD5 As String) As String
            Dim BackUpFolder As String = String.Empty
            Dim InBoundPath As String = String.Empty
            If Len(RecID) = 36 Then
                Dim clsrss As New ets.BL.RSS
                clsrss.SettingID = ProcessID
                clsrss.getRSSDetails()
                Dim FolderPath As String = clsrss.FolderPath
                clsrss = Nothing
                BackUpFolder = Server.MapPath("/ETS_Files") & "/BackUp"
                If ProcessID = "11111111-1111-1111-1111-111111111111" Then
                    InBoundPath = Server.MapPath("/ETS_Files") & "/DSSInBound"
                Else
                    InBoundPath = Server.MapPath("/ETS_Files") & "/InBound/" & FolderPath
                End If
                If Not String.IsNullOrEmpty(BackUpFolder) Then

                    Dim oFile As New IO.FileInfo(IO.Path.Combine(BackUpFolder, RecID & IO.Path.GetExtension(FileName)))
                    If oFile.Exists Then
                        Dim CLSFIL As New ets.BL.FileImportLog
                        With CLSFIL
                            .MD5Value = MD5
                            .ProcessID = ProcessID
                            .RecordID = Guid.NewGuid().ToString
                            .CJobNumber = CJobNumber
                            .FileName = FileName
                            .Error = "Re-Import"
                            .UserID = Session("UserID").ToString
                            .DateProcessed = Now()
                            If .ReImport(Session("ContractorID").ToString) Then
                                Return "Sent for Re-Import"
                            Else
                                Return "Failed Re-Importing"
                            End If
                        End With
                        CLSFIL = Nothing
                    Else
                        Return "File Not exist"
                    End If
                Else
                    Return "BackUp folder not found"
                End If
            Else
                Return "Record Not Found"
            End If
        End Function
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
        Protected Sub dlist_TemplateSelection(ByVal sender As Object, ByVal e As DBauer.Web.UI.WebControls.HierarGridTemplateSelectionEventArgs) Handles dlist.TemplateSelection
            e.TemplateFilename = "History.ascx"
        End Sub

        Protected Sub btnReImport_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnReImport.Click
            Dim varStr As String = String.Empty
            varStr = "<table border=1><tr><td><b>FileName</b></td><td><b>Status</b></td></tr>"
            For i As Integer = 0 To dlist.Items.Count - 1
                Dim Chk As CheckBox = CType(dlist.Items(i).FindControl("chkJob"), CheckBox)
                If Not Chk Is Nothing Then
                    If Chk.Checked Then
                        Dim hdnID As HiddenField = Chk.FindControl("hdnID")
                        Dim varRecID As String = hdnID.Value

                        hdnID = Chk.FindControl("hdnFileName")
                        Dim varFileName = hdnID.Value

                        hdnID = Chk.FindControl("hdnCJobNumber")
                        Dim varCJobNumber = hdnID.Value

                        hdnID = Chk.FindControl("hdnProcessID")
                        Dim varProcessID = hdnID.Value

                        Dim lbl As Label = Chk.FindControl("lblMD5Value")
                        Dim varMD5 = lbl.Text
                        'varStr = varStr & "<tr><td>" & varFileName & "</td><td>" & ReImport(varProcessID, varCJobNumber, varFileName, varRecID, varMD5) & "</td></tr>"
                        varStr = varStr & "<tr><td>" & varFileName & "</td><td>Processed</td></tr>"
                    End If
                End If
            Next

            If Not String.IsNullOrEmpty(varStr) Then
                varStr = varStr & "</table>"
            End If
            Response.Write("<BR>" & varStr)
            Response.End()
            Exit Sub
        End Sub
        Private Property GridViewSortDirection() As SortDirection
            Get
                If ViewState("sortDirection") Is Nothing Then
                    ViewState("sortDirection") = SortDirection.Ascending
                End If
                Return DirectCast(ViewState("sortDirection"), SortDirection)
            End Get
            Set(ByVal value As SortDirection)
                ViewState("sortDirection") = value
            End Set
        End Property
        Protected Sub dlist_SortCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridSortCommandEventArgs) Handles dlist.SortCommand
            'rptBindPhy()
            'dlist.DataBind()
            Dim sortExpression As String = e.SortExpression
            ViewState("SortExpression") = sortExpression
            If GridViewSortDirection = SortDirection.Ascending Then
                GridViewSortDirection = SortDirection.Descending
                Hsort.Value = sortExpression
                Horder.Value = " DESC "
            Else
                GridViewSortDirection = SortDirection.Ascending
                Hsort.Value = sortExpression
                Horder.Value = " ASC "
            End If
            'Response.Write(HdnWhereClause.Value)

            rptBindPhy(Hsort.Value, Horder.Value)
            dlist.DataBind()
        End Sub
    End Class
End Namespace