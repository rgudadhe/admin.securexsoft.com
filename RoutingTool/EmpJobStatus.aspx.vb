Imports System.Data.SqlClient
Imports System.Data

Partial Class RoutingTool_Default
    Inherits BasePage
    Dim hourdiff As Integer
    Public int1 As Integer
    Public int2 As Integer
    Dim int3 As Integer
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            ShowRecords()
        End If
        hourdiff = DateDiff(DateInterval.Hour, ServStartDate, Now)
    End Sub
    Private Sub ShowRecords()
        lblmins.Text = "0"
        lbljobs.Text = "0"
        ProLevel.Value = Request("ProLevel")
        HAccID.Value = Request("AccID")
        HUserID.Value = Request("UserID")
        Dim ProtocolMins As Integer
        Dim DoneMins As Integer
        Dim AwaitingEntry As Integer
        Dim CheckedOut As Integer
        Dim NotFinished As Integer

        Dim PendingMins As Integer

        

        Dim Cell1 As New TableCell
        Dim Cell2 As New TableCell
        Dim Cell3 As New TableCell
        Dim Cell4 As New TableCell
        Dim Cell5 As New TableCell
        Dim Row1 As New TableRow
        Dim ClmCount As Integer

        Cell1.Text = "Account Name"
        Cell2.Text = "Protocol Mins"
        Cell4.Text = "Pending Mins"

        Row1.Cells.Add(Cell1)
        Row1.Cells.Add(Cell2)
        Row1.Cells.Add(Cell4)
        ClmCount = 0

        Dim clsRo As ETS.BL.Routing
        Dim Ds As New DataSet
        Try
            clsRo = New ETS.BL.Routing
            Ds = clsRo.RoutingToolEmpStatusAccInfoByLevel(Request("ProLeveL"), Request("AccID"), Session("contractorid"))
            If Ds.Tables.Count > 0 Then
                If Ds.Tables(0).Rows.Count > 0 Then
                    For Each DrRec As DataRow In Ds.Tables(0).Rows
                        NotFinished = 0
                        ' Dim Actname As New String
                        LblActName.Text = DrRec("AccountName").ToString

                        If IsDBNull(DrRec("ProtocolMins")) Then
                            ProtocolMins = 0
                        Else
                            ProtocolMins = DrRec("ProtocolMins").ToString
                        End If

                        If IsDBNull(DrRec("AwaitingEntry")) Then
                            AwaitingEntry = 0
                        Else
                            AwaitingEntry = FormatNumber((DrRec("AwaitingEntry").ToString / 60), 0)
                        End If


                        If IsDBNull(DrRec("CheckedOut")) Then
                            CheckedOut = 0
                        Else
                            CheckedOut = FormatNumber((DrRec("CheckedOut").ToString / 60), 0)
                        End If

                        If IsDBNull(DrRec("DoneMins")) Then
                            DoneMins = 0
                        Else
                            DoneMins = FormatNumber((DrRec("DoneMins").ToString / 60), 0)
                        End If
                        Dim NotRouted As Integer

                        If IsDBNull(DrRec("NotRouted")) Then
                            NotRouted = 0
                        Else
                            NotRouted = FormatNumber((DrRec("NotRouted").ToString / 60), 0)
                        End If

                        NotFinished = AwaitingEntry + CheckedOut

                        If ProtocolMins > DoneMins Then

                            PendingMins = ProtocolMins - DoneMins
                            If NotFinished < PendingMins Then
                                PendingMins = NotFinished
                            End If
                        Else
                            PendingMins = 0
                        End If
                        LblTotmins.Text = NotRouted
                        LblPendMins.Text = PendingMins
                        Lblstatus.Text = DrRec("Levelname").ToString

                    Next
                End If
            End If

        Catch ex As Exception
            Response.Write(ex.Message)
        Finally
            Ds.Dispose()
            clsRo = Nothing
        End Try

        AssignUser()
        'ActStatus()
    End Sub
    Sub ActStatus()
        ShowRecords()
        Dim intExcMins As Integer
        intExcMins = 0

        TotJobs.Value = 0
        Dim ActName As String
        Dim TransID As String
        Dim SubmitDate As String
        Dim DueDate As Date
        Dim Jobnumber As String
        Dim TemplateName As String
        Dim uName As String
        Dim duration As String
        Dim Mins As Integer
        Dim dirStatus As String
        Dim DirMins As Integer
        Dim LevelNo As Integer
        Dim LvlAssn As String
        Dim Priority As Boolean
        Dim TAT As String
        Dim i As Integer
        Dim Lvl(0) As String
        Dim LvlN(0) As String
        Dim SelQuery As String
        Dim JoinQuery As String
        SelQuery = ""
        JoinQuery = ""
        i = 0
        LvlAssn = ""
        Dim X As Integer
        X = 0

        Dim clsPL As ETS.BL.ProductionLevels
        Dim DS As New DataSet
        Dim DV As New DataView
        Dim DRRec As DataTableReader
        Try
            clsPL = New ETS.BL.ProductionLevels
            clsPL.ContractorID = Session("contractorid")
            clsPL.JobsRouting = True
            DS = clsPL.getPLevelList()
            If DS.Tables.Count > 0 Then
                If DS.Tables(0).Rows.Count > 0 Then
                    DV = New DataView(DS.Tables(0), "LevelNo < " & ProLevel.Value & " AND LevelNo NOT IN ('1073741824', '2147483647')", "", DataViewRowState.CurrentRows)
                    If DV.ToTable().Rows.Count > 0 Then
                        DRRec = DV.ToTable().CreateDataReader

                        If DRRec.HasRows Then
                            int1 = 10
                            int2 = 11

                            While (DRRec.Read)
                                TableCell11.Visible = True
                                'Response.Write(DRRec("LevelNo"))
                                LevelNo = DRRec("LevelNo") + 100
                                If i = 0 Then
                                    LDone.Text = "" & DRRec("LevelName").ToString & "ID</td>"
                                    LvlAssn = LevelNo
                                Else
                                    ReDim Preserve Lvl(i)
                                    ReDim Preserve LvlN(i)
                                    LDone.Text = LDone.Text & "<td>" & DRRec("LevelName").ToString & "ID</td>"
                                    LvlAssn = LvlAssn & "," & LevelNo
                                End If
                                SelQuery = SelQuery & " UA" & i & ".username,"
                                JoinQuery = JoinQuery & " LEFT JOIN (Select U.username, transcriptionID from tbltranscriptionstatus T, tblUsers U where T.userID = U.UserID and T.USerLevel = " & DRRec("LevelNo") & ") AS UA" & i & " ON " & "UA" & i & ".transcriptionID=T.transcriptionID"
                                Lvl(i) = DRRec("LevelNo")
                                LvlN(i) = DRRec("LevelName")
                                i = i + 1
                            End While
                        Else
                            int1 = 9
                            int2 = 10
                            TableCell11.Visible = False
                        End If
                        DRRec.Close()
                    End If
                End If
            End If
        Catch ex As Exception
            Response.Write(ex.Message)
        Finally
            DS.Dispose()
            clsPL = Nothing
            DV.Dispose()
            DRRec = Nothing
        End Try

        If String.IsNullOrEmpty(LDone.Text) Then
            LDone.Text = "&nbsp;"
            TableCell11.Visible = False
        End If
        DirMins = 0
        Dim k As Integer
        k = 0


        Dim clsRo As ETS.BL.Routing
        Dim DSJobs As New DataSet
        Dim JobDR As DataTableReader
        Try
            clsRo = New ETS.BL.Routing
            DSJobs = clsRo.RoutingToolEmpJobStatusRecordsByUsrID(SelQuery, JoinQuery, HAccID.Value, ProLevel.Value, HUserID.Value, Session("ContractorID").ToString)
            If DSJobs.Tables.Count > 0 Then
                If DSJobs.Tables(0).Rows.Count > 0 Then
                    JobDR = DSJobs.Tables(0).CreateDataReader

                    If JobDR.HasRows Then
                        While (JobDR.Read)
                            Mins = 0
                            Dim c1 As New TableCell
                            Dim c2 As New TableCell
                            Dim c3 As New TableCell
                            Dim c4 As New TableCell
                            Dim c5 As New TableCell
                            Dim c6 As New TableCell
                            Dim c7 As New TableCell
                            Dim c8 As New TableCell
                            Dim c9 As New TableCell
                            Dim c10 As New TableCell
                            Dim c11 As New TableCell
                            Dim c12 As New TableCell
                            Dim c13 As New TableCell
                            Dim c14 As New TableCell
                            Dim r As New TableRow

                            If IsDBNull(JobDR("AccountName")) Then
                                ActName = ""
                            Else
                                ActName = JobDR("AccountName").ToString
                            End If

                            If IsDBNull(JobDR("TranscriptionID")) Then
                                TransID = ""
                            Else
                                TransID = JobDR("TranscriptionID").ToString
                            End If



                            If IsDBNull(JobDR("SubmitDate")) Then
                                SubmitDate = ""
                            Else
                                SubmitDate = JobDR("SubmitDate").ToString
                            End If

                            If IsDBNull(JobDR("DueDate")) Then
                                DueDate = Now
                                '  r.BackColor = Drawing.Color.White
                                r.ForeColor = Drawing.Color.Black
                            Else
                                DueDate = JobDR("DueDate").ToString
                                If IsDate(DueDate) Then
                                    If DueDate < Now() Then
                                        'r.BackColor = Drawing.Color.Maroon
                                        r.ForeColor = Drawing.Color.Maroon
                                    End If
                                End If
                            End If

                            If IsDBNull(JobDR("Jobnumber")) Then
                                Jobnumber = ""
                            Else
                                Jobnumber = JobDR("Jobnumber").ToString
                            End If

                            If IsDBNull(JobDR("TemplateName")) Then
                                TemplateName = ""
                            Else
                                TemplateName = JobDR("TemplateName").ToString
                            End If

                            If IsDBNull(JobDR("uName")) Then
                                uName = ""
                            Else
                                uName = JobDR("uName").ToString
                            End If

                            If IsDBNull(JobDR("Duration")) Then
                                duration = ""
                            Else
                                duration = JobDR("Duration").ToString
                            End If
                            'Response.Write(JobDR("Mins").ToString)

                            If IsDBNull(JobDR("Mins")) Then
                                Mins = ""
                            Else
                                Mins = JobDR("Mins").ToString
                            End If

                            If JobDR("Priority").ToString = "True" Then
                                '      r.BackColor = Drawing.Color.Yellow
                                r.ForeColor = Drawing.Color.BurlyWood
                                Priority = True
                            Else
                                Priority = False
                            End If


                            TAT = JobDR("TAT").ToString


                            If IsDBNull(JobDR("direct")) Then
                                dirStatus = "No"
                            Else
                                dirStatus = JobDR("direct").ToString
                                r.ForeColor = Drawing.Color.Black

                                DirMins = DirMins + Mins

                            End If


                            intExcMins = intExcMins + Mins


                            'If intExcMins < ExcMins Then
                            k = k + 1
                            Dim CB1 As New CheckBox



                            CB1.ID = "TransID" & k
                            CB1.InputAttributes.Add("Value", TransID & "#" & Mins)
                            'CB1.Attributes.Add("Checked", "No")
                            CB1.EnableViewState = "False"
                            CB1.InputAttributes.Add("onclick", "highlightRow(this)")
                            'CB1.Checked = "False"

                            c1.Controls.Add(CB1)
                            c13.Text = DateDiff(DateInterval.Hour, Now, DueDate)
                            c2.Text = Jobnumber
                            c3.Text = duration
                            c4.Text = SubmitDate
                            c5.Text = DueDate
                            c6.Text = ActName
                            c7.Text = uName
                            c8.Text = TemplateName

                            If Not IsDBNull(JobDR("LevelName")) Then
                                c9.Text = "Pending " & JobDR("LevelName").ToString
                            End If

                            c10.Text = TAT
                            c11.Text = Priority
                            c12.Text = dirStatus
                            c14.Text = JobDR("category").ToString
                            r.Cells.Add(c1)
                            r.Cells.Add(c2)
                            If Not String.IsNullOrEmpty(LDone.Text) Then
                                r.Cells.Add(c9)
                            End If

                            For X = 1 To i
                                Dim CellX As New TableCell
                                If String.IsNullOrEmpty(JobDR(X - 1).ToString) Then
                                    CellX.Text = "&nbsp"
                                Else
                                    CellX.Text = JobDR(X - 1).ToString
                                End If
                                r.Cells.Add(CellX)
                            Next
                            r.Cells.Add(c10)
                            r.Cells.Add(c11)
                            r.Cells.Add(c3)
                            r.Cells.Add(c4)
                            r.Cells.Add(c5)
                            r.Cells.Add(c13)
                            r.Cells.Add(c6)
                            r.Cells.Add(c7)
                            r.Cells.Add(c14)
                            r.Cells.Add(c12)

                            ' r.Cells.Add(c8)
                            r.Font.Size = "9"
                            r.Font.Italic = False

                            Table4.Rows.Add(r)

                            'Response.Write(CB1.Checked)


                            'Else
                            '    Exit While
                            'End If

                        End While
                        'Response.Write(intExcMins)

                        LblTMins.Text = FormatNumber((intExcMins / 60), 0)
                        lblTotJobs.Text = k
                        LblDMins.Text = FormatNumber((DirMins / 60), 0)
                        TotJobs.Value = k
                    Else

                        ' LblTotmins.Text = intExcMins
                        LblDMins.Text = DirMins
                        lblTotJobs.Text = k
                        Table4.Visible = False
                        'submit.Visible = False


                    End If
                    JobDR.Close()
                End If
            End If
        Catch ex As Exception
            Response.Write(ex.Message)
        Finally
            clsRo = Nothing
            DSJobs.Dispose()
            JobDR = Nothing
        End Try
    End Sub
    Sub AssignUser()
        hourdiff = DateDiff(DateInterval.Hour, ServStartDate, Now)
        Dim varLevelName As String = String.Empty
        Dim clsPL As ETS.BL.ProductionLevels
        Try
            clsPL = New ETS.BL.ProductionLevels
            clsPL.ContractorID = Session("ContractorID").ToString
            clsPL.LevelNo = ProLevel.Value
            clsPL.getPLevelDetails()
            If Not String.IsNullOrEmpty(clsPL.LevelName) Then
                varLevelName = clsPL.LevelName
            End If
        Catch ex As Exception
            Response.Write(ex.Message)
        Finally
            clsPL = Nothing
        End Try

        Dim scdMins As Integer
        Dim MinsDone As Integer
        Dim MinsAssn As Integer
        Dim MinsPend As Integer
        Dim DirMins As Integer
        Dim Userid As String = String.Empty

        CellMdone.Text = "Finished[" & varLevelName.ToString & "]"
        CellCout.Text = "Assigned[" & varLevelName.ToString & "]"

        Dim clsRo As ETS.BL.Routing
        Dim DS As New DataSet
        Try
            clsRo = New ETS.BL.Routing
            DS = clsRo.RoutingToolEmpStatusByLevelByUsrID(ProLevel.Value, HAccID.Value, ProcStartDate, hourdiff, ServStartDate, ServEndDate, Session("ContractorID"), HUserID.Value)

            If DS.Tables.Count > 0 Then
                If DS.Tables(0).Rows.Count > 0 Then
                    For Each DRRec1 As Data.DataRow In DS.Tables(0).Rows
                        Dim CL1 As New TableCell
                        Dim CL2 As New TableCell
                        Dim CL3 As New TableCell
                        Dim CL4 As New TableCell
                        Dim CL5 As New TableCell
                        Dim RW1 As New TableRow
                        Dim CL6 As New TableCell
                        Dim CL7 As New TableCell
                        Userid = DRRec1("UserID").ToString

                        RW1.Style("overflow") = "auto"
                        CL1.Text = DRRec1("uname")
                        CL1.HorizontalAlign = HorizontalAlign.Left
                        CL2.Text = DRRec1("username")
                        If IsDBNull(DRRec1("SchMins")) Then
                            scdMins = 0
                            CL3.Text = 0
                        Else
                            scdMins = DRRec1("SchMins").ToString
                            CL3.Text = DRRec1("SchMins").ToString
                        End If



                        If IsDBNull(DRRec1("DNMins")) Then
                            MinsDone = 0
                            CL4.Text = 0
                        Else
                            MinsDone = FormatNumber((DRRec1("DNMins").ToString / 60), 0)
                            CL4.Text = FormatNumber((DRRec1("DNMins").ToString / 60), 0)
                        End If


                        If IsDBNull(DRRec1("CHMins")) Then
                            MinsAssn = 0
                            CL5.Text = 0
                        Else
                            Dim varLevelNo As Integer = CInt(ProLevel.Value) + 100
                            MinsAssn = FormatNumber((DRRec1("CHMins").ToString / 60), 0)
                            CL5.Text = "<a href='JobsRouting.aspx?Userid=" & DRRec1("Userid").ToString & "&ProLevel=" & varLevelNo & " '  Target=_Blank>" & FormatNumber((DRRec1("CHMins").ToString / 60), 0) & "</a>"
                            'Response.Write("Test" & varLevelNo)
                        End If



                        If scdMins > 0 Then
                            MinsPend = scdMins - (MinsDone + MinsAssn)
                            If MinsPend < 0 Then
                                MinsPend = 0
                            End If
                        Else
                            MinsPend = 0
                        End If



                        If IsDBNull(DRRec1("DirMins")) Then
                            DirMins = 0
                            CL7.Text = 0
                        Else
                            DirMins = FormatNumber((DRRec1("DirMins").ToString / 60), 0)
                            CL7.Text = FormatNumber((DRRec1("DirMins").ToString / 60), 0)
                        End If

                        CL7.Text = DirMins
                        CL6.Text = MinsPend
                        Dim STime As New TableCell
                        Dim ETime As New TableCell
                        STime.Text = DRRec1("starttime").ToString
                        ETime.Text = DRRec1("endtime").ToString
                        RW1.Cells.Add(CL1)
                        RW1.Cells.Add(CL2)
                        CL3.BorderColor = Drawing.Color.DimGray
                        RW1.Cells.Add(CL3)
                        RW1.Cells.Add(STime)
                        RW1.Cells.Add(ETime)

                        Dim CellD As New TableCell

                        If IsDBNull(DRRec1("DNMins1")) Then
                            CellD.Text = 0
                        Else
                            CellD.Text = FormatNumber((DRRec1("DNMins1").ToString / 60), 0)
                        End If


                        RW1.Cells.Add(CellD)
                        CL4.BorderColor = Drawing.Color.DimGray
                        'RW1.Cells.Add(CL4)

                        Dim CellC As New TableCell
                        If IsDBNull(DRRec1("CHMins1")) Then
                            CellC.Text = 0
                        Else
                            CellC.Text = "<a href='JobsRouting.aspx?Userid=" & DRRec1("Userid").ToString & "&ProLevel=" & CInt(ProLevel.Value) + 100 & " '  Target=_Blank>" & FormatNumber((DRRec1("CHMins1").ToString / 60), 0) & "</a>"
                        End If
                        RW1.Cells.Add(CellC)

                        CL5.BorderColor = Drawing.Color.DimGray
                        CL6.BorderColor = Drawing.Color.DimGray
                        'CL7.BorderColor = Drawing.Color.DimGray
                        'RW1.Cells.Add(CL5)
                        RW1.Cells.Add(CL6)
                        RW1.Cells.Add(CL7)


                        Table2.Rows.Add(RW1)
                    Next
                End If
            End If
        Catch ex As Exception
            Response.Write(ex.Message)
        Finally
            clsRo = Nothing
        End Try
    End Sub
    Protected Function chkLevel(ByVal AdminLevel As Long, ByVal Level As Long) As Boolean
        If (AdminLevel And Level) = Level Then
            chkLevel = True
        Else
            chkLevel = False
        End If
    End Function
    Protected Sub submit_Click1(ByVal sender As Object, ByVal e As System.EventArgs) Handles submit.Click
        Dim TransID As String
        Dim I As Integer
        Dim realStatus As Integer
        Dim ArrayL() As String
        Dim SelTransID As String
        realStatus = 100 + CInt(ProLevel.Value)
        'Dim DT As New DataTable
        'DT.Columns.Add(New DataColumn("TransID"))

        Dim DT As New DataTable
        DT.Columns.Add("CurrentStatus", GetType(System.Int32))
        DT.Columns.Add("TranscriptionID", GetType(System.String))

        Dim varNoJobRequested As Integer = 0
        For I = 1 To TotJobs.Value
            Dim DRTransID As DataRow = DT.NewRow
            TransID = "TransID" & I
            TransID = Request(TransID)
            ArrayL = Split(TransID, "#")

            SelTransID = ArrayL(0)

            If Not String.IsNullOrEmpty(SelTransID) Then
                'DRTransID("TransID") = SelTransID
                'DT.Rows.Add(DRTransID)

                Dim DRow As DataRow = DT.NewRow
                DRow("TranscriptionID") = SelTransID
                DRow("CurrentStatus") = CInt(ProLevel.Value)

                DT.Rows.Add(DRow)
                varNoJobRequested = varNoJobRequested + 1
            End If
        Next

        If DT.Rows.Count > 0 Then
            'Dim clsRo As ETS.BL.Routing
            'Try
            '    clsRo = New ETS.BL.Routing
            '    lblStatusMsg.Text = clsRo.AssignedJobstoUsrFromEmpJobStatus(DT, ProLevel.Value, HUserID.Value, Request.UserHostAddress(), Session("UserID"), varNoJobRequested)
            'Catch ex As Exception
            '    Response.Write(ex.Message)
            'Finally
            '    clsRo = Nothing
            'End Try
            Dim clsDic As ETS.BL.Dictations
            Dim RetDT As New DataTable
            Dim varOprRecCount As Integer = 0
            Try
                clsDic = New ETS.BL.Dictations
                RetDT = clsDic.AssignDictations(HUserID.Value.ToString, realStatus, Session("UserID"), False, Request.UserHostAddress(), DT)
                For Each RetRw As Data.DataRow In RetDT.Rows
                    If RetRw("Result") = True Then
                        varOprRecCount = varOprRecCount + 1
                    End If
                Next
                lblStatusMsg.Text = varOprRecCount & " records updated out of " & varNoJobRequested
            Catch ex As Exception
                Response.Write(ex.Message)
            Finally
                clsDic = Nothing
            End Try
        End If
        ShowRecords()
        'ActStatus()
    End Sub
    Protected Sub showRec_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles showRec.Click
        ActStatus()
    End Sub
End Class
