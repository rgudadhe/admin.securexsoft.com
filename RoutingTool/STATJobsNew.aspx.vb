Imports System.Data.SqlClient

Partial Class RoutingTool_Default
    Inherits BasePage

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        lblmins.Text = "0"
        lbljobs.Text = "0"
        If Not IsPostBack Then

            DLChoice.Visible = False
            Dim LI As New ListItem
            LI.Text = "Select Option"
            LI.Value = ""
            LI.Selected = True
            Dim LI1 As New ListItem
            LI1.Text = "Change Status"
            LI1.Value = "Status"
            Dim LI2 As New ListItem
            LI2.Text = "Change TAT"
            LI2.Value = "TAT"
            Dim LI3 As New ListItem
            LI3.Text = "Assign User"
            LI3.Value = "Assign"
            DLStatus.Items.Add(LI)
            DLStatus.Items.Add(LI1)
            DLStatus.Items.Add(LI2)
            DLStatus.Items.Add(LI3)
            If ProLevel.Value = "" Then
                ProLevel.Value = Request("ProLevel")
            End If
            If HUserID.Value = "" Then
                HUserID.Value = Request("UserID")
            End If
            If HAccID.Value = "" Then
                HAccID.Value = Request("AccID")
            End If
            'AssignUser()
            DictStatus()
        End If








    End Sub
    Sub DictStatus()
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
        Dim Priority As Boolean
        Dim TAT As String

        DirMins = 0
        Dim k As Integer
        Dim strJobnumber As String
        k = 0

        Dim clsRo As ETS.BL.Routing
        Dim Ds As New Data.DataSet
        Try
            clsRo = New ETS.BL.Routing
            Ds = clsRo.RoutingToolSTATJobsRecords(Session("ContractorID"), Session("WorkgroupID"))
            If Ds.Tables.Count > 0 Then
                If Ds.Tables(0).Rows.Count > 0 Then
                    For Each jobDR As Data.DataRow In Ds.Tables(0).Rows
                        If strJobnumber = jobDR("jobnumber").ToString Then
                        Else
                            strJobnumber = jobDR("jobnumber").ToString

                            'Response.Write(JobDR("Direct"))
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
                            Dim c15 As New TableCell
                            Dim CLevelNo As Integer
                            Dim r As New TableRow

                            If IsDBNull(jobDR("AccountName")) Then
                                ActName = ""
                            Else
                                ActName = jobDR("AccountName").ToString
                            End If

                            If IsDBNull(jobDR("TranscriptionID")) Then
                                TransID = ""
                            Else
                                TransID = jobDR("TranscriptionID").ToString
                            End If



                            If IsDBNull(jobDR("SubmitDate")) Then
                                SubmitDate = ""
                            Else
                                SubmitDate = jobDR("SubmitDate").ToString
                            End If

                            If IsDBNull(jobDR("DueDate")) Then
                                DueDate = Now
                                'r.BackColor = Drawing.Color.White
                                r.ForeColor = Drawing.Color.Black
                            Else
                                DueDate = jobDR("DueDate").ToString
                                If IsDate(DueDate) Then
                                    If DueDate < Now() Then
                                        'r.BackColor = Drawing.Color.Maroon
                                        r.ForeColor = Drawing.Color.Maroon
                                    End If
                                End If
                            End If

                            If IsDBNull(jobDR("Jobnumber")) Then
                                Jobnumber = ""
                            Else
                                Jobnumber = "<a href='jobdetails.aspx?status=" & jobDR("status").ToString() & "&transid=" & jobDR("transcriptionid").ToString() & "'>" & jobDR("Jobnumber").ToString & "</a>"
                            End If

                            If IsDBNull(jobDR("TemplateName")) Then
                                TemplateName = ""
                            Else
                                TemplateName = jobDR("TemplateName").ToString
                            End If

                            If IsDBNull(jobDR("uName")) Then
                                uName = "&nbsp;"
                            Else
                                uName = jobDR("uName").ToString
                            End If

                            If IsDBNull(jobDR("Duration")) Then
                                duration = ""
                            Else
                                duration = jobDR("Duration").ToString
                            End If
                            'Response.Write(JobDR("Mins").ToString)

                            If IsDBNull(jobDR("Mins")) Then
                                Mins = ""
                            Else
                                Mins = jobDR("Mins").ToString
                            End If

                            If jobDR("Priority").ToString = "True" Then
                                'r.BackColor = Drawing.Color.Yellow
                                '                            r.ForeColor = Drawing.Color.BurlyWood
                                Priority = True
                            Else
                                Priority = False
                            End If


                            TAT = jobDR("TAT").ToString
                            CLevelNo = jobDR("Status")

                            If IsDBNull(jobDR("direct")) Then
                                dirStatus = "No"
                            Else
                                dirStatus = jobDR("direct").ToString
                                r.ForeColor = Drawing.Color.Black

                                DirMins = DirMins + Mins

                            End If


                            intExcMins = intExcMins + Mins


                            'If intExcMins < ExcMins Then
                            k = k + 1
                            Dim CB1 As New CheckBox



                            CB1.ID = "TransID" & k
                            CB1.InputAttributes.Add("Value", TransID & "#" & Mins & "#" & CLevelNo)
                            'CB1.Attributes.Add("Checked", "No")
                            CB1.EnableViewState = "False"
                            CB1.InputAttributes.Add("onclick", "highlightRow(this)")
                            'CB1.Checked = "False"

                            c1.Controls.Add(CB1)

                            c2.Text = Jobnumber
                            c3.Text = duration
                            c4.Text = SubmitDate
                            c5.Text = DueDate
                            c6.Text = ActName
                            c7.Text = uName
                            c8.Text = TemplateName
                            c9.Text = jobDR("LevelName").ToString
                            c10.Text = TAT
                            c11.Text = Priority
                            c12.Text = dirStatus
                            c13.Text = IIf(IsDBNull(jobDR("username").ToString), "&nbsp;", jobDR("username").ToString)
                            c14.Text = jobDR("RemTAT").ToString
                            If jobDR("downloaded").ToString.ToLower = "true" Then
                                c15.Text = "Yes"
                            Else
                                c15.Text = "No"
                            End If
                            r.Cells.Add(c1)
                            r.Cells.Add(c2)
                            r.Cells.Add(c9)
                            r.Cells.Add(c13)
                            r.Cells.Add(c10)
                            r.Cells.Add(c15)
                            r.Cells.Add(c11)
                            r.Cells.Add(c3)
                            r.Cells.Add(c4)
                            r.Cells.Add(c5)
                            r.Cells.Add(c14)
                            r.Cells.Add(c6)
                            r.Cells.Add(c7)
                            r.Cells.Add(c12)

                            ' r.Cells.Add(c8)
                            r.Font.Size = "9"
                            r.Font.Italic = False

                            Table4.Rows.Add(r)

                            'Response.Write(CB1.Checked)


                            'Else
                            '    Exit While
                            'End If
                        End If
                    Next
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
            Else
                ' LblTotmins.Text = intExcMins
                LblDMins.Text = DirMins
                lblTotJobs.Text = k
                Table4.Visible = False
                'submit.Visible = False
            End If
        Catch ex As Exception
            Response.Write(ex.Message)
        Finally
            clsRo = Nothing
            Ds.Dispose()
        End Try
    End Sub
    Protected Sub DLStatus_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles DLStatus.SelectedIndexChanged
        If DLStatus.Items(0).Value = "" Then
            DLStatus.Items.RemoveAt(0)
        End If
        DLChoice.Items.Clear()
        If DLStatus.SelectedValue = "Status" Then
            DLUser.Visible = False
            DLChoice.AutoPostBack = False
            DLChoice.Visible = True
            Dim LI As New ListItem
            LI.Text = "Select Status"
            LI.Value = ""
            LI.Selected = True
            DLChoice.Items.Add(LI)
            Dim clsPL As ETS.BL.ProductionLevels
            Dim DSPL As New Data.DataSet
            Dim DVPL As Data.DataView
            Try
                clsPL = New ETS.BL.ProductionLevels
                clsPL.ContractorID = Session("contractorid")
                clsPL.JobsRouting = True
                DSPL = clsPL.getPLevelList

                If DSPL.Tables.Count > 0 Then
                    If DSPL.Tables(0).Rows.Count > 0 Then
                        DVPL = New Data.DataView(DSPL.Tables(0), " LevelNo NOT IN ('1073741824', '2147483647') ", String.Empty, Data.DataViewRowState.CurrentRows)
                        If DVPL.Count > 0 Then
                            For Each DRRec As Data.DataRow In DVPL.ToTable.Rows
                                Dim LItem As New ListItem
                                LItem.Text = "Pending " & DRRec("LevelName").ToString
                                LItem.Value = DRRec("LevelNo").ToString
                                DLChoice.Items.Add(LItem)
                            Next
                        End If
                    End If
                End If
            Catch ex As Exception
            Finally
                clsPL = Nothing
                DSPL.Dispose()
                DVPL.Dispose()
            End Try

        ElseIf DLStatus.SelectedValue = "TAT" Then
            DLUser.Visible = False
            DLChoice.AutoPostBack = False
            DLChoice.Visible = True
            Dim LI As New ListItem
            LI.Text = "Select TAT"
            LI.Value = ""
            LI.Selected = True
            DLChoice.Items.Add(LI)
            Dim LItem1 As New ListItem
            LItem1.Text = "4"
            LItem1.Value = "4"
            Dim LItem2 As New ListItem
            LItem2.Text = "12"
            LItem2.Value = "12"
            Dim LItem3 As New ListItem
            LItem3.Text = "24"
            LItem3.Value = "24"
            Dim LItem4 As New ListItem
            LItem4.Text = "48"
            LItem4.Value = "48"
            Dim LItem5 As New ListItem
            LItem5.Text = "72"
            LItem5.Value = "72"
            Dim LItem6 As New ListItem
            LItem6.Text = "96"
            LItem6.Value = "96"
            Dim LItem7 As New ListItem
            LItem7.Text = "120"
            LItem7.Value = "120"
            DLChoice.Items.Add(LItem1)
            DLChoice.Items.Add(LItem2)
            DLChoice.Items.Add(LItem3)
            DLChoice.Items.Add(LItem4)
            DLChoice.Items.Add(LItem5)
            DLChoice.Items.Add(LItem6)
            DLChoice.Items.Add(LItem7)
        ElseIf DLStatus.SelectedValue = "Assign" Then
            DLUser.Visible = False
            DLChoice.AutoPostBack = True
            DLChoice.Visible = True
            Dim LI As New ListItem
            LI.Text = "Select Level"
            LI.Value = ""
            LI.Selected = True
            DLChoice.Items.Add(LI)

            Dim clsPL As ETS.BL.ProductionLevels
            Dim DSPL As New Data.DataSet
            Dim DVPL As Data.DataView
            Try
                clsPL = New ETS.BL.ProductionLevels
                clsPL.ContractorID = Session("contractorid")
                clsPL.JobsRouting = True
                DSPL = clsPL.getPLevelList

                If DSPL.Tables.Count > 0 Then
                    If DSPL.Tables(0).Rows.Count > 0 Then
                        DVPL = New Data.DataView(DSPL.Tables(0), " LevelNo NOT IN ('1073741824', '2147483647') ", String.Empty, Data.DataViewRowState.CurrentRows)
                        If DVPL.Count > 0 Then
                            For Each DRRec As Data.DataRow In DVPL.ToTable.Rows
                                Dim LItem As New ListItem
                                LItem.Text = "Pending " & DRRec("LevelName").ToString
                                LItem.Value = DRRec("LevelNo").ToString
                                DLChoice.Items.Add(LItem)
                            Next
                        End If
                    End If
                End If
            Catch ex As Exception
            Finally
                clsPL = Nothing
                DSPL.Dispose()
                DVPL.Dispose()
            End Try

            
        End If
        DictStatus()
    End Sub
    Protected Sub Button1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button1.Click
        Dim TransID As String
        Dim I As Integer
        Dim ArrayL() As String
        Dim SelTransID As String
        Dim varCLevelNo As Integer

        Dim DT As New Data.DataTable
        DT.Columns.Add("CurrentStatus", GetType(System.Int32))
        DT.Columns.Add("TranscriptionID", GetType(System.String))

        Try

            Dim varNoJobRequested As Integer = 0

            For I = 1 To TotJobs.Value
                Dim DRTransID As Data.DataRow = DT.NewRow
                TransID = "TransID" & I
                TransID = Request(TransID)
                If Not String.IsNullOrEmpty(TransID) Then
                    ArrayL = Split(TransID, "#")
                    SelTransID = ArrayL(0)
                    varCLevelNo = ArrayL(2)
                    If Not String.IsNullOrEmpty(SelTransID) Then
                        Dim DRow As Data.DataRow = DT.NewRow
                        DRow("TranscriptionID") = SelTransID
                        DRow("CurrentStatus") = varCLevelNo

                        DT.Rows.Add(DRow)
                        varNoJobRequested = varNoJobRequested + 1
                    End If
                End If
            Next

            If DT.Rows.Count > 0 Then
                Dim clsDic As ETS.BL.Dictations
                Dim RetDT As New Data.DataTable
                Dim varOprRecCount As Integer = 0
                Try
                    clsDic = New ETS.BL.Dictations
                    If Trim(UCase(DLStatus.SelectedValue)) = Trim(UCase("Status")) Then
                        RetDT = clsDic.setDictationStatus(DLChoice.SelectedValue, Session("UserID").ToString, Request.UserHostAddress(), DT)
                    ElseIf Trim(UCase(DLStatus.SelectedValue)) = Trim(UCase("TAT")) Then
                        RetDT = clsDic.setDictationTAT(DLChoice.SelectedValue, DT)
                    ElseIf Trim(UCase(DLStatus.SelectedValue)) = Trim(UCase("Assign")) Then
                        RetDT = clsDic.AssignDictations(DLUser.SelectedValue.ToString, DLChoice.SelectedValue, Session("UserID"), False, Request.UserHostAddress(), DT)
                    End If

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
                    RetDT.Dispose()
                End Try
            End If
        Catch ex As Exception
            Response.Write(ex.Message)
        Finally
            DT.Dispose()
        End Try
        DictStatus()
    End Sub
    Protected Sub DLChoice_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles DLChoice.SelectedIndexChanged
        If DLStatus.SelectedValue = "Assign" Then
            If DLChoice.SelectedValue <> "" Then
                DLUser.Items.Clear()
                DLUser.Visible = True
                Dim clsRo As ETS.BL.Routing
                Dim DS As New Data.DataSet
                Try
                    clsRo = New ETS.BL.Routing
                    DS = clsRo.RoutingToolUsrLstByLevel(Session("ContractorID"), DLChoice.SelectedValue)
                    If DS.Tables.Count > 0 Then
                        If DS.Tables(0).Rows.Count > 0 Then
                            DLUser.DataSource = DS.Tables(0)
                            DLUser.DataTextField = "uname"
                            DLUser.DataValueField = "userid"
                            DLUser.DataBind()
                        End If
                    End If
                Catch ex As Exception
                    Response.Write(ex.Message)
                Finally
                    clsRo = Nothing
                End Try
            Else
                DLUser.Visible = False
            End If
        Else
            DLUser.Visible = True
        End If
    End Sub
End Class
