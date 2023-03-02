Imports MainModule
Partial Class ERSSMain_IssueType
    Inherits BasePage
    Dim objMainModule As New MainModule
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            Dim clsERSSIC As ETS.BL.ERSSIssueCategory
            Dim DS As New Data.DataSet
            Dim DV As New Data.DataView
            Dim objRecGetIssueCate As Data.DataTableReader
            Try

                clsERSSIC = New ETS.BL.ERSSIssueCategory()
                clsERSSIC.ContractorID = Session("ContractorID").ToString
                DS = clsERSSIC.getIssueCategoryList()
                DV = New Data.DataView(DS.Tables(0))
                DV.RowFilter = "(IsDeleted IS NULL OR IsDeleted=0)"

                Dim varStrCAteName As String
                Dim varLstItemEmpty As New ListItem
                varLstItemEmpty.Text = "Please Select"
                varLstItemEmpty.Value = ""
                DropDownIssueCate.Items.Add(varLstItemEmpty)

                objRecGetIssueCate = DV.ToTable().CreateDataReader

                If objRecGetIssueCate.HasRows Then
                    While objRecGetIssueCate.Read
                        Dim varLStItem As New ListItem
                        If Not objRecGetIssueCate.IsDBNull(objRecGetIssueCate.GetOrdinal("CateName")) Then
                            varStrCAteName = objRecGetIssueCate.GetString(objRecGetIssueCate.GetOrdinal("CateName"))
                        End If
                        varLStItem.Text = varStrCAteName
                        varLStItem.Value = objRecGetIssueCate.GetGuid(objRecGetIssueCate.GetOrdinal("CategoryID")).ToString

                        DropDownIssueCate.Items.Add(varLStItem)
                    End While
                End If
                DropDownIssueCate.Items(0).Selected = True
            Catch ex As Exception
            Finally
                clsERSSIC = Nothing
                DS = Nothing
                DV = Nothing
            End Try
        End If
    End Sub
    Protected Sub DropDownIssueCate_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles DropDownIssueCate.SelectedIndexChanged
        Dim clsERSSIT As ETS.BL.ERSSIssueType
        Dim DS As New Data.DataSet
        Dim DV As New Data.DataView
        Dim objRecGetIssueTypes As Data.DataTableReader

        Try
            Dim varStrCateID As String
            varStrCateID = DropDownIssueCate.Items(DropDownIssueCate.SelectedIndex).Value.ToString
            If varStrCateID <> "" Then
                clsERSSIT = New ETS.BL.ERSSIssueType()
                clsERSSIT.CategoryID = varStrCateID
                DS = clsERSSIT.getIssueTypeList

                DV = New Data.DataView(DS.Tables(0))
                DV.RowFilter = "(IsDeleted IS NULL OR IsDeleted=0)"

                objRecGetIssueTypes = DV.ToTable.CreateDataReader
                If objRecGetIssueTypes.HasRows Then
                    While objRecGetIssueTypes.Read
                        Dim varTblRow As New TableRow
                        Dim varTblCellName As New TableCell
                        Dim varTblCellDesc As New TableCell
                        Dim varTblCellMode As New TableCell
                        Dim varTblCellCopyTo As New TableCell
                        Dim varTblCellEdit As New TableCell
                        Dim varStrId As String
                        varStrId = objRecGetIssueTypes.GetGuid(objRecGetIssueTypes.GetOrdinal("IssueID")).ToString

                        If Not objRecGetIssueTypes.IsDBNull(objRecGetIssueTypes.GetOrdinal("IssueName")) Then
                            varTblCellName.Text = objRecGetIssueTypes.GetString(objRecGetIssueTypes.GetOrdinal("IssueName"))
                        Else
                            varTblCellName.Text = ""
                        End If

                        If Not objRecGetIssueTypes.IsDBNull(objRecGetIssueTypes.GetOrdinal("Description")) Then
                            varTblCellDesc.Text = objRecGetIssueTypes.GetString(objRecGetIssueTypes.GetOrdinal("Description"))
                        Else
                            varTblCellDesc.Text = ""
                        End If

                        If Not objRecGetIssueTypes.IsDBNull(objRecGetIssueTypes.GetOrdinal("Mode")) Then
                            varTblCellMode.Text = objRecGetIssueTypes.GetString(objRecGetIssueTypes.GetOrdinal("Mode"))
                        Else
                            varTblCellMode.Text = ""
                        End If

                        If Not objRecGetIssueTypes.IsDBNull(objRecGetIssueTypes.GetOrdinal("CopyToTeam")) Then
                            varTblCellCopyTo.Text = objRecGetIssueTypes.GetString(objRecGetIssueTypes.GetOrdinal("CopyToTeam"))
                        Else
                            varTblCellCopyTo.Text = ""
                        End If
                        varTblCellEdit.Text = "<a href="""" OnClick=""window.open('EditIssueType.aspx?ID=" & varStrId & "','', 'width=450,height=360,status=1,scrollbars=1');return false;"" >EDIT</a>"
                        varTblRow.Cells.Add(varTblCellName)
                        varTblRow.Cells.Add(varTblCellDesc)
                        'varTblRow.Cells.Add(varTblCellMode)
                        'varTblRow.Cells.Add(varTblCellCopyTo)
                        varTblRow.Cells.Add(varTblCellEdit)
                        table1.Rows.Add(varTblRow)
                    End While
                End If

                objRecGetIssueTypes.Close()

                Dim varTblRowMain As New TableRow
                Dim varTblCellNameMain As New TableCell
                varTblCellNameMain.ColumnSpan = 5
                varTblCellNameMain.HorizontalAlign = HorizontalAlign.Right
                varTblCellNameMain.Text = "<a href="""" OnClick=""window.open('AddIssueType.aspx?CID=" & varStrCateID & "','', 'width=450,height=360,status=1,scrollbars=1');return false;"" >ADD</a>"

                varTblRowMain.Cells.Add(varTblCellNameMain)
                table1.Rows.Add(varTblRowMain)
            End If
        Catch ex As Exception
        Finally
            clsERSSIT = Nothing
            DS = Nothing
            DV = Nothing
        End Try
    End Sub
End Class
