Imports System.Data.SqlClient

Partial Class Excess_Minutes_Default
    Inherits BasePage

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If IsPostBack Then
            submit_Click()
            Excmins(SrvStDate.Value)
        Else
            SrvStDate.Value = Request("SrvStDate")
            HAccID.Value = Request("AccID")
            Excmins(SrvStDate.Value)
        End If

    End Sub
    Private Sub Excmins(ByVal SrvStDate As Date)
        Session("WorkgroupID") = "3BC5ABE9-8EDC-4CCD-8FD3-C8A001ED8083"
        Try
            Dim ProtocolMins As Integer
            Dim FreshMins As Integer
            Dim DoneMins As Integer
            Dim NotFinishedMins As Integer
            Dim PendingMins As Integer
            Dim ExcMins As Integer
            Dim intExcMins As Integer
            Dim intExcSecs As Integer
            intExcMins = 0
            intExcSecs = 0
            Dim SrvEnddate As Date
            SrvEnddate = SrvStDate.AddDays(1)
            SrvEnddate = SrvStDate.AddDays(1)


            Dim clsRo As ETS.BL.Routing
            Dim Ds As New Data.DataSet
            Dim DV As New Data.DataView
            Try
                clsRo = New ETS.BL.Routing
                Ds = clsRo.RoutingToolExcessMinutesReport(Session("ContractorID"), Session("WorkgroupID"), SrvStDate, SrvEnddate)

                If Ds.Tables.Count > 0 Then
                    If Ds.Tables(0).Rows.Count > 0 Then
                        DV = New Data.DataView(Ds.Tables(0), " AccountID='" & HAccID.Value & "' ", String.Empty, Data.DataViewRowState.CurrentRows)
                        If DV.Count > 0 Then
                            For Each DRRec As Data.DataRow In DV.ToTable.Rows
                                Dim c1 As New TableCell
                                Dim c2 As New TableCell
                                Dim c3 As New TableCell
                                Dim c4 As New TableCell
                                Dim c5 As New TableCell
                                Dim c6 As New TableCell
                                Dim c7 As New TableCell
                                Dim r As New TableRow

                                c1.Text = DRRec("Accountname").ToString
                                If IsDBNull(DRRec("ProtocolMins")) Then
                                    ProtocolMins = 0
                                Else
                                    ProtocolMins = DRRec("ProtocolMins").ToString
                                End If

                                If IsDBNull(DRRec("FreshMins")) Then
                                    FreshMins = 0
                                Else

                                    FreshMins = FormatNumber(DRRec("FreshMins").ToString / 60, 0)
                                End If

                                If IsDBNull(DRRec("NotFinished")) Then
                                    NotFinishedMins = 0
                                Else
                                    NotFinishedMins = FormatNumber(DRRec("NotFinished").ToString / 60, 0)
                                End If

                                If IsDBNull(DRRec("DoneMins")) Then
                                    DoneMins = 0
                                Else
                                    DoneMins = FormatNumber(DRRec("DoneMins").ToString / 60, 0)
                                End If


                                If IsDBNull(DRRec("PendingMins")) Then
                                    PendingMins = 0
                                Else
                                    PendingMins = FormatNumber(DRRec("PendingMins").ToString / 60, 0)
                                End If


                                NotFinishedMins = FreshMins - DoneMins
                                c2.Text = ProtocolMins
                                c3.Text = FreshMins
                                c4.Text = DoneMins
                                c5.Text = NotFinishedMins
                                c6.Text = PendingMins
                                If ProtocolMins > DoneMins Then
                                    ExcMins = FreshMins - ProtocolMins
                                Else
                                    ExcMins = FreshMins - DoneMins
                                End If

                                If ExcMins > 0 And ProtocolMins > 0 Then
                                    c7.Text = "<a href='UpExcMinsNew.aspx?AccID=" & DRRec("AccountID").ToString & "&SrvStDate=" & SrvStDate & "' Target=_Blank>" & ExcMins & "</a>"
                                Else
                                    c7.Text = 0
                                End If

                                r.Cells.Add(c1)
                                r.Cells.Add(c2)
                                r.Cells.Add(c3)
                                r.Cells.Add(c4)
                                r.Cells.Add(c5)
                                r.Cells.Add(c6)

                                r.Cells.Add(c7)
                                If FreshMins > 0 Then
                                    Table2.Rows.Add(r)
                                End If
                            Next
                        End If
                    End If
                End If
            Catch ex As Exception
                Response.Write(ex.Message)
            Finally
                clsRo = Nothing
            End Try




            TotJobs.Value = 0
            Dim ActName As String
            Dim TransID As String
            Dim SubmitDate As String
            Dim DueDate As String
            Dim Jobnumber As String
            Dim TemplateName As String
            Dim uName As String
            Dim duration As String
            Dim k As Integer
            k = 0
            If ExcMins > 0 Then

                Dim clsRo1 As ETS.BL.Routing
                Dim DsRec As New Data.DataSet
                Try
                    clsRo1 = New ETS.BL.Routing
                    DsRec = clsRo1.RoutingToolExcessMinutesRecords(HAccID.Value.ToString, SrvStDate, SrvEnddate, Session("ContractorID").ToString)
                    If DsRec.Tables.Count > 0 Then
                        If DsRec.Tables(0).Rows.Count > 0 Then
                            For Each ExcDataReader As Data.DataRow In DsRec.Tables(0).Rows
                                If IsDBNull(ExcDataReader("AccountName")) Then
                                    ActName = ""
                                Else
                                    ActName = ExcDataReader("AccountName").ToString
                                End If

                                If IsDBNull(ExcDataReader("TranscriptionID")) Then
                                    TransID = ""
                                Else
                                    TransID = ExcDataReader("TranscriptionID").ToString
                                End If

                                If IsDBNull(ExcDataReader("SubmitDate")) Then
                                    SubmitDate = ""
                                Else
                                    SubmitDate = ExcDataReader("SubmitDate").ToString
                                End If

                                If IsDBNull(ExcDataReader("DueDate")) Then
                                    DueDate = ""
                                Else
                                    DueDate = ExcDataReader("DueDate").ToString
                                End If

                                If IsDBNull(ExcDataReader("Jobnumber")) Then
                                    Jobnumber = ""
                                Else
                                    Jobnumber = ExcDataReader("Jobnumber").ToString
                                End If

                                If IsDBNull(ExcDataReader("TemplateName")) Then
                                    TemplateName = ""
                                Else
                                    TemplateName = ExcDataReader("TemplateName").ToString
                                End If

                                If IsDBNull(ExcDataReader("uName")) Then
                                    uName = ""
                                Else
                                    uName = ExcDataReader("uName").ToString
                                End If

                                If IsDBNull(ExcDataReader("Duration")) Then
                                    duration = ""
                                Else
                                    duration = ExcDataReader("Duration").ToString
                                End If

                                'If IsDBNull(ExcDataReader("Mins")) Then
                                '    Mins = ""
                                'Else
                                '    Mins = ExcDataReader("Mins").ToString
                                'End If
                                intExcSecs = intExcSecs + ExcDataReader("Secs").ToString
                                intExcMins = (intExcSecs / 60)
                                If intExcMins < ExcMins Then
                                    k = k + 1
                                    Dim CB1 As New CheckBox
                                    lblTotMins.Text = intExcMins
                                    lblTotJobs.Text = k

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

                                    Dim r As New TableRow
                                    CB1.ID = "TransID" & k
                                    CB1.Checked = False
                                    'Response.Write(CB1.Checked)


                                    CB1.InputAttributes.Add("Value", TransID)
                                    CB1.InputAttributes.Add("onclick", "highlightRow(this)")
                                    TotJobs.Value = k
                                    c1.Controls.Add(CB1)
                                    c2.Text = Jobnumber
                                    c3.Text = duration
                                    c4.Text = SubmitDate
                                    c5.Text = DueDate
                                    c6.Text = ActName
                                    c7.Text = uName
                                    c8.Text = TemplateName
                                    'If InStr(ExcDataReader("lname").ToString, "CheckedOut") > 0 Then
                                    c9.Text = ExcDataReader("lname").ToString
                                    'Else
                                    '    c9.Text = "Pending " & ExcDataReader("lname").ToString
                                    'End If

                                    c10.Text = ExcDataReader("TAT").ToString
                                    c11.Text = ExcDataReader("priority").ToString

                                    r.Cells.Add(c1)
                                    r.Cells.Add(c2)
                                    r.Cells.Add(c3)
                                    r.Cells.Add(c9)
                                    r.Cells.Add(c8)
                                    r.Cells.Add(c4)
                                    r.Cells.Add(c10)
                                    r.Cells.Add(c5)
                                    r.Cells.Add(c11)
                                    r.Cells.Add(c6)
                                    r.Cells.Add(c7)
                                    r.Cells.Add(c8)
                                    Table3.Rows.Add(r)
                                Else
                                    Exit For
                                End If
                            Next
                        End If
                    End If
                Catch ex As Exception
                    Response.Write(ex.Message)
                Finally
                    clsRo1 = Nothing
                    DsRec.Dispose()
                End Try
            Else
                lblTotMins.Text = intExcMins
                lblTotJobs.Text = k
                Table3.Visible = False
                submit.Visible = False
            End If
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub
    Sub submit_Click()
        Dim TransID As String
        Dim I As Integer

        Dim DT As New Data.DataTable
        DT.Columns.Add("TransID", GetType(System.String))

        Dim varNoJobRequested As Integer = 0
        For I = 1 To TotJobs.Value
            TransID = "TransID" & I
            If Request(TransID) <> "" Then
                Dim DRow As Data.DataRow = DT.NewRow
                DRow("TranscriptionID") = Request(TransID)

                DT.Rows.Add(DRow)
                varNoJobRequested = varNoJobRequested + 1
            End If
        Next

        If DT.Rows.Count > 0 Then
            Dim clsRo As ETS.BL.Routing
            Try
                clsRo = New ETS.BL.Routing
                lblStatusMsg.Text = clsRo.UpdateJobsTATAndDateFromExcessMins(DT, varNoJobRequested)
            Catch ex As Exception
                Response.Write(ex.Message)
            Finally
                clsRo = Nothing
            End Try
        End If
    End Sub
End Class
