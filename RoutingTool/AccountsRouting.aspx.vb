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
            DLStatus.Items.Add(LI)
            DLStatus.Items.Add(LI1)
            DLStatus.Items.Add(LI2)
            If ProLevel.Value = "" Then
                ProLevel.Value = Request("ProLevel")
                Response.Write(ProLevel.Value)
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
        Dim strConn As String
        Dim t2 As New Table
        t2.Style("width") = "100%"
        t2.BorderWidth = 2
        t2.GridLines = GridLines.Both
        Dim intExcMins As Integer
        intExcMins = 0
        strConn = System.Configuration.ConfigurationManager.AppSettings("ETSCon")
        Dim strQuery As String
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
        Dim Lvlchk As String = ProLevel.Value
        Dim LvlFound As Boolean = False
        i = 0
        LvlAssn = ""

        Dim clsPL As ETS.BL.ProductionLevels
        Dim DS As New Data.DataSet
        Dim DV As Data.DataView
        Try
            clsPL = New ETS.BL.ProductionLevels
            clsPL.ContractorID = Session("ContractorID").ToString
            DS = clsPL.getPLevelList
            If DS.Tables.Count > 0 Then
                If DS.Tables(0).Rows.Count > 0 Then
                    DV = New Data.DataView(DS.Tables(0), " LevelNo NOT IN ('1073741824', '2147483647') ", String.Empty, Data.DataViewRowState.CurrentRows)
                    If DV.Count > 0 Then
                        For Each DRRec As Data.DataRow In DV.ToTable.Rows
                            LevelNo = DRRec("LevelNo").ToString + 100
                            If LevelNo = ProLevel.Value Then
                                LvlFound = True
                                Lvlchk = DRRec("LevelNo").ToString
                                Exit For
                            End If
                        Next
                    End If
                End If
            End If
        Catch ex As Exception
            'Response.Write(ex.Message)
        Finally
            clsPL = Nothing
            DS.Dispose()
            DV.Dispose()
        End Try

        'strQuery = "Select Levelname, LevelNo from tblProductionLevels where contractorid='" & Session("contractorid") & "' and JobsRouting = 'True' and LevelNo not in ('1073741824', '2147483647') "
        'Dim CMUsr As New SqlCommand(strQuery, New SqlConnection(strConn))
        'Try
        '    CMUsr.Connection.Open()
        '    Dim DRRec As SqlDataReader = CMUsr.ExecuteReader()
        '    If DRRec.HasRows Then
        '        While (DRRec.Read)
        '            'Response.Write(DRRec("LevelNo"))
        '            LevelNo = DRRec("LevelNo").ToString + 100
        '            If LevelNo = ProLevel.Value Then
        '                LvlFound = True
        '                Lvlchk = DRRec("LevelNo").ToString
        '            End If
        '            If i = 0 Then
        '                LvlAssn = LevelNo
        '            Else
        '                LvlAssn = LvlAssn & "," & LevelNo
        '            End If
        '            i = i + 1
        '        End While
        '    End If
        '    DRRec.Close()
        'Finally
        '    If CMUsr.Connection.State = System.Data.ConnectionState.Open Then
        '        CMUsr.Connection.Close()
        '        CMUsr = Nothing
        '    End If
        'End Try
        DirMins = 0
        Dim k As Integer = 0
        Dim strJobnumber As String = String.Empty

        Dim clsRo As ETS.BL.Routing
        Dim DSRo As New Data.DataSet
        Try
            clsRo = New ETS.BL.Routing

            If HAccID.Value = "" Then
                DSRo = clsRo.RoutingToolAccountRoutingRecordsByLevel(Lvlchk, "", LevelNo, Session("ContractorID").ToString)
            Else
                'Response.Write(Lvlchk & "<BR>" & LevelNo)
                DSRo = clsRo.RoutingToolAccountRoutingRecordsByLevel(Lvlchk, HAccID.Value.ToString, LevelNo, Session("ContractorID").ToString)
            End If

            If DSRo.Tables.Count > 0 Then
                If DSRo.Tables(0).Rows.Count > 0 Then
                    For Each JobDR As Data.DataRow In DSRo.Tables(0).Rows
                        If strJobnumber = JobDR("jobnumber").ToString Then
                        Else
                            strJobnumber = JobDR("jobnumber").ToString
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
                                r.ForeColor = Drawing.Color.Black
                            Else
                                DueDate = JobDR("DueDate").ToString
                                If IsDate(DueDate) Then
                                    If DueDate < Now() Then
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
                            If IsDBNull(JobDR("Mins")) Then
                                Mins = ""
                            Else
                                Mins = JobDR("Mins").ToString
                            End If

                            If JobDR("Priority").ToString = "True" Then
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

                            c2.Text = Jobnumber
                            c3.Text = duration
                            c4.Text = SubmitDate
                            c5.Text = DueDate
                            c6.Text = ActName
                            c7.Text = uName
                            c8.Text = TemplateName
                            c9.Text = "Checked Out " & JobDR("LevelName").ToString
                            c10.Text = TAT
                            c11.Text = Priority
                            c12.Text = dirStatus
                            c13.Text = JobDR("username").ToString
                            c14.Text = DateDiff(DateInterval.Hour, Now, DueDate)
                            r.Cells.Add(c1)
                            r.Cells.Add(c2)
                            r.Cells.Add(c9)
                            r.Cells.Add(c13)
                            r.Cells.Add(c10)
                            r.Cells.Add(c11)
                            r.Cells.Add(c3)
                            r.Cells.Add(c4)
                            r.Cells.Add(c5)
                            r.Cells.Add(c14)
                            r.Cells.Add(c6)
                            r.Cells.Add(c7)
                            r.Cells.Add(c12)

                            ' r.Cells.Add(c8)
                            r.Font.Size = "8"
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
                    LblDMins.Text = DirMins
                    lblTotJobs.Text = k
                    Table4.Visible = False
                End If
            Else
                LblDMins.Text = DirMins
                lblTotJobs.Text = k
                Table4.Visible = False
            End If
        Catch ex As Exception
            'Response.Write(ex.Message)
        Finally
            clsRo = Nothing
            DSRo.Dispose()
        End Try


        'If HAccID.Value = "" Then
        '    strQuery = "Select distinct T.TranscriptionID,  T.Jobnumber, T.TAT, T.Priority, T.submitDate, T.DueDate, A.AccountName, T.duration, datediff(s,0,T.duration) as Mins, P.uname, TL.TemplateName, PL.LevelName, U.username, PU.direct, l.datemodified from tbltranscriptionmain T LEFT JOIN tblTranscriptionLog  L  ON L.TranscriptionID= T.TranscriptionID and T.status = L.status LEFT JOIN (Select userid, username from tblusers) AS U ON U.Userid=L.UserID LEFT JOIN tblProductionLevels PL ON PL.LevelNo = (T.status-100) LEFT JOIN (Select AccountID, AccountName from tblAccounts) AS A ON A.accountID=T.AccountID  LEFT JOIN (Select PhysicianID, firstname + ' ' + lastname as uname from tblphysicians) AS P ON P.physicianID=T.dictatorID     LEFT JOIN (Select userid, PhysicianID, LevelNo, 'Yes' as Direct from tblUserPrLvlMgmt where LevelNo in (" & Lvlchk & ") and direct='True') AS PU ON PU.userid=L.UserID and PU.physicianID=T.dictatorID   LEFT JOIN (Select TemplateID, TemplateName from tblTemplates) AS TL ON TL.TemplateID=T.TemplateID where   T.status in (" & ProLevel.Value & ") and L.DateModified = (SELECT MAX(DateModified) AS MaxDate FROM tblTranscriptionLog where transcriptionid=L.TranscriptionID) order by  t.duedate "
        'Else
        '    strQuery = "Select distinct T.TranscriptionID,  T.Jobnumber, T.TAT, T.Priority, T.submitDate, T.DueDate, A.AccountName, T.duration, datediff(s,0,T.duration) as Mins, P.uname, TL.TemplateName, PL.LevelName, U.username, PU.direct, l.datemodified from tbltranscriptionmain T LEFT JOIN tblTranscriptionLog  L  ON L.TranscriptionID= T.TranscriptionID and T.status = L.status LEFT JOIN (Select userid, username from tblusers) AS U ON U.Userid=L.UserID LEFT JOIN tblProductionLevels PL ON PL.LevelNo = (T.status-100) LEFT JOIN (Select AccountID, AccountName from tblAccounts) AS A ON A.accountID=T.AccountID  LEFT JOIN (Select PhysicianID, firstname + ' ' + lastname as uname from tblphysicians) AS P ON P.physicianID=T.dictatorID     LEFT JOIN (Select userid, PhysicianID, LevelNo, 'Yes' as Direct from tblUserPrLvlMgmt where LevelNo in (" & Lvlchk & ") and direct='True') AS PU ON PU.userid=L.UserID and PU.physicianID=T.dictatorID   LEFT JOIN (Select TemplateID, TemplateName from tblTemplates) AS TL ON TL.TemplateID=T.TemplateID where  T.AccountID = '" & HAccID.Value & "' and T.status in (" & ProLevel.Value & ") and L.DateModified = (SELECT MAX(DateModified) AS MaxDate FROM tblTranscriptionLog where transcriptionid=L.TranscriptionID) order by  t.duedate "
        'End If
        'Response.Write(strQuery.ToString)
        'Dim DRExc As New SqlCommand(strQuery, New SqlConnection(strConn))
        'Try
        '    DRExc.Connection.Open()
        '    Dim JobDR As SqlDataReader = DRExc.ExecuteReader()
        '    If JobDR.HasRows Then
        '        While (JobDR.Read)
        '            If strJobnumber = JobDR("jobnumber").ToString Then
        '            Else
        '                strJobnumber = JobDR("jobnumber").ToString
        '                Mins = 0
        '                Dim c1 As New TableCell
        '                Dim c2 As New TableCell
        '                Dim c3 As New TableCell
        '                Dim c4 As New TableCell
        '                Dim c5 As New TableCell
        '                Dim c6 As New TableCell
        '                Dim c7 As New TableCell
        '                Dim c8 As New TableCell
        '                Dim c9 As New TableCell
        '                Dim c10 As New TableCell
        '                Dim c11 As New TableCell
        '                Dim c12 As New TableCell
        '                Dim c13 As New TableCell
        '                Dim c14 As New TableCell
        '                Dim r As New TableRow
        '                If IsDBNull(JobDR("AccountName")) Then
        '                    ActName = ""
        '                Else
        '                    ActName = JobDR("AccountName").ToString
        '                End If

        '                If IsDBNull(JobDR("TranscriptionID")) Then
        '                    TransID = ""
        '                Else
        '                    TransID = JobDR("TranscriptionID").ToString
        '                End If
        '                If IsDBNull(JobDR("SubmitDate")) Then
        '                    SubmitDate = ""
        '                Else
        '                    SubmitDate = JobDR("SubmitDate").ToString
        '                End If

        '                If IsDBNull(JobDR("DueDate")) Then
        '                    DueDate = Now
        '                    r.ForeColor = Drawing.Color.Black
        '                Else
        '                    DueDate = JobDR("DueDate").ToString
        '                    If IsDate(DueDate) Then
        '                        If DueDate < Now() Then
        '                            r.ForeColor = Drawing.Color.Maroon
        '                        End If
        '                    End If
        '                End If
        '                If IsDBNull(JobDR("Jobnumber")) Then
        '                    Jobnumber = ""
        '                Else
        '                    Jobnumber = JobDR("Jobnumber").ToString
        '                End If

        '                If IsDBNull(JobDR("TemplateName")) Then
        '                    TemplateName = ""
        '                Else
        '                    TemplateName = JobDR("TemplateName").ToString
        '                End If

        '                If IsDBNull(JobDR("uName")) Then
        '                    uName = ""
        '                Else
        '                    uName = JobDR("uName").ToString
        '                End If

        '                If IsDBNull(JobDR("Duration")) Then
        '                    duration = ""
        '                Else
        '                    duration = JobDR("Duration").ToString
        '                End If
        '                If IsDBNull(JobDR("Mins")) Then
        '                    Mins = ""
        '                Else
        '                    Mins = JobDR("Mins").ToString
        '                End If

        '                If JobDR("Priority").ToString = "True" Then
        '                    r.ForeColor = Drawing.Color.BurlyWood
        '                    Priority = True
        '                Else
        '                    Priority = False
        '                End If


        '                TAT = JobDR("TAT").ToString


        '                If IsDBNull(JobDR("direct")) Then
        '                    dirStatus = "No"
        '                Else
        '                    dirStatus = JobDR("direct").ToString
        '                    r.ForeColor = Drawing.Color.Black

        '                    DirMins = DirMins + Mins

        '                End If


        '                intExcMins = intExcMins + Mins


        '                'If intExcMins < ExcMins Then
        '                k = k + 1
        '                Dim CB1 As New CheckBox



        '                CB1.ID = "TransID" & k
        '                CB1.InputAttributes.Add("Value", TransID & "#" & Mins)
        '                'CB1.Attributes.Add("Checked", "No")
        '                CB1.EnableViewState = "False"
        '                CB1.InputAttributes.Add("onclick", "highlightRow(this)")
        '                'CB1.Checked = "False"

        '                c1.Controls.Add(CB1)

        '                c2.Text = Jobnumber
        '                c3.Text = duration
        '                c4.Text = SubmitDate
        '                c5.Text = DueDate
        '                c6.Text = ActName
        '                c7.Text = uName
        '                c8.Text = TemplateName
        '                c9.Text = "Checked Out " & JobDR("LevelName").ToString
        '                c10.Text = TAT
        '                c11.Text = Priority
        '                c12.Text = dirStatus
        '                c13.Text = JobDR("username").ToString
        '                c14.Text = DateDiff(DateInterval.Hour, Now, DueDate)
        '                r.Cells.Add(c1)
        '                r.Cells.Add(c2)
        '                r.Cells.Add(c9)
        '                r.Cells.Add(c13)
        '                r.Cells.Add(c10)
        '                r.Cells.Add(c11)
        '                r.Cells.Add(c3)
        '                r.Cells.Add(c4)
        '                r.Cells.Add(c5)
        '                r.Cells.Add(c14)
        '                r.Cells.Add(c6)
        '                r.Cells.Add(c7)
        '                r.Cells.Add(c12)

        '                ' r.Cells.Add(c8)
        '                r.Font.Size = "8"
        '                r.Font.Italic = False

        '                Table4.Rows.Add(r)

        '                'Response.Write(CB1.Checked)


        '                'Else
        '                '    Exit While
        '                'End If
        '            End If
        '        End While
        '        'Response.Write(intExcMins)

        '        LblTMins.Text = FormatNumber((intExcMins / 60), 0)
        '        lblTotJobs.Text = k
        '        LblDMins.Text = FormatNumber((DirMins / 60), 0)
        '        TotJobs.Value = k
        '    Else

        '        ' LblTotmins.Text = intExcMins
        '        LblDMins.Text = DirMins
        '        lblTotJobs.Text = k
        '        Table4.Visible = False
        '        'submit.Visible = False


        '    End If
        '    JobDR.Close()
        'Finally
        '    If DRExc.Connection.State = System.Data.ConnectionState.Open Then
        '        DRExc.Connection.Close()
        '        DRExc = Nothing
        '    End If
        'End Try

    End Sub



    Protected Function chkLevel(ByVal AdminLevel As Long, ByVal Level As Long) As Boolean
        If (AdminLevel And Level) = Level Then
            chkLevel = True
        Else
            chkLevel = False
        End If
    End Function


    Sub submit_Click()
        Dim TransID As String
        Dim I As Integer
        Dim realStatus As Integer
        Dim ArrayL() As String
        Dim SelTransID As String
        realStatus = 100 + CInt(ProLevel.Value)
        For I = 1 To TotJobs.Value
            TransID = "TransID" & I
            TransID = Request(TransID)
            ArrayL = Split(TransID, "#")

            SelTransID = ArrayL(0)
            If SelTransID <> "" Then
                Dim strConn As String
                strConn = System.Configuration.ConfigurationManager.AppSettings("ETSCon")
                Dim strQuery As String
                strQuery = "update tbltranscriptionMain set status=" & realStatus & "  Where TranscriptionID ='" & SelTransID & "' "
                'Response.Write(strQuery)
                'Response.End()

                Dim DRExc As New SqlCommand(strQuery, New SqlConnection(strConn))
                Try
                    DRExc.Connection.Open()
                    DRExc.ExecuteNonQuery()
                Finally
                    If DRExc.Connection.State = System.Data.ConnectionState.Open Then
                        DRExc.Connection.Close()
                        DRExc = Nothing
                    End If
                End Try

                strQuery = "Insert Into tblTranscriptionLog (userid, TranscriptionID, Status, Datemodified, IP) Values ('" & HUserID.Value & "', '" & SelTransID & "', " & realStatus & ",'" & Now & "', '10.0.0.96') "
                Dim DRExc1 As New SqlCommand(strQuery, New SqlConnection(strConn))
                Try
                    DRExc1.Connection.Open()
                    DRExc1.ExecuteNonQuery()
                Finally
                    If DRExc1.Connection.State = System.Data.ConnectionState.Open Then
                        DRExc1.Connection.Close()
                        DRExc1 = Nothing
                    End If
                End Try

            End If

        Next

    End Sub
    Protected Sub DLStatus_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles DLStatus.SelectedIndexChanged
        If DLStatus.Items(0).Value = "" Then
            DLStatus.Items.RemoveAt(0)
        End If
        DLChoice.Items.Clear()
        If DLStatus.SelectedValue = "Status" Then
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
                clsPL.LevelNo = ProLevel.Value - 100
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

            'Dim strConn As String
            'strConn = System.Configuration.ConfigurationManager.AppSettings("ETSCon")
            'Dim strQuery As String
            'strQuery = "Select Levelname, LevelNo from tblProductionLevels where levelno = '" & ProLevel.Value - 100 & "' and contractorid='" & Session("contractorid") & "' and JobsRouting = 'True' and LevelNo not in ('1073741824', '2147483647') "
            ''Response.Write(strQuery)
            'Dim CMUsr As New SqlCommand(strQuery, New SqlConnection(strConn))
            'Try
            '    CMUsr.Connection.Open()
            '    Dim DRRec As SqlDataReader = CMUsr.ExecuteReader()
            '    If DRRec.HasRows Then
            '        While (DRRec.Read)
            '            Dim LItem As New ListItem
            '            LItem.Text = "Pending " & DRRec("LevelName").ToString
            '            LItem.Value = DRRec("LevelNo").ToString
            '            DLChoice.Items.Add(LItem)
            '        End While
            '    End If
            '    DRRec.Close()
            'Finally
            '    If CMUsr.Connection.State = System.Data.ConnectionState.Open Then
            '        CMUsr.Connection.Close()
            '        CMUsr = Nothing
            '    End If
            'End Try
        ElseIf DLStatus.SelectedValue = "TAT" Then
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
        End If
        'AssignUser()
        DictStatus()
    End Sub
    Protected Sub Button1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button1.Click
        Dim TransID As String
        Dim I As Integer
        Dim ArrayL() As String
        Dim SelTransID As String
        'Dim realStatus As Integer

        'Dim clsRo As ETS.BL.Routing
        Dim DT As New Data.DataTable
        DT.Columns.Add("CurrentStatus", GetType(System.Int32))
        DT.Columns.Add("TranscriptionID", GetType(System.String))

        Try
            'clsRo = New ETS.BL.Routing
            'realStatus = DLChoice.SelectedValue

            'Dim DT As New Data.DataTable
            'DT.Columns.Add(New Data.DataColumn("TransID"))
            Dim varNoJobRequested As Integer = 0

            'For I = 1 To TotJobs.Value
            '    Dim DRTransID As Data.DataRow = DT.NewRow
            '    TransID = "TransID" & I
            '    TransID = Request(TransID)
            '    ArrayL = Split(TransID, "#")

            '    SelTransID = ArrayL(0)

            '    If Not String.IsNullOrEmpty(SelTransID) Then
            '        DRTransID("TransID") = SelTransID
            '        DT.Rows.Add(DRTransID)
            '        varNoJobRequested = varNoJobRequested + 1
            '    End If
            'Next

            For I = 1 To TotJobs.Value
                Dim DRTransID As Data.DataRow = DT.NewRow
                TransID = "TransID" & I
                TransID = Request(TransID)
                ArrayL = Split(TransID, "#")

                SelTransID = ArrayL(0)

                If Not String.IsNullOrEmpty(SelTransID) Then
                    'DRTransID("TransID") = SelTransID
                    'DT.Rows.Add(DRTransID)

                    Dim DRow As Data.DataRow = DT.NewRow
                    DRow("TranscriptionID") = SelTransID
                    DRow("CurrentStatus") = ProLevel.Value
                    'lblStatusMsg.Text = SelTransID.ToString & "<BR>"


                    DT.Rows.Add(DRow)
                    varNoJobRequested = varNoJobRequested + 1
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
                'Try
                '    If Trim(UCase(DLStatus.SelectedValue)) = Trim(UCase("Status")) Then
                '        lblStatusMsg.Text = clsRo.UpdateJobsStatusFromAccountRouting(DT, ProLevel.Value, realStatus, Request.UserHostAddress(), Session("UserID"), varNoJobRequested)
                '    ElseIf Trim(UCase(DLStatus.SelectedValue)) = Trim(UCase("TAT")) Then
                '        lblStatusMsg.Text = clsRo.UpdateJobsTATFromAccountRouting(DT, DLChoice.SelectedValue, varNoJobRequested)
                '    End If
                'Catch ex As Exception
                '    Response.Write(ex.Message)
                'Finally
                '    clsRo = Nothing
                'End Try
            End If
        Catch ex As Exception
            Response.Write(ex.Message)
        Finally
            DT.Dispose()
        End Try


        'Dim TransID As String
        'Dim I As Integer
        'Dim realStatus As Integer
        'Dim TAT As String
        'Dim ArrayL() As String
        'Dim SelTransID As String
        ''Response.Write(DLStatus.SelectedValue)
        'If DLStatus.SelectedValue = "Status" Then
        '    realStatus = DLChoice.SelectedValue
        '    For I = 1 To TotJobs.Value
        '        TransID = "TransID" & I
        '        TransID = Request(TransID)
        '        'Response.Write(Request(TransID))
        '        ArrayL = Split(TransID, "#")

        '        SelTransID = ArrayL(0)
        '        If SelTransID <> "" Then
        '            Dim strConn As String
        '            strConn = System.Configuration.ConfigurationManager.AppSettings("ETSCon")
        '            Dim SQLConn As New SqlConnection
        '            SQLConn.ConnectionString = strConn
        '            SQLConn.Open()
        '            Dim Trans1 As SqlTransaction
        '            Trans1 = SQLConn.BeginTransaction
        '            Dim RecUp1 As Integer = 0
        '            Dim RecUp2 As Integer = 0
        '            Dim strQuery As String
        '            strQuery = "update tbltranscriptionMain set status=" & realStatus & "  Where TranscriptionID ='" & SelTransID & "' and status ='" & ProLevel.Value & "' "
        '            Dim DRExc As New SqlCommand(strQuery, SQLConn)
        '            DRExc.Transaction = Trans1
        '            RecUp1 = DRExc.ExecuteNonQuery()
        '            strQuery = "Insert Into tblTranscriptionLog (AssignedBy, TranscriptionID, Status, Datemodified, IP) Values ('" & Session("userid") & "', '" & SelTransID & "', " & realStatus & ",'" & Now & "','" & Request.UserHostAddress() & "') "
        '            Dim DRExc1 As New SqlCommand(strQuery, SQLConn)
        '            DRExc1.Transaction = Trans1
        '            RecUp2 = DRExc1.ExecuteNonQuery()

        '            If RecUp1 > 0 And RecUp2 > 0 Then
        '                Trans1.Commit()
        '                'Response.Write("<script>alert('Records have been updated successfully')</script>")
        '            Else
        '                Trans1.Rollback()
        '                'Response.Write("<script>alert('Issue in updating records.')</script>")
        '            End If


        '            If SQLConn.State = System.Data.ConnectionState.Open Then
        '                SQLConn.Close()
        '                SQLConn = Nothing
        '            End If
        '        End If
        '    Next
        'ElseIf DLStatus.SelectedValue = "TAT" Then
        '    TAT = DLChoice.SelectedValue
        '    For I = 1 To TotJobs.Value
        '        TransID = "TransID" & I
        '        TransID = Request(TransID)
        '        ArrayL = Split(TransID, "#")

        '        SelTransID = ArrayL(0)
        '        If SelTransID <> "" Then
        '            Dim strConn As String
        '            strConn = System.Configuration.ConfigurationManager.AppSettings("ETSCon")
        '            Dim strQuery As String
        '            strQuery = "update tbltranscriptionMain set TAT=" & TAT & "  Where TranscriptionID ='" & SelTransID & "' "
        '            'Response.Write(strQuery)

        '            Dim DRExc As New SqlCommand(strQuery, New SqlConnection(strConn))
        '            Try
        '                DRExc.Connection.Open()
        '                DRExc.ExecuteNonQuery()
        '            Finally
        '                If DRExc.Connection.State = System.Data.ConnectionState.Open Then
        '                    DRExc.Connection.Close()
        '                    DRExc = Nothing
        '                End If
        '            End Try

        '        End If
        '    Next
        'End If

        ''AssignUser()
        DictStatus()
    End Sub
End Class
