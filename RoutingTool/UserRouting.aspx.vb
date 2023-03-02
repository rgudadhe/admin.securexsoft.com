Imports System.Data.SqlClient
Imports System.Data
Partial Class RoutingTool_Default
    Inherits BasePage
    Private hourdiff As Integer
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        hourdiff = DateDiff(DateInterval.Hour, ServStartDate, Now)
        If IsPostBack Then
            submit_Click()
        End If
        lblmins.Text = "0"
        lbljobs.Text = "0"
        If ProLevel.Value = "" Then
            ProLevel.Value = Request("ProLevel")
        End If
        If HUserID.Value = "" Then
            HUserID.Value = Request("UserID")
        End If

        If HAccID.Value = "" Then
            HAccID.Value = Request("AccID")
        End If



        AssignUser()
        'Table2.Visible = False

        'Page.FindControl(Table5).Visible = False


        If Request("DictStatus") = "Yes" Then
            DictStatus()
            Table6.Visible = False
        Else
            Table5.Visible = False
            ActStatus()
        End If
        'DictStatus()





    End Sub
    Sub ActStatus()
        hourdiff = DateDiff(DateInterval.Hour, ServStartDate, Now)
        Dim strConn As String
        Dim t2 As New Table
        t2.Style("width") = "100%"
        t2.BorderWidth = 2
        t2.GridLines = GridLines.Both
        Dim AwaitingEntry As Integer
        Dim directmins As Integer
        Dim NotFinished As Integer
        strConn = System.Configuration.ConfigurationManager.AppSettings("ETSCon")

        Dim strQuery As String

        Dim Cell1 As New TableCell
        Dim Cell2 As New TableCell
        Dim Cell3 As New TableCell
        Dim Cell4 As New TableCell
        Dim Cell5 As New TableCell
        Dim AccID As String
        Dim Row1 As New TableRow
        Cell1.Text = "Account Name"
        'Row1.CssClass = "SMSelected"
        Row1.Style("text-align") = "Center"
        Cell1.CssClass = "alt1"

        Row1.Cells.Add(Cell1)

        strQuery = "Select DISTINCT  LevelName, LevelNo from tblproductionlevels where contractorid='" & Session("contractorid") & "' and  LevelNo = " & ProLevel.Value & " order by LevelNo"
        Dim CommPL As New SqlCommand(strQuery, New SqlConnection(strConn))
        Try
            CommPL.Connection.Open()
            Dim DRPL As SqlDataReader = CommPL.ExecuteReader()
            If DRPL.HasRows Then
                While (DRPL.Read)

                    Dim Ncell As New TableCell

                    Ncell.Text = "Pending " & DRPL("LevelName") & " Mins"
                    Row1.Cells.Add(Ncell)
                    Ncell.CssClass = "alt1"
                End While
            End If

            Cell5.Text = "Direct Mins"
            Cell5.CssClass = "alt1"
            Row1.Cells.Add(Cell5)
            DRPL.Close()

            Table3.Rows.Add(Row1)

        Finally
            If CommPL.Connection.State = System.Data.ConnectionState.Open Then
                CommPL.Connection.Close()
                CommPL = Nothing
            End If
        End Try

        'strQuery = "Select DISTINCT  A.AccountID, A.Accountname, A.ProtocolMins,   T2.AwaitingEntry, T3.Checkedout, T4.DoneMins, T6.pendingmins from tblaccounts AS A   LEFT JOIN (Select TM.AccountID, sum(datediff(s,0,TM.duration)/60) as AwaitingEntry  from  tbltranscriptionmain TM, tblProductionLevels L  where  TM.status=L.LevelNo and TM.status not in(1073741824)  and submitdate >= '1/9/2008' group by AccountID) AS T2 ON T2.accountid = A.accountid LEFT JOIN (Select TM.AccountID, sum(datediff(s,0,TM.duration)/60) as CheckedOut  from  tbltranscriptionmain TM   where  TM.status not in (Select LevelNo from tblProductionLevels)  and TM.status not in(1073741824)  and submitdate >= '1/9/2008' group by AccountID) AS T3 ON T3.accountid = A.accountid Left Join  (Select AccountID, sum(datediff(s,0,duration)/60) as DoneMins  from  tbltranscriptionmain  where   status in(1073741824)  and submitdate >= '1/9/2008' group by AccountID) AS T4 ON T4.accountid = A.accountid  Left Join  (Select AccountID, sum(datediff(s,0,duration)/60) as PendingMins  from tbltranscriptionmain  where   status not in(1073741824)  and submitdate < '1/9/2008'  group by AccountID) AS T6 ON T6.accountid = A.accountid  order by Accountname"
        'strQuery = "Select DISTINCT  A.AccountID, A.Accountname, A.ProtocolMins,   T2.AwaitingEntry, T3.Checkedout, T4.DoneMins, T6.pendingmins from tblaccounts AS A   LEFT JOIN (Select TM.AccountID, sum(datediff(s,0,TM.duration)/60) as AwaitingEntry  from  tbltranscriptionmain TM, tblProductionLevels L  where  TM.status=L.LevelNo and TM.status not in(1073741824)  and submitdate >= '1/9/2008' and L.LevelNo = " & ProLevel.Value & " group by AccountID) AS T2 ON T2.accountid = A.accountid LEFT JOIN (Select TM.AccountID, sum(datediff(s,0,TM.duration)/60) as CheckedOut  from  tbltranscriptionmain TM   where  TM.status not in (Select LevelNo from tblProductionLevels)  and TM.status not in(1073741824)  and submitdate >= '1/9/2008' group by AccountID) AS T3 ON T3.accountid = A.accountid Left Join  (Select AccountID, sum(datediff(s,0,duration)/60) as DoneMins  from  tbltranscriptionmain  where   status in(1073741824)  and submitdate >= '1/9/2008' group by AccountID) AS T4 ON T4.accountid = A.accountid  Left Join  (Select AccountID, sum(datediff(s,0,duration)/60) as PendingMins  from tbltranscriptionmain  where   status = " & ProLevel.Value & "  and submitdate < '1/9/2008'  group by AccountID) AS T6 ON T6.accountid = A.accountid  order by Accountname"
        strQuery = "Select DISTINCT  A.AccountID, A.Accountname, A.ProtocolMins,   T2.AwaitingEntry, T3.Direct from tblaccounts AS A   LEFT JOIN (Select TM.AccountID, sum(datediff(s,0,TM.duration)) as AwaitingEntry  from  tbltranscriptionmain TM, tblProductionLevels L  where  TM.status=L.LevelNo  and L.LevelNo = " & ProLevel.Value & " group by AccountID) AS T2 ON T2.accountid = A.accountid LEFT JOIN (select M.AccountID, sum(datediff(s,0,M.duration)) as direct from tblUserPrLvlMgmt L, tbltranscriptionMain M where L.PhysicianID= M.DictatorID and L.userid='" & HUserID.Value & "' and Direct='True' and M.status=L.LevelNo and M.status=" & ProLevel.Value & "  group by AccountID) AS T3 ON T3.accountid = A.accountid  where A.contractorid='" & Session("contractorid") & "'  order by Accountname"
        'Response.Write(strQuery)


        'strQuery = "Select DISTINCT  A.AccountID, A.Accountname, A.ProtocolMins,   T2.AwaitingEntry, T3.Checkedout, T4.DoneMins, T6.pendingmins from tblaccounts AS A   LEFT JOIN (Select TM.AccountID, sum(datediff(s,0,TM.duration)/60) as AwaitingEntry  from  tbltranscriptionmain TM, tblProductionLevels L  where  TM.status=L.LevelNo and TM.status not in(1073741824) and (L.ForcedRouting is null or L.ForcedRouting = 'False')  and submitdate >= '1/9/2008' group by AccountID) AS T2 ON T2.accountid = A.accountid LEFT JOIN (Select TM.AccountID, sum(datediff(s,0,TM.duration)/60) as CheckedOut  from  tbltranscriptionmain TM   where  TM.status not in (Select LevelNo from tblProductionLevels where )  and TM.status not in(1073741824)  and submitdate >= '1/9/2008' group by AccountID) AS T3 ON T3.accountid = A.accountid Left Join  (Select AccountID, sum(datediff(s,0,duration)/60) as DoneMins  from  tbltranscriptionmain  where   status in(1073741824)  and submitdate >= '1/9/2008' group by AccountID) AS T4 ON T4.accountid = A.accountid  Left Join  (Select AccountID, sum(datediff(s,0,duration)/60) as PendingMins  from tbltranscriptionmain  where   status not in(1073741824)  and submitdate < '1/9/2008'  group by AccountID) AS T6 ON T6.accountid = A.accountid  order by Accountname"
        Dim CmdRec As New SqlCommand(strQuery, New SqlConnection(strConn))
        Try
            CmdRec.Connection.Open()
            Dim DRRec As SqlDataReader = CmdRec.ExecuteReader()
            If DRRec.HasRows Then
                While (DRRec.Read)
                    Dim c1 As New TableCell
                    Dim c2 As New TableCell
                    Dim c3 As New TableCell
                    Dim c4 As New TableCell
                    Dim c5 As New TableCell
                    Dim c6 As New TableCell
                    Dim c7 As New TableCell
                    Dim r As New TableRow
                    NotFinished = 0
                    ' Dim Actname As New String
                    AccID = DRRec("AccountID").ToString

                    If IsDBNull(DRRec("AwaitingEntry")) Then
                        AwaitingEntry = 0
                    Else
                        AwaitingEntry = FormatNumber((DRRec("AwaitingEntry").ToString / 60), 0)
                    End If



                    If IsDBNull(DRRec("direct")) Then
                        directmins = 0
                    Else
                        directmins = FormatNumber((DRRec("Direct").ToString / 60), 0)
                    End If

                    If AwaitingEntry > 0 Then

                        c1.Text = DRRec("Accountname").ToString
                        c2.Text = "<a href='UserRouting.aspx?AccID=" & AccID & "&ProLevel=" & ProLevel.Value & "&Userid=" & HUserID.Value & "&DictStatus=Yes'>" & AwaitingEntry & "</a>"
                        'c2.Text = AwaitingEntry
                        If directmins > 0 Then
                            c3.Text = "<a href='UserRouting.aspx?AccID=" & AccID & "&ProLevel=" & ProLevel.Value & "&Userid=" & HUserID.Value & "&DictStatus=Yes&Direct=Yes'>" & directmins & "</a>"
                        Else
                            c3.Text = 0
                        End If

                        'c3.Text = directmins
                        r.Cells.Add(c1)
                        r.Cells.Add(c2)
                        r.Cells.Add(c3)
                        Table3.Rows.Add(r)
                    End If

                End While
            End If
            DRRec.Close()
        Finally
            If CmdRec.Connection.State = System.Data.ConnectionState.Open Then
                CmdRec.Connection.Close()
                CmdRec = Nothing
            End If
        End Try

    End Sub

    Sub DictStatus()
        hourdiff = DateDiff(DateInterval.Hour, ServStartDate, Now)
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
        'If Request("Direct") = "Yes" Then
        '    strQuery = "Select T.TranscriptionID,  T.Jobnumber, T.submitDate, T.DueDate, A.AccountName, T.duration, datediff(s,0,T.duration) as Mins, P.uname, TL.TemplateName, PU.direct from tbltranscriptionmain T LEFT JOIN (Select AccountID, AccountName from tblAccounts) AS A ON A.accountID=T.AccountID  LEFT JOIN (Select PhysicianID, firstname + ' ' + lastname as uname from tblphysicians) AS P ON P.physicianID=T.dictatorID  INNER JOIN (Select userid, PhysicianID, LevelNo, 'Yes' as Direct from tblUserPrLvlMgmt where userid='" & HUserID.Value & "' and LevelNo=" & ProLevel.Value & " and direct='True') AS PU ON PU.physicianID=T.dictatorID  LEFT JOIN (Select TemplateID, TemplateName from tblTemplates) AS TL ON TL.TemplateID=T.TemplateID where T.AccountID='" & HAccID.Value & "' and T.status =" & Request("ProLevel") & " order by  DueDate  Asc"
        'Else
        '    strQuery = "Select T.TranscriptionID,  T.Jobnumber, T.submitDate, T.DueDate, A.AccountName, T.duration, datediff(s,0,T.duration) as Mins, P.uname, TL.TemplateName, PU.direct from tbltranscriptionmain T LEFT JOIN (Select AccountID, AccountName from tblAccounts) AS A ON A.accountID=T.AccountID  LEFT JOIN (Select PhysicianID, firstname + ' ' + lastname as uname from tblphysicians) AS P ON P.physicianID=T.dictatorID  LEFT JOIN (Select userid, PhysicianID, LevelNo, 'Yes' as Direct from tblUserPrLvlMgmt where userid='" & HUserID.Value & "' and LevelNo=" & ProLevel.Value & " and direct='True') AS PU ON PU.physicianID=T.dictatorID  LEFT JOIN (Select TemplateID, TemplateName from tblTemplates) AS TL ON TL.TemplateID=T.TemplateID where T.AccountID='" & HAccID.Value & "' and T.status =" & Request("ProLevel") & " order by  DueDate  Asc"
        'End If
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
        '        'Response.Write(JobDR("Mins").ToString)

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
        '        'CB1.Attributes.Add("Checked", "No")
        '        CB1.EnableViewState = "False"
        '        CB1.InputAttributes.Add("onclick", "highlightRow(this)")
        '        'CB1.Checked = "False"

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
        '    'Response.Write(intExcMins)

        '    LblTMins.Text = FormatNumber((intExcMins / 60), 0)
        '    lblTotJobs.Text = k
        '    LblDMins.Text = FormatNumber((DirMins / 60), 0)
        '    TotJobs.Value = k
        'Else

        '    ' LblTotmins.Text = intExcMins
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
        strQuery = "Select Levelname, LevelNo from tblProductionLevels where JobsRouting = 'True' and LevelNo < " & ProLevel.Value & " and LevelNo not in ('1073741824', '2147483647') "
        Dim CMUsr As New SqlCommand(strQuery, New SqlConnection(strConn))
        Try
            CMUsr.Connection.Open()
            Dim DRRec As SqlDataReader = CMUsr.ExecuteReader()
            If DRRec.HasRows Then
                While (DRRec.Read)
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
                TableCell11.Visible = False
            End If
            DRRec.Close()
        Finally
            If CMUsr.Connection.State = System.Data.ConnectionState.Open Then
                CMUsr.Connection.Close()
                CMUsr = Nothing
            End If
        End Try
        DirMins = 0
        Dim k As Integer
        Dim X As Integer
        X = 0
        k = 0
        'strQuery = "Select T.TranscriptionID,  T.Jobnumber, T.submitDate, T.DueDate, A.AccountName, T.duration, datediff(s,0,T.duration)/60 as Mins, P.uname, TL.TemplateName from tbltranscriptionmain T LEFT JOIN (Select AccountID, AccountName from tblAccounts) AS A ON A.accountID=T.AccountID  LEFT JOIN (Select PhysicianID, firstname + ' ' + lastname as uname from tblphysicians) AS P ON P.physicianID=T.dictatorID  LEFT JOIN (Select TemplateID, TemplateName from tblTemplates) AS TL ON TL.TemplateID=T.TemplateID where T.submitdate >= '1/9/2008' and T.submitdate < '1/10/2008' and T.status =" & Request("ProLevel") & " and T.AccountID = '" & HAccID.Value & "'  order by DueDate Desc"
        'strQuery = "Select T.TranscriptionID,  T.Jobnumber, T.submitDate, T.DueDate, A.AccountName, T.duration, datediff(s,0,T.duration) as Mins, P.uname, TL.TemplateName, PU.direct from tbltranscriptionmain T LEFT JOIN (Select AccountID, AccountName from tblAccounts) AS A ON A.accountID=T.AccountID  LEFT JOIN (Select PhysicianID, firstname + ' ' + lastname as uname from tblphysicians) AS P ON P.physicianID=T.dictatorID  INNER JOIN (Select userid, PhysicianID, LevelNo, 'Yes' as Direct from tblUserPrLvlMgmt where userid='" & HUserID.Value & "' and LevelNo=" & ProLevel.Value & " and direct='True') AS PU ON PU.physicianID=T.dictatorID  LEFT JOIN (Select TemplateID, TemplateName from tblTemplates) AS TL ON TL.TemplateID=T.TemplateID where T.AccountID='" & HAccID.Value & "' and T.status =" & Request("ProLevel") & " order by  DueDate  Asc"
        If Request("Direct") = "Yes" Then
            strQuery = "Select" & SelQuery & " T.TranscriptionID, T.TAT, T.Priority,  T.Jobnumber, T.submitDate, T.DueDate, A.AccountName, T.duration, datediff(s,0,T.duration) as Mins, P.uname, TL.TemplateName, PU.direct, PL.LevelName,isnull(P.category, 'B') as category from tbltranscriptionmain T  " & JoinQuery & " LEFT JOIN tblProductionLevels PL ON PL.LevelNo = T.status LEFT JOIN (Select AccountID, AccountName from tblAccounts) AS A ON A.accountID=T.AccountID  LEFT JOIN (Select PhysicianID, firstname + ' ' + lastname as uname, category from tblphysicians) AS P ON P.physicianID=T.dictatorID  INNER JOIN (Select userid, PhysicianID, LevelNo, 'Yes' as Direct from tblUserPrLvlMgmt where userid='" & HUserID.Value & "' and LevelNo=" & ProLevel.Value & " and direct='True') AS PU ON PU.physicianID=T.dictatorID  LEFT JOIN (Select TemplateID, TemplateName from tblTemplates) AS TL ON TL.TemplateID=T.TemplateID where T. contractorid='" & Session("contractorid") & "' and PL. contractorid='" & Session("contractorid") & "' and T.AccountID='" & HAccID.Value & "' and T.status =" & Request("ProLevel") & " order by  DueDate  Asc"
        Else
            strQuery = "Select" & SelQuery & " T.TranscriptionID, T.TAT, T.Priority,  T.Jobnumber, T.submitDate, T.DueDate, A.AccountName, T.duration, datediff(s,0,T.duration) as Mins, P.uname, TL.TemplateName, PU.direct, PL.LevelName,isnull(P.category, 'B') as category from tbltranscriptionmain T " & JoinQuery & " LEFT JOIN tblProductionLevels PL ON PL.LevelNo = T.status LEFT JOIN (Select AccountID, AccountName from tblAccounts) AS A ON A.accountID=T.AccountID  LEFT JOIN (Select PhysicianID, firstname + ' ' + lastname as uname, category from tblphysicians) AS P ON P.physicianID=T.dictatorID  LEFT JOIN (Select userid, PhysicianID, LevelNo, 'Yes' as Direct from tblUserPrLvlMgmt where userid='" & HUserID.Value & "' and LevelNo=" & ProLevel.Value & " and direct='True') AS PU ON PU.physicianID=T.dictatorID  LEFT JOIN (Select TemplateID, TemplateName from tblTemplates) AS TL ON TL.TemplateID=T.TemplateID where  T.contractorid='" & Session("contractorid") & "' and PL. contractorid='" & Session("contractorid") & "' and T.AccountID='" & HAccID.Value & "' and T.status =" & Request("ProLevel") & " order by  DueDate  Asc"
        End If

        '  Response.Write(strQuery)
        '  Response.End()

        Dim DRExc As New SqlCommand(strQuery, New SqlConnection(strConn))
        Try
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
                        SubmitDate = Now()
                    Else
                        SubmitDate = JobDR("SubmitDate").ToString
                    End If

                    If IsDBNull(JobDR("DueDate")) Then
                        DueDate = now
                        'r.BackColor = Drawing.Color.White
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
                        'r.BackColor = Drawing.Color.Yellow
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
                    c9.Text = "Pending " & JobDR("LevelName").ToString
                    c10.Text = TAT
                    c11.Text = Priority
                    c12.Text = dirStatus
                    c13.Text = DateDiff(DateInterval.Hour, Now, DueDate)
                    c14.Text = JobDR("category").ToString
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

        Finally
            If DRExc.Connection.State = System.Data.ConnectionState.Open Then
                DRExc.Connection.Close()
                DRExc = Nothing
            End If
        End Try

    End Sub

    Sub AssignUser()
        'Panel1.Visible = True
        hourdiff = DateDiff(DateInterval.Hour, ServStartDate, Now)
        'Response.Write(ProcTime)
        'Response.Write(ServStartDate)
        'Response.Write(ProcStartDate)
        ''Response.Write(ServEndDate)

        Dim strConn As String
        Dim strQuery As String
        Dim scdMins As Integer
        Dim MinsDone As Integer
        Dim MinsAssn As Integer
        Dim MinsPend As Integer
        Dim DirMins As Integer
        Dim LvlNo As Integer
        Dim COLvlNo As Integer
        Dim LvlName As String
        Dim LevelNo As Integer
        Dim LvlNOAssgn As String
        Dim LvlAssn As String
        Dim LvlA(0) As String
        Dim i As Integer
        Dim j As Integer
        i = 0

        LvlAssn = ""
        LvlName = ""
        LvlNOAssgn = ""
        LvlNo = ProLevel.Value
        COLvlNo = 100 + LvlNo
        strConn = System.Configuration.ConfigurationManager.AppSettings("ETSCon")
        Dim Userid As String
        Dim LvlCount As Integer
        Dim Lvl(0) As String

        Dim SelQuery As String
        Dim JoinQuery As String
        SelQuery = ""
        JoinQuery = ""
        Dim Incr As Integer
        Incr = 0
        strQuery = "Select Levelname, LevelNo from tblProductionLevels where contractorid='" & Session("contractorid") & "' and  JobsRouting = 'True' and LevelNo not in ('1073741824', '2147483647') "
        Dim CMUsr As New SqlCommand(strQuery, New SqlConnection(strConn))
        Try
            CMUsr.Connection.Open()
            Dim DRRec As SqlDataReader = CMUsr.ExecuteReader()
            If DRRec.HasRows Then
                While (DRRec.Read)
                    'Response.Write(DRRec("LevelNo"))
                    Incr = Incr + 1


                    LevelNo = DRRec("LevelNo") + 100
                    If i = 0 Then

                        SelQuery = "DN" & Incr & ".DNmins" & Incr & ", "
                        SelQuery = SelQuery & "CH" & Incr & ".CHmins" & Incr & ", "
                        JoinQuery = "LEFT OUTER JOIN (select sum(datediff(s,0,M.duration)) as DNMins" & Incr & ", L.userid from tbltranscriptionstatus L, tbltranscriptionMain M where  datediff(hh, L.Datemodified, getdate()) <= " & hourdiff & "     and L.TranscriptionID= M.TranscriptionID and L.Userlevel in (" & DRRec("LevelNo").ToString & ") and L.status in (Select LevelNo from tblProductionlevels where levelno not in (" & DRRec("LevelNo").ToString & ")) group by L.UserID) DN" & Incr & " ON DN" & Incr & ".userid = U.USerID "
                        JoinQuery = JoinQuery & "LEFT OUTER JOIN (select sum(datediff(s,0,M.duration)) as CHMins" & Incr & ", L.userid from tbltranscriptionlog L, tbltranscriptionMain M where L.TranscriptionID= M.TranscriptionID  and M.status = L.status and M.status in (" & LevelNo & ")  and L.datemodified=(select max(datemodified)from tbltranscriptionlog where TranscriptionID= M.TranscriptionID and M.status = status and status in (" & LevelNo & ") )  group by L.UserID) CH" & Incr & " ON CH" & Incr & ".userid = U.USerID "
                        LDone1.Text = "Finished[" & DRRec("LevelName").ToString & "]</td>"
                        LOut.Text = "Assigned[" & DRRec("LevelName").ToString & "]</td>"
                        LvlAssn = LevelNo
                        LvlNOAssgn = DRRec("LevelNo").ToString
                    Else

                        SelQuery = SelQuery & "DN" & Incr & ".DNmins" & Incr & ", "
                        SelQuery = SelQuery & "CH" & Incr & ".CHmins" & Incr & ", "
                        JoinQuery = JoinQuery & "LEFT OUTER JOIN (select sum(datediff(s,0,M.duration)) as DNMins" & Incr & ", L.userid from tbltranscriptionstatus L, tbltranscriptionMain M where  datediff(hh, L.Datemodified, getdate()) <= " & hourdiff & "  and L.TranscriptionID= M.TranscriptionID and L.Userlevel in (" & DRRec("LevelNo").ToString & ") and L.status in (Select LevelNo from tblProductionlevels where levelno not in (" & DRRec("LevelNo").ToString & ")) group by L.UserID) DN" & Incr & " ON DN" & Incr & ".userid = U.USerID "
                        JoinQuery = JoinQuery & "LEFT OUTER JOIN (select sum(datediff(s,0,M.duration)) as CHMins" & Incr & ", L.userid from tbltranscriptionlog L, tbltranscriptionMain M where L.TranscriptionID= M.TranscriptionID  and M.status = L.status and M.status in (" & LevelNo & ") and L.datemodified=(select max(datemodified)from tbltranscriptionlog where TranscriptionID= M.TranscriptionID and M.status = status and status in (" & LevelNo & ") ) group by L.UserID) CH" & Incr & " ON CH" & Incr & ".userid = U.USerID "
                        ReDim Preserve Lvl(i)
                        ReDim Preserve LvlA(i)
                        LDone1.Text = LDone1.Text & "<td>Finished[" & DRRec("LevelName").ToString & "]</td>"
                        LOut.Text = LOut.Text & "<td>Assigned[" & DRRec("LevelName").ToString & "]</td>"
                        LvlAssn = LvlAssn & "," & LevelNo
                        LvlNOAssgn = LvlNOAssgn & "," & DRRec("LevelNo").ToString
                    End If
                    LvlA(i) = LevelNo
                    Lvl(i) = DRRec("LevelNo")
                    i = i + 1
                End While
            End If
            DRRec.Close()
        Finally
            If CMUsr.Connection.State = ConnectionState.Open Then
                CMUsr.Connection.Close()
            End If
        End Try
        LDone1.Text = LDone1.Text & "<td class=""alt1"">Finished[Total]"
        LOut.Text = LOut.Text & "<td class=""alt1"">Assigned[Total]"

        'CellMdone.ColumnSpan = i + 1
        'CellCout.ColumnSpan = i + 1

        'strQuery = "Select distinct U.FirstName +' ' + U.LastName as uname, U.Username, U.UserID, S.Mins as SCHMins, S.startTime, S.EndTime, L.RecFound from (Select * from tblUsers where (Isdeleted is NULL) or (Isdeleted = 'False')) U, tblUsersLevels UL, tblSchedule S, (Select 'Yes' as RecFound, userID, startdate, enddate from tblLeave) L where   dbo.chkLevel(UL.ProductionLevel, " & LvlNo & ")='True'  and L.startDate <= '" & ProcStartDate & "' and L.endDate >= '" & ProcStartDate & "' and S.ScheduleDate = '" & ProcStartDate & "' and  UL.UserID = U.USerID and U.UserID*=S.USerID  and U.UserID*=L.USerID order by RecFound, uname"
        strQuery = "Select distinct U.FirstName +' ' + U.LastName as uname, U.Username, " & SelQuery & " DN.DNMins, CH.CHMins, DR.Mins as DIRMins, U.UserID, S.Mins as SCHMins, S.startTime, S.EndTime, L.RecFound from (Select * from tblUsers where (Isdeleted is NULL) or (Isdeleted = 'False')) U INNER JOIN tblUsersLevels UL ON  UL.UserID = U.USerID LEFT OUTER JOIN (Select * from tblSchedule where ScheduleDate = '" & ProcStartDate & "' ) S ON U.UserID=S.USerID LEFT OUTER  JOIN (Select 'Yes' as RecFound, userID, startdate, enddate from tblLeave where startDate <= '" & ProcStartDate & "' and endDate >= '" & ProcStartDate & "') L ON U.UserID=L.USerID " & JoinQuery & " LEFT OUTER JOIN (select sum(datediff(s,0,M.duration)) as DNMins, L.userid from tbltranscriptionstatus L, tbltranscriptionMain M where  datediff(hh, L.Datemodified, getdate()) <= " & hourdiff & "    and L.TranscriptionID= M.TranscriptionID  and L.Userlevel in (" & LvlNOAssgn & ") and L.status in (Select LevelNo from tblProductionlevels where levelno not in (" & LvlNOAssgn & ")) group by L.UserID) DN ON DN.userid = U.USerID " & "LEFT OUTER JOIN (select sum(datediff(s,0,M.duration)) as CHMins, L.userid from tbltranscriptionlog L, tbltranscriptionMain M where L.TranscriptionID= M.TranscriptionID  and M.status = L.status and M.status in (" & LvlAssn & ")  and L.datemodified=(select max(datemodified)from tbltranscriptionlog where TranscriptionID= M.TranscriptionID and M.status = status and status in (" & LvlAssn & ") ) group by L.UserID) CH ON CH.userid = U.USerID " & "LEFT OUTER JOIN (select sum(datediff(s,0,M.duration)) as Mins, L.userid from tblUserPrLvlMgmt L, tbltranscriptionMain M where L.PhysicianID= M.DictatorID and  Direct='True' and M.status=L.LevelNo and M.status=" & LvlNo & "  group by L.UserID) DR ON DR.userid = U.USerID " & " where   U.contractorid='" & Session("contractorid") & "' and   U.UserID='" & HUserID.Value & "' and  dbo.chkLevel(UL.ProductionLevel, " & LvlNo & ")='True'   order by schmins desc "

        Dim CMUser As New SqlCommand(strQuery, New SqlConnection(strConn))
        Try
            CMUser.Connection.Open()
            Dim DRRec1 As SqlDataReader = CMUser.ExecuteReader()
            If DRRec1.HasRows Then
                If DRRec1.Read Then
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


                    'CMUser1.Connection.Close()







                    If IsDBNull(DRRec1("CHMins")) Then
                        MinsAssn = 0
                        CL5.Text = 0
                    Else
                        MinsAssn = FormatNumber((DRRec1("CHMins").ToString / 60), 0)
                        CL5.Text = "<a href='JobsRouting.aspx?Userid=" & DRRec1("Userid").ToString & "&ProLevel=" & LvlAssn & " '  Target=_Blank>" & FormatNumber((DRRec1("CHMins").ToString / 60), 0) & "</a>"
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
                    If String.IsNullOrEmpty(STime.Text) Then
                        STime.Text = "&nbsp"
                    End If
                    If String.IsNullOrEmpty(ETime.Text) Then
                        ETime.Text = "&nbsp"
                    End If

                    RW1.Cells.Add(CL1)
                    RW1.Cells.Add(CL2)
                    'CL3.BorderColor = Drawing.Color.DimGray
                    RW1.Cells.Add(CL3)
                    RW1.Cells.Add(STime)
                    RW1.Cells.Add(ETime)
                    For j = 1 To Incr

                        Dim CellD As New TableCell

                        If IsDBNull(DRRec1("DNMins" & j)) Then
                            CellD.Text = 0
                        Else
                            CellD.Text = FormatNumber((DRRec1("DNMins" & j).ToString / 60), 0)
                        End If


                        RW1.Cells.Add(CellD)

                    Next
                    'CL4.BorderColor = Drawing.Color.DimGray
                    RW1.Cells.Add(CL4)
                    For j = 1 To Incr
                        Dim CellC As New TableCell
                        If IsDBNull(DRRec1("CHMins" & j)) Then
                            CellC.Text = 0
                        Else
                            CellC.Text = "<a href='JobsRouting.aspx?Userid=" & DRRec1("Userid").ToString & "&ProLevel=" & LvlA(j - 1) & " '  Target=_Blank>" & FormatNumber((DRRec1("CHMins" & j).ToString / 60), 0) & "</a>"
                        End If
                        RW1.Cells.Add(CellC)
                    Next
                    'CL5.BorderColor = Drawing.Color.DimGray
                    'CL6.BorderColor = Drawing.Color.DimGray
                    'CL7.BorderColor = Drawing.Color.DimGray
                    RW1.Cells.Add(CL5)
                    RW1.Cells.Add(CL6)
                    RW1.Cells.Add(CL7)


                    Table2.Rows.Add(RW1)
                    'Exit Sub
                End If
            End If
            DRRec1.Close()
        Finally
            If CMUser.Connection.State = ConnectionState.Open Then
                CMUser.Connection.Close()
            End If
        End Try
        
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
        realStatus = 100 + CInt(prolevel.value)
        For I = 1 To TotJobs.Value
            TransID = "TransID" & I
            TransID = Request(TransID)
            ArrayL = Split(TransID, "#")

            SelTransID = ArrayL(0)
            If SelTransID <> "" Then
                Dim strConn As String
                strConn = System.Configuration.ConfigurationManager.AppSettings("ETSCon")
                Dim SQLConn As New SqlConnection
                SQLConn.ConnectionString = strConn
                SQLConn.Open()
                Dim Trans1 As SqlTransaction
                Trans1 = SQLConn.BeginTransaction
                Dim RecUp1 As Integer = 0
                Dim RecUp2 As Integer = 0
                Dim RecUp3 As Integer = 0
                Dim RecUp4 As Integer = 0
                Dim strQuery As String
                strQuery = "update tbltranscriptionMain set status=" & realStatus & "  Where TranscriptionID ='" & SelTransID & "'  and status ='" & ProLevel.Value & "' "
                Dim DRExc As New SqlCommand(strQuery, SQLConn)
                DRExc.Transaction = Trans1
                RecUp1 = DRExc.ExecuteNonQuery()

                strQuery = "Insert Into tblTranscriptionLog (userid, TranscriptionID,UserLevel, Status, Datemodified, IP,AssignedBy) Values ('" & HUserID.Value & "', '" & SelTransID & "', " & ProLevel.Value & ", " & realStatus & ",'" & Now & "', '" & Request.UserHostAddress() & "' ,'" & Session("UserID") & "') "
                Dim DRExc1 As New SqlCommand(strQuery, SQLConn)
                DRExc1.Transaction = Trans1
                RecUp2 = DRExc1.ExecuteNonQuery()

                strQuery = "Delete from tblTranscriptionCKDStatus  Where status = '" & ProLevel.Value & "' and TranscriptionID ='" & SelTransID & "' "
                Dim DRExc3 As New SqlCommand(strQuery, SQLConn)
                DRExc3.Transaction = Trans1
                RecUp3 = DRExc3.ExecuteNonQuery()

                strQuery = "Insert Into tblTranscriptionCKDStatus (userid, TranscriptionID, Status, Datemodified) Values ('" & HUserID.Value & "', '" & SelTransID & "', " & realStatus & ",'" & Now & "') "
                Dim DRExc4 As New SqlCommand(strQuery, SQLConn)
                DRExc4.Transaction = Trans1
                RecUp4 = DRExc4.ExecuteNonQuery()
                If RecUp1 > 0 And RecUp2 > 0 And RecUp4 > 0 Then
                    Trans1.Commit()
                    ''Response.Write("<script>alert('Records have been updated successfully')</script>")
                Else
                    Trans1.Rollback()
                    ''Response.Write("<script>alert('Issue in updating records.')</script>")
                End If


                If SQLConn.State = System.Data.ConnectionState.Open Then
                    SQLConn.Close()
                    SQLConn = Nothing
                End If

            End If

        Next

    End Sub

   

  
    
    
    
    
End Class
