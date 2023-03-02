Imports System.Data.SqlClient

Partial Class RoutingTool_Default
    Inherits BasePage

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
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
            End If
            If HUserID.Value = "" Then
                HUserID.Value = Request("UserID")
            End If
            If HAccID.Value = "" Then
                HAccID.Value = Request("AccID")
            End If

        End If
        ActStatus()




    End Sub

    Sub ActStatus()
        'Dim strConn As String
        'Dim t2 As New Table
        't2.Style("width") = "100%"
        't2.BorderWidth = 2
        't2.GridLines = GridLines.Both
        'Dim intExcMins As Integer
        'intExcMins = 0
        'strConn = System.Configuration.ConfigurationManager.AppSettings("ETSCon")
        'Dim strQuery As String
        'TotJobs.Value = 0
        'Dim ActName As String
        'Dim TransID As String
        'Dim SubmitDate As String
        'Dim DueDate As String
        'Dim Jobnumber As String
        'Dim TemplateName As String
        'Dim uName As String
        'Dim duration As String
        'Dim Mins As Integer
        'Dim dirStatus As String
        'Dim DirMins As Integer

        'DirMins = 0
        'Dim k As Integer
        'k = 0
        ''strQuery = "Select T.TranscriptionID,  T.Jobnumber, T.submitDate, T.DueDate, A.AccountName, T.duration, datediff(s,0,T.duration)/60 as Mins, P.uname, TL.TemplateName from tbltranscriptionmain T LEFT JOIN (Select AccountID, AccountName from tblAccounts) AS A ON A.accountID=T.AccountID  LEFT JOIN (Select PhysicianID, firstname + ' ' + lastname as uname from tblphysicians) AS P ON P.physicianID=T.dictatorID  LEFT JOIN (Select TemplateID, TemplateName from tblTemplates) AS TL ON TL.TemplateID=T.TemplateID where T.submitdate >= '1/9/2008' and T.submitdate < '1/10/2008' and T.status =" & Request("ProLevel") & " and T.AccountID = '" & HAccID.Value & "'  order by DueDate Desc"
        'strQuery = "Select T.TranscriptionID,  T.Jobnumber, T.submitDate, T.DueDate, A.AccountName, T.duration, datediff(s,0,T.duration) as Mins, P.uname, TL.TemplateName, PU.direct from tbltranscriptionmain T LEFT JOIN (Select AccountID, AccountName from tblAccounts) AS A ON A.accountID=T.AccountID  LEFT JOIN (Select PhysicianID, firstname + ' ' + lastname as uname from tblphysicians) AS P ON P.physicianID=T.dictatorID  LEFT JOIN (Select userid, PhysicianID, LevelNo, 'Yes' as Direct from tblUserPrLvlMgmt where userid='" & HUserID.Value & "' and LevelNo=" & ProLevel.Value & " and direct='True') AS PU ON PU.physicianID=T.dictatorID  LEFT JOIN (Select TemplateID, TemplateName from tblTemplates) AS TL ON TL.TemplateID=T.TemplateID where  T.status =" & Request("ProLevel") & " and T.AccountID = '" & HAccID.Value & "'  order by  PU.direct Desc "
        ''Response.Write(strQuery)

        'Dim DRExc As New SqlCommand(strQuery, New SqlConnection(strConn))
        'DRExc.Connection.Open()
        'Dim JobDR As SqlDataReader = DRExc.ExecuteReader()
        'If JobDR.HasRows Then
        '    While (JobDR.Read)
        '        'Response.Write(JobDR("Direct"))
        '        Mins = 0
        '        Dim c1 As New TableCell
        '        Dim c2 As New TableCell
        '        Dim c3 As New TableCell
        '        Dim c4 As New TableCell
        '        Dim c5 As New TableCell
        '        Dim c6 As New TableCell
        '        Dim c7 As New TableCell
        '        Dim c8 As New TableCell
        '        Dim c9 As New TableCell
        '        Dim r As New TableRow

        '        If IsDBNull(JobDR("AccountName")) Then
        '            ActName = ""
        '        Else
        '            ActName = JobDR("AccountName").ToString
        '        End If

        '        If IsDBNull(JobDR("TranscriptionID")) Then
        '            TransID = ""
        '        Else
        '            TransID = JobDR("TranscriptionID").ToString
        '        End If



        '        If IsDBNull(JobDR("SubmitDate")) Then
        '            SubmitDate = ""
        '        Else
        '            SubmitDate = JobDR("SubmitDate").ToString
        '        End If

        '        If IsDBNull(JobDR("DueDate")) Then
        '            DueDate = ""
        '        Else
        '            DueDate = JobDR("DueDate").ToString
        '        End If

        '        If IsDBNull(JobDR("Jobnumber")) Then
        '            Jobnumber = ""
        '        Else
        '            Jobnumber = JobDR("Jobnumber").ToString
        '        End If

        '        If IsDBNull(JobDR("TemplateName")) Then
        '            TemplateName = ""
        '        Else
        '            TemplateName = JobDR("TemplateName").ToString
        '        End If

        '        If IsDBNull(JobDR("uName")) Then
        '            uName = ""
        '        Else
        '            uName = JobDR("uName").ToString
        '        End If

        '        If IsDBNull(JobDR("Duration")) Then
        '            duration = ""
        '        Else
        '            duration = JobDR("Duration").ToString
        '        End If

        '        If IsDBNull(JobDR("Mins")) Then
        '            Mins = ""
        '        Else
        '            Mins = JobDR("Mins").ToString
        '        End If

        '        If IsDBNull(JobDR("direct")) Then
        '            dirStatus = "No"
        '        Else
        '            dirStatus = JobDR("direct").ToString
        '            r.BackColor = Drawing.Color.Honeydew

        '            DirMins = DirMins + Mins

        '        End If


        '        intExcMins = intExcMins + Mins


        '        'If intExcMins < ExcMins Then
        '        k = k + 1
        '        Dim CB1 As New CheckBox



        '        CB1.ID = "TransID" & k
        '        CB1.InputAttributes.Add("Value", TransID & "#" & Mins)
        '        'CB1.InputAttributes.Add("Checked", "")
        '        'CB1.EnableViewState = "False"
        '        CB1.InputAttributes.Add("onclick", "highlightRow(this)")
        '        'CB1.Checked = True
        '        'Response.Write(CB1.Checked)

        '        c1.Controls.Add(CB1)

        '        c2.Text = Jobnumber
        '        c3.Text = duration
        '        c4.Text = SubmitDate
        '        c5.Text = DueDate
        '        c6.Text = ActName
        '        c7.Text = uName
        '        c8.Text = TemplateName
        '        c9.Text = dirStatus

        '        r.Cells.Add(c1)
        '        r.Cells.Add(c2)
        '        r.Cells.Add(c9)
        '        r.Cells.Add(c3)
        '        r.Cells.Add(c4)
        '        r.Cells.Add(c5)
        '        r.Cells.Add(c6)
        '        r.Cells.Add(c7)
        '        r.Cells.Add(c8)
        '        Table4.Rows.Add(r)
        '        'Response.Write(CB1.Checked)


        '        'Else
        '        '    Exit While
        '        'End If

        '    End While
        '    LblTMins.Text = FormatNumber((intExcMins / 60), 0)
        '    lblTotJobs.Text = k
        '    LblDMins.Text = FormatNumber((DirMins / 60), 0)
        '    TotJobs.Value = k
        'Else
        '    LblTotmins.Text = intExcMins
        '    LblDMins.Text = DirMins
        '    lblTotJobs.Text = k
        '    Table4.Visible = False
        '    submit.Visible = False


        'End If
        'JobDR.Close()
        'DRExc.Connection.Close()


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
        Dim DueDate As String
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
    
        DirMins = 0
        Dim k As Integer
        k = 0
        strQuery = "Select T.TranscriptionID, T.TAT, T.Priority,   T.Jobnumber, T.submitDate, T.DueDate, A.AccountName, T.duration, datediff(s,0,T.duration) as Mins, P.uname, TL.TemplateName, PL.LevelName from tbltranscriptionmain T  LEFT JOIN tblProductionLevels PL ON PL.LevelNo = T.status LEFT JOIN (Select AccountID, AccountName from tblAccounts) AS A ON A.accountID=T.AccountID  LEFT JOIN (Select PhysicianID, firstname + ' ' + lastname as uname from tblphysicians) AS P ON P.physicianID=T.dictatorID    LEFT JOIN (Select TemplateID, TemplateName from tblTemplates) AS TL ON TL.TemplateID=T.TemplateID where  T.status not in ('1073741824', '2147483647') and T.Priority = 'True'   "
        Dim DRExc As New SqlCommand(strQuery, New SqlConnection(strConn))
        DRExc.Connection.Open()
        Dim JobDR As SqlDataReader = DRExc.ExecuteReader()
        If JobDR.HasRows Then
            While (JobDR.Read)
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
                    DueDate = ""
                    r.BackColor = Drawing.Color.White
                    r.ForeColor = Drawing.Color.Black
                Else
                    DueDate = JobDR("DueDate").ToString
                    If IsDate(DueDate) Then
                        If DueDate < Now() Then
                            '            r.BackColor = Drawing.Color.Maroon
                '            r.ForeColor = Drawing.Color.black
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
                    '            r.BackColor = Drawing.Color.Yellow
                    '            r.ForeColor = Drawing.Color.Black
                    Priority = True
                Else
                    Priority = False
                End If


                TAT = JobDR("TAT").ToString


                '    If IsDBNull(JobDR("direct")) Then
                dirStatus = "No"
                'Else
                'dirStatus = JobDR("direct").ToString
                ''     r.BackColor = Drawing.Color.Honeydew

                'DirMins = DirMins + Mins

                'End If


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
                c9.Text = "Pending " & JobDR("LevelName").ToString
                c10.Text = TAT
                c11.Text = Priority
                c12.Text = dirStatus

                r.Cells.Add(c1)
                r.Cells.Add(c2)
                r.Cells.Add(c9)
                For X = 1 To i
                    Dim CellX As New TableCell
                    CellX.Text = JobDR(X - 1).ToString
                    r.Cells.Add(CellX)
                Next
                r.Cells.Add(c10)
                r.Cells.Add(c11)
                r.Cells.Add(c3)
                r.Cells.Add(c4)
                r.Cells.Add(c5)
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
        DRExc.Connection.Close()
      
    End Sub

    'Sub AssignUser()
    '    'Dim strConn As String
    '    'Dim strQuery As String
    '    'Dim scdMins As Integer
    '    'Dim MinsDone As Integer
    '    'Dim MinsAssn As Integer
    '    'Dim MinsPend As Integer
    '    'Dim DirMins As Integer
    '    'strConn = System.Configuration.ConfigurationManager.AppSettings("ETSCon")
    '    'Dim Userid As String

    '    ''strQuery = "Select U.FirstName +' ' + U.LastName as uname, U.Username, U.UserID, S.SchMins from tblUsers U, tblAccountUserAssgn A, tblUsersLevels UL, tblUsersSchMins S where dbo.chkLevel(UL.ProductionLevel, " & Request("ProLevel") & ")='True' and A.LevelNo=" & Request("ProLevel") & " and   UL.UserID = U.USerID and A.UserID=U.USerID and AccountID='" & Request("AccID") & "' and U.UserID*=S.USerID and U.UserID='" & Request("UserID") & "' and S.schDate = '1/9/2008' order by U.Firstname"
    '    'strQuery = "Select U.FirstName +' ' + U.LastName as uname, U.Username, U.UserID, S.SchMins from (Select * from tblUsers where (Isdeleted is NULL) or (Isdeleted = 'False')) U LEFT JOIN tblAccountUserAssgn A ON A.UserID=U.USerID and A.AccountID='" & Request("AccID") & "'  and A.LevelNo=" & Request("ProLevel") & " INNER JOIN tblUsersLevels UL ON dbo.chkLevel(UL.ProductionLevel, " & Request("ProLevel") & ")='True' and   UL.UserID = U.USerID LEFT JOIN tblUsersSchMins S ON U.UserID=S.USerID  and S.schDate = '1/9/2008'  where  U.UserID='" & Request("UserID") & "'order by U.Firstname"
    '    ''Response.Write(strQuery)

    '    'Dim CMUser As New SqlCommand(strQuery, New SqlConnection(strConn))
    '    'CMUser.Connection.Open()
    '    'Dim SQLCmd1 As SqlDataReader = CMUser.ExecuteReader()
    '    'If SQLCmd1.HasRows Then
    '    '    While (SQLCmd1.Read)
    '    '        Dim CL1 As New TableCell
    '    '        Dim CL2 As New TableCell
    '    '        Dim CL3 As New TableCell
    '    '        Dim CL4 As New TableCell
    '    '        Dim RW1 As New TableRow
    '    '        Dim CL6 As New TableCell
    '    '        Dim CL7 As New TableCell
    '    '        Userid = SQLCmd1("UserID").ToString
    '    '        CL1.Text = SQLCmd1("uname")
    '    '        CL2.Text = SQLCmd1("username")
    '    '        If IsDBNull(SQLCmd1("SchMins")) Then
    '    '            scdMins = 0
    '    '            CL3.Text = 0
    '    '        Else
    '    '            scdMins = SQLCmd1("SchMins").ToString
    '    '            CL3.Text = SQLCmd1("SchMins").ToString
    '    '        End If

    '    '        strQuery = "Select sum(Mins) as Mins from (select distinct L.TranscriptionID, datediff(s,0,M.duration)  as Mins from tbltranscriptionlog L, tbltranscriptionMain M where L.TranscriptionID= M.TranscriptionID and L.userid='" & Userid & "' and L.Userlevel = " & Request("ProLevel") & " and L.status in (Select LevelNo from tblProductionlevels where levelno <> " & Request("ProLevel") & ")) as DT"
    '    '        Dim CMUser1 As New SqlCommand(strQuery, New SqlConnection(strConn))
    '    '        CMUser1.Connection.Open()
    '    '        Dim SQLCmd2 As SqlDataReader = CMUser1.ExecuteReader()
    '    '        If SQLCmd2.HasRows Then
    '    '            While (SQLCmd2.Read)
    '    '                If IsDBNull(SQLCmd2("Mins")) Then
    '    '                    MinsDone = 0
    '    '                    CL4.Text = 0
    '    '                Else
    '    '                    MinsDone = FormatNumber((SQLCmd2("Mins").ToString / 60), 0)
    '    '                    CL4.Text = FormatNumber((SQLCmd2("Mins").ToString / 60), 0)
    '    '                End If

    '    '                'Response.Write(SQLCmd2("Mins").ToString)
    '    '            End While
    '    '        End If
    '    '        SQLCmd2.Close()
    '    '        CMUser1.Connection.Close()
    '    '        Dim realStatus As Integer
    '    '        realStatus = 100 + CInt(Request("prolevel"))




    '    '        strQuery = "Select sum(Mins) as Mins from (select distinct L.TranscriptionID, datediff(s,0,M.duration)  as Mins from tbltranscriptionlog L, tbltranscriptionMain M where L.TranscriptionID= M.TranscriptionID and L.userid='" & Userid & "' and L.status = " & realStatus & " ) as DT"
    '    '        'Response.Write(strQuery)
    '    '        Dim CL5 As New TableCell
    '    '        Dim CMUser2 As New SqlCommand(strQuery, New SqlConnection(strConn))
    '    '        CMUser2.Connection.Open()
    '    '        Dim SQLCmd3 As SqlDataReader = CMUser2.ExecuteReader()
    '    '        If SQLCmd3.HasRows Then
    '    '            While (SQLCmd3.Read)

    '    '                If IsDBNull(SQLCmd3("Mins")) Then
    '    '                    MinsAssn = 0
    '    '                    CL5.Text = 0
    '    '                Else
    '    '                    MinsAssn = FormatNumber((SQLCmd3("Mins").ToString / 60), 0)
    '    '                    CL5.Text = FormatNumber((SQLCmd3("Mins").ToString / 60), 0)
    '    '                End If
    '    '                'Response.Write(SQLCmd2("Mins").ToString)
    '    '            End While
    '    '        End If
    '    '        SQLCmd3.Close()
    '    '        CMUser2.Connection.Close()

    '    '        If scdMins > 0 Then
    '    '            MinsPend = scdMins - (MinsDone + MinsAssn)
    '    '            If MinsPend < 0 Then
    '    '                MinsPend = 0
    '    '            End If
    '    '        Else
    '    '            MinsPend = 0
    '    '        End If

    '    '        strQuery = "Select sum(Mins) as Mins from (select distinct M.TranscriptionID, datediff(s,0,M.duration) as Mins from tblUserPrLvlMgmt L, tbltranscriptionMain M where L.PhysicianID= M.DictatorID and L.userid='" & Userid & "' and Direct='True' and M.status = " & Request("ProLevel") & " and M.status=L.LevelNo and M.AccountID='" & Request("AccID") & "' and submitdate >= '1/9/2008' and submitdate < '1/10/2008' ) as DT"

    '    '        'Response.Write(strQuery)
    '    '        Dim CMUser3 As New SqlCommand(strQuery, New SqlConnection(strConn))
    '    '        CMUser3.Connection.Open()
    '    '        Dim SQLCmd4 As SqlDataReader = CMUser3.ExecuteReader()
    '    '        If SQLCmd4.HasRows Then
    '    '            While (SQLCmd4.Read)

    '    '                If IsDBNull(SQLCmd4("Mins")) Then
    '    '                    DirMins = 0
    '    '                    CL7.Text = 0
    '    '                Else
    '    '                    DirMins = FormatNumber((SQLCmd4("Mins").ToString / 60), 0)
    '    '                    CL7.Text = FormatNumber((SQLCmd4("Mins").ToString / 60), 0)
    '    '                End If

    '    '            End While
    '    '        End If

    '    '        SQLCmd4.Close()
    '    '        CMUser3.Connection.Close()


    '    '        CL7.Text = DirMins

    '    '        CL6.Text = MinsPend



    '    '        RW1.Cells.Add(CL1)
    '    '        RW1.Cells.Add(CL2)
    '    '        RW1.Cells.Add(CL3)
    '    '        RW1.Cells.Add(CL4)
    '    '        RW1.Cells.Add(CL5)
    '    '        RW1.Cells.Add(CL6)
    '    '        RW1.Cells.Add(CL7)

    '    '        Table2.Rows.Add(RW1)

    '    '    End While
    '    'End If

    '    Dim strConn As String
    '    Dim strQuery As String
    '    Dim scdMins As Integer
    '    Dim MinsDone As Integer
    '    Dim MinsAssn As Integer
    '    Dim MinsPend As Integer
    '    Dim DirMins As Integer
    '    Dim LvlNo As Integer
    '    Dim COLvlNo As Integer
    '    Dim LevelNo As Integer
    '    Dim LvlAssn As String
    '    Dim i As Integer
    '    i = 0
    '    LvlAssn = ""
    '    'LvlNo = DLLevel.SelectedValue
    '    COLvlNo = 100 + LvlNo
    '    strConn = System.Configuration.ConfigurationManager.AppSettings("ETSCon")
    '    Dim Userid As String

    '    strQuery = "Select Levelname, LevelNo from tblProductionLevels where JobsRouting = 'True' and LevelNo not in ('1073741824', '2147483647') "
    '    Dim CMUsr As New SqlCommand(strQuery, New SqlConnection(strConn))
    '    CMUsr.Connection.Open()
    '    Dim DRRec As SqlDataReader = CMUsr.ExecuteReader()
    '    If DRRec.HasRows Then
    '        While (DRRec.Read)
    '            'Response.Write(DRRec("LevelNo"))
    '            LevelNo = DRRec("LevelNo") + 100
    '            If i = 0 Then
    '                LvlAssn = LevelNo
    '            Else
    '                LvlAssn = LvlAssn & "," & LevelNo
    '            End If
    '            i = i + 1
    '        End While
    '    End If
    '    DRRec.Close()
    '    CMUsr.Connection.Close()
    '    strQuery = "Select distinct U.FirstName +' ' + U.LastName as uname, U.Username, U.UserID, S.SchMins, L.RecFound from (Select * from tblUsers where (Isdeleted is NULL) or (Isdeleted = 'False')) U, tblUsersLevels UL, tblUsersSchMins S, (Select 'Yes' as RecFound, userID, startdate, enddate from tblLeave) L where   dbo.chkLevel(UL.ProductionLevel, " & LvlNo & ")='True'  and L.startDate <= '" & ProcStartDate & "' and L.endDate >= '" & ProcStartDate & "' and S.SchDate = '" & ProcStartDate & "' and U.UserID='" & HUserID.Value & "' and  UL.UserID = U.USerID and U.UserID*=S.USerID  and U.UserID*=L.USerID order by RecFound, uname"
    '    'Response.Write(strQuery)

    '    Dim CMUser As New SqlCommand(strQuery, New SqlConnection(strConn))
    '    CMUser.Connection.Open()
    '    Dim DRRec1 As SqlDataReader = CMUser.ExecuteReader()
    '    If DRRec1.HasRows Then
    '        While (DRRec1.Read)
    '            Dim CL1 As New TableCell
    '            Dim CL2 As New TableCell
    '            Dim CL3 As New TableCell
    '            Dim CL4 As New TableCell
    '            Dim RW1 As New TableRow
    '            Dim CL6 As New TableCell
    '            Dim CL7 As New TableCell
    '            If DRRec1("RecFound").ToString <> "" Then
    '                RW1.BackColor = Drawing.Color.LightGray
    '                RW1.ForeColor = Drawing.Color.White
    '            End If


    '            Userid = DRRec1("UserID").ToString
    '            CL1.Text = DRRec1("uname")
    '            CL2.Text = DRRec1("username")
    '            If IsDBNull(DRRec1("SchMins")) Then
    '                scdMins = 0
    '                CL3.Text = 0
    '            Else
    '                scdMins = DRRec1("SchMins").ToString
    '                CL3.Text = DRRec1("SchMins").ToString
    '            End If


    '            strQuery = "Select sum(Mins) as Mins from (select distinct L.TranscriptionID, datediff(s,0,M.duration)  as Mins from tbltranscriptionlog L, tbltranscriptionMain M where M.Submitdate >= '" & ServStartDate & "' and M.Submitdate >= '" & ServEndDate & "' and  L.TranscriptionID= M.TranscriptionID and L.UserID='" & HUserID.Value & "'   and L.Userlevel = " & LvlNo & "  and L.status in (Select LevelNo from tblProductionlevels where levelno <> " & LvlNo & "  )) as DT"
    '            Dim CMUser1 As New SqlCommand(strQuery, New SqlConnection(strConn))
    '            CMUser1.Connection.Open()
    '            Dim DRRec2 As SqlDataReader = CMUser1.ExecuteReader()
    '            If DRRec2.HasRows Then
    '                While (DRRec2.Read)
    '                    If IsDBNull(DRRec2("Mins")) Then
    '                        MinsDone = 0
    '                        CL4.Text = 0
    '                    Else
    '                        MinsDone = FormatNumber((DRRec2("Mins").ToString / 60), 0)
    '                        CL4.Text = FormatNumber((DRRec2("Mins").ToString / 60), 0)
    '                    End If


    '                    'Response.Write(DRRec2("Mins").ToString)
    '                End While
    '            End If
    '            DRRec2.Close()
    '            CMUser1.Connection.Close()





    '            strQuery = "Select sum(Mins) as Mins from (select distinct L.TranscriptionID, datediff(s,0,M.duration) as Mins from tbltranscriptionlog L, tbltranscriptionMain M where L.TranscriptionID= M.TranscriptionID and L.UserID='" & HUserID.Value & "'   and M.status = L.status and M.status in (" & LvlAssn & ") ) as DT"
    '            'Response.Write(strQuery)
    '            Dim CL5 As New TableCell
    '            Dim CMUser2 As New SqlCommand(strQuery, New SqlConnection(strConn))
    '            CMUser2.Connection.Open()
    '            Dim DRRec3 As SqlDataReader = CMUser2.ExecuteReader()
    '            If DRRec3.HasRows Then
    '                While (DRRec3.Read)

    '                    If IsDBNull(DRRec3("Mins")) Then
    '                        MinsAssn = 0
    '                        CL5.Text = 0
    '                    Else
    '                        MinsAssn = FormatNumber((DRRec3("Mins").ToString / 60), 0)
    '                        CL5.Text = "<a href='JobsRouting.aspx?Userid=" & DRRec1("Userid").ToString & "&ProLevel=" & LvlNo & " '  Target=_Blank>" & FormatNumber((DRRec3("Mins").ToString / 60), 0) & "</a>"

    '                    End If
    '                    'Response.Write(DRRec2("Mins").ToString)
    '                End While
    '            End If
    '            DRRec3.Close()
    '            CMUser2.Connection.Close()

    '            If scdMins > 0 Then
    '                MinsPend = scdMins - (MinsDone + MinsAssn)
    '                If MinsPend < 0 Then
    '                    MinsPend = 0
    '                End If
    '            Else
    '                MinsPend = 0
    '            End If

    '            strQuery = "Select sum(Mins) as Mins from (select distinct M.TranscriptionID, datediff(s,0,M.duration)  as Mins from tblUserPrLvlMgmt L, tbltranscriptionMain M where L.PhysicianID= M.DictatorID  and L.UserID='" & HUserID.Value & "'   and Direct='True' and M.status=L.LevelNo and M.status=" & LvlNo & " and submitdate >= '1/9/2008' and submitdate < '1/10/2008' ) as DT"

    '            'Response.Write(strQuery)
    '            Dim CMUser3 As New SqlCommand(strQuery, New SqlConnection(strConn))
    '            CMUser3.Connection.Open()
    '            Dim DRRec4 As SqlDataReader = CMUser3.ExecuteReader()
    '            If DRRec4.HasRows Then
    '                While (DRRec4.Read)

    '                    If IsDBNull(DRRec4("Mins")) Then
    '                        DirMins = 0
    '                        CL7.Text = 0
    '                    Else
    '                        DirMins = FormatNumber((DRRec4("Mins").ToString / 60), 0)
    '                        CL7.Text = FormatNumber((DRRec4("Mins").ToString / 60), 0)
    '                    End If

    '                End While
    '            End If

    '            DRRec4.Close()
    '            CMUser3.Connection.Close()


    '            CL7.Text = DirMins
    '            CL6.Text = MinsPend


    '            RW1.Cells.Add(CL1)
    '            RW1.Cells.Add(CL2)
    '            RW1.Cells.Add(CL3)
    '            RW1.Cells.Add(CL4)
    '            RW1.Cells.Add(CL5)
    '            RW1.Cells.Add(CL6)
    '            RW1.Cells.Add(CL7)


    '            Table2.Rows.Add(RW1)

    '        End While
    '    End If
    'End Sub

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
        realStatus = 100 + CInt(prolevel.value)
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
                DRExc.Connection.Open()
                DRExc.ExecuteNonQuery()
                DRExc.Connection.Close()

                strQuery = "Insert Into tblTranscriptionLog (userid, TranscriptionID, Status, Datemodified, IP) Values ('" & HUserID.Value & "', '" & SelTransID & "', " & realStatus & ",'" & Now & "', '10.0.0.96') "
                Dim DRExc1 As New SqlCommand(strQuery, New SqlConnection(strConn))
                DRExc1.Connection.Open()
                DRExc1.ExecuteNonQuery()
                DRExc1.Connection.Close()

            End If

        Next

    End Sub


    Protected Sub DLStatus_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles DLStatus.SelectedIndexChanged
        Label1.Text = "Ok"
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
            Dim strConn As String
            strConn = System.Configuration.ConfigurationManager.AppSettings("ETSCon")
            Dim strQuery As String
            strQuery = "Select Levelname, LevelNo from tblProductionLevels where JobsRouting = 'True' and LevelNo not in ('1073741824', '2147483647') "
            Dim CMUsr As New SqlCommand(strQuery, New SqlConnection(strConn))
            CMUsr.Connection.Open()
            Dim DRRec As SqlDataReader = CMUsr.ExecuteReader()
            If DRRec.HasRows Then
                While (DRRec.Read)
                    Dim LItem As New ListItem
                    LItem.Text = "Pending " & DRRec("LevelName").ToString
                    LItem.Value = DRRec("LevelNo").ToString
                    DLChoice.Items.Add(LItem)
                End While
            End If
            DRRec.Close()
            CMUsr.Connection.Close()
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

    End Sub
End Class
