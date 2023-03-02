Imports System.Data.SqlClient

Partial Class RoutingTool_Default
    Inherits BasePage

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        ActStatus()
        MTStatus()
    End Sub
  

    Sub MTStatus()
        Dim strConn As String
        Dim t2 As New Table
        t2.Style("width") = "100%"
        t2.BorderWidth = 2
        t2.GridLines = GridLines.Both
        Dim intExcMins As Integer
        intExcMins = 0
        strConn = System.Configuration.ConfigurationManager.AppSettings("ETSCon")
        Dim strQuery As String

        Dim ActName As String
        Dim TransID As String
        Dim SubmitDate As String
        Dim DueDate As String
        Dim Jobnumber As String
        Dim TemplateName As String
        Dim uName As String
        Dim duration As String
        Dim Mins As Double
        Dim dirStatus As String
        Dim DirMins As Integer
        Dim LevelNo As Integer
        Dim LvlAssn As String
        Dim Priority As Boolean
        Dim TAT As String
        Dim i As Integer
        Dim Lvlchk As String = ProLevel.Value
        Dim LvlFound As Boolean = False
        Dim Totmins As Double
        Dim Totjobs As Integer
        Dim Totimins As Double
        Dim Totijobs As Integer

        Totmins = 0
        Totjobs = 0
        Totimins = 0
        Totijobs = 0
        i = 0
        LvlAssn = ""
        strQuery = "Select Levelname, LevelNo from tblProductionLevels where ContractorID='" & Session("ContractorID").ToString & "' and JobsRouting = 'True' and LevelNo not in ('1073741824', '2147483647') "
        Dim CMUsr As New SqlCommand(strQuery, New SqlConnection(strConn))
        Try
            CMUsr.Connection.Open()
            Dim DRRec As SqlDataReader = CMUsr.ExecuteReader()
            If DRRec.HasRows Then
                While (DRRec.Read)
                    'Response.Write(DRRec("LevelNo"))
                    LevelNo = DRRec("LevelNo").ToString + 100
                    If LevelNo = 1 Then
                        LvlFound = True
                        Lvlchk = DRRec("LevelNo").ToString
                    End If
                    If i = 0 Then
                        LvlAssn = LevelNo
                    Else
                        LvlAssn = LvlAssn & "," & LevelNo
                    End If
                    i = i + 1
                End While
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
        Dim strJobnumber As String
        k = 0
        'strQuery = "Select T.TranscriptionID,  T.Jobnumber, T.submitDate, T.DueDate, A.AccountName, T.duration, datediff(s,0,T.duration)/60 as Mins, P.uname, TL.TemplateName from tbltranscriptionmain T LEFT JOIN (Select AccountID, AccountName from tblAccounts) AS A ON A.accountID=T.AccountID  LEFT JOIN (Select PhysicianID, firstname + ' ' + lastname as uname from tblphysicians) AS P ON P.physicianID=T.dictatorID  LEFT JOIN (Select TemplateID, TemplateName from tblTemplates) AS TL ON TL.TemplateID=T.TemplateID where T.submitdate >= '1/9/2008' and T.submitdate < '1/10/2008' and T.status =" & Request("ProLevel") & " and T.AccountID = '" & HAccID.Value & "'  order by DueDate Desc"
        'strQuery = "Select T.TranscriptionID,  T.Jobnumber, T.submitDate, T.DueDate, A.AccountName, T.duration, datediff(s,0,T.duration) as Mins, P.uname, TL.TemplateName, PU.direct from tbltranscriptionmain T LEFT JOIN (Select AccountID, AccountName from tblAccounts) AS A ON A.accountID=T.AccountID  LEFT JOIN (Select PhysicianID, firstname + ' ' + lastname as uname from tblphysicians) AS P ON P.physicianID=T.dictatorID  INNER JOIN (Select userid, PhysicianID, LevelNo, 'Yes' as Direct from tblUserPrLvlMgmt where userid='" & HUserID.Value & "' and LevelNo=" & ProLevel.Value & " and direct='True') AS PU ON PU.physicianID=T.dictatorID  LEFT JOIN (Select TemplateID, TemplateName from tblTemplates) AS TL ON TL.TemplateID=T.TemplateID where T.AccountID='" & HAccID.Value & "' and T.status =" & Request("ProLevel") & " order by  DueDate  Asc"
        'strQuery = "Select distinct T.TranscriptionID,  T.Jobnumber, T.TAT, T.Priority, T.submitDate, T.DueDate, A.AccountName, T.duration, datediff(s,0,T.duration) as Mins, P.uname, TL.TemplateName, PL.LevelName, U.username, PU.direct from tbltranscriptionmain T INNER JOIN (select * from tbltranscriptionlog order by datemodified) AS L  ON L.TranscriptionID= T.TranscriptionID and T.status = L.status LEFT JOIN (Select userid, username from tblusers) AS U ON U.Userid=L.UserID LEFT JOIN tblProductionLevels PL ON PL.LevelNo = (T.status-100) LEFT JOIN (Select AccountID, AccountName from tblAccounts) AS A ON A.accountID=T.AccountID  LEFT JOIN (Select PhysicianID, firstname + ' ' + lastname as uname from tblphysicians) AS P ON P.physicianID=T.dictatorID     LEFT JOIN (Select userid, PhysicianID, LevelNo, 'Yes' as Direct from tblUserPrLvlMgmt where LevelNo in (" & ProLevel.Value & ") and direct='True') AS PU ON PU.userid=L.UserID and PU.physicianID=T.dictatorID   LEFT JOIN (Select TemplateID, TemplateName from tblTemplates) AS TL ON TL.TemplateID=T.TemplateID where  T.AccountID = '" & HAccID.Value & "' and T.status in (" & ProLevel.Value & ") order by  DueDate  Asc"
        '        strQuery = "Select distinct T.TranscriptionID,  T.Jobnumber, T.TAT, T.Priority, T.submitDate, T.DueDate, A.AccountName, T.duration, datediff(s,0,T.duration) as Mins, P.uname, TL.TemplateName, PL.LevelName, U.username, PU.direct, l.datemodified from tbltranscriptionmain T LEFT JOIN tblTranscriptionLog  L  ON L.TranscriptionID= T.TranscriptionID and T.status = L.status LEFT JOIN (Select userid, username from tblusers) AS U ON U.Userid=L.UserID LEFT JOIN tblProductionLevels PL ON PL.LevelNo = (T.status-100) LEFT JOIN (Select AccountID, AccountName from tblAccounts) AS A ON A.accountID=T.AccountID  LEFT JOIN (Select PhysicianID, firstname + ' ' + lastname as uname from tblphysicians) AS P ON P.physicianID=T.dictatorID     LEFT JOIN (Select userid, PhysicianID, LevelNo, 'Yes' as Direct from tblUserPrLvlMgmt where LevelNo in (" & Lvlchk & ") and direct='True') AS PU ON PU.userid=L.UserID and PU.physicianID=T.dictatorID   LEFT JOIN (Select TemplateID, TemplateName from tblTemplates) AS TL ON TL.TemplateID=T.TemplateID where  T.AccountID = '" & HAccID.Value & "' and T.status in (" & ProLevel.Value & ") order by  jobnumber, l.datemodified  desc"
        'strQuery = "Select mtname, username,  (isnull(sum(Mins),0)/60) as mins, Count(Jobnumber) as RecCount from (Select distinct T.TranscriptionID,  T.Jobnumber, T.TAT, T.Priority, T.submitDate, T.DueDate, A.AccountName, A.AccountID, T.duration, datediff(s,0,T.duration) as Mins, P.uname, TL.TemplateName, PL.LevelName, U.username, U.mtname, PU.direct, l.datemodified from tbltranscriptionmain T INNER JOIN tblTranscriptionLog  L  ON L.TranscriptionID= T.TranscriptionID and T.status = L.status LEFT JOIN (Select Firstname + ' ' + Lastname as mtname,userid, username from tblusers) AS U ON U.Userid=L.UserID LEFT JOIN tblProductionLevels PL ON PL.LevelNo = '1' LEFT JOIN (Select AccountID, AccountName from tblAccounts) AS A ON A.accountID=T.AccountID  LEFT JOIN (Select PhysicianID, firstname + ' ' + lastname as uname from tblphysicians) AS P ON P.physicianID=T.dictatorID     LEFT JOIN (Select userid, PhysicianID, LevelNo, Direct from tblUserPrLvlMgmt where LevelNo ='1') AS PU ON PU.userid=L.UserID and PU.physicianID=T.dictatorID   LEFT JOIN (Select TemplateID, TemplateName from tblTemplates) AS TL ON TL.TemplateID=T.TemplateID where T.status = '101' and L.datemodified=(select max(datemodified) from tbltranscriptionlog where TranscriptionID= T.TranscriptionID and (PU.direct is null  or direct='false' ) and T.status = status and T.status in (101)) ) T group by mtname, username"
        strQuery = "select * from (Select mtname, username,userid,  (isnull(sum(Mins),0)) as mins, Count(Jobnumber) as RecCount,sum(indmins)  as indmins, sum(indjobs) as indjobs from (Select distinct T.TranscriptionID, isnull(PU.indirect,1) as indjobs, isnull(PU.indirect,datediff(s,0,T.duration)) as indmins, T.Jobnumber, T.TAT, T.Priority, T.submitDate, T.DueDate, A.AccountName, A.AccountID, T.duration, datediff(s,0,T.duration) as Mins, P.uname, TL.TemplateName, PL.LevelName, U.username, U.mtname, PU.direct, l.datemodified, U.userid from tbltranscriptionmain T INNER JOIN tblTranscriptionLog  L  ON L.TranscriptionID= T.TranscriptionID and T.status = L.status LEFT JOIN (Select Firstname + ' ' + Lastname as mtname,userid, username from tblusers  where ContractorID='" & Session("ContractorID") & "') AS U ON U.Userid=L.UserID LEFT JOIN tblProductionLevels PL ON PL.LevelNo = '1'  LEFT JOIN (Select AccountID, AccountName from tblAccounts  where ContractorID='" & Session("ContractorID") & "') AS A ON A.accountID=T.AccountID  LEFT JOIN (Select PhysicianID, firstname + ' ' + lastname as uname from tblphysicians) AS P ON P.physicianID=T.dictatorID     LEFT JOIN (Select userid, PhysicianID, LevelNo, 0 as inDirect, direct from tblUserPrLvlMgmt where LevelNo ='1'  and direct='True' ) AS PU ON PU.userid=L.UserID and PU.physicianID=T.dictatorID   LEFT JOIN (Select TemplateID, TemplateName from tblTemplates) AS TL ON TL.TemplateID=T.TemplateID where T.status = '101' and L.datemodified=(select max(datemodified) from tbltranscriptionlog where TranscriptionID= T.TranscriptionID   and T.accountid not in ('1B410F55-42D0-4329-83B3-7FBBD04A5196', 'DEA2179E-530E-4063-A37D-6754AF5C3B73')  and T.status = status and T.status in (101)) and T.ContractorID='" & Session("ContractorID") & "' ) T group by mtname, username, userid) D where indjobs > 0"
        'Response.Write(strQuery)

        Dim DRExc As New SqlCommand(strQuery, New SqlConnection(strConn))
        Try
            DRExc.Connection.Open()
            Dim JobDR As SqlDataReader = DRExc.ExecuteReader()
            If JobDR.HasRows Then
                While (JobDR.Read)
                    Dim c1 As New TableCell
                    Dim c2 As New TableCell
                    Dim c3 As New TableCell
                    Dim c4 As New TableCell
                    Dim c5 As New TableCell
                    Dim c6 As New TableCell
                    Dim r As New TableRow
                    c1.Text = JobDR("MTName").ToString
                    c6.Text = JobDR("userName").ToString
                    c1.HorizontalAlign = HorizontalAlign.Left
                    Mins = CDbl(JobDR("Mins").ToString / 60)
                    Totmins = Totmins + Mins
                    Totjobs = Totjobs + CInt(JobDR("reccount").ToString)
                    Totimins = Totimins + CDbl(JobDR("indMins").ToString / 60)
                    Totijobs = Totijobs + CInt(JobDR("indjobs").ToString)
                    c3.Text = CInt(JobDR("Mins").ToString / 60)
                    c2.Text = CInt(JobDR("reccount").ToString)
                    c5.Text = CInt(JobDR("indMins").ToString / 60)
                    If CInt(JobDR("indJobs").ToString) > 0 Then
                        c4.Text = "<a href='indirectjobs.aspx?Userid=" & JobDR("Userid").ToString & "'  Target=_Blank>" & CInt(JobDR("indJobs").ToString) & "</a>"
                    Else
                        c4.Text = CInt(JobDR("indJobs").ToString)
                    End If
                    '            c4.Text = CInt(JobDR("indJobs").ToString)
                    r.Cells.Add(c1)
                    r.Cells.Add(c6)
                    r.Cells.Add(c2)
                    r.Cells.Add(c3)
                    r.Cells.Add(c4)
                    r.Cells.Add(c5)
                    r.Font.Size = "9"
                    r.Font.Italic = False

                    Table2.Rows.Add(r)

                    'Response.Write(CB1.Checked)


                    'Else
                    '    Exit While
                    'End If
                End While
                'Response.Write(intExcMins)

                LblTMins1.Text = CInt(Totmins)
                lblTotJobs1.Text = Totjobs
                LblTiMins1.Text = CInt(Totimins)
                lblTotiJobs1.Text = Totijobs
            Else

                LblTMins1.Text = 0
                lblTotJobs1.Text = 0
                LblTiMins1.Text = 0
                lblTotiJobs1.Text = 0

            End If
            JobDR.Close()
        Finally
            If DRExc.Connection.State = System.Data.ConnectionState.Open Then
                DRExc.Connection.Close()
                DRExc = Nothing
            End If
        End Try

    End Sub

    Sub ActStatus()
        Dim strConn As String
        Dim t2 As New Table
        t2.Style("width") = "100%"
        t2.BorderWidth = 2
        t2.GridLines = GridLines.Both
        Dim intExcMins As Integer
        intExcMins = 0
        strConn = System.Configuration.ConfigurationManager.AppSettings("ETSCon")
        Dim strQuery As String

        Dim ActName As String
        Dim TransID As String
        Dim SubmitDate As String
        Dim DueDate As String
        Dim Jobnumber As String
        Dim TemplateName As String
        Dim uName As String
        Dim duration As String
        Dim Mins As Double
        Dim dirStatus As String
        Dim DirMins As Integer
        Dim LevelNo As Integer
        Dim LvlAssn As String
        Dim Priority As Boolean
        Dim TAT As String
        Dim i As Integer
        Dim Lvlchk As String = ProLevel.Value
        Dim LvlFound As Boolean = False
        Dim Totmins As Double
        Dim Totjobs As Integer
        Totmins = 0
        Totjobs = 0
        i = 0
        LvlAssn = ""
        strQuery = "Select Levelname, LevelNo from tblProductionLevels where  ContractorID='" & Session("ContractorID").ToString & "' and JobsRouting = 'True' and LevelNo not in ('1073741824', '2147483647') "
        Dim CMUsr As New SqlCommand(strQuery, New SqlConnection(strConn))
        Try
            CMUsr.Connection.Open()
            Dim DRRec As SqlDataReader = CMUsr.ExecuteReader()
            If DRRec.HasRows Then
                While (DRRec.Read)
                    'Response.Write(DRRec("LevelNo"))
                    LevelNo = DRRec("LevelNo").ToString + 100
                    If LevelNo = 1 Then
                        LvlFound = True
                        Lvlchk = DRRec("LevelNo").ToString
                    End If
                    If i = 0 Then
                        LvlAssn = LevelNo
                    Else
                        LvlAssn = LvlAssn & "," & LevelNo
                    End If
                    i = i + 1
                End While
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
        Dim strJobnumber As String
        k = 0
        'strQuery = "Select T.TranscriptionID,  T.Jobnumber, T.submitDate, T.DueDate, A.AccountName, T.duration, datediff(s,0,T.duration)/60 as Mins, P.uname, TL.TemplateName from tbltranscriptionmain T LEFT JOIN (Select AccountID, AccountName from tblAccounts) AS A ON A.accountID=T.AccountID  LEFT JOIN (Select PhysicianID, firstname + ' ' + lastname as uname from tblphysicians) AS P ON P.physicianID=T.dictatorID  LEFT JOIN (Select TemplateID, TemplateName from tblTemplates) AS TL ON TL.TemplateID=T.TemplateID where T.submitdate >= '1/9/2008' and T.submitdate < '1/10/2008' and T.status =" & Request("ProLevel") & " and T.AccountID = '" & HAccID.Value & "'  order by DueDate Desc"
        'strQuery = "Select T.TranscriptionID,  T.Jobnumber, T.submitDate, T.DueDate, A.AccountName, T.duration, datediff(s,0,T.duration) as Mins, P.uname, TL.TemplateName, PU.direct from tbltranscriptionmain T LEFT JOIN (Select AccountID, AccountName from tblAccounts) AS A ON A.accountID=T.AccountID  LEFT JOIN (Select PhysicianID, firstname + ' ' + lastname as uname from tblphysicians) AS P ON P.physicianID=T.dictatorID  INNER JOIN (Select userid, PhysicianID, LevelNo, 'Yes' as Direct from tblUserPrLvlMgmt where userid='" & HUserID.Value & "' and LevelNo=" & ProLevel.Value & " and direct='True') AS PU ON PU.physicianID=T.dictatorID  LEFT JOIN (Select TemplateID, TemplateName from tblTemplates) AS TL ON TL.TemplateID=T.TemplateID where T.AccountID='" & HAccID.Value & "' and T.status =" & Request("ProLevel") & " order by  DueDate  Asc"
        'strQuery = "Select distinct T.TranscriptionID,  T.Jobnumber, T.TAT, T.Priority, T.submitDate, T.DueDate, A.AccountName, T.duration, datediff(s,0,T.duration) as Mins, P.uname, TL.TemplateName, PL.LevelName, U.username, PU.direct from tbltranscriptionmain T INNER JOIN (select * from tbltranscriptionlog order by datemodified) AS L  ON L.TranscriptionID= T.TranscriptionID and T.status = L.status LEFT JOIN (Select userid, username from tblusers) AS U ON U.Userid=L.UserID LEFT JOIN tblProductionLevels PL ON PL.LevelNo = (T.status-100) LEFT JOIN (Select AccountID, AccountName from tblAccounts) AS A ON A.accountID=T.AccountID  LEFT JOIN (Select PhysicianID, firstname + ' ' + lastname as uname from tblphysicians) AS P ON P.physicianID=T.dictatorID     LEFT JOIN (Select userid, PhysicianID, LevelNo, 'Yes' as Direct from tblUserPrLvlMgmt where LevelNo in (" & ProLevel.Value & ") and direct='True') AS PU ON PU.userid=L.UserID and PU.physicianID=T.dictatorID   LEFT JOIN (Select TemplateID, TemplateName from tblTemplates) AS TL ON TL.TemplateID=T.TemplateID where  T.AccountID = '" & HAccID.Value & "' and T.status in (" & ProLevel.Value & ") order by  DueDate  Asc"
        '        strQuery = "Select distinct T.TranscriptionID,  T.Jobnumber, T.TAT, T.Priority, T.submitDate, T.DueDate, A.AccountName, T.duration, datediff(s,0,T.duration) as Mins, P.uname, TL.TemplateName, PL.LevelName, U.username, PU.direct, l.datemodified from tbltranscriptionmain T LEFT JOIN tblTranscriptionLog  L  ON L.TranscriptionID= T.TranscriptionID and T.status = L.status LEFT JOIN (Select userid, username from tblusers) AS U ON U.Userid=L.UserID LEFT JOIN tblProductionLevels PL ON PL.LevelNo = (T.status-100) LEFT JOIN (Select AccountID, AccountName from tblAccounts) AS A ON A.accountID=T.AccountID  LEFT JOIN (Select PhysicianID, firstname + ' ' + lastname as uname from tblphysicians) AS P ON P.physicianID=T.dictatorID     LEFT JOIN (Select userid, PhysicianID, LevelNo, 'Yes' as Direct from tblUserPrLvlMgmt where LevelNo in (" & Lvlchk & ") and direct='True') AS PU ON PU.userid=L.UserID and PU.physicianID=T.dictatorID   LEFT JOIN (Select TemplateID, TemplateName from tblTemplates) AS TL ON TL.TemplateID=T.TemplateID where  T.AccountID = '" & HAccID.Value & "' and T.status in (" & ProLevel.Value & ") order by  jobnumber, l.datemodified  desc"
        strQuery = "Select Accountname, AccountID, (isnull(sum(Mins),0)) as mins, Count(Jobnumber) as RecCount from (Select distinct T.TranscriptionID,  T.Jobnumber, T.TAT, T.Priority, T.submitDate, T.DueDate, A.AccountName, A.AccountID, T.duration, datediff(s,0,T.duration) as Mins, P.uname, TL.TemplateName, PL.LevelName, U.username, PU.direct, l.datemodified from tbltranscriptionmain T INNER JOIN tblTranscriptionLog  L  ON L.TranscriptionID= T.TranscriptionID and T.status = L.status LEFT JOIN (Select userid, username from tblusers where ContractorID='" & Session("ContractorID") & "') AS U ON U.Userid=L.UserID LEFT JOIN tblProductionLevels PL ON PL.LevelNo = '1' LEFT JOIN (Select AccountID, AccountName from tblAccounts where ContractorID='" & Session("ContractorID") & "') AS A ON A.accountID=T.AccountID  LEFT JOIN (Select PhysicianID, firstname + ' ' + lastname as uname from tblphysicians) AS P ON P.physicianID=T.dictatorID     LEFT JOIN (Select userid, PhysicianID, LevelNo, Direct from tblUserPrLvlMgmt where LevelNo ='1' and direct='True') AS PU ON PU.userid=L.UserID and PU.physicianID=T.dictatorID   LEFT JOIN (Select TemplateID, TemplateName from tblTemplates) AS TL ON TL.TemplateID=T.TemplateID where T.status = '101' and L.datemodified=(select max(datemodified) from tbltranscriptionlog where TranscriptionID= T.TranscriptionID and T.status = status and T.status in (101) and PL.ContractorID='" & Session("ContractorID").ToString & "' and T.ContractorID='" & Session("ContractorID") & "')  and T.accountid not in ('1B410F55-42D0-4329-83B3-7FBBD04A5196', 'DEA2179E-530E-4063-A37D-6754AF5C3B73') and (PU.direct ='False' or PU.direct is null) ) T group by Accountname, AccountID"
        Response.Write(strQuery)
        Response.Write("<BR>Workgroup ID : " & Session("WorkgroupID"))

        Dim DRExc As New SqlCommand(strQuery, New SqlConnection(strConn))
        Try
            DRExc.Connection.Open()
            Dim JobDR As SqlDataReader = DRExc.ExecuteReader()
            If JobDR.HasRows Then
                While (JobDR.Read)
                    Dim c1 As New TableCell
                    Dim c2 As New TableCell
                    Dim c3 As New TableCell
                    Dim r As New TableRow
                    c1.Text = JobDR("AccountName").ToString
                    c1.HorizontalAlign = HorizontalAlign.Left
                    Mins = CDbl(JobDR("Mins").ToString / 60)
                    'Response.Write(JobDR("Mins").ToString)
                    Totmins = Totmins + Mins
                    Totjobs = Totjobs + CInt(JobDR("reccount").ToString)
                    If CInt(JobDR("reccount").ToString) > 0 Then
                        c2.Text = "<a href='indirectjobsA.aspx?accountid=" & JobDR("accountid").ToString & "'  Target=_Blank>" & CInt(JobDR("reccount").ToString) & "</a>"
                    Else
                        c2.Text = CInt(JobDR("reccount").ToString)
                    End If

                    c3.Text = CInt(JobDR("Mins").ToString / 60)
                    'c2.Text = CInt(JobDR("reccount").ToString)
                    r.Cells.Add(c1)
                    r.Cells.Add(c2)
                    r.Cells.Add(c3)
                    r.Font.Size = "9"
                    r.Font.Italic = False

                    Table4.Rows.Add(r)

                    'Response.Write(CB1.Checked)


                    'Else
                    '    Exit While
                    'End If
                End While
                'Response.Write(intExcMins)

                LblTMins.Text = CInt(Totmins)
                lblTotJobs.Text = Totjobs
            Else

                LblTMins.Text = 0
                lblTotJobs.Text = 0


            End If
            JobDR.Close()
        Finally
            If DRExc.Connection.State = System.Data.ConnectionState.Open Then
                DRExc.Connection.Close()
                DRExc = Nothing
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




End Class
