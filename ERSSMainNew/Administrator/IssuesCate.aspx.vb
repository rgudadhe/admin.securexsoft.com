Imports MainModule
Partial Class ERSSMain_IssuesCate
    Inherits BasePage
    Dim objMainModule As New MainModule
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            Dim clsERSSIC As ETS.BL.ERSSIssueCategory
            Dim objRecGetIssueCate As Data.DataTableReader
            Dim DS As New Data.DataSet
            Dim DV As New Data.DataView
            Try

                Dim varStrCateName As String
                Dim varStrDesc As String
                Dim varStrID As String
                clsERSSIC = New ETS.BL.ERSSIssueCategory()
                clsERSSIC.ContractorID = Session("ContractorID").ToString
                DS = clsERSSIC.getIssueCategoryList()
                DV = New Data.DataView(DS.Tables(0))
                DV.RowFilter = "(IsDeleted IS NULL OR IsDeleted=0)"
                objRecGetIssueCate = DV.ToTable().CreateDataReader
                If objRecGetIssueCate.HasRows Then
                    While objRecGetIssueCate.Read
                        Dim varTblRow As New TableRow
                        Dim varTblCellName As New TableCell
                        Dim varTblCellDesc As New TableCell
                        Dim varTblCellEdit As New TableCell
                        varStrID = objRecGetIssueCate.GetGuid(objRecGetIssueCate.GetOrdinal("CategoryID")).ToString
                        If Not objRecGetIssueCate.IsDBNull(objRecGetIssueCate.GetOrdinal("CateName")) Then
                            varStrCateName = objRecGetIssueCate.GetString(objRecGetIssueCate.GetOrdinal("CateName"))
                        End If
                        If Not objRecGetIssueCate.IsDBNull(objRecGetIssueCate.GetOrdinal("Description")) Then
                            varStrDesc = objRecGetIssueCate.GetString(objRecGetIssueCate.GetOrdinal("Description"))
                        End If
                        varTblCellName.Text = varStrCateName
                        varTblCellDesc.Text = varStrDesc
                        varTblCellEdit.Text = "<a href="""" OnClick=""window.open('EditIssueCate.aspx?ID=" & varStrID & "','', 'width=450,height=240,status=1,scrollbars=1');return false;"" >EDIT</a>"

                        varTblRow.Cells.Add(varTblCellName)
                        varTblRow.Cells.Add(varTblCellDesc)
                        varTblRow.Cells.Add(varTblCellEdit)

                        table1.Rows.Add(varTblRow)
                    End While
                End If
                objRecGetIssueCate.Close()
                Dim varTblRowMain As New TableRow
                Dim varTblCellNameMain As New TableCell
                varTblRowMain.HorizontalAlign = HorizontalAlign.Right
                varTblCellNameMain.HorizontalAlign = HorizontalAlign.Right
                varTblCellNameMain.Text = "<div style=text-align:right><a href="""" OnClick=""window.open('AddIssueCate.aspx','', 'width=450,height=240,status=1,scrollbars=1');return false;"" >ADD</a></div>"
                varTblCellNameMain.ColumnSpan = 3

                varTblRowMain.Cells.Add(varTblCellNameMain)
                table1.Rows.Add(varTblRowMain)
            Catch ex As Exception
            Finally
                DS = Nothing
                clsERSSIC = Nothing
                objRecGetIssueCate = Nothing
                DV = Nothing
            End Try
        End If
    End Sub
End Class
