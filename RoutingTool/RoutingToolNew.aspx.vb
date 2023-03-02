
Partial Class RoutingTool_RoutingToolNew
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            Dim clsPL As ETS.BL.ProductionLevels
            Dim DS As New Data.DataSet
            Try
                clsPL = New ETS.BL.ProductionLevels
                clsPL.ContractorID = Session("contractorid")
                clsPL.JobsRouting = True
                DS = clsPL.getPLevelList

                If DS.Tables.Count > 0 Then
                    If DS.Tables(0).Rows.Count > 0 Then
                        For Each DR As Data.DataRow In DS.Tables(0).Rows
                            Dim varLevelNo As Integer = DR("LevelNo")
                            Dim vartblRow As New HtmlTableRow
                            Dim vartblCellLName As New HtmlTableCell
                            Dim vartblCellPMin As New HtmlTableCell
                            Dim vartblCellAMin As New HtmlTableCell


                            vartblCellLName.Align = "center"
                            vartblCellLName.InnerHtml = "<a style=""font-family:Trebuchet MS; font-size:10pt;"" href=""RoutingToolMainPage.aspx?ddlLevels=" & varLevelNo & """>" & DR("LevelName") & "</a>"

                            Dim varMins As String = GetTotalMins(varLevelNo)
                            Dim varTempArr As String()
                            varTempArr = varMins.Split("#")

                            vartblCellPMin.Align = "center"

                            If Not String.IsNullOrEmpty(varTempArr(0)) Then
                                If Trim(UCase(varTempArr(0))) = Trim(UCase("0(0)")) Then
                                    vartblCellPMin.InnerText = 0
                                Else
                                    vartblCellPMin.InnerText = varTempArr(0)
                                End If
                            End If
                            vartblCellAMin.Align = "center"

                            If Not String.IsNullOrEmpty(varTempArr(1)) Then
                                If Trim(UCase(varTempArr(1))) = Trim(UCase("0(0)")) Then
                                    vartblCellAMin.InnerText = 0
                                Else
                                    vartblCellAMin.InnerText = varTempArr(1)
                                End If
                            End If

                            vartblRow.Cells.Add(vartblCellLName)
                            vartblRow.Cells.Add(vartblCellPMin)
                            vartblRow.Cells.Add(vartblCellAMin)


                            tblMain.Rows.Add(vartblRow)

                        Next
                        'ddlLevels.DataSource = DS
                        'ddlLevels.DataTextField = "LevelName"
                        'ddlLevels.DataValueField = "LevelNo"
                        'ddlLevels.DataBind()
                    End If
                End If
                'ddlLevels.Items.Insert(0, New ListItem("Please Select", String.Empty))
            Catch ex As Exception
                Response.Write(ex.Message)
            Finally
                DS = Nothing
                clsPL = Nothing
            End Try
        End If
    End Sub
    Protected Function GetTotalMins(ByVal LevelNo) As String
        Dim varReturn As String = String.Empty
        Dim DS As New Data.DataSet
        Dim clsRo As ETS.BL.Routing
        Try
            clsRo = New ETS.BL.Routing
            DS = clsRo.GetRoutingJobByLevel(Session("ContractorID").ToString, Session("WorkGroupID").ToString, LevelNo)
            If DS.Tables.Count > 0 Then
                If DS.Tables(0).Rows.Count > 0 Then
                    Dim varAEMins As Long = 0
                    Dim varCntAEMins As Long = 0
                    Dim varChkMins As Long = 0
                    Dim varCntChkMins As Long = 0

                    Dim vartempAColName As String = "AwaitingEntry" & LevelNo
                    Dim vartempACntColName As String = "CntAE" & LevelNo
                    Dim vartempCcolName As String = "CheckedOut" & LevelNo
                    Dim vartempCCntColName As String = "CntCH" & LevelNo


                    varAEMins = Format(DS.Tables(0).Compute("Sum(" & vartempAColName & ")", String.Empty) / 60, 0)
                    varCntAEMins = DS.Tables(0).Compute("Sum(" & vartempACntColName & ")", String.Empty)
                    varChkMins = Format(DS.Tables(0).Compute("Sum(" & vartempCcolName & ")", String.Empty) / 60, 0)
                    varCntChkMins = DS.Tables(0).Compute("Sum(" & vartempCCntColName & ")", String.Empty)

                    varReturn = varAEMins & "(" & varCntAEMins & ")"
                    varReturn = varReturn & "#" & varChkMins & "(" & varCntChkMins & ")"

                End If
            End If

        Catch ex As Exception
            Response.Write(ex.Message)
        Finally
            clsRo = Nothing
            DS.Dispose()
        End Try
        Return varReturn.ToString
    End Function
    'Protected Sub btnSubmit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSubmit.Click
    '    Server.Transfer("RoutingToolMainPage.aspx")
    'End Sub
End Class
