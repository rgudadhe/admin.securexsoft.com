Imports System.Data.SqlClient

Partial Class RoutingTool_Default
    Inherits BasePage

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
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

        If Not IsPostBack Then
            Table4.Visible = False
            LblDsp.Text = "Show All Users"
            AssignUser()
        End If



    End Sub
    Sub AssignUser()
        Dim strConn As String
        Dim strQuery As String
        Dim scdMins As Integer
        Dim MinsDone As Integer
        Dim MinsAssn As Integer
        Dim MinsPend As Integer
        Dim DirMins As Integer


        strConn = System.Configuration.ConfigurationManager.AppSettings("ETSCon")
        Dim Userid As String

        strQuery = "Select U.FirstName +' ' + U.LastName as uname, U.Username, U.UserID, S.SchMins from (Select * from tblUsers where (Isdeleted is NULL) or (Isdeleted = 'False')) U, tblAccountUserAssgn A, tblUsersLevels UL, tblUsersSchMins S where dbo.chkLevel(UL.ProductionLevel, " & Request("ProLevel") & ")='True' and A.LevelNo=" & Request("ProLevel") & " and   UL.UserID = U.USerID and A.UserID=U.USerID and AccountID='" & Request("AccID") & "' and U.UserID*=S.USerID and S.schDate = '1/9/2008' order by U.Firstname"
        Dim CMUser As New SqlCommand(strQuery, New SqlConnection(strConn))
        CMUser.Connection.Open()
        Dim SQLCmd1 As SqlDataReader = CMUser.ExecuteReader()
        If SQLCmd1.HasRows Then
            While (SQLCmd1.Read)
                Dim CL1 As New TableCell
                Dim CL2 As New TableCell
                Dim CL3 As New TableCell
                Dim CL4 As New TableCell
                Dim RW1 As New TableRow
                Dim CL6 As New TableCell
                Dim CL7 As New TableCell

                Userid = SQLCmd1("UserID").ToString
                CL1.Text = "<a href='EmpJobStatus.aspx?AccID=" & Request("AccID").ToString & "&ProLevel=" & Request("ProLevel") & "&Userid=" & SQLCmd1("Userid").ToString & "'  Target=_Blank>" & SQLCmd1("uname") & "</a>"
                CL2.Text = SQLCmd1("username")
                If IsDBNull(SQLCmd1("SchMins")) Then
                    scdMins = 0
                    CL3.Text = 0
                Else
                    scdMins = SQLCmd1("SchMins").ToString
                    CL3.Text = SQLCmd1("SchMins").ToString
                End If


                strQuery = "Select sum(Mins) as Mins from (select distinct L.TranscriptionID, datediff(s,0,M.duration)  as Mins from tbltranscriptionlog L, tbltranscriptionMain M where L.TranscriptionID= M.TranscriptionID and L.userid='" & Userid & "' and L.Userlevel = " & Request("ProLevel") & " and L.status in (Select LevelNo from tblProductionlevels where levelno <> " & Request("ProLevel") & ")) as DT"
                Dim CMUser1 As New SqlCommand(strQuery, New SqlConnection(strConn))
                CMUser1.Connection.Open()
                Dim SQLCmd2 As SqlDataReader = CMUser1.ExecuteReader()
                If SQLCmd2.HasRows Then
                    While (SQLCmd2.Read)
                        If IsDBNull(SQLCmd2("Mins")) Then
                            MinsDone = 0
                            CL4.Text = 0
                        Else
                            MinsDone = FormatNumber((SQLCmd2("Mins").ToString / 60), 0)
                            CL4.Text = FormatNumber((SQLCmd2("Mins").ToString / 60), 0)
                        End If


                        'Response.Write(SQLCmd2("Mins").ToString)
                    End While
                End If
                SQLCmd2.Close()
                CMUser1.Connection.Close()
                Dim realStatus As Integer
                realStatus = 100 + CInt(Request("prolevel"))




                strQuery = "Select sum(Mins) as Mins from (select distinct L.TranscriptionID, datediff(s,0,M.duration)  as Mins from tbltranscriptionlog L, tbltranscriptionMain M where L.TranscriptionID= M.TranscriptionID and L.userid='" & Userid & "' and L.status = " & realStatus & " ) as DT"
                'Response.Write(strQuery)
                Dim CL5 As New TableCell
                Dim CMUser2 As New SqlCommand(strQuery, New SqlConnection(strConn))
                CMUser2.Connection.Open()
                Dim SQLCmd3 As SqlDataReader = CMUser2.ExecuteReader()
                If SQLCmd3.HasRows Then
                    While (SQLCmd3.Read)

                        If IsDBNull(SQLCmd3("Mins")) Then
                            MinsAssn = 0
                            CL5.Text = 0
                        Else
                            MinsAssn = FormatNumber((SQLCmd3("Mins").ToString / 60), 0)
                            CL5.Text = FormatNumber((SQLCmd3("Mins").ToString / 60), 0)
                        End If
                        'Response.Write(SQLCmd2("Mins").ToString)
                    End While
                End If
                SQLCmd3.Close()
                CMUser2.Connection.Close()

                If scdMins > 0 Then
                    MinsPend = scdMins - (MinsDone + MinsAssn)
                    If MinsPend < 0 Then
                        MinsPend = 0
                    End If
                Else
                    MinsPend = 0
                End If

                strQuery = "Select sum(Mins) as Mins from (select distinct M.TranscriptionID, datediff(s,0,M.duration)  as Mins from tblUserPrLvlMgmt L, tbltranscriptionMain M where L.PhysicianID= M.DictatorID and L.userid='" & Userid & "' and Direct='True' and M.status = " & Request("ProLevel") & " and M.status=L.LevelNo and M.AccountID='" & Request("AccID") & "' and submitdate >= '1/9/2008' and submitdate < '1/10/2008' ) as DT"

                'Response.Write(strQuery)
                Dim CMUser3 As New SqlCommand(strQuery, New SqlConnection(strConn))
                CMUser3.Connection.Open()
                Dim SQLCmd4 As SqlDataReader = CMUser3.ExecuteReader()
                If SQLCmd4.HasRows Then
                    While (SQLCmd4.Read)

                        If IsDBNull(SQLCmd4("Mins")) Then
                            DirMins = 0
                            CL7.Text = 0
                        Else
                            DirMins = FormatNumber((SQLCmd4("Mins").ToString / 60), 0)
                            CL7.Text = FormatNumber((SQLCmd4("Mins").ToString / 60), 0)
                        End If

                    End While
                End If

                SQLCmd4.Close()
                CMUser3.Connection.Close()


                CL7.Text = DirMins
                CL6.Text = MinsPend


                RW1.Cells.Add(CL1)
                RW1.Cells.Add(CL2)
                RW1.Cells.Add(CL3)
                RW1.Cells.Add(CL4)
                RW1.Cells.Add(CL5)
                RW1.Cells.Add(CL6)
                RW1.Cells.Add(CL7)

                Table2.Rows.Add(RW1)

            End While
        End If
    End Sub
    Sub AllUser()
        Dim strConn As String
        Dim strQuery As String
        strConn = System.Configuration.ConfigurationManager.AppSettings("ETSCon")
        Dim Userid As String
        Dim scdMins As Integer
        Dim MinsDone As Integer
        Dim MinsAssn As Integer
        Dim MinsPend As Integer
        Dim DirMins As Integer
        strQuery = "Select U.FirstName +' ' + U.LastName as uname, U.Username, U.UserID, S.SchMins from tblUsers U, tblUsersLevels UL, tblUsersSchMins S where dbo.chkLevel(UL.ProductionLevel, " & Request("ProLevel") & ")='True'  and   UL.UserID = U.USerID and  U.UserID*=S.USerID and S.schDate = '1/9/2008' order by U.Firstname"
        'Response.Write(strQuery)
        Dim CMUser As New SqlCommand(strQuery, New SqlConnection(strConn))
        CMUser.Connection.Open()
        Dim SQLCmd1 As SqlDataReader = CMUser.ExecuteReader()
        If SQLCmd1.HasRows Then
            While (SQLCmd1.Read)
                Dim CL1 As New TableCell
                Dim CL2 As New TableCell
                Dim CL3 As New TableCell
                Dim CL4 As New TableCell
                Dim CL6 As New TableCell
                Dim CL7 As New TableCell


                Dim RW1 As New TableRow
                Userid = SQLCmd1("UserID").ToString

                CL1.Text = "<a href='EmpJobStatus.aspx?AccID=" & Request("AccID").ToString & "&ProLevel=" & Request("ProLevel") & "&Userid=" & SQLCmd1("Userid").ToString & "'  Target=_Blank>" & SQLCmd1("uname") & "</a>"
                CL2.Text = SQLCmd1("username")
                If IsDBNull(SQLCmd1("SchMins")) Then
                    scdMins = 0
                    CL3.Text = 0
                Else
                    scdMins = SQLCmd1("SchMins").ToString
                    CL3.Text = SQLCmd1("SchMins").ToString
                End If

                strQuery = "Select sum(Mins) as Mins from (select distinct L.TranscriptionID, datediff(s,0,M.duration)/60  as Mins from tbltranscriptionlog L, tbltranscriptionMain M where L.TranscriptionID= M.TranscriptionID and L.userid='" & Userid & "' and L.Userlevel = " & Request("ProLevel") & " and L.status in (Select LevelNo from tblProductionlevels where levelno <> " & Request("ProLevel") & ")) as DT"
                Dim CMUser1 As New SqlCommand(strQuery, New SqlConnection(strConn))
                CMUser1.Connection.Open()
                Dim SQLCmd2 As SqlDataReader = CMUser1.ExecuteReader()
                If SQLCmd2.HasRows Then
                    While (SQLCmd2.Read)
                        If IsDBNull(SQLCmd2("Mins")) Then
                            MinsDone = 0
                            CL4.Text = 0
                        Else
                            MinsDone = SQLCmd2("Mins").ToString
                            CL4.Text = SQLCmd2("Mins").ToString
                        End If

                        'Response.Write(SQLCmd2("Mins").ToString)
                    End While
                End If
                SQLCmd2.Close()
                CMUser1.Connection.Close()
                Dim realStatus As Integer
                realStatus = 100 + CInt(Request("prolevel"))

                strQuery = "Select sum(Mins) as Mins from (select distinct L.TranscriptionID, datediff(s,0,M.duration)/60  as Mins from tbltranscriptionlog L, tbltranscriptionMain M where L.TranscriptionID= M.TranscriptionID and L.userid='" & Userid & "' and L.status = " & realStatus & " ) as DT"
                'Response.Write(strQuery)
                Dim CL5 As New TableCell
                Dim CMUser2 As New SqlCommand(strQuery, New SqlConnection(strConn))
                CMUser2.Connection.Open()
                Dim SQLCmd3 As SqlDataReader = CMUser2.ExecuteReader()
                If SQLCmd3.HasRows Then
                    While (SQLCmd3.Read)

                        If IsDBNull(SQLCmd3("Mins")) Then
                            MinsAssn = 0
                            CL5.Text = 0
                        Else
                            MinsAssn = SQLCmd3("Mins").ToString
                            CL5.Text = SQLCmd3("Mins").ToString
                        End If

                    End While
                End If

                SQLCmd3.Close()
                CMUser2.Connection.Close()

                strQuery = "Select sum(Mins) as Mins from (select distinct M.TranscriptionID, datediff(s,0,M.duration)/60  as Mins from tblUserPrLvlMgmt L, tbltranscriptionMain M where L.PhysicianID= M.DictatorID and L.userid='" & Userid & "' and Direct='True' and M.status = " & Request("ProLevel") & " and M.status=L.LevelNo and M.AccountID='" & Request("AccID") & "' and submitdate >= '1/9/2008' and submitdate < '1/10/2008' ) as DT"

                'Response.Write(strQuery)
                Dim CMUser3 As New SqlCommand(strQuery, New SqlConnection(strConn))
                CMUser3.Connection.Open()
                Dim SQLCmd4 As SqlDataReader = CMUser3.ExecuteReader()
                If SQLCmd4.HasRows Then
                    While (SQLCmd4.Read)

                        If IsDBNull(SQLCmd4("Mins")) Then
                            DirMins = 0
                            CL7.Text = 0
                        Else
                            DirMins = SQLCmd4("Mins").ToString
                            CL7.Text = SQLCmd4("Mins").ToString
                        End If

                    End While
                End If

                SQLCmd4.Close()
                CMUser3.Connection.Close()

                If scdMins > 0 Then
                    MinsPend = scdMins - (MinsDone + MinsAssn)
                    If MinsPend < 0 Then
                        MinsPend = 0
                    End If
                Else
                    MinsPend = 0
                End If

                CL6.Text = MinsPend
                CL7.Text = DirMins

                RW1.Cells.Add(CL1)
                RW1.Cells.Add(CL2)
                RW1.Cells.Add(CL3)
                RW1.Cells.Add(CL4)
                RW1.Cells.Add(CL5)
                RW1.Cells.Add(CL6)
                RW1.Cells.Add(CL7)

                Table4.Rows.Add(RW1)

            End While
        End If
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

