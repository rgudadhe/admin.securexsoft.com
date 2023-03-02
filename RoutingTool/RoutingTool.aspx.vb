Imports System.Data
Imports System.Data.SqlClient

Partial Class RoutingTool_Default
    Inherits BasePage

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim strConn As String
        strConn = System.Configuration.ConfigurationManager.AppSettings("ETSCon")
        Dim t2 As New Table
        t2.Style("width") = "100%"
        t2.BorderWidth = 2
        t2.GridLines = GridLines.Both
        Dim ProtocolMins As Integer
        Dim DoneMins As Integer
        Dim pDoneMins As Integer
        Dim AwaitingEntry As Integer
        Dim CheckedOut As Integer
        Dim NotFinished As Integer
        Dim PrvPendingMins As Integer
        Dim PendingMins As Integer
        Dim PndDate As Date = ProcStartDate.AddDays(-1)
        Dim strQuery As String

        Dim Cell1 As New TableCell
        Dim Cell2 As New TableCell
        Dim Cell3 As New TableCell
        Dim Cell4 As New TableCell
        Dim Cell5 As New TableCell
        Dim Cell6 As New TableCell
        Dim CellP1 As New TableCell
        Dim CellA1 As New TableCell
        Dim CellP2 As New TableCell
        Dim CellA2 As New TableCell
        Dim CellPP As New TableCell
        Dim AccID As String
        Dim PndMins() As Integer
        Dim PndJobs() As Integer
        Dim AssgnMins() As Integer
        Dim AssgnJobs() As Integer
        Dim TPndMins() As Integer
        Dim TAssgnMins() As Integer
        'Dim TPPMins As Integer
        Dim JIOS_P As Integer
        Dim JIOS_A As Integer
        Dim TJIOS_P As Integer
        Dim TJIOS_A As Integer
        Dim TotPndMins As Integer
        Dim TotAssgnMins As Integer
        Dim TotPPmins As Integer = 0
        Dim i As Integer
        Dim j As Integer
        Dim LevelNo As Long
        i = 0
        JIOS_P = 0
        JIOS_A = 0
        Dim ACount As Integer
        Dim Row1 As New TableRow
        Dim Row2 As New TableRow
        Row1.CssClass = "SMSelected"
        Row1.Style("text-align") = "Center"
        Row1.CssClass = "noScroll"
        Row2.CssClass = "noScroll"
        Dim LvlPName() As String
        Dim LvlAName() As String
        Dim ClmCount As Integer

        Cell1.Text = "Account Name"
        Cell2.Text = "PROT"
        Cell4.Text = "JTBR"
        Cell6.Text = "FINS"
        CellP1.Text = "Pending Mins"
        CellA1.Text = "Assigned Mins"
        CellPP.Text = "Backlog"
        'CellPP.RowSpan = 2
        'Cell1.RowSpan = 2
        'Cell2.RowSpan = 2
        'Cell3.RowSpan = 2
        'Cell4.RowSpan = 2
        'Cell5.RowSpan = 2
        Row1.Cells.Add(Cell1)
        Row1.Cells.Add(Cell2)
        Row1.Cells.Add(Cell4)
        Row1.Cells.Add(Cell6)
        Row1.Cells.Add(CellP1)
        Row1.Cells.Add(CellA1)
        Row1.Cells.Add(CellPP)
        ClmCount = 0

        strQuery = "Select DISTINCT  LevelName, LevelNo from tblproductionlevels where Jobsrouting = 'True' and LevelNo not in(1073741824)  order by LevelNo"

        'Response.Write(strQuery)
        Dim LvlA(0) As String
        Dim Lvl(0) As String
        Dim SelQuery As String
        Dim JoinQuery As String
        Dim arrLevels(0) As Integer
        SelQuery = ""
        JoinQuery = ""
        Dim Incr As Integer
        Incr = 0
        Dim CommPL As New SqlCommand(strQuery, New SqlConnection(strConn))
        Try
            Dim objDAPL As New System.Data.SqlClient.SqlDataAdapter
            objDAPL.SelectCommand = CommPL

            Dim objPL As New System.Data.DataSet()
            objDAPL.Fill(objPL)

            If objPL.tables(0).rows.count > 0 Then
                ReDim arrLevels(objPL.tables(0).rows.count)
                For Each DRPL As datarow In objPL.tables(0).rows
                    LevelNo = DRPL("LevelNo") + 100
                    ClmCount = ClmCount + 1

                    arrLevels(Incr) = CInt(DRPL("LevelNo"))
                    Incr = Incr + 1
                    If ClmCount = 1 Then
                        ReDim LvlPName(ClmCount)
                        ReDim LvlAName(ClmCount)
                        CellP2.Text = DRPL("LevelName") & "</td>"
                        CellA2.Text = DRPL("LevelName") & "</td>"
                    Else
                        ReDim Preserve Lvl(i)
                        ReDim Preserve LvlA(i)
                        CellP2.Text = CellP2.Text & "<td>" & DRPL("LevelName") & "</td>"
                        CellA2.Text = CellA2.Text & "<td>" & DRPL("LevelName") & "</td>"
                        ReDim Preserve LvlPName(ClmCount)
                        ReDim Preserve LvlAName(ClmCount)
                    End If
                    ReDim PndMins(ClmCount)
                    ReDim PndJobs(ClmCount)
                    ReDim AssgnMins(ClmCount)
                    ReDim AssgnJobs(ClmCount)
                    ReDim TPndMins(ClmCount)
                    ReDim TAssgnMins(ClmCount)
                    PndMins(ClmCount) = 0
                    PndJobs(ClmCount) = 0
                    AssgnMins(ClmCount) = 0
                    AssgnJobs(ClmCount) = 0
                    TPndMins(ClmCount) = 0
                    TAssgnMins(ClmCount) = 0

                    LvlPName(ClmCount) = DRPL("LevelNO")
                    LvlAName(ClmCount) = LevelNo
                    Dim Ncell As New TableCell

                    LvlA(i) = LevelNo
                    Lvl(i) = DRPL("LevelNo")
                Next
            End If
            objPL.dispose()
        Finally
            If CommPL.Connection.State = Data.ConnectionState.Open Then
                CommPL.Connection.Close()
            End If
        End Try

        CellP2.Text = CellP2.Text & "<td>JIOS</td><td>Total"
        CellA2.Text = CellA2.Text & "<td>JIOS</td><td>Total"
        CellP1.ColumnSpan = ClmCount + 2
        CellA1.ColumnSpan = ClmCount + 2

        Row1.Cells.Add(Cell3)
        Row1.Cells.Add(Cell5)

        Dim Cell21 As New TableCell
        Dim Cell22 As New TableCell
        Dim Cell23 As New TableCell
        Dim Cell24 As New TableCell
        Dim Cell25 As New TableCell
        Dim Cell26 As New TableCell
        Row2.Cells.Add(Cell21)
        Row2.Cells.Add(Cell22)
        Row2.Cells.Add(Cell23)
        Row2.Cells.Add(Cell26)
        Row2.Cells.Add(CellP2)
        Row2.Cells.Add(CellA2)


        Row1.BackColor = Drawing.Color.DimGray
        Row1.ForeColor = Drawing.Color.White
        Row2.BackColor = Drawing.Color.DimGray
        Row2.ForeColor = Drawing.Color.White
        Table2.Rows.Add(Row1)
        Table2.Rows.Add(Row2)



        Dim SQLCmd As New SqlCommand("SF_RoutingToolActWise", New SqlConnection(strConn))
        Try
            Dim oParam As New Data.SqlClient.SqlParameter("@ContractorID", Data.SqlDbType.UniqueIdentifier)
            oParam.Value = New Guid(Session("ContractorID").ToString)
            SQLCmd.Parameters.Add(oParam)

            oParam = New Data.SqlClient.SqlParameter("@WorkGroupID", Data.SqlDbType.UniqueIdentifier)
            oParam.Value = New Guid(Session("WorkGroupID").ToString)
            SQLCmd.Parameters.Add(oParam)

            SQLCmd.CommandType = Data.CommandType.StoredProcedure

            Dim objDA As New System.Data.SqlClient.SqlDataAdapter
            objDA.SelectCommand = SQLCmd

            Dim objDS As New System.Data.DataSet()
            objDA.Fill(objDS)

            Dim xCount As Integer
            If objDS.Tables(0).Rows.count > 0 Then
                For Each DRRec As datarow In objDS.Tables(0).Rows
                    JIOS_P = 0
                    Dim c1 As New TableCell
                    Dim c2 As New TableCell
                    Dim c3 As New TableCell
                    Dim c4 As New TableCell
                    Dim c5 As New TableCell
                    Dim c6 As New TableCell
                    Dim c7 As New TableCell
                    Dim cellpend As New TableCell
                    Dim cellPrvPend As New TableCell
                    Dim cellassgn As New TableCell
                    Dim r As New TableRow
                    NotFinished = 0
                    AccID = DRRec("AccountID").ToString
                    c1.Text = DRRec("Accountname").ToString
                    c1.HorizontalAlign = HorizontalAlign.Left
                    If IsDBNull(DRRec("ProtocolMins")) Then
                        ProtocolMins = 0
                    Else
                        ProtocolMins = DRRec("ProtocolMins").ToString
                    End If
                    AwaitingEntry = 0
                    CheckedOut = 0
                    JIOS_A = 0
                    DoneMins = 0
                    For xCount = 1 To 6
                        If xCount >= 3 Then
                            ACount = ACount + ACount
                        Else
                            ACount = xCount
                        End If
                        AwaitingEntry = AwaitingEntry + FormatNumber((DRRec("AwaitingEntry" & ACount).ToString / 60), 0)
                        CheckedOut = CheckedOut + FormatNumber((DRRec("CheckedOut" & ACount).ToString / 60), 0)
                        JIOS_A = JIOS_A + DRRec("CheckedOut" & ACount).ToString
                    Next
                    DoneMins = FormatNumber((DRRec("DoneMins").ToString / 60), 0)
                    PrvPendingMins = FormatNumber((DRRec("PrvPendingMins").ToString / 60), 0)


                    For xCount = 1 To Incr
                        ACount = arrLevels(xCount - 1)
                        If IsDBNull(DRRec("AwaitingEntry" & ACount)) Then
                            PndMins(XCount) = 0
                            PndJobs(XCount) = 0
                        Else
                            PndJobs(XCount) = DRRec("CntAE" & ACount).ToString
                            PndMins(XCount) = FormatNumber((DRRec("AwaitingEntry" & ACount) / 60), 0)
                            'NotFinished = NotFinished + FormatNumber((DRRec("AwaitingEntry" & ACount) / 60), 0)
                        End If

                    Next


                    NotFinished = AwaitingEntry + CheckedOut
                    c2.Text = ProtocolMins
                    'Response.Write(AwaitingEntry + CheckedOut)

                    c3.Text = AwaitingEntry + CheckedOut
                    c4.Text = DoneMins
                    c6.Text = PrvPendingMins

                    If ProtocolMins > DoneMins Then
                        PendingMins = ProtocolMins - DoneMins
                        If NotFinished < PendingMins Then
                            PendingMins = NotFinished
                        End If
                    Else
                        PendingMins = 0
                    End If
                    'Response.Write(ProtocolMins & "#" & DoneMins & "#" & PendingMins)
                    c5.Text = PendingMins
                    'If ExcMins > 0 Then
                    '    c7.Text = "<a href=UpExcMins.aspx?AccID=" & DRRec("AccountID").ToString & " Target=_Blank>" & ExcMins & "</a>"
                    'Else
                    '    c7.Text = 0
                    'End If
                    cellpend.Text = AwaitingEntry
                    cellassgn.Text = CheckedOut
                    TotAssgnMins = TotAssgnMins + CheckedOut
                    r.Cells.Add(c1)
                    r.Cells.Add(c2)
                    r.Cells.Add(c5)
                    r.Cells.Add(c4)

                    For xCount = 1 To Incr
                        ACount = arrLevels(xCount - 1)
                        Dim NewCell As New TableCell
                        If PndJobs(XCount) > 0 Then
                            NewCell.Text = "<a href='Empstatus.aspx?AccID=" & DRRec("AccountID").ToString & "&ProLevel=" & LvlPName(XCount) & "' Target=_Blank>" & PndMins(XCount) & "(" & PndJobs(XCount) & ")" & "</a>"
                        Else
                            NewCell.Text = PndMins(XCount)
                        End If
                        TPndMins(XCount) = TPndMins(XCount) + PndMins(XCount)
                        JIOS_P = JIOS_P + PndMins(XCount)

                        PndMins(XCount) = 0
                        r.Cells.Add(NewCell)
                    Next
                    Dim CellP As New TableCell
                    If (AwaitingEntry - JIOS_P) > 0 Then
                        CellP.Text = AwaitingEntry - JIOS_P
                        TJIOS_P = TJIOS_P + (AwaitingEntry - JIOS_P)
                        TotPndMins = TotPndMins + AwaitingEntry
                    Else
                        TotPndMins = TotPndMins + JIOS_P
                        cellpend.Text = JIOS_P
                        CellP.Text = 0
                    End If

                    r.Cells.Add(CellP)
                    r.Cells.Add(cellpend)

                    For xCount = 1 To Incr
                        ACount = arrLevels(xCount - 1)
                        Dim NewCell As New TableCell
                        If Not IsDBNull(DRRec("Checkedout" & ACount)) Then
                            JIOS_A = JIOS_A - DRRec("Checkedout" & ACount).ToString
                            TAssgnMins(XCount) = TAssgnMins(XCount) + CInt(DRRec("Checkedout" & ACount) / 60)
                            AssgnJobs(XCount) = DRRec("CntCH" & ACount).ToString
                            NewCell.Text = "<a href='AccountsRouting.aspx?AccID=" & DRRec("AccountID").ToString & "&ProLevel=" & LvlAName(XCount) & "' Target=_Blank>" & FormatNumber((DRRec("Checkedout" & ACount) / 60), 0) & "(" & AssgnJobs(XCount) & ")" & "</a>"
                        Else
                            NewCell.Text = 0
                        End If
                        r.Cells.Add(NewCell)
                    Next
                    Dim CellJ As New TableCell
                    If JIOS_A > 0 Then
                        CellJ.Text = FormatNumber((JIOS_A / 60), 0)
                        TJIOS_A = TJIOS_A + (JIOS_A / 60)
                    Else
                        CellJ.Text = 0
                    End If
                    r.Cells.Add(CellJ)
                    r.Cells.Add(cellassgn)
                    cellPrvPend.Text = FormatNumber((DRRec("PrvPendingMins").ToString / 60), 0)
                    TotPPmins = TotPPmins + FormatNumber((DRRec("PrvPendingMins").ToString / 60), 0)
                    r.Cells.Add(cellPrvPend)
                    Table2.Rows.Add(r)
                Next
                Dim tc1 As New TableCell
                Dim tc2 As New TableCell
                Dim tc3 As New TableCell
                Dim tc4 As New TableCell
                Dim tc5 As New TableCell
                Dim tc6 As New TableCell
                Dim tc7 As New TableCell
                Dim tcellpend As New TableCell
                Dim tcellassgn As New TableCell
                Dim tcellPPMins As New TableCell

                Dim tr As New TableRow
                tr.BackColor = Drawing.Color.LightGray
                tr.ForeColor = Drawing.Color.Black
                tc1.ColumnSpan = 4
                tc1.Text = "Total"
                tc1.HorizontalAlign = HorizontalAlign.Right
                tr.Cells.Add(tc1)
                For ACount = 1 To Incr
                    Dim NewCell As New TableCell
                    NewCell.Text = "<a href='AccountStatus.aspx?AccID=&ProLevel=" & LvlPName(ACount) & "' Target=_Blank>" & TPndMins(ACount) & "</a>"
                    tr.Cells.Add(NewCell)
                Next
                tc4.Text = TJIOS_P
                tr.Cells.Add(tc4)
                tc2.Text = TotPndMins
                tr.Cells.Add(tc2)
                For ACount = 1 To Incr
                    Dim NewCell As New TableCell
                    NewCell.Text = "<a href='AccountsRouting.aspx?AccID=&ProLevel=" & LvlAName(ACount) & "' Target=_Blank>" & TAssgnMins(ACount) & "</a>"
                    tr.Cells.Add(NewCell)
                Next
                tc5.Text = TJIOS_A
                tr.Cells.Add(tc5)
                tc3.Text = TotAssgnMins
                tr.Cells.Add(tc3)
                tcellPPMins.Text = TotPPmins
                tr.Cells.Add(tcellPPMins)
                Table2.Rows.Add(tr)
            End If
        Finally
            If SQLCmd.Connection.State = Data.ConnectionState.Open Then
                SQLCmd.Connection.Close()
            End If
        End Try

    End Sub
End Class
