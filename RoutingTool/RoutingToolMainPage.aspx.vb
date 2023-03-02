
Partial Class RoutingTool_RoutingToolMainPage
    Inherits BasePage
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Dim varLevelName As String = String.Empty
        Dim varLevelNo As String = String.Empty
        Dim clsPL As ETS.BL.ProductionLevels
        Try
            clsPL = New ETS.BL.ProductionLevels
            clsPL.ContractorID = Session("ContractorID").ToString
            clsPL.LevelNo = Request("ddlLevels")
            clsPL.getPLevelDetails()
            If Not String.IsNullOrEmpty(clsPL.LevelName) Then
                varLevelName = clsPL.LevelName
            End If
        Catch ex As Exception
        Finally
            clsPL = Nothing
        End Try
        varLevelNo = Request("ddlLevels")



        Dim vartblRowMain As New TableRow
        Dim vartblCellAccName As New TableCell
        Dim vartblCellProtocolMins As New TableCell
        Dim vartblCellJTBRMins As New TableCell
        Dim vartblCellFinishedMins As New TableCell
        Dim vartblCellPendingMins As New TableCell
        Dim vartblCellCheckedMins As New TableCell
        Dim vartblCellBacklogMins As New TableCell

        vartblCellAccName.Text = "Account Name"
        vartblCellAccName.HorizontalAlign = HorizontalAlign.Center
        vartblCellProtocolMins.Text = "PROT"
        vartblCellProtocolMins.HorizontalAlign = HorizontalAlign.Center
        vartblCellJTBRMins.Text = "JTBR"
        vartblCellJTBRMins.HorizontalAlign = HorizontalAlign.Center
        vartblCellFinishedMins.Text = "FINS"
        vartblCellFinishedMins.HorizontalAlign = HorizontalAlign.Center
        vartblCellPendingMins.Text = varLevelName.ToString & " Pending Mins"
        vartblCellPendingMins.HorizontalAlign = HorizontalAlign.Center
        vartblCellCheckedMins.Text = varLevelName.ToString & " Assigned Mins"
        vartblCellCheckedMins.HorizontalAlign = HorizontalAlign.Center
        vartblCellBacklogMins.Text = "Backlog"
        vartblCellBacklogMins.HorizontalAlign = HorizontalAlign.Center

        vartblRowMain.Cells.Add(vartblCellAccName)
        vartblRowMain.Cells.Add(vartblCellProtocolMins)
        vartblRowMain.Cells.Add(vartblCellJTBRMins)
        vartblRowMain.Cells.Add(vartblCellFinishedMins)
        vartblRowMain.Cells.Add(vartblCellPendingMins)
        vartblRowMain.Cells.Add(vartblCellCheckedMins)
        vartblRowMain.Cells.Add(vartblCellBacklogMins)
        vartblRowMain.BackColor = Drawing.Color.Gray
        vartblRowMain.ForeColor = Drawing.Color.White
        vartblRowMain.Font.Name = "Trebuchet MS"
        vartblRowMain.Font.Bold = True
        Table2.Rows.Add(vartblRowMain)

        Dim DS As New Data.DataSet
        Dim clsRo As ETS.BL.Routing
        Try
            clsRo = New ETS.BL.Routing
            Session("WorkGroupID") = "3BC5ABE9-8EDC-4CCD-8FD3-C8A001ED8083"

            DS = clsRo.GetRoutingJobByLevel(Session("ContractorID").ToString, Session("WorkGroupID").ToString, varLevelNo)
            If DS.Tables.Count > 0 Then
                If DS.Tables(0).Rows.Count > 0 Then
                    For Each DRRec As Data.DataRow In DS.Tables(0).Rows
                        
                        Dim varAccID As String = String.Empty
                        Dim varRow As New TableRow
                        varRow.Font.Name = "Trebuchet MS"
                        varRow.BackColor = Drawing.Color.LightGray
                        Dim varcellAcc As New TableCell
                        varcellAcc.HorizontalAlign = HorizontalAlign.Left
                        Dim varCellJTBR As New TableCell
                        varCellJTBR.HorizontalAlign = HorizontalAlign.Center
                        Dim varcellProt As New TableCell
                        varcellProt.HorizontalAlign = HorizontalAlign.Center
                        Dim varcellFinished As New TableCell
                        varcellFinished.HorizontalAlign = HorizontalAlign.Center
                        Dim varcellPendingMins As New TableCell
                        varcellPendingMins.HorizontalAlign = HorizontalAlign.Center
                        Dim varcellAssignedMins As New TableCell
                        varcellAssignedMins.HorizontalAlign = HorizontalAlign.Center
                        Dim varcellBacklogMins As New TableCell
                        varcellBacklogMins.HorizontalAlign = HorizontalAlign.Center

                        Dim ProtocolMins As Integer = 0
                        Dim FinishedMins As Integer = 0
                        Dim PrvPendingMins As Integer = 0

                        Dim AwaitingEntry As Integer = 0
                        Dim AwaitingEntryCnt As Integer = 0
                        Dim CheckedOut As Integer = 0
                        Dim CheckedOutCnt As Integer = 0
                        Dim DoneMins As Integer = 0
                        Dim NotFinished As Integer = 0
                        varAccID = DRRec("AccountID").ToString
                        varcellAcc.Text = DRRec("Accountname")

                        If IsDBNull(DRRec("ProtocolMins")) Then
                            ProtocolMins = 0
                        Else
                            ProtocolMins = DRRec("ProtocolMins").ToString
                        End If

                        If IsDBNull(DRRec("ProtocolMins")) Then
                            AwaitingEntry = 0
                        Else
                            AwaitingEntry = DRRec("AwaitingEntry2").ToString
                        End If

                        AwaitingEntry = FormatNumber((DRRec("AwaitingEntry" & varLevelNo).ToString / 60), 0)
                        AwaitingEntryCnt = DRRec("CntAE" & varLevelNo).ToString
                        CheckedOut = FormatNumber((DRRec("CheckedOut" & varLevelNo).ToString / 60), 0)
                        CheckedOutCnt = DRRec("CntCH" & varLevelNo).ToString
                        DoneMins = FormatNumber((DRRec("DoneMins").ToString / 60), 0)
                        PrvPendingMins = FormatNumber((DRRec("PrvPendingMins").ToString / 60), 0)


                        NotFinished = AwaitingEntry + CheckedOut
                        varcellProt.Text = ProtocolMins
                        'Response.Write(AwaitingEntry + CheckedOut)

                        varCellJTBR.Text = AwaitingEntry + CheckedOut
                        varcellFinished.Text = DoneMins
                        varcellBacklogMins.Text = PrvPendingMins

                        If AwaitingEntry > 0 And AwaitingEntryCnt > 0 Then
                            varcellPendingMins.Text = "<a href='EmpstatusNew.aspx?AccID=" & varAccID.ToString & "&ProLevel=" & varLevelNo & "' style=""font-family:Trebuchet MS"" Target=_Blank>" & AwaitingEntry & "(" & AwaitingEntryCnt & ")" & "</a>"
                        Else
                            varcellPendingMins.Text = 0
                        End If

                        If CheckedOut > 0 And CheckedOutCnt > 0 Then
                            varcellAssignedMins.Text = "<a href='AccountsRouting.aspx?AccID=" & varAccID.ToString & "&ProLevel=" & varLevelNo + 100 & "' style=""font-family:Trebuchet MS"" Target=_Blank>" & CheckedOut & "(" & CheckedOutCnt & ")" & "</a>"
                        Else
                            varcellAssignedMins.Text = 0
                        End If


                        varRow.Cells.Add(varcellAcc)
                        varRow.Cells.Add(varcellProt)
                        varRow.Cells.Add(varCellJTBR)
                        varRow.Cells.Add(varcellFinished)
                        varRow.Cells.Add(varcellPendingMins)
                        varRow.Cells.Add(varcellAssignedMins)
                        varRow.Cells.Add(varcellBacklogMins)
                        Table2.Rows.Add(varRow)


                    Next
                End If
            End If

        Catch ex As Exception
            Response.Write(ex.Message)
        End Try



        'Dim t2 As New Table
        't2.Style("width") = "100%"
        't2.BorderWidth = 2
        't2.GridLines = GridLines.Both
        'Dim ProtocolMins As Integer
        'Dim DoneMins As Integer
        'Dim pDoneMins As Integer
        'Dim AwaitingEntry As Integer
        'Dim CheckedOut As Integer
        'Dim NotFinished As Integer
        'Dim PrvPendingMins As Integer
        'Dim PendingMins As Integer
        'Dim PndDate As Date = ProcStartDate.AddDays(-1)
        'Dim strQuery As String

        'Dim Cell1 As New TableCell
        'Dim Cell2 As New TableCell
        'Dim Cell3 As New TableCell
        'Dim Cell4 As New TableCell
        'Dim Cell5 As New TableCell
        'Dim Cell6 As New TableCell
        'Dim CellP1 As New TableCell
        'Dim CellA1 As New TableCell
        'Dim CellP2 As New TableCell
        'Dim CellA2 As New TableCell
        'Dim CellPP As New TableCell
        'Dim AccID As String
        'Dim PndMins As Integer
        'Dim PndJobs As Integer
        'Dim AssgnMins As Integer
        'Dim AssgnJobs As Integer


        'Dim JIOS_P As Integer
        'Dim JIOS_A As Integer
        'Dim TJIOS_P As Integer
        'Dim TJIOS_A As Integer
        'Dim TotPndMins As Integer
        'Dim TotAssgnMins As Integer
        'Dim TotPPmins As Integer = 0
        'Dim i As Integer
        'Dim j As Integer
        'Dim LevelNo As Long
        'i = 0
        'JIOS_P = 0
        'JIOS_A = 0
        'Dim ACount As Integer
        'Dim Row1 As New TableRow
        'Dim Row2 As New TableRow
        'Row1.CssClass = "SMSelected"
        'Row1.Style("text-align") = "Center"
        ''Row1.CssClass = "noScroll"
        ''Row2.CssClass = "noScroll"
        'Dim LvlPName() As String
        'Dim LvlAName() As String
        'Dim ClmCount As Integer

        'Cell1.Text = "Account Name"
        'Cell2.Text = "PROT"
        'Cell4.Text = "JTBR"
        'Cell6.Text = "FINS"
        'CellP1.Text = "Pending Mins"
        'CellA1.Text = "Assigned Mins"
        'CellPP.Text = "Backlog"
        ''CellPP.RowSpan = 2
        ''Cell1.RowSpan = 2
        ''Cell2.RowSpan = 2
        ''Cell3.RowSpan = 2
        ''Cell4.RowSpan = 2
        ''Cell5.RowSpan = 2
        'Row1.Cells.Add(Cell1)
        'Row1.Cells.Add(Cell2)
        'Row1.Cells.Add(Cell4)
        'Row1.Cells.Add(Cell6)
        'Row1.Cells.Add(CellP1)
        'Row1.Cells.Add(CellA1)
        'Row1.Cells.Add(CellPP)

        'CellP2.Text = "MT</td><td>JIOS</td><td>Total</td>"
        'CellA2.Text = "MT</td><td>JIOS</td><td>Total</td>"
        'CellP1.ColumnSpan = 3
        'CellA1.ColumnSpan = 3

        'Row1.Cells.Add(Cell3)
        'Row1.Cells.Add(Cell5)

        'Dim Cell21 As New TableCell
        'Dim Cell22 As New TableCell
        'Dim Cell23 As New TableCell
        'Dim Cell24 As New TableCell
        'Dim Cell25 As New TableCell
        'Dim Cell26 As New TableCell
        'Dim cell28 As New TableCell
        'Row2.Cells.Add(Cell21)
        'Row2.Cells.Add(Cell22)
        'Row2.Cells.Add(Cell23)
        'Row2.Cells.Add(Cell26)
        'Row2.Cells.Add(CellP2)
        'Row2.Cells.Add(CellA2)
        'Row2.Cells.Add(cell28)


        'Row1.BackColor = Drawing.Color.DimGray
        'Row1.ForeColor = Drawing.Color.White
        'Row2.BackColor = Drawing.Color.DimGray
        'Row2.ForeColor = Drawing.Color.White
        'Table2.Rows.Add(Row1)
        'Table2.Rows.Add(Row2)

        'Dim DS As New Data.DataSet
        'Dim clsRo As ETS.BL.Routing
        'Try
        '    Session("WorkGroupID") = "3BC5ABE9-8EDC-4CCD-8FD3-C8A001ED8083"
        '    clsRo = New ETS.BL.Routing
        '    DS = clsRo.GetRoutingJobByLevel(Session("ContractorID").ToString, Session("WorkGroupID").ToString, Request.Form(""))

        '    If DS.Tables.Count > 0 Then
        '        If DS.Tables(0).Rows.Count > 0 Then
        '            For Each DRRec As Data.DataRow In DS.Tables(0).Rows
        '                Dim c1 As New TableCell
        '                Dim c2 As New TableCell
        '                Dim c3 As New TableCell
        '                Dim c4 As New TableCell
        '                Dim c5 As New TableCell
        '                Dim c6 As New TableCell
        '                Dim c7 As New TableCell
        '                Dim cellpend As New TableCell
        '                Dim cellPrvPend As New TableCell
        '                Dim cellassgn As New TableCell
        '                Dim r As New TableRow
        '                NotFinished = 0
        '                AccID = DRRec("AccountID").ToString
        '                c1.Text = DRRec("Accountname").ToString
        '                c1.HorizontalAlign = HorizontalAlign.Left
        '                If IsDBNull(DRRec("ProtocolMins")) Then
        '                    ProtocolMins = 0
        '                Else
        '                    ProtocolMins = DRRec("ProtocolMins").ToString
        '                End If
        '                AwaitingEntry = 0
        '                CheckedOut = 0
        '                JIOS_A = 0
        '                DoneMins = 0

        '                c1.Text = DRRec("Accountname").ToString
        '                c1.HorizontalAlign = HorizontalAlign.Left
        '                If IsDBNull(DRRec("ProtocolMins")) Then
        '                    ProtocolMins = 0
        '                Else
        '                    ProtocolMins = DRRec("ProtocolMins").ToString
        '                End If
        '                AwaitingEntry = 0
        '                CheckedOut = 0
        '                JIOS_A = 0
        '                DoneMins = 0

        '                DoneMins = FormatNumber((DRRec("DoneMins").ToString / 60), 0)
        '                PrvPendingMins = FormatNumber((DRRec("PrvPendingMins").ToString / 60), 0)





        '                NotFinished = AwaitingEntry + CheckedOut
        '                c2.Text = ProtocolMins
        '                'Response.Write(AwaitingEntry + CheckedOut)

        '                c3.Text = AwaitingEntry + CheckedOut
        '                c4.Text = DoneMins
        '                c6.Text = PrvPendingMins

        '                r.Cells.Add(c1)
        '                r.Cells.Add(c2)
        '                r.Cells.Add(c5)
        '                r.Cells.Add(c4)

        '                Table2.Rows.Add(r)
        '            Next
        '        End If
        '    End If

        'Catch ex As Exception

        'Finally
        '    DS.Dispose()
        'End Try
    End Sub
End Class
