Imports System.Data.SqlClient
Imports System.Data
Partial Class RoutingTool_Default
    Inherits BasePage
    Private hourdiff As Integer
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        hourdiff = DateDiff(DateInterval.Hour, ServStartDate, Now)
        'Response.Write(ServStartDate & "#" & hourdiff)
        Dim strConn As String
        Dim t2 As New Table
        t2.Style("width") = "100%"
        t2.BorderWidth = 2
        t2.GridLines = GridLines.Both
        Dim ProtocolMins As Integer
        Dim DoneMins As Integer
        Dim AwaitingEntry As Integer
        Dim CheckedOut As Integer
        Dim NotFinished As Integer

        Dim PendingMins As Integer

        strConn = System.Configuration.ConfigurationManager.AppSettings("ETSCon")

        Dim strQuery As String

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



        'strQuery = "Select DISTINCT  A.AccountID, A.Accountname, A.ProtocolMins,   T2.AwaitingEntry, T3.Checkedout, T4.DoneMins, T6.pendingmins from tblaccounts AS A   LEFT JOIN (Select TM.AccountID, sum(datediff(s,0,TM.duration)/60) as AwaitingEntry  from  tbltranscriptionmain TM, tblProductionLevels L  where  TM.status=L.LevelNo and TM.status not in(1073741824)  and submitdate >= '1/9/2008' and submitdate < '1/10/2008' and (L.ForcedRouting is null or L.ForcedRouting = 'False') group by AccountID) AS T2 ON T2.accountid = A.accountid LEFT JOIN (Select TM.AccountID, sum(datediff(s,0,TM.duration)/60) as CheckedOut  from  tbltranscriptionmain TM   where  TM.status not in (Select LevelNo from tblProductionLevels)  and TM.status not in(1073741824)  and submitdate >= '1/9/2008' and submitdate < '1/10/2008' group by AccountID) AS T3 ON T3.accountid = A.accountid Left Join  (Select AccountID, sum(datediff(s,0,duration)/60) as DoneMins  from  tbltranscriptionmain  where   status in(1073741824)  and submitdate >= '1/9/2008' and submitdate < '1/10/2008' group by AccountID) AS T4 ON T4.accountid = A.accountid  Left Join  (Select AccountID, sum(datediff(s,0,duration)/60) as PendingMins  from tbltranscriptionmain  where   status not in(1073741824)  and submitdate < '1/9/2008'  group by AccountID) AS T6 ON T6.accountid = A.accountid  order by Accountname"
        'strQuery = "Select DISTINCT  A.AccountID, A.Accountname, A.ProtocolMins,  T7.NotRouted, T7.LevelName, T2.AwaitingEntry, T3.Checkedout, T4.DoneMins from tblaccounts AS A   LEFT JOIN (Select TM.AccountID, sum(datediff(s,0,TM.duration)) as AwaitingEntry  from  tbltranscriptionmain TM, tblProductionLevels L  where  TM.status=L.LevelNo and TM.status not in(1073741824)  group by AccountID) AS T2 ON T2.accountid = A.accountid LEFT JOIN (Select TM.AccountID, L.Levelname, sum(datediff(s,0,TM.duration)) as NotRouted  from  tbltranscriptionmain TM, tblProductionLevels L  where  TM.status=L.LevelNo and TM.status in(" & Request("ProLeveL") & ") group by AccountID, L.Levelname) AS T7 ON T7.accountid = A.accountid LEFT JOIN (Select TM.AccountID, sum(datediff(s,0,TM.duration)) as CheckedOut  from  tbltranscriptionmain TM   where  TM.status not in (Select LevelNo from tblProductionLevels)  and TM.status not in(1073741824)  group by AccountID) AS T3 ON T3.accountid = A.accountid Left Join  (Select AccountID, sum(datediff(s,0,duration)) as DoneMins  from  tbltranscriptionmain  where   status in(1073741824)  group by AccountID) AS T4 ON T4.accountid = A.accountid  where A.AccountID='" & Request("AccID") & "' order by Accountname"
        strQuery = "Select DISTINCT  A.AccountID, A.Accountname, A.ProtocolMins,  T7.NotRouted, T7.LevelName, T2.AwaitingEntry, T3.Checkedout, T4.DoneMins from tblaccounts AS A   LEFT JOIN (Select TM.AccountID, sum(datediff(s,0,TM.duration)) as AwaitingEntry  from  tbltranscriptionmain TM, tblProductionLevels L  where  TM.status=L.LevelNo and TM.status not in(1073741824)    and TM.submitdate > '3/1/2009'   group by AccountID) AS T2 ON T2.accountid = A.accountid LEFT JOIN (Select TM.AccountID, L.Levelname, sum(datediff(s,0,TM.duration)) as NotRouted  from  tbltranscriptionmain TM, tblProductionLevels L  where  TM.status=L.LevelNo and TM.status in(" & Request("ProLeveL") & ")    and TM.submitdate > '3/1/2009'    group by AccountID, L.Levelname) AS T7 ON T7.accountid = A.accountid LEFT JOIN (Select TM.AccountID, sum(datediff(s,0,TM.duration)) as CheckedOut  from  tbltranscriptionmain TM   where  TM.status not in (Select LevelNo from tblProductionLevels)  and TM.status not in(1073741824)   and TM.submitdate > '3/1/2009'  group by AccountID) AS T3 ON T3.accountid = A.accountid Left Join  (Select AccountID, sum(datediff(s,0,duration)) as DoneMins  from  tbltranscriptionmain  where   status in(1073741824)  and submitdate > '3/1/2009' group by AccountID) AS T4 ON T4.accountid = A.accountid  where A.AccountID='" & Request("AccID") & "' order by Accountname"
        'Response.Write(strQuery)
        'Response.End()
        Dim SQLCmd As New SqlCommand(strQuery, New SqlConnection(strConn))
        Try
            SQLCmd.Connection.Open()
            Dim DRRec As SqlDataReader = SQLCmd.ExecuteReader()
            If DRRec.HasRows Then
                While (DRRec.Read)

                    NotFinished = 0
                    ' Dim Actname As New String
                    LblActName.Text = DRRec("AccountName").ToString

                    If IsDBNull(DRRec("ProtocolMins")) Then
                        ProtocolMins = 0
                    Else
                        ProtocolMins = DRRec("ProtocolMins").ToString
                    End If

                    'If IsDBNull(DRRec("PFreshMins")) Then
                    '    PFreshMins = 0
                    'Else
                    '    PFreshMins = DRRec("PFreshMins").ToString
                    'End If

                    If IsDBNull(DRRec("AwaitingEntry")) Then
                        AwaitingEntry = 0
                    Else
                        AwaitingEntry = FormatNumber((DRRec("AwaitingEntry").ToString / 60), 0)
                    End If


                    'If IsDBNull(DRRec("PNotFinished")) Then
                    '    PNotFinishedMins = 0
                    'Else
                    '    PNotFinishedMins = DRRec("PNotFinished").ToString
                    'End If


                    If IsDBNull(DRRec("CheckedOut")) Then
                        CheckedOut = 0
                    Else
                        CheckedOut = FormatNumber((DRRec("CheckedOut").ToString / 60), 0)
                    End If

                    'If IsDBNull(DRRec("PDoneMins")) Then
                    '    PDoneMins = 0
                    'Else
                    '    PDoneMins = DRRec("PDoneMins").ToString
                    'End If

                    If IsDBNull(DRRec("DoneMins")) Then
                        DoneMins = 0
                    Else
                        DoneMins = FormatNumber((DRRec("DoneMins").ToString / 60), 0)
                    End If
                    Dim NotRouted As Integer

                    If IsDBNull(DRRec("NotRouted")) Then
                        NotRouted = 0
                    Else
                        NotRouted = FormatNumber((DRRec("NotRouted").ToString / 60), 0)
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
                    Lblstatus.Text = DRRec("Levelname").ToString

                End While
            End If
            DRRec.Close()
        Finally
            If SQLCmd.Connection.State = ConnectionState.Open Then
                SQLCmd.Connection.Close()
            End If
        End Try

        


        If Not IsPostBack Then
            Table4.Visible = False
            lblDsp.Text = "Show All Users"
            AssignUser()
        End If



    End Sub
    Sub AssignUser()
        hourdiff = DateDiff(DateInterval.Hour, ServStartDate, Now)
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
        LvlNo = Request("ProLevel")
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
        strQuery = "Select Levelname, LevelNo from tblProductionLevels where contractorid='" & Session("contractorid") & "' and JobsRouting = 'True' and LevelNo not in ('1073741824', '2147483647') "
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
                        SelQuery = SelQuery & "ADN" & Incr & ".ADNmins" & Incr & ", "
                        SelQuery = SelQuery & "ACH" & Incr & ".ACHmins" & Incr & ", "
                        JoinQuery = "LEFT OUTER JOIN (select sum(datediff(s,0,M.duration)) as DNMins" & Incr & ", L.userid from tbltranscriptionstatus L, tbltranscriptionMain M where datediff(hh, L.Datemodified, getdate()) <= " & hourdiff & " and L.TranscriptionID= M.TranscriptionID and L.Userlevel in (" & DRRec("LevelNo").ToString & ") and L.status in (Select LevelNo from tblProductionlevels where levelno not in (" & DRRec("LevelNo").ToString & ")) group by L.UserID) DN" & Incr & " ON DN" & Incr & ".userid = U.USerID "
                        JoinQuery = JoinQuery & "LEFT OUTER JOIN (select sum(datediff(s,0,M.duration)) as CHMins" & Incr & ", L.userid from tbltranscriptionlog L, tbltranscriptionMain M where L.TranscriptionID= M.TranscriptionID  and M.status = L.status and M.status in (" & LevelNo & ")  and L.datemodified=(select max(datemodified)from tbltranscriptionlog where TranscriptionID= M.TranscriptionID and M.status = status and status in (" & LevelNo & ") ) group by L.UserID) CH" & Incr & " ON CH" & Incr & ".userid = U.USerID "
                        JoinQuery = JoinQuery & "LEFT OUTER JOIN (select sum(datediff(s,0,M.duration)) as ADNMins" & Incr & ", L.userid from tbltranscriptionstatus L, tbltranscriptionMain M where M.AccountID = '" & Request("ACcID") & "' and datediff(hh, L.Datemodified, getdate()) <= " & hourdiff & " and L.TranscriptionID= M.TranscriptionID and L.Userlevel in (" & DRRec("LevelNo").ToString & ") and L.status in (Select LevelNo from tblProductionlevels where levelno not in (" & DRRec("LevelNo").ToString & ")) group by L.UserID) ADN" & Incr & " ON ADN" & Incr & ".userid = U.USerID "
                        JoinQuery = JoinQuery & "LEFT OUTER JOIN (select sum(datediff(s,0,M.duration)) as ACHMins" & Incr & ", L.userid from tbltranscriptionlog L, tbltranscriptionMain M where M.AccountID = '" & Request("ACcID") & "' and  L.TranscriptionID= M.TranscriptionID  and M.status = L.status and M.status in (" & LevelNo & ")  and L.datemodified=(select max(datemodified)from tbltranscriptionlog where TranscriptionID= M.TranscriptionID and M.status = status and status in (" & LevelNo & ") ) group by L.UserID) ACH" & Incr & " ON ACH" & Incr & ".userid = U.USerID "
                        LDone.Text = "Finished[" & DRRec("LevelName").ToString & "]</td>"
                        LOut.Text = "Assigned[" & DRRec("LevelName").ToString & "]</td>"
                        LvlAssn = LevelNo
                        LvlNOAssgn = DRRec("LevelNo").ToString
                    Else

                        SelQuery = SelQuery & "DN" & Incr & ".DNmins" & Incr & ", "
                        SelQuery = SelQuery & "CH" & Incr & ".CHmins" & Incr & ", "
                        SelQuery = SelQuery & "ADN" & Incr & ".ADNmins" & Incr & ", "
                        SelQuery = SelQuery & "ACH" & Incr & ".ACHmins" & Incr & ", "
                        JoinQuery = JoinQuery & "LEFT OUTER JOIN (select sum(datediff(s,0,M.duration)) as DNMins" & Incr & ", L.userid from tbltranscriptionstatus L, tbltranscriptionMain M where  datediff(hh, L.Datemodified, getdate()) <= " & hourdiff & " and L.TranscriptionID= M.TranscriptionID and L.Userlevel in (" & DRRec("LevelNo").ToString & ") and L.status in (Select LevelNo from tblProductionlevels where levelno not in (" & DRRec("LevelNo").ToString & ")) group by L.UserID) DN" & Incr & " ON DN" & Incr & ".userid = U.USerID "
                        JoinQuery = JoinQuery & "LEFT OUTER JOIN (select sum(datediff(s,0,M.duration)) as CHMins" & Incr & ", L.userid from tbltranscriptionlog L, tbltranscriptionMain M where L.TranscriptionID= M.TranscriptionID  and M.status = L.status and M.status in (" & LevelNo & ")  and L.datemodified=(select max(datemodified)from tbltranscriptionlog where TranscriptionID= M.TranscriptionID and M.status = status and status in (" & LevelNo & ") ) group by L.UserID) CH" & Incr & " ON CH" & Incr & ".userid = U.USerID "
                        JoinQuery = JoinQuery & "LEFT OUTER JOIN (select sum(datediff(s,0,M.duration)) as ADNMins" & Incr & ", L.userid from tbltranscriptionstatus L, tbltranscriptionMain M where M.AccountID = '" & Request("ACcID") & "' and datediff(hh, L.Datemodified, getdate()) <= " & hourdiff & " and L.TranscriptionID= M.TranscriptionID and L.Userlevel in (" & DRRec("LevelNo").ToString & ") and L.status in (Select LevelNo from tblProductionlevels where levelno not in (" & DRRec("LevelNo").ToString & ")) group by L.UserID) ADN" & Incr & " ON ADN" & Incr & ".userid = U.USerID "
                        JoinQuery = JoinQuery & "LEFT OUTER JOIN (select sum(datediff(s,0,M.duration)) as ACHMins" & Incr & ", L.userid from tbltranscriptionlog L, tbltranscriptionMain M where M.AccountID = '" & Request("ACcID") & "' and  L.TranscriptionID= M.TranscriptionID  and M.status = L.status and M.status in (" & LevelNo & ")  and L.datemodified=(select max(datemodified)from tbltranscriptionlog where TranscriptionID= M.TranscriptionID and M.status = status and status in (" & LevelNo & ") ) group by L.UserID) ACH" & Incr & " ON ACH" & Incr & ".userid = U.USerID "
                        ReDim Preserve Lvl(i)
                        ReDim Preserve LvlA(i)
                        LDone.Text = LDone.Text & "<td>Finished[" & DRRec("LevelName").ToString & "]</td>"
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
        LDone.Text = LDone.Text & "<td>Finished[Total]"
        LOut.Text = LOut.Text & "<td>Assigned[Total]"
        'Response.Write(LDone.Text)
        'CellMdone.ColumnSpan = i + 1
        'CellCout.ColumnSpan = i + 1

        'strQuery = "Select distinct U.FirstName +' ' + U.LastName as uname, U.Username, U.UserID, S.Mins as SCHMins, S.startTime, S.EndTime, L.RecFound from (Select * from tblUsers where (Isdeleted is NULL) or (Isdeleted = 'False')) U, tblUsersLevels UL, tblSchedule S, (Select 'Yes' as RecFound, userID, startdate, enddate from tblLeave) L where   dbo.chkLevel(UL.ProductionLevel, " & LvlNo & ")='True'  and L.startDate <= '" & ProcStartDate & "' and L.endDate >= '" & ProcStartDate & "' and S.ScheduleDate = '" & ProcStartDate & "' and  UL.UserID = U.USerID and U.UserID*=S.USerID  and U.UserID*=L.USerID order by RecFound, uname"
        ' If LvlNo = 1 Then
        'strQuery = "Select distinct U.FirstName +' ' + U.LastName as uname, U.Username, " & SelQuery & " DN.DNMins, CH.CHMins, DR.Mins as DIRMins, U.UserID, S.Mins as SCHMins, S.startTime, S.EndTime, L.RecFound, L.TypeOfLeave from (Select * from tblUsers where  designationid in ('c5a08061-bd27-4f28-afc5-fed6979d2fd3','b8e8bbc8-e865-4e28-b720-633b2f152568','124e1989-b7db-4fee-86c3-59b075422455','7659a291-613e-44d7-a852-c4329da2f8bb','18a475b5-77b3-4c85-ba56-0e4998e60d83') and (Isdeleted is NULL or Isdeleted = 'False')) U INNER JOIN tblUsersLevels UL ON  UL.UserID = U.USerID INNER JOIN (Select * from tblAccountUserAssgn where AccountID='" & Request("AccID") & "' and LevelNo=" & Request("ProLevel") & " ) A ON A.UserID=U.USerID LEFT OUTER JOIN (Select * from tblSchedule where ScheduleDate = '" & ProcStartDate & "' ) S ON U.UserID=S.USerID LEFT OUTER  JOIN (Select 'Yes' as RecFound, userID, startdate, enddate, TypeOfLeave from tblLeave where (Isdeleted is NULL Or IsDeleted = 'False') and startDate <= '" & ProcStartDate & "' and endDate >= '" & ProcStartDate & "') L ON U.UserID=L.USerID " & JoinQuery & " LEFT OUTER JOIN (select sum(datediff(s,0,M.duration)) as DNMins, L.userid from tbltranscriptionstatus L, tbltranscriptionMain M where  datediff(hh, L.Datemodified, getdate()) <= " & hourdiff & "  and L.TranscriptionID= M.TranscriptionID  and L.Userlevel in (" & LvlNOAssgn & ") and L.status in (Select LevelNo from tblProductionlevels) group by L.UserID) DN ON DN.userid = U.USerID " & "LEFT OUTER JOIN (select sum(datediff(s,0,M.duration)) as CHMins, L.userid from tbltranscriptionlog L, tbltranscriptionMain M where L.TranscriptionID= M.TranscriptionID  and M.status = L.status and M.status in (" & LvlAssn & ")  and L.datemodified=(select max(datemodified)from tbltranscriptionlog where TranscriptionID= M.TranscriptionID and M.status = status and status in (" & LvlAssn & ") ) group by L.UserID) CH ON CH.userid = U.USerID " & "LEFT OUTER JOIN (select sum(datediff(s,0,M.duration)) as Mins, L.userid from tblUserPrLvlMgmt L, tbltranscriptionMain M where L.PhysicianID= M.DictatorID and  Direct='True' and M.status=L.LevelNo and M.status=" & LvlNo & " and  M.Submitdate >= '" & ServStartDate & "' and M.Submitdate <=  '" & ServEndDate & "'  group by L.UserID) DR ON DR.userid = U.USerID " & " where U.contractorid='" & Session("contractorid") & "' and   (U.Isdeleted is null or U.isdeleted = 'False') and dbo.chkLevel(UL.ProductionLevel, " & LvlNo & ")='True'   order by CHMins desc "
        'Else
        strQuery = "Select distinct U.FirstName +' ' + U.LastName as uname, U.Username, " & SelQuery & " DN.DNMins, CH.CHMins, DR.Mins as DIRMins, U.UserID, S.Mins as SCHMins, S.startTime, S.EndTime, L.RecFound, L.TypeOfLeave from (Select * from tblUsers where (Isdeleted is NULL) or (Isdeleted = 'False')) U INNER JOIN tblUsersLevels UL ON  UL.UserID = U.USerID INNER JOIN (Select * from tblAccountUserAssgn where AccountID='" & Request("AccID") & "' and LevelNo=" & Request("ProLevel") & " ) A ON A.UserID=U.USerID LEFT OUTER JOIN (Select * from tblSchedule where ScheduleDate = '" & ProcStartDate & "' ) S ON U.UserID=S.USerID LEFT OUTER  JOIN (Select 'Yes' as RecFound, userID, startdate, enddate, TypeOfLeave from tblLeave where (Isdeleted is NULL Or IsDeleted = 'False') and startDate <= '" & ProcStartDate & "' and endDate >= '" & ProcStartDate & "') L ON U.UserID=L.USerID " & JoinQuery & " LEFT OUTER JOIN (select sum(datediff(s,0,M.duration)) as DNMins, L.userid from tbltranscriptionstatus L, tbltranscriptionMain M where  datediff(hh, L.Datemodified, getdate()) <= " & hourdiff & "  and L.TranscriptionID= M.TranscriptionID  and L.Userlevel in (" & LvlNOAssgn & ") and L.status in (Select LevelNo from tblProductionlevels) group by L.UserID) DN ON DN.userid = U.USerID " & "LEFT OUTER JOIN (select sum(datediff(s,0,M.duration)) as CHMins, L.userid from tbltranscriptionlog L, tbltranscriptionMain M where L.TranscriptionID= M.TranscriptionID  and M.status = L.status and M.status in (" & LvlAssn & ")  and L.datemodified=(select max(datemodified)from tbltranscriptionlog where TranscriptionID= M.TranscriptionID and M.status = status and status in (" & LvlAssn & ") ) group by L.UserID) CH ON CH.userid = U.USerID " & "LEFT OUTER JOIN (select sum(datediff(s,0,M.duration)) as Mins, L.userid from tblUserPrLvlMgmt L, tbltranscriptionMain M where L.PhysicianID= M.DictatorID and  Direct='True' and M.status=L.LevelNo and M.status=" & LvlNo & " and  M.Submitdate >= '" & ServStartDate & "' and M.Submitdate <=  '" & ServEndDate & "'  group by L.UserID) DR ON DR.userid = U.USerID " & " where U.contractorid='" & Session("contractorid") & "' and   (U.Isdeleted is null or U.isdeleted = 'False') and dbo.chkLevel(UL.ProductionLevel, " & LvlNo & ")='True'   order by CHMins desc "
        'End If

        'Response.Write(SelQuery)
        'Response.Write(JoinQuery)
        'Response.Write(strQuery)
        'Response.End()

        Dim CMUser As New SqlCommand(strQuery, New SqlConnection(strConn))
        Try
            CMUser.CommandTimeout = 1200
            CMUser.Connection.Open()
            Dim DRRec1 As SqlDataReader = CMUser.ExecuteReader()
            If DRRec1.HasRows Then
                While (DRRec1.Read)
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
                    If DRRec1("RecFound").ToString <> "" Then
                        'RW1.BackColor = Drawing.Color.LightGray
                        'RW1.ForeColor = Drawing.Color.White
                        'CL1.Text = DRRec1("uname")
                        If DRRec1("TypeOfLeave").ToString = "HL" Or DRRec1("TypeOfLeave").ToString = "LWPHL" Then
                            RW1.BackColor = Drawing.Color.LightBlue
                            RW1.ForeColor = Drawing.Color.White
                            CL1.Text = DRRec1("uname")
                        Else
                            RW1.BackColor = Drawing.Color.LightGray
                            RW1.ForeColor = Drawing.Color.White
                            CL1.Text = DRRec1("uname")
                        End If
                    Else
                        CL1.Text = "<a href='EmpJobStatus.aspx?AccID=" & Request("AccID").ToString & "&ProLevel=" & Request("ProLevel") & "&Userid=" & DRRec1("Userid").ToString & "'  Target=_Blank>" & DRRec1("uname") & "</a>"
                    End If

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
                    RW1.Cells.Add(CL1)
                    RW1.Cells.Add(CL2)
                    CL3.BorderColor = Drawing.Color.DimGray
                    RW1.Cells.Add(CL3)
                    RW1.Cells.Add(STime)
                    RW1.Cells.Add(ETime)
                    For j = 1 To Incr

                        Dim CellD As New TableCell

                        If IsDBNull(DRRec1("DNMins" & j)) Then
                            CellD.Text = 0
                        Else

                            If IsDBNull(DRRec1("ADNMins" & j)) Then
                                CellD.Text = FormatNumber((DRRec1("DNMins" & j).ToString / 60), 0)
                            Else
                                CellD.Text = FormatNumber((DRRec1("DNMins" & j).ToString / 60), 0) & "(" & FormatNumber((DRRec1("ADNMins" & j).ToString / 60), 0) & ")"
                            End If
                        End If


                        RW1.Cells.Add(CellD)

                    Next
                    CL4.BorderColor = Drawing.Color.DimGray
                    RW1.Cells.Add(CL4)
                    For j = 1 To Incr
                        Dim CellC As New TableCell
                        If IsDBNull(DRRec1("CHMins" & j)) Then
                            CellC.Text = 0
                        Else
                            If IsDBNull(DRRec1("ACHMins" & j)) Then
                                CellC.Text = "<a href='JobsRouting.aspx?Userid=" & DRRec1("Userid").ToString & "&ProLevel=" & LvlA(j - 1) & " '  Target=_Blank>" & FormatNumber((DRRec1("CHMins" & j).ToString / 60), 0) & "</a>"
                            Else
                                CellC.Text = "<a href='JobsRouting.aspx?Userid=" & DRRec1("Userid").ToString & "&ProLevel=" & LvlA(j - 1) & " '  Target=_Blank>" & FormatNumber((DRRec1("CHMins" & j).ToString / 60), 0) & "(" & FormatNumber((DRRec1("ACHMins" & j).ToString / 60), 0) & ")" & "</a>"
                            End If

                        End If
                        RW1.Cells.Add(CellC)
                    Next
                    CL5.BorderColor = Drawing.Color.DimGray
                    CL6.BorderColor = Drawing.Color.DimGray
                    'CL7.BorderColor = Drawing.Color.DimGray
                    RW1.Cells.Add(CL5)
                    RW1.Cells.Add(CL6)
                    RW1.Cells.Add(CL7)
                    'Response.Write(RW1)

                    Table2.Rows.Add(RW1)
                    'Exit Sub
                End While
            End If
            DRRec1.Close()
        Finally
            If CMUser.Connection.State = ConnectionState.Open Then
                CMUser.Connection.Close()
            End If
        End Try
        'Response.Write(strQuery)
    End Sub
    Sub AllUser()
        hourdiff = DateDiff(DateInterval.Hour, ServStartDate, Now)
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
        LvlNo = Request("ProLevel")
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
        strQuery = "Select Levelname, LevelNo from tblProductionLevels where contractorid='" & Session("contractorid") & "' and JobsRouting = 'True' and LevelNo not in ('1073741824', '2147483647') "
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
                        JoinQuery = "LEFT OUTER JOIN (select sum(datediff(s,0,M.duration)) as DNMins" & Incr & ", L.userid from tbltranscriptionstatus L, tbltranscriptionMain M where  datediff(hh, L.Datemodified, getdate()) <= " & hourdiff & "  and L.TranscriptionID= M.TranscriptionID and L.Userlevel in (" & DRRec("LevelNo").ToString & ") and L.status in (Select LevelNo from tblProductionlevels where levelno not in (" & DRRec("LevelNo").ToString & ")) group by L.UserID) DN" & Incr & " ON DN" & Incr & ".userid = U.USerID "
                        JoinQuery = JoinQuery & "LEFT OUTER JOIN (select sum(datediff(s,0,M.duration)) as CHMins" & Incr & ", L.userid from tbltranscriptionlog L, tbltranscriptionMain M where L.TranscriptionID= M.TranscriptionID  and M.status = L.status and M.status in (" & LevelNo & ")  and L.datemodified=(select max(datemodified)from tbltranscriptionlog where TranscriptionID= M.TranscriptionID and M.status = status and status in (" & LevelNo & ") ) group by L.UserID) CH" & Incr & " ON CH" & Incr & ".userid = U.USerID "
                        LDone1.Text = "Finished[" & DRRec("LevelName").ToString & "]</td>"
                        LOut1.Text = "Assigned[" & DRRec("LevelName").ToString & "]</td>"
                        LvlAssn = LevelNo
                        LvlNOAssgn = DRRec("LevelNo").ToString
                    Else

                        SelQuery = SelQuery & "DN" & Incr & ".DNmins" & Incr & ", "
                        SelQuery = SelQuery & "CH" & Incr & ".CHmins" & Incr & ", "
                        JoinQuery = JoinQuery & "LEFT OUTER JOIN (select sum(datediff(s,0,M.duration)) as DNMins" & Incr & ", L.userid from tbltranscriptionstatus L, tbltranscriptionMain M where  datediff(hh, L.Datemodified, getdate()) <= " & hourdiff & " and L.TranscriptionID= M.TranscriptionID and L.Userlevel in (" & DRRec("LevelNo").ToString & ") and L.status in (Select LevelNo from tblProductionlevels where levelno not in (" & DRRec("LevelNo").ToString & ")) group by L.UserID) DN" & Incr & " ON DN" & Incr & ".userid = U.USerID "
                        JoinQuery = JoinQuery & "LEFT OUTER JOIN (select sum(datediff(s,0,M.duration)) as CHMins" & Incr & ", L.userid from tbltranscriptionlog L, tbltranscriptionMain M where L.TranscriptionID= M.TranscriptionID  and M.status = L.status and M.status in (" & LevelNo & ")  and L.datemodified=(select max(datemodified)from tbltranscriptionlog where TranscriptionID= M.TranscriptionID and M.status = status and status in (" & LevelNo & ") ) group by L.UserID) CH" & Incr & " ON CH" & Incr & ".userid = U.USerID "
                        ReDim Preserve Lvl(i)
                        ReDim Preserve LvlA(i)
                        LDone1.Text = LDone1.Text & "<td>Finished[" & DRRec("LevelName").ToString & "]</td>"
                        LOut1.Text = LOut1.Text & "<td>Assigned[" & DRRec("LevelName").ToString & "]</td>"
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
        LDone1.Text = LDone1.Text & "<td>Finished[Total]"
        LOut1.Text = LOut1.Text & "<td>Assigned[Total]"
        'Response.Write(LDone.Text)
        'CellMdone.ColumnSpan = i + 1
        'CellCout.ColumnSpan = i + 1

        'strQuery = "Select distinct U.FirstName +' ' + U.LastName as uname, U.Username, U.UserID, S.Mins as SCHMins, S.startTime, S.EndTime, L.RecFound from (Select * from tblUsers where (Isdeleted is NULL) or (Isdeleted = 'False')) U, tblUsersLevels UL, tblSchedule S, (Select 'Yes' as RecFound, userID, startdate, enddate from tblLeave) L where   dbo.chkLevel(UL.ProductionLevel, " & LvlNo & ")='True'  and L.startDate <= '" & ProcStartDate & "' and L.endDate >= '" & ProcStartDate & "' and S.ScheduleDate = '" & ProcStartDate & "' and  UL.UserID = U.USerID and U.UserID*=S.USerID  and U.UserID*=L.USerID order by RecFound, uname"
        strQuery = "Select distinct U.FirstName +' ' + U.LastName as uname, U.Username, " & SelQuery & " DN.DNMins, CH.CHMins, DR.Mins as DIRMins, U.UserID, S.Mins as SCHMins, S.startTime, S.EndTime, L.RecFound,L.TypeOfLeave from (Select * from tblUsers where (Isdeleted is NULL) or (Isdeleted = 'False')) U INNER JOIN tblUsersLevels UL ON  UL.UserID = U.USerID LEFT OUTER JOIN (Select * from tblSchedule where ScheduleDate = '" & ProcStartDate & "' ) S ON U.UserID=S.USerID LEFT OUTER  JOIN (Select 'Yes' as RecFound, userID, startdate, enddate,TypeOfLeave from tblLeave where (Isdeleted is NULL Or IsDeleted = 'False') and startDate <= '" & ProcStartDate & "' and endDate >= '" & ProcStartDate & "') L ON U.UserID=L.USerID " & JoinQuery & " LEFT OUTER JOIN (select sum(datediff(s,0,M.duration)) as DNMins, L.userid from tbltranscriptionstatus L, tbltranscriptionMain M where  datediff(hh, L.Datemodified, getdate()) <= " & hourdiff & " and L.TranscriptionID= M.TranscriptionID  and L.Userlevel in (" & LvlNOAssgn & ") and L.status in (Select LevelNo from tblProductionlevels) group by L.UserID) DN ON DN.userid = U.USerID " & "LEFT OUTER JOIN (select sum(datediff(s,0,M.duration)) as CHMins, L.userid from tbltranscriptionlog L, tbltranscriptionMain M where L.TranscriptionID= M.TranscriptionID  and M.status = L.status and M.status in (" & LvlAssn & ")  and L.datemodified=(select max(datemodified)from tbltranscriptionlog where TranscriptionID= M.TranscriptionID and M.status = status and status in (" & LvlAssn & ") ) group by L.UserID) CH ON CH.userid = U.USerID " & "LEFT OUTER JOIN (select sum(datediff(s,0,M.duration)) as Mins, L.userid from tblUserPrLvlMgmt L, tbltranscriptionMain M where L.PhysicianID= M.DictatorID and  Direct='True' and M.status=L.LevelNo and M.status=" & LvlNo & " and  M.Submitdate >= '" & ServStartDate & "' and M.Submitdate <=  '" & ServEndDate & "'  group by L.UserID) DR ON DR.userid = U.USerID " & " where  U.contractorid='" & Session("contractorid") & "' and  (U.Isdeleted is null or U.isdeleted = 'False') and   dbo.chkLevel(UL.ProductionLevel, " & LvlNo & ")='True'   order by CHMins desc "
        'Response.Write(SelQuery)
        'Response.Write(JoinQuery)
        'Response.Write(strQuery)
        'Response.End()

        Dim CMUser As New SqlCommand(strQuery, New SqlConnection(strConn))
        Try
            CMUser.Connection.Open()
            Dim DRRec1 As SqlDataReader = CMUser.ExecuteReader()
            If DRRec1.HasRows Then
                While (DRRec1.Read)
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
                    If DRRec1("RecFound").ToString <> "" Then
                        'RW1.BackColor = Drawing.Color.LightGray
                        'RW1.ForeColor = Drawing.Color.White
                        'CL1.Text = DRRec1("uname")
                        If DRRec1("TypeOfLeave").ToString = "HL" Or DRRec1("TypeOfLeave").ToString = "LWPHL" Then
                            RW1.BackColor = Drawing.Color.LightBlue
                            RW1.ForeColor = Drawing.Color.White
                            CL1.Text = DRRec1("uname")
                        Else
                            RW1.BackColor = Drawing.Color.LightGray
                            RW1.ForeColor = Drawing.Color.White
                            CL1.Text = DRRec1("uname")
                        End If
                    Else
                        CL1.Text = "<a href='EmpJobStatus.aspx?AccID=" & Request("AccID").ToString & "&ProLevel=" & Request("ProLevel") & "&Userid=" & DRRec1("Userid").ToString & "'  Target=_Blank>" & DRRec1("uname") & "</a>"
                    End If

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
                    RW1.Cells.Add(CL1)
                    RW1.Cells.Add(CL2)
                    CL3.BorderColor = Drawing.Color.DimGray
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
                    CL4.BorderColor = Drawing.Color.DimGray
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
                    CL5.BorderColor = Drawing.Color.DimGray
                    CL6.BorderColor = Drawing.Color.DimGray
                    'CL7.BorderColor = Drawing.Color.DimGray
                    RW1.Cells.Add(CL5)
                    RW1.Cells.Add(CL6)
                    RW1.Cells.Add(CL7)
                    'Response.Write(RW1)

                    Table4.Rows.Add(RW1)
                    'Exit Sub
                End While
            End If
            DRRec1.Close()
        Finally
            If CMUser.Connection.State = ConnectionState.Open Then
                CMUser.Connection.Close()
            End If
        End Try
        'Response.Write(strQuery)
    End Sub
    Protected Function chkLevel(ByVal AdminLevel As Long, ByVal Level As Long) As Boolean
        If (AdminLevel And Level) = Level Then
            chkLevel = True
        Else
            chkLevel = False
        End If
    End Function

    Protected Sub lblDsp_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lblDsp.Click

        If lblDsp.Text = "Show Assigned Users" Then
            Table4.Visible = False
            Table2.Visible = True
            lblDsp.Text = "Show All Users"
            AssignUser()
        ElseIf lblDsp.Text = "Show All Users" Then
            Table2.Visible = False
            Table4.Visible = True
            lblDsp.Text = "Show Assigned Users"
            AllUser()
        End If
    End Sub
End Class

