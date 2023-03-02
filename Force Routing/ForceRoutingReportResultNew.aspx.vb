
Partial Class Force_Routing_ForceRoutingReportResult
    Inherits BasePage
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try

            If Request.QueryString("Opr") <> "" And Request.QueryString("ID") <> "" Then
                Dim varStrOpr As String = String.Empty
                Dim varStrID As String = String.Empty
                Dim varStrULevel As String = String.Empty

                varStrOpr = Request.QueryString("Opr")
                varStrID = Request.QueryString("ID")
                varStrULevel = Request.QueryString("UserLevel")

                Dim varStrQuery As String = String.Empty
                Dim varflag As Boolean
                Dim clsCR As ETS.BL.CustomRouting
                Try
                    clsCR = New ETS.BL.CustomRouting

                    If Trim(UCase(varStrOpr)) = Trim(UCase("Account")) Then
                        varflag = clsCR.btn_RemoveClick_For_Account(varStrID)
                    ElseIf Trim(UCase(varStrOpr)) = Trim(UCase("Dictator")) Then
                        varflag = clsCR.btn_RemoveClick_For_Dictator(varStrID)
                    ElseIf Trim(UCase(varStrOpr)) = Trim(UCase("Template")) Then
                        varflag = clsCR.btn_RemoveClick_For_Template(varStrID)
                    ElseIf Trim(UCase(varStrOpr)) = Trim(UCase("User")) Then
                        If Not String.IsNullOrEmpty(varStrULevel) Then
                            varflag = clsCR.btn_RemoveClick_For_User(varStrID, varStrULevel)
                        End If
                    End If

                    If varflag Then
                        DBind(varStrOpr, varStrULevel)
                    End If

                Catch ex As Exception
                    Response.Write(ex.Message)
                Finally
                    clsCR = Nothing
                End Try
            End If

            If Not Page.IsPostBack Then
                DBind(Request.Form("ddlRouting"), Request.Form("ULevel"))
            End If
        Catch ex As Exception
            Response.Write(ex.Message & "hh")
        End Try
    End Sub
    Private Sub DBind(ByVal Routing As String, ByVal ULevel As String)
        Dim varflag As Boolean = True
        If Routing <> "" Then
            tblResult.Rows.Clear()
            lblMsg.Text = ""
            Dim varUserLevelNo As String = String.Empty
            varUserLevelNo = ULevel
            Dim varStrQueryForceRouting As String = String.Empty

            Try

                Dim varArrCellName As New ArrayList

                Dim clsPL As ETS.BL.ProductionLevels
                Dim DS As New Data.DataSet
                Try
                    clsPL = New ETS.BL.ProductionLevels
                    clsPL.ForcedRouting = 1
                    clsPL.ContractorID = Session("ContractorID").ToString
                    DS = clsPL.getPLevelList
                    If DS.Tables.Count > 0 Then
                        If DS.Tables(0).Rows.Count > 0 Then
                            For Each oRec As Data.DataRow In DS.Tables(0).Rows

                                'If Not oRec.IsNull(oRec("LevelNo")) Then
                                If String.IsNullOrEmpty(varStrQueryForceRouting) Then
                                    varStrQueryForceRouting = ",(SELECT [ETS].[dbo].[chkLevel] (F.Levels," & oRec("LevelNo") & ")) as " & oRec("LevelName") & ""
                                Else
                                    varStrQueryForceRouting = varStrQueryForceRouting & ",(SELECT [ETS].[dbo].[chkLevel] (F.Levels," & oRec("LevelNo") & ")) as " & oRec("LevelName") & ""
                                End If
                                varArrCellName.Add(oRec("LevelName"))
                                'End If
                            Next
                        End If
                    End If
                Catch ex As Exception
                Finally
                    clsPL = Nothing
                End Try

                'lblMsg.Text = "Q : " & varStrQueryForceRouting
                Dim vartbl As String = String.Empty
                Dim varStrQuery As String = String.Empty

                Dim clsCR As ETS.BL.CustomRouting
                Dim DSRec As New Data.DataSet

                Try
                    clsCR = New ETS.BL.CustomRouting
                    If Trim(UCase(Routing)) = Trim(UCase("Account")) Then
                        DSRec = clsCR.ForceRoutingViewAccount(Session("ContractorID"), varStrQueryForceRouting)
                        vartbl = "Account"
                    ElseIf Trim(UCase(Routing)) = Trim(UCase("Dictator")) Then
                        DSRec = clsCR.ForceRoutingViewDictator(Session("ContractorID"), varStrQueryForceRouting)
                        vartbl = "Dictator"
                    ElseIf Trim(UCase(Routing)) = Trim(UCase("Template")) Then
                        DSRec = clsCR.ForceRoutingViewTemplate(Session("ContractorID"), varStrQueryForceRouting)
                        vartbl = "Template"
                    ElseIf Trim(UCase(Routing)) = Trim(UCase("User")) Then
                        If Not String.IsNullOrEmpty(varUserLevelNo) Then
                            DSRec = clsCR.ForceRoutingViewUser(Session("ContractorID"), varStrQueryForceRouting, varUserLevelNo)
                        Else
                            lblMsg.Text = "Please select user level"
                            lblMsg.ForeColor = Drawing.Color.Red
                            varflag = False
                        End If
                        vartbl = "User"
                    End If

                    If varflag Then
                        'lblMsg.Text = lblMsg.Text & "Count : " & DSRec.Tables(0).Rows.Count
                        Dim oRecM As Data.DataTableReader = DSRec.Tables(0).CreateDataReader

                        If oRecM.HasRows Then
                            Dim vartblRowM As New TableRow
                            Dim vartblCellnameM As New TableCell
                            vartblCellnameM.Text = "Name"
                            vartblRowM.Cells.Add(vartblCellnameM)
                            vartblCellnameM.HorizontalAlign = HorizontalAlign.Center

                            For i As Integer = 0 To varArrCellName.Count - 1
                                Dim varCell As New TableCell
                                varCell.Text = varArrCellName(i)
                                varCell.HorizontalAlign = HorizontalAlign.Center
                                vartblRowM.Cells.Add(varCell)
                            Next

                            Dim vartblCellRemoveM As New TableCell
                            vartblCellRemoveM.Text = ""
                            vartblRowM.Cells.Add(vartblCellRemoveM)

                            vartblRowM.CssClass = "SMSelected"
                            tblResult.Rows.Add(vartblRowM)


                            While oRecM.Read
                                Dim vartblRow As New TableRow
                                Dim vartblCellname As New TableCell
                                vartblCellname.Text = oRecM("Name")
                                vartblRow.Cells.Add(vartblCellname)

                                For j As Integer = 0 To varArrCellName.Count - 1
                                    Dim varCell As New TableCell
                                    Dim varValue As Boolean

                                    Dim varChkBox As New CheckBox

                                    varValue = oRecM(varArrCellName(j))
                                    If varValue Then
                                        varChkBox.Checked = True
                                    End If
                                    varChkBox.Enabled = False
                                    varCell.Controls.Add(varChkBox)


                                    varCell.HorizontalAlign = HorizontalAlign.Center

                                    vartblRow.Cells.Add(varCell)
                                Next

                                Dim vartblCellRemove As New TableCell

                                If Trim(UCase(Routing)) <> Trim(UCase("User")) Then
                                    vartblCellRemove.Text = "<a href=ForceRoutingReportResult.aspx?Opr=" & vartbl & "&ID=" & oRecM("ID").ToString & ">Remove</a>"
                                Else
                                    vartblCellRemove.Text = "<a href=ForceRoutingReportResult.aspx?Opr=" & vartbl & "&ID=" & oRecM("ID").ToString & "&UserLevel=" & varUserLevelNo & ">Remove</a>"
                                End If
                                vartblRow.Cells.Add(vartblCellRemove)
                                tblResult.Rows.Add(vartblRow)
                            End While
                        Else
                            lblMsg.Text = "No records founds"
                        End If
                    End If

                Catch ex As Exception
                    Response.Write(ex.Message)
                Finally
                    DSRec = Nothing
                    clsCR = Nothing
                End Try
            Catch ex As Exception
                Response.Write(ex.Message)
            Finally
            End Try
        End If
    End Sub
End Class
